// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using FellowOakDicom.IO.Buffer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FellowOakDicom.Serialization
{

    /// <summary>
    /// Converts a DicomDataset object to and from JSON using the NewtonSoft Json.NET library
    /// </summary>
    public class JsonDicomConverter : JsonConverter
    {
        private readonly bool _writeTagsAsKeywords;
        private readonly bool _autoValidate;
        private readonly NumberSerializationMode _numberSerializationMode;
        private readonly static Encoding[] _jsonTextEncodings = { Encoding.UTF8 };
        private readonly static char _personNameComponentGroupDelimiter = '=';
        private readonly static string[] _personNameComponentGroupNames = { "Alphabetic", "Ideographic", "Phonetic" };

        /// <summary>
        /// Initialize the JsonDicomConverter.
        /// </summary>
        /// <param name="writeTagsAsKeywords">Whether to write the json keys as DICOM keywords instead of tags. This makes the json non-compliant to DICOM JSON.</param>
        /// <param name="autoValidate">Whether the content of DicomItems shall be validated when deserializing.</param>
        /// <param name="numberSerializationMode">Defines how numbers should be serialized. Default 'AsNumber', will throw errors when a number is not parsable.</param>
        public JsonDicomConverter(bool writeTagsAsKeywords = false, bool autoValidate = true, NumberSerializationMode numberSerializationMode = NumberSerializationMode.AsNumber)
        {
            _writeTagsAsKeywords = writeTagsAsKeywords;
            _autoValidate = autoValidate;
            _numberSerializationMode = numberSerializationMode;
        }

        #region JsonConverter overrides

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer _)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var dataset = (DicomDataset)value;

            writer.WriteStartObject();
            foreach (var item in dataset)
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

                WriteJsonDicomItem(writer, item);
            }
            writer.WriteEndObject();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var itemObject = JToken.Load(reader);
            var dataset = ReadJsonDataset(itemObject);
            return dataset;
        }

        private DicomDataset ReadJsonDataset(JToken obj)
        {
            var dataset = _autoValidate
                ? new DicomDataset()
                : new DicomDataset().NotValidated();
            if (obj.Type == JTokenType.Null) { return null; }
            if (!(obj is JObject itemObject)) { throw new JsonReaderException("Malformed DICOM json"); }

            foreach (var property in itemObject.Properties())
            {
                var tagstr = property.Name;
                DicomTag tag = ParseTag(tagstr);
                var item = ReadJsonDicomItem(tag, property.Value);
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
        public override bool CanConvert(Type objectType)
        {
            return typeof(DicomDataset).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
        #endregion

        /// <summary>
        /// Create an instance of a IBulkDataUriByteBuffer. Override this method to use a different IBulkDataUriByteBuffer implementation in applications.
        /// </summary>
        /// <param name="bulkDataUri">The URI of a bulk data element as defined in <see cref="!:http://dicom.nema.org/medical/dicom/current/output/chtml/part19/chapter_A.html#table_A.1.5-2">Table A.1.5-2 in PS3.19</see>.</param>
        /// <returns>An instance of a Bulk URI Byte buffer.</returns>
        protected virtual IBulkDataUriByteBuffer CreateBulkDataUriByteBuffer(string bulkDataUri)
        {
            return new BulkDataUriByteBuffer(bulkDataUri);
        }

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
            DicomItem item;
            switch (vr)
            {
                case "AE":
                    item = new DicomApplicationEntity(tag, (string[])data);
                    break;
                case "AS":
                    item = new DicomAgeString(tag, (string[])data);
                    break;
                case "AT":
                    item = new DicomAttributeTag(tag, ((string[])data).Select(ParseTag).ToArray());
                    break;
                case "CS":
                    item = new DicomCodeString(tag, (string[])data);
                    break;
                case "DA":
                    item = new DicomDate(tag, (string[])data);
                    break;
                case "DS":
                    if (data is IByteBuffer dataBufferDS)
                    {
                        item = new DicomDecimalString(tag, dataBufferDS);
                    }
                    else if (data is decimal[] dataAsNumbers)
                    {
                        item = new DicomDecimalString(tag, dataAsNumbers);
                    }
                    else
                    {
                        item = new DicomDecimalString(tag, (string[])data);
                    }
                    break;
                case "DT":
                    item = new DicomDateTime(tag, (string[])data);
                    break;
                case "FD":
                    if (data is IByteBuffer dataBufferFD)
                    {
                        item = new DicomFloatingPointDouble(tag, dataBufferFD);
                    }
                    else
                    {
                        item = new DicomFloatingPointDouble(tag, (double[])data);
                    }
                    break;
                case "FL":
                    if (data is IByteBuffer dataBufferFL)
                    {
                        item = new DicomFloatingPointSingle(tag, dataBufferFL);
                    }
                    else
                    {
                        item = new DicomFloatingPointSingle(tag, (float[])data);
                    }
                    break;
                case "IS":
                    if (data is IByteBuffer dataBufferIS)
                    {
                        item = new DicomIntegerString(tag, dataBufferIS);
                    }
                    else if (data is string[] dataAsStrings)
                    {
                        item = new DicomIntegerString(tag, dataAsStrings);
                    }
                    else
                    {
                        item = new DicomIntegerString(tag, (int[])data);
                    }
                    break;
                case "LO":
                    item = new DicomLongString(tag, (string[])data);
                    break;
                case "LT":
                    if (data is IByteBuffer dataBufferLT)
                    {
                        item = new DicomLongText(tag, _jsonTextEncodings, dataBufferLT);
                    }
                    else
                    {
                        item = new DicomLongText(tag, data.AsStringArray().SingleOrEmpty());
                    }
                    break;
                case "OB":
                    item = new DicomOtherByte(tag, (IByteBuffer)data);
                    break;
                case "OD":
                    item = new DicomOtherDouble(tag, (IByteBuffer)data);
                    break;
                case "OF":
                    item = new DicomOtherFloat(tag, (IByteBuffer)data);
                    break;
                case "OL":
                    item = new DicomOtherLong(tag, (IByteBuffer)data);
                    break;
                case "OW":
                    item = new DicomOtherWord(tag, (IByteBuffer)data);
                    break;
                case "OV":
                    item = new DicomOtherVeryLong(tag, (IByteBuffer)data);
                    break;
                case "PN":
                    item = new DicomPersonName(tag, (string[])data);
                    break;
                case "SH":
                    item = new DicomShortString(tag, (string[])data);
                    break;
                case "SL":
                    if (data is IByteBuffer dataBufferSL)
                    {
                        item = new DicomSignedLong(tag, dataBufferSL);
                    }
                    else
                    {
                        item = new DicomSignedLong(tag, (int[])data);
                    }
                    break;
                case "SQ":
                    item = new DicomSequence(tag, ((DicomDataset[])data));
                    break;
                case "SS":
                    if (data is IByteBuffer dataBufferSS)
                    {
                        item = new DicomSignedShort(tag, dataBufferSS);
                    }
                    else
                    {
                        item = new DicomSignedShort(tag, (short[])data);
                    }
                    break;
                case "ST":
                    if (data is IByteBuffer dataBufferST)
                    {
                        item = new DicomShortText(tag, _jsonTextEncodings, dataBufferST);
                    }
                    else
                    {
                        item = new DicomShortText(tag, data.AsStringArray().FirstOrEmpty());
                    }
                    break;
                case "SV":
                    if (data is IByteBuffer dataBufferSV)
                    {
                        item = new DicomSignedVeryLong(tag, dataBufferSV);
                    }
                    else if (data is string[] dataAsStrings)
                    {
                        var dataAsLongs = dataAsStrings.Select(s => long.Parse(s)).ToArray();
                        item = new DicomSignedVeryLong(tag, dataAsLongs);
                    }
                    else
                    {
                        item = new DicomSignedVeryLong(tag, (long[])data);
                    }
                    break;
                case "TM":
                    item = new DicomTime(tag, (string[])data);
                    break;
                case "UC":
                    if (data is IByteBuffer dataBufferUC)
                    {
                        item = new DicomUnlimitedCharacters(tag, _jsonTextEncodings, dataBufferUC);
                    }
                    else
                    {
                        item = new DicomUnlimitedCharacters(tag, data.AsStringArray().SingleOrDefault());
                    }
                    break;
                case "UI":
                    item = new DicomUniqueIdentifier(tag, (string[])data);
                    break;
                case "UL":
                    if (data is IByteBuffer dataBufferUL)
                    {
                        item = new DicomUnsignedLong(tag, dataBufferUL);
                    }
                    else
                    {
                        item = new DicomUnsignedLong(tag, (uint[])data);
                    }
                    break;
                case "UN":
                    item = new DicomUnknown(tag, (IByteBuffer)data);
                    break;
                case "UR":
                    item = new DicomUniversalResource(tag, data.AsStringArray().SingleOrEmpty());
                    break;
                case "US":
                    if (data is IByteBuffer dataBufferUS)
                    {
                        item = new DicomUnsignedShort(tag, dataBufferUS);
                    }
                    else
                    {
                        item = new DicomUnsignedShort(tag, (ushort[])data);
                    }
                    break;
                case "UT":
                    if (data is IByteBuffer dataBufferUT)
                    {
                        item = new DicomUnlimitedText(tag, _jsonTextEncodings, dataBufferUT);
                    }
                    else
                    {
                        item = new DicomUnlimitedText(tag, data.AsStringArray().SingleOrEmpty());
                    }
                    break;
                case "UV":
                    if (data is IByteBuffer dataBufferUV)
                    {
                        item = new DicomUnsignedVeryLong(tag, dataBufferUV);
                    }
                    else if (data is string[] dataAsStrings)
                    {
                        var dataAsULongs = dataAsStrings.Select(s => ulong.Parse(s)).ToArray();
                        item = new DicomUnsignedVeryLong(tag, dataAsULongs);
                    }
                    else
                    {
                        item = new DicomUnsignedVeryLong(tag, (ulong[])data);
                    }
                    break;
                default:
                    throw new NotSupportedException("Unsupported value representation");
            }
            return item;
        }

        #endregion

        #region WriteJson helpers

        private void WriteJsonDicomItem(JsonWriter writer, DicomItem item)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("vr");
            writer.WriteValue(item.ValueRepresentation.Code);

            switch (item.ValueRepresentation.Code)
            {
                case "PN":
                    WriteJsonPersonName(writer, (DicomPersonName)item);
                    break;
                case "SQ":
                    WriteJsonSequence(writer, (DicomSequence)item);
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
                    WriteJsonElement<float>(writer, (DicomElement)item);
                    break;
                case "FD":
                    WriteJsonElement<double>(writer, (DicomElement)item);
                    break;
                case "IS":
                    WriteJsonAsNumberOrString(writer, (DicomElement)item, () =>
                        WriteJsonElement<int>(writer, (DicomElement)item));
                    break;
                case "SL":
                    WriteJsonElement<int>(writer, (DicomElement)item);
                    break;
                case "SS":
                    WriteJsonElement<short>(writer, (DicomElement)item);
                    break;
                case "SV":
                    WriteJsonAsNumberOrString(writer, (DicomElement)item, () =>
                        WriteJsonElement<long>(writer, (DicomElement)item));
                    break;
                case "UL":
                    WriteJsonElement<uint>(writer, (DicomElement)item);
                    break;
                case "US":
                    WriteJsonElement<ushort>(writer, (DicomElement)item);
                    break;
                case "UV":
                    WriteJsonAsNumberOrString(writer, (DicomElement)item, () =>
                        WriteJsonElement<ulong>(writer, (DicomElement)item));
                    break;
                case "DS":
                    WriteJsonAsNumberOrString(writer, (DicomElement)item, () =>
                        WriteJsonDecimalString(writer, (DicomElement)item));
                    break;
                case "AT":
                    WriteJsonAttributeTag(writer, (DicomElement)item);
                    break;
                default:
                    WriteJsonElement<string>(writer, (DicomElement)item);
                    break;
            }

            writer.WriteEndObject();
        }

        private void WriteJsonAsNumberOrString(JsonWriter writer, DicomElement elem, Action numberWriterAction)
        {
            if (_numberSerializationMode == NumberSerializationMode.AsString)
            {
                WriteJsonElement<string>(writer, elem);
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
                        WriteJsonElement<string>(writer, elem);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private void WriteJsonDecimalString(JsonWriter writer, DicomElement elem)
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
                    writerActions.Add(writer.WriteNull);
                }
                else
                {
                    var fix = FixDecimalString(val);
                    if (ulong.TryParse(fix, NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong xulong))
                    {
                        writerActions.Add(() => writer.WriteValue(xulong));
                    }
                    else if (long.TryParse(fix, NumberStyles.Integer, CultureInfo.InvariantCulture, out long xlong))
                    {
                        writerActions.Add(() => writer.WriteValue(xlong));
                    }
                    else if (decimal.TryParse(fix, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal xdecimal))
                    {
                        writerActions.Add(() => writer.WriteValue(xdecimal));
                    }
                    else if (double.TryParse(fix, NumberStyles.Float, CultureInfo.InvariantCulture, out double xdouble))
                    {
                        writerActions.Add(() => writer.WriteValue(xdouble));
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

        private static void WriteJsonElement<T>(JsonWriter writer, DicomElement elem)
        {
            if (elem.Count != 0)
            {
                var values = elem.Get<T[]>();
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var val in values)
                {
                    if (val == null || (typeof(T) == typeof(string) && val.Equals("")))
                    {
                        writer.WriteNull();
                    }
                    else
                    {
                        writer.WriteValue(val);
                    }
                }
                writer.WriteEndArray();
            }
        }

        private static void WriteJsonAttributeTag(JsonWriter writer, DicomElement elem)
        {
            if (elem.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var val in elem.Get<DicomTag[]>())
                {
                    if (val == null) { writer.WriteNull(); }
                    else { writer.WriteValue(((uint)val).ToString("X8")); }
                }
                writer.WriteEndArray();
            }
        }

        private static void WriteJsonOther(JsonWriter writer, DicomElement elem)
        {
            if (elem.Buffer is IBulkDataUriByteBuffer buffer)
            {
                writer.WritePropertyName("BulkDataURI");
                writer.WriteValue(buffer.BulkDataUri);
            }
            else if (elem.Count != 0)
            {
                writer.WritePropertyName("InlineBinary");
                writer.WriteValue(Convert.ToBase64String(elem.Buffer.Data));
            }
        }

        private void WriteJsonSequence(JsonWriter writer, DicomSequence seq)
        {
            if (seq.Items.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();

                foreach (var child in seq.Items) { WriteJson(writer, child, null); }

                writer.WriteEndArray();
            }
        }

        private static void WriteJsonPersonName(JsonWriter writer, DicomPersonName pn)
        {
            if (pn.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();

                foreach (var val in pn.Get<string[]>())
                {
                    if (string.IsNullOrEmpty(val))
                    {
                        writer.WriteNull();
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
                                writer.WriteValue(componentGroupValue);
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

        private DicomItem ReadJsonDicomItem(DicomTag tag, JToken token)
        {
            var typeProp = token["vr"] ?? throw new JsonReaderException("Malformed DICOM json");

            string vr = typeProp.Value<string>();

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
                    data = ReadJsonOX(token);
                    break;
                case "SQ":
                    data = ReadJsonSequence(token);
                    break;
                case "PN":
                    data = ReadJsonPersonName(token);
                    break;
                case "FL":
                    data = ReadJsonMultiNumber<float>(token);
                    break;
                case "FD":
                    data = ReadJsonMultiNumber<double>(token);
                    break;
                case "IS":
                    data = ReadJsonMultiNumberOrString<int>(token);
                    break;
                case "SL":
                    data = ReadJsonMultiNumber<int>(token);
                    break;
                case "SS":
                    data = ReadJsonMultiNumber<short>(token);
                    break;
                case "SV":
                    data = ReadJsonMultiNumberOrString<long>(token);
                    break;
                case "UL":
                    data = ReadJsonMultiNumber<uint>(token);
                    break;
                case "US":
                    data = ReadJsonMultiNumber<ushort>(token);
                    break;
                case "UV":
                    data = ReadJsonMultiNumberOrString<ulong>(token);
                    break;
                case "DS":
                    data = ReadJsonMultiNumberOrString<decimal>(token);
                    break;
                default:
                    data = ReadJsonMultiString(token);
                    break;
            }

            DicomItem item = CreateDicomItem(tag, vr, data);
            return item;
        }

        private object ReadJsonMultiString(JToken itemObject)
        {
            if (itemObject["Value"] is JArray items)
            {
                return ReadJsonMultiStringValue(items);
            }
            else if (itemObject["BulkDataURI"] is JToken bulk)
            {
                return ReadJsonBulkDataUri(bulk);
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        private static string[] ReadJsonMultiStringValue(JArray items)
        {
            var childStrings = new List<string>();
            foreach (var item in items)
            {
                if (item.Type == JTokenType.Null)
                {
                    childStrings.Add(null);
                }
                else
                {
                    childStrings.Add(item.Value<string>());
                }
            }
            var data = childStrings.ToArray();
            return data;
        }

        private object ReadJsonMultiNumberOrString<T>(JToken itemObject)
        {
            if (itemObject["Value"] is JArray items)
            {
                if (items.Count == 0)
                {
                    return Array.Empty<T>();
                }
                else if (items.First().Type == JTokenType.String)
                {
                    return ReadJsonMultiStringValue(items);
                }
                else
                {
                    return ReadJsonMultiNumberValue<T>(items);
                }
            }
            else if (itemObject["BulkDataURI"] is JToken bulk)
            {
                return ReadJsonBulkDataUri(bulk);
            }
            else
            {
                return Array.Empty<T>();
            }
        }

        private object ReadJsonMultiNumber<T>(JToken itemObject)
        {
            if (itemObject["Value"] is JToken token)
            {
                return ReadJsonMultiNumberValue<T>(token);
            }
            else if (itemObject["BulkDataURI"] is JToken bulk)
            {
                return ReadJsonBulkDataUri(bulk);
            }
            else
            {
                return Array.Empty<T>();
            }
        }

        private static T[] ReadJsonMultiNumberValue<T>(JToken token)
        {
            if (!(token is JArray tokens)) { return Array.Empty<T>(); }
            var childValues = new List<T>();
            foreach (var item in tokens)
            {
                if (!(item.Type == JTokenType.String && item.Value<string>() == "NaN")
                    && !(item.Type == JTokenType.Float || item.Type == JTokenType.Integer))
                {
                    throw new JsonReaderException("Malformed DICOM json");
                }
                childValues.Add((T)Convert.ChangeType(item.Value<object>(), typeof(T)));
            }
            var data = childValues.ToArray();
            return data;
        }

        private string[] ReadJsonPersonName(JToken itemObject)
        {
            if (itemObject["Value"] is JArray tokens)
            {
                var childStrings = new List<string>();
                foreach (var item in tokens)
                {
                    if (item.Type == JTokenType.Null)
                    {
                        childStrings.Add(null);
                    }
                    else
                    {
                        // parse
                        var componentGroupCount = 3;
                        var componentGroupValues = new string[componentGroupCount];
                        for (int i = 0; i < componentGroupCount; i++)
                        {
                            if (item[_personNameComponentGroupNames[i]] is JToken jToken)
                            {
                                if (jToken.Type != JTokenType.String) { throw new JsonReaderException("Malformed DICOM json"); }
                                componentGroupValues[i] = jToken.Value<string>();
                            }
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

                        childStrings.Add(pnVal);
                    }
                }
                var data = childStrings.ToArray();
                return data;
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        private DicomDataset[] ReadJsonSequence(JToken itemObject)
        {
            if (itemObject["Value"] is JArray items)
            {
                var childItems = new List<DicomDataset>();
                foreach (var item in items)
                {
                    childItems.Add(ReadJsonDataset(item));
                }
                var data = childItems.ToArray();
                return data;
            }
            else
            {
                return Array.Empty<DicomDataset>();
            }
        }

        private IByteBuffer ReadJsonOX(JToken itemObject)
        {
            if (itemObject["InlineBinary"] is JToken inline)
            {
                return ReadJsonInlineBinary(inline);
            }
            else if (itemObject["BulkDataURI"] is JToken bulk)
            {
                return ReadJsonBulkDataUri(bulk);
            }
            return EmptyBuffer.Value;
        }

        private static IByteBuffer ReadJsonInlineBinary(JToken token)
        {
            if (token is JArray array)
            {
                token = array.First();
            }
            if (token.Type != JTokenType.String) { throw new JsonReaderException("Malformed DICOM json"); }
            var data = new MemoryByteBuffer(Convert.FromBase64String(token.Value<string>()));
            return data;
        }

        private IBulkDataUriByteBuffer ReadJsonBulkDataUri(JToken token)
        {
            if (token.Type != JTokenType.String) { throw new JsonReaderException("Malformed DICOM json"); }
            var data = CreateBulkDataUriByteBuffer(token.Value<string>());
            return data;
        }

        #endregion
    }


    internal static class JsonDicomConverterExtensions
    {

        public static string[] AsStringArray(this object data) => (string[])data;

        public static string FirstOrEmpty(this string[] array) => array.Length > 0 ? array[0] : string.Empty;

        public static string SingleOrEmpty(this string[] array) => array.Length > 0 ? array.Single() : string.Empty;

    }
}
