// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

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
                var ds = conv.Read(ref reader, typeToConvert, options);
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
    /// Converts a DicomDataset object to and from JSON using the NewtonSoft Json.NET library
    /// </summary>
    public class DicomJsonConverter : JsonConverter<DicomDataset>
    {

        private readonly bool _writeTagsAsKeywords;
        private readonly bool _autoValidate;
        private readonly static Encoding _jsonTextEncoding = Encoding.UTF8;

        private delegate T GetValue<out T>(Utf8JsonReader reader);
        private delegate void WriteValue<in T>(Utf8JsonWriter writer, T value);


        /// <summary>
        /// Initialize the JsonDicomConverter.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        /// <param name="autoValidate">Whether the content of DicomItems shall be validated as soon as they are added to the DicomDataset. </param>
        public DicomJsonConverter(bool writeTagsAsKeywords = false, bool autoValidate = true)
        {
            _writeTagsAsKeywords = writeTagsAsKeywords;
            _autoValidate = autoValidate;
        }

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
            if (reader.TokenType != JsonTokenType.StartObject) { return null; }
            reader.Read();

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                Assume(ref reader, JsonTokenType.PropertyName);
                var tagstr = reader.GetString();
                DicomTag tag = ParseTag(tagstr);
                reader.Read(); // move to value
                var item = ReadJsonDicomItem(tag, ref reader);
                dataset.Add(item);
            }
            AssumeAndSkip(ref reader, JsonTokenType.EndObject);

            foreach (var item in dataset)
            {
                if (item.Tag.IsPrivate && ((item.Tag.Element & 0xff00) != 0))
                {
                    var privateCreatorTag = new DicomTag(item.Tag.Group, (ushort)(item.Tag.Element >> 8));

                    if (dataset.Contains(privateCreatorTag))
                    {
                        item.Tag.PrivateCreator = new DicomPrivateCreator(dataset.GetSingleValue<string>(privateCreatorTag));
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
        /// <param name="bulkDataUri">The URI of a bulk data element as defined in <see cref="!:http://dicom.nema.org/medical/dicom/current/output/chtml/part19/chapter_A.html#table_A.1.5-2">Table A.1.5-2 in PS3.19</see>.</param>
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
                            ? new DicomLongText(tag, _jsonTextEncoding, dataBufferLT)
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
                            ? new DicomShortText(tag, _jsonTextEncoding, dataBufferST)
                            : new DicomShortText(tag, data.GetAsStringArray().GetFirstOrEmpty()),
                "SV" => data is IByteBuffer dataBufferSV
                                ? new DicomSignedVeryLong(tag, dataBufferSV)
                                : new DicomSignedVeryLong(tag, (long[])data),
                "TM" => new DicomTime(tag, (string[])data),
                "UC" => data is IByteBuffer dataBufferUC
                            ? new DicomUnlimitedCharacters(tag, _jsonTextEncoding, dataBufferUC)
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
                            ? new DicomUnlimitedText(tag, _jsonTextEncoding, dataBufferUT)
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
                case "SL":
                    WriteJsonElement<int>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "SS":
                    WriteJsonElement<short>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "SV":
                    WriteJsonElement<long>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "UL":
                    WriteJsonElement<uint>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "US":
                    WriteJsonElement<ushort>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "UV":
                    WriteJsonElement<ulong>(writer, (DicomElement)item, (w, v) => writer.WriteNumberValue(v));
                    break;
                case "DS":
                    WriteJsonDecimalString(writer, (DicomElement)item);
                    break;
                case "AT":
                    WriteJsonAttributeTag(writer, (DicomElement)item);
                    break;
                default:
                    WriteJsonElement<string>(writer, (DicomElement)item, (w, v) => writer.WriteStringValue(v));
                    break;
            }
            writer.WriteEndObject();
        }

        private static void WriteJsonDecimalString(Utf8JsonWriter writer, DicomElement elem)
        {
            if (elem.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var val in elem.Get<string[]>())
                {
                    if (string.IsNullOrEmpty(val))
                    {
                        writer.WriteNullValue();
                    }
                    else
                    {
                        var fix = FixDecimalString(val);
                        if (ulong.TryParse(fix, NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong xulong))
                        {
                            writer.WriteNumberValue(xulong);
                        }
                        else if (long.TryParse(fix, NumberStyles.Integer, CultureInfo.InvariantCulture, out long xlong))
                        {
                            writer.WriteNumberValue(xlong);
                        }
                        else if (decimal.TryParse(fix, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal xdecimal))
                        {
                            writer.WriteNumberValue(xdecimal);
                        }
                        else if (double.TryParse(fix, NumberStyles.Float, CultureInfo.InvariantCulture, out double xdouble))
                        {
                            writer.WriteNumberValue(xdouble);
                        }
                        else
                        {
                            throw new FormatException($"Cannot write dicom number {val} to json");
                        }
                    }
                }
                writer.WriteEndArray();
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

            throw new ArgumentException("Failed converting DS value to json");
        }

        private static void WriteJsonElement<T>(Utf8JsonWriter writer, DicomElement elem, WriteValue<T> writeValue)
        {
            if (elem.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var val in elem.Get<T[]>())
                {
                    if (val == null || (typeof(T) == typeof(string) && val.Equals("")))
                    {
                        writer.WriteNullValue();
                    }
                    else if (val is float f && float.IsNaN(f))
                    {
                        writer.WriteStringValue("NaN");
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
                writer.WriteStartArray();
                writer.WriteBase64StringValue(elem.Buffer.Data);
                writer.WriteEndArray();
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
                        writer.WriteStartObject();
                        writer.WritePropertyName("Alphabetic");
                        writer.WriteStringValue(val);
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
            AssumeAndSkip(ref reader, JsonTokenType.StartObject);
            var currentDepth = reader.CurrentDepth;

            Assume(ref reader, JsonTokenType.PropertyName);

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
                    data = ReadJsonMultiNumber<float>(ref reader, r => r.GetSingle());
                    break;
                case "FD":
                    data = ReadJsonMultiNumber<double>(ref reader, r => r.GetDouble());
                    break;
                case "IS":
                    data = ReadJsonMultiNumber<int>(ref reader, r => r.GetInt32());
                    break;
                case "SL":
                    data = ReadJsonMultiNumber<int>(ref reader, r => r.GetInt32());
                    break;
                case "SS":
                    data = ReadJsonMultiNumber<short>(ref reader, r => r.GetInt16());
                    break;
                case "SV":
                    data = ReadJsonMultiNumber<long>(ref reader, r => r.GetInt64());
                    break;
                case "UL":
                    data = ReadJsonMultiNumber<uint>(ref reader, r => r.GetUInt32());
                    break;
                case "US":
                    data = ReadJsonMultiNumber<ushort>(ref reader, r => r.GetUInt16());
                    break;
                case "UV":
                    data = ReadJsonMultiNumber<ulong>(ref reader, r => r.GetUInt64());
                    break;
                case "DS":
                    data = ReadJsonMultiNumber<decimal>(ref reader, r => r.GetDecimal());
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
            AssumeAndSkip(ref reader, JsonTokenType.EndObject);

            DicomItem item = CreateDicomItem(tag, vr, data);
            return item;
        }


        private object ReadJsonMultiString(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<string>();
            }
            string propertyname = ReadPropertyName(ref reader);

            if (propertyname == "Value")
            {
                return ReadJsonMultiStringValue(ref reader);
            }
            else if (propertyname == "BulkDataURI")
            {
                // JToken bulk
                return ReadJsonBulkDataUri(ref reader);
            }
            else
            {
                return Array.Empty<string>();
            }
        }


        private static string ReadPropertyName(ref Utf8JsonReader reader)
        {
            Assume(ref reader, JsonTokenType.PropertyName);
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
            AssumeAndSkip(ref reader, JsonTokenType.StartArray);
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
            AssumeAndSkip(ref reader, JsonTokenType.EndArray);
            var data = childStrings.ToArray();
            return data;
        }


        private object ReadJsonMultiNumber<T>(ref Utf8JsonReader reader, GetValue<T> getValue)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<T>();
            }
            string propertyname = ReadPropertyName(ref reader);

            if (propertyname == "Value")
            {
                return ReadJsonMultiNumberValue<T>(ref reader, getValue);
            }
            else if (propertyname == "BulkDataURI")
            {
                return ReadJsonBulkDataUri(ref reader);
            }
            else
            {
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
            AssumeAndSkip(ref reader, JsonTokenType.StartArray);

            var childValues = new List<T>();
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    childValues.Add(getValue(reader));
                }
                else if (reader.TokenType == JsonTokenType.String && reader.GetString() == "NaN")
                {
                    childValues.Add((T)(float.NaN as object));
                }
                else
                {
                    throw new JsonException("Malformed DICOM json, number expected");
                }
                reader.Read();
            }
            AssumeAndSkip(ref reader, JsonTokenType.EndArray);

            var data = childValues.ToArray();
            return data;
        }


        private string[] ReadJsonPersonName(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<string>();
            }
            var propertyName = ReadPropertyName(ref reader);

            if (propertyName == "Value")
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    reader.Read();
                    return Array.Empty<string>();
                }
                else
                {
                    AssumeAndSkip(ref reader, JsonTokenType.StartArray);

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
                            reader.Read(); // read into object
                            while (reader.TokenType != JsonTokenType.EndObject)
                            {
                                if (reader.TokenType == JsonTokenType.PropertyName
                                    && reader.GetString() == "Alphabetic")
                                {
                                    reader.Read(); // skip propertyname
                                    childStrings.Add(reader.GetString()); // read value
                                }
                                reader.Read();
                            }
                            AssumeAndSkip(ref reader, JsonTokenType.EndObject);
                        }
                        else
                        {
                            // TODO: invalid. handle this?
                        }
                    }
                    AssumeAndSkip(ref reader, JsonTokenType.EndArray);
                    var data = childStrings.ToArray();
                    return data;
                }
            }
            else
            {
                throw new JsonException("Malformed DICOM json, property 'Value' expected");
            }
        }


        private DicomDataset[] ReadJsonSequence(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return Array.Empty<DicomDataset>();
            }
            var propertyName = ReadPropertyName(ref reader);

            if (propertyName == "Value")
            {
                AssumeAndSkip(ref reader, JsonTokenType.StartArray);
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
                    }
                    else
                    {
                        throw new JsonException("Malformed DICOM json, object expected");
                    }
                }
                AssumeAndSkip(ref reader, JsonTokenType.EndArray);
                var data = childItems.ToArray();
                return data;
            }
            else
            {
                return Array.Empty<DicomDataset>();
            }
        }


        private IByteBuffer ReadJsonOX(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return EmptyBuffer.Value;
            }
            var propertyName = ReadPropertyName(ref reader);

            if (propertyName == "InlineBinary")
            {
                return ReadJsonInlineBinary(ref reader);
            }
            else if (propertyName == "BulkDataURI")
            {
                return ReadJsonBulkDataUri(ref reader);
            }
            return EmptyBuffer.Value;
        }


        private static IByteBuffer ReadJsonInlineBinary(ref Utf8JsonReader reader)
        {
            AssumeAndSkip(ref reader, JsonTokenType.StartArray);
            if (reader.TokenType != JsonTokenType.String) { throw new JsonException("Malformed DICOM json. string expected"); }
            var data = new MemoryByteBuffer(reader.GetBytesFromBase64());
            reader.Read();
            AssumeAndSkip(ref reader, JsonTokenType.EndArray);
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


        private static void Assume(ref Utf8JsonReader reader, JsonTokenType tokenType)
        {
            if (reader.TokenType != tokenType)
            {
                throw new JsonException($"invalid: {tokenType} expected at position {reader.TokenStartIndex}, instead found {reader.TokenType}");
            }
        }


        private static void AssumeAndSkip(ref Utf8JsonReader reader, JsonTokenType tokenType)
        {
            Assume(ref reader, tokenType);
            reader.Read();
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

    }


    internal static class JsonDicomConverterExtensions
    {

        public static string[] GetAsStringArray(this object data) => (string[])data;

        public static string GetFirstOrEmpty(this string[] array) => array.Length > 0 ? array[0] : string.Empty;

        public static string GetSingleOrEmpty(this string[] array) => array.Length > 0 ? array.Single() : string.Empty;

    }

}
