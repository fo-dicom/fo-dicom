// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network.Client.States
{

    public interface IInitialisationWithConnectionParameters
    {
        IDicomClientConnection Connection { get; set; }
    }
}
