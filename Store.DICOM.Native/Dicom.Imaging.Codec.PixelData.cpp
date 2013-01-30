#include "Dicom.Imaging.Codec.PixelData.h"

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

PixelData::PixelData(GetFrameDelegate^ getFrame, AddFrameDelegate^ addFrame)
{
	_getFrame = getFrame;
	_addFrame = addFrame;
}

Array<unsigned char>^ PixelData::GetFrame(int index)
{
	return _getFrame(index);
}

void PixelData::AddFrame(const Array<unsigned char>^ buffer)
{
	_addFrame(buffer);
}

} // Codec
} // Imaging
} // Dicom