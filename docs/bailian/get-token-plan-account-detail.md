> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取账号详情

> 获取TokenPlan账号详情及组织信息

<meta property="og:title" content="获取账号详情" />

## 前提条件 <span id="tpd8c7ec13af" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用本接口所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

## 请求说明 <span id="tpfecdfb73cd" />

- <strong>HTTP 方法</strong>：GET

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>GET [https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/account](https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/account)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口是阿里云 OpenAPI，采用 AccessKey 签名（签名算法 `ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer {API_KEY}` 方式。请求需携带公共请求头 `x-acs-action: GetTokenPlanAccountDetail`、`x-acs-version: 2026-02-10`、`x-acs-date`、`x-acs-content-sha256`、`x-acs-signature-nonce` 以及 `Authorization`。

  推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/api/ModelStudio/2026-02-10/GetTokenPlanAccountDetail) 发起调用，可免去自行计算签名。

在登录状态下，获取 TokenPlan 管理平台的账号信息

## 请求参数 <span id="tp18869f904b" />

<p>该接口无请求参数。</p>

## 返回参数 <span id="tp76888e64bf" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>调用接口是否成功：<br />true：成功<br />false：失败</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应状态码。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应信息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>业务数据</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AccountId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>账号 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AccountType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>账号类型：<br />ALIYUN<br />SSO<br />SA</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>IsDeleted</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>账号全局状态：0=正常，1=已冻结</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Name</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>账号显示名称</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AliyunUid</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>阿里云 UID（ALIYUN 类型账号）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Email</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>邮箱（SSO 类型账号）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CreatedAt</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>创建时间</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>OrgMemberships</p></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>组织成员关系列表（树形结构，组织 → 工作空间）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>OrgId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MemberStatus</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织成员状态<br />ACTIVE<br />INITIAL<br />FROZEN</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>RoleId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织角色 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>RoleCode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织角色编码<br />ORG\_OWNER<br />ORG\_ADMIN<br />ORG\_MEMBER</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Workspaces</p></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>该组织下账号已加入的工作空间列表</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>工作空间列表。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>WorkspaceId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>工作空间 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MemberStatus</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>成员状态<br />ACTIVE<br />FROZEN</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>RoleId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>工作空间角色 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>RoleCode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>工作空间角色编码<br />WS\_ADMIN<br />WS\_MEMBER</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>HttpStatusCode</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>HTTP 状态码</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>RequestId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>请求唯一标识</p></td></tr></tbody></table>

## 请求示例 <span id="tp042836b244" />

本接口使用阿里云 OpenAPI 签名，下例中 `${SIGNATURE}` 需按 `ACS3-HMAC-SHA256` 算法计算得出，`x-acs-date`、`x-acs-signature-nonce` 需替换为实际值。

```bash
curl -X GET "https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/account" \
    --header "x-acs-action: GetTokenPlanAccountDetail" \
    --header "x-acs-version: 2026-02-10" \
    --header "x-acs-date: 2026-01-01T12:00:00Z" \
    --header "x-acs-content-sha256: e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" \
    --header "x-acs-signature-nonce: ${SIGNATURE_NONCE}" \
    --header "Authorization: ACS3-HMAC-SHA256 Credential=${ALIBABA_CLOUD_ACCESS_KEY_ID},SignedHeaders=host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version,Signature=${SIGNATURE}"
```

## 返回示例 <span id="tpc58f9bfcab" />

```json expandable
{
  "Success": true,
  "Data": {
    "AccountId": "acc_123456789",
    "AccountType": "ALIYUN",
    "IsDeleted": true,
    "Name": "test_name",
    "AliyunUid": "1122334455",
    "Email": "test@email.com",
    "CreatedAt": "Thu May 28 14:33:52 CST 2026",
    "OrgMemberships": [
      {
        "OrgId": "org_123456789",
        "MemberStatus": "ENABLE",
        "RoleId": "SYSTEM_ROLE_ORG_OWNER",
        "RoleCode": "ORG_MEMBER",
        "Workspaces": [
          {
            "WorkspaceId": "ws_123456789",
            "MemberStatus": "ACTIVE",
            "RoleId": "SYSTEM_ROLE_WS_ADMIN",
            "RoleCode": "WS_ADMIN"
          }
        ]
      }
    ]
  }
}
```

## 错误码 <span id="tp23a3979af5" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
