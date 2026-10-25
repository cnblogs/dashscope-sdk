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
