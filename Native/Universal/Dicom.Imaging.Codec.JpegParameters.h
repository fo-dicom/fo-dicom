// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#ifndef __DICOM_IMAGING_CODEC_JPEGPARAMETERS_H__
#define __DICOM_IMAGING_CODEC_JPEGPARAMETERS_H__

#pragma once

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

	private class DicomJpegSampleFactor {
	public:
		enum { SF444 = 0, SF422 = 1, Unknown = 2 };
	};

	public ref class NativeJpegParameters sealed
	{
	public:
		NativeJpegParameters();

		property int Quality;
		property int SmoothingFactor;
		property bool ConvertColorspaceToRGB;
		property int SampleFactor;
		property int Predictor;
		property int PointTransform;
	};

} // Codec
} // Imaging
} // Dicom

#endif