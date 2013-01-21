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
	    private bool _disposed;

        #endregion

        #region CONSTRUCTORS

        internal FileStream(string name, FileMode mode)
        {
			if (mode != FileMode.Create) throw new NotSupportedException("Only supported file mode is Create");

			try
			{
				Name = name;
				_stream = Task.Run(async () =>
					                         {
						                         var file = await FileHelper.CreateStorageFileAsync(name);
												 return await file.OpenAsync(FileAccessMode.ReadWrite);
											 }).Result;
				_writer = new DataWriter(_stream);
				_disposed = false;

			}
			catch
			{
				Name = String.Empty;
				_stream = null;
				_writer = null;
				_disposed = true;
			}
		}

        #endregion

		#region PROPERTIES

		internal string Name { get; private set; }

		#endregion

		#region METHODS

		internal new void WriteByte(byte value)
        {
			if (_disposed) throw new ObjectDisposedException("File stream is disposed or could not be initialized.");
            _writer.WriteByte(value);
        }

        internal void Close()
        {
			if (_disposed) throw new ObjectDisposedException("File stream is disposed or could not be initialized.");
			Task.Run(async () =>
                               {
                                   await _writer.StoreAsync();
                                   _writer.Dispose();
                               }).Wait();
        }

		protected override void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing)
			{
				_writer.Dispose();
				_stream.Dispose();
			}
			_disposed = true;

			base.Dispose(disposing);
		}

        #endregion
    }
}