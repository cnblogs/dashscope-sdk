> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster-Acting-创建World API参考

> 创建一个角色演绎 World。使用自然语言 Prompt 和必填首帧图创建 World，接口立即返回加密 World ID（encryptedWorldId），World 在后台异步构建，客户端轮询构建进度直至完成。

<meta property="og:title" content="HappyOyster-Acting-创建World API参考" />

## 适用范围 <span id="ho-act-cw-scope-title" />

创建一个 Acting World。调用前请确认以下事项：

- <strong>鉴权要求</strong>：仅支持<strong>主 API Key</strong>调用，临时 API Key 不可用（错误码 `403003`）。

  - 获取主 API Key：[获取与配置 API Key](/zh/model-studio/get-api-key#c38fb45bc6sje)。
- <strong>调用模式</strong>：推荐使用异步模式。

  - <strong>异步模式</strong>（默认）：`async=true`，接口立即返回 `encryptedWorldId`，需轮询[查询World构建状态](/zh/model-studio/happyoyster-acting-query-world-build-status-api-reference)获取进度。
  - <strong>同步模式</strong>：`async=false`，服务端内部轮询（间隔 3s，最长 120s），构建完成后返回；超时则降级为异步，客户端继续轮询。
- <strong>接口限制</strong>：本接口只能创建 Acting World，无需传 `mode`（服务端按 `3` 写入，传入非 `3` 返回 `400000`）。`creationModel` 恒为 `simple`，`uploadMode` 固定为 `first_frame`，进房版本固定为 `actingV2`。

## HTTP调用 <span id="ho-act-cw-http-title" />

<Tabs>
  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds`

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
        #### 请求参数 <span id="ho-act-cw-req-title" />
      </td>

      <td rowSpan={12}>
        <Tabs>
          <Tab title="首帧图（异步创建）">
            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "async": true,
                "prompt": "金发双马尾少女，白色蝴蝶结，额前金色新月冠，白衬衫、深蓝水手领与蓝色大领结，蓝白菱格短裙。右手举起一张写着「晚安」的黄色便签。身后深红丝绒帘与花丛。面对镜头，二次元写实混搭，暖色室内光。",
                "resolution": "480p",
                "aspectRatio": "9:16",
                "firstFrameImage": {
                    "url": "https://g-adoc.alcasset.com/media/maas_docs/sfm-cn/common/images/6a4b3c2d1e0f9fc5.png",
                    "referenceType": "default"
                }
            }'
            ```
          </Tab>

          <Tab title="首帧图base64（异步创建）">
            实际调用时 `base64` 需传入完整的 data URI，下面示例中的字符串已截断，仅用于演示格式。

            ```bash
            curl --location 'https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v2/apps/happyoyster-1.0-acting/openapi/v1/worlds' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "async": true,
                "prompt": "客厅沙发上的轻松对谈，角色自然看向镜头",
                "resolution": "480p",
                "aspectRatio": "9:16",
                "firstFrameImage": {
                    "base64": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQ...",
                    "referenceType": "default"
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
        ##### 请求体（Request Body） <span id="ho-act-cw-req-body-h5" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>async</strong> `boolean` （可选）

        是否异步创建。默认 `true`：

        - `true`：立即返回，World 在后台构建，客户端轮询[查询World构建状态](/zh/model-studio/happyoyster-acting-query-world-build-status-api-reference)。
        - `false`：服务端每 3 秒轮询一次，最长等待 120 秒，构建完成后返回；超时仍返回 `generating`，客户端随后改为自行轮询。
      </td>
    </tr>

    <tr>
      <td>
        <strong>creationModel</strong> `string` （可选）

        创建子模式。默认 `simple`，Acting 仅支持 `simple`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>prompt</strong> `string` <strong>（必选）</strong>

        角色、场景和演绎目标的自然语言描述。非空，最长 2000 字符。缺失、空白或超限返回 `400000`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>uploadMode</strong> `string` （可选）

        图片上传模式。默认 `first_frame`，Acting 仅支持 `first_frame`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>resolution</strong> `string` （可选）

        视频分辨率。默认 `480p`。可选值：

        - `480p`
        - `720p`
      </td>
    </tr>

    <tr>
      <td>
        <strong>aspectRatio</strong> `string` （可选）

        推流画幅，同时决定首帧图方向，建议两者保持一致。默认 `9:16`。可选值：

        - `9:16`（竖屏）：建议配竖屏首帧
        - `16:9`（横屏）：建议配横屏首帧

        即竖屏推流（`9:16`）建议传竖屏首帧，横屏推流（`16:9`）建议传横屏首帧。默认 `9:16`，需要横屏时须显式传入 `aspectRatio=16:9`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>refWorldId</strong> `string` （可选）

        基于已有 Acting World 衍生创建。必须是当前主账号名下的 Acting 加密 World ID；其它模型或其它主账号的 World 返回 `403001`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>firstFrameImage</strong> `object` <strong>（必选）</strong>

        复用为 World 首帧的图片引用。`url` 与 `base64` 二选一且互斥。图片约束如下：

        - 格式：JPG / JPEG / PNG / WebP
        - 大小：单张严格小于 6 MB
        - 宽高比：`aspectRatio=9:16` 时宽 / 高为 0.5–0.667；`aspectRatio=16:9` 时为 1.5–2.0
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

          首帧参考类型。默认 `default`，当前按 `default` 使用。
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
        #### 响应参数 <span id="ho-act-cw-resp-header-h4" />
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

          <Tab title="请求失败">
            ```json
            {
                "code": 400000,
                "message": "Invalid request parameters.",
                "data": null
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

        错误信息。成功时为 `null`；失败时为可读错误信息。
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

          当前创建状态：

          - `generating`：构建中
          - `ready`：就绪
          - `failed`：构建失败

          <strong>firstFrame</strong> `string`

          World 首帧 URL；尚未生成时为 `null`。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="ho-act-cw-errorcode-title" />

如果模型调用失败并返回报错信息，请参见[HappyOyster 错误码](/zh/model-studio/happyoyster-error-code)进行解决。

## 下一步 <span id="ho-act-cw-next-title" />

创建成功后可进行以下操作：

- [查询World构建状态](/zh/model-studio/happyoyster-acting-query-world-build-status-api-reference)：每 3–5 秒轮询，直到 World 进入 `ready`。
- World 进入 `ready` 后，调用[获取体验凭证](/zh/model-studio/happyoyster-acting-get-travel-credential-api-reference)换取一次性 `ticket`。
- [查询World详情](/zh/model-studio/happyoyster-acting-query-world-detail-api-reference)：查询完整创建元数据。
