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
		static void Encode(PixelData^ oldPixelData, PixelData^ newPixelData);
		static void Decode(PixelData^ oldPixelData, PixelData^ newPixelData);
	};
} // Codec
} // Imaging
} // Dicom

#endif