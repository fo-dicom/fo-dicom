#include "Dicom.Imaging.Codec.JpegLS.h"

#include "CharLS/interface.h"
#include "CharLS/publictypes.h"
#include "CharLS/util.h"
#include "CharLS/defaulttraits.h"
#include "CharLS/losslesstraits.h"
#include "CharLS/colortransform.h"
#include "CharLS/processline.h"

namespace Dicom {
namespace Imaging {
namespace Codec {

static String^ GetErrorMessage(JLS_ERROR error) {
	switch (error) {
	case InvalidJlsParameters:
		return "Invalid JPEG-LS parameters";
	case ParameterValueNotSupported:
		return "Parameter value not supported";
	case UncompressedBufferTooSmall:
		return "Uncompressed buffer too small";
	case CompressedBufferTooSmall:
		return "Compressed buffer too small";
	case InvalidCompressedData:
		return "Invalid compressed data";
	case TooMuchCompressedData:
		return "Too much compressed data";
	case ImageTypeNotSupported:
		return "Image type not supported";
	case UnsupportedBitDepthForTransform:
		return "Unsupported bit depth for transform";
	case UnsupportedColorTransform:
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
	params.bitspersample = oldPixelData->BitsStored;
	params.bytesperline = oldPixelData->BytesAllocated * oldPixelData->Width * oldPixelData->SamplesPerPixel;
	params.components = oldPixelData->SamplesPerPixel;

	params.ilv = ILV_NONE;
	params.colorTransform = COLORXFORM_NONE;

	if (oldPixelData->SamplesPerPixel == 3) {
		params.ilv = (interleavemode)jparams->InterleaveMode;
		if (oldPixelData->PhotometricInterpretation == PhotometricInterpretation::Rgb)
			params.colorTransform = (int)jparams->ColorTransform;
	}

	if (oldPixelData->TransferSyntaxIsLossy) {
		params.allowedlossyerror = jparams->AllowedError;
	}

	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ frameData = oldPixelData->GetFrame(frame);

		// assume compressed frame will be smaller than original
		Array<unsigned char>^ jpegData = ref new Array<unsigned char>(frameData->Length);

		size_t jpegDataSize = 0;

		JLS_ERROR err = JpegLsEncode((void*)jpegData->Data, jpegData->Length, &jpegDataSize, (void*)frameData->Data, frameData->Length, &params);
		if (err != OK) throw ref new FailureException(GetErrorMessage(err));

		Array<unsigned char>^ buffer = ref new Array<unsigned char>(jpegDataSize + ((jpegDataSize & 1) == 1 ? 1 : 0));
		for (int i = 0; i < jpegDataSize; ++i) buffer[i] = jpegData[i];

		newPixelData->AddFrame(buffer);
	}
}

void DicomJpegLsNativeCodec::Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegLSParameters^ parameters) {
	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ jpegData = oldPixelData->GetFrame(frame);

		int frameSize = newPixelData->UncompressedFrameSize; if ((frameSize & 1) == 1) ++frameSize;
		Array<unsigned char>^ frameData = ref new Array<unsigned char>(frameSize);

		JlsParameters params = {0};

		JLS_ERROR err = JpegLsDecode((void*)frameData->Data, frameData->Length, (void*)jpegData->Data, jpegData->Length, &params);
		if (err != OK) throw ref new FailureException(GetErrorMessage(err));

		newPixelData->AddFrame(frameData);
	}
}

} // Codec
} // Imaging
} // Dicom