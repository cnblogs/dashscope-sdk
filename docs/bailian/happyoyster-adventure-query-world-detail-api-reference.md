> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Adventure-查询World详情 API参考

> 查询单个 Adventure World 的完整元数据和可回显创建参数。构建进度轮询请使用查询World构建状态接口。

<meta property="og:title" content="HappyOyster-Adventure-查询World详情 API参考" />

## 适用范围 <span id="ho-adv-qwd-scope-title" />

查询单个 Adventure World 的完整元数据和可回显创建参数。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>前置条件</strong>：使用[创建World](/zh/model-studio/happyoyster-adventure-create-world-api-reference)接口返回的 `encryptedWorldId` 查询。构建进度轮询应使用[查询World构建状态](/zh/model-studio/happyoyster-adventure-query-world-build-status-api-reference)。
- <strong>调用方</strong>：您的服务端调用。

## HTTP调用 <span id="ho-adv-qwd-http-title" />

<Tabs>
  <Tab title="华北2（北京）">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/detail`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/detail`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/detail`

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
        #### 请求参数 <span id="ho-adv-qwd-req-title" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="查询World详情">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds/detail?encryptedWorldId={encryptedWorldId}' \
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
        ##### Query 参数 <span id="ho-adv-qwd-query-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedWorldId</strong> `string` <strong>（必选）</strong>

        当前主账号名下的 Adventure 加密 World ID。由[创建World](/zh/model-studio/happyoyster-adventure-create-world-api-reference)接口返回。
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
        #### 响应参数 <span id="ho-adv-qwd-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="查询成功">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedWorldId": "enc_a1b2****",
                    "name": "江南雨夜奇遇",
                    "status": "ready",
                    "mode": 1,
                    "creationModel": "simple",
                    "prompt": "黄昏的江南水乡，石桥与乌篷船",
                    "eventStyle": null,
                    "perspective": "third_person",
                    "uploadMode": "first_frame",
                    "resolution": null,
                    "aspectRatio": null,
                    "layout": null,
                    "narrative": null,
                    "refWorldId": null,
                    "firstFrameImage": {
                        "url": "https://cdn.happyoyster.com/frames/jiangnan.jpg",
                        "referenceType": "default"
                    },
                    "inputImages": null,
                    "sceneImage": null,
                    "roleImage": null,
                    "scenePrompt": null,
                    "rolePrompt": null,
                    "scriptList": null,
                    "previewUrl": null,
                    "createdAt": "2026-06-03T10:00:00Z",
                    "updatedAt": "2026-06-03T10:05:00Z"
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

          <strong>name</strong> `string`

          World 名称。

          <strong>status</strong> `string`

          构建状态：`generating` / `ready` / `failed`。

          <strong>mode</strong> `integer`

          Adventure 恒为 `1`。

          <strong>creationModel</strong> `string`

          Adventure 恒为 `simple`。

          <strong>prompt</strong> `string`

          创建时提交的顶层 Prompt。

          <strong>eventStyle</strong> `null`

          Adventure 不使用该创建参数，通常为 `null`。该字段仅 Directing `creationModel=simple` 生效。

          <strong>perspective</strong> `string`

          视角：`first_person` / `third_person`。

          <strong>uploadMode</strong> `string`

          固定为 `first_frame`。

          <strong>resolution</strong> `null`

          Adventure 不使用该字段，通常为 `null`。

          <strong>aspectRatio</strong> `null`

          Adventure 不使用该字段，固定为 `null`。

          <strong>layout</strong> `null`

          Adventure 不使用该字段，通常为 `null`。

          <strong>narrative</strong> `null`

          Adventure 不使用该字段，通常为 `null`。

          <strong>refWorldId</strong> `string`

          衍生创建时使用的加密参考 World ID；无则为 `null`。

          <strong>firstFrameImage</strong> `object`

          创建时的必填首帧图片；只返回 URL 形式。含 `url` 和 `referenceType`。

          <strong>inputImages</strong> `null`

          Adventure 当前不使用该字段。

          <strong>scriptList</strong> `null`

          Adventure 不支持 ScriptList，固定为 `null`。

          <strong>previewUrl</strong> `null`

          当前固定为 `null`。

          <strong>createdAt</strong> `string`

          创建时间，ISO 8601 格式。

          <strong>updatedAt</strong> `string`

          最近更新时间，ISO 8601 格式。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-adv-qwd-notes-title" />

- 本接口可查询 `generating`、`ready` 或 `failed` World；部分构建产物在未就绪时可能为 `null`。
- 详情接口不回显 `async`，因为它是调用行为开关，不是 World 属性。
- 图片只返回 URL 和 `referenceType`，不会回传创建请求中的 base64 内容。
- 查询其它模型、其它主账号或已删除的 World 均返回 `403001`，不会泄漏资源是否存在。

## 错误码 <span id="ho-adv-qwd-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-adv-qwd-next-title" />

- [查询World列表](/zh/model-studio/happyoyster-adventure-query-world-list-api-reference)：分页查询所有 Adventure World。
- [删除World](/zh/model-studio/happyoyster-adventure-delete-world-api-reference)：删除不再使用的 World。
- World 为 `ready` 时可[获取体验凭证](/zh/model-studio/happyoyster-adventure-get-travel-credential-api-reference)。
