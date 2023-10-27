// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Network)]
    public sealed class GH1669
    {
        private readonly XUnitDicomLogger _logger;

        public GH1669(ITestOutputHelper output) 
        {
            _logger = new XUnitDicomLogger(output)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }
        
        [Fact]
        public async Task ShouldAcceptMaxClientsAllowedConnectionsAtAllTimes()
        {
            // Arrange
            const int numberOfClients = 6;
            const int maxClientsAllowed = 3;
            var delayPerClient = TimeSpan.FromSeconds(5);
            var serverDelay = TimeSpan.FromSeconds(1);
            
            var port = Ports.GetNext();
            using var server = (ConfigurableDicomCEchoProviderServer)
                DicomServerFactory.Create<
                    ConfigurableDicomCEchoProvider,
                    ConfigurableDicomCEchoProviderServer>(
                    NetworkManager.IPv4Any,
                    port,
                    configure: o => o.MaxClientsAllowed = maxClientsAllowed
                );
            server.Logger = _logger.IncludePrefix("Server").WithMinimumLevel(LogLevel.Debug);
            server.OnRequest(async dicomCEchoRequest =>
            {
                // Simulate a delay upon every C-ECHO request to keep the connection open
                await Task.Delay(delayPerClient);
            });
            server.MaxClientsAllowedWaitInterval = serverDelay;

            var clients = new List<IDicomClient>(numberOfClients);
            for (var i = 1; i <= numberOfClients; i++)
            {
                var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                client.ClientOptions.AssociationRequestTimeoutInMs = (int) TimeSpan.FromMinutes(1).TotalMilliseconds;
                client.ServiceOptions.RequestTimeout = TimeSpan.FromMinutes(1);
                client.Logger = _logger.IncludePrefix($"Client{i}").WithMinimumLevel(LogLevel.Debug);
                clients.Add(client);
            }

            // Act
            var responses = new ConcurrentQueue<DicomCEchoResponse>();
            var sendTasks = new List<Task>();
            foreach (var client in clients)
            {
                var request = new DicomCEchoRequest
                {
                    OnResponseReceived = (request, response) => responses.Enqueue(response),
                };
                await client.AddRequestAsync(request);
            }
            foreach (var client in clients)
            {
                sendTasks.Add(client.SendAsync());
            }
            await Task.WhenAll(sendTasks);

            // Assert
            Assert.Equal(responses.Count, clients.Count);
            foreach (var response in responses)
            {
                Assert.Equal(DicomState.Success, response.Status.State);
            }
        }
    }
}
