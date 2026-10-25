> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 图像擦除补全API参考

> 本文介绍图像擦除补全模型的输入输出参数。图像擦除补全通过指定图像mask中要删除的人体、宠物、物品、文字、水印等图像区域，在保留背景的同时移除图像中的一个或多个人物、物体、文字等元素。

<meta property="og:title" content="图像擦除补全API参考" />

<strong>相关指南</strong>：[图像擦除补全](/zh/model-studio/image-erasure-and-completion)

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
  - image-erase-completion 模型当前仅提供<strong>免费体验</strong>，免费额度用完后不可调用且不支持付费，推荐参考[图像编辑-千问](/zh/model-studio/qwen-image-edit-guide)或[图像编辑-万相2.1](/zh/model-studio/wanx-image-edit)获取替代方案。
</Tip>

## 模型概览 <span id="a0c22dbf96vss" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24.559342%" }} />

    <col style={{ width: "27.262045%" }} />

    <col style={{ width: "16.098707%" }} />

    <col style={{ width: "17.273796%" }} />

    <col style={{ width: "14.80611%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td rowSpan={2}>
        <strong>模型名</strong>
      </td>

      <td rowSpan={2}>
        <strong>免费额度</strong>[（查看）](/zh/model-studio/new-free-quota)
      </td>

      <td rowSpan={2}>
        <strong>计费单价</strong>
      </td>

      <td colSpan={2}>
        <strong>限流（含主账号与RAM子账号）</strong>
      </td>
    </tr>

    <tr>
      <td>
        <strong>任务下发接口QPS限制</strong>
      </td>

      <td>
        <strong>同时处理中任务数量</strong>
      </td>
    </tr>

    <tr>
      <td>
        image-erase-completion
      </td>

      <td>
        500张
      </td>

      <td>
        目前仅供免费体验。

        > 免费额度用完后不可调用，推荐参考[图像编辑-千问](/zh/model-studio/qwen-image-edit-guide)或[图像编辑-万相2.1](/zh/model-studio/wanx-image-edit)获取替代方案。
      </td>

      <td>
        2
      </td>

      <td>
        1
      </td>
    </tr>
  </tbody>
</table>

## 前提条件 <span id="0e41425c577u4" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="42703589880ts" />

为了减少等待时间并且避免请求超时，服务采用异步方式提供。您需要发起两个请求：

- <strong>创建任务</strong>：首先发送一个请求创建文生图任务，该请求会返回任务ID。
- <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询模型生成的结果。

### 步骤1：创建任务获取任务ID <span id="63a6f712191qa" />

`POST https://dashscope.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求头（Headers） <span id="afcd41d2b57zf" />

        <strong>Content-Type</strong>*string*<strong>必选</strong>

        请求内容类型。固定为`application/json`。

        <strong>Authorization</strong>*string*<strong>必选</strong>

        推荐您使用阿里云百炼API-Key，也可填DashScope API-Key。例如：Bearer d1xxx2a。

        <strong>X-DashScope-Async</strong>*string*<strong>必选</strong>

        是否开启异步处理。必须开启异步处理，设置为`enable`。

        #### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong>*string*<strong>必选</strong>

        调用模型。

        <strong>input</strong>*object*<strong>必选</strong>

        输入图像的基本信息，比如图像URL。

        <Accordion title="属性" defaultOpen>
          <strong>image\_url</strong>*string*<strong>必选</strong>

          输入图像URL地址或者图像base64数据。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图片格式：JPEG、PNG，JPG，BMP，WEBP。
          - 图像分辨率：不低于512×512像素且不超过4096×4096像素。
          - 图像单边长度范围：\[512, 4096]，单位像素。
          - 图片大小：不超过10MB。
          - URL地址中不能包含中文字符。

          <strong>mask\_url</strong>*string*<strong>必选</strong>

          输入擦除区域掩码图像URL地址或者图像base64数据。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图片格式：JPG、JPEG、PNG、HEIF、WEBP。
          - 图像分辨率：不低于512×512像素且不超过4096×4096像素。
          - 图像单边长度范围：\[512, 4096]，单位像素。
          - 图片大小：不超过10MB。

          在与图像相同大小的空白画布进行涂抹，非0值区域为擦除掩码区域；也可选用[人物实例分割](/zh/model-studio/image-instance-segmentation-api-reference)直接产生人物的分割结果作为擦除掩码区域。

          <strong>foreground\_url</strong>*string* 可选

          输入保留区域掩码图像URL地址或者图像base64数据。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图片格式：JPG、JPEG、PNG、HEIF、WEBP。
          - 图像分辨率：不低于512×512像素且不超过4096×4096像素。
          - 图像单边长度范围：\[512, 4096]，单位像素。
          - 图片大小：不超过10MB。

          在与图像相同大小的空白画布进行涂抹，非0值区域为保留掩码区域；也可选用[人物实例分割](/zh/model-studio/image-instance-segmentation-api-reference)，通过指定人物ID的方式分离该人物的掩码区域，保留该人物不被擦除。
        </Accordion>

        <strong>parameters</strong>*object* 可选

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>fast\_mode</strong>*bool*<strong>可选</strong>

          是否为快速模式，默认为false，快速模式推理耗时约为非快速模式的四分之一，适合不需要生成大量细节的场景。

          <strong>dilate\_flag</strong>*bool*<strong>可选</strong>

          默认为true，建议若擦除mask为算法分割结果，设置为true；若擦除mask为涂抹结果，设置为false。

          <strong>add\_watermark</strong> *boolean*<strong>可选</strong>

          添加`Generated by AI`水印。默认值为true，在输出图像左下角处添加水印。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="图像擦除补全">
            ```bash
            curl --location --request POST 'https://dashscope.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data-raw '{
                "model": "image-erase-completion",
                "input": {
                        "image_url": "http://xxx/input.png",
                        "mask_url": "http://xxx/mask.png",
                        "foreground_url": "http://xxx/foreground.png"
                    },
                "parameters":{
                    "dilate_flag":true
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
        #### 响应 <span id="9ea3a66e2a0uv" />

        <strong>output</strong>*object*

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>*string*

          任务id，任务唯一标识。

          <strong>task\_status</strong>*string*

          任务状态。

          - PENDING：排队中
          - RUNNING：处理中
          - SUSPENDED：挂起
          - SUCCEEDED：执行成功
          - FAILED：执行失败
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
                  "task_id": "53950fb7-281a-4e60-b543-xxxxxxxxxxxx"
              },
              "request_id": "1027557e-8c3f-9db5-8cd2-xxxxxxxxxxxx"
          }
          ```

          ```json 异常响应
          {
              "code":"InvalidApiKey",
              "message":"Invalid API-key provided.",
              "request_id":"fb53c4ec-1c12-4fc4-a580-xxxxxxxxxxxx"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="481754a27e8sb" />

`GET https://dashscope.aliyuncs.com/api/v1/tasks/{task_id}`

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

        API-Key，例如：Bearer d1\*\*2a。

        #### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong>*string*<strong>必选</strong>

        任务id。
      </td>

      <td>
        <Tabs>
          <Tab title="获取任务结果">
            ```bash
            curl -X GET \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            https://dashscope.aliyuncs.com/api/v1/tasks/53950fb7-281a-4e60-b543-xxxxxxxxxxxx
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
        #### 响应 <span id="b14ec019d4k6f" />

        <strong>output</strong>*object*

        输出的任务信息。

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

          <strong>task\_metrics</strong> *object*

          任务统计信息。

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

          任务执行时间。

          <strong>end\_time</strong> *string*

          任务完成时间。

          <strong>output\_image\_url</strong> *string*

          输出图像URL地址。

          <strong>code</strong>*string*

          接口错误码。接口成功请求不会返回该参数。

          <strong>message</strong>*string*

          接口错误信息。接口成功请求不会返回该参数。
        </Accordion>

        <strong>usage</strong>*object*

        输出信息统计。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong>integer

          生成图像的数量。
        </Accordion>

        <strong>request\_id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            对于本模型，任务在结束之后的状态会持续保留24小时以备客户随时查询，24小时之后，任务将从系统中清除，相关的结果也将一并清除；对应的，任务生成的结果为图像的URL地址，出于安全考虑，该URL的下载有效期也是24小时，需要用户在获取任务结果后根据需要及时使用或者转存。

            ```json
            {
                "request_id": "b67df059-ca6a-9d51-afcd-9b3c4456b1e2",
                "output": {
                    "task_id": "53950fb7-281a-4e60-b543-xxxxxxxxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2024-05-16 13:50:01.247",
                    "scheduled_time": "2024-05-16 13:50:01.354",
                    "end_time": "2024-05-16 13:50:27.795",
                    "output_image_url": "http://xxx/result.png"
                },
                "usage": {
                    "image_count": 1
                }
            }
            ```
          </Tab>

          <Tab title="任务执行中">
            ```json
            {
                "request_id":"7574ee8f-38a3-4b1e-9280-11c33ab46e51",
                "output":{
                    "task_id":"53950fb7-281a-4e60-b543-xxxxxxxxxxxx",
                    "task_status":"RUNNING",
                    "task_metrics":{
                        "TOTAL":1,
                        "SUCCEEDED":1,
                        "FAILED":0
                    }
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            ```json
            {
               "request_id":"4246a1de-2aab-9b49-ba87-e0d12e221a06",
               "output":{
                  "task_id":"53950fb7-281a-4e60-b543-xxxxxxxxxxxx",
                  "task_status":"FAILED",
                  "submit_time":"2024-03-22 12:07:07.183",
                  "scheduled_time":"2024-03-22 12:07:07.253",
                  "end_time":"2024-03-22 12:07:07.604",
                  "code":"InternalError.Algo",
                  "message":"video generation error"
               }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 状态码说明 <span id="eYRtD" />

DashScope阿里云百炼模型服务通用状态码请查阅：[错误码](/zh/model-studio/error-code)。

此API还有特定状态码，具体如下所示：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "14.330218%" }} /><col style={{ width: "23.831776%" }} /><col style={{ width: "41.588785%" }} /><col style={{ width: "20.249221%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter.JsonPhrase</p></td><td><p>input json error</p></td><td><p>输入JSON错误</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.FileDownload</p></td><td><p>oss download error</p></td><td><p>输入图像下载失败</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.ImageFormat</p></td><td><p>read image error</p></td><td><p>读取图像失败</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.ImageContent</p></td><td><p>The image content does not comply with green network verification</p></td><td><p>图像内容不合规</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>the parameters must conform to the specification: xxx</p></td><td><p>输入参数值超出范围</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.ImageResolution</p></td><td><p>the size of input image is too small or too large</p></td><td><p>输入图像的尺寸过小或者过大</p></td></tr><tr><td><p>500</p></td><td><p>InternalError.Algo</p></td><td><p>algorithm process error</p></td><td><p>算法错误</p></td></tr><tr><td><p>500</p></td><td><p>InternalError.FileUpload</p></td><td><p>oss upload error</p></td><td><p>文件上传失败</p></td></tr></tbody></table>
