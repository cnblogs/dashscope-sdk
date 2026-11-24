using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class LiveTranslate
    {
        private const string GroupName = "qwen-livetranslate-realtime";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        public static readonly SocketMessageSnapshot FinishSession = new(GroupName, "finish-session");

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ConversationItemCreated = new(
                "qwen-livetranslate-realtime",
                "conversation.item.created",
                new LiveTranslateServerEvent(
                    EventId: "event_UxjmskZ5rgf2WBRh8cWTV",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_Rx37Sl4OTriZNsjBpmKca",
                    Item: new LiveTranslateItem
                    {
                        Id = "item_KEyNgQoCC7qJuIU7R8fc7",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<LiveTranslateItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ConversationItemCreated2 = new(
                "qwen-livetranslate-realtime",
                "conversation.item.created-2",
                new LiveTranslateServerEvent(
                    EventId: "event_VEyBJcnyKQPDuX8wCpxJt",
                    Type: "conversation.item.created",
                    Item: new LiveTranslateItem
                    {
                        Id = "item_Rx37Sl4OTriZNsjBpmKca",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<LiveTranslateItemContent>
                            {
                                new LiveTranslateItemContent(Type: "input_audio")
                            }
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ConversationItemInputAudioTranscriptionCompleted = new(
                "qwen-livetranslate-realtime",
                "conversation.item.input_audio_transcription.completed",
                new LiveTranslateServerEvent(
                    EventId: "event_YNxFFCkpGOQbh42RhmOkm",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_Rx37Sl4OTriZNsjBpmKca",
                    ContentIndex: 0,
                    Transcript: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ConversationItemInputAudioTranscriptionText = new(
                "qwen-livetranslate-realtime",
                "conversation.item.input_audio_transcription.text",
                new LiveTranslateServerEvent(
                    EventId: "event_WY7DID8Kxez1YEPnlzEKK",
                    Type: "conversation.item.input_audio_transcription.text",
                    ItemId: "item_Rx37Sl4OTriZNsjBpmKca",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ConversationItemInputAudioTranscriptionText2 = new(
                "qwen-livetranslate-realtime",
                "conversation.item.input_audio_transcription.text-2",
                new LiveTranslateServerEvent(
                    EventId: "event_DCr2xmqZqSa5WUAP5a6Ja",
                    Type: "conversation.item.input_audio_transcription.text",
                    ItemId: "item_Rx37Sl4OTriZNsjBpmKca",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral"));

        // A response stream timeout captured by the first replay of the VAD group, before the
        // script sent finish-session right after the audio: the server gives up after 61
        // seconds without a finish. Later replays do not produce the event, the fixture is kept
        // for the error path of the session.
        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            Timeout = new(
                "qwen-livetranslate-realtime",
                "error",
                new LiveTranslateServerEvent(
                    EventId: "event_LQSO9sqnQf5aQ2ZvJU8nm",
                    Type: "error",
                    Error: new LiveTranslateError
                    {
                        Code = "COMMON_ERROR",
                        Message = "Response stream timeout (timeout_seconds=61, elapsed_ms=62605)"
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            InputAudioBufferSpeechStarted = new(
                "qwen-livetranslate-realtime",
                "input_audio_buffer.speech_started",
                new LiveTranslateServerEvent(
                    EventId: "event_GeZWpZJGp8FR1rpjjefiF",
                    Type: "input_audio_buffer.speech_started",
                    ItemId: "item_Rx37Sl4OTriZNsjBpmKca",
                    AudioStartMs: 0));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            InputAudioBufferSpeechStopped = new(
                "qwen-livetranslate-realtime",
                "input_audio_buffer.speech_stopped",
                new LiveTranslateServerEvent(
                    EventId: "event_HUx5TJfpfV5USb4uYi5Pp",
                    Type: "input_audio_buffer.speech_stopped",
                    ItemId: "item_Rx37Sl4OTriZNsjBpmKca",
                    AudioEndMs: 3861));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseAudioDelta = new(
                "qwen-livetranslate-realtime",
                "response.audio.delta",
                new LiveTranslateServerEvent(
                    EventId: "event_QADn3lUpV9rjoz6ZEwpKg",
                    Type: "response.audio.delta",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseAudioDelta2 = new(
                "qwen-livetranslate-realtime",
                "response.audio.delta-2",
                new LiveTranslateServerEvent(
                    EventId: "event_RtBBLjZ5QmIDAUeETQIQV",
                    Type: "response.audio.delta",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseAudioDone = new(
                "qwen-livetranslate-realtime",
                "response.audio.done",
                new LiveTranslateServerEvent(
                    EventId: "event_QgiVj75irkFSBmbAwDcpT",
                    Type: "response.audio.done",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53"));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseAudioTranscriptDone = new(
                "qwen-livetranslate-realtime",
                "response.audio_transcript.done",
                new LiveTranslateServerEvent(
                    EventId: "event_WKqkWBkQrby1oVtJYrZDv",
                    Type: "response.audio_transcript.done",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Transcript: "Cnblogs was founded in 2004, code changes the world.  "));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseAudioTranscriptText = new(
                "qwen-livetranslate-realtime",
                "response.audio_transcript.text",
                new LiveTranslateServerEvent(
                    EventId: "event_UoOdsn85kknADHNZ68Wb5",
                    Type: "response.audio_transcript.text",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Text: string.Empty,
                    Stash: "C"));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseAudioTranscriptText2 = new(
                "qwen-livetranslate-realtime",
                "response.audio_transcript.text-2",
                new LiveTranslateServerEvent(
                    EventId: "event_IusaWbQm3AaGBjidxYbjJ",
                    Type: "response.audio_transcript.text",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Text: "Cnblogs was founded in 2004,",
                    Stash: " code changes the world."));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseContentPartAdded = new(
                "qwen-livetranslate-realtime",
                "response.content_part.added",
                new LiveTranslateServerEvent(
                    EventId: "event_TeB6YQavg3wY72kc743A2",
                    Type: "response.content_part.added",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Part: new LiveTranslateItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseContentPartDone = new(
                "qwen-livetranslate-realtime",
                "response.content_part.done",
                new LiveTranslateServerEvent(
                    EventId: "event_SsgLfe2k2yYkhs8Ty5wxr",
                    Type: "response.content_part.done",
                    ItemId: "item_KEyNgQoCC7qJuIU7R8fc7",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Part: new LiveTranslateItemContent(
                        Type: "audio",
                        Text: "Cnblogs was founded in 2004, code changes the world.  ")));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseCreated = new(
                "qwen-livetranslate-realtime",
                "response.created",
                new LiveTranslateServerEvent(
                    EventId: "event_LDWyICngDazfOrYLCzct8",
                    Type: "response.created",
                    Response: new LiveTranslateResponse
                    {
                        Id = "resp_RHvg4yDKEAEVTVnaMlc53",
                        Object = "realtime.response",
                        ConversationId = string.Empty,
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<LiveTranslateItem>()
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseDone = new(
                "qwen-livetranslate-realtime",
                "response.done",
                new LiveTranslateServerEvent(
                    EventId: "event_ZL1QhLHkjtlXWRVOuvZQw",
                    Type: "response.done",
                    Response: new LiveTranslateResponse
                    {
                        Id = "resp_RHvg4yDKEAEVTVnaMlc53",
                        Object = "realtime.response",
                        ConversationId = "conv_WjXIvHCgVSTtSdYLaZTHO",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<LiveTranslateItem>
                        {
                            new LiveTranslateItem
                            {
                                Id = "item_KEyNgQoCC7qJuIU7R8fc7",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<LiveTranslateItemContent>
                                {
                                    new LiveTranslateItemContent(
                                        Type: "audio",
                                        Transcript: "Cnblogs was founded in 2004, code changes the world.  ")
                                }
                            }
                        },
                        Usage = new LiveTranslateUsage
                        {
                            TotalTokens = 212,
                            InputTokens = 131,
                            OutputTokens = 81,
                            InputTokensDetails = new LiveTranslateTokenDetails { TextTokens = 103, AudioTokens = 28 },
                            OutputTokensDetails = new LiveTranslateTokenDetails { TextTokens = 28, AudioTokens = 53 }
                        }
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseOutputItemAdded = new(
                "qwen-livetranslate-realtime",
                "response.output_item.added",
                new LiveTranslateServerEvent(
                    EventId: "event_O4pdwQEXyBp8zApp5JB8v",
                    Type: "response.output_item.added",
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Item: new LiveTranslateItem
                    {
                        Id = "item_KEyNgQoCC7qJuIU7R8fc7",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<LiveTranslateItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            ResponseOutputItemDone = new(
                "qwen-livetranslate-realtime",
                "response.output_item.done",
                new LiveTranslateServerEvent(
                    EventId: "event_Zc2FYCykuUWepmAwDG8vS",
                    Type: "response.output_item.done",
                    OutputIndex: 0,
                    ResponseId: "resp_RHvg4yDKEAEVTVnaMlc53",
                    Item: new LiveTranslateItem
                    {
                        Id = "item_KEyNgQoCC7qJuIU7R8fc7",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<LiveTranslateItemContent>
                        {
                            new LiveTranslateItemContent(
                                Type: "audio",
                                Text: "Cnblogs was founded in 2004, code changes the world.  ")
                        }
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            SessionCreated = new(
                "qwen-livetranslate-realtime",
                "session.created",
                new LiveTranslateServerEvent(
                    EventId: "event_U7HJzT8ykVRGM75m5IeUI",
                    Type: "session.created",
                    Session: new LiveTranslateSession
                    {
                        Id = "sess_VMwU3DNx5nbyoK8jsWNRQ",
                        Object = "realtime.session",
                        Model = "qwen3.5-livetranslate-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        SampleRate = 16000,
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new LiveTranslateTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.2f,
                            SilenceDurationMs = 1000
                        },
                        Translation = new LiveTranslateTranslation { Language = "en" }
                    }));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            SessionFinished = new(
                "qwen-livetranslate-realtime",
                "session.finished",
                new LiveTranslateServerEvent(
                    EventId: "event_YYWitTjmMOJkNrX49uqwE",
                    Type: "session.finished"));

        public static readonly SocketMessageSnapshot<LiveTranslateServerEvent>
            SessionUpdated = new(
                "qwen-livetranslate-realtime",
                "session.updated",
                new LiveTranslateServerEvent(
                    EventId: "event_O77aWTOJ3EzFnGheixf2l",
                    Type: "session.updated",
                    Session: new LiveTranslateSession
                    {
                        Id = "sess_VMwU3DNx5nbyoK8jsWNRQ",
                        Object = "realtime.session",
                        Model = "qwen3.5-livetranslate-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        SampleRate = 16000,
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        InputAudioTranscription =
                            new LiveTranslateTranscription(Model: "qwen3-asr-flash-realtime", Language: "zh"),
                        TurnDetection = new LiveTranslateTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.2f,
                            SilenceDurationMs = 1000,
                            CreateResponse = true,
                            InterruptResponse = true
                        },
                        Translation = new LiveTranslateTranslation
                        {
                            Language = "en",
                            Corpus = new LiveTranslateCorpus
                            {
                                Phrases = new Dictionary<string, string> { ["博客园"] = "Cnblogs" }
                            }
                        }
                    }));
    }
}
