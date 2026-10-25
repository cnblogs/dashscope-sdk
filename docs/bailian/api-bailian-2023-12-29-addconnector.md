> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# AddConnector

> 创建连接器，当前接口仅支持创建文件类型连接器。

<meta property="og:title" content="AddConnector" />

## 接口说明 <span id="api-detail-51" />

- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:AddCategory 权限点），并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)后，方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口不具备幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 5 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-doc-debug" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.92%" }} /><col style={{ width: "10.51%" }} /><col style={{ width: "35.68%" }} /><col style={{ width: "16.34%" }} /><col style={{ width: "10.52%" }} /></colgroup><thead><tr><th><p><strong>操作</strong></p></th><th><p><strong>访问级别</strong></p></th><th><p><strong>资源类型</strong></p></th><th><p><strong>条件关键字</strong></p></th><th><p><strong>关联操作</strong></p></th></tr></thead><tbody><tr><td><p>sfm:AddConnector</p></td><td><p>create</p></td><td><p><span>\*</span>全部资源</p> <p><code>\*</code></p></td><td><span>无</span></td><td><span>无</span></td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```plaintext
POST /{WorkspaceId}/datacenter/connector HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>知识库所在工作空间 ID</p></div></td><td><p>llm-3shx2gu255oxxxx</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>ConnectorName</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>连接器名称</p></div></td><td><p>test-connector</p></td></tr><tr><td><span>ConnectorType</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>连接器类型</p></div></td><td><p>FILE</p></td></tr><tr><td><span>Description</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>描述</p></div></td><td><p>文件连接器</p></td></tr><tr><td><span /> <span>FileConnectorConfig</span></td><td><p>object</p></td><td><p><span>否</span></p></td><td><div><p>文件连接器参数</p></div></td><td /></tr><tr><td><span>RegionId</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>存储 bucket 区域</p></div></td><td><p>cn-hangzhou</p></td></tr><tr><td><span>BucketName</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>存储 bucket 名称</p></div></td><td><p>zyb-docker-registry-jn</p></td></tr><tr><td><span>StorageType</span></td><td><p>string</p></td><td><p><span>否</span></p></td><td><div><p>文件存储位置 OSS\_CUSTOM：使用自有 OSS 存储 OSS\_PLATFORM：使用平台 OSS 存储</p></div></td><td><p>OSS\_CUSTOM</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码</p></div></td><td><p>Success</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段</p></div></td><td /></tr><tr><td><span>ConnectorId</span></td><td><p>string</p></td><td><div><p>连接器 ID</p></div></td><td><p>conn\_file\_e0c9db4030b2465a9478028f7d76cd92\_1234</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息</p></div></td><td><p>Current file status does not support delete.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>Id of the request</p></div></td><td><p>35A267BF-FBFA-54DB-8394-AA3B0742D833</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>接口调用是否成功，可能值为：</p> <ul><li><p>true：成功</p></li> <li><p>false：失败</p></li></ul></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "Success",
  "Data": {
    "ConnectorId": "conn_file_e0c9db4030b2465a9478028f7d76cd92_1234"
  },
  "Message": "Current file status does not support delete.",
  "RequestId": "35A267BF-FBFA-54DB-8394-AA3B0742D833",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/AddConnector#workbench-doc-change-demo)。
