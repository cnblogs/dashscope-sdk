using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class AudioChat
    {
        private const string GroupName = "qwen-audio-realtime";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemCreated = new(
                "qwen-audio-realtime",
                "conversation.item.created",
                new AudioChatServerEvent(
                    EventId: "event_XQIAQjaBbNmqR1pYDlkwg",
                    Type: "conversation.item.created",
                    Item: new AudioChatItem
                    {
                        Id = "item_XQIAQLGzyIiIw7G21nEHW",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "user",
                        Content = new List<AudioChatItemContent> { new(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemCreated2 = new(
                "qwen-audio-realtime",
                "conversation.item.created-2",
                new AudioChatServerEvent(
                    EventId: "event_SQbQq3tOpsK1oB4Xh8jtE",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_XQIAQLGzyIiIw7G21nEHW",
                    Item: new AudioChatItem
                    {
                        Id = "item_XQIAT2GwQfKnNDVTNSzmJ",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<AudioChatItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemInputAudioTranscriptionCompleted = new(
                "qwen-audio-realtime",
                "conversation.item.input_audio_transcription.completed",
                new AudioChatServerEvent(
                    EventId: "event_F971PRx59nx7ZWQlhu2XH",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_XQIAQLGzyIiIw7G21nEHW",
                    ContentIndex: 0,
                    Transcript: "博客园成立于2004年，代码改变世界。"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemInputAudioTranscriptionDelta = new(
                "qwen-audio-realtime",
                "conversation.item.input_audio_transcription.delta",
                new AudioChatServerEvent(
                    EventId: "event_CotI7zQoiTIpguQFYv8Ir",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_XQIAQLGzyIiIw7G21nEHW",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "驳",
                    Obfuscation: "R5xSi6YaJKsTFS"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemInputAudioTranscriptionDelta2 = new(
                "qwen-audio-realtime",
                "conversation.item.input_audio_transcription.delta-2",
                new AudioChatServerEvent(
                    EventId: "event_JuKleTayUdmBiFsstkT11",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_XQIAQLGzyIiIw7G21nEHW",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客园成立于2004年，代码改编世界",
                    Obfuscation: "SzooiwWE3MTzTY"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            InputAudioBufferCommitted = new(
                "qwen-audio-realtime",
                "input_audio_buffer.committed",
                new AudioChatServerEvent(
                    EventId: "event_AXmhUm5QFRdmDE6m7tyJg",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_XQIAQLGzyIiIw7G21nEHW"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            InputAudioBufferSpeechStarted = new(
                "qwen-audio-realtime",
                "input_audio_buffer.speech_started",
                new AudioChatServerEvent(
                    EventId: "event_L7yf49rdTlj1zJdru688M",
                    Type: "input_audio_buffer.speech_started",
                    ItemId: "item_XQIAQLGzyIiIw7G21nEHW",
                    AudioStartMs: 0));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            InputAudioBufferSpeechStopped = new(
                "qwen-audio-realtime",
                "input_audio_buffer.speech_stopped",
                new AudioChatServerEvent(
                    EventId: "event_UAh6839l3Do84SArVDTJ7",
                    Type: "input_audio_buffer.speech_stopped",
                    ItemId: "item_XQIAQLGzyIiIw7G21nEHW",
                    AudioEndMs: 3620));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioDelta = new(
                "qwen-audio-realtime",
                "response.audio.delta",
                new AudioChatServerEvent(
                    EventId: "event_QI9NbVoSix6M2UPDRcNrm",
                    Type: "response.audio.delta",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioDelta2 = new(
                "qwen-audio-realtime",
                "response.audio.delta-2",
                new AudioChatServerEvent(
                    EventId: "event_St4s7RtpAmtuRKpKljEGz",
                    Type: "response.audio.delta",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioDone = new(
                "qwen-audio-realtime",
                "response.audio.done",
                new AudioChatServerEvent(
                    EventId: "event_MxT9jZDZWYXIAdrzhd1mf",
                    Type: "response.audio.done",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioTranscriptDelta = new(
                "qwen-audio-realtime",
                "response.audio_transcript.delta",
                new AudioChatServerEvent(
                    EventId: "event_EPql1eZ6PnkFMrniPFlPE",
                    Type: "response.audio_transcript.delta",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Delta: "哇"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioTranscriptDelta2 = new(
                "qwen-audio-realtime",
                "response.audio_transcript.delta-2",
                new AudioChatServerEvent(
                    EventId: "event_MMFKMDsTZ3dfoadlP8MWH",
                    Type: "response.audio_transcript.delta",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Delta: "？"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioTranscriptDone = new(
                "qwen-audio-realtime",
                "response.audio_transcript.done",
                new AudioChatServerEvent(
                    EventId: "event_M2Qox2Tfj5tydWscQNT45",
                    Type: "response.audio_transcript.done",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Transcript: "哇，2004年就成立啦，真的很有历史感呢！“代码改变世界”这句口号也特别有力量，感觉他们一直都很专注于用技术创造价值。看来你对这家企业很关注呀，是打算了解他们的最新动态，还是对某个具体项目感兴趣？"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseContentPartAdded = new(
                "qwen-audio-realtime",
                "response.content_part.added",
                new AudioChatServerEvent(
                    EventId: "event_NWlzjDAHrFXbLY8vwSxU7",
                    Type: "response.content_part.added",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Part: new AudioChatItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseContentPartDone = new(
                "qwen-audio-realtime",
                "response.content_part.done",
                new AudioChatServerEvent(
                    EventId: "event_RpeE3NchkkpqwTnjFD6mv",
                    Type: "response.content_part.done",
                    ItemId: "item_XQIAT2GwQfKnNDVTNSzmJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Part: new AudioChatItemContent(
                        Type: "audio",
                        Text: "哇，2004年就成立啦，真的很有历史感呢！“代码改变世界”这句口号也特别有力量，感觉他们一直都很专注于用技术创造价值。看来你对这家企业很关注呀，是打算了解他们的最新动态，还是对某个具体项目感兴趣？")));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseCreated = new(
                "qwen-audio-realtime",
                "response.created",
                new AudioChatServerEvent(
                    EventId: "event_SMmhnTBKcjx4jjJTX3iVZ",
                    Type: "response.created",
                    Response: new AudioChatResponse
                    {
                        Id = "resp_XQIAWhjAMHUYuuFRRLeCp",
                        Object = "realtime.response",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        Output = new List<AudioChatItem>()
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseDone = new(
                "qwen-audio-realtime",
                "response.done",
                new AudioChatServerEvent(
                    EventId: "event_JxzDNG2SzpEcoXVdl2Wgs",
                    Type: "response.done",
                    Response: new AudioChatResponse
                    {
                        Id = "resp_XQIAWhjAMHUYuuFRRLeCp",
                        Object = "realtime.response",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        Output = new List<AudioChatItem>
                        {
                            new()
                            {
                                Id = "item_XQIAT2GwQfKnNDVTNSzmJ",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<AudioChatItemContent>
                                {
                                    new(
                                        Type: "audio",
                                        Transcript: "哇，2004年就成立啦，真的很有历史感呢！“代码改变世界”这句口号也特别有力量，感觉他们一直都很专注于用技术创造价值。看来你对这家企业很关注呀，是打算了解他们的最新动态，还是对某个具体项目感兴趣？")
                                }
                            }
                        },
                        Usage = new AudioChatUsage
                        {
                            TotalTokens = 591,
                            InputTokens = 304,
                            OutputTokens = 287,
                            InputTokensDetails = new AudioChatTokenDetails { TextTokens = 258, AudioTokens = 46 },
                            OutputTokensDetails = new AudioChatTokenDetails { TextTokens = 58, AudioTokens = 229 }
                        }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseOutputItemAdded = new(
                "qwen-audio-realtime",
                "response.output_item.added",
                new AudioChatServerEvent(
                    EventId: "event_EiWXala1KVWYmOWyh2Lwq",
                    Type: "response.output_item.added",
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Item: new AudioChatItem
                    {
                        Id = "item_XQIAT2GwQfKnNDVTNSzmJ",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<AudioChatItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseOutputItemDone = new(
                "qwen-audio-realtime",
                "response.output_item.done",
                new AudioChatServerEvent(
                    EventId: "event_MCMyRvWS3az6zt8Qe4Ox4",
                    Type: "response.output_item.done",
                    OutputIndex: 0,
                    ResponseId: "resp_XQIAWhjAMHUYuuFRRLeCp",
                    Item: new AudioChatItem
                    {
                        Id = "item_XQIAT2GwQfKnNDVTNSzmJ",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<AudioChatItemContent>
                        {
                            new(
                                Type: "audio",
                                Text: "哇，2004年就成立啦，真的很有历史感呢！“代码改变世界”这句口号也特别有力量，感觉他们一直都很专注于用技术创造价值。看来你对这家企业很关注呀，是打算了解他们的最新动态，还是对某个具体项目感兴趣？")
                        }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            SessionCreated = new(
                "qwen-audio-realtime",
                "session.created",
                new AudioChatServerEvent(
                    EventId: "event_H5KKmVAbBoWgf1aYXTvNI",
                    Type: "session.created",
                    Session: new AudioChatSession
                    {
                        Id = "sess_XQIA4wVOAUEdv1CemANCh",
                        Object = "realtime.session",
                        Model = "qwen-audio-3.0-realtime-flash",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        InputAudioTranscription = new AudioChatTranscription(Model: "fun-asr"),
                        TurnDetection = new AudioChatTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800
                        }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            SessionUpdated = new(
                "qwen-audio-realtime",
                "session.updated",
                new AudioChatServerEvent(
                    EventId: "event_Ykm2rahCqk8FJn962Q9J7",
                    Type: "session.updated",
                    Session: new AudioChatSession
                    {
                        Id = "sess_XQIA4wVOAUEdv1CemANCh",
                        Object = "realtime.session",
                        Model = "qwen-audio-3.0-realtime-flash",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        InputAudioTranscription = new AudioChatTranscription(Model: "fun-asr"),
                        TurnDetection = new AudioChatTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 500
                        }
                    }));
    }

    public static class AudioChatManual
    {
        private const string GroupName = "qwen-audio-realtime-manual";

        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");

        public static readonly SocketMessageSnapshot ResponseCreate = new(GroupName, "response-create");

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemCreated = new(
                "qwen-audio-realtime-manual",
                "conversation.item.created",
                new AudioChatServerEvent(
                    EventId: "event_BTciJ61pIyNZhbB23OT6r",
                    Type: "conversation.item.created",
                    Item: new AudioChatItem
                    {
                        Id = "item_BTciV4aTVORfmQSXuogNm",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "user",
                        Content = new List<AudioChatItemContent> { new(Type: "input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemCreated2 = new(
                "qwen-audio-realtime-manual",
                "conversation.item.created-2",
                new AudioChatServerEvent(
                    EventId: "event_C8L9H7VDgavmOTWpnwtn3",
                    Type: "conversation.item.created",
                    PreviousItemId: "item_BTciV4aTVORfmQSXuogNm",
                    Item: new AudioChatItem
                    {
                        Id = "item_BTciU9CfDIRXyf6BjSZOJ",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<AudioChatItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemInputAudioTranscriptionCompleted = new(
                "qwen-audio-realtime-manual",
                "conversation.item.input_audio_transcription.completed",
                new AudioChatServerEvent(
                    EventId: "event_RB6SuOM8e251Dp8KWRvTA",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_BTciV4aTVORfmQSXuogNm",
                    ContentIndex: 0,
                    Transcript: "博客园成立于2004年，代码改变世界。"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemInputAudioTranscriptionDelta = new(
                "qwen-audio-realtime-manual",
                "conversation.item.input_audio_transcription.delta",
                new AudioChatServerEvent(
                    EventId: "event_IXfk7Y8fwmBGa6p4wPMhi",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_BTciV4aTVORfmQSXuogNm",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "驳",
                    Obfuscation: "EjPgAfu4fed4sm"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ConversationItemInputAudioTranscriptionDelta2 = new(
                "qwen-audio-realtime-manual",
                "conversation.item.input_audio_transcription.delta-2",
                new AudioChatServerEvent(
                    EventId: "event_Py8LZ11xHEzABHjtGjuR1",
                    Type: "conversation.item.input_audio_transcription.delta",
                    ItemId: "item_BTciV4aTVORfmQSXuogNm",
                    ContentIndex: 0,
                    Text: string.Empty,
                    Stash: "博客园成立于2004年，代码改编世界",
                    Obfuscation: "X6YGWcVxuNidZn"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            InputAudioBufferCommitted = new(
                "qwen-audio-realtime-manual",
                "input_audio_buffer.committed",
                new AudioChatServerEvent(
                    EventId: "event_YnDG9xTCePf2QnBTUSXPG",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_BTciV4aTVORfmQSXuogNm"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioDelta = new(
                "qwen-audio-realtime-manual",
                "response.audio.delta",
                new AudioChatServerEvent(
                    EventId: "event_QNVRKtDtU6ijl2BE3KoHW",
                    Type: "response.audio.delta",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioDelta2 = new(
                "qwen-audio-realtime-manual",
                "response.audio.delta-2",
                new AudioChatServerEvent(
                    EventId: "event_K3trXKTQLb8ZXRSizRUAo",
                    Type: "response.audio.delta",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Delta: "<base64>"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioDone = new(
                "qwen-audio-realtime-manual",
                "response.audio.done",
                new AudioChatServerEvent(
                    EventId: "event_NulXo8HjDBv5wjZyo7Wtp",
                    Type: "response.audio.done",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioTranscriptDelta = new(
                "qwen-audio-realtime-manual",
                "response.audio_transcript.delta",
                new AudioChatServerEvent(
                    EventId: "event_ZfPJ5S3dEpVCgqzdAPBBj",
                    Type: "response.audio_transcript.delta",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Delta: "哇"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioTranscriptDelta2 = new(
                "qwen-audio-realtime-manual",
                "response.audio_transcript.delta-2",
                new AudioChatServerEvent(
                    EventId: "event_RuhXuuHdqEtV9YPJ8UkoU",
                    Type: "response.audio_transcript.delta",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Delta: "。"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseAudioTranscriptDone = new(
                "qwen-audio-realtime-manual",
                "response.audio_transcript.done",
                new AudioChatServerEvent(
                    EventId: "event_PgsK5o9JG7p12QSLqO4C9",
                    Type: "response.audio_transcript.done",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Transcript: "哇，2004年就开始啦，那时候互联网刚火起来没多久呢。博客园确实是个老牌的开发者社区，很多程序员都在这里分享代码心得，互相学习，氛围一直挺好的。"));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseContentPartAdded = new(
                "qwen-audio-realtime-manual",
                "response.content_part.added",
                new AudioChatServerEvent(
                    EventId: "event_VckyNJ2hTFOHipNtYqwav",
                    Type: "response.content_part.added",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Part: new AudioChatItemContent(Type: "audio", Text: string.Empty)));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseContentPartDone = new(
                "qwen-audio-realtime-manual",
                "response.content_part.done",
                new AudioChatServerEvent(
                    EventId: "event_Jvk64Rq19d6I8O3xPCNWj",
                    Type: "response.content_part.done",
                    ItemId: "item_BTciU9CfDIRXyf6BjSZOJ",
                    ContentIndex: 0,
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Part: new AudioChatItemContent(
                        Type: "audio",
                        Text: "哇，2004年就开始啦，那时候互联网刚火起来没多久呢。博客园确实是个老牌的开发者社区，很多程序员都在这里分享代码心得，互相学习，氛围一直挺好的。")));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseCreated = new(
                "qwen-audio-realtime-manual",
                "response.created",
                new AudioChatServerEvent(
                    EventId: "event_De1FDhZXbFvWEvOxpXn4I",
                    Type: "response.created",
                    Response: new AudioChatResponse
                    {
                        Id = "resp_BTciYRb4grCEdXTEt4O7n",
                        Object = "realtime.response",
                        Status = "in_progress",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        Output = new List<AudioChatItem>()
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseDone = new(
                "qwen-audio-realtime-manual",
                "response.done",
                new AudioChatServerEvent(
                    EventId: "event_MkbmfbWzgFPSKCpAvzuBk",
                    Type: "response.done",
                    Response: new AudioChatResponse
                    {
                        Id = "resp_BTciYRb4grCEdXTEt4O7n",
                        Object = "realtime.response",
                        Status = "completed",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        Output = new List<AudioChatItem>
                        {
                            new()
                            {
                                Id = "item_BTciU9CfDIRXyf6BjSZOJ",
                                Object = "realtime.item",
                                Type = "message",
                                Status = "completed",
                                Role = "assistant",
                                Content = new List<AudioChatItemContent>
                                {
                                    new(
                                        Type: "audio",
                                        Transcript: "哇，2004年就开始啦，那时候互联网刚火起来没多久呢。博客园确实是个老牌的开发者社区，很多程序员都在这里分享代码心得，互相学习，氛围一直挺好的。")
                                }
                            }
                        },
                        Usage = new AudioChatUsage
                        {
                            TotalTokens = 526,
                            InputTokens = 307,
                            OutputTokens = 219,
                            InputTokensDetails = new AudioChatTokenDetails { TextTokens = 258, AudioTokens = 49 },
                            OutputTokensDetails = new AudioChatTokenDetails { TextTokens = 41, AudioTokens = 178 }
                        }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseOutputItemAdded = new(
                "qwen-audio-realtime-manual",
                "response.output_item.added",
                new AudioChatServerEvent(
                    EventId: "event_Ml1bjPEHRgqvdGzUB23eu",
                    Type: "response.output_item.added",
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Item: new AudioChatItem
                    {
                        Id = "item_BTciU9CfDIRXyf6BjSZOJ",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<AudioChatItemContent>()
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            ResponseOutputItemDone = new(
                "qwen-audio-realtime-manual",
                "response.output_item.done",
                new AudioChatServerEvent(
                    EventId: "event_ZuvgbpY6bvRErerstRFdb",
                    Type: "response.output_item.done",
                    OutputIndex: 0,
                    ResponseId: "resp_BTciYRb4grCEdXTEt4O7n",
                    Item: new AudioChatItem
                    {
                        Id = "item_BTciU9CfDIRXyf6BjSZOJ",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "completed",
                        Role = "assistant",
                        Content = new List<AudioChatItemContent>
                        {
                            new(
                                Type: "audio",
                                Text: "哇，2004年就开始啦，那时候互联网刚火起来没多久呢。博客园确实是个老牌的开发者社区，很多程序员都在这里分享代码心得，互相学习，氛围一直挺好的。")
                        }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            SessionCreated = new(
                "qwen-audio-realtime-manual",
                "session.created",
                new AudioChatServerEvent(
                    EventId: "event_HFeDyCaDZEmpfGVJWC2Pg",
                    Type: "session.created",
                    Session: new AudioChatSession
                    {
                        Id = "sess_BTci2RPSOjcVPRThOjA3w",
                        Object = "realtime.session",
                        Model = "qwen-audio-3.0-realtime-flash",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        InputAudioTranscription = new AudioChatTranscription(Model: "fun-asr"),
                        TurnDetection = new AudioChatTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.5f,
                            SilenceDurationMs = 800
                        }
                    }));

        public static readonly SocketMessageSnapshot<AudioChatServerEvent>
            SessionUpdated = new(
                "qwen-audio-realtime-manual",
                "session.updated",
                new AudioChatServerEvent(
                    EventId: "event_YOYbzPVH54WGxiZ4Siek4",
                    Type: "session.updated",
                    Session: new AudioChatSession
                    {
                        Id = "sess_BTci2RPSOjcVPRThOjA3w",
                        Object = "realtime.session",
                        Model = "qwen-audio-3.0-realtime-flash",
                        Modalities = new List<string> { "text", "audio" },
                        Voice = "longanqian",
                        InputAudioTranscription = new AudioChatTranscription(Model: "fun-asr")
                    }));
    }
}
