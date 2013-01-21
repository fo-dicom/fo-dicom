using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class FileStreamTests
    {
        [TestMethod]
        public void Constructor_WriteByte_Close_FileOfNonZeroLengthCreated()
        {
            var fileName = Path.Combine(IOHelper.GetMyDocumentsPath(), "test.dcm");
            var stream = new FileStream(fileName, FileMode.Create);
            stream.WriteByte(65);
            stream.Close();

	        var expected = "A";
	        var actual = Task.Run(async () => await PathIO.ReadTextAsync(fileName)).Result;
	        Assert.AreEqual(expected, actual);
        }
    }
}