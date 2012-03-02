using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Dicom {
	public sealed class DicomMaskedTag : IFormattable {
		public const uint FullMask = 0xffffffff;

		public DicomMaskedTag(DicomTag tag) {
			Tag = tag;
			Mask = FullMask;
		}

		private DicomMaskedTag() {
		}

		private DicomTag _tag = null;
		public DicomTag Tag {
			get {
				if (_tag == null)
					_tag = new DicomTag(Group, Element);
				return _tag;
			}
			set {
				_tag = value;
				Card = ((uint)Group << 16) | (uint)Element;
			}
		}

		public ushort Group {
			get { return Tag.Group; }
		}

		public ushort Element {
			get { return Tag.Element; }
		}

		public uint Card {
			get;
			private set;
		}

		public uint Mask {
			get;
			private set;
		}

		public override string ToString() {
			return ToString("G", null);
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			if (formatProvider != null) {
				ICustomFormatter fmt = formatProvider.GetFormat(this.GetType()) as ICustomFormatter;
				if (fmt != null)
					return fmt.Format(format, this, formatProvider);
			}

			switch (format) {
				case "g": {
						string s = Group.ToString("x4");
						string x = String.Empty;
						x += ((Mask & 0xf0000000) != 0) ? s[0] : 'x';
						x += ((Mask & 0x0f000000) != 0) ? s[1] : 'x';
						x += ((Mask & 0x00f00000) != 0) ? s[2] : 'x';
						x += ((Mask & 0x000f0000) != 0) ? s[3] : 'x';
						return x;
					}
				case "e": {
						string s = Element.ToString("x4");
						string x = String.Empty;
						x += ((Mask & 0x0000f000) != 0) ? s[0] : 'x';
						x += ((Mask & 0x00000f00) != 0) ? s[1] : 'x';
						x += ((Mask & 0x000000f0) != 0) ? s[2] : 'x';
						x += ((Mask & 0x0000000f) != 0) ? s[3] : 'x';
						return x;
					}
				case "G":
				default: {
						return String.Format("({0},{1})", this.ToString("g", null), this.ToString("e", null));
					}
			}
		}

		public bool IsMatch(DicomTag tag) {
			return Card == ((((uint)tag.Group << 16) | (uint)tag.Element) & Mask);
		}

		public static DicomMaskedTag Parse(string s) {
			try {
				if (s.Length < 8)
					throw new ArgumentOutOfRangeException("s", "Expected a string of 8 or more characters");

				int pos = 0;
				if (s[pos] == '(')
					pos++;

				int idx = s.IndexOf(',');
				if (idx == -1)
					idx = pos + 4;

				string group = s.Substring(pos, idx - pos);

				pos = idx + 1;

				string element = null;
				if (s[s.Length - 1] == ')')
					element = s.Substring(pos, s.Length - pos - 1);
				else
					element = s.Substring(pos);

				return Parse(group, element);
			} catch (Exception e) {
				if (e is DicomDataException)
					throw;
				else
					throw new DicomDataException("Error parsing masked DICOM tag ['" + s + "']", e);
			}
		}

		public static DicomMaskedTag Parse(string group, string element) {
			try {
				DicomMaskedTag tag = new DicomMaskedTag();

				ushort g = ushort.Parse(group.ToLower().Replace('x', '0'), NumberStyles.HexNumber);
				ushort e = ushort.Parse(element.ToLower().Replace('x', '0'), NumberStyles.HexNumber);
				tag.Tag = new DicomTag(g, e);

				string mask = group + element;
				mask = mask.Replace('0', 'f').Replace('1', 'f').Replace('2', 'f')
							.Replace('3', 'f').Replace('4', 'f').Replace('5', 'f')
							.Replace('6', 'f').Replace('7', 'f').Replace('8', 'f')
							.Replace('9', 'f').Replace('a', 'f').Replace('b', 'f')
							.Replace('c', 'f').Replace('d', 'f').Replace('e', 'f')
							.Replace('f', 'f').Replace('x', '0');
				tag.Mask = uint.Parse(mask, NumberStyles.HexNumber);

				return tag;
			} catch (Exception e) {
				throw new DicomDataException("Error parsing masked DICOM tag [group:'" + group + "', element:'" + element +"']", e);
			}
		}
	}
}
