// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Threading.Tasks;

namespace Dicom.Network
{
    /// <summary>
    /// Interface for initializing long-running operations in a DICOM service.
    /// </summary>
    public interface IDicomServiceInitialization
    {
        /// <summary>
        /// Setup long-running operations that the DICOM service manages.
        /// </summary>
        /// <returns>Awaitable task maintaining the long-running operation(s).</returns>
        Task InitializeAsync();
    }
}