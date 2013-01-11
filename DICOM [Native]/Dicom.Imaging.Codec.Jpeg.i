#define IJGE_BLOCKSIZE 16384

namespace IJGVERS {
	// private error handler struct
	struct ErrorStruct {
	  // the standard IJG error handler object
	  struct jpeg_error_mgr pub;
	};

	// error handler, executes longjmp
	void ErrorExit(j_common_ptr cinfo) {
		ErrorStruct *myerr = (ErrorStruct *)cinfo->err;
	}

	// message handler for warning messages and the like
	void OutputMessage(j_common_ptr cinfo) {
		ErrorStruct *myerr = (ErrorStruct *)cinfo->err;
		char buffer[JMSG_LENGTH_MAX];
		(*cinfo->err->format_message)((jpeg_common_struct *)cinfo, buffer); /* Create the message */
		//DicomLog::Debug::Log->Info("IJG: {0}", gcnew String(buffer));
	}
}


JPEGCODEC::JPEGCODEC(JpegMode mode, int predictor, int point_transform) {
	Mode = mode;
	Predictor = predictor;
	PointTransform = point_transform;
}

namespace IJGVERS {
	J_COLOR_SPACE getJpegColorSpace(PhotometricInterpretation^ photometricInterpretation) {
		if (photometricInterpretation == PhotometricInterpretation::Rgb)
			return JCS_RGB;
		else if (photometricInterpretation == PhotometricInterpretation::Monochrome1 || photometricInterpretation == PhotometricInterpretation::Monochrome2)
			return JCS_GRAYSCALE;
		else if (photometricInterpretation == PhotometricInterpretation::PaletteColor)
			return JCS_UNKNOWN;
		else if (photometricInterpretation == PhotometricInterpretation::YbrFull || photometricInterpretation == PhotometricInterpretation::YbrFull422 || PhotometricInterpretation::YbrPartial422)
			return JCS_YCbCr;
		else
			return JCS_UNKNOWN;
	}

	// callbacks for compress-destination-manager
	void initDestination(j_compress_ptr cinfo) {
		JPEGCODEC^ thisPtr = (JPEGCODEC^)JPEGCODEC::This;
		thisPtr->MemoryBuffer = gcnew MemoryStream();
		thisPtr->DataArray = gcnew PinnedByteArray(IJGE_BLOCKSIZE);
		cinfo->dest->next_output_byte = (unsigned char*)(void*)thisPtr->DataArray->Pointer;
		cinfo->dest->free_in_buffer = IJGE_BLOCKSIZE;
	}

	ijg_boolean emptyOutputBuffer(j_compress_ptr cinfo) {
		JPEGCODEC^ thisPtr = (JPEGCODEC^)JPEGCODEC::This;
		thisPtr->MemoryBuffer->Write(thisPtr->DataArray->Data, 0, IJGE_BLOCKSIZE);
		cinfo->dest->next_output_byte = (unsigned char*)(void*)thisPtr->DataArray->Pointer;
		cinfo->dest->free_in_buffer = IJGE_BLOCKSIZE;
		return TRUE;
	}

	void termDestination(j_compress_ptr cinfo) {
		JPEGCODEC^ thisPtr = (JPEGCODEC^)JPEGCODEC::This;
		int count = IJGE_BLOCKSIZE - cinfo->dest->free_in_buffer;
		thisPtr->MemoryBuffer->Write(thisPtr->DataArray->Data, 0, count);
		thisPtr->DataArray = nullptr;
	}

	// Borrowed from DCMTK djeijgXX.cxx
	/*
	 * jpeg_simple_spectral_selection() creates a scan script
	 * for progressive JPEG with spectral selection only,
	 * similar to jpeg_simple_progression() for full progression.
	 * The scan sequence for YCbCr is as proposed in the IJG documentation.
	 * The scan sequence for all other color models is somewhat arbitrary.
	 */
	void jpeg_simple_spectral_selection(j_compress_ptr cinfo) {
		int ncomps = cinfo->num_components;
		jpeg_scan_info *scanptr = NULL;
		int nscans = 0;

		/* Safety check to ensure start_compress not called yet. */
		if (cinfo->global_state != CSTATE_START) ERREXIT1(cinfo, JERR_BAD_STATE, cinfo->global_state);

		if (ncomps == 3 && cinfo->jpeg_color_space == JCS_YCbCr) nscans = 7;
		else nscans = 1 + 2 * ncomps;	/* 1 DC scan; 2 AC scans per component */

		/* Allocate space for script.
		* We need to put it in the permanent pool in case the application performs
		* multiple compressions without changing the settings.  To avoid a memory
		* leak if jpeg_simple_spectral_selection is called repeatedly for the same JPEG
		* object, we try to re-use previously allocated space, and we allocate
		* enough space to handle YCbCr even if initially asked for grayscale.
		*/
		if (cinfo->script_space == NULL || cinfo->script_space_size < nscans) {
		cinfo->script_space_size = nscans > 7 ? nscans : 7;
		cinfo->script_space = (jpeg_scan_info *)
		  (*cinfo->mem->alloc_small) ((j_common_ptr) cinfo, 
		  JPOOL_PERMANENT, cinfo->script_space_size * sizeof(jpeg_scan_info));
		}
		scanptr = cinfo->script_space;
		cinfo->scan_info = scanptr;
		cinfo->num_scans = nscans;

		if (ncomps == 3 && cinfo->jpeg_color_space == JCS_YCbCr) {
			/* Custom script for YCbCr color images. */

			// Interleaved DC scan for Y,Cb,Cr:
			scanptr[0].component_index[0] = 0;
			scanptr[0].component_index[1] = 1;
			scanptr[0].component_index[2] = 2;
			scanptr[0].comps_in_scan = 3;
			scanptr[0].Ss = 0;
			scanptr[0].Se = 0;
			scanptr[0].Ah = 0;
			scanptr[0].Al = 0;

			// AC scans
			// First two Y AC coefficients
			scanptr[1].component_index[0] = 0;
			scanptr[1].comps_in_scan = 1;
			scanptr[1].Ss = 1;
			scanptr[1].Se = 2;
			scanptr[1].Ah = 0;
			scanptr[1].Al = 0;

			// Three more
			scanptr[2].component_index[0] = 0;
			scanptr[2].comps_in_scan = 1;
			scanptr[2].Ss = 3;
			scanptr[2].Se = 5;
			scanptr[2].Ah = 0;
			scanptr[2].Al = 0;

			// All AC coefficients for Cb
			scanptr[3].component_index[0] = 1;
			scanptr[3].comps_in_scan = 1;
			scanptr[3].Ss = 1;
			scanptr[3].Se = 63;
			scanptr[3].Ah = 0;
			scanptr[3].Al = 0;

			// All AC coefficients for Cr
			scanptr[4].component_index[0] = 2;
			scanptr[4].comps_in_scan = 1;
			scanptr[4].Ss = 1;
			scanptr[4].Se = 63;
			scanptr[4].Ah = 0;
			scanptr[4].Al = 0;

			// More Y coefficients
			scanptr[5].component_index[0] = 0;
			scanptr[5].comps_in_scan = 1;
			scanptr[5].Ss = 6;
			scanptr[5].Se = 9;
			scanptr[5].Ah = 0;
			scanptr[5].Al = 0;

			// Remaining Y coefficients
			scanptr[6].component_index[0] = 0;
			scanptr[6].comps_in_scan = 1;
			scanptr[6].Ss = 10;
			scanptr[6].Se = 63;
			scanptr[6].Ah = 0;
			scanptr[6].Al = 0;
		}
		else
		{
			/* All-purpose script for other color spaces. */
			int j=0;

			// Interleaved DC scan for all components
			for (j=0; j<ncomps; j++) scanptr[0].component_index[j] = j;
			scanptr[0].comps_in_scan = ncomps;
			scanptr[0].Ss = 0;
			scanptr[0].Se = 0;
			scanptr[0].Ah = 0;
			scanptr[0].Al = 0;

			// first AC scan for each component
			for (j=0; j<ncomps; j++) {
				scanptr[j+1].component_index[0] = j;
				scanptr[j+1].comps_in_scan = 1;
				scanptr[j+1].Ss = 1;
				scanptr[j+1].Se = 5;
				scanptr[j+1].Ah = 0;
				scanptr[j+1].Al = 0;
			}

			// second AC scan for each component
			for (j=0; j<ncomps; j++) {
				scanptr[j+ncomps+1].component_index[0] = j;
				scanptr[j+ncomps+1].comps_in_scan = 1;
				scanptr[j+ncomps+1].Ss = 6;
				scanptr[j+ncomps+1].Se = 63;
				scanptr[j+ncomps+1].Ah = 0;
				scanptr[j+ncomps+1].Al = 0;
			}
		}
	}
}

void JPEGCODEC::Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) {
	if ((oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrIct) ||
		(oldPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrRct))
		throw gcnew DicomCodecException("Photometric Interpretation '{0}' not supported by JPEG encoder!", oldPixelData->PhotometricInterpretation);

	PinnedByteArray^ frameArray = nullptr;
	
	if (oldPixelData->BitsAllocated == 16 && oldPixelData->BitsStored <= 8) {
		IByteBuffer^ frameBuffer = oldPixelData->GetFrame(frame);
		frameArray = gcnew PinnedByteArray(
						ByteConverter::UnpackLow16(frameBuffer)->Data);
	}
	else
		frameArray = gcnew PinnedByteArray(oldPixelData->GetFrame(frame)->Data);

	try {
		if (oldPixelData->PlanarConfiguration == PlanarConfiguration::Planar && oldPixelData->SamplesPerPixel > 1) {
			if (oldPixelData->SamplesPerPixel != 3 || oldPixelData->BitsStored > 8)
				throw gcnew DicomCodecException("Planar reconfiguration only implemented for SamplesPerPixel=3 && BitsStores <= 8");

			newPixelData->PlanarConfiguration = PlanarConfiguration::Interleaved;
			frameArray = gcnew PinnedByteArray(
				PixelDataConverter::PlanarToInterleaved24(
					gcnew MemoryByteBuffer(frameArray->Data))->Data);
		}

		struct jpeg_compress_struct cinfo;
		struct IJGVERS::ErrorStruct jerr;
		cinfo.err = jpeg_std_error(&jerr.pub);
		jerr.pub.error_exit = IJGVERS::ErrorExit;
		jerr.pub.output_message = IJGVERS::OutputMessage;

		jpeg_create_compress(&cinfo);
		cinfo.client_data = nullptr;
		
		JPEGCODEC::This = this;

		// Specify destination manager
		jpeg_destination_mgr dest;
		dest.init_destination = IJGVERS::initDestination;
		dest.empty_output_buffer = IJGVERS::emptyOutputBuffer;
		dest.term_destination = IJGVERS::termDestination;
		cinfo.dest = &dest;

		cinfo.image_width = oldPixelData->Width;
		cinfo.image_height = oldPixelData->Height;
		cinfo.input_components = oldPixelData->SamplesPerPixel;
		cinfo.in_color_space = IJGVERS::getJpegColorSpace(oldPixelData->PhotometricInterpretation);

		jpeg_set_defaults(&cinfo);

		cinfo.optimize_coding = true;

		if (Mode == JpegMode::Baseline || Mode == JpegMode::Sequential) {
			jpeg_set_quality(&cinfo, params->Quality, 0);
		}
		else if (Mode == JpegMode::SpectralSelection) {
			jpeg_set_quality(&cinfo, params->Quality, 0);
			IJGVERS::jpeg_simple_spectral_selection(&cinfo);
		}
		else if (Mode == JpegMode::Progressive) {
			jpeg_set_quality(&cinfo, params->Quality, 0);
			jpeg_simple_progression(&cinfo);
		}
		else {
			jpeg_simple_lossless(&cinfo, Predictor, PointTransform);
		}
		
		cinfo.smoothing_factor = params->SmoothingFactor;

		if (Mode == JpegMode::Lossless) {
			jpeg_set_colorspace(&cinfo, cinfo.in_color_space);
			cinfo.comp_info[0].h_samp_factor = 1;
			cinfo.comp_info[0].v_samp_factor = 1;
		}
		else {
			// initialize sampling factors
			if (cinfo.jpeg_color_space == JCS_YCbCr && params->SampleFactor != DicomJpegSampleFactor::Unknown) {
				switch(params->SampleFactor) {
				  case DicomJpegSampleFactor::SF444: /* 4:4:4 sampling (no subsampling) */
					cinfo.comp_info[0].h_samp_factor = 1;
					cinfo.comp_info[0].v_samp_factor = 1;
					break;
				  case DicomJpegSampleFactor::SF422: /* 4:2:2 sampling (horizontal subsampling of chroma components) */
					cinfo.comp_info[0].h_samp_factor = 2;
					cinfo.comp_info[0].v_samp_factor = 1;
					break;
				//case DicomJpegSampleFactor::SF411: /* 4:1:1 sampling (horizontal and vertical subsampling of chroma components) */
				//	cinfo.comp_info[0].h_samp_factor = 2;
				//	cinfo.comp_info[0].v_samp_factor = 2;
				//	break;
				}
			}
			else {
				if (params->SampleFactor == DicomJpegSampleFactor::Unknown)
					jpeg_set_colorspace(&cinfo, cinfo.in_color_space);

				// JPEG color space is not YCbCr, disable subsampling.
				cinfo.comp_info[0].h_samp_factor = 1;
				cinfo.comp_info[0].v_samp_factor = 1;
			}
		}

		// all other components are set to 1x1
		for (int sfi = 1; sfi < MAX_COMPONENTS; sfi++) {
			cinfo.comp_info[sfi].h_samp_factor = 1;
			cinfo.comp_info[sfi].v_samp_factor = 1;
		}

		jpeg_start_compress(&cinfo, TRUE);

		JSAMPROW row_pointer[1];
		int row_stride = oldPixelData->Width * oldPixelData->SamplesPerPixel * (oldPixelData->BitsStored <= 8 ? 1 : oldPixelData->BytesAllocated);

		unsigned char* framePtr = (unsigned char*)(void*)frameArray->Pointer;
		while (cinfo.next_scanline < cinfo.image_height) {
			row_pointer[0] = (JSAMPLE *)(&framePtr[cinfo.next_scanline * row_stride]);
			jpeg_write_scanlines(&cinfo, row_pointer, 1);
		}

		jpeg_finish_compress(&cinfo);
		jpeg_destroy_compress(&cinfo);

		if (oldPixelData->PhotometricInterpretation == PhotometricInterpretation::Rgb && cinfo.jpeg_color_space == JCS_YCbCr) {
			if (params->SampleFactor == DicomJpegSampleFactor::SF422)
				newPixelData->PhotometricInterpretation = PhotometricInterpretation::YbrFull422;
			else
				newPixelData->PhotometricInterpretation = PhotometricInterpretation::YbrFull;
		}

		IByteBuffer^ buffer;
		if (MemoryBuffer->Length >= (1 * 1024 * 1024) || oldPixelData->NumberOfFrames > 1)
			buffer = gcnew TempFileBuffer(MemoryBuffer->ToArray());
		else
			buffer = gcnew MemoryByteBuffer(MemoryBuffer->ToArray());
		buffer = EvenLengthBuffer::Create(buffer);
		newPixelData->AddFrame(buffer);
	} finally {
		MemoryBuffer = nullptr;
	}
}



namespace IJGVERS {
	// private source manager struct
	struct SourceManagerStruct {
		// the standard IJG source manager object
		struct jpeg_source_mgr pub;

		// number of bytes to skip at start of buffer
		long skip_bytes;

		// buffer from which reading will continue as soon as the current buffer is empty
		unsigned char *next_buffer;

		// buffer size
		unsigned int *next_buffer_size;
	};

	void initSource(j_decompress_ptr /* cinfo */) {
	}

	ijg_boolean fillInputBuffer(j_decompress_ptr cinfo) {
		SourceManagerStruct *src = (SourceManagerStruct *)(cinfo->src);

		// if we already have the next buffer, switch buffers
		if (src->next_buffer) {
			src->pub.next_input_byte    = src->next_buffer;
			src->pub.bytes_in_buffer    = (unsigned int) src->next_buffer_size;
			src->next_buffer            = NULL;
			src->next_buffer_size       = 0;

			// The suspension was caused by IJG16skipInputData iff src->skip_bytes > 0.
			// In this case we must skip the remaining number of bytes here.
			if (src->skip_bytes > 0) {
				if (src->pub.bytes_in_buffer < (unsigned long) src->skip_bytes) {
					src->skip_bytes            -= src->pub.bytes_in_buffer;
					src->pub.next_input_byte   += src->pub.bytes_in_buffer;
					src->pub.bytes_in_buffer    = 0;
					// cause a suspension return
					return FALSE;
				}
				else {
					src->pub.bytes_in_buffer   -= (unsigned int) src->skip_bytes;
					src->pub.next_input_byte   += src->skip_bytes;
					src->skip_bytes             = 0;
				}
			}
			return TRUE;
		}

		// otherwise cause a suspension return
		return FALSE;
	}

	void skipInputData(j_decompress_ptr cinfo, long num_bytes) {
		SourceManagerStruct *src = (SourceManagerStruct *)(cinfo->src);

		if (src->pub.bytes_in_buffer < (size_t)num_bytes) {
			src->skip_bytes             = num_bytes - src->pub.bytes_in_buffer;
			src->pub.next_input_byte   += src->pub.bytes_in_buffer;
			src->pub.bytes_in_buffer    = 0; // causes a suspension return
		}
		else {
			src->pub.bytes_in_buffer   -= (unsigned int) num_bytes;
			src->pub.next_input_byte   += num_bytes;
			src->skip_bytes             = 0;
		}
	}

	void termSource(j_decompress_ptr /* cinfo */) {
	}
}

void JPEGCODEC::Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) {
	PinnedByteArray^ jpegArray = gcnew PinnedByteArray(oldPixelData->GetFrame(frame)->Data);
	
	jpeg_decompress_struct dinfo;
	memset(&dinfo, 0, sizeof(dinfo));

	IJGVERS::SourceManagerStruct src;
	memset(&src, 0, sizeof(IJGVERS::SourceManagerStruct));
	src.pub.init_source       = IJGVERS::initSource;
	src.pub.fill_input_buffer = IJGVERS::fillInputBuffer;
	src.pub.skip_input_data   = IJGVERS::skipInputData;
	src.pub.resync_to_restart = jpeg_resync_to_restart;
	src.pub.term_source       = IJGVERS::termSource;
	src.pub.bytes_in_buffer   = 0;
	src.pub.next_input_byte   = NULL;
	src.skip_bytes            = 0;
	src.next_buffer           = (unsigned char*)(void*)jpegArray->Pointer;
	src.next_buffer_size      = (unsigned int*)jpegArray->ByteSize;

    IJGVERS::ErrorStruct jerr;
	memset(&jerr, 0, sizeof(IJGVERS::ErrorStruct));
	dinfo.err = jpeg_std_error(&jerr.pub);
	jerr.pub.error_exit = IJGVERS::ErrorExit;
	jerr.pub.output_message = IJGVERS::OutputMessage;

	jpeg_create_decompress(&dinfo);
	dinfo.src = (jpeg_source_mgr*)&src.pub;

	if (jpeg_read_header(&dinfo, TRUE) == JPEG_SUSPENDED)
		throw gcnew DicomCodecException("Unable to decompress JPEG: Suspended");
		
	if (newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull422 || newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrPartial422)
		newPixelData->PhotometricInterpretation = PhotometricInterpretation::YbrFull;
	else
		newPixelData->PhotometricInterpretation = oldPixelData->PhotometricInterpretation;

	if (params->ConvertColorspaceToRGB && (dinfo.out_color_space == JCS_YCbCr || dinfo.out_color_space == JCS_RGB)) { 
		if (oldPixelData->PixelRepresentation == PixelRepresentation::Signed) 
			throw gcnew DicomCodecException("JPEG codec unable to perform colorspace conversion on signed pixel data");
		//dinfo.jpeg_color_space = JCS_YCbCr;
		dinfo.out_color_space = JCS_RGB;
		newPixelData->PhotometricInterpretation = PhotometricInterpretation::Rgb;
		newPixelData->PlanarConfiguration = PlanarConfiguration::Interleaved;
	}
	else {
		dinfo.jpeg_color_space = JCS_UNKNOWN; 
		dinfo.out_color_space = JCS_UNKNOWN;
	}
	
	if (newPixelData->PhotometricInterpretation == PhotometricInterpretation::YbrFull)
		newPixelData->PlanarConfiguration = PlanarConfiguration::Planar;

	jpeg_calc_output_dimensions(&dinfo);
	jpeg_start_decompress(&dinfo);

	int rowSize = dinfo.output_width * dinfo.output_components * sizeof(JSAMPLE);
	int frameSize = rowSize * dinfo.output_height;
	if ((frameSize % 2) != 0)
		frameSize++;

	PinnedByteArray^ frameArray = gcnew PinnedByteArray(frameSize);
	unsigned char* framePtr = (unsigned char*)(void*)frameArray->Pointer;

	while (dinfo.output_scanline < dinfo.output_height) {
		int rows = jpeg_read_scanlines(&dinfo, (JSAMPARRAY)&framePtr, 1);
		framePtr += rows * rowSize;
	}

	jpeg_destroy_decompress(&dinfo);

	IByteBuffer^ buffer;
	if (frameArray->Count >= (1 * 1024 * 1024) || oldPixelData->NumberOfFrames > 1)
		buffer = gcnew TempFileBuffer(frameArray->Data);
	else
		buffer = gcnew MemoryByteBuffer(frameArray->Data);
	buffer = EvenLengthBuffer::Create(buffer);
	
	if (newPixelData->PlanarConfiguration == PlanarConfiguration::Planar && newPixelData->SamplesPerPixel > 1) {
		if (oldPixelData->SamplesPerPixel != 3 || oldPixelData->BitsStored > 8)
				throw gcnew DicomCodecException("Planar reconfiguration only implemented for SamplesPerPixel=3 && BitsStores <= 8");

		buffer = PixelDataConverter::InterleavedToPlanar24(buffer);
	}

	newPixelData->AddFrame(buffer);
}

int JPEGCODEC::ScanHeaderForPrecision(DicomPixelData^ pixelData) {
	PinnedByteArray^ jpegArray = gcnew PinnedByteArray(pixelData->GetFrame(0)->Data);
	
	jpeg_decompress_struct dinfo;
	memset(&dinfo, 0, sizeof(dinfo));

	IJGVERS::SourceManagerStruct src;
	memset(&src, 0, sizeof(IJGVERS::SourceManagerStruct));
	src.pub.init_source       = IJGVERS::initSource;
	src.pub.fill_input_buffer = IJGVERS::fillInputBuffer;
	src.pub.skip_input_data   = IJGVERS::skipInputData;
	src.pub.resync_to_restart = jpeg_resync_to_restart;
	src.pub.term_source       = IJGVERS::termSource;
	src.pub.bytes_in_buffer   = 0;
	src.pub.next_input_byte   = NULL;
	src.skip_bytes            = 0;
	src.next_buffer           = (unsigned char*)(void*)jpegArray->Pointer;
	src.next_buffer_size      = (unsigned int*)jpegArray->ByteSize;

    IJGVERS::ErrorStruct jerr;
	memset(&jerr, 0, sizeof(IJGVERS::ErrorStruct));
	dinfo.err = jpeg_std_error(&jerr.pub);
	jerr.pub.error_exit = IJGVERS::ErrorExit;
	jerr.pub.output_message = IJGVERS::OutputMessage;

	jpeg_create_decompress(&dinfo);
	dinfo.src = (jpeg_source_mgr*)&src.pub;

	if (jpeg_read_header(&dinfo, TRUE) == JPEG_SUSPENDED) {
		jpeg_destroy_decompress(&dinfo);
		throw gcnew DicomCodecException("Unable to read JPEG header: Suspended");
	}

	jpeg_destroy_decompress(&dinfo);

	return dinfo.data_precision;
}
