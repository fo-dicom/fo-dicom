// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Network.Tls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace FellowOakDicom.Network.Client
{
    public interface IDicomClientFactory
    {
        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe);

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="tlsInitiator">The handler to initialte TLS security, if null then no TLS is enabled.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        IDicomClient Create(string host, int port, ITlsInitiator tlsInitiator, string callingAe, string calledAe);

    }

    public static class DicomClientFactory
    {

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/>.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public static IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe)
            => Setup.ServiceProvider
                .GetRequiredService<IDicomClientFactory>().Create(host, port, useTls, callingAe, calledAe);

        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/> out of DI-container.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="tlsInitiator">The handler to initialte TLS security, if null then no TLS is enabled.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public static IDicomClient Create(string host, int port, ITlsInitiator tlsInitiator, string callingAe, string calledAe)
            => Setup.ServiceProvider
                .GetRequiredService<IDicomClientFactory>().Create(host, port, tlsInitiator, callingAe, calledAe);
    }

    public class DefaultDicomClientFactory : IDicomClientFactory
    {
        private readonly IOptions<DicomClientOptions> _defaultClientOptions;
        private readonly IOptions<DicomServiceOptions> _defaultServiceOptions;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;
        private readonly IServiceProvider _serviceProvider;

        public DefaultDicomClientFactory(
            IOptions<DicomClientOptions> defaultClientOptions,
            IOptions<DicomServiceOptions> defaultServiceOptions,
            ILoggerFactory loggerFactory,
            IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory,
            IServiceProvider serviceProvider)
        {
            _defaultClientOptions = defaultClientOptions ?? throw new ArgumentNullException(nameof(defaultClientOptions));
            _defaultServiceOptions = defaultServiceOptions ?? throw new ArgumentNullException(nameof(defaultServiceOptions));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
            _serviceProvider = serviceProvider;
        }

        public virtual IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            ITlsInitiator tlsInitiator= null;
            if (useTls)
            {
                // if Tls has to be active, use the initiator from DI, otherwise the DefaulttlsInitialter
                tlsInitiator = _serviceProvider.GetService<ITlsInitiator>() ?? new DefaultTlsInitiator();
            }
            return Create(host, port, tlsInitiator, callingAe, calledAe);
        }

        public virtual IDicomClient Create(string host, int port, ITlsInitiator tlsInitiator, string callingAe, string calledAe)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (callingAe != null && callingAe.Length > DicomVR.AE.MaximumLength)
            {
                throw new ArgumentException($"Calling AE '{callingAe}' is {callingAe.Length} characters long, " +
                                            $"which is longer than the maximum allowed length ({DicomVR.AE.MaximumLength} characters)");
            }

            if (calledAe != null && calledAe.Length > DicomVR.AE.MaximumLength)
            {
                throw new ArgumentException($"Called AE '{calledAe}' is {calledAe.Length} characters long, " +
                                            $"which is longer than the maximum allowed length ({DicomVR.AE.MaximumLength} characters)");
            }

            var clientOptions = _defaultClientOptions.Value.Clone();
            var serviceOptions = _defaultServiceOptions.Value.Clone();

            return new DicomClient(host, port, tlsInitiator, callingAe, calledAe, clientOptions, serviceOptions, _loggerFactory, _advancedDicomClientConnectionFactory);
        }
    }
}