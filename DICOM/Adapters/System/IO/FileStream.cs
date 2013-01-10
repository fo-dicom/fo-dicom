using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    public class FileStream
    {
        #region FIELDS

        private readonly IRandomAccessStream _stream;
        private readonly DataWriter _writer;

        #endregion

        #region CONSTRUCTORS

        public FileStream(string name, FileMode mode)
        {
            _stream = Task.Run(async () =>
                                         {
                                             var file = await KnownFolders.DocumentsLibrary.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                                             return await file.OpenAsync(FileAccessMode.ReadWrite);
                                         }).Result;
            _writer = new DataWriter(_stream) { UnicodeEncoding = UnicodeEncoding.Utf8, ByteOrder = ByteOrder.LittleEndian };
        }

        #endregion

        #region METHODS

        public void WriteByte(byte value)
        {
            _writer.WriteByte(value);
        }

        public void Close()
        {
            var flushed = Task.Run(async () => await _writer.StoreAsync()).Result;
            _writer.Dispose();
        }

        #endregion
    }
}