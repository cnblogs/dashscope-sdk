> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Qwen-MT-Uni API参考

<meta property="og:title" content="Qwen-MT-Uni API参考" />

## 模型概览 <span id="uni-overview" />

Qwen-MT-Uni 是一款面向图片、文本、音频及各类文档的全模态翻译模型，通过统一的格式识别、智能路由、内容抽取、跨模态翻译与原格式重构链路，实现多类型输入的一体化高保真翻译。同时提供**同步调用**（请求后等待并直接返回翻译结果，适合文本、图片、小文件等耗时较短的场景）和**异步调用**（请求头加 `X-DashScope-Async: enable`，先返回 `task_id`，再通过查询接口轮询结果，适合大文档、长音频等耗时较长的场景）两种模式。

接口采用 DashScope 标准协议，同步调用顶层出现字符串 `code` 表示失败（错误码 = `code`，原因 = `message`），否则顶层有 `output` 表示成功（结果在 `output.Data`，用量在 `usage`）；异步调用需轮询查询接口，直到 `output.task_status = SUCCEEDED`（任务执行完毕），再看 `output.Success`：`true` 表示业务成功、`false` 表示业务失败（原因在 `output.Code`、`output.Message`）。

## 前提条件 <span id="uni-prerequisites" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## 支持的输入格式 <span id="uni-input-formats" />

- **字符串**：输入 `str` 或 `list[str]`，输出保持相同的标量/数组形状。
- **PDF**：输入 `.pdf`，输出 `.pdf`。
- **Word**：输入 `.docx`，输出 `.docx`。
- **PowerPoint**：输入 `.pptx`，输出 `.pptx`。
- **Excel**：输入 `.xlsx`，输出 `.xlsx`。
- **TXT**：输入 `.txt`，输出 `.txt`。
- **HTML**：输入 `.html`、`.htm`，输出 `.html`。
- **Markdown**：输入 `.md`、`.markdown`、`.mdown`、`.mkd`，输出 `.md`。
- **图像**：输入 `.png`、`.jpg`、`.jpeg`，输出 `.jpg`。
- **音频**：输入 `.mp3`、`.wav`，输出 `.mp3`。

<Note>
  * 单文件不超过 **100 MB**，文档不超过 **200 页**，音频时长需在 **3 秒～60 分钟** 之间。
  * URL 地址中不能包含中文字符。
  * 旧版二进制 Word（`.doc`）和 PowerPoint（`.ppt`）需要先转换为 OOXML 格式（`.docx`/`.pptx`）再传入。
</Note>

## 同步调用 <span id="uni-sync" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`，调用时请将 `{WorkspaceId}` 替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

同步模式下，请求会等待处理完成后直接返回翻译结果（文本或译后文件 URL），无需轮询任务状态。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数

        ##### 请求头（Headers）

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为 `application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼 API Key 进行身份认证。示例值：`Bearer sk-xxxx`。

        ##### 请求体（Request Body）

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，设置为 `qwen-mt-uni`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入参数对象，包含以下字段：

        <Accordion title="属性" defaultOpen>
          <strong>fileUrl</strong> `string` <strong>（条件必选）</strong>

          可访问的 HTTP(S) URL，服务端自动判断模态类型（文档 / 图片 / 音频）。

          - **支持格式**：PDF、DOCX、PPTX、XLSX、TXT、HTML、Markdown、JPG、PNG、MP3、WAV。
          - **大小限制**：单文件不超过 100 MB，文档不超过 200 页，音频时长需在 3 秒～60 分钟之间。
          - URL 地址中不能包含中文字符。
          - 与 `source_texts` **必须且只能提供一个**。

          <strong>source\_texts</strong> `string | array<string>` <strong>（条件必选）</strong>

          一条非空字符串或非空字符串数组。批量翻译保持输入顺序，响应保持标量/数组形状。与 `fileUrl` **必须且只能提供一个**。

          <strong>source\_lang</strong> `string` （可选）

          源语言代码，例如 `zh`。不填则自动识别。

          <strong>target\_lang</strong> `string` <strong>（必选）</strong>

          目标语言代码或项目支持的语言名称，例如 `ko`。详见[支持的语种](/zh/model-studio/qwen-mt-uni-api#uni-languages)。

          <strong>ext</strong> `object` （可选）

          扩展字段对象。

          <Accordion title="属性" defaultOpen>
            <strong>domainHint</strong> `string` （可选）

            英文领域提示，最多 200 个英文单词。为使译文风格更贴合特定领域，可以使用英文描述使用场景、译文风格等需求。

            <Tip>
              领域提示语句当前**只支持英文**。
            </Tip>

            <strong>示例：</strong>`These sentences are from seller-buyer conversations on a B2C ecommerce platform. Translate them into clear, engaging customer service language.`

            <strong>format\_hint</strong> `string` （可选）

            格式提示。当 `fileUrl` 没有可识别后缀时，用它显式指定格式，如 `pdf`、`image`。

            <strong>sensitives</strong> `array` （可选）

            敏感词列表，**区分大小写**，最多 50 个非空字符串。提取文本与某项完全相同时保留原文且不发送给模型。

            <strong>示例：</strong>`["全场9折", "七天无理由退换"]`

            <strong>glossary</strong> `array` （可选）

            术语表，最多 100 组 `{"src": "源文本", "tgt": "目标文本"}`。当前作为 Prompt 术语表接入，支持保持原文、指定翻译和空目标词。

            <strong>示例：</strong>`[{"src": "应用程序接口", "tgt": "API"}, {"src": "机器学习", "tgt": "ML"}]`

            <strong>config</strong> `object` （可选）

            <Accordion title="属性" defaultOpen>
              <strong>imageSegment</strong> `bool` （可选）

              **仅图像格式生效**。是否开启图像主体分割。开启后，将跳过对图像中主体（如人物、商品、Logo）上文字的翻译。

              - `false`：（默认值）翻译图像中的所有文字。
              - `true`：不翻译图像主体的文字。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td>
        ```bash
        curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
        --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
        --header 'Content-Type: application/json' \
        --data '{
            "model": "qwen-mt-uni",
            "input": {
                "fileUrl": "https://maas-infra-public.oss-cn-hangzhou.aliyuncs.com/lzhe/test/03bc3a6a-a02b-11f1-8874-00163e79676e.jpg",
                "target_lang": "zh"
            }
        }'
        ```
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
        #### 响应参数（成功）

        <strong>output</strong> `object`

        业务结果容器；成功时返回，失败时不返回。

        <Accordion title="属性" defaultOpen>
          <strong>Success</strong> `boolean`

          是否成功，成功为 `true`。

          <strong>Code</strong> `integer`

          业务结果码，成功为 `200`。

          <strong>Message</strong> `string`

          结果说明，成功固定为 `Success`。

          <strong>RequestId</strong> `string`

          本次调用的唯一 ID，便于问题排查。

          <strong>Data</strong> `object`

          翻译结果。

          <Accordion title="属性" defaultOpen>
            <strong>FileType</strong> `string`

            结果类型，可能值：`strings`（文本）/ `document`（文档）/ `image`（图片）/ `audio`（音频）。

            <strong>TranslatedTexts</strong> `string | array<string>`

            文本翻译结果。**仅传 `source_texts` 时返回**，形状与入参一致。

            <strong>TranslatedFileUrl</strong> `string`

            译后文件的下载地址（带签名，有有效期）。**仅传 `fileUrl` 时返回**。
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        Token 用量统计（在 output 之外的顶层），按 `input_tokens` 计费。

        <Accordion title="属性" defaultOpen>
          <strong>input\_tokens</strong> `integer`

          输入 Token 总量。

          <strong>output\_tokens</strong> `integer`

          输出 Token 总量。

          <strong>total\_tokens</strong> `integer`

          输入与输出 Token 总量。

          <strong>input\_tokens\_details</strong> `object`

          按内容类型划分的输入 Token 用量明细，包含 `document_tokens`、`audio_tokens`、`character_tokens`、`image_tokens`。

          <strong>output\_tokens\_details</strong> `object`

          按内容类型划分的输出 Token 用量明细，字段同 `input_tokens_details`。
        </Accordion>

        <strong>request\_id</strong> `string`

        顶层请求 ID，成功和失败均返回。

        #### 响应参数（失败）

        <strong>code</strong> `string`

        错误码字符串，如 `InvalidParameter`。**仅失败返回**。

        <strong>message</strong> `string`

        错误详情，形如 `InvalidParameter: <原因>`。**仅失败返回**。

        <strong>request\_id</strong> `string`

        请求 ID。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应">
            ```json
            {
                "output": {
                    "Code": 200,
                    "Data": {
                        "FileType": "image",
                        "TranslatedFileUrl": "http://dashscope-a717.oss-cn-beijing.aliyuncs.com/xxx.jpg?Expires=xxx"
                    },
                    "Message": "Success",
                    "RequestId": "a20aa16a-2445-929f-9130-af70b4a0ade4",
                    "Success": true
                },
                "usage": {
                    "input_tokens": 1887,
                    "input_tokens_details": {
                        "audio_tokens": 0,
                        "character_tokens": 0,
                        "document_tokens": 0,
                        "image_tokens": 1887
                    },
                    "output_tokens": 1887,
                    "output_tokens_details": {
                        "audio_tokens": 0,
                        "character_tokens": 0,
                        "document_tokens": 0,
                        "image_tokens": 1887
                    },
                    "total_tokens": 3774
                },
                "request_id": "a20aa16a-2445-929f-9130-af70b4a0ade4"
            }
            ```
          </Tab>

          <Tab title="异常响应">
            请参见[错误码](/zh/model-studio/qwen-mt-uni-api#uni-errors)进行解决。

            ```json
            {
                "code": "InvalidParameter",
                "message": "InvalidParameter: missing required field: input.target_lang",
                "request_id": "a20aa16a-2445-929f-9130-af70b4a0ade4"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 异步调用 <span id="uni-async" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`，调用时请将 `{WorkspaceId}` 替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

异步调用流程分两步：

1. **创建任务获取任务 ID**：发送一个请求创建任务，该请求会返回**任务 ID（task\_id）**。
2. **根据任务 ID 查询结果**：使用 `task_id` 轮询任务状态，直到任务完成并获得译后文件 URL 或译文文本。

### 步骤1：创建任务获取任务 ID <span id="uni-async-create" />

<Note>
  - 创建成功后，使用接口返回的 `task_id` 查询结果，`task_id` 有效期为 24 小时。**请勿重复创建任务**，轮询获取即可。
  - 请求参数与同步调用**完全一致**，只需在请求头额外加上 `X-DashScope-Async: enable`。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="uni-async-req" />

        ##### 请求头（Headers）

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为 `application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼 API Key 进行身份认证。示例值：`Bearer sk-xxxx`。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。**必须设置为** `enable`。缺少此请求头将使用同步模式。

        ##### 请求体（Request Body）

        请求体字段与[同步调用](/zh/model-studio/qwen-mt-uni-api#uni-sync)完全一致，包括 `model`、`input`（`fileUrl`/`source_texts`、`source_lang`、`target_lang`、`ext`）等参数。
      </td>

      <td>
        ```bash
        curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
        --header 'X-DashScope-Async: enable' \
        --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
        --header 'Content-Type: application/json' \
        --data '{
            "model": "qwen-mt-uni",
            "input": {
                "fileUrl": "https://maas-infra-public.oss-cn-hangzhou.aliyuncs.com/lzhe/test/03bc3a6a-a02b-11f1-8874-00163e79676e.jpg",
                "target_lang": "zh"
            }
        }'
        ```
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
        #### 响应参数 <span id="uni-async-resp" />

        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          任务 ID。查询有效期 24 小时。

          <strong>task\_status</strong> `string`

          任务状态。

          <Accordion title="枚举值" defaultOpen>
            - `PENDING`：任务排队中
            - `RUNNING`：任务处理中
            - `SUCCEEDED`：任务执行完成，需结合 `output.Success` 判断业务是否成功
            - `FAILED`：任务执行失败
            - `UNKNOWN`：任务不存在或状态未知
          </Accordion>
        </Accordion>

        <strong>request\_id</strong> `string`

        请求唯一标识，可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应">
            请保存 `task_id`，用于查询任务状态与结果。

            ```json
            {
                "output": {
                    "task_id": "5dfd1549-e956-4f26-8802-6c9e06f704c6",
                    "task_status": "PENDING"
                },
                "request_id": "fe9a9059-02dd-497c-a62c-8e27a0081c02"
            }
            ```
          </Tab>

          <Tab title="异常响应">
            创建任务失败，请参见[错误码](/zh/model-studio/qwen-mt-uni-api#uni-errors)进行解决。

            ```json
            {
                "code": "InvalidParameter",
                "message": "InvalidParameter: missing required field: input.target_lang",
                "request_id": "a20aa16a-2445-929f-9130-af70b4a0ade4"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务 ID 查询结果 <span id="uni-async-query" />

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<Note>
  - `task_id` 有效期为 **24 小时**，若 ID 不存在或已过期，任务状态将返回 `UNKNOWN`。
  - 任务成功后返回的 `TranslatedFileUrl` 有效期为 **24 小时**，请及时下载并保存文件。
  - 此查询接口的默认 RPS 为 1。如需更高频次的查询或事件通知，请[配置异步任务回调](/zh/model-studio/async-task-api)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数

        ##### 请求头（Headers）

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼 API Key 进行身份认证。示例值：`Bearer sk-xxxx`。

        ##### URL 路径参数（Path parameters）

        <strong>task\_id</strong> `string` <strong>（必选）</strong>

        任务 ID，由创建任务接口返回。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            您需要将 `86ecf553-d340-4e21-xxxxxxxxx` 替换为真实的 `task_id`。

            ```bash
            curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY"
            ```
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
        #### 响应参数

        <Note>
          **字段返回时机**：`task_status` 为 `PENDING` / `RUNNING` 时，`TranslatedFileUrl`、`TranslatedTexts`、`usage`、`end_time` 均为空，调用方需继续轮询。
        </Note>

        <strong>output</strong> `object`

        异步任务状态及执行结果。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          异步任务的唯一标识；三种状态均返回。

          <strong>task\_status</strong> `string`

          异步任务状态。`PENDING`：等待处理；`RUNNING`：处理中；`SUCCEEDED`：任务执行完成。**业务失败示例中该字段仍为 `SUCCEEDED`，需结合 `output.Success` 判断业务是否成功。**

          <strong>submit\_time</strong> `string`

          任务提交时间，格式为 `yyyy-MM-dd HH:mm:ss.SSS`。成功和失败示例均返回。

          <strong>scheduled\_time</strong> `string`

          任务开始调度时间，格式为 `yyyy-MM-dd HH:mm:ss.SSS`。成功和失败示例均返回。

          <strong>end\_time</strong> `string`

          任务结束时间，格式为 `yyyy-MM-dd HH:mm:ss.SSS`。成功和失败示例均返回。

          <strong>Success</strong> `boolean`

          业务执行是否成功。`true`：成功；`false`：失败。**待处理示例不返回。**

          <strong>Code</strong> `integer`

          业务结果码。`200` 表示成功，`400` 表示请求参数错误。待处理示例不返回。

          <strong>Message</strong> `string`

          业务执行结果说明。成功示例为 `Success`；失败时返回错误详情，例如 `InvalidParameter: missing required field: input.target_lang`。

          <strong>RequestId</strong> `string`

          业务执行请求标识。成功和失败示例均返回。

          <strong>Data</strong> `object`

          翻译结果数据；**仅业务成功时返回**。

          <Accordion title="属性" defaultOpen>
            <strong>FileType</strong> `string`

            结果类型：`strings` / `document` / `image` / `audio`。

            <strong>TranslatedFileUrl</strong> `string`

            译后文件的下载地址（带签名及有效期参数）。**仅传 `fileUrl` 时返回**。

            <strong>TranslatedTexts</strong> `string | array<string>`

            文本翻译结果。**仅传 `source_texts` 时返回**，形状与入参一致。
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        Token 用量统计。成功示例返回详细统计；失败示例返回空对象 `{}`；待处理示例不返回。字段与同步调用一致。

        <strong>request\_id</strong> `string`

        本次查询请求的唯一标识；三种状态均返回。
      </td>

      <td>
        <Tabs>
          <Tab title="处理中">
            ```json
            {
                "request_id": "6e8d1dd6-9e02-9793-ad25-6e7fda5eeea0",
                "output": {
                    "task_id": "df8fc25e-b9ee-4e96-9aa2-24d69681299e",
                    "task_status": "PENDING"
                }
            }
            ```
          </Tab>

          <Tab title="处理成功">
            ```json
            {
                "request_id": "47bb236e-4ecc-90a7-ac39-9ab3435d2cc8",
                "output": {
                    "task_id": "9a7ccda6-4604-4541-a2f3-f9d412c0779b",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-09-15 13:35:06.883",
                    "scheduled_time": "2026-09-15 13:35:06.931",
                    "end_time": "2026-09-15 13:35:10.149",
                    "Message": "Success",
                    "RequestId": "9a7ccda6-4604-4541-a2f3-f9d412c0779b",
                    "Data": {
                        "FileType": "image",
                        "TranslatedFileUrl": "http://dashscope-a717.oss-cn-beijing.aliyuncs.com/xxx.jpg?Expires=xxx"
                    },
                    "Code": 200,
                    "Success": true
                },
                "usage": {
                    "input_tokens_details": {
                        "document_tokens": 0,
                        "audio_tokens": 0,
                        "character_tokens": 0,
                        "image_tokens": 1887
                    },
                    "total_tokens": 3774,
                    "output_tokens": 1887,
                    "input_tokens": 1887,
                    "output_tokens_details": {
                        "document_tokens": 0,
                        "audio_tokens": 0,
                        "character_tokens": 0,
                        "image_tokens": 1887
                    }
                }
            }
            ```
          </Tab>

          <Tab title="处理失败">
            业务失败时 `task_status` 仍为 `SUCCEEDED`，需通过 `output.Success = false` 判断。

            ```json
            {
                "request_id": "<查询请求id>",
                "output": {
                    "task_id": "<task-id>",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-09-14 11:09:36.876",
                    "scheduled_time": "2026-09-14 11:09:36.926",
                    "end_time": "2026-09-14 11:09:37.050",
                    "Success": false,
                    "Code": 400,
                    "Message": "InvalidParameter: missing required field: input.target_lang",
                    "RequestId": "<=task_id>"
                },
                "usage": {}
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 支持的语种 <span id="uni-languages" />

若不确定源语种，可将 `source_lang` 留空以自动识别。下表列出全部受支持的翻译方向：

| 源语种（中文名）   | 源语种（编码） | 可选目标语种                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| ---------- | ------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 中文（简体）     | `zh`    | 中文（繁体）（`zh-tw`）、英语（`en`）、日语（`ja`）、韩语（`ko`）、哈萨克语（`kk`）、马来语（`ms`）、泰语（`th`）                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 中文（繁体）     | `zh-tw` | 中文（简体）（`zh`）、英语（`en`）、日语（`ja`）、韩语（`ko`）、哈萨克语（`kk`）、马来语（`ms`）、泰语（`th`）                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
| 英语         | `en`    | 阿拉伯语（`ar`）、阿塞拜疆语（`az`）、孟加拉语（`bn`）、波斯尼亚语（`bs`）、捷克语（`cs`）、丹麦语（`da`）、德语（`de`）、希腊语（`el`）、西班牙语（`es`）、爱沙尼亚语（`et`）、芬兰语（`fi`）、法语（`fr`）、希伯来语（`he`）、印地语（`hi`）、匈牙利语（`hu`）、印度尼西亚语（`id`）、意大利语（`it`）、日语（`ja`）、韩语（`ko`）、立陶宛语（`lt`）、拉脱维亚语（`lv`）、马来语（`ms`）、缅甸语（`my`）、尼泊尔语（`ne`）、荷兰语（`nl`）、挪威语（`no`）、波兰语（`pl`）、葡萄牙语-巴西（`pt`）、葡萄牙语-葡萄牙（`pt-pt`）、罗马尼亚语（`ro`）、罗马尼亚语-乌尔都（`ro_ur`）、俄语（`ru`）、僧伽罗语（`si`）、斯洛伐克语（`sk`）、斯洛文尼亚语（`sl`）、塞尔维亚语（`sr`）、瑞典语（`sv`）、泰语（`th`）、菲律宾语（`tl`）、土耳其语（`tr`）、乌克兰语（`uk`）、乌尔都语（`ur`）、越南语（`vi`）、中文（简体）（`zh`）、中文（繁体）（`zh-tw`） |
| 西班牙语       | `es`    | 保加利亚语（`bg`）、捷克语（`cs`）、丹麦语（`da`）、德语（`de`）、希腊语（`el`）、英语（`en`）、爱沙尼亚语（`et`）、芬兰语（`fi`）、法语（`fr`）、克罗地亚语（`hr`）、匈牙利语（`hu`）、意大利语（`it`）、立陶宛语（`lt`）、拉脱维亚语（`lv`）、荷兰语（`nl`）、挪威语（`no`）、波兰语（`pl`）、葡萄牙语-巴西（`pt`）、葡萄牙语-葡萄牙（`pt-pt`）、罗马尼亚语（`ro`）、俄语（`ru`）、斯洛伐克语（`sk`）、瑞典语（`sv`）                                                                                                                                                                                                                                                               |
| 阿拉伯语       | `ar`    | 英语（`en`）、土耳其语（`tr`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| 土耳其语       | `tr`    | 阿拉伯语（`ar`）、捷克语（`cs`）、德语（`de`）、希腊语（`el`）、英语（`en`）、匈牙利语（`hu`）、罗马尼亚语（`ro`）、斯洛伐克语（`sk`）                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| 罗马尼亚语      | `ro`    | 英语（`en`）、土耳其语（`tr`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| 阿塞拜疆语      | `az`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 孟加拉语       | `bn`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 捷克语        | `cs`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 德语         | `de`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 希腊语        | `el`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 芬兰语        | `fi`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 法语         | `fr`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 希伯来语       | `he`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 印地语        | `hi`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 匈牙利语       | `hu`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 印度尼西亚语     | `id`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 意大利语       | `it`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 日语         | `ja`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 韩语         | `ko`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 马来语        | `ms`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 缅甸语        | `my`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 尼泊尔语       | `ne`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 荷兰语        | `nl`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 波兰语        | `pl`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 葡萄牙语（巴西）   | `pt`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 俄语         | `ru`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 僧伽罗语       | `si`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 瑞典语        | `sv`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 泰语         | `th`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 菲律宾语       | `tl`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 乌克兰语       | `uk`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 乌尔都语       | `ur`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 越南语        | `vi`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 保加利亚语      | `bg`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 波斯尼亚语      | `bs`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 丹麦语        | `da`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 爱沙尼亚语      | `et`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 克罗地亚语      | `hr`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 立陶宛语       | `lt`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 拉脱维亚语      | `lv`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 挪威语        | `no`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 葡萄牙语（葡萄牙）  | `pt-pt` | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 罗马尼亚语（乌尔都） | `ro_ur` | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 斯洛伐克语      | `sk`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 斯洛文尼亚语     | `sl`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 塞尔维亚语      | `sr`    | 英语（`en`）                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |

## 计费与限流 <span id="uni-billing" />

- 模型免费额度和计费单价请参见[模型价格](/zh/model-studio/model-pricing#uni-pricing)。
- 模型限流请参见[限流](/zh/model-studio/rate-limit#uni-rate-limit)。
- 计费说明：按 `input_tokens` 计费。同步与异步价格一致。
- **注意**：模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="uni-errors" />

请参见[错误码](/zh/model-studio/error-code)。
