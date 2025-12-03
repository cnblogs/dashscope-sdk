namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Outputs from the tools.
    /// </summary>
    public class ToolInfoOutput
    {
        /// <summary>
        /// Output from the code interpreter.
        /// </summary>
        public ToolInfoCodeInterpreterOutput? CodeInterpreter { get; set; }

        /// <summary>
        /// Type of the tool.
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
