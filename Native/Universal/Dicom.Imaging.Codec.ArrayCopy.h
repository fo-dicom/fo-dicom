// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#ifndef __DICOM_IMAGING_CODEC_ARRAYCOPY_H__
#define __DICOM_IMAGING_CODEC_ARRAYCOPY_H__

#pragma once

namespace Dicom {
namespace Imaging {
namespace Codec {
namespace Arrays {
	template<typename T> void Copy(
		const Platform::Array<T>^ sourceArray, 
		int sourceIndex, 
		Platform::Array<T>^ destinationArray, 
		int destinationIndex, 
		int length) {
			for (int i = 0; i < length; ++i, ++sourceIndex, ++destinationIndex)
				destinationArray[destinationIndex] = sourceArray[sourceIndex];
	};

	template<typename T> void Copy(
		const Platform::Array<T>^ sourceArray, 
		Platform::Array<T>^ destinationArray, 
		int length) {
			for (int i = 0; i < length; ++i)
				destinationArray[i] = sourceArray[i];
	};

	template<typename T> void Copy(
		T* const sourceArray,
		Platform::Array<T>^ destinationArray, 
		int length) {
			for (int i = 0; i < length; ++i)
				destinationArray[i] = sourceArray[i];
	};
} // Arrays
} // Codec
} // Imaging
} // Dicom

#endif