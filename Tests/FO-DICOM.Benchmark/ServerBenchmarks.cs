// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    [MaxIterationCount(25)]
    public class ServerBenchmarks
    {
        private string _rootPath;
        private DicomFile _sampleFile;
        private IDicomServer _cStoreServer;
        private IDicomServerFactory _dicomServerFactory;
        private IDicomClientFactory _dicomClientFactory;
        private IDicomServer _cEchoServer;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddFellowOakDicom()
                .AddLogManager<LogManager.NullLoggerManager>()
                .Configure<DicomClientOptions>(o =>
                {
                    o.AssociationLingerTimeoutInMs = 0;
                })
                .Configure<DicomServiceOptions>(o =>
                {
                    o.LogDataPDUs = false;
                    o.LogDimseDatasets = false;
                    o.MaxPDULength = 512 * 1024 * 1024;
                });

            var serviceProvider = services.BuildServiceProvider();

            _dicomServerFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            _dicomClientFactory = serviceProvider.GetRequiredService<IDicomClientFactory>();

            _rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _sampleFile = DicomFile.Open(Path.Combine(_rootPath, "Data\\GH355.dcm"));
            _cStoreServer = _dicomServerFactory.Create<NopCStoreProvider>(11112);
            _cEchoServer = _dicomServerFactory.Create<DicomCEchoProvider>(11113);
        }

        [GlobalCleanup]
        public void Teardown()
        {
            _cStoreServer.Stop();
            _cEchoServer.Stop();
            _cStoreServer.Dispose();
            _cEchoServer.Dispose();
        }

        [Benchmark]
        public async Task SendEchoToServer()
        {
            var client = _dicomClientFactory.Create("127.0.0.1", _cEchoServer.Port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            await client.AddRequestAsync(new DicomCEchoRequest());
            await client.SendAsync();
        }

        [Benchmark]
        public async Task SendStoreToServer()
        {
            var client = _dicomClientFactory.Create("127.0.0.1", _cStoreServer.Port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            await client.AddRequestAsync(new DicomCStoreRequest(_sampleFile));
            await client.SendAsync();
        }
    }
}
