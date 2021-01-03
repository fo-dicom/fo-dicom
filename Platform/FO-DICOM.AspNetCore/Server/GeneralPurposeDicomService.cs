// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.AspNetCore.Server
{
    internal class GeneralPurposeDicomService : DicomService, IDicomServiceProvider, IDicomCEchoProvider
    {

        public GeneralPurposeDicomService(INetworkStream stream, Encoding fallbackEncoding, Logger log, ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
           : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
        {
        }


        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            var builder = UserState as DicomServiceBuilder;
            if (builder?.EchoHandler == null)
            {
                association.PresentationContexts.Where(pc => pc.AbstractSyntax == DicomUID.Verification).Each(pc => pc.SetResult(DicomPresentationContextResult.RejectAbstractSyntaxNotSupported));
            }
            else
            {
                association.PresentationContexts.Where(pc => pc.AbstractSyntax == DicomUID.Verification).Each(pc => pc.SetResult(DicomPresentationContextResult.Accept));
            }

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

    }
}
