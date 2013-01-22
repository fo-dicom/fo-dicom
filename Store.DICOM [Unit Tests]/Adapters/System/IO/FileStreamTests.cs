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
	    private readonly string _fileName = IOHelper.GetMyDocumentsPath(@"Test folder\test-file.dic");

		[TestCleanup]
		public void CleanUp()
		{
			File.Delete(_fileName);
			try
			{
				Task.Run(async () =>
				{
					var folder =
						await StorageFolder.GetFolderFromPathAsync(IOHelper.GetMyDocumentsPath("Test folder"));
					await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
				}).Wait();
			}
			catch
			{
			}
		}

		[TestMethod]
		public void Write_SomeData_SufficientlyWrittenToFile()
		{
			var bytes = new byte[] { 65, 66, 67, 68 };
			using (var stream = new FileStream(_fileName, FileMode.CreateNew))
			{
				stream.Write(bytes, 0, 3);
				stream.Write(bytes, 3, 1);
			}

			var expected = "ABCD";
			var actual = Task.Run(async () => await PathIO.ReadTextAsync(_fileName)).Result;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Read_SomeData_SufficientlyReadFromFile()
		{
			var expected = new byte[] { 69, 70, 71, 72 };
			Task.Run(async () =>
				               {
					               var file = await FileHelper.CreateStorageFileAsync(_fileName);
					               await FileIO.WriteBytesAsync(file, expected);
				               }).Wait();

			var actual = new byte[4];
			using (var stream = new FileStream(_fileName, FileMode.Open))
			{
				stream.Read(actual, 0, 1);
				stream.Read(actual, 1, 3);
			}

			CollectionAssert.AreEqual(expected, actual);
		}
	}
}