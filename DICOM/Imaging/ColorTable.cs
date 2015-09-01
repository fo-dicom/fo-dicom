// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.IO;

namespace Dicom.Imaging
{
    using Dicom.IO;

    public static class ColorTable
    {
        public static readonly Color32[] Monochrome1 = InitGrayscaleLUT(true);

        public static readonly Color32[] Monochrome2 = InitGrayscaleLUT(false);

        private static Color32[] InitGrayscaleLUT(bool reverse)
        {
            Color32[] LUT = new Color32[256];
            int i;
            byte b;
            if (reverse)
            {
                for (i = 0, b = 255; i < 256; i++, b--)
                {
                    LUT[i] = new Color32(0xff, b, b, b);
                }
            }
            else
            {
                for (i = 0, b = 0; i < 256; i++, b++)
                {
                    LUT[i] = new Color32(0xff, b, b, b);
                }
            }
            return LUT;
        }

        public static Color32[] Reverse(Color32[] lut)
        {
            Color32[] clone = new Color32[lut.Length];
            Array.Copy(lut, clone, clone.Length);
            Array.Reverse(clone);
            return clone;
        }

        public static Color32[] LoadLUT(string file)
        {
            try
            {
                byte[] data = File.ReadAllBytes(file);
                if (data.Length != (256 * 3)) return null;

                Color32[] LUT = new Color32[256];
                for (int i = 0; i < 256; i++)
                {
                    LUT[i] = new Color32(0xff, data[i], data[i + 256], data[i + 512]);
                }
                return LUT;
            }
            catch
            {
                return null;
            }
        }

        public static void SaveLUT(string path, Color32[] lut)
        {
            if (lut.Length != 256) return;

            var file = IOManager.CreateFileReference(path);
            file.Delete();

            using (var fs = file.OpenWrite())
            {
                for (var i = 0; i < 256; i++) fs.WriteByte(lut[i].R);
                for (var i = 0; i < 256; i++) fs.WriteByte(lut[i].G);
                for (var i = 0; i < 256; i++) fs.WriteByte(lut[i].B);
            }
        }
    }
}

