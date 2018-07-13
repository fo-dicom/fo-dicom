// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
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
