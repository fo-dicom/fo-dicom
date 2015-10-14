// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.IO;

    /// <summary>
    /// Interface representing a network stream.
    /// </summary>
    public interface INetworkStream : IDisposable
    {
        /// <summary>
        /// Get corresponding <see cref="Stream"/> object.
        /// </summary>
        /// <returns>Network stream as <see cref="Stream"/> object.</returns>
        Stream AsStream();
    }
}
