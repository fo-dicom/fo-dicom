// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging.Codec
{
    public static class NativeJpegParametersExtensions
    {
        public static NativeJpegParameters ToNativeJpegParameters(this DicomCodecParams codecParams)
        {
            var jpegParams = codecParams as DicomJpegParams;
            if (jpegParams == null) return null;

            return new NativeJpegParameters
                       {
                           Quality = jpegParams.Quality,
                           SmoothingFactor = jpegParams.SmoothingFactor,
                           ConvertColorspaceToRGB = jpegParams.ConvertColorspaceToRGB,
                           SampleFactor = (int)jpegParams.SampleFactor,
                           Predictor = jpegParams.Predictor,
                           PointTransform = jpegParams.PointTransform
                       };
        }
    }
}
