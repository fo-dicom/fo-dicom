// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Association;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Network
{
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network), TestCaseOrderer("FellowOakDicom.Tests.Helpers.PriorityOrderer", "fo-dicom.Tests")]
    public class DicomServerTest
    {
        private readonly XUnitDicomLogger _logger;

        public DicomServerTest(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper).IncludeTimestamps().IncludeThreadId();
        }

        #region Unit Tests

        [Fact]
        public async Task Constructor_EstablishTwoWithSamePort_ShouldYieldAccessibleException()
        {
            var port = Ports.GetNext();

            var server1 = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
            while (!server1.IsListening)
            {
                await Task.Delay(10);
            }

            var exception = Record.Exception(() => DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")));
            Assert.IsType<DicomNetworkException>(exception);

            Assert.True(server1.IsListening);
            Assert.Null(server1.Exception);
        }

        [Fact(Skip = "Flaky test. The DICOM Server is not always immediately stopped. We should implement proper cancellation support all the way through DicomService")]
        public async Task Stop_IsListening_TrueUntilStopRequested()
        {
            var port = Ports.GetNext();

            var server = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
            while (!server.IsListening)
            {
                await Task.Delay(10);
            }

            for (var i = 0; i < 10; ++i)
            {
                await Task.Delay(500);
                Assert.True(server.IsListening);
            }

            server.Stop();
            await Task.Delay(1000);

            Assert.False(server.IsListening);
        }

        [Fact]
        public void Create_GetInstanceSamePort_ReturnsInstance()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var server = DicomServerRegistry.Get(port)?.DicomServer;
                Assert.Equal(port, server.Port);
            }
        }

        [Fact]
        public void Create_GetInstanceSamePortAfterDisposal_ReturnsNull()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"))) { /* do nothing here */ }

            var server = DicomServerRegistry.Get(port)?.DicomServer;
            Assert.Null(server);
        }

        [Fact]
        public async Task Create_TwiceOnSamePortWithDisposalInBetween_DoesNotThrow()
        {
            var port = Ports.GetNext();

            Task dicomServerTask;
            using (var dicomServer = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer1")))
            {
                dicomServerTask = dicomServer.Registration.Task;
            }

            // Wait for full shutdown with 1 minute timeout
            var oneMinuteTimeout = Task.Delay(TimeSpan.FromMinutes(1));
            if (await Task.WhenAny(dicomServerTask, oneMinuteTimeout) == oneMinuteTimeout)
            {
                throw new InvalidOperationException("DICOM server still hasn't shut down after one minute");
            }

            var e = Record.Exception(
                () =>
                    {
                        using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer2")))
                        {
                            Assert.NotNull(DicomServerRegistry.Get(port)?.DicomServer);
                        }
                    });
            Assert.Null(e);
        }

        [Fact]
        public void Create_TwiceOnSamePortWithoutDisposalInBetween_Throws()
        {
            var port = Ports.GetNext();

            Exception e;
            using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                e = Record.Exception(
                    () =>
                    {
                        using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
                        {
                            Assert.NotNull(DicomServerRegistry.Get(port)?.DicomServer);
                        }
                    });
            }

            Assert.NotNull(e);
            Assert.IsType<DicomNetworkException>(e);
        }

        [Fact]
        public void Create_GetInstanceDifferentPort_ReturnsNull()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var server = DicomServerRegistry.Get(Ports.GetNext());
                Assert.Null(server);
            }
        }

        [Fact]
        public void Create_MultipleInstancesSamePort_Throws()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var e = Record.Exception(() => DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")));
                Assert.IsType<DicomNetworkException>(e);
            }
        }

        [Fact]
        public async Task Create_MultipleInstancesDifferentPorts_AllRegistered()
        {
            var ports = new int[20].Select(i => Ports.GetNext()).ToArray();

            foreach (var port in ports)
            {
                var server = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
                while (!server.IsListening) { await Task.Delay(10); }
            }

            foreach (var port in ports)
            {
                Assert.Equal(port, DicomServerRegistry.Get(port)?.DicomServer.Port);
            }

            foreach (var port in ports)
            {
                DicomServerRegistry.Get(port)?.DicomServer.Dispose();
            }
        }

        [Fact]
        public async Task IsListening_DicomServerRunningOnPort_ReturnsTrue()
        {
            var port = Ports.GetNext();

            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
            while (!server.IsListening) { await Task.Delay(10); }
            Assert.True(DicomServerRegistry.Get(port).DicomServer.IsListening);
        }

        [Fact]
        public async Task IsListening_DicomServerStoppedOnPort_ReturnsFalse()
        {
            var port = Ports.GetNext();

            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
            while (!server.IsListening) { await Task.Delay(10); }
            server.Stop();
            while (server.IsListening) { await Task.Delay(10); }

            var dicomServer = DicomServerRegistry.Get(port)?.DicomServer;
            Assert.NotNull(dicomServer);
            Assert.False(dicomServer.IsListening);
        }

        [Fact]
        public void IsListening_DicomServerNotInitializedOnPort_ReturnsFalse()
        {
            var port = Ports.GetNext();

            using (DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                Assert.False(DicomServerRegistry.Get(Ports.GetNext())?.DicomServer?.IsListening ?? false);
            }
        }


        [Fact]
        public async Task SendMaxPDU()
        {
            var port = Ports.GetNext();
            uint serverPduLength = 400000;
            uint clientPduLength = serverPduLength / 2;

            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port);
            server.Options.MaxPDULength = serverPduLength;

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.ServiceOptions.MaxPDULength = clientPduLength; // explicitly choose a different value
            await client.AddRequestAsync(new DicomCEchoRequest());

            uint serverPduInAssociationAccepted = 0;
            client.AssociationAccepted += (sender, e) => serverPduInAssociationAccepted = e.Association.MaximumPDULength;

            await client.SendAsync();

            Assert.Equal(serverPduLength, serverPduInAssociationAccepted);
        }


        [Fact]
        public async Task Send_KnownSOPClass_SendSucceeds()
        {
            var port = Ports.GetNext();
            using (DicomServerFactory.Create<SimpleCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
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
            using (DicomServerFactory.Create<SimpleCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(new DicomFile(ds))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.Logger = _logger.IncludePrefix("DicomClient");
                await client.AddRequestAsync(request);

                await client.SendAsync();

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
            using (DicomServerFactory.Create<SimpleCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(new DicomFile(ds))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
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

            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));
            while (!server.IsListening) { await Task.Delay(10); }

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.Logger = _logger.IncludePrefix("DicomClient");
            await client.AddRequestAsync(new DicomCEchoRequest());
            await client.SendAsync();
            await Task.Delay(100);

            server.Stop();
            await Task.Delay(100);

            var actual = ((DicomServer<DicomCEchoProvider>)server).CompletedServicesCount;
            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task Send_LoopbackListenerKnownSOPClass_SendSucceeds()
        {
            var port = Ports.GetNext();
            using (DicomServerFactory.Create<SimpleCStoreProvider>(NetworkManager.IPv4Loopback, port, logger: _logger.IncludePrefix("DicomServer")))
            {
                DicomStatus status = null;
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"))
                {
                    OnResponseReceived = (req, res) => status = res.Status
                };

                var client = DicomClientFactory.Create(NetworkManager.IPv4Loopback, port, false, "SCU", "ANY-SCP");
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
            using (DicomServerFactory.Create<SimpleCStoreProvider>(NetworkManager.IPv6Any, port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"));

                var client = DicomClientFactory.Create(NetworkManager.IPv4Loopback, port, false, "SCU", "ANY-SCP");
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
            using (DicomServerFactory.Create<SimpleCStoreProvider>(NetworkManager.IPv4Any, port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var request = new DicomCStoreRequest(TestData.Resolve("CT-MONO2-16-ankle"));

                var client = DicomClientFactory.Create(NetworkManager.IPv6Loopback, port, false, "SCU", "ANY-SCP");
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
            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port, logger: _logger.IncludePrefix("DicomServer"));

            var e = Record.Exception(
                () =>
                {
                    using (DicomServerFactory.Create<DicomCEchoProvider>(NetworkManager.IPv6Any, port, logger: _logger.IncludePrefix("DicomServer")))
                    {
                            // do nothing here
                    }
                });
            Assert.Null(e);
        }

        [Fact]
        public async Task Create_SubclassedServer_SufficientlyCreated()
        {
            var port = Ports.GetNext();

            using var server = DicomServerFactory.Create<DicomCEchoProvider, DicomCEchoProviderServer>(null, port, logger: _logger.IncludePrefix("DicomServer"));

            Assert.IsType<DicomCEchoProviderServer>(server);
            Assert.Equal(DicomServerRegistry.Get(port)?.DicomServer, server);

            var status = DicomStatus.UnrecognizedOperation;
            var handle = new ManualResetEventSlim();

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
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

        [Fact]
        public async Task MaxClientsAllowed_ShouldRejectFurtherConnectionsWhenLimitIsReached()
        {
            var port = Ports.GetNext();
            var serverLogger = _logger.IncludePrefix("Server");
            var clientLogger = _logger.IncludePrefix("Client");
            using var server = DicomServerFactory.Create<AsyncDicomCEchoProvider>(port, logger: serverLogger, configure: o => o.MaxClientsAllowed = 1);
            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = clientLogger,
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };
            var associationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "AnySCU",
                CalledAE = "AnySCP",
            };
            associationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            IAdvancedDicomClientConnection connection1 = null, connection2 = null;
            IAdvancedDicomClientAssociation association1 = null, association2 = null;
            Exception exception1 = null, exception2 = null;
            try
            {
                connection1 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
                association1 = await connection1.OpenAssociationAsync(associationRequest, CancellationToken.None);
                await association1.ReleaseAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                exception1 = e;
            }

            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                connection2 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cts.Token);
                association2 = await connection2.OpenAssociationAsync(associationRequest, cts.Token);
                await association2.ReleaseAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                exception2 = e;
            }

            association1?.Dispose();
            association2?.Dispose();
            connection1?.Dispose();
            connection2?.Dispose();

            // Nothing should have gone wrong with connection 1
            Assert.NotNull(association1);
            Assert.Null(exception1);

            // Connection 2 should have timed out because of the cancellation token
            Assert.Null(association2);
            Assert.NotNull(exception2);
            Assert.IsType<OperationCanceledException>(exception2);
        }

        [Fact]
        public async Task MaxClientsAllowed_ShouldAllowFurtherConnectionsWhenPreviousConnectionsAreDropped()
        {
            var port = Ports.GetNext();
            var serverLogger = _logger.IncludePrefix("Server");
            var clientLogger = _logger.IncludePrefix("Client");
            using var server = DicomServerFactory.Create<AsyncDicomCEchoProvider>(port, logger: serverLogger, configure: o => o.MaxClientsAllowed = 1);
            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = server.Port,
                },
                Logger = clientLogger,
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };
            var associationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "AnySCU",
                CalledAE = "AnySCP",
            };
            associationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            IAdvancedDicomClientConnection connection1 = null, connection2 = null;
            IAdvancedDicomClientAssociation association1 = null, association2 = null;
            Exception exception1 = null, exception2 = null;
            try
            {
                connection1 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, CancellationToken.None);
                association1 = await connection1.OpenAssociationAsync(associationRequest, CancellationToken.None);
                await association1.ReleaseAsync(CancellationToken.None);
                association1.Dispose();
                connection1.Dispose();
            }
            catch (Exception e)
            {
                exception1 = e;
            }

            // Allow 5 seconds for the server to clean up its running DICOM services
            await Task.Delay(5000);

            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                connection2 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cts.Token);
                association2 = await connection2.OpenAssociationAsync(associationRequest, cts.Token);
                await association2.ReleaseAsync(CancellationToken.None);
                association2.Dispose();
                connection2.Dispose();
            }
            catch (Exception e)
            {
                exception2 = e;
            }

            association1?.Dispose();
            association2?.Dispose();
            connection1?.Dispose();
            connection2?.Dispose();

            // Nothing should have gone wrong with connection 1
            Assert.NotNull(association1);
            Assert.Null(exception1);

            // Nothing should have gone wrong with connection 2
            Assert.NotNull(association2);
            Assert.Null(exception2);
        }

        [Fact]
        public async Task RemoveUnusedServicesAsync_ShouldDisposeFinishedDicomServices()
        {
            var port = Ports.GetNext();
            var serverLogger = _logger.IncludePrefix("Server");
            var clientLogger = _logger.IncludePrefix("Client");
            var disposedDicomServices = new ConcurrentStack<DicomService>();
            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = port,
                },
                Logger = clientLogger,
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };
            var associationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "AnySCU",
                CalledAE = "AnySCP",
            };
            associationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());

            using (var server = (DisposableDicomCEchoProviderServer)DicomServerFactory
                       .Create<DisposableDicomCEchoProvider, DisposableDicomCEchoProviderServer>(
                           "127.0.0.1", port, logger: serverLogger))
            {
                server.OnDispose = service => disposedDicomServices.Push(service);

                // Open and close connection
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                using var connection =
                    await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cts.Token);
                using var association = await connection.OpenAssociationAsync(associationRequest, cts.Token);
                var response2 =
                    await association.SendCEchoRequestAsync(new DicomCEchoRequest(), CancellationToken.None);
                await association.ReleaseAsync(CancellationToken.None);
                Assert.Equal(DicomState.Success, response2.Status.State);

                server.Stop();

                // Wait for the server to shut down gracefully
                await server.Registration.Task;
            }

            var uniqueDisposedServices = new HashSet<DicomService>(disposedDicomServices);
            Assert.Single(uniqueDisposedServices);
        }

        [Fact]
        public async Task RemoveUnusedServicesAsync_ShouldDisposeFinishedDicomServicesEvenIfInitialConnectionIsNeverClosed()
        {
            var port = Ports.GetNext();
            var serverLogger = _logger.IncludePrefix("Server");
            var clientLogger = _logger.IncludePrefix("Client");
            var disposedDicomServices = new ConcurrentStack<DicomService>();
            var connectionRequest = new AdvancedDicomClientConnectionRequest
            {
                NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                {
                    Host = "127.0.0.1",
                    Port = port,
                },
                Logger = clientLogger,
                FallbackEncoding = DicomEncoding.Default,
                DicomServiceOptions = new DicomServiceOptions()
            };
            var associationRequest = new AdvancedDicomClientAssociationRequest
            {
                CallingAE = "AnySCU",
                CalledAE = "AnySCP",
            };
            associationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());
            int numberOfDisposedDicomServices;
            using(var server = (DisposableDicomCEchoProviderServer) DicomServerFactory.Create<DisposableDicomCEchoProvider, DisposableDicomCEchoProviderServer>(
                      "127.0.0.1", port, logger: serverLogger))
            {
                server.OnDispose = service => disposedDicomServices.Push(service);

                // Open connection 1. This connection will stay open for the duration of the test
                using var connection1 = await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest,
                        CancellationToken.None);
                using var association1 =
                    await connection1.OpenAssociationAsync(associationRequest, CancellationToken.None);
                var response1 =
                    await association1.SendCEchoRequestAsync(new DicomCEchoRequest(), CancellationToken.None);
                Assert.Equal(DicomState.Success, response1.Status.State);

                // Give RemoveUnusedServicesAsync the chance to detect a DicomService is present, and let it begin waiting for the connection to close
                await Task.Delay(1000);

                // Open and close connection 2
                {
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                    using var connection2 =
                        await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cts.Token);
                    using var association2 = await connection2.OpenAssociationAsync(associationRequest, cts.Token);
                    var response2 =
                        await association2.SendCEchoRequestAsync(new DicomCEchoRequest(), CancellationToken.None);
                    await association2.ReleaseAsync(CancellationToken.None);
                    Assert.Equal(DicomState.Success, response2.Status.State);
                }

                // Open and close connection 3
                {
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                    using var connection3 =
                        await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest, cts.Token);
                    using var association3 = await connection3.OpenAssociationAsync(associationRequest, cts.Token);
                    var response3 =
                        await association3.SendCEchoRequestAsync(new DicomCEchoRequest(), CancellationToken.None);
                    await association3.ReleaseAsync(CancellationToken.None);
                    Assert.Equal(DicomState.Success, response3.Status.State);
                }

                // Give RemoveUnusedServicesAsync the chance to cleanup disconnected services
                await Task.Delay(1000);

                // Verify that the 2 services were that already disconnected are disposed
                numberOfDisposedDicomServices = disposedDicomServices.Distinct().Count();
                Assert.True(numberOfDisposedDicomServices >= 2);

                // Stop the server
                server.Stop();

                // Wait for graceful server shutdown
                await server.Registration.Task;
            }

            // Verify that, after the server is disposed, all 3 services were disposed (even the one that never dropped its connection)
            numberOfDisposedDicomServices = disposedDicomServices.Distinct().Count();
            Assert.Equal(3, numberOfDisposedDicomServices);
        }

        [Fact]
        public async Task RemoveUnusedServicesAsync_ShouldBeAbleToDisposeAHundredServices()
        {
            var port = Ports.GetNext();
            var serverLogger = _logger.IncludePrefix("Server").WithMinimumLevel(LogLevel.Information);
            var clientLogger = _logger.IncludePrefix("Client").WithMinimumLevel(LogLevel.Information);
            var disposedDicomServices = new ConcurrentStack<DicomService>();

            using (var server = (DisposableDicomCEchoProviderServer)DicomServerFactory
                       .Create<DisposableDicomCEchoProvider, DisposableDicomCEchoProviderServer>(
                           "127.0.0.1", port, logger: serverLogger))
            {
                server.OnDispose = service => disposedDicomServices.Push(service);

                var services = Enumerable.Range(0, 100)
                    .AsParallel()
                    .Select(async _ =>
                    {
                        // Open and close connection
                        var connectionRequest = new AdvancedDicomClientConnectionRequest
                        {
                            NetworkStreamCreationOptions = new NetworkStreamCreationOptions
                            {
                                Host = "127.0.0.1",
                                Port = port,
                            },
                            Logger = clientLogger,
                            FallbackEncoding = DicomEncoding.Default,
                            DicomServiceOptions = new DicomServiceOptions()
                        };
                        var associationRequest = new AdvancedDicomClientAssociationRequest
                        {
                            CallingAE = "AnySCU",
                            CalledAE = "AnySCP",
                        };
                        associationRequest.PresentationContexts.AddFromRequest(new DicomCEchoRequest());
                        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
                        using var connection =
                            await AdvancedDicomClientConnectionFactory.OpenConnectionAsync(connectionRequest,
                                cts.Token);
                        using var association = await connection.OpenAssociationAsync(associationRequest, cts.Token);

                        // Keep association for 50ms
                        await Task.Delay(50, cts.Token);

                        var response2 =
                            await association.SendCEchoRequestAsync(new DicomCEchoRequest(), CancellationToken.None);
                        await association.ReleaseAsync(CancellationToken.None);
                        Assert.Equal(DicomState.Success, response2.Status.State);
                    });

                await Task.WhenAll(services);

                server.Stop();

                // Wait for the server to shut down gracefully
                await server.Registration.Task;
            }

            var uniqueDisposedServices = new HashSet<DicomService>(disposedDicomServices);
            Assert.Equal(100, uniqueDisposedServices.Count);
        }

        private void TestFoDicomUnhandledException(int port)
        {
            var server = DicomServerFactory.Create<DicomCEchoProvider>(port);
            Thread.Sleep(500);
            server.Stop();
        }

        [Fact(Skip = "This test is flaky because it crashes whenever a parallel test happens to have an unobserved exception")]
        public async Task StopServerWithoutException()
        {
            object ue = null;
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => ue = args.ExceptionObject;
            TaskScheduler.UnobservedTaskException += (sender, args) => ue = args.Exception;

            var port = Ports.GetNext();

            await Task.Factory.StartNew(() => TestFoDicomUnhandledException(port));

            await Task.Delay(2000);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Assert.Null(ue);
        }

        #endregion

        #region Support Types

        public class DicomCEchoProviderServer : DicomServer<DicomCEchoProvider>
        {
            private readonly DicomServiceDependencies _dicomServiceDependencies;

            public DicomCEchoProviderServer(DicomServerDependencies dicomServerDependencies,
                DicomServiceDependencies dicomServiceDependencies) :
                base(dicomServerDependencies)
            {
                _dicomServiceDependencies = dicomServiceDependencies;
            }

            protected override DicomCEchoProvider CreateScp(INetworkStream stream)
                => new DicomCEchoProvider(stream, null, _dicomServiceDependencies.LoggerFactory.CreateLogger("DicomEchoProvider"), _dicomServiceDependencies);
        }

        public class DisposableDicomCEchoProviderServer : DicomServer<DisposableDicomCEchoProvider>
        {
            private readonly DicomServiceDependencies _dicomServiceDependencies;

            public DisposableDicomCEchoProviderServer(DicomServerDependencies dicomServerDependencies,
                DicomServiceDependencies dicomServiceDependencies) :
                base(dicomServerDependencies)
            {
                _dicomServiceDependencies = dicomServiceDependencies ?? throw new ArgumentNullException(nameof(dicomServiceDependencies));
            }

            public Action<DicomService> OnDispose { get; set; } = _ => { };

            protected override DisposableDicomCEchoProvider CreateScp(INetworkStream stream)
            {
                var logger = _dicomServiceDependencies.LoggerFactory.CreateLogger("DisposableDicomCEchoProviderServer");
                return new DisposableDicomCEchoProvider(stream, null, logger, _dicomServiceDependencies, OnDispose);
            }
        }

        public class DisposableDicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
        {
            private readonly Action<DicomService> _onDispose;

            public DisposableDicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Microsoft.Extensions.Logging.ILogger log,
                DicomServiceDependencies dicomServiceDependencies, Action<DicomService> onDispose)
                : base(stream, fallbackEncoding, log, dicomServiceDependencies)
            {
                _onDispose = onDispose ?? throw new ArgumentNullException(nameof(onDispose));
            }

            /// <inheritdoc />
            public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.SetResult(DicomPresentationContextResult.Accept);
                }

                await SendAssociationAcceptAsync(association).ConfigureAwait(false);
            }

            /// <inheritdoc />
            public async Task OnReceiveAssociationReleaseRequestAsync()
            {
                await SendAssociationReleaseResponseAsync().ConfigureAwait(false);
            }

            /// <inheritdoc />
            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            /// <inheritdoc />
            public void OnConnectionClosed(Exception exception)
            {
            }

            public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
            {
                return Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
            }

            protected override void Dispose(bool disposing)
            {
                _onDispose(this);

                base.Dispose(disposing);
            }
        }


        #endregion
    }
}

