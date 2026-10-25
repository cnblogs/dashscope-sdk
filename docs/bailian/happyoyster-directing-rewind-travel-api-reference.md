> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-回溯Travel API参考

> 将 Travel 回溯到指定视频时间点，并从实际落点恢复运行。Travel 必须先进入 paused 状态。

<meta property="og:title" content="HappyOyster-Directing-回溯Travel API参考" />

## 适用范围 <span id="ho-dir-rw-scope-title" />

将 Travel 回溯到指定视频时间点，并从实际落点恢复运行。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，主 API Key 或临时 API Key 均可调用。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：Travel 必须先进入 `paused` 状态，不能在 `running` 时直接回溯。可通过[查询Travel状态](/zh/model-studio/happyoyster-directing-query-travel-status-api-reference)接口确认。
- <strong>调用方</strong>：您的服务端或客户端均可调用。

## HTTP调用 <span id="ho-dir-rw-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/rewind`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/rewind`

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
        #### 请求参数 <span id="ho-dir-rw-req-title" />
      </td>

      <td rowSpan={6}>
        <Tabs>
          <Tab title="回溯Travel">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/rewind' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "encryptedTravelId": "{encryptedTravelId}",
                "rewindToSec": 84.0,
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
        ##### 请求体（Request Body） <span id="ho-dir-rw-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedTravelId</strong> `string` <strong>（必选）</strong>

        要回溯的 Directing 加密 Travel ID。由[客户端进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)接口返回。
      </td>
    </tr>

    <tr>
      <td>
        <strong>rewindToSec</strong> `number` <strong>（必选）</strong>

        目标视频时间，单位秒。大于或等于 `0`，建议为 4 的倍数（如 4、8、12）。服务端按 `Math.round(rewindToSec / 4) × 4` 四舍五入到最近的 4 秒边界。实际落点以响应中的 `resumedAtSec` / `actualRewindToSec` 为准。

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
        #### 响应参数 <span id="ho-dir-rw-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="回溯成功">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "status": "running",
                    "resumedAtSec": 84.0,
                    "actualRewindToSec": 84.0
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

          加密的 Travel ID。

          <strong>status</strong> `string`

          回溯完成后的状态，返回 `running`。

          <strong>resumedAtSec</strong> `number`

          实际恢复播放的秒数，可能与请求值略有偏差。

          <strong>actualRewindToSec</strong> `number`

          实际回溯落点，与 `resumedAtSec` 相同。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-dir-rw-notes-title" />

- Travel 必须先进入 `paused`；不能在 `running` 时直接回溯。
- [暂停Travel](/zh/model-studio/happyoyster-directing-pause-travel-api-reference)存在约 3 秒的异步确认屏障。收到 pause 响应后，仍应轮询[查询Travel状态](/zh/model-studio/happyoyster-directing-query-travel-status-api-reference)，确认对外状态已经是 `paused` 再调用本接口。
- 剧本按 4 秒 Block 组织，`rewindToSec` 建议使用 4、8、12 等 4 的倍数（如 `6 → 8`）。
- 实际落点以 `resumedAtSec` / `actualRewindToSec` 为准。

## 错误码 <span id="ho-dir-rw-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-rw-next-title" />

回溯后 Travel 回到 `running`：

- [查询Travel状态](/zh/model-studio/happyoyster-directing-query-travel-status-api-reference)：确认运行状态。
- 普通模式：[发送过程指令](/zh/model-studio/happyoyster-directing-instruct-travel-api-reference)。
- [暂停Travel](/zh/model-studio/happyoyster-directing-pause-travel-api-reference)：再次暂停以进行下一次回溯。
- [结束Travel](/zh/model-studio/happyoyster-directing-end-travel-api-reference)：结束会话并处理产物。
