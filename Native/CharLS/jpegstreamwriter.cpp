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

using namespace std;
using namespace charls;


namespace
{
    bool IsDefault(const JpegLSPresetCodingParameters& custom)
    {
        if (custom.MaximumSampleValue != 0)
            return false;

        if (custom.Threshold1 != 0)
            return false;

        if (custom.Threshold2 != 0)
            return false;

        if (custom.Threshold3 != 0)
            return false;

        if (custom.ResetValue != 0)
            return false;

        return true;
    }
}


JpegStreamWriter::JpegStreamWriter() :
    _data(),
    _byteOffset(0),
    _lastCompenentIndex(0)
{
}


void JpegStreamWriter::AddColorTransform(ColorTransformation transformation)
{
    AddSegment(JpegMarkerSegment::CreateColorTransformSegment(transformation));
}


size_t JpegStreamWriter::Write(const ByteStreamInfo& info)
{
    _data = info;

    WriteMarker(JpegMarkerCode::StartOfImage);

    for (size_t i = 0; i < _segments.size(); ++i)
    {
        _segments[i]->Serialize(*this);
    }

    WriteMarker(JpegMarkerCode::EndOfImage);

    return _byteOffset;
}


void JpegStreamWriter::AddScan(const ByteStreamInfo& info, const JlsParameters& params)
{
    if (!IsDefault(params.custom))
    {
        AddSegment(JpegMarkerSegment::CreateJpegLSPresetParametersSegment(params.custom));
    }
    else if (params.bitsPerSample > 12)
    {
        const JpegLSPresetCodingParameters preset = ComputeDefault((1 << params.bitsPerSample) - 1, params.allowedLossyError);
        AddSegment(JpegMarkerSegment::CreateJpegLSPresetParametersSegment(preset));
    }

    // Note: it is a common practice to start to count components by index 1.
    _lastCompenentIndex += 1;
    const int componentCount = params.interleaveMode == InterleaveMode::None ? 1 : params.components;
    AddSegment(JpegMarkerSegment::CreateStartOfScanSegment(_lastCompenentIndex, componentCount, params.allowedLossyError, params.interleaveMode));

    AddSegment(make_unique<JpegImageDataSegment>(info, params, componentCount));
}
