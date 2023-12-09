// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Enumeration of presentation context results.
    /// </summary>
    public enum DicomPresentationContextResult : byte
    {
        /// <summary>
        /// Presentation context is proposed.
        /// </summary>
        Proposed = 255,

        /// <summary>
        /// Presentation context is accepted.
        /// </summary>
        Accept = 0,

        /// <summary>
        /// Presentation context is rejected by user.
        /// </summary>
        RejectUser = 1,

        /// <summary>
        /// Presentation context is rejected for unspecified reason.
        /// </summary>
        RejectNoReason = 2,

        /// <summary>
        /// Presentation context is rejected due to abstract syntax not being supported.
        /// </summary>
        RejectAbstractSyntaxNotSupported = 3,

        /// <summary>
        /// Presentation context is rejected due to transfer syntaxes not being supported.
        /// </summary>
        RejectTransferSyntaxesNotSupported = 4
    }

    /// <summary>
    /// Representation of a presentation context.
    /// </summary>
    public class DicomPresentationContext
    {
        #region Private Members

        private readonly List<DicomTransferSyntax> _transferSyntaxes;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomPresentationContext"/> class.
        /// </summary>
        /// <param name="pcid">
        /// The presentation context ID.
        /// </param>
        /// <param name="abstractSyntax">
        /// The abstract syntax associated with the presentation context.
        /// </param>
        public DicomPresentationContext(byte pcid, DicomUID abstractSyntax)
            : this(pcid, abstractSyntax, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomPresentationContext"/> class.
        /// </summary>
        /// <param name="pcid">
        /// The presentation context ID.
        /// </param>
        /// <param name="abstractSyntax">
        /// The abstract syntax associated with the presentation context.
        /// </param>
        /// <param name="userRole">
        /// Indicates whether SCU role is supported.
        /// </param>
        /// <param name="providerRole">
        /// Indicates whether SCP role is supported.
        /// </param>
        public DicomPresentationContext(
            byte pcid,
            DicomUID abstractSyntax,
            bool? userRole,
            bool? providerRole)
        {
            ID = pcid;
            Result = DicomPresentationContextResult.Proposed;
            AbstractSyntax = abstractSyntax;
            _transferSyntaxes = new List<DicomTransferSyntax>();
            UserRole = userRole;
            ProviderRole = providerRole;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomPresentationContext"/> class.
        /// </summary>
        /// <param name="pcid">
        /// The presentation context ID.
        /// </param>
        /// <param name="abstractSyntax">
        /// The abstract syntax associated with the presentation context.
        /// </param>
        /// <param name="transferSyntax">
        /// Accepted transfer syntax.
        /// </param>
        /// <param name="result">
        /// Result of presentation context negotiation.
        /// </param>
        internal DicomPresentationContext(
            byte pcid,
            DicomUID abstractSyntax,
            DicomTransferSyntax transferSyntax,
            DicomPresentationContextResult result)
        {
            ID = pcid;
            Result = result;
            AbstractSyntax = abstractSyntax;
            _transferSyntaxes = new List<DicomTransferSyntax> { transferSyntax };
            UserRole = null;
            ProviderRole = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the presentation context ID.
        /// </summary>
        public byte ID { get; }

        /// <summary>
        /// Gets the association negotiation result.
        /// </summary>
        public DicomPresentationContextResult Result { get; private set; }

        /// <summary>
        /// Gets the abstact syntax associated with the presentation context.
        /// </summary>
        public DicomUID AbstractSyntax { get; }

        /// <summary>
        /// Gets the accepted transfer syntax, if defined, otherwise <code>null</code>.
        /// </summary>
        public DicomTransferSyntax AcceptedTransferSyntax { get; private set; }

        /// <summary>
        /// Gets an indicator whether presentation context supports an SCU role. If undefined, default value is assumed.
        /// </summary>
        public bool? UserRole { get; internal set; }

        /// <summary>
        /// Gets an indicator whether presentation context supports an SCU role. If undefined, default value is assumed.
        /// </summary>
        public bool? ProviderRole { get; internal set; }

        #endregion

        #region Public Members

        /// <summary>
        /// Get presentation context valid for C-GET requests for the specified <paramref name="abstractSyntax"/>.
        /// </summary>
        /// <param name="abstractSyntax">Abstract syntax for which presentation context should be generated.</param>
        /// <param name="transferSyntaxes">Supported transfer syntaxes. If <code>null</code> or empty, <see cref="DicomTransferSyntax.ImplicitVRLittleEndian"/> is 
        /// added as default transfer syntax.</param>
        /// <returns>Presentation context valid for C-GET requests for the specified <paramref name="abstractSyntax"/>.</returns>
        public static DicomPresentationContext GetScpRolePresentationContext(
            DicomUID abstractSyntax,
            params DicomTransferSyntax[] transferSyntaxes)
        {
            var pc = new DicomPresentationContext(0, abstractSyntax, false, true);

            if (transferSyntaxes != null && transferSyntaxes.Length > 0)
            {
                foreach (var transferSyntax in transferSyntaxes)
                {
                    pc.AddTransferSyntax(transferSyntax);
                }
            }
            else
            {
                pc.AddTransferSyntax(DicomTransferSyntax.ImplicitVRLittleEndian);
            }

            return pc;
        }

        /// <summary>
        /// Get, potentially filtered, collection of presentation contexts valid for C-GET requests.
        /// </summary>
        /// <param name="filter">Filter to apply when selecting a sub-set of active storage UID:s, or null to select all.</param>
        /// <param name="transferSyntaxes">Supported transfer syntaxes.</param>
        /// <returns>Collection of presentation contexts valid for C-GET requests.</returns>
        public static IEnumerable<DicomPresentationContext> GetScpRolePresentationContextsFromStorageUids(
            string filter,
            params DicomTransferSyntax[] transferSyntaxes)
        {
            var noFilter = string.IsNullOrEmpty(filter?.Trim());
            var capsFilter = noFilter ? string.Empty : filter.ToUpperInvariant();

            return
                DicomUID.Enumerate()
                    .Where(
                        uid =>
                        uid.StorageCategory != DicomStorageCategory.None && !uid.IsRetired
                        && (noFilter || uid.Name.ToUpperInvariant().Contains(capsFilter)))
                    .Select(uid => GetScpRolePresentationContext(uid, transferSyntaxes));
        }

        /// <summary>
        /// Get storage category collection of presentation contexts valid for C-GET requests.
        /// </summary>
        /// <param name="storageCategory">Storage category for which the sub-set of presentation context should be selected.</param>
        /// <param name="transferSyntaxes">Supported transfer syntaxes.</param>
        /// <returns>Collection of presentation contexts valid for C-GET requests.</returns>
        public static IEnumerable<DicomPresentationContext> GetScpRolePresentationContextsFromStorageUids(
            DicomStorageCategory storageCategory,
            params DicomTransferSyntax[] transferSyntaxes)
        {
            return
                DicomUID.Enumerate()
                    .Where(uid => uid.StorageCategory == storageCategory && !uid.IsRetired)
                    .Select(uid => GetScpRolePresentationContext(uid, transferSyntaxes));
        }

        /// <summary>
        /// Sets the <c>Result</c> of this presentation context.
        /// 
        /// The preferred method of accepting presentation contexts is to call one of the <c>AcceptTransferSyntaxes</c> methods.
        /// </summary>
        /// <param name="result">Result status to return for this proposed presentation context.</param>
        public void SetResult(DicomPresentationContextResult result)
        {
            if (result == DicomPresentationContextResult.Accept && _transferSyntaxes.Count == 0)
            {
                throw new DicomNetworkException(
                    "For result Acceptance, at least one transfer syntax must be defined prior to SetResult call.");
            }

            SetResult(result, _transferSyntaxes.FirstOrDefault());
        }

        /// <summary>
        /// Sets the <c>Result</c> and <c>AcceptedTransferSyntax</c> of this presentation context.
        /// 
        /// The preferred method of accepting presentation contexts is to call one of the <c>AcceptTransferSyntaxes</c> methods.
        /// </summary>
        /// <param name="result">Result status to return for this proposed presentation context.</param>
        /// <param name="acceptedTransferSyntax">Accepted transfer syntax for this proposed presentation context.</param>
        public void SetResult(DicomPresentationContextResult result, DicomTransferSyntax acceptedTransferSyntax)
        {
            var isAccept = result == DicomPresentationContextResult.Accept;
            if (isAccept && acceptedTransferSyntax == null)
            {
                throw new DicomNetworkException(
                    "Result Acceptance must be accompanied by a non-null accepted transfer syntax.");
            }

            _transferSyntaxes.Clear();
            _transferSyntaxes.Add(acceptedTransferSyntax);

            Result = result;
            AcceptedTransferSyntax = isAccept ? acceptedTransferSyntax : null;
        }

        /// <summary>
        /// Compares a list of transfer syntaxes accepted by the SCP against the list of transfer syntaxes proposed by the SCU. Sets the presentation 
        /// context <c>Result</c> to <c>DicomPresentationContextResult.Accept</c> if an accepted transfer syntax is found. If no accepted transfer
        /// syntax is found, the presentation context <c>Result</c> is set to <c>DicomPresentationContextResult.RejectTransferSyntaxesNotSupported</c>.
        /// </summary>
        /// <param name="acceptedTransferSyntaxes">Transfer syntaxes that the SCP accepts for the proposed abstract syntax.</param>
        /// <returns>Returns <c>true</c> if an accepted transfer syntax was found. Returns <c>false</c> if no accepted transfer syntax was found.</returns>
        public bool AcceptTransferSyntaxes(params DicomTransferSyntax[] acceptedTransferSyntaxes)
        {
            return AcceptTransferSyntaxes(acceptedTransferSyntaxes, false);
        }

        /// <summary>
        /// Compares a list of transfer syntaxes accepted by the SCP against the list of transfer syntaxes proposed by the SCU. Sets the presentation 
        /// context <c>Result</c> to <c>DicomPresentationContextResult.Accept</c> if an accepted transfer syntax is found. If no accepted transfer
        /// syntax is found, the presentation context <c>Result</c> is set to <c>DicomPresentationContextResult.RejectTransferSyntaxesNotSupported</c>.
        /// </summary>
        /// <param name="acceptedTransferSyntaxes">Transfer syntaxes that the SCP accepts for the proposed abstract syntax.</param>
        /// <param name="scpPriority">If set to <c>true</c>, transfer syntaxes will be accepted in the order specified by <paramref name="acceptedTransferSyntaxes"/>. 
        /// If set to <c>false</c>, transfer syntaxes will be accepted in the order proposed by the SCU.</param>
        /// <returns>Returns <c>true</c> if an accepted transfer syntax was found. Returns <c>false</c> if no accepted transfer syntax was found.</returns>
        public bool AcceptTransferSyntaxes(DicomTransferSyntax[] acceptedTransferSyntaxes, bool scpPriority)
        {
            if (Result == DicomPresentationContextResult.Accept)
            {
                return true;
            }

            if (scpPriority)
            {
                // let the SCP decide which syntax that it would prefer
                foreach (DicomTransferSyntax ts in acceptedTransferSyntaxes)
                {
                    if (ts != null && HasTransferSyntax(ts))
                    {
                        SetResult(DicomPresentationContextResult.Accept, ts);
                        return true;
                    }
                }
            }
            else
            {
                // accept syntaxes in the order that the SCU proposed them
                foreach (DicomTransferSyntax ts in _transferSyntaxes)
                {
                    if (acceptedTransferSyntaxes.Contains(ts))
                    {
                        SetResult(DicomPresentationContextResult.Accept, ts);
                        return true;
                    }
                }
            }

            SetResult(DicomPresentationContextResult.RejectTransferSyntaxesNotSupported);

            return false;
        }

        /// <summary>
        /// Add transfer syntax.
        /// </summary>
        /// <param name="ts">Transfer syntax to add to presentation context.</param>
        public void AddTransferSyntax(DicomTransferSyntax ts)
        {
            if (ts != null && !_transferSyntaxes.Contains(ts))
            {
                _transferSyntaxes.Add(ts);
            }
        }

        /// <summary>
        /// Remove transfer syntax.
        /// </summary>
        /// <param name="ts">Transfer syntax to remove from presentation context.</param>
        public void RemoveTransferSyntax(DicomTransferSyntax ts)
        {
            if (ts != null && _transferSyntaxes.Contains(ts))
            {
                _transferSyntaxes.Remove(ts);
            }
        }

        /// <summary>
        /// Clear all supported transfer syntaxes.
        /// </summary>
        public void ClearTransferSyntaxes()
        {
            _transferSyntaxes.Clear();
        }

        /// <summary>
        /// Get read-only list of supported transfer syntaxes.
        /// </summary>
        /// <returns></returns>
        public IList<DicomTransferSyntax> GetTransferSyntaxes()
        {
            return new ReadOnlyCollection<DicomTransferSyntax>(_transferSyntaxes);
        }

        /// <summary>
        /// Checks whether presentation context contains <paramref name="ts">transfer syntax</paramref>.
        /// </summary>
        /// <param name="ts">Transfer syntax to check.</param>
        /// <returns><code>true</code> if <paramref name="ts">transfer syntax</paramref> is supported, <code>false</code> otherwise.</returns>
        public bool HasTransferSyntax(DicomTransferSyntax ts)
        {
            return _transferSyntaxes.Contains(ts);
        }

        /// <summary>
        /// Get user-friendly description of negotiation result.
        /// </summary>
        /// <returns>User-friendly description of negotiation result.</returns>
        public string GetResultDescription()
        {
            return Result switch
            {
                DicomPresentationContextResult.Accept => "Accept",
                DicomPresentationContextResult.Proposed => "Proposed",
                DicomPresentationContextResult.RejectAbstractSyntaxNotSupported => "Reject - Abstract Syntax Not Supported",
                DicomPresentationContextResult.RejectNoReason => "Reject - No Reason",
                DicomPresentationContextResult.RejectTransferSyntaxesNotSupported => "Reject - Transfer Syntaxes Not Supported",
                DicomPresentationContextResult.RejectUser => "Reject - User",
                _ => "Unknown",
            };
        }

        #endregion
    }
}
