> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# HappyOyster 概述

> HappyOyster 是实时交互的开放式世界模型。输入一段自然语言 Prompt 和一张首帧图，即可生成一个可实时演绎、探索、互动的数字世界，输出为可进房的实时视频流。适用于互动剧、影视预演、AI 陪伴、可玩世界等场景。

<meta property="og:title" content="HappyOyster 概述" />

模型效果展示和提示词编写参见[HappyOyster 使用指南](/zh/model-studio/happyoyster-guide)。

## 简介 <span id="ho-overview-intro" />

HappyOyster 提供三种体验模式，各自独立部署，覆盖不同业务场景：

| **模式**          | **输入**                                     | **交互方式**               |
| --------------- | ------------------------------------------ | ---------------------- |
| 世界探索（Adventure） | Prompt + 首帧图（横屏）                           | 方向 / 视角 / 动作指令         |
| 实时导演（Directing） | Prompt 或结构化剧本 + 首帧图（横屏），可选参考图（用于剧本生成与角色参考） | 文本指令 / 剧本列表；支持暂停、回溯、恢复 |
| 角色演绎（Acting）    | Prompt + 首帧图（默认竖屏 9:16，也支持 16:9）           | 文本指令；支持暂停、恢复；**不支持回溯** |

## 整体架构 <span id="ho-overview-architecture" />

HappyOyster 采用<strong>服务端 + 客户端</strong>分离的集成方式：

- <strong>您的服务端</strong>通过 HappyOyster Open API（使用主 API Key，标准 HTTPS REST）管理世界的全生命周期，包括创建 / 管理世界、换取凭证、查询历史与产物。Open API 按体验模式拆分为 Adventure / Directing / Acting 三套独立接口。
- <strong>您的客户端</strong>通过 HappyOyster SDK（使用临时 API Key + ticket，走 RTC 实时音视频通道）进行实时体验，覆盖 Android、iOS、Web 三端。SDK 封装了 RTC 建连、视频播放、状态轮询与交互指令，无需直接对接底层实时通信协议。

<Image src="https://g-adoc.alcasset.com/media/maas_docs/sfm/common/images/6a4b3c2d1e0f9fa8.png" alt="image" width={900} />

所有接口通过阿里云百炼平台网关鉴权，凭证体系及获取方式详见[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。

## Open API 与 SDK <span id="ho-overview-openapi-sdk" />

### 分工概览 <span id="ho-overview-responsibility" />

| **维度** | **服务端 HappyOyster Open API**            | **客户端 HappyOyster SDK**            |
| ------ | --------------------------------------- | ---------------------------------- |
| 调用方    | 您的后端服务                                  | 您的 App 或 Web 前端                    |
| 鉴权凭证   | 主 API Key（长期有效，仅服务端持有）                  | 临时 API Key（token）+ 一次性 ticket（短时效） |
| 核心职责   | 世界管理（创建、状态轮询、查询、删除）、凭证换取、Travel 控制、产物查询 | RTC 建连与视频渲染、实时互动指令、过程控制、状态回调       |
| 通信方式   | 标准 HTTPS REST 请求                        | RTC 实时音视频通道（SDK 内部封装）              |
| 适用平台   | 任意后端语言（Python、Java、Node.js 等）           | Android、iOS、Web                    |

### 能力矩阵 <span id="ho-overview-capability" />

| **能力**           | **Open API（服务端）**                                                                      | **SDK（客户端）**                                           |
| ---------------- | -------------------------------------------------------------------------------------- | ------------------------------------------------------ |
| 创建 / 管理 World    | 支持                                                                                     | 不支持                                                    |
| 轮询世界构建状态         | 支持                                                                                     | 不支持                                                    |
| 换取 ticket        | 支持                                                                                     | 不支持（消费 ticket）                                         |
| 注入 HTTP 鉴权 token | 不支持                                                                                    | 支持（`updateToken`）                                      |
| 进房 + RTC 建连      | 支持（SDK 内部调用）                                                                           | 支持（Travel 启动，SDK 内部封装）                                 |
| 实时视频播放           | 不支持                                                                                    | 支持（挂载 SDK 提供的视频视图；Acting 按回包 `aspectRatio` 定竖 / 横屏）    |
| 状态轮询             | 支持（SDK 内部调用）                                                                           | 支持（状态回调透出）                                             |
| 实时导演 / 角色演绎文本指令  | 支持（`instruct`）                                                                         | 支持（`sendInstruct`）                                     |
| 世界探索操控指令         | 不支持                                                                                    | 支持（`sendCommand`；Acting 不可用）                           |
| 暂停 / 恢复          | 支持                                                                                     | 支持（Directing 与 Acting；Adventure 调用被 SDK 以 `103003` 拒绝） |
| 回溯               | 支持（`rewind`；仅 Directing）                                                               | 支持（仅 Directing；其他模式以 `103003` 拒绝）                      |
| 结束体验             | 支持                                                                                     | 支持（Travel 结束，SDK 内部封装）                                 |
| 更新剧本（ScriptList） | 支持（`update-script`；仅 Directing `scriptlist`。Acting 与 Directing `simple` 调用返回 `409000`） | 不支持                                                    |
| 查询历史 Travel      | 支持                                                                                     | 不支持                                                    |
| 获取视频产物           | 支持                                                                                     | 不支持                                                    |

<Note>
  SDK <strong>不负责</strong>世界的创建与管理；实时导演的剧本（Script List）模式仅在服务端接入，SDK 仅参与推流、播放与文本指令输入。
</Note>

## 适用场景 <span id="ho-overview-scenario" />

| **场景**       | **推荐模式**        | **服务端关键 API**                                    | **客户端关键 SDK 能力**               |
| ------------ | --------------- | ------------------------------------------------ | ------------------------------ |
| 互动游戏 / 可玩世界  | 世界探索（Adventure） | 创建世界 → 凭证换取                                      | `sendCommand` + 状态回调           |
| AI 陪伴 / 虚拟导游 | 世界探索（Adventure） | 首帧图 + prompt 创建                                  | 实时体验 + 视频 View                 |
| 互动短剧 / 影视预演  | 实时导演(Directing) | `simple` prompt 或 `scriptlist` 结构化剧本             | `sendInstruct` + 暂停 / 回溯       |
| 视频通话 / 竖屏陪伴  | 角色演绎(Acting)    | 必填 `prompt` + `firstFrameImage`，可选 `aspectRatio` | `sendInstruct` + 暂停 / 恢复（不含回溯） |
| 内容平台 / 二创    | 实时导演(Directing) | 结束后 `artifacts` 导出                               | 体验 + 服务端取产物                    |
| 教育模拟         | 世界探索(Adventure) | 首帧图 + prompt 搭建场景                                | 快速进房体验                         |

## 使用限制 <span id="ho-overview-limits" />

- <strong>画幅规则</strong>：
  - 世界探索（Adventure）：必须上传首帧图，视频画幅按首帧图比例。
  - 实时导演（Directing）：`simple` 子模式可选上传首帧图，`scriptlist` 子模式必填首帧图；上传首帧图时须为横屏（宽高比 1.5–2.0），画幅按首帧图；创建时传入的 `aspectRatio` 会被忽略。
  - 角色演绎（Acting）：必须上传首帧图；画幅由创建时的 `aspectRatio` 控制，缺省 `9:16`（竖屏），可显式传 `16:9`。进房 / 世界详情会回显该字段，客户端应据此设置播放器方向。首帧宽高比须与目标画幅匹配，否则返回 `400000`。
- <strong>跨模型访问</strong>：World 与 Travel 严格属于其创建模型，跨模型访问会返回 `403001`（world）或 `404000`（travel）。
- <strong>模式差异</strong>：角色演绎（Acting）不支持回溯（`rewind`）与 `sendCommand`；世界探索（Adventure）调用暂停 / 恢复会返回 `103003`。

## 术语速查 <span id="ho-overview-concepts" />

- **World（世界）**：一个完整的数字世界定义，包含角色、场景和剧本。World 可预制、可复用，是所有体验的基础。
- **Travel（体验）**：基于某个 World 发起的一次实时体验会话，通常经历「初始化 → 准备 → 运行 →（可暂停 / 回溯）→ 结束」几个阶段，各端具体状态取值请以对应 SDK API 参考为准。
- **ticket**：一次性进房凭证，由服务端换取后下发给客户端。
- **token**（临时 API Key）：客户端 SDK 的 HTTP 层鉴权凭证，由服务端签发后注入 SDK，需定期续期。

## 模型用量查询 <span id="ho-overview-usage" />

当前控制台“模型用量”模块暂不支持世界模型的用量统计，请通过[账单](https://billing-cost.console.aliyun.com/finance/expense-report/expense-detail-by-instance?StatisticItem=DEFAULT_CHARGE_ITEM\&PipCode%5BfilterMode%5D=IN\&PipCode%5Bvalues%5D=sfm\&CommodityCode%5BfilterMode%5D=IN\&CommodityCode%5Bvalues%5D=sfm_inference_public_cn\&StatisticCycle=HOURLY)查看。

## 下一步 <span id="ho-overview-next" />

- [快速开始](/zh/model-studio/happyoyster-quick-start)：完成端到端接入流程。

  - [获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)
  - [接入流程](/zh/model-studio/happyoyster-integration-flow)
  - [下载 SDK 与 Demo](/zh/model-studio/happyoyster-sdk-and-demo)
