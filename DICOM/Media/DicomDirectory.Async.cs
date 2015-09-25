// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Media
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    public partial class DicomDirectory
    {
        #region Save/Load Methods

        [Obsolete]
        public static new IAsyncResult BeginOpen(string fileName, AsyncCallback callback, object state)
        {
            return BeginOpen(fileName, DicomEncoding.Default, callback, state);
        }

        [Obsolete]
        public static new IAsyncResult BeginOpen(
            string fileName,
            Encoding fallbackEncoding,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(Task.Run(() => Open(fileName, fallbackEncoding)), callback, state);
        }

        [Obsolete]
        public static new DicomDirectory EndOpen(IAsyncResult result)
        {
            return AsyncFactory.ToEnd<DicomDirectory>(result);
        }

        #endregion
    }
}