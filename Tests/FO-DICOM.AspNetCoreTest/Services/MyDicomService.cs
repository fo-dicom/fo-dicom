// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FO_DICOM.AspNetCoreTest.Services
{
    public class MyDicomService : DicomService, IDicomServiceProvider
    {

        public MyDicomService(INetworkStream stream, Encoding fallbackEncoding, ILogger logger, DicomServiceDependencies dependencies)
            : base(stream, fallbackEncoding, logger, dependencies)
        {
        }


        public Task OnReceiveAssociationRequestAsync(DicomAssociation association)
        {
            foreach(var context in association.PresentationContexts)
            {
                context.SetResult(DicomPresentationContextResult.Accept, context.GetTransferSyntaxes().First());
            }
            return SendAssociationAcceptAsync(association);
        }


        public Task OnReceiveAssociationReleaseRequestAsync()
            => SendAssociationReleaseResponseAsync();


        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        { }

        public void OnConnectionClosed(Exception exception)
        { }

    }
}
