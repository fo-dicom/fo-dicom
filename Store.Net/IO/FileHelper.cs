// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Threading.Tasks;
using Windows.Storage;

namespace System.IO
{
	public static class FileHelper
	{
		#region METHODS

		public static async Task<StorageFile> CreateStorageFileAsync(string path)
		{
			var folderName = Path.GetDirectoryName(path);
			if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
			var folder = await StorageFolder.GetFolderFromPathAsync(folderName);

			var fileName = Path.GetFileName(path);
			return await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
		}

		public static async Task<StorageFile> GetStorageFileAsync(string path)
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