// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public partial class DicomFile
    {
        [Obsolete]
        public static IAsyncResult BeginOpen(string fileName, AsyncCallback callback, object state)
        {
            return BeginOpen(fileName, DicomEncoding.Default, callback, state);
        }

        [Obsolete]
        public static IAsyncResult BeginOpen(
            string fileName,
            Encoding fallbackEncoding,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(Task.Run(() => Open(fileName, fallbackEncoding)), callback, state);
        }

        [Obsolete]
        public static DicomFile EndOpen(IAsyncResult result)
        {
            return AsyncFactory.ToEnd<DicomFile>(result);
        }
    }
}
