// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    internal static partial class LogMessages
    {

        /// <summary>
        /// Writes the debug message <c>"Sending association request from {CallingAE} to {CalledAE}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Sending association request from {CallingAE} to {CalledAE}")]
        internal static partial void LogSendingAssociationRequest(this ILogger logger, string callingAE, string calledAE);

        /// <summary>
        /// Writes the debug message <c>"Association request from {CallingAE} to {CalledAE} has been accepted"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} has been accepted")]
        internal static partial void LogAssociationRequestAccepted(this ILogger logger, string callingAE, string calledAE);

        /// <summary>
        /// Writes the debug message <c>"Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has rejected it: {Result} {Source} {Reason}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has rejected it: {Result} {Source} {Reason}")]
        internal static partial void LogAssociationRejectedFromCalled(this ILogger logger, string callingAE, string calledAE, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Writes the debug message <c>"Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has aborted it: {Source} {Reason}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} failed because {CalledAE} has aborted it: {Source} {Reason}")]
        internal static partial void LogAssociationAbortedFromCaller(this ILogger logger, string callingAE, string calledAE, DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Writes the debug message <c>"Association request from {CallingAE} to {CalledAE} failed because the connection was closed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Association request from {CallingAE} to {CalledAE} failed because the connection was closed")]
        internal static partial void LogAssocitionClosed(this ILogger logger, string callingAE, string calledAE);

    }
}
