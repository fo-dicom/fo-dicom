// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;

    public interface INetworkStream : IDisposable
    {
        Stream AsStream();
    }
}
