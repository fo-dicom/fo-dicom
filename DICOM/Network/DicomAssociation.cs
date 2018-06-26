// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System.Collections.Generic;
    using System.Text;

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
            MaxAsyncOpsInvoked = 1;
            MaxAsyncOpsPerformed = 1;
            ExtendedNegotiations = new List<DicomExtendedNegotiation>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomAssociation"/> class.
        /// </summary>
        /// <param name="callingAe">The calling Application Entity.</param>
        /// <param name="calledAe">The called Application Entity.</param>
        /// <param name="maxPduLength">Maximum PDU length.</param>
        public DicomAssociation(string callingAe, string calledAe, uint maxPduLength = 16384)
            : this()
        {
            CallingAE = callingAe;
            CalledAE = calledAe;
            MaximumPDULength = maxPduLength;
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
        /// Gets the maximum PDU length.
        /// </summary>
        public uint MaximumPDULength { get; internal set; }

        /// <summary>
        /// Gets the supported presentation contexts.
        /// </summary>
        public DicomPresentationContextCollection PresentationContexts { get; private set; }

        /// <summary>
        /// Gets supported extended negotiations
        /// </summary>
        public List<DicomExtendedNegotiation> ExtendedNegotiations { get; private set; }

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
            sb.AppendFormat(
                "Implementation Class:   {0}\n",
                this.RemoteImplementationClassUID ?? DicomImplementation.ClassUID);
            sb.AppendFormat("Implementation Version: {0}\n", RemoteImplementationVersion ?? DicomImplementation.Version);
            sb.AppendFormat("Maximum PDU Length:     {0}\n", MaximumPDULength);
            sb.AppendFormat("Async Ops Invoked:      {0}\n", MaxAsyncOpsInvoked);
            sb.AppendFormat("Async Ops Performed:    {0}\n", MaxAsyncOpsPerformed);
            sb.AppendFormat("Presentation Contexts:  {0}\n", PresentationContexts.Count);
            foreach (var pc in PresentationContexts)
            {
                sb.AppendFormat("  Presentation Context:  {0} [{1}]\n", pc.ID, pc.Result);
                if (pc.AbstractSyntax.Name != "Unknown") sb.AppendFormat("       Abstract Syntax:  {0}\n", pc.AbstractSyntax.Name);
                else
                    sb.AppendFormat(
                        "       Abstract Syntax:  {0} [{1}]\n",
                        pc.AbstractSyntax.Name,
                        pc.AbstractSyntax.UID);
                foreach (var tx in pc.GetTransferSyntaxes())
                {
                    sb.AppendFormat("       Transfer Syntax:  {0}\n", tx.UID.Name);
                }
            }

            if (ExtendedNegotiations.Count > 0)
            {
                sb.AppendFormat("Extended Negotiations: {0}\n", ExtendedNegotiations.Count);
                foreach (DicomExtendedNegotiation exNeg in ExtendedNegotiations)
                {
                    sb.AppendFormat("  Extended Negotiation: {0}\n", exNeg.SopClassUid);
                }
            }

            sb.Length = sb.Length - 1;
            return sb.ToString();
        }
    }
}
