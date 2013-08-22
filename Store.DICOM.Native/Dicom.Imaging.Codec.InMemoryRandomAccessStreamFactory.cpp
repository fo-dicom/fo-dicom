#include "Dicom.Imaging.Codec.InMemoryRandomAccessStreamFactory.h"

using namespace Windows::Storage::Streams;

namespace Dicom {
namespace Imaging {
namespace Codec {

	IRandomAccessStream^ InMemoryRandomAccessStreamFactory::Create()
	{
#ifdef WINDOWS_PHONE
		// TODO Add actual implementation!
		return nullptr;
#else
		return ref new InMemoryRandomAccessStream();
#endif
	}

} // Codec
} // Imaging
} // Dicom
