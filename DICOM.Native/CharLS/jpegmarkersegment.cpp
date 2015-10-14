//
// (C) CharLS Team 2014, all rights reserved. See the accompanying "License.txt" for licensed use. 
//

#include "jpegmarkersegment.h"
#include "jpegmarkercode.h"
#include "util.h"
#include <vector>
#include <cstdint>


using namespace charls;


JpegMarkerSegment* JpegMarkerSegment::CreateStartOfFrameMarker(int width, int height, int32_t bitsPerSample, int32_t componentCount)
{
    ASSERT(width >= 0 && width <= UINT16_MAX);
    ASSERT(height >= 0 && height <= UINT16_MAX);
    ASSERT(bitsPerSample > 0 && bitsPerSample <= UINT8_MAX);
    ASSERT(componentCount > 0 && componentCount <= (UINT8_MAX - 1));

    // Create a Frame Header as defined in T.87, C.2.2 and T.81, B.2.2
    std::vector<uint8_t> content;
    content.push_back(static_cast<uint8_t>(bitsPerSample)); // P = Sample precision
    push_back(content, static_cast<uint16_t>(height));    // Y = Number of lines
    push_back(content, static_cast<uint16_t>(width));    // X = Number of samples per line

    // Components
    content.push_back(static_cast<uint8_t>(componentCount)); // Nf = Number of image components in frame
    for (int component = 0; component < componentCount; ++component)
    {
        // Component Specification parameters
        content.push_back(static_cast<uint8_t>(component + 1)); // Ci = Component identifier
        content.push_back(0x11);                                // Hi + Vi = Horizontal sampling factor + Vertical sampling factor
        content.push_back(0);                                   // Tqi = Quantization table destination selector (reserved for JPEG-LS, should be set to 0)
    }

    return new JpegMarkerSegment(JpegMarkerCode::StartOfFrameJpegLS, std::move(content));
}


JpegMarkerSegment* JpegMarkerSegment::CreateJpegFileInterchangeFormatMarker(const JfifParameters& jfifParameters)
{
    uint8_t jfifID [] = { 'J', 'F', 'I', 'F', '\0' };

    std::vector<uint8_t> rgbyte;
    for (int i = 0; i < static_cast<int>(sizeof(jfifID)); i++)
    {
        rgbyte.push_back(jfifID[i]);
    }

    push_back(rgbyte, static_cast<uint16_t>(jfifParameters.Ver));

    rgbyte.push_back(jfifParameters.units);
    push_back(rgbyte, static_cast<uint16_t>(jfifParameters.XDensity));
    push_back(rgbyte, static_cast<uint16_t>(jfifParameters.YDensity));

    // thumbnail
    rgbyte.push_back(static_cast<uint8_t>(jfifParameters.Xthumb));
    rgbyte.push_back(static_cast<uint8_t>(jfifParameters.Ythumb));
    if (jfifParameters.Xthumb > 0)
    {
        if (jfifParameters.pdataThumbnail)
            throw std::system_error(static_cast<int>(ApiResult::InvalidJlsParameters), CharLSCategoryInstance());

        rgbyte.insert(rgbyte.end(), static_cast<uint8_t*>(jfifParameters.pdataThumbnail), static_cast<uint8_t*>(jfifParameters.pdataThumbnail) + 3 * jfifParameters.Xthumb * jfifParameters.Ythumb);
    }

    return new JpegMarkerSegment(JpegMarkerCode::ApplicationData0, std::move(rgbyte));
}


JpegMarkerSegment* JpegMarkerSegment::CreateJpegLSExtendedParametersMarker(const JlsCustomParameters& customParameters)
{
    std::vector<uint8_t> rgbyte;

    rgbyte.push_back(1);
    push_back(rgbyte, static_cast<uint16_t>(customParameters.MAXVAL));
    push_back(rgbyte, static_cast<uint16_t>(customParameters.T1));
    push_back(rgbyte, static_cast<uint16_t>(customParameters.T2));
    push_back(rgbyte, static_cast<uint16_t>(customParameters.T3));
    push_back(rgbyte, static_cast<uint16_t>(customParameters.RESET));

    return new JpegMarkerSegment(JpegMarkerCode::JpegLSExtendedParameters, std::move(rgbyte));
}


JpegMarkerSegment* JpegMarkerSegment::CreateColorTransformMarker(ColorTransformation transformation)
{
    std::vector<uint8_t> rgbyteXform;

    rgbyteXform.push_back('m');
    rgbyteXform.push_back('r');
    rgbyteXform.push_back('f');
    rgbyteXform.push_back('x');
    rgbyteXform.push_back(static_cast<uint8_t>(transformation));

    return new JpegMarkerSegment(JpegMarkerCode::ApplicationData8, std::move(rgbyteXform));
}


JpegMarkerSegment* JpegMarkerSegment::CreateStartOfScanMarker(const JlsParameters& params, int32_t icomponent)
{
    uint8_t itable = 0;

    std::vector<uint8_t> rgbyte;

    if (icomponent < 0)
    {
        rgbyte.push_back(static_cast<uint8_t>(params.components));
        for (int i = 0; i < params.components; ++i)
        {
            rgbyte.push_back(uint8_t(i + 1));
            rgbyte.push_back(itable);
        }
    }
    else
    {
        rgbyte.push_back(1);
        rgbyte.push_back(static_cast<uint8_t>(icomponent));
        rgbyte.push_back(itable);
    }

    rgbyte.push_back(static_cast<uint8_t>(params.allowedlossyerror));
    rgbyte.push_back(static_cast<uint8_t>(params.ilv));
    rgbyte.push_back(0); // transform

    return new JpegMarkerSegment(JpegMarkerCode::StartOfScan, std::move(rgbyte));
}
