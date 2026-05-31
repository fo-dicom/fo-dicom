// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    internal static partial class LogMessages
    {

        [LoggerMessage(Level = LogLevel.Debug, Message = "Sending association request from {CallingAE} to {CalledAE}")]
        internal static partial void LogSendingAssociationRequest(this ILogger logger, string callingAE, string calledAE);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} has been accepted")]
        internal static partial void LogAssociationRequestAccepted(this ILogger logger, string callingAE, string calledAE);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has rejected it: {Result} {Source} {Reason}")]
        internal static partial void LogAssociationRejectedFromCalled(this ILogger logger, string callingAE, string calledAE, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has aborted it: {Source} {Reason}")]
        internal static partial void LogAssociationAbortedFromCaller(this ILogger logger, string callingAE, string calledAE, DicomAbortSource source, DicomAbortReason reason);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} failed because the connection was closed")]
        internal static partial void LogAssocitionClosed(this ILogger logger, string callingAE, string calledAE);

    }
}
