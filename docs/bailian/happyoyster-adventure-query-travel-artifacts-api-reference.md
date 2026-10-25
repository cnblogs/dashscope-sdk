> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Adventure-查询Travel产物 API参考

> 查询已完成 Adventure Travel 的录制原片和三个合成变体。对外交付推荐使用 withInstructionAndWatermark。

<meta property="og:title" content="HappyOyster-Adventure-查询Travel产物 API参考" />

## 适用范围 <span id="ho-adv-qta-scope-title" />

查询已完成 Adventure Travel 的录制原片和三个合成变体。`withInstruction` 在 Adventure 中表示**探索交互贴图版**，不是文本指令字幕版。对外交付推荐使用 `withInstructionAndWatermark`。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>前置条件</strong>：Travel 状态为 `completed`。原片 URL 可用是返回产物响应的前提。可通过[查询Travel状态](/zh/model-studio/happyoyster-adventure-query-travel-status-api-reference)接口确认。
- <strong>调用方</strong>：您的服务端调用。

## HTTP调用 <span id="ho-adv-qta-http-title" />

<Tabs>
  <Tab title="华北2（北京）">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels/artifacts`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels/artifacts`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels/artifacts`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<table bordertype="no-border">
  <colgroup>
    <col style={{ width: "53.91%" }} />

    <col style={{ width: "46.09%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="ho-adv-qta-req-title" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="查询Travel产物">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels/artifacts?encryptedTravelId={encryptedTravelId}' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY"
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key 鉴权。仅支持<strong>主 API Key</strong>，以 `sk-` 开头，如 `sk-xxx`。通常配置为环境变量 `$DASHSCOPE_API_KEY`。临时 API Key（`st-` 开头）调用返回 `403003`。
      </td>
    </tr>

    <tr>
      <td>
        ##### Query 参数 <span id="ho-adv-qta-query-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedTravelId</strong> `string` <strong>（必选）</strong>

        状态为 `completed` 的 Adventure 加密 Travel ID。由[客户端进入房间](/zh/model-studio/happyoyster-adventure-enter-travel-api-reference)接口返回。
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border">
  <colgroup>
    <col style={{ width: "53.5%" }} />

    <col style={{ width: "46.5%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="ho-adv-qta-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="四路产物全部就绪">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "composeStatus": "ready",
                    "video": {
                        "original": {
                            "url": "https://cdn.happyoyster.com/exports/trvl_a1b2c3d4e5f6_raw.mp4?v=2",
                            "status": "ready",
                            "resolution": "720p",
                            "durationSec": 90
                        },
                        "withWatermark": {
                            "url": "https://cdn.happyoyster.com/exports/trvl_a1b2c3d4e5f6_wm.mp4?v=2",
                            "status": "ready",
                            "resolution": null,
                            "durationSec": 90
                        },
                        "withInstruction": {
                            "url": "https://cdn.happyoyster.com/exports/trvl_a1b2c3d4e5f6_overlay.mp4?v=2",
                            "status": "ready",
                            "resolution": null,
                            "durationSec": 90
                        },
                        "withInstructionAndWatermark": {
                            "url": "https://cdn.happyoyster.com/exports/trvl_a1b2c3d4e5f6_all.mp4?v=2",
                            "status": "ready",
                            "resolution": null,
                            "durationSec": 90
                        }
                    }
                }
            }
            ```
          </Tab>

          <Tab title="合成仍在处理">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "composeStatus": "processing",
                    "video": {
                        "original": {
                            "url": "https://cdn.happyoyster.com/exports/trvl_a1b2c3d4e5f6_raw.mp4?v=2",
                            "status": "ready",
                            "resolution": "720p",
                            "durationSec": 90
                        },
                        "withWatermark": {
                            "url": null,
                            "status": "processing",
                            "resolution": null,
                            "durationSec": null
                        },
                        "withInstruction": {
                            "url": null,
                            "status": "processing",
                            "resolution": null,
                            "durationSec": null
                        },
                        "withInstructionAndWatermark": {
                            "url": null,
                            "status": "processing",
                            "resolution": null,
                            "durationSec": null
                        }
                    }
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong> `integer`

        返回码。`0` 表示成功，非 0 为错误码。
      </td>
    </tr>

    <tr>
      <td>
        <strong>message</strong> `string`

        错误信息。成功时为 `null`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>data</strong> `object`

        响应数据。失败时为 `null`。

        <Accordion title="属性" defaultOpen>
          <strong>encryptedTravelId</strong> `string`

          加密 Travel ID。

          <strong>composeStatus</strong> `string`

          三个合成变体的聚合状态：

          - `ready`：`withWatermark`、`withInstruction`、`withInstructionAndWatermark` 均为 `ready`
          - `partial`：至少一个合成变体为 `ready`，但未全部就绪
          - `processing`：三个合成变体均不是 `ready`

          只聚合三个合成变体，不包含 `original`。

          <strong>video</strong> `object`

          主线视频的四种固定变体。每项含 `url`、`status`、`resolution`、`durationSec`。

          - `original`：录制原片，优先返回可用的 720p 表示
          - `withWatermark`：仅水印合成版
          - `withInstruction`：仅探索交互贴图合成版（不是文本指令字幕版）
          - `withInstructionAndWatermark`：探索交互贴图 + 水印合成版，推荐用于对外交付

          <strong>video.\*.url</strong> `string`

          下载 URL；`processing` 或 `unavailable` 时为 `null`。

          <strong>video.\*.status</strong> `string`

          单项状态：

          - `ready`：已就绪，`url` 可访问
          - `processing`：仍在合成，`url=null`
          - `unavailable`：合成失败或 URL 暂不可用，`url=null`

          <strong>video.\*.resolution</strong> `string`

          `original` 命中 720p 时为 `"720p"`；其它情况可能为 `null`。

          <strong>video.\*.durationSec</strong> `integer`

          统一解析得到的视频时长秒数；可解析时所有 `ready` 变体均填写该值，`processing` / `unavailable` 或时长暂不可解析时可为 `null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-adv-qta-notes-title" />

- 客户端可在 `composeStatus != ready` 时按受控间隔轮询。
- 不要把 `withInstruction` 解释为导演文本字幕；在 Adventure 中它是探索交互贴图。
- 对外交付推荐读取 `video.withInstructionAndWatermark`；如业务只需要原片，可继续读取 `video.original.url`。
- 四个视频变体共用同一时长解析结果，因此同一次响应中已就绪且时长可解析的变体会返回一致的 `durationSec`。
- `TRAVEL_NO_STREAM_AUTO_END` 结束的 Travel 为 `failed`，不会生成可查询产物。
- 不要将 `404000` 一律展示为「尚未完成」：请先通过[查询Travel状态](/zh/model-studio/happyoyster-adventure-query-travel-status-api-reference)或[查询Travel列表](/zh/model-studio/happyoyster-adventure-query-travel-list-api-reference)读取 `status` 与 `errorCode`；对 `failed` 的 Travel 展示失败原因。

##### 404000 场景 <span id="ho-adv-qta-404000-h5" />

| 场景                                           | 接口表现                                                                                                                           |
| -------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------ |
| Travel 仍在进行中（`init` / `pending` / `running`） | 返回业务码 `404000`，`message` = `Video is still being generated, please try again once the process is complete`                     |
| Travel 处于 `failed`                           | 返回业务码 `404000`，`message` = `Experience failed and no video was produced (errorCode=<errorCode>): <errorMessage>`；失败是终态，不会再产出视频 |
| Travel 不存在、不归属或不是 Adventure                  | 返回业务码 `404000`                                                                                                                 |
| 原片 URL 不可用                                   | 返回业务码 `404000`；原片是整个接口的硬门槛                                                                                                     |
| 原片已就绪、合成仍在处理                                 | HTTP 200；对应合成条目 `status=processing`、`url=null`                                                                                 |
| 合成失败或 URL 暂不可用                               | HTTP 200；对应合成条目 `status=unavailable`、`url=null`                                                                                |

## 错误码 <span id="ho-adv-qta-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-adv-qta-next-title" />

- [查询Travel列表](/zh/model-studio/happyoyster-adventure-query-travel-list-api-reference)：查看其它历史 Travel。
- [结束Travel](/zh/model-studio/happyoyster-adventure-end-travel-api-reference)：结束新的 Travel 后再来查询产物。
