using Dicom.IO.Buffer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicom.IO
{
    public class JsonDicomConverter : JsonConverter
    {
        #region JsonConverter overrides
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var dataset = value as DicomDataset;

            writer.WriteStartObject();
            foreach (var item in dataset)
            {
                writer.WritePropertyName(item.Tag.Group.ToString("X4") + item.Tag.Element.ToString("X4"));
                WriteJsonDicomItem(writer, item, serializer);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dataset = new DicomDataset();
            if (reader.TokenType == JsonToken.Null)
                return null;
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonReaderException("Malformed DICOM json");
            reader.Read();
            while (reader.TokenType == JsonToken.PropertyName)
            {
                var tagstr = (string)reader.Value;
                DicomTag tag = ParseTag(tagstr);
                reader.Read();
                var item = ReadJsonDicomItem(tag, reader, serializer);
                dataset.Add(item);
                reader.Read();
            }
            if (reader.TokenType != JsonToken.EndObject)
                throw new JsonReaderException("Malformed DICOM json");

            return dataset;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DicomDataset).IsAssignableFrom(objectType);
        }
        #endregion

        #region Utilities
        private static DicomTag ParseTag(string tagstr)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tagstr, @"\A\b[0-9a-fA-F]+\b\Z"))
            {
                var group = Convert.ToUInt16(tagstr.Substring(0, 4), 16);
                var element = Convert.ToUInt16(tagstr.Substring(4), 16);
                DicomTag tag = new DicomTag(group, element);
                return tag;
            }

            var dictEntry = DicomDictionary.Default.FirstOrDefault(entry => entry.Keyword == tagstr || entry.Name == tagstr);
            if (dictEntry != null)
                return dictEntry.Tag;

            return null;
        }

        private static DicomItem CreateDicomItem(DicomTag tag, string vr, object data)
        {
            DicomItem item = null;
            switch (vr)
            {
                case "AE": item = new DicomApplicationEntity(tag, (string[])data); break;
                case "AS": item = new DicomAgeString(tag, (string[])data); break;
                case "AT": item = new DicomAttributeTag(tag, ((string[])data).Select(s => ParseTag(s)).ToArray()); break;
                case "CS": item = new DicomCodeString(tag, (string[])data); break;
                case "DA": item = new DicomDate(tag, (string[])data); break;
                case "DS": item = new DicomDecimalString(tag, (string[])data); break;
                case "DT": item = new DicomDateTime(tag, (string[])data); break;
                case "FD": item = new DicomFloatingPointDouble(tag, ((string[])data).Select(s => double.Parse(s)).ToArray()); break;
                case "FL": item = new DicomFloatingPointSingle(tag, ((string[])data).Select(s => float.Parse(s)).ToArray()); break;
                case "IS": item = new DicomIntegerString(tag, (string[])data); break;
                case "LO": item = new DicomLongString(tag, (string[])data); break;
                case "LT": item = new DicomLongText(tag, ((string[])data)[0]); break;
                case "OB": item = new DicomOtherByte(tag, (IByteBuffer)data); break;
                case "OF": item = new DicomOtherFloat(tag, (IByteBuffer)data); break;
                case "OW": item = new DicomOtherWord(tag, (IByteBuffer)data); break;
                case "PN": item = new DicomPersonName(tag, (string[])data); break;
                case "SH": item = new DicomShortString(tag, (string[])data); break;
                case "SL": item = new DicomSignedLong(tag, ((string[])data).Select(s => int.Parse(s)).ToArray()); break;
                case "SS": item = new DicomSignedShort(tag, ((string[])data).Select(s => short.Parse(s)).ToArray()); break;
                case "ST": item = new DicomShortText(tag, ((string[])data)[0]); break;
                case "SQ": item = new DicomSequence(tag, ((DicomDataset[])data)); break;
                case "TM": item = new DicomTime(tag, (string[])data); break;
                case "UI": item = new DicomUniqueIdentifier(tag, (string[])data); break;
                case "UL": item = new DicomUnsignedLong(tag, ((string[])data).Select(s => uint.Parse(s)).ToArray()); break;
                case "UN": item = new DicomUnknown(tag, (byte[])data); break;
                case "US": item = new DicomUnsignedShort(tag, ((string[])data).Select(s => ushort.Parse(s)).ToArray()); break;
                case "UT": item = new DicomUnlimitedText(tag, ((string[])data)[0]); break;
                default:
                    throw new NotSupportedException("Unsupported value representation");
            }
            return item;
        }
        #endregion

        #region WriteJson helpers
        private void WriteJsonDicomItem(JsonWriter writer, DicomItem item, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("vr");
            writer.WriteValue(item.ValueRepresentation.Code);

            switch (item.ValueRepresentation.Code)
            {
                case "PN":
                    WriteJsonPN(writer, (DicomPersonName)item);
                    break;
                case "SQ":
                    WriteJsonSQ(writer, (DicomSequence)item, serializer);
                    break;
                case "OB":
                // case "OD": /* Currently unsupported by fodicom, and does not appear in the PS3.5 Data Dictionary...
                case "OF":
                case "OW":
                case "UN":
                    WriteJsonOX(writer, (DicomElement)item);
                    break;
              default:
                    WriteJsonString(writer, (DicomStringElement)item);
                    break;
            }
            writer.WriteEndObject();
        }

        private static void WriteJsonString(JsonWriter writer, DicomStringElement elem)
        {
            if (elem.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();
                foreach (var val in elem.Get<string[]>())
                {
                    if (String.IsNullOrEmpty(val))
                        writer.WriteNull();
                    else
                        writer.WriteValue(val);
                }
                writer.WriteEndArray();
            }
        }

        private static void WriteJsonOX(JsonWriter writer, DicomElement elem)
        {
            if (elem.Buffer is BulkUriByteBuffer)
            {
                writer.WritePropertyName("BulkDataURI");
                writer.WriteValue(((BulkUriByteBuffer)elem.Buffer).BulkDataUri);
            }
            else if (elem.Count != 0)
            {
                writer.WritePropertyName("InlineBinary");
                writer.WriteValue(System.Convert.ToBase64String(elem.Buffer.Data));
            }
        }

        private void WriteJsonSQ(JsonWriter writer, DicomSequence seq, JsonSerializer serializer)
        {
            if (seq.Items.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();

                foreach (var child in seq.Items)
                    WriteJson(writer, child, serializer);

                writer.WriteEndArray();
            }
        }

        private static void WriteJsonPN(JsonWriter writer, DicomPersonName pn)
        {
            if (pn.Count != 0)
            {
                writer.WritePropertyName("Value");
                writer.WriteStartArray();

                foreach (var val in pn.Get<string[]>())
                {
                    if (String.IsNullOrEmpty(val))
                    {
                        writer.WriteNull();
                    }
                    else
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("Alphabetic");
                        writer.WriteValue(val);
                        writer.WriteEndObject();
                    }
                }

                writer.WriteEndArray();
            }
        }
        #endregion

        #region ReadJson helpers
        private DicomItem ReadJsonDicomItem(DicomTag tag, JsonReader reader, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonReaderException("Malformed DICOM json");
            reader.Read();
            if (reader.TokenType != JsonToken.PropertyName)
                throw new JsonReaderException("Malformed DICOM json");
            if (reader.Value != "vr")
                reader.Read();
            if (reader.TokenType != JsonToken.String)
                throw new JsonReaderException("Malformed DICOM json");
            string vr = (string)reader.Value;

            object data = null;

            switch (vr)
            {
                case "OB":
                case "OD":
                case "OF":
                case "OW":
                case "UN":
                    data = ReadJsonOX(reader);
                    break;
                case "SQ":
                    data = ReadJsonSQ(reader, serializer);
                    break;
                case "PN":
                    data = ReadJsonPN(reader);
                    break;
                default:
                    data = ReadJsonMultiString(reader);
                    break;
            }

            if (reader.TokenType != JsonToken.EndObject)
                throw new JsonReaderException("Malformed DICOM json");

            DicomItem item = CreateDicomItem(tag, vr, data);

            return item;
        }

        private object ReadJsonMultiString(JsonReader reader)
        {
            reader.Read();
            if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "Value")
            {
                return ReadJsonMultiStringValue(reader);
            }
            else if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "BulkDataURI")
            {
                return ReadJsonBulkDataUri(reader);
            }
            else
            {
                return new string[0];
            }
        }

        private static string[] ReadJsonMultiStringValue(JsonReader reader)
        {
            var childStrings = new List<string>();
            reader.Read();
            if (reader.TokenType != JsonToken.StartArray)
                throw new JsonReaderException("Malformed DICOM json");
            reader.Read();
            while (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Null)
            {
                if (reader.TokenType == JsonToken.Null)
                    childStrings.Add(null);
                else
                    childStrings.Add((string)reader.Value);
                reader.Read();
            }
            if (reader.TokenType != JsonToken.EndArray)
                throw new JsonReaderException("Malformed DICOM json");
            var data = childStrings.ToArray();
            reader.Read();
            return data;
        }

        private string[] ReadJsonPN(JsonReader reader)
        {
            reader.Read();
            if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "Value")
            {
                var childStrings = new List<string>();
                reader.Read();
                if (reader.TokenType != JsonToken.StartArray)
                    throw new JsonReaderException("Malformed DICOM json");
                reader.Read();
                while (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.Null)
                {
                    if (reader.TokenType == JsonToken.Null)
                    {
                        childStrings.Add(null);
                    }
                    else
                    {
                        reader.Read();
                        if (reader.TokenType != JsonToken.PropertyName)
                            throw new JsonReaderException("Malformed DICOM json");
                        if ((string)reader.Value == "Alphabetic")
                        {
                            reader.Read();
                            if (reader.TokenType != JsonToken.String)
                                throw new JsonReaderException("Malformed DICOM json");
                            childStrings.Add((string)reader.Value);
                        }
                        else
                        {
                            reader.Read();
                        }
                        reader.Read();
                        if (reader.TokenType != JsonToken.EndObject)
                            throw new JsonReaderException("Malformed DICOM json");
                    }
                    reader.Read();
                }
                if (reader.TokenType != JsonToken.EndArray)
                    throw new JsonReaderException("Malformed DICOM json");
                var data = childStrings.ToArray();
                reader.Read();
                return data;
            }
            else
            {
                return new string[0];
            }
        }

        private DicomDataset[] ReadJsonSQ(JsonReader reader, JsonSerializer serializer)
        {
            reader.Read();
            if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "Value")
            {
                reader.Read();
                if (reader.TokenType != JsonToken.StartArray)
                    throw new JsonReaderException("Malformed DICOM json");
                reader.Read();
                var childItems = new List<DicomDataset>();
                while (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.Null)
                {
                    childItems.Add((DicomDataset)ReadJson(reader, typeof(DicomDataset), null, serializer));
                    reader.Read();
                }
                var data = childItems.ToArray();
                if (reader.TokenType != JsonToken.EndArray)
                    throw new JsonReaderException("Malformed DICOM json");
                reader.Read();
                return data;
            }
            else
            {
                return new DicomDataset[0];
            }
        }

        private IByteBuffer ReadJsonOX(JsonReader reader)
        {
            reader.Read();
            if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "InlineBinary")
            {
                return ReadJsonInlineBinary(reader);
            }
            else if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "BulkDataURI")
            {
                return ReadJsonBulkDataUri(reader);
            }
            else
                return null;
        }

        private static IByteBuffer ReadJsonInlineBinary(JsonReader reader)
        {
            reader.Read();
            if (reader.TokenType != JsonToken.String)
                throw new JsonReaderException("Malformed DICOM json");
            var data = new MemoryByteBuffer(System.Convert.FromBase64String(reader.Value as string));
            reader.Read();
            return data;
        }

        private static IByteBuffer ReadJsonBulkDataUri(JsonReader reader)
        {
            reader.Read();
            if (reader.TokenType != JsonToken.String)
                throw new JsonReaderException("Malformed DICOM json");
            var data = new BulkUriByteBuffer((string)reader.Value);
            reader.Read();
            return data;
        }
        #endregion
    }
}
