> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetAlipayUrl - 获取支付宝打赏URL

> 获取应用上支付宝的打赏链接。

<meta property="og:title" content="GetAlipayUrl - 获取支付宝打赏URL" />

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/GetAlipayUrl)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:GetAlipayUrl</span></td><td><span>none</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
GET /openapi/alipay/transfer/url HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>workspace\_id</span></td><td>string</td><td>否</td><td><div><p>百炼工作空间的 id</p></div></td><td><span>xxxllm-czb8d47ks</span></td></tr><tr><td><span>app\_id</span></td><td>string</td><td>否</td><td><div><p>百炼应用的 id</p></div></td><td><span>asfasdfxxasdf</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>requestId</span></td><td>string</td><td><div><p>请求的唯一 id</p></div></td><td><span>6a71f2d9-f1c9-913b-818b-11402910xxxx</span></td></tr><tr><td><span /><span>data</span></td><td>object</td><td><div><p>返回数据</p></div></td><td><span /></td></tr><tr><td><span>qrUrl</span></td><td>string</td><td><div><p>生成的打赏链接</p></div></td><td><span>[https://xxxxxx.aliyun-inc.com](https://xxxxxx.aliyun-inc.com)</span></td></tr><tr><td><span>code</span></td><td>string</td><td><div><p>对应的打赏的 code，用于回查打赏状态</p></div></td><td><span>xxsdfasfw</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "requestId": "6a71f2d9-f1c9-913b-818b-11402910xxxx\n",
  "data": {
    "qrUrl": "https://xxxxxx.aliyun-inc.com",
    "code": "xxsdfasfw"
  }
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。
