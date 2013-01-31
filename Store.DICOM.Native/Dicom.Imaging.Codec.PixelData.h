#ifndef __DICOM_IMAGING_CODEC_PIXELDATA_H__
#define __DICOM_IMAGING_CODEC_PIXELDATA_H__

#pragma once

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

	private class PlanarConfiguration {
	public:
		enum { Interleaved = 0, Planar = 1 };
	};

	private class PixelRepresentation {
	public:
		enum { Unsigned = 0, Signed = 1 };
	};

	public delegate Array<unsigned char>^ GetFrameDelegate(int index);
	public delegate void AddFrameDelegate(const Array<unsigned char>^ buffer);

	public ref class NativePixelData sealed
	{
	public:
		property GetFrameDelegate^ GetFrameImpl;
		property AddFrameDelegate^ AddFrameImpl;

		property int NumberOfFrames;
		property int Width;
		property int Height;
		property int BytesAllocated;
		property int SamplesPerPixel;
		property int UncompressedFrameSize;
		property int PlanarConfiguration;
		property String^ PhotometricInterpretation;

		Array<unsigned char>^ GetFrame(int index);
		void AddFrame(const Array<unsigned char>^ buffer);
	};

} // Codec
} // Imaging
} // Dicom

#endif