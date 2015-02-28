// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace Dicom.Imaging.Codec
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