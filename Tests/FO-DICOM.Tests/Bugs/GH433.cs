// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Network;
using FellowOakDicom.Tests.Network;
using FellowOakDicom.Tests.Network.Client;
using System.Threading.Tasks;
using FellowOakDicom.Network.Client;
using Xunit;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("Network"), Trait("Category", "Network")]
    public class GH433 : IClassFixture<GlobalFixture>
    {
        private readonly IDicomServerFactory _serverFactory;
        private readonly IDicomClientFactory _clientFactory;

        public GH433(GlobalFixture globalFixture)
        {
            _serverFactory = globalFixture.GetRequiredService<IDicomServerFactory>();
            _clientFactory = globalFixture.GetRequiredService<IDicomClientFactory>();
        }

        #region Unit tests

        [Fact]
        public async Task DicomClientSend_ToAcceptedAssociation_ShouldSendRequest()
        {
            var port = Ports.GetNext();

            using (_serverFactory.Create<DicomClientTest.MockCEchoProvider>(port))
            {
                var locker = new object();

                var expected = DicomStatus.Success;
                DicomStatus actual = null;

                var client = _clientFactory.Create("localhost", port, false, "SCU", "ANY-SCP");
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
        public async Task DicomClientSend_ToRejectedAssociation_ShouldNotSendRequest()
        {
            var port = Ports.GetNext();

            using (_serverFactory.Create<DicomClientTest.MockCEchoProvider>(port))
            {
                var locker = new object();
                DicomStatus status = null;

                var client = _clientFactory.Create("localhost", port, false, "SCU", "WRONG-SCP");
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
