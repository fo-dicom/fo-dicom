// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Reader
{
    public class DicomReaderException : DicomException
    {
        public DicomReaderException(string message)
            : base(message)
        {
        }

        public DicomReaderException(string format, params object[] args)
            : base(format, args)
        {
        }

        public DicomReaderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
