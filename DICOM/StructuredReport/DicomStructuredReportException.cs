﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.StructuredReport
{
    public class DicomStructuredReportException : DicomException
    {
        public DicomStructuredReportException(string message)
            : base(message)
        {
        }

        public DicomStructuredReportException(string format, params object[] args)
            : base(format, args)
        {
        }

        public DicomStructuredReportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
