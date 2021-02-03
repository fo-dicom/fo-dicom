// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        private readonly ILogManager _logManager;
        private readonly INetworkManager _networkManager;
        private readonly ITranscoderManager _transcoderManager;
        private readonly IOptions<DicomClientOptions> _defaultClientOptions;
        private readonly IOptions<DicomServiceOptions> _defaultServiceOptions;

        public DefaultDicomClientFactory(
            ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager,
            IOptions<DicomClientOptions> defaultClientOptions,
            IOptions<DicomServiceOptions> defaultServiceOptions
            )
        {
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            _networkManager = networkManager ?? throw new ArgumentNullException(nameof(networkManager));
            _transcoderManager = transcoderManager ?? throw new ArgumentNullException(nameof(transcoderManager));
            _defaultClientOptions = defaultClientOptions ?? throw new ArgumentNullException(nameof(defaultClientOptions));
            _defaultServiceOptions = defaultServiceOptions ?? throw new ArgumentNullException(nameof(defaultServiceOptions));
        }

        public virtual IDicomClient Create(string host, int port, bool useTls, string callingAe, string calledAe)
        {
            var clientOptions = _defaultClientOptions.Value.Clone();
            var serviceOptions = _defaultServiceOptions.Value.Clone();

            return new DicomClient(host, port, useTls, callingAe, calledAe, clientOptions, serviceOptions, _networkManager, _logManager, _transcoderManager);
        }
    }
}