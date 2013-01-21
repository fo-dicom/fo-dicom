using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

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
            return GetStorageFile(path) != null;
        }

        internal static void Delete(string path)
        {
        }

        internal static Stream Create(string path)
        {
            return OpenWrite(path);
        }

        internal static void Move(string sourceFileName, string destFileName)
        {
        }

        internal static Stream OpenRead(string path)
        {
            return Task.Run(async () => await Directory.Root.OpenStreamForReadAsync(path)).Result;
        }

        internal static Stream OpenWrite(string path)
        {
            return Task.Run(async () => await Directory.Root.OpenStreamForWriteAsync(path, CreationCollisionOption.ReplaceExisting)).Result;
        }

        internal static byte[] ReadAllBytes(string path)
        {
            byte[] bytes = null;
            var status =
                Task.Run(async () =>
                                   {
                                       var file = GetStorageFile(path);
                                       using (var stream = await file.OpenAsync(FileAccessMode.Read))
                                       using (var reader = new DataReader(stream))
                                       {
                                           var size = stream.Size;
                                           await reader.LoadAsync((uint)size);
                                           bytes = new byte[size];
                                           reader.ReadBytes(bytes);
                                           return 0;
                                       }
                                   }).Result;
            return bytes;
        }

        internal static void WriteAllBytes(string path, byte[] bytes)
        {
            var status =
                Task.Run(async () =>
                                   {
                                       var file = await Directory.Root.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
                                       using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                                       using (var writer = new DataWriter(stream))
                                       {
                                           writer.WriteBytes(bytes);
                                           return await writer.StoreAsync();
                                       }
                                   }).Result;
        }

        internal static FileAttributes GetAttributes(string path)
        {
            return FileAttributes.Normal;
        }

        internal static void SetAttributes(string path, FileAttributes attributes)
        {
        }

        private static StorageFile GetStorageFile(string path)
        {
            try
            {
                var folderName = Path.GetDirectoryName(path);
                var fileName = Path.GetFileName(path);
                var file = Task.Run(async () =>
                {
                    var folder = await Directory.Root.GetFolderAsync(folderName);
                    return await folder.GetFileAsync(fileName);
                }).Result;
                return file;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}