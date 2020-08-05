// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Dicom.Log;
using Dicom.Network;

namespace DICOM.Benchmarks.Desktop
{
    [MemoryDiagnoser]
    public class DicomClientBenchmarks
    {
        private IDicomServer _server;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _server = DicomServer.Create<SimpleDicomCEchoProvider>(NetworkManager.IPv4Any, Ports.GetNext(), options: new DicomServiceOptions
            {
                LogDimseDatasets = false,
                LogDataPDUs = false,
            });
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _server.Dispose();
        }

        [Benchmark]
        public async Task NewDicomClient_SendEchos()
        {
            var client = new Dicom.Network.Client.DicomClient("127.0.0.1", _server.Port, false, "SCU", "ANY-SCP");
            client.NegotiateAsyncOps(1, 1);
            client.AssociationLingerTimeoutInMs = 0;

            var requests = Enumerable.Range(0, 1000).Select(i => new DicomCEchoRequest());

            await client.AddRequestsAsync(requests).ConfigureAwait(false);
            await client.SendAsync().ConfigureAwait(false);
        }

        [Benchmark]
        public async Task OldDicomClient_SendEchos()
        {
            var client = new Dicom.Network.DicomClient();
            client.NegotiateAsyncOps(1, 1);
            client.Linger = 0;

            var requests = Enumerable.Range(0, 1000).Select(i => new DicomCEchoRequest());

            foreach (var request in requests)
                client.AddRequest(request);

            await client.SendAsync("127.0.0.1", _server.Port, false, "SCU", "ANY-SCP").ConfigureAwait(false);
        }
    }

    public class SimpleDicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
    {
        public SimpleDicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(DicomPresentationContextResult.Accept);
            }

            await SendAssociationAcceptAsync(association).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationReleaseRequestAsync()
        {
            await SendAssociationReleaseResponseAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }

    public static class Ports
    {
        private static readonly object Lock = new object();
        private static int _port = 11112;

        public static int GetNext()
        {
            lock (Lock)
            {
                return _port++;
            }
        }
    }
}
