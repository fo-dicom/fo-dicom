using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class FileStreamTests
    {
        [TestMethod]
        public void Constructor_WriteByte_Close_FileOfNonoZeroLengthCreated()
        {
            var fileName = Path.Combine(Mock.System.IO.Path.GetTempPath(), "test.dcm");
            var stream = new FileStream(fileName, FileMode.Create);
            stream.WriteByte(65);
            stream.Close();

            // TODO Add verification that file contains ASCII character 65 (A)
        }
    }
}