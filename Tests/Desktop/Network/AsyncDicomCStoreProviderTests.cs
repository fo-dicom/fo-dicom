using System;
using System.Text;
using System.Threading.Tasks;
using Dicom.Helpers;
using Dicom.Log;
using Xunit;
using Xunit.Abstractions;

namespace Dicom.Network
{
    public class AsyncDicomCStoreProviderTests
    {
        private readonly XUnitDicomLogger _logger;

        public AsyncDicomCStoreProviderTests(ITestOutputHelper testOutputHelper)
        {
            _logger = new XUnitDicomLogger(testOutputHelper)
                .IncludeTimestamps()
                .IncludeThreadId()
                .WithMinimumLevel(LogLevel.Debug);
        }

        [Fact]
        public async Task OnCStoreRequestAsync_ShouldRespond()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<AsyncDicomCStoreProvider>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Network.Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

                DicomCStoreResponse response = null;
                DicomRequest.OnTimeoutEventArgs timeout = null;
                var request = new DicomCStoreRequest(@"./Test Data/10200904.dcm")
                {
                    OnResponseReceived = (req, res) => response = res,
                    OnTimeout = (sender, args) => timeout = args
                };

                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.NotNull(response);
                Assert.Equal(DicomStatus.Success, response.Status);
                Assert.Null(timeout);
            }
        }
    }

    #region helper classes

    public class AsyncDicomCStoreProvider : DicomService, IDicomServiceProvider, IAsyncDicomCStoreProvider
    {
        public AsyncDicomCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        async Task WaitForALittleBit()
        {
            var ms = new Random().Next(10);
            await Task.Delay(ms).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            await WaitForALittleBit().ConfigureAwait(false);

            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(DicomPresentationContextResult.Accept);
            }

            await SendAssociationAcceptAsync(association);
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

        public async Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
        {
            await WaitForALittleBit().ConfigureAwait(false);

            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
        {
            throw new NotImplementedException();
        }
    }


    #endregion
}
