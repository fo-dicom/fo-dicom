// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.IO.Buffer;

namespace Dicom.Imaging.Codec
{
    public static class NativePixelDataExtensions
    {
        #region METHODS

        public static NativePixelData ToNativePixelData(this DicomPixelData dicomPixelData)
        {
            return new NativePixelData
                       {
                           NumberOfFrames = dicomPixelData.NumberOfFrames,
                           Width = dicomPixelData.Width,
                           Height = dicomPixelData.Height,
                           SamplesPerPixel = dicomPixelData.SamplesPerPixel,
                           HighBit = dicomPixelData.HighBit,
                           BitsStored = dicomPixelData.BitsStored,
                           BitsAllocated = dicomPixelData.BitsAllocated,
                           BytesAllocated = dicomPixelData.BytesAllocated,
                           UncompressedFrameSize = dicomPixelData.UncompressedFrameSize,
                           PlanarConfiguration = (int)dicomPixelData.PlanarConfiguration,
                           PixelRepresentation = (int)dicomPixelData.PixelRepresentation,
                           TransferSyntaxIsLossy = dicomPixelData.Syntax.IsLossy,
                           PhotometricInterpretation = dicomPixelData.PhotometricInterpretation.Value,
                           GetFrameImpl = index => dicomPixelData.GetFrame(index).Data,
                           AddFrameImpl = buffer => dicomPixelData.AddFrame(new MemoryByteBuffer(buffer)),
                           SetPlanarConfigurationImpl =
                               value => dicomPixelData.PlanarConfiguration = (PlanarConfiguration)value,
                           SetPhotometricInterpretationImpl =
                               value =>
                               dicomPixelData.PhotometricInterpretation =
                               PhotometricInterpretation.Parse(value)
                       };
        }

        #endregion
    }
}
