// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;

    public sealed class DesktopNetworkStream : INetworkStream
    {
        #region FIELDS

        private bool disposed = false;

        #endregion

        #region CONSTRUCTORS

        ~DesktopNetworkStream()
        {
            this.Dispose(false);
        }

        #endregion

        #region METHODS

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        public Stream AsStream()
        {
            throw new System.NotImplementedException();
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed) return;
        }

        #endregion
    }
}
