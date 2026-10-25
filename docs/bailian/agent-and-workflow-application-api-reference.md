> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 工作流与旧版智能体应用 API

> 本文介绍 DashScope API 调用阿里云百炼应用（ 智能体 、 工作流 ）的输入与输出参数，并提供典型场景下的调用示例。

<meta property="og:title" content="工作流与旧版智能体应用 API" />

<Tip>
  本文档仅适用于华北2（北京）地域。
</Tip>

<strong>相关指南</strong>

- 请参阅[调用智能体应用](/zh/model-studio/call-single-agent-application)、[调用工作流应用](/zh/model-studio/invoke-workflow-application)。
- 如需通过 Responses API 调用，请参阅 [Responses API](/zh/model-studio/openai-responses-api)。

## 前置准备 <span id="e846d6db23z61" />

开始前，请确保您已完成以下操作：

1. <strong>创建应用：</strong>前往[应用管理](https://bailian.console.aliyun.com/?tab=app#/app-center)创建阿里云[百炼应用](/zh/model-studio/application-introduction)并获取应用 ID；
2. <strong>获取 API Key：</strong>通过[密钥管理](https://bailian.console.aliyun.com/?tab=app#/api-key)获取，并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
3. <strong>安装SDK（可选）：</strong>若使用 SDK 调用，请安装相应语言的[DashScope SDK](/zh/model-studio/install-sdk)。

## 调用方式 <span id="6c6a50b025gbh" />

- <strong>HTTP 接口调用</strong>

  请求地址：`POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion`

  > 其中 `APP_ID` 需替换为您的实际应用 ID
- <strong>SDK 调用</strong>

  Python/Java SDK：本文已默认配置正确的 endpoint

  自定义 endpoint：可通过 base\_url 参数配置

<strong>在线调试</strong>：通过<strong>应用卡片 -> 发布 -> API 调试</strong>路径进入调试页面后，填写参数并点击运行即可。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="989b585548qnj" />

        <strong>app\_id</strong>`string`<strong>（必选）</strong>

        应用标识。

        可在[应用管理](https://bailian.console.aliyun.com/#/app-center)页面的应用卡片上获取应用 ID。

        > Java SDK中为 <strong>appId。</strong>通过 HTTP 调用时，请将实际的应用 ID 放入 <strong>URL</strong>中，替换`APP_ID`。

        <strong>prompt</strong>`string`<strong>（必选）</strong>

        用户的输入指令，用于指导应用生成回复。

        > 通过 HTTP 调用时，请将 <strong>prompt</strong>放入 <strong>input</strong>对象中。

        <strong>session\_id</strong>`string` （可选）

        历史对话标识。

        传入`session_id`时，请求将自动携带云端存储的对话历史。此时必须传递`prompt`。

        该 ID 在连续 1 小时内无任何请求后将自动失效。

        > Java SDK 中为 <strong>setSessionId</strong>。通过 HTTP 调用时，请将 <strong>session\_id</strong>放入 <strong>input</strong>对象中。

        <strong>messages</strong>`array`（可选）

        传递给大模型的上下文，按对话顺序排列。

        当使用`messages`参数实现多轮对话时，无需传递`prompt`和 `session_id`。

        若同时传入`session_id`和`messages`，则大模型优先使用`messages`中的内容，忽略`session_id`和`prompt`。

        > 通过HTTP调用时，请将 <strong>messages</strong> 放入 <strong>input</strong> 对象中。

        > 使用该参数，Python Dashscope SDK的版本至少应为1.20.14，Java Dashscope SDK的版本至少应为2.17.0。

        <Accordion title="消息类型" defaultOpen>
          System Message`object`（可选）

          系统消息，用于设定大模型的角色、语气、任务目标或约束条件等。一般放在`messages`数组的第一位。

          <Accordion title="属性">
            <strong>content</strong>`string`<strong>（必选）</strong>

            系统指令，用于明确模型的角色、行为规范、回答风格和任务约束等。

            <strong>role</strong>`string`<strong>（必选）</strong>

            系统消息的角色，固定为`system`。
          </Accordion>

          User Message`object`<strong>（必选）</strong>

          用户消息，用于向模型传递问题、指令或上下文等。

          <Accordion title="属性">
            <strong>content</strong>`string`<strong>（必选）</strong>

            消息内容。

            <Accordion title="属性">
              <strong>text</strong>`string`<strong>（必选）</strong>

              输入的文本。
            </Accordion>

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，固定为`user`。
          </Accordion>

          Assistant Message`object`（可选）

          模型的回复。通常用于在多轮对话中作为上下文回传给模型。

          <Accordion title="属性">
            <strong>content</strong>`string`<strong>（必选）</strong>

            模型回复的文本内容。

            <strong>role</strong>`string`<strong>（必选）</strong>

            助手消息的角色，固定为`assistant`。
          </Accordion>
        </Accordion>

        <strong>workspace</strong> `string` （可选）

        业务空间标识。相关文档：[获取Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h)。

        仅调用[子业务空间](/zh/model-studio/use-workspace)的应用时需传递`workspace ID`。

        > 通过 HTTP 调用时，请指定Header中的 <strong>X-DashScope-WorkSpace</strong>。

        <strong>stream</strong> `boolean`（可选） 默认值为 `False`

        是否以流式输出方式回复。

        推荐设置为`True`，可提升阅读体验并降低超时风险。

        参数值：

        - `False`（默认）：模型生成全部内容后一次性返回；
        - `True`（推荐）：边生成边输出，每生成一部分内容即返回一个数据块（chunk）。需实时逐个读取这些块以拼接完整回复。

        > 通过Java SDK实现流式输出请通过`streamCall`接口调用；通过HTTP实现流式输出请在Header中指定`X-DashScope-SSE`为`enable`。

        <strong>incremental\_output</strong> `boolean`（可选）默认值为 `False`

        在流式输出模式下是否开启增量输出。

        推荐设置为`True`，可提升阅读体验。

        参数值：

        - `False`（默认）：每次输出当前已经生成的整个序列，最后一次输出为生成的完整结果。

        ```plaintext
        I
        I like
        I like apple
        I like apple.
        ```

        - `True`（推荐）：增量输出，即后续输出内容不包含已输出的内容。需要实时地逐个读取这些片段以获得完整的结果。

        ```plaintext
        I
        like
        apple
        .
        ```

        > Java SDK中为<strong>incrementalOutput</strong>\*。\*通过HTTP调用时，请将<strong>incremental\_output</strong>放入<strong>parameters</strong>对象中。

        <strong>flow\_stream\_mode</strong> `string`（可选）默认值为`full_thoughts`

        <strong>工作流应用</strong>的流式输出模式。相关文档：[流式输出](/zh/model-studio/invoke-workflow-application#b3be03a1ff21e)。

        参数值：

        - `message_format_plus`<strong>（推荐）</strong>：消息增强模式。

          在`workflow_message`字段输出所有节点的执行过程和结果，覆盖所有节点类型。与`message_format`返回相同的数据结构，但支持全部节点类型的流式推送。

          <Tip>
            客户端处理规则：文本类节点（OUTPUT / END text 模式）的`message.content`为增量 delta，需追加拼接；JSON 类节点（LLM / Component / AgentGroup 等）的`message.content`为完整 JSON 快照，需整体替换。当`node_is_completed=true`时标记该节点执行完毕。
          </Tip>

          <strong>子画布节点推流</strong>：循环节点和批处理节点的子画布内节点也会流式推送，通过响应字段`parent_node_id`标识所属父节点。

          <Tip>
            暂不支持嵌套子画布：循环/批处理节点的子画布内不能再放置循环或批处理节点。
          </Tip>

          > Java SDK 中为`FlowStreamMode.MESSAGE_FORMAT_PLUS`。

        - `message_format`<strong>（推荐）</strong>：消息模式。

          在`message`字段输出指定节点（<strong>流程输出</strong>节点或<strong>结束</strong>节点）的结果。

          <Tip>
            在控制台应用中开启目标节点的<strong>流式输出</strong>开关，即可流式返回结果；未开启时，一次性返回该节点的最终结果。
          </Tip>

          > Java SDK 中为`FlowStreamMode.MESSAGE_FORMAT`。

        - `full_thoughts`（默认）：完整思考模式。

          <Note>
            <strong>不推荐新业务使用</strong>。建议改用`message_format`或`message_format_plus`。
          </Note>

          在`thoughts`字段输出所有节点的结果。

          <Tip>
            使用此模式时，必须同时将 `has_thoughts` 参数设置为 `True`。
          </Tip>

          > Java SDK 中为`FlowStreamMode.FULL_THOUGHTS`。

        > Python SDK 版本至少为1.24.0，Java SDK 版本至少为2.22.23。通过HTTP调用时，请将<strong>flow\_stream\_mode</strong>放入<strong>parameters</strong>对象中。

        <strong>biz\_params</strong>`object` （可选）

        应用通过自定义变量、节点或插件传递参数时，使用该字段进行传递。相关文档：[调用智能体应用-传递自定义参数](/zh/model-studio/call-single-agent-application#b774db7cdc0aa)，[调用工作流应用-传递自定义参数](/zh/model-studio/invoke-workflow-application#6e644d5a7b3ia)。

        > Java SDK 中为 <strong>bizParams</strong>。通过HTTP调用时，请将 <strong>biz\_params</strong>放入 <strong>input</strong>对象中。

        <strong>工作流应用</strong>开始节点的自定义变量直接传递，示例：

        ```python
        biz_params = {"city": "杭州"}
        ```

        <strong>智能体应用</strong>通过以下字段传递提示词变量或插件变量参数：

        <Accordion title="属性" defaultOpen>
          <strong>user\_prompt\_params</strong> `object` （可选）

          表示自定义提示词变量参数信息。

          一个应用内的变量名不可重复，且上限 10 个。

          使用步骤：

          1. 在应用内[配置自定义变量并在提示词中引用](/zh/model-studio/single-agent-application#ddecb789a81hy)，然后<strong>发布</strong>应用。
          2. API调用通过此参数传递变量信息。

          示例：

          ```python
          biz_params ={
              "user_prompt_params": {
                  "date": "2025年03月03日",
                  "city": "杭州"
              }
          }
          ```

          <strong>user\_defined\_params</strong> `object` （可选）

          表示自定义插件参数信息。

          一个应用内添加的插件不可重复，且上限 10 个。

          <Accordion title="属性" defaultOpen>
            <strong>tool\_id</strong>`string` （可选）

            插件 ID，可在插件卡片上获取。

            <strong>$\{plugin\_params}</strong>`string`（可选）

            对象最内侧包含的多个键值对。每个键值对表示用户自定义的待传递参数名及其指定值。如：

            ```json
            "article_index": 2
            ```
          </Accordion>

          使用步骤：

          1. 在应用内关联指定插件，并<strong>发布</strong>应用。
          2. API调用通过此参数传递插件信息。

          可提供多个键值对，其中每个键为插件的 `TOOL_ID`，值为该插件所需的参数对象。示例：

          ```plaintext
          "user_defined_params": {
                  "<TOOL_ID>": {
                      "article_index": 2},
                  "<TOOL_ID>": {
                      "article_index": 8}
                  }
          ```

          <strong>user\_defined\_tokens</strong> `object`（可选）

          表示自定义插件的用户级鉴权信息。

          一个应用内添加的插件不可重复，且上限 10 个。

          <Accordion title="属性" defaultOpen>
            <strong>tool\_id</strong>`string` （可选）

            插件 ID，可在插件卡片中获取。通过`<TOOL_ID>`字段传递。

            <strong>user\_token</strong> `string` （可选）

            传递该插件需要的用户鉴权信息，如实际`DASHSCOPE_API_KEY`的值。
          </Accordion>

          使用步骤：

          1. 在应用内关联指定插件，并<strong>发布</strong>应用。
          2. API调用通过此参数传递插件用户级鉴权信息。

          可提供多个键值对，其中每个键为插件的 `TOOL_ID`，值为`user_token` 对象。
        </Accordion>

        <strong>memory\_id</strong> `string` （可选）

        长期记忆体 ID，参阅[CreateMemory](/zh/model-studio/api-bailian-2023-12-29-creatememory)创建。相关文档：[长期记忆](/zh/model-studio/call-single-agent-application#de63036b85aj0)。

        <Note>
          仅<strong>智能体应用</strong>支持此参数。
        </Note>

        1. 在应用中打开<strong>长期记忆</strong>开关，并<strong>发布</strong>应用。
        2. 通过指定 `memory_id` 调用应用时，系统依据用户偏好信息自动构建和保存长期记忆。
        3. 后续使用同一 `memory_id` 调用时，系统会恢复这些长期记忆，并与最新的用户消息合并提供给模型处理。

        > Java SDK 中为 <strong>memoryId</strong>。通过 HTTP 调用时，请将 <strong>memory\_id</strong> 放入<strong>input</strong>对象中。

        <strong>has\_thoughts</strong> `boolean` （可选）默认值为 `False`

        是否输出插件调用、知识检索的过程，或已开启思考模式的模型思考过程，在`thoughts`字段中查看。

        参数值：

        - True：输出。
        - False（默认）：不输出。

        > Java SDK 中为 <strong>hasThoughts</strong>。通过 HTTP 调用时，请将 <strong>has\_thoughts</strong>放入 <strong>parameters</strong>对象中。

        <strong>image\_list</strong> `array`（可选）

        图片列表。支持图像 URL 和 Data URL（Base64 编码）。应用内需选择[图像与视频理解](/zh/model-studio/vision)模型。

        `base64`编码格式可构建为 [Data URL](https://www.rfc-editor.org/rfc/rfc2397)：`data:[MIME_type];base64,{base64_image}`。详细说明和代码示例见本文档右侧[视觉理解](/zh/model-studio/agent-and-workflow-application-api-reference)章节。

        使用场景：

        - 图片检索（<strong>智能体应用</strong>）：根据上传的图片链接，检索包含图片链接的结构化知识库。
        - 视觉理解：通过<strong>千问VL系列模型</strong>的视觉理解能力，分析图像内容实现问答。

        相关文档：[上传文件（文档、图片、视频或音频）](/zh/model-studio/call-single-agent-application#30619780ddy93)。

        > Java SDK 中为 <strong>images</strong>。通过 HTTP 调用时，请将 <strong>image\_list</strong>放入 <strong>input</strong>对象中。

        <strong>file\_list</strong> `array`（可选）

        包含一个或多个文件 URL 的列表。

        <Note>
          仅<strong>智能体应用</strong>支持此参数。
        </Note>

        相关文档：[上传文件（文档、图片、视频或音频）](/zh/model-studio/call-single-agent-application#30619780ddy93)。

        > Java SDK中为 <strong>files</strong>。通过HTTP调用时，请将 <strong>file\_list</strong>放入 <strong>input</strong>对象中。

        > Python Dashscope SDK 的版本至少应为1.24.7，Java Dashscope SDK的版本至少应为2.21.13。

        <strong>rag\_options</strong> `object` （可选）

        用于配置与检索相关的参数。包括但不限于对指定的知识库或文档进行检索。相关文档：[检索知识库](/zh/model-studio/call-single-agent-application#bb173820c5whx)。

        <Note>
          仅<strong>智能体应用</strong>支持此参数。
        </Note>

        > Java SDK 中为 <strong>ragOptions</strong>。通过HTTP调用时，请将 <strong>rag\_options</strong>放入 <strong>parameters</strong>对象中。

        <Accordion title="属性" defaultOpen>
          <strong>pipeline\_ids</strong> `array`（<strong>必选）</strong>

          包含一个或多个知识库 ID 的列表。上限5个。

          检索指定知识库内的所有文档。

          获取方式：

          - [知识库](https://bailian.console.aliyun.com/#/knowledge-base)页面获取知识库 ID；
          - 或通过[CreateIndex](/zh/model-studio/api-bailian-2023-12-29-createindex)接口（仅支持非结构化知识库）返回的`Data.Id`。

          > Java SDK 中为<strong>pipelineIds</strong>。

          <strong>file\_ids</strong>`array`（可选）

          包含一个或多个非结构化文档 ID 的列表。上限5个。

          检索指定知识库内的非结构化文档。

          传入文档 ID 时，必须同时在 `pipeline_ids` 字段中传入这些文档所属的知识库 ID。

          获取方式：

          - [应用数据管理](https://bailian.console.aliyun.com/#/data-center)页面的文档列表中获取文档 ID；
          - 或通过[AddFile](/zh/model-studio/api-bailian-2023-12-29-addfile)接口导入文档返回的 ID。

          > Java SDK 中为 <strong>fileIds</strong>。

          <strong>metadata\_filter</strong>`object` （可选）

          用于筛选非结构化文档的元数据。通过指定一个或多个键值对，检索指定知识库内具备该元数据的非结构化文档。

          使用前提：

          传入元数据时，必须同时在 `pipeline_ids` 字段中传入这些元数据所属的知识库 ID。

          查看方式：

          - 访问[知识库](https://bailian.console.aliyun.com/#/knowledge-base)页面，单击知识库卡片的<strong>查看详情Meta信息</strong>查看。
          - 或通过[ListChunks](/zh/model-studio/api-bailian-2023-12-29-listchunks)接口获取。

          该对象由一个或多个键值对组成：

          - 键 (Key)：`String` 类型，代表元数据的名称。
          - 值 (Value)：

            - 单一值匹配：值为一个 `String`，表示只检索该字段值完全等于此字符串的文档。

              - 示例: `"author": "John.Doe"`
            - 多值“或”匹配：值为一个 `Array` (数组) 或 `List` (列表)，包含多个 `String`。这表示检索该字段值匹配数组中任意一个值的文档（逻辑为 `OR`）。

              - 示例: `"source": ["internal_wiki", "public_docs"]`

          组合逻辑：<br />不同键之间为“与”(AND) 逻辑。例如，`"author": "John.Doe", "source": ["internal_wiki", "public_docs"]` 表示筛选出作者是 "John.Doe" 并且来源是 "internal\_wiki" 或 "public\_docs" 的文档。<br />

          > Java SDK 中为 <strong>metadataFilter</strong>。

          <strong>tags</strong> `array` （可选）

          包含一个或多个非结构化文档标签的列表。

          检索具备该标签的非结构化文档。

          查看方式：

          - 访问[应用数据管理](https://bailian.console.aliyun.com/#/data-center)页面单击<strong>标签</strong>查看。
          - 或通过[DescribeFile](/zh/model-studio/api-bailian-2023-12-29-describefile)接口获取。

          <strong>structured\_filter</strong> `object` （可选）

          用于对结构化数据进行元数据过滤。通过指定一个或多个键值对，可以筛选出符合条件的文档切片。

          查看方式：

          - 可在[知识库](https://bailian.console.aliyun.com/#/knowledge-base)页面，单击知识库卡片的<strong>查看详情查看索引</strong>查看。
          - 或通过[ListChunks](/zh/model-studio/api-bailian-2023-12-29-listchunks)接口获取。

          该对象由一个或多个键值对组成：

          > 每个键（列名）都必须有对应的值，不支持仅通过列名筛选。

          - 键 (Key)：必须是 `String` 类型，代表要筛选的元数据字段名（列名）。
          - 值 (Value)：

            - 单一条件匹配：值为一个 `String` 或 `Number`，表示只检索该字段值完全匹配的文档。示例：`"category": "公司新闻"`。
            - 多值“或”匹配：值为一个 `Array` 或 `List`，包含多个 `String` 或 `Number`。这表示检索该字段值匹配数组中任意一个值的文档（逻辑为 `OR`）。示例：`"year": 2024, "department": ["技术部", "产品部"]`。

          组合逻辑：<br />不同键之间为“与”(AND) 逻辑。例如，`"year": 2024, "department": ["技术部", "产品部"]` 表示筛选出年份是 2024 并且 部门是 "技术部" 或 "产品部" 的文档切片。<br />

          > Java SDK 中为 <strong>structuredFilter</strong>。

          <strong>session\_file\_ids</strong>`array`（可选）

          包含一个或多个文件 ID 的列表。上限 10 个。

          <Note>
            仅<strong>智能体应用</strong>支持此参数。
          </Note>

          在调用<strong>智能体应用</strong>时传递文件 ID，系统将提取文件中的<strong>文字内容</strong>，作为大模型问答的内容依据。

          相关文档：[上传文件（文档、图片、视频或音频）](/zh/model-studio/call-single-agent-application#30619780ddy93)。

          <Tip>
            会话文件ID必须以“`file_session_`”开头，且文件状态为 FILE\_IS\_READY。否则调用失败。
          </Tip>

          > Java SDK 中为<strong>sessionFileIds</strong>。

          > Python Dashscope SDK 的版本至少应为1.20.14，Java Dashscope SDK 的版本至少应为2.17.0。
        </Accordion>

        <strong>model\_id</strong>`string`（可选）

        模型名称。

        <Note>
          仅<strong>智能体应用</strong>支持此参数。
        </Note>

        API 调用时，可通过此参数传递本次调用使用的模型名称。

        优先级：当通过 API 传递的`model_id`与控制台配置不同时，以 API 参数值为准。

        > Java SDK 中为 <strong>modelId</strong>。通过HTTP调用时，请将 <strong>model\_id</strong>放入 <strong>parameters</strong>对象中。

        > Java Dashscope SDK 的版本至少应为<strong>2.19.3</strong>。

        <strong>enable\_system\_time</strong>`boolean` （可选）默认值为 `True`

        控制模型是否自动获取当前时间（北京时间）。

        <Note>
          仅<strong>智能体应用</strong>支持此参数。
        </Note>

        参数值：

        - True（默认）：启用当前时间，模型可直接响应实时时间请求（如“今天日期”）。
        - False：禁用当前时间。需通过自定义变量手动传入时间。例如：

        ```python
        # 通过 user_prompt_params 传递自定义时间
        biz_params ={
            "user_prompt_params": {
                "date": "2025年03月03日"
            }
        }
        ```

        适用场景：

        - 需自定义时间来源，不依赖当前时间。
        - 避免因当前时间变化导致模型输出结果变动。

        > Java SDK 中为 <strong>enableSystemTime</strong>。通过HTTP调用时，请将 <strong>enable\_system\_time</strong>放入 <strong>parameters</strong>对象中。

        > Java Dashscope SDK 的版本至少应为<strong>2.19.3</strong>。

        <strong>enable\_web\_search</strong>`boolean` （可选）默认值为 `false`

        模型在生成回复时是否使用互联网搜索结果进行参考。

        <Note>
          仅<strong>智能体应用</strong>支持此参数。
        </Note>

        参数值：

        - True：启用互联网搜索，模型会将搜索结果作为生成回复过程中的参考信息。

          <Note>
            模型会自行判断是否需要以及何时触发互联网搜索。
          </Note>
        - False（默认）：关闭互联网搜索。

        优先级：

        - 若调用时未设置此参数，以应用内联网搜索

          开关状态为准。
        - 若调用时设置了`enable_web_search`，以 API 参数为准。

        > 启用互联网搜索功能可能会增加 Token 的消耗。

        > Java SDK中为<strong>enableWebSearch</strong>。通过HTTP调用时，请将 <strong>enable\_web\_search</strong>放入 <strong>parameters</strong>对象中。

        > Java Dashscope SDK的版本至少应为<strong>2.19.3</strong>。

        <strong>dialog\_round</strong>`integer` （可选）

        携带的上下文轮数。

        <Note>
          仅<strong>智能体应用</strong>支持此参数。
        </Note>

        设置输入模型的最大历史对话轮数，轮数越多，对话相关性越强。

        优先级：当通过 API 传递的`dialog_round`与控制台配置不同时，则以 API 参数值为准。

        > Java SDK中为<strong>dialogRound</strong>。通过HTTP调用时，请将 <strong>dialog\_round</strong>放入 <strong>parameters</strong>对象中。

        > Java Dashscope SDK的版本至少应为<strong>2.19.3</strong>。

        <strong>enable\_thinking</strong> `boolean` （可选）默认值为 `false`

        此参数用于在[深度思考模型](/zh/model-studio/deep-thinking)中切换思考模式和非思考模式。仅适用于<strong>智能体应用</strong>。

        <Tip>
          <strong>工作流应用</strong>不支持通过此参数动态控制思考模式，需要在各大模型节点中分别设置。
        </Tip>

        参数值：

        - False（默认）：非思考模式。直接返回最终答案（`text`字段）。
        - True：启用思考模式。模型先输出思考过程，再返回最终答案。

        优先级：

        - 若调用时未设置此参数，以应用内模型的思考模式开关状态为准。
        - 若调用时设置了`enable_thinking`，以 API 参数为准。

        <Tip>
          要获取思考过程的内容，必须同时将`has_thoughts`设为`True`，则：

          1. <strong>思考过程：智能体应用</strong>通过`thought`字段获取，<strong>工作流应用</strong>通过`reasoningContent`字段获取。
          2. <strong>最终答案：</strong>`text`字段获取。
        </Tip>

        > 开启`enable_thinking`有极小概率不会输出思考过程。

        > Java SDK中为<strong>enableThinking</strong>。通过HTTP调用时，请将 <strong>enable\_thinking</strong>放入 <strong>parameters</strong>对象中。

        > Java Dashscope SDK的版本至少应为<strong>2.20.0</strong>。
      </td>

      <td>
        <Tabs>
          <Tab title="单轮对话">
            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                from dashscope import Application
                response = Application.call(
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 替换为实际的应用 ID
                    prompt='你是谁？')

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print(response.output.text)
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                // 建议dashscope SDK的版本 >= 2.12.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;

                public class Main {
                    public static void appCall()
                            throws ApiException, NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID")
                                .prompt("你是谁？")
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);

                        System.out.printf("text: %s\n",
                                result.getOutput().getText());
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.err.println("message："+e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "你是谁？"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```

                    > APP\_ID替换为实际的应用 ID。
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例</strong>

                    ```php expandable
                    <?php

                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你是谁？'
                        ]
                    ];

                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }}
                    else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "你是谁？"
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你是谁？""
                                    },
                                    ""parameters"": {},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "你是谁？",
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>

              <Tab title="dashscope CLI">
                <strong>请求示例</strong>

                ```bash
                export DASHSCOPE_API_KEY="your-api-key"
                # 将 {WorkspaceId} 替换为业务空间ID，cn-beijing 替换为对应地域（新加坡: ap-southeast-1, 美东: us-east-1）
                export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
                dashscope application create -a your-app-id -p "你好"
                ```

                > 将 `your-app-id` 替换为实际的应用 ID。`-a` 指定应用 ID，`-p` 指定 prompt 内容。

                <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

                完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="多轮对话">
            通过`session_id`或`messages`启用多轮对话。相关文档：[调用智能体应用-多轮对话](/zh/model-studio/call-single-agent-application#6ca125d59eyc9)，[调用工作流应用-多轮对话](/zh/model-studio/invoke-workflow-application#6ca125d59eyc9)。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python expandable
                import os
                from http import HTTPStatus
                from dashscope import Application
                def call_with_session():
                    response = Application.call(
                        # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        api_key=os.getenv("DASHSCOPE_API_KEY"),
                        app_id='APP_ID',  # 替换为实际的应用 ID
                        prompt='你是谁？')

                    if response.status_code != HTTPStatus.OK:
                        print(f'request_id={response.request_id}')
                        print(f'code={response.status_code}')
                        print(f'message={response.message}')
                        print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                        return response

                    responseNext = Application.call(
                                # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                api_key=os.getenv("DASHSCOPE_API_KEY"),
                                app_id='APP_ID',  # 替换为实际的应用 ID
                                prompt='你有什么技能?',
                                session_id=response.output.session_id)  # 上一轮response的session_id

                    if responseNext.status_code != HTTPStatus.OK:
                        print(f'request_id={responseNext.request_id}')
                        print(f'code={responseNext.status_code}')
                        print(f'message={responseNext.message}')
                        print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                    else:
                        print('%s\n session_id=%s\n' % (responseNext.output.text, responseNext.output.session_id))
                        # print('%s\n' % (response.usage))

                if __name__ == '__main__':
                    call_with_session()
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import java.util.Arrays;
                import java.util.List;
                public class Main {
                    public static void callWithSession()
                            throws ApiException, NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                // 替换为实际的应用 ID
                                .appId("APP_ID")
                                .prompt("你是谁？")
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);

                        param.setSessionId(result.getOutput().getSessionId());
                        param.setPrompt("你有什么技能?");
                        result = application.call(param);

                        System.out.printf("%s\n session_id: %s\n",
                                result.getOutput().getText(), result.getOutput().getSessionId());
                    }

                    public static void main(String[] args) {
                        try {
                            callWithSession();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例（上一轮对话）</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "你是谁？"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "你有什么技能？",
                            "session_id":"4f8ef7233dc641aba496cb201fa59f8c"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例（上一轮对话）</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你是谁？'
                        ]
                    ];

                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        };
                        if (isset($response_data['output']['session_id'])) {
                            echo "session_id={$response_data['output']['session_id']}\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你有什么技能？',
                            // 替换为实际上一轮对话返回的session_id
                            'session_id' => '2e658bcb514f4d30ab7500b4766a8d43'
                        ]
                    ];

                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        };
                        if (isset($response_data['output']['session_id'])) {
                            echo "session_id={$response_data['output']['session_id']}\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例（上一轮对话）</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "你是谁？"
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                                console.log(`session_id=${response.data.output.session_id}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }
                    callDashScope();
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;
                        // session_id替换为实际上一轮对话的session_id
                        const data = {
                            input: {
                                prompt: "你有什么技能？",
                                session_id: 'fe4ce8b093bf46159ea9927a7b22f0d3',
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                                console.log(`session_id=${response.data.output.session_id}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }
                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例（上一轮对话）</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你是谁？""
                                    },
                                    ""parameters"": {},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你有什么技能？"",
                                        ""session_id"": ""7b830e4cc8fe44faad0e648f9b71435f""
                                    },
                                    ""parameters"": {},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例（上一轮对话）</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "你是谁？",
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt":     "你有什么技能？",
                    			"session_id": "f7eea37f0c734c20998a021b688d6de2", // 替换为实际上一轮对话的session_id
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>

                > APP\_ID替换为实际的应用 ID。下一轮对话的输入参数`session_id`字段值替换为实际上一轮对话返回的session\_id值。
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="传递 参数">
            通过`biz_params`传递自定义参数。相关文档：[调用智能体应用-传递自定义参数](/zh/model-studio/call-single-agent-application#b774db7cdc0aa)，[调用工作流应用-传递自定义参数](/zh/model-studio/invoke-workflow-application#6e644d5a7b3ia)。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                # 建议dashscope SDK 的版本 >= 1.14.0
                from dashscope import Application
                biz_params = {
                    # 智能体应用的自定义插件输入参数传递，自定义的插件ID替换<TOOL_ID>
                    "user_defined_params": {
                        "<TOOL_ID>": {
                            "article_index": 2}}}
                response = Application.call(
                        # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        api_key=os.getenv("DASHSCOPE_API_KEY"),
                        app_id='APP_ID',
                        prompt='寝室公约内容',
                        biz_params=biz_params)

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))  # 处理只输出文本text
                    # print('%s\n' % (response.usage))
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.utils.JsonUtils;

                public class Main {
                    public static void appCall() throws NoApiKeyException, InputRequiredException {
                        String bizParams =
                                // 智能体应用的自定义插件输入参数传递，自定义的插件ID替换<TOOL_ID>
                                "{\"user_defined_params\":{\"<TOOL_ID>\":{\"article_index\":2}}}";
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID")
                                .prompt("寝室公约内容")
                                .bizParams(JsonUtils.parse(bizParams))
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n",
                                result.getOutput().getText());
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "寝室公约内容",
                            "biz_params":
                            {
                                "user_defined_params":
                                {
                                    "<TOOL_ID>":
                                        {
                                        "article_index": 2
                                        }
                                }
                            }
                        },
                        "parameters":  {},
                        "debug":{}
                    }'
                    ```

                    > APP\_ID替换为实际的应用 ID。\<TOOL\_ID>替换为插件ID。
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例</strong>

                    ```php expandable
                    <?php

                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID
                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";
                    //<TOOL_ID>替换为实际的插件ID
                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '寝室公约内容',
                            'biz_params' => [
                            'user_defined_params' => [
                                '<TOOL_ID>' => [
                                    'article_index' => 2
                                    ]
                                ]
                            ]
                        ],
                    ];
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID
                        const pluginCode = 'TOOL_ID';// 替换为实际的插件ID
                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "寝室公约内容",
                                biz_params: {
                                    user_defined_params: {
                                        [pluginCode]: {
                                            // article_index为自定义插件的变量，替换为实际的插件变量
                                            'article_index': 3
                                        }
                                    }
                                }
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            console.log("Sending request to DashScope API...");

                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                if (response.data.output && response.data.output.text) {
                                    console.log(`${response.data.output.text}`);
                                }
                            } else {
                                console.log("Request failed:");
                                if (response.data.request_id) {
                                    console.log(`request_id=${response.data.request_id}`);
                                }
                                console.log(`code=${response.status}`);
                                if (response.data.message) {
                                    console.log(`message=${response.data.message}`);
                                } else {
                                    console.log('message=Unknown error');
                                }
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }
                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例</strong>

                    ```csharp expandable
                    using System.Text;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY")?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID";// 替换为实际的应用ID

                            if (string.IsNullOrEmpty(apiKey))
                            {
                                Console.WriteLine("请确保设置了 DASHSCOPE_API_KEY。");
                                return;
                            }

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                string pluginCode = "TOOL_ID"; // TOOL_ID替换为实际的插件 ID
                                string jsonContent = $@"{{
                                    ""input"": {{
                                        ""prompt"": ""寝室公约内容"",
                                        ""biz_params"": {{
                                            ""user_defined_params"": {{
                                                ""{pluginCode}"": {{
                                                    ""article_index"": 2
                                                }}
                                            }}
                                        }}
                                    }},
                                    ""parameters"": {{}},
                                    ""debug"": {{}}
                                }}";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID"           // 替换为实际的应用 ID
                    	pluginCode := "TOOL_ID" // 替换为实际的插件 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]interface{}{
                    			"prompt": "寝室公约内容",
                    			"biz_params": map[string]interface{}{
                    				"user_defined_params": map[string]interface{}{
                    					pluginCode: map[string]interface{}{
                    						"article_index": 2,
                    					},
                    				},
                    			},
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="发布新版本对 API 调用的影响">
            应用发布新版本后，APP\_ID 始终指向最新已发布版本。发布时正在进行的请求不会被中断，会按发布前的配置正常完成；发布完成后，通过同一 APP\_ID 发起的新请求将使用新版本的配置（包括<strong>提示词</strong>、模型、工具等）。

            API 不支持通过参数指定调用历史版本。请求体仅包含`input`和`parameters`字段，不提供版本号或历史版本参数。

            多轮对话的`session_id`在新版本发布后继续使用时，可能出现上下文理解偏差或工具调用异常，因为该会话的历史上下文基于发布前的配置产生。建议在发布重大变更前结束活跃的多轮对话，或在发布后使用新的`session_id`重新开始会话。

            如需恢复发布前的配置，请在<strong>百炼控制台</strong>的<strong>应用详情页</strong>执行版本回滚：

            1. 单击<strong>版本管理</strong>。
            2. 选中需要恢复的历史版本。
            3. 单击<strong>覆盖当前草稿</strong>。
            4. 单击<strong>发布</strong>，使回滚生效。

            为降低对线上调用的影响，建议在业务低峰期发布重大变更；如需同时对外提供多个版本的能力，可通过创建多个应用（使用不同的 APP\_ID）实现版本管理。
          </Tab>

          <Tab title="流式输出">
            通过`stream`实现流式输出。相关文档：[调用智能体应用-流式输出](/zh/model-studio/call-single-agent-application#b3be03a1ff21e)，[调用工作流应用-流式输出](/zh/model-studio/invoke-workflow-application#b3be03a1ff21e)。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                from dashscope import Application
                responses = Application.call(
                            # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            api_key=os.getenv("DASHSCOPE_API_KEY"),
                            app_id='APP_ID',
                            prompt='你是谁？',
                            stream=True,  # 流式输出
                            incremental_output=True)  # 增量输出

                for response in responses:
                    if response.status_code != HTTPStatus.OK:
                        print(f'request_id={response.request_id}')
                        print(f'code={response.status_code}')
                        print(f'message={response.message}')
                        print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                    else:
                        print(f'{response.output.text}\n')  # 处理只输出文本text
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                // 建议dashscope SDK的版本 >= 2.15.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import io.reactivex.Flowable;// 流式输出
                // 智能体应用调用实现流式输出结果

                public class Main {
                    public static void streamCall() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                // 替换为实际的应用 ID
                                .appId("APP_ID")
                                .prompt("你是谁?")
                                // 增量输出
                                .incrementalOutput(true)
                                .build();
                        Application application = new Application();
                        // .streamCall（）：流式输出内容
                        Flowable<ApplicationResult> result = application.streamCall(param);
                        result.blockingForEach(data -> {
                            System.out.printf("%s\n",
                                    data.getOutput().getText());
                        });
                    }
                    public static void main(String[] args) {
                        try {
                            streamCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --header 'X-DashScope-SSE: enable' \
                    --data '{
                        "input": {
                            "prompt": "你是谁？"

                        },
                        "parameters":  {
                            "incremental_output":true
                        },
                        "debug": {}
                    }'
                    ```

                    > APP\_ID替换为实际的应用 ID。
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例</strong>

                    ```php expandable
                    <?php

                    // 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你是谁？'],
                        "parameters" => [
                            'incremental_output' => true]];// 增量输出
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, false); // 不返回传输的数据
                    curl_setopt($ch, CURLOPT_WRITEFUNCTION, function ($ch, $string) {
                        echo $string; // 处理流式数据
                        return strlen($string);
                    });
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key,
                        'X-DashScope-SSE: enable' // 流式输出
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);

                    if ($status_code != 200) {
                        echo "HTTP Status Code: $status_code\n";
                        echo "Request Failed.\n";
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例</strong>

                    <Accordion title="1.输出完整响应" defaultOpen>
                      ```javascript expandable
                      const axios = require('axios');

                      async function callDashScope() {
                          //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                          const apiKey = process.env.DASHSCOPE_API_KEY;
                          const appId = 'APP_ID';// 替换为实际的应用 ID

                          const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                          const data = {
                              input: {
                                  prompt: "你是谁？"
                              },
                              parameters: {
                                  'incremental_output' : true // 增量输出
                              },
                              debug: {}
                          };

                          try {
                              console.log("Sending request to DashScope API...");

                              const response = await axios.post(url, data, {
                                  headers: {
                                      'Authorization': `Bearer ${apiKey}`,
                                      'Content-Type': 'application/json',
                                      'X-DashScope-SSE': 'enable' // 流式输出
                                  },
                                  responseType: 'stream' // 用于处理流式响应
                              });

                              if (response.status === 200) {
                                  // 处理流式响应
                                  response.data.on('data', (chunk) => {
                                      console.log(`Received chunk: ${chunk.toString()}`);
                                  });
                              } else {
                                  console.log("Request failed:");
                                  if (response.data.request_id) {
                                      console.log(`request_id=${response.data.request_id}`);
                                  }
                                  console.log(`code=${response.status}`);
                                  if (response.data.message) {
                                      console.log(`message=${response.data.message}`);
                                  } else {
                                      console.log('message=Unknown error');
                                  }
                              }
                          } catch (error) {
                              console.error(`Error calling DashScope: ${error.message}`);
                              if (error.response) {
                                  console.error(`Response status: ${error.response.status}`);
                                  console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                              }
                          }
                      }

                      callDashScope();
                      ```
                    </Accordion>

                    可展开折叠面板查看具体内容：

                    <Accordion title="2.只输出text字段内容">
                      ```javascript expandable
                      const axios = require('axios');
                      const { Transform } = require('stream');

                      async function callDashScope() {
                          //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                          const apiKey = process.env.DASHSCOPE_API_KEY;
                          const appId = 'APP_ID'; // 替换为实际的应用 ID

                          const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                          const data = {
                              input: { prompt: "你是谁？" },
                              parameters: { incremental_output: true }, // 增量输出
                              debug: {}
                          };

                          try {
                              console.log("Sending request to DashScope API...");

                              const response = await axios.post(url, data, {
                                  headers: {
                                      'Authorization': `Bearer ${apiKey}`,
                                      'Content-Type': 'application/json',
                                      'X-DashScope-SSE': 'enable' // 流式输出
                                  },
                                  responseType: 'stream' // 用于处理流式响应
                              });

                              if (response.status === 200) {
                                  // // 处理流式响应 SSE协议解析转换流
                                  const sseTransformer = new Transform({
                                      transform(chunk, encoding, callback) {
                                          this.buffer += chunk.toString();

                                          // 按SSE事件分割（两个换行符）
                                          const events = this.buffer.split(/\n\n/);
                                          this.buffer = events.pop() || ''; // 保留未完成部分

                                          events.forEach(eventData => {
                                              const lines = eventData.split('\n');
                                              let textContent = '';

                                              // 解析事件内容
                                              lines.forEach(line => {
                                                  if (line.startsWith('data:')) {
                                                      try {
                                                          const jsonData = JSON.parse(line.slice(5).trim());
                                                          if (jsonData.output?.text) {
                                                              textContent = jsonData.output.text;
                                                          }
                                                      } catch(e) {
                                                          console.error('JSON解析错误:', e.message);
                                                      }
                                                  }
                                              });

                                              if (textContent) {
                                                  // 添加换行符并推送
                                                  this.push(textContent + '\n');
                                              }
                                          });

                                          callback();
                                      },
                                      flush(callback) {
                                          if (this.buffer) {
                                              this.push(this.buffer + '\n');
                                          }
                                          callback();
                                      }
                                  });
                                  sseTransformer.buffer = '';

                                  // 管道处理
                                  response.data
                                      .pipe(sseTransformer)
                                      .on('data', (textWithNewline) => {
                                          process.stdout.write(textWithNewline); // 自动换行输出
                                      })
                                      .on('end', () => console.log(""))
                                      .on('error', err => console.error("管道错误:", err));

                              } else {
                                  console.log("请求失败，状态码:", response.status);
                                  response.data.on('data', chunk => console.log(chunk.toString()));
                              }
                          } catch (error) {
                              console.error(`API调用失败: ${error.message}`);
                              if (error.response) {
                                  console.error(`状态码: ${error.response.status}`);
                                  error.response.data.on('data', chunk => console.log(chunk.toString()));
                              }
                          }
                      }

                      callDashScope();
                      ```
                    </Accordion>
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例</strong>

                    ```csharp expandable
                    using System.Net;
                    using System.Text;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID
                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                client.DefaultRequestHeaders.Add("X-DashScope-SSE", "enable");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你是谁""
                                    },
                                    ""parameters"": {""incremental_output"": true},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                                try
                                {
                                    var request = new HttpRequestMessage(HttpMethod.Post, url);
                                    request.Content = content;

                                    HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                                        using (var stream = await response.Content.ReadAsStreamAsync())
                                        using (var reader = new StreamReader(stream))
                                        {
                                            string? line; // 声明为可空字符串
                                            while ((line = await reader.ReadLineAsync()) != null)
                                            {
                                                if (line.StartsWith("data:"))
                                                {
                                                    string data = line.Substring(5).Trim();
                                                    Console.WriteLine(data);
                                                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    	"strings"
                    	"time"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体，其中incremental_output为是否开启流式响应
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "你是谁？",
                    		},
                    		"parameters": map[string]interface{}{
                    			"incremental_output": true,
                    		},
                    		"debug": map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头,其中X-DashScope-SSE设置为enable，表示开启流式响应
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")
                    	req.Header.Set("X-DashScope-SSE", "enable")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	if resp.StatusCode != http.StatusOK {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		body, _ := io.ReadAll(resp.Body)
                    		fmt.Println(string(body))
                    		return
                    	}

                    	// 处理流式响应
                    	reader := io.Reader(resp.Body)
                    	buf := make([]byte, 1024)
                    	for {
                    		n, err := reader.Read(buf)
                    		if n > 0 {
                    			data := string(buf[:n])
                    			lines := strings.Split(data, "\n")
                    			for _, line := range lines {
                    				line = strings.TrimSpace(line)
                    				if len(line) >= 5 && line[:5] == "data:" {
                    					timestamp := time.Now().Format("2006-01-02 15:04:05.000")
                    					fmt.Printf("%s: %s\n", timestamp, line[5:])
                    				} else if len(line) > 0 {
                    					fmt.Println(line)
                    				}
                    			}
                    		}
                    		if err != nil {
                    			if err == io.EOF {
                    				break
                    			}
                    			fmt.Printf("Error reading response: %v\n", err)
                    			break
                    		}
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="检索知识库">
            调用<strong>智能体应用</strong>时，通过`rag_options`实现知识库检索。相关文档：[检索知识库](/zh/model-studio/call-single-agent-application#bb173820c5whx)。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                # 建议dashscope SDK 的版本 >= 1.20.11
                from dashscope import Application
                response = Application.call(
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 应用ID替换APP_ID
                    prompt='请帮我推荐一款3000元以下的手机',
                    rag_options={
                        "pipeline_ids": ["PIPELINE_ID1","PIPELINE_ID2"],  # 替换为实际的知识库ID,逗号隔开多个
                    }
                )

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))  # 处理只输出文本text
                    # print('%s\n' % (response.usage))
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                // 建议dashscope SDK 的版本 >= 2.16.8；
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import java.util.Arrays;

                public class Main {
                    public static void streamCall() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用ID
                                .prompt("请帮我推荐一款3000元左右的手机")
                                .ragOptions(RagOptions.builder()
                                        // 替换为实际指定的知识库ID，逗号隔开多个
                                        .pipelineIds(Arrays.asList("PIPELINES_ID1", "PIPELINES_ID2"))
                                        .build())
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n",
                                result.getOutput().getText());// 处理只输出文本text
                    }

                    public static void main(String[] args) {
                        try {
                            streamCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "请帮我推荐一款3000元以下的手机"
                        },
                        "parameters":  {
                                        "rag_options" : {
                                        "pipeline_ids":["PIPELINE_ID1"]}
                        },
                        "debug": {}
                    }'
                    ```

                    > APP\_ID替换为实际的应用 ID，PIPELINE\_ID1替换为指定的知识库ID。
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '请帮我推荐一款3000元以下的手机'
                        ],
                        "parameters" => [
                            'rag_options' => [
                                'pipeline_ids' => ['PIPELINE_ID1','PIPELINE_ID2']//替换为指定的知识库ID,逗号隔开多个
                            ]
                        ]
                    ];
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例</strong>

                    ```javascript expandable
                    const axios = require('axios');
                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';//替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "请帮我推荐一款3000元以下的手机"
                            },
                            parameters: {
                                rag_options:{
                                    pipeline_ids:['PIPELINE_ID1','PIPELINE_ID2']  // 替换为指定的知识库ID，多个请用逗号隔开
                                }
                            },
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例</strong>

                    ```csharp expandable
                    using System.Text;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY")?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID";// 替换为实际的应用ID
                            // PIPELINE_ID1替换为指定的知识库ID
                            if (string.IsNullOrEmpty(apiKey))
                            {
                                Console.WriteLine("请确保设置了 DASHSCOPE_API_KEY。");
                                return;
                            }

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                string jsonContent = $@"{{
                                    ""input"": {{
                                        ""prompt"": ""请帮我推荐一款3000元以下的手机""
                                    }},
                                    ""parameters"": {{
                                        ""rag_options"" : {{
                                            ""pipeline_ids"":[""PIPELINE_ID1""]
                                        }}
                                    }},
                                    ""debug"": {{}}
                                }}";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "请帮我推荐一款3000元以下的手机",
                    		},
                    		"parameters": map[string]interface{}{
                    			"rag_options": map[string]interface{}{
                    				"pipeline_ids": []string{"PIPELINE_ID1"}, // 替换为指定的知识库ID
                    			},
                    		},
                    		"debug": map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>

            <strong>查看检索过程信息：</strong>调用时在代码中添加`has_thoughts`并设置为True，则检索的过程信息会在`output`的`thoughts`字段中返回。
          </Tab>

          <Tab title="长期记忆">
            调用<strong>智能体应用</strong>时，指定 `memory_id` 启用长期记忆。相关文档：[长期记忆](/zh/model-studio/call-single-agent-application#de63036b85aj0)。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例（生成记忆体内容）</strong>

                ```python
                # DashScope SDK版本不低于1.22.1
                from http import HTTPStatus
                import os
                from dashscope import Application
                response = Application.call(
                           # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            api_key=os.getenv("DASHSCOPE_API_KEY"),
                            app_id='APP_ID',  # 请输入实际的应用 ID
                            prompt='用户饮食偏好：面食',
                            memory_id='MEMORY_ID')  # 请输入实际的记忆体 ID

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))  # 处理只输出text
                    # print('%s\n' % (response.usage))
                ```

                <strong>请求示例（再次调用）</strong>

                ```python
                # DashScope SDK版本不低于1.22.1
                from http import HTTPStatus
                import os
                from dashscope import Application
                response = Application.call(
                           # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            api_key=os.getenv("DASHSCOPE_API_KEY"),
                            app_id='APP_ID',  # 请输入实际的应用 ID
                            prompt='美食推荐',
                            memory_id='MEMORY_ID')  # 请输入实际的记忆体 ID

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))  # 处理只输出text
                    # print('%s\n' % (response.usage))
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例（生成记忆体内容）</strong>

                ```java expandable
                // DashScope SDK版本不低于2.17.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;

                public class Main {
                    public static void callWithMemory() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用 ID
                                .prompt("用户饮食偏好：面食")
                                .memoryId("MEMORY_ID") // 替换为实际的记忆体 ID
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);

                        System.out.printf("%s\n",
                                result.getOutput().getText()); // 处理只输出文本text
                    }
                    public static void main(String[] args) {
                        try {
                            callWithMemory();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```

                <strong>请求示例（再次调用）</strong>

                ```java expandable
                // DashScope SDK版本不低于2.17.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;

                public class Main {
                    public static void callWithMemory() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用 ID
                                .prompt("美食推荐")
                                .memoryId("MEMORY_ID") // 替换为实际的记忆体 ID
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);

                        System.out.printf("text: %s\n",
                                result.getOutput().getText()); // 处理只输出文本text
                    }
                    public static void main(String[] args) {
                        try {
                            callWithMemory();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例（生成记忆体内容）</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "用户饮食偏好：面食",
                            "memory_id": "MEMORY_ID"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```

                    <strong>请求示例（再次调用）</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "美食推荐",
                            "memory_id": "MEMORY_ID"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```

                    > APP\_ID替换为实际的应用 ID。
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例（生成记忆体内容）</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID
                    $memory_id = 'MEMORY_ID'; // 替换为实际的记忆体 ID
                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/{$application_id}/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '用户饮食偏好：面食',
                            'memory_id' => $memory_id
                        ]
                    ];
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```

                    <strong>请求示例（再次调用）</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID
                    $memory_id = 'MEMORY_ID'; // 替换为实际的记忆体 ID
                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/{$application_id}/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '美食推荐',
                            'memory_id' => $memory_id
                        ]
                    ];
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例（生成记忆体内容）</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID
                        const memoryId = 'MEMORY_ID';// 替换为实际的memory_id

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "用户饮食偏好：面食",
                                memory_id: memoryId
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            console.log("Sending request to DashScope API...");

                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                if (response.data.output && response.data.output.text) {
                                    console.log(`${response.data.output.text}`);
                                }
                            } else {
                                console.log("Request failed:");
                                if (response.data.request_id) {
                                    console.log(`request_id=${response.data.request_id}`);
                                }
                                console.log(`code=${response.status}`);
                                if (response.data.message) {
                                    console.log(`message=${response.data.message}`);
                                } else {
                                    console.log('message=Unknown error');
                                }
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```

                    <strong>请求示例（再次调用）</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID
                        const memoryId = 'MEMORY_ID';// 替换为实际的memory_id

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "美食推荐",
                                memory_id: memoryId
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            console.log("Sending request to DashScope API...");

                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                if (response.data.output && response.data.output.text) {
                                    console.log(`${response.data.output.text}`);
                                }
                            } else {
                                console.log("Request failed:");
                                if (response.data.request_id) {
                                    console.log(`request_id=${response.data.request_id}`);
                                }
                                console.log(`code=${response.status}`);
                                if (response.data.message) {
                                    console.log(`message=${response.data.message}`);
                                } else {
                                    console.log('message=Unknown error');
                                }
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例（生成记忆体内容）</strong>

                    ```csharp expandable
                    using System.Text;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY")?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID";// 替换为实际的应用ID
                            string memoryId = "MEMORY_ID";//替换为实际的memory_id
                            if (string.IsNullOrEmpty(apiKey))
                            {
                                Console.WriteLine("请确保设置了 DASHSCOPE_API_KEY。");
                                return;
                            }

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                string jsonContent = $@"{{
                                    ""input"": {{
                                        ""prompt"": ""用户饮食偏好：面食"",
                                        ""memory_id"":""{memoryId}""
                                    }},
                                    ""parameters"": {{}},
                                    ""debug"": {{}}
                                }}";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```

                    <strong>请求示例（再次调用）</strong>

                    ```csharp expandable
                    using System.Text;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY")?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID";// 替换为实际的应用ID
                            string memoryId = "MEMORY_ID";//替换为实际的memory_id
                            if (string.IsNullOrEmpty(apiKey))
                            {
                                Console.WriteLine("请确保设置了 DASHSCOPE_API_KEY。");
                                return;
                            }

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                string jsonContent = $@"{{
                                    ""input"": {{
                                        ""prompt"": ""美食推荐"",
                                        ""memory_id"":""{memoryId}""
                                    }},
                                    ""parameters"": {{}},
                                    ""debug"": {{}}
                                }}";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例（生成记忆体内容）</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt":    "用户饮食偏好：面食",
                    			"memory_id": "MEMORY_ID", // 替换为实际的记忆体 ID
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```

                    <strong>请求示例（再次调用）</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt":    "美食推荐",
                    			"memory_id": "MEMORY_ID", // 替换为实际的记忆体 ID
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="上传文件">
            调用<strong>智能体应用</strong>时，指定 `session_file_ids` /`file_list`启用上传文件功能。相关文档：[文件问答](/zh/model-studio/call-single-agent-application#30619780ddy93)。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                # 建议dashscope SDK 的版本 >= 1.20.14
                from dashscope import Application
                response = Application.call(
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 应用ID替换APP_ID
                    prompt='请根据以下文件帮我推荐一款3000元以下的手机',
                    rag_options={
                        "session_file_ids": ["Session_File_ID1"],  # Session_File_ID1 替换为实际的临时文件ID,逗号隔开多个
                    }
                )

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))  # 处理只输出文本text
                    # print('%s\n' % (response.usage))
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                // 建议dashscope SDK 的版本 >= 2.17.0；
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;

                import java.util.Arrays;

                public class Main {
                    public static void appCall() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用ID
                                .prompt("请根据以下文件帮我推荐一款3000元左右的手机")
                                .ragOptions(RagOptions.builder()
                                        .sessionFileIds(Arrays.asList("Session_File_ID1", "Session_File_ID2"))  // 替换为实际指定的临时文件 ID，逗号隔开多个
                                        .build())
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n",
                                result.getOutput().getText());// 处理只输出文本text
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "请根据以下文件帮我推荐一款3000元以下的手机"
                        },
                        "parameters":  {
                                        "rag_options" : {
                                        "session_file_ids":["Session_File_ID1"]}
                        },
                        "debug": {}
                    }'
                    ```
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '请根据以下文件帮我推荐一款3000元以下的手机'
                        ],
                        "parameters" => [
                            'rag_options' => [
                                'session_file_ids' => ['Session_File_ID1']//替换为指定的临时文件ID,逗号隔开多个
                            ]
                        ]
                    ];
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例</strong>

                    ```javascript expandable
                    const axios = require('axios');
                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';//替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "请根据以下文件帮我推荐一款3000元以下的手机"
                            },
                            parameters: {
                                rag_options:{
                                    session_file_ids:['Session_File_ID1']  // 替换为指定的临时文件 ID，多个请用逗号隔开
                                }
                            },
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例</strong>

                    ```csharp expandable
                    using System.Text;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY")?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID";// 替换为实际的应用ID
                            // Session_File_ID1替换为指定的临时文件ID
                            if (string.IsNullOrEmpty(apiKey))
                            {
                                Console.WriteLine("请确保设置了 DASHSCOPE_API_KEY。");
                                return;
                            }

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                string jsonContent = $@"{{
                                    ""input"": {{
                                        ""prompt"": ""请根据以下文件帮我推荐一款3000元以下的手机""
                                    }},
                                    ""parameters"": {{
                                        ""rag_options"" : {{
                                            ""session_file_ids"":[""Session_File_ID1""]
                                        }}
                                    }},
                                    ""debug"": {{}}
                                }}";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "请根据以下文件帮我推荐一款3000元以下的手机",
                    		},
                    		"parameters": map[string]interface{}{
                    			"rag_options": map[string]interface{}{
                    				"session_file_ids": []string{"Session_File_ID1"}, // 替换为实际的临时文件
                    			},
                    		},
                    		"debug": map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="视觉理解">
            通过 `image_list` 参数传入图像 URL 或 Data URL（Base64 编码）启用视觉理解功能。

            <strong>应用配置：</strong>应用内需使用[图像与视频理解](/zh/model-studio/vision)模型。相关文档：[文件问答](/zh/model-studio/call-single-agent-application#30619780ddy93)。

            <Note>
              支持使用 Base64 编码本地图像。将图像编码为 Base64 字符串后，按`data:[MIME_type];base64,{base64_image}`格式构建 Data URL 传入。`MIME_type`必须与图像格式匹配。常见格式：PNG 使用 `image/png`，JPEG 使用 `image/jpeg`，WebP 使用 `image/webp`。
            </Note>

            <Tabs>
              <Tab title="Python">
                <strong>URL请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                # 建议dashscope SDK 的版本 >= 1.20.14
                from dashscope import Application
                response = Application.call(
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 应用ID替换APP_ID
                    prompt='这是什么',
                    image_list=['https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg'],
                )

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))  # 处理只输出文本text
                    # print('%s\n' % (response.usage))
                ```

                <strong>Base64 编码示例</strong>

                ```python expandable
                import os
                import base64
                from http import HTTPStatus
                from dashscope import Application

                # 将本地图像编码为 Base64 字符串
                def encode_image(image_path):
                    with open(image_path, "rb") as image_file:
                        return base64.b64encode(image_file.read()).decode("utf-8")

                # 编码本地图像
                base64_image = encode_image("/path/to/your/image.jpeg")  # 替换为实际图像路径

                # 构建 Data URL（注意：MIME type 需与图像格式匹配）
                data_url = f"data:image/jpeg;base64,{base64_image}"

                # 调用应用 API
                response = Application.call(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 应用ID替换APP_ID
                    prompt='这张图片里有什么？',
                    image_list=[data_url],  # 使用 Data URL
                )

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))
                ```
              </Tab>

              <Tab title="Java">
                <strong>URL请求示例</strong>

                ```java expandable
                // 建议dashscope SDK 的版本 >= 2.19.0；
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import java.util.Arrays;

                public class Main {
                    public static void appCall() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用ID
                                .prompt("这是什么？")
                                .images(Arrays.asList("https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"))
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n",
                                result.getOutput().getText());// 处理只输出文本text
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```

                <strong>Base64 编码示例</strong>

                ```java expandable
                // 建议dashscope SDK 的版本 >= 2.19.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import java.io.IOException;
                import java.nio.file.Files;
                import java.nio.file.Paths;
                import java.util.Arrays;
                import java.util.Base64;

                public class Main {
                    // 将本地图像编码为 Base64 字符串
                    public static String encodeImage(String imagePath) throws IOException {
                        byte[] imageBytes = Files.readAllBytes(Paths.get(imagePath));
                        return Base64.getEncoder().encodeToString(imageBytes);
                    }

                    public static void appCall() throws NoApiKeyException, InputRequiredException, IOException {
                        // 编码本地图像
                        String base64Image = encodeImage("/path/to/your/image.jpeg"); // 替换为实际图像路径

                        // 构建 Data URL（注意：MIME type 需与图像格式匹配）
                        String dataUrl = "data:image/jpeg;base64," + base64Image;

                        ApplicationParam param = ApplicationParam.builder()
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用ID
                                .prompt("这张图片里有什么？")
                                .images(Arrays.asList(dataUrl)) // 使用 Data URL
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n", result.getOutput().getText());
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException | IOException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>URL请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "这是什么",
                            "image_list":["https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"]
                        },
                        "debug": {}
                    }'
                    ```

                    <strong>Base64 编码示例</strong>

                    ```bash
                    # 将本地图像编码为 Base64 字符串
                    base64_image=$(base64 -i /path/to/your/image.jpeg)  # macOS/Linux

                    # 构建 Data URL（注意：MIME type 需与图像格式匹配）
                    data_url="data:image/jpeg;base64,${base64_image}"

                    # 调用应用 API
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data "{
                        \"input\": {
                            \"prompt\": \"这张图片里有什么？\",
                            \"image_list\": [\"${data_url}\"]
                        },
                        \"debug\": {}
                    }"
                    ```
                  </Tab>

                  <Tab title="PHP">
                    <strong>URL请求示例</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            "prompt" => "这是什么",
                            "image_list" => ["https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"],
                        ],
                        "debug" => [],
                    ];
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```

                    <strong>Base64 编码示例</strong>

                    ```php expandable
                    <?php
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID';

                    function encodeImage($imagePath) {
                        if (!file_exists($imagePath)) die("Error: Image file not found");
                        return base64_encode(file_get_contents($imagePath));
                    }

                    $base64Image = encodeImage("/path/to/your/image.jpeg");
                    $dataUrl = "data:image/jpeg;base64," . $base64Image;

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";
                    $data = ["input" => ["prompt" => "这张图片里有什么？", "image_list" => [$dataUrl]], "debug" => []];

                    $ch = curl_init($url);
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($data));
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, ['Content-Type: application/json', 'Authorization: Bearer ' . $api_key]);

                    $response = curl_exec($ch);
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    curl_close($ch);

                    if ($status_code == 200) {
                        $response_data = json_decode($response, true);
                        echo $response_data['output']['text'];
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>URL请求示例</strong>

                    ```javascript expandable
                    import axios from 'axios';
                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';//替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "这是什么",
                                image_list: ["https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"],
                            },
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```

                    <strong>Base64 编码示例</strong>

                    ```javascript
                    import axios from 'axios';
                    import fs from 'fs';

                    async function callWithBase64() {
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';

                        const imageBuffer = fs.readFileSync('/path/to/your/image.jpeg');
                        const base64Image = imageBuffer.toString('base64');
                        const dataUrl = `data:image/jpeg;base64,${base64Image}`;

                        const response = await axios.post(
                            `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`,
                            { input: { prompt: "这张图片里有什么？", image_list: [dataUrl] } },
                            { headers: { 'Authorization': `Bearer ${apiKey}`, 'Content-Type': 'application/json' } }
                        );

                        console.log(response.data.output.text);
                    }

                    callWithBase64();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>URL请求示例</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY")?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID";// 替换为实际的应用ID

                            if (string.IsNullOrEmpty(apiKey))
                            {
                                Console.WriteLine("请确保设置了 DASHSCOPE_API_KEY。");
                                return;
                            }

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                string jsonContent = $@"{{
                                    ""input"": {{
                                        ""prompt"": ""这是什么"",
                                        ""image_list"": [""https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg""]
                                    }},
                                    ""parameters"": {{
                                    }},
                                    ""debug"": {{}}
                                }}";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```

                    <strong>Base64 编码示例</strong>

                    ```csharp expandable
                    using System;
                    using System.IO;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            var apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY");
                            var appId = "APP_ID";

                            byte[] imageBytes = File.ReadAllBytes("/path/to/your/image.jpeg");
                            var base64Image = Convert.ToBase64String(imageBytes);
                            var dataUrl = $"data:image/jpeg;base64,{base64Image}";

                            using var client = new HttpClient();
                            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                            var json = $@"{{""input"": {{""prompt"": ""这张图片里有什么？"", ""image_list"": [""{dataUrl}""]}}, ""debug"": {{}}}}";
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            var response = await client.PostAsync($"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion", content);

                            Console.WriteLine(await response.Content.ReadAsStringAsync());
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>URL请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]interface{}{
                    			"prompt":     "这是什么",
                    			"image_list": []string{"https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"},
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```

                    <strong>Base64 编码示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/base64"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID"

                    	imageData, _ := os.ReadFile("/path/to/your/image.jpeg")
                    	base64Image := base64.StdEncoding.EncodeToString(imageData)
                    	dataUrl := fmt.Sprintf("data:image/jpeg;base64,%s", base64Image)

                    	requestBody := map[string]interface{}{
                    		"input": map[string]interface{}{"prompt": "这张图片里有什么？", "image_list": []string{dataUrl}},
                    	}

                    	jsonData, _ := json.Marshal(requestBody)
                    	req, _ := http.NewRequest("POST", fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId), bytes.NewBuffer(jsonData))
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	client := &http.Client{}
                    	resp, _ := client.Do(req)
                    	defer resp.Body.Close()

                    	body, _ := io.ReadAll(resp.Body)
                    	fmt.Println(string(body))
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 响应对象 <span id="f304e81314ngo" />

        <strong>status\_code</strong> `string`

        返回的状态码。

        200表示请求成功，否则表示请求失败。

        请求失败可通过`code`获取错误码、`message`获取错误详细信息。

        > Java SDK不会返回该参数。调用失败会抛出异常，异常信息为<strong>status\_code</strong>和<strong>message</strong>的内容。

        <strong>request\_id</strong> `string`

        本次调用的唯一标识符。

        > Java SDK返回参数为`requestId`。

        <strong>code</strong> `string`

        表示错误码，调用成功时为空值。

        > 只有Python SDK返回该参数。

        <strong>message</strong> `string`

        表示错误详细信息，请求成功则忽略。

        > 只有Python SDK返回该参数。

        <strong>output</strong> `object`

        调用结果信息。

        <Accordion title="output属性" defaultOpen>
          <strong>text</strong> `string`

          模型生成的回复内容。

          <strong>finish\_reason</strong> `string`

          完成原因。

          `stop`为自然结束（遇预设标记），`null`为强制中断（如达到最大长度限制或手动停止）。

          <strong>session\_id</strong>`string`

          当前对话的唯一标识。

          在后续请求中传入，可携带历史对话记录。

          <strong>thoughts</strong>`array`

          调用时将`has_thoughts`参数设置为True，即可在`thoughts`中查看插件调用、知识检索的过程，或[深度思考模型](/zh/model-studio/deep-thinking)的思考过程。

          <Accordion title="thoughts属性">
            <strong>thought</strong> `string`

            模型的思考过程。

            当在控制台<strong>智能体应用</strong>中选择了[深度思考模型](/zh/model-studio/deep-thinking)，并成功发布应用后，若在 API 调用时将 `has_thoughts` 参数设为 `True`，则模型的思考过程将在此字段中返回。

            <strong>reasoningContent</strong>`string`

            模型的思考过程。

            当在控制台<strong>工作流应用</strong>中选择了[深度思考模型](/zh/model-studio/deep-thinking)，并成功发布应用后，若在 API 调用时将 `has_thoughts` 参数设为 `True`，则模型的思考过程将在此字段中返回。

            <strong>action\_type</strong> `string`

            大模型返回的执行步骤类型。如API表示执行API插件、agentRag表示执行知识检索、reasoning表示执行[深度思考模型](/zh/model-studio/deep-thinking)的思考过程。

            <strong>action\_name</strong> `string`

            执行的action名称，如知识检索、API插件、思考过程。

            <strong>action</strong> `string`

            执行的步骤。

            <strong>action\_input\_stream</strong> `string`

            入参的流式结果。

            <strong>action\_input</strong> `string`

            插件的输入参数。

            <strong>observation</strong> `string`

            检索或插件的过程。
          </Accordion>

          <strong>doc\_references</strong> `array`

          检索的召回文档中被模型引用的文档信息。

          在百炼控制台的<strong>智能体应用</strong>内，打开<strong>展示回答来源</strong>开关并<strong>发布</strong>应用，`doc_references`才可能包含有效信息。

          <Accordion title="doc_references属性">
            <strong>index\_id</strong> `string`

            模型引用的召回文档索引，如\[1]。

            <strong>title</strong> `string`

            模型引用的文本切片标题。

            <strong>doc\_id</strong> `string`

            模型引用的文档ID。

            <strong>doc\_name</strong> `string`

            模型引用的文档名。

            <strong>doc\_url</strong> `string`

            模型引用的文档链接，若文档保存于阿里云百炼平台的安全存储空间中时为`null`。

            <strong>text</strong> `string`

            模型引用的具体文本内容。

            <strong>biz\_id</strong> `string`

            模型引用的业务关联标识。

            <strong>images</strong> `array`

            模型引用的图片URL列表。

            <strong>page\_number</strong>`array`

            模型引用的文本切片的页码。

            > 此参数仅支持在2024年10月25日后创建的知识库。

            > 如需使用该参数，Python Dashscope SDK的版本至少应为1.20.14；Java Dashscope SDK的版本至少应为2.16.10。
          </Accordion>

          <strong>workflow\_message</strong>`object`

          包含工作流节点状态和消息的对象。

          <Note>
            <strong>工作流应用特有输出字段</strong>：

            - 此字段<strong>仅在调用工作流应用</strong>时返回，智能体应用不会返回此字段。
            - 在流式输出模式下，当`flow_stream_mode`设置为`message_format`或`message_format_plus`时，此字段会实时返回工作流各节点的执行状态和消息。
            - 在非流式输出模式下，此字段返回最终节点（<strong>结束</strong>节点或<strong>流程输出</strong>节点）的状态和消息。

            <strong>与智能体应用的输出区别</strong>：

            - <strong>智能体应用</strong>：主要通过`output.text`字段返回最终回复，`thoughts`字段（需`has_thoughts=true`）返回插件调用和知识检索过程。
            - <strong>工作流应用</strong>：除了`output.text`外，还通过`workflow_message`字段返回工作流节点的详细执行信息，包括节点名称、类型、状态和每个节点的输出内容。
          </Note>

          <Accordion title="workflow_message属性">
            <strong>node\_status</strong>`string`

            当前节点的执行状态，例如 executing（执行中）。

            <strong>node\_type</strong>`string`

            当前节点的类型，例如End（结束节点）。

            <strong>node\_msg\_seq\_id</strong>`integer`

            节点内消息的序列号。

            <strong>node\_name</strong>`string`

            当前节点的名称，例如结束。

            <strong>message</strong>`object`

            包含具体消息内容的对象。

            <Accordion title="message属性" defaultOpen>
              <strong>content</strong>`string`

              消息的具体文本内容。

              <strong>role</strong>`string`

              消息发送者的角色，例如Assistant。
            </Accordion>

            <strong>node\_is\_completed</strong>`boolean`

            指示当前节点是否已完成执行。true表示完成，false 表示未完成。

            <strong>node\_id</strong>`string`

            当前节点的唯一标识ID。

            <strong>parent\_node\_id</strong>`string`

            当前节点所属父节点（循环节点 / 批处理节点）的 ID。

            - 主画布节点：返回 null。
            - 子画布节点：返回所属循环节点或批处理节点的`node_id`。

            仅`message_format_plus`模式下子画布节点会返回非空值。
          </Accordion>

          <strong>子画布节点 content 结构</strong>（`message_format_plus`）

          当`flow_stream_mode=message_format_plus`且节点位于子画布内（`parent_node_id`非空）时，`message.content`为该次迭代/分支的完整 NodeResult JSON。

          <Accordion title="子画布节点 content 字段">
            <strong>nodeId</strong>`string`

            子画布内节点 ID。

            <strong>nodeName</strong>`string`

            节点名称。

            <strong>nodeType</strong>`string`

            节点类型，如 LLM、Plugin、Output 等。

            <strong>nodeStatus</strong>`string`

            节点执行状态：`success`（成功）/ `fail`（失败）/ `skip`（跳过，未执行）/ `executing`（执行中）。

            <strong>input</strong>`string`

            节点输入内容。

            <strong>output</strong>`string`

            节点输出内容。

            <strong>index</strong>`integer`

            子画布迭代/分支序号，从 0 起递增。

            <strong>parentNodeId</strong>`string`

            父节点（循环节点 / 批处理节点）ID。

            <strong>nodeExecTime</strong>`string`

            节点执行耗时，如`100ms`。

            <strong>errorCode</strong>`string`

            节点执行失败时返回。

            <strong>errorInfo</strong>`string`

            节点执行失败时返回。
          </Accordion>

          <Note>
            文本类节点（Output / End text 模式）和 LLM 等 JSON 类节点的处理规则与主画布一致：文本类是 delta 需拼接，JSON 类是快照需整体替换；`node_is_completed=true`标记节点结束。
          </Note>
        </Accordion>

        <strong>usage</strong> `object`

        表示本次请求使用的数据信息。

        <Accordion title="usage属性" defaultOpen>
          <strong>models</strong>`array`

          本次调用的模型信息。

          <Accordion title="models属性">
            <strong>model\_id</strong> `string`

            本次应用调用到的模型 ID。

            <strong>input\_tokens</strong> `integer`

            用户输入文本转换成Token后的长度。

            <strong>output\_tokens</strong> `integer`

            模型生成回复转换为Token后的长度。
          </Accordion>
        </Accordion>
      </td>

      <td>
        <strong>单轮对话响应示例</strong>

        ```json
        {
            "output": {
                "finish_reason": "stop",
                "session_id": "6105c965c31b40958a43dc93c28c7a59",
                "text": "我是千问，由阿里云开发的AI助手。我被设计用来回答各种问题、提供信息和与用户进行对话。有什么我可以帮助你的吗？"
            },
            "usage": {
                "models": [
                    {
                        "output_tokens": 36,
                        "model_id": "qwen-plus",
                        "input_tokens": 74
                    }
                ]
            },
            "request_id": "f97ee37d-0f9c-9b93-b6bf-bd263a232bf9"
        }
        ```

        <strong>指定知识库响应示例</strong>

        调用应用知识库功能时，想要输出召回文档中被模型引用的文档信息，可在百炼控制台的<strong>智能体应用</strong>内，单击<strong>检索配置</strong>，打开<strong>展示回答来源</strong>开关，<strong>发布</strong>应用。

        ```json
        {
            "text": "根据您的预算，我推荐您考虑百炼 Zephyr Z9。这款手机轻巧便携，拥有6.4英寸1080 x 2340像素的屏幕，搭配128GB存储与6GB RAM，非常适合日常使用<ref>[1]</ref>。此外，它还配备了4000mAh电池以及30倍数字变焦镜头，能够捕捉远处细节，价格区间在2499-2799元之间，完全符合您的预算需求<ref>[1]</ref>。",
            "finish_reason": "stop",
            "session_id": "6c1d47fa5eca46b2ad0668c04ccfbf13",
            "thoughts": null,
            "doc_references": [
                {
                    "index_id": "1",
                    "title": "百炼手机产品介绍",
                    "doc_id": "file_7c0e9abee4f142f386e488c9baa9cf38_10317360",
                    "doc_name": "百炼系列手机产品介绍",
                    "doc_url": null,
                    "text": "【文档名】:百炼系列手机产品介绍\n【标题】:百炼手机产品介绍\n【正文】:参考售价：5999- 6499。百炼 Ace Ultra ——游戏玩家之选：配备 6.67英寸 1080 x 2400像素屏幕，内置 10GB RAM与 256GB存储，确保游戏运行丝滑无阻。百炼 Ace Ultra ——游戏玩家之选：配备 6.67英寸 1080 x 2400像素屏幕，内置 10GB RAM与 256GB存储，确保游戏运行丝滑无阻。5500mAh电池搭配液冷散热系统，长时间游戏也能保持冷静。高动态双扬声器，沉浸式音效升级游戏体验。参考售价：3999- 4299。百炼 Zephyr Z9 ——轻薄便携的艺术：轻巧的 6.4英寸 1080 x 2340像素设计，搭配 128GB存储与 6GB RAM，日常使用游刃有余。4000mAh电池确保一天无忧，30倍数字变焦镜头捕捉远处细节，轻薄而不失强大。参考售价：2499- 2799。百炼 Flex Fold+ ——折叠屏新纪元：集创新与奢华于一身，主屏 7.6英寸 1800 x 2400像素与外屏 4.7英寸 1080 x 2400像素，多角度自由悬停设计，满足不同场景需求。512GB存储、12GB RAM，加之 4700mAh电池与 UTG超薄柔性玻璃，开启折叠屏时代新篇章。此外，这款手机还支持双卡双待、卫星通话，帮助您在世界各地都能畅联通话。参考零售价：9999- 10999。\n",
                    "biz_id": null,
                    "images": [

                    ],
                    "page_number": [
                        0]
                }]
        }
        ```

        <strong>异常响应示例</strong>

        在访问请求出错的情况下，输出的结果中会通过 code 和 message 指明错误原因。

        此处展示未传入正确`API-KEY`的异常响应示例。

        ```json
        request_id=1d14958f-0498-91a3-9e15-be477971967b,
        code=401,
        message=Invalid API-key provided.
        ```
      </td>
    </tr>
  </tbody>
</table>

## QPM限制 <span id="qpm_limit_h2_workflow" />

单应用默认QPM（每分钟请求数）为15000。

## 错误码 <span id="97e145aeabbwf" />

如果调用失败并返回报错信息，请参阅[错误码](/zh/model-studio/error-code)进行解决。
