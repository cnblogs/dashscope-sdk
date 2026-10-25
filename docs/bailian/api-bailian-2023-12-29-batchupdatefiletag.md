> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# BatchUpdateFileTag

> 该接口用于批量更新数据连接中的文档标签。

<meta property="og:title" content="BatchUpdateFileTag" />

## 调试 <span id="api-doc-debug" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

## 授权信息 <span id="api-detail-32" />

当前API暂无授权信息透出。

## 请求语法 <span id="api-detail-53" />

```plaintext
PUT /{WorkspaceId}/datacenter/batchupdatetag HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>业务空间 ID。在百炼的<a href="https://bailian.console.aliyun.com/knowledge-base#/home">控制台首页</a>，单击页面左上角业务空间详情图标获取。</p></div></td><td><p>llm-3shx2gu255oqxxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span>FileInfos</span></td><td><p>array\<object></p></td><td><p><span>是</span></p></td><td><div><p>需要更新的文档列表</p></div></td><td /></tr><tr><td><span /> <span /></td><td><p>object</p></td><td><p><span>是</span></p></td><td><div /></td><td /></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>数据中心的文件 ID，您可以在<a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>页面，单击文件名称旁的 ID 图标获取。</p></div></td><td><p>file\_3d5319366e2c46309f4c11cfbeacd5fd\_10045951</p></td></tr><tr><td><span /> <span>tags</span></td><td><p>array</p></td><td><p><span>是</span></p></td><td><div><ul><li><p>文件关联的标签列表。最多传入 100 个标签，所有标签字符长度总和不能超过 700。</p></li></ul></div></td><td /></tr><tr><td><span /></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>标签值，每个标签最多 32 个字符，支持 Unicode 中 letter 分类下的字符（其中包括英文、中文和数字等），下划线\_，中划线-，标签中不能包含空格。</p></div></td><td><p>TagA</p></td></tr><tr><td><span>UpdateMode</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>更新模式，仅支持 APPEND（追加）和 OVERWRITE（覆盖）</p></div></td><td><p>OVERWRITE</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码</p></div></td><td><p>Success</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口返回的业务字段。</p></div></td><td /></tr><tr><td><span /> <span>UpdateFileTagResultList</span></td><td><p>array\<object></p></td><td><div><p>标签更新的结果列表</p></div></td><td /></tr><tr><td><span /> <span /></td><td><p>object</p></td><td><div /></td><td /></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><div><p>文件 ID。</p></div></td><td><p>file\_f40f2a32205d44b4a93b11617113da15\_10045951</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr><tr><td><span>ErrorCode</span></td><td><p>string</p></td><td><div><p>返回错误码，仅当 Success 为 false 时返回。</p></div></td><td><p>NoPermission</p></td></tr><tr><td><span>ErrorMessage</span></td><td><p>string</p></td><td><div><p>错误描述信息，仅当 Success 为 false 时返回。</p></div></td><td><p>FileId not exists.</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息</p></div></td><td><p>Required parameter(FileId) missing or invalid, please check the request parameters.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>Id of the request</p></div></td><td><p>17204B98-xxxx-4F9A-8464-2446A84821CA</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值：</p> <ul><li><p>true：成功。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "Success",
  "Data": {
    "UpdateFileTagResultList": [
      {
        "FileId": "file_f40f2a32205d44b4a93b11617113da15_10045951",
        "Success": true,
        "ErrorCode": "NoPermission",
        "ErrorMessage": "FileId not exists."
      }
    ]
  },
  "Message": "Required parameter(FileId) missing or invalid, please check the request parameters.",
  "RequestId": "17204B98-xxxx-4F9A-8464-2446A84821CA",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/BatchUpdateFileTag#workbench-doc-change-demo)。
