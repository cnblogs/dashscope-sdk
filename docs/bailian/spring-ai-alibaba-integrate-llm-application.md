> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 使用Spring AI Alibaba集成阿里云百炼大模型应用

<meta property="og:title" content="使用Spring AI Alibaba集成阿里云百炼大模型应用" />

阿里云百炼推出的大模型应用，有效解决了大模型在处理私有领域问题、获取最新信息、遵循固定流程以及自动规划复杂项目等方面的局限，显著拓展了其应用范围。

本文介绍如何通过[Spring AI Alibaba](https://java2ai.com/?spm=4347728f.638c0b20.0.0.23f87982NTcSMy)集成阿里云百炼大模型应用（仅支持集成[智能体应用（Agent 1.0）](/zh/model-studio/single-agent-application)和[工作流应用](/zh/model-studio/workflow-application)）。

## 前期准备 <span id="48a4bafd73wfc" />

1. [获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)（推荐使用“`DASHSCOPE_API_KEY`”作为环境变量名），避免因硬编码导致的泄露风险。
2. 创建阿里云百炼大模型应用。

   创建以下任一类型应用并获取应用ID，并将应用ID配置到环境变量（推荐使用“`APP_ID`”作为环境变量名）：

   - [创建智能体应用](/zh/model-studio/single-agent-application#a9de1b3f7enzm)
   - [创建工作流应用](/zh/model-studio/workflow-application)
3. 如果是在子业务空间创建的阿里云百炼大模型应用，需要[获取业务空间 ID](/zh/model-studio/use-workspace)，并将其配置到环境变量（推荐使用“`WORKSPACE_ID`”作为环境变量名）。

## 操作流程 <span id="505e0d5ed4ajc" />

### 1. 初始化Spring Boot工程 <span id="26239bc5e4is9" />

#### 环境要求 <span id="40aceec8a0hzq" />

- Spring Boot 3.x
- JDK 17 或更高版本

您可以通过以下两种方式初始化工程：

1. <strong>下载完整的示例工程快速上手（推荐）</strong>

   完整示例工程请参见[bailian-agent.zip](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250619/dmxqoh/bailian-agent.zip)。下载代码到本地后，跳转到步骤【[3. 配置参数](/zh/model-studio/spring-ai-alibaba-integrate-llm-application#463fff19fcdr8)】完成操作，然后执行步骤【[5. 启动Spring Boot工程并测试](/zh/model-studio/spring-ai-alibaba-integrate-llm-application#87c3be85cdyhb)】。
2. <strong>从零开始搭建基础工程</strong>

   可参见[https://start.spring.io/](https://start.spring.io/)或者使用Intellij IDE等快速创建一个空的Spring Boot项目，然后按后续步骤依次执行。

### 2. 添加依赖 <span id="e34e43903eeux" />

在`pom.xml`中添加Spring AI Alibaba等相关依赖：

```xml expandable
<dependencies>
    <dependency>
        <groupId>com.alibaba.cloud.ai</groupId>
        <artifactId>spring-ai-alibaba-starter-dashscope</artifactId>
        <version>1.0.0.2</version>
    </dependency>

    <dependency>
        <groupId>org.springframework.boot</groupId>
        <artifactId>spring-boot-starter-web</artifactId>
        <exclusions>
            <exclusion>
                <groupId>org.springframework.boot</groupId>
                <artifactId>spring-boot-starter-logging</artifactId>
            </exclusion>
        </exclusions>
        <version>3.4.0</version>
    </dependency>
    <dependency>
        <groupId>org.springframework.boot</groupId>
        <artifactId>spring-boot-starter-log4j2</artifactId>
        <version>3.4.0</version>
    </dependency>

    <dependency>
        <groupId>org.apache.commons</groupId>
        <artifactId>commons-lang3</artifactId>
        <version>3.14.0</version>
    </dependency>
</dependencies>
```

### 3. 配置参数 <span id="463fff19fcdr8" />

在`application.yml`中配置阿里云百炼大模型应用ID、阿里云百炼API Key和业务空间ID（仅在子业务空间创建百炼大模型应用时需要）。

```yaml
spring:
  ai:
    dashscope:
      agent:
        app-id: ${APP_ID} # 大模型应用ID，如未配置环境变量，请在这里替换为实际的值
      api-key: ${DASHSCOPE_API_KEY} # 百炼API Key，如未配置环境变量，请在这里替换为实际的值
      #workspace-id: ${WORKSPACE_ID} # 业务空间ID，可选，未配置时使用主账号空间
# 如端口冲突，可自定义新的端口
#server:
#  port: 9000
```

### 4. 调用阿里云百炼大模型应用 <span id="00c906dca5ikt" />

Spring AI Alibaba使用`DashScopeAgent`调用阿里云百炼大模型应用。

<CodeGroup>
  ```java 非流式调用 expandable
  import com.alibaba.cloud.ai.dashscope.agent.DashScopeAgent;
  import com.alibaba.cloud.ai.dashscope.agent.DashScopeAgentOptions;
  import com.alibaba.cloud.ai.dashscope.api.DashScopeAgentApi;

  import org.springframework.ai.chat.messages.AssistantMessage;
  import org.springframework.ai.chat.model.ChatResponse;
  import org.springframework.ai.chat.prompt.Prompt;
  import org.springframework.beans.factory.annotation.Value;
  import org.springframework.web.bind.annotation.GetMapping;
  import org.springframework.web.bind.annotation.RequestMapping;
  import org.springframework.web.bind.annotation.RequestParam;
  import org.springframework.web.bind.annotation.RestController;

  import org.slf4j.Logger;
  import org.slf4j.LoggerFactory;

  import java.util.List;

  @RestController
  @RequestMapping("/ai")
  public class BailianAgentController {
      private static final Logger logger = LoggerFactory.getLogger(BailianAgentController.class);

      private DashScopeAgent agent;

      @Value("${spring.ai.dashscope.agent.app-id}")
      private String appId;

      public BailianAgentController(DashScopeAgentApi dashscopeAgentApi) {
          this.agent = new DashScopeAgent(dashscopeAgentApi);
      }

      @GetMapping("/bailian/agent/call")
      public String call(@RequestParam(value = "message",
              defaultValue = "如何使用SDK快速调用阿里云百炼的应用?") String message) {
          ChatResponse response = agent.call(new Prompt(message, DashScopeAgentOptions.builder().withAppId(appId).build()));
          if (response == null || response.getResult() == null) {
              logger.error("chat response is null");
              return "chat response is null";
          }

          AssistantMessage app_output = response.getResult().getOutput();
          String content = app_output.getText();

          DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput output = (DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput) app_output.getMetadata().get("output");
          List<DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput.DashScopeAgentResponseOutputDocReference> docReferences = output.docReferences();
          List<DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput.DashScopeAgentResponseOutputThoughts> thoughts = output.thoughts();

          logger.info("content:\n{}\n\n", content);

          if (docReferences != null && !docReferences.isEmpty()) {
              for (DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput.DashScopeAgentResponseOutputDocReference docReference : docReferences) {
                  logger.info("{}\n\n", docReference);
              }
          }

          if (thoughts != null && !thoughts.isEmpty()) {
              for (DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput.DashScopeAgentResponseOutputThoughts thought : thoughts) {
                  logger.info("{}\n\n", thought);
              }
          }

          return content;
      }
  }
  ```

  ```java 流式调用 expandable
  import java.util.List;

  import com.alibaba.cloud.ai.dashscope.agent.DashScopeAgent;
  import com.alibaba.cloud.ai.dashscope.agent.DashScopeAgentOptions;
  import com.alibaba.cloud.ai.dashscope.api.DashScopeAgentApi;
  import org.slf4j.Logger;
  import org.slf4j.LoggerFactory;
  import reactor.core.publisher.Flux;

  import org.springframework.ai.chat.messages.AssistantMessage;
  import org.springframework.ai.chat.prompt.Prompt;
  import org.springframework.beans.factory.annotation.Value;
  import org.springframework.web.bind.annotation.GetMapping;
  import org.springframework.web.bind.annotation.RequestMapping;
  import org.springframework.web.bind.annotation.RequestParam;
  import org.springframework.web.bind.annotation.RestController;

  @RestController
  @RequestMapping("/ai")
  public class BailianAgentStreamController {
      private static final Logger logger = LoggerFactory.getLogger(BailianAgentStreamController.class);
      private DashScopeAgent agent;

      @Value("${spring.ai.dashscope.agent.app-id}")
      private String appId;

      public BailianAgentStreamController(DashScopeAgentApi dashscopeAgentApi) {
          this.agent = new DashScopeAgent(dashscopeAgentApi,
                  DashScopeAgentOptions.builder()
                          .withSessionId("current_session_id")
                          .withIncrementalOutput(true)
                          .withHasThoughts(true)
                          .build());
      }

      @GetMapping(value="/bailian/agent/stream", produces="text/event-stream")
      public Flux<String> stream(@RequestParam(value = "message",
              defaultValue = "你好，请问你的知识库文档主要是关于什么内容的?") String message) {
          return agent.stream(new Prompt(message, DashScopeAgentOptions.builder().withAppId(appId).build())).map(response -> {
              if (response == null || response.getResult() == null) {
                  logger.error("chat response is null");
                  return "chat response is null";
              }

              AssistantMessage app_output = response.getResult().getOutput();
              String content = app_output.getText();

              DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput output = (DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput) app_output.getMetadata().get("output");
              List<DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput.DashScopeAgentResponseOutputDocReference> docReferences = output.docReferences();
              List<DashScopeAgentApi.DashScopeAgentResponse.DashScopeAgentResponseOutput.DashScopeAgentResponseOutputThoughts> thoughts = output.thoughts();

              logger.info("content:\n{}\n\n", content);

              return content;
          });
      }
  }
  ```
</CodeGroup>

### 5. 启动Spring Boot工程并测试 <span id="87c3be85cdyhb" />

运行如下代码启动Spring Boot工程并测试（例如使用Postman进行测试）。

```java
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class BailianAgentApplication {
    public static void main(String[] args) {
        SpringApplication.run(BailianAgentApplication.class, args);
    }
}
```

<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0157503471/p933131.png" alt="image" width={876} />

## 计费说明 <span id="ef85c7ac23t5h" />

百炼应用本身不收取费用，但通过应用调用模型时会产生模型推理（调用）的相关费用。有关模型推理（调用）费用详情，请参见[计费项](/zh/model-studio/billing-for-model-studio)。

## 错误码 <span id="97e145aeabbwf" />

通用错误码信息请参见[错误码](/zh/model-studio/error-code)。

## 了解更多 <span id="39372403d7k3x" />

- [Spring AI Alibaba](https://java2ai.com/?spm=4347728f.638c0b20.0.0.23f87982NTcSMy)：提供文档教程、实战博客和开发者社区，帮助您快速开发Java生成式AI应用。
- [工作流与旧版智能体应用 API应用 DashScope API 参考](/zh/model-studio/agent-and-workflow-application-api-reference)：提供应用调用相关的接口说明与调用示例。
- [spring-ai-alibaba-examples](https://github.com/springaialibaba/spring-ai-alibaba-examples/tree/main/spring-ai-alibaba-rag-example?spm=4347728f.4b30b334.0.0.63ed66f4GcFkdj)：更多Spring AI Alibaba示例代码的Github仓库地址。
