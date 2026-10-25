> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询共享包明细

> 分页查询共享包明细。

<meta property="og:title" content="查询共享包明细" />

## 前提条件 <span id="tpea3a7e8fa9" />

已获取阿里云账号或 RAM 用户的 AccessKey，并已为其授予调用本接口所需的 RAM 权限。建议将 AccessKey 配置为环境变量 `ALIBABA_CLOUD_ACCESS_KEY_ID` 和 `ALIBABA_CLOUD_ACCESS_KEY_SECRET`，避免明文写入代码。

## 请求说明 <span id="tp68f319fd39" />

- <strong>HTTP 方法</strong>：GET

- <strong>请求地址</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>地域</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>华北2（北京）</p></td><td style={{ verticalAlign: "top" }}><p><code>GET [https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/subscription/shared-packages](https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/subscription/shared-packages)</code></p></td></tr></tbody></table>

- <strong>认证方式</strong>

  本接口是阿里云 OpenAPI，采用 AccessKey 签名（签名算法 `ACS3-HMAC-SHA256`），不支持 `Authorization: Bearer {API_KEY}` 方式。请求需携带公共请求头 `x-acs-action: ListSubscriptionSharedPackages`、`x-acs-version: 2026-02-10`、`x-acs-date`、`x-acs-content-sha256`、`x-acs-signature-nonce` 以及 `Authorization`。

  推荐使用阿里云 SDK 或 [OpenAPI Explorer](https://api.aliyun.com/api/ModelStudio/2026-02-10/ListSubscriptionSharedPackages) 发起调用，可免去自行计算签名。

## 请求参数 <span id="tp7526376150" />

所有参数均通过 Query String 传递。数组类型参数（StatusList）使用 Flat 格式：以 `参数名.序号` 逐个传递，序号从 1 开始，例如 `StatusList.1=NORMAL`、`StatusList.2=<第二个取值>`。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "22%" }} /><col style={{ width: "12%" }} /><col style={{ width: "10%" }} /><col style={{ width: "56%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>PageNo</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>分页页号。默认值：1 取值范围：正整数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PageSize</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>分页参数：每页显示条数，默认值 10。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>StatusList</p></td><td style={{ verticalAlign: "top" }}><p>Array</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>状态筛选列表</p></td></tr></tbody></table>

## 返回参数 <span id="tpa8d300a0c4" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "12%" }} /><col style={{ width: "63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Success</p></td><td style={{ verticalAlign: "top" }}><p>boolean</p></td><td style={{ verticalAlign: "top" }}><p>调用接口是否成功：<br />true：成功<br />false：失败</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Code</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应状态码。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Message</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>响应信息。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Data</p></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p>业务数据</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Items</p></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>数据条目</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>数据条目</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>InstanceCode</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>席位 instance code</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>EquityList</p></td><td style={{ verticalAlign: "top" }}><p>Array\[Object]</p></td><td style={{ verticalAlign: "top" }}><p>当前生效的权益实例</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p /></td><td style={{ verticalAlign: "top" }}><p>object</p></td><td style={{ verticalAlign: "top" }}><p /></td></tr><tr><td style={{ verticalAlign: "top" }}><p>EquityType</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>权益类型（如 CREDITS、SPN、资源包等）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleInstanceId</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>权益 code（订阅 code，credits 场景下不需要消费）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleStartTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>当前周期开始时间，毫秒</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleEndTime</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>当前周期结束时间，毫秒</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleTotalValue</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>当前周期的总额度</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleSurplusValue</p></td><td style={{ verticalAlign: "top" }}><p>number</p></td><td style={{ verticalAlign: "top" }}><p>当前周期的剩余额度</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>CycleVersion</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>当前周期的时序版本</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Status</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>席位状态：<br />CREATING —— 创建中<br />NORMAL —— 有效状态<br />LIMIT ——欠费受限<br />RELEASE —— 到期释放<br />STOP —— 到期停机<br />REFUNDED —— 已退款</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Total</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>席位的总数量</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PageNo</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>页码，取值大于 0 且不超过 Integer 数据类型的最大值，默认值为 1。页码，取值大于 0 且不超过 Integer 数据类型的最大值</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>PageSize</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>每页条数。</p></td></tr></tbody></table>

## 请求示例 <span id="tp838016008a" />

本接口使用阿里云 OpenAPI 签名，下例中 `${SIGNATURE}` 需按 `ACS3-HMAC-SHA256` 算法计算得出，`x-acs-date`、`x-acs-signature-nonce` 需替换为实际值。

```bash
curl -X GET "https://modelstudio.cn-beijing.aliyuncs.com/tokenplan/subscription/shared-packages?PageNo=1&PageSize=10&StatusList.1=NORMAL" \
    --header "x-acs-action: ListSubscriptionSharedPackages" \
    --header "x-acs-version: 2026-02-10" \
    --header "x-acs-date: 2026-01-01T12:00:00Z" \
    --header "x-acs-content-sha256: e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" \
    --header "x-acs-signature-nonce: ${SIGNATURE_NONCE}" \
    --header "Authorization: ACS3-HMAC-SHA256 Credential=${ALIBABA_CLOUD_ACCESS_KEY_ID},SignedHeaders=host;x-acs-action;x-acs-content-sha256;x-acs-date;x-acs-signature-nonce;x-acs-version,Signature=${SIGNATURE}"
```

## 返回示例 <span id="tpde25f588c1" />

```json expandable
{
  "Success": true,
  "Data": {
    "Items": [
      {
        "InstanceCode": "subs-1234567",
        "EquityList": [
          {
            "EquityType": "CREDITS",
            "CycleInstanceId": "123456",
            "CycleStartTime": 1775232000,
            "CycleEndTime": 1756310400,
            "CycleTotalValue": 100,
            "CycleSurplusValue": 40,
            "CycleVersion": 1
          }
        ],
        "Status": "NORMAL"
      }
    ],
    "Total": 100,
    "PageNo": 1,
    "PageSize": 10
  }
}
```

## 错误码 <span id="tp608affbcbd" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
