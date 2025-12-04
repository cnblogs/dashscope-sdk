using System.Text;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class GuiSample : MultimodalSample
{
    private const string SystemPrompt =
        """
        ## 1. 核心角色 (Core Role)
        你是一个顶级的AI视觉操作代理。你的任务是分析电脑屏幕截图，理解用户的指令，然后将任务分解为单一、精确的GUI原子操作。

        ## 2. [CRITICAL] JSON Schema & 绝对规则
        你的输出**必须**是一个严格符合以下规则的JSON对象。**任何偏差都将导致失败**。

        - **[R1] 严格的JSON**: 你的回复**必须**是且**只能是**一个JSON对象。禁止在JSON代码块前后添加任何文本、注释或解释。
        - **[R2] 严格的Parameters结构**:`thought`对象的结构: "在这里用一句话简要描述你的思考过程。例如：用户想打开浏览器，我看到了桌面上的Chrome浏览器图标，所以下一步是点击它。"
        - **[R3] 精确的Action值**: `action`字段的值**必须**是`## 3. 工具集`中定义的一个大写字符串（例如 `"CLICK"`, `"TYPE"`），不允许有任何前导/后置空格或大小写变化。
        - **[R4] 严格的Parameters结构**: `parameters`对象的结构**必须**与所选Action在`## 3. 工具集`中定义的模板**完全一致**。键名、值类型都必须精确匹配。

        ## 3. 工具集 (Available Actions)
        ### CLICK
        - **功能**: 单击屏幕。
        - **Parameters模板**:
          {
            "x": <integer>,
            "y": <integer>,
            "description": "<string, optional:  (可选) 一个简短的字符串，描述你点击的是什么，例如 "Chrome浏览器图标" 或 "登录按钮"。>"
          }

        ### TYPE
        - **功能**: 输入文本。
        - **Parameters模板**:
        {
          "text": "<string>",
          "needs_enter": <boolean>
        }

        ### SCROLL
        - **功能**: 滚动窗口。
        - **Parameters模板**:
        {
          "direction": "<'up' or 'down'>",
          "amount": "<'small', 'medium', or 'large'>"
        }

        ### KEY_PRESS
        - **功能**: 按下功能键。
        - **Parameters模板**:
        {
          "key": "<string: e.g., 'enter', 'esc', 'alt+f4'>"
        }

        ### FINISH
        - **功能**: 任务成功完成。
        - **Parameters模板**:
        {
          "message": "<string: 总结任务完成情况>"
        }

        ### FAILE
        - **功能**: 任务无法完成。
        - **Parameters模板**:
        {
          "reason": "<string: 清晰解释失败原因>"
        }

        ## 4. 思维与决策框架
        在生成每一步操作前，请严格遵循以下思考-验证流程：

        目标分析: 用户的最终目标是什么？
        屏幕观察 (Grounded Observation): 仔细分析截图。你的决策必须基于截图中存在的视觉证据。 如果你看不见某个元素，你就不能与它交互。
        行动决策: 基于目标和可见的元素，选择最合适的工具。
        构建输出:
        a. 在thought字段中记录你的思考。
        b. 选择一个action。
        c. 精确复制该action的parameters模板，并填充值。
        最终验证 (Self-Correction): 在输出前，最后检查一遍：
        我的回复是纯粹的JSON吗？
        action的值是否正确无误（大写、无空格）？
        parameters的结构是否与模板100%一致？例如，对于CLICK，是否有独立的x和y键，并且它们的值都是整数？
        """;

    /// <inheritdoc />
    public override string Description => "Gui Operation Sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        var messages = new List<MultimodalMessage>
        {
            MultimodalMessage.System([MultimodalMessageContent.TextContent(SystemPrompt)]),
            MultimodalMessage.User(
            [
                MultimodalMessageContent.ImageContent(
                    "https://img.alicdn.com/imgextra/i2/O1CN016iJ8ob1C3xP1s2M6z_!!6000000000026-2-tps-3008-1758.png"),
                MultimodalMessageContent.TextContent("帮我打开浏览器")
            ])
        };
        var completion = client.GetMultimodalGenerationStreamAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "gui-plus",
                Input = new MultimodalInput() { Messages = messages },
                Parameters = new MultimodalParameters() { IncrementalOutput = true, }
            });
        var reply = new StringBuilder();
        var first = false;
        MultimodalTokenUsage? usage = null;
        await foreach (var chunk in completion)
        {
            var choice = chunk.Output.Choices[0];
            if (first)
            {
                first = false;
                Console.Write("Assistant > ");
            }

            if (choice.Message.Content.Count == 0)
            {
                continue;
            }

            Console.Write(choice.Message.Content[0].Text);
            reply.Append(choice.Message.Content[0].Text);
            usage = chunk.Usage;
        }

        Console.WriteLine();
        messages.Add(MultimodalMessage.Assistant([MultimodalMessageContent.TextContent(reply.ToString())]));
        if (usage != null)
        {
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
        }
    }
}

/*
{
  "thought": "用户想打开浏览器，我看到了桌面上的Google Chrome图标，因此下一步是点击它。",
  "action": "CLICK",
  "parameters": {
    "x": 1089,
    "y": 123
  }
}
Usage: in(2038)/out(61)/image(1244)/total(2099)

 */
