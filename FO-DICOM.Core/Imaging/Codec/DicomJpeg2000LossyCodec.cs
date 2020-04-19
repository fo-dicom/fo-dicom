// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Imaging.Codec
{
    public class DicomJpeg2000LossyCodec : DicomJpeg2000Codec
    {
        public override DicomTransferSyntax TransferSyntax => DicomTransferSyntax.JPEG2000Lossy;

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpeg2000CodecImpl.Encode(oldPixelData, newPixelData, parameters as DicomJpeg2000Params);
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            DicomJpeg2000CodecImpl.Decode(oldPixelData, newPixelData, parameters as DicomJpeg2000Params);
        }

    }
}
