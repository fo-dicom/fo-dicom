using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FellowOakDicom.Imaging;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Network.Client.Advanced.Connection;
using FellowOakDicom.Tests.Helpers;
using FellowOakDicom.Tests.Network;
using FellowOakDicom.Tests.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace FellowOakDicom.Tests.Bugs
{
    [Collection("Network")]
    public class GH1359
    {
        private readonly XUnitDicomLogger _logger;

        public GH1359(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper);
        }

        private IDicomClientFactory CreateClientFactory(INetworkManager networkManager)
        {
            var logManager = Setup.ServiceProvider.GetRequiredService<ILogManager>();
            var dicomServiceDependencies = Setup.ServiceProvider.GetRequiredService<DicomServiceDependencies>();
            var defaultClientOptions = Setup.ServiceProvider.GetRequiredService<IOptions<DicomClientOptions>>();
            var defaultServiceOptions = Setup.ServiceProvider.GetRequiredService<IOptions<DicomServiceOptions>>();
            var advancedDicomClientConnectionFactory = new DefaultAdvancedDicomClientConnectionFactory(networkManager, logManager, defaultServiceOptions, dicomServiceDependencies);
            return new DefaultDicomClientFactory(
                defaultClientOptions,
                defaultServiceOptions,
                logManager,
                advancedDicomClientConnectionFactory);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task SendingCStoreRequest_AfterPreviousCStoreRequestTimedOut_ShouldUseSeparateAssociation(int asyncInvoked)
        {
            // Arrange
            var port = Ports.GetNext();
            using var server = (ConfigurableDicomCStoreServer) DicomServerFactory.Create<ConfigurableDicomCStoreProvider, ConfigurableDicomCStoreServer>("127.0.0.1", port);
            server.Options.MaxPDULength = 1024;
            server.Options.LogDimseDatasets = false;
            server.Options.LogDataPDUs = false;
            server.Logger = _logger.IncludePrefix("Server");

            var originalDicomFilePath = @"./Test Data/TestPattern_Palette.dcm";
            var originalDicomFile = await DicomFile.OpenAsync(originalDicomFilePath);
            var responses = new ConcurrentQueue<DicomCStoreResponse>();
            var requestsThatSucceeded = new ConcurrentQueue<DicomCStoreRequest>();
            var requestsThatFailed = new ConcurrentQueue<DicomCStoreRequest>();
            var shouldTimeoutNextRequest = false;
            var requests = Enumerable.Range(0, 3)
                .Select(i => new DicomCStoreRequest(originalDicomFilePath))
                .ToList();
            var firstRequest = requests[0];
            var secondRequest = requests[1];
            var thirdRequest = requests[2];
            for (int i = 0; i < requests.Count; i++)
            {
                requests[i].OnResponseReceived = (request, response) =>
                {
                    if (response.Status.State == DicomState.Success)
                    {
                        requestsThatSucceeded.Enqueue(request);
                    }
                    else
                    {
                        requestsThatFailed.Enqueue(request);
                    }

                    responses.Enqueue(response);
                };
                requests[i].OnTimeout += (sender, args) =>
                {
                    requestsThatFailed.Enqueue(sender as DicomCStoreRequest);
                };
            }

            firstRequest.OnRequestSent += (sender, args) =>
            {
                shouldTimeoutNextRequest = true;
            };

            var receivedRequests = new List<DicomCStoreRequest>();
            var random = new Random();
            server.OnCStoreRequest = (association, storeRequest) =>
            {
                receivedRequests.Add(storeRequest);

                Thread.Sleep(random.Next(0, 100));

                return new DicomCStoreResponse(storeRequest, DicomStatus.Success);
            };

            var clientFactory = CreateClientFactory(new DicomClientTimeoutTest.ConfigurableNetworkManager(
                () =>
                {
                    // Simulate a single network error after the first request
                    if (shouldTimeoutNextRequest)
                    {
                        shouldTimeoutNextRequest = false;
                        Thread.Sleep(2000);
                    }
                }
            ));
            var client = clientFactory.Create("127.0.0.1", port, false, "AnySCU", "AnySCP");
            client.ClientOptions.AssociationLingerTimeoutInMs = 0;
            client.ServiceOptions.RequestTimeout = TimeSpan.FromSeconds(1);
            client.ServiceOptions.MaxPDULength = server.Options.MaxPDULength;
            client.Logger = _logger.IncludePrefix("Client");
            client.NegotiateAsyncOps(asyncInvoked, 1);
            Exception exception = null;

            // Act
            await client.AddRequestsAsync(requests).ConfigureAwait(false);
            try
            {
                await client.SendAsync(CancellationToken.None).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
            var messageIdsThatTimedOut = new HashSet<ushort>(requestsThatFailed.Select(r => r.MessageID));
            var numberOfRequestsThatSucceeded = responses.Count(r => r.Status.State == DicomState.Success);
            var numberOfRequestsThatFailed = requests.Count - numberOfRequestsThatSucceeded;

            _logger.Info($"Succeeded: {numberOfRequestsThatSucceeded}");
            _logger.Info($"Failed: {numberOfRequestsThatFailed}");

            Assert.Contains(requestsThatSucceeded, r => r.MessageID == firstRequest.MessageID);
            Assert.Contains(requestsThatFailed, r => r.MessageID == secondRequest.MessageID);
            Assert.Contains(requestsThatSucceeded, r => r.MessageID == thirdRequest.MessageID);

            var receivedRequestsThatSucceeded = receivedRequests
                .Where(r => !messageIdsThatTimedOut.Contains(r.MessageID))
                .ToList();

            var expectedPixelData = DicomPixelData.Create(originalDicomFile.Dataset);
            var expectedNumberOfFrames = expectedPixelData.NumberOfFrames;
            var expectedWidth = expectedPixelData.Width;
            var expectedHeight = expectedPixelData.Height;
            var expectedFrames = Enumerable.Range(0, expectedPixelData.NumberOfFrames)
                .Select(frame => expectedPixelData.GetFrame(frame))
                .ToArray();

            Parallel.For((long)0, receivedRequestsThatSucceeded.Count, i =>
            {
                var request = receivedRequestsThatSucceeded[(int) i];
                _logger.Info($"Verifying pixel data of request [{request.MessageID}]");

                var actualPixelData = DicomPixelData.Create(request.File.Dataset);

                var expectedPhotometricInterpretation = expectedPixelData.PhotometricInterpretation.Value;
                Assert.Equal(expectedPhotometricInterpretation, actualPixelData.PhotometricInterpretation.Value);
                Assert.Equal(expectedNumberOfFrames, actualPixelData.NumberOfFrames);
                Assert.Equal(expectedWidth, actualPixelData.Width);
                Assert.Equal(expectedHeight, actualPixelData.Height);

                for (var frame = 0; frame < actualPixelData.NumberOfFrames; frame++)
                {
                    var actualFrame = actualPixelData.GetFrame(frame);
                    var expectedFrame = expectedFrames[frame];

                    Assert.Equal(expectedFrame.Size, actualFrame.Size);

                    var actualData = actualFrame.Data;
                    var expectedData = expectedFrame.Data;

                    var actualFirstByte = actualData[0];
                    var actualMiddleByte = actualData[(int) (actualData.Length / 2.0)];
                    var actualLastByte = actualData[actualData.Length - 1];
                    var expectedFirstByte = expectedData[0];
                    var expectedMiddleByte = expectedData[(int) (expectedData.Length / 2.0)];
                    var expectedLastByte = expectedData[expectedData.Length - 1];
                    Assert.Equal(expectedData.Length, actualData.Length);
                    Assert.Equal(expectedFirstByte, actualFirstByte);
                    Assert.Equal(expectedMiddleByte, actualMiddleByte);
                    Assert.Equal(expectedLastByte, actualLastByte);
                }
            });
        }
    }

    #region support utilities

    public class ConfigurableDicomCStoreServer : DicomServer<ConfigurableDicomCStoreProvider>
    {
        private readonly DicomServiceDependencies _dicomServiceDependencies;
        public Func<DicomAssociation, DicomCStoreRequest, DicomCStoreResponse> OnCStoreRequest { get; set; }
        public Action<DicomAssociation> OnAssociationRequest { get; set; }

        public ConfigurableDicomCStoreServer(DicomServerDependencies dependencies,
            DicomServiceDependencies dicomServiceDependencies): base(dependencies)
        {
            _dicomServiceDependencies = dicomServiceDependencies ?? throw new ArgumentNullException(nameof(dicomServiceDependencies));
        }

        protected override ConfigurableDicomCStoreProvider CreateScp(INetworkStream stream)
        {
            if (OnCStoreRequest == null)
                throw new InvalidOperationException($"Failed to configure {nameof(OnCStoreRequest)} before opening an association");

            return new ConfigurableDicomCStoreProvider(stream, Encoding.UTF8, Logger, _dicomServiceDependencies, OnCStoreRequest, OnAssociationRequest);
        }
    }

    public class ConfigurableDicomCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        private readonly Func<DicomAssociation, DicomCStoreRequest, DicomCStoreResponse> _onCStoreRequest;
        private readonly Action<DicomAssociation> _onAssociationRequest;

        public ConfigurableDicomCStoreProvider(
            INetworkStream stream,
            Encoding fallbackEncoding,
            ILogger logger,
            DicomServiceDependencies dependencies,
            Func<DicomAssociation, DicomCStoreRequest, DicomCStoreResponse> onCStoreRequest,
            Action<DicomAssociation> onAssociationRequest
        ) : base(stream, fallbackEncoding, logger, dependencies)
        {
            _onCStoreRequest = onCStoreRequest ?? throw new ArgumentNullException(nameof(onCStoreRequest));
            _onAssociationRequest = onAssociationRequest;
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) { }

        public void OnConnectionClosed(Exception exception) { }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            if (_onAssociationRequest != null)
            {
                _onAssociationRequest(association.Clone());
            }

            foreach (var presentationContext in association.PresentationContexts)
            {
                foreach (var ts in presentationContext.GetTransferSyntaxes())
                {
                    presentationContext.SetResult(DicomPresentationContextResult.Accept, ts);
                    break;
                }
            }

            return SendAssociationAcceptAsync(association);
        }

        public Task OnReceiveAssociationReleaseRequestAsync()
        {
            return SendAssociationReleaseResponseAsync();
        }

        public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
        {
            return Task.FromResult(_onCStoreRequest(Association, request));
        }

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
        {
            return Task.CompletedTask;
        }
    }

    #endregion
}
