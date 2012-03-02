#ifndef __DICOM_IMAGING_CODEC_JPEG2000_H__
#define __DICOM_IMAGING_CODEC_JPEG2000_H__

#pragma once

using namespace System;
using namespace System::ComponentModel::Composition;

using namespace Dicom;
using namespace Dicom::Imaging;
using namespace Dicom::Imaging::Codec;

namespace Dicom {
namespace Imaging {
namespace Codec {

	public ref class DicomJpeg2000NativeCodec abstract : public DicomJpeg2000Codec
	{
	public:
		virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;
		virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;
	};

	[Export(IDicomCodec::typeid)]
	public ref class DicomJpeg2000LosslessCodec : public DicomJpeg2000NativeCodec
	{
	public:
		virtual property DicomTransferSyntax^ TransferSyntax {
			DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEG2000Lossless; }
		}
	};

	[Export(IDicomCodec::typeid)]
	public ref class DicomJpeg2000LossyCodec : public DicomJpeg2000NativeCodec
	{
	public:
		virtual property DicomTransferSyntax^ TransferSyntax {
			DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEG2000Lossy; }
		}
	};

} // Jpeg2000
} // Codec
} // Dicom

#endif
