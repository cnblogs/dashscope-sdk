> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-首尾帧生视频API参考（2.2）

> 万相2.2-首尾帧生视频模型基于 首帧图像 、 尾帧图像和文本提示词 ，生成一段平滑过渡的视频。

<meta property="og:title" content="万相-首尾帧生视频API参考（2.2）" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/image-to-video-first-and-last-frames-guide)

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证模型、Endpoint URL 和 API Key 均属于<strong>同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/image-to-video-first-and-last-frames-guide#06f39eafa2dwt)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- <strong>安装 SDK</strong>：如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。

<Note>
  本文的示例代码适用于<strong>北京地域</strong>。
</Note>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP调用 <span id="42703589880ts" />

由于图生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="81185ac18blmj" />

<Tabs>
  <Tab title="北京">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

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
        #### 请求参数 <span id="afcd41d2b57zf" />

        ##### 请求头（Headers） <span id="79473e45c592d" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。示例值：wan2.2-kf2v-flash。

        详情参见百炼控制台。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` （可选）

          文本提示词。支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

          如果首尾帧的主体和场景变化较大，建议描写变化过程，例如运镜过程（镜头向左移动）或者主体运动过程（人向前奔跑）。

          示例值：一只黑色小猫好奇地看向天空，<strong>镜头从平视逐渐上升</strong>，最后<strong>俯拍</strong>它的好奇的眼神。

          提示词的使用技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在视频画面中看到的内容，可以对视频画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>first\_frame\_url</strong> `string` <strong>（必选）</strong>

          首帧图像的URL或 Base64 编码数据。<strong>输出视频的宽高比将以此图像为基准。</strong>

          图像限制：

          - 图像格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
          - 图像分辨率：图像的宽度和高度范围为\[240,8000]，单位为像素。
          - 文件大小：不超过10MB。

          支持输入的格式：

          1. 公网URL:

             - 支持 HTTP 或 HTTPS 协议。
             - 示例值：<a href="https://wanx.alicdn.com/xxx/first_frame.png">[https://wanx.alicdn.com/xxx/first\_frame.png](https://wanx.alicdn.com/xxx/first_frame.png)</a>。
          2. 临时URL：

             - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
             - 示例值：oss\://dashscope-instant/xxx/xxx.png。
          3. Base64 编码图像后的字符串：

             - 数据格式：`data:{MIME_type};base64,{base64_data}`。
             - 示例值：data:image/png;base64,GDU7MtCZzEbTbmRZ......。（编码字符串过长，仅展示片段）
             - 详情请参见[如何输入图像](/zh/model-studio/image-to-video-first-and-last-frames-guide#69308edfbebnx)。

          <strong>last\_frame\_url</strong> `string` （可选）

          尾帧图像的URL或 Base64 编码数据。

          图像限制：

          - 图像格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
          - 图像分辨率：图像的宽度和高度范围为\[240,8000]，单位为像素。尾帧图像分辨率可与首帧不同，无需强制对齐。
          - 文件大小：不超过10MB。

          支持输入的格式：

          1. 公网URL:

             - 支持 HTTP 或 HTTPS 协议。
             - 示例值：<a href="https://wanx.alicdn.com/xxxx/last_frame.png">[https://wanx.alicdn.com/xxxx/last\_frame.png](https://wanx.alicdn.com/xxxx/last_frame.png)</a>。
          2. 临时URL：

             - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
             - 示例值：oss\://dashscope-instant/xxx/xxx.png。
          3. Base64 编码图像后的字符串：

             - 数据格式：`data:{MIME_type};base64,{base64_data}`。
             - 示例值：data:image/png;base64,GDU7MtCZ......。（编码字符串过长，仅展示片段）
             - 详情请参见[如何输入图像](/zh/model-studio/image-to-video-first-and-last-frames-guide#69308edfbebnx)。

          <strong>template</strong> `string` （可选）

          视频特效模板的名称。使用此参数时，仅需传入 `first_frame_url`。

          不同模型支持不同的特效模板。调用前请查阅[万相-图生视频-视频特效列表](/zh/model-studio/wanx-video-effects)，以免调用失败。

          示例值：hufu-1，表示使用“唐韵翩然”特效。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` （可选）

          <Tip>
            <strong>resolution</strong>直接影响费用，同一模型：1080P > 720P > 480P，调用前请确认百炼控制台。
          </Tip>

          生成的视频分辨率档位。仅用于调整视频的清晰度（总像素），不改变视频的宽高比，<strong>视频宽高比将与首帧图像 first\_frame\_url 的宽高比保持一致</strong>。

          此参数的默认值和可用枚举值依赖于 model 参数，规则如下：

          - wan2.2-kf2v-flash：可选值：480P、720P、1080P。默认值为`720P`。
          - wanx2.1-kf2v-plus：可选值：720P。默认值为`720P`。

          示例值：720P。

          <strong>duration</strong> `integer` （可选）

          <Tip>
            duration直接影响费用，按秒计费，调用前请确认百炼控制台。
          </Tip>

          视频生成时长，单位为秒。当前参数值固定为5，且不支持修改。模型将始终生成5秒时长的视频。

          <strong>prompt\_extend</strong>`bool` （可选）

          是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

          - true：默认值，开启智能改写。
          - false：不开启智能改写。

          示例值：true。

          <strong>watermark</strong> `bool` （可选）

          是否添加水印标识，水印位于图片右下角，文案为“AI生成”。

          - false：默认值，不添加水印。
          - true：添加水印。

          示例值：false。

          <strong>seed</strong>`integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`。

          未指定时，系统自动生成随机种子。若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="首尾帧生视频">
            根据首帧、尾帧和prompt生成视频。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.2-kf2v-flash",
                "input": {
                    "first_frame_url": "https://wanx.alicdn.com/material/20250318/first_frame.png",
                    "last_frame_url": "https://wanx.alicdn.com/material/20250318/last_frame.png",
                    "prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。"
                },
                "parameters": {
                    "resolution": "480P",
                    "prompt_extend": true
                }
            }'
            ```
          </Tab>

          <Tab title="使用Base64">
            首帧first\_frame\_url和尾帧last\_frame\_url参数支持传入图像的 Base64 编码字符串。先下载[first\_frame\_base64](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250722/vzjwiv/first_frame_base64.txt)和[last\_frame\_base64](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250722/qbtevh/last_frame_base64.txt)文件，并将完整内容粘贴至对应参数中。

            格式参见[如何输入图像](/zh/model-studio/image-to-video-first-and-last-frames-guide#69308edfbebnx)。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wanx2.1-kf2v-plus",
                "input": {
                    "first_frame_url": "data:image/png;base64,GDU7MtCZzEbTbmRZ......",
                    "last_frame_url": "data:image/png;base64,VBORw0KGgoAAAANSUh......",
                    "prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。"
                },
                "parameters": {
                    "resolution": "720P",
                    "prompt_extend": true
                }
            }'
            ```
          </Tab>

          <Tab title="使用视频特效">
            必须传入`first_frame_url`和`template`，无需传入prompt和last\_frame\_url。

            不同模型支持不同的特效模板。调用前请查阅[万相-图生视频-视频特效列表](/zh/model-studio/wanx-video-effects)，以免调用失败。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wanx2.1-kf2v-plus",
                "input": {
                    "first_frame_url": "https://ty-yuanfang.oss-cn-hangzhou.aliyuncs.com/lizhengjia.lzj/tmp/11.png",
                    "template": "hanfu-1"
                },
                "parameters": {
                    "resolution": "720P",
                    "prompt_extend": true
                }
            }'
            ```
          </Tab>

          <Tab title="使用反向提示词">
            通过 negative\_prompt 指定生成的视频避免出现“人物”元素。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wanx2.1-kf2v-plus",
                "input": {
                    "first_frame_url": "https://wanx.alicdn.com/material/20250318/first_frame.png",
                    "last_frame_url": "https://wanx.alicdn.com/material/20250318/last_frame.png",
                    "prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
                    "negative_prompt": "人物"
                },
                "parameters": {
                    "resolution": "720P",
                    "prompt_extend": true
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
        #### 响应参数 <span id="cd3063363egdc" />

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

### 步骤2：根据任务ID查询结果 <span id="420e6e77a17om" />

<Tabs>
  <Tab title="北京">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Note>
  - <strong>轮询建议</strong>：视频生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 15 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
  - <strong>结果链接</strong>：任务成功后返回视频链接，有效期为 <strong>24 小时</strong>。建议在获取链接后立即下载并转存至永久存储（如[阿里云 OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)）。
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
        #### 请求参数 <span id="a9e0408c70ylw" />

        ##### 请求头（Headers） <span id="2d2fb41da6idy" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="031d8a0fafqhp" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            请将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id。

            > 若使用新加坡地域的模型，需将base\_url替换为https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx，其中\{WorkspaceId}需替换为真实的业务空间ID。

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
        #### 响应参数 <span id="810682d979i8q" />

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

          <strong>actual\_prompt</strong> `string`

          开启 prompt 智能改写后，返回实际使用的优化后 prompt。若未开启该功能，则不返回此字段。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>video\_duration</strong> `integer`

          生成视频的时长，单位秒。枚举值为5。计费公式：费用 = 视频秒数 × 单价。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为1。

          <strong>video\_ratio</strong> `string`

          当前仅当2.1模型返回该值。生成视频的比例，固定为standard。

          <strong>SR</strong> `integer`

          当前仅当2.2模型返回该值。生成视频的分辨率档位，枚举值为480、720、1080。
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
                "request_id": "ec016349-6b14-9ad6-8009-xxxxxx",
                "output": {
                    "task_id": "3f21a745-9f4b-4588-b643-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-04-18 10:36:58.394",
                    "scheduled_time": "2025-04-18 10:37:13.802",
                    "end_time": "2025-04-18 10:45:23.004",
                    "video_url": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/xxx.mp4?xxxxx",
                    "orig_prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
                    "actual_prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。小猫的黄色眼睛明亮有神，毛发光滑，胡须清晰可见。背景是简单的浅色墙面，突显小猫的黑色身影。近景特写，强调小猫的表情变化和眼神细节。"
                },
                "usage": {
                    "video_duration": 5,
                    "video_count": 1,
                    "SR": 480
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
                    "message": "The size is not match xxxxxx"
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

## DashScope SDK调用 <span id="db17b3098c2iv" />

SDK 的参数命名与[HTTP接口](/zh/model-studio/text-to-video-api-reference)基本一致，参数结构根据语言特性进行封装。

由于图生视频任务耗时较长（通常为1-5分钟），SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### Python SDK调用 <span id="a3ad9a3b6d9if" />

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.23.8`，再运行以下代码。

  若版本过低，可能会触发 “url error, please check url!” 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 `base_http_api_url`:

<Tabs>
  <Tab title="北京">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1'`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

#### 示例代码 <span id="f2b1639e488og" />

<Tabs>
  <Tab title="同步调用">
    本示例展示三种图像输入方式：公网URL、Base64编码、本地文件路径。

    ##### 请求示例 <span id="3cd4969b00193" />

    ```python expandable
    import base64
    import os
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import mimetypes
    import dashscope

    # 以下为北京地域URL，各地域的URL不同，获取URL：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # --- 辅助函数：用于 Base64 编码 ---
    # 格式为 data:{MIME_type};base64,{base64_data}
    def encode_file(file_path):
        mime_type, _ = mimetypes.guess_type(file_path)
        if not mime_type or not mime_type.startswith("image/"):
            raise ValueError("不支持或无法识别的图像格式")
        with open(file_path, "rb") as image_file:
            encoded_string = base64.b64encode(image_file.read()).decode('utf-8')
        return f"data:{mime_type};base64,{encoded_string}"

    """
    图像输入方式说明：
    以下提供了三种图片输入方式，三选一即可

    1. 使用公网URL - 适合已有公开可访问的图片
    2. 使用本地文件 - 适合本地开发测试
    3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
    """

    # 【方式一】使用公网图片 URL
    first_frame_url = "https://wanx.alicdn.com/material/20250318/first_frame.png"
    last_frame_url = "https://wanx.alicdn.com/material/20250318/last_frame.png"

    # 【方式二】使用本地文件（支持绝对路径和相对路径）
    # 格式要求：file:// + 文件路径
    # 示例（绝对路径）：
    # first_frame_url = "file://" + "/path/to/your/first_frame.png"  # Linux/macOS
    # last_frame_url = "file://" + "C:/path/to/your/last_frame.png"  # Windows
    # 示例（相对路径）：
    # first_frame_url = "file://" + "./first_frame.png"              # 以实际路径为准
    # last_frame_url = "file://" + "./last_frame.png"                # 以实际路径为准

    # 【方式三】使用Base64编码的图片
    # first_frame_url = encode_file("./first_frame.png")            # 以实际路径为准
    # last_frame_url = encode_file("./last_frame.png")              # 以实际路径为准

    def sample_sync_call_kf2v():
        print('please wait...')
        rsp = VideoSynthesis.call(api_key=api_key,
                                  model="wan2.2-kf2v-flash",
                                  prompt="写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
                                  first_frame_url=first_frame_url,
                                  last_frame_url=last_frame_url,
                                  resolution="720P",
                                  prompt_extend=True)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_sync_call_kf2v()
    ```

    ##### 响应示例 <span id="0c0533477emwb" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "status_code": 200,
        "request_id": "efa545b3-f95c-9e3a-a3b6-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "721164c6-8619-4a35-a6d9-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.mp4?xxxxx",
            "submit_time": "2025-02-12 11:03:30.701",
            "scheduled_time": "2025-02-12 11:06:05.378",
            "end_time": "2025-02-12 11:12:18.853",
            "orig_prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
            "actual_prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。小猫毛发乌黑光亮，眼睛大而明亮，瞳孔呈金黄色。它抬头仰望，耳朵竖立，显得格外专注。镜头上移后，小猫转头直视镜头，眼神中充满好奇与警觉。背景简洁，突出小猫的细节特征。近景特写，自然光线柔和。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 5,
            "video_ratio": "standard"
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    本示例展示异步调用方式。该方式会立即返回任务ID，需要自行轮询或等待任务完成。

    ##### 请求示例 <span id="b994ca9918t7e" />

    ```python
    import os
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope

    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。

    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="589b80853e6rn" />

<Tip>
  请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.20.9`，再运行以下代码。

  若版本过低，可能会触发 “url error, please check url!” 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

#### 示例代码 <span id="8287893d0cod7" />

<Tabs>
  <Tab title="同步调用">
    本示例展示同步调用方式，并支持三种图像输入方式：公网URL、Base64编码、本地文件路径。

    ##### 请求示例 <span id="3e3609efb9rhl" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesis;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisParam;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.InputRequiredException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.io.IOException;
    import java.nio.file.Files;
    import java.nio.file.Path;
    import java.nio.file.Paths;
    import java.util.Base64;
    import java.util.HashMap;
    import java.util.Map;

    /**
     * 环境要求
     *      dashscope java SDK >= 2.20.9
     * 更新maven依赖:
     *      https://mvnrepository.com/artifact/com.alibaba/dashscope-sdk-java
     */
    public class Kf2vSync {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。

    ```
  </Tab>

  <Tab title="异步调用">
    本示例展示异步调用方式。该方式会立即返回任务ID，需要自行轮询或等待任务完成。

    ##### 请求示例 <span id="2fc30f223b9py" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesis;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisListResult;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisParam;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.InputRequiredException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.task.AsyncTaskListParam;
    import com.alibaba.dashscope.utils.JsonUtils;
    import com.alibaba.dashscope.utils.Constants;
    import java.util.HashMap;
    import java.util.Map;

    /**
     * 环境要求
     *      dashscope java SDK >= 2.20.9
     * 更新maven依赖:
     *      https://mvnrepository.com/artifact/com.alibaba/dashscope-sdk-java
     */

    public class Kf2vAsync {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。

    ```
  </Tab>
</Tabs>

## 使用限制 <span id="445b4bb8acqle" />

- <strong>数据时效</strong>：任务`task_id`和 视频`video_url`均只保留 24 小时，过期后将无法查询或下载。
- <strong>音频支持</strong>：当前仅支持生成无声视频，不支持音频输出。如有需要，可通过[语音合成](/zh/model-studio/speech-recognition-api-reference)生成音频。
- <strong>内容审核</strong>：输入prompt 和图像、输出视频均会经过内容安全审核，含违规内容将返回 “IPInfringementSuspect”或“DataInspectionFailed”错误，详情请参见[错误码](/zh/model-studio/error-code)。

## 错误码 <span id="0d0a562d05nkh" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="2b6ac4aea9h5n" />

#### Q：如何生成特定宽高比（如3:4）的视频？ <span id="da90d99af1c8v" />

<strong>A：</strong> 输出视频的宽高比由<strong>输入首帧图像（first\_frame\_url）</strong>决定，但<strong>无法保证精确比例</strong>（如严格3:4），会存在一定偏差。

- <strong>为什么会有偏差？</strong>

  模型会以输入图像的比例为基准，结合设置的分辨率档位（resolution）总像素，自动计算出最接近的合法分辨率。由于要求视频的长和宽必须是 16 的倍数，模型会对最终分辨率做微调，因此无法保证输出比例严格等于 3:4，但会非常接近。

  - 例如：输入图像750×1000（宽高比 3:4 = 0.75），并设置 resolution = "720P"（目标总像素约 92 万），实际输出816×1104（宽高比 ≈ 0.739，总像素约90万）。
- <strong>实践建议</strong>：

  - <strong>输入控制</strong>：尽量使用与目标比例一致的图片作为首帧输入。
  - <strong>后期处理</strong>：如果您对比例有严格要求，建议在视频生成后，使用编辑工具进行简单的裁剪或黑边填充。

### Q：如何获取视频存储的访问域名白名单？ <span id="a2d8df6eeabd3" />

A： 模型生成的视频存储于阿里云OSS，API将返回一个临时的公网URL。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会根据业务情况进行动态变更，为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。
