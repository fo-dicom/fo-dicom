// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "CharLS/charls.h"
#include "Dicom.Imaging.Codec.JpegLs.h"

using namespace System;

using namespace Dicom;
using namespace Dicom::IO;
using namespace Dicom::IO::Buffer;

namespace Dicom {
	namespace Imaging {
		namespace Codec {

			public ref class DicomJpegLsCodecException : public DicomCodecException {
			public:
				DicomJpegLsCodecException(CharlsApiResultType error) : DicomCodecException(GetErrorMessage(error)) {
				}

			private:
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
			};

			void DicomJpegLsNativeCodec::Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) {
				if ((oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull422) ||
					(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial422) ||
					(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial420))
					throw gcnew DicomCodecException("Photometric Interpretation '{0}' not supported by JPEG-LS encoder", oldPixelData->PhotometricInterpretation);

				DicomJpegLsParams^ jparams = (DicomJpegLsParams^)parameters;
				if (jparams == nullptr)
					jparams = (DicomJpegLsParams^)GetDefaultParameters();

				JlsParameters params = { 0 };
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

				if (TransferSyntax == DicomTransferSyntax::JPEGLSNearLossless) {
					params.allowedLossyError = jparams->AllowedError;
				}

				for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
					IByteBuffer^ frameData = oldPixelData->GetFrame(frame);
					PinnedByteArray^ frameArray = gcnew PinnedByteArray(frameData->Data);

					// assume compressed frame will be smaller than original
					array<unsigned char>^ jpegData = gcnew array<unsigned char>(frameData->Size);
					PinnedByteArray^ jpegArray = gcnew PinnedByteArray(jpegData);

					size_t jpegDataSize = 0;

					char errorMessage[256];
					CharlsApiResultType err = JpegLsEncode((void*)jpegArray->Pointer, jpegArray->Count, &jpegDataSize, (void*)frameArray->Pointer, frameArray->Count, &params, errorMessage);
					if (err != CharlsApiResultType::OK) throw gcnew DicomJpegLsCodecException(err);

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

					JlsParameters params = { 0 };

					char errorMessage[256];
					CharlsApiResultType err = JpegLsDecode((void*)frameArray->Pointer, frameData->Length, (void*)jpegArray->Pointer, jpegData->Size, &params, errorMessage);
					if (err != CharlsApiResultType::OK) throw gcnew DicomJpegLsCodecException(err);

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