// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace System.IO
{
    public static class Directory
    {
		#region FIELDS

	    private const char DirectorySeparatorChar = '\\';

	    #endregion

        public static bool Exists(string path)
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

	    public static DirectoryInfo CreateDirectory(string path)
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
			    throw e.InnerException ?? e;
		    }
	    }

	    public static string[] GetDirectories(string path)
        {
            var folders = Task.Run(async () =>
                                             {
                                                 var root = await GetStorageFolder(path);
                                                 return await root.GetFoldersAsync();
                                             }).Result;
            return folders.Select(folder => folder.Name).ToArray();
        }

	    public static string[] GetFiles(string path, string searchPattern = "*")
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