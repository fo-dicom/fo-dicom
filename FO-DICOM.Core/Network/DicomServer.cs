// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network.Tls;
using FellowOakDicom.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Representation of a DICOM server.
    /// </summary>
    /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
    public class DicomServer<T> : IDicomServer<T> where T : DicomService, IDicomServiceProvider
    {
        #region FIELDS
        
        private readonly INetworkManager _networkManager;
        
        private readonly ILoggerFactory _loggerFactory;

        private readonly List<RunningDicomService> _services;

        private readonly CancellationTokenSource _cancellationSource;       

        private readonly CancellationToken _cancellationToken;

        private readonly Channel<int> _servicesChannel = Channel.CreateUnbounded<int>(new UnboundedChannelOptions
        {
            SingleWriter = false,
            SingleReader = true
        });

        /// <summary>
        /// A task that will complete when the server is stopped
        /// </summary>
        private readonly TaskCompletionSource<bool> _stopped;
        
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
            _stopped = TaskCompletionSourceFactory.Create<bool>();
            
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
            set => _logger  = value;
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
            return Task.WhenAll(ListenForConnectionsAsync(), RemoveUnusedServicesAsync());
        }

        /// <inheritdoc />
        public virtual void Stop()
        {
            if (!_cancellationSource.IsCancellationRequested)
            {
                _cancellationSource.Cancel();
                _stopped.TrySetResult(true);
            }
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
                _servicesChannel.Writer.TryComplete();
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

                    var networkStream = await listener
                        .AcceptNetworkStreamAsync(_tlsAcceptor, Options.TcpNoDelay, Options.TcpReceiveBufferSize, Options.TcpSendBufferSize, Logger, _cancellationToken)
                        .ConfigureAwait(false);

                    if (networkStream != null)
                    {
                        var scp = CreateScp(networkStream);
                        if (Options != null)
                        {
                            scp.Options = Options;
                        }

                        var serviceTask = scp.RunAsync();
                        int numberOfServices;
                        lock (_services)
                        {
                            _services.Add(new RunningDicomService(scp, serviceTask));
                            numberOfServices = _services.Count;
                        }

                        Logger.LogDebug("Accepted an incoming client connection, there are now {NumberOfServices} connected clients", numberOfServices);

                        // We don't actually care about the values inside the channel, they just serve as a notification that a service has connected
                        // Fire and forget
                        _ = _servicesChannel.Writer.WriteAsync(numberOfServices, _cancellationToken);
                        
                        if (maxClientsAllowed > 0 && numberOfServices == maxClientsAllowed)
                        {
                            Logger.LogWarning("Reached the maximum number of simultaneously connected clients, further incoming connections will be blocked until one or more clients disconnect");
                        }
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

        /// <summary>
        /// Remove no longer used client connections.
        /// </summary>
        private async Task RemoveUnusedServicesAsync()
        {
            int maxClientsAllowed = _serverOptions.MaxClientsAllowed;
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Logger.LogDebug("Waiting for incoming client connections");
                    
                    // First, we wait until at least one service is running
                    // We don't actually care about the values inside the channel, they just serve as a notification that a service has connected
                    // It is also possible that the DICOM server is stopped while are waiting here
                    var aServiceHasStarted = _servicesChannel.Reader.ReadAsync(_cancellationToken).AsTask();
                    await Task.WhenAny(aServiceHasStarted, _stopped.Task).ConfigureAwait(false);
                    _cancellationToken.ThrowIfCancellationRequested();

                    // Then, we wait until at least one service completes
                    // We must take into account that more services can start while we wait here
                    while (true)
                    {
                        List<RunningDicomService> runningDicomServices;
                        
                        while (_servicesChannel.Reader.TryRead(out _))
                        {
                            // Discard queued new services, we're only interested in new arrivals after we start waiting                            
                        }
                        lock (_services)
                        {
                            runningDicomServices = _services.ToList();
                        }
                        var numberOfDicomServices = runningDicomServices.Count;
                        Logger.LogDebug("There are {NumberOfDicomServices} running DICOM services", numberOfDicomServices);
                        if (numberOfDicomServices == 0)
                        {
                            // No more services at all? Exit early
                            break;
                        }

                        var tasks = new List<Task>(numberOfDicomServices + 1);
                        var anotherServiceHasStarted = _servicesChannel.Reader.ReadAsync(_cancellationToken).AsTask();
                        tasks.Add(anotherServiceHasStarted);
                        tasks.AddRange(runningDicomServices.Select(s => s.Task));
                        var winner = await Task.WhenAny(tasks).ConfigureAwait(false);
                        if (winner == anotherServiceHasStarted)
                        {
                            try
                            {
                                await anotherServiceHasStarted;
                            }
                            catch(OperationCanceledException)
                            {
                                // If the server is disposed while we were waiting, deal with that gracefully
                                break;
                            }
                            catch (ChannelClosedException)
                            {
                                // If the server is disposed while we were waiting, deal with that gracefully
                                break;
                            }
                            
                            // If another service started, we must restart the Task.WhenAny with the new set of running service tasks
                            Logger.LogDebug("Another DICOM service has started while the cleanup was waiting for one or more DICOM services to complete");
                        }
                        else
                        {
                            Logger.LogDebug("One or more running DICOM services have completed");
                            break;
                        }
                    }
                    
                    int numberOfRemainingServices;
                    var servicesToDispose = new List<RunningDicomService>();
                    lock (_services)
                    {
                        for (var i = _services.Count - 1; i >= 0; i--)
                        {
                            var service = _services[i];
                            if (service.Task.IsCompleted)
                            {
                                _services.RemoveAt(i);
                                servicesToDispose.Add(service);
                            }
                        }

                        numberOfRemainingServices = _services.Count;
                    }
                    var numberOfCompletedServices = servicesToDispose.Count;
                    foreach (var service in servicesToDispose)
                    {
                        try
                        {
                            service.Dispose();
                        }
                        catch (Exception e)
                        {
                            Logger.LogWarning("An error occurred while trying to dispose a completed DICOM service: {@Error}", e);
                        }
                    }

                    // Avoid object disposed exception if we can
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        _maxClientsSemaphore?.Release(numberOfCompletedServices);
                    }

                    Logger.LogDebug("Cleaned up {NumberOfCompletedServices} completed DICOM services", numberOfCompletedServices);
                    if (numberOfRemainingServices > 0)
                    {
                        Logger.LogDebug("There are still {NumberOfRemainingServices} clients connected now", numberOfRemainingServices);    
                    }
                    else
                    {
                        Logger.LogDebug("There are no clients connected now");
                    }

                    if (maxClientsAllowed > 0)
                    {
                        if (numberOfRemainingServices == maxClientsAllowed)
                        {
                            Logger.LogDebug("Cannot accept more incoming client connections until one or more clients disconnect");
                        }
                        else 
                        {
                            var numberOfExtraClientsAllowed = maxClientsAllowed - numberOfRemainingServices;
                            Logger.LogDebug(
                                "{NumberOfExtraServicesAllowed} more incoming client connections are allowed",
                                numberOfExtraClientsAllowed);
                        }
                    }
                    else
                    {
                        Logger.LogDebug("Unlimited more incoming client connections are allowed");
                    }
                }
                catch (ChannelClosedException)
                {
                    Logger.LogInformation("Disconnected client cleanup manually terminated");
                    ClearServices();
                }
                catch (OperationCanceledException)
                {
                    Logger.LogInformation("Disconnected client cleanup manually terminated");
                    ClearServices();
                }
                catch (Exception e)
                {
                    Logger.LogWarning(e, "Exception removing disconnected clients");
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
                Task.ContinueWith((t) => Service.Dispose());
            }

            public void Dispose() => Service.Dispose();
        }

        #endregion
    }
}
