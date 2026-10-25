> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-参考生视频API参考（2.6）

> 万相2.6-参考生视频模型支持 多模态输入 ，可将人或物体作为主角，生成单角色表演或多角色互动视频。

<meta property="og:title" content="万相-参考生视频API参考（2.6）" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/video-to-video-guide)

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证模型、Endpoint URL 和 API Key 均属于<strong>同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/video-to-video-guide#06f39eafa2dwt)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

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

<Tip>
  此接口为<strong>旧版协议</strong>，只支持<strong>wan2.6模型</strong>。
</Tip>

由于视频生成任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="81185ac18blmj" />

<Tabs>
  <Tab title="北京">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="弗吉尼亚">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`
  </Tab>

  <Tab title="法兰克福">
    `POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

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

        模型名称。模型列表与价格详见[模型价格](/zh/model-studio/model-pricing#5c3d28ad8a4x8)。

        示例值：wan2.6-r2v-flash。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字、字母、标点占一个字符，超过部分会自动截断。

          - wan2.6-r2v-flash：长度不超过1500个字符。
          - wan2.6-r2v：长度不超过1500个字符。

          角色引用说明：通过“<strong>character1、character2</strong>”这类标识引用参考角色，每个参考（视频或图像）仅包含单一角色。模型仅通过此方式识别参考中的角色。

          示例值：character1在沙发上开心地看电影。

          提示词的使用技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在视频画面中出现的内容，可以对视频画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>reference\_urls</strong> `array[string]` <strong>（必选）</strong>

          <Tip>
            reference\_urls直接影响费用，计费规则请参见[计费与限流](/zh/model-studio/video-to-video-guide#6f5774ce5fqie)。
          </Tip>

          上传的参考文件 URL 数组，支持传入视频和图像。用于提取角色形象与音色（如有），以生成符合参考特征的视频。

          - 每个 URL 可指向 <strong>一张图像</strong> 或 <strong>一段视频</strong>：

            - 图像数量：0～5。
            - 视频数量：0～3。
            - 总数限制：图像 + 视频 ≤ 5。
          - 传入多个参考文件时，按照数组顺序定义角色的顺序。即第 1 个 URL 对应 character1，第 2 个对应 character2，以此类推。
          - 每个参考文件仅包含一个主体角色。例如 character1 为小女孩，character2 为闹钟。

          支持输入的格式：

          1. 公网URL:

             - 支持 HTTP 或 HTTPS 协议。
             - 示例值：<a href="https://cdn.translate.alibaba.com/xxx.png">[https://cdn.translate.alibaba.com/xxx.png](https://cdn.translate.alibaba.com/xxx.png)</a>。
          2. 临时URL：

             - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
             - 示例值：oss\://dashscope-instant/xxx/xxx.png。

          参考视频要求：

          - 格式：MP4、MOV。
          - 时长：1s～30s。
          - 视频大小：不超过100MB。

          参考图像要求：

          - 格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
          - 分辨率：宽高均需在\[240,8000]像素之间。
          - 图像大小：不超过20MB。

          示例值：\["<a href="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.mp4">[https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.mp4](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.mp4)</a>", "<a href="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.jpg">[https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.jpg](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.jpg)</a>"]。

          <Accordion title="已废弃字段">
            <strong>reference\_video\_urls</strong> `array[string]`

            <Tip>
              推荐使用`reference_urls`替代`reference_video_urls`。
            </Tip>

            上传的参考视频文件 URL 数组。用于提取角色形象与音色（如有），以生成符合参考特征的视频。

            - 最多支持 <strong>3 个视频</strong>。
            - 传入多个视频时，按照数组顺序定义视频角色的顺序。即第 1 个 URL 对应 character1，第 2 个对应 character2，以此类推。
            - 每个参考视频仅包含一个角色（如 character1 为小女孩，character2 为闹钟）。
            - URL支持 HTTP 或 HTTPS 协议。本地文件可通过[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。

            单个视频要求：

            - 格式：MP4、MOV。
            - 时长：2～30s。
            - 文件大小：视频不超过100MB。

            示例值：\["<a href="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.mp4">[https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.mp4](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/xxx.mp4)</a>"]。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。如设置视频分辨率、开启prompt智能改写、添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>size</strong> `string` （可选）

          <Tip>
            - size直接影响费用，费用 = 单价（基于分辨率）× 时长（秒）。同一模型：1080P > 720P ，请在调用前确认[模型价格](/zh/model-studio/model-pricing#5c3d28ad8a4x8)。
            - size必须设置为具体数值（如 `1280*720`），而不是 1:1或720P。
          </Tip>

          指定生成的视频分辨率，格式为`宽*高`。该参数的默认值和可用枚举值依赖于 model 参数，规则如下：

          - wan2.6-r2v-flash：默认值为 `1920*1080`（1080P）。可选分辨率：720P、1080P对应的所有分辨率。
          - wan2.6-r2v：默认值为 `1920*1080`（1080P）。可选分辨率：720P、1080P对应的所有分辨率。

          720P档位：可选的视频分辨率及其对应的视频宽高比为：

          - `1280*720`：16:9。
          - `720*1280`：9:16。
          - `960*960`：1:1。
          - `1088*832`：4:3。
          - `832*1088`：3:4。

          1080P档位：可选的视频分辨率及其对应的视频宽高比为：

          - `1920*1080`： 16:9。
          - `1080*1920`： 9:16。
          - `1440*1440`： 1:1。
          - `1632*1248`： 4:3。
          - `1248*1632`： 3:4。

          <strong>duration</strong> `integer` （可选）

          <Tip>
            duration直接影响费用。费用 = 单价（基于分辨率）× 时长（秒），请在调用前确认[模型价格](/zh/model-studio/model-pricing#5c3d28ad8a4x8)。
          </Tip>

          生成视频的时长，单位为秒。

          - wan2.6-r2v-flash：取值为\[2, 10]之间的整数。默认值为5。
          - wan2.6-r2v：取值为\[2, 10]之间的整数。默认值为5。

          示例值：5。

          <strong>shot\_type</strong> `string` （可选）

          指定生成视频的镜头类型，即视频是由一个连续镜头还是多个切换镜头组成。

          参数优先级：`shot_type > prompt`。例如，若 shot\_type设置为"single"，即使 prompt 中包含“生成多镜头视频”，模型仍会输出单镜头视频。

          可选值：

          - single：默认值，输出单镜头视频
          - multi：输出多镜头视频。

          示例值：single。

          <Note>
            当希望严格控制视频的叙事结构（如产品展示用单镜头、故事短片用多镜头），可通过此参数指定。
          </Note>

          <strong>audio</strong> `boolean` （可选）

          <Tip>
            audio直接影响费用，有声视频与无声视频价格不同，请在调用前确认[模型价格](/zh/model-studio/model-pricing#5c3d28ad8a4x8)。
          </Tip>

          <strong>支持模型：wan2.6-r2v-flash。</strong>

          是否生成有声视频。

          可选值：

          - true：默认值，输出有声视频。
          - false：输出无声视频。

          示例值：true。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为“AI生成”。

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
          <Tab title="多角色互动（参考图像和视频）">
            通过`reference_urls`传入图像和视频URL。同时设置`shot_type`为`multi`，生成多镜头视频。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.6-r2v-flash",
                "input": {
                    "prompt": "Character2 坐在靠窗的椅子上，手持 character3，在 character4 旁演奏一首舒缓的美国乡村民谣。Character1 对Character2开口说道：“听起来不错”",
                    "reference_urls": [
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/hfugmr/wan-r2v-role1.mp4",
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png",
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                    ]
                },
                "parameters": {
                    "size": "1280*720",
                    "duration": 10,
                    "audio": true,
                    "shot_type": "multi",
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="多角色互动（参考视频）">
            通过`reference_urls`传入多个视频URL。同时设置`shot_type`为`multi`，生成多镜头视频。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.6-r2v",
                "input": {
                    "prompt": "character1对character2说: “I’ll rely on you tomorrow morning!” character2 回答: “You can count on me!”",
                    "reference_urls": [
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251217/dlrrly/%E5%B0%8F%E5%A5%B3%E5%AD%A91%E8%8B%B1%E6%96%872.mp4",
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251217/fkxknn/%E9%93%83%E9%93%83.mp4"
                    ]
                },
                "parameters": {
                    "size": "1280*720",
                    "duration": 10,
                    "shot_type": "multi"
                }
            }'
            ```
          </Tab>

          <Tab title="单角色扮演">
            通过`reference_urls`传入单个视频URL。同时设置`shot_type`为`multi`，生成多镜头视频。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.6-r2v",
                "input": {
                    "prompt": "character1一边喝奶茶，一边随着音乐即兴跳舞。",
                    "reference_urls":["https://cdn.wanx.aliyuncs.com/static/demo-wan26/vace.mp4"]
                },
                "parameters": {
                    "size": "1280*720",
                    "duration": 5,
                    "shot_type":"multi"
                }
            }'
            ```
          </Tab>

          <Tab title="生成无声视频">
            仅支持`wan2.6-r2v-flash`生成无声视频。

            当生成无声视频时，<strong>必须显式设置</strong> `parameters.audio = false`。

            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.6-r2v-flash",
                "input": {
                    "prompt": "character1一边喝奶茶，一边随着音乐即兴跳舞。",
                    "reference_urls":["https://cdn.wanx.aliyuncs.com/static/demo-wan26/vace.mp4"]
                },
                "parameters": {
                    "size": "1280*720",
                    "duration": 5,
                    "audio": false,
                    "shot_type":"multi"
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

  <Tab title="弗吉尼亚">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="法兰克福">
    `GET https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

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
        #### 响应参数 <span id="810682d979i8q" />

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`<strong>（必选）</strong>

          任务ID。

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
          <strong>input\_video\_duration</strong> `integer`

          输入的参考视频的时长，单位秒。

          <strong>output\_video\_duration</strong> `integer`

          输出视频的时长，单位秒。

          <strong>duration</strong> `float`

          总视频时长。计费按duration时长计算。

          计算公式：`duration = input_video_duration + output_video_duration`。

          <strong>SR</strong> `integer`

          生成视频的分辨率档位。示例值：720。

          <strong>size</strong>`string`

          生成视频的分辨率。格式为“宽*高*”*，示例值：1280*720。

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
                "request_id": "caa62a12-8841-41a6-8af2-xxxxxx",
                "output": {
                    "task_id": "eff1443c-ccab-4676-aad3-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-12-16 00:25:59.869",
                    "scheduled_time": "2025-12-16 00:25:59.900",
                    "end_time": "2025-12-16 00:30:35.396",
                    "orig_prompt": "character1在沙发上开心的看电影",
                    "video_url": "https://dashscope-result-sh.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx"
                },
                 "usage": {
                    "duration": 10.0,
                    "size": "1280*720",
                    "input_video_duration": 5,
                    "output_video_duration": 5,
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

## DashScope SDK调用 <span id="r2v702a0h2sdk" />

SDK 的参数命名与[HTTP接口](/zh/model-studio/legacy-wan-reference-to-video-api-reference#42703589880ts)基本一致，参数结构根据语言特性进行封装。

由于参考生视频任务耗时较长（通常为1-5分钟），SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### Python SDK调用 <span id="29484e9a6a0qr" />

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.16`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
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

  <Tab title="美国">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1'`
  </Tab>

  <Tab title="法兰克福">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1'`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tabs>
  <Tab title="同步调用">
    同步调用会阻塞等待，直到视频生成完成并返回结果。

    ##### 请求示例 <span id="r2v741a6h5rq01" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为北京地域URL，各地域的URL不同
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_sync_call_r2v():
        # 同步调用，直接返回结果
        print('please wait...')
        rsp = VideoSynthesis.call(
            api_key=api_key,
            model='wan2.6-r2v-flash',
            prompt='Character2 坐在靠窗的椅子上，手持 character3，在 character4 旁演奏一首舒缓的美国乡村民谣。Character1 对Character2开口说道：“听起来不错”',
            reference_urls=[
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/hfugmr/wan-r2v-role1.mp4",
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png",
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
            ],
            shot_type='multi',
            audio=True,
            size='1280*720',
            duration=10,
            watermark=True)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_sync_call_r2v()
    ```
  </Tab>

  <Tab title="异步调用">
    异步调用会立即返回任务ID，需要自行轮询或等待任务完成。

    ##### 请求示例 <span id="r2v747a7h5rq02" />

    ```python expandable
    import os
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope

    # 以下为北京地域URL，各地域的URL不同
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call_r2v_26():
        # 异步调用，返回一个task_id
        rsp = VideoSynthesis.async_call(
            api_key=api_key,
            model='wan2.6-r2v-flash',
            prompt='Character2 坐在靠窗的椅子上，手持 character3，在 character4 旁演奏一首舒缓的美国乡村民谣。Character1 对Character2开口说道：“听起来不错”',
            reference_urls=[
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/hfugmr/wan-r2v-role1.mp4",
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png",
                "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
            ],
            shot_type='multi',
            audio=True,
            size='1280*720',
            duration=10,
            watermark=True)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print("task_id: %s" % rsp.output.task_id)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

        # 获取异步任务信息
        status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

        # 等待异步任务结束
        rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_async_call_r2v_26()
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="3f41c60916y03" />

<Tip>
  请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.14`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 `baseHttpApiUrl`:

<Tabs>
  <Tab title="北京">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="法兰克福">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1";`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tabs>
  <Tab title="同步调用">
    同步调用会阻塞等待，直到视频生成完成并返回结果。

    ##### 请求示例 <span id="r2v832a7h5jr01" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesis;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisParam;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.InputRequiredException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.JsonUtils;
    import com.alibaba.dashscope.utils.Constants;

    import java.util.ArrayList;
    import java.util.List;

    public class Ref2Video26 {

        static {
            // 以下为北京地域url，各地域的url不同
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void ref2video26() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();
            List<String> referenceUrls = new ArrayList<>();
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/hfugmr/wan-r2v-role1.mp4");
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4");
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png");
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png");

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.6-r2v-flash")
                            .prompt("Character2 坐在靠窗的椅子上，手持 character3，在 character4 旁演奏一首舒缓的美国乡村民谣。Character1 对Character2开口说道：“听起来不错”")
                            .referenceUrls(referenceUrls)
                            .shotType(VideoSynthesis.ShotType.MULTI)
                            .audio(Boolean.TRUE)
                            .size("1280*720")
                            .duration(10)
                            .watermark(Boolean.TRUE)
                            .build();
            System.out.println("please wait...");
            VideoSynthesisResult result = vs.call(param);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                ref2video26();
            } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    异步调用会立即返回任务ID，需要自行轮询或等待任务完成。

    ##### 请求示例 <span id="r2v839a7h5jr02" />

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

    import java.util.ArrayList;
    import java.util.List;

    public class Ref2Video26Async {

        static {
            // 以下为北京地域url，各地域的url不同
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void asyncRef2video26() throws ApiException, NoApiKeyException, InputRequiredException, InterruptedException {
            VideoSynthesis vs = new VideoSynthesis();
            List<String> referenceUrls = new ArrayList<>();
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/hfugmr/wan-r2v-role1.mp4");
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4");
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png");
            referenceUrls.add("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png");

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.6-r2v-flash")
                            .prompt("Character2 坐在靠窗的椅子上，手持 character3，在 character4 旁演奏一首舒缓的美国乡村民谣。Character1 对Character2开口说道：“听起来不错”")
                            .referenceUrls(referenceUrls)
                            .shotType(VideoSynthesis.ShotType.MULTI)
                            .audio(Boolean.TRUE)
                            .size("1280*720")
                            .duration(10)
                            .watermark(Boolean.TRUE)
                            .build();
            // 提交异步任务
            VideoSynthesisResult result = vs.asyncCall(param);
            System.out.println("task_id: " + result.getOutput().getTaskId());
            System.out.println(JsonUtils.toJson(result));

            // 等待任务完成
            result = vs.wait(result, null);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                asyncRef2video26();
            } catch (ApiException | NoApiKeyException | InputRequiredException | InterruptedException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```
  </Tab>
</Tabs>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
