> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyHorse-参考生视频API参考

> HappyHorse-参考生视频模型支持传入 多张参考图像 ，通过 文本提示词 描述场景，将图像中的主体角色融合生成一段流畅的视频。

<meta property="og:title" content="HappyHorse-参考生视频API参考" />

## 适用范围 <span id="hhr003a0h2scp" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Note>
  本文的示例代码适用于<strong>华北2（北京）地域</strong>。
</Note>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP调用 <span id="hhr101a0h2http" />

由于参考生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="hhr111a0h4stp1" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
  </Tab>

  <Tab title="德国（法兰克福）">
    `POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
  </Tab>

  <Tab title="日本（东京）">
    `POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Note>
  - 创建成功后，使用接口返回的 `task_id` 查询结果，task\_id 有效期为 24 小时。<strong>请勿重复创建任务</strong>，轮询获取即可。
  - 新手指引请参见[Postman](/zh/model-studio/first-call-to-image-and-video-api)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="hhr123a0h4reqp" />

        ##### 请求头（Headers） <span id="hhr144a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="hhr156a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。

        可选值：

        - `happyhorse-1.1-r2v`
        - `happyhorse-1.0-r2v`

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，包括参考图像和提示词。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持任何语言输入，长度不超过5000个非中文字符或2500个中文字符，超过部分会自动截断。

          <strong>参考指代</strong>：在prompt中通过“<strong>\[Image 1]、\[Image 2]</strong>”标识指代`media`数组中对应位置的参考图像，顺序与`media`数组顺序一致。使用时需要指明参考图中的具体对象，例如“\[Image 1]中身着红色旗袍的女性”。

          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体素材列表，用于指定参考图像。

          数组的每个元素为一个媒体对象，包含 `type`和 `url`字段。

          - 按照数组顺序定义`prompt`中角色引用的顺序。
          - 数组中的第 1 个`reference_image`对应 <strong>\[Image 1]</strong>，第 2 个对应 <strong>\[Image 2]</strong>，以此类推。

          <Accordion title="元素属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。固定值为：

            - `reference_image`：参考图像。

            素材限制：

            - 参考图像数量：1～9张。

            <strong>url</strong> `string` <strong>（必选）</strong>

            参考图像URL或 Base64 编码数据。

            图像限制：

            - 格式：JPEG、JPG、PNG、WEBP。
            - 分辨率：短边不低于400像素，推荐720P以上清晰图。避免传入过小、模糊或压缩过度的图像，影响效果。
            - 文件大小：不超过20MB。

            支持输入的格式：

            1. 公网URL：

               - 支持 HTTP 或 HTTPS 协议。
               - 示例值：<a href="https://xxx/xxx.jpg">[https://xxx/xxx.jpg](https://xxx/xxx.jpg)</a>。
            2. Base64 编码图像后的字符串：

               - 数据格式：`data:{MIME_type};base64,{base64_data}`。
               - 示例值：data:image/png;base64,GDU7MtCZzEbTbmRZ......（示例已截断，仅做演示）。

                 <Accordion title="Base64编码数据格式" defaultOpen>
                   格式： `data:{MIME_type};base64,{base64_data}` 。

                   - \{base64\_data}：图像文件经过 Base64 编码后的字符串。
                   - \{MIME\_type}：图像的媒体类型，需与文件格式对应。

                   <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><tbody><tr><td><p>图像格式</p></td><td><p>MIME Type</p></td></tr><tr><td><p>JPEG</p></td><td><p>image/jpeg</p></td></tr><tr><td><p>JPG</p></td><td><p>image/jpeg</p></td></tr><tr><td><p>PNG</p></td><td><p>image/png</p></td></tr><tr><td><p>WEBP</p></td><td><p>image/webp</p></td></tr></tbody></table>
                 </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频生成参数。如设置视频分辨率、宽高比、时长等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` （可选）

          生成视频的分辨率档位。

          可选值：

          - `480P`
          - `720P`
          - `1080P`：默认值。

          <strong>ratio</strong> `string` （可选）

          生成视频的宽高比。

          可选值：

          - `16:9`：默认值。
          - `9:16`
          - `3:4`
          - `4:3`
          - `4:5`
          - `5:4`
          - `1:1`
          - `9:21`
          - `21:9`

          <strong>duration</strong> `integer` （可选）

          生成视频的时长，单位为秒。

          取值范围：`3~15`之间的整数。

          默认值：`5`。

          <strong>watermark</strong> `boolean` （可选）

          是否在生成的视频上添加水印标识。水印位于视频右下角，文案固定为“Happy Horse”。

          - `true`：默认值，添加水印。
          - `false`：不添加水印。

          <strong>seed</strong>`integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`。

          未指定时，系统自动生成随机种子。若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="参考生视频（多图像）">
            ```bash expandable
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "happyhorse-1.1-r2v",
                "input": {
                    "prompt": "[Image 1]中身着红色旗袍的女性，镜头先以侧面中景勾勒旗袍修身剪裁与S型曲线，随即切换至低角度仰拍，捕捉她轻抬玉手展开[Image 2]中的折扇的同时，[Image 3]中的流苏耳坠随头部转动轻盈摆动的细节，最后推近至面部特写，定格在她指尖轻点扇骨、眼波流转间的含蓄风情，多视角全方位展现东方韵味。",
                    "media": [
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/mvzfud/hh-v2v-girl.jpg"
                        },
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/fvuihk/hh-v2v2-folding-fan.jpg"
                        },
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/imerii/hh-v2v-earrings.jpg"
                        }
                    ]
                },
                "parameters": {
                    "resolution": "720P",
                    "ratio": "16:9",
                    "duration": 5
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
        #### 响应参数 <span id="hhr379a0h4rsp1" />

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

### 步骤2：根据任务ID查询结果 <span id="hhr411a0h4stp2" />

<Tabs>
  <Tab title="华北2（北京）">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="德国（法兰克福）">
    `GET https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="日本（东京）">
    `GET https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>
</Tabs>

<Note>
  - <strong>轮询建议</strong>：视频生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 15 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
  - <strong>task\_id 有效期</strong>：<strong>24小时</strong>，超时后将无法查询结果，接口将返回任务状态为`UNKNOWN`。
  - <strong>RPS 限制</strong>：查询接口默认RPS为20。如需更高频查询或事件通知，建议[配置异步任务回调](/zh/model-studio/async-task-api)。
  - <strong>更多操作</strong>：如需批量查询、取消任务等操作，请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="hhr434a0h4qr01" />

        ##### 请求头（Headers） <span id="hhr444a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="hhr450a0h5qr02" />

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
        #### 响应参数 <span id="hhr464a0h4qrs1" />

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

          <strong>轮询过程中的状态流转：</strong>

          - PENDING（排队中） → RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
          - 初次查询状态通常为 PENDING（排队中）或 RUNNING（处理中）。
          - 当状态变为 SUCCEEDED 时，响应中将包含生成的视频URL。
          - 若状态为 FAILED，请检查错误信息并重试。
          - 若状态为 CANCELED，表示任务已取消，如需继续请重新提交任务。
          - 若状态为 UNKNOWN，表示任务不存在或状态未知，可能在 task\_id 不存在或超过 24 小时有效期后出现。

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>video\_url</strong>`string`

          视频URL。仅在 task\_status 为 SUCCEEDED 时返回。

          链接有效期24小时，可通过此URL下载视频。视频格式为MP4（H.264 编码）。

          <strong>orig\_prompt</strong> `string`

          原始输入的prompt，对应请求参数`prompt`。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>duration</strong> `integer`

          生成视频的总视频时长，用于计费。

          <strong>input\_video\_duration</strong> `integer`

          输入视频的总时长，单位为秒。参考生视频中固定为0。

          <strong>output\_video\_duration</strong> `integer`

          输出视频的总时长，单位为秒。

          <strong>ratio</strong> `string`

          生成视频的宽高比。

          <strong>SR</strong> `integer`

          生成视频的分辨率档位。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为1。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            视频URL仅保留24小时，超时后会被自动清除，请及时保存生成的视频。

            ```json
            {
                "request_id": "35137489-2862-96cb-b6f2-xxxxxx",
                "output": {
                    "task_id": "1469cfc3-3004-4d9e-ab10-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-04-25 15:03:25.848",
                    "scheduled_time": "2026-04-25 15:03:25.884",
                    "end_time": "2026-04-25 15:04:05.882",
                    "orig_prompt": "[Image 1]中身着红色旗袍的女性，镜头先以侧面中景勾勒旗袍修身剪裁与S型曲线，随即切换至低角度仰拍，捕捉她轻抬玉手展开[Image 2]中的折扇的同时，[Image 3]中的流苏耳坠随头部转动轻盈摆动的细节，最后推近至面部特写，定格在她指尖轻点扇骨、眼波流转间的含蓄风情，多视角全方位展现东方韵味。",
                    "video_url": "https://dashscope-result.oss-cn-beijing.aliyuncs.com/xxxx.mp4"
                },
                "usage": {
                    "duration": 5,
                    "input_video_duration": 0,
                    "output_video_duration": 5,
                    "video_count": 1,
                    "SR": 720,
                    "ratio": "16:9"
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "e5d70b02-ebd3-98ce-9fe8-759d7d7b107d",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-a0c6a421c010",
                    "task_status": "FAILED",
                    "code": "InvalidParameter",
                    "message": "The resolution is not valid xxxxxx"
                }
            }
            ```
          </Tab>

          <Tab title="任务查询过期">
            task\_id查询有效期为 24 小时，超时后将无法查询，返回以下报错信息。

            ```json
            {
                "request_id": "a4de7c32-7057-9f82-8581-xxxxxx",
                "output": {
                    "task_id": "502a00b1-19d9-4839-a82f-xxxxxx",
                    "task_status": "UNKNOWN"
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
