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
                                             var file = await KnownFolders.DocumentsLibrary.CreateFileAsync(name);
                                             return await file.OpenAsync(FileAccessMode.ReadWrite);
                                         }).Result;
            _writer = new DataWriter(_stream);
        }

        #endregion

        #region METHODS

        public void WriteByte(byte value)
        {
            _writer.WriteByte(value);
        }

        public void Close()
        {
            _writer.Dispose();
            _stream.Dispose();
        }

        #endregion
    }
}