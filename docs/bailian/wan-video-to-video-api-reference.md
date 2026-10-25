> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相2.7-参考生视频API参考

> 万相-参考生视频模型支持 多模态输入 （图片、视频、音频），生成保持角色形象和音色一致性的视频，适用于单角色表演或多角色互动场景。

<meta property="og:title" content="万相2.7-参考生视频API参考" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/video-to-video-guide)

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证模型、Endpoint URL和API Key 均属于<strong>同一地域</strong>。跨地域调用将会失败。

- [选择模型](/zh/model-studio/video-to-video-guide#06f39eafa2dwt)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL。
- <strong>配置API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Note>
  本文的示例代码适用于<strong>北京地域</strong>。
</Note>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP调用 <span id="7f493e3256ajz" />

<Tip>
  此接口为<strong>新版协议</strong>，支持<strong>wan2.7模型</strong>。
</Tip>

由于视频生成任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="19494db8e8gey" />

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
        #### 请求参数 <span id="34ce9ac268wb7" />

        ##### 请求头（Headers） <span id="69e447998dxho" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="ad5d07b350xer" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。模型列表与价格详见[模型价格](/zh/model-studio/model-pricing#5c3d28ad8a4x8)。

        示例值：wan2.7-r2v、wan2.7-r2v-2026-06-12。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字、字母、标点占一个字符，超过部分会自动截断。

          - wan2.7-r2v、wan2.7-r2v-2026-06-12：不超过5000个字符。

          <strong>参考指代</strong>：当为中文提示词时，参考图片时通过“<strong>图1、图2</strong>”这类标识指代，参考视频时通过“<strong>视频1、视频2</strong>”这类标识指代。英文提示词则写为“Image 1”、"Video 1”这类标识。英文字母和数字之间有空格，首字母大写。顺序与`media`数组顺序一致。图和视频分别计数，即同时存在图1、视频1等标识。若参考素材有且仅有一张图片或一个视频，则可简化表述为”<strong>参考图片</strong>”或“<strong>参考视频</strong>”。

          <strong>画面描述</strong>：假设参考图1是一只猫，图2是一个房间，要描述猫在房间里玩耍，支持两种写法：一种是直接使用标识指代（如“图1在图2里玩耍”）；另一种是结合主体与场景补充说明（如“图1的猫在图2的房间里玩耍”）。

          当参考图片为多宫格（故事板图像）时，提示词建议按照多分镜的形式描述画面内容。无需描述每个宫格，提供关键分镜内容即可，模型将自动识别宫格逻辑并智能补全镜头内容。为达到更好的效果，建议单次仅输入一张多宫格图。

          提示词的使用技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在视频画面中出现的内容，可以对视频画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体素材数组，素材包括图像、视频和音频。支持图像/视频输入作为视觉参考，图像支持多视图，常见参考角色、道具、场景等。

          - 数组中每个元素为一个媒体对象，包含 `type` 与 `url` 字段。
          - 按照数组顺序定义`prompt`中角色引用的顺序。图和视频分别计数，即可同时存在图1、视频1。

            - 数组中的第 1 个`reference_video` 对应 <strong>视频1</strong>，第 2 个对应 <strong>视频2</strong>，以此类推。
            - 数组中的第 1 个`reference_image`对应 <strong>图1</strong>，第 2 个对应 <strong>图2</strong>，以此类推。

          <Accordion title="属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。可选值为：

            - `reference_image`：参考图像。提供主体角色（人物/动物/物体）和场景参考。
            - `reference_video`：参考视频。提供主体角色（人物/动物/物体）和音色参考，不推荐传入空镜视频。
            - `first_frame`：首帧图像。基于首帧生成视频，通常包含主体角色（人物/动物/物体）。支持同时传入首帧图联合控制，常见用法如下：

              - 首帧中已经出现待参考主体：此时可以搭配主体参考强化一致性，或进行音色参考。
              - 首帧中未出现待参考主体：此时可以用主体参考来定义视频动态过程中新出现的主体特征。

            素材限制：

            - 首帧图像，最多传入1张。
            - 参考图像和参考视频至少传入1个，<strong>参考图像 + 参考视频 ≤ 5</strong>。
            - 参考素材为主体角色时，仅包含单一角色。

            <strong>url</strong> `string` <strong>（必选）</strong>

            媒体素材URL。每个值可指向<strong>一张图像或一段视频</strong>。

            <Accordion title="传入参考图像（type=reference_image）" defaultOpen>
              参考图像URL或 Base64 编码数据。参考图可以是主体（人物/动物/物体）或者背景。当包含主体时，仅包含一个角色。

              图像限制：

              - 格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
              - 分辨率：宽度和高度范围为\[240, 8000]像素。
              - 宽高比：1:8～8:1。
              - 文件大小：不超过20MB。

              支持输入的格式：

              1. 公网URL:

                 - 支持HTTP或HTTPS协议。
                 - 示例值：<a href="https://xxx/xxx.png">[https://xxx/xxx.png](https://xxx/xxx.png)</a>。
              2. 临时URL：

                 - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                 - 示例值：oss\://dashscope-instant/xxx/xxx.png。
              3. Base64 编码图像后的字符串：

                 - 数据格式：`data:{MIME_type};base64,{base64_data}`。
                 - 示例值：data:image/png;base64,GDU7MtCZzEbTbmRZ......。（编码字符串过长，仅展示片段）
                 - 详情请参见[传入图像](/zh/model-studio/image-to-video-guide#32d9db99f1fk0)。
            </Accordion>

            <Accordion title="传入参考视频（type=reference_video）" defaultOpen>
              参考视频URL。视频内容建议包含主体（人物/动物/物体），不建议使用背景或空镜视频。当包含主体时，仅包含一个角色。若视频有声音，也可以参考音色。

              视频限制：

              - 格式：mp4、mov。
              - 时长：1～30s。
              - 分辨率：宽度和高度范围为\[240,4096]像素。
              - 宽高比：1:8～8:1。
              - 文件大小：不超过100MB。

              支持输入的格式：

              1. 公网URL：

                 - 支持HTTP和HTTPS协议。
                 - 示例值：<a href="https://xxx/xxx.mp4">[https://xxx/xxx.mp4](https://xxx/xxx.mp4)</a>。
              2. 临时URL：

                 - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                 - 示例值：oss\://dashscope-instant/xxx/xxx.mp4。
            </Accordion>

            <strong>reference\_voice</strong> `string` <strong>（可选）</strong>

            音频 URL。用于指定参考素材（图像/视频）中主体角色的音色。与`reference_image`或`reference_video`搭配使用。该音频仅参考音色，与说话内容无关。建议参考音频语种与提示词语种保持一致，匹配效果更佳。

            音频生效逻辑：

            - 默认行为：若 `reference_video` 本身包含音频，但未指定 `reference_voice`，默认使用视频原声。
            - 优先级：若同时传入 `reference_video`（含音频）和 `reference_voice`，则优先使用 `reference_voice` 的音色，覆盖视频原声。

            音频限制：

            - 格式：wav、mp3。
            - 时长：1～10s。
            - 文件大小：不超过15MB。

            支持输入的格式：

            1. 公网URL：

               - 支持HTTP和HTTPS协议。
               - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
            2. 临时URL：

               - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
               - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频处理参数，如设置视频分辨率。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` （可选）

          <Tip>
            `resolution`直接影响费用，请在调用前确认[百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)。
          </Tip>

          生成视频的分辨率档位，用于控制视频的清晰度（总像素）。

          - wan2.7-r2v、wan2.7-r2v-2026-06-12：可选值：720P、1080P。默认值为`1080P`。

          <strong>ratio</strong> `string` （可选）

          生成视频的宽高比。

          生效逻辑：

          - 未传入首帧图像：按指定的 `ratio` 参数生成视频。
          - 已传入首帧图像：自动忽略 `ratio` 参数，以首帧图像的宽高比生成近似比例的视频。

          可选值为：

          - `16:9`（默认值）
          - `9:16`
          - `1:1`
          - `4:3`
          - `3:4`

          不同宽高比对应的输出视频分辨率（宽高像素值）见下方表格。

          <strong>duration</strong> `integer` （可选）

          <Tip>
            duration直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#5c3d28ad8a4x8)。
          </Tip>

          生成视频的时长，单位为秒。

          - wan2.7-r2v、wan2.7-r2v-2026-06-12：默认值为5。

            - 当参考素材中包含视频时：取值为\[2, 10]之间的整数。
            - 当参考素材中不包含视频时：取值为\[2, 15]之间的整数。

          <strong>prompt\_extend</strong>`boolean` （可选）

          是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

          - true：默认值，开启智能改写。
          - false：不开启智能改写。

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
          <Tab title="多主体参考（图像+视频+音色）">
            传入多张参考素材（图像+视频），并指定音色生成视频。

            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-r2v-2026-06-12",
                "input": {
                    "prompt": "视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 ",
                    "media": [
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/sjuytr/wan-r2v-object-girl.jpg",
                            "reference_voice": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/gbqewz/wan-r2v-girl-voice.mp3"
                        },
                        {
                            "type": "reference_video",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                            "reference_voice": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/isllrq/wan-r2v-boy-voice.mp3"
                        },
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/rtjeqf/wan-r2v-object3.png"
                        },
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png"
                        },
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                        }
                    ]
                },
                "parameters": {
                    "resolution": "720P",
                    "ratio": "16:9",
                    "duration": 10,
                    "prompt_extend": false,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="单图参考（多宫格图像）">
            传入一张九宫格参考图，可以控制故事走向、机位构图与角色设定，生成视频。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.7-r2v-2026-06-12",
                "input": {
                    "prompt": "参考图片，3D卡通冒险电影风，角色Q版但材质细腻，动作流畅，色彩鲜明，保持角色与森林场景一致，不要加入文字。氛围： 冒险、轻快、神秘、童趣。角色： 小男孩探险家：圆帽、背包、短斗篷。小伙伴：会飞的小机器人，圆形身体，蓝色发光眼。场景： 奇幻森林，巨大树根、蘑菇、藤蔓、藏宝洞口、阳光光束。分镜脚本： 1. 全景：奇幻森林里高大树木与光束交错，环境神秘明亮。 2. 中景：小男孩拨开藤蔓向前探路。 3. 中景：小机器人飞在他身边，用蓝光扫描前方。 4. 特写：一张旧藏宝图在男孩手里展开。 5. 近景：他露出兴奋表情，眼睛亮起来。 6. 动作镜头：两人跳过树根和小溪，继续深入森林。 7. 中景：藤蔓后方露出一个被苔藓覆盖的宝箱。 8. 特写：宝箱边缘闪出金色光芒。 9. 收束镜头：男孩和小机器人站在宝箱前惊喜对望，冒险感拉满。",
                    "media": [
                        {
                            "type": "reference_image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260403/wgjaxy/banana_storyboard_00000020.png"
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
        #### 响应参数 <span id="17bfdb2bdclsh" />

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

### 步骤2：根据任务ID查询结果 <span id="f0dccc56b7vho" />

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
        #### 请求参数 <span id="adeb821afa3qe" />

        ##### 请求头（Headers） <span id="10fdb2d3060rn" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="13b97de165qnk" />

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
        #### 响应参数 <span id="e1ca3ef445nzd" />

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

          输入视频的时长，单位秒。

          <strong>output\_video\_duration</strong> `integer`

          输出视频的时长，单位秒。

          <strong>duration</strong> `integer`

          总视频时长。计费按duration时长计算。

          计算公式：`duration = input_video_duration + output_video_duration`。

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
                "request_id": "52cade0d-905e-9b7d-a01e-xxxxxx",
                "output": {
                    "task_id": "18814247-f944-4102-aa4a-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-04-02 22:53:19.537",
                    "scheduled_time": "2026-04-02 22:53:30.427",
                    "end_time": "2026-04-02 23:00:39.287",
                    "orig_prompt": "视频2抱着图片3在咖啡厅里弹奏一支舒缓的美式乡村民谣，视频1笑着看着视频2，并缓缓向他走去",
                    "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?xxxx"
                },
                "usage": {
                    "duration": 15,
                    "input_video_duration": 5,
                    "output_video_duration": 10,
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

SDK 的参数命名与[HTTP接口](/zh/model-studio/wan-video-to-video-api-reference#7f493e3256ajz)基本一致，参数结构根据语言特性进行封装。

参考生视频任务通常需要1–5分钟。SDK 封装了HTTP异步调用流程，支持同步和异步两种调用方式。

> 实际耗时取决于排队任务数和服务运行状态。

### Python SDK调用 <span id="549026665ct3b" />

<Tip>
  运行以下代码前，请确保 DashScope Python SDK版本 <strong>≥1.25.16</strong>。

  版本过低可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 <strong>base\_http\_api\_url</strong>:

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
    同步调用会阻塞直到视频生成完成，直接返回结果。

    ##### 请求示例 <span id="r2v742a0h5rq01" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_sync_call_r2v():
        # 同步调用，直接返回结果
        print('please wait...')
        rsp = VideoSynthesis.call(
            api_key=api_key,
            model='wan2.7-r2v-2026-06-12',
            prompt='视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 ',
            media=[
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/sjuytr/wan-r2v-object-girl.jpg",
                    "reference_voice": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/gbqewz/wan-r2v-girl-voice.mp3"
                },
                {
                    "type": "reference_video",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                    "reference_voice": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/isllrq/wan-r2v-boy-voice.mp3"
                },
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/rtjeqf/wan-r2v-object3.png"
                },
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png"
                },
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                }
            ],
            resolution='720P',
            ratio='16:9',
            duration=10,
            prompt_extend=False,
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

    ##### 响应示例 <span id="i2v27pysyncrsph" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "status_code": 200,
        "request_id": "b040d446-f9b6-977f-b9ad-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "5dab3291-393e-424d-929b-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "submit_time": "2026-04-17 17:12:49.076",
            "scheduled_time": "2026-04-17 17:13:00.384",
            "end_time": "2026-04-17 17:29:43.386",
            "orig_prompt": "视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 "
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 15,
            "input_video_duration": 5,
            "output_video_duration": 10,
            "SR": 720,
            "ratio": "16:9"
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    异步调用立即返回任务ID，需自行轮询或等待任务完成。

    ##### 请求示例 <span id="r2v751a0h5rq02" />

    ```python expandable
    import os
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call_r2v():
        # 异步调用，返回一个task_id
        rsp = VideoSynthesis.async_call(
            api_key=api_key,
            model='wan2.7-r2v-2026-06-12',
            prompt='视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 ',
            media=[
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/sjuytr/wan-r2v-object-girl.jpg",
                    "reference_voice": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/gbqewz/wan-r2v-girl-voice.mp3"
                },
                {
                    "type": "reference_video",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                    "reference_voice": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/isllrq/wan-r2v-boy-voice.mp3"
                },
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/rtjeqf/wan-r2v-object3.png"
                },
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png"
                },
                {
                    "type": "reference_image",
                    "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                }
            ],
            resolution='720P',
            ratio='16:9',
            duration=10,
            prompt_extend=False,
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
        sample_async_call_r2v()
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
        "request_id": "b040d446-f9b6-977f-b9ad-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "5dab3291-393e-424d-929b-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxx",
            "submit_time": "2026-04-17 17:12:49.076",
            "scheduled_time": "2026-04-17 17:13:00.384",
            "end_time": "2026-04-17 17:29:43.386",
            "orig_prompt": "视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 "
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 15,
            "input_video_duration": 5,
            "output_video_duration": 10,
            "SR": 720,
            "ratio": "16:9"
        }
    }
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="3f41c60916y03" />

<Tip>
  运行以下代码前，请确保 DashScope Java SDK版本 <strong>≥2.22.14</strong>。

  版本过低可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

根据模型所在地域设置 <strong>baseHttpApiUrl</strong>:

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
    同步调用会阻塞直到视频生成完成，直接返回结果。

    ##### 请求示例 <span id="r2v836a0h5jr01" />

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
    import java.util.HashMap;
    import java.util.List;
    import java.util.Map;

    public class Ref2Video {

        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void ref2video() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();
            List<VideoSynthesisParam.Media> media = new ArrayList<VideoSynthesisParam.Media>(){{
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/sjuytr/wan-r2v-object-girl.jpg")
                        .type("reference_image")
                        .referenceVoice("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/gbqewz/wan-r2v-girl-voice.mp3")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4")
                        .type("reference_video")
                        .referenceVoice("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/isllrq/wan-r2v-boy-voice.mp3")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/rtjeqf/wan-r2v-object3.png")
                        .type("reference_image")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png")
                        .type("reference_image")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png")
                        .type("reference_image")
                        .build());
            }};
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("resolution", "720P");
            parameters.put("ratio", "16:9");
            parameters.put("prompt_extend", false);
            parameters.put("watermark", true);

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.7-r2v-2026-06-12")
                            .prompt("视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 ")
                            .media(media)
                            .duration(10)
                            .parameters(parameters)
                            .build();
            System.out.println("please wait...");
            VideoSynthesisResult result = vs.call(param);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                ref2video();
            } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="i2v27javasyncrsph" />

    > video\_url 有效期24小时，请及时下载视频。

    ```json
    {
        "request_id": "f6365287-336f-9f2b-ab59-xxxxxx",
        "output": {
            "task_id": "cb7f1da5-a987-41de-b0a4-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxxx",
            "orig_prompt": "视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 ",
            "submit_time": "2026-04-17 17:15:11.536",
            "scheduled_time": "2026-04-17 17:15:20.316",
            "end_time": "2026-04-17 17:29:44.277"
        },
        "usage": {
            "video_count": 1,
            "duration": 15.0,
            "input_video_duration": 5.0,
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
    异步调用立即返回任务ID，需自行轮询或等待任务完成。

    ##### 请求示例 <span id="r2v843a0h5jr02" />

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
    import java.util.HashMap;
    import java.util.List;
    import java.util.Map;

    public class Ref2VideoAsync {

        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void asyncRef2video() throws ApiException, NoApiKeyException, InputRequiredException, InterruptedException {
            VideoSynthesis vs = new VideoSynthesis();
            List<VideoSynthesisParam.Media> media = new ArrayList<VideoSynthesisParam.Media>(){{
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/sjuytr/wan-r2v-object-girl.jpg")
                        .type("reference_image")
                        .referenceVoice("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/gbqewz/wan-r2v-girl-voice.mp3")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4")
                        .type("reference_video")
                        .referenceVoice("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260408/isllrq/wan-r2v-boy-voice.mp3")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/rtjeqf/wan-r2v-object3.png")
                        .type("reference_image")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png")
                        .type("reference_image")
                        .build());
                add(VideoSynthesisParam.Media.builder()
                        .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png")
                        .type("reference_image")
                        .build());
            }};
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("resolution", "720P");
            parameters.put("ratio", "16:9");
            parameters.put("prompt_extend", false);
            parameters.put("watermark", true);

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.7-r2v-2026-06-12")
                            .prompt("视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 ")
                            .media(media)
                            .duration(10)
                            .parameters(parameters)
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
                asyncRef2video();
            } catch (ApiException | NoApiKeyException | InputRequiredException | InterruptedException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="i2v27javaasyncrsph" />

    1、创建任务的响应示例

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
        "request_id": "f6365287-336f-9f2b-ab59-xxxxxx",
        "output": {
            "task_id": "cb7f1da5-a987-41de-b0a4-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://dashscope-a717.oss-accelerate.aliyuncs.com/xxx.mp4?Expires=xxxx",
            "orig_prompt": "视频1抱着图3，在图4的椅子上弹奏一支舒缓的乡村民谣，并说道：“今天的阳光真好。”图1手中拿着图2，路过视频1，把手中的图2放到视频1旁边的桌子上，并说道：“真好听，能不能再唱一遍”。 ",
            "submit_time": "2026-04-17 17:15:11.536",
            "scheduled_time": "2026-04-17 17:15:20.316",
            "end_time": "2026-04-17 17:29:44.277"
        },
        "usage": {
            "video_count": 1,
            "duration": 15.0,
            "input_video_duration": 5.0,
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

## 常见问题

#### 从 wan2.6 升级到 wan2.7，代码需要改哪些地方？

主要区别如下：

1. <strong>提示词的参考引用写法不同</strong>

   wan2.7 不再使用 character1、character2 这类标识，需要改为按类型分别指代：图像用“图n”（如图1、图2），视频用“视频n”（如视频1、视频2）。英文提示词则写为“Image 1”、"Video 1”这类标识。提示词说明详见[prompt参数](/zh/model-studio/wan-video-to-video-api-reference)。
2. <strong>多镜头控制方式不同</strong>

   - wan2.7：不支持 shot\_type 参数，通过在 prompt 中描述分镜脚本来实现多镜头效果。
   - wan2.6：通过设置 shot\_type 为 multi 来生成多镜头视频。
3. <strong>请求参数结构不同</strong>

   - 参考素材传入方式：wan2.7 使用 `media`（对象数组，每个元素含 `type` 和 `url`），替代 wan2.6 的 `reference_urls`（字符串数组）。
   - 分辨率参数：wan2.7 使用 `resolution`（如 `720P`），替代 wan2.6 的 `size`（如 `1280*720`）。
   - audio 参数：wan2.7 默认生成有声视频，无需设置。wan2.6 需要显式设置 `audio` 为 `true` 或 `false`。

#### 如何为主体配音（音色参考）？ <span id="a3b4c5d6e7f8g" />

仅 <strong>wan2.7</strong> 支持。在 `media` 中通过 `reference_voice` 传入音频 URL，可为参考图像或参考视频指定参考音色。

```json
{
    "media": [
        {
            "type": "reference_image",
            "url": "<参考图像URL>",
            "reference_voice": "<音频URL>"
        },
        {
            "type": "reference_video",
            "url": "<参考视频URL>",
            "reference_voice": "<音频URL>"
        }
    ]
}
```
