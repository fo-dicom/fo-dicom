// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "Dicom.Imaging.Codec.Jpeg2000Parameters.h"

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

NativeJpeg2000Parameters::NativeJpeg2000Parameters()
{
	Irreversible = true;
	Rate = 20;
	IsVerbose = false;
	AllowMCT = true;
	UpdatePhotometricInterpretation = true;
	EncodeSignedPixelValuesAsUnsigned = false;

	int rates[] = {1280, 640, 320, 160, 80, 40, 20, 10, 5 };
	RateLevels = ref new Array<int>(rates, 9);
}

} // Codec
} // Imaging
} // Dicom