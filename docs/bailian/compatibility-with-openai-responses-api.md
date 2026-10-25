> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI Responses接口兼容

> 阿里云百炼的通义千问模型支持 OpenAI 兼容 Responses 接口。作为Chat Completions API的演进版本，Responses API能够以更简洁的方式提供智能体原生功能。

<meta property="og:title" content="OpenAI Responses接口兼容" />

<strong>相较于OpenAI Chat Completions API 的优势：</strong>

- <strong>内置工具</strong>：内置联网搜索、网页抓取、代码解释器、文搜图、图搜图等工具，可在处理复杂任务时获得更佳效果，详情参考[调用内置工具](/zh/model-studio/compatibility-with-openai-responses-api#11cd08d0ffnxw)。
- <strong>更灵活的输入</strong>：支持直接传入字符串作为模型输入，也兼容 Chat 格式的消息数组。
- <strong>简化上下文管理</strong>：通过传递上一轮响应的 `previous_response_id`，无需手动构建完整的消息历史数组。

输入输出参数说明请参考[OpenAI Responses API参考](/zh/model-studio/qwen-api-via-openai-responses)。

## 前提条件 <span id="780c8d0e61pln" />

您需要先[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。若通过 OpenAI SDK 进行调用，需要[安装SDK](/zh/model-studio/install-sdk)。

## 支持的模型 <span id="models-title" />

`qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、`qwen3.8-omni-flash`、`qwen3.7-max`、`qwen3.7-max-2026-05-20`、`qwen3.7-max-2026-06-08`、`qwen3.7-max-2026-05-17`、`qwen3.7-max-preview`、`qwen3.7-plus`、`qwen3.7-plus-2026-05-26`、`qwen3.6-plus`、`qwen3.6-plus-2026-04-02`、`qwen3.5-plus`、`qwen3.5-plus-2026-04-20`、`qwen3.5-plus-2026-02-15`、`qwen3.7-flash`、`qwen3.7-flash-2026-07-15`、`qwen3.6-flash`、`qwen3.6-flash-2026-04-16`、`qwen3.5-flash`、`qwen3.5-flash-2026-02-23`、`qwen3.6-35b-a3b`、`qwen3.5-397b-a17b`、`qwen3.5-122b-a10b`、`qwen3.5-27b`、`qwen3.5-35b-a3b`、`deepseek-v4.1-flash`、`deepseek-v4.1-flash-1017`、`deepseek-v4-pro`、`deepseek-v4-pro-0813`、`deepseek-v4-flash-0731`、`glm-5.3`、`glm-5.2`、`kimi-k3`

<Tip>
  非列表中阿里云百炼直供文本生成模型仅支持基础兼容能力，Agent 能力（内置工具等）受限。
</Tip>

使用 `qwen3.8-omni-flash` 处理音视频输入并生成文本回复，示例见[Omni 调用指南](/zh/model-studio/qwen-omni#qwen38-offline)。

## 服务地址 <span id="endpoints-title" />

<Tip>
  OpenAI 兼容接口 Responses API 的旧版路径 `/api/v2/apps/protocols/compatible-mode/v1/responses` 已经停止维护，将不再保证功能可用性，请尽快迁移至新版路径 `/compatible-mode/v1/responses`。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

<Tabs>
  <Tab title="华北2（北京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/responses`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/responses`
  </Tab>

  <Tab title="德国（法兰克福）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1/responses`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="日本（东京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1/responses`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="中国香港">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/compatible-mode/v1/responses`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

## 代码示例 <span id="examples-title" />

### 基础调用 <span id="example-basic-title" />

最简单的调用方式，发送一条消息并获取模型回复。

<CodeGroup dropdown>
  ```python Python
  import os
  from openai import OpenAI

  client = OpenAI(
      # 若没有配置环境变量，请用百炼 API Key 将下行替换为：api_key="sk-xxx"
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )

  response = client.responses.create(
      model="qwen3.8-max",
      input="你能做些什么？"
  )

  # 获取模型回复
  # print(response.model_dump_json())
  print(response.output_text)
  ```

  ```javascript Node.js
  import OpenAI from "openai";

  const openai = new OpenAI({
      // 若没有配置环境变量，请用百炼 API Key 将下行替换为：apiKey: "sk-xxx"
      apiKey: process.env.DASHSCOPE_API_KEY,
      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
  });

  async function main() {
      const response = await openai.responses.create({
          model: "qwen3.8-max",
          input: "你能做些什么？"
      });

      // 获取模型回复
      console.log(response.output_text);
  }

  main();
  ```

  ```bash curl
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "input": "你能做些什么？"
  }'
  ```
</CodeGroup>

<strong>响应示例</strong>

> 以下为API返回的完整响应。

```json expandable
{
    "created_at": 1771225825,
    "id": "0c842a11-c7d1-45da-b7ec-4e668c389xxx",
    "model": "qwen3.8-max",
    "object": "response",
    "output": [
        {
            "id": "msg_0bdb8ab9-f1de-4db6-82c8-6c1185b91xxx",
            "summary": [
                {
                    "text": "Thinking Process:\n\n1.  **Analyze the Request ...",
                    "type": "summary_text"
                }
            ],
            "type": "reasoning"
        },
        {
            "content": [
                {
                    "annotations": [],
                    "text": "你好！作为一个人工智能助手，我可以协助你完成多种任务。以下是我的一些主要能力：\n\n1.  **文字创作与编辑**\n    *   撰写邮件、文章、报告、故事或社交媒体文案。\n    *   润色、改写或总结现有的文本内容。\n\n2.  **编程与技术支持**\n    *   编写、调试或解释代码（支持多种编程语言，如 Python、JavaScript、C++ 等）。\n    *   提供技术概念的解释和解决方案建议。\n\n3.  **知识问答与学习**\n    *   回答各领域的问题（我的知识更新至 2026 年）。\n    *   协助学习新概念、制定学习计划或解答习题。\n\n4.  **语言翻译**\n    *   支持多种语言之间的互译，帮助你跨越语言障碍。\n\n5.  **数据分析与整理**\n    *   协助整理信息、提取关键点或进行逻辑分析。\n    *   帮助格式化数据或生成表格结构。\n\n6.  **创意与头脑风暴**\n    *   提供创意灵感、策划方案或建议。\n    *   陪你聊天，提供情感支持或生活建议。\n\n有什么具体需要我帮忙的吗？随时告诉我！",
                    "type": "output_text"
                }
            ],
            "id": "msg_c8bb3db1-d235-44e7-9704-55b584022xxx",
            "role": "assistant",
            "status": "completed",
            "type": "message"
        }
    ],
    "parallel_tool_calls": false,
    "status": "completed",
    "tool_choice": "auto",
    "tools": [],
    "usage": {
        "input_tokens": 49,
        "input_tokens_details": {
            "cached_tokens": 0
        },
        "output_tokens": 1384,
        "output_tokens_details": {
            "reasoning_tokens": 1110
        },
        "total_tokens": 1433,
        "x_details": [
            {
                "input_tokens": 49,
                "output_tokens": 1384,
                "output_tokens_details": {
                    "reasoning_tokens": 1110
                },
                "total_tokens": 1433,
                "x_billing_type": "response_api"
            }
        ]
    }
}
```

### 多轮对话 <span id="example-multiturn-title" />

通过 `previous_response_id` 参数自动关联上下文，无需手动构建消息历史，当前响应`id`有效期为7天。

> `previous_response_id` 应传入上一轮响应中的顶层 `id` （`resp_xxx`，UUID格式），而不是 `output` 数组内消息的 `id` （`msg_56c860c4-3ad8-4a96-8553-d2f94c259xxx`）。

<CodeGroup dropdown>
  ```python Python
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )

  # 第一轮对话
  response1 = client.responses.create(
      model="qwen3.8-max",
      input="我的名字是张三，请记住。"
  )
  print(f"第一轮回复: {response1.output_text}")

  # 第二轮对话 - 使用 previous_response_id 关联上下文，响应id有效期为7天
  response2 = client.responses.create(
      model="qwen3.8-max",
      input="你还记得我的名字吗？",
      previous_response_id=response1.id
  )
  print(f"第二轮回复: {response2.output_text}")
  ```

  ```javascript Node.js
  import OpenAI from "openai";

  const openai = new OpenAI({
      apiKey: process.env.DASHSCOPE_API_KEY,
      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
  });

  async function main() {
      // 第一轮对话
      const response1 = await openai.responses.create({
          model: "qwen3.8-max",
          input: "我的名字是张三，请记住。"
      });
      console.log(`第一轮回复: ${response1.output_text}`);

      // 第二轮对话 - 使用 previous_response_id 关联上下文，响应id有效期为7天
      const response2 = await openai.responses.create({
          model: "qwen3.8-max",
          input: "你还记得我的名字吗？",
          previous_response_id: response1.id
      });
      console.log(`第二轮回复: ${response2.output_text}`);
  }

  main();
  ```

  ```bash curl
  # 第一轮对话
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "input": "我的名字是张三，请记住。"
  }'

  # 第二轮对话 - 使用上一轮返回的 id 作为 previous_response_id
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "input": "你还记得我的名字吗？",
      "previous_response_id": "第一轮返回的响应id"
  }'
  ```
</CodeGroup>

<strong>第二轮对话响应示例</strong>

```json expandable
{
  "id": "f0dbb153-117f-9bbf-8176-5284b47f3xxx",
  "created_at": 1769169951.0,
  "model": "qwen3.8-max",
  "object": "response",
  "status": "completed",
  "output": [
    {
      "id": "msg_56c860c4-3ad8-4a96-8553-d2f94c259xxx",
      "type": "message",
      "role": "assistant",
      "status": "completed",
      "content": [
        {
          "type": "output_text",
          "text": "当然记得，你的名字是张三！",
          "annotations": []
        }
      ]
    }
  ],
  "usage": {
    "input_tokens": 73,
    "output_tokens": 10,
    "total_tokens": 83,
    "input_tokens_details": {
      "cached_tokens": 0
    },
    "output_tokens_details": {
      "reasoning_tokens": 0
    }
  }
}
```

<strong>说明：</strong>第二轮对话的 `input_tokens` 为 73，包含了第一轮的上下文，模型成功记住了名字"张三"。

### 深度思考 <span id="example-deep-thinking-title" />

通过 `reasoning.effort` 控制思考强度，支持的档位及默认值因模型而异，详见 [reasoning 参数说明](/zh/model-studio/qwen-api-via-openai-responses)。思考内容通过 `reasoning` 类型的输出项返回。

下方 `qwen3.8-max` 示例使用 `reasoning.effort`，该参数的优先级高于 `enable_thinking`；不支持通过 `thinking_budget` 控制最大思考长度。

<CodeGroup dropdown>
  ```python Python expandable
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )

  response = client.responses.create(
      model="qwen3.8-max",
      input="9.9和9.11谁大？",
      reasoning={"effort": "medium"}
  )

  # 处理输出
  for item in response.output:
      if item.type == "reasoning":
          print("=== 思考过程 ===")
          for summary in item.summary:
              print(summary.text)
      elif item.type == "message":
          print("\n=== 最终答案 ===")
          print(item.content[0].text)

  # 查看思考 Token 数
  print(f"\n思考 Token 数: {response.usage.output_tokens_details.reasoning_tokens}")
  ```

  ```javascript Node.js expandable
  import OpenAI from "openai";

  const openai = new OpenAI({
      apiKey: process.env.DASHSCOPE_API_KEY,
      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
  });

  async function main() {
      const response = await openai.responses.create({
          model: "qwen3.8-max",
          input: "9.9和9.11谁大？",
          reasoning: { effort: "medium" }
      });

      for (const item of response.output) {
          if (item.type === "reasoning") {
              console.log("=== 思考过程 ===");
              for (const summary of item.summary) {
                  console.log(summary.text);
              }
          } else if (item.type === "message") {
              console.log("\n=== 最终答案 ===");
              console.log(item.content[0].text);
          }
      }

      // 查看思考 Token 数
      console.log(`\n思考 Token 数: ${response.usage.output_tokens_details.reasoning_tokens}`);
  }

  main();
  ```

  ```bash curl
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "input": "9.9和9.11谁大？",
      "reasoning": {"effort": "medium"}
  }'
  ```
</CodeGroup>

<strong>响应示例</strong>

```json expandable
{
    "created_at": 1774498317,
    "id": "resp_xxx",
    "model": "qwen3.8-max",
    "object": "response",
    "output": [
        {
            "id": "msg_xxx",
            "summary": [
                {
                    "text": "Thinking Process:\n\n1.  **Analyze the Request:**\n    *   Question: \"9.9 和 9.11 谁大？\" (Which is larger, 9.9 or 9.11?)\n    *   Context: The user is asking a simple mathematical comparison question.\n    *   Current Date: Thursday, March 26, 2026 (provided in the system prompt).\n    *   Knowledge Cutoff: 2026 (provided in the system prompt).\n\n2.  **Evaluate the Numbers:**\n    *   Number A: 9.9\n    *   Number B: 9.11\n    *   These are decimal numbers.\n    *   Compare the integer part: Both are 9.\n    *   Compare the tenths place (first decimal digit):\n        *   9.9 has 9 in the tenths place.\n        *   9.11 has 1 in the tenths place.\n    *   Since 9 > 1, 9.9 is greater than 9.11.\n\n3.  **Consider Potential Ambiguities:**\n    *   Could this be version numbering? (e.g., software versions). In versioning, 9.11 is often \"newer\" or \"higher\" than 9.9. However, mathematically, 9.9 > 9.11.\n    *   Could this be dates? (September 9th vs September 11th). 11th is later.\n    *   Standard interpretation: Without context, decimal numbers are assumed to be mathematical values.\n    *   Common pitfall: Some people mistakenly treat decimals like whole numbers (where 11 > 9), leading them to think 9.11 > 9.9. This is a known cognitive bias or misconception in elementary math.\n    *   Decision: Provide the mathematical answer clearly, but perhaps acknowledge the versioning context if relevant (though usually, for this specific question, it's a math test). Given the simplicity, stick to the mathematical truth first.\n\n4.  **Formulate the Answer:**\n    *   Direct answer: 9.9 is larger.\n    *   Explanation: Compare place values. 9.9 = 9.90, 9.11 = 9.11. 90 hundredths > 11 hundredths.\n    *   Tone: Helpful, clear.\n\n5.  **Drafting the Response (Internal Monologue/Draft):**\n    *   Okay, mathematically 9.9 is bigger. 9.9 is 9 and 9 tenths. 9.11 is 9 and 11 hundredths (or 1 tenth and 1 hundredth). 9 tenths is more than 1 tenth.\n    *   So, 9.9 > 9.11.\n    *   I should answer in Chinese since the question is in Chinese.\n    *   \"9.9 更大。\" (9.9 is larger.)\n    *   Add a brief explanation to avoid confusion. \"因为 9.9 等于 9.90，而 9.90 大于 9.11。\" (Because 9.9 equals 9.90, and 9.90 is greater than 9.11.)\n\n6.  **Refining based on \"Version Number\" possibility:**\n    *   Sometimes this is a trick question about software versions. In semver, 9.11 > 9.9.\n    *   However, usually, when asked plainly \"which is larger\", it refers to numerical value.\n    *   I will provide the numerical answer as primary, maybe add a note about versions if it feels necessary. But keeping it simple is better. Most likely, this is testing the decimal comparison logic.\n    *   Let's stick to the mathematical value as the default.\n\n7.  **Final Polish:**\n    *   Answer: 9.9 大。\n    *   Reasoning: 比较小数时，先看整数部分，再看十分位。9.9 的十分位是 9，9.11 的十分位是 1。9 > 1，所以 9.9 > 9.11。\n\n8.  **Output Generation:** (Matching the thought process)\n    *   State clearly which is larger.\n    *   Explain why.\n\n    *Self-Correction on Date/Time:* The system prompt mentions the current date is 2026. This doesn't affect the math question, but I should ensure I don't reference anything post-2026 if I were adding context (not needed here).\n\n    *Final Decision:* Just answer the math question directly.\n\n    \"从数值上看，9.9 更大。\" (Numerically, 9.9 is larger.)\n    Explain: 9.9 = 9.90, 9.11 = 9.11. 90 > 11.\n\n    Wait, is there any other interpretation?\n    - Dates? 9/9 vs 9/11. 11th is later.\n    - Versions? 9.11 is newer.\n    - But \"谁大\" (who is big/larger) usually implies magnitude.\n    - I will answer based on numerical magnitude but briefly mention the version context if it adds value without confusing. Actually, better to be decisive. Numerical value is the standard interpretation for \"who is larger\" with numbers.\n\n    Let's go with the numerical answer.\n\n    **Plan:**\n    1. State 9.9 is larger numerically.\n    2. Explain the decimal comparison.\n    3. (Optional but helpful) Mention that if it's version numbers, 9.11 might be considered \"higher\". But prioritize the math.\n    Actually, for a simple query like this, over-explaining versions might be confusing. I'll stick to the math but acknowledge the common confusion.\n\n    *Revised Plan:*\n    1. Direct answer: 9.9 is larger (mathematically).\n    2. Explanation: Place value.\n    3. Note: Unless it's a version number.\n\n    Let's keep it concise.\n\n    \"在数学数值上，9.9 更大。\"\n    \"因为 9.9 等于 9.90，比 9.11 大。\"\n    \"如果是版本号，则 9.11 版本通常高于 9.9 版本。\"\n    This covers both bases.\n\n    Wait, looking at common AI training, this is often a \"trap\" question to see if the AI treats decimals like integers. The correct mathematical answer is crucial.\n\n    Okay, final output structure:\n    1. Mathematical answer.\n    2. Brief explanation.\n    3. Contextual note (versions).\n\n    Let's write it.\n    \"从数学数值上看，9.9 更大。\"\n    \"比较小数时，先看整数部分（都是 9），再看十分位：9.9 的十分位是 9，而 9.11 的十分位是 1。因为 9 > 1，所以 9.9 > 9.11。\"\n    \"不过，如果是软件版本号，通常 9.11 版会比 9.9 版更新。\"\n\n    This is balanced.\n\n    Wait, I need to check the instruction again. \"Please remember the current actual time...\". This is just system context. I don't need to mention the date in the answer unless relevant. It's not relevant here.\n\n    Okay, ready to generate.\n\n    Wait, one more check. Is there any cultural context? In some contexts, people might say 9.11 is bigger because 11 > 9. This is the misconception I need to correct.\n    So, emphasizing the mathematical rule is key.\n\n    Final check:\n    9.9 = 9 + 9/10 = 9.90\n    9.11 = 9 + 1/10 + 1/100 = 9.11\n    9.90 > 9.11. Correct.\n\n    Response will be in Chinese.\n    \"从数值大小来看，9.9 更大。\"\n    Explain why.\n    Mention version exception.\n    Done.\n",
                    "type": "summary_text"
                }
            ],
            "type": "reasoning"
        },
        {
            "content": [
                {
                    "annotations": [],
                    "text": "从**数学数值**上看，**9.9 更大**。\n\n原因如下：\n比较小数时，先看整数部分，都是 9；再看十分位（小数点后第一位）：\n*   9.9 的十分位是 **9**\n*   9.11 的十分位是 **1**\n\n因为 9 大于 1，所以 **9.9 > 9.11**（可以把 9.9 看作 9.90 来比较）。\n\n**注意**：如果是**软件版本号**，通常 9.11 版会比 9.9 版更新（更高），但在纯数字大小比较中，9.9 更大。",
                    "type": "output_text"
                }
            ],
            "id": "msg_xxx",
            "role": "assistant",
            "status": "completed",
            "type": "message"
        }
    ],
    "parallel_tool_calls": false,
    "status": "completed",
    "tool_choice": "auto",
    "tools": [],
    "usage": {
        "input_tokens": 57,
        "input_tokens_details": {
            "cached_tokens": 0
        },
        "output_tokens": 2018,
        "output_tokens_details": {
            "reasoning_tokens": 1861
        },
        "total_tokens": 2075,
        "x_details": [
            {
                "input_tokens": 57,
                "output_tokens": 2018,
                "output_tokens_details": {
                    "reasoning_tokens": 1861
                },
                "total_tokens": 2075,
                "x_billing_type": "response_api"
            }
        ]
    }
}
```

### 流式输出 <span id="example-stream-title" />

通过流式输出实时接收模型生成的内容，适合长文本生成场景。

<CodeGroup dropdown>
  ```python Python
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )

  stream = client.responses.create(
      model="qwen3.8-max",
      input="请简单介绍一下人工智能。",
      stream=True
  )

  print("开始接收流式输出:")
  for event in stream:
      # print(event.model_dump_json())  # 取消注释以查看原始事件响应
      if event.type == 'response.output_text.delta':
          # 实时打印增量文本
          print(event.delta, end='', flush=True)
      elif event.type == 'response.completed':
          print("\n流式输出完成")
          print(f"总 Token 数: {event.response.usage.total_tokens}")
  ```

  ```javascript Node.js expandable
  import OpenAI from "openai";

  const openai = new OpenAI({
      apiKey: process.env.DASHSCOPE_API_KEY,
      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
  });

  async function main() {
      const stream = await openai.responses.create({
          model: "qwen3.8-max",
          input: "请简单介绍一下人工智能。",
          stream: true
      });

      console.log("开始接收流式输出:");
      for await (const event of stream) {
          // console.log(JSON.stringify(event));  // 取消注释以查看原始事件响应
          if (event.type === 'response.output_text.delta') {
              process.stdout.write(event.delta);
          } else if (event.type === 'response.completed') {
              console.log("\n流式输出完成");
              console.log(`总 Token 数: ${event.response.usage.total_tokens}`);
          }
      }
  }

  main();
  ```

  ```bash curl
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "input": "请简单介绍一下人工智能。",
      "stream": true
  }'
  ```
</CodeGroup>

<strong>响应示例</strong>

```json
{"response":{"id":"47a71e7d-868c-4204-9693-ef8ff9058xxx","created_at":1769417481.0,"error":null,"incomplete_details":null,"instructions":null,"metadata":null,"model":"","object":"response","output":[],"parallel_tool_calls":false,"temperature":null,"tool_choice":"auto","tools":[],"top_p":null,"background":null,"completed_at":null,"conversation":null,"max_output_tokens":null,"max_tool_calls":null,"previous_response_id":null,"prompt":null,"prompt_cache_key":null,"prompt_cache_retention":null,"reasoning":null,"safety_identifier":null,"service_tier":null,"status":"queued","text":null,"top_logprobs":null,"truncation":null,"usage":null,"user":null},"sequence_number":0,"type":"response.created"}
{"response":{"id":"47a71e7d-868c-4204-9693-ef8ff9058xxx","created_at":1769417481.0,"error":null,"incomplete_details":null,"instructions":null,"metadata":null,"model":"","object":"response","output":[],"parallel_tool_calls":false,"temperature":null,"tool_choice":"auto","tools":[],"top_p":null,"background":null,"completed_at":null,"conversation":null,"max_output_tokens":null,"max_tool_calls":null,"previous_response_id":null,"prompt":null,"prompt_cache_key":null,"prompt_cache_retention":null,"reasoning":null,"safety_identifier":null,"service_tier":null,"status":"in_progress","text":null,"top_logprobs":null,"truncation":null,"usage":null,"user":null},"sequence_number":1,"type":"response.in_progress"}
{"item":{"id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","content":[],"role":"assistant","status":"in_progress","type":"message"},"output_index":0,"sequence_number":2,"type":"response.output_item.added"}
{"content_index":0,"item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","output_index":0,"part":{"annotations":[],"text":"","type":"output_text","logprobs":null},"sequence_number":3,"type":"response.content_part.added"}
{"content_index":0,"delta":"人工智能","item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":4,"type":"response.output_text.delta"}
{"content_index":0,"delta":"（Art","item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":5,"type":"response.output_text.delta"}
{"content_index":0,"delta":"ificial Intelligence，","item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":6,"type":"response.output_text.delta"}
{"content_index":0,"delta":"简称 AI）","item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":7,"type":"response.output_text.delta"}
... (省略中间事件) ...
{"content_index":0,"delta":"领域，正在深刻改变我们的","item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":38,"type":"response.output_text.delta"}
{"content_index":0,"delta":"生活和工作方式","item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":39,"type":"response.output_text.delta"}
{"content_index":0,"delta":"。","item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":40,"type":"response.output_text.delta"}
{"content_index":0,"item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","logprobs":[],"output_index":0,"sequence_number":41,"text":"人工智能（Artificial Intelligence，简称 AI）是指由计算机系统模拟人类智能行为的技术和科学。xxxx","type":"response.output_text.done"}
{"content_index":0,"item_id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","output_index":0,"part":{"annotations":[],"text":"人工智能（Artificial Intelligence，简称 AI）是指由计算机系统模拟人类智能行为的技术和科学。xxx","type":"output_text","logprobs":null},"sequence_number":42,"type":"response.content_part.done"}
{"item":{"id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","content":[{"annotations":[],"text":"人工智能（Artificial Intelligence，简称 AI）是指由计算机系统模拟人类智能行为的技术和科学。它旨在让机器能够执行通常需要人类智能才能完成的任务，例如：\n\n- **学习**（如通过数据训练模型）  \n- **推理**（如逻辑判断和问题求解）  \n- **感知**（如识别图像、语音或文字）  \n- **理解语言**（如自然语言处理）  \n- **决策**（如在复杂环境中做出最优选择）\n\n人工智能可分为**弱人工智能**（专注于特定任务，如语音助手、推荐系统）和**强人工智能**（具备类似人类的通用智能，目前尚未实现）。\n\n当前，AI 已广泛应用于医疗、金融、交通、教育、娱乐等多个领域，正在深刻改变我们的生活和工作方式。","type":"output_text","logprobs":null}],"role":"assistant","status":"completed","type":"message"},"output_index":0,"sequence_number":43,"type":"response.output_item.done"}
{"response":{"id":"47a71e7d-868c-4204-9693-ef8ff9058xxx","created_at":1769417481.0,"error":null,"incomplete_details":null,"instructions":null,"metadata":null,"model":"qwen3.8-max","object":"response","output":[{"id":"msg_16db29d6-c1d3-47d7-9177-0fba81964xxx","content":[{"annotations":[],"text":"人工智能（Artificial Intelligence，简称 AI）是xxxxxx","type":"output_text","logprobs":null}],"role":"assistant","status":"completed","type":"message"}],"parallel_tool_calls":false,"temperature":null,"tool_choice":"auto","tools":[],"top_p":null,"background":null,"completed_at":null,"conversation":null,"max_output_tokens":null,"max_tool_calls":null,"previous_response_id":null,"prompt":null,"prompt_cache_key":null,"prompt_cache_retention":null,"reasoning":null,"safety_identifier":null,"service_tier":null,"status":"completed","text":null,"top_logprobs":null,"truncation":null,"usage":{"input_tokens":37,"input_tokens_details":{"cached_tokens":0},"output_tokens":166,"output_tokens_details":{"reasoning_tokens":0},"total_tokens":203},"user":null},"sequence_number":44,"type":"response.completed"}
```

### 调用内置工具 <span id="11cd08d0ffnxw" />

开启内置工具可在处理复杂任务时获得更佳效果，当前网页抓取与代码解释器工具限时免费，支持的工具请参见[工具调用](/zh/model-studio/tool-calls)。

`qwen3.8-omni-flash` 的内置工具仅支持 `web_search`，同时支持自定义 `function` 工具。

<CodeGroup dropdown>
  ```python Python
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
  )

  response = client.responses.create(
      model="qwen3.8-max",
      input="帮我找一下阿里云官网，并提取首页的关键信息",
      # 建议同时开启内置工具以取得最佳效果
      tools=[
          {"type": "web_search"},
          {"type": "code_interpreter"},
          {"type": "web_extractor"}
      ],
      reasoning={"effort": "medium"}
  )

  # 取消以下注释查看中间过程输出
  # print(response.output)
  print(response.output_text)
  ```

  ```javascript Node.js expandable
  import OpenAI from "openai";

  const openai = new OpenAI({
      apiKey: process.env.DASHSCOPE_API_KEY,
      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
  });

  async function main() {
      const response = await openai.responses.create({
          model: "qwen3.8-max",
          input: "帮我找一下阿里云官网，并提取首页的关键信息",
          tools: [
              { type: "web_search" },
              { type: "code_interpreter" },
              { type: "web_extractor" }
          ],
          reasoning: { effort: "medium" }
      });

      for (const item of response.output) {
          if (item.type === "reasoning") {
              console.log("模型正在思考...");
          } else if (item.type === "web_search_call") {
              console.log(`搜索查询: ${item.action.query}`);
          } else if (item.type === "web_extractor_call") {
              console.log("正在抽取网页内容...");
          } else if (item.type === "message") {
              console.log(`回复内容: ${item.content[0].text}`);
          }
      }
  }

  main();
  ```

  ```bash curl
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "input": "帮我找一下阿里云官网，并提取首页的关键信息",
      "tools": [
          {
              "type": "web_search"
          },
          {
              "type": "code_interpreter"
          },
          {
              "type": "web_extractor"
          }
      ],
      "reasoning": {"effort": "medium"}
  }'
  ```
</CodeGroup>

<strong>响应示例</strong>

```json expandable
{
    "id": "69258b21-5099-9d09-92e8-8492b1955xxx",
    "object": "response",
    "status": "completed",
    "output": [
        {
            "type": "reasoning",
            "summary": [
                {
                    "type": "summary_text",
                    "text": "用户要求找阿里云官网并提取信息..."
                }
            ]
        },
        {
            "type": "web_search_call",
            "status": "completed",
            "action": {
                "query": "阿里云官网",
                "type": "search",
                "sources": [
                    {
                        "type": "url",
                        "url": "https://cn.aliyun.com/"
                    },
                    {
                        "type": "url",
                        "url": "https://www.alibabacloud.com/zh"
                    }
                ]
            }
        },
        {
            "type": "reasoning",
            "summary": [
                {
                    "type": "summary_text",
                    "text": "搜索结果显示阿里云官网URL..."
                }
            ]
        },
        {
            "type": "web_extractor_call",
            "status": "completed",
            "goal": "提取阿里云官网首页的关键信息",
            "output": "通义大模型、完整产品体系、AI解决方案...",
            "urls": [
                "https://cn.aliyun.com/"
            ]
        },
        {
            "type": "message",
            "role": "assistant",
            "status": "completed",
            "content": [
                {
                    "type": "output_text",
                    "text": "阿里云官网关键信息：通义大模型，云计算服务..."
                }
            ]
        }
    ],
    "usage": {
        "input_tokens": 40836,
        "output_tokens": 2106,
        "total_tokens": 42942,
        "output_tokens_details": {
            "reasoning_tokens": 677
        },
        "x_tools": {
            "web_extractor": {
                "count": 1
            },
            "web_search": {
                "count": 1
            }
        }
    }
}
```

### Session 缓存 <span id="example-session-cache-title" />

在多轮对话场景中，开启 Session 缓存可让服务端自动缓存对话上下文，降低推理延迟与使用成本。您无需手动管理缓存，只需按正常多轮对话方式调用即可。

自动生效的隐式缓存另见[上下文缓存](/zh/model-studio/context-cache)。

<strong>使用方式</strong>：在请求 Header 中添加 `x-dashscope-session-cache: enable` 开启，或设置为 `disable` 关闭。默认值为 `disable`。

<strong>缓存行为</strong>：

- 启用 Session 缓存：

  - 模型支持显式缓存：使用显式缓存，计费和相关约束参照[显式缓存](/zh/model-studio/context-cache#825f201c5fy6o)。
  - 模型不支持显式缓存但支持隐式缓存：使用隐式缓存，计费和相关约束参照[隐式缓存](/zh/model-studio/context-cache#2317ea09cfxok)。
- 不启用 Session 缓存：与正常调用行为一致。支持隐式缓存的模型，隐式缓存仍自动开启，但不享受 Session 缓存带来的增益。

<CodeGroup dropdown>
  ```python Python expandable
  import os
  from openai import OpenAI

  client = OpenAI(
      api_key=os.getenv("DASHSCOPE_API_KEY"),
      base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
      # 通过 default_headers 开启 Session 缓存
      default_headers={"x-dashscope-session-cache": "enable"}
  )

  # 构造超过 1024 Token 的长文本，确保能触发缓存创建（若未达到1024 Token，后续累积对话上下文超过1024 Token时将触发缓存创建）
  long_context = "人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。" * 50

  # 第一轮对话
  response1 = client.responses.create(
      model="qwen3.8-max",
      input=long_context + "\n\n基于以上背景知识，请简短介绍机器学习中的随机森林算法。",
  )
  print(f"第一轮回复: {response1.output_text}")

  # 第二轮对话：通过 previous_response_id 关联上下文，缓存由服务端自动处理
  response2 = client.responses.create(
      model="qwen3.8-max",
      input="它和 GBDT 有什么主要区别？",
      previous_response_id=response1.id,
  )
  print(f"第二轮回复: {response2.output_text}")

  # 查看缓存命中情况
  usage = response2.usage
  print(f"输入 Token: {usage.input_tokens}")
  print(f"缓存命中 Token: {usage.input_tokens_details.cached_tokens}")
  ```

  ```javascript Node.js expandable
  import OpenAI from "openai";

  const openai = new OpenAI({
      apiKey: process.env.DASHSCOPE_API_KEY,
      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
      // 通过 defaultHeaders 开启 Session 缓存
      defaultHeaders: {"x-dashscope-session-cache": "enable"}
  });

  // 构造超过 1024 Token 的长文本，确保能触发缓存创建（若未达到1024 Token，后续累积对话上下文超过1024 Token时将触发缓存创建）
  const longContext = "人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。".repeat(50);

  async function main() {
      // 第一轮对话
      const response1 = await openai.responses.create({
          model: "qwen3.8-max",
          input: longContext + "\n\n基于以上背景知识，请简短介绍机器学习中的随机森林算法，包括基本原理和应用场景。"
      });
      console.log(`第一轮回复: ${response1.output_text}`);

      // 第二轮对话：通过 previous_response_id 关联上下文，缓存由服务端自动处理
      const response2 = await openai.responses.create({
          model: "qwen3.8-max",
          input: "它和 GBDT 有什么主要区别？",
          previous_response_id: response1.id
      });
      console.log(`第二轮回复: ${response2.output_text}`);

      // 查看缓存命中情况
      console.log(`输入 Token: ${response2.usage.input_tokens}`);
      console.log(`缓存命中 Token: ${response2.usage.input_tokens_details.cached_tokens}`);
  }

  main();
  ```

  ```bash curl
  # 第一轮对话
  # 请将 input 替换为超过 1024 Token 的长文本，以确保触发缓存创建
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -H "x-dashscope-session-cache: enable" \
  -d '{
      "model": "qwen3.8-max",
      "input": "人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。人工智能是计算机科学的一个重要分支，致力于研究和开发能够模拟、延伸和扩展人类智能的理论、方法、技术及应用系统。\n\n基于以上背景知识，请简短介绍机器学习中的随机森林算法，包括基本原理和应用场景。"
  }'

  # 第二轮对话 - 使用上一轮返回的 id 作为 previous_response_id
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -H "x-dashscope-session-cache: enable" \
  -d '{
      "model": "qwen3.8-max",
      "input": "它和 GBDT 有什么主要区别？",
      "previous_response_id": "第一轮返回的响应id"
  }'
  ```
</CodeGroup>

## 从 Chat Completions 迁移到 Responses API <span id="migration-title" />

如果您当前使用的是 OpenAI Chat Completions API，可以通过以下步骤迁移到 Responses API。Responses API 提供了更简洁的接口和更强大的功能，同时保持了与 Chat Completions 的兼容性。

### 1. 更新端点地址 <span id="migration-step1-title" />

从 `/v1/chat/completions` 更新为 `/v1/responses`。

<CodeGroup dropdown>
  ```python Python expandable
  # Chat Completions API
  completion = client.chat.completions.create(
      model="qwen3.8-max",
      messages=[
          {"role": "system", "content": "你是一个有帮助的助手。"},
          {"role": "user", "content": "你好！"}
      ]
  )
  print(completion.choices[0].message.content)

  # Responses API - 可以使用相同的消息格式
  response = client.responses.create(
      model="qwen3.8-max",
      input=[
          {"role": "system", "content": "你是一个有帮助的助手。"},
          {"role": "user", "content": "你好！"}
      ]
  )
  print(response.output_text)

  # Responses API - 或使用更简洁的格式
  response = client.responses.create(
      model="qwen3.8-max",
      input="你好！"
  )
  print(response.output_text)
  ```

  ```javascript Node.js expandable
  // Chat Completions API
  const completion = await client.chat.completions.create({
      model: "qwen3.8-max",
      messages: [
          { role: "system", content: "你是一个有帮助的助手。" },
          { role: "user", content: "你好！" }
      ]
  });
  console.log(completion.choices[0].message.content);

  // Responses API - 可以使用相同的消息格式
  const response = await client.responses.create({
      model: "qwen3.8-max",
      input: [
          { role: "system", content: "你是一个有帮助的助手。" },
          { role: "user", content: "你好！" }
      ]
  });
  console.log(response.output_text);

  // Responses API - 或使用更简洁的格式
  const response2 = await client.responses.create({
      model: "qwen3.8-max",
      input: "你好！"
  });
  console.log(response2.output_text);
  ```

  ```bash curl
  # Chat Completions API
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "messages": [
          {"role": "system", "content": "你是一个有帮助的助手。"},
          {"role": "user", "content": "你好！"}
      ]
  }'

  # Responses API - 使用更简洁的格式
  curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{
      "model": "qwen3.8-max",
      "input": "你好！"
  }'
  ```
</CodeGroup>

### 2. 更新响应处理 <span id="migration-step2-title" />

Responses API 的响应结构有所不同。使用 `output_text` 快捷方法获取文本输出，或通过 `output` 数组访问详细信息。

<strong>响应对比</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        # Chat Completions 响应
        {
          "id": "chatcmpl-416b0ea5-e362-9fec-97c5-0a60b5d7xxx",
          "choices": [
            {
              "finish_reason": "stop",
              "index": 0,
              "logprobs": null,
              "message": {
                "content": "你好！很高兴见到你～ 有什么我可以帮你的吗？",
                "refusal": null,
                "role": "assistant",
                "function_call": null,
                "tool_calls": null
              }
            }
          ],
          "created": 1769416269,
          "model": "qwen3.8-max",
          "object": "chat.completion",
          "service_tier": null,
          "system_fingerprint": null,
          "usage": {
            "completion_tokens": 14,
            "prompt_tokens": 22,
            "total_tokens": 36,
            "prompt_tokens_details": {
              "cached_tokens": 0
            }
          }
        }
        ```
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json expandable
        # Responses API 响应
        {
          "id": "d69c735d-0f5e-4b6c-9c2a-8cab5eb14xxx",
          "created_at": 1769416269.0,
          "model": "qwen3.8-max",
          "object": "response",
          "status": "completed",
          "output": [
            {
              "id": "msg_3426d3e5-8da7-4dd8-a6a5-7c2cd866xxx",
              "type": "message",
              "role": "assistant",
              "status": "completed",
              "content": [
                {
                  "type": "output_text",
                  "text": "你好！今天是2026年1月26日，星期一。有什么我可以帮你的吗？",
                  "annotations": []
                }
              ]
            }
          ],
          "usage": {
            "input_tokens": 34,
            "output_tokens": 25,
            "total_tokens": 59,
            "input_tokens_details": {
              "cached_tokens": 0
            },
            "output_tokens_details": {
              "reasoning_tokens": 0
            }
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

### 3. 简化多轮对话管理 <span id="migration-step3-title" />

在 Chat Completions 中需要手动管理消息历史数组，而 Responses API 提供了 `previous_response_id` 参数自动关联上下文，当前响应`id`有效期为7天。

<Tabs>
  <Tab title="Python">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
      <colgroup>
        <col style={{ width: "50%" }} />

        <col style={{ width: "50%" }} />
      </colgroup>

      <tbody>
        <tr>
          <td style={{ verticalAlign: "top" }}>
            ```python
            # Chat Completions - 需要手动管理消息历史
            messages = [
                {"role": "system", "content": "你是一个有帮助的助手。"},
                {"role": "user", "content": "法国的首都是哪里？"}
            ]
            res1 = client.chat.completions.create(
                model="qwen3.8-max",
                messages=messages
            )

            # 手动添加响应到历史
            messages.append(res1.choices[0].message)
            messages.append({"role": "user", "content": "它的人口是多少？"})

            res2 = client.chat.completions.create(
                model="qwen3.8-max",
                messages=messages
            )
            ```
          </td>

          <td style={{ verticalAlign: "top" }}>
            ```python
            # Responses API - 使用 previous_response_id 自动关联
            res1 = client.responses.create(
                model="qwen3.8-max",
                input="法国的首都是哪里？"
            )

            # 只需传递上一轮的 ID
            res2 = client.responses.create(
                model="qwen3.8-max",
                input="它的人口是多少？",
                previous_response_id=res1.id
            )
            ```
          </td>
        </tr>
      </tbody>
    </table>
  </Tab>

  <Tab title="Node.js">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
      <colgroup>
        <col style={{ width: "50%" }} />

        <col style={{ width: "50%" }} />
      </colgroup>

      <tbody>
        <tr>
          <td style={{ verticalAlign: "top" }}>
            ```javascript
            // Chat Completions - 需要手动管理消息历史
            let messages = [
                { role: "system", content: "你是一个有帮助的助手。" },
                { role: "user", content: "法国的首都是哪里？" }
            ];
            const res1 = await client.chat.completions.create({
                model: "qwen3.8-max",
                messages
            });

            // 手动添加响应到历史
            messages = messages.concat([res1.choices[0].message]);
            messages.push({ role: "user", content: "它的人口是多少？" });

            const res2 = await client.chat.completions.create({
                model: "qwen3.8-max",
                messages
            });
            ```
          </td>

          <td style={{ verticalAlign: "top" }}>
            ```javascript
            // Responses API - 使用 previous_response_id 自动关联
            const res1 = await client.responses.create({
                model: "qwen3.8-max",
                input: "法国的首都是哪里？"
            });

            // 只需传递上一轮的 ID
            const res2 = await client.responses.create({
                model: "qwen3.8-max",
                input: "它的人口是多少？",
                previous_response_id: res1.id
            });
            ```
          </td>
        </tr>
      </tbody>
    </table>
  </Tab>
</Tabs>

### 4. 使用内置工具 <span id="migration-step4-title" />

Responses API 内置了多种工具，无需自行实现。只需在 `tools` 参数中指定即可，当前代码解释器与网页抓取工具限时免费，详情请参见[工具调用](/zh/model-studio/tool-calls)。

<Tabs>
  <Tab title="Python">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
      <colgroup>
        <col style={{ width: "50%" }} />

        <col style={{ width: "50%" }} />
      </colgroup>

      <tbody>
        <tr>
          <td style={{ verticalAlign: "top" }}>
            ```python
            # Chat Completions - 需要自己实现工具函数
            def web_search(query):
                # 需要自己实现网络搜索逻辑
                import requests
                r = requests.get(f"https://api.example.com/search?q={query}")
                return r.json().get("results", [])

            completion = client.chat.completions.create(
                model="qwen3.8-max",
                messages=[{"role": "user", "content": "法国现任总统是谁？"}],
                functions=[{
                    "name": "web_search",
                    "description": "搜索网络信息",
                    "parameters": {
                        "type": "object",
                        "properties": {"query": {"type": "string"}},
                        "required": ["query"]
                    }
                }]
            )
            ```
          </td>

          <td style={{ verticalAlign: "top" }}>
            ```python
            # Responses API - 直接使用内置工具
            response = client.responses.create(
                model="qwen3.8-max",
                input="法国现任总统是谁？",
                tools=[{"type": "web_search"}]  # 直接启用网络搜索
            )
            print(response.output_text)
            ```
          </td>
        </tr>
      </tbody>
    </table>
  </Tab>

  <Tab title="Node.js">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
      <colgroup>
        <col style={{ width: "50%" }} />

        <col style={{ width: "50%" }} />
      </colgroup>

      <tbody>
        <tr>
          <td style={{ verticalAlign: "top" }}>
            ```javascript
            // Chat Completions - 需要自己实现工具函数
            async function web_search(query) {
                const fetch = (await import('node-fetch')).default;
                const res = await fetch(`https://api.example.com/search?q=${query}`);
                const data = await res.json();
                return data.results;
            }

            const completion = await client.chat.completions.create({
                model: "qwen3.8-max",
                messages: [{ role: "user", content: "法国现任总统是谁？" }],
                functions: [{
                    name: "web_search",
                    description: "搜索网络信息",
                    parameters: {
                        type: "object",
                        properties: { query: { type: "string" } },
                        required: ["query"]
                    }
                }]
            });
            ```
          </td>

          <td style={{ verticalAlign: "top" }}>
            ```javascript
            // Responses API - 直接使用内置工具
            const response = await client.responses.create({
                model: "qwen3.8-max",
                input: "法国现任总统是谁？",
                tools: [{ type: "web_search" }]  // 直接启用网络搜索
            });
            console.log(response.output_text);
            ```
          </td>
        </tr>
      </tbody>
    </table>
  </Tab>

  <Tab title="curl">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
      <colgroup>
        <col style={{ width: "50%" }} />

        <col style={{ width: "50%" }} />
      </colgroup>

      <tbody>
        <tr>
          <td style={{ verticalAlign: "top" }}>
            ```bash
            # Chat Completions - 需要自己实现工具
            # 这里展示调用外部搜索 API 的示例
            curl https://api.example.com/search \
              -G \
              --data-urlencode "q=法国现任总统" \
              --data-urlencode "key=$SEARCH_API_KEY"
            ```
          </td>

          <td style={{ verticalAlign: "top" }}>
            ```bash
            # Responses API - 直接使用内置工具
            curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/responses \
            -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
            -H "Content-Type: application/json" \
            -d '{
                "model": "qwen3.8-max",
                "input": "法国现任总统是谁？",
                "tools": [{"type": "web_search"}]
            }'
            ```
          </td>
        </tr>
      </tbody>
    </table>
  </Tab>
</Tabs>

## 常见问题 <span id="faq-title" />

### Q：如何传递多轮对话的上下文？ <span id="9c46dd5a352cp" />

A：在发起新一轮对话请求时，请将上一轮模型响应成功返回的`id`作为 `previous_response_id` 参数传入。

### Q：为何无法打印 output\_text？ <span id="9e44ac2b5af26" />

A：OpenAI Python SDK 在某些版本（如1.99.2）错误移除了该属性，请更新 SDK 为最新版以避免该报错。

## 相关文档 <span id="related-title" />

- [创建响应](/zh/model-studio/qwen-api-via-openai-responses)
- [获取与配置 API Key](/zh/model-studio/get-api-key)
- [配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)
