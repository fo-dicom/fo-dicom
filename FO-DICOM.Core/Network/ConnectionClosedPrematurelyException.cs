// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Network
{
    public class ConnectionClosedPrematurelyException : DicomNetworkException
    {
        public ConnectionClosedPrematurelyException()
            : base("The connection closed prematurely")
        {
        }
    }
}