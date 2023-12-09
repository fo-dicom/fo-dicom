// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace FellowOakDicom
{

    public enum DicomDictionaryFormat
    {
        XML
    }

    public class DicomDictionaryReader
    {
        private DicomDictionary _dict;

        private DicomDictionaryFormat _format;

        private Stream _stream;

        public DicomDictionaryReader(DicomDictionary dict, DicomDictionaryFormat format, Stream stream)
        {
            _dict = dict;
            _format = format;
            _stream = stream;
        }

        public void Process()
        {
            if (_format == DicomDictionaryFormat.XML) ReadDictionaryXML();
        }

        private void ReadDictionaryXML()
        {
            DicomDictionary dict = _dict;

            var xdoc = XDocument.Load(_stream);

            IEnumerable<XElement> xdicts;

            if (xdoc.Root.Name == "dictionaries")
            {
                xdicts = xdoc.Root.Elements("dictionary");
            }
            else
            {
                XElement xdict = xdoc.Element("dictionary");
                if (xdict == null) throw new DicomDataException("Expected <dictionary> root node in DICOM dictionary.");

                var dicts = new List<XElement>();
                dicts.Add(xdict);
                xdicts = dicts;
            }

            foreach (var xdict in xdicts)
            {
                XAttribute creator = xdict.Attribute("creator");
                if (creator != null && !string.IsNullOrEmpty(creator.Value))
                {
                    dict = _dict[_dict.GetPrivateCreator(creator.Value)];
                }
                else
                {
                    dict = _dict;
                }

                foreach (XElement xentry in xdict.Elements("tag"))
                {
                    string name = xentry.Value ?? "Unknown";

                    string keyword = string.Empty;
                    if (xentry.Attribute("keyword") != null)
                    {
                        keyword = xentry.Attribute("keyword").Value;
                    }

                    var vrs = new List<DicomVR>();
                    XAttribute xvr = xentry.Attribute("vr");
                    if (xvr != null && !string.IsNullOrEmpty(xvr.Value))
                    {
                        string[] vra = xvr.Value.Split('_', '/', '\\', ',', '|');
                        foreach (string vr in vra)
                        {
                            vrs.Add(DicomVR.Parse(vr));
                        }
                    }
                    else
                    {
                        vrs.Add(DicomVR.NONE);
                    }

                    var vm = DicomVM.Parse(xentry.Attribute("vm").Value);

                    bool retired = false;
                    XAttribute xretired = xentry.Attribute("retired");
                    if (xretired != null && !string.IsNullOrEmpty(xretired.Value) && bool.Parse(xretired.Value))
                    {
                        retired = true;
                    }

                    string group = xentry.Attribute("group").Value;
                    string element = xentry.Attribute("element").Value;
                    if (group.ToLower().Contains("x") || element.ToLower().Contains("x"))
                    {
                        var tag = DicomMaskedTag.Parse(group, element);
                        tag.Tag.PrivateCreator = dict.PrivateCreator;
                        dict.Add(new DicomDictionaryEntry(tag, name, keyword, vm, retired, vrs.ToArray()));
                    }
                    else
                    {
                        var tag = DicomTag.Parse(group + "," + element);
                        tag.PrivateCreator = dict.PrivateCreator;
                        dict.Add(new DicomDictionaryEntry(tag, name, keyword, vm, retired, vrs.ToArray()));
                    }
                }
            }
        }
    }
}
