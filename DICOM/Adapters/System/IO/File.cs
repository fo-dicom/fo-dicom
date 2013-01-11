using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    public class File
    {
        #region METHODS

        public static bool Exists(string path)
        {
            return GetStorageFile(path) != null;
        }

        public static void Delete(string path)
        {
        }

        public static Stream Create(string path)
        {
            return OpenWrite(path);
        }

        public static void Move(string sourceFileName, string destFileName)
        {
        }

        public static Stream OpenRead(string path)
        {
            return Task.Run(async () => await Directory.RootFolder.OpenStreamForReadAsync(path)).Result;
        }

        public static Stream OpenWrite(string path)
        {
            return Task.Run(async () => await Directory.RootFolder.OpenStreamForWriteAsync(path, CreationCollisionOption.ReplaceExisting)).Result;
        }

        public static byte[] ReadAllBytes(string path)
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

        public static void WriteAllBytes(string path, byte[] bytes)
        {
            var status =
                Task.Run(async () =>
                                   {
                                       var file = await Directory.RootFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
                                       using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                                       using (var writer = new DataWriter(stream))
                                       {
                                           writer.WriteBytes(bytes);
                                           return await writer.StoreAsync();
                                       }
                                   }).Result;
        }

        public static FileAttributes GetAttributes(string path)
        {
            return FileAttributes.Normal;
        }

        public static void SetAttributes(string path, FileAttributes attributes)
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
                    var folder = await Directory.RootFolder.GetFolderAsync(folderName);
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