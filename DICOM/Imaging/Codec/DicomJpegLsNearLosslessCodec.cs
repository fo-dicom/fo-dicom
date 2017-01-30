// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpegLsNearLosslessCodec : DicomJpegLsCodec
    {
        public override DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.JPEGLSNearLossless;
            }
        }

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpegLsNativeCodec.Encode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpegLSParameters());
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpegLsNativeCodec.Decode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpegLSParameters());
        }
    }
}
