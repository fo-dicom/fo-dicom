using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom.StructuredReport {
	public class DicomCodeItem : DicomDataset {
		public DicomCodeItem(DicomDataset dataset) : base(dataset) {
		}

		public DicomCodeItem(DicomSequence sequence) {
			if (sequence.Items.Count == 0)
				throw new DicomDataException("No code item found in sequence.");
			Add(sequence.Items[0]);
		}

		public DicomCodeItem(string value, string scheme, string meaning, string version=null) {
			Add(DicomTag.CodeValue, value);
			Add(DicomTag.CodingSchemeDesignator, scheme);
			Add(DicomTag.CodeMeaning, meaning);
			if (version != null)
				Add(DicomTag.CodingSchemeVersion, version);
		}

		public string Value {
			get { return Get<string>(DicomTag.CodeValue, 0, String.Empty); }
		}

		public string Scheme {
			get { return Get<string>(DicomTag.CodingSchemeDesignator, 0, String.Empty); }
		}

		public string Meaning {
			get { return Get<string>(DicomTag.CodeMeaning, 0, String.Empty); }
		}

		public string Version {
			get { return Get<string>(DicomTag.CodingSchemeVersion, 0, String.Empty); }
		}

		public override bool Equals(object obj) {
			if (Object.ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != typeof(DicomCodeItem))
				return false;
			return Value == ((DicomCodeItem)obj).Value && Scheme == ((DicomCodeItem)obj).Scheme && Version == ((DicomCodeItem)obj).Version;
		}

		public static bool operator ==(DicomCodeItem a, DicomCodeItem b) {
			if (((object)a == null) && ((object)b == null))
				return true;
			if (((object)a == null) || ((object)b == null))
				return false;
			return a.Equals(b);
		}

		public static bool operator !=(DicomCodeItem a, DicomCodeItem b) {
			return !(a == b);
		}

		private int _hash = 0;
		public override int GetHashCode() {
			if (_hash == 0)
				_hash = ToString().GetHashCode();
			return _hash;
		}

		public override string ToString() {
			if (!String.IsNullOrEmpty(Version))
				return String.Format("({0},{1}:{2},\"{3}\")", Value, Scheme, Version, Meaning);
			return String.Format("({0},{1},\"{2}\")", Value, Scheme, Meaning);
		}
	}
}
