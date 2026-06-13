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

        /// <summary>
        /// Writes the debug message <c>"{Request} is being sent"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request} is being sent")]
        internal static partial void DebugRequestSent(this ILogger logger, DicomRequest request);

        /// <summary>
        /// Writes the debug message <c>"The current association can no longer accept P-DATA-TF messages, a new association will have to be created for the remaining requests"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "The current association can no longer accept P-DATA-TF messages, a new association will have to be created for the remaining requests")]
        internal static partial void DebugAssociationCannotProcessData(this ILogger logger);

        /// <summary>
        /// Writes the debug message <c>"Sending {NumberOfRequests} requests"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Sending {NumberOfRequests} requests")]
        internal static partial void DebugNumberOfRequestsSending(this ILogger logger, int numberOfRequests);

        /// <summary>
        /// Writes the debug message <c>"Lingering on open association for {AssociationLingerTimeoutInMs} ms"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Lingering on open association for {AssociationLingerTimeoutInMs} ms")]
        internal static partial void DebugLingeringForTimeout(this ILogger logger, int associationLingerTimeoutInMs);

        /// <summary>
        /// Writes the debug message <c>"{Request} has completed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request} has completed")]
        internal static partial void DebugRequestCompleted(this ILogger logger, DicomRequest request);

        /// <summary>
        /// Writes the debug message <c>"{Request} has timed out"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request} has timed out")]
        internal static partial void DebugRequestTimedOut(this ILogger logger, DicomRequest request);

        /// <summary>
        /// Writes the debug message <c>"[{OldState}] --> [{NewState}]"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "[{OldState}] --> [{NewState}]")]
        internal static partial void DebugClientStateChanged(this ILogger logger, DicomClientState oldState, DicomClientState newState);

        /// <summary>
        /// Writes the warning message <c>"Successful user identity negotiation with type {UserIdentityType} was required but server response was null"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Successful user identity negotiation with type {UserIdentityType} was required but server response was null")]
        internal static partial void WarningNoValidUserIdentityNegotiation(this ILogger logger, DicomUserIdentityType? userIdentityType);

        /// <summary>
        /// Writes the warning message <c>"DICOM request sending was cancelled"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "DICOM request sending was cancelled")]
        internal static partial void WarningSendingRequestsCancelled(this ILogger logger);

        /// <summary>
        /// Writes the error message <c>"An error occurred while sending DICOM requests"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred while sending DICOM requests")]
        internal static partial void ErrorWhileSendingRequest(this ILogger logger, Exception exception);


    }
}
