#ifndef __JPEGCODEC_H__
#define __JPEGCODEC_H__

#pragma once

#include "Dicom.Imaging.Codec.PixelData.h"
#include "Dicom.Imaging.Codec.JpegParameters.h"

using namespace Platform;
using namespace Dicom::Imaging::Codec;

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

public interface class IJpegNativeCodec {
	void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);
	void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);

	int ScanHeaderForPrecision(NativePixelData^ pixelData);

	property JpegMode Mode { JpegMode get(); };
	property int Predictor { int get(); };
	property int PointTransform { int get(); }
};
/*
public ref class Jpeg16Codec sealed : public IJpegNativeCodec {
public:
	Jpeg16Codec(JpegMode mode, int predictor, int point_transform);

	virtual void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);
	virtual void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);

	virtual int ScanHeaderForPrecision(NativePixelData^ pixelData);

	virtual property JpegMode Mode { JpegMode get() { return _mode; } };
	virtual property int Predictor { int get() { return _predictor; } };
	virtual property int PointTransform { int get() { return _pointTransform; } }

private:
	JpegMode _mode;
	int _predictor;
	int _pointTransform;

//	[ThreadStatic]
//	static Jpeg16Codec^ This;
};*/

public ref class Jpeg12Codec sealed : public IJpegNativeCodec {
public:
	Jpeg12Codec(JpegMode mode, int predictor, int point_transform);

	virtual void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);
	virtual void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);

	virtual int ScanHeaderForPrecision(NativePixelData^ pixelData);

	virtual property JpegMode Mode { JpegMode get() { return _mode; } };
	virtual property int Predictor { int get() { return _predictor; } };
	virtual property int PointTransform { int get() { return _pointTransform; } }

private:
	JpegMode _mode;
	int _predictor;
	int _pointTransform;

//	[ThreadStatic]
//	static Jpeg12Codec^ This;
};
/*
public ref class Jpeg8Codec sealed : public IJpegNativeCodec {
public:
	Jpeg8Codec(JpegMode mode, int predictor, int point_transform);

	virtual void Encode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);
	virtual void Decode(NativePixelData^ oldPixelData, NativePixelData^ newPixelData, NativeJpegParameters^ params, int frame);

	virtual int ScanHeaderForPrecision(NativePixelData^ pixelData);

	virtual property JpegMode Mode { JpegMode get() { return _mode; } };
	virtual property int Predictor { int get() { return _predictor; } };
	virtual property int PointTransform { int get() { return _pointTransform; } }

private:
	JpegMode _mode;
	int _predictor;
	int _pointTransform;

//	[ThreadStatic]
//	static Jpeg8Codec^ This;
};*/
} // Jpeg
/*
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
*/
} // Codec
} // Imaging
} // Dicom

#endif