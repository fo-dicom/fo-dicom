// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.AspNetCore.Configs;
using FellowOakDicom.Network;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.AspNetCore.Server
{
    class GeneralPurposeDicomServerService : IHostedService
    {
        private IDicomServer _server;
        private readonly IDicomServerFactory _serverFactory;
        private readonly DicomServiceBuilder _serviceBuilder;
        private readonly IOptions<ServerConfiguration> _serverConfiguration;


        public GeneralPurposeDicomServerService(IDicomServerFactory serverFactory, DicomServiceBuilder builder, IOptions<ServerConfiguration> serverConfiguration)
        {
            _serverFactory = serverFactory;
            // TODO: get settings
            _serviceBuilder = builder;
            _serverConfiguration = serverConfiguration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var configuration = _serverConfiguration.Value;
            _serviceBuilder.ConfigureAction?.Invoke(configuration);

            // preload dictionary to prevent tiemouts
            _ = DicomDictionary.Default;

            _server = _serverFactory.Create<GeneralPurposeDicomService>(
                configuration.Port,
                userState: _serviceBuilder
                );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_server != null)
            {
                _server.Stop();
                _server = null;
            }
            return Task.CompletedTask;
        }

    }
}
