using Xunit;

namespace MD5Sum.Tests
{
    public class MD5HelperTests
    {
        [Fact]
        public void MD5Helper_Hash_File1_ShouldBeExactMD5HashString()
        {
            using var sourceFile = EmbeddedResource.Open("MD5Sum.Tests.test_data.file1.json");

            var hash = MD5Helper.Hash(sourceFile);
            Assert.Equal("3c70eb44fda2ca471eee5ffdd92e39d8", hash);
        }
        
        [Fact]
        public void MD5Helper_Hash_File2_ShouldBeExactMD5HashString()
        {
            using var sourceFile = EmbeddedResource.Open("MD5Sum.Tests.test_data.file2.json");

            var hash = MD5Helper.Hash(sourceFile);
            Assert.Equal("459a8e463a4ec1e9236e5f0babab8bb1", hash);
        }
    }
}