using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MD5Sum
{
    /// <summary>
    /// Contains functionality to validate MD5 files.
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Validates if all the hashes in the given md5 file are valid using the given files.
        /// </summary>
        /// <param name="md5File">The md5 file to use for validation.</param>
        /// <param name="files">The files that need validation.</param>
        /// <returns>True if all hashes found in the md5 file are valid, false if one of the hashes was invalid or not all files could be validated.</returns>
        public static Task<bool> TryAsync(string md5File, params string[] files)
        {
            return TryAsync(md5File, (IEnumerable<string>)files);
        }
        
        /// <summary>
        /// Validates if all the hashes in the given md5 file are valid using the given files.
        /// </summary>
        /// <param name="md5File">The md5 file to use for validation.</param>
        /// <param name="files">The files that need validation.</param>
        /// <returns>True if all hashes found in the md5 file are valid, false if one of the hashes was invalid or not all files could be validated.</returns>
        public static Task<bool> TryAsync(string md5File, IEnumerable<string> files)
        {
            if (!File.Exists(md5File)) throw new FileNotFoundException("md5 file not found.");
            using var stream = File.OpenRead(md5File);
            return TryAsync(stream, files);
        }
        
        /// <summary>
        /// Validates if all the hashes in the given md5 file are valid using the given files.
        /// </summary>
        /// <param name="md5FileStream">The md5 file to use for validation.</param>
        /// <param name="files">The files that need validation.</param>
        /// <returns>True if all hashes found in the md5 file are valid, false if one of the hashes was invalid or not all files could be validated.</returns>
        internal static async Task<bool> TryAsync(Stream md5FileStream, IEnumerable<string> files)
        {
            var streamReader = new StreamReader(md5FileStream);

            var fileContent = streamReader.ReadToEnd();
            if (string.IsNullOrEmpty(fileContent)) return true;
            var lines = fileContent.Split(MD5Helper.LineEnding);

            var filesByName = files.ToDictionary(Path.GetFileName);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                
                var hashAndName = line.Split(new[] {MD5Helper.Separator},
                    StringSplitOptions.None);
                if (hashAndName.Length < 2) throw new Exception("Invalid md5 file.");
                if (!filesByName.TryGetValue(hashAndName[1], out var file)) continue; // it's ok if a file does not have to be checked.

                filesByName.Remove(hashAndName[1]);
                if (!File.Exists(file)) throw new FileNotFoundException("One of the files to check doesn't exist.");
                using var fileStream = File.OpenRead(file);

                var fileHash = MD5Helper.Hash(fileStream);

                if (fileHash != hashAndName[0]) return false;
            }

            if (filesByName.Count > 0) return false; // could not validate all files.

            return true;
        }

        /// <summary>
        /// Matches the two files, returns true if they are identical.
        /// </summary>
        /// <param name="md5file1">The first file.</param>
        /// <param name="md5file2">The second file.</param>
        /// <returns>True if files are identical, false otherwise.</returns>
        public static async Task<bool> TryCompareAsync(string md5file1, string md5file2)
        {
            if (!File.Exists(md5file1)) throw new FileNotFoundException("md5 file1 not found.");
            if (!File.Exists(md5file2)) throw new FileNotFoundException("md5 file2 not found.");
            
            using var stream1 = File.OpenRead(md5file1);
            using var stream2 = File.OpenRead(md5file2);

            return await TryCompareAsync(stream1, stream2);
        }

        internal static async Task<bool> TryCompareAsync(Stream stream1, Stream stream2)
        {
            while (true)
            {
                var stream1Byte = stream1.ReadByte();
                var stream2Byte = stream2.ReadByte();

                if (stream1Byte == stream2Byte)
                {
                    if (stream1Byte == -1) break;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}