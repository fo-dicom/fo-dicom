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
					       BytesAllocated = dicomPixelData.BytesAllocated,
					       UncompressedFrameSize = dicomPixelData.UncompressedFrameSize,
					       PlanarConfiguration = (int)dicomPixelData.PlanarConfiguration,
					       GetFrameImpl = index => dicomPixelData.GetFrame(index).Data,
					       AddFrameImpl = buffer => dicomPixelData.AddFrame(new MemoryByteBuffer(buffer))
				       };
		}

		#endregion
	}
}