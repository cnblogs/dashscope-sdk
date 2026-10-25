> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetBillingTrend

> 查询指定时间范围内的账单趋势信息，支持按天或按月聚合，并按维度分组和筛选。

<meta property="og:title" content="GetBillingTrend" />

## 请求语法 <span id="api-detail-35" />

```
GET /modelstudio/billing/trend HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>granularity</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>查询粒度，不能为空。取值：</p> <ul><li><p>MONTH：按月</p></li><li><p>DAY：按天</p></li></ul></div></td><td><p>DAY</p></td></tr><tr><td><span>timePeriod</span></td><td><p>object</p></td><td><p><span>是</span></p></td><td><div><p>查询时间范围，包含开始和结束时间，不能为空。</p></div></td><td /></tr><tr><td><span>timePeriod.start</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>开始时间，格式为 YYYY-MM-DD。</p></div></td><td><p>2026-08-01</p></td></tr><tr><td><span>timePeriod.end</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>结束时间，格式为 YYYY-MM-DD。</p></div></td><td><p>2026-08-31</p></td></tr><tr><td><span>groupBy</span></td><td><p>array\<object></p></td><td><p><span>是</span></p></td><td><div><p>分组条件，必须且只能包含一个元素。支持的维度 Code 见下方补充说明。</p></div></td><td /></tr><tr><td><span>groupBy.code</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>维度 Code。</p></div></td><td><p>MAAS\_TYPE</p></td></tr><tr><td><span>filter</span></td><td><p>object</p></td><td><p><span>否</span></p></td><td><div><p>维度过滤条件。</p></div></td><td /></tr><tr><td><span>filter.dimensions</span></td><td><p>array\<object></p></td><td><p><span>否</span></p></td><td><div><p>筛选维度列表。</p></div></td><td /></tr><tr><td><span>filter.dimensions.code</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>维度 Code。</p></div></td><td><p>BASE\_MODEL</p></td></tr><tr><td><span>filter.dimensions.values</span></td><td><p>array\<string></p></td><td><p><span>否</span></p></td><td><div><p>筛选值列表。</p></div></td><td><p>\["qwen-max"]</p></td></tr><tr><td><span>filter.dimensions.selectType</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>筛选类型。取值：</p> <ul><li><p>IN</p></li><li><p>NOT</p></li></ul></div></td><td><p>IN</p></td></tr><tr><td><span>topNum</span></td><td><p>integer</p></td><td><p><span>否</span></p></td><td><div><p>返回分组数量，范围为 1～20，默认 20。其余分组合并为"其他"。</p></div></td><td><p>20</p></td></tr><tr><td><span>zeroFilter</span></td><td><p>boolean</p></td><td><p><span>否</span></p></td><td><div><p>是否过滤金额为 0 的分组，默认 true。</p></div></td><td><p>true</p></td></tr><tr><td><span>regionId</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>地域 ID。</p></div></td><td><p>cn-beijing</p></td></tr><tr><td><span>locale</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>返回语言，默认 en-US。取值：</p> <ul><li><p>en-US：英文</p></li><li><p>zh-CN：中文</p></li></ul></div></td><td><p>zh-CN</p></td></tr></tbody></table>

**补充说明**

`groupBy[].code` 和 `filter.dimensions[].code` 支持以下值，建议统一使用大写：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "15%" }} /><col style={{ width: "65%" }} /></colgroup><thead><tr><th><p><strong>维度 Code</strong></p></th><th><p><strong>含义</strong></p></th><th><p><strong>filter.dimensions\[].values 可传值</strong></p></th></tr></thead><tbody><tr><td><p>MAAS\_TYPE</p></td><td><p>MaaS 业务类型</p></td><td><p>inference（模型调用）、training（模型训练）、model\_units（模型单元）、capacity\_reserved（容量预留）</p></td></tr><tr><td><p>BASE\_MODEL</p></td><td><p>基础模型</p></td><td><p>账单数据中的基础模型原始值，例如 qwen-plus</p></td></tr><tr><td><p>API\_KEY\_ID</p></td><td><p>API Key ID</p></td><td><p>账单数据中的 API Key ID 原始值</p></td></tr><tr><td><p>WORKSPACE\_ID</p></td><td><p>业务空间 ID</p></td><td><p>账单数据中的 Workspace ID 原始值</p></td></tr><tr><td><p>FEE\_TYPE</p></td><td><p>费用类型</p></td><td><p>billing（账单费用）、subscription（订阅购买费用）</p></td></tr><tr><td><p>CHARGE\_TYPE</p></td><td><p>付费类型</p></td><td><p>postpaid（后付费）、prepaid（预付费）</p></td></tr><tr><td><p>BUSINESS\_REGION</p></td><td><p>业务地域</p></td><td><p>cn-beijing、ap-southeast-1、cn-hongkong 等，取值以实际值为准</p></td></tr><tr><td><p>SERVICE\_SITE</p></td><td><p>服务站点</p></td><td><p>asia-pacific-china、global、international 等，取值以实际值为准</p></td></tr><tr><td><p>ARTICLE\_CODE</p></td><td><p>商品 Code</p></td><td><p>sfm\_inferenceglobal\_public\_intl 等，取值以实际值为准</p></td></tr></tbody></table>

<Note>
  所有维度的 `filter.dimensions[].values` 均可传 `DIMENSION_FILTER_NULL_VALUE`，表示匹配字段为 NULL 或空字符串的数据。
</Note>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "14%" }} /><col style={{ width: "35%" }} /><col style={{ width: "26%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>requestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>099A671E-FA21-5A36-8A73-918572DDEF53</p></td></tr><tr><td><span>code</span></td><td><p>string</p></td><td><div><p>请求结果代码。</p></div></td><td><p>200</p></td></tr><tr><td><span>message</span></td><td><p>string</p></td><td><div><p>请求结果说明。</p></div></td><td><p>null</p></td></tr><tr><td><span>success</span></td><td><p>boolean</p></td><td><div><p>请求是否成功。</p></div></td><td><p>true</p></td></tr><tr><td><span>data</span></td><td><p>object</p></td><td><div><p>返回数据。</p></div></td><td /></tr><tr><td><span>data.costTotals</span></td><td><p>object</p></td><td><div><p>整个查询时间范围内的费用合计，包含 TopN 和"其他"。</p></div></td><td /></tr><tr><td><span>data.costTotals.amount</span></td><td><p>string</p></td><td><div><p>费用总金额</p></div></td><td><p>100</p></td></tr><tr><td><span>data.costTotals.pretaxAmount</span></td><td><p>string</p></td><td><div><p>不含税金额</p></div></td><td><p>94.34</p></td></tr><tr><td><span>data.costTotals.taxAmount</span></td><td><p>string</p></td><td><div><p>税费金额</p></div></td><td><p>5.66</p></td></tr><tr><td><span>data.costTotals.currency</span></td><td><p>string</p></td><td><div><p>金额币种</p></div></td><td><p>CNY</p></td></tr><tr><td><span>data.groupByTotal</span></td><td><p>array\<object></p></td><td><div><p>周期内 TopN 分组及可选"其他"分组的费用合计</p></div></td><td /></tr><tr><td><span>data.groupByTotal.key</span></td><td><p>string</p></td><td><div><p>分组维度值</p></div></td><td><p>qwen-plus</p></td></tr><tr><td><span>data.groupByTotal.name</span></td><td><p>string</p></td><td><div><p>分组展示名称，受入参 locale 影响</p></div></td><td><p>qwen-plus</p></td></tr><tr><td><span>data.groupByTotal.amount</span></td><td><p>string</p></td><td><div><p>当前分组的总金额</p></div></td><td><p>60</p></td></tr><tr><td><span>data.groupByTotal.pretaxAmount</span></td><td><p>string</p></td><td><div><p>当前分组的不含税金额</p></div></td><td><p>56.60</p></td></tr><tr><td><span>data.groupByTotal.taxAmount</span></td><td><p>string</p></td><td><div><p>当前分组的税费金额</p></div></td><td><p>3.40</p></td></tr><tr><td><span>data.resultByTime</span></td><td><p>array\<object></p></td><td><div><p>按时间升序排列的费用趋势列表</p></div></td><td /></tr><tr><td><span>data.resultByTime.period</span></td><td><p>string</p></td><td><div><p>统计周期。DAY 返回 yyyyMMdd，MONTH 返回 yyyyMM。</p></div></td><td><p>20260801</p></td></tr><tr><td><span>data.resultByTime.total</span></td><td><p>object</p></td><td><div><p>当前周期的费用合计</p></div></td><td /></tr><tr><td><span>data.resultByTime.total.amount</span></td><td><p>string</p></td><td><div><p>当前周期的费用总金额</p></div></td><td><p>30</p></td></tr><tr><td><span>data.resultByTime.total.pretaxAmount</span></td><td><p>string</p></td><td><div><p>当前周期的不含税金额</p></div></td><td><p>28.30</p></td></tr><tr><td><span>data.resultByTime.total.taxAmount</span></td><td><p>string</p></td><td><div><p>当前周期的税费金额</p></div></td><td><p>1.70</p></td></tr><tr><td><span>data.resultByTime.total.currency</span></td><td><p>string</p></td><td><div><p>当前周期的金额币种</p></div></td><td><p>CNY</p></td></tr><tr><td><span>data.resultByTime.periodDetails</span></td><td><p>array\<object></p></td><td><div><p>当前周期实际存在的费用分组</p></div></td><td /></tr><tr><td><span>data.resultByTime.periodDetails.key</span></td><td><p>string</p></td><td><div><p>分组维度值。TopN 以外的数据使用 DIMENSION\_GROUP\_OTHERS\_VALUE。</p></div></td><td><p>qwen-plus</p></td></tr><tr><td><span>data.resultByTime.periodDetails.name</span></td><td><p>string</p></td><td><div><p>分组展示名称，受入参 locale 影响。</p></div></td><td><p>qwen-plus</p></td></tr><tr><td><span>data.resultByTime.periodDetails.amount</span></td><td><p>string</p></td><td><div><p>当前周期内该分组的金额</p></div></td><td><p>20</p></td></tr><tr><td><span>data.resultByTime.periodDetails.pretaxAmount</span></td><td><p>string</p></td><td><div><p>当前周期内该分组的不含税金额</p></div></td><td><p>18.87</p></td></tr><tr><td><span>data.resultByTime.periodDetails.taxAmount</span></td><td><p>string</p></td><td><div><p>当前周期内该分组的税费金额</p></div></td><td><p>1.13</p></td></tr><tr><td><span>data.resultByTime.periodDetails.percentage</span></td><td><p>string</p></td><td><div><p>当前分组金额占当前周期总金额的比例</p></div></td><td><p>0.6667</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "requestId": "099A671E-FA21-5A36-8A73-918572DDEF53",
  "code": "200",
  "message": "null",
  "success": true,
  "data": {
    "costTotals": {
      "amount": "100",
      "pretaxAmount": "94.34",
      "taxAmount": "5.66",
      "currency": "CNY"
    },
    "groupByTotal": [
      {
        "key": "qwen-plus",
        "name": "qwen-plus",
        "amount": "60",
        "pretaxAmount": "56.60",
        "taxAmount": "3.40"
      }
    ],
    "resultByTime": [
      {
        "period": "20260801",
        "total": {
          "amount": "30",
          "pretaxAmount": "28.30",
          "taxAmount": "1.70",
          "currency": "CNY"
        },
        "periodDetails": [
          {
            "key": "qwen-plus",
            "name": "qwen-plus",
            "amount": "20",
            "pretaxAmount": "18.87",
            "taxAmount": "1.13",
            "percentage": "0.6667"
          }
        ]
      }
    ]
  }
}
```
