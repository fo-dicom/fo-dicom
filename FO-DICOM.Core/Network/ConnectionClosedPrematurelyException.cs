// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    public class ConnectionClosedPrematurelyException : DicomNetworkException
    {
        public ConnectionClosedPrematurelyException(Exception innerException) : base("The connection closed prematurely", innerException) { }

        public ConnectionClosedPrematurelyException()
            : base("The connection closed prematurely")
        {
        }
    }
}
