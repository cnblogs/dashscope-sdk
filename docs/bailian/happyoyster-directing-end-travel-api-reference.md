> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-结束Travel API参考

> 结束 Directing Travel。正常结束时状态变为 completed 并进入产物处理；无推流超时结束时变为 failed，且不生成回放产物。

<meta property="og:title" content="HappyOyster-Directing-结束Travel API参考" />

## 适用范围 <span id="ho-dir-en-scope-title" />

结束 Directing Travel。正常结束时状态变为 `completed` 并进入产物处理；无推流超时结束时变为 `failed`，且不生成回放产物。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，主 API Key 或临时 API Key 均可调用。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：Travel 必须尚未进入终态，并处于服务端可结束的状态。可通过[查询Travel状态](/zh/model-studio/happyoyster-directing-query-travel-status-api-reference)接口确认。
- <strong>调用方</strong>：您的服务端或客户端均可调用。

## HTTP调用 <span id="ho-dir-en-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/end`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/end`

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
        #### 请求参数 <span id="ho-dir-en-req-title" />
      </td>

      <td rowSpan={7}>
        <Tabs>
          <Tab title="正常结束">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/end' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "encryptedTravelId": "{encryptedTravelId}",
                "userAgent": "your-client/1.2.0"
            }'
            ```
          </Tab>

          <Tab title="无推流超时结束">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/end' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "encryptedTravelId": "{encryptedTravelId}",
                "failCode": "TRAVEL_NO_STREAM_AUTO_END",
                "userAgent": "your-client/1.2.0"
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
        ##### 请求体（Request Body） <span id="ho-dir-en-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedTravelId</strong> `string` <strong>（必选）</strong>

        要结束的 Directing 加密 Travel ID。由[客户端进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)接口返回。
      </td>
    </tr>

    <tr>
      <td>
        <strong>failCode</strong> `string` <strong>（可选）</strong>

        失败结束原因。省略时按正常结束处理。当前仅支持：

        - `TRAVEL_NO_STREAM_AUTO_END`：用于[进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)后超过 `noStreamAutoEndTimeoutSec` 仍未收到推流的场景，该结束方式不生成回放产物

        传入未支持的值返回 `400000`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>userAgent</strong> `string` <strong>（可选）</strong>

        SDK 或客户端版本标识。非空字符串，优先于 HTTP `User-Agent`。
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
        #### 响应参数 <span id="ho-dir-en-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="正常结束">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "status": "completed",
                    "endedAt": "2026-06-04T00:03:00Z",
                    "durationSec": 180
                }
            }
            ```
          </Tab>

          <Tab title="无推流超时结束">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "status": "failed",
                    "errorCode": "TRAVEL_NO_STREAM_AUTO_END",
                    "errorMessage": "Something went wrong.",
                    "endedAt": "2026-06-04T00:03:00Z",
                    "durationSec": null
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

          已结束的加密 Travel ID。

          <strong>status</strong> `string`

          结束状态：`completed`（正常结束）/ `failed`（带受支持 `failCode` 时的失败结束）。

          <strong>errorCode</strong> `string`

          失败原因代码；正常结束时为 `null`。

          <strong>errorMessage</strong> `string`

          失败说明；正常结束时为 `null`。

          <strong>endedAt</strong> `string`

          结束时间，ISO 8601 格式。

          <strong>durationSec</strong> `integer`

          有效视频时长秒数；无推流失败结束或时长尚不可解析时为 `null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-dir-en-notes-title" />

- 客户端应在进房后轮询[查询Travel状态](/zh/model-studio/happyoyster-directing-query-travel-status-api-reference)。如果在 `noStreamAutoEndTimeoutSec` 内 `rtcStatus` 未进入推流态，使用 `TRAVEL_NO_STREAM_AUTO_END` 结束。
- 传入 `TRAVEL_NO_STREAM_AUTO_END` 后 Travel 为 `failed`，不能查询到回放产物。
- 正常结束后，`durationSec` 优先使用最终视频文件时长；最终视频尚不可用时，使用当前最新可用生成结果的 `durationMs`。两者均不可用时为 `null`。
- 正常结束进入 `completed` 后，产物合成仍可能进行中；按[查询Travel产物](/zh/model-studio/happyoyster-directing-query-travel-artifacts-api-reference)的返回状态决定是否继续轮询。

## 错误码 <span id="ho-dir-en-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-en-next-title" />

Travel 进入 `completed` 后：

- [查询Travel产物](/zh/model-studio/happyoyster-directing-query-travel-artifacts-api-reference)：轮询获取录制原片和合成变体。
- [查询Travel列表](/zh/model-studio/happyoyster-directing-query-travel-list-api-reference)：查看历史 Travel。
