using System;
using System.Collections.Generic;
using System.Text;

namespace FellowOakDicom.Imaging
{
    public interface IImageManager
    {

        IImage CreateImage(int width, int height);

    }
}
