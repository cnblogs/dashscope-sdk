using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Cnblogs.DashScope.Core.Internals;

// back-porting from dotnet/runtime
internal abstract class JsonSeparatorNamingPolicy : JsonNamingPolicy
{
    private readonly bool _lowercase;
    private readonly char _separator;

    internal JsonSeparatorNamingPolicy(bool lowercase, char separator)
    {
        _lowercase = lowercase;
        _separator = separator;
    }

    public sealed override string ConvertName(string? name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return ConvertNameCore(_separator, _lowercase, name.AsSpan());
    }

    private static string ConvertNameCore(char separator, bool lowercase, ReadOnlySpan<char> chars)
    {
        var rentedBuffer = (char[]?)null;
        var minimumLength = (int)(1.2 * chars.Length);
        var destination = minimumLength > 128 /*0x80*/
            ? (Span<char>)(rentedBuffer = ArrayPool<char>.Shared.Rent(minimumLength))
            : stackalloc char[128 /*0x80*/];
        var separatorState = SeparatorState.NotStarted;
        var charsWritten = 0;
        for (var index = 0; index < chars.Length; ++index)
        {
            var c = chars[index];
            var unicodeCategory = char.GetUnicodeCategory(c);
            switch (unicodeCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                    switch (separatorState)
                    {
                        case SeparatorState.UppercaseLetter:
                            if (index + 1 < chars.Length && char.IsLower(chars[index + 1]))
                            {
                                WriteChar(separator, ref destination);
                            }

                            break;
                        case SeparatorState.LowercaseLetterOrDigit:
                        case SeparatorState.SpaceSeparator:
                            WriteChar(separator, ref destination);
                            break;
                    }

                    if (lowercase)
                        c = char.ToLowerInvariant(c);
                    WriteChar(c, ref destination);
                    separatorState = SeparatorState.UppercaseLetter;
                    break;
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (separatorState == SeparatorState.SpaceSeparator)
                        WriteChar(separator, ref destination);
                    if (!lowercase && unicodeCategory == UnicodeCategory.LowercaseLetter)
                        c = char.ToUpperInvariant(c);
                    WriteChar(c, ref destination);
                    separatorState = SeparatorState.LowercaseLetterOrDigit;
                    break;
                case UnicodeCategory.SpaceSeparator:
                    if (separatorState != SeparatorState.NotStarted)
                    {
                        separatorState = SeparatorState.SpaceSeparator;
                    }

                    break;
                default:
                    WriteChar(c, ref destination);
                    separatorState = SeparatorState.NotStarted;
                    break;
            }
        }

        var str = destination.Slice(0, charsWritten).ToString();
        if (rentedBuffer != null)
        {
            destination.Slice(0, charsWritten).Clear();
            ArrayPool<char>.Shared.Return(rentedBuffer);
        }

        return str;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void WriteChar(char value, ref Span<char> destination)
        {
            if (charsWritten == destination.Length)
                ExpandBuffer(ref destination);
            destination[charsWritten++] = value;
        }

        void ExpandBuffer(ref Span<char> destination)
        {
            var destination1 = ArrayPool<char>.Shared.Rent(checked(destination.Length * 2));
            destination.CopyTo((Span<char>)destination1);
            if (rentedBuffer != null)
            {
                destination.Slice(0, charsWritten).Clear();
                ArrayPool<char>.Shared.Return(rentedBuffer);
            }

            rentedBuffer = destination1;
            destination = (Span<char>)rentedBuffer;
        }
    }

    private enum SeparatorState
    {
        NotStarted,
        UppercaseLetter,
        LowercaseLetterOrDigit,
        SpaceSeparator,
    }
}
