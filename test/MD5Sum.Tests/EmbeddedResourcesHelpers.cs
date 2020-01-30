using System.IO;

namespace MD5Sum.Tests
{
    internal static class EmbeddedResource
    {
        internal static Stream Open(string path)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(
                path);
        }
    }
}