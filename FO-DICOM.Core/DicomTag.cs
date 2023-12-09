// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using System;
using System.Globalization;

namespace FellowOakDicom
{

    /// <summary>
    /// DICOM Tag
    /// </summary>
    public sealed partial class DicomTag : IFormattable, IEquatable<DicomTag>, IComparable<DicomTag>, IComparable
    {
        public static readonly DicomTag Unknown = new DicomTag(0xffff, 0xffff);

        public DicomTag(ushort group, ushort element)
        {
            Group = group;
            Element = element;
        }

        public DicomTag(ushort group, ushort element, string privateCreator)
            : this(group, element, DicomDictionary.Default.GetPrivateCreator(privateCreator))
        {
        }

        public DicomTag(ushort group, ushort element, DicomPrivateCreator privateCreator)
        {
            Group = group;
            Element = element;
            PrivateCreator = privateCreator;
        }

        public static implicit operator DicomTag(uint tag)
        {
            return new DicomTag((ushort)((tag >> 16) & 0xffff), (ushort)(tag & 0xffff));
        }

        public static explicit operator uint(DicomTag tag)
        {
            return (uint)(tag.Group << 16) | tag.Element;
        }

        public ushort Group { get; }

        public ushort Element { get; }

        public bool IsPrivate => Group.IsOdd();

        public DicomPrivateCreator PrivateCreator { get; set; }

        public DicomDictionaryEntry DictionaryEntry => DicomDictionary.Default[this];

        public override string ToString()
            => ToString("G", null);

        /// <summary>
        /// This method returns a string representation of the DicomTag.
        /// Use one of the following formats as parameter:
        /// - "G": returns for example "(0028,0010)" for public and "(0029,1001:MYPRIVATE)" for private tags
        /// - "X": returns for example "(0028,0010)" for public and "(0029,xx01:MYPRIVATE)" for private tags
        /// - "J": returns for example "00280010" for public and "00291001" for private tags
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider?.GetFormat(GetType()) is ICustomFormatter fmt)
            {
                return fmt.Format(format, this, formatProvider);
            }

            switch (format)
            {
                case "X":
                    {
                        return PrivateCreator != null
                            ? string.Format("({0:x4},xx{1:x2}:{2})", Group, Element & 0xff, PrivateCreator.Creator)
                            : string.Format("({0:x4},{1:x4})", Group, Element);
                    }
                case "g":
                    {
                        return PrivateCreator != null
                            ? string.Format("{0:x4},{1:x4}:{2}", Group, Element, PrivateCreator.Creator)
                            : string.Format("{0:x4},{1:x4}", Group, Element);
                    }
                case "J":
                    {
                        return string.Format("{0:X4}{1:X4}", Group, Element);
                    }
                case "G":
                default:
                    {
                        return PrivateCreator != null
                            ? string.Format("({0:x4},{1:x4}:{2})", Group, Element, PrivateCreator.Creator)
                            : string.Format("({0:x4},{1:x4})", Group, Element);
                    }
            }
        }

        public int CompareTo(DicomTag other)
        {
            if (Group != other.Group) return Group.CompareTo(other.Group);

            if (Element != other.Element) return Element.CompareTo(other.Element);

            // sort by private creator only if element values are equal
            if (PrivateCreator != null || other.PrivateCreator != null)
            {
                if (PrivateCreator == null) return -1;
                if (other.PrivateCreator == null) return 1;

                return PrivateCreator.CompareTo(other.PrivateCreator);
            }

            return 0;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (!(obj is DicomTag)) throw new ArgumentException("Passed non-DicomTag to comparer", nameof(obj));
            return CompareTo(obj as DicomTag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals(obj as DicomTag);
        }

        public bool Equals(DicomTag other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (Group != other.Group) return false;

            if (PrivateCreator != null || other.PrivateCreator != null)
            {
                if (PrivateCreator == null || other.PrivateCreator == null) return false;

                if (PrivateCreator.Creator != other.PrivateCreator.Creator) return false;

                return (Element & 0xff) == (other.Element & 0xff);
            }

            return Element == other.Element;
        }

        public static bool operator ==(DicomTag a, DicomTag b)
        {
            if (((object)a == null) && ((object)b == null)) return true;
            if (((object)a == null) || ((object)b == null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(DicomTag a, DicomTag b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (PrivateCreator == null)
                {
                    return ((uint)(Group << 16) | Element).GetHashCode();
                }

                return ((uint)(Group << 16) | (uint)(Element & 0xff)).GetHashCode() ^ PrivateCreator.GetHashCode();
            }
        }

        public static DicomTag Parse(string s)
        {
            try
            {
                if (s.Length < 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(s), "Expected a string of 8 or more characters");
                }

                int pos = 0;
                if (s[pos] == '(') { pos++; }

                ushort group = ushort.Parse(s.Substring(pos, 4), NumberStyles.HexNumber);
                pos += 4;

                if (s[pos] == ',') { pos++; }

                ushort element = ushort.Parse(s.Substring(pos, 4), NumberStyles.HexNumber);
                pos += 4;

                DicomPrivateCreator creator = null;
                if (s.Length > pos && s[pos] == ':')
                {
                    pos++;

                    string c = null;
                    if (s[s.Length - 1] == ')')
                    {
                        c = s.Substring(pos, s.Length - pos - 1);
                    }
                    else
                    {
                        c = s.Substring(pos);
                    }

                    creator = DicomDictionary.Default.GetPrivateCreator(c);
                }

                //TODO: get value from related DicomDictionaryEntry

                return new DicomTag(group, element, creator);
            }
            catch (Exception e)
            {
                throw new DicomDataException($"Error parsing DICOM tag ['{s}']", e);
            }
        }

    }
}
