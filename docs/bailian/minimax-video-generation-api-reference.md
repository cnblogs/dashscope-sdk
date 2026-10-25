> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# MiniMax-视频生成API文档

> MiniMax-视频生成模型支持 文生视频、图生视频-基于首帧、图生视频-基于尾帧、图生视频-基于首尾帧以及多模态参考生视频 。

<meta property="og:title" content="MiniMax-视频生成API文档" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="mm04h2svc" />

请前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索"MiniMax"，找到<strong>MiniMax</strong>模型卡片，单击<strong>立即开通</strong>，在弹窗内确认开通及授权。

## 适用范围 <span id="mm09h2scope" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/video-generate-edit-model)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP调用 <span id="mm101h2http" />

由于视频生成任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="mm111h3step1" />

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
        #### 请求参数 <span id="mm123h4reqp" />

        ##### 请求头（Headers） <span id="mm172h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="mm184h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。

        可选值：

        - `MiniMax/MiniMax-H3`

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词、媒体素材等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，按字符数计算长度，不超过7000个字符，超过将返回错误。

          <strong>media</strong> `array` （可选）

          文生视频任务无需填写此参数。

          媒体素材列表，用于指定图像、视频或音频素材。

          支持的输入组合（对应不同生成场景）：

          - <strong>图生视频-首帧</strong>：1张`first_frame`图片。
          - <strong>图生视频-尾帧</strong>：1张`last_frame`图片。
          - <strong>图生视频-首尾帧</strong>：1张`first_frame` + 1张`last_frame`图片。
          - <strong>多模态参考生视频</strong>：参考图片（`image_url`）+ 参考视频（`feature`）+ 参考音频（`driving_audio`）的组合。

          <Note>
            图生视频与多模态参考生视频互斥：media中出现`image_url` / `feature` / `driving_audio`任一类型时，不能再出现`first_frame` / `last_frame`（反之亦然），二者不可混用。
          </Note>

          <Accordion title="属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。可选值：

            - `first_frame`：首帧图片。
            - `last_frame`：尾帧图片。
            - `image_url`：参考图片（多模态参考生视频）。
            - `feature`：参考视频（多模态参考生视频）。
            - `driving_audio`：参考音频（多模态参考生视频）。

            <strong>url</strong> `string` <strong>（必选）</strong>

            媒体素材URL。素材包括图像、视频、音频。

            <Accordion title="传入图像（type=first_frame或last_frame或image_url）" defaultOpen>
              图像URL。

              - 支持 HTTP 或 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.png">[https://xxx/xxx.png](https://xxx/xxx.png)</a>。

              图像限制：

              - 格式：JPG、JPEG、PNG、WEBP、HEIC、HEIF。
              - 分辨率：宽和高的范围为\[256, 5760]像素。
              - 宽高比（宽/高）：\[0.4, 2.5]。
              - 文件大小：不超过30MB。
              - 数量限制：首帧最多1张、尾帧最多1张、参考图最多9张。
            </Accordion>

            <Accordion title="传入视频（type=feature）" defaultOpen>
              视频文件的 URL。仅多模态参考生视频场景使用。

              - 支持 HTTP 和 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.mp4">[https://xxx/xxx.mp4](https://xxx/xxx.mp4)</a>。

              视频限制：

              - 格式：MP4、MOV。
              - 编码：视频 H.264/AVC、H.265/HEVC；音频 AAC、MP3。
              - 单段时长：\[2, 15]秒，总时长不超过15秒。
              - 分辨率：宽和高的范围为\[256, 5760]像素。
              - 宽高比（宽/高）：\[0.4, 2.5]。
              - 帧率：\[23.976, 60]fps。
              - 文件大小：不超过50MB。
              - 数量限制：最多3个视频。
            </Accordion>

            <Accordion title="传入音频（type=driving_audio）" defaultOpen>
              音频文件的 URL。仅多模态参考生视频场景使用。

              - 支持 HTTP 和 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。

              音频限制：

              - 格式：WAV、MP3。
              - 单段时长：\[2, 15]秒，总时长不超过15秒。
              - 文件大小：不超过15MB。
              - 数量限制：最多3个音频。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频生成参数。如设置分辨率、画面比例、视频时长等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` <strong>（必选）</strong>

          视频分辨率档位。

          - `768P`：标准分辨率。
          - `2K`：2K高清分辨率。

          <strong>ratio</strong> `string` （可选）

          生成视频的宽高比例，默认为 `adaptive`（自动，由输入自适应选择最合适的宽高比）。

          可选值：

          - `adaptive`：自适应（默认值）
          - `16:9`
          - `9:16`
          - `1:1`
          - `4:3`
          - `3:4`
          - `21:9`

          各场景填写规则：

          - <strong>文生视频</strong>：必填，且不能为 `adaptive`，需指定具体比例。
          - <strong>图生视频</strong>：宽高比由输入图片决定，恒为 `adaptive`；传入其他值不会报错，但会被忽略。
          - <strong>多模态参考生视频</strong>：可选，默认 `adaptive`，也可指定具体比例。

          <strong>duration</strong> `integer` <strong>（必选）</strong>

          <Tip>
            duration直接影响费用，按秒计费，时间越长费用越高，请前往[百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)查看价格。
          </Tip>

          生成视频的时长，单位为秒。取值为\[4, 15]之间的整数。

          示例值：5。

          <strong>watermark</strong> `boolean` （可选）

          是否在生成视频中添加水印标识。水印位于视频右下角，文案固定为"AI生成"。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```bash 文生视频（curl）
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
              -H 'X-DashScope-Async: enable' \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
              "model": "MiniMax/MiniMax-H3",
              "input": {
                  "prompt": "史诗级太空歌剧院线预告：女舰长独自站在巨大观景窗前，最后一支舰队正在集结并跃迁离去，强光爆闪、舰桥震动，她被留在原地。"
              },
              "parameters": {
                  "resolution": "768P",
                  "ratio": "16:9",
                  "duration": 5,
                  "watermark": true
              }
          }'
          ```

          ```python 文生视频（Python SDK） expandable
          from http import HTTPStatus
          from dashscope import VideoSynthesis
          import dashscope
          import os

          # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
          # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
          api_key = os.getenv("DASHSCOPE_API_KEY")

          def sample_async_call():
              # 异步提交视频生成任务
              rsp = VideoSynthesis.async_call(api_key=api_key,
                                          model='MiniMax/MiniMax-H3',
                                          prompt='史诗级太空歌剧院线预告：女舰长独自站在巨大观景窗前，最后一支舰队正在集结并跃迁离去，强光爆闪、舰桥震动，她被留在原地。',
                                          resolution='768P',
                                          ratio='16:9',
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

          ```bash 图生视频（首帧生视频）
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
              -H 'X-DashScope-Async: enable' \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
              "model": "MiniMax/MiniMax-H3",
              "input": {
                  "prompt": "让图片中的人物动起来，头发被微风吹动",
                  "media": [
                      {
                          "type": "first_frame",
                          "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260121/zlpocv/wan-i2v-haigui.webp"
                      }
                  ]
              },
              "parameters": {
                  "resolution": "768P",
                  "duration": 5,
                  "watermark": true
              }
          }'
          ```

          ```bash 图生视频（尾帧生视频）
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
              -H 'X-DashScope-Async: enable' \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
              "model": "MiniMax/MiniMax-H3",
              "input": {
                  "prompt": "镜头缓慢推进，画面逐渐聚焦到图片中的场景",
                  "media": [
                      {
                          "type": "last_frame",
                          "url": "https://wanx.alicdn.com/material/20250318/last_frame.png"
                      }
                  ]
              },
              "parameters": {
                  "resolution": "768P",
                  "duration": 5,
                  "watermark": true
              }
          }'
          ```

          ```bash 图生视频（首尾帧生视频） expandable
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
              -H 'X-DashScope-Async: enable' \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
              "model": "MiniMax/MiniMax-H3",
              "input": {
                  "prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
                  "media": [
                      {
                          "type": "first_frame",
                          "url": "https://wanx.alicdn.com/material/20250318/first_frame.png"
                      },
                      {
                          "type": "last_frame",
                          "url": "https://wanx.alicdn.com/material/20250318/last_frame.png"
                      }
                  ]
              },
              "parameters": {
                  "resolution": "768P",
                  "duration": 5,
                  "watermark": true
              }
          }'
          ```

          ```bash 多模态参考生视频 expandable
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
              -H 'X-DashScope-Async: enable' \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
              "model": "MiniMax/MiniMax-H3",
              "input": {
                  "prompt": "参考视频中的角色缓缓转头，微笑着挥手致意，背景光影柔和流转",
                  "media": [
                      {
                          "type": "image_url",
                          "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260320/knsple/wan-r2v-role-frame.jpg"
                      },
                      {
                          "type": "feature",
                          "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4"
                      }
                  ]
              },
              "parameters": {
                  "resolution": "768P",
                  "ratio": "16:9",
                  "duration": 5,
                  "watermark": true
              }
          }'
          ```
        </CodeGroup>
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
        #### 响应参数 <span id="mm505h4rsp1" />

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

### 步骤2：根据任务ID查询结果 <span id="mm601h3step2" />

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
        #### 请求参数 <span id="mm624h4qr01" />

        ##### 请求头（Headers） <span id="mm634h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="mm640h5qr02" />

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
        #### 响应参数 <span id="mm654h4qrs1" />

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

          视频格式为MP4（H.264 编码）。视频链接有效期30天，但不建议将其作为长期存储依赖，请及时下载。

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

          计费总秒数，等于 input\_seconds + output\_seconds。

          <strong>size</strong> `string`

          生成视频的分辨率。示例值：1344\*768。

          <strong>SR</strong> `string`

          生成视频的分辨率档位。示例值：768。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为1。

          <strong>image\_count</strong> `integer`

          计费图片数量。输入图片5张以内免费，返回0；超过5张时返回超出部分的数量（例如输入7张图片，返回2，即7-5=2）。

          <strong>input\_seconds</strong> `integer`

          输入参考视频的总秒数。所有输入视频的毫秒级时长求和后四舍五入取整。无参考视频时为0。

          <strong>output\_seconds</strong> `integer`

          输出视频的总秒数。

          <strong>total\_seconds</strong> `integer`

          总秒数，等于 input\_seconds + output\_seconds。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "bde5ed1a-86de-9fac-xxxx-xxxxxxxxxxxx",
                "output": {
                    "task_id": "ca7eaceb-2b00-4f29-xxxx-xxxxxxxxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-08-20 13:54:07.204",
                    "scheduled_time": "2026-08-20 13:54:07.249",
                    "end_time": "2026-08-20 13:55:58.633",
                    "video_url": "https://xxx.oss-cn-shanghai.aliyuncs.com/xxx/output.mp4?Expires=xxx&Signature=YOUR_SIGNATURE"
                },
                "usage": {
                    "SR": "768",
                    "duration": 5,
                    "image_count": 0,
                    "input_seconds": 0,
                    "output_seconds": 5,
                    "size": "1344*768",
                    "total_seconds": 5,
                    "video_count": 1
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
                    "message": "The parameter is invalid xxxxxx"
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
