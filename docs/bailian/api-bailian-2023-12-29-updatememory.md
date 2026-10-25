> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# UpdateMemory - 更新长期记忆体

> 更新指定长期记忆体的描述信息。

<meta property="og:title" content="UpdateMemory - 更新长期记忆体" />

## 接口说明 <span id="api-detail-51" />

- 本接口具有幂等性。

<strong>限流说明：</strong> 请确保两次请求间隔至少 1 秒，否则可能触发系统限流。如遇限流，请稍后重试。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/UpdateMemory)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:UpdateMemory</span></td><td><span>update</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
PUT /{workspaceId}/memories/{memoryId} HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>workspaceId</span></td><td>string</td><td>是</td><td><div><p>长期记忆体所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><span>llm-3z7uw7fwz0vexxxx</span></td></tr><tr><td><span>memoryId</span></td><td>string</td><td>是</td><td><div><p>长期记忆体 ID，对应<a href="https://help.aliyun.com/zh/model-studio/developer-reference/api-bailian-2023-12-29-creatememory">CreateMemory</a>接口返回的<code>memoryId</code>。</p></div></td><td><span>6bff4f317a14442fbc9f73d29dbdxxxx</span></td></tr><tr><td><span>description</span></td><td>string</td><td>否</td><td><div><p>新的描述信息。长度为 1\~50 个字符，支持 Unicode 中 letter 分类下的字符（其中包括英文、中文和数字等）。可以包含半角冒号（:）、下划线（\_）、半角句号（.）或者短划线（-）。</p></div></td><td><span>我的大模型应用\$APP\_ID关于B用户的长期记忆体</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>requestId</span></td><td>string</td><td><div><p>请求 ID。</p></div></td><td><span>6a71f2d9-f1c9-913b-818b-11402910xxxx</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "requestId": "6a71f2d9-f1c9-913b-818b-11402910xxxx"
}
```

## 错误码 <span id="h2-url-6" />

<table><thead><tr><th>HTTP status code</th><th>错误码</th><th>错误信息</th><th>描述</th></tr></thead><tbody><tr><td><span>400</span></td><td><span>Memory.DescriptionInvalid</span></td><td><span>Input parameter description is invalid.</span></td><td><span>长期记忆的描述信息无效</span></td></tr><tr><td><span>404</span></td><td><span>Memory.MemoryIdNotFound</span></td><td><span>Memory Id not exist or is not authorized.</span></td><td><span>memoryId 未找到</span></td></tr><tr><td><span>500</span></td><td><span>Memory.InternalError</span></td><td><span>Memory service inner exception.</span></td><td><span>长期记忆服务内部异常。</span></td></tr></tbody></table>

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。
