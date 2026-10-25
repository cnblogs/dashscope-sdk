> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# UpdateTableFromAuthorizedOss

> 使用已授权OSS Bucket中的文件更新阿里云百炼数据连接中表格连接器中的表格。

<meta property="og:title" content="UpdateTableFromAuthorizedOss" />

## 接口说明 <span id="api-detail-51" />

- 请确保该 OSS Bucket 与阿里云百炼同属一个阿里云账号（主账号），并已按[从 OSS 导入数据配置说明](/zh/model-studio/data-import-instructions)完成授权。

  - 支持的 Bucket 存储类型不包括归档、冷归档或深度冷归档。支持内容加密的 Bucket。支持公共读写/公共读/私有的 Bucket。
  - 如需使用开启 [Referer 防盗链](https://help.aliyun.com/zh/oss/user-guide/hotlink-protection)的 Bucket，须参考[仅允许受信任的网站访问](https://help.aliyun.com/zh/oss/user-guide/hotlink-protection)将域名`*.console.aliyun.com`添加到白名单 Referer 中。
- RAM 用户（子账号）需要首先获取阿里云百炼的 [API 权限](/zh/model-studio/grant-data-access-permission-to-ram-user)（需要`AliyunBailianDataFullAccess`，已包括 sfm:UpdateTableFromAuthorizedOss 权限点）并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)方可调用本接口。阿里云账号（主账号）可直接调用无须授权。建议您通过最新版[阿里云百炼 SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29)来调用本接口。
- 本接口不具有幂等性。

<strong>限流说明：</strong>
本接口频繁调用会被限流，频率请勿超过 5 次/秒。如遇限流，请稍后重试。

## 调试 <span id="api-doc-debug" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

## 授权信息 <span id="api-detail-32" />

当前API暂无授权信息透出。

## 请求语法 <span id="api-detail-53" />

```plaintext
PUT /{WorkspaceId}/datacenter/table/fromoss/{TableId} HTTP/1.1
```

## 路径参数 <span id="api-doc-request-path-parameters" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>文件所属的业务空间 ID。获取方式请参见<a href="/zh/model-studio/use-workspace">如何使用业务空间</a>。</p></div></td><td><p>llm-3z7uw7fwz0vxxxx</p></td></tr><tr><td><span>TableId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>更新的表格 ID</p></div></td><td><p>table\_100b51399c404966b7d49f71e388a3fd\_12738336</p></td></tr></tbody></table>

## 请求参数 <span id="api-detail-36" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.64%" }} /><col style={{ width: "11.82%" }} /><col style={{ width: "10.84%" }} /><col style={{ width: "30.47%" }} /><col style={{ width: "23.6%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span>OssBucket</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>OSS Bucket 名称，详见<a href="https://help.aliyun.com/zh/oss/user-guide/oss-bucket-overview">存储空间</a>。</p></div></td><td><p>yinghuo-ai</p></td></tr><tr><td><span>OssRegionId</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>OSS Bucket 的地域 ID。获取方式请参见 <a href="https://help.aliyun.com/zh/oss/user-guide/regions-and-endpoints">OSS 地域和访问域名</a>。</p></div></td><td><p>cn-beijing</p></td></tr><tr><td><span>OssKey</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>导入文件在 OSS Bucket 中的键名（Key），详见<a href="https://help.aliyun.com/zh/oss/user-guide/object-overview">对象命名</a>。</p></div></td><td><p>a0deedbce4a8162b8d66c63ace28330c</p></td></tr><tr><td><span>UpdateMode</span></td><td><p>string</p></td><td><p><span>是</span></p></td><td><div><p>更新模式，仅支持 APPEND（追加）和 OVERWRITE（覆盖）</p></div></td><td><p>OVERWRITE</p></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.497064%" }} /><col style={{ width: "13.773565%" }} /><col style={{ width: "34.119707%" }} /><col style={{ width: "26.609663%" }} /></colgroup><thead><tr><th><p><strong>名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><span /> <span /></td><td><p>object</p></td><td><div><p>Schema of Response</p></div></td><td /></tr><tr><td><span>Code</span></td><td><p>string</p></td><td><div><p>错误状态码。</p></div></td><td><p>DataCenter.FileTooLarge</p></td></tr><tr><td><span /> <span>Data</span></td><td><p>object</p></td><td><div><p>接口业务数据字段。</p></div></td><td /></tr><tr><td><span>TableId</span></td><td><p>string</p></td><td><div><p>和入参 TableId 一致</p></div></td><td><p>table\_df96ebd5da8640e5a0991b3d15f39d4d\_12792097</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>当前表格状态，一般上传之后会变成“TO\_IMPORT”，表示系统已经接收上传的文件，等待调度写入到数据表中</p></div></td><td><p>TO\_IMPORT</p></td></tr><tr><td><span>Message</span></td><td><p>string</p></td><td><div><p>错误信息</p></div></td><td><p>Required parameter(%s) missing or invalid, please check the request parameters.</p></td></tr><tr><td><span>RequestId</span></td><td><p>string</p></td><td><div><p>Id of the request</p></div></td><td><p>7BA8ADD9-53D6-53F0-918F-A1E776AD230E</p></td></tr><tr><td><span>Status</span></td><td><p>string</p></td><td><div><p>接口返回的状态码</p></div></td><td><p>200</p></td></tr><tr><td><span>Success</span></td><td><p>boolean</p></td><td><div><p>是否成功</p></div></td><td><p>true</p></td></tr></tbody></table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```json
{
  "Code": "DataCenter.FileTooLarge",
  "Data": {
    "TableId": "table_df96ebd5da8640e5a0991b3d15f39d4d_12792097",
    "Status": "TO_IMPORT"
  },
  "Message": "Required parameter(%s) missing or invalid, please check the request parameters.",
  "RequestId": "7BA8ADD9-53D6-53F0-918F-A1E776AD230E",
  "Status": "200",
  "Success": true
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

更多信息，参考[变更详情](https://api.aliyun.com/document/bailian/2023-12-29/UpdateTableFromAuthorizedOss#workbench-doc-change-demo)。
