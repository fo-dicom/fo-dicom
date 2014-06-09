// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Threading.Tasks;

namespace System.IO
{
    public class File
    {
		#region METHODS

        public static bool Exists(string path)
        {
			try
			{
				return Task.Run(async () => await FileHelper.GetStorageFileAsync(path)).Result != null;
			}
			catch
			{
				return false;
			}
        }

	    public static void Delete(string path)
	    {
		    try
		    {
			    Task.Run(async () =>
				                   {
					                   var file = await FileHelper.GetStorageFileAsync(path);
					                   await file.DeleteAsync();
				                   }).Wait();
		    }
		    catch
		    {
		    }
	    }

	    public static FileStream Create(string path)
	    {
			return new FileStream(path, FileMode.Create);
	    }

	    public static void Move(string sourceFileName, string destFileName)
	    {
			try
			{
				Task.Run(async () =>
						   {
							   var src = await FileHelper.GetStorageFileAsync(sourceFileName);
							   var dest = await FileHelper.CreateStorageFileAsync(destFileName);
							   await src.MoveAndReplaceAsync(dest);
						   }).Wait();

			}
			catch
			{
			}
		}

        public static FileStream Open(string path, FileMode mode, FileAccess access)
        {
            return new FileStream(path, mode, access);
        }

        public static FileStream OpenRead(string path)
	    {
		    return new FileStream(path, FileMode.Open);
	    }

	    public static FileStream OpenWrite(string path)
	    {
			return new FileStream(path, FileMode.OpenOrCreate);
	    }

	    public static byte[] ReadAllBytes(string path)
	    {
			using (var stream = new FileStream(path, FileMode.Open))
			{
		    var bytes = new byte[stream.Length];
				stream.Read(bytes, 0, (int)stream.Length);
				return bytes;
			}
	    }

	    public static void WriteAllBytes(string path, byte[] bytes)
	    {
			using (var stream = new FileStream(path, FileMode.Create))
			{
				stream.Write(bytes, 0, bytes.Length);
			}
		}

	    public static FileAttributes GetAttributes(string path)
        {
            return FileAttributes.Normal;
        }

        public static void SetAttributes(string path, FileAttributes attributes)
        {
        }

	    #endregion
    }
}