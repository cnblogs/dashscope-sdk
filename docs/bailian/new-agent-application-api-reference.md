> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 新版智能体应用 API 参考

> 本文介绍 DashScope API 调用阿里云百炼新版智能体应用的输入与输出参数。

<meta property="og:title" content="新版智能体应用 API 参考" />

<strong>相关指南：</strong>[新版智能体应用（Agent 2.0）](/zh/model-studio/new-single-agent-application)。

<Tip>
  本文档仅适用于华北2（北京）地域。
</Tip>

## 前置准备 <span id="e846d6db23z61" />

开始前，请确保您已完成以下操作：

1. <strong>创建应用：</strong>前往[应用管理](https://bailian.console.aliyun.com/?tab=app#/app-center)创建阿里云百炼<strong>新版智能体应用</strong>并获取应用 ID。
2. <strong>获取 API Key：</strong>通过[密钥管理](https://bailian.console.aliyun.com/?tab=app#/api-key)获取，并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
3. <strong>安装SDK（可选）：</strong>若使用 SDK 调用，请安装相应语言的[DashScope SDK](/zh/model-studio/install-sdk)。

## 调用方式 <span id="69cac67a477k2" />

- <strong>HTTP 接口调用</strong>

  请求地址：`POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion`

  > 其中 `APP_ID` 需替换为您的实际应用 ID。
- <strong>SDK 调用</strong>

  Python/Java SDK：已默认配置正确的 endpoint

  自定义 endpoint：可通过 base\_url 参数配置

<strong>在线调试</strong>：通过<strong>应用卡片 -> 发布 -> API 调试</strong>路径进入调试页面后，填写参数并点击运行即可。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="989b585548qnj" />

        <strong>app\_id</strong>`string`<strong>（必选）</strong>

        应用标识。

        在[应用管理](https://bailian.console.aliyun.com/#/app-center)的应用卡片上获取。

        > Java SDK中为 <strong>appId。</strong>通过 HTTP 调用时，请将实际的应用 ID 放入 <strong>URL</strong>中，替换`APP_ID`。

        <strong>prompt</strong>`string`<strong>（必选）</strong>

        用户的输入指令，用于指导应用生成回复。

        > 通过 HTTP 调用时，请将 <strong>prompt</strong>放入 <strong>input</strong>对象中。

        <strong>session\_id</strong>`string` （可选）

        历史对话标识。

        传入`session_id`时，请求将自动携带云端存储的对话历史。此时必须传递`prompt`。

        该 ID 在连续<strong>1 小时内</strong>无任何请求后将自动失效。

        > Java SDK 中为 <strong>setSessionId</strong>。通过 HTTP 调用时，请将 <strong>session\_id</strong>放入 <strong>input</strong>对象中。

        <strong>workspace</strong> `string` （可选）

        业务空间标识。相关文档：[获取Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h)。

        仅调用[子业务空间](/zh/model-studio/use-workspace)的应用时需传递`workspace ID`。

        > 通过 HTTP 调用时，请指定Header中的 <strong>X-DashScope-WorkSpace</strong>。

        <strong>stream</strong> `boolean`（可选） 默认值为 `false`

        是否以流式输出方式回复。

        推荐设置为`true`，可提升阅读体验并降低超时风险。

        参数值：

        - `false`（默认）：模型生成全部内容后一次性返回；
        - `true`（推荐）：边生成边输出，每生成一部分内容即返回一个数据块（chunk）。需实时逐个读取这些块以拼接完整回复。

        > 通过Java SDK实现流式输出请通过`streamCall`接口调用；通过HTTP实现流式输出请在Header中指定`X-DashScope-SSE`为`enable`。

        <strong>incremental\_output</strong> `boolean`（可选）默认值为 `false`

        在<strong>流式输出模式下</strong>是否开启增量输出。

        推荐设置为`true`，可提升阅读体验。

        参数值：

        - `false`（默认）：每次输出当前已经生成的整个序列，最后一次输出为生成的完整结果。

        ```plaintext
        I
        I like
        I like apple
        I like apple.
        ```

        - `true`（推荐）：增量输出，即后续输出内容不包含已输出的内容。需要实时地逐个读取这些片段以获得完整的结果。

        ```plaintext
        I
        like
        apple
        .
        ```

        > Java SDK中为<strong>incrementalOutput</strong>\*。\*通过HTTP调用时，请将<strong>incremental\_output</strong>放入<strong>parameters</strong>对象中。

        <strong>enable\_thinking</strong> `boolean` （可选）默认值为 `false`

        此参数用于在[深度思考模型](/zh/model-studio/deep-thinking)中切换思考模式和非思考模式。

        参数值：

        - False（默认）：非思考模式。直接返回最终答案（`text`字段）。
        - True：启用思考模式。模型先输出思考过程，再返回最终答案。

        优先级：

        - 若调用时未设置此参数，以应用内模型的思考模式开关状态为准。
        - 若调用时设置了`enable_thinking`，以 API 参数为准。

        <Tip>
          要获取思考过程的内容，必须同时将`has_thoughts`设为`True`，则：

          1. 思考过程：`thought`字段获取。
          2. 最终答案：`text`字段获取。
        </Tip>

        > 开启`enable_thinking`有极小概率不会输出思考过程。

        > Java SDK中为<strong>enableThinking</strong>。通过HTTP调用时，请将 <strong>enable\_thinking</strong>放入 <strong>parameters</strong>对象中。

        > Java Dashscope SDK的版本至少应为<strong>2.20.0</strong>。

        <strong>has\_thoughts</strong> `boolean` （可选）默认值为 `false`

        是否输出已开启思考模式的模型思考过程，在`thoughts`字段中查看。

        参数值：

        - True：输出。
        - False（默认）：不输出。

        > Java SDK 中为 <strong>hasThoughts</strong>。通过 HTTP 调用时，请将 <strong>has\_thoughts</strong>放入 <strong>parameters</strong>对象中。

        <strong>image\_list</strong> `array`（可选）

        图片列表。支持图像 URL 和 Data URL（Base64 编码）。应用内需选择<strong>[图像与视频理解](/zh/model-studio/vision)</strong>模型。

        `base64`编码格式可构建为 [Data URL](https://www.rfc-editor.org/rfc/rfc2397)：`data:[MIME_type];base64,{base64_image}`。详细说明和代码示例见本文档[视觉理解章节](/zh/model-studio/new-agent-application-api-reference)。

        > Java SDK 中为 <strong>images</strong>。通过 HTTP 调用时，请将 <strong>image\_list</strong>放入 <strong>input</strong>对象中。

        <strong>file\_list</strong> `array`（可选）

        文件 URL 列表。

        > Java SDK中为 <strong>files</strong>。通过HTTP调用时，请将 <strong>file\_list</strong>放入 <strong>input</strong>对象中。

        > Python Dashscope SDK 的版本至少应为1.24.7，Java Dashscope SDK的版本至少应为2.21.13。

        <strong>model\_id</strong>`string`（可选）

        模型名称。

        API 调用时，可通过此参数传递本次调用使用的模型名称。

        优先级：当通过 API 传递的`model_id`与控制台配置不同时，以 API 参数值为准。

        > Java SDK 中为 <strong>modelId</strong>。通过HTTP调用时，请将 <strong>model\_id</strong>放入 <strong>parameters</strong>对象中。

        > Java Dashscope SDK 的版本至少应为<strong>2.19.3</strong>。
      </td>

      <td>
        <Tabs>
          <Tab title="单轮对话">
            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                from dashscope import Application
                response = Application.call(
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 替换为实际的应用 ID
                    prompt='你是谁？')

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print(response.output.text)
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                // 建议dashscope SDK的版本 >= 2.12.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;

                public class Main {
                    public static void appCall()
                            throws ApiException, NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID")
                                .prompt("你是谁？")
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);

                        System.out.printf("text: %s\n",
                                result.getOutput().getText());
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.err.println("message："+e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "你是谁？"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```

                    > APP\_ID替换为实际的应用 ID。
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例</strong>

                    ```php expandable
                    <?php

                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你是谁？'
                        ]
                    ];

                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }}
                    else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "你是谁？"
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你是谁？""
                                    },
                                    ""parameters"": {},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "你是谁？",
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="多轮对话">
            多轮对话通过`session_id`维护会话上下文：

            1. 首次请求：无需传入 session\_id，响应中会返回新生成的session\_id。

            2. 后续请求：携带上一次响应的 session\_id 即可延续对话。

            3. 有效期：session\_id 在最后一次请求后 1 小时内有效。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python expandable
                import os
                from http import HTTPStatus
                from dashscope import Application
                def call_with_session():
                    response = Application.call(
                        # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        api_key=os.getenv("DASHSCOPE_API_KEY"),
                        app_id='APP_ID',  # 替换为实际的应用 ID
                        prompt='你是谁？')

                    if response.status_code != HTTPStatus.OK:
                        print(f'request_id={response.request_id}')
                        print(f'code={response.status_code}')
                        print(f'message={response.message}')
                        print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                        return response

                    responseNext = Application.call(
                                # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                api_key=os.getenv("DASHSCOPE_API_KEY"),
                                app_id='APP_ID',  # 替换为实际的应用 ID
                                prompt='你有什么技能?',
                                session_id=response.output.session_id)  # 上一轮response的session_id

                    if responseNext.status_code != HTTPStatus.OK:
                        print(f'request_id={responseNext.request_id}')
                        print(f'code={responseNext.status_code}')
                        print(f'message={responseNext.message}')
                        print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                    else:
                        print('%s\n session_id=%s\n' % (responseNext.output.text, responseNext.output.session_id))
                        # print('%s\n' % (response.usage))

                if __name__ == '__main__':
                    call_with_session()
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import java.util.Arrays;
                import java.util.List;
                public class Main {
                    public static void callWithSession()
                            throws ApiException, NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                // 替换为实际的应用 ID
                                .appId("APP_ID")
                                .prompt("你是谁？")
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);

                        param.setSessionId(result.getOutput().getSessionId());
                        param.setPrompt("你有什么技能?");
                        result = application.call(param);

                        System.out.printf("%s\n session_id: %s\n",
                                result.getOutput().getText(), result.getOutput().getSessionId());
                    }

                    public static void main(String[] args) {
                        try {
                            callWithSession();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例（上一轮对话）</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "你是谁？"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "你有什么技能？",
                            "session_id":"4f8ef7233dc641aba496cb201fa59f8c"
                        },
                        "parameters":  {},
                        "debug": {}
                    }'
                    ```
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例（上一轮对话）</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你是谁？'
                        ]
                    ];

                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        };
                        if (isset($response_data['output']['session_id'])) {
                            echo "session_id={$response_data['output']['session_id']}\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你有什么技能？',
                            // 替换为实际上一轮对话返回的session_id
                            'session_id' => '2e658bcb514f4d30ab7500b4766a8d43'
                        ]
                    ];

                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        };
                        if (isset($response_data['output']['session_id'])) {
                            echo "session_id={$response_data['output']['session_id']}\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例（上一轮对话）</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "你是谁？"
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                                console.log(`session_id=${response.data.output.session_id}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }
                    callDashScope();
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```javascript expandable
                    const axios = require('axios');

                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';// 替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;
                        // session_id替换为实际上一轮对话的session_id
                        const data = {
                            input: {
                                prompt: "你有什么技能？",
                                session_id: 'fe4ce8b093bf46159ea9927a7b22f0d3',
                            },
                            parameters: {},
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                                console.log(`session_id=${response.data.output.session_id}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }
                    callDashScope();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例（上一轮对话）</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你是谁？""
                                    },
                                    ""parameters"": {},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你有什么技能？"",
                                        ""session_id"": ""7b830e4cc8fe44faad0e648f9b71435f""
                                    },
                                    ""parameters"": {},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例（上一轮对话）</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "你是谁？",
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```

                    <strong>请求示例（下一轮对话）</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt":     "你有什么技能？",
                    			"session_id": "f7eea37f0c734c20998a021b688d6de2", // 替换为实际上一轮对话的session_id
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>

                > APP\_ID替换为实际的应用 ID。下一轮对话的输入参数`session_id`字段值替换为实际上一轮对话返回的session\_id值。
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="流式输出">
            通过`stream`实现流式输出。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                from dashscope import Application
                responses = Application.call(
                            # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            api_key=os.getenv("DASHSCOPE_API_KEY"),
                            app_id='APP_ID',
                            prompt='你是谁？',
                            stream=True,  # 流式输出
                            incremental_output=True)  # 增量输出

                for response in responses:
                    if response.status_code != HTTPStatus.OK:
                        print(f'request_id={response.request_id}')
                        print(f'code={response.status_code}')
                        print(f'message={response.message}')
                        print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                    else:
                        print(f'{response.output.text}\n')  # 处理只输出文本text
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                // 建议dashscope SDK的版本 >= 2.15.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import io.reactivex.Flowable;// 流式输出
                // 智能体应用调用实现流式输出结果

                public class Main {
                    public static void streamCall() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                // 替换为实际的应用 ID
                                .appId("APP_ID")
                                .prompt("你是谁?")
                                // 增量输出
                                .incrementalOutput(true)
                                .build();
                        Application application = new Application();
                        // .streamCall（）：流式输出内容
                        Flowable<ApplicationResult> result = application.streamCall(param);
                        result.blockingForEach(data -> {
                            System.out.printf("%s\n",
                                    data.getOutput().getText());
                        });
                    }
                    public static void main(String[] args) {
                        try {
                            streamCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --header 'X-DashScope-SSE: enable' \
                    --data '{
                        "input": {
                            "prompt": "你是谁？"

                        },
                        "parameters":  {
                            "incremental_output":true
                        },
                        "debug": {}
                    }'
                    ```

                    > APP\_ID替换为实际的应用 ID。
                  </Tab>

                  <Tab title="PHP">
                    <strong>请求示例</strong>

                    ```php expandable
                    <?php

                    // 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            'prompt' => '你是谁？'],
                        "parameters" => [
                            'incremental_output' => true]];// 增量输出
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, false); // 不返回传输的数据
                    curl_setopt($ch, CURLOPT_WRITEFUNCTION, function ($ch, $string) {
                        echo $string; // 处理流式数据
                        return strlen($string);
                    });
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key,
                        'X-DashScope-SSE: enable' // 流式输出
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);

                    if ($status_code != 200) {
                        echo "HTTP Status Code: $status_code\n";
                        echo "Request Failed.\n";
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>请求示例</strong>

                    <Accordion title="1.输出完整响应" defaultOpen>
                      ```javascript expandable
                      const axios = require('axios');

                      async function callDashScope() {
                          //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                          const apiKey = process.env.DASHSCOPE_API_KEY;
                          const appId = 'APP_ID';// 替换为实际的应用 ID

                          const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                          const data = {
                              input: {
                                  prompt: "你是谁？"
                              },
                              parameters: {
                                  'incremental_output' : 'true' // 增量输出
                              },
                              debug: {}
                          };

                          try {
                              console.log("Sending request to DashScope API...");

                              const response = await axios.post(url, data, {
                                  headers: {
                                      'Authorization': `Bearer ${apiKey}`,
                                      'Content-Type': 'application/json',
                                      'X-DashScope-SSE': 'enable' // 流式输出
                                  },
                                  responseType: 'stream' // 用于处理流式响应
                              });

                              if (response.status === 200) {
                                  // 处理流式响应
                                  response.data.on('data', (chunk) => {
                                      console.log(`Received chunk: ${chunk.toString()}`);
                                  });
                              } else {
                                  console.log("Request failed:");
                                  if (response.data.request_id) {
                                      console.log(`request_id=${response.data.request_id}`);
                                  }
                                  console.log(`code=${response.status}`);
                                  if (response.data.message) {
                                      console.log(`message=${response.data.message}`);
                                  } else {
                                      console.log('message=Unknown error');
                                  }
                              }
                          } catch (error) {
                              console.error(`Error calling DashScope: ${error.message}`);
                              if (error.response) {
                                  console.error(`Response status: ${error.response.status}`);
                                  console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                              }
                          }
                      }

                      callDashScope();
                      ```
                    </Accordion>

                    可展开折叠面板查看具体内容：

                    <Accordion title="2.只输出text字段内容">
                      ```javascript expandable
                      const axios = require('axios');
                      const { Transform } = require('stream');

                      async function callDashScope() {
                          //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                          const apiKey = process.env.DASHSCOPE_API_KEY;
                          const appId = 'APP_ID'; // 替换为实际的应用 ID

                          const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                          const data = {
                              input: { prompt: "你是谁？" },
                              parameters: { incremental_output: true }, // 增量输出
                              debug: {}
                          };

                          try {
                              console.log("Sending request to DashScope API...");

                              const response = await axios.post(url, data, {
                                  headers: {
                                      'Authorization': `Bearer ${apiKey}`,
                                      'Content-Type': 'application/json',
                                      'X-DashScope-SSE': 'enable' // 流式输出
                                  },
                                  responseType: 'stream' // 用于处理流式响应
                              });

                              if (response.status === 200) {
                                  // // 处理流式响应 SSE协议解析转换流
                                  const sseTransformer = new Transform({
                                      transform(chunk, encoding, callback) {
                                          this.buffer += chunk.toString();

                                          // 按SSE事件分割（两个换行符）
                                          const events = this.buffer.split(/\n\n/);
                                          this.buffer = events.pop() || ''; // 保留未完成部分

                                          events.forEach(eventData => {
                                              const lines = eventData.split('\n');
                                              let textContent = '';

                                              // 解析事件内容
                                              lines.forEach(line => {
                                                  if (line.startsWith('data:')) {
                                                      try {
                                                          const jsonData = JSON.parse(line.slice(5).trim());
                                                          if (jsonData.output?.text) {
                                                              textContent = jsonData.output.text;
                                                          }
                                                      } catch(e) {
                                                          console.error('JSON解析错误:', e.message);
                                                      }
                                                  }
                                              });

                                              if (textContent) {
                                                  // 添加换行符并推送
                                                  this.push(textContent + '\n');
                                              }
                                          });

                                          callback();
                                      },
                                      flush(callback) {
                                          if (this.buffer) {
                                              this.push(this.buffer + '\n');
                                          }
                                          callback();
                                      }
                                  });
                                  sseTransformer.buffer = '';

                                  // 管道处理
                                  response.data
                                      .pipe(sseTransformer)
                                      .on('data', (textWithNewline) => {
                                          process.stdout.write(textWithNewline); // 自动换行输出
                                      })
                                      .on('end', () => console.log(""))
                                      .on('error', err => console.error("管道错误:", err));

                              } else {
                                  console.log("请求失败，状态码:", response.status);
                                  response.data.on('data', chunk => console.log(chunk.toString()));
                              }
                          } catch (error) {
                              console.error(`API调用失败: ${error.message}`);
                              if (error.response) {
                                  console.error(`状态码: ${error.response.status}`);
                                  error.response.data.on('data', chunk => console.log(chunk.toString()));
                              }
                          }
                      }

                      callDashScope();
                      ```
                    </Accordion>
                  </Tab>

                  <Tab title="C#">
                    <strong>请求示例</strong>

                    ```csharp expandable
                    using System.Net;
                    using System.Text;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            //若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");
                            string appId = "APP_ID"; // 替换为实际的应用ID
                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                client.DefaultRequestHeaders.Add("X-DashScope-SSE", "enable");

                                string jsonContent = @"{
                                    ""input"": {
                                        ""prompt"": ""你是谁""
                                    },
                                    ""parameters"": {""incremental_output"": true},
                                    ""debug"": {}
                                }";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                                try
                                {
                                    var request = new HttpRequestMessage(HttpMethod.Post, url);
                                    request.Content = content;

                                    HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        Console.WriteLine("Request successful:");
                                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                                        using (var stream = await response.Content.ReadAsStreamAsync())
                                        using (var reader = new StreamReader(stream))
                                        {
                                            string? line; // 声明为可空字符串
                                            while ((line = await reader.ReadLineAsync()) != null)
                                            {
                                                if (line.StartsWith("data:"))
                                                {
                                                    string data = line.Substring(5).Trim();
                                                    Console.WriteLine(data);
                                                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    	"strings"
                    	"time"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体，其中incremental_output为是否开启流式响应
                    	requestBody := map[string]interface{}{
                    		"input": map[string]string{
                    			"prompt": "你是谁？",
                    		},
                    		"parameters": map[string]interface{}{
                    			"incremental_output": true,
                    		},
                    		"debug": map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头,其中X-DashScope-SSE设置为enable，表示开启流式响应
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")
                    	req.Header.Set("X-DashScope-SSE", "enable")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	if resp.StatusCode != http.StatusOK {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		body, _ := io.ReadAll(resp.Body)
                    		fmt.Println(string(body))
                    		return
                    	}

                    	// 处理流式响应
                    	reader := io.Reader(resp.Body)
                    	buf := make([]byte, 1024)
                    	for {
                    		n, err := reader.Read(buf)
                    		if n > 0 {
                    			data := string(buf[:n])
                    			lines := strings.Split(data, "\n")
                    			for _, line := range lines {
                    				line = strings.TrimSpace(line)
                    				if len(line) >= 5 && line[:5] == "data:" {
                    					timestamp := time.Now().Format("2006-01-02 15:04:05.000")
                    					fmt.Printf("%s: %s\n", timestamp, line[5:])
                    				} else if len(line) > 0 {
                    					fmt.Println(line)
                    				}
                    			}
                    		}
                    		if err != nil {
                    			if err == io.EOF {
                    				break
                    			}
                    			fmt.Printf("Error reading response: %v\n", err)
                    			break
                    		}
                    	}
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="文件问答">
            指定`file_list`传入文件（文档、图片、音视频）URL，启用文件问答功能。

            <strong>应用配置：</strong>应用内需开启<strong>预解析文件</strong>开关。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                # Dashscope SDK 的版本至少应为1.24.7
                import os
                from http import HTTPStatus
                from dashscope import Application

                responses = Application.call(
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='YOUR_APP_ID',  # 应用ID替换YOUR_APP_ID
                    prompt='一句话总结文件内容',
                    stream=True,  # 流式输出
                    incremental_output=True, # 增量输出
                    file_list=["https://dashscope.oss-cn-beijing.aliyuncs.com/audios/welcome.mp3"],
                )

                for response in responses:
                    if response.status_code != HTTPStatus.OK:
                        print(f'request_id={response.request_id}')
                        print(f'code={response.status_code}')
                        print(f'message={response.message}')
                        print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                    else:
                        print('%s\n' % (response.output.text))
                ```

                <strong>响应示例</strong>

                ```json
                该文件是一条简短的欢迎
                信息，内容为"欢迎
                与使用阿里云"。
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                // dashscope SDK 的版本 >= 2.21.13；
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;

                import java.util.Arrays;
                import java.util.Collections;
                import java.util.List;

                public class Main {
                    public static void appCall() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("YOUR_APP_ID") // 替换为实际的应用ID
                                .prompt("一句话总结文件内容")
                                .files(Arrays.asList(
                                        "https://dashscope.oss-cn-beijing.aliyuncs.com/audios/welcome.mp3"))
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n",
                                result.getOutput().getText());// 处理只输出文本text
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```

                <strong>响应示例</strong>

                ```json
                该文件是一条简短的欢迎信息，内容为"欢迎与使用阿里云"。
                ```
              </Tab>

              <Tab title="curl">
                <strong>请求示例</strong>

                ```bash
                curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/{YOUR_APP_ID}/completion \
                        --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                        --header 'Content-Type: application/json' \
                        --data '{
                        "input": {
                            "prompt": "一句话总结文件内容",
                            "file_list":["https://dashscope.oss-cn-beijing.aliyuncs.com/audios/welcome.mp3"]
                        },
                        "debug": {}
                        }'
                ```

                <strong>响应示例</strong>

                ```json
                {
                    "output": {
                        "finish_reason": "stop",
                        "reject_status": false,
                        "session_id": "545547dddcbf4296af0173da1a6e5a74",
                        "text": "该文件是阿里云的欢迎提示，内容为\"欢迎与使用阿里云\"。"
                    },
                    "usage": {
                        "models": [
                            {
                                "input_tokens": 103,
                                "model_id": "qwen-plus-latest",
                                "output_tokens": 304
                            }
                        ]
                    },
                    "request_id": "6900cfac-0aa5-4c6e-aa9c-7a69f7b4d5bc"
                }
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="视觉理解">
            通过 `image_list` 参数传入图像 URL 或 Data URL（Base64 编码）启用视觉理解功能。应用内需使用<strong>[图像与视频理解](/zh/model-studio/vision)</strong>模型。

            <Note>
              支持使用 Base64 编码本地图像。将图像编码为 Base64 字符串后，按`data:[MIME_type];base64,{base64_image}`格式构建 Data URL 传入。`MIME_type` 必须与图像格式匹配。常见格式：PNG 使用 `image/png`，JPEG 使用 `image/jpeg`，WebP 使用 `image/webp`。
            </Note>

            <Tabs>
              <Tab title="Python">
                <strong>URL请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                # 建议dashscope SDK 的版本 >= 1.20.14
                from dashscope import Application
                response = Application.call(
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 应用ID替换APP_ID
                    prompt='这是什么',
                    image_list=['https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg'],
                )

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))  # 处理只输出文本text
                    # print('%s\n' % (response.usage))
                ```

                <strong>Base64 编码示例</strong>

                ```python expandable
                import os
                import base64
                from http import HTTPStatus
                from dashscope import Application

                # 将本地图像编码为 Base64 字符串
                def encode_image(image_path):
                    with open(image_path, "rb") as image_file:
                        return base64.b64encode(image_file.read()).decode("utf-8")

                # 编码本地图像
                base64_image = encode_image("/path/to/your/image.jpeg")  # 替换为实际图像路径

                # 构建 Data URL（注意：MIME type 需与图像格式匹配）
                data_url = f"data:image/jpeg;base64,{base64_image}"

                # 调用应用 API
                response = Application.call(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    app_id='APP_ID',  # 应用ID替换APP_ID
                    prompt='这张图片里有什么？',
                    image_list=[data_url],  # 使用 Data URL
                )

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))
                ```
              </Tab>

              <Tab title="Java">
                <strong>URL请求示例</strong>

                ```java expandable
                // 建议dashscope SDK 的版本 >= 2.19.0；
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import java.util.Arrays;

                public class Main {
                    public static void appCall() throws NoApiKeyException, InputRequiredException {
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用ID
                                .prompt("这是什么？")
                                .images(Arrays.asList("https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"))
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n",
                                result.getOutput().getText());// 处理只输出文本text
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```

                <strong>Base64 编码示例</strong>

                ```java expandable
                // 建议dashscope SDK 的版本 >= 2.19.0
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import java.io.IOException;
                import java.nio.file.Files;
                import java.nio.file.Paths;
                import java.util.Arrays;
                import java.util.Base64;

                public class Main {
                    // 将本地图像编码为 Base64 字符串
                    public static String encodeImage(String imagePath) throws IOException {
                        byte[] imageBytes = Files.readAllBytes(Paths.get(imagePath));
                        return Base64.getEncoder().encodeToString(imageBytes);
                    }

                    public static void appCall() throws NoApiKeyException, InputRequiredException, IOException {
                        // 编码本地图像
                        String base64Image = encodeImage("/path/to/your/image.jpeg"); // 替换为实际图像路径

                        // 构建 Data URL（注意：MIME type 需与图像格式匹配）
                        String dataUrl = "data:image/jpeg;base64," + base64Image;

                        ApplicationParam param = ApplicationParam.builder()
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID") // 替换为实际的应用ID
                                .prompt("这张图片里有什么？")
                                .images(Arrays.asList(dataUrl)) // 使用 Data URL
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n", result.getOutput().getText());
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException | IOException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <Tabs>
                  <Tab title="curl">
                    <strong>URL请求示例</strong>

                    ```bash
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/{APP_ID}/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data '{
                        "input": {
                            "prompt": "这是什么",
                            "image_list":["https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"]
                        },
                        "debug": {}
                    }'
                    ```

                    <strong>Base64 编码示例</strong>

                    ```bash
                    # 将本地图像编码为 Base64 字符串
                    base64_image=$(base64 -i /path/to/your/image.jpeg)  # macOS/Linux

                    # 构建 Data URL（注意：MIME type 需与图像格式匹配）
                    data_url="data:image/jpeg;base64,${base64_image}"

                    # 调用应用 API
                    curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/{APP_ID}/completion \
                    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                    --header 'Content-Type: application/json' \
                    --data "{
                        \"input\": {
                            \"prompt\": \"这张图片里有什么？\",
                            \"image_list\": [\"${data_url}\"]
                        },
                        \"debug\": {}
                    }"
                    ```
                  </Tab>

                  <Tab title="PHP">
                    <strong>URL请求示例</strong>

                    ```php expandable
                    <?php
                    # 若没有配置环境变量，可用百炼API Key将下行替换为：$api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID'; // 替换为实际的应用 ID

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";

                    // 构造请求数据
                    $data = [
                        "input" => [
                            "prompt" => "这是什么",
                            "image_list" => ["https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"],
                        ],
                        "debug" => [],
                    ];
                    // 将数据编码为 JSON
                    $dataString = json_encode($data);

                    // 检查 json_encode 是否成功
                    if (json_last_error() !== JSON_ERROR_NONE) {
                        die("JSON encoding failed with error: " . json_last_error_msg());
                    }

                    // 初始化 cURL 对话
                    $ch = curl_init($url);

                    // 设置 cURL 选项
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, $dataString);
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, [
                        'Content-Type: application/json',
                        'Authorization: Bearer ' . $api_key
                    ]);

                    // 执行请求
                    $response = curl_exec($ch);

                    // 检查 cURL 执行是否成功
                    if ($response === false) {
                        die("cURL Error: " . curl_error($ch));
                    }

                    // 获取 HTTP 状态码
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    // 关闭 cURL 对话
                    curl_close($ch);
                    // 解码响应数据
                    $response_data = json_decode($response, true);
                    // 处理响应
                    if ($status_code == 200) {
                        if (isset($response_data['output']['text'])) {
                            echo "{$response_data['output']['text']}\n";
                        } else {
                            echo "No text in response.\n";
                        }
                    }else {
                        if (isset($response_data['request_id'])) {
                            echo "request_id={$response_data['request_id']}\n";}
                        echo "code={$status_code}\n";
                        if (isset($response_data['message'])) {
                            echo "message={$response_data['message']}\n";}
                        else {
                            echo "message=Unknown error\n";}
                    }
                    ?>
                    ```

                    <strong>Base64 编码示例</strong>

                    ```php expandable
                    <?php
                    $api_key = getenv("DASHSCOPE_API_KEY");
                    $application_id = 'APP_ID';

                    function encodeImage($imagePath) {
                        if (!file_exists($imagePath)) die("Error: Image file not found");
                        return base64_encode(file_get_contents($imagePath));
                    }

                    $base64Image = encodeImage("/path/to/your/image.jpeg");
                    $dataUrl = "data:image/jpeg;base64," . $base64Image;

                    $url = "https://dashscope.aliyuncs.com/api/v1/apps/$application_id/completion";
                    $data = ["input" => ["prompt" => "这张图片里有什么？", "image_list" => [$dataUrl]], "debug" => []];

                    $ch = curl_init($url);
                    curl_setopt($ch, CURLOPT_CUSTOMREQUEST, "POST");
                    curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($data));
                    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
                    curl_setopt($ch, CURLOPT_HTTPHEADER, ['Content-Type: application/json', 'Authorization: Bearer ' . $api_key]);

                    $response = curl_exec($ch);
                    $status_code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
                    curl_close($ch);

                    if ($status_code == 200) {
                        $response_data = json_decode($response, true);
                        echo $response_data['output']['text'];
                    }
                    ?>
                    ```
                  </Tab>

                  <Tab title="Node.js">
                    <strong>需安装相关依赖：</strong>

                    ```bash
                    npm install axios
                    ```

                    <strong>URL请求示例</strong>

                    ```javascript expandable
                    import axios from 'axios';
                    async function callDashScope() {
                        // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey='sk-xxx'。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';//替换为实际的应用 ID

                        const url = `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`;

                        const data = {
                            input: {
                                prompt: "这是什么",
                                image_list: ["https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"],
                            },
                            debug: {}
                        };

                        try {
                            const response = await axios.post(url, data, {
                                headers: {
                                    'Authorization': `Bearer ${apiKey}`,
                                    'Content-Type': 'application/json'
                                }
                            });

                            if (response.status === 200) {
                                console.log(`${response.data.output.text}`);
                            } else {
                                console.log(`request_id=${response.headers['request_id']}`);
                                console.log(`code=${response.status}`);
                                console.log(`message=${response.data.message}`);
                            }
                        } catch (error) {
                            console.error(`Error calling DashScope: ${error.message}`);
                            if (error.response) {
                                console.error(`Response status: ${error.response.status}`);
                                console.error(`Response data: ${JSON.stringify(error.response.data, null, 2)}`);
                            }
                        }
                    }

                    callDashScope();
                    ```

                    <strong>Base64 编码示例</strong>

                    ```javascript
                    import axios from 'axios';
                    import fs from 'fs';

                    async function callWithBase64() {
                        const apiKey = process.env.DASHSCOPE_API_KEY;
                        const appId = 'APP_ID';

                        const imageBuffer = fs.readFileSync('/path/to/your/image.jpeg');
                        const base64Image = imageBuffer.toString('base64');
                        const dataUrl = `data:image/jpeg;base64,${base64Image}`;

                        const response = await axios.post(
                            `https://dashscope.aliyuncs.com/api/v1/apps/${appId}/completion`,
                            { input: { prompt: "这张图片里有什么？", image_list: [dataUrl] } },
                            { headers: { 'Authorization': `Bearer ${apiKey}`, 'Content-Type': 'application/json' } }
                        );

                        console.log(response.data.output.text);
                    }

                    callWithBase64();
                    ```
                  </Tab>

                  <Tab title="C#">
                    <strong>URL请求示例</strong>

                    ```csharp expandable
                    using System;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            // 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                            string apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY")?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");;
                            string appId = "APP_ID";// 替换为实际的应用ID

                            if (string.IsNullOrEmpty(apiKey))
                            {
                                Console.WriteLine("请确保设置了 DASHSCOPE_API_KEY。");
                                return;
                            }

                            string url = $"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion";

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                                string jsonContent = $@"{{
                                    ""input"": {{
                                        ""prompt"": ""这是什么"",
                                        ""image_list"": [""https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg""]
                                    }},
                                    ""parameters"": {{
                                    }},
                                    ""debug"": {{}}
                                }}";

                                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                                try
                                {
                                    HttpResponseMessage response = await client.PostAsync(url, content);

                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                                        string responseBody = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(responseBody);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error calling DashScope: {ex.Message}");
                                }
                            }
                        }
                    }
                    ```

                    <strong>Base64 编码示例</strong>

                    ```csharp expandable
                    using System;
                    using System.IO;
                    using System.Net.Http;
                    using System.Text;
                    using System.Threading.Tasks;

                    class Program
                    {
                        static async Task Main(string[] args)
                        {
                            var apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY");
                            var appId = "APP_ID";

                            byte[] imageBytes = File.ReadAllBytes("/path/to/your/image.jpeg");
                            var base64Image = Convert.ToBase64String(imageBytes);
                            var dataUrl = $"data:image/jpeg;base64,{base64Image}";

                            using var client = new HttpClient();
                            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                            var json = $@"{{""input"": {{""prompt"": ""这张图片里有什么？"", ""image_list"": [""{dataUrl}""]}}, ""debug"": {{}}}}";
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            var response = await client.PostAsync($"https://dashscope.aliyuncs.com/api/v1/apps/{appId}/completion", content);

                            Console.WriteLine(await response.Content.ReadAsStringAsync());
                        }
                    }
                    ```
                  </Tab>

                  <Tab title="Go">
                    <strong>URL请求示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	// 若没有配置环境变量，可用百炼API Key将下行替换为：apiKey := "sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID" // 替换为实际的应用 ID

                    	if apiKey == "" {
                    		fmt.Println("请确保设置了DASHSCOPE_API_KEY。")
                    		return
                    	}

                    	url := fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId)

                    	// 创建请求体
                    	requestBody := map[string]interface{}{
                    		"input": map[string]interface{}{
                    			"prompt":     "这是什么",
                    			"image_list": []string{"https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"},
                    		},
                    		"parameters": map[string]interface{}{},
                    		"debug":      map[string]interface{}{},
                    	}

                    	jsonData, err := json.Marshal(requestBody)
                    	if err != nil {
                    		fmt.Printf("Failed to marshal JSON: %v\n", err)
                    		return
                    	}

                    	// 创建 HTTP POST 请求
                    	req, err := http.NewRequest("POST", url, bytes.NewBuffer(jsonData))
                    	if err != nil {
                    		fmt.Printf("Failed to create request: %v\n", err)
                    		return
                    	}

                    	// 设置请求头
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	// 发送请求
                    	client := &http.Client{}
                    	resp, err := client.Do(req)
                    	if err != nil {
                    		fmt.Printf("Failed to send request: %v\n", err)
                    		return
                    	}
                    	defer resp.Body.Close()

                    	// 读取响应
                    	body, err := io.ReadAll(resp.Body)
                    	if err != nil {
                    		fmt.Printf("Failed to read response: %v\n", err)
                    		return
                    	}

                    	// 处理响应
                    	if resp.StatusCode == http.StatusOK {
                    		fmt.Println("Request successful:")
                    		fmt.Println(string(body))
                    	} else {
                    		fmt.Printf("Request failed with status code: %d\n", resp.StatusCode)
                    		fmt.Println(string(body))
                    	}
                    }
                    ```

                    <strong>Base64 编码示例</strong>

                    ```go expandable
                    package main

                    import (
                    	"bytes"
                    	"encoding/base64"
                    	"encoding/json"
                    	"fmt"
                    	"io"
                    	"net/http"
                    	"os"
                    )

                    func main() {
                    	apiKey := os.Getenv("DASHSCOPE_API_KEY")
                    	appId := "APP_ID"

                    	imageData, _ := os.ReadFile("/path/to/your/image.jpeg")
                    	base64Image := base64.StdEncoding.EncodeToString(imageData)
                    	dataUrl := fmt.Sprintf("data:image/jpeg;base64,%s", base64Image)

                    	requestBody := map[string]interface{}{
                    		"input": map[string]interface{}{"prompt": "这张图片里有什么？", "image_list": []string{dataUrl}},
                    	}

                    	jsonData, _ := json.Marshal(requestBody)
                    	req, _ := http.NewRequest("POST", fmt.Sprintf("https://dashscope.aliyuncs.com/api/v1/apps/%s/completion", appId), bytes.NewBuffer(jsonData))
                    	req.Header.Set("Authorization", "Bearer "+apiKey)
                    	req.Header.Set("Content-Type", "application/json")

                    	client := &http.Client{}
                    	resp, _ := client.Do(req)
                    	defer resp.Body.Close()

                    	body, _ := io.ReadAll(resp.Body)
                    	fmt.Println(string(body))
                    }
                    ```
                  </Tab>
                </Tabs>
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="自定义参数传递">
            通过`biz_params`传递自定义参数。相关文档：[调用智能体应用-传递自定义参数](/zh/model-studio/call-single-agent-application#b774db7cdc0aa)。

            <Tabs>
              <Tab title="Python">
                <strong>请求示例</strong>

                ```python
                import os
                from http import HTTPStatus
                # 建议dashscope SDK 的版本 >= 1.14.0
                from dashscope import Application
                biz_params = {
                    # 智能体应用的自定义插件输入参数传递，自定义的插件ID替换<TOOL_ID>
                    "user_defined_params": {
                        "<TOOL_ID>": {
                            "article_index": 2}}}
                response = Application.call(
                        # 若没有配置环境变量，可用百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                        api_key=os.getenv("DASHSCOPE_API_KEY"),
                        app_id='APP_ID',
                        prompt='寝室公约内容',
                        biz_params=biz_params)

                if response.status_code != HTTPStatus.OK:
                    print(f'request_id={response.request_id}')
                    print(f'code={response.status_code}')
                    print(f'message={response.message}')
                    print(f'请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code')
                else:
                    print('%s\n' % (response.output.text))
                ```
              </Tab>

              <Tab title="Java">
                <strong>请求示例</strong>

                ```java expandable
                import com.alibaba.dashscope.app.*;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.InputRequiredException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.utils.JsonUtils;

                public class Main {
                    public static void appCall() throws NoApiKeyException, InputRequiredException {
                        String bizParams =
                                // 智能体应用的自定义插件输入参数传递，自定义的插件ID替换<TOOL_ID>
                                "{\"user_defined_params\":{\"<TOOL_ID>\":{\"article_index\":2}}}";
                        ApplicationParam param = ApplicationParam.builder()
                                // 若没有配置环境变量，可用百炼API Key将下行替换为：.apiKey("sk-xxx")。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .appId("APP_ID")
                                .prompt("寝室公约内容")
                                .bizParams(JsonUtils.parse(bizParams))
                                .build();

                        Application application = new Application();
                        ApplicationResult result = application.call(param);
                        System.out.printf("%s\n",
                                result.getOutput().getText());
                    }

                    public static void main(String[] args) {
                        try {
                            appCall();
                        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
                            System.out.printf("Exception: %s", e.getMessage());
                            System.out.println("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="HTTP">
                <strong>请求示例</strong>

                ```bash
                curl -X POST https://dashscope.aliyuncs.com/api/v1/apps/APP_ID/completion \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "input": {
                        "prompt": "寝室公约内容",
                        "biz_params":
                        {
                            "user_defined_params":
                            {
                                "<TOOL_ID>":
                                    {
                                    "article_index": 2
                                    }
                            }
                        }
                    },
                    "parameters":  {},
                    "debug":{}
                }'
                ```

                > APP\_ID替换为实际的应用 ID。\<TOOL\_ID>替换为插件ID。
              </Tab>
            </Tabs>
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
        ### 响应对象 <span id="f304e81314ngo" />
      </td>

      <td rowSpan={7}>
        <strong>成功响应示例</strong>

        ```json
        {
            "status_code": 200,
            "request_id": "fdfc3182-bc9d-4b45-a287-cd83b13aca02",
            "code": "",
            "message": "",
            "output": {
                "text": "你好！我是千问，阿里巴巴集团旗下的超大规模语言模型。我可以帮助你回答问题、创作文字，比如写故事、写公文、写邮件、写剧本、逻辑推理、编程等等。有什么需要我帮忙的吗？",
                "finish_reason": "stop",
                "session_id": "cbb2e26ac4cc4cc3b2d114e1f73c127e",
                "thoughts": null,
                "doc_references": null,
                "workflow_message": null
            },
            "usage": {
                "models": [
                    {
                        "model_id": "qwen-plus-latest",
                        "input_tokens": 142,
                        "output_tokens": 296
                    }
                ]
            }
        }
        ```

        <strong>异常响应示例</strong>

        在访问请求出错的情况下，输出的结果中会通过 code 和 message 指明错误原因。

        此处展示未传入正确`API-KEY`的异常响应示例。

        ```json
        request_id=1d14958f-0498-91a3-9e15-be477971967b,
        code=401,
        message=Invalid API-key provided.
        ```
      </td>
    </tr>

    <tr>
      <td>
        <strong>status\_code</strong> `string`

        返回的状态码。

        200表示请求成功，否则表示请求失败。

        请求失败可通过`code`获取错误码、`message`获取错误详细信息。

        > Java SDK不会返回该参数。调用失败会抛出异常，异常信息为<strong>status\_code</strong>和<strong>message</strong>的内容。
      </td>
    </tr>

    <tr>
      <td>
        <strong>request\_id</strong> `string`

        本次调用的唯一标识符。

        > Java SDK返回参数为`requestId`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong> `string`

        表示错误码，调用成功时为空值。

        > 只有Python SDK返回该参数。
      </td>
    </tr>

    <tr>
      <td>
        <strong>message</strong> `string`

        表示错误详细信息，请求成功则忽略。

        > 只有Python SDK返回该参数。
      </td>
    </tr>

    <tr>
      <td>
        <strong>output</strong> `object`

        调用结果信息。

        <Accordion title="output属性" defaultOpen>
          <strong>text</strong> `string`

          模型生成的回复内容。

          <strong>finish\_reason</strong> `string`

          完成原因。

          `stop`为自然结束（遇预设标记），`null`为强制中断（如达到最大长度限制或手动停止）。

          <strong>session\_id</strong>`string`

          当前对话的唯一标识。

          在后续请求中传入，可携带历史对话记录。

          <strong>thoughts</strong>`array`

          调用时将`has_thoughts`参数设置为True，即可在`thoughts`中查看[深度思考模型](/zh/model-studio/deep-thinking)的思考过程。

          <Accordion title="thoughts属性">
            <strong>thought</strong> `string`

            模型的思考过程。

            <strong>使用步骤</strong>

            1. 控制台应用内选择[深度思考模型](/zh/model-studio/deep-thinking)，并成功发布；
            2. API 调用时将 `has_thoughts` 参数设为 `True`。

            <strong>action\_type</strong> `string`

            大模型返回的执行步骤类型。如reasoning表示[深度思考模型](/zh/model-studio/deep-thinking)的思考过程。

            <strong>action\_name</strong> `string`

            执行的action名称，如思考过程。

            <strong>action</strong> `string`

            执行的步骤。

            <strong>action\_input\_stream</strong> `string`

            入参的流式结果。

            <strong>action\_input</strong> `string`

            输入参数。
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>doc\_references</strong> `null`

        新版智能体应用（Agent 2.0）将知识库统一为工具由智能体自主调用，该字段固定返回 `null`（与旧版智能体应用 Agent 1.0 不同）。如需在回答中展示知识来源，请在应用配置中开启<strong>展示回答来源</strong>功能，引用信息将以角标形式嵌入回答文本中。
      </td>
    </tr>

    <tr>
      <td>
        <strong>usage</strong> `object`

        表示本次请求使用的数据信息。

        <Accordion title="usage属性" defaultOpen>
          <strong>models</strong>`array`

          本次调用的模型信息。

          <Accordion title="models属性">
            <strong>model\_id</strong> `string`

            本次应用调用到的模型 ID。

            <strong>input\_tokens</strong> `integer`

            用户输入文本转换成Token后的长度。

            <strong>output\_tokens</strong> `integer`

            模型生成回复转换为Token后的长度。
          </Accordion>
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## QPM限制 <span id="qpm_limit_h2_new_agent" />

单应用默认QPM（每分钟请求数）为15000。

## 错误码 <span id="97e145aeabbwf" />

如果调用失败并返回报错信息，请参阅[错误码](/zh/model-studio/error-code)进行解决。
