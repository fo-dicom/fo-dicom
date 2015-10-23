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