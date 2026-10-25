> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 异步调用API参考

> 本文介绍如何通过 OpenAI 兼容模式的 Responses API 异步调用 阿里云百炼应用（ 智能体 、 工作流 ）。对于 耗时较长 的任务，只需在请求中设置 background 为 true，API 便会立即返回一个任务 ID，用于后续的查询与管理。这种“先提交、后查询”的方式，可有效避免请求超时或长时间等待。

<meta property="og:title" content="异步调用API参考" />

<strong>相关参考</strong>

- <strong>同步调用</strong>：对于需要即时获取结果的实时交互场景，请参阅[同步调用 API 参考](/zh/model-studio/synchronous-call-api-reference)。
- <strong>DashScope API</strong>：如需获取更多功能，请参阅[DashScope API](/zh/model-studio/application-dashscope-api-reference)。

<Tip>
  本文档仅适用于华北2（北京）地域。
</Tip>

<Note>
  异步任务暂不支持流式输出（stream=true）。
</Note>

## 前提条件 <span id="bcad59b409rk8" />

- 已[获取 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。如果通过OpenAI SDK进行调用，还需要[安装SDK](/zh/model-studio/install-sdk)。
- 已创建[阿里云百炼应用](/zh/model-studio/application-introduction)，并已获取应用ID：在[应用管理](https://bailian.console.aliyun.com/?tab=app#/app-center)页面的应用卡片上复制其ID。

## 快速开始 <span id="7226dca8fe4ld" />

本节提供完整的 Python 和 Java 示例，演示如何发起一个异步任务，然后通过轮询方式持续检查任务状态，直到任务完成并获取最终结果。

这个示例覆盖了异步调用的核心流程：

1. <strong>创建任务</strong>：调用 `create` 方法并设置 `background=True`，获取任务 ID。
2. <strong>轮询状态</strong>：在一个循环中，定期调用 `retrieve` 方法查询任务状态。
3. <strong>处理结果</strong>：当任务状态变为 `completed`、`failed` 或 `cancelled` 时，退出循环并展示最终结果。

<Accordion title="代码示例" defaultOpen>
  <CodeGroup dropdown>
    ```python Python expandable
    import asyncio
    import os
    from openai import AsyncOpenAI
    import time
    # 1. 初始化客户端
    ## 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
    api_key = os.getenv("DASHSCOPE_API_KEY")
    app_id = 'APP_ID'  # 请替换为实际的应用ID
    base_url = f'https://dashscope.aliyuncs.com/api/v2/apps/agent/{app_id}/compatible-mode/v1/'
    client = AsyncOpenAI(api_key=api_key, base_url=base_url)
    async def main():
        start_time = time.time()
        # 2. 发起异步任务
        print("Step 1: 正在创建异步任务...")
        create_response = await client.responses.create(
            input="请为我规划一个为期三天的北京旅游行程，要求包含故宫、长城。",
            background=True
        )
        task_id = create_response.id
        print(f"  - 任务创建成功！ Task ID: {task_id}")
        print(f"  - 初始状态: {create_response.status}")
        # 3. 轮询任务状态直到任务完成
        print("\nStep 2: 开始轮询任务状态 (每 2 秒一次)...")
        while True:
            retrieve_response = await client.responses.retrieve(task_id)
            status = retrieve_response.status
            print(f"  - 当前状态: {status}")
            # 检查任务是否已进入终态
            if status in ['completed', 'failed', 'cancelled']:
                print("\nStep 3: 任务已完成或终止。")
                final_response = retrieve_response
                break
            # 等待 2 秒后再次查询
            await asyncio.sleep(2)
        # 4. 处理并打印最终结果
        if final_response.status == 'completed':
            print("  - 状态: Success")
            # 提取并打印可读的文本结果
            if final_response.output and final_response.output[0].content:
                result_text = final_response.output[0].content[0].text
                print("\n--- 任务输出 ---")
                print(result_text)
                print("-----------------")
            else:
                print("任务已完成，但未返回有效输出。")
        else: # 'failed' or 'cancelled'
            print(f"  - 状态: {final_response.status.upper()}")
            print("\n--- 最终响应详情 ---")
            # 打印完整的JSON以供调试
            print(final_response.model_dump_json(indent=2))
            print("-----------------------")
        end_time = time.time()
        print(f"\n总耗时: {end_time - start_time:.2f} 秒")
    if __name__ == "__main__":
        asyncio.run(main())
    ```

    ```java Java expandable
    import com.openai.client.OpenAIClient;
    import com.openai.client.okhttp.OpenAIOkHttpClient;
    import com.openai.models.responses.*;
    import com.fasterxml.jackson.databind.ObjectMapper;
    import com.fasterxml.jackson.databind.SerializationFeature;
    public class QuickStart {
        public static void main(String[] args) throws Exception {
            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
            String apiKey = System.getenv("DASHSCOPE_API_KEY");
            String appId = "APP_ID"; // 替换为实际的应用 ID
            String baseUrl = "https://dashscope.aliyuncs.com/api/v2/apps/agent/" + appId + "/compatible-mode/v1/";
            OpenAIClient client = OpenAIOkHttpClient.builder()
                    .apiKey(apiKey)
                    .baseUrl(baseUrl)
                    .build();
            ObjectMapper mapper = new ObjectMapper();
            mapper.enable(SerializationFeature.INDENT_OUTPUT);
            long startTime = System.currentTimeMillis();
            // 1. 创建异步任务
            System.out.println("Step 1: 正在创建异步任务...");
            Response createResponse = client.responses().create(
                    ResponseCreateParams.builder()
                            .input("请为我规划一个为期三天的北京旅游行程，要求包含故宫、长城。")
                            .background(true)
                            .build()
            );
            String taskId = createResponse.id();
            System.out.println("  - 任务创建成功！Task ID: " + taskId);
            System.out.println("  - 初始状态: " + createResponse.status().orElse(null));
            // 2. 轮询任务状态直到任务完成
            System.out.println("\nStep 2: 开始轮询任务状态 (每 2 秒一次)...");
            Response finalResponse = null;
            while (true) {
                Response retrieveResponse = client.responses().retrieve(taskId);
                ResponseStatus status = retrieveResponse.status().orElse(null);
                System.out.println("  - 当前状态: " + status);
                // 检查任务是否已进入终态
                if (ResponseStatus.COMPLETED.equals(status)
                        || ResponseStatus.FAILED.equals(status)
                        || ResponseStatus.CANCELLED.equals(status)) {
                    System.out.println("\nStep 3: 任务已完成或终止。");
                    finalResponse = retrieveResponse;
                    break;
                }
                // 等待 2 秒后再次查询
                Thread.sleep(2000);
            }
            // 3. 处理并打印最终结果
            ResponseStatus finalStatus = finalResponse.status().orElse(null);
            if (ResponseStatus.COMPLETED.equals(finalStatus)) {
                System.out.println("  - 状态: Success");
                if (!finalResponse.output().isEmpty()) {
                    System.out.println("\n--- 任务输出 ---");
                    System.out.println(mapper.writeValueAsString(finalResponse.output()));
                    System.out.println("-----------------");
                }
            } else {
                System.out.println("  - 状态: " + finalStatus);
                System.out.println("\n--- 最终响应详情 ---");
                System.out.println(mapper.writeValueAsString(finalResponse));
                System.out.println("-----------------------");
            }
            long endTime = System.currentTimeMillis();
            System.out.println("\n总耗时: " + (endTime - startTime) / 1000.0 + " 秒");
        }
    }
    ```
  </CodeGroup>
</Accordion>

## 具体流程 <span id="c61b3d137fqwd" />

以下章节详细介绍了创建、查询、取消和删除异步任务的 API 操作。

### 创建任务 <span id="026f7c4749osf" />

将`background`参数设置为`true`来开启异步模式，创建异步任务，立即获取任务 ID。

<strong>Endpoint:</strong>`POST https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses`

> 请将 `{APP_ID}` 替换为实际的应用 ID。

#### 请求示例 <span id="18178f8a09ch9" />

<Tabs>
  <Tab title="单轮对话">
    <CodeGroup dropdown>
      ```python Python
      from openai import AsyncOpenAI
      import asyncio
      import os
      # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
      api_key=os.getenv("DASHSCOPE_API_KEY")
      app_id='APP_ID' # 替换为实际的应用 ID
      base_url = f'https://dashscope.aliyuncs.com/api/v2/apps/agent/{app_id}/compatible-mode/v1/'
      # 初始化异步客户端
      client = AsyncOpenAI(
        api_key=api_key,
        base_url=base_url
      )
      async def main():
        # 异步调用
        response = await client.responses.create(
          input="你是谁？",
          background=True
        )
        # 打印完整的响应JSON对象
        print(response.model_dump_json(indent=2))
      # 运行异步主函数
      if __name__ == "__main__":
        asyncio.run(main())
      ```

      ```java Java expandable
      import com.openai.client.OpenAIClient;
      import com.openai.client.okhttp.OpenAIOkHttpClient;
      import com.openai.models.responses.*;
      import com.fasterxml.jackson.databind.ObjectMapper;
      import com.fasterxml.jackson.databind.SerializationFeature;
      public class CreateTask {
          public static void main(String[] args) throws Exception {
              // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              String appId = "APP_ID"; // 替换为实际的应用 ID
              String baseUrl = "https://dashscope.aliyuncs.com/api/v2/apps/agent/" + appId + "/compatible-mode/v1/";
              OpenAIClient client = OpenAIOkHttpClient.builder()
                      .apiKey(apiKey)
                      .baseUrl(baseUrl)
                      .build();
              // 异步调用
              Response response = client.responses().create(
                      ResponseCreateParams.builder()
                              .input("你是谁？")
                              .background(true)
                              .build()
              );
              // 打印完整的响应JSON对象
              ObjectMapper mapper = new ObjectMapper();
              mapper.enable(SerializationFeature.INDENT_OUTPUT);
              System.out.println(mapper.writeValueAsString(response));
          }
      }
      ```

      ```bash curl
      # 若没有配置环境变量，请将 {DASHSCOPE_API_KEY} 替换为实际的阿里云百炼API Key，但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
      # 请将 APP_ID 替换为实际的应用ID
      # 实际使用时请删除注释内容
      curl --location "https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses" \
      --header 'Content-Type: application/json' \
      --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
      --data '{
          "input": "你是谁？",
          "background": true
      }'
      ```
    </CodeGroup>
  </Tab>

  <Tab title="参数传递">
    <strong>智能体应用配置</strong>

    <strong>插件参数传递</strong>：在应用内选择指定插件工具，<strong>发布</strong>应用。在<strong>插件</strong>区域，已添加名为<strong>寝室公约查询工具</strong>的插件，该插件用于根据输入的数字索引查询特定条目的寝室公约内容。

    <strong>工作流应用配置</strong>

    <table className="table-wide" autofit="false" tablewidth="811" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
      <colgroup>
        <col style={{ width: "49.691739%" }} />

        <col style={{ width: "50.308261%" }} />
      </colgroup>

      <tbody>
        <tr>
          <td>
            <strong>自定义参数传递</strong>

            > 此处以查询城市美食作为示例。

            1. 在应用内的<strong>开始</strong>创建自定义参数；
            2. 在<strong>大模型节点</strong>的<strong>提示词</strong>处引用自定义参数；
            3. 测试无误后，<strong>发布</strong>应用。

            在<strong>模型配置</strong>中选择<strong>通义千问-Plus-Latest</strong>模型，在<strong>提示词</strong>中输入`请介绍 /city 这个城市的五个特色美食，仅返回名称，用逗号分隔。`，其中`/city`通过输入"/"插入，引用<strong>开始</strong>节点的自定义参数。
          </td>

          <td>
            <strong>插件参数传递</strong>

            > 此处以寝室公约查询工具作为示例。

            1. 如需传递插件，需添加<strong>插件节点</strong>，选择指定插件工具；
            2. 在应用内的<strong>开始</strong>创建自定义参数，并将其传入插件节点的输入参数中；
            3. 在<strong>大模型节点</strong>的<strong>提示词</strong>处引用自定义参数和插件输出参数；
            4. 测试无误后，<strong>发布</strong>应用。

            在<strong>模型配置</strong>中选择<strong>通义千问-Plus-Latest</strong>模型，在<strong>提示词</strong>和<strong>用户提示词</strong>中输入"查询寝室公约内容第"并插入变量`开始/article_index`，再接"条内容"并插入变量`插件1/article`。
          </td>
        </tr>
      </tbody>
    </table>

    #### API 调用 <span id="439e0bc444odl" />

    - <strong>自定义参数：</strong>调用时通过`biz_params`传递，参数名和类型要与应用内的参数配置保持一致。

      <CodeGroup dropdown>
        ```python Python expandable
        from openai import AsyncOpenAI
        import asyncio
        import os
        # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
        api_key=os.getenv("DASHSCOPE_API_KEY")
        app_id='APP_ID' # 替换为实际的应用 ID
        base_url = f'https://dashscope.aliyuncs.com/api/v2/apps/agent/{app_id}/compatible-mode/v1/'
        # 初始化异步客户端
        client = AsyncOpenAI(
          api_key=api_key,
          base_url=base_url
        )
        async def main():
          # 异步调用
          response = await client.responses.create(
            input="你好",
            extra_body={
                "biz_params": {"city": "北京"}
            },
            background=True
          )
          # 打印完整的响应JSON对象
          print(response.model_dump_json(indent=2))
        # 运行异步主函数
        if __name__ == "__main__":
          asyncio.run(main())
        ```

        ```java Java expandable
        import com.openai.client.OpenAIClient;
        import com.openai.client.okhttp.OpenAIOkHttpClient;
        import com.openai.models.responses.*;
        import com.fasterxml.jackson.databind.ObjectMapper;
        import com.fasterxml.jackson.databind.SerializationFeature;
        import java.util.HashMap;
        import java.util.Map;
        public class CreateTaskWithCustomParams {
            public static void main(String[] args) throws Exception {
                // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                String apiKey = System.getenv("DASHSCOPE_API_KEY");
                String appId = "APP_ID"; // 替换为实际的应用 ID
                String baseUrl = "https://dashscope.aliyuncs.com/api/v2/apps/agent/" + appId + "/compatible-mode/v1/";
                OpenAIClient client = OpenAIOkHttpClient.builder()
                        .apiKey(apiKey)
                        .baseUrl(baseUrl)
                        .build();
                // 异步调用，通过 putAdditionalBodyProperty 传递自定义参数
                Map<String, Object> bizParams = new HashMap<>();
                bizParams.put("city", "北京");
                Response response = client.responses().create(
                        ResponseCreateParams.builder()
                                .input("你好")
                                .background(true)
                                .putAdditionalBodyProperty("biz_params",
                                        com.openai.core.JsonValue.from(bizParams))
                                .build()
                );
                // 打印完整的响应JSON对象
                ObjectMapper mapper = new ObjectMapper();
                mapper.enable(SerializationFeature.INDENT_OUTPUT);
                System.out.println(mapper.writeValueAsString(response));
            }
        }
        ```

        ```bash curl
        #!/bin/bash
        # ==================== 配置区域 ====================
        # 若没有配置环境变量，请将 ${DASHSCOPE_API_KEY} 替换为实际的阿里云百炼 API Key
        # 但不建议在生产环境中直接将 API Key 硬编码到代码中，以减少 API Key 泄露风险
        # {APP_ID} 替换为实际的应用 ID
        # ==================== 调用 API ====================
        curl --location "https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses" \
        --header 'Content-Type: application/json' \
        --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
        --data '{
            "input": "你是谁？",
            "background": true,
            "biz_params": {
                "city": "杭州"
            }
        }'
        ```
      </CodeGroup>
    - <strong>插件参数：</strong>调用时通过`biz_params`传递，`user_defined_params`传递工具参数，`user_defined_tokens`传递鉴权参数，参数名和类型要与工具参数配置保持一致。

      <CodeGroup dropdown>
        ```python Python expandable
        """
        阿里云百炼应用调用示例
        异步调用
        """
        from openai import AsyncOpenAI
        import asyncio
        import os
        # ==================== 配置区域 ====================
        # API Key 配置
        # 若没有配置环境变量，可将下行替换为：api_key="sk-xxx"
        # 但不建议在生产环境中直接将 API Key 硬编码到代码中，以减少泄露风险
        api_key = os.getenv("DASHSCOPE_API_KEY")
        # 应用 ID（在百炼控制台 -> 应用 -> 发布渠道 -> API 中获取）
        app_id = 'YOUR_APP_ID'  # 替换为实际的应用 ID
        # Base URL
        base_url = f'https://dashscope.aliyuncs.com/api/v2/apps/agent/{app_id}/compatible-mode/v1/'
        # ==================== 客户端初始化 ====================
        client = AsyncOpenAI(
            api_key=api_key,
            base_url=base_url
        )
        # ==================== 业务参数配置（可选）====================
        # biz_params 用于传递自定义参数给应用中的工具/插件
        # YOUR_TOOL_ID 需要替换为实际的工具 ID（在百炼控制台的插件工具详情页获取）
        biz_params = {
            # 自定义参数：传递给指定工具的参数
            "user_defined_params": {
                "YOUR_TOOL_ID": {  # 替换为实际的工具 ID
                    "article_index": 9   # 自定义参数，根据工具需求设置
                }
            },
            # 自定义 Token：为指定工具提供认证信息，YOUR_TOKEN替换为示例的认证 Token
            "user_defined_tokens": {
                "YOUR_TOOL_ID": {  # 替换为实际的工具 ID
                    "user_token": "YOUR_TOKEN" # 工具所需的认证 Token
                }
            }
        }
        # ==================== 主函数 ====================
        async def main():
            """异步调用 Agent 应用"""
            # 发起异步请求
            # - input: 用户输入的问题或指令
            # - extra_body: 额外参数，包括 biz_params（业务参数）
            # - background: True 表示后台异步执行
            response = await client.responses.create(
                input="你好",
                extra_body={
                    "biz_params": biz_params
                },
                background=True
            )
            # 打印完整的响应 JSON 对象
            print(response.model_dump_json(indent=2))
        # ==================== 程序入口 ====================
        if __name__ == "__main__":
            asyncio.run(main())
        ```

        ```java Java expandable
        import com.openai.client.OpenAIClient;
        import com.openai.client.okhttp.OpenAIOkHttpClient;
        import com.openai.models.responses.*;
        import com.fasterxml.jackson.databind.ObjectMapper;
        import com.fasterxml.jackson.databind.SerializationFeature;
        import java.util.HashMap;
        import java.util.Map;
        public class CreateTaskWithPluginParams {
            public static void main(String[] args) throws Exception {
                // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                String apiKey = System.getenv("DASHSCOPE_API_KEY");
                String appId = "YOUR_APP_ID"; // 替换为实际的应用 ID
                String baseUrl = "https://dashscope.aliyuncs.com/api/v2/apps/agent/" + appId + "/compatible-mode/v1/";
                OpenAIClient client = OpenAIOkHttpClient.builder()
                        .apiKey(apiKey)
                        .baseUrl(baseUrl)
                        .build();
                // 构建 biz_params，包含 user_defined_params 和 user_defined_tokens
                // 自定义参数：传递给指定工具的参数
                Map<String, Object> toolParams = new HashMap<>();
                toolParams.put("article_index", 9);  // 自定义参数，根据工具需求设置
                Map<String, Object> userDefinedParams = new HashMap<>();
                userDefinedParams.put("YOUR_TOOL_ID", toolParams);  // 替换为实际的工具 ID
                // 自定义 Token：为指定工具提供认证信息
                Map<String, Object> toolTokens = new HashMap<>();
                toolTokens.put("user_token", "YOUR_TOKEN");  // 工具所需的认证 Token
                Map<String, Object> userDefinedTokens = new HashMap<>();
                userDefinedTokens.put("YOUR_TOOL_ID", toolTokens);  // 替换为实际的工具 ID
                Map<String, Object> bizParams = new HashMap<>();
                bizParams.put("user_defined_params", userDefinedParams);
                bizParams.put("user_defined_tokens", userDefinedTokens);
                // 异步调用，通过 putAdditionalBodyProperty 传递插件参数
                Response response = client.responses().create(
                        ResponseCreateParams.builder()
                                .input("你好")
                                .background(true)
                                .putAdditionalBodyProperty("biz_params",
                                        com.openai.core.JsonValue.from(bizParams))
                                .build()
                );
                // 打印完整的响应JSON对象
                ObjectMapper mapper = new ObjectMapper();
                mapper.enable(SerializationFeature.INDENT_OUTPUT);
                System.out.println(mapper.writeValueAsString(response));
            }
        }
        ```

        ```bash curl expandable
        #!/bin/bash
        # ==================== 配置区域 ====================
        # 若没有配置环境变量，请将 ${DASHSCOPE_API_KEY} 替换为实际的阿里云百炼 API Key
        # 但不建议在生产环境中直接将 API Key 硬编码到代码中，以减少 API Key 泄露风险
        APP_ID="YOUR_APP_ID"  # 替换为实际的应用 ID
        # ==================== 调用 API ====================
        curl --location "https://dashscope.aliyuncs.com/api/v2/apps/agent/${APP_ID}/compatible-mode/v1/responses" \
        --header 'Content-Type: application/json' \
        --header "Authorization: Bearer ${DASHSCOPE_API_KEY}" \
        --data '{
            "input": "你是谁？",
            "background": true,
            "biz_params": {
                "user_defined_params": {
                    "YOUR_PLUGIN_ID": {
                        "article_index": 9
                    }
                },
                "user_defined_tokens": {
                    "YOUR_PLUGIN_ID": {
                        "user_token": "YOUR_TOKEN"
                    }
                }
            }
        }'
        # ==================== 参数说明 ====================
        # biz_params 用于传递自定义参数给 Agent 中的工具/插件：
        #
        # user_defined_params - 自定义参数
        #   YOUR_PLUGIN_ID: 替换为实际的工具 ID
        #   article_index: 自定义参数名和值，根据工具需求设置
        #
        # user_defined_tokens - 自定义 Token（鉴权信息）
        #   YOUR_PLUGIN_ID: 替换为实际的工具 ID
        #   user_token: 工具所需的认证 Token
        ```
      </CodeGroup>
  </Tab>
</Tabs>

#### 请求字段说明 <span id="d42aadc9d9w8i" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.094926%" }} /><col style={{ width: "19.639935%" }} /><col style={{ width: "12.274959%" }} /><col style={{ width: "45.99018%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必填</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p><code>input</code></p></td><td><p>string/array</p></td><td><p>是</p></td><td><p>请求的核心输入内容。可以是单个字符串，或是一个包含多轮对话历史的消息数组。</p></td></tr><tr><td><p><code>background</code></p></td><td><p>boolean</p></td><td><p>是</p></td><td><p>是否以异步方式执行任务。</p> <ul><li><p><code>false</code>（默认值）：启用同步模式。API将保持连接直到任务完成。</p></li> <li><p><code>true</code>：启用异步模式。API将立即返回一个任务ID，可通过查询接口来获取任务状态和结果。</p></li></ul></td></tr><tr><td><p>extra\_body</p></td><td><p><em>object</em></p></td><td><p>否</p></td><td><p>额外参数字段。</p></td></tr><tr><td><p>extra\_body.biz\_params</p></td><td><p><em>object</em></p></td><td><p>否</p></td><td><p>应用通过自定义变量、节点或插件传递参数时，使用该字段进行传递。</p></td></tr><tr><td><p>user\_defined\_params</p></td><td><p><em>object</em></p></td><td><p>否</p></td><td><p>表示自定义插件参数信息。</p> <p>一个应用内添加的插件不可重复，且上限 10 个。</p></td></tr><tr><td><p>user\_defined\_tokens</p></td><td><p><em>object</em></p></td><td><p>否</p></td><td><p>表示自定义插件的用户级鉴权信息。</p> <p>一个应用内添加的插件不可重复，且上限 10 个。</p></td></tr><tr><td><p>user\_defined\_tokens.user\_token</p></td><td><p><em>string</em></p></td><td><p>否</p></td><td><p>传递该插件需要的用户鉴权信息。</p></td></tr></tbody></table>

#### 响应示例 <span id="d36eb6ccd9leh" />

```json expandable
{
  "id": "bcb9728b-a7f8-480c-ace0-8d61ff776857",
  "created_at": 1761875532,
  "error": null,
  "incomplete_details": null,
  "instructions": null,
  "metadata": null,
  "model": "",
  "object": "response",
  "output": [],
  "parallel_tool_calls": false,
  "temperature": null,
  "tool_choice": "auto",
  "tools": [],
  "top_p": null,
  "background": null,
  "conversation": null,
  "max_output_tokens": null,
  "max_tool_calls": null,
  "previous_response_id": null,
  "prompt": null,
  "prompt_cache_key": null,
  "reasoning": null,
  "safety_identifier": null,
  "service_tier": null,
  "status": "queued",
  "text": null,
  "top_logprobs": null,
  "truncation": null,
  "usage": null,
  "user": null
}
```

#### 响应字段说明 <span id="625318021422x" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.951557%" }} /><col style={{ width: "17.647059%" }} /><col style={{ width: "56.401384%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p><code>id</code></p></td><td><p>string</p></td><td><p>异步任务的唯一标识符，用于后续查询、取消或删除操作。</p></td></tr><tr><td><p><code>status</code></p></td><td><p>string</p></td><td><p>任务的初始状态，通常为 <code>queued</code>。</p></td></tr><tr><td><p><code>created\_at</code></p></td><td><p>integer</p></td><td><p>任务创建时间的Unix时间戳（秒）。</p></td></tr><tr><td><p><code>object</code></p></td><td><p>string</p></td><td><p>对象类型，固定为<code>response</code>。</p></td></tr></tbody></table>

### 查询任务 <span id="d32a504fb9m3c" />

获取指定任务的当前状态和执行结果。

<strong>Endpoint:</strong>`GET https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses/{RESPONSE_ID}`

> 请将 `{APP_ID}` 替换为实际的应用 ID，将 `{RESPONSE_ID}` 替换为创建任务时返回的任务ID。

#### 请求示例 <span id="9be2bd68364i3" />

<CodeGroup dropdown>
  ```python Python
  import asyncio
  import os
  from openai import AsyncOpenAI
  # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
  api_key=os.getenv("DASHSCOPE_API_KEY")
  app_id='APP_ID' # 1、替换为实际的应用 ID
  base_url = f'https://dashscope.aliyuncs.com/api/v2/apps/agent/{app_id}/compatible-mode/v1/'
  # 初始化客户端
  client = AsyncOpenAI(api_key=api_key, base_url=base_url)
  async def main():
    # 查询任务状态
    response = await client.responses.retrieve("6e4ba287-0e93-4d67-9955-ef1017ed3384") # 2、替换为实际的任务 ID
    print(response.model_dump_json(indent=2))
  if __name__ == "__main__":
    asyncio.run(main())
  ```

  ```java Java
  import com.openai.client.OpenAIClient;
  import com.openai.client.okhttp.OpenAIOkHttpClient;
  import com.openai.models.responses.*;
  import com.fasterxml.jackson.databind.ObjectMapper;
  import com.fasterxml.jackson.databind.SerializationFeature;
  public class RetrieveTask {
      public static void main(String[] args) throws Exception {
          // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
          String apiKey = System.getenv("DASHSCOPE_API_KEY");
          String appId = "APP_ID"; // 1、替换为实际的应用 ID
          String baseUrl = "https://dashscope.aliyuncs.com/api/v2/apps/agent/" + appId + "/compatible-mode/v1/";
          OpenAIClient client = OpenAIOkHttpClient.builder()
                  .apiKey(apiKey)
                  .baseUrl(baseUrl)
                  .build();
          // 查询任务状态
          String responseId = "6e4ba287-0e93-4d67-9955-ef1017ed3384"; // 2、替换为实际的任务 ID
          Response response = client.responses().retrieve(responseId);
          ObjectMapper mapper = new ObjectMapper();
          mapper.enable(SerializationFeature.INDENT_OUTPUT);
          System.out.println(mapper.writeValueAsString(response));
      }
  }
  ```

  ```bash curl
  # 若没有配置环境变量，请将 {DASHSCOPE_API_KEY} 替换为实际的阿里云百炼API Key，但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
  # 1、请将 APP_ID 替换为实际的应用ID
  # 2、请将 RESPONSE_ID 替换为创建异步任务返回的ID
  # 实际使用时请删除注释内容
  curl "https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses/{RESPONSE_ID}" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer ${DASHSCOPE_API_KEY}"
  ```
</CodeGroup>

#### 响应示例 <span id="988f9d9b3emm4" />

```json expandable
{
  "id": "bcb9728b-a7f8-480c-ace0-8d61ff776857",
  "created_at": 1761875537,
  "error": null,
  "incomplete_details": null,
  "instructions": null,
  "metadata": null,
  "model": "",
  "object": "response",
  "output": [
    {
      "id": "msg_d89e8cb9-f8fa-4092-bb86-928e21839f64",
      "content": [
        {
          "annotations": [],
          "text": "你好，我是千问（Qwen），是阿里巴巴集团旗下的千问实验室自主研发的超大规模语言模型。我能够回答问题、创作文字，比如写故事、写公文、写邮件、写剧本等等，还能进行逻辑推理、编程，甚至表达观点和玩游戏。我的目标是成为你值得信赖的智能助手。有什么我可以帮到你的吗？",
          "type": "output_text",
          "logprobs": null
        }
      ],
      "role": "assistant",
      "status": "completed",
      "type": "message"
    }
  ],
  "parallel_tool_calls": false,
  "temperature": null,
  "tool_choice": "auto",
  "tools": [],
  "top_p": null,
  "background": null,
  "conversation": null,
  "max_output_tokens": null,
  "max_tool_calls": null,
  "previous_response_id": null,
  "prompt": null,
  "prompt_cache_key": null,
  "reasoning": null,
  "safety_identifier": null,
  "service_tier": null,
  "status": "completed",
  "text": null,
  "top_logprobs": null,
  "truncation": null,
  "usage": null,
  "user": null
}
```

#### 响应字段说明 <span id="d5ac8508f3ml7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.807018%" }} /><col style={{ width: "22.647528%" }} /><col style={{ width: "54.545455%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p><code>id</code></p></td><td><p>string</p></td><td><p>异步任务的唯一标识符。</p></td></tr><tr><td><p><code>status</code></p></td><td><p>string</p></td><td><p>任务的当前状态，详见<a href="/zh/model-studio/asynchronous-call-api-reference#2627a01eaf9z0">任务生命周期</a>。</p></td></tr><tr><td><p><code>output</code></p></td><td><p>array</p></td><td><p>任务的输出结果。当 <code>status</code> 为 <code>completed</code> 时，此字段包含最终结果。</p></td></tr></tbody></table>

### 取消任务 <span id="6af325d2b07kn" />

取消一个正在进行中的异步任务。此操作仅对状态为 `queued` 或 `running` 的任务有效。

<strong>Endpoint:</strong>`POST https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses/{RESPONSE_ID}/cancel`

> 请将 `{APP_ID}` 替换为实际的应用ID，将 `{RESPONSE_ID}` 替换为创建任务时返回的任务ID。

#### 请求示例 <span id="1d9884a1cdawr" />

<CodeGroup dropdown>
  ```python
  import asyncio
  import os
  from openai import AsyncOpenAI
  # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
  api_key=os.getenv("DASHSCOPE_API_KEY")
  app_id='APP_ID' # 1、替换为实际的应用 ID
  base_url = f'https://dashscope.aliyuncs.com/api/v2/apps/agent/{app_id}/compatible-mode/v1/'
  # 初始化客户端
  client = AsyncOpenAI(api_key=api_key, base_url=base_url)
  async def main():
    # 取消任务
    response = await client.responses.cancel("6e4ba287-0e93-4d67-9955-ef1017ed3384") # 2、替换为实际的任务 ID
    print(response.model_dump_json(indent=2))
  if __name__ == "__main__":
    asyncio.run(main())
  ```

  ```java
  import com.openai.client.OpenAIClient;
  import com.openai.client.okhttp.OpenAIOkHttpClient;
  import com.openai.models.responses.*;
  import com.fasterxml.jackson.databind.ObjectMapper;
  import com.fasterxml.jackson.databind.SerializationFeature;
  public class CancelTask {
      public static void main(String[] args) throws Exception {
          // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
          String apiKey = System.getenv("DASHSCOPE_API_KEY");
          String appId = "APP_ID"; // 1、替换为实际的应用 ID
          String baseUrl = "https://dashscope.aliyuncs.com/api/v2/apps/agent/" + appId + "/compatible-mode/v1/";
          OpenAIClient client = OpenAIOkHttpClient.builder()
                  .apiKey(apiKey)
                  .baseUrl(baseUrl)
                  .build();
          // 取消任务
          String responseId = "6e4ba287-0e93-4d67-9955-ef1017ed3384"; // 2、替换为实际的任务 ID
          Response response = client.responses().cancel(responseId);
          ObjectMapper mapper = new ObjectMapper();
          mapper.enable(SerializationFeature.INDENT_OUTPUT);
          System.out.println(mapper.writeValueAsString(response));
      }
  }
  ```

  ```bash
  # 若没有配置环境变量，请将 {DASHSCOPE_API_KEY} 替换为实际的阿里云百炼API Key，但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
  # 1、请将 APP_ID 替换为实际的应用ID
  # 2、请将 RESPONSE_ID 替换为创建异步任务返回的ID
  # 实际使用时请删除注释内容
  curl -X POST "https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses/{RESPONSE_ID}/cancel" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer ${DASHSCOPE_API_KEY}"
  ```
</CodeGroup>

#### 响应示例 <span id="cddf4db3306pf" />

```json
{
    "created_at": 1761877521,
    "id": "568b1459-126d-4719-b58f-b41078bacb48",
    "model": "",
    "object": "response",
    "output": [
    ],
    "parallel_tool_calls": false,
    "status": "cancelled",
    "tool_choice": "auto",
    "tools": [
    ]
}
```

#### 响应字段说明 <span id="db6dc2e84bvik" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.691057%" }} /><col style={{ width: "26.01626%" }} /><col style={{ width: "48.292683%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p><code>id</code></p></td><td><p>string</p></td><td><p>被操作任务的唯一标识符。</p></td></tr><tr><td><p><code>status</code></p></td><td><p>string</p></td><td><p><code>cancelled</code>：表示任务已成功取消。</p> <p><code>failed</code>：表示取消失败，因为任务已经处于终态（如<code>completed</code>或<code>failed</code>）。</p></td></tr><tr><td><p><code>object</code></p></td><td><p>string</p></td><td><p>对象类型，固定为<code>response</code>。</p></td></tr></tbody></table>

### 删除任务记录 <span id="54279eb5690js" />

删除一个已处于终态（`completed`, `failed`, `cancelled`）的任务记录。此操作不可恢复。

<strong>Endpoint:</strong>`DELETE https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses/{RESPONSE_ID}`

> 请将 `{APP_ID}` 替换为实际的应用ID，将 `{RESPONSE_ID}` 替换为创建任务时返回的任务ID。

#### 请求示例 <span id="4c3b4c54b2e2p" />

<CodeGroup dropdown>
  ```python Python expandable
  import asyncio
  import os
  from openai import AsyncOpenAI
  # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。
  # 不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
  api_key = os.getenv("DASHSCOPE_API_KEY")
  app_id = 'APP_ID'  # 1. 替换为您的应用ID
  base_url = f'https://dashscope.aliyuncs.com/api/v2/apps/agent/{app_id}/compatible-mode/v1/'
  client = AsyncOpenAI(api_key=api_key, base_url=base_url)
  async def main():
      # 2. 替换为要删除的任务ID
      response_id_to_delete = "b17c64f94-7034-403e-aef5-4f48647656d5"
      try:
          response_wrapper = await client.responses.with_raw_response.delete(response_id_to_delete)
          http_response = response_wrapper.http_response
          response_json = http_response.json()
          # 检查响应内容中的 "deleted" 字段是否为 true。
          if response_json.get("deleted") is True:
              print("任务记录已成功删除。")
              print(response_json)
          else:
              print("删除操作未成功")
              print(response_json)
      except Exception as e:
          print(f"\n调用API时发生严重错误: {e}")
  if __name__ == "__main__":
      asyncio.run(main())
  ```

  ```java Java
  import com.openai.client.OpenAIClient;
  import com.openai.client.okhttp.OpenAIOkHttpClient;
  import com.openai.models.responses.*;
  public class DeleteTask {
      public static void main(String[] args) throws Exception {
          // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
          String apiKey = System.getenv("DASHSCOPE_API_KEY");
          String appId = "APP_ID"; // 1. 替换为您的应用ID
          String baseUrl = "https://dashscope.aliyuncs.com/api/v2/apps/agent/" + appId + "/compatible-mode/v1/";
          OpenAIClient client = OpenAIOkHttpClient.builder()
                  .apiKey(apiKey)
                  .baseUrl(baseUrl)
                  .build();
          // 2. 替换为要删除的任务ID
          String responseId = "b17c64f94-7034-403e-aef5-4f48647656d5";
          client.responses().delete(responseId);
          System.out.println("任务记录已成功删除。ID: " + responseId);
      }
  }
  ```

  ```bash curl
  # 若没有配置环境变量，请将 {DASHSCOPE_API_KEY} 替换为实际的阿里云百炼API Key，但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
  # 1、请将 APP_ID 替换为实际的应用ID
  # 2、请将 RESPONSE_ID 替换为创建异步任务返回的ID
  # 实际使用时请删除注释内容
  curl -X DELETE "https://dashscope.aliyuncs.com/api/v2/apps/agent/{APP_ID}/compatible-mode/v1/responses/{RESPONSE_ID}" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer ${DASHSCOPE_API_KEY}"
  ```
</CodeGroup>

#### 响应示例 <span id="c34ec9858dn8w" />

```json
{
    "deleted": true,
    "id": "c8eddc0e-faf5-4228-a853-9c411194015f",
    "object": "response"
}
```

#### 响应字段说明 <span id="daf6eee6f2fhj" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18.72%" }} /><col style={{ width: "18.08%" }} /><col style={{ width: "63.2%" }} /></colgroup><thead><tr><th><p><strong>字段名</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p><code>id</code></p></td><td><p>string</p></td><td><p>被删除任务的唯一标识符。</p></td></tr><tr><td><p><code>deleted</code></p></td><td><p>boolean</p></td><td><p><code>true</code> 表示任务记录已成功删除。</p></td></tr><tr><td><p><code>object</code></p></td><td><p>string</p></td><td><p>对象类型，固定为<code>response</code>。</p></td></tr></tbody></table>

## 任务生命周期 <span id="2627a01eaf9z0" />

异步任务的生命周期包含以下状态：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "27.244094%" }} /><col style={{ width: "72.755906%" }} /></colgroup><thead><tr><th><p><strong>状态值</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p><code>queued</code></p></td><td><p>任务已成功创建，正在队列中等待系统调度。</p></td></tr><tr><td><p><code>running</code></p></td><td><p>任务正在执行中。</p></td></tr><tr><td><p><code>completed</code></p></td><td><p>任务已成功完成。可在响应的 <code>output</code> 字段中获取结果。</p></td></tr><tr><td><p><code>cancelled</code></p></td><td><p>任务在执行完成前被用户主动取消。</p></td></tr><tr><td><p><code>failed</code></p></td><td><p>任务执行失败。可在响应的 <code>output</code> 字段中查看错误信息。</p></td></tr></tbody></table>

## 错误码 <span id="1f1a8d78e3wck" />

如果应用调用失败并返回报错信息，请参阅[错误码](/zh/model-studio/error-code)进行解决。
