using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;

// ReSharper disable CheckNamespace

namespace System.IO
// ReSharper restore CheckNamespace
{
	[TestClass]
	public class FileTests
	{
		[TestCleanup]
		public void CleanUp()
		{
			try
			{
				Task.Run(async () =>
						   {
							   var folder =
								   await StorageFolder.GetFolderFromPathAsync(IOHelper.GetMyDocumentsPath("Temporary folder"));
							   await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
						   }).Wait();
			}
			catch
			{
			}
		}

		[TestMethod]
		public void Exists_ExistingFile_ReturnsTrue()
		{
			var fileName = IOHelper.GetMyDocumentsPath(@"Visual Studio 2012\Settings\CurrentSettings.vssettings");
			var expected = true;
			var actual = File.Exists(fileName);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Exists_NonExistingFile_ReturnsFalse()
		{
			var fileName = IOHelper.GetMyDocumentsPath(@"Visual Studio 2012\SomeTypicallySillyName.dcm");
			var expected = false;
			var actual = File.Exists(fileName);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void WriteAllBytes_ReadAllBytes_CreatesNonZeroLengthFile()
		{
			var expected = new byte[] { 65, 66, 67, 68 };
			var fileName = IOHelper.GetMyDocumentsPath("test.dcm");
			File.WriteAllBytes(fileName, expected);
			var actual = File.ReadAllBytes(fileName);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Delete_FileInSubfolder_ConfirmedDeleted()
		{
			var fileName = IOHelper.GetMyDocumentsPath(@"Temporary folder\temporary file.dcm");
			File.Create(fileName);
			Assert.IsTrue(File.Exists(fileName));
			File.Delete(fileName);
			Assert.IsFalse(File.Exists(fileName));
		}
	}
}