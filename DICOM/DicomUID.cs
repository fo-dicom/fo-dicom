using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace Dicom {
	public enum DicomUidType {
		TransferSyntax,
		SOPClass,
		MetaSOPClass,
		SOPInstance,
		ApplicationContextName,
		CodingScheme,
		FrameOfReference,
		LDAP,
		Unknown
	}

	public sealed partial class DicomUID : DicomParseable {
		public readonly static DicomUID Implementation = new DicomUID("1.3.6.1.4.1.30071.7", "DICOM for .NET 4.0", DicomUidType.Unknown);

		private string _uid;
		private string _name;
		private DicomUidType _type;

		public DicomUID(string uid, string name, DicomUidType type) {
			_uid = uid;
			_name = name;
			_type = type;
		}

		public string UID {
			get {
				return _uid;
			}
		}

		public string Name {
			get {
				return _name;
			}
		}

		public DicomUidType Type {
			get {
				return _type;
			}
		}

		public static bool IsValid(string uid) {
			if (String.IsNullOrEmpty(uid))
				return false;

			// only checks that the UID contains valid characters
			foreach (char c in uid) {
				if (c != '.' && !Char.IsDigit(c))
					return false;
			}

			return true;
		}

		public static DicomUID Parse(string s) {
			string u = s.TrimEnd(' ', '\0');

			DicomUID uid = null;
			if (_uids.TryGetValue(u, out uid))
				return uid;

			if (!IsValid(u))
				throw new DicomDataException("Invalid characters in UID string ['" + u + "']");

			return new DicomUID(u, "Unknown", DicomUidType.Unknown);
		}

		private static IDictionary<string, DicomUID> _uids;

		static DicomUID() {
			_uids = new ConcurrentDictionary<string, DicomUID>();
			LoadInternalUIDs();
		}

		public static bool operator ==(DicomUID a, DicomUID b) {
			if (((object)a == null) && ((object)b == null))
				return true;
			if (((object)a == null) || ((object)b == null))
				return false;
			return a.UID == b.UID;
		}
		public static bool operator !=(DicomUID a, DicomUID b) {
			return !(a == b);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(this, obj))
				return true;
			if (!(obj is DicomUID))
				return false;
			return (obj as DicomUID).UID == UID;
		}

		public override int GetHashCode() {
			return UID.GetHashCode();
		}

		public override string ToString() {
			return UID;
		}
	}
}
