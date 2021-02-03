// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Dicom.Log;
using Dicom.Network;

namespace Dicom.Bugs
{
    internal class VideoCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        private List<string> _storedFiles = new List<string>();

        private static readonly DicomTransferSyntax[] AcceptedVideoTransferSyntaxes =
        {
            DicomTransferSyntax.MPEG2,
            DicomTransferSyntax.Lookup(DicomUID.MPEG4HP41),
            DicomTransferSyntax.ImplicitVRLittleEndian
        };

        public VideoCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.AcceptTransferSyntaxes(AcceptedVideoTransferSyntaxes);
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
            foreach(var tempFile in _storedFiles)
            {
                File.Delete(tempFile);
            }
            _storedFiles.Clear();
        }

        public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
        {
            var tempName = Path.GetTempFileName();
            Logger.Info(tempName);
            request.File.Save(tempName);

            _storedFiles.Add(tempName);

            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
        {
        }
    }
}
