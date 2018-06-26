// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "Dicom.Imaging.Codec.JpegParameters.h"

namespace Dicom {
namespace Imaging {
namespace Codec {

NativeJpegParameters::NativeJpegParameters()
{
	Quality = 90;
	SmoothingFactor = 0;
	ConvertColorspaceToRGB = false;
	SampleFactor = DicomJpegSampleFactor::SF444;
	Predictor = 1;
	PointTransform = 0;
}

} // Codec
} // Imaging
} // Dicom