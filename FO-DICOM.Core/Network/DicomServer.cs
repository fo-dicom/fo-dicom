// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Tls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Representation of a DICOM server.
    /// </summary>
    /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
    public class DicomServer<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T> : IDicomServer<T> where T : DicomService, IDicomServiceProvider
    {
        #region FIELDS

        private readonly INetworkManager _networkManager;

        private readonly ILoggerFactory _loggerFactory;

        private readonly List<RunningDicomService> _services;

        private readonly CancellationTokenSource _cancellationSource;

        private readonly CancellationToken _cancellationToken;

        private string _ipAddress;

        private int _port;

        private ILogger _logger;

        private object _userState;

        private ITlsAcceptor _tlsAcceptor;

        private Encoding _fallbackEncoding;

        private bool _isIpAddressSet;

        private bool _isPortSet;

        private bool _wasStarted;

        private bool _disposed;

        private SemaphoreSlim _maxClientsSemaphore;

        private DicomServerOptions _serverOptions;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of the <see cref="DicomServer{T}"/> class.
        /// </summary>
        public DicomServer(DicomServerDependencies dependencies)
        {
            _networkManager = dependencies.NetworkManager ?? throw new ArgumentNullException(nameof(dependencies.NetworkManager));
            _loggerFactory = dependencies.LoggerFactory ?? throw new ArgumentNullException(nameof(dependencies.LoggerFactory));

            _cancellationSource = new CancellationTokenSource();
            _cancellationToken = _cancellationSource.Token;

            _services = new List<RunningDicomService>();

            IsListening = false;
            Exception = null;

            _isIpAddressSet = false;
            _isPortSet = false;
            _wasStarted = false;

            _disposed = false;
        }

        #endregion

        #region PROPERTIES

        /// <inheritdoc />
        public virtual string IPAddress
        {
            get => _ipAddress;
            protected set
            {
                if (_isIpAddressSet && !string.Equals(_ipAddress, value, StringComparison.OrdinalIgnoreCase))
                {
                    throw new DicomNetworkException($"IP Address cannot be set twice. Current value: {_ipAddress}");
                }

                _ipAddress = value;
                _isIpAddressSet = true;
            }
        }

        /// <inheritdoc />
        public virtual int Port
        {
            get => _port;
            protected set
            {
                if (_isPortSet && _port != value)
                {
                    throw new DicomNetworkException($"Port cannot be set twice. Current value: {_port}");
                }

                _port = value;
                _isPortSet = true;
            }
        }

        /// <inheritdoc />
        public bool IsListening { get; protected set; }

        /// <inheritdoc />
        public Exception Exception { get; protected set; }

        public DicomServiceOptions Options { get; private set; }

        /// <inheritdoc />
        public ILogger Logger
        {
            get => _logger ??= _loggerFactory.CreateLogger(Log.LogCategories.Network);
            set => _logger = value;
        }

        /// <inheritdoc />
        public IServiceScope ServiceScope { get; set; }

        /// <inheritdoc />
        public DicomServerRegistration Registration { get; set; }

        /// <summary>
        /// Gets the number of clients currently connected to the server.
        /// </summary>
        /// <remarks>Included for testing purposes only.</remarks>
        internal int CompletedServicesCount
        {
            get
            {
                lock (_services)
                {
                    return _services.Count(service => service.Task.IsCompleted);
                }
            }
        }

        internal TimeSpan MaxClientsAllowedWaitInterval { get; set; }

        #endregion

        #region METHODS

        /// <inheritdoc />
        public virtual Task StartAsync(string ipAddress, int port, ITlsAcceptor tlsAcceptor, Encoding fallbackEncoding,
            DicomServiceOptions serviceOptions, object userState, DicomServerOptions serverOptions)
        {
            if (_wasStarted)
            {
                throw new DicomNetworkException("Server has already been started once, cannot be started again.");
            }
            _wasStarted = true;

            IPAddress = string.IsNullOrEmpty(ipAddress?.Trim()) ? NetworkManager.IPv4Any : ipAddress;
            Port = port;

            _serverOptions = serverOptions;
            Options = serviceOptions;

            _userState = userState;
            _tlsAcceptor = tlsAcceptor;
            _fallbackEncoding = fallbackEncoding;
            _maxClientsSemaphore = serverOptions.MaxClientsAllowed > 0
                ? new SemaphoreSlim(serverOptions.MaxClientsAllowed, serverOptions.MaxClientsAllowed)
                : null;
            MaxClientsAllowedWaitInterval = TimeSpan.FromSeconds(60);
            return ListenForConnectionsAsync();
        }

        /// <inheritdoc />
        public virtual void Stop()
        {
            if (!_cancellationSource.IsCancellationRequested)
            {
                _cancellationSource.Cancel();
            }
        }

        public virtual int GetNumberOfConnectedClients()
        {
            return _services.Count;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
                _maxClientsSemaphore?.Dispose();
                Registration?.Dispose();
                ServiceScope?.Dispose();
            }

            ClearServices();

            _disposed = true;
        }

        /// <summary>
        /// Create an instance of the DICOM service class.
        /// </summary>
        /// <param name="stream">Network stream.</param>
        /// <returns>An instance of the DICOM service class.</returns>
        protected virtual T CreateScp(INetworkStream stream)
        {
            var creator = ActivatorUtilities.CreateFactory(typeof(T), new[] { typeof(INetworkStream), typeof(Encoding), typeof(ILogger) });
            var instance = (T)creator(ServiceScope.ServiceProvider, new object[] { stream, _fallbackEncoding, Logger });

            // Please do not use property injection. See https://stackoverflow.com/a/39853478/563070
            /*foreach (var propertyInfo in typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanWrite))
            {
                if (propertyInfo.GetValue(instance) is null)
                {
                    var service = Setup.ServiceProvider.GetService(propertyInfo.PropertyType);
                    if (service != null)
                    {
                        propertyInfo.SetValue(instance, service);
                    }
                }
            }*/

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
                listener = _networkManager.CreateNetworkListener(IPAddress, Port);
                await listener.StartAsync().ConfigureAwait(false);
                _port = listener.Port;
                IsListening = true;

                var maxClientsAllowed = _serverOptions.MaxClientsAllowed;

                while (!_cancellationToken.IsCancellationRequested)
                {
                    if (maxClientsAllowed > 0)
                    {
                        // If max clients is configured and the limit is reached
                        // we need to wait until one of the existing clients closes its connection
                        while (!await _maxClientsSemaphore.WaitAsync(MaxClientsAllowedWaitInterval, _cancellationToken).ConfigureAwait(false))
                        {
                            Logger.LogWarning("Waited {MaxClientsAllowedInterval}, " +
                                               "but we still cannot accept another incoming connection " +
                                               "because the maximum number of clients ({MaxClientsAllowed}) has been reached",
                                MaxClientsAllowedWaitInterval, maxClientsAllowed);
                        }
                    }

                    var tcpClient = await listener
                        .AcceptTcpClientAsync(Options.TcpNoDelay, Options.TcpReceiveBufferSize, Options.TcpSendBufferSize, Logger, _cancellationToken)
                        .ConfigureAwait(false);

                    if (tcpClient != null)
                    {
                        // Process incoming TcpClient in a background task to not block the main listener
                        _ = Task.Run(() =>
                        {
                            INetworkStream networkStream = null;
                            RunningDicomService runningService = null;
                            try
                            {
                                // let the INetworkStream dispose the TcpClient
                                networkStream = _networkManager.CreateNetworkStream(tcpClient, _tlsAcceptor, ownsTcpClient: true);

                                var scp = CreateScp(networkStream);
                                scp.RunsAsServer = true;
                                if (Options != null)
                                {
                                    scp.Options = Options;
                                }

                                var serviceTask = scp.RunAsync();
                                int numberOfServices;
                                runningService = new RunningDicomService(scp, serviceTask);
                                lock (_services)
                                {
                                    _services.Add(runningService);
                                    numberOfServices = _services.Count;
                                }
                                runningService.Task.ContinueWith((t) => RemoveCompletedService(runningService), TaskContinuationOptions.PreferFairness | TaskContinuationOptions.RunContinuationsAsynchronously);

                                Logger.LogDebug(
                                    "Accepted an incoming client connection, there are now {NumberOfServices} connected clients",
                                    numberOfServices);

                                if (maxClientsAllowed > 0 && numberOfServices == maxClientsAllowed)
                                {
                                    Logger.LogWarning(
                                        "Reached the maximum number of simultaneously connected clients, further incoming connections will be blocked until one or more clients disconnect");
                                }
                            }
                            catch (OperationCanceledException)
                            {
                                Logger.LogWarning("Cancellation occurred while accepting an incoming client connection");
                            }
                            catch (Exception e)
                            {
                                Logger.LogError(e, "An exception occurred while accepting an incoming client connection");
                                tcpClient.Close();
                                networkStream?.Dispose();
                                RemoveCompletedService(runningService);
                            }
                        }, _cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                Logger.LogWarning(e, "DICOM server was canceled");
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Exception listening for DICOM services");

                Stop();
                Exception = e;
            }
            finally
            {
                listener?.Stop();
                IsListening = false;
            }
        }

        private void RemoveCompletedService(RunningDicomService runningService)
        {
            // Avoid object disposed exception if we can
            if (!_cancellationToken.IsCancellationRequested)
            {
                _maxClientsSemaphore?.Release(1);
            }
            if (runningService != null)
            {
                lock (_services)
                {
                    _services.Remove(runningService);
                }
            }
        }

        private void ClearServices()
        {
            var servicesToDispose = new List<RunningDicomService>();
            lock (_services)
            {
                servicesToDispose.AddRange(_services);
                _services.Clear();
            }

            foreach (var service in servicesToDispose)
            {
                try
                {
                    service.Dispose();
                }
                catch (Exception e)
                {
                    Logger.LogWarning("An error occurred while trying to dispose a DICOM service: {@Error}", e);
                }
            }
        }

        #endregion

        #region INNER TYPES

        class RunningDicomService : IDisposable
        {
            public DicomService Service { get; }
            public Task Task { get; }

            public RunningDicomService(DicomService service, Task task)
            {
                Service = service ?? throw new ArgumentNullException(nameof(service));
                Task = task ?? throw new ArgumentNullException(nameof(task));
                Task.ContinueWith((t) => Service.Dispose(), TaskContinuationOptions.AttachedToParent);
            }

            public void Dispose() => Service.Dispose();
        }

        #endregion
    }
}
