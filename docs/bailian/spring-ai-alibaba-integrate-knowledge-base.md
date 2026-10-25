> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 通过Spring AI Alibaba检索阿里云百炼知识库

<meta property="og:title" content="通过Spring AI Alibaba检索阿里云百炼知识库" />

## 适用范围 <span id="48a4bafd73wfc" />

- <strong>JDK 版本：</strong>JDK 17 或更高版本
- <strong>Spring Boot版本：</strong>Spring Boot 3 GA或更高版本
- <strong>Spring AI Alibaba版本：</strong>本文的嵌套 SearchFilters 示例已在 1.1.2.3 版本验证

## 选择调用方式 <span id="saa-retrieval-methods" />

在 Spring AI Alibaba 应用中，可以使用 SDK 检索器，也可以通过 Spring HTTP 客户端直接调用 RAG API。两种方式都支持 `_operator` 与嵌套 `_conditions`，但调用入口和参数不同：

| 方式                | 检索入口                                                                                    | 知识库参数                                                                    | 适用场景                    |
| ----------------- | --------------------------------------------------------------------------------------- | ------------------------------------------------------------------------ | ----------------------- |
| SAA DashScope SDK | `/api/v1/indices/pipeline/{pipeline_id}/retrieve`                                       | 在 `DashScopeDocumentRetrieverOptions` 中设置 `indexName`，由 SDK 查询知识库并解析检索路径 | 使用现成的检索器接入 Advisor 问答流程 |
| RAG API（HTTP 直调）  | `https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/indices/rag/index/retrieve` | 请求体传入知识库 ID `index_id`                                                   | 自行控制 HTTP 请求、检索参数和结果处理  |

HTTP 直调仍可使用 SAA 的 `ChatClient` 生成回答，不需要更换框架。不要将 RAG API 的完整检索 URL 配置为 `DashScopeApi` 的 `baseUrl`：SDK 会自行拼接 Pipeline 路径，两者不是可直接互换的地址。

## 操作步骤 <span id="cce2df8a4dv78" />

以下步骤使用 SAA DashScope SDK。HTTP 直调示例见[通过 HTTP 直接调用 RAG API](#saa-rag-api-http)。

### 1. 获取项目代码 <span id="c342e0bb0bev9" />

完整的示例代码请参见[Spring AI Alibaba项目示例](https://github.com/spring-ai-alibaba/examples/tree/main)下的<strong>bailian-rag-knowledge</strong>。请将整个[examples](https://github.com/spring-ai-alibaba/examples/tree/main)目录下载到本地，以确保项目结构和依赖完整。

### 2. 环境配置 <span id="5d62688059rcf" />

[获取阿里云百炼的API Key](/zh/model-studio/get-api-key)并配置到环境变量，环境变量名使用`AI_DASHSCOPE_API_KEY`。使用环境变量可避免硬编码带来的安全风险。

> 如需操作[子业务空间](/zh/model-studio/use-workspace)的知识库，还需要获取[业务空间 ID](/zh/model-studio/use-workspace)，并配置到环境变量，环境变量名使用`AI_DASHSCOPE_WORKSPACE_ID`。

```yaml
# bailian-rag-knowledge的配置文件路径：bailian-rag-knowledge/src/main/resources/application.yml
spring:
  ai:
    dashscope:
      api-key: ${AI_DASHSCOPE_API_KEY} # 阿里云百炼的 API Key
      # workspace-id: ${AI_DASHSCOPE_WORKSPACE_ID} # 业务空间 ID（可选），检索默认业务空间的知识库时无需配置
```

### 3. 示例代码 <span id="f5d5b01550n2u" />

通过 `DashScopeApi` 检索阿里云百炼知识库。Spring AI Alibaba DashScope SDK 通过以下路径发送检索请求：

```text
/api/v1/indices/pipeline/{pipeline_id}/retrieve
```

该路径与 [RAG API](/zh/model-studio/rag-api-overview) 的 `/api/v1/indices/rag/index/retrieve` 是不同的调用入口，但均支持通过 `_operator` 和嵌套 `_conditions` 表达同一字段的多个 OR 条件。

<Accordion title="Controller示例代码">
  ```java
  import org.springframework.web.bind.annotation.*;

  import com.alibaba.cloud.ai.example.rag.knowledge.service.RagService;
  import reactor.core.publisher.Flux;

  @RestController
  @RequestMapping("/ai")
  public class CloudRagController {

      /** 注入CloudRagService */
      private final RagService cloudRagService;

      public CloudRagController(RagService cloudRagService) {
          this.cloudRagService = cloudRagService;
      }

      @GetMapping(value="/bailian/knowledge/generate", produces="text/event-stream")
      public Flux<String> generate(@RequestParam(value = "message",
                      defaultValue = "你好，请问你的知识库文档主要是关于什么内容的?") String message) {
          return cloudRagService.retrieve(message).map(x -> x.getResult().getOutput().getText());
      }
  }
  ```
</Accordion>

当用户提问时，`DashScopeDocumentRetriever`会检索与问题最相关的文本切片，并将这些上下文与原始问题一并提交给大模型（默认为`qwen-max`）生成回答。

<Accordion title="Service示例代码">
  ```java expandable
  import com.alibaba.cloud.ai.advisor.DocumentRetrievalAdvisor;
  import com.alibaba.cloud.ai.dashscope.api.DashScopeApi;
  import com.alibaba.cloud.ai.dashscope.rag.*;
  import org.springframework.ai.chat.client.ChatClient;
  import org.springframework.ai.chat.model.ChatResponse;
  import org.springframework.ai.chat.prompt.SystemPromptTemplate;
  import org.springframework.ai.document.Document;
  import org.springframework.ai.document.DocumentReader;
  import org.springframework.ai.rag.retrieval.search.DocumentRetriever;
  import org.springframework.ai.vectorstore.VectorStore;
  import org.springframework.beans.factory.annotation.Value;
  import org.springframework.core.io.Resource;
  import org.springframework.stereotype.Service;
  import reactor.core.publisher.Flux;

  import java.io.File;
  import java.io.FileOutputStream;
  import java.io.IOException;
  import java.io.InputStream;
  import java.util.List;
  import java.util.Map;

  @Service()
  public class CloudRagService implements RagService {
      /** 此处INDEX_NAME的值为待检索知识库的名称，知识库需要提前创建好 */
      private static final String INDEX_NAME = "测试知识库";

      /** 提示词模板 */
      private static final String retrievalSystemTemplate = """
          {query}

          以下是上下文信息。
          ---------------------
          {question_answer_context}
          ---------------------
          请根据上下文，而不是先验知识，回复用户评论。如果答案不在上下文中，告知用户认为你不能回答这个问题。
          """;

      private final ChatClient chatClient;

      private final DashScopeApi dashscopeApi;

      public CloudRagService(ChatClient.Builder builder, DashScopeApi dashscopeApi) {
          // 以下字段和值仅为示例，请替换为知识库中实际可检索的字段和值。
          Map<String, Object> orGroup = Map.of(
              "_operator", "OR",
              "_conditions", List.of(
                  Map.of("department", "Corporate"),
                  Map.of("department", Map.of("like", "%Research%")),
                  Map.of("document_type", Map.of("like", "%Policy%"))
              )
          );
          DashScopeDocumentRetrieverOptions retrieverOptions =
              DashScopeDocumentRetrieverOptions.builder()
                  .indexName(INDEX_NAME)
                  .searchFilters(List.of(orGroup))
                  .build();
          DocumentRetriever retriever = new DashScopeDocumentRetriever(
              dashscopeApi,
              retrieverOptions
          );

          this.dashscopeApi = dashscopeApi;
          // 初始化ChatClient，此处设置待检索知识库和待调用的模型
          this.chatClient = builder
              .defaultAdvisors(new DocumentRetrievalAdvisor(retriever, new SystemPromptTemplate(retrievalSystemTemplate)))
              // 模型默认使用qwen-max，可通过如下代码设置不同的模型
              //.defaultOptions(DashScopeChatOptions.builder().withModel("qwen-plus").build())
              .build();
      }

      @Override
      public Flux<ChatResponse> retrieve(String message) {
          return chatClient.prompt().user(message).stream().chatResponse();
      }
  }
  ```
</Accordion>

示例中的 `searchFilters` 表示“部门等于 `Corporate`，或部门名称包含 `Research`，或文档类型包含 `Policy`”。如不需要过滤，可省略 `searchFilters` 配置。完整语法和不同调用方式的差异请参见[知识库 SearchFilters](/zh/model-studio/how-to-use-search-filters)。

## 最佳实践：通过 HTTP 直接调用 RAG API <span id="saa-rag-api-http" />

以下示例在前文 SAA 项目中使用 `RestClient` 调用 RAG API，并将返回结果转换为 `Document`，接入同一个 `DocumentRetrievalAdvisor`。这不是切换 SAA SDK 的接口地址，而是实现一个独立的 HTTP 检索器。

运行前确认：

- 项目包含 Spring Web 和 Jackson（例如使用 `spring-boot-starter-web`）。`RestClient` 需要 Spring Framework 6.1 或更高版本，对应 Spring Boot 3.2 或更高版本；同时满足所用 SAA 版本的依赖要求。
- 配置 `AI_DASHSCOPE_API_KEY`、`AI_DASHSCOPE_WORKSPACE_ID` 和 `BAILIAN_INDEX_ID` 环境变量，分别为 API Key、目标业务空间 ID 和知识库 ID。HTTP 直调必须指定业务空间 ID，即使使用默认业务空间。鉴权与权限要求见 [RAG API 认证](/zh/model-studio/rag-api-authentication)。
- 知识库已完成数据导入。复用前文的 `orGroup`，并将其中的示例字段和值替换为知识库中实际可检索的字段和值。HTTP 请求体使用 `index_id`、`search_filters`，不是 OpenAPI 的 `IndexId`、`SearchFilters`。

**1. 实现 HTTP 检索器。** 将以下 `RagApiDocumentRetriever.java` 放在 `CloudRagService` 所在的包中，文件顶部的 `package` 声明与该服务保持一致。

<Accordion title="RagApiDocumentRetriever.java">
  ```java expandable
  import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
  import com.fasterxml.jackson.annotation.JsonProperty;
  import org.springframework.ai.document.Document;
  import org.springframework.ai.rag.Query;
  import org.springframework.ai.rag.retrieval.search.DocumentRetriever;
  import org.springframework.http.MediaType;
  import org.springframework.web.client.RestClient;

  import java.util.List;
  import java.util.Map;

  public class RagApiDocumentRetriever implements DocumentRetriever {
      private final RestClient client;
      private final String indexId;
      private final List<Map<String, Object>> searchFilters;

      public RagApiDocumentRetriever(RestClient client, String indexId,
                                     List<Map<String, Object>> searchFilters) {
          this.client = client;
          this.indexId = indexId;
          this.searchFilters = searchFilters;
      }

      @Override
      public List<Document> retrieve(Query query) {
          Map<String, Object> body = Map.of(
              "index_id", indexId,
              "query", query.text(),
              "search_filters", searchFilters
          );
          RetrievalResponse response = client.post()
              .uri("/api/v1/indices/rag/index/retrieve")
              .contentType(MediaType.APPLICATION_JSON)
              .body(body)
              .retrieve()
              .body(RetrievalResponse.class);

          if (response == null || !response.success() || response.data() == null
                  || response.data().nodes() == null) {
              String requestId = response == null ? null : response.requestId();
              throw new IllegalStateException("Knowledge base retrieval failed; request_id=" + requestId);
          }
          return response.data().nodes().stream()
              .map(node -> Document.builder()
                  .text(node.text())
                  .metadata(node.metadata())
                  .score(node.score())
                  .build())
              .toList();
      }

      @JsonIgnoreProperties(ignoreUnknown = true)
      public record RetrievalResponse(boolean success, RetrievalData data,
                                      @JsonProperty("request_id") String requestId) {}

      @JsonIgnoreProperties(ignoreUnknown = true)
      public record RetrievalData(List<RetrievalNode> nodes) {}

      @JsonIgnoreProperties(ignoreUnknown = true)
      public record RetrievalNode(String text, Map<String, Object> metadata, Double score) {}
  }
  ```
</Accordion>

**2. 接入现有问答流程。** 在前文 `CloudRagService` 中，将以下 `import` 添加到文件顶部；在构造方法中，将 `retrieverOptions` 和 `DashScopeDocumentRetriever` 的初始化代码替换为其余代码。保留 `orGroup`、提示词模板和 `ChatClient` 的初始化代码，不重复注册另一个 Service：

```java expandable
import org.springframework.http.client.JdkClientHttpRequestFactory;
import org.springframework.web.client.RestClient;
import java.net.http.HttpClient;
import java.time.Duration;

JdkClientHttpRequestFactory requestFactory = new JdkClientHttpRequestFactory(
    HttpClient.newBuilder().connectTimeout(Duration.ofSeconds(10)).build()
);
requestFactory.setReadTimeout(Duration.ofSeconds(30));
RestClient client = RestClient.builder()
    .baseUrl("https://" + System.getenv("AI_DASHSCOPE_WORKSPACE_ID")
        + ".cn-beijing.maas.aliyuncs.com")
    .defaultHeaders(headers -> headers.setBearerAuth(System.getenv("AI_DASHSCOPE_API_KEY")))
    .requestFactory(requestFactory)
    .build();
DocumentRetriever retriever = new RagApiDocumentRetriever(
    client,
    System.getenv("BAILIAN_INDEX_ID"),
    List.of(orGroup)
);
```

HTTP 客户端在服务初始化时创建并复用。示例的连接超时为 10 秒，读取超时为 30 秒，请按业务要求调整。检索请求使用 Bearer API Key 和 JSON 请求体，嵌套 Map/List 会作为 JSON 对象和数组发送，不要先将 `_conditions` 序列化为字符串。

成功响应中的 `data.nodes` 会转换为包含文本、元数据和相似度分数的 `Document`。空列表表示没有命中切片；HTTP 错误由 `RestClient.retrieve()` 抛出，业务失败或响应缺少切片列表也会抛出异常，不会被当作空结果。调用前文 Controller 时，仍通过原始问题和检索上下文生成回答。排查接口问题时保留响应中的 `request_id`，不要记录 API Key。

**响应式应用：** `RestClient` 是同步客户端，上述流程包含阻塞检索。需要端到端非阻塞处理时，使用 `WebClient` 调用同一 RAG API，保持相同的鉴权和 JSON 请求体，通过 `Mono`/`Flux` 组合检索与模型调用；不要在响应式事件循环线程中调用 `.block()`。`WebClient` 并不会将 RAG Retrieve 接口变为 SSE 接口，检索响应仍是一次性 JSON。

## 了解更多 <span id="39372403d7k3x" />

- [Spring AI Alibaba](https://java2ai.com/?spm=4347728f.638c0b20.0.0.23f87982NTcSMy)：提供文档教程、实战博客和开发者社区，帮助您快速开发 Java 生成式 AI 应用。
- [创建和使用知识库](/zh/model-studio/rag-knowledge-base)：深入了解阿里云百炼知识库的核心功能与最佳实践。
