// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    public class ServerBenchmarks
    {

        private string _rootpath;
        private DicomFile _sampleFile;
        private IDicomServer _server;

        [GlobalSetup]
        public void Setup()
        {
            _rootpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _sampleFile = DicomFile.Open(Path.Combine(_rootpath, "Data\\GH355.dcm"));
            _server = DicomServerFactory.Create<NopCStoreProvider>(11113);
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
            var port = 11112;
            using (DicomServerFactory.Create<DicomCEchoProvider>(port))
            {
                // do nothing here
            }
        }


        [Benchmark]
        public async Task SendEchoToServer()
        {
            var port = 11112;
            using (var server = DicomServerFactory.Create<DicomCEchoProvider>(port))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.ServiceOptions.LogDimseDatasets = false;
                client.ServiceOptions.LogDataPDUs = false;
                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();
                server.Stop();
            }
        }


        [Benchmark]
        public async Task SendStoreToNewServer()
        {
            var port = 11112;
            using (var server = DicomServerFactory.Create<NopCStoreProvider>(port))
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.ServiceOptions.LogDimseDatasets = false;
                client.ServiceOptions.LogDataPDUs = false;
                await client.AddRequestAsync(new DicomCStoreRequest(_sampleFile));
                await client.SendAsync();
                server.Stop();
            }
        }

        [Benchmark]
        public async Task SendStoreToExistingServer()
        {
            var client = DicomClientFactory.Create("127.0.0.1", _server.Port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.LogDimseDatasets = false;
            client.ServiceOptions.LogDataPDUs = false;
            await client.AddRequestAsync(new DicomCStoreRequest(_sampleFile));
            await client.SendAsync();
        }

    }
}
