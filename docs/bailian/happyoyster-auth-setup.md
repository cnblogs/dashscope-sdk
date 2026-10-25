> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取鉴权凭证

> 获取 HappyOyster 服务端与客户端所需的鉴权凭证：API Host、主 API Key、临时 API Key、ticket。

<meta property="og:title" content="获取鉴权凭证" />

关于 HappyOyster 的整体架构，请参见[概述](/zh/model-studio/happyoyster-overview)。

## 鉴权凭证 <span id="ho-auth-cred" />

所有接口通过阿里云百炼平台网关鉴权，涉及以下三种凭证：

<table><colgroup><col style={{ width: "19.02%" }} /><col style={{ width: "19.26%" }} /><col style={{ width: "40.25%" }} /><col style={{ width: "21.47%" }} /></colgroup><thead><tr><th><p><strong>凭证</strong></p></th><th><p><strong>使用方</strong></p></th><th><p><strong>用途</strong></p></th><th><p><strong>有效期</strong></p></th></tr></thead><tbody><tr><td><p>主 API Key</p></td><td><p>仅服务端</p></td><td><p>调用所有 Open API（管理世界、换取 ticket、生成临时 API Key、查询产物）</p></td><td><p>长期</p></td></tr><tr><td><p>临时 API Key<br />（SDK 中又称 <code>token</code>）</p></td><td><p>客户端 SDK</p></td><td><p>SDK HTTP 层鉴权（通过 <code>updateToken</code> 注入），过期后重新获取并注入即可</p></td><td><p>默认 1 分钟，最长 30 分钟（通过 <code>expire\_in\_seconds</code> 配置），需续期</p></td></tr><tr><td><p>ticket</p></td><td><p>客户端 SDK</p></td><td><p>仅用于单次进入房间凭证，由服务端换取后下发给客户端</p></td><td><p>30 分钟，一次性</p></td></tr></tbody></table>

凭证流转关系：

- 主 API Key 仅限服务端持有，客户端永不接触。
- 服务端用主 API Key 生成临时 API Key 和 ticket，下发给客户端。
- 客户端通过 `updateToken` 注入临时 API Key（token）。过期时向您的服务端重新请求并再次注入，无需重新换取 `ticket`。
- ticket 为一次性进房凭证，有效期 30 分钟，使用后即失效。每次开始新的 Travel 需重新换取。

## 获取鉴权凭证 <span id="ho-auth-get" />

API Host 与主 API Key 均从百炼控制台获取，临时 API Key 与 `ticket` 由您的服务端调用接口签发后下发给客户端。

<Tip>
  主 API Key 仅在服务端使用，客户端 SDK 只使用临时 API Key，切勿将主 API Key 下发到客户端或打包进 App 分发。
</Tip>

<Note>
  API Host 与 API Key 必须属同一业务空间，否则返回 <code>AccessDenied</code>。
</Note>

### 1. 获取 API Host <span id="ho-auth-host" />

在[业务空间管理](https://bailian.console.aliyun.com/cn-beijing?tab=globalset#/efm/business_management)页面，复制 <strong>API Host</strong> 列的内容。服务端与客户端 SDK 都需配置该 API Host。

### 2. 获取主 API Key <span id="ho-auth-master-key" />

先[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置 API Key 到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。主 API Key 仅存储在您的服务端，不得下发到客户端或打包进 App 分发。

### 3. 获取临时 API Key <span id="ho-auth-temp-key" />

服务端使用主 API Key 调用[生成临时 API Key](/zh/model-studio/generate-temporary-api-key)接口，将返回的临时 API Key 下发给客户端 SDK，通过 `updateToken` 注入。过期后重新生成并再次注入即可，无需重新换取 `ticket`。

### 4. 获取 ticket <span id="ho-auth-ticket" />

服务端使用主 API Key，根据世界模式调用对应的获取体验凭证接口，取得一次性进房凭证 `ticket` 后下发给客户端：

- 世界探索（Adventure）：[HappyOyster-Adventure-获取体验凭证](/zh/model-studio/happyoyster-adventure-get-travel-credential-api-reference)
- 实时导演（Directing）：[HappyOyster-Directing-获取体验凭证](/zh/model-studio/happyoyster-directing-get-travel-credential-api-reference)
- 角色演绎（Acting）：[HappyOyster-Acting-获取体验凭证](/zh/model-studio/happyoyster-acting-get-travel-credential-api-reference)

`ticket` 与 Travel 一一绑定，30 分钟内一次性使用；每次开始新的 Travel 需重新换取。

## 下一步 <span id="ho-auth-next" />

- [接入流程](/zh/model-studio/happyoyster-integration-flow)：服务端准备世界、下发凭证、获取产物；客户端 SDK 完成实时体验。
