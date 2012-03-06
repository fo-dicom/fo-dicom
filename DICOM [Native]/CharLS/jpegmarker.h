// 
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
// 
#ifndef CHARLS_JPEGMARKER
#define CHARLS_JPEGMARKER

#include <memory>
#include <vector>
#include "util.h"


// This file defines JPEG-LS markers: The header and the actual pixel data. Header markers have fixed length, the pixeldata not.


class JpegSegment;

enum JPEGLS_ColorXForm
{
	// default (RGB)
	COLORXFORM_NONE = 0,

	// Color transforms as defined by HP
	COLORXFORM_HP1,
	COLORXFORM_HP2,
	COLORXFORM_HP3,

	// Defined by HP but not supported by CharLS
	COLORXFORM_RGB_AS_YUV_LOSSY,
	COLORXFORM_MATRIX,

	XFORM_BIGENDIAN = 1 << 29,
	XFORM_LITTLEENDIAN = 1 << 30,
};


ByteStreamInfo FromByteArray(const void* bytes, size_t count);
ByteStreamInfo FromStream(std::basic_streambuf<char>* stream);
void SkipBytes(ByteStreamInfo* streamInfo, size_t count);

//
// JpegMarkerWriter: minimal implementation to write JPEG markers
//
class JpegMarkerWriter
{
	friend class JpegMarkerSegment;
	friend class JpegImageDataSegment;

public:
	JpegMarkerWriter(const JfifParameters& jfifParameters, Size size, LONG bitsPerSample, LONG ccomp);
	virtual ~JpegMarkerWriter();

	void AddScan(ByteStreamInfo info, const JlsParameters* pparams);

	void AddLSE(const JlsCustomParameters* pcustom);
	void AddColorTransform(int i);
	size_t GetBytesWritten()
		{ return _byteOffset; }

	size_t GetLength()
		{ return _data.count - _byteOffset; }

	size_t Write(ByteStreamInfo info);

	void EnableCompare(bool bCompare) 
		{ _bCompare = bCompare; }

private:
	BYTE* GetPos() const
		{ return _data.rawData + _byteOffset; }

	ByteStreamInfo OutputStream() const
	{ 
		ByteStreamInfo data = _data;
		data.count -= _byteOffset;
		data.rawData += _byteOffset;
		return data; 
	}

	void WriteByte(BYTE val)
	{ 
		ASSERT(!_bCompare || _data.rawData[_byteOffset] == val);
		
		if (_data.rawStream != NULL)
		{
			_data.rawStream->sputc(val);
		}
		else
		{
			if (_byteOffset >= _data.count)
				throw JlsException(CompressedBufferTooSmall);

			_data.rawData[_byteOffset++] = val; 
		}
	}

	void WriteBytes(const std::vector<BYTE>& rgbyte)
	{
		for (size_t i = 0; i < rgbyte.size(); ++i)
		{
			WriteByte(rgbyte[i]);
		}
	}

	void WriteWord(USHORT val)
	{
		WriteByte(BYTE(val / 0x100));
		WriteByte(BYTE(val % 0x100));
	}

	void Seek(size_t byteCount)
	{
		if (_data.rawStream != NULL)
			return;

		_byteOffset += byteCount;
	}

private:
	bool _bCompare;
	ByteStreamInfo _data;
	size_t _byteOffset;
	LONG _lastCompenentIndex;
	std::vector<JpegSegment*> _segments;
};


//
// JpegMarkerReader: minimal implementation to read JPEG markers
//
class JpegMarkerReader
{
public:
	JpegMarkerReader(ByteStreamInfo byteStreamInfo);

	const JlsParameters& GetMetadata() const
		{ return _info; } 

	const JlsCustomParameters& GetCustomPreset() const
	{ return _info.custom; } 

	void Read(ByteStreamInfo info);
	void ReadHeader();

	void EnableCompare(bool bCompare)
		{ _bCompare = bCompare;	}

	void SetInfo(JlsParameters* info) { _info = *info; }

	void SetRect(JlsRect rect) { _rect = rect; }

	void ReadStartOfScan(bool firstComponent);
	BYTE ReadByte();

private:
	void ReadScan(ByteStreamInfo rawPixels);	
	int ReadPresetParameters();
	int ReadComment();
	int ReadStartOfFrame();
	int ReadWord();
	void ReadNBytes(std::vector<char>& dst, int byteCount);
	int ReadMarker(BYTE marker);

	// JFIF
	void ReadJfif();
	// Color Transform Application Markers & Code Stream (HP extension)
	int ReadColorSpace();
	int ReadColorXForm();

private:
	ByteStreamInfo _byteStream;
	bool _bCompare;
	JlsParameters _info;
	JlsRect _rect;
};


#endif
