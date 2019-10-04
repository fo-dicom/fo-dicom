// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FellowOakDicom
{

    public enum DicomUidType
    {
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
        MappingResource,
        ContextGroupName,
        Unknown
    }

    public enum DicomStorageCategory
    {
        None,
        Image,
        PresentationState,
        StructuredReport,
        Waveform,
        Document,
        Raw,
        Other,
        Private,
        Volume
    }

    public sealed partial class DicomUID : DicomParseable
    {
        public static string RootUID { get; set; } = "1.2.826.0.1.3680043.2.1343.1";

        public DicomUID(string uid, string name, DicomUidType type, bool retired = false)
        {
            UID = uid;
            Name = name;
            Type = type;
            IsRetired = retired;
        }

        public string UID { get; private set; }

        public string Name { get; private set; }

        public DicomUidType Type { get; private set; }

        public bool IsRetired { get; private set; }

        public static void Register(DicomUID uid)
        {
            _uids.Add(uid.UID, uid);
        }

        public static DicomUID Generate()
        {
            return DicomUIDGenerator.GenerateDerivedFromUUID();
        }

        [Obsolete("This method may return statistically non-unique UIDs and is deprecated, use the method Generate()")]
        public static DicomUID Generate(string name)
        {
            var now = DateTime.UtcNow;
            var uid = $"{RootUID}.{now.ToString("yyyyMMddHHmmss")}.{now.Ticks}";

            return new DicomUID(uid, name, DicomUidType.SOPInstance);
        }

        public static DicomUID Append(DicomUID baseUid, long nextSeq)
        {
            StringBuilder uid = new StringBuilder();
            uid.Append(baseUid.UID).Append('.').Append(nextSeq);
            return new DicomUID(uid.ToString(), "SOP Instance UID", DicomUidType.SOPInstance);
        }

        [Obsolete("This validation of Uid also accepts invalid uids like leading zeros or multiple dots without numbers etc", false)]
        public static bool IsValid(string uid)
        {
            if (string.IsNullOrEmpty(uid)) return false;

            // only checks that the UID contains valid characters
            foreach (char c in uid)
            {
                if (c != '.' && !char.IsDigit(c)) return false;
            }

            return true;
        }

        public static DicomUID Parse(string s)
        {
            return Parse(s, "Unknown", DicomUidType.Unknown);
        }

        public static DicomUID Parse(string s, string name = "Unknown", DicomUidType type = DicomUidType.Unknown)
        {
            string u = s.TrimEnd(' ', '\0');

            if (_uids.TryGetValue(u, out DicomUID uid)) return uid;

            return new DicomUID(u, name, type);
        }

        private static readonly IDictionary<string, DicomUID> _uids = new ConcurrentDictionary<string, DicomUID>();

        static DicomUID()
        {
            LoadInternalUIDs();
            LoadPrivateUIDs();
        }

        public static IEnumerable<DicomUID> Enumerate()
        {
            return _uids.Values;
        }

        public bool IsImageStorage => StorageCategory == DicomStorageCategory.Image;

        public bool IsVolumeStorage => StorageCategory == DicomStorageCategory.Volume;

        public DicomStorageCategory StorageCategory
        {
            get
            {
                if (!UID.StartsWith("1.2.840.10008") && Type == DicomUidType.SOPClass) return DicomStorageCategory.Private;

                if (Type != DicomUidType.SOPClass || !Name.Contains("Storage")) return DicomStorageCategory.None;

                if (Name.Contains("Image Storage")) return DicomStorageCategory.Image;

                if (Name.Contains("Volume Storage")) return DicomStorageCategory.Volume;

                if (this == DicomUID.BlendingSoftcopyPresentationStateStorage
                    || this == DicomUID.ColorSoftcopyPresentationStateStorage
                    || this == DicomUID.GrayscaleSoftcopyPresentationStateStorage
                    || this == DicomUID.PseudoColorSoftcopyPresentationStateStorage) return DicomStorageCategory.PresentationState;

                if (this == DicomUID.AudioSRStorageTrialRETIRED || this == DicomUID.BasicTextSRStorage
                    || this == DicomUID.ChestCADSRStorage || this == DicomUID.ComprehensiveSRStorage
                    || this == DicomUID.ComprehensiveSRStorageTrialRETIRED
                    || this == DicomUID.DetailSRStorageTrialRETIRED || this == DicomUID.EnhancedSRStorage
                    || this == DicomUID.MammographyCADSRStorage || this == DicomUID.TextSRStorageTrialRETIRED
                    || this == DicomUID.XRayRadiationDoseSRStorage) return DicomStorageCategory.StructuredReport;

                if (this == DicomUID.AmbulatoryECGWaveformStorage || this == DicomUID.BasicVoiceAudioWaveformStorage
                    || this == DicomUID.CardiacElectrophysiologyWaveformStorage
                    || this == DicomUID.GeneralECGWaveformStorage || this == DicomUID.HemodynamicWaveformStorage
                    || this == DicomUID.TwelveLeadECGWaveformStorage || this == DicomUID.WaveformStorageTrialRETIRED) return DicomStorageCategory.Waveform;

                if (this == DicomUID.EncapsulatedCDAStorage || this == DicomUID.EncapsulatedPDFStorage) return DicomStorageCategory.Document;

                if (this == DicomUID.RawDataStorage) return DicomStorageCategory.Raw;

                return DicomStorageCategory.Other;
            }
        }

        public static bool operator ==(DicomUID a, DicomUID b)
        {
            if ((a is null) && (b is null)) return true;
            if ((a is null) || (b is null)) return false;
            return a.UID == b.UID;
        }

        public static bool operator !=(DicomUID a, DicomUID b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is DicomUID)) return false;
            return (obj as DicomUID).UID == UID;
        }

        public override int GetHashCode()
        {
            return UID.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name} [{UID}]";
        }

    }


    public static class MetaSopClasses
    {

        public static Dictionary<DicomUID, DicomUID[]> Instances { get; } = new Dictionary<DicomUID, DicomUID[]>
        {
            {
                DicomUID.BasicGrayscalePrintManagementMetaSOPClass, new DicomUID[]
                {
                    DicomUID.BasicFilmSessionSOPClass,
                    DicomUID.BasicFilmBoxSOPClass,
                    DicomUID.BasicGrayscaleImageBoxSOPClass,
                    DicomUID.PrinterSOPClass
                }
            },
            {
                DicomUID.BasicColorPrintManagementMetaSOPClass, new DicomUID[]
                {
                    DicomUID.BasicFilmSessionSOPClass,
                    DicomUID.BasicFilmBoxSOPClass,
                    DicomUID.BasicColorImageBoxSOPClass,
                    DicomUID.PrinterSOPClass
                }
            }
        };


        public static DicomUID[] GetMetaSopClass(DicomUID sopClass)
        {
            return Instances
                .Where(kvp => kvp.Value.Contains(sopClass))
                .Select(kvp => kvp.Key)
                .DefaultIfEmpty(sopClass)
                .ToArray();
        }

    }

}
