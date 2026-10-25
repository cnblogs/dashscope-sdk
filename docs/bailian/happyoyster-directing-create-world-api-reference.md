> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-创建World API参考

> 创建一个实时导演 World。支持普通模式（自然语言 Prompt）和剧本模式（结构化 ScriptList），接口立即返回加密 World ID，World 在后台异步构建，客户端轮询构建进度直至完成。

<meta property="og:title" content="HappyOyster-Directing-创建World API参考" />

## 适用范围 <span id="ho-dir-cw-scope-title" />

创建一个 Directing World。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>调用模式</strong>：推荐使用异步模式。

  - <strong>异步模式</strong>（默认）：`async=true`，接口立即返回 `encryptedWorldId`，需轮询[查询World构建状态](/zh/model-studio/happyoyster-directing-query-world-build-status-api-reference)获取进度。
  - <strong>同步模式</strong>：`async=false`，服务端内部轮询（间隔 3s，最长 120s），构建完成后返回；超时则降级为异步，客户端继续轮询。
- <strong>接口限制</strong>：本接口只能创建 Directing World，无需传 `mode`（服务端按 `2` 写入，传入非 `2` 返回 `400000`）。`creationModel` 支持 `simple`（普通模式，默认）和 `scriptlist`（剧本模式），进房版本固定为 `storyV2`，`aspectRatio` 和 `maxExperienceTimeSec` 固定为 `null`。

## HTTP调用 <span id="ho-dir-cw-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

### 普通模式（`creationModel=simple`） <span id="ho-dir-cw-simple-title" />

<table bordertype="no-border">
  <colgroup>
    <col style={{ width: "53.91%" }} />

    <col style={{ width: "46.09%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="ho-dir-cw-simple-req-title" />
      </td>

      <td rowSpan={14}>
        <Tabs>
          <Tab title="普通模式·纯文本（异步创建）">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "async": true,
                "creationModel": "simple",
                "eventStyle": "normal",
                "prompt": "第一视角（POV）互动视频，镜头模拟我的双眼，锁定正对面的白色马尔济斯小狗：白色厨师帽、黑色扣子厨师服。柔和居家厨房背景，光影温暖，小狗眼神始终看向镜头。",
                "resolution": "720p",
                "layout": "Stable",
                "narrative": "Calm"
            }'
            ```
          </Tab>

          <Tab title="普通模式·文本+参考图（异步创建）">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "async": true,
                "creationModel": "simple",
                "eventStyle": "normal",
                "prompt": "窗边咖啡馆。黑长发年轻女子穿米色高领毛衣侧坐，左手托腮望向布满雨珠的玻璃窗。木桌上有一杯冒热气的深色咖啡。窗外雨夜暖黄街灯，室内一盏吊灯。安静雨夜。",
                "resolution": "720p",
                "inputImages": [
                    {
                        "url": "https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/common/images/6a4b3c2d1e0f9fc7.png",
                        "referenceType": "default"
                    }
                ]
            }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型，固定为 `application/json`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key 鉴权。仅支持<strong>主 API Key</strong>，以 `sk-` 开头，如 `sk-xxx`。通常配置为环境变量 `$DASHSCOPE_API_KEY`。临时 API Key（`st-` 开头）调用返回 `403003`。
      </td>
    </tr>

    <tr>
      <td>
        ##### 请求体（Request Body） <span id="ho-dir-cw-simple-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>async</strong> `boolean` <strong>（可选）</strong>

        是否异步创建。默认 `true`：

        - `true`：立即返回，World 在后台构建，客户端轮询[查询World构建状态](/zh/model-studio/happyoyster-directing-query-world-build-status-api-reference)。
        - `false`：服务端每 3 秒轮询一次，最长等待 120 秒，构建完成后返回；超时仍返回 `generating`，客户端随后改为自行轮询。
      </td>
    </tr>

    <tr>
      <td>
        <strong>creationModel</strong> `string` <strong>（可选）</strong>

        创建子模式，普通模式传 `simple`（默认）。由您提供自然语言 `prompt`，服务端生成完整 45 拍剧本、首帧（可自带）和角色参考图。World 创建后，可在 Travel 阶段调用以下控制接口：

        - `instruct`：发送文本过程指令
        - `pause`：暂停
        - `resume`：恢复
        - `rewind`：回溯
        - `end`：结束

        各接口说明见[补充说明](#ho-dir-cw-notes-title)。
      </td>
    </tr>

    <tr>
      <td>
        <strong>eventStyle</strong> `string` <strong>（可选）</strong>

        仅 `creationModel=simple` 生效：选择剧本生成模板。默认 `normal`。可选值：

        - `normal`：常规 / 标准风格（默认）。按用户意图补全 4–5 幕，节奏相对平稳，不强行加入冲突、反转或三幕高潮。
        - `dramatic`：戏剧 / 冲突风格。按约 180 秒三幕骨架生成剧本：开场钩子、上升冲突、转折、高潮收束；开放演绎时按类型安排反转（悬疑 / 惊悚 / 逆袭等），节奏更密、戏剧性更强。
        - `regular`：旧值，仅为兼容历史入参保留，服务端按 `normal` 处理；新调用请勿使用。
      </td>
    </tr>

    <tr>
      <td>
        <strong>refWorldId</strong> `string` <strong>（可选）</strong>

        基于已有 Directing World 衍生创建。必须是当前主账号名下的 Directing 加密 World ID；其它模型或其它主账号的 World 返回 `403001`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>prompt</strong> `string` <strong>（必选）</strong>

        世界主题描述，支持中英文。非空，最长 2000 字符。
      </td>
    </tr>

    <tr>
      <td>
        <strong>resolution</strong> `string` <strong>（必选）</strong>

        视频分辨率。可选值：

        - `480p`
        - `720p`
      </td>
    </tr>

    <tr>
      <td>
        <strong>layout</strong> `string` <strong>（可选）</strong>

        镜头运动风格（镜头怎么动、切得多猛）。可选值：

        - `Stable`：镜头稳、运动少，偏连续长镜头，切镜稀疏
        - `Fast`：镜头快、动能强，硬切 / 快速变焦 / 景别跳变更密
        - `Calm`：介于两者之间，镜头克制，不急不猛
      </td>
    </tr>

    <tr>
      <td>
        <strong>narrative</strong> `string` <strong>（可选）</strong>

        叙事风格（戏密不密、情绪强不强）。可选值：

        - `Calm`：事件少，偏氛围、慢推进
        - `Dramatic`：事件密，反应 / 反转 / 障碍更明显，张力高
        - `Normal`：常规叙事密度，中间档
        - `Steady`：节奏匀、不堆高潮，和 `Normal` 接近
      </td>
    </tr>

    <tr>
      <td>
        <strong>firstFrameImage</strong> `object` <strong>（可选）</strong>

        提供后直接复用为 World 首帧，跳过 AI 首帧生成。`url` 与 `base64` 二选一且互斥。图片约束如下：

        - 格式：JPG / JPEG / PNG / WebP
        - 大小：单张严格小于 6 MB
        - 宽高比：必须为横屏，宽 / 高为 1.5–2.0（画面比例跟随该图）
        - 内容安全：未通过内容安全或版权 / IP 校验返回 `403004` / `403005`

        <Accordion title="属性" defaultOpen>
          <strong>url</strong> `string` <strong>（条件必选）</strong>

          首帧图片 URL。约束：

          - 必须是带 Host 的合法 `http` / `https` URL，并可由服务端访问
          - 真实格式、大小和首帧宽高比在转存后校验
          - 异步请求可能先返回 `generating`，随后 World 因图片校验失败进入 `failed`

          <strong>base64</strong> `string` <strong>（条件必选）</strong>

          首帧图片 base64。约束：

          - 推荐使用完整 data URI `data:image/<subtype>;base64,<payload>`
          - 在创建入口同步校验格式、大小和首帧宽高比

          <strong>referenceType</strong> `string` <strong>（可选）</strong>

          参考图类型，默认 `default`。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>inputImages</strong> `array` <strong>（可选）</strong>

        用于剧本生成和角色参考图，最多 6 张，与 `firstFrameImage` 相互独立。数组每项的 `url` 与 `base64` 二选一且互斥。图片约束如下：

        - 格式：JPG / JPEG / PNG / WebP
        - 大小：单张严格小于 6 MB
        - 宽高比：必须为横屏，宽 / 高为 1.5–2.0（画面比例跟随该图）
        - 内容安全：未通过内容安全或版权 / IP 校验返回 `403004` / `403005`

        <Accordion title="属性" defaultOpen>
          <strong>url</strong> `string` <strong>（条件必选）</strong>

          图片 URL。约束：

          - 必须是带 Host 的合法 `http` / `https` URL，并可由服务端访问
          - 真实格式、大小和宽高比在转存后校验
          - 异步请求可能先返回 `generating`，随后 World 因图片校验失败进入 `failed`

          <strong>base64</strong> `string` <strong>（条件必选）</strong>

          图片 base64。约束：

          - 推荐使用完整 data URI `data:image/<subtype>;base64,<payload>`
          - 在创建入口同步校验格式、大小和宽高比

          <strong>referenceType</strong> `string` <strong>（可选）</strong>

          参考图类型，默认 `default`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

### 剧本模式（`creationModel=scriptlist`） <span id="ho-dir-cw-scriptlist-title" />

<table bordertype="no-border">
  <colgroup>
    <col style={{ width: "53.91%" }} />

    <col style={{ width: "46.09%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="ho-dir-cw-scriptlist-req-title" />
      </td>

      <td rowSpan={10}>
        <Tabs>
          <Tab title="剧本模式（异步创建）">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "async": true,
                "creationModel": "scriptlist",
                "resolution": "720p",
                "firstFrameImage": {
                    "url": "https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/common/images/6a4b3c2d1e0f9fc7.png",
                    "referenceType": "default"
                },
                "scriptList": {
                    "videoTitle": "苏念·今晚陪你坐一会儿",
                    "synopsis": "少女苏念坐在浅色书桌前正对镜头，陪你慢慢聊天、把今天放下。",
                    "subjects": [
                        {
                            "label": "[character_1]",
                            "name": "苏念",
                            "type": "character",
                            "gender": "female",
                            "age": "少女",
                            "ethnicity": "东亚",
                            "appearance": "黑色中长直发偏分，圆润脸型，米白色针织上衣，二次元动漫风，干净线条",
                            "position": "画面中央，坐在浅色书桌前",
                            "voice": "温柔少女声，语速偏慢，音量适中"
                        }
                    ],
                    "acts": [
                        {
                            "turn": 1,
                            "content": "[character_1] 坐在浅色书桌前正对镜头，身体微微前倾，眼睛弯起轻声说「来啦，今晚也陪你坐一会儿，慢慢聊」",
                            "cameraType": "Push-in",
                            "shotSize": "Medium",
                            "cut": "long-take"
                        },
                        {
                            "turn": 2,
                            "content": "[character_1] 双手交叠放在桌上，轻轻点头说「先把今天那些烦心的事，都暂时放到一边吧」",
                            "cameraType": "Static",
                            "shotSize": "Medium",
                            "cut": "long-take"
                        },
                        {
                            "turn": 3,
                            "content": "[character_1] 微微歪头，表情温和地看着镜头，问「这一天过下来，你还好吗，累不累」",
                            "cameraType": "Static",
                            "shotSize": "Medium",
                            "cut": "long-take"
                        }
                    ]
                }
            }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型，固定为 `application/json`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key 鉴权。仅支持<strong>主 API Key</strong>，以 `sk-` 开头，如 `sk-xxx`。通常配置为环境变量 `$DASHSCOPE_API_KEY`。临时 API Key（`st-` 开头）调用返回 `403003`。
      </td>
    </tr>

    <tr>
      <td>
        ##### 请求体（Request Body） <span id="ho-dir-cw-scriptlist-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>async</strong> `boolean` <strong>（可选）</strong>

        是否异步创建。默认 `true`：

        - `true`：立即返回，World 在后台构建，客户端轮询[查询World构建状态](/zh/model-studio/happyoyster-directing-query-world-build-status-api-reference)。
        - `false`：服务端每 3 秒轮询一次，最长等待 120 秒，构建完成后返回；超时仍返回 `generating`，客户端随后改为自行轮询。
      </td>
    </tr>

    <tr>
      <td>
        <strong>creationModel</strong> `string` <strong>（可选）</strong>

        创建子模式，剧本模式传 `scriptlist`。结构化剧本由您提供，服务端不再生成剧本，仅做组装并落库。World 创建后，可在 Travel 阶段调用以下控制接口：

        - `update-script`：全量替换剧本
        - `pause`：暂停
        - `resume`：恢复
        - `rewind`：回溯
        - `end`：结束

        不支持 `instruct`（发送文本过程指令）。各接口说明见[补充说明](#ho-dir-cw-notes-title)。

        <Note>`eventStyle` 不参与 scriptlist 创建，请勿传入。</Note>
      </td>
    </tr>

    <tr>
      <td>
        <strong>refWorldId</strong> `string` <strong>（可选）</strong>

        基于已有 Directing World 衍生创建。必须是当前主账号名下的 Directing 加密 World ID；其它模型或其它主账号的 World 返回 `403001`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>resolution</strong> `string` <strong>（必选）</strong>

        视频分辨率。可选值：

        - `480p`
        - `720p`
      </td>
    </tr>

    <tr>
      <td>
        <strong>firstFrameImage</strong> `object` <strong>（必选）</strong>

        直接复用为 World 首帧的图片引用。`url` 与 `base64` 二选一且互斥。图片约束如下：

        - 格式：JPG / JPEG / PNG / WebP
        - 大小：单张严格小于 6 MB
        - 宽高比：必须为横屏，宽 / 高为 1.5–2.0（画面比例跟随该图）
        - 内容安全：未通过内容安全或版权 / IP 校验返回 `403004` / `403005`

        <Accordion title="属性" defaultOpen>
          <strong>url</strong> `string` <strong>（条件必选）</strong>

          首帧图片 URL。约束：

          - 必须是带 Host 的合法 `http` / `https` URL，并可由服务端访问
          - 真实格式、大小和首帧宽高比在转存后校验
          - 异步请求可能先返回 `generating`，随后 World 因图片校验失败进入 `failed`

          <strong>base64</strong> `string` <strong>（条件必选）</strong>

          首帧图片 base64。约束：

          - 推荐使用完整 data URI `data:image/<subtype>;base64,<payload>`
          - 在创建入口同步校验格式、大小和首帧宽高比

          <strong>referenceType</strong> `string` <strong>（可选）</strong>

          参考图类型，默认 `default`。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>scriptList</strong> `object` <strong>（必选）</strong>

        结构化剧本，必须包含 `synopsis` 与非空 `acts`。

        <Accordion title="属性" defaultOpen>
          <strong>synopsis</strong> `string` <strong>（必选）</strong>

          故事梗概。非空，最长 2000 字。

          <strong>videoTitle</strong> `string` <strong>（可选）</strong>

          世界名称。默认 `New World`，最长 128 字。

          <strong>scene</strong> `string` <strong>（可选）</strong>

          场景设定。默认 `Static Shot`，最长 64 字。

          <strong>style</strong> `string` <strong>（可选）</strong>

          视觉风格。默认 `Stable`，最长 64 字。

          <strong>speed</strong> `string` <strong>（可选）</strong>

          叙事节奏。默认 `Steady`，最长 64 字。

          <strong>language</strong> `string` <strong>（可选）</strong>

          剧本语言。默认 `en`（英文），中文传 `zh`。最长 64 字。

          <strong>setting</strong> `string` <strong>（可选）</strong>

          世界观或背景设定。最长 2000 字。

          <strong>soundtrack</strong> `string` <strong>（可选）</strong>

          配乐描述。最长 500 字。

          <strong>prologue</strong> `string` <strong>（可选）</strong>

          开场白。最长 1000 字。

          <strong>videoTags</strong> `array<string>` <strong>（可选）</strong>

          视频标签。最多 20 个，单个标签最长 32 字。

          <strong>subjects</strong> `array<object>` <strong>（可选）</strong>

          预定义主体，最多 6 个。数组每项含以下属性：

          <Accordion title="subjects[] 属性">
            <strong>label</strong> `string` <strong>（可选）</strong>

            在 `acts[].content` 中引用主体。格式为 `[character_x]`，默认按数组顺序分配。

            <strong>name</strong> `string` <strong>（可选）</strong>

            人类可读名称，不渲染为画面文字。最长 64 字。

            <strong>type</strong> `string` <strong>（可选）</strong>

            主体类型，默认 `character`。决定该主体的形象与其它属性如何填写。可选值：

            - `character`：人类角色（默认）。按人填写 `gender` / `position` / `ethnicity` / `age` / `appearance`。
            - `animal`：真实动物（猫、狗、马等）。用物种代替性别、亚种 / 品种代替族裔。
            - `creature`：非人 / 幻想生物（龙、妖怪、外星人等）。同样按非人形象填写。
            - `narrator`：旁白 / 画外音。只有声音、不出画面，填写 `voice`，不产出参考图。

            <strong>refImage</strong> `object` <strong>（可选）</strong>

            主体参考图，`url` 与 `base64` 二选一且互斥，图片严格小于 6 MB。

            <Accordion title="refImage 属性">
              <strong>url</strong> `string` <strong>（条件必填）</strong>

              主体参考图 URL。

              <strong>base64</strong> `string` <strong>（条件必填）</strong>

              主体参考图 base64。

              <strong>referenceType</strong> `string` <strong>（可选）</strong>

              参考图类型，默认 `default`。
            </Accordion>

            <strong>gender</strong> `string` <strong>（可选）</strong>

            性别描述。最长 64 字。

            <strong>position</strong> `string` <strong>（可选）</strong>

            画面位置。最长 64 字。

            <strong>ethnicity</strong> `string` <strong>（可选）</strong>

            族裔或人种描述。最长 64 字。

            <strong>age</strong> `string` <strong>（可选）</strong>

            年龄描述。最长 64 字。

            <strong>appearance</strong> `string` <strong>（可选）</strong>

            外观细节。最长 500 字。

            <strong>voice</strong> `string` <strong>（可选）</strong>

            音色、语速和音量描述。最长 200 字。
          </Accordion>

          <strong>acts</strong> `array<object>` <strong>（必选）</strong>

          逐拍剧本，1–45 条，全部 `content` 合计不超过 100000 字。数组每项含以下属性：

          <Accordion title="acts[] 属性">
            <strong>turn</strong> `int` <strong>（可选）</strong>

            turn 序号。1–45，不可重复，默认按数组顺序从 1 递增。

            <strong>content</strong> `string` <strong>（必选）</strong>

            本拍剧本。非空，单拍最长 2000 字，可用 `[character_x]` 引用主体。

            <strong>cameraType</strong> `string` <strong>（可选）</strong>

            镜头类型（镜头怎么拍）。默认 `Static`。可选值：

            - `Static`：机位锁死，不推不摇（默认值）。景别靠 `shotSize` 控制：远 / 中 / 近
            - `Tracking`：跟拍。机位跟着主体走，保持跟拍距离
            - `Pan Left`：机位不动，镜头向左横摇
            - `Pan Right`：机位不动，镜头向右横摇
            - `Tilt Up`：镜头上仰
            - `Tilt Down`：镜头下俯
            - `Push-in`：光学 / 物理推进，画面逐渐靠近
            - `Pull-out`：拉远，画面逐渐变宽
            - `POV Forward`：第一人称，镜头即眼睛，向前走
            - `POV Look Down`：第一人称低头看
            - `POV Look Up`：第一人称抬头看
            - `POV Turn Left`：第一人称左转看
            - `POV Turn Right`：第一人称右转看

            <strong>shotSize</strong> `string` <strong>（可选）</strong>

            景别，默认 `Medium`。决定这一拍能写多细，换景别应靠切镜，不要在同一拍里既写全身又写指尖。可选值：

            - `Wide`：远景 / 全景，含全身与环境关系。适合走位、站位、空间调度，不写微表情、指尖。
            - `Medium`：中景（默认），上半身姿态与手势。适合对话、日常动作，不写脚部走位、精细手部操作。
            - `Close-up`：特写，一张脸 / 一只手 / 一件道具。适合表情、关键细节，不写全身移动。

            与切镜搭配：要看更细，先 `cut-in` 到 `Close-up`，再 `cut-out` 回宽景；多数拍用 `long-take` 稳住同一景别，避免景别来回跳。

            <strong>cut</strong> `string` <strong>（可选）</strong>

            切镜方式（这一拍怎么切进来）。默认 `long-take`。可选值：

            - `long-take`：不切，上一镜接着拍。默认，也最稳
            - `hard-cut`：硬切，瞬间换成另一镜。对话正反打用这个
            - `cut-in`：切到更近的景别，例如 `Medium` → `Close-up`。看细节、接手部互动用
            - `cut-out`：切到更远的景别，例如 `Close-up` → `Medium` / `Wide`
            - `cutaway`：短暂切到主线之外的细节
            - `cutback`：从 `cutaway` 切回主主体。只能跟在 `cutaway` 后面
            - `camera movement transition`：用快速运镜把两段连起来，不是硬切
          </Accordion>
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border">
  <colgroup>
    <col style={{ width: "53.5%" }} />

    <col style={{ width: "46.5%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="ho-dir-cw-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="异步创建">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedWorldId": "enc_a1b2****",
                    "status": "generating",
                    "firstFrame": null
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong> `integer`

        返回码。`0` 表示成功，非 0 为错误码。
      </td>
    </tr>

    <tr>
      <td>
        <strong>message</strong> `string`

        错误信息。成功时为 `null`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>data</strong> `object`

        响应数据。失败时为 `null`。

        <Accordion title="属性" defaultOpen>
          <strong>encryptedWorldId</strong> `string`

          加密 World ID，同步 / 异步模式均返回。后续查询构建状态、World 详情和换取体验凭证均使用此值。

          <strong>status</strong> `string`

          当前创建状态：

          - `generating`：构建中
          - `ready`：就绪
          - `failed`：构建失败

          <strong>firstFrame</strong> `string`

          World 首帧 URL；尚未生成时为 `null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 补充说明 <span id="ho-dir-cw-notes-title" />

- <strong>字数计算</strong>：文档中的「最长 N 字」按字符数（Unicode 字符）统计，不区分中英文——中文汉字、英文字母、数字、空格和标点符号均各算 1 个字符。
- <strong>ScriptList 提交要求</strong>：创建 World 时 `acts` 最多 45 条，不要求恰好 45 条；Travel 中调用 [update-script](/zh/model-studio/happyoyster-directing-update-script-api-reference) 时才要求完整提交 45 条。
- <strong>Travel 控制接口</strong>：`creationModel` 中列出的接口名是 World 创建后、在 Travel 阶段可调用的服务端接口，不是本接口的入参枚举值。含义如下：

  - `instruct`：向运行中的 Travel 发送[文本过程指令](/zh/model-studio/happyoyster-directing-instruct-travel-api-reference)，仅普通模式。
  - `pause`：[暂停](/zh/model-studio/happyoyster-directing-pause-travel-api-reference) Travel。
  - `resume`：[恢复](/zh/model-studio/happyoyster-directing-resume-travel-api-reference)播放。
  - `rewind`：[回溯](/zh/model-studio/happyoyster-directing-rewind-travel-api-reference)到指定时间点，需先暂停。
  - `end`：[结束](/zh/model-studio/happyoyster-directing-end-travel-api-reference) Travel。
  - `update-script`：[全量替换剧本](/zh/model-studio/happyoyster-directing-update-script-api-reference)，仅剧本模式。

## 错误码 <span id="ho-dir-cw-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-cw-next-title" />

创建成功后可进行以下操作：

- [查询World构建状态](/zh/model-studio/happyoyster-directing-query-world-build-status-api-reference)：每 3–5 秒轮询，直到 World 进入 `ready`。
- World 进入 `ready` 后，调用[获取体验凭证](/zh/model-studio/happyoyster-directing-get-travel-credential-api-reference)换取一次性 `ticket`。
- [查询World详情](/zh/model-studio/happyoyster-directing-query-world-detail-api-reference)：查询完整创建元数据和 ScriptList。
