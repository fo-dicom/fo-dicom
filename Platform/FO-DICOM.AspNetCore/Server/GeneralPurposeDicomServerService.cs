// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.AspNetCore.Server
{
    class GeneralPurposeDicomServerService : IHostedService
    {
        private IDicomServer _server;
        private readonly IDicomServerFactory _serverFactory;
        private readonly IConfiguration _configuration;
        private readonly DicomServiceBuilder _serviceBuilder;

        public DicomServerServiceOptions Options { get; set; } = new DicomServerServiceOptions();

        public GeneralPurposeDicomServerService(IConfiguration configuration, IDicomServerFactory serverFactory, DicomServiceBuilder builder)
        {
            _serverFactory = serverFactory;
            // TODO: get settings
            _configuration = configuration;
            _serviceBuilder = builder;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // preload dictionary to prevent tiemouts
            _ = DicomDictionary.Default;
            _server = _serverFactory.Create<GeneralPurposeDicomService>(
                Options.Port,
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
