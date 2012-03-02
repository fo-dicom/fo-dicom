using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.IO.Buffer {
	public interface IByteBuffer {
		bool IsMemory {
			get;
		}

		uint Size {
			get;
		}

		byte[] Data {
			get;
		}

		byte[] GetByteRange(int offset, int count);
	}
}
