// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    [MaxIterationCount(25)]
    [MaxWarmupCount(10)]
    [InvocationCount(128,16)]
    public class ServerBenchmarks
    {
        private string _rootPath;
        private DicomFile _sampleFile;
        private IDicomServer _cStoreServer;
        private IDicomServerFactory _dicomServerFactory;
        private IDicomClientFactory _dicomClientFactory;
        private IDicomServer _cEchoServer;
        private IDicomClient _cEchoClient;
        private IDicomClient _cStoreClient;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddFellowOakDicom()
                .Configure<DicomClientOptions>(o =>
                {
                    o.AssociationLingerTimeoutInMs = 0;
                })
                .Configure<DicomServiceOptions>(o =>
                {
                    o.LogDataPDUs = false;
                    o.LogDimseDatasets = false;
                    o.MaxPDULength = 512 * 1024 * 1024;
                })
                .AddSingleton<ILoggerFactory, NullLoggerFactory>();

            var serviceProvider = services.BuildServiceProvider();

            _dicomServerFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            _dicomClientFactory = serviceProvider.GetRequiredService<IDicomClientFactory>();

            _rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _sampleFile = DicomFile.Open(Path.Combine(_rootPath, "Data\\GH355.dcm"));
            _cStoreServer = _dicomServerFactory.Create<NopCStoreProvider>(11118);
            _cEchoServer = _dicomServerFactory.Create<DicomCEchoProvider>(11119);
            _cEchoClient = _dicomClientFactory.Create("127.0.0.1", _cEchoServer.Port, false, "SCU", "ANY-SCP");
            _cEchoClient.ServiceOptions.LogDimseDatasets = false;
            _cEchoClient.ServiceOptions.LogDataPDUs = false;
            _cEchoClient.ClientOptions.AssociationLingerTimeoutInMs = 0;
            _cEchoClient.ClientOptions.MaximumNumberOfRequestsPerAssociation = 1;
            _cStoreClient = _dicomClientFactory.Create("127.0.0.1", _cStoreServer.Port, false, "SCU", "ANY-SCP");
            _cStoreClient.ServiceOptions.LogDimseDatasets = false;
            _cStoreClient.ServiceOptions.LogDataPDUs = false;
            _cStoreClient.ClientOptions.AssociationLingerTimeoutInMs = 0;
            _cStoreClient.ClientOptions.MaximumNumberOfRequestsPerAssociation = 1;
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
            await _cEchoClient.AddRequestAsync(new DicomCEchoRequest());
            await _cEchoClient.SendAsync();
        }

        [Benchmark]
        public async Task SendStoreToServer()
        {
            await _cStoreClient.AddRequestAsync(new DicomCStoreRequest(_sampleFile));
            await _cStoreClient.SendAsync();
        }
    }
}
