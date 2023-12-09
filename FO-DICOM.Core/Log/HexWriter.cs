// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Text;

namespace FellowOakDicom.Log
{

    public class HexWriter
    {
        public HexWriter(byte[] data)
        {
            Data = data;
            Start = 0;
            End = Data.Length;
            Offset = 0;
        }

        /// <summary>Bytes to be displayed</summary>
        public byte[] Data { get; private set; }

        /// <summary>First byte to be displayed</summary>
        public int Start { get; set; }

        /// <summary>Last byte to be displayed</summary>
        public int End { get; set; }

        /// <summary>Offset to be added to the printed position</summary>
        public int Offset { get; set; }

        public static string ToString(byte[] data)
        {
            return new HexWriter(data).ToString();
        }

        public static string ToString(byte[] data, int start, int length)
        {
            var hw = new HexWriter(data)
            {
                Start = start,
                End = start + length
            };
            return hw.ToString();
        }

        public static string ToString(Stream stream, int start, int length)
        {
            byte[] data = new byte[length];
            stream.Position = start;
            stream.Read(data, 0, length);

            var hw = new HexWriter(data)
            {
                Start = 0,
                End = length,
                Offset = start
            };
            return hw.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int b = Start; b < End;)
            {
                b += WriteLine(sb, b);
            }

            return sb.ToString();
        }

        private int WriteLine(StringBuilder sb, int start)
        {
            int b = start;
            int end = start + (16 - ((Offset + b) % 16));
            int start_offset = start + Offset;

            sb.AppendFormat("{0:x8}  ", start_offset - (start_offset % 16));

            // blanks up to starting point
            for (int i = 0, c = ((Offset + b) % 16); i < c; i++)
            {
                if ((i % 16) == 8) sb.Append(' ');
                sb.Append("   ");
            }

            // write bytes
            for (; b < end && b < End; b++)
            {
                if (((Offset + b) % 16) == 8) sb.Append(' ');
                sb.AppendFormat("{0:x2} ", Data[b]);
            }

            // blanks to finish line past end point
            for (; b < end; b++)
            {
                if (((Offset + b) % 16) == 8) sb.Append(' ');
                sb.Append("   ");
            }

            sb.Append(' ');
            b = start;

            // blanks up to starting point
            for (int i = (start_offset % 16); i > 0; i--)
            {
                sb.Append(' ');
            }

            // convert bytes to characters
            for (; b < end && b < End; b++)
            {
                if (Data[b] < 32 || Data[b] > 128) sb.Append('.');
                else sb.Append((char)Data[b]);
            }

            // blanks past end point
            for (; b < end; b++)
            {
                sb.Append(' ');
            }

            sb.AppendLine();

            return b - start;
        }
    }
}
