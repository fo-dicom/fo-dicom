// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

#if !NET35
using System.Threading.Tasks;
#endif

namespace FellowOakDicom.Network
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
        /// <returns>Awaitable task maintaining the long-running operation(s).</returns>
        Task RunAsync();
#endif
    }
}
