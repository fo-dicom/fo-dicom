// Copyright (c) 2012-2018 fo-dicom contributors.
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
        /// Gets whether or not the send queue is empty.
        /// </summary>
        public bool IsSendQueueEmpty
        {
            get
            {
                lock (_lock)
                {
                    return _msgQueue.Count == 0 && _pending.Count == 0;
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
            _pduQueueWatcher.Wait();

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
                    if (_writing) return;

                    if (_pduQueue.Count == 0) return;

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
                    TryCloseConnection(e, true);
                }
                catch (Exception e)
                {
                    Logger.Error("Exception sending PDU: {@error}", e);
                    TryCloseConnection(e);
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
                            TryCloseConnection();
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
                                TryCloseConnection();
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
                            await ((this as IDicomServiceProvider)?.OnReceiveAssociationRequestAsync(Association))
                                .ConfigureAwait(false);
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
                            (this as IDicomServiceUser)?.OnReceiveAssociationAccept(Association);
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
                            (this as IDicomServiceUser)?.OnReceiveAssociationReject(
                                pdu.Result,
                                pdu.Source,
                                pdu.Reason);
                            if (TryCloseConnection()) return;
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
                            await ((this as IDicomServiceProvider)?.OnReceiveAssociationReleaseRequestAsync())
                                .ConfigureAwait(false);

                            break;
                        }
                        case 0x06:
                        {
                            var pdu = new AReleaseRP();
                            pdu.Read(raw);
                            Logger.Info("{logId} <- Association release response", LogID);
                            (this as IDicomServiceUser)?.OnReceiveAssociationReleaseResponse();
                            if (TryCloseConnection()) return;
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
                            (this as IDicomService)?.OnReceiveAbort(pdu.Source, pdu.Reason);
                            if (TryCloseConnection()) return;
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
                    TryCloseConnection(force: true);
                }
                catch (NullReferenceException)
                {
                    // connection already closed; silently ignore
                    TryCloseConnection(force: true);
                }
                catch (IOException e)
                {
                    // LogIOException returns true for underlying socket error (probably due to forcibly closed connection), 
                    // in that case discard exception
                    TryCloseConnection(LogIOException(e, Logger, true) ? null : e, true);
                }
                catch (Exception e)
                {
                    Logger.Error("Exception processing PDU: {@error}", e);
                    TryCloseConnection(e, true);
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

                            var command = new DicomDataset();

                            var reader = new DicomReader();
                            reader.IsExplicitVR = false;
                            reader.Read(new StreamByteSource(_dimseStream), new DicomDatasetReaderObserver(command));

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

                                _dimse.Dataset = new DicomDataset();
                                _dimse.Dataset.InternalTransferSyntax = pc.AcceptedTransferSyntax;

                                var source = new StreamByteSource(_dimseStream);
                                source.Endian = pc.AcceptedTransferSyntax.Endian;

                                var reader = new DicomReader();
                                reader.IsExplicitVR = pc.AcceptedTransferSyntax.IsExplicitVR;
                                reader.Read(source, new DicomDatasetReaderObserver(_dimse.Dataset));

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
                                    (this as IDicomCStoreProvider).OnCStoreRequestException(
                                        _dimseStreamFile != null ? _dimseStreamFile.Name : null, e);
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

            if (!DicomMessage.IsRequest(dimse.Type))
            {
                var rsp = dimse as DicomResponse;
                DicomRequest req;
                lock (_lock)
                {
                    req = _pending.FirstOrDefault(x => x.MessageID == rsp.RequestMessageID);
                }

                if (req != null)
                {
                    rsp.UserState = req.UserState;
                    req.PostResponse(this, rsp);
                    if (rsp.Status.State != DicomState.Pending)
                    {
                        lock (_lock)
                        {
                            _pending.Remove(req);
                        }
                    }
                }

                return;
            }

            if (dimse.Type == DicomCommandField.CStoreRequest)
            {
                if (this is IDicomCStoreProvider)
                {
                    var response = (this as IDicomCStoreProvider).OnCStoreRequest(dimse as DicomCStoreRequest);
                    await SendResponseAsync(response).ConfigureAwait(false);
                    return;
                }

                if (this is IDicomServiceUser)
                {
                    var response = (this as IDicomServiceUser).OnCStoreRequest(dimse as DicomCStoreRequest);
                    await SendResponseAsync(response).ConfigureAwait(false);
                    return;
                }

                throw new DicomNetworkException("C-Store SCP not implemented");
            }

            if (dimse.Type == DicomCommandField.CFindRequest)
            {
                if (!(this is IDicomCFindProvider)) throw new DicomNetworkException("C-Find SCP not implemented");

                var responses = (this as IDicomCFindProvider).OnCFindRequest(dimse as DicomCFindRequest);
                foreach (var response in responses) await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.CGetRequest)
            {
                if (!(this is IDicomCGetProvider)) throw new DicomNetworkException("C-GET SCP not implemented");

                var responses = (this as IDicomCGetProvider).OnCGetRequest(dimse as DicomCGetRequest);
                foreach (var response in responses) await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.CMoveRequest)
            {
                if (!(this is IDicomCMoveProvider)) throw new DicomNetworkException("C-Move SCP not implemented");

                var responses = (this as IDicomCMoveProvider).OnCMoveRequest(dimse as DicomCMoveRequest);
                foreach (var response in responses) await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.CEchoRequest)
            {
                if (!(this is IDicomCEchoProvider)) throw new DicomNetworkException("C-Echo SCP not implemented");

                var response = (this as IDicomCEchoProvider).OnCEchoRequest(dimse as DicomCEchoRequest);
                await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.NActionRequest || dimse.Type == DicomCommandField.NCreateRequest
                || dimse.Type == DicomCommandField.NDeleteRequest
                || dimse.Type == DicomCommandField.NEventReportRequest
                || dimse.Type == DicomCommandField.NGetRequest || dimse.Type == DicomCommandField.NSetRequest)
            {
                if (!(this is IDicomNServiceProvider)) throw new DicomNetworkException("N-Service SCP not implemented");

                DicomResponse response = null;
                if (dimse.Type == DicomCommandField.NActionRequest)
                    response = (this as IDicomNServiceProvider).OnNActionRequest(dimse as DicomNActionRequest);
                else if (dimse.Type == DicomCommandField.NCreateRequest)
                    response = (this as IDicomNServiceProvider).OnNCreateRequest(dimse as DicomNCreateRequest);
                else if (dimse.Type == DicomCommandField.NDeleteRequest)
                    response =
                        (this as IDicomNServiceProvider).OnNDeleteRequest(dimse as DicomNDeleteRequest);
                else if (dimse.Type == DicomCommandField.NEventReportRequest)
                    response =
                        (this as IDicomNServiceProvider).OnNEventReportRequest(
                            dimse as DicomNEventReportRequest);
                else if (dimse.Type == DicomCommandField.NGetRequest)
                    response =
                        (this as IDicomNServiceProvider).OnNGetRequest(dimse as DicomNGetRequest);
                else if (dimse.Type == DicomCommandField.NSetRequest)
                    response =
                        (this as IDicomNServiceProvider).OnNSetRequest(
                            dimse as DicomNSetRequest);

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

        private async Task SendNextMessageAsync()
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

                    if (msg is DicomRequest)
                    {
                        _pending.Add(msg as DicomRequest);
                    }
                }

                await DoSendMessageAsync(msg).ConfigureAwait(false);

                lock (_lock) _sending = false;
            }

            if (sendQueueEmpty)
            {
                await OnSendQueueEmptyAsync().ConfigureAwait(false);
            }
        }

        private async Task DoSendMessageAsync(DicomMessage msg)
        {
            DicomPresentationContext pc;
            if (msg is DicomCStoreRequest)
            {
                pc =
                    Association.PresentationContexts.FirstOrDefault(
                        x =>
                            x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.SOPClassUID
                            && x.AcceptedTransferSyntax == (msg as DicomCStoreRequest).TransferSyntax);
                if (pc == null)
                    pc =
                        Association.PresentationContexts.FirstOrDefault(
                            x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.SOPClassUID);
            }
            else if (msg is DicomResponse)
            {
                //the presentation context should be set already from the request object
                pc = msg.PresentationContext;

                //fail safe if no presentation context is already assigned to the response (is this going to happen)
                if (pc == null)
                {
                    pc =
                        this.Association.PresentationContexts.FirstOrDefault<DicomPresentationContext>(
                            x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.SOPClassUID);
                }
            }
            else
            {
                pc =
                    Association.PresentationContexts.FirstOrDefault(
                        x => x.Result == DicomPresentationContextResult.Accept && x.AbstractSyntax == msg.SOPClassUID);
            }

            if (pc == null)
            {
                pc = msg.PresentationContext;
            }

            if (pc == null)
            {
                lock (_lock)
                {
                    _pending.Remove(msg as DicomRequest);
                }

                try
                {
                    if (msg is DicomCStoreRequest)
                        (msg as DicomCStoreRequest).PostResponse(
                            this,
                            new DicomCStoreResponse(msg as DicomCStoreRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomCEchoRequest)
                        (msg as DicomCEchoRequest).PostResponse(
                            this,
                            new DicomCEchoResponse(msg as DicomCEchoRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomCFindRequest)
                        (msg as DicomCFindRequest).PostResponse(
                            this,
                            new DicomCFindResponse(msg as DicomCFindRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomCGetRequest)
                        (msg as DicomCGetRequest).PostResponse(
                            this,
                            new DicomCGetResponse(msg as DicomCGetRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomCMoveRequest)
                        (msg as DicomCMoveRequest).PostResponse(
                            this,
                            new DicomCMoveResponse(msg as DicomCMoveRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomNActionRequest)
                        (msg as DicomNActionRequest).PostResponse(
                            this,
                            new DicomNActionResponse(msg as DicomNActionRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomNCreateRequest)
                        (msg as DicomNCreateRequest).PostResponse(
                            this,
                            new DicomNCreateResponse(msg as DicomNCreateRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomNDeleteRequest)
                        (msg as DicomNDeleteRequest).PostResponse(
                            this,
                            new DicomNDeleteResponse(msg as DicomNDeleteRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomNEventReportRequest)
                        (msg as DicomNEventReportRequest).PostResponse(
                            this,
                            new DicomNEventReportResponse(msg as DicomNEventReportRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomNGetRequest)
                        (msg as DicomNGetRequest).PostResponse(
                            this,
                            new DicomNGetResponse(msg as DicomNGetRequest, DicomStatus.SOPClassNotSupported));
                    else if (msg is DicomNSetRequest)
                        (msg as DicomNSetRequest).PostResponse(
                            this,
                            new DicomNSetResponse(msg as DicomNSetRequest, DicomStatus.SOPClassNotSupported));
                    else
                    {
                        Logger.Warn("Unknown message type: {type}", msg.Type);
                    }
                }
                catch
                {
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
                    stream = new PDataTFStream(this, pc.ID, Association.MaximumPDULength);

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
                    }
                }
            }
        }

        private bool TryCloseConnection(Exception exception = null, bool force = false)
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

                (this as IDicomService)?.OnConnectionClosed(exception);
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
            int errorCode;
            string errorDescriptor;
            if (NetworkManager.IsSocketException(e.InnerException, out errorCode, out errorDescriptor))
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

            private PDataTF _pdu;

            private byte[] _bytes;

            private int _length;

            #endregion

            #region Public Constructors

            public PDataTFStream(DicomService service, byte pcid, uint max)
            {
                _service = service;
                _command = true;
                _pcid = pcid;
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
                if (_length > 0) await CreatePDVAsync(last).ConfigureAwait(false);

                if (_pdu.PDVs.Count > 0)
                {
                    if (last) _pdu.PDVs[_pdu.PDVs.Count - 1].IsLastFragment = true;

                    await _service.SendPDUAsync(_pdu).ConfigureAwait(false);

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
