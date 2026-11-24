using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public partial class Snapshots
{
    public static class AsrRealtime
    {
        private const string GroupName = "qwen-asr-realtime";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        public static readonly SocketMessageSnapshot FinishSession = new(GroupName, "finish-session");

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SessionCreated = new(
                "qwen-asr-realtime",
                "session.created",
                new AsrRealtimeServerEvent(
                    EventId: "event_TgKTrcTCUSZ6B3NJaPVCm",
                    Type: "session.created",
                    Session:
                    new AsrRealtimeSession
                    {
                        Id = "sess_Z1PvwtztItlADVAdcTv7f",
                        Object = "realtime.session",
                        Model = "qwen3-asr-flash-realtime",
                        Modalities = new List<string> { "text" },
                        InputAudioFormat = "pcm",
                        SampleRate = 16000,
                        InputAudioTranscription = new AsrRealtimeTranscription { Model = "qwen3-asr-flash-realtime" },
                        TurnDetection = new AsrRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.2f,
                            SilenceDurationMs = 800
                        }
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SessionUpdated = new(
                "qwen-asr-realtime",
                "session.updated",
                new AsrRealtimeServerEvent(
                    EventId: "event_GBWSUoHfdLvpwW232H593",
                    Type: "session.updated",
                    Session:
                    new AsrRealtimeSession
                    {
                        Id = "sess_Z1PvwtztItlADVAdcTv7f",
                        Object = "realtime.session",
                        Model = "qwen3-asr-flash-realtime",
                        Modalities = new List<string> { "text" },
                        InputAudioFormat = "pcm",
                        SampleRate = 16000,
                        InputAudioTranscription =
                            new AsrRealtimeTranscription { Language = "zh", Model = "qwen3-asr-flash-realtime" },
                        TurnDetection = new AsrRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.0f,
                            SilenceDurationMs = 400,
                            CreateResponse = true,
                            InterruptResponse = true
                        }
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SpeechStarted = new(
                "qwen-asr-realtime",
                "input_audio_buffer.speech_started",
                new AsrRealtimeServerEvent(
                    EventId: "event_ILB4WHCpVuvlMnGKesffg",
                    Type: "input_audio_buffer.speech_started",
                    ItemId: "item_Fptp1UdxNgUZaZtE5UUEp",
                    AudioStartMs: 0));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SpeechStopped = new(
                "qwen-asr-realtime",
                "input_audio_buffer.speech_stopped",
                new AsrRealtimeServerEvent(
                    EventId: "event_EAH3tp3tkS9ufbBsiejwh",
                    Type: "input_audio_buffer.speech_stopped",
                    ItemId: "item_Fptp1UdxNgUZaZtE5UUEp",
                    AudioEndMs: 2240));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            Committed = new(
                "qwen-asr-realtime",
                "input_audio_buffer.committed",
                new AsrRealtimeServerEvent(
                    EventId: "event_Z6Gp2IDTObL99KyiLpHgG",
                    Type: "input_audio_buffer.committed",
                    ItemId: "item_Fptp1UdxNgUZaZtE5UUEp"));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            ItemCreated = new(
                "qwen-asr-realtime",
                "conversation.item.created",
                new AsrRealtimeServerEvent(
                    EventId: "event_EWqGUGbEtviD22fmfo4KD",
                    Type: "conversation.item.created",
                    Item: new AsrRealtimeItem
                    {
                        Id = "item_Fptp1UdxNgUZaZtE5UUEp",
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<AsrRealtimeItemContent> { new("input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            TranscriptionText = new(
                "qwen-asr-realtime",
                "conversation.item.input_audio_transcription.text",
                new AsrRealtimeServerEvent(
                    EventId: "event_KWzfgyLPQzBurBTWNlfUn",
                    Type: "conversation.item.input_audio_transcription.text",
                    ItemId: "item_Fptp1UdxNgUZaZtE5UUEp",
                    ContentIndex: 0,
                    Language: "zh",
                    Emotion: "neutral",
                    Text: string.Empty,
                    Stash: "博客"));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            TranscriptionTextLast = new(
                "qwen-asr-realtime",
                "conversation.item.input_audio_transcription.text-18",
                new AsrRealtimeServerEvent(
                    EventId: "event_AJqgvmkwcVm5glwClplYI",
                    Type: "conversation.item.input_audio_transcription.text",
                    ItemId: "item_W7UE3OXSBDMiff6Q9SkXy",
                    ContentIndex: 0,
                    Language: "zh",
                    Emotion: "neutral",
                    Text: string.Empty,
                    Stash: "代码改变世界。"));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            TranscriptionCompleted = new(
                "qwen-asr-realtime",
                "conversation.item.input_audio_transcription.completed",
                new AsrRealtimeServerEvent(
                    EventId: "event_G1757gqFFdUpO9MnkujVh",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ItemId: "item_Fptp1UdxNgUZaZtE5UUEp",
                    ContentIndex: 0,
                    Language: "zh",
                    Emotion: "neutral",
                    Transcript: "博客园成立于二零零四年。",
                    Usage: new AsrRealtimeUsage
                    {
                        Duration = 2,
                        TotalTokens = 62,
                        InputTokens = 44,
                        OutputTokens = 18,
                        InputTokensDetails = new AsrRealtimeTokenDetails { TextTokens = 3, AudioTokens = 41 },
                        OutputTokensDetails = new AsrRealtimeTokenDetails { TextTokens = 18 }
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SessionFinished = new(
                "qwen-asr-realtime",
                "session.finished",
                new AsrRealtimeServerEvent(
                    EventId: "event_QS3i9ZVOHMyv5zVqo11bl",
                    Type: "session.finished"));
    }

    public static class AsrRealtimeManual
    {
        private const string GroupName = "qwen-asr-realtime-manual";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        public static readonly SocketMessageSnapshot CommitAudio = new(GroupName, "commit-audio");

        public static readonly SocketMessageSnapshot FinishSession = new(GroupName, "finish-session");

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SessionCreated = new(
                "qwen-asr-realtime-manual",
                "session.created",
                new AsrRealtimeServerEvent(
                    EventId: "event_DvwkryVfJVDRFwlLxtBJA",
                    Type: "session.created",
                    Session:
                    new AsrRealtimeSession
                    {
                        Id = "sess_HjQY45VlFFyO7J499AurE",
                        Object = "realtime.session",
                        Model = "qwen3-asr-flash-realtime",
                        Modalities = new List<string> { "text" },
                        InputAudioFormat = "pcm",
                        SampleRate = 16000,
                        InputAudioTranscription = new AsrRealtimeTranscription { Model = "qwen3-asr-flash-realtime" },
                        TurnDetection = new AsrRealtimeTurnDetection
                        {
                            Type = "server_vad",
                            Threshold = 0.2f,
                            SilenceDurationMs = 800
                        }
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SessionUpdated = new(
                "qwen-asr-realtime-manual",
                "session.updated",
                new AsrRealtimeServerEvent(
                    EventId: "event_U6Y5Aky2kDE1X6jyW3DQO",
                    Type: "session.updated",
                    Session:
                    new AsrRealtimeSession
                    {
                        Id = "sess_HjQY45VlFFyO7J499AurE",
                        Object = "realtime.session",
                        Model = "qwen3-asr-flash-realtime",
                        Modalities = new List<string> { "text" },
                        InputAudioFormat = "pcm",
                        SampleRate = 16000,
                        InputAudioTranscription = new AsrRealtimeTranscription
                        {
                            Language = "zh", Model = "qwen3-asr-flash-realtime"
                        },
                        TurnDetection = null // the manual mode is acknowledged without the member
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            Committed = new(
                "qwen-asr-realtime-manual",
                "input_audio_buffer.committed",
                new AsrRealtimeServerEvent(
                    EventId: "event_FZSSMKeYfbCara1kZyVXz",
                    Type: "input_audio_buffer.committed"));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            ItemCreated = new(
                "qwen-asr-realtime-manual",
                "conversation.item.created",
                new AsrRealtimeServerEvent(
                    EventId: "event_MQDTcjGKbz7WgsSFAnD6d",
                    Type: "conversation.item.created",
                    Item: new AsrRealtimeItem
                    {
                        Object = "realtime.item",
                        Type = "message",
                        Status = "in_progress",
                        Role = "assistant",
                        Content = new List<AsrRealtimeItemContent> { new("input_audio") }
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            TranscriptionText = new(
                "qwen-asr-realtime-manual",
                "conversation.item.input_audio_transcription.text",
                new AsrRealtimeServerEvent(
                    EventId: "event_MdMeEiRjc8z2qpeWDRQIz",
                    Type: "conversation.item.input_audio_transcription.text",
                    ContentIndex: 0,
                    Language: "zh",
                    Emotion: "neutral",
                    Text: string.Empty,
                    Stash: "博客"));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            TranscriptionCompleted = new(
                "qwen-asr-realtime-manual",
                "conversation.item.input_audio_transcription.completed",
                new AsrRealtimeServerEvent(
                    EventId: "event_UamS9oXhXMTlYDHBZhN1f",
                    Type: "conversation.item.input_audio_transcription.completed",
                    ContentIndex: 0,
                    Language: "zh",
                    Emotion: "neutral",
                    Transcript: "博客园成立于二零零四年，代码改变世界。",
                    Usage: new AsrRealtimeUsage
                    {
                        Duration = 4,
                        TotalTokens = 79,
                        InputTokens = 57,
                        OutputTokens = 22,
                        InputTokensDetails = new AsrRealtimeTokenDetails { TextTokens = 3, AudioTokens = 54 },
                        OutputTokensDetails = new AsrRealtimeTokenDetails { TextTokens = 22 }
                    }));

        public static readonly SocketMessageSnapshot<AsrRealtimeServerEvent>
            SessionFinished = new(
                "qwen-asr-realtime-manual",
                "session.finished",
                new AsrRealtimeServerEvent(
                    EventId: "event_AwAOrmAT37UDFkOhQ9ptl",
                    Type: "session.finished"));
    }
}
