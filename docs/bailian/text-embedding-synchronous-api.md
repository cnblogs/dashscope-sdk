> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 同步接口API详情

> 通用文本向量模型可将文本数据转换为数值向量，用于语义搜索、推荐、聚类、分类等下游任务。

<meta property="og:title" content="同步接口API详情" />

## 模型概览 <span id="0e41425c577u4" />

<Tabs>
  <Tab title="北京">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
      <colgroup>
        <col style={{ width: "16.89%" }} />

        <col style={{ width: "16.8%" }} />

        <col style={{ width: "6.85%" }} />

        <col style={{ width: "9.75%" }} />

        <col style={{ width: "11.39%" }} />

        <col style={{ width: "24.03%" }} />

        <col style={{ width: "14.29%" }} />
      </colgroup>

      <thead>
        <tr>
          <th>
            <strong>模型名称</strong>
          </th>

          <th>
            <strong>向量维度</strong>
          </th>

          <th>
            <strong>最大行数</strong>
          </th>

          <th>
            <strong>单行最大</strong>[Token](/zh/model-studio/billing-for-model-studio)<strong>数</strong>
          </th>

          <th>
            <strong>单价（每千输入Token）</strong>
          </th>

          <th>
            <strong>支持语种</strong>
          </th>

          <th>
            <strong>免费额度</strong>[（注）](/zh/model-studio/new-free-quota#977b13081ab56)
          </th>
        </tr>
      </thead>

      <tbody>
        <tr>
          <td>
            qwen3.7-text-embedding
          </td>

          <td>
            2560、2,048、1,536、1,024（默认）、768、512、256
          </td>

          <td>
            20
          </td>

          <td>
            128,000
          </td>

          <td>
            0.0005元

            [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.00025元
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等201种主流语种与方言
          </td>

          <td rowSpan={4}>
            各100万Token

            有效期：百炼开通后90天内
          </td>
        </tr>

        <tr>
          <td>
            qwen3.7-text-embedding-flash
          </td>

          <td>
            1,024（默认）、768、512、256
          </td>

          <td>
            20
          </td>

          <td>
            128,000
          </td>

          <td>
            0.000125元

            [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.000063元
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等201种主流语种与方言
          </td>
        </tr>

        <tr>
          <td>
            text-embedding-v4

            > 属于[Qwen3-Embedding](https://qwenlm.github.io/zh/blog/qwen3-embedding/)系列
          </td>

          <td>
            2,048、1,536、1,024（默认）、768、512、256、128、64
          </td>

          <td rowSpan={2}>
            10
          </td>

          <td rowSpan={2}>
            8,192
          </td>

          <td rowSpan={2}>
            0.0005元

            [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.00025元
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等100+主流语种及多种编程语言
          </td>
        </tr>

        <tr>
          <td>
            text-embedding-v3
          </td>

          <td>
            1,024（默认）、768、512、256、128或64
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等50+主流语种
          </td>
        </tr>

        <tr>
          <td>
            text-embedding-v2
          </td>

          <td rowSpan={2}>
            1,536
          </td>

          <td rowSpan={2}>
            25
          </td>

          <td rowSpan={2}>
            2,048
          </td>

          <td rowSpan={2}>
            0.0007元

            [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.00035元
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语
          </td>

          <td rowSpan={2}>
            各50万Token

            有效期：百炼开通后90天内
          </td>
        </tr>

        <tr>
          <td>
            text-embedding-v1
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语
          </td>
        </tr>
      </tbody>
    </table>
  </Tab>

  <Tab title="新加坡">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
      <colgroup>
        <col style={{ width: "16.89%" }} />

        <col style={{ width: "16.8%" }} />

        <col style={{ width: "6.85%" }} />

        <col style={{ width: "9.75%" }} />

        <col style={{ width: "11.39%" }} />

        <col style={{ width: "24.03%" }} />

        <col style={{ width: "14.29%" }} />
      </colgroup>

      <thead>
        <tr>
          <th>
            <strong>模型名称</strong>
          </th>

          <th>
            <strong>向量维度</strong>
          </th>

          <th>
            <strong>最大行数</strong>
          </th>

          <th>
            <strong>单行最大</strong>Token<strong>数</strong>
          </th>

          <th>
            <strong>单价（每千输入Token）</strong>
          </th>

          <th>
            <strong>支持语种</strong>
          </th>

          <th>
            <strong>免费额度</strong>
          </th>
        </tr>
      </thead>

      <tbody>
        <tr>
          <td>
            qwen3.7-text-embedding
          </td>

          <td>
            2560、2,048、1,536、1,024（默认）、768、512、256
          </td>

          <td>
            20
          </td>

          <td>
            128,000
          </td>

          <td>
            0.000525元
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等201种主流语种与方言
          </td>

          <td>
            无免费额度
          </td>
        </tr>

        <tr>
          <td>
            text-embedding-v4

            > 属于[Qwen3-Embedding](https://qwenlm.github.io/zh/blog/qwen3-embedding/)系列
          </td>

          <td>
            2,048、1,536、1,024（默认）、768、512、256、128、64
          </td>

          <td rowSpan={2}>
            10
          </td>

          <td rowSpan={2}>
            8,192
          </td>

          <td rowSpan={2}>
            0.000514元
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等100+主流语种及多种编程语言
          </td>

          <td>
            无免费额度
          </td>
        </tr>

        <tr>
          <td>
            text-embedding-v3
          </td>

          <td>
            1,024（默认）、768、512
          </td>

          <td>
            中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等50+主流语种
          </td>

          <td>
            50万Token

            有效期：百炼开通后90天内
          </td>
        </tr>
      </tbody>
    </table>
  </Tab>
</Tabs>

关于模型限流，请参考[限流](/zh/model-studio/rate-limit#953ddcd76495l)。

## 前提条件 <span id="9d4c1e7735hni" />

若熟悉OpenAI生态，可使用兼容API快速迁移；DashScope API则提供更丰富的独有特性。请根据您的需求选择。

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。如果通过SDK调用，还需要[安装DashScope SDK](/zh/model-studio/install-sdk)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## OpenAI兼容 <span id="f4e8c45964o4x" />

<Tabs>
  <Tab title="公共云">
    <strong>使用SDK调用时需配置的base\_url：</strong>`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    <strong>使用HTTP方式调用时需配置的endpoint：</strong>`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/embeddings`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="d150fab3e6rgl" />

        <strong>model</strong>`string`<strong>必选</strong>

        调用的模型名称，参考[模型概览](/zh/model-studio/text-embedding-synchronous-api#0e41425c577u4)表格中的模型名称进行选择。

        <strong>input</strong>`array<string> 或 string 或 file`<strong>必选</strong>

        输入待处理的文本。可以是字符串（string）、字符串列表（array）或文件（file）。不同模型版本支持的文本长度和批量大小不同，具体如下：

        - <strong>qwen3.7-text-embedding 模型：</strong>

          - <strong>输入为字符串</strong>：最长支持 <strong>128,000</strong> Token。
          - <strong>输入为字符串列表或文件</strong>：最多支持 <strong>20</strong> 条（行），每条（行）最长支持 <strong>128,000</strong> Token。
        - <strong>text-embedding-v3 / v4 模型：</strong>

          - <strong>输入为字符串</strong>：最长支持 <strong>8,192</strong> Token。
          - <strong>输入为字符串列表或文件</strong>：最多支持 <strong>10</strong> 条（行），每条（行）最长支持 <strong>8,192</strong> Token。
        - <strong>text-embedding-v1 / v2 模型：</strong>

          - <strong>输入为字符串</strong>：最长支持 <strong>2,048</strong> Token。
          - <strong>输入为字符串列表或文件</strong>：最多支持 <strong>25</strong> 条（行），每条（行）最长支持 <strong>2,048</strong> Token。

        <strong>dimensions</strong> `integer` <strong>可选</strong>

        指定的向量维度，必须为以下值之一：`2560（仅适用于qwen3.7-text-embedding）、`2048（仅适用于`text-embedding-v4`）、1536（仅适用于`text-embedding-v4`）1024、768、512、256、128 或 64，默认值为1024。

        <strong>encoding\_format</strong> `string` <strong>可选</strong>

        用于控制返回的Embedding格式，入参支持`float`和`base64`，出参的Embedding数据也支持`float`和`base64`两种格式。实际返回格式取决于请求路由到的网关：

        - <strong>老网关</strong>：无论`encoding_format`设置为`float`还是`base64`，均只返回`float`类型数据，不支持输出`base64`数据。
        - <strong>新网关</strong>：

          - <strong>短请求</strong>：按`encoding_format`设置的格式返回数据。
          - <strong>长请求</strong>：内部路由到老网关，仅返回`float`类型数据。
      </td>

      <td>
        <Tabs>
          <Tab title="输入字符串">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  api_key=os.getenv("DASHSCOPE_API_KEY"),  # 如果您没有配置环境变量，请在此处用您的API Key进行替换
                  # 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              )

              completion = client.embeddings.create(
                  model="qwen3.7-text-embedding",
                  input='衣服的质量杠杠的，很漂亮，不枉我等了这么久啊，喜欢，以后还来这里买',
                  dimensions=1024, # 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                  encoding_format="float"
              )

              print(completion.model_dump_json())
              ```

              ```java Java expandable
              import com.openai.client.OpenAIClient;
              import com.openai.client.okhttp.OpenAIOkHttpClient;
              import com.openai.models.embeddings.CreateEmbeddingResponse;
              import com.openai.models.embeddings.EmbeddingCreateParams;

              public class Main {
                  public static void main(String[] args) {
                      // 创建客户端，使用环境变量中的API密钥
                      OpenAIClient client = OpenAIOkHttpClient.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              // 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
                              .baseUrl("https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1")
                              .build();

                      // 创建向量化请求参数
                      EmbeddingCreateParams params = EmbeddingCreateParams.builder()
                              .model("qwen3.7-text-embedding")
                              .input(EmbeddingCreateParams.Input.ofString("衣服的质量杠杠的，很漂亮，不枉我等了这么久啊，喜欢，以后还来这里买"))
                              // 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                              .dimensions(1024)
                              .build();

                      try {
                          // 发送请求并获取响应
                          CreateEmbeddingResponse response = client.embeddings().create(params);
                          System.out.println(response);
                      } catch (Exception e) {
                          System.err.println("请求出错，请查看错误码对照网页：");
                          System.err.println("https://help.aliyun.com/zh/model-studio/faq-about-alibaba-cloud-model-studio?spm=a2c4g.11186623.help-menu-2400256.d_0_17_0.18733a66lTrcHv#1c38f58abfcml");
                          System.err.println("错误详情：" + e.getMessage());
                          e.printStackTrace();
                      }
                  }
              }
              ```

              ```bash curl
              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/embeddings' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '{
                  "model": "qwen3.7-text-embedding",
                  "input": "风急天高猿啸哀，渚清沙白鸟飞回，无边落木萧萧下，不尽长江滚滚来",
                  "dimensions": 1024,
                  "encoding_format": "float"
              }'
              ```

              ```bash dashscope CLI
              # 通过 dashscope 命令行调用
              export DASHSCOPE_API_KEY="your-api-key"
              # 将 {WorkspaceId} 替换为业务空间ID，cn-beijing 替换为对应地域（新加坡: ap-southeast-1, 美东: us-east-1）
              export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
              dashscope embeddings create -m text-embedding-v3 -i "你好世界"
              ```
            </CodeGroup>

            <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>
          </Tab>

          <Tab title="输入字符串列表">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  api_key=os.getenv("DASHSCOPE_API_KEY"),  # 如果您没有配置环境变量，请在此处用您的API Key进行替换
                  # 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              )

              completion = client.embeddings.create(
                  model="qwen3.7-text-embedding",
                  input=['风急天高猿啸哀', '渚清沙白鸟飞回', '无边落木萧萧下', '不尽长江滚滚来'],
                  dimensions=1024,# 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                  encoding_format="float"
              )

              print(completion.model_dump_json())
              ```

              ```java Java expandable
              import com.openai.client.OpenAIClient;
              import com.openai.client.okhttp.OpenAIOkHttpClient;
              import com.openai.models.embeddings.CreateEmbeddingResponse;
              import com.openai.models.embeddings.EmbeddingCreateParams;

              import java.util.Arrays;
              import java.util.List;
              import java.util.ArrayList;

              public class Main {
                  public static void main(String[] args) {
                      // 创建客户端，使用环境变量中的API密钥
                      OpenAIClient client = OpenAIOkHttpClient.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              // 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
                              .baseUrl("https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1")
                              .build();

                      // 创建输入字符串列表
                      List<String> inputList = Arrays.asList(
                          "风急天高猿啸哀",
                          "渚清沙白鸟飞回",
                          "无边落木萧萧下",
                          "不尽长江滚滚来"
                      );

                      // 存储所有响应的列表
                      List<CreateEmbeddingResponse> responses = new ArrayList<>();

                      // 循环处理每个字符串
                      for (String text : inputList) {
                          try {
                              // 创建向量化请求参数
                              EmbeddingCreateParams params = EmbeddingCreateParams.builder()
                                      .model("qwen3.7-text-embedding")
                                      .input(EmbeddingCreateParams.Input.ofString(text))
                                      // 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                                      .dimensions(1024)
                                      .build();

                              // 发送请求并获取响应
                              CreateEmbeddingResponse response = client.embeddings().create(params);
                              responses.add(response);
                              System.out.println("处理文本: " + text);
                              System.out.println("向量化结果: " + response);
                              System.out.println("------------------------");
                          } catch (Exception e) {
                              System.err.println("处理文本时出错: " + text);
                              System.err.println("错误详情：" + e.getMessage());
                              e.printStackTrace();
                          }
                      }

                      // 打印总结信息
                      System.out.println("\n总共处理了 " + responses.size() + " 个文本");
                  }
              }
              ```

              ```bash curl
              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/embeddings' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '{
                  "model": "qwen3.7-text-embedding",
                  "input": [
                      "风急天高猿啸哀",
                      "渚清沙白鸟飞回",
                      "无边落木萧萧下",
                      "不尽长江滚滚来"
                      ],
                  "dimensions": 1024,
                  "encoding_format": "float"
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="输入文件">
            <Tabs>
              <Tab title="Python">
                ```python
                import os
                from openai import OpenAI

                client = OpenAI(
                    api_key=os.getenv("DASHSCOPE_API_KEY"),  # 如果您没有配置环境变量，请在此处用您的API Key进行替换
                    # 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
                    base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                )
                # 确保将 'texts_to_embedding.txt' 替换为您自己的文件名或路径
                with open('texts_to_embedding.txt', 'r', encoding='utf-8') as f:
                    completion = client.embeddings.create(
                        model="qwen3.7-text-embedding",
                        input=f,
                        dimensions=1024,# 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                        encoding_format="float"
                    )
                print(completion.model_dump_json())
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                import com.openai.client.OpenAIClient;
                import com.openai.client.okhttp.OpenAIOkHttpClient;
                import com.openai.models.embeddings.CreateEmbeddingResponse;
                import com.openai.models.embeddings.EmbeddingCreateParams;

                import java.io.BufferedReader;
                import java.io.FileReader;
                import java.io.IOException;
                import java.nio.file.Paths;

                public class Main {
                    public static void main(String[] args) {
                        // 创建客户端，使用环境变量中的API密钥
                        OpenAIClient client = OpenAIOkHttpClient.builder()
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                // 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
                                .baseUrl("https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1")
                                .build();

                        // 确保将 'texts_to_embedding.txt' 替换为您自己的文件名或绝对路径
                        String filePath = "/src/main/java/org/example/text_to_embedding.txt";

                        try {
                            // 读取文件内容
                            StringBuilder fileContent = new StringBuilder();
                            try (BufferedReader reader = new BufferedReader(new FileReader(filePath))) {
                                String line;
                                while ((line = reader.readLine()) != null) {
                                    fileContent.append(line).append("\n");
                                }
                            }

                            // 创建向量化请求参数
                            EmbeddingCreateParams params = EmbeddingCreateParams.builder()
                                    .model("qwen3.7-text-embedding")
                                    .input(EmbeddingCreateParams.Input.ofString(fileContent.toString()))
                                    // 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                                    .dimensions(1024)
                                    .build();

                            // 发送请求并获取响应
                            CreateEmbeddingResponse response = client.embeddings().create(params);
                            System.out.println(response);

                        } catch (IOException e) {
                            System.err.println("读取文件时出错：" + e.getMessage());
                            e.printStackTrace();
                        } catch (Exception e) {
                            System.err.println("请求出错，请查看错误码对照网页：");
                            System.err.println("https://help.aliyun.com/zh/model-studio/faq-about-alibaba-cloud-model-studio?spm=a2c4g.11186623.help-menu-2400256.d_0_17_0.18733a66lTrcHv#1c38f58abfcml");
                            System.err.println("错误详情：" + e.getMessage());
                            e.printStackTrace();
                        }
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                > 确保将 'texts\_to\_embedding.txt' 替换为您自己的文件名或路径

                ```bash
                FILE_CONTENT=$(cat texts_to_embedding.txt | jq -Rs .)
                curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/embeddings' \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "model": "qwen3.7-text-embedding",
                    "input": ['"$FILE_CONTENT"'],
                    "dimensions": 1024,
                    "encoding_format": "float"
                }'
                ```
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
        ### 响应对象 <span id="c451676492bm7" />

        <strong>data</strong>`array`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>embedding</strong>`list`

          本次调用返回object对象的value，类型是元素为float数据的数组，包含具体Embedding向量。

          <strong>index</strong>`integer`

          本结构中的算法结果对应的输入文字在输入数组中的索引值。

          <strong>object</strong>*string*

          本次调用返回的object对象类型，默认为embedding。
        </Accordion>

        <strong>model</strong>`string`

        本次调用的模型名。

        <strong>object</strong>*string*

        本次调用返回的data类型，默认为list。

        <strong>usage</strong>`object`

        本次请求的 Token 用量。`qwen3.7-text-embedding-flash` 返回 `prompt_tokens` 和 `total_tokens`；其他已上线的文本向量模型仅返回 `total_tokens`。

        <Accordion title="属性" defaultOpen>
          <strong>prompt\_tokens</strong>*integer*

          仅 `qwen3.7-text-embedding-flash` 返回。表示本次请求输入内容的 Token 总数，取值与 `total_tokens` 相同。

          <strong>total\_tokens</strong> *integer*

          本次请求消耗的 Token 总数，用于计量计费和审计。Token 数目按输入字符串被模型 Tokenizer 解析后的结果统计，与自行估算的字符数或词数可能不一致。
        </Accordion>

        <strong>id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <CodeGroup>
          ```json qwen3.7-text-embedding 成功响应 expandable
          {
            "data": [
              {
                "embedding": [
                  -0.0695386752486229, 0.030681096017360687, ...
                ],
                "index": 0,
                "object": "embedding"
              },
              ...
              {
                "embedding": [
                  -0.06348952651023865, 0.060446035116910934, ...
                ],
                "index": 5,
                "object": "embedding"
              }
            ],
            "model": "qwen3.7-text-embedding",
            "object": "list",
            "usage": {
              "total_tokens": 184
            },
            "id": "73591b79-d194-9bca-8bb5-xxxxxxxxxxxx"
          }
          ```

          ```json qwen3.7-text-embedding-flash 成功响应
          {
            "data": [
              {
                "embedding": [-0.0695386752486229, 0.030681096017360687, ...],
                "index": 0,
                "object": "embedding"
              }
            ],
            "model": "qwen3.7-text-embedding-flash",
            "object": "list",
            "usage": {
              "prompt_tokens": 184,
              "total_tokens": 184
            },
            "id": "73591b79-d194-9bca-8bb5-xxxxxxxxxxxx"
          }
          ```

          ```json 异常响应
          {
              "error": {
                  "message": "Incorrect API key provided. ",
                  "type": "invalid_request_error",
                  "param": null,
                  "code": "invalid_api_key"
              }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## DashScope <span id="69cac67a477k2" />

<Tabs>
  <Tab title="公共云">
    <strong>使用SDK调用时需配置的base\_url：</strong>https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1

    <strong>使用HTTP方式调用时需配置的endpoint：</strong>POST https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/embeddings/text-embedding/text-embedding

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="afcd41d2b57zf" />

        <strong>model</strong>`string`<strong>必选</strong>

        调用的模型，参考[模型概览](/zh/model-studio/text-embedding-synchronous-api#0e41425c577u4)表格中的模型名称进行选择。

        <strong>input</strong>`string`*或*`array<string>`<strong>必选</strong>

        输入待处理的文本。可以是字符串（string）、字符串列表（array）或文件（file）。不同模型版本支持的文本长度和批量大小不同，具体如下：

        - <strong>qwen3.7-text-embedding、qwen3.7-text-embedding-flash 模型：</strong>

          - <strong>输入为字符串</strong>：最长支持 <strong>128,000</strong> Token。
          - <strong>输入为字符串列表或文件</strong>：最多支持 <strong>20</strong> 条（行），每条（行）最长支持 <strong>128,000</strong> Token。
        - <strong>text-embedding-v3 / v4 模型：</strong>

          - <strong>输入为字符串</strong>：最长支持 <strong>8,192</strong> Token。
          - <strong>输入为字符串列表或文件</strong>：最多支持 <strong>10</strong> 条（行），每条（行）最长支持 <strong>8,192</strong> Token。
        - <strong>text-embedding-v1 / v2 模型：</strong>

          - <strong>输入为字符串</strong>：最长支持 <strong>2,048</strong> Token。
          - <strong>输入为字符串列表或文件</strong>：最多支持 <strong>25</strong> 条（行），每条（行）最长支持 <strong>2,048</strong> Token。

        <strong>text\_type</strong> `string`<strong>可选</strong>

        > 通过 HTTP 调用时，请将 <strong>text\_type</strong>放入parameters对象中。

        文本转换为向量后可以应用于检索、聚类、分类等下游任务，对检索这类非对称任务为了达到更好的检索效果建议区分查询文本（query）和底库文本（document）类型，入库、聚类、分类等对称任务可以不用特殊指定，采用系统默认值`document`即可。

        <strong>dimension</strong> `integer` <strong>可选</strong>

        > 通过 HTTP 调用时，请将 <strong>dimension</strong>放入parameters对象中。

        指定的向量维度，必须为以下值之一：`2560（仅适用于qwen3.7-text-embedding）、`2048（仅适用于`text-embedding-v4`）、1536（仅适用于`text-embedding-v4`）1024、768、512、256、128 或 64，默认值为1024。

        <strong>output\_type</strong> `string` <strong>可选</strong>

        > 通过 HTTP 调用时，请将 <strong>output\_type</strong>放入parameters对象中。

        用户指定输出离散向量表示只适用于`qwen3.7-text-embedding、``text-embedding-v3`与`text-embedding-v4`模型，取值在dense、sparse、dense\&sparse之间，默认取dense，只输出连续向量。稀疏向量输出（`sparse`和`dense&sparse`）仅支持通过 DashScope 接口调用。

        <strong>instruct</strong> `string` <strong>可选</strong>

        添加自定义任务说明，可用于指导模型理解查询意图。建议使用英文撰写，通常可带来约 1%–5% 的效果提升。

        该参数仅在 `text_type` 取值为 `query` 时生效，并按输入文本的条数计入 Token 用量。`text_type` 取值为 `document` 或未设置 `text_type` 时，该参数不生效，也不计入 Token 用量。
      </td>

      <td>
        <Tabs>
          <Tab title="输入字符串">
            <CodeGroup dropdown>
              ```python Python
              import dashscope
              from http import HTTPStatus
              # 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              resp = dashscope.TextEmbedding.call(
                  model="qwen3.7-text-embedding",
                  input='衣服的质量杠杠的，很漂亮，不枉我等了这么久啊，喜欢，以后还来这里买',
                  dimension=1024,  # 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                  output_type="dense&sparse"
              )

              print(resp) if resp.status_code == HTTPStatus.OK else print(resp)
              ```

              ```java Java expandable
              import java.util.Arrays;
              import com.alibaba.dashscope.embeddings.TextEmbedding;
              import com.alibaba.dashscope.embeddings.TextEmbeddingParam;
              import com.alibaba.dashscope.embeddings.TextEmbeddingResult;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.utils.Constants;

              /**
               * 千问文本向量模型调用示例
               */
              public final class Main {
                  public static void main(String[] args) {
                      // 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
                      Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                      try {
                          // 构建请求参数
                          TextEmbeddingParam param = TextEmbeddingParam
                                  .builder()
                                  .model("qwen3.7-text-embedding")  // 使用text-embedding-v4模型
                                  .texts(Arrays.asList("衣服的质量杠杠的，很漂亮，不枉我等了这么久啊，喜欢，以后还来这里买"))  // 输入文本
                                  .parameter("dimension", 1024)  // 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                                  .build();

                          // 创建模型实例并调用
                          TextEmbedding textEmbedding = new TextEmbedding();
                          TextEmbeddingResult result = textEmbedding.call(param);

                          // 输出结果
                          System.out.println(result);

                      } catch (ApiException | NoApiKeyException e) {
                          System.out.println("调用失败：" + e.getMessage());
                      }
                  }
              }
              ```

              ```bash curl
              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/embeddings/text-embedding/text-embedding' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '{
                  "model": "qwen3.7-text-embedding",
                  "input": {
                      "texts": [
                      "风急天高猿啸哀，渚清沙白鸟飞回，无边落木萧萧下，不尽长江滚滚来"
                      ]
                  },
                  "parameters": {
                      "dimension": 1024,
                      "output_type": "dense"
                  }
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="输入字符串列表">
            <CodeGroup dropdown>
              ```python Python expandable
              import dashscope
              from http import HTTPStatus
              # 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
              dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

              DASHSCOPE_MAX_BATCH_SIZE = 10

              inputs = ['风急天高猿啸哀', '渚清沙白鸟飞回', '无边落木萧萧下', '不尽长江滚滚来']

              result = None
              batch_counter = 0
              for i in range(0, len(inputs), DASHSCOPE_MAX_BATCH_SIZE):
                  batch = inputs[i:i + DASHSCOPE_MAX_BATCH_SIZE]
                  resp = dashscope.TextEmbedding.call(
                      model="qwen3.7-text-embedding",
                      input=batch,
                      dimension=1024  # 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                  )
                  if resp.status_code == HTTPStatus.OK:
                      if result is None:
                          result = resp
                      else:
                          for emb in resp.output['embeddings']:
                              emb['text_index'] += batch_counter
                              result.output['embeddings'].append(emb)
                          result.usage['total_tokens'] += resp.usage['total_tokens']
                  else:
                      print(resp)
                  batch_counter += len(batch)

              print(result)
              ```

              ```java Java expandable
              import java.util.Arrays;
              import java.util.List;
              import com.alibaba.dashscope.embeddings.TextEmbedding;
              import com.alibaba.dashscope.embeddings.TextEmbeddingParam;
              import com.alibaba.dashscope.embeddings.TextEmbeddingResult;
              import com.alibaba.dashscope.exception.ApiException;
              import com.alibaba.dashscope.exception.NoApiKeyException;
              import com.alibaba.dashscope.embeddings.TextEmbeddingResultItem;
              import com.alibaba.dashscope.utils.Constants;

              public final class Main {
                  private static final int DASHSCOPE_MAX_BATCH_SIZE = 10;

                  public static void main(String[] args) {
                      // 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
                      Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                      List<String> inputs = Arrays.asList(
                              "风急天高猿啸哀",
                              "渚清沙白鸟飞回",
                              "无边落木萧萧下",
                              "不尽长江滚滚来"
                      );

                      TextEmbeddingResult result = null;
                      int batchCounter = 0;

                      for (int i = 0; i < inputs.size(); i += DASHSCOPE_MAX_BATCH_SIZE) {
                          List<String> batch = inputs.subList(i, Math.min(i + DASHSCOPE_MAX_BATCH_SIZE, inputs.size()));
                          TextEmbeddingParam param = TextEmbeddingParam.builder()
                                  .model("qwen3.7-text-embedding")
                                  .texts(batch)
                                  .parameter("dimension", 1024)  // 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                                  .build();

                          TextEmbedding textEmbedding = new TextEmbedding();
                          try {
                              TextEmbeddingResult resp = textEmbedding.call(param);
                              if (resp != null) {
                                  if (result == null) {
                                      result = resp;
                                  } else {
                                      for (var emb : resp.getOutput().getEmbeddings()) {
                                          emb.setTextIndex(emb.getTextIndex() + batchCounter);
                                          result.getOutput().getEmbeddings().add(emb);
                                      }
                                      result.getUsage().setTotalTokens(result.getUsage().getTotalTokens() + resp.getUsage().getTotalTokens());
                                  }
                              } else {
                                  System.out.println(resp);
                              }
                          } catch (ApiException | NoApiKeyException e) {
                              e.printStackTrace();
                          }
                          batchCounter += batch.size();
                      }

                      System.out.println(result);
                  }
              }
              ```

              ```bash curl
              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/embeddings/text-embedding/text-embedding' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header 'Content-Type: application/json' \
              --data '{
                  "model": "qwen3.7-text-embedding",
                  "input": {
                      "texts": [
                        "风急天高猿啸哀",
                        "渚清沙白鸟飞回",
                        "无边落木萧萧下",
                        "不尽长江滚滚来"
                      ]
                  },
                  "parameters": {
                        "dimension": 1024,
                        "output_type": "dense"
                  }
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="输入文件">
            <Tabs>
              <Tab title="Python">
                ```python
                from http import HTTPStatus
                from dashscope import TextEmbedding
                # 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
                dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

                with open('texts_to_embedding.txt', 'r', encoding='utf-8') as f:
                    resp = TextEmbedding.call(
                        model="qwen3.7-text-embedding",
                        input=f,
                        dimension=1024 # 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                    )

                    if resp.status_code == HTTPStatus.OK:
                        print(resp)
                    else:
                        print(resp)
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                import java.io.BufferedReader;
                import java.io.FileReader;
                import java.io.IOException;
                import com.alibaba.dashscope.embeddings.TextEmbedding;
                import com.alibaba.dashscope.embeddings.TextEmbeddingParam;
                import com.alibaba.dashscope.embeddings.TextEmbeddingResult;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.utils.Constants;

                public final class Main {
                    public static void main(String[] args) {
                        // 以下为华北2（北京）地域的配置，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的配置不同。
                        Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
                        try (BufferedReader reader = new BufferedReader(new FileReader("<文件所来自的内容根的路径>"))) {
                            StringBuilder content = new StringBuilder();
                            String line;
                            while ((line = reader.readLine()) != null) {
                                content.append(line).append("\n");
                            }

                            TextEmbeddingParam param = TextEmbeddingParam.builder()
                                    .model("qwen3.7-text-embedding")
                                    .text(content.toString())
                                    .parameter("dimension", 1024)  // 指定向量维度（仅 qwen3.7-text-embedding、text-embedding-v3及 text-embedding-v4支持该参数）
                                    .build();

                            TextEmbedding textEmbedding = new TextEmbedding();
                            TextEmbeddingResult result = textEmbedding.call(param);

                            if (result != null) {
                                System.out.println(result);
                            } else {
                                System.out.println("Failed to get embedding: " + result);
                            }
                        } catch (IOException | ApiException | NoApiKeyException e) {
                            e.printStackTrace();
                        }
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                > 确保将 'texts\_to\_embedding.txt' 替换为您自己的文件名或路径

                ```bash
                FILE_CONTENT=$(cat texts_to_embedding.txt | jq -Rs .)
                curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/embeddings/text-embedding/text-embedding' \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "model": "qwen3.7-text-embedding",
                    "input": {
                        "texts": ['"$FILE_CONTENT"']
                    },
                    "parameters": {
                        "dimension": 1024,
                        "output_type": "dense"
                    }
                }'
                ```
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
        ### 响应对象 <span id="e9c6861476m70" />

        <strong>status\_code</strong> `string`

        状态码，表示请求的执行结果（如 200 表示成功）。

        <strong>request\_id</strong> `string`

        请求唯一标识。可用于请求明细溯源和问题排查。

        <strong>code</strong> `string`

        请求失败，表示错误码，成功时返回参数中该参数为空。

        <strong>message</strong> `string`

        请求失败，表示失败详细信息，成功时返回参数中该参数为空。

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>embeddings</strong>`array`

          本次请求的算法输出内容，是一个由结构组成的数组，每一个数组中包含一个对应的输入 text 的算法输出内容。

          <Accordion title="属性">
            <strong>sparse\_embedding</strong> `array`

            对应字符串的算法输出离散向量表示 （sparse embedding仅适用于`text-embedding-v3`与`text-embedding-v4`）。

            <Accordion title="属性">
              <strong>index</strong> `integer`

              词汇或字符在词汇表中的位置索引。

              <strong>value</strong> `float`

              表示该 `Token` 的权重或重要性分数，值越高，表示该 `Token` 在当前文本上下文中的重要性或相关性越大。

              <strong>token</strong> `string`

              实际的文本单元或词汇表中的词。
            </Accordion>

            <strong>embedding</strong> `array`

            对应字符串的算法输出连续向量表示 （dense embedding)。

            <strong>text\_index</strong> `integer`

            本结构中的算法结果对应的输入文字在输入数组中的索引值。
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求的 Token 用量。`qwen3.7-text-embedding-flash` 返回 `prompt_tokens` 和 `total_tokens`；其他已上线的文本向量模型仅返回 `total_tokens`。

        <Accordion title="属性" defaultOpen>
          <strong>prompt\_tokens</strong> `integer`

          仅 `qwen3.7-text-embedding-flash` 返回。表示本次请求输入内容的 Token 总数，取值与 `total_tokens` 相同。

          <strong>total\_tokens</strong> `integer`

          本次请求消耗的 Token 总数，用于计量计费和审计。Token 数目按输入字符串被模型 Tokenizer 解析后的结果统计，与自行估算的字符数或词数可能不一致。

          设置 `instruct` 且 `text_type` 取值为 `query` 时，`instruct` 会按输入文本的条数重复计入 Token 用量。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json qwen3.7-text-embedding 成功响应 expandable
          {   "status_code": 200,
              "request_id": "1ba94ac8-e058-99bc-9cc1-7fdb37940a46",
              "code": "",
              "message": "",
              "output":{
                  "embeddings": [
                    {
                       "sparse_embedding":[
                         {"index":7149,"value":0.829,"token":"风"},
                         .....
                         {"index":111290,"value":0.9004,"token":"哀"}],
                       "embedding": [-0.006929283495992422,-0.005336422007530928, ...],
                       "text_index": 0
                    },
                    {
                       "sparse_embedding":[
                         {"index":246351,"value":1.0483,"token":"渚"},
                         .....
                         {"index":2490,"value":0.8579,"token":"回"}],
                       "embedding": [-0.006929283495992422,-0.005336422007530928, ...],
                       "text_index": 1
                    },
                    {
                       "sparse_embedding":[
                         {"index":3759,"value":0.7065,"token":"无"},
                         .....
                         {"index":1130,"value":0.815,"token":"下"}],
                       "embedding": [-0.006929283495992422,-0.005336422007530928, ...],
                       "text_index": 2
                    },
                    {
                       "sparse_embedding":[
                         {"index":562,"value":0.6752,"token":"不"},
                         .....
                         {"index":1589,"value":0.7097,"token":"来"}],
                       "embedding": [-0.001945948973298072,-0.005336422007530928, ...],
                       "text_index": 3
                    }
                  ]
              },
              "usage":{
                  "total_tokens":27
              }
          }
          ```

          ```json qwen3.7-text-embedding-flash 成功响应
          {
              "status_code": 200,
              "request_id": "1ba94ac8-e058-99bc-9cc1-7fdb37940a46",
              "code": "",
              "message": "",
              "output": {
                  "embeddings": [
                      {
                          "embedding": [-0.006929283495992422, -0.005336422007530928, ...],
                          "text_index": 0
                      }
                  ]
              },
              "usage": {
                  "prompt_tokens": 27,
                  "total_tokens": 27
              }
          }
          ```

          ```json 异常响应
          {
              "code":"InvalidApiKey",
              "message":"Invalid API-key provided.",
              "request_id":"xxxxxxxx"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。
