// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

namespace FellowOakDicom
{

    public partial class DicomUID
    {
        private static void LoadPrivateUIDs()
        {
            _uids.Add(DicomUID.GEPrivateImplicitVRBigEndian.UID, DicomUID.GEPrivateImplicitVRBigEndian);
            _uids.Add(DicomUID.PrivateFujiCRImageStorage.UID, DicomUID.PrivateFujiCRImageStorage);
            _uids.Add(DicomUID.PrivateGEDicomCTImageInfoObject.UID, DicomUID.PrivateGEDicomCTImageInfoObject);
            _uids.Add(DicomUID.PrivateGEDicomDisplayImageInfoObject.UID, DicomUID.PrivateGEDicomDisplayImageInfoObject);
            _uids.Add(DicomUID.PrivateGEDicomMRImageInfoObject.UID, DicomUID.PrivateGEDicomMRImageInfoObject);
            _uids.Add(DicomUID.PrivatePhilipsCTSyntheticImageStorage.UID, DicomUID.PrivatePhilipsCTSyntheticImageStorage);
            _uids.Add(DicomUID.PrivatePhilipsCXImageStorage.UID, DicomUID.PrivatePhilipsCXImageStorage);
            _uids.Add(DicomUID.PrivatePhilipsCXSyntheticImageStorage.UID, DicomUID.PrivatePhilipsCXSyntheticImageStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRColorImageStorage.UID, DicomUID.PrivatePhilipsMRColorImageStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRSyntheticImageStorage.UID, DicomUID.PrivatePhilipsMRSyntheticImageStorage);
            _uids.Add(DicomUID.PrivatePhilipsPerfusionImageStorage.UID, DicomUID.PrivatePhilipsPerfusionImageStorage);
            _uids.Add(DicomUID.PrivatePixelMedFloatingPointImageStorage.UID, DicomUID.PrivatePixelMedFloatingPointImageStorage);
            _uids.Add(DicomUID.PrivatePixelMedLegacyConvertedEnhancedCTImageStorage.UID, DicomUID.PrivatePixelMedLegacyConvertedEnhancedCTImageStorage);
            _uids.Add(DicomUID.PrivatePixelMedLegacyConvertedEnhancedMRImageStorage.UID, DicomUID.PrivatePixelMedLegacyConvertedEnhancedMRImageStorage);
            _uids.Add(DicomUID.PrivatePixelMedLegacyConvertedEnhancedPETImageStorage.UID, DicomUID.PrivatePixelMedLegacyConvertedEnhancedPETImageStorage);
            _uids.Add(DicomUID.PrivatePMODMultiFrameImageStorage.UID, DicomUID.PrivatePMODMultiFrameImageStorage);
            _uids.Add(DicomUID.PrivateToshibaUSImageStorage.UID, DicomUID.PrivateToshibaUSImageStorage);
            _uids.Add(DicomUID.PrivateAgfaArrivalTransaction.UID, DicomUID.PrivateAgfaArrivalTransaction);
            _uids.Add(DicomUID.PrivateAgfaBasicAttributePresentationState.UID, DicomUID.PrivateAgfaBasicAttributePresentationState);
            _uids.Add(DicomUID.PrivateAgfaDictationTransaction.UID, DicomUID.PrivateAgfaDictationTransaction);
            _uids.Add(DicomUID.PrivateAgfaReportApprovalTransaction.UID, DicomUID.PrivateAgfaReportApprovalTransaction);
            _uids.Add(DicomUID.PrivateAgfaReportTranscriptionTransaction.UID, DicomUID.PrivateAgfaReportTranscriptionTransaction);
            _uids.Add(DicomUID.PrivateERADPracticeBuilderReportDictationStorage.UID, DicomUID.PrivateERADPracticeBuilderReportDictationStorage);
            _uids.Add(DicomUID.PrivateERADPracticeBuilderReportTextStorage.UID, DicomUID.PrivateERADPracticeBuilderReportTextStorage);
            _uids.Add(DicomUID.PrivateGE3DModelStorage.UID, DicomUID.PrivateGE3DModelStorage);
            _uids.Add(DicomUID.PrivateGECollageStorage.UID, DicomUID.PrivateGECollageStorage);
            _uids.Add(DicomUID.PrivateGEeNTEGRAProtocolorNMGenieStorage.UID, DicomUID.PrivateGEeNTEGRAProtocolorNMGenieStorage);
            _uids.Add(DicomUID.PrivateGEPETRawDataStorage.UID, DicomUID.PrivateGEPETRawDataStorage);
            _uids.Add(DicomUID.PrivateGERTPlanStorage.UID, DicomUID.PrivateGERTPlanStorage);
            _uids.Add(DicomUID.PrivatePhilips3DObjectStorage.UID, DicomUID.PrivatePhilips3DObjectStorage);
            _uids.Add(DicomUID.PrivatePhilips3DObjectStorageRetired.UID, DicomUID.PrivatePhilips3DObjectStorageRetired);
            _uids.Add(DicomUID.PrivatePhilips3DPresentationStateStorage.UID, DicomUID.PrivatePhilips3DPresentationStateStorage);
            _uids.Add(DicomUID.PrivatePhilipsCompositeObjectStorage.UID, DicomUID.PrivatePhilipsCompositeObjectStorage);
            _uids.Add(DicomUID.PrivatePhilipsHPLive3D01Storage.UID, DicomUID.PrivatePhilipsHPLive3D01Storage);
            _uids.Add(DicomUID.PrivatePhilipsHPLive3D02Storage.UID, DicomUID.PrivatePhilipsHPLive3D02Storage);
            _uids.Add(DicomUID.PrivatePhilipsLiveRunStorage.UID, DicomUID.PrivatePhilipsLiveRunStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRCardioAnalysisStorage.UID, DicomUID.PrivatePhilipsMRCardioAnalysisStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRCardioAnalysisStorageRetired.UID, DicomUID.PrivatePhilipsMRCardioAnalysisStorageRetired);
            _uids.Add(DicomUID.PrivatePhilipsMRCardioProfileStorage.UID, DicomUID.PrivatePhilipsMRCardioProfileStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRCardioStorage.UID, DicomUID.PrivatePhilipsMRCardioStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRCardioStorageRetired.UID, DicomUID.PrivatePhilipsMRCardioStorageRetired);
            _uids.Add(DicomUID.PrivatePhilipsMRExamcardStorage.UID, DicomUID.PrivatePhilipsMRExamcardStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRSeriesDataStorage.UID, DicomUID.PrivatePhilipsMRSeriesDataStorage);
            _uids.Add(DicomUID.PrivatePhilipsMRSpectrumStorage.UID, DicomUID.PrivatePhilipsMRSpectrumStorage);
            _uids.Add(DicomUID.PrivatePhilipsPerfusionStorage.UID, DicomUID.PrivatePhilipsPerfusionStorage);
            _uids.Add(DicomUID.PrivatePhilipsReconstructionStorage.UID, DicomUID.PrivatePhilipsReconstructionStorage);
            _uids.Add(DicomUID.PrivatePhilipsRunStorage.UID, DicomUID.PrivatePhilipsRunStorage);
            _uids.Add(DicomUID.PrivatePhilipsSpecialisedXAStorage.UID, DicomUID.PrivatePhilipsSpecialisedXAStorage);
            _uids.Add(DicomUID.PrivatePhilipsSurfaceStorage.UID, DicomUID.PrivatePhilipsSurfaceStorage);
            _uids.Add(DicomUID.PrivatePhilipsSurfaceStorageRetired.UID, DicomUID.PrivatePhilipsSurfaceStorageRetired);
            _uids.Add(DicomUID.PrivatePhilipsVolumeSetStorage.UID, DicomUID.PrivatePhilipsVolumeSetStorage);
            _uids.Add(DicomUID.PrivatePhilipsVolumeStorage.UID, DicomUID.PrivatePhilipsVolumeStorage);
            _uids.Add(DicomUID.PrivatePhilipsVolumeStorageRetired.UID, DicomUID.PrivatePhilipsVolumeStorageRetired);
            _uids.Add(DicomUID.PrivatePhilipsVRMLStorage.UID, DicomUID.PrivatePhilipsVRMLStorage);
            _uids.Add(DicomUID.PrivatePhilipsXRayMFStorage.UID, DicomUID.PrivatePhilipsXRayMFStorage);
            _uids.Add(DicomUID.PrivateSiemensAXFrameSetsStorage.UID, DicomUID.PrivateSiemensAXFrameSetsStorage);
            _uids.Add(DicomUID.PrivateSiemensCSANonImageStorage.UID, DicomUID.PrivateSiemensCSANonImageStorage);
            _uids.Add(DicomUID.PrivateSiemensCTMRVolumeStorage.UID, DicomUID.PrivateSiemensCTMRVolumeStorage);
            _uids.Add(DicomUID.PrivateTomTecAnnotationStorage.UID, DicomUID.PrivateTomTecAnnotationStorage);
        }

        /// <summary>GE Private Implicit VR Big Endian</summary>
        /// <remarks>Same as Implicit VR Little Endian except for big endian pixel data.</remarks>
        public static readonly DicomUID GEPrivateImplicitVRBigEndian = new DicomUID("1.2.840.113619.5.2", "Private GE Implicit VR Big Endian", DicomUidType.TransferSyntax);

        /// <summary>Private Fuji CR Image Storage</summary>
        public static readonly DicomUID PrivateFujiCRImageStorage = new DicomUID("1.2.392.200036.9125.1.1.2", "Private Fuji CR Image Storage", DicomUidType.SOPClass);

        /// <summary>Private GE Dicom CT Image Info Object</summary>
        public static readonly DicomUID PrivateGEDicomCTImageInfoObject = new DicomUID("1.2.840.113619.4.3", "Private GE Dicom CT Image Info Object", DicomUidType.SOPClass);

        /// <summary>Private GE Dicom Display Image Info Object</summary>
        public static readonly DicomUID PrivateGEDicomDisplayImageInfoObject = new DicomUID("1.2.840.113619.4.4", "Private GE Dicom Display Image Info Object", DicomUidType.SOPClass);

        /// <summary>Private GE Dicom MR Image Info Object</summary>
        public static readonly DicomUID PrivateGEDicomMRImageInfoObject = new DicomUID("1.2.840.113619.4.2", "Private GE Dicom MR Image Info Object", DicomUidType.SOPClass);

        /// <summary>Private Philips CT Synthetic Image Storage</summary>
        public static readonly DicomUID PrivatePhilipsCTSyntheticImageStorage = new DicomUID("1.3.46.670589.5.0.9", "Private Philips CT Synthetic Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips CX Image Storage</summary>
        public static readonly DicomUID PrivatePhilipsCXImageStorage = new DicomUID("1.3.46.670589.2.4.1.1", "Private Philips CX Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips CX Synthetic Image Storage</summary>
        public static readonly DicomUID PrivatePhilipsCXSyntheticImageStorage = new DicomUID("1.3.46.670589.5.0.12", "Private Philips CX Synthetic Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Color Image Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRColorImageStorage = new DicomUID("1.3.46.670589.11.0.0.12.3", "Private Philips MR Color Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Synthetic Image Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRSyntheticImageStorage = new DicomUID("1.3.46.670589.5.0.10", "Private Philips MR Synthetic Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Perfusion Image Storage</summary>
        public static readonly DicomUID PrivatePhilipsPerfusionImageStorage = new DicomUID("1.3.46.670589.5.0.14", "Private Philips Perfusion Image Storage", DicomUidType.SOPClass);

        /// <summary>Private PixelMed Floating Point Image Storage</summary>
        public static readonly DicomUID PrivatePixelMedFloatingPointImageStorage = new DicomUID("1.3.6.1.4.1.5962.301.9", "Private PixelMed Floating Point Image Storage", DicomUidType.SOPClass);

        /// <summary>Private PixelMed Legacy Converted Enhanced CT Image Storage</summary>
        public static readonly DicomUID PrivatePixelMedLegacyConvertedEnhancedCTImageStorage = new DicomUID("1.3.6.1.4.1.5962.301.1", "Private PixelMed Legacy Converted Enhanced CT Image Storage", DicomUidType.SOPClass);

        /// <summary>Private PixelMed Legacy Converted Enhanced MR Image Storage</summary>
        public static readonly DicomUID PrivatePixelMedLegacyConvertedEnhancedMRImageStorage = new DicomUID("1.3.6.1.4.1.5962.301.2", "Private PixelMed Legacy Converted Enhanced MR Image Storage", DicomUidType.SOPClass);

        /// <summary>Private PixelMed Legacy Converted Enhanced PET Image Storage</summary>
        public static readonly DicomUID PrivatePixelMedLegacyConvertedEnhancedPETImageStorage = new DicomUID("1.3.6.1.4.1.5962.301.3", "Private PixelMed Legacy Converted Enhanced PET Image Storage", DicomUidType.SOPClass);

        /// <summary>Private PMOD Multi-frame Image Storage</summary>
        public static readonly DicomUID PrivatePMODMultiFrameImageStorage = new DicomUID("2.16.840.1.114033.5.1.4.1.1.130", "Private PMOD Multi-frame Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Toshiba US Image Storage</summary>
        public static readonly DicomUID PrivateToshibaUSImageStorage = new DicomUID("1.2.392.200036.9116.7.8.1.1.1", "Private Toshiba US Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Agfa Arrival Transaction</summary>
        public static readonly DicomUID PrivateAgfaArrivalTransaction = new DicomUID("1.2.124.113532.3500.8.1", "Private Agfa Arrival Transaction", DicomUidType.SOPClass);

        /// <summary>Private Agfa Basic Attribute Presentation State</summary>
        public static readonly DicomUID PrivateAgfaBasicAttributePresentationState = new DicomUID("1.2.124.113532.3500.7", "Private Agfa Basic Attribute Presentation State", DicomUidType.SOPClass);

        /// <summary>Private Agfa Dictation Transaction</summary>
        public static readonly DicomUID PrivateAgfaDictationTransaction = new DicomUID("1.2.124.113532.3500.8.2", "Private Agfa Dictation Transaction", DicomUidType.SOPClass);

        /// <summary>Private Agfa Report Approval Transaction</summary>
        public static readonly DicomUID PrivateAgfaReportApprovalTransaction = new DicomUID("1.2.124.113532.3500.8.4", "Private Agfa Report Approval Transaction", DicomUidType.SOPClass);

        /// <summary>Private Agfa Report Transcription Transaction</summary>
        public static readonly DicomUID PrivateAgfaReportTranscriptionTransaction = new DicomUID("1.2.124.113532.3500.8.3", "Private Agfa Report Transcription Transaction", DicomUidType.SOPClass);

        /// <summary>Private ERAD Practice Builder Report Dictation Storage</summary>
        public static readonly DicomUID PrivateERADPracticeBuilderReportDictationStorage = new DicomUID("1.2.826.0.1.3680043.293.1.0.2", "Private ERAD Practice Builder Report Dictation Storage", DicomUidType.SOPClass);

        /// <summary>Private ERAD Practice Builder Report Text Storage</summary>
        public static readonly DicomUID PrivateERADPracticeBuilderReportTextStorage = new DicomUID("1.2.826.0.1.3680043.293.1.0.1", "Private ERAD Practice Builder Report Text Storage", DicomUidType.SOPClass);

        /// <summary>Private GE 3D Model Storage</summary>
        public static readonly DicomUID PrivateGE3DModelStorage = new DicomUID("1.2.840.113619.4.26", "Private GE 3D Model Storage", DicomUidType.SOPClass);

        /// <summary>Private GE Collage Storage</summary>
        public static readonly DicomUID PrivateGECollageStorage = new DicomUID("1.2.528.1.1001.5.1.1.1", "Private GE Collage Storage", DicomUidType.SOPClass);

        /// <summary>Private GE eNTEGRA Protocol or NM Genie Storage</summary>
        public static readonly DicomUID PrivateGEeNTEGRAProtocolorNMGenieStorage = new DicomUID("1.2.840.113619.4.27", "Private GE eNTEGRA Protocol or NM Genie Storage", DicomUidType.SOPClass);

        /// <summary>Private GE PET Raw Data Storage</summary>
        public static readonly DicomUID PrivateGEPETRawDataStorage = new DicomUID("1.2.840.113619.4.30", "Private GE PET Raw Data Storage", DicomUidType.SOPClass);

        /// <summary>Private GE RT Plan Storage</summary>
        public static readonly DicomUID PrivateGERTPlanStorage = new DicomUID("1.2.840.113619.4.5.249", "Private GE RT Plan Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips 3D Object Storage</summary>
        public static readonly DicomUID PrivatePhilips3DObjectStorage = new DicomUID("1.3.46.670589.5.0.2.1", "Private Philips 3D Object Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips 3D Object Storage (Retired)</summary>
        public static readonly DicomUID PrivatePhilips3DObjectStorageRetired = new DicomUID("1.3.46.670589.5.0.2", "Private Philips 3D Object Storage (Retired)", DicomUidType.SOPClass, true);

        /// <summary>Private Philips 3D Presentation State Storage</summary>
        public static readonly DicomUID PrivatePhilips3DPresentationStateStorage = new DicomUID("1.3.46.670589.2.5.1.1", "Private Philips 3D Presentation State Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Composite Object Storage</summary>
        public static readonly DicomUID PrivatePhilipsCompositeObjectStorage = new DicomUID("1.3.46.670589.5.0.4", "Private Philips Composite Object Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips HP Live 3D 01 Storage</summary>
        public static readonly DicomUID PrivatePhilipsHPLive3D01Storage = new DicomUID("1.2.840.113543.6.6.1.3.10001", "Private Philips HP Live 3D 01 Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips HP Live 3D 02 Storage</summary>
        public static readonly DicomUID PrivatePhilipsHPLive3D02Storage = new DicomUID("1.2.840.113543.6.6.1.3.10002", "Private Philips HP Live 3D 02 Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Live Run Storage</summary>
        public static readonly DicomUID PrivatePhilipsLiveRunStorage = new DicomUID("1.3.46.670589.7.8.1618510092", "Private Philips Live Run Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Cardio Analysis Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRCardioAnalysisStorage = new DicomUID("1.3.46.670589.5.0.11.1", "Private Philips MR Cardio Analysis Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Cardio Analysis Storage (Retired)</summary>
        public static readonly DicomUID PrivatePhilipsMRCardioAnalysisStorageRetired = new DicomUID("1.3.46.670589.5.0.11", "Private Philips MR Cardio Analysis Storage (Retired)", DicomUidType.SOPClass, true);

        /// <summary>Private Philips MR Cardio Profile Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRCardioProfileStorage = new DicomUID("1.3.46.670589.5.0.7", "Private Philips MR Cardio Profile Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Cardio Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRCardioStorage = new DicomUID("1.3.46.670589.5.0.8.1", "Private Philips MR Cardio Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Cardio Storage (Retired)</summary>
        public static readonly DicomUID PrivatePhilipsMRCardioStorageRetired = new DicomUID("1.3.46.670589.5.0.8", "Private Philips MR Cardio Storage (Retired)", DicomUidType.SOPClass, true);

        /// <summary>Private Philips MR Examcard Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRExamcardStorage = new DicomUID("1.3.46.670589.11.0.0.12.4", "Private Philips MR Examcard Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Series Data Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRSeriesDataStorage = new DicomUID("1.3.46.670589.11.0.0.12.2", "Private Philips MR Series Data Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips MR Spectrum Storage</summary>
        public static readonly DicomUID PrivatePhilipsMRSpectrumStorage = new DicomUID("1.3.46.670589.11.0.0.12.1", "Private Philips MR Spectrum Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Perfusion Storage</summary>
        public static readonly DicomUID PrivatePhilipsPerfusionStorage = new DicomUID("1.3.46.670589.5.0.13", "Private Philips Perfusion Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Reconstruction Storage</summary>
        public static readonly DicomUID PrivatePhilipsReconstructionStorage = new DicomUID("1.3.46.670589.7.8.16185100130", "Private Philips Reconstruction Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Run Storage</summary>
        public static readonly DicomUID PrivatePhilipsRunStorage = new DicomUID("1.3.46.670589.7.8.16185100129", "Private Philips Run Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Specialised XA Storage</summary>
        public static readonly DicomUID PrivatePhilipsSpecialisedXAStorage = new DicomUID("1.3.46.670589.2.3.1.1", "Private Philips Specialised XA Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Surface Storage</summary>
        public static readonly DicomUID PrivatePhilipsSurfaceStorage = new DicomUID("1.3.46.670589.5.0.3.1", "Private Philips Surface Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Surface Storage (Retired)</summary>
        public static readonly DicomUID PrivatePhilipsSurfaceStorageRetired = new DicomUID("1.3.46.670589.5.0.3", "Private Philips Surface Storage (Retired)", DicomUidType.SOPClass, true);

        /// <summary>Private Philips Volume Set Storage</summary>
        public static readonly DicomUID PrivatePhilipsVolumeSetStorage = new DicomUID("1.3.46.670589.2.11.1.1", "Private Philips Volume Set Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Volume Storage</summary>
        public static readonly DicomUID PrivatePhilipsVolumeStorage = new DicomUID("1.3.46.670589.5.0.1.1", "Private Philips Volume Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips Volume Storage (Retired)</summary>
        public static readonly DicomUID PrivatePhilipsVolumeStorageRetired = new DicomUID("1.3.46.670589.5.0.1", "Private Philips Volume Storage (Retired)", DicomUidType.SOPClass, true);

        /// <summary>Private Philips VRML Storage</summary>
        public static readonly DicomUID PrivatePhilipsVRMLStorage = new DicomUID("1.3.46.670589.2.8.1.1", "Private Philips VRML Storage", DicomUidType.SOPClass);

        /// <summary>Private Philips X-Ray MF Storage</summary>
        public static readonly DicomUID PrivatePhilipsXRayMFStorage = new DicomUID("1.3.46.670589.7.8.1618510091", "Private Philips X-Ray MF Storage", DicomUidType.SOPClass);

        /// <summary>Private Siemens AX Frame Sets Storage</summary>
        public static readonly DicomUID PrivateSiemensAXFrameSetsStorage = new DicomUID("1.3.12.2.1107.5.99.3.11", "Private Siemens AX Frame Sets Storage", DicomUidType.SOPClass);

        /// <summary>Private Siemens CSA Non Image Storage</summary>
        public static readonly DicomUID PrivateSiemensCSANonImageStorage = new DicomUID("1.3.12.2.1107.5.9.1", "Private Siemens CSA Non Image Storage", DicomUidType.SOPClass);

        /// <summary>Private Siemens CT MR Volume Storage</summary>
        public static readonly DicomUID PrivateSiemensCTMRVolumeStorage = new DicomUID("1.3.12.2.1107.5.99.3.10", "Private Siemens CT MR Volume Storage", DicomUidType.SOPClass);

        /// <summary>Private TomTec Annotation Storage</summary>
        public static readonly DicomUID PrivateTomTecAnnotationStorage = new DicomUID("1.2.276.0.48.5.1.4.1.1.7", "Private TomTec Annotation Storage", DicomUidType.SOPClass);

    }
}
