// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;
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
                var client = new Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
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

        [Fact]
        public async Task OnCStoreRequestAsync_PreferedTransfersyntax()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<AsyncDicomCStoreProviderPreferingUncompressedTS>(port, logger: _logger.IncludePrefix("DicomServer")))
            {
                var client = new Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP")
                {
                    Logger = _logger.IncludePrefix(typeof(DicomClient).Name)
                };

                int numberOfContexts = 0;
                DicomTransferSyntax accpetedTS = null;
                // create a request with a jpeg-encoded file
                var request = new DicomCStoreRequest(@"./Test Data/CT1_J2KI");
                client.AssociationAccepted += (sender, e) =>
                {
                    numberOfContexts = e.Association.PresentationContexts.Count;
                    accpetedTS = e.Association.PresentationContexts.First().AcceptedTransferSyntax;
                };
                await client.AddRequestAsync(request).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.Equal(2, numberOfContexts); // one for the jpeg2k TS and one for the mandatory ImplicitLittleEndian
                Assert.Equal(DicomTransferSyntax.JPEG2000Lossy, accpetedTS);
            }
        }


    }

    #region helper classes

    public class AsyncDicomCStoreProviderPreferingUncompressedTS : AsyncDicomCStoreProvider
    {

        private static readonly DicomTransferSyntax[] _acceptedTransferSyntaxes = new DicomTransferSyntax[]
           {
               DicomTransferSyntax.ExplicitVRLittleEndian,
               DicomTransferSyntax.ExplicitVRBigEndian,
               DicomTransferSyntax.ImplicitVRLittleEndian
           };

        private static readonly DicomTransferSyntax[] _acceptedImageTransferSyntaxes = new DicomTransferSyntax[]
        {
               // Uncompressed
               DicomTransferSyntax.ExplicitVRLittleEndian,
               DicomTransferSyntax.ExplicitVRBigEndian,
               DicomTransferSyntax.ImplicitVRLittleEndian,
               // Lossless
               DicomTransferSyntax.JPEGLSLossless,
               DicomTransferSyntax.JPEG2000Lossless,
               DicomTransferSyntax.JPEGProcess14SV1,
               DicomTransferSyntax.JPEGProcess14,
               DicomTransferSyntax.RLELossless,
               // Lossy
               DicomTransferSyntax.JPEGLSNearLossless,
               DicomTransferSyntax.JPEG2000Lossy,
               DicomTransferSyntax.JPEGProcess1,
               DicomTransferSyntax.JPEGProcess2_4,
        };

        public AsyncDicomCStoreProviderPreferingUncompressedTS(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        { }

        public override Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.Verification)
                {
                    pc.AcceptTransferSyntaxes(_acceptedTransferSyntaxes);
                }
                else if (pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None)
                {
                    pc.AcceptTransferSyntaxes(_acceptedImageTransferSyntaxes, true);
                }
            }

            return SendAssociationAcceptAsync(association);
        }

    }


    public class AsyncDicomCStoreProvider : DicomService, IDicomServiceProvider, IAsyncDicomCStoreProvider
    {
        public AsyncDicomCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        /// <inheritdoc />
        public virtual async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
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
            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
        {
            throw new NotImplementedException();
        }
    }


    #endregion
}
