> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Acting-查询World列表 API参考

> 分页查询当前主账号名下的 Acting World。接口自动按模型隔离，不会返回 Directing 或 Adventure World。

<meta property="og:title" content="HappyOyster-Acting-查询World列表 API参考" />

## 适用范围 <span id="ho-act-qwl-scope-title" />

分页查询当前主账号名下的 Acting World。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>调用方</strong>：您的服务端调用。

## HTTP调用 <span id="ho-act-qwl-http-title" />

<Tabs>
  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds`

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
        #### 请求参数 <span id="ho-act-qwl-req-title" />
      </td>

      <td rowSpan={7}>
        <Tabs>
          <Tab title="查询World列表">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds?page=1&pageSize=20&status=ready' \
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
        ##### Query 参数 <span id="ho-act-qwl-query-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>page</strong> `integer` <strong>（可选）</strong>

        页码，默认 `1`。`page <= 0` 时按 `1` 处理。
      </td>
    </tr>

    <tr>
      <td>
        <strong>pageSize</strong> `integer` <strong>（可选）</strong>

        每页条数，默认 `20`。`pageSize <= 0` 时按 `20`，大于 `100` 时按 `100` 处理。
      </td>
    </tr>

    <tr>
      <td>
        <strong>status</strong> `string` <strong>（可选）</strong>

        按构建状态筛选。未知值不筛选。可选值：

        - `generating`：构建中
        - `ready`：就绪
        - `failed`：构建失败
      </td>
    </tr>

    <tr>
      <td>
        <strong>mode</strong> `integer` <strong>（可选）</strong>

        无需传入。列表已按 Acting 模型过滤，`items[].mode` 恒为 `3`。若仍传入且非 `3` 返回 `400000`。
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
        #### 响应参数 <span id="ho-act-qwl-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        ```json 查询成功
        {
            "code": 0,
            "message": null,
            "data": {
                "items": [
                    {
                        "encryptedWorldId": "enc_a1b2****",
                        "name": "深夜视频通话",
                        "status": "ready",
                        "mode": 3,
                        "previewUrl": null,
                        "createdAt": "2026-09-09T08:00:00Z",
                        "perspective": null,
                        "creationModel": "simple",
                        "uploadMode": "first_frame"
                    },
                    {
                        "encryptedWorldId": "enc_g7h8****",
                        "name": "客厅轻松对谈",
                        "status": "ready",
                        "mode": 3,
                        "previewUrl": null,
                        "createdAt": "2026-09-09T09:00:00Z",
                        "perspective": null,
                        "creationModel": "simple",
                        "uploadMode": "first_frame"
                    }
                ],
                "pagination": {
                    "page": 1,
                    "pageSize": 20,
                    "total": 2,
                    "hasMore": false
                }
            }
        }
        ```
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
          <strong>items</strong> `array`

          当前页 World 列表；无结果时为空数组。每项含 `encryptedWorldId`、`name`、`status`（`generating` / `ready` / `failed`）、`mode`（恒为 `3`）、`previewUrl`（通常为 `null`）、`createdAt`、`perspective`（通常为 `null`）、`creationModel`（恒为 `simple`）、`uploadMode`（固定为 `first_frame`）。

          <strong>pagination</strong> `object`

          分页信息。含 `page`（当前页码）、`pageSize`（实际每页条数）、`total`（总记录数）、`hasMore`（`page × pageSize < total` 时为 `true`）。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-act-qwl-notes-title" />

- 模型过滤自动生效，列表不会混入其它模型的 World，且 `items[].mode` 恒为 `3`。
- 列表项是轻量快照，不返回图片、Prompt、`resolution`、`aspectRatio` 或 `refWorldId`；需要完整配置时调用[查询World详情](/zh/model-studio/happyoyster-acting-query-world-detail-api-reference)。
- 翻页时应使用响应中的 `pagination.pageSize` 和 `hasMore`，不要假设请求值一定原样采用。
- 空结果不是错误，返回 `items=[]` 和对应分页信息。

## 错误码 <span id="ho-act-qwl-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-act-qwl-next-title" />

- [查询World详情](/zh/model-studio/happyoyster-acting-query-world-detail-api-reference)：查看单个 World 完整元数据。
- [删除World](/zh/model-studio/happyoyster-acting-delete-world-api-reference)：删除不再使用的 World。
