// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom
{
    public class DicomFileException : DicomDataException
    {
        public DicomFileException(DicomFile file, string message)
            : base(message)
        {
            File = file;
        }

        public DicomFileException(DicomFile file, string format, params object[] args)
            : base(format, args)
        {
            File = file;
        }

        public DicomFileException(DicomFile file, string message, Exception innerException)
            : base(message, innerException)
        {
            File = file;
        }

        public DicomFile File { get; private set; }
    }
}
