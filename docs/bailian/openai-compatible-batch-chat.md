> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI兼容-Batch Chat

> 对于数据标注、内容生成等无需实时响应的场景，实时推理API存在成本高、并发受限的问题。阿里云百炼的批量对话（Batch Chat）API ，保持了与实时API一致的同步调用方式，您只需发起请求等待最终结果返回。目前该功能享有 官网限时 5 折优惠 ，能将您的推理成本直接降低 50%。

<meta property="og:title" content="OpenAI兼容-Batch Chat" />

> 本接口仅支持提交单个请求。如需一次性传入多个请求，可通过文件方式提交，详情请参考[OpenAI兼容-Batch（文件输入）](/zh/model-studio/batch-interfaces-compatible-with-openai)。

## 工作原理 <span id="b5a1e1c0f0pm4" />

1. <strong>请求提交</strong>：客户端发起请求并建立连接。
2. <strong>连接保持</strong>：请求进入队列排队，客户端保持连接等待。
3. <strong>同步返回</strong>：请求处理完成后，服务端通过之前保持的连接，将完整结果一次性返回给客户端。

   > 如果超过最长等待时间，连接将自动断开并返回超时错误。

## 适用范围 <span id="db634c824cnsq" />

<Tabs>
  <Tab title="华北2（北京）">
    - <strong>文本生成模型：</strong>qwen3.8-max、qwen3.8-flash、qwen3.7-max、qwen3.7-plus、qwen3.6-plus、qwen3.7-flash、qwen3.6-flash、qwen3.5-plus、qwen3.5-flash、qwen-plus、qwen-flash
    - <strong>图像与视频理解模型：</strong>qwen3.8-max、qwen3.8-flash、qwen3.7-plus、qwen3.6-plus、qwen3.7-flash、qwen3.6-flash、qwen3.5-plus、qwen3.5-flash、qwen3.5-omni-plus、qwen3.5-omni-flash、qwen3-vl-plus

    <Tip>
      * 在Batch 场景下，`qwen3.8-max`、`qwen3.8-flash`、`qwen3.7-max`、`qwen3.7-plus`、`qwen3.6-plus`、`qwen3.7-flash`、`qwen3.6-flash`、`qwen3.5-plus`、`qwen3.5-flash`、`qwen3.5-omni-flash`和`qwen3.5-omni-plus`单次请求的上下文 Token 数最大支持 256K，`qwen3.5-omni-plus`、`qwen3.5-omni-flash`不支持语音输出。
      * 部分模型支持思考模式，开启后会产生思考`tokens`导致成本增加。
      * `qwen3.8`、`qwen3.7`、`qwen3.6`和`qwen3.5` 系列模型默认开启思考模式。建议使用混合思考模型时，显式设置`enable_thinking`参数（`true`开启/`false`关闭）。
      * 在 JSONL 请求体中，`enable_thinking` 为 `body` 的顶层参数，须与 `model` 同级传入，不能放在 `extra_body` 中。
    </Tip>
  </Tab>
</Tabs>

## 如何使用 <span id="0193a7c785jez" />

### 前提条件 <span id="775d9d6151wyi" />

- 已开通阿里云百炼服务，并已[获取 API Key](/zh/model-studio/get-api-key)。

  > 建议[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)中以降低API Key的泄露风险。
- 若通过OpenAI SDK进行调用，需要[安装SDK](/zh/model-studio/install-sdk)。

```bash
pip3 install -U openai
```

### 步骤1：配置 API 端点 <span id="59d4777741z2b" />

只需修改 API 端点（base\_url），即可轻松将现有的实时推理请求切换至批量推理。请根据调用方式，配置正确的 API 端点。

<strong>SDK配置：</strong>将`base_url`设置为`https://batch.dashscope.aliyuncs.com/compatible-mode/v1`

<strong>HTTP调用：</strong>请求端点为`POST https://batch.dashscope.aliyuncs.com/compatible-mode/v1/chat/completions`

### 步骤2：发起调用 <span id="54c7b37eabwub" />

此处以文本对话为例，展示如何调用 Batch Chat 接口。

请求的默认等待超时时间为3600秒（1小时），在多数情况下无需额外配置。

> 下方示例展示如何主动设置一个自定义的超时时间（60-3600秒）作为参考。

<Tabs>
  <Tab title="Python">
    <strong>请求示例</strong>

    ```python
    import os
    from openai import OpenAI

    client = OpenAI(
       # 若没有配置环境变量,可用阿里云百炼API Key将下行替换为：api_key="sk-xxx",但不建议在生产环境中直接将API Key硬编码到代码中,以减少API Key泄露风险.
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        base_url="https://batch.dashscope.aliyuncs.com/compatible-mode/v1",  # 阿里云百炼Batch chat API 的 URL
    ).with_options(timeout=1800.0) # 设置1800秒（30分钟）的等待时间，最长3600秒

    completion = client.chat.completions.create(
        model="qwen-plus",
        messages=[
            {"role": "system", "content": "You are a helpful assistant."},
            {"role": "user", "content": "你是谁？"},
        ]
    )
    print(completion.choices[0].message.content)
    ```

    <strong>响应示例</strong>

    ```json
    我是千问，阿里巴巴集团旗下的超大规模语言模型。我能够回答问题、创作文字，比如写故事、写公文、写邮件、写剧本、逻辑推理、编程等等，还能表达观点，玩游戏等。如果你有任何问题或需要帮助，欢迎随时告诉我！
    ```
  </Tab>

  <Tab title="Java">
    <strong>请求示例</strong>

    ```java expandable
    import com.openai.client.OpenAIClient;
    import com.openai.client.okhttp.OpenAIOkHttpClient;
    import com.openai.models.chat.completions.ChatCompletion;
    import com.openai.models.chat.completions.ChatCompletionCreateParams;

    import java.time.Duration;

    public class Main {
        public static void main(String[] args) {
            OpenAIClient client = OpenAIOkHttpClient.builder()
                    // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                    .baseUrl("https://batch.dashscope.aliyuncs.com/compatible-mode/v1")  // # 阿里云百炼Batch chat API 的 URL
                    .timeout(Duration.ofSeconds(1800)) // 设置等待时间1800秒（半小时），最长3600秒
                    .build();

            ChatCompletionCreateParams params = ChatCompletionCreateParams.builder()
                    .addUserMessage("你是谁")
                    .model("qwen-plus")
                    .build();

            try {
                ChatCompletion chatCompletion = client.chat().completions().create(params);
                System.out.println(chatCompletion);
            } catch (Exception e) {
                System.err.println("Error occurred: " + e.getMessage());
                e.printStackTrace();
            }
        }
    }
    ```

    <strong>响应示例</strong>

    ```json
    ChatCompletion{id=chatcmpl-a12c115e-15fc-94f9-a984-81bd65f0527b, choices=[Choice{finishReason=stop, index=0, logprobs=, message=ChatCompletionMessage{
    content=我是千问，阿里巴巴集团旗下的超大规模语言模型。我可以帮助你回答问题、创作文字、提供信息查询等服务。很高兴认识你！, refusal=, role=assistant, annotations=, audio=, functionCall=, toolCalls=, additionalProperties={}}, additionalProperties={}}], created=1763609020, model=qwen-plus, object_=chat.completion, serviceTier=, systemFingerprint=, usage=CompletionUsage{completionTokens=33, promptTokens=10, totalTokens=43, completionTokensDetails=, promptTokensDetails=, additionalProperties={}}, additionalProperties={}}
    ```
  </Tab>

  <Tab title="Node.js">
    <strong>请求示例</strong>

    ```javascript
    import OpenAI from "openai";

    const openai = new OpenAI(
        {
            // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
            apiKey: process.env.DASHSCOPE_API_KEY,
            baseURL: "https://batch.dashscope.aliyuncs.com/compatible-mode/v1", // 阿里云百炼Batch chat API的 URL
            // 在这里添加超时设置 (单位：毫秒)
            // 1800秒 * 1000 = 1,800,000 毫秒，最长支持 3,600,000 毫秒
            timeout: 1800 * 1000,
        }
    );

    async function main() {
        const completion = await openai.chat.completions.create({
            model: "qwen-plus",  //此处以qwen-plus为例，可按需更换模型名称。
            messages: [
                { role: "system", content: "You are a helpful assistant." },
                { role: "user", content: "你是谁？" }
            ],
        });
        console.log(JSON.stringify(completion))
    }

    main();
    ```

    <strong>响应示例</strong>

    ```json
    {
        "created": 1763618557,
        "usage": {
            "completion_tokens": 80,
            "prompt_tokens": 22,
            "total_tokens": 102
        },
        "model": "qwen-plus",
        "id": "chatcmpl-af23c086-8662-91eb-b236-892032ddee92",
        "choices": [
            {
                "finish_reason": "stop",
                "index": 0,
                "message": {
                    "role": "assistant",
                    "content": "我是千问，由阿里云研发的超大规模语言模型。我能够回答问题、创作文字，如写故事、公文、邮件、剧本等，还能进行逻辑推理、编程，表达观点，玩游戏等。我支持多种语言，包括中文、英文、德语、法语、西班牙语等。如果你有任何问题或需要帮助，欢迎随时告诉我！"
                }
            }
        ],
        "object": "chat.completion"
    }
    ```
  </Tab>

  <Tab title="Go">
    <strong>请求示例</strong>

    ```go expandable
    package main

    import (
        "context"
        "os"
        "time"

        "github.com/openai/openai-go"
        "github.com/openai/openai-go/option"
    )

    func main() {
        client := openai.NewClient(
        option.WithAPIKey(os.Getenv("DASHSCOPE_API_KEY")),
        // 阿里云百炼Batch chat API 的 URL
        option.WithBaseURL("https://batch.dashscope.aliyuncs.com/compatible-mode/v1"),
        )
        // 设置超时时间：1800秒 = 30分钟，最长支持3600秒
        ctx, cancel := context.WithTimeout(context.Background(), 3600*time.Second)
        defer cancel()

        chatCompletion, err := client.Chat.Completions.New(
        ctx, openai.ChatCompletionNewParams{
        Messages: []openai.ChatCompletionMessageParamUnion{
        openai.UserMessage("你是谁"),
        },
        Model: "qwen-plus",
        },
        )

        if err != nil {
        panic(err.Error())
        }

        println(chatCompletion.Choices[0].Message.Content)
    }
    ```

    <strong>响应示例</strong>

    ```json
    我是千问，由阿里云研发的超大规模语言模型。我可以生成各种类型的文本，如文章、故事、诗歌、故事等，并能够根据不同的场景和需求进行变换和扩展。此外，我还能够回答各种问题，提供帮助和解决方案。很高兴为您服务！
    ```
  </Tab>

  <Tab title="C#（HTTP）">
    <strong>请求示例</strong>

    ```csharp expandable
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            // 设置超时时间：1800秒 = 30分钟，最长支持3600秒
            Timeout = TimeSpan.FromSeconds(1800)
        };

        static async Task Main(string[] args)
        {
            // 若没有配置环境变量，请用百炼API Key将下行替换为：string? apiKey = "sk-xxx";
            string? apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("API Key 未设置。请确保环境变量 'DASHSCOPE_API_KEY' 已设置。");
                return;
            }

            // 设置请求 URL 和内容
            string url = "https://batch.dashscope.aliyuncs.com/compatible-mode/v1/chat/completions"; // Batch chat API 的 URL
            // 此处以qwen-plus为例，可按需更换模型名称。
            string jsonContent = @"{
                ""model"": ""qwen-plus"",
                ""messages"": [
                    {
                        ""role"": ""system"",
                        ""content"": ""You are a helpful assistant.""
                    },
                    {
                        ""role"": ""user"",
                        ""content"": ""你是谁？""
                    }
                ]
            }";

            // 发送请求并获取响应
            string result = await SendPostRequestAsync(url, jsonContent, apiKey);

            // 输出结果
            Console.WriteLine(result);
        }

        private static async Task<string> SendPostRequestAsync(string url, string jsonContent, string apiKey)
        {
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                // 设置请求头
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // 发送请求并获取响应
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                // 处理响应
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"请求失败: {response.StatusCode}";
                }
            }
        }
    }
    ```

    <strong>响应示例</strong>

    ```json
    {
        "created": 1763620689,
        "usage": {
            "completion_tokens": 60,
            "prompt_tokens": 22,
            "total_tokens": 82
        },
        "model": "qwen-plus",
        "id": "chatcmpl-db85828d-af47-97a3-a2f4-120b8f7d72d3",
        "choices": [
            {
                "finish_reason": "stop",
                "index": 0,
                "message": {
                    "role": "assistant",
                    "content": "我是千问，阿里巴巴集团旗下的超大规模语言模型。我能够回答问题、创作文字，比如写故事、写公文、写邮件、写剧本、逻辑推理、编程等等，还能表达观点，玩游戏等。如果你有任何问题或需要帮助，欢迎随时告诉我！"
                }
            }
        ],
        "object": "chat.completion"
    }
    ```
  </Tab>

  <Tab title="PHP（HTTP）">
    <strong>请求示例</strong>

    ```php expandable
    <?php
    // 设置Batch Chat 请求 的 URL
    $url = 'https://batch.dashscope.aliyuncs.com/compatible-mode/v1/chat/completions';
    // 若没有配置环境变量，请用百炼API Key将下行替换为：$apiKey = "sk-xxx";
    $apiKey = getenv('DASHSCOPE_API_KEY');
    // 设置请求头
    $headers = [
        'Authorization: Bearer '.$apiKey,
        'Content-Type: application/json'
    ];
    // 设置请求体
    $data = [
        // 此处以qwen-plus为例，可按需更换模型名称。
        "model" => "qwen-plus",
        "messages" => [
            [
                "role" => "system",
                "content" => "You are a helpful assistant."
            ],
            [
                "role" => "user",
                "content" => "你是谁？"
            ]
        ]
    ];
    // 初始化cURL会话
    $ch = curl_init();
    // 设置cURL选项
    curl_setopt($ch, CURLOPT_URL, $url);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($data));
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
    // 在此处设置Batch chat 请求的超时时间：1800秒 = 30分钟，最长支持3600秒
    curl_setopt($ch, CURLOPT_TIMEOUT, 1800);
    // 执行cURL会话
    $response = curl_exec($ch);
    // 检查是否有错误发生
    if (curl_errno($ch)) {
        echo 'Curl error: ' . curl_error($ch);
    }
    // 关闭cURL资源
    curl_close($ch);
    // 输出响应结果
    echo $response;
    ?>
    ```

    <strong>响应示例</strong>

    ```json
    {
        "created": 1763621824,
        "usage": {
            "completion_tokens": 81,
            "prompt_tokens": 22,
            "total_tokens": 103
        },
        "model": "qwen-plus",
        "id": "chatcmpl-b25aeb86-5cfe-93ea-ab03-aa3de1381c23",
        "choices": [
            {
                "finish_reason": "stop",
                "index": 0,
                "message": {
                    "role": "assistant",
                    "content": "我是千问，由阿里云研发的超大规模语言模型。我能够回答问题、创作文字，如写故事、公文、邮件、剧本等，还能进行逻辑推理、编程，甚至表达观点和玩游戏。我支持多种语言，包括但不限于中文、英文、德语、法语、西班牙语等。如果你有任何问题或需要帮助，欢迎随时告诉我！"
                }
            }
        ],
        "object": "chat.completion"
    }
    ```
  </Tab>

  <Tab title="curl">
    <strong>请求示例</strong>

    > 设置最长等待时间max-time为1800秒，最长支持3600秒。

    ```bash
    curl -X POST https://batch.dashscope.aliyuncs.com/compatible-mode/v1/chat/completions \
    --max-time 1800 \
    -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
    -H "Content-Type: application/json" \
    -d '{
        "model": "qwen-plus",
        "messages": [
            {
                "role": "system",
                "content": "You are a helpful assistant."
            },
            {
                "role": "user",
                "content": "你是谁？"
            }
        ]
    }'
    ```

    <strong>响应示例</strong>

    ```json
    {
        "created": 1763622152,
        "usage": {
            "completion_tokens": 79,
            "prompt_tokens": 22,
            "total_tokens": 101
        },
        "model": "qwen-plus",
        "id": "chatcmpl-daa344d2-60df-9b79-81a4-28c9a10a0a0e",
        "choices": [
            {
                "finish_reason": "stop",
                "index": 0,
                "message": {
                    "role": "assistant",
                    "content": "我是千问，阿里巴巴集团旗下的超大规模语言模型。我能够回答问题、创作文字，如写故事、公文、邮件、剧本等，还能进行逻辑推理、编程，表达观点，玩游戏等。我支持多种语言，包括但不限于中文、英文、德语、法语、西班牙语等。如果你有任何问题或需要帮助，欢迎随时告诉我！"
                }
            }
        ],
        "object": "chat.completion"
    }
    ```
  </Tab>
</Tabs>

## 使用限制 <span id="3d488affb644p" />

- <strong>等待时间</strong>：提交请求后，会同步等待结果，服务端最长会保持连接3600秒（1小时）。可根据实际需求设置自定义超时时间，取值范围为60-3600秒。
- <strong>并发限制：</strong>单个账户为每个模型最多可维持10,000个等待中的请求。超出此限制的新请求将被拒绝并返回相应错误码，直到有请求完成并释放出可用位置。
- <strong>调用速率：</strong>单个账户提交请求的频率上限为 1000 QPS（10,000次 / 10秒）。

  > 此上限为系统设定的理论最大值。在实际调用中，API的可用资源会受到整体系统负载的动态影响，建议您在代码中实现重试逻辑。

## 计费说明 <span id="fecc98d9514rv" />

- <strong>计费单价：</strong>按成功请求的输入和输出Token计费，目录价与对应模型实时调用价格一致。<strong>官网限时为 Batch Chat 调用提供 5 折优惠，其最终费用为实时调用价格的 50%</strong>，具体请参见[模型列表](/zh/model-studio/models)。
- <strong>计费范围：</strong>仅对任务中成功执行的请求进行计费。任何失败的请求（包括系统错误或超时）均不计费。

<Tip>
  批量推理为独立计费项，不支持[预付费](https://common-buy.aliyun.com/?commodityCode=sfm_llminference_spn_public_cn)（节省计划、资源包）、[新人免费额度](/zh/model-studio/new-free-quota)等优惠，以及[上下文缓存](/zh/model-studio/context-cache)等功能。
</Tip>

## 错误码 <span id="efc117f59a2gm" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="d1decfa0cf0dj" />

1. <strong>Batch Chat 请求耗时和实时 API 比较有区别吗？</strong>

   有区别。Batch Chat 请求需要排队等待调度，因此端到端耗时通常高于实时 API。请求在服务端最长等待时间为1小时，超时未执行将自动断开并返回错误。
2. <strong>如何选择使用 Batch Chat 还是Batch File？</strong>

   当业务逻辑需要以 API 同步调用的方式、高并发地提交大量独立的对话请求时，选择 Batch Chat。当需要处理的是包含大量请求的单个大文件，并且可以接受异步获取结果文件，则选择 Batch File。
3. <strong>Batch Chat 能保证请求全部完成吗？</strong>

   不保证。Batch Chat 使用的是 Batch 资源，完成情况取决于系统资源分配情况。如果系统资源繁忙，请求可能在队列中等待。若超过最长等待时间仍未被调度执行，连接将超时断开，此时请求不会被计费，可以稍后重试。

## 相关文档 <span id="78a981373cqe1" />

- 查看模型实时调用的完整参数列表，请参阅[OpenAI兼容-Chat](/zh/model-studio/qwen-api-via-openai-chat-completions)。
- 如需通过提交文件进行批量处理，并异步获取结果，请参阅[OpenAI兼容-Batch（文件输入）](/zh/model-studio/batch-interfaces-compatible-with-openai)。
