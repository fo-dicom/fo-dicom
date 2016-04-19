// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

using Dicom.Imaging.Render;
using Dicom.IO;

using UnityEngine;

namespace Dicom.Imaging
{
    public class UnityImage : ImageBase<Texture2D>
    {
        public UnityImage(int width, int height, PinnedIntArray pixels, Texture2D image)
            : base(width, height, pixels, image)
        {
        }

        public override void Render(int components, bool flipX, bool flipY, int rotation)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawGraphics(IEnumerable<IGraphic> graphics)
        {
            throw new System.NotImplementedException();
        }

        public override IImage Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}