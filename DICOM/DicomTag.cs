using System;
using System.Globalization;

using Dicom.Imaging.Mathematics;

namespace Dicom {
	/// <summary>
	/// DICOM Tag
	/// </summary>
	public sealed partial class DicomTag : IFormattable, IEquatable<DicomTag>, IComparable<DicomTag>, IComparable {
		public readonly static DicomTag Unknown = new DicomTag(0xffff, 0xffff);

		public DicomTag(ushort group, ushort element) {
			Group = group;
			Element = element;
		}

		public DicomTag(ushort group, ushort element, string privateCreator) : this(group, element, DicomDictionary.Default.GetPrivateCreator(privateCreator)) {
		}

		public DicomTag(ushort group, ushort element, DicomPrivateCreator privateCreator) {
			Group = group;
			Element = element;
			PrivateCreator = privateCreator;
		}

		public ushort Group {
			get;
			private set;
		}

		public ushort Element {
			get;
			private set;
		}

		public bool IsPrivate {
			get { return Group.IsOdd(); }
		}

		public DicomPrivateCreator PrivateCreator {
			get;
			internal set;
		}

		public DicomDictionaryEntry DictionaryEntry {
			get {
				return DicomDictionary.Default[this];
			}
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
			case "X": {
				if (PrivateCreator != null)
					return String.Format("({0:x4},xx{1:x2}:{2})", Group, Element & 0xff, PrivateCreator.Creator);
				else
					return String.Format("({0:x4},{1:x4})", Group, Element);
			}
			case "g": {
					if (PrivateCreator != null)
						return String.Format("{0:x4},{1:x4}:{2}", Group, Element, PrivateCreator.Creator);
					else
						return String.Format("{0:x4},{1:x4}", Group, Element);
				}
            case "J": {
                        return String.Format("{0:X4}{1:X4}", Group, Element);
                }
			case "G":
			default: {
				if (PrivateCreator != null)
					return String.Format("({0:x4},{1:x4}:{2})", Group, Element, PrivateCreator.Creator);
				else
					return String.Format("({0:x4},{1:x4})", Group, Element);
				}
			}
		}

		public int CompareTo(DicomTag other) {
			if (Group != other.Group)
				return Group.CompareTo(other.Group);

			if (Element != other.Element)
				return Element.CompareTo(other.Element);

			// sort by private creator only if element values are equal
			if (PrivateCreator != null || other.PrivateCreator != null) {
			    if (PrivateCreator == null)
			        return -1;
			    if (other.PrivateCreator == null)
			        return 1;

				if (PrivateCreator != other.PrivateCreator)
					return PrivateCreator.CompareTo(other.PrivateCreator);
			}

			return 0;
		}

		public int CompareTo(object obj) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (!(obj is DicomTag))
				throw new ArgumentException("Passed non-DicomTag to comparer", "obj");
			return CompareTo(obj as DicomTag);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(obj, null))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (GetType() != obj.GetType())
				return false;
			return Equals(obj as DicomTag);
		}

		public bool Equals(DicomTag other) {
			if (ReferenceEquals(other, null))
				return false;
			if (ReferenceEquals(this, other))
				return true;

			if (Group != other.Group)
				return false;

			if (PrivateCreator != null || other.PrivateCreator != null) {
				if (PrivateCreator == null || other.PrivateCreator == null)
					return false;

				if (PrivateCreator.Creator != other.PrivateCreator.Creator)
					return false;

				return (Element & 0xff) == (other.Element & 0xff);
			}

			return Element == other.Element;
		}

		public static bool operator ==(DicomTag a, DicomTag b) {
			if (((object)a == null) && ((object)b == null))
				return true;
			if (((object)a == null) || ((object)b == null))
				return false;
			return a.Equals(b);
		}

		public static bool operator !=(DicomTag a, DicomTag b) {
			return !(a == b);
		}

		private int _hash = 0;

		public override int GetHashCode() {
			if (_hash == 0)
				_hash = ToString("X", null).GetHashCode();
			return _hash;
		}

		public static DicomTag Parse(string s) {
			try {
				if (s.Length < 8)
					throw new ArgumentOutOfRangeException("s", "Expected a string of 8 or more characters");

				int pos = 0;
				if (s[pos] == '(')
					pos++;

				ushort group = ushort.Parse(s.Substring(pos, 4), NumberStyles.HexNumber); pos += 4;

				if (s[pos] == ',')
					pos++;

				ushort element = ushort.Parse(s.Substring(pos, 4), NumberStyles.HexNumber); pos += 4;

				DicomPrivateCreator creator = null;
				if (s.Length > pos && s[pos] == ':') {
					pos++;

					string c = null;
					if (s[s.Length - 1] == ')')
						c = s.Substring(pos, s.Length - pos - 1);
					else
						c = s.Substring(pos);

					creator = DicomDictionary.Default.GetPrivateCreator(c);
				}

				//TODO: get value from related DicomDictionaryEntry

				return new DicomTag(group, element, creator);
			} catch (Exception e) {
				throw new DicomDataException("Error parsing DICOM tag ['" + s + "']", e);
			}
		}
	}
}
