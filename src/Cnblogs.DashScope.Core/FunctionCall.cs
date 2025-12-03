namespace Cnblogs.DashScope.Core
{
    /// <summary>
    /// Represents a call to function.
    /// </summary>
    public class FunctionCall
    {
        /// <summary>
        /// Create an empty function call.
        /// </summary>
        public FunctionCall()
        {
        }

        /// <summary>
        /// Create a function call.
        /// </summary>
        /// <param name="name">Name of the function to be called.</param>
        /// <param name="arguments">Arguments that passed to the function.</param>
        public FunctionCall(string name, string? arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        /// <summary>Name of the function to call.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Arguments of this call, usually a json string.</summary>
        public string? Arguments { get; set; }
    }
}
