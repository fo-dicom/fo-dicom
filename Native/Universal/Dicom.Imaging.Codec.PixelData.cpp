// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "Dicom.Imaging.Codec.PixelData.h"

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

	String^ NativePixelData::PhotometricInterpretation::get()
	{
		return _photometricInterpretation;
	}

	void NativePixelData::PhotometricInterpretation::set(String^ value)
	{
		if (SetPhotometricInterpretationImpl != nullptr) SetPhotometricInterpretationImpl(value);
		_photometricInterpretation = value;
	}

	int NativePixelData::PlanarConfiguration::get()
	{
		return _planarConfiguration;
	}

	void NativePixelData::PlanarConfiguration::set(int value)
	{
		if (SetPlanarConfigurationImpl != nullptr) SetPlanarConfigurationImpl(value);
		_planarConfiguration = value;
	}

	Array<unsigned char>^ NativePixelData::GetFrame(int index)
	{
		if (GetFrameImpl == nullptr) throw ref new NullReferenceException("GetFrame delegate not defined");
		return GetFrameImpl(index);
	}

	void NativePixelData::AddFrame(const Array<unsigned char>^ buffer)
	{
		if (AddFrameImpl == nullptr) throw ref new NullReferenceException("AddFrame delegate not defined");
		AddFrameImpl(buffer);
	}

	Array<unsigned char>^ NativePixelData::InterleavedToPlanar24(Array<unsigned char>^ oldPixels)
	{
		Array<unsigned char>^ newPixels = ref new Array<unsigned char>(oldPixels->Length);
		int pixelCount = newPixels->Length / 3;

		for (int n = 0; n < pixelCount; n++) {
			newPixels[n + (pixelCount * 0)] = oldPixels[(n * 3) + 0];
			newPixels[n + (pixelCount * 1)] = oldPixels[(n * 3) + 1];
			newPixels[n + (pixelCount * 2)] = oldPixels[(n * 3) + 2];
		}

		return newPixels;
	}

	Array<unsigned char>^ NativePixelData::PlanarToInterleaved24(Array<unsigned char>^ oldPixels)
	{
		Array<unsigned char>^ newPixels = ref new Array<unsigned char>(oldPixels->Length);
		int pixelCount = newPixels->Length / 3;

		for (int n = 0; n < pixelCount; n++) {
			newPixels[(n * 3) + 0] = oldPixels[n + (pixelCount * 0)];
			newPixels[(n * 3) + 1] = oldPixels[n + (pixelCount * 1)];
			newPixels[(n * 3) + 2] = oldPixels[n + (pixelCount * 2)];
		}

		return newPixels;
	}

	Array<unsigned char>^ NativePixelData::UnpackLow16(Array<unsigned char>^ data) {
		Array<unsigned char>^ bytes = ref new Array<unsigned char>(data->Length / 2);
		for (unsigned int i = 0; i < bytes->Length && (i * 2) < data->Length; i++) {
			bytes[i] = data[i * 2];
		}
		return bytes;
	}

} // Codec
} // Imaging
} // Dicom