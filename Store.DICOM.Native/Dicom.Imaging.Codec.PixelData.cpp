#include "Dicom.Imaging.Codec.PixelData.h"

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

Array<unsigned char>^ CodecPixelData::GetFrame(int index)
{
	return GetFrameImpl(index);
}

void CodecPixelData::AddFrame(const Array<unsigned char>^ buffer)
{
	AddFrameImpl(buffer);
}

} // Codec
} // Imaging
} // Dicom