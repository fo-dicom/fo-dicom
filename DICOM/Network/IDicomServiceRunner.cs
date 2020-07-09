// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Dicom.Network
{
    /// <summary>
    /// Interface for initializing long-running operations in a DICOM service.
    /// </summary>
    public interface IDicomServiceRunner
    {
#if !NET35
        /// <summary>
        /// Setup long-running operations that the DICOM service manages.
        /// </summary>
        /// <param name="cancellationToken">The token that cancels this SCP</param>
        /// <returns>Awaitable task maintaining the long-running operation(s).</returns>
        Task RunAsync(CancellationToken cancellationToken = default(CancellationToken));
#endif
    }
}
