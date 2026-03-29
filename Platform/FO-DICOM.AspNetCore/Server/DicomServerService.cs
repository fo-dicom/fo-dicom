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
    class DicomServerService<T> : IHostedService where T : DicomService, IDicomServiceProvider
    {
        private IDicomServer _server;
        private readonly IDicomServerFactory _serverFactory;
        private readonly IOptions<ServerConfiguration> _serverConfiguration;

        public DicomServerService(IDicomServerFactory serverFactory, IOptions<ServerConfiguration> serverConfiguration)
        {
            _serverFactory = serverFactory;
            _serverConfiguration = serverConfiguration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // preload dictionary to prevent tiemouts
            _ = DicomDictionary.Default;
            _server = _serverFactory.Create<T>(
                _serverConfiguration.Value.Port
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
