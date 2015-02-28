// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

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
						   SetPlanarConfigurationImpl = value => dicomPixelData.PlanarConfiguration = (PlanarConfiguration)value,
						   SetPhotometricInterpretationImpl = value => dicomPixelData.PhotometricInterpretation = PhotometricInterpretation.Parse(value)
				       };
		}

		#endregion
	}
}