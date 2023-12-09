// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Network
{
    public class DicomNetworkException : DicomException
    {
        public DicomNetworkException(string message)
            : base(message)
        {
        }

        public DicomNetworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
