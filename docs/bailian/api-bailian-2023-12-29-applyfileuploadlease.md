> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# ApplyFileUploadLease

> 请求一个上传租约用于上传知识库文件，或智能体应用会话交互的文件。

<meta property="og:title" content="ApplyFileUploadLease" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:ApplyFileUploadLease 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口不具备幂等性。

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

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:ApplyFileUploadLease</p></td><td><p>none</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
POST /{WorkspaceId}/datacenter/category/{CategoryId} HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>CategoryId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>上传用于构建<a href="/zh/model-studio/rag-knowledge-base">知识库</a>的文件时：</p> <ul><li><p>该字段代表上传文件所属类目 ID，即 <strong>AddCategory</strong> 接口返回的<code>CategoryId</code>。您也可以在<span><a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>-文件页签</span>，单击类目名称旁的 ID 图标获取。此处允许传入 default，即使用系统创建的“默认类目”。</p></li></ul> <div><p>如需新增数据表并上传数据，请使用阿里云百炼控制台，API 不支持。</p></div> <ul><li><p>用于智能体应用<a href="https://help.aliyun.com/zh/model-studio/user-guide/file-interaction">会话交互</a>的文件时：</p></li> <li><p>此处传入 default 即可，系统会自动创建或者匹配默认类目，后续会开放动态文件类目接口及控制台管理页面。</p></li></ul></div></td><td><p>cate\_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx</p></td></tr><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>上传文件所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3z7uw7fwz0vexxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>FileName</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>上传用于构建<a href="/zh/model-studio/rag-knowledge-base">知识库</a>的文件：</p> <ul><li>该字段代表上传文件的名称，注意后缀需要带上文件格式类型，支持格式： <ul><li><p>文档（小于 150MB）：doc、docx、wps、ppt、pptx、xls、xlsx、md、txt、pdf、epub、mobi。</p></li> <li><p>表格（建议 10MB 以内，10 万行以内）：xls、xlsx。</p></li> <li><p>纯文本（建议不要超过 10MB）：md、txt。</p></li> <li><p>图片（小于 20MB，最短边 > 15px， 长边 \< 8192px，长宽比 \< 50）：png、jpg、jpeg、bmp、gif。</p></li> <li><p>音频：aac、amr、flac、flv、m4a、mp3、mpeg、ogg、opus、wav、webm、wma。</p></li> <li><p>视频：mp4、mkv、avi、mov、wmv。</p></li></ul></li> <li><p>文件名称长度限制 4-128 个字符。其它限制请参考<a href="/zh/model-studio/rag-knowledge-base-specifications">知识库配额与限制</a>。</p></li></ul> <div><p>如需新增数据表并上传数据，请使用阿里云百炼控制台，API 不支持。</p></div> <div><p>上传用于智能体应用<a href="https://help.aliyun.com/zh/model-studio/user-guide/file-interaction">会话交互</a>的文件：</p> <ul><li>该字段代表上传文件的名称，注意后缀需要带上文件格式类型。支持格式： <ul><li><p>文档：doc、docx、ppt、pptx、xls、xlsx、md、txt、pdf、epub、mobi。</p></li> <li><p>图片：png、jpg、jpeg、bmp、gif。</p></li> <li><p>音频：aac、amr、flac、flv、m4a、mp3、mpeg、ogg、opus、wav、webm、wma。</p></li> <li><p>视频：mp4、mkv、avi、mov、wmv。</p></li></ul></li> <li><p>文件名称长度限制 4-128 个字符。</p></li></ul></div></div></td><td><p>XXXX产品清单.pdf</p></td></tr><tr><td><span>Md5</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>上传文件的 MD5 值，服务端会验证该字段（当前暂未开启），请正确填写。</p></div></td><td><p>19657c391f6c70bcea63c154d8606bb3</p></td></tr><tr><td><span>SizeInBytes</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>上传文件的大小，单位字节，服务端会验证该字段（当前暂未开启），请正确填写。取值范围：1B-100M。</p></div></td><td><p>1000</p></td></tr><tr><td><span>CategoryType</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>类目类型，不传入该参数时，默认值为 UNSTRUCTURED。取值范围：</p> <p>UNSTRUCTURED：类目，用于构建<a href="/zh/model-studio/rag-knowledge-base">知识库</a>场景。 <span /></p> <p>SESSION\_FILE：上传用于智能体应用<a href="https://help.aliyun.com/zh/model-studio/user-guide/file-interaction">会话交互</a>的文件。</p> <div><p>如需新增数据表并上传数据，请使用阿里云百炼控制台，API 不支持。</p></div></div></td><td><p>UNSTRUCTURED</p></td></tr><tr><td><span>UseInternalEndpoint</span></td><td><p>boolean</p></td><td><p><span>否</span></p></td><td><div><div><p>若您使用了<a href="/zh/model-studio/configure-resources-in-private-network">阿里云百炼安全存储空间</a>，需要生成仅阿里云同地域内网可访问的租约 URL 链接时，此处可传入 true，以提高安全性。不传入该参数时，默认值为 false，即生成公网可访问的租约 URL。</p> <div><p>若您未开通阿里云百炼安全存储空间，或不确定是否在使用，此处请勿传入 true（会上传失败）。</p></div></div></div></td><td><p>false</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>DataCenter.FileTooLarge</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段。</p></div></td><td /></tr><tr><td><span>FileUploadLeaseId</span></td><td><p>string</p></td><td><div><p>租约唯一 ID，后续调用 <strong>AddFile</strong> 接口时，需要使用该参数。</p></div></td><td><p>1e6a159107384782be5e45ac4759b247.1719325231035</p></td></tr><tr><td><span /> <span>Param</span></td><td><p>object</p></td><td><div><p>用于上传文件的 HTTP 请求参数。</p></div></td><td /></tr><tr><td><span>Headers</span></td><td><p>any</p></td><td><div><p>需要放到 Header 中的 K-V 字段，K 和 V 均为字符串。</p> <div><p>返回的 Content-Type 可能为空值，后续按照空值上传即可。</p></div></div></td><td><p>"X-bailian-extra":"MTAwNTQyNjQ5NTE2OTE3OA==", "Content-Type":"application/pdf"</p></td></tr><tr><td><span>Method</span></td><td><p>string</p></td><td><div><p>HTTP 调用方法，可能值为：</p> <ul><li><p>PUT</p></li> <li><p>POST</p></li></ul></div></td><td><p>PUT</p></td></tr><tr><td><span>Url</span></td><td><p>string</p></td><td><div><p>文件的上传 URL 地址。</p> <div><p>该 URL 为预签名 URL，不支持 FormData 方式上传，需使用二进制方式上传。</p></div></div></td><td><p>[https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf?Expires=1716699536\&OSSAccessKeyId=YOUR\_ACCESS\_KEY\_ID\&Signature=YOUR\_SIGNATURE](https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf?Expires=1716699536\&OSSAccessKeyId=YOUR_ACCESS_KEY_ID\&Signature=YOUR_SIGNATURE)</p></td></tr><tr><td><span>Type</span></td><td><p>string</p></td><td><div><p>文件的上传方式，可能值为：</p> <ul><li><p>OSS.PreSignedURL</p></li> <li><p>HTTP</p></li></ul></div></td><td><p>HTTP</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>User not authorized to operate on the specified resource</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>778C0B3B-xxxx-5FC1-A947-36EDD13606AB</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "DataCenter.FileTooLarge",
  "Data": {
    "FileUploadLeaseId": "1e6a159107384782be5e45ac4759b247.1719325231035",
    "Param": {
      "Headers": "\"X-bailian-extra\":\"MTAwNTQyNjQ5NTE2OTE3OA==\",\n\"Content-Type\":\"application/pdf\"",
      "Method": "PUT",
      "Url": "https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf?Expires=1716699536&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE"
    },
    "Type": "HTTP"
  },
  "Message": "User not authorized to operate on the specified resource",
  "RequestId": "778C0B3B-xxxx-5FC1-A947-36EDD13606AB",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/ApplyFileUploadLease#workbench-doc-change-demo)。
