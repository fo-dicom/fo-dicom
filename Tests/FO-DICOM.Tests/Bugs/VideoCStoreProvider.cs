// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Imaging.Codec;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection(TestCollections.General)]
    internal class VideoCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        private readonly List<string> _storedFiles = new List<string>();

        private static readonly DicomTransferSyntax[] _acceptedVideoTransferSyntaxes =
        {
            DicomTransferSyntax.MPEG2,
            DicomTransferSyntax.Lookup(DicomUID.MPEG4HP41),
            DicomTransferSyntax.ImplicitVRLittleEndian
        };

        public VideoCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, log, dependencies)
        {
        }

        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach (var pc in association.PresentationContexts)
            {
                pc.AcceptTransferSyntaxes(_acceptedVideoTransferSyntaxes);
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
            _storedFiles.ForEach(file => File.Delete(file));
            _storedFiles.Clear();
        }

        public async Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
        {
            var tempName = Path.GetTempFileName();
            Logger.LogInformation(tempName);
            await request.File.SaveAsync(tempName);

            _storedFiles.Add(tempName);

            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
            => Task.CompletedTask;

    }
}
