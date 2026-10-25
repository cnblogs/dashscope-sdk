> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# SubmitIndexJob - 提交知识库创建任务

> 提交指定的 CreateIndex 任务以完成知识库创建。

<meta property="og:title" content="SubmitIndexJob - 提交知识库创建任务" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 API 权限（需要AliyunBailianDataFullAccess，已包括 sfm:SubmitIndexJob 权限点），并加入一个业务空间后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版阿里云百炼 SDK来调用本接口。
- 调用本接口前，您必须预先调用 CreateIndex 接口，并且获取到对应的IndexId。
- 调用本接口后，任务需一定时间执行，高峰期可能耗时数小时。任务完成前请勿重复发起请求。如果需要查询任务的执行状态，可调用 GetIndexJobStatus 接口查询。
- 知识库创建完成后，接下来您便可以在应用管理中将创建好的知识库与您位于相同的业务空间里的智能体应用 关联（或者通过应用调用的rag\_options传入IndexID），以便为您的阿里云百炼应用补充私有知识和提供最新信息。当然，您也可以选择不使用阿里云百炼应用，直接通过调用 Retrieve 接口来查询知识库。
- 本接口不具备幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 10 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/SubmitIndexJob)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:SubmitIndexJob</span></td><td><span>create</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
POST /{WorkspaceId}/index/submit_index_job HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td>string</td><td>是</td><td><div><p>知识库所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><span>llm-3shx2gu255oqxxxx</span></td></tr><tr><td><span>IndexId</span></td><td>string</td><td>是</td><td><div><p>知识库 ID，即 <strong>CreateIndex</strong> 接口返回的<code>Data.Id</code>。</p></div></td><td><span>79c0alxxxx</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>Code</span></td><td>string</td><td><div><p>错误状态码。</p></div></td><td><span>Index.InvalidParameter</span></td></tr><tr><td><span /><span>Data</span></td><td>object</td><td><div><p>接口返回的业务字段。</p></div></td><td><span /></td></tr><tr><td><span>Id</span></td><td>string</td><td><div><p>任务 ID，即调用 <strong>GetIndexJobStatus</strong> 接口时需要用到的任务<code>JobId</code>。</p></div></td><td><span>eFDr2fGRzP9gdDZWAdo3xxxx</span></td></tr><tr><td><span>IndexId</span></td><td>string</td><td><div><p>知识库 ID。</p></div></td><td><span>79c0alxxxx</span></td></tr><tr><td><span>Message</span></td><td>string</td><td><div><p>错误信息。</p></div></td><td><span>Required parameter(%s) missing or invalid, please check the request parameters.</span></td></tr><tr><td><span>RequestId</span></td><td>string</td><td><div><p>请求 ID。</p></div></td><td><span>17204B98-xxxx-4F9A-8464-2446A84821CA</span></td></tr><tr><td><span>Status</span></td><td>string</td><td><div><p>接口返回的状态码。</p></div></td><td><span>200</span></td></tr><tr><td><span>Success</span></td><td>boolean</td><td><div><p>接口调用是否成功，可能值：</p> <ul><li>true：成功。</li> <li>false：失败。</li></ul></div></td><td><span>true</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "Code": "Index.InvalidParameter",
  "Data": {
    "Id": "eFDr2fGRzP9gdDZWAdo3xxxx",
    "IndexId": "79c0alxxxx"
  },
  "Message": "Required parameter(%s) missing or invalid, please check the request parameters.",
  "RequestId": "17204B98-xxxx-4F9A-8464-2446A84821CA",
  "Status": 200,
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

<table><thead><tr><th>HTTP status code</th><th>错误码</th><th>错误信息</th><th>描述</th></tr></thead><tbody><tr><td><span>400</span></td><td><span>RagIndex.IdempotentParameterMismatch</span></td><td><span>The request uses the same client token as a previous, but non-identical request. Do not reuse a client token with different requests, unless the requests are identical.</span></td><td><span>该请求使用与先前但不相同的请求相同的客户端令牌。不要重复使用具有不同请求的客户端令牌，除非请求相同。</span></td></tr></tbody></table>

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

<table><thead><tr><th>变更时间</th><th>变更内容概要</th><th>操作</th></tr></thead></table>

暂无变更历史
