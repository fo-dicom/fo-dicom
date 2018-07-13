// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomRleCodecImpl : DicomRleCodec
    {
        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomRleNativeCodec.Encode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData());
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomRleNativeCodec.Decode(oldPixelData.ToNativePixelData(), newPixelData.ToNativePixelData());
        }
    }
}
