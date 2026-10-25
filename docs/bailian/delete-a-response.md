> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 删除响应

> 根据 Response ID 删除一个已存储的模型响应。

<meta property="og:title" content="删除响应" />

<Tabs>
  <Tab title="华北2（北京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`DELETE https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses/{response_id}`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ## 路径参数 <span id="delete-req-h2" />

        <strong>response\_id</strong>`string`（必选）

        要删除的 Response ID，格式为 `resp_xxx`。仅当原创建请求中 `store=true` 时返回的 Response ID 可被删除。
      </td>

      <td>
        <CodeGroup dropdown>
          ```python Python
          import os
          from openai import OpenAI

          client = OpenAI(
              # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
          )

          response = client.responses.delete("resp_xxx")
          print(response)
          ```

          ```javascript Node.js
          import OpenAI from "openai";

          const openai = new OpenAI({
              // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx"
              apiKey: process.env.DASHSCOPE_API_KEY,
              baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
          });

          async function main() {
              const response = await openai.responses.del("resp_xxx");
              console.log(response);
          }

          main();
          ```

          ```bash curl
          curl -X DELETE https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses/resp_xxx \
            -H "Authorization: Bearer $DASHSCOPE_API_KEY"
          ```
        </CodeGroup>
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
        ## 返回结果 <span id="delete-resp-h2" />

        <strong>id</strong> `string`

        被删除的 Response ID。

        <strong>deleted</strong> `boolean`

        是否删除成功，成功为 `true`。
      </td>

      <td>
        ```json
        {
            "deleted": true,
            "id": "resp_4ca7fa5e-6ff5-9787-bc18-af6ca5eff36c"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 错误响应 <span id="delete-error-h2" />

当指定的 Response ID 不存在时，返回以下错误：

```json
{
    "error": {
        "message": "Response with id 'resp_xxx' not found.",
        "type": "InvalidParameter"
    }
}
```
