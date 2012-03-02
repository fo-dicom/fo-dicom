using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.Imaging.Codec {
	public interface IDicomCodec {
		string Name { get; }
		DicomTransferSyntax TransferSyntax { get; }

		DicomCodecParams GetDefaultParameters();

		void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
		void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
	}
}
