> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询成员列表

> 查询组织成员列表（含席位信息），支持按名称、状态、是否分配席位过滤，支持分页。

<meta property="og:title" content="查询成员列表" />

## 前提条件 <span id="tp9d58ab803f" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用本接口所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

## 请求说明 <span id="tp0fb8f9b31d" />

- <strong>HTTP 方法</strong>：GET

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>GET [https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization/members](https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization/members)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口是阿里云 OpenAPI，采用 AccessKey 签名（签名算法 `ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer {API_KEY}` 方式。请求需携带公共请求头 `x-acs-action: ListOrganizationMembers`、`x-acs-version: 2026-02-10`、`x-acs-date`、`x-acs-content-sha256`、`x-acs-signature-nonce` 以及 `Authorization`。

  推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/api/ModelStudio/2026-02-10/ListOrganizationMembers) 发起调用，可免去自行计算签名。

## 请求参数 <span id="tp22967c2560" />

所有参数均通过 Query String 传递。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "22%" }} /><col style={{ width: "12%" }} /><col style={{ width: "10%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Name</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>成员名称模糊过滤（匹配 accountName 或 email，忽略大小写）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Status</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>成员状态过滤（如 ACTIVE、FROZEN），null 表示不过滤</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>HasSeat</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>是否已分配席位过滤：</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PageNum</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>页码，从 1 开始，默认 1</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PageSize</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>每页条数，默认 20，最大 100</p></td></tr></tbody></table>

## 返回参数 <span id="tpa543f42463" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>PageNo</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>当前页码</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PageSize</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>每页数量</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Total</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>总记录数</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>是否成功</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应状态码。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应信息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data</p></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>业务数据</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>操作结果</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AccountId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>成员账号 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AccountName</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>成员账号名称</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AccountBizId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>成员业务 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Email</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>成员邮箱</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>OrgId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>组织 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Status</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>成员状态</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Roles</p></td><td style={{ verticalAlign: "top" }}><p>Array</p></td><td style={{ verticalAlign: "top" }}><p>成员角色列表</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>成员角色</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>GmtCreate</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>加入时间</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SeatId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>席位资源分配 ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SpecType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>席位规格类型<br />standard - 标准席位<br />pro - 高级席位<br />max - 尊享席位</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>ApiKeyId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>API Key ID</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>MaskedApiKey</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>脱敏 API Key</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PackLimitInfo</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>HasShareLimit</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>UpperLimit</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>UsedCredits</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AvailableLimit</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>FrozenCredits</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>IsAvailable</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleStartTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleEndTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>LastConfirmedTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SubscriptionInfo</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>InstanceCode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>ProductCode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SpecType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PayMode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Status</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>StartTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>EndTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>EquityList</p></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>EquityType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleStartTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleEndTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleTotalValue</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleSurplusValue</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>EquityUnit</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr></tbody></table>

## 请求示例 <span id="tp412cbc40c7" />

本接口使用阿里云 OpenAPI 签名，下例中 `${SIGNATURE}` 需按 `ACS3-HMAC-SHA256` 算法计算得出，`x-acs-date`、`x-acs-signature-nonce` 需替换为实际值。示例 URL 中的中文取值为便于阅读未做编码；实际计算签名时，规范化请求（CanonicalRequest）的 Query String 需按 RFC 3986 对键和取值做百分号编码，否则会返回 `SignatureDoesNotMatch`。使用阿里云 SDK 时会自动处理。

```bash
curl -X GET "https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/organization/members?Name=成员名称&Status=ACTIVE&HasSeat=true&PageNum=1&PageSize=20" \
    --header "x-acs-action: ListOrganizationMembers" \
    --header "x-acs-version: 2026-02-10" \
    --header "x-acs-date: 2026-01-01T12:00:00Z" \
    --header "x-acs-content-sha256: e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" \
    --header "x-acs-signature-nonce: ${SIGNATURE_NONCE}" \
    --header "Authorization: ACS3-HMAC-SHA256 Credential=${ALIBABA_CLOUD_ACCESS_KEY_ID},SignedHeaders=host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version,Signature=${SIGNATURE}"
```

## 返回示例 <span id="tpde5bfe7f8b" />

```json expandable
{
  "PageNo": 18,
  "PageSize": 20,
  "Total": 1,
  "Success": true,
  "Data": [
    {
      "AccountId": "acc_123456789",
      "AccountName": "test_001",
      "AccountBizId": "112233",
      "Email": "test@email.com",
      "OrgId": "org_123456789",
      "Status": "ACTIVE",
      "Roles": [
        "ORG_MEMBER"
      ],
      "GmtCreate": "2026-06-10T11:57:42.000+00:00",
      "SeatId": "seat_123456",
      "SpecType": "standard",
      "ApiKeyId": "key_123456789",
      "MaskedApiKey": "prefix.abc****456",
      "PackLimitInfo": {
        "HasShareLimit": true,
        "UpperLimit": 0,
        "UsedCredits": 0,
        "AvailableLimit": 0,
        "FrozenCredits": 0,
        "IsAvailable": true,
        "CycleStartTime": 0,
        "CycleEndTime": 0,
        "LastConfirmedTime": 0
      },
      "SubscriptionInfo": {
        "InstanceCode": "",
        "ProductCode": "",
        "SpecType": "",
        "PayMode": "",
        "Status": "",
        "StartTime": 0,
        "EndTime": 0,
        "EquityList": [
          {
            "EquityType": "",
            "CycleStartTime": 0,
            "CycleEndTime": 0,
            "CycleTotalValue": 0,
            "CycleSurplusValue": 0,
            "EquityUnit": ""
          }
        ]
      }
    }
  ]
}
```

## 错误码 <span id="tp2a70b91abb" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
