> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 决策模型 API

> 调用 POST /compatible-mode/v1/systemone 接口使用百炼决策模型（decision-model-preview），一次前向返回分类、评分、是非判断及其概率分布与置信度，不生成文本。适用于工单分流、内容审核、智能体路由与结果校验等高频结构化决策场景。

<meta property="og:title" content="决策模型 API" />

## 前提条件 <span id="dm-010" />

已创建 API Key 并配置为环境变量 `DASHSCOPE_API_KEY`。配置方法请参见[配置 API Key 到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Tip>
  **在线体验**：在编写代码前，可先通过 [Playground](https://bailian.console.aliyun.com/systemone_playground) 在线体验决策模型。选择预设场景（客服对话判断、Agent 完成度核验、工单分流、内容审核、Agent 任务路由、医疗导诊辅助），输入业务内容并运行模型，查看判断结果、概率分布与 Token 用量，再修改条件观察结果变化。
</Tip>

## 请求说明 <span id="dm-020" />

<strong>协议</strong>：TypeSafe System One（`POST /compatible-mode/v1/systemone`）

一次请求携带业务 `state` 与若干 typed 问题（`choice` / `noul` / `score`），模型一次前向返回每个问题的判定结果与概率分布，其中 `choice` 与 `score` 额外返回置信度，**不生成文本**，因此延迟与成本与输出长度无关。

<strong>请求地址</strong>

调用时请将 `{WorkspaceId}` 替换为业务空间 ID，详见[地域与域名](/zh/model-studio/regions#h2_migrate_domain)。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><thead><tr><th><p><strong>地域</strong></p></th><th><p><strong>Endpoint</strong></p></th></tr></thead><tbody><tr><td><p>华北2（北京）</p></td><td><p><code>https\://\{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/systemone</code></p></td></tr><tr><td><p>新加坡</p></td><td><p><code>https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/systemone</code></p></td></tr></tbody></table>

<Columns cols={2}>
  <Column>
    ## 请求参数 <span id="dm-100" />

    <strong>Content-Type</strong> · `String` · Header · 必选

    请求类型：`application/json`。

    <strong>Authorization</strong> · `String` · Header · 必选

    API-Key，格式：`Bearer $DASHSCOPE_API_KEY`。

    <strong>model</strong> · `String` · Body · 必选

    模型名，取值 `decision-model-preview`。

    <strong>state</strong> · `String / Object / Array` · Body · 必选

    待决策的业务状态：工单文本、对话或结构化对象（对象会被序列化后送模型）。

    <strong>questions</strong> · `Object` · Body · 必选

    问题表。key 为调用方自定义的问题 id；value 为问题对象，字段如下。

    <Accordion title="问题对象属性">
      <strong>type</strong> · `String` · 必选

      问题类型，取值：

      - `choice`：多选一
      - `noul`：是否判断
      - `score`：有序量表

      <strong>instructions</strong> · `String` · 可选

      问题描述或评判标准。

      <strong>criteria</strong> · `Object / Array` · 视类型

      - `choice`：选项名 → 选项描述 的映射（1–255 项，建议给全量选项并补 `other` 兜底）。
      - `noul`：可选 `{"true":…, "false":…}` 描述。
      - `score`：从低到高的等级描述数组（2–255 级，建议 3–7 级且每级可清晰区分）。
    </Accordion>

    <strong>接口限制与建议</strong>

    - 问题数上限 16（延迟随问题数近线性增长）。
    - `choice` 选项 ≤ 255。
    - `score` 等级 2–255（建议 3–7 级）。
    - 上下文上限 65536 token，超长 `state` 会被拒绝。
  </Column>

  <Column>
    ## 请求示例 <span id="dm-400" />

    Python 示例使用 TypeSafe SDK（`typesafe-sdk`）调用，`base_url` 指向百炼网关，SDK 会自动拼接 `/v1/systemone` 并解析返回体。安装：`pip install typesafe-sdk`。

    <Tabs>
      <Tab title="工单分流示例">
        一次请求同时完成「派单团队 choice」「是否升级 noul」「严重度 score」三项决策。以下以华北2（北京）地域为例。

        <CodeGroup>
          ```bash title="curl"
          curl -sS -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/systemone \
            -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
            -H 'Content-Type: application/json' \
            -d '{
              "model": "decision-model-preview",
              "state": {"ticket_id": "T-1001", "content": "订单支付后超过 24 小时仍未到账，用户无法继续使用核心服务，要求立即处理。"},
              "questions": {
                "department": {"type": "choice", "instructions": "应该由哪个团队处理？",
                               "criteria": {"billing": "支付、退款和账单问题", "technical": "产品故障和集成问题"}},
                "escalate":   {"type": "noul",  "instructions": "是否需要立即通知值班人员？"},
                "severity":   {"type": "score", "instructions": "这个问题有多严重？",
                               "criteria": ["轻微问题，不影响功能", "部分功能受影响，但存在替代方案",
                                            "核心功能不可用，没有替代方案", "造成严重业务或安全影响"]}
              }
            }'
          ```

          ```python title="Python"
          import os
          from typesafe_sdk import TypeSafeClient

          client = TypeSafeClient(
              api_key=os.environ["DASHSCOPE_API_KEY"],
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode",
          )

          result = client.system_one(
              model="decision-model-preview",
              state={"ticket_id": "T-1001", "content": "订单支付后超过 24 小时仍未到账，用户无法继续使用核心服务。"},
              questions={
                  "department": {"type": "choice", "instructions": "应该由哪个团队处理？",
                                 "criteria": {"billing": "支付、退款和账单问题", "technical": "产品故障和集成问题"}},
                  "escalate": {"type": "noul", "instructions": "是否需要立即通知值班人员？"},
                  "severity": {"type": "score", "instructions": "这个问题有多严重？",
                               "criteria": ["轻微问题，不影响功能", "部分功能受影响，但存在替代方案",
                                            "核心功能不可用，没有替代方案", "造成严重业务或安全影响"]},
              },
          )

          print(result.answers["department"])   # choice, confidence, probabilities
          print(result.answers["escalate"])     # noul P(yes)
          print(result.answers["severity"])     # score, legend, probabilities
          ```
        </CodeGroup>
      </Tab>

      <Tab title="是非判断示例">
        对一个 `noul` 问题返回 P(yes) 概率，0=否 1=是。以下以华北2（北京）地域为例。

        <CodeGroup>
          ```bash title="curl"
          curl -sS -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/systemone \
            -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
            -H 'Content-Type: application/json' \
            -d '{
              "model": "decision-model-preview",
              "state": "这段评论是否在索要退款？",
              "questions": {
                "refund": {"type": "noul", "instructions": "是否在索要退款？"}
              }
            }'
          ```

          ```python title="Python"
          import os
          from typesafe_sdk import TypeSafeClient

          client = TypeSafeClient(
              api_key=os.environ["DASHSCOPE_API_KEY"],
              base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode",
          )

          result = client.system_one(
              model="decision-model-preview",
              state="这段评论是否在索要退款？",
              questions={"refund": {"type": "noul", "instructions": "是否在索要退款？"}},
          )

          print(result.answers["refund"])   # noul P(yes)
          ```
        </CodeGroup>
      </Tab>
    </Tabs>
  </Column>
</Columns>

<Columns cols={2}>
  <Column>
    ## 返回参数 <span id="dm-300" />

    <strong>model</strong> · `String`

    模型名回显。

    <strong>request\_id</strong> · `String`

    请求 ID，用于问题排查。

    <strong>answers</strong> · `Object`

    答案表，key 与请求中的问题 id 一一对应，value 为答案对象，字段如下。

    <Accordion title="答案对象属性">
      <strong>type</strong> · `String`

      与问题类型一致（`choice` / `noul` / `score`）。

      <strong>choice</strong> · `String`

      `choice` 类型：选中的选项名。

      <strong>noul</strong> · `Float`

      `noul` 类型：P(yes)，0=否 1=是。

      <strong>score</strong> · `Float`

      `score` 类型：等级索引的概率加权期望，可落在两级之间（如 `1.43`）。

      <strong>probabilities</strong> · `Object`

      每个选项 / 等级的概率（和为 1）。

      <strong>confidence</strong> · `Float`

      该答案的置信度（`choice` / `score` 类型返回）。

      <strong>legend</strong> · `Object`

      `score` 类型：等级索引 → 等级描述。
    </Accordion>

    <strong>usage</strong> · `Object`

    计量信息：

    - `input_tokens`（Integer）：输入 token 数。

    <strong>latency\_ms</strong> · `Float`

    服务端耗时（毫秒）。
  </Column>

  <Column>
    ## 返回示例 <span id="dm-500" />

    以下为工单分流示例的返回结果（节选），概率与置信度以实际返回为准。

    ```json expandable
    {
      "model": "decision-model-preview",
      "request_id": "7b986c65-b223-9341-b5f0-b988e27ecaac",
      "answers": {
        "department": {
          "type": "choice",
          "choice": "billing",
          "confidence": 0.88,
          "probabilities": {"billing": 0.94, "technical": 0.06}
        },
        "escalate": {
          "type": "noul",
          "noul": 0.99
        },
        "severity": {
          "type": "score",
          "score": 2.25,
          "confidence": 0.91,
          "legend": {
            "0": "轻微问题，不影响功能",
            "1": "部分功能受影响，但存在替代方案",
            "2": "核心功能不可用，没有替代方案",
            "3": "造成严重业务或安全影响"
          },
          "probabilities": {"0": 0.0, "1": 0.01, "2": 0.73, "3": 0.26}
        }
      },
      "usage": {"input_tokens": 125},
      "latency_ms": 52.9
    }
    ```
  </Column>
</Columns>

## 错误码 <span id="dm-600" />

如果调用失败，会返回错误信息。更多错误码及解决方法，请参见[错误信息](/zh/model-studio/error-code)。
