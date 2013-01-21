using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    [TestClass]
    public class DirectoryTests
    {
		private static readonly string TopDirectoryToCreate = Path.Combine(IOHelper.GetMyDocumentsPath(), "TemporaryFolderName");
		private static readonly string SubDirectoryToCreate = Path.Combine(TopDirectoryToCreate, "With sub-folder");

		[TestCleanup]
		public void CleanUp()
		{
			try
			{
				Task.Run(async () =>
						   {
							   var subFolder = await StorageFolder.GetFolderFromPathAsync(SubDirectoryToCreate);
							   await subFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
							   var topFolder = await StorageFolder.GetFolderFromPathAsync(TopDirectoryToCreate);
							   await topFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
						   }).Wait();
			}
			catch
			{
			}
		}

        [TestMethod]
        public void Exists_NonExistingDirectory_ReturnsFalse()
        {
            var expected = false;
            var actual = Directory.Exists(Path.Combine(IOHelper.GetMyDocumentsPath(), "HowSillyOfMeToExpectADirectoryLikeThisToExist"));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Exists_ExistingDirectory_ReturnsTrue()
        {
            var expected = true;
            var actual = Directory.Exists(Path.Combine(IOHelper.GetMyDocumentsPath(), "Visual Studio 2012\\Projects"));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDirectory_ArbitraryName_ShouldExistAfterCreation()
        {
            Directory.CreateDirectory(SubDirectoryToCreate);
            var expected = true;
            var actual = Directory.Exists(SubDirectoryToCreate);
            Assert.AreEqual(expected, actual);
        }

		[TestMethod]
		public void GetDirectories_DocumentsPath_ShouldContainVS2012folder()
		{
			var dirs = Directory.GetDirectories(IOHelper.GetMyDocumentsPath());
			var expected = true;
			var actual = dirs.Any(dir => dir.Equals("Visual Studio 2012"));
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetFiles_ExistingFilesDefaultPattern_AllFilesObtained()
		{
			CreateTemporaryFiles();
			var files = Directory.GetFiles(SubDirectoryToCreate);
			DeleteTemporaryFiles();

			var expected = 2;
			var actual = files.Length;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetFiles_ExistingFilesDiscriminatingPattern_SubsetOfFilesObtained()
		{
			CreateTemporaryFiles();
			var files = Directory.GetFiles(SubDirectoryToCreate, "test_*.*");
			DeleteTemporaryFiles();

			var expected = 1;
			var actual = files.Length;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GetFiles_ExistingFilesAllFilesDiscriminated_NoFilesObtained()
		{
			CreateTemporaryFiles();
			var files = Directory.GetFiles(SubDirectoryToCreate, "test_*.dcm");
			DeleteTemporaryFiles();

			var expected = 0;
			var actual = files.Length;
			Assert.AreEqual(expected, actual);
		}

	    private static void CreateTemporaryFiles()
	    {
		    Directory.CreateDirectory(SubDirectoryToCreate);
		    Task.Run(async () =>
			                   {
				                   var folder = await StorageFolder.GetFolderFromPathAsync(SubDirectoryToCreate);
				                   await folder.CreateFileAsync("test1.dcm");
				                   await folder.CreateFileAsync("test_2.dic");
			                   }).Wait();
	    }

		private static void DeleteTemporaryFiles()
		{
			Task.Run(async () =>
			{
				var folder = await StorageFolder.GetFolderFromPathAsync(SubDirectoryToCreate);
				var files = await folder.GetFilesAsync();
				foreach (var file in files)
				{
					await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
				}
			}).Wait();
		}
    }
}
