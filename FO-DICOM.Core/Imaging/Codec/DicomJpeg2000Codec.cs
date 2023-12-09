// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.Codec
{

    public class DicomJpeg2000Params : DicomCodecParams
    {
        public DicomJpeg2000Params()
        {
            Irreversible = true;
            Rate = 20;
            IsVerbose = false;
            AllowMCT = true;
            UpdatePhotometricInterpretation = true;
            EncodeSignedPixelValuesAsUnsigned = false;

            RateLevels = new int[] { 1280, 640, 320, 160, 80, 40, 20, 10, 5 };
        }

        public bool Irreversible { get; set; }

        public int Rate { get; set; }

        public int[] RateLevels { get; set; }

        public bool IsVerbose { get; set; }

        public bool AllowMCT { get; set; }

        public bool UpdatePhotometricInterpretation { get; set; }

        public bool EncodeSignedPixelValuesAsUnsigned { get; set; }
    }

    public abstract class DicomJpeg2000Codec : IDicomCodec
    {
        public string Name => TransferSyntax.UID.Name;

        public abstract DicomTransferSyntax TransferSyntax { get; }

        public DicomCodecParams GetDefaultParameters() => new DicomJpeg2000Params();

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
