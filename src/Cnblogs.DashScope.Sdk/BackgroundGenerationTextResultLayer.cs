using System.Text.Json.Serialization;
using Cnblogs.DashScope.Sdk.Internals;

namespace Cnblogs.DashScope.Sdk;

/// <summary>
/// Represents one layer of background generation text result.
/// </summary>
/// <param name="Idx">The if of the layer.</param>
/// <param name="Type">The type of layer, can be "text" or "text_mask".</param>
/// <param name="Top">The top position of the layer.</param>
/// <param name="Left">The left position of the layer.</param>
/// <param name="Width">The width of the layer.</param>
/// <param name="Height">The height of the layer.</param>
/// <param name="SubType">Available when type is "text". The type of text, can ba "Title" or "SubTitle".</param>
/// <param name="Content">Available when type is "text". The content of the text.</param>
/// <param name="FontFamily">Available when type is "text". The font family of the text.</param>
/// <param name="FontSize">Available when type is "text". The font size of the text.</param>
/// <param name="FontWeight">Available when type is "text". The font weight of the text. e.g. Medium.</param>
/// <param name="FontColor">Available when type is "text". The color of the text in HEX format.</param>
/// <param name="Direction">Available when type is "text". The direction of the text. e.g. vertical or horizontal.</param>
/// <param name="Alignment">Available when type is "text". The alignment of the text, can be center, left or right.</param>
/// <param name="LineHeight">Available when type is "text". The line height of the text.</param>
/// <param name="FontItalic">Available when type is "text". Is the text needs to be italic.</param>
/// <param name="FontLineThrough">Available when type is "text". Is the text has line through.</param>
/// <param name="FontUnderLine">Available when type is "text". Is the text has underline.</param>
/// <param name="TextShadow">Available when type is "text". The text shadow of the text. e.g. 2px 2px #80808080</param>
/// <param name="TextStroke">Available when type is "text". The stroke of the text. e.g. 1px #fffffff0</param>
/// <param name="Opacity">The opacity of the layer.</param>
/// <param name="Radius">Available when type is "text_mask". The border radius of the mask.</param>
/// <param name="BoxShadow">Available when type is "text_mask". The box shadow of the mask.</param>
/// <param name="Color">Available when type is "text_mask". Color of the mask.</param>
/// <param name="Gradient">Available when type is "text_mask". The gradient of the mask.</param>
public record BackgroundGenerationTextResultLayer(
    int Idx,
    string Type,
    int Top,
    int Left,
    int Width,
    int Height,
    string? SubType = null,
    string? Content = null,
    string? FontFamily = null,
    int? FontSize = null,
    string? FontWeight = null,
    string? FontColor = null,
    string? Direction = null,
    string? Alignment = null,
    float? LineHeight = null,
    bool? FontItalic = null,
    bool? FontLineThrough = null,
    bool? FontUnderLine = null,
    [property: JsonConverter(typeof(DashScopeZeroAsNullConvertor))]
    string? TextShadow = null,
    string? TextStroke = null,
    float? Opacity = null,
    int? Radius = null,
    string? BoxShadow = null,
    string? Color = null,
    BackgroundGenerationTextResultGradient? Gradient = null);
