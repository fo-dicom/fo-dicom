using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dicom.Network
{
	public class RootQueryRetrieveInfoMove : IExtendedNegotiationSubItem
	{
		public byte? RelationalRetrieval { get; private set; }

		public RootQueryRetrieveInfoMove(byte? relationalRetrieval)
		{
			RelationalRetrieval = relationalRetrieval;
		}

		public static RootQueryRetrieveInfoMove Create(RawPDU pdu, int itemSize, out int bytesRead)
		{
			bytesRead = 0;
			byte? relationalRetrieval = null;
			if (itemSize > 0)
			{
				relationalRetrieval = pdu.ReadByte("Relational Retrieval");

				bytesRead = 1;
			}

			return new RootQueryRetrieveInfoMove(relationalRetrieval);
		}

		public void Write(RawPDU pdu)
		{
			if (RelationalRetrieval.HasValue)
			{
				pdu.Write("Relational RelationalRetrieval", RelationalRetrieval.Value);
			}
		}
	}
}
