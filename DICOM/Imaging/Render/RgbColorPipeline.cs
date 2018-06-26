// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Dicom.Imaging.LUT;

namespace Dicom.Imaging.Render
{
    /// <summary>
    /// RGB color pipeline implementation of <seealso cref="IPipeline"/> interface
    /// </summary>
    public class RgbColorPipeline : IPipeline
    {
        /// <inheritdoc />
        public ILUT LUT => null;
    }
}
