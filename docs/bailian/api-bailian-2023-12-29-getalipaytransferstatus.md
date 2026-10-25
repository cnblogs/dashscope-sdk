> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetAlipayTransferStatus - 查询支付宝打赏状态

> 查询应用中绑定的支付宝钱包的打赏状态。

<meta property="og:title" content="GetAlipayTransferStatus - 查询支付宝打赏状态" />

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/GetAlipayTransferStatus)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:GetAlipayTransferStatus</span></td><td><span>none</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
GET /openapi/alipay/transfer/status HTTP/1.1
POST /openapi/alipay/transfer/status HTTP/1.1GET /openapi/alipay/transfer/status HTTP/1.1
POST /openapi/alipay/transfer/status HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>workspace\_id</span></td><td>string</td><td>否</td><td><div><p>百炼工作空间 ID</p></div></td><td><span>llm-cxxxxxxb8d47ks</span></td></tr><tr><td><span>code</span></td><td>string</td><td>否</td><td><div><p>支付宝打赏链接请求返回的打赏 code</p></div></td><td><span>xxx-xxxx</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>requestId</span></td><td>string</td><td><div><p>请求的唯一 ID</p></div></td><td><span>6a71f2d9-f1c9-913b-818b-11402910xxxx</span></td></tr><tr><td><span /><span>data</span></td><td>object</td><td><div><p>打赏的结果数据</p></div></td><td><span /></td></tr><tr><td><span>code</span></td><td>string</td><td><div><p>打赏对应的 code</p></div></td><td><span>0</span></td></tr><tr><td><span>walletItemCode</span></td><td>string</td><td><div><p>支付宝商品钱包 code</p></div></td><td><span>xsdfsdf</span></td></tr><tr><td><span>mainAccountId</span></td><td>string</td><td><div><p>主账号 id（API 忽略）</p></div></td><td><span>1007576424487905</span></td></tr><tr><td><span>accountId</span></td><td>string</td><td><div><p>子账号 id（API 忽略）</p></div></td><td><span>1348393307144609</span></td></tr><tr><td><span>alipayOrderId</span></td><td>string</td><td><div><p>支付宝的订单 id</p></div></td><td><span>1234234</span></td></tr><tr><td><span>status</span></td><td>long</td><td><div><p>打赏状态</p> <ul><li>1 （成功）</li> <li>0 （删除）</li> <li>2 （待打赏）</li> <li>3 （取消）</li> <li>4 （退款）</li> <li>5（关闭）</li> <li>6（失败）</li> <li>7（纠纷、异常）</li></ul></div></td><td><span>1</span></td></tr><tr><td><span>creator</span></td><td>string</td><td><div><p>创建者（API 忽略）</p></div></td><td><span>234234</span></td></tr><tr><td><span>modifier</span></td><td>string</td><td><div><p>修改者（API 忽略）</p></div></td><td><span>234234</span></td></tr><tr><td><span>alipayOrderDetail</span></td><td>string</td><td><div><p>支付细节（API 忽略）</p></div></td><td><span>\{}</span></td></tr><tr><td><span>title</span></td><td>string</td><td><div><p>转账标题</p></div></td><td><span>test</span></td></tr><tr><td><span>qrURL</span></td><td>string</td><td><div><p>打赏链接（API 忽略）</p></div></td><td><span>[https://xxx.aliyun.com](https://xxx.aliyun.com)</span></td></tr><tr><td><span>transAmount</span></td><td>string</td><td><div><p>订单总金额，单位为：元。</p></div></td><td><span>0.22</span></td></tr><tr><td><span>scope</span></td><td>string</td><td><div><p>打赏应用的所属状态</p></div></td><td><span>publish</span></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "requestId": "6a71f2d9-f1c9-913b-818b-11402910xxxx\n",
  "data": {
    "code": 0,
    "walletItemCode": "xsdfsdf",
    "mainAccountId": 1007576424487905,
    "accountId": 1348393307144609,
    "alipayOrderId": 1234234,
    "status": 1,
    "creator": 234234,
    "modifier": 234234,
    "alipayOrderDetail": {},
    "title": "test",
    "qrURL": "https://xxx.aliyun.com",
    "transAmount": 0.22,
    "scope": "publish"
  }
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。
