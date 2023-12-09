// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    public class DicomFileException : DicomDataException
    {
        public DicomFileException(DicomFile file, string message)
            : base(message)
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
