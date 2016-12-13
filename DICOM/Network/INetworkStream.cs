// Copyright (c) 2012-2016 fo-dicom contributors.
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
        #region PROPERTIES

        /// <summary>
        /// Gets the host of the network stream.
        /// </summary>
        string Host { get; }
		string RemoteHost { get; }
        /// <summary>
        /// Gets the port of the network stream.
        /// </summary>
        int Port { get; }
		int RemotePort { get; }
        #endregion

        #region METHODS

        /// <summary>
        /// Get corresponding <see cref="Stream"/> object.
        /// </summary>
        /// <returns>Network stream as <see cref="Stream"/> object.</returns>
        Stream AsStream();

        #endregion
    }
}
