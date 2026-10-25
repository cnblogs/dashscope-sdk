> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Adventure-查询Travel列表 API参考

> 分页查询当前主账号名下的 Adventure Travel，可按状态或关联 World 筛选。

<meta property="og:title" content="HappyOyster-Adventure-查询Travel列表 API参考" />

## 适用范围 <span id="ho-adv-qtl-scope-title" />

分页查询当前主账号名下的 Adventure Travel，可按状态或关联 World 筛选。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>调用方</strong>：您的服务端调用。

## HTTP调用 <span id="ho-adv-qtl-http-title" />

<Tabs>
  <Tab title="华北2（北京）">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels`

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
        #### 请求参数 <span id="ho-adv-qtl-req-title" />
      </td>

      <td rowSpan={7}>
        <Tabs>
          <Tab title="查询Travel列表">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/travels?page=1&pageSize=20&status=completed&encryptedWorldId={encryptedWorldId}' \
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
        ##### Query 参数 <span id="ho-adv-qtl-query-h5" />
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

        按 Travel 状态筛选。未知值不筛选。可选值：

        - `init`：正在初始化会话资源
        - `pending`：排队或等待服务资源
        - `running`：正在运行
        - `failed`：Travel 失败；原因见 `errorCode` / `errorMessage`
        - `completed`：Travel 已结束，可查询产物
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedWorldId</strong> `string` <strong>（可选）</strong>

        当前主账号名下的 Adventure World ID。传入时仅返回该 World 的 Travel。本接口不支持 `mode` Query 参数，模型专属入口会自动限定为 Adventure。
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
        #### 响应参数 <span id="ho-adv-qtl-resp-header-h4" />
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
                            "encryptedTravelId": "trvl_a1b2****",
                            "status": "completed",
                            "mode": 1,
                            "encryptedWorldId": "enc_a1b2****",
                            "durationSec": 90,
                            "createdAt": "2026-06-03T10:00:00Z",
                            "endedAt": "2026-06-03T10:01:30Z"
                        },
                        {
                            "encryptedTravelId": "trvl_g7h8****",
                            "status": "running",
                            "mode": 1,
                            "encryptedWorldId": "enc_a1b2****",
                            "durationSec": null,
                            "createdAt": "2026-06-03T11:00:00Z",
                            "endedAt": null
                        },
                        {
                            "encryptedTravelId": "trvl_m3n4****",
                            "status": "failed",
                            "mode": 1,
                            "encryptedWorldId": "enc_a1b2****",
                            "durationSec": null,
                            "createdAt": "2026-06-03T12:00:00Z",
                            "endedAt": "2026-06-03T12:00:05Z",
                            "errorCode": "TRAVEL_SESSION_INIT_FAILED",
                            "errorMessage": "Failed to allocate inference resources."
                        }
                    ],
                    "pagination": {
                        "page": 1,
                        "pageSize": 20,
                        "total": 3,
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

          当前页 Travel 列表；无结果时为空数组。每项含 `encryptedTravelId`、`status`、`mode`（恒为 `1`）、`encryptedWorldId`、`durationSec`（进行中、失败或时长尚不可用时为 `null`）、`createdAt`、`endedAt`（进行中时为 `null`）；`status=failed` 的条目额外携带 `errorCode`（结构化失败原因，取值见[错误码](/zh/model-studio/happyoyster-error-code#ho-ec-travel-errorcode-title)）与 `errorMessage`（英文说明）；其他状态不含这两个字段。

          <strong>pagination</strong> `object`

          分页信息。含 `page`、`pageSize`、`total`、`hasMore`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-adv-qtl-notes-title" />

- 接口自动只返回 Adventure Travel，`items[].mode` 恒为 `1`。
- 使用 `encryptedWorldId` 筛选时，该 World 必须属于当前主账号和 Adventure 模型；其它模型的 World 返回 `403001`。
- 进行中 Travel 的 `durationSec` 和 `endedAt` 通常为 `null`。
- `failed` 条目额外携带 `errorCode` / `errorMessage`；其他状态不含这两个字段。在历史列表中展示失败原因时按 `errorCode` 分支处理。
- 未知 `status` 不会报错，而是按不筛选状态处理。
- 空结果不是错误，返回 `items=[]` 和对应分页信息。

## 错误码 <span id="ho-adv-qtl-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-adv-qtl-next-title" />

- [查询Travel状态](/zh/model-studio/happyoyster-adventure-query-travel-status-api-reference)：查询单个 Travel 的实时状态。
- [查询Travel产物](/zh/model-studio/happyoyster-adventure-query-travel-artifacts-api-reference)：查询已完成 Travel 的视频产物。
