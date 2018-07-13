// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#ifndef __DICOM_IMAGING_CODEC_JPEGLS_H__
#define __DICOM_IMAGING_CODEC_JPEGLS_H__

#pragma once

#include "Dicom.Imaging.Codec.PixelData.h"
#include "Dicom.Imaging.Codec.JpegLSParameters.h"

namespace Dicom {
namespace Imaging {
namespace Codec {
	public ref class DicomJpegLsNativeCodec sealed
	{
	public:
		static void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegLSParameters^ parameters);
		static void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegLSParameters^ parameters);
	};
} // Codec
} // Imaging
} // Dicom

#endif