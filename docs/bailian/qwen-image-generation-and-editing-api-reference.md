> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 千问-图像生成与编辑 API参考

> 千问-图像生成与编辑模型同时支持文生图（T2I）和图生图/图像编辑（I2I），可根据文本提示词直接生成图像，也可基于参考图结合编辑指令进行精确编辑。支持通过OpenAI 兼容或DashScope协议调用。

<meta property="og:title" content="千问-图像生成与编辑 API参考" />

## 模型概览 <span id="6740266-model-title" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24%" }} /><col style={{ width: "38%" }} /><col style={{ width: "38%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th><th><p><strong>输出图像规格</strong></p></th></tr></thead><tbody><tr><td><p>qwen-image-3.0-pro</p></td><td><p>千问图像生成与编辑3.0 Pro系列，同时支持文生图（T2I）和图生图/图像编辑（I2I）。</p></td><td rowSpan={4}><p>图像分辨率：</p> <ul><li><p><strong>文生图（T2I）</strong>：总像素需在512\*512至2048\*2048之间。</p></li> <li><p><strong>图生图（I2I）</strong>：总像素需在512\*512至2048\*2048之间。</p></li> <li><p><strong>默认</strong>：不指定<code>size</code>时，模型根据提示词自动推荐分辨率。</p></li></ul> <p>图像格式：png</p></td></tr><tr><td><p>qwen-image-3.0</p></td><td><p>千问图像生成与编辑3.0标准模型，同时支持文生图（T2I）和图生图/图像编辑（I2I），兼顾质量与速度。</p></td></tr><tr><td><p>qwen-image-2.1-pro</p></td><td><p>千问图像生成与编辑2.1 Pro模型，图生图/图像编辑（I2I）支持传入最多10张参考图，且能够根据提示词描述自动决定输出普通图像还是带有透明通道的图像。</p></td></tr><tr><td><p>qwen-image-2.1-turbo</p></td><td><p>千问图像生成与编辑2.1 Turbo模型，是目前Qwen-Image系列中综合表现与性价比最优的模型，图生图/图像编辑（I2I）支持传入最多10张参考图，且能够根据提示词描述自动决定输出普通图像还是带有透明通道的图像。</p></td></tr></tbody></table>

## 适用范围 <span id="hh01scope-title" />

为确保调用成功，请务必保证模型、endpoint URL 和 API Key 均属于<strong>同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/video-generate-edit-model)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：获取该地域的[API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- <strong>安装 SDK</strong>：如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。

<Note>
  本文的示例代码适用于<strong>华北2（北京）地域</strong>。
</Note>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## 接入方式 <span id="6740266-access-title" />

千问-图像生成与编辑提供三种接入方式，模型能力一致，请根据业务情况选择：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "72%" }} /></colgroup><thead><tr><th><p><strong>接入方式</strong></p></th><th><p><strong>适用场景</strong></p></th></tr></thead><tbody><tr><td><p><a href="/zh/model-studio/qwen-image-generation-and-editing-api-reference#6740266-openai-title">OpenAI 兼容</a></p></td><td><p>已基于 OpenAI Images 协议或 OpenAI SDK 开发的应用，只需切换<code>base\_url</code>和<code>model</code>即可接入。仅支持同步调用，图像输入支持公网 URL 和 Base64。</p></td></tr><tr><td><p><a href="/zh/model-studio/qwen-image-generation-and-editing-api-reference#6740266-sync-title">DashScope同步调用</a></p></td><td><p>推荐方式，功能最完整，支持公网 URL 和 Base64 两种图像输入。</p></td></tr><tr><td><p><a href="/zh/model-studio/qwen-image-generation-and-editing-api-reference#6740266-async-title">DashScope异步调用</a></p></td><td><p>批量生成或不希望长时间占用连接的场景。提交任务后通过<code>task\_id</code>轮询结果。</p></td></tr></tbody></table>

## OpenAI 兼容 <span id="6740266-openai-title" />

如果您的应用已基于 OpenAI Images 协议或 OpenAI SDK 开发，可通过 OpenAI 兼容模式调用千问-图像生成与编辑，无需改造请求结构。文生图（T2I）与图生图/图像编辑（I2I）共用同一个接口：<strong>不传</strong>`image`<strong>为文生图，传</strong>`image`<strong>为图生图</strong>。

<Note>
  OpenAI 兼容模式为<strong>同步、非流式</strong>接口，图生图通过`/images/generations`接口的扩展字段`image`实现，<strong>不使用</strong>OpenAI 官方`/images/edits`的 multipart 文件上传形式。以下能力暂不支持：异步调用、流式输出、partial image、`/images/edits`、multipart 与 mask。传入`response_format=b64_json`不会报错，但会被忽略，响应中仍返回图像URL。如需异步调用，请使用[DashScope异步调用](/zh/model-studio/qwen-image-generation-and-editing-api-reference#6740266-async-title)。
</Note>

### URL <span id="6740266-openai-url-title" />

<Tabs>
  <Tab title="华北2（北京）">
    HTTP请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/images/generations`

    SDK调用配置的base\_url：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`
  </Tab>

  <Tab title="新加坡">
    HTTP请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/images/generations`

    SDK调用配置的base\_url：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    HTTP请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/images/generations`

    SDK调用配置的base\_url：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1`
  </Tab>

  <Tab title="德国（法兰克福）">
    HTTP请求地址：`POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1/images/generations`

    SDK调用配置的base\_url：`https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1`
  </Tab>

  <Tab title="日本（东京）">
    HTTP请求地址：`POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1/images/generations`

    SDK调用配置的base\_url：`https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求参数 <span id="6740266-openai-req-params-title" />

        #### 请求头（Headers） <span id="6740266-openai-headers-h4" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        #### 请求体（Request Body） <span id="6740266-openai-body-h4" />

        与DashScope协议不同，OpenAI 兼容模式的所有参数<strong>平铺在请求体顶层</strong>，没有`input`和`parameters`嵌套结构。

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，可选值为`qwen-image-3.0-pro`、`qwen-image-3.0`、`qwen-image-2.1-pro`和`qwen-image-2.1-turbo`。

        <strong>prompt</strong> `string` <strong>（必选）</strong>

        正向提示词，用于描述您期望生成或编辑的图像内容、风格和构图。支持中英文，推荐不超过4500Token。不能为空字符串。

        <strong>image</strong> `string`<strong> 或 </strong>`array` （可选）

        输入图像的 URL 或 Base64 编码数据。不传此参数为文生图（T2I）；传入此参数为图生图（I2I），qwen-image-2.1系列支持传入1-10张图像，qwen-image-3.0系列支持传入1-3张图像。单张图像可直接传字符串，多张图像传字符串数组，按数组顺序定义图像顺序。

        <strong>注意</strong>：不能传入`null`或空数组，否则将返回400错误。

        <strong>图像要求：</strong>

        - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP和GIF。
        - 图像分辨率：宽和高须在1至8000像素之间，超过将返回400错误；建议在384至2048像素之间以获得最佳效果。
        - 图像宽高比：长边与短边之比不超过10:1。
        - 图像大小：不超过10MB。

        <strong>支持的输入格式</strong>

        1. 公网URL：支持 HTTP 和 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。
        2. Base64 编码：格式为`data:{MIME_type};base64,{base64_data}`。

        <strong>n</strong> `integer` （可选）

        输出图像的数量，支持输出1-6张图片，默认值为1。必须传入整数，传入字符串形式（如`"1"`）将返回400错误。

        <strong>size</strong> `string` （可选）

        设置输出图像的分辨率，格式为`宽x高`，例如`"1024x1024"`，也可传入`auto`。未指定时由模型根据提示词自动推荐分辨率。

        <Warning>
          OpenAI 协议使用<strong>字母</strong>`x`<strong>作为分隔符</strong>（`1024x1024`），与DashScope协议的星号`*`（`1024*1024`）不同，从DashScope迁移时请注意修改。
        </Warning>

        - <strong>文生图（T2I）</strong>：像素面积范围512x512至2048x2048，宽高比限制1:8至8:1。
        - <strong>图生图（I2I）</strong>：像素面积范围512x512至2048x2048，宽高比限制1:8至8:1。

        <strong>negative\_prompt</strong> `string` （可选）

        反向提示词，用来描述不希望在画面中看到的内容，可以对画面进行限制。仅 qwen-image-3.0 系列支持此参数。

        <strong>seed</strong> `integer`（可选）

        随机数种子，取值范围为`[0, 2147483647]`，未传入时，服务会随机选择种子。固定种子可使生成结果相对稳定。

        <strong>prompt\_extend</strong> `boolean` （可选）

        是否开启提示词智能改写，默认值为 `true`（建议开启）。开启后，模型会按照`prompt_extend_mode`指定的方式优化正向提示词，对描述较简单的提示词效果提升明显。

        <strong>prompt\_extend\_mode</strong> `string` （可选）

        提示词改写方式，默认值为`direct`。可选值：

        - `direct`：直接提示词增强（DPE），适用于大多数场景。T2I和I2I均支持。
        - `agent`：智能体提示词增强（APE），提供更精细的改写效果。仅支持文生图（T2I），图生图（I2I）场景传入`agent`将返回400错误。

        <strong>enable\_thinking</strong> `boolean` （可选）

        是否开启思考模式，默认值为`true`。开启时，模型将增强推理能力以提升出图质量，但会增加生成耗时。仅在 prompt\_extend=true 时生效，适用于 Direct T2I、Direct I2I 和 Agent T2I，I2I Agent 暂不支持。

        <strong>watermark</strong> `boolean` （可选）

        是否添加水印，默认值为 `false`。

        <Note>
          `image`、`negative_prompt`、`seed`、`prompt_extend`、`prompt_extend_mode`、`enable_thinking`、`watermark`为阿里云百炼扩展字段，不属于 OpenAI 官方参数。直接发送HTTP请求时放在请求体顶层即可；使用 OpenAI SDK 时需通过`extra_body`传入。
        </Note>
      </td>

      <td>
        <CodeGroup>
          ```bash 文生图 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/images/generations' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --data '{
              "model": "qwen-image-3.0-pro",
              "prompt": "画面是一张竖幅户外人像摄影，整体从上到下呈现温暖的午后街景氛围。顶部左侧到上方大面积被深绿色藤蔓和橙色小花覆盖，花叶从建筑檐口自然垂落，受阳光照射的叶片呈黄绿色高光，阴影处则偏深绿，形成浓密而柔和的背景层次。左上至中上区域是一块深蓝色横向招牌，招牌表面较暗、略带磨砂质感，上面以白色哥特体大字写着 Il Messaggero，文字位于画面左侧偏上，部分被前景花叶轻微遮挡，字体高对比、带装饰性尖角和粗细变化。招牌下方是报刊亭或书报摊的玻璃展示窗，黑色金属框架将橱窗分隔成多个矩形区域，内部陈列着许多报纸、杂志和书刊封面，但大多因景深虚化和光线反射而难以辨读，形成浅色纸张与深色边框交错的背景纹理。画面右上方是强烈的逆光区域，阳光从街道尽头照入，背景建筑被虚化成米灰色块面，边缘柔和，呈现明显的浅景深效果。画面中部偏右是一名年轻成年女性的半身至膝上人像，她回头面向镜头微笑，身体略向右转，肩背朝向观者，姿态自然放松。她有长而浓密的黑色波浪卷发，发丝被逆光勾勒出金色轮廓光，发梢在右侧向外散开，显得轻盈蓬松。她肤色白皙，脸型柔和偏鹅蛋形，眉形细致，眼睛明亮，眼妆清透，睫毛明显，面部带有自然高光，唇部为柔和珊瑚红色，笑容露齿，表情亲切明朗。她佩戴小巧耳饰，身穿黑色细肩带露背连衣裙，面料颜色深黑、轮廓简洁，细肩带从肩部向背部延伸，背部线条清晰。画面下部偏左到中部，她双手抱着一束玫瑰花，花束体积较大，主要由橙色、杏色、粉色和浅桃色玫瑰组成，花瓣层层卷曲，边缘被阳光照亮，绿色叶片和长花茎从花束下方垂出，花束与黑色裙装形成鲜明色彩对比。右侧背景是一条被阳光照亮的城市街道，地面呈暖灰与金黄色调，远处建筑、街边设施和一个模糊的红色圆形交通标志位于右下远景，均因焦外虚化而只保留色块和轮廓。整张照片采用暖色胶片感处理，带有细腻颗粒、柔和对比和明显逆光边缘光，人物位于视觉焦点，背景报刊亭、花藤、街道和阳光共同营造出浪漫、明亮、都市漫步式的氛围。",
              "size": "1024x1024",
              "n": 1,
              "prompt_extend": true
          }'
          ```

          ```bash 图生图 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/images/generations' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --data '{
              "model": "qwen-image-3.0-pro",
              "prompt": "帮我生成一张充满高级感的都市风格女性写真，画面中人物完美保留输入图片中这位年轻女性的面部特征与一头柔顺的黑色长发。人物脱下原本的米色针织上衣，换上一套彰显高雅气质的都市职场穿搭，身穿一件质感垂顺的香槟色真丝衬衫，外搭一件剪裁利落的深灰色休闲西装外套，下身搭配同色系的高腰阔腿裤，整体造型既干练又富有女人味。场景设定在一家装修现代简约的高端咖啡店内，背景是通透的落地玻璃窗，窗外隐约可见繁华的城市街景，室内摆放着深色实木长桌和舒适的皮质座椅，桌面上放置着一台打开的银色笔记本电脑、一份文件和一杯热气腾腾的美式咖啡。人物呈现出慵懒而放松的办公姿态，身体微微后仰倚靠在椅背上，一只手臂自然搭在扶手上，另一只手轻轻握着咖啡杯置于桌边，头部微侧，眼神清澈从容且带有一丝慵懒地直视镜头，嘴角挂着一抹优雅自信的微笑。人物化着精致得体的正式场合妆容，底妆清透干净，眉眼线条清晰利落，唇部涂抹着显气色的豆沙色口红，展现出成熟知性的魅力。光线采用午后柔和的自然光，从侧面透过落地窗洒入，在人物的面部轮廓和衣物褶皱上留下细腻的光影过渡，背景呈现自然的景深虚化效果，色彩以大地色、灰色和暖白色为主调，营造出宁静、高级且充满故事感的都市办公氛围，构图采用经典的竖幅七分身人像视角，人物位于画面视觉中心略偏右，比例协调，画质清晰细腻。",
              "image": "https://alidocs.oss-cn-zhangjiakou.aliyuncs.com/res/yBRq1ZPYEaXdyOdv/img/33a80a19-7ac7-4c64-b0fa-7d685b7046a0.png",
              "size": "1024x1024",
              "n": 1,
              "prompt_extend": true
          }'
          ```
        </CodeGroup>
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
        ### 响应参数 <span id="6740266-openai-resp-title" />

        <strong>created</strong> `integer`

        响应创建时间的 Unix 时间戳（秒）。

        <strong>data</strong> `array`

        生成结果列表。传入`n`大于1时，数组内包含多个元素。

        <Accordion title="属性" defaultOpen>
          <strong>url</strong> `string`

          生成图像的 URL，格式为PNG。<strong>链接有效期为24小时</strong>，请及时下载并保存图像。
        </Accordion>

        <strong>usage</strong> `object`

        本次调用的资源使用情况，仅调用成功时返回。此处为<strong>图片输入/输出计量信息，不是 Token 用量</strong>。

        <Accordion title="属性" defaultOpen>
          <Tabs>
            <Tab title="qwen-image-3.0 系列">
              <strong>output\_width</strong> `integer`

              最终输出图片的宽度（像素）。

              <strong>output\_height</strong> `integer`

              最终输出图片的高度（像素）。

              <strong>input\_image\_count</strong> `integer`

              用户请求中输入图片的数量。文生图（T2I）时为0，图生图（I2I）按实际输入图片数返回。

              <strong>input\_image\_type</strong> `string`

              输入图片计量档位。按输出分辨率像素面积判断：面积≤2,250,000为`qima_input_1k`，面积>2,250,000为`qima_input_2k`。

              <strong>output\_image\_count</strong> `integer`

              实际返回的输出图片数量。

              <strong>output\_image\_type</strong> `string`

              输出图片计量档位。按输出分辨率像素面积判断：面积≤2,250,000为`qima_output_1k`，面积>2,250,000为`qima_output_2k`。
            </Tab>

            <Tab title="qwen-image-2.1系列">
              <strong>image\_count</strong> `integer`

              模型生成图像的数量。

              <strong>width</strong> `integer`

              模型生成图像的宽度（像素）。

              <strong>height</strong> `integer`

              模型生成图像的高度（像素）。
            </Tab>
          </Tabs>
        </Accordion>

        <strong>error</strong> `object`

        错误信息，仅调用失败时返回。

        <Accordion title="属性" defaultOpen>
          <strong>message</strong> `string`

          请求失败的详细信息。

          <strong>type</strong> `string`

          错误类型，如`invalid_request_error`。

          <strong>param</strong> `string`

          出错的参数名，无法定位到具体参数时为`null`。

          <strong>code</strong> `string`

          请求失败的错误码。详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <Note>
          与DashScope协议不同，OpenAI 兼容模式<strong>不在响应体中返回</strong>`request_id`，请求唯一标识通过HTTP响应头`x-request-id`返回。使用 OpenAI Python SDK 时，成功响应可读取`response._request_id`，失败时可从`APIStatusError.request_id`读取。反馈问题时请提供该标识。
        </Note>
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            图像URL仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            <CodeGroup>
              ```json qwen-image-3.0 系列
              {
                  "created": 1788339600,
                  "data": [
                      {
                          "url": "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx"
                      }
                  ],
                  "usage": {
                      "output_height": 1024,
                      "output_width": 1024,
                      "input_image_count": 0,
                      "input_image_type": "qima_input_1k",
                      "output_image_count": 1,
                      "output_image_type": "qima_output_1k"
                  }
              }
              ```

              ```json qwen-image-2.1系列
              {
                  "created": 1788339600,
                  "data": [
                      {
                          "url": "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx"
                      }
                  ],
                  "usage": {
                      "image_count": 1,
                      "width": 1024,
                      "height": 1024
                  }
              }
              ```
            </CodeGroup>
          </Tab>

          <Tab title="任务执行异常">
            如果因为某种原因导致任务执行失败，将返回`error`对象，可以通过`code`和`message`字段明确指示错误原因。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "error": {
                    "message": "Field 'prompt' is required",
                    "type": "invalid_request_error",
                    "param": null,
                    "code": "InvalidParameter"
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### SDK调用 <span id="6740266-openai-sdk-title" />

请先安装或升级 OpenAI Python SDK：

```bash
pip install -U openai
```

<Tip>
  图像生成耗时较长，请显式设置足够大的客户端超时时间。多图输出（较大的`n`）或并发场景建议从600秒起配置，避免客户端提前断开。
</Tip>

<CodeGroup>
  ```python 文生图 expandable
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
      timeout=600.0,
  )

  response = client.images.generate(
      model="qwen-image-3.0-pro",
      prompt="画面是一张竖幅户外人像摄影，整体从上到下呈现温暖的午后街景氛围。顶部左侧到上方大面积被深绿色藤蔓和橙色小花覆盖，花叶从建筑檐口自然垂落，受阳光照射的叶片呈黄绿色高光，阴影处则偏深绿，形成浓密而柔和的背景层次。左上至中上区域是一块深蓝色横向招牌，招牌表面较暗、略带磨砂质感，上面以白色哥特体大字写着 Il Messaggero，文字位于画面左侧偏上，部分被前景花叶轻微遮挡，字体高对比、带装饰性尖角和粗细变化。招牌下方是报刊亭或书报摊的玻璃展示窗，黑色金属框架将橱窗分隔成多个矩形区域，内部陈列着许多报纸、杂志和书刊封面，但大多因景深虚化和光线反射而难以辨读，形成浅色纸张与深色边框交错的背景纹理。画面右上方是强烈的逆光区域，阳光从街道尽头照入，背景建筑被虚化成米灰色块面，边缘柔和，呈现明显的浅景深效果。画面中部偏右是一名年轻成年女性的半身至膝上人像，她回头面向镜头微笑，身体略向右转，肩背朝向观者，姿态自然放松。她有长而浓密的黑色波浪卷发，发丝被逆光勾勒出金色轮廓光，发梢在右侧向外散开，显得轻盈蓬松。她肤色白皙，脸型柔和偏鹅蛋形，眉形细致，眼睛明亮，眼妆清透，睫毛明显，面部带有自然高光，唇部为柔和珊瑚红色，笑容露齿，表情亲切明朗。她佩戴小巧耳饰，身穿黑色细肩带露背连衣裙，面料颜色深黑、轮廓简洁，细肩带从肩部向背部延伸，背部线条清晰。画面下部偏左到中部，她双手抱着一束玫瑰花，花束体积较大，主要由橙色、杏色、粉色和浅桃色玫瑰组成，花瓣层层卷曲，边缘被阳光照亮，绿色叶片和长花茎从花束下方垂出，花束与黑色裙装形成鲜明色彩对比。右侧背景是一条被阳光照亮的城市街道，地面呈暖灰与金黄色调，远处建筑、街边设施和一个模糊的红色圆形交通标志位于右下远景，均因焦外虚化而只保留色块和轮廓。整张照片采用暖色胶片感处理，带有细腻颗粒、柔和对比和明显逆光边缘光，人物位于视觉焦点，背景报刊亭、花藤、街道和阳光共同营造出浪漫、明亮、都市漫步式的氛围。",
      size="1024x1024",
      n=1,
      # 以上均为OpenAI官方参数。image、prompt_extend等扩展字段需通过extra_body传入，参见图生图示例
  )

  for item in response.data:
      print(item.url)
  print(f"request_id: {response._request_id}")
  ```

  ```python 图生图 expandable
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
      timeout=600.0,
  )

  response = client.images.generate(
      model="qwen-image-3.0-pro",
      prompt="帮我生成一张充满高级感的都市风格女性写真，画面中人物完美保留输入图片中这位年轻女性的面部特征与一头柔顺的黑色长发。人物换上一套彰显高雅气质的都市职场穿搭，场景设定在一家装修现代简约的高端咖啡店内。",
      size="1024x1024",
      n=1,
      extra_body={
          # 单张图像也可直接传字符串；也支持Base64：data:{MIME_type};base64,{base64_data}
          "image": [
              "https://alidocs.oss-cn-zhangjiakou.aliyuncs.com/res/yBRq1ZPYEaXdyOdv/img/33a80a19-7ac7-4c64-b0fa-7d685b7046a0.png"
          ],
          "prompt_extend": True,
      },
  )

  for item in response.data:
      print(item.url)
  print(f"request_id: {response._request_id}")
  ```

  ```python 读取request_id expandable
  import os
  from openai import OpenAI, APIStatusError

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
      timeout=600.0,
  )

  try:
      response = client.images.generate(
          model="qwen-image-3.0-pro",
          prompt="画面是一张竖幅户外人像摄影，整体从上到下呈现温暖的午后街景氛围。",
      )
      print(response.data[0].url)
  except APIStatusError as exc:
      print(f"status_code: {exc.status_code}")
      print(f"request_id: {exc.request_id}")
      print(exc)
  ```
</CodeGroup>

## DashScope同步调用（推荐） <span id="6740266-sync-title" />

### HTTP调用 <span id="6740266-http-title" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`
  </Tab>

  <Tab title="德国（法兰克福）">
    `POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`
  </Tab>

  <Tab title="日本（东京）">
    `POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`
  </Tab>

  <Tab title="中国香港">
    `POST https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="6740266-req-params-h4" />

        ##### 请求头（Headers） <span id="6740266-headers-h5" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### 请求体（Request Body） <span id="6740266-body-h5" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，可选值为`qwen-image-3.0-pro`、`qwen-image-3.0`、`qwen-image-2.1-pro`和`qwen-image-2.1-turbo`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入参数对象，包含以下字段：

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。<strong>当前仅支持单轮对话</strong>，因此数组内<strong>有且只有一个对象</strong>，该对象包含`role`和`content`两个属性。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息发送者角色，必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组，根据使用场景有不同的组合方式：

            - <strong>文生图（T2I）</strong>：仅包含一个`{"text": "..."}`对象。
            - <strong>图生图（I2I）</strong>：qwen-image-2.1系列包含1-10个`{"image": "..."}`对象，qwen-image-3.0系列包含1-3个，以及1个`{"text": "..."}`对象。

            <Accordion title="属性" defaultOpen>
              <strong>image</strong> `string` （可选）

              输入图像的 URL 或 Base64 编码数据。I2I场景下，qwen-image-2.1系列支持传入1-10张图像，qwen-image-3.0系列支持传入1-3张图像。多图输入时，按照数组顺序定义图像顺序。

              <strong>图像要求：</strong>

              - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP和GIF。
              - 图像分辨率：宽和高须在1至8000像素之间，超过将返回400错误；建议在384至2048像素之间以获得最佳效果。
              - 图像宽高比：长边与短边之比不超过10:1。
              - 图像大小：不超过10MB。

              <strong>支持的输入格式</strong>

              1. 公网URL：支持 HTTP 和 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。
              2. Base64 编码：格式为`data:{MIME_type};base64,{base64_data}`。

              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词，用于描述您期望生成或编辑的图像内容、风格和构图。支持中英文，推荐不超过4500Token。

              <strong>注意</strong>：仅支持传入一个text，不传或传入多个将报错。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        控制图像生成的附加参数。

        <Accordion title="属性" defaultOpen>
          <strong>prompt\_extend</strong> `boolean` （可选）

          是否开启提示词智能改写，默认值为 `true`（建议开启）。开启后，模型会按照`prompt_extend_mode`指定的方式优化正向提示词，对描述较简单的提示词效果提升明显。

          <strong>prompt\_extend\_mode</strong> `string` （可选）

          提示词改写方式，默认值为`direct`。可选值：

          - `direct`：直接提示词增强（DPE），适用于大多数场景。T2I和I2I均支持。
          - `agent`：智能体提示词增强（APE），提供更精细的改写效果。仅支持文生图（T2I），图生图（I2I）场景传入`agent`将返回400错误。

          <strong>enable\_thinking</strong> `boolean` （可选）

          是否开启思考模式，默认值为`true`。开启时，模型将增强推理能力以提升出图质量，但会增加生成耗时。仅在 prompt\_extend=true 时生效，适用于 Direct T2I、Direct I2I 和 Agent T2I，I2I Agent 暂不支持。

          <strong>n</strong> `integer` （可选）

          输出图像的数量，支持输出1-6张图片，默认值为1。

          <strong>size</strong> `string` （可选）

          设置输出图像的分辨率，格式为`宽*高`，例如`"1024*1024"`。未指定时由模型根据提示词自动推荐分辨率。

          - <strong>文生图（T2I）</strong>：像素面积范围512\*512至2048\*2048，宽高比限制1:8至8:1。
          - <strong>图生图（I2I）</strong>：像素面积范围512\*512至2048\*2048，宽高比限制1:8至8:1。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在画面中看到的内容，可以对画面进行限制。仅 qwen-image-3.0 系列支持此参数。

          <strong>seed</strong> `integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`，未传入时，服务会随机选择种子。固定种子可使生成结果相对稳定。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印，默认值为 `false`。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```bash 文生图 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --data '{
              "model": "qwen-image-3.0-pro",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "text": "画面是一张竖幅户外人像摄影，整体从上到下呈现温暖的午后街景氛围。顶部左侧到上方大面积被深绿色藤蔓和橙色小花覆盖，花叶从建筑檐口自然垂落，受阳光照射的叶片呈黄绿色高光，阴影处则偏深绿，形成浓密而柔和的背景层次。左上至中上区域是一块深蓝色横向招牌，招牌表面较暗、略带磨砂质感，上面以白色哥特体大字写着 Il Messaggero，文字位于画面左侧偏上，部分被前景花叶轻微遮挡，字体高对比、带装饰性尖角和粗细变化。招牌下方是报刊亭或书报摊的玻璃展示窗，黑色金属框架将橱窗分隔成多个矩形区域，内部陈列着许多报纸、杂志和书刊封面，但大多因景深虚化和光线反射而难以辨读，形成浅色纸张与深色边框交错的背景纹理。画面右上方是强烈的逆光区域，阳光从街道尽头照入，背景建筑被虚化成米灰色块面，边缘柔和，呈现明显的浅景深效果。画面中部偏右是一名年轻成年女性的半身至膝上人像，她回头面向镜头微笑，身体略向右转，肩背朝向观者，姿态自然放松。她有长而浓密的黑色波浪卷发，发丝被逆光勾勒出金色轮廓光，发梢在右侧向外散开，显得轻盈蓬松。她肤色白皙，脸型柔和偏鹅蛋形，眉形细致，眼睛明亮，眼妆清透，睫毛明显，面部带有自然高光，唇部为柔和珊瑚红色，笑容露齿，表情亲切明朗。她佩戴小巧耳饰，身穿黑色细肩带露背连衣裙，面料颜色深黑、轮廓简洁，细肩带从肩部向背部延伸，背部线条清晰。画面下部偏左到中部，她双手抱着一束玫瑰花，花束体积较大，主要由橙色、杏色、粉色和浅桃色玫瑰组成，花瓣层层卷曲，边缘被阳光照亮，绿色叶片和长花茎从花束下方垂出，花束与黑色裙装形成鲜明色彩对比。右侧背景是一条被阳光照亮的城市街道，地面呈暖灰与金黄色调，远处建筑、街边设施和一个模糊的红色圆形交通标志位于右下远景，均因焦外虚化而只保留色块和轮廓。整张照片采用暖色胶片感处理，带有细腻颗粒、柔和对比和明显逆光边缘光，人物位于视觉焦点，背景报刊亭、花藤、街道和阳光共同营造出浪漫、明亮、都市漫步式的氛围。"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "prompt_extend": true
              }
          }'
          ```

          ```bash 图生图 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --data '{
              "model": "qwen-image-3.0-pro",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "image": "https://alidocs.oss-cn-zhangjiakou.aliyuncs.com/res/yBRq1ZPYEaXdyOdv/img/33a80a19-7ac7-4c64-b0fa-7d685b7046a0.png"
                              },
                              {
                                  "text": "帮我生成一张充满高级感的都市风格女性写真，画面中人物完美保留输入图片中这位年轻女性的面部特征与一头柔顺的黑色长发。人物脱下原本的米色针织上衣，换上一套彰显高雅气质的都市职场穿搭，身穿一件质感垂顺的香槟色真丝衬衫，外搭一件剪裁利落的深灰色休闲西装外套，下身搭配同色系的高腰阔腿裤，整体造型既干练又富有女人味。场景设定在一家装修现代简约的高端咖啡店内，背景是通透的落地玻璃窗，窗外隐约可见繁华的城市街景，室内摆放着深色实木长桌和舒适的皮质座椅，桌面上放置着一台打开的银色笔记本电脑、一份文件和一杯热气腾腾的美式咖啡。人物呈现出慵懒而放松的办公姿态，身体微微后仰倚靠在椅背上，一只手臂自然搭在扶手上，另一只手轻轻握着咖啡杯置于桌边，头部微侧，眼神清澈从容且带有一丝慵懒地直视镜头，嘴角挂着一抹优雅自信的微笑。人物化着精致得体的正式场合妆容，底妆清透干净，眉眼线条清晰利落，唇部涂抹着显气色的豆沙色口红，展现出成熟知性的魅力。光线采用午后柔和的自然光，从侧面透过落地窗洒入，在人物的面部轮廓和衣物褶皱上留下细腻的光影过渡，背景呈现自然的景深虚化效果，色彩以大地色、灰色和暖白色为主调，营造出宁静、高级且充满故事感的都市办公氛围，构图采用经典的竖幅七分身人像视角，人物位于画面视觉中心略偏右，比例协调，画质清晰细腻。"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "prompt_extend": true
              }
          }'
          ```
        </CodeGroup>
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
        #### 响应参数 <span id="6740266-resp-h4" />

        <strong>output</strong> `object`

        包含模型生成结果。

        <Accordion title="属性" defaultOpen>
          <strong>rewrite\_status</strong> `string`

          提示词改写状态，具体取值由请求是否开启改写以及改写执行结果决定。

          <strong>choices</strong> `array`

          结果选项列表。

          <Accordion title="属性" defaultOpen>
            <strong>finish\_reason</strong> `string`

            任务停止原因，自然停止时为`stop`。

            <strong>message</strong> `object`

            模型返回的消息。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              消息的角色，固定为`assistant`。

              <strong>content</strong>`array`

              消息内容，包含生成的图像信息。

              <Accordion title="属性" defaultOpen>
                <strong>image</strong> `string`

                生成图像的 URL，格式为PNG。<strong>链接有效期为24小时</strong>，请及时下载并保存图像。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        本次调用的资源使用情况，仅调用成功时返回。此处为<strong>图片输入/输出计量信息，不是 Token 用量</strong>。

        <Accordion title="属性" defaultOpen>
          <Tabs>
            <Tab title="qwen-image-3.0 系列">
              <strong>output\_width</strong> `integer`

              最终输出图片的宽度（像素）。

              <strong>output\_height</strong> `integer`

              最终输出图片的高度（像素）。

              <strong>input\_image\_count</strong> `integer`

              用户请求中输入图片的数量。文生图（T2I）时为0，图生图（I2I）按实际输入图片数返回。

              <strong>input\_image\_type</strong> `string`

              输入图片计量档位。按输出分辨率像素面积判断：面积≤2,250,000为`qima_input_1k`，面积>2,250,000为`qima_input_2k`。

              <strong>output\_image\_count</strong> `integer`

              实际返回的输出图片数量。

              <strong>output\_image\_type</strong> `string`

              输出图片计量档位。按输出分辨率像素面积判断：面积≤2,250,000为`qima_output_1k`，面积>2,250,000为`qima_output_2k`。
            </Tab>

            <Tab title="qwen-image-2.1系列">
              <strong>image\_count</strong> `integer`

              模型生成图像的数量。

              <strong>width</strong> `integer`

              模型生成图像的宽度（像素）。

              <strong>height</strong> `integer`

              模型生成图像的高度（像素）。
            </Tab>
          </Tabs>
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
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            <CodeGroup>
              ```json qwen-image-3.0 系列
              {
                  "output": {
                      "choices": [
                          {
                              "finish_reason": "stop",
                              "message": {
                                  "content": [
                                      {
                                          "image": "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx"
                                      }
                                  ],
                                  "role": "assistant"
                              }
                          }
                      ]
                  },
                  "usage": {
                      "output_height": 1024,
                      "output_width": 1024,
                      "input_image_count": 0,
                      "input_image_type": "qima_input_1k",
                      "output_image_count": 1,
                      "output_image_type": "qima_output_1k"
                  },
                  "request_id": "571ae02f-5c9d-436c-83c2-f221e6df0xxx"
              }
              ```

              ```json qwen-image-2.1系列
              {
                  "output": {
                      "choices": [
                          {
                              "finish_reason": "stop",
                              "message": {
                                  "content": [
                                      {
                                          "image": "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx"
                                      }
                                  ],
                                  "role": "assistant"
                              }
                          }
                      ]
                  },
                  "usage": {
                      "image_count": 1,
                      "width": 1024,
                      "height": 1024
                  },
                  "request_id": "571ae02f-5c9d-436c-83c2-f221e6df0xxx"
              }
              ```
            </CodeGroup>
          </Tab>

          <Tab title="任务执行异常">
            如果因为某种原因导致任务执行失败，将返回相关信息，可以通过code和message字段明确指示错误原因。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "31f808fd-8eef-9004-xxxxx",
                "code": "InvalidApiKey",
                "message": "Invalid API-key provided."
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### SDK调用 <span id="6740266-sdk-title" />

以下以图生图/图像编辑（I2I）为示例，展示Python和Java SDK的调用方式。

<CodeGroup dropdown>
  ```python Python expandable
  import os
  import base64
  import mimetypes
  import dashscope
  from dashscope import MultiModalConversation

  dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

  def encode_file(file_path):
      mime_type, _ = mimetypes.guess_type(file_path)
      if not mime_type or not mime_type.startswith("image/"):
          raise ValueError("Unsupported or unrecognized image format")
      with open(file_path, "rb") as image_file:
          encoded_string = base64.b64encode(image_file.read()).decode('utf-8')
      return f"data:{mime_type};base64,{encoded_string}"

  # [方法一] 使用公网图像URL
  image_url = "https://alidocs.oss-cn-zhangjiakou.aliyuncs.com/res/yBRq1ZPYEaXdyOdv/img/33a80a19-7ac7-4c64-b0fa-7d685b7046a0.png"

  # [方法二] 使用Base64编码图像
  # image_url = encode_file("./your_image.png")

  response = MultiModalConversation.call(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      model="qwen-image-3.0-pro",
      messages=[{
          "role": "user",
          "content": [
              {"image": image_url},
              {"text": "帮我生成一张充满高级感的都市风格女性写真，画面中人物完美保留输入图片中这位年轻女性的面部特征与一头柔顺的黑色长发。人物换上一套彰显高雅气质的都市职场穿搭，场景设定在一家装修现代简约的高端咖啡店内。"}
          ]
      }],
      prompt_extend=True
  )

  print(response)
  if response.status_code == 200:
      url = response.output.choices[0].message.content[0]["image"]
      print(f"Generated image URL: {url}")
  else:
      print(f"Error: {response.code} - {response.message}")
  ```

  ```java Java expandable
  import java.util.Arrays;
  import java.util.Base64;
  import java.util.Collections;
  import java.io.IOException;
  import java.nio.file.Files;
  import java.nio.file.Path;
  import java.nio.file.Paths;
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
  import com.alibaba.dashscope.common.MultiModalMessage;
  import com.alibaba.dashscope.common.Role;
  import com.alibaba.dashscope.utils.Constants;

  public class ImageEditExample {
      public static void main(String[] args) {
          Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";

          // [方法一] 使用公网图像URL
          String imageUrl = "https://alidocs.oss-cn-zhangjiakou.aliyuncs.com/res/yBRq1ZPYEaXdyOdv/img/33a80a19-7ac7-4c64-b0fa-7d685b7046a0.png";

          // [方法二] 使用Base64编码图像
          // String imageUrl = encodeFile("/path/to/your/image.png");

          MultiModalConversation conv = new MultiModalConversation();
          MultiModalMessage userMessage = MultiModalMessage.builder()
              .role(Role.USER.getValue())
              .content(Arrays.asList(
                  Collections.singletonMap("image", imageUrl),
                  Collections.singletonMap("text", "帮我生成一张充满高级感的都市风格女性写真，画面中人物完美保留输入图片中这位年轻女性的面部特征与一头柔顺的黑色长发。人物换上一套彰显高雅气质的都市职场穿搭，场景设定在一家装修现代简约的高端咖啡店内。")
              ))
              .build();
          MultiModalConversationParam param = MultiModalConversationParam.builder()
              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
              .model("qwen-image-3.0-pro")
              .messages(Arrays.asList(userMessage))
              .parameter("prompt_extend", true)
              .build();
          try {
              MultiModalConversationResult result = conv.call(param);
              System.out.println(result);
          } catch (Exception e) {
              e.printStackTrace();
          }
      }

      public static String encodeFile(String filePath) {
          Path path = Paths.get(filePath);
          if (!Files.exists(path)) {
              throw new IllegalArgumentException("File does not exist: " + filePath);
          }
          String mimeType = null;
          try {
              mimeType = Files.probeContentType(path);
          } catch (IOException e) {
              throw new IllegalArgumentException("Cannot detect file type: " + filePath);
          }
          if (mimeType == null || !mimeType.startsWith("image/")) {
              throw new IllegalArgumentException("Unsupported or unrecognized image format");
          }
          byte[] fileBytes = null;
          try {
              fileBytes = Files.readAllBytes(path);
          } catch (IOException e) {
              throw new IllegalArgumentException("Cannot read file content: " + filePath);
          }
          String encodedString = Base64.getEncoder().encodeToString(fileBytes);
          return "data:" + mimeType + ";base64," + encodedString;
      }
  }
  ```
</CodeGroup>

## DashScope异步调用 <span id="6740266-async-title" />

千问-图像生成与编辑模型除了支持上文的同步调用外，还支持异步调用。异步接口与同步接口共用相同的请求参数结构，仅需在请求头中增加`X-DashScope-Async: enable`，服务受理后返回任务ID（`task_id`），再通过任务ID轮询查询接口获取最终结果。

<Tip>
  异步接口的请求地址与同步接口不同，请使用本节给出的Endpoint，不要沿用同步接口地址。
</Tip>

### HTTP调用 <span id="6740266-async-http-title" />

调用流程分为两步：

1. <strong>创建任务获取任务ID</strong>：发送一个请求创建任务，该请求会返回<strong>任务ID（task\_id）</strong>。
2. <strong>根据任务ID查询结果</strong>：使用task\_id轮询任务状态，直到任务完成并获得图像URL。

#### 步骤1：创建任务获取任务ID <span id="6740266-async-step1-title" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`
  </Tab>

  <Tab title="德国（法兰克福）">
    `POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`
  </Tab>

  <Tab title="日本（东京）">
    `POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`
  </Tab>

  <Tab title="中国香港">
    `POST https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`
  </Tab>
</Tabs>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ##### 请求参数 <span id="6740266-async-req-params-h5" />

        ###### 请求头（Headers） <span id="6740266-async-headers-h6" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。<strong>必须设置为</strong>`enable`。本节的Endpoint仅受理异步请求，不支持同步调用。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ###### 请求体（Request Body） <span id="6740266-async-body-h6" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，可选值为`qwen-image-3.0-pro`、`qwen-image-3.0`、`qwen-image-2.1-pro`和`qwen-image-2.1-turbo`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入参数对象，包含以下字段：

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。<strong>当前仅支持单轮对话</strong>，因此数组内<strong>有且只有一个对象</strong>，该对象包含`role`和`content`两个属性。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息发送者角色，必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组，根据使用场景有不同的组合方式：

            - <strong>文生图（T2I）</strong>：仅包含一个`{"text": "..."}`对象。
            - <strong>图生图（I2I）</strong>：qwen-image-2.1系列包含1-10个`{"image": "..."}`对象，qwen-image-3.0系列包含1-3个，以及1个`{"text": "..."}`对象。

            <Accordion title="属性" defaultOpen>
              <strong>image</strong> `string` （可选）

              输入图像的 URL 或 Base64 编码数据。I2I场景下，qwen-image-2.1系列支持传入1-10张图像，qwen-image-3.0系列支持传入1-3张图像。多图输入时，按照数组顺序定义图像顺序。

              <strong>图像要求：</strong>

              - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP和GIF。
              - 图像分辨率：宽和高须在1至8000像素之间，超过将返回400错误；建议在384至2048像素之间以获得最佳效果。
              - 图像宽高比：长边与短边之比不超过10:1。
              - 图像大小：不超过10MB。

              <strong>支持的输入格式</strong>

              1. 公网URL：支持 HTTP 和 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。
              2. Base64 编码：格式为`data:{MIME_type};base64,{base64_data}`。

              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词，用于描述您期望生成或编辑的图像内容、风格和构图。支持中英文，推荐不超过4500Token。

              <strong>注意</strong>：仅支持传入一个text，不传或传入多个将报错。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        控制图像生成的附加参数。

        <Accordion title="属性" defaultOpen>
          <strong>prompt\_extend</strong> `boolean` （可选）

          是否开启提示词智能改写，默认值为 `true`（建议开启）。开启后，模型会按照`prompt_extend_mode`指定的方式优化正向提示词，对描述较简单的提示词效果提升明显。

          <strong>prompt\_extend\_mode</strong> `string` （可选）

          提示词改写方式，默认值为`direct`。可选值：

          - `direct`：直接提示词增强（DPE），适用于大多数场景。T2I和I2I均支持。
          - `agent`：智能体提示词增强（APE），提供更精细的改写效果。仅支持文生图（T2I），图生图（I2I）场景传入`agent`将返回400错误。

          <strong>enable\_thinking</strong> `boolean` （可选）

          是否开启思考模式，默认值为`true`。开启时，模型将增强推理能力以提升出图质量，但会增加生成耗时。仅在 prompt\_extend=true 时生效，适用于 Direct T2I、Direct I2I 和 Agent T2I，I2I Agent 暂不支持。

          <strong>n</strong> `integer` （可选）

          输出图像的数量，支持输出1-6张图片，默认值为1。

          <strong>size</strong> `string` （可选）

          设置输出图像的分辨率，格式为`宽*高`，例如`"1024*1024"`。未指定时由模型根据提示词自动推荐分辨率。

          - <strong>文生图（T2I）</strong>：像素面积范围512\*512至2048\*2048，宽高比限制1:8至8:1。
          - <strong>图生图（I2I）</strong>：像素面积范围512\*512至2048\*2048，宽高比限制1:8至8:1。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在画面中看到的内容，可以对画面进行限制。仅 qwen-image-3.0 系列支持此参数。

          <strong>seed</strong> `integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`，未传入时，服务会随机选择种子。固定种子可使生成结果相对稳定。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印，默认值为 `false`。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```bash 文生图 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'X-DashScope-Async: enable' \
          --data '{
              "model": "qwen-image-3.0-pro",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "text": "画面是一张竖幅户外人像摄影，整体从上到下呈现温暖的午后街景氛围。顶部左侧到上方大面积被深绿色藤蔓和橙色小花覆盖，花叶从建筑檐口自然垂落，受阳光照射的叶片呈黄绿色高光，阴影处则偏深绿，形成浓密而柔和的背景层次。左上至中上区域是一块深蓝色横向招牌，招牌表面较暗、略带磨砂质感，上面以白色哥特体大字写着 Il Messaggero，文字位于画面左侧偏上，部分被前景花叶轻微遮挡，字体高对比、带装饰性尖角和粗细变化。招牌下方是报刊亭或书报摊的玻璃展示窗，黑色金属框架将橱窗分隔成多个矩形区域，内部陈列着许多报纸、杂志和书刊封面，但大多因景深虚化和光线反射而难以辨读，形成浅色纸张与深色边框交错的背景纹理。画面右上方是强烈的逆光区域，阳光从街道尽头照入，背景建筑被虚化成米灰色块面，边缘柔和，呈现明显的浅景深效果。画面中部偏右是一名年轻成年女性的半身至膝上人像，她回头面向镜头微笑，身体略向右转，肩背朝向观者，姿态自然放松。她有长而浓密的黑色波浪卷发，发丝被逆光勾勒出金色轮廓光，发梢在右侧向外散开，显得轻盈蓬松。她肤色白皙，脸型柔和偏鹅蛋形，眉形细致，眼睛明亮，眼妆清透，睫毛明显，面部带有自然高光，唇部为柔和珊瑚红色，笑容露齿，表情亲切明朗。她佩戴小巧耳饰，身穿黑色细肩带露背连衣裙，面料颜色深黑、轮廓简洁，细肩带从肩部向背部延伸，背部线条清晰。画面下部偏左到中部，她双手抱着一束玫瑰花，花束体积较大，主要由橙色、杏色、粉色和浅桃色玫瑰组成，花瓣层层卷曲，边缘被阳光照亮，绿色叶片和长花茎从花束下方垂出，花束与黑色裙装形成鲜明色彩对比。右侧背景是一条被阳光照亮的城市街道，地面呈暖灰与金黄色调，远处建筑、街边设施和一个模糊的红色圆形交通标志位于右下远景，均因焦外虚化而只保留色块和轮廓。整张照片采用暖色胶片感处理，带有细腻颗粒、柔和对比和明显逆光边缘光，人物位于视觉焦点，背景报刊亭、花藤、街道和阳光共同营造出浪漫、明亮、都市漫步式的氛围。"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "prompt_extend": true
              }
          }'
          ```

          ```bash 图生图 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'X-DashScope-Async: enable' \
          --data '{
              "model": "qwen-image-3.0-pro",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "image": "https://alidocs.oss-cn-zhangjiakou.aliyuncs.com/res/yBRq1ZPYEaXdyOdv/img/33a80a19-7ac7-4c64-b0fa-7d685b7046a0.png"
                              },
                              {
                                  "text": "帮我生成一张充满高级感的都市风格女性写真，画面中人物完美保留输入图片中这位年轻女性的面部特征与一头柔顺的黑色长发。人物脱下原本的米色针织上衣，换上一套彰显高雅气质的都市职场穿搭，身穿一件质感垂顺的香槟色真丝衬衫，外搭一件剪裁利落的深灰色休闲西装外套，下身搭配同色系的高腰阔腿裤，整体造型既干练又富有女人味。场景设定在一家装修现代简约的高端咖啡店内，背景是通透的落地玻璃窗，窗外隐约可见繁华的城市街景，室内摆放着深色实木长桌和舒适的皮质座椅，桌面上放置着一台打开的银色笔记本电脑、一份文件和一杯热气腾腾的美式咖啡。人物呈现出慵懒而放松的办公姿态，身体微微后仰倚靠在椅背上，一只手臂自然搭在扶手上，另一只手轻轻握着咖啡杯置于桌边，头部微侧，眼神清澈从容且带有一丝慵懒地直视镜头，嘴角挂着一抹优雅自信的微笑。人物化着精致得体的正式场合妆容，底妆清透干净，眉眼线条清晰利落，唇部涂抹着显气色的豆沙色口红，展现出成熟知性的魅力。光线采用午后柔和的自然光，从侧面透过落地窗洒入，在人物的面部轮廓和衣物褶皱上留下细腻的光影过渡，背景呈现自然的景深虚化效果，色彩以大地色、灰色和暖白色为主调，营造出宁静、高级且充满故事感的都市办公氛围，构图采用经典的竖幅七分身人像视角，人物位于画面视觉中心略偏右，比例协调，画质清晰细腻。"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "prompt_extend": true
              }
          }'
          ```
        </CodeGroup>
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
        ##### 响应参数 <span id="6740266-async-resp1-h5" />

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

#### 步骤2：根据任务ID查询结果 <span id="6740266-async-step2-title" />

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

  <Tab title="中国香港">
    `GET https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>
</Tabs>

必须使用提交任务时的地域、业务空间和API Key查询任务，不可跨地域或跨业务空间查询。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ##### 请求参数 <span id="6740266-async-step2-req-h5" />

        ###### 请求头（Headers） <span id="6740266-async-step2-headers-h6" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ###### URL路径参数（Path parameters） <span id="6740266-async-step2-path-h6" />

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
        ##### 响应参数 <span id="6740266-async-resp2-h5" />

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

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>rewrite\_status</strong> `string`

          提示词改写状态，具体取值由请求是否开启改写以及改写执行结果决定。

          <strong>choices</strong> `array`

          结果选项列表。

          <Accordion title="属性" defaultOpen>
            <strong>finish\_reason</strong> `string`

            任务停止原因，自然停止时为`stop`。

            <strong>message</strong> `object`

            模型返回的消息。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              消息的角色，固定为`assistant`。

              <strong>content</strong>`array`

              消息内容，包含生成的图像信息。

              <Accordion title="属性" defaultOpen>
                <strong>image</strong> `string`

                生成图像的 URL，格式为PNG。<strong>链接有效期为24小时</strong>，请及时下载并保存图像。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        本次调用的资源使用情况，仅调用成功时返回。此处为<strong>图片输入/输出计量信息，不是 Token 用量</strong>。

        <Accordion title="属性" defaultOpen>
          <Tabs>
            <Tab title="qwen-image-3.0 系列">
              <strong>output\_width</strong> `integer`

              最终输出图片的宽度（像素）。

              <strong>output\_height</strong> `integer`

              最终输出图片的高度（像素）。

              <strong>input\_image\_count</strong> `integer`

              用户请求中输入图片的数量。文生图（T2I）时为0，图生图（I2I）按实际输入图片数返回。

              <strong>input\_image\_type</strong> `string`

              输入图片计量档位。按输出分辨率像素面积判断：面积≤2,250,000为`qima_input_1k`，面积>2,250,000为`qima_input_2k`。

              <strong>output\_image\_count</strong> `integer`

              实际返回的输出图片数量。

              <strong>output\_image\_type</strong> `string`

              输出图片计量档位。按输出分辨率像素面积判断：面积≤2,250,000为`qima_output_1k`，面积>2,250,000为`qima_output_2k`。
            </Tab>

            <Tab title="qwen-image-2.1系列">
              <strong>image\_count</strong> `integer`

              模型生成图像的数量。

              <strong>width</strong> `integer`

              模型生成图像的宽度（像素）。

              <strong>height</strong> `integer`

              模型生成图像的高度（像素）。
            </Tab>
          </Tabs>
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
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            <CodeGroup>
              ```json qwen-image-3.0 系列
              {
                  "output": {
                      "task_id": "17d7d840-82b9-485b-a954-724d06bc88d2",
                      "task_status": "SUCCEEDED",
                      "submit_time": "2026-08-07 15:50:14.837",
                      "scheduled_time": "2026-08-07 15:50:14.884",
                      "end_time": "2026-08-07 15:50:33.607",
                      "rewrite_status": "not_use",
                      "choices": [
                          {
                              "finish_reason": "stop",
                              "message": {
                                  "role": "assistant",
                                  "content": [
                                      {
                                          "image": "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx",
                                          "type": "image"
                                      }
                                  ]
                              }
                          }
                      ]
                  },
                  "usage": {
                      "output_height": 1024,
                      "output_width": 1024,
                      "input_image_count": 0,
                      "input_image_type": "qima_input_1k",
                      "output_image_count": 1,
                      "output_image_type": "qima_output_1k"
                  },
                  "request_id": "2bd94002-5624-9129-916b-fbdde107b4ba"
              }
              ```

              ```json qwen-image-2.1系列
              {
                  "output": {
                      "task_id": "17d7d840-82b9-485b-a954-724d06bc88d2",
                      "task_status": "SUCCEEDED",
                      "submit_time": "2026-08-07 15:50:14.837",
                      "scheduled_time": "2026-08-07 15:50:14.884",
                      "end_time": "2026-08-07 15:50:33.607",
                      "rewrite_status": "not_use",
                      "choices": [
                          {
                              "finish_reason": "stop",
                              "message": {
                                  "role": "assistant",
                                  "content": [
                                      {
                                          "image": "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx",
                                          "type": "image"
                                      }
                                  ]
                              }
                          }
                      ]
                  },
                  "usage": {
                      "image_count": 1,
                      "width": 1024,
                      "height": 1024
                  },
                  "request_id": "2bd94002-5624-9129-916b-fbdde107b4ba"
              }
              ```
            </CodeGroup>
          </Tab>

          <Tab title="任务执行异常">
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "output": {
                    "task_id": "17d7d840-82b9-485b-a954-724d06bc88d2",
                    "task_status": "FAILED",
                    "code": "InternalError",
                    "message": "An internal error has occurred."
                },
                "request_id": "31f808fd-8eef-9004-xxxxx"
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
