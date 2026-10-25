> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 批处理接口API详情

<meta property="og:title" content="批处理接口API详情" />

## 模型概览 <span id="d2fcb2eda3q2w" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.39%" }} /><col style={{ width: "9.22%" }} /><col style={{ width: "8.34%" }} /><col style={{ width: "14.03%" }} /><col style={{ width: "15.11%" }} /><col style={{ width: "36.91%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>数据类型</strong></p></th><th><p><strong>向量维度</strong></p></th><th><p><strong>单次请求文本最大行数</strong></p></th><th><p><strong>单行最大输入Token</strong></p></th><th><p><strong>支持语种</strong></p></th></tr></thead><tbody><tr><td><p>text-embedding-async-v2</p></td><td rowSpan={2}><p>float（32位）</p></td><td rowSpan={2}><p>1,536</p></td><td rowSpan={2}><p>100,000</p></td><td rowSpan={2}><p>2,048</p></td><td><p>中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语</p></td></tr><tr><td><p>text-embedding-async-v1</p></td><td><p>中文、英语、西班牙语、法语、葡萄牙语、印尼语</p></td></tr></tbody></table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.39%" }} /><col style={{ width: "15.21%" }} /><col style={{ width: "17.17%" }} /><col style={{ width: "14.62%" }} /><col style={{ width: "36.61%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th rowSpan={2}><p><strong>单价</strong></p> <p><strong>（每千输入Token）</strong></p></th><th rowSpan={2}><p><strong>免费额度（</strong><a href="/zh/model-studio/new-free-quota#977b13081ab56"><strong>注</strong></a><strong>）</strong></p></th><th colSpan={2}><p><strong>限流条件（超出任一数值时触发限流）</strong></p></th></tr><tr><th><p><strong>任务下发接口RPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>text-embedding-async-v2</p></td><td rowSpan={2}><p>0.0007元</p></td><td rowSpan={2}><p>各2000万Token</p> <p>有效期：百炼开通后90天内</p></td><td rowSpan={2}><p>1</p></td><td rowSpan={2}><p>当前用户在系统通用文本向量异步作业排队中和运行中的作业数量不超过50个。</p> <p>另外，为了避免大量突发的作业占据太多资源，限制并发的作业数为3个，即任意时间，单个用户最多只有3个通用文本向量的异步作业在并发运行，其他的作业只能在队列中等待。</p></td></tr><tr><td><p>text-embedding-async-v1</p></td></tr></tbody></table>

## 前提条件 <span id="0e41425c577u4" />

通用文本向量批处理接口API支持通过HTTP和DashScope SDK进行调用。

在调用前，先[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。

## HTTP调用 <span id="eb441c2e13too" />

HTTP调用仅支持异步模式，需通过两步完成：

1. <strong>创建任务</strong>：首先发送一个请求创建任务，该请求会返回任务ID。
2. <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询模型生成的结果。

<strong>通过HTTP调用时需配置的endpoint：</strong>

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/embeddings/text-embedding/text-embedding`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

### 创建任务 <span id="6628e753b9k79" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ##### 请求参数 <span id="8cc4ebc60c7io" />

        ###### 请求头（Headers） <span id="36fa8fa1af1e6" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ###### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong> `string`<strong>（必选）</strong>

        调用模型名称，可以选择`text-embedding-async-v1`或者`text-embedding-async-v2`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        用户需要批量向量化处理的输入。

        <Accordion title="属性" defaultOpen>
          url `string` <strong>（必选）</strong>

          用户需要批量向量化的文件HTTP url。（需要embedding的内容，一行一条）

          文本限制：

          - 单行字符串最长支持 2,048 Token
          - 单次请求文本最大100,000行
          - 文件大小不超过 200MB
        </Accordion>

        <strong>parameters</strong> `object` <strong>（可选）</strong>

        向量化处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>text\_type</strong> `string` <strong>（可选）</strong>

          参数值：

          - `document`(默认值)
          - `query`

          文本转换为向量后可以应用于检索、聚类、分类等下游任务，对检索这类非对称任务为了达到更好的检索效果建议区分查询文本（query）和底库文本（document）类型；聚类、分类等对称任务无需特别指定，采用系统默认值`document`即可。
        </Accordion>
      </td>

      <td>
        ```bash
        curl -X POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/embeddings/text-embedding/text-embedding' \
            -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
            -H 'Content-Type: application/json' \
            -H 'X-DashScope-Async: enable' \
            -d '{
            "model": "text-embedding-async-v2",
            "input": {
                "url": "https://modelscope.oss-cn-beijing.aliyuncs.com/resource/text_embedding_file.txt"
            },
            "parameters": {
                "text_type": "query"
            }
        }'
        ```
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ##### 响应参数 <span id="10da1d2a1e6yp" />

        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          任务ID。查询有效期24小时。

          <strong>task\_status</strong> `string`

          任务状态。

          <Accordion title="枚举值" defaultOpen>
            - PENDING：任务排队中
            - RUNNING：任务处理中
            - SUCCEEDED：任务执行成功
            - FAILED：任务执行失败
            - CANCELED：任务已取消
            - UNKNOWN：任务不存在或状态未知
          </Accordion>
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。

        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应">
            请保存 task\_id，用于查询任务状态与结果。

            ```json
            {
                "output": {
                    "task_status": "PENDING",
                    "task_id": "0385dc79-5ff8-4d82-bcb6-xxxxxx"
                },
                "request_id": "4909100c-7b5a-9f92-bfe5-xxxxxx"
            }
            ```
          </Tab>

          <Tab title="异常响应">
            创建任务失败，请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "code": "InvalidApiKey",
                "message": "No API-key provided.",
                "request_id": "7438d53d-6eb8-4596-8835-xxxxxx"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### 根据任务ID查询结果 <span id="420e6e77a17om" />

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ##### 请求参数 <span id="eb9c8d77b6xg8" />

        ###### 请求头（Headers） <span id="36a28b657f196" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ###### URL路径参数（Path parameters） <span id="0e29e015d8opn" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            请将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id。

            > 若使用新加坡地域的模型，需将base\_url替换为https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx，其中\{WorkspaceId}需替换为真实的业务空间ID。

            ```bash
            curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY"
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ##### 响应参数 <span id="f277d7425foyq" />

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          任务ID。查询有效期24小时。

          <strong>task\_status</strong> `string`

          任务状态。

          <Accordion title="枚举值" defaultOpen>
            - PENDING：任务排队中
            - RUNNING：任务处理中
            - SUCCEEDED：任务执行成功
            - FAILED：任务执行失败
            - CANCELED：任务已取消
            - UNKNOWN：任务不存在或状态未知
          </Accordion>

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>url</strong> `string`

          模型生成图片的URL地址。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>total\_tokens</strong>`integer`

          本次请求输入内容的Token数目，根据用户输入的文本文件被模型Tokenizer解析之后所对应的Token数目来进行计算。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            任务数据（如任务状态、处理结果URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json
            {
                "request_id":"<Your Request ID>",
                "output":{
                    "task_id":"<Your Task ID>",
                    "task_status":"SUCCEEDED",
                    "submit_time":"2025-02-20 14:37:13.328",
                    "scheduled_time":"2025-02-20 14:37:13.352",
                    "end_time":"2025-02-20 14:37:14.159",
                    "url":"<包含输出结果的url>"
                },
                "usage":{
                    "total_tokens":258
                }
            }
            ```
          </Tab>

          <Tab title="任务执行中">
            ```json
            {
                "request_id":"<Your Request ID>",
                "output":{
                    "task_id":"<Your Task ID>",
                    "task_status":"RUNNING"
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            如果因为某种原因导致任务执行失败，任务状态将被设置为FAILED，并通过code和message字段明确指示错误原因。

            ```json
            {
              "request_id": "<Your Request ID>"
              "output": {
                    "task_id": "<Your Task ID>",
                    "task_status": "FAILED",
                    "submit_time":"2025-02-20 15:57:19.039",
                    "scheduled_time":"2025-02-20 15:57:19.059",
                    "end_time":"2025-02-20 15:57:19.418",
                    "code": "xxx",
                     "message": "xxxxxx"
              }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## Dashscope <span id="69cac67a477k2" />

请先确认已安装最新版DashScope SDK，否则可能运行报错：[安装SDK](/zh/model-studio/install-sdk)。

DashScope SDK目前已支持Python和Java。

SDK与HTTP接口的参数名基本一致，参数结构根据不同语言的SDK封装而定。参数说明可参考[万相-文生图V2](/zh/model-studio/text-to-image-v2-api-reference)。

由于模型处理时间较长，底层服务采用异步方式提供。SDK在上层进行了封装，支持同步、异步两种调用方式。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求体 <span id="afcd41d2b57zf" />

        <strong>model</strong> `string`<strong>（必选）</strong>

        调用模型名称，可以选择`text-embedding-async-v1`或者`text-embedding-async-v2`。

        url `string` <strong>（必选）</strong>

        用户需要批量向量化的文件HTTP url。（需要向量化的内容，一行一条）

        文本限制：

        - 单行字符串最长支持2,048Token
        - 单次请求文本最大100,000行
        - 文件大小不超过 200MB

        <strong>text\_type</strong> `string`<strong>可选</strong>

        文本转换为向量后可以应用于检索、聚类、分类等下游任务，对检索这类非对称任务为了达到更好的检索效果建议区分查询文本（query）和底库文本（document）类型；聚类、分类等对称任务可以不用特殊指定，采用系统默认值`document`即可。
      </td>

      <td>
        <Tabs>
          <Tab title="同步调用">
            <CodeGroup dropdown>
              ```python Python
              from dashscope import BatchTextEmbedding
              import dashscope
              # 以下为华北2（北京）地域的配置，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的配置不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              result = BatchTextEmbedding.call(BatchTextEmbedding.Models.text_embedding_async_v1,
                                               url="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241016/nigwvr/text_embedding_file.txt",
                                               text_type="document")
              print(result)
              ```

              ```java Java expandable
              import com.alibaba.dashscope.embeddings.BatchTextEmbedding;
              import com.alibaba.dashscope.embeddings.BatchTextEmbeddingParam;
              import com.alibaba.dashscope.embeddings.BatchTextEmbeddingResult;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.task.AsyncTaskListParam;
              import com.alibaba.dashscope.task.AsyncTaskListResult;
              import com.alibaba.dashscope.utils.JsonUtils;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {
                      // 以下为华北2（北京）地域的配置，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的配置不同。
                      Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                  public static void basicCall() throws ApiException, NoApiKeyException {
                      BatchTextEmbeddingParam param = BatchTextEmbeddingParam.builder()
                              .model(BatchTextEmbedding.Models.TEXT_EMBEDDING_ASYNC_V1)
                              .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241016/nigwvr/text_embedding_file.txt")
                              .textType(BatchTextEmbeddingParam.TextType.DOCUMENT)
                              .build();
                      BatchTextEmbedding textEmbedding = new BatchTextEmbedding();
                      BatchTextEmbeddingResult result = textEmbedding.call(param);
                      System.out.println(result);
                  }

                  public static void main(String[] args) {
                      // 以下为华北2（北京）地域的配置，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的配置不同。
                      Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                      try {
                          basicCall();
                      } catch (ApiException | NoApiKeyException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```
            </CodeGroup>
          </Tab>

          <Tab title="异步调用">
            <CodeGroup dropdown>
              ```python Python expandable
              from dashscope import BatchTextEmbedding
              import dashscope
              from http import HTTPStatus
              # 以下为华北2（北京）地域的配置，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的配置不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              # 创建异步任务
              def create_async_task():
                  rsp = BatchTextEmbedding.async_call(model=BatchTextEmbedding.Models.text_embedding_async_v1,
                                                      url="https://modelscope.oss-cn-beijing.aliyuncs.com/resource/text_embedding_file.txt",
                                                      text_type="document")
                  if rsp.status_code == HTTPStatus.OK:
                      print(rsp.output)
                      print(rsp.usage)
                  else:
                      print('Failed, status_code: %s, code: %s, message: %s' %
                            (rsp.status_code, rsp.code, rsp.message))
                  return rsp

              # 获取异步任务信息
              def fetch_task_status(task):
                  status = BatchTextEmbedding.fetch(task)
                  print(status)
                  if status.status_code == HTTPStatus.OK:
                      print(status.output.task_status)
                  else:
                      print('Failed, status_code: %s, code: %s, message: %s' %
                            (status.status_code, status.code, status.message))

              # 等待异步任务结束，内部封装轮询逻辑，会一直等待任务结束
              def wait_task(task):
                  rsp = BatchTextEmbedding.wait(task)
                  print(rsp)
                  if rsp.status_code == HTTPStatus.OK:
                      print(rsp.output.task_status)
                  else:
                      print('Failed, status_code: %s, code: %s, message: %s' %
                            (rsp.status_code, rsp.code, rsp.message))

              # 取消异步任务，只有处于PENDING状态的任务才可以取消
              def cancel_task(task):
                  rsp = BatchTextEmbedding.cancel(task)
                  print(rsp)
                  if rsp.status_code == HTTPStatus.OK:
                      print(rsp.output.task_status)
                  else:
                      print('Failed, status_code: %s, code: %s, message: %s' %
                            (rsp.status_code, rsp.code, rsp.message))

              if __name__ == '__main__':
                  task_info = create_async_task()
                  fetch_task_status(task_info)
                  wait_task(task_info)
              ```

              ```java Java expandable
              import com.alibaba.dashscope.embeddings.BatchTextEmbedding;
              import com.alibaba.dashscope.embeddings.BatchTextEmbeddingParam;
              import com.alibaba.dashscope.embeddings.BatchTextEmbeddingResult;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.task.AsyncTaskListParam;
              import com.alibaba.dashscope.task.AsyncTaskListResult;
              import com.alibaba.dashscope.utils.JsonUtils;
              import com.alibaba.dashscope.utils.Constants;

              public class Main {
                      // 以下为华北2（北京）地域的配置，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的配置不同。
                      Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";

                  /**创建批处理任务*/
                  public static BatchTextEmbeddingResult createTask() throws ApiException, NoApiKeyException {
                      BatchTextEmbeddingParam param = BatchTextEmbeddingParam.builder()
                              .model(BatchTextEmbedding.Models.TEXT_EMBEDDING_ASYNC_V1)
                              .url("https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241016/nigwvr/text_embedding_file.txt")
                              .textType(BatchTextEmbeddingParam.TextType.DOCUMENT)
                              .build();
                      BatchTextEmbedding textEmbedding = new BatchTextEmbedding();
                      return textEmbedding.asyncCall(param);

                  }

                  /**获取任务状态*/
                  public static void fetchTaskStatus(BatchTextEmbeddingResult result)
                          throws ApiException, NoApiKeyException {
                      BatchTextEmbedding textEmbedding = new BatchTextEmbedding();
                      result = textEmbedding.fetch(result, null);
                      System.out.println(result);
                  }

                  /**等待任务结束，wait内部封装了轮询逻辑，会一直等待任务结束*/
                  public static void waitTask(BatchTextEmbeddingResult result)
                          throws ApiException, NoApiKeyException {
                      BatchTextEmbedding textEmbedding = new BatchTextEmbedding();
                      result = textEmbedding.wait(result, null);
                      System.out.println(result);
                  }

                  /**取消任务，只能取消处于pending状态的任务*/
                  public static void cancelTask(BatchTextEmbeddingResult result)
                          throws ApiException, NoApiKeyException {
                      BatchTextEmbedding textEmbedding = new BatchTextEmbedding();
                      result = textEmbedding.cancel(result, null);
                      System.out.println(result);
                  }

                  /**查询已经提交的任务*/
                  public static void list() throws ApiException, NoApiKeyException {
                      AsyncTaskListParam param = AsyncTaskListParam.builder().pageNo(1).pageSize(20).build();
                      BatchTextEmbedding textEmbedding = new BatchTextEmbedding();
                      AsyncTaskListResult result = textEmbedding.list(param);
                      System.out.println(JsonUtils.toJson(result));
                  }

                  public static void main(String[] args) {
                      try {
                          BatchTextEmbeddingResult task = createTask();
                          fetchTaskStatus(task);
                          waitTask(task);
                          // list();
                      } catch (ApiException | NoApiKeyException e) {
                          System.out.println(e.getMessage());
                      }
                      System.exit(0);
                  }
              }
              ```
            </CodeGroup>
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="e9c6861476m70" />

        <strong>status\_code</strong> `string`

        请求状态码，表示请求的执行结果（如 200 表示成功）。详情请参见[错误码](/zh/model-studio/error-code)。

        <strong>request\_id</strong> `string`

        请求唯一标识。可用于请求明细溯源和问题排查。

        <strong>code</strong> `string`

        请求失败时表示错误码，成功时返回参数中该参数为空。详情请参见[错误码](/zh/model-studio/error-code)。

        <strong>message</strong> `string`

        请求失败，表示失败详细信息，成功时返回参数中该参数为空。详情请参见[错误码](/zh/model-studio/error-code)。

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong>`string`

          任务唯一标识。可用于任务明细溯源和问题排查。

          <strong>task\_status</strong>`string`

          任务状态

          - SUCCEEDED: 任务执行成功
          - FAILED: 任务执行失败
          - CANCELED： 任务被取消
          - PENDING：任务排队中
          - SUSPENDED：任务挂起
          - RUNNING：任务处理中

          <strong>url</strong>`string`

          包含Embedding结果的url，任务执行失败时返回参数中该参数为空。

          <strong>submit\_time</strong>`string`

          任务提交时间

          > Java SDK的返回结果中不包含此字段

          <strong>scheduled\_time</strong>`string`

          任务调度时间

          > Java SDK的返回结果中不包含此字段

          <strong>end\_time</strong>`string`

          任务处理结束时间

          > Java SDK的返回结果中不包含此字段

          <strong>code</strong>`string`

          任务执行失败，表示失败原因，成功执行时响应中不会包含此参数。

          <strong>message</strong>`string`

          任务执行失败，表示失败详细信息，成功执行时响应中不会包含此参数。
        </Accordion>

        <strong>usage</strong>`object`

        <strong>属性</strong>

        <Accordion title="Details" defaultOpen>
          <strong>total\_tokens</strong> `integer`

          本次请求输入内容的Token数目，算法的计量是根据用户输入字符串被模型Tokenizer解析之后对应的Token数目来进行。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="同步调用">
            <Tabs>
              <Tab title="成功响应">
                ```json
                {
                    "status_code": 200,
                    "request_id": "<Your Request ID>",
                    "code": null,
                    "message": "",
                    "output": {
                        "task_id": "<Your Task ID>",
                        "task_status": "SUCCEEDED",
                        "url": "<包含输出结果的url>",
                        "submit_time": "2023-09-07 10:22:52.459",
                        "scheduled_time": "2023-09-07 10:22:52.481",
                        "end_time": "2023-09-07 10:22:53.419"
                    },
                    "usage": {
                        "total_tokens": 384
                    }
                }
                ```
              </Tab>

              <Tab title="异常响应">
                <strong>请求失败</strong>

                ```json
                {
                    "code":"InvalidApiKey",
                    "message":"Invalid API-key provided.",
                    "request_id":"<Your Request ID>"
                }
                ```

                <strong>任务执行失败</strong>

                ```json
                {
                    "status_code": 200,
                    "request_id": "<Your Request ID>",
                    "code": null,
                    "message": "",
                    "output": {
                        "task_id": "<Your Task ID>",
                        "task_status": "FAILED",
                        "url": "null",
                        "submit_time": "2023-09-07 10:22:52.459",
                        "scheduled_time": "2023-09-07 10:22:52.481",
                        "end_time": "2023-09-07 10:22:53.419"
                        "code": "InvalidFile.TypeNotTxt",
                        "message": "File type should be txt"
                    },
                    "usage": null
                }
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="异步调用">
            <Tabs>
              <Tab title="成功响应">
                ```json
                {
                    "status_code": 200,
                    "request_id": "<Your Request ID>",
                    "code": null,
                    "message": "",
                    "output": {
                        "task_id": "<Your Task ID>",
                        "task_status": "SUCCEEDED",
                        "url": "<包含输出结果的url>",
                        "submit_time": "2025-02-21 13:51:26.465",
                        "scheduled_time": "2025-02-21 13:51:26.507",
                        "end_time": "2025-02-21 13:51:27.273"
                    },
                    "usage": {
                        "total_tokens": 384
                    }
                }
                ```
              </Tab>

              <Tab title="异常响应">
                <strong>请求失败</strong>

                ```json
                {
                    "code":"InvalidApiKey",
                    "message":"Invalid API-key provided.",
                    "request_id":"<Your Request ID>"
                }
                ```

                <strong>任务执行失败</strong>

                ```json
                {
                    "status_code": 200,
                    "request_id": "<Your Request ID>",
                    "code": null,
                    "message": "",
                    "output": {
                        "task_id": "<Your Task ID>",
                        "task_status": "FAILED",
                        "url": "null",
                        "submit_time": "2023-09-07 10:22:52.459",
                        "scheduled_time": "2023-09-07 10:22:52.481",
                        "end_time": "2023-09-07 10:22:53.419"
                        "code": "InvalidFile.TypeNotTxt",
                        "message": "File type should be txt"
                    },
                    "usage": null
                }
                ```
              </Tab>
            </Tabs>
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 常见问题 <span id="b03b052639hkc" />

### 输入文件限制 <span id="49c9b59f66abi" />

- 输入文件需为 <strong>UTF-8 编码的文本文件</strong>，每行包含一个需要计算文字向量的字符串。系统会逐行处理每个输入，并在最终输出文件中返回对应的行号和生成的 embedding 结果。
- 单个文件大小不得超过 <strong>200MB</strong>。
- 单次请求的文本行数不得超过 <strong>100,000 行</strong>。
- 每行内容的长度不得超过 <strong>2,048 Token</strong>。
- 空行（即不包含任何字符的行）会被系统自动跳过，不会计算其文字向量。然而，为了便于结果对应，输出文件中仍会保留这些空行的行号。

### 输出文件说明 <span id="43b0a60e7fzap" />

- 当任务成功完成后，提交的输入数据将被转换为向量结果，并存储在输出文件中。为了节省存储空间并方便下载，输出文件会被压缩为 `.gz` 格式。下载至本地后可解压缩以获取对应的文本输出文件。
- 任务数据（如任务状态、处理结果URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存包含任务结果的输出文件。
- 经过模型向量化后输出的文件是一个 jsonl 格式文件，即每一行都是一个完整的 json 结构，包含对应输入文件特定行的向量化输出。

## 错误码 <span id="7cfb84ee806h3" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 基础限流 <span id="6d04415aa6jdb" />

为了保证用户调用模型的公平性，通用文本向量对用户设置了基础限流。如果超出调用限制，用户的API请求将因为限流控制而失败，用户需要等待一段时间待满足限流条件后方能再次调用。各模型详细限流条件请参见[限流](/zh/model-studio/rate-limit)。
