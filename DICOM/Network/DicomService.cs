// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if NET35

using System.Text;
using Dicom.Log;

namespace Dicom.Network
{
    /// <summary>
    /// Dummy base class for DICOM network services in Unity.
    /// </summary>
    public abstract class DicomService
    {
        #region CONSTRUCTORS

        protected DicomService(INetworkStream stream, Encoding fallbackEncoding, Logger log)
        {
        }

        #endregion

        #region Send Methods

        protected void SendAssociationRequest(DicomAssociation association) { }

        protected void SendAssociationAccept(DicomAssociation association) { }

        protected void SendAssociationReject(
            DicomRejectResult result,
            DicomRejectSource source,
            DicomRejectReason reason)
        { }

        protected void SendAssociationReleaseRequest() { }

        protected void SendAssociationReleaseResponse() { }

        protected void SendAbort(DicomAbortSource source, DicomAbortReason reason) { }

        #endregion
    }
}

#else

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dicom.Imaging.Codec;
using Dicom.IO;
using Dicom.IO.Reader;
using Dicom.IO.Writer;
using Dicom.Log;
using Dicom.Network.Client;

namespace Dicom.Network
{

	/// <summary>
	/// Delegate for passing byte array
	/// </summary>
	/// <param name="unsupportedBytes">byte array to be passed to the delegate.</param>
	public delegate void PDUBytesHandler(byte[] unsupportedBytes);

    /// <summary>
    /// Base class for DICOM network services.
    /// </summary>
    public abstract class DicomService : IDicomServiceRunner, IDisposable
    {
        #region FIELDS

        private const int MaxBytesToRead = 16384;

        private bool _disposed = false;

        private bool _isInitialized;

        private readonly INetworkStream _network;

        private readonly object _lock;

        private volatile bool _writing;

        private volatile bool _sending;

        private readonly Queue<PDU> _pduQueue;

        private readonly Queue<DicomMessage> _msgQueue;

        private readonly List<DicomRequest> _pending;

        private DicomMessage _dimse;

        private int _readLength;

        private readonly Encoding _fallbackEncoding;

        private readonly ManualResetEventSlim _pduQueueWatcher;

        protected readonly AsyncManualResetEvent _isDisconnectedFlag;

        protected Stream _dimseStream;

        protected IFileReference _dimseStreamFile;

        private int _isCheckingForTimeouts = 0;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomService"/> class.
        /// </summary>
        /// <param name="stream">Network stream.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="log">Logger</param>
        protected DicomService(INetworkStream stream, Encoding fallbackEncoding, Logger log)
        {
            _isDisconnectedFlag = new AsyncManualResetEvent();

            _network = stream;
            _lock = new object();
            _pduQueue = new Queue<PDU>();
            _pduQueueWatcher = new ManualResetEventSlim(true);
            _msgQueue = new Queue<DicomMessage>();
            _pending = new List<DicomRequest>();
            _fallbackEncoding = fallbackEncoding ?? DicomEncoding.Default;

            MaximumPDUsInQueue = 16;
            Logger = log ?? LogManager.GetLogger("Dicom.Network");
            Options = new DicomServiceOptions();

            _isInitialized = false;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the DICOM service options.
        /// </summary>
        public DicomServiceOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the log ID.
        /// </summary>
        private string LogID { get; set; }

        /// <summary>
        /// Gets or sets a user state associated with the service.
        /// </summary>
        public object UserState { get; set; }

        /// <summary>
        /// Gets the DICOM association.
        /// </summary>
        public DicomAssociation Association { get; internal set; }

        /// <summary>
        /// Gets whether or not the service is connected.
        /// </summary>
        public bool IsConnected => !_isDisconnectedFlag.IsSet;

        /// <summary>
        /// Gets whether or not both the message queue and the pending queue is empty.
        /// </summary>
        public bool IsSendQueueEmpty
        {
            get
            {
                bool isSendQueueEmpty;
                lock (_lock)
                {
                    isSendQueueEmpty = _msgQueue.Count == 0 && _pending.Count == 0;
                }
                return isSendQueueEmpty;
            }
        }

        /// <summary>
        /// Gets whether or not SendNextMessage is required, i.e. if any requests still have to be sent and there is no send loop currently running.
        /// </summary>
        public bool IsSendNextMessageRequired
        {
            get
            {
                lock (_lock)
                {
                    return _msgQueue.Count > 0 && !_sending;
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of PDUs in queue.
        /// </summary>
        public int MaximumPDUsInQueue { get; set; }

        /// <summary>
        /// Gets or sets an event handler to handle unsupported PDU bytes.
        /// </summary>
        public PDUBytesHandler DoHandlePDUBytes { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _dimseStream?.Dispose();
                _network?.Dispose();
                _pduQueueWatcher?.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        /// Send request from service.
        /// </summary>
        /// <param name="request">Request to send.</param>
        public virtual Task SendRequestAsync(DicomRequest request)
        {
            return SendMessageAsync(request);
        }

        /// <summary>
        /// Send response from service.
        /// </summary>
        /// <param name="response">Response to send.</param>
        protected Task SendResponseAsync(DicomResponse response)
        {
            return SendMessageAsync(response);
        }

        /// <summary>
        /// The purpose of this method is to return the Stream that a SopInstance received
        /// via CStoreSCP will be written to.  This default implementation creates a temporary
        /// file and returns a FileStream on top of it.  Child classes can override this to write
        /// to another stream and avoid the I/O associated with the temporary file if so desired.
        /// Beware that some SopInstances can be very large so using a MemoryStream() could cause
        /// out of memory situations.
        /// </summary>
        /// <param name="file">A DicomFile with FileMetaInfo populated.</param>
        /// <returns>The stream to write the SopInstance to.</returns>
        protected virtual void CreateCStoreReceiveStream(DicomFile file)
        {
            _dimseStreamFile = TemporaryFile.Create();

            _dimseStream = _dimseStreamFile.Open();
            file.Save(_dimseStream);
            _dimseStream.Seek(0, SeekOrigin.End);
        }

        /// <summary>
        /// The purpose of this method is to create a DicomFile for the SopInstance received via
        /// CStoreSCP to pass to the IDicomCStoreProvider.OnCStoreRequest method for processing.
        /// This default implementation will return a DicomFile if the stream created by
        /// CreateCStoreReceiveStream() is seekable or null if it is not.  Child classes that
        /// override CreateCStoreReceiveStream may also want override this to return a DicomFile
        /// for unseekable streams or to do cleanup related to receiving that specific instance.
        /// </summary>
        /// <returns>The DicomFile or null if the stream is not seekable.</returns>
        protected virtual DicomFile GetCStoreDicomFile()
        {
            if (_dimseStreamFile != null)
            {
                if (_dimseStream != null) _dimseStream.Dispose();
                return DicomFile.Open(_dimseStreamFile, _fallbackEncoding);
            }

            if (_dimseStream != null && _dimseStream.CanSeek)
            {
                _dimseStream.Seek(0, SeekOrigin.Begin);
                return DicomFile.Open(_dimseStream, _fallbackEncoding);
            }

            return null;
        }

        /// <summary>
        /// Asynchronously send single PDU.
        /// </summary>
        /// <param name="pdu">PDU to send.</param>
        /// <returns>Awaitable task.</returns>
        protected Task SendPDUAsync(PDU pdu)
        {
            try
            {
                _pduQueueWatcher.Wait();
            }
            catch (ObjectDisposedException)
            {
                // ignore ObjectDisposedException, that may happen, when closing a connection.
                return Task.FromResult(false); // TODO Replace with Task.CompletedTask when moving to .NET 4.6
            }

            lock (_lock)
            {
                _pduQueue.Enqueue(pdu);
                if (_pduQueue.Count >= MaximumPDUsInQueue) _pduQueueWatcher.Reset();
            }

            return SendNextPDUAsync();
        }

        private async Task SendNextPDUAsync()
        {
            while (IsConnected)
            {
                PDU pdu;

                lock (_lock)
                {
                    if (_writing)
                    {
                        return;
                    }

                    if (_pduQueue.Count == 0)
                    {
                        return;
                    }

                    _writing = true;

                    pdu = _pduQueue.Dequeue();
                    if (_pduQueue.Count < MaximumPDUsInQueue) _pduQueueWatcher.Set();
                }

                if (Options.LogDataPDUs && pdu is PDataTF) Logger.Info("{logId} -> {pdu}", LogID, pdu);

                try
                {
                    var ms = new MemoryStream();
                    pdu.Write().WritePDU(ms);

                    var buffer = ms.ToArray();

                    await _network.AsStream().WriteAsync(buffer, 0, (int)ms.Length).ConfigureAwait(false);
                }
                catch (IOException e)
                {
                    LogIOException(e, Logger, false);
                    await TryCloseConnectionAsync(e, true).ConfigureAwait(false);
                }
                catch (ObjectDisposedException e)
                {
                    // ignore ObjectDisposedException, that may happen, when closing a connection.
                }
                catch (Exception e)
                {
                    Logger.Error("Exception sending PDU: {@error}", e);
                    await TryCloseConnectionAsync(e).ConfigureAwait(false);
                }

                lock (_lock) _writing = false;
            }
        }

        private async Task ListenAndProcessPDUAsync()
        {
            while (IsConnected)
            {
                try
                {
                    var stream = _network.AsStream();

                    // Read PDU header
                    _readLength = 6;

                    var buffer = new byte[6];
                    var count = await stream.ReadAsync(buffer, 0, 6).ConfigureAwait(false);

                    do
                    {
                        if (count == 0)
                        {
                            // disconnected
                            await TryCloseConnectionAsync().ConfigureAwait(false);
                            return;
                        }

                        _readLength -= count;
                        if (_readLength > 0)
                        {
                            count = await stream.ReadAsync(buffer, 6 - _readLength, _readLength).ConfigureAwait(false);
                        }
                    } while (_readLength > 0);

                    var length = BitConverter.ToInt32(buffer, 2);
                    length = Endian.Swap(length);

                    _readLength = length;

                    // Read PDU
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(buffer, 0, buffer.Length);
                        while (_readLength > 0)
                        {
                            int bytesToRead = Math.Min(_readLength, MaxBytesToRead);
                            var tempBuffer = new byte[bytesToRead];
                            count = await stream.ReadAsync(tempBuffer, 0, bytesToRead)
                                .ConfigureAwait(false);

                            if (count == 0)
                            {
                                // disconnected
                                await TryCloseConnectionAsync().ConfigureAwait(false);
                                return;
                            }

                            ms.Write(tempBuffer, 0, count);

                            _readLength -= count;
                        }

                        buffer = ms.ToArray();
                    }

                    var raw = new RawPDU(buffer);

                    switch (raw.Type)
                    {
                        case 0x01:
                        {
                            Association = new DicomAssociation
                            {
                                RemoteHost = _network.RemoteHost,
                                RemotePort = _network.RemotePort
                            };

                            var pdu = new AAssociateRQ(Association);
                            if (DoHandlePDUBytes != null)
                            {
                                pdu.HandlePDUBytes += DoHandlePDUBytes;
                            }

                            pdu.Read(raw);
                            if (DoHandlePDUBytes != null)
                            {
                                pdu.HandlePDUBytes -= DoHandlePDUBytes;
                            }

                            LogID = Association.CallingAE;
                            if (Options.UseRemoteAEForLogName) Logger = LogManager.GetLogger(LogID);
                            Logger.Info(
                                "{callingAE} <- Association request:\n{association}",
                                LogID,
                                Association.ToString());
                            if (this is IDicomServiceProvider provider)
                                await provider.OnReceiveAssociationRequestAsync(Association).ConfigureAwait(false);
                            break;
                        }
                        case 0x02:
                        {
                            var pdu = new AAssociateAC(Association);
                            pdu.Read(raw);
                            LogID = Association.CalledAE;
                            Logger.Info(
                                "{calledAE} <- Association accept:\n{assocation}",
                                LogID,
                                Association.ToString());
                            if (this is IDicomServiceUser dicomServiceUser)
                                dicomServiceUser.OnReceiveAssociationAccept(Association);
                            if (this is IDicomClientConnection connection)
                                await connection.OnReceiveAssociationAcceptAsync(Association).ConfigureAwait(false);
                            break;
                        }
                        case 0x03:
                        {
                            var pdu = new AAssociateRJ();
                            pdu.Read(raw);
                            Logger.Info(
                                "{logId} <- Association reject [result: {pduResult}; source: {pduSource}; reason: {pduReason}]",
                                LogID,
                                pdu.Result,
                                pdu.Source,
                                pdu.Reason);
                            if (this is IDicomServiceUser user)
                                user.OnReceiveAssociationReject(pdu.Result, pdu.Source, pdu.Reason);
                            if (this is IDicomClientConnection connection)
                                await connection.OnReceiveAssociationRejectAsync(pdu.Result, pdu.Source, pdu.Reason).ConfigureAwait(false);
                            if (await TryCloseConnectionAsync().ConfigureAwait(false)) return;
                            break;
                        }
                        case 0x04:
                        {
                            var pdu = new PDataTF();
                            pdu.Read(raw);
                            if (Options.LogDataPDUs) Logger.Info("{logId} <- {@pdu}", LogID, pdu);
                            await ProcessPDataTFAsync(pdu).ConfigureAwait(false);
                            break;
                        }
                        case 0x05:
                        {
                            var pdu = new AReleaseRQ();
                            pdu.Read(raw);
                            Logger.Info("{logId} <- Association release request", LogID);
                            if(this is IDicomServiceProvider provider)
                                await provider.OnReceiveAssociationReleaseRequestAsync().ConfigureAwait(false);

                            break;
                        }
                        case 0x06:
                        {
                            var pdu = new AReleaseRP();
                            pdu.Read(raw);
                            Logger.Info("{logId} <- Association release response", LogID);
                            if (this is IDicomServiceUser user)
                                user.OnReceiveAssociationReleaseResponse();
                            if (this is IDicomClientConnection connection)
                                await connection.OnReceiveAssociationReleaseResponseAsync().ConfigureAwait(false);
                            if (await TryCloseConnectionAsync().ConfigureAwait(false)) return;
                            break;
                        }
                        case 0x07:
                        {
                            var pdu = new AAbort();
                            pdu.Read(raw);
                            Logger.Info(
                                "{logId} <- Abort: {pduSource} - {pduReason}",
                                LogID,
                                pdu.Source,
                                pdu.Reason);
                            if (this is IDicomService service)
                                service.OnReceiveAbort(pdu.Source, pdu.Reason);
                            else if (this is IDicomClientConnection connection)
                                await connection.OnReceiveAbortAsync(pdu.Source, pdu.Reason).ConfigureAwait(false);
                            if (await TryCloseConnectionAsync().ConfigureAwait(false)) return;
                            break;
                        }
                        case 0xFF:
                        {
                            break;
                        }
                        default:
                            throw new DicomNetworkException("Unknown PDU type");
                    }
                }
                catch (ObjectDisposedException)
                {
                    // silently ignore
                    await TryCloseConnectionAsync(force: true).ConfigureAwait(false);
                }
                catch (NullReferenceException)
                {
                    // connection already closed; silently ignore
                    await TryCloseConnectionAsync(force: true).ConfigureAwait(false);
                }
                catch (IOException e)
                {
                    // LogIOException returns true for underlying socket error (probably due to forcibly closed connection),
                    // in that case discard exception
                    await TryCloseConnectionAsync(LogIOException(e, Logger, true) ? null : e, true).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Logger.Error("Exception processing PDU: {@error}", e);
                    await TryCloseConnectionAsync(e, true).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Process P-DATA-TF PDUs.
        /// </summary>
        /// <param name="pdu">PDU to process.</param>
        private async Task ProcessPDataTFAsync(PDataTF pdu)
        {
            try
            {
                foreach (var pdv in pdu.PDVs)
                {
                    if (_dimse == null)
                    {
                        // create stream for receiving command
                        if (_dimseStream == null)
                        {
                            _dimseStream = new MemoryStream();
                            _dimseStreamFile = null;
                        }
                    }
                    else
                    {
                        // create stream for receiving dataset
                        if (_dimseStream == null)
                        {
                            if (_dimse.Type == DicomCommandField.CStoreRequest)
                            {
                                var pc = Association.PresentationContexts.FirstOrDefault(x => x.ID == pdv.PCID);

                                var file = new DicomFile();
                                file.FileMetaInfo.MediaStorageSOPClassUID = pc.AbstractSyntax;
                                file.FileMetaInfo.MediaStorageSOPInstanceUID = _dimse.Command.GetSingleValue<DicomUID>(DicomTag.AffectedSOPInstanceUID);
                                file.FileMetaInfo.TransferSyntax = pc.AcceptedTransferSyntax;
                                file.FileMetaInfo.ImplementationClassUID = Association.RemoteImplementationClassUID;
                                file.FileMetaInfo.ImplementationVersionName = Association.RemoteImplementationVersion;
                                file.FileMetaInfo.SourceApplicationEntityTitle = Association.CallingAE;

                                CreateCStoreReceiveStream(file);
                            }
                            else
                            {
                                _dimseStream = new MemoryStream();
                                _dimseStreamFile = null;
                            }
                        }
                    }

                    await _dimseStream.WriteAsync(pdv.Value, 0, pdv.Value.Length).ConfigureAwait(false);

                    if (pdv.IsLastFragment)
                    {
                        if (pdv.IsCommand)
                        {
                            _dimseStream.Seek(0, SeekOrigin.Begin);

                            var command = new DicomDataset().NotValidated();

                            var reader = new DicomReader();
                            reader.IsExplicitVR = false;
                            reader.Read(new StreamByteSource(_dimseStream, FileReadOption.Default), new DicomDatasetReaderObserver(command));

                            _dimseStream = null;
                            _dimseStreamFile = null;

                            var type = command.GetSingleValue<DicomCommandField>(DicomTag.CommandField);
                            switch (type)
                            {
                                case DicomCommandField.CStoreRequest:
                                    _dimse = new DicomCStoreRequest(command);
                                    break;
                                case DicomCommandField.CStoreResponse:
                                    _dimse = new DicomCStoreResponse(command);
                                    break;
                                case DicomCommandField.CFindRequest:
                                    _dimse = new DicomCFindRequest(command);
                                    break;
                                case DicomCommandField.CFindResponse:
                                    _dimse = new DicomCFindResponse(command);
                                    break;
                                case DicomCommandField.CGetRequest:
                                    _dimse = new DicomCGetRequest(command);
                                    break;
                                case DicomCommandField.CGetResponse:
                                    _dimse = new DicomCGetResponse(command);
                                    break;
                                case DicomCommandField.CMoveRequest:
                                    _dimse = new DicomCMoveRequest(command);
                                    break;
                                case DicomCommandField.CMoveResponse:
                                    _dimse = new DicomCMoveResponse(command);
                                    break;
                                case DicomCommandField.CEchoRequest:
                                    _dimse = new DicomCEchoRequest(command);
                                    break;
                                case DicomCommandField.CEchoResponse:
                                    _dimse = new DicomCEchoResponse(command);
                                    break;
                                case DicomCommandField.NActionRequest:
                                    _dimse = new DicomNActionRequest(command);
                                    break;
                                case DicomCommandField.NActionResponse:
                                    _dimse = new DicomNActionResponse(command);
                                    break;
                                case DicomCommandField.NCreateRequest:
                                    _dimse = new DicomNCreateRequest(command);
                                    break;
                                case DicomCommandField.NCreateResponse:
                                    _dimse = new DicomNCreateResponse(command);
                                    break;
                                case DicomCommandField.NDeleteRequest:
                                    _dimse = new DicomNDeleteRequest(command);
                                    break;
                                case DicomCommandField.NDeleteResponse:
                                    _dimse = new DicomNDeleteResponse(command);
                                    break;
                                case DicomCommandField.NEventReportRequest:
                                    _dimse = new DicomNEventReportRequest(command);
                                    break;
                                case DicomCommandField.NEventReportResponse:
                                    _dimse = new DicomNEventReportResponse(command);
                                    break;
                                case DicomCommandField.NGetRequest:
                                    _dimse = new DicomNGetRequest(command);
                                    break;
                                case DicomCommandField.NGetResponse:
                                    _dimse = new DicomNGetResponse(command);
                                    break;
                                case DicomCommandField.NSetRequest:
                                    _dimse = new DicomNSetRequest(command);
                                    break;
                                case DicomCommandField.NSetResponse:
                                    _dimse = new DicomNSetResponse(command);
                                    break;
                                default:
                                    _dimse = new DicomMessage(command);
                                    break;
                            }
                            _dimse.PresentationContext =
                                Association.PresentationContexts.FirstOrDefault(x => x.ID == pdv.PCID);
                            if (!_dimse.HasDataset)
                            {
                                await PerformDimseAsync(_dimse).ConfigureAwait(false);
                                _dimse = null;
                                return;
                            }
                        }
                        else
                        {
                            if (_dimse.Type != DicomCommandField.CStoreRequest)
                            {
                                _dimseStream.Seek(0, SeekOrigin.Begin);

                                var pc = Association.PresentationContexts.FirstOrDefault(x => x.ID == pdv.PCID);

                                _dimse.Dataset = new DicomDataset { InternalTransferSyntax = pc.AcceptedTransferSyntax };

                                var source = new StreamByteSource(_dimseStream, FileReadOption.Default);
                                source.Endian = pc.AcceptedTransferSyntax.Endian;

                                var reader = new DicomReader { IsExplicitVR = pc.AcceptedTransferSyntax.IsExplicitVR };

                                // when receiving data via network, accept it and dont validate
                                using (var unvalidated = new UnvalidatedScope(_dimse.Dataset))
                                {
                                    reader.Read(source, new DicomDatasetReaderObserver(_dimse.Dataset));
                                }

                                _dimseStream = null;
                                _dimseStreamFile = null;
                            }
                            else
                            {
                                var request = _dimse as DicomCStoreRequest;

                                try
                                {
                                    var dicomFile = GetCStoreDicomFile();
                                    _dimseStream = null;
                                    _dimseStreamFile = null;

                                    // NOTE: dicomFile will be valid with the default implementation of CreateCStoreReceiveStream() and
                                    // GetCStoreDicomFile(), but can be null if a child class overrides either method and changes behavior.
                                    // See documentation on CreateCStoreReceiveStream() and GetCStoreDicomFile() for information about why
                                    // this might be desired.
                                    request.File = dicomFile;
                                    if (request.File != null)
                                    {
                                        request.Dataset = request.File.Dataset;
                                    }
                                }
                                catch (Exception e)
                                {
                                    // failed to parse received DICOM file; send error response instead of aborting connection
                                    await SendResponseAsync(new DicomCStoreResponse(request,
                                            new DicomStatus(DicomStatus.ProcessingFailure, e.Message)))
                                        .ConfigureAwait(false);

                                    Logger.Error("Error parsing C-Store dataset: {@error}", e);
                                    (this as IDicomCStoreProvider).OnCStoreRequestException(_dimseStreamFile?.Name, e);
                                    return;
                                }
                            }

                            await PerformDimseAsync(this._dimse).ConfigureAwait(false);
                            _dimse = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception processing P-Data-TF PDU: {@error}", e);
                throw;
            }
            finally
            {
                await SendNextMessageAsync().ConfigureAwait(false);
            }
        }

        private async Task PerformDimseAsync(DicomMessage dimse)
        {
            Logger.Info("{logId} <- {dicomMessage}", LogID, dimse.ToString(Options.LogDimseDatasets));

            if (!DicomMessage.IsRequest(dimse.Type) && dimse is DicomResponse rsp)
            {
                DicomRequest req;
                lock (_lock)
                {
                    req = _pending.FirstOrDefault(x => x.MessageID == rsp.RequestMessageID);
                }

                if (req != null)
                {
                    try
                    {
                        rsp.UserState = req.UserState;
                        req.PostResponse(this, rsp);
                    }
                    finally
                    {
                        if (rsp.Status.State != DicomState.Pending)
                        {
                            lock (_lock)
                            {
                                _pending.Remove(req);
                            }

                            if (this is IDicomClientConnection connection)
                            {
                                await connection.OnRequestCompletedAsync(req, rsp).ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            req.LastPendingResponseReceived = DateTime.Now;
                        }
                    }
                }

                return;
            }

            if (dimse.Type == DicomCommandField.CStoreRequest)
            {
                if (this is IDicomCStoreProvider thisDicomCStoreProvider)
                {
                    var response = thisDicomCStoreProvider.OnCStoreRequest(dimse as DicomCStoreRequest);
                    await SendResponseAsync(response).ConfigureAwait(false);
                    return;
                }

                if (this is IDicomServiceUser thisDicomServiceUser)
                {
                    var response = thisDicomServiceUser.OnCStoreRequest(dimse as DicomCStoreRequest);
                    await SendResponseAsync(response).ConfigureAwait(false);
                    return;
                }

                if (this is IDicomClientConnection connection)
                {
                    var response = await connection.OnCStoreRequestAsync(dimse as DicomCStoreRequest).ConfigureAwait(false);
                    await SendResponseAsync(response).ConfigureAwait(false);
                    return;
                }

                throw new DicomNetworkException("C-Store SCP not implemented");
            }

            if (dimse.Type == DicomCommandField.CFindRequest)
            {
                var thisAsCFindProvider = this as IDicomCFindProvider ?? throw new DicomNetworkException("C-Find SCP not implemented");

                var responses = thisAsCFindProvider.OnCFindRequest(dimse as DicomCFindRequest);
                foreach (var response in responses) await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.CGetRequest)
            {
                var thisAsCGetProvider = this as IDicomCGetProvider ?? throw new DicomNetworkException("C-GET SCP not implemented");

                var responses = thisAsCGetProvider.OnCGetRequest(dimse as DicomCGetRequest);
                foreach (var response in responses) await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.CMoveRequest)
            {
                var thisAsCMoveProvider = this as IDicomCMoveProvider ?? throw new DicomNetworkException("C-Move SCP not implemented");

                var responses = thisAsCMoveProvider.OnCMoveRequest(dimse as DicomCMoveRequest);
                foreach (var response in responses) await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.CEchoRequest)
            {
                var thisAsCEchoProvider = this as IDicomCEchoProvider ?? throw new DicomNetworkException("C-Echo SCP not implemented");

                var response = thisAsCEchoProvider.OnCEchoRequest(dimse as DicomCEchoRequest);
                await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.NActionRequest || dimse.Type == DicomCommandField.NCreateRequest
                || dimse.Type == DicomCommandField.NDeleteRequest
                || dimse.Type == DicomCommandField.NEventReportRequest
                || dimse.Type == DicomCommandField.NGetRequest || dimse.Type == DicomCommandField.NSetRequest)
            {
                var thisAsNServiceProvider = this as IDicomNServiceProvider ?? throw new DicomNetworkException("N-Service SCP not implemented");

                DicomResponse response = null;
                switch (dimse.Type)
                {
                    case DicomCommandField.NActionRequest:
                        response = thisAsNServiceProvider.OnNActionRequest(dimse as DicomNActionRequest);
                        break;
                    case DicomCommandField.NCreateRequest:
                        response = thisAsNServiceProvider.OnNCreateRequest(dimse as DicomNCreateRequest);
                        break;
                    case DicomCommandField.NDeleteRequest:
                        response = thisAsNServiceProvider.OnNDeleteRequest(dimse as DicomNDeleteRequest);
                        break;
                    case DicomCommandField.NEventReportRequest:
                        response = thisAsNServiceProvider.OnNEventReportRequest(dimse as DicomNEventReportRequest);
                        break;
                    case DicomCommandField.NGetRequest:
                        response = thisAsNServiceProvider.OnNGetRequest(dimse as DicomNGetRequest);
                        break;
                    case DicomCommandField.NSetRequest:
                        response = thisAsNServiceProvider.OnNSetRequest(dimse as DicomNSetRequest);
                        break;
                }

                await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            throw new DicomNetworkException("Operation not implemented");
        }

        private Task SendMessageAsync(DicomMessage message)
        {
            lock (_lock)
            {
                _msgQueue.Enqueue(message);
            }

            return SendNextMessageAsync();
        }

        internal async Task SendNextMessageAsync()
        {
            var sendQueueEmpty = false;

            while (true)
            {
                DicomMessage msg;
                lock (_lock)
                {
                    if (_sending)
                    {
                        break;
                    }

                    if (_msgQueue.Count == 0)
                    {
                        if (_pending.Count == 0) sendQueueEmpty = true;
                        break;
                    }

                    if (Association.MaxAsyncOpsInvoked > 0
                        && _pending.Count(req => req.Type != DicomCommandField.CGetRequest)
                        >= Association.MaxAsyncOpsInvoked)
                    {
                        break;
                    }

                    _sending = true;

                    msg = _msgQueue.Dequeue();

                    if (msg is DicomRequest dicomRequest)
                    {
                        _pending.Add(dicomRequest);

                        dicomRequest.PendingSince = DateTime.Now;

#pragma warning disable 4014 This call should not be awaited because it can only complete when the pending queue is empty
                        Task.Factory.StartNew(CheckForTimeouts, TaskCreationOptions.LongRunning).ConfigureAwait(false);
#pragma warning restore 4014
                    }
                }

                try
                {
                    await DoSendMessageAsync(msg).ConfigureAwait(false);
                }
                finally
                {
                    lock (_lock) _sending = false;
                }
            }

            if (sendQueueEmpty)
            {
                await OnSendQueueEmptyAsync().ConfigureAwait(false);
            }
        }

        private async Task DoSendMessageAsync(DicomMessage msg)
        {
            DicomPresentationContext pc;
            if (msg is DicomCStoreRequest dicomCStoreRequest)
            {
                pc =
                    Association.PresentationContexts.FirstOrDefault(
                        x =>
                            x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.SOPClassUID
                            && x.AcceptedTransferSyntax == dicomCStoreRequest.TransferSyntax);
                if (pc == null)
                    pc = Association.PresentationContexts.FirstOrDefault(
                            x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.SOPClassUID);
            }
            else if (msg is DicomResponse)
            {
                //the presentation context should be set already from the request object
                pc = msg.PresentationContext;

                //fail safe if no presentation context is already assigned to the response (is this going to happen)
                if (pc == null)
                {
                    pc = Association.PresentationContexts.FirstOrDefault(
                            x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.SOPClassUID);
                }
            }
            else
            {
                var metaSopClasses = MetaSopClasses.GetMetaSopClass(msg.SOPClassUID);
                pc = Association.PresentationContexts.FirstOrDefault(
                        x => x.Result == DicomPresentationContextResult.Accept && metaSopClasses.Contains(x.AbstractSyntax));
            }

            if (pc == null)
            {
                pc = msg.PresentationContext;
            }

            if (pc == null)
            {
                var request = msg as DicomRequest;

                lock (_lock)
                {
                    _pending.Remove(request);
                }

                try
                {
                    DicomResponse response;

                    switch (msg)
                    {
                        case DicomCStoreRequest cStoreRequest:
                            response = new DicomCStoreResponse(cStoreRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomCEchoRequest cEchoRequest:
                            response = new DicomCEchoResponse(cEchoRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomCFindRequest cFindRequest:
                            response = new DicomCFindResponse(cFindRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomCGetRequest cGetRequest:
                            response = new DicomCGetResponse(cGetRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomCMoveRequest cMoveRequest:
                            response = new DicomCMoveResponse(cMoveRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomNActionRequest nActionRequest:
                            response = new DicomNActionResponse(nActionRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomNCreateRequest nCreateRequest:
                            response = new DicomNCreateResponse(nCreateRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomNDeleteRequest nDeleteRequest:
                            response = new DicomNDeleteResponse(nDeleteRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomNEventReportRequest nEventReportRequest:
                            response = new DicomNEventReportResponse(nEventReportRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomNGetRequest nGetRequest:
                            response = new DicomNGetResponse(nGetRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        case DicomNSetRequest nSetRequest:
                            response = new DicomNSetResponse(nSetRequest, DicomStatus.SOPClassNotSupported);
                            break;
                        default:
                            response = null;
                            Logger.Warn("Unknown message type: {type}", msg.Type);
                            break;

                    }

                    if (response != null)
                    {
                        request.PostResponse(this, response);
                    }

                    if (this is IDicomClientConnection connection)
                    {
                        await connection.OnRequestCompletedAsync(request, response).ConfigureAwait(false);
                    }
                }
                catch(Exception e)
                {
                    Logger.Error("Exception in DoSendMessageAsync: {error}", e);
                }

                Logger.Error("No accepted presentation context found for abstract syntax: {sopClassUid}", msg.SOPClassUID);
            }
            else
            {
                // force calculation of command group length as required by standard
                msg.Command.RecalculateGroupLengths();

                if (msg.HasDataset)
                {
                    // remove group lengths as recommended in PS 3.5 7.2
                    //
                    //	2. It is recommended that Group Length elements be removed during storage or transfer
                    //	   in order to avoid the risk of inconsistencies arising during coercion of data
                    //	   element values and changes in transfer syntax.
                    msg.Dataset.RemoveGroupLengths();

                    if (msg.Dataset.InternalTransferSyntax != pc.AcceptedTransferSyntax)
                    {
                        var changeTransferSyntax = true;

                        if (!TranscoderManager.CanTranscode(msg.Dataset.InternalTransferSyntax,
                                pc.AcceptedTransferSyntax) && msg.Dataset.Contains(DicomTag.PixelData))
                        {
                            Logger.Warn(
                                "Conversion of dataset transfer syntax from: {datasetSyntax} to: {acceptedSyntax} is not supported.",
                                msg.Dataset.InternalTransferSyntax, pc.AcceptedTransferSyntax);

                            if (Options.IgnoreUnsupportedTransferSyntaxChange)
                            {
                                Logger.Warn("Will attempt to transfer dataset as-is.");
                                changeTransferSyntax = false;
                            }
                            else
                            {
                                Logger.Warn("Pixel Data (7fe0,0010) is removed from dataset.");
                                msg.Dataset = msg.Dataset.Clone().Remove(DicomTag.PixelData);
                            }
                        }

                        if (changeTransferSyntax)
                        {
                            msg.Dataset = msg.Dataset.Clone(pc.AcceptedTransferSyntax);
                        }
                    }
                }

                Logger.Info("{logId} -> {dicomMessage}", LogID, msg.ToString(Options.LogDimseDatasets));

                PDataTFStream stream = null;
                try
                {
                    stream = new PDataTFStream(this, pc.ID, Association.MaximumPDULength, msg);

                    var writer = new DicomWriter(
                        DicomTransferSyntax.ImplicitVRLittleEndian,
                        DicomWriteOptions.Default,
                        new StreamByteTarget(stream));

                    var commandWalker = new DicomDatasetWalker(msg.Command);
                    await commandWalker.WalkAsync(writer).ConfigureAwait(false);

                    if (msg.HasDataset)
                    {
                        await stream.SetIsCommandAsync(false).ConfigureAwait(false);

                        writer = new DicomWriter(
                            pc.AcceptedTransferSyntax,
                            DicomWriteOptions.Default,
                            new StreamByteTarget(stream));

                        var datasetWalker = new DicomDatasetWalker(msg.Dataset);
                        await datasetWalker.WalkAsync(writer).ConfigureAwait(false);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error("Exception sending DIMSE: {@error}", e);
                }
                finally
                {
                    if (stream != null)
                    {
                        await stream.FlushAsync(CancellationToken.None).ConfigureAwait(false);
                        stream.Dispose();
                        msg.LastPDUSent = DateTime.Now;
                    }
                }
            }
        }

        private async Task CheckForTimeouts()
        {
            if (Options?.RequestTimeout == null)
                return;

            var requestTimeout = Options.RequestTimeout.Value;

            if (Interlocked.CompareExchange(ref _isCheckingForTimeouts, 1, 0) != 0)
                return;

            List<DicomRequest> timedOutPendingRequests;
            lock (_lock)
            {
                if (!_pending.Any())
                    return;

                timedOutPendingRequests = _pending.Where(p => p.IsTimedOut(requestTimeout)).ToList();
            }

            if (timedOutPendingRequests.Any())
            {
                for (var i = timedOutPendingRequests.Count - 1; i >= 0; i--)
                {
                    DicomRequest timedOutPendingRequest = timedOutPendingRequests[i];
                    try
                    {
                        Logger.Warn($"Request [{timedOutPendingRequest.MessageID}] timed out, removing from pending queue and triggering timeout callbacks");
                        timedOutPendingRequest.OnTimeout?.Invoke(timedOutPendingRequest, new DicomRequest.OnTimeoutEventArgs(requestTimeout));
                    }
                    finally
                    {
                        lock (_lock)
                        {
                            _pending.Remove(timedOutPendingRequest);
                        }

                        if (this is IDicomClientConnection connection)
                        {
                            await connection.OnRequestTimedOutAsync(timedOutPendingRequest, requestTimeout).ConfigureAwait(false);
                        }
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            _isCheckingForTimeouts = 0;

            await CheckForTimeouts().ConfigureAwait(false);
        }

        private async Task<bool> TryCloseConnectionAsync(Exception exception = null, bool force = false)
        {
            try
            {
                if (!IsConnected) return true;

                lock (_lock)
                {
                    if (force)
                    {
                        _pduQueue.Clear();
                        _msgQueue.Clear();
                        _pending.Clear();
                    }

                    if (_pduQueue.Count > 0 || _msgQueue.Count > 0 || _pending.Count > 0)
                    {
                        Logger.Info(
                            "Queue(s) not empty, PDUs: {pduCount}, messages: {msgCount}, pending requests: {pendingCount}",
                            _pduQueue.Count,
                            _msgQueue.Count,
                            _pending.Count);
                        return false;
                    }
                }

                if(this is IDicomService dicomService)
                    dicomService.OnConnectionClosed(exception);
                else if (this is IDicomClientConnection connection)
                    await connection.OnConnectionClosedAsync(exception).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.Error("Error during close attempt: {@error}", e);
                throw;
            }

            lock (_lock) _isDisconnectedFlag.Set();

            Logger.Info("Connection closed");

            if (exception != null) throw exception;
            return true;
        }

        private bool IsStillPending(DicomRequest request)
        {
            lock (_lock)
            {
                return _pending.Contains(request);
            }
        }

        #endregion

        #region Send Methods

        /// <summary>
        /// Send association request.
        /// </summary>
        /// <param name="association">DICOM association.</param>
        protected Task SendAssociationRequestAsync(DicomAssociation association)
        {
            LogID = association.CalledAE;
            if (Options.UseRemoteAEForLogName) Logger = LogManager.GetLogger(LogID);

            Logger.Info("{calledAE} -> Association request:\n{association}", LogID, association.ToString());

            Association = association;
            return SendPDUAsync(new AAssociateRQ(Association));
        }

        /// <summary>
        /// Send association accept response.
        /// </summary>
        /// <param name="association">DICOM association.</param>
        protected Task SendAssociationAcceptAsync(DicomAssociation association)
        {
            Association = association;

            // reject all presentation contexts that have not already been accepted or rejected
            foreach (var pc in Association.PresentationContexts)
            {
                if (pc.Result == DicomPresentationContextResult.Proposed) pc.SetResult(DicomPresentationContextResult.RejectNoReason);
            }

            Logger.Info("{logId} -> Association accept:\n{association}", LogID, association.ToString());

            return SendPDUAsync(new AAssociateAC(Association));
        }

        /// <summary>
        /// Send association reject response.
        /// </summary>
        /// <param name="result">Rejection result.</param>
        /// <param name="source">Rejection source.</param>
        /// <param name="reason">Rejection reason.</param>
        protected Task SendAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            Logger.Info("{logId} -> Association reject [result: {result}; source: {source}; reason: {reason}]", LogID,
                result, source, reason);
            return SendPDUAsync(new AAssociateRJ(result, source, reason));
        }

        /// <summary>
        /// Send association release request.
        /// </summary>
        protected Task SendAssociationReleaseRequestAsync()
        {
            Logger.Info("{logId} -> Association release request", LogID);
            return SendPDUAsync(new AReleaseRQ());
        }

        /// <summary>
        /// Send association release response.
        /// </summary>
        protected Task SendAssociationReleaseResponseAsync()
        {
            Logger.Info("{logId} -> Association release response", LogID);
            return SendPDUAsync(new AReleaseRP());
        }

        /// <summary>
        /// Send abort request.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Abort reason.</param>
        protected Task SendAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            Logger.Info("{logId} -> Abort [source: {source}; reason: {reason}]", LogID, source, reason);
            return SendPDUAsync(new AAbort(source, reason));
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Setup long-running operations that the DICOM service manages.
        /// </summary>
        /// <returns>Awaitable task maintaining the long-running operation(s).</returns>
        public virtual Task RunAsync()
        {
            if (_isInitialized) return Task.FromResult(false); // TODO Replace with Task.CompletedTask when moving to .NET 4.6
            _isInitialized = true;

            return ListenAndProcessPDUAsync();
        }

        /// <summary>
        /// Action to perform when send queue is empty.
        /// </summary>
        protected virtual Task OnSendQueueEmptyAsync()
        {
            return Task.FromResult(false); // TODO Replace with Task.CompletedTask when moving to .NET 4.6
        }

        #endregion

        #region Helper methods

        private static bool LogIOException(Exception e, Logger logger, bool reading)
        {
            if (NetworkManager.IsSocketException(e.InnerException, out int errorCode, out string errorDescriptor))
            {
                logger.Info(
                    $"Socket error while {(reading ? "reading" : "writing")} PDU: {{socketError}} [{{errorCode}}]",
                    errorDescriptor,
                    errorCode);
                return true;
            }

            if (e.InnerException is ObjectDisposedException)
            {
                logger.Info($"Object disposed while {(reading ? "reading" : "writing")} PDU: {{@error}}", e);
            }
            else
            {
                logger.Error($"I/O exception while {(reading ? "reading" : "writing")} PDU: {{@error}}", e);
            }

            return false;
        }

        #endregion

        #region INNER TYPES

        private class PDataTFStream : Stream
        {
            #region Private Members

            private readonly DicomService _service;

            private bool _command;

            private readonly uint _pduMax;

            private uint _max;

            private readonly byte _pcid;

            private readonly DicomMessage _dicomMessage;

            private PDataTF _pdu;

            private byte[] _bytes;

            private int _length;

            #endregion

            #region Public Constructors

            public PDataTFStream(DicomService service, byte pcid, uint max, DicomMessage dicomMessage)
            {
                _service = service;
                _command = true;
                _pcid = pcid;
                _dicomMessage = dicomMessage;
                _pduMax = Math.Min(max, Int32.MaxValue);
                _max = _pduMax == 0
                           ? _service.Options.MaxCommandBuffer
                           : Math.Min(_pduMax, _service.Options.MaxCommandBuffer);

                _pdu = new PDataTF();

                // Max PDU Size - Current Size - Size of PDV header
                _bytes = new byte[_max - CurrentPduSize() - 6];
            }

            #endregion

            #region Public Properties

            public async Task SetIsCommandAsync(bool value)
            {
                // recalculate maximum PDU buffer size
                if (_command != value)
                {
                    _max = _pduMax == 0
                        ? _service.Options.MaxCommandBuffer
                        : Math.Min(
                            _pduMax,
                            value ? _service.Options.MaxCommandBuffer : _service.Options.MaxDataBuffer);

                    await CreatePDVAsync(true).ConfigureAwait(false);
                    _command = value;
                }
            }

            #endregion

            #region Private Members

            private uint CurrentPduSize()
            {
                // PDU header + PDV header + PDV data
                return 6 + _pdu.GetLengthOfPDVs();
            }

            private async Task CreatePDVAsync(bool last)
            {
                try
                {
                    if (_bytes == null) _bytes = new byte[0];

                    if (_length < _bytes.Length) Array.Resize(ref _bytes, _length);

                    PDV pdv = new PDV(_pcid, _bytes, _command, last);
                    _pdu.PDVs.Add(pdv);

                    // reset length in case we recurse into WritePDU()
                    _length = 0;
                    // is the current PDU at its maximum size or do we have room for another PDV?
                    if (_service.Options.MaxPDVsPerPDU != 0 && _pdu.PDVs.Count >= _service.Options.MaxPDVsPerPDU
                        || CurrentPduSize() + 6 >= _max || !_command && last)
                    {
                        await WritePDUAsync(last).ConfigureAwait(false);
                    }

                    // Max PDU Size - Current Size - Size of PDV header
                    uint max = _max - CurrentPduSize() - 6;
                    _bytes = last ? null : new byte[max];
                }
                catch (Exception e)
                {
                    _service.Logger.Error("Exception creating PDV: {@error}", e);
                    throw;
                }
            }

            private async Task WritePDUAsync(bool last)
            {
                // Immediately stop sending PDUs if the message is no longer pending (e.g. because it timed out)
                if (_dicomMessage is DicomRequest req && !_service.IsStillPending(req))
                {
                    _pdu = new PDataTF();
                    return;
                }

                if (_length > 0) await CreatePDVAsync(last).ConfigureAwait(false);

                if (_pdu.PDVs.Count > 0)
                {
                    if (last) _pdu.PDVs[_pdu.PDVs.Count - 1].IsLastFragment = true;

                    await _service.SendPDUAsync(_pdu).ConfigureAwait(false);

                    _dicomMessage.LastPDUSent = DateTime.Now;

                    _pdu = new PDataTF();
                }
            }

            #endregion

            #region Stream Members

            public override bool CanRead => false;

            public override bool CanSeek => false;

            public override bool CanWrite => true;

            public override void Flush()
            {
            }

            public override long Length
            {
                get
                {
                    throw new NotSupportedException();
                }
            }

            public override long Position
            {
                get
                {
                    throw new NotSupportedException();
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                try
                {
                    WriteAsync(buffer, offset, count, CancellationToken.None).Wait();
                }
                catch (AggregateException e)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    throw e.Flatten().InnerException;
                }
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override async Task WriteAsync(byte[] buffer, int offset, int count,
                CancellationToken cancellationToken)
            {
                try
                {
                    if (_bytes == null || _bytes.Length == 0)
                    {
                        // Max PDU Size - Current Size - Size of PDV header
                        uint max = _max - CurrentPduSize() - 6;
                        _bytes = new byte[max];
                    }

                    while (count >= _bytes.Length - _length)
                    {
                        var c = Math.Min(count, _bytes.Length - _length);

                        Array.Copy(buffer, offset, _bytes, _length, c);

                        _length += c;
                        offset += c;
                        count -= c;

                        await CreatePDVAsync(false).ConfigureAwait(false);
                    }

                    if (count > 0)
                    {
                        Array.Copy(buffer, offset, _bytes, _length, count);
                        _length += count;

                        if (_bytes.Length == _length) await CreatePDVAsync(false).ConfigureAwait(false);
                    }
                }
                catch (Exception e)
                {
                    _service.Logger.Error("Exception writing data to PDV: {@error}", e);
                    throw;
                }
            }

            public override async Task FlushAsync(CancellationToken cancellationToken)
            {
                await CreatePDVAsync(true).ConfigureAwait(false);
                await WritePDUAsync(true).ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}

#endif
