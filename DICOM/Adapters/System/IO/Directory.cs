using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Windows.Storage;
using Windows.Storage.Search;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    internal static class Directory
    {
		#region FIELDS

	    private const char DirectorySeparatorChar = '\\';

	    private static readonly Logger Logger;

	    #endregion

		#region CONSTRUCTORS

        static Directory()
        {
	        Logger = LogManager.GetLogger(typeof(Directory));
            Root = KnownFolders.DocumentsLibrary;
        }

        #endregion

        #region PROPERTIES

        internal static StorageFolder Root { get; set; }

        #endregion

        internal static bool Exists(string path)
        {
            try
            {
                var folder = Task.Run(async () => await GetStorageFolder(path)).Result;
                return folder != null;
            }
            catch
            {
                return false;
            }
        }

	    internal static DirectoryInfo CreateDirectory(string path)
	    {
		    try
		    {
			    Task.Run(async () =>
				                   {
					                   var subFolders = new Stack<string>();
					                   var root = Path.GetDirectoryName(path.TrimEnd(DirectorySeparatorChar) + DirectorySeparatorChar);
					                   while (!String.IsNullOrEmpty(root) && !Exists(root))
					                   {
						                   subFolders.Push(root.Substring(root.LastIndexOf(DirectorySeparatorChar) + 1));
						                   root = Path.GetDirectoryName(root);
					                   }
					                   if (String.IsNullOrEmpty(root))
						                   throw new ArgumentException("Failed to identify an existing root folder.");

					                   var rootFolder = await StorageFolder.GetFolderFromPathAsync(root);
					                   while (subFolders.Count > 0)
					                   {
						                   rootFolder = await rootFolder.CreateFolderAsync(subFolders.Pop());
					                   }
				                   }).Wait();
			    return new DirectoryInfo(path);
		    }
		    catch (Exception e)
		    {
			    Logger.Error("Failed to create directory, error message: {0}", e.Message);
			    throw;
		    }
	    }

	    internal static string[] GetDirectories(string path)
        {
            var folders = Task.Run(async () =>
                                             {
                                                 var root = await GetStorageFolder(path);
                                                 return await root.GetFoldersAsync();
                                             }).Result;
            return folders.Select(folder => folder.Name).ToArray();
        }

	    internal static string[] GetFiles(string path, string searchPattern = "*")
	    {
		    var files = Task.Run(async () =>
			                               {
				                               var folder = await GetStorageFolder(path);
				                               var fileQuery =
					                               folder.CreateFileQueryWithOptions(new QueryOptions
						                                                                 {
							                                                                 UserSearchFilter = searchPattern
						                                                                 });
				                               return await fileQuery.GetFilesAsync();
			                               }).Result;

		    return files.Select(file => file.Path).ToArray();
	    }

	    /// <summary>
		/// Get storage folder from specified directory path.
		/// </summary>
		/// <param name="path">Directory path.</param>
		/// <returns>Storage folder (task) for specified directory path.</returns>
		private static async Task<StorageFolder> GetStorageFolder(string path)
		{
			var directoryName = Path.GetDirectoryName(path.TrimEnd(DirectorySeparatorChar) + DirectorySeparatorChar);
			return await StorageFolder.GetFolderFromPathAsync(directoryName);
		}
    }
}