// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public static class NativeJpegParametersExtensions
	{
		 public static NativeJpegParameters ToNativeJpegParameters(this DicomCodecParams codecParams)
		 {
			 var jpegParams = codecParams as DicomJpegParams;
			 if (jpegParams == null) return null;

			 return new NativeJpegParameters
				        {
							Quality = jpegParams.Quality,
							SmoothingFactor = jpegParams.SmoothingFactor,
							ConvertColorspaceToRGB = jpegParams.ConvertColorspaceToRGB,
							SampleFactor = (int)jpegParams.SampleFactor,
							Predictor = jpegParams.Predictor,
							PointTransform = jpegParams.PointTransform
				        };
		 }
	}
}