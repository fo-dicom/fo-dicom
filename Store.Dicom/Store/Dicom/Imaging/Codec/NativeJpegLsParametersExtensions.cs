// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public static class NativeJpegLsParametersExtensions
	{
		 public static NativeJpegLsParameters ToNativeJpegLsParameters(this DicomCodecParams codecParams)
		 {
			 var jlsParams = codecParams as DicomJpegLsParams;
			 if (jlsParams == null) return null;

			 return new NativeJpegLsParameters
				        {
					        AllowedError = jlsParams.AllowedError,
					        InterleaveMode = (int)jlsParams.InterleaveMode,
					        ColorTransform = (int)jlsParams.ColorTransform
				        };
		 }
	}
}