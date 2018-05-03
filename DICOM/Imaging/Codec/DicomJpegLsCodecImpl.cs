// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using CharLS;
using Dicom.IO.Buffer;

namespace Dicom.Imaging.Codec
{
    internal static class DicomJpegLsCodecImpl
    {
        internal static void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpegLsParams parameters)
        {
            if (oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrFull422 ||
                oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial422 ||
                oldPixelData.PhotometricInterpretation == PhotometricInterpretation.YbrPartial420)
                throw new InvalidOperationException(
                    "Photometric Interpretation '{oldPixelData.PhotometricInterpretation}' not supported by JPEG-LS encoder");

            var jparameters = new JlsParameters
            {
                width = oldPixelData.Width,
                height = oldPixelData.Height,
                bitsPerSample = oldPixelData.BitsStored,
                stride = oldPixelData.BytesAllocated * oldPixelData.Width * oldPixelData.SamplesPerPixel,
                components = oldPixelData.SamplesPerPixel,
                interleaveMode = oldPixelData.SamplesPerPixel == 1
                    ? InterleaveMode.None
                    : oldPixelData.PlanarConfiguration == PlanarConfiguration.Interleaved
                        ? InterleaveMode.Sample
                        : InterleaveMode.Line,
                colorTransformation = ColorTransformation.None
            };

            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var frameData = oldPixelData.GetFrame(frame);

                // assume compressed frame will be smaller than original
                var jpegData = new byte[frameData.Size];

                ulong jpegDataSize;
                string errorMessage;
                var err = JpegLs.Encode(jpegData, frameData.Data, jparameters, out jpegDataSize, out errorMessage);
                if (err != ApiResult.OK) throw new InvalidOperationException(GetErrorMessage(err, errorMessage));

                Array.Resize(ref jpegData, (int) jpegDataSize + ((jpegDataSize & 1) == 1 ? 1 : 0));

                newPixelData.AddFrame(new MemoryByteBuffer(jpegData));
            }
        }

        internal static void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomJpegLsParams parameters)
        {
            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                var jpegData = oldPixelData.GetFrame(frame);

                var frameSize = newPixelData.UncompressedFrameSize;
                if ((frameSize & 1) == 1) ++frameSize;
                var frameData = new byte[frameSize];

                string errorMessage;
                var err = JpegLs.Decode(frameData, jpegData.Data, null, out errorMessage);
                if (err != ApiResult.OK) throw new InvalidOperationException(GetErrorMessage(err, errorMessage));

                newPixelData.AddFrame(new MemoryByteBuffer(frameData));
            }
        }

        private static string GetErrorMessage(ApiResult error, string errorMessage)
        {
            return $"[{error}] {errorMessage}";
        }
    }
}
