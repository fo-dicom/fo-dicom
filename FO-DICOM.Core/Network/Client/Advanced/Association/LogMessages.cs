// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Network.Client.Advanced.Association
{
    /// <summary>
    /// Provides a collection of methods that log predefined, structured log messages for DICOM requests,
    /// responses, and connection events at debug and warning levels.
    /// </summary>
    /// <remarks>This static helper class encapsulates logging methods for various DICOM communication events 
    /// to enable consistent and efficient logging. The methods are intended for internal use and utilize the 
    /// .NET LoggerMessage attribute for high-performance logging. The class supports both ID-based and DICOM 
    /// object-based messages and provides methods for typical events such as timeouts, connection drops, 
    /// terminations, and improperly released resources.</remarks>
    internal static partial class LogMessages
    {
        /// <summary>
        /// Writes the debug message <c>"Request [{MessageID}]: {Status}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: {Status}")]
        internal static partial void DebugRequest(this ILogger logger, ushort messageId, DicomState status);

        /// <summary>
        /// Writes the debug message <c>"Request [{MessageID}]: {Status} {ResponseChannelIsGoneNote}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: {Status} {ResponseChannelIsGoneNote}")]
        internal static partial void DebugRequestResponseChannelIsGone(this ILogger logger, ushort messageId, DicomState status, string responseChannelIsGoneNote);

        /// <summary>
        /// Writes the debug message <c>"Request [{MessageID}]: Time-Out after {Timeout} {Info}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: Time-Out after {Timeout} {Info}")]
        internal static partial void DebugRequestTimeout(this ILogger logger, ushort messageId, TimeSpan timeout, string info);

        /// <summary>
        /// Writes the debug message <c>"{Request}: Time-Out after {Timeout}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: Time-Out after {Timeout}")]
        internal static partial void DebugRequestTimeout(this ILogger logger, DicomRequest request, TimeSpan timeout);

        /// <summary>
        /// Writes the debug message <c>"Request [{MessageID}]: Aborted {Info}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: Aborted {Info}")]
        internal static partial void DebugRequestAborted(this ILogger logger, int messageId, string info);

        /// <summary>
        /// Writes the debug message <c>"{Request}: Association was aborted"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: Association was aborted")]
        internal static partial void DebugRequestAborted(this ILogger logger, DicomRequest request);

        /// <summary>
        /// Writes the debug message <c>"Request [{MessageID}]: Connection closed {Info}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: Connection closed {Info}")]
        internal static partial void DebugRequestClosed(this ILogger logger, int messageId, string info);

        /// <summary>
        /// Writes the debug message <c>"{Request}: Connection was closed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: Connection was closed")]
        internal static partial void DebugRequestClosed(this ILogger logger, DicomRequest request);

        /// <summary>
        /// Writes the debug message <c>"Connection closed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Connection closed")]
        internal static partial void DebugConnectionClosed(this ILogger logger);

        /// <summary>
        /// Writes the debug message <c>"{Request}: {Response}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: {Response}")]
        internal static partial void DebugRequestResponse(this ILogger logger, DicomRequest request, DicomResponse response);

        /// <summary>
        /// Writes the debug message <c>"Waiting for association {Association} to be released"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Waiting for association {Association} to be released")]
        internal static partial void DebugWaitForAssociationRelease(this ILogger logger, string association);

        /// <summary>
        /// Writes the debug message <c>"Waiting for association {Association} to be released"</c> to log
        /// </summary>
        internal static void DebugWaitForAssociationRelease(this ILogger logger, DicomAssociation association)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.DebugWaitForAssociationRelease(AssociationToString(association));
            }
        }

        /// <summary>
        /// Writes the debug message <c>"Association {Association} has been released"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Association {Association} has been released")]
        internal static partial void DebugAssociationReleased(this ILogger logger, string association);

        /// <summary>
        /// Writes the debug message <c>"Association {Association} has been released"</c> to log
        /// </summary>
        internal static void DebugAssociationReleased(this ILogger logger, DicomAssociation association)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.DebugAssociationReleased(AssociationToString(association));
            }
        }

        /// <summary>
        /// Writes the debug message <c>"Association {Association} has been aborted"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Association {Association} has been aborted")]
        internal static partial void DebugAssociationAborted(this ILogger logger, string association);

        /// <summary>
        /// Writes the debug message <c>"Association {Association} has been aborted"</c> to log
        /// </summary>
        internal static void DebugAssociationAborted(this ILogger logger, DicomAssociation association)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.DebugAssociationAborted(AssociationToString(association));
            }
        }

        /// <summary>
        /// Writes the warning message <c>"DICOM association {Association} was not disposed correctly, but was garbage collected instead"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "DICOM association {Association} was not disposed correctly, but was garbage collected instead")]
        internal static partial void WarningAssociationNotDisposed(this ILogger logger, string association);

        /// <summary>
        /// Writes the debug message <c>"DICOM association {Association} was not disposed correctly, but was garbage collected instead"</c> to log
        /// </summary>
        internal static void WarningAssociationNotDisposed(this ILogger logger, DicomAssociation association)
        {
            if (logger.IsEnabled(LogLevel.Warning))
            {
                logger.WarningAssociationNotDisposed(AssociationToString(association));
            }
        }

        private static string AssociationToString(DicomAssociation association)
        {
            var callingAE = association.CallingAE ?? "<no calling AE>";
            var calledAE = association.CalledAE ?? "<no called AE>";
            var remoteHost = association.RemoteHost ?? "<no remote host>";
            var remotePort = association.RemotePort;
            return $"from {callingAE} to {calledAE} @{remoteHost}:{remotePort}";
        }
    }
}
