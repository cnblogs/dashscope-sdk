> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取用户画像

> 查询指定用户已提取的画像属性

<meta property="og:title" content="获取用户画像" />

获取指定用户的画像属性。画像提取为异步过程；调用 AddMemory 添加对话后，首次查询可能尚未返回属性值，请按业务重试策略再次查询。

<Note>
  若返回的属性值均为空，请确认调用 [AddMemory](/zh/model-studio/memory/add-memory) 时已传入相同的 `profile_schema`。未传该参数时不会触发画像提取。
</Note>

## 请求方法与路径

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas/{profile_schema_id}/user_profile`

调用时请将 `{WorkspaceId}` 替换为真实的[业务空间 ID](/zh/model-studio/regions#h2_migrate_domain)。

## 请求参数

| 参数                  | 类型     | 必填 | 位置    | 说明      |
| ------------------- | ------ | -- | ----- | ------- |
| `profile_schema_id` | string | 是  | Path  | 画像模板 ID |
| `user_id`           | string | 是  | Query | 记忆实体 ID |
| `memory_library_id` | string | 否  | Query | 记忆库 ID  |

## 返回结果

| 字段                           | 类型     | 说明     |
| ---------------------------- | ------ | ------ |
| `request_id`                 | string | 请求 ID  |
| `profile.schema_name`        | string | 画像模板名称 |
| `profile.schema_description` | string | 画像模板描述 |
| `profile.attributes`         | array  | 画像属性列表 |
| `profile.attributes[].id`    | string | 属性 ID  |
| `profile.attributes[].name`  | string | 属性名称   |
| `profile.attributes[].value` | string | 属性值    |

## 代码示例

<CodeGroup>
  ```bash cURL
  curl -X GET "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/profile_schemas/{profile_schema_id}/user_profile?user_id=user_001" \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header "Content-Type: application/json"
  ```

  ```python Python
  from agentscope_runtime.tools.modelstudio_memory import (
      GetUserProfile, GetUserProfileInput,
  )
  import asyncio

  async def main():
      get_profile = GetUserProfile()
      try:
          profile = await get_profile.arun(GetUserProfileInput(
              schema_id="YOUR_SCHEMA_ID",
              user_id="user_001"
          ))
          for attr in profile.profile.attributes:
              print(f"{attr.name}: {attr.value or '未提取'}")
      finally:
          await get_profile.close()

  asyncio.run(main())
  ```
</CodeGroup>

## 响应示例

```json
{
  "profile": {
    "attributes": [
      {"id": "bb28e6cd522443fc9bf7e75b1377bf9d", "name": "年龄", "value": "28"},
      {"id": "04eb06f5f04a437b82d07ceb088103f9", "name": "爱好", "value": "踢足球"},
      {"id": "2eff4652a6bc47b0803635033b1e08dd", "name": "职业", "value": "软件工程师"}
    ],
    "schema_description": "包含年龄和兴趣的用户信息",
    "schema_name": "用户基础画像"
  },
  "request_id": "78cb6090-a7a5-9897-861c-082a64b43d97"
}
```

<Tip>
  完整画像流程参见[使用用户画像](/zh/model-studio/memory/user-profile)。
</Tip>
