// 
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
// 

#include "jpegstreamreader.h"
#include "util.h"
#include "jpegstreamwriter.h"
#include "jpegimagedatasegment.h"
#include "jpegmarkercode.h"
#include "decoderstrategy.h"
#include "encoderstrategy.h"
#include "jlscodecfactory.h"
#include "defaulttraits.h"
#include <memory>

using namespace charls;


// Default bin sizes for JPEG-LS statistical modeling. Can be overriden at compression time, however this is rarely done.
const int BASIC_T1 = 3;
const int BASIC_T2 = 7;
const int BASIC_T3 = 21;


// JFIF\0
uint8_t jfifID [] = { 'J', 'F', 'I', 'F', '\0' };


int32_t CLAMP(int32_t i, int32_t j, int32_t MAXVAL)
{
    if (i > MAXVAL || i < j)
        return j;

    return i;
}


JlsCustomParameters ComputeDefault(int32_t MAXVAL, int32_t NEAR)
{
    JlsCustomParameters preset = JlsCustomParameters();

    int32_t FACTOR = (MIN(MAXVAL, 4095) + 128) / 256;

    preset.T1 = CLAMP(FACTOR * (BASIC_T1 - 2) + 2 + 3*NEAR, NEAR + 1, MAXVAL);
    preset.T2 = CLAMP(FACTOR * (BASIC_T2 - 3) + 3 + 5*NEAR, preset.T1, MAXVAL);
    preset.T3 = CLAMP(FACTOR * (BASIC_T3 - 4) + 4 + 7*NEAR, preset.T2, MAXVAL);
    preset.MAXVAL = MAXVAL;
    preset.RESET = BASIC_RESET;
    return preset;
}


ApiResult CheckParameterCoherent(const JlsParameters& parameters)
{
    if (parameters.bitspersample < 2 || parameters.bitspersample > 16)
        return ApiResult::ParameterValueNotSupported;

    if (parameters.ilv < InterleaveMode::None || parameters.ilv > InterleaveMode::Sample)
        return ApiResult::InvalidCompressedData;

    switch (parameters.components)
    {
        case 4: return parameters.ilv == InterleaveMode::Sample ? ApiResult::ParameterValueNotSupported : ApiResult::OK;
        case 3: return ApiResult::OK;
        case 1: return parameters.ilv != InterleaveMode::None ? ApiResult::ParameterValueNotSupported : ApiResult::OK;
        case 0: return ApiResult::InvalidJlsParameters;

        default: return parameters.ilv != InterleaveMode::None ? ApiResult::ParameterValueNotSupported : ApiResult::OK;
    }
}


void JpegImageDataSegment::Serialize(JpegStreamWriter& streamWriter)
{
    JlsParameters info = _info;
    info.components = _ccompScan;
    auto codec = JlsCodecFactory<EncoderStrategy>().GetCodec(info, _info.custom);
    std::unique_ptr<ProcessLine> processLine(codec->CreateProcess(_rawStreamInfo));
    ByteStreamInfo compressedData = streamWriter.OutputStream();
    size_t cbyteWritten = codec->EncodeScan(std::move(processLine), compressedData, streamWriter._bCompare ? streamWriter.GetPos() : nullptr);
    streamWriter.Seek(cbyteWritten);
}


JpegStreamReader::JpegStreamReader(ByteStreamInfo byteStreamInfo) :
    _byteStream(byteStreamInfo),
    _bCompare(false),
    _info(),
    _rect()
{
}


void JpegStreamReader::Read(ByteStreamInfo rawPixels)
{
    ReadHeader();

    auto result = CheckParameterCoherent(_info);
    if (result != ApiResult::OK)
        throw std::system_error(static_cast<int>(result), CharLSCategoryInstance());

    if (_rect.Width <= 0)
    {
        _rect.Width = _info.width;
        _rect.Height = _info.height;
    }

    int64_t bytesPerPlane = static_cast<int64_t>(_rect.Width) * _rect.Height * ((_info.bitspersample + 7)/8);

    if (rawPixels.rawData && int64_t(rawPixels.count) < bytesPerPlane * _info.components)
        throw std::system_error(static_cast<int>(ApiResult::UncompressedBufferTooSmall), CharLSCategoryInstance());

    int componentIndex = 0;

    while (componentIndex < _info.components)
    {
        ReadStartOfScan(componentIndex == 0);

        std::unique_ptr<DecoderStrategy> qcodec = JlsCodecFactory<DecoderStrategy>().GetCodec(_info, _info.custom);
        std::unique_ptr<ProcessLine> processLine(qcodec->CreateProcess(rawPixels));
        qcodec->DecodeScan(std::move(processLine), _rect, &_byteStream, _bCompare); 
        SkipBytes(&rawPixels, static_cast<size_t>(bytesPerPlane));

        if (_info.ilv != InterleaveMode::None)
            return;

        componentIndex += 1;
    }
}


void JpegStreamReader::ReadNBytes(std::vector<char>& dst, int byteCount)
{
    for (int i = 0; i < byteCount; ++i)
    {
        dst.push_back(static_cast<char>(ReadByte()));
    }
}


void JpegStreamReader::ReadHeader()
{
    if (ReadByte() != 0xFF)
        throw std::system_error(static_cast<int>(ApiResult::MissingJpegMarkerStart), CharLSCategoryInstance());

    if (static_cast<JpegMarkerCode>(ReadByte()) != JpegMarkerCode::StartOfImage)
        throw std::system_error(static_cast<int>(ApiResult::InvalidCompressedData), CharLSCategoryInstance());

    for (;;)
    {
        if (ReadByte() != 0xFF)
            throw std::system_error(static_cast<int>(ApiResult::MissingJpegMarkerStart), CharLSCategoryInstance());

        JpegMarkerCode marker = static_cast<JpegMarkerCode>(ReadByte());
        if (marker == JpegMarkerCode::StartOfScan)
            return;

        int32_t cbyteMarker = ReadWord();

        int bytesRead = ReadMarker(marker) + 2;

        int paddingToRead = cbyteMarker - bytesRead;

        if (paddingToRead < 0)
            throw std::system_error(static_cast<int>(ApiResult::InvalidCompressedData), CharLSCategoryInstance());

        for (int i = 0; i < paddingToRead; ++i)
        {
            ReadByte();
        }
    }
}


int JpegStreamReader::ReadMarker(JpegMarkerCode marker)
{
    // ISO/IEC 14495-1, ITU-T Recommendation T.87, C.1.1. defines the following markers valid for a JPEG-LS byte stream:
    // SOF55, LSE, SOI, EOI, SOS, DNL, DRI, RSTm, APPn, COM.
    // All other markers shall not be present.
    switch (marker)
    {
        case JpegMarkerCode::StartOfFrameJpegLS:
            return ReadStartOfFrame();

        case JpegMarkerCode::Comment:
            return ReadComment();

        case JpegMarkerCode::JpegLSExtendedParameters:
            return ReadPresetParameters();

        case JpegMarkerCode::ApplicationData0:
            return 0;

        case JpegMarkerCode::ApplicationData7:
            return ReadColorSpace();

        case JpegMarkerCode::ApplicationData8:
            return ReadColorXForm();

        case JpegMarkerCode::StartOfFrameBaselineJpeg:
        case JpegMarkerCode::StartOfFrameExtendedSequential:
        case JpegMarkerCode::StartOfFrameProgressive:
        case JpegMarkerCode::StartOfFrameLossless:
        case JpegMarkerCode::StartOfFrameDifferentialSequential:
        case JpegMarkerCode::StartOfFrameDifferentialProgressive:
        case JpegMarkerCode::StartOfFrameDifferentialLossless:
        case JpegMarkerCode::StartOfFrameExtendedArithemtic:
        case JpegMarkerCode::StartOfFrameProgressiveArithemtic:
        case JpegMarkerCode::StartOfFrameLosslessArithemtic:
            {
                std::ostringstream message;
                message << "JPEG encoding with marker " << static_cast<unsigned int>(marker) << " is not supported.";
                throw CreateSystemError(ApiResult::UnsupportedEncoding, message.str());
            }

        // Other tags not supported (among which DNL DRI)
        default:
            {
                std::ostringstream message;
                message << "Unknown JPEG marker " << static_cast<unsigned int>(marker) << " encountered.";
                throw CreateSystemError(ApiResult::UnknownJpegMarker, message.str());
            }
    }
}


int JpegStreamReader::ReadPresetParameters()
{
    int type = ReadByte();

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


void JpegStreamReader::ReadStartOfScan(bool firstComponent)
{
    if (!firstComponent)
    {
        if (ReadByte() != 0xFF)
            throw std::system_error(static_cast<int>(ApiResult::MissingJpegMarkerStart), CharLSCategoryInstance());
        if (static_cast<JpegMarkerCode>(ReadByte()) != JpegMarkerCode::StartOfScan)
            throw std::system_error(static_cast<int>(ApiResult::InvalidCompressedData), CharLSCategoryInstance());// TODO: throw more specific error code.
    }
    int length = ReadByte();
    length = length * 256 + ReadByte(); // TODO: do something with 'length' or remove it.

    int componentCount = ReadByte();
    if (componentCount != 1 && componentCount != _info.components)
        throw std::system_error(static_cast<int>(ApiResult::ParameterValueNotSupported), CharLSCategoryInstance());

    for (int i = 0; i < componentCount; ++i)
    {
        ReadByte();
        ReadByte();
    }
    _info.allowedlossyerror = ReadByte();
    _info.ilv = static_cast<InterleaveMode>(ReadByte());
    if (!(_info.ilv == InterleaveMode::None || _info.ilv == InterleaveMode::Line || _info.ilv == InterleaveMode::Sample))
        throw std::system_error(static_cast<int>(ApiResult::InvalidCompressedData), CharLSCategoryInstance());// TODO: throw more specific error code.
    if (ReadByte() != 0)
        throw std::system_error(static_cast<int>(ApiResult::InvalidCompressedData), CharLSCategoryInstance());// TODO: throw more specific error code.

    if(_info.bytesperline == 0)
    {
        int width = _rect.Width != 0 ? _rect.Width : _info.width;
        int components = _info.ilv == InterleaveMode::None ? 1 : _info.components;
        _info.bytesperline = components * width * ((_info.bitspersample + 7)/8);
    }
}


int JpegStreamReader::ReadComment()
{
    return 0;
}


void JpegStreamReader::ReadJfif()
{
    for(int i = 0; i < static_cast<int>(sizeof(jfifID)); i++)
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
        std::vector<char> tempbuff(static_cast<char*>(_info.jfif.pdataThumbnail), 
            static_cast<char*>(_info.jfif.pdataThumbnail)+3*_info.jfif.Xthumb*_info.jfif.Ythumb);
        ReadNBytes(tempbuff, 3*_info.jfif.Xthumb*_info.jfif.Ythumb);
    }
}


int JpegStreamReader::ReadStartOfFrame()
{
    _info.bitspersample = ReadByte();
    int cline = ReadWord();
    int ccol = ReadWord();
    _info.width = ccol;
    _info.height = cline;
    _info.components= ReadByte();
    return 6;
}


uint8_t JpegStreamReader::ReadByte()
{
    if (_byteStream.rawStream)
        return static_cast<uint8_t>(_byteStream.rawStream->sbumpc());

    if (_byteStream.count == 0)
        throw std::system_error(static_cast<int>(ApiResult::CompressedBufferTooSmall), CharLSCategoryInstance());

    uint8_t value = _byteStream.rawData[0];
    SkipBytes(&_byteStream, 1);
    return value;
}


int JpegStreamReader::ReadWord()
{
    int i = ReadByte() * 256;
    return i + ReadByte();
}


int JpegStreamReader::ReadColorSpace()
{
    return 0;
}


int JpegStreamReader::ReadColorXForm()
{
    std::vector<char> sourceTag;
    ReadNBytes(sourceTag, 4);

    if (strncmp(&sourceTag[0], "mrfx", 4) != 0)
        return 4;

    auto xform = static_cast<ColorTransformation>(ReadByte());
    switch (xform)
    {
        case ColorTransformation::None:
        case ColorTransformation::HP1:
        case ColorTransformation::HP2:
        case ColorTransformation::HP3:
            _info.colorTransform = xform;
            return 5;
        case ColorTransformation::RgbAsYuvLossy:
        case ColorTransformation::Matrix:
            throw std::system_error(static_cast<int>(ApiResult::ImageTypeNotSupported), CharLSCategoryInstance());
        default:
            throw std::system_error(static_cast<int>(ApiResult::InvalidCompressedData), CharLSCategoryInstance());
    }
}
