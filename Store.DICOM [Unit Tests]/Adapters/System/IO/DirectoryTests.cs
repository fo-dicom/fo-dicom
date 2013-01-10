using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class DirectoryTests
    {
        [TestMethod]
        public void Exists_NonExistingDirectory_ReturnsFalse()
        {
            var expected = false;
            var actual = Directory.Exists("HowSillyOfMeToExpectADirectoryLikeThisToExist");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Exists_ExistingDirectory_ReturnsTrue()
        {
            var expected = true;
            var actual = Directory.Exists("Visual Studio 2012");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDirectory_ArbitraryName_ShouldExistAfterCreation()
        {
            var folderName = "TemporaryFolderName";
            Directory.CreateDirectory(folderName);

            var expected = true;
            var actual = Directory.Exists(folderName);
            Assert.AreEqual(expected, actual);
        }
    }
}
