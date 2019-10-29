// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using FellowOakDicom.Tests.Network;
using FellowOakDicom.Tests.Network.Client;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("Network"), Trait("Category", "Network")]
    public class GH433
    {
        #region Unit tests

        [Fact]
        public void OldDicomClientSend_ToAcceptedAssociation_ShouldSendRequest()
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
        public async Task DicomClientSend_ToAcceptedAssociation_ShouldSendRequest()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomClientTest.MockCEchoProvider>(port))
            {
                var locker = new object();

                var expected = DicomStatus.Success;
                DicomStatus actual = null;

                var client = new FellowOakDicom.Network.Client.DicomClient("localhost", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(
                    new DicomCEchoRequest
                        {
                            OnResponseReceived = (rq, rsp) =>
                                {
                                    lock (locker) actual = rsp.Status;
                                }
                        }).ConfigureAwait(false);
                await client.SendAsync().ConfigureAwait(false);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void OldDicomClientSend_ToRejectedAssociation_ShouldNotSendRequest()
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

        [Fact]
        public async Task DicomClientSend_ToRejectedAssociation_ShouldNotSendRequest()
        {
            var port = Ports.GetNext();

            using (DicomServer.Create<DicomClientTest.MockCEchoProvider>(port))
            {
                var locker = new object();
                DicomStatus status = null;

                var client = new FellowOakDicom.Network.Client.DicomClient("localhost", port, false, "SCU", "WRONG-SCP");
                await client.AddRequestAsync(
                    new DicomCEchoRequest
                    {
                        OnResponseReceived = (rq, rsp) =>
                        {
                            lock (locker) status = rsp.Status;
                        }
                    }).ConfigureAwait(false);

                try
                {
                    await client.SendAsync().ConfigureAwait(false);
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
