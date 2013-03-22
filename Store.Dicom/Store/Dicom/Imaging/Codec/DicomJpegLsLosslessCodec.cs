// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace Dicom.Imaging.Codec
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