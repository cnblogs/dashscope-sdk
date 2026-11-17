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

.PARAMETER Case
    One or more case names (without the .request.body.json suffix) to refresh.
    With -Socket, group names without the "socket-" prefix (e.g. speech-synthesizer).
    Omit and leave -All unset to choose interactively from git-modified bodies.

.PARAMETER All
    Refresh every case that has a .request.body.json under -DataDir.
    With -Socket, refresh every socket-* group.

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

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1
    # interactively pick from git-modified *.request.body.json files

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1 -Case single-generation-message-sse -RedactAuth

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1 -Socket -Case speech-synthesizer
#>
[CmdletBinding()]
param(
    [Parameter(Position = 0, ValueFromRemainingArguments)]
    [string[]]$Case,

    [switch]$All,

    [string]$DataDir,

    [switch]$RedactAuth,

    [string]$ApiKey,

    [switch]$Socket,

    [string]$SocketUri = 'wss://dashscope.aliyuncs.com/api-ws/v1/inference/'
)

$ErrorActionPreference = 'Stop'

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
    if ($url -match '^https?://') { return $url }

    # relative path -> combine with Host header
    $hostName = $Headers['host']
    if (-not $hostName) { throw "Relative URL '$url' but no Host header." }
    $scheme = 'https'
    return "${scheme}://${hostName}${url}"
}

function Parse-RequestHeader {
    param([string]$Path)
    $lines = Get-Content -LiteralPath $Path
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

function Invoke-RefreshSocketGroup {
    param([string]$Group)

    $runPath = Join-Path $DataDir "socket-$Group.run-task.json"
    if (-not (Test-Path $runPath)) {
        Write-Host "  [skip] $Group : no socket-$Group.run-task.json" -ForegroundColor Yellow
        return
    }
    $continuePath = Join-Path $DataDir "socket-$Group.continue-task.json"
    $finishPath = Join-Path $DataDir "socket-$Group.finish-task.json"

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
    $utf8NoBom = [System.Text.UTF8Encoding]::new($false)
    $failed = $false

    try {
        $null = $ws.ConnectAsync([Uri]$SocketUri, $cts.Token).GetAwaiter().GetResult()
        Write-Host "  connected, model=$model format=$format" -ForegroundColor DarkGray
        Send-SocketText -Ws $ws -Json $runJson

        # event name -> raw json, last occurrence wins
        $events = [ordered]@{}
        $followUpsSent = $false
        while ($ws.State -eq [System.Net.WebSockets.WebSocketState]::Open) {
            $msg = Receive-SocketMessage -Ws $ws -BinarySink $audio -Token $cts.Token
            if ($msg.Type -eq 'close') { break }
            if ($msg.Type -eq 'binary') { continue }

            $event = ($msg.Text | ConvertFrom-Json).header.event
            Write-Host "    <- $event"
            $events[$event] = $msg.Text

            if ($event -eq 'task-started' -and -not $followUpsSent) {
                $followUpsSent = $true
                if (Test-Path $continuePath) {
                    Send-SocketText -Ws $ws -Json (Get-Content -LiteralPath $continuePath -Raw)
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

        foreach ($name in $events.Keys) {
            $path = Join-Path $DataDir "socket-$Group.$name.json"
            [System.IO.File]::WriteAllText($path, $events[$name], $utf8NoBom)
            Write-Host "    wrote socket-$Group.$name.json" -ForegroundColor DarkGray
        }
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

    # --- write request.header.txt (normalized) ---
    $authValue = if ($RedactAuth) { 'Bearer <redacted>' } else { "Bearer $ApiKey" }
    $reqLines = [System.Collections.Generic.List[string]]::new()
    $reqLines.Add("$method $url")
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

# ---------- HttpClient with gzip/deflate auto-decompression ----------
Add-Type -AssemblyName System.Net.Http -ErrorAction SilentlyContinue
$handler = [System.Net.Http.HttpClientHandler]::new()
$handler.AutomaticDecompression = [System.Net.DecompressionMethods]::GZip -bor `
    [System.Net.DecompressionMethods]::Deflate -bor [System.Net.DecompressionMethods]::Brotli
$httpClient = [System.Net.Http.HttpClient]::new($handler)
$httpClient.Timeout = [TimeSpan]::FromMinutes(5)

try {
    if ($Socket) {
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
