// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.AspNetCore.Server
{
    internal class GeneralPurposeDicomService : DicomService, IDicomServiceProvider, IDicomCEchoProvider, IDicomCStoreProvider
    {

        private static readonly DicomTransferSyntax[] _uncompressedTransferSyntaxes =
        {
            DicomTransferSyntax.ExplicitVRLittleEndian,
            DicomTransferSyntax.ExplicitVRBigEndian,
            DicomTransferSyntax.ImplicitVRLittleEndian
        };

        private readonly DicomTransferSyntax[] _availableImageTransferSyntaxes;

        public GeneralPurposeDicomService(INetworkStream stream, Encoding fallbackEncoding, ILogger log, DicomServiceDependencies dependencies)
           : base(stream, fallbackEncoding, log, dependencies)
        {
            _availableImageTransferSyntaxes = DicomTransferSyntax.KnownEntries
                .Where(t => dependencies.TranscoderManager.CanTranscode(t, DicomTransferSyntax.ImplicitVRLittleEndian))
                .Union(_uncompressedTransferSyntaxes)
                .ToArray();
        }


        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            var builder = UserState as DicomServiceBuilder;

            association.PresentationContexts
                .Where(pc => pc.AbstractSyntax == DicomUID.Verification)
                .Each(pc => pc.SetResult(builder?.EchoHandler == null ? DicomPresentationContextResult.RejectAbstractSyntaxNotSupported : DicomPresentationContextResult.Accept));

            association.PresentationContexts
                .Where(pc => pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None)
                .Each(pc =>
                {
                    if (builder?.InstanceReceivedHandlerAsync == null)
                    {
                        pc.SetResult(DicomPresentationContextResult.RejectAbstractSyntaxNotSupported);
                    }
                    else
                    {
                        pc.AcceptTransferSyntaxes(_availableImageTransferSyntaxes);
                    }
                });

            if (builder?.AssociationRequestHandler != null && !builder.AssociationRequestHandler(association))
            {
                return SendAssociationRejectAsync(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser, DicomRejectReason.NoReasonGiven);
            }
            else
            {
                return SendAssociationAcceptAsync(association);
            }
        }


        public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request)
        {
            var builder = UserState as DicomServiceBuilder;
            if (builder?.EchoHandler == null)
            {
                return Task.FromResult(new DicomCEchoResponse(request, DicomStatus.SOPClassNotSupported));
            }
            else
            {
                return Task.FromResult(builder.EchoHandler(request));
            }
        }

        public void OnConnectionClosed(Exception exception)
        { }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        { }

        public Task OnReceiveAssociationReleaseRequestAsync()
           => SendAssociationReleaseResponseAsync();


        public async Task<DicomCStoreResponse> OnCStoreRequestAsync(DicomCStoreRequest request)
        {
            var builder = UserState as DicomServiceBuilder;
            var resultStatus = DicomStatus.Success;

            if (builder.InstanceReceivedHandlerAsync != null)
            {
                var instanceReceivedEventArgs = new InstanceReceivedEventArgs(Association, request.File);
                var result = await builder.InstanceReceivedHandlerAsync.Invoke(instanceReceivedEventArgs);
                if (!result)
                {
                    resultStatus = instanceReceivedEventArgs.ResultStatus;
                }
            }
            return new DicomCStoreResponse(request, resultStatus);
        }


        public Task OnCStoreRequestExceptionAsync(string tempFileName, Exception e) => throw new NotImplementedException();


    }
}
