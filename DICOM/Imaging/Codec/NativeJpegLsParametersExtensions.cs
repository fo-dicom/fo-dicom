// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public static class NativeJpegLSParametersExtensions
    {
        public static NativeJpegLSParameters ToNativeJpegLSParameters(this DicomCodecParams codecParams)
        {
            var jlsParams = codecParams as DicomJpegLsParams;
            if (jlsParams == null) return null;

            return new NativeJpegLSParameters
                       {
                           AllowedError = jlsParams.AllowedError,
                           InterleaveMode = (int)jlsParams.InterleaveMode,
                           ColorTransform = (int)jlsParams.ColorTransform
                       };
        }
    }
}
