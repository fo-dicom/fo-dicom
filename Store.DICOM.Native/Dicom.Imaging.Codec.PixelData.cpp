#include "Dicom.Imaging.Codec.PixelData.h"

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

Array<unsigned char>^ NativePixelData::GetFrame(int index)
{
	return GetFrameImpl(index);
}

void NativePixelData::AddFrame(const Array<unsigned char>^ buffer)
{
	AddFrameImpl(buffer);
}

} // Codec
} // Imaging
} // Dicom