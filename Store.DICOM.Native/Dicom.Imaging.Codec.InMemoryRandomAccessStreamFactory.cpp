// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

#include "Dicom.Imaging.Codec.InMemoryRandomAccessStreamFactory.h"

#ifdef WINDOWS_PHONE
#include "ppltasks.h"

using namespace Concurrency;
using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Storage;
#endif

using namespace Windows::Storage::Streams;

namespace Dicom {
namespace Imaging {
namespace Codec {

	IRandomAccessStream^ InMemoryRandomAccessStreamFactory::Create()
	{
#ifdef WINDOWS_PHONE
		auto tempFolder = ApplicationData::Current->LocalFolder;
		auto tempFile = create_task(tempFolder->CreateFileAsync("memory.stream", CreationCollisionOption::ReplaceExisting)).get();
		return create_task(tempFile->OpenAsync(FileAccessMode::ReadWrite)).get();
#else
		return ref new InMemoryRandomAccessStream();
#endif
	}

} // Codec
} // Imaging
} // Dicom
