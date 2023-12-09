// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;

namespace FellowOakDicom.Imaging.Codec.JpegLossless
{

    internal class ScanHeader
    {

        internal ScanComponent[] components;

        public int Ah { get; private set; }

        public int Al { get; private set; }

        public int NumComponents { get; private set; }

        public int Selection { get; private set; }

        public int SpectralEnd { get; private set; }


        internal int Read(IDataStream data)
        {
            int count = 0;
            int length = data.Get16();
            count += 2;

            NumComponents = data.Get8();
            count++;

            components = new ScanComponent[NumComponents];

            for (int i = 0; i < NumComponents; i++)
            {
                components[i] = new ScanComponent();

                if (count > length)
                {
                    throw new IOException("ERROR: scan header format error");
                }

                components[i].ScanCompSel = data.Get8();
                count++;

                int tempval = data.Get8();
                count++;

                components[i].DcTabSel = tempval >> 4;
                components[i].AcTabSel = tempval & 0x0F;
            }

            Selection = data.Get8();
            count++;

            SpectralEnd = data.Get8();
            count++;

            int temp = data.Get8();
            Ah = temp >> 4;
            Al = temp & 0x0F;
            count++;

            if (count != length)
            {
                throw new IOException("ERROR: scan header format error [count!=Ns]");
            }

            return 1;
        }


    }
}
