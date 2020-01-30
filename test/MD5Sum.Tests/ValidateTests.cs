using System.Threading.Tasks;
using Xunit;

namespace MD5Sum.Tests
{
    public class ValidateTests
    {
        [Fact]
        public async Task Validate_TryCompareAsync_File1MD5_File1MD5_ShouldReturnTrue()
        {
            await using var md5File1 = EmbeddedResource.Open("MD5Sum.Tests.test_data.file1.json.md5");
            await using var md5File2 = EmbeddedResource.Open("MD5Sum.Tests.test_data.file1.json.md5");
            
            Assert.True(await Validate.TryCompareAsync(md5File1, md5File2));
        }
        
        [Fact]
        public async Task Validate_TryCompareAsync_File1MD5_File2MD5_ShouldReturnFalse()
        {
            await using var md5File1 = EmbeddedResource.Open("MD5Sum.Tests.test_data.file1.json.md5");
            await using var md5File2 = EmbeddedResource.Open("MD5Sum.Tests.test_data.file2.json.md5");
            
            Assert.False(await Validate.TryCompareAsync(md5File1, md5File2));
        }
    }
}