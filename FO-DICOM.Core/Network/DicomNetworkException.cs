// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Network
{
    public class DicomNetworkException : DicomException
    {
        public DicomNetworkException(string message)
            : base(message)
        {
        }

        public DicomNetworkException(string format, params object[] args)
            : base(format, args)
        {
        }

        public DicomNetworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
