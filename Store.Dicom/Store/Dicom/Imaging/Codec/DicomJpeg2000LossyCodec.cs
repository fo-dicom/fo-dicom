// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public class DicomJpeg2000LossyCodec : DicomJpeg2000Codec
	{
		public override DicomTransferSyntax TransferSyntax
		{
			get { return DicomTransferSyntax.JPEG2000Lossy; }
		}

		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			throw new System.NotImplementedException();
		}

		public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomJpeg2000NativeCodec.Decode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData(), parameters.ToNativeJpeg2000Parameters());
		}
	}
}