> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 爱诗-文生视频API参考

> 爱诗-文生视频模型基于 文本提示词 ，生成一段流畅的视频。

<meta property="og:title" content="爱诗-文生视频API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

1. 前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>PixVerse</strong>，找到 PixVerse 模型卡片，单击<strong>立即开通</strong>；
2. 在弹窗内确认开通及授权。

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL或 DashScope SDK URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- <strong>安装 SDK</strong>：如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。

<Note>
  本文的示例代码适用于<strong>华北2（北京）地域</strong>。
</Note>

## HTTP调用 <span id="pv101a0h2http" />

由于文生视频任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="pv111a0h4stp1" />

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
        #### 请求参数 <span id="pv123a0h4reqp" />

        ##### 请求头（Headers） <span id="pv142a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="pv154a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。

        可选值：

        - pixverse/pixverse-c1-t2v
        - pixverse/pixverse-v6-t2v
        - pixverse/pixverse-v5.6-t2v

        <Accordion title="模型选型" defaultOpen>
          * 针对打斗、法术特效及高速运动等动态场景，推荐选用 <strong>c1</strong>。
          * 通用场景推荐使用<strong>v6</strong>，<strong>v5.6</strong>建议直接升级至<strong>v6</strong>。
        </Accordion>

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母占一个字符，字符编码为UTF-8，超过部分会自动截断。

          - pixverse/pixverse-c1-t2v：不超过5000个字符。
          - pixverse/pixverse-v6-t2v：不超过5000个字符。
          - pixverse/pixverse-v5.6-t2v：不超过2048个字符。
        </Accordion>

        <strong>parameters</strong> `object` <strong>（必选）</strong>

        视频生成参数。如设置视频分辨率、时长、是否生成音频等。

        <Accordion title="属性" defaultOpen>
          <strong>size</strong> `string` <strong>（必选）</strong>

          生成视频的分辨率，格式为`宽*高`的像素值。

          <Tip>
            - size直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#pxt002a0h3001)。
            - 使用pixverse/pixverse-v5.6-t2v模型，若设置size为1808*1808，且duration为8时，实际输出的视频分辨率为1440*1440。
          </Tip>

          不同分辨率档位和宽高比对应的可选值见下方表格。

          <Accordion title="pixverse/pixverse-c1-t2v、pixverse/pixverse-v6-t2v模型" defaultOpen>
            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p>size取值（宽\*高）</p></td></tr><tr><td rowSpan={8}><p>360P</p></td><td><p>16:9</p></td><td><p>640\*360</p></td></tr><tr><td><p>4:3</p></td><td><p>640\*480</p></td></tr><tr><td><p>1:1</p></td><td><p>640\*640</p></td></tr><tr><td><p>3:4</p></td><td><p>480\*640</p></td></tr><tr><td><p>9:16</p></td><td><p>360\*640</p></td></tr><tr><td><p>3:2</p></td><td><p>640\*432</p></td></tr><tr><td><p>2:3</p></td><td><p>432\*640</p></td></tr><tr><td><p>21:9</p></td><td><p>640\*288</p></td></tr><tr><td rowSpan={8}><p>540P</p></td><td><p>16:9</p></td><td><p>1024\*576</p></td></tr><tr><td><p>4:3</p></td><td><p>1024\*768</p></td></tr><tr><td><p>1:1</p></td><td><p>1024\*1024</p></td></tr><tr><td><p>3:4</p></td><td><p>768\*1024</p></td></tr><tr><td><p>9:16</p></td><td><p>576\*1024</p></td></tr><tr><td><p>3:2</p></td><td><p>1024\*688</p></td></tr><tr><td><p>2:3</p></td><td><p>688\*1024</p></td></tr><tr><td><p>21:9</p></td><td><p>1024\*448</p></td></tr><tr><td rowSpan={8}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>4:3</p></td><td><p>1108\*832</p></td></tr><tr><td><p>1:1</p></td><td><p>960\*960</p></td></tr><tr><td><p>3:4</p></td><td><p>832\*1108</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td><p>3:2</p></td><td><p>1200\*800</p></td></tr><tr><td><p>2:3</p></td><td><p>800\*1200</p></td></tr><tr><td><p>21:9</p></td><td><p>1280\*560</p></td></tr><tr><td rowSpan={8}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>4:3</p></td><td><p>1664\*1248</p></td></tr><tr><td><p>1:1</p></td><td><p>1440\*1440</p></td></tr><tr><td><p>3:4</p></td><td><p>1248\*1664</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr><tr><td><p>3:2</p></td><td><p>1776\*1184</p></td></tr><tr><td><p>2:3</p></td><td><p>1184\*1776</p></td></tr><tr><td><p>21:9</p></td><td><p>1920\*832</p></td></tr></tbody></table>
          </Accordion>

          <Accordion title="pixverse/pixverse-v5.6-t2v模型" defaultOpen>
            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p>size取值（宽\*高）</p></td></tr><tr><td rowSpan={5}><p>360P</p></td><td><p>16:9</p></td><td><p>640\*360</p></td></tr><tr><td><p>4:3</p></td><td><p>640\*480</p></td></tr><tr><td><p>1:1</p></td><td><p>640\*640</p></td></tr><tr><td><p>3:4</p></td><td><p>480\*640</p></td></tr><tr><td><p>9:16</p></td><td><p>360\*640</p></td></tr><tr><td rowSpan={5}><p>540P</p></td><td><p>16:9</p></td><td><p>1024\*576</p></td></tr><tr><td><p>4:3</p></td><td><p>1024\*768</p></td></tr><tr><td><p>1:1</p></td><td><p>1024\*1024</p></td></tr><tr><td><p>3:4</p></td><td><p>768\*1024</p></td></tr><tr><td><p>9:16</p></td><td><p>576\*1024</p></td></tr><tr><td rowSpan={5}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>4:3</p></td><td><p>1280\*960</p></td></tr><tr><td><p>1:1</p></td><td><p>1280\*1280</p></td></tr><tr><td><p>3:4</p></td><td><p>960\*1280</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td rowSpan={5}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>4:3</p></td><td><p>1920\*1440</p></td></tr><tr><td><p>1:1</p></td><td><p>1808\*1808</p></td></tr><tr><td><p>3:4</p></td><td><p>1440\*1920</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr></tbody></table>
          </Accordion>

          <strong>duration</strong> `integer` <strong>（必选）</strong>

          <Tip>
            duration直接影响费用，按秒计费，请在调用前确认[模型价格](/zh/model-studio/model-pricing#pxt002a0h3001)。
          </Tip>

          生成视频的时长，单位为秒。

          - pixverse/pixverse-c1-t2v：取值范围为\[1, 15]之间的整数。
          - pixverse/pixverse-v6-t2v：取值范围为\[1, 15]之间的整数。
          - pixverse/pixverse-v5.6-t2v：

            - 当 size 为 360P / 540P / 720P 对应的所有分辨率时：取值为5、8、10。
            - 当size为1080P对应的所有分辨率时：取值为5、8。

          <strong>audio</strong> `boolean` （可选）

          <Tip>
            audio直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#pxt002a0h3001)。
          </Tip>

          是否生成有声视频。开启后模型将根据视频内容自动生成匹配的背景音乐或音效。

          - `false`：默认值，输出无声视频。
          - `true`：输出有声视频。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为“AI生成”。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。

          <strong>shot\_type</strong> `string` （可选）

          <strong>支持模型</strong>：pixverse/pixverse-v6-t2v。

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
          <Tab title="文生视频">
            支持模型：pixverse/pixverse-c1-t2v、pixverse/pixverse-v6-t2v、pixverse/pixverse-v5.6-t2v。

            ```bash
            # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "pixverse/pixverse-c1-t2v",
                "input": {
                    "prompt": "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。"
                },
                "parameters": {
                    "size": "1280*720",
                    "duration": 5,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="文生视频（多镜头）">
            <Tabs>
              <Tab title="pixverse-c1">
                支持模型：pixverse/pixverse-c1-t2v。

                在`prompt`中描述多镜头场景即可，不支持设置 `shot_type`参数。

                ```bash
                # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
                curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                    -H 'X-DashScope-Async: enable' \
                    -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    -H 'Content-Type: application/json' \
                    -d '{
                    "model": "pixverse/pixverse-c1-t2v",
                    "input": {
                        "prompt": "镜头1：模特面向镜头平稳向前漫步；镜头2： 快速切换至中景，展示腰线裁剪与蓝白格纹棉质面料的清爽质感；镜头3： 远景捕捉微风吹拂裙摆的自然动态，画面干净明亮。"
                    },
                    "parameters": {
                        "size": "1280*720",
                        "duration": 5,
                        "audio": true,
                        "watermark": true
                    }
                }'
                ```
              </Tab>

              <Tab title="pixverse-v6">
                支持模型：pixverse/pixverse-v6-t2v。

                在`prompt`中描述多镜头场景，并设置 `shot_type` 为`multi`， 即可生成有声多镜头视频。

                ```bash
                # 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
                curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                    -H 'X-DashScope-Async: enable' \
                    -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    -H 'Content-Type: application/json' \
                    -d '{
                    "model": "pixverse/pixverse-v6-t2v",
                    "input": {
                        "prompt": "镜头1：模特面向镜头平稳向前漫步；镜头2： 快速切换至中景，展示腰线裁剪与蓝白格纹棉质面料的清爽质感；镜头3： 远景捕捉微风吹拂裙摆的自然动态，画面干净明亮。"
                    },
                    "parameters": {
                        "size": "1280*720",
                        "duration": 5,
                        "audio": true,
                        "shot_type": "multi",
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
        #### 响应参数 <span id="pv355a0h4rsp1" />

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

### 步骤2：根据任务ID查询结果 <span id="pv401a0h4stp2" />

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
        #### 请求参数 <span id="pv414a0h4qr01" />

        ##### 请求头（Headers） <span id="pv424a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="pv430a0h5qr02" />

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
        #### 响应参数 <span id="pv438a0h4qrs1" />

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

          生成视频的总视频时长（秒），用于计费。

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
                "request_id": "19171ea5-9efb-4d35-93a1-xxxxxx",
                "output": {
                    "task_id": "7ed706b7-a9a9-4319-820c-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-20 10:34:41.630",
                    "scheduled_time": "2026-03-20 10:34:41.655",
                    "end_time": "2026-03-20 10:35:12.725",
                    "orig_prompt": "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。",
                    "video_url": "https://media.pixverseai.cn/xxxx.mp4"
                },
                "usage": {
                    "duration": 5,
                    "shot_type": "single",
                    "size": "1280*720",
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

## DashScope SDK调用 <span id="pv601a0h2sdk1" />

SDK 的参数命名与[HTTP接口](/zh/model-studio/pixverse-text-to-video-api-reference#pv101a0h2http)基本一致，参数结构根据语言特性进行封装。

由于文生视频任务耗时较长（通常为1-5分钟），SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### Python SDK调用 <span id="pv607a0h3pyth" />

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.8`，再运行以下代码。

  若版本过低，可能会触发 "url error, please check url!" 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

<strong>北京地域</strong>：`dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="pv626a0h5syn1" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为北京地域URL
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_sync_call_t2v():
        # call sync api, will return the result
        print('please wait...')
        rsp = VideoSynthesis.call(api_key=api_key,
                                  model='pixverse/pixverse-c1-t2v',
                                  prompt='下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。',
                                  size='1280*720',
                                  duration=5,
                                  audio=False,
                                  watermark=False)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_sync_call_t2v()
    ```

    ##### 响应示例 <span id="pv628a0h5syn2" />

    > `video_url`暂无过期时间，但不建议将其作为长期存储，请及时下载。

    ```json
    {
        "status_code": 200,
        "request_id": "2b68d32e-86c8-4383-8a18-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "ec40bb42-02d4-44d8-bf35-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://media.pixverseai.cn/xxx.mp4",
            "submit_time": "2026-03-20 10:59:01.993",
            "scheduled_time": "2026-03-20 10:59:02.028",
            "end_time": "2026-03-20 11:00:06.322",
            "orig_prompt": "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "size": "1280*720",
            "fps": 24,
            "audio": false,
            "SR": "720"
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="pv636a0h5asy1" />

    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 以下为北京地域URL
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call_t2v():
        # call async api, will return the task information
        # you can get task status with the returned task id.
        rsp = VideoSynthesis.async_call(api_key=api_key,
                                        model='pixverse/pixverse-c1-t2v',
                                        prompt='下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。',
                                        size='1280*720',
                                        duration=5,
                                        audio=False,
                                        watermark=True)
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

        # wait the task complete, will call fetch interval, and check it's in finished status.
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

    ##### 响应示例 <span id="ae28dc8f72g6i" />

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

    > `video_url` 暂无过期时间，但不建议将其作为长期存储，请及时下载。

    ```json
    {
        "status_code": 200,
        "request_id": "2b68d32e-86c8-4383-8a18-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "ec40bb42-02d4-44d8-bf35-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://media.pixverseai.cn/xxx.mp4",
            "submit_time": "2026-03-20 10:59:01.993",
            "scheduled_time": "2026-03-20 10:59:02.028",
            "end_time": "2026-03-20 11:00:06.322",
            "orig_prompt": "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。"
        },
        "usage": {
            "video_count": 1,
            "video_duration": 0,
            "video_ratio": "",
            "duration": 10,
            "size": "1280*720",
            "fps": 24,
            "audio": false,
            "SR": "720"
        }
    }
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="589b80853e6rn" />

<Tip>
  请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.6`，再运行以下代码。

  若版本过低，可能会触发 “url error, please check url!” 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

<strong>北京地域</strong>：`Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="3e3609efb9rhl" />

    ```java expandable
    // 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
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
            // 以下为北京地域url
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
        // 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * Create a video compositing task and wait for the task to complete.
         */
        public static void text2Video() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();
            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("pixverse/pixverse-c1-t2v")
                            .prompt("下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。")
                            .size("1280*720")
                            .duration(5)
                            .audio(true)
                            .watermark(true)
                            .seed(12345)
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

    ##### 响应示例 <span id="dfe6c9cc786iy" />

    > `video_url` 暂无过期时间，但不建议将其作为长期存储，请及时下载。

    ```json
    {
        "request_id": "bd1109bd-6c63-4e62-8bb8-xxxxxx",
        "output": {
            "task_id": "72af7c13-dad5-4aaa-b85d-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://media.pixverseai.cn/xxx.mp4",
            "orig_prompt": "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。",
            "submit_time": "2026-03-20 11:21:13.227",
            "scheduled_time": "2026-03-20 11:21:13.252",
            "end_time": "2026-03-20 11:21:43.924"
        },
        "usage": {
            "video_count": 1,
            "duration": 5.0,
            "size": "1280*720",
            "input_video_duration": 0.0,
            "output_video_duration": 0.0,
            "SR": "720"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="2fc30f223b9py" />

    ```java expandable
    // 调用时请将 {WorkspaceId} 替换为真实的业务空间ID。
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesis;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisListResult;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisParam;
    import com.alibaba.dashscope.aigc.videosynthesis.VideoSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.InputRequiredException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.task.AsyncTaskListParam;
    import com.alibaba.dashscope.utils.JsonUtils;
    import com.alibaba.dashscope.utils.Constants;

    public class Text2Video {

        static {
            // 以下为北京地域url
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
        // 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * Create a video compositing task and wait for the task to complete.
         */
        public static void text2Video() throws ApiException, NoApiKeyException, InputRequiredException {
            VideoSynthesis vs = new VideoSynthesis();

            VideoSynthesisParam param =
                    VideoSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("pixverse/pixverse-c1-t2v")
                            .prompt("下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。")
                            .size("1280*720")
                            .duration(5)
                            .audio(true)
                            .watermark(true)
                            .seed(12345)
                            .build();

            // 异步调用
            VideoSynthesisResult task = vs.asyncCall(param);
            System.out.println(JsonUtils.toJson(task));
            System.out.println("please wait...");

            //获取结果
            VideoSynthesisResult result = vs.wait(task, apiKey);
            System.out.println(JsonUtils.toJson(result));
        }

        // 获取任务列表
        public static void listTask() throws ApiException, NoApiKeyException {
            VideoSynthesis is = new VideoSynthesis();
            AsyncTaskListParam param = AsyncTaskListParam.builder().build();
            param.setApiKey(apiKey);
            VideoSynthesisListResult result = is.list(param);
            System.out.println(result);
        }

        // 获取单个任务结果
        public static void fetchTask(String taskId) throws ApiException, NoApiKeyException {
            VideoSynthesis is = new VideoSynthesis();
            // 如果已设置 DASHSCOPE_API_KEY 为环境变量，apiKey 可为空
            VideoSynthesisResult result = is.fetch(taskId, apiKey);
            System.out.println(result.getOutput());
            System.out.println(result.getUsage());
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

    ##### 响应示例 <span id="38e1e3c7303rm" />

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

    > `video_url` 暂无过期时间，但不建议将其作为长期存储，请及时下载。

    ```json
    {
        "request_id": "bd1109bd-6c63-4e62-8bb8-xxxxxx",
        "output": {
            "task_id": "72af7c13-dad5-4aaa-b85d-xxxxxx",
            "task_status": "SUCCEEDED",
            "video_url": "https://media.pixverseai.cn/xxx.mp4",
            "orig_prompt": "下着雨，赛博城市里，一只浣熊在栏杆上行走。突然他眼睛发出蓝光，变身成一架高科技无人机，快速飞离画面。",
            "submit_time": "2026-03-20 11:21:13.227",
            "scheduled_time": "2026-03-20 11:21:13.252",
            "end_time": "2026-03-20 11:21:43.924"
        },
        "usage": {
            "video_count": 1,
            "duration": 5.0,
            "size": "1280*720",
            "input_video_duration": 0.0,
            "output_video_duration": 0.0,
            "SR": "720"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>
</Tabs>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="pv801a0h2faq1" />

#### Q：为什么1080P分辨率下不能使用10秒时长？ <span id="pv806a0h4fq02" />

A： `pixverse-v5.6-t2v` 在 1080P 下不支持 10 秒。建议降低分辨率至 720P/540P/360P，或切换至 `pixverse-c1-t2v`或`pixverse-v6-t2v` 模型。
