using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    /// <summary>
    /// The omni realtime group "qwen-omni-realtime" (VAD mode: the server detects the speech, commits the audio and answers in text and audio, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtime
    {
        private const string GroupName = "qwen-omni-realtime";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_Wlheg1iuy6FicJuKxKppj",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_P5UBjFSatjYjp2QfJoULj",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_MzJSGnrmpQ6uykNtHfhBY",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_P5UBjFSatjYjp2QfJoULj",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_IGKcsW8RQfcQcTAzct5ou",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionCompleted = new(
            GroupName,
            "conversation.item.input_audio_transcription.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_UnMKEtbN6hLRN57YiveRv",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_P5UBjFSatjYjp2QfJoULj",
                    ContentIndex: 0,
                    Transcript: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_LWJmHvVwwY36j5hjp6KIa",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_P5UBjFSatjYjp2QfJoULj",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "伯",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "UjeMT767wBq9s7"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta2 = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_RZQYHenwRJueJxNDT3Giv",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_P5UBjFSatjYjp2QfJoULj",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "DCYWRrfNsfR1fy"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferCommitted = new(
            GroupName,
            "input_audio_buffer.committed",
            new OmniRealtimeServerEvent(
                    EventId: "event_QFuNJGKkILZD2Wmg6MOXf",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_P5UBjFSatjYjp2QfJoULj"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferSpeechStarted = new(
            GroupName,
            "input_audio_buffer.speech_started",
            new OmniRealtimeServerEvent(
                    EventId: "event_YkfMyOxAxZynaJvteXgJl",
                    Type: "input_audio_buffer.speech_started",
                    ItemId: "item_P5UBjFSatjYjp2QfJoULj",
                    AudioStartMs: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferSpeechStopped = new(
            GroupName,
            "input_audio_buffer.speech_stopped",
            new OmniRealtimeServerEvent(
                    EventId: "event_Z3mOmNrvCvVGmk5qseQiY",
                    Type: "input_audio_buffer.speech_stopped",
                    ItemId: "item_P5UBjFSatjYjp2QfJoULj",
                    AudioEndMs: 3620));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta = new(
            GroupName,
            "response.audio.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_ASX2iuZxjjpChTKzppxGD",
                    Type: "response.audio.delta",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta2 = new(
            GroupName,
            "response.audio.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_KPmWe5e2VUVfjlyhzDjX9",
                    Type: "response.audio.delta",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDone = new(
            GroupName,
            "response.audio.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_IqOBEMmfipLBkuecudeNl",
                    Type: "response.audio.done",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta = new(
            GroupName,
            "response.audio_transcript.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_Awc8My7aUil8YvtTlPp4E",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "博客"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta2 = new(
            GroupName,
            "response.audio_transcript.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_EnMvahXmHXZ8JigLGZYWm",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "代码改变世界"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDone = new(
            GroupName,
            "response.audio_transcript.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_KrBykx65XkJHzcpTMN2CD",
                    Type: "response.audio_transcript.done",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Transcript: "博客园成立于2004年，代码改变世界"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartAdded = new(
            GroupName,
            "response.content_part.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_BU5vOmaHiCGkEG3oSuXUy",
                    Type: "response.content_part.added",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartDone = new(
            GroupName,
            "response.content_part.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_CAPOJPW4bZQJmXTA1n1eN",
                    Type: "response.content_part.done",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    ItemId: "item_IGKcsW8RQfcQcTAzct5ou",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: "博客园成立于2004年，代码改变世界")));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_ZpOHVWPoMm2xGPplYu4r9",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_Cji2pDtIdPEtHWCNdT7eH",
                        Object = "realtime.response",
                        ConversationId = "conv_Y2Ho8bfyl2sXVL3ZB5ojl",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_FVvrra7yLHlF1DvSG61WK",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_Cji2pDtIdPEtHWCNdT7eH",
                        Object = "realtime.response",
                        ConversationId = "conv_Y2Ho8bfyl2sXVL3ZB5ojl",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_IGKcsW8RQfcQcTAzct5ou",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "audio", Transcript: "博客园成立于2004年，代码改变世界") }
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 809,
                            InputTokens = 742,
                            OutputTokens = 67,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 714,
                                AudioTokens = 28
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 13,
                                AudioTokens = 54
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_UEzeNPsIVS4DpBI1uiVqJ",
                    Type: "response.output_item.added",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_IGKcsW8RQfcQcTAzct5ou",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_KzFUojtKT93UFZf5IWXGS",
                    Type: "response.output_item.done",
                    ResponseId: "resp_Cji2pDtIdPEtHWCNdT7eH",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_IGKcsW8RQfcQcTAzct5ou",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_Bzv5GFTKVoS3bu6Bj8X4a",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_SDqIaRocGWnUobS28h9zy",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_DVmIU56GHmPSfqLkw7hbn",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_SDqIaRocGWnUobS28h9zy",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        Instructions = "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-manual" (Manual mode with the full session configuration, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeManual
    {
        private const string GroupName = "qwen-omni-realtime-manual";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");
        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");
        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_LLOw4S5MJmSbPRTcckQwD",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_NlinGHfGYmyCdWjS8LKmM",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_PFKFeazsAnFyeVqHTqSFr",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_NlinGHfGYmyCdWjS8LKmM",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_REO2eD2YliOS24FQbbDuu",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionCompleted = new(
            GroupName,
            "conversation.item.input_audio_transcription.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_WwuhYP4UGztARmw4rrCXM",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_NlinGHfGYmyCdWjS8LKmM",
                    ContentIndex: 0,
                    Transcript: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_DtiV7A5w1gSYT3ATEraHx",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_NlinGHfGYmyCdWjS8LKmM",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "DExozH4xHDOurS"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta2 = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_STEASuuyjALCAwtOQVvrA",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_NlinGHfGYmyCdWjS8LKmM",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "Pa58ZLiyToEEd5"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferCommitted = new(
            GroupName,
            "input_audio_buffer.committed",
            new OmniRealtimeServerEvent(
                    EventId: "event_ICYFlmW538vDdvWOrfvhf",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_NlinGHfGYmyCdWjS8LKmM"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta = new(
            GroupName,
            "response.audio.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_J29RQbcEJullARVISXTxF",
                    Type: "response.audio.delta",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta2 = new(
            GroupName,
            "response.audio.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_VMaXk6t5o268DeAkgFOWT",
                    Type: "response.audio.delta",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDone = new(
            GroupName,
            "response.audio.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_DjizEV3YiGn1QqVRcvttX",
                    Type: "response.audio.done",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta = new(
            GroupName,
            "response.audio_transcript.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_Dqe5hzpIXDe3fQYKrRok8",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "博"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta2 = new(
            GroupName,
            "response.audio_transcript.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_KMJKqKIBwD3g37niGilWN",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "的发展。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDone = new(
            GroupName,
            "response.audio_transcript.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_HK4mLsvMselsiD9HVSQ9n",
                    Type: "response.audio_transcript.done",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Transcript: "博科园（BokeYuan）确实成立于2004年，是一家专注于科技、教育、学术和知识分享的平台。其理念“代码改变世界”体现了对技术创新和编程价值的重视。博科园致力于传播科学知识，推动学术交流，并通过内容创作和技术开发促进数字化学习的发展。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartAdded = new(
            GroupName,
            "response.content_part.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_RV1uoklSY4jI8hZVyKLWZ",
                    Type: "response.content_part.added",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartDone = new(
            GroupName,
            "response.content_part.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_RGuX5ksDAfj71VszYXk7F",
                    Type: "response.content_part.done",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    ItemId: "item_REO2eD2YliOS24FQbbDuu",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(
                        Type: "audio",
                        Text: "博科园（BokeYuan）确实成立于2004年，是一家专注于科技、教育、学术和知识分享的平台。其理念“代码改变世界”体现了对技术创新和编程价值的重视。博科园致力于传播科学知识，推动学术交流，并通过内容创作和技术开发促进数字化学习的发展。")));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_HVqhJxhQaqFbXD8knViFd",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_YsQWYrYCQKMkksnBJdpnA",
                        Object = "realtime.response",
                        ConversationId = "conv_NGybH6KXZyF5146FCJL7m",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Serena",
                        OutputAudioFormat = "wav",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_YnbbrAsqjcwYuKTWhgoXH",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_YsQWYrYCQKMkksnBJdpnA",
                        Object = "realtime.response",
                        ConversationId = "conv_NGybH6KXZyF5146FCJL7m",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Serena",
                        OutputAudioFormat = "wav",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_REO2eD2YliOS24FQbbDuu",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<OmniRealtimeItemContent>
                                {
                                    new OmniRealtimeItemContent(
                                        Type: "audio",
                                        Transcript: "博科园（BokeYuan）确实成立于2004年，是一家专注于科技、教育、学术和知识分享的平台。其理念“代码改变世界”体现了对技术创新和编程价值的重视。博科园致力于传播科学知识，推动学术交流，并通过内容创作和技术开发促进数字化学习的发展。")
                                }
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 1103,
                            InputTokens = 742,
                            OutputTokens = 361,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 714,
                                AudioTokens = 28
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 66,
                                AudioTokens = 295
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_GWu4OsYuDHYu3ZPxMZIdK",
                    Type: "response.output_item.added",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_REO2eD2YliOS24FQbbDuu",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_VFEo1iVOSUkOkMxLvTBF2",
                    Type: "response.output_item.done",
                    ResponseId: "resp_YsQWYrYCQKMkksnBJdpnA",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_REO2eD2YliOS24FQbbDuu",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_TKMCoSdD5DBrOxMQqsFO5",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_QQmVFPo1zH4YoBNUT8Xid",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_GGBz7aoDjyT8l7rrc2g8M",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_QQmVFPo1zH4YoBNUT8Xid",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Serena",
                        Audio = new OmniRealtimeAudio(
                            Input: new OmniRealtimeAudioInput(
                                Format: new OmniRealtimeAudioFormat(
                                    Type: "pcm",
                                    SampleRate: 16000,
                                    SampleFormat: "s16le",
                                    Channels: 1,
                                    Packing: "interleaved",
                                    ChannelLayout: "mono")),
                            Output: new OmniRealtimeAudioOutput(Format: new OmniRealtimeAudioFormat(Type: "wav", SampleRate: 24000), Voice: "Serena")),
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        Instructions = "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。",
                        TurnDetection = null,
                        EnableSearch = false,
                        SearchOptions = new OmniRealtimeSearchOptions(EnableSource: true),
                        Video = new OmniRealtimeVideo(Input: new OmniRealtimeVideoInput(RepresentationCompact: "none")),
                        Temperature = 0.6f,
                        TopP = 0.95f,
                        TopK = 20,
                        MaxTokens = 16384,
                        RepetitionPenalty = 0.0f,
                        PresencePenalty = 1.0f,
                        Seed = 1314
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-image" (Manual mode with an image appended after the audio, the input transcription is off, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeImage
    {
        private const string GroupName = "qwen-omni-realtime-image";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");
        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");
        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_FjR6NeTBQRjeksZYgz3LL",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_OC3MeaGHm1xIouJeEwG5s",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Y6QAjLpbiUhk3mkgFvpPI",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_OC3MeaGHm1xIouJeEwG5s",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_JQ7qqp8iMyd6DhgV6wJO7",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferCommitted = new(
            GroupName,
            "input_audio_buffer.committed",
            new OmniRealtimeServerEvent(
                    EventId: "event_OVrEwE7tWEKyjoAn8NCNd",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_OC3MeaGHm1xIouJeEwG5s"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta = new(
            GroupName,
            "response.audio.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_RNTnTkwrlCP15u21BUXeF",
                    Type: "response.audio.delta",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta2 = new(
            GroupName,
            "response.audio.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_W6LnOqLzDRJQ78pPElXW6",
                    Type: "response.audio.delta",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDone = new(
            GroupName,
            "response.audio.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_J2gj5IrsjDFrA3UZUtD4B",
                    Type: "response.audio.done",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta = new(
            GroupName,
            "response.audio_transcript.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_ZdKw6uLAYoBu5nPWMyY8g",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "图片"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta2 = new(
            GroupName,
            "response.audio_transcript.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Zj2kdi3H8UQodwO9p4nT1",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDone = new(
            GroupName,
            "response.audio_transcript.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_SukdOJb6F1oeXXXbScLEc",
                    Type: "response.audio_transcript.done",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Transcript: "图片里显示的编程语言有 Python 和 Java，另外还有一个 curl。不过严格来说，curl 不是编程语言，而是一个命令行工具，用来发送 HTTP 请求的。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartAdded = new(
            GroupName,
            "response.content_part.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_JnKuDHzn2GhKm2f6sYbFS",
                    Type: "response.content_part.added",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartDone = new(
            GroupName,
            "response.content_part.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_S3mu3PwMdRhR5pqOgzukq",
                    Type: "response.content_part.done",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    ItemId: "item_JQ7qqp8iMyd6DhgV6wJO7",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(
                        Type: "audio",
                        Text: "图片里显示的编程语言有 Python 和 Java，另外还有一个 curl。不过严格来说，curl 不是编程语言，而是一个命令行工具，用来发送 HTTP 请求的。")));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_GBS2Yo6ctyrLfZlaR1gUR",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_WbNshDC2IEL8478hID4hU",
                        Object = "realtime.response",
                        ConversationId = "conv_LFnCq95e4b8OS7Z3LJ4yW",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_XhSYlbZVPEzv1mwRdhO7s",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_WbNshDC2IEL8478hID4hU",
                        Object = "realtime.response",
                        ConversationId = "conv_LFnCq95e4b8OS7Z3LJ4yW",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_JQ7qqp8iMyd6DhgV6wJO7",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<OmniRealtimeItemContent>
                                {
                                    new OmniRealtimeItemContent(
                                        Type: "audio",
                                        Transcript: "图片里显示的编程语言有 Python 和 Java，另外还有一个 curl。不过严格来说，curl 不是编程语言，而是一个命令行工具，用来发送 HTTP 请求的。")
                                }
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 1067,
                            InputTokens = 865,
                            OutputTokens = 202,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 716,
                                AudioTokens = 21,
                                VideoTokens = 128
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 36,
                                AudioTokens = 166
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_HZhRTKnazWprnQJ6QmqV5",
                    Type: "response.output_item.added",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_JQ7qqp8iMyd6DhgV6wJO7",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_YSVlr42YjyI12wl4amRwR",
                    Type: "response.output_item.done",
                    ResponseId: "resp_WbNshDC2IEL8478hID4hU",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_JQ7qqp8iMyd6DhgV6wJO7",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_JFITHpdFE2pLb4jdXi4JY",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_FyrQ6DhdFKBLBTA2QSab4",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_WJwCAgU6p9UJAKIVlqZjy",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_FyrQ6DhdFKBLBTA2QSab4",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = null,
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        Instructions = "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。",
                        TurnDetection = null
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-text" (Manual mode, the turn is a text message item instead of audio, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeText
    {
        private const string GroupName = "qwen-omni-realtime-text";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");
        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");
        public static readonly SocketMessageSnapshot ItemCreate = new(GroupName, "item-create");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_OMYi1RZucZ56ppW6QhD5U",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_M3D7T2szgRp5oqPnhiOYq",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_text", Text: "用一句话介绍博客园。") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_J5A5muxa85hAA8r7KgPGA",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_M3D7T2szgRp5oqPnhiOYq",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_MTa7tK1eCs9feg3otIkj9",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta = new(
            GroupName,
            "response.audio.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_H2kMNHSFPl8Faxw7rmb5s",
                    Type: "response.audio.delta",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta2 = new(
            GroupName,
            "response.audio.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_U5abwhpupvAx8IZ952FSj",
                    Type: "response.audio.delta",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDone = new(
            GroupName,
            "response.audio.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_Kkr3N8ghvMiwOZgVgcRRH",
                    Type: "response.audio.done",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta = new(
            GroupName,
            "response.audio_transcript.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_WTDqUCGWHHraR5oyzmewr",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "博客"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta2 = new(
            GroupName,
            "response.audio_transcript.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Krar8LR6jxby5U6RL3wNw",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "等功能。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDone = new(
            GroupName,
            "response.audio_transcript.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_FrjxO4dtLRJsZ8jWSQxqb",
                    Type: "response.audio_transcript.done",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Transcript: "博客园是一个面向开发者的技术分享平台，提供博客、讨论区和知识库等功能。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartAdded = new(
            GroupName,
            "response.content_part.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_GWopTn9tMewE17x7liZco",
                    Type: "response.content_part.added",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartDone = new(
            GroupName,
            "response.content_part.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_YNhGbgUWXLmuUjIJFFbv8",
                    Type: "response.content_part.done",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    ItemId: "item_MTa7tK1eCs9feg3otIkj9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: "博客园是一个面向开发者的技术分享平台，提供博客、讨论区和知识库等功能。")));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_LzXUb2mbnunRy35C2nCF1",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_VGDm61BljOn12Td9kqaet",
                        Object = "realtime.response",
                        ConversationId = "conv_Ml2OqlHIE89UKrV4fVb5J",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_VL4ONcx63Jzyb4PKWtpQV",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_VGDm61BljOn12Td9kqaet",
                        Object = "realtime.response",
                        ConversationId = "conv_Ml2OqlHIE89UKrV4fVb5J",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_MTa7tK1eCs9feg3otIkj9",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<OmniRealtimeItemContent>
                                {
                                    new OmniRealtimeItemContent(Type: "audio", Transcript: "博客园是一个面向开发者的技术分享平台，提供博客、讨论区和知识库等功能。")
                                }
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 827,
                            InputTokens = 716,
                            OutputTokens = 111,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 716
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 20,
                                AudioTokens = 91
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_AnhxLrBGNjOxSimLekS2J",
                    Type: "response.output_item.added",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_MTa7tK1eCs9feg3otIkj9",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_Rg8ctmDIVXR5r9QQJWJmf",
                    Type: "response.output_item.done",
                    ResponseId: "resp_VGDm61BljOn12Td9kqaet",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_MTa7tK1eCs9feg3otIkj9",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_IpvXNhKhMRrMsYvLWStIf",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_A8Gd8MT9O6mTcTxDRJ6vF",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_SacoiGmjNvQRlChDJPgqi",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_A8Gd8MT9O6mTcTxDRJ6vF",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        Instructions = "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。",
                        TurnDetection = null
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-function-call" (Manual mode with a function tool, the answer is text only, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeFunctionCall
    {
        private const string GroupName = "qwen-omni-realtime-function-call";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");
        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");
        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");
        public static readonly SocketMessageSnapshot ItemCreate = new(GroupName, "item-create");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_HDN8rECMoUPWOHlvrOQao",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_CX12osjWdVgf3ujZvahwC",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_FZVsAOKXuAfljOKb1RGC8",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_CX12osjWdVgf3ujZvahwC",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_XXvrKWGCNOc6BNW8xLslY",
                        Object = "realtime.item",
                        Type = "function_call",
                        Status = "in_progress",
                        CallId = "call_b673370472b141f992d989c0",
                        Name = "get_current_weather",
                        Arguments = string.Empty
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated3 = new(
            GroupName,
            "conversation.item.created-3",
            new OmniRealtimeServerEvent(
                    EventId: "event_SVweYv9QBnW1SaDpZ9EQl",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_XXvrKWGCNOc6BNW8xLslY",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_S3UclblT2mqIHomcZgmZv",
                        Object = "realtime.item",
                        Type = "function_call_output",
                        Status = "completed",
                        CallId = "call_b673370472b141f992d989c0",
                        Output = "北京今天天气为霾转晴，气温4/-4℃，微风"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated4 = new(
            GroupName,
            "conversation.item.created-4",
            new OmniRealtimeServerEvent(
                    EventId: "event_VcGdozLa7rX2hBb26jAWI",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_S3UclblT2mqIHomcZgmZv",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_Srec2XQwWXIgM32PhB1p9",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionCompleted = new(
            GroupName,
            "conversation.item.input_audio_transcription.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_NcMtpgoVgOaGZQcmUf4yR",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_CX12osjWdVgf3ujZvahwC",
                    ContentIndex: 0,
                    Transcript: "北京今天天气怎么样？",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_BrbQIFZ7bJHu2Mk3UqTt7",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_CX12osjWdVgf3ujZvahwC",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "北京",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "PX8LJmBfeEdnkw"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta2 = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_OsghvPMahIPDWAdSWwJxB",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_CX12osjWdVgf3ujZvahwC",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "北京今天天气怎么样？",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "MgfWKuOsEiuf8O"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferCommitted = new(
            GroupName,
            "input_audio_buffer.committed",
            new OmniRealtimeServerEvent(
                    EventId: "event_W62pxJfTOnEMLt8zxAcEn",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_CX12osjWdVgf3ujZvahwC"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartAdded = new(
            GroupName,
            "response.content_part.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_Ik2Fox48xCkrvnyMJEOLe",
                    Type: "response.content_part.added",
                    ResponseId: "resp_GWgnMz5XdyWpaaC7KQvyz",
                    ItemId: "item_Srec2XQwWXIgM32PhB1p9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "text", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartDone = new(
            GroupName,
            "response.content_part.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_VW1Hgd5jVSi2PjCwBZVxB",
                    Type: "response.content_part.done",
                    ResponseId: "resp_GWgnMz5XdyWpaaC7KQvyz",
                    ItemId: "item_Srec2XQwWXIgM32PhB1p9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "text", Text: "北京今天天气为霾转晴，气温在4℃到-4℃之间，伴有微风。建议您外出时注意保暖，并关注空气质量变化。")));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_Yq37KSspm3ZSTemt2WLEw",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_ZIkMoJbS4q5xHIj4agnKJ",
                        Object = "realtime.response",
                        ConversationId = "conv_UG8fYiETQo7fXVKF3ZkHI",
                        Status = "in_progress",
                        Modalities = new List<string> { "text" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated2 = new(
            GroupName,
            "response.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_ZsmipESCPvWOTEe3Kcy5D",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_GWgnMz5XdyWpaaC7KQvyz",
                        Object = "realtime.response",
                        ConversationId = "conv_UG8fYiETQo7fXVKF3ZkHI",
                        Status = "in_progress",
                        Modalities = new List<string> { "text" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_MpDiLEnKracAg7ZQKJtS7",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_ZIkMoJbS4q5xHIj4agnKJ",
                        Object = "realtime.response",
                        ConversationId = "conv_UG8fYiETQo7fXVKF3ZkHI",
                        Status = "completed",
                        Modalities = new List<string> { "text" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_XXvrKWGCNOc6BNW8xLslY",
                                Object = "realtime.item",
                                Type = "function_call",
                                Status = "completed",
                                CallId = "call_b673370472b141f992d989c0",
                                Name = "get_current_weather",
                                Arguments = "{\"location\": \"北京\"}"
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 359,
                            InputTokens = 332,
                            OutputTokens = 27,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 318,
                                AudioTokens = 14
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 27
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone2 = new(
            GroupName,
            "response.done-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_FHPfnQ1bK3s4l8pZZ7NRz",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_GWgnMz5XdyWpaaC7KQvyz",
                        Object = "realtime.response",
                        ConversationId = "conv_UG8fYiETQo7fXVKF3ZkHI",
                        Status = "completed",
                        Modalities = new List<string> { "text" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_Srec2XQwWXIgM32PhB1p9",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<OmniRealtimeItemContent>
                                {
                                    new OmniRealtimeItemContent(Type: "text", Text: "北京今天天气为霾转晴，气温在4℃到-4℃之间，伴有微风。建议您外出时注意保暖，并关注空气质量变化。")
                                }
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 424,
                            InputTokens = 391,
                            OutputTokens = 33,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 377,
                                AudioTokens = 14
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 33
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseFunctionCallArgumentsDelta = new(
            GroupName,
            "response.function_call_arguments.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_OsxTu8V3x6zbIMcnXcj4f",
                    Type: "response.function_call_arguments.delta",
                    ResponseId: "resp_ZIkMoJbS4q5xHIj4agnKJ",
                    ItemId: "item_XXvrKWGCNOc6BNW8xLslY",
                    OutputIndex: 0,
                    Delta: "{\"location\": \"北京",
                    CallId: "call_b673370472b141f992d989c0",
                    Obfuscation: "Olt7NQkzxFp1p2"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseFunctionCallArgumentsDelta2 = new(
            GroupName,
            "response.function_call_arguments.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Idu5cmFwYKMfCsqnJaayM",
                    Type: "response.function_call_arguments.delta",
                    ResponseId: "resp_ZIkMoJbS4q5xHIj4agnKJ",
                    ItemId: "item_XXvrKWGCNOc6BNW8xLslY",
                    OutputIndex: 0,
                    Delta: "}",
                    CallId: "call_b673370472b141f992d989c0",
                    Obfuscation: "JNIxuc7xjO7PM6"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseFunctionCallArgumentsDone = new(
            GroupName,
            "response.function_call_arguments.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_LPnVS5r2w9X5Ak4hIxKjO",
                    Type: "response.function_call_arguments.done",
                    ResponseId: "resp_ZIkMoJbS4q5xHIj4agnKJ",
                    ItemId: "item_XXvrKWGCNOc6BNW8xLslY",
                    OutputIndex: 0,
                    CallId: "call_b673370472b141f992d989c0",
                    Name: "get_current_weather",
                    Arguments: "{\"location\": \"北京\"}"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_GiL6MhJX1FalwBzW5aVAe",
                    Type: "response.output_item.added",
                    ResponseId: "resp_ZIkMoJbS4q5xHIj4agnKJ",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_XXvrKWGCNOc6BNW8xLslY",
                        Object = "realtime.item",
                        Type = "function_call",
                        Status = "in_progress",
                        CallId = "call_b673370472b141f992d989c0",
                        Name = "get_current_weather",
                        Arguments = string.Empty
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded2 = new(
            GroupName,
            "response.output_item.added-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_XLrhDW9gWjm7IGJnFPm1A",
                    Type: "response.output_item.added",
                    ResponseId: "resp_GWgnMz5XdyWpaaC7KQvyz",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_Srec2XQwWXIgM32PhB1p9",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_DA4ao6YMLiYo89ldXVb9W",
                    Type: "response.output_item.done",
                    ResponseId: "resp_ZIkMoJbS4q5xHIj4agnKJ",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_XXvrKWGCNOc6BNW8xLslY",
                        Object = "realtime.item",
                        Type = "function_call",
                        Status = "completed",
                        CallId = "call_b673370472b141f992d989c0",
                        Name = "get_current_weather",
                        Arguments = "{\"location\": \"北京\"}"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone2 = new(
            GroupName,
            "response.output_item.done-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_EaLRZT41H7RQdqcxsad8z",
                    Type: "response.output_item.done",
                    ResponseId: "resp_GWgnMz5XdyWpaaC7KQvyz",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_Srec2XQwWXIgM32PhB1p9",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "text") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseTextDelta = new(
            GroupName,
            "response.text.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_Jr989M6Xxt4BGwcQG7Jmt",
                    Type: "response.text.delta",
                    ResponseId: "resp_GWgnMz5XdyWpaaC7KQvyz",
                    ItemId: "item_Srec2XQwWXIgM32PhB1p9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "北京"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseTextDelta2 = new(
            GroupName,
            "response.text.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Uu4xZsJiPbE3b94BkaIWi",
                    Type: "response.text.delta",
                    ResponseId: "resp_GWgnMz5XdyWpaaC7KQvyz",
                    ItemId: "item_Srec2XQwWXIgM32PhB1p9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseTextDone = new(
            GroupName,
            "response.text.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_UxGjtRTCFcF9zz2JztbHc",
                    Type: "response.text.done",
                    ResponseId: "resp_GWgnMz5XdyWpaaC7KQvyz",
                    ItemId: "item_Srec2XQwWXIgM32PhB1p9",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Text: "北京今天天气为霾转晴，气温在4℃到-4℃之间，伴有微风。建议您外出时注意保暖，并关注空气质量变化。"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_JNKGAIwP5MFjNzMSX26kn",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_GtCDpd9g88sZa7lWKEwJd",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_LwaiPNY96ekbi7dYuQUZH",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_GtCDpd9g88sZa7lWKEwJd",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        Instructions = "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。",
                        TurnDetection = null,
                        Tools = new List<OmniRealtimeTool>
                        {
                            new OmniRealtimeTool(
                                Type: "function",
                                Function: new OmniRealtimeFunction(
                                    Name: "get_current_weather",
                                    Description: "当你想查询指定城市的天气时非常有用。",
                                    Parameters: JsonSerializer.SerializeToElement(
                                        new
                                        {
                                            type = "object",
                                            properties = new { location = new { type = "string", description = "城市或县区，比如北京市、杭州市、余杭区等。" } },
                                            required = new[] { "location" }
                                        })))
                        }
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-cancel" (Manual mode, the response in progress is cancelled, captured with qwen3.5-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeCancel
    {
        private const string GroupName = "qwen-omni-realtime-cancel";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");
        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");
        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");
        public static readonly SocketMessageSnapshot ResponseCancel = new(GroupName, "response-cancel");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_GFArwFgPtXAa29RqJJmvx",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_UkrFqqiDMOV7s5I6Zgbsa",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_BWH46Af1L3wActbvaSqzM",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_UkrFqqiDMOV7s5I6Zgbsa",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_U5FTiaNPgU26IudmhsF8R",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionCompleted = new(
            GroupName,
            "conversation.item.input_audio_transcription.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_Mu3w2zziO2jvVAOmMTexK",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_UkrFqqiDMOV7s5I6Zgbsa",
                    ContentIndex: 0,
                    Transcript: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_OMNY8cuvHc5Wv4mOOsVaa",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_UkrFqqiDMOV7s5I6Zgbsa",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "IhR58NsSqxWaqB"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta2 = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_EkedJttJWQwMsgnGEdmqe",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_UkrFqqiDMOV7s5I6Zgbsa",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客园成立于二零零四年，代码改变世界。",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "Isjpa92YHxEuRB"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferCommitted = new(
            GroupName,
            "input_audio_buffer.committed",
            new OmniRealtimeServerEvent(
                    EventId: "event_HWkHhXV9GOkoMD7UcKFaP",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_UkrFqqiDMOV7s5I6Zgbsa"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta = new(
            GroupName,
            "response.audio.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_IpzftxDk3Vvuqjk1zb4fu",
                    Type: "response.audio.delta",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta2 = new(
            GroupName,
            "response.audio.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_PXqbfk3xrcPDCG3eZMn77",
                    Type: "response.audio.delta",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDone = new(
            GroupName,
            "response.audio.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_BCOiZtmQCO6iSlKCflUQd",
                    Type: "response.audio.done",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta = new(
            GroupName,
            "response.audio_transcript.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_XK8Hlo2fXvku9m672utwI",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "没错"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta2 = new(
            GroupName,
            "response.audio_transcript.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_GD79wqnWolR2CVCHnVv1r",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "吗？"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDone = new(
            GroupName,
            "response.audio_transcript.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_EA8aAlWMLY6qoL87qvecN",
                    Type: "response.audio_transcript.done",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Transcript: "没错，博客园确实成立于2004年，它一直秉持着“代码改变世界”的理念，为开发者提供了一个分享技术、交流经验的平台。有什么关于编程或技术的问题需要我帮忙解答吗？"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartAdded = new(
            GroupName,
            "response.content_part.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_Ch7AI1pOoSoqZ1fntVvCs",
                    Type: "response.content_part.added",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartDone = new(
            GroupName,
            "response.content_part.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_H5S1uYUlaDKIo8JiCvnGL",
                    Type: "response.content_part.done",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    ItemId: "item_U5FTiaNPgU26IudmhsF8R",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(
                        Type: "audio",
                        Text: "没错，博客园确实成立于2004年，它一直秉持着“代码改变世界”的理念，为开发者提供了一个分享技术、交流经验的平台。有什么关于编程或技术的问题需要我帮忙解答吗？")));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_JQQYWWURMW7bedTmGj3kU",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_HrRVlzjJozBnqbbwnukUH",
                        Object = "realtime.response",
                        ConversationId = "conv_I71V7HHMZ8jTWRGAZeC6A",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_VISkKC3beYTOuGlNaywE9",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_HrRVlzjJozBnqbbwnukUH",
                        Object = "realtime.response",
                        ConversationId = "conv_I71V7HHMZ8jTWRGAZeC6A",
                        Status = "cancelled",
                        StatusDetails = new OmniRealtimeStatusDetails(Type: "cancelled", Reason: "client_cancelled"),
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_U5FTiaNPgU26IudmhsF8R",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "incomplete",
                                Role = "assistant",
                                Content = new List<OmniRealtimeItemContent>
                                {
                                    new OmniRealtimeItemContent(
                                        Type: "audio",
                                        Transcript: "没错，博客园确实成立于2004年，它一直秉持着“代码改变世界”的理念，为开发者提供了一个分享技术、交流经验的平台。有什么关于编程或技术的问题需要我帮忙解答吗？")
                                }
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_JIbRto58JvnlG6X8qbdah",
                    Type: "response.output_item.added",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_U5FTiaNPgU26IudmhsF8R",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_YdlzsoUb1sneRsJiXyem2",
                    Type: "response.output_item.done",
                    ResponseId: "resp_HrRVlzjJozBnqbbwnukUH",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_U5FTiaNPgU26IudmhsF8R",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_OprpqoQCWbvxs7lHSCdex",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_TPZqKVwiMEIBgEzDsy4qD",
                        Object = "realtime.session",
                        Model = "qwen3.5-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_YzMnWxOA7R8cbpEBQFu2h",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_TPZqKVwiMEIBgEzDsy4qD",
                        Object = "realtime.session",
                        Model = "qwen3.5-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        Instructions = "你是个人助理小云，请你准确且友好地解答用户的问题，始终以乐于助人的态度回应。",
                        TurnDetection = null
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-mcp" (Manual mode with a remote MCP tool (deepwiki), the approval is granted, then the answer follows, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeMcp
    {
        private const string GroupName = "qwen-omni-realtime-mcp";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");
        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");
        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");
        public static readonly SocketMessageSnapshot McpApprovalResponse = new(GroupName, "mcp-approval-response");
        public static readonly SocketMessageSnapshot ResponseCreateAfterMcp = new(GroupName, "response-create-after-mcp");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_NSGiqNb68IpbHRHSqm1oy",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_N79ZRwi7MwMGgNMe75lqZ",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_a2a49da0befb468ca8c3e80a342b105d",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_61e410ba3314422d9942bc40785a8e22",
                        Type = "mcp_list_tools",
                        ServerLabel = "deepwiki",
                        Tools = new List<OmniRealtimeMcpTool>
                        {
                            new OmniRealtimeMcpTool(
                                Name: "ask_wiki_question",
                                Description: "Ask any question about a GitHub repository's codebase and get an AI-powered answer\ngrounded in its DeepWiki.",
                                InputSchema: JsonSerializer.SerializeToElement(
                                    new
                                    {
                                        type = "object",
                                        additionalProperties = false,
                                        properties = new
                                        {
                                            repoName = new
                                            {
                                                anyOf = new object[] { new { type = "string" }, new { items = new { type = "string" }, type = "array" } },
                                                description = "GitHub repository or list of repositories (max 10) in owner/repo format."
                                            },
                                            question = new { description = "The question to ask about the repository.", type = "string" }
                                        },
                                        required = new[] { "repoName", "question" }
                                    })),
                            new OmniRealtimeMcpTool(
                                Name: "read_wiki_contents",
                                Description: "View documentation about a GitHub repository.",
                                InputSchema: JsonSerializer.SerializeToElement(
                                    new
                                    {
                                        type = "object",
                                        additionalProperties = false,
                                        properties = new
                                        {
                                            repoName = new { description = "GitHub repository in owner/repo format (e.g. \"facebook/react\").", type = "string" }
                                        },
                                        required = new[] { "repoName" }
                                    })),
                            new OmniRealtimeMcpTool(
                                Name: "read_wiki_structure",
                                Description: "Get a list of documentation topics for a GitHub repository.",
                                InputSchema: JsonSerializer.SerializeToElement(
                                    new
                                    {
                                        type = "object",
                                        additionalProperties = false,
                                        properties = new
                                        {
                                            repoName = new { description = "GitHub repository in owner/repo format (e.g. \"facebook/react\").", type = "string" }
                                        },
                                        required = new[] { "repoName" }
                                    }))
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated3 = new(
            GroupName,
            "conversation.item.created-3",
            new OmniRealtimeServerEvent(
                    EventId: "event_WcUkK78fBoQ2bMm9hmCGg",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_N79ZRwi7MwMGgNMe75lqZ",
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcp_PWMHpP3QAq7L4OtVDbsrO",
                        Object = "realtime.item",
                        Type = "mcp_call",
                        Status = "in_progress",
                        CallId = "call_9e56d95d8b9a4736a4f2b952",
                        Name = "read_wiki_structure",
                        Arguments = string.Empty,
                        ServerLabel = "deepwiki"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated4 = new(
            GroupName,
            "conversation.item.created-4",
            new OmniRealtimeServerEvent(
                    EventId: "event_40ac5abc601a4e059b18f6b36f3c4da8",
                    Type: "conversation.item.created",
                    ResponseId: "resp_PcX1StpGBwurYelHANGoP",
                    ItemId: "mcpr_446150f407094202a6d6efa319a7b154",
                    PreviousItemId: "mcp_PWMHpP3QAq7L4OtVDbsrO",
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcpr_446150f407094202a6d6efa319a7b154",
                        Type = "mcp_approval_request",
                        CallId = "call_9e56d95d8b9a4736a4f2b952",
                        Name = "read_wiki_structure",
                        Arguments = "{\"repoName\": \"facebook/react\"}",
                        ServerLabel = "deepwiki"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated5 = new(
            GroupName,
            "conversation.item.created-5",
            new OmniRealtimeServerEvent(
                    EventId: "event_Q14zgMR78wtoHvXOSlAlo",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_ENI7cczqXsVzvF5DRS7eo",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_YzHjYGLriJ6kaKWUx3bMB",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionCompleted = new(
            GroupName,
            "conversation.item.input_audio_transcription.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_PcbeT62J3qBFO41Yb2brc",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_N79ZRwi7MwMGgNMe75lqZ",
                    ContentIndex: 0,
                    Transcript: "帮我查一下GitHub上Facebook React仓库的文档结构。",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_VwKEfUfTJdoJs2jxI5Obc",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_N79ZRwi7MwMGgNMe75lqZ",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "帮我",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "EC4cFv48XYgVKe"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta2 = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Uwua7JL11gyoc9t2GCfIN",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_N79ZRwi7MwMGgNMe75lqZ",
                    ContentIndex: 0,
                    Text: "帮我查一下GitHub上Facebook React",
                    Stash: "仓库的文档结构。",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "FaT6WVfeHHn2AB"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferCommitted = new(
            GroupName,
            "input_audio_buffer.committed",
            new OmniRealtimeServerEvent(
                    EventId: "event_IDNgRbF7iIHYmsVh2IYZ7",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_N79ZRwi7MwMGgNMe75lqZ"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> McpListToolsCompleted = new(
            GroupName,
            "mcp_list_tools.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_8b94926643e248269fcbe882800889f7",
                    Type: "mcp_list_tools.completed",
                    ItemId: "item_61e410ba3314422d9942bc40785a8e22"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> McpListToolsInProgress = new(
            GroupName,
            "mcp_list_tools.in_progress",
            new OmniRealtimeServerEvent(
                    EventId: "event_5d98a29eff8f482fb90688b1712957f4",
                    Type: "mcp_list_tools.in_progress",
                    ItemId: "item_61e410ba3314422d9942bc40785a8e22"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta = new(
            GroupName,
            "response.audio.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_UoWbPJpDwOuQfrXPbHpt6",
                    Type: "response.audio.delta",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDelta2 = new(
            GroupName,
            "response.audio.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Z6S4VjrBfVGMC65xAMDLq",
                    Type: "response.audio.delta",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioDone = new(
            GroupName,
            "response.audio.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_PttfrxTTfAMgsjCbGvVxL",
                    Type: "response.audio.done",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta = new(
            GroupName,
            "response.audio_transcript.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_WA5PbltHULC2ANzT5v28H",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "React"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDelta2 = new(
            GroupName,
            "response.audio_transcript.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Z8JBoBt8mHBh14NRNyXk5",
                    Type: "response.audio_transcript.delta",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Delta: "哪一部分？"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseAudioTranscriptDone = new(
            GroupName,
            "response.audio_transcript.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_VBsJaOBdQk6BRb5iU2XHz",
                    Type: "response.audio_transcript.done",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Transcript: "React 仓库的文档结构很清晰，从概览、核心架构到渲染目标、编译器、DevTools、构建工具，最后是附加包和术语表。你想深入了解哪一部分？"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartAdded = new(
            GroupName,
            "response.content_part.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_DOmzK24BNdM6NAyBotiIE",
                    Type: "response.content_part.added",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseContentPartDone = new(
            GroupName,
            "response.content_part.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_PAqMAnwGXLKvvsmOugWLv",
                    Type: "response.content_part.done",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    ItemId: "item_YzHjYGLriJ6kaKWUx3bMB",
                    OutputIndex: 0,
                    ContentIndex: 0,
                    Part: new OmniRealtimeItemContent(
                        Type: "audio",
                        Text: "React 仓库的文档结构很清晰，从概览、核心架构到渲染目标、编译器、DevTools、构建工具，最后是附加包和术语表。你想深入了解哪一部分？")));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_GCNHrcL1iaVTfqB1GVXlK",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_PcX1StpGBwurYelHANGoP",
                        Object = "realtime.response",
                        ConversationId = "conv_VquzBBGtlBhF4spBmkwRR",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated2 = new(
            GroupName,
            "response.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_Ich12k8A6pkEHQSZGZm91",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_A7sBZYdWSAh1DqhrCNyxQ",
                        Object = "realtime.response",
                        ConversationId = "conv_VquzBBGtlBhF4spBmkwRR",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_G3xUcggV722YEKEOqRy1d",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_PcX1StpGBwurYelHANGoP",
                        Object = "realtime.response",
                        ConversationId = "conv_VquzBBGtlBhF4spBmkwRR",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "mcp_PWMHpP3QAq7L4OtVDbsrO",
                                Object = "realtime.item",
                                Type = "mcp_call",
                                Status = "completed",
                                CallId = "call_9e56d95d8b9a4736a4f2b952",
                                Name = "read_wiki_structure",
                                Arguments = "{\"repoName\": \"facebook/react\"}",
                                Output = "{\"_meta\":{\"fastmcp\":{\"wrap_result\":true},\"io.modelcontextprotocol/serverInfo\":{\"name\":" +
                                         "\"DeepWiki\",\"version\":\"4.0.2\"}},\"content\":[{\"text\":\"Available pages for facebook/react" +
                                         ":\\n\\n- 1 React Repository Overview\\n  - 1.1 Repository Structure and Packages\\n  - 1.2 Featu" +
                                         "re Flags System\\n- 2 Core Reconciler Architecture\\n  - 2.1 Fiber Work Loop and Scheduling\\n  " +
                                         "- 2.2 Hooks Implementation\\n  - 2.3 Host Configurations and Renderer Interface\\n  - 2.4 Suspense," +
                                         " Error Boundaries, and Concurrent Features\\n  - 2.5 Scheduler Package\\n- 3 Rendering Targets\\" +
                                         "n  - 3.1 React DOM: Client Rendering\\n  - 3.2 React DOM: Event System\\n  - 3.3 Server-Side Ren" +
                                         "dering: Fizz\\n  - 3.4 React Server Components: Flight Protocol\\n  - 3.5 React Native Renderer " +
                                         "(Fabric)\\n  - 3.6 View Transitions and Gesture Scheduling\\n- 4 React Compiler\\n  - 4.1 Compil" +
                                         "er Pipeline and HIR\\n  - 4.2 Compiler Validation Passes\\n  - 4.3 Compiler Tooling: ESLint Plugin," +
                                         " Playground, LSP, and MCP Server\\n  - 4.4 Compiler Rust Port\\n- 5 React DevTools\\n  - 5.1 DevTools Backend: Hook," +
                                         " Agent, and Renderer Interface\\n  - 5.2 DevTools Frontend: Store," +
                                         " Components Panel, and Profiler\\n  - 5.3 DevTools Distribution: Extensions," +
                                         " Standalone, Inline, Facade, and CDT-MCP\\n- 6 Build System and Tooling\\n  - 6.1 Rollup Build P" +
                                         "ipeline\\n  - 6.2 Jest Test Infrastructure\\n  - 6.3 ESLint," +
                                         " Flow, and Code Quality\\n  - 6.4 Release Process and CI/CD\\n- 7 Additional Packages and Utilit" +
                                         "ies\\n  - 7.1 React Refresh (Fast Refresh)\\n  - 7.2 Test Renderer," +
                                         " react-is, and Utility Packages\\n  - 7.3 Fixtures and Integration Examples\\n- 8 Glossary\",\"t" +
                                         "ype\":\"text\"}],\"isError\":false,\"resultType\":\"complete\",\"structuredContent\":{\"result\"" +
                                         ":\"Available pages for facebook/react:\\n\\n- 1 React Repository Overview\\n  - 1.1 Repository S" +
                                         "tructure and Packages\\n  - 1.2 Feature Flags System\\n- 2 Core Reconciler Architecture\\n  - 2." +
                                         "1 Fiber Work Loop and Scheduling\\n  - 2.2 Hooks Implementation\\n  - 2.3 Host Configurations an" +
                                         "d Renderer Interface\\n  - 2.4 Suspense, Error Boundaries," +
                                         " and Concurrent Features\\n  - 2.5 Scheduler Package\\n- 3 Rendering Targets\\n  - 3.1 React DOM" +
                                         ": Client Rendering\\n  - 3.2 React DOM: Event System\\n  - 3.3 Server-Side Rendering: Fizz\\n  -" +
                                         " 3.4 React Server Components: Flight Protocol\\n  - 3.5 React Native Renderer (Fabric)\\n  - 3.6" +
                                         " View Transitions and Gesture Scheduling\\n- 4 React Compiler\\n  - 4.1 Compiler Pipeline and HI" +
                                         "R\\n  - 4.2 Compiler Validation Passes\\n  - 4.3 Compiler Tooling: ESLint Plugin," +
                                         " Playground, LSP, and MCP Server\\n  - 4.4 Compiler Rust Port\\n- 5 React DevTools\\n  - 5.1 DevTools Backend: Hook," +
                                         " Agent, and Renderer Interface\\n  - 5.2 DevTools Frontend: Store," +
                                         " Components Panel, and Profiler\\n  - 5.3 DevTools Distribution: Extensions," +
                                         " Standalone, Inline, Facade, and CDT-MCP\\n- 6 Build System and Tooling\\n  - 6.1 Rollup Build P" +
                                         "ipeline\\n  - 6.2 Jest Test Infrastructure\\n  - 6.3 ESLint," +
                                         " Flow, and Code Quality\\n  - 6.4 Release Process and CI/CD\\n- 7 Additional Packages and Utilit" +
                                         "ies\\n  - 7.1 React Refresh (Fast Refresh)\\n  - 7.2 Test Renderer," +
                                         " react-is, and Utility Packages\\n  - 7.3 Fixtures and Integration Examples\\n- 8 Glossary\"}}",
                                ServerLabel = "deepwiki"
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 1518,
                            InputTokens = 1456,
                            OutputTokens = 62,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 1421,
                                AudioTokens = 35
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 62
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone2 = new(
            GroupName,
            "response.done-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_UBRQWAq5gnd1dTufUkyMF",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_A7sBZYdWSAh1DqhrCNyxQ",
                        Object = "realtime.response",
                        ConversationId = "conv_VquzBBGtlBhF4spBmkwRR",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "item_YzHjYGLriJ6kaKWUx3bMB",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<OmniRealtimeItemContent>
                                {
                                    new OmniRealtimeItemContent(
                                        Type: "audio",
                                        Transcript: "React 仓库的文档结构很清晰，从概览、核心架构到渲染目标、编译器、DevTools、构建工具，最后是附加包和术语表。你想深入了解哪一部分？")
                                }
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 2684,
                            InputTokens = 2465,
                            OutputTokens = 219,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 2430,
                                AudioTokens = 35
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 41,
                                AudioTokens = 178
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallCompleted = new(
            GroupName,
            "response.mcp_call.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_4891d47c75d546fbb0f21fe510801c24",
                    Type: "response.mcp_call.completed",
                    ItemId: "mcp_PWMHpP3QAq7L4OtVDbsrO",
                    OutputIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallInProgress = new(
            GroupName,
            "response.mcp_call.in_progress",
            new OmniRealtimeServerEvent(
                    EventId: "event_27107150dcdf40bd8b3f6e7ac37f3dd8",
                    Type: "response.mcp_call.in_progress",
                    ItemId: "mcp_PWMHpP3QAq7L4OtVDbsrO",
                    OutputIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallArgumentsDelta = new(
            GroupName,
            "response.mcp_call_arguments.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_JtrgHzSMYWitjpGHmzYo3",
                    Type: "response.mcp_call_arguments.delta",
                    ResponseId: "resp_PcX1StpGBwurYelHANGoP",
                    ItemId: "mcp_PWMHpP3QAq7L4OtVDbsrO",
                    OutputIndex: 0,
                    Delta: "{\"repoName\": ",
                    Obfuscation: "SPf9tYlMu2JdA6"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallArgumentsDelta2 = new(
            GroupName,
            "response.mcp_call_arguments.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_AmMIyyL6hxkXQEvYfhhj4",
                    Type: "response.mcp_call_arguments.delta",
                    ResponseId: "resp_PcX1StpGBwurYelHANGoP",
                    ItemId: "mcp_PWMHpP3QAq7L4OtVDbsrO",
                    OutputIndex: 0,
                    Delta: "}",
                    Obfuscation: "N6MxEuM3HRr5WW"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallArgumentsDone = new(
            GroupName,
            "response.mcp_call_arguments.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_TEh2Yf95zmx2LCJV2i362",
                    Type: "response.mcp_call_arguments.done",
                    ResponseId: "resp_PcX1StpGBwurYelHANGoP",
                    ItemId: "mcp_PWMHpP3QAq7L4OtVDbsrO",
                    OutputIndex: 0,
                    Arguments: "{\"repoName\": \"facebook/react\"}"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_WKTWyAb8ugoJYmU5pDr3M",
                    Type: "response.output_item.added",
                    ResponseId: "resp_PcX1StpGBwurYelHANGoP",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcp_PWMHpP3QAq7L4OtVDbsrO",
                        Object = "realtime.item",
                        Type = "mcp_call",
                        Status = "in_progress",
                        CallId = "call_9e56d95d8b9a4736a4f2b952",
                        Name = "read_wiki_structure",
                        Arguments = string.Empty,
                        ServerLabel = "deepwiki"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded2 = new(
            GroupName,
            "response.output_item.added-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_QxS85XjLGmNSMgWS2Ho9Y",
                    Type: "response.output_item.added",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_YzHjYGLriJ6kaKWUx3bMB",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_477dd0aaa4d24075bc236080efe88146",
                    Type: "response.output_item.done",
                    ResponseId: "resp_PcX1StpGBwurYelHANGoP",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcp_PWMHpP3QAq7L4OtVDbsrO",
                        Type = "mcp_call",
                        Status = "completed",
                        CallId = "call_9e56d95d8b9a4736a4f2b952",
                        Name = "read_wiki_structure",
                        Arguments = "{\"repoName\": \"facebook/react\"}",
                        Output = "{\"_meta\":{\"fastmcp\":{\"wrap_result\":true},\"io.modelcontextprotocol/serverInfo\":{\"name\":" +
                                 "\"DeepWiki\",\"version\":\"4.0.2\"}},\"content\":[{\"text\":\"Available pages for facebook/react" +
                                 ":\\n\\n- 1 React Repository Overview\\n  - 1.1 Repository Structure and Packages\\n  - 1.2 Featu" +
                                 "re Flags System\\n- 2 Core Reconciler Architecture\\n  - 2.1 Fiber Work Loop and Scheduling\\n  " +
                                 "- 2.2 Hooks Implementation\\n  - 2.3 Host Configurations and Renderer Interface\\n  - 2.4 Suspense," +
                                 " Error Boundaries, and Concurrent Features\\n  - 2.5 Scheduler Package\\n- 3 Rendering Targets\\" +
                                 "n  - 3.1 React DOM: Client Rendering\\n  - 3.2 React DOM: Event System\\n  - 3.3 Server-Side Ren" +
                                 "dering: Fizz\\n  - 3.4 React Server Components: Flight Protocol\\n  - 3.5 React Native Renderer " +
                                 "(Fabric)\\n  - 3.6 View Transitions and Gesture Scheduling\\n- 4 React Compiler\\n  - 4.1 Compil" +
                                 "er Pipeline and HIR\\n  - 4.2 Compiler Validation Passes\\n  - 4.3 Compiler Tooling: ESLint Plugin," +
                                 " Playground, LSP, and MCP Server\\n  - 4.4 Compiler Rust Port\\n- 5 React DevTools\\n  - 5.1 DevTools Backend: Hook," +
                                 " Agent, and Renderer Interface\\n  - 5.2 DevTools Frontend: Store," +
                                 " Components Panel, and Profiler\\n  - 5.3 DevTools Distribution: Extensions," +
                                 " Standalone, Inline, Facade, and CDT-MCP\\n- 6 Build System and Tooling\\n  - 6.1 Rollup Build P" +
                                 "ipeline\\n  - 6.2 Jest Test Infrastructure\\n  - 6.3 ESLint," +
                                 " Flow, and Code Quality\\n  - 6.4 Release Process and CI/CD\\n- 7 Additional Packages and Utilit" +
                                 "ies\\n  - 7.1 React Refresh (Fast Refresh)\\n  - 7.2 Test Renderer," +
                                 " react-is, and Utility Packages\\n  - 7.3 Fixtures and Integration Examples\\n- 8 Glossary\",\"t" +
                                 "ype\":\"text\"}],\"isError\":false,\"resultType\":\"complete\",\"structuredContent\":{\"result\"" +
                                 ":\"Available pages for facebook/react:\\n\\n- 1 React Repository Overview\\n  - 1.1 Repository S" +
                                 "tructure and Packages\\n  - 1.2 Feature Flags System\\n- 2 Core Reconciler Architecture\\n  - 2." +
                                 "1 Fiber Work Loop and Scheduling\\n  - 2.2 Hooks Implementation\\n  - 2.3 Host Configurations an" +
                                 "d Renderer Interface\\n  - 2.4 Suspense, Error Boundaries," +
                                 " and Concurrent Features\\n  - 2.5 Scheduler Package\\n- 3 Rendering Targets\\n  - 3.1 React DOM" +
                                 ": Client Rendering\\n  - 3.2 React DOM: Event System\\n  - 3.3 Server-Side Rendering: Fizz\\n  -" +
                                 " 3.4 React Server Components: Flight Protocol\\n  - 3.5 React Native Renderer (Fabric)\\n  - 3.6" +
                                 " View Transitions and Gesture Scheduling\\n- 4 React Compiler\\n  - 4.1 Compiler Pipeline and HI" +
                                 "R\\n  - 4.2 Compiler Validation Passes\\n  - 4.3 Compiler Tooling: ESLint Plugin," +
                                 " Playground, LSP, and MCP Server\\n  - 4.4 Compiler Rust Port\\n- 5 React DevTools\\n  - 5.1 DevTools Backend: Hook," +
                                 " Agent, and Renderer Interface\\n  - 5.2 DevTools Frontend: Store," +
                                 " Components Panel, and Profiler\\n  - 5.3 DevTools Distribution: Extensions," +
                                 " Standalone, Inline, Facade, and CDT-MCP\\n- 6 Build System and Tooling\\n  - 6.1 Rollup Build P" +
                                 "ipeline\\n  - 6.2 Jest Test Infrastructure\\n  - 6.3 ESLint," +
                                 " Flow, and Code Quality\\n  - 6.4 Release Process and CI/CD\\n- 7 Additional Packages and Utilit" +
                                 "ies\\n  - 7.1 React Refresh (Fast Refresh)\\n  - 7.2 Test Renderer," +
                                 " react-is, and Utility Packages\\n  - 7.3 Fixtures and Integration Examples\\n- 8 Glossary\"}}",
                        ServerLabel = "deepwiki"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone2 = new(
            GroupName,
            "response.output_item.done-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_C6IFGSzxcb7mOJfTu1aZM",
                    Type: "response.output_item.done",
                    ResponseId: "resp_A7sBZYdWSAh1DqhrCNyxQ",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_YzHjYGLriJ6kaKWUx3bMB",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_VoazFRFV2Lx1PhAFdj41b",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_WRx8mHHCt6Jm6efFqbvdx",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_H3f25j3vefEbNShOXiuxw",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_WRx8mHHCt6Jm6efFqbvdx",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        Instructions = "你是技术助理，涉及 GitHub 仓库的问题必须先调用 deepwiki 工具查询，不要凭记忆回答。",
                        TurnDetection = null,
                        Tools = new List<OmniRealtimeTool> { new OmniRealtimeTool(Type: "mcp", ServerLabel: "deepwiki", RequireApproval: "always") }
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-mcp-rejected" (Manual mode with a remote MCP tool, the approval is rejected, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeMcpRejected
    {
        private const string GroupName = "qwen-omni-realtime-mcp-rejected";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");
        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");
        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");
        public static readonly SocketMessageSnapshot McpApprovalResponse = new(GroupName, "mcp-approval-response");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated = new(
            GroupName,
            "conversation.item.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_S97jo3dwFXAjVi3HDTbtG",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_UaAlHpEEA4xYUtFae64Xp",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "user",
                        Content = new List<OmniRealtimeItemContent> { new OmniRealtimeItemContent(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated2 = new(
            GroupName,
            "conversation.item.created-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_49098fc58c5041e98ebacca45749938c",
                    Type: "conversation.item.created",
                    Item: new OmniRealtimeItem
                    {
                        Id = "item_0c6b9d7243d443c3be531afc4bb87a24",
                        Type = "mcp_list_tools",
                        ServerLabel = "deepwiki",
                        Tools = new List<OmniRealtimeMcpTool>
                        {
                            new OmniRealtimeMcpTool(
                                Name: "ask_wiki_question",
                                Description: "Ask any question about a GitHub repository's codebase and get an AI-powered answer\ngrounded in its DeepWiki.",
                                InputSchema: JsonSerializer.SerializeToElement(
                                    new
                                    {
                                        type = "object",
                                        additionalProperties = false,
                                        properties = new
                                        {
                                            repoName = new
                                            {
                                                anyOf = new object[] { new { type = "string" }, new { items = new { type = "string" }, type = "array" } },
                                                description = "GitHub repository or list of repositories (max 10) in owner/repo format."
                                            },
                                            question = new { description = "The question to ask about the repository.", type = "string" }
                                        },
                                        required = new[] { "repoName", "question" }
                                    })),
                            new OmniRealtimeMcpTool(
                                Name: "read_wiki_contents",
                                Description: "View documentation about a GitHub repository.",
                                InputSchema: JsonSerializer.SerializeToElement(
                                    new
                                    {
                                        type = "object",
                                        additionalProperties = false,
                                        properties = new
                                        {
                                            repoName = new { description = "GitHub repository in owner/repo format (e.g. \"facebook/react\").", type = "string" }
                                        },
                                        required = new[] { "repoName" }
                                    })),
                            new OmniRealtimeMcpTool(
                                Name: "read_wiki_structure",
                                Description: "Get a list of documentation topics for a GitHub repository.",
                                InputSchema: JsonSerializer.SerializeToElement(
                                    new
                                    {
                                        type = "object",
                                        additionalProperties = false,
                                        properties = new
                                        {
                                            repoName = new { description = "GitHub repository in owner/repo format (e.g. \"facebook/react\").", type = "string" }
                                        },
                                        required = new[] { "repoName" }
                                    }))
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated3 = new(
            GroupName,
            "conversation.item.created-3",
            new OmniRealtimeServerEvent(
                    EventId: "event_JCX5QPigkd1BcznUX6xNy",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_UaAlHpEEA4xYUtFae64Xp",
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcp_RqMoQSHouZK2HufJoIlSN",
                        Object = "realtime.item",
                        Type = "mcp_call",
                        Status = "in_progress",
                        CallId = "call_e76aab808f3645e59eca36b5",
                        Name = "read_wiki_structure",
                        Arguments = string.Empty,
                        ServerLabel = "deepwiki"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemCreated4 = new(
            GroupName,
            "conversation.item.created-4",
            new OmniRealtimeServerEvent(
                    EventId: "event_d293151151234731a150685f7d2a50ef",
                    Type: "conversation.item.created",
                    ResponseId: "resp_AymNBfW1yNou1awnCgMgK",
                    ItemId: "mcpr_cadf0c074dca45fcb73609a2a4bef549",
                    PreviousItemId: "mcp_RqMoQSHouZK2HufJoIlSN",
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcpr_cadf0c074dca45fcb73609a2a4bef549",
                        Type = "mcp_approval_request",
                        CallId = "call_e76aab808f3645e59eca36b5",
                        Name = "read_wiki_structure",
                        Arguments = "{\"repoName\": \"facebook/react\"}",
                        ServerLabel = "deepwiki"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionCompleted = new(
            GroupName,
            "conversation.item.input_audio_transcription.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_Nl486do5bRS8AtFMEJYht",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_UaAlHpEEA4xYUtFae64Xp",
                    ContentIndex: 0,
                    Transcript: "帮我查一下GitHub上Facebook React仓库的文档结构。",
                    Language: "zh",
                    Emotion: "neutral"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_ZTHZK7fD5bDnBe91pG6y9",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_UaAlHpEEA4xYUtFae64Xp",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "帮我",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "T3RxzAyq7L3fXJ"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ConversationItemInputAudioTranscriptionDelta2 = new(
            GroupName,
            "conversation.item.input_audio_transcription.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_FFk9AJg1Wi3OtebmFlQkO",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_UaAlHpEEA4xYUtFae64Xp",
                    ContentIndex: 0,
                    Text: "帮我查一下GitHub上Facebook React",
                    Stash: "仓库的文档结构。",
                    Language: "zh",
                    Emotion: "neutral",
                    Obfuscation: "HqnpX9qLrEtFrr"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> InputAudioBufferCommitted = new(
            GroupName,
            "input_audio_buffer.committed",
            new OmniRealtimeServerEvent(
                    EventId: "event_X2lhvHg3X1gNffgsTbtXB",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_UaAlHpEEA4xYUtFae64Xp"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> McpListToolsCompleted = new(
            GroupName,
            "mcp_list_tools.completed",
            new OmniRealtimeServerEvent(
                    EventId: "event_e9a244e733dc4ec99cb7cbdb793b727b",
                    Type: "mcp_list_tools.completed",
                    ItemId: "item_0c6b9d7243d443c3be531afc4bb87a24"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> McpListToolsInProgress = new(
            GroupName,
            "mcp_list_tools.in_progress",
            new OmniRealtimeServerEvent(
                    EventId: "event_1d865703d5b84007bbe3960ce9314853",
                    Type: "mcp_list_tools.in_progress",
                    ItemId: "item_0c6b9d7243d443c3be531afc4bb87a24"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_M69KHNpsPgKpOZiPekdyP",
                    Type: "response.created",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_AymNBfW1yNou1awnCgMgK",
                        Object = "realtime.response",
                        ConversationId = "conv_KkZ7EjC8aX2gqsXLISFz5",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>()
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_PTQVtrn2S6C2uz4wb2wfk",
                    Type: "response.done",
                    Response: new OmniRealtimeResponse
                    {
                        Id = "resp_AymNBfW1yNou1awnCgMgK",
                        Object = "realtime.response",
                        ConversationId = "conv_KkZ7EjC8aX2gqsXLISFz5",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        OutputAudioFormat = "pcm",
                        Output = new List<OmniRealtimeItem>
                        {
                            new OmniRealtimeItem
                            {
                                Id = "mcp_RqMoQSHouZK2HufJoIlSN",
                                Object = "realtime.item",
                                Type = "mcp_call",
                                Status = "failed",
                                CallId = "call_e76aab808f3645e59eca36b5",
                                Name = "read_wiki_structure",
                                Arguments = "{\"repoName\": \"facebook/react\"}",
                                ServerLabel = "deepwiki",
                                Error = new OmniRealtimeError
                                {
                                    Type = "tool_execution_error",
                                    Message = "MCP tool call failed (approval_rejected)."
                                }
                            }
                        },
                        Usage = new OmniRealtimeUsage
                        {
                            TotalTokens = 1518,
                            InputTokens = 1456,
                            OutputTokens = 62,
                            InputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 1421,
                                AudioTokens = 35
                            },
                            OutputTokensDetails = new OmniRealtimeTokenDetails
                            {
                                TextTokens = 62
                            }
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallFailed = new(
            GroupName,
            "response.mcp_call.failed",
            new OmniRealtimeServerEvent(
                    EventId: "event_601cecf3cf4a4fb9a6a7f06991a0ce62",
                    Type: "response.mcp_call.failed",
                    ItemId: "mcp_RqMoQSHouZK2HufJoIlSN",
                    OutputIndex: 0));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallArgumentsDelta = new(
            GroupName,
            "response.mcp_call_arguments.delta",
            new OmniRealtimeServerEvent(
                    EventId: "event_HQAhqRpJ9nAmAikLKsxSk",
                    Type: "response.mcp_call_arguments.delta",
                    ResponseId: "resp_AymNBfW1yNou1awnCgMgK",
                    ItemId: "mcp_RqMoQSHouZK2HufJoIlSN",
                    OutputIndex: 0,
                    Delta: "{\"repoName\": \"",
                    Obfuscation: "JjHS9nFUXTLFkt"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallArgumentsDelta2 = new(
            GroupName,
            "response.mcp_call_arguments.delta-2",
            new OmniRealtimeServerEvent(
                    EventId: "event_NxHqkeMNtNOpwoPlqJKvO",
                    Type: "response.mcp_call_arguments.delta",
                    ResponseId: "resp_AymNBfW1yNou1awnCgMgK",
                    ItemId: "mcp_RqMoQSHouZK2HufJoIlSN",
                    OutputIndex: 0,
                    Delta: "}",
                    Obfuscation: "DGgrV6or5garU4"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseMcpCallArgumentsDone = new(
            GroupName,
            "response.mcp_call_arguments.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_HrBhFLIFPqAVG8YElxTsK",
                    Type: "response.mcp_call_arguments.done",
                    ResponseId: "resp_AymNBfW1yNou1awnCgMgK",
                    ItemId: "mcp_RqMoQSHouZK2HufJoIlSN",
                    OutputIndex: 0,
                    Arguments: "{\"repoName\": \"facebook/react\"}"));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemAdded = new(
            GroupName,
            "response.output_item.added",
            new OmniRealtimeServerEvent(
                    EventId: "event_JMcBl2O5vOoCtgiiTwXUG",
                    Type: "response.output_item.added",
                    ResponseId: "resp_AymNBfW1yNou1awnCgMgK",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcp_RqMoQSHouZK2HufJoIlSN",
                        Object = "realtime.item",
                        Type = "mcp_call",
                        Status = "in_progress",
                        CallId = "call_e76aab808f3645e59eca36b5",
                        Name = "read_wiki_structure",
                        Arguments = string.Empty,
                        ServerLabel = "deepwiki"
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> ResponseOutputItemDone = new(
            GroupName,
            "response.output_item.done",
            new OmniRealtimeServerEvent(
                    EventId: "event_7469a971ef174f38975c6736f741754a",
                    Type: "response.output_item.done",
                    ResponseId: "resp_AymNBfW1yNou1awnCgMgK",
                    OutputIndex: 0,
                    Item: new OmniRealtimeItem
                    {
                        Id = "mcp_RqMoQSHouZK2HufJoIlSN",
                        Type = "mcp_call",
                        Status = "failed",
                        CallId = "call_e76aab808f3645e59eca36b5",
                        Name = "read_wiki_structure",
                        Arguments = "{\"repoName\": \"facebook/react\"}",
                        ServerLabel = "deepwiki",
                        Error = new OmniRealtimeError
                        {
                            Type = "tool_execution_error",
                            Message = "MCP tool call failed (approval_rejected)."
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_MxrCFySKbiRWWDbDqgofl",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_Cxg7N7mHERyi6KN7p7j71",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new OmniRealtimeServerEvent(
                    EventId: "event_IQ9lXDQ6DPOPVLysecRcx",
                    Type: "session.updated",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_Cxg7N7mHERyi6KN7p7j71",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        Instructions = "你是技术助理，涉及 GitHub 仓库的问题必须先调用 deepwiki 工具查询，不要凭记忆回答。",
                        TurnDetection = null,
                        Tools = new List<OmniRealtimeTool> { new OmniRealtimeTool(Type: "mcp", ServerLabel: "deepwiki", RequireApproval: "always") }
                    }));
    }

    /// <summary>
    /// The omni realtime group "qwen-omni-realtime-error" (The session.update is rejected: invalid modalities, captured with qwen3.8-omni-flash-realtime).
    /// </summary>
    public static class OmniRealtimeInvalidModalities
    {
        private const string GroupName = "qwen-omni-realtime-error";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        // Server events, captured verbatim.
        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> Error = new(
            GroupName,
            "error",
            new OmniRealtimeServerEvent(
                    EventId: "event_MeiyyxQUYTmiOOjfcn3XL",
                    Type: "error",
                    Error: new OmniRealtimeError
                    {
                        Code = "invalid_value",
                        Message = "Invalid modalities: ['audio']. Supported combinations are: ['text'] and ['audio', 'text']."
                    }));

        public static readonly SocketMessageSnapshot<OmniRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new OmniRealtimeServerEvent(
                    EventId: "event_Y7MVuzlwPcgmOIXbpbk3q",
                    Type: "session.created",
                    Session: new OmniRealtimeSession
                    {
                        Id = "sess_OoQVqE7K2qjh35btp6gxI",
                        Object = "realtime.session",
                        Model = "qwen3.8-omni-flash-realtime",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "Tina",
                        InputAudioTranscription = new OmniRealtimeTranscription(Model: "qwen3-asr-flash-realtime"),
                        InputAudioFormat = "pcm",
                        OutputAudioFormat = "pcm",
                        TurnDetection = new OmniRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800,
                            PrefixPaddingMs = 300,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));
    }
}
