> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 异步任务管理 API

> 阿里云百炼的部分模型（如图像生成、视频生成等）因处理时间较长，采用异步调用机制，通常需要调用两个接口完成操作：先创建任务获取 ID，再通过该 ID 查询结果。为了方便管理异步任务，阿里云百炼提供了一组通用的异步任务接口，支持查询单个任务结果、批量查询多个任务状态、以及取消正在排队且尚未处理的任务。

<meta property="og:title" content="异步任务管理 API" />

## 前提条件 <span id="c445b17ce67lk" />

异步任务API通过HTTP进行调用。

在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## 查询异步任务结果接口 <span id="d6e9544d10arj" />

<strong>API描述</strong>：根据`任务ID（task_id）`查询任务状态和任务结果。

<strong>流量限制</strong>：20 QPS，即每秒每个账号（含主账号及其子账号）最多发起 20 次请求。

<Tip>
  - 支持查询当前 API Key 所属阿里云主账号下的所有任务（包括该主账号下通过任意 API Key 提交的任务），但无法查询其他主账号提交的任务。
  - 异步任务在完成后通常保留 24 小时 （具体以对应任务的 API 文档为准），超时后系统将自动清理历史任务数据。
</Tip>

<Note>
  查询任务结果的次数不影响任务的执行。建议根据任务类型合理设置查询间隔：

  - 较快的任务（如文本向量）可以使用较短的查询间隔。
  - 较慢的任务（如图像生成、视频生成）建议使用较长的查询间隔。
  - 避免过于频繁地查询任务状态，以防触发限流（20 QPS）。
</Note>

#### 请求接口 <span id="b4c46c6698p5y" />

```http
GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks{task_id}
```

#### 入参描述 <span id="e4f146cf879vh" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.779825%" }} /><col style={{ width: "14.742968%" }} /><col style={{ width: "13.967022%" }} /><col style={{ width: "9.796314%" }} /><col style={{ width: "26.479146%" }} /><col style={{ width: "18.234724%" }} /></colgroup><thead><tr><th><p><strong>传参方式</strong></p></th><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Header</p></td><td><p>Authorization</p></td><td><p>String</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer sk-xxx</p></td><td><p>Bearer sk-xxx</p></td></tr><tr><td><p>Path</p></td><td><p>task\_id</p></td><td><p>String</p></td><td><p>是</p></td><td><p>需查询的任务ID</p></td><td><p>a8532587-xxxx-xxxx-xxxx-0c46b17950d1</p></td></tr></tbody></table>

#### 出参描述 <span id="0e0827df7a2pq" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.77%" }} /><col style={{ width: "10.69%" }} /><col style={{ width: "35.99%" }} /><col style={{ width: "32.55%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码</p></td><td><p>7574ee8f-xxxx-xxxx-xxxx-11c33ab46e51</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><ul><li><p>任务成功时，<code>output</code> 包含模型生成的结果对象，具体内容因任务类型而异</p></li> <li><p>任务失败或部分失败时，<code>output</code> 会返回对应的 <code>code</code> 和 <code>message</code>，说明失败原因</p></li> <li><p>对于包含多个子任务的任务，<code>output</code> 可能同时包含成功的子任务结果和失败的错误信息</p></li></ul></td><td><p>-</p></td></tr><tr><td><p>output.task\_id</p></td><td><p>String</p></td><td><p>查询任务的 task\_id</p></td><td><p>a8532587-xxxx-xxxx-xxxx-0c46b17950d1</p></td></tr><tr><td><p>output.task\_status</p></td><td><p>String</p></td><td><p>任务状态</p> <ul><li><p>对于包含多个子任务的任务，只要有一个子任务成功，整个任务会被标记为成功</p></li> <li><p>对于失败的子任务，会在 output 中展示具体报错</p></li></ul></td><td><p>任务状态：</p> <ul><li><p>PENDING：任务排队中</p></li> <li><p>RUNNING：任务处理中</p></li> <li><p>SUCCEEDED：任务执行成功</p></li> <li><p>FAILED：任务执行失败</p></li> <li><p>UNKNOWN：任务不存在或状态未知</p></li></ul></td></tr><tr><td><p>output.submit\_time</p></td><td><p>String</p></td><td><p>任务提交时间</p></td><td><p>2023-12-20 21:36:31.896</p></td></tr><tr><td><p>output.scheduled\_time</p></td><td><p>String</p></td><td><p>任务调度时间，即开始执行时间</p></td><td><p>2023-12-20 21:36:39.009</p></td></tr><tr><td><p>output.end\_time</p></td><td><p>String</p></td><td><p>任务结束时间</p></td><td><p>2023-12-20 21:36:45.913</p></td></tr><tr><td><p>output.code</p></td><td><p>String</p></td><td><p>错误码，仅在任务失败时返回</p></td><td><p>-</p></td></tr><tr><td><p>output.message</p></td><td><p>String</p></td><td><p>错误信息，仅任务失败时返回</p></td><td><p>-</p></td></tr><tr><td><p>output.task\_metrics</p></td><td><p>Object</p></td><td><p>任务指标，包含子任务状态的统计信息</p></td><td><p><code>\{</code></p> <p><code>"TOTAL": 4, //子任务总数</code></p> <p><code>"SUCCEEDED": 3, //子任务成功数</code></p> <p><code>"FAILED": 1 //子任务失败数</code></p> <p><code>}</code></p></td></tr><tr><td><p>usage</p></td><td><p>Object</p></td><td><p>本次请求产生的计量信息，根据实际任务的不同，相关的计量信息也有所不同</p></td><td><p><code>"usage": \{"image\_count": 1}</code></p></td></tr></tbody></table>

#### 请求示例 <span id="aeeeae52fa6dl" />

<Tabs>
  <Tab title="curl">
    ```bash
    curl -X GET 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks73205176-xxxx-xxxx-xxxx-16bd5d902219' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```
  </Tab>

  <Tab title="Python SDK">
    异步任务接口与具体任务类型无关，`fetch`、`list`、`cancel` 方法在任意异步任务类（如 `ImageSynthesis`、`VideoSynthesis`、`Transcription` 等）上均可调用，底层均请求 `/api/v1/tasks`，可查询当前账号下任意任务。

    ```python
    import os
    from dashscope import ImageSynthesis

    task_id = '73205176-xxxx-xxxx-xxxx-16bd5d902219'
    resp = ImageSynthesis.fetch(
        task_id,
        api_key=os.getenv('DASHSCOPE_API_KEY'),
    )
    print(resp.output)
    ```
  </Tab>

  <Tab title="dashscope CLI">
    ```bash
    export DASHSCOPE_API_KEY="your-api-key"
    # 查询指定任务状态（以 video-synthesis 为例）
    dashscope video-synthesis fetch <task_id>
    ```

    <Note>
      dashscope CLI 目前仅 `video-synthesis` 子命令支持 `fetch`/`list`/`cancel` 任务管理；`image-synthesis`、`transcription` 等子命令暂未提供。查询其他类型任务请用上方 Python SDK 或 curl。
    </Note>

    <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

    完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
  </Tab>
</Tabs>

<Note>
  若未配置阿里云百炼API Key到环境变量，请将`$DASHSCOPE_API_KEY`替换为实际API Key，例如：`--header "Authorization: Bearer sk-xxx"`。
</Note>

#### 响应示例 <span id="f5e9fdac53z9n" />

```json expandable
{
    "request_id": "45ac7f13-xxxx-xxxx-xxxx-e03c35068d83",
    "output": {
        "task_id": "73205176-xxxx-xxxx-xxxx-16bd5d902219",
        "task_status": "SUCCEEDED",
        "submit_time": "2023-12-20 21:36:31.896",
        "scheduled_time": "2023-12-20 21:36:39.009",
        "end_time": "2023-12-20 21:36:45.913",
        "results": [
            {
                "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx1.png"
            },
            {
                "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx2.png"
            },
            {
                "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx3.png"
            },
            {
                "code": "DataInspectionFailed",
                "message": "Output data may contain inappropriate content.",
            }
        ],
        "task_metrics": {
            "TOTAL": 4,
            "SUCCEEDED": 3,
            "FAILED": 1
        }
    },
    "usage": {
        "image_count": 3
    }
}
```

## 批量查询异步任务状态接口 <span id="7bd06355eazul" />

<strong>API描述</strong>：支持通过组合多种查询条件，批量获取多个异步任务的当前状态。该接口适用于一次性查看多个任务的执行进度。

<strong>流量限制</strong>：20 QPS，即每秒每个账号（含主账号及其子账号）最多发起 20 次请求。

<Tip>
  - 支持查询当前 API Key 所属阿里云主账号下的所有任务（包括该主账号下通过任意 API Key 提交的任务），但无法查询到其他主账号下的任务。
  - 已结束的任务在超时后将被系统自动清理，届时将无法查询到相关任务数据。
</Tip>

#### 请求接口 <span id="30dfa91dfcpz5" />

```http
GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks
```

#### 入参描述 <span id="a969f20c0ahxy" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "11.56%" }} /><col style={{ width: "13.93%" }} /><col style={{ width: "9.6%" }} /><col style={{ width: "8.05%" }} /><col style={{ width: "38.39%" }} /><col style={{ width: "18.47%" }} /></colgroup><thead><tr><th><p><strong>传参方式</strong></p></th><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Header</p></td><td><p>Authorization</p></td><td><p>String</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer sk-xxx</p></td><td><p>Bearer sk-xxx</p></td></tr><tr><td rowSpan={7}><p>Params</p></td><td><p>task\_id</p></td><td><p>String</p></td><td><p>否</p></td><td><p>需要查询任务的 task\_id</p> <p>若指定task\_id，接口仅返回该任务的状态信息；若需批量查询多个任务状态，请勿传入此参数</p></td><td><p>a8532587-xxxx-xxxx-xxxx-0c46b17950d1</p></td></tr><tr><td><p>start\_time</p></td><td><p>String</p></td><td><p>否</p></td><td><p>任务开始时间，格式为：<code>YYYYMMDDhhmmss</code></p> <ul><li><p>若未指定开始时间（start\_time为空），系统将查询指定结束时间（end\_time）前24小时的数据</p></li> <li><p>若开始和结束时间均未指定（均为空），系统默认查询最近24小时的数据。</p></li> <li><p>请确保您设定的开始和结束时间的差距不超过24小时</p></li></ul></td><td rowSpan={2}><p>20230420193058 代表 2023 年 4 月 20 日 19 点 30 分 58 秒</p></td></tr><tr><td><p>end\_time</p></td><td><p>String</p></td><td><p>否</p></td><td><p>任务结束时间，格式为：<code>YYYYMMDDhhmmss</code></p> <ul><li><p>若未指定结束时间（end\_time为空），系统将查询指定开始时间（start\_time）后24小时的数据。</p></li> <li><p>请确保您设定的开始和结束时间的差距不超过24小时</p></li></ul></td></tr><tr><td><p>model\_name</p></td><td><p>String</p></td><td><p>否</p></td><td><p>模型名称</p></td><td><p>wanx-v1</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>否</p></td><td><p>任务状态：</p> <ul><li><p>PENDING：任务排队中</p></li> <li><p>RUNNING：任务处理中</p></li> <li><p>SUCCEEDED：任务执行成功</p></li> <li><p>FAILED：任务执行失败</p></li> <li><p>CANCELED：任务取消成功</p></li> <li><p>UNKNOWN：任务不存在或状态未知</p></li></ul></td><td /></tr><tr><td><p>page\_no</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>当前页，默认查询第1页</p></td><td><p>-</p></td></tr><tr><td><p>page\_size</p></td><td><p>Integer</p></td><td><p>否</p></td><td><p>每页查询条数，默认查询10条</p></td><td><p>-</p></td></tr></tbody></table>

#### 出参描述 <span id="980dce330dnhp" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24.098235%" }} />

    <col style={{ width: "11.05142%" }} />

    <col style={{ width: "29.086723%" }} />

    <col style={{ width: "35.763622%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>字段</strong>
      </th>

      <th>
        <strong>类型</strong>
      </th>

      <th>
        <strong>描述</strong>
      </th>

      <th>
        <strong>示例值</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        request\_id
      </td>

      <td>
        String
      </td>

      <td>
        本次请求的系统唯一码
      </td>

      <td>
        7574ee8f-xxxx-xxxx-xxxx-11c33ab46e51
      </td>
    </tr>

    <tr>
      <td>
        data
      </td>

      <td>
        Array
      </td>

      <td>
        查询结果列表
      </td>

      <td rowSpan={13}>
        ```json
        "data": [
            {
                "api_key_id": "235",
                "caller_uid": "1808342417264262",
                "end_time": 1682527200093,
                "gmt_create": 1682514589152,
                "model_name": "paraformer-16k-1",
                "region": "cn-hangzhou",
                "request_id": "32b67b58-xxxx-xxxx-xxxx-230f0aee64d9",
                "start_time": 1682515862179,
                "status": "FAILED",
                "task_id": "cf52b16b-xxxx-xxxx-xxxx-17f9c211440c",
                "user_api_unique_key": "apikey:v1:audio:asr:transcription:paraformer-16k-1"
            }
        ]
        ```
      </td>
    </tr>

    <tr>
      <td>
        data\[].api\_key\_id
      </td>

      <td>
        String
      </td>

      <td>
        API Key的id
      </td>
    </tr>

    <tr>
      <td>
        data\[].caller\_parent\_id
      </td>

      <td>
        String
      </td>

      <td>
        阿里云主账号ID
      </td>
    </tr>

    <tr>
      <td>
        data\[].caller\_uid
      </td>

      <td>
        String
      </td>

      <td>
        阿里云账号ID
      </td>
    </tr>

    <tr>
      <td>
        data\[].gmt\_create
      </td>

      <td>
        Long
      </td>

      <td>
        任务创建时间，Date时间毫秒数
      </td>
    </tr>

    <tr>
      <td>
        data\[].start\_time
      </td>

      <td>
        Long
      </td>

      <td>
        任务开始时间，Date时间毫秒数
      </td>
    </tr>

    <tr>
      <td>
        data\[].end\_time
      </td>

      <td>
        Long
      </td>

      <td>
        任务结束时间，Date时间毫秒数
      </td>
    </tr>

    <tr>
      <td>
        data\[].region
      </td>

      <td>
        String
      </td>

      <td>
        地域，例如：cn-hangzhou
      </td>
    </tr>

    <tr>
      <td>
        data\[].request\_id
      </td>

      <td>
        String
      </td>

      <td>
        提交任务的请求id
      </td>
    </tr>

    <tr>
      <td>
        data\[].status
      </td>

      <td>
        String
      </td>

      <td>
        任务状态：

        - PENDING：任务排队中
        - RUNNING：任务处理中
        - SUCCEEDED：任务执行成功
        - FAILED：任务执行失败
        - CANCELED：任务取消成功
        - UNKNOWN：任务不存在或状态未知
      </td>
    </tr>

    <tr>
      <td>
        data\[].task\_id
      </td>

      <td>
        String
      </td>

      <td>
        任务id
      </td>
    </tr>

    <tr>
      <td>
        data\[].user\_api\_unique\_key
      </td>

      <td>
        String
      </td>

      <td>
        API 的唯一key（提交任务时，模型API的各要素唯一索引）
      </td>
    </tr>

    <tr>
      <td>
        data\[].model\_name
      </td>

      <td>
        String
      </td>

      <td>
        模型名称
      </td>
    </tr>

    <tr>
      <td>
        page\_no
      </td>

      <td>
        Integer
      </td>

      <td>
        当前页
      </td>

      <td>
        `"page_no": 1`
      </td>
    </tr>

    <tr>
      <td>
        page\_size
      </td>

      <td>
        Integer
      </td>

      <td>
        每页查询条数
      </td>

      <td>
        `"page_size": 10`
      </td>
    </tr>

    <tr>
      <td>
        total\_page
      </td>

      <td>
        Integer
      </td>

      <td>
        总页数
      </td>

      <td>
        `"total_page": 4`
      </td>
    </tr>

    <tr>
      <td>
        total
      </td>

      <td>
        Integer
      </td>

      <td>
        总条数
      </td>

      <td>
        `"total": 39`
      </td>
    </tr>

    <tr>
      <td>
        code
      </td>

      <td>
        String
      </td>

      <td>
        调用失败的时候返回的错误码
      </td>

      <td>
        `"code": "Throttling.RateQuota"`
      </td>
    </tr>

    <tr>
      <td>
        message
      </td>

      <td>
        String
      </td>

      <td>
        调用失败的时候返回的错误信息
      </td>

      <td>
        `"message": "Requests rate limit exceeded, please try again later."`
      </td>
    </tr>
  </tbody>
</table>

#### 请求示例 <span id="7619f5b773v6c" />

<Tabs>
  <Tab title="curl">
    ```bash height="62"
    curl -X GET 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks?start_time=xxx&end_time=xxx&status=xxx' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```
  </Tab>

  <Tab title="Python SDK">
    ```python
    import os
    from dashscope import ImageSynthesis

    resp = ImageSynthesis.list(
        start_time='20230420193000',
        end_time='20230420203000',
        status='RUNNING',
        page_no=1,
        page_size=10,
        api_key=os.getenv('DASHSCOPE_API_KEY'),
    )
    print(resp.output)
    ```

    `list` 还支持按 `model_name`、`api_key_id`、`region` 等条件筛选，参数与 HTTP API 的 Query 参数一一对应。

    <Note>
      `fetch`、`list`、`cancel` 在工作空间专属域名（`{WorkspaceId}.{region}.maas.aliyuncs.com`）和通用域名（`dashscope.aliyuncs.com`）均支持，跨地域（如 cn-beijing、cn-shanghai、cn-hangzhou）均可调用。调用时 API Key 须与对应业务空间匹配。
    </Note>
  </Tab>

  <Tab title="dashscope CLI">
    ```bash
    export DASHSCOPE_API_KEY="your-api-key"
    # 列出视频合成任务（支持 --start-time/--end-time/--status/--model-name 等筛选）
    dashscope video-synthesis list --status RUNNING -s 10
    ```

    <Note>
      dashscope CLI 目前仅 `video-synthesis` 子命令支持任务管理；查询其他类型任务（图像、语音转写等）请用上方 Python SDK 或 curl。
    </Note>

    <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

    完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
  </Tab>
</Tabs>

#### 响应示例 <span id="bc0d4b18dbf5s" />

```json expandable
{
    "total": 2,
    "data": [
        {
            "api_key_id": "15xxxx",
            "caller_parent_id": "xxxxxxxxx",
            "caller_uid": "xxxxxxxxx",
            "gmt_create": 1745568428109,
            "model_name": "wanx2.1-kf2v-plus",
            "region": "cn-beijing",
            "request_id": "1abfc3c8-dd25-98da-ad0b-xxxxxx",
            "start_time": 1745568428138,
            "status": "RUNNING",
            "task_id": "50e2ccea-abc4-43d7-a0dc-xxxxxx",
            "user_api_unique_key": "apikey:v1:aigc:image2video:video-synthesis:wanx2.1-kf2v-plus"
        },
        {
            "api_key_id": "15xxxx",
            "caller_parent_id": "xxxxxxxxx",
            "caller_uid": "xxxxxxxxx",
            "end_time": 1745568302481,
            "gmt_create": 1745568293253,
            "model_name": "wanx2.1-t2i-turbo",
            "region": "cn-beijing",
            "request_id": "f6bf34d9-bf87-9e8b-9ed4-xxxxxx",
            "start_time": 1745568293273,
            "status": "SUCCEEDED",
            "task_id": "3c777dbc-8cc6-4d80-aa90-xxxxxx",
            "user_api_unique_key": "apikey:v1:aigc:text2image:image-synthesis:wanx2.1-t2i-turbo"
        }
    ],
    "total_page": 1,
    "page_no": 1,
    "request_id": "f6756b7e-d0bb-9b74-813a-xxxxxx",
    "page_size": 10
}
```

## 取消异步任务接口 <span id="7e160e9b18vt2" />

<strong>API描述</strong>：用于取消异步任务，仅支持取消状态为 `PENDING` 的任务 （即排队中且尚未开始处理的任务），其他状态的任务无法取消。

<strong>流量限制</strong>：20 QPS，即每秒每个账号（含主账号及其子账号）最多发起 20 次请求。

<Tip>
  - 支持取消当前 API Key 所属阿里云主账号下的所有任务（包括该主账号下通过任意 API Key 提交的任务），但无法取消其他主账号提交的任务。
</Tip>

#### 请求接口 <span id="34b67e3965yn1" />

```http
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks{task_id}/cancel
```

#### 入参描述 <span id="2fb0d75706a0q" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.730622%" }} /><col style={{ width: "14.65848%" }} /><col style={{ width: "12.586339%" }} /><col style={{ width: "9.439754%" }} /><col style={{ width: "26.170376%" }} /><col style={{ width: "20.414428%" }} /></colgroup><thead><tr><th><p><strong>传参方式</strong></p></th><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Header</p></td><td><p>Authorization</p></td><td><p>String</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer sk-xxx</p></td><td><p>Bearer sk-xxx</p></td></tr><tr><td><p>Path</p></td><td><p>task\_id</p></td><td><p>String</p></td><td><p>是</p></td><td><p>待取消的任务 task\_id</p></td><td><p>a8532587-xxxx-xxxx-xxxx-0c46b17950d1</p></td></tr></tbody></table>

#### 出参描述 <span id="aace106edbd82" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.098235%" }} /><col style={{ width: "10.974674%" }} /><col style={{ width: "29.086723%" }} /><col style={{ width: "35.840368%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码</p></td><td><p>7574ee8f-xxxx-xxxx-xxxx-11c33ab46e51</p></td></tr><tr><td><p>code</p></td><td><p>String</p></td><td><p>调用失败的时候返回的错误码</p></td><td><p><code>"code": "Throttling.RateQuota"</code></p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>调用失败的时候返回的错误信息</p></td><td><p><code>"message": "Requests rate limit exceeded, please try again later."</code></p></td></tr></tbody></table>

#### 请求示例 <span id="36601f8482ilp" />

<Tabs>
  <Tab title="curl">
    ```bash
    curl -X POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks73205176-xxxx-xxxx-xxxx-16bd5d902219/cancel' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```
  </Tab>

  <Tab title="Python SDK">
    ```python
    import os
    from dashscope import ImageSynthesis

    task_id = '73205176-xxxx-xxxx-xxxx-16bd5d902219'
    resp = ImageSynthesis.cancel(
        task_id,
        api_key=os.getenv('DASHSCOPE_API_KEY'),
    )
    print(resp)
    ```

    仅状态为 `PENDING` 的任务可取消，其他状态会返回失败。
  </Tab>

  <Tab title="dashscope CLI">
    ```bash
    export DASHSCOPE_API_KEY="your-api-key"
    # 取消任务（以 video-synthesis 为例，仅 PENDING 状态可取消）
    dashscope video-synthesis cancel <task_id>
    ```

    <Note>
      dashscope CLI 目前仅 `video-synthesis` 子命令支持 `cancel`；取消其他类型任务请用上方 Python SDK 或 curl。
    </Note>

    <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

    完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
  </Tab>
</Tabs>

#### 响应示例 <span id="7eb12430118ay" />

```json
{
    "request_id": "45ac7f13-xxxx-xxxx-xxxx-e03c35068d83"
}
```

## 错误码 <span id="3b9b15a6a8qkl" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.029316%" }} /><col style={{ width: "20.521173%" }} /><col style={{ width: "30.184582%" }} /><col style={{ width: "36.264929%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>UnsupportedOperation</p></td><td><p>Failed to cancel the task, please confirm if the task is in PENDING status.</p></td><td><p>取消任务失败，请确认任务状态为<code>PENDING</code>。</p> <p>仅 PENDING 状态的任务可取消，其他状态任务无法取消。</p></td></tr></tbody></table>

## 常见问题 <span id="f7b3c4d5e6mz1" />

### 任务查询返回 DataInspectionFailed 错误码是什么含义？ <span id="b9d5e6f7a8ob3" />

`DataInspectionFailed` 表示输出数据可能包含不适当的内容，已被内容安全审核拦截。对于包含多个子任务的异步任务（如批量图像生成），部分子任务可能返回此错误码，而其他子任务仍正常完成。此时整体任务状态可能为 `SUCCEEDED`，但 `task_metrics` 中会显示失败的子任务数量。

### 如何判断异步任务是否执行完成？ <span id="e2a8b9c0d1re6" />

通过查询任务接口返回的 `task_status` 字段判断：

- `PENDING`：任务排队中，尚未开始处理。
- `RUNNING`：任务处理中。
- `SUCCEEDED`：任务执行成功。
- `FAILED`：任务执行失败，请查看 `output` 中的 `code` 和 `message` 了解失败原因。

### 频繁查询任务状态会影响任务执行吗？ <span id="e4a0b1c2d3dq8" />

不会。查询任务状态的请求与任务执行是独立的，查询次数不影响任务的执行进度和结果。但请注意，查询接口有 20 QPS 的限流限制，过于频繁的查询可能会触发限流，返回错误响应。建议根据任务类型合理设置查询间隔。
