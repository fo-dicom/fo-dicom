using System;
using System.Net;

namespace Dicom.IO.Buffer
{
	public sealed class BulkUriByteBuffer : IByteBuffer
	{
		private byte[] _buffer;
		private uint? _size;

		public BulkUriByteBuffer(string bulkDataUri)
		{
			this.BulkDataUri = bulkDataUri;
		}

		public bool IsMemory
		{
			get
			{
				return _buffer != null;
			}
		}

		public string BulkDataUri { get; private set; }

		public byte[] Data
		{
			get
			{
				if (_buffer == null)
					throw new InvalidOperationException(
						"BulkUriByteBuffer cannot provide Data until either GetData() has been called.");
				return _buffer;
			}
		}

		public byte[] GetData()
		{
			if (_buffer != null) return _buffer;

			using (var response = WebRequest.Create(BulkDataUri).GetResponse())
			{
				int count = (int)response.ContentLength;
				var buffer = new Byte[count];
				response.GetResponseStream().Read(buffer, 0, count);
				_buffer = buffer;
				_size = (uint)_buffer.LongLength;
			}

			return _buffer;
		}

		public byte[] GetByteRange(int offset, int count)
		{
			if (_buffer == null)
			{
				GetData();
			}

			var range = new byte[count];
			Array.Copy(Data, offset, range, 0, count);
			return range;
		}

		public uint Size
		{
			get
			{
				if (!_size.HasValue)
					throw new InvalidOperationException(
						"BulkUriByteBuffer cannot provide Size until either GetData() or GetSize() has been called.");

				return _size.Value;
			}
		}

		public uint GetSize()
		{
			if (_size.HasValue) return _size.Value;
			try
			{
				var request = WebRequest.Create(BulkDataUri);
				request.Method = "HEAD";
				using (var response = request.GetResponse())
				{
					if (response.ContentLength != -1) return (uint)response.ContentLength;
				}
			}
			catch (WebException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			GetData();
			return Size;
		}
	}
}
