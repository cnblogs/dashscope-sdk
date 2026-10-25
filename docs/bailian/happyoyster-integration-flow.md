> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 接入流程

> 服务端调用 HappyOyster Open API 准备世界与产物导出，客户端 SDK 完成 RTC 建连与实时互动。

<meta property="og:title" content="接入流程" />

HappyOyster 采用<strong>服务端 + 客户端</strong>分离的接入方式：服务端通过 Open API 管理世界与产物，客户端 SDK 完成实时体验。开始前请先完成[获取鉴权凭证](/zh/model-studio/happyoyster-auth-setup)。

<Note>
  - 服务端 Open API 按体验模式拆分为 Adventure（世界探索）/ Directing（实时导演）/ Acting（角色演绎）三套，请根据业务选择对应模式的接口。
  - World 与 Travel 严格属于其创建模型，跨模型访问会返回 `403001`（world）或 `404000`（travel）。
  - SDK <strong>不负责</strong>世界的创建与管理，仅负责客户端实时体验。
</Note>

## 一、准备世界（服务端 Open API） <span id="ho-flow-phase-prepare" />

1. <strong>创建世界</strong>，按模式选择接口：[Adventure-创建世界](/zh/model-studio/happyoyster-adventure-create-world-api-reference) / [Directing-创建世界](/zh/model-studio/happyoyster-directing-create-world-api-reference) / [Acting-创建世界](/zh/model-studio/happyoyster-acting-create-world-api-reference)。
2. <strong>轮询构建状态</strong>（3–5 秒间隔）至 `ready`：[Adventure-查询世界构建状态](/zh/model-studio/happyoyster-adventure-query-world-build-status-api-reference) / [Directing-查询世界构建状态](/zh/model-studio/happyoyster-directing-query-world-build-status-api-reference) / [Acting-查询世界构建状态](/zh/model-studio/happyoyster-acting-query-world-build-status-api-reference)。
3. <strong>换取 ticket</strong>：使用主 API Key 调用 [Adventure-获取体验凭证](/zh/model-studio/happyoyster-adventure-get-travel-credential-api-reference) / [Directing-获取体验凭证](/zh/model-studio/happyoyster-directing-get-travel-credential-api-reference) / [Acting-获取体验凭证](/zh/model-studio/happyoyster-acting-get-travel-credential-api-reference)，获得一次性 `ticket`。
4. <strong>下发凭证给客户端</strong>：将 `ticket` 与[临时 API Key](/zh/model-studio/happyoyster-auth-setup#ho-auth-temp-key)（作为 SDK 的 `token`）一并下发。

## 二、实时体验（客户端 SDK） <span id="ho-flow-phase-experience" />

SDK 封装 RTC 建连、视频播放与交互指令。

1. <strong>初始化 SDK</strong>：`initialize` + `updateToken`，注入 API Host 与临时 API Key。
2. <strong>开始体验</strong>：`startTravel(ticket)`，SDK 自动进房并建连。
3. <strong>挂载视频</strong>：将 `attachVideo()` 返回的视图加入布局；Acting 依据进房回包的 `aspectRatio` 设置容器方向。
4. <strong>实时互动</strong>：

   - 世界探索（Adventure）：调用 `sendCommand` 发送方向 / 视角 / 动作指令。
   - 实时导演（Directing）：调用 `sendInstruct` 发送文本指令驱动剧情。
   - 角色演绎（Acting）：调用 `sendInstruct` 发送文本指令。
5. <strong>过程控制</strong>：

   - 世界探索（Adventure）：不支持暂停、回溯。
   - 实时导演（Directing）：支持 `pause`、`rewind`、`resume`。
   - 角色演绎（Acting）：支持 `pause`、`resume`，不支持 `rewind`。
6. <strong>结束体验</strong>：`endTravel`，释放 RTC 与资源。

## 三、获取产物（服务端 Open API） <span id="ho-flow-phase-artifact" />

Travel 结束后，使用主 API Key 轮询 [Adventure-查询Travel产物](/zh/model-studio/happyoyster-adventure-query-travel-artifacts-api-reference) / [Directing-查询Travel产物](/zh/model-studio/happyoyster-directing-query-travel-artifacts-api-reference) / [Acting-查询Travel产物](/zh/model-studio/happyoyster-acting-query-travel-artifacts-api-reference)，直至 `composeStatus = ready`。推荐对外交付 `video.withInstructionAndWatermark`（指令 + 水印合成版）。

## 下一步 <span id="ho-flow-next" />

- [下载 SDK 与 Demo](/zh/model-studio/happyoyster-sdk-and-demo)：各端 SDK 包、接入指南、API 参考与开源 Demo。
