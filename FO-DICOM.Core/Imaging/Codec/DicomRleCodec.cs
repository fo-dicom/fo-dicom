// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Imaging.Codec
{

    public abstract class DicomRleCodec : IDicomCodec
    {

        public string Name => DicomTransferSyntax.RLELossless.UID.Name;

        public DicomTransferSyntax TransferSyntax => DicomTransferSyntax.RLELossless;

        public DicomCodecParams GetDefaultParameters() => null;

        public abstract void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters);

        public abstract void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters);
    }
}
