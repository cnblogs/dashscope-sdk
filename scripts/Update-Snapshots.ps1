<#
.SYNOPSIS
    Replays a snapshot case's request against the real DashScope API and
    refreshes the three derived files (request.header.txt, response.header.txt,
    response.body.txt). You only edit <case>.request.body.json by hand.

.DESCRIPTION
    The test fixtures under test/Cnblogs.DashScope.Tests.Shared/RawHttpData/
    each consist of four files. The request body is hand-maintained; this script
    re-sends it to the live DashScope API and regenerates the other three so the
    fixtures stay consistent with what the SDK actually sends/expects.

    GET cases (e.g. get-task-*) have no .request.body.json; their URL is replayed
    as-is from request.header.txt. Note that task queries only work within the
    task retention window (24h), and each poll returns a fresh request id, so
    replaying a GET task case means syncing the ResponseModel request id again.

    Authorization is taken ONLY from $env:DASHSCOPE_API_KEY (or -ApiKey), never
    from the stale token inside the existing request.header.txt.

    With -Socket, websocket groups (socket-<group>.<message>.json) are replayed
    instead: the hand-maintained run-task/continue-task/finish-task request
    messages drive one live session, and the captured server events are written
    back verbatim as socket-<group>.<event>.json, the audio as
    socket-<group>.audio.<format> (format taken from run-task parameters).

    For speech recognition (ASR) groups, the audio flows the other way: put the
    input audio in socket-<group>.input.<format> and the script sends it as
    binary frames (3200-byte chunks) after task-started, before finish-task.
    Repeated server events are kept as socket-<group>.<event>.json,
    socket-<group>.<event>-2.json, ... in arrival order (e.g. the intermediate
    and final result-generated events of one sentence).

.PARAMETER Case
    One or more case names (without the .request.body.json suffix) to refresh.
    With -Socket, group names without the "socket-" prefix (e.g. speech-synthesizer).
    Omit and leave -All unset to choose interactively from git-modified bodies.

.PARAMETER All
    Refresh every case that has a .request.body.json under -DataDir.
    With -Socket, refresh every socket-* group.

.PARAMETER WorkspaceId
    Resolves the {WorkspaceId} placeholder of workspace domain request lines
    ({WorkspaceId}.{region}.maas.aliyuncs.com). Defaults to $env:DASHSCOPE_WORKSPACE_ID.
    The placeholder is written back into request.header.txt, so committed fixtures
    never carry the real workspace subdomain.

.PARAMETER DataDir
    RawHttpData directory. Defaults to test/Cnblogs.DashScope.Tests.Shared/RawHttpData
    relative to the repo root (auto-located from this script's path).

.PARAMETER RedactAuth
    Write "Authorization: Bearer <redacted>" into request.header.txt. Tests never
    validate the token, so this is safe and avoids leaking keys.
    Not applicable with -Socket: websocket auth lives in the handshake only and
    is never persisted.

.PARAMETER ApiKey
    Override $env:DASHSCOPE_API_KEY.

.PARAMETER Socket
    Replay websocket (socket-*) groups instead of HTTP cases.

.PARAMETER SocketUri
    Websocket endpoint. Defaults to wss://dashscope.aliyuncs.com/api-ws/v1/inference/
    (with -RealtimeTts / -RealtimeAsr / -RealtimeAudio: wss://dashscope.aliyuncs.com/api-ws/v1/realtime,
    without query; with -RealtimeOmni: the workspace domain of -Region). A {WorkspaceId}
    placeholder is resolved from -WorkspaceId, the placeholder itself is never persisted.

.PARAMETER Region
    The region of the workspace domain, cn-beijing by default. Picked when -RealtimeOmni
    builds its default -SocketUri.

.PARAMETER RealtimeTts
    Replay TTS realtime (qwen3-tts-*-realtime) socket groups instead of HTTP cases. The group
    files are socket-<group>.session-update.json / .append-text.json / .commit-text.json /
    .finish-session.json (hand-written client events); server events are written back verbatim
    as socket-<group>.<type>.json (repeated events as <type>-2.json, ... in arrival order) and
    the decoded audio as socket-<group>.audio.<format>. Requires -Model (the ?model= query
    parameter of the realtime endpoint).

.PARAMETER RealtimeAudio
    Replay audio chat / live translation (qwen-audio-*-realtime,
    qwen*-livetranslate-*-realtime) socket groups instead of HTTP cases. Same group files as
    -RealtimeAsr plus an optional socket-<group>.response-create.json (sent after
    input_audio_buffer.committed, for the push-to-talk mode). The session ends at
    session.finished or at response.done (whichever arrives); the repeated delta events of
    a stream (response.audio.delta, *.transcription.* delta / text, ...) are trimmed to their
    first and last occurrences. Requires -Model.

.PARAMETER RealtimeOmni
    Replay omni realtime (qwen3.8-omni-flash-realtime, qwen3.5-omni-*-realtime) socket groups
    instead of HTTP cases. The hand-maintained client events drive one live session:
    socket-<group>.session-update.json (required, sent on session.created), the input audio
    socket-<group>.input.<format> (appended as base64 chunks of 3200 bytes on session.updated,
    followed by two seconds of silence when the VAD stays on - it only detects the end of a
    speech after some silence), an optional socket-<group>.image.jpg (appended right after the
    audio), an optional socket-<group>.commit-audio.json (manual mode, sent after the appends)
    and socket-<group>.response-create.json (sent after input_audio_buffer.committed).
    socket-<group>.item-create.json holding a message item (text input) is sent once the session
    is configured, the response.create follows its confirmation. A response.done carrying
    function_call items is answered with socket-<group>.item-create.json ({call_id} is replaced
    by the live call id and written back) and followed by another response.create once the
    output was confirmed; a mcp_approval_request item is answered with
    socket-<group>.mcp-approval-response.json ({approval_request_id} replaced likewise); a
    response.done carrying mcp_call items is followed by socket-<group>.response-create-after-mcp.json
    once; socket-<group>.response-cancel.json is sent when the first content part of a response
    starts. The session ends at the response.done that needed no follow-up. Repeated delta
    events are trimmed to their first and last occurrences. Requires -Model.

.PARAMETER Model
    The model name for the ?model= query parameter, required with -RealtimeTts,
    -RealtimeAsr, -RealtimeAudio and -RealtimeOmni, e.g. qwen3-tts-flash-realtime.

.PARAMETER TrimAudioDeltas
    HTTP SSE cases only: streams that answer with audio (e.g. qwen3-livetranslate-flash on
    the OpenAI compatible endpoint) send hundreds of chunks carrying large base64 payloads.
    Keep the chunks outside the audio as they are, and only the first and the last chunk of
    each run of consecutive audio chunks, with the base64 payload replaced by "<base64>".

.EXAMPLE
pwsh ./scripts/Update-Snapshots.ps1 -Case openai-compatible-livetranslate-sse -RedactAuth -TrimAudioDeltas

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1
    # interactively pick from git-modified *.request.body.json files

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1 -Case single-generation-message-sse -RedactAuth

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1 -Socket -Case speech-synthesizer

.EXAMPLE
pwsh ./scripts/Update-Snapshots.ps1 -RealtimeTts -Model qwen3-tts-flash-realtime -Case qwen-tts-realtime

.EXAMPLE
pwsh ./scripts/Update-Snapshots.ps1 -RealtimeOmni -Model qwen3.8-omni-flash-realtime -Case qwen-omni-realtime,qwen-omni-realtime-manual
#>
[CmdletBinding()]
param(
    [Parameter(Position = 0, ValueFromRemainingArguments)]
    [string[]]$Case,

    [switch]$All,

    [string]$DataDir,

    [switch]$RedactAuth,

    [string]$ApiKey,

    [string]$WorkspaceId,

    [string]$Region = 'cn-beijing',

    [switch]$Socket,

    [string]$SocketUri = 'wss://dashscope.aliyuncs.com/api-ws/v1/inference/',

    [switch]$RealtimeTts,

    [switch]$RealtimeAsr,

    [switch]$RealtimeAudio,

    [switch]$RealtimeOmni,

    [string]$Model,

    [switch]$TrimAudioDeltas
)

$ErrorActionPreference = 'Stop'

# accept comma separated names as well ("-Case a,b"): PowerShell only turns the commas into an
# array by itself when the command is typed into it, not when pwsh is launched from another shell
if ($Case) {
    $Case = @($Case | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim() } | Where-Object { $_ })
}

# ---------- locate repo root ----------
$repoRoot = $PSScriptRoot
while ($repoRoot -and -not (Test-Path (Join-Path $repoRoot '.git'))) {
    $parent = Split-Path $repoRoot -Parent
    if ($parent -eq $repoRoot) { break }
    $repoRoot = $parent
}
if (-not (Test-Path (Join-Path $repoRoot '.git'))) {
    throw "Could not locate repo root (.git) from '$PSScriptRoot'. Pass -DataDir explicitly."
}

if (-not $DataDir) {
    $DataDir = Join-Path $repoRoot 'test/Cnblogs.DashScope.Tests.Shared/RawHttpData'
}
if (-not (Test-Path $DataDir)) { throw "DataDir not found: $DataDir" }

# ---------- api key ----------
if (-not $ApiKey) { $ApiKey = $env:DASHSCOPE_API_KEY }
if (-not $ApiKey) {
    throw "No API key. Set `$env:DASHSCOPE_API_KEY or pass -ApiKey."
}

# ---------- workspace id ----------
# APIs on the workspace domain ({WorkspaceId}.{region}.maas.aliyuncs.com) keep a
# {WorkspaceId} placeholder in the committed request.header.txt; the placeholder is
# resolved from here when replaying and written back when the header file is refreshed.
if (-not $WorkspaceId) { $WorkspaceId = $env:DASHSCOPE_WORKSPACE_ID }

# ---------- headers we never replay (HttpClient computes/host-specific) ----------
$skipHeaders = @(
    'host', 'content-length', 'connection', 'accept-encoding',
    'postman-token', 'cache-control', 'user-agent', 'request-start-time',
    'authorization'
)

function Get-AbsoluteUrl {
    param([string]$RequestLine, [hashtable]$Headers)

    # $RequestLine like "POST https://..." or "POST /path HTTP/1.1"
    $parts = $RequestLine -split ' ', 3
    if ($parts.Count -lt 2) { throw "Cannot parse request line: '$RequestLine'" }
    $url = $parts[1]
    if ($url -match '^https?://') {
        if ($url.Contains('{WorkspaceId}')) {
            if (-not $WorkspaceId) {
                throw "URL '$url' contains the {WorkspaceId} placeholder. Set `$env:DASHSCOPE_WORKSPACE_ID or pass -WorkspaceId."
            }
            $url = $url.Replace('{WorkspaceId}', $WorkspaceId)
        }
        return $url
    }

    # relative path -> combine with Host header
    $hostName = $Headers['host']
    if (-not $hostName) { throw "Relative URL '$url' but no Host header." }
    $scheme = 'https'
    return "${scheme}://${hostName}${url}"
}

function Get-RedactedUrl {
    param([string]$Url)

    # write the workspace id back as a placeholder so committed fixtures do not
    # carry the real workspace subdomain
    if ($WorkspaceId) {
        return $Url -replace "(?<=^https?://)$([regex]::Escape($WorkspaceId))\.", '{WorkspaceId}.'
    }
    return $Url
}

function Parse-RequestHeader {
    param([string]$Path)
    # @() keeps single-line files (request line only) addressable as an array.
    $lines = @(Get-Content -LiteralPath $Path)
    $headers = @{}
    for ($i = 1; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        if ([string]::IsNullOrWhiteSpace($line)) { continue }
        $idx = $line.IndexOf(':')
        if ($idx -le 0) { continue }
        $name = $line.Substring(0, $idx).Trim()
        $value = $line.Substring($idx + 1).Trim()
        $headers[$name] = $value
    }
    return [pscustomobject]@{ RequestLine = $lines[0]; Headers = $headers }
}

function Select-CasesInteractive {
    # Use git status to find changed request files (bodies or headers; GET cases have no body).
    $status = git -C $repoRoot status --porcelain (Join-Path $repoRoot '*' 2>$null) 2>$null
    # The pathspec with repoRoot may be ignored; fall back to plain status.
    if (-not $status) { $status = git -C $repoRoot status --porcelain }
    $changed = @(
        $status | ForEach-Object {
            # porcelain format: "XY path" (path may be quoted for special chars)
            $raw = $_
            # strip leading 2-char status + space
            $p = $raw.Substring(3).Trim('"')
            $p
        } | Where-Object { $_ -match '\.request\.(body\.json|header\.txt)$' }
    )

    if (-not $changed) {
        Write-Host "No modified *.request.body.json found by git. Pass -Case or -All." -ForegroundColor Yellow
        exit 1
    }

    # normalize to case names, dedup
    $names = @(
        $changed | ForEach-Object {
            $fn = Split-Path $_ -Leaf
            $fn -replace '\.request\.body\.json$', ''
        } | Sort-Object -Unique
    )

    Write-Host "`nModified request bodies:" -ForegroundColor Cyan
    for ($i = 0; $i -lt $names.Count; $i++) {
        Write-Host ("  [{0,2}] {1}" -f $i, $names[$i])
    }
    Write-Host ""
    $answer = Read-Host "Enter indices (comma/space separated), 'all', or 'q' to quit"
    $answer = $answer.Trim()
    if (-not $answer -or $answer -eq 'q') { Write-Host "Cancelled."; exit 0 }
    if ($answer -eq 'all') { return ,$names }

    $picked = @()
    foreach ($tok in ($answer -split '[ ,]+') | Where-Object { $_ -ne '' }) {
        if ($tok -match '^\d+$') {
            $n = [int]$tok
            if ($n -ge 0 -and $n -lt $names.Count) { $picked += $names[$n] }
            else { Write-Host "  ignoring out-of-range index $tok" -ForegroundColor Yellow }
        }
    }
    if (-not $picked) { Write-Host "Nothing selected."; exit 0 }
    return ,$picked
}

function Resolve-Cases {
    if ($All) {
        # every case has a .request.header.txt (GET cases have no body file)
        $all = @(Get-ChildItem -LiteralPath $DataDir -Filter '*.request.header.txt' |
                ForEach-Object { $_.BaseName -replace '\.request\.header$', '' } |
                Sort-Object -Unique)
        if (-not $all) { Write-Host "No *.request.header.txt in $DataDir" -ForegroundColor Yellow; exit 0 }
        return ,$all
    }
    if ($Case -and $Case.Count) { return ,$Case }
    return ,(Select-CasesInteractive)
}

# the {WorkspaceId} placeholder of a websocket uri resolves like the one of the request lines
if ($SocketUri.Contains('{WorkspaceId}')) {
    if (-not $WorkspaceId) {
        throw "The websocket uri contains the {WorkspaceId} placeholder. Set `$env:DASHSCOPE_WORKSPACE_ID or pass -WorkspaceId."
    }
    $SocketUri = $SocketUri.Replace('{WorkspaceId}', $WorkspaceId)
}

# ---------- websocket replay (socket-<group>.<message>.json) ----------

function Select-SocketCasesInteractive {
    $status = git -C $repoRoot status --porcelain 2>$null
    $names = @(
        $status | ForEach-Object { $_.Substring(3).Trim('"') } |
            Where-Object { $_ -match '^.*socket-[^/\\]+\.json$' } |
            ForEach-Object { (Split-Path $_ -Leaf) -replace '^socket-', '' -replace '\..*$', '' } |
            Sort-Object -Unique
    )
    if (-not $names) {
        Write-Host "No modified socket-*.json found by git. Pass -Case <group> or -All." -ForegroundColor Yellow
        exit 1
    }
    Write-Host "`nModified socket groups:" -ForegroundColor Cyan
    for ($i = 0; $i -lt $names.Count; $i++) {
        Write-Host ("  [{0,2}] {1}" -f $i, $names[$i])
    }
    Write-Host ""
    $answer = (Read-Host "Enter indices (comma/space separated), 'all', or 'q' to quit").Trim()
    if (-not $answer -or $answer -eq 'q') { Write-Host "Cancelled."; exit 0 }
    if ($answer -eq 'all') { return ,$names }
    $picked = @()
    foreach ($tok in ($answer -split '[ ,]+') | Where-Object { $_ -ne '' }) {
        if ($tok -match '^\d+$') {
            $n = [int]$tok
            if ($n -ge 0 -and $n -lt $names.Count) { $picked += $names[$n] }
            else { Write-Host "  ignoring out-of-range index $tok" -ForegroundColor Yellow }
        }
    }
    if (-not $picked) { Write-Host "Nothing selected."; exit 0 }
    return ,$picked
}

function Resolve-SocketCases {
    if ($All) {
        $groups = @(Get-ChildItem -LiteralPath $DataDir -Filter 'socket-*.run-task.json' |
                ForEach-Object { ($_.BaseName -split '\.', 2)[1] } |
                Sort-Object -Unique)
        if (-not $groups) { Write-Host "No socket-*.run-task.json in $DataDir" -ForegroundColor Yellow; exit 0 }
        return ,$groups
    }
    if ($Case -and $Case.Count) {
        # accept both "speech-synthesizer" and "socket-speech-synthesizer"
        return ,@($Case | ForEach-Object { $_ -replace '^socket-', '' })
    }
    return ,(Select-SocketCasesInteractive)
}

function Send-SocketText {
    param($Ws, [string]$Json)
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($Json)
    $null = $Ws.SendAsync(
        [ArraySegment[byte]]::new($bytes),
        [System.Net.WebSockets.WebSocketMessageType]::Text,
        $true,
        [System.Threading.CancellationToken]::None).GetAwaiter().GetResult()
}

function Send-SocketBinary {
    param($Ws, [byte[]]$Data, [int]$ChunkSize = 3200)
    for ($offset = 0; $offset -lt $Data.Length; $offset += $ChunkSize) {
        $count = [Math]::Min($ChunkSize, $Data.Length - $offset)
        $segment = [ArraySegment[byte]]::new($Data, $offset, $count)
        $null = $Ws.SendAsync(
            $segment,
            [System.Net.WebSockets.WebSocketMessageType]::Binary,
            $true,
            [System.Threading.CancellationToken]::None).GetAwaiter().GetResult()
    }
}

function Receive-SocketMessage {
    # Returns @{ Type = 'text'|'binary'|'close'; Text = <string> }; binary frames
    # are accumulated into $BinarySink by the caller-supplied stream.
    param($Ws, [System.IO.MemoryStream]$BinarySink, [System.Threading.CancellationToken]$Token)

    $buffer = [byte[]]::new(64 * 1024)
    $segment = [ArraySegment[byte]]::new($buffer)
    $text = [System.IO.MemoryStream]::new()
    try {
        while ($true) {
            $result = $Ws.ReceiveAsync($segment, $Token).GetAwaiter().GetResult()
            if ($result.MessageType -eq [System.Net.WebSockets.WebSocketMessageType]::Close) {
                return [pscustomobject]@{ Type = 'close'; Text = $null }
            }
            if ($result.MessageType -eq [System.Net.WebSockets.WebSocketMessageType]::Binary) {
                $BinarySink.Write($buffer, 0, $result.Count)
                if ($result.EndOfMessage) { return [pscustomobject]@{ Type = 'binary'; Text = $null } }
                continue
            }
            $text.Write($buffer, 0, $result.Count)
            if ($result.EndOfMessage) {
                return [pscustomobject]@{
                    Type = 'text'
                    Text = [System.Text.Encoding]::UTF8.GetString($text.ToArray())
                }
            }
        }
    }
    finally {
        $text.Dispose()
    }
}

function Write-GroupEvents {
    # Writes the captured server events of a socket group as socket-<group>.<event>.json
    # (repeated events numbered -2, ...), replacing the previous captures of those events.
    param([string]$Group, $Events)

    $utf8NoBom = [System.Text.UTF8Encoding]::new($false)
    foreach ($name in $Events.Keys) {
        # drop stale numbered variants from previous runs (e.g. result-generated-2.json),
        # but keep differently named fixtures such as task-finished-cancel.json
        Get-ChildItem -LiteralPath $DataDir -Filter "socket-$Group.$name*.json" -File |
            Where-Object { $_.BaseName -match "\.$([regex]::Escape($name))(-\d+)?$" } |
            Remove-Item
        for ($i = 0; $i -lt $Events[$name].Count; $i++) {
            $suffix = if ($i -eq 0) { '' } else { "-$($i + 1)" }
            $path = Join-Path $DataDir "socket-$Group.$name$suffix.json"
            [System.IO.File]::WriteAllText($path, $Events[$name][$i], $utf8NoBom)
            Write-Host "    wrote socket-$Group.$name$suffix.json" -ForegroundColor DarkGray
        }
    }

    # event files this run did not produce are left untouched: they are either kept on purpose
    # (e.g. a captured error) or a leftover of an earlier run, say so instead of staying silent
    $clientMessages = @(
        'run-task', 'continue-task', 'finish-task',
        'session-update', 'append-text', 'commit-text', 'commit-audio', 'finish-session', 'response-create',
        'item-create', 'mcp-approval-response', 'response-create-after-mcp', 'response-cancel'
    )
    $prefix = "socket-$Group."
    Get-ChildItem -LiteralPath $DataDir -Filter "$prefix*.json" -File |
        ForEach-Object { $_.BaseName.Substring($prefix.Length) } |
        Where-Object { $clientMessages -notcontains $_ } |
        ForEach-Object { $_ -replace '-\d+$', '' } |
        Select-Object -Unique |
        Where-Object { -not $Events.Contains($_) } |
        ForEach-Object {
            Write-Host "    kept socket-$Group.$_.json: not produced by this run" -ForegroundColor Yellow
        }
}

function Trim-AudioDeltas {
    # OpenAI compatible chat completion streams that answer with audio (e.g.
    # qwen3-livetranslate-flash) send the audio as a run of consecutive chunks with large base64
    # payloads. Keep every chunk outside such a run, and only its first and last chunk with the
    # payload replaced by a placeholder; every kept event stays on the wire line it arrived on.
    param([string]$Body)

    $kept = [System.Collections.Generic.List[string]]::new()
    $pendingLast = $null
    $inRun = $false
    $lines = $Body -split '\r?\n'
    for ($i = 0; $i -le $lines.Count; $i++) {
        $isAudio = $false
        if ($i -lt $lines.Count) {
            $line = $lines[$i]
            if ($line.StartsWith('data: {')) {
                try {
                    $chunk = $line.Substring('data: '.Length) | ConvertFrom-Json
                    $isAudio = $null -ne $chunk.choices[0].delta.audio
                }
                catch {
                    $isAudio = $false
                }
            }
        }

        if ($isAudio) {
            if ($inRun) {
                # only the last chunk of the run is kept besides the first one
                $pendingLast = $line
            }
            else {
                $kept.Add(($line -replace '"data":"[A-Za-z0-9+/=]{100,}"', '"data":"<base64>"'))
                $kept.Add('')
                $inRun = $true
            }
            continue
        }

        # the blank line between two events does not end a run of audio events
        if ($inRun -and $line -eq '') {
            continue
        }

        if ($inRun) {
            if ($pendingLast) {
                $kept.Add(($pendingLast -replace '"data":"[A-Za-z0-9+/=]{100,}"', '"data":"<base64>"'))
                $kept.Add('')
            }
            $pendingLast = $null
            $inRun = $false
        }
        if ($i -lt $lines.Count) {
            $kept.Add($line)
        }
    }
    return ($kept -join "`n")
}

function Invoke-RefreshSocketGroup {
    param([string]$Group)

    $runPath = Join-Path $DataDir "socket-$Group.run-task.json"
    if (-not (Test-Path $runPath)) {
        Write-Host "  [skip] $Group : no socket-$Group.run-task.json" -ForegroundColor Yellow
        return
    }
    $continuePath = Join-Path $DataDir "socket-$Group.continue-task.json"
    $finishPath = Join-Path $DataDir "socket-$Group.finish-task.json"
    # ASR groups: input audio to send as binary frames after task-started
    $inputAudio = Get-ChildItem -LiteralPath $DataDir -Filter "socket-$Group.input.*" -File |
        Select-Object -First 1

    $runJson = Get-Content -LiteralPath $runPath -Raw
    $runObj = $runJson | ConvertFrom-Json
    $model = $runObj.payload.model
    $format = $runObj.payload.parameters.format
    if (-not $format) { $format = 'pcm' }

    $ws = [System.Net.WebSockets.ClientWebSocket]::new()
    # mirror DashScopeClientWebSocket: lowercase "bearer"
    $ws.Options.SetRequestHeader('Authorization', "bearer $ApiKey")
    $cts = [System.Threading.CancellationTokenSource]::new([TimeSpan]::FromMinutes(3))
    $audio = [System.IO.MemoryStream]::new()
    $failed = $false

    try {
        $null = $ws.ConnectAsync([Uri]$SocketUri, $cts.Token).GetAwaiter().GetResult()
        Write-Host "  connected, model=$model format=$format" -ForegroundColor DarkGray
        Send-SocketText -Ws $ws -Json $runJson

        # event name -> raw json occurrences, in arrival order
        $events = [ordered]@{}
        $followUpsSent = $false
        while ($ws.State -eq [System.Net.WebSockets.WebSocketState]::Open) {
            $msg = Receive-SocketMessage -Ws $ws -BinarySink $audio -Token $cts.Token
            if ($msg.Type -eq 'close') { break }
            if ($msg.Type -eq 'binary') { continue }

            $event = ($msg.Text | ConvertFrom-Json).header.event
            Write-Host "    <- $event"
            if (-not $events.Contains($event)) {
                $events[$event] = [System.Collections.Generic.List[string]]::new()
            }
            $events[$event].Add($msg.Text)

            if ($event -eq 'task-started' -and -not $followUpsSent) {
                $followUpsSent = $true
                if (Test-Path $continuePath) {
                    Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $continuePath -Raw)
                }
                if ($inputAudio) {
                    Write-Host "    -> $($inputAudio.Name) ($($inputAudio.Length) bytes)" -ForegroundColor DarkGray
                    Send-SocketBinary -Ws $ws -Data ([System.IO.File]::ReadAllBytes($inputAudio.FullName))
                }
                if (Test-Path $finishPath) {
                    Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $finishPath -Raw)
                }
            }
            if ($event -in 'task-finished', 'task-failed') {
                if ($event -eq 'task-failed') { $failed = $true }
                break
            }
        }
        if ($ws.State -eq [System.Net.WebSockets.WebSocketState]::Open) {
            $null = $ws.CloseAsync(
                [System.Net.WebSockets.WebSocketCloseStatus]::NormalClosure,
                'Closing',
                [System.Threading.CancellationToken]::None).GetAwaiter().GetResult()
        }

        Write-GroupEvents -Group $Group -Events $events
        if ($audio.Length -gt 0) {
            $audioPath = Join-Path $DataDir "socket-$Group.audio.$format"
            [System.IO.File]::WriteAllBytes($audioPath, $audio.ToArray())
            Write-Host "    wrote socket-$Group.audio.$format ($($audio.Length) bytes)" -ForegroundColor DarkGray
        }

        $mark = if ($failed) { '✗' } else { '✓' }
        $color = if ($failed) { 'Red' } else { 'Green' }
        Write-Host ("  {0} {1}" -f $mark, $Group) -ForegroundColor $color
        if ($failed) {
            $err = $events['task-failed'] | ConvertFrom-Json
            Write-Host ("    {0}: {1}" -f $err.header.error_code, $err.header.error_message) -ForegroundColor Red
        }
    }
    finally {
        $ws.Dispose()
        $cts.Dispose()
        $audio.Dispose()
    }
}

function Invoke-RefreshCase {
    param([string]$Name)

    $bodyPath = Join-Path $DataDir "$Name.request.body.json"
    $reqHeaderPath = Join-Path $DataDir "$Name.request.header.txt"
    $respHeaderPath = Join-Path $DataDir "$Name.response.header.txt"
    $respBodyPath = Join-Path $DataDir "$Name.response.body.txt"

    if (-not (Test-Path $reqHeaderPath)) {
        Write-Host "  [skip] $Name : no .request.header.txt" -ForegroundColor Yellow
        return
    }

    $parsed = Parse-RequestHeader -Path $reqHeaderPath
    $method = ($parsed.RequestLine -split ' ', 2)[0]
    $url = Get-AbsoluteUrl -RequestLine $parsed.RequestLine -Headers $parsed.Headers

    $hasBody = Test-Path $bodyPath
    if ($hasBody -eq $false -and $method -ne 'GET') {
        Write-Host "  [skip] $Name : no .request.body.json (only GET is supported without body)" -ForegroundColor Yellow
        return
    }

    # build outgoing request
    $req = [System.Net.Http.HttpRequestMessage]::new([System.Net.Http.HttpMethod]::new($method), $url)

    $contentType = $null
    $forwardHeaders = [ordered]@{}
    foreach ($k in $parsed.Headers.Keys) {
        if ($skipHeaders -contains $k.ToLower()) { continue }
        if ($k -ieq 'content-type') { $contentType = $parsed.Headers[$k]; continue }
        $forwardHeaders[$k] = $parsed.Headers[$k]
    }

    if ($hasBody) {
        if (-not $contentType) { $contentType = 'application/json' }
        $body = Get-Content -LiteralPath $bodyPath -Raw
        $req.Content = [System.Net.Http.StringContent]::new($body, [System.Text.Encoding]::UTF8, $contentType)
    }

    # Authorization always from env/key; -RedactAuth only affects what is written to request.header.txt
    $req.Headers.TryAddWithoutValidation('Authorization', "Bearer $ApiKey") | Out-Null
    foreach ($k in $forwardHeaders.Keys) {
        $req.Headers.TryAddWithoutValidation($k, $forwardHeaders[$k]) | Out-Null
    }

    $resp = $httpClient.SendAsync($req, [System.Net.Http.HttpCompletionOption]::ResponseHeadersRead).GetAwaiter().GetResult()
    $respBody = $resp.Content.ReadAsStringAsync().GetAwaiter().GetResult()
    if ($TrimAudioDeltas) {
        $respBody = Trim-AudioDeltas -Body $respBody
    }

    # --- write request.header.txt (normalized) ---
    $authValue = if ($RedactAuth) { 'Bearer <redacted>' } else { "Bearer $ApiKey" }
    $reqLines = [System.Collections.Generic.List[string]]::new()
    $reqLines.Add("$method $(Get-RedactedUrl $url)")
    $reqLines.Add("Authorization: $authValue")
    if ($contentType) { $reqLines.Add("Content-Type: $contentType") }
    foreach ($k in $forwardHeaders.Keys) { $reqLines.Add("${k}: " + $forwardHeaders[$k]) }
    [System.IO.File]::WriteAllLines($reqHeaderPath, $reqLines, (New-Object System.Text.UTF8Encoding $false))

    # --- write response.header.txt ---
    $respLines = [System.Collections.Generic.List[string]]::new()
    $respLines.Add("HTTP/1.1 $([int]$resp.StatusCode) $($resp.ReasonPhrase)")
    foreach ($h in $resp.Headers) { foreach ($v in $h.Value) { $respLines.Add("$($h.Key): $v") } }
    foreach ($h in $resp.Content.Headers) { foreach ($v in $h.Value) { $respLines.Add("$($h.Key): $v") } }
    [System.IO.File]::WriteAllLines($respHeaderPath, $respLines, (New-Object System.Text.UTF8Encoding $false))

    # --- write response.body.txt ---
    [System.IO.File]::WriteAllText($respBodyPath, $respBody, (New-Object System.Text.UTF8Encoding $false))

    $mark = if ($resp.IsSuccessStatusCode) { '✓' } else { '✗' }
    $color = if ($resp.IsSuccessStatusCode) { 'Green' } else { 'Red' }
    Write-Host ("  {0} {1} -> {2} {3}" -f $mark, $Name, [int]$resp.StatusCode, $resp.ReasonPhrase) -ForegroundColor $color
}

function Invoke-RefreshRealtimeTtsGroup {
    param([string]$Group)

    $sessionUpdatePath = Join-Path $DataDir "socket-$Group.session-update.json"
    if (-not (Test-Path $sessionUpdatePath)) {
        Write-Host "  [skip] $Group : no socket-$Group.session-update.json" -ForegroundColor Yellow
        return
    }
    if (-not $Model) {
        throw '-Model is required with -RealtimeTts (the ?model= query parameter of the realtime endpoint).'
    }
    $appendPath = Join-Path $DataDir "socket-$Group.append-text.json"
    $commitPath = Join-Path $DataDir "socket-$Group.commit-text.json"
    $finishPath = Join-Path $DataDir "socket-$Group.finish-session.json"

    $format = ((Get-Content -LiteralPath $sessionUpdatePath -Raw | ConvertFrom-Json).session).response_format
    if (-not $format) { $format = 'pcm' }
    $uri = "$($SocketUri.TrimEnd('/'))?model=$([uri]::EscapeDataString($Model))"

    $ws = [System.Net.WebSockets.ClientWebSocket]::new()
    # mirror TtsRealtimeSocketSession: lowercase "bearer"
    $ws.Options.SetRequestHeader('Authorization', "bearer $ApiKey")
    $cts = [System.Threading.CancellationTokenSource]::new([TimeSpan]::FromMinutes(3))
    $audio = [System.IO.MemoryStream]::new()
    $failed = $false

    try {
        $null = $ws.ConnectAsync([Uri]$uri, $cts.Token).GetAwaiter().GetResult()
        Write-Host "  connected, model=$Model format=$format" -ForegroundColor DarkGray

        # event type -> raw json occurrences, in arrival order
        $events = [ordered]@{}
        $finished = $false
        while ($ws.State -eq [System.Net.WebSockets.WebSocketState]::Open -and -not $finished) {
            $msg = Receive-SocketMessage -Ws $ws -BinarySink $audio -Token $cts.Token
            if ($msg.Type -eq 'close') { break }
            if ($msg.Type -eq 'binary') { continue }

            $obj = $msg.Text | ConvertFrom-Json
            $type = $obj.type
            Write-Host "    <- $type"
            if (-not $events.Contains($type)) {
                $events[$type] = [System.Collections.Generic.List[string]]::new()
            }
            $events[$type].Add($msg.Text)

            if ($type -eq 'response.audio.delta' -and $obj.delta) {
                $bytes = [Convert]::FromBase64String($obj.delta)
                $audio.Write($bytes, 0, $bytes.Length)
            }

            switch ($type) {
                'session.created' {
                    Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $sessionUpdatePath -Raw)
                }
                'session.updated' {
                    if (Test-Path $appendPath) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $appendPath -Raw)
                    }
                    if (Test-Path $commitPath) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $commitPath -Raw)
                    }
                }
                'response.done' {
                    if (Test-Path $finishPath) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $finishPath -Raw)
                    }
                }
                'session.finished' {
                    $finished = $true
                }
                'error' {
                    $failed = $true
                    $err = $obj.error
                    Write-Host ("    {0}: {1}" -f $err.code, $err.message) -ForegroundColor Red
                }
            }

            if ($failed) { break }
        }
        if ($ws.State -eq [System.Net.WebSockets.WebSocketState].Open) {
            $null = $ws.CloseAsync(
                [System.Net.WebSockets.WebSocketCloseStatus]::NormalClosure,
                'Closing',
                [System.Threading.CancellationToken]::None).GetAwaiter().GetResult()
        }

        Write-GroupEvents -Group $Group -Events $events
        if ($audio.Length -gt 0) {
            $audioPath = Join-Path $DataDir "socket-$Group.audio.$format"
            [System.IO.File]::WriteAllBytes($audioPath, $audio.ToArray())
            Write-Host "    wrote socket-$Group.audio.$format ($($audio.Length) bytes)" -ForegroundColor DarkGray
        }

        $mark = if ($failed) { '✗' } else { '✓' }
        $color = if ($failed) { 'Red' } else { 'Green' }
        Write-Host ("  {0} {1}" -f $mark, $Group) -ForegroundColor $color
    }
    finally {
        $ws.Dispose()
        $cts.Dispose()
        $audio.Dispose()
    }
}

function Invoke-RefreshRealtimeAsrGroup {
    param(
        [string]$Group,
        [switch]$SupportsResponse,
        [switch]$TrimDeltaStreams
    )

    $sessionUpdatePath = Join-Path $DataDir "socket-$Group.session-update.json"
    if (-not (Test-Path $sessionUpdatePath)) {
        Write-Host "  [skip] $Group : no socket-$Group.session-update.json" -ForegroundColor Yellow
        return
    }
    if (-not $Model) {
        throw '-Model is required with -RealtimeAsr (the ?model= query parameter of the realtime endpoint).'
    }
    $commitPath = Join-Path $DataDir "socket-$Group.commit-audio.json"
    $finishPath = Join-Path $DataDir "socket-$Group.finish-session.json"
    $responseCreatePath = Join-Path $DataDir "socket-$Group.response-create.json"

    $sessionObj = (Get-Content -LiteralPath $sessionUpdatePath -Raw | ConvertFrom-Json).session
    $format = $sessionObj.input_audio_format
    if (-not $format) { $format = 'pcm' }
    $inputPath = Join-Path $DataDir "socket-$Group.input.$format"
    if (-not (Test-Path $inputPath)) {
        throw "No socket-$Group.input.$format under $DataDir. The ASR realtime groups send it as base64 chunks."
    }
    $uri = "$($SocketUri.TrimEnd('/'))?model=$([uri]::EscapeDataString($Model))"

    $ws = [System.Net.WebSockets.ClientWebSocket]::new()
    # mirror AsrRealtimeSocketSession: lowercase "bearer"
    $ws.Options.SetRequestHeader('Authorization', "bearer $ApiKey")
    $cts = [System.Threading.CancellationTokenSource]::new([TimeSpan]::FromMinutes(3))
    $failed = $false

    try {
        $null = $ws.ConnectAsync([Uri]$uri, $cts.Token).GetAwaiter().GetResult()
        Write-Host "  connected, model=$Model format=$format" -ForegroundColor DarkGray

        $events = [ordered]@{}
        $finished = $false
        $audioSent = $false
        while ($ws.State -eq [System.Net.WebSockets.WebSocketState]::Open -and -not $finished) {
            $msg = Receive-SocketMessage -Ws $ws -Token $cts.Token
            if ($msg.Type -eq 'close') { break }
            if ($msg.Type -eq 'binary') { continue }

            $obj = $msg.Text | ConvertFrom-Json
            $type = $obj.type
            Write-Host "    <- $type"
            if (-not $events.Contains($type)) {
                $events[$type] = [System.Collections.Generic.List[string]]::new()
            }
            $events[$type].Add($msg.Text)

            switch ($type) {
                'session.created' {
                    Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $sessionUpdatePath -Raw)
                }
                'session.updated' {
                    # append the input audio as base64 chunks of 3200 bytes (no server ack)
                    $bytes = [System.IO.File]::ReadAllBytes($inputPath)
                    for ($offset = 0; $offset -lt $bytes.Length; $offset += 3200) {
                        $size = [Math]::Min(3200, $bytes.Length - $offset)
                        $chunk = [byte[]]::new($size)
                        [Array]::Copy($bytes, $offset, $chunk, 0, $size)
                        $append = @{
                            event_id = [Guid]::NewGuid().ToString()
                            type = 'input_audio_buffer.append'
                            audio = [Convert]::ToBase64String($chunk)
                        } | ConvertTo-Json -Compress
                        Send-SocketText -Ws $ws -Json $append
                    }
                    Write-Host "    -> appended $($bytes.Length) bytes of $format audio" -ForegroundColor DarkGray
                    $audioSent = $true
                    if (Test-Path $commitPath) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $commitPath -Raw)
                    }
                    elseif ($SupportsResponse -and (Test-Path $finishPath)) {
                        # live translation VAD: finish right after the audio, the server
                        # completes the remaining translation before session.finished
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $finishPath -Raw)
                    }
                }
                'conversation.item.input_audio_transcription.completed' {
                    # give late events (e.g. the completed one after commit) a moment, then finish
                    if ($audioSent -and (Test-Path $finishPath) -and -not $events.Contains('session.finished')) {
                        Start-Sleep -Milliseconds 500
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $finishPath -Raw)
                    }
                }
                'input_audio_buffer.committed' {
                    # push-to-talk mode: trigger the inference manually once the audio is committed
                    if ($SupportsResponse -and (Test-Path $responseCreatePath)) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $responseCreatePath -Raw)
                    }
                }
                'response.done' {
                    # audio chat / live translation responses; finish the session when asked to,
                    # otherwise the session ends here (audio chat has no session.finish)
                    if ($SupportsResponse -and (Test-Path $finishPath) -and -not $events.Contains('session.finished')) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $finishPath -Raw)
                    }
                    elseif ($SupportsResponse) {
                        $finished = $true
                    }
                }
                'session.finished' {
                    $finished = $true
                }
                'error' {
                    $failed = $true
                    $err = $obj.error
                    Write-Host ("    {0}: {1}" -f $err.code, $err.message) -ForegroundColor Red
                }
            }

            if ($failed) { break }
        }
        if ($ws.State -eq [System.Net.WebSockets.WebSocketState].Open) {
            $null = $ws.CloseAsync(
                [System.Net.WebSockets.WebSocketCloseStatus].NormalClosure,
                'Closing',
                [System.Threading.CancellationToken]::None).GetAwaiter().GetResult()
        }

        # audio chat / live translation sessions fire hundreds of delta events; keep the
        # first and last occurrence of each delta stream to cover both ends of the flow
        $deltaStreamTypes = @(
            'response.audio.delta'
            'response.audio_transcript.delta'
            'response.text.delta'
            'response.function_call_arguments.delta'
            'conversation.item.input_audio_transcription.delta'
            'conversation.item.ambient_audio_transcription.delta'
            'conversation.item.input_audio_transcription.text'
            'response.audio_transcript.text'
            'response.text.text'
        )
        if ($TrimDeltaStreams) {
            foreach ($name in @($events.Keys)) {
                if ($deltaStreamTypes -contains $name -and $events[$name].Count -gt 2) {
                    Write-Host "    trimmed $name from $($events[$name].Count) to the first and last occurrences" -ForegroundColor DarkGray
                    $events[$name] = [System.Collections.Generic.List[string]]@(
                        $events[$name][0]
                        $events[$name][$events[$name].Count - 1]
                    )
                }
            }
        }

        Write-GroupEvents -Group $Group -Events $events

        $mark = if ($failed) { '✗' } else { '✓' }
        $color = if ($failed) { 'Red' } else { 'Green' }
        Write-Host ("  {0} {1}" -f $mark, $Group) -ForegroundColor $color
    }
    finally {
        $ws.Dispose()
        $cts.Dispose()
    }
}

function Invoke-RefreshRealtimeOmniGroup {
    param([string]$Group)

    $sessionUpdatePath = Join-Path $DataDir "socket-$Group.session-update.json"
    if (-not (Test-Path $sessionUpdatePath)) {
        Write-Host "  [skip] $Group : no socket-$Group.session-update.json" -ForegroundColor Yellow
        return
    }
    if (-not $Model) {
        throw '-Model is required with -RealtimeOmni (the ?model= query parameter of the realtime endpoint).'
    }

    $commitPath = Join-Path $DataDir "socket-$Group.commit-audio.json"
    $responseCreatePath = Join-Path $DataDir "socket-$Group.response-create.json"
    $itemCreatePath = Join-Path $DataDir "socket-$Group.item-create.json"
    $mcpApprovalPath = Join-Path $DataDir "socket-$Group.mcp-approval-response.json"
    $mcpFollowUpPath = Join-Path $DataDir "socket-$Group.response-create-after-mcp.json"
    $responseCancelPath = Join-Path $DataDir "socket-$Group.response-cancel.json"
    $inputAudio = Get-ChildItem -LiteralPath $DataDir -Filter "socket-$Group.input.*" -File |
        Select-Object -First 1
    $image = Get-ChildItem -LiteralPath $DataDir -Filter "socket-$Group.image.*" -File |
        Select-Object -First 1

    $sessionObj = (Get-Content -LiteralPath $sessionUpdatePath -Raw | ConvertFrom-Json).session
    $format = $sessionObj.input_audio_format
    $outputFormat = $sessionObj.output_audio_format
    # 16 kHz 16 bit mono pcm is what a session starts with
    $sampleRate = 16000
    $channels = 1
    if ($sessionObj.audio.input.format) {
        $inputFormat = $sessionObj.audio.input.format
        if (-not $format) { $format = $inputFormat.type }
        if ($inputFormat.sample_rate) { $sampleRate = $inputFormat.sample_rate }
        if ($inputFormat.channels) { $channels = $inputFormat.channels }
    }
    if ($sessionObj.audio.output.format.type) { $outputFormat = $sessionObj.audio.output.format.type }
    if (-not $format) { $format = 'pcm' }
    if (-not $outputFormat) { $outputFormat = 'pcm' }
    $uri = "$($SocketUri.TrimEnd('/'))?model=$([uri]::EscapeDataString($Model))"

    $utf8NoBom = [System.Text.UTF8Encoding]::new($false)
    $ws = [System.Net.WebSockets.ClientWebSocket]::new()
    # mirror OmniRealtimeSocketSession: lowercase "bearer"
    $ws.Options.SetRequestHeader('Authorization', "bearer $ApiKey")
    $cts = [System.Threading.CancellationTokenSource]::new([TimeSpan]::FromMinutes(3))
    $failed = $false

    # the audio goes out as base64 chunks inside input_audio_buffer.append events (no server ack)
    $appendAudio = {
        param([byte[]]$Bytes)
        $count = 0
        for ($offset = 0; $offset -lt $Bytes.Length; $offset += 3200) {
            $size = [Math]::Min(3200, $Bytes.Length - $offset)
            $chunk = [byte[]]::new($size)
            [Array]::Copy($Bytes, $offset, $chunk, 0, $size)
            $append = @{
                event_id = [Guid]::NewGuid().ToString()
                type = 'input_audio_buffer.append'
                audio = [Convert]::ToBase64String($chunk)
            } | ConvertTo-Json -Compress
            Send-SocketText -Ws $ws -Json $append
            $count++
        }
        return $count
    }

    try {
        $null = $ws.ConnectAsync([Uri]$uri, $cts.Token).GetAwaiter().GetResult()
        Write-Host "  connected, model=$Model" -ForegroundColor DarkGray

        $events = [ordered]@{}
        $finished = $false
        $responseCreateSent = $false
        $cancelSent = $false
        $mcpFollowUpSent = $false
        # function call outputs were written back, the response.create follows their confirmation
        $outputFollowUpPending = $false
        $messageFollowUpPending = $false

        while ($ws.State -eq [System.Net.WebSockets.WebSocketState]::Open -and -not $finished) {
            $msg = Receive-SocketMessage -Ws $ws -Token $cts.Token
            if ($msg.Type -eq 'close') { break }
            if ($msg.Type -eq 'binary') { continue }

            $obj = $msg.Text | ConvertFrom-Json
            $type = $obj.type
            Write-Host "    <- $type"
            if (-not $events.Contains($type)) {
                $events[$type] = [System.Collections.Generic.List[string]]::new()
            }
            $events[$type].Add($msg.Text)

            switch ($type) {
                'session.created' {
                    Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $sessionUpdatePath -Raw)
                }
                'session.updated' {
                    if ($inputAudio) {
                        $bytes = [System.IO.File]::ReadAllBytes($inputAudio.FullName)
                        $count = & $appendAudio $bytes
                        Write-Host "    -> $count x input_audio_buffer.append ($($bytes.Length) bytes of $format audio)" -ForegroundColor DarkGray
                        if (-not (Test-Path $commitPath)) {
                            # the VAD mode only detects the end of the speech after some silence, which a
                            # live microphone keeps delivering: two seconds of it end the turn
                            $null = & $appendAudio ([byte[]]::new(2 * $sampleRate * 2 * $channels))
                            Write-Host '    -> 2 s of silence' -ForegroundColor DarkGray
                        }
                    }
                    if ($image) {
                        $appendImage = @{
                            event_id = [Guid]::NewGuid().ToString()
                            type = 'input_image_buffer.append'
                            image = [Convert]::ToBase64String([System.IO.File]::ReadAllBytes($image.FullName))
                        } | ConvertTo-Json -Compress
                        Send-SocketText -Ws $ws -Json $appendImage
                        Write-Host "    -> input_image_buffer.append ($($image.Name))" -ForegroundColor DarkGray
                    }
                    if (Test-Path $commitPath) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $commitPath -Raw)
                    }
                    if (Test-Path $itemCreatePath) {
                        $item = (Get-Content -LiteralPath $itemCreatePath -Raw | ConvertFrom-Json).item
                        if ($item.type -eq 'message') {
                            # a message item (text input) goes in once the session is configured
                            Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $itemCreatePath -Raw)
                            Write-Host "    -> conversation.item.create (message / $($item.role))" -ForegroundColor DarkGray
                            $messageFollowUpPending = $true
                        }
                    }
                }
                'input_audio_buffer.committed' {
                    # manual mode: trigger the response once the audio is committed
                    if ((Test-Path $responseCreatePath) -and -not $responseCreateSent) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $responseCreatePath -Raw)
                        $responseCreateSent = $true
                    }
                }
                'conversation.item.created' {
                    if ($obj.item.type -eq 'mcp_approval_request' -and (Test-Path $mcpApprovalPath)) {
                        # the approval request id is only known live: substitute it and keep the
                        # fixture in step with the capture
                        $template = Get-Content -LiteralPath $mcpApprovalPath -Raw
                        $approval = $template.Replace('{approval_request_id}', [string]$obj.item.id)
                        Send-SocketText -Ws $ws -Json $approval
                        if ($approval -ne $template) {
                            [System.IO.File]::WriteAllText($mcpApprovalPath, $approval, $utf8NoBom)
                        }
                        Write-Host '    -> mcp-approval-response' -ForegroundColor DarkGray
                    }
                    elseif ($outputFollowUpPending -and $obj.item.type -eq 'function_call_output') {
                        # the function call output is in the conversation, let the model use it
                        $outputFollowUpPending = $false
                        if (Test-Path $responseCreatePath) {
                            Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $responseCreatePath -Raw)
                            Write-Host '    -> response-create (after the function call output)' -ForegroundColor DarkGray
                        }
                    }
                    elseif ($messageFollowUpPending -and $obj.item.type -eq 'message' -and $obj.item.role -eq 'user') {
                        # the message is in the conversation, ask for the response to it
                        $messageFollowUpPending = $false
                        if (Test-Path $responseCreatePath) {
                            Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $responseCreatePath -Raw)
                            Write-Host '    -> response-create (after the message item)' -ForegroundColor DarkGray
                        }
                    }
                }
                'response.content_part.added' {
                    if ((Test-Path $responseCancelPath) -and -not $cancelSent) {
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $responseCancelPath -Raw)
                        $cancelSent = $true
                        Write-Host '    -> response-cancel' -ForegroundColor DarkGray
                    }
                }
                'response.done' {
                    $output = @($obj.response.output | Where-Object { $null -ne $_ })
                    $functionCalls = @($output | Where-Object { $_.type -eq 'function_call' })
                    if ($functionCalls.Count -gt 0 -and (Test-Path $itemCreatePath)) {
                        # one function_call_output per function call, the call id is only known live
                        $template = Get-Content -LiteralPath $itemCreatePath -Raw
                        $itemCreate = $template
                        foreach ($call in $functionCalls) {
                            $itemCreate = $template.Replace('{call_id}', [string]$call.call_id)
                            Send-SocketText -Ws $ws -Json $itemCreate
                        }
                        if ($itemCreate -ne $template) {
                            [System.IO.File]::WriteAllText($itemCreatePath, $itemCreate, $utf8NoBom)
                        }
                        Write-Host "    -> $($functionCalls.Count) x conversation.item.create (function_call_output)" -ForegroundColor DarkGray
                        $outputFollowUpPending = $true
                    }
                    elseif (@($output | Where-Object { $_.type -eq 'mcp_call' }).Count -gt 0 -and
                            (Test-Path $mcpFollowUpPath) -and -not $mcpFollowUpSent) {
                        # the server executed the MCP tools, ask for the answer based on the results
                        Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $mcpFollowUpPath -Raw)
                        $mcpFollowUpSent = $true
                        Write-Host '    -> response-create (after the MCP calls)' -ForegroundColor DarkGray
                    }
                    else {
                        # nothing to write back or to follow up: the turn is over
                        $finished = $true
                    }
                }
                'error' {
                    $failed = $true
                    $err = $obj.error
                    Write-Host ("    {0}: {1}" -f $err.code, $err.message) -ForegroundColor Red
                }
            }

            if ($failed) { break }
        }
        if ($ws.State -eq [System.Net.WebSockets.WebSocketState]::Open) {
            $null = $ws.CloseAsync(
                [System.Net.WebSockets.WebSocketCloseStatus]::NormalClosure,
                'Closing',
                [System.Threading.CancellationToken]::None).GetAwaiter().GetResult()
        }

        # the responses fire hundreds of delta events; keep the first and last occurrence of
        # each delta stream to cover both ends of the flow
        $deltaStreamTypes = @(
            'response.audio.delta'
            'response.audio_transcript.delta'
            'response.text.delta'
            'response.function_call_arguments.delta'
            'response.mcp_call_arguments.delta'
            'conversation.item.input_audio_transcription.delta'
        )
        foreach ($name in @($events.Keys)) {
            if ($deltaStreamTypes -contains $name -and $events[$name].Count -gt 2) {
                Write-Host "    trimmed $name from $($events[$name].Count) to the first and last occurrences" -ForegroundColor DarkGray
                $events[$name] = [System.Collections.Generic.List[string]]@(
                    $events[$name][0]
                    $events[$name][$events[$name].Count - 1]
                )
            }
        }

        Write-GroupEvents -Group $Group -Events $events

        $mark = if ($failed) { '✗' } else { '✓' }
        $color = if ($failed) { 'Red' } else { 'Green' }
        Write-Host ("  {0} {1}" -f $mark, $Group) -ForegroundColor $color
    }
    finally {
        $ws.Dispose()
        $cts.Dispose()
    }
}

# ---------- HttpClient with gzip/deflate auto-decompression ----------
Add-Type -AssemblyName System.Net.Http -ErrorAction SilentlyContinue
$handler = [System.Net.Http.HttpClientHandler]::new()
$handler.AutomaticDecompression = [System.Net.DecompressionMethods]::GZip -bor `
    [System.Net.DecompressionMethods]::Deflate -bor [System.Net.DecompressionMethods]::Brotli
$httpClient = [System.Net.Http.HttpClient]::new($handler)
$httpClient.Timeout = [TimeSpan]::FromMinutes(5)

try {
    if ($RealtimeTts) {
        if (-not $Case -or -not $Case.Count) {
            throw 'Pass explicit group names with -Case when using -RealtimeTts (e.g. -Case qwen-tts-realtime).'
        }
        $SocketUri = if ($SocketUri -eq 'wss://dashscope.aliyuncs.com/api-ws/v1/inference/') {
            'wss://dashscope.aliyuncs.com/api-ws/v1/realtime'
        }
        else { $SocketUri }
        Write-Host "Replaying $($Case.Count) TTS realtime group(s) against live DashScope API..." -ForegroundColor Cyan
        foreach ($g in $Case) { Invoke-RefreshRealtimeTtsGroup -Group $g }
    }
    elseif ($RealtimeAsr) {
        if (-not $Case -or $All) {
            throw 'Pass explicit group names with -Case when using -RealtimeAsr (e.g. -Case qwen-asr-realtime).'
        }
        $SocketUri = if ($SocketUri -eq 'wss://dashscope.aliyuncs.com/api-ws/v1/inference/') {
            'wss://dashscope.aliyuncs.com/api-ws/v1/realtime'
        }
        else { $SocketUri }
        Write-Host "Replaying $($Case.Count) ASR realtime group(s) against live DashScope API..." -ForegroundColor Cyan
        foreach ($g in $Case) { Invoke-RefreshRealtimeAsrGroup -Group $g }
    }
    elseif ($RealtimeAudio) {
        if (-not $Case -or -not $Case.Count) {
            throw 'Pass explicit group names with -Case when using -RealtimeAudio (e.g. -Case qwen-audio-realtime).'
        }
        $SocketUri = if ($SocketUri -eq 'wss://dashscope.aliyuncs.com/api-ws/v1/inference/') {
            'wss://dashscope.aliyuncs.com/api-ws/v1/realtime'
        }
        else { $SocketUri }
        Write-Host "Replaying $($Case.Count) audio chat / live translation realtime group(s) against live DashScope API..." -ForegroundColor Cyan
        foreach ($g in $Case) {
            Invoke-RefreshRealtimeAsrGroup -Group $g -SupportsResponse -TrimDeltaStreams
        }
    }
    elseif ($RealtimeOmni) {
        if (-not $Case -or -not $Case.Count) {
            throw 'Pass explicit group names with -Case when using -RealtimeOmni (e.g. -Case qwen-omni-realtime).'
        }
        if (-not $Model) {
            throw '-Model is required with -RealtimeOmni (the ?model= query parameter of the realtime endpoint).'
        }
        if ($SocketUri -eq 'wss://dashscope.aliyuncs.com/api-ws/v1/inference/') {
            # the Bailian documentation serves the omni realtime models on the workspace domain
            $SocketUri = if ($WorkspaceId) {
                "wss://$WorkspaceId.$Region.maas.aliyuncs.com/api-ws/v1/realtime"
            }
            else {
                'wss://dashscope.aliyuncs.com/api-ws/v1/realtime'
            }
        }
        Write-Host "Replaying $($Case.Count) omni realtime group(s) against live DashScope API..." -ForegroundColor Cyan
        foreach ($g in $Case) { Invoke-RefreshRealtimeOmniGroup -Group $g }
    }
    elseif ($Socket) {
        $groups = Resolve-SocketCases
        Write-Host "Replaying $($groups.Count) websocket group(s) against live DashScope API..." -ForegroundColor Cyan
        foreach ($g in $groups) { Invoke-RefreshSocketGroup -Group $g }
    }
    else {
        $cases = Resolve-Cases
        Write-Host "Refreshing $($cases.Count) case(s) against live DashScope API..." -ForegroundColor Cyan
        foreach ($c in $cases) { Invoke-RefreshCase -Name $c }
    }
    Write-Host "Done." -ForegroundColor Green
}
finally {
    $httpClient.Dispose()
    $handler.Dispose()
}
