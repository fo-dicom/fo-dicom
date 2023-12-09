// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec.JpegLossless;

namespace FellowOakDicom.Imaging.Codec
{

    public class JpegLosslessDecoderWrapperProcess14SV1 : DicomJpegLosslessDecoder
    {

        public override DicomTransferSyntax TransferSyntax => DicomTransferSyntax.JPEGProcess14SV1;

    }
}
