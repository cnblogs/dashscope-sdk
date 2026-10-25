> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取组织信息

> 获取指定组织的信息

<meta property="og:title" content="获取组织信息" />

## 前提条件 <span id="tpba43b7a3a5" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用本接口所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

## 请求说明 <span id="tp221e1cb4f4" />

- <strong>HTTP 方法</strong>：GET

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>GET [https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization](https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口是阿里云 OpenAPI，采用 AccessKey 签名（签名算法 `ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer {API_KEY}` 方式。请求需携带公共请求头 `x-acs-action: GetOrganization`、`x-acs-version: 2026-02-10`、`x-acs-date`、`x-acs-content-sha256`、`x-acs-signature-nonce` 以及 `Authorization`。

  推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/api/ModelStudio/2026-02-10/GetOrganization) 发起调用，可免去自行计算签名。

通过 OrgId，获取指定组织的信息

## 请求参数 <span id="tp7ca7dec30f" />

<p>该接口无请求参数。</p>

## 返回参数 <span id="tpbebc77d941" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>调用接口是否成功：<br />true：成功<br />false：失败</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应状态码。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应信息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>业务数据</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>OrgId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>NamespaceId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>产品命名空间 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Name</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织名称</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Description</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织描述</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Status</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>状态（ACTIVE/FROZEN）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>OwnerId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织 Owner 的 UAC 账号 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>OwnerBizAccountId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织 Owner 的业务账号标识（ALIYUN 类型为 aliyunUid，SSO 类型为 userIdentifier）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>GmtCreate</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>创建时间</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>GmtModified</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>修改时间</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>HttpStatusCode</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>HTTP 状态码</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>RequestId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>请求唯一标识</p></td></tr></tbody></table>

## 请求示例 <span id="tp4e614553dc" />

本接口使用阿里云 OpenAPI 签名，下例中 `${SIGNATURE}` 需按 `ACS3-HMAC-SHA256` 算法计算得出，`x-acs-date`、`x-acs-signature-nonce` 需替换为实际值。

```bash
curl -X GET "https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization" \
    --header "x-acs-action: GetOrganization" \
    --header "x-acs-version: 2026-02-10" \
    --header "x-acs-date: 2026-01-01T12:00:00Z" \
    --header "x-acs-content-sha256: e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" \
    --header "x-acs-signature-nonce: ${SIGNATURE_NONCE}" \
    --header "Authorization: ACS3-HMAC-SHA256 Credential=${ALIBABA_CLOUD_ACCESS_KEY_ID},SignedHeaders=host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version,Signature=${SIGNATURE}"
```

## 返回示例 <span id="tpa2d91d61a0" />

```json expandable
{
  "Success": true,
  "Data": {
    "OrgId": "org_123456789",
    "NamespaceId": "namespace-1",
    "Name": "组织名称",
    "Description": "组织描述",
    "Status": "ACTIVE",
    "OwnerId": "acc_123456789",
    "OwnerBizAccountId": "1543686331379464",
    "GmtCreate": "2025-11-20T02:26:35Z",
    "GmtModified": "2025-11-13T02:11:56Z"
  }
}
```

## 错误码 <span id="tp6672b297d6" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
