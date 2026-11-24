# AGENTS.md

面向在本仓库工作的 AI agent。项目是阿里云百炼（DashScope）的 .NET SDK，本质是两层映射：

1. `Microsoft.Extensions.AI` 抽象 → DashScope 模型（`src/Cnblogs.DashScope.AI`）
2. DashScope 模型 → 官方 wire format（`src/Cnblogs.DashScope.Core` 序列化 + `src/Cnblogs.DashScope.Sdk` HTTP/WebSocket）

测试分两层：MSEAI 层验证 MSEAI → DashScope SDK 的对象映射，wire 层以 `test/Cnblogs.DashScope.Tests.Shared/RawHttpData` 下的真实抓包为契约（见「两层测试的分工」）。详细规范见 [CONTRIBUTING.md](./CONTRIBUTING.md)，本文只讲操作流程和经验教训。

## 常用命令

```bash
dotnet build Cnblogs.DashScope.Sdk.sln -v q --nologo          # 0 错误才算过
dotnet test Cnblogs.DashScope.Sdk.sln --no-build --nologo     # Sdk.UnitTests × 3 TFM + AI.UnitTests
```

- 真机调用需要 `DASHSCOPE_API_KEY` 环境变量（已配置）。
- `pwsh`（7+）、`ffmpeg/ffprobe` 本机可用。
- `git status` 里的 `bin/`、`obj/` 噪音直接忽略。

## 两层测试的分工

| 层 | 测试项目 | 验证什么 | 期望值形态 |
|---|---|---|---|
| MSEAI → DashScope SDK | `test/Cnblogs.DashScope.AI.UnitTests` | MSEAI 抽象被映射成**形状正确的 DashScope 对象** | 纯内存对象，无 Name，不挂抓包 |
| DashScope SDK → 阿里云 | `test/Cnblogs.DashScope.Sdk.UnitTests` | 出站 HTTP/WebSocket 与真实抓包等价 | RawHttpData 真实快照（RequestSnapshot，Name ↔ 抓包契约） |

- MSEAI 层的测试（`ChatClientTests` / `EmbeddingClientTests`）：NSubstitute Mock `IDashScopeClient`，调用 chat/embedding client，用 `IsEquivalent` 断言 SDK 收到的 `ModelRequest` 与期望等价。**不验证真实快照**——它只关心映射后的对象形状。
- 真实快照永远属于 wire 层，用于验证 DashScope SDK → 阿里云的映射。不要给 MSEAI 层的期望值加 Name 或关联 RawHttpData 抓包；快照放 `Snapshots.MicrosoftExtensionsAi.cs`，用 `AiClientSnapshot<TRequest, TResponse>`（非流式）或 `SdkMessageSnapshot<TRequest, TResponse>`（流式）。真实快照允许两层共用（如 `ConversationMessageWithDocUrlsIncremental`）。
- 实操含义：给 MSEAI 层加测试不需要真机；给 wire 层加用例必须走真机捕获流程（见下）。

## API 同步工作流（issue → PR）

### 1. Issue

- 用 `.github/ISSUE_TEMPLATE/` 的模板（API 缺失 → `[API Sync]`），创建前先 `gh issue list --search` 查重。
- 正文结构：百炼文档链接 / SDK 对应的方法 / 其他信息（参数语义、取值范围、模型限制）。

### 2. 分支

- 命名：`support-<feature>` / `fix-<bug>` / `update-<doc>` / `remove-<target>`。
- 不推送，等用户审查后才提交&推送。

### 3. 对照官方文档

- 百炼文档的 markdown 快照在 `docs/bailian/`，用 `git diff` 看官方变更；`scripts/Update-BailianDocs.ps1` 可刷新。
- 文档没写的细节（参数格式校验、模型/音色支持范围）以**真机探测**为准。

### 4. Core 改动规则

- 序列化用 `SnakeCaseLower` + `WhenWritingNull`（见 `DashScopeDefaults.SerializationOptions`），PascalCase 属性自动映射 snake_case，一般不需要 `JsonPropertyName`；字典键不会被转换。
- `WhenWritingNull` 会把 null 属性整个省略。文档里「传 null = 关闭、不传 = 服务端默认」的成员（如 realtime session 的 `turn_detection`：对象 = 配置，`null` = 手动模式，不传 = 默认 VAD）这样表达：属性加 `[JsonIgnore(Condition = JsonIgnoreCondition.Never)]` 让 null 一定上线，再用一个静态哨兵对象（如 `AsrRealtimeTurnDetection.ServerDefault`）表示「省略」，发送前用 `ReferenceEquals` 比较并移除该成员（见 `AsrRealtimeSocketSession.UpdateSessionAsync`）。**不要为此加额外的 bool 字段**。STJ 的值级别 converter 只能把哨兵写成 null，做不到「省略成员」，所以比较放在组装事件的地方；只有两态（null 即关闭、不需要「省略」）的成员只加 `Never` 即可。新 API 的「设为 null 即可」类参数一律真机确认回显。
- 新类型放独立文件；公开 API 写英文 XML 注释（CONTRIBUTING 要求）。
- **一个端点的一种调用模式对应一个方法，输入类型也只有一个**：类比 ASP.NET Core 的「HTTP 动词 + 路径 = 一个 handler」。body 的差异（不同模型族的字段）用同一输入/参数类型上的**可选属性**表达，注释标明适用模型；不要为不同 body 形态发明新类型或新重载（如 `VideoSynthesisInput` / `IVideoSynthesisParameters` 一个类型族覆盖 HappyHorse/wan2.7/wan3.0/legacy wan2.1-2.6/VACE 的全部字段）。同一 URL 的同步调用与异步任务（`X-DashScope-Async` 头区分）算两种调用模式，各有一个方法名（如 `GetEmbeddingsAsync` / `BatchGetEmbeddingsAsync`）；REST 资源组（Files/Batches）按路由+动词区分，不受此限。理由：本 SDK 非官方（百炼文档里不存在），用户是拿着百炼文档的 curl 示例在 IDE 里找对应方法的，任何我们自己发明的类型名都会迫使他们频繁翻 README。
- **`Cnblogs.DashScope.Tests.Shared` 目标是 net6.0 / C# 10**：不能用集合表达式 `["x"]`，用 `new List<T> { "x" }`。
- 不硬编码模型名（Wanx 层和 `TextEmbeddingModel` 已删除），用户传字符串；新 API 只接受 string model。
- Obsolete 政策：标记满一个发布版本后可删（破坏性改动计划随 2.0.0）。

### 5. 快照三种流程

所有流程共同点：**只有请求（体/消息）是手写的，响应一律真机捕获**；捕获后把真实值同步进 `Snapshots.<Api>.cs` 的类型化快照并注册到测试的 TheoryData。`pwsh scripts/Update-Snapshots.ps1 -Case <name> -RedactAuth`（提交必须带 `-RedactAuth`）。

**HTTP 用例**（`{name}-{nosse|sse}.*` 四件套）：
1. 手写 `request.body.json` + 最小 `request.header.txt`（请求行 + Host）。
2. 脚本重放生成其余三个文件。
3. 新用例优先 sse 变体；非流式路径本身是被测对象时才用 nosse。
4. 响应里带大段 base64（如音频）时加 `-TrimAudioDeltas`：脚本只保留每段音频 chunk 的首尾、载荷换成 `<base64>` 占位符。不要手工修剪夹具 —— 重跑脚本会被整份覆盖回去。

**GET 用例**（如 `get-task-*`，无 request body）：
- 脚本按 `request.header.txt` 里记录的 URL 原样重放。
- 任务查询只在 24h 保留期内有效；每次轮询返回新 request id，重放后需同步 `ResponseModel` 的 `RequestId`。

**WebSocket 用例**（`socket-{group}.{message}.json`）：
1. 手写 `run-task` / `continue-task` / `finish-task` 三个请求消息。
2. `pwsh scripts/Update-Snapshots.ps1 -Socket -Case <group>` 跑完整会话，服务端事件按原始报文写回 `socket-{group}.{event}.json`，音频写 `socket-{group}.audio.<format>`。
3. 故意填无效值（如不存在的模型）可以捕获真实的 `task-failed` 夹具。
4. 回放没有产生的事件文件脚本不会删，只会提示 `kept socket-{group}.{event}.json: not produced by this run`：不是有意保留的就删掉；有意保留的（如真实捕获的 `error` 事件）要有测试用上它。

### 6. 真机探测惯例

- **不要直接改正式夹具去试错**：复制到 `.tmp-probe/` 临时目录，用 `-DataDir .tmp-probe` 回放，结束后删除。
- 探测常见结论要沉淀：参数格式校验（如 `aigc_propagate_id` 拒绝 UUID、接受数字）、模型支持范围（如 cosyvoice-v2 不支持 instruction）、模型下线（`Model not exist`）。
- 音频相关参数用 ffprobe 验证：`bit_rate` 对应 bps（96 → 96000）；AIGC 隐性标写入库后 `ffprobe -show_entries format_tags` 能看到 `TAG:AIGC` 里的 `ContentPropagator`/`PropagateID`。
- 失败捕获也是资产：跑挂了的 task-failed 会写进夹具，记得同步类型化快照，否则等价断言会挂。

### 7. 文档

- README.md 和 README.zh-Hans.md **双语同步更新**（新增 API 示例时两份都要）。
- sample 只在新调用形态出现时添加：与现有调用模式完全一致、仅模型名/参数不同的 API 不加 sample，README 示例即可（如 z-image-turbo 走 `GetMultimodalGenerationAsync`）。
- 流程/脚本行为变化时更新 CONTRIBUTING.md（也是双语）。

### 8. 提交

- Conventional commits，英文：`feat: ...` / `fix: ...` / `docs: ...` / `refactor!: ...`；破坏性加 `!`；标题关联 issue 号 `(#xxx)`。
- 提交前全量测试必须绿。

## 快速参考：一次典型的参数同步

```bash
git checkout main && git pull --ff-only && git checkout -b support-xxx
# 1. Core 加属性/类型
# 2. 手写请求体（HTTP）或 run-task 消息（WebSocket）
pwsh scripts/Update-Snapshots.ps1 -Case <name> -RedactAuth            # HTTP
pwsh scripts/Update-Snapshots.ps1 -Socket -Case <group>               # WebSocket
# 3. 同步 Snapshots.<Api>.cs 的 ResponseModel（request id/usage/输出等真实值）
# 4. README 双语
dotnet build Cnblogs.DashScope.Sdk.sln -v q --nologo
dotnet test Cnblogs.DashScope.Sdk.sln --no-build --nologo
git add -A && git commit -m "feat: ... (#issue)"
```
