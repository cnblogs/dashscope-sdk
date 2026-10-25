> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Agent 管理概述

> RAG Agent 实例的全生命周期管理 API。

<meta property="og:title" content="Agent 管理概述" />

RAG Agent 是[知识检索](/zh/model-studio/rag-knowledge-retrieval)（[`knowledge/search`](/zh/model-studio/knowledgesearch)）和[知识问答](/zh/model-studio/rag-knowledge-qa)（[`knowledge/chat`](/zh/model-studio/knowledgechat)）服务的运行主体，`agent_config` 承载检索策略。Agent 管理 API 支持通过代码完成 Agent 的创建、配置、发布、查询和删除，无需前往控制台手动操作。

## 和运行时 API 的关系 <span id="h-ragapi-overview-runtime" />

```text
管理 API（本组）                              运行时 API
┌───────────────────────────┐
│ create / update / deploy  │── 产出 agent_id ─▶ knowledge/search
│ list / get                │── 产出 agent_id ─▶ knowledge/chat
│ copy / delete             │
└───────────────────────────┘
```

先用管理 API 创建并发布 Agent，获取 `agent_id` 后，再用运行时 API 发起检索或问答。

## 生命周期 <span id="h-ragapi-overview-lifecycle" />

```text
create
  └─▶ draft / beta
        ├─▶ update（编辑草稿配置）
        ├─▶ deploy ─▶ deployed（版本号自增）
        │             ├─ 仅改描述 ─▶ update（仅修改版本描述）
        │             ├─ 改配置 ─▶ update（编辑草稿配置）─▶ deploy 发布新版本
        │             ├─ copy ─▶ 新 Agent（draft / beta）
        │             └─ delete ─▶ deleted
        └─▶ delete ─▶ deleted
```

<Note>
  `agent_config` 仅支持修改 `beta` 草稿版本。已发布版本仅能修改 `agent_version_desc` 字段；如需更新配置，须先通过 update 接口更新草稿，再通过 deploy 接口发布新版本。删除为软删除，接口幂等：重复删除不会报错。
</Note>

## API 列表 <span id="h-ragapi-overview-apis" />

所有端点均为 POST + JSON body，路径前缀 `/api/v1/indices/rag/app/`。

| 操作                                                | 端点                 | 说明                     |
| ------------------------------------------------- | ------------------ | ---------------------- |
| [创建 Agent](/zh/model-studio/rag-api-agent-create) | `POST /app/create` | 创建新 Agent，初始状态 `draft` |
| [更新 Agent](/zh/model-studio/rag-api-agent-update) | `POST /app/update` | 更新名称、描述或草稿配置           |
| [发布 Agent](/zh/model-studio/rag-api-agent-deploy) | `POST /app/deploy` | 将 beta 草稿发布为新版本        |
| [删除 Agent](/zh/model-studio/rag-api-agent-delete) | `POST /app/delete` | 软删除，接口幂等               |
| [查询列表](/zh/model-studio/rag-api-agent-list)       | `POST /app/list`   | 分页查询当前租户下的 Agent       |
| [查询详情](/zh/model-studio/rag-api-agent-get)        | `POST /app/get`    | 获取 Agent 完整信息，支持指定版本   |
| [复制 Agent](/zh/model-studio/rag-api-agent-copy)   | `POST /app/copy`   | 复制已有 Agent，生成新的草稿      |

## 场景 <span id="h-ragapi-overview-scene" />

Agent 分为两种场景（`agent_scene`）：

| 场景   | 值        | 用途              | 对应运行时 API                                              |
| ---- | -------- | --------------- | ------------------------------------------------------ |
| 知识问答 | `chat`   | 配置问答模型、引用、拒答等   | [`knowledge/chat`](/zh/model-studio/knowledgechat)     |
| 知识检索 | `search` | 配置多库路由、混排、检索参数等 | [`knowledge/search`](/zh/model-studio/knowledgesearch) |

两种场景的 `agent_config` 结构不同，详见各接口文档。

<Note>
  对外使用 `chat`（内部映射为 `general`）；`search` 内外一致。
</Note>

## 权限要求 <span id="h-ragapi-overview-permission" />

写操作（create / update / deploy / delete / copy）需要对应的知识库权限，由[业务空间成员管理](https://bailian.console.aliyun.com/?tab=globalset#/efm/business_management)中的角色决定：

| 操作     | 需要的权限  |
| ------ | ------ |
| create | 知识库-创建 |
| update | 知识库-修改 |
| deploy | 知识库-修改 |
| delete | 知识库-删除 |
| copy   | 知识库-创建 |

拥有`知识库-操作(FullAccess)`权限可执行以上所有操作。调用返回 403 时，说明当前用户缺少对应权限，请联系业务空间管理员调整角色。读操作（list / get）仅需 API Key。
