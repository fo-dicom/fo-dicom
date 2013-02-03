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

} // Codec
} // Imaging
} // Dicom