// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection("Network"), Trait("Category", "Network"), TestCaseOrderer("FellowOakDicom.Tests.Helpers.PriorityOrderer", "fo-dicom.Tests")]
    public class DicomServerTest
    {
        private readonly XUnitDicomLogger _logger;

        public DicomServerTest(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper).IncludeTimestamps().IncludeThreadId();
        }

        #region Unit Tests

        [Fact]
        public void Constructor_EstablishTwoWithSamePort_ShouldYieldAccessibleException()
        {
            var port = Ports.GetNext();

            var server1 = Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
            while (!server1.IsListening)
            {
                Thread.Sleep(10);
            }

            var exception = Record.Exception(() => Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")));
            Assert.IsType<DicomNetworkException>(exception);

            Assert.True(server1.IsListening);
            Assert.Null(server1.Exception);
        }

        [Fact]
        public void Stop_IsListening_TrueUntilStopRequested()
        {
            var port = Ports.GetNext();

            var server = Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
            while (!server.IsListening)
            {
                Thread.Sleep(10);
            }

            for (var i = 0; i < 10; ++i)
            {
                Thread.Sleep(500);
                Assert.True(server.IsListening);
            }

            server.Stop();
            Thread.Sleep(500);

            Assert.False(server.IsListening);
        }

        [Fact]
        public void Create_GetInstanceSamePort_ReturnsInstance()
        {
            var port = Ports.GetNext();

            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var server = Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port)?.DicomServer;
                Assert.Equal(port, server.Port);
            }
        }

        [Fact]
        public void Create_GetInstanceSamePortAfterDisposal_ReturnsNull()
        {
            var port = Ports.GetNext();

            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"))) { }

            var server = Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port)?.DicomServer;
            Assert.Null(server);
        }

        [Fact]
        public void Create_TwiceOnSamePortWithDisposalInBetween_DoesNotThrow()
        {
            var port = Ports.GetNext();

            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
            }

            var e = Record.Exception(
                () =>
                    {
                        using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
                        {
                            Assert.NotNull(Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port)?.DicomServer);
                        }
                    });
            Assert.Null(e);
        }

        [Fact]
        public void Create_GetInstanceDifferentPort_ReturnsNull()
        {
            var port = Ports.GetNext();

            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var server = Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(Ports.GetNext());
                Assert.Null(server);
            }
        }

        [Fact]
        public void Create_MultipleInstancesSamePort_Throws()
        {
            var port = Ports.GetNext();

            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var e = Record.Exception(() => Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")));
                Assert.IsType<DicomNetworkException>(e);
            }
        }

        [Fact]
        public void Create_MultipleInstancesDifferentPorts_AllRegistered()
        {
            var ports = new int[20].Select(i => Ports.GetNext()).ToArray();

            foreach (var port in ports)
            {
                var server = Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
                while (!server.IsListening) { Thread.Sleep(10); }
            }

            foreach (var port in ports)
            {
                Assert.Equal(port, Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port)?.DicomServer.Port);
            }

            foreach (var port in ports)
            {
                Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port)?.DicomServer.Dispose();
            }
        }

        [Fact]
        public void IsListening_DicomServerRunningOnPort_ReturnsTrue()
        {
            var port = Ports.GetNext();

            using (var server = Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                while (!server.IsListening) { Thread.Sleep(10); }
                Assert.True(Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port).DicomServer.IsListening);
            }
        }

        [Fact]
        public void IsListening_DicomServerStoppedOnPort_ReturnsFalse()
        {
            var port = Ports.GetNext();

            using (var server = Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                server.Stop();
                Thread.Sleep(500);

                var dicomServer = Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port)?.DicomServer;
                Assert.NotNull(dicomServer);
                Assert.False(dicomServer.IsListening);
            }
        }

        [Fact]
        public void IsListening_DicomServerNotInitializedOnPort_ReturnsFalse()
        {
            var port = Ports.GetNext();

            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                Assert.False(Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(Ports.GetNext())?.DicomServer?.IsListening ?? false);
            }
        }

        [Fact]
        public async Task Send_KnownSOPClass_SendSucceeds()
        {
            var port = Ports.GetNext();
            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<SimpleCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(request);

                await client.SendAsync();

                Assert.Equal(DicomStatus.Success, status);
            }
        }

        [Fact, TestPriority(1)]
        public async Task Send_PrivateNotRegisteredSOPClass_SendFails()
        {
            var uid = new DicomUID("1.1.1.1", "Private Fo-Dicom Storage", DicomUidType.SOPClass);
            var ds = new DicomDataset(
               new DicomUniqueIdentifier(DicomTag.SOPClassUID, uid),
               new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3.4.5"));
            var port = Ports.GetNext();
            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<SimpleCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(new DicomFile(ds))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(request).ConfigureAwait(false);

                await client.SendAsync().ConfigureAwait(false);

                Assert.Equal(DicomStatus.SOPClassNotSupported, status);
            }
        }

        [Fact, TestPriority(2)]
        public async Task Send_PrivateRegisteredSOPClass_SendSucceeds()
        {
            var uid = new DicomUID("1.1.1.1", "Private Fo-Dicom Storage", DicomUidType.SOPClass);
            DicomUID.Register(uid);
            var ds = new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, uid),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3.4.5"));

            var port = Ports.GetNext();
            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<SimpleCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(new DicomFile(ds))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(request);

                await client.SendAsync();

                Assert.Equal(DicomStatus.Success, status);
            }
        }

        [Fact]
        public async Task Stop_DisconnectedClientsCount_ShouldBeZeroAfterShortDelay()
        {
            var port = Ports.GetNext();

            using (var server = Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                while (!server.IsListening) { Thread.Sleep(10); }

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(new DicomCEchoRequest());
                await client.SendAsync();
                Thread.Sleep(100);

                server.Stop();
                Thread.Sleep(100);

                var actual = ((DicomServer<DicomCEchoProvider>) server).CompletedServicesCount;
                Assert.Equal(0, actual);
            }
        }

        [Fact]
        public async Task Send_LoopbackListenerKnownSOPClass_SendSucceeds()
        {
            var port = Ports.GetNext();
            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<SimpleCStoreProvider>(NetworkManager.IPv4Loopback, port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create(NetworkManager.IPv4Loopback, port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(request);

                await client.SendAsync();

                Assert.Equal(DicomStatus.Success, status);
            }
        }

        [Fact]
        public async Task Send_FromIpv4ToIpv6AnyListenerKnownSOPClass_SendFails()
        {
            var port = Ports.GetNext();
            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<SimpleCStoreProvider>(NetworkManager.IPv6Any, port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"));

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create(NetworkManager.IPv4Loopback, port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(request);

                var exception = await Record.ExceptionAsync(async () => await client.SendAsync());

                Assert.NotNull(exception);
                Assert.Contains("Socket", (exception.InnerException ?? exception).GetType().Name);
            }
        }

        [Fact]
        public async Task Send_FromIpv6ToIpv4AnyListenerKnownSOPClass_SendFails()
        {
            var port = Ports.GetNext();
            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<SimpleCStoreProvider>(NetworkManager.IPv4Any, port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"));

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create(NetworkManager.IPv6Loopback, port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(request);

                var exception = await Record.ExceptionAsync(async () => await client.SendAsync());

                Assert.NotNull(exception);
            }
        }

        [Fact]
        public void CanCreateIpv4AndIpv6()
        {
            var port = Ports.GetNext();
            using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var e = Record.Exception(
                    () =>
                    {
                        using (Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider>(NetworkManager.IPv6Any, port, logger: _logger.IncludePrefix("DicomServer")))
                        {
                            // do nothing here
                        }
                    });
                Assert.Null(e);
            }
        }

        [Fact]
        public async Task Create_SubclassedServer_SufficientlyCreated()
        {
            var port = Ports.GetNext();

            using (var server = Setup.ServiceProvider.GetRequiredService<IDicomServerFactory>().Create<DicomCEchoProvider, DicomCEchoProviderServer>(null, port, logger: _logger.IncludePrefix("DicomServer")))
            {
                Assert.IsType<DicomCEchoProviderServer>(server);
                Assert.Equal(Setup.ServiceProvider.GetRequiredService<IDicomServerRegistry>().Get(port)?.DicomServer, server);

                var status = DicomStatus.UnrecognizedOperation;
                var handle = new ManualResetEventSlim();

                var client = Setup.ServiceProvider.GetRequiredService<IDicomClientFactory>().Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(new DicomCEchoRequest
                {
                    OnResponseReceived = (req, rsp) =>
                    {
                        status = rsp.Status;
                        handle.Set();
                    }
                });
                await client.SendAsync();

                handle.Wait(1000);
                Assert.Equal(DicomStatus.Success, status);
            }
        }

        #endregion

        #region Support Types

        public class DicomCEchoProviderServer : DicomServer<DicomCEchoProvider>
        {
            private readonly ILogManager _logManager;
            private readonly INetworkManager _networkManager;
            private readonly ITranscoderManager _transcoderManager;

            public DicomCEchoProviderServer(ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager) :
                base(networkManager, logManager)
            {
                _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
                _networkManager = networkManager;
                _transcoderManager = transcoderManager;
            }

            protected override DicomCEchoProvider CreateScp(INetworkStream stream)
                => new DicomCEchoProvider(stream, null, _logManager.GetLogger("DicomEchoProvider"), _logManager, _networkManager, _transcoderManager);
        }

        #endregion
    }
}

