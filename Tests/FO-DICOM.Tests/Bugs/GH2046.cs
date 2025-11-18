// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection(TestCollections.Network)]
    public class GH2046
    {
        private readonly XUnitDicomLogger _logger;

        public GH2046(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper).IncludeTimestamps().IncludeThreadId();
        }

        [Fact]
        public async Task RemoveUnusedServicesAsync_ShouldCleanupAllFinishedInternalServices()
        {
            var serverLogger = _logger.IncludePrefix("Server").WithMinimumLevel(LogLevel.Information);
            var disposedDicomServices = new ConcurrentStack<DicomService>();
            var cEchoRequestCount = 0;

            using var server = (DicomServerTest.DisposableDicomCEchoProviderServer)DicomServerFactory
                       .Create<DicomServerTest.DisposableDicomCEchoProvider, DicomServerTest.DisposableDicomCEchoProviderServer>(
                           "127.0.0.1", 0, logger: serverLogger);
            server.OnDispose = service => disposedDicomServices.Push(service);

            // Verify no services disposed yet
            Assert.Equal(0, disposedDicomServices.Count);

            var numberOfClients = 50;

            //First run to warm up
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfClients; i++)
            {
                tasks.Add(Task.Run(SendCEchoRequests));
            }

            await Task.WhenAll(tasks);

            //Second run, so we're sure there are no more allocations happening
            tasks.Clear();
            for (int i = 0; i < numberOfClients; i++)
            {
                tasks.Add(Task.Run(SendCEchoRequests));
            }

            await Task.WhenAll(tasks);

            Assert.Equal(100, cEchoRequestCount); // Make sure all clients actually sent their request

            // Wait for disposal to complete with timeout
            var timeout = System.TimeSpan.FromSeconds(10);
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var previousCount = 0;
            var stableCount = 0;

            // Wait until disposal count stabilizes (no change for 200ms)
            while (stopwatch.Elapsed < timeout)
            {
                await Task.Delay(50);
                var currentCount = disposedDicomServices.Distinct().Count();

                if (currentCount == previousCount)
                {
                    stableCount += 50;
                    if (stableCount >= 200) break; // Stable for 200ms
                }
                else
                {
                    stableCount = 0;
                    previousCount = currentCount;
                }
            }

            var uniqueDisposedServices = new HashSet<DicomService>(disposedDicomServices);

            // Should have exactly 100 unique disposed services
            Assert.Equal(100, uniqueDisposedServices.Count);

            // Total count should also be 100 (no duplicates)
            Assert.Equal(100, disposedDicomServices.Count);

            server.Stop();

            // Wait for the server to shut down gracefully
            await server.Registration.Task;

            async Task SendCEchoRequests()
            {
                //Send a simple CEcho request
                var client = DicomClientFactory.Create("127.0.0.1", server.Port, false, "AnySCU", "AnySCP");
                var request = new DicomCEchoRequest
                {
                    OnResponseReceived = (echoRequest, response) =>
                    {
                        Interlocked.Increment(ref cEchoRequestCount);
                    }
                };
                await client.AddRequestAsync(request);
                await client.SendAsync();

            }
        }
    }
}
