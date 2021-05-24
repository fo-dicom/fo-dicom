/*
// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.States
{

    public abstract class DicomClientWithConnectionState : IDicomClientState
    {
        protected DicomClientWithConnectionState(IInitialisationWithConnectionParameters parameters)
        {
            Connection = parameters.Connection ?? throw new ArgumentNullException(nameof(IInitialisationWithConnectionParameters.Connection));
        }

        public IDicomClientConnection Connection { get; }
    }
}
*/
