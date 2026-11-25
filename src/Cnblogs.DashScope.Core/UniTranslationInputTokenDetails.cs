namespace Cnblogs.DashScope.Core;

/// <summary>
/// The breakdown of the input tokens of an omni-modal translation by content type.
/// </summary>
/// <param name="AudioTokens">The count of the tokens of the audio.</param>
/// <param name="CharacterTokens">The count of the tokens of the texts.</param>
/// <param name="DocumentTokens">The count of the tokens of the documents.</param>
/// <param name="ImageTokens">The count of the tokens of the images.</param>
public record UniTranslationInputTokenDetails(int AudioTokens, int CharacterTokens, int DocumentTokens, int ImageTokens);
