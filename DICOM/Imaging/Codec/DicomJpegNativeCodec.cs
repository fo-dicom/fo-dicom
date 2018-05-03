// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging.Codec.Jpeg;

namespace Dicom.Imaging.Codec
{
    public abstract class DicomJpegNativeCodec : DicomJpegCodec
    {
        #region CONSTRUCTORS

        #endregion

        #region METHODS

        protected abstract IJpegNativeCodec GetCodec(int bits, DicomJpegParams jparams);

        public override void Encode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            if (oldPixelData.NumberOfFrames == 0) return;

            // IJG eats the extra padding bits. Is there a better way to test for this?
            if (oldPixelData.BitsAllocated == 16 && oldPixelData.BitsStored <= 8)
            {
                // check for embedded overlays?
                newPixelData.Dataset.AddOrUpdate(DicomTag.BitsAllocated, (ushort)8);
            }

            var jparams = parameters as DicomJpegParams ?? GetDefaultParameters() as DicomJpegParams;

            var codec = GetCodec(oldPixelData.BitsStored, jparams);

            var oldNativeData = oldPixelData.ToNativePixelData();
            var newNativeData = newPixelData.ToNativePixelData();
            var jNativeParams = jparams.ToNativeJpegParameters();
            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                codec.Encode(oldNativeData, newNativeData, jNativeParams, frame);
            }
        }

        public override void Decode(
            DicomPixelData oldPixelData,
            DicomPixelData newPixelData,
            DicomCodecParams parameters)
        {
            if (oldPixelData.NumberOfFrames == 0) return;

            // IJG eats the extra padding bits. Is there a better way to test for this?
            if (newPixelData.BitsAllocated == 16 && newPixelData.BitsStored <= 8)
            {
                // check for embedded overlays here or below?
                newPixelData.Dataset.AddOrUpdate(DicomTag.BitsAllocated, (ushort)8);
            }

            var jparams = parameters as DicomJpegParams ?? GetDefaultParameters() as DicomJpegParams;

            var oldNativeData = oldPixelData.ToNativePixelData();
            int precision;
            try
            {
                try
                {
                    precision = JpegHelper.ScanJpegForBitDepth(oldPixelData);
                }
                catch
                {
                    // if the internal scanner chokes on an image, try again using ijg
                    precision = new Jpeg12Codec(JpegMode.Baseline, 0, 0).ScanHeaderForPrecision(oldNativeData);
                }
            }
            catch
            {
                // the old scanner choked on several valid images...
                // assume the correct encoder was used and let libijg handle the rest
                precision = oldPixelData.BitsStored;
            }

            if (newPixelData.BitsStored <= 8 && precision > 8)
                newPixelData.Dataset.AddOrUpdate(DicomTag.BitsAllocated, (ushort)16); // embedded overlay?

            var codec = GetCodec(precision, jparams);

            var newNativeData = newPixelData.ToNativePixelData();
            var jNativeParams = jparams.ToNativeJpegParameters();
            for (var frame = 0; frame < oldPixelData.NumberOfFrames; frame++)
            {
                codec.Decode(oldNativeData, newNativeData, jNativeParams, frame);
            }
        }

        #endregion
    }
}
