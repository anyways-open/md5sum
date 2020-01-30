using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

[assembly: InternalsVisibleTo("MD5Sum.Tests")]
namespace MD5Sum
{
    internal static class MD5Helper
    {
        private static readonly ThreadLocal<MD5CryptoServiceProvider> Local = 
            new ThreadLocal<MD5CryptoServiceProvider>(() => new MD5CryptoServiceProvider());

        internal const char LineEnding = '\n';
        internal const string Separator = "  ";

        internal static string Hash(Stream file)
        {
            var bytes = Local.Value.ComputeHash(file);
            return BitConverter.ToString(bytes)
                .Replace("-", string.Empty).ToLower();
        }
    }
}