using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MD5Sum.Tests.Functional
{
    public class ValidateTests
    {
        [Fact]
        public async Task Validate_TryAsync_File1_File1MD5_ShouldReturnTrue()
        {
            Assert.True(await Validate.TryAsync(Path.Combine("test-data", "file1.json.md5"),
                Path.Combine("test-data", "file1.json")));
        }
        
        [Fact]
        public async Task Validate_TryAsync_File1_File1MD5Invalid_ShouldReturnFalse()
        {
            Assert.False(await Validate.TryAsync(Path.Combine("test-data", "file1.json.invalid.md5"),
                Path.Combine("test-data", "file1.json")));
        }
    }
}