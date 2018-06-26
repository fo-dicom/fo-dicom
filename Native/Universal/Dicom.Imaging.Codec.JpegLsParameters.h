// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#ifndef __DICOM_IMAGING_CODEC_JPEGLSPARAMETERS_H__
#define __DICOM_IMAGING_CODEC_JPEGLSPARAMETERS_H__

#pragma once

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

	private class DicomJpegLsInterleaveMode {
	public:
		enum { None = 0, Line = 1, Sample = 2 };
	};

	private class DicomJpegLsColorTransform {
	public:
		enum { None = 0, HP1 = 1, HP2 = 2, HP3 = 3 };
	};

	public ref class NativeJpegLSParameters sealed
	{
	public:
		NativeJpegLSParameters();

		property int AllowedError;
		property int InterleaveMode;
		property int ColorTransform;
	};

} // Codec
} // Imaging
} // Dicom

#endif