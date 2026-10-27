# Before submitting your PR

- DON'T make PRs for style changes or just typos.
- DON'T surprise us with big pull requests. Instead, file an issue and start a discussion so we can agree on a direction before you invest a large amount of time.
- DON'T add API additions without filing an issue and discussing with us first.
- DO write tests for your code.
- DO write XML comments for your public APIs.
- DO give priority to the current style of the project or file you're changing.

# 提交 PR 之前

- 拒绝 PR 突袭。在添加新的 API 之前，请先提 Issue 与我们讨论。
- 只包含代码风格改变/修改错字的 PR 会被拒绝，请提 Issue。
- 拒绝超大 PR。请确保已经在 Issue 里进行了充分讨论并达成了一致。
- 为你的代码添加测试。如果是 BUG 修复，尽可能添加一个回归测试。
- 为公开 API 添加 XML 注释，注释内容需要是英文的。
- 尊重原有文件或项目的代码风格。

# Testing the two mapping layers

The SDK is essentially two layers of mapping, and each layer has its own kind of test:

1. `Microsoft.Extensions.AI` abstractions → DashScope models (`src/Cnblogs.DashScope.AI`, e.g. `ChatOptions.ToolMode` → `ToolChoice`)
2. DashScope models → Alibaba Cloud wire format (`src/Cnblogs.DashScope.Core` serialization + `src/Cnblogs.DashScope.Sdk` HTTP)

## Layer 2: wire format tests (`test/Cnblogs.DashScope.Sdk.UnitTests`)

- Test cases are `RequestSnapshot<TRequest, TResponse>` records defined in `test/Cnblogs.DashScope.Tests.Shared/Utils/Snapshots.<Api>.cs` (e.g. `Snapshots.TextGeneration.cs`). The `Name` is a contract, not decoration: it maps to four captured raw HTTP files under `test/Cnblogs.DashScope.Tests.Shared/RawHttpData`:

  ```
  {name}-{nosse|sse}.request.header.txt
  {name}-{nosse|sse}.request.body.json
  {name}-{nosse|sse}.response.header.txt
  {name}-{nosse|sse}.response.body.txt
  ```

- The `*SerializationTests` assert that the outgoing HTTP request body is JSON-equivalent to the captured request, and that captured responses deserialize into the expected models. Snapshots are referenced from the `TheoryData` fields at the bottom of each test class.
- Adding a case here means capturing real traffic: run `./Create-TestFiles.ps1 <name> <nosse|sse>` to scaffold the four empty files, fill them with the real capture, then add the snapshot and register it in the theory data.

## Layer 1: M.E.AI mapping tests (`test/Cnblogs.DashScope.AI.UnitTests`)

- These tests verify the mapping only, so expectations are plain in-memory objects with no raw files behind them. Mock `IDashScopeClient` with NSubstitute, call the chat/embedding client, and assert that the mapped `ModelRequest` is equivalent to the snapshot via `IsEquivalent`.
- Snapshots live in `Snapshots.MicrosoftExtensionsAi.cs` and deliberately carry no `Name`:
  - `AiClientSnapshot<TRequest, TResponse>` — non-streaming (`RequestModel` + `ResponseModel`)
  - `SdkMessageSnapshot<TRequest, TResponse>` — streaming (`Request` + a list of `Response`s)

## The rule

Never add a `RequestSnapshot` whose `Name` has no matching RawHttpData capture. Per-API snapshot files (`Snapshots.TextGeneration.cs`, `Snapshots.MultimodalGeneration.cs`, ...) must keep the "Name ↔ real capture" contract; M.E.AI expectations belong in `Snapshots.MicrosoftExtensionsAi.cs` with the Name-less types. A real snapshot may be shared by both layers (e.g. `ConversationMessageWithDocUrlsIncremental`).

# 测试两层映射

本 SDK 本质上是两层映射，每层各有对应的测试：

1. `Microsoft.Extensions.AI` 抽象 → DashScope 模型（`src/Cnblogs.DashScope.AI`，例如 `ChatOptions.ToolMode` → `ToolChoice`）
2. DashScope 模型 → 阿里云 wire format（`src/Cnblogs.DashScope.Core` 序列化 + `src/Cnblogs.DashScope.Sdk` HTTP）

## 第二层：wire format 测试（`test/Cnblogs.DashScope.Sdk.UnitTests`）

- 测试用例是 `RequestSnapshot<TRequest, TResponse>` record，定义在 `test/Cnblogs.DashScope.Tests.Shared/Utils/Snapshots.<Api>.cs`（例如 `Snapshots.TextGeneration.cs`）。其中 `Name` 是契约而不是装饰：它对应 `test/Cnblogs.DashScope.Tests.Shared/RawHttpData` 下的四个真实抓包文件：

  ```
  {name}-{nosse|sse}.request.header.txt
  {name}-{nosse|sse}.request.body.json
  {name}-{nosse|sse}.response.header.txt
  {name}-{nosse|sse}.response.body.txt
  ```

- `*SerializationTests` 断言出站 HTTP 请求体与抓包请求 JSON 等价，且抓包响应能反序列化为期望的模型。快照通过各测试类底部的 `TheoryData` 字段引用。
- 在这一层新增用例意味着抓取真实流量：先运行 `./Create-TestFiles.ps1 <name> <nosse|sse>` 生成四个空文件，填入真实抓包内容，然后添加快照并注册到 TheoryData。

## 第一层：M.E.AI 映射测试（`test/Cnblogs.DashScope.AI.UnitTests`）

- 这层测试只验证映射，因此期望值是纯内存对象，背后没有原始文件。用 NSubstitute Mock `IDashScopeClient`，调用 chat/embedding 客户端，然后通过 `IsEquivalent` 断言映射出的 `ModelRequest` 与快照等价。
- 快照放在 `Snapshots.MicrosoftExtensionsAi.cs`，刻意不带 `Name`：
  - `AiClientSnapshot<TRequest, TResponse>` —— 非流式（`RequestModel` + `ResponseModel`）
  - `SdkMessageSnapshot<TRequest, TResponse>` —— 流式（`Request` + 一组 `Response`）

## 一条规则

不要添加 `Name` 没有对应 RawHttpData 抓包的 `RequestSnapshot`。按 API 分类的快照文件（`Snapshots.TextGeneration.cs`、`Snapshots.MultimodalGeneration.cs`、...）必须保持「Name ↔ 真实抓包」的契约；M.E.AI 的期望值应放在 `Snapshots.MicrosoftExtensionsAi.cs`，使用不带 Name 的类型。真实快照允许两层共用（例如 `ConversationMessageWithDocUrlsIncremental`）。

# Syncing upstream Bailian API docs

The Markdown source of the Bailian (Model Studio) API docs is tracked under `docs/bailian/` so upstream API changes can be reviewed with plain `git diff`. Requires PowerShell 7+ (`pwsh`) and `curl.exe`.

- `scripts/Update-BailianDocs.ps1` downloads the site index (`llms.txt`), pulls the pages of the API reference tree into `docs/bailian/`, and regenerates `docs/bailian/index.md`. Changes in `index.md` mean pages were added, removed, or renamed upstream.
- Unchanged files are never rewritten, so whatever shows up in `git status` after a run is a real upstream edit.
- Language-SDK pages (Java/Python/Android/...) are excluded by default; add `-IncludeSdkPages` to include them, and `-Prune` to delete local files that disappeared from the index.

```bash
pwsh scripts/Update-BailianDocs.ps1                    # or: -Prune
git diff --stat docs/bailian                           # which pages changed upstream
git diff docs/bailian/qwen-api-via-dashscope.md        # e.g. text generation API parameters
```

When a diff reveals an API/parameter the SDK is missing or that got deprecated, file an issue with the "API 缺失" (`[API Sync]`) template and link the doc page, then commit the refreshed `docs/bailian/` snapshot alongside (or before) the SDK changes that react to it.

# 同步百炼官方文档

百炼 API 文档的 Markdown 原文存放在 `docs/bailian/`，用于通过 `git diff` 跟踪官方 API 的变更。需要 PowerShell 7+（`pwsh`）和 `curl.exe`。

- `scripts/Update-BailianDocs.ps1` 会下载全站索引（`llms.txt`），将「API 参考」树的页面拉取到 `docs/bailian/`，并重新生成 `docs/bailian/index.md`；`index.md` 出现变化即表示官方新增、下线或改名了页面。
- 内容未变化的文件不会被重写，运行后 `git status` 里出现的都是官方真实改动。
- 默认排除各语言 SDK 文档（Java/Python/Android/...），可用 `-IncludeSdkPages` 包含；官方下线页面后可用 `-Prune` 清理本地残留文件。

```bash
pwsh scripts/Update-BailianDocs.ps1                    # 或加 -Prune
git diff --stat docs/bailian                           # 官方改动了哪些页面
git diff docs/bailian/qwen-api-via-dashscope.md        # 例如文本生成 API 的参数变更
```

如果 diff 中发现 SDK 缺失的 API/参数或已废弃项，请使用「API 缺失」（`[API Sync]`）模板提 Issue 并附上文档链接；随后把刷新后的 `docs/bailian/` 快照与响应这些变更的 SDK 代码一起提交（或先提交快照）。
