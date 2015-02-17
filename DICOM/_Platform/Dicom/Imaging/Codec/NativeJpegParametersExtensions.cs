// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace Dicom.Imaging.Codec
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