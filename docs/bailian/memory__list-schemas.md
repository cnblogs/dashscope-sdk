> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 列出画像模板

> 分页查询全部画像模板

<meta property="og:title" content="列出画像模板" />

分页查询当前用户的全部画像模板。

## 请求方法与路径

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                  | 类型      | 必填 | 位置    | 说明         |
| ------------------- | ------- | -- | ----- | ---------- |
| `page_num`          | integer | 否  | Query | 页码，默认 1    |
| `page_size`         | integer | 否  | Query | 每页数量，默认 10 |
| `memory_library_id` | string  | 否  | Query | 记忆库 ID     |

## 返回结果

| 字段                                    | 类型      | 说明      |
| ------------------------------------- | ------- | ------- |
| `profile_schemas`                     | array   | 画像模板列表  |
| `profile_schemas[].profile_schema_id` | string  | 画像模板 ID |
| `profile_schemas[].name`              | string  | 画像模板名称  |
| `profile_schemas[].description`       | string  | 画像模板描述  |
| `profile_schemas[].plan_version`      | string  | 收费计划    |
| `profile_schemas[].extract_scene`     | string  | 提取场景    |
| `request_id`                          | string  | 请求 ID   |
| `total`                               | integer | 画像模板总数  |

## 代码示例

```bash cURL
curl -X GET "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas?page_size=10&page_num=1" \
  --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
  --header "Content-Type: application/json"
```

## 响应示例

```json
{
  "profile_schemas": [
    {
      "description": "包含年龄和兴趣的用户信息",
      "extract_scene": "efficient",
      "name": "用户基础画像",
      "plan_version": "pro",
      "profile_schema_id": "50edf54eaa5842e891d959ae115205be"
    },
    {
      "description": "系统自动创建的默认画像模板",
      "extract_scene": "efficient",
      "name": "默认画像",
      "plan_version": "pro",
      "profile_schema_id": "976e2e6e59f64ed0b25e6ca0268624bb"
    }
  ],
  "request_id": "3c46983f-85fd-92a5-8440-2e459cc91b2f",
  "total": 2
}
```

<Tip>
  查看单个模板详情使用 [GetProfileSchema](/zh/model-studio/memory/get-schema)。
</Tip>
