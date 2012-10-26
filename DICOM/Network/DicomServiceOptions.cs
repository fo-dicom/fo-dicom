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
			LogDimseDatasets = false;
			MaxCommandBuffer = 1 * 1024;		//1KB
			MaxDataBuffer = 1 * 1024 * 1024;	//1MB
			ThreadPoolLinger = 200;
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

		/// <summary>Maximum buffer length for command PDVs when generating P-Data-TF PDUs.</summary>
		public uint MaxCommandBuffer {
			get;
			set;
		}

		/// <summary>Maximum buffer length for data PDVs when generating P-Data-TF PDUs.</summary>
		public uint MaxDataBuffer {
			get;
			set;
		}

		/// <summary>Amount of time in milliseconds to retain Thread Pool thread to process additional requests.</summary>
		public int ThreadPoolLinger {
			get;
			set;
		}
	}
}
