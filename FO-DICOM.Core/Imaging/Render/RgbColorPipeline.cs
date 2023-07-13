﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.Imaging.LUT;

namespace FellowOakDicom.Imaging.Render
{

    /// <summary>
    /// RGB color pipeline implementation of <see cref="IPipeline"/> interface
    /// </summary>
    public class RgbColorPipeline : IPipeline
    {
        /// <inheritdoc />
        public ILUT LUT => null;
    }
}
