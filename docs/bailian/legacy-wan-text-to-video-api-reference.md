> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-文生视频API参考（2.1-2.6）

> 万相-文生视频模型基于 文本提示词 ，生成一段流畅的视频。

<meta property="og:title" content="万相-文生视频API参考（2.1-2.6）" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/text-to-video-guide)

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/text-to-video-guide#06f39eafa2dwt)：确认模型所属的地域。
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

<Tip>
  此接口为<strong>旧版协议</strong>，支持<strong>wan2.6及早期系列模型（wan2.5/wan2.2/wanx2.1）</strong>。
</Tip>

由于文生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

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

        模型名称。可选模型请参见[模型列表](/zh/model-studio/model-pricing#ba6f7744d5e0o)。

        示例值：wan2.6-t2v。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母占一个字符，超过部分会自动截断。长度限制因模型版本而异：

          - wan2.6和wan2.5系列模型：不超过1500个字符。
          - wan2.2 和wanx2.1系列模型：不超过800个字符。

          示例值：一只小猫在月光下奔跑。

          提示词的使用技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在视频画面中看到的内容，可以对视频画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>audio\_url</strong> `string` （可选）

          <strong>支持模型：wan2.6和wan2.5系列模型。</strong>

          音频文件URL，模型将使用该音频生成视频。

          支持输入的格式：

          1. 公网URL：

             - 支持 HTTP 和 HTTPS 协议。
             - 示例值：<a href="https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3">[https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3](https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3)</a>。
          2. 临时URL：

             - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
             - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

          音频限制：

          - 格式：wav、mp3。
          - 时长：3～30s。
          - 文件大小：不超过15MB。
          - 超限处理：若音频长度超过 `duration` 值（ 如5秒），自动截取前5秒，其余部分丢弃。若音频长度不足视频时长，超出音频长度部分为无声视频。例如，音频为3秒，视频时长为5秒，输出视频前3秒有声，后2秒无声。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。如设置视频分辨率、开启prompt智能改写、添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>size</strong> `string` （可选）

          <Tip>
            - size直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#ba6f7744d5e0o)。
            - size必须设置为具体数值（如 `1280*720`），而不是 1:1或480P。
          </Tip>

          指定生成的视频分辨率，格式为`宽*高`。该参数的默认值和可用枚举值依赖于 model 参数，规则如下：

          - <strong>wan2.6-t2v</strong>：默认值为 `1920*1080`（1080P）。可选分辨率：720P、1080P对应的所有分辨率。
          - <strong>wan2.5-t2v-preview</strong>：默认值为 `1920*1080`（1080P）。可选分辨率：480P、720P、1080P对应的所有分辨率。
          - <strong>wan2.2-t2v-plus</strong>：默认值为 `1920*1080`（1080P）。可选分辨率：480P、1080P对应的所有分辨率。
          - <strong>wanx2.1-t2v-turbo</strong> ：默认值为 `1280*720`（720P）。可选分辨率：480P、720P 对应的所有分辨率。
          - <strong>wanx2.1-t2v-plus</strong>：默认值为`1280*720`（720P）。可选分辨率：720P 对应的所有分辨率。

          480P档位：可选的视频分辨率及其对应的视频宽高比为：

          - `832*480`：16:9。
          - `480*832`：9:16。
          - `624*624`：1:1。

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
            duration直接影响费用。费用 = 单价（基于分辨率）× 时长（秒），请在调用前确认[模型价格](/zh/model-studio/model-pricing#ba6f7744d5e0o)。
          </Tip>

          生成视频的时长，单位为秒。该参数的取值依赖于 model参数：

          - wan2.6-t2v：取值为\[2, 15]之间的整数。默认值为5。
          - wan2.5-t2v-preview：可选值为5、10。默认值为5。
          - wan2.2-t2v-plus：固定为5秒，且不支持修改。
          - wanx2.1-t2v-plus：固定为5秒，且不支持修改。
          - wanx2.1-t2v-turbo：固定为5秒，且不支持修改。

          <strong>prompt\_extend</strong>`boolean` （可选）

          是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

          - `true`：默认值，开启智能改写。
          - `false`：不开启智能改写。

          <strong>shot\_type</strong> `string` （可选）

          <strong>支持模型：wan2.6模型。</strong>

          指定生成视频的镜头类型，即视频是由一个连续镜头还是多个切换镜头组成。

          可选值：

          - `single`：默认值，输出单镜头视频。
          - `multi`：输出多镜头视频。

          生效条件：仅当`"prompt_extend": true` 时生效。

          参数优先级：`shot_type > prompt`。例如，若 shot\_type设置为"single"，即使 prompt 中包含“生成多镜头视频”，模型仍会输出单镜头视频。

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
          <Tab title="多镜头叙事">
            - `shot_type`: 多镜头必须设为 `"multi"`。
            - `prompt_extend`: 多镜头必须设为 `true`（开启智能改写以优化分镜描述）。

            ```bash

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.6-t2v",
                "input": {
                    "prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。",
                    "audio_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3"
                },
                "parameters": {
                    "size": "1280*720",
                    "prompt_extend": true,
                    "duration": 10,
                    "shot_type":"multi"
                }
            }'
            ```
          </Tab>

          <Tab title="传入音频文件">
            支持模型：wan2.6/wan2.5系列模型。

            可通过 `input.audio_url` 参数传入自定义音频的 URL。

            ```bash

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.5-t2v-preview",
                "input": {
                    "prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。",
                    "audio_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3"
                },
                "parameters": {
                    "size": "832*480",
                    "prompt_extend": true,
                    "duration": 10
                }
            }'
            ```
          </Tab>

          <Tab title="自动配音">
            支持模型wan2.6/wan2.5系列模型。

            若不提供 `input.audio_url` ，模型将根据视频内容自动生成匹配的背景音乐或音效。

            ```bash

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.5-t2v-preview",
                "input": {
                    "prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。"
                },
                "parameters": {
                    "size": "832*480",
                    "prompt_extend": true,
                    "duration": 10
                }
            }'
            ```
          </Tab>

          <Tab title="生成无声视频">
            仅wan2.2 和wanx2.1系列模型支持生成无声视频。默认生成无声视频，无需设置。

            > wan2.6 及wan2.5系列模型默认生成有声视频。

            ```bash

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.2-t2v-plus",
                "input": {
                    "prompt": "低对比度，在一个复古的70年代风格地铁站里，街头音乐家在昏暗的色彩和粗糙的质感中演奏。他穿着旧式夹克，手持吉他，专注地弹奏。通勤者匆匆走过，一小群人渐渐聚拢聆听。镜头慢慢向右移动，捕捉到乐器声与城市喧嚣交织的场景，背景中有老式的地铁标志和斑驳的墙面。"
                },
                "parameters": {
                    "size": "832*480",
                    "prompt_extend": true
                }
            }'
            ```
          </Tab>

          <Tab title="使用反向提示词">
            支持模型：所有模型。

            通过 negative\_prompt 排除“花朵”元素，避免其出现在视频画面中。

            ```bash

            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.2-t2v-plus",
                "input": {
                    "prompt": "一只小猫在月光下奔跑",
                    "negative_prompt": "花朵"
                },
                "parameters": {
                    "size": "832*480"
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

          当 `prompt_extend=true` 时，系统会对输入 prompt 进行智能改写，此字段返回实际用于生成的优化后 prompt。

          - 若 `prompt_extend=false`，该字段不会返回。
          - 注意：wan2.6 模型无论 `prompt_extend` 取值如何，均不返回此字段。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>video\_duration</strong> `integer`

          仅在使用 wan2.5 及以下版本模型时返回，用于计费。

          生成视频的时长，单位秒。枚举值为5、10。

          <strong>duration</strong> `float`

          仅在使用 wan2.6 模型时返回，用于计费。

          表示总的视频时长，且`duration=input_video_duration+output_video_duration`。

          <strong>input\_video\_duration</strong> `integer`

          仅在使用 wan2.6 模型时返回。固定为0。

          <strong>output\_video\_duration</strong> `integer`

          仅在使用 wan2.6 模型时返回。

          输出视频的时长，单位秒。其值等同于`input.duration`的值。

          <strong>SR</strong> `integer`

          仅在使用 wan2.6 模型时返回。生成视频的分辨率档位。示例值：720。

          <strong>size</strong> `string`

          仅在使用 wan2.6 模型时返回。生成视频的分辨率。格式为“宽*高*”*，示例值：1920*1080。

          <strong>video\_ratio</strong> `string`

          仅 wan2.5 及以下版本模型时返回。生成视频的分辨率。格式为“宽*高*”*，示例值：832*480。

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
                    "submit_time": "2025-09-29 14:18:52.331",
                    "scheduled_time": "2025-09-29 14:18:59.290",
                    "end_time": "2025-09-29 14:23:39.407",
                    "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。",
                    "video_url": "https://dashscope-result-sh.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx"
                },
                "usage": {
                    "duration": 10,
                    "size": "1280*720",
                    "input_video_duration": 0,
                    "output_video_duration": 10,
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

## DashScope SDK调用 <span id="db17b3098c2iv" />

SDK 的参数命名与HTTP接口基本一致，参数结构根据语言特性进行封装。

由于文生视频任务耗时较长（通常为1-5分钟），SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### Python SDK调用 <span id="t2v26pyh426" />

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.16`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 `dashscope.base_http_api_url`:

<Tabs>
  <Tab title="北京">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1'`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="弗吉尼亚">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1'`
  </Tab>

  <Tab title="法兰克福">
    `dashscope.base_http_api_url = 'https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1'`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="3cd4969b00193" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。获取URL：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_sync_call_t2v():
        # call sync api, will return the result
        print('please wait...')
        rsp = VideoSynthesis.call(api_key=api_key,
                                  model='wan2.6-t2v',
                                  prompt='一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。',
                                  audio_url='https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3',
                                  size='1280*720',
                                  duration=10,
                                  negative_prompt="",
                                  prompt_extend=True,
                                  watermark=False,
                                  seed=12345)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_sync_call_t2v()
    ```

    ##### 响应示例 <span id="0c0533477emwb" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "status_code": 200,
        "request_id": "167f3beb-3dd0-47fe-a83c-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "5b65411f-d946-4e29-859e-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-result-bj.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "submit_time": "2025-10-23 11:47:23.879",
            "scheduled_time": "2025-10-23 11:47:34.351",
            "end_time": "2025-10-23 11:52:35.323",
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "size": "1280*720",
            "input_video_duration": 0,
            "output_video_duration": 10,
            "SR": 720
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="b994ca9918t7e" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。获取URL：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call_t2v():
        # call async api, will return the task information
        # you can get task status with the returned task id.
        rsp = VideoSynthesis.async_call(api_key=api_key,
                                        model='wan2.6-t2v',
                                        prompt='一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。',
                                        audio_url='https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3',
                                        size='1280*720',
                                        duration=10,
                                        negative_prompt="",
                                        prompt_extend=True,
                                        watermark=False,
                                        seed=12345)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print("task_id: %s" % rsp.output.task_id)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

        # get the task information include the task status.
        status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)  # check the task status
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

        # wait the task complete, will call fetch interval, and check it's in finished status.
        rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_async_call_t2v()
    ```

    ##### 响应示例 <span id="ae28dc8f72g6i" />

    1、创建任务的响应示例

    ```json
    {
    	"status_code": 200,
    	"request_id": "c86ff7ba-8377-917a-90ed-xxxxxx",
    	"code": "",
    	"message": "",
    	"output": {
    		"task_id": "721164c6-8619-4a35-a6d9-xxxxxx",
    		"task_status": "PENDING",
    		"video_url": ""
    	},
    	"usage": null
    }
    ```

    2、查询任务结果的响应示例

    > video\_url 有效期24小时，请及时下载视频。

    ```
    {
        "status_code": 200,
        "request_id": "167f3beb-3dd0-47fe-a83c-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "5b65411f-d946-4e29-859e-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-result-bj.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "submit_time": "2025-10-23 11:47:23.879",
            "scheduled_time": "2025-10-23 11:47:34.351",
            "end_time": "2025-10-23 11:52:35.323",
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "size": "1280*720",
            "input_video_duration": 0,
            "output_video_duration": 10,
            "SR": 720
        }
    }
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="38800f946d43r" />

<Tip>
  请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.14`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 `Constants.baseHttpApiUrl`:

<Tabs>
  <Tab title="北京">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="弗吉尼亚">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1";`
  </Tab>

  <Tab title="法兰克福">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1";`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="3e3609efb9rhl" />

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
    import java.util.HashMap;
    import java.util.Map;

    public class Text2Video {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。获取URL：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * Create a video compositing task and wait for the task to complete.
         */
        public static void text2Video() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);
            parameters.put("watermark", false);
            parameters.put("seed", 12345);

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.6-t2v")
                            .prompt("一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。")
                            .audioUrl("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3")
                            .negativePrompt("")
                            .size("1280*720")
                            .duration(10)
                            .parameters(parameters)
                            .build();
            System.out.println("please wait...");
            VideoSynthesisResult result = vs.call(param);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                text2Video();
            } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="dfe6c9cc786iy" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "request_id": "c1209113-8437-424f-a386-xxxxxx",
        "output": {
            "task_id": "966cebcd-dedc-4962-af88-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-result-sh.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。",
            "submit_time": "2026-01-22 23:13:40.553",
            "scheduled_time": "2026-01-22 23:13:49.415",
            "end_time": "2026-01-22 23:17:56.380"
        },
        "usage": {
            "video_count": 1,
            "duration": 10.0,
            "size": "1280*720",
            "input_video_duration": 0.0,
            "output_video_duration": 10.0,
            "SR": "720"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>

  <Tab title="异步调用">
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

    public class Text2Video {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。获取URL：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

         // 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * Create a video compositing task and wait for the task to complete.
         */
        public static void text2Video() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);
            parameters.put("watermark", false);
            parameters.put("seed", 12345);

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.6-t2v")
                            .prompt("一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。")
                            .audioUrl("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3")
                            .negativePrompt("")
                            .size("1280*720")
                            .duration(10)
                            .parameters(parameters)
                            .build();

            // 异步调用
            VideoSynthesisResult task = vs.asyncCall(param);
            System.out.println(JsonUtils.toJson(task));
            System.out.println("please wait...");

            //获取结果
            VideoSynthesisResult result = vs.wait(task, apiKey);
            System.out.println(JsonUtils.toJson(result));
        }

        // 获取任务列表
        public static void listTask() throws ApiException, NoApiKeyException {
            VideoSynthesis is = new VideoSynthesis();
            AsyncTaskListParam param = AsyncTaskListParam.builder().build();
            param.setApiKey(apiKey);
            VideoSynthesisListResult result = is.list(param);
            System.out.println(result);
        }

        // 获取单个任务结果
        public static void fetchTask(String taskId) throws ApiException, NoApiKeyException {
            VideoSynthesis is = new VideoSynthesis();
            // 如果已设置 DASHSCOPE_API_KEY 为环境变量，apiKey 可为空
            VideoSynthesisResult result = is.fetch(taskId, apiKey);
            System.out.println(result.getOutput());
            System.out.println(result.getUsage());
        }

        public static void main(String[] args) {
            try {
                text2Video();
            } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="38e1e3c7303rm" />

    1、创建任务的响应示例。

    ```json
    {
            "request_id": "5dbf9dc5-4f4c-9605-85ea-xxxxxxxx",
    	"output": {
    		"task_id": "7277e20e-aa01-4709-xxxxxxxx",
    		"task_status": "PENDING"
    	}
    }
    ```

    2、查询任务结果的响应示例

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "request_id": "c1209113-8437-424f-a386-xxxxxx",
        "output": {
            "task_id": "966cebcd-dedc-4962-af88-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-result-sh.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。",
            "submit_time": "2026-01-22 23:13:40.553",
            "scheduled_time": "2026-01-22 23:13:49.415",
            "end_time": "2026-01-22 23:17:56.380"
        },
        "usage": {
            "video_count": 1,
            "duration": 10.0,
            "size": "1280*720",
            "input_video_duration": 0.0,
            "output_video_duration": 10.0,
            "SR": "720"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>
</Tabs>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="c928ee689aw6l" />

#### Q: 如何将临时的视频链接转为永久链接？ <span id="99f5bae3c2y7a" />

A: 不能直接转换该链接。正确的做法是：后端服务获取到url后，通过代码下载该视频文件，然后将其上传到永久对象存储服务（如阿里云 OSS），生成一个新的、永久访问链接。

<Accordion title="示例代码：下载视频到本地">
  ```python
  import requests

  def download_and_save_video(video_url, save_path):
      try:
          response = requests.get(video_url, stream=True, timeout=300) # 设置超时
          response.raise_for_status() # 如果HTTP状态码不是200，则引发异常
          with open(save_path, 'wb') as f:
              for chunk in response.iter_content(chunk_size=8192):
                  f.write(chunk)
          print(f"视频已成功下载到: {save_path}")
          # 此处可以接上传到永久存储的逻辑
      except requests.exceptions.RequestException as e:
          print(f"下载视频失败: {e}")

  if __name__ == '__main__':
      video_url = "http://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxxx"
      save_path = "video.mp4"
      download_and_save_video(video_url, save_path)
  ```
</Accordion>

#### Q: 返回的视频链接可以在浏览器中直接播放吗？ <span id="bc74fbbfach4a" />

A: 不建议这样做，因为链接会在 24 小时后失效。最佳实践是后端下载转存后，使用永久链接进行视频播放。
