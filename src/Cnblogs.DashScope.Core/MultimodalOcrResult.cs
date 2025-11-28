namespace Cnblogs.DashScope.Core;

/// <summary>
/// OCR result from the model.
/// </summary>
/// <param name="WordsInfo">The words that model recognized.</param>
/// <param name="KvResult">Meta info that extracted from the image.</param>
public record MultimodalOcrResult(List<MultimodalOcrWordInfo>? WordsInfo, Dictionary<string, object?> KvResult);
