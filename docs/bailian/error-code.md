> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 错误码

> 本文介绍使用阿里云百炼服务可能出现的错误信息及解决方案。

<meta property="og:title" content="错误码" />

## 使用阿里云 AI 助理 <span id="14aad410332ik" />

推荐使用[阿里云 AI 助理](https://www.aliyun.com/ai-assistant/)，其知识库整合了阿里云官方帮助文档，输入错误信息即可获取解决方案。

示例问题：

```plaintext
报错信息：'code': 'Arrearage', 'param': None, 'message': 'Access denied, please make sure your account is in good standing.', 'type': 'Arrearage'}帮我看下什么原因
```

AI 助理准确分析出原因，并给出解决方案：

分析结果表明账号因欠费导致访问被拒绝，可能原因包括账号欠费、余额不足或充值延迟。建议登录阿里云控制台进入<strong>费用与成本</strong>页面检查账户状态，完成充值后等待几分钟让系统更新即可恢复正常访问。

## 如何获取 Request ID？ <span id="request-id-faq-title" />

Request ID 格式为 UUID（例如 `649b2bbc-c541-9e16-9845-db7fe4fe5b2d`），包含在 API 响应的 Header 或 Body 中，建议在调用失败时记录该值，便于自助排查或提交工单时提供。

若未记录 Request ID，可登录<strong>模型监控</strong>页面，在目标模型的<strong>日志</strong>页签查询调用记录（含 Request ID、状态码、错误码、请求和响应内容等）。

前置条件：

- 需由主账号或已授权的子账号先开通推理日志。
- 仅支持华北2（北京）、新加坡、美国（弗吉尼亚）地域的部分模型。
- 数据存在分钟级延迟，开通前的历史调用记录不可追溯。

详情请参见[模型监控](/zh/model-studio/model-telemetry)文档。

## 400-InvalidParameter <span id="9270b004a1e1p" />

### parameter.enable\_thinking must be set to false for non-streaming calls/ parameter.enable\_thinking only support stream call <span id="enable-thinking-only-for-streaming" />

<strong>原因：</strong> 使用非流式输出方式调用了思考模式模型。

<strong>解决方案：</strong>请将`enable_thinking`参数设置为`false`，或者改用[流式输出](/zh/model-studio/stream)方式调用思考模式模型。

### The thinking\_budget parameter must be a positive integer and not greater than xxx <span id="8626bba5afbgz" />

<strong>原因：</strong> `thinking_budget` 参数不在可选值范围内。

<strong>解决方案：</strong> 请参见模型列表中模型的最大思维链长度，指定为大于0且不超过该长度的值。

### This model only support stream mode, please enable the stream parameter to access the model. / current user api does not support http call. <span id="stream-only-model" />

<strong>原因：</strong> 模型仅支持[流式输出](/zh/model-studio/stream)，但调用时未启用流式输出。

<strong>解决方案：</strong> 请使用[流式输出](/zh/model-studio/stream)方式调用模型。

### This model does not support enable\_search. <span id="d86ef92f928in" />

<strong>原因：</strong> 当前模型不支持[联网搜索](/zh/model-studio/web-search)能力，但指定了`enable_search`参数为`true`。

<strong>解决方案：</strong> 请调用支持联网搜索能力的模型。

### 暂时不支持当前设置的语种！ <span id="fc465a0e45f4a" />

<strong>原因：</strong> 使用 Qwen-MT 模型时，传入的 `source_lang` 或 `target_lang` 格式错误，或不在[支持的语言](/zh/model-studio/machine-translation#038d2865bbydc)里。

<strong>解决方案：</strong> 请传入正确格式的英文名或语种编码。

### The incremental\_output parameter must be "true" when enable\_thinking is true <span id="cabf71f55e154" />

<strong>原因：</strong> 模型开启思考模式时仅支持增量流式输出，未将`incremental_output`参数设置为`true`。

<strong>解决方案：</strong> 请将`incremental_output`参数设置为`true`再调用，API将返回增量内容。

### The incremental\_output parameter of this model cannot be set to False. <span id="bad5900569uaz" />

<strong>原因：</strong> 模型仅支持增量流式输出，未将`incremental_output`参数设置为`true`。

<strong>解决方案：</strong> 请将`incremental_output`参数设置为`true`再调用，API将返回增量内容。

### Range of input length should be \[1, xxx]（InternalError.Algo.InvalidParameter） <span id="81221bf732m4b" />

<strong>原因：</strong> 调用模型时输入内容长度超过模型上限。

<strong>解决方案：</strong>

- 若通过代码调用，请控制 messages 数组中的 Token 数在模型最大输入Token范围内；
- 使用对话客户端（如Chatbox）或阿里云百炼控制台进行连续对话时，每次请求都会附带历史记录，容易超出模型限制。超出限制后，请开启新对话。

### Range of max\_tokens should be \[1, xxx] <span id="cc5c90b6db061" />

<strong>原因：</strong> `max_tokens` 参数设置未在 \[1, 模型最大输出 Token 数]的范围内。

<strong>解决方案：</strong> `max_tokens`上限请参考模型列表文档中的"最大输出 Token 数"。

### Temperature should be in \[0.0, 2.0)/'temperature' must be Float <span id="00b2e620e2k3l" />

<strong>原因：</strong> temperature参数设置不在\[0.0, 2.0)范围。

<strong>解决方案：</strong> 将temperature参数设置为大于等于0，小于2的数字。

### Range of top\_p should be (0.0, 1.0]/'top\_p' must be Float <span id="6ad42985bchwz" />

<strong>原因：</strong> top\_p参数设置不在`(0.0, 1.0]`范围。

<strong>解决方案：</strong> 将`top_p`参数设置为大于0，小于等于1的数字。

### Parameter top\_k be greater than or equal to 0 <span id="a7daaec3ee60a" />

<strong>原因：</strong> `top_k`参数设置为小于0的数字。

<strong>解决方案：</strong> 将`top_k`参数设置为大于等于0的数字。

### Repetition\_penalty should be greater than 0.0 <span id="19c77b8f69k4j" />

<strong>原因：</strong> `repetition_penalty`参数设置为小于等于0的数字。

<strong>解决方案：</strong> 将`repetition_penalty`参数设置为大于0的数字。

### Presence\_penalty should be in \[-2.0, 2.0] <span id="d2f56c0b9a5hl" />

<strong>原因：</strong> `presence_penalty`参数不在`[-2.0,2.0]`区间。

<strong>解决方案：</strong> 将`presence_penalty`参数设置在`[-2.0,2.0]`区间。

### Range of n should be \[1, 4] <span id="e7b6b3d88dqzg" />

<strong>原因：</strong> n 参数设置未在 \[1, 4]的范围内。

<strong>解决方案：</strong> 将 n 参数设置在\[1, 4]范围内。

### Range of seed should be \[0, 9223372036854775807] <span id="9d2c556870a3n" />

<strong>原因：</strong> 使用DashScope协议时，`seed` 参数设置未在 \[0, 9223372036854775807]的范围内。

<strong>解决方案：</strong> 将`seed`参数设置在 \[0, 9223372036854775807]的范围内。

### Request method 'GET' is not supported. <span id="cfc43a9cb3nq4" />

<strong>原因：</strong> 当前接口不支持 `GET` 请求方法。

<strong>解决方案：</strong> 请查阅接口文档，使用该接口支持的请求方法（如 `POST` 等）重新发起请求。

### messages with role "tool" must be a response to a preceeding message with "tool\_calls" <span id="a938d59de521i" />

<strong>原因：</strong> 在工具调用时没有向 messages 数组添加 Assistant Message。

<strong>解决方案：</strong> 请将模型第一轮响应的 Assistant Message 添加到 messages 数组后再添加 Tool Message。

### An assistant message with "tool\_calls" must be followed by tool messages responding to each "tool\_call\_id" <span id="assistant-tool-calls-followed-by-tool" />

<strong>原因：</strong> assistant 消息包含 tool\_calls 时，后续必须跟随 tool 角色消息来响应每个 tool\_call\_id。常见于使用工具调用百炼 API 时消息序列格式不正确，例如在带 tool\_calls 的 assistant 消息之后直接追加 user 消息。

<strong>解决方案：</strong> 在带 tool\_calls 的 assistant 消息之后、发送下一个 user 消息之前，插入对应的 tool 角色消息（每条包含匹配的 tool\_call\_id 和该工具的执行结果）。assistant 消息中的每个 tool\_call\_id 都必须有且仅有一条 tool 消息与之对应。

### Required body invalid, please check the request body format. <span id="f56634b51dv6e" />

<strong>原因：</strong> 请求体（body）格式不符合接口要求。

<strong>解决方案：</strong> 请检查请求体，确保为标准的JSON字符串。常见问题有：多了`,`、括号未闭合、引号未闭合等。

错误示例（`text` 字段缺少闭合引号，导致请求体不是合法 JSON）：

```json
{"model":"qwen-image-3.0-pro","input":{"messages":[{"role":"user","content":[{"image":"https://example.com/img.png"},{"text":"请将图片背景改为蓝色}]}]}}
```

正确示例：

```json
{"model":"qwen-image-3.0-pro","input":{"messages":[{"role":"user","content":[{"image":"https://example.com/img.png"},{"text":"请将图片背景改为蓝色"}]}]}}
```

提交请求前可使用 jsonlint.com 等 JSON 校验工具检查请求体语法。也可借助阿里云AI助理帮助修复请求体格式。

### input content must be a string. <span id="3117787756d0w" />

<strong>原因：</strong> 纯文本模型不支持将 messages 中的 content 设置为非字符串类型。

<strong>解决方案：</strong> 请勿将content设置为如`[{"type": "text","text": "你是谁？"}]`的数组类型。

### The content field is a required field. <span id="0359e401062qi" />

<strong>原因：</strong> 发起请求时，未指定`content`参数，如`{"role": "user"}`。

<strong>解决方案：</strong> 请指定`content`参数。如`{"role": "user","content": "你是谁"}`。

### Either "prompt" or "messages" must exist and cannot both be none <span id="199fba0265wum" />

<strong>原因：</strong> 调用大模型时，既未指定`messages`参数，也未指定`prompt`参数（即将废弃）。如果指定了`messages`参数后报错，可能是因为格式错误，例如通过DashScope-HTTP时，`messages`需放入`input`对象中，而不是与`model`参数并列。

<strong>解决方案：</strong> 请指定`messages`参数。如果已指定但仍报错，请参见[文本生成](/zh/model-studio/qwen-api-reference)API文档，检查其位置是否正确。

### 'messages' must contain the word 'json' in some form, to use 'response\_format' of type 'json\_object'. <span id="4b2b7bcaefr8i" />

<strong>原因：</strong> 使用[结构化输出](/zh/model-studio/qwen-structured-output)时，提示词中不包含 `json` 关键词。

<strong>解决方案：</strong> 在提示词中加入`json`（不区分大小写），如：“请以json格式输出”。

### Json mode response is not supported when enable\_thinking is true <span id="ccffa8d337loe" />

<strong>原因：</strong> 使用[结构化输出](/zh/model-studio/qwen-structured-output)时开启了模型的思考模式。

<strong>解决方案：</strong> 请在使用结构化输出时，将`enable_thinking`设为`false`关闭思考模式。也可参见常见问题[思考模式模型如何结构化输出？](/zh/model-studio/qwen-structured-output#2f7ba877d3hle)

### Tool names are not allowed to be \[search] <span id="04d5652789r0r" />

<strong>原因：</strong> 工具名称无法设置为`search`。

<strong>解决方案：</strong> 工具名称请设置为`search`之外的值。

### Unknown format of response\_format, response\_format should be a dict, includes 'type' and an optional key 'json\_schema'. The response\_format type from user is xxx. <span id="d08defd0e7kbk" />

<strong>原因：</strong> 指定的`response_format`参数不符合规定。

<strong>解决方案：</strong> 如需使用[结构化输出](/zh/model-studio/qwen-structured-output)功能，请将`response_format`参数设置为`{"type": "json_object"}`。

### The value of the enable\_thinking parameter is restricted to True. <span id="9c16fdf75fnn1" />

<strong>原因：</strong> 部分模型（如`qwen3.7-plus`）不可将`enable_thinking`参数设为 `false`。

<strong>解决方案：</strong>

- 若通过第三方工具调用（如 Cherry Studio），请打开输入框的思考开关。
- 若通过代码调用，请将`enable_thinking`设为`true`。

### 'audio' output only support with stream=true <span id="913e991f8ccxl" />

<strong>原因：</strong> 请求输出音频，但未开启流式输出。

<strong>解决方案：</strong> 设置`stream`参数为`true`以启用流式输出。

### tool\_choice is one of the strings that should be \["none", "auto"] <span id="5f69577806hre" />

<strong>原因：</strong> 发起 Function Calling 时指定的 `tool_choice` 参数有误。

<strong>解决方案：</strong> 请指定为 "auto"（由大模型自主选择工具）或 "none"（强制不使用工具）。

### Model not exist. <span id="281b1fd57cuwp" />

<strong>原因：</strong> 设置的`model`参数不存在或格式不正确。

<strong>解决方案：</strong>

- <strong>检查模型名称格式：</strong>确认`model`参数大小写是否正确，是否存在多余的空格。
- <strong>使用正确的模型名称：</strong>请对照[模型列表](/zh/model-studio/models)中的模型名称，检查输入的`model`是否正确。请勿混用开源社区的模型名与百炼模型ID，如应该使用`qwen3.7-plus`，而非`Qwen/Qwen3.7-Plus`。

### The product is not activated, please confirm that you have activated products and try again after activation. <span id="product-not-activated-h3" />

<strong>原因：</strong> 所调用的模型服务未开通（未激活）。通过 OpenAI 兼容接口调用时，若目标模型未在模型市场开通，网关会返回该错误，错误码为 `invalid_parameter_error`。

<strong>解决方案：</strong>

- <strong>开通模型服务：</strong>请前往阿里云百炼控制台[模型市场](https://bailian.console.aliyun.com/cn-beijing/model/market)，确认所调用的模型（如 `kimi-k3`）处于已开通状态后再行调用。
- <strong>开通百炼服务：</strong>若尚未开通阿里云百炼服务，请先开通百炼服务后再调用。

### The result\_format parameter must be "message" when enable\_thinking is true <span id="03c89b9446uke" />

<strong>原因：</strong> 调用思考模式模型，`result_format`参数未设置为`"message"`。

<strong>解决方案：</strong> 将`result_format`参数设置为`"message"`。

### The audio is empty <span id="16cccdf4c2hxn" />

<strong>原因：</strong> 输入音频时间过短，导致采样点不足。

<strong>解决方案：</strong> 请增加音频的时间。

### File parsing in progress, please try again later. <span id="8ebe1b1599e7l" />

<strong>原因</strong>：使用 Qwen-Long 模型时，文件未完成解析。

<strong>解决方案</strong>：请等待文件解析完成后再重试。

### The "stop" parameter must be of type "str", "list\[str]", "list\[int]", or "list\[list\[int]]", and all elements within the list must be of the same type. <span id="ef8ea2018dmcj" />

<strong>原因：</strong> `stop` 参数不符合`str`, `list[str]`, `list[int],` 或`list[list[int]]`格式。

<strong>解决方案：</strong> 参见[文本生成](/zh/model-studio/qwen-api-reference)API 文档，设置正确格式的`stop` 参数。

### Value error, batch size is invalid, it should not be larger than xxx. <span id="a6c6e3b337tyh" />

<strong>原因：</strong> 调用 Embedding 模型时，文本数量超过模型上限。

<strong>解决方案：</strong> 参考[Embedding](/zh/model-studio/embedding)文档中模型的<strong>批次大小</strong>信息，控制传入文本的数量。

### Invalid file \[id:file-fe-\*\*\*\*\*\*\*\*\*\*]. <span id="d34b1f4f88ev2" />

<strong>原因</strong>：提供的 file-id 无效。例如输入错误、使用了不属于当前阿里云账号的 file-id。

<strong>解决方案</strong>：通过[OpenAI兼容-File](/zh/model-studio/openai-file-interface)确认file-id是否有效，或重新[OpenAI兼容-File](/zh/model-studio/openai-file-interface)来获取新的file\_id后进行调用。

### \[] is too short <span id="431ed59dd7buy" />

<strong>原因：</strong> 输入的messages为空数组。

<strong>解决方案：</strong> 请添加 message 后再发起请求。

### The tool call is not supported. <span id="0a08c660173rp" />

<strong>原因：</strong> 使用的模型不支持传入tools参数。

<strong>解决方案：</strong> 请更换为支持Function Calling的Qwen或DeepSeek模型。

### Repetitive tool calls detected in the conversation history. The same tool call with identical name and arguments has been repeated across multiple consecutive rounds. Please modify your request or adjust the tool call arguments to avoid infinite loops. <span id="ie-algo-ip-rtc-h3" />

<strong>原因：</strong> 在对话历史中检测到重复的工具调用：名称和参数完全相同的工具调用在多个连续轮次中重复出现，模型可能已陷入循环。该错误对应的 HTTP 状态码为 400，错误码为`InternalError.Algo.InvalidParameter`。

<strong>解决方案：</strong>

- 在每轮工具调用后，将工具的执行结果以 Tool Message 形式追加到`messages`数组中，再发起下一轮请求，便于模型据此推进而非重复发起相同调用。
- 检查工具是否正常返回结果；若工具持续返回相同或无效结果，建议在应用侧增加工具调用次数上限或终止逻辑，避免无限循环。
- 必要时优化提示词，明确任务完成的条件，避免模型反复发起同一工具调用。

### The provided messages input is invalid. The error info is \[Unexpected item type in content] / Input should be a valid string / Input should be a valid dictionary or instance of ... (input.messages.x.content...) <span id="ie-algo-ip-uit-h3" />

<strong>原因：</strong> `messages` 数组中某条消息的 `content` 字段包含了不受支持的元素类型。当 `content` 为数组时，其中每个元素必须是字符串，或符合规范的对象（如 `{"type": "text", "text": "..."}`、`{"type": "image_url", ...}` 等）；若数组中混入了数字、布尔值、嵌套数组，或 `type` 取值不受支持的对象，则会触发该错误。该错误对应的 HTTP 状态码为 400，错误码为 `InternalError.Algo.InvalidParameter`。

<strong>常见场景：</strong>使用纯文本模型（如 Qwen-Max 文本系列，包括 `qwen3.7-max`）时，若 `messages`（尤其是多轮对话历史）中包含图片（`image_url`）等多模态 `content` 元素时，由于纯文本模型不支持图片等模态输入，同样会触发该错误（在部分接入工具/智能体中，该报错可能被转述为「模型服务商返回空内容」等提示）。

<strong>解决方案：</strong>

- <strong>纯文本模型：</strong>请将 `content` 设置为字符串，不要传入数组或其他类型。若业务需要图片等多模态输入，请改用多模态模型（如 Qwen-VL、Qwen3-VL 系列）；若需继续使用纯文本模型，请在调用前从 `messages` 及对话历史中移除图片（`image_url`）等多模态元素。
- <strong>多模态模型：</strong>`content` 数组中的每个元素都需为合法对象，`type` 仅支持 `text`、`image_url`、`video_url`、`video` 等模型支持的模态类型，请勿混入数字、布尔值、嵌套数组，或缺少 `type`、`type` 取值非法的元素。

### Required parameter(xxx) missing or invalid, please check the request parameters. <span id="1ad29d0a9bu04" />

<strong>原因：</strong> 接口调用参数不合法。

<strong>解决方案：</strong> 请检查请求参数，确保所有必需参数都已提供且格式正确。

当报错信息中参数名称为`data_sources`时，通常是因为调用 CreateIndex 接口时未指定必传参数`SourceType`导致后续 SubmitIndexJob 接口报错。基于给定文档创建知识库时，此参数需传入`DATA_CENTER_FILE`；基于给定类目创建知识库时，此参数需传入`DATA_CENTER_CATEGORY`。详见[CreateIndex](/zh/model-studio/api-bailian-2023-12-29-createindex)文档。

### input must contain file\_urls <span id="27d6c2a728jia" />

<strong>原因：</strong> 使用语音识别（Paraformer）的录音文件识别时，未对请求参数`file_urls`赋值。

<strong>解决方案：</strong> 请在请求中包含`file_urls`参数并为其赋值。

### The provided URL does not appear to be valid. Ensure it is correctly formatted. <span id="6136acf455ur2" />

<strong>原因：</strong> 当使用视觉理解、全模态或音频理解模型时，传入数据的 URL 或本地路径无效或不符合要求。

<strong>解决方案：</strong>

- <strong>传入 URL</strong> ：需要以 `http://`、 `https://`、`data:`开头。若以`data:`开头, 在 Base64 编码数据前需要包含`"base64"`。
- <strong>传入本地路径</strong>：需要以`file://`开头。
- <strong>传入临时URL</strong>：

  - 通过 HTTP 调用，需确保请求的 Header 中添加了参数 `X-DashScope-OssResourceResolve: enable`。
  - 通过 SDK 调用：仅支持 DashScope SDK调用，请勿使用 OpenAI SDK。

### Input should be a valid dictionary or instance of GPT3Message <span id="ce18c49a88v5c" />

<strong>原因：</strong> messages 字段的构造格式不符合要求，例如括号数量不匹配、缺少必要的键值对等。

<strong>解决方案：</strong> 请检查`messages`字段的JSON结构是否正确。

### Value error, contents is neither str nor list of str. : input.contents <span id="7e89870921vrc" />

<strong>原因：</strong> 使用 Embedding 模型时，输入不是字符串也不是字符串数组。

<strong>解决方案：</strong> 请修改输入格式为字符串或字符串列表。

### No backend server available <span id="no-backend-server-available" />

<strong>原因：</strong> 百炼平台后端服务暂时不可用，可能由服务更新、扩容、资源调度或临时故障导致。

<strong>解决方案：</strong>

1. 前往阿里云控制台的[费用与成本](https://usercenter2.aliyun.com/home)页面，确认账号不存在欠费。已欠费请及时充值，充值后等待几分钟生效。
2. 使用 `curl` 或 `ping` 测试本地网络到 `dashscope.aliyuncs.com` 的连通性，排除网络或防火墙拦截。
3. 登录阿里云百炼控制台，查看所调用服务的状态是否正常。
4. 确认账号与网络均正常后，等待几分钟重新调用 API。

### File \[id:file-fe-xxx] format is not supported. <span id="0ab4677a2fcii" />

<strong>原因：</strong> Qwen-Long模型仅限于处理纯文本格式文件（TXT、DOCX、PDF、EPUB、MOBI、MD），不支持图片或扫描文档。

<strong>解决方案：</strong> 如需对图片内容进行文本提取、分析和总结，可使用千问VL模型。

### File \[id:file-fe-\*\*\*\*\*\*\*\*\*\*] cannot be found. <span id="2903f3bb13wai" />

<strong>原因：</strong> 仅在Qwen-Long模型的对话场景中，在发起对话请求后的极短时间内调用OpenAI文件兼容接口删除相关文件时才会出现。

<strong>解决方案：</strong> 请等待模型完成对话后再删除相关文件。

### Too many files provided. <span id="6dd16086bckk1" />

<strong>原因：</strong> 提供的file-id数量超限。

<strong>解决方案：</strong> 请确保file-id数量小于100。

### File \[id:file-fe-\*\*\*\*\*\*\*\*\*\*] exceeds size limit. <span id="02821fafe6pl4" />

<strong>原因：</strong>文件大小超出限制。

<strong>解决方案：</strong> 确保文件小于150 MB。

### File \[id:file-fe-\*\*\*\*\*\*\*\*\*\*] exceeds page limits (15000 pages). <span id="3a432b04d5xbx" />

<strong>原因：</strong> 文件页数超出限制。

<strong>解决方案：</strong> 确保文件页数少于15000页。

### File \[id:file-fe-\*\*\*\*\*\*\*\*\*\*] content blank. <span id="437b8b12bd4n5" />

<strong>原因：</strong> 文件内容为空。

<strong>解决方案：</strong> 确保文件内容不为空。

### Total message token length exceed model limit (10000000 tokens). <span id="a81cc451ab2hm" />

<strong>原因：</strong> 输入总长度超过了10,000,000 Token。

<strong>解决方案：</strong> 请确保message长度符合要求。

### The video modality input does not meet the requirements because: the range of sequence images shoule be (4, 512)./(4,80). <span id="7b6863668blat" />

<strong>原因：</strong> 使用千问 VL 模型以图像列表方式输入视频时，图像数量不符合要求。

<strong>解决方案：</strong> Qwen3-VL与Qwen2.5-VL系列模型需传入4-512张图片；其他模型需传入4-80张图片。详情可参见[图像与视频理解](/zh/model-studio/vision)。

### Exceeded limit on max bytes per data-uri item : 10485760'. / Multimodal file size is too large <span id="82de1c8dc2mdr" />

<strong>原因：</strong> 向多模态模型（Qwen-VL、QVQ、Qwen-Omni）传入的本地图像或视频超出大小限制。

<strong>解决方案：</strong>

- <strong>本地文件</strong>：Base64 编码后单个文件不得超过 10 MB。
- <strong>文件 URL</strong>：图像文件不得超过 10 MB；对于视频文件，

  - Qwen3-VL、qwen3-vl-max：不超过 2GB；
  - qwen3-vl-plus系列：不超过 1GB；
  - 其他模型不超过 150MB。

> 压缩文件体积请参见[如何将图像或视频压缩到满足要求的大小？](/zh/model-studio/vision)

### Input should be 'Cherry', 'Serena', 'Ethan' or 'Chelsie': parameters.audio.voice <span id="b976cdf3760zy" />

<strong>原因：</strong> 使用Qwen-Omni或Qwen-TTS 时`voice`参数指定错误。

<strong>解决方案：</strong> 请指定为'Cherry', 'Serena', 'Ethan' 或 'Chelsie'中的一个。

### The image length and width do not meet the model restrictions. <span id="8f428a371bom3" />

<strong>原因：</strong> 传入千问VL模型的图像尺寸（长度和宽度）不符合模型的要求。

<strong>解决方案：</strong> 图像尺寸需满足以下要求：宽度和高度均不小于10像素，且宽高比不应超过200:1或1:200。

### Failed to decode the image during the data inspection. <span id="1d27ef1d1byyp" />

<strong>原因：</strong> 图像解码失败。

<strong>解决方案：</strong> 请确认图像是否有损坏，以及图像格式是否符合要求。

### The file format is illegal and cannot be opened. / The audio format is illegal and cannot be opened. / The media format is not supported or incorrect for the data inspection. <span id="56bec83894di6" />

<strong>原因：</strong> 无法支持的文件格式或文件无法打开。

<strong>解决方案：</strong> 请确认文件是否损坏、文件扩展名和实际格式是否匹配、文件格式是否支持。

### The input messages do not contain elements with the role of user. <span id="a7763649f7vn6" />

<strong>原因：</strong>

- 调用模型时，未向模型传入 User Message；
- 或API调用阿里云百炼工作流应用时，在开始节点中传入的参数，需通过`biz_params`参数传递（而非`user_prompt_params`）。

<strong>解决方案：</strong> 请确保向模型传入User Message，或正确传递自定义参数。

### Failed to download multimodal content. / Download the media resource timed out during the data inspection process. / Unable to download the media resource during the data inspection process. <span id="download_error" />

<strong>原因：</strong>服务端无法下载公网 URL 指向的媒体文件，可能由以下原因导致。

- <strong>连通性问题：</strong> 使用了[阿里云对象存储服务](https://www.aliyun.com/product/oss)内网地址。
- <strong>网络延迟：</strong> 跨地域访问引发超时。
- <strong>服务不稳定：</strong> 源存储服务响应慢或不可达。
- <strong>安全策略拦截：</strong> 服务端下载公网 URL 时携带的 User-Agent 包含 `DashScopeUserBot` 标识，第三方服务器的安全策略可能拦截该标识导致下载失败。

<strong>解决方案：</strong>

- <strong>更换存储服务</strong>

  建议使用与模型服务同地域的存储服务。推荐使用[阿里云对象存储服务](https://www.aliyun.com/product/oss)生成公网链接（请勿使用内网地址）。
- <strong>调整传输方式</strong>

  若传入公网 URL 方式失败，可参考[传入本地文件（Base64 编码或文件路径）](/zh/model-studio/vision#d987f8de5395x)切换为推荐的传入方式：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "10.57%" }} /><col style={{ width: "20.78%" }} /><col style={{ width: "30.76%" }} /><col style={{ width: "37.89%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>文件类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>文件规格</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>DashScope SDK（Python、Java）</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>OpenAI 兼容 / DashScope HTTP</strong></p></th></tr></thead><tbody><tr><td rowSpan={2} style={{ verticalAlign: "top" }}><p>图像</p></td><td style={{ verticalAlign: "top" }}><p>大于 7MB 小于 10MB</p></td><td style={{ verticalAlign: "top" }}><p>传入本地路径</p></td><td style={{ verticalAlign: "top" }}><p>仅支持公网 URL，建议使用<a href="https://www.aliyun.com/product/oss">阿里云对象存储服务</a></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>小于 7MB</p></td><td style={{ verticalAlign: "top" }}><p>传入本地路径</p></td><td style={{ verticalAlign: "top" }}><p>Base64 编码</p></td></tr><tr><td rowSpan={3} style={{ verticalAlign: "top" }}><p>视频</p></td><td style={{ verticalAlign: "top" }}><p>大于 100 MB</p></td><td style={{ verticalAlign: "top" }}><p>仅支持公网 URL，建议使用<a href="https://www.aliyun.com/product/oss">阿里云对象存储服务</a></p></td><td style={{ verticalAlign: "top" }}><p>仅支持公网 URL，建议使用<a href="https://www.aliyun.com/product/oss">阿里云对象存储服务</a></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>大于 7MB 小于 100 MB</p></td><td style={{ verticalAlign: "top" }}><p>传入本地路径</p></td><td style={{ verticalAlign: "top" }}><p>仅支持公网 URL，建议使用<a href="https://www.aliyun.com/product/oss">阿里云对象存储服务</a></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>小于 7MB</p></td><td style={{ verticalAlign: "top" }}><p>传入本地路径</p></td><td style={{ verticalAlign: "top" }}><p>Base64 编码</p></td></tr><tr><td rowSpan={3} style={{ verticalAlign: "top" }}><p>音频</p></td><td style={{ verticalAlign: "top" }}><p>大于 10 MB</p></td><td style={{ verticalAlign: "top" }}><p>仅支持公网 URL，建议使用<a href="https://www.aliyun.com/product/oss">阿里云对象存储服务</a></p></td><td style={{ verticalAlign: "top" }}><p>仅支持公网 URL，建议使用<a href="https://www.aliyun.com/product/oss">阿里云对象存储服务</a></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>大于 7MB 小于 10 MB</p></td><td style={{ verticalAlign: "top" }}><p>传入本地路径</p></td><td style={{ verticalAlign: "top" }}><p>仅支持公网 URL，建议使用<a href="https://www.aliyun.com/product/oss">阿里云对象存储服务</a></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>小于 7MB</p></td><td style={{ verticalAlign: "top" }}><p>传入本地路径</p></td><td style={{ verticalAlign: "top" }}><p>Base64 编码</p></td></tr></tbody></table>

  > Base64 编码会增大数据体积，原始文件大小应小于 7 MB。

  > 使用 Base64 或本地路径可避免服务端下载超时，提升稳定性。
- <strong>放行</strong>`DashScopeUserBot`

  若图片托管在您控制的服务器上，请在安全策略中放行 User-Agent 包含 `DashScopeUserBot` 的请求。也可改用[传入本地文件（Base64 编码或文件路径）](/zh/model-studio/vision#d987f8de5395x)的方式，避免服务端下载公网 URL。

### Failed to find the requested media resource during the data inspection process. <span id="6fccfc0574up1" />

<strong>原因：</strong>传入的资源URL无效或无法访问。

<strong>解决方案：</strong>

- 确认URL格式正确且可访问
- 确认资源文件未被删除或移动
- 确认URL未过期（如使用OSS签名URL，需检查有效期）

### url error, please check url！ <span id="error_url" />

- <strong>原因一： 模型名称与API 端点不匹配</strong>：例如调用纯文本模型时使用了多模态接口，或者调用多模态模型时使用了纯文本接口。

  <strong>解决方案：</strong>

  1. 通过DashScope使用qwen3.7-plus、qwen3-vl-plus、qwen3.8-max、qwen3.8-flash等<strong>多模态模型</strong>时：需使用MultiModalConversation.call() 或multimodal-generation端点，具体参考[图像与视频理解](/zh/model-studio/vision)。从纯文本模型（如qwen3.7-max）切换为多模态模型（如qwen3.8-max）时，需将调用方式从Generation.call() 改为MultiModalConversation.call()，否则会返回url error。

     > 若使用 spring-ai-alibaba 框架，请确认是否设置多模态参数[withMultiModel](https://github.com/spring-ai-alibaba/examples/blob/c66ffdec789defe4adf86b34bac0084df3b71e92/spring-ai-alibaba-multi-model-example/dashscope-multi-model/src/main/java/com/alibaba/cloud/ai/example/multi/controller/MultiModelController.java#L82)。
  2. 通过DashScope使用qwen3.7-max、qwen-plus、deepseek-v4-pro等<strong>纯文本模型</strong>时，需使用Generation.call() 或 text-generation 端点，具体参考[概述](/zh/model-studio/text-generation)。
  3. 通过DashScope使用CosyVoice<strong>声音复刻</strong>接口时：该接口包含 `model` 与 `target_model` 参数：需将 `model` 设置为 `voice-enrollment`， `target_model` 设置为具体的 CosyVoice 模型，具体参考[CosyVoice声音复刻/设计API](/zh/model-studio/cosyvoice-clone-design-api)。
- <strong>原因二：DashScope SDK 版本过低：</strong>调用图像/视频生成模型时，旧版SDK无法识别正确的服务端地址；

  <strong>解决方案：</strong>[升级 SDK 版本](/zh/model-studio/install-sdk)

### Don't have authorization to access the media resource during the data inspection process. <span id="3ba5afbd70phx" />

<strong>原因：</strong> 调用模型时，传入的OSS中带签名的文件URL已经过期。

<strong>解决方案：</strong> 请确保在文件URL的有效期内访问该文件。

### The item of content should be a message of a certain modal. <span id="2e18d98a0djz7" />

<strong>原因：</strong> 使用DashScope SDK调用多模态模型时，`content` 数组中每个元素的键必须为以下值之一：`image`、`video`、`audio` 或 `text`。

<strong>解决方案：</strong> 请并使用正确的`content`参数。

### Invalid video file. <span id="43da9d83ef2dz" />

<strong>原因：</strong> 传入的视频文件无效。

<strong>解决方案：</strong> 请检查视频文件是否损坏或格式是否正确。

### The video modality input does not meet the requirements because: The video file is too long. <span id="4789f1ff21nlu" />

<strong>原因：</strong> 传入千问VL模型或者Qwen-Omni 模型的视频时长超过限制。

<strong>解决方案：</strong>

- Qwen2.5-VL模型支持的视频时长应在2秒至10分钟之间。
- 其他千问VL或 Qwen-Omni-Turbo 模型支持的视频时长应在2秒至40秒之间。

其他 Qwen-Omni 型号的视频限制见[单一模态输入](/zh/model-studio/qwen-omni#h2-single-modal-input)。

### Field required: xxx <span id="936d0ca16e2wm" />

<strong>原因：</strong> 缺少入参。

<strong>解决方案：</strong> 请根据错误提示`xxx`补充对应的参数。

### The request is missing required parameters or in a wrong format, please check the parameters that you send. <span id="cb2ec4543be2h" />

<strong>原因：</strong> 缺少入参，或入参格式错误。

<strong>解决方案：</strong> 请检查请求参数是否完整且格式正确。

### Missing training files. <span id="3443400d5aoxp" />

<strong>原因：</strong> 参数错误，缺少参数或者参数格式问题等。

### The style is invalid. <span id="a92e9d7b84avs" />

<strong>原因：</strong> style不在枚举范围内。

<strong>解决方案：</strong> 请检查`style`参数的取值是否正确。

### The style\_level is invalid. <span id="39a57aedebi9f" />

<strong>原因：</strong> style\_level不在枚举范围内。

<strong>解决方案：</strong> 详情参见[EMO 视频生成](/zh/model-studio/emo-api)。

### parameters.video\_ratio must be 9:16 or 3:4. <span id="d147c86a163yl" />

<strong>原因：</strong> video\_ratio 入参只能为 9:16 或 3:4。

<strong>解决方案：</strong> 请修改`video_ratio`参数为 "9:16" 或 "3:4"。

### the xxx parm is invalid! <span id="f78e96731aa4l" />

<strong>原因：</strong> 输入参数超出范围。

<strong>解决方案：</strong> 详情参见[视频风格重绘](/zh/model-studio/video-style-transform-api-reference)。

### input json error. <span id="f74242572fvns" />

<strong>原因：</strong> 输入JSON错误。

<strong>解决方案：</strong> 请检查请求的JSON格式是否正确。

### read image error. <span id="3aa64fea0bu40" />

<strong>原因：</strong> 读取图像失败。

<strong>解决方案：</strong> 请检查图像文件是否损坏或格式是否正确。

### the parameters must conform to the specification: xxx. <span id="d2c213820br34" />

<strong>原因：</strong> 输入参数值超出范围。

<strong>解决方案：</strong> 请根据错误提示`xxx`检查并修正参数值。

### The size of person image and coarse\_image are not the same. <span id="7e3ee4babdgjw" />

<strong>原因：</strong> coarse\_image分辨率和person\_image不一致。

<strong>解决方案：</strong> 请确保`coarse_image`和`person_image`的分辨率一致。

### The request is missing required parameters or the parameters are out of the specified range, please check the parameters that you send. <span id="4801e93aceyx4" />

<strong>原因：</strong> 缺少必要的接口调用参数或参数越界。

<strong>解决方案：</strong> 请检查并修正请求参数。

### image format error <span id="02fbd65b8914g" />

<strong>原因：</strong> 图片格式错误。

<strong>解决方案：</strong> 需要是图片url或者Base64字符串。

### No messages found in input <span id="c44c41e120xr0" />

<strong>原因：</strong> 请求参数中需要有messages字段。

<strong>解决方案：</strong> 详情参见[千问-图像编辑](/zh/model-studio/qwen-image-edit-api)。

### Invalid image format or corrupted file <span id="942bf3bd8bsz2" />

<strong>原因：</strong> 输入图片格式错误或文件损坏。

<strong>解决方案：</strong> 请检查文件是否可正常打开和下载，确保文件完整且格式符合要求。

### download image failed <span id="1356a14c63oq9" />

<strong>原因：</strong> 图片不能下载。

<strong>解决方案：</strong> 请检查文件是否可正常下载。

### messages length only support 1 <span id="913b3852abn4n" />

<strong>原因：</strong> messages数组长度仅支持 1。

<strong>解决方案：</strong> 即只能传入一条对话消息。详情参见[千问-图像编辑](/zh/model-studio/qwen-image-edit-api)。

### content length only support 2 <span id="23cf5fcc0dc3m" />

<strong>原因：</strong> content数组长度仅支持为2。

<strong>解决方案：</strong> 即只能传入一组text和image。详情参见[千问-图像编辑](/zh/model-studio/qwen-image-edit-api)。

### lack of image or text <span id="20bab1af67gld" />

<strong>原因：</strong> 请求参数缺少image或text字段。

<strong>解决方案：</strong> 详情参见[千问-图像编辑](/zh/model-studio/qwen-image-edit-api)。

### num\_images\_per\_prompt must be 1. <span id="802dc15665v51" />

<strong>原因：</strong> 请求参数不合法，参数`n`（生成图片数量）只能设置为1。

<strong>解决方案：</strong> 请将参数`n`的值设置为1。

### Input files format not supported. <span id="a5b8a6b1beq55" />

<strong>原因：</strong> 音频、图片格式不符合要求。

<strong>解决方案：</strong> 音频支持格式mp3, wav, aac，图片支持格式jpg, jpeg, png, bmp, webp。详情参见[LivePortrait 视频生成](/zh/model-studio/liveportrait-api)。

### Failed to download input files. <span id="27d828761enyc" />

<strong>原因：</strong> 输入文件下载失败。

<strong>解决方案：</strong> 请检查文件URL是否可访问，网络是否通畅。

### oss download error. <span id="d3dc2298f58xl" />

<strong>原因：</strong> 输入图像下载失败。

<strong>解决方案：</strong> 请检查图像的OSS链接是否正确且可访问。

### The image content does not comply with green network verification. <span id="2df01b4b59aj3" />

<strong>原因：</strong> 图像内容不合规。

<strong>解决方案：</strong> 请更换符合内容安全规范的图像。

### read video error. <span id="5ee56eeaa09re" />

<strong>原因：</strong> 读取视频失败。

<strong>解决方案：</strong> 请检查视频文件是否损坏或格式不受支持。

### the size of input image is too small or too large. <span id="53168a7affchp" />

<strong>原因：</strong> 输入图像的尺寸过小或者过大。

<strong>解决方案：</strong> 请调整图像尺寸以符合API要求。

### The request parameter is invalid, please check the request parameter. <span id="b985256a56crq" />

<strong>原因：</strong> 该报错为多场景通用报错：可能是`clothes_type`入参不合规（AI试衣-图片分割），也可能是画幅入参不合规。

<strong>解决方案：</strong> 详情参见[AI试衣-图片分割](/zh/model-studio/aitryon-parsing-api)；若为画幅入参问题，可选"1:1"或"3:4"。

### The type or value of \{parameter} is out of definition. <span id="424c936759loo" />

<strong>原因：</strong> 参数类型或值不符合要求。

<strong>解决方案：</strong> 详情参见[LivePortrait 视频生成](/zh/model-studio/liveportrait-api)。

### request timeout after 23 seconds. <span id="7b9f15725drh2" />

<strong>原因：</strong> 超过23秒未向服务发送数据。该报错信息在使用[实时语音合成（Sambert）](/zh/model-studio/sambert-speech-synthesis)、[语音识别（Paraformer）](/zh/model-studio/paraformer-real-time-speech-recognition-api-reference)和[实时语音合成（Qwen-Audio-TTS/CosyVoice）](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)时产生。

<strong>解决方案：</strong> 请检查为什么长时间未向服务器发送数据。如果长时间（超过23秒）不向服务端发送消息，请及时结束任务。

### Please ensure input text is valid. <span id="dc047c34e6lzk" />

<strong>原因：</strong> 若您使用[实时语音合成（Qwen-Audio-TTS/CosyVoice）](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)，此错误通常是由于未发送待合成文本引起的。可能原因包括：参数遗漏（未为 `text` 参数赋值）或代码异常（导致对 `text` 参数的赋值失败）。

<strong>解决方案：</strong> 请排查代码，确保 `text` 参数被正确赋值并发送。

### Missing required parameter 'xxx'! Please follow the protocol! <span id="2672a27f04522" />

<strong>错误示例</strong>：

- Missing required parameter 'payload.model'! Please follow the protocol!
- Missing required parameter 'payload.task\_group'! Please follow the protocol!

<strong>原因：</strong>

1. WebSocket 事件 JSON 格式有误（通用原因）。

   使用 WebSocket 协议调用模型时，发送的 JSON 格式不正确，常见问题包括：

   - JSON 结构层级错误，参数没有放在正确的嵌套层级下。
   - 参数名拼写错误，例如 `task_group` 写成 `taskgroup`。
   - 参数值为空或未正确赋值。
2. stop 后未重新调用 call 方法（特定场景）。

   仅适用于以下场景：使用 Fun-ASR 或 Paraformer 实时语音识别的DashScope Java SDK。

   调用 `stop()` 方法结束本次识别后，没有重新调用 `call()` 方法就继续发送数据，导致会话状态异常，触发该报错。

   <strong>SDK 用户与 WebSocket 用户的区别：</strong>"Missing required parameter"是服务端返回的协议层错误，仅在直接使用 WebSocket 协议时出现。如果使用 DashScope Java SDK 的 Recognition 类，SDK 会在客户端拦截此类操作，抛出"状态无效：预期的识别状态应为已启动，但当前为空闲状态"（Invalid state）异常，而非返回"Missing required parameter"错误。

<strong>解决方案：</strong>

1. 针对原因1，检查 JSON 格式与参数。
2. 针对原因2，确保每次调用 `stop()` 之后，下一轮识别前必须重新调用 `call()` 方法。如果使用 DashScope Java SDK，请留意客户端抛出的"状态无效"（Invalid state）异常；如果直接使用 WebSocket 协议，则会收到"Missing required parameter"错误。

### \[tts:]Engine return error code: 418 <span id="4f506a938a4qe" />

<strong>原因：</strong> 使用[实时语音合成（Qwen-Audio-TTS/CosyVoice）](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)，请求参数 `voice`（音色）不正确，或 `model`（模型）与 `voice`（音色）版本不匹配。

<strong>解决方案：</strong>

1. <strong>检查</strong> `voice` <strong>参数赋值</strong>：

   - 如果使用的是默认音色，请对照[Python SDK](/zh/model-studio/cosyvoice-python-sdk)中的“voice参数”进行确认。
   - 如果使用的是声音复刻音色，请通过[CosyVoice声音复刻/设计API](/zh/model-studio/cosyvoice-clone-design-api)接口确认音色状态为“OK”，并确保音色归属账号与调用账号一致。
2. <strong>检查版本匹配</strong>：v2模型只能使用v2的音色，v1模型只能使用v1的音色，两者不可混用。

### Request voice is invalid! <span id="a4600d0262hc3" />

<strong>原因：</strong> 若您使用[实时语音合成（Qwen-Audio-TTS/CosyVoice）](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)，此错误通常是因为未设置音色。

<strong>解决方案：</strong> 请检查是否对`voice`参数赋值。若您使用[WebSocket API参考](/zh/model-studio/cosyvoice-websocket-api)，请参照API文档按照正确JSON格式配置参数。

### ref\_images\_url and obj\_or\_bg must be the same length. <span id="237cb9c9eecp3" />

<strong>原因：</strong> 使用[万相-视频编辑（2.1）](/zh/model-studio/legacy-wanx-vace-api-reference)的多图参考功能时，`ref_images_url`和`obj_or_bg`的数组长度不一致。

<strong>解决方案：</strong> 请确保`ref_images_url`和`obj_or_bg`的数组长度一致。

### check input data style. <span id="1a67cab97bzx3" />

<strong>原因：</strong> 输入参数不满足入参要求。

<strong>解决方案：</strong> 请检查并修正输入参数。

### An error during model pre-process. <span id="6aea77ceaelrp" />

<strong>原因：</strong> 传入了错误格式的 content 字段。

<strong>解决方案：</strong>

- 若通过代码调用，请勿将content设置为如`[{"type": "text", "text": "你是谁？"}]`的array类型。
- 若使用 [Cline](/zh/model-studio/cline) ，请在设置界面单击`MODEL CONFIGURATION`，并勾选 <strong>Enable R1 messages format</strong>。

### The image size is not supported for the data inspection. <span id="80fd826206ngp" />

<strong>原因：</strong>

- 传入千问VL模型的图像尺寸（长度和宽度）不符合模型的要求。
- 输出图像大小超出限制（10MB）。

<strong>解决方案：</strong>

- 图像尺寸需满足以下要求：

  - 图像的宽度和高度均不小于10像素。
  - 宽高比不应超过200:1或1:200
- 调整生成图像的参数。

### Wrong Content-Type of multimodal url <span id="065e89dd58uf2" />

<strong>原因</strong>：URL请求的响应头信息`Content-Type`字段不正确。

> 千问VL模型支持的Content Type为：image/bmp、image/bmp、image/icns、image/x-icon、image/jpeg、image/jp2、image/png、image/sgi、image/tiff、image/webp。详情可参见[千问VL模型支持的图像](/zh/model-studio/vision)。

<strong>解决方案</strong>：

<Accordion title="查看 Content-Type 字段">
  1. 打开浏览器（如Chrome或Firefox）。
  2. 打开开发者工具（通常按F12或右键选择“检查”）。
  3. 切换到Network标签。
  4. 将图片的URL输入到地址栏并访问。
  5. 找到对应的请求，查看Headers部分，在“响应头”（Response Headers）部分中查找Content-Type字段。
</Accordion>

### Field required: image\_url <span id="068dee94174u6" />

<strong>原因：</strong> 缺少入参`image_url`。

<strong>解决方案：</strong> 请参考[Emoji 视频生成](/zh/model-studio/emoji-api)，传入`image_url`参数。

### Field required: driven\_id <span id="e204d0079324i" />

<strong>原因：</strong> 缺少入参`driven_id`。

<strong>解决方案：</strong> 请参考[Emoji 视频生成](/zh/model-studio/emoji-api)，传入`driven_id`参数。

### Invalid ext\_bbox <span id="6b8ba11ceeyae" />

<strong>原因：</strong> 输入`ext_bbox`参数无效。

<strong>解决方案：</strong> 请参考[Emoji 视频生成](/zh/model-studio/emoji-api)，传入正确的`ext_bbox`。

### Driven not exist: driven\_id <span id="ed5667fb76b8l" />

<strong>原因：</strong>输入`driven_id`不存在。

<strong>解决方案：</strong> 请参考[Emoji 视频生成](/zh/model-studio/emoji-api)，传入正确的`driven_id`。

### Text request limit violated, expected 1. <span id="fc77e3fcbfax3" />

<strong>原因：</strong>在调用CosyVoice语音合成的[WebSocket API参考](/zh/model-studio/cosyvoice-websocket-api)时，将`enable_ssml`设为`true`后多次发送continue-task指令。

<strong>解决方案：</strong> `enable_ssml`设为`true`后，只允许发送一次continue-task指令。

### SSML text is not supported at the moment! <span id="01c99b4504lv5" />

<strong>原因：</strong>使用CosyVoice语音合成功能时，当前模型或音色不支持 SSML，或未正确启用 SSML 功能。

<strong>解决方案：</strong>请根据[限制与约束](/zh/model-studio/introduction-to-cosyvoice-ssml-markup-language)进行排查

### \[tts:]Engine return error code: 428 <span id="17aa73b94884d" />

<strong>原因：</strong>CosyVoice语音合成`instruction` 参数使用有误，具体可能包括以下情况

- <strong>长度超限</strong>：`instruction` 不可超过 100 字符（汉字按 2 个字符计算，其余字符按 1 个字符计算）
- <strong>格式有误</strong>或<strong>语言有误</strong>：仅以下模型支持 `instruction`，且规则各不相同：

  - cosyvoice-v3.5-flash、cosyvoice-v3.5-plus：可自由输入任意指令（如情感、语速等）
  - cosyvoice-v3-flash：

    - 须按固定格式编写
    - 仅支持中文 `instruction`
    - 不同音色支持的 `instruction` 不同，详情请参见[CosyVoice音色列表](/zh/model-studio/cosyvoice-voice-list)

<strong>解决方案：</strong>

- 检查 `instruction` 字符长度是否超过 100
- 确认所用模型是否支持 `instruction` 参数
- 使用 cosyvoice-v3-flash 时，确保 `instruction` 为中文且符合对应音色的固定格式

### At least one of 'lyrics' or 'prompt' must be provided. <span id="a3b7e2c4f1m8k" />

<strong>原因：</strong> 使用Fun-Music模型时，请求中未提供`lyrics`或`prompt`参数。

<strong>解决方案：</strong> 请在请求中至少提供`lyrics`或`prompt`参数之一。

### Lyrics content is illegal and cannot be used for music generation. <span id="b0c4d1e5f2t5r" />

<strong>原因：</strong> 使用Fun-Music模型时，歌词内容检测未通过，可能包含侵权内容。

<strong>解决方案：</strong> 请修改歌词内容，确保不包含侵权或违规内容后重试。

## 400-invalid\_request\_error-invalid\_value <span id="c79a10f70ad2j" />

### -1 is lesser than the minimum of 0 - 'seed'/'seed' must be Integer <span id="ac2f420bc14i2" />

<strong>原因：</strong> 使用OpenAI兼容协议时，`seed` 参数设置未在 \[0, 2<sup>31</sup>-1]的范围内。

<strong>解决方案：</strong> 将`seed`参数设置在 \[0, 2<sup>31</sup>-1]的范围内。

## 400-invalid\_request\_error <span id="43282c028ar0z" />

### you must provide a model parameter. <span id="3a92c18ee560a" />

<strong>原因：</strong> 请求时没有提供 `model` 参数。

<strong>解决方案：</strong> 请在请求中添加`model`参数。

## 400-InvalidParameter.NotSupportEnableThinking <span id="8185ed2587vjd" />

### The model xxx does not support enable\_thinking. <span id="50ea597fd7awl" />

<strong>原因：</strong> 当前使用的模型不支持设定参数 `enable_thinking`。

<strong>解决方案：</strong> 请求时去掉`enable_thinking`参数，或使用支持思考模式的模型。

## 400-invalid\_value <span id="a49d5136b1e1c" />

### The requested voice 'xxx' is not supported. <span id="29219324c410f" />

<strong>原因：</strong> 在进行Qwen-TTS实时语音合成时，选用的音色是通过Qwen-TTS声音复刻功能生成的，但二者使用的模型不同。

<strong>解决方案：</strong> 请检查声音复刻时的请求参数`target_model`和语音合成时的请求参数`model`是否一致。

## 400-Arrearage <span id="083013fdf2buo" />

### Access denied, please make sure your account is in good standing. <span id="overdue_payment" />

<strong>原因：</strong> API Key 所属的阿里云账号存在欠费，导致访问被拒绝。

<strong>解决方案：</strong>前往[费用与成本](https://usercenter2.aliyun.com/home)查看是否欠费：

- 未欠费：请确认该 API Key 是否属于当前账号。如果账号不存在欠费的情况，可能账户出现异常，详情请联系客服进一步排查。
- 欠费：请及时充值。充值后，系统余额可能存在延迟，请稍等后重试。

<Note>
  Q：购买了资源包，且只调用了资源包内的模型，为什么会欠费？

  A：请核对资源包的可抵扣范围。以qwen-plus/qwen-plus-latest系列资源包为例：

  资源包仅可抵扣qwen-plus、qwen-plus-latest模型input tokens和output tokens费用，且仅支持抵扣单次请求输入在0\<Token≤128K阶梯范围内产生的实时推理费用（非思考模式）

  不支持抵扣的费用包括：

  - 单次请求输入在Token>128K阶梯范围产生的费用。
  - Batch调用、上下文缓存、模型调优、模型部署产生的费用。
</Note>

### API provider returned a billing error — your API key has run out of credits or has an insufficient balance. Check your provider's billing dashboard and top up or switch to a different API key. <span id="openclaw-billing-error" />

<strong>原因：</strong> 通过 OpenClaw 等第三方客户端调用百炼时，若底层账号欠费或余额不足，客户端会将服务端返回的计费类错误聚合为此英文提示。其底层对应的百炼服务端错误码为 Arrearage（即上文 <strong>Access denied, please make sure your account is in good standing.</strong>），并非客户端本身的计费问题。

<strong>解决方案：</strong> 前往[费用与成本](https://usercenter2.aliyun.com/home)查看账号是否欠费：欠费请及时充值（充值后系统余额可能存在延迟，请稍候后重试）；未欠费请确认调用所用的 API Key 是否属于当前账号。

### isv.OUT\_OF\_SERVICE <span id="isv-out-of-service" />

<strong>原因：</strong> 阿里云账户余额不足，导致服务被暂停。其底层原因与本节的 Arrearage（`Access denied, please make sure your account is in good standing.`）一致，均为账户欠费。

<strong>解决方案：</strong> 前往[费用与成本](https://usercenter2.aliyun.com/home)为阿里云账户充值，余额充足后服务自动恢复。

## 400-Contain.Forbidden.Label.Error <span id="forbidden-label-h2" />

### contain forbidden create order label <span id="contain-forbidden-create-order-label" />

<strong>原因：</strong> 阿里云账号存在禁止下单的标签，本次下单请求被拦截，无法下单。

<strong>解决方案：</strong> 通过[错误码诊断](https://api.aliyun.com/troubleshoot?q=Contain.Forbidden.Label.Error)查看该错误码的诊断结果；并联系客服确认该标签的添加原因并申请解除，解除后重新下单。

## 400- DataInspectionFailed/data\_inspection\_failed <span id="f836121381tv2" />

### Input or output data may contain inappropriate content. / Input data may contain inappropriate content. / Output data may contain inappropriate content. <span id="inappropriate_content" />

<strong>原因：</strong> 输入或者输出包含疑似敏感内容被绿网拦截。

<strong>解决方案：</strong> 请修改输入内容后重试。如需申请内容加白，请联系客户经理。

### Input xxx data may contain inappropriate content. <span id="63fcebbe0960k" />

<strong>原因：</strong> 输入数据（如提示词或图像）可能包含敏感内容。 <strong>解决方案：</strong> 内容合规检查，请修改输入后重试。

### Qwen rejected the input image before model inference; no actual ad/porn/OCR result was produced. <span id="qwen_img_prefilter_h3" />

<strong>原因：</strong> 输入图片在进入模型推理前，被内容安全前置检测（绿网）判定为疑似包含敏感或违规内容而拦截。模型未对该图片进行推理，因此不会返回任何识别、OCR 或分析结果。此为对多模态输入图片的安全合规前置校验，与上文输入内容被绿网拦截属同一内容安全机制。

<strong>解决方案：</strong> 请更换或修改输入图片后重试；若确认图片内容合规仍被持续拦截，可提交工单进一步核实。

## APIConnectionError（客户端网络异常，服务端未返回状态码） <span id="4fbf7d70ffjab" />

### Connection error. <span id="5c18c53576jsp" />

<strong>原因：</strong> 本地网络问题，通常是因为开启了代理。

<strong>解决方案：</strong> 请关闭或者重启代理。

### The operation was canceled. / Connection reset <span id="operation-canceled-connection-reset" />

<strong>原因：</strong> 客户端设置的超时时间过短或网络波动，可能导致请求被取消（如 `The operation was canceled`）或连接重置（如 `Connection reset`）。

<strong>解决方案：</strong>

- 适当放宽客户端超时配置（如调整至 50 秒以上）。
- 使用 `curl` 命令测试网络连通性，以排除本地网络问题。

## 400-InvalidFile.DownloadFailed <span id="c04b23ec0bugz" />

### The audio file cannot be downloaded. <span id="9e997f8c55374" />

<strong>原因：</strong> 使用[语音识别（Paraformer）](/zh/model-studio/paraformer-real-time-speech-recognition-api-reference)录音文件识别，待识别文件下载失败。

<strong>解决方案：</strong> 请检查待识别音频文件URL是否可通过公网访问。

## 400-InvalidFile.AudioLengthError <span id="208735a309uny" />

### Audio length must be between 1s and 300s. <span id="76b72eb9880df" />

<strong>原因：</strong> 音频长度不符合要求。

<strong>解决方案：</strong> 请确保音频时长在\[1, 300]秒范围内

### Audio length must be between 1s and 180s. <span id="e507f88361jnq" />

<strong>原因：</strong> 音频长度不符合要求。

<strong>解决方案：</strong> 请确保音频时长在\[1, 180]秒范围内。

## 400-InvalidFile.NoHuman <span id="8ef7e181d1yp6" />

### The input image has no human body. Please upload other image with single person. <span id="7e27bbf8cfu8u" />

<strong>原因：</strong> 输入图片中没有人或未检测到人脸。

<strong>解决方案：</strong> 请上传单人照。

## 400-InvalidFile.BodyProportion <span id="1c86f01821u2j" />

### The proportion of the detected person in the picture is too large or too small, please upload other image. <span id="87bd5233266g8" />

<strong>原因：</strong> 上传图片中人物占比不符合要求。

<strong>解决方案：</strong> 请上传符合人物占比要求的图片。

## 400-InvalidFile.FacePose <span id="da7af1d9bezun" />

### The pose of the detected face is invalid, please upload other image with whole face and expected orientation. <span id="fb4858cfbfoew" />

<strong>原因：</strong> 上传图片中人物面部姿态不符合要求（要求面部可见，头部朝向无严重偏移）。

<strong>解决方案：</strong> 请上传符合要求的图片。

### The pose of the detected face is invalid, please upload other image with the expected oriention. <span id="3102c08fc7hva" />

<strong>原因：</strong> 上传图片中人物面部姿态不符合要求（要求面部朝向无严重偏移）。

<strong>解决方案：</strong> 请确保图片中人脸朝向无偏斜。

### The pose of the detected face is invalid, please upload other image with the expected orientation. <span id="6fa52f885c6wt" />

<strong>原因：</strong> 上传图片中人物面部姿态不符合要求（要求面部朝向无严重偏移）。

<strong>解决方案：</strong> 请确保图片中人脸朝向无偏斜。

## 400-InvalidFile.Resolution <span id="048b4ab769qb4" />

### The image resolution is invalid, please make sure that the largest length of image is smaller than 7000, and the smallest length of image is larger than 400. <span id="94964bec92mt0" />

<strong>原因：</strong> 上传图像大小不符合要求。

<strong>解决方案：</strong> 上传图片的分辨率不得高于7000*7000，且不得低于400*400。

### The image resolution is invalid, please make sure that the largest length of image is smaller than 4096, and the smallest length of image is larger than 224. <span id="636ef70d9f1rm" />

<strong>原因：</strong> 上传图像大小不符合要求。

解决方案： 上传图片的分辨率最长边小于 4096 像素，且最短边大于 224 像素。

### The image resolution is invalid, please make sure that the largest length of image is smaller than xxx, and the smallest length of image is larger than yyy. <span id="adbdd0f2c6812" />

<strong>原因：</strong> 上传图像大小不符合要求。

<strong>解决方案：</strong> 上传图片的分辨率不得高于xxx*xxx，且不得低于yyy*yyy。

### The image resolution is invalid, please make sure that the aspect ratio is smaller than xxx, and largest length of image is smaller than yyy. <span id="b256644e8cyfl" />

<strong>原因：</strong> 上传图像大小不符合要求。

<strong>解决方案：</strong> 上传图片的长宽比必须小于xxx，且分辨率不得高于yyy\*yyy。

### Invalid video resolution. The height or width of video must be xxx \~ yyy. <span id="c8f1246b25q5n" />

<strong>原因：</strong> 视频分辨率不符合要求。

<strong>解决方案：</strong> 视频边长需介于xxx-yyy之间。

## 400-InvalidFile.FPS <span id="104e267043a8j" />

### Invalid video FPS. The video FPS must be 15 \~ 60. <span id="cbbafc57a4001" />

<strong>原因：</strong> 视频帧率不符合要求。

<strong>解决方案：</strong> 视频帧率需介于15-60fps之间。

## 400-InvalidFile.Value <span id="6735522da8j2d" />

### The value of the image is invalid, please upload other clearer image. <span id="ebdb21093e7al" />

<strong>原因：</strong> 上传图片过暗不符合要求。

<strong>解决方案：</strong> 请确保图片中人脸清晰。

## 400-InvalidFile.FrontBody <span id="d9dd15789e2l9" />

### The pose of the detected person is invalid, please upload other image with the front view. <span id="2213529c64gtn" />

<strong>原因：</strong> 上传图片中人物背身不符合要求。

<strong>解决方案：</strong> 请确保图片中人物正面朝向镜头。

## 400-InvalidFile.FullFace <span id="5c57e01c37qyg" />

### The pose of the detected face is invalid, please upload other image with whole face. <span id="262d646c5flkd" />

<strong>原因：</strong> 上传图片中人物面部姿态不符合要求（要求面部可见）。

<strong>解决方案：</strong> 请确保图片中人脸完整无遮挡。

## 400-InvalidFile.FaceNotMatch <span id="397748d6e8lfa" />

### There are no matched face in the video with the provided reference image. <span id="765d5ba07a38c" />

<strong>原因：</strong> 参考图与视频人脸匹配失败。

<strong>解决方案：</strong> 详情参见[VideoRetalk视频生成](/zh/model-studio/videoretalk-api)。

## 400-InvalidFile.Content <span id="6f331f83624zg" />

### The first frame of input video has no human body. Please choose another clip. <span id="e97b87da99znx" />

<strong>原因：</strong> 视频首帧需要有人。

<strong>解决方案：</strong> 请选择包含人体的视频片段。

### The human is too small in the first frame of input video. Please choose another clip. <span id="37ab82df6b93t" />

<strong>原因：</strong> 视频首帧人物过小。

<strong>解决方案：</strong> 请选择首帧人物占比较大的视频。

### The human is not clear in the first frame of input video. Please choose another clip. <span id="58d5a3815c80m" />

<strong>原因：</strong> 视频首帧人物不清晰。

<strong>解决方案：</strong> 请选择首帧人物清晰的视频。

### The input image has no human body or multi human bodies. Please upload other image with single person. <span id="a8391a22eeeke" />

<strong>原因：</strong> 输入图片中没有人或有多人。

<strong>解决方案：</strong> 请上传单人照。

### The input image has no human body or has unclear human body. Please upload other image. <span id="fdb1b081b9f1o" />

<strong>原因：</strong> 输入图片中人体不完整或者没有人体。

<strong>解决方案：</strong> 请上传包含完整清晰人体的图片。

### The input image has multi human bodies. Please upload other image with single person. <span id="da5ece1a709al" />

<strong>原因：</strong> 输入图片中有多人。

<strong>解决方案：</strong> 请上传单人照。

## 400-InvalidFile.FullBody <span id="bbfbd885d0p6c" />

### The human is not fullbody in the first frame of input video. Please choose another clip. <span id="62189d4fb6k0l" />

<strong>原因：</strong> 视频首帧人物不完整。

<strong>解决方案：</strong> 需露出人物全身。

### The pose of the detected person is invalid, please upload other image with whole body, or change the ratio parameter to 1:1。 <span id="7503c2a8c0z1t" />

<strong>原因：</strong> 上传图片中人物姿态不符合要求。

<strong>解决方案：</strong> 请上传符合要求的图片，头像照要求头部完整可见，半身照要求髋部以上完整可见，或者调整图像宽高比为1:1。

## 400-InvalidFile.BodyPose <span id="2ce6e57817o4k" />

### The pose of the detected person is invalid, please upload other image with whole body and expected orientation. <span id="3f30131d38q7f" />

<strong>原因：</strong> 单人的动作不符合要求。

<strong>解决方案：</strong> 请上传符合要求的图片，要求肩膀及踝部可见，非背身，非坐姿，人物朝向无严重偏移。

## 400-InvalidFile.Size <span id="e53052d41anfw" />

### Invalid file size. The video file size must be less than 200MB, and the audio file size must be less than 15MB. <span id="4b7c2916d83vc" />

<strong>原因：</strong> 文件大小不符合要求。

<strong>解决方案：</strong> 视频文件必须小于200MB，音频文件必须小于15MB。

### Invalid file size, The image file size must be smaller than 5MB. <span id="5b8149a42b93h" />

<strong>原因：</strong> 文件大小不符合要求。

<strong>解决方案：</strong> 图片文件必须小于5MB。

### Invalid file size. The video/audio/image file size must be less than xxxMB. <span id="a2b831c2b8o2v" />

<strong>原因：</strong> 文件大小不符合要求。

<strong>解决方案：</strong> 视频/音频/图像文件必须小于指定的MB数。

## 400-InvalidFile.Duration <span id="df386c98dciny" />

### Invalid file duration. The file duration must be xxx s \~ yyy s. <span id="ebc928f216l5i" />

<strong>原因：</strong> 文件时长不符合要求。

<strong>解决方案：</strong> 视频/音频文件时长需要介于xxx-yyy s之间。

## 400-InvalidFile.ImageSize <span id="3ab6be1e3adqm" />

### The size of image is beyond limit. <span id="98ea2009d8sdg" />

<strong>原因：</strong> 图片大小超出限制。

<strong>解决方案：</strong> 要求图片长宽比例不大于2，且最长边不大于4096。

## 400-InvalidFile.AspectRatio <span id="181c889a02m9p" />

### Invalid file ratio. The file aspect ratio (height/width) must be between 3:1 and 1:3. <span id="4d0d0408e845g" />

<strong>原因：</strong> 文件长宽比不符合要求。

<strong>解决方案：</strong> 视频文件长宽比需要介于3:1到1:3之间。

### Invalid file ratio. The file aspect ratio (height/width) must be between 2.0 and 0.5. <span id="b4a6a8102a6wn" />

<strong>原因：</strong> 文件长宽比不符合要求。

<strong>解决方案：</strong> 图片文件宽高比必须在2.0到0.5之间。

## 400-InvalidFile.Openerror <span id="d6971254e81pc" />

### Invalid file, cannot open file as video/audio/image. <span id="c0d6116868i8o" />

<strong>原因：</strong> 文件无法打开。

<strong>解决方案：</strong> 请检查文件是否损坏或格式是否正确。

## 400-InvalidFile.Template.Content <span id="62a45cdad8i6s" />

### Invalid template content. <span id="65f25520806kx" />

<strong>原因：</strong> 动作模板无权限，或模板内容不符合要求。

<strong>解决方案：</strong> 请检查模板权限和内容。

## 400-InvalidFile.Format <span id="372f5717b4znd" />

### Invalid file format，the request file format is one of the following types: MP4, AVI, MOV, MP3, WAV, AAC, JPEG, JPG, PNG, BMP, and WEBP. <span id="fd5eef38d117o" />

<strong>原因：</strong> 文件格式不符合要求。

<strong>解决方案：</strong> 使用符合要求的文件：视频支持mp4、avi、mov；音频支持mp3, wav, aac；图片支持jpg, jpeg, png, bmp, webp。

## 400-InvalidFile.MultiHuman <span id="a27060735151p" />

### The input image has multi human bodies. Please upload other image with single person. <span id="9a8a70f4bcbps" />

<strong>原因：</strong> 输入图片中有多人。

<strong>解决方案：</strong> 请上传单人照。

## 400-InvalidPerson <span id="b1ad131e29lnf" />

### The input image has no human body or multi human bodies. Please upload other image with single person. <span id="d33020a7ddrt7" />

<strong>原因：</strong> 输入图片中没有人或有多人。

<strong>解决方案：</strong> 请上传单人照。

## 400-InvalidParameter.DataInspection <span id="d4def69268vky" />

### Unable to download the media resource during the data inspection process. <span id="84c05e90a3oca" />

<strong>原因：</strong> 下载图片或音频文件超时。

<strong>解决方案：</strong> 如果从海外发起调用，由于跨境网络不稳定，可能会导致下载资源超时。请将文件存储到国内的 [OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss) 中，再发起模型调用。也可以使用[临时存储空间](/zh/model-studio/get-temporary-file-url)上传文件。

## 400-FlowNotPublished <span id="b4a60a2ce4ijj" />

### Flow has not published yet, please publish flow and try again. <span id="fa397bf3bew1f" />

<strong>原因：</strong> 流程未发布。

<strong>解决方案：</strong> 请发布流程后再重试。

## 400- InvalidImage.ImageSize <span id="75fa690829fh5" />

### The size of image is beyond limit. <span id="0ffbb8b288vfv" />

<strong>原因：</strong> 图片大小超出限制。

<strong>解决方案：</strong> 要求图片长宽比例不大于2，且最长边不大于4096。

## 400- InvalidImage.NoHumanFace <span id="007e368c9dy6c" />

### No human face detected. <span id="d63b398504doi" />

<strong>原因：</strong> 未检测到人脸（仅生成任务异步查询接口）。

<strong>解决方案：</strong> 请上传包含清晰人脸的图片。

## 400- InvalidImageResolution <span id="da01c607ada0h" />

### The input image resolution is too large or small. <span id="f4214add372tj" />

<strong>原因：</strong> 输入图像分辨率过大或过小。

<strong>解决方案：</strong> 图像分辨率不低于256×256像素，不超过5760×3240像素。

## 400- InvalidImageFormat <span id="a0b7a16bd8exe" />

### The input image is in invalid format. <span id="388af0747dzhh" />

<strong>原因：</strong> 图片格式不符合要求。

<strong>解决方案：</strong> 使用JPEG、PNG、JPG、BMP、WEBP格式的图片。

## 400- InvalidURL <span id="64d6eb0868hn9" />

### Invalid URL provided in your request. <span id="8ea4253ce5572" />

<strong>原因：</strong> URL 无效。

<strong>解决方案：</strong> 使用有效的 URL。

### Required URL is missing or invalid, please check the request URL. <span id="59f5db978fluc" />

<strong>原因：</strong> 输入的URL无效或缺失。

<strong>解决方案：</strong> 请提供正确的URL。

### The request URL is invalid, make sure the url is correct and is an image. <span id="3035971634vfv" />

<strong>原因：</strong> 输入的URL无效。

<strong>解决方案：</strong> 请确保URL正确且指向一个图像文件。

### The input audio is longer than xxs. <span id="c8689491ecuie" />

<strong>原因：</strong> 输入的音频文件超过最大时长xx秒。

<strong>解决方案：</strong> 请将音频文件裁剪至xx秒以内。

### File size is larger than 15MB. <span id="1a22d3f4d29ur" />

<strong>原因：</strong> 输入的音频文件超过最大限制15MB。

<strong>解决方案：</strong> 请将音频文件压缩至15MB以内。

### File type is not supported. Allowed types are: .wav, .mp3. <span id="c86c120a0frab" />

<strong>原因：</strong> 输入的音频格式不合规。

<strong>解决方案：</strong> 当前仅支持wav、mp3格式。

### The request URL is invalid, please check the request URL is available and the request image format is one of the following types: JPEG, JPG, PNG, BMP, and WEBP. <span id="734be871d520e" />

<strong>原因：</strong> 图片不可访问或下载的文件格式不支持。

<strong>解决方案：</strong> 请确保URL可访问，且图片格式为JPEG, JPG, PNG, BMP或WEBP。

## 400-InvalidImage.FileFormat <span id="acc5e024f7a00" />

### Invalid image type. Please ensure the uploaded file is a valid image. <span id="f3dc8c3193wbb" />

<strong>原因：</strong>图片文件格式不支持。

<strong>解决方案：</strong>使用JPG、JPEG、PNG、BMP、WEBP格式的图片。

## 400- InvalidURL.ConnectionRefused <span id="c40e7ac503gmh" />

### Connection to xxx refused, please provide available URL. <span id="fd88361112wqo" />

<strong>原因：</strong> 下载被拒绝。

<strong>解决方案：</strong> 请提供可用的URL。

## 400- InvalidURL.Timeout <span id="8f0e4cd147p0r" />

### Download xxx timeout, please check network connection. <span id="75bc34df2dszq" />

<strong>原因：</strong> 下载超时。

<strong>解决方案：</strong> 请检查网络连接。

## 400- BadRequestException <span id="81d08f6decqm0" />

### Invalid part type. <span id="7b97d99dbeuxq" />

<strong>原因：</strong> 仅在Qwen-Long模型的对话场景中，用户上传了Qwen-Long模型暂不支持的文件类型。

<strong>解决方案：</strong> 请上传Qwen-Long支持的文件类型。

## 400- BadRequest.EmptyInput <span id="14f7cc8b18l2j" />

### Required input parameter missing from request. <span id="8d5e1083c4nze" />

<strong>原因：</strong> 请求时未添加`input`参数。

<strong>解决方案：</strong> 请在请求中添加`input`参数。

## 400-BadRequest.EmptyParameters <span id="0472622e59c9i" />

### Required parameter "parameters" missing from request. <span id="7a2724f009rnk" />

<strong>原因：</strong>请求时未添加 `parameters`参数。

<strong>解决方案：</strong> 请在请求中添加`parameters`参数。

## 400-BadRequest.EmptyModel <span id="ab1cc34f69650" />

### Required parameter "model" missing from request. <span id="3ec94aebe8a2e" />

<strong>原因：</strong> 请求时未提供 `model`参数。

<strong>解决方案：</strong> 请在请求中添加`model`参数。

## 400-BadRequest.IllegalInput <span id="9f736f4a4bkoq" />

### The input parameter requires json format. <span id="e201b8e74dvxo" />

<strong>原因：</strong> 入参格式不符合API要求的JSON格式。

<strong>解决方案：</strong> 请检查入参数格式，确保为标准的JSON。

## 400-BadRequest.InputDownloadFailed <span id="28733eb69earp" />

### Failed to download the input file: xxx. <span id="4748fd9658fsz" />

<strong>原因：</strong> 下载输入文件失败，可能是由于下载超时、下载失败或者文件超过限额大小。

<strong>解决方案：</strong> 请根据详细错误信息`xxx`排查。

### Failed to download the input file. <span id="4c3178c4d5v4c" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，服务器下载待复刻音频失败。

<strong>解决方案：</strong> 请检查音频文件是否可以正常下载，若能下载，请检查音频文件大小是否超出限制（超过10MB）。

## 400-BadRequest.UnsupportedFileFormat <span id="4f0a6b6d0670t" />

### File format unsupported. <span id="db79516063t8d" />

<strong>原因：</strong>[CosyVoice声音复刻](/zh/model-studio/voice-replica-1)时，上传的音频格式不符合模型要求。

<strong>解决方案：</strong> 音频格式需为 WAV（16bit）、MP3 或 M4A。需要注意的是，不能仅凭文件后缀名判断格式，例如，后缀名为 `.mp3` 的文件可能是其他格式（如 Opus）。建议通过工具（如ffprobe、mediainfo）或命令（如Linux/macOS的file命令）确认音频文件的实际编码格式，以确保符合要求。

### Input file format is not supported. <span id="1f26623bb0inl" />

<strong>原因：</strong> 输入文件的格式不受支持。

<strong>解决方案：</strong> 请使用支持的文件格式。

## 400-BadRequest.TooLarge <span id="f0a69d648ddl2" />

### Payload Too Large. <span id="e12d1bf5aew1s" />

<strong>原因：</strong> 文件大小超出限制。

<strong>解决方案：</strong>

- “purpose”参数为“file-extract”时文档不能超150MB、图片不能超20MB。
- “purpose”参数为“batch”时，文件不能超500MB。 请拆分并分批[上传文件](/zh/model-studio/openai-file-interface)。

## 400-BadRequest.ResourceNotExist <span id="224fcd129860o" />

### The Required resource not exist. <span id="03edbbe6e2hwp" />

<strong>原因：</strong>

- [CosyVoice声音复刻](/zh/model-studio/voice-replica-1)更新、查询或删除接口调用时，对应音色不存在。
- 使用[定制热词（Paraformer）](/zh/model-studio/custom-hot-words)时，更新、查询或删除接口调用的热词资源不存在。

## 400- Throttling.AllocationQuota <span id="8d3476833aqht" />

### 您当前的配额为xxx <span id="48615f838ets9" />

<strong>原因：</strong> [CosyVoice声音复刻](/zh/model-studio/voice-replica-1)音色数量已达限额。

<strong>解决方案：</strong> [删除](/zh/model-studio/cosyvoice-clone-design-api)部分音色。

### Free allocated quota exceeded. <span id="e1ff62776chpa" />

<strong>原因：</strong> 使用定制热词（Paraformer）时，热词数目已超过上限（每个账号默认10个）。

<strong>解决方案：</strong> 删除部分热词。

### Maximum voice storage limit exceeded, please delete existing voices. <span id="69b7568d28b1h" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，超过主账号可用的音色数目上限。

<strong>解决方案：</strong> 请[删除](/zh/model-studio/qwen-tts-voice-cloning)一部分音色。

## 400-InvalidGarment <span id="27a38648fcg0s" />

### Missing clothing image.Please input at least one top garment or bottom garment image. <span id="94b477f7a5l08" />

<strong>原因：</strong> 缺少服饰图片。

<strong>解决方案：</strong> 请至少提供一张上装 (top\_garment\_url) 或下装 (bottom\_garment\_url) 的图片。

## 400-InvalidSchema <span id="811b3cc8ccmjl" />

### Database schema is invalid for text2sql. <span id="a5cbdd7535fgy" />

<strong>原因：</strong> 未输入数据库Schema信息。

<strong>解决方案：</strong> 请输入数据库Schema信息。

## 400- InvalidSchemaFormat <span id="976b4820377b0" />

### Database schema format is invalid for text2sql. <span id="5c1fb3a453w3m" />

<strong>原因：</strong> 输入数据表信息格式异常。

<strong>解决方案：</strong> 请检查并修正数据表信息的格式。

## 400-Audio. AudioShortError <span id="ebfd0b9f8aa9v" />

### valid audio too short! <span id="3b50830f4bwj4" />

<strong>原因：</strong> 用于[CosyVoice声音复刻](/zh/model-studio/voice-replica-1)的音频有效时长过短。

<strong>解决方案：</strong>音频时长应尽量控制在 10\~15 秒之间。录音时请确保朗读连贯，并包含至少一段超过 5 秒的连续语音。

## 400-Audio. AudioSilentError <span id="d1d58f394fe4f" />

### silent audio error. <span id="c3a2b1a77cg2c" />

<strong>原因：</strong> [CosyVoice声音复刻](/zh/model-studio/voice-replica-1)音频文件为静音或非静音长度过短。

<strong>解决方案：</strong> 用于声音复刻的音频时长应尽量控制在 10\~15 秒之间，并包含至少一段超过 5 秒的连续语音。

## 400-InvalidInputLength <span id="68d090b4b0m6x" />

### The image resolution is invalid, please make sure that the largest length of image is smaller than 4096, and the smallest length of image is larger than 150. and the size of image ranges from 5KB to 5MB. <span id="07b6d245976bc" />

<strong>原因：</strong> 图片尺寸或文件大小不符合要求。

<strong>解决方案：</strong> 请参见[输入图片要求](/zh/model-studio/aitryon-plus-api#7239603c85fye)。

## 400-FaqRuleBlocked <span id="9075c1fa28p7h" />

### Input or output data is blocked by faq rule. <span id="1874e51c40d5d" />

<strong>原因：</strong> 命中FAQ规则干预模块。

## 400-ClientDisconnect <span id="a8f7c2a7a4zi6" />

### Client disconnected before task finished! <span id="client-disconnected" />

<strong>原因：</strong> 任务结束前，客户端主动断开了连接。该报错信息在使用语音合成或识别相关服务时产生。

<strong>解决方案：</strong> 请检查代码，不要在任务结束前断开和服务端的连接。

## 400-ServiceUnavailableError <span id="8ce8a1037fy7j" />

### Role must be user or assistant and Content length must be greater than 0. <span id="35c9c8e907hba" />

<strong>原因：</strong> 输入内容长度为0或`role`不正确。

<strong>解决方案：</strong> 请检查输入内容长度大于0，并确保参数格式（如`role`）符合API文档的要求。

## 400-IPInfringementSuspect <span id="b0108ae3a2juf" />

### Input data is suspected of being involved in IP infringement. <span id="b130546d6alnn" />

<strong>原因：</strong> 输入数据（如提示词或图像）涉嫌知识产权侵权。

<strong>解决方案：</strong> 内容合规检查，请检查输入，确保不包含引发侵权风险的内容。

## 400-UnsupportedOperation <span id="d91dc0bf42k4t" />

### The operation is unsupported on the referee object. <span id="2761c10d0aond" />

<strong>原因：</strong> 关联的对象不支持该操作。

<strong>解决方案：</strong> 请检查操作对象和操作类型是否匹配。

### The fine-tune job can not be deleted because it is succeeded,failed or canceled. <span id="55b762964bdft" />

<strong>原因：</strong> 无法删除该微调任务，因为其状态已是“成功”、“失败”或“已取消”。

<strong>解决方案：</strong> 只有处于特定状态的任务才能被删除，请勿删除已终结状态的任务。

## 400-CustomRoleBlocked <span id="46a93eb6dfugb" />

### Input or output data may contain inappropriate content with custom rule. <span id="1f1f7f148cjvl" />

<strong>原因：</strong> 请求或响应内容没有通过自定义策略。

<strong>解决方案：</strong> 请检查内容或调整自定义策略。

## 400-Audio.PreprocessError <span id="4d74bd62ffoo8" />

### Audio preprocess error. <span id="7b6494d724paq" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，待复刻音频预处理异常，可能的原因为：`text`参数内容与音频文本差别过大、有效人声过短、无声音等。

<strong>解决方案：</strong> 请调整`text`参数的内容，若调整后无效，请参照录音操作指南重新录制音频。

### No segments meet minimum duration requirement <span id="7f01f842c1dh3" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，待复刻音频有效人声过短。

<strong>解决方案：</strong> 请参照录音操作指南重新录制音频。

## 400-BadRequest.VoiceNotFound <span id="aceb2ac118nqs" />

### Voice '%s' not found. <span id="989e84042cby0" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，调用删除音色接口时，音色已删除或音色不存在。

<strong>解决方案：</strong> 请检查传入的`voice`参数是否正确。

## 400-Audio.DecoderError <span id="ae4ceecf7fbuw" />

### Decoder audio file failed. <span id="b4e9f6a4a4ndh" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，待复刻音频解码失败。/ CosyVoice声音复刻音频文件解码失败。

<strong>解决方案：</strong> 请检查音频文件是否损坏，并确保音频满足音频文件格式要求（如Qwen-TTS）或为 WAV（16bit）、MP3 或 M4A（如CosyVoice）。

## 400-Audio.AudioRateError <span id="aea44d3ed5rzi" />

### File sample rate unsupported. <span id="925daf562ffvt" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻或CosyVoice声音复刻时，待复刻音频采样率不符合要求。

<strong>解决方案：</strong> 采样率需大于等于24000 Hz。

## 400-Audio.DurationLimitError <span id="6b20b432bb15i" />

### Audio duration exceeds maximum allowed limit. <span id="118cb1977au4e" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，待复刻音频过长。

<strong>解决方案：</strong> 音频不得超过60秒。

## 401- InvalidApiKey/invalid\_api\_key <span id="f0a70479b3nre" />

### Invalid API-key provided. / Incorrect API key provided. <span id="apikey_error" />

<strong>原因：</strong> API Key 填写错误。

<strong>解决方案：</strong> 常见错误原因及修正方式如下：

- <strong>读取错误的环境变量</strong>

  - <strong>错误写法</strong>：`api_key=os.getenv("sk-xxx")`，系统将尝试读取名为 `sk-xxx` 的环境变量，而非将 `sk-xxx` 当作密钥。
  - <strong>正确写法：</strong>

    - <strong>若已配置环境变量：</strong>请写为`api_key=os.getenv("DASHSCOPE_API_KEY")`；

      > 确保运行前已设置`DASHSCOPE_API_KEY`环境变量。
    - <strong>若未配置环境变量</strong>：请写为`api_key = "sk-xxx"`。

      > 此方式便于调试，请勿用于生产环境。
- <strong>填写错误</strong>：阿里云百炼的 API Key 以 `sk-` 开头，请确认未误填其他模型提供商的密钥，且复制时未包含多余空格或换行符。
- <strong>套餐专属 API Key（Coding Plan / Token Plan 团队版）</strong>：Coding Plan 与 Token Plan 团队版均提供以 `sk-sp-` 开头的专属 API Key，<strong>必须配合各自的专属 Base URL 使用</strong>，不可与通用 API Key/Base URL 混用（混用会返回本鉴权错误）。其中 Coding Plan 的专属地址为 <a href="https://coding.dashscope.aliyuncs.com/v1">[https://coding.dashscope.aliyuncs.com/v1](https://coding.dashscope.aliyuncs.com/v1)</a>；Token Plan 团队版的专属 Base URL 可在控制台<strong>我的订阅</strong>的 API Key 区域查看。请确认同时更新了 API Key 和 Base URL，具体配置方法请分别参见接入AI工具与[快速开始](/zh/model-studio/token-plan-team-quickstart)。
- <strong>地域不匹配</strong>：API Key 和 Base URL 属于不同的地域，例如使用了华北2（北京）地域的 API Key 和新加坡地域的 Base URL。请确认您使用的 API Key 位于[北京](https://bailian.console.aliyun.com/settings/api-key)地域页面或[新加坡](https://bailian.console.aliyun.com/settings/api-key)地域页面，或[美国](https://bailian.console.aliyun.com/us-east-1)地域页面。各地域对应的 Base URL 如下：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "16%" }} /><col style={{ width: "42%" }} /><col style={{ width: "42%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>OpenAI兼容</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>DashScope</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>[https://dashscope.aliyuncs.com/compatible-mode/v1](https://dashscope.aliyuncs.com/compatible-mode/v1)</code></p></td><td style={{ verticalAlign: "top" }}><p><code>[https://dashscope.aliyuncs.com/api/v1](https://dashscope.aliyuncs.com/api/v1)</code></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>新加坡</p></td><td style={{ verticalAlign: "top" }}><p><code>https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1</code></p></td><td style={{ verticalAlign: "top" }}><p><code>https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1</code></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>美国（弗吉尼亚）</p></td><td style={{ verticalAlign: "top" }}><p><code>https\://\{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1</code></p></td><td style={{ verticalAlign: "top" }}><p><code>https\://\{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1</code></p></td></tr></tbody></table>

  <Tip>
    阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

    - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
    - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

    其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
  </Tip>
- <strong>工具适配问题</strong>：第三方工具未正确适配（如[Dify](/zh/model-studio/dify)最新版本插件不稳定导致报错，可尝试安装旧版本千问插件；旧版本[Cline](/zh/model-studio/cline)调用模型时<strong>API Provider</strong>选择了<strong>Alibaba Qwen</strong>，应选择<strong>OpenAI兼容</strong>）。

若以上均不符合，可能是 API Key 被删除，请重新获取并发起调用。

## 401-NOT AUTHORIZED <span id="f9c2421d8953a" />

### Access denied: Either you are not authorized to access this workspace, or the workspace does not exist. Please:\nVerify the workspace configuration.\nCheck your API endpoint settings. Ensure you are targeting the correct environment. <span id="45c72b5db8rqa" />

<strong>原因：</strong>

- \{WorkspaceId}值无效，或当前账号不是该业务空间的成员。
- 或者请求的接入地址（服务接入点）有误。

<strong>解决方案：</strong>

- 请确认\{WorkspaceId}值无误且账号已是该业务空间的成员后，再调用接口。
- 中国站用户请使用<strong>华北2（北京）地域的接入地址；国际站用户请使用新加坡</strong>地域的接入地址。使用[在线调试](https://api.aliyun.com/api/bailian/2023-12-29/CreateIndex)时，确认服务地址正确。

  ![image](https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7513215871/p1090718.png)

## 401-invalid access token or token expired <span id="a3f7b2c8d1token" />

### invalid access token or token expired. <span id="b4e8c3d9f2tokenmsg" />

<strong>可能原因：</strong> Token Plan 误用了 Coding Plan 或其他套餐的 Base URL。

<strong>解决方案：</strong> 请使用 Token Plan 专属的 Base URL：

- Anthropic 兼容端点：`https://token-plan.cn-beijing.maas.aliyuncs.com/apps/anthropic`
- OpenAI 兼容端点：`https://token-plan.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

## 403- AccessDenied/access\_denied <span id="f6436bf1edmmz" />

### Current user api does not support asynchronous calls. <span id="65b6af3dafigq" />

<strong>原因：</strong> 接口不支持异步调用。

<strong>解决方案：</strong> 请移除请求头中的 `X-DashScope-Async`，或将其值设为 `disable`。

### current user api does not support synchronous calls. <span id="e1b9c181334xz" />

<strong>原因：</strong> 接口不支持同步调用。

<strong>解决方案：</strong> 请在请求头中设置 `X-DashScope-Async: enable`。

### Invalid according to Policy: Policy expired. <span id="aa6297294cqip" />

<strong>原因：</strong> 在获取临时公网URL时，文件上传凭证已经过期。

<strong>解决方案：</strong> 请重新调用[文件上传凭证接口](/zh/model-studio/get-temporary-file-url#32db94982cllx)生成新凭证。

### Access denied. <span id="access_denied" />

<strong>原因：</strong> 无权访问此模型。可能因该模型需申请权限、模型免费额度已耗尽且不支持付费使用（如 `deepseek-r1-distill-llama-70b`），或该模型已下线。

<strong>解决方案：</strong> 若该模型已下线，请在[模型下线机制说明](/zh/model-studio/model-depreciation)中确认其下线状态，并改用该文档推荐的替代模型。

<strong>使用通用 API Key 调用 Token Plan 专属模型</strong>

<strong>原因：</strong> Token Plan 专属模型（如 `qwen3.8-max-preview`）仅限 Token Plan 订阅用户使用专属 API Key（以 `sk-sp-` 开头）配合 Token Plan 专属 Base URL 调用。使用通用 API Key（以 `sk-` 开头）调用这些模型会返回 403 `access_denied` 错误。

<strong>解决方案：</strong> 请前往阿里云百炼控制台，在<strong>我的订阅</strong>的 API Key 区域获取 Token Plan 专属 API Key 与专属 Base URL，并确认同时更新了两者，具体配置方法请参见[快速开始](/zh/model-studio/token-plan-team-quickstart)。若 API Key 与 Base URL 不匹配（例如专属 API Key 搭配通用 Base URL），将返回 401 鉴权错误，请参见本文档 <strong>401-InvalidApiKey/invalid\_api\_key</strong>。

<strong>解决方案：</strong> 请前往阿里云百炼控制台，在[模型广场](https://bailian.console.aliyun.com/model/market)的对应模型卡片下方单击<strong>立即申请</strong>发起测试申请。或改用其他模型，例如千问或万相的文生图模型替代 Flux。

## 403- AccessDenied.Unpurchased <span id="d39f809e7ajgg" />

### Access to model denied. Please make sure you are eligible for using the model. <span id="service-not-enabled" />

<strong>原因：</strong>

- 未开通阿里云百炼服务。

<strong>解决方案：</strong> 请参照以下流程开通阿里云百炼服务。

1. <strong>注册账号</strong>：如果没有阿里云账号，您需要先[注册](https://account.aliyun.com/register/qr_register.htm?oauth_callback=https%3A%2F%2Fbailian.console.aliyun.com%2F%3FapiKey%3D1)阿里云账号。
2. <strong>开通阿里云百炼：</strong>使用<strong>阿里云主账号</strong>前往[阿里云百炼大模型服务平台](https://bailian.console.aliyun.com/)，阅读并同意协议后，将自动开通阿里云百炼，如果未弹出服务协议，则表示您已经开通。

   > 如果开通服务时提示“您尚未进行实名认证”，请先进行[实名认证](https://help.aliyun.com/zh/account/verify-your-identity-individual-account)。

## 403- Model.AccessDenied <span id="94a2e01649ylu" />

### Model access denied. <span id="model-access-denied" />

<strong>原因：</strong> 无权限调用对应的标准模型或自定义模型。

<strong>解决方案：</strong>

- <strong>调用标准模型</strong>：使用子业务空间的API-KEY调用标准模型（例如`qwen-plus`）时，子业务空间需具备该模型的调用权限。详见[模型调用授权](/zh/model-studio/use-workspace)。
- <strong>调用自定义模型</strong>：自定义模型部署成功后，仅能用其所在业务空间的API-KEY调用，且无需模型调用授权。

## 403- App.AccessDenied <span id="d4270f908657q" />

### App access denied. <span id="a276721df6ze0" />

<strong>原因：</strong> 无权限访问应用或者模型。

<strong>解决方案：</strong>

- 仔细确认对访问的业务空间和子账号做了访问授权。
- 仔细检查应用是否发布。
- 仔细核实传入的APP ID、API KEY是否正确。
- 如果是Claude Code报错，请使用默认业务空间的API Key。
- 若上述建议都正确，建议刷新数据重新发布再调用，或尝试重新创建智能体。

## 403- Workspace.AccessDenied <span id="526f4a5439j5c" />

### Workspace access denied. <span id="a9c8f3d338mtf" />

<strong>原因：</strong> 无权限访问业务空间的应用或者模型。

<strong>解决方案：</strong>

- 如果调用子业务空间的应用，请参考[业务空间](/zh/model-studio/call-rag-application-through-api-old#78c7ddbcca64f)。
- 如果调用子业务空间的模型，请参考[子业务空间的模型调用](/zh/model-studio/model-calling-in-sub-workspace)。
- 也可改为使用主账号的API KEY，主账号具有所有业务空间的权限。

## 403- Endpoint.AccessDenied <span id="endpoint-ad-h2" />

### Workspace endpoint access denied. <span id="endpoint-access-denied" />

<strong>原因：</strong> 可能调用了已下线的模型（例如`qwen-max-2025-01-25`等历史快照版本）。模型下线后，对应的调用端点不再提供服务，会返回该报错。

<strong>解决方案：</strong>

- 前往[模型下线机制说明](/zh/model-studio/model-depreciation)确认所调用的模型是否已下线。
- 若模型已下线，请改用其推荐的替代模型重新调用。

## 403-AllocationQuota.FreeTierOnly <span id="a9d20b11c0obu" />

### The free tier of the model has been exhausted. If you wish to continue access the model on a paid basis, please disable the "use free tier only" mode in the management console. <span id="free-tier-exhausted" />

<strong>原因一</strong>：全新未认证用户免费额度用尽后发起请求。

<strong>解决方案</strong>：[认证](https://myaccount.console.aliyun.com/cert-info)并充值后再进行调用。

<strong>原因二</strong>：用户开启了[免费额度用完即停](/zh/model-studio/new-free-quota#d1cb80ac11i92)，且免费额度耗尽后发起请求。

> 控制台显示的免费额度为分钟级更新（需手动刷新页面）。

<strong>解决方案</strong>：

- 如需继续以付费方式调用，可随时关闭[免费额度用完即停](/zh/model-studio/new-free-quota#d1cb80ac11i92)开关，无需等到免费额度用完；关闭后即可按量付费继续调用。
- 若在使用 Coding Plan 时遇到此问题，通常是配置错误所致。Coding Plan 需要配置专属的 Base URL 和 [API Key](https://bailian.console.aliyun.com/cn-beijing/subscription/coding-plan)，详情请参见[Coding Plan快速开始](/zh/model-studio/coding-plan-quickstart)。
- 关闭开关后仍报错，或控制台显示已有额度但调用仍被拒绝时，请检查 <strong>API Key</strong> 是否发生变动或失效，建议重置 API Key 或新建 Key 后重新测试。
- 若控制台的调用记录中没有对应请求，可能是请求未到达服务端，请检查客户端插件配置或本地网络连接是否正常。

## 404- ModelNotFound/ model\_not\_found <span id="8f3300113cbrz" />

### The provided model xxx is not supported by the Batch API. <span id="6ef4755005yut" />

<strong>原因：</strong> 当前模型暂不支持 Batch 调用，或者可能存在模型名称拼写错误。

<strong>解决方案：</strong> 请参考[OpenAI兼容-Batch（文件输入）](/zh/model-studio/batch-interfaces-compatible-with-openai)，确认支持 Batch调用的模型及其正确名称。

### Model can not be found. / The model xxx does not exist. / The model xxx does not exist or you do not have access to it. <span id="30d9dfb7f82dh" />

<strong>原因：</strong> 当前访问的模型不存在，或您还未开通阿里云百炼服务。

<strong>解决方案：</strong>

- 请对照[模型列表](/zh/model-studio/models)中的模型名称，检查您输入的模型名称（参数`model`的取值）是否正确。
- 请前往[模型广场](https://bailian.console.aliyun.com/model/market)开通模型服务。
- 如果您通过国际站 API 端点（如 `{WorkspaceId}.us-east-1.maas.aliyuncs.com`）发起调用，请注意不同地域可用的模型列表不同。调用前请确认目标模型是否在该地域可用。
- 如果您使用<strong>专属部署端点</strong>调用模型，出现 ModelNotFound 时还需检查以下两点：

  1. <strong>确认使用部署名称</strong>：专属部署端点的 `model` 参数应填写部署名称（model code），而非模型名称。部署名称可在百炼控制台的<strong>专属部署</strong>页面查看，格式类似 `qwen3.5-flash-2026-02-23-9439a8e1eafa`。使用模型名称调用专属部署端点，或在标准 API 端点上使用部署名称，均会返回 `model_not_found`。
  2. <strong>区分调用方式</strong>：通用调用使用标准 API 端点和模型名称，专属部署调用使用专属部署端点 URL 和部署名称，两者不可混用。通用调用与专属部署调用的 API-KEY 也可能不同，混用会导致 `invalid_api_key` 认证错误。

## 404- model\_not\_supported <span id="fc28915aee15i" />

### Unsupported model xxx for OpenAI compatibility mode. <span id="fed580e231sl6" />

<strong>原因：</strong> 当前模型不支持以 OpenAI 兼容方式接入。

<strong>解决方案：</strong> 请您使用DashScope原生方式调用。

## 404-WorkSpaceNotFound <span id="f90c423862gfc" />

### WorkSpace can not be found. <span id="e6f1849a80fct" />

<strong>原因：</strong> 工作空间不存在。

<strong>解决方案：</strong>请确认 Base URL 中填写的 \{WorkspaceId} 正确。\{WorkspaceId} 为您的百炼业务空间 ID，可在[业务空间管理](https://bailian.console.aliyun.com/settings/workspace)页面查看。如仍报错，请确认您的账号是该业务空间的成员。

## 404-NotFound <span id="d9beba50faz5m" />

### Not found! <span id="8d96fac71dv3x" />

<strong>原因：</strong>

- 要查询/操作的资源不存在。
- 使用定制热词时，传入的热词ID无效或对应热词不存在。

<strong>解决方案：</strong>

- 请检查要查询/操作的资源ID是否错误。
- 检查热词ID是否正确并参照API文档按照正确的方式进行调用。

### Request path not found. <span id="e3f7a4b8c5w8u" />

<strong>原因：</strong> 使用Fun-Music模型时，访问的服务地址不存在。

<strong>解决方案：</strong> 请检查接口路径是否正确及是否存在异常字符。

## 409- Conflict <span id="eb97278096enw" />

### Model instance xxx already exists, please specify a suffix. <span id="effbdef3f9p2u" />

<strong>原因：</strong> 已存在重名的部署实例。

<strong>解决方案：</strong>为部署的模型指定不同的后缀名。

## 429-Throttling <span id="5ca0afb383zeh" />

### Requests throttling triggered. <span id="86886acaa2hce" />

<strong>原因：</strong> 接口调用触发限流。

<strong>解决方案：</strong> 请降低调用频率或稍后重试。

### Too many fine-tune job in running, please retry later. / Only 20 fine-tune job in running or succeeded allowed per user. <span id="d2b66090a4yee" />

<strong>原因：</strong> 资源的创建触发平台限制。

<strong>解决方案：</strong> 可以删除不再使用的模型。如需提高并发量或保留更多模型，请联系商务经理进行申请提额。

### Too many requests in route. Please try again later. <span id="f8f289b6f1f7t" />

<strong>原因</strong>：请求过多触发限流。

<strong>解决方案</strong>：请稍后重试。

### All models are temporarily rate-limited. Please try again in a few minutes. <span id="all-models-rate-limited" />

<strong>原因：</strong> 当使用 Coding Plan 等通过客户端（如 Claude Code）调用多个模型的场景时，若所有可用模型均触发 429 限流，客户端会聚合返回此提示。底层对应的百炼服务端错误码为 Throttling.RateQuota 或 Throttling.AllocationQuota。

<strong>解决方案：</strong>

- 等待数分钟后重试，限流会自动解除。
- 降低请求并发数，避免短时间内发送过多请求。
- 如需更高调用频率，可参考[限流](/zh/model-studio/rate-limit)申请提额。

## 429- Throttling.RateQuota/LimitRequests/limit\_requests/ResourceExhausted/Too many requests <span id="5ed5532c85ckv" />

### You have exceeded your request limit./Requests rate limit exceeded, please try again later. /You exceeded your current requests list. <span id="rate_limit" />

<strong>原因：</strong> 调用频率（RPS/RPM）触发限流。

<strong>解决方案：</strong> 请参考[限流](/zh/model-studio/rate-limit)，控制调用频率。

<strong>说明：</strong> 若单次调用即返回 429，请检查请求头是否设置了 `X-DashScope-Async: enable`。部分模型（如 `qwen-image-3.0-pro`）不支持异步调用，设置该请求头时即使单次请求也会返回 429。请移除 `X-DashScope-Async` 请求头，或将其值设为 `disable`，然后使用同步调用重试。详情请参见本文档中「<strong>403-</strong>AccessDenied/access\_denied」下「<strong>Current user api does not support asynchronous calls.</strong>」条目。

## 429- Throttling.BurstRate/limit\_burst\_rate <span id="6d23103c75skz" />

### Request rate increased too quickly. To ensure system stability, please adjust your client logic to scale requests more smoothly over time. <span id="ab8a2d94ec57g" />

<strong>原因</strong>：在未达到限流条件时，调用频率骤增，触发系统稳定性保护机制。

<strong>解决方案</strong>：建议优化客户端调用逻辑，采用平滑请求策略（如匀速调度、指数退避或请求队列缓冲），将请求均匀分散在时间窗口内，避免瞬时高峰。

## 429-Throttling.AllocationQuota/insufficient\_quota <span id="02f52a91cb53j" />

### Allocated quota exceeded, please increase your quota limit./ You exceeded your current quota, please check your plan and billing details. <span id="token_limit" />

<strong>原因：</strong> 每秒钟或每分钟消耗Token数（TPS/TPM）触发限流。

<strong>解决方案：</strong> 前往[限流](/zh/model-studio/rate-limit)文档查看模型限流条件并调整调用策略；如果默认额度无法满足业务需求，请联系商务经理申请提额。

> 可参考[FAQ](/zh/model-studio/rate-limit#d1688da3e3egs)避免触发限流。

### Too many requests. Batch requests are being throttled due to system capacity limits. Please try again later. <span id="ad754f35c4dj2" />

<strong>原因：</strong> Batch请求过多触发限流。

<strong>解决方案：</strong> 暂时无法处理您的请求，请稍后再进行重试。

### Free allocated quota exceeded. <span id="2c77a3ab9244i" />

<strong>原因：</strong> 免费额度已到期或耗尽，且该模型暂不支持按量计费。

<strong>解决方案：</strong> 使用其它模型替换，例如：千问Audio模型额度耗尽，可使用[非实时（Qwen-Omni）](/zh/model-studio/qwen-omni)模型。

### Maximum voice-clone voice limit exceeded. <span id="fef6888787hzq" />

<strong>原因：</strong> 使用Qwen-TTS声音复刻时，超过主账号可用的音色数目上限。

<strong>解决方案：</strong> 请[删除](/zh/model-studio/qwen-tts-voice-cloning)一部分音色。

## 429-Throttling.Concurrency <span id="throttling-concurrency-h2" />

### Too many concurrent requests. <span id="throttling-concurrency-h3" />

<strong>原因：</strong> 当前并发请求数超出平台动态分配的上限。

<strong>解决方案：</strong> 等待片刻后重试即可。平台会根据整体资源负载动态调整并发上限，高峰时段可能触发此限制。

## 429-Throttling.ServiceOverloaded <span id="throttling-service-overloaded" />

### InternalError.Algo: An error occurred in model serving, error message is: \[Too many requests.] <span id="throttling-service-overloaded-msg" />

<strong>原因：</strong> 网络资源目前处于过载状态，暂时无法处理您的请求。

<strong>解决方案：</strong> 请稍后再试。

## 429-Throttling.ResourceExhausted <span id="throttling-resource-exhausted" />

### InternalError.Algo: An error occurred in model serving, error message is: \[Too many requests.] <span id="throttling-resource-exhausted-msg" />

<strong>原因：</strong> 网络资源目前处于饱和状态，暂时无法处理您的请求。

<strong>解决方案：</strong> 请稍后再试。

## 429-CommodityNotPurchased <span id="21660e7f18lo0" />

### Commodity has not purchased yet. <span id="5b71f88164esm" />

<strong>原因：</strong> 业务空间未订购。

<strong>解决方案：</strong> 请先订购业务空间服务。

## 429-PrepaidBillOverdue <span id="a6077d9e71ljf" />

### The prepaid bill is overdue. <span id="4884b2a2c3xem" />

<strong>原因：</strong> 业务空间预付费账单到期。

## 429-PostpaidBillOverdue <span id="187530dff3ojs" />

### The postpaid bill is overdue. <span id="23bb8a9992qix" />

<strong>原因：</strong> 模型推理商品已失效。

## 429-BudgetLimitExceeded <span id="budget-exhausted" />

### The budget configured in Budget Management has been exhausted. You will be unable to make further API calls until the budget limit is increased or reset. <span id="budget-exhausted-msg" />

<strong>原因：</strong> 您在[预算管理](/zh/model-studio/budget-management)中设置的预算额度已用尽，且开启了「达预算即停」。

<strong>解决方案：</strong> 在[预算管理](https://bailian.console.aliyun.com/cn-beijing/costing-balance/budget)页面提高预算额度或关闭「达预算即停」，即可恢复调用。

<Warning>
  服务停止存在一定延迟，延迟时段内产生的费仍将正常收取。若已达预算阈值触发停机后，又发放了免费额度，停机不会自动恢复，需手动调整预算阈值。
</Warning>

## 430-Audio.DecoderError <span id="af8063ee6cz1q" />

### Decoder audio file failed. <span id="a074390f8decc" />

<strong>原因</strong>：[CosyVoice声音复刻](/zh/model-studio/voice-replica-1)音频文件解码失败。

<strong>解决方案</strong>：建议通过工具（如ffprobe、mediainfo）或命令（如Linux/macOS的file命令）确认音频文件的实际编码格式，以确保符合要求。

## 430-Audio.FileSizeExceed <span id="33fbadd9e3lk0" />

### File too large <span id="724c82d84eqig" />

<strong>原因：</strong> [CosyVoice声音复刻](/zh/model-studio/voice-replica-1)音频文件大小超限。

<strong>解决方案：</strong> 用于声音复刻的音频文件需10M以内。

## 430-Audio.AudioRateError <span id="077dc8bd08klb" />

### File sample rate unsupported <span id="3ebbf2edc9tz5" />

<strong>原因：</strong> [CosyVoice声音复刻](/zh/model-studio/voice-replica-1)音频文件采样率不支持。

<strong>解决方案：</strong> 采样率设置为16KHz及以上。

## 430-Audio.AudioSilentError <span id="723c061282ofd" />

### Silent file unsupported. <span id="b5f7b12fd0ela" />

<strong>原因：</strong>[CosyVoice声音复刻](/zh/model-studio/voice-replica-1)音频文件为静音或非静音长度过短。

<strong>解决方案：</strong> 音频时长应尽量控制在 10\~15 秒之间，并包含至少一段超过 5 秒的连续语音。

## 500-InternalError/internal\_error <span id="bd2637cca0gx8" />

### An internal error has occured, please try again later or contact service support. <span id="6a5edac3397ed" />

<strong>原因：</strong> 内部错误。

<strong>解决方案：</strong>

- 如果您使用[（Qwen-Omni）模型](/zh/model-studio/qwen-omni)，且所用接口要求流式输出，请设置 `stream=true`。
- 如果您使用[CosyVoice声音复刻](/zh/model-studio/voice-replica-1)，则可能的原因是：

  - 音频文件不规范，比如声音本身有问题，有杂音或者声音忽高忽低。请参见[录音操作指南](/zh/model-studio/recording-guide)录音后重试。
  - 录音文件URL无法访问，请按照[CosyVoice声音复刻/设计API](/zh/model-studio/cosyvoice-clone-design-api)中的说明操作后重试。
  - 录音文件时长过长。尽量选择10\~15秒的录音。录音时请确保朗读连贯，并包含至少一段超过 5 秒的连续语音。

### Internal server error! <span id="070f11f6b8g7l" />

<strong>原因：</strong> 内部算法错误。

<strong>解决方案：</strong> 请稍后重试。

### audio preprocess server error <span id="3c38cea088hsh" />

使用[CosyVoice声音复刻](/zh/model-studio/voice-replica-1)：

- <strong>原因：</strong>音频文件不规范，比如声音本身有问题，有杂音或者声音忽高忽低。

  <strong>解决方案</strong>：请参见[录音操作指南](/zh/model-studio/recording-guide)录音后重试。
- <strong>原因</strong>：录音文件URL无法访问，

  <strong>解决方案</strong>：请按照[CosyVoice声音复刻/设计API](/zh/model-studio/cosyvoice-clone-design-api)中的说明操作后重试。
- <strong>原因</strong>：录音文件时长过长。

  <strong>解决方案</strong>：尽量选择10\~15秒的录音。录音时请确保朗读连贯，并包含至少一段超过 5 秒的连续语音。

### request asr failed <span id="febd7a6a526lu" />

<strong>原因：</strong>使用[CosyVoice声音复刻](/zh/model-studio/voice-replica-1)时，音频文件不规范，未包含有效人声或声音不清晰，杂音大。

<strong>解决方案</strong>：请参见[录音操作指南](/zh/model-studio/recording-guide)录音后重试。

### Remote cancelled grpc stream. <span id="b4e2a1c7d9rcs" />

<strong>原因：</strong> 语音合成中使用的音色不存在。

<strong>解决方案：</strong> 请检查`voice`参数，确保指定了正确的音色名称。可用音色请参见[实时语音合成（Qwen-Audio-TTS/CosyVoice）](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)。

## 500- InternalError.FileUpload <span id="138a10424ew8f" />

### oss upload error. <span id="db9bd442198tm" />

<strong>原因：</strong> 文件上传失败。

<strong>解决方案：</strong> 请检查OSS配置和网络。

## 500- InternalError.Upload <span id="2121a68cf3myr" />

### Failed to upload result. <span id="06735d277azf7" />

<strong>原因：</strong> 生成结果上传失败。

<strong>解决方案：</strong> 请检查存储配置或稍后重试。

## 500- InternalError.Algo <span id="67efd50170pur" />

### inference internal error. <span id="6f02ee53cc185" />

<strong>原因：</strong> 服务异常。

<strong>解决方案：</strong> 请先尝试重试，排除偶发情况。

### Expecting ',' delimiter: line x column xxx (char xxx) <span id="7701da53dex3y" />

<strong>原因：</strong> 模型生成的JSON数据不合法，无法正常发起工具调用。

<strong>解决方案：</strong> 建议更换最新的模型或优化提示词后重试。

### Missing Content-Length of multimodal url. <span id="1cdd8405c6l7m" />

<strong>原因：</strong> URL请求的响应头信息缺失`Content-Length`字段。

<strong>解决方案：</strong> 如果问题无法解决，请尝试使用其他图片链接。

<Accordion title="查看 Content-Length 字段">
  1. 打开浏览器（如Chrome或Firefox）。
  2. 打开开发者工具（通常按F12或右键选择“检查”）。
  3. 切换到Network标签。
  4. 将图片的URL输入到地址栏并访问。
  5. 找到对应的请求，查看Headers部分，在“响应头”（Response Headers）部分中查找Content-Length字段
</Accordion>

### An error occurred in model serving, error message is: \[Request rejected by inference engine!] <span id="954d7ba104zwb" />

<strong>原因：</strong> 模型服务底层服务器出现错误。

<strong>解决方案：</strong> 请稍后重试。

### An internal error has occured during algorithm execution. <span id="207c14c28c7ws" />

<strong>原因：</strong> 算法运行时发生错误。

<strong>解决方案：</strong> 请稍后重试。

### Inference error: Inference error. <span id="e3af31c4beikv" />

<strong>原因：</strong> 推理发生错误。

<strong>解决方案：</strong> 请检查输入的图片文件是否有损坏或检查人物图片的质量（需包含完整清晰的人脸）。

### Role must be in \[user, assistant] <span id="2f8c7576afnj6" />

<strong>原因：</strong> 在使用Qwen-MT模型时，messages数组中包含了非 `user`角色的消息。

<strong>解决方案：</strong> 请确保messages数组中仅包含一个元素，且该元素必须是用户消息（User Message）。

### Embedding\_pipeline\_Error: xxx <span id="f8c84e45c65r1" />

<strong>原因：</strong> 图像或视频预处理出错。

<strong>解决方案：</strong> 请确认上传的图片或视频及请求代码符合要求后重试。

### Receive batching backend response failed! <span id="a55b6c2005gf7" />

<strong>原因：</strong> 服务内部错误。

<strong>解决方案：</strong> 请稍后重试。

### \[music]Receive batching backend response failed! <span id="f0a4b1c5d2d5b" />

<strong>原因：</strong> 使用Fun-Music模型时，服务超出并发限制。

<strong>解决方案：</strong> 请降低并发请求数量后重试。

### Other kinds of server error. <span id="c3d7e4f8a5a8e" />

<strong>原因：</strong> 使用Fun-Music模型时，系统内部未知异常。

<strong>解决方案：</strong> 建议提供请求ID给技术人员排查。

### An internal error has occured during execution, please try again later or contact service support. / algorithm process error. / inference error. / An internal error occurs during computation, please try this model later. <span id="9e36b3761e59v" />

<strong>原因：</strong> 内部算法错误。

<strong>解决方案：</strong> 请稍后重试。

### list index out of range <span id="0e6937c788fpv" />

<strong>原因：</strong> messages 数组最后一位需为 User Message。

<strong>解决方案：</strong> 请调整`messages`数组的顺序，确保最后一个元素是 `{"role": "user", ...}`。

## 500- InternalError.Timeout <span id="1eb7e256d83v3" />

### An internal timeout error has occured during execution, please try again later or contact service support. <span id="c5e29fcd28pep" />

<strong>原因：</strong> 异步任务提交后，在3小时内未返回结果，导致超时。

<strong>解决方案：</strong> 请检查任务执行情况，或联系技术支持。

## 500- InternalError.Configuration <span id="internal-configuration-h2" />

### Internal configuration error. <span id="internal-configuration-h3" />

<strong>原因：</strong> 服务端配置异常。

<strong>解决方案：</strong> 请稍后重试，或联系技术支持。

## 500- InternalError.DataInspection <span id="internal-data-inspection-h2" />

### Content moderation service unavailable. <span id="internal-data-inspection-h3" />

<strong>原因：</strong> 内容审核服务暂时不可用。

<strong>解决方案：</strong> 请稍后重试。

## 500- InternalError.TranslationFailed <span id="internal-translation-failed-h2" />

### Translation failed. <span id="internal-translation-failed-h3" />

<strong>原因：</strong> 翻译处理失败。

<strong>解决方案：</strong> 请稍后重试；如仍失败，请检查输入内容或联系技术支持。

## 500- SystemError <span id="50347c2acaf8t" />

### An system error has occured, please try again later. <span id="3e50e0f48doim" />

<strong>原因：</strong> 系统错误。

<strong>解决方案：</strong> 请稍后重试。若您使用[调用百炼应用](/zh/model-studio/spring-ai-alibaba-integrate-llm-application)，请参照示例代码或说明文档，查看是否代码编写有误，若依然无法确定问题，加入[Spring AI Alibaba官网](https://java2ai.com/?spm=4347728f.638c0b20.0.0.23f87982NTcSMy)最下方提供的DING群，联系开发人员进行定位。

## 500- ModelServiceFailed <span id="2a0b5c68fe7wl" />

### Failed to request model service. <span id="387f609641g32" />

<strong>原因：</strong> 模型服务调用失败。

<strong>解决方案：</strong> 请稍后重试。

## 500- RequestTimeOut <span id="4b8a2c0eddbn6" />

### Request timed out, please try again later. / Response timeout! / I/O error on POST request for "<a href="https://dashscope.aliyuncs.com/compatible-mode/v1/chat/completions">[https://dashscope.aliyuncs.com/compatible-mode/v1/chat/completions](https://dashscope.aliyuncs.com/compatible-mode/v1/chat/completions)</a>": timeout <span id="response-time-out" />

<strong>原因：</strong>

- 调用大模型时请求超时，超时报错时间为300秒。
- 使用语音识别（Paraformer）时，长时间未向服务器发送音频或者长时间发送静音音频。
- 调用图像生成或编辑模型时，因图片尺寸较大或处理复杂度较高，处理时间超出限制。

<strong>解决方案：</strong>

- 通过流式输出方式发起请求，具体操作请参见[流式输出](/zh/model-studio/stream)。
- 将请求参数`heartbeat`设为`true`或及时结束识别任务。
- 调用图像模型时，可尝试降低图片分辨率、简化编辑需求，或稍后重试。

> 调用千问模型时，响应体中会将已生成的内容返回，不再报超时错误。详情请参见[文本生成](/zh/model-studio/text-generation#11241147efwpm)。

## 500-ResponseTimeout <span id="b6c0d7e1f8z1x" />

### Response stream timeout <span id="c7d1e8f2a9a2y" />

<strong>原因：</strong> 使用Fun-Music模型时，内部执行超时。

<strong>解决方案：</strong> 建议进行重试调用。

## 500-InvokePluginFailed <span id="798a834394yx9" />

### Failed to invoke plugin. <span id="fd04ab20262zm" />

<strong>原因：</strong> 插件调用失败。

<strong>解决方案：</strong> 请检查插件配置和可用性。

## 500-AppProcessFailed <span id="ec69a32d2bkut" />

### Failed to proceed application request. <span id="9fe3b91164ads" />

<strong>原因：</strong> 应用流程处理失败。

<strong>解决方案：</strong> 请检查应用配置和流程节点。

## 500-RewriteFailed <span id="8bd8508b625me" />

### Failed to rewrite content for prompt. <span id="260bd75c37rnk" />

<strong>原因：</strong> 调用改写prompt的大模型失败。

<strong>解决方案：</strong> 请稍后重试。

## 500-RetrivalFailed <span id="24da98fcb7uil" />

### Failed to retrieve data from documents. <span id="51d8b6e392t7f" />

<strong>原因：</strong> 文档检索失败。

<strong>解决方案：</strong> 请检查文档索引和检索配置。

## 500/503-ServiceUnavailable <span id="16863964ceyeb" />

### Too many requests. Your requests are being throttled due to system capacity limits. Please try again later. <span id="system_limit" />

<strong>原因：</strong> 网络资源目前处于饱和状态，暂时无法处理您的请求。

<strong>解决方案：</strong> 请稍后再进行尝试。

## 503- ModelUnavailable <span id="6ed2afd626wdv" />

### Model is unavailable, please try again later. <span id="0f85dfe140sfm" />

<strong>原因：</strong> 模型暂时无法提供服务。

<strong>解决方案：</strong> 请稍后重试。

## 504- GatewayTimeout.InputDownload <span id="gateway-timeout-input-download-h2" />

### Input file download timed out. <span id="gateway-timeout-input-download-h3" />

<strong>原因：</strong> 服务端下载 `fileUrl` 指向的输入文件超时。

<strong>解决方案：</strong> 请检查 URL 可访问性、文件大小及所在服务的网络状况，或改用更稳定的下载地址后重试。

## SDK 报错 <span id="5f63cd02d0ygl" />

### error.AuthenticationError: No api key provided. You can set by dashscope.api\_key = your\_api\_key in code, or you can set it via environment variable DASHSCOPE\_API\_KEY= your\_api\_key. <span id="4b998b64d8vhc" />

<strong>原因：</strong> 使用DashScope SDK 时未提供API Key。

<strong>解决方案：</strong> 具体配置API Key的方法，请参见[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

### openai.OpenAIError: The api\_key client option must be set either by passing api\_key to the client or by setting the OPENAI\_API\_KEY environment variable <span id="df212e38aaq4v" />

<strong>原因：</strong> 使用 OpenAI SDK 时未传入 API Key。

<strong>解决方案：</strong>

- <strong>通过环境变量传入 API Key 来源（推荐）</strong>

  将`DASHSCOPE_API_KEY`设为环境变量（参见[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)），初始化`client`时，通过`os.getenv`读取：

  `client = OpenAI(api_key=os.getenv("DASHSCOPE_API_KEY"),...)`
- <strong>明文传入 API Key（仅限测试）</strong>

  直接将 API Key 传入`api_key`参数：

  `client = OpenAI(api_key="sk-...", ...)`

  <strong>注意：</strong>此方法存在安全风险，请勿用于生产环境。

### Bad Request for url:xxx <span id="a54eec659cape" />

<strong>原因：</strong> 使用 Python requests 库时，添加 `response.raise_for_status()`语句导致报错时不返回服务端的具体错误内容。

<strong>解决方案：</strong> 请用 `print(response.json())` 查看服务端返回信息。

### Cannot resolve symbol 'ttsv2' <span id="edb73511edpg7" />

<strong>原因：</strong> 若您使用[实时语音合成（Qwen-Audio-TTS/CosyVoice）](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)，出现该问题的原因是DashScope SDK版本过低。

<strong>解决方案：</strong> 请[安装最新版 DashScope SDK](/zh/model-studio/install-sdk)。

## NetworkError <span id="198e64a46ffuw" />

### NoApiKeyException: Can not find api-key. <span id="ab657028da885" />

<strong>原因：</strong> 环境变量配置没有生效。

<strong>解决方案：</strong> 您可以重启客户端或IDE后重试。更多情况请参考[常见问题](/zh/model-studio/configure-api-key-through-environment-variables)。

### ConnectException: Failed to connect to dashscope.aliyuncs.com <span id="e986475cfddz0" />

<strong>原因：</strong> 本地网络环境存在异常。

<strong>解决方案：</strong> 请检查本地网络，例如因证书问题导致无法访问 HTTPS，防火墙设置有误等情况。建议您更换网络环境或服务器进行测试。

### InputRequiredException: Parameter invalid: text is null <span id="22f1d818d4oc4" />

<strong>原因</strong>：使用[实时语音合成（Qwen-Audio-TTS/CosyVoice）](/zh/model-studio/cosyvoice-large-model-for-speech-synthesis)时未发送待合成文本。

<strong>解决方案：</strong>调用语音合成接口时为 `text` 参数赋值。

### MultiModalConversation.call() missing 1 required positional argument: 'messages' <span id="79e0084c4fvqj" />

<strong>原因：</strong>当前使用的DashScope SDK版本过低。

<strong>解决方案：</strong>请[安装最新版 DashScope SDK](/zh/model-studio/install-sdk)。

## mismatched\_model <span id="9eaf6d38bdirx" />

### The model 'xxx' for this request does not match the rest of the batch. Each batch must contain requests for a single model. <span id="bbd5cd7d62gts" />

<strong>原因：</strong> 在单个 Batch 任务中，所有请求都必须选用同一个模型。

<strong>解决方案：</strong> 请根据[OpenAI兼容-Batch（文件输入）](/zh/model-studio/batch-interfaces-compatible-with-openai)检查您的输入文件。

## duplicate\_custom\_id <span id="2918776353rep" />

### The custom\_id 'xxx' for this request is a duplicate of another request. The custom\_id parameter must be unique for each request in a batch. <span id="b39aecf7a5lbm" />

<strong>原因：</strong> 在单个 Batch 任务中，每条请求的 ID 必须唯一。

<strong>解决方案：</strong> 请根据[OpenAI兼容-Batch（文件输入）](/zh/model-studio/batch-interfaces-compatible-with-openai)检查您的输入文件，确保所有请求 ID 不重复。

### Upload file capacity exceed limit. / Upload file number exceed limit. <span id="2cf16e9641hvy" />

<strong>原因：</strong> 上传文件失败，当前阿里云账号下的阿里云百炼存储空间已满或接近满额。

<strong>解决方案：</strong> 可以通过[OpenAI兼容-File](/zh/model-studio/openai-file-interface)接口删除不需要的文件以释放空间。当前存储空间支持最大文件数为10000个，总量不超过100 GB。

## WebSocket 报错 <span id="48dfed8a3buwo" />

### Invalid payload data <span id="443ca522a8yxs" />

<strong>原因：</strong> 使用语音识别的WebSocket API，发送给服务端的JSON格式有误。

<strong>解决方案：</strong>

1. 检查发送`run-task`指令时，`payload`中是否有“`"input": {}`”，若无，请添加。
2. 确认在最终是否发送了完整的`finish-task`指令，且遵循其格式说明。请勿发送自创内容（如`{ "input": { "end_of_stream": true } }`）。

### The decoded text message was too big for the output buffer and the endpoint does not support partial messages <span id="1692216a0fq74" />

<strong>原因：</strong> 使用语音识别（Paraformer）的流式语音识别时，服务返回的识别结果数据量过大。

<strong>解决方案：</strong> 请分段发送待识别音频，建议每次发送的音频时长约为100毫秒，数据大小保持在1KB至16KB之间。

### TimeoutError: websocket connection could not established within 5s. Please check your network connection, firewall settings, or server status. <span id="2bbffc7566y0r" />

<strong>原因：</strong> 若您使用语音合成（CosyVoice），无法在5秒内建立websocket连接。

<strong>解决方案：</strong> 请检查本地网络、防火墙设置，或更换网络环境或服务器进行测试。

### unsupported audio format:xxx <span id="08d730e5b0yv5" />

<strong>原因：</strong> CosyVoice声音复刻时，上传的音频格式不符合模型要求。

<strong>解决方案：</strong> 音频格式需为 WAV（16bit）、MP3 或 M4A。请注意，不能仅凭文件后缀名判断格式，建议通过工具（如ffprobe、mediainfo）或命令（如Linux/macOS的file命令）确认音频文件的实际编码格式。

### internal unknown error <span id="16acd1c2c222f" />

<strong>原因：</strong> CosyVoice声音复刻音频文件格式可能不符合要求。

<strong>解决方案：</strong> 音频格式需为 WAV（16bit）、MP3 或 M4A。建议通过工具（如ffprobe、mediainfo）或命令确认音频文件的实际编码格式。

### Invalid backend response received (missing status name) <span id="76b1c23355kt1" />

<strong>原因：</strong> 使用语音识别（Paraformer）的录音文件识别的RESTful API时，请求参数拼写有误。

<strong>解决方案：</strong> 请参照API文档检查代码。

### NO\_INPUT\_AUDIO\_ERROR <span id="19b94c72fajam" />

<strong>原因：</strong> 未检测到有效语音。

<strong>解决方案：</strong> 若您使用语音识别（Paraformer）实时语音识别，请通过如下方式排查：

1. 检查是否有音频输入。
2. 检查音频格式是否正确（支持pcm、wav、mp3、opus、speex、aac、amr等）。

### SUCCESS\_WITH\_NO\_VALID\_FRAGMENT <span id="6e02fbcd28lzd" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，识别结果查询接口调用成功，但是VAD模块未检测到有效语音。

<strong>解决方案：</strong> 请排查录音文件是否包含有效语音，如果都是无效语音（例如纯静音），则没有识别结果是正常现象。

### ASR\_RESPONSE\_HAVE\_NO\_WORDS <span id="bd1d8d669ewjz" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，识别结果查询接口调用成功，但是最终识别结果为空。

<strong>解决方案：</strong> 请排查录音文件是否包含有效语音，或有效语音是否都是语气词且开启了顺滑参数`disfluency_removal_enabled`，导致语气词被过滤。

### FILE\_DOWNLOAD\_FAILED <span id="a96126552cz69" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，待识别文件下载失败。

<strong>解决方案：</strong> 请检查录音文件路径是否正确，以及是否可以通过外网访问和下载。

### FILE\_CHECK\_FAILED <span id="1e4ec7dd191q9" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，文件格式错误。

<strong>解决方案：</strong> 请检查录音文件是否是单轨/双轨的WAV格式或MP3格式。

### FILE\_TOO\_LARGE <span id="072211a7080gh" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，待识别文件过大。

<strong>解决方案：</strong> 请检查录音文件大小是否超过2GB，超过则需您对录音文件分段。

### FILE\_NORMALIZE\_FAILED <span id="a4be7d4fb8fwg" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，待识别文件归一化失败。

<strong>解决方案：</strong> 请检查录音文件是否有损坏，是否可以正常播放。

### FILE\_PARSE\_FAILED <span id="25e758c3d5fb7" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，文件解析失败。

<strong>解决方案：</strong> 请检查录音文件是否有损坏，是否可以正常播放。

### MKV\_PARSE\_FAILED <span id="287746c3f5l7x" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，MKV解析失败。

<strong>解决方案：</strong> 请检查录音文件是否损坏，是否可以正常播放。

### FILE\_TRANS\_TASK\_EXPIRED <span id="19063d730ebu4" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，录音文件识别任务过期。

<strong>解决方案：</strong> TaskId不存在，或者已过期。请重新提交任务。

### REQUEST\_INVALID\_FILE\_URL\_VALUE <span id="e2e32b45a4mtw" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，请求file\_link参数非法。

<strong>解决方案：</strong> 请确认`file_url`参数格式是否正确。

### CONTENT\_LENGTH\_CHECK\_FAILED <span id="19a3ccaec9rxq" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，`content-length`检查失败。

<strong>解决方案：</strong> 请检查下载待识别录音文件时，HTTP response中的`content-length`与文件实际大小是否一致。

### FILE\_404\_NOT\_FOUND <span id="e574897b87w7i" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，需要下载的文件不存在。

<strong>解决方案：</strong> 请检查文件URL是否正确。

### FILE\_403\_FORBIDDEN <span id="d82f1a72f2dok" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，没有权限下载待识别录音。

<strong>解决方案：</strong> 请检查文件访问权限。

### FILE\_SERVER\_ERROR <span id="1d050b1f621sg" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，请求的文件所在的服务不可用。

<strong>解决方案：</strong> 请稍后重试或检查文件服务器状态。

### AUDIO\_DURATION\_TOO\_LONG <span id="7413b323103ja" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，请求的文件时长超过12小时。

<strong>解决方案：</strong> 建议将音频进行切分，分多次提交识别任务。可使用FFmpeg等工具切分。

### DECODE\_ERROR <span id="787cf9e8f26pr" />

<strong>原因：</strong> 若您使用语音识别（Paraformer）录音文件识别，检测音频文件信息失败。

<strong>解决方案：</strong> 请确认文件下载链接中文件为支持的音频格式。

### CLIENT\_ERROR-\[qwen-tts:]Engine return error code: 411 <span id="fb8f998860y0r" />

<strong>原因：</strong> 在进行Qwen-TTS实时语音合成时，选用的模型是`qwen-tts-vc-realtime-2025-08-20`，但音色是默认音色。该模型仅支持复刻音色。

<strong>解决方案：</strong> 请使用通过声音复刻生成的音色，而非默认音色。

### NO\_VALID\_AUDIO\_ERROR <span id="f0fed5b7b4nue" />

<strong>原因：</strong> 使用语音识别（Paraformer）时，待识别音频无效。

<strong>解决方案：</strong> 请检查音频格式、采样率等是否满足要求。

### InvalidParameter: task can not be null <span id="c73ad195bbx3a" />

<strong>原因：</strong>使用CosyVoice语音合成WebSocket API时，run-task 指令或 finish-task 指令的 payload 中缺少 input 字段，或 continue-task 指令中缺少 input.text 字段。

<strong>解决方案：</strong>

1. 检查 run-task 指令：确保 payload 中包含 `"input": {}`（空对象），不可省略 input 字段。
2. 检查 continue-task 指令：确保 payload.input 中包含 text 字段，且 text 不为空字符串。
3. 检查 finish-task 指令：确保 payload 中包含 `"input": {}`。

### close code 1007 Model not found <span id="ws-close-1007-model-not-found" />

<strong>原因：</strong>通过 WebSocket 协议调用实时模型（如 Qwen3-Omni-Flash-Realtime）时，请求中的模型名称错误，或该模型服务尚未开通。服务端以 `close code 1007` 关闭连接，reason 为 `Model not found`。

<strong>解决方案：</strong>

- 请对照[模型列表](/zh/model-studio/models)中的模型名称，检查您输入的模型名称（参数 `model` 的取值）是否正确。实时模型的名称含 `-realtime` 等后缀，不可省略或改写。
- 请前往[模型广场](https://bailian.console.aliyun.com/model/market)确认该模型服务是否已开通；未开通时请先开通再重试。

## 200- BailianGateway.Workspace.NotAuthorised <span id="2d753c201eeyp" />

<strong>控制台出现错误信息“部分功能使用受限”。</strong>

<strong>原因：</strong>此报错可能由以下原因导致：（1）访问 URL 中包含了特殊字符或非标准格式，导致工作空间授权验证失败。（2）RAM 子账号操作没有权限的业务空间。

<strong>解决方案：</strong>（1）请重新访问百炼控制台首页，再导航到目标页面。（2）需要主账号或具有管理员权限的 RAM 账号为该子账号开通对应业务空间的权限。

## 200-BailianGateway.Team.NotAuthorised <span id="bailian_team_notauth_h2" />

<strong>原因：</strong>当前 RAM 子账号没有所访问团队（组织）的权限。当 RAM 子账号访问其未被授权的团队（组织）资源时，网关的团队级授权校验未通过，返回此报错。

<strong>解决方案：</strong>请主账号或具有管理员权限的 RAM 账号在权限管理中，将该 RAM 子账号添加至对应团队（组织）并授予相应权限。

## Coding Plan <span id="e1f2a3b4c5d6t" />

### Connection error <span id="f2a3b4c5d6e7u" />

<strong>原因：</strong> Base URL 拼写错误或网络问题。

<strong>解决方案：</strong> 检查 Base URL 域名拼写及网络连接。

### hour allocated quota exceeded <span id="c5d6e7f8a9b0x" />

<strong>原因：</strong> 每 5 小时请求额度已用完。

<strong>解决方案：</strong> 等待 5 小时后额度自动恢复。

### week allocated quota exceeded <span id="f8a9b0c1d2e3a" />

<strong>原因：</strong> 每周请求额度已用完。

<strong>解决方案：</strong> 等待至每周一 00:00:00（UTC+8）额度重置。

### month allocated quota exceeded <span id="c1d2e3f4a5b6d" />

<strong>原因：</strong> 每月请求额度已用完。

<strong>解决方案：</strong> 等待至订阅月对应日 00:00:00（UTC+8）额度重置。

### concurrency allocated quota exceeded <span id="f4a5b6c7d8e9g" />

<strong>原因：</strong> 当前并发请求数超出平台动态分配的上限。

<strong>解决方案：</strong> 等待片刻后重试即可。平台会根据整体资源负载动态调整并发上限，高峰时段可能触发此限制。

### usage allocated quota exceeded. please try again later. <span id="c7d8e9f0a1b2j" />

<strong>原因：</strong> Coding Plan 除调用次数限制外，还会对短时间内的资源消耗进行综合评估。当系统检测到短时间内资源消耗较高时，会触发临时限流。

<strong>解决方案：</strong> 通常等待一小时后即可恢复。建议将大型任务拆分为多个小任务，分时段提交。
