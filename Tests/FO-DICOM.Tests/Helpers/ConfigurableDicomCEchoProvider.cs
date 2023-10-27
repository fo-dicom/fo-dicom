// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Network;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Tests.Helpers
{
    public class ConfigurableDicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
    {
        private readonly Func<DicomAssociation, Task<bool>> _onAssociationRequest;
        private readonly Func<DicomCEchoRequest, Task> _onRequest;

        public ConfigurableDicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
            DicomServiceDependencies dicomServiceDependencies, Func<DicomAssociation, Task<bool>> onAssociationRequest,
            Func<DicomCEchoRequest, Task> onRequest)
            : base(stream, fallbackEncoding, log, dicomServiceDependencies)
        {
            _onAssociationRequest =
                onAssociationRequest ?? throw new ArgumentNullException(nameof(onAssociationRequest));
            _onRequest = onRequest ?? throw new ArgumentNullException(nameof(onRequest));
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            var accept = await _onAssociationRequest(association);

            foreach (var pc in association.PresentationContexts)
            {
                pc.SetResult(accept
                    ? DicomPresentationContextResult.Accept
                    : DicomPresentationContextResult.RejectNoReason);
            }

            if (accept)
            {
                await SendAssociationAcceptAsync(association).ConfigureAwait(false);
            }
            else
            {
                await SendAssociationRejectAsync(DicomRejectResult.Transient, DicomRejectSource.ServiceUser,
                    DicomRejectReason.NoReasonGiven).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task OnReceiveAssociationReleaseRequestAsync()
        {
            await SendAssociationReleaseResponseAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) { }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception) { }

        public async Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
        {
            await _onRequest(request);
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }
}
