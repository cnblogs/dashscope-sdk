> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 千问-图像翻译API参考

> 千问-图像翻译模型（Qwen-MT-Image）可精准翻译图像中的文字，并保留原始排版。该模型还支持领域提示、敏感词过滤、术语干预等自定义功能。

<meta property="og:title" content="千问-图像翻译API参考" />

## 模型概览 <span id="5849834e92mb7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33%" }} /><col style={{ width: "33%" }} /><col style={{ width: "33%" }} /></colgroup><tbody><tr><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6962921671/p1007156.webp" alt="1" width={300} /></p> <p>源语种：中文</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6962921671/p1006389.webp" alt="2" width={300} /></p> <p>英文</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6962921671/p1006396.webp" alt="3" width={300} /></p> <p>日文</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6962921671/p1006438.webp" alt="4" width={300} /></p> <p>韩语</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6962921671/p1006451.webp" alt="es" width={300} /></p> <p>西班牙语</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6962921671/p1006452.webp" alt="fr" width={300} /></p> <p>法语</p></td></tr></tbody></table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15%" }} /><col style={{ width: "50%" }} /><col style={{ width: "15%" }} /><col style={{ width: "20%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th><th><p><strong>可用地域</strong></p></th><th><p><strong>输出图像规格</strong></p></th></tr></thead><tbody><tr><td><p>qwen-mt-image-2.0</p></td><td><p>专注做图片翻译的模型服务，能将中、英、日等55个语言的图片翻译到指定的语言，精准还原图片排版和内容信息。</p> <p>支持术语定义、敏感词过滤、商品主体检测等自定义功能，提供灵活、准确、高效的图像本地化服务。</p></td><td><p>华北2（北京）</p> <p>新加坡</p></td><td><p>图片格式：JPG。</p></td></tr><tr><td><p>qwen-mt-image</p></td><td><p>千问-图像翻译模型</p> <p>支持中/英文与其他语种之间的互译，但不支持在非中/英语种之间直接翻译（例如，从日语翻译为韩语）。详情请参见<a href="/zh/model-studio/qwen-mt-image-api#d2aa4b03d2kco">支持的语种</a>。</p></td><td><p>华北2（北京）</p></td><td><p>图片格式：JPG。</p></td></tr></tbody></table>

## 前提条件 <span id="0e41425c577u4" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## 同步调用 <span id="f6g7h8i9j0syn" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`，调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

同步模式下，请求会等待处理完成后直接返回翻译后的图像URL，无需轮询任务状态。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数

        ##### 请求头（Headers）

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### 请求体（Request Body）

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，设置为`qwen-mt-image-2.0`或`qwen-mt-image`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入参数对象，包含以下字段：

        <Accordion title="属性" defaultOpen>
          <strong>image\_url</strong> `string` <strong>（必选）</strong>

          图像的公网可访问的URL，支持 HTTP 和 HTTPS 协议。如需获取本地文件的公网URL，请参见[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url) 。

          - <strong>格式限制</strong>：JPG、JPEG、PNG、BMP、PNM、PPM、TIFF、WEBP
          - <strong>尺寸限制</strong>：图像的宽度和高度均需在15-8192像素范围内，宽高比在1:10至10:1范围内。
          - <strong>大小限制</strong>：不超过100MB
          - URL地址中不能包含中文字符。
          - <strong>示例</strong>：`https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp`

          <strong>source\_lang</strong> `string` <strong>（必选）</strong>

          [源语种](/zh/model-studio/qwen-mt-image-api#d2aa4b03d2kco)。

          - <strong>支持值</strong>：语种全称、语种编码或`auto`（自动检测），对大小写不敏感
          - <strong>限制</strong>：与`target_lang`不同。`qwen-mt-image-2.0`无语种限制；`qwen-mt-image`要求源语种或目标语种至少有一项为中文或英文。
          - <strong>示例</strong>：`Chinese`、`en`、`auto`

          <strong>target\_lang</strong> `string` <strong>（必选）</strong>

          [目标语种](/zh/model-studio/qwen-mt-image-api#d2aa4b03d2kco)。

          - <strong>支持值</strong>：语种全称或语种编码，对大小写不敏感
          - <strong>限制</strong>：与`source_lang`不同。`qwen-mt-image-2.0`无语种限制；`qwen-mt-image`要求源语种或目标语种至少有一项为中文或英文。
          - <strong>示例</strong>：`Chinese`、`en`

          <strong>ext</strong> `object` （可选）

          可选拓展字段。

          <Accordion title="属性" defaultOpen>
            <strong>domainHint</strong> `string` （可选）

            领域提示，为使译文风格更贴合特定领域，可以使用英文描述使用场景、译文风格等需求。

            为确保翻译效果，建议不超过200个英文单词。

            <Tip>
              领域提示语句当前<strong>只支持英文</strong>。
            </Tip>

            <strong>示例：</strong>These sentences are from seller-buyer conversations on a B2C ecommerce platform. Translate them into clear, engaging customer service language, ensuring the translation is appropriate for handling potential issues or disputes.

            <strong>sensitives</strong> `array` （可选）

            配置敏感词，以在翻译前过滤图片中<strong>完全匹配</strong>的文本，<strong>对大小写敏感</strong>。

            敏感词的语种可与源语种不一致，支持全部的[源语种](/zh/model-studio/qwen-mt-image-api)和[目标语种](/zh/model-studio/qwen-mt-image-api#d2aa4b03d2kco)。为确保翻译效果，建议单次请求添加的敏感词不超过50个。

            <strong>示例：</strong>\["全场9折", "七天无理由退换"]

            <strong>terminologies</strong> `array` （可选）

            术语干预，为特定术语设定译文，以满足特定领域的翻译需求，术语对的语种需要与`source_lang`和`target_lang`对应。

            <Accordion title="属性" defaultOpen>
              <strong>src</strong> `string` <strong>（必选）</strong>

              术语的源文本，语种需要与源语种`source_lang`一致。

              <strong>tgt</strong>`string` <strong>（必选）</strong>

              术语的目标文本，语种需要与目标语种`target_lang`一致。
            </Accordion>

            <strong>示例</strong>：\[\{"src": "应用程序接口", "tgt": "API"}, \{"src": "机器学习", "tgt": "ML"}]

            <strong>config</strong> `object` （可选）

            <Accordion title="属性" defaultOpen>
              <strong>imageSegment</strong>`bool` （可选）

              是否开启图像主体分割。开启后，将跳过对图像中主体（如人物、商品、Logo）上文字的翻译。

              - `false`：（默认值）翻译图像中的所有文字。
              - `true`：不翻译图像主体的文字。

              > <strong>注意</strong>：旧版本参数名为`skipImgSegment`（是否跳过图像主体分割）。为保持兼容，该参数仍受支持，但建议使用新的 `imageSegment`参数。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="curl">
            ```bash
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "qwen-mt-image-2.0",
                "input": {
                    "image_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp",
                    "source_lang": "zh",
                    "target_lang": "en"
                }
            }'
            ```
          </Tab>

          <Tab title="Python">
            ```python
            import requests
            import os

            # 请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同
            url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis"

            headers = {
              "Authorization": f"Bearer {os.getenv('DASHSCOPE_API_KEY')}",
              "Content-Type": "application/json"
            }

            # 同步调用：不设置 X-DashScope-Async 请求头
            payload = {
              "model": "qwen-mt-image-2.0",
              "input": {
                "image_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp",
                "source_lang": "zh",
                "target_lang": "en"
              }
            }

            response = requests.post(url, headers=headers, json=payload)
            result = response.json()
            print(result)
            # 同步模式直接返回 image_url
            image_url = result["output"]["image_url"]
            print(f"翻译完成，图像URL: {image_url}")
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
        #### 响应参数

        <strong>output</strong> `object`

        输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>image\_url</strong> `string`

          模型生成图像的URL地址，与原图长宽相同，JPG格式。有效期为24小时，请及时下载并保存图像。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          模型生成图像的数量，固定为1。
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
            ```json
            {
                "output": {
                    "image_url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx.jpg?Expires=xxx"
                },
                "usage": {
                    "image_count": 1
                },
                "request_id": "b60d747a-adee-940a-aa3e-ac55b957189a"
            }
            ```
          </Tab>

          <Tab title="异常响应">
            请参见[错误码](/zh/model-studio/error-code)进行解决。

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

## 异步调用 <span id="42703589880ts" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`，调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

异步调用流程分两步：

1. <strong>创建任务获取任务ID</strong>：发送一个请求创建任务，该请求会返回<strong>任务ID（task\_id）</strong>。
2. <strong>根据任务ID查询结果</strong>：使用task\_id轮询任务状态，直到任务完成并获得图像URL。

### 步骤1：创建任务获取任务ID <span id="db8cf70432oe3" />

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

        异步处理配置参数。<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将使用同步模式（仅`qwen-mt-image-2.0`支持同步）。`qwen-mt-image`模型必须设置此请求头。
        </Tip>

        ##### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，设置为`qwen-mt-image-2.0`或`qwen-mt-image`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入参数对象，包含以下字段：

        <Accordion title="属性" defaultOpen>
          <strong>image\_url</strong> `string` <strong>（必选）</strong>

          图像的公网可访问的URL，支持 HTTP 和 HTTPS 协议。如需获取本地文件的公网URL，请参见[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url) 。

          - <strong>格式限制</strong>：JPG、JPEG、PNG、BMP、PNM、PPM、TIFF、WEBP
          - <strong>尺寸限制</strong>：图像的宽度和高度均需在15-8192像素范围内，宽高比在1:10至10:1范围内。
          - <strong>大小限制</strong>：不超过100MB
          - URL地址中不能包含中文字符。
          - <strong>示例</strong>：`https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp`

          <strong>source\_lang</strong> `string` <strong>（必选）</strong>

          [源语种](/zh/model-studio/qwen-mt-image-api#d2aa4b03d2kco)。

          - <strong>支持值</strong>：语种全称、语种编码或`auto`（自动检测），对大小写不敏感
          - <strong>限制</strong>：与`target_lang`不同。`qwen-mt-image-2.0`无语种限制；`qwen-mt-image`要求源语种或目标语种至少有一项为中文或英文。
          - <strong>示例</strong>：`Chinese`、`en`、`auto`

          <strong>target\_lang</strong> `string` <strong>（必选）</strong>

          [目标语种](/zh/model-studio/qwen-mt-image-api#d2aa4b03d2kco)。

          - <strong>支持值</strong>：语种全称或语种编码，对大小写不敏感
          - <strong>限制</strong>：与`source_lang`不同。`qwen-mt-image-2.0`无语种限制；`qwen-mt-image`要求源语种或目标语种至少有一项为中文或英文。
          - <strong>示例</strong>：`Chinese`、`en`

          <strong>ext</strong> `object` （可选）

          可选拓展字段。

          <Accordion title="属性" defaultOpen>
            <strong>domainHint</strong> `string` （可选）

            领域提示，为使译文风格更贴合特定领域，可以使用英文描述使用场景、译文风格等需求。

            为确保翻译效果，建议不超过200个英文单词。

            <Tip>
              领域提示语句当前<strong>只支持英文</strong>。
            </Tip>

            <strong>示例：</strong>These sentences are from seller-buyer conversations on a B2C ecommerce platform. Translate them into clear, engaging customer service language, ensuring the translation is appropriate for handling potential issues or disputes.

            <strong>sensitives</strong> `array` （可选）

            配置敏感词，以在翻译前过滤图片中<strong>完全匹配</strong>的文本，<strong>对大小写敏感</strong>。

            敏感词的语种可与源语种不一致，支持全部的[源语种](/zh/model-studio/qwen-mt-image-api)和[目标语种](/zh/model-studio/qwen-mt-image-api#d2aa4b03d2kco)。为确保翻译效果，建议单次请求添加的敏感词不超过50个。

            <strong>示例：</strong>\["全场9折", "七天无理由退换"]

            <strong>terminologies</strong> `array` （可选）

            术语干预，为特定术语设定译文，以满足特定领域的翻译需求，术语对的语种需要与`source_lang`和`target_lang`对应。

            <Accordion title="属性" defaultOpen>
              <strong>src</strong> `string` <strong>（必选）</strong>

              术语的源文本，语种需要与源语种`source_lang`一致。

              <strong>tgt</strong>`string` <strong>（必选）</strong>

              术语的目标文本，语种需要与目标语种`target_lang`一致。
            </Accordion>

            <strong>示例</strong>：\[\{"src": "应用程序接口", "tgt": "API"}, \{"src": "机器学习", "tgt": "ML"}]

            <strong>config</strong> `object` （可选）

            <Accordion title="属性" defaultOpen>
              <strong>imageSegment</strong>`bool` （可选）

              是否开启图像主体分割。开启后，将跳过对图像中主体（如人物、商品、Logo）上文字的翻译。

              - `false`：（默认值）翻译图像中的所有文字。
              - `true`：不翻译图像主体的文字。

              > <strong>注意</strong>：旧版本参数名为`skipImgSegment`（是否跳过图像主体分割）。为保持兼容，该参数仍受支持，但建议使用新的 `imageSegment`参数。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="curl">
            ```bash
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "qwen-mt-image-2.0",
                "input": {
                    "image_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp",
                    "source_lang": "zh",
                    "target_lang": "en",
                    "ext": {
                        "config": {
                            "imageSegment": false
                        }
                    }
                }
            }'
            ```
          </Tab>

          <Tab title="Python">
            ```python
            import requests
            import os

            # 请将 {WorkspaceId} 替换为您的百炼业务空间ID
            url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis"

            headers = {
              "X-DashScope-Async": "enable",
              "Authorization": f"Bearer {os.getenv('DASHSCOPE_API_KEY')}",
              "Content-Type": "application/json"
            }

            payload = {
              "model": "qwen-mt-image-2.0",
              "input": {
                "image_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250916/ordhsk/1.webp",
                "source_lang": "zh",
                "target_lang": "en",
                "ext": {
                  "config": {
                    "imageSegment": False
                  }
                }
              }
            }

            response = requests.post(url, headers=headers, json=payload)
            result = response.json()
            print(result)
            # 保存 task_id，用于步骤2查询结果
            task_id = result["output"]["task_id"]
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

        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

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

### 步骤2：根据任务ID查询结果 <span id="420e6e77a17om" />

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<Note>
  - `task_id` 有效期为<strong>24小时</strong>，若ID不存在或已过期，任务状态将返回 `UNKNOWN`。
  - 任务成功后返回的 `url`有效期为<strong>24小时</strong>，请及时下载并保存图像。
  - 此查询接口的默认RPS为1。如需更高频次的查询或事件通知，请[配置异步任务回调](/zh/model-studio/async-task-api)。
  - 如需批量查询或取消任务，请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
</Note>

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
            您需要将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id。

            ```bash
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
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

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>image\_url</strong> `string`

          模型生成图像的URL地址，与原图长宽相同，JPG格式。有效期为24小时，请及时下载并保存图像。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息，详情请参见[错误码](/zh/model-studio/error-code)。

          通常请求成功时不会返回此参数，仅在图像中无可翻译文本（例如，在分割图像主体后，其余部分无文字）时，<strong>任务仍会成功并正常计费</strong>，但会返回`No text detected for translation`的提示。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          模型生成图像的数量，固定为1。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功-存在可翻译内容">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json
            {
                "request_id": "5fec62eb-bf94-91f8-b9f4-f7f758e4e27e",
                "output": {
                    "task_id": "72c52225-8444-4cab-ad0c-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-08-13 18:11:16.954",
                    "scheduled_time": "2025-08-13 18:11:17.003",
                    "end_time": "2025-08-13 18:11:23.860",
                    "image_url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx?Expires=xxx"
                },
                "usage": {
                    "image_count":1
                }
            }
            ```
          </Tab>

          <Tab title="任务执行成功-无可翻译内容">
            在图像中无可翻译文本（例如，在识别出图像主体后，其余部分无文字）时，<strong>任务仍会成功并正常计费</strong>，但会返回`No text detected for translation`的提示。

            ```json
            {
                "request_id": "0ccb84aa-e034-431d-9d54-08e14fxxxxxx",
                "output": {
                    "task_id": "34ec4208-97d6-498b-a390-9173f7xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-01-12 17:56:23.625",
                    "scheduled_time": "2026-01-12 17:56:23.656",
                    "end_time": "2026-01-12 17:56:25.324",
                    "image_url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx.jpg?Expires=xxx",
                    "message": "No text detected for translation"
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
                "request_id": "daad9007-6acd-9fb3-a6bc-d55902b9c9ff",
                "output": {
                    "task_id": "fe8aa114-d9f1-4f76-b598-xxxxxx",
                    "task_status": "FAILED",
                    "submit_time": "2025-08-20 09:54:21.911",
                    "scheduled_time": "2025-08-20 09:54:21.984",
                    "end_time": "2025-08-20 12:55:00.818",
                    "code": "InternalError",
                    "message": "xxxxxx"
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 支持的语种 <span id="d2aa4b03d2kco" />

若不确定源语种，可将 `source_lang` 设置为 `auto` 进行自动检测。

### qwen-mt-image-2.0 支持的语种

`qwen-mt-image-2.0`支持以下55种语言的任意组合互译，无需源语种或目标语种为中文或英文。所有语种均可作为源语种和目标语种。

| 语种（中文名） | 英文全称                | 编码    |
| ------- | ------------------- | ----- |
| 简体中文    | Simplified Chinese  | zh    |
| 繁体中文    | Traditional Chinese | zh-tw |
| 英语      | English             | en    |
| 日语      | Japanese            | ja    |
| 韩语      | Korean              | ko    |
| 德语      | German              | de    |
| 西班牙语    | Spanish             | es    |
| 俄语      | Russian             | ru    |
| 法语      | French              | fr    |
| 葡萄牙语    | Portuguese          | pt    |
| 意大利语    | Italian             | it    |
| 越南语     | Vietnamese          | vi    |
| 印度尼西亚语  | Indonesian          | id    |
| 马来语     | Malay               | ms    |
| 泰语      | Thai                | th    |
| 阿拉伯语    | Arabic              | ar    |
| 荷兰语     | Dutch               | nl    |
| 波兰语     | Polish              | pl    |
| 土耳其语    | Turkish             | tr    |
| 乌克兰语    | Ukrainian           | uk    |
| 希腊语     | Greek               | el    |
| 匈牙利语    | Hungarian           | hu    |
| 罗马尼亚语   | Romanian            | ro    |
| 捷克语     | Czech               | cs    |
| 瑞典语     | Swedish             | sv    |
| 丹麦语     | Danish              | da    |
| 芬兰语     | Finnish             | fi    |
| 挪威语     | Norwegian           | no    |
| 印地语     | Hindi               | hi    |
| 泰米尔语    | Tamil               | ta    |
| 泰卢固语    | Telugu              | te    |
| 尼泊尔语    | Nepali              | ne    |
| 波斯语     | Persian             | fa    |
| 阿塞拜疆语   | Azerbaijani         | az    |
| 哈萨克语    | Kazakh              | kk    |
| 乌兹别克语   | Uzbek               | uz    |
| 蒙古语     | Mongolian           | mn    |
| 维吾尔语    | Uyghur              | ug    |
| 白俄罗斯语   | Belarusian          | be    |
| 保加利亚语   | Bulgarian           | bg    |
| 塞尔维亚语   | Serbian             | sr    |
| 克罗地亚语   | Croatian            | hr    |
| 波斯尼亚语   | Bosnian             | bs    |
| 斯洛文尼亚语  | Slovenian           | sl    |
| 斯洛伐克语   | Slovak              | sk    |
| 马其顿语    | Macedonian          | mk    |
| 拉脱维亚语   | Latvian             | lv    |
| 立陶宛语    | Lithuanian          | lt    |
| 爱尔兰语    | Irish               | ga    |
| 卢森堡语    | Luxembourgish       | lb    |
| 南非荷兰语   | Afrikaans           | af    |
| 拉丁语     | Latin               | la    |
| 车臣语     | Chechen             | ce    |
| 印古什语    | Ingush              | inh   |
| 马里语     | Mari                | chm   |

### qwen-mt-image 支持的语种

`qwen-mt-image`要求<strong>源语种或目标语种必须至少有一种是中文或英文</strong>，不支持在两个非中、英语种之间直接翻译（例如，从日语翻译为韩语）。

| 语种（中文名） | 英文全称       | 编码 | 支持作为源语种 | 支持作为目标语种 |
| ------- | ---------- | -- | ------- | -------- |
| 简体中文    | Chinese    | zh | 支持      | 支持       |
| 英文      | English    | en | 支持      | 支持       |
| 韩语      | Korean     | ko | 支持      | 支持       |
| 日语      | Japanese   | ja | 支持      | 支持       |
| 俄语      | Russian    | ru | 支持      | 支持       |
| 西班牙语    | Spanish    | es | 支持      | 支持       |
| 法语      | French     | fr | 支持      | 支持       |
| 葡萄牙语    | Portuguese | pt | 支持      | 支持       |
| 意大利语    | Italian    | it | 支持      | 支持       |
| 德语      | German     | de | 支持      | 不支持      |
| 越南语     | Vietnamese | vi | 支持      | 支持       |
| 马来语     | Malay      | ms | 不支持     | 支持       |
| 泰语      | Thai       | th | 不支持     | 支持       |
| 印尼语     | Indonesian | id | 不支持     | 支持       |
| 阿拉伯语    | Arabian    | ar | 不支持     | 支持       |

## 计费与限流 <span id="5dafefe82bgrh" />

- 模型免费额度和计费单价请参见[模型价格](/zh/model-studio/model-pricing#a3250eda6bgjx)。
- 模型限流请参见[限流](/zh/model-studio/rate-limit#f812e7c63axvx)。
- 计费说明：按成功生成的<strong>图像张数</strong>计费。模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。
- <strong>注意</strong>：如果图像中无可翻译文本，或在启用主体识别功能后，非主体部分无文字时，<strong>任务仍记为成功</strong>并<strong>正常计费</strong>，此时接口会返回`No text detected for translation`的提示。

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="c928ee689aw6l" />

#### Q：为什么图中的内容没有被翻译？ <span id="fde7e2e421sy9h4" />

A：因为启用了[主体分割](/zh/model-studio/qwen-mt-image-api)功能，模型不会翻译图片中人物、商品、Logo等主体上的文字。若需翻译所有文字，请将`ext.config.imgSegment`参数设置为`false`。

#### Q：如何将临时的图像链接转为永久链接？ <span id="c8e2ec8554vqhh4" />

A：临时链接无法直接转为永久链接。需通过后端服务下载图像，再上传至对象存储服务（如阿里云 OSS）以生成新的永久链接。

<Accordion title="示例代码：下载图像到本地">
  ```python
  import requests

  def download_and_save_image(image_url, save_path):
    try:
      response = requests.get(image_url, stream=True, timeout=300)
      response.raise_for_status()
      with open(save_path, 'wb') as f:
        for chunk in response.iter_content(chunk_size=8192):
          f.write(chunk)
      print(f"图像已成功下载到: {save_path}")
    except requests.exceptions.RequestException as e:
      print(f"图像下载失败: {e}")

  if __name__ == '__main__':
    image_url = "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx?Expires=xxx"
    save_path = "image-translation.jpg"
    download_and_save_image(image_url, save_path)
  ```
</Accordion>

### Q: 如何查看模型调用量？ <span id="ae11776b04p4u" />

A: 模型调用完一小时后，请在[<strong>模型监控</strong>（北京）](https://bailian.console.aliyun.com/cn-beijing/model/telemetry)或[<strong>模型监控</strong>（新加坡）](https://bailian.console.aliyun.com/ap-southeast-1/model/telemetry) 页面，查看模型的调用次数、成功率等指标。详情请参见[账单查询与成本管理](/zh/model-studio/bill-query-and-cost-management)。

### Q：如何获取图像存储的访问域名白名单？ <span id="5fb153423ct8z" />

A： 模型生成的图像存储于阿里云OSS，API将返回一个临时的公网URL。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会根据业务情况进行动态变更，为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。

#### Q：翻译后的文字没有覆盖原内容，出现格式错位怎么办？

A：复杂排版（如多列文本、混合字体大小）的图片在翻译后容易出现文字错位，属于已知的模型限制。建议将复杂排版的图片分割为简单区域后分别翻译，即可规避此问题。
