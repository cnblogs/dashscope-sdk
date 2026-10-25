> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-查询Travel状态 API参考

> 查询 Directing Travel 生命周期、服务端推流状态、已执行文本指令和章节信息，也可同时上报客户端拉流或播放心跳。

<meta property="og:title" content="HappyOyster-Directing-查询Travel状态 API参考" />

## 适用范围 <span id="ho-dir-qts-scope-title" />

查询 Directing Travel 生命周期、服务端推流状态、已执行文本指令和章节信息，也可同时上报客户端拉流或播放心跳。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，主 API Key 或临时 API Key 均可调用。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：使用[客户端进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)接口返回的 `encryptedTravelId` 查询。
- <strong>调用方</strong>：您的服务端或客户端均可调用。建议每 2–5 秒轮询。

## HTTP调用 <span id="ho-dir-qts-http-title" />

<Tabs>
  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/status`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/status`

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
        #### 请求参数 <span id="ho-dir-qts-req-title" />
      </td>

      <td rowSpan={6}>
        <Tabs>
          <Tab title="查询Travel状态">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/status?encryptedTravelId={encryptedTravelId}&clientStreamStatus=PLAYING&clientStreamStatusTimeMs=1788940800000' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY"
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key 鉴权。不强制主 API Key，主 API Key 或临时 API Key 均可调用。

        - <strong>主 API Key</strong>：以 `sk-` 开头，如 `sk-xxx`。
        - <strong>临时 API Key</strong>：以 `st-` 开头，如 `st-xxx`。
      </td>
    </tr>

    <tr>
      <td>
        ##### Query 参数 <span id="ho-dir-qts-query-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedTravelId</strong> `string` <strong>（必选）</strong>

        Directing 加密 Travel ID。由[客户端进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)接口返回。
      </td>
    </tr>

    <tr>
      <td>
        <strong>clientStreamStatus</strong> `string` <strong>（可选）</strong>

        客户端 RTC 拉流或播放状态，大小写不敏感。无法识别的值会被忽略。可选值：

        - `DISCONNECTED`：未连接或已离开频道
        - `CONNECTING`：正在连接 RTC 频道
        - `CONNECTED`：已入会，尚未开始播放或首帧未到达
        - `PLAYING`：已收到远端流并正在渲染
        - `BUFFERING`：缓冲中
        - `PAUSED`：客户端暂停播放，不等同于服务端 `pause`
        - `RECONNECTING`：重连中
      </td>
    </tr>

    <tr>
      <td>
        <strong>clientStreamStatusTimeMs</strong> `long` <strong>（可选）</strong>

        客户端状态变更的毫秒时间戳。与 `clientStreamStatus` 配套使用。
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
        #### 响应参数 <span id="ho-dir-qts-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="Travel运行中">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "status": "running",
                    "rtcStatus": "PUSHING",
                    "updateTime": "2026-06-04T00:02:00Z",
                    "userInstructions": [
                        {
                            "instruction": "突然出现一只巨大的机器恐龙",
                            "relativeStartTimeMs": 12000,
                            "relativeEndTimeMs": 16000,
                            "startTime": 12.0,
                            "endTime": 16.0,
                            "status": "executed"
                        }
                    ],
                    "chapters": [
                        {
                            "chapterId": 1,
                            "title": "第一章",
                            "brief": "侦探进入赛博朋克城市",
                            "actRange": [0, 10],
                            "startTime": 4,
                            "endTime": 20,
                            "chapterImage": "https://cdn.happyoyster.com/chapters/ch1.jpg"
                        }
                    ],
                    "characterActions": [],
                    "environmentActions": []
                }
            }
            ```
          </Tab>

          <Tab title="Travel失败">
            `errorCode` 取值见[错误码](/zh/model-studio/happyoyster-error-code#ho-ec-travel-errorcode-title)。

            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "status": "failed",
                    "rtcStatus": null,
                    "updateTime": null,
                    "userInstructions": null,
                    "chapters": null,
                    "characterActions": null,
                    "environmentActions": null,
                    "errorCode": "TRAVEL_SESSION_INIT_FAILED",
                    "errorMessage": "Failed to allocate inference resources."
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

          <strong>status</strong> `string`

          Travel 生命周期状态：

          - `init`：正在初始化会话资源
          - `pending`：排队或等待服务资源
          - `running`：正在运行，可按创建子模式调用支持的控制接口
          - `paused`：已完成服务端暂停；可恢复或回溯
          - `failed`：Travel 失败；原因见 `errorCode` / `errorMessage`
          - `completed`：Travel 已结束，可查询产物

          <strong>rtcStatus</strong> `string`

          服务端 RTC 推流状态；与客户端上报的 `clientStreamStatus` 不同。

          <strong>updateTime</strong> `string`

          最近更新时间，ISO 8601 格式。

          <strong>userInstructions</strong> `array`

          文本指令列表；无数据时为 `null`。每项含 `instruction`、`relativeStartTimeMs` / `relativeEndTimeMs`（相对毫秒）、`startTime` / `endTime`（时间轴秒数）、`status`。

          <strong>chapters</strong> `array`

          章节列表；尚未触发章节检测时为 `null`。每项含 `chapterId`、`title`、`brief`、`actRange`、`startTime`、`endTime`、`chapterImage`。

          <strong>characterActions</strong> `array<string> | null`

          Directing 不支持 SDK 动作控制，固定返回空数组；`failed` 时为 `null`。

          <strong>environmentActions</strong> `array<string> | null`

          Directing 不支持 SDK 环境动作控制，固定返回空数组；`failed` 时为 `null`。

          <strong>errorCode</strong> `string`

          仅 `status=failed` 时返回；结构化失败原因代码，取值见[错误码](/zh/model-studio/happyoyster-error-code#ho-ec-travel-errorcode-title)。

          <strong>errorMessage</strong> `string`

          与 `errorCode` 同时返回；英文失败说明。请按 `errorCode` 分支处理，不要匹配 `errorMessage` 文案。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-dir-qts-notes-title" />

- 建议每 2–5 秒轮询。
- `running` 状态可按 `creationModel` 调用支持的控制接口：普通模式可 `instruct`、`pause`、`resume`、`rewind`、`end`；剧本模式可 `update-script`、`pause`、`resume`、`rewind`、`end`。
- 本接口不返回 `mode`、`aspectRatio`、`playUrl`、`bgmUrl` 或 `sessionId`；播流配置以[进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)响应为准。
- `clientStreamStatus` 是客户端播放侧心跳，`rtcStatus` 是服务端推流侧状态，两者不可互相替代。
- `failed` 是终态，不会再产生成片；请按 `errorCode` 展示失败原因，不要展示为「尚未完成」。

## 错误码 <span id="ho-dir-qts-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-qts-next-title" />

Travel 为 `running` 或 `paused` 时：

- 普通模式：[发送过程指令](/zh/model-studio/happyoyster-directing-instruct-travel-api-reference)。
- 剧本模式：[剧本全量更新](/zh/model-studio/happyoyster-directing-update-script-api-reference)。
- 通用：[暂停](/zh/model-studio/happyoyster-directing-pause-travel-api-reference) / [恢复](/zh/model-studio/happyoyster-directing-resume-travel-api-reference) / [回溯](/zh/model-studio/happyoyster-directing-rewind-travel-api-reference) / [结束](/zh/model-studio/happyoyster-directing-end-travel-api-reference)。
