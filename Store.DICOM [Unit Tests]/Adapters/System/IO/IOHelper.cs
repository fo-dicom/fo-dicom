using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
	internal static class IOHelper
	{
		internal static string GetMyDocumentsPath()
		{
			return Task.Run(async () =>
				                      {
					                      var folders = await KnownFolders.DocumentsLibrary.GetFoldersAsync();
					                      return Path.GetDirectoryName(folders.First().Path);
				                      }).Result;
		}
	}
}