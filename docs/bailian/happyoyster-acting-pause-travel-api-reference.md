> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Acting-暂停Travel API参考

> 请求暂停正在运行的 Acting Travel。暂停后视频生成停止，可通过恢复Travel接口继续。

<meta property="og:title" content="HappyOyster-Acting-暂停Travel API参考" />

## 适用范围 <span id="ho-act-pa-scope-title" />

请求暂停正在运行的 Acting Travel。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，主 API Key 或临时 API Key 均可调用。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：Travel 状态需为 `running`。可通过[查询Travel状态](/zh/model-studio/happyoyster-acting-query-travel-status-api-reference)接口确认。
- <strong>调用方</strong>：您的服务端或客户端均可调用。

## HTTP调用 <span id="ho-act-pa-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/pause`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/pause`

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
        #### 请求参数 <span id="ho-act-pa-req-title" />
      </td>

      <td rowSpan={6}>
        <Tabs>
          <Tab title="暂停Travel">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/pause' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "encryptedTravelId": "{encryptedTravelId}",
                "userAgent": "HappyOyster-Web/1.2.0"
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
        ##### 请求体（Request Body） <span id="ho-act-pa-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedTravelId</strong> `string` <strong>（必选）</strong>

        要暂停的 Acting 加密 Travel ID。由[客户端进入房间](/zh/model-studio/happyoyster-acting-enter-travel-api-reference)接口返回。
      </td>
    </tr>

    <tr>
      <td>
        <strong>userAgent</strong> `string` <strong>（可选）</strong>

        SDK 或客户端版本标识。非空字符串，优先于 HTTP `User-Agent`，用于 SDK 版本和平台统计。
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
        #### 响应参数 <span id="ho-act-pa-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="暂停成功">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "status": "paused"
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

          暂停目标状态，返回 `paused`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-act-pa-notes-title" />

- Travel 当前状态必须为 `running`。
- 进房响应中的 `version` 必须为 `actingV2`；Acting 进房固定返回该值。
- 暂停确认是异步过程。收到成功响应后，如果需要立即恢复或发送依赖暂停态的操作，应继续轮询[查询Travel状态](/zh/model-studio/happyoyster-acting-query-travel-status-api-reference)，直到查询状态为 `paused`。
- 暂停确认通常存在约 3 秒的状态屏障，实际状态以查询结果为准。
- 暂停不改变 `aspectRatio`，客户端继续保持进房时确定的播放器方向。

## 错误码 <span id="ho-act-pa-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-act-pa-next-title" />

暂停后可进行以下操作：

- [恢复Travel](/zh/model-studio/happyoyster-acting-resume-travel-api-reference)：恢复已暂停的 Travel 继续生成。
- [发送文本过程指令](/zh/model-studio/happyoyster-acting-instruct-travel-api-reference)：在 `paused` 状态发送指令只会接受指令，不会自动恢复。
- [结束Travel](/zh/model-studio/happyoyster-acting-end-travel-api-reference)：直接结束 Travel。
