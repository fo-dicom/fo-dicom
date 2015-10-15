//
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use. 
//
#ifndef CHARLS_JPEGSTREAMREADER
#define CHARLS_JPEGSTREAMREADER

#include "publictypes.h"
#include <cstdint>
#include <vector>


enum class JpegMarkerCode : uint8_t;
struct JlsParameters;
class JpegCustomParameters;


charls::ApiResult CheckParameterCoherent(const JlsParameters& pparams);
JlsCustomParameters ComputeDefault(int32_t MAXVAL, int32_t NEAR);



//
// JpegStreamReader: minimal implementation to read a JPEG byte stream.
//
class JpegStreamReader
{
public:
    JpegStreamReader(ByteStreamInfo byteStreamInfo);

    const JlsParameters& GetMetadata() const
    {
        return _info;
    }

    const JlsCustomParameters& GetCustomPreset() const
    {
        return _info.custom;
    }

    void Read(ByteStreamInfo info);
    void ReadHeader();

    void EnableCompare(bool bCompare)
    {
        _bCompare = bCompare;
    }

    void SetInfo(const JlsParameters& info)
    {
        _info = info;
    }

    void SetRect(const JlsRect& rect)
    {
        _rect = rect;
    }

    void ReadStartOfScan(bool firstComponent);
    uint8_t ReadByte();

private:
    void ReadScan(ByteStreamInfo rawPixels);
    int ReadPresetParameters();
    int ReadComment();
    int ReadStartOfFrame();
    int ReadWord();
    void ReadNBytes(std::vector<char>& dst, int byteCount);
    int ReadMarker(JpegMarkerCode marker);

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
