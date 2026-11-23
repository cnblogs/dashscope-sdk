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
- Adding a case here means capturing real traffic: hand-write only the request body, then generate the other three files with `scripts/Update-Snapshots.ps1` (see [Refreshing RawHttpData snapshots](#refreshing-rawhttpdata-snapshots)). Finish by adding the snapshot record and registering it in the theory data.
- Prefer the SSE (streaming) variant when adding a new case: LLM APIs are expected to be streaming-first going forward, so a new case should always ship with `-sse` capture, and `-nosse` is only added when the non-streaming path itself is what you are testing.

## Layer 1: M.E.AI mapping tests (`test/Cnblogs.DashScope.AI.UnitTests`)

- These tests verify the mapping only, so expectations are plain in-memory objects with no raw files behind them. Mock `IDashScopeClient` with NSubstitute, call the chat/embedding client, and assert that the mapped `ModelRequest` is equivalent to the snapshot via `IsEquivalent`.
- Snapshots live in `Snapshots.MicrosoftExtensionsAi.cs` and deliberately carry no `Name`:
  - `AiClientSnapshot<TRequest, TResponse>` — non-streaming (`RequestModel` + `ResponseModel`)
  - `SdkMessageSnapshot<TRequest, TResponse>` — streaming (`Request` + a list of `Response`s)

## The rule

Never add a `RequestSnapshot` whose `Name` has no matching RawHttpData capture. Per-API snapshot files (`Snapshots.TextGeneration.cs`, `Snapshots.MultimodalGeneration.cs`, ...) must keep the "Name ↔ real capture" contract; M.E.AI expectations belong in `Snapshots.MicrosoftExtensionsAi.cs` with the Name-less types. A real snapshot may be shared by both layers (e.g. `ConversationMessageWithDocUrlsIncremental`).

## When to add an OpenAI compatible method

Some Bailian capabilities (e.g. qwen3-livetranslate-flash, temporary file upload for batch) are only served on the OpenAI compatible endpoints (`/compatible-mode/v1/*`); the SDK exposes them as the `OpenAiCompatible*` methods. **These methods are the fallback, not the default**: when a capability can be called via the DashScope API, always prefer the DashScope method (or the Microsoft.Extensions.AI integration for chat) - the OpenAI compatible family is only for capabilities the DashScope API cannot reach. The generic chat completions pair (`OpenAiCompatibleGetChatCompletionAsync` / `OpenAiCompatibleGetChatCompletionStreamAsync`) should not grow model-specific siblings; model-specific extras (e.g. `translation_options`, `speech_rate`) live as optional properties of the shared request type.

## Refreshing RawHttpData snapshots

Of the four files behind a snapshot case, only `<name>-<nosse|sse>.request.body.json` is hand-maintained. `scripts/Update-Snapshots.ps1` replays a request body against the live DashScope API and regenerates the other three files (`request.header.txt`, `response.header.txt`, `response.body.txt`), so fixtures always reflect what the API actually sends. Generating the three derived files with this script is the default; don't hand-write them.

1. Write the request body by hand. Optionally scaffold the four empty files first with `./Create-TestFiles.ps1 <name> <nosse|sse>`.
2. Put a minimal `request.header.txt` next to it — the script only reads the request line (`POST /api/v1/... HTTP/1.1`, or an absolute URL) plus the `Host` header for relative URLs. The file gets rewritten by the replay.
3. Set `$env:DASHSCOPE_API_KEY` and run the script:

   ```bash
   pwsh scripts/Update-Snapshots.ps1 -Case <name>-<nosse|sse> -RedactAuth
   # omit -Case to pick interactively from git-modified request bodies
   ```

4. Open the regenerated `response.body.txt` and copy the real values (request id, token counts, message content, ...) into the case's `ResponseModel` in `Snapshots.<Api>.cs`, then register the snapshot in the theory data.

Values the server computes (request ids, token counts, cache hits, model output) change between replays: re-running the script on an existing case means syncing its `ResponseModel` again. Always use `-RedactAuth` when committing, so the refreshed `request.header.txt` carries a redacted token instead of your real key.

Some streams answer with hundreds of chunks carrying large base64 payloads (e.g. qwen3-livetranslate-flash on the OpenAI compatible endpoint, about 1 MB of audio). Add `-TrimAudioDeltas` for such a case: the script keeps every chunk outside the audio and only the first and the last chunk of each run of audio chunks, with the base64 payload replaced by `<base64>`:

```bash
pwsh scripts/Update-Snapshots.ps1 -Case openai-compatible-livetranslate-sse -RedactAuth -TrimAudioDeltas
```

Some APIs (qwen-audio-tts speech synthesis / audio generation, voice customization) are only served on the workspace domain (`{WorkspaceId}.{region}.maas.aliyuncs.com`). For those cases write the request line with a `{WorkspaceId}` placeholder, mirroring the Bailian docs:

```
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer
```

The script resolves the placeholder from `-WorkspaceId` or `$env:DASHSCOPE_WORKSPACE_ID` (the workspace must belong to the API key's region) and writes the placeholder back when regenerating `request.header.txt`, so committed fixtures never carry the real workspace subdomain.

GET cases (e.g. `get-task-*` task queries) have no `.request.body.json`; the script replays the recorded URL as-is. Task queries only work within the task retention window (24h), and each poll returns a fresh request id, so replaying a task query case also means syncing the `RequestId` of its `ResponseModel`.

## Refreshing socket snapshots

Websocket cases (`socket-<group>.<message>.json`, e.g. the speech synthesizer) are replayed with the same script plus `-Socket`. The three request messages (`run-task`, `continue-task`, `finish-task`) are hand-maintained; the script connects to `wss://dashscope.aliyuncs.com/api-ws/v1/inference/`, drives one live session with them, and writes the captured server events verbatim as `socket-<group>.<event>.json` plus the audio stream as `socket-<group>.audio.<format>` (format taken from the run-task parameters). Afterwards, sync the typed `SocketMessageSnapshot` models in `Snapshots.SocketRequests.cs` with the captured values, same as the HTTP flow above.

For speech recognition (ASR) groups the audio flows the other way: put the hand-maintained input audio next to the requests as `socket-<group>.input.<format>` and the script sends it as binary frames (3200-byte chunks) after `task-started`, before `finish-task`. Repeated server events are kept in arrival order as `socket-<group>.<event>.json`, `socket-<group>.<event>-2.json`, ... which is how the intermediate and final `result-generated` events of one sentence end up in separate fixtures.

```bash
pwsh scripts/Update-Snapshots.ps1 -Socket -Case speech-synthesizer
```

TTS realtime groups (qwen3-tts-*-realtime models) use a different event protocol and are replayed with `-RealtimeTts -Model <model>`: the four client events (`session-update`, `append-text`, `commit-text`, `finish-session`) are hand-maintained; the script connects to `wss://dashscope.aliyuncs.com/api-ws/v1/realtime?model=<model>`, drives one live session, and writes the server events verbatim as `socket-<group>.<type>.json` (type contains dots, e.g. `response.audio.delta.json`; repeated events numbered `-2`, ...) plus the base64-decoded audio as `socket-<group>.audio.<format>` (format taken from the session-update).

```bash
pwsh scripts/Update-Snapshots.ps1 -RealtimeTts -Model qwen3-tts-flash-realtime -Case qwen-tts-realtime
```

If a `task-failed` event comes back the error body is captured too — a deliberately invalid model in `run-task` is a handy way to (re)capture the `task-failed` fixture. Verify audio-related parameters with `ffprobe` (e.g. `ffprobe -show_entries stream=bit_rate` should match the `bit_rate` parameter, in bps).

ASR realtime groups (qwen3-asr-*-realtime models) are replayed with `-RealtimeAsr -Model <model>`: the client events (`session-update`, optional `commit-audio`, `finish-session`) are hand-maintained and the input audio is put in `socket-<group>.input.<format>` (format taken from the session-update); the script connects to `wss://dashscope.aliyuncs.com/api-ws/v1/realtime?model=<model>`, appends the audio as base64 chunks of 3200 bytes, and writes the server events verbatim as `socket-<group>.<type>.json` (repeated events numbered `-2`, ...).

```bash
pwsh scripts/Update-Snapshots.ps1 -RealtimeAsr -Model qwen3-asr-flash-realtime -Case qwen-asr-realtime
```

Audio chat / live translation groups (qwen-audio-*-realtime, qwen*-livetranslate-*-realtime models) are replayed with `-RealtimeAudio -Model <model>`: same group files as `-RealtimeAsr` plus an optional `response-create` (sent after `input_audio_buffer.committed`, for the push-to-talk mode). The session ends at `session.finished` or at `response.done` (whichever arrives); because these sessions fire hundreds of delta events, the repeated delta streams (response.audio.delta, `*`.transcription delta / text, ...) are trimmed to their first and last occurrences before being written. For live translation VAD groups, put the `finish-session` alongside - the server needs it right after the audio to flush the remaining translation (otherwise the response stream times out).

```bash
pwsh scripts/Update-Snapshots.ps1 -RealtimeAudio -Model qwen-audio-3.0-realtime-flash -Case qwen-audio-realtime,qwen-audio-realtime-manual
```

Event files of the group that a replay does not produce are left untouched (e.g. an `error` captured once and kept on purpose) and are reported as `kept socket-<group>.<event>.json: not produced by this run` - delete them unless they are meant to stay.

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
- 在这一层新增用例意味着抓取真实流量：只需手写请求体，其余三个文件用 `scripts/Update-Snapshots.ps1` 生成（见「刷新 RawHttpData 快照」），然后添加快照并注册到 TheoryData。
- 新增用例时优先提供 SSE（流式）变体：后续大模型 API 预计以流式输出为主，因此新用例应当总是附带 `-sse` 抓包；只有当要验证的就是非流式路径本身时才补充 `-nosse` 变体。

## 第一层：M.E.AI 映射测试（`test/Cnblogs.DashScope.AI.UnitTests`）

- 这层测试只验证映射，因此期望值是纯内存对象，背后没有原始文件。用 NSubstitute Mock `IDashScopeClient`，调用 chat/embedding 客户端，然后通过 `IsEquivalent` 断言映射出的 `ModelRequest` 与快照等价。
- 快照放在 `Snapshots.MicrosoftExtensionsAi.cs`，刻意不带 `Name`：
  - `AiClientSnapshot<TRequest, TResponse>` —— 非流式（`RequestModel` + `ResponseModel`）
  - `SdkMessageSnapshot<TRequest, TResponse>` —— 流式（`Request` + 一组 `Response`）

## 一条规则

不要添加 `Name` 没有对应 RawHttpData 抓包的 `RequestSnapshot`。按 API 分类的快照文件（`Snapshots.TextGeneration.cs`、`Snapshots.MultimodalGeneration.cs`、...）必须保持「Name ↔ 真实抓包」的契约；M.E.AI 的期望值应放在 `Snapshots.MicrosoftExtensionsAi.cs`，使用不带 Name 的类型。真实快照允许两层共用（例如 `ConversationMessageWithDocUrlsIncremental`）。

## 什么时候加 OpenAI 兼容方法

百炼的某些能力（如 qwen3-livetranslate-flash、批处理的临时文件上传）只在 OpenAI 兼容端点（`/compatible-mode/v1/*`）上提供服务，SDK 以 `OpenAiCompatible*` 方法暴露它们。**这些方法是兜底，不是默认选项**：能力可以通过 DashScope 接口调用时，一律优先 DashScope 方法（聊天用 Microsoft.Extensions.AI 集成也行）—— OpenAI 兼容族只覆盖 DashScope 接口够不到的能力。通用的 chat completions 方法对（`OpenAiCompatibleGetChatCompletionAsync` / `OpenAiCompatibleGetChatCompletionStreamAsync`）不要再长出模型专用的兄弟方法；模型特有的扩展参数（如 `translation_options`、`speech_rate`）作为共享请求类型的可选属性存在。

## 刷新 RawHttpData 快照

每个快照用例背后的四个文件中，只有 `<name>-<nosse|sse>.request.body.json` 需要手工维护。`scripts/Update-Snapshots.ps1` 会把请求体重放到真实 DashScope API 并重新生成其余三个文件（`request.header.txt`、`response.header.txt`、`response.body.txt`），保证夹具始终反映 API 的真实行为。**用脚本生成这三个派生文件是默认流程，不要手写。**

1. 手写请求体。可以先用 `./Create-TestFiles.ps1 <name> <nosse|sse>` 生成四个空文件。
2. 在旁边放一个最小化的 `request.header.txt` —— 脚本只读取请求行（`POST /api/v1/... HTTP/1.1`，或绝对 URL）和相对 URL 时需要的 `Host` 头。该文件会被回放结果重写。
3. 设置 `$env:DASHSCOPE_API_KEY` 后运行脚本：

   ```bash
   pwsh scripts/Update-Snapshots.ps1 -Case <name>-<nosse|sse> -RedactAuth
   # 省略 -Case 可从 git 修改过的请求体中交互式选择
   ```

4. 打开重新生成的 `response.body.txt`，把真实值（request id、token 计数、消息内容等）同步到 `Snapshots.<Api>.cs` 中对应用例的 `ResponseModel`，然后将快照注册到 TheoryData。

服务端计算的值（request id、token 计数、缓存命中、模型输出）在每次回放间都会变化：对已有用例重跑脚本后需要再次同步其 `ResponseModel`。提交时始终使用 `-RedactAuth`，确保刷新后的 `request.header.txt` 中是脱敏 token 而不是真实密钥。

有些流式响应会返回数百个携带大段 base64 的 chunk（如 OpenAI 兼容端点上的 qwen3-livetranslate-flash，音频约 1 MB）。这类用例加 `-TrimAudioDeltas`：脚本保留音频之外的全部 chunk，每段连续的音频 chunk 只保留首尾两个，并把 base64 载荷替换为 `<base64>`：

```bash
pwsh scripts/Update-Snapshots.ps1 -Case openai-compatible-livetranslate-sse -RedactAuth -TrimAudioDeltas
```

部分 API（qwen-audio-tts 语音合成/音频生成、音色定制）只在 workspace 子域名（`{WorkspaceId}.{region}.maas.aliyuncs.com`）上提供服务。这类用例的请求行使用 `{WorkspaceId}` 占位符书写，与百炼文档保持一致：

```
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer
```

脚本从 `-WorkspaceId` 或 `$env:DASHSCOPE_WORKSPACE_ID` 解析占位符（业务空间须与 API Key 同地域），并在重新生成 `request.header.txt` 时写回占位符，确保提交的夹具中不包含真实的 workspace 子域名。

GET 用例（如 `get-task-*` 任务查询）没有 `.request.body.json`，脚本会按记录的 URL 原样重放。任务查询只在任务保留期（24 小时）内有效，且每次轮询都会返回新的 request id，因此重放任务查询用例后同样需要同步其 `ResponseModel` 的 `RequestId`。

## 刷新 socket 快照

WebSocket 用例（`socket-<group>.<message>.json`，如语音合成）用同一脚本加 `-Socket` 回放。三个请求消息（`run-task`、`continue-task`、`finish-task`）手工维护；脚本会连接 `wss://dashscope.aliyuncs.com/api-ws/v1/inference/`，用它们驱动一次真实会话，把捕获的服务端事件按原始报文写入 `socket-<group>.<event>.json`，音频流写入 `socket-<group>.audio.<format>`（format 取自 run-task 参数）。之后按上面的 HTTP 流程，把 `Snapshots.SocketRequests.cs` 中对应的 `SocketMessageSnapshot` 模型与捕获值同步。

语音识别（ASR）组的音频方向相反：把手工维护的输入音频放到请求消息旁边，命名为 `socket-<group>.input.<format>`，脚本会在 `task-started` 之后、`finish-task` 之前，把它按二进制帧（3200 字节分片）发送出去。重复出现的服务端事件会按到达顺序保留为 `socket-<group>.<event>.json`、`socket-<group>.<event>-2.json`……同一句的中间与最终 `result-generated` 事件就是这样落到不同夹具里的。

```bash
pwsh scripts/Update-Snapshots.ps1 -Socket -Case speech-synthesizer
```

TTS realtime 组（qwen3-tts-*-realtime 模型）使用另一套事件协议，用 `-RealtimeTts -Model <model>` 回放：四个客户端事件（`session-update`、`append-text`、`commit-text`、`finish-session`）手工维护；脚本连接 `wss://dashscope.aliyuncs.com/api-ws/v1/realtime?model=<model>`，驱动一次真实会话，服务端事件按原始报文写入 `socket-<group>.<type>.json`（type 含点号，如 `response.audio.delta.json`；重复事件编号 `-2`……），base64 解码后的音频写入 `socket-<group>.audio.<format>`（format 取自 session-update）。

```bash
pwsh scripts/Update-Snapshots.ps1 -RealtimeTts -Model qwen3-tts-flash-realtime -Case qwen-tts-realtime
```

如果收到 `task-failed` 事件，错误报文同样会被捕获——在 `run-task` 里故意填一个无效模型是（重新）捕获 `task-failed` 夹具的简便方法。音频相关参数可用 `ffprobe` 验证（例如 `ffprobe -show_entries stream=bit_rate` 的结果应与 `bit_rate` 参数一致，单位 bps）。

ASR realtime 组（qwen3-asr-*-realtime 模型）用 `-RealtimeAsr -Model <model>` 回放：客户端事件（`session-update`、可选的 `commit-audio`、`finish-session`）手工维护，输入音频放在 `socket-<group>.input.<format>`（format 取自 session-update）；脚本连接 `wss://dashscope.aliyuncs.com/api-ws/v1/realtime?model=<model>`，把音频按 3200 字节分块 base64 发送，服务端事件按原始报文写入 `socket-<group>.<type>.json`（重复事件编号 `-2`……）。

```bash
pwsh scripts/Update-Snapshots.ps1 -RealtimeAsr -Model qwen3-asr-flash-realtime -Case qwen-asr-realtime
```

语音对话 / 实时翻译组（qwen-audio-*-realtime、qwen*-livetranslate-*-realtime 模型）用 `-RealtimeAudio -Model <model>` 回放：组文件与 `-RealtimeAsr` 相同，另加可选的 `response-create`（在 `input_audio_buffer.committed` 之后发送，用于 push-to-talk 模式）。会话在 `session.finished` 或 `response.done`（先到者）处结束；由于这类会话会产生数百个 delta 事件，重复的 delta 流（response.audio.delta、`*`.transcription 的 delta / text……）写入前会修剪为首尾两个。实时翻译 VAD 组请把 `finish-session` 一并放好 —— 服务端需要在音频发完之后立刻收到它来冲刷剩余翻译（否则响应流会超时）。

```bash
pwsh scripts/Update-Snapshots.ps1 -RealtimeAudio -Model qwen-audio-3.0-realtime-flash -Case qwen-audio-realtime,qwen-audio-realtime-manual
```

回放没有产生的事件文件不会被改动（例如只捕获过一次、特意保留的 `error`），脚本会提示 `kept socket-<group>.<event>.json: not produced by this run` —— 不是有意保留的就删掉。

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
