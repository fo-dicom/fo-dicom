// ReSharper disable CheckNamespace

using Dicom.IO.Buffer;

namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
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
					       BytesAllocated = dicomPixelData.BytesAllocated,
					       UncompressedFrameSize = dicomPixelData.UncompressedFrameSize,
					       PlanarConfiguration = (int)dicomPixelData.PlanarConfiguration,
						   PhotometricInterpretation = dicomPixelData.PhotometricInterpretation.Value,
					       GetFrameImpl = index => dicomPixelData.GetFrame(index).Data,
					       AddFrameImpl = buffer => dicomPixelData.AddFrame(new MemoryByteBuffer(buffer)),
						   SetPhotometricInterpretationImpl = value => dicomPixelData.PhotometricInterpretation = PhotometricInterpretation.Parse(value)
				       };
		}

		#endregion
	}
}