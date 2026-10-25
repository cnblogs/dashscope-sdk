> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取成员与席位统计

> 查询组织成员统计信息，包含成员总数、管理员数、普通成员数、已分配席位成员数、未分配席位成员数。

<meta property="og:title" content="获取成员与席位统计" />

## 前提条件 <span id="tpbb6827e85a" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用本接口所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

## 请求说明 <span id="tpddac43fea6" />

- <strong>HTTP 方法</strong>：GET

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>GET [https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization/member-seat-stats](https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization/member-seat-stats)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口是阿里云 OpenAPI，采用 AccessKey 签名（签名算法 `ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer {API_KEY}` 方式。请求需携带公共请求头 `x-acs-action: GetOrganizationMemberSeatStats`、`x-acs-version: 2026-02-10`、`x-acs-date`、`x-acs-content-sha256`、`x-acs-signature-nonce` 以及 `Authorization`。

  推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/api/ModelStudio/2026-02-10/GetOrganizationMemberSeatStats) 发起调用，可免去自行计算签名。

## 请求参数 <span id="tpf56162fb67" />

<p>该接口无请求参数。</p>

## 返回参数 <span id="tp8df6d1e902" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>OrgId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>TotalMemberCount</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>成员总数</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>OwnerRoleUserCount</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>主账号数（ORG\_OWNER 角色）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AdminRoleUserCount</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>管理员数（ORG\_ADMIN 角色）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MemberRoleUserCount</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>普通成员数（ORG\_MEMBER 角色）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SeatedMemberCount</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>已分配席位成员数</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>UnseatedMemberCount</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>未分配席位成员数</p></td></tr></tbody></table>

## 请求示例 <span id="tp6aedeab040" />

本接口使用阿里云 OpenAPI 签名，下例中 `${SIGNATURE}` 需按 `ACS3-HMAC-SHA256` 算法计算得出，`x-acs-date`、`x-acs-signature-nonce` 需替换为实际值。

```bash
curl -X GET "https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization/member-seat-stats" \
    --header "x-acs-action: GetOrganizationMemberSeatStats" \
    --header "x-acs-version: 2026-02-10" \
    --header "x-acs-date: 2026-01-01T12:00:00Z" \
    --header "x-acs-content-sha256: e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" \
    --header "x-acs-signature-nonce: ${SIGNATURE_NONCE}" \
    --header "Authorization: ACS3-HMAC-SHA256 Credential=${ALIBABA_CLOUD_ACCESS_KEY_ID},SignedHeaders=host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version,Signature=${SIGNATURE}"
```

## 返回示例 <span id="tp856445eea3" />

```json expandable
{
  "OrgId": "5ffd468b1e45db3c1cc26ad6",
  "TotalMemberCount": 12,
  "OwnerRoleUserCount": 1,
  "AdminRoleUserCount": 3,
  "MemberRoleUserCount": 8,
  "SeatedMemberCount": 2,
  "UnseatedMemberCount": 10
}
```

## 错误码 <span id="tpdc99b51a5f" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
