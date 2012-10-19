using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Network {
	/// <summary>
	/// Options to control the behavior of the <see cref="DicomService"/> base class.
	/// </summary>
	public class DicomServiceOptions {
		/// <summary>Default options for use with the <see cref="DicomService"/> base class.</summary>
		public readonly static DicomServiceOptions Default = new DicomServiceOptions();
		
		/// <summary>Constructor</summary>
		public DicomServiceOptions() {
			LogDataPDUs = false;
		}

		/// <summary>Write message to log for each P-Data-TF PDU sent or received.</summary>
		public bool LogDataPDUs {
			get;
			set;
		}

		/// <summary>Write command and data datasets to log.</summary>
		public bool LogDimseDatasets {
			get;
			set;
		}
	}
}
