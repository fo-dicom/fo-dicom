using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using Dicom.IO.Buffer;
using Dicom.Log;

namespace Dicom {
	public class DicomDataset : IEnumerable<DicomItem> {
		private IDictionary<DicomTag, DicomItem> _items;

		public DicomDataset() {
			_items = new SortedList<DicomTag, DicomItem>();
			InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
		}

		public DicomDataset(params DicomItem[] items) : this() {
			foreach (DicomItem item in items)
				_items[item.Tag] = item;
		}

		public DicomDataset(IEnumerable<DicomItem> items) : this() {
			foreach (DicomItem item in items)
				_items[item.Tag] = item;
		}

		public DicomTransferSyntax InternalTransferSyntax {
			get;
			internal set;
		}

		public bool Exists(DicomTag tag) {
			return _items.ContainsKey(tag);
		}

		public T Get<T>(DicomTag tag, int n=0) {
			return Get<T>(tag, n, default(T));
		}

		public T Get<T>(DicomTag tag, T defaultValue) {
			return Get<T>(tag, 0, defaultValue);
		}

		public T Get<T>(DicomTag tag, int n, T defaultValue) {
			DicomItem item = null;
			if (!_items.TryGetValue(tag, out item))
				return defaultValue;

			if (typeof(T) == typeof(DicomItem))
				return (T)(object)item;

			if (typeof(T).IsSubclassOf(typeof(DicomItem)))
				return (T)(object)item;

			if (typeof(T) == typeof(DicomVR))
				return (T)(object)item.ValueRepresentation;

			if (item.GetType().IsSubclassOf(typeof(DicomElement))) {
				DicomElement element = (DicomElement)item;

				if (typeof(IByteBuffer).IsAssignableFrom(typeof(T)))
					return (T)(object)element.Buffer;

				if (typeof(T) == typeof(byte[]))
					return (T)(object)element.Buffer.Data;

				if (n >= element.Count)
					return defaultValue;
				return (T)(object)element.Get<T>(n);
			}

			throw new DicomDataException("Unable to get a value type of {0} from DICOM item of type {1}", typeof(T), item.GetType());
		}

		public DicomDataset Add(params DicomItem[] items) {
			foreach (DicomItem item in items)
				_items[item.Tag] = item;
			return this;
		}

		public DicomDataset Add(IEnumerable<DicomItem> items) {
			foreach (DicomItem item in items)
				_items[item.Tag] = item;
			return this;
		}

		public DicomDataset Add<T>(DicomTag tag, params T[] values) {
			if (values == null)
				return this;

			var entry = DicomDictionary.Default[tag];
			if (entry == null)
				throw new DicomDataException("Tag {0} not found in DICOM dictionary", tag);

			var vr = entry.ValueRepresentations.FirstOrDefault(x => x.ValueType == typeof(T));
			if (vr == null)
				vr = entry.ValueRepresentations.First();

			if (vr == DicomVR.AE) {
				if (typeof(T) == typeof(string))
					return Add(new DicomApplicationEntity(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.AS) {
				if (typeof(T) == typeof(string))
					return Add(new DicomAgeString(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.AT) {
				if (typeof(T) == typeof(DicomTag))
					return Add(new DicomAttributeTag(tag, values.Cast<DicomTag>().ToArray()));
			}

			if (vr == DicomVR.CS) {
				if (typeof(T) == typeof(string))
					return Add(new DicomCodeString(tag, values.Cast<string>().ToArray()));
				if (typeof(T).IsEnum)
					return Add(new DicomCodeString(tag, values.Select(x => x.ToString()).ToArray()));
			}

			if (vr == DicomVR.DA) {
				if (typeof(T) == typeof(DateTime))
					return Add(new DicomDate(tag, values.Cast<DateTime>().ToArray()));
				if (typeof(T) == typeof(DicomDateRange))
					return Add(new DicomDate(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()));
				if (typeof(T) == typeof(string))
					return Add(new DicomDate(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.DS) {
				if (typeof(T) == typeof(decimal))
					return Add(new DicomDecimalString(tag, values.Cast<decimal>().ToArray()));
				if (typeof(T) == typeof(string))
					return Add(new DicomDecimalString(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.DT) {
				if (typeof(T) == typeof(DateTime))
					return Add(new DicomDateTime(tag, values.Cast<DateTime>().ToArray()));
				if (typeof(T) == typeof(DicomDateRange))
					return Add(new DicomDateTime(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()));
				if (typeof(T) == typeof(string))
					return Add(new DicomDateTime(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.FD) {
				if (typeof(T) == typeof(double))
					return Add(new DicomFloatingPointDouble(tag, values.Cast<double>().ToArray()));
			}

			if (vr == DicomVR.FL) {
				if (typeof(T) == typeof(float))
					return Add(new DicomFloatingPointSingle(tag, values.Cast<float>().ToArray()));
			}

			if (vr == DicomVR.IS) {
				if (typeof(T) == typeof(int))
					return Add(new DicomIntegerString(tag, values.Cast<int>().ToArray()));
				if (typeof(T) == typeof(string))
					return Add(new DicomIntegerString(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.LO) {
				if (typeof(T) == typeof(string))
					return Add(new DicomLongString(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.LT) {
				if (typeof(T) == typeof(string))
					return Add(new DicomLongText(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.OB) {
				if (typeof(T) == typeof(byte))
					return Add(new DicomOtherByte(tag, values.Cast<byte>().ToArray()));
			}

			if (vr == DicomVR.OF) {
				if (typeof(T) == typeof(float))
					return Add(new DicomOtherFloat(tag, values.Cast<float>().ToArray()));
			}

			if (vr == DicomVR.OW) {
				if (typeof(T) == typeof(ushort))
					return Add(new DicomOtherWord(tag, values.Cast<ushort>().ToArray()));
			}

			if (vr == DicomVR.PN) {
				if (typeof(T) == typeof(string))
					return Add(new DicomPersonName(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.SH) {
				if (typeof(T) == typeof(string))
					return Add(new DicomShortString(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.SL) {
				if (typeof(T) == typeof(int))
					return Add(new DicomSignedLong(tag, values.Cast<int>().ToArray()));
			}

			if (vr == DicomVR.SS) {
				if (typeof(T) == typeof(short))
					return Add(new DicomSignedShort(tag, values.Cast<short>().ToArray()));
			}

			if (vr == DicomVR.ST) {
				if (typeof(T) == typeof(string))
					return Add(new DicomShortText(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.TM) {
				if (typeof(T) == typeof(DateTime))
					return Add(new DicomTime(tag, values.Cast<DateTime>().ToArray()));
				if (typeof(T) == typeof(DicomDateRange))
					return Add(new DicomTime(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()));
				if (typeof(T) == typeof(string))
					return Add(new DicomTime(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.UI) {
				if (typeof(T) == typeof(DicomUID))
					return Add(new DicomUniqueIdentifier(tag, values.Cast<DicomUID>().ToArray()));
				if (typeof(T) == typeof(DicomTransferSyntax))
					return Add(new DicomUniqueIdentifier(tag, values.Cast<DicomTransferSyntax>().ToArray()));
			}

			if (vr == DicomVR.UL) {
				if (typeof(T) == typeof(uint))
					return Add(new DicomUnsignedLong(tag, values.Cast<uint>().ToArray()));
			}

			if (vr == DicomVR.UN) {
				if (typeof(T) == typeof(byte))
					return Add(new DicomUnknown(tag, values.Cast<byte>().ToArray()));
			}

			if (vr == DicomVR.US) {
				if (typeof(T) == typeof(ushort))
					return Add(new DicomUnsignedShort(tag, values.Cast<ushort>().ToArray()));
			}

			if (vr == DicomVR.UT) {
				if (typeof(T) == typeof(string))
					return Add(new DicomUnlimitedText(tag, values.Cast<string>().First()));
			}

			throw new InvalidOperationException(String.Format("Unable to create DICOM element of type {0} with values of type {1}", vr.Code, typeof(T).ToString()));
		}

		public bool Contains(DicomTag tag) {
			return _items.ContainsKey(tag);
		}

		/// <summary>
		/// Removes items for specified tags.
		/// </summary>
		/// <param name="tags">DICOM tags to remove</param>
		/// <returns>Current Dataset</returns>
		public DicomDataset Remove(params DicomTag[] tags) {
			foreach (DicomTag tag in tags)
				_items.Remove(tag);
			return this;
		}

		/// <summary>
		/// Removes items where the selector function returns true.
		/// </summary>
		/// <param name="selector">Selector function</param>
		/// <returns>Current Dataset</returns>
		public DicomDataset Remove(Func<DicomItem, bool> selector) {
			foreach (DicomItem item in _items.Values.Where(selector).ToArray())
				_items.Remove(item.Tag);
			return this;
		}

		/// <summary>
		/// Enumerates all DICOM items.
		/// </summary>
		/// <returns>Enumeration of DICOM items</returns>
		public IEnumerator<DicomItem> GetEnumerator() {
			return _items.Values.GetEnumerator();
		}

		/// <summary>
		/// Enumerates all DICOM items.
		/// </summary>
		/// <returns>Enumeration of DICOM items</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return _items.Values.GetEnumerator();
		}

		/// <summary>
		/// Enumerates DICOM items for specified group.
		/// </summary>
		/// <param name="group">Group</param>
		/// <returns>Enumeration of DICOM items</returns>
		public IEnumerable<DicomItem> GetGroup(ushort group) {
			return _items.Values.Where(x => x.Tag.Group == group && x.Tag.Element != 0x0000);
		}
	}
}
