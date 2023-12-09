// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Threading.Tasks;

namespace FellowOakDicom.Network
{

    /// <summary>
    /// Interface for initializing long-running operations in a DICOM service.
    /// </summary>
    public interface IDicomServiceRunner
    {
        /// <summary>
        /// Setup long-running operations that the DICOM service manages.
        /// </summary>
        /// <returns>Awaitable task maintaining the long-running operation(s).</returns>
        Task RunAsync();
    }
}
