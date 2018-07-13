// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#ifndef __DICOM_IMAGING_CODEC_PIXELDATA_H__
#define __DICOM_IMAGING_CODEC_PIXELDATA_H__

#pragma once

using namespace Platform;

namespace Dicom {
namespace Imaging {
namespace Codec {

	private class PlanarConfiguration {
	public:
		enum { Interleaved = 0, Planar = 1 };
	};

	private class PixelRepresentation {
	public:
		enum { Unsigned = 0, Signed = 1 };
	};

	private ref class PhotometricInterpretation sealed {
	public:
		static property String^ Monochrome1 { String^ get() { return "MONOCHROME1"; } }
		static property String^ Monochrome2 { String^ get() { return "MONOCHROME2"; } }
		static property String^ PaletteColor { String^ get() { return "PALETTE COLOR"; } }
		static property String^ Rgb { String^ get() { return "RGB"; } }
		static property String^ YbrFull { String^ get() { return "YBR_FULL"; } }
		static property String^ YbrFull422 { String^ get() { return "YBR_FULL_422"; } }
		static property String^ YbrPartial422 { String^ get() { return "YBR_PARTIAL_422"; } }
		static property String^ YbrPartial420 { String^ get() { return "YBR_PARTIAL_420"; } }
		static property String^ YbrIct { String^ get() { return "YBR_ICT"; } }
		static property String^ YbrRct { String^ get() { return "YBR_RCT"; } }
	};

	public delegate Array<unsigned char>^ GetFrameDelegate(int index);
	public delegate void AddFrameDelegate(const Array<unsigned char>^ buffer);
	public delegate void SetPlanarConfigurationDelegate(int value);
	public delegate void SetPhotometricInterpretationDelegate(String^ value);

	public ref class NativePixelData sealed
	{
	private:
		int _planarConfiguration;
		String^ _photometricInterpretation;

	public:
		property GetFrameDelegate^ GetFrameImpl;
		property AddFrameDelegate^ AddFrameImpl;
		property SetPlanarConfigurationDelegate^ SetPlanarConfigurationImpl;
		property SetPhotometricInterpretationDelegate^ SetPhotometricInterpretationImpl;

		property int NumberOfFrames;
		property int Width;
		property int Height;
		property int HighBit;
		property int BitsStored;
		property int BitsAllocated;
		property int BytesAllocated;
		property int SamplesPerPixel;
		property int UncompressedFrameSize;
		property int PixelRepresentation;
		property bool TransferSyntaxIsLossy;

		property int PlanarConfiguration
		{
			int get();
			void set(int value);
		}

		property String^ PhotometricInterpretation
		{
			String^ get();
			void set(String^ value);
		}

	internal:
		Array<unsigned char>^ GetFrame(int index);
		void AddFrame(const Array<unsigned char>^ buffer);

		static Array<unsigned char>^ InterleavedToPlanar24(Array<unsigned char>^ buffer);
		static Array<unsigned char>^ PlanarToInterleaved24(Array<unsigned char>^ buffer);
		static Array<unsigned char>^ UnpackLow16(Array<unsigned char>^ data);
	};

} // Codec
} // Imaging
} // Dicom

#endif