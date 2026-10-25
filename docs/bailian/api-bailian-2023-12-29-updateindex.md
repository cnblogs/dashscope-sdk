> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# UpdateIndex - 更新知识库

> 更新指定知识库的部分配置。

<meta property="og:title" content="UpdateIndex - 更新知识库" />

## 接口说明 <span id="api-detail-51" />

本接口不支持国际站。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/UpdateIndex)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:UpdateIndex</span></td><td><span>update</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
POST /{WorkspaceId}/index/update HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td>string</td><td>是</td><td><div><p>业务空间 ID</p></div></td><td><span>llm-3shx2gu255oqxxxx</span></td></tr><tr><td><span>Id</span></td><td>string</td><td>是</td><td><div><p>知识库 ID</p></div></td><td><span>79c0alxxxx</span></td></tr><tr><td><span>Name</span></td><td>string</td><td>否</td><td><div><p>知识库名称</p></div></td><td><span>企业帮助文档库</span></td></tr><tr><td><span>Description</span></td><td>string</td><td>否</td><td><div><p>知识库描述</p></div></td><td><span>企业知识库</span></td></tr><tr><td><span>RerankMinScore</span></td><td>string</td><td>否</td><td><div><p>排序最低分数，取值范围\[0-1]</p></div></td><td><span>0.01</span></td></tr><tr><td><span>DenseSimilarityTopK</span></td><td>integer</td><td>否</td><td><div><p>向量检索 Top K，通过生成输入文本的向量并在知识库中检索与其向量表示最相似的 K 个文本切片。K 的取值范围\[0-100]。 <code>DenseSimilarityTopK</code>和<code>SparseSimilarityTopK</code>二者之和小于等于 200。</p> <p>默认值为 100。</p></div></td><td><span>100</span></td></tr><tr><td><span>SparseSimilarityTopK</span></td><td>integer</td><td>否</td><td><div><p>关键词检索 TopK，即在知识库中查找与输入文本的关键词精确匹配的切片。它可以帮助您过滤掉无关的文本切片，提供更准确的结果。 取值范围\[0-100]。 <code>DenseSimilarityTopK</code>和<code>SparseSimilarityTopK</code>二者之和小于等于 200。</p> <p>默认值为：100。</p></div></td><td><span>100</span></td></tr><tr><td><span>PipelineCommercialType</span></td><td>string</td><td>否</td><td><div><p>知识库的规格类型。取值范围：</p> <ul><li><p>standard：标准版</p></li> <li><p>enterprise：旗舰版</p></li></ul></div></td><td><span>standard</span></td></tr><tr><td><span>PipelineCommercialCu</span></td><td>integer</td><td>否</td><td><div><p>指定旗舰版知识库的 RCU 数量，即仅当 pipelineCommercialType 指定 enterprise 时才需要传入。</p> <p>取值范围：\[1-200]。</p></div></td><td><span>3</span></td></tr></tbody></table>

参数 Name，Description，RerankMinScore ，至少填写一项，不能同时为空。

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>RequestId</span></td><td>string</td><td><div><p>请求 ID</p></div></td><td><span>17204B98-7734-4F9A-8464-xxx</span></td></tr><tr><td><span /><span>Data</span></td><td>object</td><td><div><p>接口业务数据字段</p></div></td><td><span /></td></tr><tr><td><span>Id</span></td><td>string</td><td><div><p>知识库 ID</p></div></td><td><span>79c0alxxxx</span></td></tr><tr><td><span>Status</span></td><td>string</td><td><div><p>接口返回的状态码</p></div></td><td><span>200</span></td></tr><tr><td><span>Success</span></td><td>boolean</td><td><div><p>接口调用是否成功，可能值为：</p> <p>true：成功</p> <p>false：失败</p></div></td><td><span>true</span></td></tr><tr><td><span>Message</span></td><td>string</td><td><div><p>错误信息</p></div></td><td><span>Required parameter(%s) missing or invalid, please check the request parameters.</span></td></tr><tr><td><span>Code</span></td><td>string</td><td><div><p>状态码</p></div></td><td><span>Success</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "RequestId": "17204B98-7734-4F9A-8464-xxx",
  "Data": {
    "Id": "79c0alxxxx"
  },
  "Status": 200,
  "Success": true,
  "Message": "Required parameter(%s) missing or invalid, please check the request parameters.",
  "Code": "Success"
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

<table><thead><tr><th>变更时间</th><th>变更内容概要</th><th>操作</th></tr></thead><tbody><tr><td><span>2026-01-19</span></td><td><span>新增 OpenAPI</span></td><td><a href="https://api.aliyun.com/document/bailian/2023-12-29/UpdateIndex?updateTime=2026-01-19#workbench-doc-change-demo">查看变更详情</a></td></tr></tbody></table>
