#ifndef __DICOM_IMAGING_CODEC_PIXELDATA_H__
#define __DICOM_IMAGING_CODEC_PIXELDATA_H__

#pragma once

using namespace Platform;
using namespace Dicom::Imaging;

namespace Dicom {
namespace Imaging {
namespace Codec {

	public delegate Array<unsigned char>^ GetFrameDelegate(int index);
	public delegate void AddFrameDelegate(const Array<unsigned char>^ buffer);

	public ref class CodecPixelData sealed
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
		property PlanarConfiguration PlanarConfiguration;

		Array<unsigned char>^ GetFrame(int index);
		void AddFrame(const Array<unsigned char>^ buffer);
	};

} // Codec
} // Imaging
} // Dicom

#endif