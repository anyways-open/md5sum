using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MD5Sum.Tests
{
    public class WriteTests
    {
        [Fact]
        public async Task Write_MD5FileLine_File1_ShouldWriteHashAndFileName()
        {
            await using var sourceFile = EmbeddedResource.Open("MD5Sum.Tests.test_data.file1.json");
            await using var md5FileWriter = new StreamWriter(new MemoryStream());
            
            await Write.MD5FileLine(md5FileWriter, sourceFile, "file1.json");
            md5FileWriter.Flush();
            
            Assert.Equal("3c70eb44fda2ca471eee5ffdd92e39d8  file1.json\n", await md5FileWriter.BaseStream.ReadToEnd());
        }
        
        [Fact]
        public async Task Write_MD5FileLine_File2_ShouldWriteHashAndFileName()
        {
            await using var sourceFile = EmbeddedResource.Open("MD5Sum.Tests.test_data.file2.json");
            await using var md5FileWriter = new StreamWriter(new MemoryStream());
            
            await Write.MD5FileLine(md5FileWriter, sourceFile, "file2.json");
            md5FileWriter.Flush();
            
            Assert.Equal("459a8e463a4ec1e9236e5f0babab8bb1  file2.json\n", await md5FileWriter.BaseStream.ReadToEnd());
        }

        [Fact]
        public async Task Write_MD5FileLine_File1_File1_ShouldWriteTwoHashesAndFileNames()
        {
            await using var sourceFile1 = EmbeddedResource.Open("MD5Sum.Tests.test_data.file1.json");
            await using var sourceFile2 = EmbeddedResource.Open("MD5Sum.Tests.test_data.file2.json");
            await using var md5FileWriter = new StreamWriter(new MemoryStream());
            
            await Write.MD5FileLine(md5FileWriter, sourceFile1, "file1.json");
            await Write.MD5FileLine(md5FileWriter, sourceFile2, "file2.json");
            md5FileWriter.Flush();
            
            Assert.Equal("3c70eb44fda2ca471eee5ffdd92e39d8  file1.json\n459a8e463a4ec1e9236e5f0babab8bb1  file2.json\n", 
                await md5FileWriter.BaseStream.ReadToEnd());
        }
    }
}