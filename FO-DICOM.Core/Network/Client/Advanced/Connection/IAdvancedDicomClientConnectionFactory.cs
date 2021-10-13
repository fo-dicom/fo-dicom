// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading;
using System.Threading.Tasks;

namespace FellowOakDicom.Network.Client.Advanced.Connection
{
    /// <summary>
    /// The factory that is responsible for creating new DICOM connections
    /// </summary>
    public interface IAdvancedDicomClientConnectionFactory
    {
        /// <summary>
        /// Opens a new TCP connection and starts a DICOM listener
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IAdvancedDicomClientConnection> ConnectAsync(AdvancedDicomClientConnectionRequest request, CancellationToken cancellationToken);
    }
}