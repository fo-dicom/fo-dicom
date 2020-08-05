// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpegProcess1Codec : DicomJpegNativeCodec
    {
        public override DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.JPEGProcess1;
            }
        }

        protected override IJpegNativeCodec GetCodec(int bits, DicomJpegParams jparams)
        {
            if (bits == 8) return new Jpeg8Codec(JpegMode.Baseline, 0, 0);
            throw new DicomCodecException("Unable to create JPEG Process 1 codec for bits stored == {0}", bits);
        }
    }
}
