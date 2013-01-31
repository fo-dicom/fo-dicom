#ifndef __DICOM_IMAGING_CODEC_JPEG2000PARAMETERS_H__
#define __DICOM_IMAGING_CODEC_JPEG2000PARAMETERS_H__

#pragma once

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

	public ref class NativeJpeg2000Parameters sealed
	{
	public:
		NativeJpeg2000Parameters();

		property bool Irreversible;
		property int Rate;
		property Array<int>^ RateLevels;
		property bool IsVerbose;
		property bool AllowMCT;
		property bool UpdatePhotometricInterpretation;
		property bool EncodeSignedPixelValuesAsUnsigned;
	};

} // Codec
} // Imaging
} // Dicom

#endif