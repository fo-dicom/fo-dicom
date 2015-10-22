//
// (C) CharLS Team 2014, all rights reserved. See the accompanying "License.txt" for licensed use. 
//

#ifndef CHARLS_JPEGIMAGEDATASEGMENT
#define CHARLS_JPEGIMAGEDATASEGMENT

#include "util.h"
#include "jpegsegment.h"
#include "jpegstreamwriter.h"

class JpegImageDataSegment : public JpegSegment
{
public:
    JpegImageDataSegment(ByteStreamInfo rawStream, const JlsParameters& info, int32_t icompStart, int ccompScan) :
        _ccompScan(ccompScan),
        _icompStart(icompStart),
        _rawStreamInfo(rawStream),
        _info(info)
    {
    }

    virtual void Serialize(JpegStreamWriter& streamWriter) override;

private:
    int _ccompScan;
    int32_t _icompStart;
    ByteStreamInfo _rawStreamInfo;
    JlsParameters _info;
};

#endif
