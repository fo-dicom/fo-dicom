// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Dicom.Log;

namespace Dicom.Network
{
    /// <summary>
    /// Representation of a DICOM server.
    /// </summary>
    /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
    public class DicomServer<T> : IDicomServer<T> where T : DicomService, IDicomServiceProvider
    {
        #region FIELDS

        private readonly List<Task> _services;

        private readonly CancellationTokenSource _cancellationSource;

        private string _ipAddress;

        private int _port;

        private Logger _logger;

        private object _userState;

        private string _certificateName;

        private Encoding _fallbackEncoding;

        private bool _isIpAddressSet;

        private bool _isPortSet;

        private bool _wasStarted;

        private bool _disposed;

        private readonly AsyncManualResetEvent _hasServicesFlag;

        private readonly AsyncManualResetEvent _hasNonMaxServicesFlag;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomServer{T}"/> class.
        /// </summary>
        public DicomServer()
        {
            _cancellationSource = new CancellationTokenSource();
            _services = new List<Task>();

            IsListening = false;
            Exception = null;

            _isIpAddressSet = false;
            _isPortSet = false;
            _wasStarted = false;

            _disposed = false;

            _hasServicesFlag = new AsyncManualResetEvent(false);
            _hasNonMaxServicesFlag = new AsyncManualResetEvent(true);
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        public virtual string IPAddress
        {
            get { return _ipAddress; }
            protected set
            {
                if (_isIpAddressSet && !string.Equals(_ipAddress, value, StringComparison.OrdinalIgnoreCase))
                    throw new DicomNetworkException("IP Address cannot be set twice. Current value: {0}", _ipAddress);
                _ipAddress = value;
                _isIpAddressSet = true;
            }
        }

        /// <inheritdoc />
        public virtual int Port
        {
            get { return _port; }
            protected set
            {
                if (_isPortSet && _port != value)
                    throw new DicomNetworkException("Port cannot be set twice. Current value: {0}", _port);
                _port = value;
                _isPortSet = true;
            }
        }

        /// <inheritdoc />
        public bool IsListening { get; protected set; }

        /// <inheritdoc />
        public Exception Exception { get; protected set; }

        public DicomServiceOptions Options { get; protected set; }

        /// <inheritdoc />
        public Logger Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger("Dicom.Network")); }
            set { _logger  = value; }
        }

        /// <summary>
        /// Gets the number of clients currently connected to the server.
        /// </summary>
        /// <remarks>Included for testing purposes only.</remarks>
        internal int CompletedServicesCount => _services.Count(service => service.IsCompleted);

        /// <summary>
        /// Gets whether the list of services contains the maximum number of services or not.
        /// </summary>
        private bool IsServicesAtMax
        {
            get
            {
                var maxClientsAllowed = Options?.MaxClientsAllowed ?? DicomServiceOptions.Default.MaxClientsAllowed;
                return maxClientsAllowed > 0 && _services.Count >= maxClientsAllowed;
            }
        }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public virtual Task StartAsync(string ipAddress, int port, string certificateName, Encoding fallbackEncoding,
            DicomServiceOptions options, object userState)
        {
            if (_wasStarted)
            {
                throw new DicomNetworkException("Server has already been started once, cannot be started again.");
            }
            _wasStarted = true;

            IPAddress = string.IsNullOrEmpty(ipAddress?.Trim()) ? NetworkManager.IPv4Any : ipAddress;
            Port = port;

            Options = options;

            _userState = userState;
            _certificateName = certificateName;
            _fallbackEncoding = fallbackEncoding;

            return Task.WhenAll(ListenForConnectionsAsync(), RemoveUnusedServicesAsync());
        }

        /// <inheritdoc />
        public virtual void Stop()
        {
            if (!_cancellationSource.IsCancellationRequested)
            {
                _cancellationSource.Cancel();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Execute the disposal.
        /// </summary>
        /// <param name="disposing">True if called from <see cref="Dispose()"/>, false otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Stop();
                _cancellationSource.Dispose();
            }

            ClearServices();

            var removed = DicomServer.Unregister(this);
            if (!removed)
            {
                Logger.Warn(
                    "Could not unregister DICOM server on port {0}, either because never registered or because has already been unregistered once.",
                    Port);
            }

            _disposed = true;
        }

        /// <summary>
        /// Create an instance of the DICOM service class.
        /// </summary>
        /// <param name="stream">Network stream.</param>
        /// <returns>An instance of the DICOM service class.</returns>
        protected virtual T CreateScp(INetworkStream stream)
        {
            var instance = (T)Activator.CreateInstance(typeof(T), stream, _fallbackEncoding, Logger);
            instance.UserState = _userState;
            return instance;
        }

        /// <summary>
        /// Listen indefinitely for network connections on the specified port.
        /// </summary>
        private async Task ListenForConnectionsAsync()
        {
            INetworkListener listener = null;
            try
            {
                var noDelay = Options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;

                listener = NetworkManager.CreateNetworkListener(IPAddress, Port);
                await listener.StartAsync().ConfigureAwait(false);
                IsListening = true;

                while (!_cancellationSource.IsCancellationRequested)
                {
                    await _hasNonMaxServicesFlag.WaitAsync().ConfigureAwait(false);

                    var networkStream = await listener
                        .AcceptNetworkStreamAsync(_certificateName, noDelay, _cancellationSource.Token)
                        .ConfigureAwait(false);

                    if (networkStream != null)
                    {
                        var scp = CreateScp(networkStream);
                        if (Options != null)
                        {
                            scp.Options = Options;
                        }

                        _services.Add(scp.RunAsync());

                        _hasServicesFlag.Set();
                        if (IsServicesAtMax) _hasNonMaxServicesFlag.Reset();
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Logger.Error("Exception listening for DICOM services, {@error}", e);

                Stop();
                Exception = e;
            }
            finally
            {
                listener?.Stop();
                IsListening = false;
            }
        }

        /// <summary>
        /// Remove no longer used client connections.
        /// </summary>
        private async Task RemoveUnusedServicesAsync()
        {
            while (!_cancellationSource.IsCancellationRequested)
            {
                try
                {
                    await _hasServicesFlag.WaitAsync().ConfigureAwait(false);
                    await Task.WhenAny(_services).ConfigureAwait(false);

                    _services.RemoveAll(service => service.IsCompleted);

                    if (_services.Count == 0) _hasServicesFlag.Reset();
                    if (!IsServicesAtMax) _hasNonMaxServicesFlag.Set();
                }
                catch (OperationCanceledException)
                {
                    Logger.Info("Disconnected client cleanup manually terminated.");
                    ClearServices();
                }
                catch (Exception e)
                {
                    Logger.Warn("Exception removing disconnected clients, {@error}", e);
                }
            }
        }

        private void ClearServices()
        {
            _services.Clear();
            _hasServicesFlag.Reset();
            _hasNonMaxServicesFlag.Set();
        }

        #endregion

        #region INNER TYPES

        #endregion
    }

    /// <summary>
    /// Support class for managing multiple DICOM server instances.
    /// </summary>
    /// <remarks>Controls that only one DICOM server per <see cref="IDicomServer.Port"/> is initialized. Current implementation
    /// only allows one server per port. It is not possible to initialize multiple servers listening to different network interfaces 
    /// (for example IPv4 vs. IPv6) via these methods if the port is the same.</remarks>
    public static class DicomServer
    {
        #region FIELDS

        private static readonly IDictionary<IDicomServer, Task> _servers = new Dictionary<IDicomServer, Task>();

        private static readonly object _lock = new object();

        #endregion

        #region METHODS

        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <param name="port">Port to listen to.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        public static IDicomServer Create<T>(
            int port,
            string certificateName = null,
            DicomServiceOptions options = null,
            Encoding fallbackEncoding = null,
            Logger logger = null) where T : DicomService, IDicomServiceProvider
        {
            return Create<T, DicomServer<T>>(NetworkManager.IPv4Any, port, null, certificateName, options,
                fallbackEncoding, logger);
        }

        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <param name="port">Port to listen to.</param>
        /// <param name="userState">Optional optional parameters.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        [Obsolete("Use suitable DicomServer.Create overload instead.")]
        public static IDicomServer Create<T>(
            int port,
            object userState,
            string certificateName = null,
            DicomServiceOptions options = null,
            Encoding fallbackEncoding = null,
            Logger logger = null) where T : DicomService, IDicomServiceProvider
        {
            return Create<T, DicomServer<T>>(NetworkManager.IPv4Any, port, userState, certificateName, options,
                fallbackEncoding, logger);
        }

        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <param name="ipAddress">IP address(es) to listen to.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="userState">Optional optional parameters.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        public static IDicomServer Create<T>(
            string ipAddress,
            int port,
            object userState = null,
            string certificateName = null,
            DicomServiceOptions options = null,
            Encoding fallbackEncoding = null,
            Logger logger = null) where T : DicomService, IDicomServiceProvider
        {
            return Create<T, DicomServer<T>>(ipAddress, port, userState, certificateName, options, fallbackEncoding,
                logger);
        }

        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <typeparam name="TServer">Concrete DICOM server type to be returned.</typeparam>
        /// <param name="ipAddress">IP address(es) to listen to. Value <code>null</code> applies default, IPv4Any.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="userState">Optional optional parameters.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <returns>An instance of <typeparamref name="TServer"/>, that starts listening for connections in the background.</returns>
        public static IDicomServer Create<T, TServer>(
            string ipAddress,
            int port,
            object userState = null,
            string certificateName = null,
            DicomServiceOptions options = null,
            Encoding fallbackEncoding = null,
            Logger logger = null) where T : DicomService, IDicomServiceProvider where TServer : IDicomServer<T>, new()
        {
            bool portInUse;
            lock (_lock)
            {
                portInUse = _servers.Any(IsMatching(port));
            }

            if (portInUse)
            {
                throw new DicomNetworkException("There is already a DICOM server registered on port: {0}", port);
            }

            var server = new TServer();
            if (logger != null) server.Logger = logger;

            var runner = server.StartAsync(ipAddress, port, certificateName, fallbackEncoding, options, userState);

            lock (_lock)
            {
                if (_servers.Any(IsMatching(port)))
                {
                    throw new DicomNetworkException(
                        "Could not register DICOM server on port {0}, probably because another server just registered to the same port.",
                        port);
                }

                _servers.Add(server, runner);
            }

            return server;
        }

        /// <summary>
        /// Gets DICOM server instance registered to <paramref name="port"/>.
        /// </summary>
        /// <param name="port">Port number for which DICOM server is requested.</param>
        /// <returns>Registered DICOM server for <paramref name="port"/>.</returns>
        public static IDicomServer GetInstance(int port)
        {
            IDicomServer server;
            lock (_lock)
            {
                server = _servers.SingleOrDefault(IsMatching(port)).Key;
            }

            return server;
        }

        /// <summary>
        /// Gets service listener for the DICOM server instance registered to <paramref name="port"/>.
        /// </summary>
        /// <param name="port">Port number for which the service listener is requested.</param>
        /// <returns>Service listener for the <paramref name="port"/> DICOM server.</returns>
        public static Task GetListener(int port)
        {
            Task listener;
            lock (_lock)
            {
                listener = _servers.SingleOrDefault(IsMatching(port)).Value;
            }

            return listener;
        }

        /// <summary>
        /// Gets an indicator of whether a DICOM server is registered and listening on the specified <paramref name="port"/>.
        /// </summary>
        /// <param name="port">Port number for which listening status is requested.</param>
        /// <returns>True if DICOM server on <paramref name="port"/> is registered and listening, false otherwise.</returns>
        public static bool IsListening(int port)
        {
            return GetInstance(port)?.IsListening ?? false;
        }

        /// <summary>
        /// Removes a DICOM server from the list of registered servers.
        /// </summary>
        /// <param name="server">Server to remove.</param>
        /// <returns>True if <paramref name="server"/> could be removed, false otherwise.</returns>
        internal static bool Unregister(IDicomServer server)
        {
            bool removed;
            lock (_lock)
            {
                removed = _servers.Remove(server);
            }

            return removed;
        }

        /// <summary>
        /// Gets the function to be used in LINQ queries when searching for server matches.
        /// </summary>
        /// <param name="port">Matching port.</param>
        /// <returns>Function to be used in LINQ queries when searching for server matches.</returns>
        private static Func<KeyValuePair<IDicomServer, Task>, bool> IsMatching(int port)
        {
            return s => s.Key.Port == port;
        }

        #endregion
    }
}

#endif
