using System.Text.Json.Serialization;
using Cnblogs.DashScope.Core.Internals;

namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents file id of the DashScope file.
    /// </summary>
    [JsonConverter(typeof(DashScopeFileIdConvertor))]
    public readonly struct DashScopeFileId
    {
        /// <summary>
        /// Check if two DashScopeFileId equals.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DashScopeFileId other)
        {
            return Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is DashScopeFileId other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Initialize a DashScopeFileId.
        /// </summary>
        /// <param name="fileId">The id of the file.</param>
        public DashScopeFileId(string fileId)
        {
            Value = fileId;
        }

        /// <summary>
        /// The value of the file id.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Get url for chat messages.
        /// </summary>
        /// <returns>Url like <c>fileid://xxxxxxx</c></returns>
        public string ToUrl() => "fileid://" + Value;

        /// <inheritdoc />
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Convert string to DashScopeFileId implicitly.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns></returns>
        public static implicit operator DashScopeFileId(string value) => new(value);

        /// <summary>
        /// Check if two file id is same.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DashScopeFileId left, DashScopeFileId right) => left.Value == right.Value;

        /// <summary>
        /// Check if two file id is not same.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DashScopeFileId left, DashScopeFileId right) => !(left == right);
    }
}
