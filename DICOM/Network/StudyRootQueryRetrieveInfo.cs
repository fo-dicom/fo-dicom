using System;

namespace Dicom.Network
{
	public class StudyRootQueryRetrieveInfo : IExtendedNegotiationSubItem
	{
		public byte RelationalQueries { get; private set; }

		public byte DateTimeMatching { get; private set; }

		public byte FuzzySemanticMatching { get; private set; }

		public byte TimezoneQueryAdjustment { get; private set; }

		public StudyRootQueryRetrieveInfo(byte relationalQueries, byte dateTimeMatching, byte fuzzySemanticMatching, byte timezoneQueryAdjustment)
		{
			RelationalQueries = relationalQueries;
			DateTimeMatching = dateTimeMatching;
			FuzzySemanticMatching = fuzzySemanticMatching;
			TimezoneQueryAdjustment = timezoneQueryAdjustment;
		}

		public static StudyRootQueryRetrieveInfo Create(RawPDU raw, out int bytesRead)
		{
			byte relationalQueries = raw.ReadByte("Relational-queries");
			byte dateTimeMatching = raw.ReadByte("Date-Time matching");
			byte fuzzySemanticMatching = raw.ReadByte("Fuzzy semantic matching");
			byte timezoneQueryAdjustment = raw.ReadByte("Timezone query adjustment");

			bytesRead = 4;
			return new StudyRootQueryRetrieveInfo(relationalQueries, dateTimeMatching, fuzzySemanticMatching, timezoneQueryAdjustment);
		}

		public void Write(RawPDU pdu)
		{
			pdu.Write("Relational-queries", RelationalQueries);
			pdu.Write("Date-Time matching", DateTimeMatching);
			pdu.Write("Fuzzy semantic matching", FuzzySemanticMatching);
			pdu.Write("Timezone query adjustment", TimezoneQueryAdjustment);
		}
	}

}
