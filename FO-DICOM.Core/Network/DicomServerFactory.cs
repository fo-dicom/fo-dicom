// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;
using FellowOakDicom.Network.Tls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FellowOakDicom.Network
{
    public interface IDicomServerFactory
    {
        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <param name="port">Port to listen to.</param>
        /// <param name="tlsAcceptor">Handler to accept authenticated connections.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <param name="userState">Optional parameters</param>
        /// <param name="configure">Configures the service options of the newly created DICOM server</param>
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        IDicomServer Create<T>(
            int port,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null,
            object userState = null,
            Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider;
        
        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <param name="ipAddress">IP address(es) to listen to. Value <code>null</code> applies default, IPv4Any.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="tlsAcceptor">Handler to accept authenticated connections.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <param name="userState">Optional parameters.</param>
        /// <param name="configure">Configures the service options of the newly created DICOM server</param>
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        IDicomServer Create<T>(
            string ipAddress,
            int port,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null,
            object userState = null,
            Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider;
        
        /// <summary>
        /// Creates a DICOM server object.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <typeparam name="TServer">Concrete DICOM server type to be returned.</typeparam>
        /// <param name="ipAddress">IP address(es) to listen to. Value <code>null</code> applies default, IPv4Any.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="userState">Optional parameters</param>
        /// <param name="tlsAcceptor">Handler to accept authenticated connections.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <param name="configure">Configures the service options of the newly created DICOM server</param>
        /// <returns>An instance of <typeparamref name="TServer"/>, that starts listening for connections in the background.</returns>
        IDicomServer Create<T, TServer>(
            string ipAddress,
            int port,
            object userState = null,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null,
            Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider where TServer : IDicomServer<T>;
    }

    public static class DicomServerFactory
    {
        /// <summary>
        /// Creates a DICOM server object out of DI-container.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <param name="port">Port to listen to.</param>
        /// <param name="tlsAcceptor">Handler to accept authenticated connections.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <param name="userState">Optional parameters</param>
        /// <param name="configure">Configures the service options of the newly created DICOM server</param>
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        public static IDicomServer Create<T>(
            int port,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null,
            object userState = null,
            Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider
            => Setup.ServiceProvider
            .GetRequiredService<IDicomServerFactory>().Create<T>(port, tlsAcceptor, fallbackEncoding, logger, userState, configure);

        /// <summary>
        /// Creates a DICOM server object out of DI-container.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <param name="ipAddress">IP address(es) to listen to. Value <code>null</code> applies default, IPv4Any.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="tlsAcceptor">Handler to accept authenticated connections.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="userState">Optional parameters</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <param name="configure">Configures the service options of the newly created DICOM server</param>
        /// <returns>An instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.</returns>
        public static IDicomServer Create<T>(
            string ipAddress,
            int port,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null,
            object userState = null,
            Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider
            => Setup.ServiceProvider
            .GetRequiredService<IDicomServerFactory>().Create<T>(ipAddress, port, tlsAcceptor, fallbackEncoding, logger, userState, configure);

        /// <summary>
        /// Creates a DICOM server object out of DI-container.
        /// </summary>
        /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
        /// <typeparam name="TServer">Concrete DICOM server type to be returned.</typeparam>
        /// <param name="ipAddress">IP address(es) to listen to. Value <code>null</code> applies default, IPv4Any.</param>
        /// <param name="port">Port to listen to.</param>
        /// <param name="userState">Optional optional parameters.</param>
        /// <param name="tlsAcceptor">Handler to accept authenticated connections.</param>
        /// <param name="fallbackEncoding">Fallback encoding.</param>
        /// <param name="logger">Logger, if null default logger will be applied.</param>
        /// <param name="configure">Configures the service options of the newly created DICOM server</param>
        /// <returns>An instance of <typeparamref name="TServer"/>, that starts listening for connections in the background.</returns>
        public static IDicomServer Create<T, TServer>(
            string ipAddress,
            int port,
            object userState = null,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null,
            Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider where TServer : IDicomServer<T>
            => Setup.ServiceProvider
            .GetRequiredService<IDicomServerFactory>().Create<T, TServer>(ipAddress, port, userState, tlsAcceptor, fallbackEncoding, logger, configure);
    }

    public class DefaultDicomServerFactory : IDicomServerFactory
    {
        private readonly IServiceScopeFactory  _serviceScopeFactory;
        private readonly IDicomServerRegistry _dicomServerRegistry;
        private readonly IOptions<DicomServiceOptions> _defaultServiceOptions;
        private readonly IOptions<DicomServerOptions> _defaultServerOptions;

        public DefaultDicomServerFactory(
            IServiceScopeFactory  serviceScopeFactory,
            IDicomServerRegistry dicomServerRegistry,
            IOptions<DicomServiceOptions> defaultServiceOptions, 
            IOptions<DicomServerOptions> defaultServerOptions)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _dicomServerRegistry = dicomServerRegistry ?? throw new ArgumentNullException(nameof(dicomServerRegistry));
            _defaultServiceOptions = defaultServiceOptions ?? throw new ArgumentNullException(nameof(defaultServiceOptions));
            _defaultServerOptions = defaultServerOptions ?? throw new ArgumentNullException(nameof(defaultServerOptions));
        }

        public IDicomServer Create<T>(
            int port,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null, 
            ILogger logger = null,
            object userState = null,
            Action<DicomServerOptions> configure = null)
            where T : DicomService, IDicomServiceProvider 
            => Create<T, DicomServer<T>>(NetworkManager.IPv4Any, port, userState, tlsAcceptor, fallbackEncoding, logger, configure);

        public IDicomServer Create<T>(
            string ipAddress,
            int port, 
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null, 
            object userState = null,
            Action<DicomServerOptions> configure = null) where T : DicomService, IDicomServiceProvider
            => Create<T, DicomServer<T>>(ipAddress, port, userState, tlsAcceptor, fallbackEncoding, logger, configure);

        public virtual IDicomServer Create<TServiceProvider, TServer>(
            string ipAddress,
            int port,
            object userState = null,
            ITlsAcceptor tlsAcceptor = null,
            Encoding fallbackEncoding = null,
            ILogger logger = null,
            Action<DicomServerOptions> configure = null) where TServiceProvider : DicomService, IDicomServiceProvider where TServer : IDicomServer<TServiceProvider>
        {
            var dicomServerScope = _serviceScopeFactory.CreateScope();
            
            if (!_dicomServerRegistry.IsAvailable(port, ipAddress))
            {
                throw new DicomNetworkException($"There is already a DICOM server registered on port: {port}");
            }

            var creator = ActivatorUtilities.CreateFactory(typeof(TServer), Array.Empty<Type>());
            var server = (TServer) creator(dicomServerScope.ServiceProvider, Array.Empty<object>());
            
            if (logger != null)
            {
                server.Logger = logger;
            }
            server.ServiceScope = dicomServerScope;

            var serviceOptions = _defaultServiceOptions.Value.Clone();
            var serverOptions = _defaultServerOptions.Value.Clone();

            // if not explicitly set, try to get a tls handler from DI container
            tlsAcceptor ??= dicomServerScope.ServiceProvider.GetService<ITlsAcceptor>();

            configure?.Invoke(serverOptions);

            var runner = server.StartAsync(ipAddress, port, tlsAcceptor, fallbackEncoding, serviceOptions, userState, serverOptions);

            if (server.Exception != null)
            {
                server.Dispose();
                throw new DicomNetworkException("Failed to start DICOM server", server.Exception);
            }

            var registration = _dicomServerRegistry.Register(server, runner);

            server.Registration = registration;

            return server;
        }
    }
}
