// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dicom.Network
{
    /// <summary>
    /// Collection of presentation contexts, with unique ID:s.
    /// </summary>
    public class DicomPresentationContextCollection : ICollection<DicomPresentationContext>
    {
        #region FIELDS

        private readonly object locker = new object();

        private readonly SortedDictionary<byte, DicomPresentationContext> _pc;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="DicomPresentationContextCollection"/>.
        /// </summary>
        public DicomPresentationContextCollection()
        {
            _pc = new SortedDictionary<byte, DicomPresentationContext>();
        }

        #endregion

        #region INDEXERS

        /// <summary>
        /// Gets the presentation context associated with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Presentation context ID.</param>
        /// <returns>Presentation context associated with <paramref name="id"/></returns>
        public DicomPresentationContext this[byte id] =>_pc[id];

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the number of presentation contexts in collection.
        /// </summary>
        public int Count => _pc.Count;

        /// <summary>
        /// Gets whether collection is read-only. Is always <code>false</code>.
        /// </summary>
        public bool IsReadOnly => false;

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize and add new presentation context.
        /// </summary>
        /// <param name="abstractSyntax">Abstract syntax of the presentation context.</param>
        /// <param name="transferSyntaxes">Supported transfer syntaxes.</param>
        public void Add(DicomUID abstractSyntax, params DicomTransferSyntax[] transferSyntaxes)
        {
            Add(abstractSyntax, null, null, transferSyntaxes);
        }

        /// <summary>
        /// Initialize and add new presentation context.
        /// </summary>
        /// <param name="abstractSyntax">Abstract syntax of the presentation context.</param>
        /// <param name="userRole">Supports SCU role?</param>
        /// <param name="providerRole">Supports SCP role?</param>
        /// <param name="transferSyntaxes">Supported transfer syntaxes.</param>
        public void Add(DicomUID abstractSyntax, bool? userRole, bool? providerRole, params DicomTransferSyntax[] transferSyntaxes)
        {
            var pc = new DicomPresentationContext(GetNextPresentationContextID(), abstractSyntax, userRole, providerRole);

            foreach (var tx in transferSyntaxes) pc.AddTransferSyntax(tx);

            Add(pc);
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public void Add(DicomPresentationContext item)
        {
            _pc.Add(item.ID, item);
        }

        /// <summary>
        /// Add presentation contexts obtained from DICOM request.
        /// </summary>
        /// <param name="request">Request from which presentation context(s) should be obtained.</param>
        public void AddFromRequest(DicomRequest request)
        {
            if (request is DicomCStoreRequest)
            {
                var cstore = request as DicomCStoreRequest;

                var pcs = _pc.Values.Where(x => x.AbstractSyntax == request.SOPClassUID);
                if (cstore.TransferSyntax == DicomTransferSyntax.ImplicitVRLittleEndian)
                {
                    pcs = pcs.Where(x => x.GetTransferSyntaxes().Contains(DicomTransferSyntax.ImplicitVRLittleEndian));
                }
                else
                {
                    pcs = pcs.Where(x => x.AcceptedTransferSyntax == cstore.TransferSyntax);
                }

                var pc = pcs.FirstOrDefault();
                if (pc == null)
                {
                    var tx = new List<DicomTransferSyntax>();
                    if (cstore.TransferSyntax != DicomTransferSyntax.ImplicitVRLittleEndian) tx.Add(cstore.TransferSyntax);
                    if (cstore.AdditionalTransferSyntaxes != null) tx.AddRange(cstore.AdditionalTransferSyntaxes);
                    tx.Add(DicomTransferSyntax.ImplicitVRLittleEndian);

                    Add(cstore.SOPClassUID, tx.ToArray());
                }
            }
            else
            {
                if (request.PresentationContext != null)
                {
                    var pc =
                        _pc.Values.FirstOrDefault(
                            x =>
                                x.AbstractSyntax == request.PresentationContext.AbstractSyntax &&
                                request.PresentationContext.GetTransferSyntaxes().All(y => x.GetTransferSyntaxes().Contains(y)));

                    if (pc == null)
                    {
                        var transferSyntaxes = request.PresentationContext.GetTransferSyntaxes().ToArray();
                        if (!transferSyntaxes.Any())
                            transferSyntaxes = new[] { DicomTransferSyntax.ImplicitVRLittleEndian };
                        Add(
                            request.PresentationContext.AbstractSyntax,
                            request.PresentationContext.UserRole,
                            request.PresentationContext.ProviderRole,
                            transferSyntaxes);
                    }
                }
                else
                {
                    var pc = _pc.Values.FirstOrDefault(x => x.AbstractSyntax == request.SOPClassUID);
                    if (pc == null)
                        Add(request.SOPClassUID, DicomTransferSyntax.ImplicitVRLittleEndian);
                }
            }
        }

        /// <summary>
        /// Clear all presentation contexts in collection.
        /// </summary>
        public void Clear()
        {
            _pc.Clear();
        }

        /// <summary>
        /// Indicates if specified presentation context is contained in collection.
        /// </summary>
        /// <param name="item">Presentation context to search for.</param>
        /// <returns><code>true</code> if <paramref name="item"/> is contained in collection, <code>false</code> otherwise.</returns>
        public bool Contains(DicomPresentationContext item)
        {
            return _pc.ContainsKey(item.ID) && _pc[item.ID].AbstractSyntax == item.AbstractSyntax;
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
        void ICollection<DicomPresentationContext>.CopyTo(DicomPresentationContext[] array, int arrayIndex)
        {
            throw new NotSupportedException("Not implemented");
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
        public bool Remove(DicomPresentationContext item)
        {
            return _pc.Remove(item.ID);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<DicomPresentationContext> GetEnumerator()
        {
            return _pc.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets a unique presentation context ID.
        /// </summary>
        /// <returns>Unique presentation context ID.</returns>
        private byte GetNextPresentationContextID()
        {
            lock (locker)
            {
                if (_pc.Count == 0) return 1;

                var id = _pc.Max(x => x.Key) + 2;

                if (id >= 256) throw new DicomNetworkException("Too many presentation contexts configured for this association!");

                return (byte)id;
            }
        }

        #endregion
    }
}
