> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 鞋靴模特API参考

> 本文介绍鞋靴模特模型的输入输出参数。鞋靴模特模型支持输入多视角鞋靴系列图片，同时对输入模特模板图的鞋子区域进行鞋靴AI试穿，实现模特鞋靴布局重绘生成，最终生成图片的效果布局自然、细节丰富、画面细腻、试穿结果逼真。可用于模特商品图设计、新鞋AI试穿、模特穿戴布局重绘等场景。

<meta property="og:title" content="鞋靴模特API参考" />

<strong>相关指南</strong>：[鞋靴模特](/zh/model-studio/shoes-and-boots-model)

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
  - shoemodel-v1 模型当前仅提供<strong>免费体验</strong>，免费额度用完后不可调用且不支持付费。
</Tip>

## 模型概览 <span id="a0c22dbf96vss" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "14.570361%" }} />

    <col style={{ width: "26.027397%" }} />

    <col style={{ width: "15.442092%" }} />

    <col style={{ width: "21.793275%" }} />

    <col style={{ width: "22.166874%" }} />
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
        shoemodel-v1
      </td>

      <td>
        500张
      </td>

      <td>
        目前仅供免费体验。

        > 免费额度用完后不可调用，敬请关注后续动态。
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

## 前提条件 <span id="15044a800dt2u" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="ffa47ca257gkh" />

为了减少等待时间并且避免请求超时，服务采用异步方式提供。您需要发起两个请求：

- <strong>创建任务</strong>：首先发送一个请求创建鞋靴模特任务，该请求会返回任务ID。
- <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询模型生成的结果。

### 步骤1：创建任务获取任务ID <span id="bde0fc73feflg" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/virtualmodel/generation`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求头（Headers） <span id="afcd41d2b57zf" />

        <strong>Authorization</strong>*string*<strong>必选</strong>

        推荐您使用阿里云百炼API-Key，也可填DashScope API-Key。例如：Bearer d1xxx2a。

        <strong>X-DashScope-Async</strong>*string*<strong>必选</strong>

        是否使用DashScope异步调用。HTTP只支持异步调用，设置为`enable`。

        <strong>Content-Type</strong>*string*<strong>必选</strong>

        请求内容类型。固定为`application/json`。

        #### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong>*string*<strong>必选</strong>

        调用模型。鞋靴模特生成模型为`shoemodel-v1`。

        <strong>parameters</strong> Integer<strong>必选</strong>

        图片生成的数量，目前支持 1\~4 张，默认值 1。

        <strong>input</strong>*object*<strong>必选</strong>

        输入图像的基本信息，比如图像URL地址。

        <Accordion title="属性" defaultOpen>
          <strong>template\_image\_url</strong> *string*<strong>必选</strong>

          模板模特图片的URL地址。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图片大小建议小于5M。
          - 图像格式：jpg、png、jpeg、bmp、webp、avif。
          - 图像比例：图长边与短边的比例需在`[2:3, 3:2]` 范围内，推荐比例为`4:3`。

          <strong>shoe\_image\_url</strong> *list* <strong>必选</strong>

          鞋靴多视角图片URL地址。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          - 图片大小建议小于5M。
          - 图像格式：jpg、png、jpeg、bmp、webp、avif。
          - 图像比例：图长边与短边的比例需在`[2:3, 3:2]` 范围内，推荐与模特模板一样，比例为`4:3`。
          - 多视角图片张数小于3。

          <strong>scale</strong> *float* 可选

          控制生成强度。

          范围在\[2.0,8.0]，默认为5.0，数值越大，颜色越鲜亮。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="鞋靴模特试穿">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/virtualmodel/generation' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "shoemodel-v1",
                "input": {
                    "template_image_url": "https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8268778171/p809310.webp",
                    "shoe_image_url": ["https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8268778171/p809301.webp"]
                },
                "parameters":
                {
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
        </Accordion>

        <strong>code</strong>*string*

        任务执行失败的错误码。

        <strong>message</strong>*string*

        任务执行失败的详细信息。

        <strong>request\_id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <CodeGroup>
          ```json 正常响应
          {
              "output": {
          	"task_id": "d76ec1e8-ea27-4038-8913-xxxxxxxxxxxx",
                  "task_status": "PENDING"
              }
              "request_id": "7574ee8f-38a3-4b1e-9280-11c33ab46e51"
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

          <strong>results</strong> *array*

          输出图片列表。每个元素包含一个`url`字段，表示生成图片的URL。

          <strong>code</strong>*string*

          任务执行失败的错误码。

          <strong>message</strong>*string*

          任务执行失败的详细信息。
        </Accordion>

        <strong>request\_id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <CodeGroup>
          ```json 任务执行成功
          {
              "request_id":"<your request id>",
              "output":{
                  "task_id":"<your task id>",
                  "task_status":"SUCCEEDED",
                  "submit_time":"2024-05-16 13:50:xx.xxx",
                  "scheduled_time":"2024-05-16 13:50:xx.xxx",
                  "end_time":"2024-05-16 13:50:xx.xxx",
                  "results":[
                      {
                          "url":"https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx/res_img.png?Expires=xxx&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE"
                      }
                  ],
                  "task_metrics":{
                      "TOTAL":1,
                      "SUCCEEDED":1,
                      "FAILED":0
                  }
              },
              "usage":{
                  "image_count":1
              }
          }
          ```

          ```json 任务执行中
          {
              "request_id":"e5d70b02-ebd3-98ce-9fe8-759d7d7b107d",
              "output":{
                  "task_id":"86ecf553-d340-4e21-af6e-a0c6a421c010",
                  "task_status":"RUNNING",
                  "task_metrics":{
                      "TOTAL":1,
                      "SUCCEEDED":1,
                      "FAILED":0
                  }
              }
          }
          ```

          ```json 任务执行失败
          {
            "request_id": "<your request id>",
            "output": {
              "task_id": "<your task id>",
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
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

此API还有特定状态码，具体如下所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.475189%" }} /><col style={{ width: "18.166526%" }} /><col style={{ width: "39.444912%" }} /><col style={{ width: "26.913373%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>Required parameter(s) missing or invalid, please check the request parameters.（可根据实际情况修改）</p></td><td><p>接口调用参数不合法</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Content</p></td><td><p>The input image has no human body or has unclear human body. Please upload other image.</p></td><td><p>输入图片中人体不完整或者没有人体</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>The request is missing required parameters or in a wrong format, please check the parameters that you send.</p></td><td><p>入参格式不对</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.DataInspection</p></td><td><p>Unable to download the media resource during the data inspection process.</p></td><td><p>数据检查过程中无法下载媒体资源，请检查输入URL是否可访问以及图片格式是否正确</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Resolution</p></td><td><p>The image resolution is invalid, please make sure that the aspect ratio is smaller than 3:2, and largest length of image is smaller than 4096.</p></td><td><p>上传图片大小不符合要求</p></td></tr><tr><td><p>500</p></td><td><p>InternalError.Algo</p></td><td><p>An internal error occurs during computation, please try this model later.</p></td><td><p>算法运行错误</p></td></tr></tbody></table>
