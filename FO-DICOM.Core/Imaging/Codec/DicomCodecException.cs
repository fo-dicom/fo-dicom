// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace FellowOakDicom.Imaging.Codec
{

    public class DicomCodecException : DicomException
    {
        public DicomCodecException(string message)
            : base(message)
        {
        }

        public DicomCodecException(string format, params object[] args)
            : base(format, args)
        {
        }

        public DicomCodecException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
