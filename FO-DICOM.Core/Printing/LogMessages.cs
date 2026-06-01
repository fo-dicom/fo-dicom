// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Printing
{
    internal static partial class LogMessages
    {

        /// <summary>
        /// Writes the information message <c>"Applying display format {ImageDisplayFormat} for film box {SOPInstanceUID}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "Applying display format {ImageDisplayFormat} for film box {SOPInstanceUID}")]
        internal static partial void InformationApplyDisplayFormat(this ILogger logger, string imageDisplayFormat, DicomUID SOPInstanceUID);

        /// <summary>
        /// Writes the error message <c>"No display format present in N-CREATE Basic Film Box dataset"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "No display format present in N-CREATE Basic Film Box dataset")]
        internal static partial void ErrorNoDisplayFormatPresent(this ILogger logger);

        /// <summary>
        /// Writes the error message <c>"Exception in FilmBox initialization"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Exception in FilmBox initialization")]
        internal static partial void ErrorInitializeFilmbox(this ILogger logger, Exception exception);


    }
}
