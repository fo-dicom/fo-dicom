using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;

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

		[TestMethod]
		public void GetTempFileName_TryCreate_ShouldNotThrowException()
		{
			var path = Path.GetTempFileName();
			try
			{
				Task.Run(async () =>
					               {
						               var folder = await 
							               StorageFolder.GetFolderFromPathAsync(global::System.IO.Path.GetDirectoryName(path));
						               var file = await folder.CreateFileAsync(global::System.IO.Path.GetFileName(path));
						               await file.DeleteAsync();
					               }).Wait();
			}
			catch (Exception e)
			{
				Assert.Fail("Failed to access temporary file, message: {0}", e.Message);
			}
		}
    }
}