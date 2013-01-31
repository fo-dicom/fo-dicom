#ifndef __DICOM_IMAGING_CODEC_RLE_H__
#define __DICOM_IMAGING_CODEC_RLE_H__

#include "Dicom.Imaging.Codec.PixelData.h"

#pragma once

namespace Dicom {
namespace Imaging {
namespace Codec {
	public ref class DicomRleNativeCodec sealed
	{
	public:
		static void Encode(CodecPixelData^ oldPixelData, CodecPixelData^ newPixelData);
		static void Decode(CodecPixelData^ oldPixelData, CodecPixelData^ newPixelData);
	};
} // Codec
} // Imaging
} // Dicom

#endif