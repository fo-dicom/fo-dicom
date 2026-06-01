// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;

namespace FellowOakDicom
{
    internal static partial class LogMessages
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="charset"></param>
        [LoggerMessage(Level = LogLevel.Warning, Message = "\'{Charset}\' is not a valid DICOM encoding - using ASCII encoding instead")]
        internal static partial void WarningCharsetNoValidEncoding(this ILogger logger, string charset);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Unknown escape sequence found in string, using ASCII encoding")]
        internal static partial void WarningUnknownEscapeSequenceInAscii(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Found escape sequence for '{EncodingName}', which is not defined in Specific Character Set, using ASCII encoding instead")]
        internal static partial void WarningUnknownEscapeSequenceFallbackToAscii(this ILogger logger, string encodingName);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Could not encode string '{Fragment}' with given encodings, using replacement characters for encoding")]
        internal static partial void WarningNotEncodableFragment(this ILogger logger, string fragment);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Could not decode string '{Decoded}' with given encoding, using replacement characters")]
        internal static partial void WarningNotDecodableString(this ILogger logger, string decoded);

    }
}
