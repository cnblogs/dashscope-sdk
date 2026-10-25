> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 千问-图像编辑API参考

> 千问-图像编辑模型支持多图输入和多图输出，可精确修改图内文字、增删或移动物体、改变主体动作、迁移图片风格及增强画面细节。

<meta property="og:title" content="千问-图像编辑API参考" />

<table><tbody><tr><td><p><strong>快速入口：</strong><Link href="/zh/model-studio/qwen-image-edit-guide">使用指南</Link> | <Link href="https://qwen.ai/blog?id=1675c295dc29dd31073e5b3f72876e9d684e41c6&from=research.research-list">技术博客</Link> | <Link href="https://bailian.console.aliyun.com/model/experience/vision/imageGenerate?modelId=qwen-image-edit-3.0-pro">在线体验</Link></p></td></tr></tbody></table>

## 模型概览 <span id="9809c6c428y0y" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "31.36%" }} /><col style={{ width: "45.16%" }} /><col style={{ width: "23.48%" }} /></colgroup><thead><tr><th><p><strong>提示词</strong></p></th><th><p><strong>输入图</strong></p></th><th><p><strong>输出图像</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>在画面右下角石板路旁、靠近树干根部的位置，以浅灰墨色手写体题写一首七言绝句，字体为行楷风格，笔触自然流畅、略带飞白，大小适中（约占画面高度1/10），与整体水墨淡雅氛围协调。诗文内容为：“青石桥畔柳风轻， 素手拈花闭目听。 一水碧痕浮旧梦， 半篙烟雨入空舲。”诗句横向排列，四句分两行书写（前两句一行，后两句一行），末句“舲”字右下角钤一枚朱红小印，印文为“江南”二字篆书，尺寸约等于单字高度的1/3。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3279833771/p1058430.webp" alt="image (18)-2026-03-10-16-39-59" width={250} height={250} /></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3279833771/p1058432.png" alt="image (19)" width={250} height={250} /></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>使用图一的城市照片作为底图。请勿更改照片中的真实建筑、街道、车辆或人物。保持照片的真实性。三个图二中的卡通形象在建筑物周围，一个趴在建筑物上方，一个从建筑物的右边探出头来，一个坐在建筑物前的空地上。该形象应采用扁平化的图形风格绘制，轮廓清晰，类似于壁画或海报插图。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3279833771/p1058424.webp" alt="image (15)-combine" width={625.438} height={250} /></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3279833771/p1058425.png" alt="image (17)" width={250} height={250} /></p></td></tr></tbody></table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24%" }} />

    <col style={{ width: "38%" }} />

    <col style={{ width: "38%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>模型名称</strong>
      </th>

      <th>
        <strong>模型简介</strong>
      </th>

      <th>
        <strong>输出图像规格</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        qwen-image-2.0-pro `推荐`

        > 当前与qwen-image-2.0-pro-2026-04-22能力相同
      </td>

      <td rowSpan={4}>
        千问图像生成与编辑模型Pro系列。文字渲染、真实质感、语义遵循能力更强。

        > 图像生成请参考[千问-文生图](/zh/model-studio/qwen-image-api)。
      </td>

      <td rowSpan={6}>
        图像分辨率：

        - <strong>可指定</strong>：图像总像素需在512*512至2048*2048之间。
        - <strong>默认</strong>：总像素数接近 1024\*1024，宽高比与输入图（多图输入时为最后一张）相近。

        图像格式：png

        图像张数：1-6张
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-pro-2026-06-22 `推荐`
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-pro-2026-04-22
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-pro-2026-03-03
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0 `推荐`

        > 当前与qwen-image-2.0-2026-03-03能力相同
      </td>

      <td rowSpan={2}>
        千问图像生成与编辑模型加速版，兼顾效果与响应速度。

        > 图像生成请参考[千问-文生图](/zh/model-studio/qwen-image-api)。
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-2026-03-03 `推荐`
      </td>
    </tr>
  </tbody>
</table>

各地域支持的模型请参见[百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)。

## 前提条件 <span id="45139ce723d8f" />

在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。目前，该SDK已支持Python和Java。

> 请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

<Tip>
  华北2（北京）和新加坡地域拥有独立的 <strong>API Key</strong>与<strong>请求地址</strong>，不可混用，跨地域调用将导致鉴权失败或服务报错。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP调用 <span id="42703589880ts" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

<strong>新加坡地域</strong>：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

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

        ##### 请求体（Request Body） <span id="45d1fa2260zn6" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，示例值qwen-image-2.0-pro。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入参数对象，包含以下字段：

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。<strong>当前仅支持单轮对话</strong>，因此数组内<strong>有且只有一个对象</strong>，该对象包含`role`和`content`两个属性。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息发送者角色，必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容，包含1-3张图像，格式为 `{"image": "..."}`；以及单个编辑指令，格式为 `{"text": "..."}`。

            <Accordion title="属性" defaultOpen>
              <strong>image</strong> `string` <strong>（必选）</strong>

              输入图像的 URL 或 Base64 编码数据。支持传入1-3张图像。

              多图输入时，按照数组顺序定义图像顺序，输出图像的比例以最后一张为准。

              <strong>图像要求：</strong>

              - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP和GIF。

                > 输出图像为PNG格式，对于GIF动图，仅处理其第一帧。
              - 图像分辨率：为获得最佳效果，建议图像的宽和高均在384像素至3072像素之间。分辨率过低可能导致生成效果模糊，过高则会增加处理时长。
              - 图像大小：不超过10MB。

              <strong>支持的输入格式</strong>

              1. 公网URL：

                 - 支持 HTTP 和 HTTPS 协议。
                 - 示例值：`https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250925/fpakfo/image36.webp`。
              2. 临时URL：

                 - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                 - 示例值：`oss://dashscope-instant/xxx/2024-07-18/xxx/cat.png`。
              3. 传入 Base64 编码图像后的字符串

                 - 示例值：`data:image/jpeg;base64,GDU7MtCZz...`（示例已截断，仅做演示）
                 - Base64 编码规范请参见[通过Base64编码传入图片](/zh/model-studio/qwen-image-edit-api)。

              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词用于描述您期望生成的图像内容、风格和构图。

              支持中英文，qwen-image-2.0系列模型长度上限为 1300 Token，超出部分将自动截断。

              <strong>注意</strong>：仅支持传入一个text，不传或传入多个将报错。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        控制图像生成的附加参数。

        <Accordion title="属性" defaultOpen>
          <strong>n</strong> `integer` （可选）

          输出图像的数量，默认值为1。

          对于qwen-image-2.0系列模型，可选择输出1-6张图片。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在画面中看到的内容，可以对画面进行限制。

          支持中英文，长度上限500个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>size</strong> `string` （可选）

          设置输出图像的分辨率，格式为`宽*高`，例如`"1024*1536"`。

          <strong>qwen-image-2.0系列模型</strong>：

          - 图像总像素需在512*512至2048*2048之间。
          - 默认总像素数接近 `1024*1024`，宽高比与输入图（多图输入时为最后一张）相近。

          > 指定 `size` 参数，系统会以 `size`指定的宽高为目标，将实际输出图像的宽高调整为最接近的16的倍数。例如，设置`1033*1032`，输出图像尺寸为`1040*1024`。

          <Accordion title="常见比例推荐分辨率" defaultOpen>
            - 1:1: 1024*1024、1536*1536
            - 2:3: 768*1152、1024*1536
            - 3:2: 1152*768、1536*1024
            - 3:4: 960*1280、1080*1440
            - 4:3: 1280*960、1440*1080
            - 9:16: 720*1280、1080*1920
            - 16:9: 1280*720、1920*1080
            - 21:9: 1344*576、2048*872
          </Accordion>

          <strong>支持模型</strong>：所有模型。

          <strong>prompt\_extend</strong> `boolean` （可选）

          是否开启提示词智能改写，默认值为 `true`。开启后，模型会优化正向提示词（`text`），对描述较简单的提示词效果提升明显。

          <strong>支持模型</strong>：所有模型。

          <strong>watermark</strong> `boolean` （可选）

          是否在图像右下角添加 "Qwen-Image" 水印。默认值为 `false`。

          <strong>seed</strong> `integer` （可选）

          随机数种子，取值范围`[0,2147483647]`。

          使用相同的`seed`参数值可使生成内容保持相对稳定。若不提供，算法将自动使用随机数种子。

          <strong>注意</strong>：模型生成过程具有概率性，即使使用相同的`seed`，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```bash 单图编辑 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --data '{
              "model": "qwen-image-2.0-pro",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/jiydyi/image+%2818%29-2026-03-10-16-39-59.webp"
                              },
                              {
                                  "text": "在画面右下角石板路旁、靠近树干根部的位置，以浅灰墨色手写体题写一首七言绝句，字体为行楷风格，笔触自然流畅、略带飞白，大小适中（约占画面高度1/10），与整体水墨淡雅氛围协调。诗文内容为：“青石桥畔柳风轻， 素手拈花闭目听。 一水碧痕浮旧梦， 半篙烟雨入空舲。”诗句横向排列，四句分两行书写（前两句一行，后两句一行），末句“舲”字右下角钤一枚朱红小印，印文为“江南”二字篆书，尺寸约等于单字高度的1/3。"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "n": 1,
                  "negative_prompt": " ",
                  "prompt_extend": true,
                  "watermark": false,
                  "size": "2048*2048"
              }
          }'
          ```

          ```bash 多图融合 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --data '{
              "model": "qwen-image-2.0-pro",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/rdsgaa/image+%2815%29.png"
                              },
                              {
                                  "image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/qokhtl/image+%2816%29.png"
                              },
                              {
                                  "text": "使用图一的城市照片作为底图。请勿更改照片中的真实建筑、街道、车辆或人物。保持照片的真实性。三个图二中的卡通形象在建筑物周围，一个趴在建筑物上方，一个从建筑物的右边探出头来，一个坐在建筑物前的空地上。该形象应采用扁平化的图形风格绘制，轮廓清晰，类似于壁画或海报插图。"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "n": 1,
                  "negative_prompt": " ",
                  "prompt_extend": true,
                  "watermark": false,
                  "size": "2048*2048"
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
        #### 响应参数 <span id="b7e833ee1f962" />

        <strong>output</strong> `object`

        包含模型生成结果。

        <Accordion title="属性" defaultOpen>
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

        本次调用的资源使用情况，仅调用成功时返回。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的张数。

          <strong>width</strong> `integer`

          生成图像的宽度（像素）。

          <strong>height</strong> `integer`

          生成图像的高度（像素）。
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

            ```json
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
                    "height": 2048,
                    "image_count": 1,
                    "width": 2048
                },
                "request_id": "571ae02f-5c9d-436c-83c2-f221e6df0xxx"
            }
            ```
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

## DashScope SDK调用 <span id="db17b3098c2iv" />

SDK 的参数命名与[HTTP接口](/zh/model-studio/qwen-image-edit-api#42703589880ts)基本一致，参数结构根据语言特性进行封装，完整参数列表请参见[千问 API 参考](/zh/model-studio/qwen-api-reference)。

### Python SDK调用 <span id="a3ad9a3b6d9if" />

<Note>
  - 推荐安装最新版DashScope Python SDK，否则可能运行报错：[安装或升级SDK](/zh/model-studio/install-sdk)。
  - 不支持异步接口。
</Note>

#### 请求示例 <span id="3533b0e3eag1b" />

<CodeGroup>
  ```python 通过公网URL传入图片 expandable
  import json
  import os
  from dashscope import MultiModalConversation
  import dashscope

  # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
  dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

  # 模型支持输入1-3张图片
  messages = [
      {
          "role": "user",
          "content": [
              {"image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/rdsgaa/image+%2815%29.png"},
              {"image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/qokhtl/image+%2816%29.png"},
              {"text": "使用图一的城市照片作为底图。请勿更改照片中的真实建筑、街道、车辆或人物。保持照片的真实性。三个图二中的卡通形象在建筑物周围，一个趴在建筑物上方，一个从建筑物的右边探出头来，一个坐在建筑物前的空地上。该形象应采用扁平化的图形风格绘制，轮廓清晰，类似于壁画或海报插图。"}
          ]
      }
  ]

  # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
  # 若没有配置环境变量，请用百炼 API Key 将下行替换为：api_key="sk-xxx"
  api_key = os.getenv("DASHSCOPE_API_KEY")

  # qwen-image-2.0系列支持输出1-6张图片
  response = MultiModalConversation.call(
      api_key=api_key,
      model="qwen-image-2.0-pro",
      messages=messages,
      stream=False,
      n=1,
      watermark=False,
      negative_prompt=" ",
      prompt_extend=True,
      size="2048*2048",
  )

  if response.status_code == 200:
      # 如需查看完整响应，请取消下行注释
      # print(json.dumps(response, ensure_ascii=False))
      for i, content in enumerate(response.output.choices[0].message.content):
          print(f"输出图像{i+1}的URL:{content['image']}")
  else:
      print(f"HTTP返回码：{response.status_code}")
      print(f"错误码：{response.code}")
      print(f"错误信息：{response.message}")
      print("请参考文档：https://help.aliyun.com/zh/model-studio/error-code")
  ```

  ```python 通过Base64编码传入图片 expandable
  import json
  import os
  from dashscope import MultiModalConversation
  import base64
  import mimetypes
  import dashscope

  # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
  dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

  # ---用于 Base64 编码 ---
  # 格式为 data:{mime_type};base64,{base64_data}
  def encode_file(file_path):
      mime_type, _ = mimetypes.guess_type(file_path)
      if not mime_type or not mime_type.startswith("image/"):
          raise ValueError("不支持或无法识别的图像格式")

      try:
          with open(file_path, "rb") as image_file:
              encoded_string = base64.b64encode(
                  image_file.read()).decode('utf-8')
          return f"data:{mime_type};base64,{encoded_string}"
      except IOError as e:
          raise IOError(f"读取文件时出错: {file_path}, 错误: {str(e)}")

  # 获取图像的 Base64 编码
  # 调用编码函数，请将 "/path/to/your/image.png" 替换为您的本地图片文件路径，否则无法运行
  image = encode_file("/path/to/your/image.png")

  messages = [
      {
          "role": "user",
          "content": [
              {"image": image},
              {"text": "在画面右下角石板路旁、靠近树干根部的位置，以浅灰墨色手写体题写一首七言绝句，字体为行楷风格，笔触自然流畅、略带飞白，大小适中（约占画面高度1/10），与整体水墨淡雅氛围协调。诗文内容为：“青石桥畔柳风轻， 素手拈花闭目听。 一水碧痕浮旧梦， 半篙烟雨入空舲。”诗句横向排列，四句分两行书写（前两句一行，后两句一行），末句“舲”字右下角钤一枚朱红小印，印文为“江南”二字篆书，尺寸约等于单字高度的1/3。"}
          ]
      }
  ]

  # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
  # 若没有配置环境变量，请用百炼 API Key 将下行替换为：api_key="sk-xxx"
  api_key = os.getenv("DASHSCOPE_API_KEY")

  # qwen-image-2.0系列支持输出1-6张图片
  response = MultiModalConversation.call(
      api_key=api_key,
      model="qwen-image-2.0-pro",
      messages=messages,
      stream=False,
      n=1,
      watermark=False,
      negative_prompt=" ",
      prompt_extend=True,
      size="2048*2048",
  )

  if response.status_code == 200:
      # 如需查看完整响应，请取消下行注释
      # print(json.dumps(response, ensure_ascii=False))
      for i, content in enumerate(response.output.choices[0].message.content):
          print(f"输出图像{i+1}的URL:{content['image']}")
  else:
      print(f"HTTP返回码：{response.status_code}")
      print(f"错误码：{response.code}")
      print(f"错误信息：{response.message}")
      print("请参考文档：https://help.aliyun.com/zh/model-studio/error-code")
  ```

  ```python 通过URL下载图像
  # 需要安装requests以下载图像: pip install requests
  import requests

  def download_image(image_url, save_path='output.png'):
      try:
          response = requests.get(image_url, stream=True, timeout=300)  # 设置超时
          response.raise_for_status()  # 如果HTTP状态码不是200，则引发异常
          with open(save_path, 'wb') as f:
              for chunk in response.iter_content(chunk_size=8192):
                  f.write(chunk)
          print(f"图像已成功下载到: {save_path}")

      except requests.exceptions.RequestException as e:
          print(f"图像下载失败: {e}")

  image_url = "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx"
  download_image(image_url, save_path='output.png')
  ```
</CodeGroup>

#### 响应示例 <span id="177b27c9b28it" />

图像链接的有效期为24小时，请及时下载图像。

> `input_tokens`、`output_tokens`和`characters`为兼容字段，当前固定为0。

```json expandable
{
    "status_code": 200,
    "request_id": "959afba6-544e-487e-b58a-6bd9fea97xxx",
    "code": "",
    "message": "",
    "output": {
        "text": null,
        "finish_reason": null,
        "choices": [
            {
                "finish_reason": "stop",
                "message": {
                    "role": "assistant",
                    "content": [
                        {
                            "image": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxx.png?Expires=xxx"
                        }
                    ]
                }
            }
        ],
        "audio": null
    },
    "usage": {
        "input_tokens": 0,
        "output_tokens": 0,
        "characters": 0,
        "height": 2048,
        "image_count": 1,
        "width": 2048
    }
}
```

### Java SDK调用 <span id="589b80853e6rn" />

<Note>
  推荐安装最新版DashScope Java SDK，否则可能运行报错：[安装或升级SDK](/zh/model-studio/install-sdk)。
</Note>

#### 请求示例 <span id="75001167f1m0v" />

<CodeGroup>
  ```java 通过公网URL传入图片 expandable
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
  import com.alibaba.dashscope.common.MultiModalMessage;
  import com.alibaba.dashscope.common.Role;
  import com.alibaba.dashscope.exception.ApiException;
  import com.alibaba.dashscope.exception.NoApiKeyException;
  import com.alibaba.dashscope.exception.UploadFileException;
  import com.alibaba.dashscope.utils.Constants;
  import com.alibaba.dashscope.utils.JsonUtils;

  import java.io.IOException;
  import java.util.*;

  public class QwenImageEdit {

      static {
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
      }

      // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      // 若没有配置环境变量，请用百炼 API Key 将下行替换为：apiKey="sk-xxx"
      static String apiKey = System.getenv("DASHSCOPE_API_KEY");

      public static void call() throws ApiException, NoApiKeyException, UploadFileException, IOException {

          MultiModalConversation conv = new MultiModalConversation();

          // 模型支持输入1-3张图片
          MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                  .content(Arrays.asList(
                          Collections.singletonMap("image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/rdsgaa/image+%2815%29.png"),
                          Collections.singletonMap("image", "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260310/qokhtl/image+%2816%29.png"),
                          Collections.singletonMap("text", "使用图一的城市照片作为底图。请勿更改照片中的真实建筑、街道、车辆或人物。保持照片的真实性。三个图二中的卡通形象在建筑物周围，一个趴在建筑物上方，一个从建筑物的右边探出头来，一个坐在建筑物前的空地上。该形象应采用扁平化的图形风格绘制，轮廓清晰，类似于壁画或海报插图。")
                  )).build();
          // qwen-image-2.0系列支持输出1-6张图片
          Map<String, Object> parameters = new HashMap<>();
          parameters.put("watermark", false);
          parameters.put("negative_prompt", " ");
          parameters.put("n", 1);
          parameters.put("prompt_extend", true);
          parameters.put("size", "2048*2048");

          MultiModalConversationParam param = MultiModalConversationParam.builder()
                  .apiKey(apiKey)
                  .model("qwen-image-2.0-pro")
                  .messages(Collections.singletonList(userMessage))
                  .parameters(parameters)
                  .build();

          MultiModalConversationResult result = conv.call(param);
          // 如需查看完整响应，请取消下行注释
          // System.out.println(JsonUtils.toJson(result));
          List<Map<String, Object>> contentList = result.getOutput().getChoices().get(0).getMessage().getContent();
          int imageIndex = 1;
          for (Map<String, Object> content : contentList) {
              if (content.containsKey("image")) {
                  System.out.println("输出图像" + imageIndex + "的URL：" + content.get("image"));
                  imageIndex++;
              }
          }
      }

      public static void main(String[] args) {
          try {
              call();
          } catch (ApiException | NoApiKeyException | UploadFileException | IOException e) {
              System.out.println(e.getMessage());
          }
      }
  }
  ```

  ```java 通过Base64编码传入图片 expandable
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
  import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
  import com.alibaba.dashscope.common.MultiModalMessage;
  import com.alibaba.dashscope.common.Role;
  import com.alibaba.dashscope.exception.ApiException;
  import com.alibaba.dashscope.exception.NoApiKeyException;
  import com.alibaba.dashscope.exception.UploadFileException;
  import com.alibaba.dashscope.utils.Constants;
  import com.alibaba.dashscope.utils.JsonUtils;

  import java.io.IOException;
  import java.nio.file.Files;
  import java.nio.file.Path;
  import java.nio.file.Paths;
  import java.util.*;

  public class QwenImageEdit {

      static {
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
      }

      // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      // 若没有配置环境变量，请用百炼 API Key 将下行替换为：apiKey="sk-xxx"
      static String apiKey = System.getenv("DASHSCOPE_API_KEY");

      public static void call() throws ApiException, NoApiKeyException, UploadFileException, IOException {

          // 请将 "/path/to/your/image.png" 替换为您的本地图片文件路径，否则无法运行
          String image = encodeFile("/path/to/your/image.png");

          MultiModalConversation conv = new MultiModalConversation();

          MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                  .content(Arrays.asList(
                          Collections.singletonMap("image", image),
                          Collections.singletonMap("text", "在画面右下角石板路旁、靠近树干根部的位置，以浅灰墨色手写体题写一首七言绝句，字体为行楷风格，笔触自然流畅、略带飞白，大小适中（约占画面高度1/10），与整体水墨淡雅氛围协调。诗文内容为：“青石桥畔柳风轻， 素手拈花闭目听。 一水碧痕浮旧梦， 半篙烟雨入空舲。”诗句横向排列，四句分两行书写（前两句一行，后两句一行），末句“舲”字右下角钤一枚朱红小印，印文为“江南”二字篆书，尺寸约等于单字高度的1/3。")
                  )).build();
          // qwen-image-2.0系列支持输出1-6张图片
          Map<String, Object> parameters = new HashMap<>();
          parameters.put("watermark", false);
          parameters.put("negative_prompt", " ");
          parameters.put("n", 1);
          parameters.put("prompt_extend", true);
          parameters.put("size", "2048*2048");

          MultiModalConversationParam param = MultiModalConversationParam.builder()
                  .apiKey(apiKey)
                  .model("qwen-image-2.0-pro")
                  .messages(Collections.singletonList(userMessage))
                  .parameters(parameters)
                  .build();

          MultiModalConversationResult result = conv.call(param);
          // 如需查看完整响应，请取消下行注释
          // System.out.println(JsonUtils.toJson(result));
          List<Map<String, Object>> contentList = result.getOutput().getChoices().get(0).getMessage().getContent();
          int imageIndex = 1;
          for (Map<String, Object> content : contentList) {
              if (content.containsKey("image")) {
                  System.out.println("输出图像" + imageIndex + "的URL：" + content.get("image"));
                  imageIndex++;
              }
          }
      }

      /**
       * 将文件编码为Base64字符串
       * @param filePath 文件路径
       * @return Base64字符串，格式为 data:{mime_type};base64,{base64_data}
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
          try {
              call();
          } catch (ApiException | NoApiKeyException | UploadFileException | IOException e) {
              System.out.println(e.getMessage());
          }
      }
  }
  ```

  ```java 通过URL下载图像 expandable
  import java.io.FileOutputStream;
  import java.io.InputStream;
  import java.net.HttpURLConnection;
  import java.net.URL;

  public class ImageDownloader {
      public static void downloadImage(String imageUrl, String savePath) {
          try {
              URL url = new URL(imageUrl);
              HttpURLConnection connection = (HttpURLConnection) url.openConnection();
              connection.setConnectTimeout(5000);
              connection.setReadTimeout(300000);
              connection.setRequestMethod("GET");
              InputStream inputStream = connection.getInputStream();
              FileOutputStream outputStream = new FileOutputStream(savePath);
              byte[] buffer = new byte[8192];
              int bytesRead;
              while ((bytesRead = inputStream.read(buffer)) != -1) {
                  outputStream.write(buffer, 0, bytesRead);
              }
              inputStream.close();
              outputStream.close();

              System.out.println("图像已成功下载到: " + savePath);
          } catch (Exception e) {
              System.err.println("图像下载失败: " + e.getMessage());
          }
      }

      public static void main(String[] args) {
          String imageUrl = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx?Expires=xxx";
          String savePath = "output.png";
          downloadImage(imageUrl, savePath);
      }
  }
  ```
</CodeGroup>

#### 响应示例 <span id="9e9be7d617n7f" />

图像链接的有效期为24小时，请及时下载图像。

```json expandable
{
    "requestId": "5d5c3260-fc6c-4b4f-8b35-c06366effxxx",
    "usage": {
        "image_count": 1,
        "width": 2048,
        "height": 2048
    },
    "output": {
        "choices": [
            {
                "finish_reason": "stop",
                "message": {
                    "role": "assistant",
                    "content": [
                        {
                            "image": "https://dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com/xxx.png?Expires=xxx"
                        }
                    ]
                }
            }
        ]
    },
    "status_code": 200,
    "code": "",
    "message": ""
}
```

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 计费与限流 <span id="5dafefe82bgrh" />

- 模型免费额度和计费单价请参见[选择模型](/zh/model-studio/models)。
- 模型限流请参见[千问（Qwen-Image）](/zh/model-studio/rate-limit#f812e7c63axvx)。
- 计费说明：按成功生成的 <strong>图像张数</strong> 计费。模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。

## 常见问题 <span id="fe491e0f39tfz" />

#### Q：千问图像编辑模型支持哪些语言？ <span id="e70c6f863ecyc" />

A：目前正式支持<strong>简体中文和英文</strong>；其他语言可自行尝试，但效果存在不确定性。

##### Q: 如何查看模型调用量？ <span id="ae11776b04p4u" />

A: 模型调用完一小时后，请在[<strong>模型监控</strong>（北京）](https://bailian.console.aliyun.com/cn-beijing/model/telemetry)或[<strong>模型监控</strong>（新加坡）](https://bailian.console.aliyun.com/ap-southeast-1/model/telemetry) 页面，查看模型的调用次数、成功率等指标。详情请参见[账单查询与成本管理](/zh/model-studio/bill-query-and-cost-management)。

##### Q：如何获取图像存储的访问域名白名单？ <span id="5fb153423ct8z" />

A： 模型生成的图像存储于阿里云OSS，API将返回一个临时的公网URL（链接有效期为 24 小时，请及时下载保存）。图片URL的域名格式为`dashscope-{标识}.oss-accelerate.aliyuncs.com`或`dashscope-{标识}.oss-cn-{地域}.aliyuncs.com`。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会动态变更，以下bucket名称仅供参考，可能随时更新：dashscope-a717、dashscope-66f3、dashscope-7c2c、dashscope-2522、dashscope-c72b、dashscope-0484、dashscope-7e0f、dashscope-5859、dashscope-5496、dashscope-35f9、dashscope-31d9、dashscope-7f1f、dashscope-cc75、dashscope-64e9。为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。API当前仅支持返回URL格式，不支持base64格式输出。
