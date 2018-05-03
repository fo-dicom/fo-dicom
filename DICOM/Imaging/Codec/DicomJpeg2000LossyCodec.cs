// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpeg2000LossyCodec : DicomJpeg2000Codec
    {
        public override DicomTransferSyntax TransferSyntax => DicomTransferSyntax.JPEG2000Lossy;

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
#if NETFX_CORE && !HOLOLENS
            DicomJpeg2000NativeCodec.Encode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpeg2000Parameters());
#else
            DicomJpeg2000CodecImpl.Encode(oldPixelData, newPixelData, parameters as DicomJpeg2000Params);
#endif
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
#if NETFX_CORE && !HOLOLENS
            DicomJpeg2000NativeCodec.Decode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpeg2000Parameters());
#else
            DicomJpeg2000CodecImpl.Decode(oldPixelData, newPixelData, parameters as DicomJpeg2000Params);
#endif
        }
    }
}
