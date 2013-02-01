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

	int NativePixelData::Precision::get()
	{
		if (GetPrecisionImpl == nullptr) throw ref new NullReferenceException("GetPrecision delegate not defined");
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

} // Codec
} // Imaging
} // Dicom