> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 更新 Agent

> 更新 Agent 的名称、描述或草稿配置。

<meta property="og:title" content="更新 Agent" />

## 前提 <span id="h-ragapi-update-prereq" />

已获取阿里云百炼 API Key 与业务空间 ID，鉴权方式见[API 认证](/zh/model-studio/rag-api-authentication)。更新 Agent 需要知识库-修改权限，由[业务空间成员管理](https://bailian.console.aliyun.com/?tab=globalset#/efm/business_management)中的角色决定，详见[权限要求](/zh/model-studio/rag-api-agent-overview)。

## 接口 <span id="h-ragapi-update-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/app/update`

更新指定 Agent 的名称、描述、版本描述或配置。仅 `beta` 版本允许修改 `agent_config`；传入已发布版本号时只能修改 `agent_version_desc`。

<Warning>
  `agent_config` 仅允许修改 `beta` 草稿版本；已发布版本仅能修改 `agent_version_desc`。要修改已发布版本的配置，需先调用 update 接口更新 `beta` 草稿，再调用 deploy 接口发布新版本。
</Warning>

## 请求体 <span id="h-ragapi-update-req" />

| 字段                   | 必填 | 类型     | 说明                                                        |
| -------------------- | -- | ------ | --------------------------------------------------------- |
| `agent_id`           | 是  | string | Agent ID。                                                 |
| `agent_name`         | 否  | string | 新的 Agent 名称，最长 200 字符。                                    |
| `agent_desc`         | 否  | string | 新的 Agent 描述，最长 1000 字符。                                   |
| `agent_version`      | 否  | string | Agent 版本号，默认 `beta`。传入已发布版本号时只能修改 `agent_version_desc`。   |
| `agent_version_desc` | 否  | string | 版本描述。                                                     |
| `agent_config`       | 否  | object | 新的 Agent 配置（仅 beta 版本允许修改），结构同创建接口的 agent\_config。子字段见下表。 |

<Note>
  `agent_config` 结构与[创建 Agent](/zh/model-studio/rag-api-agent-create)相同。
</Note>

<strong>agent\_config 子字段</strong>

| 字段                              | 必填 | 类型             | 说明                                                                                                       |
| ------------------------------- | -- | -------------- | -------------------------------------------------------------------------------------------------------- |
| `agent_model`                   | 否  | string         | Agent 使用的模型名称，如 `qwen3.7-plus`、`qwen3.6-plus`。                                                           |
| `agent_policy`                  | 否  | string         | Agent 策略：`turbo`（极速模式）或 `agentic`（智能体模式）。                                                                |
| `enable_anti_leak`              | 否  | string         | 是否开启防泄漏功能，`true` 或 `false`。                                                                              |
| `enable_citation`               | 否  | string         | 是否开启引用标注，`true` 或 `false`。                                                                               |
| `enable_kb_router`              | 否  | string         | 是否启用知识库路由（search 场景），`true` 或 `false`。                                                                   |
| `enable_refusal`                | 否  | string         | 是否开启拒答功能，`true` 或 `false`。                                                                               |
| `enable_rich_text`              | 否  | string         | 是否开启富文本输出，`true` 或 `false`。                                                                              |
| `enable_session_file`           | 否  | string         | 是否开启会话文件功能，`true` 或 `false`。                                                                             |
| `hybrid_rerank`                 | 否  | object         | 全局混排配置（search 场景）。子字段见下表。                                                                                |
| `kb_router_model`               | 否  | string         | 路由模型名称，`enable_kb_router` 为 `true` 时必填，须在平台白名单内。                                                         |
| `kb_search_configs`             | 否  | array\<object> | 知识库检索配置列表。元素字段见下表。                                                                                       |
| `max_completion_tokens`         | 否  | integer        | 最大生成 token 数，须大于等于 1。                                                                                    |
| `max_num_llm_calls`             | 否  | integer        | Agent 最大 LLM 调用次数，范围 \[1, 30]。                                                                           |
| `rerank_top_n`                  | 否  | integer        | 混排 Top N（search 场景），范围 \[1, 20]。                                                                         |
| `session_file_max_parse_length` | 否  | integer        | 会话文件最大解析长度，须小于模型的 max\_context\_length。                                                                  |
| `temperature`                   | 否  | number         | 模型采样温度，取值范围 \[0, 2]。                                                                                     |
| `user_system_prompt`            | 否  | string         | 自定义系统提示词（chat 场景），放在 agent\_config 顶层，透传不校验，长度/生效由下游运行时决定。注意字段名是 user\_system\_prompt 而非 system\_prompt。 |

<strong>agent\_config.kb\_search\_configs\[] 元素</strong>

| 字段                        | 必填 | 类型      | 说明                                               |
| ------------------------- | -- | ------- | ------------------------------------------------ |
| `credibility_level`       | 否  | string  | 知识库可信度等级：`low`、`medium`、`high`。不传时默认回填 `medium`。 |
| `dense_similarity_top_k`  | 否  | integer | 稠密检索返回的 Top-K 数量，范围 \[1, 100]。                   |
| `id`                      | 否  | string  | 关联的知识库 Pipeline ID。                              |
| `rerank`                  | 否  | object  | 重排序配置。子字段见下表。                                    |
| `rerank_min_score`        | 否  | number  | 重排序最低分数阈值，范围 \[0, 1]，分数低于此值的切片会被过滤。              |
| `rerank_top_n`            | 否  | integer | 重排序后保留的 Top-N 数量，范围 \[1, 20]。                    |
| `search_filters`          | 否  | array   | 检索过滤条件，按知识库 schema 校验。                           |
| `sparse_similarity_top_k` | 否  | integer | 稀疏检索 Top K，范围 \[0, 100]。                         |
| `weight`                  | 否  | number  | 知识库权重。                                           |

<strong>kb\_search\_configs\[].rerank 子字段</strong>

| 字段                | 必填 | 类型     | 说明                                                                                          |
| ----------------- | -- | ------ | ------------------------------------------------------------------------------------------- |
| `model_name`      | 否  | string | 重排序模型名称，如 `gte-rerank-hybrid`。                                                              |
| `rerank_instruct` | 否  | string | 自定义重排序指令，仅在 `rerank_mode` 为 `custom` 时生效，最长 500 字符。                                         |
| `rerank_mode`     | 否  | string | 重排序模式：`qa`（问答匹配）、`similar`（相似度匹配）或 `custom`（自定义指令）。非 `custom` 模式下 `rerank_instruct` 会被自动清空。 |

<strong>agent\_config.hybrid\_rerank 子字段</strong>

| 字段                | 必填 | 类型     | 说明                                                                      |
| ----------------- | -- | ------ | ----------------------------------------------------------------------- |
| `model_name`      | 否  | string | 混排模型代码。含 VL 知识库时仅限多模态模型；其他情况支持全部排序模型。                                   |
| `rerank_mode`     | 否  | string | 混排模式：`qa`、`similar` 或 `custom`。非 `custom` 模式下 `rerank_instruct` 会被自动清空。 |
| `rerank_instruct` | 否  | string | 自定义混排指令，仅在 `rerank_mode` 为 `custom` 时生效，最长 500 字符。                      |

## 请求示例 <span id="h-ragapi-update-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/app/update" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "agent_id": "aid-8f3a1b2c4d5e6f70",
    "agent_name": "客服知识问答助手 v2",
    "agent_config": {
        "agent_policy": "agentic",
        "agent_model": "qwen3.6-plus",
        "enable_session_file": "false",
        "enable_refusal": "true",
        "enable_anti_leak": "true",
        "enable_rich_text": "false",
        "enable_citation": "true",
        "temperature": 0.7,
        "max_num_llm_calls": 10,
        "kb_search_configs": [
            {
                "id": "pipeline-xxxxxx",
                "weight": 1,
                "dense_similarity_top_k": 15,
                "rerank_top_n": 5,
                "rerank": {
                    "model_name": "gte-rerank-hybrid",
                    "rerank_mode": "custom",
                    "rerank_instruct": "根据用户问题的技术领域对文档进行相关性排序"
                }
            }
        ]
    }
}'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-update-resp" />

操作成功返回 200。

```json
{
  "request_id": "b2c3d4e5-f6a7-8901-bcde-f12345678901",
  "status_code": 200,
  "code": "Success",
  "message": "",
  "status": "SUCCESS",
  "success": true,
  "data": {
    "agent_id": "aid-8f3a1b2c4d5e6f70"
  }
}
```

## 响应字段 <span id="h-ragapi-update-dto" />

| 字段              | 类型      | 说明                         |
| --------------- | ------- | -------------------------- |
| `request_id`    | string  | 请求唯一标识，排查问题时请提供此 ID。       |
| `status_code`   | integer | HTTP 状态码。                  |
| `code`          | string  | 响应码，成功时为 `Success`。        |
| `message`       | string  | 提示信息。                      |
| `status`        | string  | 请求状态：`SUCCESS` 或 `FAILED`。 |
| `success`       | boolean | 请求是否成功。                    |
| `data.agent_id` | string  | Agent ID。                  |

## 错误码 <span id="h-ragapi-update-err" />

| HTTP 状态码 | 错误码                      | 说明                      |
| -------- | ------------------------ | ----------------------- |
| 400      | `Index.InvalidParameter` | 请求参数不合法，请检查参数是否完整且类型正确。 |

失败响应示例：

```json
{
  "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "status_code": 400,
  "code": "Index.InvalidParameter",
  "message": "请求参数不合法，请检查参数是否完整且类型正确。",
  "status": "FAILED",
  "success": false
}
```
