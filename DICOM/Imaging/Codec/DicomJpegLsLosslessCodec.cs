// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public class DicomJpegLsLosslessCodec : DicomJpegLsCodec
    {
        public override DicomTransferSyntax TransferSyntax
        {
            get
            {
                return DicomTransferSyntax.JPEGLSLossless;
            }
        }

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
#if NETFX_CORE && !HOLOLENS
            DicomJpegLsNativeCodec.Encode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpegLSParameters());
#else
            DicomJpegLsCodecImpl.Encode(oldPixelData, newPixelData, parameters as DicomJpegLsParams);
#endif
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
#if NETFX_CORE && !HOLOLENS
            DicomJpegLsNativeCodec.Decode(
                oldPixelData.ToNativePixelData(),
                newPixelData.ToNativePixelData(),
                parameters.ToNativeJpegLSParameters());
#else
            DicomJpegLsCodecImpl.Decode(oldPixelData, newPixelData, parameters as DicomJpegLsParams);
#endif
        }
    }
}
