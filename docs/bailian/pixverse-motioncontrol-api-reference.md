> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 爱诗-视频动作模仿API参考

> 爱诗-视频动作模仿模型支持输入 人物图片 和 动作参考视频 ，将视频中的动作迁移到目标角色上，生成角色复现相同动作的新视频。适用于动作模仿、编舞复刻、角色动画生成等场景。

<meta property="og:title" content="爱诗-视频动作模仿API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

1. 前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>PixVerse</strong>，找到 PixVerse 模型卡片，单击<strong>立即开通</strong>；
2. 在弹窗内确认开通及授权。

## 适用范围 <span id="pvmc003a0h2scp" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Note>
  本文的视频动作模仿示例适用于<strong>华北2（北京）地域</strong>。
</Note>

## HTTP调用 <span id="pvmc101a0h2http" />

由于视频动作模仿任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="pvmc111a0h4stp1" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

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
        #### 请求参数 <span id="pvmc123a0h4reqp" />

        ##### 请求头（Headers） <span id="pvmc142a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="pvmc154a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。固定值：`pixverse/pixverse-motioncontrol`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，包括角色图片和动作视频。

        <Accordion title="属性" defaultOpen>
          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体素材列表，必须包含 1 个 `image_url` 类型的角色图片和 1 个 `video_url` 类型的动作视频。

          <Accordion title="元素属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体类型。取值范围：

            - `image_url`：角色图片。
            - `video_url`：动作视频。

            <strong>url</strong> `string` <strong>（必选）</strong>

            媒体资源的 URL 地址。

            - `image_url`：角色图片的 URL。建议单人主体，画面清晰。

              - 文件大小：不超过 20MB。
              - 分辨率：最长边不超过 10000px。
              - 支持格式：PNG、JPEG、WebP。
            - `video_url`：动作视频的 URL。建议为单人主体，画面动作清晰。

              - 文件大小：不超过 100MB。
              - 时长：不超过 30 秒。
              - 支持格式：MP4、MOV。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` <strong>（必选）</strong>

        视频生成参数。如设置输出视频清晰度、是否添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` <strong>（必选）</strong>

          输出视频的清晰度。取值范围：`360P`、`540P`、`720P`。实际输出分辨率会根据输入素材的宽高比自适应调整。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为"AI生成"。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="视频动作模仿">
            传入角色图片和动作视频，生成角色模仿动作的视频。

            ```bash
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-motioncontrol",
                "input": {
                    "media": [
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/adsyrp/move_input_image.jpeg"
                        },
                        {
                            "type": "video_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/kaakcn/move_input_video.mp4"
                        }
                    ]
                },
                "parameters": {
                    "resolution": "720P",
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="Python SDK">
            ```python expandable
            from http import HTTPStatus
            from dashscope import VideoSynthesis
            import dashscope
            import os

            # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
            # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
            api_key = os.getenv("DASHSCOPE_API_KEY")

            media = [{'type': 'image_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/adsyrp/move_input_image.jpeg'}, {'type': 'video_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250919/kaakcn/move_input_video.mp4'}]

            def sample_async_call():
                # 异步提交视频生成任务
                rsp = VideoSynthesis.async_call(api_key=api_key,
                                            model='pixverse/pixverse-motioncontrol',
                                            media=media,
                                            resolution='720P',
                                            watermark=True)
                print(rsp)
                if rsp.status_code == HTTPStatus.OK:
                    print("task_id: %s" % rsp.output.task_id)
                else:
                    print('Failed, status_code: %s, code: %s, message: %s' %
                          (rsp.status_code, rsp.code, rsp.message))

                # 查询任务状态
                status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
                if status.status_code == HTTPStatus.OK:
                    print(status.output.task_status)
                else:
                    print('Failed, status_code: %s, code: %s, message: %s' %
                          (status.status_code, status.code, status.message))

                # 等待任务完成
                rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
                print(rsp)
                if rsp.status_code == HTTPStatus.OK:
                    print(rsp.output.video_url)
                else:
                    print('Failed, status_code: %s, code: %s, message: %s' %
                          (rsp.status_code, rsp.code, rsp.message))

            if __name__ == '__main__':
                sample_async_call()
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="pvmc401a0h4stp2" />

<strong>北京地域</strong>：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<Note>
  - <strong>轮询建议</strong>：视频生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 15 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
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
        #### 请求参数 <span id="pvmc424a0h4qr01" />

        ##### 请求头（Headers） <span id="pvmc434a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="pvmc440a0h5qr02" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            将`{task_id}`完整替换为上一步接口返回的`task_id`的值。`task_id`查询有效期为24小时，并请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

            ```bash
            curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id} \
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
        #### 响应参数 <span id="pvmc454a0h4qrs1" />

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

          <strong>轮询过程中的状态流转：</strong>

          - PENDING（排队中） → RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
          - 初次查询状态通常为 PENDING（排队中）或 RUNNING（处理中）。
          - 当状态变为 SUCCEEDED 时，响应中将包含生成的视频URL。
          - 若状态为 FAILED，请检查错误信息并重试。
          - 若状态为 CANCELED，表示任务已取消，如需继续请重新提交任务。
          - 若状态为 UNKNOWN，表示任务不存在或状态未知，可能在 task\_id 不存在或超过 24 小时有效期后出现。

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>video\_url</strong>`string`

          视频URL。仅在 task\_status 为 SUCCEEDED 时返回。

          视频格式为MP4（H.264 编码）。视频链接暂无过期时间，但不建议将其作为长期存储，请及时下载。

          <strong>orig\_prompt</strong> `string`

          原始输入的prompt，对应请求参数`prompt`。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>duration</strong> `integer`

          计费时长（秒）。输出视频时长与输入的动作参考视频时长一致，计费按向上取整。

          <strong>size</strong> `string`

          输出视频的分辨率。

          <strong>fps</strong> `integer`

          输出视频的帧率。

          <strong>SR</strong> `string`

          输出视频的分辨率档位。

          <strong>video\_count</strong> `integer`

          视频数量，固定为 1。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "43aa3b9f-71c7-98e0-9529-xxxxxx",
                "output": {
                    "task_id": "e699df60-145c-464f-ab6a-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-07-14 18:15:04.045",
                    "scheduled_time": "2026-07-14 18:15:04.093",
                    "end_time": "2026-07-14 18:16:17.412",
                    "video_url": "https://media.pixverseai.cn/xxxx.mp4"
                },
                "usage": {
                    "duration": 6,
                    "size": "1280*704",
                    "fps": 24,
                    "video_count": 1,
                    "SR": "720"
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
                    "message": "Invalid field type or value. Please verify your input data."
                }
            }
            ```
          </Tab>

          <Tab title="任务查询过期">
            task\_id查询有效期为 24 小时，超时后将无法查询，返回以下报错信息。

            ```json
            {
                "request_id": "a4de7c32-7057-9f82-8581-xxxxxx",
                "output": {
                    "task_id": "502a00b1-19d9-4839-a82f-xxxxxx",
                    "task_status": "UNKNOWN"
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
