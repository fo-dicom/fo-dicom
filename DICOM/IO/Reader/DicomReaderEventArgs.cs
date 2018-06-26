﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using Dicom.IO.Buffer;

namespace Dicom.IO.Reader
{
    public class DicomReaderEventArgs : EventArgs
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
