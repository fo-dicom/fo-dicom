// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network.Client.Advanced;
using FellowOakDicom.Network.Client.Advanced.Connection;
using Microsoft.Extensions.DependencyInjection;
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
    }

    public static class DicomClientFactory
    {
        /// <summary>
        /// Initializes an instance of <see cref="DicomClient"/> out of DI-container.
        /// </summary>
        /// <param name="host">DICOM host.</param>
        /// <param name="port">Port.</param>
        /// <param name="useTls">True if TLS security should be enabled, false otherwise.</param>
        /// <param name="callingAe">Calling Application Entity Title.</param>
        /// <param name="calledAe">Called Application Entity Title.</param>
        public static IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe)
            => Setup.ServiceProvider
                .GetRequiredService<IDicomClientFactory>().Create(host, port, useTls, callingAe, calledAe);
    }

    public class DefaultDicomClientFactory : IDicomClientFactory
    {
        private readonly IOptions<DicomClientOptions> _defaultClientOptions;
        private readonly IOptions<DicomServiceOptions> _defaultServiceOptions;
        private readonly ILogManager _logManager;
        private readonly IAdvancedDicomClientConnectionFactory _advancedDicomClientConnectionFactory;

        public DefaultDicomClientFactory(
            IOptions<DicomClientOptions> defaultClientOptions,
            IOptions<DicomServiceOptions> defaultServiceOptions,
            ILogManager logManager,
            IAdvancedDicomClientConnectionFactory advancedDicomClientConnectionFactory)
        {
            _defaultClientOptions = defaultClientOptions ?? throw new ArgumentNullException(nameof(defaultClientOptions));
            _defaultServiceOptions = defaultServiceOptions ?? throw new ArgumentNullException(nameof(defaultServiceOptions));
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            _advancedDicomClientConnectionFactory = advancedDicomClientConnectionFactory ?? throw new ArgumentNullException(nameof(advancedDicomClientConnectionFactory));
        }

        public virtual IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe)
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
            var logger = _logManager.GetLogger("Dicom.Network");

            return new DicomClient(host, port, useTls, callingAe, calledAe, clientOptions, serviceOptions, logger, _advancedDicomClientConnectionFactory);
        }
    }
}