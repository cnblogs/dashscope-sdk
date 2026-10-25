> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 爱诗-图生视频-基于首帧API参考

> 爱诗-图生视频模型根据 输入图像 和 文本提示词 ，生成一段流畅的视频。

<meta property="og:title" content="爱诗-图生视频-基于首帧API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

1. 前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>PixVerse</strong>，找到 PixVerse 模型卡片，单击<strong>立即开通</strong>；
2. 在弹窗内确认开通及授权。

## 适用范围 <span id="c3d4e5f6g7h8i" />

为确保调用成功，请务必保证模型、endpoint URL 和 API Key 均属于<strong>同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：获取该地域的[API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Note>
  本文的示例代码适用于<strong>华北2（北京）地域</strong>。
</Note>

## HTTP调用 <span id="u1v2w3x4y5z6a" />

图生视频任务耗时较长（通常为1-5分钟），API采用异步调用的方式。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="x4y5z6a7b8c9d" />

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
        #### 请求参数 <span id="h4i5j6k7l8m9n" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="z2a3b4c5d6e7f" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。

        可选值：

        - pixverse/pixverse-c1-it2v
        - pixverse/pixverse-v6-it2v
        - pixverse/pixverse-v5.6-it2v

        <Accordion title="模型选型" defaultOpen>
          * 针对打斗、法术特效及高速运动等动态场景，推荐选用 <strong>c1</strong>。
          * 通用场景推荐使用<strong>v6</strong>，<strong>v5.6</strong>建议直接升级至<strong>v6</strong>。
        </Accordion>

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词、媒体素材等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` （可选）

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母占一个字符，字符编码为UTF-8，超过部分会自动截断。

          - pixverse/pixverse-c1-it2v：不超过5000个字符。
          - pixverse/pixverse-v6-it2v：不超过5000个字符。
          - pixverse/pixverse-v5.6-it2v：不超过2048个字符。

          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体素材列表，用于指定视频生成所需的图像。

          数组的每个元素为一个媒体对象，包含 `type` 与 `url` 字段。

          <Accordion title="属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。固定值为：

            - `image_url`：图像URL。

            素材限制：图像数量推荐为1张。

            > 若传入多张图像，系统将默认采用最后一个，建议始终只传一张图像以确保预期效果。

            <strong>url</strong> `string` <strong>（必选）</strong>

            图像文件的URL地址，必须为公网可访问的URL。

            - 支持 HTTP 或 HTTPS 协议。
            - 示例值：<a href="https://xxx/xxx.jpg">[https://xxx/xxx.jpg](https://xxx/xxx.jpg)</a>。

            图像限制：

            - 格式：JPG、PNG、WEBP。
            - 分辨率：图像的宽度和高度均不超过10000像素。
            - 文件大小：不超过20MB。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频处理参数，如设置视频分辨率、设置视频时长、开启音频生成、添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` <strong>（必选）</strong>

          <Tip>
            resolution直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#pxi002a0h3001)。
          </Tip>

          指定生成的视频分辨率档位。

          可选值：`360P`、`540P`、`720P`、`1080P`。

          <strong>duration</strong> `integer` <strong>（必选）</strong>

          <Tip>
            duration直接影响费用，按秒计费，请在调用前确认[模型价格](/zh/model-studio/model-pricing#pxi002a0h3001)。
          </Tip>

          生成视频的时长，单位为秒。

          - pixverse/pixverse-c1-it2v：取值范围为\[1, 15]之间的整数。
          - pixverse/pixverse-v6-it2v：取值范围为\[1, 15]之间的整数。
          - pixverse/pixverse-v5.6-it2v：

            - 当 resolution 为 360P / 540P / 720P 对应的所有分辨率时：取值为5、8、10。
            - 当resolution为1080P对应的所有分辨率时：取值为5、8。

          <strong>audio</strong> `boolean` （可选）

          <Tip>
            audio直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#pxi002a0h3001)。
          </Tip>

          是否生成有声视频。开启后模型将根据视频内容自动生成匹配的背景音乐或音效。

          - `false`：默认值，输出无声视频。
          - `true`：输出有声视频。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为"AI生成"。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。

          <strong>shot\_type</strong> `string` （可选）

          <strong>支持模型</strong>：pixverse/pixverse-v6-it2v。

          指定生成视频的镜头类型，控制视频是由一个连续镜头还是多镜头组成。

          - `single`：默认值，生成单镜头视频。
          - `multi`：多镜头，系统会进行智能分镜。

          使用建议：prompt 参数优先级高于 shot\_type 。为获得最佳效果，建议此参数设置与 prompt 描述保持一致。

          - 若想稳定输出单镜头：设置 `shot_type="single"` 并在 prompt 中描述单镜头场景。
          - 若想稳定输出多镜头：设置 `shot_type="multi"` 并在 prompt 中描述多镜头场景。

          <strong>seed</strong>`integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`。

          未指定时，系统自动生成随机种子。若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="图生视频">
            支持模型：pixverse/pixverse-c1-it2v、pixverse/pixverse-v6-it2v、pixverse/pixverse-v5.6-it2v。

            ```bash
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-c1-it2v",
                "input": {
                    "media": [
                        {
                            "type": "image_url",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260121/zlpocv/wan-i2v-haigui.webp"
                        }
                    ],
                    "prompt": "镜头从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。"
                },
                "parameters": {
                    "resolution": "720P",
                    "duration": 5,
                    "audio": false,
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

            media = [{'type': 'image_url', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260121/zlpocv/wan-i2v-haigui.webp'}]

            def sample_async_call():
                # 异步提交视频生成任务
                rsp = VideoSynthesis.async_call(api_key=api_key,
                                            model='pixverse/pixverse-c1-it2v',
                                            media=media,
                                            prompt='镜头从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。',
                                            resolution='720P',
                                            duration=5,
                                            audio=False,
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

          <Tab title="图生视频（多镜头）">
            <Tabs>
              <Tab title="pixverse-c1">
                支持模型：pixverse/pixverse-c1-it2v。

                在`prompt`中描述多镜头场景即可，不支持设置 `shot_type`参数。

                ```bash
                # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
                curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                    -H 'X-DashScope-Async: enable' \
                    -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    -H 'Content-Type: application/json' \
                    -d '{
                    "model": "pixverse/pixverse-c1-it2v",
                    "input": {
                        "media": [
                            {
                                "type": "image_url",
                                "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260121/zlpocv/wan-i2v-haigui.webp"
                            }
                        ],
                        "prompt": "镜头1：从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。镜头2：远景，海龟旁边围绕着海草，海草也在左右摇摆"
                    },
                    "parameters": {
                        "resolution": "720P",
                        "duration": 8,
                        "audio": false,
                        "watermark": true
                    }
                }'
                ```
              </Tab>

              <Tab title="pixverse-v6">
                支持模型：pixverse/pixverse-v6-it2v。

                在`prompt`中描述多镜头场景，并设置 `shot_type` 为`multi`， 即可生成有声多镜头视频。

                ```bash
                # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
                curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                    -H 'X-DashScope-Async: enable' \
                    -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    -H 'Content-Type: application/json' \
                    -d '{
                    "model": "pixverse/pixverse-v6-it2v",
                    "input": {
                        "media": [
                            {
                                "type": "image_url",
                                "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260121/zlpocv/wan-i2v-haigui.webp"
                            }
                        ],
                        "prompt": "镜头1：从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。镜头2：远景，海龟旁边围绕着海草，海草也在左右摇摆"
                    },
                    "parameters": {
                        "resolution": "720P",
                        "duration": 8,
                        "shot_type": "multi",
                        "audio": false,
                        "watermark": true
                    }
                }'
                ```
              </Tab>
            </Tabs>
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
        #### 响应参数 <span id="64ee5e75edznz" />

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

### 步骤2：根据任务ID查询结果 <span id="6da9d53d19x71" />

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
        #### 请求参数 <span id="19f6e90ee5rnq" />

        ##### 请求头（Headers） <span id="1e961f0eb7gwq" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="96525d350eazf" />

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
        #### 响应参数 <span id="9fed9a5bc6zes" />

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

          视频格式为MP4（H.264 编码）。视频链接暂无过期时间，但不建议将其作为长期存储依赖，请及时下载。

          <strong>orig\_prompt</strong> `string`

          原始输入的prompt，对应请求参数`prompt`。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计，只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>duration</strong> `integer`

          生成视频的总视频时长，用于计费。

          <strong>size</strong> `string`

          生成视频的分辨率。

          <strong>fps</strong> `integer`

          生成视频的帧率。

          <strong>SR</strong> `string`

          生成视频的分辨率档位。

          <strong>audio</strong> `boolean`

          生成视频是否为有声视频。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为1。

          <strong>shot\_type</strong>`string`

          生成视频的镜头类型。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "7df19cf7-d76c-4bb8-b4c5-xxxxxx",
                "output": {
                    "task_id": "5abf2c85-ea81-4cbf-8918-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-20 11:48:50.499",
                    "scheduled_time": "2026-03-20 11:48:50.551",
                    "end_time": "2026-03-20 11:49:46.462",
                    "orig_prompt": "镜头从海龟下方缓缓上移，海龟悠然游动，腹部细节清晰可见。",
                    "video_url": "https://media.pixverseai.cn/xxxx.mp4"
                },
                "usage": {
                    "duration": 5,
                    "shot_type": "single",
                    "size": "992*944",
                    "fps": 24,
                    "video_count": 1,
                    "audio": false,
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

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
