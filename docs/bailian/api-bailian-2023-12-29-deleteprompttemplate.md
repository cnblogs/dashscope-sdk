> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DeletePromptTemplate - 删除Prompt模板

> 基于模板Id删除Prompt模板。

<meta property="og:title" content="DeletePromptTemplate - 删除Prompt模板" />

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/DeletePromptTemplate)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:DeletePromptTemplate</span></td><td><span>delete</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
DELETE /{workspaceId}/promptTemplates/{promptTemplateId} HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>workspaceId</span></td><td>string</td><td>是</td><td><div><p>业务空间 Id</p></div></td><td><span>llm-us9hjmt32nysdxxx</span></td></tr><tr><td><span>promptTemplateId</span></td><td>string</td><td>是</td><td><div><p>模板 Id</p></div></td><td><span>6e49109bfeb94a39bb268f4e483ccxxx</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>requestId</span></td><td>string</td><td><div><p>请求 Id</p></div></td><td><span>FE9B6CBF-47E6-5D76-9C5D-B814DD5ABxxx</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "requestId": "FE9B6CBF-47E6-5D76-9C5D-B814DD5ABxxx"
}
```

## 错误码 <span id="h2-url-6" />

<table><thead><tr><th>HTTP status code</th><th>错误码</th><th>错误信息</th><th>描述</th></tr></thead><tbody><tr><td><span>404</span></td><td><span>PromptTemplate.TemplateNotFound</span></td><td><span>Prompt template not found.</span></td><td><span>未找到Prompt模板</span></td></tr><tr><td><span>500</span></td><td><span>PromptTemplate.InternalError</span></td><td><span>Prompt template service inner exception.</span></td><td><span>prompt模板服务内部异常</span></td></tr></tbody></table>

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。
