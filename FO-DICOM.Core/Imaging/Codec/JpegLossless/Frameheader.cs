// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;

namespace FellowOakDicom.Imaging.Codec.JpegLossless
{

    internal class FrameHeader
    {

        public ComponentSpec[] Components { get; private set; }

        public int DimX { get; private set; }

        public int DimY { get; private set; }

        public int NumComponents { get; private set; }


        public int Precision { get; private set; }


        internal int Read(IDataStream data)
        {
            int count = 0;

            int length = data.Get16();
            count += 2;

            Precision = data.Get8();
            count++;

            DimY = data.Get16();
            count += 2;

            DimX = data.Get16();
            count += 2;

            NumComponents = data.Get8();
            count++;

            //components = new ComponentSpec[numComp]; // some image exceed this range...
            Components = new ComponentSpec[256]; // setting to 256 -- not sure what it should be.

            for (int i = 1; i <= NumComponents; i++)
            {
                if (count > length)
                {
                    throw new IOException("ERROR: frame format error");
                }

                int c = data.Get8();
                count++;

                if (count >= length)
                {
                    throw new IOException("ERROR: frame format error [c>=Lf]");
                }

                int temp = data.Get8();
                count++;

                if (Components[c] == null)
                {
                    Components[c] = new ComponentSpec();
                }

                Components[c].hSamp = temp >> 4;
                Components[c].vSamp = temp & 0x0F;
                Components[c].quantTableSel = data.Get8();
                count++;
            }

            if (count != length)
            {
                throw new IOException("ERROR: frame format error [Lf!=count]");
            }

            return 1;
        }
    }
}
