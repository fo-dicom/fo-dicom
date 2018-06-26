// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpegLossless14Codec : DicomJpegNativeCodec
    {
        public override DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.JPEGProcess14;
            }
        }

        protected override IJpegNativeCodec GetCodec(int bits, DicomJpegParams jparams)
        {
            if (bits <= 8) return new Jpeg8Codec(JpegMode.Lossless, jparams.Predictor, jparams.PointTransform);
            if (bits <= 12) return new Jpeg12Codec(JpegMode.Lossless, jparams.Predictor, jparams.PointTransform);
            if (bits <= 16) return new Jpeg16Codec(JpegMode.Lossless, jparams.Predictor, jparams.PointTransform);
            throw new DicomCodecException("Unable to create JPEG Process 14 codec for bits stored == {0}", bits);
        }
    }
}
