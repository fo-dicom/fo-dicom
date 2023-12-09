// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.Text;

namespace FellowOakDicom.Serialization
{
    /// <summary>
    /// Does the conversion of <see cref="DicomDataset"/> to an XML string
    /// </summary>
    public static class DicomXML
    {
        #region Public methods

        /// <summary>
        /// Converts a <see cref="DicomDataset"/> to a XML-String
        /// </summary>
        /// <param name="dataset">The DicomDataset that is converted to XML-String</param>
        public static string ConvertDicomToXML(DicomDataset dataset)
        {
            string xmlString = DicomToXml(dataset);
            return xmlString;
        }

        /// <summary>
        /// Converts the <see cref="DicomDataset"/> into an XML string.
        /// </summary>
        /// <param name="dataset">Dataset to serialize.</param>
        /// <returns>An XML string.</returns>
        public static string WriteToXml(this DicomDataset dataset)
        {
            return ConvertDicomToXML(dataset);
        }

        /// <summary>
        /// Converts the <see cref="DicomFile"/> into an XML string.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>An XML string.</returns>
        public static string WriteToXml(this DicomFile file)
        {
            return ConvertDicomToXML(file.Dataset);
        }

        #endregion

        #region Private Methods

        private static string DicomToXml(DicomDataset dataset)
        {
            var xmlOutput = new StringBuilder();
            xmlOutput.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            xmlOutput.AppendLine(@"<NativeDicomModel>");

            DicomDatasetToXml(xmlOutput, dataset);

            xmlOutput.AppendLine(@"</NativeDicomModel>");
            return xmlOutput.ToString();
        }

        private static void DicomDatasetToXml(StringBuilder xmlOutput, DicomDataset dataset)
        {
            foreach (var item in dataset)
            {
                if (item is DicomElement)
                {
                    DicomElementToXml(xmlOutput, (DicomElement)item);
                }
                else if (item is DicomSequence)
                {
                    var sq = item as DicomSequence;

                    WriteDicomAttribute(xmlOutput, sq);
                    for (var i = 0; i < sq.Items.Count; i++)
                    {
                        xmlOutput.AppendLine($@"<Item number=""{i + 1}"">");

                        DicomDatasetToXml(xmlOutput, sq.Items[i]);

                        xmlOutput.AppendLine(@"</Item>");
                    }
                    xmlOutput.AppendLine(@"</DicomAttribute>");
                }
                else if (item is DicomFragmentSequence)
                {
                    var sq = item as DicomFragmentSequence;

                    WriteDicomAttribute(xmlOutput, sq);

                    for (var i = 0; i < sq.Fragments.Count; i++)
                    {
                        xmlOutput.AppendLine($@"<Fragment number=""{i + 1}"">");
                        if (sq.Fragments[i].Data?.Length > 0)
                        {
                            var binaryString = GetBinaryBase64(sq.Fragments[i]);
                            xmlOutput.AppendLine($@"<InlineBinary>{binaryString}</InlineBinary>");
                        }
                        xmlOutput.AppendLine(@"</Fragment>");
                    }
                    xmlOutput.AppendLine(@"</DicomAttribute>");
                }
            }
        }

        private static void DicomElementToXml(StringBuilder xmlOutput, DicomElement item)
        {
            WriteDicomAttribute(xmlOutput, item);

            var vr = item.ValueRepresentation.Code;

            if (vr == DicomVRCode.OB || vr == DicomVRCode.OD || vr == DicomVRCode.OF || vr == DicomVRCode.OW ||
                vr == DicomVRCode.OL || vr == DicomVRCode.UN)
            {
                var binaryString = GetBinaryBase64(item.Buffer);
                xmlOutput.AppendLine($@"<InlineBinary>{binaryString}</InlineBinary>");
            }
            else if (vr == DicomVRCode.PN)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    xmlOutput.AppendLine($@"<PersonName number=""{i + 1}"">");
                    xmlOutput.AppendLine(@"<Alphabetic>");

                    var person = new DicomPersonName(item.Tag, item.Get<string>(i));

                    string lastName = person.Last;
                    if (!string.IsNullOrEmpty(lastName)) xmlOutput.AppendLine($@"<FamilyName>{EscapeXml(lastName)}</FamilyName>");
                    string givenName = person.First;
                    if (!string.IsNullOrEmpty(givenName)) xmlOutput.AppendLine($@"<GivenName>{EscapeXml(givenName)}</GivenName>");
                    string middleName = person.Middle;
                    if (!string.IsNullOrEmpty(middleName)) xmlOutput.AppendLine($@"<MiddleName>{EscapeXml(middleName)}</MiddleName>");
                    string prefixName = person.Prefix;
                    if (!string.IsNullOrEmpty(prefixName)) xmlOutput.AppendLine($@"<NamePrefix>{EscapeXml(prefixName)}</NamePrefix>");
                    string suffixName = person.Suffix;
                    if (!string.IsNullOrEmpty(suffixName)) xmlOutput.AppendLine($@"<NameSuffix>{EscapeXml(suffixName)}</NameSuffix>");

                    xmlOutput.AppendLine(@"</Alphabetic>");
                    xmlOutput.AppendLine(@"</PersonName>");
                }
            }
            else
            {
                for (int i = 0; i < item.Count; i++)
                {
                    var valueString = EscapeXml(item.Get<string>(i));
                    xmlOutput.AppendLine($@"<Value number=""{i + 1}"">{valueString}</Value>");
                }
            }

            xmlOutput.AppendLine(@"</DicomAttribute>");
        }

        private static void WriteDicomAttribute(StringBuilder xmlOutput, DicomItem item)
        {
            if (item.Tag.IsPrivate && item.Tag.PrivateCreator != null)
            {
                xmlOutput.AppendLine($@"<DicomAttribute tag=""{item.Tag.Group:X4}{item.Tag.Element:X4}"" vr=""{item.ValueRepresentation.Code}"" keyword=""{item.Tag.DictionaryEntry.Keyword}"" privateCreator=""{item.Tag.PrivateCreator.Creator}"">");
            }
            else
            {
                xmlOutput.AppendLine($@"<DicomAttribute tag=""{item.Tag.Group:X4}{item.Tag.Element:X4}"" vr=""{item.ValueRepresentation.Code}"" keyword=""{item.Tag.DictionaryEntry.Keyword}"">");
            }

        }

        private static string GetBinaryBase64(IByteBuffer buffer)
        {
            if (buffer == null) return string.Empty;
            return Convert.ToBase64String(buffer.Data);
        }
        private static string EscapeXml(string text)
        {
            if (text == null)
            {
                return string.Empty;
            }
            return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }

        #endregion
    }
}
