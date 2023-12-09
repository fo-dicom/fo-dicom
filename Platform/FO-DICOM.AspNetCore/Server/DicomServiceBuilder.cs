// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Network;
using System;
using System.Threading.Tasks;

namespace FellowOakDicom.AspNetCore.Server
{
    public class DicomServiceBuilder
    {
        internal Func<DicomCEchoRequest, DicomCEchoResponse> EchoHandler { get; set; } = null;
        internal Func<DicomAssociation, bool> AssociationRequestHandler { get; set; } = null;
        internal Func<InstanceReceivedEventArgs, Task<bool>> InstanceReceivedHandlerAsync { get; set; } = null;

        internal DicomServiceBuilder()
        {
        }


        /// <summary>
        /// Sets a custom handler to be executed on AssociationRequest
        /// </summary>
        public DicomServiceBuilder OnAssociationRequest(Func<DicomAssociation, bool> associationRequestHandler)
        {
            AssociationRequestHandler = associationRequestHandler;
            return this;
        }

        /// <summary>
        /// AssociationRequests are only accepted if the called AETitle is the same as the one provided
        /// </summary>
        public DicomServiceBuilder CheckAssociationForCalledAET(string calledAET)
        {
            AssociationRequestHandler = association => association.CalledAE == calledAET;
            return this;
        }

        public DicomServiceBuilder OnInstanceReceived(Func<InstanceReceivedEventArgs, Task<bool>> p)
        {
            InstanceReceivedHandlerAsync = p;
            return this;
        }

        /// <summary>
        /// All DicomEchoRequests are answered with Success
        /// </summary>
        public DicomServiceBuilder AnswerDicomEcho()
        {
            EchoHandler = request => new DicomCEchoResponse(request, DicomStatus.Success);
            return this;
        }

        /// <summary>
        /// Sets a custom handler to be executed on EchoRequest
        /// </summary>
        public DicomServiceBuilder OnEcho(Func<DicomCEchoRequest, DicomCEchoResponse> echoHandler)
        {
            EchoHandler = echoHandler;
            return this;
        }

    }
}
