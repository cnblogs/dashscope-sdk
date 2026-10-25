> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DeleteChunk - 删除切片

> 删除知识库中的指定文本切片，被删的文本切片将无法被检索和召回。

<meta property="og:title" content="DeleteChunk - 删除切片" />

## 接口说明 <span id="api-detail-51" />

<strong>警告</strong>  文本切片被删除后无法恢复（硬删除），请谨慎操作。

- 权限要求：

  - RAM 用户（子账号）：需先获取阿里云百炼的 API 权限（可使用AliyunBailianDataFullAccess策略，该策略已包含本接口所需的 sfm:DeleteChunk 权限点），并加入一个业务空间后，才能调用本接口。
  - 阿里云账号（主账号）：默认拥有权限，可直接调用。
- 调用方式：推荐使用最新版阿里云百炼 SDK调用，SDK 已封装复杂的签名计算逻辑，可简化您的调用过程。
- 生效延迟：更新后通常立即生效，高峰期可能稍有延迟（秒级）。
- 幂等性：本接口具有幂等性。对已成功删除的文本切片进行重复操作时，接口将返回成功响应。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 10 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/DeleteChunk)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:DeleteChunk</span></td><td><span>delete</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
POST /{WorkspaceId}/chunk/delete HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>PipelineId</span></td><td>string</td><td>是</td><td><div><p>知识库 ID，即 <strong>CreateIndex</strong> 接口返回的<code>Data.Id</code>。</p></div></td><td><span>79c0alxxxx</span></td></tr><tr><td><span>WorkspaceId</span></td><td>string</td><td>是</td><td><div><p>知识库所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><span>llm-3z7uw7fwz0vexxxx</span></td></tr><tr><td><span /><span>ChunkIds</span></td><td>array</td><td>是</td><td><div><p>需要删除的文本切片列表（单次最多传入 10 个文本切片 ID）。</p></div></td><td><span /></td></tr><tr><td><span /></td><td>string</td><td>是</td><td><div><p>文本切片 ID，即 <strong>ListChunks</strong> 接口返回的 Node 对象里<code>Metadata</code>中的<code>\_id</code>字段。</p></div></td><td><span>llm-5ip55o1zrzxx\_09fe52x\_xxxxx\_033b551e10024029992e79767b151fxx\_10024xx\_0</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>RequestId</span></td><td>string</td><td><div><p>请求 ID。</p></div></td><td><span>35A267BF-xxxx-54DB-8394-AA3B0742D833</span></td></tr><tr><td><span>Code</span></td><td>string</td><td><div><p>错误状态码。</p></div></td><td><span /></td></tr><tr><td><span>Message</span></td><td>string</td><td><div><p>错误信息。</p></div></td><td><span /></td></tr><tr><td><span>Success</span></td><td>boolean</td><td><div><p>接口调用是否成功，可能值：</p> <ul><li>true：成功。</li> <li>false：失败。</li></ul></div></td><td><span>true</span></td></tr><tr><td><span>Data</span></td><td>boolean</td><td><div><p>请求成功返回的业务数据。</p></div></td><td><span>true</span></td></tr><tr><td><span>Status</span></td><td>string</td><td><div><p>接口返回的状态码。</p></div></td><td><span>200</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "RequestId": "35A267BF-xxxx-54DB-8394-AA3B0742D833\n",
  "Code": "",
  "Message": "",
  "Success": true,
  "Data": true,
  "Status": 200
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。
