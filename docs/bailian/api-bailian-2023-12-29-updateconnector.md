> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# UpdateConnector

> 编辑连接器

<meta property="og:title" content="UpdateConnector" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:UpdateConnector 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口不具备幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 5 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-doc-debug" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

## 授权信息 <span id="api-detail-32" />

当前API暂无授权信息透出。

## 请求语法 <span id="api-detail-53" />

```plaintext
PUT /{WorkspaceId}/datacenter/connector/{ConnectorId} HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>业务空间 ID</p></div></td><td><p>llm-3z7uw7fwz0vexxxx</p></td></tr><tr><td><span>ConnectorId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>所属数据连接（Connector）的实例 id，请到百炼控制台获取。</p></div></td><td><p>conn\_xxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>ConnectorName</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>连接器名称</p></div></td><td><p>test-connector</p></td></tr><tr><td><span>Description</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>描述</p></div></td><td><p>never\_delete\_aeip\_95\_us-west-1</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码</p></div></td><td><p>Index.Forbidden</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段</p></div></td><td /></tr><tr><td><span>ConnectorId</span></td><td><p>string</p></td><td><div><p>连接器 id</p></div></td><td><p>conn\_file\_e0c9db4030b2465a9478028f7d76cd92\_1234</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息</p></div></td><td><p>Required parameter(%s) missing or invalid, please check the request parameters.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>Id of the request</p></div></td><td><p>778C0B3B-03C1-5FC1-A947-36EDD13606AB</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>是否成功</p></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "Index.Forbidden",
  "Data": {
    "ConnectorId": "conn_file_e0c9db4030b2465a9478028f7d76cd92_1234"
  },
  "Message": "Required parameter(%s) missing or invalid, please check the request parameters.",
  "RequestId": "778C0B3B-03C1-5FC1-A947-36EDD13606AB",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/UpdateConnector#workbench-doc-change-demo)。
