> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相2.7-图生视频API参考

> 万相2.7-图生视频模型全新升级，支持 多模态输入 （文本/图像/音频/视频），可完成 首帧生视频、首尾帧生视频、视频续写 三大任务。

<meta property="og:title" content="万相2.7-图生视频API参考" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/wan-image-to-video-guide)

<Note>
  全新推出的<strong>图生视频 API</strong>（wan2.7模型）支持上述三大任务，<strong>推荐优先选用</strong>。

  原[图生视频-基于首帧](/zh/model-studio/legacy-image-to-video-api-reference)（wan2.6及早期模型）仅支持首帧生视频。
</Note>

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证模型、endpoint URL 和 API Key 均属于<strong>同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/video-generate-edit-model)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：获取该地域的[API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
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

## HTTP调用 <span id="a5a4ad982dpoz" />

<Tip>
  此接口为<strong>图生视频新版协议</strong>，仅支持<strong>wan2.7模型</strong>。
</Tip>

图生视频任务耗时较长（通常为1-5分钟），API采用异步调用的方式。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="9b685d1a9b6wv" />

<Tabs>
  <Tab title="北京">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
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
        #### 请求参数 <span id="f3da63afcam8r" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="ee4e0b58694zl" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。模型列表与价格详见[模型价格](/zh/model-studio/model-pricing#e715eca061ba4)。

        示例值：wan2.7-i2v-2026-04-25。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` （可选）

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，长度不超过5000个字符。每个汉字/字母占一个字符，超过部分会自动截断。

          示例值：一只小猫在草地上奔跑。

          提示词使用技巧详见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在视频画面中看到的内容，可以对视频画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体素材列表，用于指定视频生成所需的参考素材（图像、音频和视频）。

          数组的每个元素为一个媒体对象，包含 `type` 与 `url` 字段。

          <Accordion title="素材组合" defaultOpen>
            仅支持以下特定的素材组合，非法组合将报错。

            - <strong>首帧生视频</strong>：

              - 首帧：`first_frame`
              - 首帧+音频：`first_frame+driving_audio`
            - <strong>首尾帧生视频</strong>：

              - 首帧+尾帧：`first_frame+last_frame`
              - 首帧+尾帧+音频：`first_frame+last_frame+driving_audio`
            - <strong>视频续写</strong>：

              - 首段视频续写：`first_clip`
              - 首段视频+尾帧续写：`first_clip+last_frame`
          </Accordion>

          <Accordion title="属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。可选值为：

            - `first_frame`：首帧。
            - `last_frame`：尾帧。
            - `driving_audio`：驱动音频。
            - `first_clip`：首视频片段。

            使用限制：每种 `type` 在 `media` 数组中最多出现一次（例如：不可同时传入两个 `first_frame`）。

            <strong>url</strong> `string` <strong>（必选）</strong>

            媒体素材URL。素材包括图像、音频和视频。

            <Accordion title="传入图像（type=first_frame或last_frame）" defaultOpen>
              首帧或尾帧URL，或 Base64 编码数据。目前支持首帧生视频、首尾帧生视频。

              图像限制：

              - 格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
              - 分辨率：宽和高的范围为\[240, 8000]像素。
              - 宽高比：1:8～8:1。
              - 文件大小：不超过20MB。

              支持输入的格式：

              1. 公网URL:

                 - 支持 HTTP 或 HTTPS 协议。
                 - 示例值：<a href="https://xxx/xxx.png">[https://xxx/xxx.png](https://xxx/xxx.png)</a>。
              2. 临时URL：

                 - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                 - 示例值：oss\://dashscope-instant/xxx/xxx.png。
              3. Base64 编码图像后的字符串：

                 - 数据格式：`data:{MIME_type};base64,{base64_data}`。
                 - 示例值：data:image/png;base64,GDU7MtCZzEbTbmRZ......（示例已截断，仅做演示）。

                   <Accordion title="Base64编码数据格式">
                     格式： `data:{MIME_type};base64,{base64_data}` 。

                     - \{base64\_data}：图像文件经过 Base64 编码后的字符串。
                     - \{MIME\_type}：图像的媒体类型，需与文件格式对应。

                     <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><tbody><tr><td><p>图像格式</p></td><td><p>MIME Type</p></td></tr><tr><td><p>JPEG</p></td><td><p>image/jpeg</p></td></tr><tr><td><p>JPG</p></td><td><p>image/jpeg</p></td></tr><tr><td><p>PNG</p></td><td><p>image/png</p></td></tr><tr><td><p>BMP</p></td><td><p>image/bmp</p></td></tr><tr><td><p>WEBP</p></td><td><p>image/webp</p></td></tr></tbody></table>
                   </Accordion>

              <Accordion title="传入音频（type=driving_audio）" defaultOpen>
                音频文件的 URL。

                - 传入音频：模型将以该音频为驱动源生成视频（如口型同步、动作卡点等）。
                - 未传入音频：模型将根据视频画面内容，自动生成匹配的背景音乐或音效。

                音频限制：

                - 格式：wav、mp3。
                - 时长：2～30s。
                - 文件大小：不超过15MB。
                - 截断处理：若音频长度超过 `duration` 值（如5秒），自动截取前5秒，其余部分丢弃。若音频长度不足视频时长，超出音频长度部分为无声视频。例如，音频为3秒，视频时长为5秒，输出视频前3秒有声，后2秒无声。

                支持输入的格式：

                1. 公网URL：

                   - 支持 HTTP 和 HTTPS 协议。
                   - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                2. 临时URL：

                   - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                   - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。
              </Accordion>

              <Accordion title="传入视频（type=first_clip）" defaultOpen>
                视频文件的 URL。模型将基于该视频内容进行续写生成，续写时长的上限由 `duration` 参数控制。

                > 例如：当duration=15时，输入视频 3 秒，则模型续写生成 12 秒，最终输出视频总时长 15 秒， 按 15 秒计费。

                视频限制：

                - 格式：mp4、mov。
                - 时长：2～10s。
                - 分辨率：宽和高的范围为\[240, 4096]像素。
                - 宽高比：1:8～8:1。
                - 文件大小：不超过100MB。

                支持输入的格式：

                1. 公网URL：

                   - 支持 HTTP 和 HTTPS 协议。
                   - 示例值：<a href="https://xxx/xxx.mp4">[https://xxx/xxx.mp4](https://xxx/xxx.mp4)</a>。
                2. 临时URL：

                   - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                   - 示例值：oss\://dashscope-instant/xxx/xxx.mp4。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频处理参数，如设置视频分辨率、设置视频时长、开启prompt智能改写、添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` （可选）

          <Tip>
            resolution直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e715eca061ba4)。
          </Tip>

          指定生成的视频分辨率档位，用于控制视频的清晰度（总像素）。

          模型根据选择的分辨率档位，自动缩放至相近总像素。<strong>视频宽高比尽量与输入素材（首帧或首段视频）保持一致</strong>，详见[常见问题](/zh/model-studio/image-to-video-general-api-reference#646b718e448ww)。

          可选值：

          - 720P
          - 1080P（默认值）

          <strong>duration</strong> `integer` （可选）

          <Tip>
            duration直接影响费用，按秒计费，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e715eca061ba4)。
          </Tip>

          生成视频的时长，单位为秒。

          取值为\[2, 15]之间的整数。默认值为5。

          <strong>prompt\_extend</strong>`boolean` （可选）

          是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

          - `true`：默认值，开启智能改写。
          - `false`：不开启智能改写。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为“AI生成”。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。

          <strong>seed</strong>`integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`。

          未指定时，系统自动生成随机种子。若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="首帧生视频">
            基于首帧图像和音频生成视频。

            ```bash expandable

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-i2v-2026-04-25",
                "input": {
                    "prompt": "一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由rap构成，没有其他对话或杂音。",
                    "media": [
                        {
                            "type": "first_frame",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/wpimhv/rap.png"
                        },
                        {
                            "type": "driving_audio",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/ozwpvi/rap.mp3"

                        }
                    ]
                },
                "parameters": {
                    "resolution": "720P",
                    "duration": 10,
                    "prompt_extend": true,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="首尾帧生视频">
            传入首帧和尾帧生成视频。

            ```bash expandable

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-i2v-2026-04-25",
                "input": {
                    "prompt": "写实风格,一只小黑猫好奇地仰望天空,镜头从平视角度逐渐升高,最后以俯视角度捕捉到它好奇的眼神。",
                    "media": [
                        {
                            "type": "first_frame",
                            "url": "https://wanx.alicdn.com/material/20250318/first_frame.png"
                        },
                        {
                            "type": "last_frame",
                            "url": "https://wanx.alicdn.com/material/20250318/last_frame.png"
                        }
                    ]
                },
                "parameters": {
                    "resolution": "720P",
                    "duration": 10,
                    "prompt_extend": false,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="视频续写">
            基于首段视频片段，让模型生成后续内容。

            ```bash

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-i2v-2026-04-25",
                "input": {
                    "prompt": "一个女孩对镜自拍，自拍结束后背着书包出门",
                    "media": [
                        {
                            "type": "first_clip",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/hfugmr/wan-r2v-role1.mp4"
                        }
                    ]
                },
                "parameters": {
                    "resolution": "720P",
                    "duration": 10,
                    "prompt_extend": true,
                    "watermark": true
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
        #### 响应参数 <span id="f61855da6bvbh" />

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

### 步骤2：根据任务ID查询结果 <span id="be5d09eac3ayn" />

<Tabs>
  <Tab title="北京">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

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
        #### 请求参数 <span id="fd010a8092qiw" />

        ##### 请求头（Headers） <span id="50cb3a1aa6yzu" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="f96cc5e8cf62s" />

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
        #### 响应参数 <span id="08a68c21dbgfk" />

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

        输出信息统计，只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>input\_video\_duration</strong> `integer`

          输入的视频的时长，单位秒。

          <strong>output\_video\_duration</strong> `integer`

          输出视频的时长，单位秒。

          <strong>duration</strong> `integer`

          总的视频时长，用于计费。

          <strong>SR</strong> `integer`

          输出视频的分辨率档位。示例值：720。

          <strong>video\_count</strong> `integer`

          输出视频的数量。固定为1。
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
                "request_id": "2ca1c497-f9e0-449d-9a3f-xxxxxx",
                "output": {
                    "task_id": "af6efbc0-4bef-4194-8246-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-09-25 11:07:28.590",
                    "scheduled_time": "2025-09-25 11:07:35.349",
                    "end_time": "2025-09-25 11:17:11.650",
                    "orig_prompt": "一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由他的rap构成，没有其他对话或杂音。",
                    "video_url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.mp4?Expires=xxx"
                },
                "usage": {
                    "duration": 15,
                    "input_video_duration": 0,
                    "output_video_duration": 15,
                    "video_count": 1,
                    "SR": 720
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

## DashScope SDK调用 <span id="b9e4d8f3a2sdk" />

SDK 的参数命名与[HTTP接口](/zh/model-studio/image-to-video-general-api-reference)基本一致，参数结构根据语言特性进行封装。

由于图生视频任务耗时较长（通常为1-5分钟），SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### Python SDK调用 <span id="b5e0d4f9a8pysdk" />

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.16`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 `dashscope.base_http_api_url`:

<Tabs>
  <Tab title="北京">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'`
  </Tab>

  <Tab title="新加坡">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1'`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="i2v27pysyncrqh" />

    ```python expandable
    # -*- coding: utf-8 -*-
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    media = [
        {
            "type": "first_frame",
            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/wpimhv/rap.png"
        },
        {
            "type": "driving_audio",
            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/ozwpvi/rap.mp3"
        }
    ]

    def sample_sync_call():
        print('----sync call, please wait a moment----')
        rsp = VideoSynthesis.call(
            api_key=api_key,
            model="wan2.7-i2v-2026-04-25",
            media=media,
            resolution="720P",
            duration=10,
            watermark=True,
            prompt="一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由rap构成，没有其他对话或杂音。",
        )
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_sync_call()
    ```

    ##### 响应示例 <span id="i2v27pysyncrsph" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "status_code": 200,
        "request_id": "ac5faf37-ddfa-9720-a0c5-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "b97c6d86-ad73-4bb7-80ff-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "submit_time": "2026-04-13 10:45:47.597",
            "scheduled_time": "2026-04-13 10:45:56.342",
            "end_time": "2026-04-13 10:47:26.273",
            "orig_prompt": "一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由rap构成，没有其他对话或杂音。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "input_video_duration": 0,
            "output_video_duration": 10,
            "SR": 720
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="i2v27pyasyncrqh" />

    ```python expandable
    # -*- coding: utf-8 -*-
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    media = [
        {
            "type": "first_frame",
            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/wpimhv/rap.png"
        },
        {
            "type": "driving_audio",
            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/ozwpvi/rap.mp3"
        }
    ]

    def sample_async_call():
        # 提交异步任务，立即返回任务信息
        rsp = VideoSynthesis.async_call(
            api_key=api_key,
            model="wan2.7-i2v-2026-04-25",
            media=media,
            resolution="720P",
            duration=10,
            watermark=True,
            prompt="一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由rap构成，没有其他对话或杂音。",
        )
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print("task_id: %s" % rsp.output.task_id)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

        # 查询任务状态
        status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

        # 等待任务完成
        rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_async_call()
    ```

    ##### 响应示例 <span id="15a0d419b9q5r" />

    1、创建任务的响应示例

    ```json
    {
        "status_code": 200,
        "request_id": "6dc3bf6c-be18-9268-9c27-xxxxxx",
        "code": "",
        "message": "",
        "output": {
            "task_id": "686391d9-7ecf-4290-a8e9-xxxxxx",
            "task_status": "PENDING",
            "video_url": ""
        },
        "usage": null
    }
    ```

    2、查询任务结果的响应示例

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "status_code": 200,
        "request_id": "ac5faf37-ddfa-9720-a0c5-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "b97c6d86-ad73-4bb7-80ff-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "submit_time": "2026-04-13 10:45:47.597",
            "scheduled_time": "2026-04-13 10:45:56.342",
            "end_time": "2026-04-13 10:47:26.273",
            "orig_prompt": "一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由rap构成，没有其他对话或杂音。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "input_video_duration": 0,
            "output_video_duration": 10,
            "SR": 720
        }
    }
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="b2c3d4e5f6javasdk" />

<Tip>
  请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.14`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 `Constants.baseHttpApiUrl`:

<Tabs>
  <Tab title="北京">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"`
  </Tab>

  <Tab title="新加坡">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1"`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tabs>
  <Tab title="同步调用">
    同步调用会阻塞等待，直到视频生成完成并返回结果。

    ##### 请求示例 <span id="i2v27javasyncrqh" />

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

    import java.util.ArrayList;
    import java.util.List;

    public class Image2Video {

        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void syncCall() {
            VideoSynthesis videoSynthesis = new VideoSynthesis();
            final String prompt = "一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由他的rap构成，没有其他对话或杂音。";
            List<VideoSynthesisParam.Media> media = new ArrayList<VideoSynthesisParam.Media>(){{
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/wpimhv/rap.png")
                        .type("first_frame")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/ozwpvi/rap.mp3")
                        .type("driving_audio")
                        .build());
            }};
            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.7-i2v-2026-04-25")
                            .prompt(prompt)
                            .media(media)
                            .watermark(true)
                            .duration(10)
                            .resolution("720P")
                            .build();
            VideoSynthesisResult result = null;
            try {
                System.out.println("---sync call, please wait a moment----");
                result = videoSynthesis.call(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            } catch (InputRequiredException e) {
                throw new RuntimeException(e);
            }
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            syncCall();
        }
    }

    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="i2v27javaasyncrqh" />

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

    import java.util.ArrayList;
    import java.util.List;

    public class Image2Video {

        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void asyncCall() {
            VideoSynthesis videoSynthesis = new VideoSynthesis();
            final String prompt = "一幅都市奇幻艺术的场景。一个充满动感的涂鸦艺术角色。一个由喷漆所画成的少年，正从一面混凝土墙上活过来。他一边用极快的语速演唱一首英文rap，一边摆着一个经典的、充满活力的说唱歌手姿势。场景设定在夜晚一个充满都市感的铁路桥下。灯光来自一盏孤零零的街灯，营造出电影般的氛围，充满高能量和惊人的细节。视频的音频部分完全由他的rap构成，没有其他对话或杂音。";
            List<VideoSynthesisParam.Media> media = new ArrayList<VideoSynthesisParam.Media>(){{
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/wpimhv/rap.png")
                        .type("first_frame")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/ozwpvi/rap.mp3")
                        .type("driving_audio")
                        .build());
            }};
            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.7-i2v-2026-04-25")
                            .prompt(prompt)
                            .media(media)
                            .watermark(true)
                            .duration(10)
                            .resolution("720P")
                            .build();
            VideoSynthesisResult result = null;
            try {
                System.out.println("---async call, please wait a moment----");
                result = videoSynthesis.asyncCall(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            } catch (InputRequiredException e) {
                throw new RuntimeException(e);
            }
            System.out.println(JsonUtils.toJson(result));

            String taskId = result.getOutput().getTaskId();
            System.out.println("taskId=" + taskId);

            try {
                result = videoSynthesis.wait(taskId, apiKey);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));
            System.out.println(JsonUtils.toJson(result.getOutput()));
        }

        public static void main(String[] args) {
            asyncCall();
        }
    }

    ```
  </Tab>
</Tabs>

## 错误码 <span id="0d0a562d05nkh" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="2b6ac4aea9h5n" />

#### Q：如何生成特定宽高比（如3:4）的视频？ <span id="646b718e448ww" />

A： 输出视频的宽高比由 <strong>输入素材（首帧图像或首视频片段）</strong>决定，但<strong>无法保证严格等于输入比例</strong>（如精确 3:4），实际输出会存在微小偏差。

下面以“输入首帧图像”为例说明具体逻辑：

- <strong>为什么会有偏差？</strong>

  - 执行逻辑：以输入图像的比例为基准比例参考，结合 `resolution` 档位的目标总像素，且<strong>视频长宽必须为 16 的倍数</strong>（视频编码规范），系统会自动微调至最接近的合法分辨率。
  - 计算示例：输入首帧图像750×1000（宽高比 3:4 = 0.75），并设置 resolution = "720P"（目标总像素约 92 万），实际输出视频的分辨率为816×1104（宽高比 ≈ 0.739，总像素约90万）。
- <strong>实践建议</strong>：

  - 输入控制：尽量使用与目标比例一致的首帧或首视频片段作为输入。
  - 后期处理：如果您对比例有严格要求，建议在视频生成后，使用编辑工具进行简单的裁剪或黑边填充。

#### Q：如何获取视频存储的访问域名白名单？ <span id="c7421e6e32dj8" />

A： 模型生成的视频存储于阿里云OSS，API将返回一个临时的公网URL。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会根据业务情况进行动态变更，为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。
