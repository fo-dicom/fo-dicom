#include "Dicom.Imaging.Codec.JpegLsParameters.h"

namespace Dicom {
namespace Imaging {
namespace Codec {

NativeJpegLsParameters::NativeJpegLsParameters()
{
	AllowedError = 3;
	InterleaveMode = DicomJpegLsInterleaveMode::Line;
	ColorTransform = DicomJpegLsColorTransform::HP1;
}

} // Codec
} // Imaging
} // Dicom