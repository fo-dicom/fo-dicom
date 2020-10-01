﻿// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Log;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Dicom.Network
{
    internal class SimpleCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        private List<string> _storedFiles = new List<string>();

        private static readonly DicomTransferSyntax[] AcceptedTransferSyntaxes =
        {
            DicomTransferSyntax.ExplicitVRLittleEndian,
            DicomTransferSyntax.ExplicitVRBigEndian,
            DicomTransferSyntax.ImplicitVRLittleEndian
        };

        private static readonly DicomTransferSyntax[] AcceptedImageTransferSyntaxes =
        {
            // Lossless
            DicomTransferSyntax.JPEGLSLossless,
            DicomTransferSyntax.JPEG2000Lossless,
            DicomTransferSyntax.JPEGProcess14SV1,
            DicomTransferSyntax.JPEGProcess14,
            DicomTransferSyntax.RLELossless,

            // Lossy
            DicomTransferSyntax.JPEGLSNearLossless,
            DicomTransferSyntax.JPEG2000Lossy,
            DicomTransferSyntax.JPEGProcess1,
            DicomTransferSyntax.JPEGProcess2_4,

            // Uncompressed
            DicomTransferSyntax.ExplicitVRLittleEndian,
            DicomTransferSyntax.ExplicitVRBigEndian,
            DicomTransferSyntax.ImplicitVRLittleEndian
        };

        public SimpleCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.Verification) pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
                else if (pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None) pc.AcceptTransferSyntaxes(AcceptedImageTransferSyntaxes);
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
            foreach(var tmpFile in _storedFiles)
            {
                File.Delete(tmpFile);
            }
            _storedFiles.Clear();
        }

        public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
        {
            var tempName = Path.GetTempFileName();
            Logger.Info(tempName);

            // as the incoming file could be not-valid
            request.File.Dataset.ValidateItems = false;
            request.File.FileMetaInfo.ValidateItems = false;

            request.File.Save(tempName);

            _storedFiles.Add(tempName);

            return new DicomCStoreResponse(request, DicomStatus.Success)
            {
                Dataset = request.Dataset
            };
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
        {
        }
    }
}
