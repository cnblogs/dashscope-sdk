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

    Authorization is taken ONLY from $env:DASHSCOPE_API_KEY (or -ApiKey), never
    from the stale token inside the existing request.header.txt.

.PARAMETER Case
    One or more case names (without the .request.body.json suffix) to refresh.
    Omit and leave -All unset to choose interactively from git-modified bodies.

.PARAMETER All
    Refresh every case that has a .request.body.json under -DataDir.

.PARAMETER DataDir
    RawHttpData directory. Defaults to test/Cnblogs.DashScope.Tests.Shared/RawHttpData
    relative to the repo root (auto-located from this script's path).

.PARAMETER RedactAuth
    Write "Authorization: Bearer <redacted>" into request.header.txt. Tests never
    validate the token, so this is safe and avoids leaking keys.

.PARAMETER ApiKey
    Override $env:DASHSCOPE_API_KEY.

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1
    # interactively pick from git-modified *.request.body.json files

.EXAMPLE
    pwsh ./scripts/Update-Snapshots.ps1 -Case single-generation-message-sse -RedactAuth
#>
[CmdletBinding()]
param(
    [Parameter(Position = 0, ValueFromRemainingArguments)]
    [string[]]$Case,

    [switch]$All,

    [string]$DataDir,

    [switch]$RedactAuth,

    [string]$ApiKey
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
    'postman-token', 'cache-control', 'user-agent', 'request-start-time'
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
    # Use git status to find changed request bodies.
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
        } | Where-Object { $_ -match '\.request\.body\.json$' }
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
        $all = @(Get-ChildItem -LiteralPath $DataDir -Filter '*.request.body.json' |
                ForEach-Object { $_.BaseName -replace '\.request\.body$', '' } |
                Sort-Object -Unique)
        if (-not $all) { Write-Host "No *.request.body.json in $DataDir" -ForegroundColor Yellow; exit 0 }
        return ,$all
    }
    if ($Case -and $Case.Count) { return ,$Case }
    return ,(Select-CasesInteractive)
}

function Invoke-RefreshCase {
    param([string]$Name)

    $bodyPath = Join-Path $DataDir "$Name.request.body.json"
    $reqHeaderPath = Join-Path $DataDir "$Name.request.header.txt"
    $respHeaderPath = Join-Path $DataDir "$Name.response.header.txt"
    $respBodyPath = Join-Path $DataDir "$Name.response.body.txt"

    if (-not (Test-Path $bodyPath)) {
        Write-Host "  [skip] $Name : no .request.body.json (GET/multipart not supported)" -ForegroundColor Yellow
        return
    }
    if (-not (Test-Path $reqHeaderPath)) {
        Write-Host "  [skip] $Name : no .request.header.txt" -ForegroundColor Yellow
        return
    }

    $parsed = Parse-RequestHeader -Path $reqHeaderPath
    $method = ($parsed.RequestLine -split ' ', 2)[0]
    $url = Get-AbsoluteUrl -RequestLine $parsed.RequestLine -Headers $parsed.Headers

    $body = Get-Content -LiteralPath $bodyPath -Raw

    # build outgoing request
    $req = [System.Net.Http.HttpRequestMessage]::new([System.Net.Http.HttpMethod]::new($method), $url)

    $contentType = 'application/json'
    $forwardHeaders = [ordered]@{}
    foreach ($k in $parsed.Headers.Keys) {
        if ($skipHeaders -contains $k.ToLower()) { continue }
        if ($k -ieq 'content-type') { $contentType = $parsed.Headers[$k]; continue }
        $forwardHeaders[$k] = $parsed.Headers[$k]
    }

    $content = [System.Net.Http.StringContent]::new($body, [System.Text.Encoding]::UTF8, $contentType)
    $req.Content = $content

    # Authorization always from env/key
    $authValue = if ($RedactAuth) { 'Bearer <redacted>' } else { "Bearer $ApiKey" }
    $req.Headers.TryAddWithoutValidation('Authorization', $authValue) | Out-Null
    foreach ($k in $forwardHeaders.Keys) {
        $req.Headers.TryAddWithoutValidation($k, $forwardHeaders[$k]) | Out-Null
    }

    $resp = $httpClient.SendAsync($req, [System.Net.Http.HttpCompletionOption]::ResponseHeadersRead).GetAwaiter().GetResult()
    $respBody = $resp.Content.ReadAsStringAsync().GetAwaiter().GetResult()

    # --- write request.header.txt (normalized) ---
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
    $cases = Resolve-Cases
    Write-Host "Refreshing $($cases.Count) case(s) against live DashScope API..." -ForegroundColor Cyan
    foreach ($c in $cases) { Invoke-RefreshCase -Name $c }
    Write-Host "Done." -ForegroundColor Green
}
finally {
    $httpClient.Dispose()
    $handler.Dispose()
}
