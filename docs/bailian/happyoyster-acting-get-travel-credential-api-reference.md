> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Acting-获取体验凭证 API参考

> 三方服务端使用主 API Key 换取短时效、一次性的 ticket，再将其安全下发给客户端。此接口只校验 World 归属、状态与规格，不创建 Travel。

<meta property="og:title" content="HappyOyster-Acting-获取体验凭证 API参考" />

## 适用范围 <span id="ho-act-gtc-scope-title" />

三方服务端使用主 API Key 换取短时效、一次性的 `ticket`，再将其安全下发给客户端。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>前置条件</strong>：World 必须属于当前主账号和 Acting 模型，且状态为 `ready`。可通过[查询World构建状态](/zh/model-studio/happyoyster-acting-query-world-build-status-api-reference)接口确认。
- <strong>调用方</strong>：您的<strong>服务端</strong>调用，换得的 `ticket` 应只下发给准备立即进房的客户端。

## HTTP调用 <span id="ho-act-gtc-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds/get-travel-credential`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds/get-travel-credential`

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
        #### 请求参数 <span id="ho-act-gtc-req-title" />
      </td>

      <td rowSpan={5}>
        <Tabs>
          <Tab title="获取体验凭证">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds/get-travel-credential' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "encryptedWorldId": "{encryptedWorldId}"
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

        API Key 鉴权。仅支持<strong>主 API Key</strong>，以 `sk-` 开头，如 `sk-xxx`。通常配置为环境变量 `$DASHSCOPE_API_KEY`。临时 API Key（`st-` 开头）调用返回 `403003`。
      </td>
    </tr>

    <tr>
      <td>
        ##### 请求体（Request Body） <span id="ho-act-gtc-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedWorldId</strong> `string` <strong>（必选）</strong>

        当前主账号名下、状态为 `ready` 的 Acting World ID。由[创建World](/zh/model-studio/happyoyster-acting-create-world-api-reference)接口返回。
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
        #### 响应参数 <span id="ho-act-gtc-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="换取成功">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "ticket": "tk_a1b2****",
                    "expiresIn": 1800,
                    "encryptedWorldId": "enc_a1b2****"
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
          <strong>ticket</strong> `string`

          前缀为 `tk_` 的一次性 Travel 凭证。有效期 1800 秒（30 分钟），且只能成功使用一次。

          <strong>expiresIn</strong> `integer`

          有效期秒数，固定为 `1800`，即 30 分钟。

          <strong>encryptedWorldId</strong> `string`

          凭证对应的加密 World ID。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-act-gtc-notes-title" />

- World 必须属于当前主账号和 Acting 模型，且状态为 `ready`。
- `ticket` 有效期为 1800 秒且只能成功使用一次；过期或已使用后须重新换取。
- `ticket` 应只下发给准备立即进房的客户端，不应持久化为长期访问凭证。
- 调用本接口不会创建 Travel；Travel 在[进入房间](/zh/model-studio/happyoyster-acting-enter-travel-api-reference)成功时创建。
- Acting 功能闸主要在创建和进房时生效；换凭证仍会校验 World 对应规格，规格未开通时返回 `403007`。

## 错误码 <span id="ho-act-gtc-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-act-gtc-next-title" />

换取 `ticket` 后：

- [客户端进入房间](/zh/model-studio/happyoyster-acting-enter-travel-api-reference)：客户端使用 `ticket` 创建 Travel 并获取 RTC 配置。
