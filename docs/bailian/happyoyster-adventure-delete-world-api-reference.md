> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Adventure-删除World API参考

> 删除当前主账号名下的 Adventure World。同账号下 World 已不存在时幂等返回 deleted=false。

<meta property="og:title" content="HappyOyster-Adventure-删除World API参考" />

## 适用范围 <span id="ho-adv-dw-scope-title" />

删除当前主账号名下的 Adventure World。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>前置条件</strong>：使用[创建World](/zh/model-studio/happyoyster-adventure-create-world-api-reference)接口返回的 `encryptedWorldId` 删除。
- <strong>调用方</strong>：您的服务端调用。本接口不接受请求体字段 `userAgent`。

## HTTP调用 <span id="ho-adv-dw-http-title" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/delete`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/delete`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/delete`

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
        #### 请求参数 <span id="ho-adv-dw-req-title" />
      </td>

      <td rowSpan={5}>
        <Tabs>
          <Tab title="删除World">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/delete' \
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
        ##### 请求体（Request Body） <span id="ho-adv-dw-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedWorldId</strong> `string` <strong>（必选）</strong>

        要删除的 Adventure 加密 World ID。由[创建World](/zh/model-studio/happyoyster-adventure-create-world-api-reference)接口返回。
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
        #### 响应参数 <span id="ho-adv-dw-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="删除成功">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedWorldId": "enc_a1b2****",
                    "deleted": true
                }
            }
            ```
          </Tab>

          <Tab title="World 已不存在">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedWorldId": "enc_a1b2****",
                    "deleted": false
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
          <strong>encryptedWorldId</strong> `string`

          请求中的加密 World ID。

          <strong>deleted</strong> `boolean`

          `true` 表示已删除；`false` 表示本次未删除到 World。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-adv-dw-notes-title" />

- 同账号下 World 已不存在时返回 `code=0` 且 `deleted=false`，不返回 `403001`。
- ID 可解析但跨账号、workspace 不匹配或资源不是 Adventure World 时，返回 `403001`。
- 删除后继续调用详情、构建状态或换凭证会按资源不存在处理。

## 错误码 <span id="ho-adv-dw-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-adv-dw-next-title" />

- [查询World列表](/zh/model-studio/happyoyster-adventure-query-world-list-api-reference)：查看剩余 Adventure World。
- [创建World](/zh/model-studio/happyoyster-adventure-create-world-api-reference)：创建新的 Adventure World。
