// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.IO;
using FellowOakDicom.IO.Reader;
using FellowOakDicom.IO.Writer;
using FellowOakDicom.Memory;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tools;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
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

        private const int _maxBytesToRead = 16384;

        private long _isDisposed;

        private bool _isInitialized;

        private readonly INetworkStream _network;
        
        private readonly IMemoryProvider _memoryProvider;

        private readonly Stream _writeStream;

        private readonly object _lock;

        private volatile bool _writing;

        private volatile bool _sending;

        private readonly Queue<PDU> _pduQueue;

        private readonly Queue<DicomMessage> _msgQueue;

        private readonly List<DicomRequest> _pending;

        private DicomMessage _dimse;

        private int _bytesToRead;

        private readonly Encoding _fallbackEncoding;

        private readonly ManualResetEventSlim _pduQueueWatcher;

        protected readonly TaskCompletionSource<bool> _isDisconnectedFlag;

        protected Stream _dimseStream;

        protected IFileReference _dimseStreamFile;

        private int _isCheckingForTimeouts = 0;
        
        private bool _canStillProcessPDataTF;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomService"/> class.
        /// </summary>
        /// <param name="stream">Network stream.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger</param>
        /// <param name="dependencies">The dependencies of this DICOM service</param>
        protected DicomService(
            INetworkStream stream,
            Encoding fallbackEncoding,
            ILogger logger,
            DicomServiceDependencies dependencies)
        {
            _isDisconnectedFlag = TaskCompletionSourceFactory.Create<bool>();
            _canStillProcessPDataTF = true;
            _isInitialized = false;
            _network = stream;
            _memoryProvider = dependencies.MemoryProvider;
            _writeStream = new BufferedStream(_network.AsStream());
            _lock = new object();
            _pduQueue = new Queue<PDU>();
            _pduQueueWatcher = new ManualResetEventSlim(true);
            _msgQueue = new Queue<DicomMessage>();
            _pending = new List<DicomRequest>();
            _fallbackEncoding = fallbackEncoding;

            MaximumPDUsInQueue = 16;

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            LoggerFactory = dependencies.LoggerFactory ?? throw new ArgumentNullException(nameof(dependencies.LoggerFactory));
            NetworkManager = dependencies.NetworkManager ?? throw new ArgumentNullException(nameof(dependencies.NetworkManager));
            TranscoderManager = dependencies.TranscoderManager ?? throw new ArgumentNullException(nameof(dependencies.TranscoderManager));

            Options = new DicomServiceOptions();
        }
        
        #endregion
        
        #region FINALIZER 
        
        /// <summary>
        /// The finalizer will be called when this instance is not disposed properly.
        /// </summary>
        /// <remarks>Failing to dispose indicates wrong usage</remarks>
        ~DicomService() => Dispose(false);
        
        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

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
        public bool IsConnected => !_isDisconnectedFlag.Task.IsCompleted;

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
        /// Gets whether or not the connection can still process P-DATA-TF
        /// </summary>
        public bool CanStillProcessPDataTF => _canStillProcessPDataTF;

        /// <summary>
        /// Gets or sets the maximum number of PDUs in queue.
        /// </summary>
        public int MaximumPDUsInQueue { get; set; }

        /// <summary>
        /// Gets or sets an event handler to handle unsupported PDU bytes.
        /// </summary>
        public PDUBytesHandler DoHandlePDUBytes { get; set; }

        /// <summary>
        /// The network manager being used by this DICOM service
        /// </summary>
        private INetworkManager NetworkManager { get; }

        /// <summary>
        /// The log manager being used by this DICOM service
        /// </summary>
        private ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// The transcoder manager being used by this DICOM service
        /// </summary>
        private ITranscoderManager TranscoderManager { get; }
        
        #endregion

        #region METHODS
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfAlreadyDisposed()
        {
            if (Interlocked.Read(ref _isDisposed) == 0)
            {
                return;
            }

            ThrowDisposedException();
        }
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowDisposedException() => throw new ObjectDisposedException("This DICOM service is already disposed and can no longer be used");

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Guard against multiple concurrent disposals
            if (Interlocked.CompareExchange(ref _isDisposed, 1, 0) != 0)
            {
                return;
            }

            if (disposing)
            {
                _dimseStream?.Dispose();
                try
                {
                    _writeStream?.Dispose();
                }
                catch(IOException)
                {
                    // The buffered stream will try to flush its contents upon disposal, which might fail if the underlying network stream is already closed
                    // This can be ignored here
                }
                _network?.Dispose();
                _pduQueueWatcher?.Dispose();
            }
            else
            {
                Logger.LogWarning("DICOM service {DicomServiceType} was not disposed correctly, but was garbage collected instead", GetType().FullName);
            }
        }

        /// <summary>
        /// Send request from service.
        /// </summary>
        /// <param name="request">Request to send.</param>
        public virtual Task SendRequestAsync(DicomRequest request)
        {
            ThrowIfAlreadyDisposed();
            
            return SendMessageAsync(request);
        }

        /// <summary>
        /// Send response from service.
        /// </summary>
        /// <param name="response">Response to send.</param>
        protected Task SendResponseAsync(DicomResponse response)
        {
            ThrowIfAlreadyDisposed();
            
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
                if (_dimseStream != null)
                {
                    _dimseStream.Dispose();
                }

                return DicomFile.Open(_dimseStreamFile, _fallbackEncoding ?? DicomEncoding.Default);
            }

            if (_dimseStream != null && _dimseStream.CanSeek)
            {
                _dimseStream.Seek(0, SeekOrigin.Begin);
                return DicomFile.Open(_dimseStream, _fallbackEncoding ?? DicomEncoding.Default);
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
            if (!IsConnected)
            {
                throw new DicomNetworkException("Cannot send PDU because the connection to the DICOM server is lost");
            }
            
            try
            {
                while (IsConnected && !_pduQueueWatcher.Wait(60 * 1000))
                {
                    // Every minute, we check whether it still makes sense to wait for the PDU queue watcher flag 
                    // by verifying we still have a connection
                }
            }
            catch (ObjectDisposedException)
            {
                // When the DICOM service is disposed, the _pduQueueWatcher is also disposed
                throw new DicomNetworkException("Cannot send PDU because the association has already been disposed");
            }
            
            if (!IsConnected)
            {
                throw new DicomNetworkException("Cannot send PDU because the connection to the DICOM server is lost");
            }

            lock (_lock)
            {
                if (pdu is PDataTF && !_canStillProcessPDataTF)
                {
                    throw new DicomNetworkException(
                        "Cannot write P-DATA-TF over current DICOM association because a previous P-DATA-TF timed out before it was sent completely"
                    );
                }
                
                _pduQueue.Enqueue(pdu);
                if (_pduQueue.Count >= MaximumPDUsInQueue)
                {
                    _pduQueueWatcher.Reset();
                }
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
                    if (_pduQueue.Count < MaximumPDUsInQueue)
                    {
                        _pduQueueWatcher.Set();
                    }
                }
                
                if (pdu is PDataTF && !_canStillProcessPDataTF)
                {
                    throw new DicomNetworkException(
                        "Cannot write P-DATA-TF over current DICOM association because a previous P-DATA-TF timed out before it was sent completely"
                    );
                }

                if (Options.LogDataPDUs && pdu is PDataTF)
                {
                    Logger.LogInformation("{logId} -> {pdu}", LogID, pdu);
                }

                try
                {
                    await pdu.WriteAsync(_writeStream, CancellationToken.None).ConfigureAwait(false);
                    await _writeStream.FlushAsync(CancellationToken.None).ConfigureAwait(false);
                }
                catch (IOException e)
                {
                    LogIOException(e, Logger, false);
                    await TryCloseConnectionAsync(e, true).ConfigureAwait(false);
                    throw new DicomNetworkException("An IO exception occurred while sending a PDU", e);
                }
                catch (ObjectDisposedException e)
                {
                    // This may happen when closing a connection.
                    Logger.LogError(e, "An 'object disposed' exception occurred while writing the next PDU to the network stream. " +
                                 "This can happen when the connection is being closed");
                    throw new DicomNetworkException("This DICOM service was disposed while sending a PDU", e);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Exception sending PDU");
                    await TryCloseConnectionAsync(e, true).ConfigureAwait(false);
                    throw new DicomNetworkException("An exception occurred while sending a PDU", e);
                }
                finally
                {
                    lock (_lock)
                    {
                        _writing = false;
                    }
                }
            }
        }

        private async Task ListenAndProcessPDUAsync()
        {
            while (IsConnected)
            {
                try
                {
                    var stream = _network.AsStream();

                    // Read common fields of the PDU header. The first 6 bytes contain the type and the length
                    _bytesToRead = RawPDU.CommonFieldsLength;

                    // This is the (extremely small) buffer we use to read the raw PDU header
                    using var rawPduCommonFieldsBuffer = _memoryProvider.Provide(RawPDU.CommonFieldsLength);
                    
                    var count = await stream.ReadAsync(rawPduCommonFieldsBuffer.Bytes, 0, rawPduCommonFieldsBuffer.Length).ConfigureAwait(false);

                    do
                    {
                        if (count == 0)
                        {
                            // disconnected
                            Logger.LogDebug("Read 0 bytes from network stream while reading PDU header, connection will be marked as closed");
                            await TryCloseConnectionAsync(force: true).ConfigureAwait(false);
                            return;
                        }

                        _bytesToRead -= count;
                        
                        if (_bytesToRead > 0)
                        {
                            count = await stream.ReadAsync(rawPduCommonFieldsBuffer.Bytes, rawPduCommonFieldsBuffer.Length - _bytesToRead, _bytesToRead).ConfigureAwait(false);
                        }
                    }
                    while (_bytesToRead > 0);
                    
                    // The first byte contains the PDU type
                    // The second byte is reserved
                    // The remaining four bytes contain the PDU length
                    var pduTypeByte = rawPduCommonFieldsBuffer.Bytes[0];
                    if (!Enum.IsDefined(typeof(RawPduType), pduTypeByte))
                    {
                        throw new DicomNetworkException("Unknown PDU type: " + pduTypeByte);
                    }
                    var pduLength = BitConverter.ToInt32(rawPduCommonFieldsBuffer.Bytes, 2);
                    pduLength = Endian.Swap(pduLength);

                    _bytesToRead = pduLength;

                    // Read PDU
                    var rawPduLength = pduLength + RawPDU.CommonFieldsLength;
                    
                    // This is the buffer that will hold the entire Raw PDU at once
                    using var rawPduBuffer = _memoryProvider.Provide(rawPduLength);
                    
                    Array.Copy(rawPduCommonFieldsBuffer.Bytes, 0, rawPduBuffer.Bytes, 0, RawPDU.CommonFieldsLength);
                    int rawPduOffset = RawPDU.CommonFieldsLength;
                    while (_bytesToRead > 0)
                    {
                        int bytesToRead = Math.Min(_bytesToRead, _maxBytesToRead);

                        count = await stream.ReadAsync(rawPduBuffer.Bytes, rawPduOffset, bytesToRead).ConfigureAwait(false);

                        if (count == 0)
                        {
                            // disconnected
                            Logger.LogDebug("Read 0 bytes from network stream while reading PDU, connection will be marked as closed");
                            await TryCloseConnectionAsync(force: true).ConfigureAwait(false);
                            return;
                        }

                        rawPduOffset += count;
                        _bytesToRead -= count;
                    }

                    using var rawPduStream = new MemoryStream(rawPduBuffer.Bytes, 0, rawPduLength);
                    using var raw = new RawPDU(rawPduStream, _memoryProvider);

                    switch (raw.Type)
                    {
                        case RawPduType.A_ASSOCIATE_RQ:
                            {
                                Association = new DicomAssociation
                                {
                                    RemoteHost = _network.RemoteHost,
                                    RemotePort = _network.RemotePort, 
                                    Options = Options
                                };

                                var pdu = new AAssociateRQ(Association, _memoryProvider);
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
                                if (Options.UseRemoteAEForLogName)
                                {
                                    Logger = LoggerFactory.CreateLogger(LogID);
                                }

                                Logger.LogInformation(
                                    "{CallingAE} <- Association request:\n{Association}",
                                    LogID,
                                    Association);
                                if (this is IDicomServiceProvider provider)
                                {
                                    await provider.OnReceiveAssociationRequestAsync(Association).ConfigureAwait(false);
                                }

                                break;
                            }
                        case RawPduType.A_ASSOCIATE_AC:
                            {
                                var pdu = new AAssociateAC(Association, _memoryProvider);
                                pdu.Read(raw);
                                LogID = Association.CalledAE;
                                Logger.LogInformation(
                                    "{CalledAE} <- Association accept:\n{Assocation}",
                                    LogID,
                                    Association);
                                if (this is IDicomClientConnection connection)
                                {
                                    await connection.OnReceiveAssociationAcceptAsync(Association).ConfigureAwait(false);
                                }

                                break;
                            }
                        case RawPduType.A_ASSOCIATE_RJ:
                            {
                                var pdu = new AAssociateRJ(_memoryProvider);
                                pdu.Read(raw);
                                Logger.LogInformation(
                                    "{logId} <- Association reject [result: {pduResult}; source: {pduSource}; reason: {pduReason}]",
                                    LogID,
                                    pdu.Result,
                                    pdu.Source,
                                    pdu.Reason);

                                if (this is IDicomClientConnection connection)
                                {
                                    await connection.OnReceiveAssociationRejectAsync(pdu.Result, pdu.Source, pdu.Reason).ConfigureAwait(false);
                                }

                                if (await TryCloseConnectionAsync().ConfigureAwait(false))
                                {
                                    return;
                                }

                                break;
                            }
                        case RawPduType.P_DATA_TF:
                            {
                                using var pdu = new PDataTF(_memoryProvider);
                                pdu.Read(raw);
                                if (Options.LogDataPDUs)
                                {
                                    Logger.LogInformation("{logId} <- {@pdu}", LogID, pdu);
                                }

                                await ProcessPDataTFAsync(pdu).ConfigureAwait(false);
                                break;
                            }
                        case RawPduType.A_RELEASE_RQ:
                            {
                                var pdu = new AReleaseRQ(_memoryProvider);
                                pdu.Read(raw);
                                Logger.LogInformation("{logId} <- Association release request", LogID);
                                if (this is IDicomServiceProvider provider)
                                {
                                    await provider.OnReceiveAssociationReleaseRequestAsync().ConfigureAwait(false);
                                }

                                break;
                            }
                        case RawPduType.A_RELEASE_RP:
                            {
                                var pdu = new AReleaseRP(_memoryProvider);
                                pdu.Read(raw);
                                Logger.LogInformation("{logId} <- Association release response", LogID);
                                if (this is IDicomClientConnection connection)
                                {
                                    await connection.OnReceiveAssociationReleaseResponseAsync().ConfigureAwait(false);
                                }

                                if (await TryCloseConnectionAsync().ConfigureAwait(false))
                                {
                                    return;
                                }

                                break;
                            }
                        case RawPduType.A_ABORT:
                            {
                                var pdu = new AAbort(_memoryProvider);
                                pdu.Read(raw);
                                Logger.LogInformation(
                                    "{logId} <- Abort: {pduSource} - {pduReason}",
                                    LogID,
                                    pdu.Source,
                                    pdu.Reason);
                                if (this is IDicomService service)
                                {
                                    service.OnReceiveAbort(pdu.Source, pdu.Reason);
                                }
                                else if (this is IDicomClientConnection connection)
                                {
                                    await connection.OnReceiveAbortAsync(pdu.Source, pdu.Reason).ConfigureAwait(false);
                                }

                                if (await TryCloseConnectionAsync().ConfigureAwait(false))
                                {
                                    return;
                                }

                                break;
                            }
                        default:
                            throw new DicomNetworkException("Unknown PDU type");
                    }
                }
                catch (ObjectDisposedException e)
                {
                    // silently ignore
                    Logger.LogDebug(e, "An 'object disposed' exception occurred while listening to the network stream. " +
                                 "This can happen when the connection is being closed. ");
                    await TryCloseConnectionAsync(force: true).ConfigureAwait(false);
                }
                catch (NullReferenceException e)
                {
                    // connection already closed; silently ignore
                    Logger.LogDebug(e, "A 'null reference' exception occurred while listening to the network stream. " +
                                 "This can happen when the connection is already closed. ");
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
                    Logger.LogError(e, "Exception processing PDU");
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
                                if (_fallbackEncoding != null)
                                {
                                    file.Dataset.FallbackEncodings = new[] { _fallbackEncoding };
                                }
                                file.FileMetaInfo.MediaStorageSOPClassUID = pc.AbstractSyntax;
                                file.FileMetaInfo.MediaStorageSOPInstanceUID = _dimse.Command.GetSingleValue<DicomUID>(DicomTag.AffectedSOPInstanceUID);
                                file.FileMetaInfo.TransferSyntax = pc.AcceptedTransferSyntax;
                                file.FileMetaInfo.ImplementationClassUID = Association.RemoteImplementationClassUID ?? DicomImplementation.ClassUID;
                                file.FileMetaInfo.ImplementationVersionName = Association.RemoteImplementationVersion ?? DicomImplementation.Version;
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

                    await _dimseStream.WriteAsync(pdv.Value.Bytes, 0, pdv.Value.Length).ConfigureAwait(false);

                    if (pdv.IsLastFragment)
                    {
                        if (pdv.IsCommand)
                        {
                            _dimseStream.Seek(0, SeekOrigin.Begin);

                            var command = new DicomDataset().NotValidated();

                            var reader = new DicomReader(_memoryProvider) { IsExplicitVR = false };
                            reader.Read(StreamByteSourceFactory.Create(_dimseStream, FileReadOption.Default),
                                new DicomDatasetReaderObserver(command, _fallbackEncoding ?? DicomEncoding.Default));

                            _dimseStream = null;
                            _dimseStreamFile = null;

                            var type = command.GetSingleValue<DicomCommandField>(DicomTag.CommandField);
                            _dimse = type switch
                            {
                                DicomCommandField.CStoreRequest => new DicomCStoreRequest(command),
                                DicomCommandField.CStoreResponse => new DicomCStoreResponse(command),
                                DicomCommandField.CFindRequest => new DicomCFindRequest(command),
                                DicomCommandField.CFindResponse => new DicomCFindResponse(command),
                                DicomCommandField.CGetRequest => new DicomCGetRequest(command),
                                DicomCommandField.CGetResponse => new DicomCGetResponse(command),
                                DicomCommandField.CMoveRequest => new DicomCMoveRequest(command),
                                DicomCommandField.CMoveResponse => new DicomCMoveResponse(command),
                                DicomCommandField.CEchoRequest => new DicomCEchoRequest(command),
                                DicomCommandField.CEchoResponse => new DicomCEchoResponse(command),
                                DicomCommandField.NActionRequest => new DicomNActionRequest(command),
                                DicomCommandField.NActionResponse => new DicomNActionResponse(command),
                                DicomCommandField.NCreateRequest => new DicomNCreateRequest(command),
                                DicomCommandField.NCreateResponse => new DicomNCreateResponse(command),
                                DicomCommandField.NDeleteRequest => new DicomNDeleteRequest(command),
                                DicomCommandField.NDeleteResponse => new DicomNDeleteResponse(command),
                                DicomCommandField.NEventReportRequest => new DicomNEventReportRequest(command),
                                DicomCommandField.NEventReportResponse => new DicomNEventReportResponse(command),
                                DicomCommandField.NGetRequest => new DicomNGetRequest(command),
                                DicomCommandField.NGetResponse => new DicomNGetResponse(command),
                                DicomCommandField.NSetRequest => new DicomNSetRequest(command),
                                DicomCommandField.NSetResponse => new DicomNSetResponse(command),
                                _ => new DicomMessage(command),
                            };
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
                                
                                var source = new StreamByteSource(_dimseStream, FileReadOption.Default)
                                {
                                    Endian = pc.AcceptedTransferSyntax.Endian
                                };

                                var reader = new DicomReader(_memoryProvider) { IsExplicitVR = pc.AcceptedTransferSyntax.IsExplicitVR };

                                // when receiving data via network, accept it and dont validate
                                using var unvalidated = new UnvalidatedScope(_dimse.Dataset);
                                reader.Read(source, new DicomDatasetReaderObserver(_dimse.Dataset, _fallbackEncoding ?? DicomEncoding.Default));

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
                                    string errorComment = e.Message;
                                    if (errorComment.Length > DicomVR.LO.MaximumLength)
                                    {
                                        errorComment = errorComment.Substring(0, (int) DicomVR.LO.MaximumLength - 2) + "..";
                                    }
                                    await SendResponseAsync(new DicomCStoreResponse(request, new DicomStatus(DicomStatus.ProcessingFailure, errorComment))).ConfigureAwait(false);

                                    Logger.LogError(e, "Error parsing C-Store dataset");
                                    await (this as IDicomCStoreProvider)?.OnCStoreRequestExceptionAsync(_dimseStreamFile?.Name, e);
                                    return;
                                }
                            }

                            await PerformDimseAsync(_dimse).ConfigureAwait(false);
                            _dimse = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Exception processing P-Data-TF PDU");
                throw;
            }
            finally
            {
                await SendNextMessageAsync().ConfigureAwait(false);
            }
        }

        private async Task PerformDimseAsync(DicomMessage dimse)
        {
            if (Logger.IsEnabled(LogLevel.Information))
            {
                Logger.LogInformation("{LogId} <- {DicomMessage}", LogID, dimse.ToString(Options.LogDimseDatasets));
            }

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
                            
                            if (this is IDicomClientConnection connection)
                            {
                                await connection.OnRequestPendingAsync(req, rsp).ConfigureAwait(false);
                            }
                        }
                    }
                }

                return;
            }

            if (dimse.Type == DicomCommandField.CStoreRequest)
            {
                if (this is IDicomCStoreProvider thisDicomCStoreProvider)
                {
                    var response = await thisDicomCStoreProvider.OnCStoreRequestAsync(dimse as DicomCStoreRequest).ConfigureAwait(false);
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

                var asyncResponses = thisAsCFindProvider.OnCFindRequestAsync(dimse as DicomCFindRequest);
                await foreach (var response in asyncResponses.ConfigureAwait(false))
                {
                    await SendResponseAsync(response).ConfigureAwait(false);
                }

                return;
            }

            if (dimse.Type == DicomCommandField.CGetRequest)
            {
                var thisAsCGetProvider = this as IDicomCGetProvider ?? throw new DicomNetworkException("C-GET SCP not implemented");

                var asyncResponses = thisAsCGetProvider.OnCGetRequestAsync(dimse as DicomCGetRequest);
                await foreach (var response in asyncResponses.ConfigureAwait(false))
                {
                    await SendResponseAsync(response).ConfigureAwait(false);
                }

                return;
            }

            if (dimse.Type == DicomCommandField.CMoveRequest)
            {
                var thisAsCMoveProvider = this as IDicomCMoveProvider ?? throw new DicomNetworkException("C-Move SCP not implemented");

                var asyncResponses = thisAsCMoveProvider.OnCMoveRequestAsync(dimse as DicomCMoveRequest);
                await foreach (var response in asyncResponses.ConfigureAwait(false))
                {
                    await SendResponseAsync(response).ConfigureAwait(false);
                }

                return;
            }

            if (dimse.Type == DicomCommandField.CEchoRequest)
            {
                var thisAsCEchoProvider = this as IDicomCEchoProvider ?? throw new DicomNetworkException("C-Echo SCP not implemented");

                var response = await thisAsCEchoProvider.OnCEchoRequestAsync(dimse as DicomCEchoRequest).ConfigureAwait(false);
                await SendResponseAsync(response).ConfigureAwait(false);
                return;
            }

            if (dimse.Type == DicomCommandField.NActionRequest || dimse.Type == DicomCommandField.NCreateRequest
                || dimse.Type == DicomCommandField.NDeleteRequest
                || dimse.Type == DicomCommandField.NEventReportRequest
                || dimse.Type == DicomCommandField.NGetRequest || dimse.Type == DicomCommandField.NSetRequest)
            {
                if (this is IDicomNServiceProvider thisAsNServiceProvider)
                {
                    DicomResponse response = null;
                    switch (dimse.Type)
                    {
                        case DicomCommandField.NActionRequest:
                            response = await thisAsNServiceProvider.OnNActionRequestAsync(dimse as DicomNActionRequest).ConfigureAwait(false);
                            break;
                        case DicomCommandField.NCreateRequest:
                            response = await thisAsNServiceProvider.OnNCreateRequestAsync(dimse as DicomNCreateRequest).ConfigureAwait(false);
                            break;
                        case DicomCommandField.NDeleteRequest:
                            response = await thisAsNServiceProvider.OnNDeleteRequestAsync(dimse as DicomNDeleteRequest).ConfigureAwait(false);
                            break;
                        case DicomCommandField.NEventReportRequest:
                            response = await thisAsNServiceProvider.OnNEventReportRequestAsync(dimse as DicomNEventReportRequest).ConfigureAwait(false);
                            break;
                        case DicomCommandField.NGetRequest:
                            response = await thisAsNServiceProvider.OnNGetRequestAsync(dimse as DicomNGetRequest).ConfigureAwait(false);
                            break;
                        case DicomCommandField.NSetRequest:
                            response = await thisAsNServiceProvider.OnNSetRequestAsync(dimse as DicomNSetRequest).ConfigureAwait(false);
                            break;
                    }

                    await SendResponseAsync(response).ConfigureAwait(false);

                    if ((dimse.Type == DicomCommandField.NActionRequest) &&
                        (this is IDicomNEventReportRequestProvider thisAsAsyncNEventReportRequestProvider))
                    {
                        await thisAsAsyncNEventReportRequestProvider.OnSendNEventReportRequestAsync(dimse as DicomNActionRequest).ConfigureAwait(false);
                    }

                    return;
                }

                if (this is IDicomClientConnection thisAsConnection)
                {
                    switch (dimse.Type)
                    {
                        case DicomCommandField.NEventReportRequest:
                            var response = await thisAsConnection.OnNEventReportRequestAsync(dimse as DicomNEventReportRequest).ConfigureAwait(false);
                            await SendResponseAsync(response).ConfigureAwait(false);
                            break;
                    }
                    return;
                }

                throw new DicomNetworkException("N-Service SCP not implemented");
            }

            throw new DicomNetworkException("Operation not implemented");
        }

        private Task SendMessageAsync(DicomMessage message)
        {
            if (message == null)
            {
                return Task.CompletedTask;
            }

            lock (_lock)
            {
                _msgQueue.Enqueue(message);
            }

            return SendNextMessageAsync();
        }

        internal async Task SendNextMessageAsync()
        {
            ThrowIfAlreadyDisposed();
            
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
                        if (_pending.Count == 0)
                        {
                            sendQueueEmpty = true;
                        }

                        break;
                    }

                    if (Association.MaxAsyncOpsInvoked > 0
                        && _pending.Count(req => req.Type != DicomCommandField.CGetRequest && req.Type != DicomCommandField.NActionRequest)
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

                        // This call should not be awaited because it can only complete when the pending queue is empty
#pragma warning disable 4014 
                        Task.Factory.StartNew(CheckForTimeouts, TaskCreationOptions.LongRunning).ConfigureAwait(false);
#pragma warning restore 4014
                    }
                }

                try
                {
                    await DoSendMessageAsync(msg).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Failed to send DICOM message");

                    if (msg is DicomRequest dicomRequest)
                    {
                        Logger.LogDebug("Removing request [{MessageID}] from pending queue because an error occurred while sending it", dicomRequest.MessageID);

                        lock (_lock)
                        {
                            _pending.Remove(dicomRequest);
                        }
                    }

                    throw;
                }
                finally
                {
                    lock (_lock)
                    {
                        _sending = false;
                    }
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
                var accpetedPcs = Association.PresentationContexts
                    .Where(x =>
                        x.Result == DicomPresentationContextResult.Accept &&
                        x.AbstractSyntax == msg.SOPClassUID
                    );

                pc = accpetedPcs.FirstOrDefault(x => x.AcceptedTransferSyntax == dicomCStoreRequest.TransferSyntax)
                    ?? accpetedPcs.FirstOrDefault();
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
                            Logger.LogWarning("Unknown message type: {type}", msg.Type);
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
                catch (Exception e)
                {
                    Logger.LogError(e, "An error occurred while sending a DICOM message");
                }

                Logger.LogError("No accepted presentation context found for abstract syntax: {sopClassUid}", msg.SOPClassUID);

                msg.NotAllPDUsWereSentSuccessfully();
            }
            else
            {
                // force calculation of command group length as required by standard
                if (_fallbackEncoding != null && msg.HasDataset)
                {
                    msg.Dataset.FallbackEncodings = new[] { _fallbackEncoding };
                }
                msg.Command.OnBeforeSerializing();
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

                        var transcoderManager = TranscoderManager;
                        if (!transcoderManager.CanTranscode(msg.Dataset.InternalTransferSyntax,
                                pc.AcceptedTransferSyntax) && msg.Dataset.Contains(DicomTag.PixelData))
                        {
                            Logger.LogWarning(
                                "Conversion of dataset transfer syntax from: {datasetSyntax} to: {acceptedSyntax} is not supported.",
                                msg.Dataset.InternalTransferSyntax, pc.AcceptedTransferSyntax);

                            if (Options.IgnoreUnsupportedTransferSyntaxChange)
                            {
                                Logger.LogWarning("Will attempt to transfer dataset as-is.");
                                changeTransferSyntax = false;
                            }
                            else
                            {
                                Logger.LogWarning("Pixel Data (7fe0,0010) is removed from dataset.");
                                msg.Dataset = msg.Dataset.Clone().Remove(DicomTag.PixelData);
                            }
                        }

                        if (changeTransferSyntax)
                        {
                            msg.Dataset = msg.Dataset.Clone(pc.AcceptedTransferSyntax);
                        }
                    }
                }

                if (!IsConnected)
                {
                    throw new DicomNetworkException($"Failed to send {msg} because the connection to the DICOM server was lost");
                }

                Logger.LogInformation("{logId} -> {dicomMessage}", LogID, msg.ToString(Options.LogDimseDatasets));

                // This specialized Stream will write byte contents as PDUs with nested PDVs
                PDataTFStream pDataStream = null;
                // When the accepted transfer syntax is deflated, we must deflate the DICOM data set (not the command!)
                DeflateStream deflateStream = null;
                try
                {
                    pDataStream = new PDataTFStream(this, _memoryProvider, pc.ID, Association.MaximumPDULength, msg);

                    var writer = new DicomWriter(
                        DicomTransferSyntax.ImplicitVRLittleEndian,
                        DicomWriteOptions.Default,
                        new StreamByteTarget(pDataStream));

                    var commandWalker = new DicomDatasetWalker(msg.Command);
                    await commandWalker.WalkAsync(writer).ConfigureAwait(false);

                    if (msg.HasDataset)
                    {
                        await pDataStream.SetIsCommandAsync(false).ConfigureAwait(false);

                        Stream outputStream;
                        if (pc.AcceptedTransferSyntax.IsDeflate)
                        {
                            deflateStream = new DeflateStream(pDataStream, CompressionMode.Compress, true);
                            outputStream = deflateStream;
                        }
                        else
                        {
                            outputStream = pDataStream;
                        }

                        writer = new DicomWriter(
                            pc.AcceptedTransferSyntax,
                            DicomWriteOptions.Default,
                            new StreamByteTarget(outputStream));

                        var datasetWalker = new DicomDatasetWalker(msg.Dataset);
                        
                        await datasetWalker.WalkAsync(writer).ConfigureAwait(false);
                        
                        if (deflateStream != null)
                        {
                            await deflateStream.FlushAsync(CancellationToken.None).ConfigureAwait(false);
                            
                            // Deflate stream in .NET Framework only fully flushes when disposed...
                            deflateStream.Dispose();
                        }                    
                    }
                    
                    await pDataStream.FlushAsync(CancellationToken.None).ConfigureAwait(false);
                    
                    msg.LastPDUSent = DateTime.Now;
                    msg.AllPDUsWereSentSuccessfully();

                    if (msg is DicomRequest request)
                    {
                        request.OnRequestSent?.Invoke(request, new DicomRequest.OnRequestSentEventArgs());
                    }
                }
                catch (Exception e)
                {
                    msg.NotAllPDUsWereSentSuccessfully();
                    Logger.LogError(e, "An error occurred while sending a DICOM message");
                    throw new DicomNetworkException($"Failed to send DICOM message {msg}", e);
                }
                finally
                {
                    deflateStream?.Dispose();
                    pDataStream?.Dispose();
                }
            }
        }

        private async Task CheckForTimeouts()
        {
            while (true)
            {
                if (Options?.RequestTimeout == null)
                {
                    return;
                }

                var requestTimeout = Options.RequestTimeout.Value;

                if (Interlocked.CompareExchange(ref _isCheckingForTimeouts, 1, 0) != 0)
                {
                    return;
                }

                try
                {
                    List<DicomRequest> timedOutPendingRequests;
                    lock (_lock)
                    {
                        if (!_pending.Any())
                        {
                            return;
                        }

                        timedOutPendingRequests = _pending.Where(p => p.IsTimedOut(requestTimeout)).ToList();
                    }

                    if (timedOutPendingRequests.Any())
                    {
                        for (var i = timedOutPendingRequests.Count - 1; i >= 0; i--)
                        {
                            DicomRequest timedOutPendingRequest = timedOutPendingRequests[i];
                            try
                            {
                                Logger.LogWarning($"Request [{timedOutPendingRequest.MessageID}] timed out, removing from pending queue and triggering timeout callbacks");
                                timedOutPendingRequest.OnTimeout?.Invoke(timedOutPendingRequest, new DicomRequest.OnTimeoutEventArgs(requestTimeout));
                            }
                            finally
                            {
                                lock (_lock)
                                {
                                    _pending.Remove(timedOutPendingRequest);
                                    
                                    if (timedOutPendingRequest.AllPDUsSent.Status != TaskStatus.RanToCompletion)
                                    {
                                        _canStillProcessPDataTF = false;
                                        timedOutPendingRequest.NotAllPDUsWereSentSuccessfully();
                                    }
                                }

                                if (this is IDicomClientConnection connection)
                                {
                                    await connection.OnRequestTimedOutAsync(timedOutPendingRequest, requestTimeout).ConfigureAwait(false);
                                }
                            }
                        }
                    }

                    await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "An error occurred in the Fellow Oak DICOM timeout detection loop");
                }
                finally
                {
                    _isCheckingForTimeouts = 0;
                }
            }
        }

        private async Task<bool> TryCloseConnectionAsync(Exception exception = null, bool force = false)
        {
            try
            {
                if (!IsConnected)
                {
                    return true;
                }

                lock (_lock)
                {
                    if (force)
                    {
                        _pduQueue.Clear();
                        _msgQueue.Clear();
                        foreach (var req in _pending)
                        {
                            req.NotAllPDUsWereSentSuccessfully();
                        }
                        _pending.Clear();
                    }

                    if (_pduQueue.Count > 0 || _msgQueue.Count > 0 || _pending.Count > 0)
                    {
                        Logger.LogInformation(
                            "Tried to close connection but queues are not empty, PDUs: {pduCount}, messages: {msgCount}, pending requests: {pendingCount}",
                            _pduQueue.Count,
                            _msgQueue.Count,
                            _pending.Count);
                        return false;
                    }
                }

                if (this is IDicomService dicomService)
                {
                    dicomService.OnConnectionClosed(exception);
                }
                else if (this is IDicomClientConnection connection)
                {
                    await connection.OnConnectionClosedAsync(exception).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error during close attempt");
                throw;
            }

            lock (_lock)
            {
                _isDisconnectedFlag.TrySetResult(true);
                // Unblock other threads waiting to write another PDU that don't realize the connection is being closed
                _pduQueueWatcher.Set();
            }

            Logger.LogInformation("Connection closed");

            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }

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
            ThrowIfAlreadyDisposed();
            
            LogID = association.CalledAE;
            if (Options.UseRemoteAEForLogName)
            {
                Logger = LoggerFactory.CreateLogger(LogID);
            }

            Logger.LogInformation("{CalledAE} -> Association request:\n{Association}", LogID, association);

            Association = association;
            return SendPDUAsync(new AAssociateRQ(Association, _memoryProvider));
        }

        /// <summary>
        /// Send association accept response.
        /// </summary>
        /// <param name="association">DICOM association.</param>
        protected Task SendAssociationAcceptAsync(DicomAssociation association)
        {
            ThrowIfAlreadyDisposed();
            
            Association = association;

            // reject all presentation contexts that have not already been accepted or rejected
            foreach (var pc in Association.PresentationContexts)
            {
                if (pc.Result == DicomPresentationContextResult.Proposed)
                {
                    pc.SetResult(DicomPresentationContextResult.RejectNoReason);
                }
            }

            Logger.LogInformation("{LogId} -> Association accept:\n{Association}", LogID, association);

            return SendPDUAsync(new AAssociateAC(Association, _memoryProvider));
        }

        /// <summary>
        /// Send association reject response.
        /// </summary>
        /// <param name="result">Rejection result.</param>
        /// <param name="source">Rejection source.</param>
        /// <param name="reason">Rejection reason.</param>
        protected Task SendAssociationRejectAsync(DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            ThrowIfAlreadyDisposed();
            Logger.LogInformation("{logId} -> Association reject [result: {result}; source: {source}; reason: {reason}]", LogID,
                result, source, reason);
            return SendPDUAsync(new AAssociateRJ(result, source, reason, _memoryProvider));
        }

        /// <summary>
        /// Send association release request.
        /// </summary>
        protected Task SendAssociationReleaseRequestAsync()
        {
            ThrowIfAlreadyDisposed();
            Logger.LogInformation("{logId} -> Association release request", LogID);
            return SendPDUAsync(new AReleaseRQ(_memoryProvider));
        }

        /// <summary>
        /// Send association release response.
        /// </summary>
        protected Task SendAssociationReleaseResponseAsync()
        {
            ThrowIfAlreadyDisposed();
            Logger.LogInformation("{logId} -> Association release response", LogID);
            return SendPDUAsync(new AReleaseRP(_memoryProvider));
        }

        /// <summary>
        /// Send abort request.
        /// </summary>
        /// <param name="source">Abort source.</param>
        /// <param name="reason">Abort reason.</param>
        protected Task SendAbortAsync(DicomAbortSource source, DicomAbortReason reason)
        {
            ThrowIfAlreadyDisposed();
            Logger.LogInformation("{logId} -> Abort [source: {source}; reason: {reason}]", LogID, source, reason);
            return SendPDUAsync(new AAbort(source, reason, _memoryProvider));
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Setup long-running operations that the DICOM service manages.
        /// </summary>
        /// <returns>Awaitable task maintaining the long-running operation(s).</returns>
        public virtual Task RunAsync()
        {
            if (_isInitialized)
            {
                return Task.CompletedTask;
            }

            _isInitialized = true;

            return ListenAndProcessPDUAsync();
        }

        /// <summary>
        /// Action to perform when send queue is empty.
        /// </summary>
        protected virtual Task OnSendQueueEmptyAsync()
            => Task.CompletedTask;

        #endregion

        #region Helper methods

        private bool LogIOException(Exception e, ILogger logger, bool reading)
        {
            if (NetworkManager.IsSocketException(e.InnerException, out int errorCode, out string errorDescriptor))
            {
                logger.LogInformation(
                    $"Socket error while {(reading ? "reading" : "writing")} PDU: {{socketError}} [{{errorCode}}]",
                    errorDescriptor,
                    errorCode);
                return true;
            }

            if (e.InnerException is ObjectDisposedException)
            {
                logger.LogInformation($"Object disposed while {(reading ? "reading" : "writing")} PDU");
            }
            else
            {
                logger.LogError(e, $"I/O exception while {(reading ? "reading" : "writing")} PDU");
            }

            return false;
        }

        #endregion
        
        #region INNER TYPES 
        
        internal class PDataTFStream : Stream
        {
            #region Private Members

            private readonly DicomService _service;

            private bool _command;

            private readonly uint _pduMax;

            private uint _max;

            private readonly byte _pcid;

            private readonly DicomMessage _dicomMessage;
            private readonly IMemoryProvider _memoryProvider;

            private PDataTF _pdu;

            /// <summary>
            /// This will be a rented byte array to server as a buffer for the current PDV
            /// </summary>
            private IMemory _memory;

            private int _length;

            #endregion

            #region Public Constructors

            public PDataTFStream(DicomService service, IMemoryProvider memoryProvider, byte pcid, uint max, DicomMessage dicomMessage)
            {
                _service = service ?? throw new ArgumentNullException(nameof(service));
                _memoryProvider = memoryProvider ?? throw new ArgumentNullException(nameof(memoryProvider));
                _command = true;
                _pcid = pcid;
                _dicomMessage = dicomMessage;
                _pduMax = Math.Min(max, int.MaxValue);
                _max = _pduMax == 0
                    ? _service.Options.MaxCommandBuffer
                    : Math.Min(_pduMax, _service.Options.MaxCommandBuffer);

                _pdu = new PDataTF(_memoryProvider);

                // Max PDU Size - Current Size - Size of PDV header
                _memory = ProvideEvenLengthMemory((int) (_max - CurrentPduSize() - RawPDU.CommonFieldsLength));
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
                return RawPDU.CommonFieldsLength + _pdu.GetLengthOfPDVs();
            }

            private async Task CreatePDVAsync(bool last)
            {
                try
                {
                    var memory = _memory;
                    // Immediately set to null so we cannot double dispose it
                    _memory = null;
                    if (memory == null)
                    {
                        throw new InvalidOperationException("Tried to write another PDV after the last PDV");
                    }

                    // Ensure PDV has even length
                    if (_length % 2 == 1)
                    {
                        if (memory.Length <= _length)
                        {
                            // This scenario should be prevented by ProvideEvenLengthMemory
                            throw new InvalidOperationException("Current memory is not large enough to pad with one extra byte to make the PDV even");
                        }

                        // Rented array is large enough to fit another byte, so we don't have to do anything special
                        memory.Bytes[_length] = 0;
                        _length++;
                    }

                    var pdv = new PDV(_pcid, memory, _length, _command, last);
                    _pdu.PDVs.Add(pdv);
                    
                    // reset length in case we recurse into WritePDU()
                    _length = 0;
                    // is the current PDU at its maximum size or do we have room for another PDV?
                    if ((_service.Options.MaxPDVsPerPDU != 0 && _pdu.PDVs.Count >= _service.Options.MaxPDVsPerPDU)
                        || CurrentPduSize() + RawPDU.CommonFieldsLength >= _max 
                        || (!_command && last))
                    {
                        await WritePDUAsync(last).ConfigureAwait(false);
                    }
                    
                    if (!last)
                    {
                        // Max PDU Size - Current Size - Size of PDV header
                        var memoryLength = (int)(_max - CurrentPduSize() - RawPDU.CommonFieldsLength);
                        _memory = ProvideEvenLengthMemory(memoryLength);
                    }
                }
                catch (Exception e)
                {
                    _service.Logger.LogError(e, "Exception creating PDV");
                    throw;
                }
            }

            private async Task WritePDUAsync(bool last)
            {
                // Immediately stop sending PDUs if the message is no longer pending (e.g. because it timed out)
                if (_dicomMessage is DicomRequest req && !_service.IsStillPending(req))
                {
                    _pdu.Dispose();
                    _pdu = new PDataTF(_memoryProvider);
                    return;
                }

                if (_length > 0)
                {
                    await CreatePDVAsync(last).ConfigureAwait(false);
                }

                if (_pdu.PDVs.Count > 0)
                {
                    if (last)
                    {
                        _pdu.PDVs[_pdu.PDVs.Count - 1].IsLastFragment = true;
                    }

                    try
                    {
                        await _service.SendPDUAsync(_pdu).ConfigureAwait(false);
                        _dicomMessage.LastPDUSent = DateTime.Now;
                    }
                    finally
                    {
                        _pdu.Dispose();
                        _pdu = new PDataTF(_memoryProvider);
                    }
                }
            }
            
            private IMemory ProvideEvenLengthMemory(int length)
            {
                // Since these byte arrays will be used to create PDVs
                // and since PDVs must have an even number of bytes
                // we ensure that the capacity is always at least an even amount 
                if (length % 2 == 1)
                {
                    length += 1;
                }

                return _memoryProvider.Provide(length);
            }

            #endregion

            #region Stream Members

            public override bool CanRead => false;

            public override bool CanSeek => false;

            public override bool CanWrite => true;

            public override void Flush() { }

            public override long Length => throw new NotSupportedException();

            public override long Position
            {
                get => throw new NotSupportedException();
                set => throw new NotSupportedException();
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

            public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                try
                {
                    if (_memory == null)
                    {
                        // Max PDU Size - Current Size - Size of PDV header
                        var memoryLength = (int) (_max - CurrentPduSize() - RawPDU.CommonFieldsLength);
                        _memory = ProvideEvenLengthMemory(memoryLength);
                    }

                    while (count >= _memory.Length - _length)
                    {
                        var c = Math.Min(count, _memory.Length - _length);

                        buffer.AsSpan(offset, c).CopyTo(_memory.Span.Slice(_length, c));
                        
                        _length += c;
                        offset += c;
                        count -= c;

                        await CreatePDVAsync(false).ConfigureAwait(false);
                    }

                    if (count > 0)
                    {
                        buffer.AsSpan(offset, count).CopyTo(_memory.Span.Slice(_length, count));
                        _length += count;

                        if (_memory.Length == _length)
                        {
                            await CreatePDVAsync(false).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception e)
                {
                    _service.Logger.LogError(e, "Exception writing data to PDV");
                    throw;
                }
            }

            public override async Task FlushAsync(CancellationToken cancellationToken)
            {
                await CreatePDVAsync(true).ConfigureAwait(false);
                await WritePDUAsync(true).ConfigureAwait(false);
            }

            #endregion
            
            #region Disposable Members
            
            protected override void Dispose(bool disposing)
            {
                var bytes = Interlocked.Exchange(ref _memory, null);
                bytes?.Dispose();

                var pdu = Interlocked.Exchange(ref _pdu, null);
                pdu?.Dispose();

                base.Dispose(disposing);
            }
            
            #endregion
        }

        
        #endregion 
    }
}

