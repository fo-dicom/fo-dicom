// Copyright (c) 2012-2017 fo-dicom contributors.
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

        private DicomServiceOptions _options;

        private Encoding _fallbackEncoding;

        private bool _isIpAddressSet;

        private bool _isPortSet;

        private bool _wasStarted;

        private bool _disposed;

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

            DicomServer.Add(this);
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

        #endregion

        #region METHODS

        /// <inheritdoc />
        public virtual Task StartAsync(string ipAddress, int port, object userState, string certificateName,
            DicomServiceOptions options, Encoding fallbackEncoding)
        {
            if (_wasStarted)
            {
                throw new DicomNetworkException("Server has already been started once, cannot be started again.");
            }
            _wasStarted = true;

            IPAddress = ipAddress;
            Port = port;

            _options = options;
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
                _services.Clear();
            }

            var removed = DicomServer.Remove(this);
            if (!removed)
            {
                Logger.Warn(
                    "Could not unregister DICOM server on port {0}, either because registration failed or because server has already been unregistered once.",
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
                var noDelay = _options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;

                listener = NetworkManager.CreateNetworkListener(IPAddress, Port);
                await listener.StartAsync().ConfigureAwait(false);
                IsListening = true;

                while (!_cancellationSource.IsCancellationRequested)
                {
                    var token = _cancellationSource.Token;
                    await WaitUntilClientIsAttachableAsync(token).ConfigureAwait(false);

                    var networkStream = await listener.AcceptNetworkStreamAsync(_certificateName, noDelay, token)
                        .ConfigureAwait(false);

                    if (networkStream != null)
                    {
                        var scp = CreateScp(networkStream);
                        if (_options != null)
                        {
                            scp.Options = _options;
                        }

                        _services.Add(scp.RunAsync());
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

        private async Task WaitUntilClientIsAttachableAsync(CancellationToken token)
        {
            var maxClientsAllowed = _options?.MaxClientsAllowed ?? DicomServiceOptions.Default.MaxClientsAllowed;
            if (maxClientsAllowed == 0) return;

            while (!token.IsCancellationRequested && _services.Count >= maxClientsAllowed)
            {
                await Task.Delay(10, token).ConfigureAwait(false);
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
                    if (_services.Count > 0)
                    {
                        await Task.WhenAny(_services).ConfigureAwait(false);
                        _services.RemoveAll(service => service.IsCompleted);
                    }
                    else
                    {
                        await Task.Delay(1000, _cancellationSource.Token).ConfigureAwait(false);
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.Info("Disconnected client cleanup manually terminated.");
                    _services.RemoveAll(service => service.IsCompleted);
                }
                catch (Exception e)
                {
                    Logger.Warn("Exception removing disconnected clients, {@error}", e);
                }
            }
        }

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
        /// <param name="ipAddress">IP address(es) to listen to.</param>
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
                portInUse = _servers.Any(s => s.Key.Port == port);
            }

            if (portInUse)
            {
                throw new DicomNetworkException("There is already a DICOM server registered on port: {0}", port);
            }

            var server = new TServer();
            if (logger != null) server.Logger = logger;

            var runner = server.StartAsync(string.IsNullOrEmpty(ipAddress) ? NetworkManager.IPv4Any : ipAddress, port,
                userState, certificateName, options, fallbackEncoding);

            lock (_lock)
            {
                _servers[server] = runner;
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
                server = _servers.SingleOrDefault(s => s.Key.Port == port).Key;
            }

            return server;
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
        /// Add a DICOM server to the list of registered servers.
        /// </summary>
        /// <param name="server">Server to add.</param>
        public static void Add(IDicomServer server)
        {
            lock (_lock)
            {
                if (_servers.Any(s => s.Key.Port == server.Port))
                {
                    throw new DicomNetworkException(
                        "Could not register DICOM server on port {0}, probably because another server simultaneously registered on the same port.",
                        server.Port);
                }

                _servers.Add(server, null);
            }
        }

        /// <summary>
        /// Removes a DICOM server from the list of registered servers.
        /// </summary>
        /// <param name="server">Server to remove.</param>
        /// <returns>True if <paramref name="server"/> could be removed, false otherwise.</returns>
        internal static bool Remove(IDicomServer server)
        {
            bool removed;
            lock (_lock)
            {
                removed = _servers.Remove(server);
            }

            return removed;
        }

        #endregion
    }
}

#endif
