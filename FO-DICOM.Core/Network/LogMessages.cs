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

        /// <summary>
        /// Writes the debug message <c>"Waiting for inbound client connection to {IPAddress}:{Port}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Waiting for inbound client connection to {IPAddress}:{Port}")]
        internal static partial void DebugWaitForInboundConnection(this ILogger logger, IPAddress ipAddress, int port);

        /// <summary>
        /// Writes the debug message <c>"Client connected to {IPAddress}:{Port}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Client connected to {IPAddress}:{Port}")]
        internal static partial void DebugClientConnected(this ILogger logger, IPAddress ipAddress, int port);

        /// <summary>
        /// Writes the debug message <c>"Listener for {IPAddress}:{Port} has stopped because it was cancelled"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Listener for {IPAddress}:{Port} has stopped because it was cancelled")]
        internal static partial void DebugConnectionCancelled(this ILogger logger, IPAddress iPAddress, int port);

        /// <summary>
        /// Writes the debug message <c>"Listener for {IPAddress}:{Port} has stopped because the connection was closed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Listener for {IPAddress}:{Port} has stopped because the connection was closed")]
        internal static partial void DebugConnectionClosed(this ILogger logger, IPAddress iPAddress, int port);

        /// <summary>
        /// Writes the error message <c>"An error occurred while listening for inbound client connections to {IPAddress}:{Port}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred while listening for inbound client connections to {IPAddress}:{Port}")]
        internal static partial void ErrorWhileListeningForConnection(this ILogger logger, Exception exception, IPAddress iPAddress, int port);

        /// <summary>
        /// Writes the debug message <c>"Accepted an incoming client connection, there are now {NumberOfServices} connected clients"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Accepted an incoming client connection, there are now {NumberOfServices} connected clients")]
        internal static partial void DebugAcceptedIncommingConnection(this ILogger logger, int numberOfServices);

        /// <summary>
        /// Writes the warning message <c>"Reached the maximum number of simultaneously connected clients, further incoming connections will be blocked until one or more clients disconnect"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Reached the maximum number of simultaneously connected clients, further incoming connections will be blocked until one or more clients disconnect")]
        internal static partial void WaringReachedMaximumNumberOfConnections(this ILogger logger);

        /// <summary>
        /// Writes the warning message <c>"Cancellation occurred while accepting an incoming client connection"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Cancellation occurred while accepting an incoming client connection")]
        internal static partial void WaringAcceptingIncomingConnectionCanceled(this ILogger logger);

        /// <summary>
        /// Writes the error message <c>"An exception occurred while accepting an incoming client connection"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "An exception occurred while accepting an incoming client connection")]
        internal static partial void ErrorWhileAcceptionIncommingConnection(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the warning message <c>"DICOM server was canceled"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "DICOM server was canceled")]
        internal static partial void WarningServerWasCanceled(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Exception listening for DICOM services"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Exception listening for DICOM services")]
        internal static partial void ErrorOnServerListening(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the warning message <c>"An error occurred while trying to dispose a DICOM service"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "An error occurred while trying to dispose a DICOM service")]
        internal static partial void WarningErrorDisposingServer(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the warning message <c>"Waited {MaxClientsAllowedInterval}, but we still cannot accept another incoming connection because the maximum number of clients ({MaxClientsAllowed}) has been reached"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Waited {MaxClientsAllowedInterval}, but we still cannot accept another incoming connection because the maximum number of clients ({MaxClientsAllowed}) has been reached")]
        internal static partial void WarningMaxClientsAllowedReached(this ILogger logger, int maxClientsAllowed, TimeSpan maxClientsAllowedInterval);

        /// <summary>
        /// Writes the debug message <c>"Read 0 bytes from network stream while reading PDU header, connection will be marked as closed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Read 0 bytes from network stream while reading PDU header, connection will be marked as closed")]
        internal static partial void DebugNoDataFromConnectionStream(this ILogger logger);

        /// <summary>
        /// Writes the debug message <c>"An 'object disposed' exception occurred while listening to the network stream. This can happen when the connection is being closed."</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "An 'object disposed' exception occurred while listening to the network stream. This can happen when the connection is being closed.")]
        internal static partial void DebugConnectionDisposed(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the debug message <c>"A 'null reference' exception occurred while listening to the network stream. This can happen when the connection is already closed."</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "A 'null reference' exception occurred while listening to the network stream. This can happen when the connection is already closed.")]
        internal static partial void DebugConnectionNullReference(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the debug message <c>"Removing request [{MessageID}] from pending queue because an error occurred while sending it"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Debug, Message = "Removing request [{MessageID}] from pending queue because an error occurred while sending it")]
        internal static partial void DebugRemovingRequestFromQueueBecauseOfError(this ILogger logger, ushort messageID, Exception exception);

        /// <summary>
        /// Writes the information message <c>"{LogID} -> {pdu}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} -> {pdu}")]
        internal static partial void InformationDataPduSent(this ILogger logger, string logId, PDataTF pdu);

        /// <summary>
        /// Writes the information message <c>"{CallingAE} <- Association request:\n{Association}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{CallingAE} <- Association request:\n{Association}")]
        internal static partial void InformationAssociationRequestReceived(this ILogger logger, string callingAE, DicomAssociation association);

        /// <summary>
        /// Writes the information message <c>"{CalledAE} <- Association accept:\n{Association}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{CalledAE} <- Association accept:\n{Association}")]
        internal static partial void InformationAssociationAcceptReceived(this ILogger logger, string calledAE, DicomAssociation association);

        /// <summary>
        /// Writes the information message <c>"{LogID} <- Association reject [result: {Result}; source: {Source}; reason: {Reason}]"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Association reject [result: {Result}; source: {Source}; reason: {Reason}]")]
        internal static partial void InformationAssociationRejectedReceived(this ILogger logger, string logId, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Writes the information message <c>"{LogID} <- {@pdu}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- {@pdu}")]
        internal static partial void InformationDataPduReceived(this ILogger logger, string logId, PDataTF pdu);

        /// <summary>
        /// Writes the information message <c>"{LogID} <- Association release request"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Association release request")]
        internal static partial void InformationAssociationReleaseRequestReceived(this ILogger logger, string logId);

        /// <summary>
        /// Writes the information message <c>"{LogID} <- Association release response"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Association release response")]
        internal static partial void InformationAssociationReleaseResponseReceived(this ILogger logger, string logId);

        /// <summary>
        /// Writes the information message <c>"{LogID} <- Abort: {Source} - {Reason}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- Abort: {Source} - {Reason}")]
        internal static partial void InformationAbortReceived(this ILogger logger, string logId, DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Writes the information message <c>"{LogID} <- {DicomMessage}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} <- {DicomMessage}")]
        internal static partial void InformationDicomMessageReceived(this ILogger logger, string logId, string dicomMessage);

        /// <summary>
        /// Writes the information message <c>"{LogID} -> {DicomMessage}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogID} -> {DicomMessage}")]
        internal static partial void InformationDicomMessageSent(this ILogger logger, string logId, string dicomMessage);

        /// <summary>
        /// Writes the information message <c>"Tried to close connection but queues are not empty, PDUs: {pduCount}, messages: {msgCount}, pending requests: {pendingCount}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "Tried to close connection but queues are not empty, PDUs: {pduCount}, messages: {msgCount}, pending requests: {pendingCount}")]
        internal static partial void InformationTriedCloseConnectionWithNonemptyQueues(this ILogger logger, int pduCount, int msgCount, int pendingCount);

        /// <summary>
        /// Writes the information message <c>"Connection closed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "Connection closed")]
        internal static partial void InformationConnectionClosed(this ILogger logger);

        /// <summary>
        /// Writes the information message <c>"{CalledAE} -> Association request:\n{Association}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{CalledAE} -> Association request:\n{Association}")]
        internal static partial void InformationAssociationRequenstSent(this ILogger logger, string calledAE, DicomAssociation association);

        /// <summary>
        /// Writes the information message <c>"{LogId} -> Association accept:\n{Association}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{LogId} -> Association accept:\n{Association}")]
        internal static partial void InformationAssociationAcceptSent(this ILogger logger, string logId, DicomAssociation association);

        /// <summary>
        /// Writes the information message <c>"{logId} -> Association reject [result: {result}; source: {source}; reason: {reason}]"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Association reject [result: {result}; source: {source}; reason: {reason}]")]
        internal static partial void InformationAssociationRejectSent(this ILogger logger, string logId, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason);

        /// <summary>
        /// Writes the information message <c>"{logId} -> Association release request"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Association release request")]
        internal static partial void InformationAssociationReleaseRequestSent(this ILogger logger, string logId);

        /// <summary>
        /// Writes the information message <c>"{logId} -> Association release response"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Association release response")]
        internal static partial void InformationAssociationReleaseResponseSent(this ILogger logger, string logId);

        /// <summary>
        /// Writes the information message <c>"{logId} -> Abort [source: {source}; reason: {reason}]"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "{logId} -> Abort [source: {source}; reason: {reason}]")]
        internal static partial void InformationAbortSent(this ILogger logger, string logId, DicomAbortSource source, DicomAbortReason reason);

        /// <summary>
        /// Writes the information message <c>"Socket error while {operation} PDU: {socketError} [{errorCode}]"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "Socket error while {operation} PDU: {socketError} [{errorCode}]")]
        internal static partial void InformationSocketError(this ILogger logger, string operation, string socketError, int errorCode);

        /// <summary>
        /// Writes the information message <c>"Object disposed while {operation} PDU"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Information, Message = "Object disposed while {operation} PDU")]
        internal static partial void InformationDisposedPDU(this ILogger logger, string operation);

        /// <summary>
        /// Writes the error message <c>"I/O exception while {operation} PDU"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "I/O exception while {operation} PDU")]
        internal static partial void ErrorIOExceptionPDU(this ILogger logger, string operation, Exception exception);

        /// <summary>
        /// Writes the warning message <c>"DICOM service {DicomServiceType} was not disposed correctly, but was garbage collected instead"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "DICOM service {DicomServiceType} was not disposed correctly, but was garbage collected instead")]
        internal static partial void WarningDicomServiceNotDisposed(this ILogger logger, string dicomServiceType);

        /// <summary>
        /// Writes the warning message <c>"Unknown message type: {type}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Unknown message type: {type}")]
        internal static partial void WarningUnknownMessageType(this ILogger logger, DicomCommandField type);

        /// <summary>
        /// Writes the warning message <c>"Conversion of dataset transfer syntax from: {datasetSyntax} to: {acceptedSyntax} is not supported."</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Conversion of dataset transfer syntax from: {datasetSyntax} to: {acceptedSyntax} is not supported.")]
        internal static partial void WarningConversionOfTransfersyntaxNotSupported(this ILogger logger, DicomTransferSyntax datasetSyntax, DicomTransferSyntax acceptedSyntax);

        /// <summary>
        /// Writes the warning message <c>"Will attempt to transfer dataset as-is."</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Will attempt to transfer dataset as-is.")]
        internal static partial void WarningNoConversionOfTransfersyntax(this ILogger logger);

        /// <summary>
        /// Writes the warning message <c>"Pixel Data (7fe0,0010) is removed from dataset."</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Pixel Data (7fe0,0010) is removed from dataset.")]
        internal static partial void WarningPixelDataRemoved(this ILogger logger);

        /// <summary>
        /// Writes the warning message <c>"Request [{messageID}] timed out, removing from pending queue and triggering timeout callbacks"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Warning, Message = "Request [{messageID}] timed out, removing from pending queue and triggering timeout callbacks")]
        internal static partial void WarningRequestTimedOut(this ILogger logger, ushort messageID);

        /// <summary>
        /// Writes the error message <c>"An 'object disposed' exception occurred while writing the next PDU to the network stream. This can happen when the connection is being closed"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "An 'object disposed' exception occurred while writing the next PDU to the network stream. This can happen when the connection is being closed")]
        internal static partial void ErrorObjectDisposedWhileWritingToStream(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Exception sending PDU"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Exception sending PDU")]
        internal static partial void ErrorSendingPdu(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Exception processing PDU"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Exception processing PDU")]
        internal static partial void ErrorProcessingPdu(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Error parsing C-Store dataset"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Error parsing C-Store dataset")]
        internal static partial void ErrorParsingCStore(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Exception processing P-Data-TF PDU"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Exception processing P-Data-TF PDU")]
        internal static partial void ErrorProcessingDataPdu(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"An error occurred while sending a DICOM message"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred while sending a DICOM message")]
        internal static partial void ErrorSendingMessage(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"No accepted presentation context found for abstract syntax: {sopClassUid}"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "No accepted presentation context found for abstract syntax: {sopClassUid}")]
        internal static partial void ErrorNoPresentationContextForAbstractSyntax(this ILogger logger, DicomUID sopClassUid);

        /// <summary>
        /// Writes the error message <c>"An error occurred in the Fellow Oak DICOM timeout detection loop"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "An error occurred in the Fellow Oak DICOM timeout detection loop")]
        internal static partial void ErrorInTimoutDetection(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Error during close attempt"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Error during close attempt")]
        internal static partial void ErrorWhenClosingConnection(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Exception creating PDV"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Exception creating PDV")]
        internal static partial void ErrorCreatingPDV(this ILogger logger, Exception exception);

        /// <summary>
        /// Writes the error message <c>"Exception writing data to PDV"</c> to log
        /// </summary>
        [LoggerMessage(Level = LogLevel.Error, Message = "Exception writing data to PDV")]
        internal static partial void ErrorWritingPDV(this ILogger logger, Exception exception);

    }
}
