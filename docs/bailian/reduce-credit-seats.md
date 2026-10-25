> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 退订席位

> 退订单个未使用的席位。

<meta property="og:title" content="退订席位" />

## 前提条件 <span id="tp124bb3e189" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用 BssOpenApi 所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

已获取待退订席位的 `InstanceId`。可从[查询订阅席位明细](/zh/model-studio/get-subscription-seat-details)返回的 `InstanceCode` 获取。仅未使用 Credits 的席位支持退订。

## 请求说明 <span id="tpcea34e8cc2" />

- <strong>HTTP 方法</strong>：POST

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>中国站</p></td><td style={{ verticalAlign: "top" }}><p><code>POST [https://business.aliyuncs.com/](https://business.aliyuncs.com/)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口属于 BssOpenApi（版本 `2023-09-30`），采用 AccessKey 签名（RPC 签名，`HMAC-SHA1`），不支持 `Authorization: Bearer {API_KEY}` 方式。

  推荐使用阿里云 SDK 的 `CommonRequest` 发起调用，SDK 会自动生成公共参数并计算签名。

## 请求参数 <span id="tp195594c9b1" />

全部参数通过表单或 Query String 传递。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "12%" }} /><col style={{ width: "10%" }} /><col style={{ width: "48%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>ProductCode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>产品代码。通过 <code>QueryProductList</code> 获取，例如 <code>sfm</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>ProductType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>产品类型。通过 <code>QueryProductList</code> 获取，例如 <code>sfm\_tokenplanteams\_dp\_cn</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>SubscriptionType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>订阅类型。通过 <code>QueryProductList</code> 获取，例如 <code>Subscription</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>ClientToken</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>幂等性令牌。由客户端生成，需保证不同请求间唯一，最大 64 个 ASCII 字符。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>InstanceId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>待退订的席位实例 ID。仅支持未使用 Credits 的席位。</p></td></tr></tbody></table>

## 返回参数 <span id="tp170cfc7367" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>RequestId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>请求 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>请求是否成功。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>结果码，成功为 <code>Success</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>结果描述。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>业务数据。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data.InstanceId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>实例 ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data.Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>退款是否成功。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data.ErrorInfo</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>失败时的错误信息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data.ErrorCode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>失败时的错误码。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data.HostId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>站点标识。中国站返回 <code>cn</code>；部分响应可能不返回该字段。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>AccessDeniedDetail</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>RAM 权限被拒绝时的详细错误信息。</p></td></tr></tbody></table>

外层 `Success` 表示请求是否被受理，退款结果以 `Data.Success` 为准。失败时查看 `Data.ErrorInfo` 和 `Data.ErrorCode`。

## 请求示例 <span id="tp1dcf001448" />

本接口使用 BssOpenApi 的 RPC 签名。下例中 `${SIGNATURE}` 需按签名算法计算，`SignatureNonce` 和 `Timestamp` 需替换为实际值。使用阿里云 SDK 时会自动处理公共参数和签名。

```bash
curl -X POST "https://business.aliyuncs.com/" \
    --data-urlencode "Action=ReduceCreditSeats" \
    --data-urlencode "Version=2023-09-30" \
    --data-urlencode "Format=JSON" \
    --data-urlencode "AccessKeyId=${ALIBABA_CLOUD_ACCESS_KEY_ID}" \
    --data-urlencode "SignatureMethod=HMAC-SHA1" \
    --data-urlencode "SignatureVersion=1.0" \
    --data-urlencode "SignatureNonce=${SIGNATURE_NONCE}" \
    --data-urlencode "Timestamp=2026-01-01T12:00:00Z" \
    --data-urlencode "Signature=${SIGNATURE}" \
    --data-urlencode "ProductCode=sfm" \
    --data-urlencode "ProductType=sfm_tokenplanteams_dp_cn" \
    --data-urlencode "SubscriptionType=Subscription" \
    --data-urlencode "ClientToken=example-client-token-001" \
    --data-urlencode "InstanceId=subs-example0000000004"
```

## 返回示例 <span id="tpad1408c46a" />

```json expandable
{
  "RequestId": "request-id-example",
  "Success": true,
  "Code": "Success",
  "Message": "Successful!",
  "Data": {
    "Success": true
  }
}
```

## 错误码 <span id="tpe27f20b3f3" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见 [BssOpenApi 错误码](https://help.aliyun.com/zh/user-center/developer-reference/api-bssopenapi-2017-12-14-error-codes)。
