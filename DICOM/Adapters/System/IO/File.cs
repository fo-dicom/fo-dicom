using System.Threading.Tasks;

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

	    internal static FileStream Create(string path)
	    {
			return new FileStream(path, FileMode.Create);
	    }

	    internal static void Move(string sourceFileName, string destFileName)
        {
        }

	    internal static FileStream OpenRead(string path)
	    {
		    return new FileStream(path, FileMode.Open);
	    }

	    internal static FileStream OpenWrite(string path)
	    {
			return new FileStream(path, FileMode.OpenOrCreate);
	    }

	    internal static byte[] ReadAllBytes(string path)
	    {
			using (var stream = new FileStream(path, FileMode.Open))
			{
		    var bytes = new byte[stream.Length];
				stream.Read(bytes, 0, (int)stream.Length);
				return bytes;
			}
	    }

	    internal static void WriteAllBytes(string path, byte[] bytes)
	    {
			using (var stream = new FileStream(path, FileMode.Create))
			{
				stream.Write(bytes, 0, bytes.Length);
			}
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