// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public class DicomJpeg2000LosslessCodec : DicomJpeg2000Codec
	{
		public override DicomTransferSyntax TransferSyntax
		{
			get { return DicomTransferSyntax.JPEG2000Lossless; }
		}

		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomJpeg2000NativeCodec.Encode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData(), parameters.ToNativeJpeg2000Parameters());
		}

		public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomJpeg2000NativeCodec.Decode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData(), parameters.ToNativeJpeg2000Parameters());
		}
	}
}