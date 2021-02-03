// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
