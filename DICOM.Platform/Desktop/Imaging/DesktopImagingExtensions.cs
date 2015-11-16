using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Dicom.Imaging
{
    public static class DesktopImagingExtensions
    {
        /// <summary>
        /// Convenience method to access WinForms IImage instance as as WinForms Bitmap
        /// The returned Bitmap will be disposed when the IImage is disposed
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap AsBitmap(this IImage image)
        {
            return image.As<Bitmap>();
        }

        /// <summary>
        /// Convenience method to access WinForms IImage instance as as WinForms Bitmap
        /// The caller is responsible for disposal of the returned Bitmap
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap AsBitmapCopy(this IImage image)
        {
            return (Bitmap)image.AsBitmap().Clone();
        }

        /// <summary>
        /// Convenience method to access WPF IImage instance as a WPF WriteableBitmap
        /// The WriteableBitmap will be disposed when the IImage is disposed
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static WriteableBitmap AsWriteableBitmap(this IImage image)
        {
            return image.As<WriteableBitmap>();
        }

        /// <summary>
        /// Convenience method to access WPF IImage instance as a WPF WriteableBitmap
        /// The caller is responsible for disposal of the returned WriteableBitmap
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static WriteableBitmap AsWriteableBitmapCopy(this IImage image)
        {
            return image.AsWriteableBitmap().Clone();
        }
    }
}
