#include "Dicom.Imaging.Codec.JpegLs.h"

#include "CharLS/interface.h"
#include "CharLS/publictypes.h"
#include "CharLS/util.h"
#include "CharLS/defaulttraits.h"
#include "CharLS/losslesstraits.h"
#include "CharLS/colortransform.h"
#include "CharLS/processline.h"

using namespace System;

using namespace Dicom;
using namespace Dicom::IO;
using namespace Dicom::IO::Buffer;

namespace Dicom {
namespace Imaging {
namespace Codec {

public ref class DicomJpegLsCodecException : public DicomCodecException {
public:
	DicomJpegLsCodecException(JLS_ERROR error) : DicomCodecException(GetErrorMessage(error)) {
	}

private:
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
};

void DicomJpegLsNativeCodec::Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) {
	if ((oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull422)    ||
		(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial422) ||
		(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial420))
		throw gcnew DicomCodecException("Photometric Interpretation '{0}' not supported by JPEG-LS encoder", oldPixelData->PhotometricInterpretation);

	DicomJpegLsParams^ jparams = (DicomJpegLsParams^)parameters;
	if (jparams == nullptr)
		jparams = (DicomJpegLsParams^)GetDefaultParameters();

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

	if (TransferSyntax == DicomTransferSyntax::JPEGLSNearLossless) {
		params.allowedlossyerror = jparams->AllowedError;
	}

	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		IByteBuffer^ frameData = oldPixelData->GetFrame(frame);
		PinnedByteArray^ frameArray = gcnew PinnedByteArray(frameData->Data);

		// assume compressed frame will be smaller than original
		array<unsigned char>^ jpegData = gcnew array<unsigned char>(frameData->Size);
		PinnedByteArray^ jpegArray = gcnew PinnedByteArray(jpegData);

		size_t jpegDataSize = 0;

		JLS_ERROR err = JpegLsEncode((void*)jpegArray->Pointer, jpegArray->Count, &jpegDataSize, (void*)frameArray->Pointer, frameArray->Count, &params);
		if (err != OK) throw gcnew DicomJpegLsCodecException(err);

		Array::Resize(jpegData, (int)jpegDataSize);

		IByteBuffer^ buffer;
		if (jpegDataSize >= (1 * 1024 * 1024) || oldPixelData->NumberOfFrames > 1)
			buffer = gcnew TempFileBuffer(jpegData);
		else
			buffer = gcnew MemoryByteBuffer(jpegData);
		buffer = EvenLengthBuffer::Create(buffer);
		newPixelData->AddFrame(buffer);
	}
}

void DicomJpegLsNativeCodec::Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) {
	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		IByteBuffer^ jpegData = oldPixelData->GetFrame(frame);
		PinnedByteArray^ jpegArray = gcnew PinnedByteArray(jpegData->Data);

		array<unsigned char>^ frameData = gcnew array<unsigned char>(newPixelData->UncompressedFrameSize);
		PinnedByteArray^ frameArray = gcnew PinnedByteArray(frameData);

		JlsParameters params = {0};

		JLS_ERROR err = JpegLsDecode((void*)frameArray->Pointer, frameData->Length, (void*)jpegArray->Pointer, jpegData->Size, &params);
		if (err != OK) throw gcnew DicomJpegLsCodecException(err);

		IByteBuffer^ buffer;
		if (frameData->Length >= (1 * 1024 * 1024) || oldPixelData->NumberOfFrames > 1)
			buffer = gcnew TempFileBuffer(frameData);
		else
			buffer = gcnew MemoryByteBuffer(frameData);
		buffer = EvenLengthBuffer::Create(buffer);
		newPixelData->AddFrame(buffer);
	}
}

} // Codec
} // Imaging
} // Dicom