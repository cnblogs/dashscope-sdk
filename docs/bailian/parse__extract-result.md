> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询抽取结果

> 查询抽取任务状态和结构化结果

<meta property="og:title" content="查询抽取结果" />

调用本接口查询抽取任务状态和结构化结果。`data.status` 为 `processing` 时继续轮询，为 `success` 或 `failed` 时停止。

<Note>
  调用前请确保已获取 API Key，详见[鉴权](/zh/model-studio/parse/authentication)。
</Note>

## 请求参数

| 参数                             | 类型     | 必填 | 说明                           |
| ------------------------------ | ------ | -- | ---------------------------- |
| `biz_id`                       | string | 是  | `/extract/submit` 返回的业务任务 ID |
| `oss_config`                   | object | 否  | OSS 输出配置。                    |
| `oss_config.access_key_id`     | string | 否  | 托管 OSS AccessKey ID。         |
| `oss_config.access_key_secret` | string | 否  | 托管 OSS AccessKey Secret。     |
| `oss_config.security_token`    | string | 否  | 托管 OSS Security Token。       |

## 返回结果

| 字段                                      | 类型      | 说明                                              |
| --------------------------------------- | ------- | ----------------------------------------------- |
| `request_id`                            | string  | 请求 ID                                           |
| `data.status`                           | string  | 处理状态：`init` 、`processing`、`success` 或 `failed`。 |
| `data.page_count`                       | integer | 预估页数                                            |
| `data.extract_result_json`              | object  | 按 `extract_schema` 生成的结构化抽取结果                   |
| `data.fields`                           | array   | 字段级抽取结果                                         |
| `data.fields[].path`                    | string  | 字段路径                                            |
| `data.fields[].schema_type`             | string  | Schema 字段类型                                     |
| `data.fields[].status`                  | string  | 字段状态；公开示例包含 `found` 、`miss`和 `inferred`         |
| `data.fields[].value`                   | any     | 字段值，可以是 Schema 所定义的任意 JSON value                |
| `data.fields[].citations`               | array   | 引用列表                                            |
| `data.fields[].citations[].citation_id` | string  | 引用 ID                                           |
| `data.fields[].citations[].quote`       | string  | 引用原文                                            |
| `data.fields[].citations[].page`        | integer | 引用页码                                            |
| `data.fields[].citations[].page_width`  | integer | 页面宽度                                            |
| `data.fields[].citations[].page_height` | integer | 页面高度                                            |
| `data.fields[].citations[].bbox`        | array   | 相对于页面宽、高的坐标，`[x,y,w,h]`格式                       |
| `data.fields[].reason`                  | string  | 字段结果的原因说明。status为`inferred`时生效                  |

<Note>
  当状态为 `processing` 时继续查询本接口；当状态为 `failed` 时，结合[错误码](/zh/model-studio/parse/errors)处理失败原因。
</Note>

## 代码示例

```bash cURL
curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/extract/result' \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H 'Content-Type: application/json' \
  -d '{
    "biz_id": "biz_xxx"
  }'
```

```
{
  "request_id": "request_xxx",
  "data": {
    "status": "success",
    "extract_result_json": {
      "contract_number": "contract_xxx",
      "total_amount": 144000
    },
    "fields": [
      {
        "path": "contract_number",
        "schema_type": "string",
        "status": "found",
        "value": "contract_xxx",
        "citations": [
          {
            "citation_id": "citation_xxx",
            "quote": "合同编号：contract_xxx",
            "page": 1,
            "page_width": 1080,
            "page_height": 1920,
            "bbox": [100, 100, 100, 100]
          }
        ]
      }
    ],
    "page_count": 10
  }
}
```
