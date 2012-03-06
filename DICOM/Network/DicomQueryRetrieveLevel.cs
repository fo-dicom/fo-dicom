using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public enum DicomQueryRetrieveLevel {
		Patient,
		Study,
		Series,
		Image,
		Worklist
	}
}
