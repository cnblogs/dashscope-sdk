namespace Cnblogs.DashScope.Core;

/// <summary>
/// Word location and info recognized by the model.
/// </summary>
/// <param name="Text">OCR result.</param>
/// <param name="RotateRect">Four point of the word rect, (0, 0) is the left-top of the image. The points are clockwise, starting from the left-top of the rect. e.g. [x1, y1, x2, y2, x3, y3, x4, y4]</param>
/// <param name="Location">Another presentation of the word rect. First two are the center point of the rect, then follows the width and height. The last value is rect's rotate angle from the landscape. e.g. [center_x, center_y, width, height, angle]</param>
public record MultimodalOcrWordInfo(string Text, int[] RotateRect, int[] Location);
