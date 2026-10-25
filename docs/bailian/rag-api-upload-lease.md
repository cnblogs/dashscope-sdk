> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 申请上传租约

> 上传文件到数据中心的第一步。返回 OSS 预签名 URL 和租约 ID，用于后续上传文件内容。

<meta property="og:title" content="申请上传租约" />

## 前提 <span id="h-ragapi-upload-lease-prereq" />

已获取阿里云百炼 API Key（[控制台 API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)）并完成鉴权配置，详见 [RAG API 概览](/zh/model-studio/rag-api-overview)与[鉴权](/zh/model-studio/rag-api-authentication)。数据导入接口与其他 RAG API 使用相同的 Base URL 与鉴权方式，路径前缀为 `/api/v1/connector/dash/`。

## 接口 <span id="h-ragapi-upload-lease-endpoint" />

<strong>POST</strong> `/api/v1/connector/dash/applyFileUploadLease`

上传文件到数据中心的第一步。返回 OSS 预签名 URL 和租约 ID，用于后续上传文件内容。完整上传流程为：1) 调用本接口获取租约 → 2) 使用返回的 URL 通过 PUT 上传文件到 OSS → 3) 调用 addFile 注册文件。

<Note>
  文件上传分为三步：

  1. **申请租约**（本接口）— 获取 OSS 预签名 URL 和 `leaseId`
  2. **PUT 上传到 OSS** — 使用返回的 `param.url` 和 `param.headers` 通过 HTTP PUT 上传文件二进制内容
  3. **注册文件**（[`addFile`](/zh/model-studio/rag-api-add-file)）— 使用 `leaseId` 将文件注册到数据中心

  请注意 `sizeBytes` 必须以**字符串格式**传入（如 `"1048576"`），不能传数字。
</Note>

## 请求体 <span id="h-ragapi-upload-lease-req" />

| 字段                    | 必填 | 类型      | 说明                                                                                                                                                                                |
| --------------------- | -- | ------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `category`            | 是  | string  | 类目 ID（注意：此字段名为 category，不是 categoryId），通过 `listCategory` 获取。                                                                                                                      |
| `fileName`            | 是  | string  | 文件名称，包含扩展名。                                                                                                                                                                       |
| `sizeBytes`           | 是  | string  | 文件大小（字节），以字符串格式传入。                                                                                                                                                                |
| `contentMd5`          | 是  | string  | 文件内容的 MD5 值（Base64 编码），长度 1\~64 个字符。                                                                                                                                              |
| `categoryType`        | 否  | string  | 类目类型，默认 `UNSTRUCTURED`。`UNSTRUCTURED`：类目，用于构建知识库；`SESSION_FILE`：用于智能体应用会话交互的文件。使用 `SESSION_FILE` 时，addFile 的 categoryType 也须传 `SESSION_FILE`；此类文件仅当前会话有效，关闭会话后过期（最长 7 天），不支持长期保存。 |
| `useInternalEndpoint` | 否  | boolean | 是否使用安全存储空间的内网 Endpoint 上传，默认 false。                                                                                                                                               |

## 请求示例 <span id="h-ragapi-upload-lease-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/connector/dash/applyFileUploadLease" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "category": "cate_abc123",
    "fileName": "product-guide.pdf",
    "sizeBytes": "1048576",
    "contentMd5": "d41d8cd98f00b204e9800998ecf8427e"
  }'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-upload-lease-resp" />

成功返回 200。

```json
{
  "code": "Success",
  "message": "",
  "messageUnmodified": false,
  "requestId": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
  "data": {
    "type": "OSS.PreSignedUrl",
    "param": {
      "url": "https://oss-example.aliyuncs.com/...",
      "method": "PUT",
      "headers": {
        "x-bailian-extra": "MTkxMDA3MzE0NjA0NDgzNA==",
        "Content-Type": "text/plain"
      }
    },
    "leaseId": "82845f78ab0343ada293c9595ed93dbf.1782191906659"
  },
  "status": 200
}
```

### 响应字段 <span id="h-ragapi-upload-lease-dto" />

| 字段          | 类型      | 说明                                                                                                                                                                                                                                                                                   |
| ----------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `code`      | string  | 响应码，成功时为 `Success`。                                                                                                                                                                                                                                                                  |
| `message`   | string  | 错误或提示信息，成功时为空字符串。                                                                                                                                                                                                                                                                    |
| `requestId` | string  | 请求唯一标识，排查问题时请提供此 ID。                                                                                                                                                                                                                                                                 |
| `data`      | object  | 申请结果。子字段 `type`（string，上传类型，固定为 `OSS.PreSignedUrl`）、`leaseId`（string，上传租约 ID，在注册文件时使用）、`param`（object，OSS 上传参数。子字段：`url`（string，OSS 预签名上传 URL）、`method`（string，HTTP 方法，固定为 `PUT`）、`headers`（object，上传时需要携带的请求头。子字段：`x-bailian-extra`（string，百炼平台附加信息）、`Content-Type`（string，内容类型）））。 |
| `status`    | integer | 业务状态码。成功为 `200`，参数错误等失败场景为 `400`。注意失败时 HTTP 状态码仍为 200，需根据本字段与 `code` 判断结果。                                                                                                                                                                                                           |

## 错误码 <span id="h-ragapi-upload-lease-err" />

| HTTP 状态码 | 错误码 `code`         | 说明                                                           |
| -------- | ------------------ | ------------------------------------------------------------ |
| 400      | `InvalidParameter` | 请求参数无效。`message` 示例：`Required parameter missing or invalid.` |
| 401      | `InvalidApiKey`    | 鉴权失败，API Key 无效或缺失。`message` 示例：`Invalid API-key provided.`  |

业务失败时 HTTP 状态码仍为 200，需根据响应体中的 `status` 与 `code` 判断结果。
