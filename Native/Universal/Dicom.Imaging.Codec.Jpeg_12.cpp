// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include <vector>
#include "ppltasks.h"

#include "Dicom.Imaging.Codec.ArrayCopy.h"
#include "Dicom.Imaging.Codec.Jpeg.h"
#include "Dicom.Imaging.Codec.PixelData.h"

using namespace Concurrency;
using namespace Platform;
using namespace Windows::Storage::Streams;

#define IJGVERS IJG12
#define JPEGCODEC Jpeg12Codec

namespace Dicom {
namespace Imaging {
namespace Codec {

extern "C" {
#define boolean ijg_boolean
#include "stdio.h"
#include "string.h"
#include "setjmp.h"
#include "libijg12/jpeglib12.h"
#include "libijg12/jerror12.h"
#include "libijg12/jpegint12.h"
#undef boolean

// disable any preprocessor magic the IJG library might be doing with the "const" keyword
#ifdef const
#undef const
#endif
} // extern "C"

#include "Dicom.Imaging.Codec.Jpeg.i"

} // Codec
} // Imaging
} // Dicom