using Cnblogs.DashScope.Core;
using Microsoft.Extensions.AI;

namespace Cnblogs.DashScope.Sample.MsExtensionsAI
{
    public class RawInputExample : MsExtensionsAiSample
    {
        /// <inheritdoc />
        public override string Description => "Chat with raw message and parameter input";

        /// <inheritdoc />
        public override async Task RunAsync(IDashScopeClient client)
        {
            var messages = new List<TextChatMessage>()
            {
                TextChatMessage.DocUrl(
                    "从这两份产品手册中，提取所有产品信息，并整理成一个标准的JSON数组。每个对象需要包含：model(产品的型号)、name(产品的名称)、price(价格（去除货币符号和逗号）)",
                    new[]
                    {
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251107/jockge/%E7%A4%BA%E4%BE%8B%E4%BA%A7%E5%93%81%E6%89%8B%E5%86%8CA.docx",
                        "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251107/ztwxzr/%E7%A4%BA%E4%BE%8B%E4%BA%A7%E5%93%81%E6%89%8B%E5%86%8CB.docx"
                    })
            };
            var parameters = new TextGenerationParameters()
            {
                ResultFormat = "message", IncrementalOutput = true,
            };

            var response = client
                .AsChatClient("qwen-doc-turbo")
                .GetStreamingResponseAsync(
                    messages.Select(x => new ChatMessage() { RawRepresentation = x }),
                    new ChatOptions()
                    {
                        AdditionalProperties = new AdditionalPropertiesDictionary() { { "raw", parameters } }
                    });
            await foreach (var chunk in response)
            {
                Console.Write(chunk.Text);
            }
        }
    }
}

/*
```json
[
  {
    "model": "PRO-100",
    "name": "智能打印机",
    "price": "8999"
  },
  {
    "model": "PRO-200",
    "name": "智能扫描仪",
    "price": "12999"
  },
  {
    "model": "PRO-300",
    "name": "智能会议系统",
    "price": "25999"
  },
  {
    "model": "PRO-400",
    "name": "智能考勤机",
    "price": "6999"
  },
  {
    "model": "PRO-500",
    "name": "智能文件柜",
    "price": "15999"
  },
  {
    "model": "SEC-100",
    "name": "智能监控摄像头",
    "price": "3999"
  },
  {
    "model": "SEC-200",
    "name": "智能门禁系统",
    "price": "15999"
  },
  {
    "model": "SEC-300",
    "name": "智能报警系统",
    "price": "28999"
  },
  {
    "model": "SEC-400",
    "name": "智能访客系统",
    "price": "9999"
  },
  {
    "model": "SEC-500",
    "name": "智能停车管理",
    "price": "22999"
  }
]
```
 */
