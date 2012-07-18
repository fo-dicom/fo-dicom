using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO.Buffer;

namespace Dicom.IO {
	public delegate void ByteTargetCallback(IByteTarget target, object state);

	public interface IByteTarget {
		Endian Endian {
			get;
			set;
		}

		long Position {
			get;
		}

		void Write(byte v);
		void Write(short v);
		void Write(ushort v);
		void Write(int v);
		void Write(uint v);
		void Write(long v);
		void Write(ulong v);
		void Write(float v);
		void Write(double v);

		void Write(byte[] buffer, uint offset=0, uint count=0xffffffff, ByteTargetCallback callback=null, object state=null);

		void Close();
	}
}
