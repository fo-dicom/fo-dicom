// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom.Benchmark
{
    [MemoryDiagnoser]
    [MaxIterationCount(20)]
    [MaxWarmupCount(10)]
    public class ServerBenchmarks
    {
        private const int NumberOfServers = 1000;
        private string _rootPath;
        private DicomFile _sampleFile;
        private int _currentServer;
        private IDicomServer[] _cStoreServers;
        private IDicomServer[] _cEchoServers;
        private IDicomClient[] _cStoreClients;
        private IDicomClient[] _cEchoClients;
        private AdvancedDicomClientConnectionRequest _cEchoAdvancedConnectionRequest;
        private AdvancedDicomClientConnectionRequest _cStoreAdvancedConnectionRequest;
        private AdvancedDicomClientAssociationRequest _cStoreAdvancedAssociationRequest;
        private IAdvancedDicomClient _advancedDicomClient;

        [Params("DicomClient", "AdvancedDicomClient")]
        public string Client { get; set; }

        [Params(1,100)]
        public int NumberOfRequests { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddFellowOakDicom()
                .AddLogManager<NullLoggerManager>()
                .Configure<DicomClientOptions>(o =>
                {
                    o.AssociationLingerTimeoutInMs = 0;
                })
                .Configure<DicomServiceOptions>(o =>
                {
                    o.LogDataPDUs = false;
                    o.LogDimseDatasets = false;
                    o.MaxPDULength = 512 * 1024 * 1024;
                    o.RequestTimeout = null;
                    o.TcpNoDelay = true;
                });

            var serviceProvider = services.BuildServiceProvider();

            var dicomServerFactory = serviceProvider.GetRequiredService<IDicomServerFactory>();
            var dicomClientFactory = serviceProvider.GetRequiredService<IDicomClientFactory>();
            var advancedDicomClientFactory = serviceProvider.GetRequiredService<IAdvancedDicomClientFactory>();

            _rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _sampleFile = DicomFile.Open(Path.Combine(_rootPath, "Data\\ct.dcm"));
            _advancedDicomClient = advancedDicomClientFactory.Create(new AdvancedDicomClientCreationRequest());

            // Setup servers
            int port = 11112;
            _cStoreServers = new IDicomServer[NumberOfServers];
            _cEchoServers = new IDicomServer[NumberOfServers];
            for (int i = 0; i < NumberOfServers; i++)
            {
                port++;
                IDicomServer server = null;
                while (server == null)
                {
                    try
                    {
                        server = dicomServerFactory.Create<NopCStoreProvider>(port);
                    }
                    catch (Exception)
                    {
                        // Assume port is occupied
                        port++;
                    }
                }

                _cStoreServers[i] = server;
            }
            for (int i = 0; i < _cEchoServers.Length; i++)
            {
                port++;
                IDicomServer server = null;
                while (server == null)
                {
                    try
                    {
                        server = dicomServerFactory.Create<DicomCEchoProvider>(port);
                    }
                    catch (Exception)
                    {
                        // Assume port is occupied
                        port++;
                    }
                }

                _cEchoServers[i] = server;
            }

            // Setup DicomClient
            _cStoreClients = new IDicomClient[_cStoreServers.Length];
            for (int i = 0; i < _cStoreServers.Length; i++)
            {
                var server = _cStoreServers[i];
                var client = dicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(100, 100);
                _cStoreClients[i] = client;
            }
            _cEchoClients = new IDicomClient[_cEchoServers.Length];
            for (int i = 0; i < _cEchoServers.Length; i++)
            {
                var server = _cEchoServers[i];
                var client = dicomClientFactory.Create("127.0.0.1", server.Port, false, "SCU", "ANY-SCP");
                client.NegotiateAsyncOps(100, 100);
                _cEchoClients[i] = client;
            }

            // Setup AdvancedDicomClient
            _cStoreAdvancedConnectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    NoDelay = true,
                    UseTls = false
                }
            };
            _cEchoAdvancedConnectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    NoDelay = true,
                    UseTls = false
                }
            };
            _cStoreAdvancedAssociationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "SCU",
                CalledAE = "ANY-SCP",
                MaxAsyncOpsInvoked = 100,
                MaxAsyncOpsPerformed = 100,
                PresentationContexts =
                {
                    { DicomUID.CTImageStorage, DicomTransferSyntax.ImplicitVRLittleEndian }
                }
            };
        }

        [GlobalCleanup]
        public void Teardown()
        {
            foreach (var server in _cEchoServers.Concat(_cStoreServers))
            {
                server.Stop();
                server.Dispose();
            }
        }

        [Benchmark(Description = "C-ECHO")]
        public async Task SendEchoToServer()
        {
            // Round-robin select a server to avoid socket exhaustion
            _currentServer = (_currentServer + 1) % NumberOfServers;
            var requests = new List<DicomCEchoRequest>(NumberOfRequests);
            for (int i = 0; i < NumberOfRequests; i++)
            {
                requests.Add(new DicomCEchoRequest());
            }
            switch (Client)
            {
                case "DicomClient":
                {
                    var client = _cEchoClients[_currentServer];
                    await client.AddRequestsAsync(requests);
                    await client.SendAsync(CancellationToken.None);
                    break;
                }
                case "AdvancedDicomClient":
                {
                    var server = _cEchoServers[_currentServer];
                    _cEchoAdvancedConnectionRequest.NetworkStreamCreationOptions.Port = server.Port;
                    using var connection = await _advancedDicomClient.OpenConnectionAsync(_cEchoAdvancedConnectionRequest, CancellationToken.None);
                    using var association = await _advancedDicomClient.OpenAssociationAsync(connection, _cStoreAdvancedAssociationRequest, CancellationToken.None);
                    var tasks = new Task[NumberOfRequests];
                    for (int i = 0; i < NumberOfRequests; i++)
                    {
                        tasks[i] = association.SendCEchoRequestAsync(requests[i], CancellationToken.None);
                    }
                    await Task.WhenAll(tasks);
                    await association.ReleaseAsync(CancellationToken.None);
                    break;
                }
            }
        }

        [Benchmark(Description = "C-STORE")]
        public async Task SendStoreToServer()
        {
            // Round-robin select a server to avoid socket exhaustion
            _currentServer = (_currentServer + 1) % NumberOfServers;
            var requests = new List<DicomCStoreRequest>(NumberOfRequests);
            for (int i = 0; i < NumberOfRequests; i++)
            {
                requests.Add(new DicomCStoreRequest(_sampleFile));
            }
            switch (Client)
            {
                case "DicomClient":
                {
                    var client = _cStoreClients[_currentServer];
                    await client.AddRequestsAsync(requests);
                    await client.SendAsync(CancellationToken.None);
                    break;
                }
                case "AdvancedDicomClient":
                {
                    var server = _cStoreServers[_currentServer];
                    _cStoreAdvancedConnectionRequest.NetworkStreamCreationOptions.Port = server.Port;
                    using var connection = await _advancedDicomClient.OpenConnectionAsync(_cStoreAdvancedConnectionRequest, CancellationToken.None);
                    using var association = await _advancedDicomClient.OpenAssociationAsync(connection, _cStoreAdvancedAssociationRequest, CancellationToken.None);
                    var tasks = new Task[NumberOfRequests];
                    for (int i = 0; i < NumberOfRequests; i++)
                    {
                        tasks[i] = association.SendCStoreRequestAsync(requests[i], CancellationToken.None);
                    }
                    await Task.WhenAll(tasks);
                    await association.ReleaseAsync(CancellationToken.None);
                    break;
                }
            }
        }
    }
}
