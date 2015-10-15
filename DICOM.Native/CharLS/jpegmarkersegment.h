//
// (C) CharLS Team 2014, all rights reserved. See the accompanying "License.txt" for licensed use. 
//

#ifndef CHARLS_JPEGMARKERSEGMENT
#define CHARLS_JPEGMARKERSEGMENT

#include "util.h"
#include "jpegsegment.h"
#include "jpegstreamwriter.h"
#include <vector>
#include <cstdint>


enum class JpegMarkerCode : uint8_t;


class JpegMarkerSegment : public JpegSegment
{
public:
    JpegMarkerSegment(JpegMarkerCode marker, std::vector<uint8_t>&& content) :
        _marker(marker),
        _content(content)
    {
    }

    virtual void Serialize(JpegStreamWriter& streamWriter) override
    {
        streamWriter.WriteByte(0xFF);
        streamWriter.WriteByte(static_cast<uint8_t>(_marker));
        streamWriter.WriteWord(static_cast<uint16_t>(_content.size() + 2));
        streamWriter.WriteBytes(_content);
    }

    /// <summary>
    /// Creates a JPEG-LS Start Of Frame (SOF-55) marker.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <param name="bitsPerSample">The bits per sample.</param>
    /// <param name="componentCount">The component count.</param>
    static JpegMarkerSegment* CreateStartOfFrameMarker(int width, int height, int32_t bitsPerSample, int32_t componentCount);
    static JpegMarkerSegment* CreateJpegFileInterchangeFormatMarker(const JfifParameters& jfif);
    static JpegMarkerSegment* CreateJpegLSExtendedParametersMarker(const JlsCustomParameters& pcustom);
    static JpegMarkerSegment* CreateColorTransformMarker(charls::ColorTransformation transformation);
    static JpegMarkerSegment* CreateStartOfScanMarker(const JlsParameters& pparams, int32_t icomponent);

private:
    JpegMarkerCode _marker;
    std::vector<uint8_t> _content;
};

#endif
