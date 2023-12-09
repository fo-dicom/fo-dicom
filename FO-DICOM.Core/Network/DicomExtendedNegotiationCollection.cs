// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;

namespace FellowOakDicom.Network
{
    public class DicomExtendedNegotiationCollection : ICollection<DicomExtendedNegotiation>
    {
        #region FIELDS

        private readonly Dictionary<DicomUID, DicomExtendedNegotiation> _ec;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="DicomExtendedNegotiationCollection"/>.
        /// </summary>
        public DicomExtendedNegotiationCollection()
        {
            _ec = new Dictionary<DicomUID, DicomExtendedNegotiation>();
        }

        #endregion

        #region INDEXERS

        /// <summary>
        /// Gets the Extended negotiation associated with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">SOP Class UID.</param>
        /// <returns>Extended negotiation associated with <paramref name="id"/></returns>
        public DicomExtendedNegotiation this[DicomUID id] => _ec[id];

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the number of extended negotiations in collection.
        /// </summary>
        public int Count => _ec.Count;

        /// <summary>
        /// Gets whether collection is read-only. Is always <code>false</code>.
        /// </summary>
        public bool IsReadOnly => false;

        #endregion

        #region METHODS

        /// <summary>
        /// Add new extended negotiation object.
        /// </summary>
        /// <param name="item">Extended negotiation to be added.</param>
        public void Add(DicomExtendedNegotiation item)
        {
            if (item?.SopClassUid == null)
            {
                return;
            }

            _ec.Add(item.SopClassUid, item);
        }

        /// <summary>
        /// Add Extended Negotiation with Service Class Application Info
        /// </summary>
        /// <param name="sopClassUid">SOP Class UID.</param>
        /// <param name="applicationInfo">Service Class Application Info.</param>
        public void Add(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo)
        {
            Add(new DicomExtendedNegotiation(sopClassUid, applicationInfo));
        }

        /// <summary>
        /// Add Common Extended Negotiation with Service Class UID and Related General SOP Class UIDs
        /// </summary>
        /// <param name="sopClassUid">SOP Class UID.</param>
        /// <param name="serviceClassUid">Common Service Class UID.</param>
        /// <param name="relatedGeneralSopClasses">Related General SOP Classes.</param>
        public void Add(DicomUID sopClassUid, DicomUID serviceClassUid, params DicomUID[] relatedGeneralSopClasses)
        {
            Add(new DicomExtendedNegotiation(sopClassUid, serviceClassUid, relatedGeneralSopClasses));
        }

        /// <summary>
        /// Add (Common) Extended Negotiation with Application Info, Service Class UID and Related General SOP Class UIDs
        /// </summary>
        /// <param name="sopClassUid">SOP Class UID.</param>
        /// <param name="applicationInfo">Service Class Application Info.</param>
        /// <param name="serviceClassUid">Common Service Class UID.</param>
        /// <param name="relatedGeneralSopClasses">Related General SOP Classes.</param>
        public void Add(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo, DicomUID serviceClassUid, params DicomUID[] relatedGeneralSopClasses)
        {
            Add(new DicomExtendedNegotiation(sopClassUid, applicationInfo, serviceClassUid, relatedGeneralSopClasses));
        }

        /// <summary>
        /// Add Extended Negotiation obtained from DICOM request.
        /// Note: The extended negotiation will affect all requests that share the same SOP class within an association.
        /// </summary>
        /// <param name="dicomRequest">Request from which extended negotiation info should be obtained.</param>
        public void AddFromRequest(DicomRequest dicomRequest)
        {
            if (dicomRequest is DicomCStoreRequest cStoreRequest)
            {
                if (cStoreRequest.ApplicationInfo != null || cStoreRequest.CommonServiceClassUid != null)
                {
                    AddOrUpdate(cStoreRequest.SOPClassUID, cStoreRequest.ApplicationInfo,
                        cStoreRequest.CommonServiceClassUid, cStoreRequest.RelatedGeneralSopClasses?.ToArray());
                }
            }
            else if (dicomRequest.ApplicationInfo != null)
            {
                AddOrUpdate(dicomRequest.SOPClassUID, dicomRequest.ApplicationInfo);
            }
        }

        /// <summary>
        /// Add or update Extended Negotiation with Service Class Application Info.
        /// </summary>
        /// <param name="sopClassUid">SOP Class UID.</param>
        /// <param name="applicationInfo">Service Class Application Info.</param>
        public void AddOrUpdate(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo)
        {
            if (_ec.TryGetValue(sopClassUid, out var en))
            {
                en.RequestedApplicationInfo = applicationInfo;
            }
            else
            {
                Add(sopClassUid, applicationInfo);
            }
        }

        /// <summary>
        /// Add or update Common Extended Negotiation with Service Class UID and Related General SOP Class UIDs.
        /// </summary>
        /// <param name="sopClassUid">SOP Class UID.</param>
        /// <param name="serviceClassUid">Common Service Class UID.</param>
        /// <param name="relatedGeneralSopClasses">Related General SOP Classes.</param>
        public void AddOrUpdate(DicomUID sopClassUid, DicomUID serviceClassUid, params DicomUID[] relatedGeneralSopClasses)
        {
            if (_ec.TryGetValue(sopClassUid, out var en))
            {
                en.ServiceClassUid = serviceClassUid;
                en.RelatedGeneralSopClasses.Clear();
                en.RelatedGeneralSopClasses.AddRange(relatedGeneralSopClasses);
            }
            else
            {
                Add(sopClassUid, serviceClassUid, relatedGeneralSopClasses);
            }
        }

        /// <summary>
        /// Add or update (Common) Extended Negotiation with Application Info, Service Class UID and Related General SOP Class UIDs
        /// </summary>
        /// <param name="sopClassUid">SOP Class UID.</param>
        /// <param name="applicationInfo">Service Class Application Info.</param>
        /// <param name="serviceClassUid">Common Service Class UID.</param>
        /// <param name="relatedGeneralSopClasses">Related General SOP Classes.</param>
        public void AddOrUpdate(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo, DicomUID serviceClassUid, params DicomUID[] relatedGeneralSopClasses)
        {
            if (_ec.TryGetValue(sopClassUid, out var en))
            {
                en.RequestedApplicationInfo = applicationInfo ?? en.RequestedApplicationInfo;
                if (serviceClassUid != null)
                {
                    en.ServiceClassUid = serviceClassUid;
                    en.RelatedGeneralSopClasses.Clear();
                    en.RelatedGeneralSopClasses.AddRange(relatedGeneralSopClasses);
                }
            }
            Add(new DicomExtendedNegotiation(sopClassUid, applicationInfo, serviceClassUid, relatedGeneralSopClasses));
        }

        /// <summary>
        /// Accept Extended Negotiation with Service Class Application Info from the SCP.
        /// </summary>
        /// <param name="sopClassUid">SOP Class UID.</param>
        /// <param name="applicationInfo">Service Class Application Info.</param>
        public void AcceptApplicationInfo(DicomUID sopClassUid, DicomServiceApplicationInfo applicationInfo)
        {
            if (_ec.TryGetValue(sopClassUid, out var en))
            {
                en.AcceptApplicationInfo(applicationInfo);
            }
        }

        /// <summary>
        /// Clear all extended negotiations in collection.
        /// </summary>
        public void Clear()
        {
            _ec.Clear();
        }

        /// <summary>
        /// Indicates if specified extended negotiation is contained in collection.
        /// </summary>
        /// <param name="item">Extended negotiation to search for.</param>
        /// <returns><code>true</code> if <paramref name="item"/> is contained in collection, <code>false</code> otherwise.</returns>
        public bool Contains(DicomExtendedNegotiation item)
        {
            return item?.SopClassUid != null && _ec.ContainsKey(item.SopClassUid);
        }

        /// <summary>
        /// Indicates if specified uid is contained in collection
        /// </summary>
        /// <param name="item">SOP Class UID to search for.</param>
        /// <returns><code>true</code> if <paramref name="item"/> is contained in collection, <code>false</code> otherwise.</returns>
        public bool Contains(DicomUID item)
        {
            return _ec.ContainsKey(item);
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="array">Array of extended negotiations</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="System.NotSupportedException"></exception>
        public void CopyTo(DicomExtendedNegotiation[] array, int arrayIndex)
        {
            throw new NotSupportedException("Not implemented");
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<DicomExtendedNegotiation> GetEnumerator()
        {
            return _ec.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes the Extended Negotiation with same SOP Class if it exists.
        /// </summary>
        /// <param name="item">Extended Negotiation with same SOP Class to be removed.</param>
        /// <returns><code>true</code> when <paramref name="item"/> is removed from collection, <code>false</code> otherwise.</returns>
        public bool Remove(DicomExtendedNegotiation item)
        {
            return item?.SopClassUid != null && _ec.Remove(item.SopClassUid);
        }

        #endregion
    }
}
