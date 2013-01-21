using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class FileTests
    {
         [TestMethod]
         public void Exists_ExistingFile_ReturnsTrue()
         {
             var fileName = Path.Combine(IOHelper.GetMyDocumentsPath(), @"Visual Studio 2012\Settings\CurrentSettings.vssettings");
             var expected = true;
             var actual = File.Exists(fileName);
             Assert.AreEqual(expected, actual);
         }

         [TestMethod]
         public void Exists_NonExistingFile_ReturnsFalse()
         {
             var fileName = Path.Combine(IOHelper.GetMyDocumentsPath(), @"Visual Studio 2012\SomeTypicallySillyName.dcm");
             var expected = false;
             var actual = File.Exists(fileName);
             Assert.AreEqual(expected, actual);
         }

        [TestMethod]
        public void WriteAllBytes_ReadAllBytes_CreatesNonZeroLengthFile()
        {
            var expected = new byte[] { 65, 66, 67, 68 };
            var fileName = Path.Combine(IOHelper.GetMyDocumentsPath(), "test.dcm");
            File.WriteAllBytes(fileName, expected);
            var actual = File.ReadAllBytes(fileName);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}