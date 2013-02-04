// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public class DicomJpegLsLosslessCodec : DicomJpegLsCodec
	{
		public override DicomTransferSyntax TransferSyntax
		{
			get { return DicomTransferSyntax.JPEGLSLossless; }
		}

		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomJpegLsNativeCodec.Encode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData(), parameters.ToNativeJpegLSParameters());
		}

		public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomJpegLsNativeCodec.Decode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData(), parameters.ToNativeJpegLSParameters());
		}
	}
}