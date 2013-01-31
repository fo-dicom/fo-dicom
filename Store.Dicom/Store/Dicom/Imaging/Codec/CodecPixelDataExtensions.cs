// ReSharper disable CheckNamespace

using Dicom.IO.Buffer;

namespace Dicom.Imaging.Codec
// ReSharper restore CheckNamespace
{
	public static class CodecPixelDataExtensions
	{
		#region METHODS

		public static CodecPixelData ToCodecPixelData(this DicomPixelData dicomPixelData)
		{
			return new CodecPixelData
				       {
					       NumberOfFrames = dicomPixelData.NumberOfFrames,
					       Width = dicomPixelData.Width,
					       Height = dicomPixelData.Height,
					       SamplesPerPixel = dicomPixelData.SamplesPerPixel,
					       BytesAllocated = dicomPixelData.BytesAllocated,
					       UncompressedFrameSize = dicomPixelData.UncompressedFrameSize,
					       PlanarConfiguration = dicomPixelData.PlanarConfiguration,
					       GetFrameImpl = index => dicomPixelData.GetFrame(index).Data,
					       AddFrameImpl = buffer => dicomPixelData.AddFrame(new MemoryByteBuffer(buffer))
				       };
		}

		#endregion
	}
}