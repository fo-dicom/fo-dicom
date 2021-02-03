// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.Codec.JpegLossless;

namespace FellowOakDicom.Imaging.Codec
{

    public class JpegLosslessDecoderWrapperProcess14 : DicomJpegLosslessDecoder
    {

        public override DicomTransferSyntax TransferSyntax => DicomTransferSyntax.JPEGProcess14;

    }

}
