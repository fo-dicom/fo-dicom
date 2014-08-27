using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Dicom.IO;
using Dicom.IO.Buffer;

using Dicom.Imaging.Mathematics;

namespace Dicom {
	public abstract class DicomElement : DicomItem {
		protected DicomElement(DicomTag tag, IByteBuffer data) : base(tag) {
			this.Buffer = data;
		}

		/// <summary>Gets the number of values that the DICOM element contains.</summary>
		/// <value>Number of value items</value>
		public abstract int Count {
			get;
		}

		public IByteBuffer Buffer {
			get;
			protected set;
		}

		public uint Length {
			get {
				if (Buffer != null)
					return Buffer.Size;
				return 0;
			}
		}

		public abstract T Get<T>(int item = -1);
	}

	/// <summary>
	/// Base class for a DICOM string element.
	/// </summary>
	/// <seealso cref="DicomPersonName"/>
	public abstract class DicomStringElement : DicomElement {
		protected DicomStringElement(DicomTag tag, string value) : this(tag, DicomEncoding.Default, value) {
		}

		protected DicomStringElement(DicomTag tag, Encoding encoding, string value) : base(tag, EmptyBuffer.Value) {
			Encoding = encoding;
			Buffer = ByteConverter.ToByteBuffer(value ?? String.Empty, encoding, ValueRepresentation.PaddingValue);
		}

		protected DicomStringElement(DicomTag tag, Encoding encoding, IByteBuffer buffer) : base(tag, buffer) {
			Encoding = encoding;
		}

		public Encoding Encoding {
			get;
			protected set;
		}

		/// <summary>Gets the number of values that the DICOM element contains.</summary>
		/// <value>Number of value items</value>
		public override int Count {
			get { return 1; }
		}

		private string _value = null;
		protected string StringValue {
			get {
				if (_value == null && Buffer != null)
					_value = Encoding.GetString(Buffer.Data, 0, (int)Buffer.Size).TrimEnd((char)ValueRepresentation.PaddingValue);
				return _value;
			}
		}

		public override T Get<T>(int item = -1) {
			if (typeof(T) == typeof(string) || typeof(T) == typeof(object))
				return (T)((object)StringValue);

			if (typeof(T) == typeof(string[]) || typeof(T) == typeof(object[]))
				return (T)(object)(new string[] { StringValue });

			if (typeof(T).IsSubclassOf(typeof(DicomParseable)))
				return (T)DicomParseable.Parse<T>(StringValue);

			if (typeof(T).IsEnum)
				return (T)Enum.Parse(typeof(T), StringValue, true);

			throw new InvalidCastException("Unable to convert DICOM " + ValueRepresentation.Code + " value to '" + typeof(T).Name + "'");
		}
	}

	public abstract class DicomMultiStringElement : DicomStringElement {
		protected DicomMultiStringElement(DicomTag tag, params string[] values) : base(tag, DicomEncoding.Default, String.Join("\\", values)) {
		}

		protected DicomMultiStringElement(DicomTag tag, Encoding encoding, params string[] values) : base(tag, encoding, String.Join("\\", values)) {
		}

		protected DicomMultiStringElement(DicomTag tag, Encoding encoding, IByteBuffer buffer) : base(tag, encoding, buffer) {
		}

		private int _count = -1;
		public override int Count {
			get {
				if (_count == -1) {
					if (String.IsNullOrEmpty(StringValue))
						_count = 0;
					else
						_count = StringValue.Split('\\').Count();
				}
				return _count;
			}
		}

		private string[] _values = null;
		public override T Get<T>(int item = -1) {
			if (_values == null) {
				if (String.IsNullOrEmpty(StringValue))
					_values = new string[0];
				else
					_values = StringValue.Split('\\');
				_count = _values.Length;
			}

			if (typeof(T) == typeof(string) || typeof(T) == typeof(object)) {
				if (item == -1)
					return (T)((object)StringValue);

				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				return (T)((object)_values[item]);
			}

			if (typeof(T) == typeof(string[]) || typeof(T) == typeof(object[]))
				return (T)(object)_values;

			if (typeof(T).IsSubclassOf(typeof(DicomParseable)))
				return (T)DicomParseable.Parse<T>(_values[item]);

			if (typeof(T).IsEnum)
				return (T)Enum.Parse(typeof(T), _values[item], true);

			throw new InvalidCastException("Unable to convert DICOM " + ValueRepresentation.Code + " value to '" + typeof(T).Name + "'");
		}
	}

	public abstract class DicomDateElement : DicomMultiStringElement {
		protected DicomDateElement(DicomTag tag, string[] dateFormats, params DateTime[] values) : base(tag, DicomEncoding.Default, values.Select(x => x.ToString(dateFormats[0])).ToArray()) {
			DateFormats = dateFormats;
		}

		protected DicomDateElement(DicomTag tag, string[] dateFormats, DicomDateRange range) : base(tag, DicomEncoding.Default, range.ToString(dateFormats[0])) {
			DateFormats = dateFormats;
		}

		protected DicomDateElement(DicomTag tag, string[] dateFormats, params string[] values) : base(tag, DicomEncoding.Default, String.Join("\\", values)) {
			DateFormats = dateFormats;
		}

		protected DicomDateElement(DicomTag tag, string[] dateFormats, IByteBuffer buffer) : base(tag, DicomEncoding.Default, buffer) {
			DateFormats = dateFormats;
		}

		protected string[] DateFormats {
			get;
			private set;
		}

		private DateTime[] _values = null;
		public override T Get<T>(int item = -1) {
			// no need to parse DateTime values if returning string(s)
			if (typeof(T) == typeof(string) || typeof(T) == typeof(string[]))
				return base.Get<T>(item);

			if (typeof(T) == typeof(DicomDateRange)) {
				string[] vals = base.Get<string>(item).Split('-');
				var range = new DicomDateRange();
				if (vals.Length >= 2) {
					if (!String.IsNullOrEmpty(vals[0]))
						range.Minimum = DateTime.ParseExact(vals[0], DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault);
					if (!String.IsNullOrEmpty(vals[1]))
						range.Maximum = DateTime.ParseExact(vals[1], DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault);
				} else if (vals.Length == 1) {
					range.Minimum = DateTime.ParseExact(vals[0], DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault);
					range.Maximum = range.Minimum.AddDays(1).AddMilliseconds(-1);
				}
				return (T)(object)range;
			}

			if (_values == null) {
				string[] vals = base.Get<string[]>();
				if (vals.Length == 1 && String.IsNullOrEmpty(vals[0]))
					_values = new DateTime[0];
				else {
					_values = new DateTime[vals.Length];
					for (int i = 0; i < vals.Length; i++)
						_values[i] = DateTime.ParseExact(vals[i], DateFormats, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault);
				}
			}

			if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(object)) {
                if (item == -1)
                    return (T)((object) _values[0]);

				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				return (T)((object)_values[item]);
			}

			if (typeof(T) == typeof(DateTime[]) || typeof(T) == typeof(object[])) {
				return (T)(object)_values;
			}

			return base.Get<T>(item);
		}
	}

	public abstract class DicomValueElement<Tv> : DicomElement where Tv: struct {
		protected DicomValueElement(DicomTag tag, params Tv[] values) : this(tag, ByteConverter.ToByteBuffer<Tv>(values)) {
		}

		protected DicomValueElement(DicomTag tag, IByteBuffer data) : base(tag, data) {
		}

		public override int Count {
			get { return (int)Buffer.Size / ValueRepresentation.UnitSize; }
		}

		#region Public Members
		public override T Get<T>(int item = 0) {
			if (typeof(T) == typeof(Tv)) {
				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				return ByteConverter.Get<T>(Buffer, item);
			}

			if (typeof(T) == typeof(Tv[])) {
				// Is there a way to avoid this cast?
				return (T)(object)ByteConverter.ToArray<Tv>(Buffer);
			}

			if (typeof(T) == typeof(string)) {
				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				return (T)(object)ByteConverter.Get<Tv>(Buffer, item).ToString();
			}

			if (typeof(T) == typeof(string[])) {
				return (T)(object)ByteConverter.ToArray<Tv>(Buffer).Select(x => x.ToString()).ToArray();
			}

			if (typeof(T).IsEnum) {
				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				var s = ByteConverter.Get<Tv>(Buffer, item).ToString();
				return (T)Enum.Parse(typeof(T), s, true);
			}

			if (typeof(T).IsValueType) {
				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				return (T)Convert.ChangeType(ByteConverter.Get<Tv>(Buffer, item), typeof(T));
			}

			throw new InvalidCastException("Unable to convert DICOM " + ValueRepresentation.Code + " value to '" + typeof(T).Name + "'");
		}
		#endregion
	}

	/// <summary>Application Entity (AE)</summary>
	public class DicomApplicationEntity : DicomMultiStringElement {
		#region Public Constructors
		public DicomApplicationEntity(DicomTag tag, params string[] values) : base(tag, DicomEncoding.Default, values) {
		}

		public DicomApplicationEntity(DicomTag tag, IByteBuffer data) : base(tag, DicomEncoding.Default, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR  ValueRepresentation {
			get { return DicomVR.AE; }
		}
		#endregion
	}

	/// <summary>Age String (AS)</summary>
	public class DicomAgeString : DicomMultiStringElement {
		#region Public Constructors
		public DicomAgeString(DicomTag tag, params string[] values) : base(tag, DicomEncoding.Default, values) {
		}

		public DicomAgeString(DicomTag tag, IByteBuffer data) : base(tag, DicomEncoding.Default, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.AS; }
		}
		#endregion
	}

	/// <summary>Attribute Tag (AT)</summary>
	public class DicomAttributeTag : DicomElement {
		#region Public Constructors
		public DicomAttributeTag(DicomTag tag, params DicomTag[] values) : base(tag, EmptyBuffer.Value) {
			Values = values;
		}

		public DicomAttributeTag(DicomTag tag, IByteBuffer data) : base(tag, data) {
		}
		#endregion

		#region Public Properties
		public override int Count {
			get { return (int)Buffer.Size / 4; }
		}

		public override DicomVR ValueRepresentation {
			get { return DicomVR.AT; }
		}

		private DicomTag[] _values;
		public IEnumerable<DicomTag> Values {
			get {
				if (_values == null) {
					var values = new List<DicomTag>();
					var parts = ByteBufferEnumerator<ushort>.Create(Buffer).ToArray();
					for (int i = 0; i < parts.Length; i += 2) {
						var group = parts[i + 0];
						var element = parts[i + 1];
						values.Add(new DicomTag(group, element));
					}
					_values = values.ToArray();
				}
				return _values;
			}
			private set {
				_values = value.ToArray();
				int length = _values.Length * 4;
				byte[] buffer = new byte[length];
				for (int i = 0; i < _values.Length; i++) {
					var bytes = BitConverter.GetBytes(_values[i].Group);
					Array.Copy(bytes, 0, buffer, i * 4, 2);
					bytes = BitConverter.GetBytes(_values[i].Element);
					Array.Copy(bytes, 0, buffer, i * 4 + 2, 2);
				}
				Buffer = new MemoryByteBuffer(buffer);
			}
		}
		#endregion

		#region Public Members
		public override T Get<T>(int item = 0) {
			var tags = Values.ToArray();

			if (typeof(T) == typeof(DicomTag))
				return (T)(object)tags[item];

			if (typeof(T) == typeof(DicomTag[]))
				return (T)(object)tags;

			if (typeof(T) == typeof(string))
				return (T)(object)tags[item].ToString();

			if (typeof(T) == typeof(string[]))
				return (T)(object)tags.Select(x => x.ToString()).ToArray();

			throw new InvalidCastException("Unable to convert DICOM " + ValueRepresentation.Code + " value to '" + typeof(T).Name + "'");
		}
		#endregion
	}

	/// <summary>Code String (CS)</summary>
	public class DicomCodeString : DicomMultiStringElement {
		#region Public Constructors
		public DicomCodeString(DicomTag tag, params string[] values) : base(tag, DicomEncoding.Default, values) {
		}

		public DicomCodeString(DicomTag tag, IByteBuffer data) : base (tag, DicomEncoding.Default, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.CS; }
		}
		#endregion
	}

	/// <summary>Date (DA)</summary>
	public class DicomDate : DicomDateElement {
		#region Public Constructors
		public DicomDate(DicomTag tag, params DateTime[] values) : base(tag, PrivateDateFormats, values) {
		}

		public DicomDate(DicomTag tag, DicomDateRange range) : base(tag, PrivateDateFormats, range) {
		}

		public DicomDate(DicomTag tag, params string[] values) : base(tag, PrivateDateFormats, values) {
		}

		public DicomDate(DicomTag tag, IByteBuffer data) : base (tag, PrivateDateFormats, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.DA; }
		}

		private static string[] _formats;
		private static string[] PrivateDateFormats {
			get {
				if (_formats == null) {
					_formats = new string[6];
					_formats[0] = "yyyyMMdd";
					_formats[1] = "yyyy.MM.dd";
					_formats[2] = "yyyy/MM/dd";
					_formats[3] = "yyyy";
					_formats[4] = "yyyyMM";
					_formats[5] = "yyyy.MM";
				}
				return _formats;
			}
		}
		#endregion
	}

	/// <summary>Decimal String (DS)</summary>
	public class DicomDecimalString : DicomMultiStringElement {
		#region Public Constructors
		public DicomDecimalString(DicomTag tag, params decimal[] values) : base(tag, DicomEncoding.Default, values.Select(x => x.ToString()).ToArray()) {
		}

		public DicomDecimalString(DicomTag tag, params string[] values) : base(tag, DicomEncoding.Default, values) {
		}

		public DicomDecimalString(DicomTag tag, IByteBuffer data) : base (tag, DicomEncoding.Default, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.DS; }
		}
		#endregion

		#region Public Members
		private decimal[] _values;
		public override T Get<T>(int item = -1) {
			// no need to parse values if returning string(s)
			if (typeof(T) == typeof(string) || typeof(T) == typeof(string[]))
				return base.Get<T>(item);

			if (_values == null) {
				_values = base.Get<string[]>().Select(x => decimal.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture)).ToArray();
			}

			if (typeof(T) == typeof(decimal) || typeof(T) == typeof(object)) {
                return (T)(object)_values[Math.Max(item, 0)];
			}

			if (typeof(T) == typeof(decimal[]) || typeof(T) == typeof(object[])) {
				return (T)(object)_values;
			}

			if (typeof(T) == typeof(double))
                return (T)(object)Convert.ToDouble(_values[Math.Max(item, 0)]);

			if (typeof(T) == typeof(double[]))
				return (T)(object)_values.Select(x => Convert.ToDouble(x)).ToArray();

			return base.Get<T>(item);
		}
		#endregion
	}

	/// <summary>Date Time (DT)</summary>
	public class DicomDateTime : DicomDateElement {
		#region Public Constructors
		public DicomDateTime(DicomTag tag, params DateTime[] values) : base(tag, PrivateDateFormats, values) {
		}

		public DicomDateTime(DicomTag tag, DicomDateRange range) : base(tag, PrivateDateFormats, range) {
		}

		public DicomDateTime(DicomTag tag, params string[] values) : base(tag, PrivateDateFormats, values) {
		}

		public DicomDateTime(DicomTag tag, IByteBuffer data) : base (tag, PrivateDateFormats, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.DT; }
		}

		private static string[] _formats;
		private static string[] PrivateDateFormats {
			get {
				if (_formats == null) {
				    _formats = new string[11];
					_formats[0] = "yyyyMMddHHmmsszzz";
					_formats[1] = "yyyyMMddHHmmsszz";
					_formats[2] = "yyyyMMddHHmmssz";
					_formats[3] = "yyyyMMddHHmmss.fff";
					_formats[4] = "yyyyMMddHHmmss.ff";
					_formats[5] = "yyyyMMddHHmmss.f";
					_formats[6] = "yyyyMMddHHmmss";
					_formats[7] = "yyyyMMddHHmm";
					_formats[8] = "yyyyMMdd";
					_formats[9] = "yyyy.MM.dd";
					_formats[10] = "yyyy/MM/dd";
				}
				return _formats;
			}
		}
		#endregion
	}

	/// <summary>Floating Point Double (FD)</summary>
	public class DicomFloatingPointDouble : DicomValueElement<double> {
		#region Public Constructors
		public DicomFloatingPointDouble(DicomTag tag, params double[] values) : base(tag, values) {
		}

		public DicomFloatingPointDouble(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.FD; }
		}
		#endregion
	}

	/// <summary>Floating Point Single (FL)</summary>
	public class DicomFloatingPointSingle : DicomValueElement<float> {
		#region Public Constructors
		public DicomFloatingPointSingle(DicomTag tag, params float[] values) : base(tag, values) {
		}

		public DicomFloatingPointSingle(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.FL; }
		}
		#endregion
	}

	/// <summary>Integer String (IS)</summary>
	public class DicomIntegerString : DicomMultiStringElement {
		#region Public Constructors
		public DicomIntegerString(DicomTag tag, params int[] values) : base(tag, DicomEncoding.Default, values.Select(x => x.ToString()).ToArray()) {
		}

		public DicomIntegerString(DicomTag tag, params string[] values) : base(tag, DicomEncoding.Default, values) {
		}

		public DicomIntegerString(DicomTag tag, IByteBuffer data) : base (tag, DicomEncoding.Default, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.IS; }
		}
		#endregion

		#region Public Members
		private int[] _values;
		public override T Get<T>(int item = -1) {
			// no need to parse values if returning string(s)
			if (typeof(T) == typeof(string) || typeof(T) == typeof(string[]))
				return base.Get<T>(item);

			if (_values == null) {
				_values = base.Get<string[]>().Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray();
			}

			if (typeof(T) == typeof(int) || typeof(T) == typeof(object)) {
                return (T)(object)_values[Math.Max(item, 0)];
			}

			if (typeof(T) == typeof(int[]) || typeof(T) == typeof(object[])) {
				return (T)(object)_values;
			}

			if (typeof(T).IsEnum) {
				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				return (T)(object)_values[item];
			}

			if (typeof(T).IsValueType) {
				if (item < 0 || item >= Count)
					throw new ArgumentOutOfRangeException("item", "Index is outside the range of available value items");

				return (T)Convert.ChangeType(_values[item], typeof(T));
			}

			return base.Get<T>(item);
		}
		#endregion
	}

	/// <summary>Long String (LO)</summary>
	public class DicomLongString : DicomMultiStringElement {
		#region Public Constructors
        public DicomLongString(DicomTag tag, params string[] values) : base(tag, values) {
		}

        public DicomLongString(DicomTag tag, Encoding encoding, params string[] values) : base(tag, encoding, values) {
        }

		public DicomLongString(DicomTag tag, Encoding encoding, IByteBuffer data) : base (tag, encoding, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.LO; }
		}
		#endregion
	}

	/// <summary>Long Text (LT)</summary>
	public class DicomLongText : DicomStringElement {
		#region Public Constructors
		public DicomLongText(DicomTag tag, string value) : base(tag, value) {
		}

        public DicomLongText(DicomTag tag, Encoding encoding, string value) : base(tag, encoding, value) {
        }

		public DicomLongText(DicomTag tag, Encoding encoding, IByteBuffer data) : base(tag, encoding, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.LT; }
		}
		#endregion
	}

	/// <summary>Other Byte (OB)</summary>
	public class DicomOtherByte : DicomValueElement<byte> {
		#region Public Constructors
		public DicomOtherByte(DicomTag tag, params byte[] values) : base(tag, values) {
		}

		public DicomOtherByte(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.OB; }
		}
		#endregion

		#region Public Methods
		public override T Get<T>(int item = -1) {
			if (!typeof(T).IsArray && item == -1)
				item = 0;

			if (typeof(T) == typeof(short))
				return (T)(object)ByteBufferEnumerator<short>.Create(Buffer).ToArray().GetValue(item);

			if (typeof(T) == typeof(short[]))
				return (T)(object)ByteBufferEnumerator<short>.Create(Buffer).ToArray();

			if (typeof(T) == typeof(ushort))
				return (T)(object)ByteBufferEnumerator<ushort>.Create(Buffer).ToArray().GetValue(item);

			if (typeof(T) == typeof(ushort[]))
				return (T)(object)ByteBufferEnumerator<ushort>.Create(Buffer).ToArray();

			if (typeof(T) == typeof(int))
				return (T)(object)ByteBufferEnumerator<int>.Create(Buffer).ToArray().GetValue(item);

			if (typeof(T) == typeof(int[]))
				return (T)(object)ByteBufferEnumerator<int>.Create(Buffer).ToArray();

			if (typeof(T) == typeof(uint))
				return (T)(object)ByteBufferEnumerator<uint>.Create(Buffer).ToArray().GetValue(item);

			if (typeof(T) == typeof(uint[]))
				return (T)(object)ByteBufferEnumerator<uint>.Create(Buffer).ToArray();

			if (typeof(T) == typeof(float))
				return (T)(object)ByteBufferEnumerator<float>.Create(Buffer).ToArray().GetValue(item);

			if (typeof(T) == typeof(float[]))
				return (T)(object)ByteBufferEnumerator<float>.Create(Buffer).ToArray();

			if (typeof(T) == typeof(double))
				return (T)(object)ByteBufferEnumerator<double>.Create(Buffer).ToArray().GetValue(item);

			if (typeof(T) == typeof(double[]))
				return (T)(object)ByteBufferEnumerator<double>.Create(Buffer).ToArray();

			return base.Get<T>(item);
		}
		#endregion
	}

	/// <summary>Other Word (OW)</summary>
	public class DicomOtherWord : DicomValueElement<ushort> {
		#region Public Constructors
		public DicomOtherWord(DicomTag tag, params ushort[] values) : base(tag, values) {
		}

		public DicomOtherWord(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.OW; }
		}
		#endregion
	}

	/// <summary>Other Float (OF)</summary>
	public class DicomOtherFloat : DicomValueElement<float> {
		#region Public Constructors
		public DicomOtherFloat(DicomTag tag, params float[] values) : base(tag, values) {
		}

		public DicomOtherFloat(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.OF; }
		}
		#endregion
	}

	/// <summary>Person Name (PN)</summary>
	public sealed class DicomPersonName : DicomMultiStringElement {
		#region Public Constructors
        public DicomPersonName(DicomTag tag, params string[] values) : base(tag, values) {
		}

        public DicomPersonName(DicomTag tag, Encoding encoding, params string[] values) : base(tag, encoding, values) {
        }

        public DicomPersonName(DicomTag tag, string Last, string First, string Middle = null, string Prefix = null, string Suffix = null) : 
            base(tag, ConcatName(Last, First, Middle, Prefix, Suffix)) {
		}

        public DicomPersonName(DicomTag tag, Encoding encoding,  string Last, string First, string Middle = null, string Prefix = null, string Suffix = null) : 
            base(tag, encoding, ConcatName(Last, First, Middle, Prefix, Suffix)) {
        }

		public DicomPersonName(DicomTag tag, Encoding encoding, IByteBuffer data) : base (tag, encoding, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.PN; }
		}

		public string Last {
            get {
                string[] s = Get<string>().Split('\\');
                if (s.Count() < 1) return "";
                s = s[0].Split('=');
                if (s.Count() < 1) return "";
                s = s[0].Split('^');
                if (s.Count() < 1) return "";
                return s[0];
            }
		}

        public string First
        {
            get
            {
                string[] s = Get<string>().Split('\\');
                if (s.Count() < 1) return "";
                s = s[0].Split('=');
                if (s.Count() < 1) return "";
                s = s[0].Split('^');
                if (s.Count() < 2) return "";
                return s[1];
            }
        }
        
        public string Middle
        {
            get {
                string[] s = Get<string>().Split('\\');
                if (s.Count() < 1) return "";
                s = s[0].Split('=');
                if (s.Count() < 1) return "";
                s = s[0].Split('^');
                if (s.Count() < 3) return "";
                return s[2];
            }
		}


        public string Prefix
        {
            get {
                string[] s = Get<string>().Split('\\');
                if (s.Count() < 1) return "";
                s = s[0].Split('=');
                if (s.Count() < 1) return "";
                s = s[0].Split('^');
                if (s.Count() < 4) return "";
                return s[3];
            }
        }

        public string Suffix
        {
            get {
                string[] s = Get<string>().Split('\\');
                if (s.Count() < 1) return "";
                s = s[0].Split('=');
                if (s.Count() < 1) return "";
                s = s[0].Split('^');
                if (s.Count() < 5) return "";
                return s[4];
            }
        }
        #endregion
        #region Private Functions
        private static string ConcatName(string Last, string First, string Middle = null, string Prefix = null, string Suffix = null)
        {
            if (!String.IsNullOrEmpty(Suffix)) return Last + "^" + First + "^" + Middle + "^" + Prefix + "^" + Suffix;
            if (!String.IsNullOrEmpty(Prefix)) return Last + "^" + First + "^" + Middle + "^" + Prefix;
            if (!String.IsNullOrEmpty(Middle)) return Last + "^" + First + "^" + Middle;
            if (!String.IsNullOrEmpty(First))  return Last + "^" + First;
            return Last;
        }
        #endregion
    }

	/// <summary>Short String (SH)</summary>
	public class DicomShortString : DicomMultiStringElement {
		#region Public Constructors
        public DicomShortString(DicomTag tag, params string[] values) : base(tag, values) {
		}

        public DicomShortString(DicomTag tag, Encoding encoding, params string[] values) : base(tag, encoding, values) {
        }

		public DicomShortString(DicomTag tag, Encoding encoding, IByteBuffer data) : base (tag, encoding, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.SH; }
		}
		#endregion
	}

	/// <summary>Signed Long (SL)</summary>
	public class DicomSignedLong : DicomValueElement<int> {
		#region Public Constructors
		public DicomSignedLong(DicomTag tag, params int[] values) : base(tag, values) {
		}

		public DicomSignedLong(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.SL; }
		}
		#endregion
	}

	/// <summary>Signed Short (SS)</summary>
	public class DicomSignedShort : DicomValueElement<short> {
		#region Public Constructors
		public DicomSignedShort(DicomTag tag, params short[] values) : base(tag, values) {
		}

		public DicomSignedShort(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.SS; }
		}
		#endregion

		#region Public Members
		public override T Get<T>(int item = 0) {
			if (typeof(T) == typeof(int) || typeof(T) == typeof(int[]))
				return (T)(object)base.Get<T>(item);

			return base.Get<T>(item);
		}
		#endregion
	}

	/// <summary>Short Text (ST)</summary>
	public class DicomShortText : DicomStringElement {
		#region Public Constructors
		public DicomShortText(DicomTag tag, string value) : base(tag, value) {
		}

        public DicomShortText(DicomTag tag, Encoding encoding, string value) : base(tag, encoding, value) {
        }

		public DicomShortText(DicomTag tag, Encoding encoding, IByteBuffer data) : base(tag, encoding, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.ST; }
		}
		#endregion
	}

	/// <summary>Time (TM)</summary>
	public class DicomTime : DicomDateElement {
		#region Public Constructors
		public DicomTime(DicomTag tag, params DateTime[] values) : base(tag, PrivateDateFormats, values) {
		}

		public DicomTime(DicomTag tag, DicomDateRange range) : base(tag, PrivateDateFormats, range) {
		}

		public DicomTime(DicomTag tag, params string[] values) : base(tag, PrivateDateFormats, values) {
		}

		public DicomTime(DicomTag tag, IByteBuffer data) : base (tag, PrivateDateFormats, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.TM; }
		}

		private static string[] _formats;
		private static string[] PrivateDateFormats {
			get {
				if (_formats == null) {
					_formats = new string[31];
					_formats[0] = "HHmmss";
					_formats[1] = "HH";
					_formats[2] = "HHmm";
					_formats[3] = "HHmmssf";
					_formats[4] = "HHmmssff";
					_formats[5] = "HHmmssfff";
					_formats[6] = "HHmmssffff";
					_formats[7] = "HHmmssfffff";
					_formats[8] = "HHmmssffffff";
					_formats[9] = "HHmmss.f";
					_formats[10] = "HHmmss.ff";
					_formats[11] = "HHmmss.fff";
					_formats[12] = "HHmmss.ffff";
					_formats[13] = "HHmmss.fffff";
					_formats[14] = "HHmmss.ffffff";
					_formats[15] = "HH.mm";
					_formats[16] = "HH.mm.ss";
					_formats[17] = "HH.mm.ss.f";
					_formats[18] = "HH.mm.ss.ff";
					_formats[19] = "HH.mm.ss.fff";
					_formats[20] = "HH.mm.ss.ffff";
					_formats[21] = "HH.mm.ss.fffff";
					_formats[22] = "HH.mm.ss.ffffff";
					_formats[23] = "HH:mm";
					_formats[24] = "HH:mm:ss";
					_formats[25] = "HH:mm:ss:f";
					_formats[26] = "HH:mm:ss:ff";
					_formats[27] = "HH:mm:ss:fff";
					_formats[28] = "HH:mm:ss:ffff";
					_formats[29] = "HH:mm:ss:fffff";
					_formats[30] = "HH:mm:ss:ffffff";
					_formats[25] = "HH:mm:ss.f";
					_formats[26] = "HH:mm:ss.ff";
					_formats[27] = "HH:mm:ss.fff";
					_formats[28] = "HH:mm:ss.ffff";
					_formats[29] = "HH:mm:ss.fffff";
					_formats[30] = "HH:mm:ss.ffffff";
				}
				return _formats;
			}
		}
		#endregion
	}

	/// <summary>Unique Identifier (UI)</summary>
	public class DicomUniqueIdentifier : DicomMultiStringElement {
		#region Public Constructors
		public DicomUniqueIdentifier(DicomTag tag, params string[] values) : base(tag, values) {
		}

		public DicomUniqueIdentifier(DicomTag tag, params DicomUID[] values) : base(tag, values.Select(x => x.UID).ToArray()) {
		}

		public DicomUniqueIdentifier(DicomTag tag, params DicomTransferSyntax[] values) : base(tag, values.Select(x => x.UID.UID).ToArray()) {
		}

		public DicomUniqueIdentifier(DicomTag tag, IByteBuffer data) : base(tag, DicomEncoding.Default, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.UI; }
		}
		#endregion

		#region Public Members
		private DicomUID[] _values;
		public override T Get<T>(int item = -1) {
			if (_values == null) {
				_values = base.Get<string[]>().Select(x => DicomUID.Parse(x)).ToArray();
			}

			if (typeof(T) == typeof(DicomTransferSyntax)) {
				return (T)(object)DicomTransferSyntax.Lookup(_values[item]);
			}

			if (typeof(T) == typeof(DicomTransferSyntax[])) {
				return (T)(object)_values.Select(x => DicomTransferSyntax.Lookup(x)).ToArray();
			}

			if (typeof(T) == typeof(DicomUID) || typeof(T) == typeof(object)) {
				return (T)(object)_values[item];
			}

			if (typeof(T) == typeof(DicomUID[]) || typeof(T) == typeof(object[])) {
				return (T)(object)_values;
			}

			return base.Get<T>(item);
		}
		#endregion
	}

	/// <summary>Unsigned Long (UL)</summary>
	public class DicomUnsignedLong : DicomValueElement<uint> {
		#region Public Constructors
		public DicomUnsignedLong(DicomTag tag, params uint[] values) : base(tag, values) {
		}

		public DicomUnsignedLong(DicomTag tag, IByteBuffer data) : base(tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.UL; }
		}
		#endregion
	}

	/// <summary>Unknown (UN)</summary>
	public class DicomUnknown : DicomOtherByte {
		#region Public Constructors
		public DicomUnknown(DicomTag tag, params byte[] values) : base(tag, values) {
		}

		public DicomUnknown(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.UN; }
		}
		#endregion
	}

	/// <summary>Unsigned Short (US)</summary>
	public class DicomUnsignedShort : DicomValueElement<ushort> {
		#region Public Constructors
		public DicomUnsignedShort(DicomTag tag, params ushort[] values) : base(tag, values) {
		}

		public DicomUnsignedShort(DicomTag tag, IByteBuffer data) : base (tag, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.US; }
		}
		#endregion

		#region Public Members
		public override T Get<T>(int item = 0) {
			if (typeof(T) == typeof(int) || typeof(T) == typeof(int[]))
				return (T)(object)base.Get<T>(item);

			return base.Get<T>(item);
		}
		#endregion
	}

	/// <summary>Unlimited Text (UT)</summary>
	public class DicomUnlimitedText : DicomStringElement {
		#region Public Constructors
		public DicomUnlimitedText(DicomTag tag, string value) : base(tag, value) {
		}

		public DicomUnlimitedText(DicomTag tag, Encoding encoding, string value) : base(tag, encoding, value) {
		}

		public DicomUnlimitedText(DicomTag tag, Encoding encoding, IByteBuffer data) : base(tag, encoding, data) {
		}
		#endregion

		#region Public Properties
		public override DicomVR ValueRepresentation {
			get { return DicomVR.UT; }
		}
		#endregion
	}
}
