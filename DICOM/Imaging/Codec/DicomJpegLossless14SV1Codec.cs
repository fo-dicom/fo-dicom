// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpegLossless14SV1Codec : DicomJpegNativeCodec
    {
        public override DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.JPEGProcess14SV1;
            }
        }

        protected override IJpegNativeCodec GetCodec(int bits, DicomJpegParams jparams)
        {
            if (bits <= 8) return new Jpeg8Codec(JpegMode.Lossless, 1, jparams.PointTransform);
            if (bits <= 12) return new Jpeg12Codec(JpegMode.Lossless, 1, jparams.PointTransform);
            if (bits <= 16) return new Jpeg16Codec(JpegMode.Lossless, 1, jparams.PointTransform);
            throw new DicomCodecException("Unable to create JPEG Process 14 [SV1] codec for bits stored == {0}", bits);
        }
    }
}
