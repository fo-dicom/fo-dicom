// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network;
using FellowOakDicom.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DicomClient = FellowOakDicom.Network.Client.DicomClient;

namespace FellowOakDicom.Tests.Network
{

    [Collection("Network"), Trait("Category", "Network")]
    public class DicomAcceptedPresentationContextTest
    {

        [Fact]
        public async Task AcceptEchoButNotStoreContexts()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<AcceptOnlyEchoProvider>(port))
            {
                var echoReq = new DicomCEchoRequest();
                DicomStatus echoStatus = DicomStatus.Pending;
                echoReq.OnResponseReceived += (req, resp) => echoStatus = resp.Status;

                var storeReq = new DicomCStoreRequest(@".\Test Data\CT1_J2KI");
                DicomStatus storeStatus = DicomStatus.Pending;
                storeReq.OnResponseReceived += (req, resp) => storeStatus = resp.Status;

                var filmSession = new FilmSession(DicomUID.BasicFilmSessionSOPClass, DicomUID.Generate());
                var printReq = new DicomNCreateRequest(filmSession.SOPClassUID, filmSession.SOPInstanceUID);
                DicomStatus printStatus = DicomStatus.Pending;
                printReq.OnResponseReceived += (req, resp) => printStatus = resp.Status;

                var client = new DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestsAsync(echoReq, storeReq, printReq);

                await client.SendAsync();

                Assert.Equal(DicomStatus.Success, echoStatus);
                Assert.Equal(DicomStatus.SOPClassNotSupported, storeStatus);
                Assert.Equal(DicomStatus.SOPClassNotSupported, printStatus);
            }
        }

        [Fact]
        public async Task AcceptPrintContexts()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<AcceptOnlyEchoPrintManagementProvider>(port))
            {
                var echoReq = new DicomCEchoRequest();
                DicomStatus echoStatus = DicomStatus.Pending;
                echoReq.OnResponseReceived += (req, resp) => echoStatus = resp.Status;

                var storeReq = new DicomCStoreRequest(@".\Test Data\CT1_J2KI");
                DicomStatus storeStatus = DicomStatus.Pending;
                storeReq.OnResponseReceived += (req, resp) => storeStatus = resp.Status;

                var filmSession = new FilmSession(DicomUID.BasicFilmSessionSOPClass, DicomUID.Generate());
                var printReq = new DicomNCreateRequest(filmSession.SOPClassUID, filmSession.SOPInstanceUID);
                DicomStatus printStatus = DicomStatus.Pending;
                printReq.OnResponseReceived += (req, resp) => printStatus = resp.Status;

                var client = new DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestsAsync(echoReq, storeReq, printReq);

                await client.SendAsync();

                Assert.Equal(DicomStatus.Success, echoStatus);
                Assert.Equal(DicomStatus.SOPClassNotSupported, storeStatus);
                Assert.Equal(DicomStatus.Success, printStatus);
            }
        }

        [Fact]
        public async Task AcceptStoreContexts()
        {
            int port = Ports.GetNext();
            using (DicomServer.Create<AcceptOnlyEchoStoreProvider>(port))
            {
                var echoReq = new DicomCEchoRequest();
                DicomStatus echoStatus = DicomStatus.Pending;
                echoReq.OnResponseReceived += (req, resp) => echoStatus = resp.Status;

                var storeReq = new DicomCStoreRequest(@".\Test Data\CT1_J2KI");
                DicomStatus storeStatus = DicomStatus.Pending;
                storeReq.OnResponseReceived += (req, resp) => storeStatus = resp.Status;

                var filmSession = new FilmSession(DicomUID.BasicFilmSessionSOPClass, DicomUID.Generate());
                var printReq = new DicomNCreateRequest(filmSession.SOPClassUID, filmSession.SOPInstanceUID);
                DicomStatus printStatus = DicomStatus.Pending;
                printReq.OnResponseReceived += (req, resp) => printStatus = resp.Status;

                var client = new DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestsAsync(echoReq, storeReq, printReq);

                await client.SendAsync();

                Assert.Equal(DicomStatus.Success, echoStatus);
                Assert.Equal(DicomStatus.Success, storeStatus);
                Assert.Equal(DicomStatus.SOPClassNotSupported, printStatus);
            }
        }


    }


    internal class AcceptOnlyEchoProvider : SimpleAssociationAcceptProvider
    {
        public AcceptOnlyEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log) : base(stream, fallbackEncoding, log)
        {
            AcceptedSopClasses.Add(DicomUID.Verification);
        }
    }

    internal class AcceptOnlyEchoPrintManagementProvider : SimpleAssociationAcceptProvider
    {
        public AcceptOnlyEchoPrintManagementProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log) : base(stream, fallbackEncoding, log)
        {
            AcceptedSopClasses.AddRange(new[] { DicomUID.Verification, DicomUID.BasicGrayscalePrintManagementMetaSOPClass });
        }
    }

    internal class AcceptOnlyEchoStoreProvider : SimpleAssociationAcceptProvider
    {
        public AcceptOnlyEchoStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log) : base(stream, fallbackEncoding, log)
        {
            AcceptedSopClasses.Add(DicomUID.Verification);
            AcceptedSopClasses.AddRange(DicomUID.Enumerate().Where(u => u.IsImageStorage));
        }
    }

    internal class SimpleAssociationAcceptProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider, IDicomNServiceProvider, IDicomCEchoProvider
    {

        private static readonly DicomTransferSyntax[] AcceptedTransferSyntaxes =
        {
            DicomTransferSyntax.ExplicitVRLittleEndian,
            DicomTransferSyntax.ExplicitVRBigEndian,
            DicomTransferSyntax.ImplicitVRLittleEndian
        };

        protected List<DicomUID> AcceptedSopClasses { get; } = new List<DicomUID>();

        public SimpleAssociationAcceptProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
          : base(stream, fallbackEncoding, log)
        {
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                if (AcceptedSopClasses.Contains(pc.AbstractSyntax))
                {
                    pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
                }
            }

            return SendAssociationAcceptAsync(association);
        }

        public Task OnReceiveAssociationReleaseRequestAsync()
        {
            return SendAssociationReleaseResponseAsync();
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        public void OnConnectionClosed(Exception exception)
        {
        }

        public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
            => Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success) { Dataset = request.Dataset });

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
            => Task.CompletedTask;

        public Task<DicomNActionResponse> OnNActionRequestAsync(DicomNActionRequest request)
            => Task.FromResult(new DicomNActionResponse(request, DicomStatus.Success) { Dataset = request.Dataset });

        public Task<DicomNCreateResponse> OnNCreateRequestAsync(DicomNCreateRequest request)
            => Task.FromResult(new DicomNCreateResponse(request, DicomStatus.Success) { Dataset = request.Dataset });

        public Task<DicomNDeleteResponse> OnNDeleteRequestAsync(DicomNDeleteRequest request)
            => Task.FromResult(new DicomNDeleteResponse(request, DicomStatus.Success) { Dataset = request.Dataset });

        public Task<DicomNEventReportResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request)
            => Task.FromResult(new DicomNEventReportResponse(request, DicomStatus.Success) { Dataset = request.Dataset });

        public Task<DicomNGetResponse> OnNGetRequestAsync(DicomNGetRequest request)
            => Task.FromResult(new DicomNGetResponse(request, DicomStatus.Success) { Dataset = request.Dataset });

        public Task<DicomNSetResponse> OnNSetRequestAsync(DicomNSetRequest request)
            => Task.FromResult(new DicomNSetResponse(request, DicomStatus.Success) { Dataset = request.Dataset });

        public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
            => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success) { Dataset = request.Dataset });
    }

}
