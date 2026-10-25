> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# GetIndexMonitor - 获取知识库监控数据

> 调用GetIndexMonitor接口，查询指定知识库在特定时间范围内的监控数据。这些数据对于性能分析、容量规划和成本管理至关重要。 监控数据主要包含两大维度： 存储监控：获取知识库的索引存储限额和当前使用量。 检索（QPS）监控：获取查询时间段内总的及按时间窗口细分的检索性能指标，包括QPS峰值、总请求数、平均QPS，并细分为成功、失败和被限流的请求。

<meta property="og:title" content="GetIndexMonitor - 获取知识库监控数据" />

## 接口说明 <span id="api-detail-51" />

本接口不支持国际站。

## 调试 <span id="api-detail-23" />

您可以在OpenAPI Explorer中直接运行该接口，免去您计算签名的困扰。运行成功后，OpenAPI Explorer可以自动生成SDK代码示例。

[调试](https://api.aliyun.com/api/bailian/2023-12-29/GetIndexMonitor)

## 授权信息 <span id="api-detail-32" />

下表是API对应的授权信息，可以在RAM权限策略语句的`Action`元素中使用，用来给RAM用户或RAM角色授予调用此API的权限。具体说明如下：

- 操作：是指具体的权限点。
- 访问级别：是指每个操作的访问级别，取值为写入（Write）、读取（Read）或列出（List）。
- 资源类型：是指操作中支持授权的资源类型。具体说明如下：

  - 对于必选的资源类型，用前面加 \* 表示。
  - 对于不支持资源级授权的操作，用`全部资源`表示。
- 条件关键字：是指云产品自身定义的条件关键字。
- 关联操作：是指成功执行操作所需要的其他权限。操作者必须同时具备关联操作的权限，操作才能成功。

<table><thead><tr><th>操作</th><th>访问级别</th><th>资源类型</th><th>条件关键字</th><th>关联操作</th></tr></thead><tbody><tr><td><span>sfm:GetIndexMonitor</span></td><td><span>get</span></td><td><div><div><span>\*</span><span>全部资源</span></div><div><code>\*</code></div></div></td><td><ul>无</ul></td><td>无</td></tr></tbody></table>

## 请求语法 <span id="api-detail-53" />

```
GET /{WorkspaceId}/rag/index/monitor HTTP/1.1
```

## 请求参数 <span id="api-detail-36" />

<table><thead><tr><th>名称</th><th>类型</th><th>必填</th><th>描述</th><th>示例值</th></tr></thead><tbody><tr><td><span>WorkspaceId</span></td><td>string</td><td>是</td><td><div><p>知识库所在工作空间 ID</p></div></td><td><span>llm-3shx2gu255oqxxxx</span></td></tr><tr><td><span>IndexId</span></td><td>string</td><td>是</td><td><div><p>目标知识库的唯一 ID</p></div></td><td><span>kb-123456xxxx</span></td></tr><tr><td><span>StartTimestamp</span></td><td>long</td><td>是</td><td><div><p>查询起始时间，秒级 Unix 时间戳</p></div></td><td><span>1767604500</span></td></tr><tr><td><span>EndTimestamp</span></td><td>long</td><td>是</td><td><div><p>查询结束时间，最大支持起始时间+30d，秒级 Unix 时间戳</p></div></td><td><span>1767604500</span></td></tr></tbody></table>

## 返回参数 <span id="api-detail-41" />

<table>
  <thead>
    <tr>
      <th>
        名称
      </th>

      <th>
        类型
      </th>

      <th>
        描述
      </th>

      <th>
        示例值
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td />

      <td>
        object
      </td>

      <td>
        Schema of Response
      </td>

      <td />
    </tr>

    <tr>
      <td>
        RequestId
      </td>

      <td>
        string
      </td>

      <td>
        请求 ID
      </td>

      <td>
        778C0B3B-xxxx-5FC1-A947-36EDD13606AB
      </td>
    </tr>

    <tr>
      <td>
        Code
      </td>

      <td>
        string
      </td>

      <td>
        状态码
      </td>

      <td>
        200
      </td>
    </tr>

    <tr>
      <td>
        Data
      </td>

      <td>
        any
      </td>

      <td>
        响应的核心数据对象。

        <strong>pipelineCommercialType</strong>(String):知识库规格

        - standard：标准版
        - enterprise：旗舰版

        <strong>storageMonitorData</strong>(Object): 知识库的存储监控数据

        - indexStorageLimit(Number): 知识库的索引存储限额，单位为 GB。
        - indexStorageUsage(Number): 知识库当前已占用的索引存储额度，单位为 GB。

        <strong>pipelineCommercialCu</strong>（Integer）：表示旗舰版知识库的 RCU，如 2。

        <strong>qpsMonitorData</strong> (Object): 知识库在整个查询时间段内的检索（QPS）聚合监控数据

        - peakQps(Integer): 整个时间段内的最高 QPS 峰值
        - totalRequests(Integer): 整个时间段内的总请求次数
        - avgQpsOfActiveSeconds(Number): 整个时间段内，所有活跃秒（即有调用的秒）的平均 QPS
        - monitorData(Array): 按时间窗口划分的详细监控数据数组。数组中的每个对象代表一个时间窗口的统计信息。

          <Accordion title="Details">
            <strong>子属性</strong>

            - successData(Object):该窗口内请求成功的统计数据。
            - limitData(Object): 该窗口内被限流的统计数据。
            - failData(Object): 该窗口内调用失败的统计数据。
            - peakQpsInWindowRange(Integer): 该窗口内的总 QPS 峰值（成功+限流+失败）。
            - totalRequests(Integer): 该窗口内的总请求数（成功+限流+失败）。
            - windowRange(Integer): 时间窗口的开始时间（秒级 Unix 时间戳）。
            - windowRangeEnd(Integer): 时间窗口的结束时间（秒级 Unix 时间戳）。
            - avgQpsOfActiveSeconds(Number): 该窗口内活跃秒的平均 QPS。

            <strong>successData, limitData, failData 这三个对象的内部结构完全相同，具体如下：</strong>

            - peakQpsInWindowRange(Integer): 对应状态下的 QPS 峰值。
            - totalRequests(Integer): 对应状态下的总请求数。
            - avgQpsOfActiveSeconds(Number): 对应状态下活跃秒的平均 QPS。
          </Accordion>
      </td>

      <td>
        \{
        "code": "Success",
        "status\_code": 200,
        "data": \{
        "pipelineCommercialType": "standard",       "storageMonitorData": Object\{...},
        "qpsMonitorData": Object\{...}
        },
        "success": true,
        "message": "success",
        "request\_id": "65d34b79-b97e-478e-a0a3-xxx",
        "status": "SUCCESS"
        }
      </td>
    </tr>

    <tr>
      <td>
        Message
      </td>

      <td>
        string
      </td>

      <td>
        状态信息
      </td>

      <td>
        success
      </td>
    </tr>

    <tr>
      <td>
        Success
      </td>

      <td>
        boolean
      </td>

      <td>
        请求是否成功
      </td>

      <td>
        true
      </td>
    </tr>

    <tr>
      <td>
        Status
      </td>

      <td>
        integer
      </td>

      <td>
        接口返回的状态码
      </td>

      <td>
        SUCCESS
      </td>
    </tr>
  </tbody>
</table>

## 示例 <span id="api-detail-1" />

正常返回示例

`JSON`格式

```
{
  "RequestId": "778C0B3B-xxxx-5FC1-A947-36EDD13606AB",
  "Code": 200,
  "Data": "{\n    \"code\": \"Success\",\n    \"status_code\": 200,\n    \"data\": {\n\"pipelineCommercialType\": \"standard\",       \"storageMonitorData\": Object{...},\n        \"qpsMonitorData\": Object{...}\n    },\n    \"success\": true,\n    \"message\": \"success\",\n    \"request_id\": \"65d34b79-b97e-478e-a0a3-xxx\",\n    \"status\": \"SUCCESS\"\n}",
  "Message": "success",
  "Success": true,
  "Status": 0
}
```

## 错误码 <span id="h2-url-6" />

访问[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)查看更多错误码。

## 变更历史 <span id="workbench-doc-change-demo" />

<table><thead><tr><th>变更时间</th><th>变更内容概要</th><th>操作</th></tr></thead><tbody><tr><td><span>2026-01-14</span></td><td><span>新增 OpenAPI</span></td><td><a href="https://api.aliyun.com/document/bailian/2023-12-29/GetIndexMonitor?updateTime=2026-01-14#workbench-doc-change-demo">查看变更详情</a></td></tr></tbody></table>
