// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.Linq;
using System.Text;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Representation of a DICOM association.
    /// </summary>
    public sealed class DicomAssociation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DicomAssociation"/> class. 
        /// </summary>
        public DicomAssociation()
        {
            PresentationContexts = new DicomPresentationContextCollection();
            ExtendedNegotiations = new DicomExtendedNegotiationCollection();
            MaxAsyncOpsInvoked = 1;
            MaxAsyncOpsPerformed = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomAssociation"/> class.
        /// </summary>
        /// <param name="callingAe">The calling Application Entity.</param>
        /// <param name="calledAe">The called Application Entity.</param>
        public DicomAssociation(string callingAe, string calledAe)
            : this()
        {
            CallingAE = callingAe;
            CalledAE = calledAe;
        }

        /// <summary>
        /// Gets the calling application entity.
        /// </summary>
        public string CallingAE { get; internal set; }

        /// <summary>
        /// Gets the called application entity.
        /// </summary>
        public string CalledAE { get; internal set; }

        /// <summary>
        /// Gets or sets the supported maximum number of asynchronous operations invoked.
        /// </summary>
        public int MaxAsyncOpsInvoked { get; set; }

        /// <summary>
        /// Gets or sets the supported maximum number of asynchronous operations performed.
        /// </summary>
        public int MaxAsyncOpsPerformed { get; set; }

        /// <summary>
        /// Gets the remote host.
        /// </summary>
        public string RemoteHost { get; internal set; }

        /// <summary>
        /// Gets the remote port.
        /// </summary>
        public int RemotePort { get; internal set; }

        /// <summary>
        /// Gets the remote implementation class UID.
        /// </summary>
        public DicomUID RemoteImplementationClassUID { get; internal set; }

        /// <summary>
        /// Gets the remote implementation version.
        /// </summary>
        public string RemoteImplementationVersion { get; internal set; }

        /// <summary>
        /// Gets the maximum PDU length that the remote service accepts.
        /// </summary>
        public uint MaximumPDULength { get; internal set; }

        /// <summary>
        /// Gets the supported presentation contexts.
        /// </summary>
        public DicomPresentationContextCollection PresentationContexts { get; private set; }

        /// <summary>
        /// Gets the (common) extended negotiations
        /// </summary>
        public DicomExtendedNegotiationCollection ExtendedNegotiations { get; private set; }

        /// <summary>
        /// Gets or sets options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        public DicomServiceOptions Options { get; internal set; }

        /// <summary>
        /// Gets or sets the user identity negotiation information
        /// </summary>
        public DicomUserIdentityNegotiation UserIdentityNegotiation { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Calling AE Title:       {0}\n", CallingAE);
            sb.AppendFormat("Called AE Title:        {0}\n", CalledAE);
            sb.AppendFormat("Remote host:            {0}\n", RemoteHost);
            sb.AppendFormat("Remote port:            {0}\n", RemotePort);
            sb.AppendFormat("Implementation Class:   {0}\n", RemoteImplementationClassUID ?? DicomImplementation.ClassUID);
            sb.AppendFormat("Implementation Version: {0}\n", RemoteImplementationVersion ?? DicomImplementation.Version);
            sb.AppendFormat("Maximum PDU Length:     {0}\n", MaximumPDULength);
            sb.AppendFormat("Async Ops Invoked:      {0}\n", MaxAsyncOpsInvoked);
            sb.AppendFormat("Async Ops Performed:    {0}\n", MaxAsyncOpsPerformed);
            sb.AppendFormat("Presentation Contexts:  {0}\n", PresentationContexts.Count);
            if (UserIdentityNegotiation != null && UserIdentityNegotiation.UserIdentityType.HasValue)
            { 
                sb.AppendFormat("User Identity:          {0}\n", UserIdentityNegotiation.UserIdentityType.ToString());
            }

            foreach (var pc in PresentationContexts)
            {
                sb.AppendFormat("  Presentation Context:  {0} [{1}]\n", pc.ID, pc.Result);
                if (pc.AbstractSyntax.Name != "Unknown")
                {
                    sb.AppendFormat("       Abstract Syntax:  {0}\n", pc.AbstractSyntax.Name);
                }
                else
                {
                    sb.AppendFormat("       Abstract Syntax:  {0}\n", pc.AbstractSyntax);
                }

                foreach (var tx in pc.GetTransferSyntaxes())
                {
                    sb.AppendFormat("       Transfer Syntax:  {0}\n", tx.UID.Name);
                }
            }

            if (ExtendedNegotiations.Count > 0)
            {
                sb.AppendFormat("Extended Negotiations:  {0}\n", ExtendedNegotiations.Count);
                foreach (DicomExtendedNegotiation ex in ExtendedNegotiations)
                {
                    if (ex.SopClassUid.Name != "Unknown")
                    {
                        sb.AppendFormat("  Extended Negotiation:  {0}\n", ex.SopClassUid.Name);
                    }
                    else
                    {
                        sb.AppendFormat("  Extended Negotiation:  {0}\n", ex.SopClassUid);
                    }

                    if (ex.RequestedApplicationInfo != null)
                    {
                        sb.AppendFormat("      Application Info:  {0}\n", ex.GetApplicationInfo());
                    }

                    if (ex.ServiceClassUid != null)
                    {
                        sb.AppendFormat("         Service Class:  {0}\n", ex.ServiceClassUid);
                    }

                    if (ex.RelatedGeneralSopClasses.Any())
                    {
                        sb.AppendFormat("   Related SOP Classes:  {0}\n", ex.RelatedGeneralSopClasses.Count);
                        foreach (var rel in ex.RelatedGeneralSopClasses)
                        {
                            sb.AppendFormat("      Related SOP Class:  {0}\n", rel);
                        }
                    }
                }
            }

            sb.Length -= 1;
            return sb.ToString();
        }
        
        /// <summary>
        /// Creates a snapshot clone of this DICOM association.
        /// This can be helpful when trying to diagnose association issues, because DicomAssociations are typically modified during the DICOM handshake
        /// </summary>
        /// <returns></returns>
        internal DicomAssociation Clone()
        {
            var clone = new DicomAssociation(CallingAE, CalledAE)
            {
                Options = Options,
                RemoteHost = RemoteHost,
                RemotePort = RemotePort,
                MaxAsyncOpsInvoked = MaxAsyncOpsInvoked,
                MaxAsyncOpsPerformed = MaxAsyncOpsPerformed,
                RemoteImplementationVersion = RemoteImplementationVersion,
                RemoteImplementationClassUID = RemoteImplementationClassUID,
                MaximumPDULength = MaximumPDULength,
                UserIdentityNegotiation = UserIdentityNegotiation
            };

            foreach (var presentationContext in PresentationContexts)
            {
                clone.PresentationContexts.Add(
                    presentationContext.AbstractSyntax,
                    presentationContext.UserRole,
                    presentationContext.ProviderRole,
                    presentationContext.GetTransferSyntaxes().ToArray()
                );
            }

            foreach (var extendedNegotiation in ExtendedNegotiations)
            {
                clone.ExtendedNegotiations.Add(
                    extendedNegotiation.SopClassUid,
                    extendedNegotiation.RequestedApplicationInfo,
                    extendedNegotiation.ServiceClassUid,
                    extendedNegotiation.RelatedGeneralSopClasses.ToArray()
                );
            }

            return clone;
        }
    }
}
