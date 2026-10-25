> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Adventure-创建World API参考

> 创建一个世界探索 World。使用自然语言 Prompt 和必填首帧图创建 World，接口立即返回加密 World ID，World 在后台异步构建，客户端轮询构建进度直至完成。

<meta property="og:title" content="HappyOyster-Adventure-创建World API参考" />

## 适用范围 <span id="ho-adv-cw-scope-title" />

创建一个 Adventure World。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>调用模式</strong>：推荐使用异步模式。

  - <strong>异步模式</strong>（默认）：`async=true`，接口立即返回 `encryptedWorldId`，需轮询[查询World构建状态](/zh/model-studio/happyoyster-adventure-query-world-build-status-api-reference)获取进度。
  - <strong>同步模式</strong>：`async=false`，服务端内部轮询（间隔 3s，最长 120s），构建完成后返回；超时则降级为异步，客户端继续轮询。
- <strong>接口限制</strong>：本接口只能创建 Adventure World，无需传 `mode`（服务端按 `1` 写入，传入非 `1` 返回 `400000`）。`creationModel` 恒为 `simple`，`uploadMode` 固定为 `first_frame`，进房版本固定为 `wanderV2`。

## HTTP调用 <span id="ho-adv-cw-http-title" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds`

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
        #### 请求参数 <span id="ho-adv-cw-req-title" />
      </td>

      <td rowSpan={10}>
        <Tabs>
          <Tab title="首帧图（异步创建）">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "async": true,
                "perspective": "third_person",
                "prompt": "第三人称跟拍：一名身穿黑色防寒服、头戴黑色头盔的骑手骑着黑色雪地摩托驶向远方，履带扬起细雪。前方是积雪覆盖的针叶林，更远处是日照岩壁的陡峭雪山与蓝天白云。冬日晴空，雪地高光强烈，开阔冷冽。",
                "firstFrameImage": {
                    "url": "https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/common/images/6a4b3c2d1e0f9fc6.png"
                }
            }'
            ```
          </Tab>

          <Tab title="首帧图base64（异步创建）">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-adventure/openapi/v1/worlds' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "async": true,
                "perspective": "third_person",
                "prompt": "第三人称跟拍：一名身穿黑色防寒服、头戴黑色头盔的骑手骑着黑色雪地摩托驶向远方，履带扬起细雪。前方是积雪覆盖的针叶林，更远处是日照岩壁的陡峭雪山与蓝天白云。冬日晴空，雪地高光强烈，开阔冷冽。",
                "firstFrameImage": {
                    "base64": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQ..."
                }
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
        ##### 请求体（Request Body） <span id="ho-adv-cw-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>async</strong> `boolean` （可选）

        是否异步创建。默认 `true`：

        - `true`：立即返回，World 在后台构建，客户端轮询[查询World构建状态](/zh/model-studio/happyoyster-adventure-query-world-build-status-api-reference)。
        - `false`：服务端每 3 秒轮询一次，最长等待 120 秒，构建完成后返回；超时仍返回 `generating`，客户端随后改为自行轮询。
      </td>
    </tr>

    <tr>
      <td>
        <strong>perspective</strong> `string` <strong>（必选）</strong>

        视角。可选值：

        - `first_person`：第一人称
        - `third_person`：第三人称

        缺少该字段返回 `400000`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>prompt</strong> `string` <strong>（必选）</strong>

        世界主题描述，支持中英文。非空，最长 2000 字符。缺失、空白或超限返回 `400000`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>creationModel</strong> `string` （可选）

        创建子模式。默认 `simple`，Adventure 仅支持 `simple`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>uploadMode</strong> `string` （可选）

        图片上传模式。默认 `first_frame`，Adventure 仅支持 `first_frame`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>refWorldId</strong> `string` （可选）

        基于已有 Adventure World 衍生创建。必须是当前主账号名下的 Adventure 加密 World ID；其它模型或其它主账号的 World 返回 `403001`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>firstFrameImage</strong> `object` <strong>（必选）</strong>

        直接复用为 World 首帧的图片引用。`url` 与 `base64` 二选一且互斥。图片约束如下：

        - 格式：JPG / JPEG / PNG / WebP
        - 大小：单张严格小于 6 MB
        - 宽高比：必须为横屏，宽 / 高为 1.5–2.0（画面比例跟随该图）
        - 内容安全：未通过内容安全或版权 / IP 校验返回 `403004` / `403005`

        <Accordion title="属性" defaultOpen>
          <strong>url</strong> `string` （条件必选）

          首帧图片 URL。与 `base64` 二选一且互斥。约束：

          - 必须是带 Host 的合法 `http` / `https` URL，并可由服务端访问
          - 真实格式、大小和首帧宽高比在转存后校验
          - 异步请求可能先返回 `generating`，随后 World 因图片校验失败进入 `failed`

          <strong>base64</strong> `string` （条件必选）

          首帧图片 base64。与 `url` 二选一且互斥。约束：

          - 推荐使用完整 data URI `data:image/<subtype>;base64,<payload>`
          - 在创建入口同步校验格式、大小和首帧宽高比

          <strong>referenceType</strong> `string` （可选）

          参考图类型。默认 `default`，当前按 `default` 使用。
        </Accordion>
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
        #### 响应参数 <span id="ho-adv-cw-resp-header-h4" />
      </td>

      <td rowSpan={4}>
        <Tabs>
          <Tab title="异步创建">
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

          服务端生成的加密 World ID。同步和异步模式均返回，后续构建状态轮询、详情查询、换取体验凭证均使用此值。

          <strong>status</strong> `string`

          当前创建状态：`generating`（构建中）或 `ready`（就绪）；同步构建失败时也可能为 `failed`。

          <strong>firstFrame</strong> `string`

          World 首帧 URL；尚未生成时为 `null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="ho-adv-cw-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-adv-cw-next-title" />

创建成功后可进行以下操作：

- [查询World构建状态](/zh/model-studio/happyoyster-adventure-query-world-build-status-api-reference)：每 3–5 秒轮询，直到 World 进入 `ready`。
- World 进入 `ready` 后，调用[获取体验凭证](/zh/model-studio/happyoyster-adventure-get-travel-credential-api-reference)换取一次性 `ticket`。
- [查询World详情](/zh/model-studio/happyoyster-adventure-query-world-detail-api-reference)：查询完整创建元数据。
