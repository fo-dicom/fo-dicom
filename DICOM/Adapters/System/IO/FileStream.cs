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
            // TODO Handle alternative create/open/read/write scenarios
            _stream = Task.Run(async () =>
                                         {
                                             var file = await Directory.Root.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
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
            Task.Run(async () =>
                               {
                                   var status = await _writer.StoreAsync();
                                   _writer.Dispose();
                                   return status;
                               });
        }

        #endregion
    }
}