using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    internal class File
    {
		#region PROPERTIES

		internal static string[] DicomFileExtensions
		{
			get { return new[] { ".dcm", ".dic" }; }
		}

		#endregion

		#region METHODS

        internal static bool Exists(string path)
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

	    internal static void Delete(string path)
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

	    internal static Stream Create(string path)
	    {
		    return Task.Run(async () =>
			                          {
				                          var file = await FileHelper.CreateStorageFileAsync(path);
				                          return await file.OpenStreamForWriteAsync();
			                          }).Result;
	    }

	    internal static void Move(string sourceFileName, string destFileName)
        {
        }

        internal static Stream OpenRead(string path)
        {
            return Task.Run(async () =>
	                                  {
		                                  var file = await FileHelper.GetStorageFileAsync(path);
		                                  return await file.OpenStreamForReadAsync();
	                                  }).Result;
        }

	    internal static Stream OpenWrite(string path)
	    {
		    return Task.Run(async () =>
			                          {
				                          var file = Exists(path)
					                                     ? await FileHelper.GetStorageFileAsync(path)
					                                     : await FileHelper.CreateStorageFileAsync(path);
				                          return await file.OpenStreamForWriteAsync();
			                          }).Result;
	    }

	    internal static byte[] ReadAllBytes(string path)
	    {
		    byte[] bytes = null;
		    Task.Run(async () =>
			                   {
				                   var buffer = await PathIO.ReadBufferAsync(path);
								   bytes = new byte[buffer.Length];
				                   buffer.CopyTo(bytes);
			                   }).Wait();
		    return bytes;
	    }

	    internal static void WriteAllBytes(string path, byte[] bytes)
	    {
		    Task.Run(async () => await PathIO.WriteBytesAsync(path, bytes)).Wait();
	    }

	    internal static FileAttributes GetAttributes(string path)
        {
            return FileAttributes.Normal;
        }

        internal static void SetAttributes(string path, FileAttributes attributes)
        {
        }

	    #endregion
    }
}