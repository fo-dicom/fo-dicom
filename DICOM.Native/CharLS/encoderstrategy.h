// 
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
// 

#ifndef CHARLS_ENCODERSTRATEGY
#define CHARLS_ENCODERSTRATEGY

#include "processline.h"
#include "decoderstrategy.h"


// Purpose: Implements encoding to stream of bits. In encoding mode JpegLsCodec inherits from EncoderStrategy
class EncoderStrategy
{

public:
    explicit EncoderStrategy(const JlsParameters& info) :
        _info(info),
        _valcurrent(0),
        _bitpos(0),
        _compressedLength(0),
        _position(nullptr),
        _isFFWritten(false),
        _bytesWritten(0),
        _compressedStream(nullptr)
    {
    }

    virtual ~EncoderStrategy() 
    {
    }

    int32_t PeekByte();

    void OnLineBegin(int32_t cpixel, void* ptypeBuffer, int32_t pixelStride)
    {
        _processLine->NewLineRequested(ptypeBuffer, cpixel, pixelStride);
    }

    void OnLineEnd(int32_t /*cpixel*/, void* /*ptypeBuffer*/, int32_t /*pixelStride*/) { }

    virtual void SetPresets(const JlsCustomParameters& presets) = 0;

    virtual std::size_t EncodeScan(std::unique_ptr<ProcessLine> rawData, ByteStreamInfo& compressedData, void* pvoidCompare) = 0;

    virtual ProcessLine* CreateProcess(ByteStreamInfo rawStreamInfo) = 0;

protected:

    void Init(ByteStreamInfo& compressedStream)
    {
        _bitpos = 32;
        _valcurrent = 0;

        if (compressedStream.rawStream)
        {
            _compressedStream = compressedStream.rawStream;
            _buffer.resize(4000);
            _position = static_cast<uint8_t*>(&_buffer[0]);
            _compressedLength = _buffer.size();
        }
        else
        {
            _position = compressedStream.rawData;
            _compressedLength = compressedStream.count;
        }
    }

    void AppendToBitStream(int32_t value, int32_t length)
    {
        ASSERT(length < 32 && length >= 0);
        ASSERT((!_qdecoder) || (length == 0 && value == 0) ||( _qdecoder->ReadLongValue(length) == value));

#ifndef NDEBUG
        if (length < 32)
        {
            int mask = (1 << (length)) - 1;
            ASSERT((value | mask) == mask);
        }
#endif

        _bitpos -= length;
        if (_bitpos >= 0)
        {
            _valcurrent = _valcurrent | (value << _bitpos);
            return;
        }
        _valcurrent |= value >> -_bitpos;

        Flush();

        ASSERT(_bitpos >=0);
        _valcurrent |= value << _bitpos;
    }

    void EndScan()
    {
        Flush();

        // if a 0xff was written, Flush() will force one unset bit anyway
        if (_isFFWritten)
            AppendToBitStream(0, (_bitpos - 1) % 8);
        else
            AppendToBitStream(0, _bitpos % 8);

        Flush();
        ASSERT(_bitpos == 0x20);

        if (_compressedStream)
        {
            OverFlow();
        }
    }

    void OverFlow()
    {
        if (!_compressedStream)
            throw std::system_error(static_cast<int>(charls::ApiResult::CompressedBufferTooSmall), CharLSCategoryInstance());

        std::size_t bytesCount = _position - static_cast<uint8_t*>(&_buffer[0]);
        std::size_t bytesWritten = static_cast<std::size_t>(_compressedStream->sputn(reinterpret_cast<char*>(&_buffer[0]), _position - static_cast<uint8_t*>(&_buffer[0])));

        if (bytesWritten != bytesCount)
            throw std::system_error(static_cast<int>(charls::ApiResult::CompressedBufferTooSmall), CharLSCategoryInstance());

        _position = static_cast<uint8_t*>(&_buffer[0]);
        _compressedLength = _buffer.size();
    }

    void Flush()
    {
        if (_compressedLength < 4 && _compressedStream)
        {
            OverFlow();
        }

        for (int32_t i = 0; i < 4; ++i)
        {
            if (_bitpos >= 32)
                break;

            if (_isFFWritten)
            {
                // insert highmost bit
                *_position = static_cast<uint8_t>(_valcurrent >> 25);
                _valcurrent = _valcurrent << 7;
                _bitpos += 7;
            }
            else
            {
                *_position = static_cast<uint8_t>(_valcurrent >> 24);
                _valcurrent = _valcurrent << 8;
                _bitpos += 8;
            }

            _isFFWritten = *_position == 0xFF;
            _position++;
            _compressedLength--;
            _bytesWritten++;
        }
    }

    std::size_t GetLength() const
    {
        return _bytesWritten - (_bitpos - 32) / 8;
    }

    inlinehint void AppendOnesToBitStream(int32_t length)
    {
        AppendToBitStream((1 << length) - 1, length);
    }

    std::unique_ptr<DecoderStrategy> _qdecoder;

protected:
    JlsParameters _info;
    std::unique_ptr<ProcessLine> _processLine;

private:
    unsigned int _valcurrent;
    int32_t _bitpos;
    std::size_t _compressedLength;

    // encoding
    uint8_t* _position;
    bool _isFFWritten;
    std::size_t _bytesWritten;

    std::vector<uint8_t> _buffer;
    std::basic_streambuf<char>* _compressedStream;
};

#endif
