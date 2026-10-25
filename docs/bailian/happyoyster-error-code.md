> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster 错误码

> HappyOyster Open API 响应结构与错误码说明，包括业务码、典型场景与处理建议。

<meta property="og:title" content="HappyOyster 错误码" />

本文说明 HappyOyster Open API 的响应结构和错误码，适用于 Adventure、Directing、Acting 三个模型的全部 Open API 接口。

## 响应结构 <span id="ho-ec-envelope-title" />

请求通过网关鉴权后（含业务成功与业务错误），响应统一返回 HTTP 200，Body 为如下 JSON 结构体：

```json
{
  "code": 0,
  "message": null,
  "data": {}
}
```

| 字段        | 类型             | 说明                                     |
| --------- | -------------- | -------------------------------------- |
| `code`    | integer        | 业务返回码。`0` 表示成功，非 `0` 表示业务错误，详见下文错误码列表。 |
| `message` | string \       | null                                   | 可读的错误信息；`code=0` 时为 `null`。       |
| `data`    | object \       | null                                   | 业务数据。成功时为对应接口的返回对象；错误时通常为 `null`。 |

网关层的 AK、签名、时间戳等鉴权失败会直接被拦截，响应不遵循该结构，通常以 4xx / 5xx HTTP 状态码返回，详见[错误码](/zh/model-studio/error-code)。

## 错误码列表 <span id="ho-ec-list-title" />

| code     | 说明                           | 典型场景与处理建议                                                                                                                                                                  |
| -------- | ---------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `0`      | 成功                           | 请求成功，读取 `data`。                                                                                                                                                            |
| `400000` | 请求参数无效                       | `mode` 与当前模型不符；`prompt` 或 `firstFrameImage` 缺失；`prompt` 超长；`creationModel` / `uploadMode` / `resolution` / `aspectRatio` / `perspective` 等取值非法；图片或加密 ID 格式错误。按接口文档校正入参后重试。 |
| `400001` | 图片 URL 拉取或转存失败               | 首帧图或参考图 URL 不可访问或已过期。检查 URL 可访问性与有效期后重试创建 World。                                                                                                                           |
| `401010` | `ticket` 无效或已过期              | 进房（enter-travel）。重新调用获取体验凭证换取新 `ticket`。                                                                                                                                   |
| `401011` | `ticket` 已使用                 | 进房。`ticket` 为一次性凭证，需重新换取。                                                                                                                                                  |
| `403001` | World 不存在、已删除、不归属或不属于当前模型    | 查询、换凭证、进房与列表筛选。删除时仅跨账号、workspace 或跨模型 ID 返回该码；同账号下已不存在的 ID 返回 `code=0, deleted=false`。                                                                                     |
| `403002` | World 不是 `ready` 或不可用        | 换凭证、进房。先轮询构建状态至 `ready` 再操作。                                                                                                                                               |
| `403003` | 当前接口仅允许主 API Key             | World 管理、Travel 列表、产物等接口。改用主 API Key（`sk-` 开头）调用。                                                                                                                          |
| `403004` | 输入内容未通过内容安全策略                | 创建 World、发送 `instruct`。调整文本内容后重试。                                                                                                                                          |
| `403005` | 输入图片版权或 IP 校验不通过             | 创建 World。更换合规图片后重试。                                                                                                                                                        |
| `403007` | 功能或服务规格未开通                   | 创建、换凭证、进房。确认已开通对应模型能力。                                                                                                                                                     |
| `403008` | 容量配置暂不可用                     | 创建、换凭证、进房。稍后重试或联系服务方扩容。                                                                                                                                                    |
| `404000` | Travel 不存在、不归属、不属于当前模型或无可用产物 | Travel 状态查询、控制、结束、产物查询。                                                                                                                                                    |
| `409000` | 请求与当前资源状态冲突                  | 暂停、恢复、结束、指令等控制接口；对不支持该接口的模型调用同样返回。                                                                                                                                         |
| `429001` | 当前规格并发已满                     | 进房。稍后重试或提升并发规格。                                                                                                                                                            |
| `429002` | 当前可用容量不足                     | 进房。稍后重试。                                                                                                                                                                   |
| `500000` | 系统内部错误                       | 未分类异常；对不支持的接口（如部分模型的 `rewind`）调用也可能返回。                                                                                                                                     |
| `500001` | 推流资源分配失败                     | 进房。稍后重试。                                                                                                                                                                   |

跨模型访问不泄露资源是否存在，World 统一返回 `code=403001`，Travel 统一返回 `code=404000`。

## Travel 失败 errorCode <span id="ho-ec-travel-errorcode-title" />

查询 Travel 状态或列表时，`status=failed` 的条目会额外返回结构化字段 `errorCode` 与英文说明 `errorMessage`（失败响应结构见对应接口文档）。同一 `errorCode` 在查询接口和结束 Travel 响应中的 `errorMessage` 文案可能不同；请按 `errorCode` 分支处理，不要匹配 `errorMessage`。

| errorCode                           | errorMessage                                         | 说明                                      |
| ----------------------------------- | ---------------------------------------------------- | --------------------------------------- |
| `TRAVEL_SESSION_INIT_FAILED`        | `Failed to allocate inference resources.`            | 进房时推理会话初始化失败，多为推理资源不足                   |
| `TRAVEL_NO_STREAM_AUTO_END`         | `No video stream was received before timeout.`       | 客户端超时未收到推流，通过结束 Travel 传入 `failCode` 结束 |
| `TRAVEL_STREAM_CREATE_FAILED`       | `Failed to create the video stream.`                 | 推流通道创建失败                                |
| `CONTENT_VIDEO_MODERATION_REJECTED` | `Something went wrong.`                              | 体验画面被内容安全策略中止                           |
| `TRAVEL_RUNTIME_FAILED`             | `The experience was interrupted by a runtime error.` | 其它运行期失败，或服务端未记录具体原因                     |

未列出的 `errorCode` 按未知失败处理即可。
