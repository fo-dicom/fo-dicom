using System;

using NLog;

namespace Dicom.Imaging.Codec {
	public class DicomCodecParams {
		protected DicomCodecParams() {
			Logger = LogManager.GetLogger("Dicom.Imaging.Codec");
		}

		public Logger Logger {
			get;
			protected set;
		}
	}
}
