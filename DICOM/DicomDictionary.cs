using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dicom {
	public partial class DicomDictionary : IEnumerable<DicomDictionaryEntry> {
		#region Private Members
		public readonly static DicomDictionaryEntry UnknownTag = new DicomDictionaryEntry(DicomMaskedTag.Parse("xxxx","xxxx"), "Unknown", "Unknown", DicomVM.VM_1_n, false, 
			DicomVR.UN, DicomVR.AE, DicomVR.AS, DicomVR.AT, DicomVR.CS, DicomVR.DA, DicomVR.DS, DicomVR.DT, DicomVR.FD, DicomVR.FL, DicomVR.IS, DicomVR.LO, DicomVR.LT, DicomVR.OB, 
			DicomVR.OF, DicomVR.OW, DicomVR.PN, DicomVR.SH, DicomVR.SL, DicomVR.SQ, DicomVR.SS, DicomVR.ST, DicomVR.TM, DicomVR.UI, DicomVR.UL, DicomVR.US, DicomVR.UT);

		public readonly static DicomDictionaryEntry PrivateCreatorTag = new DicomDictionaryEntry(DicomMaskedTag.Parse("xxxx", "00xx"), "Private Creator", "PrivateCreator", DicomVM.VM_1, false, DicomVR.LO);

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

		private static void LoadInternalDictionaries() {
			lock (_lock) {
				if (_default == null) {
					_default = new DicomDictionary();
					_default.Add(new DicomDictionaryEntry(DicomMaskedTag.Parse("xxxx", "0000"), "Group Length", "GroupLength", DicomVM.VM_1, false, DicomVR.UL));
					try {
						var assembly = Assembly.GetExecutingAssembly();
						var stream = assembly.GetManifestResourceStream("Dicom.Dictionaries.DICOM Dictionary.xml.gz");
						var gzip = new GZipStream(stream, CompressionMode.Decompress);
						var reader = new DicomDictionaryReader(_default, DicomDictionaryFormat.XML, gzip);
						reader.Process();
					} catch (Exception e) {
						throw new DicomDataException("Unable to load DICOM dictionary from resources.\n\n" + e.Message, e);
					}
					try {
						var assembly = Assembly.GetExecutingAssembly();
						var stream = assembly.GetManifestResourceStream("Dicom.Dictionaries.Private Dictionary.xml.gz");
						var gzip = new GZipStream(stream, CompressionMode.Decompress);
						var reader = new DicomDictionaryReader(_default, DicomDictionaryFormat.XML, gzip);
						reader.Process();
					} catch (Exception e) {
						throw new DicomDataException("Unable to load private dictionary from resources.\n\n" + e.Message, e);
					}
				}
			}
		}

		public static DicomDictionary Default {
			get {
				if (_default == null)
					LoadInternalDictionaries();
				return _default;
			}
			set {
				lock (_lock)
					_default = value;
			}
		}

		public DicomPrivateCreator PrivateCreator {
			get { return _privateCreator; }
			internal set { _privateCreator = value; }
		}

		public DicomDictionaryEntry this[DicomTag tag] {
			get {
				if (_private != null && tag.PrivateCreator != null) {
					DicomDictionary pvt = null;
					if (_private.TryGetValue(tag.PrivateCreator, out pvt))
						return pvt[tag];
				}

				// special case for private creator tag
				if (tag.IsPrivate && tag.Element != 0x0000 && tag.Element <= 0x00ff)
					return PrivateCreatorTag;

				DicomDictionaryEntry entry = null;
				if (_entries.TryGetValue(tag, out entry))
					return entry;

				// this is faster than LINQ query
				foreach (var x in _masked) {
					if (x.MaskTag.IsMatch(tag))
						return x;
				}

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
				// allow overwriting of existing entries
				_entries[entry.Tag] = entry;
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
			using (var fs = File.OpenRead(file)) {
				Stream s = fs;

				if (file.EndsWith(".gz"))
					s = new GZipStream(s, CompressionMode.Decompress);

				DicomDictionaryReader reader = new DicomDictionaryReader(this, format, s);
				reader.Process();
			}
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
