> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 人物实例分割API参考

> 本文介绍人物实例分割模型的输入输出参数。人物实例分割运用了检测和分割技术，不仅能够在图像中识别出不同的对象，而且还能准确地画出每一个对象边界的像素级掩码（mask）。

<meta property="og:title" content="人物实例分割API参考" />

<strong>相关指南</strong>：[人物实例分割](/zh/model-studio/image-instance-segmentation)

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
  - image-instance-segmentation 模型当前仅提供<strong>免费体验</strong>，免费额度用完后不可调用且不支持付费。
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
        image-instance-segmentation
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

## 前提条件 <span id="5167b4367apns" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="42703589880ts" />

为了减少等待时间并且避免请求超时，服务采用异步方式提供。您需要发起两个请求：

- <strong>创建任务</strong>：首先发送一个请求创建人物实例分割任务，该请求会返回任务ID。
- <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询模型生成的结果。

### 步骤1：创建任务获取任务ID <span id="bde0fc73feflg" />

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

          输入图像URL地址。

          URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。您也可在此[获取临时公网URL](/zh/model-studio/get-temporary-file-url)。

          图像限制：

          - 图片格式：JPEG，PNG，JPG，BMP，WEBP。
          - 图像分辨率：不低于512×512像素且不超过4096×4096像素。
          - 图像单边长度范围：\[512, 4096]，单位像素。
          - 图片大小：不超过10M。
          - URL地址中不能包含中文字符。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="人物实例分割">
            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data-raw '{
                "model": "image-instance-segmentation",
                "input": {
                        "image_url": "http://xxx/image.png"
                    },
                "parameters":{
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

        <strong>request\_id（可选）</strong>*string*

        本次请求的系统唯一码。
      </td>

      <td>
        <CodeGroup>
          ```json 成功响应
          {
              "output": {
                  "task_status": "PENDING",
                  "task_id": "53950fb7-281a-4e60-xxxxxxxxxxxx"
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

        API-Key，例如：Bearer d1\*\*2a。

        #### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong>*string*<strong>必选</strong>

        任务id。
      </td>

      <td>
        <Tabs>
          <Tab title="获取任务结果">
            ```bash
            # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            curl -X GET \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/53950fb7-281a-4e60-xxxxxxxxxxxx
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

        <strong>request\_id</strong>*string*

        本次请求的系统唯一码。

        <strong>status\_code</strong>*int*

        200（HTTPStatus.OK）表示请求成功，否则表示请求失败，可以通过code获取错误码，通过message字段获取错误详细信息。

        <strong>code</strong>*string*

        如果失败表示错误码，参考错误码表。

        <strong>message</strong>*string*

        如果失败，内容为失败详细信息。

        <strong>output</strong>*object*

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>*string*

          任务id。

          <strong>task\_status</strong>*string*

          任务状态。

          - PENDING：排队中。
          - RUNNING：处理中。
          - SUCCEEDED：成功。
          - FAILED：失败。
          - UNKNOWN：任务不存在或状态未知。

          <strong>output\_image\_url</strong>*string*

          分割后图像URL地址。限制输出图像长宽比范围为1:10-10:1。

          <strong>output\_vis\_image\_url</strong>*string*

          分割后可视化图像的URL地址，限制输出图像长宽比范围为1:10-10:1。
        </Accordion>

        <strong>usage</strong>*object*

        输出信息统计。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong>*integer*

          本次请求生成图像数量。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            对于本模型，任务在结束之后的状态会持续保留24小时以备客户随时查询，24小时之后，任务将从系统中清除，相关的结果也将一并清除；对应的，任务生成的结果为图像的URL地址，出于安全考虑，该URL的下载有效期也是24小时，需要用户在获取任务结果后根据需要及时使用或者转存。

            ```json
            {
                "request_id": "b67df059-ca6a-9d51-afcd-9b3c4456b1e2",
                "output": {
                    "task_id": "53950fb7-281a-4e60-xxxxxxxxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2024-05-16 13:50:01.247",
                    "scheduled_time": "2024-05-16 13:50:01.354",
                    "end_time": "2024-05-16 13:50:27.795",
                    "output_image_url": "http://xxx/result1.png",
                    "output_vis_image_url":"http://xxx/result2.png"
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
                "request_id":"7574ee8f-38a3-4b1e-xxxxxxxxxxxx",
                "output":{
                    "task_id":"53950fb7-281a-4e60-xxxxxxxxxxxx",
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
                "request_id": "dccfdf23-b38e-97a6-a07b-f35118c1ada6",
                "output": {
                    "task_id": "53950fb7-281a-4e60-xxxxxxxxxxxx",
                    "task_status": "FAILED",
                    "submit_time": "2024-05-16 14:15:14.103",
                    "scheduled_time": "2024-05-16 14:15:14.154",
                    "end_time": "2024-05-16 14:15:14.694",
                    "code": "InvalidParameter.FileDownload",
                    "message": "download for input_image error"
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 补充：如何从人物实例分割掩码图中获取保留人物和待擦除人物掩码图？ <span id="RWDRR" />

以下代码示例将得到的人物实例分割结果图拆分成擦除区域图以及保留区域图，后续调用图像擦除模型擦除不需要的人像。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.029797%" }} /><col style={{ width: "25.029797%" }} /><col style={{ width: "25.029797%" }} /><col style={{ width: "24.910608%" }} /></colgroup><thead><tr><th><p><strong>原图</strong></p></th><th><p><strong>人物实例分割结果</strong></p></th><th><p><strong>保留区域</strong></p></th><th><p><strong>擦除区域</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0184161571/p840837.png" alt="图片擦除2-原图.png" className="inline" noZoom height={150} /></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6043984271/p841094.png" alt="image" className="inline" noZoom height={150} /></p> <p>mask.png</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0184161571/p840841.png" alt="图片擦除2-保留.png" className="inline" noZoom height={150} /></p> <p>reserve.png</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0184161571/p840839.png" alt="图片擦除2-擦除.png" className="inline" noZoom height={150} /></p> <p>remove.png</p></td></tr></tbody></table>

```python expandable
import cv2
import numpy as np

def get_remove_mask(mask_path, remove_mask_path, reserve_ids=[]):
    mask = cv2.imread(mask_path)
    instance_num = int(mask.max())
    new_mask = np.zeros((mask.shape[0], mask.shape[1], 3))
    for i in range(1, instance_num+1):
        if i in reserve_ids:
            continue
        new_mask[mask[:, :, 0] == i] = (255, 255, 255)
    cv2.imwrite(remove_mask_path, new_mask.astype(np.uint8))

def get_reserve_mask(mask_path, reserve_mask_path, reserve_ids=[]):
    mask = cv2.imread(mask_path)
    instance_num = int(mask.max())
    new_mask = np.zeros((mask.shape[0], mask.shape[1], 3))
    for i in range(1, instance_num + 1):
        if i in reserve_ids:
            new_mask[mask[:,:,0] == i]  = (255, 255, 255)
    cv2.imwrite(reserve_mask_path, new_mask.astype(np.uint8))

if __name__ == '__main__':
    mask_path = 'mask.png'
    remove_mask_path = 'remove.png'
    reserve_mask_path = 'reserve.png'
    get_remove_mask(mask_path, remove_mask_path, reserve_ids=[1])
    get_reserve_mask(mask_path, reserve_mask_path, reserve_ids=[1])
```

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

此API还有特定状态码，具体如下所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.161994%" }} /><col style={{ width: "21.573209%" }} /><col style={{ width: "40.88785%" }} /><col style={{ width: "24.376947%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter.JsonPhrase</p></td><td><p>input json error</p></td><td><p>输入JSON错误</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.FileDownload</p></td><td><p>oss download error</p></td><td><p>输入图像下载失败</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.ImageFormat</p></td><td><p>read image error</p></td><td><p>读取图像失败</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter.ImageContent</p></td><td><p>The image content does not comply with green network verification.</p></td><td><p>图像内容不合规</p></td></tr><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>the parameters must conform to the specification: xxx</p></td><td><p>输入参数超出范围</p></td></tr><tr><td><p>500</p></td><td><p>InternalError.Algo</p></td><td><p>algorithm process error</p></td><td><p>算法错误</p></td></tr><tr><td><p>500</p></td><td><p>InternalError.FileUpload</p></td><td><p>oss upload error</p></td><td><p>文件上传失败</p></td></tr></tbody></table>
