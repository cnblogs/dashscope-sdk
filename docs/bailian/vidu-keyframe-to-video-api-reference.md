> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Vidu-图生视频-基于首尾帧API参考

> Vidu-首尾帧生视频模型基于 首帧图像 、 尾帧图像和文本提示词 ，生成一段平滑过渡的视频。

<meta property="og:title" content="Vidu-图生视频-基于首尾帧API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

请前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>Vidu</strong>，找到 <strong>Vidu</strong> 模型卡片，单击<strong>立即开通</strong>；在弹窗内确认开通及授权。

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP调用 <span id="pk101a0h2http" />

由于首尾帧生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="pk111a0h4stp1" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

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
        #### 请求参数 <span id="pk123a0h4reqp" />

        ##### 请求头（Headers） <span id="pk142a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="pk154a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选值：

        - `vidu/viduq3-pro_start-end2video`
        - `vidu/viduq3-turbo_start-end2video`
        - `vidu/viduq2-pro_start-end2video`
        - `vidu/viduq2-turbo_start-end2video`

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，包含首帧、尾帧图像和提示词。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述首帧到尾帧之间的变化过程。

          支持中英文，每个汉字/字母占一个字符，不超过5000个字符，超过部分会自动截断。

          示例值：一只小猫从窗台向下跳跃，轻盈地落在沙发上，然后好奇地环顾四周。

          提示词编写请参见[Vidu视频生成Prompt指南](/zh/model-studio/vidu-video-generation-prompt-guide)。

          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体资源列表，包含首帧和尾帧图像。

          数组的每个元素为一个媒体对象，包含 `type` 与 `url` 字段。

          <Accordion title="属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体类型。固定值为：

            - `image`：图像URL。

            素材限制：

            - 图像素材有且只有<strong>2张</strong>。
            - 数组的第一个`image`表示首帧，第二个`image`表示尾帧。

            <strong>url</strong> `string` <strong>（必选）</strong>

            图像的公网可访问URL。

            - 支持 HTTP 或 HTTPS 协议。
            - 示例值：<a href="https://xxx/xxx.png">[https://xxx/xxx.png](https://xxx/xxx.png)</a>。

            图像限制：

            - 格式：JPG、JPEG、PNG、WEBP。
            - 宽高比：1:4～4:1。
            - 文件大小：不超过50MB。
            - 分辨率：首帧和尾帧的分辨率需相近，两者的分辨率（宽\*高的总像素值）比值需在 0.8～1.25 之间。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` <strong>（必选）</strong>

        视频生成参数，如设置视频分辨率、时长等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` <strong>（必选）</strong>

          <Tip>
            resolution直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#744137eff69v4)。
          </Tip>

          生成视频的分辨率。可选值：

          - `540P`
          - `720P`：默认值。
          - `1080P`

          <strong>duration</strong> `integer` <strong>（必选）</strong>

          <Tip>
            duration直接影响费用，按秒计费，时间越长费用越高，请在调用前确认[模型价格](/zh/model-studio/model-pricing#744137eff69v4)。
          </Tip>

          生成视频的时长，单位为秒。

          - vidu/viduq3-pro\_start-end2video、vidu/viduq3-turbo\_start-end2video：取值为\[1, 16]之间的整数。默认值为5。
          - vidu/viduq2-pro\_start-end2video、vidu/viduq2-turbo\_start-end2video：取值为\[1, 10]之间的整数。默认值为5。

          <strong>audio</strong> `boolean` （可选）

          <strong>支持模型</strong>：vidu/viduq3-pro\_start-end2video、vidu/viduq3-turbo\_start-end2video。

          是否生成有声视频。开启后模型将根据视频内容自动生成匹配的背景音乐或音效。

          - `false`：默认值，输出无声视频。
          - `true`：输出有声视频。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为“内容由AI生成”。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。

          <strong>seed</strong>`integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`。

          未指定时，系统自动生成随机种子。若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="首尾帧生视频">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "vidu/viduq3-turbo_start-end2video",
                "input": {
                    "media": [
                        {
                            "type": "image",
                            "url": "https://wanx.alicdn.com/material/20250318/first_frame.png"
                        },
                        {
                            "type": "image",
                            "url": "https://wanx.alicdn.com/material/20250318/last_frame.png"
                        }
                    ],
                    "prompt": "一只小猫从窗台向下跳跃，轻盈地落在沙发上，然后好奇地环顾四周。"
                },
                "parameters": {
                    "resolution": "540P",
                    "duration": 5,
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

            media = [{'type': 'image', 'url': 'https://wanx.alicdn.com/material/20250318/first_frame.png'}, {'type': 'image', 'url': 'https://wanx.alicdn.com/material/20250318/last_frame.png'}]

            def sample_async_call():
                # 异步提交视频生成任务
                rsp = VideoSynthesis.async_call(api_key=api_key,
                                            model='vidu/viduq3-turbo_start-end2video',
                                            media=media,
                                            prompt='一只小猫从窗台向下跳跃，轻盈地落在沙发上，然后好奇地环顾四周。',
                                            resolution='540P',
                                            duration=5,
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

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="pk285a0h4rsp1" />

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

### 步骤2：根据任务ID查询结果 <span id="pk401a0h4stp2" />

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
        #### 请求参数 <span id="pk414a0h4qr01" />

        ##### 请求头（Headers） <span id="pk424a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="pk430a0h5qr02" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            将`{task_id}`完整替换为上一步接口返回的`task_id`的值。`task_id`查询有效期为24小时

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
        #### 响应参数 <span id="pk438a0h4qrs1" />

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

          视频格式为MP4（H.264 编码）。视频链接<strong>有效期为24小时</strong>，请及时下载。

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

          总的视频计费时长（秒）。示例值：5。

          <strong>output\_video\_duration</strong>`integer`

          输出视频的时长（秒）。示例值：5。

          <strong>size</strong> `string`

          输出视频的分辨率。示例值：828\*624。

          <strong>fps</strong> `integer`

          输出视频的帧率。示例值：24。

          <strong>SR</strong> `string`

          输出视频的分辨率档位。示例值：540。

          <strong>audio</strong> `boolean`

          输出视频是否为有声视频。示例值：false。

          <strong>video\_count</strong> `integer`

          输出视频的数量。固定为1。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "cf7a3a1d-27b0-4d45-8b89-xxxxxx",
                "output": {
                    "task_id": "88125b85-b53d-45f1-ba13-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-27 14:39:15.041",
                    "scheduled_time": "2026-03-27 14:39:15.081",
                    "end_time": "2026-03-27 14:40:03.428",
                    "orig_prompt": "一只小猫从窗台向下跳跃，轻盈地落在沙发上，然后好奇地环顾四周。",
                    "video_url": "https://prod-ss-vidu.s3.cn-northwest-1.amazonaws.com.cn/xxx.mp4?xxx"
                },
                "usage": {
                    "duration": 5,
                    "size": "828*624",
                    "output_video_duration": 5,
                    "fps": 24,
                    "video_count": 1,
                    "audio": false,
                    "SR": "540"
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
                    "message": "The size is not match xxxxxx"
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

## 错误码 <span id="0d0a562d05nkh" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="pk801a0h2faq1" />

#### Q：media数组中首帧和尾帧的顺序是否有要求？ <span id="pk803a0h4fq01" />

A：有顺序要求。`media`数组必须包含两个 `type=image` 的对象：第一个元素表示为首帧，第二个元素表示为尾帧。

#### Q：首帧和尾帧图像的分辨率是否需要一致？ <span id="pk804a0h4fq02" />

A：建议保持相近。首帧和尾帧的总像素数（宽×高）比值需控制在 0.8～1.25 之间。若分辨率差异过大，会导致生成质量下降。
