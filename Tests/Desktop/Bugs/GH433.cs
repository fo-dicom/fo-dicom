// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Network;

using Xunit;

namespace Dicom.Bugs
{
    [Collection("Network"), Trait("Category", "Network")]
    public class GH433
    {
        #region Unit tests

        [Fact]
        public void DicomClientSend_ToAcceptedAssociation_ShouldSendRequest()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomClientTest.MockCEchoProvider>(port))
            {
                var locker = new object();

                var expected = DicomStatus.Success;
                DicomStatus actual = null;

                var client = new DicomClient();
                client.AddRequest(
                    new DicomCEchoRequest
                        {
                            OnResponseReceived = (rq, rsp) =>
                                {
                                    lock (locker) actual = rsp.Status;
                                }
                        });
                client.Send("localhost", port, false, "SCU", "ANY-SCP");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void DicomClientSend_ToRejectedAssociation_ShouldNotSendRequest()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomClientTest.MockCEchoProvider>(port))
            {
                var locker = new object();
                DicomStatus status = null;

                var client = new DicomClient();
                client.AddRequest(
                    new DicomCEchoRequest
                    {
                        OnResponseReceived = (rq, rsp) =>
                        {
                            lock (locker) status = rsp.Status;
                        }
                    });

                try
                {
                    client.Send("localhost", port, false, "SCU", "WRONG-SCP");
                }
                catch
                {
                }

                Assert.Null(status);
            }
        }

        #endregion
    }
}
