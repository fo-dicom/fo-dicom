// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.StructuredReport
{
    public class DicomStructuredReportException : DicomException
    {
        public DicomStructuredReportException(string message)
            : base(message)
        {
        }

        public DicomStructuredReportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
