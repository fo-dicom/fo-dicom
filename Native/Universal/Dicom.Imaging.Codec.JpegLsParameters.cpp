// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "Dicom.Imaging.Codec.JpegLSParameters.h"

namespace Dicom {
namespace Imaging {
namespace Codec {

NativeJpegLSParameters::NativeJpegLSParameters()
{
	AllowedError = 3;
	InterleaveMode = DicomJpegLsInterleaveMode::Line;
	ColorTransform = DicomJpegLsColorTransform::HP1;
}

} // Codec
} // Imaging
} // Dicom