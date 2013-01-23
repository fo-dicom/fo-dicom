using System.Threading.Tasks;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
	internal static class FileHelper
	{
		#region METHODS

		internal static async Task<StorageFile> CreateStorageFileAsync(string path)
		{
			var folderName = Path.GetDirectoryName(path);
			if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
			var folder = await StorageFolder.GetFolderFromPathAsync(folderName);

			var fileName = Path.GetFileName(path);
			return await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
		}

		internal static async Task<StorageFile> GetStorageFileAsync(string path)
		{
			var folderName = Path.GetDirectoryName(path);
			if (!Directory.Exists(folderName)) throw new FileNotFoundException("Cannot find specified folder.");
			var folder = await StorageFolder.GetFolderFromPathAsync(folderName);

			var fileName = Path.GetFileName(path);
			return await folder.GetFileAsync(fileName);
		}
		
		#endregion
	}
}