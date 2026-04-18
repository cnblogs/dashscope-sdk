namespace Cnblogs.DashScope.Core;

/// <summary>
/// Options for code interpreter
/// </summary>
public interface ICodeInterpreterParameter
{
    /// <summary>
    /// Allow model to call internal Python interpreter. Can not use with tools.
    /// </summary>
    bool? EnableCodeInterpreter { get; }
}
