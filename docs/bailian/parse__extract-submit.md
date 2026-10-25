> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 提交抽取任务

> 提交结构化信息抽取任务，返回 biz_id 用于异步查询

<meta property="og:title" content="提交抽取任务" />

提交结构化信息抽取任务，返回 `biz_id`。输入可以是文件 URL，也可以是此前解析任务返回的 `parsed_file_biz_id`。信息抽取为异步任务：提交后使用该 ID 调用[查询抽取结果](/zh/model-studio/parse/extract-result)获取处理状态和结构化结果。

<Note>
  调用前请确保已获取 API Key，详见[鉴权](/zh/model-studio/parse/authentication)。
</Note>

## 请求参数

| 参数                                                       | 类型      | 必填 | 说明                                       |
| -------------------------------------------------------- | ------- | -- | ---------------------------------------- |
| `file_url`                                               | string  | 否  | 待抽取文档的可访问 URL。与 `parsed_file_biz_id` 二选一 |
| `parsed_file_biz_id`                                     | string  | 否  | 已完成解析的业务任务 ID。与 `file_url` 二选一           |
| `file_name`                                              | string  | 否  | 文件名                                      |
| `file_name_extension`                                    | string  | 否  | 文件类型                                     |
| `config_id`                                              | string  | 否  | 已保存的 Extract 配置 ID，例如 `config_xxx`。      |
| `processing`                                             | object  | 否  | 内联处理配置，优先级高于 `config_id`。                |
| `processing.user_prompt`                                 | string  | 否  | 用户 Prompt                                |
| `processing.extract_processing_config`                   | object  | 否  | 内联抽取处理配置。使用 `processing` 时必须提供           |
| `processing.extract_processing_config.citation_required` | boolean | 否  | 是否返回引用信息，默认 `true`                       |
| `processing.extract_processing_config.allow_inference`   | boolean | 否  | 是否允许推断，默认 `false`                        |
| `processing.extract_processing_config.extract_schema`    | object  | 否  | 抽取 Schema，值为 JSON 对象。使用内联处理配置时必须提供       |
| `output`                                                 | object  | 否  | 输出配置                                     |
| `output.oss_config`                                      | object  | 否  | OSS 输出配置                                 |
| `output.oss_config.bucket`                               | string  | 否  | OSS Bucket 名称                            |
| `output.oss_config.endpoint`                             | string  | 否  | OSS Endpoint                             |
| `output.oss_config.access_key_id`                        | string  | 否  | OSS AccessKey ID                         |
| `output.oss_config.access_key_secret`                    | string  | 否  | OSS AccessKey Secret                     |
| `output.oss_config.security_token`                       | string  | 否  | OSS Security Token                       |

<Note>
  请求需要分别完成两组二选一：文件来源使用 `file_url` 或 `parsed_file_biz_id`；处理定义使用 `config_id` 或内联 `processing`。复用解析结果时，该结果需要仍在 7 天保留期内，并且输入类型必须支持复用。
</Note>

## 返回结果

| 字段            | 类型     | 说明                             |
| ------------- | ------ | ------------------------------ |
| `request_id`  | string | 请求 ID                          |
| `data.biz_id` | string | 业务任务 ID，用于调用 `/extract/result` |

## 代码示例

以下示例分别展示两种处理定义， `config_id` 与 `processing` 同时存在时，`processing` 会优先生效。示例中的 URL、任务 ID 和配置 ID 均为脱敏占位值。

<Tabs>
  <Tab title="使用已保存配置">
    ```bash title="通过 config_id 提交"
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/extract/submit' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "file_url": "https://example.com/contracts/sample.pdf",
        "file_name": "sample.pdf",
        "config_id": "config_xxx"
      }'
    ```
  </Tab>

  <Tab title="使用内联配置">
    `extract_schema` 是字符串，因此其中的 JSON 需要转义。

    ```bash title="通过 processing 提交"
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/extract/submit' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "parsed_file_biz_id": "biz_xxx",
        "processing": {
            "user_prompt": "按 Schema 抽取合同信息",
            "extract_processing_config": {
                "citation_required": true,
                "allow_inference": false,
                "extract_schema": {
                    "type": "object",
                    "properties": {
                        "contract_number": {
                            "type": "string"
                        },
                        "total_amount": {
                            "type": "number"
                        }
                    }
                }
            }
        }
    }'
    ```
  </Tab>
</Tabs>

```json title="提交抽取任务响应"
{
  "request_id": "request_xxx",
  "data": {
    "biz_id": "biz_xxx"
  }
}
```

<Warning>
  抽取不支持音视频输入。复用的解析结果若已超过 7 天保留期或类型不匹配，接口会返回对应错误；详见[错误码](/zh/model-studio/parse/errors)。Schema 规模和长文档页数仅按工程设计边界规划，公开配额以正式页面为准，见[支持的文件与限制](/zh/model-studio/parse/supported-files-and-limits)。
</Warning>

<Tip>
  提交后，使用[查询抽取结果](/zh/model-studio/parse/extract-result)轮询任务状态并获取结构化结果。
</Tip>
