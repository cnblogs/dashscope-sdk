> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-剧本全量更新 API参考

> 在剧本模式 Travel 中提交完整的 45 个 turn，全量替换当前 Travel 的 acts。仅 creationModel=scriptlist 支持。

<meta property="og:title" content="HappyOyster-Directing-剧本全量更新 API参考" />

## 适用范围 <span id="ho-dir-us-scope-title" />

在剧本模式（`creationModel=scriptlist`）Travel 中提交完整的 45 个 turn，全量替换当前 Travel 的 `acts`。调用前请确认以下事项：

- <strong>鉴权要求</strong>：<strong>不强制主 API Key</strong>，主 API Key 或临时 API Key 均可调用。获取方式请参见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。
- <strong>前置条件</strong>：Travel 的 `creationModel` 必须为 `scriptlist`，状态需为 `running` 或 `pending`。可通过[查询Travel状态](/zh/model-studio/happyoyster-directing-query-travel-status-api-reference)接口确认。
- <strong>调用方</strong>：您的服务端或客户端均可调用。

## HTTP调用 <span id="ho-dir-us-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/update-script`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/update-script`

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
        #### 请求参数 <span id="ho-dir-us-req-title" />
      </td>

      <td rowSpan={8}>
        <Tabs>
          <Tab title="剧本全量更新">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/travels/update-script' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "encryptedTravelId": "{encryptedTravelId}",
                "scriptList": {
                    "acts": [
                        {
                            "turn": 1,
                            "content": "[character_1] wakes up and looks toward the door.",
                            "cameraType": "Static",
                            "shotSize": "Medium",
                            "cut": "long-take"
                        },
                        {
                            "turn": 2,
                            "content": "[character_1] walks slowly across the dark room.",
                            "cameraType": "Tracking",
                            "shotSize": "Wide",
                            "cut": "hard-cut"
                        }
                    ]
                },
                "userAgent": "your-client/1.2.0"
            }'
            ```
          </Tab>
        </Tabs>

        > 示例中仅展示第 1、2 个 act 用于说明结构，**不可直接提交**。实际请求必须包含 turn 1–45 共 45 条，连续且不重复。
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
        ##### 请求体（Request Body） <span id="ho-dir-us-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedTravelId</strong> `string` <strong>（必选）</strong>

        要更新的 Directing 加密 Travel ID，`creationModel` 必须为 `scriptlist`。由[客户端进入房间](/zh/model-studio/happyoyster-directing-enter-travel-api-reference)接口返回。
      </td>
    </tr>

    <tr>
      <td>
        <strong>scriptList</strong> `object` <strong>（必选）</strong>

        全量剧本容器。仅 `acts` 生效；即使包含 `subjects`、`synopsis`、`scene`、`style`、`speed`、`language`、`setting`、`soundtrack`、`prologue` 或 `videoTags`，这些字段也会被忽略，平台继续使用创建 World 时已保存的值。

        <strong>scriptList.acts</strong> `array` <strong>（必选）</strong>

        完整 act 列表。约束：

        - 必须恰好 45 条
        - `turn` 必须覆盖 1–45，连续且不可重复
        - `content` 非空，单拍最长 2000 字；全部 `content` 合计最长 100000 字
        - `cameraType`（可选，默认 `Static`）、`shotSize`（可选 `Wide` / `Medium` / `Close-up`，默认 `Medium`）、`cut`（可选，默认 `long-take`）

        本接口是全量替换，不支持只提交发生变化的 turn。
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
        #### 响应参数 <span id="ho-dir-us-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="更新已接受">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedTravelId": "trvl_a1b2****",
                    "accepted": true,
                    "turnCount": 45
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

          <strong>accepted</strong> `boolean`

          是否已接受完整剧本并进入处理流程。

          <strong>turnCount</strong> `integer`

          本次全量更新的 turn 数，固定为 `45`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-dir-us-notes-title" />

- 仅 `creationModel=scriptlist` 支持本接口；普通模式 Travel 误调用返回 `409000`。
- Travel 状态必须为 `running` 或 `pending`。
- `acts` 必须恰好 45 条；turn 必须为 1–45，连续且不可重复，否则返回 `400000`。
- 剧本内容未通过内容安全策略时返回 `403004`；触发版权或 IP 合规拒绝时返回 `403005`。

## 错误码 <span id="ho-dir-us-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-us-next-title" />

更新后：

- [查询Travel状态](/zh/model-studio/happyoyster-directing-query-travel-status-api-reference)：查看章节信息。
- [暂停Travel](/zh/model-studio/happyoyster-directing-pause-travel-api-reference) / [回溯Travel](/zh/model-studio/happyoyster-directing-rewind-travel-api-reference)。
- [结束Travel](/zh/model-studio/happyoyster-directing-end-travel-api-reference)：结束会话并处理产物。
