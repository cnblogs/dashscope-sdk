> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Acting-客户端进入房间 API参考

> 客户端使用 ticket 创建实际 Travel，并获取 RTC 入会配置、Acting 能力版本和播放器画幅。调用成功即创建 Travel。

<meta property="og:title" content="HappyOyster-Acting-客户端进入房间 API参考" />

## 适用范围 <span id="ho-act-et-scope-title" />

客户端使用 `ticket` 创建实际 Travel，并获取 RTC 入会配置、Acting 能力版本和播放器画幅。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，使用 `ticket` 完成进房校验。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：`ticket` 由[获取体验凭证](/zh/model-studio/happyoyster-acting-get-travel-credential-api-reference)接口换取，未过期且未使用，对应 World 状态为 `ready`。
- <strong>调用方</strong>：您的客户端调用。

## HTTP调用 <span id="ho-act-et-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/enter-travel`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/enter-travel`

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
        #### 请求参数 <span id="ho-act-et-req-title" />
      </td>

      <td rowSpan={6}>
        <Tabs>
          <Tab title="客户端进入房间">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/enter-travel' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "ticket": "{ticket}"
            }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。
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
        ##### 请求体（Request Body） <span id="ho-act-et-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>ticket</strong> `string` <strong>（必选）</strong>

        未过期且未使用的一次性凭证。由[获取体验凭证](/zh/model-studio/happyoyster-acting-get-travel-credential-api-reference)接口换取。同一 `ticket` 再次使用返回 `401011`。
      </td>
    </tr>
  </tbody>
</table>

<Note>
  Acting 进房不消费 `maxExperienceTimeSec`，请勿传入。若仍传入非法档位，会在模型分流前返回 `400000`。
</Note>

<table bordertype="no-border">
  <colgroup>
    <col style={{ width: "53.5%" }} />

    <col style={{ width: "46.5%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="ho-act-et-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="进房成功">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "encryptedWorldId": "enc_a1b2****",
                    "mode": 3,
                    "creationModel": "simple",
                    "playUrl": null,
                    "firstFrame": "https://cdn.happyoyster.com/frames/acting_world_xyz789.jpg",
                    "rtcConfig": {
                        "channelId": "stream_abc123",
                        "appId": "18bca2e3218c46aebf8ff3a32fb12311",
                        "token": "007eJxTYOh...",
                        "userId": "user_1"
                    },
                    "version": "actingV2",
                    "aspectRatio": "9:16",
                    "noStreamAutoEndTimeoutSec": 30,
                    "maxExperienceTimeSec": null
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

          新创建的加密 Travel ID。后续[查询Travel状态](/zh/model-studio/happyoyster-acting-query-travel-status-api-reference)、暂停、恢复、结束、产物查询均使用此值。

          <strong>encryptedWorldId</strong> `string`

          当前 Travel 对应的加密 World ID。

          <strong>mode</strong> `integer`

          Acting 恒为 `3`。

          <strong>creationModel</strong> `string`

          Acting 恒为 `simple`。

          <strong>playUrl</strong> `null`

          暂不可用，固定为 `null`。

          <strong>firstFrame</strong> `string`

          World 首帧 URL。

          <strong>rtcConfig</strong> `object`

          RTC 入会配置；没有可用推流频道时为 `null`，客户端不能据此开始播放。

          - `channelId`：RTC 频道 ID
          - `appId`：平台分配的 RTC 应用 ID
          - `token`：RTC 入会 Token
          - `userId`：RTC 入会用户 ID，固定为 `user_1`

          <strong>version</strong> `string`

          Acting 进房版本，固定为 `actingV2`。须按此版本调用控制接口。

          <strong>aspectRatio</strong> `string`

          画幅。客户端须在拉流前据此设置播放器方向：

          - `9:16`（竖屏）
          - `16:9`（横屏）

          <strong>noStreamAutoEndTimeoutSec</strong> `integer`

          无推流自动结束超时秒数，默认 30，以实际响应为准。进房后若在此时间内 `rtcStatus` 未进入推流态，应调用[结束Travel](/zh/model-studio/happyoyster-acting-end-travel-api-reference)并传入 `failCode=TRAVEL_NO_STREAM_AUTO_END`。

          <strong>maxExperienceTimeSec</strong> `null`

          Acting 不使用最大体验时长，固定返回 `null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-act-et-notes-title" />

- `ticket` 对应的 World 必须为 `ready`，且 ticket 未过期、未使用。
- 调用成功即创建 Travel；同一 `ticket` 再次使用返回 `401011`。
- Acting 功能或规格未开通时返回 `403007`，且不会创建 Travel。
- `version` 必须按 `actingV2` 处理；不要按其它能力版本调用控制接口。
- 客户端应在建立拉流或渲染容器前读取 `aspectRatio`：`9:16` 使用竖屏容器，`16:9` 使用横屏容器。
- 不要传 `maxExperienceTimeSec`。Acting 不按该字段自动结束，客户端不能据此做倒计时。
- `rtcConfig=null` 表示当前没有可用推流频道，客户端不能据此开始播放。

## 错误码 <span id="ho-act-et-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-act-et-next-title" />

进房成功后：

- [查询Travel状态](/zh/model-studio/happyoyster-acting-query-travel-status-api-reference)：每 2–5 秒轮询，Travel 为 `running` 或 `paused` 时可发送指令。
- [发送文本过程指令](/zh/model-studio/happyoyster-acting-instruct-travel-api-reference)：驱动后续角色表演。
- [暂停Travel](/zh/model-studio/happyoyster-acting-pause-travel-api-reference) / [恢复Travel](/zh/model-studio/happyoyster-acting-resume-travel-api-reference)：按需控制运行节奏。
- [结束Travel](/zh/model-studio/happyoyster-acting-end-travel-api-reference)：结束时进入 `completed`，开始处理产物。
