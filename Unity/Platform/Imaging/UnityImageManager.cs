// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Imaging
{
    public class UnityImageManager : ImageManager
    {
        protected override IImage CreateImageImpl(int width, int height)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsDefault
        {
            get
            {
                return true;
            }
        }
    }
}