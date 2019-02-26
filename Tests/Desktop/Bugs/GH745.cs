// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Log;
using Dicom.Network;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dicom.Bugs
{

    [Collection("Network")]
    public class GH745
    {

        private readonly ITestOutputHelper output;

        public GH745(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task DicomClientShallNotCloseConnectionTooEarly_CEchoSerialAsync(int expected)
        {
            var port = Ports.GetNext();

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) await Task.Delay(50);

                var actual = 0;

                var client = new DicomClient();
                for (var i = 0; i < expected; i++)
                {
                    client.AddRequest(
                        new DicomCEchoRequest
                        {
                            OnResponseReceived = (req, res) =>
                            {
                                output.WriteLine("Response #{0} / expected #{1}", actual, req.UserState);
                                Interlocked.Increment(ref actual);
                                output.WriteLine("         #{0} / expected #{1}", actual - 1, req.UserState);
                            },
                            UserState = i
                        }
                    );
                    output.WriteLine("Sending #{0}", i);
                    await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP", 600 * 1000);
                    output.WriteLine("Sent (or timed out) #{0}", i);
                    if (i != actual-1)
                    {
                        output.WriteLine("  waiting #{0}", i);
                        await Task.Delay((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                        output.WriteLine("  waited #{0}", i);
                    }
                }

                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData(10)]
        [InlineData(75)]
        [InlineData(200)]
        public async Task DicomClientShallNotCloseConnectionTooEarly_CEchoParallelAsync(int expected)
        {
            int port = Ports.GetNext();

            using (var server = DicomServer.Create<DicomCEchoProvider>(port))
            {
                while (!server.IsListening) await Task.Delay(50);

                var actual = 0;

                var requests = Enumerable.Range(0, expected).Select(
                    async requestIndex =>
                    {
                        var client = new DicomClient();
                        client.AddRequest(
                            new DicomCEchoRequest
                            {
                                OnResponseReceived = (req, res) =>
                                {
                                    output.WriteLine("Response #{0}", requestIndex);
                                    Interlocked.Increment(ref actual);
                                }
                            }
                        );

                        output.WriteLine("Sending #{0}", requestIndex);
                        await client.SendAsync("127.0.0.1", port, false, "SCU", "ANY-SCP", 600 * 1000);
                        output.WriteLine("Sent (or timed out) #{0}", requestIndex);
                    }
                ).ToArray();

                await Task.WhenAll(requests);

                Assert.Equal(expected, actual);
            }
        }

    }



    /// <summary>
    /// Implementation of a C-ECHO Service Class Provider.
    /// </summary>
    public class DicomCEchoProviderRelay : DicomService, IDicomServiceProvider, IDicomCEchoProvider
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomCEchoProvider"/> class.
        /// </summary>
        /// <param name="stream">Network stream on which DICOM communication is establshed.</param>
        /// <param name="fallbackEncoding">Text encoding if not specified within messaging.</param>
        /// <param name="log">DICOM logger.</param>
        public DicomCEchoProviderRelay(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        /// <inheritdoc />
        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(pc.AbstractSyntax == DicomUID.Verification
                    ? DicomPresentationContextResult.Accept
                    : DicomPresentationContextResult.RejectAbstractSyntaxNotSupported);
            }

            return SendAssociationAcceptAsync(association);
        }

        /// <inheritdoc />
        public Task OnReceiveAssociationReleaseRequestAsync()
        {
            return SendAssociationReleaseResponseAsync();
        }


        public Action<DicomAbortSource, DicomAbortReason> ReceiveAbort { get; set; }

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            ReceiveAbort?.Invoke(source, reason);
        }

        public Action<Exception> ConnectionClosed { get; set; }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
            ConnectionClosed?.Invoke(exception);
        }

        public Func<DicomCEchoRequest, DicomCEchoResponse> EchoRequest { get; set; }

        /// <summary>
        /// Event handler for C-ECHO request.
        /// </summary>
        /// <param name="request">C-ECHO request.</param>
        /// <returns>C-ECHO response with Success status.</returns>
        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return EchoRequest?.Invoke(request) ?? new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }
}
