> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# AddFilesFromAuthorizedOss

> 将已授权OSS Bucket中的文件导入阿里云百炼应用数据中。

<meta property="og:title" content="AddFilesFromAuthorizedOss" />

## 接口说明 <span id="api-detail-51" />

- 请确保该 OSS Bucket 与阿里云百炼同属一个阿里云账号（主账号），并已按[从 OSS 导入数据配置说明](/zh/model-studio/data-import-instructions)完成授权。

  - 支持的 Bucket 存储类型不包括归档、冷归档或深度冷归档。支持内容加密的 Bucket。支持公共读写/公共读/私有的 Bucket。
  - 如需使用开启 [Referer 防盗链](https://help.aliyun.com/zh/oss/user-guide/hotlink-protection)的 Bucket，须参考[仅允许受信任的网站访问](https://help.aliyun.com/zh/oss/user-guide/hotlink-protection)将域名`*.console.aliyun.com`添加到白名单 Referer 中。
- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:AddFilesFromAuthorizedOss 权限点）并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口不具有幂等性。

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

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:AddFilesFromAuthorizedOss</p></td><td><p>create</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
POST /{WorkspaceId}/datacenter/file/fromoss HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>业务空间 ID，将文件导入至该业务空间。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3z7uw7fwz0vxxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>CategoryId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>用于指定文件导入目标类目。即 AddCategory 接口返回的<code>CategoryId</code>。您也可以在<span><a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>-文件页签</span>，单击类目名称旁的 ID 图标获取类目 ID。此处允许传入 default，即使用系统创建的“默认类目”。</p></div></td><td><p>cate\_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx</p></td></tr><tr><td><span>CategoryType</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>类目类型，可选，默认值为 UNSTRUCTURED，取值范围：</p> <ul><li><p>UNSTRUCTURED：类目，用于构建知识库场景。</p></li></ul> <div><p>本接口不支持导入用于智能体应用<a href="https://help.aliyun.com/zh/model-studio/user-guide/file-interaction">会话交互</a>的 SESSION\_FILE，请使用 <strong>AddFile</strong> 接口从本地上传 SESSION\_FILE。</p></div></div> <div><p><strong>枚举值：</strong></p> <ul><li>UNSTRUCTURED : <div><p>UNSTRUCTURED</p></div></li></ul></div></td><td><p>UNSTRUCTURED</p></td></tr><tr><td><span>OssRegionId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>OSS Bucket 的地域 ID。获取方式请参见 <a href="https://help.aliyun.com/zh/oss/user-guide/regions-and-endpoints">OSS 地域和访问域名</a>。</p></div></td><td><p>cn-beijing</p></td></tr><tr><td><span>OssBucketName</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>OSS Bucket 名称，详见<a href="https://help.aliyun.com/zh/oss/user-guide/oss-bucket-overview">存储空间</a>。</p></div></td><td><p>bucketNamexxxxx</p></td></tr><tr><td><span /> <span>FileDetails</span></td><td><p>array\<object></p></td><td><p><span>是</span></p></td><td><div><p>导入文件列表。一次最多可上传 10 个文件。</p> <div><p>一次最多可上传 10 个文件。</p></div></div></td><td /></tr><tr><td><span /> <span /></td><td><p>array\<object></p></td><td><p><span>是</span></p></td><td><div><p>文件对象。</p></div></td><td /></tr><tr><td><span>FileName</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>导入文件的名称，注意后缀需要带上文件格式类型。</p> <ul><li><p>支持格式：pdf、docx、doc、txt、md、pptx、ppt、xlsx、xls、html、png、jpg、jpeg、bmp、gif。</p></li> <li><p>文件名称长度限制 4-128 个字符。</p></li> <li><p>对文件上传要求限制，请参见<a href="/zh/model-studio/rag-knowledge-base-specifications">知识库配额与限制</a>。</p></li></ul> <div>当导入的文件名称与知识库中已有文件名称重复时，接口仍会返回<code>Status</code>为<code>SUCCESS</code>，但该文件实际不会被导入知识库，已有的同名文件保持不变。请确保每次导入的文件名称唯一。</div> <div><p>如需新增数据表并上传数据，请使用阿里云百炼控制台，API 不支持。</p></div></div></td><td><p>this\_is\_temp\_xxxx.pdf</p></td></tr><tr><td><span>OssKey</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>导入文件在 OSS Bucket 中的键名（Key），详见<a href="https://help.aliyun.com/zh/oss/user-guide/object-overview">对象命名</a>。</p></div></td><td><p>root/path/this\_is\_temp\_xxxx.pdf</p></td></tr><tr><td><span>Parser</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>解析器类型。可能取值范围包括：</p> <ul><li><p>DOCMIND（智能文档解析）</p></li> <li><p>DOCMIND\_DIGITAL（电子文档解析）</p></li> <li><p>DOCMIND\_LLM\_VERSION（大模型文档解析）</p></li> <li><p>DASH\_QWEN\_VL\_PARSER（Qwen VL 解析）</p></li> <li><p>DOCMIND\_LLM\_VERSION\_MEDIA（音视频解析）</p></li> <li><p>AUTO\_SELECT（自动选择解析器）</p></li></ul> <p><div>当 CategoryType 为 UNSTRUCTURED 时，解析器会根据当前类目的数据解析设置，对您上传的文件进行解析。</div> <div>当 CategoryType 为 SESSION\_FILE 时，系统将使用默认方式（不支持更改）解析文件内容。</div></p></div></td><td><p>AUTO\_SELECT</p></td></tr><tr><td><span /> <span>ParserConfig</span></td><td><p>object</p></td><td><p><span>否</span></p></td><td><div><p>解析器配置，仅当类型被设置为 Qwen VL 解析时才需要传入。</p></div></td><td /></tr><tr><td><span>ModelName</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>模型名称。</p></div></td><td><p>qwen3-vl-plus</p></td></tr><tr><td><span>ModelPrompt</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>调用 Qwen VL 解析时的 Prompt。</p></div></td><td><p>#角色 你是一个专业的图片内容标注人员，擅长识别并描述出图片中的内容。 # 任务目标 请结合输入图片，详细描述图片中的内容。</p></td></tr><tr><td><span /> <span>Tags</span></td><td><p>array</p></td><td><p><span>否</span></p></td><td><div><p>文件关联的标签列表。默认值为空，即文件不关联任何标签。最多传入 10 个标签。</p></div></td><td /></tr><tr><td><span /></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>文件的标签。每个标签最多 12 个字符，支持 Unicode 中 letter 分类下的字符（其中包括英文、中文和数字等），下划线\_，中划线-。</p></div></td><td><p>产品介绍</p></td></tr><tr><td><span>OverWriteFileByOssKey</span></td><td><p>boolean</p></td><td><p><span>否</span></p></td><td><div><p>是否按照 OssKey 覆盖类目中的相同文件，默认值为 false，即不覆盖。</p></div></td><td><p>false</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>success</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段。</p></div></td><td /></tr><tr><td><span /> <span>AddFileResultList</span></td><td><p>array\<object></p></td><td><div><p>文件导入结果列表。</p></div></td><td /></tr><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>文件导入结果列表。</p></div></td><td /></tr><tr><td><span>FileId</span></td><td><p>string</p></td><td><div><p>文件 ID，请妥善保管该值，它将用于后续与此文件相关的所有 API 操作。</p></div></td><td><p>file\_809f469a59ac449586ec692576xxxxx\_102248XXX</p></td></tr><tr><td><span>OssKey</span></td><td><p>string</p></td><td><div><p>导入文件在 OSS Bucket 中的键名（Key）。</p></div></td><td><p>root/path/this\_is\_temp\_xxxx.pdf</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>文件导入状态。状态可能值为：</p> <ul><li><p>SUCCESS：导入（应用数据）完成。</p></li> <li><p>FAILED：导入（应用数据）失败。</p></li></ul> <div><p>状态为 SUCCESS 的文件才能用于创建/更新知识库。</p></div></div></td><td><p>SUCCESS</p></td></tr><tr><td><span>Msg</span></td><td><p>string</p></td><td><div><p>文件导入失败时返回错误信息。</p></div></td><td><p>size too large</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>Cant find out category for category\_id param.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>17204B98-xxxx-4F9A-8464-2446A84821CA</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>string</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功 。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "success",
  "Data": {
    "AddFileResultList": [
      {
        "FileId": "file_809f469a59ac449586ec692576xxxxx_102248XXX",
        "OssKey": "root/path/this_is_temp_xxxx.pdf",
        "Status": "SUCCESS",
        "Msg": "size too large"
      }
    ]
  },
  "Message": "Cant find out category for category_id param.",
  "RequestId": "17204B98-xxxx-4F9A-8464-2446A84821CA",
  "Status": "200",
  "Success": "true"
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/AddFilesFromAuthorizedOss#workbench-doc-change-demo)。
