// Copyright (c) 2010-2015 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

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