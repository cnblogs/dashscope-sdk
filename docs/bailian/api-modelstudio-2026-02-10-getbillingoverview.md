> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetBillingOverview

> 查询指定月份的账单总览信息，支持按维度分组聚合和筛选。

<meta property="og:title" content="GetBillingOverview" />

## 请求语法 <span id="api-detail-35" />

```
GET /modelstudio/billing/overview HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>billMonth</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>账单月份，格式为 YYYY-MM，不能为空。</p></div></td><td><p>2026-08</p></td></tr><tr><td><span>groupBy</span></td><td><p>array\<object></p></td><td><p><span>是</span></p></td><td><div><p>分组条件列表。当前必须且只能传入一个分组维度。支持的维度 Code 见下方补充说明。</p></div></td><td /></tr><tr><td><span>groupBy.code</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>维度 Code。</p></div></td><td><p>MAAS\_TYPE</p></td></tr><tr><td><span>filter</span></td><td><p>object</p></td><td><p><span>否</span></p></td><td><div><p>过滤条件。</p></div></td><td /></tr><tr><td><span>filter.dimensions</span></td><td><p>array\<object></p></td><td><p><span>否</span></p></td><td><div><p>筛选维度列表。</p></div></td><td /></tr><tr><td><span>filter.dimensions.code</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>维度 Code。</p></div></td><td><p>BASE\_MODEL</p></td></tr><tr><td><span>filter.dimensions.values</span></td><td><p>array\<string></p></td><td><p><span>否</span></p></td><td><div><p>筛选值列表。</p></div></td><td><p>\["qwen-max"]</p></td></tr><tr><td><span>filter.dimensions.selectType</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>筛选类型。取值：</p> <ul><li><p>IN</p></li><li><p>NOT</p></li></ul></div></td><td><p>IN</p></td></tr><tr><td><span>topNum</span></td><td><p>integer</p></td><td><p><span>否</span></p></td><td><div><p>返回分组数量，范围为 1～20，默认 20。</p></div></td><td><p>20</p></td></tr><tr><td><span>zeroFilter</span></td><td><p>boolean</p></td><td><p><span>否</span></p></td><td><div><p>是否过滤金额为 0 的分组，默认 true。</p></div></td><td><p>true</p></td></tr><tr><td><span>regionId</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>地域 ID。</p></div></td><td><p>cn-beijing</p></td></tr><tr><td><span>locale</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>返回语言，默认 en-US。取值：</p> <ul><li><p>en-US：英文</p></li><li><p>zh-CN：中文</p></li></ul></div></td><td><p>zh-CN</p></td></tr></tbody></table>

**补充说明**

`groupBy[].code` 和 `filter.dimensions[].code` 支持以下值，建议统一使用大写：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "65%" }} /></colgroup><thead><tr><th><p><strong>维度 Code</strong></p></th><th><p><strong>含义</strong></p></th><th><p><strong>filter.dimensions\[].values 可传值</strong></p></th></tr></thead><tbody><tr><td><p>MAAS\_TYPE</p></td><td><p>MaaS 业务类型</p></td><td><p>inference（模型调用）、training（模型训练）、model\_units（模型单元）、capacity\_reserved（容量预留）</p></td></tr><tr><td><p>BASE\_MODEL</p></td><td><p>基础模型</p></td><td><p>账单数据中的基础模型原始值，例如 qwen-plus</p></td></tr><tr><td><p>API\_KEY\_ID</p></td><td><p>API Key ID</p></td><td><p>账单数据中的 API Key ID 原始值</p></td></tr><tr><td><p>WORKSPACE\_ID</p></td><td><p>业务空间 ID</p></td><td><p>账单数据中的 Workspace ID 原始值</p></td></tr><tr><td><p>FEE\_TYPE</p></td><td><p>费用类型</p></td><td><p>billing（账单费用）、subscription（订阅购买费用）</p></td></tr><tr><td><p>CHARGE\_TYPE</p></td><td><p>付费类型</p></td><td><p>postpaid（后付费）、prepaid（预付费）</p></td></tr><tr><td><p>BUSINESS\_REGION</p></td><td><p>业务地域</p></td><td><p>cn-beijing、ap-southeast-1、cn-hongkong 等，取值以实际值为准</p></td></tr><tr><td><p>SERVICE\_SITE</p></td><td><p>服务站点</p></td><td><p>asia-pacific-china、global、international 等，取值以实际值为准</p></td></tr><tr><td><p>ARTICLE\_CODE</p></td><td><p>商品 Code</p></td><td><p>sfm\_inferenceglobal\_public\_intl 等，取值以实际值为准</p></td></tr></tbody></table>

<Note>
  所有维度的 `filter.dimensions[].values` 均可传 `DIMENSION_FILTER_NULL_VALUE`，表示匹配字段为 NULL 或空字符串的数据。
</Note>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "14%" }} /><col style={{ width: "35%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>requestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>BB521414-5D38-5E66-AA66-963B2B4200E2</p></td></tr><tr><td><span>code</span></td><td><p>string</p></td><td><div><p>请求结果代码。</p></div></td><td><p>200</p></td></tr><tr><td><span>message</span></td><td><p>string</p></td><td><div><p>请求结果说明。</p></div></td><td><p>null</p></td></tr><tr><td><span>success</span></td><td><p>boolean</p></td><td><div><p>请求是否成功。</p></div></td><td><p>true</p></td></tr><tr><td><span>data</span></td><td><p>object</p></td><td><div><p>账单费用概览数据。</p></div></td><td /></tr><tr><td><span>data.currency</span></td><td><p>string</p></td><td><div><p>金额币种</p></div></td><td><p>USD</p></td></tr><tr><td><span>data.totalAmount</span></td><td><p>string</p></td><td><div><p>金额合计</p></div></td><td><p>31228.60</p></td></tr><tr><td><span>data.pretaxAmount</span></td><td><p>string</p></td><td><div><p>不含税金额合计</p></div></td><td><p>28729.32</p></td></tr><tr><td><span>data.taxAmount</span></td><td><p>string</p></td><td><div><p>税费合计</p></div></td><td><p>2499.28</p></td></tr><tr><td><span>data.groups</span></td><td><p>array\<object></p></td><td><div><p>按金额降序排列的 TopN 分组列表</p></div></td><td /></tr><tr><td><span>data.groups.key</span></td><td><p>string</p></td><td><div><p>分组维度值。空值返回 DIMENSION\_FILTER\_NULL\_VALUE。</p></div></td><td><p>inference</p></td></tr><tr><td><span>data.groups.name</span></td><td><p>string</p></td><td><div><p>分组展示名称，受 locale 参数影响；空值展示为-。</p></div></td><td><p>模型调用</p></td></tr><tr><td><span>data.groups.articleCodes</span></td><td><p>array\<string></p></td><td><div><p>当前分组涉及的商品 Code 列表</p></div></td><td /></tr><tr><td><span>data.groups.amount</span></td><td><p>string</p></td><td><div><p>当前分组金额</p></div></td><td><p>3000</p></td></tr><tr><td><span>data.groups.percentage</span></td><td><p>string</p></td><td><div><p>分组金额占 TopN 金额合计的比例</p></div></td><td><p>0.10</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "requestId": "BB521414-5D38-5E66-AA66-963B2B4200E2",
  "code": "200",
  "message": "null",
  "success": true,
  "data": {
    "currency": "USD",
    "totalAmount": "31228.60",
    "pretaxAmount": "28729.32",
    "taxAmount": "2499.28",
    "groups": [
      {
        "key": "inference",
        "name": "模型调用",
        "articleCodes": [
          "sfm_inferenceglobal_public_intl"
        ],
        "amount": "3000",
        "percentage": "0.10"
      }
    ]
  }
}
```
