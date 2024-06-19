// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace FellowOakDicom.Benchmark
{
    public class NopCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        public NopCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        public void OnConnectionClosed(Exception exception)
        {
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association, CancellationToken cancellationToken)
        {
            foreach (var presentationContext in association.PresentationContexts)
            {
                foreach (var ts in presentationContext.GetTransferSyntaxes())
                {
                    presentationContext.SetResult(DicomPresentationContextResult.Accept, ts);
                    break;
                }
            }

            return SendAssociationAcceptAsync(association);
        }

        public Task OnReceiveAssociationReleaseRequestAsync(CancellationToken cancellationToken)
        {
            return SendAssociationReleaseResponseAsync();
        }

        public Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request, CancellationToken cancellationToken)
            => Task.FromResult(new DicomCStoreResponse(request, DicomStatus.Success));

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e, CancellationToken cancellationToken)
            => Task.CompletedTask;

    }
}
