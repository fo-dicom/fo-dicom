using Dicom.IO.Buffer;
using Dicom.Imaging.Codec.Jpeg;

// ReSharper disable CheckNamespace
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
						   SetPhotometricInterpretationImpl = value => dicomPixelData.PhotometricInterpretation = PhotometricInterpretation.Parse(value),
				       };
		}

		#endregion
	}
}