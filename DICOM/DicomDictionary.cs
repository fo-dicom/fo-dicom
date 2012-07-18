using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public partial class DicomDictionary : IEnumerable<DicomDictionaryEntry> {
		#region Private Members
		private readonly static DicomDictionaryEntry UnknownTag = new DicomDictionaryEntry(DicomMaskedTag.Parse("xxxx","xxxx"), "Unknown", "Unknown", DicomVM.VM_1_n, false, 
			DicomVR.UN, DicomVR.AE, DicomVR.AS, DicomVR.AT, DicomVR.CS, DicomVR.DA, DicomVR.DS, DicomVR.DT, DicomVR.FD, DicomVR.FL, DicomVR.IS, DicomVR.LO, DicomVR.LT, DicomVR.OB, 
			DicomVR.OF, DicomVR.OW, DicomVR.PN, DicomVR.SH, DicomVR.SL, DicomVR.SQ, DicomVR.SS, DicomVR.ST, DicomVR.TM, DicomVR.UI, DicomVR.UL, DicomVR.US, DicomVR.UT);

		private readonly static DicomDictionaryEntry GroupLength = new DicomDictionaryEntry(DicomMaskedTag.Parse("xxxx","0000"), "Group Length", "GroupLength", DicomVM.VM_1, false, DicomVR.UL);

		private DicomPrivateCreator _privateCreator;
		private IDictionary<string, DicomPrivateCreator> _creators;
		private IDictionary<DicomPrivateCreator, DicomDictionary> _private;
		private IDictionary<DicomTag, DicomDictionaryEntry> _entries;
		private List<DicomDictionaryEntry> _masked;
		private bool _sortMasked;
		#endregion

		#region Constructors
		public DicomDictionary() {
			_creators = new Dictionary<string, DicomPrivateCreator>();
			_private = new Dictionary<DicomPrivateCreator, DicomDictionary>();
			_entries = new Dictionary<DicomTag, DicomDictionaryEntry>();
			_masked = new List<DicomDictionaryEntry>();
		}

		private DicomDictionary(DicomPrivateCreator creator) {
			_privateCreator = creator;
			_entries = new Dictionary<DicomTag, DicomDictionaryEntry>();
			_masked = new List<DicomDictionaryEntry>();
		}
		#endregion

		#region Properties
		private static object _lock = new object();
		private static DicomDictionary _default;
		public static DicomDictionary Default {
			get {
				lock (_lock) {
					if (_default == null) {
						_default = new DicomDictionary();
						LoadInternalDictionary(_default);
						_default.Add(GroupLength);
					}
					return _default;
				}
			}
			set {
				lock (_lock)
					_default = value;
			}
		}

		public DicomDictionaryEntry this[DicomTag tag] {
			get {
				if (_private != null && tag.PrivateCreator != null) {
					DicomDictionary pvt = null;
					if (_private.TryGetValue(tag.PrivateCreator, out pvt))
						return pvt[tag];
				}

				DicomDictionaryEntry entry = null;
				if (_entries.TryGetValue(tag, out entry))
					return entry;

				entry = _masked.Where(x => x.MaskTag.IsMatch(tag)).FirstOrDefault();
				if (entry != null)
					return entry;

				return UnknownTag;
			}
		}

		public DicomDictionary this[DicomPrivateCreator creator] {
			get {
				DicomDictionary pvt = null;
				if (!_private.TryGetValue(creator, out pvt)) {
					pvt = new DicomDictionary(creator);
					_private.Add(creator, pvt);
				}
				return pvt;
			}
		}
		#endregion

		#region Public Methods
		public void Add(DicomDictionaryEntry entry) {
			if (_privateCreator != null) {
				entry.Tag = new DicomTag(entry.Tag.Group, entry.Tag.Element, _privateCreator);
				if (entry.MaskTag != null)
					entry.MaskTag.Tag = entry.Tag;
			}

			if (entry.MaskTag == null) {
				_entries.Add(entry.Tag, entry);
			} else {
				_masked.Add(entry);
				_sortMasked = true;
			}
		}

		public DicomPrivateCreator GetPrivateCreator(string creator) {
			DicomPrivateCreator pvt = null;
			if (!_creators.TryGetValue(creator, out pvt)) {
				pvt = new DicomPrivateCreator(creator);
				_creators.Add(creator, pvt);
			}
			return pvt;
		}

		public void Load(string file, DicomDictionaryFormat format) {
			DicomDictionaryReader reader = new DicomDictionaryReader(this, format, file);
			reader.Process();
		}
		#endregion

		#region IEnumerable Members

		public IEnumerator<DicomDictionaryEntry> GetEnumerator() {
			List<DicomDictionaryEntry> items = new List<DicomDictionaryEntry>();
			items.AddRange(_entries.Values.OrderBy(x => x.Tag));

			if (_sortMasked) {
				_masked.Sort((a, b) => { return a.MaskTag.Mask.CompareTo(b.MaskTag.Mask); });
				_sortMasked = false;
			}
			items.AddRange(_masked);

			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		#endregion
	}
}
