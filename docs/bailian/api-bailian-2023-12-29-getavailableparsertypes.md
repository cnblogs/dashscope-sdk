> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetAvailableParserTypes - 获取文件支持的解析器类型

> 根据输入的文件类型（文件扩展名），获取所有支持的解析器类型列表。

<meta property="og:title" content="GetAvailableParserTypes - 获取文件支持的解析器类型" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（`AliyunBailianDataFullAccess`或`AliyunBailianDataReadOnlyAccess`均可，已包括 sfm:GetAvailableParserTypes 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口具有幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 10 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/GetAvailableParserTypes)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:GetAvailableParserTypes</span></td><td><span>get</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
GET /{WorkspaceId}/datacenter/parser/parsertype HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td>string</td><td>是</td><td><div><p>文件所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><span>llm-3shx2gu255oqxxxx</span></td></tr><tr><td><span>FileType</span></td><td>string</td><td>是</td><td><div><p>文件类型（扩展名），用于获取支持该文件类型的解析器。可能值为：</p> <ul><li>doc</li> <li>docx</li> <li>ppt</li> <li>pptx</li> <li>xls</li> <li>xlsx</li> <li>md</li> <li>txt</li> <li>pdf</li> <li>png</li> <li>jpg</li> <li>jpeg</li> <li>bmp</li> <li>gif</li> <li>html</li></ul></div></td><td><span>pdf</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>RequestId</span></td><td>string</td><td><div><p>请求 ID。</p></div></td><td><span>17204B98-7734-4F9A-8464-2446XXXXXXX</span></td></tr><tr><td><span /><span>Data</span></td><td>object</td><td><div><p>请求成功返回的业务数据。</p></div></td><td><span /></td></tr><tr><td><span>FileType</span></td><td>string</td><td><div><p>文件类型，与输入参数中的 FileType 相同。</p></div></td><td><span>pdf</span></td></tr><tr><td><span /><span>ParserList</span></td><td>array\<object></td><td><div><p>当前类型支持的解析器列表。</p></div></td><td><span /></td></tr><tr><td><span /><span>ParserList</span></td><td>object</td><td><div><p>解析器对象。</p></div></td><td><span /></td></tr><tr><td><span>DisplayName</span></td><td>string</td><td><div><p>解析方式的显示名称。</p></div></td><td><span>智能文档解析</span></td></tr><tr><td><span>Parser</span></td><td>string</td><td><div><p>解析器 code。可能值为：</p> <ul><li>DOCMIND（智能文档解析）</li> <li>DOCMIND\_DIGITAL （电子文档解析）</li> <li>DOCMIND\_LLM\_VERSION （大模型文档解析）</li> <li>DASH\_QWEN\_VL\_PARSER （Qwen-VL 解析）</li></ul></div></td><td><span>DOCMIND</span></td></tr><tr><td><span>Status</span></td><td>string</td><td><div><p>接口返回的状态码。</p></div></td><td><span>200</span></td></tr><tr><td><span>Success</span></td><td>boolean</td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li>true：成功。</li> <li>false：失败。</li></ul></div></td><td><span>true</span></td></tr><tr><td><span>Message</span></td><td>string</td><td><div><p>错误信息</p></div></td><td><span>User not authorized to operate on the specified resource</span></td></tr><tr><td><span>Code</span></td><td>string</td><td><div><p>返回的错误 code。</p></div></td><td><span>DataCenter.Throttling</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "RequestId": "17204B98-7734-4F9A-8464-2446XXXXXXX",
  "Data": {
    "FileType": "pdf",
    "ParserList": [
      {
        "DisplayName": "智能文档解析",
        "Parser": "DOCMIND"
      }
    ]
  },
  "Status": 200,
  "Success": true,
  "Message": "User not authorized to operate on the specified resource",
  "Code": "DataCenter.Throttling"
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。
