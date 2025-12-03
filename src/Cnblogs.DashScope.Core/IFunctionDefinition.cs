namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// The function definition contract.
    /// </summary>
    public interface IFunctionDefinition
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Descriptions about the functions for model to reference on.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// JSON schema of the function parameters.
        /// </summary>
        public object? Parameters { get; }
    }
}
