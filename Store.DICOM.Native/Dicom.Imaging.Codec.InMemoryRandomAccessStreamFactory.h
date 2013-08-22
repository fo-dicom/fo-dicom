#ifndef __DICOM_IMAGING_CODEC_INMEMORYRANDOMACCESSSTREAMFACTORY_H__
#define __DICOM_IMAGING_CODEC_INMEMORYRANDOMACCESSSTREAMFACTORY_H__

#pragma once

using namespace Windows::Storage::Streams;

namespace Dicom {
namespace Imaging {
namespace Codec {

	public ref class InMemoryRandomAccessStreamFactory sealed
	{
	public:
		// METHODS
		static IRandomAccessStream^ Create();
	};

} // Codec
} // Imaging
} // Dicom

#endif