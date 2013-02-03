using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dicom.IO;

namespace Dicom {
	public abstract class DicomItem : IComparable<DicomItem>, IComparable {
		protected DicomItem(DicomTag tag) {
			Tag = tag;

			//DicomDictionaryEntry entry = DicomDictionary.Default[Tag];
			//if (entry != null && !entry.ValueRepresentations.Contains(ValueRepresentation))
			//	throw new DicomDataException("{0} is not a valid value representation for {1}", ValueRepresentation, Tag);
		}

		public DicomTag Tag {
			get;
			protected set;
		}

		public abstract DicomVR ValueRepresentation {
			get;
		}

		public int CompareTo(DicomItem other) {
			return Tag.CompareTo(other.Tag);
		}

		public int CompareTo(object obj) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			if (!(obj is DicomItem))
				throw new ArgumentException("Passed non-DicomItem to comparer", "obj");
			return CompareTo(obj as DicomItem);
		}

		public override string ToString() {
			if (Tag.DictionaryEntry != null)
				return String.Format("{0} {1} {2}", Tag, ValueRepresentation, Tag.DictionaryEntry.Name);
			return String.Format("{0} {1} Unknown", Tag, ValueRepresentation);
		}
	}
}
