// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
		static void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData);
		static void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData);
	};
} // Codec
} // Imaging
} // Dicom

#endif