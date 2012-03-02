using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO.Buffer;

namespace Dicom.IO {
	public delegate void ByteSourceCallback(IByteSource source, object state);

	public interface IByteSource {
		Endian Endian {
			get;
			set;
		}

		long Position {
			get;
		}

		long Marker {
			get;
		}

		bool IsEOF {
			get;
		}

		bool CanRewind {
			get;
		}

		byte GetUInt8();

		short GetInt16();
		ushort GetUInt16();

		int GetInt32();
		uint GetUInt32();

		long GetInt64();
		ulong GetUInt64();

		float GetSingle();
		double GetDouble();

		byte[] GetBytes(int count);

		IByteBuffer GetBuffer(uint count);

		void Skip(int count);

		void Mark();
		void Rewind();

		void PushMilestone(uint count);
		void PopMilestone();
		bool HasReachedMilestone();

		bool Require(uint count);
		bool Require(uint count, ByteSourceCallback callback, object state);
	}
}
