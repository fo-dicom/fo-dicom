// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom
{

    public class DicomDataException : DicomException
    {
        public DicomDataException(string message)
            : base(message)
        {
        }

        public DicomDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
