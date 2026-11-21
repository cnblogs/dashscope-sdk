using System.Text.Json;
using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public partial class Snapshots
{
    public static class TtsRealtime
    {
        private const string GroupName = "qwen-tts-realtime";

        // Client events. The SDK generates the event_id, tests replace it with the fixture value
        // before comparing the sent json.
        public static readonly SocketMessageSnapshot SessionUpdate = new(GroupName, "session-update");

        public static readonly SocketMessageSnapshot AppendText = new(GroupName, "append-text");

        public static readonly SocketMessageSnapshot CommitText = new(GroupName, "commit-text");

        public static readonly SocketMessageSnapshot FinishSession = new(GroupName, "finish-session");

        // Server events.
        public static readonly SocketMessageSnapshot<TtsRealtimeServerEvent> SessionCreated = new(
            GroupName,
            "session.created",
            new TtsRealtimeServerEvent(
                EventId: "event_F5K1Cs9fECMkfWLqPzcO4",
                Type: "session.created",
                Session: new TtsRealtimeSession
                {
                    Id = "sess_RRxqkAnh2nwJebJeHof2u",
                    Object = "realtime.session",
                    Mode = "server_commit",
                    Model = "qwen3-tts-flash-realtime",
                    Voice = "Cherry",
                    ResponseFormat = "pcm",
                    SampleRate = 24000
                }));

        public static readonly SocketMessageSnapshot<TtsRealtimeServerEvent> SessionUpdated = new(
            GroupName,
            "session.updated",
            new TtsRealtimeServerEvent(
                EventId: "event_MpIHhpoCsDpCRB6a6xw2H",
                Type: "session.updated",
                Session: new TtsRealtimeSession
                {
                    Id = "sess_RRxqkAnh2nwJebJeHof2u",
                    Object = "realtime.session",
                    Mode = "server_commit",
                    Model = "qwen3-tts-flash-realtime",
                    Voice = "Cherry",
                    LanguageType = "chinese",
                    ResponseFormat = "pcm",
                    SampleRate = 24000
                }));

        public static readonly SocketMessageSnapshot<TtsRealtimeServerEvent> Committed = new(
            GroupName,
            "input_text_buffer.committed",
            new TtsRealtimeServerEvent(
                EventId: "event_DTO8lrgjGmSInjtx81gZh",
                Type: "input_text_buffer.committed",
                ItemId: string.Empty));

        public static readonly SocketMessageSnapshot<TtsRealtimeServerEvent> ResponseCreated = new(
            GroupName,
            "response.created",
            new TtsRealtimeServerEvent(
                EventId: "event_IznYgpLKqZUj179Lor6wE",
                Type: "response.created",
                Response: new TtsRealtimeResponse(
                    Id: "resp_Wt8HkKrjywBviOK2Uvwlv",
                    Object: "realtime.response",
                    ConversationId: string.Empty,
                    Status: "in_progress",
                    Voice: "Cherry",
                    Output: new List<TtsRealtimeItem>())));

        public static readonly SocketMessageSnapshot<TtsRealtimeServerEvent> ResponseDone = new(
            GroupName,
            "response.done",
            new TtsRealtimeServerEvent(
                EventId: "event_LYYVCVeJZZ8NdhQiqbWaN",
                Type: "response.done",
                Response: new TtsRealtimeResponse(
                    Id: "resp_Wt8HkKrjywBviOK2Uvwlv",
                    Object: "realtime.response",
                    ConversationId: string.Empty,
                    Status: "completed",
                    Modalities: new List<string> { "text", "audio" },
                    Voice: "Cherry",
                    Output: new List<TtsRealtimeItem>
                    {
                        new(
                            Id: "item_MLvkYqld4h78BIfG7t4Ew",
                            Object: "realtime.item",
                            Type: "message",
                            Status: "completed",
                            Role: "assistant",
                            Content: new List<TtsRealtimeContentPart> { new(Type: "audio", Transcript: string.Empty) })
                    },
                    Usage: new TtsRealtimeUsage(
                        Characters: 14,
                        InputTokens: 6,
                        OutputTokens: 18,
                        TotalTokens: 24,
                        InputTokensDetails: new TtsRealtimeTokenDetails(TextTokens: 6),
                        OutputTokensDetails: new TtsRealtimeTokenDetails(TextTokens: 0, AudioTokens: 18)))));

        public static readonly SocketMessageSnapshot<TtsRealtimeServerEvent> SessionFinished = new(
            GroupName,
            "session.finished",
            new TtsRealtimeServerEvent(
                EventId: "event_YNxKzjhzGM271YV1gFtqL",
                Type: "session.finished"));

        public static readonly string AudioDeltaBase64 = JsonDocument.Parse(
                System.IO.File.ReadAllText(Path.Combine("RawHttpData", "socket-qwen-tts-realtime.response.audio.delta.json")))
            .RootElement.GetProperty("delta").GetString()!;

        public static readonly SocketMessageSnapshot<TtsRealtimeServerEvent> AudioDelta = new(
            GroupName,
            "response.audio.delta",
            new TtsRealtimeServerEvent(
                EventId: "event_YYyeGm2gy9J6NLef8jTX8",
                Type: "response.audio.delta",
                ItemId: "item_MLvkYqld4h78BIfG7t4Ew",
                OutputIndex: 0,
                ContentIndex: 0,
                Delta: AudioDeltaBase64));

        public static readonly byte[] Audio =
            System.IO.File.ReadAllBytes(Path.Combine("RawHttpData", "socket-qwen-tts-realtime.audio.pcm"))[..1000];
    }
}
