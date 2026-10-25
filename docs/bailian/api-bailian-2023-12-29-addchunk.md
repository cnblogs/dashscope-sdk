> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# AddChunk

> 使用此API可为文档搜索类（document）、数据查询类（table）、图片问答类（image）知识库添加切片。

<meta property="og:title" content="AddChunk" />

## 接口说明 <span id="api-detail-51" />

- 对于文档搜索类（document）、数据查询类（table）、图片问答类（image）知识库，本接口可向指定知识库中添加切片内容；目前尚不支持对音视频搜索类（multimedia）知识库进行相关操作。仅当数据源为表格连接器(excel)时，对数据查询与图片问答类型知识库的操作方可生效。
- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:ChunkList 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 调用本接口前，请确保您的知识库已经创建完成且未被删除（即知识库 ID`IndexId`有效）。
- 本接口具有幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 10 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-doc-debug" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

## 授权信息 <span id="api-detail-32" />

当前API暂无授权信息透出。

## 请求语法 <span id="api-detail-53" />

```plaintext
POST /{WorkspaceId}/chunk/create HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>工作区标识</p></div></td><td><p>llm-19hxxxxx7htdf9lh</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>PipelineId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>知识库 id</p></div></td><td><p>79c0alxxxx</p></td></tr><tr><td><span>dataId</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>文件 id</p></div></td><td><p>doc\_xxx</p></td></tr><tr><td><span /> <span>field</span></td><td><p>object</p></td><td><p><span>否</span></p></td><td><div><p>插入的切片内容信息，以键值对形式传入。文档搜索类知识库使用固定 key 列表：</p> <ul><li><p>content（<strong>String</strong>）：<strong>必填</strong>，切片正文内容</p></li> <li><p>title（<strong>String</strong>）<strong>选填</strong>，切片标题</p></li> <li><p>image\_urls（<strong>Array</strong>）：<strong>选填</strong>，切片包含的图片链接，最多 10 张</p></li></ul> <p>数据查询类、图片问答类知识库 key 不固定，由该知识库的数据源表格决定：key 为 Excel 列标题，value 为对应列的值。</p></div></td><td><p>\{ "content": "The Bailian platform supports parsing multiple document formats including PDF, Word, and PPT.", "title": "Document Parsing and Chunking", "image\_urls": \[ "[https://example.com/images/chunk-flow.png](https://example.com/images/chunk-flow.png)", "[https://example.com/images/parsing-result.png](https://example.com/images/parsing-result.png)" ] }</p></td></tr><tr><td><span /></td><td><p>any</p></td><td><p><span>否</span></p></td><td><div><p>插入切片的表头字段信息，仅数据查询类与图片问答类知识库支持。需要参与检索或参与回复的表头为必填。各类型取值要求：</p> <ul><li><p><strong>String 类型</strong> ：最大长度 6000</p></li> <li><p><strong>时间 类型</strong>：13 位时间戳（毫秒）</p></li> <li><p><strong>Long 类型</strong>：整数，最大 2147483647</p></li> <li><p><strong>Double 类型</strong>：支持小数</p></li> <li><p><strong>image\_url 类型</strong>：最多 5 张，多张用英文逗号拼接为一个字符串</p></li></ul></div></td><td><p>\{"Product Name": "Wireless Bluetooth Headphones", "Publish Time": 1752624000000, "Stock Quantity": 1580, "Unit Price": 299.99, "image\_url":"[https://example.com/images/headphones-front.jpg,https://example.com/images/headphones-side.jpg,https://example.com/images/headphones-package.jpg](https://example.com/images/headphones-front.jpg,https://example.com/images/headphones-side.jpg,https://example.com/images/headphones-package.jpg)" }</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 id</p></div></td><td><p>35A267BF-xxxx-54DB-8394-AA3B0742D833</p></td></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码</p></div></td><td><p>Index.InvalidParameter</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息</p></div></td><td><p>Required parameter(%s) missing or invalid, please check the request parameters.</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功</p></div> <div><p><strong>枚举值：</strong></p> <ul><li>true : <div><p>true</p></div></li> <li>false : <div><p>false</p></div></li></ul></div></td><td><p>true</p></td></tr><tr><td><span>Data</span></td><td><p>boolean</p></td><td><div><p>请求成功返回的业务数据</p></div> <div><p><strong>枚举值：</strong></p> <ul><li>true : <div><p>true</p></div></li> <li>false : <div><p>false</p></div></li></ul></div></td><td><p>true</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码</p></div></td><td><p>200</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "RequestId": "35A267BF-xxxx-54DB-8394-AA3B0742D833",
  "Code": "Index.InvalidParameter",
  "Message": "Required parameter(%s) missing or invalid, please check the request parameters.",
  "Success": true,
  "Data": true,
  "Status": "200"
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/AddChunk#workbench-doc-change-demo)。
