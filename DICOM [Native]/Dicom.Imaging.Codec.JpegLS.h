#ifndef __DICOM_IMAGING_CODEC_JPEGLS_H__
#define __DICOM_IMAGING_CODEC_JPEGLS_H__

#pragma once

using namespace System;
using namespace System::ComponentModel::Composition;

using namespace Dicom;
using namespace Dicom::Imaging;

namespace Dicom {
namespace Imaging {
namespace Codec {
	public ref class DicomJpegLsNativeCodec abstract : public DicomJpegLsCodec
	{
	public:
		virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;
		virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;
	};

	[Export(IDicomCodec::typeid)]
	public ref class DicomJpegLsLosslessCodec : public DicomJpegLsNativeCodec
	{
	public:
		virtual property DicomTransferSyntax^ TransferSyntax {
			DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEGLSLossless; }
		}
	};

	[Export(IDicomCodec::typeid)]
	public ref class DicomJpegLsNearLosslessCodec : public DicomJpegLsNativeCodec
	{
	public:
		virtual property DicomTransferSyntax^ TransferSyntax {
			DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEGLSNearLossless; }
		}
	};
} // Codec
} // Imaging
} // Dicom

#endif