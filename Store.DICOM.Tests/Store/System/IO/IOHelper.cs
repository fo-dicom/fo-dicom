using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
	internal static class IOHelper
	{
		internal static string GetMyDocumentsPath(string relativePath)
		{
			return Task.Run(async () =>
				                      {
					                      var folders = await KnownFolders.DocumentsLibrary.GetFoldersAsync();
					                      return Path.Combine(Path.GetDirectoryName(folders.First(f => !f.Path.ToLowerInvariant().Contains("skydrive")).Path), relativePath);
				                      }).Result;
		}
	}
}