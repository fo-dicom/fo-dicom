using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    internal class FileStream : MemoryStream
    {
        #region FIELDS

        private readonly IRandomAccessStream _stream;
        private readonly DataWriter _writer;

        #endregion

        #region CONSTRUCTORS

        internal FileStream(string name, FileMode mode)
        {
	        Name = name;

            // TODO Handle alternative create/open/read/write scenarios
            _stream = Task.Run(async () =>
                                         {
                                             var file = await Directory.Root.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                                             return await file.OpenAsync(FileAccessMode.ReadWrite);
                                         }).Result;
            _writer = new DataWriter(_stream);
        }

        #endregion

		#region PROPERTIES

		internal string Name { get; private set; }

		#endregion

		#region METHODS

		internal new void WriteByte(byte value)
        {
            _writer.WriteByte(value);
        }

        internal void Close()
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