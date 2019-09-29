// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dicom.Network
{
    public partial class DicomServiceCoreTest
    {
        #region Unit tests

        private class SimpleCMoveProvider : DicomServiceCore, IDicomCMoveProviderAsync, IDicomServiceProvider
        {
            public IAsyncEnumerable<DicomCMoveResponse> OnCMoveRequestAsync(DicomCMoveRequest request)
            {
                var x = UserState as CMoveResponder;
                return x.Handler(request);
            }

            public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
            {
                foreach (var pc in association.PresentationContexts)
                {
                    pc.AcceptTransferSyntaxes(DicomTransferSyntax.ImplicitVRLittleEndian, DicomTransferSyntax.ExplicitVRLittleEndian);
                }

                return SendAssociationAcceptAsync(association);
            }

            public Task OnReceiveAssociationReleaseRequestAsync()
            {
                return SendAssociationReleaseResponseAsync();
            }

            public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
            {
            }

            public void OnConnectionClosed(Exception exception)
            {
            }

            public SimpleCMoveProvider(INetworkStream stream, System.Text.Encoding encoding, Log.Logger log) : base(stream, encoding, log) { }
        }
        #endregion
    }
}
