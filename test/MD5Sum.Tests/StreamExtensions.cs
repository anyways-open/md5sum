using System.IO;
using System.Threading.Tasks;

namespace MD5Sum.Tests
{
    internal static class StreamExtensions
    {
        public static async Task<string> ReadToEnd(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync();
        }
    }
}