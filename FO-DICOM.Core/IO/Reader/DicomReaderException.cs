// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.IO.Reader
{

    public class DicomReaderException : DicomException
    {

        public DicomReaderException(string message)
            : base(message)
        {
        }

        public DicomReaderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
