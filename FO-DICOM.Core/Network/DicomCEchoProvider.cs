// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Implementation of a C-ECHO Service Class Provider.
    /// </summary>
    public class DicomCEchoProvider : DicomService, IDicomServiceProvider, IDicomCEchoProvider
    {
        /// <summary>
        /// Initializes an instance of the <see cref="DicomCEchoProvider"/> class.
        /// </summary>
        /// <param name="stream">Network stream on which DICOM communication is establshed.</param>
        /// <param name="fallbackEncoding">Text encoding if not specified within messaging.</param>
        /// <param name="log">DICOM logger.</param>
        /// <param name="logManager">The log manager</param>
        /// <param name="networkManager">The network manager</param>
        /// <param name="transcoderManager">The transcoder manager</param>
        public DicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log,
            DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        /// <inheritdoc />
        public virtual Task OnReceiveAssociationRequestAsync(DicomAssociation association)
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
        public virtual Task OnReceiveAssociationReleaseRequestAsync()
            => SendAssociationReleaseResponseAsync();

        /// <inheritdoc />
        public virtual void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        /// <inheritdoc />
        public virtual void OnConnectionClosed(Exception exception)
        {
        }

        /// <summary>
        /// Event handler for C-ECHO request.
        /// </summary>
        /// <param name="request">C-ECHO request.</param>
        /// <returns>C-ECHO response with Success status.</returns>
        public virtual Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
            => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
    }
}
