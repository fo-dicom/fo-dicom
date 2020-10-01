﻿// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Log;
using FellowOakDicom.Network;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FellowOakDicom.Imaging.Codec;
using Xunit;
using System.Collections.Generic;

namespace FellowOakDicom.Tests.Bugs
{

    [Collection("General")]
    internal class VideoCStoreProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        private readonly List<string> _storedFiles = new List<string>();

        private static readonly DicomTransferSyntax[] _acceptedVideoTransferSyntaxes =
        {
            DicomTransferSyntax.MPEG2,
            DicomTransferSyntax.Lookup(DicomUID.MPEG4AVCH264HighProfileLevel41),
            DicomTransferSyntax.ImplicitVRLittleEndian
        };

        public VideoCStoreProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log, ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
            : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
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
            Logger.Info(tempName);
            await request.File.SaveAsync(tempName);

            _storedFiles.Add(tempName);

            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e)
            => Task.CompletedTask;

    }
}
