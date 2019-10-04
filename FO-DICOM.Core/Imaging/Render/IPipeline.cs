// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.LUT;

namespace FellowOakDicom.Imaging.Render
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
