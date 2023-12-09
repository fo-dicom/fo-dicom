// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.IO;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Interface representing a network stream.
    /// </summary>
    public interface INetworkStream : IDisposable
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the remote host of the network stream.
        /// </summary>
        string RemoteHost { get; }

        /// <summary>
        /// Gets the local host of the network stream.
        /// </summary>
        string LocalHost { get; }

        /// <summary>
        /// Gets the remote port of the network stream.
        /// </summary>
        int RemotePort { get; }

        /// <summary>
        /// Gets the local port of the network stream.
        /// </summary>
        int LocalPort { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Get corresponding <see cref="System.IO.Stream"/> object.
        /// </summary>
        /// <returns>Network stream as <see cref="System.IO.Stream"/> object.</returns>
        Stream AsStream();

        #endregion
    }
}
