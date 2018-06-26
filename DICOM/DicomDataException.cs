// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
{
    public class DicomDataException : DicomException
    {
        public DicomDataException(string message)
            : base(message)
        {
        }

        public DicomDataException(string format, params object[] args)
            : base(format, args)
        {
        }

        public DicomDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
