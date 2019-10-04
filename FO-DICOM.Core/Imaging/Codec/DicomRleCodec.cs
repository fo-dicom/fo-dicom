// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public abstract class DicomRleCodec : IDicomCodec
    {
        public string Name
        {
            get
            {
                return DicomTransferSyntax.RLELossless.UID.Name;
            }
        }

        public DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.RLELossless;
            }
        }

        public DicomCodecParams GetDefaultParameters()
        {
            return null;
        }

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
