> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# AddFile

> 将存储于阿里云百炼临时存储空间内的文件导入至阿里云百炼数据连接（原应用数据）。

<meta property="og:title" content="AddFile" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:AddFile 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
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

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:AddFile</p></td><td><p>create</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
PUT /{WorkspaceId}/datacenter/file HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>业务空间 ID，即文件将上传至该业务空间中。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3shx2gu255oqxxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>LeaseId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>上传租约 ID，对应 <strong>ApplyFileUploadLease</strong> 接口返回的<code>FileUploadLeaseId</code>。</p></div></td><td><p>68abd1dea7b6404d8f7d7b9f7fbd332d.17166xxxxxxxx</p></td></tr><tr><td><span>Parser</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>解析器类型。可能取值范围包括：</p> <ul><li><p>DOCMIND（智能文档解析）</p></li> <li><p>DOCMIND\_DIGITAL（电子文档解析）</p></li> <li><p>DOCMIND\_LLM\_VERSION（大模型文档解析）</p></li> <li><p>DASH\_QWEN\_VL\_PARSER（Qwen VL 解析）</p></li> <li><p>DOCMIND\_LLM\_VERSION\_MEDIA（音视频解析）</p></li> <li><p>AUTO\_SELECT（自动选择解析器）</p></li></ul> <p><div>当 CategoryType 为 UNSTRUCTURED 时，解析器会根据当前类目的数据解析设置，对您上传的文件进行解析。</div> <div>当 CategoryType 为 SESSION\_FILE 时，系统将使用默认方式（不支持更改）解析文件内容。</div></p></div></td><td><p>AUTO\_SELECT</p></td></tr><tr><td><span>CategoryId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><ul><li><p>当 CategoryType 为 UNSTRUCTURED 时，需传入上传文件所属类目 ID，即 <strong>AddCategory</strong> 接口返回的<code>CategoryId</code>。您也可以前往<a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>-文件页签，单击类目名称旁的 ID 图标获取类目 ID。此处允许传入 default，即使用系统创建的“默认类目”。</p></li> <li><p>当 CategoryType 为 SESSION\_FILE 时，传入“default”即可。</p></li></ul></div></td><td><p>cate\_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx</p></td></tr><tr><td><span /> <span>Tags</span></td><td><p>array</p></td><td><p><span>否</span></p></td><td><div><ul><li><p>文件关联的标签列表。最多传入 100 个标签，所有标签字符长度总和不能超过 700。</p></li> <li><p>默认值为空，即不设置标签。</p></li></ul></div></td><td /></tr><tr><td><span /></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>文件的标签。每个标签最多 32 个字符，支持 Unicode 中 letter 分类下的字符（其中包括英文、中文和数字等），下划线\_，中划线-。</p></div></td><td><p>产品介绍</p></td></tr><tr><td><span>CategoryType</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>类目类型，可选，默认值为 UNSTRUCTURED，取值范围：</p> <ul><li><p>UNSTRUCTURED：类目，用于构建知识库场景。</p></li> <li><p>SESSION\_FILE：用于智能体应用<a href="https://help.aliyun.com/zh/model-studio/user-guide/file-interaction">会话交互</a>的文件。 <div>在使用 <code>SESSION\_FILE</code> 的情况下，调用 ApplyFileUploadLease 接口时，CategoryType 参数也应传入 <code>SESSION\_FILE</code>。</div> <div>仅用户当前会话有效，用户关闭会话后文件过期（最长有效期为 7 天），不支持长期保存。</div></p></li></ul></div></td><td><p>UNSTRUCTURED</p></td></tr><tr><td><span>OriginalFileUrl</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><div><p>通过此参数为文件添加一个 URL，系统将在构建<a href="/zh/model-studio/rag-knowledge-base">文档搜索类知识库</a>时记录该链接。在使用阿里云百炼控制台与<a href="/zh/model-studio/single-agent-application">智能体应用</a>对话时，随该文件召回结果返回（通过<code>docUrl</code>字段）。</p> <div><p>智能体应用必须开启<strong>知识库</strong>，并启用<strong>展示回答来源</strong>功能，否则此参数不生效。</p></div></div></div></td><td><p>[www.test.com/111.docx](http://www.test.com/111.docx)</p></td></tr><tr><td><span /> <span>ParserConfig</span></td><td><p>object</p></td><td><p><span>否</span></p></td><td><div><p>解析器配置，仅当类型被设置为 Qwen VL 解析时才需要传入。</p></div></td><td /></tr><tr><td><span>ModelName</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>模型名称。</p></div></td><td><p>qwen3-vl-plus</p></td></tr><tr><td><span>ModelPrompt</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>调用 Qwen VL 解析时的 Prompt。</p></div></td><td><p>#角色 你是一个专业的图片内容标注人员，擅长识别并描述出图片中的内容。 # 任务目标 请结合输入图片，详细描述图片中的内容。</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码</p></div></td><td><p>DataCenter.FileTooLarge</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段</p></div></td><td /></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><div><p>文件 ID，请妥善保管该值，它将用于后续与此文件相关的所有 API 操作</p></div></td><td><p>file\_9a65732555b54d5ea10796ca5742ba22\_xxxxxxxx</p></td></tr><tr><td><span>Parser</span></td><td><p>string</p></td><td><div><p>解析该文件使用的解析器类型。可能值为：</p> <ul><li><p>DASHSCOPE\_DOCMIND：阿里云文档智能解析</p></li></ul></div></td><td><p>DASHSCOPE\_DOCMIND</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息</p></div></td><td><p>User not authorized to operate on the specified resource.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 ID</p></div></td><td><p>778C0B3B-xxxx-5FC1-A947-36EDD13606AB</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>string</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功</p></li> <li><p>false：失败</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "DataCenter.FileTooLarge",
  "Data": {
    "FileId": "file_9a65732555b54d5ea10796ca5742ba22_xxxxxxxx",
    "Parser": "DASHSCOPE_DOCMIND"
  },
  "Message": "User not authorized to operate on the specified resource.",
  "RequestId": "778C0B3B-xxxx-5FC1-A947-36EDD13606AB",
  "Status": "200",
  "Success": "true"
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/AddFile#workbench-doc-change-demo)。
