> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 在LangChain中使用阿里云百炼

> 本文主要介绍如何将阿里云百炼提供的模型集成到大模型应用开发框架LangChain中。

<meta property="og:title" content="在LangChain中使用阿里云百炼" />

## 前提条件 <span id="1dd449b3bc5sf" />

- 已开通阿里云百炼服务并获得API Key， 请参考[获取与配置 API Key](/zh/model-studio/get-api-key)。
- 已[将API Key配置到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## 聊天模型（Chat Model） <span id="8503294497krr" />

<Tabs>
  <Tab title="Python">
    <Tabs>
      <Tab title="OpenAI">
        只支持阿里云百炼的部分模型。完整列表请参考：[OpenAI 兼容模式支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)。调用费用、输入输出上限等请参考：[选择模型](/zh/model-studio/models)。

        使用前需要安装以下依赖：

        ```bash
        pip install langchain_openai
        ```

        模型调用：

        ```python
        from langchain_openai import ChatOpenAI
        import os

        chatLLM = ChatOpenAI(
            api_key=os.getenv("DASHSCOPE_API_KEY"),
            base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
            model="qwen-plus",  # 此处以qwen-plus为例，您可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
            # other params...
        )
        messages = [
            {"role": "system", "content": "You are a helpful assistant."},
            {"role": "user", "content": "你是谁？"}]
        response = chatLLM.invoke(messages)
        print(response.model_dump_json())
        ```

        工具调用等进阶技巧请前往 LangChain 官方的 [ChatOpenAI](https://python.langchain.com/docs/integrations/chat/openai/)。完整的 API参考文档请前往 LangChain 官方的 [ChatOpenAI API Reference](https://python.langchain.com/api_reference/openai/chat_models/langchain_openai.chat_models.base.ChatOpenAI.html)。
      </Tab>

      <Tab title="DashScope">
        支持阿里云百炼所有的文本生成模型，完整列表与调用费用请参考：[选择模型](/zh/model-studio/models)。（也支持[部署](/zh/model-studio/model-deployment-introduction)后的模型）

        使用前需要安装以下依赖：

        ```bash
        pip install langchain-community
        pip install dashscope
        ```

        模型调用：

        ```python
        from langchain_community.chat_models.tongyi import ChatTongyi
        from langchain_core.messages import HumanMessage
        import os

        chatLLM = ChatTongyi(
            model="qwen-plus",   # 此处以qwen-plus为例，您可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
            dashscope_api_key=os.getenv("DASHSCOPE_API_KEY"),
            streaming=True,
            # other params...
        )
        res = chatLLM.stream([HumanMessage(content="hi")], streaming=True)
        for r in res:
            print("chat resp:", r.content)
        ```

        多模态调用、工具调用等进阶技巧请前往 LangChain 官方的 [ChatTongyi](https://python.langchain.com/docs/integrations/chat/tongyi/)。完整的 API参考文档请前往 LangChain 官方的 [ChatTongyi API Reference](https://python.langchain.com/api_reference/community/chat_models/langchain_community.chat_models.tongyi.ChatTongyi.html)。
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="JavaScript">
    <Tabs>
      <Tab title="OpenAI">
        只支持阿里云百炼的部分模型。完整列表请参考：[OpenAI 兼容模式支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)。调用费用、输入输出上限等请参考：[选择模型](/zh/model-studio/models)。

        使用前需要安装以下依赖：

        ```bash
        npm install @langchain/openai @langchain/core
        ```

        模型调用：

        ```javascript expandable
        import { ChatOpenAI } from "@langchain/openai";

        const llm = new ChatOpenAI({
          // 此处以qwen-plus为例，您可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          model: "qwen-plus",
          apiKey: process.env.DASHSCOPE_API_KEY,
          configuration: {
            baseURL: "https://dashscope.aliyuncs.com/compatible-mode/v1",
            // other params...
          },
          // other params...
        });

        const aiMsg = await llm.invoke([
          {
            role: "system",
            content:
              "You are a helpful assistant that translates English to French. Translate the user sentence.",
          },
          {
            role: "user",
            content: "I love programming.",
          },
        ]);
        console.log('---------------------------');
        console.log(aiMsg.content);
        ```

        工具调用等进阶技巧请前往 LangChain 官方的 [ChatOpenAI](https://js.langchain.com/docs/integrations/chat/openai/)。完整的 API参考文档请前往 LangChain 官方的 [ChatOpenAI API Reference](https://v03.api.js.langchain.com/classes/_langchain_openai.ChatOpenAI.html)。
      </Tab>

      <Tab title="DashScope">
        支持阿里云百炼所有的文本生成模型，完整列表与调用费用请参考：[选择模型](/zh/model-studio/models)。（也支持[部署](/zh/model-studio/model-deployment-introduction)后的模型）

        使用前需要安装以下依赖：

        ```bash
        npm install @langchain/community @langchain/core
        ```

        模型调用：

        ```javascript expandable
        import { ChatAlibabaTongyi } from "@langchain/community/chat_models/alibaba_tongyi";
        import { HumanMessage } from "@langchain/core/messages";

        // Default model is qwen-plus
        const qwenPlus = new ChatAlibabaTongyi({
          alibabaApiKey: process.env.DASHSCOPE_API_KEY,
          // other params...
        });

        // Use qwen-plus
        const qwenPlus = new ChatAlibabaTongyi({
        // 此处以qwen-plus为例，您可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          model: "qwen-plus",
          temperature: 1,
          alibabaApiKey: process.env.DASHSCOPE_API_KEY,
          // other params...
        });

        const messages = [new HumanMessage("Hello")];

        const res = await qwenTurbo.invoke(messages);

        const res2 = await qwenPlus.invoke(messages);

        console.log('---------------------------');
        console.log(res.content);
        console.log('---------------------------');
        console.log(res2.content);
        ```

        多模态调用、工具调用等进阶技巧请前往 LangChain 官方的 [ChatTongyi](https://js.langchain.com/docs/integrations/chat/alibaba_tongyi/)。完整的 API参考文档请前往 LangChain 官方的 [ChatTongyi API Reference](https://v03.api.js.langchain.com/classes/_langchain_community.chat_models_alibaba_tongyi.ChatAlibabaTongyi.html)。
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="Java">
    > LangChain4j 1.0.0-beta3 需要 Java 17 及以上版本。使用较低版本（如 Java 11）编译时会出现 `Unsupported class file major version 61` 错误。

    <Tabs>
      <Tab title="OpenAI">
        只支持阿里云百炼的部分模型。完整列表请参考：[OpenAI 兼容模式支持的模型列表](/zh/model-studio/compatibility-of-openai-with-dashscope#eadfc13038jd5)。调用费用、输入输出上限等请参考：[选择模型](/zh/model-studio/models)。

        借助LangChain4j开源库，您可以使用Java编码以实现相关功能。有Plain Java（纯Java）和Spring Boot这两种实现方式，更多的接口细节和代码示例，请参见[LangChain4j OpenAI官网](https://docs.langchain4j.dev/integrations/language-models/open-ai)。

        <Tabs>
          <Tab title="Plain Java">
            1. 添加依赖

               以Maven为例，在pom.xml中添加如下依赖：

            ```xml
            <!-- https://mvnrepository.com/artifact/dev.langchain4j/langchain4j-open-ai -->
            <dependency>
                <groupId>dev.langchain4j</groupId>
                <artifactId>langchain4j-open-ai</artifactId>
                <version>1.0.0-beta3</version>
            </dependency>
            ```

            2. 编写代码，调用模型

            ```java
            import dev.langchain4j.data.message.SystemMessage;
            import dev.langchain4j.data.message.UserMessage;
            import dev.langchain4j.model.chat.ChatLanguageModel;
            import dev.langchain4j.model.openai.OpenAiChatModel;

            public class LangChainOpenAITest {
                public static void main(String[] args) {
                    ChatLanguageModel model = OpenAiChatModel.builder()
                            .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                            .baseUrl("https://dashscope.aliyuncs.com/compatible-mode/v1")
                            .modelName("qwen-plus")
                            .build();

                    SystemMessage systemMessage = SystemMessage.from("你是心理专家");
                    UserMessage userMessage = UserMessage.from("你好");

                    System.out.println(model.chat(systemMessage, userMessage).aiMessage().text());
                }
            }
            ```
          </Tab>

          <Tab title="Spring Boot">
            1. 添加依赖

               以Maven为例，在pom.xml中添加如下依赖：

            ```xml
            <dependencies>
                <dependency>
                    <groupId>org.springframework.boot</groupId>
                    <artifactId>spring-boot-starter-web</artifactId>
                </dependency>

                <!-- https://mvnrepository.com/artifact/dev.langchain4j/langchain4j-open-ai-spring-boot-starter -->
                <dependency>
                    <groupId>dev.langchain4j</groupId>
            	<artifactId>langchain4j-open-ai-spring-boot-starter</artifactId>
            	<version>1.0.0-beta3</version>
                </dependency>
            </dependencies>
            ```

            2. 配置模型信息

               在application.properties中配置模型、端口号等信息：

            ```properties
            langchain4j.open-ai.chat-model.api-key=${DASHSCOPE_API_KEY}
            langchain4j.open-ai.chat-model.model-name=qwen-plus
            langchain4j.open-ai.chat-model.base-url=https://dashscope.aliyuncs.com/compatible-mode/v1

            server.port=9000
            ```

            3. 编写代码，调用模型

            ```java
            import dev.langchain4j.model.chat.ChatLanguageModel;
            import org.springframework.web.bind.annotation.GetMapping;
            import org.springframework.web.bind.annotation.RequestParam;
            import org.springframework.web.bind.annotation.RestController;

            @RestController
            public class ChatLanguageModelController {
                ChatLanguageModel chatLanguageModel;

                ChatLanguageModelController(ChatLanguageModel chatLanguageModel) {
                    this.chatLanguageModel = chatLanguageModel;
                }

                @GetMapping("/chat")
                public String chat(@RequestParam(value = "message", defaultValue = "你好") String message) {
                    return chatLanguageModel.chat(message);
                }
            }
            ```
          </Tab>
        </Tabs>
      </Tab>

      <Tab title="DashScope">
        支持阿里云百炼所有的文本生成模型，完整列表与调用费用请参考：[选择模型](/zh/model-studio/models)。（也支持[部署](/zh/model-studio/model-deployment-introduction)后的模型）

        借助LangChain4j开源库，您可以使用Java编码以实现相关功能。有Plain Java（纯Java）和Spring Boot这两种实现方式，更多的接口细节和代码示例，请参见[LangChain4j DashScope官网](https://docs.langchain4j.dev/integrations/language-models/dashscope)。

        <Tabs>
          <Tab title="Plain Java">
            1. 添加依赖

               以Maven为例，在pom.xml中添加如下依赖：

            ```xml
            <!-- https://mvnrepository.com/artifact/dev.langchain4j/langchain4j-community-dashscope -->
            <dependency>
                <groupId>dev.langchain4j</groupId>
                <artifactId>langchain4j-community-dashscope</artifactId>
                <version>1.0.0-beta3</version>
            </dependency>
            ```

            2. 编写代码，调用模型

            ```java expandable
            import dev.langchain4j.community.model.dashscope.QwenChatModel;
            import dev.langchain4j.community.model.dashscope.QwenStreamingChatModel;
            import dev.langchain4j.data.message.ChatMessage;
            import dev.langchain4j.data.message.SystemMessage;
            import dev.langchain4j.data.message.UserMessage;
            import dev.langchain4j.model.chat.ChatLanguageModel;
            import dev.langchain4j.model.chat.StreamingChatLanguageModel;
            import dev.langchain4j.model.chat.request.ChatRequest;
            import dev.langchain4j.model.chat.response.ChatResponse;
            import dev.langchain4j.model.chat.response.StreamingChatResponseHandler;

            public class LangChainDashScopeTest {
                public static void main(String[] args) {
                    chatLanguageModelTest();
            //        streamingChatLanguageModelTest();
                }

                public static void chatLanguageModelTest() {
                    ChatLanguageModel qwenModel = QwenChatModel.builder()
                            .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                            .modelName("qwen-plus")
                            .build();
            //        System.out.println(qwenModel.chat("你好"));
                    ChatRequest request = ChatRequest.builder().messages(new ChatMessage[]{SystemMessage.from("你是心理专家"), UserMessage.from("你好")}).build();
                    System.out.println(qwenModel.chat(request).aiMessage().text());
                }

                public static void streamingChatLanguageModelTest() {
                    StreamingChatLanguageModel model = QwenStreamingChatModel.builder()
                            .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                            .modelName("qwen-plus")
                            .build();
                    model.chat("你好", new StreamingChatResponseHandler() {
                        @Override
                        public void onPartialResponse(String s) {
                            System.out.println(s);
                        }

                        @Override
                        public void onCompleteResponse(ChatResponse chatResponse) {
                            System.out.println("对话结束");
                            System.exit(0);
                        }

                        @Override
                        public void onError(Throwable throwable) {
                            System.out.println("出现异常");
                            System.exit(0);
                        }
                    });
                }
            }
            ```
          </Tab>

          <Tab title="Spring Boot">
            1. 添加依赖

               以Maven为例，在pom.xml中添加如下依赖：

            ```xml
            <dependencies>
                <dependency>
                    <groupId>org.springframework.boot</groupId>
                    <artifactId>spring-boot-starter-web</artifactId>
                </dependency>

                <!-- https://mvnrepository.com/artifact/dev.langchain4j/langchain4j-community-dashscope-spring-boot-starter -->
                <dependency>
                    <groupId>dev.langchain4j</groupId>
            	<artifactId>langchain4j-community-dashscope-spring-boot-starter</artifactId>
            	<version>1.0.0-beta3</version>
                </dependency>
            </dependencies>
            ```

            2. 配置模型信息

               在application.properties中配置模型、端口号等信息：

            ```properties
            langchain4j.community.dashscope.chat-model.api-key=${DASHSCOPE_API_KEY}
            langchain4j.community.dashscope.chat-model.model-name=qwen-plus

            server.port=9000
            ```

            3. 编写代码，调用模型

            ```java
            import dev.langchain4j.community.model.dashscope.QwenChatModel;
            import org.springframework.web.bind.annotation.GetMapping;
            import org.springframework.web.bind.annotation.RequestParam;
            import org.springframework.web.bind.annotation.RestController;

            @RestController
            public class QwenChatModelController {
                QwenChatModel qwenChatModel;

                QwenChatModelController(QwenChatModel qwenChatModel) {
                    this.qwenChatModel = qwenChatModel;
                }

                @GetMapping("/chat")
                public String chat(@RequestParam(value = "message", defaultValue = "你好") String message) {
                    return qwenChatModel.chat(message);
                }
            }
            ```
          </Tab>
        </Tabs>
      </Tab>
    </Tabs>
  </Tab>
</Tabs>

## 文本嵌入模型（Embedding Model） <span id="e43d66bb40rpq" />

#### 支持的模型： <span id="8dde9a2a902qv" />

> MTEB、CMTEB是 Embedding 模型的通用评估指标，数值越大，模型效果越好。text-embedding-v3 与 text-embedding-v4模型当前无法通过 LangChain 框架接口指定向量维度，默认采用 1024 维度作为输出向量维度值。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "21.787709%" }} /><col style={{ width: "12.011173%" }} /><col style={{ width: "25.558659%" }} /><col style={{ width: "13.268156%" }} /><col style={{ width: "27.374302%" }} /></colgroup><thead><tr><th><p><strong>模型</strong></p></th><th><p><strong>MTEB</strong></p></th><th><p><strong>MTEB（Retrieval task）</strong></p></th><th><p><strong>CMTEB</strong></p></th><th><p><strong>CMTEB (Retrieval task)</strong></p></th></tr></thead><tbody><tr><td><p>text-embedding-v1</p></td><td><p>58.30</p></td><td><p>45.47</p></td><td><p>59.84</p></td><td><p>56.59</p></td></tr><tr><td><p>text-embedding-v2</p></td><td><p>60.13</p></td><td><p>49.49</p></td><td><p>62.17</p></td><td><p>62.78</p></td></tr><tr><td><p>text-embedding-v3（1024维度）</p></td><td><p>63.39</p></td><td><p>55.41</p></td><td><p>68.92</p></td><td><p>73.23</p></td></tr><tr><td><p>text-embedding-v4（1024维度）</p></td><td><p>68.36</p></td><td><p>59.30</p></td><td><p>70.14</p></td><td><p>73.98</p></td></tr></tbody></table>

<Tabs>
  <Tab title="Python">
    <Tabs>
      <Tab title="DashScope">
        使用前需要安装以下依赖：

        ```bash
        pip install langchain-community
        pip install dashscope
        ```

        模型调用：

        ```python
        from langchain_community.embeddings import DashScopeEmbeddings
        embeddings = DashScopeEmbeddings(
            model="text-embedding-v4",
            # other params...
        )

        text = "This is a test document."

        query_result = embeddings.embed_query(text)
        print("文本向量长度：", len(query_result), sep='')

        doc_results = embeddings.embed_documents(
            [
                "Hi there!",
                "Oh, hello!",
                "What's your name?",
                "My friends call me World",
                "Hello World!"
            ])
        print("文本向量数量：", len(doc_results), "，文本向量长度：", len(doc_results[0]), sep='')
        ```

        详细介绍与更多使用方式请前往 LangChain 官方的 [DashScope Embeddings](https://python.langchain.com/docs/integrations/text_embedding/dashscope/)。完整的 API参考文档请前往 LangChain 官方的 [Embedding API Reference](https://python.langchain.com/api_reference/community/embeddings/langchain_community.embeddings.dashscope.DashScopeEmbeddings.html)。
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="JavaScript">
    <Tabs>
      <Tab title="DashScope">
        使用前需要安装以下依赖：

        ```bash
        npm install @langchain/community @langchain/core
        ```

        模型调用：

        ```javascript
        import { AlibabaTongyiEmbeddings } from "@langchain/community/embeddings/alibaba_tongyi";

        const model = new AlibabaTongyiEmbeddings({
          apiKey: process.env.DASHSCOPE_API_KEY,
          modelName: "text-embedding-v4",
          // other params...
          });
        const res = await model.embedQuery(
          "What would be a good company name a company that makes colorful socks?",
        );
        console.log('---------------------------');
        console.log({ res });
        ```

        详细介绍与更多使用方式请前往 LangChain 官方的 [DashScope Embeddings](https://js.langchain.com/docs/integrations/text_embedding/alibaba_tongyi/)。完整的 API参考文档请前往 LangChain 官方的 [Embedding API Reference](https://v03.api.js.langchain.com/classes/_langchain_community.embeddings_alibaba_tongyi.AlibabaTongyiEmbeddings.html)。
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="Java">
    <Tabs>
      <Tab title="DashScope">
        借助LangChain4j开源库，您可以使用Java编码以实现相关功能。更多的接口细节和代码示例，请参见[LangChain4j DashScope官网](https://docs.langchain4j.dev/integrations/language-models/dashscope)。

        1. 添加依赖

           以Maven为例，在pom.xml中添加如下依赖：

        ```xml
        <!-- https://mvnrepository.com/artifact/dev.langchain4j/langchain4j-community-dashscope -->
        <dependency>
            <groupId>dev.langchain4j</groupId>
            <artifactId>langchain4j-community-dashscope</artifactId>
            <version>1.0.0-beta3</version>
        </dependency>
        ```

        2. 编写代码，调用模型

        ```java
        import dev.langchain4j.community.model.dashscope.QwenEmbeddingModel;
        import dev.langchain4j.data.embedding.Embedding;

        import java.util.List;

        import static dev.langchain4j.data.segment.TextSegment.textSegment;
        import static java.util.Arrays.asList;

        public class LangChainEmbeddingsTest {
            public static void main(String[] args) {
                QwenEmbeddingModel model = QwenEmbeddingModel.builder()
                        .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                        .modelName("text-embedding-v4")
                        .build();

                List<Embedding> embeddings = model.embedAll(asList(textSegment("hello"), textSegment("how are you?")))
                        .content();
                System.out.println(embeddings.size());
            }
        }
        ```
      </Tab>
    </Tabs>
  </Tab>
</Tabs>

## 重排序模型（Reranker Model） <span id="510526a06d8by" />

#### 支持的模型： <span id="dd9114527blo8" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "12.5%" }} /><col style={{ width: "12.5%" }} /><col style={{ width: "12.5%" }} /><col style={{ width: "12.5%" }} /><col style={{ width: "12.5%" }} /><col style={{ width: "12.5%" }} /><col style={{ width: "12.5%" }} /><col style={{ width: "12.5%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>最大Document数量</strong></p></th><th><p><strong>单条最大输入Token</strong></p></th><th><p><strong>请求最大输入Token</strong></p></th><th><p><strong>语种支持</strong></p></th><th><p><strong>单价（每千Token）</strong></p></th><th><p><strong>免费额度</strong></p></th><th><p><strong>应用场景</strong></p></th></tr></thead><tbody><tr><td><p>qwen3-vl-rerank</p></td><td><p>100</p></td><td><p>8,000</p></td><td rowSpan={2}><p>120,000</p></td><td><p>中、英、日、韩、法、德等33种主流语言</p></td><td><p>图片：0.0018元</p> <p>文字：0.0007元</p></td><td rowSpan={3} /><td><ul><li><p>图像聚类</p></li> <li><p>跨模态搜索</p></li> <li><p>图片检索</p></li></ul></td></tr><tr><td><p>qwen3-rerank</p></td><td rowSpan={2}><p>500</p></td><td rowSpan={2}><p>4,000</p></td><td><p>中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等100+主流语种</p></td><td><p>0.0005元</p></td><td rowSpan={2}><ul><li><p>文本语义检索</p></li> <li><p>RAG应用</p></li></ul></td></tr><tr><td><p>gte-rerank-v2</p></td><td><p>30,000</p></td><td><p>中、英、日、韩、泰语、西、法、葡、德、印尼语、阿拉伯语等50余语种</p></td><td><p>0.0008元</p></td></tr></tbody></table>

<Tabs>
  <Tab title="Python">
    <Tabs>
      <Tab title="DashScope">
        使用前需要安装以下依赖：

        ```bash
        pip install langchain-community
        pip install dashscope
        ```

        模型调用：

        ```python
        from langchain_community.document_compressors.dashscope_rerank import DashScopeRerank

        sequence = ["text1", "text2", "text3"]

        reranker = DashScopeRerank(
            model="gte-rerank-v2",
            # other params...
        )
        print(reranker.rerank(documents=sequence, query="文本3", top_n=2))
        ```

        详细介绍与更多使用方式请前往 LangChain 官方的 [DashScope Rerank](https://python.langchain.com/docs/integrations/document_transformers/dashscope_rerank/)。完整的 API参考文档请前往 LangChain 官方的 [Rerank API Reference](https://python.langchain.com/api_reference/community/document_compressors/langchain_community.document_compressors.dashscope_rerank.DashScopeRerank.html)。
      </Tab>
    </Tabs>
  </Tab>
</Tabs>
