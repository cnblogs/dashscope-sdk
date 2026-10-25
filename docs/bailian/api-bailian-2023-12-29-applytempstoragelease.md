> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# ApplyTempStorageLease - 申请临时文件上传许可

> 该接口用于高代码部署，其他场景暂不支持。用于申请临时文件上传许可，之后需要自己完成文件上传动作。

<meta property="og:title" content="ApplyTempStorageLease - 申请临时文件上传许可" />

## 接口说明 <span id="api-detail-51" />

1、该接口用于高代码部署，其他场景暂不支持。
2、通过此接口获取到临时文件上传许可之后，需要自己完成文件上传动作。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/ApplyTempStorageLease)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:ApplyTempStorageLease</span></td><td><span>none</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
POST /{WorkspaceId}/datacenter HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>FileName</span></td><td>string</td><td>是</td><td><div><p>文件名称，需要包含文件后缀</p></div></td><td><span>example.txt</span></td></tr><tr><td><span>SizeInBytes</span></td><td>long</td><td>是</td><td><div><p>文件大小（单位字节）</p></div></td><td><span>1024</span></td></tr><tr><td><span>WorkspaceId</span></td><td>string</td><td>是</td><td><div><p>百炼业务空间 id</p></div></td><td><span>llm-mbhn96xxxx</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table><thead><tr><th>名称</th><th>类型</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span /><span /></td><td>object</td><td><div><p>Schema of Response</p></div></td><td><span /></td></tr><tr><td><span>Code</span></td><td>string</td><td><div><p>错误状态码。</p></div></td><td><span>DataCenter.FileTooLarge</span></td></tr><tr><td><span /><span>Data</span></td><td>object</td><td><div><p>接口业务数据字段。</p></div></td><td><span /></td></tr><tr><td><span>TempStorageLeaseId</span></td><td>string</td><td><div><p>租约唯一 ID，后续应用内获取上传的文件时，需要使用该参数。</p></div></td><td><span>1e6a159107384782be5e45ac4759b247.1719325231035</span></td></tr><tr><td><span /><span>Param</span></td><td>object</td><td><div><p>用于上传文件的 HTTP 参数。</p></div></td><td><span /></td></tr><tr><td><span>Headers</span></td><td>any</td><td><div><p>需要放到 Header 中的 K-V 字段，K 和 V 均为字符串。</p></div></td><td><span>Content-Type: application/json</span></td></tr><tr><td><span>Method</span></td><td>string</td><td><div><p>HTTP 调用方法，可能值为： PUT POST</p></div></td><td><span>PUT</span></td></tr><tr><td><span>Url</span></td><td>string</td><td><div><p>文件上传的授权 URL 地址。</p></div></td><td><span>[https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf?Expires=1716699536\&OSSAccessKeyId=YOUR\_ACCESS\_KEY\_ID\&Signature=YOUR\_SIGNATURE](https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf?Expires=1716699536\&OSSAccessKeyId=YOUR_ACCESS_KEY_ID\&Signature=YOUR_SIGNATURE)</span></td></tr><tr><td><span>Message</span></td><td>string</td><td><div><p>错误信息。</p></div></td><td><span>User not authorized to operate on the specified resource</span></td></tr><tr><td><span>RequestId</span></td><td>string</td><td><div><p>请求 ID。</p></div></td><td><span>778C0B3B-xxxx-5FC1-A947-36EDD13606AB</span></td></tr><tr><td><span>Status</span></td><td>string</td><td><div><p>接口返回的状态码。</p></div></td><td><span>200</span></td></tr><tr><td><span>Success</span></td><td>boolean</td><td><div><p>接口调用是否成功</p></div></td><td><span /></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "Code": "DataCenter.FileTooLarge",
  "Data": {
    "TempStorageLeaseId": "1e6a159107384782be5e45ac4759b247.1719325231035",
    "Param": {
      "Headers": "Content-Type: application/json",
      "Method": "PUT",
      "Url": "https://bailian-datahub-data-origin-prod.oss-cn-hangzhou.aliyuncs.com/1005426495169178/10024405/68abd1dea7b6404d8f7d7b9f7fbd332d.1716698936847.pdf?Expires=1716699536&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE"
    }
  },
  "Message": "User not authorized to operate on the specified resource",
  "RequestId": "778C0B3B-xxxx-5FC1-A947-36EDD13606AB",
  "Status": 200,
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

<table><thead><tr><th>变更时间</th><th>变更内容概要</th><th>操作</th></tr></thead><tbody><tr><td><span>2025-11-04</span></td><td><span>新增 OpenAPI</span></td><td><a href="https://api.aliyun.com/document/bailian/2023-12-29/ApplyTempStorageLease?updateTime=2025-11-04#workbench-doc-change-demo">查看变更详情</a></td></tr></tbody></table>
