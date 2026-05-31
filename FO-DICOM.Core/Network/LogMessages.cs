// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace FellowOakDicom.Network
{
    internal static partial class LogMessages
    {

        [LoggerMessage(Level = LogLevel.Debug, Message = "Waiting for inbound client connection to {IPAddress}:{Port}")]
        internal static partial void DebugWaitForInboundConnection(this ILogger logger, IPAddress ipAddress, int port);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Client connected to {IPAddress}:{Port}")]
        internal static partial void DebugClientConnected(this ILogger logger, IPAddress ipAddress, int port);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Listener for {IPAddress}:{Port} has stopped because it was cancelled")]
        internal static partial void DebugConnectionCancelled(this ILogger logger, IPAddress iPAddress, int port);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Listener for {IPAddress}:{Port} has stopped because the connection was closed")]
        internal static partial void DebugConnectionClosed(this ILogger logger, IPAddress iPAddress, int port);

        [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred while listening for inbound client connections to {IPAddress}:{Port}")]
        internal static partial void ErrorWhileListeningForConnection(this ILogger logger, Exception exception, IPAddress iPAddress, int port);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Accepted an incoming client connection, there are now {NumberOfServices} connected clients")]
        internal static partial void DebugAcceptedIncommingConnection(this ILogger logger, int numberOfServices);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Reached the maximum number of simultaneously connected clients, further incoming connections will be blocked until one or more clients disconnect")]
        internal static partial void WaringReachedMaximumNumberOfConnections(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Cancellation occurred while accepting an incoming client connection")]
        internal static partial void WaringAcceptingIncomingConnectionCanceled(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Error, Message = "An exception occurred while accepting an incoming client connection")]
        internal static partial void ErrorWhileAcceptionIncommingConnection(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Warning, Message = "DICOM server was canceled")]
        internal static partial void WarningServerWasCanceled(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Exception listening for DICOM services")]
        internal static partial void ErrorOnServerListening(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Warning, Message = "An error occurred while trying to dispose a DICOM service")]
        internal static partial void WarningErrorDisposingServer(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Waited {MaxClientsAllowedInterval}, but we still cannot accept another incoming connection because the maximum number of clients ({MaxClientsAllowed}) has been reached")]
        internal static partial void WarningMaxClientsAllowedReached(this ILogger logger, int maxClientsAllowed, TimeSpan maxClientsAllowedInterval);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Read 0 bytes from network stream while reading PDU header, connection will be marked as closed")]
        internal static partial void DebugNoDataFromConnectionStream(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Debug, Message = "An 'object disposed' exception occurred while listening to the network stream. This can happen when the connection is being closed.")]
        internal static partial void DebugConnectionDisposed(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Debug, Message = "A 'null reference' exception occurred while listening to the network stream. This can happen when the connection is already closed.")]
        internal static partial void DebugConnectionNullReference(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Debug, Message = "Removing request [{MessageID}] from pending queue because an error occurred while sending it")]
        internal static partial void DebugRemovingRequestFromQueueBecauseOfError(this ILogger logger, ushort messageID, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Failed to send DICOM message")]
        internal static partial void ErrorSendingMessage(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} -> {pdu}")]
        internal static partial void InformationDataPduSent(this ILogger logger, string logId, PDataTF pdu);

        [LoggerMessage(Level = LogLevel.Information, Message = "{CallingAE} <- Association request:\n{Association}")]
        internal static partial void InformationAssociationRequestReceived(this ILogger logger, string callingAE, DicomAssociation association);

        [LoggerMessage(Level = LogLevel.Information, Message = "{CalledAE} <- Association accept:\n{Association}")]
        internal static partial void InformationAssociationAcceptReceived(this ILogger logger, string calledAE, DicomAssociation association);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Association reject [result: {Result}; source: {Source}; reason: {Reason}]")]
        internal static partial void InformationAssociationRejectedReceived(this ILogger logger, string logId, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- {@pdu}")]
        internal static partial void InformationDataPduReceived(this ILogger logger, string logId, PDataTF pdu);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Association release request")]
        internal static partial void InformationAssociationReleaseRequestReceived(this ILogger logger, string logId);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Association release response")]
        internal static partial void InformationAssociationReleaseResponseReceived(this ILogger logger, string logId);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Abort: {Source} - {Reason}")]
        internal static partial void InformationAbortReceived(this ILogger logger, string logId, DicomAbortSource source, DicomAbortReason reason);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- {DicomMessage}")]
        internal static partial void InformationDicomMessageReceived(this ILogger logger, string logId, string dicomMessage);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} -> {DicomMessage}")]
        internal static partial void InformationDicomMessageSent(this ILogger logger, string logId, string dicomMessage);

        [LoggerMessage(Level = LogLevel.Information, Message = "Tried to close connection but queues are not empty, PDUs: {pduCount}, messages: {msgCount}, pending requests: {pendingCount}")]
        internal static partial void InformationTriedCloseConnectionWithNonemptyQueues(this ILogger logger, int pduCount, int msgCount, int pendingCount);

        [LoggerMessage(Level = LogLevel.Information, Message = "Connection closed")]
        internal static partial void InformationConnectionClosed(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Information, Message = "{CalledAE} -> Association request:\n{Association}")]
        internal static partial void InformationAssociationRequenstSent(this ILogger logger, string calledAE, DicomAssociation association);

        [LoggerMessage(Level = LogLevel.Information, Message = "{LogId} -> Association accept:\n{Association}")]
        internal static partial void InformationAssociationAcceptSent(this ILogger logger, string logId, DicomAssociation association);

        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Association reject [result: {result}; source: {source}; reason: {reason}]")]
        internal static partial void InformationAssociationRejectSent(this ILogger logger, string logId, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);


    }
}
