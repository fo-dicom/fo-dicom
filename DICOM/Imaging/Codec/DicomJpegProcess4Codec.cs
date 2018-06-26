// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpegProcess4Codec : DicomJpegNativeCodec
    {
        public override DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.JPEGProcess2_4;
            }
        }

        protected override IJpegNativeCodec GetCodec(int bits, DicomJpegParams jparams)
        {
            if (bits == 8) return new Jpeg8Codec(JpegMode.Sequential, 0, 0);
            if (bits <= 12) return new Jpeg12Codec(JpegMode.Sequential, 0, 0);
            throw new DicomCodecException("Unable to create JPEG Process 4 codec for bits stored == {0}", bits);
        }
    }
}
