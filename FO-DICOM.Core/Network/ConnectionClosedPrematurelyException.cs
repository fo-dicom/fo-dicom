// Copyright (c) 2012-2022 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
