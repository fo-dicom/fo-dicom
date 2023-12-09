// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom.IO.Reader
{

    internal class DicomReaderEventArgs : EventArgs
    {

        public readonly long Position;

        public readonly DicomTag Tag;

        public readonly DicomVR VR;

        public readonly IByteBuffer Data;

        public DicomReaderEventArgs(long position, DicomTag tag, DicomVR vr, IByteBuffer data)
        {
            Position = position;
            Tag = tag;
            VR = vr;
            Data = data;
        }
    }
}
