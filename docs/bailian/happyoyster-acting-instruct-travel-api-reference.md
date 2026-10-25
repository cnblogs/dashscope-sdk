> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Acting-发送文本过程指令 API参考

> 向 Acting Travel 发送一条文本指令，驱动后续角色表演和画面生成。Travel 为 running 或 paused 时可调用。

<meta property="og:title" content="HappyOyster-Acting-发送文本过程指令 API参考" />

## 适用范围 <span id="ho-act-in-scope-title" />

向 Acting Travel 发送一条文本指令，驱动后续角色表演和画面生成。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，主 API Key 或临时 API Key 均可调用。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：Travel 状态需为 `running` 或 `paused`。可通过[查询Travel状态](/zh/model-studio/happyoyster-acting-query-travel-status-api-reference)接口确认。
- <strong>调用方</strong>：您的服务端或客户端均可调用。

## HTTP调用 <span id="ho-act-in-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/instruct`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/instruct`

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
        #### 请求参数 <span id="ho-act-in-req-title" />
      </td>

      <td rowSpan={8}>
        <Tabs>
          <Tab title="发送文本过程指令">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/travels/instruct' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "encryptedTravelId": "{encryptedTravelId}",
                "content": "微笑着问候，并询问我今天过得怎么样",
                "clientRequestId": "instruction_greeting_001",
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
        ##### 请求体（Request Body） <span id="ho-act-in-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedTravelId</strong> `string` <strong>（必选）</strong>

        要控制的 Acting 加密 Travel ID。由[客户端进入房间](/zh/model-studio/happyoyster-acting-enter-travel-api-reference)接口返回。
      </td>
    </tr>

    <tr>
      <td>
        <strong>content</strong> `string` <strong>（必选）</strong>

        文本过程指令。非空，最长 2000 字符；缺失、为空或超限返回 `400000`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>clientRequestId</strong> `string` <strong>（可选）</strong>

        指令重试的计费幂等键。同一条逻辑指令重试时复用同值，避免重复计费。推荐格式 `[A-Za-z0-9_-]{1,32}`。

        - 缺省或格式不符合建议时不会返回参数错误：服务端会回退使用网关 `request_id`，仍不可用时使用随机计费幂等键
        - 该字段只用于计费去重，不保证指令处理本身只执行一次
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
        #### 响应参数 <span id="ho-act-in-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="指令已接受">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "content": "微笑着问候，并询问我今天过得怎么样",
                    "accepted": true
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

          <strong>content</strong> `string`

          已接受的指令文本。

          <strong>accepted</strong> `boolean`

          是否已接受指令并进入处理流程。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-act-in-notes-title" />

- Travel 状态必须为 `running` 或 `paused`。
- `content` 必填、不可为空或只有空白字符，最长 2000 字符。
- 在 `paused` 状态发送指令只会接受指令，**不会自动恢复** Travel；需要继续运行时，客户端必须另行调用[恢复Travel](/zh/model-studio/happyoyster-acting-resume-travel-api-reference)。
- 同一条逻辑指令重试时应复用 `clientRequestId`，以避免重复计费。
- 已结束或失败的 Travel 不可写。
- 指令未通过内容安全策略时返回 `403004`。
- Acting 的 `creationModel` 恒为 `simple`，不需要判断 ScriptList 子模式。

## 错误码 <span id="ho-act-in-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-act-in-next-title" />

发送指令后：

- [查询Travel状态](/zh/model-studio/happyoyster-acting-query-travel-status-api-reference)：查看指令执行状态和章节信息。
- [暂停Travel](/zh/model-studio/happyoyster-acting-pause-travel-api-reference) / [恢复Travel](/zh/model-studio/happyoyster-acting-resume-travel-api-reference)：控制运行节奏。
- [结束Travel](/zh/model-studio/happyoyster-acting-end-travel-api-reference)：结束会话并处理产物。
