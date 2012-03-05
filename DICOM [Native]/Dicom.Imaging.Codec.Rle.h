#ifndef __DICOM_IMAGING_CODEC_RLE_H__
#define __DICOM_IMAGING_CODEC_RLE_H__

#pragma once

using namespace System;
using namespace System::ComponentModel::Composition;

using namespace Dicom;
using namespace Dicom::Imaging;

namespace Dicom {
namespace Imaging {
namespace Codec {
	[Export(IDicomCodec::typeid)]
	public ref class DicomRleNativeCodec : DicomRleCodec
	{
	public:
		virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;
		virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;
	};
} // Codec
} // Imaging
} // Dicom

#endif