// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    /// <summary>
    /// Interface for default classification of manager types.
    /// </summary>
    internal interface IClassifiedManager
    {
        /// <summary>
        /// Gets whether or not this type is classified as a default manager.
        /// </summary>
        bool IsDefault { get; }
    }
}
