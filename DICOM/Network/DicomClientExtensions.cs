﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;

    public static class DicomClientExtensions
    {
        [Obsolete]
        public static IAsyncResult BeginSend(
            this DicomClient @this,
            string host,
            int port,
            bool useTls,
            string callingAe,
            string calledAe,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(@this.SendAsync(host, port, useTls, callingAe, calledAe), callback, state);
        }

        [Obsolete]
        public static IAsyncResult BeginSend(
            this DicomClient @this,
            Stream stream,
            string callingAe,
            string calledAe,
            AsyncCallback callback,
            object state)
        {
            return AsyncFactory.ToBegin(@this.SendAsync(stream, callingAe, calledAe), callback, state);
        }

        [Obsolete]
        public static void EndSend(this DicomClient @this, IAsyncResult result)
        {
            AsyncFactory.ToEnd(result);
        }
    }
}