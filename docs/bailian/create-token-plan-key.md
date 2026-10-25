> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 创建 API Key

> 创建 UAC API Key

<meta property="og:title" content="创建 API Key" />

## 前提条件 <span id="tp641fff3c06" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用本接口所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

## 请求说明 <span id="tpda2c918e9e" />

- <strong>HTTP 方法</strong>：POST

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>POST [https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/api-keys](https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/api-keys)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口是阿里云 OpenAPI，采用 AccessKey 签名（签名算法 `ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer {API_KEY}` 方式。请求需携带公共请求头 `x-acs-action: CreateTokenPlanKey`、`x-acs-version: 2026-02-10`、`x-acs-date`、`x-acs-content-sha256`、`x-acs-signature-nonce` 以及 `Authorization`。

  推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/api/ModelStudio/2026-02-10/CreateTokenPlanKey) 发起调用，可免去自行计算签名。

## 请求参数 <span id="tp3a89dd91f3" />

所有参数均通过 Query String 传递。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "22%" }} /><col style={{ width: "12%" }} /><col style={{ width: "10%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>AccountId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>账号 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Description</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>Key 描述</p></td></tr></tbody></table>

## 返回参数 <span id="tpbca07af3f9" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>是否成功</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>错误码（成功时为空）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>错误信息（成功时为空）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>业务数据</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>ApiKeyId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>API Key ID（系统生成）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SourceId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>BaiLian 返回的 source\_id</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PlainApiKey</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>BaiLian 返回的明文 API Key（只在创建时返回一次）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MaskedApiKey</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>BaiLian 返回的 masked\_api\_key（如 sk\_\*\*\*cdef）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Description</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>Key 描述</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CreatedAt</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>Key 创建时间</p></td></tr></tbody></table>

## 请求示例 <span id="tpf4db855648" />

本接口使用阿里云 OpenAPI 签名，下例中 `${SIGNATURE}` 需按 `ACS3-HMAC-SHA256` 算法计算得出，`x-acs-date`、`x-acs-signature-nonce` 需替换为实际值。示例 URL 中的中文取值为便于阅读未做编码；实际计算签名时，规范化请求（CanonicalRequest）的 Query String 需按 RFC 3986 对键和取值做百分号编码，否则会返回 `SignatureDoesNotMatch`。使用阿里云 SDK 时会自动处理。

```bash
curl -X POST "https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/api-keys?AccountId=acc_123456789&Description=APIKEY描述" \
    --header "x-acs-action: CreateTokenPlanKey" \
    --header "x-acs-version: 2026-02-10" \
    --header "x-acs-date: 2026-01-01T12:00:00Z" \
    --header "x-acs-content-sha256: e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" \
    --header "x-acs-signature-nonce: ${SIGNATURE_NONCE}" \
    --header "Authorization: ACS3-HMAC-SHA256 Credential=${ALIBABA_CLOUD_ACCESS_KEY_ID},SignedHeaders=host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version,Signature=${SIGNATURE}"
```

## 返回示例 <span id="tp0e80d0b394" />

```json expandable
{
  "Success": true,
  "Data": {
    "ApiKeyId": "ak-123456",
    "SourceId": "123",
    "PlainApiKey": "sk-ws.abc123456",
    "MaskedApiKey": "sk_***6",
    "Description": "APIKEY描述",
    "CreatedAt": "2025-07-18T03:19:17Z"
  }
}
```

## 错误码 <span id="tp7b8b488425" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
