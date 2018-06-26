﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    /// <summary>
    /// Structure of DICOM file
    /// </summary>
    public enum DicomFileFormat
    {
        /// <summary>Parser was unable to determine structure of file. Possibly not DICOM.</summary>
        Unknown,

        /// <summary>Valid DICOM file containing preamble and file meta info.</summary>
        DICOM3,

        /// <summary>DICOM file that does not contain preamble but does contain file meta info.</summary>
        DICOM3NoPreamble,

        /// <summary>DICOM file that does not contain preamble or file meta info.</summary>
        DICOM3NoFileMetaInfo,

        /// <summary>ACR-NEMA 1.0</summary>
        ACRNEMA1,

        /// <summary>ACR-NEMA 2.0</summary>
        ACRNEMA2
    }
}
