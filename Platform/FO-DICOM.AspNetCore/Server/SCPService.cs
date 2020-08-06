// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Log;
using FellowOakDicom.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FellowOakDicom.AspNetCore.Server
{
    internal class SCPService : DicomService, IDicomServiceProvider
    {

        public SCPService(INetworkStream stream, Encoding fallbackEncoding, Logger log, ILogManager logManager, INetworkManager networkManager, ITranscoderManager transcoderManager)
            : base(stream, fallbackEncoding, log, logManager, networkManager, transcoderManager)
        {
        }

        public void OnConnectionClosed(Exception exception) => throw new NotImplementedException();
        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) => throw new NotImplementedException();
        public Task OnReceiveAssociationReleaseRequestAsync() => throw new NotImplementedException();
        public Task OnReceiveAssociationRequestAsync(DicomAssociation association) => throw new NotImplementedException();
    }
}
