// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.Benchmark.Infrastructure;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class ServerBenchmarks
    {
        private string _rootPath;
        private DicomFile _sampleFile;
        private IDicomServer _server;
        private IDicomServerFactory _dicomServerFactory;
        private IDicomClientFactory _dicomClientFactory;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddFellowOakDicom()
                .AddLogManager<LogManager.NullLoggerManager>()
                .Configure<DicomClientOptions>(o => o.AssociationLingerTimeoutInMs = 0);

            var serviceProvider = services.BuildServiceProvider();

            _dicomServerFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            _dicomClientFactory = serviceProvider.GetRequiredService<IDicomClientFactory>();

            _rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _sampleFile = DicomFile.Open(Path.Combine(_rootPath, "Data\\GH355.dcm"));
            _server = _dicomServerFactory.Create<NopCStoreProvider>(Ports.GetNext());
        }

        [GlobalCleanup]
        public void Teardown()
        {
            _server.Stop();
            _server.Dispose();
        }

        [Benchmark]
        public void StartServer()
        {
            var port = Ports.GetNext();
            using var server = _dicomServerFactory.Create<DicomCEchoProvider>(port);
            server.Stop();
        }

        [Benchmark]
        public async Task SendEchoToServer()
        {
            var port = Ports.GetNext();
            using var server = _dicomServerFactory.Create<DicomCEchoProvider>(port);
            var client = _dicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            await client.AddRequestAsync(new DicomCEchoRequest());
            await client.SendAsync();
            server.Stop();
        }

        [Benchmark]
        public async Task SendStoreToNewServer()
        {
            var port = Ports.GetNext();
            using var server = _dicomServerFactory.Create<NopCStoreProvider>(port);
            var client = _dicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            await client.AddRequestAsync(new DicomCStoreRequest(_sampleFile));
            await client.SendAsync();
            server.Stop();
        }

        [Benchmark]
        public async Task SendStoreToExistingServer()
        {
            var client = _dicomClientFactory.Create("127.0.0.1", _server.Port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            await client.AddRequestAsync(new DicomCStoreRequest(_sampleFile));
            await client.SendAsync();
        }

    }
}
