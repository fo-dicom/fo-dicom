// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.IO.Reader
{
    public enum DicomReaderResult
    {
        Processing,

        Success,

        Error,

        Stopped,

        Suspended
    }

    public interface IDicomReader
    {
        bool IsExplicitVR { get; set; }

        DicomReaderResult Status { get; }

        DicomReaderResult Read(IByteSource source, IDicomReaderObserver observer, DicomTag stop = null);

        IAsyncResult BeginRead(
            IByteSource source,
            IDicomReaderObserver observer,
            DicomTag stop,
            AsyncCallback callback,
            object state);

        DicomReaderResult EndRead(IAsyncResult result);
    }
}
