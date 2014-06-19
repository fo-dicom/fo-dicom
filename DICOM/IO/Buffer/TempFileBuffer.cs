using System;
using System.IO;

namespace Dicom.IO.Buffer {
	public sealed class TempFileBuffer : IByteBuffer {
		private TemporaryFile _file;
		private uint _size;

		public TempFileBuffer(byte[] data) {
			_file = new TemporaryFile();
		    using (var stream = File.OpenWrite(_file.Name))
		    {
                stream.Write(data, 0, data.Length);
		    }
			_size = (uint)data.Length;
		}

		public bool IsMemory {
			get { return false; }
		}

		public uint Size {
			get { return _size; }
		}

		public byte[] Data {
		    get
		    {
		        using (var stream = File.OpenRead(_file.Name))
		        {
		            var count = stream.Length;
		            var buffer = new byte[count];
		            stream.Read(buffer, 0, (int)count);
		            return buffer;
		        }
		    }
		}

		public byte[] GetByteRange(int offset, int count) {
			byte[] buffer = new byte[count];

			using (Stream fs = File.OpenRead(_file.Name)) {
				fs.Seek(offset, SeekOrigin.Begin);
				fs.Read(buffer, 0, count);
			}

			return buffer;
		}
	}
}
