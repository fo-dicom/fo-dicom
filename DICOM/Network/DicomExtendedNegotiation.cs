// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for extended negotiation sub-items.
    /// </summary>
    public interface IExtendedNegotiationSubItem
    {
        /// <summary>
        /// Write PDU data.
        /// </summary>
        /// <param name="pdu">PDU data to write.</param>
        void Write(RawPDU pdu);
    }

    /// <summary>
    /// Delegate representing creation of an <see cref="IExtendedNegotiationSubItem"/>.
    /// </summary>
    /// <param name="raw">Raw PDU.</param>
    /// <param name="itemSize">Item size.</param>
    /// <param name="bytesRead">Bytes read.</param>
    /// <returns>Created <see cref="IExtendedNegotiationSubItem"/>.</returns>
    public delegate IExtendedNegotiationSubItem CreateIExtendedNegotiationSubItemDelegate(
        RawPDU raw,
        int itemSize,
        out int bytesRead);

    /// <summary>
    /// Class for managing DICOM extended negotiation.
    /// </summary>
    public class DicomExtendedNegotiation
    {
        private static readonly Dictionary<DicomUID, CreateIExtendedNegotiationSubItemDelegate> subItemCreators =
            new Dictionary<DicomUID, CreateIExtendedNegotiationSubItemDelegate>();

        static DicomExtendedNegotiation()
        {
            AddSubItemCreator(DicomUID.StudyRootQueryRetrieveInformationModelFIND, RootQueryRetrieveInfoFind.Create);
            AddSubItemCreator(DicomUID.PatientRootQueryRetrieveInformationModelFIND, RootQueryRetrieveInfoFind.Create);
            AddSubItemCreator(DicomUID.StudyRootQueryRetrieveInformationModelMOVE, RootQueryRetrieveInfoMove.Create);
            AddSubItemCreator(DicomUID.PatientRootQueryRetrieveInformationModelMOVE, RootQueryRetrieveInfoMove.Create);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomExtendedNegotiation"/> class.
        /// </summary>
        /// <param name="sopClassUid">SOP class UID.</param>
        /// <param name="subItem">Extended negotiation sub-item.</param>
        public DicomExtendedNegotiation(DicomUID sopClassUid, IExtendedNegotiationSubItem subItem)
        {
            SopClassUid = sopClassUid;
            SubItem = subItem;
        }

        /// <summary>
        /// Gets SOP Class UID.
        /// </summary>
        public DicomUID SopClassUid { get; }

        /// <summary>
        /// Gets extended negotiation sub-item.
        /// </summary>
        public IExtendedNegotiationSubItem SubItem { get; }

        /// <summary>
        /// Add item for creating extended negotiation sub-items.
        /// </summary>
        /// <param name="uid">Associated UID.</param>
        /// <param name="creator">Creator instance.</param>
        public static void AddSubItemCreator(DicomUID uid, CreateIExtendedNegotiationSubItemDelegate creator)
        {
            if (false == subItemCreators.ContainsKey(uid))
            {
                subItemCreators.Add(uid, creator);
            }
        }

        /// <summary>
        /// Write PDU.
        /// </summary>
        /// <param name="pdu">PDU to write.</param>
        public void Write(RawPDU pdu)
        {
            if (null != SubItem)
            {
                pdu.Write("Item-Type", (byte)0x56);
                pdu.Write("Reserved", (byte)0x00);

                pdu.MarkLength16("Item-Length");

                pdu.Write("SOP Class UID Length", (ushort)(SopClassUid.UID.Length));
                pdu.Write("SOP Class UID", SopClassUid.UID);

                SubItem.Write(pdu);

                pdu.WriteLength16();
            }
        }

        /// <summary>
        /// Factory method for creating <see cref="DicomExtendedNegotiation"/> instances.
        /// </summary>
        /// <param name="raw">Raw PDU.</param>
        /// <param name="length">Length.</param>
        /// <returns>A new <see cref="DicomExtendedNegotiation"/> instance.</returns>
        public static DicomExtendedNegotiation Create(RawPDU raw, ushort length)
        {
            var uidLen = raw.ReadUInt16("SOP Class UID Length");
            var uidStr = raw.ReadString("SOP Class UID", uidLen);

            var uid = DicomUID.Parse(uidStr);
            IExtendedNegotiationSubItem subItem = null;
            var subItemSize = 0;

            var remaining = length - uidLen - 2;
            if (subItemCreators.ContainsKey(uid))
            {
                subItem = subItemCreators[uid](raw, remaining, out subItemSize);
            }

            remaining -= subItemSize;
            if (remaining > 0)
            {
                raw.SkipBytes("Unread bytes", remaining);
            }

            return new DicomExtendedNegotiation(uid, subItem);
        }
    }
}
