#include <vector>
#include <vcclr.h>

using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

using namespace Dicom;
using namespace Dicom::IO;
using namespace Dicom::IO::Buffer;
using namespace Dicom::Imaging;
using namespace Dicom::Log;

#include "Dicom.Imaging.Codec.Jpeg.h"

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