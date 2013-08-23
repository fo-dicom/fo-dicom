// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

namespace Dicom.Imaging.Codec
{
	public class DicomJpegLossless14SV1Codec : DicomJpegNativeCodec
	{
		public override DicomTransferSyntax TransferSyntax
		{
			get { return DicomTransferSyntax.JPEGProcess14SV1; }
		}

		protected override IJpegNativeCodec GetCodec(int bits, DicomJpegParams jparams)
		{
			if (bits <= 8)
			return new Jpeg8Codec(JpegMode.Lossless, 1, jparams.PointTransform);
			if (bits <= 12)
				return new Jpeg12Codec(JpegMode.Lossless, 1, jparams.PointTransform);
			if (bits <= 16)
				return new Jpeg16Codec(JpegMode.Lossless, 1, jparams.PointTransform);
			throw new DicomCodecException("Unable to create JPEG Process 14 [SV1] codec for bits stored == {0}", bits);
		}
	}
}