// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpegProcess4Codec : DicomJpegCodec
    {
        public override DicomTransferSyntax TransferSyntax => DicomTransferSyntax.JPEGProcess2_4;

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpegCodecImpl.Encode(oldPixelData, newPixelData, parameters as DicomJpegParams);
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpegCodecImpl.Decode(oldPixelData, newPixelData, parameters as DicomJpegParams);
        }
    }
}
