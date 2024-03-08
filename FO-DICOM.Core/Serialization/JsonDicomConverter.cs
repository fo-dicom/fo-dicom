// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FellowOakDicom.Serialization
{

    [Obsolete("Please use DicomJsonConverter instead.")]
    public class DicomArrayJsonConverter : JsonConverter<DicomDataset[]>
    {
        private readonly bool _writeTagsAsKeywords;

        /// <summary>
        /// Initialize the JsonDicomConverter.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        public DicomArrayJsonConverter()
            : this(false)
        {
        }

        /// <summary>
        /// Initialize the JsonDicomConverter.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        public DicomArrayJsonConverter(bool writeTagsAsKeywords)
        {
            _writeTagsAsKeywords = writeTagsAsKeywords;
        }

        public override DicomDataset[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var datasetList = new List<DicomDataset>();
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                return datasetList.ToArray();
            }
            reader.Read();
            var conv = new DicomJsonConverter(writeTagsAsKeywords: _writeTagsAsKeywords);
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                DicomDataset ds;
                switch (reader.TokenType)
                {
                    case JsonTokenType.StartObject:
                        ds = conv.Read(ref reader, typeToConvert, options);
                        reader.AssumeAndSkip(JsonTokenType.EndObject);
                        break;
                    case JsonTokenType.Null:
                        ds = null;
                        reader.Read();
                        break;
                    default:
                        throw new JsonException($"Expected either the start of an object or null but found '{reader.TokenType}'.");
                }

                datasetList.Add(ds);
            }
            reader.Read();
            return datasetList.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, DicomDataset[] value, JsonSerializerOptions options)
        {
            var conv = new DicomJsonConverter(writeTagsAsKeywords: _writeTagsAsKeywords);
            writer.WriteStartArray();
            foreach (var ds in value)
            {
                conv.Write(writer, ds, options);
            }
            writer.WriteEndArray();
        }

    }

    /// <summary>
    /// Defines the way DICOM numbers (tags with VR: IS, DS, SV and UV) should be serialized
    /// </summary>
    public enum NumberSerializationMode
    {
        /// <summary>
        /// Always serialize DICOM numbers (tags with VR: IS, DS, SV and UV) as JSON numbers.
        /// ⚠️ This will throw FormatException when a number can't be parsed!
        /// i.e.: "00081160":{"vr":"IS","Value":[76]}
        /// </summary>
        AsNumber,

        /// <summary>
        /// Always serialize DICOM numbers (tags with VR: IS, DS, SV and UV) as JSON strings.
        /// i.e.: "00081160":{"vr":"IS","Value":["76"]}
        /// </summary>
        AsString,

        /// <summary>
        /// Try to serialize DICOM numbers (tags with VR: IS, DS, SV and UV) as JSON numbers. If not parsable as a number, defaults back to a JSON string.
        /// This won't throw an error in case a number can't be parsed. It just returns the value as a JSON string.
        /// i.e.: "00081160":{"vr":"IS","Value":[76]}
        /// or "00081160":{"vr":"IS","Value":["A non parsable value"]}
        /// </summary>
        PreferablyAsNumber
    }

    /// <summary>
    /// Converts a DicomDataset object to and from JSON using the System.Text.Json library
    /// </summary>
    public class DicomJsonConverter : JsonConverter<DicomDataset>
    {

        private readonly bool _writeTagsAsKeywords;
        private readonly bool _autoValidate;
        private readonly NumberSerializationMode _numberSerializationMode;
        private readonly static Encoding[] _jsonTextEncodings = { Encoding.UTF8 };
        private readonly static char _personNameComponentGroupDelimiter = '=';
        private readonly static string[] _personNameComponentGroupNames = { "Alphabetic", "Ideographic", "Phonetic" };

        private delegate T GetValue<out T>(Utf8JsonReader reader);
        private delegate bool TryParse<T>(string value, out T parsed);
        private delegate void WriteValue<in T>(Utf8JsonWriter writer, T value);


        /// <summary>
        /// Initialize the JsonDicomConverter.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        /// <param name="autoValidate">Whether the content of DicomItems shall be validated when deserializing.</param>
        /// <param name="numberSerializationMode">Defines how numbers should be serialized. Default 'AsNumber', will throw errors when a number is not parsable.</param>
        public DicomJsonConverter(bool writeTagsAsKeywords = false, bool autoValidate = true, NumberSerializationMode numberSerializationMode = NumberSerializationMode.AsNumber)
        {
            _writeTagsAsKeywords = writeTagsAsKeywords;
            _autoValidate = autoValidate;
            _numberSerializationMode = numberSerializationMode;
        }

        /// <summary>
        /// With his option enabled, Dicom tag keyword will be written as a
        /// distinct Json attribute. 
        /// Note! This is non-standard and may break parsers!
        /// </summary>
        public bool WriteKeyword { get; set; } = false;

        /// <summary>
        /// With his option enabled, Dicom tag name will be written as a
        /// distinct Json attribute. 
        /// Note! This is non-standard and may break parsers!
        /// </summary>
        public bool WriteName { get; set; } = false;

        #region JsonConverter overrides


        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Text.Json.Utf8JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        public override void Write(Utf8JsonWriter writer, DicomDataset value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            foreach (var item in value)
            {
                if (((uint)item.Tag & 0xffff) == 0)
                {
                    // Group length (gggg,0000) attributes shall not be included in a DICOM JSON Model object.
                    continue;
                }

                // Unknown or masked tags cannot be written as keywords
                var unknown = item.Tag.DictionaryEntry == null
                              || string.IsNullOrWhiteSpace(item.Tag.DictionaryEntry.Keyword)
                              ||
                              (item.Tag.DictionaryEntry.MaskTag != null &&
                               item.Tag.DictionaryEntry.MaskTag.Mask != 0xffffffff);
                if (_writeTagsAsKeywords && !unknown)
                {
                    writer.WritePropertyName(item.Tag.DictionaryEntry.Keyword);
                }
                else
                {
                    writer.WritePropertyName(item.Tag.Group.ToString("X4") + item.Tag.Element.ToString("X4"));
                }

                WriteJsonDicomItem(writer, item, options);
            }
            writer.WriteEndObject();
        }


        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Text.Json.Utf8JsonReader"/> to read from.</param>
        /// <param name="typeToConvert">Type of the object.</param>
        /// <param name="options">Options to apply while reading.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override DicomDataset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dataset = ReadJsonDataset(ref reader);
            return dataset;
        }


        private DicomDataset ReadJsonDataset(ref Utf8JsonReader reader)
        {
            var dataset = _autoValidate
                ? new DicomDataset()
                : new DicomDataset().NotValidated();
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"Expected the start of an object but found '{reader.TokenType}'.");
            }
            reader.Read();

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                reader.Assume(JsonTokenType.PropertyName);
                var tagstr = reader.GetString();
                DicomTag tag = ParseTag(tagstr);
                reader.Read(); // move to value
                var item = ReadJsonDicomItem(tag, ref reader);
                dataset.Add(item);
            }

            foreach (var item in dataset)
            {
                if (item.Tag.IsPrivate && ((item.Tag.Element & 0xff00) != 0))
                {
                    var privateCreatorTag = new DicomTag(item.Tag.Group, (ushort)(item.Tag.Element >> 8));

                    if (dataset.Contains(privateCreatorTag))
                    {
                        var privateCreatorItem = dataset.GetDicomItem<DicomElement>(privateCreatorTag);

                        item.Tag.PrivateCreator = new DicomPrivateCreator(privateCreatorItem.Get<string>());
                    }
                }
            }

            return dataset;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(DicomDataset).GetTypeInfo().IsAssignableFrom(typeToConvert.GetTypeInfo());
        }

        #endregion

        /// <summary>
        /// Create an instance of a IBulkDataUriByteBuffer. Override this method to use a different IBulkDataUriByteBuffer implementation in applications.
        /// </summary>
        /// <param name="bulkDataUri">The URI of a bulk data element as defined in <a href="http://dicom.nema.org/medical/dicom/current/output/chtml/part19/chapter_A.html#table_A.1.5-2">Table A.1.5-2 in PS3.19</a>.</param>
        /// <returns>An instance of a Bulk URI Byte buffer.</returns>
        protected virtual IBulkDataUriByteBuffer CreateBulkDataUriByteBuffer(string bulkDataUri) => new BulkDataUriByteBuffer(bulkDataUri);

        #region Utilities

        internal static DicomTag ParseTag(string tagstr)
        {
            if (Regex.IsMatch(tagstr, @"\A\b[0-9a-fA-F]+\b\Z"))
            {
                var group = Convert.ToUInt16(tagstr.Substring(0, 4), 16);
                var element = Convert.ToUInt16(tagstr.Substring(4), 16);
                var tag = new DicomTag(group, element);
                return tag;
            }

            return DicomDictionary.Default[tagstr];
        }

        private static DicomItem CreateDicomItem(DicomTag tag, string vr, object data)
        {
            DicomItem item = vr switch
            {
                "AE" => new DicomApplicationEntity(tag, (string[])data),
                "AS" => new DicomAgeString(tag, (string[])data),
                "AT" => new DicomAttributeTag(tag, ((string[])data).Select(ParseTag).ToArray()),
                "CS" => new DicomCodeString(tag, (string[])data),
                "DA" => new DicomDate(tag, (string[])data),
                "DS" => data is IByteBuffer dataBufferDS
                            ? new DicomDecimalString(tag, dataBufferDS)
                            : new DicomDecimalString(tag, (decimal[])data),
                "DT" => new DicomDateTime(tag, (string[])data),
                "FD" => data is IByteBuffer dataBufferFD
                            ? new DicomFloatingPointDouble(tag, dataBufferFD)
                            : new DicomFloatingPointDouble(tag, (double[])data),
                "FL" => data is IByteBuffer dataBufferFL
                            ? new DicomFloatingPointSingle(tag, dataBufferFL)
                            : new DicomFloatingPointSingle(tag, (float[])data),
                "IS" => data is IByteBuffer dataBufferIS
                            ? new DicomIntegerString(tag, dataBufferIS)
                            : new DicomIntegerString(tag, (int[])data),
                "LO" => new DicomLongString(tag, (string[])data),
                "LT" => data is IByteBuffer dataBufferLT
                            ? new DicomLongText(tag, _jsonTextEncodings, dataBufferLT)
                            : new DicomLongText(tag, data.GetAsStringArray().GetSingleOrEmpty()),
                "OB" => new DicomOtherByte(tag, (IByteBuffer)data),
                "OD" => new DicomOtherDouble(tag, (IByteBuffer)data),
                "OF" => new DicomOtherFloat(tag, (IByteBuffer)data),
                "OL" => new DicomOtherLong(tag, (IByteBuffer)data),
                "OW" => new DicomOtherWord(tag, (IByteBuffer)data),
                "OV" => new DicomOtherVeryLong(tag, (IByteBuffer)data),
                "PN" => new DicomPersonName(tag, (string[])data),
                "SH" => new DicomShortString(tag, (string[])data),
                "SL" => data is IByteBuffer dataBufferSL
                            ? new DicomSignedLong(tag, dataBufferSL)
                            : new DicomSignedLong(tag, (int[])data),
                "SQ" => new DicomSequence(tag, ((DicomDataset[])data)),
                "SS" => data is IByteBuffer dataBufferSS
                            ? new DicomSignedShort(tag, dataBufferSS)
                            : new DicomSignedShort(tag, (short[])data),
                "ST" => data is IByteBuffer dataBufferST
                            ? new DicomShortText(tag, _jsonTextEncodings, dataBufferST)
                            : new DicomShortText(tag, data.GetAsStringArray().GetFirstOrEmpty()),
                "SV" => data is IByteBuffer dataBufferSV
                                ? new DicomSignedVeryLong(tag, dataBufferSV)
                                : new DicomSignedVeryLong(tag, (long[])data),
                "TM" => new DicomTime(tag, (string[])data),
                "UC" => data is IByteBuffer dataBufferUC
                            ? new DicomUnlimitedCharacters(tag, _jsonTextEncodings, dataBufferUC)
                            : new DicomUnlimitedCharacters(tag, data.GetAsStringArray().SingleOrDefault()),
                "UI" => new DicomUniqueIdentifier(tag, (string[])data),
                "UL" => data is IByteBuffer dataBufferUL
                            ? new DicomUnsignedLong(tag, dataBufferUL)
                            : new DicomUnsignedLong(tag, (uint[])data),
                "UN" => new DicomUnknown(tag, (IByteBuffer)data),
                "UR" => new DicomUniversalResource(tag, data.GetAsStringArray().GetSingleOrEmpty()),
                "US" => data is IByteBuffer dataBufferUS
                            ? new DicomUnsignedShort(tag, dataBufferUS)
                            : new DicomUnsignedShort(tag, (ushort[])data),
                "UT" => data is IByteBuffer dataBufferUT
                            ? new DicomUnlimitedText(tag, _jsonTextEncodings, dataBufferUT)
                            : new DicomUnlimitedText(tag, data.GetAsStringArray().GetSingleOrEmpty()),
                "UV" => data is IByteBuffer dataBufferUV
                            ? new DicomUnsignedVeryLong(tag, dataBufferUV)
                            : new DicomUnsignedVeryLong(tag, (ulong[])data),
                _ => throw new NotSupportedException("Unsupported value representation"),
            };
            return item;
        }

        #endregion

        #region WriteJson helpers

        private void WriteJsonDicomItem(Utf8JsonWriter writer, DicomItem item, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("vr", item.ValueRepresentation.Code);

            switch (item.ValueRepresentation.Code)
            {
                case "PN":
                    WriteJsonPersonName(writer, (DicomPersonName)item);
                    break;
                case "SQ":
                    WriteJsonSequence(writer, (DicomSequence)item, options);
                    break;
                case "OB":
                case "OD":
                case "OF":
                case "OL":
                case "OV":
                case "OW":
                case "UN":
                    WriteJsonOther(writer, (DicomElement)item);
                    break;
                case "FL":
                    WriteJsonElement<float>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "FD":
                    WriteJsonElement<double>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "IS":
                    WriteJsonAsNumberOrString<int>(writer, (DicomElement)item,
                        (w, v) => writer.WriteNumberValue(v));
                    break;
                case "SL":
                    WriteJsonElement<int>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "SS":
                    WriteJsonElement<short>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "SV":
                    WriteJsonAsNumberOrString<long>(writer, (DicomElement)item,
                        (w, v) => writer.WriteNumberValue(v));
                    break;
                case "UL":
                    WriteJsonElement<uint>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "US":
                    WriteJsonElement<ushort>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "UV":
                    WriteJsonAsNumberOrString<ulong>(writer, (DicomElement)item,
                        (w, v) => writer.WriteNumberValue(v));
                    break;
                case "DS":
                    WriteJsonAsNumberOrString(writer, (DicomElement)item,
                        () => WriteJsonDecimalString(writer, (DicomElement)item));
                    break;
                case "AT":
                    WriteJsonAttributeTag(writer, (DicomElement)item);
                    break;
                default:
                    WriteJsonElement<string>(writer, (DicomElement)item, (w, v) => writer.WriteStringValue(v));
                    break;
            }

            if (WriteKeyword || WriteName)
            {
                var unknown = item.Tag.DictionaryEntry == null
                              || string.IsNullOrWhiteSpace(item.Tag.DictionaryEntry.Keyword)
                              || (item.Tag.DictionaryEntry.MaskTag != null && item.Tag.DictionaryEntry.MaskTag.Mask != 0xffffffff);

                if (!unknown)
                {
                    if (WriteKeyword)
                        writer.WriteString("keyword", item.Tag.DictionaryEntry.Keyword);
                    if (WriteName)
                        writer.WriteString("name", item.Tag.DictionaryEntry.Name);
                }
            }

            writer.WriteEndObject();
        }

        private void WriteJsonAsNumberOrString<T>(Utf8JsonWriter writer, DicomElement elem, WriteValue<T> numberValueWriter)
            => WriteJsonAsNumberOrString(writer, elem, () => WriteJsonElement(writer, elem, numberValueWriter));

        private void WriteJsonAsNumberOrString(Utf8JsonWriter writer, DicomElement elem, Action numberWriterAction)
        {
            if (_numberSerializationMode == NumberSerializationMode.AsString)
            {
                WriteJsonElement<string>(writer, elem, (w, v) => writer.WriteStringValue(v));
            }
            else
            {
                try
                {
                    numberWriterAction();
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException)
                {
                    if (_numberSerializationMode == NumberSerializationMode.PreferablyAsNumber)
                    {
                        WriteJsonElement<string>(writer, elem, (w, v) => writer.WriteStringValue(v));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }


        private static void WriteJsonDecimalString(Utf8JsonWriter writer, DicomElement elem)
        {
            if (elem.Count == 0) return;

            var writerActions = new List<Action>
            {
                () => writer.WritePropertyName("Value"),
                writer.WriteStartArray
            };

            foreach (var val in elem.Get<string[]>())
            {
                if (string.IsNullOrEmpty(val))
                {
                    writerActions.Add(writer.WriteNullValue);
                }
                else
                {
                    var fix = FixDecimalString(val);
                    if (TryParseULong(fix, out ulong xulong))
                    {
                        writerActions.Add(() => writer.WriteNumberValue(xulong));
                    }
                    else if (TryParseLong(fix, out long xlong))
                    {
                        writerActions.Add(() => writer.WriteNumberValue(xlong));
                    }
                    else if (TryParseDecimal(fix, out decimal xdecimal))
                    {
                        writerActions.Add(() => writer.WriteNumberValue(xdecimal));
                    }
                    else if (TryParseDouble(fix, out double xdouble))
                    {
                        writerActions.Add(() => writer.WriteNumberValue(xdouble));
                    }
                    else
                    {
                        throw new FormatException($"Cannot write dicom number {val} to json");
                    }
                }
            }
            writerActions.Add(writer.WriteEndArray);

            foreach (var action in writerActions)
            {
                action();
            }
        }

        private static bool IsValidJsonNumber(string val)
        {
            try
            {
                DicomValidation.ValidateDS(val);
                return true;
            }
            catch (DicomValidationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Fix-up a Dicom DS number for use with json.
        /// Rationale: There is a requirement that DS numbers shall be written as json numbers in part 18.F json, but the
        /// requirements on DS allows values that are not json numbers. This method "fixes" them to conform to json numbers.
        /// </summary>
        /// <param name="val">A valid DS value</param>
        /// <returns>A json number equivalent to the supplied DS value</returns>
        private static string FixDecimalString(string val)
        {
            // trim invalid padded character
            val = val.Trim().TrimEnd('\0');

            if (IsValidJsonNumber(val))
            {
                return val;
            }

            if (string.IsNullOrWhiteSpace(val)) { return null; }

            val = val.Trim();

            var negative = false;
            // Strip leading superfluous plus signs
            if (val[0] == '+')
            {
                val = val.Substring(1);
            }
            else if (val[0] == '-')
            {
                // Temporarily remove negation sign for zero-stripping later
                negative = true;
                val = val.Substring(1);
            }

            // Strip leading superfluous zeros
            if (val.Length > 1 && val[0] == '0' && val[1] != '.')
            {
                int i = 0;
                while (i < val.Length - 1 && val[i] == '0' && val[i + 1] != '.')
                {
                    i++;
                }

                val = val.Substring(i);
            }

            // Re-add negation sign
            if (negative) { val = "-" + val; }

            if (IsValidJsonNumber(val))
            {
                return val;
            }

            throw new FormatException("Failed converting DS value to json");
        }

        private static void WriteJsonElement<T>(Utf8JsonWriter writer, DicomElement elem, WriteValue<T> writeValue)
        {
            if (elem.Count != 0)
            {
                T[] values = elem.Get<T[]>();
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var val in values)
                {
                    if (val == null || (typeof(T) == typeof(string) && val.Equals("")))
                    {
                        writer.WriteNullValue();
                    }
                    else if ((val is float f && float.IsNaN(f)) || (val is double d && double.IsNaN(d)))
                    {
                        writer.WriteStringValue("NaN");
                    }
                    else if ((val is double dp && double.IsPositiveInfinity(dp)) || (val is float fp && float.IsPositiveInfinity(fp)))
                    {
                        writer.WriteStringValue("Infinity");
                    }
                    else if ((val is double dn && double.IsNegativeInfinity(dn)) || (val is float fn && float.IsNegativeInfinity(fn)))
                    {
                        writer.WriteStringValue("-Infinity");
                    }
                    else
                    {
                        writeValue(writer, val);
                    }
                }
                writer.WriteEndArray();
            }
        }

        private static void WriteJsonAttributeTag(Utf8JsonWriter writer, DicomElement elem)
        {
            if (elem.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var val in elem.Get<DicomTag[]>())
                {
                    if (val == null) { writer.WriteNullValue(); }
                    else { writer.WriteStringValue(((uint)val).ToString("X8")); }
                }
                writer.WriteEndArray();
            }
        }

        private static void WriteJsonOther(Utf8JsonWriter writer, DicomElement elem)
        {
            if (elem.Buffer is IBulkDataUriByteBuffer buffer)
            {
                writer.WritePropertyName("BulkDataURI");
                writer.WriteStringValue(buffer.BulkDataUri);
            }
            else if (elem.Count != 0)
            {
                writer.WritePropertyName("InlineBinary");
                writer.WriteBase64StringValue(elem.Buffer.Data);
            }
        }

        private void WriteJsonSequence(Utf8JsonWriter writer, DicomSequence seq, JsonSerializerOptions options)
        {
            if (seq.Items.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();

                foreach (var child in seq.Items)
                {
                    Write(writer, child, options);
                }

                writer.WriteEndArray();
            }
        }

        private static void WriteJsonPersonName(Utf8JsonWriter writer, DicomPersonName pn)
        {
            if (pn.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();

                foreach (var val in pn.Get<string[]>())
                {
                    if (string.IsNullOrEmpty(val))
                    {
                        writer.WriteNullValue();
                    }
                    else
                    {
                        var componentGroupValues = val.Split(_personNameComponentGroupDelimiter);
                        int i = 0;

                        writer.WriteStartObject();
                        foreach (var componentGroupValue in componentGroupValues)
                        {
                            // Based on standard http://dicom.nema.org/dicom/2013/output/chtml/part18/sect_F.2.html
                            // 1. Empty values are skipped
                            // 2. Leading componentGroups even if null need to have delimiters. Trailing componentGroup delimiter can be omitted
                            if (!string.IsNullOrWhiteSpace(componentGroupValue))
                            {
                                writer.WritePropertyName(_personNameComponentGroupNames[i]);
                                writer.WriteStringValue(componentGroupValue);
                            }
                            i++;
                        }
                        writer.WriteEndObject();
                    }
                }

                writer.WriteEndArray();
            }
        }


        #endregion


        #region ReadJson helpers


        private DicomItem ReadJsonDicomItem(DicomTag tag, ref Utf8JsonReader reader)
        {
            reader.AssumeAndSkip(JsonTokenType.StartObject);
            var currentDepth = reader.CurrentDepth;

            reader.Assume(JsonTokenType.PropertyName);

            string vr;
            var property = reader.GetString();
            if (property == "vr")
            {
                reader.Read();
                vr = reader.GetString();
                reader.Read();
            }
            else
            {
                // Find the value of the VR property on a copy of the reader.
                // This preserves the current location of the reader
                vr = FindValue(reader, "vr", "none");
            }

            if (vr == "none") { throw new JsonException("Malformed DICOM json. vr value missing"); }

            object data;

            switch (vr)
            {
                case "OB":
                case "OD":
                case "OF":
                case "OL":
                case "OW":
                case "OV":
                case "UN":
                    data = ReadJsonOX(ref reader);
                    break;
                case "SQ":
                    data = ReadJsonSequence(ref reader);
                    break;
                case "PN":
                    data = ReadJsonPersonName(ref reader);
                    break;
                case "FL":
                    data = ReadJsonMultiNumber(ref reader, r => r.GetSingle());
                    break;
                case "FD":
                    data = ReadJsonMultiNumber(ref reader, r => r.GetDouble());
                    break;
                case "IS":
                    data = ReadJsonMultiNumberOrString(ref reader, r => r.GetInt32(), TryParseInt);
                    break;
                case "SL":
                    data = ReadJsonMultiNumber(ref reader, r => r.GetInt32());
                    break;
                case "SS":
                    data = ReadJsonMultiNumber(ref reader, r => r.GetInt16());
                    break;
                case "SV":
                    data = ReadJsonMultiNumberOrString(ref reader, r => r.GetInt64(), TryParseLong);
                    break;
                case "UL":
                    data = ReadJsonMultiNumber(ref reader, r => r.GetUInt32());
                    break;
                case "US":
                    data = ReadJsonMultiNumber(ref reader, r => r.GetUInt16());
                    break;
                case "UV":
                    data = ReadJsonMultiNumberOrString(ref reader, r => r.GetUInt64(), TryParseULong);
                    break;
                case "DS":
                    data = ReadJsonMultiNumberOrString(ref reader, r => r.GetDecimal(), TryParseDecimal);
                    break;
                default:
                    data = ReadJsonMultiString(ref reader);
                    break;
            }

            // move to the end of the object
            while (reader.CurrentDepth >= currentDepth && reader.Read())
            {
                // skip this data
            }
            reader.AssumeAndSkip(JsonTokenType.EndObject);

            DicomItem item = CreateDicomItem(tag, vr, data);
            return item;
        }


        private object ReadJsonMultiString(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<string>();
            }

            switch (MoveToProperty(ref reader, "Value", "BulkDataURI"))
            {
                case "Value":
                    return ReadJsonMultiStringValue(ref reader);
                case "BulkDataURI":
                    return ReadJsonBulkDataUri(ref reader);
                default:
                    return Array.Empty<string>();
            }
        }

        private static string ReadPropertyName(ref Utf8JsonReader reader)
        {
            reader.Assume(JsonTokenType.PropertyName);
            var propertyname = reader.GetString();
            reader.Read();
            return propertyname;
        }


        private static string[] ReadJsonMultiStringValue(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                reader.Read();
                return Array.Empty<string>();
            }
            reader.AssumeAndSkip(JsonTokenType.StartArray);
            var childStrings = new List<string>();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    childStrings.Add(null);
                }
                else if (reader.TokenType == JsonTokenType.String)
                {
                    childStrings.Add(reader.GetString());
                }
                else
                {
                    // TODO: invalid. handle this?
                }
                reader.Read();
            }
            reader.AssumeAndSkip(JsonTokenType.EndArray);
            var data = childStrings.ToArray();
            return data;
        }


        private object ReadJsonMultiNumberOrString<T>(ref Utf8JsonReader reader, GetValue<T> getValue, TryParse<T> tryParse)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<T>();
            }

            switch (MoveToProperty(ref reader, "Value", "BulkDataURI"))
            {
                case "Value":
                    return ReadJsonMultiNumberOrStringValue<T>(ref reader, getValue, tryParse);
                case "BulkDataURI":
                    return ReadJsonBulkDataUri(ref reader);
                default:
                    return Array.Empty<T>();
            }
        }

        private static T[] ReadJsonMultiNumberOrStringValue<T>(ref Utf8JsonReader reader, GetValue<T> getValue, TryParse<T> tryParse)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                reader.Read();
                return Array.Empty<T>();
            }
            reader.AssumeAndSkip(JsonTokenType.StartArray);

            var childValues = new List<T>();
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    childValues.Add(getValue(reader));
                }
                else if (reader.TokenType == JsonTokenType.String && reader.GetString() == "NaN")
                {
                    if (typeof(T) == typeof(double))
                    {
                        childValues.Add((T)(double.NaN as object));
                    }
                    else
                    {
                        childValues.Add((T)(float.NaN as object));
                    }
                }
                else if (reader.TokenType == JsonTokenType.String && reader.GetString() == "Infinity")
                {
                    if (typeof(T) == typeof(double))
                    {
                        childValues.Add((T)(double.PositiveInfinity as object));
                    }
                    else
                    {
                        childValues.Add((T)(float.PositiveInfinity as object));
                    }
                }
                else if (reader.TokenType == JsonTokenType.String && reader.GetString() == "-Infinity")
                {
                    if (typeof(T) == typeof(double))
                    {
                        childValues.Add((T)(double.NegativeInfinity as object));
                    }
                    else
                    {
                        childValues.Add((T)(float.NegativeInfinity as object));
                    }
                }
                else if (reader.TokenType == JsonTokenType.String && tryParse(reader.GetString(), out T parsed))
                {
                    childValues.Add(parsed);
                }
                else
                {
                    throw new JsonException("Malformed DICOM json, number expected");
                }
                reader.Read();
            }
            reader.AssumeAndSkip(JsonTokenType.EndArray);

            var data = childValues.ToArray();
            return data;
        }

        private object ReadJsonMultiNumber<T>(ref Utf8JsonReader reader, GetValue<T> getValue)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<T>();
            }

            switch (MoveToProperty(ref reader, "Value", "BulkDataURI"))
            {
                case "Value":
                    return ReadJsonMultiNumberValue<T>(ref reader, getValue);
                case "BulkDataURI":
                    return ReadJsonBulkDataUri(ref reader);
                default:
                    return Array.Empty<T>();
            }
        }


        private static T[] ReadJsonMultiNumberValue<T>(ref Utf8JsonReader reader, GetValue<T> getValue)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                reader.Read();
                return Array.Empty<T>();
            }
            reader.AssumeAndSkip(JsonTokenType.StartArray);

            var childValues = new List<T>();
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    childValues.Add(getValue(reader));
                }
                else if (reader.TokenType == JsonTokenType.String && reader.GetString() == "NaN")
                {
                    if (typeof(T) == typeof(double))
                    {
                        childValues.Add((T)(double.NaN as object));
                    }
                    else
                    {
                        childValues.Add((T)(float.NaN as object));
                    }
                }
                else if (reader.TokenType == JsonTokenType.String && reader.GetString() == "Infinity")
                {
                    if (typeof(T) == typeof(double))
                    {
                        childValues.Add((T)(double.PositiveInfinity as object));
                    }
                    else
                    {
                        childValues.Add((T)(float.PositiveInfinity as object));
                    }
                }
                else if (reader.TokenType == JsonTokenType.String && reader.GetString() == "-Infinity")
                {
                    if (typeof(T) == typeof(double))
                    {
                        childValues.Add((T)(double.NegativeInfinity as object));
                    }
                    else
                    {
                        childValues.Add((T)(float.NegativeInfinity as object));
                    }
                }
                else
                {
                    throw new JsonException("Malformed DICOM json, number expected");
                }
                reader.Read();
            }
            reader.AssumeAndSkip(JsonTokenType.EndArray);

            var data = childValues.ToArray();
            return data;
        }


        private string[] ReadJsonPersonName(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<string>();
            }

            switch (MoveToProperty(ref reader, "Value"))
            {
                case "Value":
                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        reader.Read();
                        return Array.Empty<string>();
                    }
                    else
                    {
                        reader.AssumeAndSkip(JsonTokenType.StartArray);

                        var childStrings = new List<string>();
                        while (reader.TokenType != JsonTokenType.EndArray)
                        {
                            if (reader.TokenType == JsonTokenType.Null)
                            {
                                reader.Read();
                                childStrings.Add(null);
                            }
                            else if (reader.TokenType == JsonTokenType.StartObject)
                            {
                                // parse
                                reader.Read(); // read into object
                                var componentGroupCount = 3;
                                var componentGroupValues = new string[componentGroupCount];
                                while (reader.TokenType != JsonTokenType.EndObject)
                                {
                                    if (reader.TokenType == JsonTokenType.PropertyName
                                        && reader.GetString() == "Alphabetic")
                                    {
                                        reader.Read(); // skip propertyname
                                        componentGroupValues[0] = reader.GetString(); // read value
                                    }
                                    else if (reader.TokenType == JsonTokenType.PropertyName
                                        && reader.GetString() == "Ideographic")
                                    {
                                        reader.Read(); // skip propertyname
                                        componentGroupValues[1] = reader.GetString(); // read value
                                    }
                                    else if (reader.TokenType == JsonTokenType.PropertyName
                                        && reader.GetString() == "Phonetic")
                                    {
                                        reader.Read(); // skip propertyname
                                        componentGroupValues[2] = reader.GetString(); // read value
                                    }
                                    reader.Read();
                                }

                                //build
                                StringBuilder stringBuilder = new StringBuilder();
                                for (int i = 0; i < componentGroupCount; i++)
                                {
                                    var val = componentGroupValues[i];

                                    if (!string.IsNullOrWhiteSpace(val))
                                    {
                                        stringBuilder.Append(val);

                                    }
                                    stringBuilder.Append(_personNameComponentGroupDelimiter);
                                }

                                //remove optional trailing delimiters
                                string pnVal = stringBuilder.ToString().TrimEnd(_personNameComponentGroupDelimiter);

                                childStrings.Add(pnVal); // add value
                                reader.AssumeAndSkip(JsonTokenType.EndObject);
                            }
                            else
                            {
                                // TODO: invalid. handle this?
                            }
                        }
                        reader.AssumeAndSkip(JsonTokenType.EndArray);
                        var data = childStrings.ToArray();
                        return data;
                    }

                default:
                    return Array.Empty<string>();
            }
        }

        private DicomDataset[] ReadJsonSequence(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<DicomDataset>();
            }

            switch (MoveToProperty(ref reader, "Value"))
            {
                case "Value":
                    reader.AssumeAndSkip(JsonTokenType.StartArray);
                    var childItems = new List<DicomDataset>();
                    while (reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (reader.TokenType == JsonTokenType.Null)
                        {
                            reader.Read();
                            childItems.Add(null);
                        }
                        else if (reader.TokenType == JsonTokenType.StartObject)
                        {
                            childItems.Add(ReadJsonDataset(ref reader));
                            reader.AssumeAndSkip(JsonTokenType.EndObject);
                        }
                        else
                        {
                            throw new JsonException("Malformed DICOM json, object expected");
                        }
                    }
                    reader.AssumeAndSkip(JsonTokenType.EndArray);
                    var data = childItems.ToArray();
                    return data;

                default:
                    return Array.Empty<DicomDataset>();
            }
        }


        private IByteBuffer ReadJsonOX(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return EmptyBuffer.Value;
            }

            switch (MoveToProperty(ref reader, "InlineBinary", "BulkDataURI"))
            {
                case "InlineBinary":
                    return ReadJsonInlineBinary(ref reader);
                case "BulkDataURI":
                    return ReadJsonBulkDataUri(ref reader);
                default:
                    return EmptyBuffer.Value;
            }
        }


        private static IByteBuffer ReadJsonInlineBinary(ref Utf8JsonReader reader) 
            => reader.TokenType == JsonTokenType.StartArray
                ? ReadJsonInlineBinaryArray(ref reader)
                : ReadJsonInlineBinaryString(ref reader);

        private static IByteBuffer ReadJsonInlineBinaryArray(ref Utf8JsonReader reader)
        {
            reader.Read(); // caller already checked for StartArray
            var data = ReadJsonInlineBinaryString(ref reader);
            reader.AssumeAndSkip(JsonTokenType.EndArray);
            return data;            
        }

        private static IByteBuffer ReadJsonInlineBinaryString(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Malformed DICOM json. string expected");
            }
            var data = new MemoryByteBuffer(reader.GetBytesFromBase64());
            reader.Read();
            return data;
        }

        private IBulkDataUriByteBuffer ReadJsonBulkDataUri(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.String) { throw new JsonException("Malformed DICOM json. string expected"); }
            var data = CreateBulkDataUriByteBuffer(reader.GetString());
            reader.Read();
            return data;
        }


        #endregion

        /// <summary>
        /// Move the reader to the first occurance of any of the specified properties
        /// in the current Json object
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="properties"></param>
        /// <returns>
        /// The name of the property moved to, 
        /// or null if no such property exists
        /// </returns>
        private static string MoveToProperty(ref Utf8JsonReader reader, params string[] properties)
        {
            while (reader.TokenType != JsonTokenType.EndObject)
            {
                string propertyname = ReadPropertyName(ref reader);
                if (properties.Contains(propertyname))
                {
                    // This property is one of the requested
                    return propertyname;
                }

                // Move to next property
                var currentDepth = reader.CurrentDepth;
                while (reader.CurrentDepth >= currentDepth && reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.PropertyName
                        && reader.CurrentDepth == currentDepth)
                    {
                        // We have found the next property in the same object
                        break;
                    }
                }
            }

            reader.Assume(JsonTokenType.EndObject);
            return null;
        }

        private string FindValue(Utf8JsonReader reader, string property, string defaultValue)
        {
            var currentDepth = reader.CurrentDepth;
            while (reader.CurrentDepth >= currentDepth)
            {
                if (reader.CurrentDepth == currentDepth
                    && reader.TokenType == JsonTokenType.PropertyName
                    && reader.GetString() == property)
                {
                    reader.Read(); // move to value
                    return reader.GetString();
                }
                reader.Read();
            }
            return defaultValue;
        }

        private static bool TryParseInt(string value, out int parsed)
            => int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed);

        private static bool TryParseDecimal(string value, out decimal parsed)
            => decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed);

        private static bool TryParseDouble(string value, out double parsed)
            => double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed);

        private static bool TryParseLong(string value, out long parsed)
            => long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed);

        private static bool TryParseULong(string value, out ulong parsed)
            => ulong.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed);

    }

    internal static class JsonDicomConverterExtensions
    {

        public static string[] GetAsStringArray(this object data) => (string[])data;

        public static string GetFirstOrEmpty(this string[] array) => array.Length > 0 ? array[0] : string.Empty;

        public static string GetSingleOrEmpty(this string[] array) => array.Length > 0 ? array.Single() : string.Empty;

    }

}
