// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Globalization;

namespace FellowOakDicom
{

    public sealed class DicomMaskedTag : IFormattable
    {
        public const uint FullMask = 0xffffffff;

        public DicomMaskedTag(DicomTag tag)
        {
            Tag = tag;
            Mask = FullMask;
        }

        private DicomMaskedTag()
        {
        }

        private DicomTag _tag = null;

        public DicomTag Tag
        {
            get => _tag ??= new DicomTag(Group, Element);
            set
            {
                _tag = value;
                Card = ((uint)Group << 16) | (uint)Element;
            }
        }

        public ushort Group => Tag.Group;

        public ushort Element => Tag.Element;

        public uint Card { get; private set; }

        public uint Mask { get; private set; }

        public override string ToString()
        {
            return ToString("G", null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider?.GetFormat(GetType()) is ICustomFormatter fmt)
            {
                return fmt.Format(format, this, formatProvider);
            }

            switch (format)
            {
                case "g":
                    {
                        string s = Group.ToString("x4");
                        string x = string.Empty;
                        x += ((Mask & 0xf0000000) != 0) ? s[0] : 'x';
                        x += ((Mask & 0x0f000000) != 0) ? s[1] : 'x';
                        x += ((Mask & 0x00f00000) != 0) ? s[2] : 'x';
                        x += ((Mask & 0x000f0000) != 0) ? s[3] : 'x';
                        return x;
                    }
                case "e":
                    {
                        string s = Element.ToString("x4");
                        string x = string.Empty;
                        x += ((Mask & 0x0000f000) != 0) ? s[0] : 'x';
                        x += ((Mask & 0x00000f00) != 0) ? s[1] : 'x';
                        x += ((Mask & 0x000000f0) != 0) ? s[2] : 'x';
                        x += ((Mask & 0x0000000f) != 0) ? s[3] : 'x';
                        return x;
                    }
                case "G":
                default:
                    {
                        return string.Format("({0},{1})", ToString("g", null), ToString("e", null));
                    }
            }
        }

        public bool IsMatch(DicomTag tag)
        {
            return Card == ((((uint)tag.Group << 16) | tag.Element) & Mask);
        }

        public static DicomMaskedTag Parse(string s)
        {
            try
            {
                if (s.Length < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(s), "Expected a string of 8 or more characters");
                }

                int pos = 0;
                if (s[pos] == '(')
                {
                    pos++;
                }

                int idx = s.IndexOf(',');
                if (idx == -1)
                {
                    idx = pos + 4;
                }

                string group = s.Substring(pos, idx - pos);

                pos = idx + 1;

                string element = null;
                if (s[s.Length - 1] == ')')
                {
                    element = s.Substring(pos, s.Length - pos - 1);
                }
                else
                {
                    element = s.Substring(pos);
                }

                return Parse(group, element);
            }
            catch (Exception e)
            {
                if (e is DicomDataException) throw;
                else throw new DicomDataException("Error parsing masked DICOM tag ['" + s + "']", e);
            }
        }

        public static DicomMaskedTag Parse(string group, string element)
        {
            try
            {
                var tag = new DicomMaskedTag();

                ushort g = ushort.Parse(group.ToLower().Replace('x', '0'), NumberStyles.HexNumber);
                ushort e = ushort.Parse(element.ToLower().Replace('x', '0'), NumberStyles.HexNumber);
                tag.Tag = new DicomTag(g, e);

                string mask = (group + element).ToLowerInvariant();
                mask =
                    mask.Replace('0', 'f')
                        .Replace('1', 'f')
                        .Replace('2', 'f')
                        .Replace('3', 'f')
                        .Replace('4', 'f')
                        .Replace('5', 'f')
                        .Replace('6', 'f')
                        .Replace('7', 'f')
                        .Replace('8', 'f')
                        .Replace('9', 'f')
                        .Replace('a', 'f')
                        .Replace('b', 'f')
                        .Replace('c', 'f')
                        .Replace('d', 'f')
                        .Replace('e', 'f')
                        .Replace('f', 'f')
                        .Replace('x', '0');
                tag.Mask = uint.Parse(mask, NumberStyles.HexNumber);

                return tag;
            }
            catch (Exception e)
            {
                throw new DicomDataException(
                    "Error parsing masked DICOM tag [group:'" + group + "', element:'" + element + "']",
                    e);
            }
        }
    }
}
