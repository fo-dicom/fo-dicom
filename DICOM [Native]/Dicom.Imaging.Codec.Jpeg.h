#ifndef __JPEGCODEC_H__
#define __JPEGCODEC_H__

#pragma once

using namespace System;
using namespace System::ComponentModel::Composition;
using namespace System::IO;
using namespace System::Threading;

using namespace Dicom;
using namespace Dicom::Imaging;
using namespace Dicom::Imaging::Codec;
using namespace Dicom::IO;

namespace Dicom {
namespace Imaging {
namespace Codec {
namespace Jpeg {

public enum class JpegMode : int {
	Baseline,
	Sequential,
	SpectralSelection,
	Progressive,
	Lossless
};

public ref class JpegNativeCodec abstract {
public:
	virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) abstract;
	virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) abstract;

internal:
	virtual int ScanHeaderForPrecision(DicomPixelData^ pixelData) abstract;

	MemoryStream^ MemoryBuffer;
	PinnedByteArray^ DataArray;

	JpegMode Mode;
	int Predictor;
	int PointTransform;
};

public ref class Jpeg16Codec : public JpegNativeCodec {
public:
	Jpeg16Codec(JpegMode mode, int predictor, int point_transform);
	virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) override;
	virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) override;

internal:
	virtual int ScanHeaderForPrecision(DicomPixelData^ pixelData) override;

	[ThreadStatic]
	static Jpeg16Codec^ This;
};

public ref class Jpeg12Codec : public JpegNativeCodec {
public:
	Jpeg12Codec(JpegMode mode, int predictor, int point_transform);
	virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) override;
	virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) override;

internal:
	virtual int ScanHeaderForPrecision(DicomPixelData^ pixelData) override;

	[ThreadStatic]
	static Jpeg12Codec^ This;
};

public ref class Jpeg8Codec : public JpegNativeCodec {
public:
	Jpeg8Codec(JpegMode mode, int predictor, int point_transform);
	virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) override;
	virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomJpegParams^ params, int frame) override;

internal:
	virtual int ScanHeaderForPrecision(DicomPixelData^ pixelData) override;

	[ThreadStatic]
	static Jpeg8Codec^ This;
};
} // Jpeg

using namespace Dicom::Imaging::Codec::Jpeg;

public ref class DicomJpegNativeCodec abstract : public DicomJpegCodec {
public:
	virtual void Encode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;
	virtual void Decode(DicomPixelData^ oldPixelData, DicomPixelData^ newPixelData, DicomCodecParams^ parameters) override;

protected:
	virtual JpegNativeCodec^ GetCodec(int bits, DicomJpegParams^ jparams) = 0;
};

[Export(IDicomCodec::typeid)]
public ref class DicomJpegProcess1Codec : public DicomJpegNativeCodec {
public:
	virtual property DicomTransferSyntax^ TransferSyntax {
		DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEGProcess1; }
	}

protected:
	virtual JpegNativeCodec^ GetCodec(int bits, DicomJpegParams^ jparams) override {
		if (bits == 8)
			return gcnew Jpeg8Codec(JpegMode::Baseline, 0, 0);
		else
			throw gcnew DicomCodecException(String::Format("Unable to create JPEG Process 1 codec for bits stored == {0}", bits));
	}
};

[Export(IDicomCodec::typeid)]
public ref class DicomJpegProcess4Codec : public DicomJpegNativeCodec {
public:
	virtual property DicomTransferSyntax^ TransferSyntax {
		DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEGProcess2_4; }
	}

protected:
	virtual JpegNativeCodec^ GetCodec(int bits, DicomJpegParams^ jparams) override {
		if (bits == 8)
			return gcnew Jpeg8Codec(JpegMode::Sequential, 0, 0);
		else if (bits <= 12)
			return gcnew Jpeg12Codec(JpegMode::Sequential, 0, 0);
		else
			throw gcnew DicomCodecException(String::Format("Unable to create JPEG Process 4 codec for bits stored == {0}", bits));
	}
};

[Export(IDicomCodec::typeid)]
public ref class DicomJpegLossless14Codec : public DicomJpegNativeCodec {
public:
	virtual property DicomTransferSyntax^ TransferSyntax {
		DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEGProcess14; }
	}

protected:
	virtual JpegNativeCodec^ GetCodec(int bits, DicomJpegParams^ jparams) override {
		if (bits <= 8)
			return gcnew Jpeg8Codec(JpegMode::Lossless, jparams->Predictor, jparams->PointTransform);
		else if (bits <= 12)
			return gcnew Jpeg12Codec(JpegMode::Lossless, jparams->Predictor, jparams->PointTransform);
		else if (bits <= 16)
			return gcnew Jpeg16Codec(JpegMode::Lossless, jparams->Predictor, jparams->PointTransform);
		else
			throw gcnew DicomCodecException(String::Format("Unable to create JPEG Process 14 codec for bits stored == {0}", bits));
	}
};

[Export(IDicomCodec::typeid)]
public ref class DicomJpegLossless14SV1Codec : public DicomJpegNativeCodec {
public:
	virtual property DicomTransferSyntax^ TransferSyntax {
		DicomTransferSyntax^ get() override { return DicomTransferSyntax::JPEGProcess14SV1; }
	}

protected:
	virtual JpegNativeCodec^ GetCodec(int bits, DicomJpegParams^ jparams) override {
		if (bits <= 8)
			return gcnew Jpeg8Codec(JpegMode::Lossless, 1, jparams->PointTransform);
		else if (bits <= 12)
			return gcnew Jpeg12Codec(JpegMode::Lossless, 1, jparams->PointTransform);
		else if (bits <= 16)
			return gcnew Jpeg16Codec(JpegMode::Lossless, 1, jparams->PointTransform);
		else
			throw gcnew DicomCodecException(String::Format("Unable to create JPEG Process 14 [SV1] codec for bits stored == {0}", bits));
	}
};

} // Codec
} // Imaging
} // Dicom

#endif