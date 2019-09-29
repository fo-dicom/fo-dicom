// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Dicom.Network
{
    [Collection("Network"), Trait("Category", "Network")]
    public partial class DicomServiceCoreTest
    {
        private class CMoveResponder
        {
            public IReadOnlyList<DicomCMoveResponse> Responses { get; set; }
            public int NumberOfSentResponses { get; private set; } = 0;
            private ITestOutputHelper output_;
            public CMoveResponder(ITestOutputHelper output)
            {
                output_ = output;
            }

            public async IAsyncEnumerable<DicomCMoveResponse> Handler(DicomCMoveRequest req)
            {
                foreach (var response in Responses)
                {
                    await Task.Delay(10);
                    NumberOfSentResponses++;
                    output_.WriteLine($"CMoveResponder.Yielding. NumberOfSentResponses:{NumberOfSentResponses}");
                    yield return new DicomCMoveResponse(req, response.Status)
                    {
                        Failures = response.Failures,
                        Completed = response.Completed,
                        Remaining = response.Remaining
                    };
                }
            }
        }

        ITestOutputHelper output_;
        public DicomServiceCoreTest(ITestOutputHelper output)
        {
            output_ = output;
        }

        #region Unit tests
        [Fact]
        public async Task Send_CMoveRequest_DataSufficientlyTransported()
        {
            int port = Ports.GetNext();

            var request = new DicomCMoveRequest("DESTAE", "2.999.123");

            var responder = new CMoveResponder(output_)
            {
                Responses = new[] {
                new DicomCMoveResponse(request, DicomStatus.Pending) { Remaining = 2, Failures = 0, Completed = 0 },
                new DicomCMoveResponse(request, DicomStatus.Pending) { Remaining = 1, Failures = 1, Completed = 0 },
                new DicomCMoveResponse(request, DicomStatus.Pending) { Remaining = 0, Failures = 1, Completed = 1 },
                new DicomCMoveResponse(request, DicomStatus.Success) { Remaining = 0, Failures = 1, Completed = 1 } }
            };

            var receivedResponses = new List<DicomCMoveResponse>();
            using (var srv = DicomServer.Create<SimpleCMoveProvider>("127.0.0.1", port, userState: responder))
            {

                request.OnResponseReceived = (req, res) =>
                {
                    receivedResponses.Add(res);
                    output_.WriteLine($"Request.OnResponseReceived: receivedResponses.Count:{receivedResponses.Count}");
                    Assert.Equal(receivedResponses.Count, responder.NumberOfSentResponses);
                    Assert.True(receivedResponses.Zip(responder.Responses, (a, b) => CMoveResponseEqual_(a, b)).All(x => x));
                };

                var client = new Client.DicomClient("127.0.0.1", port, false, "SCU", "ANY-SCP");
                await client.AddRequestAsync(request).ConfigureAwait(false);

                await client.SendAsync().ConfigureAwait(false);
            }

            Assert.Equal(responder.Responses.Count, receivedResponses.Count);
            Assert.True(receivedResponses.Zip(responder.Responses, (a, b) => CMoveResponseEqual_(a, b)).All(x => x));
        }

        private static bool CMoveResponseEqual_(DicomCMoveResponse a, DicomCMoveResponse b)
        {
            return a.Failures == b.Failures && a.Completed == b.Completed && a.Remaining == b.Remaining && a.Status == b.Status;
        }
        #endregion
    }
}
