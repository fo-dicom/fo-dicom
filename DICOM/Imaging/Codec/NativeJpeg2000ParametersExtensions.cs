// Copyright (c) 2010-2015 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace Dicom.Imaging.Codec
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