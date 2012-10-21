using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public partial class DicomUID {
		private static void LoadPrivateUIDs() {
			_uids.Add(DicomUID.GEPrivateImplicitVRBigEndian.UID, DicomUID.GEPrivateImplicitVRBigEndian);
		}

		/// <summary>GE Private Implicit VR Big Endian</summary>
		/// <remarks>Same as Implicit VR Little Endian except for big endian pixel data.</remarks>
		public readonly static DicomUID GEPrivateImplicitVRBigEndian = new DicomUID("1.2.840.113619.5.2", "GE Private Implicit VR Big Endian", DicomUidType.TransferSyntax);
	}
}
