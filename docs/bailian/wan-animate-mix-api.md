> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-视频换人API参考

> 万相-视频换人模型能够依据人物图片和参考视频，将视频中的主角替换为图片中的角色，同时保留原视频的场景、光照和色调，实现无缝换人。

<meta property="og:title" content="万相-视频换人API参考" />

- <strong>核心功能：</strong> 在不改变原始视频的动作、表情及环境的条件下，将视频中的角色替换为指定图片中的人物。
- <strong>适用场景：</strong>适用于二次创作、影视后期制作等需要进行角色替换的场景。

## 效果示例 <span id="55a79354d2s4j" />

万相-视频换人模型wan2.2-animate-mix提供标准模式`wan-std`和专业模式`wan-pro`两种服务模式，不同模式在效果和计费上存在差异，详情参见[计费与限流](/zh/model-studio/wan-animate-mix-api#ce68f54f7bbqe)。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24.95%" }} />

    <col style={{ width: "24.95%" }} />

    <col style={{ width: "25.02%" }} />

    <col style={{ width: "25.08%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>人物图片</strong>
      </th>

      <th>
        <strong>参考视频</strong>
      </th>

      <th>
        <strong>输出视频（标准模式</strong>`wan-std`<strong>）</strong>
      </th>

      <th>
        <strong>输出视频（专业模式</strong>`wan-pro`<strong>）</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3025528571/p1008733.jpeg" alt="mix_input_image" />
      </td>

      <td>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/wqefue/mix_input_video.mp4" type="video/mp4" />
        </video>
      </td>

      <td>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/ysurjs/mix_output_std.mp4" type="video/mp4" />
        </video>
      </td>

      <td>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/nrrirl/mix_output_pro.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>
  </tbody>
</table>

## HTTP调用 <span id="42703589880ts" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

<Tip>
  华北2（北京）和新加坡地域拥有独立的 <strong>API Key</strong>与<strong>请求地址</strong>，不可混用，跨地域调用将导致鉴权失败或服务报错。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

由于视频生成耗时较长，HTTP API采用异步模式，调用流程分两步：

1. <strong>创建任务获取任务ID</strong>：发送一个请求创建任务，该请求会返回<strong>任务ID（task\_id）</strong>。
2. <strong>根据任务ID查询结果</strong>：使用task\_id轮询任务状态，直到任务完成并获得视频URL。

### 步骤1：创建任务获取任务ID <span id="db8cf70432oe3" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis`

<strong>新加坡地域</strong>：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

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

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称，必须设置为`wan2.2-animate-mix`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        视频换人的输入图片和视频。

        <Accordion title="属性" defaultOpen>
          <strong>image\_url</strong> `string` <strong>（必选）</strong>

          人物图片的公网可访问HTTP/HTTPS URL，不能包含中文等非ASCII字符，否则需编码后传入。

          本地文件可通过[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。

          - <strong>格式限制</strong>：JPG、JPEG、PNG、BMP、WEBP
          - <strong>尺寸限制</strong>：图像的宽度和高度都在`[200,4096]`像素范围内，宽高比在1:3至3:1范围内。
          - <strong>大小限制</strong>：不超过5MB
          - <strong>示例</strong>：`https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/bhkfor/mix_input_image.jpeg`

          <strong>video\_url</strong> `string` <strong>（必选）</strong>

          参考视频的公网可访问HTTP/HTTPS URL，不能包含中文等非ASCII字符，否则需编码后传入。

          本地文件可通过[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。

          <strong>建议：</strong>更高的分辨率和帧率可有效提升输出视频画质。

          - <strong>格式限制</strong>：MP4、AVI、MOV
          - <strong>尺寸限制</strong>：视频的宽度和高度<strong>都</strong>在`[200, 2048]`像素范围内，宽高比在1:3至3:1范围内。
          - <strong>大小限制</strong>：不超过200MB
          - <strong>时长限制</strong>：不小于2s且不大于30s
          - <strong>示例</strong>：`https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/wqefue/mix_input_video.mp4`

          <strong>watermark</strong> `bool`（可选）

          是否添加水印标识，水印位于图片右下角，文案为“通义AI生成”。

          - `false`（默认值）：不添加水印。
          - `true`：添加水印。
        </Accordion>

        <strong>parameters</strong> `object` <strong>（必选）</strong>

        <Accordion title="属性" defaultOpen>
          <strong>check\_image</strong> `bool` （可选）

          控制是否在处理前对输入图片进行检查。

          - `true`（默认值）：在处理前检查输入图片。
          - `false`：跳过检查，直接处理。

          <strong>mode</strong> `string` <strong>（必选）</strong>

          服务模式，提供两种选择：

          - `wan-std`：标准模式。生成速度快、性价比高，适用于快速预览和基础动画。
          - `wan-pro`：专业模式。动画更流畅、画质更优，但处理时间和费用相应增加。

          详情请参见[效果示例](/zh/model-studio/wan-animate-mix-api#55a79354d2s4j)和[计费与限流](/zh/model-studio/wan-animate-mix-api#ce68f54f7bbqe)。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="视频换人">
            以下为华北2（北京）地域的URL。请将 \{WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
                --header 'X-DashScope-Async: enable' \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "model": "wan2.2-animate-mix",
                    "input": {
                        "image_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/bhkfor/mix_input_image.jpeg",
                        "video_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/wqefue/mix_input_video.mp4",
                        "watermark": true
                    },
                    "parameters": {
                        "mode": "wan-std"
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

<Tabs>
  <Tab title="华北2（北京）">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Note>
  - <strong>轮询建议</strong>：视频生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 15 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
  - <strong>结果链接</strong>：任务成功后返回视频链接，有效期为 <strong>24 小时</strong>。建议在获取链接后立即下载并转存至永久存储（如[阿里云 OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)）。
  - <strong>task\_id 有效期</strong>：<strong>24小时</strong>，超时后将无法查询结果，接口将返回任务状态为`UNKNOWN`。
  - <strong>RPS 限制</strong>：查询接口默认RPS为20。如需更高频查询或事件通知，建议[配置异步任务回调](/zh/model-studio/async-task-api)。
  - <strong>更多操作</strong>：如需批量查询、取消任务等操作，请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
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
            请将`0385dc79-5ff8-4d82-bcb6-xxxxxx`替换为真实的task\_id。

            > 以下为华北2（北京）地域的URL。请将 \{WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。

            ```bash
            curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/0385dc79-5ff8-4d82-bcb6-xxxxxx \
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

          <strong>results</strong> `object`

          <Accordion title="属性" defaultOpen>
            <strong>video\_url</strong>`string`

            视频URL。仅在 task\_status 为 SUCCEEDED 时返回。

            链接有效期24小时，可通过此URL下载视频。视频格式为MP4（H.264 编码）。
          </Accordion>

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        仅在任务成功时返回。

        <Accordion title="属性" defaultOpen>
          <strong>video\_duration</strong> `float`

          生成视频的时长，单位：秒。

          <strong>video\_ratio</strong> `string`

          本次请求使用的服务模式。选择标准模式`wan-std`时返回`standard`，选择专业模式`wan-pro`时返回`pro`。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            视频URL仅保留24小时，超时后会被自动清除，请及时保存生成的视频。

            ```json
            {
                "request_id": "a67f8716-18ef-447c-a286-xxxxxx",
                "output": {
                    "task_id": "0385dc79-5ff8-4d82-bcb6-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-09-18 15:32:00.105",
                    "scheduled_time": "2025-09-18 15:32:15.066",
                    "end_time": "2025-09-18 15:34:41.898",
                    "results": {
                        "video_url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxx.mp4?Expires=xxxxxx"
                    }
                },
                "usage": {
                    "video_duration": 5.2,
                    "video_ratio": "standard"
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "daad9007-6acd-9fb3-a6bc-xxxxxx",
                "output": {
                    "task_id": "fe8aa114-d9f1-4f76-b598-xxxxxx",
                    "task_status": "FAILED",
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

## 使用限制 <span id="534013237a77q" />

<strong>数据时效</strong>：任务task\_id和视频URL均只保留 24 小时，过期后将无法查询或下载，请及时[下载视频到本地](/zh/model-studio/wan-animate-mix-api)。

<strong>内容审核</strong>：输入与输出内容均会经过内容安全审核，包含违规内容的请求将报错“IPInfringementSuspect”或“DataInspectionFailed”，具体参见[错误码](/zh/model-studio/error-code)。

## 计费与限流 <span id="ce68f54f7bbqe" />

- 模型免费额度和计费单价请参见[模型价格](/zh/model-studio/model-pricing#86bd5c57adaos)。
- 模型限流请参见[万相系列](/zh/model-studio/rate-limit)。
- 计费说明：

  - 输入不计费，输出计费。按成功生成的 <strong>视频秒数</strong> 计费。
  - 模型调用失败或处理错误不产生任何费用，也不消耗[免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="3d39f89cb0n1a" />

如果调用失败，请参见[错误码](/zh/model-studio/error-code)。

## 常见问题 <span id="c928ee689aw6l" />

#### Q: 如何查看模型调用量？ <span id="7daf9e8eden75" />

A: 调用数据存在分钟级延迟。请在模型监控（[北京](https://bailian.console.aliyun.com/cn-beijing/model/telemetry)或[新加坡](https://bailian.console.aliyun.com/ap-southeast-1/model/telemetry)）页面查看调用量、调用次数和成功率等指标。[如何查看模型调用记录？](/zh/model-studio/new-free-quota#ab6ba5c538rn3)

#### Q: 如何提升生成视频的质量? <span id="1da0131fc6sha" />

A: 建议：

1. 确保输入图片与参考视频中的人物画幅占比相似。
2. 尽量保持图片和视频中人物的身材比例一致。
3. 使用高清素材，避免使用模糊图片和低帧率视频，确保细节识别准确。

#### Q: 如何将临时的视频链接转为永久链接？ <span id="c257155348vl4" />

A: 不支持直接转换。请在后端下载视频文件后上传至对象存储服务（如阿里云 OSS），生成永久访问链接。

<Accordion title="示例代码：下载视频到本地">
  ```python
  import requests

  def download_and_save_video(video_url, save_path):
      try:
          response = requests.get(video_url, stream=True, timeout=300) # 设置超时
          response.raise_for_status() # 如果HTTP状态码不是200，则引发异常
          with open(save_path, 'wb') as f:
              for chunk in response.iter_content(chunk_size=8192):
                  f.write(chunk)
          print(f"视频已成功下载到: {save_path}")
          # 此处可以接上传到永久存储的逻辑
      except requests.exceptions.RequestException as e:
          print(f"下载视频失败: {e}")

  if __name__ == '__main__':
      video_url = "http://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxxx"
      save_path = "video.mp4"
      download_and_save_video(video_url, save_path)
  ```
</Accordion>

#### Q: 返回的视频链接可以在浏览器中直接播放吗？ <span id="2c24257030bb4" />

A: 不建议直接播放，链接会在 24 小时后失效。请在后端下载转存后，使用永久链接播放。

#### Q：如何获取视频存储的访问域名白名单？ <span id="c7421e6e32dj8" />

A： 模型生成的视频存储于阿里云OSS，API将返回一个临时的公网URL。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会根据业务情况进行动态变更，为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。
