> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DeleteFiles

> 批量删除文件

<meta property="og:title" content="DeleteFiles" />

## 接口说明 <span id="api-detail-51" />

- 不支持通过 API 删除数据表。如需删除数据表或表中特定的数据，请前往[应用数据](https://bailian.console.aliyun.com/?tab=app#/data-center)操作。
- 本接口用于删除[应用数据](https://bailian.console.aliyun.com/?tab=app#/data-center)中的文件，不会影响已构建好的知识库。如需删除知识库中的文件，请调用 <strong>DeleteIndexDocument</strong> 接口。
- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:DeleteFiles 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口仅能删除状态为解析失败（PARSE\_FAILED）或解析完成（PARSE\_SUCCESS）的文件。
- 本接口具有幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 10 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-doc-debug" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

## 授权信息 <span id="api-detail-32" />

当前API暂无授权信息透出。

## 请求语法 <span id="api-detail-53" />

```plaintext
POST /{WorkspaceId}/datacenter/file/delete HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>类目所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-mbhn96xxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span>FileIds</span></td><td><p>array</p></td><td><p><span>是</span></p></td><td><div><p>需要删除的文件 id 列表，单次删除最多支持 20 个文件</p></div></td><td /></tr><tr><td><span /></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>需要删除的文件 id</p></div></td><td><p>file\_xxxx</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>DataCenter.FileTooLarge</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>返回内容</p></div></td><td /></tr><tr><td><span /> <span>DeleteFileResultList</span></td><td><p>array\<object></p></td><td><div><p>删除结果</p></div></td><td /></tr><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>单个文件删除结果</p></div></td><td /></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><div><p>文件 ID。</p></div></td><td><p>file\_6b193b9b4b1546ef9eaa7340e69adfca\_10052857</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>文件删除状态。状态可能值为：</p> <ul><li><p>DELETED：删除成功。</p></li> <li><p>FAILED：删除失败。</p></li> <li><p>NOT\_FOUND：未查询到文件</p></li></ul></div></td><td><p>DELETED</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>Required parameter(%s) missing or invalid, please check the request parameters.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>Id of the request</p></div></td><td><p>17204B98-7734-4F9A-8464-2446A84821CA</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "DataCenter.FileTooLarge",
  "Data": {
    "DeleteFileResultList": [
      {
        "FileId": "file_6b193b9b4b1546ef9eaa7340e69adfca_10052857",
        "Status": "DELETED"
      }
    ]
  },
  "Message": "Required parameter(%s) missing or invalid, please check the request parameters.",
  "RequestId": "17204B98-7734-4F9A-8464-2446A84821CA",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/DeleteFiles#workbench-doc-change-demo)。
