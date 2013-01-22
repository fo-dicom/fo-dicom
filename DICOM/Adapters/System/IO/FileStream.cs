using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    internal sealed class FileStream : Stream
    {
        #region FIELDS

        private readonly IRandomAccessStream _stream;
	    private bool _disposed;

        #endregion

        #region CONSTRUCTORS

        internal FileStream(string name, FileMode mode)
        {
			try
			{
				_stream = Task.Run(async () =>
					                         {
						                         StorageFile file;
						                         ulong position;
						                         switch (mode)
						                         {
							                         case FileMode.Create:
							                         case FileMode.Truncate:
								                         file = await FileHelper.CreateStorageFileAsync(name);
								                         position = 0;
								                         break;
							                         case FileMode.CreateNew:
								                         if (File.Exists(name))
									                         throw new IOException("File mode is CreateNew, but file already exists.");
								                         file = await FileHelper.CreateStorageFileAsync(name);
								                         position = 0;
								                         break;
							                         case FileMode.OpenOrCreate:
								                         if (File.Exists(name))
								                         {
									                         file = await FileHelper.GetStorageFileAsync(name);
								                         }
								                         else
								                         {
									                         file = await FileHelper.CreateStorageFileAsync(name);
								                         }
								                         position = 0;
								                         break;
							                         case FileMode.Open:
								                         if (!File.Exists(name))
									                         throw new FileNotFoundException("File mode is Open, but file does not exist.");
								                         file = await FileHelper.GetStorageFileAsync(name);
								                         position = 0;
								                         break;
							                         case FileMode.Append:
								                         if (File.Exists(name))
								                         {
									                         file = await FileHelper.GetStorageFileAsync(name);
									                         position = (await file.GetBasicPropertiesAsync()).Size;
								                         }
								                         else
								                         {
									                         file = await FileHelper.CreateStorageFileAsync(name);
									                         position = 0;
								                         }
								                         break;
							                         default:
								                         throw new ArgumentOutOfRangeException("mode");
						                         }
												 var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
												 stream.Seek(position);
						                         return stream;
					                         }).Result;
				_disposed = false;
				Name = name;
			}
			catch
			{
				_stream = null;
				_disposed = true;
				Name = String.Empty;
			}
		}

        #endregion

		#region PROPERTIES

	    public override bool CanRead
	    {
		    get { return true; }
	    }

	    public override bool CanSeek
	    {
		    get { return true; }
	    }

	    public override bool CanWrite
	    {
		    get { return true; }
	    }

	    public override long Length
	    {
			get
			{
				if (_disposed) throw new ObjectDisposedException("_stream");
				return (long)_stream.Size;
			}
	    }

	    public override long Position
	    {
			get
			{
				if (_disposed) throw new ObjectDisposedException("_stream");
				return (long)_stream.Position;
			}
			set
			{
				if (_disposed) throw new ObjectDisposedException("_stream");
				_stream.Seek((ulong)value);
			}
	    }

	    internal string Name { get; private set; }

		#endregion

		#region METHODS

	    public override void Flush()
	    {
			if (_disposed) throw new ObjectDisposedException("_stream");
			Task.Run(async () => await _stream.FlushAsync()).Wait();
	    }

	    public override int Read(byte[] buffer, int offset, int count)
	    {
			if (_disposed) throw new ObjectDisposedException("_stream");
			return Task.Run(async () =>
			                          {
										  using (var reader = new DataReader(_stream))
										  {
											  await reader.LoadAsync((uint)count);
											  var length = Math.Min(count, (int)reader.UnconsumedBufferLength);
											  var temp = new byte[length];
											  reader.ReadBytes(temp);
											  Array.Copy(temp, 0, buffer, offset, length);
											  reader.DetachStream();
											  return length;
										  }
			                          }).Result;
	    }

	    public override long Seek(long offset, SeekOrigin origin)
	    {
			if (_disposed) throw new ObjectDisposedException("_stream");
			switch (origin)
		    {
			    case SeekOrigin.Begin:
				    _stream.Seek((ulong)offset);
				    break;
			    case SeekOrigin.Current:
				    _stream.Seek(_stream.Position + (ulong)offset);
				    break;
			    case SeekOrigin.End:
				    _stream.Seek(_stream.Size - (ulong)offset);
				    break;
			    default:
				    throw new ArgumentOutOfRangeException("offset");
		    }
		    return (long)_stream.Position;
	    }

	    public override void SetLength(long value)
	    {
			if (_disposed) throw new ObjectDisposedException("_stream");
			_stream.Size = (ulong)value;
	    }

	    public override void Write(byte[] buffer, int offset, int count)
	    {
			if (_disposed) throw new ObjectDisposedException("_stream");
		    Task.Run(async () =>
			                   {
				                   using (var writer = new DataWriter(_stream))
				                   {
					                   var temp = new byte[count];
									   Array.Copy(buffer, offset, temp, 0, count);
					                   writer.WriteBytes(temp);
					                   await writer.StoreAsync();
					                   writer.DetachStream();
				                   }
			                   }).Wait();
	    }

		protected override void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing)
			{
				Flush();
				_stream.Dispose();
			}
			_disposed = true;

			base.Dispose(disposing);
		}

	    #endregion
    }
}