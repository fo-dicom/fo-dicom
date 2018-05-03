// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render
{
    /// <summary>
    /// Pipeline interface
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        /// Get the LUT of the pipeline 
        /// </summary>
        ILUT LUT { get; }
    }
}
