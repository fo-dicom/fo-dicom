#ifndef __DICOM_IMAGING_CODEC_JPEGLS_H__
#define __DICOM_IMAGING_CODEC_JPEGLS_H__

#pragma once

#include "Dicom.Imaging.Codec.PixelData.h"
#include "Dicom.Imaging.Codec.JpegLsParameters.h"

namespace Dicom {
namespace Imaging {
namespace Codec {
	public ref class DicomJpegLsNativeCodec sealed
	{
	public:
		static void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegLsParameters^ parameters);
		static void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegLsParameters^ parameters);
	};
} // Codec
} // Imaging
} // Dicom

#endif