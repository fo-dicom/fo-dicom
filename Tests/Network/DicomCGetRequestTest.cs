// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.Threading;

    using Xunit;

    public class DicomCGetRequestTest
    {
        [Fact(Skip = "Require running Q/R SCP containing CT-MONO2-16-ankle image")]
        public void DicomCGetRequest_OneImageInSeries_Received()
        {
            var client = new DicomClient();
            client.NegotiateAsyncOps(2, 1);

            var pcs = DicomPresentationContext.GetScpRolePresentationContextsFromStorageUids(
                DicomStorageCategory.Image,
                DicomTransferSyntax.ExplicitVRLittleEndian,
                DicomTransferSyntax.ImplicitVRLittleEndian,
                DicomTransferSyntax.ImplicitVRBigEndian);
            client.AdditionalPresentationContexts.AddRange(pcs);

            DicomDataset dataset = null;
            client.OnCStoreRequest = request =>
                {
                    dataset = request.Dataset;
                    return new DicomCStoreResponse(request, DicomStatus.Success);
                };

            var get = new DicomCGetRequest(
                "1.2.840.113619.2.1.1.322987881.621.736170080.681",
                "1.2.840.113619.2.1.2411.1031152382.365.736169244");

            var handle = new ManualResetEventSlim();
            get.OnResponseReceived = (request, response) =>
            {
                handle.Set();
            };
            client.AddRequest(get);
            client.Send("localhost", 11112, false, "SCU", "COMMON");
            handle.Wait();

            Assert.Equal("RT ANKLE", dataset.Get<string>(DicomTag.StudyDescription));
        }

        [Fact(Skip = "Require running Q/R SCP containing specific study")]
        public void DicomCGetRequest_PickCTImagesInStudy_OnlyCTImagesRetrieved()
        {
            var client = new DicomClient();
            client.NegotiateAsyncOps(2, 1);

            var pc = DicomPresentationContext.GetScpRolePresentationContext(DicomUID.CTImageStorage);
            client.AdditionalPresentationContexts.Add(pc);

            var counter = 0;
            var locker = new object();
            client.OnCStoreRequest = request =>
            {
                lock (locker)
                {
                    ++counter;
                }

                return new DicomCStoreResponse(request, DicomStatus.Success);
            };

            var get = new DicomCGetRequest("1.2.840.113619.2.55.3.2609388324.145.1222836278.84");

            var handle = new ManualResetEventSlim();
            get.OnResponseReceived = (request, response) =>
            {
                if (response.Remaining == 0)
                {
                    handle.Set();
                }
            };
            client.AddRequest(get);
            client.Send("localhost", 11112, false, "SCU", "COMMON");
            handle.Wait();

            Assert.Equal(140, counter);
        }
    }
}
