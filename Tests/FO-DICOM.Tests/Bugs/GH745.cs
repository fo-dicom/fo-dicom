// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Linq;
using System.Threading;
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
    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class GH745
    {
        private readonly XUnitDicomLogger _logger;

        public GH745(ITestOutputHelper output)
        {
            _logger = new XUnitDicomLogger(output)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Warning);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task DicomClientShallNotCloseConnectionTooEarly_CEchoSerialAsync(int expected)
        {
            var port = Ports.GetNext();
            var testLogger = _logger.IncludePrefix("GH745");
            var clientLogger = _logger.IncludePrefix(nameof(DicomClient));
            var serverLogger = _logger.IncludePrefix(nameof(DicomCEchoProvider));

            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port);
            server.Logger = serverLogger;
            while (!server.IsListening) { await Task.Delay(50); }

            var actual = 0;

            var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
            client.Logger = clientLogger;
            client.ClientOptions.AssociationRequestTimeoutInMs = 600 * 1000;
            client.ClientOptions.AssociationLingerTimeoutInMs = 1; // No need to linger, we only send one request at a time

            for (var i = 0; i < expected; i++)
            {
                await client.AddRequestAsync(
                    new DicomCEchoRequest
                    {
                        OnResponseReceived = (req, res) =>
                        {
                            testLogger.LogInformation("Response #{0} / expected #{1}", actual, req.UserState);
                            Interlocked.Increment(ref actual);
                            testLogger.LogInformation("         #{0} / expected #{1}", actual - 1, req.UserState);
                        },
                        UserState = i
                    }
                );
                testLogger.LogInformation("Sending #{0}", i);
                await client.SendAsync();
                testLogger.LogInformation("Sent (or timed out) #{0}", i);
            }

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(75)]
        [InlineData(200)]
        [InlineData(1)]
        public async Task DicomClientShallNotCloseConnectionTooEarly_CEchoParallelAsync(int expected)
        {
            int port = Ports.GetNext();

            var testLogger = _logger.IncludePrefix("GH745");
            var clientLogger = _logger.IncludePrefix(nameof(DicomClient));
            var serverLogger = _logger.IncludePrefix(nameof(DicomCEchoProvider));

            using var server = DicomServerFactory.Create<DicomCEchoProvider>(port);
            server.Logger = serverLogger;
            while (!server.IsListening) { await Task.Delay(50); }

            var actual = 0;

            var requests = Enumerable.Range(0, expected).Select(
                async requestIndex =>
                {
                    var client = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");
                    client.ClientOptions.AssociationRequestTimeoutInMs = 600 * 1000;
                    client.Logger = clientLogger;

                    await client.AddRequestAsync(
                        new DicomCEchoRequest
                        {
                            OnResponseReceived = (req, res) =>
                            {
                                testLogger.LogInformation("Response #{0}", requestIndex);
                                Interlocked.Increment(ref actual);
                            }
                        }
                    );

                    testLogger.LogInformation("Sending #{0}", requestIndex);
                    await client.SendAsync();
                    testLogger.LogInformation("Sent (or timed out) #{0}", requestIndex);
                }
            ).ToArray();

            await Task.WhenAll(requests);

            Assert.Equal(expected, actual);
        }

    }
}
