using System;
using System.Collections.Generic;
using System.Linq;
using Dicom.IO.Buffer;

using Dicom.StructuredReport;

namespace Dicom {
	public class DicomDataset : IEnumerable<DicomItem> {
		private IDictionary<DicomTag, DicomItem> _items;
		private DicomTransferSyntax _syntax;

		public DicomDataset() {
			_items = new SortedList<DicomTag, DicomItem>();
			InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
		}

		public DicomDataset(params DicomItem[] items) : this() {
			if (items != null) {
				foreach (DicomItem item in items)
					if (item != null)
						_items[item.Tag] = item;
			}
		}

		public DicomDataset(IEnumerable<DicomItem> items) : this() {
			if (items != null) {
				foreach (DicomItem item in items)
					if (item != null)
						_items[item.Tag] = item;
			}
		}

		/// <summary>DICOM transfer syntax of this dataset.</summary>
		public DicomTransferSyntax InternalTransferSyntax {
			get { return _syntax; }
			internal set {
				_syntax = value;

				// update transfer syntax for sequence items
				foreach (var sq in this.Where(x => x.ValueRepresentation == DicomVR.SQ).Cast<DicomSequence>()) {
					foreach (var item in sq.Items) {
						item.InternalTransferSyntax = _syntax;
					}
				}
			}
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

				if (n >= element.Count || element.Count == 0)
					return defaultValue;

				return (T)(object)element.Get<T>(n);
			}

			if (item.GetType() == typeof(DicomSequence)) {
				if (typeof(T) == typeof(DicomCodeItem))
					return (T)(object)new DicomCodeItem((DicomSequence)item);

				if (typeof(T) == typeof(DicomMeasuredValue))
					return (T)(object)new DicomMeasuredValue((DicomSequence)item);

				if (typeof(T) == typeof(DicomReferencedSOP))
					return (T)(object)new DicomReferencedSOP((DicomSequence)item);
			}

			throw new DicomDataException("Unable to get a value type of {0} from DICOM item of type {1}", typeof(T), item.GetType());
		}

		/// <summary>
		/// Converts a dictionary tag to a valid private tag. Creates the private creator tag if needed.
		/// </summary>
		/// <param name="tag">Dictionary DICOM tag</param>
		/// <returns>Private DICOM tag</returns>
		public DicomTag GetPrivateTag(DicomTag tag) {
			// not a private tag
			if (!tag.IsPrivate)
				return tag;

			// group length
			if (tag.Element == 0x0000)
				return tag;

			// private creator?
			if (tag.PrivateCreator == null)
				return tag;

			// already a valid private tag
			if (tag.Element >= 0xff)
				return tag;

			ushort group = 0x0010;
			for (; ; group++) {
				var creator = new DicomTag(tag.Group, group);
				if (!Contains(creator)) {
					Add(new DicomLongString(creator, tag.PrivateCreator.Creator));
					break;
				}

				var value = Get<string>(creator, String.Empty);
				if (tag.PrivateCreator.Creator == value)
					return new DicomTag(tag.Group, (ushort)((group << 8) + (tag.Element & 0xff)), tag.PrivateCreator);
			}

			return new DicomTag(tag.Group, (ushort)((group << 8) + (tag.Element & 0xff)), tag.PrivateCreator);
		}

		public DicomDataset Add(params DicomItem[] items) {
			if (items != null) {
				foreach (DicomItem item in items) {
					if (item != null) {
						if (item.Tag.IsPrivate)
							_items[GetPrivateTag(item.Tag)] = item;
						else
							_items[item.Tag] = item;
					}
				}
			}
			return this;
		}

		public DicomDataset Add(IEnumerable<DicomItem> items) {
			if (items != null) {
				foreach (DicomItem item in items) {
					if (item != null) {
						if (item.Tag.IsPrivate)
							_items[GetPrivateTag(item.Tag)] = item;
						else
							_items[item.Tag] = item;
					}
				}
			}
			return this;
		}

		public DicomDataset Add<T>(DicomTag tag, params T[] values) {
			var entry = DicomDictionary.Default[tag];
			if (entry == null)
				throw new DicomDataException("Tag {0} not found in DICOM dictionary. Only dictionary tags may be added implicitly to the dataset.", tag);

			DicomVR vr = null;
			if (values != null)
				vr = entry.ValueRepresentations.FirstOrDefault(x => x.ValueType == typeof(T));
			if (vr == null)
				vr = entry.ValueRepresentations.First();

			if (vr == DicomVR.AE) {
				if (values == null)
					return Add(new DicomApplicationEntity(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomApplicationEntity(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.AS) {
				if (values == null)
					return Add(new DicomAgeString(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomAgeString(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.AT) {
				if (values == null)
					return Add(new DicomAttributeTag(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(DicomTag))
					return Add(new DicomAttributeTag(tag, values.Cast<DicomTag>().ToArray()));
			}

			if (vr == DicomVR.CS) {
				if (values == null)
					return Add(new DicomCodeString(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomCodeString(tag, values.Cast<string>().ToArray()));
				if (typeof(T).IsEnum)
					return Add(new DicomCodeString(tag, values.Select(x => x.ToString()).ToArray()));
			}

			if (vr == DicomVR.DA) {
				if (values == null)
					return Add(new DicomDate(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(DateTime))
					return Add(new DicomDate(tag, values.Cast<DateTime>().ToArray()));
				if (typeof(T) == typeof(DicomDateRange))
					return Add(new DicomDate(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()));
				if (typeof(T) == typeof(string))
					return Add(new DicomDate(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.DS) {
				if (values == null)
					return Add(new DicomDecimalString(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(decimal))
					return Add(new DicomDecimalString(tag, values.Cast<decimal>().ToArray()));
				if (typeof(T) == typeof(string))
					return Add(new DicomDecimalString(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.DT) {
				if (values == null)
					return Add(new DicomDateTime(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(DateTime))
					return Add(new DicomDateTime(tag, values.Cast<DateTime>().ToArray()));
				if (typeof(T) == typeof(DicomDateRange))
					return Add(new DicomDateTime(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()));
				if (typeof(T) == typeof(string))
					return Add(new DicomDateTime(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.FD) {
				if (values == null)
					return Add(new DicomFloatingPointDouble(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(double))
					return Add(new DicomFloatingPointDouble(tag, values.Cast<double>().ToArray()));
			}

			if (vr == DicomVR.FL) {
				if (values == null)
					return Add(new DicomFloatingPointSingle(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(float))
					return Add(new DicomFloatingPointSingle(tag, values.Cast<float>().ToArray()));
			}

			if (vr == DicomVR.IS) {
				if (values == null)
					return Add(new DicomIntegerString(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(int))
					return Add(new DicomIntegerString(tag, values.Cast<int>().ToArray()));
				if (typeof(T) == typeof(string))
					return Add(new DicomIntegerString(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.LO) {
				if (values == null)
					return Add(new DicomLongString(tag, DicomEncoding.Default, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomLongString(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.LT) {
				if (values == null)
					return Add(new DicomLongText(tag, DicomEncoding.Default, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomLongText(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.OB) {
				if (values == null)
					return Add(new DicomOtherByte(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(byte))
					return Add(new DicomOtherByte(tag, values.Cast<byte>().ToArray()));
			}

			if (vr == DicomVR.OF) {
				if (values == null)
					return Add(new DicomOtherFloat(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(float))
					return Add(new DicomOtherFloat(tag, values.Cast<float>().ToArray()));
			}

			if (vr == DicomVR.OW) {
				if (values == null)
					return Add(new DicomOtherWord(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(ushort))
					return Add(new DicomOtherWord(tag, values.Cast<ushort>().ToArray()));
			}

			if (vr == DicomVR.PN) {
				if (values == null)
					return Add(new DicomPersonName(tag, DicomEncoding.Default, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomPersonName(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.SH) {
				if (values == null)
					return Add(new DicomShortString(tag, DicomEncoding.Default, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomShortString(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.SL) {
				if (values == null)
					return Add(new DicomSignedLong(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(int))
					return Add(new DicomSignedLong(tag, values.Cast<int>().ToArray()));
			}

			if (vr == DicomVR.SQ) {
				if (values == null)
					return Add(new DicomSequence(tag));
				if (typeof(T) == typeof(DicomContentItem))
					return Add(new DicomSequence(tag, values.Cast<DicomContentItem>().Select(x => x.Dataset).ToArray()));
				if (typeof(T) == typeof(DicomDataset) || typeof(T) == typeof(DicomCodeItem) || typeof(T) == typeof(DicomMeasuredValue) || typeof(T) == typeof(DicomReferencedSOP))
					return Add(new DicomSequence(tag, values.Cast<DicomDataset>().ToArray()));
			}

			if (vr == DicomVR.SS) {
				if (values == null)
					return Add(new DicomSignedShort(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(short))
					return Add(new DicomSignedShort(tag, values.Cast<short>().ToArray()));
			}

			if (vr == DicomVR.ST) {
				if (values == null)
					return Add(new DicomShortText(tag, DicomEncoding.Default, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomShortText(tag, values.Cast<string>().First()));
			}

			if (vr == DicomVR.TM) {
				if (values == null)
					return Add(new DicomTime(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(DateTime))
					return Add(new DicomTime(tag, values.Cast<DateTime>().ToArray()));
				if (typeof(T) == typeof(DicomDateRange))
					return Add(new DicomTime(tag, values.Cast<DicomDateRange>().FirstOrDefault() ?? new DicomDateRange()));
				if (typeof(T) == typeof(string))
					return Add(new DicomTime(tag, values.Cast<string>().ToArray()));
			}

			if (vr == DicomVR.UI) {
				if (values == null)
					return Add(new DicomUniqueIdentifier(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomUniqueIdentifier(tag, values.Cast<string>().First()));
				if (typeof(T) == typeof(DicomUID))
					return Add(new DicomUniqueIdentifier(tag, values.Cast<DicomUID>().ToArray()));
				if (typeof(T) == typeof(DicomTransferSyntax))
					return Add(new DicomUniqueIdentifier(tag, values.Cast<DicomTransferSyntax>().ToArray()));
			}

			if (vr == DicomVR.UL) {
				if (values == null)
					return Add(new DicomUnsignedLong(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(uint))
					return Add(new DicomUnsignedLong(tag, values.Cast<uint>().ToArray()));
			}

			if (vr == DicomVR.UN) {
				if (values == null)
					return Add(new DicomUnknown(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(byte))
					return Add(new DicomUnknown(tag, values.Cast<byte>().ToArray()));
			}

			if (vr == DicomVR.US) {
				if (values == null)
					return Add(new DicomUnsignedShort(tag, EmptyBuffer.Value));
				if (typeof(T) == typeof(ushort))
					return Add(new DicomUnsignedShort(tag, values.Cast<ushort>().ToArray()));
			}

			if (vr == DicomVR.UT) {
				if (values == null)
					return Add(new DicomUnlimitedText(tag, DicomEncoding.Default, EmptyBuffer.Value));
				if (typeof(T) == typeof(string))
					return Add(new DicomUnlimitedText(tag, values.Cast<string>().First()));
			}

			throw new InvalidOperationException(String.Format("Unable to create DICOM element of type {0} with values of type {1}", vr.Code, typeof(T).ToString()));
		}

		/// <summary>
		/// Checks the DICOM dataset to determine if the dataset already contains an item with the specified tag.
		/// </summary>
		/// <param name="tag">DICOM tag to test</param>
		/// <returns><c>True</c> if a DICOM item with the specified tag already exists.</returns>
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
		/// Removes all items from the dataset.
		/// </summary>
		/// <returns>Current Dataset</returns>
		public DicomDataset Clear() {
			_items.Clear();
			return this;
		}

		/// <summary>
		/// Copies all items to the destination dataset.
		/// </summary>
		/// <param name="destination">Destination Dataset</param>
		/// <returns>Current Dataset</returns>
		public DicomDataset CopyTo(DicomDataset destination) {
			if (destination != null)
				destination.Add(this);
			return this;
		}

		/// <summary>
		/// Copies tags to the destination dataset.
		/// </summary>
		/// <param name="destination">Destination Dataset</param>
		/// <param name="tags">Tags to copy</param>
		/// <returns>Current Dataset</returns>
		public DicomDataset CopyTo(DicomDataset destination, params DicomTag[] tags) {
			if (destination != null) {
				foreach (var tag in tags)
					destination.Add(Get<DicomItem>(tag));
			}
			return this;
		}

		/// <summary>
		/// Copies tags matching mask to the destination dataset.
		/// </summary>
		/// <param name="destination">Destination Dataset</param>
		/// <param name="mask">Tags to copy</param>
		/// <returns>Current Dataset</returns>
		public DicomDataset CopyTo(DicomDataset destination, DicomMaskedTag mask) {
			if (destination != null)
				destination.Add(_items.Values.Where(x => mask.IsMatch(x.Tag)));
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

		public override string ToString() {
			return String.Format("DICOM Dataset [{0} items]", _items.Count);
		}
	}
}
