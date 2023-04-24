// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    public class DicomServer<T> : IDicomServer<T> where T : DicomService, IDicomServiceProvider
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

        private string _certificateName;

        private Encoding _fallbackEncoding;

        private bool _isIpAddressSet;

        private bool _isPortSet;

        private bool _wasStarted;

        private bool _disposed;

        private readonly Tools.AsyncManualResetEvent _hasServicesFlag;

        private readonly Tools.AsyncManualResetEvent _hasNonMaxServicesFlag;

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

            _hasServicesFlag = new Tools.AsyncManualResetEvent(false);
            _hasNonMaxServicesFlag = new Tools.AsyncManualResetEvent(true);
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
        
        /// <summary>
        /// Gets whether the list of services contains the maximum number of services or not.
        /// </summary>
        private bool IsServicesAtMax
        {
            get
            {
                var maxClientsAllowed = Options.MaxClientsAllowed;
                if (maxClientsAllowed <= 0)
                    return false;

                lock (_services)
                {
                    return _services.Count >= maxClientsAllowed;
                }
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
        public void Dispose() => Dispose(true);

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
                var noDelay = Options.TcpNoDelay;

                listener = _networkManager.CreateNetworkListener(IPAddress, Port);
                await listener.StartAsync().ConfigureAwait(false);
                IsListening = true;

                while (!_cancellationToken.IsCancellationRequested)
                {
                    // If max clients is configured and the limit is reached
                    // we need to wait until one of the existing clients closes its connection
                    while (true)
                    {
                        // Instead of simply waiting for the flag
                        // We use Task.WhenAny with a one minute delay in a while loop
                        // This allows us to log a warning every minute instead of silently not accepting connections
                        var hasNonMaxServicesFlag = _hasNonMaxServicesFlag.WaitAsync();
                        var oneMinuteDelay = Task.Delay(60 * 1000, _cancellationToken);
                        var winner = await Task.WhenAny(hasNonMaxServicesFlag, oneMinuteDelay).ConfigureAwait(false);
                        if (winner == hasNonMaxServicesFlag)
                        {
                            break;
                        }
                        // Allow proper triggering of the OperationCanceledException, if any
                        await oneMinuteDelay.ConfigureAwait(false);
                        _logger.LogWarning("Cannot accept another incoming connection because the maximum number of clients ({MaxClientsAllowed}) has been reached", Options.MaxClientsAllowed);
                    }

                    var networkStream = await listener
                        .AcceptNetworkStreamAsync(_certificateName, noDelay, _cancellationToken)
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
                        
                        _logger.LogDebug("Accepted an incoming client connection, there are now {NumberOfServices} connected clients", numberOfServices);
                        
                        _hasServicesFlag.Set();
                        if (IsServicesAtMax)
                        {
                            _logger.LogWarning("Reached the maximum number of simultaneously connected clients, further incoming connections will be blocked until one or more clients disconnect");
                            _hasNonMaxServicesFlag.Reset();
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
            while (!_cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogDebug("Waiting for incoming client connections");
                    
                    await _hasServicesFlag.WaitAsync().ConfigureAwait(false);
                    
                    List<Task> runningDicomServiceTasks;
                    lock (_services)
                    {
                        runningDicomServiceTasks = _services.Select(s => s.Task).ToList();
                    }
                    var numberOfDicomServices = runningDicomServiceTasks.Count;
                    _logger.LogDebug("There are {NumberOfDicomServices} running DICOM services", numberOfDicomServices);
                    if (numberOfDicomServices > 0)
                    {
                        await Task.WhenAny(runningDicomServiceTasks).ConfigureAwait(false);
                    }

                    var isHasNonMaxServicesFlagSet = false;
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

                        if (_services.Count == 0)
                        {
                            _hasServicesFlag.Reset();
                        }

                        if (!IsServicesAtMax)
                        {
                            _hasNonMaxServicesFlag.Set();
                            isHasNonMaxServicesFlagSet = true;
                        }
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
                            _logger.LogWarning("An error occurred while trying to dispose a completed DICOM service: {@Error}", e);
                        }
                    }

                    _logger.LogDebug("Cleaned up {NumberOfCompletedServices} completed DICOM services", numberOfCompletedServices);
                    if (numberOfRemainingServices > 0)
                    {
                        _logger.LogDebug("There are still {NumberOfRemainingServices} clients connected now", numberOfRemainingServices);    
                    }
                    else
                    {
                        _logger.LogDebug("There are no clients connected now");
                    }
                    
                    if (isHasNonMaxServicesFlagSet)
                    {
                        if (Options.MaxClientsAllowed > 0)
                        {
                            var numberOfExtraClientsAllowed = Options.MaxClientsAllowed - numberOfRemainingServices;
                            _logger.LogDebug("{NumberOfExtraServicesAllowed} more incoming client connections are allowed", numberOfExtraClientsAllowed);
                        }
                        else
                        {
                            _logger.LogDebug("Unlimited more incoming client connections are allowed");
                        }
                    }
                    else
                    {
                        _logger.LogDebug("Cannot accept more incoming client connections until one or more clients disconnect");    
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.LogInformation("Disconnected client cleanup manually terminated.");
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
                    _logger.LogWarning("An error occurred while trying to dispose a DICOM service: {@Error}", e);
                }
            }
            
            _hasServicesFlag.Reset();
            _hasNonMaxServicesFlag.Set();
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
            }

            public void Dispose() => Service.Dispose();
        }

        #endregion
    }
}
