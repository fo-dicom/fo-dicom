// 
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
// 

#include "config.h"
#include "util.h"
#include "header.h"
#include "jpegmarker.h"
#include "decoderstrategy.h"
#include "encoderstrategy.h"
#include <memory>


// JFIF\0
BYTE jfifID[] = {'J','F','I','F','\0'};


bool IsDefault(const JlsCustomParameters* pcustom)
{
	if (pcustom->MAXVAL != 0)
		return false;

	if (pcustom->T1 != 0)
		return false;

	if (pcustom->T2 != 0)
		return false;

	if (pcustom->T3 != 0)
		return false;

	if (pcustom->RESET != 0)
		return false;

	return true;
}


LONG CLAMP(LONG i, LONG j, LONG MAXVAL)
{
	if (i > MAXVAL || i < j)
		return j;

	return i;
}


JlsCustomParameters ComputeDefault(LONG MAXVAL, LONG NEAR)
{
	JlsCustomParameters preset = JlsCustomParameters();

	LONG FACTOR = (MIN(MAXVAL, 4095) + 128)/256;

	preset.T1 = CLAMP(FACTOR * (BASIC_T1 - 2) + 2 + 3*NEAR, NEAR + 1, MAXVAL);
	preset.T2 = CLAMP(FACTOR * (BASIC_T2 - 3) + 3 + 5*NEAR, preset.T1, MAXVAL);
	preset.T3 = CLAMP(FACTOR * (BASIC_T3 - 4) + 4 + 7*NEAR, preset.T2, MAXVAL);
	preset.MAXVAL = MAXVAL;
	preset.RESET = BASIC_RESET;
	return preset;
}


JLS_ERROR CheckParameterCoherent(const JlsParameters* pparams)
{
	if (pparams->bitspersample < 2 || pparams->bitspersample > 16)
		return ParameterValueNotSupported;

	if (pparams->ilv < 0 || pparams->ilv > 2)
		return InvalidCompressedData;

	switch (pparams->components)
	{
		case 4: return pparams->ilv == ILV_SAMPLE ? ParameterValueNotSupported : OK; 
		case 3: return OK;
		case 1: return pparams->ilv != ILV_NONE ? ParameterValueNotSupported : OK;
		case 0: return InvalidJlsParameters;

		default: return pparams->ilv != ILV_NONE ? ParameterValueNotSupported : OK; 
	}
}

//
// JpegMarkerSegment
//
class JpegMarkerSegment : public JpegSegment
{
public:
	JpegMarkerSegment(BYTE marker, std::vector<BYTE> vecbyte)
	{
		_marker = marker;
		std::swap(_vecbyte, vecbyte);
	}

	virtual void Write(JpegMarkerWriter* pstream)
	{
		pstream->WriteByte(0xFF);
		pstream->WriteByte(_marker);
		pstream->WriteWord(USHORT(_vecbyte.size() + 2));
		pstream->WriteBytes(_vecbyte);
	}

	BYTE _marker;
	std::vector<BYTE> _vecbyte;
};


//
// push_back()
//
void push_back(std::vector<BYTE>& vec, USHORT value)
{
	vec.push_back(BYTE(value / 0x100));
	vec.push_back(BYTE(value % 0x100));
}


//
// CreateMarkerStartOfFrame()
//
JpegSegment* CreateMarkerStartOfFrame(Size size, LONG bitsPerSample, LONG ccomp)
{
	std::vector<BYTE> vec;
	vec.push_back(static_cast<BYTE>(bitsPerSample));
	push_back(vec, static_cast<USHORT>(size.cy));
	push_back(vec, static_cast<USHORT>(size.cx));

	// components
	vec.push_back(static_cast<BYTE>(ccomp));
	for (BYTE component = 0; component < ccomp; component++)
	{
		// rescaling
		vec.push_back(component + 1);
		vec.push_back(0x11); 
		//"Tq1" reserved, 0
		vec.push_back(0);
	}

	return new JpegMarkerSegment(JPEG_SOF_55, vec);
}


//
// CreateJFIF()
//
JpegMarkerSegment* CreateJFIF(const JfifParameters* jfif)
{
	std::vector<BYTE> rgbyte;
	for(int i = 0; i < (int)sizeof(jfifID); i++)
	{
		rgbyte.push_back(jfifID[i]);
	}

	push_back(rgbyte, (USHORT)jfif->Ver);

	rgbyte.push_back(jfif->units);
	push_back(rgbyte, (USHORT)jfif->XDensity);
	push_back(rgbyte, (USHORT)jfif->YDensity);

	// thumbnail
	rgbyte.push_back((BYTE)jfif->Xthumb);
	rgbyte.push_back((BYTE)jfif->Ythumb);
	if(jfif->Xthumb > 0) 
	{
		if(jfif->pdataThumbnail)
			throw JlsException(InvalidJlsParameters);

		rgbyte.insert(rgbyte.end(), (BYTE*)jfif->pdataThumbnail, (BYTE*)jfif->pdataThumbnail+3*jfif->Xthumb*jfif->Ythumb
		);
	}

	return new JpegMarkerSegment(JPEG_APP0, rgbyte);
}


//
// ctor()
//
JpegMarkerWriter::JpegMarkerWriter(const JfifParameters& jfifParameters, Size size, LONG bitsPerSample, LONG ccomp) :
	_bCompare(false),
	_byteOffset(0),
	_lastCompenentIndex(0),
	_data()
{
	if (jfifParameters.Ver)
	{
		_segments.push_back(CreateJFIF(&jfifParameters));
	}

	_segments.push_back(CreateMarkerStartOfFrame(size, bitsPerSample, ccomp));
}


//
// dtor()
//
JpegMarkerWriter::~JpegMarkerWriter()
{
	for (size_t i = 0; i < _segments.size(); ++i)
	{
		delete _segments[i];
	}
	_segments.empty();
}


void JpegMarkerWriter::AddColorTransform(int i)
{
	std::vector<BYTE> rgbyteXform;
	rgbyteXform.push_back('m');
	rgbyteXform.push_back('r');
	rgbyteXform.push_back('f');
	rgbyteXform.push_back('x');
	rgbyteXform.push_back((BYTE)i);

	_segments.push_back(new JpegMarkerSegment(JPEG_APP8, rgbyteXform));
}


//
// Write()
//
size_t JpegMarkerWriter::Write(ByteStreamInfo info)
{
	_data = info;

	WriteByte(0xFF);
	WriteByte(JPEG_SOI);

	for (size_t i = 0; i < _segments.size(); ++i)
	{
		_segments[i]->Write(this);
	}

	//_bCompare = false;

	WriteByte(0xFF);
	WriteByte(JPEG_EOI);

	return _byteOffset;
}



JpegMarkerReader::JpegMarkerReader(ByteStreamInfo byteStreamInfo) :
		_byteStream(byteStreamInfo),
		_bCompare(false),
		_info(),
		_rect()
{
}


int ReadScanHeader(BYTE* compressedBytes)
{
	BYTE rgbyte[20];
	size_t readBytes = 0;
	::memcpy(rgbyte, compressedBytes, 4);
	readBytes += 4;

	size_t cbyteScanheader = rgbyte[3] - 2;

	if (cbyteScanheader > sizeof(rgbyte))
		throw JlsException(InvalidCompressedData);

	::memcpy(rgbyte, compressedBytes, cbyteScanheader);
	readBytes += cbyteScanheader;
	return (int)readBytes;
}

//
// Read()
//
void JpegMarkerReader::Read(ByteStreamInfo rawPixels)
{
	ReadHeader();

	JLS_ERROR error = CheckParameterCoherent(&_info);
	if (error != OK)
		throw JlsException(error);

	if (_rect.Width <= 0)
	{
		_rect.Width = _info.width;
		_rect.Height = _info.height;
	}

	int64_t bytesPerPlane = (int64_t)(_rect.Width) * _rect.Height * ((_info.bitspersample + 7)/8);

	if (rawPixels.rawData != NULL && int64_t(rawPixels.count) < bytesPerPlane * _info.components)
		throw JlsException(UncompressedBufferTooSmall);

	int componentIndex = 0;
	
	while (componentIndex < _info.components)
	{
		ReadStartOfScan(componentIndex == 0);

		std::auto_ptr<DecoderStrategy> qcodec = JlsCodecFactory<DecoderStrategy>().GetCodec(_info, _info.custom);	
		ProcessLine* processLine = qcodec->CreateProcess(rawPixels);
		qcodec->DecodeScan(std::auto_ptr<ProcessLine>(processLine), _rect, &_byteStream, _bCompare); 
		SkipBytes(&rawPixels, (size_t)bytesPerPlane);		

		if (_info.ilv != ILV_NONE)
			return;

		componentIndex += 1;
	}
}

// ReadNBytes()
//
void JpegMarkerReader::ReadNBytes(std::vector<char>& dst, int byteCount)
{
	for (int i = 0; i < byteCount; ++i)
	{
		dst.push_back((char)ReadByte());
	}
}


//
// ReadHeader()
//
void JpegMarkerReader::ReadHeader()
{
	if (ReadByte() != 0xFF)
		throw JlsException(InvalidCompressedData);

	if (ReadByte() != JPEG_SOI)
		throw JlsException(InvalidCompressedData);

	for (;;)
	{
		if (ReadByte() != 0xFF)
			throw JlsException(InvalidCompressedData);

		BYTE marker = (BYTE)ReadByte();

		if (marker == JPEG_SOS)
			return;

		LONG cbyteMarker = ReadWord();

		int bytesRead = ReadMarker(marker) + 2;

		int paddingToRead = cbyteMarker - bytesRead;

		if (paddingToRead < 0)
			throw JlsException(InvalidCompressedData);

		for (int i = 0; i < paddingToRead; ++i)
		{
			ReadByte();
		}
	}
}

int JpegMarkerReader::ReadMarker(BYTE marker)
{
		switch (marker)
		{
			case JPEG_SOF_55: return ReadStartOfFrame();
			case JPEG_COM: return ReadComment();
			case JPEG_LSE: return ReadPresetParameters();
			case JPEG_APP0: return 0;
			case JPEG_APP7: return ReadColorSpace();
			case JPEG_APP8: return ReadColorXForm();
			// Other tags not supported (among which DNL DRI)
			default: throw JlsException(ImageTypeNotSupported);
		}
}


JpegMarkerSegment* EncodeStartOfScan(const JlsParameters* pparams, LONG icomponent)
{
	BYTE itable = 0;

	std::vector<BYTE> rgbyte;

	if (icomponent < 0)
	{
		rgbyte.push_back((BYTE)pparams->components);
		for (LONG i = 0; i < pparams->components; ++i)
		{
			rgbyte.push_back(BYTE(i + 1));
			rgbyte.push_back(itable);
		}
	}
	else
	{
		rgbyte.push_back(1);
		rgbyte.push_back((BYTE)icomponent);
		rgbyte.push_back(itable);
	}

	rgbyte.push_back(BYTE(pparams->allowedlossyerror));
	rgbyte.push_back(BYTE(pparams->ilv));
	rgbyte.push_back(0); // transform

	return new JpegMarkerSegment(JPEG_SOS, rgbyte);
}



JpegMarkerSegment* CreateLSE(const JlsCustomParameters* pcustom)
{
	std::vector<BYTE> rgbyte;

	rgbyte.push_back(1);
	push_back(rgbyte, (USHORT)pcustom->MAXVAL);
	push_back(rgbyte, (USHORT)pcustom->T1);
	push_back(rgbyte, (USHORT)pcustom->T2);
	push_back(rgbyte, (USHORT)pcustom->T3);
	push_back(rgbyte, (USHORT)pcustom->RESET);
	
	return new JpegMarkerSegment(JPEG_LSE, rgbyte);
}

//
// ReadPresetParameters()
//
int JpegMarkerReader::ReadPresetParameters()
{
	LONG type = ReadByte();

	switch (type)
	{
	case 1:
		{
			_info.custom.MAXVAL = ReadWord();
			_info.custom.T1 = ReadWord();
			_info.custom.T2 = ReadWord();
			_info.custom.T3 = ReadWord();
			_info.custom.RESET = ReadWord();
			return 11;
		}
	}

	return 1;
}

void Assert(bool valid)
{
	if (!valid)
		throw JlsException(InvalidCompressedData);
}


//
// ReadStartOfScan()
//
void JpegMarkerReader::ReadStartOfScan(bool firstComponent)
{
	if (!firstComponent)
	{
		Assert(ReadByte() == 0xFF);
		Assert(ReadByte() == JPEG_SOS);
	}
	int length = ReadByte(); //length
	length = length * 256 + ReadByte();
	
	LONG componentCount = ReadByte();
	if (componentCount != 1 && componentCount != _info.components)
		throw JlsException(ParameterValueNotSupported);

	for (LONG i = 0; i < componentCount; ++i)
	{
		ReadByte();
		ReadByte();
	}
	_info.allowedlossyerror = ReadByte();
	_info.ilv = interleavemode(ReadByte());
	Assert(ILV_NONE <= _info.ilv && _info.ilv <= ILV_SAMPLE);
	Assert(ReadByte() == 0);

	if(_info.bytesperline == 0)
	{
		int width = _rect.Width != 0 ? _rect.Width : _info.width;
		int components = _info.ilv == ILV_NONE ? 1 : _info.components;
		_info.bytesperline = components * width * ((_info.bitspersample + 7)/8);
	}
}


//
// ReadComment()
//
int JpegMarkerReader::ReadComment()
{
	return 0;
}


//
// ReadJfif()
//
void JpegMarkerReader::ReadJfif()
{
	for(int i = 0; i < (int)sizeof(jfifID); i++)
	{
		if(jfifID[i] != ReadByte())
			return;
	}
	_info.jfif.Ver   = ReadWord();

	// DPI or DPcm
	_info.jfif.units = ReadByte();
	_info.jfif.XDensity = ReadWord();
	_info.jfif.YDensity = ReadWord();

	// thumbnail
	_info.jfif.Xthumb = ReadByte();
	_info.jfif.Ythumb = ReadByte();
	if(_info.jfif.Xthumb > 0 && _info.jfif.pdataThumbnail) 
	{
		std::vector<char> tempbuff((char*)_info.jfif.pdataThumbnail, (char*)_info.jfif.pdataThumbnail+3*_info.jfif.Xthumb*_info.jfif.Ythumb);
		ReadNBytes(tempbuff, 3*_info.jfif.Xthumb*_info.jfif.Ythumb);
	}
}


//
// ReadStartOfFrame()
//
int JpegMarkerReader::ReadStartOfFrame()
{
	_info.bitspersample = ReadByte();
	int cline = ReadWord();
	int ccol = ReadWord();
	_info.width = ccol;
	_info.height = cline;
	_info.components= ReadByte();
	return 6;
}


//
// ReadByte()
//
BYTE JpegMarkerReader::ReadByte()
{
	if (_byteStream.rawStream != NULL)
		return (BYTE)_byteStream.rawStream->sbumpc();

	if (_byteStream.count <= 0)
		throw JlsException(InvalidCompressedData);

	BYTE value = _byteStream.rawData[0]; 
	
	SkipBytes(&_byteStream, 1);

	return value;
}


//
// ReadWord()
//
int JpegMarkerReader::ReadWord()
{
	int i = ReadByte() * 256;
	return i + ReadByte();
}



class JpegImageDataSegment: public JpegSegment
{
public:

	JpegImageDataSegment(ByteStreamInfo rawStream, const JlsParameters& info, LONG icompStart, int ccompScan)  :
		_ccompScan(ccompScan),
		_icompStart(icompStart),
		_rawStreamInfo(rawStream),
		_info(info)
	{
	}

	void Write(JpegMarkerWriter* pstream)
	{
		JlsParameters info = _info;
		info.components = _ccompScan;	
		std::auto_ptr<EncoderStrategy> qcodec =JlsCodecFactory<EncoderStrategy>().GetCodec(info, _info.custom);
		ProcessLine* processLine = qcodec->CreateProcess(_rawStreamInfo);
		ByteStreamInfo compressedData = pstream->OutputStream();
		size_t cbyteWritten = qcodec->EncodeScan(std::auto_ptr<ProcessLine>(processLine), &compressedData, pstream->_bCompare ? pstream->GetPos() : NULL); 
		pstream->Seek(cbyteWritten);
	}


	int _ccompScan;
	LONG _icompStart;
	ByteStreamInfo _rawStreamInfo;
	JlsParameters _info;	
};
 

void JpegMarkerWriter::AddScan(ByteStreamInfo info, const JlsParameters* pparams)
{
	if (!IsDefault(&pparams->custom))
	{
		_segments.push_back(CreateLSE(&pparams->custom));
	}
	else if (pparams->bitspersample > 12)
	{
		JlsCustomParameters preset = ComputeDefault((1 << pparams->bitspersample) - 1, pparams->allowedlossyerror);
		_segments.push_back(CreateLSE(&preset));
	}

	_lastCompenentIndex += 1;
	_segments.push_back(EncodeStartOfScan(pparams,pparams->ilv == ILV_NONE ? _lastCompenentIndex : -1));

	int ccomp = pparams->ilv == ILV_NONE ? 1 : pparams->components;

	_segments.push_back(new JpegImageDataSegment(info, *pparams, _lastCompenentIndex, ccomp));
}


//
// ReadColorSpace()
//
int JpegMarkerReader::ReadColorSpace()
{
	return 0;
}


//
// ReadColorXForm()
//
int JpegMarkerReader::ReadColorXForm()
{
	std::vector<char> sourceTag;
	ReadNBytes(sourceTag, 4);

	if(strncmp(&sourceTag[0],"mrfx", 4) != 0)
		return 4;

	int xform = ReadByte();
	switch(xform) 
	{
		case COLORXFORM_NONE:
		case COLORXFORM_HP1:
		case COLORXFORM_HP2:
		case COLORXFORM_HP3:
			_info.colorTransform = xform;
			return 5;
		case COLORXFORM_RGB_AS_YUV_LOSSY:
		case COLORXFORM_MATRIX:
			throw JlsException(ImageTypeNotSupported);
		default:
			throw JlsException(InvalidCompressedData);
	}
}


ByteStreamInfo FromStream(std::basic_streambuf<char>* stream)
{
	ByteStreamInfo info = ByteStreamInfo();
	info.rawStream = stream;	
	return info;
}


void SkipBytes(ByteStreamInfo* streamInfo, size_t count)
{
	if (streamInfo->rawData == NULL)
		return;

	streamInfo->rawData += count;
	streamInfo->count -= count;
}
