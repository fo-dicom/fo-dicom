// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.Extensions.Logging;
using System;
using System.Net;

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

        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Association release request")]
        internal static partial void InformationAssociationReleaseRequestSent(this ILogger logger, string logId);

        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Association release response")]
        internal static partial void InformationAssociationReleaseResponseSent(this ILogger logger, string logId);

        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Abort [source: {source}; reason: {reason}]")]
        internal static partial void InformationAbortSent(this ILogger logger, string logId, DicomAbortSource source, DicomAbortReason reason);

        [LoggerMessage(Level = LogLevel.Information, Message = "Socket error while {operation} PDU: {socketError} [{errorCode}]")]
        internal static partial void InformationSocketError(this ILogger logger, string operation, string socketError, int errorCode);

        [LoggerMessage(Level = LogLevel.Information, Message = "Object disposed while {operation} PDU")]
        internal static partial void InformationDisposedPDU(this ILogger logger, string operation);

        [LoggerMessage(Level = LogLevel.Error, Message = "I/O exception while {operation} PDU")]
        internal static partial void ErrorIOExceptionPDU(this ILogger logger, string operation, Exception exception);

        [LoggerMessage(Level = LogLevel.Warning, Message = "DICOM service {DicomServiceType} was not disposed correctly, but was garbage collected instead")]
        internal static partial void WarningDicomServiceNotDisposed(this ILogger logger, string dicomServiceType);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Unknown message type: {type}")]
        internal static partial void WarningUnknownMessageType(this ILogger logger, DicomCommandField type);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Conversion of dataset transfer syntax from: {datasetSyntax} to: {acceptedSyntax} is not supported.")]
        internal static partial void WarningConversionOfTransfersyntaxNotSupported(this ILogger logger, DicomTransferSyntax datasetSyntax, DicomTransferSyntax acceptedSyntax);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Will attempt to transfer dataset as-is.")]
        internal static partial void WarningNoConversionOfTransfersyntax(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Pixel Data (7fe0,0010) is removed from dataset.")]
        internal static partial void WarningPixelDataRemoved(this ILogger logger);

        [LoggerMessage(Level = LogLevel.Warning, Message = "Request [{messageID}] timed out, removing from pending queue and triggering timeout callbacks")]
        internal static partial void WarningRequestTimedOut(this ILogger logger, ushort messageID);

        [LoggerMessage(Level = LogLevel.Error, Message = "An 'object disposed' exception occurred while writing the next PDU to the network stream. This can happen when the connection is being closed")]
        internal static partial void ErrorObjectDisposedWhileWritingToStream(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Exception sending PDU")]
        internal static partial void ErrorSendingPdu(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Exception processing PDU")]
        internal static partial void ErrorProcessingPdu(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Error parsing C-Store dataset")]
        internal static partial void ErrorParsingCStore(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Exception processing P-Data-TF PDU")]
        internal static partial void ErrorProcessingDataPdu(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred while sending a DICOM message")]
        internal static partial void ErrorSendingMessage(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "No accepted presentation context found for abstract syntax: {sopClassUid}")]
        internal static partial void ErrorNoPresentationContextForAbstractSyntax(this ILogger logger, DicomUID sopClassUid);

        [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred in the Fellow Oak DICOM timeout detection loop")]
        internal static partial void ErrorInTimoutDetection(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Error during close attempt")]
        internal static partial void ErrorWhenClosingConnection(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Exception creating PDV")]
        internal static partial void ErrorCreatingPDV(this ILogger logger, Exception exception);

        [LoggerMessage(Level = LogLevel.Error, Message = "Exception writing data to PDV")]
        internal static partial void ErrorWritingPDV(this ILogger logger, Exception exception);


    }
}
