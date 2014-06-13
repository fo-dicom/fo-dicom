using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Dicom {
	public enum DicomDictionaryFormat {
		XML
	}

	public class DicomDictionaryReader {
		private DicomDictionary _dict;
		private DicomDictionaryFormat _format;
		private Stream _stream;

		public DicomDictionaryReader(DicomDictionary dict, DicomDictionaryFormat format, Stream stream) {
			_dict = dict;
			_format = format;
			_stream = stream;
		}

		public void Process() {
			if (_format == DicomDictionaryFormat.XML)
				ReadDictionaryXML();
		}

		private void ReadDictionaryXML() {
			DicomDictionary dict = _dict;

			XDocument xdoc = XDocument.Load(_stream);

			IEnumerable<XElement> xdicts;

			if (xdoc.Root.Name == "dictionaries") {
				xdicts = xdoc.Root.Elements("dictionary");
			} else {
				XElement xdict = xdoc.Element("dictionary");
				if (xdict == null)
					throw new DicomDataException("Expected <dictionary> root node in DICOM dictionary.");

				List<XElement> dicts = new List<XElement>();
				dicts.Add(xdict);
				xdicts = dicts;
			}

			foreach (var xdict in xdicts) {
				XAttribute creator = xdict.Attribute("creator");
				if (creator != null && !String.IsNullOrEmpty(creator.Value)) {
					dict = _dict[_dict.GetPrivateCreator(creator.Value)];
				} else {
					dict = _dict;
				}

				foreach (XElement xentry in xdict.Elements("tag")) {
					string name = xentry.Value ?? "Unknown";

					string keyword = String.Empty;
					if (xentry.Attribute("keyword") != null)
						keyword = xentry.Attribute("keyword").Value;

					List<DicomVR> vrs = new List<DicomVR>();
					XAttribute xvr = xentry.Attribute("vr");
					if (xvr != null && !String.IsNullOrEmpty(xvr.Value)) {
						string[] vra = xvr.Value.Split('_', '/', '\\', ',', '|');
						foreach (string vr in vra)
							vrs.Add(DicomVR.Parse(vr));
					} else
						vrs.Add(DicomVR.NONE);

					DicomVM vm = DicomVM.Parse(xentry.Attribute("vm").Value);

					bool retired = false;
					XAttribute xretired = xentry.Attribute("retired");
					if (xretired != null && !String.IsNullOrEmpty(xretired.Value) && Boolean.Parse(xretired.Value))
						retired = true;

					string group = xentry.Attribute("group").Value;
					string element = xentry.Attribute("element").Value;
					if (group.ToLower().Contains("x") || element.ToLower().Contains("x")) {
						DicomMaskedTag tag = DicomMaskedTag.Parse(group, element);
						tag.Tag.PrivateCreator = dict.PrivateCreator;
						dict.Add(new DicomDictionaryEntry(tag, name, keyword, vm, retired, vrs.ToArray()));
					} else {
						DicomTag tag = DicomTag.Parse(group + "," + element);
						tag.PrivateCreator = dict.PrivateCreator;
						dict.Add(new DicomDictionaryEntry(tag, name, keyword, vm, retired, vrs.ToArray()));
					}
				}
			}
		}
	}
}
