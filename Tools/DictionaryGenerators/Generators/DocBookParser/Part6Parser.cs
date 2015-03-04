using System;
using System.Collections.Generic;
using System.Linq;
using Dicom;

namespace DictionaryGenerators.Generators.DocBookParser
{
	using System.Xml.Linq;

	public class Part6Parser
	{
		public static string DocBookNS = DocBookTable<object>.DocBookNS;

		public static IEnumerable<XElement> Tables(XDocument xml, Func<string, bool> captionPredicate)
		{
			var tables = xml.Descendants(DocBookNS + "table");

			var captionedTables = tables.Where(
						table =>
						table.Descendants(DocBookNS + "caption").SingleOrDefault() != null);

			var correctlyCaptionedTables =
				captionedTables.Where(table => captionPredicate(table.Descendants(DocBookNS + "caption").Single().Value));

			return correctlyCaptionedTables;
		}

		public static IEnumerable<DicomDictionaryEntry> ParseDataDictionaries(string part6filename, string part7filename)
		{
			var part6xml = XDocument.Load(part6filename);
			var part7xml = XDocument.Load(part7filename);

			var registries = new[]
				                 {
					                 "Registry of DICOM Data Elements",
													 "Registry of DICOM File Meta Elements",
					                 "Registry of DICOM Directory Structuring Elements"
				                 };

			var entries = Tables(part6xml, registries.Contains).SelectMany(table => new DataElementsTable(table).Items);

			var commandFields =
				Tables(part7xml, table => table == "Command Fields").SelectMany(table => new DataElementsTable(table).Items);

			var retiredCommandFields =
				Tables(part7xml, table => table == "Retired Command Fields").SelectMany(table => new DataElementsTable(table).Items);

			entries = entries.Concat(commandFields);

			entries = entries.Concat(retiredCommandFields);

			entries = entries.GroupBy(entry => (uint)entry.Tag).Select(es => es.First());

			//  For some Data Elements, no Name or Keyword or VR or VM is specified; these are "placeholders" that are not assigned but will not be reused.

			entries =
				entries.Where(
					entry =>
					!string.IsNullOrWhiteSpace(entry.Name) && !string.IsNullOrWhiteSpace(entry.Keyword)
					&& entry.ValueMultiplicity != null);

			entries = entries.OrderBy(entry => (uint)entry.Tag).ToArray();

			return entries;
		}

		private class OidComparer : IComparer<string>
		{
			public int Compare(string x, string y)
			{
				if (x == y) return 0;

				var xs = x.Replace("\u200b", string.Empty).Split('.');
				var ys = y.Replace("\u200b", string.Empty).Split('.');
				foreach (var pair in xs.Zip(ys, (a, b) => new Tuple<string, string>(a, b)))
				{
					if (pair.Item1 == pair.Item2) continue;
					if (pair.Item1.Length < pair.Item2.Length) return -1;
					if (pair.Item2.Length < pair.Item1.Length) return 1;
					foreach (var charpair in pair.Item1.Zip(pair.Item2, (ca, cb) => new Tuple<char, char>(ca, cb)))
					{
						if (charpair.Item1 < charpair.Item2) return -1;
						if (charpair.Item2 < charpair.Item1) return 1;
					}
				}

				if (xs.Length < ys.Length) return -1;
				if (ys.Length < xs.Length) return 1;

				return 0;
			}
		}

		public static IEnumerable<DicomUID> ParseUIDTables(string part6filename, string part16filename)
		{
			var xml = XDocument.Load(part6filename);
			var part16xml = XDocument.Load(part16filename);

			var uidtables = new[]
				                {
					                "UID Values", "Well-known Frames of Reference",
													"Context Group UID Values",
					                "Standard Color Palettes"
				                };

			return Tables(xml, uidtables.Contains)
				.SelectMany(table => new UidTable(table, part16xml).Items)
				.OrderBy(uid => uid.UID, new OidComparer())
				.GroupBy(uid => uid.UID)
				.Select(uids => uids.First());
		}
	}
}
