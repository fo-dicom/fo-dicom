// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;

#if !NET35
using System.Threading.Tasks;
#endif

using FellowOakDicom.Log;

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
        public DicomCEchoProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

#if !NET35
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
#endif

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
        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }
}
