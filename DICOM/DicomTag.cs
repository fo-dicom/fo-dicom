using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Dicom.Imaging.Mathematics;

namespace Dicom {
	/// <summary>
	/// DICOM Tag
	/// </summary>
	[Serializable]
	public sealed partial class DicomTag : IFormattable, IEquatable<DicomTag>, IComparable<DicomTag>, IComparable, ISerializable {
		public readonly static DicomTag Unknown = new DicomTag(0xffff, 0xffff);

		public DicomTag(ushort group, ushort element) {
			Group = group;
			Element = element;
		}

		public DicomTag(ushort group, ushort element, DicomPrivateCreator privateCreator) {
			Group = group;
			Element = element;
			PrivateCreator = privateCreator;
		}

		private DicomTag(SerializationInfo info, StreamingContext ctx) {
			Group = info.GetUInt16("group");
			Element = info.GetUInt16("element");
			try {
				string creator = info.GetString("creator");
				if (!String.IsNullOrWhiteSpace(creator))
					PrivateCreator = DicomDictionary.Default.GetPrivateCreator(creator);
			} catch (SerializationException) {
			}
		}

		public ushort Group {
			get;
			private set;
		}

		public ushort Element {
			get;
			private set;
		}

		private int HashCode {
			get { return (Group << 16) | Element; }
		}

		public bool IsPrivate {
			get { return Group.IsOdd(); }
		}

		public DicomPrivateCreator PrivateCreator {
			get;
			private set;
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
				case "g": {
						if (PrivateCreator != null)
							return String.Format("{0:x4},{1:x4}:{2}", Group, Element, PrivateCreator.Creator);
						else
							return String.Format("{0:x4},{1:x4}", Group, Element);
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
			if (PrivateCreator != null || other.PrivateCreator != null) {
				if (PrivateCreator == null)
					return -1;
				if (other.PrivateCreator == null)
					return 1;
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
			return CompareTo(other) == 0;
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

		public override int GetHashCode() {
			//unchecked {
			//    if (PrivateCreator == null)
			//        return (Group << 16) + Element + String.Empty.GetHashCode();
			//    else
			//        return (Group << 16) + Element + PrivateCreator.Creator.GetHashCode();
			//}
			return HashCode;
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter=true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("group", Group);
			info.AddValue("element", Element);
			if (PrivateCreator != null)
				info.AddValue("creator", PrivateCreator.Creator);
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
