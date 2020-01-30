using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MD5Sum
{
    /// <summary>
    /// Contains functionality to write MD5 files.
    /// </summary>
    public static class Write
    {
        /// <summary>
        /// Writes an md5 file for a single file.
        /// </summary>
        /// <param name="file">The file to hash.</param>
        /// <param name="md5File">The file to write to.</param>
        public static async Task MD5FileAsync(string file, string md5File = null)
        {
            if (md5File == null) md5File = file + ".md5";
            await MD5FileAsync(new[] { file }, md5File);
        }
        
        /// <summary>
        /// Writes an md5 file for the given files.
        /// </summary>
        /// <param name="files">The files to hash.</param>
        /// <param name="md5File">The file to write to.</param>
        public static async Task MD5FileAsync(IEnumerable<string> files, string md5File)
        {
            using var stream = File.Open(md5File, FileMode.Create);
            await MD5File(files, stream);
        }
        
        internal static async Task MD5File(IEnumerable<string> files, Stream stream)
        { 
            using var streamWriter = new StreamWriter(stream);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (!fileInfo.Exists) throw new FileNotFoundException("One of the files to hash was not found.", file);
                using var fileStream = fileInfo.OpenRead();
                await MD5FileLine(streamWriter, fileStream, fileInfo.Name);
            }
        }
        
        internal static async Task MD5FileLine(StreamWriter writer, Stream fileStream, string fileName)
        {
            // create hash.
            var hash = MD5Helper.Hash(fileStream);
            
            // write hash.
            await writer.WriteAsync(hash);

            // write spaces and filename.
            await writer.WriteAsync(' ');
            await writer.WriteAsync(' ');
            await writer.WriteAsync(fileName);
            
            // writer proper line ending.
            await writer.WriteAsync('\n');
        }
    }
}
