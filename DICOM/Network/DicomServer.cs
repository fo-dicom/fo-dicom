﻿// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35

using System;
using System.Collections.Concurrent;
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
    public class DicomServer<T> : IDicomServer
        where T : DicomService, IDicomServiceProvider
    {
        #region FIELDS

        private bool _disposed;

        private DicomServiceOptions _options;

        private string _certificateName;

        private Encoding _fallbackEncoding;

        private object _userState;

        private readonly CancellationTokenSource _cancellationSource;

        private readonly List<Task> _services;

        private Logger _logger;

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

            _disposed = false;
            Register();
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        public string IPAddress { get; protected set; }

        /// <inheritdoc />
        public int Port { get; protected set; }

        /// <inheritdoc />
        public Logger Logger
        {
            get { return _logger ?? (_logger = LogManager.GetLogger("Dicom.Network")); }
            set { _logger  = value; }
        }

        /// <inheritdoc />
        public bool IsListening { get; protected set; }

        /// <inheritdoc />
        public Exception Exception { get; protected set; }

        /// <inheritdoc />
        public Task BackgroundWorker { get; protected set; }

        /// <summary>
        /// Gets the number of clients currently connected to the server.
        /// </summary>
        /// <remarks>Included for testing purposes only.</remarks>
        internal int CompletedServicesCount => _services.Count(service => service.IsCompleted);

        #endregion

        #region METHODS

        /// <inheritdoc />
        public Task StartAsync(string ipAddress, int port, object userState, string certificateName, DicomServiceOptions options,
            Encoding fallbackEncoding = null)
        {
            IPAddress = ipAddress;
            Port = port;

            _options = options;
            _userState = userState;
            _certificateName = certificateName;
            _fallbackEncoding = fallbackEncoding;

            BackgroundWorker = Task.WhenAll(ListenForConnectionsAsync(), RemoveUnusedServicesAsync());
            return BackgroundWorker;
        }

        /// <inheritdoc />
        public void Stop()
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

            Unregister();
            _disposed = true;
        }

        /// <summary>
        /// Register this server to list of registered servers.
        /// </summary>
        protected void Register()
        {
            var added = DicomServer.Add(this);
            if (!added)
            {
                Logger.Warn(
                    "Could not register DICOM server on port {0}, probably because another server is already registered on the same port.",
                    Port);
            }
        }

        /// <summary>
        /// Unregister this server from list of registered servers.
        /// </summary>
        protected void Unregister()
        {
            var removed = DicomServer.Remove(this);
            if (!removed)
            {
                Logger.Warn(
                    "Could not unregister DICOM server on port {0}, either because registration failed or because server has already been unregistered once.",
                    Port);
            }
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
            try
            {
                var noDelay = _options?.TcpNoDelay ?? DicomServiceOptions.Default.TcpNoDelay;

                var listener = NetworkManager.CreateNetworkListener(IPAddress, Port);
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

                listener.Stop();
                IsListening = false;
                Exception = null;
            }
            catch (OperationCanceledException)
            {
                Logger.Info("Listening manually terminated");

                IsListening = false;
                Exception = null;
            }
            catch (Exception e)
            {
                Logger.Error("Exception listening for clients, {@error}", e);

                Stop();
                IsListening = false;
                Exception = e;
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
    public static class DicomServer
    {
        #region FIELDS

        private static readonly ConcurrentDictionary<IDicomServer, Task> Servers =
            new ConcurrentDictionary<IDicomServer, Task>(DicomServerPortComparer.Default);

        private static readonly object _locker = new object();

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
            return Create<T, DicomServer<T>>(NetworkManager.IPv4Any, port, null, certificateName, options, fallbackEncoding, logger);
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
            return Create<T, DicomServer<T>>(NetworkManager.IPv4Any, port, userState, certificateName, options, fallbackEncoding, logger);
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
            return Create<T, DicomServer<T>>(ipAddress, port, userState, certificateName, options, fallbackEncoding, logger);
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
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        public static IDicomServer Create<T, TServer>(
            string ipAddress,
            int port,
            object userState = null,
            string certificateName = null,
            DicomServiceOptions options = null,
            Encoding fallbackEncoding = null,
            Logger logger = null) where T : DicomService, IDicomServiceProvider where TServer : DicomServer<T>, new()
        {
            if (Servers.Any(server => server.Key.Port == port))
            {
                throw new DicomNetworkException("There is already a DICOM server registered on port {0}", port);
            }

            lock (_locker)
            {
                var server = new TServer();
                if (logger != null) server.Logger = logger;

                var runner = server.StartAsync(ipAddress, port, userState, certificateName, options, fallbackEncoding);
                Servers.TryUpdate(server, runner, null);

                return server;
            }
        }

        /// <summary>
        /// Gets DICOM server instance registered to <paramref name="port"/>.
        /// </summary>
        /// <param name="port">Port number for which DICOM server is requested.</param>
        /// <returns>Registered DICOM server for <paramref name="port"/>.</returns>
        public static IDicomServer GetInstance(int port)
        {
            return Servers.SingleOrDefault(server => server.Key.Port == port).Key;
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
        /// Adds a DICOM server to the list of registered servers.
        /// </summary>
        /// <param name="server">Server to add.</param>
        /// <returns>True if <paramref name="server"/> could be added, false otherwise.</returns>
        internal static bool Add(IDicomServer server)
        {
            return Servers.TryAdd(server, null);
        }

        /// <summary>
        /// Removes a DICOM server from the list of registered servers.
        /// </summary>
        /// <param name="server">Server to remove.</param>
        /// <returns>True if <paramref name="server"/> could be removed, false otherwise.</returns>
        internal static bool Remove(IDicomServer server)
        {
            Task runner;
            return Servers.TryRemove(server, out runner);
        }

        #endregion

        #region INNER TYPES

        /// <summary>
        /// Equality comparer implementation with respect to <see cref="IDicomServer"/> <see cref="IDicomServer.Port">port number</see>.
        /// </summary>
        private class DicomServerPortComparer : IEqualityComparer<IDicomServer>
        {
            public static readonly IEqualityComparer<IDicomServer> Default = new DicomServerPortComparer();

            private DicomServerPortComparer()
            {
            }

            public bool Equals(IDicomServer x, IDicomServer y)
            {
                return x != null && y != null && x.Port == y.Port;
            }

            public int GetHashCode(IDicomServer obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj));
                }
                return obj.Port;
            }
        }

        #endregion
    }
}

#endif
