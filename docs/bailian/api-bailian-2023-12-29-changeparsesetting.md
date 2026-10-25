> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# ChangeParseSetting

> 配置特定文件类型的解析方式。例如，为 .pdf 文件指定使用大模型文档解析，为 .jpg 文件指定使用Qwen VL解析。

<meta property="og:title" content="ChangeParseSetting" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:ChangeParseSetting 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口不具有幂等性。

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

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:ChangeParseSetting</p></td><td><p>update</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
PUT /{WorkspaceId}/datacenter/parser/settings HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>类目所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3z7uw7fwz0vxxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>CategoryId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>类目 ID，即 <strong>AddCategory</strong> 接口返回的<code>CategoryId</code>，或者在<span><a href="https://bailian.console.aliyun.com/?tab=app#/data-center">应用数据</a>-文件页签</span>，单击类目名称旁的 ID 图标获取。</p></div></td><td><p>cate\_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx</p></td></tr><tr><td><span>FileType</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>文件类型，扩展名，可能值为：</p> <ul><li><p>doc</p></li> <li><p>docx</p></li> <li><p>ppt</p></li> <li><p>pptx</p></li> <li><p>xls</p></li> <li><p>xlsx</p></li> <li><p>md</p></li> <li><p>txt</p></li> <li><p>pdf</p></li> <li><p>png</p></li> <li><p>jpg</p></li> <li><p>jpeg</p></li> <li><p>bmp</p></li> <li><p>gif</p></li> <li><p>html</p></li></ul></div></td><td><p>pdf</p></td></tr><tr><td><span>Parser</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>解析器标识 code。不同解析器适用于不同场景，具体参见“知识库”。可能值为：</p> <ul><li><p>DOCMIND（智能文档解析）</p></li> <li><p>DOCMIND\_DIGITAL （电子文档解析）</p></li> <li><p>DOCMIND\_LLM\_VERSION （大模型文档解析）</p></li> <li><p>DASH\_QWEN\_VL\_PARSER （Qwen VL 解析）</p></li> <li><p>DOCMIND\_LLM\_VERSION\_MEDIA（音视频解析）</p></li></ul></div> <div><p><strong>枚举值：</strong></p> <ul><li>DOCMIND : <div><p>智能文档解析</p></div></li> <li>DOCMIND\_LLM\_VERSION : <div><p>大模型文档解析</p></div></li> <li>DOCMIND\_DIGITAL : <div><p>电子文档解析</p></div></li> <li>DASH\_QWEN\_VL\_PARSER : <div><p>QWEN VL 解析</p></div></li></ul></div></td><td><p>DOCMIND</p></td></tr><tr><td><span /> <span>ParserConfig</span></td><td><p>object</p></td><td><p><span>否</span></p></td><td><div><p>解析器配置，仅当类型被设置为 Qwen VL 解析时才需要传入。</p></div></td><td /></tr><tr><td><span>modelName</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>模型名称。</p></div> <div><p><strong>枚举值：</strong></p> <ul><li>qwen3-vl-plus : <div><p>qwen3-vl-plus</p></div></li></ul></div></td><td><p>qwen3-vl-plus</p></td></tr><tr><td><span>modelPrompt</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>调用 Qwen VL 解析时的 Prompt。</p></div></td><td><p>#角色 你是一个专业的图片内容标注人员，擅长识别并描述出图片中的内容。 # 任务目标 请结合输入图片，详细描述图片中的内容。</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>请求 ID。</p></div></td><td><p>7BA8ADD9-53D6-53F0-918F-A1E776AD230E</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>请求成功返回的业务数据。</p></div></td><td /></tr><tr><td><span>ChangeResult</span></td><td><p>boolean</p></td><td><div><p>配置是否发生变更。</p> <ul><li><p>true：配置已更新。</p></li> <li><p>false：配置未发生变更，修改失败。</p></li></ul></div></td><td><p>true</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码。</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功 。</p></li> <li><p>false：失败。</p></li></ul></div></td><td><p>true</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息。</p></div></td><td><p>User not authorized to operate on the specified resource.</p></td></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>InvalidParameter</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "RequestId": "7BA8ADD9-53D6-53F0-918F-A1E776AD230E",
  "Data": {
    "ChangeResult": true
  },
  "Status": "200",
  "Success": true,
  "Message": "User not authorized to operate on the specified resource.",
  "Code": "InvalidParameter"
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/ChangeParseSetting#workbench-doc-change-demo)。
