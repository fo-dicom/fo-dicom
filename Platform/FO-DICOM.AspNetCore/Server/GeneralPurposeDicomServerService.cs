// Copyright (c) 2012-2025 fo-dicom contributors.
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
        private readonly IOptions<DicomConfiguration> _options;


        public GeneralPurposeDicomServerService(IDicomServerFactory serverFactory, DicomServiceBuilder builder, IOptions<DicomConfiguration> serviceOptions)
        {
            _serverFactory = serverFactory;
            // TODO: get settings
            _serviceBuilder = builder;
            _options = serviceOptions;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var options = _options.Value;
            _serviceBuilder.ConfigureAction?.Invoke(options);

            // preload dictionary to prevent tiemouts
            _ = DicomDictionary.Default;

            _server = _serverFactory.Create<GeneralPurposeDicomService>(
                options.Server.Port,
                userState: _serviceBuilder,
                configure: options.ServerOptions.CopyTo
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
