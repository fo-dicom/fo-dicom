// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "Dicom.Imaging.Codec.Jpeg.h"

using namespace System;
using namespace System::IO;

using namespace Dicom;

using namespace Dicom::Imaging::Codec::Jpeg;

namespace Dicom {
	namespace Imaging {
		namespace Codec {

			void DicomJpegNativeCodec::Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters)
			{
				if (oldPixelData->NumberOfFrames == 0)
					return;

				// IJG eats the extra padding bits. Is there a better way to test for this?
				if (oldPixelData->BitsAllocated == 16 && oldPixelData->BitsStored <= 8) {
					// check for embedded overlays?
					newPixelData->Dataset->AddOrUpdate(DicomTag::BitsAllocated, (unsigned short)8);
				}

				if (parameters == nullptr || parameters->GetType() != DicomJpegParams::typeid)
					parameters = GetDefaultParameters();

				DicomJpegParams^ jparams = (DicomJpegParams^)parameters;

				JpegNativeCodec^ codec = GetCodec(oldPixelData->BitsStored, jparams);

				for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
					codec->Encode(oldPixelData, newPixelData, jparams, frame);
				}
			}

			void DicomJpegNativeCodec::Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters)
			{
				if (oldPixelData->NumberOfFrames == 0)
					return;

				// IJG eats the extra padding bits. Is there a better way to test for this?
				if (newPixelData->BitsAllocated == 16 && newPixelData->BitsStored <= 8) {
					// check for embedded overlays here or below?
					newPixelData->Dataset->AddOrUpdate(DicomTag::BitsAllocated, (unsigned short)8);
				}

				if (parameters == nullptr || parameters->GetType() != DicomJpegParams::typeid)
					parameters = GetDefaultParameters();

				DicomJpegParams^ jparams = (DicomJpegParams^)parameters;

				int precision = 0;
				try {
					try {
						precision = JpegHelper::ScanJpegForBitDepth(oldPixelData);
					}
					catch (...) {
						// if the internal scanner chokes on an image, try again using ijg
						Jpeg8Codec^ c = gcnew Jpeg8Codec(JpegMode::Baseline, 0, 0);
						precision = c->ScanHeaderForPrecision(oldPixelData);
					}
				}
				catch (...) {
					// the old scanner choked on several valid images...
					// assume the correct encoder was used and let libijg handle the rest
					precision = oldPixelData->BitsStored;
				}

				if (newPixelData->BitsStored <= 8 && precision > 8)
					newPixelData->Dataset->AddOrUpdate(DicomTag::BitsAllocated, (unsigned short)16); // embedded overlay?

				JpegNativeCodec^ codec = GetCodec(precision, jparams);

				for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
					codec->Decode(oldPixelData, newPixelData, jparams, frame);
				}
			}

		} // Codec
	} // Imaging
} // Dicom