> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Vidu-文生视频API参考

> Vidu-文生视频模型基于 文本提示词 ，生成一段流畅的视频。

<meta property="og:title" content="Vidu-文生视频API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

请前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>Vidu</strong>，找到 <strong>Vidu</strong> 模型卡片，单击<strong>立即开通</strong>；在弹窗内确认开通及授权。

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- <strong>安装 SDK</strong>：如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。

## HTTP调用 <span id="vd101a0h2http" />

由于文生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="vd111a0h4stp1" />

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
        #### 请求参数 <span id="vd123a0h4reqp" />

        ##### 请求头（Headers） <span id="vd142a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="vd163a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选值为：

        - `vidu/viduq3-pro_text2video`
        - `vidu/viduq3-turbo_text2video`
        - `vidu/viduq2_text2video`

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母占一个字符，字符长度不能超过 5000 个字符，超过部分会自动截断。

          示例值：一只小猫在月光下奔跑。

          提示词编写请参见[Vidu视频生成Prompt指南](/zh/model-studio/vidu-video-generation-prompt-guide)。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频生成参数，如设置视频分辨率、时长等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` （可选）

          <Tip>
            resolution直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#2143d64593wck)。
          </Tip>

          分辨率档位。可选值：

          - `540P`
          - `720P`：默认值。
          - `1080P`

          <strong>size</strong> `string` （可选）

          生成视频的分辨率，格式为`宽*高`的像素值。

          默认值根据`resolution`而定：

          - 当 resolution=540P 时，size 默认为`1024*576`。
          - 当 resolution=720P 时，size 默认为`1280*720`。
          - 当 resolution=1080P 时，size 默认为`1920*1080`。

          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.57%" }} /><col style={{ width: "28.57%" }} /><col style={{ width: "42.86%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p><strong>size取值（宽\*高）</strong></p></td></tr><tr><td rowSpan={5}><p>540P</p></td><td><p>16:9</p></td><td><p>1024\*576</p></td></tr><tr><td><p>9:16</p></td><td><p>576\*1024</p></td></tr><tr><td><p>1:1</p></td><td><p>1024\*1024</p></td></tr><tr><td><p>4:3</p></td><td><p>1024\*768</p></td></tr><tr><td><p>3:4</p></td><td><p>768\*1024</p></td></tr><tr><td rowSpan={5}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td><p>1:1</p></td><td><p>1280\*1280</p></td></tr><tr><td><p>4:3</p></td><td><p>1280\*960</p></td></tr><tr><td><p>3:4</p></td><td><p>960\*1280</p></td></tr><tr><td rowSpan={5}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr><tr><td><p>1:1</p></td><td><p>1808\*1808</p></td></tr><tr><td><p>4:3</p></td><td><p>1920\*1440</p></td></tr><tr><td><p>3:4</p></td><td><p>1440\*1920</p></td></tr></tbody></table>

          <strong>duration</strong> `integer`（可选）

          <Tip>
            duration直接影响费用，按秒计费，时间越长费用越高，请在调用前确认[模型价格](/zh/model-studio/model-pricing#2143d64593wck)。
          </Tip>

          生成视频的时长，单位为秒。

          - vidu/viduq3-pro\_text2video、vidu/viduq3-turbo\_text2video：取值为\[1, 16]之间的整数，默认值为5。
          - vidu/viduq2\_text2video：取值为\[1, 10]之间的整数，默认值为5。

          <strong>audio</strong> `boolean` （可选）

          <strong>支持模型</strong>：vidu/viduq3-pro\_text2video、vidu/viduq3-turbo\_text2video。

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
          <Tab title="文生视频">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "vidu/viduq3-turbo_text2video",
                "input": {
                    "prompt": "一只小猫在月光下奔跑"
                },
                "parameters": {
                    "size": "1024*576",
                    "resolution": "540P",
                    "duration": 5,
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

### 步骤2：根据任务ID查询结果 <span id="vd401a0h4stp2" />

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

          视频格式为MP4（H.264 编码）。视频链接有效期<strong>24小时</strong>，请及时下载。

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

          总的视频计费时长（秒）。示例值：5。

          <strong>output\_video\_duration</strong>`integer`

          输出视频的时长（秒）。示例值：5。

          <strong>size</strong> `string`

          生成视频的分辨率。示例值：960\*528。

          <strong>fps</strong> `integer`

          生成视频的帧率。示例值：24。

          <strong>SR</strong> `string`

          生成视频的分辨率档位。示例值：540。

          <strong>audio</strong> `boolean`

          生成视频是否为有声视频。示例值：false。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为1。
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
                "request_id": "eda50dad-a6d3-4e62-a70b-26bbb797ae81",
                "output": {
                    "task_id": "d9254244-1f9b-4b4c-be82-d9560ba25708",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-27 13:32:13.962",
                    "scheduled_time": "2026-03-27 13:32:14.008",
                    "end_time": "2026-03-27 13:32:43.375",
                    "orig_prompt": "一只小猫在月光下奔跑",
                    "video_url": "https://prod-ss-vidu.s3.cn-northwest-1.amazonaws.com.cn/xxx.mp4?xxx"
                },
                "usage": {
                    "duration": 5,
                    "size": "960*528",
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

## DashScope SDK调用 <span id="vd601a0h2sdk1" />

SDK 的参数命名与[HTTP接口](/zh/model-studio/vidu-text-to-video-api-reference#vd101a0h2http)基本一致，参数结构根据语言特性进行封装。

由于文生视频任务耗时较长（通常为1-5分钟），SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### Python SDK调用 <span id="vd607a0h3pyth" />

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.8`，再运行以下代码。
</Tip>

<strong>北京地域</strong>：`dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'`

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="vd626a0h5syn1" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为北京地域URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_sync_call_t2v():
        # call sync api, will return the result
        print('please wait...')
        rsp = VideoSynthesis.call(api_key=api_key,
                                  model='vidu/viduq3-turbo_text2video',
                                  prompt='一只小猫在月光下奔跑',
                                  size='1024*576',
                                  duration=5,
                                  resolution='540P',
                                  watermark=True)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_sync_call_t2v()
    ```

    ##### 响应示例 <span id="vd628a0h5syn2" />

    > `video_url`24小时内有效，请及时下载。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "15bd86e5-28b1-40ad-a427-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "a876c6ee-1c63-4d17-b6c0-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://prod-ss-vidu.s3.cn-northwest-1.amazonaws.com.cn/xxx.mp4?xxx",
            "submit_time": "2026-03-27 13:35:49.508",
            "scheduled_time": "2026-03-27 13:35:49.540",
            "end_time": "2026-03-27 13:36:45.848",
            "orig_prompt": "一只小猫在月光下奔跑"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 5,
            "size": "960*528",
            "output_video_duration": 5,
            "fps": 24,
            "audio": false,
            "SR": "540"
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="vd637a0h5asy1" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为北京地域URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call_t2v():
        # call async api, will return the task information
        rsp = VideoSynthesis.async_call(api_key=api_key,
                                        model='vidu/viduq3-turbo_text2video',
                                        prompt='一只小猫在月光下奔跑',
                                        size='1024*576',
                                        duration=5,
                                        resolution='540P')
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print("task_id: %s" % rsp.output.task_id)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

        # get the task information include the task status.
        status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)  # check the task status
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

        # wait the task complete
        rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_async_call_t2v()
    ```

    ##### 响应示例 <span id="vd639a0h5asy2" />

    1、创建任务的响应示例

    ```json
    {
    	"status_code": 200,
    	"request_id": "c86ff7ba-8377-917a-90ed-xxxxxx",
    	"code": "",
    	"message": "",
    	"output": {
    		"task_id": "721164c6-8619-4a35-a6d9-xxxxxx",
    		"task_status": "PENDING",
    		"video_url": ""
    	},
    	"usage": null
    }
    ```

    2、查询任务结果的响应示例

    > `video_url` 24小时内有效，请及时下载。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "15bd86e5-28b1-40ad-a427-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "a876c6ee-1c63-4d17-b6c0-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://prod-ss-vidu.s3.cn-northwest-1.amazonaws.com.cn/xxx.mp4?xxx",
            "submit_time": "2026-03-27 13:35:49.508",
            "scheduled_time": "2026-03-27 13:35:49.540",
            "end_time": "2026-03-27 13:36:45.848",
            "orig_prompt": "一只小猫在月光下奔跑"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 5,
            "size": "960*528",
            "output_video_duration": 5,
            "fps": 24,
            "audio": false,
            "SR": "540"
        }
    }
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="vd649a0h3java" />

<Tip>
  请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.6`，再运行以下代码。
</Tip>

<strong>北京地域</strong>：`Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";`

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="vd659a0h5syn3" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesis;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisParam;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.InputRequiredException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.JsonUtils;
    import com.alibaba.dashscope.utils.Constants;

    public class Text2Video {

        static {
            // 以下为北京地域URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * Create a video compositing task and wait for the task to complete.
         */
        public static void text2Video() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();
            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("vidu/viduq3-turbo_text2video")
                            .prompt("一只小猫在月光下奔跑")
                            .size("1024*576")
                            .resolution("540P")
                            .duration(5)
                            .watermark(true)
                            .build();
            System.out.println("please wait...");
            VideoSynthesisResult result = vs.call(param);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                text2Video();
            } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="vd661a0h5syn4" />

    > `video_url` 24小时内有效，请及时下载。

    ```json
    {
        "request_id": "bebc5f6b-a081-4b02-ad29-xxxxxx",
        "output": {
            "task_id": "2b0e287f-ebdc-49af-82aa-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://prod-ss-vidu.s3.cn-northwest-1.amazonaws.com.cn/infer_92/xxx.mp4?xxx",
            "orig_prompt": "一只小猫在月光下奔跑",
            "submit_time": "2026-03-30 10:19:00.827",
            "scheduled_time": "2026-03-30 10:19:00.879",
            "end_time": "2026-03-30 10:19:28.172"
        },
        "usage": {
            "video_count": 1,
            "duration": 5.0,
            "size": "960*528",
            "input_video_duration": 0.0,
            "output_video_duration": 5.0,
            "SR": "540"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="vd670a0h5asy3" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesis;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisParam;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.InputRequiredException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.JsonUtils;
    import com.alibaba.dashscope.utils.Constants;

    public class Text2Video {

        static {
            // 以下为北京地域URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * Create a video compositing task and wait for the task to complete.
         */
        public static void text2Video() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("vidu/viduq3-turbo_text2video")
                            .prompt("一只小猫在月光下奔跑")
                            .size("1024*576")
                            .resolution("540P")
                            .duration(5)
                            .watermark(true)
                            .build();

            // 异步调用
            VideoSynthesisResult task = vs.asyncCall(param);
            System.out.println(JsonUtils.toJson(task));
            System.out.println("please wait...");

            //获取结果
            VideoSynthesisResult result = vs.wait(task, apiKey);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                text2Video();
            } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="vd672a0h5asy4" />

    1、创建任务的响应示例。

    ```json
    {
        "request_id": "9b583f1b-2423-4fac-bb3f-xxxxxx",
        "output": {
            "task_id": "3944b819-1bbb-4da0-a230-xxxxxx",
            "task_status": "PENDING"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```

    2、查询任务结果的响应示例

    > `video_url` 24小时内有效，请及时下载。

    ```json
    {
        "request_id": "bebc5f6b-a081-4b02-ad29-xxxxxx",
        "output": {
            "task_id": "2b0e287f-ebdc-49af-82aa-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://prod-ss-vidu.s3.cn-northwest-1.amazonaws.com.cn/infer_92/xxx.mp4?xxx",
            "orig_prompt": "一只小猫在月光下奔跑",
            "submit_time": "2026-03-30 10:19:00.827",
            "scheduled_time": "2026-03-30 10:19:00.879",
            "end_time": "2026-03-30 10:19:28.172"
        },
        "usage": {
            "video_count": 1,
            "duration": 5.0,
            "size": "960*528",
            "input_video_duration": 0.0,
            "output_video_duration": 5.0,
            "SR": "540"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>
</Tabs>

## 错误码 <span id="0d0a562d05nkh" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="a0379eaad3lho" />

#### Q：size 与 resolution 必须同时传入吗？ <span id="d64acbbcf61fn" />

<strong>A：</strong> 不必。二者均为可选参数，但<strong>推荐同时传入</strong>。同时传入可精准控制生成视频的宽高比，具体参见[size取值](/zh/model-studio/vidu-text-to-video-api-reference)。

若不同时传入，系统将按以下两种场景处理：

- <strong>仅传 size</strong>：size参数会被忽略，系统强制按默认的 `resolution=720P` 及其对应的`size`默认值（`1280*720`）处理。

  > 例如：接口返回为size="1280\*720", SR=720。
- <strong>仅传 resolution</strong>：按指定分辨率档位及该档位对应的 16:9 比例输出。

  > 例如：当resolution=540P时，接口返回 size="960*528", SR=540；当resolution=1080P时，接口返回 size="1920*1080", SR=1080。
