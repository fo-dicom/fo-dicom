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