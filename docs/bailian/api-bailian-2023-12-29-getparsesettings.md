> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetParseSettings - 获取类目解析设置

> 查询指定类目的数据解析设置。

<meta property="og:title" content="GetParseSettings - 获取类目解析设置" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（`AliyunBailianDataFullAccess`或`AliyunBailianDataReadOnlyAccess`均可，已包括 sfm:GetParseSettings 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口具有幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 10 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/GetParseSettings)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:GetParseSettings</span></td><td><span>get</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
GET /{WorkspaceId}/datacenter/parser/settings HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td>string</td><td>否</td><td><div><p>文件所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><span>llm-3shx2gu255oqxxxx</span></td></tr><tr><td><span>CategoryId</span></td><td>string</td><td>否</td><td><div><p>类目 ID，即 <strong>AddCategory</strong> 接口返回的<code>CategoryId</code>，或者在<a href="https://bailian.console.aliyun.com/cn-beijing/?tab=app#/data-center">应用数据</a>-非结构化数据页签，单击类目名称旁的 ID 图标获取。</p></div></td><td><span>cate\_cdd11b1b79a74e8bbd675c356a91ee35xxxxxxxx</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>RequestId</span></td><td>string</td><td><div><p>本次请求 requestId</p></div></td><td><span>35A267BF-xxxx-54DB-8394-AA3B0742D833</span></td></tr><tr><td><span /><span>Data</span></td><td>array\<object></td><td><div><p>请求成功返回的业务数据。</p></div></td><td><span /></td></tr><tr><td><span /><span>Data</span></td><td>object</td><td><div><p>返回具体内容</p></div></td><td><span /></td></tr><tr><td><span>FileType</span></td><td>string</td><td><div><p>文件类型，扩展名，可能值为： pdf、docx、doc 等，列举当前类目中支持的所有文件类型。</p></div></td><td><span>pdf</span></td></tr><tr><td><span>ParserDisplayName</span></td><td>string</td><td><div><p>解析方式的显示名称。</p></div></td><td><span>电子文档解析</span></td></tr><tr><td><span /><span>ParserConfig</span></td><td>object</td><td><div><p>解析器配置，仅在类型被设置为 Qwen VL 解析时才会返回。</p></div></td><td><span /></td></tr><tr><td><span>ModelName</span></td><td>string</td><td><div><p>模型名称。</p></div><div><div>枚举值：</div><ul><li>qwen3-vl-plus：qwen3-vl-plus。</li></ul></div></td><td><span>qwen3-vl-plus</span></td></tr><tr><td><span>ModelPrompt</span></td><td>string</td><td><div><p>调用 Qwen VL 解析时的 Prompt。</p></div></td><td><span>#角色 你是一个专业的图片内容标注人员，擅长识别并描述出图片中的内容。 # 任务目标 请结合输入图片，详细描述图片中的内容。</span></td></tr><tr><td><span>Parser</span></td><td>string</td><td><div><p>解析该类目下当前类型文件使用的解析器。可能值为：</p> <ul><li>DOCMIND：智能文档解析</li> <li>DOCMIND\_DIGITAL：电子文档解析</li> <li>DOCMIND\_LLM\_VERSION：大模型文档解析</li> <li>DASH\_QWEN\_VL\_PARSER：Qwen VL 解析</li></ul></div></td><td><span>DOCMIND</span></td></tr><tr><td><span>Status</span></td><td>string</td><td><div><p>接口返回的状态码。</p></div></td><td><span>200</span></td></tr><tr><td><span>Success</span></td><td>boolean</td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li>true：成功 。</li> <li>false：失败。</li></ul></div></td><td><span>True</span></td></tr><tr><td><span>Message</span></td><td>string</td><td><div><p>错误信息。</p></div></td><td><span>workspace id is null or invalid.</span></td></tr><tr><td><span>Code</span></td><td>string</td><td><div><p>错误状态码。</p></div></td><td><span>success</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "RequestId": "35A267BF-xxxx-54DB-8394-AA3B0742D833",
  "Data": [
    {
      "FileType": "pdf",
      "ParserDisplayName": "电子文档解析",
      "ParserConfig": {
        "ModelName": "qwen3-vl-plus",
        "ModelPrompt": "#角色\n你是一个专业的图片内容标注人员，擅长识别并描述出图片中的内容。\n# 任务目标\n请结合输入图片，详细描述图片中的内容。"
      },
      "Parser": "DOCMIND"
    }
  ],
  "Status": 200,
  "Success": true,
  "Message": "workspace id is null or invalid.",
  "Code": "success"
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。
