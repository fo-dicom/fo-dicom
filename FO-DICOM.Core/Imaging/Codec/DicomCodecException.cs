// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Imaging.Codec
{

    public class DicomCodecException : DicomException
    {
        public DicomCodecException(string message)
            : base(message)
        {
        }

        public DicomCodecException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
