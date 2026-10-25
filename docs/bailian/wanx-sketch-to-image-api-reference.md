> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-涂鸦作画API参考

> 本文介绍万相-涂鸦作画模型的API输入输出参数。

<meta property="og:title" content="万相-涂鸦作画API参考" />

<strong>相关指南</strong>：[涂鸦作画](/zh/model-studio/sketch-to-image)

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议从 `https://dashscope.aliyuncs.com` 迁移至新域名。

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## 模型概览 <span id="0594943e081vt" />

<strong>模型效果示意</strong>

<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/2400704371/p883780.png" alt="image" width={1000} height={254.841} />

<strong>模型简介</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.814988%" }} /><col style={{ width: "73.185012%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th></tr></thead><tbody><tr><td><p>wanx-sketch-to-image-lite</p></td><td><p>万相-涂鸦作画通过手绘图案和文字描述，生成精美的涂鸦绘画作品。</p></td></tr></tbody></table>

<strong>模型说明</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "23.200859%" }} /><col style={{ width: "11.063373%" }} /><col style={{ width: "21.160043%" }} /><col style={{ width: "18.689581%" }} /><col style={{ width: "25.886144%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th rowSpan={2}><p><strong>计费单价</strong></p></th><th colSpan={2}><p><strong>限流（主账号与RAM子账号共用）</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a><strong /></p></th></tr><tr><th><p><strong>任务下发接口QPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>wanx-sketch-to-image-lite</p></td><td><p>0.06元/张</p></td><td><p>2</p></td><td><p>1</p></td><td><p>500张</p></td></tr></tbody></table>

更多说明请参见[模型计费及限流](/zh/model-studio/wanx-sketch-to-image-api-reference#b8457b7223zhp)。

## 前提条件 <span id="8da443f305l3y" />

涂鸦作画API支持通过HTTP和DashScope SDK进行调用。

在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。目前，该SDK已支持Python和Java。

> 请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="a3ee779e75j4s" />

图像模型处理时间较长，为了避免请求超时，HTTP调用仅支持异步获取模型结果。您需要发起两个请求：

1. <strong>创建任务获取任务ID</strong>：首先发起创建任务请求，该请求会返回任务ID（task\_id）。
2. <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询任务状态及结果。任务成功执行时将返回图像URL，有效期24小时。

<Note>
  创建任务后，该任务将被加入到排队队列，等待调度执行。后续需要调用“根据任务ID查询结果接口”获取任务状态及结果。
</Note>

### 步骤1：创建任务获取任务ID <span id="5ed645237cwmf" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求参数 <span id="afcd41d2b57zf" />

        ##### 请求头（Headers） <span id="1799f28b13lo1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        <strong>X-DashScope-WorkSpace</strong>`string`（可选）

        阿里云百炼业务空间ID。示例值：llm-xxxx。

        您可以在此[获取Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h)。

        <Accordion title="详细说明">
          此参数根据阿里云百炼API Key进行填写。

          - 若为主账号API Key，可不填。不填则使用主账号权限，填写则使用对应的业务空间权限。
          - 若为RAM子账号API Key，则必填。RAM子账号一定归属于某个业务空间。

          业务空间必须具备访问模型的权限，才能调用API。若无权限，请参考[授权子业务空间模型调用、训练和部署](/zh/model-studio/use-workspace)。

          > 关于如何区分阿里云百炼主账号和RAM子账号，请参考[主账号管理](/zh/model-studio/business-space-management)。
        </Accordion>

        ##### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        调用模型。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入的基本信息，比如提示词、图像URL地址。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong>`string`<strong>（必选）</strong>

          提示词，用来描述生成图像中期望包含的元素和视觉特点。

          支持中英文，长度不超过75个字符，超过部分会自动截断。

          示例值：一棵参天大树。

          <strong>sketch\_image\_url</strong>`string`<strong>（必选）</strong>

          输入草图的URL地址。输入草图需要与输出图像的分辨率比例保持一致，否则会导致图片拉伸变形，建议使用白色背景图。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图像格式：JPG、JPEG、PNG、TIFF、WEBP。
          - 图像分辨率：不小于256×256像素且不超过2048×2048像素。
          - 图像大小：不超过10 MB。
          - URL地址中不能包含中文字符。

          草图示例：

          <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9289386271/p850798.png" alt="image" className="inline" noZoom width={150} height={151.166} />
        </Accordion>

        <strong>parameters</strong>`object`（可选）

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>style</strong>`string`（可选）

          输出图像的风格，目前支持以下风格取值：

          - \<auto>：默认值，由模型随机输出图像风格。
          - \<3d cartoon>：3D卡通。
          - \<anime>：二次元。
          - \<oil painting>：油画。
          - \<watercolor>：水彩。
          - \<sketch>：素描。
          - \<chinese painting>：中国画。
          - \<flat illustration>：扁平插画。

          <strong>size</strong>`string`（可选）

          输出图像的分辨率。目前仅支持一种图像分辨率：768\*768，且为默认值。

          <strong>n</strong>`integer`（可选）

          生成图片的数量。取值范围为1\~4张，默认为4。

          <strong>sketch\_weight</strong>`integer`（可选）

          输入草图对输出图像的约束程度。

          取值范围为0-10，取值间隔为1， 默认值为10。取值越大表示输出图像跟输入草图越相似。

          <strong>sketch\_extraction</strong>`boolean`（可选）

          如果上传图片是RGB图片，而非草图（sketch线稿），此参数可控制是否对输入图片进行sketch边缘提取。

          默认值为False，表示不进行提取。设置为True时，表示进行提取，此时，`sketch_color`字段失效。

          <strong>sketch\_color</strong>`array`（可选）

          此字段在`sketch_extraction=false`时生效，所包含数值均被视为画笔色，其余数值均会视为背景色。模型会基于一种或多种画笔色描绘的区域生成新的画作。默认值为\[]。

          当sketch\_image\_url线稿中的线条不是黑色，而是包含其他一种或多种颜色时，可以指定一个或多个RGB颜色数值作为画笔色。

          示例值：\[\[134, 134, 134], \[0, 0, 0]]
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="curl">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "wanx-sketch-to-image-lite",
                "input": {
                    "sketch_image_url": "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6609471071/p743851.jpg",
                    "prompt": "一棵参天大树"
                },
                "parameters": {
                    "size": "768*768",
                    "n": 2,
                    "sketch_weight": 3,
                    "style": "<watercolor>"
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
        #### 响应参数 <span id="b682d663e9dv4" />

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

### 步骤2：根据任务ID查询结果 <span id="edbef6b3ackew" />

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

## DashScope SDK调用 <span id="db17b3098c2iv" />

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
    import dashscope
    from dashscope import ImageSynthesis
    import os

    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    prompt = "一棵参天大树"
    sketch_image_url = "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6609471071/p743851.jpg"
    model = "wanx-sketch-to-image-lite"
    task = "image2image"

    print('----sync call, please wait a moment----')
    rsp = ImageSynthesis.call(api_key=os.getenv("DASHSCOPE_API_KEY"),
                              model=model,
                              prompt=prompt,
                              n=1,
                              style='<watercolor>',
                              size='768*768',
                              sketch_image_url=sketch_image_url,
                              task=task)
    print('response: %s' % rsp)
    if rsp.status_code == HTTPStatus.OK:
        print(rsp.output)
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
    import dashscope
    from dashscope import ImageSynthesis
    import os

    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    prompt = "一棵参天大树"
    sketch_image_url = "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6609471071/p743851.jpg"
    model = "wanx-sketch-to-image-lite"
    task = "image2image"

    # 异步调用
    def async_call():
        print('----create task----')
        task_info = create_async_task()
        print('----wait task done then save image----')
        wait_async_task(task_info)

    # 创建异步任务
    def create_async_task():
        rsp = ImageSynthesis.async_call(api_key=os.getenv("DASHSCOPE_API_KEY"),
                                        model=model,
                                        prompt=prompt,
                                        n=1,
                                        style='<watercolor>',
                                        size='768*768',
                                        sketch_image_url=sketch_image_url,
                                        task=task)
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
    import com.alibaba.dashscope.utils.Constants;

    public class Main {
        static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

        public void syncCall() {
            String prompt = "一棵参天大树";
            String sketchImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6609471071/p743851.jpg";
            String model = "wanx-sketch-to-image-lite";
            ImageSynthesisParam param = ImageSynthesisParam.builder()
                    .model(model)
                    .prompt(prompt)
                    .n(1)
                    .size("768*768")
                    .sketchImageUrl(sketchImageUrl)
                    .style("<watercolor>")
                    .build();

            String task = "image2image";
            ImageSynthesis imageSynthesis = new ImageSynthesis(task);
            ImageSynthesisResult result = null;
            try {
                System.out.println("---sync call, please wait a moment----");
                result = imageSynthesis.call(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));
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
            String prompt = "一棵参天大树";
            String sketchImageUrl = "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6609471071/p743851.jpg";
            String model = "wanx-sketch-to-image-lite";
            ImageSynthesisParam param = ImageSynthesisParam.builder()
                    .model(model)
                    .prompt(prompt)
                    .n(1)
                    .size("768*768")
                    .sketchImageUrl(sketchImageUrl)
                    .style("<watercolor>")
                    .build();

            String task = "image2image";
            ImageSynthesis imageSynthesis = new ImageSynthesis(task);
            ImageSynthesisResult result = null;
            try {
                result = imageSynthesis.asyncCall(param);
            } catch (Exception e){
                throw new RuntimeException(e.getMessage());
            }
            String taskId = result.getOutput().getTaskId();
            System.out.println("taskId=" + taskId);
            return taskId;
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

    <strong>1、步骤1：创建任务获取任务ID的响应示例</strong>

    ```json
    {
    	"request_id": "5dbf9dc5-4f4c-9605-85ea-542f97709ba8",
    	"output": {
    		"task_id": "7277e20e-aa01-4709-xxxxxxxx",
    		"task_status": "PENDING"
    	}
    }
    ```

    <strong>2、步骤2：根据任务ID查询结果的响应示例</strong>

    ```json
    {
    	"request_id": "c44213ba-7aa3-91e4-97c1-c527ade82597",
    	"output": {
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
    	},
    	"usage": {
    		"image_count": 1
    	}
    }
    ```
  </Tab>
</Tabs>

## 错误码 <span id="efc117f59a2gm" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

此API还有特定状态码，具体如下所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.177763%" }} /><col style={{ width: "19.354839%" }} /><col style={{ width: "41.592313%" }} /><col style={{ width: "25.875086%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter.DataInspection</p></td><td><p>Unable to download the media resource during the data inspection process.</p></td><td><p>输入图片无法下载，请检查URL地址是否正确且可访问。</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>Value error, format of image \{url} is not valid : payload.input.sketch</p></td><td><p>输入图片格式不合法，请确认图片格式为JPG、JPEG、PNG、TIFF或WEBP。</p></td></tr></tbody></table>

## 常见问题 <span id="45b31d4f7bk24" />

### 模型计费及限流 <span id="b8457b7223zhp" />

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
