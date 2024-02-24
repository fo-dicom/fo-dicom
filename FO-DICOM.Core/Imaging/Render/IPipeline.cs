// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

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


        /// <summary>
        /// Remove all cached data and only keep configuration data, to reduce memory consumption
        /// </summary>
        void ClearCache();
    }
}
