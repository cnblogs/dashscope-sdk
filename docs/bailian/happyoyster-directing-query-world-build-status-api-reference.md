> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-查询World构建状态 API参考

> 查询 Directing World 的构建进度。接口返回加密 World ID、构建状态和首帧 URL，客户端轮询直至 World 进入 ready。

<meta property="og:title" content="HappyOyster-Directing-查询World构建状态 API参考" />

## 适用范围 <span id="ho-dir-qwb-scope-title" />

查询 Directing World 的构建进度。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，主 API Key 或临时 API Key 均可调用。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：使用[创建World](/zh/model-studio/happyoyster-directing-create-world-api-reference)接口返回的 `encryptedWorldId` 查询。可从 `generating` 状态开始查询。
- <strong>调用方</strong>：您的服务端或客户端均可调用。

## HTTP调用 <span id="ho-dir-qwb-http-title" />

<Tabs>
  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds/build-status`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds/build-status`

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
        #### 请求参数 <span id="ho-dir-qwb-req-title" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="查询World构建状态">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds/build-status?encryptedWorldId={encryptedWorldId}' \
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
        ##### Query 参数 <span id="ho-dir-qwb-query-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedWorldId</strong> `string` <strong>（必选）</strong>

        [创建World](/zh/model-studio/happyoyster-directing-create-world-api-reference)接口返回的 Directing 加密 World ID。
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
        #### 响应参数 <span id="ho-dir-qwb-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="构建中">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedWorldId": "enc_a1b2****",
                    "status": "generating",
                    "firstFrame": null
                }
            }
            ```
          </Tab>

          <Tab title="构建完成">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedWorldId": "enc_a1b2****",
                    "status": "ready",
                    "firstFrame": "https://cdn.happyoyster.com/frames/world_xyz789.jpg",
                    "name": "午夜访客",
                    "mode": 2
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

          加密 World ID。

          <strong>status</strong> `string`

          构建状态：`generating` / `ready` / `failed`。

          <strong>firstFrame</strong> `string`

          World 首帧 URL；尚未生成或构建失败时为 `null`。

          <strong>name</strong> `string`

          World 名称。仅 `status=ready` 时返回。

          <strong>mode</strong> `integer`

          Directing 恒为 `2`。仅 `status=ready` 时返回。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-dir-qwb-notes-title" />

- 建议每 3–5 秒轮询，直到 `status` 为 `ready` 或 `failed`。
- 只有 `ready` 的 World 才能[换取体验凭证](/zh/model-studio/happyoyster-directing-get-travel-credential-api-reference)并[进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)。
- URL 图片在异步转存后校验失败时，World 可能从 `generating` 进入 `failed`。
- 本接口不返回 `aspectRatio`。

## 错误码 <span id="ho-dir-qwb-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-qwb-next-title" />

World 进入 `ready` 后：

- [获取体验凭证](/zh/model-studio/happyoyster-directing-get-travel-credential-api-reference)：换取一次性 `ticket`。
- [查询World详情](/zh/model-studio/happyoyster-directing-query-world-detail-api-reference)：查询完整创建元数据。
