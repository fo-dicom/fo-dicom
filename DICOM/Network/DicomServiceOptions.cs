using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	public class DicomServiceOptions {
		public static DicomServiceOptions Default = new DicomServiceOptions();

		public DicomServiceOptions() {
			LogDataPDUs = false;
		}

		/// <summary>Write message to log for each P-Data-TF PDU sent or received.</summary>
		public bool LogDataPDUs {
			get;
			set;
		}
	}
}
