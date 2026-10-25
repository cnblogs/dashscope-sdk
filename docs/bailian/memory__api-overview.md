> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 概览

> 服务地址、协议约定、请求格式与通用响应

<meta property="og:title" content="概览" />

通过记忆库 API，你可以以编程方式写入记忆、检索记忆、管理画像模板。全部接口通过业务空间专属域名提供服务。

## 服务地址

```text
https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/
```

`{WorkspaceId}` 是业务空间 ID，格式如 `llm-xxxxxxxxxxxx`。在[业务空间管理](https://bailian.console.aliyun.com/cn-beijing/settings/workspace)页面复制 **API Host** 列的内容即可获得包含业务空间 ID 的域名，详见[获取业务空间专属域名](/zh/model-studio/regions#h2_migrate_domain)。业务空间专属域名仅支持该业务空间所属的 API Key 调用。

全部接口使用统一的服务地址。将各接口页面列出的路径拼接到服务地址后即可得到完整请求地址，例如 `/add` 对应 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory/add`。

使用 Python SDK（`agentscope-runtime`）时，将环境变量 `MEMORY_SERVICE_ENDPOINT` 设置为服务地址（末尾不带 `/`），SDK 会通过该地址发起请求：

```bash
export MEMORY_SERVICE_ENDPOINT="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/memory"
```

## 协议约定

- 全部接口均通过 **HTTPS** 访问，不支持 HTTP
- 请求体和响应体均为 **JSON** 格式，字符集 **UTF-8**
- 写入和检索接口使用 `POST` 方法；列表查询使用 `GET` 方法；更新使用 `PATCH`；删除使用 `DELETE`

## 通用请求头

| Header          | 必填         | 说明                                                                            |
| --------------- | ---------- | ----------------------------------------------------------------------------- |
| `Authorization` | 是          | `Bearer $DASHSCOPE_API_KEY`，获取方式见[鉴权](/zh/model-studio/memory/authentication) |
| `Content-Type`  | 仅含请求体的接口必填 | `application/json`                                                            |

## 通用响应格式

成功响应的公共字段只有 `request_id`，其他业务字段因接口而异，请以对应接口页面为准。

**成功响应：**

```json
{
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

**失败响应：**

```json
{
  "code": "InvalidParameter",
  "message": "user_id is required",
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

排查问题时，请提供 `request_id`，以便技术支持快速定位。错误处理建议见[错误码](/zh/model-studio/memory/errors)。

## 分页

列表接口支持分页查询：

| 参数          | 说明        |
| ----------- | --------- |
| `page_size` | 每页数量      |
| `page_num`  | 页码，从 1 开始 |

<Note>
  记忆库将于 **2026 年 8 月 20 日** 正式商业化计费。Add 和 Search 调用区分 **Pro** 和 **Lite** 两个策略版本。详见[计费说明](/zh/model-studio/memory/billing)。
</Note>

<Tip>
  准备好凭证后，查看 [AddMemory 接口](/zh/model-studio/memory/add-memory)。
</Tip>
