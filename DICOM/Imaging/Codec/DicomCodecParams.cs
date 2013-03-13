using System;

using Dicom.Log;

namespace Dicom.Imaging.Codec {
	public class DicomCodecParams {
		protected DicomCodecParams() {
			Logger = LogManager.Default.GetLogger("Dicom.Imaging.Codec");
		}

		public Logger Logger {
			get;
			protected set;
		}
	}
}
