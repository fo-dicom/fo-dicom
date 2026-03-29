// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using FellowOakDicom.Tests.Network;
using FellowOakDicom.Tests.Network.Client;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.Network), Trait(TestTraits.Category, TestCategories.Network)]
    public class GH433
    {

        #region Unit tests

        [Fact]
        public async Task DicomClientSend_ToAcceptedAssociation_ShouldSendRequest()
        {
            using var server = DicomServerFactory.Create<DicomClientTest.MockCEchoProvider>(0);

            var locker = new object();

            var expected = DicomStatus.Success;
            DicomStatus actual = null;

            var client = DicomClientFactory.Create("localhost", server.Port, false, "SCU", "ANY-SCP");
            await client.AddRequestAsync(
                new DicomCEchoRequest
                    {
                        OnResponseReceived = (rq, rsp) =>
                            {
                                lock (locker) actual = rsp.Status;
                            }
                    });
            await client.SendAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DicomClientSend_ToRejectedAssociation_ShouldNotSendRequest()
        {
            using var server = DicomServerFactory.Create<DicomClientTest.MockCEchoProvider>(0);

            var locker = new object();
            DicomStatus status = null;

            var client = DicomClientFactory.Create("localhost", server.Port, false, "SCU", "WRONG-SCP");
            await client.AddRequestAsync(
                new DicomCEchoRequest
                {
                    OnResponseReceived = (rq, rsp) =>
                    {
                        lock (locker) status = rsp.Status;
                    }
                });

            try
            {
                await client.SendAsync();
            }
            catch
            {
            }

            Assert.Null(status);
        }

        #endregion
    }
}
