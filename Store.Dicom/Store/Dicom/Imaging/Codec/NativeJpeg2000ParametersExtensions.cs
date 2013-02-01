// ReSharper disable CheckNamespace
namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public static class NativeJpeg2000ParametersExtensions
	{
		 public static NativeJpeg2000Parameters ToNativeJpeg2000Parameters(this DicomCodecParams codecParams)
		 {
			 var jp2Params = codecParams as DicomJpeg2000Params;
			 if (jp2Params == null) return null;

			 return new NativeJpeg2000Parameters
				        {
					        Irreversible = jp2Params.Irreversible,
					        Rate = jp2Params.Rate,
					        RateLevels = jp2Params.RateLevels,
					        AllowMCT = jp2Params.AllowMCT,
					        EncodeSignedPixelValuesAsUnsigned = jp2Params.EncodeSignedPixelValuesAsUnsigned,
					        IsVerbose = jp2Params.IsVerbose,
					        UpdatePhotometricInterpretation = jp2Params.UpdatePhotometricInterpretation
				        };
		 }
	}
}