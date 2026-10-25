> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相2.7-文生视频API参考

> 万相2.7-文生视频模型基于 文本提示词 ，生成一段流畅的视频。

<meta property="og:title" content="万相2.7-文生视频API参考" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/text-to-video-guide)

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：前往模型广场选择模型，并确认其所属地域。
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

## HTTP调用 <span id="a64683584eh3t" />

<Tip>
  此接口为<strong>新版协议</strong>，仅支持<strong>wan2.7模型</strong>。
</Tip>

由于文生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="92226a3dffgfc" />

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
        #### 请求参数 <span id="788ddddd84ee2" />

        ##### 请求头（Headers） <span id="1a653493dcvcz" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="4ea8b46965895" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选模型请参见[模型列表](/zh/model-studio/model-pricing#ba6f7744d5e0o)。

        示例值：wan2.7-t2v、wan2.7-t2v-2026-06-12。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母占一个字符，超过部分会自动截断。长度限制因模型版本而异：

          - wan2.7-t2v、wan2.7-t2v-2026-06-12：不超过5000个字符。

          示例值：一只小猫在月光下奔跑。

          提示词的使用技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在视频画面中看到的内容，可以对视频画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>audio\_url</strong> `string` （可选）

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
          - 时长：2～30s。
          - 文件大小：不超过15MB。
          - 超限处理：若音频长度超过 `duration` 值（ 如5秒），自动截取前5秒，其余部分丢弃。若音频长度不足视频时长，超出音频长度部分为无声视频。例如，音频为3秒，视频时长为5秒，输出视频前3秒有声，后2秒无声。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频处理参数。如设置视频分辨率、开启prompt智能改写、添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` （可选）

          <Tip>
            resolution直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#ba6f7744d5e0o)。
          </Tip>

          生成视频的分辨率档位，用于控制视频的清晰度（总像素）。

          - wan2.7-t2v、wan2.7-t2v-2026-06-12：可选值：720P、1080P。默认值为`1080P`。

          <strong>ratio</strong> `string` （可选）

          生成视频的宽高比。

          - `16:9`（默认值）
          - `9:16`
          - `1:1`
          - `4:3`
          - `3:4`

          不同宽高比对应的输出视频分辨率（宽高像素值）见下方表格。

          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p>输出视频分辨率（宽\*高）</p></td></tr><tr><td rowSpan={5}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td><p>1:1</p></td><td><p>960\*960</p></td></tr><tr><td><p>4:3</p></td><td><p>1104\*832</p></td></tr><tr><td><p>3:4</p></td><td><p>832\*1104</p></td></tr><tr><td rowSpan={5}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr><tr><td><p>1:1</p></td><td><p>1440\*1440</p></td></tr><tr><td><p>4:3</p></td><td><p>1648\*1248</p></td></tr><tr><td><p>3:4</p></td><td><p>1248\*1648</p></td></tr></tbody></table>

          <strong>duration</strong> `integer` （可选）

          <Tip>
            duration直接影响费用，按秒计费，请在调用前确认[模型价格](/zh/model-studio/model-pricing#ba6f7744d5e0o)。
          </Tip>

          生成视频的时长，单位为秒。

          - wan2.7-t2v、wan2.7-t2v-2026-06-12：取值为\[2, 15]之间的整数。默认值为5。

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
          <Tab title="多镜头叙事">
            通过 `prompt` 自然语言控制镜头结构，无需配置`shot_type` 参数，配置也不生效。

            - 单镜头：输入“生成单镜头视频”。
            - 多镜头：输入“生成多镜头视频” 或使用时间戳描述分镜（如 “第1个镜头\[0-3秒] 全景：雨夜的纽约街头”）
            - 默认：未指定时，模型根据 `prompt` 内容自行理解。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-t2v-2026-06-12",
                "input": {
                    "prompt": "一段紧张刺激的侦探追查故事，展现电影级叙事能力。第1个镜头[0-3秒] 全景：雨夜的纽约街头，霓虹灯闪烁，一位身穿黑色风衣的侦探快步行走。 第2个镜头[3-6秒] 中景：侦探进入一栋老旧建筑，雨水打湿了他的外套，门在他身后缓缓关闭。 第3个镜头[6-9秒] 特写：侦探的眼神坚毅专注，远处传来警笛声，他微微皱眉思考。 第4个镜头[9-12秒] 中景：侦探在昏暗走廊中小心前行，手电筒照亮前方。 第5个镜头[12-15秒] 特写：侦探发现关键线索，脸上露出恍然大悟的表情。"
                },
                "parameters": {
                    "resolution": "720P",
                    "ratio": "16:9",
                    "prompt_extend": true,
                    "watermark": true,
                    "duration": 15
                }
            }'
            ```
          </Tab>

          <Tab title="传入音频文件">
            可通过 `input.audio_url` 参数传入自定义音频的 URL。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-t2v-2026-06-12",
                "input": {
                    "prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。",
                    "audio_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3"
                },
                "parameters": {
                    "resolution": "720P",
                    "ratio": "16:9",
                    "prompt_extend": true,
                    "duration": 10
                }
            }'
            ```
          </Tab>

          <Tab title="自动配音">
            若不提供 `input.audio_url` ，模型将根据视频内容自动生成匹配的背景音乐或音效。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-t2v-2026-06-12",
                "input": {
                    "prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是“可爱”与“霸气”的搞笑和史诗般的融合。"
                },
                "parameters": {
                    "resolution": "720P",
                    "ratio": "16:9",
                    "prompt_extend": true,
                    "duration": 10
                }
            }'
            ```
          </Tab>

          <Tab title="使用反向提示词">
            通过 negative\_prompt 排除“花朵”元素，避免其出现在视频画面中。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-t2v-2026-06-12",
                "input": {
                    "prompt": "一只小猫在月光下奔跑",
                    "negative_prompt": "花朵"
                },
                "parameters": {
                    "resolution": "720P",
                    "ratio": "16:9"
                }
            }'
            ```
          </Tab>

          <Tab title="dashscope CLI">
            ```bash
            export DASHSCOPE_API_KEY="your-api-key"
            # 将 {WorkspaceId} 替换为业务空间ID，cn-beijing 替换为对应地域（新加坡: ap-southeast-1, 美东: us-east-1）
            export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
            dashscope video-synthesis create -m wan2.7-t2v -p "小猫在奔跑"
            ```

            <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

            完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
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
        #### 响应参数 <span id="82c91c55bajs9" />

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

### 步骤2：根据任务ID查询结果 <span id="717bb080fb5ei" />

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
        #### 请求参数 <span id="9d845a49dby6o" />

        ##### 请求头（Headers） <span id="3e1bc6b9191mw" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="5670618eafm66" />

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
        #### 响应参数 <span id="899988bcbfllm" />

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
          <strong>duration</strong> `float`

          输出视频的总视频时长，用于计费。

          其中，`duration=output_video_duration`。

          <strong>input\_video\_duration</strong> `integer`

          固定为0。

          <strong>output\_video\_duration</strong> `integer`

          输出视频的时长，单位秒。其值等同于`input.duration`的值。

          <strong>SR</strong> `integer`

          生成视频的分辨率档位。示例值：720。

          <strong>ratio</strong> `string`

          生成视频的宽高比。示例值：16:9。

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
                    "input_video_duration": 0,
                    "output_video_duration": 10,
                    "video_count": 1,
                    "ratio": "16:9",
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

### Python SDK调用 <span id="04ca22d51edsm" />

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.16`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 `dashscope.base_http_api_url`:

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
    ##### 请求示例 <span id="t2v27pysyncreq" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为北京地域URL，各地域的URL不同，获取URL：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_sync_call_t2v():
        # call sync api, will return the result
        print('please wait...')
        rsp = VideoSynthesis.call(api_key=api_key,
                                  model='wan2.7-t2v-2026-06-12',
                                  prompt='一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说："青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。"。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是"可爱"与"霸气"的搞笑和史诗般的融合。',
                                  audio_url='https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3',
                                  resolution='720P',
                                  ratio='16:9',
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

    ##### 响应示例 <span id="t2v27pysyncresph" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "status_code": 200,
        "request_id": "72082605-4559-9bb7-aa5c-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "43b954a2-3e86-4681-9443-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxxx",
            "submit_time": "2026-04-16 10:50:12.718",
            "scheduled_time": "2026-04-16 10:50:21.430",
            "end_time": "2026-04-16 11:02:00.274",
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：\"青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。\"。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是\"可爱\"与\"霸气\"的搞笑和史诗般的融合。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "input_video_duration": 0,
            "output_video_duration": 10,
            "SR": 720,
            "ratio": "16:9"
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="t2v27pyasyncreq" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为北京地域URL，各地域的URL不同，获取URL：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call_t2v():
        # call async api, will return the task information
        # you can get task status with the returned task id.
        rsp = VideoSynthesis.async_call(api_key=api_key,
                                        model='wan2.7-t2v-2026-06-12',
                                        prompt='一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说："青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。"。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是"可爱"与"霸气"的搞笑和史诗般的融合。',
                                        audio_url='https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3',
                                        resolution='720P',
                                        ratio='16:9',
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

    ##### 响应示例 <span id="t2v27pyasyncresph" />

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

    ```json
    {
        "status_code": 200,
        "request_id": "72082605-4559-9bb7-aa5c-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "43b954a2-3e86-4681-9443-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxxx",
            "submit_time": "2026-04-16 10:50:12.718",
            "scheduled_time": "2026-04-16 10:50:21.430",
            "end_time": "2026-04-16 11:02:00.274",
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：\"青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。\"。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是\"可爱\"与\"霸气\"的搞笑和史诗般的融合。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "input_video_duration": 0,
            "output_video_duration": 10,
            "SR": 720,
            "ratio": "16:9"
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
  </Tab>

  <Tab title="新加坡">
    `Constants.baseHttpApiUrl = "https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1";`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="t2v27jvsyncreq" />

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
            // 以下为北京地域url，各地域的url不同，获取url：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
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

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.7-t2v-2026-06-12")
                            .prompt("一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是可爱与霸气的搞笑和史诗般的融合。")
                            .audioUrl("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3")
                            .negativePrompt("")
                            .resolution("720P")
                            .ratio("16:9")
                            .duration(10)
                            .promptExtend(true)
                            .watermark(false)
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

    ##### 响应示例 <span id="t2v27jvsyncresph" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "request_id": "c1209113-8437-424f-a386-xxxxxx",
        "output": {
            "task_id": "966cebcd-dedc-4962-af88-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-result-sh.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说："青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。"。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是"可爱"与"霸气"的搞笑和史诗般的融合。",
            "submit_time": "2026-04-16 23:13:40.553",
            "scheduled_time": "2026-04-16 23:13:49.415",
            "end_time": "2026-04-16 23:17:56.380"
        },
        "usage": {
            "video_count": 1,
            "duration": 10.0,
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
    ##### 请求示例 <span id="t2v27jvasyncreq" />

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
            // 以下为北京地域url，各地域的url不同，获取url：https://help.aliyun.com/zh/model-studio/text-to-video-api-reference
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

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.7-t2v-2026-06-12")
                            .prompt("一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说：”青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。“。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是可爱与霸气的搞笑和史诗般的融合。")
                            .audioUrl("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250923/hbiayh/%E4%BB%8E%E5%86%9B%E8%A1%8C.mp3")
                            .negativePrompt("")
                            .resolution("720P")
                            .ratio("16:9")
                            .duration(10)
                            .promptExtend(true)
                            .watermark(false)
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

    ##### 响应示例 <span id="t2v27jvasyncresph" />

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
            "orig_prompt": "一幅史诗级可爱的场景。一只小巧可爱的卡通小猫将军，身穿细节精致的金色盔甲，头戴一个稍大的头盔，勇敢地站在悬崖上。他骑着一匹虽小但英勇的战马，说："青海长云暗雪山，孤城遥望玉门关。黄沙百战穿金甲，不破楼兰终不还。"。悬崖下方，一支由老鼠组成的、数量庞大、无穷无尽的军队正带着临时制作的武器向前冲锋。这是一个戏剧性的、大规模的战斗场景，灵感来自中国古代的战争史诗。远处的雪山上空，天空乌云密布。整体氛围是"可爱"与"霸气"的搞笑和史诗般的融合。",
            "submit_time": "2026-04-16 23:13:40.553",
            "scheduled_time": "2026-04-16 23:13:49.415",
            "end_time": "2026-04-16 23:17:56.380"
        },
        "usage": {
            "video_count": 1,
            "duration": 10.0,
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

#### Q: 从 wan2.6 升级到 wan2.7，代码需要改哪些地方？ <span id="cd79eb5127f4j" />

A: 主要有两处变更，迁移时需调整参数：

- <strong>调整输出视频分辨率的控制方式</strong>：wan2.7不再使用 `size` 字段，改为通过 `resolution`（分辨率档位）和 `ratio`（宽高比）两个字段组合来定义输出视频分辨率；而早期模型直接使用 `size` 字段。
- <strong>镜头类型字段移除</strong>：wan2.7不再支持 `shot_type` 字段，该参数已弃用，可直接通过prompt来描述镜头类型；早期模型支持通过该字段控制镜头类型。
