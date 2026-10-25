> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DescribeFile

> 查询应用数据中文件的基本信息，包括文件名称、类型、状态等。

<meta property="og:title" content="DescribeFile" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（`AliyunBailianDataFullAccess`或`AliyunBailianDataReadOnlyAccess`均可，已包括 sfm:DescribeFile 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口具有幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 10 次/秒。如遇限流，请稍后重试。

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

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:DescribeFile</p></td><td><p>none</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
GET /{WorkspaceId}/datacenter/file/{FileId}/ HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>文件所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3shx2gu255oqxxxx</p></td></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>文件 ID，即 <strong>AddFile</strong> 接口返回的<code>FileId</code>。您也可以在<span><a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>-文件页签</span>，单击文件名称旁的 ID 图标获取。</p></div></td><td><p>file\_9a65732555b54d5ea10796ca5742ba22\_xxxxxxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead></table>

当前API无需请求参数

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>Success</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段。</p></div></td><td /></tr><tr><td><span>CategoryId</span></td><td><p>string</p></td><td><div><p>文件所属类目 ID。</p></div></td><td><p>cate\_cdd11b1b79a74e8bbd675c356a91ee3xxxxxxxx</p></td></tr><tr><td><span>CreateTime</span></td><td><p>string</p></td><td><div><p>文件实际添加到阿里云百炼中的时间戳，格式: yyyy-MM-dd HH:mm:ss，时区：UTC + 8。</p></div></td><td><p>2024-09-09 12:45:43</p></td></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><div><p>文件 ID。</p></div></td><td><p>file\_9a65732555b54d5ea10796ca5742ba22\_xxxxxxxx</p></td></tr><tr><td><span>FileName</span></td><td><p>string</p></td><td><div><p>文件名称。</p></div></td><td><p>XXX产品介绍.pdf</p></td></tr><tr><td><span>FileType</span></td><td><p>string</p></td><td><div><p>文件类型，扩展名。可能值为： pdf、docx、doc、txt、md、pptx、ppt、xlsx、xls、html、png、jpg、jpeg、bmp、gif。</p></div></td><td><p>pdf</p></td></tr><tr><td><span>Parser</span></td><td><p>string</p></td><td><div><p>解析该文件使用的解析器类型。可能值为：</p> <ul><li><p>DASHSCOPE\_DOCMIND：默认文档解析器。</p></li></ul></div></td><td><p>DASHSCOPE\_DOCMIND</p></td></tr><tr><td><span>SizeInBytes</span></td><td><p>integer</p></td><td><div><p>文件大小，单位字节 Byte。</p></div></td><td><p>1234</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>用于文档类知识库的文件（类型为 UNSTRUCTURED），状态可能值为：</p> <ul><li><p>INIT: 待解析。</p></li> <li><p>IN\_PARSE\_QUEUE：解析队列排队中。</p></li> <li><p>PARSING: 解析中。</p></li> <li>PARSE\_SUCCESS：解析完成。 <div>必须等到状态为 PARSE\_SUCCESS 才能将文档导入知识库。</div></li> <li><p>PARSE\_FAILED：解析失败。</p></li></ul> <div><p>用于智能体应用<a href="https://help.aliyun.com/zh/model-studio/user-guide/file-interaction">会话交互</a>的文件（类型为 SESSION\_FILE），状态可能值为：</p> <ul><li><p>INIT: 待解析。</p></li> <li><p>IN\_PARSE\_QUEUE：解析队列排队中。</p></li> <li><p>PARSING: 解析中。</p></li> <li><p>PARSE\_SUCCESS：解析完成。</p></li> <li><p>PARSE\_FAILED：解析失败。</p></li> <li><p>SAFE\_CHECKING: 安全检测中。</p></li> <li><p>SAFE\_CHECK\_FAILED: 安全检测失败。</p></li> <li><p>INDEX\_BUILDING：索引构建中。</p></li> <li><p>INDEX\_BUILD\_SUCCESS：索引构建成功。</p></li> <li><p>INDEX\_BUILDING\_FAILED：索引构建失败。</p></li> <li><p>INDEX\_DELETED：文件索引已删除。</p></li> <li>FILE\_IS\_READY：文件准备完毕。 <div>必须等到状态为 FILE\_IS\_READY 才能进行问答。</div></li> <li>FILE\_EXPIRED：文件过期。 <div>仅用户当前会话有效，用户关闭会话后文件过期（最长有效期为 7 天），不支持长期保存。</div></li></ul></div></div></td><td><p>PARSE\_SUCCESS</p></td></tr><tr><td><span /> <span>Tags</span></td><td><p>array</p></td><td><div><p>文件关联的标签列表，一个文件支持关联多个标签。</p></div></td><td /></tr><tr><td><span /></td><td><p>string</p></td><td><div><p>文件的标签。</p></div></td><td><p>产品介绍</p></td></tr><tr><td><span>ParseResultDownloadUrl</span></td><td><p>string</p></td><td><div /></td><td /></tr><tr><td><span>ParseErrorMessage</span></td><td><p>string</p></td><td><div><p>解析失败原因</p></div></td><td><p>Error Message</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>Requests throttling triggered.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>17204B98-xxxx-4F9A-8464-2446A84821CA</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "Success",
  "Data": {
    "CategoryId": "cate_cdd11b1b79a74e8bbd675c356a91ee3xxxxxxxx",
    "CreateTime": "2024-09-09 12:45:43",
    "FileId": "file_9a65732555b54d5ea10796ca5742ba22_xxxxxxxx",
    "FileName": "XXX产品介绍.pdf",
    "FileType": "pdf",
    "Parser": "DASHSCOPE_DOCMIND",
    "SizeInBytes": 1234,
    "Status": "PARSE_SUCCESS",
    "Tags": [
      "产品介绍"
    ],
    "ParseResultDownloadUrl": "",
    "ParseErrorMessage": "Error Message"
  },
  "Message": "Requests throttling triggered.",
  "RequestId": "17204B98-xxxx-4F9A-8464-2446A84821CA",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/DescribeFile#workbench-doc-change-demo)。
