// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#include "stdio.h"
#include "string.h"
#include <vector>

#include "Dicom.Imaging.Codec.Jpeg2000.h"
#include "Dicom.Imaging.Codec.ArrayCopy.h"

extern "C" {
#include "OpenJPEG/openjpeg.h"
#include "OpenJPEG/j2k.h"

	void opj_error_callback(const char *msg, void *usr) {
		//Dicom::Debug::Log->Error("OpenJPEG: {0}", gcnew String(msg));
	}

	void opj_warning_callback(const char *msg, void *) {
		//Dicom::Debug::Log->Warn("OpenJPEG: {0}", gcnew String(msg));
	}

	void opj_info_callback(const char *msg, void *) {
		//Dicom::Debug::Log->Info("OpenJPEG: {0}", gcnew String(msg));
	}
}

namespace Dicom {
namespace Imaging {
namespace Codec {

OPJ_COLOR_SPACE getOpenJpegColorSpace(String^ photometricInterpretation) {
	if (photometricInterpretation == PhotometricInterpretation::Rgb)
		return CLRSPC_SRGB;
	else if (photometricInterpretation == PhotometricInterpretation::Monochrome1 || photometricInterpretation == PhotometricInterpretation::Monochrome2)
		return CLRSPC_GRAY;
	else if (photometricInterpretation == PhotometricInterpretation::PaletteColor)
		return CLRSPC_GRAY;
	else if (photometricInterpretation == PhotometricInterpretation::YbrFull || photometricInterpretation == PhotometricInterpretation::YbrFull422 || photometricInterpretation == PhotometricInterpretation::YbrPartial422)
		return CLRSPC_SYCC;
	else
		return CLRSPC_UNKNOWN;
}

void DicomJpeg2000NativeCodec::Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpeg2000Parameters^ parameters) {
	if ((oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull422)    ||
		(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial422) ||
		(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial420))
		throw ref new FailureException("Photometric Interpretation '" + oldPixelData->PhotometricInterpretation + "' not supported by JPEG 2000 encoder");

	NativeJpeg2000Parameters^ jparams = parameters == nullptr ? ref new NativeJpeg2000Parameters() : parameters;

	int pixelCount = oldPixelData->Height * oldPixelData->Width;

	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ frameData = oldPixelData->GetFrame(frame);

		opj_image_cmptparm_t cmptparm[3];
		opj_cparameters_t eparams;  /* compression parameters */
		opj_event_mgr_t event_mgr;  /* event manager */
		opj_cinfo_t* cinfo = NULL;  /* handle to a compressor */
		opj_image_t *image = NULL;
		opj_cio_t *cio = NULL;

		memset(&event_mgr, 0, sizeof(opj_event_mgr_t));
		event_mgr.error_handler = opj_error_callback;
		if (jparams->IsVerbose) {
			event_mgr.warning_handler = opj_warning_callback;
			event_mgr.info_handler = opj_info_callback;
		}			

		cinfo = opj_create_compress(CODEC_J2K);

		opj_set_event_mgr((opj_common_ptr)cinfo, &event_mgr, NULL);

		opj_set_default_encoder_parameters(&eparams);
		eparams.cp_disto_alloc = 1;

		if (newPixelData->TransferSyntaxIsLossy && jparams->Irreversible)
			eparams.irreversible = 1;

		unsigned int r = 0;
		for (; r < jparams->RateLevels->Length; r++) {
			if (jparams->RateLevels[r] > jparams->Rate) {
				eparams.tcp_numlayers++;
				eparams.tcp_rates[r] = (float)jparams->RateLevels[r];
			} else
				break;
		}
		eparams.tcp_numlayers++;
		eparams.tcp_rates[r] = (float)jparams->Rate;

		if (!newPixelData->TransferSyntaxIsLossy && jparams->Rate > 0)
			eparams.tcp_rates[eparams.tcp_numlayers++] = 0;

		if (oldPixelData->PhotometricInterpretation == PhotometricInterpretation::Rgb && jparams->AllowMCT)
			eparams.tcp_mct = 1;

		memset(&cmptparm[0], 0, sizeof(opj_image_cmptparm_t) * 3);
		for (int i = 0; i < oldPixelData->SamplesPerPixel; i++) {
			cmptparm[i].bpp = oldPixelData->BitsAllocated;
			cmptparm[i].prec = oldPixelData->BitsStored;
			if (!jparams->EncodeSignedPixelValuesAsUnsigned)
				cmptparm[i].sgnd = oldPixelData->PixelRepresentation == PixelRepresentation::Signed;
			cmptparm[i].dx = eparams.subsampling_dx;
			cmptparm[i].dy = eparams.subsampling_dy;
			cmptparm[i].h = oldPixelData->Height;
			cmptparm[i].w = oldPixelData->Width;
		}

		try {
			OPJ_COLOR_SPACE color_space = getOpenJpegColorSpace(oldPixelData->PhotometricInterpretation);
			image = opj_image_create(oldPixelData->SamplesPerPixel, &cmptparm[0], color_space);

			image->x0 = eparams.image_offset_x0;
			image->y0 = eparams.image_offset_y0;
			image->x1 =	image->x0 + ((oldPixelData->Width - 1) * eparams.subsampling_dx) + 1;
			image->y1 =	image->y0 + ((oldPixelData->Height - 1) * eparams.subsampling_dy) + 1;

			for (int c = 0; c < image->numcomps; c++) {
				opj_image_comp_t* comp = &image->comps[c];

				int pos = oldPixelData->PlanarConfiguration == PlanarConfiguration::Planar ? (c * pixelCount) : c;
				const int offset = oldPixelData->PlanarConfiguration == PlanarConfiguration::Planar ? 1 : image->numcomps;

				if (oldPixelData->BytesAllocated == 1) {
					if (comp->sgnd) {
						if (oldPixelData->BitsStored < 8) {
							const unsigned char sign = 1 << oldPixelData->HighBit;
							const unsigned char mask = (0xff >> (oldPixelData->BitsAllocated - oldPixelData->BitsStored));
							for (int p = 0; p < pixelCount; p++) {
								const unsigned char pixel = frameData[pos];
								if (pixel & sign)
									comp->data[p] = -(((-pixel) & mask) + 1);
								else
									comp->data[p] = pixel;
								pos += offset;
							}
						}
						else {
							char* frameData8 = (char*)(void*)frameData->begin();
							for (int p = 0; p < pixelCount; p++) {
								comp->data[p] = frameData8[pos];
								pos += offset;
							}
						}
					}
					else {
						for (int p = 0; p < pixelCount; p++) {
							comp->data[p] = frameData[pos];
							pos += offset;
						}
					}
				}
				else if (oldPixelData->BytesAllocated == 2) {
					if (comp->sgnd) {
						if (oldPixelData->BitsStored < 16) {
							unsigned short* frameData16 = (unsigned short*)(void*)frameData->begin();
							const unsigned short sign = 1 << oldPixelData->HighBit;
							const unsigned short mask = (0xffff >> (oldPixelData->BitsAllocated - oldPixelData->BitsStored));
							for (int p = 0; p < pixelCount; p++) {
								const unsigned short pixel = frameData16[pos];
								if (pixel & sign)
									comp->data[p] = -(((-pixel) & mask) + 1);
								else
									comp->data[p] = pixel;
								pos += offset;
							}
						}
						else {
							short* frameData16 = (short*)(void*)frameData->begin();
							for (int p = 0; p < pixelCount; p++) {
								comp->data[p] = frameData16[pos];
								pos += offset;
							}
						}
					}
					else {
						unsigned short* frameData16 = (unsigned short*)(void*)frameData->begin();
						for (int p = 0; p < pixelCount; p++) {
							comp->data[p] = frameData16[pos];
							pos += offset;
						}
					}
				}
				else
					throw ref new FailureException("JPEG 2000 codec only supports Bits Allocated == 8 or 16");
			}

			opj_setup_encoder(cinfo, &eparams, image);

			cio = opj_cio_open((opj_common_ptr)cinfo, NULL, 0);

			if (opj_encode(cinfo, cio, image, eparams.index)) {
				int clen = cio_tell(cio);
				Array<unsigned char>^ cbuf = ref new Array<unsigned char>(clen + ((clen & 1) == 1 ? 1 : 0));
				Arrays::Copy(cio->buffer, cbuf, clen);

				newPixelData->AddFrame(cbuf);
			} else
				throw ref new FailureException("Unable to JPEG 2000 encode image");

			if (cio != nullptr)
				opj_cio_close(cio);
			if (image != nullptr)
				opj_image_destroy(image);
			if (cinfo != nullptr)
				opj_destroy_compress(cinfo);
		}
		catch (...) {
			if (cio != nullptr)
				opj_cio_close(cio);
			if (image != nullptr)
				opj_image_destroy(image);
			if (cinfo != nullptr)
				opj_destroy_compress(cinfo);

			throw;
		}
	}

	if (oldPixelData->PhotometricInterpretation == PhotometricInterpretation::Rgb) {
		newPixelData->PlanarConfiguration = PlanarConfiguration::Interleaved;

		if (jparams->AllowMCT && jparams->UpdatePhotometricInterpretation) {
			if (newPixelData->TransferSyntaxIsLossy && jparams->Irreversible)
				newPixelData->PhotometricInterpretation = PhotometricInterpretation::YbrIct;
			else
				newPixelData->PhotometricInterpretation = PhotometricInterpretation::YbrRct;
		}
	}
}

void DicomJpeg2000NativeCodec::Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpeg2000Parameters^ parameters) {
	NativeJpeg2000Parameters^ jparams = parameters == nullptr ? ref new NativeJpeg2000Parameters() : parameters;

	const int pixelCount = oldPixelData->Height * oldPixelData->Width;

	if (newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrIct || newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrRct)
		newPixelData->PhotometricInterpretation = PhotometricInterpretation::Rgb;

	if (newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull422 || newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial422)
		newPixelData->PhotometricInterpretation = PhotometricInterpretation::YbrFull;
	
	if (newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull)
		newPixelData->PlanarConfiguration = PlanarConfiguration::Planar;

	for (int frame = 0; frame < oldPixelData->NumberOfFrames; frame++) {
		Array<unsigned char>^ jpegData = oldPixelData->GetFrame(frame);

		// Destination frame should be of even length
		int frameSize = newPixelData->UncompressedFrameSize; if ((frameSize & 1) == 1) ++frameSize;
		Array<unsigned char>^ destArray = ref new Array<unsigned char>(frameSize);

		opj_dparameters_t dparams;
		opj_event_mgr_t event_mgr;
		opj_image_t *image = NULL;
		opj_dinfo_t* dinfo = NULL;
		opj_cio_t *cio = NULL;

		memset(&event_mgr, 0, sizeof(opj_event_mgr_t));
		event_mgr.error_handler = opj_error_callback;
		if (jparams->IsVerbose) {
			event_mgr.warning_handler = opj_warning_callback;
			event_mgr.info_handler = opj_info_callback;
		}

		opj_set_default_decoder_parameters(&dparams);
		dparams.cp_layer=0;
		dparams.cp_reduce=0;

		try {
			dinfo = opj_create_decompress(CODEC_J2K);

			opj_set_event_mgr((opj_common_ptr)dinfo, &event_mgr, NULL);

			opj_setup_decoder(dinfo, &dparams);

			bool opj_err = false;
			dinfo->client_data = (void*)&opj_err;

			cio = opj_cio_open((opj_common_ptr)dinfo, (unsigned char*)jpegData->begin(), (int)jpegData->Length);
			image = opj_decode(dinfo, cio);

			if (image == nullptr)
				throw ref new FailureException("Error in JPEG 2000 code stream!");

			for (int c = 0; c < image->numcomps; c++) {
				opj_image_comp_t* comp = &image->comps[c];

				int pos = newPixelData->PlanarConfiguration == PlanarConfiguration::Planar ? (c * pixelCount) : c;
				const int offset = newPixelData->PlanarConfiguration == PlanarConfiguration::Planar ? 1 : image->numcomps;

				if (newPixelData->BytesAllocated == 1) {
					if (comp->sgnd) {
						const unsigned char sign = 1 << newPixelData->HighBit;
                        const unsigned char mask = 0xFF ^ sign;
						for (int p = 0; p < pixelCount; p++) {
							const int i = comp->data[p];
							if (i < 0)
                                  destArray[pos] =(unsigned char)((i & mask) | sign);
							else
                                destArray[pos] = (unsigned char)(i & mask);
							pos += offset;
						}
					}
					else {
						for (int p = 0; p < pixelCount; p++) {
							destArray[pos] = (unsigned char)comp->data[p];
							pos += offset;
						}
					}
				}
				else if (newPixelData->BytesAllocated == 2) {
					const unsigned short sign = 1 << newPixelData->HighBit;
                    const unsigned short mask = 0xFFFF ^ sign;
					unsigned short* destData16 = (unsigned short*)(void *)destArray->begin();
					if (comp->sgnd) {
						for (int p = 0; p < pixelCount; p++) {
							const int i = comp->data[p];
							if (i < 0)
								//destData16[pos] = (unsigned short)(-i | sign);
                                destData16[pos] = (unsigned short)((i & mask) | sign);
							else
								//destData16[pos] = (unsigned short)(i);
                                destData16[pos] = (unsigned short)(i & mask);
							pos += offset;
						}
					}
					else {
						for (int p = 0; p < pixelCount; p++) {
							destData16[pos] = (unsigned short)comp->data[p];
							pos += offset;
						}
					}
				}
				else
					throw ref new FailureException("JPEG 2000 module only supports Bytes Allocated == 8 or 16!");
			}

			newPixelData->AddFrame(destArray);
		}
		catch (Exception^ e)
		{
			throw e;
		}
		catch (...)
		{
			throw ref new FailureException("Failed to decode JPEG 2000 image due to native error");
		}

		if (cio != nullptr)
			opj_cio_close(cio);
		if (dinfo != nullptr)
			opj_destroy_decompress(dinfo);
		if (image != nullptr)
			opj_image_destroy(image);
	}
}

} // Codec
} // Imaging
} // Dicom
