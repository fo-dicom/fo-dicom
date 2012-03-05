using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Codec {
	public abstract class DicomRleCodec : IDicomCodec {
		public string Name {
			get { return DicomTransferSyntax.RLELossless.UID.Name; }
		}

		public DicomTransferSyntax TransferSyntax {
			get { return DicomTransferSyntax.RLELossless; }
		}

		public DicomCodecParams GetDefaultParameters() {
			return null;
		}

		public abstract void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
		public abstract void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
	}
}
