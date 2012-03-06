/* 
  (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
*/ 
#ifndef CHARLS_PUBLICTYPES
#define CHARLS_PUBLICTYPES

#include "config.h"

#ifdef __cplusplus
#include <iostream>
#endif

enum JLS_ERROR
{
	OK = 0,
	InvalidJlsParameters,
	ParameterValueNotSupported,
	UncompressedBufferTooSmall,
	CompressedBufferTooSmall,
	InvalidCompressedData,
	TooMuchCompressedData,
	ImageTypeNotSupported,
	UnsupportedBitDepthForTransform,
	UnsupportedColorTransform
};


enum interleavemode
{
	ILV_NONE = 0,
	ILV_LINE = 1,
	ILV_SAMPLE = 2
};



struct JlsCustomParameters
{
	int MAXVAL;
	int T1;
	int T2;
	int T3;
	int RESET;
};


struct JlsRect
{
	int X, Y;
	int Width, Height;
};


struct JfifParameters
{
	int   Ver;
	char  units;
	int   XDensity;
	int   YDensity;
	short Xthumb;
	short Ythumb;
	void* pdataThumbnail; /* user must set buffer which size is Xthumb*Ythumb*3(RGB) before JpegLsDecode() */
};


struct JlsParameters
{
	int width;
	int height;
	int bitspersample;
	int bytesperline;	/* for [source (at encoding)][decoded (at decoding)] pixel image in user buffer */
	int components;
	int allowedlossyerror;
	enum interleavemode ilv;
	int colorTransform;
	char outputBgr;
	struct JlsCustomParameters custom;
	struct JfifParameters jfif;
};


#ifdef __cplusplus

// 
// ByteStreamInfo & FromByteArray helper function
//
// ByteStreamInfo describes the stream: either set rawStream to a valid stream, or rawData/count, not both.
// it's possible to decode to memorystreams, but using rawData will always be faster.
//
// Example use: 
//     ByteStreamInfo streamInfo = { fileStream.rdbuf() };
// or 
//     ByteStreamInfo streamInfo = FromByteArray( bytePtr, byteCount);
//
struct ByteStreamInfo
{
	std::basic_streambuf<char>* rawStream;
	BYTE* rawData;
	size_t count;
};


inline ByteStreamInfo FromByteArray(const void* bytes, size_t count)
{
	ByteStreamInfo info = ByteStreamInfo();
	info.rawData = (BYTE*)bytes;
	info.count = count;
	return info;
}

#endif

#endif
