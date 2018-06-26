// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
};

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
};

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
};
} // Codec
} // Imaging
} // Dicom

#endif