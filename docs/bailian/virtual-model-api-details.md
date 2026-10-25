> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 虚拟模特API参考

> 本文介绍万相-虚拟模特模型的输入输出参数。该模型可以对上传的真人或者人台实拍商品展示图进行智能生成，将其中的模特和背景替换为心仪的内容。在保持人物姿态不变的情况下，使用虚拟模特对商品进行更加精美、多样的展示。同时支持各种与模特产生互动的商品，如手持小商品、服装、鞋靴、配饰等。

<meta property="og:title" content="虚拟模特API参考" />

<strong>相关指南</strong>：[虚拟模特生成](/zh/model-studio/virtual-model-generation)

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
  - wanx-virtualmodel、virtualmodel-v2 模型当前仅提供<strong>免费体验</strong>，免费额度用完后不可调用且不支持付费，推荐参考[图像编辑-千问](/zh/model-studio/qwen-image-edit-guide)或[图像编辑-万相2.1](/zh/model-studio/wanx-image-edit)获取替代方案。
</Tip>

## 模型概览 <span id="50446bc4ab3pt" />

万相-虚拟模特可以对上传的真人实拍商品展示图进行智能生成，将其中的模特和背景替换为心仪的内容，在保持人物姿态不变的情况下，使用虚拟模特对商品进行更加精美、多样的展示。支持各种与模特产生互动的商品，如手持小商品、服装、鞋靴、配饰等。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.602592%" }} /><col style={{ width: "12.742981%" }} /><col style={{ width: "69.654428%" }} /></colgroup><thead><tr><th><strong>模型版本</strong></th><th><strong>模型名称</strong></th><th><strong>模型简介</strong></th></tr></thead><tbody><tr><td><p>虚拟模特</p> <p>（V1版本）</p></td><td><p>wanx-virtualmodel</p></td><td><ul><li><p>支持真人实拍图上传</p></li></ul> <ul><li><p>生成的图片短边：512像素或1024像素</p></li></ul></td></tr><tr><td><p>虚拟模特V2</p> <p>（V2版本）</p></td><td><p>virtualmodel-v2</p></td><td><ul><li><p>支持真人、人台实拍图上传</p></li> <li><p>生成的图片短边为：1024像素或2048像素</p></li> <li><p>支持改变分辨率，生成图片长宽比可选择：比例不变、2:1、16:9、4:3、1:1、3:4、 9:16、1:2。</p></li> <li><p>支持背景参考图权重自由控制</p></li> <li><p>文本引导效果更准确</p></li></ul></td></tr></tbody></table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "16.790123%" }} />

    <col style={{ width: "19.259259%" }} />

    <col style={{ width: "17.037037%" }} />

    <col style={{ width: "17.777778%" }} />

    <col style={{ width: "29.135802%" }} />
  </colgroup>

  <thead>
    <tr>
      <th rowSpan={2}>
        <strong>模型名称</strong>
      </th>

      <th rowSpan={2}>
        <strong>计费单价</strong>
      </th>

      <th colSpan={2}>
        <strong>限流（主账号与RAM子账号共用）</strong>
      </th>

      <th rowSpan={2}>
        <strong>免费额度</strong>[（查看）](/zh/model-studio/new-free-quota)
      </th>
    </tr>

    <tr>
      <th>
        <strong>任务下发接口RPS限制</strong>
      </th>

      <th>
        <strong>同时处理中任务数量</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        wanx-virtualmodel
      </td>

      <td rowSpan={2}>
        目前仅供免费体验。

        > 免费额度用完后不可调用，推荐参考[图像编辑-千问](/zh/model-studio/qwen-image-edit-guide)或[图像编辑-万相2.1](/zh/model-studio/wanx-image-edit)获取替代方案。
      </td>

      <td rowSpan={2}>
        2
      </td>

      <td rowSpan={2}>
        1
      </td>

      <td rowSpan={2}>
        500张
      </td>
    </tr>

    <tr>
      <td>
        virtualmodel-v2
      </td>
    </tr>
  </tbody>
</table>

## 前提条件 <span id="24334fea04ia9" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="ffa47ca257gkh" />

为了减少等待时间并且避免请求超时，服务采用异步方式提供。您需要发起两个请求：

- <strong>创建任务</strong>：首先发送一个请求创建文生图任务，该请求会返回任务ID。
- <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询模型生成的结果。

### 步骤1：创建任务获取任务ID <span id="24e4121a7fyos" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/virtualmodel/generation`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

> 注意：若无特殊说明，下面的参数支持在虚拟模特模型V1版本和V2版本中使用。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求头（Headers） <span id="afcd41d2b57zf" />

        <strong>Authorization</strong>*string*<strong>必选</strong>

        推荐您使用阿里云百炼API-Key，也可填DashScope API-Key。例如：Bearer d1xxx2a。

        <strong>X-DashScope-Async</strong>*string*<strong>必选</strong>

        是否使用异步调用。HTTP只支持异步调用，设置为`enable`。

        <strong>Content-Type</strong>*string*<strong>必选</strong>

        请求内容类型。固定为`application/json`。

        #### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong>*string*<strong>必选</strong>

        调用模型。

        <strong>input</strong>*object*<strong>必选</strong>

        输入图像的基本信息，比如图像URL地址。

        <Accordion title="属性" defaultOpen>
          <strong>base\_image\_url</strong> *string*<strong>必选</strong>

          原始真人展示图像URL地址。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图像格式：JPEG、JPG、PNG、WEBP。
          - 图像分辨率：不低于256×256像素且不超过4096×4096像素，人脸占比不低于128×128像素。
          - 图修比例：长宽比大于1:2且小于2:1。
          - 图像大小：不超过5MB。
          - URL地址中不能包含中文字符。

          <Accordion title="真人图像示例" defaultOpen>
            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7749386271/p850516.png" alt="image" width={200} />
          </Accordion>

          <strong>mask\_image\_url</strong> *string*<strong>必选</strong>

          对应原图的期望保留区域mask图URL，图片为（0,255）的黑白图，其中白色表示商品主体区域。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图片限制：

          - 图像格式：JPEG、JPG、PNG、WEBP。
          - 图像分辨率：与base\_image\_url参数对应的图像保持一致。
          - 图修比例：长宽比大于1:2且小于2:1。
          - 图像大小：不超过5MB。
          - URL地址中不能包含中文字符。

          <Accordion title="mask图像示例" defaultOpen>
            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7068778171/p810523.png" alt="image.png" width={200} />
          </Accordion>

          <strong>predefined\_face\_id</strong> *string 可选*

          预设人物ID。<strong>仅在V1版本使用。</strong>

          <Accordion title="枚举示例" defaultOpen>
            - girl1

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0489386271/p850530.png" alt="image" width={100} />

            - girl2

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0489386271/p850531.png" alt="image" width={100} />

            - girl3

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0489386271/p850532.png" alt="image" width={100} />

            - boy1

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0489386271/p850533.png" alt="image" width={100} />

            - boy2

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0489386271/p850534.png" alt="image" width={100} />

            - boy3

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0489386271/p850535.png" alt="image" width={100} />
          </Accordion>

          <strong>face\_image\_url</strong> *string 可选*

          期望替换的人物图像URL地址。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图片限制：

          - 图像格式：JPEG、JPG、PNG、WEBP。
          - 图像分辨率：长边像素不大于2048，人脸区域大于128×128像素。
          - 图像大小：建议不超过5MB。

          优先级低于predefined\_face\_id参数。

          <strong>prompt</strong> *string*<strong>必选</strong>

          针对生成图像背景环境、模特的全身形象描述。

          支持中英文，小于100字符。

          示例：一名年轻女子，身穿白色短裤，极简风格调色板，长镜头，双色效果，暗银色和浅粉色。

          <strong>face\_prompt</strong> *string*<strong>必选</strong>

          生成人像面部描述，支持中英文，小于100字符。

          示例：一名年轻女子，面容娇好，最好的品质。

          <strong>background\_image\_url</strong> *string 可选*

          背景环境参考图像URL地址。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图片限制：

          - 图像格式：JPEG、JPG、PNG、WEBP。
          - 图像分辨率：图像长边像素不大于4096。
          - 图像比例：长宽比小于等于2。
          - 图像大小：不超过5MB。

          <strong>bgstyle\_scale</strong> *float 可选*

          背景参考图像权重控制。<strong>仅在V2版本使用。</strong>

          取值范围：\[0.0, 1.0]，默认0.7。

          数值越大表示参考程度越大。

          <strong>realPerson</strong> *bool 可选*

          输入图片是否是真人。<strong>仅在V2版本使用。</strong>

          - true：默认值，表示输入图像是真人。
          - false：表示输入图像是人台或者非真人。

          <strong>style</strong> *string 可选*

          生成图片风格。<strong>仅在V2版本使用。</strong>

          可选参数\["","portrait"]，默认是portrait，

          说明：portrait模式会增加一些景深，突出人像的效果\*。\*

          <strong>seed</strong> *integer 可选*

          控制生成seed。<strong>仅在V2版本使用。</strong>

          取值范围：\[0, 4294967290]。不传时系统随机生成 seed。指定具体值（0～4294967290）时使用固定随机种子，相同 seed 值会生成相同结果。

          <strong>aspect\_ratio</strong> *String 可选*

          生成图片长宽比例。<strong>仅在V2版本使用。</strong>

          <Accordion title="枚举值" defaultOpen>
            可选的比例有：

            - 比例不变，默认值
            - 2:1
            - 16:9
            - 4:3
            - 1:1
            - 3:4
            - 9:16
            - 1:2
          </Accordion>
        </Accordion>

        <strong>parameters</strong>*object*可选

        <Accordion title="属性" defaultOpen>
          <strong>n</strong> *Integer 可选*

          生成图像的数量，支持 1\~4 张，默认值 1。

          <strong>short\_side\_size</strong> *string*<strong>必选</strong>

          指定生成的图像短边大小，单位：像素。生成图片和输入原图会保持相同的长宽比。

          V1版本可选值：512和1024。

          V2版本可选值：1024和2048
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <CodeGroup>
          ```bash 虚拟模特V1
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/virtualmodel/generation' \
          --header 'X-DashScope-Async: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
            "model": "wanx-virtualmodel",
            "input": {
              "base_image_url": "https://g-adoc.alcasset.com/media/maas_docs/sfm/common/images/6a4b3c2d1e0f9275.jpg",
              "mask_image_url": "https://g-adoc.alcasset.com/media/maas_docs/sfm/common/images/6a4b3c2d1e0f9274.png",
              "prompt": "一名年轻女子，身穿白色短裤，极简风格调色板，长镜头，双色效果（暗银色和浅粉色）",
              "face_prompt": "年轻女子，面容姣好，最高品质"
            },
            "parameters": {
              "short_side_size": "512",
              "n": 1
            }
          }'
          ```

          ```bash 虚拟模特V2
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/virtualmodel/generation' \
          --header 'X-DashScope-Async: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
            "model": "virtualmodel-v2",
            "input": {
              "base_image_url": "https://g-adoc.alcasset.com/media/maas_docs/sfm/common/images/6a4b3c2d1e0f9275.jpg",
              "mask_image_url": "https://g-adoc.alcasset.com/media/maas_docs/sfm/common/images/6a4b3c2d1e0f9274.png",
              "background_image_url": "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/4644874271/p827239.png",
              "prompt": "a beautiful chinese woman stands in front of a plain white background",
              "face_prompt": "a beautiful chinese woman, good face, best face, best quality"
            },
            "parameters": {
                "short_side_size": "1024",
                "n": 1
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
        #### 响应 <span id="cd3063363egdc" />

        <strong>output</strong>*object*

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> *string*

          任务id。

          <strong>task\_status</strong> *string*

          任务状态。

          - PENDING：排队中
          - RUNNING：处理中
          - SUSPENDED：挂起
          - SUCCEEDED：执行成功
          - FAILED：执行失败
          - UNKNOWN：任务不存在或状态未知
        </Accordion>

        <strong>code</strong>*string*

        接口错误码。接口成功请求不会返回该参数。

        <strong>message</strong>*string*

        接口错误信息。接口成功请求不会返回该参数。

        <strong>request\_id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <CodeGroup>
          ```json 成功响应
          {
              "output": {
                  "task_status": "PENDING",
                  "task_id": "0385dc79-5ff8-4d82-bcb6-xxxxxx"
              },
              "request_id": "4909100c-7b5a-9f92-bfe5-28c7cece6b47"
          }
          ```

          ```json 异常响应
          {
              "code":"InvalidApiKey",
              "message":"Invalid API-key provided.",
              "request_id":"fb53c4ec-1c12-4fc4-a580-cdb7c3261fc1"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="481754a27e8sb" />

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
        #### 请求头（Headers） <span id="5043fa882c26r" />

        <strong>Authorization</strong>*string*<strong>必选</strong>

        推荐使用阿里云百炼API-Key，也可填DashScope API-Key。例如：Bearer d1xxx2a。

        #### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong>*string*<strong>必选</strong>

        任务id。
      </td>

      <td>
        <Tabs>
          <Tab title="获取任务结果">
            ```bash
            curl --location --request GET 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}' \
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
        #### 响应 <span id="fcab464334gzq" />

        <strong>output</strong>*object*

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> *string*

          任务id。

          <strong>task\_status</strong> *string*

          任务状态。

          - PENDING：排队中
          - RUNNING：处理中
          - SUSPENDED：挂起
          - SUCCEEDED：执行成功
          - FAILED：执行失败
          - UNKNOWN：任务不存在或状态未知

          <strong>results</strong>*array object*

          任务结果列表，包括图像URL。

          <Accordion title="属性" defaultOpen>
            <strong>url</strong> *string*

            模型生成图片的URL地址。
          </Accordion>

          <strong>task\_metrics</strong> *object*

          任务信息统计指标。

          <Accordion title="属性" defaultOpen>
            <strong>TOTAL</strong> *integer*

            总的任务数。

            <strong>SUCCEEDED</strong> *integer*

            任务状态为成功的任务数。

            <strong>FAILED</strong> *integer*

            任务状态为失败的任务数。
          </Accordion>

          <strong>submit\_time</strong> *string*

          任务提交时间。

          <strong>scheduled\_time</strong> *string*

          任务排期执行时间。

          <strong>end\_time</strong> *string*

          任务完成时间。

          <strong>result\_url</strong> *string*

          输出图片url。

          <strong>code</strong>*string*

          任务执行失败的错误码。

          <strong>message</strong>*string*

          任务执行失败的详细信息。
        </Accordion>

        <strong>usage</strong>*object*

        输出信息统计。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> *integer*

          模型生成图像的数量。
        </Accordion>

        <strong>request\_id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json expandable
            {
                "request_id": "f24149fe-4722-9763-xxxxxx",
                "output": {
                    "task_id": "9d62befa-0139-4e4d-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-04-24 10:51:35.004",
                    "scheduled_time": "2025-04-24 10:51:35.033",
                    "end_time": "2025-04-24 10:51:59.424",
                    "results": [
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/test_1.png"
                        },
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/test_2.png"
                        }
                    ],
                    "task_metrics": {
                        "TOTAL": 2,
                        "SUCCEEDED": 2,
                        "FAILED": 0
                    }
                },
                "usage": {
                    "image_count": 2
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            ```json
            {
              "request_id": "f24149fe-4722-9763-xxxxxx",
              "output": {
                "task_id": "9d62befa-0139-4e4d-xxxxxx",
                "task_status": "FAILED",
                "submit_time": "2024-05-16 13:50:xx.xxx",
                "scheduled_time": "2024-05-16 13:50:xx.xxx",
                "end_time": "2024-05-16 13:50:xx.xxx",
                "code": "InvalidImageResolution",
                "message": "The input image resolution is too large or small"
              },
              "usage": {
                "image_num": 0
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

此API还有特定状态码，具体如下所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "9.429569%" }} /><col style={{ width: "17.98603%" }} /><col style={{ width: "49.476135%" }} /><col style={{ width: "23.108265%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidFile.Content</p></td><td><p>The input image has no human body or has unclear human body. Please upload other image</p></td><td><p>输入图片中没有人</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>The request is missing required parameters or in a wrong format, please check the parameters that you send.</p></td><td><p>入参格式不对</p></td></tr><tr><td><p>400</p></td><td><p>InvalidURL</p></td><td><p>The request URL is invalid, please check the request URL is available and the request image format is one of the following types: JPEG, JPG, PNG, BMP, and WEBP.</p></td><td><p>输入图片下载失败，请检查网络或者输入格式</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Resolution</p></td><td><p>The image resolution is invalid, please make sure that the aspect ratio is smaller than 2.0, and largest length of image is smaller than 4096</p></td><td><p>上传图片大小不符合要求</p></td></tr><tr><td><p>500</p></td><td><p>InternalError.Algo</p></td><td><p>An internal error occurs during computation, please try this model later.</p></td><td><p>算法运行错误</p></td></tr></tbody></table>
