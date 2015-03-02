using System;
using System.Collections.Generic;
using System.Linq;
using Dicom;

namespace DictionaryGenerators.Generators.DocBookParser
{
	using System.Xml.Linq;

	public abstract class DocBookTable<T>
  {
    public static readonly string DocBookNS = "{http://docbook.org/ns/docbook}";
    public static readonly string XmlNS = "{http://www.w3.org/XML/1998/namespace}";

		private T[] items_;

		private IEnumerable<XElement> ExpandSpans(XElement row)
    {
      var s = new List<XElement>();

      foreach (var td in row.Descendants(DocBookNS + "td"))
      {
        s.Add(td);
        if (td.Attributes().Any(attr => attr.Name == XmlNS + "colspan"))
          s.AddRange(Enumerable.Repeat<XElement>(null, int.Parse(td.Attribute(XmlNS+"colspan").Value)));
      }

      return s.ToArray();
    }

		private Dictionary<TKey, TValue> DictFromKeysAndValues<TKey, TValue>(
			IEnumerable<TKey> keys,
			IEnumerable<TValue> values)
		{
			var dict = new Dictionary<TKey, TValue>();
			foreach (var pair in keys.Zip(values, (x, y) => new Tuple<TKey, TValue>(x, y)))
				dict[pair.Item1] = pair.Item2;

			return dict;
		}

		public DocBookTable(XElement tableElement)
		{
			this.Table = tableElement;
		}

		public XElement Table { get; set; }

		private void Parse_() {
			this.Caption = Table.Descendants(DocBookNS + "caption").Single().Value;
      this.Headings =
        this.Table.Descendants(DocBookNS + "thead").Single().Descendants(DocBookNS + "th").Select(th => th.Value).ToArray();
      this.Items =
        this.Table.Descendants(DocBookNS + "tbody")
          .Single()
          .Descendants(DocBookNS + "tr")
          .Select(row => ParseRow(DictFromKeysAndValues(this.Headings, this.ExpandSpans(row))))
					.Where(entry => entry != null)
          .ToArray();
      Id = this.Table.Attribute(XmlNS + "id").Value;
    }

    public string Id { get; set; }
    public string Caption { get; set; }
    public string[] Headings { get; set; }

		public T[] Items
		{
			get
			{
				if (items_ == null) this.Parse_();
				return items_;
			}
			set
			{
				items_ = value;
			}
		}

		public abstract T ParseRow(Dictionary<string, XElement> row);
  }
}
