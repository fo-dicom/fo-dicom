// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;
using System.Threading.Tasks;

using Dicom.Log;

namespace Dicom.Network
{
    /// <summary>
    /// Implementation of an asynchronous C-ECHO Service Class Provider.
    /// </summary>
    public class AsyncDicomCEchoProvider : DicomService, IDicomServiceProvider, IAsyncDicomCEchoProvider
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomCEchoProvider"/> class.
        /// </summary>
        /// <param name="stream">Network stream on which DICOM communication is establshed.</param>
        /// <param name="fallbackEncoding">Text encoding if not specified within messaging.</param>
        /// <param name="log">DICOM logger.</param>
        public AsyncDicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
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

        /// <inheritdoc />
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        /// <inheritdoc />
        public void OnConnectionClosed(Exception exception)
        {
        }

        /// <summary>
        /// Event handler for C-ECHO request.
        /// </summary>
        /// <param name="request">C-ECHO request.</param>
        /// <returns>C-ECHO response with Success status.</returns>
        public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
        {
            return Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
        }
    }
}
