> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Directing-查询World详情 API参考

> 查询单个 Directing World 的当前状态、创建参数和可返回的 ScriptList 信息。构建进度轮询请使用查询World构建状态接口。

<meta property="og:title" content="HappyOyster-Directing-查询World详情 API参考" />

## 适用范围 <span id="ho-dir-qwd-scope-title" />

查询单个 Directing World 的当前状态、创建参数和可返回的 ScriptList 信息。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>前置条件</strong>：使用[创建World](/zh/model-studio/happyoyster-directing-create-world-api-reference)接口返回的 `encryptedWorldId` 查询。构建进度轮询应使用[查询World构建状态](/zh/model-studio/happyoyster-directing-query-world-build-status-api-reference)。
- <strong>调用方</strong>：您的服务端调用。

## HTTP调用 <span id="ho-dir-qwd-http-title" />

<Tabs>
  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds/detail`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds/detail`

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
        #### 请求参数 <span id="ho-dir-qwd-req-title" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="查询World详情">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-directing/openapi/v1/worlds/detail?encryptedWorldId={encryptedWorldId}' \
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
        ##### Query 参数 <span id="ho-dir-qwd-query-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>encryptedWorldId</strong> `string` <strong>（必选）</strong>

        当前主账号名下的 Directing 加密 World ID。由[创建World](/zh/model-studio/happyoyster-directing-create-world-api-reference)接口返回。
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
        #### 响应参数 <span id="ho-dir-qwd-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="查询成功（剧本模式）">
            ```json
            {
                "code": 0,
                "message": null,
                "data": {
                    "encryptedWorldId": "enc_a1b2****",
                    "name": "午夜访客",
                    "status": "ready",
                    "mode": 2,
                    "creationModel": "scriptlist",
                    "prompt": null,
                    "eventStyle": "normal",
                    "perspective": null,
                    "uploadMode": null,
                    "resolution": "720p",
                    "layout": null,
                    "narrative": null,
                    "refWorldId": null,
                    "firstFrameImage": {
                        "url": "https://cdn.happyoyster.com/frames/room.png",
                        "referenceType": "default"
                    },
                    "inputImages": null,
                    "scriptList": {
                        "synopsis": "深夜，苏黎被敲门声惊醒。",
                        "videoTitle": "午夜访客",
                        "subjects": [
                            {
                                "label": "[character_1]",
                                "name": "苏黎",
                                "type": "character",
                                "refImage": {
                                    "url": "https://cdn.happyoyster.com/subjects/suli.png",
                                    "referenceType": "default"
                                }
                            }
                        ],
                        "acts": [
                            {
                                "turn": 1,
                                "content": "雨水拍打窗户，[character_1] 从睡梦中惊醒。",
                                "cameraType": "Static",
                                "shotSize": "Wide",
                                "cut": "long-take"
                            },
                            {
                                "turn": 2,
                                "content": "[character_1] 走向房门，门外再次响起敲门声。",
                                "cameraType": "Push-in",
                                "shotSize": "Close-up",
                                "cut": "long-take"
                            }
                        ]
                    },
                    "previewUrl": null,
                    "aspectRatio": null,
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

          Directing 恒为 `2`。

          <strong>creationModel</strong> `string`

          `simple` / `scriptlist`；决定 Travel 可调用的控制接口。

          <strong>prompt</strong> `string`

          `simple` 模式创建时的 Prompt；`scriptlist` 固定为 `null`。

          <strong>eventStyle</strong> `string`

          创建时的事件风格：`normal` / `dramatic` / `regular`。

          <strong>perspective</strong> `null`

          Directing 模型通常为 `null`。

          <strong>uploadMode</strong> `null`

          Directing World 通常为 `null`。

          <strong>resolution</strong> `string`

          `480p` / `720p`。

          <strong>layout</strong> `string`

          `simple` 模式的镜头运动风格；未传或 `scriptlist` 时通常为 `null`。

          <strong>narrative</strong> `string`

          `simple` 模式的叙事风格；未传或 `scriptlist` 时通常为 `null`。

          <strong>refWorldId</strong> `string`

          衍生创建时使用的加密参考 World ID。

          <strong>firstFrameImage</strong> `object`

          创建时的首帧图；响应不返回 base64。

          <strong>inputImages</strong> `array`

          `simple` 模式创建时持久化的参考图；`scriptlist` 不接受该字段，通常为 `null`。

          <strong>scriptList</strong> `object`

          仅 `scriptlist` World 有值；结构化剧本反解失败时为 `null`。含 `synopsis`、`videoTitle`、`scene`、`style`、`speed`、`language`、`setting`、`soundtrack`、`prologue`、`videoTags`、`subjects`（含 `label`/`name`/`type`/`refImage` 等）、`acts`（按已存内容完整返回，含 `turn`/`content`/`cameraType`/`shotSize`/`cut`）。

          <strong>previewUrl</strong> `null`

          当前固定为 `null`。

          <strong>aspectRatio</strong> `null`

          Directing 模型固定为 `null`。

          <strong>createdAt</strong> `string`

          创建时间，ISO 8601 格式。

          <strong>updatedAt</strong> `string`

          最近更新时间，ISO 8601 格式。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 前置状态与调用注意事项 <span id="ho-dir-qwd-notes-title" />

- 本接口可查询 `generating`、`ready` 或 `failed` World。
- 不回显创建请求中的 `async`。
- 图片字段只返回 URL 和 `referenceType`，不会回传 base64。`simple` 模式的 `inputImages` 会作为参考图持久化并可在详情中返回；`scriptlist` 不接受该字段。
- `scriptList` 正常反解时返回已保存的完整结构，`acts` 按已存内容完整返回；反解失败时返回 `scriptList=null`，不会用空 `acts` 表示反解失败。
- 查询其它模型、其它主账号或已删除的 World 均返回 `403001`，不会泄漏资源是否存在。

## 错误码 <span id="ho-dir-qwd-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-dir-qwd-next-title" />

- [查询World列表](/zh/model-studio/happyoyster-directing-query-world-list-api-reference)：分页查询所有 Directing World。
- [删除World](/zh/model-studio/happyoyster-directing-delete-world-api-reference)：删除不再使用的 World。
- World 为 `ready` 时可[获取体验凭证](/zh/model-studio/happyoyster-directing-get-travel-credential-api-reference)。
