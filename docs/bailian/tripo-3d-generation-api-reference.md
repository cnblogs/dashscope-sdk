> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Tripo-3D模型生成

> Tripo 3D模型生成支持 文生3D模型 、 单图生3D模型和多图生3D模型 。

<meta property="og:title" content="Tripo-3D模型生成" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/tripo-3d-generation-guide)

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 适用范围 <span id="tp013a0h2scop" />

1. <strong>开通服务</strong>：前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索“<strong>Tripo</strong>”，找到Tripo模型卡片，单击<strong>立即开通</strong>，在弹窗内确认开通及授权。
2. <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP调用 <span id="tp101a0h2http" />

由于3D模型生成任务耗时较长，API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="tp111a0h3stp1" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/3d-generation`

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
        #### 请求参数 <span id="tp123a0h4reqp" />

        ##### 请求头（Headers） <span id="tp142a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="tp154a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选值：

        - `Tripo/Tripo-H3.1`：高精度3D模型生成，输出产物最高200万面。对应 Tripo 官方 API 版本 `v3.1-20260211`。
        - `Tripo/Tripo-P1.0`：专业3D模型生成，输出产物最高2万面，速度更快。对应 Tripo 官方 API 版本 `P1-20260311`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入信息。`prompt`、`image`、`images` 三者互斥，仅能选择其中一种输入方式。同时传入多个将会报错。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` （条件必填）

          文本提示词，用于描述期望生成的3D模型。<strong>仅在文生3D模型时必填。</strong>

          支持多语言（中文、英文等），每个字符（含中文汉字、英文字母等）均计为1个字符，最大长度1024个字符。

          示例值：一只可爱的猫。

          <strong>image</strong> `string` （条件必填）

          单张图像的URL。<strong>仅在单图生3D模型时必填。</strong>

          图像限制：

          - 格式：JPEG、PNG。
          - 分辨率：宽和高的范围为\[20, 6000]像素，建议边长大于256像素。
          - 文件大小：不超过20MB。

          公网URL：

          - 支持 HTTP 和 HTTPS 协议。
          - 示例值：<a href="https://xxx/xxx.jpg">[https://xxx/xxx.jpg](https://xxx/xxx.jpg)</a>。

          <strong>images</strong> `array[object]` （条件必填）

          多张图像的对象列表。<strong>仅在多图生3D模型时必填。</strong>

          数组长度固定为4，对应图片顺序为<strong>前、左、后、右</strong>。如果某个视角不需要传入图片，传入空对象`{}`即可。实际传入的有效图片数量为2\~4张。每张图像的限制与`image`参数相同。多张图像的分辨率和宽高比不要求一致。

          每个图像对象包含以下字段：

          - `type`：图像格式，可选值为`jpeg`或`png`。
          - `file_token`：图像的公网URL。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        3D模型生成参数。

        <Accordion title="属性" defaultOpen>
          <strong>texture\_quality</strong> `string` （可选）

          贴图质量。贴图是覆盖在3D模型表面的纹理图像，决定了模型的外观细节和视觉效果。

          可选值：

          - `standard`：默认值，标清贴图。
          - `detailed`：高清贴图。

          <strong>geometry\_quality</strong> `string` （可选）

          支持模型：`Tripo/Tripo-H3.1`。

          几何精度。

          - `standard`：默认值，标准版。生成的3D模型最高150万面。
          - `ultra`：超清版。生成的3D模型最高200万面。

          <strong>pbr</strong> `boolean` （可选）

          是否生成PBR材质模型。默认值为`true`。

          当`pbr`设为`true`时，会强制启用贴图（即`texture`也会被设为`true`），返回结果中包含`pbr_model_url`。

          <strong>texture</strong> `boolean` （可选）

          是否生成贴图。默认值为`true`。

          如需生成无贴图模型，需同时设置`texture`和`pbr`为`false`，返回结果中包含`base_model_url`。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文生3D模型（有贴图）">
            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/3d-generation' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "Tripo/Tripo-P1.0",
                "input": {
                    "prompt": "一只可爱的猫"
                },
                "parameters": {
                    "texture_quality": "standard"
                }
            }'
            ```
          </Tab>

          <Tab title="单图生3D模型（有贴图）">
            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/3d-generation' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "Tripo/Tripo-P1.0",
                "input": {
                    "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/cfbxhg/tripo-single.jpg"
                },
                "parameters": {
                    "texture_quality": "standard"
                }
            }'
            ```
          </Tab>

          <Tab title="多图生3D模型（有贴图）">
            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/3d-generation' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "Tripo/Tripo-P1.0",
                "input": {
                    "images": [
                        { "type": "png", "file_token": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/liafix/tripo-images-1.png" },
                        { "type": "png", "file_token": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/slgluy/tripo-images-2.png" },
                        { "type": "png", "file_token": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/zjqhyn/tripo-images-3.png" },
                        { "type": "png", "file_token": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/mqfzww/tripo-images-4.png" }
                    ]
                },
                "parameters": {
                    "texture_quality": "standard"
                }
            }'
            ```
          </Tab>

          <Tab title="多图生3D模型（传入2张图）">
            > 图片顺序为前、左、后、右，不需要的视角传入空对象`{}`即可。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/3d-generation' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "Tripo/Tripo-P1.0",
                "input": {
                    "images": [
                        { "type": "png", "file_token": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/liafix/tripo-images-1.png" },
                        {},
                        { "type": "png", "file_token": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260424/zjqhyn/tripo-images-3.png" },
                        {}
                    ]
                },
                "parameters": {
                    "texture_quality": "standard"
                }
            }'
            ```
          </Tab>

          <Tab title="无贴图生成">
            > 需同时将`texture`和`pbr`设为`false`。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/3d-generation' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "Tripo/Tripo-P1.0",
                "input": {
                    "prompt": "一只可爱的猫"
                },
                "parameters": {
                    "texture": false,
                    "pbr": false
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
        #### 响应参数 <span id="tp285a0h4rsp1" />

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

### 步骤2：根据任务ID查询结果 <span id="tp401a0h3stp2" />

<strong>北京地域</strong>：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<Note>
  - <strong>轮询建议</strong>：3D模型生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 15 秒）来获取结果。
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
        #### 请求参数 <span id="tp424a0h4qr01" />

        ##### 请求头（Headers） <span id="tp434a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="tp440a0h5qr02" />

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
        #### 响应参数 <span id="tp448a0h4qrs1" />

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

          <strong>results</strong>`array`

          生成结果列表。仅在 task\_status 为 SUCCEEDED 时返回。

          <Accordion title="属性" defaultOpen>
            <strong>pbr\_model\_url</strong> `string`

            PBR材质模型（GLB格式）的下载URL。当请求参数中`pbr`为`true`（默认）时返回。链接有效期2小时，请及时下载。

            <strong>base\_model\_url</strong> `string`

            无贴图基础模型（GLB格式）的下载URL。当请求参数中`texture`和`pbr`均为`false`时返回。链接有效期2小时，请及时下载。

            <strong>rendered\_image\_url</strong> `string`

            3D模型预览渲染图的URL（1张）。链接有效期2小时，请及时下载。
          </Accordion>

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
          <strong>3d\_task\_type</strong> `string`

          3D模型生成任务类型。枚举值：

          - `text-to-3d`：文生3D模型。
          - `image-to-3d`：单图生3D模型。
          - `multi-image-to-3d`：多图生3D模型。

          <strong>count</strong> `integer`

          生成的3D模型数量。

          <strong>texture\_quality</strong> `string`

          贴图质量。

          <strong>geometry\_quality</strong> `string`

          几何精度。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "c11018a8-3f83-9591-a636-xxxxxx",
                "output": {
                    "task_id": "051c7b40-b2c5-4341-aee4-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-04-26 14:13:14.373",
                    "scheduled_time": "2026-04-26 14:13:14.419",
                    "end_time": "2026-04-26 14:14:13.679",
                    "results": [
                        {
                            "pbr_model_url": "https://openapi.cdn.tripo3d.com/xxxx.glb?auth_key=xxxx",
                            "rendered_image_url": "https://openapi.cdn.tripo3d.com/xxxx.webp?auth_key=xxxx"
                        }
                    ]
                },
                "usage": {
                    "3d_task_type": "text-to-3d",
                    "count": 1,
                    "texture_quality": "standard"
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "e5d70b02-ebd3-98ce-9fe8-xxxxxx",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-xxxxxx",
                    "task_status": "FAILED",
                    "code": "InvalidParameter",
                    "message": "The parameter is invalid xxxxxx"
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

## 错误码 <span id="0d0a562d05nkh" />

如果模型调用失败并返回报错信息，请参见[错误信息](/zh/model-studio/error-code)进行解决。
