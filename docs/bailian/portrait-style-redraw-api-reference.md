> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 人像风格重绘API参考

> 人像风格重绘模型支持将人物照片，转换为多种预设或自定义的艺术风格。

<meta property="og:title" content="人像风格重绘API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

<Note>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议从 `https://dashscope.aliyuncs.com` 迁移至新域名。

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Note>

<table><tbody><tr><td><p><strong>快速入口：</strong><Link href="/zh/model-studio/style-repaint">使用指南</Link> ｜ <Link href="/zh/model-studio/first-call-to-image-and-video-api">HTTP调用新手指南</Link> ｜ <Link href="/zh/model-studio/new-free-quota">新人免费额度</Link> ｜ <Link href="/zh/model-studio/style-repaint#d34d4c23aew5n">计费与限流</Link></p></td></tr></tbody></table>

## 模型概览 <span id="72e2f172021m9" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "23.189927%" }} /><col style={{ width: "13.011542%" }} /><col style={{ width: "20.566632%" }} /><col style={{ width: "21.301154%" }} /><col style={{ width: "21.930745%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th rowSpan={2}><p><strong>计费单价</strong></p></th><th colSpan={2}><p><strong>限流（主账号与RAM子账号共享）</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a></p></th></tr><tr><th><p><strong>任务下发接口QPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>wanx-style-repaint-v1</p></td><td><p>0.12元/张</p></td><td><p>2</p></td><td><p>1</p></td><td><p>500张</p></td></tr></tbody></table>

## 前提条件 <span id="cd03ebba5cblt" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="65796f7146azl" />

本模型<strong>仅提供 HTTP API，暂无SDK</strong>。由于图像生成耗时较长，API 采用异步模式，调用流程分两步：

1. <strong>创建任务</strong>：创建人像风格重绘任务，获取唯一任务ID（task\_id）。
2. <strong>查询结果</strong>：使用 `task_id` 轮询任务状态，直至完成并获取生成的图像URL。图像URL有效期为24小时。

<Note>
  - 创建任务后，系统将立即返回一个 `task_id`。在步骤2中使用该 `task_id` 查询任务结果，有效期24小时。
  - 如需集成至现有项目，需自行实现对应语言的 HTTP 调用逻辑。部分示例代码请参见[人像风格重绘](/zh/model-studio/style-repaint)。
  - HTTP调用新手指南请参见[Postman](/zh/model-studio/first-call-to-image-and-video-api)。
</Note>

### 步骤1：创建任务获取任务ID <span id="161d4c0111m87" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求头（Headers） <span id="afcd41d2b57zf" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        #### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。示例值：wanx-style-repaint-v1。

        <strong>input</strong> `object` （必选）

        输入图像的基本信息，比如图像URL地址。

        <Accordion title="属性" defaultOpen>
          <strong>image\_url</strong> `string` <strong>（必选）</strong>

          输入的图像URL地址。

          - 支持公网可访问的HTTP/HTTPS地址，不包含中文字符。
          - 支持传入Base64编码字符串。
          - 对于本地文件，可通过以下两种方式获取合法参数值：

            - 获取URL：请参见[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。
            - 生成Base64编码字符串：请参见[图像Base64编码传值方式](/zh/model-studio/style-repaint#fa664addf5wph)。

          图像限制：

          - 图像分辨率：不低于256×256像素，不超过5760×3240像素。
          - 图像格式：JPEG、PNG、JPG、BMP、WEBP。
          - 图像比例：长短边比例不超过 2:1。
          - 图片大小：不超过10M。
          - 图像质量：为确保生成质量，请上传脸部清晰照片，人脸比例不宜过小，并避免夸张姿势和表情。

          <strong>style\_index</strong> `integer` <strong>（必选）</strong>

          选择一个预置的风格索引值，即可生成风格化人像。风格化效果请参考[使用指南](/zh/model-studio/style-repaint)。

          - -1：使用参考图像风格（需提供`style_ref_url`）
          - 0：复古漫画
          - 1：3D童话
          - 2：二次元
          - 3：小清新
          - 4：未来科技
          - 5：国画古风
          - 6：将军百战
          - 7：炫彩卡通
          - 8：清雅国风
          - 9：喜迎新年
          - 14：国风工笔
          - 15：恭贺新禧
          - 30：童话世界
          - 31：黏土世界
          - 32：像素世界
          - 33：冒险世界
          - 34：日漫世界
          - 35：3D世界
          - 36：二次元世界
          - 37：手绘世界
          - 38：蜡笔世界
          - 39：冰箱贴世界
          - 40：吧唧世界

          <strong>style\_ref\_url</strong> `string` （可选）

          当`style_index=-1`时，必须传入，其他风格无需传入。

          风格参考图像URL地址。风格参考效果请参考[使用指南](/zh/model-studio/style-repaint)。

          - 支持公网可访问的HTTP/HTTPS地址，不包含中文字符。
          - 支持传入Base64编码字符串。
          - 对于本地文件，可通过以下两种方式获取合法参数值：

            - 获取URL：请参见[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。
            - 生成Base64编码字符串：请参见[图像Base64编码传值方式](/zh/model-studio/style-repaint#fa664addf5wph)。

          图像限制：

          - 图片分辨率：不小于256×256像素且不超过5760×3240像素。
          - 图像比例：为取得最佳效果，建议图像长短边比例不超过 2:1，否则可能影响生成或导致报错。
          - 图片格式：JPEG、PNG、JPG、BMP、WEBP。
          - 图片大小：不超过10M。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="使用预置风格">
            设置style\_index（不能设为-1）。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "wanx-style-repaint-v1",
                "input": {
                    "image_url": "https://vigen-video.oss-cn-shanghai.aliyuncs.com/demo_image/image_demo_input.png",
                    "style_index": 3
                }
            }'
            ```
          </Tab>

          <Tab title="使用自定义风格">
            设置style\_ref\_url（风格参考图），并将 style\_index 设为 -1。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "wanx-style-repaint-v1",
                "input": {
                    "image_url": "https://vigen-video.oss-cn-shanghai.aliyuncs.com/demo_image/input_example.png",
                    "style_ref_url": "https://vigen-video.oss-cn-shanghai.aliyuncs.com/demo_image/style_example.png",
                    "style_index": -1
                }
            }'
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
        #### 响应 <span id="cd3063363egdc" />

        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          任务ID。查询有效期24小时。

          <strong>task\_status</strong> `string`

          任务状态。

          <Accordion title="枚举值" defaultOpen>
            - PENDING：任务排队中
            - RUNNING：任务处理中
            - SUCCEEDED：任务执行成功
            - FAILED：任务执行失败
            - CANCELED：任务已取消
            - UNKNOWN：任务不存在或状态未知
          </Accordion>
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。

        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应">
            请保存 task\_id，用于查询任务状态与结果。

            ```json
            {
                "output": {
                    "task_status": "PENDING",
                    "task_id": "0385dc79-5ff8-4d82-bcb6-xxxxxx"
                },
                "request_id": "4909100c-7b5a-9f92-bfe5-xxxxxx"
            }
            ```
          </Tab>

          <Tab title="异常响应">
            创建任务失败，请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "code": "InvalidApiKey",
                "message": "No API-key provided.",
                "request_id": "7438d53d-6eb8-4596-8835-xxxxxx"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="0578e132d2n1d" />

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Note>
  - 模型耗时约15秒。建议采用轮询机制，并设置合理的查询间隔（如 3 秒）来获取结果。
  - `task_id`查询有效期为<strong>24小时</strong>，超时后将无法查询结果，系统将返回任务状态为`UNKNOWN`。
  - 任务成功后返回的 `url`有效期为<strong>24小时</strong>，请及时下载并保存图像。
  - 此查询接口的默认QPS为20。如需更高频次的查询或事件通知，请[配置异步任务回调](/zh/model-studio/async-task-api)。
  - 如需批量查询或取消任务，请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求头（Headers） <span id="5043fa882c26r" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        #### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            将`{task_id}`完整替换为上一步接口返回的`task_id`的值。`task_id`查询有效期为24小时，并请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

            ```bash
            curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id} \
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
        #### 响应 <span id="f641893b4bykq" />

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          任务ID。查询有效期24小时。

          <strong>task\_status</strong> `string`

          任务状态。

          <Accordion title="枚举值" defaultOpen>
            - PENDING：任务排队中
            - RUNNING：任务处理中
            - SUCCEEDED：任务执行成功
            - FAILED：任务执行失败
            - CANCELED：任务已取消
            - UNKNOWN：任务不存在或状态未知
          </Accordion>

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>start\_time</strong> `string`

          任务开始时间。

          <strong>error\_message</strong> `string`

          错误信息。正常请求时返回，可忽略。

          <strong>error\_code</strong> `integer`

          错误码。正常请求时返回，可忽略。

          <strong>style\_index</strong> `integer`

          返回指定所选风格的索引值。

          <strong>results</strong> `array object`

          任务结果列表，包括图像URL、prompt、部分任务执行失败报错信息等。

          <Accordion title="数据结构">
            ```json
            {
                "results": [
                    {
                        "orig_prompt": "",
                        "actual_prompt": "",
                        "url": ""
                    },
                    {
                        "code": "",
                        "message": ""
                    }
                ]
            }
            ```
          </Accordion>

          <strong>task\_metrics</strong> `object`

          任务结果统计。

          <Accordion title="属性" defaultOpen>
            <strong>TOTAL</strong> `integer`

            总的任务数。

            <strong>SUCCEEDED</strong> `integer`

            任务状态为成功的任务数。

            <strong>FAILED</strong> `integer`

            任务状态为失败的任务数。
          </Accordion>

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          模型成功生成图片的数量。计费公式：费用 = 图片数量 × 单价。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json
            {
                "request_id": "f7fee4f1-1f68-9f17-85df-xxxxx",
                "output": {
                    "task_id": "316c7af0-e91f-476f-99bd-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-08-12 10:55:43.768",
                    "scheduled_time": "2025-08-12 10:55:43.799",
                    "end_time": "2025-08-12 10:55:48",
                    "error_message": "Success",
                    "start_time": "2025-08-12 10:55:43",
                    "style_index": 0,
                    "error_code": 0,
                    "results": [
                        {
                            "url": "http://oss.aliyuncs.com/xxx/abc.jpg"
                        }
                    ]
                },
                "usage": {
                    "image_count": 1
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
              "request_id": "<your request id>",
              "output": {
                "task_id": "<your task id>",
                "task_status": "FAILED",
                "submit_time": "xxx",
                "scheduled_time": "xxx",
                "end_time": "xxx",
                "code": "InvalidImageResolution",
                "message": "The input image resolution is too large or small"
              },
              "usage": {
                "image_num": 0
              }
            }
            ```
          </Tab>

          <Tab title="任务执行中">
            ```json
            {
                "request_id":"e5d70b02-ebd3-98ce-9fe8-759d7d7b107d",
                "output":{
                    "task_id":"86ecf553-d340-4e21-af6e-a0c6a421c010",
                    "task_status":"RUNNING",
                    "task_metrics":{
                        "TOTAL":1,
                        "SUCCEEDED":1,
                        "FAILED":0
                    }
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="9c31ff011bxae" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

此API还有特定状态码，具体如下所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "14.498934%" }} /><col style={{ width: "16.950959%" }} /><col style={{ width: "39.01919%" }} /><col style={{ width: "29.530917%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidURL</p></td><td><p>The request URL is invalid, make sure the url is correct and is an image</p></td><td><p>输入URL错误，请确保URL链接的正确性</p></td></tr><tr><td><p>400</p></td><td><p>InvalidImageResolution</p></td><td><p>The input image resolution is too large or small</p></td><td><p>输入图像分辨率过大或过小</p></td></tr><tr><td><p>400</p></td><td><p>InvalidImageFormat</p></td><td><p>The input image is in invalid format</p></td><td><p>输入图像格式无效</p></td></tr></tbody></table>

## 常见问题 <span id="3a78f4e212bx9" />

<strong>Q：调用风格重绘接口报错</strong>`"code":"InvalidImageFormat","message":"The input image is in invalid format"}`<strong>？</strong>

A：输出图像格式不符合要求，请查看本文档中图像参数的使用说明。

<strong>Q：输出图像的尺寸是否与输入图像一致？</strong>

A：<strong>不一致。</strong> 输出图像会保持输入图像的宽高比，但会将短边固定为 1536 像素，长边按比例缩放。
