// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.Codec
{

    public interface IDicomCodec
    {
        string Name { get; }

        DicomTransferSyntax TransferSyntax { get; }

        DicomCodecParams GetDefaultParameters();

        void Encode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);

        void Decode(DicomPixelData oldPixelData, DicomPixelData newPixelData, DicomCodecParams parameters);
    }
}
