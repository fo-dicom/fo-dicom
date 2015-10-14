//
// (C) CharLS Team 2014, all rights reserved. See the accompanying "License.txt" for licensed use. 
//


#include "jpegstreamwriter.h"
#include "util.h"
#include "jpegmarkersegment.h"
#include "jpegimagedatasegment.h"
#include "jpegmarkercode.h"
#include "jpegstreamreader.h"
#include <vector>


using namespace charls;


namespace
{
    bool IsDefault(const JlsCustomParameters& custom)
    {
        if (custom.MAXVAL != 0)
            return false;

        if (custom.T1 != 0)
            return false;

        if (custom.T2 != 0)
            return false;

        if (custom.T3 != 0)
            return false;

        if (custom.RESET != 0)
            return false;

        return true;
    }
}


JpegStreamWriter::JpegStreamWriter() :
_bCompare(false),
_data(),
_byteOffset(0),
_lastCompenentIndex(0)
{
}


JpegStreamWriter::~JpegStreamWriter()
{
    for (size_t i = 0; i < _segments.size(); ++i)
    {
        delete _segments[i];
    }
}


void JpegStreamWriter::AddColorTransform(ColorTransformation transformation)
{
    AddSegment(JpegMarkerSegment::CreateColorTransformMarker(transformation));
}


size_t JpegStreamWriter::Write(const ByteStreamInfo& info)
{
    _data = info;

    WriteMarker(JpegMarkerCode::StartOfImage);

    for (size_t i = 0; i < _segments.size(); ++i)
    {
        _segments[i]->Serialize(*this);
    }

    //_bCompare = false;

    WriteMarker(JpegMarkerCode::EndOfImage);

    return _byteOffset;
}


void JpegStreamWriter::AddScan(const ByteStreamInfo& info, const JlsParameters& params)
{
    if (!IsDefault(params.custom))
    {
        AddSegment(JpegMarkerSegment::CreateJpegLSExtendedParametersMarker(params.custom));
    }
    else if (params.bitspersample > 12)
    {
        JlsCustomParameters preset = ComputeDefault((1 << params.bitspersample) - 1, params.allowedlossyerror);
        AddSegment(JpegMarkerSegment::CreateJpegLSExtendedParametersMarker(preset));
    }

    _lastCompenentIndex += 1;
    AddSegment(JpegMarkerSegment::CreateStartOfScanMarker(params, params.ilv == InterleaveMode::None ? _lastCompenentIndex : -1));

    int ccomp = params.ilv == InterleaveMode::None ? 1 : params.components;

    AddSegment(new JpegImageDataSegment(info, params, _lastCompenentIndex, ccomp));
}
