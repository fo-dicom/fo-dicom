using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Dicom {
	public enum DicomUidType {
		TransferSyntax,
		SOPClass,
		MetaSOPClass,
		ServiceClass,
		SOPInstance,
		ApplicationContextName,
		ApplicationHostingModel,
		CodingScheme,
		FrameOfReference,
		LDAP,
		ContextGroupName,
		Unknown
	}

	public enum DicomStorageCategory {
		None,
		Image,
		PresentationState,
		StructuredReport,
		Waveform,
		Document,
		Raw,
		Other
	}

	public sealed partial class DicomUID : DicomParseable {
		private string _uid;
		private string _name;
		private DicomUidType _type;
		private bool _retired;

		public DicomUID(string uid, string name, DicomUidType type, bool retired=false) {
			_uid = uid;
			_name = name;
			_type = type;
			_retired = retired;
		}

		public string UID {
			get {
				return _uid;
			}
		}

		public string Name {
			get {
				return _name;
			}
		}

		public DicomUidType Type {
			get {
				return _type;
			}
		}

		public bool IsRetired {
			get {
				return _retired;
			}
		}

		public static DicomUID Generate() {
			var generator = new DicomUIDGenerator();
			return generator.Generate();
		}

		public static DicomUID Append(DicomUID baseUid, long nextSeq) {
			StringBuilder uid = new StringBuilder();
			uid.Append(baseUid.UID).Append('.').Append(nextSeq);
			return new DicomUID(uid.ToString(), "SOP Instance UID", DicomUidType.SOPInstance);
		}

		public static bool IsValid(string uid) {
			if (String.IsNullOrEmpty(uid))
				return false;

			// only checks that the UID contains valid characters
			foreach (char c in uid) {
				if (c != '.' && !Char.IsDigit(c))
					return false;
			}

			return true;
		}

		public static DicomUID Parse(string s) {
			string u = s.TrimEnd(' ', '\0');

			DicomUID uid = null;
			if (_uids.TryGetValue(u, out uid))
				return uid;

			//if (!IsValid(u))
			//	throw new DicomDataException("Invalid characters in UID string ['" + u + "']");

			return new DicomUID(u, "Unknown", DicomUidType.Unknown);
		}

		private static IDictionary<string, DicomUID> _uids;

		static DicomUID() {
			_uids = new ConcurrentDictionary<string, DicomUID>();
			LoadInternalUIDs();
			LoadPrivateUIDs();
		}

		public static IEnumerable<DicomUID> Enumerate() {
			return _uids.Values;
		}

		public bool IsImageStorage {
			get {
				return StorageCategory == DicomStorageCategory.Image;
			}
		}

		public DicomStorageCategory StorageCategory {
			get {
				if (Type != DicomUidType.SOPClass || !Name.Contains("Storage"))
					return DicomStorageCategory.None;

				if (Name.Contains("Image Storage"))
					return DicomStorageCategory.Image;

				if (this == DicomUID.BlendingSoftcopyPresentationStateStorageSOPClass ||
					this == DicomUID.ColorSoftcopyPresentationStateStorageSOPClass ||
					this == DicomUID.GrayscaleSoftcopyPresentationStateStorageSOPClass ||
					this == DicomUID.PseudoColorSoftcopyPresentationStateStorageSOPClass)
					return DicomStorageCategory.PresentationState;

				if (this == DicomUID.AudioSRStorageTrialRETIRED ||
					this == DicomUID.BasicTextSRStorage ||
					this == DicomUID.ChestCADSRStorage ||
					this == DicomUID.ComprehensiveSRStorage ||
					this == DicomUID.ComprehensiveSRStorageTrialRETIRED ||
					this == DicomUID.DetailSRStorageTrialRETIRED ||
					this == DicomUID.EnhancedSRStorage ||
					this == DicomUID.MammographyCADSRStorage ||
					this == DicomUID.TextSRStorageTrialRETIRED ||
					this == DicomUID.XRayRadiationDoseSRStorage)
					return DicomStorageCategory.StructuredReport;

				if (this == DicomUID.AmbulatoryECGWaveformStorage ||
					this == DicomUID.BasicVoiceAudioWaveformStorage ||
					this == DicomUID.CardiacElectrophysiologyWaveformStorage ||
					this == DicomUID.GeneralECGWaveformStorage ||
					this == DicomUID.HemodynamicWaveformStorage ||
					this == DicomUID.TwelveLeadECGWaveformStorage ||
					this == DicomUID.WaveformStorageTrialRETIRED)
					return DicomStorageCategory.Waveform;

				if (this == DicomUID.EncapsulatedCDAStorage ||
					this == DicomUID.EncapsulatedPDFStorage)
					return DicomStorageCategory.Document;

				if (this == DicomUID.RawDataStorage)
					return DicomStorageCategory.Raw;

				return DicomStorageCategory.Other;
			}
		}

		public static bool operator ==(DicomUID a, DicomUID b) {
			if (((object)a == null) && ((object)b == null))
				return true;
			if (((object)a == null) || ((object)b == null))
				return false;
			return a.UID == b.UID;
		}
		public static bool operator !=(DicomUID a, DicomUID b) {
			return !(a == b);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(this, obj))
				return true;
			if (!(obj is DicomUID))
				return false;
			return (obj as DicomUID).UID == UID;
		}

		public override int GetHashCode() {
			return UID.GetHashCode();
		}

		public override string ToString() {
			return String.Format("{0} [{1}]", Name, UID);
		}
	}
}
