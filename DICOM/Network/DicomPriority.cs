using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public enum DicomPriority : ushort {
		Low = 0x0002,
		Medium = 0x0000,
		High = 0x0001
	}
}
