> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 视频风格重绘API参考

> 视频风格重绘 API 可将输入视频转换为多种预设艺术风格，并保证画面动态流畅、内容连贯。支持8种预设风格：日式漫画、美式漫画、清新漫画、3D卡通、国风卡通、纸艺风格、简易插画、国风水墨。

<meta property="og:title" content="视频风格重绘API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 效果示意 <span id="95fb1121df2gl" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50.191083%" }} />

    <col style={{ width: "49.808917%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>输入视频</strong>
      </th>

      <th>
        <strong>输出视频（日式漫画）</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4" type="video/mp4" />
        </video>
      </td>

      <td>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/escsom/input_00002_%E6%BC%AB%E7%94%BBV4_00001.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>
  </tbody>
</table>

更多案例请参见[附录：更多风格效果示意](/zh/model-studio/video-style-transform-api-reference#a45f4df85e6h7)。

## 前提条件 <span id="3b14076a680k8" />

在调用前，您需要[获取 API Key](/zh/model-studio/get-api-key)，再[配置API Key为环境变量DASHSCOPE\_API\_KEY](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP调用 <span id="aDbIs" />

因视频处理耗时长，为避免同步请求超时，视频风格重绘采用异步调用，分为以下两步：

1. <strong>提交异步任务</strong>：通过 `POST` 请求提交原始视频 URL 和期望的风格参数，获取一个唯一的 `task_id`。
2. <strong>查询任务结果</strong>：使用 `task_id` 通过 `GET` 请求轮询任务状态，直至任务完成并获取结果视频的 URL。

### 步骤1：提交视频风格重绘任务 <span id="bepIa" />

```bash
POST https://dashscope.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis
```

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求 <span id="afcd41d2b57zf" />

        ##### 请求头（Headers） <span id="d11205db082b6" />

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

        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。设置为`video-style-transform`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入内容。

        <Accordion title="属性" defaultOpen>
          <strong>video\_url</strong>`string`<strong>（必选）</strong>

          输入视频公网URL。例如：`https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4`。

          输入视频要求：

          - <strong>分辨率</strong>：视频单边尺寸不小于 256 像素，不超过 4096 像素。长边与短边的比例不超过 1.8。
          - <strong>格式</strong>：支持 MP4、AVI、MKV、MOV、FLV、TS、MPG、MXF。
          - <strong>时长</strong>：不超过 30 秒。
          - <strong>大小</strong>：不超过 100 MB。
          - <strong>URL</strong>：若原始URL包含中文字符等非ASCII字符，请先进行URL编码。
        </Accordion>

        <strong>parameters</strong>`object` <strong>（可选）</strong>

        视频处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>style</strong>`int`<strong>（可选）</strong>

          风格类型，预设类型如下：

          - 0：日式漫画，默认值
          - 1：美式漫画
          - 2：清新漫画
          - 3：3D卡通
          - 4：国风卡通（古装输入最佳）
          - 5：纸艺风格
          - 6：简易插画
          - 7：国风水墨

          <strong>video\_fps</strong>`int`<strong>（可选）</strong>

          生成视频的帧率，默认为15，范围区间为\[15, 25]。

          <strong>animate\_emotion</strong>`bool`<strong>（可选）</strong>

          是否进行面部表情优化。默认为`true`。

          开启后，通常能提升口型与表情同步精度。在人脸区域占比较小时，关闭此项可能效果更佳。

          <strong>min\_len</strong>`int`<strong>（可选）</strong>

          指定输出视频的短边像素，用于控制分辨率。可选值为`720`或`540`，默认为`720`。

          <Note>
            此参数值影响计费，720P视频的费用会高于540P。详情请参见[计费与限流](/zh/model-studio/video-style-transform-api-reference#34ab17504ad65)。
          </Note>

          <strong>use\_SR</strong>`bool`<strong>（可选）</strong>

          是否对风格重绘后视频进行超分辨率（Super-Resolution，SR）处理。默认为`false`。设置为`true`，将免费提升画质。

          <Note>
            若`min_len`设置为540，开启此项后，输出视频将提升至1080P画质，但计费仍按照540P标准。这会增加处理耗时，推荐在需要高画质输出时开启。
          </Note>
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="生成720P视频">
            <CodeGroup dropdown>
              ```bash
              curl --location --request POST 'https://dashscope.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
              --header 'X-DashScope-Async: enable' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '{
                  "model": "video-style-transform",
                  "input": {
                      "video_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4"
                  },
                  "parameters": {
                      "style": 0,
                      "video_fps": 15
                  }
              }'
              ```

              ```python expandable
              import requests
              import os

              DASHSCOPE_API_KEY = os.getenv("DASHSCOPE_API_KEY")
              # 替换为你的视频 URL
              video_url = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4"

              response = requests.post(
                          "https://dashscope.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis",
                          headers={
                              "Authorization": f"Bearer {DASHSCOPE_API_KEY}",
                              "X-DashScope-Async": "enable",
                          },
                          json={
                          "model": "video-style-transform",
                          "input": {
                              "video_url": video_url
                          },
                          "parameters": {
                              "style": 0,
                              "video_fps": 15
                          }
                      }
                      )
              print(response.json())
              ```
            </CodeGroup>
          </Tab>

          <Tab title="生成540P视频">
            <CodeGroup dropdown>
              ```bash
              curl --location --request POST 'https://dashscope.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
              --header 'X-DashScope-Async: enable' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '{
                  "model": "video-style-transform",
                  "input": {
                      "video_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4"
                  },
                  "parameters": {
                      "style": 0,
                      "video_fps": 15,
                      "min_len": 540
                  }
              }'
              ```

              ```python expandable
              import requests
              import os

              DASHSCOPE_API_KEY = os.getenv("DASHSCOPE_API_KEY")
              # 替换为你的视频 URL
              video_url = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4"

              response = requests.post(
                          "https://dashscope.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis",
                          headers={
                              "Authorization": f"Bearer {DASHSCOPE_API_KEY}",
                              "X-DashScope-Async": "enable",
                          },
                          json={
                          "model": "video-style-transform",
                          "input": {
                              "video_url": video_url
                          },
                          "parameters": {
                              "style": 0,
                              "video_fps": 15,
                              "min_len": 540
                          }
                      }
                      )
              print(response.json())
              ```
            </CodeGroup>
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

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          任务id，任务的唯一标识，用于后续查询。

          <strong>task\_status</strong>`string`

          任务状态。

          - PENDING：排队中
          - RUNNING：处理中
          - SUSPENDED：挂起
          - SUCCEEDED：执行成功
          - FAILED：执行失败
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。

        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td>
        <CodeGroup>
          ```json 成功响应
          {
              "output": {
          	  "task_id": "xxxxxxxx",
                    "task_status": "PENDING"
              },
              "request_id": "7574ee8f-38a3-4b1e-9280-11c33ab46e51"
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

### 步骤2：查询任务执行状态和结果 <span id="1c5471b844yvg" />

```bash
GET https://dashscope.aliyuncs.com/api/v1/tasks/{task_id}
```

<Tip>
  任务结果数据（如任务状态、生成的视频URL等）有效期为24小时，超时后会被自动清除。请务必及时查询并保存结果。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求 <span id="5043fa882c26r" />

        ##### 请求头（Headers） <span id="2af8ee144dwd5" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong>`string`<strong>（必选）</strong>

        任务id。
      </td>

      <td>
        <Tabs>
          <Tab title="获取任务结果">
            您需要将`{task_id}`替换为真实的`task_id`。

            <CodeGroup dropdown>
              ```bash
              curl -X GET \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              https://dashscope.aliyuncs.com/api/v1/tasks/{task_id}
              ```

              ```python
              import requests
              import os

              DASHSCOPE_API_KEY = os.getenv("DASHSCOPE_API_KEY")
              # task_id 请替换为你的 task_id
              task_id = "0c9c33e6-b2e7-41e5-*********"

              task_response = requests.get(
                      f"https://dashscope.aliyuncs.com/api/v1/tasks/{task_id}",
                      headers={
                          "Authorization": f"Bearer {DASHSCOPE_API_KEY}"
                      })
              print(task_response.json())
              ```
            </CodeGroup>
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

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>output\_video\_url</strong>`string`

          结果视频URL地址。例如：`http://xxx/result.mp4`。

          <strong>task\_id</strong> `string`

          任务ID。查询有效期24小时。

          <strong>task\_status</strong>`string`

          任务状态。

          - PENDING：排队中
          - RUNNING：处理中
          - SUSPENDED：挂起
          - SUCCEEDED：执行成功
          - FAILED：执行失败
          - UNKNOWN：任务不存在或状态未知。

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong>`object`

        输出信息统计。

        <Accordion title="属性" defaultOpen>
          <strong>duration</strong>`float`

          生成视频时长（秒）。

          <strong>SR</strong>`int`

          用于计费的视频短边像素值（该值与您请求中设置的<strong>min\_len</strong>值相同）。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "b67df059-ca6a-9d51-afcd-xxxxxxxxxxxx",
                "output": {
                    "task_id": "d76ec1e8-ea27-4038-8913-xxxxxxxxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2024-05-16 13:50:01.247",
                    "scheduled_time": "2024-05-16 13:50:01.354",
                    "end_time": "2024-05-16 13:50:27.795",
                    "output_video_url": "http://xxx/result.mp4"
                },
                "usage": {
                    "duration": 3,
                    "SR": 720
                }
            }
            ```
          </Tab>

          <Tab title="任务执行中">
            任务提交后将处于排队状态，在得到调度之后将转为运行状态，此时任务的状态为RUNNING；

            ```json
            {
                "request_id":"e5d70b02-ebd3-98ce-9fe8-xxxxxxxxxxxx",
                "output":{
                    "task_id":"13b1848b-5493-4c0e-xxxxxxxxxxxx",
                    "task_status":"RUNNING",
                    "submit_time":"2025-09-08 15:53:13.143",
                    "scheduled_time":"2025-09-08 15:53:13.169"
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            ```json
            {
                "request_id": "dccfdf23-b38e-97a6-a07b-xxxxxxxxxxxx",
                "output": {
                    "task_id": "4cbabbdf-2c1f-43f4-b983-xxxxxxxxxxxx",
                    "task_status": "FAILED",
                    "submit_time": "2024-05-16 14:15:14.103",
                    "scheduled_time": "2024-05-16 14:15:14.154",
                    "end_time": "2024-05-16 14:15:14.694",
                    "code": "InvalidParameter.FileDownload",
                    "message": "download for input video error"
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 计费与限流 <span id="34ab17504ad65" />

仅对<strong>执行成功</strong>的任务计费，费用根据输出视频的实际时长（秒）和所选分辨率计算。

<strong>计费公式</strong>：`总费用 = 输出视频时长 (秒) × 对应分辨率的单价`（<strong>最终费用将严格按照任务成功后返回的</strong>`usage`<strong>对象中的</strong>`duration`<strong>和</strong>`SR`<strong>字段进行结算</strong>）

免费额度按输出视频时长进行抵扣，不区分视频分辨率。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "19.438669%" }} /><col style={{ width: "29.417879%" }} /><col style={{ width: "7.484407%" }} /><col style={{ width: "9.251559%" }} /><col style={{ width: "17.879418%" }} /><col style={{ width: "16.528067%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a></p></th><th rowSpan={2} colSpan={2}><p><strong>计费单价</strong></p></th><th colSpan={2}><p><strong>限流（主账号与RAM子账号共用）</strong></p></th></tr><tr><th><p><strong>任务下发接口QPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td rowSpan={2}><p>video-style-transform</p></td><td rowSpan={2}><p>600秒</p></td><td><p>720P</p></td><td><p>0.5元/秒</p></td><td rowSpan={2}><p>2</p></td><td rowSpan={2}><p>1</p></td></tr><tr><td><p>540P</p></td><td><p>0.2元/秒</p></td></tr></tbody></table>

<strong>计费示例</strong>

假设您提交一个 10 秒的视频，选择 720P 分辨率进行风格转换，任务成功后生成的视频时长为 10 秒。则本次任务费用为：`10 秒 × 0.5 元/秒 = 5 元`。

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 附录：更多风格效果示意 <span id="a45f4df85e6h7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "14.129444%" }} />

    <col style={{ width: "42.570647%" }} />

    <col style={{ width: "43.299909%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>风格名称</strong>
      </th>

      <th>
        <strong>原始视频</strong>
      </th>

      <th>
        <strong>重绘效果</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>日式漫画（style=0）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/viwndw/%E5%8E%9F%E8%A7%86%E9%A2%91.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250704/escsom/input_00002_%E6%BC%AB%E7%94%BBV4_00001.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>美式漫画（style=1）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/jfqzce/car_%E5%8E%9F%E5%9B%BE.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/jzfksx/car_style_1.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>清新漫画（style=2）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/brrqow/%E6%8C%A5%E6%89%8B_%E5%8E%9F%E5%9B%BE.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/rlttwr/%E6%8C%A5%E6%89%8B_style_2.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>3D卡通（style=3）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/cwxrjg/%E9%87%8E%E9%A4%90_%E5%8E%9F%E5%9B%BE.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/djibfo/%E9%87%8E%E9%A4%90_style_3.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>国风卡通（style=4）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/aqcsbd/%E5%BC%B9%E7%90%B43_%E5%8E%9F%E5%9B%BE.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/fpcbry/%E5%BC%B9%E7%90%B4_style_4.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>纸艺风格（style=5）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/jkzdwy/%E7%94%9F%E6%88%90_%E6%B1%BD%E8%BD%A6.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/rywoib/%E6%B1%BD%E8%BD%A6_%E7%BA%B8%E8%89%BA%E9%A3%8E%E6%A0%BC.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>简易插画（style=6）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/dkryur/%E6%A1%8C%E9%9D%A2_%E5%8E%9F%E5%9B%BE.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/fhxzka/%E6%A1%8C%E9%9D%A2_style_6.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>国风水墨（style=7）</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/dqidfz/%E5%8F%A4%E9%95%87_%E5%8E%9F%E5%9B%BE.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250909/wmulgj/%E5%8F%A4%E9%95%87_style_7.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>
  </tbody>
</table>
