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
- 新类型放独立文件；公开 API 写英文 XML 注释（CONTRIBUTING 要求）。
- **`Cnblogs.DashScope.Tests.Shared` 目标是 net6.0 / C# 10**：不能用集合表达式 `["x"]`，用 `new List<T> { "x" }`。
- 不硬编码模型名（Wanx 层和 `TextEmbeddingModel` 已删除），用户传字符串；新 API 只接受 string model。
- Obsolete 政策：标记满一个发布版本后可删（破坏性改动计划随 2.0.0）。

### 5. 快照三种流程

所有流程共同点：**只有请求（体/消息）是手写的，响应一律真机捕获**；捕获后把真实值同步进 `Snapshots.<Api>.cs` 的类型化快照并注册到测试的 TheoryData。`pwsh scripts/Update-Snapshots.ps1 -Case <name> -RedactAuth`（提交必须带 `-RedactAuth`）。

**HTTP 用例**（`{name}-{nosse|sse}.*` 四件套）：
1. 手写 `request.body.json` + 最小 `request.header.txt`（请求行 + Host）。
2. 脚本重放生成其余三个文件。
3. 新用例优先 sse 变体；非流式路径本身是被测对象时才用 nosse。

**GET 用例**（如 `get-task-*`，无 request body）：
- 脚本按 `request.header.txt` 里记录的 URL 原样重放。
- 任务查询只在 24h 保留期内有效；每次轮询返回新 request id，重放后需同步 `ResponseModel` 的 `RequestId`。

**WebSocket 用例**（`socket-{group}.{message}.json`）：
1. 手写 `run-task` / `continue-task` / `finish-task` 三个请求消息。
2. `pwsh scripts/Update-Snapshots.ps1 -Socket -Case <group>` 跑完整会话，服务端事件按原始报文写回 `socket-{group}.{event}.json`，音频写 `socket-{group}.audio.<format>`。
3. 故意填无效值（如不存在的模型）可以捕获真实的 `task-failed` 夹具。

### 6. 真机探测惯例

- **不要直接改正式夹具去试错**：复制到 `.tmp-probe/` 临时目录，用 `-DataDir .tmp-probe` 回放，结束后删除。
- 探测常见结论要沉淀：参数格式校验（如 `aigc_propagate_id` 拒绝 UUID、接受数字）、模型支持范围（如 cosyvoice-v2 不支持 instruction）、模型下线（`Model not exist`）。
- 音频相关参数用 ffprobe 验证：`bit_rate` 对应 bps（96 → 96000）；AIGC 隐性标写入库后 `ffprobe -show_entries format_tags` 能看到 `TAG:AIGC` 里的 `ContentPropagator`/`PropagateID`。
- 失败捕获也是资产：跑挂了的 task-failed 会写进夹具，记得同步类型化快照，否则等价断言会挂。

### 7. 文档

- README.md 和 README.zh-Hans.md **双语同步更新**（新增 API 示例时两份都要）。
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
