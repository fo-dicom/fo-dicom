// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;

namespace FellowOakDicom.Network.Client.Advanced.Association
{
    internal static partial class LogMessages
    {

        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: {Status}")]
        internal static partial void DebugRequest(this ILogger logger, ushort messageId, DicomState status);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: {Status} {ResponseChannelIsGoneNote}")]
        internal static partial void DebugRequestResponseChannelIsGone(this ILogger logger, ushort messageId, DicomState status, string responseChannelIsGoneNote);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: Time-Out after {Timeout} {Info}")]
        internal static partial void DebugRequestTimeout(this ILogger logger, ushort messageId, TimeSpan timeout, string info);

        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: Time-Out after {Timeout}")]
        internal static partial void DebugRequestTimeout(this ILogger logger, DicomRequest request, TimeSpan timeout);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: Aborted {Info}")]
        internal static partial void DebugRequestAborted(this ILogger logger, int messageId, string info);

        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: Association was aborted")]
        internal static partial void DebugRequestAborted(this ILogger logger, DicomRequest request);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Request [{MessageID}]: Connection closed {Info}")]
        internal static partial void DebugRequestClosed(this ILogger logger, int messageId, string info);

        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: Connection was closed")]
        internal static partial void DebugRequestClosed(this ILogger logger, DicomRequest request);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Connection closed")]
        internal static partial void DebugConnectionClosed(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Debug, Message = "{Request}: {Response}")]
        internal static partial void DebugRequestResponse(this ILogger logger, DicomRequest request, DicomResponse response);

        /// <summary>
        /// "Waiting for association {Association} to be released"
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Waiting for association {Association} to be released")]
        internal static partial void DebugWaitForAssociationRelease(this ILogger logger, string association);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Association {Association} has been released")]
        internal static partial void DebugAssociationReleased(this ILogger logger, string association);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Association {Association} has been aborted")]
        internal static partial void DebugAssociationAborted(this ILogger logger, string association);

        [LoggerMessage(Level = LogLevel.Warning, Message = "DICOM association {Association} was not disposed correctly, but was garbage collected instead")]
        internal static partial void WarningAssociationNotDisposed(this ILogger logger, string association);


        internal static void DebugAssociationAborted(this ILogger logger, DicomAssociation association)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.DebugAssociationAborted(AssociationToString(association));
            }
        }

        internal static void DebugWaitForAssociationRelease(this ILogger logger, DicomAssociation association)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.DebugWaitForAssociationRelease(AssociationToString(association));
            }
        }

        internal static void DebugAssociationReleased(this ILogger logger, DicomAssociation association)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.DebugAssociationReleased(AssociationToString(association));
            }
        }

        internal static void DebugAssociationNotDisposed(this ILogger logger, DicomAssociation association)
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
