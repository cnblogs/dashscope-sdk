> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 爱诗-视频对口型API参考

> 爱诗-视频对口型模型支持输入 视频 和 音频 （或 TTS 文本），生成与音频同步的对口型视频。

<meta property="og:title" content="爱诗-视频对口型API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

1. 前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>PixVerse</strong>，找到 PixVerse 模型卡片，单击<strong>立即开通</strong>；
2. 在弹窗内确认开通及授权。

## 适用范围 <span id="pvls003a0h2scp" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Note>
  本文的视频对口型示例适用于<strong>华北2（北京）地域</strong>。
</Note>

## HTTP调用 <span id="pvls101a0h2http" />

由于视频对口型任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="pvls111a0h4stp1" />

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
        #### 请求参数 <span id="pvls123a0h4reqp" />

        ##### 请求头（Headers） <span id="pvls142a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="pvls154a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。固定值：`pixverse/pixverse-lipsync`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，包括人脸视频和音频来源。

        <Accordion title="属性" defaultOpen>
          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体资源列表。必须包含一个 `video_url` 类型的视频，以及音频来源（二选一）：

          - <strong>方式一（音频驱动）</strong>：传入 `audio_url` 类型的音频文件。
          - <strong>方式二（TTS 合成）</strong>：同时传入 `lip_sync_tts_speaker_id` 和 `lip_sync_tts_content`。

          <Tip>
            `audio_url` 与 `lip_sync_tts_speaker_id` + `lip_sync_tts_content` 二选一，不允许同时传入。
          </Tip>

          <Accordion title="元素属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体类型标识符。取值范围：

            - `video_url`：人脸视频。
            - `audio_url`：音频文件。
            - `lip_sync_tts_speaker_id`：TTS 音色 ID。
            - `lip_sync_tts_content`：TTS 文本内容。

            <strong>url</strong> `string` <strong>（条件必选）</strong>

            当 type 为 `video_url` 或 `audio_url` 时必填。媒体资源的 URL 地址。

            - `video_url`：输入视频的 URL。视频需包含可用于对口型的人脸画面。

              - 文件大小：不超过 250MB。
              - 时长：不超过 300 秒。
              - 支持格式：MP4、MOV、WebM。
            - `audio_url`：音频文件的 URL。

              - 文件大小：不超过 100MB。
              - 时长：不超过 300 秒。
              - 支持格式：MP3、WAV、M4A、AAC。

            <strong>id</strong> `string` <strong>（条件必选）</strong>

            当 type 为 `lip_sync_tts_speaker_id` 时必填。TTS 音色 ID。取值为 `auto` 时随机选择音色，取值为数字字符串时按音色 ID 指定。支持的取值见下表。

            <Accordion title="音色取值表" defaultOpen>
              <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>id 取值</strong></p></th><th><p><strong>音色名称</strong></p></th></tr></thead><tbody><tr><td><p>auto</p></td><td><p>随机</p></td></tr><tr><td><p>2</p></td><td><p>詹有鱼</p></td></tr><tr><td><p>4</p></td><td><p>外国阿利</p></td></tr><tr><td><p>6</p></td><td><p>李解</p></td></tr><tr><td><p>10</p></td><td><p>姜姜好</p></td></tr><tr><td><p>11</p></td><td><p>老森</p></td></tr><tr><td><p>12</p></td><td><p>李杰克</p></td></tr><tr><td><p>13</p></td><td><p>钱多多</p></td></tr><tr><td><p>14</p></td><td><p>呆萌王小拍</p></td></tr><tr><td><p>16</p></td><td><p>屯里大嗓</p></td></tr><tr><td><p>18</p></td><td><p>豫语汉子</p></td></tr><tr><td><p>19</p></td><td><p>宝岛囡囡</p></td></tr><tr><td><p>20</p></td><td><p>陕西掌柜</p></td></tr><tr><td><p>21</p></td><td><p>港风阿sir</p></td></tr></tbody></table>
            </Accordion>

            <strong>content</strong> `string` <strong>（条件必选）</strong>

            当 type 为 `lip_sync_tts_content` 时必填。需要驱动口型的文本内容，支持中英文，不超过 200 字（UTF-8 编码）。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` <strong>（可选）</strong>

        视频生成参数。如设置是否添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为"AI生成"。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="音频驱动">
            传入视频和音频文件，生成对口型视频。

            ```bash
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-lipsync",
                "input": {
                    "media": [
                        {
                            "type": "video_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/pvegot/input_video_01.mp4"
                        },
                        {
                            "type": "audio_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/aumwir/stella2-%E6%9C%89%E5%A3%B0%E4%B9%A67.wav"
                        }
                    ]
                },
                "parameters": {
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

            media = [{'type': 'video_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/pvegot/input_video_01.mp4'}, {'type': 'audio_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/aumwir/stella2-%E6%9C%89%E5%A3%B0%E4%B9%A67.wav'}]

            def sample_async_call():
                # 异步提交视频生成任务
                rsp = VideoSynthesis.async_call(api_key=api_key,
                                            model='pixverse/pixverse-lipsync',
                                            media=media,
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

          <Tab title="TTS 文本驱动">
            传入视频和 TTS 文本，由模型合成语音并生成对口型视频。

            ```bash expandable
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-lipsync",
                "input": {
                    "media": [
                        {
                            "type": "video_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/pvegot/input_video_01.mp4"
                        },
                        {
                            "type": "lip_sync_tts_speaker_id",
                            "id": "auto"
                        },
                        {
                            "type": "lip_sync_tts_content",
                            "content": "保持热爱持续开心，琐碎小事不值得烦心"
                        }
                    ]
                },
                "parameters": {
                    "watermark": true
                }
            }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="pvls401a0h4stp2" />

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
        #### 请求参数 <span id="pvls424a0h4qr01" />

        ##### 请求头（Headers） <span id="pvls434a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="pvls440a0h5qr02" />

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
        #### 响应参数 <span id="pvls454a0h4qrs1" />

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

          计费时长（秒）。音频驱动方式为音频时长向上取整；TTS 方式为文本字节数（UTF-8）÷ 15 向上取整。

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
                "request_id": "ddf8140e-67c6-9e70-8953-xxxxxx",
                "output": {
                    "task_id": "1acb9d0c-7a63-4ff6-a198-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-07-15 17:01:49.352",
                    "scheduled_time": "2026-07-15 17:01:49.398",
                    "end_time": "2026-07-15 17:02:29.724",
                    "video_url": "https://media.pixverseai.cn/xxxx.mp4"
                },
                "usage": {
                    "duration": 7,
                    "size": "1080*1920",
                    "fps": 24,
                    "video_count": 1,
                    "SR": ""
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
