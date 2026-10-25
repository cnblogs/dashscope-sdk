> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 爱诗-参考生视频API参考

> 爱诗-参考生视频模型支持传入多张参考图片或视频，通过 文本提示词 描述场景，将素材中的主体角色融合生成一段流畅的视频。

<meta property="og:title" content="爱诗-参考生视频API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

1. 前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>PixVerse</strong>，找到 PixVerse 模型卡片，单击<strong>立即开通</strong>；
2. 在弹窗内确认开通及授权。

## 适用范围 <span id="pvr003a0h2scp" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/video-generate-edit-model)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP调用 <span id="pvr101a0h2http" />

由于参考生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="pvr111a0h4stp1" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

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
        #### 请求参数 <span id="pvr123a0h4reqp" />

        ##### 请求头（Headers） <span id="pvr144a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="pvr156a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。模型输出规格请参见[模型列表](/zh/model-studio/video-generate-edit-model)。

        可选值：

        - pixverse/pixverse-v6-r2v-omni
        - pixverse/pixverse-c1-r2v
        - pixverse/pixverse-v6-r2v
        - pixverse/pixverse-v5.6-r2v

        <Accordion title="模型选型" defaultOpen>
          * 需要图片+视频融合参考生成时，推荐选用 <strong>v6-r2v-omni</strong>，支持同时传入参考图片和参考视频。
          * 针对打斗、法术特效及高速运动等动态场景以及多宫格场景，推荐选用 <strong>c1</strong>。
          * 通用场景推荐使用<strong>v6</strong>，<strong>v5.6</strong>建议直接升级至v6。
        </Accordion>

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，包括参考图片和提示词。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母占一个字符，字符编码为UTF-8，超过部分会自动截断。

          - pixverse/pixverse-v6-r2v-omni：不超过5000个字符。
          - pixverse/pixverse-c1-r2v：不超过5000个字符。
          - pixverse/pixverse-v6-r2v：不超过2048个字符。
          - pixverse/pixverse-v5.6-r2v：不超过2048个字符。

          图片引用说明：当在 media 参数中传入参考图时，可以通过 `@ref_name`在prompt中直接引用该图片。

          - media 中设置的 ref\_name 在同一请求内必须全局唯一，且须与 prompt 中的引用一一对应。
          - 书写规范：必须在 `@ref_name` 后保留一个空格作为分隔。

          示例值：

          - 普通提示词：男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣。
          - 含引用的提示词：@男人 坐在靠窗的椅子上，手持@吉他 ，在@咖啡厅 旁演奏一首舒缓的美国乡村民谣。

          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体素材列表，用于指定视频生成所需的参考图像和视频。

          数组的每个元素为一个媒体对象，包含 `type`和 `url`字段。

          <Accordion title="元素属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。可选值：

            - `image_url`：图像URL。
            - `video_url`：视频URL（仅 pixverse/pixverse-v6-r2v-omni 支持）。

            素材数量限制：

            - 图像数量：pixverse/pixverse-v6-r2v-omni 最多10张，其他模型最多7张。
            - 视频数量（仅 omni）：1～2个。

            <strong>url</strong> `string` <strong>（必选）</strong>

            媒体素材URL。素材包括图像、视频（视频仅 omni 模型支持）。

            <Accordion title="传入图像（type=image_url）" defaultOpen>
              参考图像URL，必须为公网可访问的 URL。

              - 支持 HTTP 或 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.jpg">[https://xxx/xxx.jpg](https://xxx/xxx.jpg)</a>。

              图像限制：

              - 格式：JPG、PNG、WEBP。
              - 分辨率：图像的宽度和高度均不超过10000像素。
              - 文件大小：不超过20MB。
            </Accordion>

            <Accordion title="传入视频（type=video_url，仅 omni 模型）" defaultOpen>
              参考视频URL，必须为公网可访问的 URL。

              - 支持 HTTP 或 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.mp4">[https://xxx/xxx.mp4](https://xxx/xxx.mp4)</a>。

              视频限制：

              - 格式：MP4、MOV。
              - 分辨率：长边不超过1920像素。
              - 文件大小：单个视频不超过50MB。
              - 时长：两个视频总时长不超过15秒。
            </Accordion>

            <strong>ref\_name</strong> `string` （可选）

            参考图片或视频中主体的名称标识，用于在prompt中通过`@ref_name`引用。长度不超过30个字符。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` <strong>（必选）</strong>

        视频生成参数。如设置视频分辨率、时长、是否生成音频等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` <strong>（omni 模型必选）</strong>

          生成视频的分辨率档位。仅 pixverse/pixverse-v6-r2v-omni 使用此参数。

          可选值：

          - `360P`
          - `540P`
          - `720P`
          - `1080P`

          <strong>aspect\_ratio</strong> `string` <strong>（omni 模型必选）</strong>

          生成视频的宽高比。仅 pixverse/pixverse-v6-r2v-omni 使用此参数。

          可选值：

          - `auto`：自动采用第一个图片或视频的宽高比。
          - `16:9`
          - `4:3`
          - `1:1`
          - `3:4`
          - `9:16`
          - `3:2`
          - `2:3`
          - `21:9`

          <strong>size</strong> `string` <strong>（c1/v6/v5.6 模型必选）</strong>

          生成视频的分辨率，格式为`宽*高`的像素值。不支持 pixverse/pixverse-v6-r2v-omni 模型。

          不同分辨率档位对应的可选值见下方表格。

          <Accordion title="pixverse/pixverse-c1-r2v模型" defaultOpen>
            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p>size取值（宽\*高）</p></td></tr><tr><td rowSpan={8}><p>360P</p></td><td><p>16:9</p></td><td><p>640\*360</p></td></tr><tr><td><p>4:3</p></td><td><p>640\*480</p></td></tr><tr><td><p>1:1</p></td><td><p>640\*640</p></td></tr><tr><td><p>3:4</p></td><td><p>480\*640</p></td></tr><tr><td><p>9:16</p></td><td><p>360\*640</p></td></tr><tr><td><p>3:2</p></td><td><p>640\*432</p></td></tr><tr><td><p>2:3</p></td><td><p>432\*640</p></td></tr><tr><td><p>21:9</p></td><td><p>640\*288</p></td></tr><tr><td rowSpan={8}><p>540P</p></td><td><p>16:9</p></td><td><p>1024\*576</p></td></tr><tr><td><p>4:3</p></td><td><p>1024\*768</p></td></tr><tr><td><p>1:1</p></td><td><p>1024\*1024</p></td></tr><tr><td><p>3:4</p></td><td><p>768\*1024</p></td></tr><tr><td><p>9:16</p></td><td><p>576\*1024</p></td></tr><tr><td><p>3:2</p></td><td><p>1024\*688</p></td></tr><tr><td><p>2:3</p></td><td><p>688\*1024</p></td></tr><tr><td><p>21:9</p></td><td><p>1024\*448</p></td></tr><tr><td rowSpan={8}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>4:3</p></td><td><p>1108\*832</p></td></tr><tr><td><p>1:1</p></td><td><p>960\*960</p></td></tr><tr><td><p>3:4</p></td><td><p>832\*1108</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td><p>3:2</p></td><td><p>1200\*800</p></td></tr><tr><td><p>2:3</p></td><td><p>800\*1200</p></td></tr><tr><td><p>21:9</p></td><td><p>1280\*560</p></td></tr><tr><td rowSpan={8}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>4:3</p></td><td><p>1664\*1248</p></td></tr><tr><td><p>1:1</p></td><td><p>1440\*1440</p></td></tr><tr><td><p>3:4</p></td><td><p>1248\*1664</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr><tr><td><p>3:2</p></td><td><p>1776\*1184</p></td></tr><tr><td><p>2:3</p></td><td><p>1184\*1776</p></td></tr><tr><td><p>21:9</p></td><td><p>1920\*832</p></td></tr></tbody></table>
          </Accordion>

          <Accordion title="pixverse/pixverse-v6-r2v模型">
            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p>size取值（宽\*高）</p></td></tr><tr><td rowSpan={8}><p>360P</p></td><td><p>16:9</p></td><td><p>640\*360</p></td></tr><tr><td><p>4:3</p></td><td><p>640\*480</p></td></tr><tr><td><p>1:1</p></td><td><p>640\*640</p></td></tr><tr><td><p>3:4</p></td><td><p>480\*640</p></td></tr><tr><td><p>9:16</p></td><td><p>360\*640</p></td></tr><tr><td><p>3:2</p></td><td><p>640\*432</p></td></tr><tr><td><p>2:3</p></td><td><p>432\*640</p></td></tr><tr><td><p>21:9</p></td><td><p>640\*288</p></td></tr><tr><td rowSpan={8}><p>540P</p></td><td><p>16:9</p></td><td><p>1024\*576</p></td></tr><tr><td><p>4:3</p></td><td><p>1024\*768</p></td></tr><tr><td><p>1:1</p></td><td><p>1024\*1024</p></td></tr><tr><td><p>3:4</p></td><td><p>768\*1024</p></td></tr><tr><td><p>9:16</p></td><td><p>576\*1024</p></td></tr><tr><td><p>3:2</p></td><td><p>1024\*688</p></td></tr><tr><td><p>2:3</p></td><td><p>688\*1024</p></td></tr><tr><td><p>21:9</p></td><td><p>1024\*448</p></td></tr><tr><td rowSpan={8}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>4:3</p></td><td><p>1108\*832</p></td></tr><tr><td><p>1:1</p></td><td><p>960\*960</p></td></tr><tr><td><p>3:4</p></td><td><p>832\*1108</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td><p>3:2</p></td><td><p>1200\*800</p></td></tr><tr><td><p>2:3</p></td><td><p>800\*1200</p></td></tr><tr><td><p>21:9</p></td><td><p>1280\*560</p></td></tr><tr><td rowSpan={8}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>4:3</p></td><td><p>1664\*1248</p></td></tr><tr><td><p>1:1</p></td><td><p>1440\*1440</p></td></tr><tr><td><p>3:4</p></td><td><p>1248\*1664</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr><tr><td><p>3:2</p></td><td><p>1776\*1184</p></td></tr><tr><td><p>2:3</p></td><td><p>1184\*1776</p></td></tr><tr><td><p>21:9</p></td><td><p>1920\*832</p></td></tr></tbody></table>
          </Accordion>

          <Accordion title="pixverse/pixverse-v5.6-r2v模型（建议升级至v6）">
            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p>size取值（宽\*高）</p></td></tr><tr><td rowSpan={5}><p>360P</p></td><td><p>16:9</p></td><td><p>640\*360</p></td></tr><tr><td><p>4:3</p></td><td><p>640\*480</p></td></tr><tr><td><p>1:1</p></td><td><p>640\*640</p></td></tr><tr><td><p>3:4</p></td><td><p>480\*640</p></td></tr><tr><td><p>9:16</p></td><td><p>360\*640</p></td></tr><tr><td rowSpan={5}><p>540P</p></td><td><p>16:9</p></td><td><p>1024\*576</p></td></tr><tr><td><p>4:3</p></td><td><p>1024\*768</p></td></tr><tr><td><p>1:1</p></td><td><p>1024\*1024</p></td></tr><tr><td><p>3:4</p></td><td><p>768\*1024</p></td></tr><tr><td><p>9:16</p></td><td><p>576\*1024</p></td></tr><tr><td rowSpan={5}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>4:3</p></td><td><p>1108\*830</p></td></tr><tr><td><p>1:1</p></td><td><p>960\*960</p></td></tr><tr><td><p>3:4</p></td><td><p>830\*1108</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td rowSpan={5}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>4:3</p></td><td><p>1662\*1246</p></td></tr><tr><td><p>1:1</p></td><td><p>1440\*1440</p></td></tr><tr><td><p>3:4</p></td><td><p>1246\*1662</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr></tbody></table>
          </Accordion>

          示例值：1280\*720。

          <strong>duration</strong> `integer` <strong>（必选）</strong>

          <Tip>
            duration直接影响费用，请在调用前确认[爱诗-参考生视频](/zh/model-studio/model-pricing#pxr002a0h3001)。
          </Tip>

          生成视频的时长，单位为秒。

          可选值：

          - pixverse/pixverse-v6-r2v-omni：

            - 有视频参考时：必须填 `0`，系统将自动取输入视频中的最长时长。
            - 仅图片参考时：取值范围为\[1, 15]之间的整数。
          - pixverse/pixverse-c1-r2v：取值范围为\[1, 15]之间的整数。
          - pixverse/pixverse-v6-r2v：取值范围为\[1, 15]之间的整数。
          - pixverse/pixverse-v5.6-r2v：

            - 当 size 为 360P / 540P / 720P 对应的所有分辨率时：取值为5、8、10。
            - 当size为1080P对应的所有分辨率时：取值为5、8。

          示例值：5。

          <strong>audio</strong> `boolean` （可选）

          <Tip>
            audio直接影响费用，请在调用前确认[爱诗-参考生视频](/zh/model-studio/model-pricing#pxr002a0h3001)。
          </Tip>

          是否生成有声视频。开启后模型将根据视频内容自动生成匹配的背景音乐或音效。

          - `false`：默认值，输出无声视频。
          - `true`：输出有声视频。

          示例值：false。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为“AI生成”。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。

          示例值：false。

          <strong>seed</strong>`integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`。

          未指定时，系统自动生成随机种子。若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="参考生视频（图片+视频，omni）">
            通过 `media` 传入参考视频和参考图片，使用 `resolution` 和 `aspect_ratio` 控制输出分辨率和画面比例。

            ```bash expandable
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-v6-r2v-omni",
                "input": {
                    "media": [
                        {
                            "type": "video_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4",
                            "ref_name": "参考视频"
                        },
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260320/knsple/wan-r2v-role-frame.jpg",
                            "ref_name": "男人"
                        },
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png",
                            "ref_name": "咖啡厅"
                        }
                    ],
                    "prompt": "@男人 在@咖啡厅 中弹吉他 参考@参考视频"
                },
                "parameters": {
                    "resolution": "720P",
                    "aspect_ratio": "16:9",
                    "duration": 0,
                    "audio": true,
                    "watermark": false
                }
            }'
            ```
          </Tab>

          <Tab title="Python SDK">
            ```python expandable
            from http import HTTPStatus
            from dashscope import VideoSynthesis
            import dashscope
            import os

            # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
            # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
            api_key = os.getenv("DASHSCOPE_API_KEY")

            media = [{'type': 'video_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4', 'ref_name': '参考视频'}, {'type': 'image_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260320/knsple/wan-r2v-role-frame.jpg', 'ref_name': '男人'}, {'type': 'image_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png', 'ref_name': '咖啡厅'}]

            def sample_async_call():
                # 异步提交视频生成任务
                rsp = VideoSynthesis.async_call(api_key=api_key,
                                            model='pixverse/pixverse-v6-r2v-omni',
                                            media=media,
                                            prompt='@男人 在@咖啡厅 中弹吉他 参考@参考视频',
                                            resolution='720P',
                                            aspect_ratio='16:9',
                                            duration=0,
                                            audio=True,
                                            watermark=False)
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
          </Tab>

          <Tab title="参考生视频">
            通过 `media` 传入参考图像，设置 `size` 和 `duration` 控制视频分辨率和时长。

            ```bash expandable
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-c1-r2v",
                "input": {
                    "media": [
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260320/knsple/wan-r2v-role-frame.jpg"
                        },
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png"
                        },
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                        }
                    ],
                    "prompt": "男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣"
                },
                "parameters": {
                    "size": "1280*720",
                    "duration": 5,
                    "audio": false,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="参考生视频（使用ref_name）">
            在 `media` 传入参考图像，并传入`ref_name`用于设置参考图像的主体名称。

            ```bash expandable
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-c1-r2v",
                "input": {
                    "media": [
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260320/knsple/wan-r2v-role-frame.jpg",
                            "ref_name": "男人"
                        },
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png",
                            "ref_name": "吉他"
                        },
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png",
                            "ref_name": "咖啡厅"
                        }
                    ],
                    "prompt": "@男人 坐在靠窗的椅子上，手持@吉他 ，在@咖啡厅 旁演奏一首舒缓的美国乡村民谣"
                },
                "parameters": {
                    "size": "1280*720",
                    "duration": 5,
                    "audio": false,
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
        #### 响应参数 <span id="pvr379a0h4rsp1" />

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

### 步骤2：根据任务ID查询结果 <span id="pvr411a0h4stp2" />

<strong>北京地域</strong>：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

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
        #### 请求参数 <span id="pvr424a0h4qr01" />

        ##### 请求头（Headers） <span id="pvr434a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="pvr440a0h5qr02" />

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
        #### 响应参数 <span id="pvr448a0h4qrs1" />

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

          视频格式为MP4（H.264 编码）。视频链接暂无过期时间，但不建议将其作为长期存储依赖，请及时下载。

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

          <strong>size</strong> `string`

          生成视频的分辨率。

          <strong>fps</strong> `integer`

          生成视频的帧率。

          <strong>SR</strong> `string`

          生成视频的分辨率档位。

          <strong>audio</strong> `boolean`

          生成视频是否为有声视频。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为1。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "35137489-2862-96cb-b6f2-xxxxxx",
                "output": {
                    "task_id": "1469cfc3-3004-4d9e-ab10-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-10 15:03:25.848",
                    "scheduled_time": "2026-03-10 15:03:25.884",
                    "end_time": "2026-03-10 15:04:05.882",
                    "orig_prompt": "男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣",
                    "video_url": "https://media.pixverseai.cn/xxxx.mp4"
                },
                "usage": {
                    "duration": 5,
                    "size": "1280*720",
                    "fps": 24,
                    "video_count": 1,
                    "audio": false,
                    "SR": "720"
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

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="pvr801a0h2faq1" />

#### Q: prompt 效果不理想时，如何优化？ <span id="pvr806a0h4fq02" />

A： 可尝试使用 `@ref_name` 作为替代方案。

- 默认做法：直接使用 prompt，模型会自动全局解析参考图内容，适用于大多数常规场景。
- 替代方案：若发现生成效果偏离预期，可通过 `@ref_name`将参考图指定在提示词中，增强生成效果。

#### Q：如何使用ref\_name？ <span id="1736f3617ahla" />

A：`ref_name` 用于建立参考图与提示词的绑定关系。在 `media`数组中为图片设置 `ref_name` 后，prompt 中可通过 `@ref_name`语法引用对应的参考图主体。

- 例如：media 中设置 `"ref_name": "小猫"`，则 prompt 中使用`@小猫`来引用该主体。注意：@小猫后必须保留空格。
- 示例：@小猫 和@小狗 在草地上玩耍。
