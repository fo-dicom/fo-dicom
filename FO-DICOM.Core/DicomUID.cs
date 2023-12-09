// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

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

        public static DicomUID Append(DicomUID baseUid, long nextSeq)
        {
            var uid = new StringBuilder();
            uid.Append(baseUid.UID).Append('.').Append(nextSeq);
            return new DicomUID(uid.ToString(), "SOP Instance UID", DicomUidType.SOPInstance);
        }

        public static bool IsValidUid(string uid)
        {
            try
            {
                DicomValidation.ValidateUI(uid);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DicomUID Parse(string s) => Parse(s, "Unknown", DicomUidType.Unknown);

        public static DicomUID Parse(string s, string name = "Unknown", DicomUidType type = DicomUidType.Unknown)
        {
            string u = s.TrimEnd(' ', '\0');

            return _uids.TryGetValue(u, out DicomUID uid)
                ? uid
                : new DicomUID(u, name, type);
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
                if (!UID.StartsWith("1.2.840.10008") && Type == DicomUidType.SOPClass)
                {
                    return DicomStorageCategory.Private;
                }

                if (Type != DicomUidType.SOPClass || Name.StartsWith("Storage Commitment") || !Name.Contains("Storage"))
                {
                    return DicomStorageCategory.None;
                }

                if (Name.Contains("Image Storage"))
                {
                    return DicomStorageCategory.Image;
                }

                if (Name.Contains("Volume Storage"))
                {
                    return DicomStorageCategory.Volume;
                }

                if (this == BlendingSoftcopyPresentationStateStorage
                    || this == ColorSoftcopyPresentationStateStorage
                    || this == GrayscaleSoftcopyPresentationStateStorage
                    || this == PseudoColorSoftcopyPresentationStateStorage)
                {
                    return DicomStorageCategory.PresentationState;
                }

                if (this == AudioSRStorageTrialRETIRED
                    || this == BasicTextSRStorage
                    || this == ChestCADSRStorage
                    || this == ComprehensiveSRStorage
                    || this == ComprehensiveSRStorageTrialRETIRED
                    || this == DetailSRStorageTrialRETIRED
                    || this == EnhancedSRStorage
                    || this == MammographyCADSRStorage
                    || this == TextSRStorageTrialRETIRED
                    || this == XRayRadiationDoseSRStorage)
                {
                    return DicomStorageCategory.StructuredReport;
                }

                if (this == AmbulatoryECGWaveformStorage
                    || this == BasicVoiceAudioWaveformStorage
                    || this == CardiacElectrophysiologyWaveformStorage
                    || this == GeneralECGWaveformStorage
                    || this == HemodynamicWaveformStorage
                    || this == TwelveLeadECGWaveformStorage
                    || this == WaveformStorageTrialRETIRED)
                {
                    return DicomStorageCategory.Waveform;
                }

                if (this == EncapsulatedCDAStorage
                    || this == EncapsulatedPDFStorage)
                {
                    return DicomStorageCategory.Document;
                }

                if (this == RawDataStorage)
                {
                    return DicomStorageCategory.Raw;
                }

                return DicomStorageCategory.Other;
            }
        }

        public static bool operator ==(DicomUID a, DicomUID b)
        {
            if ((a is null) && (b is null))
            {
                return true;
            }

            if ((a is null) || (b is null))
            {
                return false;
            }

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
                DicomUID.BasicGrayscalePrintManagementMeta, new DicomUID[]
                {
                    DicomUID.BasicFilmSession,
                    DicomUID.BasicFilmBox,
                    DicomUID.BasicGrayscaleImageBox,
                    DicomUID.Printer
                }
            },
            {
                DicomUID.BasicColorPrintManagementMeta, new DicomUID[]
                {
                    DicomUID.BasicFilmSession,
                    DicomUID.BasicFilmBox,
                    DicomUID.BasicColorImageBox,
                    DicomUID.Printer
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
