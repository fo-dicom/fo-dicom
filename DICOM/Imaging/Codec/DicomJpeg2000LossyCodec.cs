// Copyright (c) 2010-2015 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace Dicom.Imaging.Codec
{
	public class DicomJpeg2000LossyCodec : DicomJpeg2000Codec
	{
		public override DicomTransferSyntax TransferSyntax
		{
			get { return DicomTransferSyntax.JPEG2000Lossy; }
		}

		public override void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomJpeg2000NativeCodec.Encode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData(), parameters.ToNativeJpeg2000Parameters());
		}

		public override void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters)
		{
			DicomJpeg2000NativeCodec.Decode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData(), parameters.ToNativeJpeg2000Parameters());
		}
	}
}