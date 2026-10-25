> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-查询World列表 API参考

> 分页查询当前主账号名下的 Directing World。接口自动按模型隔离，不会返回 Acting 或 Adventure World。

<meta property="og:title" content="HappyOyster-Directing-查询World列表 API参考" />

## 适用范围 <span id="ho-dir-qwl-scope-title" />

分页查询当前主账号名下的 Directing World。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>调用方</strong>：您的服务端调用。

## HTTP调用 <span id="ho-dir-qwl-http-title" />

<Tabs>
  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds`

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
        #### 请求参数 <span id="ho-dir-qwl-req-title" />
      </td>

      <td rowSpan={7}>
        <Tabs>
          <Tab title="查询World列表">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds?page=1&pageSize=20&status=ready' \
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
        ##### Query 参数 <span id="ho-dir-qwl-query-h5" />
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

        无需传入。列表已按 Directing 模型过滤，`items[].mode` 恒为 `2`。若仍传入且非 `2` 返回 `400000`。
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
        #### 响应参数 <span id="ho-dir-qwl-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="查询成功">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "items": [
                        {
                            "encryptedWorldId": "enc_a1b2****",
                            "name": "午夜访客",
                            "status": "ready",
                            "mode": 2,
                            "perspective": null,
                            "creationModel": "scriptlist",
                            "uploadMode": null,
                            "createdAt": "2026-06-03T10:00:00Z"
                        },
                        {
                            "encryptedWorldId": "enc_g7h8****",
                            "name": "霓虹小巷",
                            "status": "ready",
                            "mode": 2,
                            "perspective": null,
                            "creationModel": "simple",
                            "uploadMode": null,
                            "createdAt": "2026-06-03T12:00:00Z"
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
          <strong>items</strong> `array`

          当前页 World 列表；无结果时为空数组。每项含 `encryptedWorldId`、`name`、`status`（`generating` / `ready` / `failed`）、`mode`（恒为 `2`）、`perspective`（通常为 `null`）、`creationModel`（`simple` / `scriptlist`）、`uploadMode`（通常为 `null`）、`createdAt`。

          <strong>pagination</strong> `object`

          分页信息。含 `page`、`pageSize`、`total`、`hasMore`（`page × pageSize < total` 时为 `true`）。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-dir-qwl-notes-title" />

- 列表项是轻量快照，不返回图片、`scriptList` 或 `aspectRatio`。需要完整元数据时，按 `encryptedWorldId` 调用[查询World详情](/zh/model-studio/happyoyster-directing-query-world-detail-api-reference)。
- 未知 `status` 不会报错，而是按不筛选状态处理。
- 翻页时应使用响应中的 `pagination.pageSize` 和 `hasMore`，不要假设请求值一定原样采用。

## 错误码 <span id="ho-dir-qwl-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-qwl-next-title" />

- [查询World详情](/zh/model-studio/happyoyster-directing-query-world-detail-api-reference)：查看单个 World 完整元数据。
- [删除World](/zh/model-studio/happyoyster-directing-delete-world-api-reference)：删除不再使用的 World。
