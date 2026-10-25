> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 知识问答

> 基于知识库的智能问答接口，通过 SSE 流式输出，依次返回规划、工具调用、生成三个阶段。

<meta property="og:title" content="知识问答" />

## 接口说明 <span id="section-interface-description-title" />

- <strong>权限要求</strong>：调用本接口需提供阿里云百炼 API Key及业务空间。在控制台 [API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)及[业务空间管理](https://bailian.console.aliyun.com/cn-beijing?tab=globalset#/efm/business_management)获取。
- <strong>调用方式</strong>：HTTP REST，`POST` + `application/json`。Base URL 为 `https://{workspaceId}.cn-beijing.maas.aliyuncs.com`，其中 `{workspaceId}` 为业务空间 ID。
- <strong>前置条件</strong>：调用前须在百炼控制台 [知识问答服务页面](https://bailian.console.aliyun.com/cn-beijing/rag/qa/list) 创建并发布知识问答服务，获取应用 ID（`agent_id`），否则报 Agent 未发布。
- <strong>多轮上下文</strong>：平台不保存对话状态，每次请求需传入完整 `messages` 历史。建议限制历史长度（如最近 10 轮），避免超出模型上下文窗口。响应不返回 `session_id`。
- <strong>流式要求</strong>：`stream` 必须为 `true`，当前版本仅支持流式响应；若为 `false` 或为空将导致请求失败。
- <strong>临时文件</strong>：开启控制台文件预解析后，可在对话时通过 `parameters.agent_options.session_files` 临时传入文件（最多 10 个），文件 ID 通过[添加文件](/zh/model-studio/api-bailian-2023-12-29-addfile)接口获取。
- <strong>限流</strong>：默认用户维度 25 QPS。如遇限流，请稍后重试。

## 请求语法 <span id="section-request-syntax-title" />

```plaintext
POST /api/v2/apps/knowledge/chat HTTP/1.1
Host: {workspaceId}.cn-beijing.maas.aliyuncs.com
Authorization: Bearer <API-Key>
Content-Type: application/json
Accept: text/event-stream
```

## 请求参数 <span id="section-request-parameters-title" />

请求体为 `input` / `parameters` / `stream` 三部分。嵌套字段在下表中以缩进表示层级。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "26%" }} /><col style={{ width: "14%" }} /><col style={{ width: "10%" }} /><col style={{ width: "40%" }} /><col style={{ width: "10%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>名称</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>必填</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th><th style={{ verticalAlign: "top" }}><p>示例</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>input</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>输入参数。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>input.messages</p></td><td style={{ verticalAlign: "top" }}><p>array\<object></p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>对话消息列表，DashScope 标准格式。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>messages\[].role</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>消息角色：<code>user</code> 或 <code>assistant</code>。</p></td><td style={{ verticalAlign: "top" }}><p>user</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>messages\[].content</p></td><td style={{ verticalAlign: "top" }}><p>string\\|array</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>消息内容，支持纯文本或多模态数组。纯文本传字符串；多模态传数组，元素为 ContentPart（如 <code>\[\{"type":"text","text":"..."},\{"type":"image\_url","image\_url":\{"url":"..."}}]</code>）。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>input.request\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>业务侧自定义请求 ID。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>parameters</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>配置参数字段。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>parameters.agent\_options</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>智能体专用参数，包含 <code>agent\_id</code> 等。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>agent\_options.agent\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>问答服务（agent）应用 ID，在控制台知识问答页面创建并发布后获取。</p></td><td style={{ verticalAlign: "top" }}><p>aid-xxxxxxxxxxxxxxxx</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>agent\_options.session\_files</p></td><td style={{ verticalAlign: "top" }}><p>array\<string></p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>会话文件 ID 列表，最多 10 个。须在控制台开启文件预解析后传入才生效；文件 ID 通过 <code>addFile</code>（注册文件）接口获取。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>agent\_options.enable\_cache\_control</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>是否开启显式上下文缓存（KV Cache）。<code>true</code> 时请求打下缓存断点，前缀一致即确定性命中，命中部分的输入 Token 按折扣计费且首包延迟降低；<code>false</code>（默认）时不打断点，是否命中取决于模型侧的隐式缓存能力。同一请求只生效一种缓存模式，详见<a href="/zh/model-studio/knowledgechat#section-context-cache-title">上下文缓存</a>。</p></td><td style={{ verticalAlign: "top" }}><p>false</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>stream</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>是否开启流式输出。必须填 <code>true</code>，当前版本仅支持流式响应；填 <code>false</code> 或不填请求将失败。</p></td><td style={{ verticalAlign: "top" }}><p>true</p></td></tr></tbody></table>

## 响应参数 <span id="section-response-parameters-title" />

<strong>顶层字段</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "15%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>名称</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>output</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>模型输出。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>output.request\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>业务侧自定义请求 ID（与请求 <code>input.request\_id</code> 对应）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>output.choices</p></td><td style={{ verticalAlign: "top" }}><p>array\<object></p></td><td style={{ verticalAlign: "top" }}><p>模型输出信息，长度恒为 1。结构见下表。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>状态码，成功为 <code>200</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>状态信息，成功为 <code>Success</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>request\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>请求 ID（DashScope 平台级，全流不变）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>usage</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>用量统计（<code>input\_tokens</code> / <code>output\_tokens</code> / <code>total\_tokens</code> / <code>cached\_tokens</code>）。仅出现在 <code>tool\_calling</code> 与 <code>generation\_end</code> 等结算帧，普通流式片段不携带。<code>cached\_tokens</code> 为本次请求命中上下文缓存的输入 tokens，是多轮模型调用的累计值，开启方式见<a href="/zh/model-studio/knowledgechat#section-context-cache-title">上下文缓存</a>。</p></td></tr></tbody></table>

<strong>output.choices\[] 结构</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "15%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>名称</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>message</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>一条消息，包含模型回复的内容、工具调用信息和当前所处阶段。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.role</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>角色：<code>user</code>、<code>assistant</code>、<code>tool</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.content</p></td><td style={{ verticalAlign: "top" }}><p>string\\|array</p></td><td style={{ verticalAlign: "top" }}><p>生成内容/工具返回内容。<code>planning</code> 阶段为规划文本流，<code>generating</code> 阶段为回答文本流，边界帧可为空字符串。也可是多模态数组。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>本次 run 的唯一 ID（如 <code>run--xxxx</code>），同一轮内不变。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>消息类型：<code>ai</code>（assistant 帧，含思考/planning/生成）、<code>tool</code>（工具返回帧）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.tool\_calls</p></td><td style={{ verticalAlign: "top" }}><p>array\<object></p></td><td style={{ verticalAlign: "top" }}><p>工具调用信息。仅 <code>tool\_calling</code> 帧携带，其余为 <code>\[]</code>。每个元素含 <code>index</code> / <code>id</code> / <code>type</code>(固定 <code>function</code>) / <code>function.name</code> / <code>function.arguments</code>（JSON 字符串，需二次解析）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.tool\_call\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>工具返回帧：对应 <code>tool\_calls\[].id</code>，用于关联触发帧与返回帧。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.additional\_kwargs</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>附加参数。assistant 帧为 <code>\{}</code>；tool 帧携带 <code>extra\_json</code>，为工具返回的结构化结果（检索类工具为 <code>docs</code> 切片数组）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.extra</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>步骤状态信息。见下表。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message.response\_metadata</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>请求模型调用详细信息。边界帧携带 <code>agent\_name</code>、<code>request\_id</code>，有时含 <code>model\_name</code>、<code>finish\_reason</code>；普通片段为 <code>\{}</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>finish\_reason</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>生成结束原因，仅尾包输出 <code>stop</code>；流式片段为 <code>""</code>。</p></td></tr></tbody></table>

<strong>message.extra 结构</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "15%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>名称</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>group</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>大阶段：<code>planning</code>（规划阶段，包含开始和结束事件）、<code>generating</code>（生成阶段，包含开始和结束事件）。工具调用时大阶段还是 <code>planning</code>，只是细分步骤变成 <code>tool\_calling</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>step</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>当前步骤：<code>planning</code>、<code>tool\_calling</code>、<code>generating</code>。<code>step\_change</code> 有时可能不返回，建议以 <code>step</code> 为准。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>step\_change</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>步骤变化事件：<code>plan\_start</code> / <code>plan\_end</code> / <code>tool\_calling</code> / <code>tool\_return</code> / <code>generation\_start</code> / <code>generation\_end</code>，或空（进行中）。空包时可能不存在。</p></td></tr></tbody></table>

<strong>执行阶段（group）</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>group</p></th><th style={{ verticalAlign: "top" }}><p>说明</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>planning</p></td><td style={{ verticalAlign: "top" }}><p>规划中，包含 start 和 end 事件。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>generating</p></td><td style={{ verticalAlign: "top" }}><p>生成中，包含 start 和 end 事件。</p></td></tr></tbody></table>

<strong>当前步骤（step）</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>step</p></th><th style={{ verticalAlign: "top" }}><p>说明</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>planning</p></td><td style={{ verticalAlign: "top" }}><p>规划中。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tool\_calling</p></td><td style={{ verticalAlign: "top" }}><p>工具调用中（此时 <code>group</code> 仍为 <code>planning</code>）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>generating</p></td><td style={{ verticalAlign: "top" }}><p>生成中。</p></td></tr></tbody></table>

<strong>步骤变化事件（step\_change）</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "18%" }} /><col style={{ width: "18%" }} /><col style={{ width: "18%" }} /><col style={{ width: "46%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>step</p></th><th style={{ verticalAlign: "top" }}><p>step\_change</p></th><th style={{ verticalAlign: "top" }}><p>事件</p></th><th style={{ verticalAlign: "top" }}><p>说明</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>planning</p></td><td style={{ verticalAlign: "top" }}><p>plan\_start</p></td><td style={{ verticalAlign: "top" }}><p>开始规划</p></td><td style={{ verticalAlign: "top" }}><p><code>step</code> 变为 <code>planning</code>，后续 <code>content</code> 为规划内容。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>planning</p></td><td style={{ verticalAlign: "top" }}><p>空</p></td><td style={{ verticalAlign: "top" }}><p>规划中</p></td><td style={{ verticalAlign: "top" }}><p><code>content</code> 为规划文本流。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>planning</p></td><td style={{ verticalAlign: "top" }}><p>plan\_end</p></td><td style={{ verticalAlign: "top" }}><p>结束规划</p></td><td style={{ verticalAlign: "top" }}><p><code>step</code> 即将变化，事件发生时仍为 <code>planning</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tool\_calling</p></td><td style={{ verticalAlign: "top" }}><p>tool\_calling</p></td><td style={{ verticalAlign: "top" }}><p>工具调用</p></td><td style={{ verticalAlign: "top" }}><p>抛出完整 <code>tool\_calls</code>（含工具名与参数），携带本轮 <code>usage</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tool\_calling</p></td><td style={{ verticalAlign: "top" }}><p>tool\_return</p></td><td style={{ verticalAlign: "top" }}><p>工具返回</p></td><td style={{ verticalAlign: "top" }}><p><code>role</code> 为 <code>tool</code>，<code>content</code> 为返回摘要，<code>additional\_kwargs.extra\_json</code> 为结构化返回（检索类即 <code>docs</code>）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>generating</p></td><td style={{ verticalAlign: "top" }}><p>generation\_start</p></td><td style={{ verticalAlign: "top" }}><p>开始生成</p></td><td style={{ verticalAlign: "top" }}><p>后续 <code>content</code> 为最终回答文本流。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>generating</p></td><td style={{ verticalAlign: "top" }}><p>空</p></td><td style={{ verticalAlign: "top" }}><p>生成中</p></td><td style={{ verticalAlign: "top" }}><p><code>content</code> 为回答文本流。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>generating</p></td><td style={{ verticalAlign: "top" }}><p>generation\_end</p></td><td style={{ verticalAlign: "top" }}><p>结束生成</p></td><td style={{ verticalAlign: "top" }}><p><code>finish\_reason</code> 为 <code>stop</code>，携带最终 <code>usage</code>。</p></td></tr></tbody></table>

<strong>工具清单</strong>

模型在工具调用阶段会从以下工具里选用，工具名放在 `tool_calls[].function.name`。`arguments` 是一段 JSON 字符串，需要再解析一次才能拿到具体参数；工具的返回结果放在 `tool_return` 帧的 `additional_kwargs.extra_json.docs` 里。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "22%" }} /><col style={{ width: "22%" }} /><col style={{ width: "26%" }} /><col style={{ width: "30%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>function.name</p></th><th style={{ verticalAlign: "top" }}><p>工具</p></th><th style={{ verticalAlign: "top" }}><p>arguments</p></th><th style={{ verticalAlign: "top" }}><p>返回 docs\[]</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>semantic\_search</p></td><td style={{ verticalAlign: "top" }}><p>知识库搜索</p></td><td style={{ verticalAlign: "top" }}><p>\{"query", "target\_ids"}</p></td><td style={{ verticalAlign: "top" }}><p>命中切片数组，含正文与多维得分。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>obtain\_file</p></td><td style={{ verticalAlign: "top" }}><p>获取文件完整内容</p></td><td style={{ verticalAlign: "top" }}><p>\{"file\_id", "max\_tokens"}</p></td><td style={{ verticalAlign: "top" }}><p>文件级信息（含 Markdown 预签 URL），全文在 <code>message.content</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>execute\_sql</p></td><td style={{ verticalAlign: "top" }}><p>执行 SQL 查询（NL2SQL）</p></td><td style={{ verticalAlign: "top" }}><p>\{"sql", "knowledge\_base\_id"}</p></td><td style={{ verticalAlign: "top" }}><p>SQL 结果行，每行 = <code>\_citation\_index</code> + 查询列字段。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>section\_browse</p></td><td style={{ verticalAlign: "top" }}><p>章节检索</p></td><td style={{ verticalAlign: "top" }}><p>\{"knowledge\_base\_id", "file\_id", "section\_path"}</p></td><td style={{ verticalAlign: "top" }}><p>章节预览（前 300 字）。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>section\_peruse</p></td><td style={{ verticalAlign: "top" }}><p>章节精读</p></td><td style={{ verticalAlign: "top" }}><p>\{"knowledge\_base\_id", "file\_id", "section\_path", "max\_tokens"}</p></td><td style={{ verticalAlign: "top" }}><p>章节完整内容。</p></td></tr></tbody></table>

<Note>
  `max_tokens`（`obtain_file` / `section_peruse`）为字符串形式的数字，如 `"989482"`。`target_ids` / `knowledge_base_id` 即知识库（pipeline）ID；`section_path` 由`>` 分隔层级，例如：`开放接口文档>接口调用说明`。一次请求中多个工具可串联调用（如 `semantic_search` 定位文件 → `obtain_file` 取全文）。
</Note>

## 上下文缓存 <span id="section-context-cache-title" />

问答链路已适配模型侧的上下文缓存（KV Cache）：多轮对话与多轮工具调用中请求前缀保持稳定，命中部分的输入 Token 按折扣价计费，且首包延迟降低。缓存分为隐式、显式两种模式，<strong>同一请求只生效一种</strong>：

|         | 隐式缓存                 | 显式缓存                              |
| ------- | -------------------- | --------------------------------- |
| 如何启用    | 模型支持则默认生效，无需配置，也无法关闭 | 请求中传 `enable_cache_control: true` |
| 命中确定性   | 不确定，由系统自动识别公共前缀      | 确定性命中（前缀一致即命中）                    |
| 创建缓存计费  | 输入单价 100%（无额外开销）     | 输入单价 125%                         |
| 命中缓存计费  | 输入单价 20%             | 输入单价 10%                          |
| 缓存有效期   | 不确定，系统定期清理           | 5 分钟，每次命中后重置                      |
| 最小可缓存长度 | 1024 Token           | 1024 Token                        |

<Note>
  计费比例与有效期以百炼官方文档[上下文缓存](https://help.aliyun.com/zh/model-studio/context-cache)、[显式缓存最佳实践](https://help.aliyun.com/zh/model-studio/explicit-cache-guide)及控制台账单为准。内部实测隐式缓存命中率约为显式缓存的 75%，仅供参考，非官方指标。
</Note>

### 开启显式缓存 <span id="section-context-cache-enable-explicit" />

在 `parameters.agent_options` 中增加 `enable_cache_control` 即可，默认 `false`，其余参数保持不变：

```bash
curl -X POST "https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/knowledge/chat" \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "input": {
      "messages": [
        {"role": "user", "content": "这几款酒的产区分别是什么？"}
      ]
    },
    "parameters": {
      "agent_options": {
        "agent_id": "aid-xxxxxxxxxxxxxxxx",
        "enable_cache_control": true
      }
    },
    "stream": true
  }'
```

### 模型支持 <span id="section-context-cache-model-support" />

两种缓存的模型支持范围不同。模型由平台侧配置决定，本接口不暴露模型选择参数，能用哪种缓存取决于问答服务实际使用的模型：

| 模型                 | 显式缓存 | 隐式缓存 |
| ------------------ | ---- | ---- |
| qwen3.6-plus（当前默认） | 支持   | 不支持  |
| qwen3.6-flash      | 支持   | 不支持  |
| qwen3.7-plus       | 支持   | 支持   |

<Warning>
  当前默认模型 qwen3.6-plus **不支持隐式缓存**：不传 `enable_cache_control` 时没有任何缓存生效，`cached_tokens` 会一直为 0，这是模型侧能力限制而非接口异常。要获得缓存收益（降本 + 降首包延迟）需开启显式缓存；仅当服务运行在 qwen3.7-plus 上时，不开启才有隐式缓存兜底。模型支持列表以[显式缓存官方文档](https://help.aliyun.com/zh/model-studio/explicit-cache-guide)的“支持的模型”章节为准。
</Warning>

### 适用场景 <span id="section-context-cache-scenarios" />

缓存收益分两类场景，**单请求内的收益比跨请求更稳定、更显著**。

**强烈推荐：单请求内多次检索与思考**。一次提问触发多轮工具调用（多次 `semantic_search` / `execute_sql` / 章节浏览 + 中间思考）时，缓存断点会随每轮新增的工具结果向后移动，上一轮的完整上下文（含检索结果）成为下一轮的稳定前缀。问题越复杂、检索轮次越多、检索结果越长，节省越明显，且不受 5 分钟有效期和跨请求前缀变化的影响。

**推荐：跨请求复用**：

| 场景                       | 原因                                             |
| ------------------------ | ---------------------------------------------- |
| 多轮连续对话                   | 从第二轮起可命中上一轮的完整历史前缀，对话越长收益越大                    |
| 长 System Prompt / 大表结构场景 | 结构化知识库的表结构会拼进 system prompt，体积大且会话内稳定，是理想的缓存内容 |
| 同一份长文档 / 会话文件的反复追问       | 稳定前缀占比高，一次创建多次命中                               |
| 对延迟敏感、要求命中确定性的业务         | 显式缓存是确定性命中，不受后端资源调度影响                          |

成本盈亏判断：显式缓存首次写入多付 25%，之后每次命中省 90%，**只要发生至少一次命中，总成本就低于不用缓存**。

**不建议开启（开了也没收益，甚至更贵）**：

| 场景                  | 原因                                            |
| ------------------- | --------------------------------------------- |
| 一次性单轮短问答            | 只创建、不命中，白付 25% 创建溢价                           |
| 前缀不足 1024 Token     | 达不到最小缓存长度，不会产生缓存                              |
| 请求间隔超过 5 分钟的低频调用    | 显式缓存 5 分钟过期，下次请求必然重新创建                        |
| 多模态文档场景的跨请求复用       | 图片内容块参与前缀计算，跨请求容易因图片块变化而缓存失效；此类场景建议只指望单请求内的收益 |
| 每次会话都换知识库 / 换问答服务配置 | 前缀（含表结构、知识库信息）不同，无法复用                         |

<Note>
  上述“不建议”仅指显式缓存收益不划算。由于默认模型 qwen3.6-plus 不支持隐式缓存，不开启 `enable_cache_control` 并不会退化成隐式缓存兜底，而是完全没有缓存。若这些场景仍希望有一定缓存收益，可评估后照常开启。
</Note>

### 查看缓存命中 <span id="section-context-cache-observe" />

响应终结帧 `usage` 中的 `cached_tokens` 即本次请求命中缓存的输入 Token 数：

```json
{
  "usage": {
    "input_tokens": 12345,
    "output_tokens": 678,
    "total_tokens": 13023,
    "cached_tokens": 9800
  }
}
```

- `cached_tokens` 是本次请求内**多轮模型调用的累计值**；
- 会话首次请求 `cached_tokens` 必然为 0：首次只创建缓存，尚无可命中的内容，属预期行为；
- 从第二轮起应观察到 `cached_tokens` 大于 0，且随对话历史增长而增大。

### 注意事项 <span id="section-context-cache-notes" />

1. **两种缓存互斥**：传了 `enable_cache_control: true` 即走显式缓存，不再走隐式缓存。默认模型 qwen3.6-plus 上不存在隐式缓存，不开启就是无缓存，而非“退化成隐式”。
2. **首轮无命中是正常的**：任何缓存首次都需要先创建，`cached_tokens=0` 不代表功能异常。
3. **显式缓存 5 分钟有效期**：每次命中会重置为 5 分钟；用户长时间不发问，下一次请求会重新创建缓存。
4. **工具定义变化会导致缓存失效**：工具定义作为 system prompt 的一部分参与缓存计算。结构化知识库携带 `search_filters` 时会屏蔽 `execute_sql` 工具——同一会话中 filter 时有时无会改变工具列表，进而导致 system 前缀无法命中。建议同一会话内保持 `kb_search_configs` 稳定。
5. **前缀必须逐字节稳定**：修改知识库绑定、变更问答服务配置（如自定义指令、会话文件开关）、平台侧调整模型，都会改变前缀，缓存需重新创建。
6. **多模态场景跨请求命中不稳定**：带图片输入时图片内容块参与前缀计算，跨请求前缀容易不一致导致缓存失效；这类场景的收益主要来自单请求内的多轮检索，不应按跨请求命中来估算。
7. **超长多轮存在回溯上限**：缓存匹配从断点向前回溯最多 20 个 content 块，单轮产生大量消息（多次工具调用 + 图片块）的极端情况下可能超出回溯范围而无法命中。
8. **缓存按账号与模型隔离**：不同账号之间、同账号不同模型之间的缓存均不共享。
9. **`input_tokens` ≠ 创建 + 命中**：后端会在提示词之后追加少量 Token（通常 10 以内），计入总输入但不计入缓存统计。

### 验证缓存效果 <span id="section-context-cache-verify" />

1. 在请求中加上 `enable_cache_control: true`，用一个会触发多轮检索的复杂问题跑一次，观察终结帧 `cached_tokens` 是否大于 0；
2. 再用同一会话连续追问 2-3 轮（间隔控制在 5 分钟内），观察 `cached_tokens` 是否随历史增长而增大，并对比首包延迟；
3. 与不传该参数的同样请求做 A/B 对比（默认模型下基线应为 `cached_tokens=0`），按「适用场景」的判据决定是否常态开启。

## 错误码 <span id="section-error-codes-title" />

SSE 流式响应中，错误以 `event: error` 帧返回（`data:` 后为错误 JSON，含 `code` / `message` / `request_id`）。鉴权失败在 HTTP 层返回。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "12%" }} /><col style={{ width: "28%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>HTTP 状态码</p></th><th style={{ verticalAlign: "top" }}><p>错误码</p></th><th style={{ verticalAlign: "top" }}><p>说明</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>401</p></td><td style={{ verticalAlign: "top" }}><p>InvalidApiKey</p></td><td style={{ verticalAlign: "top" }}><p>鉴权失败，API Key 无效或缺失。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>500</p></td><td style={{ verticalAlign: "top" }}><p>AgentApp.NotFound</p></td><td style={{ verticalAlign: "top" }}><p>Agent 不存在或未发布等服务端错误，以 SSE error 帧返回。</p></td></tr></tbody></table>

## 示例 <span id="section-examples-title" />

以下示例向已发布的知识问答服务提问「什么是百炼知识库？」，`agent_id` 替换为实际问答服务 ID。

### cURL <span id="section-examples-curl" />

```bash
curl -X POST "https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/knowledge/chat" \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -H "Accept: text/event-stream" \
  -d '{
    "input": {
      "messages": [
        {"role": "user", "content": [{"type": "text", "text": "什么是百炼知识库？"}]}
      ]
    },
    "parameters": {
      "agent_options": {
        "agent_id": "aid-xxxxxxxxxxxxxxxx"
      }
    },
    "stream": true
  }'
```

### Python <span id="section-examples-python" />

```python expandable
import os
import requests

resp = requests.post(
    "https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/knowledge/chat",
    headers={
        "Authorization": f"Bearer {os.getenv('DASHSCOPE_API_KEY')}",
        "Content-Type": "application/json",
        "Accept": "text/event-stream",
    },
    json={
        "input": {
            "messages": [
                {"role": "user", "content": [{"type": "text", "text": "什么是百炼知识库？"}]}
            ]
        },
        "parameters": {
            "agent_options": {
                "agent_id": "aid-xxxxxxxxxxxxxxxx"
            }
        },
        "stream": True,
    },
    stream=True,
)
for line in resp.iter_lines():
    if line:
        print(line.decode())
```
