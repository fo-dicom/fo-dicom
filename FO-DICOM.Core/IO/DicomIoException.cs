// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.IO
{

    public class DicomIoException : DicomException
    {

        public DicomIoException(string message)
            : base(message)
        {
        }

        public DicomIoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
