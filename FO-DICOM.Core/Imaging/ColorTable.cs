// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using FellowOakDicom.IO;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom.Imaging
{

    /// <summary>
    /// Convenience class for managing color look-up tables with 256 color items.
    /// </summary>
    public static class ColorTable
    {
        /// <summary>
        /// Look-up table representing MONOCHROME1 grayscale scheme.
        /// </summary>
        public static readonly Color32[] Monochrome1 = InitGrayscaleLUT(true);

        /// <summary>
        /// Look-up table representing MONOCHROME2 grayscale scheme.
        /// </summary>
        public static readonly Color32[] Monochrome2 = InitGrayscaleLUT(false);

        /// <summary>
        /// Create a 256-item grayscale look-up table.
        /// </summary>
        /// <param name="reverse">Indicates whether grayscales should be sorted in ascending (false) or descending (true) order.</param>
        /// <returns>Grayscale look-up table.</returns>
        private static Color32[] InitGrayscaleLUT(bool reverse)
        {
            var lut = new Color32[256];
            int i;
            byte b;
            if (reverse)
            {
                for (i = 0, b = 255; i < 256; i++, b--)
                {
                    lut[i] = new Color32(0xff, b, b, b);
                }
            }
            else
            {
                for (i = 0, b = 0; i < 256; i++, b++)
                {
                    lut[i] = new Color32(0xff, b, b, b);
                }
            }
            return lut;
        }

        /// <summary>
        /// Returns the reverse of an existing color table.
        /// </summary>
        /// <param name="lut">Look-up table subject to reversal.</param>
        /// <returns>Reversed look-up table.</returns>
        public static Color32[] Reverse(Color32[] lut)
        {
            var clone = new Color32[lut.Length];
            Array.Copy(lut, clone, clone.Length);
            Array.Reverse(clone);
            return clone;
        }

        /// <summary>
        /// Load color look-up table from file.
        /// </summary>
        /// <param name="path">File name.</param>
        /// <returns>Loaded color look-up table.</returns>
        public static Color32[] LoadLUT(string path)
        {
            try
            {
                byte[] data;
                var fileRef = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(path);
                using var stream = fileRef.OpenRead();
                var length = (int)stream.Length;
                if (length != (256 * 3))
                {
                    return null;
                }

                data = new byte[length];
                stream.Read(data, 0, length);

                var lut = new Color32[256];
                for (var i = 0; i < 256; i++)
                {
                    lut[i] = new Color32(0xff, data[i], data[i + 256], data[i + 512]);
                }

                return lut;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Save color look-up table to file.
        /// </summary>
        /// <param name="path">File name.</param>
        /// <param name="lut">Look-up table to save.</param>
        public static void SaveLUT(string path, Color32[] lut)
        {
            if (lut.Length != 256)
            {
                return;
            }

            var file = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(path);
            using var fs = file.Create();
            lut.Each(c => fs.WriteByte(c.R));
            lut.Each(c => fs.WriteByte(c.G));
            lut.Each(c => fs.WriteByte(c.B));
        }
    }
}

