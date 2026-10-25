> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-图像局部重绘API参考

> 本文介绍万相-图像局部重绘模型的输入输出参数。

<meta property="og:title" content="万相-图像局部重绘API参考" />

<strong>相关指南</strong>：[图像局部重绘](/zh/model-studio/vary-region)

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
  - wanx-x-painting 模型当前仅提供<strong>免费体验</strong>，免费额度用完后不可调用且不支持付费，推荐参考[图像编辑-千问](/zh/model-studio/qwen-image-edit-guide)或[图像编辑-万相2.1](/zh/model-studio/wanx-image-edit)获取替代方案。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议从 `https://dashscope.aliyuncs.com` 迁移至新域名。

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## 模型概览 <span id="0594943e081vt" />

<strong>模型效果示意</strong>

![image](https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/1300704371/p885604.png)

<strong>模型简介</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.814988%" }} /><col style={{ width: "73.185012%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th></tr></thead><tbody><tr><td><p>wanx-x-painting</p></td><td><p>万相-图像局部重绘根据用户输入的原始图片、局部区域涂抹图和prompt提示词文字内容，在涂抹区域生成与文字描述相对应的内容，而涂抹区域外的部分则基本保持不变。</p></td></tr></tbody></table>

<strong>模型说明</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "17.726799%" }} />

    <col style={{ width: "17.622523%" }} />

    <col style={{ width: "22.523462%" }} />

    <col style={{ width: "20.542231%" }} />

    <col style={{ width: "21.584984%" }} />
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
        <strong>任务下发接口QPS限制</strong>
      </th>

      <th>
        <strong>同时处理中任务数量</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        wanx-x-painting
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

      <td>
        500张
      </td>
    </tr>
  </tbody>
</table>

更多说明请参见[模型计费及限流说明](/zh/model-studio/vary-region-api-reference#b8457b7223zhp)。

## 前提条件 <span id="8e0253f790lhf" />

图像局部重绘API支持通过HTTP和DashScope SDK进行调用。

在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。目前，该SDK已支持Python和Java。

> 请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="65796f7146azl" />

图像模型处理时间较长，为了避免请求超时，HTTP调用仅支持异步获取模型结果。您需要发起两个请求：

1. <strong>创建任务获取任务ID</strong>：首先发起创建任务请求，该请求会返回任务ID（task\_id）。
2. <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询任务状态及结果。任务成功执行时将返回图像URL，有效期24小时。

<Note>
  创建任务后，该任务将被加入到排队队列，等待调度执行。后续需要调用“根据任务ID查询结果接口”获取任务状态及结果。
</Note>

### 步骤1：创建任务获取任务ID <span id="f1a4f32d54adq" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="afcd41d2b57zf" />

        ##### 请求头（Headers） <span id="040b1c0e468bz" />

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

        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。示例值：wanx-x-painting。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入的基本信息，如提示词、图像URL等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong>`string`<strong>（必选）</strong>

          正向提示词，用来描述生成图像中期望包含的元素和视觉特点。

          支持中英文，长度不超过75个字符，超过部分会自动截断。

          <strong>base\_image\_url</strong>`string`<strong>（必选）</strong>

          输入图像URL地址，不支持填写图像Base64数据。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图像格式：JPG、JPEG、PNG、BMP。
          - 图像分辨率：大于256×256像素，小于4096×4096像素。
          - 图像大小：不超过10MB。
          - URL地址中不能包含中文字符。

          <strong>mask\_image\_url</strong>`string`<strong>（必选）</strong>

          用户标记涂抹区域的图像URL地址，需要和base\_image\_url图像分辨率保持一致。不支持填写图像Base64数据。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图像格式：JPG、JPEG、PNG、BMP。
          - 图像分辨率：大于256×256像素，小于4096×4096像素。
          - 图像大小：不超过10MB。
          - URL地址中不能包含中文字符。

          您可以使用PS或者其他图像编辑工具在原图上涂抹获得掩码图像。
        </Accordion>

        <strong>parameters</strong>`object`（可选）

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>style</strong>`string`（可选）

          输出图像的风格。目前支持以下风格取值：

          - \<auto>： 默认。
          - \<3d cartoon>：3D卡通。
          - \<anime>：动画。
          - \<oil painting>： 油画。
          - \<watercolor>：水彩。
          - \<sketch>：素描。
          - \<chinese painting>：中国画。
          - \<flat illustration>：扁平插画。

          <strong>size</strong>`string`（可选）

          输出图像的分辨率。目前支持3种图像分辨率：

          - 1024\*1024：默认值。
          - 720\*1280
          - 1280\*720

          <strong>n</strong>`integer`（可选）

          生成图片的数量。取值范围为1\~4张，默认为1。

          <strong>mask\_color</strong>`array`（可选）

          RGB颜色数值列表，用于指定掩码图片中表示涂改区域的颜色。默认值为\[]。

          当该字段为空，默认对掩码图片进行二值化处理（处理后白色代表涂抹区域）。当该字段非空时，表示RGB颜色数值列表，代表一种或多种颜色。这些RGB颜色所描绘的区域即为涂改区域，如\[0,0,0]和\[134,134,134]，而未被指定的颜色则视为背景色。

          示例值：\[\[0, 0, 0], \[134, 134, 134]]。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="图像局部重绘">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
              "model": "wanx-x-painting",
              "input": {
                "prompt": "一只狗戴着红色眼镜",
                "base_image_url": "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/source3.jpg",
                "mask_image_url": "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/glasses.png"
              },
              "parameters": {
                "size": "1024*1024",
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
        #### 响应参数 <span id="a69639162eu4i" />

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

### 步骤2：根据任务ID查询结果 <span id="25d6501e22xb4" />

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="c7a5b38b94icr" />

        #### 请求头（Headers） <span id="6cbbceaeb6jyo" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        #### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            请将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id。

            > 若使用新加坡地域的模型，需将base\_url替换为https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx，其中\{WorkspaceId}需替换为真实的业务空间ID。

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
        #### 响应参数 <span id="3f1ae57ce6qd5" />

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

          <strong>results</strong> `array of object`

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
                "request_id": "85eaba38-0185-99d7-8d16-4d9135238846",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-a0c6a421c010",
                    "task_status": "SUCCEEDED",
                    "results": [
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/123/a1.png"
                        },
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/123/b2.png"
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
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "e5d70b02-ebd3-98ce-9fe8-759d7d7b107d",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-a0c6a421c010",
                    "task_status": "FAILED",
                    "code": "InvalidParameter",
                    "message": "The size is not match the allowed size ['1024*1024', '720*1280', '1280*720']",
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
                "request_id": "85eaba38-0185-99d7-8d16-4d9135238846",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-a0c6a421c010",
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

## DashScope SDK调用 <span id="34914775efbhb" />

请先确认已安装最新版DashScope SDK：[安装SDK](/zh/model-studio/install-sdk)。

DashScope SDK目前已支持Python和Java。

SDK与HTTP接口的参数名基本一致，参数结构根据不同语言的SDK封装而定。参数说明可参考[HTTP调用](/zh/model-studio/text-to-image-api-reference#42703589880ts)。

由于图像模型处理时间较长，底层服务采用异步方式提供。SDK在上层进行了封装，支持同步、异步两种调用方式。

### Python SDK调用 <span id="a3ad9a3b6d9if" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="3cd4969b00193" />

    ```python expandable
    from http import HTTPStatus
    from urllib.parse import urlparse, unquote
    from pathlib import PurePosixPath
    import requests
    from dashscope import ImageSynthesis

    # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

    prompt = "一只狗戴着红色眼镜"
    model = "wanx-x-painting"
    task = "image2image"
    extra_input = {
        "base_image_url": "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/source3.jpg",
        "mask_image_url": "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/glasses.png"
    }

    print('----sync call, please wait a moment----')
    rsp = ImageSynthesis.call(model=model,
                              prompt=prompt,
                              n=1,
                              size='1024*1024',
                              task=task,
                              extra_input=extra_input)
    if rsp.status_code == HTTPStatus.OK:
        print(rsp)
        # save file to current directory
        for result in rsp.output.results:
            file_name = PurePosixPath(unquote(urlparse(result.url).path)).parts[-1]
            with open('./%s' % file_name, 'wb+') as f:
                f.write(requests.get(result.url).content)
    else:
        print('sync_call Failed, status_code: %s, code: %s, message: %s' %
              (rsp.status_code, rsp.code, rsp.message))
    ```

    ##### 响应示例 <span id="0c0533477emwb" />

    ```json
    {
    	"status_code": 200,
    	"request_id": "4126d9dd-e037-9f32-8d56-6d29ab3f9a06",
    	"code": null,
    	"message": "",
    	"output": {
    		"task_id": "b476bc4e-35c1-4c4e-a4d9-xxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [{
    			"url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxxx.png"
    		}],
    		"submit_time": "2024-11-01 09:50:56.081",
    		"scheduled_time": "2024-11-01 09:50:56.104",
    		"end_time": "2024-11-01 09:51:22.740",
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
    ##### 请求示例 <span id="b994ca9918t7e" />

    ```python expandable
    from http import HTTPStatus
    from urllib.parse import urlparse, unquote
    from pathlib import PurePosixPath
    import requests
    from dashscope import ImageSynthesis

    # 以下为华北2（北京）地域的配置，调用时请将WorkspaceId替换为真实的业务空间ID，各地域的配置不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

    prompt = "一只狗戴着红色眼镜"
    model = "wanx-x-painting"
    task = "image2image"
    extra_input = {
        "base_image_url": "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/source3.jpg",
        "mask_image_url": "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/glasses.png"
    }

    def async_call():
        print('----create task----')
        task_info = create_async_task()
        print('----wait task done then save image----')
        wait_async_task(task_info)

    # 创建异步任务
    def create_async_task():
        rsp = ImageSynthesis.async_call(model=model,
                                        prompt=prompt,
                                        n=1,
                                        size='1024*1024',
                                        task=task,
                                        extra_input=extra_input)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output)
        else:
            print('create_async_task Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))
        return rsp

    # 等待异步任务结束
    def wait_async_task(task):
        rsp = ImageSynthesis.wait(task)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.task_status)
            # save file to current directory
            for result in rsp.output.results:
                file_name = PurePosixPath(unquote(urlparse(result.url).path)).parts[-1]
                with open('./%s' % file_name, 'wb+') as f:
                    f.write(requests.get(result.url).content)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        async_call()
    ```

    ##### 响应示例 <span id="ae28dc8f72g6i" />

    <strong>1、创建任务的响应示例</strong>

    ```json
    {
    	"status_code": 200,
    	"request_id": "31b04171-011c-96bd-ac00-f0383b669cc7",
    	"code": "",
    	"message": "",
    	"output": {
    		"task_id": "4f90cf14-a34e-4eae-xxxxxxxx",
    		"task_status": "PENDING",
    		"results": []
    	},
    	"usage": null
    }
    ```

    <strong>2、查询任务结果的响应示例</strong>

    ```json
    {
    	"status_code": 200,
    	"request_id": "d861d3ba-4b29-9491-abad-266ef4fb2f08",
    	"code": null,
    	"message": "",
    	"output": {
    		"task_id": "4f90cf14-a34e-4eae-xxxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [{
    			"url": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxxx.png"
    		}],
    		"submit_time": "2024-10-31 20:40:35.631",
    		"scheduled_time": "2024-10-31 20:40:35.684",
    		"end_time": "2024-10-31 20:41:02.700",
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

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="3e3609efb9rhl" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.HashMap;
    import com.alibaba.dashscope.utils.Constants;

    public class Main {
        static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

        public void syncCall() {
            String task = "image2image";
            ImageSynthesis imageSynthesis = new ImageSynthesis(task);
            ImageSynthesisParam param = genImageSynthesis();
            ImageSynthesisResult result = null;
            try {
                System.out.println("---sync call, please wait a moment----");
                result = imageSynthesis.call(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));
        }

        private ImageSynthesisParam genImageSynthesis(){
            HashMap<String,Object> extraInputMap = new HashMap<>();
            extraInputMap.put("base_image_url", "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/source3.jpg");
            extraInputMap.put("mask_image_url", "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/glasses.png");
            String prompt = "一只狗戴着红色眼镜";
            String model = "wanx-x-painting";
            return ImageSynthesisParam.builder()
                    .model(model)
                    .prompt(prompt)
                    .n(1)
                    .size("1024*1024")
                    .extraInputs(extraInputMap)
                    .build();
        }

        public static void main(String[] args){
            Main text2Image = new Main();
            text2Image.syncCall();
        }

    }
    ```

    ##### 响应示例 <span id="dfe6c9cc786iy" />

    ```json
    {
    	"request_id": "150edcda-05d5-9ffe-8803-84626d1db623",
    	"output": {
    		"task_id": "f2098ff0-146e-404c-bb25-xxxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [{
    			"url": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxxx.png"
    		}],
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
    ##### 请求示例 <span id="2fc30f223b9py" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.HashMap;
    import com.alibaba.dashscope.utils.Constants;

    public class Main {
        static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

        public void asyncCall() {
            System.out.println("---create task----");
            String taskId = this.createAsyncTask();
            System.out.println("---wait task done then return image url----");
            this.waitAsyncTask(taskId);
        }

        /**
         * 创建异步任务
         * @return taskId
         */
        public String createAsyncTask() {
            String task = "image2image";
            ImageSynthesis imageSynthesis = new ImageSynthesis(task);
            ImageSynthesisParam param = genImageSynthesis();
            ImageSynthesisResult result = null;
            try {
                result = imageSynthesis.asyncCall(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }
            String taskId = result.getOutput().getTaskId();
            System.out.println("taskId=" + taskId);
            return taskId;
        }

        private ImageSynthesisParam genImageSynthesis(){
            String prompt = "一只狗戴着红色眼镜";
            String model = "wanx-x-painting";
            HashMap<String,Object> extraInputMap = new HashMap<>();
            extraInputMap.put("base_image_url", "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/source3.jpg");
            extraInputMap.put("mask_image_url", "http://synthesis-source.oss-accelerate.aliyuncs.com/lingji/validation/mask2img/demo/glasses.png");

            return ImageSynthesisParam.builder()
                    .model(model)
                    .prompt(prompt)
                    .n(1)
                    .size("1024*1024")
                    .extraInputs(extraInputMap)
                    .build();
        }

        /**
         * 等待异步任务结束
         * @param taskId 任务id
         * */
        public void waitAsyncTask(String taskId) {
            ImageSynthesis imageSynthesis = new ImageSynthesis();
            ImageSynthesisResult result = null;
            try {
                // If you have set the DASHSCOPE_API_KEY in the system environment variable, the apiKey can be null.
                result = imageSynthesis.wait(taskId, null);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }

            System.out.println(JsonUtils.toJson(result.getOutput()));
            System.out.println(JsonUtils.toJson(result.getUsage()));
        }

        public static void main(String[] args){
            Main text2Image = new Main();
            text2Image.asyncCall();
        }

    }
    ```

    ##### 响应示例 <span id="38e1e3c7303rm" />

    <strong>1、创建任务的响应示例</strong>

    ```json
    {
    	"request_id": "5dbf9dc5-4f4c-9605-85ea-542f97709ba8",
    	"output": {
    		"task_id": "7277e20e-aa01-4709-xxxxxxxx",
    		"task_status": "PENDING"
    	}
    }
    ```

    <strong>2、查询任务结果的响应示例</strong>

    <strong>output字段内容：</strong>

    ```json
    {
    	"task_id": "7277e20e-aa01-4709-xxxxxxxx",
    	"task_status": "SUCCEEDED",
    	"results": [{
    		"url": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxxx.png"
    	}],
    	"task_metrics": {
    		"TOTAL": 1,
    		"SUCCEEDED": 1,
    		"FAILED": 0
    	}
    }
    ```

    <strong>usage字段内容：</strong>

    ```json
    {
    	"image_count": 1
    }
    ```
  </Tab>
</Tabs>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="45b31d4f7bk24" />

### 模型计费及限流说明 <span id="b8457b7223zhp" />

<strong>免费额度</strong>

- 额度说明：免费额度是指模型成功生成的输出图片数量。输入图片及模型处理失败的情况不占用免费额度。
- 领取方式：开通阿里云百炼大模型服务后自动发放，有效期90天。
- 使用账号：阿里云主账号与其RAM子账号共享免费额度。
- 更多详情请参见[新人免费额度](/zh/model-studio/new-free-quota)。

<strong>限时免费</strong>

- 当计费为限时免费时，表示该模型处于公测阶段，免费额度用尽后不可使用。

<strong>计费说明</strong>

- 当计费有明确单价时，如0.2元/秒，表示该模型已商业化，免费额度用尽或过期后需付费使用。
- 计费项：只对模型成功生成的输出图片进行收费，其余情况暂不计费。
- 付费方式：由阿里云主账号统一付费。RAM子账号不能独立计量计费，必须由所属的主账号付费。如果您需要查询账单信息，请前往阿里云控制台[账单概览](https://billing-cost.console.aliyun.com/finance/month-bill/account)。
- 充值途径：您可以在阿里云控制台[费用与成本](https://billing-cost.console.aliyun.com/home?spm=a2c4g.11186623.0.0.2d543048F4KRQP)页面进行充值。
- 模型调用情况：您可以前往阿里云百炼的[模型监控](https://bailian.console.aliyun.com/cn-beijing/model/telemetry)查看模型调用量及调用次数。
- 更多计费问题请参见[计费项](/zh/model-studio/billing-for-model-studio)。

<strong>限流</strong>

- 限流说明：阿里云主账号与其RAM子账号共享限流限制。

### 图像局部重绘API通用问题 <span id="6af451b0a1ykp" />

1. <strong>输入图像的掩码图像（mask\_image）有什么限制？</strong>

   - 只能使用图像URL地址，暂不支持填写图像Base64数据。
   - 需要和输入图像的分辨率保持一致。
   - 文档中的示例掩码图像为白色背景，实际上，掩码图像并不要求必须为白色背景，可以通过`mask_color`字段指定其他颜色。
2. <strong>如何获得输入图像的掩码图像？</strong>

   您可以使用PS或者其他图像编辑工具在原图上涂抹获得掩码图像。
3. <strong>使用图像局部重绘功能时，若涂抹区域外的图片输出效果不理想，应如何处理？</strong>

   建议您使用[通用图像编辑](/zh/model-studio/wanx-image-edit-api-reference)的局部重绘功能重新生成。
