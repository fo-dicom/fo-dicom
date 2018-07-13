// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#ifndef __DICOM_IMAGING_CODEC_JPEG2000_H__
#define __DICOM_IMAGING_CODEC_JPEG2000_H__

#include "Dicom.Imaging.Codec.PixelData.h"
#include "Dicom.Imaging.Codec.Jpeg2000Parameters.h"

#pragma once

namespace Dicom {
namespace Imaging {
namespace Codec {

	public ref class DicomJpeg2000NativeCodec sealed
	{
	public:
		static void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpeg2000Parameters^ parameters);
		static void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpeg2000Parameters^ parameters);
	};

} // Jpeg2000
} // Codec
} // Dicom

#endif
