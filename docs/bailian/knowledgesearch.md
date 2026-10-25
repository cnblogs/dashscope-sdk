> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 知识检索

> 跨多个知识库执行联合语义检索，返回按相关性排序的切片。

<meta property="og:title" content="知识检索" />

## 接口说明 <span id="section-interface-description-title" />

- <strong>权限要求</strong>：调用本接口需提供阿里云百炼 API Key及业务空间。在控制台 [API Key 页面](https://bailian.console.aliyun.com/?tab=model#/api-key)及[业务空间管理](https://bailian.console.aliyun.com/cn-beijing?tab=globalset#/efm/business_management)获取。
- <strong>调用方式</strong>：HTTP REST，`POST` + `application/json`。Base URL 为 `https://{workspaceId}.cn-beijing.maas.aliyuncs.com`，其中 `{workspaceId}` 为业务空间 ID。
- <strong>前置条件</strong>：调用前须在百炼控制台 [知识检索服务页面](https://bailian.console.aliyun.com/cn-beijing/rag/retrieval/list) 创建并发布[知识检索](/zh/model-studio/rag-knowledge-retrieval)服务，获取服务 ID（`agent_id`）。检索策略（多库权重、知识路由、混排模型、混排模型模式等）预先在控制台配置进服务实例并发布，调用方只需传入检索意图（`query` / `images`）与 `agent_id`，还可通过 `kb_search_configs` 为每个知识库指定文档/标签过滤条件。未发布时返回 Agent 未发布错误。
- <strong>限流</strong>：默认用户维度 25 QPS。如遇限流，请稍后重试。

## 请求语法 <span id="section-request-syntax-title" />

```plaintext
POST /api/v1/indices/knowledge/search HTTP/1.1
Host: {workspaceId}.cn-beijing.maas.aliyuncs.com
Authorization: Bearer <API-Key>
Content-Type: application/json
```

## 请求参数 <span id="section-request-parameters-title" />

调用方仅需传入检索意图、`agent_id` 与可选的 `kb_search_configs` 过滤条件，其余检索策略配置在 `agent_config`，不在请求中暴露。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "40.47%" }} /><col style={{ width: "14.23%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>名称</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>必填</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th><th style={{ verticalAlign: "top" }}><p>示例</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>agent\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>知识检索服务（agent）实例 ID。服务端据此加载已发布的 <code>agent\_config</code>（含全部检索策略）。在控制台知识检索页面创建并发布后获取。</p></td><td style={{ verticalAlign: "top" }}><p>aid-xxxxxxxxxxxxxxxx</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>query</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>条件必填</p></td><td style={{ verticalAlign: "top" }}><p>文本检索意图。与 <code>images</code> 至少传入一个；纯图搜时可传空串；非纯图搜场景（未传 <code>images</code>）下必填，否则返回 <code>InvalidParameter</code>。</p></td><td style={{ verticalAlign: "top" }}><p>推荐一件适合秋冬的运动夹克</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>images</p></td><td style={{ verticalAlign: "top" }}><p>array\<string></p></td><td style={{ verticalAlign: "top" }}><p>条件必填</p></td><td style={{ verticalAlign: "top" }}><p>图片检索意图，元素为图片 URL（须公网可访问）。与 <code>query</code> 至少传入一个，可同时传入实现多模态检索。</p></td><td style={{ verticalAlign: "top" }}><p>\[]</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>agent\_version</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>agent 版本。标准接口仅暴露 <code>agent\_id</code> / <code>agent\_version</code> / <code>query</code> / <code>images</code> / <code>kb\_search\_configs</code>，其余检索策略一律走 <code>agent\_config</code>，不在请求中暴露。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>kb\_search\_configs</p></td><td style={{ verticalAlign: "top" }}><p>array\<object></p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>知识库过滤配置数组。每个元素对应一个知识库，可指定文档/标签过滤条件。<code>id</code> 必须属于 agent 已绑定的知识库，同一请求中不可重复。</p></td><td style={{ verticalAlign: "top" }} /></tr><tr><td style={{ verticalAlign: "top" }}><p>kb\_search\_configs\[].id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>知识库 ID（pipeline\_id），必须属于 agent 已绑定的知识库。</p></td><td style={{ verticalAlign: "top" }}><p>kb\_product\_001</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>kb\_search\_configs\[].search\_filters</p></td><td style={{ verticalAlign: "top" }}><p>array\<object></p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>过滤条件数组，每个元素是一个 JSON 对象。支持按 doc\_id、tags、自定义 meta 字段过滤。在线传入的 search\_filters 会追加到离线配置的 search\_filters 末尾（AND 关系），而非替换。总字节数不超过 80,000。非结构化知识库仅允许 tags 和 meta 两种 key，数组长度不超过 1,000。</p></td><td style={{ verticalAlign: "top" }} /></tr></tbody></table>

### kb\_search\_configs <span id="section-kb-search-configs" />

通过 `kb_search_configs` 可在请求中为每个知识库指定文档/标签过滤条件，实现运行时动态过滤。该字段为数组，每个元素对应一个知识库。

<Note>
  - `kb_search_configs[].id` 必须是 agent 离线配置中已绑定的知识库 ID，不能查询未绑定的知识库。
  - 同一请求中 `id` 不能重复。
  - 每个 `kb_search_configs` 元素必须包含 `id` 字段。
  - `search_filters` 总字节数不超过 80,000 bytes。非结构化知识库仅允许 `tags` 和 `meta` 两种 key，数组长度不超过 1,000。
  - 可过滤字段须在该知识库入卷时声明于 `index_config.fields`，否则过滤不生效。
</Note>

<Expandable title="search_filters 语法详情">
  `search_filters` 是一个 JSON 数组，每个元素是一个独立的过滤条件对象。支持三种过滤维度，每种维度用独立的 JSON 对象传入：

  ```json
  "search_filters": [
    // 按文档 ID 过滤（支持多值 IN）
    { "doc_id": "file_abc_123" },
    { "doc_id": ["file_abc_123", "file_def_456"] },

    // 按标签过滤
    { "tags": ["合同", "2024年"] },

    // 按自定义 Meta 字段过滤
    { "custom_field": "value" },

    // 高级：操作符（gte / lte / like 等）
    { "price": { "gte": 100, "lte": 500 } },
    { "name": { "like": "张%" } },

    // 高级：嵌套 OR 分组
    {
      "_operator": "OR",
      "_conditions": [
        { "status": "active" },
        { "priority": { "gte": 5 } }
      ]
    }
  ]
  ```

  **合并策略**：当 agent 离线配置中已有 `search_filters`，请求中传入的会追加到离线 filters 数组末尾，两者同时生效（AND 关系），而非替换。如果需要“只用在线条件”，离线配置中不要设置 search\_filters。
</Expandable>

## 响应参数 <span id="section-response-parameters-title" />

响应顶层是统一的结构，`data` 里包含三部分：`total` 是命中的切片总数，`nodes` 是结果列表，`cost_time` 是本次检索耗时（毫秒）。建议以 `success` 字段判断成功与否，用 `request_id` 关联日志排查。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "15%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>名称</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>业务状态码，<code>Success</code> 表示成功；失败时为对应错误码字符串。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>status\_code</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>语义状态码，成功为 200。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>status</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>状态枚举，成功 <code>SUCCESS</code>，失败 <code>ERROR</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>是否成功。<strong>调用方以此字段作为主判定。</strong></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>状态描述。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>request\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>请求唯一标识，排查问题时请提供此 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>data.total</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>命中结果总数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>data.nodes</p></td><td style={{ verticalAlign: "top" }}><p>array\<object></p></td><td style={{ verticalAlign: "top" }}><p>检索结果节点列表，按相关性排序。结构见下表。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>data.cost\_time</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>本次检索耗时（毫秒）。</p></td></tr></tbody></table>

<strong>data.nodes\[] 结构</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "15%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>名称</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>描述</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>score</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>切片与查询的相关性分数，0 到 1 之间，越大越相关。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>text</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>拼接好的切片文本，按 <code>字段名: 值</code> 逐行拼成，默认包含文档名、标题、正文。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>metadata</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>切片元数据，字段因知识库类型而异，见下表。</p></td></tr></tbody></table>

<strong>data.nodes\[].metadata 字段</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "28%" }} /><col style={{ width: "15%" }} /><col style={{ width: "57%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>字段</p></th><th style={{ verticalAlign: "top" }}><p>类型</p></th><th style={{ verticalAlign: "top" }}><p>说明</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>doc\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>文档/数据行 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>doc\_name</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>文档名称。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>title</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>切片标题。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>hier\_title</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>层级标题，按 <code>></code> 分隔。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>content</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>切片正文。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>image\_url</p></td><td style={{ verticalAlign: "top" }}><p>array\<string></p></td><td style={{ verticalAlign: "top" }}><p>切片包含的图片地址。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>audio\_url</p></td><td style={{ verticalAlign: "top" }}><p>array\<string></p></td><td style={{ verticalAlign: "top" }}><p>切片包含的音频地址。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>video\_url</p></td><td style={{ verticalAlign: "top" }}><p>array\<string></p></td><td style={{ verticalAlign: "top" }}><p>切片包含的视频地址。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_knowledge\_type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>知识类型，如 <code>document</code>、<code>image</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_knowledge\_scene</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>知识场景，如 <code>basic\_document\_qa</code>、<code>image\_qa</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>pipeline\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>所属知识库 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>workspace\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>所属业务空间 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>切片 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>nid</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>文档解析 layout ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>page\_number</p></td><td style={{ verticalAlign: "top" }}><p>array\<integer></p></td><td style={{ verticalAlign: "top" }}><p>切片所属页码列表。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>doc\_url</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>原始文档 URL。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_score</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>相关性分数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_rc\_v\_score</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>向量匹配分数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_rc\_t\_score</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>关键词匹配分数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_score\_with\_weight</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>加权相关性分数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_rank\_weight</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>排序权重。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>is\_displayed\_chunk\_content</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>该切片内容是否用于展示。</p></td></tr></tbody></table>

<Note>
  以 `_` 开头的字段是内部打分用，版本间可能变，不要写进业务逻辑。表格型知识库还会额外返回用户表格里的业务字段（字段名因表结构而异，比如商品库可能返回 `product_id`、`product_name`、`category` 等），这些字段不在上表中。
</Note>

## 错误码 <span id="section-error-codes-title" />

失败时 `success` 为 `false`、`status` 为 `ERROR`、`status_code` 为对应语义码、`code` 为错误码字符串、`message` 为描述，`request_id` 必返回。<strong>始终校验</strong>`success`<strong>，而非仅依赖 HTTP 状态码</strong>；排查问题提供 `request_id`。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "12%" }} /><col style={{ width: "28%" }} /><col style={{ width: "60%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p>HTTP 状态码</p></th><th style={{ verticalAlign: "top" }}><p>错误码</p></th><th style={{ verticalAlign: "top" }}><p>说明</p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>400</p></td><td style={{ verticalAlign: "top" }}><p>InvalidParameter</p></td><td style={{ verticalAlign: "top" }}><p>请求参数无效。多种参数错误都返回此码，仅凭错误码无法精准定位错误原因，请参照 <code>message</code> 字段，常见场景见下表。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>401</p></td><td style={{ verticalAlign: "top" }}><p>InvalidApiKey</p></td><td style={{ verticalAlign: "top" }}><p>鉴权失败，API Key 无效或缺失。</p></td></tr></tbody></table>

<strong>常见参数错误场景</strong>

| 场景                      | 错误码                | 错误信息                                                                                                         |
| ----------------------- | ------------------ | ------------------------------------------------------------------------------------------------------------ |
| `agent_id` 缺失           | `InvalidParameter` | `agent_id is required`                                                                                       |
| `query` 缺失              | `InvalidParameter` | `query is required`                                                                                          |
| kb\_id 不属于 agent 绑定的知识库 | `InvalidParameter` | `kb not bound to agent: {id}`                                                                                |
| kb\_id 重复               | `InvalidParameter` | `duplicate kb id in kb_search_configs: {id}`                                                                 |
| kb\_id 为空               | `InvalidParameter` | `kb_search_configs[{i}].id is required`                                                                      |
| search\_filters 总字节数超限  | `InvalidParameter` | `search_filters total bytes must less than 80000`                                                            |
| 非结构化 filter 包含非法 key    | `InvalidParameter` | `search_filters[{i}] contains invalid key '{key}'. Only 'tags' and 'meta' are allowed for unstructured data` |
| 非结构化 tags/meta 数组超限     | `InvalidParameter` | `search_filters[{i}].tags array size must not exceed 1000`                                                   |

## 示例 <span id="section-examples-title" />

以下示例查询「推荐一件适合秋冬的运动夹克」，`agent_id` 替换为已发布的知识检索服务 ID。

### cURL <span id="section-examples-curl" />

```bash
curl -X POST "https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/indices/knowledge/search" \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
    "agent_id": "aid-xxxxxxxxxxxxxxxx",
    "query": "推荐一件适合秋冬的运动夹克",
    "images": [],
    "kb_search_configs": [
      {
        "id": "kb_product_001",
        "search_filters": [
          {"tags": ["秋冬", "运动"]}
        ]
      }
    ]
  }'
```

### Python <span id="section-examples-python" />

```python
import os
import requests

resp = requests.post(
    "https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/indices/knowledge/search",
    headers={
        "Authorization": f"Bearer {os.getenv('DASHSCOPE_API_KEY')}",
        "Content-Type": "application/json",
    },
    json={
        "agent_id": "aid-xxxxxxxxxxxxxxxx",
        "query": "推荐一件适合秋冬的运动夹克",
        "kb_search_configs": [
            {
                "id": "kb_product_001",
                "search_filters": [
                    {"tags": ["秋冬", "运动"]}
                ],
            }
        ],
    },
)
print(resp.json())
```
