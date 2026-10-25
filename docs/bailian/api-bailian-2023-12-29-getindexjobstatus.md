> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetIndexJobStatus

> 查询指定的知识库创建任务或知识库追加任务的当前状态。

<meta property="og:title" content="GetIndexJobStatus" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（`AliyunBailianDataFullAccess`或`AliyunBailianDataReadOnlyAccess`均可，已包括 sfm:GetIndexJobStatus 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 您需要有一个正在执行中的知识库作业任务（创建知识库任务可以调用 <strong>SubmitIndexJob</strong> 接口，创建知识库追加任务可以调用 <strong>SubmitIndexAddDocumentsJob</strong> 接口），并且获得相应的`JobId`。
- 本接口调用间隔建议在 5 秒以上。
- 本接口具有幂等性。

## 调试 <span id="api-doc-debug" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:GetIndexJobStatus</p></td><td><p>get</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
GET /{WorkspaceId}/index/job/status HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>业务空间 ID，请确保您要操作的知识库在此业务空间中。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3z7uw7fwz0vexxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>JobId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>知识库作业任务 ID，即 <strong>SubmitIndexJob</strong> 接口或 <strong>SubmitIndexAddDocumentsJob</strong> 接口返回的<code>Data.Id</code>。</p></div></td><td><p>20230718xxxx-146c93bf</p></td></tr><tr><td><span>IndexId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>知识库 ID，即 <strong>CreateIndex</strong> 接口返回的<code>Data.Id</code>。</p></div></td><td><p>79c0alxxxx</p></td></tr><tr><td><span>PageNumber</span></td><td><p>integer</p></td><td><p><span>否</span></p></td><td><div><p><strong>SubmitIndexJob</strong> 接口和 <strong>SubmitIndexAddDocumentsJob</strong> 接口均支持批量导入文件。本接口返回的字段包括知识库作业任务的总状态<code>Status</code>及各文件导入的状态<code>Document.Status</code>。若文件较多，可通过<code>PageNumber</code>参数进行分页查询。起始值为 1，默认值为 1。</p></div></td><td><p>1</p></td></tr><tr><td><span>pageSize</span></td><td><p>integer</p></td><td><p><span>否</span></p></td><td><div><p>指定分页查询时每页展示的文件导入任务数量。最大值不限。默认值为 10。</p></div></td><td><p>10</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>Index.Forbidden</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段。</p></div></td><td /></tr><tr><td><span /> <span>Documents</span></td><td><p>array\<object></p></td><td><div><p>本次导入的文件列表。</p></div></td><td /></tr><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>文件对象。</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>Index.Document.ChunkError</p></td></tr><tr><td><span>DocId</span></td><td><p>string</p></td><td><div><p>文件 ID。</p></div></td><td><p>file\_9a65732555b54d5ea10796ca5742ba22\_xxxxxxxx</p></td></tr><tr><td><span>DocName</span></td><td><p>string</p></td><td><div><p>文件名称。</p></div></td><td><p>XXX产品介绍</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>document parse error</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>文件导入状态。可能值：</p> <ul><li><p>INSERT\_ERROR：导入索引失败。</p></li> <li><p>RUNNING：索引构建中。</p></li> <li><p>DELETED：已经删除。</p></li> <li><p>FINISH：索引构建成功。</p></li> <li><p>PARSE\_FAILED：解析失败</p></li> <li><p>DOC\_PARSING：解析中</p></li></ul></div></td><td><p>RUNNING</p></td></tr><tr><td><span>GmtModified</span></td><td><p>integer</p></td><td><div /></td><td /></tr><tr><td><span>JobId</span></td><td><p>string</p></td><td><div><p>作业任务 ID。</p></div></td><td><p>66122af12a4e45ddae6bd6c84555xxxx</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>知识库作业任务的当前状态。可能值：</p> <ul><li><p>COMPLETED：执行成功。</p></li> <li><p>FAILED：执行失败。</p></li> <li><p>RUNNING：执行中。</p></li> <li><p>PENDING：等待执行。</p></li></ul></div> <div><p><strong>枚举值：</strong></p> <ul><li>COMPLETED : <div><p>执行成功</p></div></li> <li>FAILED : <div><p>执行失败</p></div></li> <li>RUNNING : <div><p>执行中</p></div></li> <li>PENDING : <div><p>等待执行</p></div></li></ul></div></td><td><p>PENDING</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>User not authorized to operate on the specified resource.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>17204B98-xxxx-4F9A-8464-2446A84821CA</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "Index.Forbidden",
  "Data": {
    "Documents": [
      {
        "Code": "Index.Document.ChunkError",
        "DocId": "file_9a65732555b54d5ea10796ca5742ba22_xxxxxxxx",
        "DocName": "XXX产品介绍",
        "Message": "document parse error",
        "Status": "RUNNING",
        "GmtModified": 0
      }
    ],
    "JobId": "66122af12a4e45ddae6bd6c84555xxxx",
    "Status": "PENDING"
  },
  "Message": "User not authorized to operate on the specified resource.",
  "RequestId": "17204B98-xxxx-4F9A-8464-2446A84821CA",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/GetIndexJobStatus#workbench-doc-change-demo)。
