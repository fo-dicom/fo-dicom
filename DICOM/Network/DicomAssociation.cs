using System;
using System.Collections.Generic;
using System.Text;

namespace Dicom.Network {
	public sealed class DicomAssociation {
		public DicomAssociation() {
			PresentationContexts = new DicomPresentationContextCollection();
			MaxAsyncOpsInvoked = 1;
			MaxAsyncOpsPerformed = 1;
		}

		public DicomAssociation(string callingAe, string calledAe, uint maxPduLength = 16384) : this() {
			CallingAE = callingAe;
			CalledAE = calledAe;
			MaximumPDULength = maxPduLength;
		}

		public string CallingAE {
			get;
			internal set;
		}

		public string CalledAE {
			get;
			internal set;
		}

		public int MaxAsyncOpsInvoked {
			get;
			set;
		}

		public int MaxAsyncOpsPerformed {
			get;
			set;
		}

		public DicomUID RemoteImplemetationClassUID {
			get;
			internal set;
		}

		public string RemoteImplementationVersion {
			get;
			internal set;
		}

		public uint MaximumPDULength {
			get;
			internal set;
		}

		public DicomPresentationContextCollection PresentationContexts {
			get;
			private set;
		}

		public override string ToString() {
			var sb = new StringBuilder();
			sb.AppendFormat("Calling AE Title:       {0}\n", CallingAE);
			sb.AppendFormat("Called AE Title:        {0}\n", CalledAE);
			sb.AppendFormat("Implementation Class:   {0}\n", RemoteImplemetationClassUID ?? DicomImplementation.ClassUID);
			sb.AppendFormat("Implementation Version: {0}\n", RemoteImplementationVersion ?? DicomImplementation.Version);
			sb.AppendFormat("Maximum PDU Length:     {0}\n", MaximumPDULength);
			sb.AppendFormat("Async Ops Invoked:      {0}\n", MaxAsyncOpsInvoked);
			sb.AppendFormat("Async Ops Performed:    {0}\n", MaxAsyncOpsPerformed);
			sb.AppendFormat("Presentation Contexts:  {0}\n", PresentationContexts.Count);
			foreach (var pc in PresentationContexts) {
				sb.AppendFormat("  Presentation Context:  {0} [{1}]\n", pc.ID, pc.Result);
				if (pc.AbstractSyntax.Name != "Unknown")
					sb.AppendFormat("       Abstract Syntax:  {0}\n", pc.AbstractSyntax.Name);
				else
					sb.AppendFormat("       Abstract Syntax:  {0} [{1}]\n", pc.AbstractSyntax.Name, pc.AbstractSyntax.UID);
				foreach (var tx in pc.GetTransferSyntaxes()) {
					sb.AppendFormat("       Transfer Syntax:  {0}\n", tx.UID.Name);
				}
			}
			sb.Length = sb.Length - 1;
			return sb.ToString();
		}
	}
}
