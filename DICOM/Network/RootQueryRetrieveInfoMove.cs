// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    /// <summary>
    /// Implementation of a Root Query Retrieve Info Move sub-item.
    /// </summary>
    public class RootQueryRetrieveInfoMove : IExtendedNegotiationSubItem
    {
        /// <summary>
        /// Initializes an instance of the <see cref="RootQueryRetrieveInfoMove"/> class.
        /// </summary>
        /// <param name="relationalRetrieval">Relational retrieval flag.</param>
        public RootQueryRetrieveInfoMove(byte? relationalRetrieval)
        {
            RelationalRetrieval = relationalRetrieval;
        }

        /// <summary>
        /// Gets or sets the relational retrieval flag.
        /// </summary>
        public byte? RelationalRetrieval { get; set; }

        /// <summary>
        /// Factory method for creating a <see cref="RootQueryRetrieveInfoMove"/> instance.
        /// </summary>
        /// <param name="pdu">Raw PDU.</param>
        /// <param name="itemSize">Item size.</param>
        /// <param name="bytesRead">Bytes read.</param>
        /// <returns>The created <see cref="RootQueryRetrieveInfoMove"/> instance.</returns>
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

        /// <summary>
        /// Write PDU data.
        /// </summary>
        /// <param name="pdu">PDU data to write.</param>
        public void Write(RawPDU pdu)
        {
            if (RelationalRetrieval.HasValue)
            {
                pdu.Write("Relational RelationalRetrieval", RelationalRetrieval.Value);
            }
        }
    }
}
