> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Assistant API 快速入门（下线中）

> Assistant API 为您提供了一套 Assistant 的开发工具，帮助您方便地管理对话消息，以及调用工具。本文以从零开始构建绘画智能助手为例，帮助您快速掌握 Assistant API 的基础编码方法。

<meta property="og:title" content="Assistant API 快速入门（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

## 典型流程 <span id="0cb8a50d90wvz" />

构建智能体应用（Assistant）的典型流程如下：

1. 创建 Assistant：创建智能体时，您需要选择模型、输入指令并添加实用工具，如代码解释器、夸克搜索和函数调用。
2. 创建 Thread：当用户发起对话时，创建一个会话线程来追踪连续的对话历史。
3. 发送 Message 到 Thread：将用户发送的消息添加到对话中。
4. 发起 Run：在会话线程上运行智能体，它会解析消息，调用相应的工具或服务，生成响应并返回给您。

## 示例场景 <span id="c58e40c94dt1o" />

文本生成模型自身不具备生成图像的功能，一般需要通过特定的文生图模型，可以将文本转化为图像。借助 Assistant API 创建的智能体应用，能够自动优化用户提供的描述词，通过互联网搜索工具丰富细节，最终调用文生图工具生成高质量图像。例如，为了生成一幅栩栩如生的宠物猫图像，您只需提供基础描述，绘图助手将自动完善提示词，并直接输入至文生图工具中，高效完成图像绘制任务。

## 操作步骤 <span id="e065b979ca8hj" />

以下是在非流式输出模式下，以 Python 为例的详细流程指引。本文末尾还介绍了流式和非流式输出的 Python 及 Java SDK [完整代码](/zh/model-studio/quick-start-of-assistant-api#4bc5dea98cvv8)。

<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6468924271/p836947.png" alt="image" width={532} />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 第一步：准备开发环境 <span id="56d3740afd2n9" />

        - 申请插件使用权限：需先申请图片生成插件和夸克搜索插件的使用权限。请访问百炼控制台的[<strong>插件</strong>](https://bailian.console.aliyun.com/#/plugin-market)页面，点击相应卡片的<strong>申请插件</strong>按钮。
        - Python 解释器：Assistant API 要求 Python 解释器版本高于 3.9，您可以检查 Python 版本。如需安装特定版本的 Python 解释器，请参考[Download Python](https://www.python.org/downloads/)。
        - DashScope SDK：推荐使用最新版本的 DashScope SDK，您可以使用右侧命令检查版本。如需安装特定版本的 DashScope SDK，请使用 pip 完成安装。
        - API-KEY：Assistant API 要求传入阿里云百炼 API-KEY。您可以在这里[获取与配置 API Key](/zh/model-studio/get-api-key)。首次使用 DashScope SDK 时，建议您[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)，以免暴露此类敏感信息。
      </td>

      <td>
        ```bash
        # 检查 Python 解释器版本
        python --version
        ```

        ```bash
        # 检查 DashScope SDK
        pip list | grep dashscope
        ```

        ```bash
        # 安装 DashScope SDK 版本 1.17.0
        pip install dashscope==1.17.0
        ```
      </td>
    </tr>

    <tr>
      <td>
        ### 第二步：创建 Assistant <span id="1dc3bf9a67n3e" />

        导入 Dashscope SDK 后，您需使用 Assistant 类的 create 方法创建 Assistant 智能体。这一过程涉及以下关键参数的设定：

        - `model`大语言模型名，用于为智能体配置大模型
        - `name`智能体名称，用于区别智能体的名称
        - `description`智能体的功能描述
        - `instructions`由自然语言构成的指令，用于定义智能体的角色和任务
        - `tools`为智能体配置的工具列表

        具体到我们的案例，目标是构建一个专注于绘画的 Assistant。基于文生图工具对语言理解能力的较高要求，我们选用了千问-Max作为文本推理模型，以此强化 Assistant 的语义理解和文本生成能力。

        智能体的配置细节，包括其名称、功能描述及指令，均在随附的代码片段中有明确展示。

        为了丰富智能体的功能性和实用性，我们集成了两个官方预置插件：

        - <strong>夸克搜索</strong>，旨在帮助智能体获取关于宠物猫的广泛文本信息；
        - <strong>图片生成</strong>，确保智能体能够根据接收到的文字描述，自动生成相应的图像内容。

        创建 Assistant 没有数量限制，但频繁调用单一模型可能会触发[限流](/zh/model-studio/rate-limit)，建议根据 Assistant 的使用场景配置不同的模型。

        相关使用方法请参考[Assistants API](/zh/model-studio/assistant)。
      </td>

      <td>
        ```python
        import dashscope

        # 使用 Qwen-Max 创建一个新的绘画助手
        painting_assistant = dashscope.Assistants.create(
            model='qwen-max',  # 使用 Qwen-Max 模型以增强理解能力。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
            name='Art Maestro 艺术大师',  # 助手名称为“Art Maestro（艺术大师）”
            description='一个专注于绘画和艺术知识的AI助手。',
            instructions='''你是一个专家级的绘画助手。请提供关于绘画技巧、艺术史和创意指导的详细信息。
            使用 夸克搜索 查找关于艺术主题的准确信息，并在需要时利用图像生成工具创建视觉示例。''',
            tools=[
                {
                    'type': 'quark_search',  # 用于搜索广泛信息的工具
                    'description': '使用此工具查找关于绘画技巧、艺术史和艺术家的详细信息。'
                },
                {
                    'type': 'text_to_image',  # 用于根据描述生成图像的工具
                    'description': '使用此工具创建绘画风格、技巧或艺术概念的视觉示例。'
                }
            ]
        )

        # 打印助手的ID以确认创建成功
        print(f"绘画助手 'Art Maestro' 创建成功，ID：{painting_assistant.id}")
        ```
      </td>
    </tr>

    <tr>
      <td>
        ### 第三步：创建 Thread <span id="6f07ec89d7829" />

        Thread（线程）是 Assistant API 中的一个关键概念，它代表了一个持续的对话上下文。

        Thread 允许您在用户启动新对话时创建一个会话管理线程。Assistant 可以通过 Thread 理解整个对话的上下文，从而提供更加连贯和相关的回应。

        建议您：

        - 为每个新用户或新的对话主题创建一个新的Thread。
        - 在需要保持上下文的情况下，继续使用同一个Thread。
        - 当对话主题发生显著变化时，考虑创建新的Thread以避免上下文混淆。

        在绘画助手的场景中，Thread 可以跟踪用户的初始请求、Assistant 的初步建议、用户的反馈，以及最终的绘画结果，形成一个完整的创作过程。这确保了整个创作过程的连贯性和可追溯性。

        相关使用方法请参考[Threads API](/zh/model-studio/thread)。
      </td>

      <td>
        ```python
        from http import HTTPStatus
        import dashscope

        # 创建一个新的空线程
        thread = dashscope.Threads.create()

        # 检查线程创建是否成功
        if thread.status_code == HTTPStatus.OK:
            print(f"线程创建成功。线程 ID：{thread.id}")
            print("现在你可以开始与AI助手进行绘画对话了。")
        else:
            print(f"线程创建失败。状态码：{thread.status_code}")
            print(f"错误码：{thread.code}")
            print(f"错误信息：{thread.message}")

        # 注意：这个空线程现在可以用于保持你绘画项目讨论的上下文，
        # 包括任何关于布偶猫或其他绘画主题的未来消息。
        ```
      </td>
    </tr>

    <tr>
      <td>
        ### 第四步：添加 Message 到 Thread <span id="d945056e76gpk" />

        您输入的内容将通过 Message 对象传递。 Assistant API 支持向单一 Thread 发送一个或多个消息。创建 Message 时，您需要考虑以下参数：

        - Thread 唯一编号`thread_id`
        - 消息内容`content`

        尽管 Thread 可接收的 Token 数量无硬性限制，但实际传递给大模型的 Token 数量需符合该模型的最大输入长度限制，具体参照各千问系列模型的官方文档中关于上下文长度的说明。

        在我们的场景中，您将通过 Message 在 Thread 中发出第一条消息：“请帮我画一只布偶猫的图片”。您需要创建一个 Message 类，详细参数设置均在随附的代码片段中。

        相关使用方法请参考[Messages](/zh/model-studio/message)。

        <Note>
          Messages.create()方法执行后会自动将消息加入线程并触发后台处理，相当于同时完成消息创建和发送两个操作，这是 API 的预设行为。
        </Note>
      </td>

      <td>
        ```python
        from http import HTTPStatus
        import dashscope

        # 创建一条消息，告诉助手需要做什么？
        message = dashscope.Messages.create(thread.id, content='请帮我画一幅布偶猫的画。')

        # 检查消息创建是否成功
        if message.status_code == HTTPStatus.OK:
            print('消息创建成功！消息 ID：%s' % message.id)
        else:
            print('消息创建失败。状态码：%s，错误码：%s，错误信息：%s' % (message.status_code, message.code, message.message))
        ```
      </td>
    </tr>

    <tr>
      <td>
        ### 第五步：创建 Run 并执行 <span id="26ba28ea7415x" />

        用户将消息分配至特定 Thread 后，可以通过启动一个运行（Run）来激活预设的助手（Assistant）。该助手会以线程内的所有消息为背景，利用指定的模型和可用的插件，智能地回应用户的问题，并将生成的答案插入到线程的消息序列中。

        在本场景中，您需执行以下步骤：

        1. 初始化一个运行对象，以驱动绘画助手，传递线程ID（thread.id）和助手ID（assistant.id）。
        2. 使用运行对象的等待方法（Run.wait），直至执行完毕。
        3. 通过消息列表方法（Messages.list），检索助手绘制的宠物猫图片。

        这一系列操作确保了助手从接收问题到输出结果的自动化处理流程。

        相关使用方法请参考[Runs API](/zh/model-studio/runs)。

        <Note>
          考虑到可能会有大量用户同时使用模型，导致处理时间延长，建议等候状态显示为"complete"后再执行下一步操作，以确保流程顺畅。
        </Note>
      </td>

      <td>
        ```python
        from http import HTTPStatus
        import json
        import dashscope

        # 创建一个新的运行以执行消息
        run = dashscope.Runs.create(thread.id, assistant_id=painting_assistant.id)
        if run.status_code != HTTPStatus.OK:
            print('创建助手失败，状态码：%s，错误码：%s，错误信息：%s' % (run.status_code, run.code, run.message))
        else:
            print('创建助手成功，ID：%s' % run.id)

        # 等待运行完成或需要操作
        run = dashscope.Runs.wait(run.id, thread_id=thread.id)
        if run.status_code != HTTPStatus.OK:
            print('获取运行状态失败，状态码：%s，错误码：%s，错误信息：%s' % (run.status_code, run.code, run.message))
        else:
            print(run)

        # 获取线程消息，以获取运行输出
        msgs = dashscope.Messages.list(thread.id)
        if msgs.status_code != HTTPStatus.OK:
            print('获取消息失败，状态码：%s，错误码：%s，错误信息：%s' % (msgs.status_code, msgs.code, msgs.message))
        else:
            print(json.dumps(msgs, default=lambda o: o.__dict__, sort_keys=True, indent=4))
        ```
      </td>
    </tr>
  </tbody>
</table>

## 完整代码 <span id="4bc5dea98cvv8" />

<Tabs>
  <Tab title="非流式输出">
    <CodeGroup dropdown>
      ```python expandable
      import dashscope
      from http import HTTPStatus
      import json

      def check_status(component, operation):
          if component.status_code == HTTPStatus.OK:
              print(f"{operation} 成功。")
              return True
          else:
              print(f"{operation} 失败。状态码：{component.status_code}，错误码：{component.code}，错误信息：{component.message}")
              return False

      # 1. 创建绘画助手
      painting_assistant = dashscope.Assistants.create(
          model='qwen-max',   # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
          name='Art Maestro',
          description='用于绘画和艺术知识的AI助手',
          instructions='''提供绘画技巧、艺术史和创意指导的信息。
          使用工具进行研究和生成图像。''',
          tools=[
              {'type': 'quark_search', 'description': '用于研究艺术主题'},
              {'type': 'text_to_image', 'description': '用于创建视觉示例'}
          ]
      )

      if not check_status(painting_assistant, "助手创建"):
          exit()

      # 2. 创建一个新线程
      thread = dashscope.Threads.create()

      if not check_status(thread, "线程创建"):
          exit()

      # 3. 向线程发送消息
      message = dashscope.Messages.create(thread.id, content='请帮我画一幅布偶猫的画。')

      if not check_status(message, "消息创建"):
          exit()

      # 4. 在线程上运行助手
      run = dashscope.Runs.create(thread.id, assistant_id=painting_assistant.id)

      if not check_status(run, "运行创建"):
          exit()

      # 5. 等待运行完成
      print("等待助手处理请求...")
      run = dashscope.Runs.wait(run.id, thread_id=thread.id)

      if check_status(run, "运行完成"):
          print(f"运行完成，状态：{run.status}")
      else:
          print("运行未完成。")
          exit()

      # 6. 检索并显示助手的响应
      messages = dashscope.Messages.list(thread.id)

      if check_status(messages, "消息检索"):
          if messages.data:
              # 显示最后一条消息的内容（助手的响应）
              last_message = messages.data[0]
              print("\n助手的回应：")
              print(json.dumps(last_message, ensure_ascii=False, default=lambda o: o.__dict__, sort_keys=True, indent=4))
          else:
              print("在线程中未找到消息。")
      else:
          print("未能检索到助手的响应。")

      # 提示: 这段代码创建了一个绘画助手，开始了一段关于如何绘制布偶猫的对话，
      # 并展示了助手的回答。
      ```

      ```java expandable
      package com.example;
      import java.util.Arrays;

      import com.alibaba.dashscope.assistants.Assistant;
      import com.alibaba.dashscope.assistants.AssistantParam;
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.common.GeneralListParam;
      import com.alibaba.dashscope.common.ListResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.AssistantThread;
      import com.alibaba.dashscope.threads.ThreadParam;
      import com.alibaba.dashscope.threads.Threads;
      import com.alibaba.dashscope.threads.messages.Messages;
      import com.alibaba.dashscope.threads.messages.TextMessageParam;
      import com.alibaba.dashscope.threads.messages.ThreadMessage;
      import com.alibaba.dashscope.threads.runs.Run;
      import com.alibaba.dashscope.threads.runs.RunParam;
      import com.alibaba.dashscope.threads.runs.Runs;
      import com.alibaba.dashscope.tools.T2Image.Text2Image;
      import com.alibaba.dashscope.tools.search.ToolQuarkSearch;

      public class PaintingAssistant {
          private static boolean checkStatus(Object response, String operation) {
              if (response != null) {
                  System.out.println(operation + " 成功。");
                  return true;
              } else {
                  System.out.println(operation + " 失败。");
                  return false;
              }
          }

          public static void main(String[] args) {
              try {
                  // 1. 创建绘画助手
                  Assistants assistants = new Assistants();
                  AssistantParam assistantParam = AssistantParam.builder()
                      // 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      .model("qwen-max")
                      .name("Art Maestro")
                      .description("用于绘画和艺术知识的AI助手")
                      .instructions("提供绘画技巧、艺术史和创意指导的信息。使用工具进行研究和生成图像。")
                      .tools(Arrays.asList(ToolQuarkSearch.builder().build(),Text2Image.builder().build()))
                      .build();

                  Assistant paintingAssistant = assistants.create(assistantParam);
                  if (!checkStatus(paintingAssistant, "助手创建")) {
                      System.exit(1);
                  }

                  // 2. 创建一个新线程
                  Threads threads = new Threads();
                  AssistantThread thread = threads.create(ThreadParam.builder().build());
                  if (!checkStatus(thread, "线程创建")) {
                      System.exit(1);
                  }

                  // 3. 创建并自动发送消息到指定线程
                  Messages messages = new Messages();
                  ThreadMessage message = messages.create(thread.getId(),
                      TextMessageParam.builder()
                          .role("user")
                          .content("请帮我画一幅布偶猫的画。")
                          .build());
                  if (!checkStatus(message, "消息创建")) {
                      System.exit(1);
                  }

                  // 4. 在线程上运行助手
                  Runs runs = new Runs();
                  RunParam runParam = RunParam.builder().assistantId(paintingAssistant.getId()).build();
                  Run run = runs.create(thread.getId(), runParam);
                  if (!checkStatus(run, "运行创建")) {
                      System.exit(1);
                  }

                  // 5. 等待运行完成
                  System.out.println("等待助手处理请求...");
                  while (true) {
                      if (run.getStatus().equals(Run.Status.COMPLETED) ||
                          run.getStatus().equals(Run.Status.FAILED) ||
                          run.getStatus().equals(Run.Status.CANCELLED) ||
                          run.getStatus().equals(Run.Status.REQUIRES_ACTION) ||
                          run.getStatus().equals(Run.Status.EXPIRED)) {
                          break;
                      }
                      Thread.sleep(1000);
                      run = runs.retrieve(thread.getId(), run.getId());
                  }

                  if (checkStatus(run, "运行完成")) {
                      System.out.println("运行完成，状态：" + run.getStatus());
                  } else {
                      System.out.println("运行未完成。");
                      System.exit(1);
                  }

                  // 6. 检索并显示助手的响应
                  ListResult<ThreadMessage> messagesList = messages.list(thread.getId(), GeneralListParam.builder().build());
                  if (checkStatus(messagesList, "消息检索")) {
                      if (!messagesList.getData().isEmpty()) {
                          // 显示最后一条消息（助手的响应）
                          ThreadMessage lastMessage = messagesList.getData().get(0);
                          System.out.println("\n助手的回应：");
                          System.out.println(lastMessage.getContent());
                      } else {
                          System.out.println("在线程中未找到消息。");
                      }
                  } else {
                      System.out.println("未能检索到助手的响应。");
                  }

              } catch (ApiException | NoApiKeyException | InputRequiredException | InvalidateParameter | InterruptedException e) {
                  e.printStackTrace();
              }
          }
      }
      ```
    </CodeGroup>
  </Tab>

  <Tab title="流式输出">
    Java SDK 暂不支持流式调用图片生成工具。

    <CodeGroup dropdown>
      ```python expandable
      import dashscope
      from http import HTTPStatus
      import json
      import sys

      def check_status(response, operation):
          if response.status_code == HTTPStatus.OK:
              print(f"{operation} 成功。")
              return True
          else:
              print(f"{operation} 失败。状态码：{response.status_code}，错误码：{response.code}，错误信息：{response.message}")
              sys.exit(response.status_code)

      # 1. 创建绘画助手
      def create_painting_assistant():
          return dashscope.Assistants.create(
              model='qwen-max',  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
              name='Art Maestro',
              description='AI助手，用于绘画和艺术知识',
              instructions='''提供绘画技巧、艺术史和创意指导的信息。
              使用工具进行研究和生成图像。''',
              tools=[
                  {'type': 'quark_search', 'description': '用于研究艺术主题'},
                  {'type': 'text_to_image', 'description': '用于创建视觉示例'}
              ]
          )

      if __name__ == '__main__':
          # 创建绘画助手
          painting_assistant = create_painting_assistant()
          print(painting_assistant)
          check_status(painting_assistant, "助手创建")

          # 创建一个带有初始消息的新线程
          thread = dashscope.Threads.create(
              messages=[{
                  'role': 'user',
                  'content': '请帮我画一幅布偶猫的画。'
              }]
          )
          print(thread)
          check_status(thread, "线程创建")

          # 创建流式输出的运行
          run_iterator = dashscope.Runs.create(
              thread.id,
              assistant_id=painting_assistant.id,
              stream=True
          )

          # 迭代事件和消息
          print("处理请求中...")
          for event, msg in run_iterator:
              print(event)
              print(msg)

          # 检索并显示助手的响应
          messages = dashscope.Messages.list(thread.id)
          check_status(messages, "消息检索")

          print("\n助手的回应：")
          print(json.dumps(messages, ensure_ascii=False, default=lambda o: o.__dict__, sort_keys=True, indent=4))

      # 提示: 这个脚本创建了一个流式输出的绘画助手，开始一段关于绘制布偶猫的对话，
      # 并实时显示助手的回应。
      ```

      ```java expandable
      import java.util.Arrays;

      import com.alibaba.dashscope.assistants.Assistant;
      import com.alibaba.dashscope.assistants.AssistantParam;
      import com.alibaba.dashscope.assistants.Assistants;
      import com.alibaba.dashscope.common.GeneralListParam;
      import com.alibaba.dashscope.common.ListResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.InputRequiredException;
      import com.alibaba.dashscope.exception.InvalidateParameter;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.threads.AssistantThread;
      import com.alibaba.dashscope.threads.ThreadParam;
      import com.alibaba.dashscope.threads.Threads;
      import com.alibaba.dashscope.threads.messages.Messages;
      import com.alibaba.dashscope.threads.messages.TextMessageParam;
      import com.alibaba.dashscope.threads.messages.ThreadMessage;
      import com.alibaba.dashscope.threads.runs.AssistantStreamMessage;
      import com.alibaba.dashscope.threads.runs.Run;
      import com.alibaba.dashscope.threads.runs.RunParam;
      import com.alibaba.dashscope.threads.runs.Runs;
      import com.alibaba.dashscope.tools.T2Image.Text2Image;
      import com.alibaba.dashscope.tools.search.ToolQuarkSearch;

      import io.reactivex.Flowable;

      public class PaintingAssistant {
          private static boolean checkStatus(Object response, String operation) {
              if (response != null) {
                  System.out.println(operation + " 成功。");
                  return true;
              } else {
                  System.out.println(operation + " 失败。");
                  return false;
              }
          }

          private static Assistant createPaintingAssistant() throws ApiException, NoApiKeyException {
              Assistants assistants = new Assistants();
              AssistantParam assistantParam = AssistantParam.builder()
                  // 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  .model("qwen-max")
                  .name("Art Maestro")
                  .description("用于绘画和艺术知识的AI助手")
                  .instructions("提供绘画技巧、艺术史和创意指导的信息。使用工具进行研究和生成图像。")
                  .tools(Arrays.asList(ToolQuarkSearch.builder().build(), Text2Image.builder().build()))
                  .build();

              return assistants.create(assistantParam);
          }

          private static void runPaintingAssistant(String assistantId) throws ApiException, NoApiKeyException, InvalidateParameter, InputRequiredException, InterruptedException {
              Threads threads = new Threads();
              AssistantThread thread = threads.create(ThreadParam.builder().build());
              if (!checkStatus(thread, "线程创建")) {
                  System.exit(1);
              }
              // 创建并自动发送消息到指定线程
              Messages messages = new Messages();
              ThreadMessage message = messages.create(thread.getId(),
                  TextMessageParam.builder()
                      .role("user")
                      .content("请帮我画一幅布偶猫的画。")
                      .build());
              if (!checkStatus(message, "消息创建")) {
                  System.exit(1);
              }

              Runs runs = new Runs();
              RunParam runParam = RunParam.builder().assistantId(assistantId).stream(true).build();

              try {
                  System.out.println("尝试流式输出助手的响应...");
                  Flowable<AssistantStreamMessage> runFlowable = runs.createStream(thread.getId(), runParam);
                  runFlowable.blockingForEach(assistantStreamMessage -> {
                      System.out.println("事件：" + assistantStreamMessage.getEvent());
                      System.out.println("数据：" + assistantStreamMessage.getData());
                  });
              } catch (Exception e) {
                  System.out.println("流式输出失败，切换为非流式输出方法。");
                  e.printStackTrace();

                  // 切换到非流式输出方法
                  Run run = runs.create(thread.getId(), RunParam.builder().assistantId(assistantId).build());
                  while (true) {
                      if (run.getStatus().equals(Run.Status.COMPLETED) ||
                          run.getStatus().equals(Run.Status.FAILED) ||
                          run.getStatus().equals(Run.Status.CANCELLED) ||
                          run.getStatus().equals(Run.Status.REQUIRES_ACTION) ||
                          run.getStatus().equals(Run.Status.EXPIRED)) {
                          break;
                      }
                      Thread.sleep(1000);
                      run = runs.retrieve(thread.getId(), run.getId());
                  }
                  System.out.println("运行完成，状态：" + run.getStatus());
              }

              // 检索并显示助手的响应
              GeneralListParam listParam = GeneralListParam.builder().limit(100L).build();
              ListResult<ThreadMessage> messagesList = messages.list(thread.getId(), listParam);
              if (checkStatus(messagesList, "消息检索")) {
                  if (!messagesList.getData().isEmpty()) {
                      System.out.println("\n助手的回应：");
                      for (ThreadMessage threadMessage : messagesList.getData()) {
                          System.out.println(threadMessage.getContent());
                      }
                  } else {
                      System.out.println("在线程中未找到消息。");
                  }
              } else {
                  System.out.println("未能检索到助手的响应。");
              }
          }

          public static void main(String[] args) {
              try {
                  Assistant paintingAssistant = createPaintingAssistant();
                  if (!checkStatus(paintingAssistant, "助手创建")) {
                      System.exit(1);
                  }

                  runPaintingAssistant(paintingAssistant.getId());

              } catch (ApiException | NoApiKeyException | InputRequiredException | InvalidateParameter | InterruptedException e) {
                  System.out.println("运行绘画助手时发生错误：");
                  e.printStackTrace();
              }
          }
      }
      ```
    </CodeGroup>
  </Tab>
</Tabs>

## 后续操作 <span id="89fe12be0avb6" />

如需深入了解 Assistant API 各组件的基本操作、生命周期、数据管理及生产环境实践，请参阅[Assistant API 组件管理](/zh/model-studio/component-management)。

如需深入了解 Assistant API 各组件的参数解释，请参阅[Assistant API 开发参考](/zh/model-studio/assistantapi)。
