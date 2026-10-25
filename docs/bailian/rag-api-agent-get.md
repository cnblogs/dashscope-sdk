> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询 Agent 详情

> 获取 Agent 完整信息，支持指定版本。

<meta property="og:title" content="查询 Agent 详情" />

## 前提 <span id="h-ragapi-get-prereq" />

已获取阿里云百炼 API Key 与业务空间 ID，鉴权方式见[API 认证](/zh/model-studio/rag-api-authentication)。本接口为读操作，仅需 API Key。

## 接口 <span id="h-ragapi-get-endpoint" />

<strong>POST</strong> `/api/v1/indices/rag/app/get`

查询指定 Agent 的详细信息，包括名称、描述、场景、状态以及各版本的配置详情。可通过 `agent_version` 指定版本号查询特定版本，不传则返回所有版本。

<Note>
  不支持传 `"release"` 作为 `agent_version`。
</Note>

## 请求体 <span id="h-ragapi-get-req" />

| 字段              | 必填 | 类型     | 说明                                  |
| --------------- | -- | ------ | ----------------------------------- |
| `agent_id`      | 是  | string | Agent ID。                           |
| `agent_version` | 否  | string | 指定版本号（`beta` 或数字如 `1`）。不传则返回所有版本信息。 |

## 请求示例 <span id="h-ragapi-get-eg" />

```bash
curl -X POST "$BASE_URL/api/v1/indices/rag/app/get" \
  -H "Authorization: Bearer $BAILIAN_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "agent_id": "aid-8f3a1b2c4d5e6f70",
    "agent_version": "beta"
}'
```

其中 `BASE_URL` 为 `https://{workspace_id}.cn-beijing.maas.aliyuncs.com`（`{workspace_id}` 为业务空间 ID），`BAILIAN_API_KEY` 为阿里云百炼 API Key。

## 响应示例 <span id="h-ragapi-get-resp" />

操作成功返回 200。

```json
{
  "request_id": "f6a7b8c9-d0e1-2345-fabc-456789012345",
  "status_code": 200,
  "code": "Success",
  "message": "",
  "status": "SUCCESS",
  "success": true,
  "data": {
    "agent_id": "aid-8f3a1b2c4d5e6f70",
    "agent_name": "客服知识问答助手",
    "agent_desc": "基于产品文档的智能客服问答 Agent",
    "agent_scene": "chat",
    "agent_status": "deployed",
    "create_time": "2026-07-10T14:30:00",
    "modify_time": "2026-07-15T09:15:00",
    "agent_details": [
      {
        "agent_version": "beta",
        "agent_version_desc": "草稿版本",
        "publish_time": null,
        "agent_config": {
          "agent_policy": "agentic",
          "agent_model": "qwen3.7-plus",
          "enable_session_file": "false",
          "enable_refusal": "true",
          "enable_anti_leak": "false",
          "enable_rich_text": "false",
          "enable_citation": "true",
          "temperature": 0.5,
          "kb_search_configs": [
            {
              "id": "pipeline-xxxxxx",
              "weight": 1,
              "credibility_level": "high",
              "dense_similarity_top_k": 10,
              "rerank_top_n": 5,
              "rerank_min_score": 0.3,
              "rerank": {
                "model_name": "gte-rerank-hybrid",
                "rerank_mode": "qa"
              }
            }
          ]
        }
      }
    ]
  }
}
```

## 响应字段 <span id="h-ragapi-get-dto" />

| 字段                   | 类型             | 说明                                               |
| -------------------- | -------------- | ------------------------------------------------ |
| `request_id`         | string         | 请求唯一标识，排查问题时请提供此 ID。                             |
| `status_code`        | integer        | HTTP 状态码。                                        |
| `code`               | string         | 响应码，成功时为 `Success`。                              |
| `message`            | string         | 提示信息。                                            |
| `status`             | string         | 请求状态：`SUCCESS` 或 `FAILED`。                       |
| `success`            | boolean        | 请求是否成功。                                          |
| `data.agent_id`      | string         | Agent ID。                                        |
| `data.agent_name`    | string         | Agent 名称。                                        |
| `data.agent_desc`    | string         | Agent 描述。                                        |
| `data.agent_scene`   | string         | Agent 场景：`chat` 或 `search`。                      |
| `data.agent_status`  | string         | Agent 状态。                                        |
| `data.create_time`   | string         | 创建时间（ISO 8601 格式）。                               |
| `data.modify_time`   | string         | 修改时间（ISO 8601 格式）。                               |
| `data.agent_details` | array\<object> | Agent 版本详情列表。不指定 agent\_version 时返回所有版本。元素字段见下表。 |

<strong>data.agent\_details\[] 元素</strong>

| 字段                   | 类型     | 说明                             |
| -------------------- | ------ | ------------------------------ |
| `agent_version`      | string | 版本号，`beta` 或数字（如 `1`、`2`）。     |
| `agent_version_desc` | string | 版本描述。                          |
| `publish_time`       | string | 发布时间（ISO 8601 格式），beta 版本无此字段。 |
| `agent_config`       | object | Agent 配置。子字段见下表。               |
| `publish_user`       | string | 发布人。                           |

<strong>agent\_config 子字段</strong>

| 字段                              | 类型             | 说明                                                                                                       |
| ------------------------------- | -------------- | -------------------------------------------------------------------------------------------------------- |
| `agent_model`                   | string         | Agent 使用的模型名称，如 `qwen3.7-plus`、`qwen3.6-plus`。                                                           |
| `agent_policy`                  | string         | Agent 策略：`turbo`（极速模式）或 `agentic`（智能体模式）。                                                                |
| `enable_anti_leak`              | boolean        | 是否开启防泄漏功能，`true` 或 `false`。                                                                              |
| `enable_citation`               | boolean        | 是否开启引用标注，`true` 或 `false`。                                                                               |
| `enable_refusal`                | boolean        | 是否开启拒答功能，`true` 或 `false`。                                                                               |
| `enable_rich_text`              | boolean        | 是否开启富文本输出，`true` 或 `false`。                                                                              |
| `enable_session_file`           | boolean        | 是否开启会话文件功能，`true` 或 `false`。                                                                             |
| `hybrid_rerank`                 | object         | 全局混排配置（search 场景）。子字段见下表。                                                                                |
| `kb_search_configs`             | array\<object> | 知识库检索配置列表。元素字段见下表。                                                                                       |
| `max_completion_tokens`         | integer        | 最大生成 token 数，须大于等于 1。                                                                                    |
| `max_num_llm_calls`             | integer        | Agent 最大 LLM 调用次数，范围 \[1, 30]。                                                                           |
| `rerank_top_n`                  | integer        | 混排 Top N（search 场景），范围 \[1, 20]。                                                                         |
| `session_file_max_parse_length` | integer        | 会话文件最大解析长度，须小于模型的 max\_context\_length。                                                                  |
| `temperature`                   | number         | 模型采样温度，取值范围 \[0, 2]。                                                                                     |
| `user_system_prompt`            | string         | 自定义系统提示词（chat 场景），放在 agent\_config 顶层，透传不校验，长度/生效由下游运行时决定。注意字段名是 user\_system\_prompt 而非 system\_prompt。 |
| `anti_leak_prompt`              | string         | 防泄漏提示词。                                                                                                  |
| `refusal_prompt`                | string         | 拒答提示词。                                                                                                   |
| `credibility_prompt`            | string         | 可信度提示词。                                                                                                  |
| `enable_thinking`               | boolean        | 是否开启思考过程。                                                                                                |
| `enable_temperature`            | boolean        | 是否启用自定义温度参数。                                                                                             |
| `enable_credibility`            | boolean        | 是否开启可信度校验。                                                                                               |
| `enable_max_completion_tokens`  | boolean        | 是否启用最大输出 token 限制。                                                                                       |
| `session_file_parse_mode`       | string         | 会话文件解析模式。                                                                                                |

<strong>agent\_config.kb\_search\_configs\[] 元素</strong>

| 字段                        | 类型      | 说明                                               |
| ------------------------- | ------- | ------------------------------------------------ |
| `credibility_level`       | string  | 知识库可信度等级：`low`、`medium`、`high`。不传时默认回填 `medium`。 |
| `dense_similarity_top_k`  | integer | 稠密检索返回的 Top-K 数量，范围 \[1, 100]。                   |
| `id`                      | string  | 关联的知识库 Pipeline ID。                              |
| `rerank`                  | object  | 重排序配置。子字段见下表。                                    |
| `rerank_min_score`        | number  | 重排序最低分数阈值，范围 \[0, 1]，分数低于此值的切片会被过滤。              |
| `rerank_top_n`            | integer | 重排序后保留的 Top-N 数量，范围 \[1, 20]。                    |
| `search_filters`          | array   | 检索过滤条件，按知识库 schema 校验。                           |
| `sparse_similarity_top_k` | integer | 稀疏检索 Top K，范围 \[0, 100]。                         |
| `weight`                  | number  | 知识库权重。                                           |
| `name`                    | string  | 知识库名称。                                           |
| `desc`                    | string  | 知识库描述。                                           |
| `enable_rewrite`          | boolean | 该知识库是否开启查询改写。                                    |

<strong>kb\_search\_configs\[].rerank 子字段</strong>

| 字段                | 类型     | 说明                                                                                          |
| ----------------- | ------ | ------------------------------------------------------------------------------------------- |
| `model_name`      | string | 重排序模型名称，如 `gte-rerank-hybrid`。                                                              |
| `rerank_instruct` | string | 自定义重排序指令，仅在 `rerank_mode` 为 `custom` 时生效，最长 500 字符。                                         |
| `rerank_mode`     | string | 重排序模式：`qa`（问答匹配）、`similar`（相似度匹配）或 `custom`（自定义指令）。非 `custom` 模式下 `rerank_instruct` 会被自动清空。 |

<strong>agent\_config.hybrid\_rerank 子字段</strong>

| 字段                | 类型     | 说明                                                                      |
| ----------------- | ------ | ----------------------------------------------------------------------- |
| `model_name`      | string | 混排模型代码。含 VL 知识库时仅限多模态模型；其他情况支持全部排序模型。                                   |
| `rerank_mode`     | string | 混排模式：`qa`、`similar` 或 `custom`。非 `custom` 模式下 `rerank_instruct` 会被自动清空。 |
| `rerank_instruct` | string | 自定义混排指令，仅在 `rerank_mode` 为 `custom` 时生效，最长 500 字符。                      |

## 错误码 <span id="h-ragapi-get-err" />

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
