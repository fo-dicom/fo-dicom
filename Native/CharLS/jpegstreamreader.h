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


JpegLSPresetCodingParameters ComputeDefault(int32_t MAXVAL, int32_t NEAR);


//
// JpegStreamReader: minimal implementation to read a JPEG byte stream.
//
class JpegStreamReader
{
public:
    explicit JpegStreamReader(ByteStreamInfo byteStreamInfo);

    const JlsParameters& GetMetadata() const
    {
        return _params;
    }

    const JpegLSPresetCodingParameters& GetCustomPreset() const
    {
        return _params.custom;
    }

    void Read(ByteStreamInfo info);
    void ReadHeader();

    void SetInfo(const JlsParameters& params)
    {
        _params = params;
    }

    void SetRect(const JlsRect& rect)
    {
        _rect = rect;
    }

    void ReadStartOfScan(bool firstComponent);
    uint8_t ReadByte();

private:
    JpegMarkerCode ReadNextMarker();
    int ReadPresetParameters();
    int ReadComment() const;
    int ReadStartOfFrame();
    int ReadWord();
    void ReadNBytes(std::vector<char>& dst, int byteCount);
    int ReadMarker(JpegMarkerCode marker);

    void ReadJfif();

    // Color Transform Application Markers & Code Stream (HP extension)
    int ReadColorSpace() const;
    int ReadColorXForm();

    ByteStreamInfo _byteStream;
    JlsParameters _params;
    JlsRect _rect;
};


#endif
