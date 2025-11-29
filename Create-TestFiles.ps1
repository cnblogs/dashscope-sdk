<#
.SYNOPSIS
    通过命令行参数，为测试目的创建一组标准的 HTTP 请求和响应文件。

.DESCRIPTION
    此脚本从命令行接受一个字符串 S 和一个类型 P (nosse 或 sse)。
    然后，它会在指定的目录 'src/Cnblogs.DashScope.Tests.Shared/RawHttpData' 下
    创建四个空文件：
    - S-P.request.header.txt
    - S-P.request.body.json
    - S-P.response.header.txt
    - S-P.response.body.txt

    如果目标目录不存在，脚本会自动创建它。

.PARAMETER S
    用于文件名前缀的字符串，例如: GetChatCompletion。

.PARAMETER P
    指定请求类型，只能是 'nosse' 或 'sse'。

.EXAMPLE
    # 使用命名参数创建文件
    .\Create-TestFiles.ps1 -S "GetChatCompletion" -P "nosse"

.EXAMPLE
    # 使用位置参数创建文件（第一个值对应-S，第二个值对应-P）
    .\Create-TestFiles.ps1 "StreamChat" "sse"

.EXAMPLE
    # 如果参数无效，PowerShell 会自动报错
    .\Create-TestFiles.ps1 -S "Test" -P "invalid"
    # 错误: Cannot validate argument on parameter 'P'. The argument "invalid" does not belong to the set "nosse","sse"...
#>

# --- 1. 定义命令行参数 ---

param (
    [Parameter(Mandatory=$true, HelpMessage="请输入字符串 S，例如: GetChatCompletion")]
    [string]$S,

    [Parameter(Mandatory=$true, HelpMessage="请输入类型 P，只能是 'nosse' 或 'sse'")]
    [ValidateSet("nosse", "sse")]
    [string]$P
)

# --- 2. 定义路径和文件名 ---

# 定义基础路径
$basePath = "src/Cnblogs.DashScope.Tests.Shared/RawHttpData"

# 构建文件名的基础部分
$baseFileName = "$S-$P"

# 定义所有需要创建的文件名
$filesToCreate = @(
    "$baseFileName.request.header.txt",
    "$baseFileName.request.body.json",
    "$baseFileName.response.header.txt",
    "$baseFileName.response.body.txt"
)

# --- 3. 检查并创建目录 ---

# 检查目录是否存在，如果不存在则创建
if (-not (Test-Path -Path $basePath -PathType Container)) {
    Write-Host "目录 '$basePath' 不存在，正在创建..." -ForegroundColor Yellow
    New-Item -Path $basePath -ItemType Directory -Force | Out-Null
}

# --- 4. 创建文件 ---

Write-Host "开始为 '$baseFileName' 创建文件..."

# 遍历文件名数组，创建每个文件
foreach ($fileName in $filesToCreate) {
    # 组合完整的文件路径
    $fullPath = Join-Path -Path $basePath -ChildPath $fileName

    # 创建空文件。-Force 参数会覆盖已存在的同名文件。
    New-Item -Path $fullPath -ItemType File -Force | Out-Null

    Write-Host "已创建: '$fullPath'" -ForegroundColor Cyan
}

Write-Host "所有文件创建完成！" -ForegroundColor Green
