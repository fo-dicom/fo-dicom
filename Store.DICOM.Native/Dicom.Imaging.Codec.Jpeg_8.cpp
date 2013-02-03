#include <vector>

#include "Dicom.Imaging.Codec.Jpeg.h"
#include "Dicom.Imaging.Codec.PixelData.h"

using namespace Platform;

#define IJGVERS IJG8
#define JPEGCODEC Jpeg8Codec

namespace Dicom {
namespace Imaging {
namespace Codec {
namespace Jpeg {

extern "C" {
#define boolean ijg_boolean
#include "stdio.h"
#include "string.h"
#include "setjmp.h"
#include "libijg8/jpeglib8.h"
#include "libijg8/jerror8.h"
#include "libijg8/jpegint8.h"
#undef boolean

// disable any preprocessor magic the IJG library might be doing with the "const" keyword
#ifdef const
#undef const
#endif
} // extern "C"

#include "Dicom.Imaging.Codec.Jpeg.i"

} // Jpeg
} // Codec
} // Imaging
} // Dicom