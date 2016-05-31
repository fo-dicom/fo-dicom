// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using Xunit;

    public class DicomCGetRequestTest
    {
        [Fact(Skip = "Require running Q/R SCP containing CT-MONO2-16-ankle image")]
        public void TestCGetWorkflow()
        {
            var client = new DicomClient();
            client.NegotiateAsyncOps(16, 16);

            var pc = new DicomPresentationContext(3, DicomUID.SecondaryCaptureImageStorage, false, true);
            pc.AddTransferSyntax(DicomTransferSyntax.ImplicitVRLittleEndian);
            client.AdditionalPresentationContexts.Add(pc);

            var get = new DicomCGetRequest(
                "1.2.840.113619.2.1.1.322987881.621.736170080.681",
                "1.2.840.113619.2.1.2411.1031152382.365.736169244");

            get.OnResponseReceived = (request, response) =>
            {
                Assert.Equal(0, response.Failures);
            };
            client.AddRequest(get);
            client.Send("localhost", 11112, false, "SCU", "COMMON");
        }
    }
}
