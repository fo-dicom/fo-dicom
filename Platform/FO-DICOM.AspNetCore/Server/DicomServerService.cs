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
    class DicomServerService<T> : IHostedService where T : DicomService, IDicomServiceProvider
    {
        private IDicomServer _server;
        private IDicomServerFactory _serverFactory;
        private IConfiguration _configuration;

        public DicomServerServiceOptions Options { get; set; } = new DicomServerServiceOptions();

        public DicomServerService(IConfiguration configuration, IDicomServerFactory serverFactory)
        {
            _serverFactory = serverFactory;
            // TODO: get settings
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // preload dictionary to prevent tiemouts
            _ = DicomDictionary.Default;
            _server = _serverFactory.Create<T>(
                Options.Port
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
