using System.Reflection;

namespace Cnblogs.DashScope.Core.Internals
{
    internal class DashScopeEmbeddedResource
    {
        private static readonly string? Namespace = typeof(DashScopeEmbeddedResource).Namespace;

        internal static Stream ReadBpeFile()
        {
            return Read("qwen.tiktoken");
        }

        private static Stream Read(string fileName)
        {
            // Get the current assembly. Note: this class is in the same assembly where the embedded resources are stored.
            var assembly = typeof(DashScopeEmbeddedResource).GetTypeInfo().Assembly;
            if (assembly == null)
            {
                throw new InvalidOperationException($"[{Namespace}] {fileName} assembly not found");
            }

            // Resources are mapped like types, using the namespace and appending "." (dot) and the file name
            var resourceName = $"{Namespace}." + fileName;
            var resource = assembly.GetManifestResourceStream(resourceName);
            if (resource == null)
            {
                throw new InvalidOperationException($"{resourceName} resource not found");
            }

            // Return the resource content, in text format.
            return resource;
        }
    }
}
