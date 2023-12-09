// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using System.Linq;

namespace FellowOakDicom.Imaging.Reconstruction
{

    /// <summary>
    /// Represents a calculated cut throgh a volume.
    /// </summary>
    public class Slice
    {

        private readonly VolumeData _volume;


        public Slice(VolumeData volume, Point3D topLeft, Vector3D rowDir, Vector3D colDir, int rows, int cols, double spacing)
        {
            _volume = volume;
            TopLeft = topLeft.Clone();
            RowDirection = rowDir.Clone();
            ColumnDirection = colDir.Clone();
            Rows = rows;
            Columns = cols;
            Spacing = spacing;
            CalculateCut();
        }


        public Point3D TopLeft { get; private set; }

        public Vector3D RowDirection { get; private set; }

        public Vector3D ColumnDirection { get; private set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public double Spacing { get; private set; }

        private double[] _output;


        private void CalculateCut()
        {
            _output = _volume.GetCut(TopLeft, RowDirection, ColumnDirection, Rows, Columns, Spacing);
        }


        public void RenderIntoByteArray(byte[] data, int stride)
        {
            var lut = _volume.Lut;

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    var pixel = _output[x + y * Columns];
                    var output = lut[pixel];
                    var gray = (byte)(((int)output) & 0xFF);
                    data[x + y * stride] = gray;
                }
            }
        }


        internal byte[] RenderRawData(int bytesPerPixel)
        {
            var buffer = new byte[Rows * Columns * bytesPerPixel];

            if (bytesPerPixel == 1)
            {
                for (int i = 0; i < _output.Length; i++)
                {
                    var value = (byte)_output[i];
                    buffer[i] = value;
                }
            }
            else if (bytesPerPixel == 2)
            {
                for (int i = 0; i < _output.Length; i++)
                {
                    var value = (ushort)_output[i];
                    buffer[2 * i] = (byte)(value & 0x00ff);
                    buffer[2 * i + 1] = (byte)(value >> 8);
                }
            }

            return buffer;
        }


        public IntervalD GetMinMaxValue()
        {
            return new IntervalD(_output.Min(), _output.Max());
        }


    }
}
