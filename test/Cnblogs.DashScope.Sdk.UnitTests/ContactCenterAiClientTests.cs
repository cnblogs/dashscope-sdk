using System.Net;
using System.Text;
using Cnblogs.DashScope.Core;
using Cnblogs.DashScope.Tests.Shared.Utils;
using NSubstitute;
using NSubstitute.Extensions;

namespace Cnblogs.DashScope.Sdk.UnitTests;

public class ContactCenterAiClientTests
{
    private const string WorkspaceId = "ws-demo";
    private const string AppId = "app-demo";

    [Fact]
    public async Task AnalyzeConversation_SummaryBestPractice_SerializesResultTypesAndDialogueAsync()
    {
        var dialogue = CreateSampleDialogue();
        var request = AnalyzeConversationRequest.ForSummary(dialogue, CcaiResultTypes.Title, CcaiResultTypes.Keywords);
        var responseJson =
            """{"errorCode":"success","errorInfo":"success","finishReason":"stop","requestId":"req-1","success":true,"text":"客户咨询健康险产品","inputTokens":"10","outputTokens":"20","totalTokens":"30"}""";
        var (client, handler, bodies) = CreateClient(responseJson);

        var response = await client.AnalyzeConversationAsync(WorkspaceId, AppId, request);

        Assert.True(response.Success);
        Assert.Equal("客户咨询健康险产品", response.Text);
        Assert.Equal("req-1", response.RequestId);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/analyze_conversation",
            "AnalyzeConversation");
        AssertCapturedBodyContains(bodies, "\"resultTypes\":[\"summary\",\"title\",\"keywords\"]");
        AssertCapturedBodyContains(bodies, "\"role\":\"agent\"");
    }

    [Fact]
    public async Task AnalyzeConversation_FieldExtractionBestPractice_IncludesFieldsAsync()
    {
        var request = AnalyzeConversationRequest.ForFieldExtraction(
            CreateSampleDialogue(),
            new[]
            {
                new CcaiField { Name = "问题类型", Desc = "客户咨询的问题类型" },
                new CcaiField { Name = "公司名称", Desc = "客服所属的保险公司名称" }
            });
        var (client, handler, bodies) = CreateClient(
            """{"success":true,"text":"{\"问题类型\":\"健康险咨询\"}","requestId":"req-fields"}""");

        var response = await client.AnalyzeConversationAsync(WorkspaceId, AppId, request);

        Assert.Equal("req-fields", response.RequestId);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/analyze_conversation",
            "AnalyzeConversation");
        AssertCapturedBodyContains(bodies, "\"resultTypes\":[\"fields\"]");
        AssertCapturedBodyContains(bodies, "\"name\":\"问题类型\"");
    }

    [Fact]
    public async Task AnalyzeConversation_ServiceInspectionBestPractice_IncludesInspectionPayloadAsync()
    {
        var request = AnalyzeConversationRequest.ForServiceInspection(
            CreateSampleDialogue(),
            new CcaiServiceInspection
            {
                SceneIntroduction = "保险销售场景",
                InspectionIntroduction = "请检测客服是否存在服务不当的行为，包括：过度承诺、故意套取客户隐私信息等",
                InspectionContents = new List<CcaiInspectionContent>
                {
                    new()
                    {
                        Title = "客服是否过度承诺",
                        Content =
                            "客服在服务客户过程中，基于已有的服务标准是否存在过度承诺的行为，如：最快到货时间是12小时，无法给客户承诺更快的到货时间。"
                    },
                    new()
                    {
                        Title = "客户情绪是否正向",
                        Content =
                            "分析对话内容，输出用户在对话中表现出的情绪。"
                    }
                }
            });
        var (client, handler, bodies) = CreateClient(
            """{"success":true,"text":"未发现过度承诺","requestId":"req-qi"}""");

        var response = await client.AnalyzeConversationAsync(WorkspaceId, AppId, request);

        Assert.Equal("未发现过度承诺", response.Text);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/analyze_conversation",
            "AnalyzeConversation");
        AssertCapturedBodyContains(bodies, "\"resultTypes\":[\"service_inspection\"]");
        AssertCapturedBodyContains(bodies, "\"sceneIntroduction\":\"保险销售场景\"");
    }

    [Fact]
    public async Task AnalyzeConversationStream_ParsesSseChunksAsync()
    {
        var sse =
            "data:{\"success\":true,\"text\":\"客\",\"finishReason\":null}\n\n" +
            "data:{\"success\":true,\"text\":\"户咨询\",\"finishReason\":\"stop\",\"requestId\":\"sse-1\"}\n\n";
        var (client, _, _) = CreateClient(sse, "text/event-stream");
        var request = AnalyzeConversationRequest.ForSummary(CreateSampleDialogue());
        request.Stream = true;

        var chunks = await client.AnalyzeConversationStreamAsync(WorkspaceId, AppId, request).ToListAsync();

        Assert.Equal(2, chunks.Count);
        Assert.Equal("客", chunks[0].Text);
        Assert.Equal("户咨询", chunks[1].Text);
        Assert.Equal("stop", chunks[1].FinishReason);
    }

    [Fact]
    public async Task CreateTask_TextSummary_PostsExpectedPathAsync()
    {
        var request = new CcaiCreateTaskRequest
        {
            TaskType = "text",
            ModelCode = "tyxmTurbo",
            Dialogue = CreateSampleDialogue(),
            ResultTypes = new List<string> { CcaiResultTypes.Summary }
        };
        var (client, handler, bodies) = CreateClient(
            """{"data":{"taskId":"task-1"},"requestId":"req-task","success":"True"}""");

        var response = await client.CreateTaskAsync(WorkspaceId, AppId, request);

        Assert.Equal("task-1", response.Data?.TaskId);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/createTask",
            "CreateTask");
        AssertCapturedBodyContains(bodies, "\"taskType\":\"text\"");
    }

    [Fact]
    public async Task GetTaskResult_AppendsQueryParametersAsync()
    {
        var (client, handler, _) = CreateClient(
            """{"data":{"taskId":"task-1","taskStatus":"FINISH","text":"done"},"requestId":"req-get","success":"True"}""");

        var response = await client.GetTaskResultAsync("task-1", new[] { "asr_result" });

        Assert.Equal("FINISH", response.Data?.TaskStatus);
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m =>
                m.Method == HttpMethod.Get
                && m.RequestUri!.PathAndQuery.Contains("/ccai/app/getTaskResult", StringComparison.Ordinal)
                && m.RequestUri.Query.Contains("taskId=task-1", StringComparison.Ordinal)
                && m.RequestUri.Query.Contains("requiredFieldList=asr_result", StringComparison.Ordinal)
                && m.Headers.Contains("Authorization")
                && m.Headers.GetValues("x-acs-action").Single() == "GetTaskResult"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task RunCompletion_UsesPascalCaseBodyAsync()
    {
        var request = new CcaiRunCompletionRequest
        {
            TemplateIds = new List<long> { 47 },
            ModelCode = "tyxmTurbo",
            Stream = false,
            Dialogue = new CcaiRunCompletionDialogue
            {
                SessionId = "session-01",
                Sentences = new List<CcaiRunCompletionSentence>
                {
                    new() { Role = "user", Text = "我要办理信用卡", ChatId = "chat_1" }
                }
            }
        };
        var (client, handler, bodies) = CreateClient(
            """{"FinishReason":"stop","RequestId":"run-1","Text":"ok","inputTokens":"1","outputTokens":"2","totalTokens":"3"}""");

        var response = await client.RunCompletionAsync(WorkspaceId, AppId, request);

        Assert.Equal("ok", response.Text);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/completion",
            "RunCompletion");
        AssertCapturedBodyContains(bodies, "\"Dialogue\":{");
        AssertCapturedBodyContains(bodies, "\"TemplateIds\":[47]");
    }

    [Fact]
    public async Task RunCompletionMessage_UsesPascalCaseMessagesBodyAsync()
    {
        var request = new CcaiRunCompletionMessageRequest
        {
            ModelCode = "tyxmTurbo",
            Stream = false,
            Messages = new List<CcaiCompletionMessage>
            {
                new() { Role = "system", Content = "You are a helpful assistant." },
                new()
                {
                    Role = "user",
                    Content = "请阅读以下对话内容，按照要求执行指令任务。"
                }
            }
        };
        var (client, handler, bodies) = CreateClient(
            """{"FinishReason":"stop","RequestId":"msg-1","Text":"摘要结果","inputTokens":"1","outputTokens":"2","totalTokens":"3"}""");

        var response = await client.RunCompletionMessageAsync(WorkspaceId, AppId, request);

        Assert.Equal("摘要结果", response.Text);
        Assert.Equal("stop", response.FinishReason);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/completion_message",
            "RunCompletionMessage");
        AssertCapturedBodyContains(bodies, "\"Messages\":[");
        AssertCapturedBodyContains(bodies, "\"Role\":\"system\"");
        AssertCapturedBodyContains(bodies, "\"Content\":\"You are a helpful assistant.\"");
    }

    [Fact]
    public async Task RunCompletionMessageStream_ParsesSseChunksAsync()
    {
        var sse =
            "data:{\"FinishReason\":null,\"Text\":\"部\"}\n\n" +
            "data:{\"FinishReason\":\"stop\",\"RequestId\":\"msg-sse\",\"Text\":\"分摘要\"}\n\n";
        var (client, _, _) = CreateClient(sse, "text/event-stream");
        var request = new CcaiRunCompletionMessageRequest
        {
            Messages = new List<CcaiCompletionMessage>
            {
                new() { Role = "user", Content = "总结对话" }
            }
        };

        var chunks = await client.RunCompletionMessageStreamAsync(WorkspaceId, AppId, request).ToListAsync();

        Assert.Equal(2, chunks.Count);
        Assert.Equal("部", chunks[0].Text);
        Assert.Equal("分摘要", chunks[1].Text);
        Assert.Equal("stop", chunks[1].FinishReason);
    }

    [Fact]
    public async Task AnalyzeImage_Watermark_PostsImageUrlsAndResultTypesAsync()
    {
        var request = new CcaiAnalyzeImageRequest
        {
            Stream = false,
            ImageUrls = new List<string> { "https://img.123.com/1.jpg" },
            ResultTypes = new List<string> { CcaiImageResultTypes.Watermark }
        };
        var (client, handler, bodies) = CreateClient(
            """{"requestId":"img-1","success":true,"text":"[{\\\"num\\\":\\\"1\\\",\\\"isHit\\\":\\\"false\\\",\\\"remarks\\\":\\\"无水印\\\"}]","finishReason":"stop","inputTokens":1000,"outputTokens":2000,"totalTokens":3000}""");

        var response = await client.AnalyzeImageAsync(WorkspaceId, AppId, request);

        Assert.True(response.Success);
        Assert.Equal(1000, response.InputTokens);
        Assert.Contains("无水印", response.Text, StringComparison.Ordinal);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/analyzeImage",
            "AnalyzeImage");
        AssertCapturedBodyContains(bodies, "\"resultTypes\":[\"watermark\"]");
        AssertCapturedBodyContains(bodies, "\"imageUrls\":[\"https://img.123.com/1.jpg\"]");
        AssertCapturedBodyContains(bodies, "\"stream\":false");
    }

    [Fact]
    public async Task GeneralAnalyzeImage_CustomPrompt_PostsExpectedBodyAsync()
    {
        var request = new CcaiGeneralAnalyzeImageRequest
        {
            Stream = false,
            ImageUrls = new List<string> { "https://img.123.com/1.jpg" },
            CustomPrompt = "Analyze the content in the image",
            TemplateIds = new List<long> { 34 }
        };
        var (client, handler, bodies) = CreateClient(
            """{"requestId":"gimg-1","success":true,"text":"这张图片中没有可识别的文本内容。","finishReason":"stop","inputTokens":1000,"outputTokens":2000,"totalTokens":3000}""");

        var response = await client.GeneralAnalyzeImageAsync(WorkspaceId, AppId, request);

        Assert.Equal("gimg-1", response.RequestId);
        Assert.Equal(3000, response.TotalTokens);
        AssertSignedRequest(
            handler,
            HttpMethod.Post,
            $"/{WorkspaceId}/ccai/app/{AppId}/generalanalyzeImage",
            "GeneralAnalyzeImage");
        AssertCapturedBodyContains(bodies, "\"customPrompt\":\"Analyze the content in the image\"");
        AssertCapturedBodyContains(bodies, "\"templateIds\":[34]");
    }

    [Fact]
    public async Task AnalyzeImageStream_ParsesSseChunksAsync()
    {
        var sse =
            "data:{\"success\":true,\"text\":\"检测\",\"finishReason\":null}\n\n" +
            "data:{\"success\":true,\"text\":\"完成\",\"finishReason\":\"stop\",\"requestId\":\"img-sse\"}\n\n";
        var (client, _, _) = CreateClient(sse, "text/event-stream");
        var request = new CcaiAnalyzeImageRequest
        {
            Stream = true,
            ImageUrls = new List<string> { "https://img.123.com/1.jpg" },
            ResultTypes = new List<string> { CcaiImageResultTypes.Watermark }
        };

        var chunks = await client.AnalyzeImageStreamAsync(WorkspaceId, AppId, request).ToListAsync();

        Assert.Equal(2, chunks.Count);
        Assert.Equal("检测", chunks[0].Text);
        Assert.Equal("完成", chunks[1].Text);
        Assert.Equal("stop", chunks[1].FinishReason);
    }

    [Fact]
    public async Task AnalyzeConversation_HttpError_ThrowsContactCenterAiExceptionAsync()
    {
        var (client, _, _) = CreateClient(
            """{"errorCode":"CCAI.ParamInvalid.IllegalParamValue","errorInfo":"bad param","requestId":"err-1"}""",
            statusCode: HttpStatusCode.BadRequest);

        var ex = await Assert.ThrowsAsync<ContactCenterAiException>(() =>
            client.AnalyzeConversationAsync(
                WorkspaceId,
                AppId,
                AnalyzeConversationRequest.ForSummary(CreateSampleDialogue())));

        Assert.Equal(400, ex.Status);
        Assert.Equal("CCAI.ParamInvalid.IllegalParamValue", ex.ErrorCode);
        Assert.Equal("bad param", ex.ErrorInfo);
        Assert.Equal("err-1", ex.RequestId);
    }

    [Fact]
    public void AnalyzeConversationRequest_Factories_SetExpectedResultTypes()
    {
        var dialogue = CreateSampleDialogue();
        Assert.Equal(
            new[] { CcaiResultTypes.Summary },
            AnalyzeConversationRequest.ForSummary(dialogue).ResultTypes);
        Assert.Equal(
            new[] { CcaiResultTypes.Fields },
            AnalyzeConversationRequest.ForFieldExtraction(
                dialogue,
                new[] { new CcaiField { Name = "a", Desc = "b" } }).ResultTypes);
        Assert.Equal(
            new[] { CcaiResultTypes.ServiceInspection },
            AnalyzeConversationRequest.ForServiceInspection(
                dialogue,
                new CcaiServiceInspection
                {
                    InspectionContents = new List<CcaiInspectionContent>
                    {
                        new() { Title = "t", Content = "c" }
                    },
                    InspectionIntroduction = "i",
                    SceneIntroduction = "s"
                }).ResultTypes);
    }

    [Fact]
    public async Task CreateVocab_PostsWordWeightListAndReturnsVocabularyIdAsync()
    {
        var request = new CcaiCreateVocabRequest
        {
            WorkspaceId = WorkspaceId,
            Name = "销售词表",
            Description = "东北一区销售业务专用",
            WordWeightList = new List<CcaiWordWeight>
            {
                new() { Word = "儿童", Weight = 3 },
                new() { Word = "金属", Weight = 3 }
            }
        };
        var (client, handler, bodies) = CreateClient(
            """{"requestId":"req-vocab","success":"True","data":{"vocabularyId":"f3d82*******7"}}""");

        var response = await client.CreateVocabAsync(request);

        Assert.Equal("f3d82*******7", response.Data?.VocabularyId);
        AssertSignedRequest(handler, HttpMethod.Post, "/vocab/createVocab", "CreateVocab");
        AssertCapturedBodyContains(bodies, "\"workspaceId\":\"ws-demo\"");
        AssertCapturedBodyContains(bodies, "\"word\":\"儿童\"");
        AssertCapturedBodyContains(bodies, "\"weight\":3");
    }

    [Fact]
    public async Task UpdateVocab_PostsVocabularyIdAndWordsAsync()
    {
        var request = new CcaiUpdateVocabRequest
        {
            WorkspaceId = WorkspaceId,
            VocabularyId = "f3d82e0d********d23bd7",
            Name = "销售热词",
            Description = "南方一区销售热词",
            WordWeightList = new List<CcaiWordWeight>
            {
                new() { Word = "欧洲", Weight = 4 },
                new() { Word = "耳痛", Weight = 2 }
            }
        };
        var (client, handler, bodies) = CreateClient(
            """{"requestId":"req-upd","success":true,"data":true}""");

        var response = await client.UpdateVocabAsync(request);

        Assert.NotNull(response.Data);
        AssertSignedRequest(handler, HttpMethod.Post, "/vocab/updateVocab", "UpdateVocab");
        AssertCapturedBodyContains(bodies, "\"vocabularyId\":\"f3d82e0d********d23bd7\"");
        AssertCapturedBodyContains(bodies, "\"word\":\"欧洲\"");
    }

    [Fact]
    public async Task ListVocab_DeserializesVocabularyArrayAsync()
    {
        var (client, handler, bodies) = CreateClient(
            """{"requestId":"req-list","success":true,"data":[{"vocabularyId":"dv*****erverve","name":"热词1","description":"销售热词","audioModelCode":"nls","wordWeightList":[{"word":"儿童","weight":3}]}]}""");

        var response = await client.ListVocabAsync(new CcaiListVocabRequest { WorkspaceId = WorkspaceId });

        Assert.Single(response.Data!);
        Assert.Equal("热词1", response.Data![0].Name);
        Assert.Equal("儿童", response.Data[0].WordWeightList![0].Word);
        AssertSignedRequest(handler, HttpMethod.Post, "/vocab/listVocab", "ListVocab");
        AssertCapturedBodyContains(bodies, "\"workspaceId\":\"ws-demo\"");
    }

    [Fact]
    public async Task GetVocab_ReturnsVocabularyDetailAsync()
    {
        var (client, handler, bodies) = CreateClient(
            """{"requestId":"req-get","success":true,"data":{"vocabularyId":"rrbe***jrvrdd","name":"热词1","description":"销售热词","audioModelCode":"nls","wordWeightList":[{"word":"儿童","weight":1}]}}""");

        var response = await client.GetVocabAsync(
            new CcaiGetVocabRequest { WorkspaceId = WorkspaceId, VocabularyId = "rrbe***jrvrdd" });

        Assert.Equal("rrbe***jrvrdd", response.Data?.VocabularyId);
        Assert.Equal(1, response.Data?.WordWeightList?[0].Weight);
        AssertSignedRequest(handler, HttpMethod.Post, "/vocab/getVocab", "GetVocab");
        AssertCapturedBodyContains(bodies, "\"vocabularyId\":\"rrbe***jrvrdd\"");
    }

    [Fact]
    public async Task DeleteVocab_PostsIdsAsync()
    {
        var (client, handler, bodies) = CreateClient(
            """{"requestId":"req-del","success":true,"data":true}""");

        var response = await client.DeleteVocabAsync(
            new CcaiDeleteVocabRequest { WorkspaceId = WorkspaceId, VocabularyId = "81a3*********2d7c8" });

        Assert.NotNull(response.Data);
        AssertSignedRequest(handler, HttpMethod.Post, "/vocab/deleteVocab", "DeleteVocab");
        AssertCapturedBodyContains(bodies, "\"vocabularyId\":\"81a3*********2d7c8\"");
    }

    private static CcaiDialogue CreateSampleDialogue()
        => new()
        {
            SessionId = "session-adslsddxxxx",
            Sentences = new List<CcaiSentence>
            {
                new() { Role = "agent", Text = "您好，这里是xxx保险公司，请问有什么可以帮您" },
                new() { Role = "user", Text = "嗯，我想办理一个健康险，帮我介绍下有哪些" }
            }
        };

    private static (ContactCenterAiClient Client, MockHttpMessageHandler Handler, List<string> CapturedBodies)
        CreateClient(
            string responseBody,
            string mediaType = "application/json",
            HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var capturedBodies = new List<string>();
        var handler = Substitute.ForPartsOf<MockHttpMessageHandler>();
        handler.Configure()
            .MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
            .Returns(ci =>
            {
                var message = ci.ArgAt<HttpRequestMessage>(0);
                if (message.Content is not null)
                {
                    capturedBodies.Add(message.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }

                return new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(responseBody, Encoding.UTF8, mediaType)
                };
            });

        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://contactcenterai.cn-shanghai.aliyuncs.com/")
        };
        var client = new ContactCenterAiClient(
            httpClient,
            new ContactCenterAiOptions
            {
                AccessKeyId = "LTAI5tTestAccessKeyId",
                AccessKeySecret = "TestAccessKeySecret",
                Endpoint = "contactcenterai.cn-shanghai.aliyuncs.com"
            });
        return (client, handler, capturedBodies);
    }

    private static void AssertSignedRequest(
        MockHttpMessageHandler handler,
        HttpMethod method,
        string path,
        string action)
    {
        handler.Received().MockSend(
            Arg.Is<HttpRequestMessage>(m =>
                m.Method == method
                && m.RequestUri!.AbsolutePath == path
                && m.Headers.GetValues("x-acs-action").Single() == action
                && m.Headers.Contains("Authorization")
                && m.Headers.Contains("x-acs-content-sha256")
                && m.Headers.Contains("x-acs-date")
                && m.Headers.Contains("x-acs-signature-nonce")
                && m.Headers.Contains("x-acs-version")),
            Arg.Any<CancellationToken>());
    }

    private static void AssertCapturedBodyContains(IReadOnlyList<string> bodies, string expectedBodySubstring)
    {
        Assert.Contains(bodies, body => body.Contains(expectedBodySubstring, StringComparison.Ordinal));
    }
}
