// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "CharLS/charls.h"

#include "Dicom.Imaging.Codec.JpegLS.h"
#include "Dicom.Imaging.Codec.ArrayCopy.h"

#include <algorithm>

namespace Dicom {
namespace Imaging {
namespace Codec {

static String^ GetErrorMessage(CharlsApiResultType error) {
	switch (error) {
	case CharlsApiResultType::InvalidJlsParameters:
		return "Invalid JPEG-LS parameters";
	case CharlsApiResultType::ParameterValueNotSupported:
		return "Parameter value not supported";
	case CharlsApiResultType::UncompressedBufferTooSmall:
		return "Uncompressed buffer too small";
	case CharlsApiResultType::CompressedBufferTooSmall:
		return "Compressed buffer too small";
	case CharlsApiResultType::InvalidCompressedData:
		return "Invalid compressed data";
	case CharlsApiResultType::TooMuchCompressedData:
		return "Too much compressed data";
	case CharlsApiResultType::ImageTypeNotSupported:
		return "Image type not supported";
	case CharlsApiResultType::UnsupportedBitDepthForTransform:
		return "Unsupported bit depth for transform";
	case CharlsApiResultType::UnsupportedColorTransform:
		return "Unsupported color transform";
	default:
		return "Unknown error";
	}
}

void DicomJpegLsNativeCodec::Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegLSParameters^ parameters) {
	if ((oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull422)    ||
		(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial422) ||
		(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial420))
		throw ref new FailureException("Photometric Interpretation '" + oldPixelData->PhotometricInterpretation + "' not supported by JPEG-LS encoder");

	NativeJpegLSParameters^ jparams = parameters == nullptr ? ref new NativeJpegLSParameters() : parameters;

	JlsParameters params = {0};
	params.width = oldPixelData->Width;
	params.height = oldPixelData->Height;
	params.bitsPerSample = oldPixelData->BitsStored;
	params.stride = oldPixelData->BytesAllocated * oldPixelData->Width * oldPixelData->SamplesPerPixel;
	params.components = oldPixelData->SamplesPerPixel;

	params.interleaveMode = oldPixelData->SamplesPerPixel == 1
		? CharlsInterleaveModeType::None :
		oldPixelData->PlanarConfiguration == PlanarConfiguration::Interleaved
			? CharlsInterleaveModeType::Sample
			: CharlsInterleaveModeType::Line;
	params.colorTransformation = CharlsColorTransformationType::None;

	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ frameData = oldPixelData->GetFrame(frame);

		// assume compressed frame will be smaller than original
		Array<unsigned char>^ jpegData = ref new Array<unsigned char>(frameData->Length);

		size_t jpegDataSize = 0;

		char errorMessage[256];
		CharlsApiResultType err = JpegLsEncode((void*)jpegData->begin(), jpegData->Length, &jpegDataSize, (void*)frameData->begin(), frameData->Length, &params, errorMessage);
		if (err != CharlsApiResultType::OK) throw ref new FailureException(GetErrorMessage(err));

		Array<unsigned char>^ buffer = ref new Array<unsigned char>(static_cast<unsigned int>(jpegDataSize + ((jpegDataSize & 1) == 1 ? 1 : 0)));
		Arrays::Copy(jpegData, buffer, static_cast<int>(jpegDataSize));

		newPixelData->AddFrame(buffer);
	}
}

void DicomJpegLsNativeCodec::Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegLSParameters^ parameters) {
	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ jpegData = oldPixelData->GetFrame(frame);

		int frameSize = newPixelData->UncompressedFrameSize; if ((frameSize & 1) == 1) ++frameSize;
		Array<unsigned char>^ frameData = ref new Array<unsigned char>(frameSize);

		JlsParameters params = {0};

		char errorMessage[256];
		CharlsApiResultType err = JpegLsDecode((void*)frameData->begin(), frameData->Length, (void*)jpegData->begin(), jpegData->Length, &params, errorMessage);
		if (err != CharlsApiResultType::OK) throw ref new FailureException(GetErrorMessage(err));

		newPixelData->AddFrame(frameData);
	}
}

} // Codec
} // Imaging
} // Dicom