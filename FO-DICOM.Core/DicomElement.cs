// Copyright (c) 2012-2024 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;

namespace FellowOakDicom
{

    [DebuggerDisplay("Tag: {DicomDictionary.Default[Tag].Name} ({Tag.Group.ToString(\"X\")},{Tag.Element.ToString(\"X\")}), VR: {ValueRepresentation.Code}, VM: {Count}, Value: {Get<string>()}")]
    public abstract class DicomElement : DicomItem
    {
        protected DicomElement(DicomTag tag, IByteBuffer data)
            : base(tag)
        {
            Buffer = data;
        }

        /// <summary>Gets the number of values that the DICOM element contains.</summary>
        /// <value>Number of value items</value>
        public abstract int Count { get; }

        public IByteBuffer Buffer { get; protected set; }

        public uint Length => (uint)(Buffer?.Size ?? 0);

        public abstract T Get<T>(int item = -1);

        public override void Validate()
        {
            if (Buffer is BulkDataUriByteBuffer bulkbuffer && !bulkbuffer.IsMemory)
            {
                // skip validation in case of BulkDataUriByteBuffer, where the content has not been downloaded
                return;
            }
            ValidateString();
            ValidateVM();
        }

        protected virtual void ValidateVM()
        {
            if (!Tag.IsPrivate
                && (Count > 0))
            {
                var entry = Tag.DictionaryEntry;
                if (Count < entry.ValueMultiplicity.Minimum || Count > entry.ValueMultiplicity.Maximum)
                {
                    throw new DicomValidationException(ToString(), ValueRepresentation, $"Number of items {Count} does not match ValueMultiplicity {entry.ValueMultiplicity}");
                }
            }
        }

        protected virtual void ValidateString() { }

        public abstract bool Equals(DicomElement other);
    }

    /// <summary>
    /// Base class for a DICOM string element.
    /// </summary>
    /// <seealso cref="DicomPersonName"/>
    public abstract class DicomStringElement : DicomElement
    {

        #region FIELDS

        private string _value;

        private readonly Encoding[] _bufferEncodings;
        private Encoding[] _targetEncodings;

        #endregion

        #region Constructors

        protected DicomStringElement(DicomTag tag, string value)
            : base(tag, EmptyBuffer.Value)
        {
            _value = value ?? string.Empty;
            if (!string.IsNullOrEmpty(_value))
            {
                Buffer = new LazyByteBuffer(StringToBytes);
            }
        }

        protected DicomStringElement(DicomTag tag, Encoding[] encodings, IByteBuffer buffer)
            : base(tag, buffer)
        {
            _bufferEncodings = encodings ?? DicomEncoding.DefaultArray;
            TargetEncodings = _bufferEncodings;
        }

        #endregion

        #region Public Properties

        public Encoding[] TargetEncodings
        {
            get => ValueRepresentation.IsStringEncoded ? _targetEncodings : DicomEncoding.DefaultArray;
            set
            {
                if (ValueRepresentation.IsStringEncoded)
                {
                    if (!(Buffer is LazyByteBuffer) && (Buffer != EmptyBuffer.Value) &&
                        !value.SequenceEqual(_bufferEncodings))
                    {
                        _value = StringValue;
                        Buffer = new LazyByteBuffer(StringToBytes);
                    }

                    _targetEncodings = value;
                }
            }
        }

        public Encoding TargetEncoding
        {
            get => _targetEncodings?.FirstOrDefault() ?? DicomEncoding.Default;
            set => TargetEncodings = new[] { value };
        }

        /// <summary>Gets the number of values that the DICOM element contains.</summary>
        /// <value>Number of value items.</value>
        public override int Count => 1;

        protected string StringValue
        {
            get
            {
                if (_value == null && Buffer != null)
                {
                    _value = Buffer == EmptyBuffer.Value
                        ? string.Empty
                        : DicomEncoding.DecodeBytes(Buffer, _bufferEncodings, ValueRepresentation == DicomVR.PN)
                                .TrimEnd((char)ValueRepresentation.PaddingValue);
                }
                return _value;
            }
        }

        #endregion

        #region Public Methods

        public override T Get<T>(int item = -1)
        {
            if (typeof(T) == typeof(string) || typeof(T) == typeof(object))
            {
                return (T)((object)StringValue);
            }

            if (typeof(T) == typeof(string[]) || typeof(T) == typeof(object[]))
            {
                return (T)(object)(new string[] { StringValue });
            }

            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(DicomParseable)))
            {
                return DicomParseable.Parse<T>(StringValue);
            }

            if (typeof(T).GetTypeInfo().IsEnum)
            {
                return (T)Enum.Parse(typeof(T), StringValue.Replace("\0", string.Empty), true);
            }

            throw new InvalidCastException($"Unable to convert DICOM {ValueRepresentation.Code} value to '{typeof(T).Name}'");
        }

        #endregion

        private byte[] StringToBytes()
        {
            if (_value is null)
            {
                return Array.Empty<byte>();
            }

            // the target encoding shall only be used for encoded strings;
            // other strings must be encoded with the default encoding (ASCII)
            var bytes = ValueRepresentation.IsStringEncoded
                ? DicomEncoding.EncodeString(_value, TargetEncodings, ValueRepresentation == DicomVR.PN)
                : DicomEncoding.Default.GetBytes(_value);
            if (bytes.Length.IsOdd())
            {
                Array.Resize(ref bytes, bytes.Length + 1);
                bytes[bytes.Length - 1] = ValueRepresentation.PaddingValue;
            }

            return bytes;
        }

        protected override void ValidateString()
        {
            ValueRepresentation?.ValidateString(_value);
        }

        public override bool Equals(DicomElement other)
        {
            if (other is DicomStringElement otherStringElement)
            {
                return this.StringValue == otherStringElement.StringValue;
            }
            return false;
        }
    }

    public abstract class DicomMultiStringElement : DicomStringElement
    {

        #region FIELDS

        private int _count = -1;
        private string[] _values;

        #endregion

        #region Constructors

        protected DicomMultiStringElement(DicomTag tag, params string[] values)
            : base(tag, string.Join("\\", values))
        {
        }

        protected DicomMultiStringElement(DicomTag tag, Encoding[] encodings, IByteBuffer buffer)
            : base(tag, encodings, buffer)
        {
        }

        #endregion

        #region Public Properties

        public override int Count
        {
            get
            {
                EnsureSplitValues();
                return _count;
            }
        }

        #endregion

        #region Private Methods

        private void EnsureSplitValues()
        {
            if (_values == null || _count == -1)
            {
                _values = string.IsNullOrEmpty(StringValue)
                    ? Array.Empty<string>()
                    : StringValue.Split('\\');
                _count = _values.Length;
            }
        }

        #endregion

        #region Public Methods

        protected override void ValidateString()
        {
            EnsureSplitValues();
            _values.ToList().ForEach(ValueRepresentation.ValidateString);
        }

        public override T Get<T>(int item = -1)
        {
            EnsureSplitValues();

            if (typeof(T) == typeof(string) || typeof(T) == typeof(object))
            {
                if (item == -1) return (T)((object)StringValue);

                if (item < 0 || item >= Count) throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");

                return (T)((object)_values[item]);
            }

            if (typeof(T) == typeof(string[]) || typeof(T) == typeof(object[]))
            {
                return (T)(object)_values;
            }

            if (item == -1)
            {
                item = 0;
            }

            if (item < 0 || item >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");
            }

            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(DicomParseable)))
            {
                return DicomParseable.Parse<T>(_values[item]);
            }

            var t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            if (t.GetTypeInfo().IsEnum)
            {
                return (T)Enum.Parse(t, _values[item].Replace("\0", string.Empty), true);
            }

            throw new InvalidCastException($"Unable to convert DICOM {ValueRepresentation.Code} value to '{typeof(T).Name}'");
        }

        public override bool Equals(DicomElement other)
        {
            if (other is DicomMultiStringElement otherMultiString)
            {
                return otherMultiString.Count == this.Count && otherMultiString.StringValue == this.StringValue;
            }
            return false;
        }

        #endregion

    }

    public abstract class DicomDateElement : DicomMultiStringElement
    {
        #region FIELDS

        private static readonly CultureInfo _dicomDateElementFormat = CultureInfo.InvariantCulture;

        private const DateTimeStyles _dicomDateElementStyle = DateTimeStyles.NoCurrentDateDefault;

        private DateTime[] _values;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a <see cref="DicomDateElement"/> instance.
        /// </summary>
        /// <param name="tag">DICOM tag.</param>
        /// <param name="dateFormats">Supported date/time formats.</param>
        /// <param name="values">Values.</param>
        protected DicomDateElement(DicomTag tag, string[] dateFormats, params DateTime[] values)
            : base(tag, values.Select(x => x.ToString(dateFormats[0]).Replace(":", string.Empty)).ToArray())
        {
            DateFormats = dateFormats;
        }

        /// <summary>
        /// Initializes a <see cref="DicomDateElement"/> instance.
        /// </summary>
        /// <param name="tag">DICOM tag.</param>
        /// <param name="dateFormats">Supported date/time formats.</param>
        /// <param name="range">Date/time range.</param>
        protected DicomDateElement(DicomTag tag, string[] dateFormats, DicomDateRange range)
            : base(tag, range.ToString(dateFormats[0]).Replace(":", string.Empty))
        {
            DateFormats = dateFormats;
        }

        /// <summary>
        /// Initializes a <see cref="DicomDateElement"/> instance.
        /// </summary>
        /// <param name="tag">DICOM tag.</param>
        /// <param name="dateFormats">Supported date/time formats.</param>
        /// <param name="values">Values.</param>
        protected DicomDateElement(DicomTag tag, string[] dateFormats, params string[] values)
            : base(tag, string.Join("\\", values))
        {
            DateFormats = dateFormats;
        }

        /// <summary>
        /// Initializes a <see cref="DicomDateElement"/> instance.
        /// </summary>
        /// <param name="tag">DICOM tag.</param>
        /// <param name="dateFormats">Supported date/time formats.</param>
        /// <param name="buffer">Byte buffer from which to read values.</param>
        protected DicomDateElement(DicomTag tag, string[] dateFormats, IByteBuffer buffer)
            : base(tag, null, buffer)
        {
            DateFormats = dateFormats;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Supported date formats.
        /// </summary>
        protected string[] DateFormats { get; }

        #endregion

        #region METHODS

        /// <summary>
        /// Get element value(s).
        /// </summary>
        /// <typeparam name="T">Return value type.</typeparam>
        /// <param name="item">Item index, if applicable.</param>
        /// <returns>Value(s) of type <typeparamref name="T"/>, at position <paramref name="item"/> if applicable.</returns>
        public override T Get<T>(int item = -1)
        {
            // no need to parse DateTime values if returning string(s)
            if (typeof(T) == typeof(string) || typeof(T) == typeof(string[])) return base.Get<T>(item);

            if (typeof(T) == typeof(DicomDateRange))
            {
                string[] vals = base.Get<string>(item).Split('-');
                var range = new DicomDateRange();
                if (vals.Length >= 2)
                {
                    if (!string.IsNullOrEmpty(vals[0]))
                        range.Minimum = DateTime.ParseExact(
                            vals[0],
                            DateFormats,
                            _dicomDateElementFormat,
                            _dicomDateElementStyle);
                    if (!string.IsNullOrEmpty(vals[1]))
                        range.Maximum = DateTime.ParseExact(
                            vals[1],
                            DateFormats,
                            _dicomDateElementFormat,
                            _dicomDateElementStyle);
                }
                else if (vals.Length == 1)
                {
                    range.Minimum = DateTime.ParseExact(
                        vals[0],
                        DateFormats,
                        _dicomDateElementFormat,
                        _dicomDateElementStyle);
                    range.Maximum = range.Minimum.AddDays(1).AddMilliseconds(-1);
                }
                return (T)(object)range;
            }

            if (_values == null)
            {
                string[] vals = base.Get<string[]>();
                if (vals.Length == 1 && string.IsNullOrEmpty(vals[0]))
                {
                    _values = Array.Empty<DateTime>();
                }
                else
                {
                    _values = new DateTime[vals.Length];
                    for (int i = 0; i < vals.Length; i++)
                    {
                        _values[i] = DateTime.ParseExact(
                            vals[i],
                            DateFormats,
                            _dicomDateElementFormat,
                            _dicomDateElementStyle);
                    }
                }
            }

            if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(object))
            {
                if (item == -1) return (T)((object)_values[0]);

                if (item < 0 || item >= Count) throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");

                return (T)((object)_values[item]);
            }

            if (typeof(T) == typeof(DateTime[]) || typeof(T) == typeof(object[]))
            {
                return (T)(object)_values;
            }

            return base.Get<T>(item);
        }

        #endregion

    }

    public abstract class DicomValueElement<Tv> : DicomElement
        where Tv : struct
    {

        #region Constructors

        protected DicomValueElement(DicomTag tag, params Tv[] values)
            : this(tag, ByteConverter.ToByteBuffer<Tv>(values))
        {
        }

        protected DicomValueElement(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override int Count => (int)Buffer.Size / ValueRepresentation.UnitSize;

        #endregion

        #region Public Members

        public override T Get<T>(int item = -1)
        {
            if (item == -1) item = 0;

            if (typeof(T) == typeof(object))
            {
                if (item < 0 || item >= Count) throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");

                return (T)(object)ByteConverter.Get<Tv>(Buffer, item);
            }

            if (typeof(T) == typeof(object[]))
            {
                return (T)(object)ByteConverter.ToArray<Tv>(Buffer).Cast<object>().ToArray();
            }

            if (typeof(T) == typeof(Tv))
            {
                if (item < 0 || item >= Count) throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");

                return ByteConverter.Get<T>(Buffer, item);
            }

            if (typeof(T) == typeof(Tv[]))
            {
                // Is there a way to avoid this cast?
                return (T)(object)ByteConverter.ToArray<Tv>(Buffer);
            }

            if (typeof(T) == typeof(string))
            {
                if (item < 0 || item >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");
                }

                return (T)(object)ByteConverter.Get<Tv>(Buffer, item).ToString();
            }

            if (typeof(T) == typeof(string[]))
            {
                return (T)(object)ByteConverter.ToArray<Tv>(Buffer).Select(x => x.ToString()).ToArray();
            }

            if (typeof(T).GetTypeInfo().IsValueType)
            {
                if (item < 0 || item >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");
                }

                // If nullable, need to apply conversions on underlying type (#212)
                var t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                if (t.GetTypeInfo().IsEnum)
                {
                    var s = ByteConverter.Get<Tv>(Buffer, item).ToString();
                    return (T)Enum.Parse(t, s.Replace("\0", string.Empty), true);
                }

                return (T)Convert.ChangeType(ByteConverter.Get<Tv>(Buffer, item), t);
            }

            throw new InvalidCastException($"Unable to convert DICOM {ValueRepresentation.Code} value to '{typeof(T).Name}'");
        }

        public override bool Equals(DicomElement other)
        {
            if (other is DicomValueElement<Tv> otherValue)
            {
                return this.Buffer.Data.SequenceEqual(otherValue.Buffer.Data);
            }
            return false;
        }

        #endregion

    }

    /// <summary>Application Entity (AE)</summary>
    public class DicomApplicationEntity : DicomMultiStringElement
    {
        #region Public Constructors

        public DicomApplicationEntity(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomApplicationEntity(DicomTag tag, IByteBuffer data)
            : base(tag, null, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.AE;

        #endregion

    }

    /// <summary>Age String (AS)</summary>
    public class DicomAgeString : DicomMultiStringElement
    {

        #region Public Constructors

        public DicomAgeString(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomAgeString(DicomTag tag, IByteBuffer data)
            : base(tag, null, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.AS;

        #endregion

    }

    /// <summary>Attribute Tag (AT)</summary>
    public class DicomAttributeTag : DicomElement
    {
        #region FIELDS

        private DicomTag[] _values;

        #endregion

        #region Public Constructors

        public DicomAttributeTag(DicomTag tag, params DicomTag[] values)
            : base(tag, EmptyBuffer.Value)
        {
            Values = values;
        }

        public DicomAttributeTag(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override int Count => (int)Buffer.Size / 4;

        public override DicomVR ValueRepresentation => DicomVR.AT;

        public IEnumerable<DicomTag> Values
        {
            get
            {
                if (_values == null)
                {
                    var values = new List<DicomTag>();
                    var parts = ByteConverter.ToArray<ushort>(Buffer);
                    for (int i = 0; i < parts.Length; i += 2)
                    {
                        var group = parts[i + 0];
                        var element = parts[i + 1];
                        values.Add(DicomTagsIndex.LookupOrCreate(group, element) ?? new DicomTag(group, element));
                    }
                    _values = values.ToArray();
                }
                return _values;
            }
            private set
            {
                _values = value.ToArray();
                int length = _values.Length * 4;
                byte[] buffer = new byte[length];
                for (int i = 0; i < _values.Length; i++)
                {
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

        public override T Get<T>(int item = -1)
        {
            if (item == -1) item = 0;
            var tags = Values.ToArray();

            if (typeof(T) == typeof(DicomTag)) return (T)(object)tags[item];

            if (typeof(T) == typeof(DicomTag[])) return (T)(object)tags;

            if (typeof(T) == typeof(string)) return (T)(object)tags[item].ToString();

            if (typeof(T) == typeof(string[])) return (T)(object)tags.Select(x => x.ToString()).ToArray();

            throw new InvalidCastException(
                $"Unable to convert DICOM {ValueRepresentation.Code} value to '{typeof(T).Name}'");
        }

        public override bool Equals(DicomElement other)
        {
            if (other is DicomAttributeTag otherAttribute)
            {
                return (this.Values == null && otherAttribute.Values == null)
                    || (this.Values != null && otherAttribute.Values != null && 
                        this.Values.SequenceEqual(otherAttribute.Values));
            }
            return false;
        }

        #endregion
    }

    /// <summary>Code String (CS)</summary>
    public class DicomCodeString : DicomMultiStringElement
    {
        #region Public Constructors

        public DicomCodeString(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomCodeString(DicomTag tag, IByteBuffer data)
            : base(tag, null, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.CS;

        #endregion

    }

    /// <summary>Date (DA)</summary>
    public class DicomDate : DicomDateElement
    {

        #region FIELDS

        private static string[] _formats;

        #endregion

        #region Public Constructors

        public DicomDate(DicomTag tag, params DateTime[] values)
            : base(tag, PrivateDateFormats, values)
        {
        }

        public DicomDate(DicomTag tag, DicomDateRange range)
            : base(tag, PrivateDateFormats, range)
        {
        }

        public DicomDate(DicomTag tag, params string[] values)
            : base(tag, PrivateDateFormats, values)
        {
        }

        public DicomDate(DicomTag tag, IByteBuffer data)
            : base(tag, PrivateDateFormats, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.DA;

        private static string[] PrivateDateFormats
        {
            get
            {
                if (_formats == null)
                {
                    _formats = new[]
                                   {
                                        "yyyyMMdd",
                                        "yyyy.MM.dd",
                                        "yyyy/MM/dd",
                                        "yyyy",
                                        "yyyyMM",
                                        "yyyy.MM"
                                   };
                }
                return _formats;
            }
        }

        #endregion

    }

    /// <summary>Decimal String (DS)</summary>
    public class DicomDecimalString : DicomMultiStringElement
    {

        #region FIELDS

        private decimal[] _values;

        #endregion

        #region Public Constructors

        public DicomDecimalString(DicomTag tag, params decimal[] values)
            : base(tag, values.Select(x => ToDecimalString(x)).ToArray())
        {
        }

        public DicomDecimalString(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomDecimalString(DicomTag tag, IByteBuffer data)
            : base(tag, null, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.DS;

        #endregion

        #region Public Members

        public override T Get<T>(int item = -1)
        {
            // no need to parse values if returning string(s)
            if (typeof(T) == typeof(string) || typeof(T) == typeof(string[])) return base.Get<T>(item);

            if (_values == null)
            {
                _values =
                    base.Get<string[]>()
                        .Select(x => decimal.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture))
                        .ToArray();
            }

            if (typeof(T).GetTypeInfo().IsArray)
            {
                var t = typeof(T).GetElementType();

                if (t == typeof(decimal)) return (T)(object)_values;

                var tu = Nullable.GetUnderlyingType(t) ?? t;
                var tmp = _values.Select(x => Convert.ChangeType(x, tu));

                if (t == typeof(object)) return (T)(object)tmp.ToArray();
                if (t == typeof(double)) return (T)(object)tmp.Cast<double>().ToArray();
                if (t == typeof(float)) return (T)(object)tmp.Cast<float>().ToArray();
                if (t == typeof(long)) return (T)(object)tmp.Cast<long>().ToArray();
                if (t == typeof(int)) return (T)(object)tmp.Cast<int>().ToArray();
                if (t == typeof(short)) return (T)(object)tmp.Cast<short>().ToArray();
                if (t == typeof(decimal?)) return (T)(object)tmp.Cast<decimal?>().ToArray();
                if (t == typeof(double?)) return (T)(object)tmp.Cast<double?>().ToArray();
                if (t == typeof(float?)) return (T)(object)tmp.Cast<float?>().ToArray();
                if (t == typeof(long?)) return (T)(object)tmp.Cast<long?>().ToArray();
                if (t == typeof(int?)) return (T)(object)tmp.Cast<int?>().ToArray();
                if (t == typeof(short?)) return (T)(object)tmp.Cast<short?>().ToArray();
            }
            else if (typeof(T).GetTypeInfo().IsValueType || typeof(T) == typeof(object))
            {
                if (item == -1) item = 0;
                if (item < 0 || item >= Count) throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");

                // If nullable, need to apply conversions on underlying type (#212)
                var t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                return (T)Convert.ChangeType(_values[item], t);
            }

            return base.Get<T>(item);
        }

        public static string ToDecimalString(decimal value)
        {
            var valueString = value.ToString(CultureInfo.InvariantCulture);
            if (valueString.Length > 16)
            {
                valueString = value.ToString("G10", CultureInfo.InvariantCulture);
            }
            return valueString;
        }

        #endregion

    }

    /// <summary>Date Time (DT)</summary>
    public class DicomDateTime : DicomDateElement
    {
        #region Public Constructors

        public DicomDateTime(DicomTag tag, params DateTime[] values)
            : base(tag, PrivateDateFormats, values)
        {
        }

        public DicomDateTime(DicomTag tag, DicomDateRange range)
            : base(tag, PrivateDateFormats, range)
        {
        }

        public DicomDateTime(DicomTag tag, params string[] values)
            : base(tag, PrivateDateFormats, values)
        {
        }

        public DicomDateTime(DicomTag tag, IByteBuffer data)
            : base(tag, PrivateDateFormats, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.DT;

        private static string[] _formats;

        private static string[] PrivateDateFormats
        {
            get
            {
                if (_formats == null)
                {
                    _formats = new[]
                    {
                        "yyyyMMddHHmmss",
                        "yyyyMMddHHmmsszzz",
                        "yyyyMMddHHmmsszz",
                        "yyyyMMddHHmmssz",
                        "yyyyMMddHHmmss.ffffff",
                        "yyyyMMddHHmmss.fffff",
                        "yyyyMMddHHmmss.ffff",
                        "yyyyMMddHHmmss.fff",
                        "yyyyMMddHHmmss.ff",
                        "yyyyMMddHHmmss.f",
                        "yyyyMMddHHmm",
                        "yyyyMMddHH",
                        "yyyyMMdd",
                        "yyyyMM",
                        "yyyy",
                        "yyyyMMddHHmmss.ffffffzzz",
                        "yyyyMMddHHmmss.fffffzzz",
                        "yyyyMMddHHmmss.ffffzzz",
                        "yyyyMMddHHmmss.fffzzz",
                        "yyyyMMddHHmmss.ffzzz",
                        "yyyyMMddHHmmss.fzzz",
                        "yyyyMMddHHmmzzz",
                        "yyyyMMddHHzzz",
                        "yyyy.MM.dd",
                        "yyyy/MM/dd"
                    };
                }
                return _formats;
            }
        }

        #endregion
    }

    /// <summary>Floating Point Double (FD)</summary>
    public class DicomFloatingPointDouble : DicomValueElement<double>
    {
        #region Public Constructors

        public DicomFloatingPointDouble(DicomTag tag, params double[] values)
            : base(tag, values)
        {
        }

        public DicomFloatingPointDouble(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.FD;

        #endregion
    }

    /// <summary>Floating Point Single (FL)</summary>
    public class DicomFloatingPointSingle : DicomValueElement<float>
    {
        #region Public Constructors

        public DicomFloatingPointSingle(DicomTag tag, params float[] values)
            : base(tag, values)
        {
        }

        public DicomFloatingPointSingle(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.FL;

        #endregion
    }

    /// <summary>Integer String (IS)</summary>
    public class DicomIntegerString : DicomMultiStringElement
    {
        #region Public Constructors

        public DicomIntegerString(DicomTag tag, params int[] values)
            : base(tag, values.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray())
        {
        }

        public DicomIntegerString(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomIntegerString(DicomTag tag, IByteBuffer data)
            : base(tag, null, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.IS;

        #endregion

        #region Public Members

        private int[] _values;

        public override T Get<T>(int item = -1)
        {
            // no need to parse values if returning string(s)
            if (typeof(T) == typeof(string) || typeof(T) == typeof(string[])) return base.Get<T>(item);

            // Normalize item argument if necessary (#231)
            if (item == -1)
            {
                item = 0;
            }

            if (_values == null)
            {
                _values = base.Get<string[]>().Select(x => int.Parse(x, NumberStyles.Integer | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture)).ToArray();
            }

            if (typeof(T) == typeof(int) || typeof(T) == typeof(object))
            {
                return (T)(object)_values[item];
            }

            if (typeof(T).GetTypeInfo().IsArray)
            {
                var t = typeof(T).GetElementType();

                if (t == typeof(int)) return (T)(object)_values;

                var tu = Nullable.GetUnderlyingType(t) ?? t;
                var tmp = _values.Select(x => Convert.ChangeType(x, tu));

                if (t == typeof(object)) return (T)(object)tmp.ToArray();
                if (t == typeof(decimal)) return (T)(object)tmp.Cast<decimal>().ToArray();
                if (t == typeof(double)) return (T)(object)tmp.Cast<double>().ToArray();
                if (t == typeof(float)) return (T)(object)tmp.Cast<float>().ToArray();
                if (t == typeof(long)) return (T)(object)tmp.Cast<long>().ToArray();
                if (t == typeof(int)) return (T)(object)tmp.Cast<int>().ToArray();
                if (t == typeof(short)) return (T)(object)tmp.Cast<short>().ToArray();
                if (t == typeof(byte)) return (T)(object)tmp.Cast<byte>().ToArray();
                if (t == typeof(ulong)) return (T)(object)tmp.Cast<ulong>().ToArray();
                if (t == typeof(uint)) return (T)(object)tmp.Cast<uint>().ToArray();
                if (t == typeof(ushort)) return (T)(object)tmp.Cast<ushort>().ToArray();
                if (t == typeof(decimal?)) return (T)(object)tmp.Cast<decimal?>().ToArray();
                if (t == typeof(double?)) return (T)(object)tmp.Cast<double?>().ToArray();
                if (t == typeof(float?)) return (T)(object)tmp.Cast<float?>().ToArray();
                if (t == typeof(long?)) return (T)(object)tmp.Cast<long?>().ToArray();
                if (t == typeof(int?)) return (T)(object)tmp.Cast<int?>().ToArray();
                if (t == typeof(short?)) return (T)(object)tmp.Cast<short?>().ToArray();
                if (t == typeof(byte?)) return (T)(object)tmp.Cast<byte?>().ToArray();
                if (t == typeof(ulong?)) return (T)(object)tmp.Cast<ulong?>().ToArray();
                if (t == typeof(uint?)) return (T)(object)tmp.Cast<uint?>().ToArray();
                if (t == typeof(ushort?)) return (T)(object)tmp.Cast<ushort?>().ToArray();
            }
            else if (typeof(T).GetTypeInfo().IsValueType)
            {
                if (item < 0 || item >= Count) throw new ArgumentOutOfRangeException(nameof(item), "Index is outside the range of available value items");

                // If nullable, need to apply conversions on underlying type (#212)
                var t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

                if (t.GetTypeInfo().IsEnum)
                {
                    return (T)Enum.ToObject(t, _values[item]);
                }

                return (T)Convert.ChangeType(_values[item], t);
            }

            return base.Get<T>(item);
        }

        #endregion
    }

    /// <summary>Long String (LO)</summary>
    public class DicomLongString : DicomMultiStringElement
    {
        #region Public Constructors

        public DicomLongString(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomLongString(DicomTag tag, Encoding[] encoding, IByteBuffer data)
            : base(tag, encoding, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.LO;

        #endregion
    }

    /// <summary>Long Text (LT)</summary>
    public class DicomLongText : DicomStringElement
    {
        #region Public Constructors

        public DicomLongText(DicomTag tag, string value)
            : base(tag, value)
        {
        }

        public DicomLongText(DicomTag tag, Encoding[] encodings, IByteBuffer data)
            : base(tag, encodings, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.LT;

        #endregion
    }

    /// <summary>Other Byte (OB)</summary>
    public class DicomOtherByte : DicomValueElement<byte>
    {
        #region Public Constructors

        public DicomOtherByte(DicomTag tag, params byte[] values)
            : base(tag, values)
        {
        }

        public DicomOtherByte(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.OB;

        #endregion

        #region Public Methods

        public override T Get<T>(int item = -1)
        {
            if (!typeof(T).IsArray && item == -1) item = 0;

            if (typeof(T) == typeof(short)) return (T)(object)ByteConverter.Get<short>(Buffer, item);

            if (typeof(T) == typeof(short[])) return (T)(object)ByteConverter.ToArray<short>(Buffer);

            if (typeof(T) == typeof(ushort)) return (T)(object)ByteConverter.Get<ushort>(Buffer, item);

            if (typeof(T) == typeof(ushort[])) return (T)(object)ByteConverter.ToArray<ushort>(Buffer);

            if (typeof(T) == typeof(int)) return (T)(object)ByteConverter.Get<int>(Buffer, item);

            if (typeof(T) == typeof(int[])) return (T)(object)ByteConverter.ToArray<int>(Buffer);

            if (typeof(T) == typeof(uint)) return (T)(object)ByteConverter.Get<uint>(Buffer, item);

            if (typeof(T) == typeof(uint[])) return (T)(object)ByteConverter.ToArray<uint>(Buffer);

            if (typeof(T) == typeof(float)) return (T)(object)ByteConverter.Get<float>(Buffer, item);

            if (typeof(T) == typeof(float[])) return (T)(object)ByteConverter.ToArray<float>(Buffer);

            if (typeof(T) == typeof(double)) return (T)(object)ByteConverter.Get<double>(Buffer, item);

            if (typeof(T) == typeof(double[])) return (T)(object)ByteConverter.ToArray<double>(Buffer);

            return base.Get<T>(item);
        }


        public override bool Equals(DicomElement other)
        {
            if (other is DicomOtherByte otherByte)
            {
                return this.Count == otherByte.Count;
            }
            return false;
        }

        #endregion

        protected override void ValidateVM()
        {
            // do not check length of items
        }

    }

    /// <summary>Other Word (OW)</summary>
    public class DicomOtherWord : DicomValueElement<ushort>
    {
        #region Public Constructors

        public DicomOtherWord(DicomTag tag, params ushort[] values)
            : base(tag, values)
        {
        }

        public DicomOtherWord(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.OW;

        public override bool Equals(DicomElement other)
        {
            if (other is DicomOtherWord otherByte)
            {
                return this.Count == otherByte.Count;
            }
            return false;
        }

        #endregion

        protected override void ValidateVM()
        {
            // do not check length of items
        }

    }

    /// <summary>Other Long (OL)</summary>
    public class DicomOtherLong : DicomValueElement<uint>
    {
        #region Public Constructors

        public DicomOtherLong(DicomTag tag, params uint[] values)
            : base(tag, values)
        {
        }

        public DicomOtherLong(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.OL;

        #endregion

        protected override void ValidateVM()
        {
            // do not check length of items
        }
    }

    /// <summary>Other Double (OD)</summary>
    public class DicomOtherDouble : DicomValueElement<double>
    {
        #region Public Constructors

        public DicomOtherDouble(DicomTag tag, params double[] values)
            : base(tag, values)
        {
        }

        public DicomOtherDouble(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.OD;

        #endregion

        protected override void ValidateVM()
        {
            // do not check length of items
        }

    }

    /// <summary>Other Float (OF)</summary>
    public class DicomOtherFloat : DicomValueElement<float>
    {
        #region Public Constructors

        public DicomOtherFloat(DicomTag tag, params float[] values)
            : base(tag, values)
        {
        }

        public DicomOtherFloat(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.OF;

        #endregion

        protected override void ValidateVM()
        {
            // do not check length of items
        }
    }

    /// <summary>Other Very Long (OV)</summary>
    public class DicomOtherVeryLong : DicomValueElement<ulong>
    {
        #region Public Constructors

        public DicomOtherVeryLong(DicomTag tag, params ulong[] values)
            : base(tag, values)
        {
        }

        public DicomOtherVeryLong(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.OV;

        #endregion

        protected override void ValidateVM()
        {
            // do not check length of items
        }
    }

    /// <summary>Person Name (PN)</summary>
    public sealed class DicomPersonName : DicomMultiStringElement
    {
        #region Public Constructors

        public DicomPersonName(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomPersonName(
            DicomTag tag,
            string Last,
            string First,
            string Middle = null,
            string Prefix = null,
            string Suffix = null)
            : base(tag, ConcatName(Last, First, Middle, Prefix, Suffix))
        {
        }

        public DicomPersonName(DicomTag tag, Encoding[] encodings, IByteBuffer data)
            : base(tag, encodings, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.PN;

        /// <summary> Family name or empty string. </summary>
        public string Last => GetComponent(0);

        /// <summary> Given name or empty string. </summary>
        public string First => GetComponent(1);

        /// <summary> Middle name or empty string. </summary>
        public string Middle => GetComponent(2);

        /// <summary> Name prefix or empty string. </summary>
        public string Prefix => GetComponent(3);

        /// <summary> Name suffix or empty string. </summary>
        public string Suffix => GetComponent(4);

        #endregion

        #region Private Functions

        private string GetComponent(int index)
        {
            string[] s = Get<string>()?.Split('\\');
            if (s == null || !s.Any())
            {
                return "";
            }

            s = s[0].Split('=');
            if (!s.Any())
            {
                return "";
            }

            s = s[0].Split('^');
            if (s.Count() < index + 1)
            {
                return "";
            }

            return s[index];
        }

        private static string ConcatName(
            string Last,
            string First,
            string Middle = null,
            string Prefix = null,
            string Suffix = null)
        {
            if (!string.IsNullOrEmpty(Suffix)) return Last + "^" + First + "^" + Middle + "^" + Prefix + "^" + Suffix;
            if (!string.IsNullOrEmpty(Prefix)) return Last + "^" + First + "^" + Middle + "^" + Prefix;
            if (!string.IsNullOrEmpty(Middle)) return Last + "^" + First + "^" + Middle;
            if (!string.IsNullOrEmpty(First)) return Last + "^" + First;
            return Last;
        }

        #endregion

        #region Public Functions

        public static bool HaveSameContent(DicomPersonName nameA, DicomPersonName nameB)
        {
            if (nameA == null && nameB == null) { return true; } // both are null
            if (nameA == null || nameB == null) { return false; } // one us null but the other is not
            return string.Compare(nameA.Last.ToLowerInvariant(), nameB.Last.ToLowerInvariant(), StringComparison.CurrentCultureIgnoreCase) == 0
                && string.Compare(nameA.First.ToLowerInvariant(), nameB.First.ToLowerInvariant(), StringComparison.CurrentCultureIgnoreCase) == 0
                && string.Compare(nameA.Middle.ToLowerInvariant(), nameB.Middle.ToLowerInvariant(), StringComparison.CurrentCultureIgnoreCase) == 0
                && string.Compare(nameA.Prefix.ToLowerInvariant(), nameB.Prefix.ToLowerInvariant(), StringComparison.CurrentCultureIgnoreCase) == 0
                && string.Compare(nameA.Suffix.ToLowerInvariant(), nameB.Suffix.ToLowerInvariant(), StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        #endregion

    }

    /// <summary>Short String (SH)</summary>
    public class DicomShortString : DicomMultiStringElement
    {
        #region Public Constructors

        public DicomShortString(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomShortString(DicomTag tag, Encoding[] encodings, IByteBuffer data)
            : base(tag, encodings, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.SH;

        #endregion
    }

    /// <summary>Signed Long (SL)</summary>
    public class DicomSignedLong : DicomValueElement<int>
    {
        #region Public Constructors

        public DicomSignedLong(DicomTag tag, params int[] values)
            : base(tag, values)
        {
        }

        public DicomSignedLong(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.SL;

        #endregion
    }

    /// <summary>Signed Short (SS)</summary>
    public class DicomSignedShort : DicomValueElement<short>
    {
        #region Public Constructors

        public DicomSignedShort(DicomTag tag, params short[] values)
            : base(tag, values)
        {
        }

        public DicomSignedShort(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.SS;

        #endregion

        #region Public Members

        public override T Get<T>(int item = -1)
        {
            if (typeof(T) == typeof(int) || typeof(T) == typeof(int[])) return (T)(object)base.Get<T>(item);

            return base.Get<T>(item);
        }

        #endregion
    }

    /// <summary>Short Text (ST)</summary>
    public class DicomShortText : DicomStringElement
    {
        #region Public Constructors

        public DicomShortText(DicomTag tag, string value)
            : base(tag, value)
        {
        }

        public DicomShortText(DicomTag tag, Encoding[] encodings, IByteBuffer data)
            : base(tag, encodings, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.ST;

        #endregion
    }

    /// <summary>Signed Very Long (SV)</summary>
    public class DicomSignedVeryLong : DicomValueElement<long>
    {
        #region Public Constructors

        public DicomSignedVeryLong(DicomTag tag, params long[] values)
            : base(tag, values)
        {
        }

        public DicomSignedVeryLong(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.SV;

        #endregion
    }

    /// <summary>Time (TM)</summary>
    public class DicomTime : DicomDateElement
    {
        #region Public Constructors

        public DicomTime(DicomTag tag, params DateTime[] values)
            : base(tag, PrivateDateFormats, values)
        {
        }

        public DicomTime(DicomTag tag, DicomDateRange range)
            : base(tag, PrivateDateFormats, range)
        {
        }

        public DicomTime(DicomTag tag, params string[] values)
            : base(tag, PrivateDateFormats, values)
        {
        }

        public DicomTime(DicomTag tag, IByteBuffer data)
            : base(tag, PrivateDateFormats, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.TM;

        private static string[] _formats;

        private static string[] PrivateDateFormats
        {
            get
            {
                if (_formats == null)
                {
                    _formats = new[]
                                {
                                    "HHmmss",
                                    "HH",
                                    "HHmm",
                                    "HHmmssf",
                                    "HHmmssff",
                                    "HHmmssfff",
                                    "HHmmssffff",
                                    "HHmmssfffff",
                                    "HHmmssffffff",
                                    "HHmmss.f",
                                    "HHmmss.ff",
                                    "HHmmss.fff",
                                    "HHmmss.ffff",
                                    "HHmmss.fffff",
                                    "HHmmss.ffffff",
                                    "HH.mm",
                                    "HH.mm.ss",
                                    "HH.mm.ss.f",
                                    "HH.mm.ss.ff",
                                    "HH.mm.ss.fff",
                                    "HH.mm.ss.ffff",
                                    "HH.mm.ss.fffff",
                                    "HH.mm.ss.ffffff",
                                    "HH:mm",
                                    "HH:mm:ss",
                                    "HH:mm:ss:f",
                                    "HH:mm:ss:ff",
                                    "HH:mm:ss:fff",
                                    "HH:mm:ss:ffff",
                                    "HH:mm:ss:fffff",
                                    "HH:mm:ss:ffffff",
                                    "HH:mm:ss.f",
                                    "HH:mm:ss.ff",
                                    "HH:mm:ss.fff",
                                    "HH:mm:ss.ffff",
                                    "HH:mm:ss.fffff",
                                    "HH:mm:ss.ffffff"
                                };
                }
                return _formats;
            }
        }

        #endregion
    }

    /// <summary>Unlimited Characters (UC)</summary>
    public class DicomUnlimitedCharacters : DicomMultiStringElement
    {
        #region Public Constructors

        public DicomUnlimitedCharacters(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomUnlimitedCharacters(DicomTag tag, Encoding[] encodings, IByteBuffer data)
            : base(tag, encodings, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.UC;

        #endregion

        protected override void ValidateVM()
        {
            // do not validate the number of items
        }

    }

    /// <summary>Unique Identifier (UI)</summary>
    public class DicomUniqueIdentifier : DicomMultiStringElement
    {

        #region FIELDS

        private DicomUID[] _values;

        #endregion

        #region Public Constructors

        public DicomUniqueIdentifier(DicomTag tag, params string[] values)
            : base(tag, values)
        {
        }

        public DicomUniqueIdentifier(DicomTag tag, params DicomUID[] values)
            : base(tag, values.Select(x => x.UID).ToArray())
        {
        }

        public DicomUniqueIdentifier(DicomTag tag, params DicomTransferSyntax[] values)
            : base(tag, values.Select(x => x.UID.UID).ToArray())
        {
        }

        public DicomUniqueIdentifier(DicomTag tag, IByteBuffer data)
            : base(tag, null, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.UI;

        #endregion

        #region Public Members

        public override T Get<T>(int item = -1)
        {
            if (_values == null)
            {
                _values = base.Get<string[]>().Select(s => DicomUID.Parse(s)).ToArray();
            }

            if (typeof(T) == typeof(DicomTransferSyntax))
            {
                return (T)(object)DicomTransferSyntax.Lookup(_values[item]);
            }

            if (typeof(T) == typeof(DicomTransferSyntax[]))
            {
                return (T)(object)_values.Select(DicomTransferSyntax.Lookup).ToArray();
            }

            if (typeof(T) == typeof(DicomUID) || typeof(T) == typeof(object))
            {
                return (T)(object)_values[item];
            }

            if (typeof(T) == typeof(DicomUID[]) || typeof(T) == typeof(object[]))
            {
                return (T)(object)_values;
            }

            return base.Get<T>(item);
        }

        #endregion
    }

    /// <summary>Unsigned Long (UL)</summary>
    public class DicomUnsignedLong : DicomValueElement<uint>
    {
        #region Public Constructors

        public DicomUnsignedLong(DicomTag tag, params uint[] values)
            : base(tag, values)
        {
        }

        public DicomUnsignedLong(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.UL;

        #endregion
    }

    /// <summary>Unknown (UN)</summary>
    public class DicomUnknown : DicomOtherByte
    {
        #region Public Constructors

        public DicomUnknown(DicomTag tag, params byte[] values)
            : base(tag, values)
        {
        }

        public DicomUnknown(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.UN;

        #endregion

        protected override void ValidateVM()
        {
            // do not validate number of items
        }

    }

    /// <summary>Universal Resource Identifier or Universal Resource Locator (UR)</summary>
    public class DicomUniversalResource : DicomStringElement
    {
        #region Public Constructors

        public DicomUniversalResource(DicomTag tag, string value)
            : base(tag, value)
        {
        }

        public DicomUniversalResource(DicomTag tag, Encoding[] encodings, IByteBuffer data)
            : base(tag, encodings, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.UR;

        #endregion
    }

    /// <summary>Unsigned Short (US)</summary>
    public class DicomUnsignedShort : DicomValueElement<ushort>
    {
        #region Public Constructors

        public DicomUnsignedShort(DicomTag tag, params ushort[] values)
            : base(tag, values)
        {
        }

        public DicomUnsignedShort(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.US;

        #endregion

        #region Public Members

        public override T Get<T>(int item = -1)
        {
            if (typeof(T) == typeof(int) || typeof(T) == typeof(int[])) return (T)(object)base.Get<T>(item);

            return base.Get<T>(item);
        }

        #endregion
    }

    /// <summary>Unlimited Text (UT)</summary>
    public class DicomUnlimitedText : DicomStringElement
    {
        #region Public Constructors

        public DicomUnlimitedText(DicomTag tag, string value)
            : base(tag, value)
        {
        }

        public DicomUnlimitedText(DicomTag tag, Encoding[] encodings, IByteBuffer data)
            : base(tag, encodings, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.UT;

        #endregion
    }

    /// <summary>Unsigned Very Long (UV)</summary>
    public class DicomUnsignedVeryLong : DicomValueElement<ulong>
    {
        #region Public Constructors

        public DicomUnsignedVeryLong(DicomTag tag, params ulong[] values)
            : base(tag, values)
        {
        }

        public DicomUnsignedVeryLong(DicomTag tag, IByteBuffer data)
            : base(tag, data)
        {
        }

        #endregion

        #region Public Properties

        public override DicomVR ValueRepresentation => DicomVR.UV;

        #endregion
    }
}
