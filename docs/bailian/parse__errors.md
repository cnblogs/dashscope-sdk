> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 错误码

> 查看 ParseX API 的错误响应格式、HTTP 状态码、错误消息与处理含义。

<meta property="og:title" content="错误码" />

当请求失败时，API 返回请求 ID、错误码和错误消息。

## 错误响应结构

| 字段           | 类型       | 说明               |
| ------------ | -------- | ---------------- |
| `request_id` | `string` | 请求 ID，可用于定位失败请求。 |
| `code`       | `string` | 错误码。             |
| `message`    | `string` | 错误消息。            |

```json title="错误响应示例"
{
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "code": "NotExistBizId",
  "message": "The bizId not exist."
}
```

## 常见错误码

| code             | HTTP | message                                    | 说明           |
| ---------------- | ---: | ------------------------------------------ | ------------ |
| `NotExistBizId`  | 400  | `The bizId not exist.`                     | biz ID 不存在   |
| `ResultNotReady` | 409  | `Retrieve result if job is not completed.` | 任务未完成，结果尚未就绪 |

## 参数错误

| code                     | HTTP | message                                                     | 说明                 |
| ------------------------ | ---: | ----------------------------------------------------------- | ------------------ |
| `FileDownloadFailed`     | 400  | `File download failed.`                                     | 文件下载失败，不可重试        |
| `FileDownloadTimeout`    | 400  | `File download timed out.`                                  | 文件下载超时，可重试         |
| `FileFormatNotSupported` | 400  | `Unsupported file formats`                                  | 不支持的文件格式           |
| `FileSizeExceeded`       | 400  | `File size exceeds limit`                                   | 文件大小超过限制           |
| `InvalidFileUrl`         | 400  | `URL is invalid or inaccessible`                            | URL 不合法或不可访问       |
| `ParseResultExpired`     | 400  | `The ParseResult has exceeded the 30-day retention period.` | 解析结果已过期，超过 30 天保留期 |
| `PageCountExceeded`      | 400  | `The page count exceeds the limit.`                         | 页数超过限制             |
| `PageRangeInvalid`       | 400  | `The page range is invalid.`                                | 页码范围无效             |

## 任务错误

| code                     | HTTP | message                                                   | 说明          |
| ------------------------ | ---: | --------------------------------------------------------- | ----------- |
| `ProcessingFailed`       | 500  | `An internal error occurred in the processing engine.`    | 处理引擎内部错误    |
| `ProcessingTimeout`      | 500  | `The processing timed out.`                               | 处理超时        |
| `BatchProcessingFailed`  | 500  | `The batch processing failed.`                            | 批量处理失败      |
| `CustomerOssWriteFailed` | 500  | `Failed to write to the customer OSS.`                    | 写入客户 OSS 失败 |
| `InternalError`          | 500  | `An internal error has occurred, please try again later.` | 内部错误        |

## 抽取错误

| code                     | HTTP | message                                                                                        | 说明          |
| ------------------------ | ---: | ---------------------------------------------------------------------------------------------- | ----------- |
| `ParseResultNotReusable` | 400  | `The ParseResult is not reusable because the input is audio/video or the type does not match.` | 解析结果不可复用    |
| `UnsupportedFileType`    | 400  | `Extract does not support audio/video input.`                                                  | 抽取不支持音视频输入  |
| `InvalidSchema`          | 400  | `The schema format is invalid.`                                                                | Schema 格式无效 |

## 其他错误

| code                    | HTTP | message                                | 说明     |
| ----------------------- | ---: | -------------------------------------- | ------ |
|                         |      |                                        |        |
| `AccountOverdue`        | 403  | `Service suspended due to unpaid fees` | 欠费停服   |
| `ServiceQuotaExhausted` | 503  | `The service quota is exhausted.`      | 服务配额耗尽 |

## 处理建议

<AccordionGroup>
  <Accordion title="任务结果尚未就绪">
    收到 `ResultNotReady` 时，任务尚未完成。继续使用原 `biz_id` 调用对应的结果端点。
  </Accordion>

  <Accordion title="文件下载失败">
    `FileDownloadFailed` 标记为不可重试；请先修复文件 URL 或访问条件。`FileDownloadTimeout` 标记为可重试。
  </Accordion>

  <Accordion title="抽取任务无法复用解析结果">
    检查解析结果是否超过 7 天保留期，以及输入是否为抽取不支持的音视频或不匹配类型。
  </Accordion>
</AccordionGroup>

返回到[API 概览](/zh/model-studio/parse/api-overview)查看异步调用流程。
