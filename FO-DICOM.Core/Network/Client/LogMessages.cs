// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Client.States;
using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Network.Client
{
    internal static partial class LogMessages
    {

        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request} is being sent")]
        internal static partial void DebugRequestSent(this ILogger logger, DicomRequest request);

        [LoggerMessage(Level = LogLevel.Debug, Message = "The current association can no longer accept P-DATA-TF messages, a new association will have to be created for the remaining requests")]
        internal static partial void DebugAssociationCannotProcessData(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Sending {NumberOfRequests} requests")]
        internal static partial void DebugNumberOfRequestsSending(this ILogger logger, int numberOfRequests);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Lingering on open association for {AssociationLingerTimeoutInMs} ms")]
        internal static partial void DebugLingeringForTimeout(this ILogger logger, int associationLingerTimeoutInMs);

        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request} has completed")]
        internal static partial void DebugRequestCompleted(this ILogger logger, DicomRequest request);

        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request} has timed out")]
        internal static partial void DebugRequestTimedOut(this ILogger logger, DicomRequest request);

        [LoggerMessage(Level = LogLevel.Debug, Message = "[{OldState}] --> [{NewState}]")]
        internal static partial void DebugClientStateChanged(this ILogger logger, DicomClientState oldState, DicomClientState newState);


    }
}
