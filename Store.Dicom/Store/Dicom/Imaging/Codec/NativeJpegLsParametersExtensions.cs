// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public static class NativeJpegLSParametersExtensions
	{
		 public static NativeJpegLSParameters ToNativeJpegLSParameters(this DicomCodecParams codecParams)
		 {
			 var jlsParams = codecParams as DicomJpegLsParams;
			 if (jlsParams == null) return null;

			 return new NativeJpegLSParameters
				        {
					        AllowedError = jlsParams.AllowedError,
					        InterleaveMode = (int)jlsParams.InterleaveMode,
					        ColorTransform = (int)jlsParams.ColorTransform
				        };
		 }
	}
}