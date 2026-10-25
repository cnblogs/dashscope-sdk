> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取订阅席位与额度统计

> 查询订阅席位与额度统计，包含订阅周期、各类型席位数量、已分配席位数与剩余 Credits。

<meta property="og:title" content="获取订阅席位与额度统计" />

## 前提条件 <span id="tpf546fc0f4e" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用本接口所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

## 请求说明 <span id="tp1bb47deadc" />

- <strong>HTTP 方法</strong>：GET

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>GET [https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/subscription/stats](https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/subscription/stats)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口是阿里云 OpenAPI，采用 AccessKey 签名（签名算法 `ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer {API_KEY}` 方式。请求需携带公共请求头 `x-acs-action: GetSubscriptionStats`、`x-acs-version: 2026-02-10`、`x-acs-date`、`x-acs-content-sha256`、`x-acs-signature-nonce` 以及 `Authorization`。

  推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/api/ModelStudio/2026-02-10/GetSubscriptionStats) 发起调用，可免去自行计算签名。

## 请求参数 <span id="tp7448744f47" />

<p>该接口无请求参数。</p>

## 返回参数 <span id="tp83612d1380" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>调用接口是否成功：<br />true：成功<br />false：失败</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应状态码。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应信息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>业务数据</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SubscriptionStartTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>订阅开始时间,毫秒</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SubscriptionEndTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>订阅结束时间，毫秒</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Items</p></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>席位信息列表（按 specType 分组）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>列表数据</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SeatType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>席位类型（specType）<br />standard - 标准席位<br />pro - 高级席位<br />max - 尊享席位</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SeatRefreshTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>本周期刷新时间，毫秒</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>TotalSeats</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>总席位数</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AssignedSeats</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>已分配席位数</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SeatCredits</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>席位总 Credits 额度</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SeatRemainingCredits</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>本周期剩余 Credits</p></td></tr></tbody></table>

## 请求示例 <span id="tp96c97993b3" />

本接口使用阿里云 OpenAPI 签名，下例中 `${SIGNATURE}` 需按 `ACS3-HMAC-SHA256` 算法计算得出，`x-acs-date`、`x-acs-signature-nonce` 需替换为实际值。

```bash
curl -X GET "https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/subscription/stats" \
    --header "x-acs-action: GetSubscriptionStats" \
    --header "x-acs-version: 2026-02-10" \
    --header "x-acs-date: 2026-01-01T12:00:00Z" \
    --header "x-acs-content-sha256: e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" \
    --header "x-acs-signature-nonce: ${SIGNATURE_NONCE}" \
    --header "Authorization: ACS3-HMAC-SHA256 Credential=${ALIBABA_CLOUD_ACCESS_KEY_ID},SignedHeaders=host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version,Signature=${SIGNATURE}"
```

## 返回示例 <span id="tp1813294040" />

```json expandable
{
  "Success": true,
  "Data": {
    "SubscriptionStartTime": 1788328800000,
    "SubscriptionEndTime": 1790920800000,
    "Items": [
      {
        "SeatType": "standard",
        "SeatRefreshTime": 1790920800000,
        "TotalSeats": 2,
        "AssignedSeats": 1,
        "SeatCredits": 100,
        "SeatRemainingCredits": 40
      }
    ]
  }
}
```

## 错误码 <span id="tpfe19ee0400" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
