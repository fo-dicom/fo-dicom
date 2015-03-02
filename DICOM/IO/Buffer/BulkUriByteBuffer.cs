using System;
using System.Net;

namespace Dicom.IO.Buffer
{
	public sealed class BulkUriByteBuffer : IByteBuffer
	{
		public BulkUriByteBuffer(string bulkDataUri)
		{
			this.BulkDataUri = bulkDataUri;
		}

		public bool IsMemory
		{
			get
			{
				return false;
			}
		}

		public string BulkDataUri { get; private set; }

		public byte[] Data
		{
			get
			{
				using (var response = WebRequest.Create(BulkDataUri).GetResponse())
				{
					int count = (int)response.ContentLength;
					var buffer = new Byte[count];
					response.GetResponseStream().Read(buffer, 0, count);
					return buffer;
				}
			}
		}

		public byte[] GetByteRange(int offset, int count)
		{
			using (var response = WebRequest.Create(BulkDataUri).GetResponse())
			{
				var buffer = new Byte[count];
				response.GetResponseStream().Read(buffer, offset, count);
				return buffer;
			}
		}

		public uint Size
		{
			get
			{
				using (var response = WebRequest.Create(BulkDataUri).GetResponse())
				{
					return (uint)response.ContentLength;
				}
			}
		}
	}
}
