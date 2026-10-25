> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# ListFile

> 获取指定类目下一个或多个文档的详细信息。

<meta property="og:title" content="ListFile" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:ListFile 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 分页查询首页时，仅需设置`MaxResults`以限制返回信息的条目数，返回结果中的`NextToken`将作为查询后续页的凭证。查询后续页时，将`NextToken`参数设置为上一次返回结果中获取到的`NextToken`作为查询凭证（如果`NextToken`为空，表示结果已经完全返回，不需要再请求），并设置`MaxResults`限制返回条目数。
- 本接口具有幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 5 次/秒。如遇限流，请稍后重试。

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

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:ListFile</p></td><td><p>list</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
GET /{WorkspaceId}/datacenter/files HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>类目所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3shx2gu255oqxxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>CategoryId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>类目 ID，即 <strong>AddCategory</strong> 接口返回的<code>CategoryId</code>。您也可以在<a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>-文件页签，单击类目名称旁的 ID 图标获取。</p></div></td><td><p>cate\_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx</p></td></tr><tr><td><span>NextToken</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>查询凭证（Token），取值为上一次 API 调用返回的 NextToken 参数值。</p></div></td><td><p>AAAAAdH70eOCSCKtacdomNzak4U=</p></td></tr><tr><td><span>MaxResults</span></td><td><p>integer</p></td><td><p><span>否</span></p></td><td><div><p>分页查询时每页行数。取值范围\[1-200]。</p> <p>默认值： 当不设置值或设置的值小于 1 时，默认值为 20。当设置的值大于 200 时，默认值为 200。</p></div></td><td><p>20</p></td></tr><tr><td><span>FileName</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>文件名称（不含后缀），仅支持按照文件名精准查找，不支持模糊搜索。</p></div></td><td><p>product-overview</p></td></tr><tr><td><span /> <span>FileIds</span></td><td><p>array</p></td><td><p><span>否</span></p></td><td><div><p>需要查询的文件 id 列表，单次查询最多支持 20 个文件</p></div></td><td /></tr><tr><td><span /></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>文件 id</p></div></td><td><p>file\_xxxx</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>success</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段。</p></div></td><td /></tr><tr><td><span /> <span>FileList</span></td><td><p>array\<object></p></td><td><div><p>类目下的文件列表。</p></div></td><td /></tr><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>文件对象。</p></div></td><td /></tr><tr><td><span>CategoryId</span></td><td><p>string</p></td><td><div><p>文件所属类目 ID。</p></div></td><td><p>cate\_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx</p></td></tr><tr><td><span>CreateTime</span></td><td><p>string</p></td><td><div><p>文件实际添加到阿里云百炼中的时间戳，格式: yyyy-MM-dd HH:mm:ss，时区：UTC + 8。</p></div></td><td><p>2024-09-09 11:03:35</p></td></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><div><p>文件 ID，即 <strong>AddFile</strong> 接口返回的<code>FileId</code>。您也可以在<a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>页面，单击文件名称旁的图标获取。</p></div></td><td><p>file\_5ff599b3455a45db8c41b0054b361518\_xxxxxxxx</p></td></tr><tr><td><span>FileName</span></td><td><p>string</p></td><td><div><p>文件名称。</p></div></td><td><p>product-overview\.pdf</p></td></tr><tr><td><span>FileType</span></td><td><p>string</p></td><td><div><p>文件格式类型。可能值为：pdf、docx、doc、txt、md、pptx、ppt、xlsx、xls、html、png、jpg、jpeg、bmp、gif。</p></div></td><td><p>docx</p></td></tr><tr><td><span>Parser</span></td><td><p>string</p></td><td><div><p>文档解析器。可能值为：</p> <ul><li><p>DASHSCOPE\_DOCMIND：阿里云文档智能解析。</p></li></ul></div></td><td><p>DASHSCOPE\_DOCMIND</p></td></tr><tr><td><span>SizeInBytes</span></td><td><p>integer</p></td><td><div><p>文件大小，单位字节。</p></div></td><td><p>512</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>文件解析状态，可能值为：</p> <ul><li><p>INIT: 初始化状态，等待调度中。</p></li> <li><p>PARSING: 解析中。</p></li> <li><p>PARSE\_SUCCESS：解析完成。</p></li> <li><p>PARSE\_FAILED：解析失败。</p></li></ul></div></td><td><p>PARSE\_SUCCESS</p></td></tr><tr><td><span>ParseErrorMessage</span></td><td><p>string</p></td><td><div><p>解析失败原因</p></div></td><td><p>Error Message</p></td></tr><tr><td><span /> <span>Tags</span></td><td><p>array</p></td><td><div><p>文件关联的标签列表，一个文档支持关联多个标签。</p></div></td><td /></tr><tr><td><span /></td><td><p>string</p></td><td><div><p>文件的标签。</p></div></td><td><p>tag-A</p></td></tr><tr><td><span>HasNext</span></td><td><p>boolean</p></td><td><div><p>符合查询条件的类目数据是否存在下一页，可能值为：</p> <ul><li><p>true：是。</p></li> <li><p>false：否。</p></li></ul></div></td><td><p>true</p></td></tr><tr><td><span>MaxResults</span></td><td><p>integer</p></td><td><div><p>分页查询时每页行数。</p></div></td><td><p>20</p></td></tr><tr><td><span>NextToken</span></td><td><p>string</p></td><td><div><p>本次调用返回的查询凭证值。</p></div></td><td><p>4jzbJk9J6lNeuXD9hP0viA==</p></td></tr><tr><td><span>TotalCount</span></td><td><p>integer</p></td><td><div><p>返回结果的总条数。</p></div></td><td><p>48</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>Requests throttling triggered.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>8F97A63B-xxxx-527F-9D6E-467B6A7E8CF1</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json expandable
{
  "Code": "success",
  "Data": {
    "FileList": [
      {
        "CategoryId": "cate_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx",
        "CreateTime": "2024-09-09 11:03:35",
        "FileId": "file_5ff599b3455a45db8c41b0054b361518_xxxxxxxx",
        "FileName": "product-overview.pdf",
        "FileType": "docx",
        "Parser": "DASHSCOPE_DOCMIND",
        "SizeInBytes": 512,
        "Status": "PARSE_SUCCESS",
        "ParseErrorMessage": "Error Message",
        "Tags": [
          "tag-A"
        ]
      }
    ],
    "HasNext": true,
    "MaxResults": 20,
    "NextToken": "4jzbJk9J6lNeuXD9hP0viA==",
    "TotalCount": 48
  },
  "Message": "Requests throttling triggered.",
  "RequestId": "8F97A63B-xxxx-527F-9D6E-467B6A7E8CF1",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/ListFile#workbench-doc-change-demo)。
