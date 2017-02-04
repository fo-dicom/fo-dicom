using Dicom;
using Dicom.IO.Buffer;
using System;
using System.Text;

namespace DICOM.Shared.Log
{

    public class DicomXML
    {
       
        string _xmlString;

        public DicomXML(DicomDataset dataset)
        {
            _xmlString = DicomToXml(dataset);
        }

        public string XmlString => _xmlString;

        #region Private Methods

        private string DicomToXml(DicomDataset dataset)
        {
            var xmlOutput = new StringBuilder();
            xmlOutput.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            xmlOutput.AppendLine(@"<NativeDicomModel>");

            DicomDatasetToXml(xmlOutput, dataset);

            xmlOutput.AppendLine(@"</NativeDicomModel>");
            return xmlOutput.ToString();
        }

        private void DicomDatasetToXml(StringBuilder xmlOutput, DicomDataset dataset)
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
                    for (int i = 0; i < sq.Items.Count; i++)
                    {
                        xmlOutput.AppendFormat(@"<Item number=""{0}"">", i + 1);
                        xmlOutput.AppendLine();

                        DicomDatasetToXml(xmlOutput, sq.Items[i]);

                        xmlOutput.AppendLine(@"</Item>");
                    }
                    xmlOutput.AppendLine(@"</DicomAttribute>");
                }
            }
        }

        private void DicomElementToXml(StringBuilder xmlOutput, DicomElement item)
        {
            WriteDicomAttribute(xmlOutput, item);

            var vr = item.ValueRepresentation.Code;

            if (vr == DicomVRCode.OB || vr == DicomVRCode.OD || vr == DicomVRCode.OF || vr == DicomVRCode.OW ||
                vr == DicomVRCode.OL || vr == DicomVRCode.UN)
            {
                string binaryString = GetBinaryBase64(item);
                xmlOutput.AppendFormat(@"<InlineBinary>{0}</InlineBinary>", binaryString);
                xmlOutput.AppendLine();
            }
            else
            {
                for (int i = 0; i < item.Count; i++)
                {
                    var valueString = EscapeXml(item.Get<String>(i));
                    xmlOutput.AppendFormat(@"<Value number=""{0}"">{1}</Value>", i + 1, valueString);
                    xmlOutput.AppendLine();
                }
            }

            xmlOutput.AppendLine(@"</DicomAttribute>");
        }

        private void WriteDicomAttribute(StringBuilder xmlOutput, DicomItem item)
        {
            if (item.Tag.IsPrivate && item.Tag.PrivateCreator != null)
            {
                xmlOutput.AppendFormat(@"<DicomAttribute tag=""{0:X4}{1:X4}"" vr=""{2}"" keyword=""{3}"" privateCreator=""{4}"">", item.Tag.Group, item.Tag.Element, item.ValueRepresentation.Code, item.Tag.DictionaryEntry.Keyword, item.Tag.PrivateCreator.Creator);
            }
            else
            {
                xmlOutput.AppendFormat(@"<DicomAttribute tag=""{0:X4}{1:X4}"" vr=""{2}"" keyword=""{3}"">", item.Tag.Group, item.Tag.Element, item.ValueRepresentation.Code, item.Tag.DictionaryEntry.Keyword);
            }
            xmlOutput.AppendLine();
        }

        private string GetBinaryBase64(DicomElement item)
        {
            IByteBuffer buffer = item.Buffer;
            if (buffer == null) return string.Empty;
            return Convert.ToBase64String(buffer.Data);
        }

        private string EscapeXml(string text)
        {
            return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }

        #endregion

    }
}
