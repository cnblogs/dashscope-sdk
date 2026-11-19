namespace Cnblogs.DashScope.Core;

/// <summary>
/// One media item of a video synthesis task.
/// </summary>
/// <param name="Type">The media type: first_frame, last_frame, image_url, video_url, audio_url, lip_sync_tts_speaker_id or lip_sync_tts_content (PixVerse models), reference_image, reference_video, reference_audio (wan3.0), driving_audio or first_clip (wan2.7 image-to-video), video (video editing), file or link (wan3.0).</param>
/// <param name="Url">The url of the media, a Base64 data URI or an asset id. Not set for the PixVerse lipsync types lip_sync_tts_speaker_id / lip_sync_tts_content.</param>
/// <param name="ReferenceVoice">The url of the audio used as the voice of the subject in this media item, overriding the audio of a reference video. Supported by wan2.7 reference-to-video.</param>
/// <param name="RefName">The name of the subject in this media item, referenced in the prompt as "@ref_name" (keep a space after the reference). Must be unique within a request. PixVerse reference-to-video only.</param>
/// <param name="Id">The id of the TTS voice: auto (random) or a voice id from the Bailian documentation. PixVerse lipsync lip_sync_tts_speaker_id only.</param>
/// <param name="Content">The text to synthesize and drive the lip sync, Chinese or English, up to 200 characters. PixVerse lipsync lip_sync_tts_content only.</param>
public record VideoSynthesisMedia(
    string Type,
    string? Url = null,
    string? ReferenceVoice = null,
    string? RefName = null,
    string? Id = null,
    string? Content = null);
