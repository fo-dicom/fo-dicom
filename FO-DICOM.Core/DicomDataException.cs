// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    public class DicomDataException : DicomException
    {
        public DicomTag Tag { get; }

        public DicomDataException(string message, DicomTag tag = null)
            : base(message)
        {
            Tag = tag;
        }

        public DicomDataException(string message, Exception innerException, DicomTag tag = null)
            : base(message, innerException)
        {
            Tag = tag;
        }
    }
}
