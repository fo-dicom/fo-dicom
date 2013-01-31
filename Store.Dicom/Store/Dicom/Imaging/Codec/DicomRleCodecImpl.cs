// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public class DicomRleCodecImpl : DicomRleCodec {

		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters) {
			DicomRleNativeCodec.Encode(oldPixelData.ToCodecPixelData(), newPixelData.ToCodecPixelData());
		}

		public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomRleNativeCodec.Decode(oldPixelData.ToCodecPixelData(), newPixelData.ToCodecPixelData());
		}
	}
}
