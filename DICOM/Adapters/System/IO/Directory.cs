using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    public static class Directory
    {
        #region CONSTRUCTORS

        static Directory()
        {
            Root = KnownFolders.DocumentsLibrary;
        }

        #endregion

        #region PROPERTIES

        public static StorageFolder Root { get; set; }

        #endregion

        public static bool Exists(string path)
        {
            try
            {
                var folder = Task.Run(async () => await Root.GetFolderAsync(path)).Result;
                return folder != null;
            }
            catch
            {
                return false;
            }
        }

        public static void CreateDirectory(string path)
        {
            var folder = Task.Run(async () => await Root.CreateFolderAsync(path, CreationCollisionOption.ReplaceExisting)).Result;
        }

        public static string[] GetDirectories(string path)
        {
            var folders = Task.Run(async () =>
                                             {
                                                 var root = await Root.GetFolderAsync(path);
                                                 return await root.GetFoldersAsync();
                                             }).Result;
            return folders.Select(folder => folder.Name).ToArray();
        }

        public static string[] GetFiles(string path, string searchPattern = "*")
        {
            var files = Task.Run(async () =>
                                           {
                                               var root = await Root.GetFolderAsync(path);
                                               return await root.GetFilesAsync();
                                           }).Result;

            // TODO Handle scenario with "real" search patterns
            return files.Select(folder => folder.Name).ToArray();
        }
    }
}