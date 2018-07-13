// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO
{
    public class DicomIoException : DicomException
    {
        public DicomIoException(string message)
            : base(message)
        {
        }

        public DicomIoException(string format, params object[] args)
            : base(format, args)
        {
        }

        public DicomIoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
