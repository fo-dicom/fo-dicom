using System.Threading.Tasks;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    public static class Directory
    {
        public static bool Exists(string path)
        {
            try
            {
                var folder = Task.Run(async () => await KnownFolders.DocumentsLibrary.GetFolderAsync(path)).Result;
                return folder != null;
            }
            catch
            {
                return false;
            }
        }

        public static void CreateDirectory(string path)
        {
            var folder = Task.Run(async () => await KnownFolders.DocumentsLibrary.CreateFolderAsync(path)).Result;
        }
    }
}