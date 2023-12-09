// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using Xunit;
using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Tests.Network
{

    [Collection(TestCollections.Network)]
    public class DicomNEventReportResponseTest
    {
        #region Unit tests

        [Fact]
        public void EventTypeIDGetter_ResponseCreatedFromRequest_DoesNotThrow()
        {
            var request = new DicomNEventReportRequest(
                DicomUID.BasicFilmSession,
                new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                1);
            var response = new DicomNEventReportResponse(request, DicomStatus.Success);

            var exception = Record.Exception(() => response.EventTypeID);
            Assert.Null(exception);
        }

        [Fact]
        public void SOPInstanceUIDGetter_ResponseCreatedFromRequest_DoesNotThrow()
        {
            var request = new DicomNEventReportRequest(
                DicomUID.BasicFilmSession,
                new DicomUID("1.2.3", null, DicomUidType.SOPInstance),
                1);
            var response = new DicomNEventReportResponse(request, DicomStatus.Success);

            var exception = Record.Exception(() => response.SOPInstanceUID);
            Assert.Null(exception);
        }

        #endregion

#if NET462
        [Fact(Skip = "This test is flaky in .NET Framework")]
#else
        [Fact]
#endif
        public async Task ClientHandleNEventReport_SynchronousEvent()
        {
            var port = Ports.GetNext();
            using (DicomServerFactory.Create<SimpleStorageComitmentProvider>(port))
            {
                DicomStatus status = null;
                int verifiedInstances = 0;
                DateTime stampNActionResponse = DateTime.MinValue;
                DateTime stampNEventReportRequest = DateTime.MinValue;

                var dicomClient = DicomClientFactory.Create("127.0.0.1", port, false, "SCU", "ANY-SCP");

                var nActionDicomDataSet = new DicomDataset
                {
                    { DicomTag.TransactionUID,  DicomUIDGenerator.GenerateDerivedFromUUID().UID },
                    {
                        DicomTag.ReferencedSOPSequence,
                        new DicomDataset()
                        {
                            { DicomTag.ReferencedSOPClassUID, "1.2.840.10008.5.1.4.1.1.1" },
                            { DicomTag.ReferencedSOPInstanceUID, "1.3.46.670589.30.2273540226.4.54" }
                        },
                        new DicomDataset()
                        {
                            { DicomTag.ReferencedSOPClassUID, "1.2.840.10008.5.1.4.1.1.1" },
                            { DicomTag.ReferencedSOPInstanceUID, "1.3.46.670589.30.2273540226.4.59" }
                        }
                    }
                };

                var nActionRequest = new DicomNActionRequest(DicomUID.StorageCommitmentPushModel, DicomUID.StorageCommitmentPushModel, 1)
                {
                    Dataset = nActionDicomDataSet,
                    OnResponseReceived = (DicomNActionRequest request, DicomNActionResponse response) =>
                    {
                        status = response.Status;
                        stampNActionResponse = DateTime.Now;
                    },
                };

                await dicomClient.AddRequestAsync(nActionRequest);

                dicomClient.OnNEventReportRequest = (eventReq) =>
                {
                    var refSopSequence = eventReq.Dataset.GetSequence(DicomTag.ReferencedSOPSequence);
                    foreach (var item in refSopSequence.Items)
                    {
                        verifiedInstances += 1;
                    }
                    stampNEventReportRequest = DateTime.Now;
                    return Task.FromResult(new DicomNEventReportResponse(eventReq, DicomStatus.Success));
                };

                dicomClient.ClientOptions.AssociationLingerTimeoutInMs = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
                await dicomClient.SendAsync();

                Assert.Equal(DicomStatus.Success, status);
                Assert.Equal(2, verifiedInstances);
                Assert.True(stampNActionResponse < stampNEventReportRequest);
            }
        }

    }


    internal class SimpleStorageComitmentProvider : DicomService, IDicomServiceProvider, IDicomNServiceProvider, IDicomNEventReportRequestProvider
    {
        private static readonly DicomTransferSyntax[] _acceptedTransferSyntaxes =
        {
            DicomTransferSyntax.ExplicitVRLittleEndian,
            DicomTransferSyntax.ExplicitVRBigEndian,
            DicomTransferSyntax.ImplicitVRLittleEndian
        };


        public SimpleStorageComitmentProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.AcceptTransferSyntaxes(_acceptedTransferSyntaxes);
            }

            return SendAssociationAcceptAsync(association);
        }

        public Task OnReceiveAssociationReleaseRequestAsync() => SendAssociationReleaseResponseAsync();

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        public void OnConnectionClosed(Exception exception)
        {
        }

        public Task<DicomNActionResponse> OnNActionRequestAsync(DicomNActionRequest request)
        {
            /*
            // first return the success-response
            await SendResponseAsync(new DicomNActionResponse(request, DicomStatus.Success));

            // then synchronously send NEvents
            if (request.Dataset.Contains(DicomTag.ReferencedSOPSequence))
            {
                var referencedSequence = request.Dataset.GetSequence(DicomTag.ReferencedSOPSequence);
                foreach (var referencedDataset in referencedSequence)
                {
                    // This can also be done within a thread later
                    //_ = Task.Run(async () =>
                    //  {
                    //      await Task.Delay(TimeSpan.FromSeconds(1));
                    var resultDs = new DicomDataset
                          {
                            {
                                DicomTag.ReferencedSOPSequence,
                                new DicomDataset
                                {
                                    { DicomTag.ReferencedSOPClassUID, referencedDataset.GetString(DicomTag.ReferencedSOPClassUID) },
                                    { DicomTag.ReferencedSOPInstanceUID, referencedDataset.GetString(DicomTag.ReferencedSOPInstanceUID) }
                                }
                            }
                          };
                    await SendRequestAsync(new DicomNEventReportRequest(DicomUID.StorageCommitmentPushModelSOPClass, DicomUID.Generate(), 1) { Dataset = resultDs });
                    //});
                }
            }

            return null;
            */
            return Task.FromResult(new DicomNActionResponse(request, DicomStatus.Success));
        }

        public async Task OnSendNEventReportRequestAsync(DicomNActionRequest request)
        {
            // synchronously send NEvents
            if (request.Dataset.Contains(DicomTag.ReferencedSOPSequence))
            {
                var referencedSequence = request.Dataset.GetSequence(DicomTag.ReferencedSOPSequence);
                foreach (var referencedDataset in referencedSequence)
                {
                    var resultDs = new DicomDataset
                          {
                            {
                                DicomTag.ReferencedSOPSequence,
                                new DicomDataset
                                {
                                    { DicomTag.ReferencedSOPClassUID, referencedDataset.GetString(DicomTag.ReferencedSOPClassUID) },
                                    { DicomTag.ReferencedSOPInstanceUID, referencedDataset.GetString(DicomTag.ReferencedSOPInstanceUID) }
                                }
                            }
                          };
                    await SendRequestAsync(new DicomNEventReportRequest(DicomUID.StorageCommitmentPushModel, DicomUID.StorageCommitmentPushModel, 2) { Dataset = resultDs });
                }
            }
        }

        public Task<DicomNCreateResponse> OnNCreateRequestAsync(DicomNCreateRequest request) => throw new NotImplementedException();
        public Task<DicomNDeleteResponse> OnNDeleteRequestAsync(DicomNDeleteRequest request) => throw new NotImplementedException();
        public Task<DicomNEventReportResponse> OnNEventReportRequestAsync(DicomNEventReportRequest request) => throw new NotImplementedException();
        public Task<DicomNGetResponse> OnNGetRequestAsync(DicomNGetRequest request) => throw new NotImplementedException();
        public Task<DicomNSetResponse> OnNSetRequestAsync(DicomNSetRequest request) => throw new NotImplementedException();

    }
}

