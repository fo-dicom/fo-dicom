using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

// ReSharper disable CheckNamespace
namespace Override.System.IO
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void GetTempPath_ReturnsNonZeroLengthPath()
        {
            var tempPath = Path.GetTempPath();
            Assert.IsTrue(tempPath.Length > 0);
        }

        [TestMethod]
        public void GetTempFileName_ShouldStartWithTempPath()
        {
            var tempFileName = Path.GetTempFileName();
            Assert.IsTrue(tempFileName.StartsWith(Path.GetTempPath()));
        }
    }
}