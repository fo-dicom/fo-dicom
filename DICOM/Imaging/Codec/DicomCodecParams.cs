using System;
using Dicom.Log;

namespace Dicom.Imaging.Codec {
	public class DicomCodecParams {
		protected DicomCodecParams() {
			Logger = VoidLogger.Instance;
		}

		public DicomLogger Logger {
			get;
			protected set;
		}
	}
}
