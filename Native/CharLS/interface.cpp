//
// (C) Jan de Vaan 2007-2010, all rights reserved. See the accompanying "License.txt" for licensed use.
//

#include "charls.h"
#include "util.h"
#include "jpegstreamreader.h"
#include "jpegstreamwriter.h"
#include "jpegmarkersegment.h"
#include <cstring>

using namespace std;
using namespace charls;

namespace
{

void VerifyInput(const ByteStreamInfo& uncompressedStream, const JlsParameters& parameters)
{
    if (!uncompressedStream.rawStream && !uncompressedStream.rawData)
        throw charls_error(ApiResult::InvalidJlsParameters, "rawStream or rawData needs to reference to something");

    if (parameters.width < 1 || parameters.width > 65535)
        throw charls_error(ApiResult::InvalidJlsParameters, "width needs to be in the range [1, 65535]");

    if (parameters.height < 1 || parameters.height > 65535)
        throw charls_error(ApiResult::InvalidJlsParameters, "height needs to be in the range [1, 65535]");

    if (parameters.bitsPerSample < 2 || parameters.bitsPerSample > 16)
        throw charls_error(ApiResult::InvalidJlsParameters, "bitspersample needs to be in the range [2, 16]");

    if (!(parameters.interleaveMode == InterleaveMode::None || parameters.interleaveMode == InterleaveMode::Sample || parameters.interleaveMode == InterleaveMode::Line))
        throw charls_error(ApiResult::InvalidJlsParameters, "interleaveMode needs to be set to a value of {None, Sample, Line}");

    if (parameters.components < 1 || parameters.components > 255)
        throw charls_error(ApiResult::InvalidJlsParameters, "components needs to be in the range [1, 255]");

    if (uncompressedStream.rawData)
    {
        if (uncompressedStream.count < static_cast<size_t>(parameters.height * parameters.width * parameters.components * (parameters.bitsPerSample > 8 ? 2 : 1)))
            throw charls_error(ApiResult::InvalidJlsParameters, "uncompressed size does not match with the other parameters");
    }

    switch (parameters.components)
    {
    case 3:
        break;
    case 4:
        if (parameters.interleaveMode == InterleaveMode::Sample)
            throw charls_error(ApiResult::InvalidJlsParameters, "interleaveMode cannot be set to Sample in combination with components = 4");
        break;
    default:
        if (parameters.interleaveMode != InterleaveMode::None)
            throw charls_error(ApiResult::InvalidJlsParameters, "interleaveMode can only be set to None in combination with components = 1");
        break;
    }
}


ApiResult ResultAndErrorMessage(ApiResult result, char* errorMessage)
{
    if (errorMessage)
    {
        errorMessage[0] = 0;
    }

    return result;
}


ApiResult ResultAndErrorMessageFromException(char* errorMessage)
{
    try
    {
        // retrow the exception.
        throw;
    }
    catch (const charls_error& error)
    {
        if (errorMessage)
        {
            ASSERT(strlen(error.what()) < ErrorMessageSize);
            strcpy(errorMessage, error.what());
        }

        return static_cast<ApiResult>(error.code().value());
    }
    catch (...)
    {
        return ResultAndErrorMessage(ApiResult::UnexpectedFailure, errorMessage);
    }
}

} // namespace


CHARLS_IMEXPORT(ApiResult) JpegLsEncodeStream(ByteStreamInfo compressedStreamInfo, size_t& pcbyteWritten,
    ByteStreamInfo rawStreamInfo, const struct JlsParameters& params, char* errorMessage)
{
    try
    {
        VerifyInput(rawStreamInfo, params);

        JlsParameters info = params;
        if (info.stride == 0)
        {
            info.stride = info.width * ((info.bitsPerSample + 7)/8);
            if (info.interleaveMode != InterleaveMode::None)
            {
                info.stride *= info.components;
            }
        }

        JpegStreamWriter writer;
        if (info.jfif.version)
        {
            writer.AddSegment(JpegMarkerSegment::CreateJpegFileInterchangeFormatSegment(info.jfif));
        }

        writer.AddSegment(JpegMarkerSegment::CreateStartOfFrameSegment(info.width, info.height, info.bitsPerSample, info.components));

        if (info.colorTransformation != ColorTransformation::None)
        {
            writer.AddColorTransform(info.colorTransformation);
        }

        if (info.interleaveMode == InterleaveMode::None)
        {
            const int32_t cbyteComp = info.width * info.height * ((info.bitsPerSample + 7) / 8);
            for (int32_t component = 0; component < info.components; ++component)
            {
                writer.AddScan(rawStreamInfo, info);
                SkipBytes(rawStreamInfo, cbyteComp);
            }
        }
        else
        {
            writer.AddScan(rawStreamInfo, info);
        }

        writer.Write(compressedStreamInfo);
        pcbyteWritten = writer.GetBytesWritten();

        return ResultAndErrorMessage(ApiResult::OK, errorMessage);
    }
    catch (...)
    {
        return ResultAndErrorMessageFromException(errorMessage);
    }
}


CHARLS_IMEXPORT(ApiResult) JpegLsDecodeStream(ByteStreamInfo rawStream, ByteStreamInfo compressedStream, const JlsParameters* info, char* errorMessage)
{
    try
    {
        JpegStreamReader reader(compressedStream);

        if (info)
        {
            reader.SetInfo(*info);
        }

        reader.Read(rawStream);

        return ResultAndErrorMessage(ApiResult::OK, errorMessage);
    }
    catch (...)
    {
        return ResultAndErrorMessageFromException(errorMessage);
    }
}


CHARLS_IMEXPORT(ApiResult) JpegLsReadHeaderStream(ByteStreamInfo rawStreamInfo, JlsParameters* params, char* errorMessage)
{
    try
    {
        JpegStreamReader reader(rawStreamInfo);
        reader.ReadHeader();
        reader.ReadStartOfScan(true);
        *params = reader.GetMetadata();

        return ResultAndErrorMessage(ApiResult::OK, errorMessage);
    }
    catch (...)
    {
        return ResultAndErrorMessageFromException(errorMessage);
    }
}

extern "C"
{
    CHARLS_IMEXPORT(ApiResult) JpegLsEncode(void* destination, size_t destinationLength, size_t* bytesWritten, const void* source, size_t sourceLength, const struct JlsParameters* params, char* errorMessage)
    {
        if (!destination || !bytesWritten || !source || !params)
            return ApiResult::InvalidJlsParameters;

        const ByteStreamInfo rawStreamInfo = FromByteArrayConst(source, sourceLength);
        const ByteStreamInfo compressedStreamInfo = FromByteArray(destination, destinationLength);

        return JpegLsEncodeStream(compressedStreamInfo, *bytesWritten, rawStreamInfo, *params, errorMessage);
    }


    CHARLS_IMEXPORT(ApiResult) JpegLsReadHeader(const void* compressedData, size_t compressedLength, JlsParameters* params, char* errorMessage)
    {
        return JpegLsReadHeaderStream(FromByteArrayConst(compressedData, compressedLength), params, errorMessage);
    }


    CHARLS_IMEXPORT(ApiResult) JpegLsDecode(void* destination, size_t destinationLength, const void* source, size_t sourceLength, const struct JlsParameters* params, char* errorMessage)
    {
        const ByteStreamInfo compressedStream = FromByteArrayConst(source, sourceLength);
        const ByteStreamInfo rawStreamInfo = FromByteArray(destination, destinationLength);

        return JpegLsDecodeStream(rawStreamInfo, compressedStream, params, errorMessage);
    }


    CHARLS_IMEXPORT(ApiResult) JpegLsDecodeRect(void* uncompressedData, size_t uncompressedLength, const void* compressedData, size_t compressedLength,
        JlsRect roi, JlsParameters* info, char* errorMessage)
    {
        try
        {
            const ByteStreamInfo compressedStream = FromByteArrayConst(compressedData, compressedLength);
            JpegStreamReader reader(compressedStream);

            const ByteStreamInfo rawStreamInfo = FromByteArray(uncompressedData, uncompressedLength);

            if (info)
            {
                reader.SetInfo(*info);
            }

            reader.SetRect(roi);
            reader.Read(rawStreamInfo);

            return ResultAndErrorMessage(ApiResult::OK, errorMessage);
        }
        catch (...)
        {
            return ResultAndErrorMessageFromException(errorMessage);
        }
    }
}
