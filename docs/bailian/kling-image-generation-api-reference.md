> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 可灵-图像生成API参考

> 可灵-图像生成模型支持 文生图 、 参考图生图 两种任务。

<meta property="og:title" content="可灵-图像生成API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 模型概览 <span id="e5bd43cfa8l83" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.38%" }} /><col style={{ width: "27.54%" }} /><col style={{ width: "24.98%" }} /><col style={{ width: "30.1%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>能力支持</strong></p></th><th><p><strong>输入模态</strong></p></th><th><p><strong>输出图像规格</strong></p></th></tr></thead><tbody><tr><td><p>kling/kling-v3-image-generation</p></td><td><ul><li><p>文生图</p></li> <li><p>参考图生图：仅单图输入。</p></li></ul></td><td><p>文本、图像</p></td><td><p>图像分辨率：1k、2k</p> <p>宽高比：16:9、9:16、1:1</p> <p>图像张数：通过参数n指定（1～9）</p> <p>图像格式：png</p></td></tr><tr><td><p>kling/kling-v3-omni-image-generation</p></td><td><ul><li><p>文生图</p></li> <li><p>参考图生图：支持多图输入，支持生成分镜组图。</p></li></ul></td><td><p>文本、图像</p></td><td><p>图像分辨率：1k、2k、4k</p> <p>宽高比：16:9、9:16、1:1</p> <p>图像张数：</p> <ul><li><p>单图模式通过参数<code>n</code>指定：1～9</p></li> <li><p>组图模式通过<code>series\_amount</code>指定：2～9</p></li></ul> <p>图像格式：png</p></td></tr></tbody></table>

## 前提条件 <span id="8bb48973d87d6" />

1. <strong>开通服务</strong>：前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索“可灵”，找到<strong>可灵AI</strong>模型卡片，单击<strong>立即开通</strong>，在弹窗内确认开通及授权。
2. <strong>配置API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP调用 <span id="omni_http_h2" />

图像生成任务有一定耗时（通常为1-2分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤一：提交图像生成任务 <span id="omni_step1_h3" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="omni_req_hdr_h4" />

        ##### 请求头（Headers） <span id="omni_headers_h5" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="omni_body_h5" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选值：

        - `kling/kling-v3-image-generation`
        - `kling/kling-v3-omni-image-generation`

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入参数对象，包含以下字段：

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。<strong>当前仅支持单轮对话</strong>，因此数组内<strong>有且只有一个对象</strong>，该对象包含`role`和`content`两个属性。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` （可选）

            消息的角色。此参数必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容，包含文本提示词（text）和可选的参考图像（image，支持多张）。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong>`string`<strong>（条件必选）</strong>

              正向提示词，用于描述期望生成的图像内容、风格和构图。

              支持中英文，长度不超过2500个字符，每个汉字、字母、数字或符号计为一个字符，超过将返回错误。

              示例值：一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。

              <strong>注意</strong>：仅支持传入一个text，不传或传入多个将报错。

              <strong>image</strong> `string` （可选）

              参考图像的URL。

              - 支持 HTTP 或 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.png">[https://xxx/xxx.png](https://xxx/xxx.png)</a>。

              图像限制：

              - 格式：JPEG、JPG、PNG（不支持透明通道）。
              - 分辨率：宽和高的范围为\[300, 8000]像素。
              - 宽高比：在1:2.5 \~ 2.5:1之间。
              - 文件大小：不超过10MB。
              - 数量限制：参考图片数量和参考主体数量（element\_list数组长度）之和<strong>不得超过10</strong>。
            </Accordion>
          </Accordion>

          <strong>element\_list</strong> `array` （可选）

          主体列表，用于指定需要保持的主体。

          <Accordion title="属性" defaultOpen>
            <strong>element\_id</strong> `integer` （条件必填）

            传`element_list`时必填，表示主体ID。请在[可灵-主体ID列表](/zh/model-studio/kling-object-ids)获取主体ID。

            数量限制：参考图片数量和参考主体数量（element\_list数组长度）之和<strong>不得超过10</strong>。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        控制图像生成，比如图像张数、宽高比等。

        <Accordion title="属性" defaultOpen>
          <strong>n</strong> `integer` （可选）

          生成的图像张数。

          - kling/kling-v3-image-generation：取值范围为1～9。默认值为1。
          - kling/kling-v3-omni-image-generation：

            - 当且仅当`result_type=single`时生效。
            - 取值范围为1～9。默认值为1。

          <strong>result\_type</strong> `string` （可选）

          支持模型：kling/kling-v3-omni-image-generation。

          生成图像的类型。

          - `single`（ 默认值）：单图。批量生成时仅风格相似，无分镜关联。
          - `series`：组图。生成具有叙事/视觉连续性的分镜系列图像。

          <strong>series\_amount</strong> `integer` （可选）

          支持模型：kling/kling-v3-omni-image-generation。

          组图模式下的输出张数。取值范围为2～9，默认值为4。

          生效条件：当且仅当`result_type=series`时生效。

          <strong>aspect\_ratio</strong> `string` （可选）

          输出图像的宽高比。

          - `16:9`：默认值。
          - `9:16`
          - `1:1`

          示例值：16:9。

          <strong>resolution</strong> `string` （可选）

          输出图像分辨率。

          - kling/kling-v3-image-generation：可选值为`1k`、`2k`，默认值为`1k`。
          - kling/kling-v3-omni-image-generation：可选值为`1k`、`2k`、`4k`，默认值为`1k`。

          示例值：1k。

          <strong>watermark</strong> `bool` （可选）

          是否同时生成含水印的图像。水印位于图像右下角，文案固定为”可灵AI”。

          - `false`：默认值，不生成含水印的图像。
          - `true`：同时生成含水印的图像。

          示例值：false。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文生图">
            支持模型：`kling/kling-v3-omni-image-generation`、`kling/kling-v3-image-generation`。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "kling/kling-v3-image-generation",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "一间有着精致窗户的花店,漂亮的木质门,摆放着花朵"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "n": 2,
                    "aspect_ratio": "1:1",
                    "resolution": "1k"
                }
            }'
            ```
          </Tab>

          <Tab title="图生图（组图模式）">
            支持模型：`kling/kling-v3-omni-image-generation`。

            支持以下模式：

            - <strong>单图</strong>（`result_type=single`）：独立生成，批量时仅风格相似。
            - <strong>组图</strong>（`result_type=series`）：分镜序列生成，保持角色/场景/叙事连续性。

            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'X-DashScope-Async: enable' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model": "kling/kling-v3-omni-image-generation",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "参考图1的风格和图2的背景，生成番茄炒蛋"
                                },
                                {
                                    "image": "https://cdn.wanx.aliyuncs.com/tmp/pressure/umbrella1.png"
                                },
                                {
                                    "image": "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "n": 4,
                    "result_type": "series",
                    "aspect_ratio": "1:1",
                    "resolution": "1k"
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
        #### 响应参数 <span id="82c91c55bajs9" />

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

### 步骤二：查询任务结果 <span id="omni_step2_h3" />

<strong>北京地域</strong>：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Note>
  - <strong>轮询建议</strong>：图像生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 5 秒）来获取结果。
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
        #### 响应参数 <span id="d49207c2957vv" />

        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>choices</strong> `array`

          模型生成的输出内容。此数组仅包含一个元素。

          <Accordion title="属性" defaultOpen>
            <strong>finish\_reason</strong> `string`

            任务停止原因，自然停止时为`stop`。

            <strong>message</strong> `object`

            模型返回的消息。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              消息的角色，固定为`assistant`。

              <strong>content</strong>`array`

              <Accordion title="属性" defaultOpen>
                <strong>type</strong> `string`

                输出内容的类型。固定为`image`。

                <strong>image</strong> `string`

                生成图像的 URL，图像格式为PNG。<strong>链接有效期为30天</strong>，请及时下载并保存图像。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的数量。

          <strong>size</strong> `string`

          生成图片的分辨率，格式为`宽*高`。示例值：1360\*768。

          <strong>SR</strong> `string`

          生成图像的分辨率档位。示例值：1080。
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
          <Tab title="任务执行成功">
            ```json expandable
            {
                "request_id": "95146d89-9d70-481a-8c16-xxxxxx",
                "output": {
                    "task_id": "2c502d25-12a9-4517-8972-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-27 22:46:01.536",
                    "scheduled_time": "2026-03-27 22:46:01.580",
                    "end_time": "2026-03-27 22:46:24.831",
                    "finished": true,
                    "choices": [
                        {
                            "finish_reason": "stop",
                            "message": {
                                "role": "assistant",
                                "content": [
                                    {
                                        "image": "https://p4-fdl.klingai.com/xxx.png?xxx",
                                        "type": "image"
                                    },
                                    {
                                        "image": "https://p4-fdl.klingai.com/xxx.png?xxx",
                                        "type": "image"
                                    }
                                ]
                            }
                        }
                    ]
                },
                "usage": {
                    "size": "1024*1024",
                    "image_count": 2,
                    "SR": "1080"
                }
            }
            ```
          </Tab>

          <Tab title="任务执行异常">
            如果因为某种原因导致任务执行失败，将返回相关信息，可以通过code和message字段明确指示错误原因。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "a4d78a5f-655f-9639-8437-xxxxxx",
                "code": "InvalidParameter",
                "message": "num_images_per_prompt must be 1"
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
