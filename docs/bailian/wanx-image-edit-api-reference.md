> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-通用图像编辑API参考

> 本文介绍万相-通用图像编辑模型的输入输出参数。

<meta property="og:title" content="万相-通用图像编辑API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议从 `https://dashscope.aliyuncs.com` 迁移至新域名。

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

该模型通过简单的指令即可实现多样化的图像编辑，适用于扩图、去水印、风格迁移、图像修复、图像美化等场景。 当前支持以下功能：

- <strong>图像风格化</strong>：全局风格化、局部风格化。
- <strong>图像内容编辑</strong>：指令编辑（无需指定区域，仅通过指令增加/修改图片内容）、局部重绘（针对指定区域增加/删除/修改图片内容）、去文字水印（中英文）。
- <strong>图像尺寸与分辨率优化</strong>：扩图（按比例扩图）、图像超分（高清放大）。
- <strong>图像色彩处理</strong>：图像上色（黑白或灰度图像转为彩色图像）。
- <strong>基于参考图像生成</strong>：线稿生图（先提取输入图像的线稿，再参考线稿生成图像）、参考卡通形象生图。

<strong>相关指南</strong>：[图像编辑-万相2.1](/zh/model-studio/wanx-image-edit)

## 模型概览 <span id="4a3e46b825d2k" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18.715393%" }} /><col style={{ width: "10.963455%" }} /><col style={{ width: "21.151717%" }} /><col style={{ width: "19.601329%" }} /><col style={{ width: "29.568106%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th rowSpan={2}><p><strong>计费单价</strong></p></th><th colSpan={2}><p><strong>限流（主账号与RAM子账号共用）</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a><strong /></p></th></tr><tr><th><p><strong>任务下发接口RPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>wanx2.1-imageedit</p></td><td><p>0.14元/张</p></td><td><p>2</p></td><td><p>2</p></td><td><p>500张</p></td></tr></tbody></table>

更多说明请参见[模型计费与限流](/zh/model-studio/image-faq#3436cf2280fnh)。

## 模型效果 <span id="2c8b6e330fjan" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.82%" }} /><col style={{ width: "30.72%" }} /><col style={{ width: "26.74%" }} /><col style={{ width: "30.72%" }} /></colgroup><thead><tr><th><p><strong>模型功能</strong></p></th><th><p><strong>输入图像</strong></p></th><th><p><strong>输入提示词</strong></p></th><th><p><strong>输出图像</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p><strong>全局风格化</strong></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930138.png" alt="image" width={250} /></p></td><td style={{ verticalAlign: "top" }}><p>转换成法国绘本风格</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930139.png" alt="image" width={250} /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>局部风格化</strong></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930188.png" alt="image" width={250} /></p></td><td style={{ verticalAlign: "top" }}><p>把房子变成木板风格。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930413.png" alt="image" width={250} /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>指令编辑</strong></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p942602.png" alt="image" width={250} height={250} /></p></td><td style={{ verticalAlign: "top" }}><p>把她的头发修改为红色。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p942603.png" alt="image" width={250} height={250} /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>局部重绘</strong></p></td><td style={{ verticalAlign: "top" }}><p>输入图像</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930416.png" alt="image" width={250} /></p> <p>输入涂抹区域图像（白色为涂抹区域）</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930417.png" alt="image" width={250} /></p></td><td style={{ verticalAlign: "top" }}><p>一只陶瓷兔子抱着一朵陶瓷花。</p></td><td style={{ verticalAlign: "top" }}><p>输出图像</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930415.png" alt="image" width={250} /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>去文字水印</strong></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930146.png" alt="image" width={250} /></p></td><td style={{ verticalAlign: "top" }}><p>去除图像中的文字。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930418.png" alt="image" width={250} /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>扩图</strong></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930580.jpg" alt="20250319105917" width={250} height={444} /></p></td><td style={{ verticalAlign: "top" }}><p>一位绿色仙子。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930750.png" alt="image" width={250} height={444} /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>图像超分</strong></p></td><td style={{ verticalAlign: "top" }}><p>模糊图像</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930151.png" alt="image" /></p></td><td style={{ verticalAlign: "top" }}><p>图像超分。</p></td><td style={{ verticalAlign: "top" }}><p>清晰图像</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930152.png" alt="image" /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>图像上色</strong></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930149.png" alt="image" /></p></td><td style={{ verticalAlign: "top" }}><p>蓝色背景，黄色的叶子。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930150.png" alt="image" /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>线稿生图</strong></p></td><td style={{ verticalAlign: "top" }}><p>输入图像</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930155.png" alt="image" /></p></td><td style={{ verticalAlign: "top" }}><p>北欧极简风格的客厅。</p></td><td style={{ verticalAlign: "top" }}><p>提取原图的线稿并生成图像</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930156.png" alt="image" /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>参考卡通形象生图</strong></p></td><td style={{ verticalAlign: "top" }}><p>输入参考图（卡通形象）</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930153.png" alt="image" /></p></td><td style={{ verticalAlign: "top" }}><p>卡通形象小心翼翼地探出头，窥视着房间内一颗璀璨的蓝色宝石。</p></td><td style={{ verticalAlign: "top" }}><p>输出图像</p> <p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9206061571/p930154.png" alt="image" /></p></td></tr></tbody></table>

## 前提条件 <span id="0e41425c577u4" />

万相-通用图像编辑API支持通过HTTP和DashScope SDK进行调用。

在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。目前，该SDK已支持Python和Java。

> 请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="42703589880ts" />

图像模型处理时间较长，为了避免请求超时，HTTP调用仅支持异步获取模型结果。您需要发起两个请求：

1. <strong>创建任务获取任务ID</strong>：首先发起创建任务请求，该请求会返回任务ID（task\_id）。
2. <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询任务状态及结果。任务成功执行时将返回图像URL，有效期24小时。

<Note>
  创建任务后，该任务将被加入到排队队列，等待调度执行。后续需要调用“根据任务ID查询结果接口”获取任务状态及结果。
</Note>

> 通用图像编辑模型大约需要5-15秒。实际耗时取决于排队任务数量和网络状况，请您在获取结果时耐心等待。

### 步骤1：创建任务获取任务ID <span id="db8cf70432oe3" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

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

        模型名称。示例值：wanx2.1-imageedit。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong>`string`<strong>（必选）</strong>

          提示词，用来描述生成图像中期望包含的元素和视觉特点。

          支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

          > 不同功能的提示词存在差异，建议根据具体功能参考相应的技巧说明。

          <strong>function</strong>`string`<strong>（必选）</strong>

          图像编辑功能。目前支持的功能有：

          - stylization\_all：全局风格化，当前支持2种风格。[风格和提示词技巧](/zh/model-studio/wanx-image-edit#d058c678a9iqt)
          - stylization\_local：局部风格化，当前支持8种风格。[风格和提示词技巧](/zh/model-studio/wanx-image-edit#b65daaa5cb1c5)
          - description\_edit：指令编辑。通过指令即可编辑图像，简单编辑任务优先推荐这种方式。[提示词技巧](/zh/model-studio/wanx-image-edit#0c932e6efebf7)
          - description\_edit\_with\_mask：局部重绘。需要指定编辑区域，适合对编辑范围有精确控制的场景。[提示词技巧](/zh/model-studio/wanx-image-edit#b0d2188736q4f)
          - remove\_watermark：去文字水印。[提示词技巧](/zh/model-studio/wanx-image-edit#97cea0ec36xff)
          - expand：扩图。[提示词技巧](/zh/model-studio/wanx-image-edit#42a62bb11bx9n)
          - super\_resolution：图像超分。[提示词技巧](/zh/model-studio/wanx-image-edit#04e73f5e9d728)
          - colorization：图像上色。[提示词技巧](/zh/model-studio/wanx-image-edit#dad8aa53ee8ng)
          - doodle：线稿生图。[提示词技巧](/zh/model-studio/wanx-image-edit#aec91c51deqld)
          - control\_cartoon\_feature：参考卡通形象生图。[提示词技巧](/zh/model-studio/wanx-image-edit#fade6eec1ctqa)

          <strong>base\_image\_url</strong> `string` <strong>（必选）</strong>

          输入图像的URL或 Base64 编码数据。

          图像限制：

          - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP。
          - 图像分辨率：图像的宽度和高度范围为\[512, 4096]像素。
          - 图像大小：不超过10MB。
          - URL地址中不能包含中文字符。

          输入图像说明：

          1. 使用公网可访问URL

             - 支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。
             - 示例值：`http://wanx.alicdn.com/material/20250318/stylization_all_1.jpeg`。
          2. 传入 Base64 编码图像后的字符串

             - 数据格式：`data:{MIME_type};base64,{base64_data}`。
             - 示例值：`data:image/jpeg;base64,GDU7MtCZzEbTbmRZ......`。
             - 示例中的编码字符串不完整，仅做演示，更多内容请参见[传值方式](/zh/model-studio/wanx-image-edit-api-reference#c92c9077cbebb)。

          <strong>mask\_image\_url</strong> `string` （可选）

          仅当`function`设置为`description_edit_with_mask`（局部重绘）时必填，其余情况无需填写。

          涂抹区域图像的URL或 Base64 编码数据。

          支持传入公网可访问的 URL（HTTP/HTTPS）或 Base64 编码字符串，更多内容请参见[传值方式](/zh/model-studio/wanx-image-edit-api-reference#c92c9077cbebb)。

          涂抹区域图像要求：

          - 图像分辨率 ：必须与`base_image_url`的图像分辨率保持一致。图像宽度和高度需在\[512, 4096]像素之间。
          - 图像格式 ：支持JPG、JPEG、PNG、BMP、TIFF、WEBP。
          - 图像大小 ：不超过10MB。
          - URL地址中不能包含中文字符。

          涂抹区域颜色要求：

          - 白色区域 ：表示需要编辑的部分，必须使用纯白色（RGB值为\[255,255,255]），否则可能无法正确识别。
          - 黑色区域：表示无需改变的部分，必须使用纯黑色（RGB值为\[0,0,0]），否则可能无法正确识别。

          关于如何获取涂抹区域图像：使用PS抠图或其他工具生成黑白涂抹图像。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <Tabs>
            <Tab title="通用">
              <strong>n</strong> `integer` （可选）

              生成图片的数量。范围1\~4，默认1。

              <strong>seed</strong>`integer`（可选）

              随机数种子，用于控制生成内容的随机性。取值范围`[0, 2147483647]`，未指定时自动生成；使用相同值可保持结果稳定。

              <strong>watermark</strong> `bool` （可选）

              是否添加水印标识，水印位于图片右下角，文案为“AI生成”。

              - false：默认值，不添加水印。
              - true：添加水印。
            </Tab>

            <Tab title="全局风格化">
              <strong>n</strong>、<strong>seed</strong>、<strong>watermark</strong>

              说明见通用参数。

              <strong>strength</strong> `float` （可选）

              当`function`设置为 `stylization_all`（全局风格化）时填写。

              图像修改幅度。范围\[0.0, 1.0]，默认0.5。

              值越接近0，则越接近原图效果；值越接近1，对原图的修改幅度越大。
            </Tab>

            <Tab title="指令编辑">
              <strong>n</strong>、<strong>seed</strong>、<strong>watermark</strong>

              说明见通用参数。

              <strong>strength</strong> `float` （可选）

              当`function`设置为`description_edit`（指令编辑）时填写。

              图像修改幅度。范围\[0.0, 1.0]，默认0.5。

              值越接近0，则越接近原图效果；值越接近1，对原图的修改幅度越大。
            </Tab>

            <Tab title="扩图">
              <strong>n</strong>、<strong>seed</strong>、<strong>watermark</strong>

              说明见通用参数。

              <strong>top\_scale</strong> `float`（可选）

              当`function`设置为`expand`（扩图）时才需填写。

              向上扩展比例，默认1.0，范围\[1.0, 2.0]。

              <strong>bottom\_scale</strong> `float`（可选）

              当`function`设置为`expand`（扩图）时才需填写。

              向下扩展比例，默认1.0，范围\[1.0, 2.0]。

              <strong>left\_scale</strong> `float`（可选）

              当`function`设置为`expand`（扩图）时才需填写。

              向左扩展比例，默认1.0，范围\[1.0, 2.0]。

              <strong>right\_scale</strong> `float`（可选）

              当`function`设置为`expand`（扩图）时才需填写。

              向右扩展比例，默认1.0，范围\[1.0, 2.0]。
            </Tab>

            <Tab title="图像超分">
              <strong>n</strong>、<strong>seed</strong>、<strong>watermark</strong>

              说明见通用参数。

              <strong>upscale\_factor</strong> `integer`（可选）

              当`function`设置为`super_resolution`（图像超分）时才需填写。

              图像超分的放大倍数。在放大图像的同时增强细节，提升图像分辨率，实现高清处理。

              范围1\~4，默认1。当设置为1时，仅进行高清处理，不放大。
            </Tab>

            <Tab title="线稿生图">
              <strong>n</strong>、<strong>seed</strong>、<strong>watermark</strong>

              说明见通用参数。

              <strong>is\_sketch</strong> `bool` （可选）

              当`function`设置为`doodle`（线稿生图）时才需填写。

              输入图像是否为线稿图像。

              - false：默认值，输入图像不为线稿图像。模型会先从输入图像中提取线稿，再参考提取的线稿生成新的图像。
              - true：输入图像为线稿图像。模型将直接基于输入图像生成图像，适用于涂鸦作画场景。
            </Tab>
          </Tabs>
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="全局风格化">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "stylization_all",
                "prompt": "转换成法国绘本风格",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/stylization_all_1.jpeg"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="传入本地文件（base64）">
            下面以全局风格化为例，展示Base64传参示例。

            由于 Base64 编码后的字符串较长，请下载[image\_base64](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250722/lzyoev/stylization_image_base64.txt)，并将文本内容全部复制到`base_image_url`参数中。

            数据格式详见[传值方式](/zh/model-studio/wanx-image-edit-api-reference#c92c9077cbebb)。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "stylization_all",
                "prompt": "转换成法国绘本风格",
                "base_image_url": "data:image/jpeg;base64,/9j/4AAQSkZJR......"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="局部风格化">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "stylization_local",
                "prompt": "把房子变成木板风格。",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/stylization_local_1.png"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="指令编辑">
            无需指定区域，通过指令即可增加/修改图片内容。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "description_edit",
                "prompt": "把她的头发修改为红色。",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/description_edit_2.png"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="局部重绘">
            针对指定区域,可增加/删除/修改图片内容。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "description_edit_with_mask",
                "prompt": "陶瓷兔子拿着陶瓷小花。",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3.jpeg",
                "mask_image_url": "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3_mask.png"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="去文字水印">
            可去除中英文文字水印。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "remove_watermark",
                "prompt": "去除图像中的文字",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/remove_watermark_1.png"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="扩图">
            支持在上下左右四个方向按比例扩图。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "expand",
                "prompt": "一位绿色仙子",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/expand_2.jpg"
              },
              "parameters": {
                "top_scale": 1.5,
                "bottom_scale": 1.5,
                "left_scale": 1.5,
                "right_scale": 1.5,
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="图像超分">
            支持将模糊图像进行高清放大。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "super_resolution",
                "prompt": "图像超分。",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/super_resolution_1.jpeg"
              },
              "parameters": {
                "upscale_factor": 2,
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="图像上色">
            支持将黑白或灰度图像转为彩色图像。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "colorization",
                "prompt": "蓝色背景，黄色的叶子。",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/colorization_1.jpeg"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="线稿生图">
            先提取输入图像的线稿，再参考该线稿生成图像。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "doodle",
                "prompt": "北欧极简风格的客厅。",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/doodle_1.png"
              },
              "parameters": {
                "n": 1
              }
            }'
            ```
          </Tab>

          <Tab title="参考卡通形象生图">
            支持参考卡通形象生成图像。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx2.1-imageedit",
              "input": {
                "function": "control_cartoon_feature",
                "prompt": "卡通形象小心翼翼地探出头，窥视着房间内一颗璀璨的蓝色宝石。",
                "base_image_url": "http://wanx.alicdn.com/material/20250318/control_cartoon_feature_1.png"
              },
              "parameters": {
                "n": 1
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

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="eb9c8d77b6xg8" />

        ##### 请求头（Headers） <span id="36a28b657f196" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="0e29e015d8opn" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            请将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id。

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
        #### 响应参数 <span id="f277d7425foyq" />

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

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>results</strong> `array object`

          任务结果列表，包括图像URL、部分任务执行失败报错信息等。

          <Accordion title="数据结构">
            ```json
            {
                "results": [
                    {
                        "url": ""
                    },
                    {
                        "code": "",
                        "message": ""
                    }
                ]
            }
            ```
          </Accordion>

          <strong>task\_metrics</strong> `object`

          任务结果统计。

          <Accordion title="属性" defaultOpen>
            <strong>TOTAL</strong> `integer`

            总的任务数。

            <strong>SUCCEEDED</strong> `integer`

            任务状态为成功的任务数。

            <strong>FAILED</strong> `integer`

            任务状态为失败的任务数。
          </Accordion>

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          模型成功生成图片的数量。计费公式：费用 = 图片数量 × 单价。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json
            {
                "request_id": "eeef0935-02e9-9742-bb55-xxxxxx",
                "output": {
                    "task_id": "a425c46f-dc0a-400f-879e-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-02-21 17:56:31.786",
                    "scheduled_time": "2025-02-21 17:56:31.821",
                    "end_time": "2025-02-21 17:56:42.530",
                    "results": [
                        {
                            "url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/aaa.png"
                        }
                    ],
                    "task_metrics": {
                        "TOTAL": 1,
                        "SUCCEEDED": 1,
                        "FAILED": 0
                    }
                },
                "usage": {
                    "image_count": 1
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
                    "task_id": "86ecf553-d340-4e21-af6e-xxxxxx",
                    "task_status": "FAILED",
                    "code": "InvalidParameter",
                    "message": "xxxxxx",
                    "task_metrics": {
                        "TOTAL": 4,
                        "SUCCEEDED": 0,
                        "FAILED": 4
                    }
                }
            }
            ```
          </Tab>

          <Tab title="任务部分失败">
            模型可以在一次任务中生成多张图片。只要有一张图片生成成功，任务状态将标记为`SUCCEEDED`，并且返回相应的图像URL。对于生成失败的图片，结果中会返回相应的失败原因。同时在usage统计中，只会对成功的结果计数。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "85eaba38-0185-99d7-8d16-xxxxxx",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "results": [
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/123/a1.png"
                        },
                        {
                            "code": "InternalError.Timeout",
                            "message": "An internal timeout error has occured during execution, please try again later or contact service support."
                        }
                    ],
                    "task_metrics": {
                        "TOTAL": 2,
                        "SUCCEEDED": 1,
                        "FAILED": 1
                    }
                },
                "usage": {
                    "image_count": 1
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

请先确认已[安装最新版DashScope SDK](/zh/model-studio/install-sdk)，否则可能导致运行报错。

DashScope SDK目前已支持Python和Java。

SDK与HTTP接口的参数名基本一致，参数结构根据不同语言的SDK封装而定。参数说明可参考[HTTP调用](/zh/model-studio/legacy-image-to-video-api-reference#42703589880ts)。

由于视频模型处理时间较长，底层服务采用异步方式提供。SDK在上层进行了封装，支持同步、异步两种调用方式。

> 通用图像编辑模型大约需要5-15秒。实际耗时取决于排队任务数量和网络状况，请您在获取结果时耐心等待。

### Python SDK调用 <span id="a3ad9a3b6d9if" />

<Tip>
  <strong>本示例要求 dashscope Python SDK ≥ 1.23.8。</strong>低于该版本运行本示例时会报错 <strong>TypeError: got multiple values for keyword argument 'function'</strong>。请先执行 <strong>pip install -U "dashscope>=1.23.8"</strong> 升级 SDK 后再运行示例。
</Tip>

使用Python SDK处理图像文件时，支持以下三种方式输入图像。请根据您的场景选择其中一种即可。

1. 公网 URL：公网可访问的图像 URL（HTTP/HTTPS）。您可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。
2. Base64编码：传入Base64编码文件后的字符串，格式为`data:{MIME_type};base64,{base64_data}`。
3. 本地文件路径：支持传入文件的<strong>绝对路径和相对路径</strong>。请参考下表，传入正确的文件路径。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.63%" }} /><col style={{ width: "29.02%" }} /><col style={{ width: "26.52%" }} /><col style={{ width: "23.83%" }} /></colgroup><thead><tr><th><p><strong>系统</strong></p></th><th><p><strong>传入的文件路径</strong></p></th><th><p><strong>示例（绝对路径）</strong></p></th><th><p><strong>示例（相对路径）</strong></p></th></tr></thead><tbody><tr><td><p>Linux或macOS系统</p></td><td rowSpan={2}><p>file://\{文件的绝对路径或相对路径}</p></td><td><p>file:///home/images/test.png</p></td><td><p>file://./images/test.png</p></td></tr><tr><td><p>Windows系统</p></td><td><p>file://D:/images/test.png</p></td><td><p>file://./images/test.png</p></td></tr></tbody></table>

#### 示例代码 <span id="f2b1639e488og" />

<Note>
  在调用代码前，请确保已安装 DashScope Python SDK，推荐升级至最新版本：`pip install -U dashscope`，详见[安装SDK](/zh/model-studio/install-sdk)。
</Note>

<Tabs>
  <Tab title="同步调用">
    本示例展示同步调用方式，并支持三种图像输入方式：公网URL、Base64编码、本地文件路径。

    ##### 请求示例 <span id="3cd4969b00193" />

    ```python expandable
    import base64
    import os
    from http import HTTPStatus
    from dashscope import ImageSynthesis
    import dashscope
    import mimetypes

    """
    环境要求：
        dashscope python SDK >= 1.23.8
    安装/升级SDK:
        pip install -U dashscope
    """

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    api_key = os.getenv("DASHSCOPE_API_KEY")
    # 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

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
    mask_image_url = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3_mask.png"
    base_image_url = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3.jpeg"

    # 【方式二】使用本地文件（支持绝对路径和相对路径）
    # 格式要求：file:// + 文件路径
    # 示例（绝对路径）：
    # mask_image_url = "file://" + "/path/to/your/mask_image.png"     # Linux/macOS
    # base_image_url = "file://" + "C:/path/to/your/base_image.jpeg"  # Windows
    # 示例（相对路径）：
    # mask_image_url = "file://" + "./mask_image.png"                 # 以实际路径为准
    # base_image_url = "file://" + "./base_image.jpeg"                # 以实际路径为准

    # 【方式三】使用Base64编码的图片
    # mask_image_url = encode_file("./mask_image.png")               # 以实际路径为准
    # base_image_url = encode_file("./base_image.jpeg")              # 以实际路径为准

    def sample_sync_call_imageedit():
        print('please wait...')
        rsp = ImageSynthesis.call(api_key=api_key,
                                  model="wanx2.1-imageedit",
                                  function="description_edit_with_mask",
                                  prompt="陶瓷兔子拿着陶瓷小花",
                                  mask_image_url=mask_image_url,
                                  base_image_url=base_image_url,
                                  n=1)
        assert rsp.status_code == HTTPStatus.OK

        print('response: %s' % rsp)
        if rsp.status_code == HTTPStatus.OK:
            for result in rsp.output.results:
                print("---------------------------")
                print(result.url)
        else:
            print('sync_call Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_sync_call_imageedit()
    ```

    ##### 响应示例 <span id="0c0533477emwb" />

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "dc41682c-4e4a-9010-bc6f-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "6e319d88-a07a-420c-9493-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [
                {
                    "url": "https://dashscope-result-wlcb-acdr-1.oss-cn-wulanchabu-acdr-1.aliyuncs.com/xxx.png?xxxxxx"
                }
            ],
            "submit_time": "2025-05-26 14:58:27.320",
            "scheduled_time": "2025-05-26 14:58:27.339",
            "end_time": "2025-05-26 14:58:39.170",
            "task_metrics": {
                "TOTAL": 1,
                "SUCCEEDED": 1,
                "FAILED": 0
            }
        },
        "usage": {
            "image_count": 1
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    本示例仅展示异步调用方式。

    ##### 请求示例 <span id="b994ca9918t7e" />

    ```python expandable
    import os
    from http import HTTPStatus
    from dashscope import ImageSynthesis
    import dashscope

    """
    环境要求：
        dashscope python SDK >= 1.23.4
    安装/升级SDK:
        pip install -U dashscope
    """

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    api_key = os.getenv("DASHSCOPE_API_KEY")
    # 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

    # 使用公网图片 URL
    mask_image_url = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3_mask.png"
    base_image_url = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3.jpeg"

    def sample_async_call_imageedit():
        # 异步调用，返回一个task_id
        rsp = ImageSynthesis.async_call(api_key=api_key,
                                        model="wanx2.1-imageedit",
                                        function="description_edit_with_mask",
                                        prompt="陶瓷兔子拿着陶瓷小花",
                                        mask_image_url=mask_image_url,
                                        base_image_url=base_image_url,
                                        n=1)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print("task_id: %s" % rsp.output.task_id)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

        # 获取异步任务信息
        status = ImageSynthesis.fetch(task=rsp, api_key=api_key)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)  # check the task status
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

        # 等待异步任务结束
        rsp = ImageSynthesis.wait(rsp)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output)
            for result in rsp.output.results:
                print("---------------------------")
                print(result.url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_async_call_imageedit()
    ```

    ##### 响应示例 <span id="ae28dc8f72g6i" />

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

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "dc41682c-4e4a-9010-bc6f-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "6e319d88-a07a-420c-9493-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [
                {
                    "url": "https://dashscope-result-wlcb-acdr-1.oss-cn-wulanchabu-acdr-1.aliyuncs.com/xxx.png?Expires=17xxxxxx"
                }
            ],
            "submit_time": "2025-05-26 14:58:27.320",
            "scheduled_time": "2025-05-26 14:58:27.339",
            "end_time": "2025-05-26 14:58:39.170",
            "task_metrics": {
                "TOTAL": 1,
                "SUCCEEDED": 1,
                "FAILED": 0
            }
        },
        "usage": {
            "image_count": 1
        }
    }
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="589b80853e6rn" />

使用Java SDK处理图像文件时，支持以下三种方式输入图像。请根据您的场景选择其中一种即可。

1. 公网 URL：公网可访问的图像 URL（HTTP/HTTPS）。您可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。
2. Base 64编码：传入Base64编码文件后的字符串，格式为`data:{MIME_type};base64,{base64_data}`。
3. 本地文件路径：仅支持传入文件的<strong>绝对路径</strong>。请参考下表，传入正确的文件路径。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "31.87%" }} /><col style={{ width: "36.88%" }} /><col style={{ width: "31.25%" }} /></colgroup><thead><tr><th><p><strong>系统</strong></p></th><th><p><strong>传入的文件路径</strong></p></th><th><p><strong>示例</strong></p></th></tr></thead><tbody><tr><td><p>Linux或macOS系统</p></td><td><p>file://\{文件的绝对路径}</p></td><td><p>file:///home/images/test.png</p></td></tr><tr><td><p>Windows系统</p></td><td><p>file:///\{文件的绝对路径}</p></td><td><p>file:///D:/images/test.png</p></td></tr></tbody></table>

#### 示例代码 <span id="ae42e9ea5chzu" />

<Note>
  在调用代码前，请确保已安装 DashScope Java SDK。推荐您升级至最新版本，详见[安装SDK](/zh/model-studio/install-sdk)。
</Note>

<Tabs>
  <Tab title="同步调用">
    本示例展示同步调用方式，并支持三种图像输入方式：公网URL、Base64编码、本地文件路径。

    ##### 请求示例 <span id="36e8ee89107qx" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.JsonUtils;
    import com.alibaba.dashscope.utils.Constants;

    import java.io.IOException;
    import java.nio.file.Files;
    import java.nio.file.Path;
    import java.nio.file.Paths;
    import java.util.Base64;
    import java.util.HashMap;
    import java.util.Map;

    /**
     * 环境要求
     *      dashscope java SDK >=2.20.9
     * 更新maven依赖:
     *      https://mvnrepository.com/artifact/com.alibaba/dashscope-sdk-java
     */

    public class ImageEditSync {
        static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * 图像输入方式说明：三选一即可
         *
         * 1. 使用公网URL - 适合已有公开可访问的图片
         * 2. 使用本地文件 - 适合本地开发测试
         * 3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
         */

        //【方式一】公网URL
        static String maskImageUrl = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3_mask.png";
        static String baseImageUrl = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3.jpeg";

        //【方式二】本地文件路径（file://+绝对路径 or file:///+绝对路径）
        // static String maskImageUrl = "file://" + "/your/path/to/mask_image.png";    // Linux/macOS
        // static String baseImageUrl = "file:///" + "C:/your/path/to/base_image.png";  // Windows

        //【方式三】Base64编码
        // static String maskImageUrl = encodeFile("/your/path/to/mask_image.png");
        // static String baseImageUrl = encodeFile("/your/path/to/base_image.png");

        public static void syncCall() {
            // 设置parameters参数
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);

            ImageSynthesisParam param =
                    ImageSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wanx2.1-imageedit")
                            .function(ImageSynthesis.ImageEditFunction.DESCRIPTION_EDIT_WITH_MASK)
                            .prompt("陶瓷兔子拿着陶瓷小花")
                            .maskImageUrl(maskImageUrl)
                            .baseImageUrl(baseImageUrl)
                            .n(1)
                            .size("1024*1024")
                            .parameters(parameters)
                            .build();

            ImageSynthesis imageSynthesis = new ImageSynthesis();
            ImageSynthesisResult result = null;
            try {
                System.out.println("---sync call, please wait a moment----");
                result = imageSynthesis.call(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));
        }

        /**
         * 将文件编码为Base64字符串
         * @param filePath 文件路径
         * @return Base64字符串，格式为 data:{MIME_type};base64,{base64_data}
         */
        public static String encodeFile(String filePath) {
            Path path = Paths.get(filePath);
            if (!Files.exists(path)) {
                throw new IllegalArgumentException("文件不存在: " + filePath);
            }
            // 检测MIME类型
            String mimeType = null;
            try {
                mimeType = Files.probeContentType(path);
            } catch (IOException e) {
                throw new IllegalArgumentException("无法检测文件类型: " + filePath);
            }
            if (mimeType == null || !mimeType.startsWith("image/")) {
                throw new IllegalArgumentException("不支持或无法识别的图像格式");
            }
            // 读取文件内容并编码
            byte[] fileBytes = null;
            try{
                fileBytes = Files.readAllBytes(path);
            } catch (IOException e) {
                throw new IllegalArgumentException("无法读取文件内容: " + filePath);
            }

            String encodedString = Base64.getEncoder().encodeToString(fileBytes);
            return "data:" + mimeType + ";base64," + encodedString;
        }

        public static void main(String[] args) {
            syncCall();
        }
    }
    ```

    ##### 响应示例 <span id="75c2320e48eer" />

    > url 有效期24小时，请及时下载图像。

    ```json
    {
        "request_id": "bf6c6361-f0fc-949c-9d60-xxxxxx",
        "output": {
            "task_id": "958db858-153b-4c81-b243-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [
                {
                    "url": "https://dashscope-result-wlcb-acdr-1.oss-cn-wulanchabu-acdr-1.aliyuncs.com/xxx.png?xxxxxx"
                }
            ],
            "task_metrics": {
                "TOTAL": 1,
                "SUCCEEDED": 1,
                "FAILED": 0
            }
        },
        "usage": {
            "image_count": 1
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    本示例仅展示异步调用方式。

    ##### 请求示例 <span id="49cd0bd3b7x31" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisListResult;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.task.AsyncTaskListParam;
    import com.alibaba.dashscope.utils.JsonUtils;
    import com.alibaba.dashscope.utils.Constants;

    import java.util.HashMap;
    import java.util.Map;

    /**
     * 环境要求
     *      dashscope java SDK >= 2.20.1
     * 更新maven依赖:
     *      https://mvnrepository.com/artifact/com.alibaba/dashscope-sdk-java
     */

    public class ImageEditAsync {
        static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        //【方式一】公网URL
        static String maskImageUrl = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3_mask.png";
        static String baseImageUrl = "http://wanx.alicdn.com/material/20250318/description_edit_with_mask_3.jpeg";

        public static void asyncCall() {
            // 设置parameters参数
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);

            ImageSynthesisParam param =
                    ImageSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wanx2.1-imageedit")
                            .function(ImageSynthesis.ImageEditFunction.DESCRIPTION_EDIT_WITH_MASK)
                            .prompt("陶瓷兔子拿着陶瓷小花")
                            .maskImageUrl(maskImageUrl)
                            .baseImageUrl(baseImageUrl)
                            .n(1)
                            .size("1024*1024")
                            .parameters(parameters)
                            .build();
            ImageSynthesis imageSynthesis = new ImageSynthesis();
            ImageSynthesisResult result = null;
            try {
                System.out.println("---async call, please wait a moment----");
                result = imageSynthesis.asyncCall(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }

            System.out.println(JsonUtils.toJson(result));

            String taskId = result.getOutput().getTaskId();

            System.out.println("taskId=" + taskId);

            try {
                result = imageSynthesis.wait(taskId, apiKey);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));
            System.out.println(JsonUtils.toJson(result.getOutput()));
        }

        public static void listTask() throws ApiException, NoApiKeyException {
            ImageSynthesis is = new ImageSynthesis();
            AsyncTaskListParam param = AsyncTaskListParam.builder().build();
            param.setApiKey(apiKey);
            ImageSynthesisListResult result = is.list(param);
            System.out.println(result);
        }

        public void fetchTask(String taskId) throws ApiException, NoApiKeyException {
            ImageSynthesis is = new ImageSynthesis();
            // 如果已设置 DASHSCOPE_API_KEY 为环境变量，apiKey 可为空。
            ImageSynthesisResult result = is.fetch(taskId, apiKey);
            System.out.println(result.getOutput());
            System.out.println(result.getUsage());
        }

        public static void main(String[] args) {
            asyncCall();
        }
    }
    ```

    ##### 响应示例 <span id="13d30c3a8eq42" />

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

    > url 有效期24小时，请及时下载图像。

    ```json
    {
    	"request_id": "3d740fc4-a968-9c36-b0e7-xxxxxxxx",
    	"output": {
    		"task_id": "34dcf4b0-ed84-441e-91cb-xxxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [
    			{
    				"url": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxx.png"
    			}
    		],
    		"submit_time": "2025-02-21 17:56:31.786",
    		"scheduled_time": "2025-02-21 17:56:31.821",
    		"end_time": "2025-02-21 17:56:42.530",
    		"task_metrics": {
    			"TOTAL": 1,
    			"SUCCEEDED": 1,
    			"FAILED": 0
    		}
    	},
    	"usage": {
    		"image_count": 1
    	}
    }
    ```
  </Tab>
</Tabs>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

此API还有特定状态码，具体如下所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.799448%" }} /><col style={{ width: "20.975161%" }} /><col style={{ width: "28.702852%" }} /><col style={{ width: "36.522539%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>InvalidParameter</p></td><td><p>请求参数不合法。</p></td></tr><tr><td><p>400</p></td><td><p>IPInfringementSuspect</p></td><td><p>Input data is suspected of being involved in IP infringement.</p></td><td><p>输入数据（如提示词或图像）涉嫌知识产权侵权。请检查输入，确保不包含引发侵权风险的内容。</p></td></tr><tr><td><p>400</p></td><td><p>DataInspectionFailed</p></td><td><p>Input data may contain inappropriate content.</p></td><td><p>输入数据（如提示词或图像）可能包含敏感内容。请修改输入后重试。</p></td></tr><tr><td><p>500</p></td><td><p>InternalError</p></td><td><p>InternalError</p></td><td><p>服务异常。请先尝试重试，排除偶发情况。</p></td></tr></tbody></table>

## 输入图像说明 <span id="b5b5df5ee6g5s" />

### 传值方式 <span id="c92c9077cbebb" />

输入图像支持多种字符串格式，不同调用方式的支持情况如下表所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /><col style={{ width: "25%" }} /></colgroup><thead><tr><th><p><strong>调用方式</strong></p></th><th><p><strong>HTTP</strong></p></th><th><p><strong>Python SDK</strong></p></th><th><p><strong>Java SDK</strong></p></th></tr></thead><tbody><tr><td><p>支持的输入图像方式</p></td><td><ul><li><p>公网URL</p></li> <li><p>Base64编码</p></li></ul></td><td><ul><li><p>公网URL</p></li> <li><p>Base64编码</p></li> <li><p>本地文件路径</p></li></ul></td><td><ul><li><p>公网URL</p></li> <li><p>Base64编码</p></li> <li><p>本地文件路径</p></li></ul></td></tr></tbody></table>

<strong>方式一：使用公网URL</strong>

- 提供一个公网可访问的图像地址，支持 HTTP 或 HTTPS 协议。请在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。
- 示例值：`https://xxxx/img.png`。

<strong>方式二：使用Base64编码</strong>

将本地图片文件转换为 Base64 格式的字符串，并根据`data:{MIME_type};base64,{base64_data}`格式拼接成完整的字符串。

- 转换代码请参见[示例代码](/zh/model-studio/wanx-image-edit-api-reference#f2b1639e488og)。
- \{MIME\_type}：图像的媒体类型，需与文件格式对应。
- \{base64\_data}：图像文件经过 Base64 编码后的字符串。
- MIME 类型对应关系：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>图像格式</strong></p></th><th><p><strong>MIME Type</strong></p></th></tr></thead><tbody><tr><td><p>JPEG</p></td><td><p>image/jpeg</p></td></tr><tr><td><p>JPG</p></td><td><p>image/jpeg</p></td></tr><tr><td><p>PNG</p></td><td><p>image/png</p></td></tr><tr><td><p>BMP</p></td><td><p>image/bmp</p></td></tr><tr><td><p>TIFF</p></td><td><p>image/tiff</p></td></tr><tr><td><p>WEBP</p></td><td><p>image/webp</p></td></tr></tbody></table>
- 示例值<strong>：</strong>`"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAABDg......"`<strong>。</strong>

  注意：为便于展示，上述 Base64 字符串是截断的。在实际使用中，请务必传入完整的编码字符串。

<strong>方式三：本地文件路径</strong>

- HTTP调用不支持本地文件路径，仅Python SDK和Java SDK支持。
- 关于本地文件路径的传入规则，请参见[Python SDK调用](/zh/model-studio/wanx-image-edit-api-reference#a3ad9a3b6d9if)和[Java SDK调用](/zh/model-studio/wanx-image-edit-api-reference#589b80853e6rn)。

## 常见问题 <span id="17fdffba319en" />

图像模型的通用问题请参见[常见问题](/zh/model-studio/image-faq)文档，包括以下内容：模型计费与限流规则、接口高频报错解决方法等。
