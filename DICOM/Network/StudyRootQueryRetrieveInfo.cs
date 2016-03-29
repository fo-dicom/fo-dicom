using System;

namespace Dicom.Network
{
	public class RootQueryRetrieveInfoFind : IExtendedNegotiationSubItem
	{
		public byte? RelationalQueries { get; private set; }

		public byte? DateTimeMatching { get; private set; }

		public byte? FuzzySemanticMatching { get; private set; }

		public byte? TimezoneQueryAdjustment { get; private set; }

		public byte? EnhancedMultiFrameImageConversion { get; private set; }

		public RootQueryRetrieveInfoFind(byte? relationalQueries, byte? dateTimeMatching, byte? fuzzySemanticMatching, byte? timezoneQueryAdjustment, byte? enhancedMultiFrameImageConversion)
		{
			RelationalQueries = relationalQueries;
			DateTimeMatching = dateTimeMatching;
			FuzzySemanticMatching = fuzzySemanticMatching;
			TimezoneQueryAdjustment = timezoneQueryAdjustment;
			EnhancedMultiFrameImageConversion = enhancedMultiFrameImageConversion;
		}

		public static RootQueryRetrieveInfoFind Create(RawPDU raw, int itemSize, out int bytesRead)
		{
			byte? relationalQueries = null;
			byte? dateTimeMatching = null;
			byte? fuzzySemanticMatching = null;
			byte? timezoneQueryAdjustment = null;
			byte? enhancedMultiFrameImageConversion = null;

			bytesRead = 0;
			if (itemSize > 0)
			{
				relationalQueries = raw.ReadByte("Relational-queries");
				bytesRead++;

				if (itemSize > 1)
				{
					dateTimeMatching = raw.ReadByte("Date-Time matching");
					bytesRead++;

					if (itemSize > 2)
					{
						fuzzySemanticMatching = raw.ReadByte("Fuzzy semantic matching");
						bytesRead++;

						if (itemSize > 3)
						{
							timezoneQueryAdjustment = raw.ReadByte("Timezone query adjustment");
							bytesRead++;

							if (itemSize > 4)
							{
								enhancedMultiFrameImageConversion = raw.ReadByte("Enhanced Multi-Frame Image Conversion");
								bytesRead++;
							}
						}
					}
				}
			}

			return new RootQueryRetrieveInfoFind(relationalQueries, dateTimeMatching, fuzzySemanticMatching, timezoneQueryAdjustment, enhancedMultiFrameImageConversion);
		}

		public void Write(RawPDU pdu)
		{
			if (RelationalQueries.HasValue)
			{
				pdu.Write("Relational-queries", RelationalQueries.Value);

				if (DateTimeMatching.HasValue)
				{
					pdu.Write("Date-Time matching", DateTimeMatching.Value);
					if (FuzzySemanticMatching.HasValue)
					{
						pdu.Write("Fuzzy semantic matching", FuzzySemanticMatching.Value);
						if (TimezoneQueryAdjustment.HasValue)
						{
							pdu.Write("Timezone query adjustment", TimezoneQueryAdjustment.Value);

							if (EnhancedMultiFrameImageConversion.HasValue)
							{
								pdu.Write("Enhanced Multi-Frame Image Conversion", EnhancedMultiFrameImageConversion.Value);
							}
						}
					}
				}
			}
		}
	}

}
