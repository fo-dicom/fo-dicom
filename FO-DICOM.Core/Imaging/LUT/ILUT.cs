// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Interface for Lookup table definition
    /// </summary>
    public interface ILUT
    {
        /// <summary>
        /// Returns true if the lookup table is valid
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Get the minimum output value
        /// </summary>
        double MinimumOutputValue { get; }

        /// <summary>
        /// Get the maximum output value
        /// </summary>
        double MaximumOutputValue { get; }

        /// <summary>
        /// Indexer to transform input value into output value
        /// </summary>
        /// <param name="input">Input value</param>
        /// <returns>Output value</returns>
        double this[double input] { get; }

        /// <summary>
        /// Forces the recalculation of LUT
        /// </summary>
        void Recalculate();
    }
}
