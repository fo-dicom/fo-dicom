// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FellowOakDicom
{

    public partial class DicomUID
    {

        private static void LoadInternalUIDs()
        {
            _uids.Add(DicomUID.Verification.UID, DicomUID.Verification);
            _uids.Add(DicomUID.ImplicitVRLittleEndian.UID, DicomUID.ImplicitVRLittleEndian);
            _uids.Add(DicomUID.ExplicitVRLittleEndian.UID, DicomUID.ExplicitVRLittleEndian);
            _uids.Add(DicomUID.DeflatedExplicitVRLittleEndian.UID, DicomUID.DeflatedExplicitVRLittleEndian);
            _uids.Add(DicomUID.ExplicitVRBigEndianRETIRED.UID, DicomUID.ExplicitVRBigEndianRETIRED);
            _uids.Add(DicomUID.JPEGBaseline1.UID, DicomUID.JPEGBaseline1);
            _uids.Add(DicomUID.JPEGExtended24.UID, DicomUID.JPEGExtended24);
            _uids.Add(DicomUID.JPEGExtended35RETIRED.UID, DicomUID.JPEGExtended35RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionNonHierarchical68RETIRED.UID, DicomUID.JPEGSpectralSelectionNonHierarchical68RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionNonHierarchical79RETIRED.UID, DicomUID.JPEGSpectralSelectionNonHierarchical79RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionNonHierarchical1012RETIRED.UID, DicomUID.JPEGFullProgressionNonHierarchical1012RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionNonHierarchical1113RETIRED.UID, DicomUID.JPEGFullProgressionNonHierarchical1113RETIRED);
            _uids.Add(DicomUID.JPEGLosslessNonHierarchical14.UID, DicomUID.JPEGLosslessNonHierarchical14);
            _uids.Add(DicomUID.JPEGLosslessNonHierarchical15RETIRED.UID, DicomUID.JPEGLosslessNonHierarchical15RETIRED);
            _uids.Add(DicomUID.JPEGExtendedHierarchical1618RETIRED.UID, DicomUID.JPEGExtendedHierarchical1618RETIRED);
            _uids.Add(DicomUID.JPEGExtendedHierarchical1719RETIRED.UID, DicomUID.JPEGExtendedHierarchical1719RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionHierarchical2022RETIRED.UID, DicomUID.JPEGSpectralSelectionHierarchical2022RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionHierarchical2123RETIRED.UID, DicomUID.JPEGSpectralSelectionHierarchical2123RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionHierarchical2426RETIRED.UID, DicomUID.JPEGFullProgressionHierarchical2426RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionHierarchical2527RETIRED.UID, DicomUID.JPEGFullProgressionHierarchical2527RETIRED);
            _uids.Add(DicomUID.JPEGLosslessHierarchical28RETIRED.UID, DicomUID.JPEGLosslessHierarchical28RETIRED);
            _uids.Add(DicomUID.JPEGLosslessHierarchical29RETIRED.UID, DicomUID.JPEGLosslessHierarchical29RETIRED);
            _uids.Add(DicomUID.JPEGLossless.UID, DicomUID.JPEGLossless);
            _uids.Add(DicomUID.JPEGLSLossless.UID, DicomUID.JPEGLSLossless);
            _uids.Add(DicomUID.JPEGLSLossyNearLossless.UID, DicomUID.JPEGLSLossyNearLossless);
            _uids.Add(DicomUID.JPEG2000LosslessOnly.UID, DicomUID.JPEG2000LosslessOnly);
            _uids.Add(DicomUID.JPEG2000.UID, DicomUID.JPEG2000);
            _uids.Add(DicomUID.JPEG2000Part2MultiComponentLosslessOnly.UID, DicomUID.JPEG2000Part2MultiComponentLosslessOnly);
            _uids.Add(DicomUID.JPEG2000Part2MultiComponent.UID, DicomUID.JPEG2000Part2MultiComponent);
            _uids.Add(DicomUID.JPIPReferenced.UID, DicomUID.JPIPReferenced);
            _uids.Add(DicomUID.JPIPReferencedDeflate.UID, DicomUID.JPIPReferencedDeflate);
            _uids.Add(DicomUID.MPEG2.UID, DicomUID.MPEG2);
            _uids.Add(DicomUID.MPEG2MainProfileHighLevel.UID, DicomUID.MPEG2MainProfileHighLevel);
            _uids.Add(DicomUID.MPEG4AVCH264HighProfileLevel41.UID, DicomUID.MPEG4AVCH264HighProfileLevel41);
            _uids.Add(DicomUID.MPEG4AVCH264BDCompatibleHighProfileLevel41.UID, DicomUID.MPEG4AVCH264BDCompatibleHighProfileLevel41);
            _uids.Add(DicomUID.MPEG4AVCH264HighProfileLevel42For2DVideo.UID, DicomUID.MPEG4AVCH264HighProfileLevel42For2DVideo);
            _uids.Add(DicomUID.MPEG4AVCH264HighProfileLevel42For3DVideo.UID, DicomUID.MPEG4AVCH264HighProfileLevel42For3DVideo);
            _uids.Add(DicomUID.MPEG4AVCH264StereoHighProfileLevel42.UID, DicomUID.MPEG4AVCH264StereoHighProfileLevel42);
            _uids.Add(DicomUID.HEVCH265MainProfileLevel51.UID, DicomUID.HEVCH265MainProfileLevel51);
            _uids.Add(DicomUID.HEVCH265Main10ProfileLevel51.UID, DicomUID.HEVCH265Main10ProfileLevel51);
            _uids.Add(DicomUID.RLELossless.UID, DicomUID.RLELossless);
            _uids.Add(DicomUID.RFC2557MIMEEncapsulationRETIRED.UID, DicomUID.RFC2557MIMEEncapsulationRETIRED);
            _uids.Add(DicomUID.XMLEncodingRETIRED.UID, DicomUID.XMLEncodingRETIRED);
            _uids.Add(DicomUID.SMPTEST211020UncompressedProgressiveActiveVideo.UID, DicomUID.SMPTEST211020UncompressedProgressiveActiveVideo);
            _uids.Add(DicomUID.SMPTEST211020UncompressedInterlacedActiveVideo.UID, DicomUID.SMPTEST211020UncompressedInterlacedActiveVideo);
            _uids.Add(DicomUID.SMPTEST211030PCMDigitalAudio.UID, DicomUID.SMPTEST211030PCMDigitalAudio);
            _uids.Add(DicomUID.MediaStorageDirectoryStorage.UID, DicomUID.MediaStorageDirectoryStorage);
            _uids.Add(DicomUID.TalairachBrainAtlasFrameOfReference.UID, DicomUID.TalairachBrainAtlasFrameOfReference);
            _uids.Add(DicomUID.SPM2T1FrameOfReference.UID, DicomUID.SPM2T1FrameOfReference);
            _uids.Add(DicomUID.SPM2T2FrameOfReference.UID, DicomUID.SPM2T2FrameOfReference);
            _uids.Add(DicomUID.SPM2PDFrameOfReference.UID, DicomUID.SPM2PDFrameOfReference);
            _uids.Add(DicomUID.SPM2EPIFrameOfReference.UID, DicomUID.SPM2EPIFrameOfReference);
            _uids.Add(DicomUID.SPM2FILT1FrameOfReference.UID, DicomUID.SPM2FILT1FrameOfReference);
            _uids.Add(DicomUID.SPM2PETFrameOfReference.UID, DicomUID.SPM2PETFrameOfReference);
            _uids.Add(DicomUID.SPM2TRANSMFrameOfReference.UID, DicomUID.SPM2TRANSMFrameOfReference);
            _uids.Add(DicomUID.SPM2SPECTFrameOfReference.UID, DicomUID.SPM2SPECTFrameOfReference);
            _uids.Add(DicomUID.SPM2GRAYFrameOfReference.UID, DicomUID.SPM2GRAYFrameOfReference);
            _uids.Add(DicomUID.SPM2WHITEFrameOfReference.UID, DicomUID.SPM2WHITEFrameOfReference);
            _uids.Add(DicomUID.SPM2CSFFrameOfReference.UID, DicomUID.SPM2CSFFrameOfReference);
            _uids.Add(DicomUID.SPM2BRAINMASKFrameOfReference.UID, DicomUID.SPM2BRAINMASKFrameOfReference);
            _uids.Add(DicomUID.SPM2AVG305T1FrameOfReference.UID, DicomUID.SPM2AVG305T1FrameOfReference);
            _uids.Add(DicomUID.SPM2AVG152T1FrameOfReference.UID, DicomUID.SPM2AVG152T1FrameOfReference);
            _uids.Add(DicomUID.SPM2AVG152T2FrameOfReference.UID, DicomUID.SPM2AVG152T2FrameOfReference);
            _uids.Add(DicomUID.SPM2AVG152PDFrameOfReference.UID, DicomUID.SPM2AVG152PDFrameOfReference);
            _uids.Add(DicomUID.SPM2SINGLESUBJT1FrameOfReference.UID, DicomUID.SPM2SINGLESUBJT1FrameOfReference);
            _uids.Add(DicomUID.ICBM452T1FrameOfReference.UID, DicomUID.ICBM452T1FrameOfReference);
            _uids.Add(DicomUID.ICBMSingleSubjectMRIFrameOfReference.UID, DicomUID.ICBMSingleSubjectMRIFrameOfReference);
            _uids.Add(DicomUID.IEC61217FixedCoordinateSystemFrameOfReference.UID, DicomUID.IEC61217FixedCoordinateSystemFrameOfReference);
            _uids.Add(DicomUID.HotIronColorPaletteSOPInstance.UID, DicomUID.HotIronColorPaletteSOPInstance);
            _uids.Add(DicomUID.PETColorPaletteSOPInstance.UID, DicomUID.PETColorPaletteSOPInstance);
            _uids.Add(DicomUID.HotMetalBlueColorPaletteSOPInstance.UID, DicomUID.HotMetalBlueColorPaletteSOPInstance);
            _uids.Add(DicomUID.PET20StepColorPaletteSOPInstance.UID, DicomUID.PET20StepColorPaletteSOPInstance);
            _uids.Add(DicomUID.SpringColorPaletteSOPInstance.UID, DicomUID.SpringColorPaletteSOPInstance);
            _uids.Add(DicomUID.SummerColorPaletteSOPInstance.UID, DicomUID.SummerColorPaletteSOPInstance);
            _uids.Add(DicomUID.FallColorPaletteSOPInstance.UID, DicomUID.FallColorPaletteSOPInstance);
            _uids.Add(DicomUID.WinterColorPaletteSOPInstance.UID, DicomUID.WinterColorPaletteSOPInstance);
            _uids.Add(DicomUID.BasicStudyContentNotificationSOPClassRETIRED.UID, DicomUID.BasicStudyContentNotificationSOPClassRETIRED);
            _uids.Add(DicomUID.Papyrus3ImplicitVRLittleEndianRETIRED.UID, DicomUID.Papyrus3ImplicitVRLittleEndianRETIRED);
            _uids.Add(DicomUID.StorageCommitmentPushModelSOPClass.UID, DicomUID.StorageCommitmentPushModelSOPClass);
            _uids.Add(DicomUID.StorageCommitmentPushModelSOPInstance.UID, DicomUID.StorageCommitmentPushModelSOPInstance);
            _uids.Add(DicomUID.StorageCommitmentPullModelSOPClassRETIRED.UID, DicomUID.StorageCommitmentPullModelSOPClassRETIRED);
            _uids.Add(DicomUID.StorageCommitmentPullModelSOPInstanceRETIRED.UID, DicomUID.StorageCommitmentPullModelSOPInstanceRETIRED);
            _uids.Add(DicomUID.ProceduralEventLoggingSOPClass.UID, DicomUID.ProceduralEventLoggingSOPClass);
            _uids.Add(DicomUID.ProceduralEventLoggingSOPInstance.UID, DicomUID.ProceduralEventLoggingSOPInstance);
            _uids.Add(DicomUID.SubstanceAdministrationLoggingSOPClass.UID, DicomUID.SubstanceAdministrationLoggingSOPClass);
            _uids.Add(DicomUID.SubstanceAdministrationLoggingSOPInstance.UID, DicomUID.SubstanceAdministrationLoggingSOPInstance);
            _uids.Add(DicomUID.DICOMUIDRegistry.UID, DicomUID.DICOMUIDRegistry);
            _uids.Add(DicomUID.DICOMControlledTerminology.UID, DicomUID.DICOMControlledTerminology);
            _uids.Add(DicomUID.AdultMouseAnatomyOntology.UID, DicomUID.AdultMouseAnatomyOntology);
            _uids.Add(DicomUID.UberonOntology.UID, DicomUID.UberonOntology);
            _uids.Add(DicomUID.IntegratedTaxonomicInformationSystemITISTaxonomicSerialNumberTSN.UID, DicomUID.IntegratedTaxonomicInformationSystemITISTaxonomicSerialNumberTSN);
            _uids.Add(DicomUID.MouseGenomeInitiativeMGI.UID, DicomUID.MouseGenomeInitiativeMGI);
            _uids.Add(DicomUID.PubChemCompoundCID.UID, DicomUID.PubChemCompoundCID);
            _uids.Add(DicomUID.ICD11.UID, DicomUID.ICD11);
            _uids.Add(DicomUID.NewYorkUniversityMelanomaClinicalCooperativeGroup.UID, DicomUID.NewYorkUniversityMelanomaClinicalCooperativeGroup);
            _uids.Add(DicomUID.MayoClinicNonRadiologicalImagesSpecificBodyStructureAnatomicalSurfaceRegionGuide.UID, DicomUID.MayoClinicNonRadiologicalImagesSpecificBodyStructureAnatomicalSurfaceRegionGuide);
            _uids.Add(DicomUID.ImageBiomarkerStandardisationInitiative.UID, DicomUID.ImageBiomarkerStandardisationInitiative);
            _uids.Add(DicomUID.RadiomicsOntology.UID, DicomUID.RadiomicsOntology);
            _uids.Add(DicomUID.DICOMApplicationContextName.UID, DicomUID.DICOMApplicationContextName);
            _uids.Add(DicomUID.DetachedPatientManagementSOPClassRETIRED.UID, DicomUID.DetachedPatientManagementSOPClassRETIRED);
            _uids.Add(DicomUID.DetachedPatientManagementMetaSOPClassRETIRED.UID, DicomUID.DetachedPatientManagementMetaSOPClassRETIRED);
            _uids.Add(DicomUID.DetachedVisitManagementSOPClassRETIRED.UID, DicomUID.DetachedVisitManagementSOPClassRETIRED);
            _uids.Add(DicomUID.DetachedStudyManagementSOPClassRETIRED.UID, DicomUID.DetachedStudyManagementSOPClassRETIRED);
            _uids.Add(DicomUID.StudyComponentManagementSOPClassRETIRED.UID, DicomUID.StudyComponentManagementSOPClassRETIRED);
            _uids.Add(DicomUID.ModalityPerformedProcedureStepSOPClass.UID, DicomUID.ModalityPerformedProcedureStepSOPClass);
            _uids.Add(DicomUID.ModalityPerformedProcedureStepRetrieveSOPClass.UID, DicomUID.ModalityPerformedProcedureStepRetrieveSOPClass);
            _uids.Add(DicomUID.ModalityPerformedProcedureStepNotificationSOPClass.UID, DicomUID.ModalityPerformedProcedureStepNotificationSOPClass);
            _uids.Add(DicomUID.DetachedResultsManagementSOPClassRETIRED.UID, DicomUID.DetachedResultsManagementSOPClassRETIRED);
            _uids.Add(DicomUID.DetachedResultsManagementMetaSOPClassRETIRED.UID, DicomUID.DetachedResultsManagementMetaSOPClassRETIRED);
            _uids.Add(DicomUID.DetachedStudyManagementMetaSOPClassRETIRED.UID, DicomUID.DetachedStudyManagementMetaSOPClassRETIRED);
            _uids.Add(DicomUID.DetachedInterpretationManagementSOPClassRETIRED.UID, DicomUID.DetachedInterpretationManagementSOPClassRETIRED);
            _uids.Add(DicomUID.StorageServiceClass.UID, DicomUID.StorageServiceClass);
            _uids.Add(DicomUID.BasicFilmSessionSOPClass.UID, DicomUID.BasicFilmSessionSOPClass);
            _uids.Add(DicomUID.BasicFilmBoxSOPClass.UID, DicomUID.BasicFilmBoxSOPClass);
            _uids.Add(DicomUID.BasicGrayscaleImageBoxSOPClass.UID, DicomUID.BasicGrayscaleImageBoxSOPClass);
            _uids.Add(DicomUID.BasicColorImageBoxSOPClass.UID, DicomUID.BasicColorImageBoxSOPClass);
            _uids.Add(DicomUID.ReferencedImageBoxSOPClassRETIRED.UID, DicomUID.ReferencedImageBoxSOPClassRETIRED);
            _uids.Add(DicomUID.BasicGrayscalePrintManagementMetaSOPClass.UID, DicomUID.BasicGrayscalePrintManagementMetaSOPClass);
            _uids.Add(DicomUID.ReferencedGrayscalePrintManagementMetaSOPClassRETIRED.UID, DicomUID.ReferencedGrayscalePrintManagementMetaSOPClassRETIRED);
            _uids.Add(DicomUID.PrintJobSOPClass.UID, DicomUID.PrintJobSOPClass);
            _uids.Add(DicomUID.BasicAnnotationBoxSOPClass.UID, DicomUID.BasicAnnotationBoxSOPClass);
            _uids.Add(DicomUID.PrinterSOPClass.UID, DicomUID.PrinterSOPClass);
            _uids.Add(DicomUID.PrinterConfigurationRetrievalSOPClass.UID, DicomUID.PrinterConfigurationRetrievalSOPClass);
            _uids.Add(DicomUID.PrinterSOPInstance.UID, DicomUID.PrinterSOPInstance);
            _uids.Add(DicomUID.PrinterConfigurationRetrievalSOPInstance.UID, DicomUID.PrinterConfigurationRetrievalSOPInstance);
            _uids.Add(DicomUID.BasicColorPrintManagementMetaSOPClass.UID, DicomUID.BasicColorPrintManagementMetaSOPClass);
            _uids.Add(DicomUID.ReferencedColorPrintManagementMetaSOPClassRETIRED.UID, DicomUID.ReferencedColorPrintManagementMetaSOPClassRETIRED);
            _uids.Add(DicomUID.VOILUTBoxSOPClass.UID, DicomUID.VOILUTBoxSOPClass);
            _uids.Add(DicomUID.PresentationLUTSOPClass.UID, DicomUID.PresentationLUTSOPClass);
            _uids.Add(DicomUID.ImageOverlayBoxSOPClassRETIRED.UID, DicomUID.ImageOverlayBoxSOPClassRETIRED);
            _uids.Add(DicomUID.BasicPrintImageOverlayBoxSOPClassRETIRED.UID, DicomUID.BasicPrintImageOverlayBoxSOPClassRETIRED);
            _uids.Add(DicomUID.PrintQueueSOPInstanceRETIRED.UID, DicomUID.PrintQueueSOPInstanceRETIRED);
            _uids.Add(DicomUID.PrintQueueManagementSOPClassRETIRED.UID, DicomUID.PrintQueueManagementSOPClassRETIRED);
            _uids.Add(DicomUID.StoredPrintStorageSOPClassRETIRED.UID, DicomUID.StoredPrintStorageSOPClassRETIRED);
            _uids.Add(DicomUID.HardcopyGrayscaleImageStorageSOPClassRETIRED.UID, DicomUID.HardcopyGrayscaleImageStorageSOPClassRETIRED);
            _uids.Add(DicomUID.HardcopyColorImageStorageSOPClassRETIRED.UID, DicomUID.HardcopyColorImageStorageSOPClassRETIRED);
            _uids.Add(DicomUID.PullPrintRequestSOPClassRETIRED.UID, DicomUID.PullPrintRequestSOPClassRETIRED);
            _uids.Add(DicomUID.PullStoredPrintManagementMetaSOPClassRETIRED.UID, DicomUID.PullStoredPrintManagementMetaSOPClassRETIRED);
            _uids.Add(DicomUID.MediaCreationManagementSOPClassUID.UID, DicomUID.MediaCreationManagementSOPClassUID);
            _uids.Add(DicomUID.DisplaySystemSOPClass.UID, DicomUID.DisplaySystemSOPClass);
            _uids.Add(DicomUID.DisplaySystemSOPInstance.UID, DicomUID.DisplaySystemSOPInstance);
            _uids.Add(DicomUID.ComputedRadiographyImageStorage.UID, DicomUID.ComputedRadiographyImageStorage);
            _uids.Add(DicomUID.DigitalXRayImageStorageForPresentation.UID, DicomUID.DigitalXRayImageStorageForPresentation);
            _uids.Add(DicomUID.DigitalXRayImageStorageForProcessing.UID, DicomUID.DigitalXRayImageStorageForProcessing);
            _uids.Add(DicomUID.DigitalMammographyXRayImageStorageForPresentation.UID, DicomUID.DigitalMammographyXRayImageStorageForPresentation);
            _uids.Add(DicomUID.DigitalMammographyXRayImageStorageForProcessing.UID, DicomUID.DigitalMammographyXRayImageStorageForProcessing);
            _uids.Add(DicomUID.DigitalIntraOralXRayImageStorageForPresentation.UID, DicomUID.DigitalIntraOralXRayImageStorageForPresentation);
            _uids.Add(DicomUID.DigitalIntraOralXRayImageStorageForProcessing.UID, DicomUID.DigitalIntraOralXRayImageStorageForProcessing);
            _uids.Add(DicomUID.CTImageStorage.UID, DicomUID.CTImageStorage);
            _uids.Add(DicomUID.EnhancedCTImageStorage.UID, DicomUID.EnhancedCTImageStorage);
            _uids.Add(DicomUID.LegacyConvertedEnhancedCTImageStorage.UID, DicomUID.LegacyConvertedEnhancedCTImageStorage);
            _uids.Add(DicomUID.UltrasoundMultiFrameImageStorageRETIRED.UID, DicomUID.UltrasoundMultiFrameImageStorageRETIRED);
            _uids.Add(DicomUID.UltrasoundMultiFrameImageStorage.UID, DicomUID.UltrasoundMultiFrameImageStorage);
            _uids.Add(DicomUID.MRImageStorage.UID, DicomUID.MRImageStorage);
            _uids.Add(DicomUID.EnhancedMRImageStorage.UID, DicomUID.EnhancedMRImageStorage);
            _uids.Add(DicomUID.MRSpectroscopyStorage.UID, DicomUID.MRSpectroscopyStorage);
            _uids.Add(DicomUID.EnhancedMRColorImageStorage.UID, DicomUID.EnhancedMRColorImageStorage);
            _uids.Add(DicomUID.LegacyConvertedEnhancedMRImageStorage.UID, DicomUID.LegacyConvertedEnhancedMRImageStorage);
            _uids.Add(DicomUID.NuclearMedicineImageStorageRETIRED.UID, DicomUID.NuclearMedicineImageStorageRETIRED);
            _uids.Add(DicomUID.UltrasoundImageStorageRETIRED.UID, DicomUID.UltrasoundImageStorageRETIRED);
            _uids.Add(DicomUID.UltrasoundImageStorage.UID, DicomUID.UltrasoundImageStorage);
            _uids.Add(DicomUID.EnhancedUSVolumeStorage.UID, DicomUID.EnhancedUSVolumeStorage);
            _uids.Add(DicomUID.SecondaryCaptureImageStorage.UID, DicomUID.SecondaryCaptureImageStorage);
            _uids.Add(DicomUID.MultiFrameSingleBitSecondaryCaptureImageStorage.UID, DicomUID.MultiFrameSingleBitSecondaryCaptureImageStorage);
            _uids.Add(DicomUID.MultiFrameGrayscaleByteSecondaryCaptureImageStorage.UID, DicomUID.MultiFrameGrayscaleByteSecondaryCaptureImageStorage);
            _uids.Add(DicomUID.MultiFrameGrayscaleWordSecondaryCaptureImageStorage.UID, DicomUID.MultiFrameGrayscaleWordSecondaryCaptureImageStorage);
            _uids.Add(DicomUID.MultiFrameTrueColorSecondaryCaptureImageStorage.UID, DicomUID.MultiFrameTrueColorSecondaryCaptureImageStorage);
            _uids.Add(DicomUID.StandaloneOverlayStorageRETIRED.UID, DicomUID.StandaloneOverlayStorageRETIRED);
            _uids.Add(DicomUID.StandaloneCurveStorageRETIRED.UID, DicomUID.StandaloneCurveStorageRETIRED);
            _uids.Add(DicomUID.WaveformStorageTrialRETIRED.UID, DicomUID.WaveformStorageTrialRETIRED);
            _uids.Add(DicomUID.TwelveLeadECGWaveformStorage.UID, DicomUID.TwelveLeadECGWaveformStorage);
            _uids.Add(DicomUID.GeneralECGWaveformStorage.UID, DicomUID.GeneralECGWaveformStorage);
            _uids.Add(DicomUID.AmbulatoryECGWaveformStorage.UID, DicomUID.AmbulatoryECGWaveformStorage);
            _uids.Add(DicomUID.HemodynamicWaveformStorage.UID, DicomUID.HemodynamicWaveformStorage);
            _uids.Add(DicomUID.CardiacElectrophysiologyWaveformStorage.UID, DicomUID.CardiacElectrophysiologyWaveformStorage);
            _uids.Add(DicomUID.BasicVoiceAudioWaveformStorage.UID, DicomUID.BasicVoiceAudioWaveformStorage);
            _uids.Add(DicomUID.GeneralAudioWaveformStorage.UID, DicomUID.GeneralAudioWaveformStorage);
            _uids.Add(DicomUID.ArterialPulseWaveformStorage.UID, DicomUID.ArterialPulseWaveformStorage);
            _uids.Add(DicomUID.RespiratoryWaveformStorage.UID, DicomUID.RespiratoryWaveformStorage);
            _uids.Add(DicomUID.StandaloneModalityLUTStorageRETIRED.UID, DicomUID.StandaloneModalityLUTStorageRETIRED);
            _uids.Add(DicomUID.StandaloneVOILUTStorageRETIRED.UID, DicomUID.StandaloneVOILUTStorageRETIRED);
            _uids.Add(DicomUID.GrayscaleSoftcopyPresentationStateStorage.UID, DicomUID.GrayscaleSoftcopyPresentationStateStorage);
            _uids.Add(DicomUID.ColorSoftcopyPresentationStateStorage.UID, DicomUID.ColorSoftcopyPresentationStateStorage);
            _uids.Add(DicomUID.PseudoColorSoftcopyPresentationStateStorage.UID, DicomUID.PseudoColorSoftcopyPresentationStateStorage);
            _uids.Add(DicomUID.BlendingSoftcopyPresentationStateStorage.UID, DicomUID.BlendingSoftcopyPresentationStateStorage);
            _uids.Add(DicomUID.XAXRFGrayscaleSoftcopyPresentationStateStorage.UID, DicomUID.XAXRFGrayscaleSoftcopyPresentationStateStorage);
            _uids.Add(DicomUID.GrayscalePlanarMPRVolumetricPresentationStateStorage.UID, DicomUID.GrayscalePlanarMPRVolumetricPresentationStateStorage);
            _uids.Add(DicomUID.CompositingPlanarMPRVolumetricPresentationStateStorage.UID, DicomUID.CompositingPlanarMPRVolumetricPresentationStateStorage);
            _uids.Add(DicomUID.AdvancedBlendingPresentationStateStorage.UID, DicomUID.AdvancedBlendingPresentationStateStorage);
            _uids.Add(DicomUID.VolumeRenderingVolumetricPresentationStateStorage.UID, DicomUID.VolumeRenderingVolumetricPresentationStateStorage);
            _uids.Add(DicomUID.SegmentedVolumeRenderingVolumetricPresentationStateStorage.UID, DicomUID.SegmentedVolumeRenderingVolumetricPresentationStateStorage);
            _uids.Add(DicomUID.MultipleVolumeRenderingVolumetricPresentationStateStorage.UID, DicomUID.MultipleVolumeRenderingVolumetricPresentationStateStorage);
            _uids.Add(DicomUID.XRayAngiographicImageStorage.UID, DicomUID.XRayAngiographicImageStorage);
            _uids.Add(DicomUID.EnhancedXAImageStorage.UID, DicomUID.EnhancedXAImageStorage);
            _uids.Add(DicomUID.XRayRadiofluoroscopicImageStorage.UID, DicomUID.XRayRadiofluoroscopicImageStorage);
            _uids.Add(DicomUID.EnhancedXRFImageStorage.UID, DicomUID.EnhancedXRFImageStorage);
            _uids.Add(DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED.UID, DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED);
            _uids.Add(DicomUID.UID_1_2_840_10008_5_1_4_1_1_12_77RETIRED.UID, DicomUID.UID_1_2_840_10008_5_1_4_1_1_12_77RETIRED);
            _uids.Add(DicomUID.XRay3DAngiographicImageStorage.UID, DicomUID.XRay3DAngiographicImageStorage);
            _uids.Add(DicomUID.XRay3DCraniofacialImageStorage.UID, DicomUID.XRay3DCraniofacialImageStorage);
            _uids.Add(DicomUID.BreastTomosynthesisImageStorage.UID, DicomUID.BreastTomosynthesisImageStorage);
            _uids.Add(DicomUID.BreastProjectionXRayImageStorageForPresentation.UID, DicomUID.BreastProjectionXRayImageStorageForPresentation);
            _uids.Add(DicomUID.BreastProjectionXRayImageStorageForProcessing.UID, DicomUID.BreastProjectionXRayImageStorageForProcessing);
            _uids.Add(DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForPresentation.UID, DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForPresentation);
            _uids.Add(DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForProcessing.UID, DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForProcessing);
            _uids.Add(DicomUID.NuclearMedicineImageStorage.UID, DicomUID.NuclearMedicineImageStorage);
            _uids.Add(DicomUID.ParametricMapStorage.UID, DicomUID.ParametricMapStorage);
            _uids.Add(DicomUID.UID_1_2_840_10008_5_1_4_1_1_40RETIRED.UID, DicomUID.UID_1_2_840_10008_5_1_4_1_1_40RETIRED);
            _uids.Add(DicomUID.RawDataStorage.UID, DicomUID.RawDataStorage);
            _uids.Add(DicomUID.SpatialRegistrationStorage.UID, DicomUID.SpatialRegistrationStorage);
            _uids.Add(DicomUID.SpatialFiducialsStorage.UID, DicomUID.SpatialFiducialsStorage);
            _uids.Add(DicomUID.DeformableSpatialRegistrationStorage.UID, DicomUID.DeformableSpatialRegistrationStorage);
            _uids.Add(DicomUID.SegmentationStorage.UID, DicomUID.SegmentationStorage);
            _uids.Add(DicomUID.SurfaceSegmentationStorage.UID, DicomUID.SurfaceSegmentationStorage);
            _uids.Add(DicomUID.TractographyResultsStorage.UID, DicomUID.TractographyResultsStorage);
            _uids.Add(DicomUID.RealWorldValueMappingStorage.UID, DicomUID.RealWorldValueMappingStorage);
            _uids.Add(DicomUID.SurfaceScanMeshStorage.UID, DicomUID.SurfaceScanMeshStorage);
            _uids.Add(DicomUID.SurfaceScanPointCloudStorage.UID, DicomUID.SurfaceScanPointCloudStorage);
            _uids.Add(DicomUID.VLImageStorageTrialRETIRED.UID, DicomUID.VLImageStorageTrialRETIRED);
            _uids.Add(DicomUID.VLMultiFrameImageStorageTrialRETIRED.UID, DicomUID.VLMultiFrameImageStorageTrialRETIRED);
            _uids.Add(DicomUID.VLEndoscopicImageStorage.UID, DicomUID.VLEndoscopicImageStorage);
            _uids.Add(DicomUID.VideoEndoscopicImageStorage.UID, DicomUID.VideoEndoscopicImageStorage);
            _uids.Add(DicomUID.VLMicroscopicImageStorage.UID, DicomUID.VLMicroscopicImageStorage);
            _uids.Add(DicomUID.VideoMicroscopicImageStorage.UID, DicomUID.VideoMicroscopicImageStorage);
            _uids.Add(DicomUID.VLSlideCoordinatesMicroscopicImageStorage.UID, DicomUID.VLSlideCoordinatesMicroscopicImageStorage);
            _uids.Add(DicomUID.VLPhotographicImageStorage.UID, DicomUID.VLPhotographicImageStorage);
            _uids.Add(DicomUID.VideoPhotographicImageStorage.UID, DicomUID.VideoPhotographicImageStorage);
            _uids.Add(DicomUID.OphthalmicPhotography8BitImageStorage.UID, DicomUID.OphthalmicPhotography8BitImageStorage);
            _uids.Add(DicomUID.OphthalmicPhotography16BitImageStorage.UID, DicomUID.OphthalmicPhotography16BitImageStorage);
            _uids.Add(DicomUID.StereometricRelationshipStorage.UID, DicomUID.StereometricRelationshipStorage);
            _uids.Add(DicomUID.OphthalmicTomographyImageStorage.UID, DicomUID.OphthalmicTomographyImageStorage);
            _uids.Add(DicomUID.WideFieldOphthalmicPhotographyStereographicProjectionImageStorage.UID, DicomUID.WideFieldOphthalmicPhotographyStereographicProjectionImageStorage);
            _uids.Add(DicomUID.WideFieldOphthalmicPhotography3DCoordinatesImageStorage.UID, DicomUID.WideFieldOphthalmicPhotography3DCoordinatesImageStorage);
            _uids.Add(DicomUID.OphthalmicOpticalCoherenceTomographyEnFaceImageStorage.UID, DicomUID.OphthalmicOpticalCoherenceTomographyEnFaceImageStorage);
            _uids.Add(DicomUID.OphthalmicOpticalCoherenceTomographyBScanVolumeAnalysisStorage.UID, DicomUID.OphthalmicOpticalCoherenceTomographyBScanVolumeAnalysisStorage);
            _uids.Add(DicomUID.VLWholeSlideMicroscopyImageStorage.UID, DicomUID.VLWholeSlideMicroscopyImageStorage);
            _uids.Add(DicomUID.LensometryMeasurementsStorage.UID, DicomUID.LensometryMeasurementsStorage);
            _uids.Add(DicomUID.AutorefractionMeasurementsStorage.UID, DicomUID.AutorefractionMeasurementsStorage);
            _uids.Add(DicomUID.KeratometryMeasurementsStorage.UID, DicomUID.KeratometryMeasurementsStorage);
            _uids.Add(DicomUID.SubjectiveRefractionMeasurementsStorage.UID, DicomUID.SubjectiveRefractionMeasurementsStorage);
            _uids.Add(DicomUID.VisualAcuityMeasurementsStorage.UID, DicomUID.VisualAcuityMeasurementsStorage);
            _uids.Add(DicomUID.SpectaclePrescriptionReportStorage.UID, DicomUID.SpectaclePrescriptionReportStorage);
            _uids.Add(DicomUID.OphthalmicAxialMeasurementsStorage.UID, DicomUID.OphthalmicAxialMeasurementsStorage);
            _uids.Add(DicomUID.IntraocularLensCalculationsStorage.UID, DicomUID.IntraocularLensCalculationsStorage);
            _uids.Add(DicomUID.MacularGridThicknessAndVolumeReportStorage.UID, DicomUID.MacularGridThicknessAndVolumeReportStorage);
            _uids.Add(DicomUID.OphthalmicVisualFieldStaticPerimetryMeasurementsStorage.UID, DicomUID.OphthalmicVisualFieldStaticPerimetryMeasurementsStorage);
            _uids.Add(DicomUID.OphthalmicThicknessMapStorage.UID, DicomUID.OphthalmicThicknessMapStorage);
            _uids.Add(DicomUID.CornealTopographyMapStorage.UID, DicomUID.CornealTopographyMapStorage);
            _uids.Add(DicomUID.TextSRStorageTrialRETIRED.UID, DicomUID.TextSRStorageTrialRETIRED);
            _uids.Add(DicomUID.AudioSRStorageTrialRETIRED.UID, DicomUID.AudioSRStorageTrialRETIRED);
            _uids.Add(DicomUID.DetailSRStorageTrialRETIRED.UID, DicomUID.DetailSRStorageTrialRETIRED);
            _uids.Add(DicomUID.ComprehensiveSRStorageTrialRETIRED.UID, DicomUID.ComprehensiveSRStorageTrialRETIRED);
            _uids.Add(DicomUID.BasicTextSRStorage.UID, DicomUID.BasicTextSRStorage);
            _uids.Add(DicomUID.EnhancedSRStorage.UID, DicomUID.EnhancedSRStorage);
            _uids.Add(DicomUID.ComprehensiveSRStorage.UID, DicomUID.ComprehensiveSRStorage);
            _uids.Add(DicomUID.Comprehensive3DSRStorage.UID, DicomUID.Comprehensive3DSRStorage);
            _uids.Add(DicomUID.ExtensibleSRStorage.UID, DicomUID.ExtensibleSRStorage);
            _uids.Add(DicomUID.ProcedureLogStorage.UID, DicomUID.ProcedureLogStorage);
            _uids.Add(DicomUID.MammographyCADSRStorage.UID, DicomUID.MammographyCADSRStorage);
            _uids.Add(DicomUID.KeyObjectSelectionDocumentStorage.UID, DicomUID.KeyObjectSelectionDocumentStorage);
            _uids.Add(DicomUID.ChestCADSRStorage.UID, DicomUID.ChestCADSRStorage);
            _uids.Add(DicomUID.XRayRadiationDoseSRStorage.UID, DicomUID.XRayRadiationDoseSRStorage);
            _uids.Add(DicomUID.RadiopharmaceuticalRadiationDoseSRStorage.UID, DicomUID.RadiopharmaceuticalRadiationDoseSRStorage);
            _uids.Add(DicomUID.ColonCADSRStorage.UID, DicomUID.ColonCADSRStorage);
            _uids.Add(DicomUID.ImplantationPlanSRStorage.UID, DicomUID.ImplantationPlanSRStorage);
            _uids.Add(DicomUID.AcquisitionContextSRStorage.UID, DicomUID.AcquisitionContextSRStorage);
            _uids.Add(DicomUID.SimplifiedAdultEchoSRStorage.UID, DicomUID.SimplifiedAdultEchoSRStorage);
            _uids.Add(DicomUID.PatientRadiationDoseSRStorage.UID, DicomUID.PatientRadiationDoseSRStorage);
            _uids.Add(DicomUID.PlannedImagingAgentAdministrationSRStorage.UID, DicomUID.PlannedImagingAgentAdministrationSRStorage);
            _uids.Add(DicomUID.PerformedImagingAgentAdministrationSRStorage.UID, DicomUID.PerformedImagingAgentAdministrationSRStorage);
            _uids.Add(DicomUID.ContentAssessmentResultsStorage.UID, DicomUID.ContentAssessmentResultsStorage);
            _uids.Add(DicomUID.EncapsulatedPDFStorage.UID, DicomUID.EncapsulatedPDFStorage);
            _uids.Add(DicomUID.EncapsulatedCDAStorage.UID, DicomUID.EncapsulatedCDAStorage);
            _uids.Add(DicomUID.EncapsulatedSTLStorage.UID, DicomUID.EncapsulatedSTLStorage);
            _uids.Add(DicomUID.PositronEmissionTomographyImageStorage.UID, DicomUID.PositronEmissionTomographyImageStorage);
            _uids.Add(DicomUID.LegacyConvertedEnhancedPETImageStorage.UID, DicomUID.LegacyConvertedEnhancedPETImageStorage);
            _uids.Add(DicomUID.StandalonePETCurveStorageRETIRED.UID, DicomUID.StandalonePETCurveStorageRETIRED);
            _uids.Add(DicomUID.EnhancedPETImageStorage.UID, DicomUID.EnhancedPETImageStorage);
            _uids.Add(DicomUID.BasicStructuredDisplayStorage.UID, DicomUID.BasicStructuredDisplayStorage);
            _uids.Add(DicomUID.CTDefinedProcedureProtocolStorage.UID, DicomUID.CTDefinedProcedureProtocolStorage);
            _uids.Add(DicomUID.CTPerformedProcedureProtocolStorage.UID, DicomUID.CTPerformedProcedureProtocolStorage);
            _uids.Add(DicomUID.ProtocolApprovalStorage.UID, DicomUID.ProtocolApprovalStorage);
            _uids.Add(DicomUID.ProtocolApprovalInformationModelFIND.UID, DicomUID.ProtocolApprovalInformationModelFIND);
            _uids.Add(DicomUID.ProtocolApprovalInformationModelMOVE.UID, DicomUID.ProtocolApprovalInformationModelMOVE);
            _uids.Add(DicomUID.ProtocolApprovalInformationModelGET.UID, DicomUID.ProtocolApprovalInformationModelGET);
            _uids.Add(DicomUID.RTImageStorage.UID, DicomUID.RTImageStorage);
            _uids.Add(DicomUID.RTDoseStorage.UID, DicomUID.RTDoseStorage);
            _uids.Add(DicomUID.RTStructureSetStorage.UID, DicomUID.RTStructureSetStorage);
            _uids.Add(DicomUID.RTBeamsTreatmentRecordStorage.UID, DicomUID.RTBeamsTreatmentRecordStorage);
            _uids.Add(DicomUID.RTPlanStorage.UID, DicomUID.RTPlanStorage);
            _uids.Add(DicomUID.RTBrachyTreatmentRecordStorage.UID, DicomUID.RTBrachyTreatmentRecordStorage);
            _uids.Add(DicomUID.RTTreatmentSummaryRecordStorage.UID, DicomUID.RTTreatmentSummaryRecordStorage);
            _uids.Add(DicomUID.RTIonPlanStorage.UID, DicomUID.RTIonPlanStorage);
            _uids.Add(DicomUID.RTIonBeamsTreatmentRecordStorage.UID, DicomUID.RTIonBeamsTreatmentRecordStorage);
            _uids.Add(DicomUID.RTPhysicianIntentStorage.UID, DicomUID.RTPhysicianIntentStorage);
            _uids.Add(DicomUID.RTSegmentAnnotationStorage.UID, DicomUID.RTSegmentAnnotationStorage);
            _uids.Add(DicomUID.RTRadiationSetStorage.UID, DicomUID.RTRadiationSetStorage);
            _uids.Add(DicomUID.CArmPhotonElectronRadiationStorage.UID, DicomUID.CArmPhotonElectronRadiationStorage);
            _uids.Add(DicomUID.DICOSCTImageStorage.UID, DicomUID.DICOSCTImageStorage);
            _uids.Add(DicomUID.DICOSDigitalXRayImageStorageForPresentation.UID, DicomUID.DICOSDigitalXRayImageStorageForPresentation);
            _uids.Add(DicomUID.DICOSDigitalXRayImageStorageForProcessing.UID, DicomUID.DICOSDigitalXRayImageStorageForProcessing);
            _uids.Add(DicomUID.DICOSThreatDetectionReportStorage.UID, DicomUID.DICOSThreatDetectionReportStorage);
            _uids.Add(DicomUID.DICOS2DAITStorage.UID, DicomUID.DICOS2DAITStorage);
            _uids.Add(DicomUID.DICOS3DAITStorage.UID, DicomUID.DICOS3DAITStorage);
            _uids.Add(DicomUID.DICOSQuadrupoleResonanceQRStorage.UID, DicomUID.DICOSQuadrupoleResonanceQRStorage);
            _uids.Add(DicomUID.EddyCurrentImageStorage.UID, DicomUID.EddyCurrentImageStorage);
            _uids.Add(DicomUID.EddyCurrentMultiFrameImageStorage.UID, DicomUID.EddyCurrentMultiFrameImageStorage);
            _uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelFIND.UID, DicomUID.PatientRootQueryRetrieveInformationModelFIND);
            _uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelMOVE.UID, DicomUID.PatientRootQueryRetrieveInformationModelMOVE);
            _uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelGET.UID, DicomUID.PatientRootQueryRetrieveInformationModelGET);
            _uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelFIND.UID, DicomUID.StudyRootQueryRetrieveInformationModelFIND);
            _uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelMOVE.UID, DicomUID.StudyRootQueryRetrieveInformationModelMOVE);
            _uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelGET.UID, DicomUID.StudyRootQueryRetrieveInformationModelGET);
            _uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelFINDRETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelFINDRETIRED);
            _uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelMOVERETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelMOVERETIRED);
            _uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelGETRETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelGETRETIRED);
            _uids.Add(DicomUID.CompositeInstanceRootRetrieveMOVE.UID, DicomUID.CompositeInstanceRootRetrieveMOVE);
            _uids.Add(DicomUID.CompositeInstanceRootRetrieveGET.UID, DicomUID.CompositeInstanceRootRetrieveGET);
            _uids.Add(DicomUID.CompositeInstanceRetrieveWithoutBulkDataGET.UID, DicomUID.CompositeInstanceRetrieveWithoutBulkDataGET);
            _uids.Add(DicomUID.DefinedProcedureProtocolInformationModelFIND.UID, DicomUID.DefinedProcedureProtocolInformationModelFIND);
            _uids.Add(DicomUID.DefinedProcedureProtocolInformationModelMOVE.UID, DicomUID.DefinedProcedureProtocolInformationModelMOVE);
            _uids.Add(DicomUID.DefinedProcedureProtocolInformationModelGET.UID, DicomUID.DefinedProcedureProtocolInformationModelGET);
            _uids.Add(DicomUID.ModalityWorklistInformationModelFIND.UID, DicomUID.ModalityWorklistInformationModelFIND);
            _uids.Add(DicomUID.GeneralPurposeWorklistManagementMetaSOPClassRETIRED.UID, DicomUID.GeneralPurposeWorklistManagementMetaSOPClassRETIRED);
            _uids.Add(DicomUID.GeneralPurposeWorklistInformationModelFINDRETIRED.UID, DicomUID.GeneralPurposeWorklistInformationModelFINDRETIRED);
            _uids.Add(DicomUID.GeneralPurposeScheduledProcedureStepSOPClassRETIRED.UID, DicomUID.GeneralPurposeScheduledProcedureStepSOPClassRETIRED);
            _uids.Add(DicomUID.GeneralPurposePerformedProcedureStepSOPClassRETIRED.UID, DicomUID.GeneralPurposePerformedProcedureStepSOPClassRETIRED);
            _uids.Add(DicomUID.InstanceAvailabilityNotificationSOPClass.UID, DicomUID.InstanceAvailabilityNotificationSOPClass);
            _uids.Add(DicomUID.RTBeamsDeliveryInstructionStorageTrialRETIRED.UID, DicomUID.RTBeamsDeliveryInstructionStorageTrialRETIRED);
            _uids.Add(DicomUID.RTConventionalMachineVerificationTrialRETIRED.UID, DicomUID.RTConventionalMachineVerificationTrialRETIRED);
            _uids.Add(DicomUID.RTIonMachineVerificationTrialRETIRED.UID, DicomUID.RTIonMachineVerificationTrialRETIRED);
            _uids.Add(DicomUID.UnifiedWorklistAndProcedureStepServiceClassTrialRETIRED.UID, DicomUID.UnifiedWorklistAndProcedureStepServiceClassTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepPushSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepPushSOPClassTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepWatchSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepWatchSOPClassTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepPullSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepPullSOPClassTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepEventSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepEventSOPClassTrialRETIRED);
            _uids.Add(DicomUID.UPSGlobalSubscriptionSOPInstance.UID, DicomUID.UPSGlobalSubscriptionSOPInstance);
            _uids.Add(DicomUID.UPSFilteredGlobalSubscriptionSOPInstance.UID, DicomUID.UPSFilteredGlobalSubscriptionSOPInstance);
            _uids.Add(DicomUID.UnifiedWorklistAndProcedureStepServiceClass.UID, DicomUID.UnifiedWorklistAndProcedureStepServiceClass);
            _uids.Add(DicomUID.UnifiedProcedureStepPushSOPClass.UID, DicomUID.UnifiedProcedureStepPushSOPClass);
            _uids.Add(DicomUID.UnifiedProcedureStepWatchSOPClass.UID, DicomUID.UnifiedProcedureStepWatchSOPClass);
            _uids.Add(DicomUID.UnifiedProcedureStepPullSOPClass.UID, DicomUID.UnifiedProcedureStepPullSOPClass);
            _uids.Add(DicomUID.UnifiedProcedureStepEventSOPClass.UID, DicomUID.UnifiedProcedureStepEventSOPClass);
            _uids.Add(DicomUID.RTBeamsDeliveryInstructionStorage.UID, DicomUID.RTBeamsDeliveryInstructionStorage);
            _uids.Add(DicomUID.RTConventionalMachineVerification.UID, DicomUID.RTConventionalMachineVerification);
            _uids.Add(DicomUID.RTIonMachineVerification.UID, DicomUID.RTIonMachineVerification);
            _uids.Add(DicomUID.RTBrachyApplicationSetupDeliveryInstructionStorage.UID, DicomUID.RTBrachyApplicationSetupDeliveryInstructionStorage);
            _uids.Add(DicomUID.GeneralRelevantPatientInformationQuery.UID, DicomUID.GeneralRelevantPatientInformationQuery);
            _uids.Add(DicomUID.BreastImagingRelevantPatientInformationQuery.UID, DicomUID.BreastImagingRelevantPatientInformationQuery);
            _uids.Add(DicomUID.CardiacRelevantPatientInformationQuery.UID, DicomUID.CardiacRelevantPatientInformationQuery);
            _uids.Add(DicomUID.HangingProtocolStorage.UID, DicomUID.HangingProtocolStorage);
            _uids.Add(DicomUID.HangingProtocolInformationModelFIND.UID, DicomUID.HangingProtocolInformationModelFIND);
            _uids.Add(DicomUID.HangingProtocolInformationModelMOVE.UID, DicomUID.HangingProtocolInformationModelMOVE);
            _uids.Add(DicomUID.HangingProtocolInformationModelGET.UID, DicomUID.HangingProtocolInformationModelGET);
            _uids.Add(DicomUID.ColorPaletteStorage.UID, DicomUID.ColorPaletteStorage);
            _uids.Add(DicomUID.ColorPaletteQueryRetrieveInformationModelFIND.UID, DicomUID.ColorPaletteQueryRetrieveInformationModelFIND);
            _uids.Add(DicomUID.ColorPaletteQueryRetrieveInformationModelMOVE.UID, DicomUID.ColorPaletteQueryRetrieveInformationModelMOVE);
            _uids.Add(DicomUID.ColorPaletteQueryRetrieveInformationModelGET.UID, DicomUID.ColorPaletteQueryRetrieveInformationModelGET);
            _uids.Add(DicomUID.ProductCharacteristicsQuerySOPClass.UID, DicomUID.ProductCharacteristicsQuerySOPClass);
            _uids.Add(DicomUID.SubstanceApprovalQuerySOPClass.UID, DicomUID.SubstanceApprovalQuerySOPClass);
            _uids.Add(DicomUID.GenericImplantTemplateStorage.UID, DicomUID.GenericImplantTemplateStorage);
            _uids.Add(DicomUID.GenericImplantTemplateInformationModelFIND.UID, DicomUID.GenericImplantTemplateInformationModelFIND);
            _uids.Add(DicomUID.GenericImplantTemplateInformationModelMOVE.UID, DicomUID.GenericImplantTemplateInformationModelMOVE);
            _uids.Add(DicomUID.GenericImplantTemplateInformationModelGET.UID, DicomUID.GenericImplantTemplateInformationModelGET);
            _uids.Add(DicomUID.ImplantAssemblyTemplateStorage.UID, DicomUID.ImplantAssemblyTemplateStorage);
            _uids.Add(DicomUID.ImplantAssemblyTemplateInformationModelFIND.UID, DicomUID.ImplantAssemblyTemplateInformationModelFIND);
            _uids.Add(DicomUID.ImplantAssemblyTemplateInformationModelMOVE.UID, DicomUID.ImplantAssemblyTemplateInformationModelMOVE);
            _uids.Add(DicomUID.ImplantAssemblyTemplateInformationModelGET.UID, DicomUID.ImplantAssemblyTemplateInformationModelGET);
            _uids.Add(DicomUID.ImplantTemplateGroupStorage.UID, DicomUID.ImplantTemplateGroupStorage);
            _uids.Add(DicomUID.ImplantTemplateGroupInformationModelFIND.UID, DicomUID.ImplantTemplateGroupInformationModelFIND);
            _uids.Add(DicomUID.ImplantTemplateGroupInformationModelMOVE.UID, DicomUID.ImplantTemplateGroupInformationModelMOVE);
            _uids.Add(DicomUID.ImplantTemplateGroupInformationModelGET.UID, DicomUID.ImplantTemplateGroupInformationModelGET);
            _uids.Add(DicomUID.NativeDICOMModel.UID, DicomUID.NativeDICOMModel);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModel.UID, DicomUID.AbstractMultiDimensionalImageModel);
            _uids.Add(DicomUID.DICOMContentMappingResource.UID, DicomUID.DICOMContentMappingResource);
            _uids.Add(DicomUID.VideoEndoscopicImageRealTimeCommunication.UID, DicomUID.VideoEndoscopicImageRealTimeCommunication);
            _uids.Add(DicomUID.VideoPhotographicImageRealTimeCommunication.UID, DicomUID.VideoPhotographicImageRealTimeCommunication);
            _uids.Add(DicomUID.AudioWaveformRealTimeCommunication.UID, DicomUID.AudioWaveformRealTimeCommunication);
            _uids.Add(DicomUID.RenditionSelectionDocumentRealTimeCommunication.UID, DicomUID.RenditionSelectionDocumentRealTimeCommunication);
            _uids.Add(DicomUID.dicomDeviceName.UID, DicomUID.dicomDeviceName);
            _uids.Add(DicomUID.dicomDescription.UID, DicomUID.dicomDescription);
            _uids.Add(DicomUID.dicomManufacturer.UID, DicomUID.dicomManufacturer);
            _uids.Add(DicomUID.dicomManufacturerModelName.UID, DicomUID.dicomManufacturerModelName);
            _uids.Add(DicomUID.dicomSoftwareVersion.UID, DicomUID.dicomSoftwareVersion);
            _uids.Add(DicomUID.dicomVendorData.UID, DicomUID.dicomVendorData);
            _uids.Add(DicomUID.dicomAETitle.UID, DicomUID.dicomAETitle);
            _uids.Add(DicomUID.dicomNetworkConnectionReference.UID, DicomUID.dicomNetworkConnectionReference);
            _uids.Add(DicomUID.dicomApplicationCluster.UID, DicomUID.dicomApplicationCluster);
            _uids.Add(DicomUID.dicomAssociationInitiator.UID, DicomUID.dicomAssociationInitiator);
            _uids.Add(DicomUID.dicomAssociationAcceptor.UID, DicomUID.dicomAssociationAcceptor);
            _uids.Add(DicomUID.dicomHostname.UID, DicomUID.dicomHostname);
            _uids.Add(DicomUID.dicomPort.UID, DicomUID.dicomPort);
            _uids.Add(DicomUID.dicomSOPClass.UID, DicomUID.dicomSOPClass);
            _uids.Add(DicomUID.dicomTransferRole.UID, DicomUID.dicomTransferRole);
            _uids.Add(DicomUID.dicomTransferSyntax.UID, DicomUID.dicomTransferSyntax);
            _uids.Add(DicomUID.dicomPrimaryDeviceType.UID, DicomUID.dicomPrimaryDeviceType);
            _uids.Add(DicomUID.dicomRelatedDeviceReference.UID, DicomUID.dicomRelatedDeviceReference);
            _uids.Add(DicomUID.dicomPreferredCalledAETitle.UID, DicomUID.dicomPreferredCalledAETitle);
            _uids.Add(DicomUID.dicomTLSCyphersuite.UID, DicomUID.dicomTLSCyphersuite);
            _uids.Add(DicomUID.dicomAuthorizedNodeCertificateReference.UID, DicomUID.dicomAuthorizedNodeCertificateReference);
            _uids.Add(DicomUID.dicomThisNodeCertificateReference.UID, DicomUID.dicomThisNodeCertificateReference);
            _uids.Add(DicomUID.dicomInstalled.UID, DicomUID.dicomInstalled);
            _uids.Add(DicomUID.dicomStationName.UID, DicomUID.dicomStationName);
            _uids.Add(DicomUID.dicomDeviceSerialNumber.UID, DicomUID.dicomDeviceSerialNumber);
            _uids.Add(DicomUID.dicomInstitutionName.UID, DicomUID.dicomInstitutionName);
            _uids.Add(DicomUID.dicomInstitutionAddress.UID, DicomUID.dicomInstitutionAddress);
            _uids.Add(DicomUID.dicomInstitutionDepartmentName.UID, DicomUID.dicomInstitutionDepartmentName);
            _uids.Add(DicomUID.dicomIssuerOfPatientID.UID, DicomUID.dicomIssuerOfPatientID);
            _uids.Add(DicomUID.dicomPreferredCallingAETitle.UID, DicomUID.dicomPreferredCallingAETitle);
            _uids.Add(DicomUID.dicomSupportedCharacterSet.UID, DicomUID.dicomSupportedCharacterSet);
            _uids.Add(DicomUID.dicomConfigurationRoot.UID, DicomUID.dicomConfigurationRoot);
            _uids.Add(DicomUID.dicomDevicesRoot.UID, DicomUID.dicomDevicesRoot);
            _uids.Add(DicomUID.dicomUniqueAETitlesRegistryRoot.UID, DicomUID.dicomUniqueAETitlesRegistryRoot);
            _uids.Add(DicomUID.dicomDevice.UID, DicomUID.dicomDevice);
            _uids.Add(DicomUID.dicomNetworkAE.UID, DicomUID.dicomNetworkAE);
            _uids.Add(DicomUID.dicomNetworkConnection.UID, DicomUID.dicomNetworkConnection);
            _uids.Add(DicomUID.dicomUniqueAETitle.UID, DicomUID.dicomUniqueAETitle);
            _uids.Add(DicomUID.dicomTransferCapability.UID, DicomUID.dicomTransferCapability);
            _uids.Add(DicomUID.UniversalCoordinatedTime.UID, DicomUID.UniversalCoordinatedTime);
            _uids.Add(DicomUID.AnatomicModifier2.UID, DicomUID.AnatomicModifier2);
            _uids.Add(DicomUID.AnatomicRegion4.UID, DicomUID.AnatomicRegion4);
            _uids.Add(DicomUID.TransducerApproach5.UID, DicomUID.TransducerApproach5);
            _uids.Add(DicomUID.TransducerOrientation6.UID, DicomUID.TransducerOrientation6);
            _uids.Add(DicomUID.UltrasoundBeamPath7.UID, DicomUID.UltrasoundBeamPath7);
            _uids.Add(DicomUID.AngiographicInterventionalDevices8.UID, DicomUID.AngiographicInterventionalDevices8);
            _uids.Add(DicomUID.ImageGuidedTherapeuticProcedures9.UID, DicomUID.ImageGuidedTherapeuticProcedures9);
            _uids.Add(DicomUID.InterventionalDrug10.UID, DicomUID.InterventionalDrug10);
            _uids.Add(DicomUID.RouteOfAdministration11.UID, DicomUID.RouteOfAdministration11);
            _uids.Add(DicomUID.RadiographicContrastAgent12.UID, DicomUID.RadiographicContrastAgent12);
            _uids.Add(DicomUID.RadiographicContrastAgentIngredient13.UID, DicomUID.RadiographicContrastAgentIngredient13);
            _uids.Add(DicomUID.IsotopesInRadiopharmaceuticals18.UID, DicomUID.IsotopesInRadiopharmaceuticals18);
            _uids.Add(DicomUID.PatientOrientation19.UID, DicomUID.PatientOrientation19);
            _uids.Add(DicomUID.PatientOrientationModifier20.UID, DicomUID.PatientOrientationModifier20);
            _uids.Add(DicomUID.PatientEquipmentRelationship21.UID, DicomUID.PatientEquipmentRelationship21);
            _uids.Add(DicomUID.CranioCaudadAngulation23.UID, DicomUID.CranioCaudadAngulation23);
            _uids.Add(DicomUID.Radiopharmaceuticals25.UID, DicomUID.Radiopharmaceuticals25);
            _uids.Add(DicomUID.NuclearMedicineProjections26.UID, DicomUID.NuclearMedicineProjections26);
            _uids.Add(DicomUID.AcquisitionModality29.UID, DicomUID.AcquisitionModality29);
            _uids.Add(DicomUID.DICOMDevices30.UID, DicomUID.DICOMDevices30);
            _uids.Add(DicomUID.AbstractPriors31.UID, DicomUID.AbstractPriors31);
            _uids.Add(DicomUID.NumericValueQualifier42.UID, DicomUID.NumericValueQualifier42);
            _uids.Add(DicomUID.UnitsOfMeasurement82.UID, DicomUID.UnitsOfMeasurement82);
            _uids.Add(DicomUID.UnitsForRealWorldValueMapping83.UID, DicomUID.UnitsForRealWorldValueMapping83);
            _uids.Add(DicomUID.LevelOfSignificance220.UID, DicomUID.LevelOfSignificance220);
            _uids.Add(DicomUID.MeasurementRangeConcepts221.UID, DicomUID.MeasurementRangeConcepts221);
            _uids.Add(DicomUID.NormalityCodes222.UID, DicomUID.NormalityCodes222);
            _uids.Add(DicomUID.NormalRangeValues223.UID, DicomUID.NormalRangeValues223);
            _uids.Add(DicomUID.SelectionMethod224.UID, DicomUID.SelectionMethod224);
            _uids.Add(DicomUID.MeasurementUncertaintyConcepts225.UID, DicomUID.MeasurementUncertaintyConcepts225);
            _uids.Add(DicomUID.PopulationStatisticalDescriptors226.UID, DicomUID.PopulationStatisticalDescriptors226);
            _uids.Add(DicomUID.SampleStatisticalDescriptors227.UID, DicomUID.SampleStatisticalDescriptors227);
            _uids.Add(DicomUID.EquationOrTable228.UID, DicomUID.EquationOrTable228);
            _uids.Add(DicomUID.YesNo230.UID, DicomUID.YesNo230);
            _uids.Add(DicomUID.PresentAbsent240.UID, DicomUID.PresentAbsent240);
            _uids.Add(DicomUID.NormalAbnormal242.UID, DicomUID.NormalAbnormal242);
            _uids.Add(DicomUID.Laterality244.UID, DicomUID.Laterality244);
            _uids.Add(DicomUID.PositiveNegative250.UID, DicomUID.PositiveNegative250);
            _uids.Add(DicomUID.SeverityOfComplication251.UID, DicomUID.SeverityOfComplication251);
            _uids.Add(DicomUID.ObserverType270.UID, DicomUID.ObserverType270);
            _uids.Add(DicomUID.ObservationSubjectClass271.UID, DicomUID.ObservationSubjectClass271);
            _uids.Add(DicomUID.AudioChannelSource3000.UID, DicomUID.AudioChannelSource3000);
            _uids.Add(DicomUID.ECGLeads3001.UID, DicomUID.ECGLeads3001);
            _uids.Add(DicomUID.HemodynamicWaveformSources3003.UID, DicomUID.HemodynamicWaveformSources3003);
            _uids.Add(DicomUID.CardiovascularAnatomicLocations3010.UID, DicomUID.CardiovascularAnatomicLocations3010);
            _uids.Add(DicomUID.ElectrophysiologyAnatomicLocations3011.UID, DicomUID.ElectrophysiologyAnatomicLocations3011);
            _uids.Add(DicomUID.CoronaryArterySegments3014.UID, DicomUID.CoronaryArterySegments3014);
            _uids.Add(DicomUID.CoronaryArteries3015.UID, DicomUID.CoronaryArteries3015);
            _uids.Add(DicomUID.CardiovascularAnatomicLocationModifiers3019.UID, DicomUID.CardiovascularAnatomicLocationModifiers3019);
            _uids.Add(DicomUID.CardiologyUnitsOfMeasurement3082RETIRED.UID, DicomUID.CardiologyUnitsOfMeasurement3082RETIRED);
            _uids.Add(DicomUID.TimeSynchronizationChannelTypes3090.UID, DicomUID.TimeSynchronizationChannelTypes3090);
            _uids.Add(DicomUID.CardiacProceduralStateValues3101.UID, DicomUID.CardiacProceduralStateValues3101);
            _uids.Add(DicomUID.ElectrophysiologyMeasurementFunctionsAndTechniques3240.UID, DicomUID.ElectrophysiologyMeasurementFunctionsAndTechniques3240);
            _uids.Add(DicomUID.HemodynamicMeasurementTechniques3241.UID, DicomUID.HemodynamicMeasurementTechniques3241);
            _uids.Add(DicomUID.CatheterizationProcedurePhase3250.UID, DicomUID.CatheterizationProcedurePhase3250);
            _uids.Add(DicomUID.ElectrophysiologyProcedurePhase3254.UID, DicomUID.ElectrophysiologyProcedurePhase3254);
            _uids.Add(DicomUID.StressProtocols3261.UID, DicomUID.StressProtocols3261);
            _uids.Add(DicomUID.ECGPatientStateValues3262.UID, DicomUID.ECGPatientStateValues3262);
            _uids.Add(DicomUID.ElectrodePlacementValues3263.UID, DicomUID.ElectrodePlacementValues3263);
            _uids.Add(DicomUID.XYZElectrodePlacementValues3264RETIRED.UID, DicomUID.XYZElectrodePlacementValues3264RETIRED);
            _uids.Add(DicomUID.HemodynamicPhysiologicalChallenges3271.UID, DicomUID.HemodynamicPhysiologicalChallenges3271);
            _uids.Add(DicomUID.ECGAnnotations3335.UID, DicomUID.ECGAnnotations3335);
            _uids.Add(DicomUID.HemodynamicAnnotations3337.UID, DicomUID.HemodynamicAnnotations3337);
            _uids.Add(DicomUID.ElectrophysiologyAnnotations3339.UID, DicomUID.ElectrophysiologyAnnotations3339);
            _uids.Add(DicomUID.ProcedureLogTitles3400.UID, DicomUID.ProcedureLogTitles3400);
            _uids.Add(DicomUID.TypesOfLogNotes3401.UID, DicomUID.TypesOfLogNotes3401);
            _uids.Add(DicomUID.PatientStatusAndEvents3402.UID, DicomUID.PatientStatusAndEvents3402);
            _uids.Add(DicomUID.PercutaneousEntry3403.UID, DicomUID.PercutaneousEntry3403);
            _uids.Add(DicomUID.StaffActions3404.UID, DicomUID.StaffActions3404);
            _uids.Add(DicomUID.ProcedureActionValues3405.UID, DicomUID.ProcedureActionValues3405);
            _uids.Add(DicomUID.NonCoronaryTranscatheterInterventions3406.UID, DicomUID.NonCoronaryTranscatheterInterventions3406);
            _uids.Add(DicomUID.PurposeOfReferenceToObject3407.UID, DicomUID.PurposeOfReferenceToObject3407);
            _uids.Add(DicomUID.ActionsWithConsumables3408.UID, DicomUID.ActionsWithConsumables3408);
            _uids.Add(DicomUID.AdministrationOfDrugsContrast3409.UID, DicomUID.AdministrationOfDrugsContrast3409);
            _uids.Add(DicomUID.NumericParametersOfDrugsContrast3410.UID, DicomUID.NumericParametersOfDrugsContrast3410);
            _uids.Add(DicomUID.IntracoronaryDevices3411.UID, DicomUID.IntracoronaryDevices3411);
            _uids.Add(DicomUID.InterventionActionsAndStatus3412.UID, DicomUID.InterventionActionsAndStatus3412);
            _uids.Add(DicomUID.AdverseOutcomes3413.UID, DicomUID.AdverseOutcomes3413);
            _uids.Add(DicomUID.ProcedureUrgency3414.UID, DicomUID.ProcedureUrgency3414);
            _uids.Add(DicomUID.CardiacRhythms3415.UID, DicomUID.CardiacRhythms3415);
            _uids.Add(DicomUID.RespirationRhythms3416.UID, DicomUID.RespirationRhythms3416);
            _uids.Add(DicomUID.LesionRisk3418.UID, DicomUID.LesionRisk3418);
            _uids.Add(DicomUID.FindingsTitles3419.UID, DicomUID.FindingsTitles3419);
            _uids.Add(DicomUID.ProcedureAction3421.UID, DicomUID.ProcedureAction3421);
            _uids.Add(DicomUID.DeviceUseActions3422.UID, DicomUID.DeviceUseActions3422);
            _uids.Add(DicomUID.NumericDeviceCharacteristics3423.UID, DicomUID.NumericDeviceCharacteristics3423);
            _uids.Add(DicomUID.InterventionParameters3425.UID, DicomUID.InterventionParameters3425);
            _uids.Add(DicomUID.ConsumablesParameters3426.UID, DicomUID.ConsumablesParameters3426);
            _uids.Add(DicomUID.EquipmentEvents3427.UID, DicomUID.EquipmentEvents3427);
            _uids.Add(DicomUID.ImagingProcedures3428.UID, DicomUID.ImagingProcedures3428);
            _uids.Add(DicomUID.CatheterizationDevices3429.UID, DicomUID.CatheterizationDevices3429);
            _uids.Add(DicomUID.DateTimeQualifiers3430.UID, DicomUID.DateTimeQualifiers3430);
            _uids.Add(DicomUID.PeripheralPulseLocations3440.UID, DicomUID.PeripheralPulseLocations3440);
            _uids.Add(DicomUID.PatientAssessments3441.UID, DicomUID.PatientAssessments3441);
            _uids.Add(DicomUID.PeripheralPulseMethods3442.UID, DicomUID.PeripheralPulseMethods3442);
            _uids.Add(DicomUID.SkinCondition3446.UID, DicomUID.SkinCondition3446);
            _uids.Add(DicomUID.AirwayAssessment3448.UID, DicomUID.AirwayAssessment3448);
            _uids.Add(DicomUID.CalibrationObjects3451.UID, DicomUID.CalibrationObjects3451);
            _uids.Add(DicomUID.CalibrationMethods3452.UID, DicomUID.CalibrationMethods3452);
            _uids.Add(DicomUID.CardiacVolumeMethods3453.UID, DicomUID.CardiacVolumeMethods3453);
            _uids.Add(DicomUID.IndexMethods3455.UID, DicomUID.IndexMethods3455);
            _uids.Add(DicomUID.SubSegmentMethods3456.UID, DicomUID.SubSegmentMethods3456);
            _uids.Add(DicomUID.ContourRealignment3458.UID, DicomUID.ContourRealignment3458);
            _uids.Add(DicomUID.CircumferentialExtent3460.UID, DicomUID.CircumferentialExtent3460);
            _uids.Add(DicomUID.RegionalExtent3461.UID, DicomUID.RegionalExtent3461);
            _uids.Add(DicomUID.ChamberIdentification3462.UID, DicomUID.ChamberIdentification3462);
            _uids.Add(DicomUID.QAReferenceMethods3465.UID, DicomUID.QAReferenceMethods3465);
            _uids.Add(DicomUID.PlaneIdentification3466.UID, DicomUID.PlaneIdentification3466);
            _uids.Add(DicomUID.EjectionFraction3467.UID, DicomUID.EjectionFraction3467);
            _uids.Add(DicomUID.EDVolume3468.UID, DicomUID.EDVolume3468);
            _uids.Add(DicomUID.ESVolume3469.UID, DicomUID.ESVolume3469);
            _uids.Add(DicomUID.VesselLumenCrossSectionalAreaCalculationMethods3470.UID, DicomUID.VesselLumenCrossSectionalAreaCalculationMethods3470);
            _uids.Add(DicomUID.EstimatedVolumes3471.UID, DicomUID.EstimatedVolumes3471);
            _uids.Add(DicomUID.CardiacContractionPhase3472.UID, DicomUID.CardiacContractionPhase3472);
            _uids.Add(DicomUID.IVUSProcedurePhases3480.UID, DicomUID.IVUSProcedurePhases3480);
            _uids.Add(DicomUID.IVUSDistanceMeasurements3481.UID, DicomUID.IVUSDistanceMeasurements3481);
            _uids.Add(DicomUID.IVUSAreaMeasurements3482.UID, DicomUID.IVUSAreaMeasurements3482);
            _uids.Add(DicomUID.IVUSLongitudinalMeasurements3483.UID, DicomUID.IVUSLongitudinalMeasurements3483);
            _uids.Add(DicomUID.IVUSIndicesAndRatios3484.UID, DicomUID.IVUSIndicesAndRatios3484);
            _uids.Add(DicomUID.IVUSVolumeMeasurements3485.UID, DicomUID.IVUSVolumeMeasurements3485);
            _uids.Add(DicomUID.VascularMeasurementSites3486.UID, DicomUID.VascularMeasurementSites3486);
            _uids.Add(DicomUID.IntravascularVolumetricRegions3487.UID, DicomUID.IntravascularVolumetricRegions3487);
            _uids.Add(DicomUID.MinMaxMean3488.UID, DicomUID.MinMaxMean3488);
            _uids.Add(DicomUID.CalciumDistribution3489.UID, DicomUID.CalciumDistribution3489);
            _uids.Add(DicomUID.IVUSLesionMorphologies3491.UID, DicomUID.IVUSLesionMorphologies3491);
            _uids.Add(DicomUID.VascularDissectionClassifications3492.UID, DicomUID.VascularDissectionClassifications3492);
            _uids.Add(DicomUID.IVUSRelativeStenosisSeverities3493.UID, DicomUID.IVUSRelativeStenosisSeverities3493);
            _uids.Add(DicomUID.IVUSNonMorphologicalFindings3494.UID, DicomUID.IVUSNonMorphologicalFindings3494);
            _uids.Add(DicomUID.IVUSPlaqueComposition3495.UID, DicomUID.IVUSPlaqueComposition3495);
            _uids.Add(DicomUID.IVUSFiducialPoints3496.UID, DicomUID.IVUSFiducialPoints3496);
            _uids.Add(DicomUID.IVUSArterialMorphology3497.UID, DicomUID.IVUSArterialMorphology3497);
            _uids.Add(DicomUID.PressureUnits3500.UID, DicomUID.PressureUnits3500);
            _uids.Add(DicomUID.HemodynamicResistanceUnits3502.UID, DicomUID.HemodynamicResistanceUnits3502);
            _uids.Add(DicomUID.IndexedHemodynamicResistanceUnits3503.UID, DicomUID.IndexedHemodynamicResistanceUnits3503);
            _uids.Add(DicomUID.CatheterSizeUnits3510.UID, DicomUID.CatheterSizeUnits3510);
            _uids.Add(DicomUID.SpecimenCollection3515.UID, DicomUID.SpecimenCollection3515);
            _uids.Add(DicomUID.BloodSourceType3520.UID, DicomUID.BloodSourceType3520);
            _uids.Add(DicomUID.BloodGasPressures3524.UID, DicomUID.BloodGasPressures3524);
            _uids.Add(DicomUID.BloodGasContent3525.UID, DicomUID.BloodGasContent3525);
            _uids.Add(DicomUID.BloodGasSaturation3526.UID, DicomUID.BloodGasSaturation3526);
            _uids.Add(DicomUID.BloodBaseExcess3527.UID, DicomUID.BloodBaseExcess3527);
            _uids.Add(DicomUID.BloodPH3528.UID, DicomUID.BloodPH3528);
            _uids.Add(DicomUID.ArterialVenousContent3529.UID, DicomUID.ArterialVenousContent3529);
            _uids.Add(DicomUID.OxygenAdministrationActions3530.UID, DicomUID.OxygenAdministrationActions3530);
            _uids.Add(DicomUID.OxygenAdministration3531.UID, DicomUID.OxygenAdministration3531);
            _uids.Add(DicomUID.CirculatorySupportActions3550.UID, DicomUID.CirculatorySupportActions3550);
            _uids.Add(DicomUID.VentilationActions3551.UID, DicomUID.VentilationActions3551);
            _uids.Add(DicomUID.PacingActions3552.UID, DicomUID.PacingActions3552);
            _uids.Add(DicomUID.CirculatorySupport3553.UID, DicomUID.CirculatorySupport3553);
            _uids.Add(DicomUID.Ventilation3554.UID, DicomUID.Ventilation3554);
            _uids.Add(DicomUID.Pacing3555.UID, DicomUID.Pacing3555);
            _uids.Add(DicomUID.BloodPressureMethods3560.UID, DicomUID.BloodPressureMethods3560);
            _uids.Add(DicomUID.RelativeTimes3600.UID, DicomUID.RelativeTimes3600);
            _uids.Add(DicomUID.HemodynamicPatientState3602.UID, DicomUID.HemodynamicPatientState3602);
            _uids.Add(DicomUID.ArterialLesionLocations3604.UID, DicomUID.ArterialLesionLocations3604);
            _uids.Add(DicomUID.ArterialSourceLocations3606.UID, DicomUID.ArterialSourceLocations3606);
            _uids.Add(DicomUID.VenousSourceLocations3607.UID, DicomUID.VenousSourceLocations3607);
            _uids.Add(DicomUID.AtrialSourceLocations3608.UID, DicomUID.AtrialSourceLocations3608);
            _uids.Add(DicomUID.VentricularSourceLocations3609.UID, DicomUID.VentricularSourceLocations3609);
            _uids.Add(DicomUID.GradientSourceLocations3610.UID, DicomUID.GradientSourceLocations3610);
            _uids.Add(DicomUID.PressureMeasurements3611.UID, DicomUID.PressureMeasurements3611);
            _uids.Add(DicomUID.BloodVelocityMeasurements3612.UID, DicomUID.BloodVelocityMeasurements3612);
            _uids.Add(DicomUID.HemodynamicTimeMeasurements3613.UID, DicomUID.HemodynamicTimeMeasurements3613);
            _uids.Add(DicomUID.ValveAreasNonMitral3614.UID, DicomUID.ValveAreasNonMitral3614);
            _uids.Add(DicomUID.ValveAreas3615.UID, DicomUID.ValveAreas3615);
            _uids.Add(DicomUID.HemodynamicPeriodMeasurements3616.UID, DicomUID.HemodynamicPeriodMeasurements3616);
            _uids.Add(DicomUID.ValveFlows3617.UID, DicomUID.ValveFlows3617);
            _uids.Add(DicomUID.HemodynamicFlows3618.UID, DicomUID.HemodynamicFlows3618);
            _uids.Add(DicomUID.HemodynamicResistanceMeasurements3619.UID, DicomUID.HemodynamicResistanceMeasurements3619);
            _uids.Add(DicomUID.HemodynamicRatios3620.UID, DicomUID.HemodynamicRatios3620);
            _uids.Add(DicomUID.FractionalFlowReserve3621.UID, DicomUID.FractionalFlowReserve3621);
            _uids.Add(DicomUID.MeasurementType3627.UID, DicomUID.MeasurementType3627);
            _uids.Add(DicomUID.CardiacOutputMethods3628.UID, DicomUID.CardiacOutputMethods3628);
            _uids.Add(DicomUID.ProcedureIntent3629.UID, DicomUID.ProcedureIntent3629);
            _uids.Add(DicomUID.CardiovascularAnatomicLocations3630.UID, DicomUID.CardiovascularAnatomicLocations3630);
            _uids.Add(DicomUID.Hypertension3640.UID, DicomUID.Hypertension3640);
            _uids.Add(DicomUID.HemodynamicAssessments3641.UID, DicomUID.HemodynamicAssessments3641);
            _uids.Add(DicomUID.DegreeFindings3642.UID, DicomUID.DegreeFindings3642);
            _uids.Add(DicomUID.HemodynamicMeasurementPhase3651.UID, DicomUID.HemodynamicMeasurementPhase3651);
            _uids.Add(DicomUID.BodySurfaceAreaEquations3663.UID, DicomUID.BodySurfaceAreaEquations3663);
            _uids.Add(DicomUID.OxygenConsumptionEquationsAndTables3664.UID, DicomUID.OxygenConsumptionEquationsAndTables3664);
            _uids.Add(DicomUID.P50Equations3666.UID, DicomUID.P50Equations3666);
            _uids.Add(DicomUID.FraminghamScores3667.UID, DicomUID.FraminghamScores3667);
            _uids.Add(DicomUID.FraminghamTables3668.UID, DicomUID.FraminghamTables3668);
            _uids.Add(DicomUID.ECGProcedureTypes3670.UID, DicomUID.ECGProcedureTypes3670);
            _uids.Add(DicomUID.ReasonForECGExam3671.UID, DicomUID.ReasonForECGExam3671);
            _uids.Add(DicomUID.Pacemakers3672.UID, DicomUID.Pacemakers3672);
            _uids.Add(DicomUID.Diagnosis3673RETIRED.UID, DicomUID.Diagnosis3673RETIRED);
            _uids.Add(DicomUID.OtherFilters3675RETIRED.UID, DicomUID.OtherFilters3675RETIRED);
            _uids.Add(DicomUID.LeadMeasurementTechnique3676.UID, DicomUID.LeadMeasurementTechnique3676);
            _uids.Add(DicomUID.SummaryCodesECG3677.UID, DicomUID.SummaryCodesECG3677);
            _uids.Add(DicomUID.QTCorrectionAlgorithms3678.UID, DicomUID.QTCorrectionAlgorithms3678);
            _uids.Add(DicomUID.ECGMorphologyDescriptions3679RETIRED.UID, DicomUID.ECGMorphologyDescriptions3679RETIRED);
            _uids.Add(DicomUID.ECGLeadNoiseDescriptions3680.UID, DicomUID.ECGLeadNoiseDescriptions3680);
            _uids.Add(DicomUID.ECGLeadNoiseModifiers3681RETIRED.UID, DicomUID.ECGLeadNoiseModifiers3681RETIRED);
            _uids.Add(DicomUID.Probability3682RETIRED.UID, DicomUID.Probability3682RETIRED);
            _uids.Add(DicomUID.Modifiers3683RETIRED.UID, DicomUID.Modifiers3683RETIRED);
            _uids.Add(DicomUID.Trend3684RETIRED.UID, DicomUID.Trend3684RETIRED);
            _uids.Add(DicomUID.ConjunctiveTerms3685RETIRED.UID, DicomUID.ConjunctiveTerms3685RETIRED);
            _uids.Add(DicomUID.ECGInterpretiveStatements3686RETIRED.UID, DicomUID.ECGInterpretiveStatements3686RETIRED);
            _uids.Add(DicomUID.ElectrophysiologyWaveformDurations3687.UID, DicomUID.ElectrophysiologyWaveformDurations3687);
            _uids.Add(DicomUID.ElectrophysiologyWaveformVoltages3688.UID, DicomUID.ElectrophysiologyWaveformVoltages3688);
            _uids.Add(DicomUID.CathDiagnosis3700.UID, DicomUID.CathDiagnosis3700);
            _uids.Add(DicomUID.CardiacValvesAndTracts3701.UID, DicomUID.CardiacValvesAndTracts3701);
            _uids.Add(DicomUID.WallMotion3703.UID, DicomUID.WallMotion3703);
            _uids.Add(DicomUID.MyocardiumWallMorphologyFindings3704.UID, DicomUID.MyocardiumWallMorphologyFindings3704);
            _uids.Add(DicomUID.ChamberSize3705.UID, DicomUID.ChamberSize3705);
            _uids.Add(DicomUID.OverallContractility3706.UID, DicomUID.OverallContractility3706);
            _uids.Add(DicomUID.VSDDescription3707.UID, DicomUID.VSDDescription3707);
            _uids.Add(DicomUID.AorticRootDescription3709.UID, DicomUID.AorticRootDescription3709);
            _uids.Add(DicomUID.CoronaryDominance3710.UID, DicomUID.CoronaryDominance3710);
            _uids.Add(DicomUID.ValvularAbnormalities3711.UID, DicomUID.ValvularAbnormalities3711);
            _uids.Add(DicomUID.VesselDescriptors3712.UID, DicomUID.VesselDescriptors3712);
            _uids.Add(DicomUID.TIMIFlowCharacteristics3713.UID, DicomUID.TIMIFlowCharacteristics3713);
            _uids.Add(DicomUID.Thrombus3714.UID, DicomUID.Thrombus3714);
            _uids.Add(DicomUID.LesionMargin3715.UID, DicomUID.LesionMargin3715);
            _uids.Add(DicomUID.Severity3716.UID, DicomUID.Severity3716);
            _uids.Add(DicomUID.MyocardialWallSegments3717.UID, DicomUID.MyocardialWallSegments3717);
            _uids.Add(DicomUID.MyocardialWallSegmentsInProjection3718.UID, DicomUID.MyocardialWallSegmentsInProjection3718);
            _uids.Add(DicomUID.CanadianClinicalClassification3719.UID, DicomUID.CanadianClinicalClassification3719);
            _uids.Add(DicomUID.CardiacHistoryDates3720RETIRED.UID, DicomUID.CardiacHistoryDates3720RETIRED);
            _uids.Add(DicomUID.CardiovascularSurgeries3721.UID, DicomUID.CardiovascularSurgeries3721);
            _uids.Add(DicomUID.DiabeticTherapy3722.UID, DicomUID.DiabeticTherapy3722);
            _uids.Add(DicomUID.MITypes3723.UID, DicomUID.MITypes3723);
            _uids.Add(DicomUID.SmokingHistory3724.UID, DicomUID.SmokingHistory3724);
            _uids.Add(DicomUID.IndicationsForCoronaryIntervention3726.UID, DicomUID.IndicationsForCoronaryIntervention3726);
            _uids.Add(DicomUID.IndicationsForCatheterization3727.UID, DicomUID.IndicationsForCatheterization3727);
            _uids.Add(DicomUID.CathFindings3728.UID, DicomUID.CathFindings3728);
            _uids.Add(DicomUID.AdmissionStatus3729.UID, DicomUID.AdmissionStatus3729);
            _uids.Add(DicomUID.InsurancePayor3730.UID, DicomUID.InsurancePayor3730);
            _uids.Add(DicomUID.PrimaryCauseOfDeath3733.UID, DicomUID.PrimaryCauseOfDeath3733);
            _uids.Add(DicomUID.AcuteCoronarySyndromeTimePeriod3735.UID, DicomUID.AcuteCoronarySyndromeTimePeriod3735);
            _uids.Add(DicomUID.NYHAClassification3736.UID, DicomUID.NYHAClassification3736);
            _uids.Add(DicomUID.NonInvasiveTestIschemia3737.UID, DicomUID.NonInvasiveTestIschemia3737);
            _uids.Add(DicomUID.PreCathAnginaType3738.UID, DicomUID.PreCathAnginaType3738);
            _uids.Add(DicomUID.CathProcedureType3739.UID, DicomUID.CathProcedureType3739);
            _uids.Add(DicomUID.ThrombolyticAdministration3740.UID, DicomUID.ThrombolyticAdministration3740);
            _uids.Add(DicomUID.MedicationAdministrationLabVisit3741.UID, DicomUID.MedicationAdministrationLabVisit3741);
            _uids.Add(DicomUID.MedicationAdministrationPCI3742.UID, DicomUID.MedicationAdministrationPCI3742);
            _uids.Add(DicomUID.ClopidogrelTiclopidineAdministration3743.UID, DicomUID.ClopidogrelTiclopidineAdministration3743);
            _uids.Add(DicomUID.EFTestingMethod3744.UID, DicomUID.EFTestingMethod3744);
            _uids.Add(DicomUID.CalculationMethod3745.UID, DicomUID.CalculationMethod3745);
            _uids.Add(DicomUID.PercutaneousEntrySite3746.UID, DicomUID.PercutaneousEntrySite3746);
            _uids.Add(DicomUID.PercutaneousClosure3747.UID, DicomUID.PercutaneousClosure3747);
            _uids.Add(DicomUID.AngiographicEFTestingMethod3748.UID, DicomUID.AngiographicEFTestingMethod3748);
            _uids.Add(DicomUID.PCIProcedureResult3749.UID, DicomUID.PCIProcedureResult3749);
            _uids.Add(DicomUID.PreviouslyDilatedLesion3750.UID, DicomUID.PreviouslyDilatedLesion3750);
            _uids.Add(DicomUID.GuidewireCrossing3752.UID, DicomUID.GuidewireCrossing3752);
            _uids.Add(DicomUID.VascularComplications3754.UID, DicomUID.VascularComplications3754);
            _uids.Add(DicomUID.CathComplications3755.UID, DicomUID.CathComplications3755);
            _uids.Add(DicomUID.CardiacPatientRiskFactors3756.UID, DicomUID.CardiacPatientRiskFactors3756);
            _uids.Add(DicomUID.CardiacDiagnosticProcedures3757.UID, DicomUID.CardiacDiagnosticProcedures3757);
            _uids.Add(DicomUID.CardiovascularFamilyHistory3758.UID, DicomUID.CardiovascularFamilyHistory3758);
            _uids.Add(DicomUID.HypertensionTherapy3760.UID, DicomUID.HypertensionTherapy3760);
            _uids.Add(DicomUID.AntilipemicAgents3761.UID, DicomUID.AntilipemicAgents3761);
            _uids.Add(DicomUID.AntiarrhythmicAgents3762.UID, DicomUID.AntiarrhythmicAgents3762);
            _uids.Add(DicomUID.MyocardialInfarctionTherapies3764.UID, DicomUID.MyocardialInfarctionTherapies3764);
            _uids.Add(DicomUID.ConcernTypes3769.UID, DicomUID.ConcernTypes3769);
            _uids.Add(DicomUID.ProblemStatus3770.UID, DicomUID.ProblemStatus3770);
            _uids.Add(DicomUID.HealthStatus3772.UID, DicomUID.HealthStatus3772);
            _uids.Add(DicomUID.UseStatus3773.UID, DicomUID.UseStatus3773);
            _uids.Add(DicomUID.SocialHistory3774.UID, DicomUID.SocialHistory3774);
            _uids.Add(DicomUID.ImplantedDevices3777.UID, DicomUID.ImplantedDevices3777);
            _uids.Add(DicomUID.PlaqueStructures3802.UID, DicomUID.PlaqueStructures3802);
            _uids.Add(DicomUID.StenosisMeasurementMethods3804.UID, DicomUID.StenosisMeasurementMethods3804);
            _uids.Add(DicomUID.StenosisTypes3805.UID, DicomUID.StenosisTypes3805);
            _uids.Add(DicomUID.StenosisShape3806.UID, DicomUID.StenosisShape3806);
            _uids.Add(DicomUID.VolumeMeasurementMethods3807.UID, DicomUID.VolumeMeasurementMethods3807);
            _uids.Add(DicomUID.AneurysmTypes3808.UID, DicomUID.AneurysmTypes3808);
            _uids.Add(DicomUID.AssociatedConditions3809.UID, DicomUID.AssociatedConditions3809);
            _uids.Add(DicomUID.VascularMorphology3810.UID, DicomUID.VascularMorphology3810);
            _uids.Add(DicomUID.StentFindings3813.UID, DicomUID.StentFindings3813);
            _uids.Add(DicomUID.StentComposition3814.UID, DicomUID.StentComposition3814);
            _uids.Add(DicomUID.SourceOfVascularFinding3815.UID, DicomUID.SourceOfVascularFinding3815);
            _uids.Add(DicomUID.VascularSclerosisTypes3817.UID, DicomUID.VascularSclerosisTypes3817);
            _uids.Add(DicomUID.NonInvasiveVascularProcedures3820.UID, DicomUID.NonInvasiveVascularProcedures3820);
            _uids.Add(DicomUID.PapillaryMuscleIncludedExcluded3821.UID, DicomUID.PapillaryMuscleIncludedExcluded3821);
            _uids.Add(DicomUID.RespiratoryStatus3823.UID, DicomUID.RespiratoryStatus3823);
            _uids.Add(DicomUID.HeartRhythm3826.UID, DicomUID.HeartRhythm3826);
            _uids.Add(DicomUID.VesselSegments3827.UID, DicomUID.VesselSegments3827);
            _uids.Add(DicomUID.PulmonaryArteries3829.UID, DicomUID.PulmonaryArteries3829);
            _uids.Add(DicomUID.StenosisLength3831.UID, DicomUID.StenosisLength3831);
            _uids.Add(DicomUID.StenosisGrade3832.UID, DicomUID.StenosisGrade3832);
            _uids.Add(DicomUID.CardiacEjectionFraction3833.UID, DicomUID.CardiacEjectionFraction3833);
            _uids.Add(DicomUID.CardiacVolumeMeasurements3835.UID, DicomUID.CardiacVolumeMeasurements3835);
            _uids.Add(DicomUID.TimeBasedPerfusionMeasurements3836.UID, DicomUID.TimeBasedPerfusionMeasurements3836);
            _uids.Add(DicomUID.FiducialFeature3837.UID, DicomUID.FiducialFeature3837);
            _uids.Add(DicomUID.DiameterDerivation3838.UID, DicomUID.DiameterDerivation3838);
            _uids.Add(DicomUID.CoronaryVeins3839.UID, DicomUID.CoronaryVeins3839);
            _uids.Add(DicomUID.PulmonaryVeins3840.UID, DicomUID.PulmonaryVeins3840);
            _uids.Add(DicomUID.MyocardialSubsegment3843.UID, DicomUID.MyocardialSubsegment3843);
            _uids.Add(DicomUID.PartialViewSectionForMammography4005.UID, DicomUID.PartialViewSectionForMammography4005);
            _uids.Add(DicomUID.DXAnatomyImaged4009.UID, DicomUID.DXAnatomyImaged4009);
            _uids.Add(DicomUID.DXView4010.UID, DicomUID.DXView4010);
            _uids.Add(DicomUID.DXViewModifier4011.UID, DicomUID.DXViewModifier4011);
            _uids.Add(DicomUID.ProjectionEponymousName4012.UID, DicomUID.ProjectionEponymousName4012);
            _uids.Add(DicomUID.AnatomicRegionForMammography4013.UID, DicomUID.AnatomicRegionForMammography4013);
            _uids.Add(DicomUID.ViewForMammography4014.UID, DicomUID.ViewForMammography4014);
            _uids.Add(DicomUID.ViewModifierForMammography4015.UID, DicomUID.ViewModifierForMammography4015);
            _uids.Add(DicomUID.AnatomicRegionForIntraOralRadiography4016.UID, DicomUID.AnatomicRegionForIntraOralRadiography4016);
            _uids.Add(DicomUID.AnatomicRegionModifierForIntraOralRadiography4017.UID, DicomUID.AnatomicRegionModifierForIntraOralRadiography4017);
            _uids.Add(DicomUID.PrimaryAnatomicStructureForIntraOralRadiographyPermanentDentitionDesignationOfTeeth4018.UID, DicomUID.PrimaryAnatomicStructureForIntraOralRadiographyPermanentDentitionDesignationOfTeeth4018);
            _uids.Add(DicomUID.PrimaryAnatomicStructureForIntraOralRadiographyDeciduousDentitionDesignationOfTeeth4019.UID, DicomUID.PrimaryAnatomicStructureForIntraOralRadiographyDeciduousDentitionDesignationOfTeeth4019);
            _uids.Add(DicomUID.PETRadionuclide4020.UID, DicomUID.PETRadionuclide4020);
            _uids.Add(DicomUID.PETRadiopharmaceutical4021.UID, DicomUID.PETRadiopharmaceutical4021);
            _uids.Add(DicomUID.CraniofacialAnatomicRegions4028.UID, DicomUID.CraniofacialAnatomicRegions4028);
            _uids.Add(DicomUID.CTMRAndPETAnatomyImaged4030.UID, DicomUID.CTMRAndPETAnatomyImaged4030);
            _uids.Add(DicomUID.CommonAnatomicRegions4031.UID, DicomUID.CommonAnatomicRegions4031);
            _uids.Add(DicomUID.MRSpectroscopyMetabolites4032.UID, DicomUID.MRSpectroscopyMetabolites4032);
            _uids.Add(DicomUID.MRProtonSpectroscopyMetabolites4033.UID, DicomUID.MRProtonSpectroscopyMetabolites4033);
            _uids.Add(DicomUID.EndoscopyAnatomicRegions4040.UID, DicomUID.EndoscopyAnatomicRegions4040);
            _uids.Add(DicomUID.XAXRFAnatomyImaged4042.UID, DicomUID.XAXRFAnatomyImaged4042);
            _uids.Add(DicomUID.DrugOrContrastAgentCharacteristics4050.UID, DicomUID.DrugOrContrastAgentCharacteristics4050);
            _uids.Add(DicomUID.GeneralDevices4051.UID, DicomUID.GeneralDevices4051);
            _uids.Add(DicomUID.PhantomDevices4052.UID, DicomUID.PhantomDevices4052);
            _uids.Add(DicomUID.OphthalmicImagingAgent4200.UID, DicomUID.OphthalmicImagingAgent4200);
            _uids.Add(DicomUID.PatientEyeMovementCommand4201.UID, DicomUID.PatientEyeMovementCommand4201);
            _uids.Add(DicomUID.OphthalmicPhotographyAcquisitionDevice4202.UID, DicomUID.OphthalmicPhotographyAcquisitionDevice4202);
            _uids.Add(DicomUID.OphthalmicPhotographyIllumination4203.UID, DicomUID.OphthalmicPhotographyIllumination4203);
            _uids.Add(DicomUID.OphthalmicFilter4204.UID, DicomUID.OphthalmicFilter4204);
            _uids.Add(DicomUID.OphthalmicLens4205.UID, DicomUID.OphthalmicLens4205);
            _uids.Add(DicomUID.OphthalmicChannelDescription4206.UID, DicomUID.OphthalmicChannelDescription4206);
            _uids.Add(DicomUID.OphthalmicImagePosition4207.UID, DicomUID.OphthalmicImagePosition4207);
            _uids.Add(DicomUID.MydriaticAgent4208.UID, DicomUID.MydriaticAgent4208);
            _uids.Add(DicomUID.OphthalmicAnatomicStructureImaged4209.UID, DicomUID.OphthalmicAnatomicStructureImaged4209);
            _uids.Add(DicomUID.OphthalmicTomographyAcquisitionDevice4210.UID, DicomUID.OphthalmicTomographyAcquisitionDevice4210);
            _uids.Add(DicomUID.OphthalmicOCTAnatomicStructureImaged4211.UID, DicomUID.OphthalmicOCTAnatomicStructureImaged4211);
            _uids.Add(DicomUID.Languages5000.UID, DicomUID.Languages5000);
            _uids.Add(DicomUID.Countries5001.UID, DicomUID.Countries5001);
            _uids.Add(DicomUID.OverallBreastComposition6000.UID, DicomUID.OverallBreastComposition6000);
            _uids.Add(DicomUID.OverallBreastCompositionFromBIRADS6001.UID, DicomUID.OverallBreastCompositionFromBIRADS6001);
            _uids.Add(DicomUID.ChangeSinceLastMammogramOrPriorSurgery6002.UID, DicomUID.ChangeSinceLastMammogramOrPriorSurgery6002);
            _uids.Add(DicomUID.ChangeSinceLastMammogramOrPriorSurgeryFromBIRADS6003.UID, DicomUID.ChangeSinceLastMammogramOrPriorSurgeryFromBIRADS6003);
            _uids.Add(DicomUID.MammographyCharacteristicsOfShape6004.UID, DicomUID.MammographyCharacteristicsOfShape6004);
            _uids.Add(DicomUID.CharacteristicsOfShapeFromBIRADS6005.UID, DicomUID.CharacteristicsOfShapeFromBIRADS6005);
            _uids.Add(DicomUID.MammographyCharacteristicsOfMargin6006.UID, DicomUID.MammographyCharacteristicsOfMargin6006);
            _uids.Add(DicomUID.CharacteristicsOfMarginFromBIRADS6007.UID, DicomUID.CharacteristicsOfMarginFromBIRADS6007);
            _uids.Add(DicomUID.DensityModifier6008.UID, DicomUID.DensityModifier6008);
            _uids.Add(DicomUID.DensityModifierFromBIRADS6009.UID, DicomUID.DensityModifierFromBIRADS6009);
            _uids.Add(DicomUID.MammographyCalcificationTypes6010.UID, DicomUID.MammographyCalcificationTypes6010);
            _uids.Add(DicomUID.CalcificationTypesFromBIRADS6011.UID, DicomUID.CalcificationTypesFromBIRADS6011);
            _uids.Add(DicomUID.CalcificationDistributionModifier6012.UID, DicomUID.CalcificationDistributionModifier6012);
            _uids.Add(DicomUID.CalcificationDistributionModifierFromBIRADS6013.UID, DicomUID.CalcificationDistributionModifierFromBIRADS6013);
            _uids.Add(DicomUID.MammographySingleImageFinding6014.UID, DicomUID.MammographySingleImageFinding6014);
            _uids.Add(DicomUID.SingleImageFindingFromBIRADS6015.UID, DicomUID.SingleImageFindingFromBIRADS6015);
            _uids.Add(DicomUID.MammographyCompositeFeature6016.UID, DicomUID.MammographyCompositeFeature6016);
            _uids.Add(DicomUID.CompositeFeatureFromBIRADS6017.UID, DicomUID.CompositeFeatureFromBIRADS6017);
            _uids.Add(DicomUID.ClockfaceLocationOrRegion6018.UID, DicomUID.ClockfaceLocationOrRegion6018);
            _uids.Add(DicomUID.ClockfaceLocationOrRegionFromBIRADS6019.UID, DicomUID.ClockfaceLocationOrRegionFromBIRADS6019);
            _uids.Add(DicomUID.QuadrantLocation6020.UID, DicomUID.QuadrantLocation6020);
            _uids.Add(DicomUID.QuadrantLocationFromBIRADS6021.UID, DicomUID.QuadrantLocationFromBIRADS6021);
            _uids.Add(DicomUID.Side6022.UID, DicomUID.Side6022);
            _uids.Add(DicomUID.SideFromBIRADS6023.UID, DicomUID.SideFromBIRADS6023);
            _uids.Add(DicomUID.Depth6024.UID, DicomUID.Depth6024);
            _uids.Add(DicomUID.DepthFromBIRADS6025.UID, DicomUID.DepthFromBIRADS6025);
            _uids.Add(DicomUID.MammographyAssessment6026.UID, DicomUID.MammographyAssessment6026);
            _uids.Add(DicomUID.AssessmentFromBIRADS6027.UID, DicomUID.AssessmentFromBIRADS6027);
            _uids.Add(DicomUID.MammographyRecommendedFollowUp6028.UID, DicomUID.MammographyRecommendedFollowUp6028);
            _uids.Add(DicomUID.RecommendedFollowUpFromBIRADS6029.UID, DicomUID.RecommendedFollowUpFromBIRADS6029);
            _uids.Add(DicomUID.MammographyPathologyCodes6030.UID, DicomUID.MammographyPathologyCodes6030);
            _uids.Add(DicomUID.BenignPathologyCodesFromBIRADS6031.UID, DicomUID.BenignPathologyCodesFromBIRADS6031);
            _uids.Add(DicomUID.HighRiskLesionsPathologyCodesFromBIRADS6032.UID, DicomUID.HighRiskLesionsPathologyCodesFromBIRADS6032);
            _uids.Add(DicomUID.MalignantPathologyCodesFromBIRADS6033.UID, DicomUID.MalignantPathologyCodesFromBIRADS6033);
            _uids.Add(DicomUID.IntendedUseOfCADOutput6034.UID, DicomUID.IntendedUseOfCADOutput6034);
            _uids.Add(DicomUID.CompositeFeatureRelations6035.UID, DicomUID.CompositeFeatureRelations6035);
            _uids.Add(DicomUID.ScopeOfFeature6036.UID, DicomUID.ScopeOfFeature6036);
            _uids.Add(DicomUID.MammographyQuantitativeTemporalDifferenceType6037.UID, DicomUID.MammographyQuantitativeTemporalDifferenceType6037);
            _uids.Add(DicomUID.MammographyQualitativeTemporalDifferenceType6038.UID, DicomUID.MammographyQualitativeTemporalDifferenceType6038);
            _uids.Add(DicomUID.NippleCharacteristic6039.UID, DicomUID.NippleCharacteristic6039);
            _uids.Add(DicomUID.NonLesionObjectType6040.UID, DicomUID.NonLesionObjectType6040);
            _uids.Add(DicomUID.MammographyImageQualityFinding6041.UID, DicomUID.MammographyImageQualityFinding6041);
            _uids.Add(DicomUID.StatusOfResults6042.UID, DicomUID.StatusOfResults6042);
            _uids.Add(DicomUID.TypesOfMammographyCADAnalysis6043.UID, DicomUID.TypesOfMammographyCADAnalysis6043);
            _uids.Add(DicomUID.TypesOfImageQualityAssessment6044.UID, DicomUID.TypesOfImageQualityAssessment6044);
            _uids.Add(DicomUID.MammographyTypesOfQualityControlStandard6045.UID, DicomUID.MammographyTypesOfQualityControlStandard6045);
            _uids.Add(DicomUID.UnitsOfFollowUpInterval6046.UID, DicomUID.UnitsOfFollowUpInterval6046);
            _uids.Add(DicomUID.CADProcessingAndFindingsSummary6047.UID, DicomUID.CADProcessingAndFindingsSummary6047);
            _uids.Add(DicomUID.CADOperatingPointAxisLabel6048.UID, DicomUID.CADOperatingPointAxisLabel6048);
            _uids.Add(DicomUID.BreastProcedureReported6050.UID, DicomUID.BreastProcedureReported6050);
            _uids.Add(DicomUID.BreastProcedureReason6051.UID, DicomUID.BreastProcedureReason6051);
            _uids.Add(DicomUID.BreastImagingReportSectionTitle6052.UID, DicomUID.BreastImagingReportSectionTitle6052);
            _uids.Add(DicomUID.BreastImagingReportElements6053.UID, DicomUID.BreastImagingReportElements6053);
            _uids.Add(DicomUID.BreastImagingFindings6054.UID, DicomUID.BreastImagingFindings6054);
            _uids.Add(DicomUID.BreastClinicalFindingOrIndicatedProblem6055.UID, DicomUID.BreastClinicalFindingOrIndicatedProblem6055);
            _uids.Add(DicomUID.AssociatedFindingsForBreast6056.UID, DicomUID.AssociatedFindingsForBreast6056);
            _uids.Add(DicomUID.DuctographyFindingsForBreast6057.UID, DicomUID.DuctographyFindingsForBreast6057);
            _uids.Add(DicomUID.ProcedureModifiersForBreast6058.UID, DicomUID.ProcedureModifiersForBreast6058);
            _uids.Add(DicomUID.BreastImplantTypes6059.UID, DicomUID.BreastImplantTypes6059);
            _uids.Add(DicomUID.BreastBiopsyTechniques6060.UID, DicomUID.BreastBiopsyTechniques6060);
            _uids.Add(DicomUID.BreastImagingProcedureModifiers6061.UID, DicomUID.BreastImagingProcedureModifiers6061);
            _uids.Add(DicomUID.InterventionalProcedureComplications6062.UID, DicomUID.InterventionalProcedureComplications6062);
            _uids.Add(DicomUID.InterventionalProcedureResults6063.UID, DicomUID.InterventionalProcedureResults6063);
            _uids.Add(DicomUID.UltrasoundFindingsForBreast6064.UID, DicomUID.UltrasoundFindingsForBreast6064);
            _uids.Add(DicomUID.InstrumentApproach6065.UID, DicomUID.InstrumentApproach6065);
            _uids.Add(DicomUID.TargetConfirmation6066.UID, DicomUID.TargetConfirmation6066);
            _uids.Add(DicomUID.FluidColor6067.UID, DicomUID.FluidColor6067);
            _uids.Add(DicomUID.TumorStagesFromAJCC6068.UID, DicomUID.TumorStagesFromAJCC6068);
            _uids.Add(DicomUID.NottinghamCombinedHistologicGrade6069.UID, DicomUID.NottinghamCombinedHistologicGrade6069);
            _uids.Add(DicomUID.BloomRichardsonHistologicGrade6070.UID, DicomUID.BloomRichardsonHistologicGrade6070);
            _uids.Add(DicomUID.HistologicGradingMethod6071.UID, DicomUID.HistologicGradingMethod6071);
            _uids.Add(DicomUID.BreastImplantFindings6072.UID, DicomUID.BreastImplantFindings6072);
            _uids.Add(DicomUID.GynecologicalHormones6080.UID, DicomUID.GynecologicalHormones6080);
            _uids.Add(DicomUID.BreastCancerRiskFactors6081.UID, DicomUID.BreastCancerRiskFactors6081);
            _uids.Add(DicomUID.GynecologicalProcedures6082.UID, DicomUID.GynecologicalProcedures6082);
            _uids.Add(DicomUID.ProceduresForBreast6083.UID, DicomUID.ProceduresForBreast6083);
            _uids.Add(DicomUID.MammoplastyProcedures6084.UID, DicomUID.MammoplastyProcedures6084);
            _uids.Add(DicomUID.TherapiesForBreast6085.UID, DicomUID.TherapiesForBreast6085);
            _uids.Add(DicomUID.MenopausalPhase6086.UID, DicomUID.MenopausalPhase6086);
            _uids.Add(DicomUID.GeneralRiskFactors6087.UID, DicomUID.GeneralRiskFactors6087);
            _uids.Add(DicomUID.OBGYNMaternalRiskFactors6088.UID, DicomUID.OBGYNMaternalRiskFactors6088);
            _uids.Add(DicomUID.Substances6089.UID, DicomUID.Substances6089);
            _uids.Add(DicomUID.RelativeUsageExposureAmount6090.UID, DicomUID.RelativeUsageExposureAmount6090);
            _uids.Add(DicomUID.RelativeFrequencyOfEventValues6091.UID, DicomUID.RelativeFrequencyOfEventValues6091);
            _uids.Add(DicomUID.QuantitativeConceptsForUsageExposure6092.UID, DicomUID.QuantitativeConceptsForUsageExposure6092);
            _uids.Add(DicomUID.QualitativeConceptsForUsageExposureAmount6093.UID, DicomUID.QualitativeConceptsForUsageExposureAmount6093);
            _uids.Add(DicomUID.QualitativeConceptsForUsageExposureFrequency6094.UID, DicomUID.QualitativeConceptsForUsageExposureFrequency6094);
            _uids.Add(DicomUID.NumericPropertiesOfProcedures6095.UID, DicomUID.NumericPropertiesOfProcedures6095);
            _uids.Add(DicomUID.PregnancyStatus6096.UID, DicomUID.PregnancyStatus6096);
            _uids.Add(DicomUID.SideOfFamily6097.UID, DicomUID.SideOfFamily6097);
            _uids.Add(DicomUID.ChestComponentCategories6100.UID, DicomUID.ChestComponentCategories6100);
            _uids.Add(DicomUID.ChestFindingOrFeature6101.UID, DicomUID.ChestFindingOrFeature6101);
            _uids.Add(DicomUID.ChestFindingOrFeatureModifier6102.UID, DicomUID.ChestFindingOrFeatureModifier6102);
            _uids.Add(DicomUID.AbnormalLinesFindingOrFeature6103.UID, DicomUID.AbnormalLinesFindingOrFeature6103);
            _uids.Add(DicomUID.AbnormalOpacityFindingOrFeature6104.UID, DicomUID.AbnormalOpacityFindingOrFeature6104);
            _uids.Add(DicomUID.AbnormalLucencyFindingOrFeature6105.UID, DicomUID.AbnormalLucencyFindingOrFeature6105);
            _uids.Add(DicomUID.AbnormalTextureFindingOrFeature6106.UID, DicomUID.AbnormalTextureFindingOrFeature6106);
            _uids.Add(DicomUID.WidthDescriptor6107.UID, DicomUID.WidthDescriptor6107);
            _uids.Add(DicomUID.ChestAnatomicStructureAbnormalDistribution6108.UID, DicomUID.ChestAnatomicStructureAbnormalDistribution6108);
            _uids.Add(DicomUID.RadiographicAnatomyFindingOrFeature6109.UID, DicomUID.RadiographicAnatomyFindingOrFeature6109);
            _uids.Add(DicomUID.LungAnatomyFindingOrFeature6110.UID, DicomUID.LungAnatomyFindingOrFeature6110);
            _uids.Add(DicomUID.BronchovascularAnatomyFindingOrFeature6111.UID, DicomUID.BronchovascularAnatomyFindingOrFeature6111);
            _uids.Add(DicomUID.PleuraAnatomyFindingOrFeature6112.UID, DicomUID.PleuraAnatomyFindingOrFeature6112);
            _uids.Add(DicomUID.MediastinumAnatomyFindingOrFeature6113.UID, DicomUID.MediastinumAnatomyFindingOrFeature6113);
            _uids.Add(DicomUID.OsseousAnatomyFindingOrFeature6114.UID, DicomUID.OsseousAnatomyFindingOrFeature6114);
            _uids.Add(DicomUID.OsseousAnatomyModifiers6115.UID, DicomUID.OsseousAnatomyModifiers6115);
            _uids.Add(DicomUID.MuscularAnatomy6116.UID, DicomUID.MuscularAnatomy6116);
            _uids.Add(DicomUID.VascularAnatomy6117.UID, DicomUID.VascularAnatomy6117);
            _uids.Add(DicomUID.SizeDescriptor6118.UID, DicomUID.SizeDescriptor6118);
            _uids.Add(DicomUID.ChestBorderShape6119.UID, DicomUID.ChestBorderShape6119);
            _uids.Add(DicomUID.ChestBorderDefinition6120.UID, DicomUID.ChestBorderDefinition6120);
            _uids.Add(DicomUID.ChestOrientationDescriptor6121.UID, DicomUID.ChestOrientationDescriptor6121);
            _uids.Add(DicomUID.ChestContentDescriptor6122.UID, DicomUID.ChestContentDescriptor6122);
            _uids.Add(DicomUID.ChestOpacityDescriptor6123.UID, DicomUID.ChestOpacityDescriptor6123);
            _uids.Add(DicomUID.LocationInChest6124.UID, DicomUID.LocationInChest6124);
            _uids.Add(DicomUID.GeneralChestLocation6125.UID, DicomUID.GeneralChestLocation6125);
            _uids.Add(DicomUID.LocationInLung6126.UID, DicomUID.LocationInLung6126);
            _uids.Add(DicomUID.SegmentLocationInLung6127.UID, DicomUID.SegmentLocationInLung6127);
            _uids.Add(DicomUID.ChestDistributionDescriptor6128.UID, DicomUID.ChestDistributionDescriptor6128);
            _uids.Add(DicomUID.ChestSiteInvolvement6129.UID, DicomUID.ChestSiteInvolvement6129);
            _uids.Add(DicomUID.SeverityDescriptor6130.UID, DicomUID.SeverityDescriptor6130);
            _uids.Add(DicomUID.ChestTextureDescriptor6131.UID, DicomUID.ChestTextureDescriptor6131);
            _uids.Add(DicomUID.ChestCalcificationDescriptor6132.UID, DicomUID.ChestCalcificationDescriptor6132);
            _uids.Add(DicomUID.ChestQuantitativeTemporalDifferenceType6133.UID, DicomUID.ChestQuantitativeTemporalDifferenceType6133);
            _uids.Add(DicomUID.ChestQualitativeTemporalDifferenceType6134.UID, DicomUID.ChestQualitativeTemporalDifferenceType6134);
            _uids.Add(DicomUID.ImageQualityFinding6135.UID, DicomUID.ImageQualityFinding6135);
            _uids.Add(DicomUID.ChestTypesOfQualityControlStandard6136.UID, DicomUID.ChestTypesOfQualityControlStandard6136);
            _uids.Add(DicomUID.TypesOfCADAnalysis6137.UID, DicomUID.TypesOfCADAnalysis6137);
            _uids.Add(DicomUID.ChestNonLesionObjectType6138.UID, DicomUID.ChestNonLesionObjectType6138);
            _uids.Add(DicomUID.NonLesionModifiers6139.UID, DicomUID.NonLesionModifiers6139);
            _uids.Add(DicomUID.CalculationMethods6140.UID, DicomUID.CalculationMethods6140);
            _uids.Add(DicomUID.AttenuationCoefficientMeasurements6141.UID, DicomUID.AttenuationCoefficientMeasurements6141);
            _uids.Add(DicomUID.CalculatedValue6142.UID, DicomUID.CalculatedValue6142);
            _uids.Add(DicomUID.LesionResponse6143.UID, DicomUID.LesionResponse6143);
            _uids.Add(DicomUID.RECISTDefinedLesionResponse6144.UID, DicomUID.RECISTDefinedLesionResponse6144);
            _uids.Add(DicomUID.BaselineCategory6145.UID, DicomUID.BaselineCategory6145);
            _uids.Add(DicomUID.BackgroundEchotexture6151.UID, DicomUID.BackgroundEchotexture6151);
            _uids.Add(DicomUID.Orientation6152.UID, DicomUID.Orientation6152);
            _uids.Add(DicomUID.LesionBoundary6153.UID, DicomUID.LesionBoundary6153);
            _uids.Add(DicomUID.EchoPattern6154.UID, DicomUID.EchoPattern6154);
            _uids.Add(DicomUID.PosteriorAcousticFeatures6155.UID, DicomUID.PosteriorAcousticFeatures6155);
            _uids.Add(DicomUID.Vascularity6157.UID, DicomUID.Vascularity6157);
            _uids.Add(DicomUID.CorrelationToOtherFindings6158.UID, DicomUID.CorrelationToOtherFindings6158);
            _uids.Add(DicomUID.MalignancyType6159.UID, DicomUID.MalignancyType6159);
            _uids.Add(DicomUID.BreastPrimaryTumorAssessmentFromAJCC6160.UID, DicomUID.BreastPrimaryTumorAssessmentFromAJCC6160);
            _uids.Add(DicomUID.ClinicalRegionalLymphNodeAssessmentForBreast6161.UID, DicomUID.ClinicalRegionalLymphNodeAssessmentForBreast6161);
            _uids.Add(DicomUID.AssessmentOfMetastasisForBreast6162.UID, DicomUID.AssessmentOfMetastasisForBreast6162);
            _uids.Add(DicomUID.MenstrualCyclePhase6163.UID, DicomUID.MenstrualCyclePhase6163);
            _uids.Add(DicomUID.TimeIntervals6164.UID, DicomUID.TimeIntervals6164);
            _uids.Add(DicomUID.BreastLinearMeasurements6165.UID, DicomUID.BreastLinearMeasurements6165);
            _uids.Add(DicomUID.CADGeometrySecondaryGraphicalRepresentation6166.UID, DicomUID.CADGeometrySecondaryGraphicalRepresentation6166);
            _uids.Add(DicomUID.DiagnosticImagingReportDocumentTitles7000.UID, DicomUID.DiagnosticImagingReportDocumentTitles7000);
            _uids.Add(DicomUID.DiagnosticImagingReportHeadings7001.UID, DicomUID.DiagnosticImagingReportHeadings7001);
            _uids.Add(DicomUID.DiagnosticImagingReportElements7002.UID, DicomUID.DiagnosticImagingReportElements7002);
            _uids.Add(DicomUID.DiagnosticImagingReportPurposesOfReference7003.UID, DicomUID.DiagnosticImagingReportPurposesOfReference7003);
            _uids.Add(DicomUID.WaveformPurposesOfReference7004.UID, DicomUID.WaveformPurposesOfReference7004);
            _uids.Add(DicomUID.ContributingEquipmentPurposesOfReference7005.UID, DicomUID.ContributingEquipmentPurposesOfReference7005);
            _uids.Add(DicomUID.SRDocumentPurposesOfReference7006.UID, DicomUID.SRDocumentPurposesOfReference7006);
            _uids.Add(DicomUID.SignaturePurpose7007.UID, DicomUID.SignaturePurpose7007);
            _uids.Add(DicomUID.MediaImport7008.UID, DicomUID.MediaImport7008);
            _uids.Add(DicomUID.KeyObjectSelectionDocumentTitle7010.UID, DicomUID.KeyObjectSelectionDocumentTitle7010);
            _uids.Add(DicomUID.RejectedForQualityReasons7011.UID, DicomUID.RejectedForQualityReasons7011);
            _uids.Add(DicomUID.BestInSet7012.UID, DicomUID.BestInSet7012);
            _uids.Add(DicomUID.DocumentTitles7020.UID, DicomUID.DocumentTitles7020);
            _uids.Add(DicomUID.RCSRegistrationMethodType7100.UID, DicomUID.RCSRegistrationMethodType7100);
            _uids.Add(DicomUID.BrainAtlasFiducials7101.UID, DicomUID.BrainAtlasFiducials7101);
            _uids.Add(DicomUID.SegmentationPropertyCategories7150.UID, DicomUID.SegmentationPropertyCategories7150);
            _uids.Add(DicomUID.SegmentationPropertyTypes7151.UID, DicomUID.SegmentationPropertyTypes7151);
            _uids.Add(DicomUID.CardiacStructureSegmentationTypes7152.UID, DicomUID.CardiacStructureSegmentationTypes7152);
            _uids.Add(DicomUID.CNSSegmentationTypes7153.UID, DicomUID.CNSSegmentationTypes7153);
            _uids.Add(DicomUID.AbdominalSegmentationTypes7154.UID, DicomUID.AbdominalSegmentationTypes7154);
            _uids.Add(DicomUID.ThoracicSegmentationTypes7155.UID, DicomUID.ThoracicSegmentationTypes7155);
            _uids.Add(DicomUID.VascularSegmentationTypes7156.UID, DicomUID.VascularSegmentationTypes7156);
            _uids.Add(DicomUID.DeviceSegmentationTypes7157.UID, DicomUID.DeviceSegmentationTypes7157);
            _uids.Add(DicomUID.ArtifactSegmentationTypes7158.UID, DicomUID.ArtifactSegmentationTypes7158);
            _uids.Add(DicomUID.LesionSegmentationTypes7159.UID, DicomUID.LesionSegmentationTypes7159);
            _uids.Add(DicomUID.PelvicOrganSegmentationTypes7160.UID, DicomUID.PelvicOrganSegmentationTypes7160);
            _uids.Add(DicomUID.PhysiologySegmentationTypes7161.UID, DicomUID.PhysiologySegmentationTypes7161);
            _uids.Add(DicomUID.ReferencedImagePurposesOfReference7201.UID, DicomUID.ReferencedImagePurposesOfReference7201);
            _uids.Add(DicomUID.SourceImagePurposesOfReference7202.UID, DicomUID.SourceImagePurposesOfReference7202);
            _uids.Add(DicomUID.ImageDerivation7203.UID, DicomUID.ImageDerivation7203);
            _uids.Add(DicomUID.PurposeOfReferenceToAlternateRepresentation7205.UID, DicomUID.PurposeOfReferenceToAlternateRepresentation7205);
            _uids.Add(DicomUID.RelatedSeriesPurposesOfReference7210.UID, DicomUID.RelatedSeriesPurposesOfReference7210);
            _uids.Add(DicomUID.MultiFrameSubsetType7250.UID, DicomUID.MultiFrameSubsetType7250);
            _uids.Add(DicomUID.PersonRoles7450.UID, DicomUID.PersonRoles7450);
            _uids.Add(DicomUID.FamilyMember7451.UID, DicomUID.FamilyMember7451);
            _uids.Add(DicomUID.OrganizationalRoles7452.UID, DicomUID.OrganizationalRoles7452);
            _uids.Add(DicomUID.PerformingRoles7453.UID, DicomUID.PerformingRoles7453);
            _uids.Add(DicomUID.AnimalTaxonomicRankValues7454.UID, DicomUID.AnimalTaxonomicRankValues7454);
            _uids.Add(DicomUID.Sex7455.UID, DicomUID.Sex7455);
            _uids.Add(DicomUID.UnitsOfMeasureForAge7456.UID, DicomUID.UnitsOfMeasureForAge7456);
            _uids.Add(DicomUID.UnitsOfLinearMeasurement7460.UID, DicomUID.UnitsOfLinearMeasurement7460);
            _uids.Add(DicomUID.UnitsOfAreaMeasurement7461.UID, DicomUID.UnitsOfAreaMeasurement7461);
            _uids.Add(DicomUID.UnitsOfVolumeMeasurement7462.UID, DicomUID.UnitsOfVolumeMeasurement7462);
            _uids.Add(DicomUID.LinearMeasurements7470.UID, DicomUID.LinearMeasurements7470);
            _uids.Add(DicomUID.AreaMeasurements7471.UID, DicomUID.AreaMeasurements7471);
            _uids.Add(DicomUID.VolumeMeasurements7472.UID, DicomUID.VolumeMeasurements7472);
            _uids.Add(DicomUID.GeneralAreaCalculationMethods7473.UID, DicomUID.GeneralAreaCalculationMethods7473);
            _uids.Add(DicomUID.GeneralVolumeCalculationMethods7474.UID, DicomUID.GeneralVolumeCalculationMethods7474);
            _uids.Add(DicomUID.Breed7480.UID, DicomUID.Breed7480);
            _uids.Add(DicomUID.BreedRegistry7481.UID, DicomUID.BreedRegistry7481);
            _uids.Add(DicomUID.WorkitemDefinition9231.UID, DicomUID.WorkitemDefinition9231);
            _uids.Add(DicomUID.NonDICOMOutputTypes9232RETIRED.UID, DicomUID.NonDICOMOutputTypes9232RETIRED);
            _uids.Add(DicomUID.ProcedureDiscontinuationReasons9300.UID, DicomUID.ProcedureDiscontinuationReasons9300);
            _uids.Add(DicomUID.ScopeOfAccumulation10000.UID, DicomUID.ScopeOfAccumulation10000);
            _uids.Add(DicomUID.UIDTypes10001.UID, DicomUID.UIDTypes10001);
            _uids.Add(DicomUID.IrradiationEventTypes10002.UID, DicomUID.IrradiationEventTypes10002);
            _uids.Add(DicomUID.EquipmentPlaneIdentification10003.UID, DicomUID.EquipmentPlaneIdentification10003);
            _uids.Add(DicomUID.FluoroModes10004.UID, DicomUID.FluoroModes10004);
            _uids.Add(DicomUID.XRayFilterMaterials10006.UID, DicomUID.XRayFilterMaterials10006);
            _uids.Add(DicomUID.XRayFilterTypes10007.UID, DicomUID.XRayFilterTypes10007);
            _uids.Add(DicomUID.DoseRelatedDistanceMeasurements10008.UID, DicomUID.DoseRelatedDistanceMeasurements10008);
            _uids.Add(DicomUID.MeasuredCalculated10009.UID, DicomUID.MeasuredCalculated10009);
            _uids.Add(DicomUID.DoseMeasurementDevices10010.UID, DicomUID.DoseMeasurementDevices10010);
            _uids.Add(DicomUID.EffectiveDoseEvaluationMethod10011.UID, DicomUID.EffectiveDoseEvaluationMethod10011);
            _uids.Add(DicomUID.CTAcquisitionType10013.UID, DicomUID.CTAcquisitionType10013);
            _uids.Add(DicomUID.ContrastImagingTechnique10014.UID, DicomUID.ContrastImagingTechnique10014);
            _uids.Add(DicomUID.CTDoseReferenceAuthorities10015.UID, DicomUID.CTDoseReferenceAuthorities10015);
            _uids.Add(DicomUID.AnodeTargetMaterial10016.UID, DicomUID.AnodeTargetMaterial10016);
            _uids.Add(DicomUID.XRayGrid10017.UID, DicomUID.XRayGrid10017);
            _uids.Add(DicomUID.UltrasoundProtocolTypes12001.UID, DicomUID.UltrasoundProtocolTypes12001);
            _uids.Add(DicomUID.UltrasoundProtocolStageTypes12002.UID, DicomUID.UltrasoundProtocolStageTypes12002);
            _uids.Add(DicomUID.OBGYNDates12003.UID, DicomUID.OBGYNDates12003);
            _uids.Add(DicomUID.FetalBiometryRatios12004.UID, DicomUID.FetalBiometryRatios12004);
            _uids.Add(DicomUID.FetalBiometryMeasurements12005.UID, DicomUID.FetalBiometryMeasurements12005);
            _uids.Add(DicomUID.FetalLongBonesBiometryMeasurements12006.UID, DicomUID.FetalLongBonesBiometryMeasurements12006);
            _uids.Add(DicomUID.FetalCranium12007.UID, DicomUID.FetalCranium12007);
            _uids.Add(DicomUID.OBGYNAmnioticSac12008.UID, DicomUID.OBGYNAmnioticSac12008);
            _uids.Add(DicomUID.EarlyGestationBiometryMeasurements12009.UID, DicomUID.EarlyGestationBiometryMeasurements12009);
            _uids.Add(DicomUID.UltrasoundPelvisAndUterus12011.UID, DicomUID.UltrasoundPelvisAndUterus12011);
            _uids.Add(DicomUID.OBEquationsAndTables12012.UID, DicomUID.OBEquationsAndTables12012);
            _uids.Add(DicomUID.GestationalAgeEquationsAndTables12013.UID, DicomUID.GestationalAgeEquationsAndTables12013);
            _uids.Add(DicomUID.OBFetalBodyWeightEquationsAndTables12014.UID, DicomUID.OBFetalBodyWeightEquationsAndTables12014);
            _uids.Add(DicomUID.FetalGrowthEquationsAndTables12015.UID, DicomUID.FetalGrowthEquationsAndTables12015);
            _uids.Add(DicomUID.EstimatedFetalWeightPercentileEquationsAndTables12016.UID, DicomUID.EstimatedFetalWeightPercentileEquationsAndTables12016);
            _uids.Add(DicomUID.GrowthDistributionRank12017.UID, DicomUID.GrowthDistributionRank12017);
            _uids.Add(DicomUID.OBGYNSummary12018.UID, DicomUID.OBGYNSummary12018);
            _uids.Add(DicomUID.OBGYNFetusSummary12019.UID, DicomUID.OBGYNFetusSummary12019);
            _uids.Add(DicomUID.VascularSummary12101.UID, DicomUID.VascularSummary12101);
            _uids.Add(DicomUID.TemporalPeriodsRelatingToProcedureOrTherapy12102.UID, DicomUID.TemporalPeriodsRelatingToProcedureOrTherapy12102);
            _uids.Add(DicomUID.VascularUltrasoundAnatomicLocation12103.UID, DicomUID.VascularUltrasoundAnatomicLocation12103);
            _uids.Add(DicomUID.ExtracranialArteries12104.UID, DicomUID.ExtracranialArteries12104);
            _uids.Add(DicomUID.IntracranialCerebralVessels12105.UID, DicomUID.IntracranialCerebralVessels12105);
            _uids.Add(DicomUID.IntracranialCerebralVesselsUnilateral12106.UID, DicomUID.IntracranialCerebralVesselsUnilateral12106);
            _uids.Add(DicomUID.UpperExtremityArteries12107.UID, DicomUID.UpperExtremityArteries12107);
            _uids.Add(DicomUID.UpperExtremityVeins12108.UID, DicomUID.UpperExtremityVeins12108);
            _uids.Add(DicomUID.LowerExtremityArteries12109.UID, DicomUID.LowerExtremityArteries12109);
            _uids.Add(DicomUID.LowerExtremityVeins12110.UID, DicomUID.LowerExtremityVeins12110);
            _uids.Add(DicomUID.AbdominalArteriesLateral12111.UID, DicomUID.AbdominalArteriesLateral12111);
            _uids.Add(DicomUID.AbdominalArteriesUnilateral12112.UID, DicomUID.AbdominalArteriesUnilateral12112);
            _uids.Add(DicomUID.AbdominalVeinsLateral12113.UID, DicomUID.AbdominalVeinsLateral12113);
            _uids.Add(DicomUID.AbdominalVeinsUnilateral12114.UID, DicomUID.AbdominalVeinsUnilateral12114);
            _uids.Add(DicomUID.RenalVessels12115.UID, DicomUID.RenalVessels12115);
            _uids.Add(DicomUID.VesselSegmentModifiers12116.UID, DicomUID.VesselSegmentModifiers12116);
            _uids.Add(DicomUID.VesselBranchModifiers12117.UID, DicomUID.VesselBranchModifiers12117);
            _uids.Add(DicomUID.VascularUltrasoundProperty12119.UID, DicomUID.VascularUltrasoundProperty12119);
            _uids.Add(DicomUID.BloodVelocityMeasurementsByUltrasound12120.UID, DicomUID.BloodVelocityMeasurementsByUltrasound12120);
            _uids.Add(DicomUID.VascularIndicesAndRatios12121.UID, DicomUID.VascularIndicesAndRatios12121);
            _uids.Add(DicomUID.OtherVascularProperties12122.UID, DicomUID.OtherVascularProperties12122);
            _uids.Add(DicomUID.CarotidRatios12123.UID, DicomUID.CarotidRatios12123);
            _uids.Add(DicomUID.RenalRatios12124.UID, DicomUID.RenalRatios12124);
            _uids.Add(DicomUID.PelvicVasculatureAnatomicalLocation12140.UID, DicomUID.PelvicVasculatureAnatomicalLocation12140);
            _uids.Add(DicomUID.FetalVasculatureAnatomicalLocation12141.UID, DicomUID.FetalVasculatureAnatomicalLocation12141);
            _uids.Add(DicomUID.EchocardiographyLeftVentricle12200.UID, DicomUID.EchocardiographyLeftVentricle12200);
            _uids.Add(DicomUID.LeftVentricleLinear12201.UID, DicomUID.LeftVentricleLinear12201);
            _uids.Add(DicomUID.LeftVentricleVolume12202.UID, DicomUID.LeftVentricleVolume12202);
            _uids.Add(DicomUID.LeftVentricleOther12203.UID, DicomUID.LeftVentricleOther12203);
            _uids.Add(DicomUID.EchocardiographyRightVentricle12204.UID, DicomUID.EchocardiographyRightVentricle12204);
            _uids.Add(DicomUID.EchocardiographyLeftAtrium12205.UID, DicomUID.EchocardiographyLeftAtrium12205);
            _uids.Add(DicomUID.EchocardiographyRightAtrium12206.UID, DicomUID.EchocardiographyRightAtrium12206);
            _uids.Add(DicomUID.EchocardiographyMitralValve12207.UID, DicomUID.EchocardiographyMitralValve12207);
            _uids.Add(DicomUID.EchocardiographyTricuspidValve12208.UID, DicomUID.EchocardiographyTricuspidValve12208);
            _uids.Add(DicomUID.EchocardiographyPulmonicValve12209.UID, DicomUID.EchocardiographyPulmonicValve12209);
            _uids.Add(DicomUID.EchocardiographyPulmonaryArtery12210.UID, DicomUID.EchocardiographyPulmonaryArtery12210);
            _uids.Add(DicomUID.EchocardiographyAorticValve12211.UID, DicomUID.EchocardiographyAorticValve12211);
            _uids.Add(DicomUID.EchocardiographyAorta12212.UID, DicomUID.EchocardiographyAorta12212);
            _uids.Add(DicomUID.EchocardiographyPulmonaryVeins12214.UID, DicomUID.EchocardiographyPulmonaryVeins12214);
            _uids.Add(DicomUID.EchocardiographyVenaCavae12215.UID, DicomUID.EchocardiographyVenaCavae12215);
            _uids.Add(DicomUID.EchocardiographyHepaticVeins12216.UID, DicomUID.EchocardiographyHepaticVeins12216);
            _uids.Add(DicomUID.EchocardiographyCardiacShunt12217.UID, DicomUID.EchocardiographyCardiacShunt12217);
            _uids.Add(DicomUID.EchocardiographyCongenital12218.UID, DicomUID.EchocardiographyCongenital12218);
            _uids.Add(DicomUID.PulmonaryVeinModifiers12219.UID, DicomUID.PulmonaryVeinModifiers12219);
            _uids.Add(DicomUID.EchocardiographyCommonMeasurements12220.UID, DicomUID.EchocardiographyCommonMeasurements12220);
            _uids.Add(DicomUID.FlowDirection12221.UID, DicomUID.FlowDirection12221);
            _uids.Add(DicomUID.OrificeFlowProperties12222.UID, DicomUID.OrificeFlowProperties12222);
            _uids.Add(DicomUID.EchocardiographyStrokeVolumeOrigin12223.UID, DicomUID.EchocardiographyStrokeVolumeOrigin12223);
            _uids.Add(DicomUID.UltrasoundImageModes12224.UID, DicomUID.UltrasoundImageModes12224);
            _uids.Add(DicomUID.EchocardiographyImageView12226.UID, DicomUID.EchocardiographyImageView12226);
            _uids.Add(DicomUID.EchocardiographyMeasurementMethod12227.UID, DicomUID.EchocardiographyMeasurementMethod12227);
            _uids.Add(DicomUID.EchocardiographyVolumeMethods12228.UID, DicomUID.EchocardiographyVolumeMethods12228);
            _uids.Add(DicomUID.EchocardiographyAreaMethods12229.UID, DicomUID.EchocardiographyAreaMethods12229);
            _uids.Add(DicomUID.GradientMethods12230.UID, DicomUID.GradientMethods12230);
            _uids.Add(DicomUID.VolumeFlowMethods12231.UID, DicomUID.VolumeFlowMethods12231);
            _uids.Add(DicomUID.MyocardiumMassMethods12232.UID, DicomUID.MyocardiumMassMethods12232);
            _uids.Add(DicomUID.CardiacPhase12233.UID, DicomUID.CardiacPhase12233);
            _uids.Add(DicomUID.RespirationState12234.UID, DicomUID.RespirationState12234);
            _uids.Add(DicomUID.MitralValveAnatomicSites12235.UID, DicomUID.MitralValveAnatomicSites12235);
            _uids.Add(DicomUID.EchoAnatomicSites12236.UID, DicomUID.EchoAnatomicSites12236);
            _uids.Add(DicomUID.EchocardiographyAnatomicSiteModifiers12237.UID, DicomUID.EchocardiographyAnatomicSiteModifiers12237);
            _uids.Add(DicomUID.WallMotionScoringSchemes12238.UID, DicomUID.WallMotionScoringSchemes12238);
            _uids.Add(DicomUID.CardiacOutputProperties12239.UID, DicomUID.CardiacOutputProperties12239);
            _uids.Add(DicomUID.LeftVentricleArea12240.UID, DicomUID.LeftVentricleArea12240);
            _uids.Add(DicomUID.TricuspidValveFindingSites12241.UID, DicomUID.TricuspidValveFindingSites12241);
            _uids.Add(DicomUID.AorticValveFindingSites12242.UID, DicomUID.AorticValveFindingSites12242);
            _uids.Add(DicomUID.LeftVentricleFindingSites12243.UID, DicomUID.LeftVentricleFindingSites12243);
            _uids.Add(DicomUID.CongenitalFindingSites12244.UID, DicomUID.CongenitalFindingSites12244);
            _uids.Add(DicomUID.SurfaceProcessingAlgorithmFamilies7162.UID, DicomUID.SurfaceProcessingAlgorithmFamilies7162);
            _uids.Add(DicomUID.StressTestProcedurePhases3207.UID, DicomUID.StressTestProcedurePhases3207);
            _uids.Add(DicomUID.Stages3778.UID, DicomUID.Stages3778);
            _uids.Add(DicomUID.SMLSizeDescriptor252.UID, DicomUID.SMLSizeDescriptor252);
            _uids.Add(DicomUID.MajorCoronaryArteries3016.UID, DicomUID.MajorCoronaryArteries3016);
            _uids.Add(DicomUID.UnitsOfRadioactivity3083.UID, DicomUID.UnitsOfRadioactivity3083);
            _uids.Add(DicomUID.RestStress3102.UID, DicomUID.RestStress3102);
            _uids.Add(DicomUID.PETCardiologyProtocols3106.UID, DicomUID.PETCardiologyProtocols3106);
            _uids.Add(DicomUID.PETCardiologyRadiopharmaceuticals3107.UID, DicomUID.PETCardiologyRadiopharmaceuticals3107);
            _uids.Add(DicomUID.NMPETProcedures3108.UID, DicomUID.NMPETProcedures3108);
            _uids.Add(DicomUID.NuclearCardiologyProtocols3110.UID, DicomUID.NuclearCardiologyProtocols3110);
            _uids.Add(DicomUID.NuclearCardiologyRadiopharmaceuticals3111.UID, DicomUID.NuclearCardiologyRadiopharmaceuticals3111);
            _uids.Add(DicomUID.AttenuationCorrection3112.UID, DicomUID.AttenuationCorrection3112);
            _uids.Add(DicomUID.TypesOfPerfusionDefects3113.UID, DicomUID.TypesOfPerfusionDefects3113);
            _uids.Add(DicomUID.StudyQuality3114.UID, DicomUID.StudyQuality3114);
            _uids.Add(DicomUID.StressImagingQualityIssues3115.UID, DicomUID.StressImagingQualityIssues3115);
            _uids.Add(DicomUID.NMExtracardiacFindings3116.UID, DicomUID.NMExtracardiacFindings3116);
            _uids.Add(DicomUID.AttenuationCorrectionMethods3117.UID, DicomUID.AttenuationCorrectionMethods3117);
            _uids.Add(DicomUID.LevelOfRisk3118.UID, DicomUID.LevelOfRisk3118);
            _uids.Add(DicomUID.LVFunction3119.UID, DicomUID.LVFunction3119);
            _uids.Add(DicomUID.PerfusionFindings3120.UID, DicomUID.PerfusionFindings3120);
            _uids.Add(DicomUID.PerfusionMorphology3121.UID, DicomUID.PerfusionMorphology3121);
            _uids.Add(DicomUID.VentricularEnlargement3122.UID, DicomUID.VentricularEnlargement3122);
            _uids.Add(DicomUID.StressTestProcedure3200.UID, DicomUID.StressTestProcedure3200);
            _uids.Add(DicomUID.IndicationsForStressTest3201.UID, DicomUID.IndicationsForStressTest3201);
            _uids.Add(DicomUID.ChestPain3202.UID, DicomUID.ChestPain3202);
            _uids.Add(DicomUID.ExerciserDevice3203.UID, DicomUID.ExerciserDevice3203);
            _uids.Add(DicomUID.StressAgents3204.UID, DicomUID.StressAgents3204);
            _uids.Add(DicomUID.IndicationsForPharmacologicalStressTest3205.UID, DicomUID.IndicationsForPharmacologicalStressTest3205);
            _uids.Add(DicomUID.NonInvasiveCardiacImagingProcedures3206.UID, DicomUID.NonInvasiveCardiacImagingProcedures3206);
            _uids.Add(DicomUID.SummaryCodesExerciseECG3208.UID, DicomUID.SummaryCodesExerciseECG3208);
            _uids.Add(DicomUID.SummaryCodesStressImaging3209.UID, DicomUID.SummaryCodesStressImaging3209);
            _uids.Add(DicomUID.SpeedOfResponse3210.UID, DicomUID.SpeedOfResponse3210);
            _uids.Add(DicomUID.BPResponse3211.UID, DicomUID.BPResponse3211);
            _uids.Add(DicomUID.TreadmillSpeed3212.UID, DicomUID.TreadmillSpeed3212);
            _uids.Add(DicomUID.StressHemodynamicFindings3213.UID, DicomUID.StressHemodynamicFindings3213);
            _uids.Add(DicomUID.PerfusionFindingMethod3215.UID, DicomUID.PerfusionFindingMethod3215);
            _uids.Add(DicomUID.ComparisonFinding3217.UID, DicomUID.ComparisonFinding3217);
            _uids.Add(DicomUID.StressSymptoms3220.UID, DicomUID.StressSymptoms3220);
            _uids.Add(DicomUID.StressTestTerminationReasons3221.UID, DicomUID.StressTestTerminationReasons3221);
            _uids.Add(DicomUID.QTcMeasurements3227.UID, DicomUID.QTcMeasurements3227);
            _uids.Add(DicomUID.ECGTimingMeasurements3228.UID, DicomUID.ECGTimingMeasurements3228);
            _uids.Add(DicomUID.ECGAxisMeasurements3229.UID, DicomUID.ECGAxisMeasurements3229);
            _uids.Add(DicomUID.ECGFindings3230.UID, DicomUID.ECGFindings3230);
            _uids.Add(DicomUID.STSegmentFindings3231.UID, DicomUID.STSegmentFindings3231);
            _uids.Add(DicomUID.STSegmentLocation3232.UID, DicomUID.STSegmentLocation3232);
            _uids.Add(DicomUID.STSegmentMorphology3233.UID, DicomUID.STSegmentMorphology3233);
            _uids.Add(DicomUID.EctopicBeatMorphology3234.UID, DicomUID.EctopicBeatMorphology3234);
            _uids.Add(DicomUID.PerfusionComparisonFindings3235.UID, DicomUID.PerfusionComparisonFindings3235);
            _uids.Add(DicomUID.ToleranceComparisonFindings3236.UID, DicomUID.ToleranceComparisonFindings3236);
            _uids.Add(DicomUID.WallMotionComparisonFindings3237.UID, DicomUID.WallMotionComparisonFindings3237);
            _uids.Add(DicomUID.StressScoringScales3238.UID, DicomUID.StressScoringScales3238);
            _uids.Add(DicomUID.PerceivedExertionScales3239.UID, DicomUID.PerceivedExertionScales3239);
            _uids.Add(DicomUID.VentricleIdentification3463.UID, DicomUID.VentricleIdentification3463);
            _uids.Add(DicomUID.ColonOverallAssessment6200.UID, DicomUID.ColonOverallAssessment6200);
            _uids.Add(DicomUID.ColonFindingOrFeature6201.UID, DicomUID.ColonFindingOrFeature6201);
            _uids.Add(DicomUID.ColonFindingOrFeatureModifier6202.UID, DicomUID.ColonFindingOrFeatureModifier6202);
            _uids.Add(DicomUID.ColonNonLesionObjectType6203.UID, DicomUID.ColonNonLesionObjectType6203);
            _uids.Add(DicomUID.AnatomicNonColonFindings6204.UID, DicomUID.AnatomicNonColonFindings6204);
            _uids.Add(DicomUID.ClockfaceLocationForColon6205.UID, DicomUID.ClockfaceLocationForColon6205);
            _uids.Add(DicomUID.RecumbentPatientOrientationForColon6206.UID, DicomUID.RecumbentPatientOrientationForColon6206);
            _uids.Add(DicomUID.ColonQuantitativeTemporalDifferenceType6207.UID, DicomUID.ColonQuantitativeTemporalDifferenceType6207);
            _uids.Add(DicomUID.ColonTypesOfQualityControlStandard6208.UID, DicomUID.ColonTypesOfQualityControlStandard6208);
            _uids.Add(DicomUID.ColonMorphologyDescriptor6209.UID, DicomUID.ColonMorphologyDescriptor6209);
            _uids.Add(DicomUID.LocationInIntestinalTract6210.UID, DicomUID.LocationInIntestinalTract6210);
            _uids.Add(DicomUID.ColonCADMaterialDescription6211.UID, DicomUID.ColonCADMaterialDescription6211);
            _uids.Add(DicomUID.CalculatedValueForColonFindings6212.UID, DicomUID.CalculatedValueForColonFindings6212);
            _uids.Add(DicomUID.OphthalmicHorizontalDirections4214.UID, DicomUID.OphthalmicHorizontalDirections4214);
            _uids.Add(DicomUID.OphthalmicVerticalDirections4215.UID, DicomUID.OphthalmicVerticalDirections4215);
            _uids.Add(DicomUID.OphthalmicVisualAcuityType4216.UID, DicomUID.OphthalmicVisualAcuityType4216);
            _uids.Add(DicomUID.ArterialPulseWaveform3004.UID, DicomUID.ArterialPulseWaveform3004);
            _uids.Add(DicomUID.RespirationWaveform3005.UID, DicomUID.RespirationWaveform3005);
            _uids.Add(DicomUID.UltrasoundContrastBolusAgents12030.UID, DicomUID.UltrasoundContrastBolusAgents12030);
            _uids.Add(DicomUID.ProtocolIntervalEvents12031.UID, DicomUID.ProtocolIntervalEvents12031);
            _uids.Add(DicomUID.TransducerScanPattern12032.UID, DicomUID.TransducerScanPattern12032);
            _uids.Add(DicomUID.UltrasoundTransducerGeometry12033.UID, DicomUID.UltrasoundTransducerGeometry12033);
            _uids.Add(DicomUID.UltrasoundTransducerBeamSteering12034.UID, DicomUID.UltrasoundTransducerBeamSteering12034);
            _uids.Add(DicomUID.UltrasoundTransducerApplication12035.UID, DicomUID.UltrasoundTransducerApplication12035);
            _uids.Add(DicomUID.InstanceAvailabilityStatus50.UID, DicomUID.InstanceAvailabilityStatus50);
            _uids.Add(DicomUID.ModalityPPSDiscontinuationReasons9301.UID, DicomUID.ModalityPPSDiscontinuationReasons9301);
            _uids.Add(DicomUID.MediaImportPPSDiscontinuationReasons9302.UID, DicomUID.MediaImportPPSDiscontinuationReasons9302);
            _uids.Add(DicomUID.DXAnatomyImagedForAnimals7482.UID, DicomUID.DXAnatomyImagedForAnimals7482);
            _uids.Add(DicomUID.CommonAnatomicRegionsForAnimals7483.UID, DicomUID.CommonAnatomicRegionsForAnimals7483);
            _uids.Add(DicomUID.DXViewForAnimals7484.UID, DicomUID.DXViewForAnimals7484);
            _uids.Add(DicomUID.InstitutionalDepartmentsUnitsAndServices7030.UID, DicomUID.InstitutionalDepartmentsUnitsAndServices7030);
            _uids.Add(DicomUID.PurposeOfReferenceToPredecessorReport7009.UID, DicomUID.PurposeOfReferenceToPredecessorReport7009);
            _uids.Add(DicomUID.VisualFixationQualityDuringAcquisition4220.UID, DicomUID.VisualFixationQualityDuringAcquisition4220);
            _uids.Add(DicomUID.VisualFixationQualityProblem4221.UID, DicomUID.VisualFixationQualityProblem4221);
            _uids.Add(DicomUID.OphthalmicMacularGridProblem4222.UID, DicomUID.OphthalmicMacularGridProblem4222);
            _uids.Add(DicomUID.Organizations5002.UID, DicomUID.Organizations5002);
            _uids.Add(DicomUID.MixedBreeds7486.UID, DicomUID.MixedBreeds7486);
            _uids.Add(DicomUID.BroselowLutenPediatricSizeCategories7040.UID, DicomUID.BroselowLutenPediatricSizeCategories7040);
            _uids.Add(DicomUID.CMDCTECCCalciumScoringPatientSizeCategories7042.UID, DicomUID.CMDCTECCCalciumScoringPatientSizeCategories7042);
            _uids.Add(DicomUID.CardiacUltrasoundReportTitles12245.UID, DicomUID.CardiacUltrasoundReportTitles12245);
            _uids.Add(DicomUID.CardiacUltrasoundIndicationForStudy12246.UID, DicomUID.CardiacUltrasoundIndicationForStudy12246);
            _uids.Add(DicomUID.PediatricFetalAndCongenitalCardiacSurgicalInterventions12247.UID, DicomUID.PediatricFetalAndCongenitalCardiacSurgicalInterventions12247);
            _uids.Add(DicomUID.CardiacUltrasoundSummaryCodes12248.UID, DicomUID.CardiacUltrasoundSummaryCodes12248);
            _uids.Add(DicomUID.CardiacUltrasoundFetalSummaryCodes12249.UID, DicomUID.CardiacUltrasoundFetalSummaryCodes12249);
            _uids.Add(DicomUID.CardiacUltrasoundCommonLinearMeasurements12250.UID, DicomUID.CardiacUltrasoundCommonLinearMeasurements12250);
            _uids.Add(DicomUID.CardiacUltrasoundLinearValveMeasurements12251.UID, DicomUID.CardiacUltrasoundLinearValveMeasurements12251);
            _uids.Add(DicomUID.CardiacUltrasoundCardiacFunction12252.UID, DicomUID.CardiacUltrasoundCardiacFunction12252);
            _uids.Add(DicomUID.CardiacUltrasoundAreaMeasurements12253.UID, DicomUID.CardiacUltrasoundAreaMeasurements12253);
            _uids.Add(DicomUID.CardiacUltrasoundHemodynamicMeasurements12254.UID, DicomUID.CardiacUltrasoundHemodynamicMeasurements12254);
            _uids.Add(DicomUID.CardiacUltrasoundMyocardiumMeasurements12255.UID, DicomUID.CardiacUltrasoundMyocardiumMeasurements12255);
            _uids.Add(DicomUID.CardiacUltrasoundLeftVentricle12257.UID, DicomUID.CardiacUltrasoundLeftVentricle12257);
            _uids.Add(DicomUID.CardiacUltrasoundRightVentricle12258.UID, DicomUID.CardiacUltrasoundRightVentricle12258);
            _uids.Add(DicomUID.CardiacUltrasoundVentriclesMeasurements12259.UID, DicomUID.CardiacUltrasoundVentriclesMeasurements12259);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryArtery12260.UID, DicomUID.CardiacUltrasoundPulmonaryArtery12260);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryVein12261.UID, DicomUID.CardiacUltrasoundPulmonaryVein12261);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryValve12262.UID, DicomUID.CardiacUltrasoundPulmonaryValve12262);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnPulmonaryMeasurements12263.UID, DicomUID.CardiacUltrasoundVenousReturnPulmonaryMeasurements12263);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnSystemicMeasurements12264.UID, DicomUID.CardiacUltrasoundVenousReturnSystemicMeasurements12264);
            _uids.Add(DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumMeasurements12265.UID, DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumMeasurements12265);
            _uids.Add(DicomUID.CardiacUltrasoundMitralValve12266.UID, DicomUID.CardiacUltrasoundMitralValve12266);
            _uids.Add(DicomUID.CardiacUltrasoundTricuspidValve12267.UID, DicomUID.CardiacUltrasoundTricuspidValve12267);
            _uids.Add(DicomUID.CardiacUltrasoundAtrioventricularValvesMeasurements12268.UID, DicomUID.CardiacUltrasoundAtrioventricularValvesMeasurements12268);
            _uids.Add(DicomUID.CardiacUltrasoundInterventricularSeptumMeasurements12269.UID, DicomUID.CardiacUltrasoundInterventricularSeptumMeasurements12269);
            _uids.Add(DicomUID.CardiacUltrasoundAorticValve12270.UID, DicomUID.CardiacUltrasoundAorticValve12270);
            _uids.Add(DicomUID.CardiacUltrasoundOutflowTractsMeasurements12271.UID, DicomUID.CardiacUltrasoundOutflowTractsMeasurements12271);
            _uids.Add(DicomUID.CardiacUltrasoundSemilunarValvesAnnulateAndSinusesMeasurements12272.UID, DicomUID.CardiacUltrasoundSemilunarValvesAnnulateAndSinusesMeasurements12272);
            _uids.Add(DicomUID.CardiacUltrasoundAorticSinotubularJunction12273.UID, DicomUID.CardiacUltrasoundAorticSinotubularJunction12273);
            _uids.Add(DicomUID.CardiacUltrasoundAortaMeasurements12274.UID, DicomUID.CardiacUltrasoundAortaMeasurements12274);
            _uids.Add(DicomUID.CardiacUltrasoundCoronaryArteriesMeasurements12275.UID, DicomUID.CardiacUltrasoundCoronaryArteriesMeasurements12275);
            _uids.Add(DicomUID.CardiacUltrasoundAortoPulmonaryConnectionsMeasurements12276.UID, DicomUID.CardiacUltrasoundAortoPulmonaryConnectionsMeasurements12276);
            _uids.Add(DicomUID.CardiacUltrasoundPericardiumAndPleuraMeasurements12277.UID, DicomUID.CardiacUltrasoundPericardiumAndPleuraMeasurements12277);
            _uids.Add(DicomUID.CardiacUltrasoundFetalGeneralMeasurements12279.UID, DicomUID.CardiacUltrasoundFetalGeneralMeasurements12279);
            _uids.Add(DicomUID.CardiacUltrasoundTargetSites12280.UID, DicomUID.CardiacUltrasoundTargetSites12280);
            _uids.Add(DicomUID.CardiacUltrasoundTargetSiteModifiers12281.UID, DicomUID.CardiacUltrasoundTargetSiteModifiers12281);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnSystemicFindingSites12282.UID, DicomUID.CardiacUltrasoundVenousReturnSystemicFindingSites12282);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnPulmonaryFindingSites12283.UID, DicomUID.CardiacUltrasoundVenousReturnPulmonaryFindingSites12283);
            _uids.Add(DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumFindingSites12284.UID, DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumFindingSites12284);
            _uids.Add(DicomUID.CardiacUltrasoundAtrioventricularValvesFindingSites12285.UID, DicomUID.CardiacUltrasoundAtrioventricularValvesFindingSites12285);
            _uids.Add(DicomUID.CardiacUltrasoundInterventricularSeptumFindingSites12286.UID, DicomUID.CardiacUltrasoundInterventricularSeptumFindingSites12286);
            _uids.Add(DicomUID.CardiacUltrasoundVentriclesFindingSites12287.UID, DicomUID.CardiacUltrasoundVentriclesFindingSites12287);
            _uids.Add(DicomUID.CardiacUltrasoundOutflowTractsFindingSites12288.UID, DicomUID.CardiacUltrasoundOutflowTractsFindingSites12288);
            _uids.Add(DicomUID.CardiacUltrasoundSemilunarValvesAnnulusAndSinusesFindingSites12289.UID, DicomUID.CardiacUltrasoundSemilunarValvesAnnulusAndSinusesFindingSites12289);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryArteriesFindingSites12290.UID, DicomUID.CardiacUltrasoundPulmonaryArteriesFindingSites12290);
            _uids.Add(DicomUID.CardiacUltrasoundAortaFindingSites12291.UID, DicomUID.CardiacUltrasoundAortaFindingSites12291);
            _uids.Add(DicomUID.CardiacUltrasoundCoronaryArteriesFindingSites12292.UID, DicomUID.CardiacUltrasoundCoronaryArteriesFindingSites12292);
            _uids.Add(DicomUID.CardiacUltrasoundAortopulmonaryConnectionsFindingSites12293.UID, DicomUID.CardiacUltrasoundAortopulmonaryConnectionsFindingSites12293);
            _uids.Add(DicomUID.CardiacUltrasoundPericardiumAndPleuraFindingSites12294.UID, DicomUID.CardiacUltrasoundPericardiumAndPleuraFindingSites12294);
            _uids.Add(DicomUID.OphthalmicUltrasoundAxialMeasurementsType4230.UID, DicomUID.OphthalmicUltrasoundAxialMeasurementsType4230);
            _uids.Add(DicomUID.LensStatus4231.UID, DicomUID.LensStatus4231);
            _uids.Add(DicomUID.VitreousStatus4232.UID, DicomUID.VitreousStatus4232);
            _uids.Add(DicomUID.OphthalmicAxialLengthMeasurementsSegmentNames4233.UID, DicomUID.OphthalmicAxialLengthMeasurementsSegmentNames4233);
            _uids.Add(DicomUID.RefractiveSurgeryTypes4234.UID, DicomUID.RefractiveSurgeryTypes4234);
            _uids.Add(DicomUID.KeratometryDescriptors4235.UID, DicomUID.KeratometryDescriptors4235);
            _uids.Add(DicomUID.IOLCalculationFormula4236.UID, DicomUID.IOLCalculationFormula4236);
            _uids.Add(DicomUID.LensConstantType4237.UID, DicomUID.LensConstantType4237);
            _uids.Add(DicomUID.RefractiveErrorTypes4238.UID, DicomUID.RefractiveErrorTypes4238);
            _uids.Add(DicomUID.AnteriorChamberDepthDefinition4239.UID, DicomUID.AnteriorChamberDepthDefinition4239);
            _uids.Add(DicomUID.OphthalmicMeasurementOrCalculationDataSource4240.UID, DicomUID.OphthalmicMeasurementOrCalculationDataSource4240);
            _uids.Add(DicomUID.OphthalmicAxialLengthSelectionMethod4241.UID, DicomUID.OphthalmicAxialLengthSelectionMethod4241);
            _uids.Add(DicomUID.OphthalmicQualityMetricType4243.UID, DicomUID.OphthalmicQualityMetricType4243);
            _uids.Add(DicomUID.OphthalmicAgentConcentrationUnits4244.UID, DicomUID.OphthalmicAgentConcentrationUnits4244);
            _uids.Add(DicomUID.FunctionalConditionPresentDuringAcquisition91.UID, DicomUID.FunctionalConditionPresentDuringAcquisition91);
            _uids.Add(DicomUID.JointPositionDuringAcquisition92.UID, DicomUID.JointPositionDuringAcquisition92);
            _uids.Add(DicomUID.JointPositioningMethod93.UID, DicomUID.JointPositioningMethod93);
            _uids.Add(DicomUID.PhysicalForceAppliedDuringAcquisition94.UID, DicomUID.PhysicalForceAppliedDuringAcquisition94);
            _uids.Add(DicomUID.ECGControlVariablesNumeric3690.UID, DicomUID.ECGControlVariablesNumeric3690);
            _uids.Add(DicomUID.ECGControlVariablesText3691.UID, DicomUID.ECGControlVariablesText3691);
            _uids.Add(DicomUID.WSIReferencedImagePurposesOfReference8120.UID, DicomUID.WSIReferencedImagePurposesOfReference8120);
            _uids.Add(DicomUID.MicroscopyLensType8121.UID, DicomUID.MicroscopyLensType8121);
            _uids.Add(DicomUID.MicroscopyIlluminatorAndSensorColor8122.UID, DicomUID.MicroscopyIlluminatorAndSensorColor8122);
            _uids.Add(DicomUID.MicroscopyIlluminationMethod8123.UID, DicomUID.MicroscopyIlluminationMethod8123);
            _uids.Add(DicomUID.MicroscopyFilter8124.UID, DicomUID.MicroscopyFilter8124);
            _uids.Add(DicomUID.MicroscopyIlluminatorType8125.UID, DicomUID.MicroscopyIlluminatorType8125);
            _uids.Add(DicomUID.AuditEventID400.UID, DicomUID.AuditEventID400);
            _uids.Add(DicomUID.AuditEventTypeCode401.UID, DicomUID.AuditEventTypeCode401);
            _uids.Add(DicomUID.AuditActiveParticipantRoleIDCode402.UID, DicomUID.AuditActiveParticipantRoleIDCode402);
            _uids.Add(DicomUID.SecurityAlertTypeCode403.UID, DicomUID.SecurityAlertTypeCode403);
            _uids.Add(DicomUID.AuditParticipantObjectIDTypeCode404.UID, DicomUID.AuditParticipantObjectIDTypeCode404);
            _uids.Add(DicomUID.MediaTypeCode405.UID, DicomUID.MediaTypeCode405);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryTestPatterns4250.UID, DicomUID.VisualFieldStaticPerimetryTestPatterns4250);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryTestStrategies4251.UID, DicomUID.VisualFieldStaticPerimetryTestStrategies4251);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryScreeningTestModes4252.UID, DicomUID.VisualFieldStaticPerimetryScreeningTestModes4252);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryFixationStrategy4253.UID, DicomUID.VisualFieldStaticPerimetryFixationStrategy4253);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryTestAnalysisResults4254.UID, DicomUID.VisualFieldStaticPerimetryTestAnalysisResults4254);
            _uids.Add(DicomUID.VisualFieldIlluminationColor4255.UID, DicomUID.VisualFieldIlluminationColor4255);
            _uids.Add(DicomUID.VisualFieldProcedureModifier4256.UID, DicomUID.VisualFieldProcedureModifier4256);
            _uids.Add(DicomUID.VisualFieldGlobalIndexName4257.UID, DicomUID.VisualFieldGlobalIndexName4257);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelComponentSemantics7180.UID, DicomUID.AbstractMultiDimensionalImageModelComponentSemantics7180);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelComponentUnits7181.UID, DicomUID.AbstractMultiDimensionalImageModelComponentUnits7181);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelDimensionSemantics7182.UID, DicomUID.AbstractMultiDimensionalImageModelDimensionSemantics7182);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelDimensionUnits7183.UID, DicomUID.AbstractMultiDimensionalImageModelDimensionUnits7183);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelAxisDirection7184.UID, DicomUID.AbstractMultiDimensionalImageModelAxisDirection7184);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelAxisOrientation7185.UID, DicomUID.AbstractMultiDimensionalImageModelAxisOrientation7185);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelQualitativeDimensionSampleSemantics7186.UID, DicomUID.AbstractMultiDimensionalImageModelQualitativeDimensionSampleSemantics7186);
            _uids.Add(DicomUID.PlanningMethods7320.UID, DicomUID.PlanningMethods7320);
            _uids.Add(DicomUID.DeIdentificationMethod7050.UID, DicomUID.DeIdentificationMethod7050);
            _uids.Add(DicomUID.MeasurementOrientation12118.UID, DicomUID.MeasurementOrientation12118);
            _uids.Add(DicomUID.ECGGlobalWaveformDurations3689.UID, DicomUID.ECGGlobalWaveformDurations3689);
            _uids.Add(DicomUID.ICDs3692.UID, DicomUID.ICDs3692);
            _uids.Add(DicomUID.RadiotherapyGeneralWorkitemDefinition9241.UID, DicomUID.RadiotherapyGeneralWorkitemDefinition9241);
            _uids.Add(DicomUID.RadiotherapyAcquisitionWorkitemDefinition9242.UID, DicomUID.RadiotherapyAcquisitionWorkitemDefinition9242);
            _uids.Add(DicomUID.RadiotherapyRegistrationWorkitemDefinition9243.UID, DicomUID.RadiotherapyRegistrationWorkitemDefinition9243);
            _uids.Add(DicomUID.ContrastBolusSubstance3850.UID, DicomUID.ContrastBolusSubstance3850);
            _uids.Add(DicomUID.LabelTypes10022.UID, DicomUID.LabelTypes10022);
            _uids.Add(DicomUID.OphthalmicMappingUnitsForRealWorldValueMapping4260.UID, DicomUID.OphthalmicMappingUnitsForRealWorldValueMapping4260);
            _uids.Add(DicomUID.OphthalmicMappingAcquisitionMethod4261.UID, DicomUID.OphthalmicMappingAcquisitionMethod4261);
            _uids.Add(DicomUID.RetinalThicknessDefinition4262.UID, DicomUID.RetinalThicknessDefinition4262);
            _uids.Add(DicomUID.OphthalmicThicknessMapValueType4263.UID, DicomUID.OphthalmicThicknessMapValueType4263);
            _uids.Add(DicomUID.OphthalmicMapPurposesOfReference4264.UID, DicomUID.OphthalmicMapPurposesOfReference4264);
            _uids.Add(DicomUID.OphthalmicThicknessDeviationCategories4265.UID, DicomUID.OphthalmicThicknessDeviationCategories4265);
            _uids.Add(DicomUID.OphthalmicAnatomicStructureReferencePoint4266.UID, DicomUID.OphthalmicAnatomicStructureReferencePoint4266);
            _uids.Add(DicomUID.CardiacSynchronizationTechnique3104.UID, DicomUID.CardiacSynchronizationTechnique3104);
            _uids.Add(DicomUID.StainingProtocols8130.UID, DicomUID.StainingProtocols8130);
            _uids.Add(DicomUID.SizeSpecificDoseEstimationMethodForCT10023.UID, DicomUID.SizeSpecificDoseEstimationMethodForCT10023);
            _uids.Add(DicomUID.PathologyImagingProtocols8131.UID, DicomUID.PathologyImagingProtocols8131);
            _uids.Add(DicomUID.MagnificationSelection8132.UID, DicomUID.MagnificationSelection8132);
            _uids.Add(DicomUID.TissueSelection8133.UID, DicomUID.TissueSelection8133);
            _uids.Add(DicomUID.GeneralRegionOfInterestMeasurementModifiers7464.UID, DicomUID.GeneralRegionOfInterestMeasurementModifiers7464);
            _uids.Add(DicomUID.MeasurementsDerivedFromMultipleROIMeasurements7465.UID, DicomUID.MeasurementsDerivedFromMultipleROIMeasurements7465);
            _uids.Add(DicomUID.SurfaceScanAcquisitionTypes8201.UID, DicomUID.SurfaceScanAcquisitionTypes8201);
            _uids.Add(DicomUID.SurfaceScanModeTypes8202.UID, DicomUID.SurfaceScanModeTypes8202);
            _uids.Add(DicomUID.SurfaceScanRegistrationMethodTypes8203.UID, DicomUID.SurfaceScanRegistrationMethodTypes8203);
            _uids.Add(DicomUID.BasicCardiacViews27.UID, DicomUID.BasicCardiacViews27);
            _uids.Add(DicomUID.CTReconstructionAlgorithm10033.UID, DicomUID.CTReconstructionAlgorithm10033);
            _uids.Add(DicomUID.DetectorTypes10030.UID, DicomUID.DetectorTypes10030);
            _uids.Add(DicomUID.CRDRMechanicalConfiguration10031.UID, DicomUID.CRDRMechanicalConfiguration10031);
            _uids.Add(DicomUID.ProjectionXRayAcquisitionDeviceTypes10032.UID, DicomUID.ProjectionXRayAcquisitionDeviceTypes10032);
            _uids.Add(DicomUID.AbstractSegmentationTypes7165.UID, DicomUID.AbstractSegmentationTypes7165);
            _uids.Add(DicomUID.CommonTissueSegmentationTypes7166.UID, DicomUID.CommonTissueSegmentationTypes7166);
            _uids.Add(DicomUID.PeripheralNervousSystemSegmentationTypes7167.UID, DicomUID.PeripheralNervousSystemSegmentationTypes7167);
            _uids.Add(DicomUID.CornealTopographyMappingUnitsForRealWorldValueMapping4267.UID, DicomUID.CornealTopographyMappingUnitsForRealWorldValueMapping4267);
            _uids.Add(DicomUID.CornealTopographyMapValueType4268.UID, DicomUID.CornealTopographyMapValueType4268);
            _uids.Add(DicomUID.BrainStructuresForVolumetricMeasurements7140.UID, DicomUID.BrainStructuresForVolumetricMeasurements7140);
            _uids.Add(DicomUID.RTDoseDerivation7220.UID, DicomUID.RTDoseDerivation7220);
            _uids.Add(DicomUID.RTDosePurposeOfReference7221.UID, DicomUID.RTDosePurposeOfReference7221);
            _uids.Add(DicomUID.SpectroscopyPurposeOfReference7215.UID, DicomUID.SpectroscopyPurposeOfReference7215);
            _uids.Add(DicomUID.ScheduledProcessingParameterConceptCodesForRTTreatment9250.UID, DicomUID.ScheduledProcessingParameterConceptCodesForRTTreatment9250);
            _uids.Add(DicomUID.RadiopharmaceuticalOrganDoseReferenceAuthority10040.UID, DicomUID.RadiopharmaceuticalOrganDoseReferenceAuthority10040);
            _uids.Add(DicomUID.SourceOfRadioisotopeActivityInformation10041.UID, DicomUID.SourceOfRadioisotopeActivityInformation10041);
            _uids.Add(DicomUID.IntravenousExtravasationSymptoms10043.UID, DicomUID.IntravenousExtravasationSymptoms10043);
            _uids.Add(DicomUID.RadiosensitiveOrgans10044.UID, DicomUID.RadiosensitiveOrgans10044);
            _uids.Add(DicomUID.RadiopharmaceuticalPatientState10045.UID, DicomUID.RadiopharmaceuticalPatientState10045);
            _uids.Add(DicomUID.GFRMeasurements10046.UID, DicomUID.GFRMeasurements10046);
            _uids.Add(DicomUID.GFRMeasurementMethods10047.UID, DicomUID.GFRMeasurementMethods10047);
            _uids.Add(DicomUID.VisualEvaluationMethods8300.UID, DicomUID.VisualEvaluationMethods8300);
            _uids.Add(DicomUID.TestPatternCodes8301.UID, DicomUID.TestPatternCodes8301);
            _uids.Add(DicomUID.MeasurementPatternCodes8302.UID, DicomUID.MeasurementPatternCodes8302);
            _uids.Add(DicomUID.DisplayDeviceType8303.UID, DicomUID.DisplayDeviceType8303);
            _uids.Add(DicomUID.SUVUnits85.UID, DicomUID.SUVUnits85);
            _uids.Add(DicomUID.T1MeasurementMethods4100.UID, DicomUID.T1MeasurementMethods4100);
            _uids.Add(DicomUID.TracerKineticModels4101.UID, DicomUID.TracerKineticModels4101);
            _uids.Add(DicomUID.PerfusionMeasurementMethods4102.UID, DicomUID.PerfusionMeasurementMethods4102);
            _uids.Add(DicomUID.ArterialInputFunctionMeasurementMethods4103.UID, DicomUID.ArterialInputFunctionMeasurementMethods4103);
            _uids.Add(DicomUID.BolusArrivalTimeDerivationMethods4104.UID, DicomUID.BolusArrivalTimeDerivationMethods4104);
            _uids.Add(DicomUID.PerfusionAnalysisMethods4105.UID, DicomUID.PerfusionAnalysisMethods4105);
            _uids.Add(DicomUID.QuantitativeMethodsUsedForPerfusionAndTracerKineticModels4106.UID, DicomUID.QuantitativeMethodsUsedForPerfusionAndTracerKineticModels4106);
            _uids.Add(DicomUID.TracerKineticModelParameters4107.UID, DicomUID.TracerKineticModelParameters4107);
            _uids.Add(DicomUID.PerfusionModelParameters4108.UID, DicomUID.PerfusionModelParameters4108);
            _uids.Add(DicomUID.ModelIndependentDynamicContrastAnalysisParameters4109.UID, DicomUID.ModelIndependentDynamicContrastAnalysisParameters4109);
            _uids.Add(DicomUID.TracerKineticModelingCovariates4110.UID, DicomUID.TracerKineticModelingCovariates4110);
            _uids.Add(DicomUID.ContrastCharacteristics4111.UID, DicomUID.ContrastCharacteristics4111);
            _uids.Add(DicomUID.MeasurementReportDocumentTitles7021.UID, DicomUID.MeasurementReportDocumentTitles7021);
            _uids.Add(DicomUID.QuantitativeDiagnosticImagingProcedures100.UID, DicomUID.QuantitativeDiagnosticImagingProcedures100);
            _uids.Add(DicomUID.PETRegionOfInterestMeasurements7466.UID, DicomUID.PETRegionOfInterestMeasurements7466);
            _uids.Add(DicomUID.GrayLevelCoOccurrenceMatrixMeasurements7467.UID, DicomUID.GrayLevelCoOccurrenceMatrixMeasurements7467);
            _uids.Add(DicomUID.TextureMeasurements7468.UID, DicomUID.TextureMeasurements7468);
            _uids.Add(DicomUID.TimePointTypes6146.UID, DicomUID.TimePointTypes6146);
            _uids.Add(DicomUID.GenericIntensityAndSizeMeasurements7469.UID, DicomUID.GenericIntensityAndSizeMeasurements7469);
            _uids.Add(DicomUID.ResponseCriteria6147.UID, DicomUID.ResponseCriteria6147);
            _uids.Add(DicomUID.FetalBiometryAnatomicSites12020.UID, DicomUID.FetalBiometryAnatomicSites12020);
            _uids.Add(DicomUID.FetalLongBoneAnatomicSites12021.UID, DicomUID.FetalLongBoneAnatomicSites12021);
            _uids.Add(DicomUID.FetalCraniumAnatomicSites12022.UID, DicomUID.FetalCraniumAnatomicSites12022);
            _uids.Add(DicomUID.PelvisAndUterusAnatomicSites12023.UID, DicomUID.PelvisAndUterusAnatomicSites12023);
            _uids.Add(DicomUID.ParametricMapDerivationImagePurposeOfReference7222.UID, DicomUID.ParametricMapDerivationImagePurposeOfReference7222);
            _uids.Add(DicomUID.PhysicalQuantityDescriptors9000.UID, DicomUID.PhysicalQuantityDescriptors9000);
            _uids.Add(DicomUID.LymphNodeAnatomicSites7600.UID, DicomUID.LymphNodeAnatomicSites7600);
            _uids.Add(DicomUID.HeadAndNeckCancerAnatomicSites7601.UID, DicomUID.HeadAndNeckCancerAnatomicSites7601);
            _uids.Add(DicomUID.FiberTractsInBrainstem7701.UID, DicomUID.FiberTractsInBrainstem7701);
            _uids.Add(DicomUID.ProjectionAndThalamicFibers7702.UID, DicomUID.ProjectionAndThalamicFibers7702);
            _uids.Add(DicomUID.AssociationFibers7703.UID, DicomUID.AssociationFibers7703);
            _uids.Add(DicomUID.LimbicSystemTracts7704.UID, DicomUID.LimbicSystemTracts7704);
            _uids.Add(DicomUID.CommissuralFibers7705.UID, DicomUID.CommissuralFibers7705);
            _uids.Add(DicomUID.CranialNerves7706.UID, DicomUID.CranialNerves7706);
            _uids.Add(DicomUID.SpinalCordFibers7707.UID, DicomUID.SpinalCordFibers7707);
            _uids.Add(DicomUID.TractographyAnatomicSites7710.UID, DicomUID.TractographyAnatomicSites7710);
            _uids.Add(DicomUID.PrimaryAnatomicStructureForIntraOralRadiographySupernumeraryDentitionDesignationOfTeeth4025.UID, DicomUID.PrimaryAnatomicStructureForIntraOralRadiographySupernumeraryDentitionDesignationOfTeeth4025);
            _uids.Add(DicomUID.PrimaryAnatomicStructureForIntraOralAndCraniofacialRadiographyTeeth4026.UID, DicomUID.PrimaryAnatomicStructureForIntraOralAndCraniofacialRadiographyTeeth4026);
            _uids.Add(DicomUID.IEC61217DevicePositionParameters9401.UID, DicomUID.IEC61217DevicePositionParameters9401);
            _uids.Add(DicomUID.IEC61217GantryPositionParameters9402.UID, DicomUID.IEC61217GantryPositionParameters9402);
            _uids.Add(DicomUID.IEC61217PatientSupportPositionParameters9403.UID, DicomUID.IEC61217PatientSupportPositionParameters9403);
            _uids.Add(DicomUID.ActionableFindingClassification7035.UID, DicomUID.ActionableFindingClassification7035);
            _uids.Add(DicomUID.ImageQualityAssessment7036.UID, DicomUID.ImageQualityAssessment7036);
            _uids.Add(DicomUID.SummaryRadiationExposureQuantities10050.UID, DicomUID.SummaryRadiationExposureQuantities10050);
            _uids.Add(DicomUID.WideFieldOphthalmicPhotographyTransformationMethod4245.UID, DicomUID.WideFieldOphthalmicPhotographyTransformationMethod4245);
            _uids.Add(DicomUID.PETUnits84.UID, DicomUID.PETUnits84);
            _uids.Add(DicomUID.ImplantMaterials7300.UID, DicomUID.ImplantMaterials7300);
            _uids.Add(DicomUID.InterventionTypes7301.UID, DicomUID.InterventionTypes7301);
            _uids.Add(DicomUID.ImplantTemplatesViewOrientations7302.UID, DicomUID.ImplantTemplatesViewOrientations7302);
            _uids.Add(DicomUID.ImplantTemplatesModifiedViewOrientations7303.UID, DicomUID.ImplantTemplatesModifiedViewOrientations7303);
            _uids.Add(DicomUID.ImplantTargetAnatomy7304.UID, DicomUID.ImplantTargetAnatomy7304);
            _uids.Add(DicomUID.ImplantPlanningLandmarks7305.UID, DicomUID.ImplantPlanningLandmarks7305);
            _uids.Add(DicomUID.HumanHipImplantPlanningLandmarks7306.UID, DicomUID.HumanHipImplantPlanningLandmarks7306);
            _uids.Add(DicomUID.ImplantComponentTypes7307.UID, DicomUID.ImplantComponentTypes7307);
            _uids.Add(DicomUID.HumanHipImplantComponentTypes7308.UID, DicomUID.HumanHipImplantComponentTypes7308);
            _uids.Add(DicomUID.HumanTraumaImplantComponentTypes7309.UID, DicomUID.HumanTraumaImplantComponentTypes7309);
            _uids.Add(DicomUID.ImplantFixationMethod7310.UID, DicomUID.ImplantFixationMethod7310);
            _uids.Add(DicomUID.DeviceParticipatingRoles7445.UID, DicomUID.DeviceParticipatingRoles7445);
            _uids.Add(DicomUID.ContainerTypes8101.UID, DicomUID.ContainerTypes8101);
            _uids.Add(DicomUID.ContainerComponentTypes8102.UID, DicomUID.ContainerComponentTypes8102);
            _uids.Add(DicomUID.AnatomicPathologySpecimenTypes8103.UID, DicomUID.AnatomicPathologySpecimenTypes8103);
            _uids.Add(DicomUID.BreastTissueSpecimenTypes8104.UID, DicomUID.BreastTissueSpecimenTypes8104);
            _uids.Add(DicomUID.SpecimenCollectionProcedure8109.UID, DicomUID.SpecimenCollectionProcedure8109);
            _uids.Add(DicomUID.SpecimenSamplingProcedure8110.UID, DicomUID.SpecimenSamplingProcedure8110);
            _uids.Add(DicomUID.SpecimenPreparationProcedure8111.UID, DicomUID.SpecimenPreparationProcedure8111);
            _uids.Add(DicomUID.SpecimenStains8112.UID, DicomUID.SpecimenStains8112);
            _uids.Add(DicomUID.SpecimenPreparationSteps8113.UID, DicomUID.SpecimenPreparationSteps8113);
            _uids.Add(DicomUID.SpecimenFixatives8114.UID, DicomUID.SpecimenFixatives8114);
            _uids.Add(DicomUID.SpecimenEmbeddingMedia8115.UID, DicomUID.SpecimenEmbeddingMedia8115);
            _uids.Add(DicomUID.SourceOfProjectionXRayDoseInformation10020.UID, DicomUID.SourceOfProjectionXRayDoseInformation10020);
            _uids.Add(DicomUID.SourceOfCTDoseInformation10021.UID, DicomUID.SourceOfCTDoseInformation10021);
            _uids.Add(DicomUID.RadiationDoseReferencePoints10025.UID, DicomUID.RadiationDoseReferencePoints10025);
            _uids.Add(DicomUID.VolumetricViewDescription501.UID, DicomUID.VolumetricViewDescription501);
            _uids.Add(DicomUID.VolumetricViewModifier502.UID, DicomUID.VolumetricViewModifier502);
            _uids.Add(DicomUID.DiffusionAcquisitionValueTypes7260.UID, DicomUID.DiffusionAcquisitionValueTypes7260);
            _uids.Add(DicomUID.DiffusionModelValueTypes7261.UID, DicomUID.DiffusionModelValueTypes7261);
            _uids.Add(DicomUID.DiffusionTractographyAlgorithmFamilies7262.UID, DicomUID.DiffusionTractographyAlgorithmFamilies7262);
            _uids.Add(DicomUID.DiffusionTractographyMeasurementTypes7263.UID, DicomUID.DiffusionTractographyMeasurementTypes7263);
            _uids.Add(DicomUID.ResearchAnimalSourceRegistries7490.UID, DicomUID.ResearchAnimalSourceRegistries7490);
            _uids.Add(DicomUID.YesNoOnly231.UID, DicomUID.YesNoOnly231);
            _uids.Add(DicomUID.BiosafetyLevels601.UID, DicomUID.BiosafetyLevels601);
            _uids.Add(DicomUID.BiosafetyControlReasons602.UID, DicomUID.BiosafetyControlReasons602);
            _uids.Add(DicomUID.SexMaleFemaleOrBoth7457.UID, DicomUID.SexMaleFemaleOrBoth7457);
            _uids.Add(DicomUID.AnimalRoomTypes603.UID, DicomUID.AnimalRoomTypes603);
            _uids.Add(DicomUID.DeviceReuse604.UID, DicomUID.DeviceReuse604);
            _uids.Add(DicomUID.AnimalBeddingMaterial605.UID, DicomUID.AnimalBeddingMaterial605);
            _uids.Add(DicomUID.AnimalShelterTypes606.UID, DicomUID.AnimalShelterTypes606);
            _uids.Add(DicomUID.AnimalFeedTypes607.UID, DicomUID.AnimalFeedTypes607);
            _uids.Add(DicomUID.AnimalFeedSources608.UID, DicomUID.AnimalFeedSources608);
            _uids.Add(DicomUID.AnimalFeedingMethods609.UID, DicomUID.AnimalFeedingMethods609);
            _uids.Add(DicomUID.WaterTypes610.UID, DicomUID.WaterTypes610);
            _uids.Add(DicomUID.AnesthesiaCategoryCodeTypeForSmallAnimalAnesthesia611.UID, DicomUID.AnesthesiaCategoryCodeTypeForSmallAnimalAnesthesia611);
            _uids.Add(DicomUID.AnesthesiaCategoryCodeTypeFromAnesthesiaQualityInitiativeAQI612.UID, DicomUID.AnesthesiaCategoryCodeTypeFromAnesthesiaQualityInitiativeAQI612);
            _uids.Add(DicomUID.AnesthesiaInductionCodeTypeForSmallAnimalAnesthesia613.UID, DicomUID.AnesthesiaInductionCodeTypeForSmallAnimalAnesthesia613);
            _uids.Add(DicomUID.AnesthesiaInductionCodeTypeFromAnesthesiaQualityInitiativeAQI614.UID, DicomUID.AnesthesiaInductionCodeTypeFromAnesthesiaQualityInitiativeAQI614);
            _uids.Add(DicomUID.AnesthesiaMaintenanceCodeTypeForSmallAnimalAnesthesia615.UID, DicomUID.AnesthesiaMaintenanceCodeTypeForSmallAnimalAnesthesia615);
            _uids.Add(DicomUID.AnesthesiaMaintenanceCodeTypeFromAnesthesiaQualityInitiativeAQI616.UID, DicomUID.AnesthesiaMaintenanceCodeTypeFromAnesthesiaQualityInitiativeAQI616);
            _uids.Add(DicomUID.AirwayManagementMethodCodeTypeForSmallAnimalAnesthesia617.UID, DicomUID.AirwayManagementMethodCodeTypeForSmallAnimalAnesthesia617);
            _uids.Add(DicomUID.AirwayManagementMethodCodeTypeFromAnesthesiaQualityInitiativeAQI618.UID, DicomUID.AirwayManagementMethodCodeTypeFromAnesthesiaQualityInitiativeAQI618);
            _uids.Add(DicomUID.AirwayManagementSubMethodCodeTypeForSmallAnimalAnesthesia619.UID, DicomUID.AirwayManagementSubMethodCodeTypeForSmallAnimalAnesthesia619);
            _uids.Add(DicomUID.AirwayManagementSubMethodCodeTypeFromAnesthesiaQualityInitiativeAQI620.UID, DicomUID.AirwayManagementSubMethodCodeTypeFromAnesthesiaQualityInitiativeAQI620);
            _uids.Add(DicomUID.TypeOfMedicationForSmallAnimalAnesthesia621.UID, DicomUID.TypeOfMedicationForSmallAnimalAnesthesia621);
            _uids.Add(DicomUID.MedicationTypeCodeTypeFromAnesthesiaQualityInitiativeAQI622.UID, DicomUID.MedicationTypeCodeTypeFromAnesthesiaQualityInitiativeAQI622);
            _uids.Add(DicomUID.MedicationForSmallAnimalAnesthesia623.UID, DicomUID.MedicationForSmallAnimalAnesthesia623);
            _uids.Add(DicomUID.InhalationalAnesthesiaAgentsForSmallAnimalAnesthesia624.UID, DicomUID.InhalationalAnesthesiaAgentsForSmallAnimalAnesthesia624);
            _uids.Add(DicomUID.InjectableAnesthesiaAgentsForSmallAnimalAnesthesia625.UID, DicomUID.InjectableAnesthesiaAgentsForSmallAnimalAnesthesia625);
            _uids.Add(DicomUID.PremedicationAgentsForSmallAnimalAnesthesia626.UID, DicomUID.PremedicationAgentsForSmallAnimalAnesthesia626);
            _uids.Add(DicomUID.NeuromuscularBlockingAgentsForSmallAnimalAnesthesia627.UID, DicomUID.NeuromuscularBlockingAgentsForSmallAnimalAnesthesia627);
            _uids.Add(DicomUID.AncillaryMedicationsForSmallAnimalAnesthesia628.UID, DicomUID.AncillaryMedicationsForSmallAnimalAnesthesia628);
            _uids.Add(DicomUID.CarrierGasesForSmallAnimalAnesthesia629.UID, DicomUID.CarrierGasesForSmallAnimalAnesthesia629);
            _uids.Add(DicomUID.LocalAnestheticsForSmallAnimalAnesthesia630.UID, DicomUID.LocalAnestheticsForSmallAnimalAnesthesia630);
            _uids.Add(DicomUID.PhaseOfProcedureRequiringAnesthesia631.UID, DicomUID.PhaseOfProcedureRequiringAnesthesia631);
            _uids.Add(DicomUID.PhaseOfSurgicalProcedureRequiringAnesthesia632.UID, DicomUID.PhaseOfSurgicalProcedureRequiringAnesthesia632);
            _uids.Add(DicomUID.PhaseOfImagingProcedureRequiringAnesthesia633.UID, DicomUID.PhaseOfImagingProcedureRequiringAnesthesia633);
            _uids.Add(DicomUID.PhaseOfAnimalHandling634.UID, DicomUID.PhaseOfAnimalHandling634);
            _uids.Add(DicomUID.HeatingMethod635.UID, DicomUID.HeatingMethod635);
            _uids.Add(DicomUID.TemperatureSensorDeviceComponentTypeForSmallAnimalProcedures636.UID, DicomUID.TemperatureSensorDeviceComponentTypeForSmallAnimalProcedures636);
            _uids.Add(DicomUID.ExogenousSubstanceTypes637.UID, DicomUID.ExogenousSubstanceTypes637);
            _uids.Add(DicomUID.ExogenousSubstance638.UID, DicomUID.ExogenousSubstance638);
            _uids.Add(DicomUID.TumorGraftHistologicType639.UID, DicomUID.TumorGraftHistologicType639);
            _uids.Add(DicomUID.Fibrils640.UID, DicomUID.Fibrils640);
            _uids.Add(DicomUID.Viruses641.UID, DicomUID.Viruses641);
            _uids.Add(DicomUID.Cytokines642.UID, DicomUID.Cytokines642);
            _uids.Add(DicomUID.Toxins643.UID, DicomUID.Toxins643);
            _uids.Add(DicomUID.ExogenousSubstanceAdministrationSites644.UID, DicomUID.ExogenousSubstanceAdministrationSites644);
            _uids.Add(DicomUID.ExogenousSubstanceTissueOfOrigin645.UID, DicomUID.ExogenousSubstanceTissueOfOrigin645);
            _uids.Add(DicomUID.PreclinicalSmallAnimalImagingProcedures646.UID, DicomUID.PreclinicalSmallAnimalImagingProcedures646);
            _uids.Add(DicomUID.PositionReferenceIndicatorForFrameOfReference647.UID, DicomUID.PositionReferenceIndicatorForFrameOfReference647);
            _uids.Add(DicomUID.PresentAbsentOnly241.UID, DicomUID.PresentAbsentOnly241);
            _uids.Add(DicomUID.WaterEquivalentDiameterMethod10024.UID, DicomUID.WaterEquivalentDiameterMethod10024);
            _uids.Add(DicomUID.RadiotherapyPurposesOfReference7022.UID, DicomUID.RadiotherapyPurposesOfReference7022);
            _uids.Add(DicomUID.ContentAssessmentTypes701.UID, DicomUID.ContentAssessmentTypes701);
            _uids.Add(DicomUID.RTContentAssessmentTypes702.UID, DicomUID.RTContentAssessmentTypes702);
            _uids.Add(DicomUID.BasisOfAssessment703.UID, DicomUID.BasisOfAssessment703);
            _uids.Add(DicomUID.ReaderSpecialty7449.UID, DicomUID.ReaderSpecialty7449);
            _uids.Add(DicomUID.RequestedReportTypes9233.UID, DicomUID.RequestedReportTypes9233);
            _uids.Add(DicomUID.CTTransversePlaneReferenceBasis1000.UID, DicomUID.CTTransversePlaneReferenceBasis1000);
            _uids.Add(DicomUID.AnatomicalReferenceBasis1001.UID, DicomUID.AnatomicalReferenceBasis1001);
            _uids.Add(DicomUID.AnatomicalReferenceBasisHead1002.UID, DicomUID.AnatomicalReferenceBasisHead1002);
            _uids.Add(DicomUID.AnatomicalReferenceBasisSpine1003.UID, DicomUID.AnatomicalReferenceBasisSpine1003);
            _uids.Add(DicomUID.AnatomicalReferenceBasisChest1004.UID, DicomUID.AnatomicalReferenceBasisChest1004);
            _uids.Add(DicomUID.AnatomicalReferenceBasisAbdomenPelvis1005.UID, DicomUID.AnatomicalReferenceBasisAbdomenPelvis1005);
            _uids.Add(DicomUID.AnatomicalReferenceBasisExtremities1006.UID, DicomUID.AnatomicalReferenceBasisExtremities1006);
            _uids.Add(DicomUID.ReferenceGeometryPlanes1010.UID, DicomUID.ReferenceGeometryPlanes1010);
            _uids.Add(DicomUID.ReferenceGeometryPoints1011.UID, DicomUID.ReferenceGeometryPoints1011);
            _uids.Add(DicomUID.PatientAlignmentMethods1015.UID, DicomUID.PatientAlignmentMethods1015);
            _uids.Add(DicomUID.ContraindicationsForCTImaging1200.UID, DicomUID.ContraindicationsForCTImaging1200);
            _uids.Add(DicomUID.FiducialsCategories7110.UID, DicomUID.FiducialsCategories7110);
            _uids.Add(DicomUID.Fiducials7111.UID, DicomUID.Fiducials7111);
            _uids.Add(DicomUID.NonImageSourceInstancePurposesOfReference7013.UID, DicomUID.NonImageSourceInstancePurposesOfReference7013);
            _uids.Add(DicomUID.RTProcessOutput7023.UID, DicomUID.RTProcessOutput7023);
            _uids.Add(DicomUID.RTProcessInput7024.UID, DicomUID.RTProcessInput7024);
            _uids.Add(DicomUID.RTProcessInputUsed7025.UID, DicomUID.RTProcessInputUsed7025);
            _uids.Add(DicomUID.ProstateSectorAnatomy6300.UID, DicomUID.ProstateSectorAnatomy6300);
            _uids.Add(DicomUID.ProstateSectorAnatomyFromPIRADSV26301.UID, DicomUID.ProstateSectorAnatomyFromPIRADSV26301);
            _uids.Add(DicomUID.ProstateSectorAnatomyFromEuropeanConcensus16SectorMinimalModel6302.UID, DicomUID.ProstateSectorAnatomyFromEuropeanConcensus16SectorMinimalModel6302);
            _uids.Add(DicomUID.ProstateSectorAnatomyFromEuropeanConcensus27SectorOptimalModel6303.UID, DicomUID.ProstateSectorAnatomyFromEuropeanConcensus27SectorOptimalModel6303);
            _uids.Add(DicomUID.MeasurementSelectionReasons12301.UID, DicomUID.MeasurementSelectionReasons12301);
            _uids.Add(DicomUID.EchoFindingObservationTypes12302.UID, DicomUID.EchoFindingObservationTypes12302);
            _uids.Add(DicomUID.EchoMeasurementTypes12303.UID, DicomUID.EchoMeasurementTypes12303);
            _uids.Add(DicomUID.EchoMeasuredProperties12304.UID, DicomUID.EchoMeasuredProperties12304);
            _uids.Add(DicomUID.BasicEchoAnatomicSites12305.UID, DicomUID.BasicEchoAnatomicSites12305);
            _uids.Add(DicomUID.EchoFlowDirections12306.UID, DicomUID.EchoFlowDirections12306);
            _uids.Add(DicomUID.CardiacPhasesAndTimePoints12307.UID, DicomUID.CardiacPhasesAndTimePoints12307);
            _uids.Add(DicomUID.CoreEchoMeasurements12300.UID, DicomUID.CoreEchoMeasurements12300);
            _uids.Add(DicomUID.OCTAProcessingAlgorithmFamilies4270.UID, DicomUID.OCTAProcessingAlgorithmFamilies4270);
            _uids.Add(DicomUID.EnFaceImageTypes4271.UID, DicomUID.EnFaceImageTypes4271);
            _uids.Add(DicomUID.OptScanPatternTypes4272.UID, DicomUID.OptScanPatternTypes4272);
            _uids.Add(DicomUID.RetinalSegmentationSurfaces4273.UID, DicomUID.RetinalSegmentationSurfaces4273);
            _uids.Add(DicomUID.OrgansForRadiationDoseEstimates10060.UID, DicomUID.OrgansForRadiationDoseEstimates10060);
            _uids.Add(DicomUID.AbsorbedRadiationDoseTypes10061.UID, DicomUID.AbsorbedRadiationDoseTypes10061);
            _uids.Add(DicomUID.EquivalentRadiationDoseTypes10062.UID, DicomUID.EquivalentRadiationDoseTypes10062);
            _uids.Add(DicomUID.RadiationDoseEstimateDistributionRepresentation10063.UID, DicomUID.RadiationDoseEstimateDistributionRepresentation10063);
            _uids.Add(DicomUID.PatientModelType10064.UID, DicomUID.PatientModelType10064);
            _uids.Add(DicomUID.RadiationTransportModelType10065.UID, DicomUID.RadiationTransportModelType10065);
            _uids.Add(DicomUID.AttenuatorCategory10066.UID, DicomUID.AttenuatorCategory10066);
            _uids.Add(DicomUID.RadiationAttenuatorMaterials10067.UID, DicomUID.RadiationAttenuatorMaterials10067);
            _uids.Add(DicomUID.EstimateMethodTypes10068.UID, DicomUID.EstimateMethodTypes10068);
            _uids.Add(DicomUID.RadiationDoseEstimationParameter10069.UID, DicomUID.RadiationDoseEstimationParameter10069);
            _uids.Add(DicomUID.RadiationDoseTypes10070.UID, DicomUID.RadiationDoseTypes10070);
            _uids.Add(DicomUID.MRDiffusionComponentSemantics7270.UID, DicomUID.MRDiffusionComponentSemantics7270);
            _uids.Add(DicomUID.MRDiffusionAnisotropyIndices7271.UID, DicomUID.MRDiffusionAnisotropyIndices7271);
            _uids.Add(DicomUID.MRDiffusionModelParameters7272.UID, DicomUID.MRDiffusionModelParameters7272);
            _uids.Add(DicomUID.MRDiffusionModels7273.UID, DicomUID.MRDiffusionModels7273);
            _uids.Add(DicomUID.MRDiffusionModelFittingMethods7274.UID, DicomUID.MRDiffusionModelFittingMethods7274);
            _uids.Add(DicomUID.MRDiffusionModelSpecificMethods7275.UID, DicomUID.MRDiffusionModelSpecificMethods7275);
            _uids.Add(DicomUID.MRDiffusionModelInputs7276.UID, DicomUID.MRDiffusionModelInputs7276);
            _uids.Add(DicomUID.UnitsOfDiffusionRateAreaOverTime7277.UID, DicomUID.UnitsOfDiffusionRateAreaOverTime7277);
            _uids.Add(DicomUID.PediatricSizeCategories7039.UID, DicomUID.PediatricSizeCategories7039);
            _uids.Add(DicomUID.CalciumScoringPatientSizeCategories7041.UID, DicomUID.CalciumScoringPatientSizeCategories7041);
            _uids.Add(DicomUID.ReasonForRepeatingAcquisition10034.UID, DicomUID.ReasonForRepeatingAcquisition10034);
            _uids.Add(DicomUID.ProtocolAssertionCodes800.UID, DicomUID.ProtocolAssertionCodes800);
            _uids.Add(DicomUID.RadiotherapeuticDoseMeasurementDevices7026.UID, DicomUID.RadiotherapeuticDoseMeasurementDevices7026);
            _uids.Add(DicomUID.ExportAdditionalInformationDocumentTitles7014.UID, DicomUID.ExportAdditionalInformationDocumentTitles7014);
            _uids.Add(DicomUID.ExportDelayReasons7015.UID, DicomUID.ExportDelayReasons7015);
            _uids.Add(DicomUID.LevelOfDifficulty7016.UID, DicomUID.LevelOfDifficulty7016);
            _uids.Add(DicomUID.CategoryOfTeachingMaterialImaging7017.UID, DicomUID.CategoryOfTeachingMaterialImaging7017);
            _uids.Add(DicomUID.MiscellaneousDocumentTitles7018.UID, DicomUID.MiscellaneousDocumentTitles7018);
            _uids.Add(DicomUID.SegmentationNonImageSourcePurposesOfReference7019.UID, DicomUID.SegmentationNonImageSourcePurposesOfReference7019);
            _uids.Add(DicomUID.LongitudinalTemporalEventTypes280.UID, DicomUID.LongitudinalTemporalEventTypes280);
            _uids.Add(DicomUID.NonLesionObjectTypePhysicalObjects6401.UID, DicomUID.NonLesionObjectTypePhysicalObjects6401);
            _uids.Add(DicomUID.NonLesionObjectTypeSubstances6402.UID, DicomUID.NonLesionObjectTypeSubstances6402);
            _uids.Add(DicomUID.NonLesionObjectTypeTissues6403.UID, DicomUID.NonLesionObjectTypeTissues6403);
            _uids.Add(DicomUID.ChestNonLesionObjectTypePhysicalObjects6404.UID, DicomUID.ChestNonLesionObjectTypePhysicalObjects6404);
            _uids.Add(DicomUID.ChestNonLesionObjectTypeTissues6405.UID, DicomUID.ChestNonLesionObjectTypeTissues6405);
            _uids.Add(DicomUID.TissueSegmentationPropertyTypes7191.UID, DicomUID.TissueSegmentationPropertyTypes7191);
            _uids.Add(DicomUID.AnatomicalStructureSegmentationPropertyTypes7192.UID, DicomUID.AnatomicalStructureSegmentationPropertyTypes7192);
            _uids.Add(DicomUID.PhysicalObjectSegmentationPropertyTypes7193.UID, DicomUID.PhysicalObjectSegmentationPropertyTypes7193);
            _uids.Add(DicomUID.MorphologicallyAbnormalStructureSegmentationPropertyTypes7194.UID, DicomUID.MorphologicallyAbnormalStructureSegmentationPropertyTypes7194);
            _uids.Add(DicomUID.FunctionSegmentationPropertyTypes7195.UID, DicomUID.FunctionSegmentationPropertyTypes7195);
            _uids.Add(DicomUID.SpatialAndRelationalConceptSegmentationPropertyTypes7196.UID, DicomUID.SpatialAndRelationalConceptSegmentationPropertyTypes7196);
            _uids.Add(DicomUID.BodySubstanceSegmentationPropertyTypes7197.UID, DicomUID.BodySubstanceSegmentationPropertyTypes7197);
            _uids.Add(DicomUID.SubstanceSegmentationPropertyTypes7198.UID, DicomUID.SubstanceSegmentationPropertyTypes7198);
            _uids.Add(DicomUID.InterpretationRequestDiscontinuationReasons9303.UID, DicomUID.InterpretationRequestDiscontinuationReasons9303);
            _uids.Add(DicomUID.GrayLevelRunLengthBasedFeatures7475.UID, DicomUID.GrayLevelRunLengthBasedFeatures7475);
            _uids.Add(DicomUID.GrayLevelSizeZoneBasedFeatures7476.UID, DicomUID.GrayLevelSizeZoneBasedFeatures7476);
            _uids.Add(DicomUID.EncapsulatedDocumentSourcePurposesOfReference7060.UID, DicomUID.EncapsulatedDocumentSourcePurposesOfReference7060);
            _uids.Add(DicomUID.ModelDocumentTitles7061.UID, DicomUID.ModelDocumentTitles7061);
            _uids.Add(DicomUID.PurposeOfReferenceToPredecessor3DModel7062.UID, DicomUID.PurposeOfReferenceToPredecessor3DModel7062);
            _uids.Add(DicomUID.ModelScaleUnits7063.UID, DicomUID.ModelScaleUnits7063);
            _uids.Add(DicomUID.ModelUsage7064.UID, DicomUID.ModelUsage7064);
            _uids.Add(DicomUID.RadiationDoseUnits10071.UID, DicomUID.RadiationDoseUnits10071);
            _uids.Add(DicomUID.RadiotherapyFiducials7112.UID, DicomUID.RadiotherapyFiducials7112);
            _uids.Add(DicomUID.MultiEnergyRelevantMaterials300.UID, DicomUID.MultiEnergyRelevantMaterials300);
            _uids.Add(DicomUID.MultiEnergyMaterialUnits301.UID, DicomUID.MultiEnergyMaterialUnits301);
            _uids.Add(DicomUID.DosimetricObjectiveTypes9500.UID, DicomUID.DosimetricObjectiveTypes9500);
            _uids.Add(DicomUID.PrescriptionAnatomyCategories9501.UID, DicomUID.PrescriptionAnatomyCategories9501);
            _uids.Add(DicomUID.RTSegmentAnnotationCategories9502.UID, DicomUID.RTSegmentAnnotationCategories9502);
            _uids.Add(DicomUID.RadiotherapyTherapeuticRoleCategories9503.UID, DicomUID.RadiotherapyTherapeuticRoleCategories9503);
            _uids.Add(DicomUID.RTGeometricInformation9504.UID, DicomUID.RTGeometricInformation9504);
            _uids.Add(DicomUID.FixationOrPositioningDevices9505.UID, DicomUID.FixationOrPositioningDevices9505);
            _uids.Add(DicomUID.BrachytherapyDevices9506.UID, DicomUID.BrachytherapyDevices9506);
            _uids.Add(DicomUID.ExternalBodyModels9507.UID, DicomUID.ExternalBodyModels9507);
            _uids.Add(DicomUID.NonSpecificVolumes9508.UID, DicomUID.NonSpecificVolumes9508);
            _uids.Add(DicomUID.PurposeOfReferenceForRTPhysicianIntentInput9509.UID, DicomUID.PurposeOfReferenceForRTPhysicianIntentInput9509);
            _uids.Add(DicomUID.PurposeOfReferenceForRTTreatmentPlanningInput9510.UID, DicomUID.PurposeOfReferenceForRTTreatmentPlanningInput9510);
            _uids.Add(DicomUID.GeneralExternalRadiotherapyProcedureTechniques9511.UID, DicomUID.GeneralExternalRadiotherapyProcedureTechniques9511);
            _uids.Add(DicomUID.TomotherapeuticRadiotherapyProcedureTechniques9512.UID, DicomUID.TomotherapeuticRadiotherapyProcedureTechniques9512);
            _uids.Add(DicomUID.FixationDevices9513.UID, DicomUID.FixationDevices9513);
            _uids.Add(DicomUID.AnatomicalStructuresForRadiotherapy9514.UID, DicomUID.AnatomicalStructuresForRadiotherapy9514);
            _uids.Add(DicomUID.RTPatientSupportDevices9515.UID, DicomUID.RTPatientSupportDevices9515);
            _uids.Add(DicomUID.RadiotherapyBolusDeviceTypes9516.UID, DicomUID.RadiotherapyBolusDeviceTypes9516);
            _uids.Add(DicomUID.RadiotherapyBlockDeviceTypes9517.UID, DicomUID.RadiotherapyBlockDeviceTypes9517);
            _uids.Add(DicomUID.RadiotherapyAccessoryNoSlotHolderDeviceTypes9518.UID, DicomUID.RadiotherapyAccessoryNoSlotHolderDeviceTypes9518);
            _uids.Add(DicomUID.RadiotherapyAccessorySlotHolderDeviceTypes9519.UID, DicomUID.RadiotherapyAccessorySlotHolderDeviceTypes9519);
            _uids.Add(DicomUID.SegmentedRTAccessoryDevices9520.UID, DicomUID.SegmentedRTAccessoryDevices9520);
            _uids.Add(DicomUID.RadiotherapyTreatmentEnergyUnit9521.UID, DicomUID.RadiotherapyTreatmentEnergyUnit9521);
            _uids.Add(DicomUID.MultiSourceRadiotherapyProcedureTechniques9522.UID, DicomUID.MultiSourceRadiotherapyProcedureTechniques9522);
            _uids.Add(DicomUID.RoboticRadiotherapyProcedureTechniques9523.UID, DicomUID.RoboticRadiotherapyProcedureTechniques9523);
            _uids.Add(DicomUID.RadiotherapyProcedureTechniques9524.UID, DicomUID.RadiotherapyProcedureTechniques9524);
            _uids.Add(DicomUID.RadiationTherapyParticle9525.UID, DicomUID.RadiationTherapyParticle9525);
            _uids.Add(DicomUID.IonTherapyParticle9526.UID, DicomUID.IonTherapyParticle9526);
            _uids.Add(DicomUID.TeletherapyIsotope9527.UID, DicomUID.TeletherapyIsotope9527);
            _uids.Add(DicomUID.BrachytherapyIsotope9528.UID, DicomUID.BrachytherapyIsotope9528);
            _uids.Add(DicomUID.SingleDoseDosimetricObjectives9529.UID, DicomUID.SingleDoseDosimetricObjectives9529);
            _uids.Add(DicomUID.PercentageAndDoseDosimetricObjectives9530.UID, DicomUID.PercentageAndDoseDosimetricObjectives9530);
            _uids.Add(DicomUID.VolumeAndDoseDosimetricObjectives9531.UID, DicomUID.VolumeAndDoseDosimetricObjectives9531);
            _uids.Add(DicomUID.NoParameterDosimetricObjectives9532.UID, DicomUID.NoParameterDosimetricObjectives9532);
            _uids.Add(DicomUID.DeliveryTimeStructure9533.UID, DicomUID.DeliveryTimeStructure9533);
            _uids.Add(DicomUID.RadiotherapyTargets9534.UID, DicomUID.RadiotherapyTargets9534);
            _uids.Add(DicomUID.RadiotherapyDoseCalculationRoles9535.UID, DicomUID.RadiotherapyDoseCalculationRoles9535);
            _uids.Add(DicomUID.RadiotherapyPrescribingAndSegmentingPersonRoles9536.UID, DicomUID.RadiotherapyPrescribingAndSegmentingPersonRoles9536);
            _uids.Add(DicomUID.EffectiveDoseCalculationMethodCategories9537.UID, DicomUID.EffectiveDoseCalculationMethodCategories9537);
            _uids.Add(DicomUID.RadiationTransportBasedEffectiveDoseMethodModifiers9538.UID, DicomUID.RadiationTransportBasedEffectiveDoseMethodModifiers9538);
            _uids.Add(DicomUID.FractionationBasedEffectiveDoseMethodModifiers9539.UID, DicomUID.FractionationBasedEffectiveDoseMethodModifiers9539);
            _uids.Add(DicomUID.ImagingAgentAdministrationAdverseEvents60.UID, DicomUID.ImagingAgentAdministrationAdverseEvents60);
            _uids.Add(DicomUID.TimeRelativeToProcedure61.UID, DicomUID.TimeRelativeToProcedure61);
            _uids.Add(DicomUID.ImagingAgentAdministrationPhaseType62.UID, DicomUID.ImagingAgentAdministrationPhaseType62);
            _uids.Add(DicomUID.ImagingAgentAdministrationMode63.UID, DicomUID.ImagingAgentAdministrationMode63);
            _uids.Add(DicomUID.ImagingAgentAdministrationPatientState64.UID, DicomUID.ImagingAgentAdministrationPatientState64);
            _uids.Add(DicomUID.PreMedicationForImagingAgentAdministration65.UID, DicomUID.PreMedicationForImagingAgentAdministration65);
            _uids.Add(DicomUID.MedicationForImagingAgentAdministration66.UID, DicomUID.MedicationForImagingAgentAdministration66);
            _uids.Add(DicomUID.ImagingAgentAdministrationCompletionStatus67.UID, DicomUID.ImagingAgentAdministrationCompletionStatus67);
            _uids.Add(DicomUID.ImagingAgentAdministrationPharmaceuticalUnitOfPresentation68.UID, DicomUID.ImagingAgentAdministrationPharmaceuticalUnitOfPresentation68);
            _uids.Add(DicomUID.ImagingAgentAdministrationConsumables69.UID, DicomUID.ImagingAgentAdministrationConsumables69);
            _uids.Add(DicomUID.Flush70.UID, DicomUID.Flush70);
            _uids.Add(DicomUID.ImagingAgentAdministrationInjectorEventType71.UID, DicomUID.ImagingAgentAdministrationInjectorEventType71);
            _uids.Add(DicomUID.ImagingAgentAdministrationStepType72.UID, DicomUID.ImagingAgentAdministrationStepType72);
            _uids.Add(DicomUID.BolusShapingCurves73.UID, DicomUID.BolusShapingCurves73);
            _uids.Add(DicomUID.ImagingAgentAdministrationConsumableCatheterType74.UID, DicomUID.ImagingAgentAdministrationConsumableCatheterType74);
            _uids.Add(DicomUID.LowHighEqual75.UID, DicomUID.LowHighEqual75);
            _uids.Add(DicomUID.TypeOfPreMedication76.UID, DicomUID.TypeOfPreMedication76);
            _uids.Add(DicomUID.LateralityWithMedian245.UID, DicomUID.LateralityWithMedian245);
            _uids.Add(DicomUID.DermatologyAnatomicSites4029.UID, DicomUID.DermatologyAnatomicSites4029);
            _uids.Add(DicomUID.QuantitativeImageFeatures218.UID, DicomUID.QuantitativeImageFeatures218);
            _uids.Add(DicomUID.GlobalShapeDescriptors7477.UID, DicomUID.GlobalShapeDescriptors7477);
            _uids.Add(DicomUID.IntensityHistogramFeatures7478.UID, DicomUID.IntensityHistogramFeatures7478);
            _uids.Add(DicomUID.GreyLevelDistanceZoneBasedFeatures7479.UID, DicomUID.GreyLevelDistanceZoneBasedFeatures7479);
            _uids.Add(DicomUID.NeighbourhoodGreyToneDifferenceBasedFeatures7500.UID, DicomUID.NeighbourhoodGreyToneDifferenceBasedFeatures7500);
            _uids.Add(DicomUID.NeighbouringGreyLevelDependenceBasedFeatures7501.UID, DicomUID.NeighbouringGreyLevelDependenceBasedFeatures7501);
            _uids.Add(DicomUID.CorneaMeasurementMethodDescriptors4242.UID, DicomUID.CorneaMeasurementMethodDescriptors4242);
            _uids.Add(DicomUID.SegmentedRadiotherapeuticDoseMeasurementDevices7027.UID, DicomUID.SegmentedRadiotherapeuticDoseMeasurementDevices7027);
            _uids.Add(DicomUID.ClinicalCourseOfDisease6098.UID, DicomUID.ClinicalCourseOfDisease6098);
            _uids.Add(DicomUID.RacialGroup6099.UID, DicomUID.RacialGroup6099);
            _uids.Add(DicomUID.RelativeLaterality246.UID, DicomUID.RelativeLaterality246);
            _uids.Add(DicomUID.BrainLesionSegmentationTypesWithNecrosis7168.UID, DicomUID.BrainLesionSegmentationTypesWithNecrosis7168);
            _uids.Add(DicomUID.BrainLesionSegmentationTypesWithoutNecrosis7169.UID, DicomUID.BrainLesionSegmentationTypesWithoutNecrosis7169);
            _uids.Add(DicomUID.NonAcquisitionModality32.UID, DicomUID.NonAcquisitionModality32);
            _uids.Add(DicomUID.Modality33.UID, DicomUID.Modality33);
            _uids.Add(DicomUID.LateralityLeftRightOnly247.UID, DicomUID.LateralityLeftRightOnly247);
            _uids.Add(DicomUID.QualitativeEvaluationModifierTypes210.UID, DicomUID.QualitativeEvaluationModifierTypes210);
            _uids.Add(DicomUID.QualitativeEvaluationModifierValues211.UID, DicomUID.QualitativeEvaluationModifierValues211);
            _uids.Add(DicomUID.GenericAnatomicLocationModifiers212.UID, DicomUID.GenericAnatomicLocationModifiers212);
            _uids.Add(DicomUID.BeamLimitingDeviceTypes9541.UID, DicomUID.BeamLimitingDeviceTypes9541);
            _uids.Add(DicomUID.CompensatorDeviceTypes9542.UID, DicomUID.CompensatorDeviceTypes9542);
            _uids.Add(DicomUID.RadiotherapyTreatmentMachineModes9543.UID, DicomUID.RadiotherapyTreatmentMachineModes9543);
            _uids.Add(DicomUID.RadiotherapyDistanceReferenceLocations9544.UID, DicomUID.RadiotherapyDistanceReferenceLocations9544);
            _uids.Add(DicomUID.FixedBeamLimitingDeviceTypes9545.UID, DicomUID.FixedBeamLimitingDeviceTypes9545);
            _uids.Add(DicomUID.RadiotherapyWedgeTypes9546.UID, DicomUID.RadiotherapyWedgeTypes9546);
            _uids.Add(DicomUID.RTBeamLimitingDeviceOrientationLabels9547.UID, DicomUID.RTBeamLimitingDeviceOrientationLabels9547);
            _uids.Add(DicomUID.GeneralAccessoryDeviceTypes9548.UID, DicomUID.GeneralAccessoryDeviceTypes9548);
            _uids.Add(DicomUID.RadiationGenerationModeTypes9549.UID, DicomUID.RadiationGenerationModeTypes9549);
            _uids.Add(DicomUID.CArmPhotonElectronDeliveryRateUnits9550.UID, DicomUID.CArmPhotonElectronDeliveryRateUnits9550);
            _uids.Add(DicomUID.TreatmentDeliveryDeviceTypes9551.UID, DicomUID.TreatmentDeliveryDeviceTypes9551);
            _uids.Add(DicomUID.CArmPhotonElectronDosimeterUnits9552.UID, DicomUID.CArmPhotonElectronDosimeterUnits9552);
            _uids.Add(DicomUID.TreatmentPoints9553.UID, DicomUID.TreatmentPoints9553);
            _uids.Add(DicomUID.EquipmentReferencePoints9554.UID, DicomUID.EquipmentReferencePoints9554);
            _uids.Add(DicomUID.RadiotherapyTreatmentPlanningPersonRoles9555.UID, DicomUID.RadiotherapyTreatmentPlanningPersonRoles9555);
            _uids.Add(DicomUID.RealTimeVideoRenditionTitles7070.UID, DicomUID.RealTimeVideoRenditionTitles7070);
        }

        ///<summary>SOP Class: Verification SOP Class</summary>
        public static readonly DicomUID Verification = new DicomUID("1.2.840.10008.1.1", "Verification SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Transfer Syntax: Implicit VR Little Endian: Default Transfer Syntax for DICOM</summary>
        public static readonly DicomUID ImplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2", "Implicit VR Little Endian: Default Transfer Syntax for DICOM", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Explicit VR Little Endian</summary>
        public static readonly DicomUID ExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1", "Explicit VR Little Endian", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Deflated Explicit VR Little Endian</summary>
        public static readonly DicomUID DeflatedExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1.99", "Deflated Explicit VR Little Endian", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Explicit VR Big Endian (Retired)</summary>
        public static readonly DicomUID ExplicitVRBigEndianRETIRED = new DicomUID("1.2.840.10008.1.2.2", "Explicit VR Big Endian (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression</summary>
        public static readonly DicomUID JPEGBaseline1 = new DicomUID("1.2.840.10008.1.2.4.50", "JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG Extended (Process 2 &amp; 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)</summary>
        public static readonly DicomUID JPEGExtended24 = new DicomUID("1.2.840.10008.1.2.4.51", "JPEG Extended (Process 2 & 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG Extended (Process 3 &amp; 5) (Retired)</summary>
        public static readonly DicomUID JPEGExtended35RETIRED = new DicomUID("1.2.840.10008.1.2.4.52", "JPEG Extended (Process 3 & 5) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Spectral Selection, Non-Hierarchical (Process 6 &amp; 8) (Retired)</summary>
        public static readonly DicomUID JPEGSpectralSelectionNonHierarchical68RETIRED = new DicomUID("1.2.840.10008.1.2.4.53", "JPEG Spectral Selection, Non-Hierarchical (Process 6 & 8) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Spectral Selection, Non-Hierarchical (Process 7 &amp; 9) (Retired)</summary>
        public static readonly DicomUID JPEGSpectralSelectionNonHierarchical79RETIRED = new DicomUID("1.2.840.10008.1.2.4.54", "JPEG Spectral Selection, Non-Hierarchical (Process 7 & 9) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Full Progression, Non-Hierarchical (Process 10 &amp; 12) (Retired)</summary>
        public static readonly DicomUID JPEGFullProgressionNonHierarchical1012RETIRED = new DicomUID("1.2.840.10008.1.2.4.55", "JPEG Full Progression, Non-Hierarchical (Process 10 & 12) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Full Progression, Non-Hierarchical (Process 11 &amp; 13) (Retired)</summary>
        public static readonly DicomUID JPEGFullProgressionNonHierarchical1113RETIRED = new DicomUID("1.2.840.10008.1.2.4.56", "JPEG Full Progression, Non-Hierarchical (Process 11 & 13) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical (Process 14)</summary>
        public static readonly DicomUID JPEGLosslessNonHierarchical14 = new DicomUID("1.2.840.10008.1.2.4.57", "JPEG Lossless, Non-Hierarchical (Process 14)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical (Process 15) (Retired)</summary>
        public static readonly DicomUID JPEGLosslessNonHierarchical15RETIRED = new DicomUID("1.2.840.10008.1.2.4.58", "JPEG Lossless, Non-Hierarchical (Process 15) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Extended, Hierarchical (Process 16 &amp; 18) (Retired)</summary>
        public static readonly DicomUID JPEGExtendedHierarchical1618RETIRED = new DicomUID("1.2.840.10008.1.2.4.59", "JPEG Extended, Hierarchical (Process 16 & 18) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Extended, Hierarchical (Process 17 &amp; 19) (Retired)</summary>
        public static readonly DicomUID JPEGExtendedHierarchical1719RETIRED = new DicomUID("1.2.840.10008.1.2.4.60", "JPEG Extended, Hierarchical (Process 17 & 19) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Spectral Selection, Hierarchical (Process 20 &amp; 22) (Retired)</summary>
        public static readonly DicomUID JPEGSpectralSelectionHierarchical2022RETIRED = new DicomUID("1.2.840.10008.1.2.4.61", "JPEG Spectral Selection, Hierarchical (Process 20 & 22) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Spectral Selection, Hierarchical (Process 21 &amp; 23) (Retired)</summary>
        public static readonly DicomUID JPEGSpectralSelectionHierarchical2123RETIRED = new DicomUID("1.2.840.10008.1.2.4.62", "JPEG Spectral Selection, Hierarchical (Process 21 & 23) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Full Progression, Hierarchical (Process 24 &amp; 26) (Retired)</summary>
        public static readonly DicomUID JPEGFullProgressionHierarchical2426RETIRED = new DicomUID("1.2.840.10008.1.2.4.63", "JPEG Full Progression, Hierarchical (Process 24 & 26) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Full Progression, Hierarchical (Process 25 &amp; 27) (Retired)</summary>
        public static readonly DicomUID JPEGFullProgressionHierarchical2527RETIRED = new DicomUID("1.2.840.10008.1.2.4.64", "JPEG Full Progression, Hierarchical (Process 25 & 27) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Lossless, Hierarchical (Process 28) (Retired)</summary>
        public static readonly DicomUID JPEGLosslessHierarchical28RETIRED = new DicomUID("1.2.840.10008.1.2.4.65", "JPEG Lossless, Hierarchical (Process 28) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Lossless, Hierarchical (Process 29) (Retired)</summary>
        public static readonly DicomUID JPEGLosslessHierarchical29RETIRED = new DicomUID("1.2.840.10008.1.2.4.66", "JPEG Lossless, Hierarchical (Process 29) (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression</summary>
        public static readonly DicomUID JPEGLossless = new DicomUID("1.2.840.10008.1.2.4.70", "JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG-LS Lossless Image Compression</summary>
        public static readonly DicomUID JPEGLSLossless = new DicomUID("1.2.840.10008.1.2.4.80", "JPEG-LS Lossless Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG-LS Lossy (Near-Lossless) Image Compression</summary>
        public static readonly DicomUID JPEGLSLossyNearLossless = new DicomUID("1.2.840.10008.1.2.4.81", "JPEG-LS Lossy (Near-Lossless) Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Image Compression (Lossless Only)</summary>
        public static readonly DicomUID JPEG2000LosslessOnly = new DicomUID("1.2.840.10008.1.2.4.90", "JPEG 2000 Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Image Compression</summary>
        public static readonly DicomUID JPEG2000 = new DicomUID("1.2.840.10008.1.2.4.91", "JPEG 2000 Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)</summary>
        public static readonly DicomUID JPEG2000Part2MultiComponentLosslessOnly = new DicomUID("1.2.840.10008.1.2.4.92", "JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression</summary>
        public static readonly DicomUID JPEG2000Part2MultiComponent = new DicomUID("1.2.840.10008.1.2.4.93", "JPEG 2000 Part 2 Multi-component Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPIP Referenced</summary>
        public static readonly DicomUID JPIPReferenced = new DicomUID("1.2.840.10008.1.2.4.94", "JPIP Referenced", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPIP Referenced Deflate</summary>
        public static readonly DicomUID JPIPReferencedDeflate = new DicomUID("1.2.840.10008.1.2.4.95", "JPIP Referenced Deflate", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG2 Main Profile / Main Level</summary>
        public static readonly DicomUID MPEG2 = new DicomUID("1.2.840.10008.1.2.4.100", "MPEG2 Main Profile / Main Level", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG2 Main Profile / High Level</summary>
        public static readonly DicomUID MPEG2MainProfileHighLevel = new DicomUID("1.2.840.10008.1.2.4.101", "MPEG2 Main Profile / High Level", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 High Profile / Level 4.1</summary>
        public static readonly DicomUID MPEG4AVCH264HighProfileLevel41 = new DicomUID("1.2.840.10008.1.2.4.102", "MPEG-4 AVC/H.264 High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1</summary>
        public static readonly DicomUID MPEG4AVCH264BDCompatibleHighProfileLevel41 = new DicomUID("1.2.840.10008.1.2.4.103", "MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video</summary>
        public static readonly DicomUID MPEG4AVCH264HighProfileLevel42For2DVideo = new DicomUID("1.2.840.10008.1.2.4.104", "MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video</summary>
        public static readonly DicomUID MPEG4AVCH264HighProfileLevel42For3DVideo = new DicomUID("1.2.840.10008.1.2.4.105", "MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2</summary>
        public static readonly DicomUID MPEG4AVCH264StereoHighProfileLevel42 = new DicomUID("1.2.840.10008.1.2.4.106", "MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: HEVC/H.265 Main Profile / Level 5.1</summary>
        public static readonly DicomUID HEVCH265MainProfileLevel51 = new DicomUID("1.2.840.10008.1.2.4.107", "HEVC/H.265 Main Profile / Level 5.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: HEVC/H.265 Main 10 Profile / Level 5.1</summary>
        public static readonly DicomUID HEVCH265Main10ProfileLevel51 = new DicomUID("1.2.840.10008.1.2.4.108", "HEVC/H.265 Main 10 Profile / Level 5.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: RLE Lossless</summary>
        public static readonly DicomUID RLELossless = new DicomUID("1.2.840.10008.1.2.5", "RLE Lossless", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: RFC 2557 MIME encapsulation (Retired)</summary>
        public static readonly DicomUID RFC2557MIMEEncapsulationRETIRED = new DicomUID("1.2.840.10008.1.2.6.1", "RFC 2557 MIME encapsulation (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: XML Encoding (Retired)</summary>
        public static readonly DicomUID XMLEncodingRETIRED = new DicomUID("1.2.840.10008.1.2.6.2", "XML Encoding (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: SMPTE ST 2110-20 Uncompressed Progressive Active Video</summary>
        public static readonly DicomUID SMPTEST211020UncompressedProgressiveActiveVideo = new DicomUID("1.2.840.10008.1.2.7.1", "SMPTE ST 2110-20 Uncompressed Progressive Active Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: SMPTE ST 2110-20 Uncompressed Interlaced Active Video</summary>
        public static readonly DicomUID SMPTEST211020UncompressedInterlacedActiveVideo = new DicomUID("1.2.840.10008.1.2.7.2", "SMPTE ST 2110-20 Uncompressed Interlaced Active Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: SMPTE ST 2110-30 PCM Digital Audio</summary>
        public static readonly DicomUID SMPTEST211030PCMDigitalAudio = new DicomUID("1.2.840.10008.1.2.7.3", "SMPTE ST 2110-30 PCM Digital Audio", DicomUidType.TransferSyntax, false);

        ///<summary>SOP Class: Media Storage Directory Storage</summary>
        public static readonly DicomUID MediaStorageDirectoryStorage = new DicomUID("1.2.840.10008.1.3.10", "Media Storage Directory Storage", DicomUidType.SOPClass, false);

        ///<summary>Well-known frame of reference: Talairach Brain Atlas Frame of Reference</summary>
        public static readonly DicomUID TalairachBrainAtlasFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.1", "Talairach Brain Atlas Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 T1 Frame of Reference</summary>
        public static readonly DicomUID SPM2T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.2", "SPM2 T1 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 T2 Frame of Reference</summary>
        public static readonly DicomUID SPM2T2FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.3", "SPM2 T2 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 PD Frame of Reference</summary>
        public static readonly DicomUID SPM2PDFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.4", "SPM2 PD Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 EPI Frame of Reference</summary>
        public static readonly DicomUID SPM2EPIFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.5", "SPM2 EPI Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 FIL T1 Frame of Reference</summary>
        public static readonly DicomUID SPM2FILT1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.6", "SPM2 FIL T1 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 PET Frame of Reference</summary>
        public static readonly DicomUID SPM2PETFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.7", "SPM2 PET Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 TRANSM Frame of Reference</summary>
        public static readonly DicomUID SPM2TRANSMFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.8", "SPM2 TRANSM Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 SPECT Frame of Reference</summary>
        public static readonly DicomUID SPM2SPECTFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.9", "SPM2 SPECT Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 GRAY Frame of Reference</summary>
        public static readonly DicomUID SPM2GRAYFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.10", "SPM2 GRAY Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 WHITE Frame of Reference</summary>
        public static readonly DicomUID SPM2WHITEFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.11", "SPM2 WHITE Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 CSF Frame of Reference</summary>
        public static readonly DicomUID SPM2CSFFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.12", "SPM2 CSF Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 BRAINMASK Frame of Reference</summary>
        public static readonly DicomUID SPM2BRAINMASKFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.13", "SPM2 BRAINMASK Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 AVG305T1 Frame of Reference</summary>
        public static readonly DicomUID SPM2AVG305T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.14", "SPM2 AVG305T1 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 AVG152T1 Frame of Reference</summary>
        public static readonly DicomUID SPM2AVG152T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.15", "SPM2 AVG152T1 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 AVG152T2 Frame of Reference</summary>
        public static readonly DicomUID SPM2AVG152T2FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.16", "SPM2 AVG152T2 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 AVG152PD Frame of Reference</summary>
        public static readonly DicomUID SPM2AVG152PDFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.17", "SPM2 AVG152PD Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: SPM2 SINGLESUBJT1 Frame of Reference</summary>
        public static readonly DicomUID SPM2SINGLESUBJT1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.18", "SPM2 SINGLESUBJT1 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: ICBM 452 T1 Frame of Reference</summary>
        public static readonly DicomUID ICBM452T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.2.1", "ICBM 452 T1 Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: ICBM Single Subject MRI Frame of Reference</summary>
        public static readonly DicomUID ICBMSingleSubjectMRIFrameOfReference = new DicomUID("1.2.840.10008.1.4.2.2", "ICBM Single Subject MRI Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known frame of reference: IEC 61217 Fixed Coordinate System Frame of Reference</summary>
        public static readonly DicomUID IEC61217FixedCoordinateSystemFrameOfReference = new DicomUID("1.2.840.10008.1.4.3.1", "IEC 61217 Fixed Coordinate System Frame of Reference", DicomUidType.FrameOfReference, false);

        ///<summary>Well-known SOP Instance: Hot Iron Color Palette SOP Instance</summary>
        public static readonly DicomUID HotIronColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.1", "Hot Iron Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: PET Color Palette SOP Instance</summary>
        public static readonly DicomUID PETColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.2", "PET Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Hot Metal Blue Color Palette SOP Instance</summary>
        public static readonly DicomUID HotMetalBlueColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.3", "Hot Metal Blue Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: PET 20 Step Color Palette SOP Instance</summary>
        public static readonly DicomUID PET20StepColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.4", "PET 20 Step Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Spring Color Palette SOP Instance</summary>
        public static readonly DicomUID SpringColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.5", "Spring Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Summer Color Palette SOP Instance</summary>
        public static readonly DicomUID SummerColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.6", "Summer Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Fall Color Palette SOP Instance</summary>
        public static readonly DicomUID FallColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.7", "Fall Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Winter Color Palette SOP Instance</summary>
        public static readonly DicomUID WinterColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.8", "Winter Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>SOP Class: Basic Study Content Notification SOP Class (Retired)</summary>
        public static readonly DicomUID BasicStudyContentNotificationSOPClassRETIRED = new DicomUID("1.2.840.10008.1.9", "Basic Study Content Notification SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Transfer Syntax: Papyrus 3 Implicit VR Little Endian (Retired)</summary>
        public static readonly DicomUID Papyrus3ImplicitVRLittleEndianRETIRED = new DicomUID("1.2.840.10008.1.20", "Papyrus 3 Implicit VR Little Endian (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>SOP Class: Storage Commitment Push Model SOP Class</summary>
        public static readonly DicomUID StorageCommitmentPushModelSOPClass = new DicomUID("1.2.840.10008.1.20.1", "Storage Commitment Push Model SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Storage Commitment Push Model SOP Instance</summary>
        public static readonly DicomUID StorageCommitmentPushModelSOPInstance = new DicomUID("1.2.840.10008.1.20.1.1", "Storage Commitment Push Model SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>SOP Class: Storage Commitment Pull Model SOP Class (Retired)</summary>
        public static readonly DicomUID StorageCommitmentPullModelSOPClassRETIRED = new DicomUID("1.2.840.10008.1.20.2", "Storage Commitment Pull Model SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Well-known SOP Instance: Storage Commitment Pull Model SOP Instance (Retired)</summary>
        public static readonly DicomUID StorageCommitmentPullModelSOPInstanceRETIRED = new DicomUID("1.2.840.10008.1.20.2.1", "Storage Commitment Pull Model SOP Instance (Retired)", DicomUidType.SOPInstance, true);

        ///<summary>SOP Class: Procedural Event Logging SOP Class</summary>
        public static readonly DicomUID ProceduralEventLoggingSOPClass = new DicomUID("1.2.840.10008.1.40", "Procedural Event Logging SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Procedural Event Logging SOP Instance</summary>
        public static readonly DicomUID ProceduralEventLoggingSOPInstance = new DicomUID("1.2.840.10008.1.40.1", "Procedural Event Logging SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>SOP Class: Substance Administration Logging SOP Class</summary>
        public static readonly DicomUID SubstanceAdministrationLoggingSOPClass = new DicomUID("1.2.840.10008.1.42", "Substance Administration Logging SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Substance Administration Logging SOP Instance</summary>
        public static readonly DicomUID SubstanceAdministrationLoggingSOPInstance = new DicomUID("1.2.840.10008.1.42.1", "Substance Administration Logging SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>DICOM UIDs as a Coding Scheme: DICOM UID Registry</summary>
        public static readonly DicomUID DICOMUIDRegistry = new DicomUID("1.2.840.10008.2.6.1", "DICOM UID Registry", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: DICOM Controlled Terminology</summary>
        public static readonly DicomUID DICOMControlledTerminology = new DicomUID("1.2.840.10008.2.16.4", "DICOM Controlled Terminology", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Adult Mouse Anatomy Ontology</summary>
        public static readonly DicomUID AdultMouseAnatomyOntology = new DicomUID("1.2.840.10008.2.16.5", "Adult Mouse Anatomy Ontology", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Uberon Ontology</summary>
        public static readonly DicomUID UberonOntology = new DicomUID("1.2.840.10008.2.16.6", "Uberon Ontology", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Integrated Taxonomic Information System (ITIS) Taxonomic Serial Number (TSN)</summary>
        public static readonly DicomUID IntegratedTaxonomicInformationSystemITISTaxonomicSerialNumberTSN = new DicomUID("1.2.840.10008.2.16.7", "Integrated Taxonomic Information System (ITIS) Taxonomic Serial Number (TSN)", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Mouse Genome Initiative (MGI)</summary>
        public static readonly DicomUID MouseGenomeInitiativeMGI = new DicomUID("1.2.840.10008.2.16.8", "Mouse Genome Initiative (MGI)", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: PubChem Compound CID</summary>
        public static readonly DicomUID PubChemCompoundCID = new DicomUID("1.2.840.10008.2.16.9", "PubChem Compound CID", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: ICD-11</summary>
        public static readonly DicomUID ICD11 = new DicomUID("1.2.840.10008.2.16.10", "ICD-11", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: New York University Melanoma Clinical Cooperative Group</summary>
        public static readonly DicomUID NewYorkUniversityMelanomaClinicalCooperativeGroup = new DicomUID("1.2.840.10008.2.16.11", "New York University Melanoma Clinical Cooperative Group", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Mayo Clinic Non-radiological Images Specific Body Structure Anatomical Surface Region Guide</summary>
        public static readonly DicomUID MayoClinicNonRadiologicalImagesSpecificBodyStructureAnatomicalSurfaceRegionGuide = new DicomUID("1.2.840.10008.2.16.12", "Mayo Clinic Non-radiological Images Specific Body Structure Anatomical Surface Region Guide", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Image Biomarker Standardisation Initiative</summary>
        public static readonly DicomUID ImageBiomarkerStandardisationInitiative = new DicomUID("1.2.840.10008.2.16.13", "Image Biomarker Standardisation Initiative", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Radiomics Ontology</summary>
        public static readonly DicomUID RadiomicsOntology = new DicomUID("1.2.840.10008.2.16.14", "Radiomics Ontology", DicomUidType.CodingScheme, false);

        ///<summary>Application Context Name: DICOM Application Context Name</summary>
        public static readonly DicomUID DICOMApplicationContextName = new DicomUID("1.2.840.10008.3.1.1.1", "DICOM Application Context Name", DicomUidType.ApplicationContextName, false);

        ///<summary>SOP Class: Detached Patient Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedPatientManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.1", "Detached Patient Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Detached Patient Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedPatientManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.4", "Detached Patient Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Detached Visit Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedVisitManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.2.1", "Detached Visit Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Detached Study Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedStudyManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.1", "Detached Study Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Study Component Management SOP Class (Retired)</summary>
        public static readonly DicomUID StudyComponentManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.2", "Study Component Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Modality Performed Procedure Step SOP Class</summary>
        public static readonly DicomUID ModalityPerformedProcedureStepSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.3", "Modality Performed Procedure Step SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Modality Performed Procedure Step Retrieve SOP Class</summary>
        public static readonly DicomUID ModalityPerformedProcedureStepRetrieveSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.4", "Modality Performed Procedure Step Retrieve SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Modality Performed Procedure Step Notification SOP Class</summary>
        public static readonly DicomUID ModalityPerformedProcedureStepNotificationSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.5", "Modality Performed Procedure Step Notification SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Detached Results Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedResultsManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.1", "Detached Results Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Detached Results Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedResultsManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.4", "Detached Results Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>Meta SOP Class: Detached Study Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedStudyManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.5", "Detached Study Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Detached Interpretation Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedInterpretationManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.6.1", "Detached Interpretation Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Service Class: Storage Service Class</summary>
        public static readonly DicomUID StorageServiceClass = new DicomUID("1.2.840.10008.4.2", "Storage Service Class", DicomUidType.ServiceClass, false);

        ///<summary>SOP Class: Basic Film Session SOP Class</summary>
        public static readonly DicomUID BasicFilmSessionSOPClass = new DicomUID("1.2.840.10008.5.1.1.1", "Basic Film Session SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Film Box SOP Class</summary>
        public static readonly DicomUID BasicFilmBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.2", "Basic Film Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Grayscale Image Box SOP Class</summary>
        public static readonly DicomUID BasicGrayscaleImageBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.4", "Basic Grayscale Image Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Color Image Box SOP Class</summary>
        public static readonly DicomUID BasicColorImageBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.4.1", "Basic Color Image Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Referenced Image Box SOP Class (Retired)</summary>
        public static readonly DicomUID ReferencedImageBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.4.2", "Referenced Image Box SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Basic Grayscale Print Management Meta SOP Class</summary>
        public static readonly DicomUID BasicGrayscalePrintManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.1.9", "Basic Grayscale Print Management Meta SOP Class", DicomUidType.MetaSOPClass, false);

        ///<summary>Meta SOP Class: Referenced Grayscale Print Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID ReferencedGrayscalePrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.9.1", "Referenced Grayscale Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Print Job SOP Class</summary>
        public static readonly DicomUID PrintJobSOPClass = new DicomUID("1.2.840.10008.5.1.1.14", "Print Job SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Annotation Box SOP Class</summary>
        public static readonly DicomUID BasicAnnotationBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.15", "Basic Annotation Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Printer SOP Class</summary>
        public static readonly DicomUID PrinterSOPClass = new DicomUID("1.2.840.10008.5.1.1.16", "Printer SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Printer Configuration Retrieval SOP Class</summary>
        public static readonly DicomUID PrinterConfigurationRetrievalSOPClass = new DicomUID("1.2.840.10008.5.1.1.16.376", "Printer Configuration Retrieval SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known Printer SOP Instance: Printer SOP Instance</summary>
        public static readonly DicomUID PrinterSOPInstance = new DicomUID("1.2.840.10008.5.1.1.17", "Printer SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known Printer SOP Instance: Printer Configuration Retrieval SOP Instance</summary>
        public static readonly DicomUID PrinterConfigurationRetrievalSOPInstance = new DicomUID("1.2.840.10008.5.1.1.17.376", "Printer Configuration Retrieval SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Meta SOP Class: Basic Color Print Management Meta SOP Class</summary>
        public static readonly DicomUID BasicColorPrintManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.1.18", "Basic Color Print Management Meta SOP Class", DicomUidType.MetaSOPClass, false);

        ///<summary>Meta SOP Class: Referenced Color Print Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID ReferencedColorPrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.18.1", "Referenced Color Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: VOI LUT Box SOP Class</summary>
        public static readonly DicomUID VOILUTBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.22", "VOI LUT Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Presentation LUT SOP Class</summary>
        public static readonly DicomUID PresentationLUTSOPClass = new DicomUID("1.2.840.10008.5.1.1.23", "Presentation LUT SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Image Overlay Box SOP Class (Retired)</summary>
        public static readonly DicomUID ImageOverlayBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.24", "Image Overlay Box SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Basic Print Image Overlay Box SOP Class (Retired)</summary>
        public static readonly DicomUID BasicPrintImageOverlayBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.24.1", "Basic Print Image Overlay Box SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Well-known Print Queue SOP Instance: Print Queue SOP Instance (Retired)</summary>
        public static readonly DicomUID PrintQueueSOPInstanceRETIRED = new DicomUID("1.2.840.10008.5.1.1.25", "Print Queue SOP Instance (Retired)", DicomUidType.SOPInstance, true);

        ///<summary>SOP Class: Print Queue Management SOP Class (Retired)</summary>
        public static readonly DicomUID PrintQueueManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.26", "Print Queue Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Stored Print Storage SOP Class (Retired)</summary>
        public static readonly DicomUID StoredPrintStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.27", "Stored Print Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Hardcopy Grayscale Image Storage SOP Class (Retired)</summary>
        public static readonly DicomUID HardcopyGrayscaleImageStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.29", "Hardcopy Grayscale Image Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Hardcopy Color Image Storage SOP Class (Retired)</summary>
        public static readonly DicomUID HardcopyColorImageStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.30", "Hardcopy Color Image Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Pull Print Request SOP Class (Retired)</summary>
        public static readonly DicomUID PullPrintRequestSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.31", "Pull Print Request SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Pull Stored Print Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID PullStoredPrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.32", "Pull Stored Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Media Creation Management SOP Class UID</summary>
        public static readonly DicomUID MediaCreationManagementSOPClassUID = new DicomUID("1.2.840.10008.5.1.1.33", "Media Creation Management SOP Class UID", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Display System SOP Class</summary>
        public static readonly DicomUID DisplaySystemSOPClass = new DicomUID("1.2.840.10008.5.1.1.40", "Display System SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Display System SOP Instance</summary>
        public static readonly DicomUID DisplaySystemSOPInstance = new DicomUID("1.2.840.10008.5.1.1.40.1", "Display System SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>SOP Class: Computed Radiography Image Storage</summary>
        public static readonly DicomUID ComputedRadiographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.1", "Computed Radiography Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Digital X-Ray Image Storage - For Presentation</summary>
        public static readonly DicomUID DigitalXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.1", "Digital X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Digital X-Ray Image Storage - For Processing</summary>
        public static readonly DicomUID DigitalXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.1.1", "Digital X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Digital Mammography X-Ray Image Storage - For Presentation</summary>
        public static readonly DicomUID DigitalMammographyXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.2", "Digital Mammography X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Digital Mammography X-Ray Image Storage - For Processing</summary>
        public static readonly DicomUID DigitalMammographyXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.2.1", "Digital Mammography X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Digital Intra-Oral X-Ray Image Storage - For Presentation</summary>
        public static readonly DicomUID DigitalIntraOralXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.3", "Digital Intra-Oral X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Digital Intra-Oral X-Ray Image Storage - For Processing</summary>
        public static readonly DicomUID DigitalIntraOralXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.3.1", "Digital Intra-Oral X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: CT Image Storage</summary>
        public static readonly DicomUID CTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.2", "CT Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced CT Image Storage</summary>
        public static readonly DicomUID EnhancedCTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.2.1", "Enhanced CT Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Legacy Converted Enhanced CT Image Storage</summary>
        public static readonly DicomUID LegacyConvertedEnhancedCTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.2.2", "Legacy Converted Enhanced CT Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ultrasound Multi-frame Image Storage (Retired)</summary>
        public static readonly DicomUID UltrasoundMultiFrameImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.3", "Ultrasound Multi-frame Image Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Ultrasound Multi-frame Image Storage</summary>
        public static readonly DicomUID UltrasoundMultiFrameImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.3.1", "Ultrasound Multi-frame Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: MR Image Storage</summary>
        public static readonly DicomUID MRImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4", "MR Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced MR Image Storage</summary>
        public static readonly DicomUID EnhancedMRImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.1", "Enhanced MR Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: MR Spectroscopy Storage</summary>
        public static readonly DicomUID MRSpectroscopyStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.2", "MR Spectroscopy Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced MR Color Image Storage</summary>
        public static readonly DicomUID EnhancedMRColorImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.3", "Enhanced MR Color Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Legacy Converted Enhanced MR Image Storage</summary>
        public static readonly DicomUID LegacyConvertedEnhancedMRImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.4", "Legacy Converted Enhanced MR Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Nuclear Medicine Image Storage (Retired)</summary>
        public static readonly DicomUID NuclearMedicineImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.5", "Nuclear Medicine Image Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Ultrasound Image Storage (Retired)</summary>
        public static readonly DicomUID UltrasoundImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.6", "Ultrasound Image Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Ultrasound Image Storage</summary>
        public static readonly DicomUID UltrasoundImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.1", "Ultrasound Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced US Volume Storage</summary>
        public static readonly DicomUID EnhancedUSVolumeStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.2", "Enhanced US Volume Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Secondary Capture Image Storage</summary>
        public static readonly DicomUID SecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7", "Secondary Capture Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Multi-frame Single Bit Secondary Capture Image Storage</summary>
        public static readonly DicomUID MultiFrameSingleBitSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.1", "Multi-frame Single Bit Secondary Capture Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Multi-frame Grayscale Byte Secondary Capture Image Storage</summary>
        public static readonly DicomUID MultiFrameGrayscaleByteSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.2", "Multi-frame Grayscale Byte Secondary Capture Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Multi-frame Grayscale Word Secondary Capture Image Storage</summary>
        public static readonly DicomUID MultiFrameGrayscaleWordSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.3", "Multi-frame Grayscale Word Secondary Capture Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Multi-frame True Color Secondary Capture Image Storage</summary>
        public static readonly DicomUID MultiFrameTrueColorSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.4", "Multi-frame True Color Secondary Capture Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Standalone Overlay Storage (Retired)</summary>
        public static readonly DicomUID StandaloneOverlayStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.8", "Standalone Overlay Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Standalone Curve Storage (Retired)</summary>
        public static readonly DicomUID StandaloneCurveStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.9", "Standalone Curve Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Waveform Storage - Trial (Retired)</summary>
        public static readonly DicomUID WaveformStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1", "Waveform Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: 12-lead ECG Waveform Storage</summary>
        public static readonly DicomUID TwelveLeadECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.1", "12-lead ECG Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: General ECG Waveform Storage</summary>
        public static readonly DicomUID GeneralECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.2", "General ECG Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ambulatory ECG Waveform Storage</summary>
        public static readonly DicomUID AmbulatoryECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.3", "Ambulatory ECG Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Hemodynamic Waveform Storage</summary>
        public static readonly DicomUID HemodynamicWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.2.1", "Hemodynamic Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Cardiac Electrophysiology Waveform Storage</summary>
        public static readonly DicomUID CardiacElectrophysiologyWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.3.1", "Cardiac Electrophysiology Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Voice Audio Waveform Storage</summary>
        public static readonly DicomUID BasicVoiceAudioWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.4.1", "Basic Voice Audio Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: General Audio Waveform Storage</summary>
        public static readonly DicomUID GeneralAudioWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.4.2", "General Audio Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Arterial Pulse Waveform Storage</summary>
        public static readonly DicomUID ArterialPulseWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.5.1", "Arterial Pulse Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Respiratory Waveform Storage</summary>
        public static readonly DicomUID RespiratoryWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.6.1", "Respiratory Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Standalone Modality LUT Storage (Retired)</summary>
        public static readonly DicomUID StandaloneModalityLUTStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.10", "Standalone Modality LUT Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Standalone VOI LUT Storage (Retired)</summary>
        public static readonly DicomUID StandaloneVOILUTStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.11", "Standalone VOI LUT Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Grayscale Softcopy Presentation State Storage</summary>
        public static readonly DicomUID GrayscaleSoftcopyPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.1", "Grayscale Softcopy Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Softcopy Presentation State Storage</summary>
        public static readonly DicomUID ColorSoftcopyPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.2", "Color Softcopy Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Pseudo-Color Softcopy Presentation State Storage</summary>
        public static readonly DicomUID PseudoColorSoftcopyPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.3", "Pseudo-Color Softcopy Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Blending Softcopy Presentation State Storage</summary>
        public static readonly DicomUID BlendingSoftcopyPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.4", "Blending Softcopy Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: XA/XRF Grayscale Softcopy Presentation State Storage</summary>
        public static readonly DicomUID XAXRFGrayscaleSoftcopyPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.5", "XA/XRF Grayscale Softcopy Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Grayscale Planar MPR Volumetric Presentation State Storage</summary>
        public static readonly DicomUID GrayscalePlanarMPRVolumetricPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.6", "Grayscale Planar MPR Volumetric Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Compositing Planar MPR Volumetric Presentation State Storage</summary>
        public static readonly DicomUID CompositingPlanarMPRVolumetricPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.7", "Compositing Planar MPR Volumetric Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Advanced Blending Presentation State Storage</summary>
        public static readonly DicomUID AdvancedBlendingPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.8", "Advanced Blending Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Volume Rendering Volumetric Presentation State Storage</summary>
        public static readonly DicomUID VolumeRenderingVolumetricPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.9", "Volume Rendering Volumetric Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Segmented Volume Rendering Volumetric Presentation State Storage</summary>
        public static readonly DicomUID SegmentedVolumeRenderingVolumetricPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.10", "Segmented Volume Rendering Volumetric Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Multiple Volume Rendering Volumetric Presentation State Storage</summary>
        public static readonly DicomUID MultipleVolumeRenderingVolumetricPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.11", "Multiple Volume Rendering Volumetric Presentation State Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: X-Ray Angiographic Image Storage</summary>
        public static readonly DicomUID XRayAngiographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.1", "X-Ray Angiographic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced XA Image Storage</summary>
        public static readonly DicomUID EnhancedXAImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.1.1", "Enhanced XA Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: X-Ray Radiofluoroscopic Image Storage</summary>
        public static readonly DicomUID XRayRadiofluoroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.2", "X-Ray Radiofluoroscopic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced XRF Image Storage</summary>
        public static readonly DicomUID EnhancedXRFImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.2.1", "Enhanced XRF Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: X-Ray Angiographic Bi-Plane Image Storage (Retired)</summary>
        public static readonly DicomUID XRayAngiographicBiPlaneImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.12.3", "X-Ray Angiographic Bi-Plane Image Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: (Retired)</summary>
        public static readonly DicomUID UID_1_2_840_10008_5_1_4_1_1_12_77RETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.12.77", "(Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: X-Ray 3D Angiographic Image Storage</summary>
        public static readonly DicomUID XRay3DAngiographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.1", "X-Ray 3D Angiographic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: X-Ray 3D Craniofacial Image Storage</summary>
        public static readonly DicomUID XRay3DCraniofacialImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.2", "X-Ray 3D Craniofacial Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Breast Tomosynthesis Image Storage</summary>
        public static readonly DicomUID BreastTomosynthesisImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.3", "Breast Tomosynthesis Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Breast Projection X-Ray Image Storage - For Presentation</summary>
        public static readonly DicomUID BreastProjectionXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.4", "Breast Projection X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Breast Projection X-Ray Image Storage - For Processing</summary>
        public static readonly DicomUID BreastProjectionXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.5", "Breast Projection X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Intravascular Optical Coherence Tomography Image Storage - For Presentation</summary>
        public static readonly DicomUID IntravascularOpticalCoherenceTomographyImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.14.1", "Intravascular Optical Coherence Tomography Image Storage - For Presentation", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Intravascular Optical Coherence Tomography Image Storage - For Processing</summary>
        public static readonly DicomUID IntravascularOpticalCoherenceTomographyImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.14.2", "Intravascular Optical Coherence Tomography Image Storage - For Processing", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Nuclear Medicine Image Storage</summary>
        public static readonly DicomUID NuclearMedicineImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.20", "Nuclear Medicine Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Parametric Map Storage</summary>
        public static readonly DicomUID ParametricMapStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.30", "Parametric Map Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: (Retired)</summary>
        public static readonly DicomUID UID_1_2_840_10008_5_1_4_1_1_40RETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.40", "(Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Raw Data Storage</summary>
        public static readonly DicomUID RawDataStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66", "Raw Data Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Spatial Registration Storage</summary>
        public static readonly DicomUID SpatialRegistrationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.1", "Spatial Registration Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Spatial Fiducials Storage</summary>
        public static readonly DicomUID SpatialFiducialsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.2", "Spatial Fiducials Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Deformable Spatial Registration Storage</summary>
        public static readonly DicomUID DeformableSpatialRegistrationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.3", "Deformable Spatial Registration Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Segmentation Storage</summary>
        public static readonly DicomUID SegmentationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.4", "Segmentation Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Surface Segmentation Storage</summary>
        public static readonly DicomUID SurfaceSegmentationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.5", "Surface Segmentation Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Tractography Results Storage</summary>
        public static readonly DicomUID TractographyResultsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.6", "Tractography Results Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Real World Value Mapping Storage</summary>
        public static readonly DicomUID RealWorldValueMappingStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.67", "Real World Value Mapping Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Surface Scan Mesh Storage</summary>
        public static readonly DicomUID SurfaceScanMeshStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.68.1", "Surface Scan Mesh Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Surface Scan Point Cloud Storage</summary>
        public static readonly DicomUID SurfaceScanPointCloudStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.68.2", "Surface Scan Point Cloud Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: VL Image Storage - Trial (Retired)</summary>
        public static readonly DicomUID VLImageStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1", "VL Image Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: VL Multi-frame Image Storage - Trial (Retired)</summary>
        public static readonly DicomUID VLMultiFrameImageStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.77.2", "VL Multi-frame Image Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: VL Endoscopic Image Storage</summary>
        public static readonly DicomUID VLEndoscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.1", "VL Endoscopic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Video Endoscopic Image Storage</summary>
        public static readonly DicomUID VideoEndoscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.1.1", "Video Endoscopic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: VL Microscopic Image Storage</summary>
        public static readonly DicomUID VLMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.2", "VL Microscopic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Video Microscopic Image Storage</summary>
        public static readonly DicomUID VideoMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.2.1", "Video Microscopic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: VL Slide-Coordinates Microscopic Image Storage</summary>
        public static readonly DicomUID VLSlideCoordinatesMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.3", "VL Slide-Coordinates Microscopic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: VL Photographic Image Storage</summary>
        public static readonly DicomUID VLPhotographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.4", "VL Photographic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Video Photographic Image Storage</summary>
        public static readonly DicomUID VideoPhotographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.4.1", "Video Photographic Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Photography 8 Bit Image Storage</summary>
        public static readonly DicomUID OphthalmicPhotography8BitImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.1", "Ophthalmic Photography 8 Bit Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Photography 16 Bit Image Storage</summary>
        public static readonly DicomUID OphthalmicPhotography16BitImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.2", "Ophthalmic Photography 16 Bit Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Stereometric Relationship Storage</summary>
        public static readonly DicomUID StereometricRelationshipStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.3", "Stereometric Relationship Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Tomography Image Storage</summary>
        public static readonly DicomUID OphthalmicTomographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.4", "Ophthalmic Tomography Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Wide Field Ophthalmic Photography Stereographic Projection Image Storage</summary>
        public static readonly DicomUID WideFieldOphthalmicPhotographyStereographicProjectionImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.5", "Wide Field Ophthalmic Photography Stereographic Projection Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Wide Field Ophthalmic Photography 3D Coordinates Image Storage</summary>
        public static readonly DicomUID WideFieldOphthalmicPhotography3DCoordinatesImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.6", "Wide Field Ophthalmic Photography 3D Coordinates Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Optical Coherence Tomography En Face Image Storage</summary>
        public static readonly DicomUID OphthalmicOpticalCoherenceTomographyEnFaceImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.7", "Ophthalmic Optical Coherence Tomography En Face Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Optical Coherence Tomography B-scan Volume Analysis Storage</summary>
        public static readonly DicomUID OphthalmicOpticalCoherenceTomographyBScanVolumeAnalysisStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.8", "Ophthalmic Optical Coherence Tomography B-scan Volume Analysis Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: VL Whole Slide Microscopy Image Storage</summary>
        public static readonly DicomUID VLWholeSlideMicroscopyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.6", "VL Whole Slide Microscopy Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Lensometry Measurements Storage</summary>
        public static readonly DicomUID LensometryMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.1", "Lensometry Measurements Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Autorefraction Measurements Storage</summary>
        public static readonly DicomUID AutorefractionMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.2", "Autorefraction Measurements Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Keratometry Measurements Storage</summary>
        public static readonly DicomUID KeratometryMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.3", "Keratometry Measurements Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Subjective Refraction Measurements Storage</summary>
        public static readonly DicomUID SubjectiveRefractionMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.4", "Subjective Refraction Measurements Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Visual Acuity Measurements Storage</summary>
        public static readonly DicomUID VisualAcuityMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.5", "Visual Acuity Measurements Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Spectacle Prescription Report Storage</summary>
        public static readonly DicomUID SpectaclePrescriptionReportStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.6", "Spectacle Prescription Report Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Axial Measurements Storage</summary>
        public static readonly DicomUID OphthalmicAxialMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.7", "Ophthalmic Axial Measurements Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Intraocular Lens Calculations Storage</summary>
        public static readonly DicomUID IntraocularLensCalculationsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.8", "Intraocular Lens Calculations Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Macular Grid Thickness and Volume Report Storage</summary>
        public static readonly DicomUID MacularGridThicknessAndVolumeReportStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.79.1", "Macular Grid Thickness and Volume Report Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Visual Field Static Perimetry Measurements Storage</summary>
        public static readonly DicomUID OphthalmicVisualFieldStaticPerimetryMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.80.1", "Ophthalmic Visual Field Static Perimetry Measurements Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Ophthalmic Thickness Map Storage</summary>
        public static readonly DicomUID OphthalmicThicknessMapStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.81.1", "Ophthalmic Thickness Map Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Corneal Topography Map Storage</summary>
        public static readonly DicomUID CornealTopographyMapStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.82.1", "Corneal Topography Map Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Text SR Storage - Trial (Retired)</summary>
        public static readonly DicomUID TextSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.1", "Text SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Audio SR Storage - Trial (Retired)</summary>
        public static readonly DicomUID AudioSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.2", "Audio SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Detail SR Storage - Trial (Retired)</summary>
        public static readonly DicomUID DetailSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.3", "Detail SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Comprehensive SR Storage - Trial (Retired)</summary>
        public static readonly DicomUID ComprehensiveSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.4", "Comprehensive SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Basic Text SR Storage</summary>
        public static readonly DicomUID BasicTextSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.11", "Basic Text SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced SR Storage</summary>
        public static readonly DicomUID EnhancedSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.22", "Enhanced SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Comprehensive SR Storage</summary>
        public static readonly DicomUID ComprehensiveSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.33", "Comprehensive SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Comprehensive 3D SR Storage</summary>
        public static readonly DicomUID Comprehensive3DSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.34", "Comprehensive 3D SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Extensible SR Storage</summary>
        public static readonly DicomUID ExtensibleSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.35", "Extensible SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Procedure Log Storage</summary>
        public static readonly DicomUID ProcedureLogStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.40", "Procedure Log Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Mammography CAD SR Storage</summary>
        public static readonly DicomUID MammographyCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.50", "Mammography CAD SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Key Object Selection Document Storage</summary>
        public static readonly DicomUID KeyObjectSelectionDocumentStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.59", "Key Object Selection Document Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Chest CAD SR Storage</summary>
        public static readonly DicomUID ChestCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.65", "Chest CAD SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: X-Ray Radiation Dose SR Storage</summary>
        public static readonly DicomUID XRayRadiationDoseSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.67", "X-Ray Radiation Dose SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Radiopharmaceutical Radiation Dose SR Storage</summary>
        public static readonly DicomUID RadiopharmaceuticalRadiationDoseSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.68", "Radiopharmaceutical Radiation Dose SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Colon CAD SR Storage</summary>
        public static readonly DicomUID ColonCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.69", "Colon CAD SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implantation Plan SR Storage</summary>
        public static readonly DicomUID ImplantationPlanSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.70", "Implantation Plan SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Acquisition Context SR Storage</summary>
        public static readonly DicomUID AcquisitionContextSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.71", "Acquisition Context SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Simplified Adult Echo SR Storage</summary>
        public static readonly DicomUID SimplifiedAdultEchoSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.72", "Simplified Adult Echo SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient Radiation Dose SR Storage</summary>
        public static readonly DicomUID PatientRadiationDoseSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.73", "Patient Radiation Dose SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Planned Imaging Agent Administration SR Storage</summary>
        public static readonly DicomUID PlannedImagingAgentAdministrationSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.74", "Planned Imaging Agent Administration SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Performed Imaging Agent Administration SR Storage</summary>
        public static readonly DicomUID PerformedImagingAgentAdministrationSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.75", "Performed Imaging Agent Administration SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Content Assessment Results Storage</summary>
        public static readonly DicomUID ContentAssessmentResultsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.90.1", "Content Assessment Results Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated PDF Storage</summary>
        public static readonly DicomUID EncapsulatedPDFStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.1", "Encapsulated PDF Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated CDA Storage</summary>
        public static readonly DicomUID EncapsulatedCDAStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.2", "Encapsulated CDA Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated STL Storage</summary>
        public static readonly DicomUID EncapsulatedSTLStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.3", "Encapsulated STL Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Positron Emission Tomography Image Storage</summary>
        public static readonly DicomUID PositronEmissionTomographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.128", "Positron Emission Tomography Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Legacy Converted Enhanced PET Image Storage</summary>
        public static readonly DicomUID LegacyConvertedEnhancedPETImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.128.1", "Legacy Converted Enhanced PET Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Standalone PET Curve Storage (Retired)</summary>
        public static readonly DicomUID StandalonePETCurveStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.129", "Standalone PET Curve Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Enhanced PET Image Storage</summary>
        public static readonly DicomUID EnhancedPETImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.130", "Enhanced PET Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Structured Display Storage</summary>
        public static readonly DicomUID BasicStructuredDisplayStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.131", "Basic Structured Display Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: CT Defined Procedure Protocol Storage</summary>
        public static readonly DicomUID CTDefinedProcedureProtocolStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.200.1", "CT Defined Procedure Protocol Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: CT Performed Procedure Protocol Storage</summary>
        public static readonly DicomUID CTPerformedProcedureProtocolStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.200.2", "CT Performed Procedure Protocol Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Protocol Approval Storage</summary>
        public static readonly DicomUID ProtocolApprovalStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.200.3", "Protocol Approval Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Protocol Approval Information Model - FIND</summary>
        public static readonly DicomUID ProtocolApprovalInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.1.1.200.4", "Protocol Approval Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Protocol Approval Information Model - MOVE</summary>
        public static readonly DicomUID ProtocolApprovalInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.1.1.200.5", "Protocol Approval Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Protocol Approval Information Model - GET</summary>
        public static readonly DicomUID ProtocolApprovalInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.1.1.200.6", "Protocol Approval Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Image Storage</summary>
        public static readonly DicomUID RTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.1", "RT Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Dose Storage</summary>
        public static readonly DicomUID RTDoseStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.2", "RT Dose Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Structure Set Storage</summary>
        public static readonly DicomUID RTStructureSetStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.3", "RT Structure Set Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Beams Treatment Record Storage</summary>
        public static readonly DicomUID RTBeamsTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.4", "RT Beams Treatment Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Plan Storage</summary>
        public static readonly DicomUID RTPlanStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.5", "RT Plan Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Brachy Treatment Record Storage</summary>
        public static readonly DicomUID RTBrachyTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.6", "RT Brachy Treatment Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Treatment Summary Record Storage</summary>
        public static readonly DicomUID RTTreatmentSummaryRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.7", "RT Treatment Summary Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Ion Plan Storage</summary>
        public static readonly DicomUID RTIonPlanStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.8", "RT Ion Plan Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Ion Beams Treatment Record Storage</summary>
        public static readonly DicomUID RTIonBeamsTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.9", "RT Ion Beams Treatment Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Physician Intent Storage</summary>
        public static readonly DicomUID RTPhysicianIntentStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.10", "RT Physician Intent Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Segment Annotation Storage</summary>
        public static readonly DicomUID RTSegmentAnnotationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.11", "RT Segment Annotation Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Radiation Set Storage</summary>
        public static readonly DicomUID RTRadiationSetStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.12", "RT Radiation Set Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: C-Arm Photon-Electron Radiation Storage</summary>
        public static readonly DicomUID CArmPhotonElectronRadiationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.13", "C-Arm Photon-Electron Radiation Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: DICOS CT Image Storage</summary>
        public static readonly DicomUID DICOSCTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.1", "DICOS CT Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: DICOS Digital X-Ray Image Storage - For Presentation</summary>
        public static readonly DicomUID DICOSDigitalXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.501.2.1", "DICOS Digital X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: DICOS Digital X-Ray Image Storage - For Processing</summary>
        public static readonly DicomUID DICOSDigitalXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.501.2.2", "DICOS Digital X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: DICOS Threat Detection Report Storage</summary>
        public static readonly DicomUID DICOSThreatDetectionReportStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.3", "DICOS Threat Detection Report Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: DICOS 2D AIT Storage</summary>
        public static readonly DicomUID DICOS2DAITStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.4", "DICOS 2D AIT Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: DICOS 3D AIT Storage</summary>
        public static readonly DicomUID DICOS3DAITStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.5", "DICOS 3D AIT Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: DICOS Quadrupole Resonance (QR) Storage</summary>
        public static readonly DicomUID DICOSQuadrupoleResonanceQRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.6", "DICOS Quadrupole Resonance (QR) Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Eddy Current Image Storage</summary>
        public static readonly DicomUID EddyCurrentImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.601.1", "Eddy Current Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Eddy Current Multi-frame Image Storage</summary>
        public static readonly DicomUID EddyCurrentMultiFrameImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.601.2", "Eddy Current Multi-frame Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient Root Query/Retrieve Information Model - FIND</summary>
        public static readonly DicomUID PatientRootQueryRetrieveInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.1.2.1.1", "Patient Root Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient Root Query/Retrieve Information Model - MOVE</summary>
        public static readonly DicomUID PatientRootQueryRetrieveInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.1.2", "Patient Root Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient Root Query/Retrieve Information Model - GET</summary>
        public static readonly DicomUID PatientRootQueryRetrieveInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.1.2.1.3", "Patient Root Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Study Root Query/Retrieve Information Model - FIND</summary>
        public static readonly DicomUID StudyRootQueryRetrieveInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.1.2.2.1", "Study Root Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Study Root Query/Retrieve Information Model - MOVE</summary>
        public static readonly DicomUID StudyRootQueryRetrieveInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.2.2", "Study Root Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Study Root Query/Retrieve Information Model - GET</summary>
        public static readonly DicomUID StudyRootQueryRetrieveInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.1.2.2.3", "Study Root Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - FIND (Retired)</summary>
        public static readonly DicomUID PatientStudyOnlyQueryRetrieveInformationModelFINDRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.1", "Patient/Study Only Query/Retrieve Information Model - FIND (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - MOVE (Retired)</summary>
        public static readonly DicomUID PatientStudyOnlyQueryRetrieveInformationModelMOVERETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.2", "Patient/Study Only Query/Retrieve Information Model - MOVE (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - GET (Retired)</summary>
        public static readonly DicomUID PatientStudyOnlyQueryRetrieveInformationModelGETRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.3", "Patient/Study Only Query/Retrieve Information Model - GET (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Composite Instance Root Retrieve - MOVE</summary>
        public static readonly DicomUID CompositeInstanceRootRetrieveMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.4.2", "Composite Instance Root Retrieve - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Composite Instance Root Retrieve - GET</summary>
        public static readonly DicomUID CompositeInstanceRootRetrieveGET = new DicomUID("1.2.840.10008.5.1.4.1.2.4.3", "Composite Instance Root Retrieve - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Composite Instance Retrieve Without Bulk Data - GET</summary>
        public static readonly DicomUID CompositeInstanceRetrieveWithoutBulkDataGET = new DicomUID("1.2.840.10008.5.1.4.1.2.5.3", "Composite Instance Retrieve Without Bulk Data - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Defined Procedure Protocol Information Model - FIND</summary>
        public static readonly DicomUID DefinedProcedureProtocolInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.20.1", "Defined Procedure Protocol Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Defined Procedure Protocol Information Model - MOVE</summary>
        public static readonly DicomUID DefinedProcedureProtocolInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.20.2", "Defined Procedure Protocol Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Defined Procedure Protocol Information Model - GET</summary>
        public static readonly DicomUID DefinedProcedureProtocolInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.20.3", "Defined Procedure Protocol Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Modality Worklist Information Model - FIND</summary>
        public static readonly DicomUID ModalityWorklistInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.31", "Modality Worklist Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>Meta SOP Class: General Purpose Worklist Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID GeneralPurposeWorklistManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.4.32", "General Purpose Worklist Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: General Purpose Worklist Information Model - FIND (Retired)</summary>
        public static readonly DicomUID GeneralPurposeWorklistInformationModelFINDRETIRED = new DicomUID("1.2.840.10008.5.1.4.32.1", "General Purpose Worklist Information Model - FIND (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: General Purpose Scheduled Procedure Step SOP Class (Retired)</summary>
        public static readonly DicomUID GeneralPurposeScheduledProcedureStepSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.4.32.2", "General Purpose Scheduled Procedure Step SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: General Purpose Performed Procedure Step SOP Class (Retired)</summary>
        public static readonly DicomUID GeneralPurposePerformedProcedureStepSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.4.32.3", "General Purpose Performed Procedure Step SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Instance Availability Notification SOP Class</summary>
        public static readonly DicomUID InstanceAvailabilityNotificationSOPClass = new DicomUID("1.2.840.10008.5.1.4.33", "Instance Availability Notification SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Beams Delivery Instruction Storage - Trial (Retired)</summary>
        public static readonly DicomUID RTBeamsDeliveryInstructionStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.1", "RT Beams Delivery Instruction Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: RT Conventional Machine Verification - Trial (Retired)</summary>
        public static readonly DicomUID RTConventionalMachineVerificationTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.2", "RT Conventional Machine Verification - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: RT Ion Machine Verification - Trial (Retired)</summary>
        public static readonly DicomUID RTIonMachineVerificationTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.3", "RT Ion Machine Verification - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Service Class: Unified Worklist and Procedure Step Service Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedWorklistAndProcedureStepServiceClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4", "Unified Worklist and Procedure Step Service Class - Trial (Retired)", DicomUidType.ServiceClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Push SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepPushSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.1", "Unified Procedure Step - Push SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Watch SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepWatchSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.2", "Unified Procedure Step - Watch SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Pull SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepPullSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.3", "Unified Procedure Step - Pull SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Event SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepEventSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.4", "Unified Procedure Step - Event SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Well-known SOP Instance: UPS Global Subscription SOP Instance</summary>
        public static readonly DicomUID UPSGlobalSubscriptionSOPInstance = new DicomUID("1.2.840.10008.5.1.4.34.5", "UPS Global Subscription SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: UPS Filtered Global Subscription SOP Instance</summary>
        public static readonly DicomUID UPSFilteredGlobalSubscriptionSOPInstance = new DicomUID("1.2.840.10008.5.1.4.34.5.1", "UPS Filtered Global Subscription SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Service Class: Unified Worklist and Procedure Step Service Class</summary>
        public static readonly DicomUID UnifiedWorklistAndProcedureStepServiceClass = new DicomUID("1.2.840.10008.5.1.4.34.6", "Unified Worklist and Procedure Step Service Class", DicomUidType.ServiceClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Push SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepPushSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.1", "Unified Procedure Step - Push SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Watch SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepWatchSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.2", "Unified Procedure Step - Watch SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Pull SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepPullSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.3", "Unified Procedure Step - Pull SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Event SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepEventSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.4", "Unified Procedure Step - Event SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Beams Delivery Instruction Storage</summary>
        public static readonly DicomUID RTBeamsDeliveryInstructionStorage = new DicomUID("1.2.840.10008.5.1.4.34.7", "RT Beams Delivery Instruction Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Conventional Machine Verification</summary>
        public static readonly DicomUID RTConventionalMachineVerification = new DicomUID("1.2.840.10008.5.1.4.34.8", "RT Conventional Machine Verification", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Ion Machine Verification</summary>
        public static readonly DicomUID RTIonMachineVerification = new DicomUID("1.2.840.10008.5.1.4.34.9", "RT Ion Machine Verification", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Brachy Application Setup Delivery Instruction Storage</summary>
        public static readonly DicomUID RTBrachyApplicationSetupDeliveryInstructionStorage = new DicomUID("1.2.840.10008.5.1.4.34.10", "RT Brachy Application Setup Delivery Instruction Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: General Relevant Patient Information Query</summary>
        public static readonly DicomUID GeneralRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.1", "General Relevant Patient Information Query", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Breast Imaging Relevant Patient Information Query</summary>
        public static readonly DicomUID BreastImagingRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.2", "Breast Imaging Relevant Patient Information Query", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Cardiac Relevant Patient Information Query</summary>
        public static readonly DicomUID CardiacRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.3", "Cardiac Relevant Patient Information Query", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Hanging Protocol Storage</summary>
        public static readonly DicomUID HangingProtocolStorage = new DicomUID("1.2.840.10008.5.1.4.38.1", "Hanging Protocol Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Hanging Protocol Information Model - FIND</summary>
        public static readonly DicomUID HangingProtocolInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.38.2", "Hanging Protocol Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Hanging Protocol Information Model - MOVE</summary>
        public static readonly DicomUID HangingProtocolInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.38.3", "Hanging Protocol Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Hanging Protocol Information Model - GET</summary>
        public static readonly DicomUID HangingProtocolInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.38.4", "Hanging Protocol Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Storage</summary>
        public static readonly DicomUID ColorPaletteStorage = new DicomUID("1.2.840.10008.5.1.4.39.1", "Color Palette Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Query/Retrieve Information Model - FIND</summary>
        public static readonly DicomUID ColorPaletteQueryRetrieveInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.39.2", "Color Palette Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Query/Retrieve Information Model - MOVE</summary>
        public static readonly DicomUID ColorPaletteQueryRetrieveInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.39.3", "Color Palette Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Query/Retrieve Information Model - GET</summary>
        public static readonly DicomUID ColorPaletteQueryRetrieveInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.39.4", "Color Palette Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Product Characteristics Query SOP Class</summary>
        public static readonly DicomUID ProductCharacteristicsQuerySOPClass = new DicomUID("1.2.840.10008.5.1.4.41", "Product Characteristics Query SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Substance Approval Query SOP Class</summary>
        public static readonly DicomUID SubstanceApprovalQuerySOPClass = new DicomUID("1.2.840.10008.5.1.4.42", "Substance Approval Query SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Storage</summary>
        public static readonly DicomUID GenericImplantTemplateStorage = new DicomUID("1.2.840.10008.5.1.4.43.1", "Generic Implant Template Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Information Model - FIND</summary>
        public static readonly DicomUID GenericImplantTemplateInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.43.2", "Generic Implant Template Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Information Model - MOVE</summary>
        public static readonly DicomUID GenericImplantTemplateInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.43.3", "Generic Implant Template Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Information Model - GET</summary>
        public static readonly DicomUID GenericImplantTemplateInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.43.4", "Generic Implant Template Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Storage</summary>
        public static readonly DicomUID ImplantAssemblyTemplateStorage = new DicomUID("1.2.840.10008.5.1.4.44.1", "Implant Assembly Template Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Information Model - FIND</summary>
        public static readonly DicomUID ImplantAssemblyTemplateInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.44.2", "Implant Assembly Template Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Information Model - MOVE</summary>
        public static readonly DicomUID ImplantAssemblyTemplateInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.44.3", "Implant Assembly Template Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Information Model - GET</summary>
        public static readonly DicomUID ImplantAssemblyTemplateInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.44.4", "Implant Assembly Template Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Storage</summary>
        public static readonly DicomUID ImplantTemplateGroupStorage = new DicomUID("1.2.840.10008.5.1.4.45.1", "Implant Template Group Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Information Model - FIND</summary>
        public static readonly DicomUID ImplantTemplateGroupInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.45.2", "Implant Template Group Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Information Model - MOVE</summary>
        public static readonly DicomUID ImplantTemplateGroupInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.45.3", "Implant Template Group Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Information Model - GET</summary>
        public static readonly DicomUID ImplantTemplateGroupInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.45.4", "Implant Template Group Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>Application Hosting Model: Native DICOM Model</summary>
        public static readonly DicomUID NativeDICOMModel = new DicomUID("1.2.840.10008.7.1.1", "Native DICOM Model", DicomUidType.ApplicationHostingModel, false);

        ///<summary>Application Hosting Model: Abstract Multi-Dimensional Image Model</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModel = new DicomUID("1.2.840.10008.7.1.2", "Abstract Multi-Dimensional Image Model", DicomUidType.ApplicationHostingModel, false);

        ///<summary>Mapping Resource: DICOM Content Mapping Resource</summary>
        public static readonly DicomUID DICOMContentMappingResource = new DicomUID("1.2.840.10008.8.1.1", "DICOM Content Mapping Resource", DicomUidType.MappingResource, false);

        ///<summary>SOP Class: Video Endoscopic Image Real-Time Communication</summary>
        public static readonly DicomUID VideoEndoscopicImageRealTimeCommunication = new DicomUID("1.2.840.10008.10.1", "Video Endoscopic Image Real-Time Communication", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Video Photographic Image Real-Time Communication</summary>
        public static readonly DicomUID VideoPhotographicImageRealTimeCommunication = new DicomUID("1.2.840.10008.10.2", "Video Photographic Image Real-Time Communication", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Audio Waveform Real-Time Communication</summary>
        public static readonly DicomUID AudioWaveformRealTimeCommunication = new DicomUID("1.2.840.10008.10.3", "Audio Waveform Real-Time Communication", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Rendition Selection Document Real-Time Communication</summary>
        public static readonly DicomUID RenditionSelectionDocumentRealTimeCommunication = new DicomUID("1.2.840.10008.10.4", "Rendition Selection Document Real-Time Communication", DicomUidType.SOPClass, false);

        ///<summary>LDAP OID: dicomDeviceName</summary>
        public static readonly DicomUID dicomDeviceName = new DicomUID("1.2.840.10008.15.0.3.1", "dicomDeviceName", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomDescription</summary>
        public static readonly DicomUID dicomDescription = new DicomUID("1.2.840.10008.15.0.3.2", "dicomDescription", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomManufacturer</summary>
        public static readonly DicomUID dicomManufacturer = new DicomUID("1.2.840.10008.15.0.3.3", "dicomManufacturer", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomManufacturerModelName</summary>
        public static readonly DicomUID dicomManufacturerModelName = new DicomUID("1.2.840.10008.15.0.3.4", "dicomManufacturerModelName", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomSoftwareVersion</summary>
        public static readonly DicomUID dicomSoftwareVersion = new DicomUID("1.2.840.10008.15.0.3.5", "dicomSoftwareVersion", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomVendorData</summary>
        public static readonly DicomUID dicomVendorData = new DicomUID("1.2.840.10008.15.0.3.6", "dicomVendorData", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomAETitle</summary>
        public static readonly DicomUID dicomAETitle = new DicomUID("1.2.840.10008.15.0.3.7", "dicomAETitle", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomNetworkConnectionReference</summary>
        public static readonly DicomUID dicomNetworkConnectionReference = new DicomUID("1.2.840.10008.15.0.3.8", "dicomNetworkConnectionReference", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomApplicationCluster</summary>
        public static readonly DicomUID dicomApplicationCluster = new DicomUID("1.2.840.10008.15.0.3.9", "dicomApplicationCluster", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomAssociationInitiator</summary>
        public static readonly DicomUID dicomAssociationInitiator = new DicomUID("1.2.840.10008.15.0.3.10", "dicomAssociationInitiator", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomAssociationAcceptor</summary>
        public static readonly DicomUID dicomAssociationAcceptor = new DicomUID("1.2.840.10008.15.0.3.11", "dicomAssociationAcceptor", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomHostname</summary>
        public static readonly DicomUID dicomHostname = new DicomUID("1.2.840.10008.15.0.3.12", "dicomHostname", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomPort</summary>
        public static readonly DicomUID dicomPort = new DicomUID("1.2.840.10008.15.0.3.13", "dicomPort", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomSOPClass</summary>
        public static readonly DicomUID dicomSOPClass = new DicomUID("1.2.840.10008.15.0.3.14", "dicomSOPClass", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomTransferRole</summary>
        public static readonly DicomUID dicomTransferRole = new DicomUID("1.2.840.10008.15.0.3.15", "dicomTransferRole", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomTransferSyntax</summary>
        public static readonly DicomUID dicomTransferSyntax = new DicomUID("1.2.840.10008.15.0.3.16", "dicomTransferSyntax", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomPrimaryDeviceType</summary>
        public static readonly DicomUID dicomPrimaryDeviceType = new DicomUID("1.2.840.10008.15.0.3.17", "dicomPrimaryDeviceType", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomRelatedDeviceReference</summary>
        public static readonly DicomUID dicomRelatedDeviceReference = new DicomUID("1.2.840.10008.15.0.3.18", "dicomRelatedDeviceReference", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomPreferredCalledAETitle</summary>
        public static readonly DicomUID dicomPreferredCalledAETitle = new DicomUID("1.2.840.10008.15.0.3.19", "dicomPreferredCalledAETitle", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomTLSCyphersuite</summary>
        public static readonly DicomUID dicomTLSCyphersuite = new DicomUID("1.2.840.10008.15.0.3.20", "dicomTLSCyphersuite", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomAuthorizedNodeCertificateReference</summary>
        public static readonly DicomUID dicomAuthorizedNodeCertificateReference = new DicomUID("1.2.840.10008.15.0.3.21", "dicomAuthorizedNodeCertificateReference", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomThisNodeCertificateReference</summary>
        public static readonly DicomUID dicomThisNodeCertificateReference = new DicomUID("1.2.840.10008.15.0.3.22", "dicomThisNodeCertificateReference", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomInstalled</summary>
        public static readonly DicomUID dicomInstalled = new DicomUID("1.2.840.10008.15.0.3.23", "dicomInstalled", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomStationName</summary>
        public static readonly DicomUID dicomStationName = new DicomUID("1.2.840.10008.15.0.3.24", "dicomStationName", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomDeviceSerialNumber</summary>
        public static readonly DicomUID dicomDeviceSerialNumber = new DicomUID("1.2.840.10008.15.0.3.25", "dicomDeviceSerialNumber", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomInstitutionName</summary>
        public static readonly DicomUID dicomInstitutionName = new DicomUID("1.2.840.10008.15.0.3.26", "dicomInstitutionName", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomInstitutionAddress</summary>
        public static readonly DicomUID dicomInstitutionAddress = new DicomUID("1.2.840.10008.15.0.3.27", "dicomInstitutionAddress", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomInstitutionDepartmentName</summary>
        public static readonly DicomUID dicomInstitutionDepartmentName = new DicomUID("1.2.840.10008.15.0.3.28", "dicomInstitutionDepartmentName", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomIssuerOfPatientID</summary>
        public static readonly DicomUID dicomIssuerOfPatientID = new DicomUID("1.2.840.10008.15.0.3.29", "dicomIssuerOfPatientID", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomPreferredCallingAETitle</summary>
        public static readonly DicomUID dicomPreferredCallingAETitle = new DicomUID("1.2.840.10008.15.0.3.30", "dicomPreferredCallingAETitle", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomSupportedCharacterSet</summary>
        public static readonly DicomUID dicomSupportedCharacterSet = new DicomUID("1.2.840.10008.15.0.3.31", "dicomSupportedCharacterSet", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomConfigurationRoot</summary>
        public static readonly DicomUID dicomConfigurationRoot = new DicomUID("1.2.840.10008.15.0.4.1", "dicomConfigurationRoot", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomDevicesRoot</summary>
        public static readonly DicomUID dicomDevicesRoot = new DicomUID("1.2.840.10008.15.0.4.2", "dicomDevicesRoot", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomUniqueAETitlesRegistryRoot</summary>
        public static readonly DicomUID dicomUniqueAETitlesRegistryRoot = new DicomUID("1.2.840.10008.15.0.4.3", "dicomUniqueAETitlesRegistryRoot", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomDevice</summary>
        public static readonly DicomUID dicomDevice = new DicomUID("1.2.840.10008.15.0.4.4", "dicomDevice", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomNetworkAE</summary>
        public static readonly DicomUID dicomNetworkAE = new DicomUID("1.2.840.10008.15.0.4.5", "dicomNetworkAE", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomNetworkConnection</summary>
        public static readonly DicomUID dicomNetworkConnection = new DicomUID("1.2.840.10008.15.0.4.6", "dicomNetworkConnection", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomUniqueAETitle</summary>
        public static readonly DicomUID dicomUniqueAETitle = new DicomUID("1.2.840.10008.15.0.4.7", "dicomUniqueAETitle", DicomUidType.LDAP, false);

        ///<summary>LDAP OID: dicomTransferCapability</summary>
        public static readonly DicomUID dicomTransferCapability = new DicomUID("1.2.840.10008.15.0.4.8", "dicomTransferCapability", DicomUidType.LDAP, false);

        ///<summary>Synchronization Frame of Reference: Universal Coordinated Time</summary>
        public static readonly DicomUID UniversalCoordinatedTime = new DicomUID("1.2.840.10008.15.1.1", "Universal Coordinated Time", DicomUidType.FrameOfReference, false);

        ///<summary>Context Group Name: Anatomic Modifier (2)</summary>
        public static readonly DicomUID AnatomicModifier2 = new DicomUID("1.2.840.10008.6.1.1", "Anatomic Modifier (2)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Region (4)</summary>
        public static readonly DicomUID AnatomicRegion4 = new DicomUID("1.2.840.10008.6.1.2", "Anatomic Region (4)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Transducer Approach (5)</summary>
        public static readonly DicomUID TransducerApproach5 = new DicomUID("1.2.840.10008.6.1.3", "Transducer Approach (5)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Transducer Orientation (6)</summary>
        public static readonly DicomUID TransducerOrientation6 = new DicomUID("1.2.840.10008.6.1.4", "Transducer Orientation (6)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Beam Path (7)</summary>
        public static readonly DicomUID UltrasoundBeamPath7 = new DicomUID("1.2.840.10008.6.1.5", "Ultrasound Beam Path (7)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Angiographic Interventional Devices (8)</summary>
        public static readonly DicomUID AngiographicInterventionalDevices8 = new DicomUID("1.2.840.10008.6.1.6", "Angiographic Interventional Devices (8)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Guided Therapeutic Procedures (9)</summary>
        public static readonly DicomUID ImageGuidedTherapeuticProcedures9 = new DicomUID("1.2.840.10008.6.1.7", "Image Guided Therapeutic Procedures (9)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interventional Drug (10)</summary>
        public static readonly DicomUID InterventionalDrug10 = new DicomUID("1.2.840.10008.6.1.8", "Interventional Drug (10)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Route of Administration (11)</summary>
        public static readonly DicomUID RouteOfAdministration11 = new DicomUID("1.2.840.10008.6.1.9", "Route of Administration (11)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiographic Contrast Agent (12)</summary>
        public static readonly DicomUID RadiographicContrastAgent12 = new DicomUID("1.2.840.10008.6.1.10", "Radiographic Contrast Agent (12)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiographic Contrast Agent Ingredient (13)</summary>
        public static readonly DicomUID RadiographicContrastAgentIngredient13 = new DicomUID("1.2.840.10008.6.1.11", "Radiographic Contrast Agent Ingredient (13)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Isotopes in Radiopharmaceuticals (18)</summary>
        public static readonly DicomUID IsotopesInRadiopharmaceuticals18 = new DicomUID("1.2.840.10008.6.1.12", "Isotopes in Radiopharmaceuticals (18)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Orientation (19)</summary>
        public static readonly DicomUID PatientOrientation19 = new DicomUID("1.2.840.10008.6.1.13", "Patient Orientation (19)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Orientation Modifier (20)</summary>
        public static readonly DicomUID PatientOrientationModifier20 = new DicomUID("1.2.840.10008.6.1.14", "Patient Orientation Modifier (20)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Equipment Relationship (21)</summary>
        public static readonly DicomUID PatientEquipmentRelationship21 = new DicomUID("1.2.840.10008.6.1.15", "Patient Equipment Relationship (21)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cranio-Caudad Angulation (23)</summary>
        public static readonly DicomUID CranioCaudadAngulation23 = new DicomUID("1.2.840.10008.6.1.16", "Cranio-Caudad Angulation (23)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiopharmaceuticals (25)</summary>
        public static readonly DicomUID Radiopharmaceuticals25 = new DicomUID("1.2.840.10008.6.1.17", "Radiopharmaceuticals (25)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nuclear Medicine Projections (26)</summary>
        public static readonly DicomUID NuclearMedicineProjections26 = new DicomUID("1.2.840.10008.6.1.18", "Nuclear Medicine Projections (26)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Acquisition Modality (29)</summary>
        public static readonly DicomUID AcquisitionModality29 = new DicomUID("1.2.840.10008.6.1.19", "Acquisition Modality (29)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DICOM Devices (30)</summary>
        public static readonly DicomUID DICOMDevices30 = new DicomUID("1.2.840.10008.6.1.20", "DICOM Devices (30)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Priors (31)</summary>
        public static readonly DicomUID AbstractPriors31 = new DicomUID("1.2.840.10008.6.1.21", "Abstract Priors (31)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Value Qualifier (42)</summary>
        public static readonly DicomUID NumericValueQualifier42 = new DicomUID("1.2.840.10008.6.1.22", "Numeric Value Qualifier (42)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Measurement (82)</summary>
        public static readonly DicomUID UnitsOfMeasurement82 = new DicomUID("1.2.840.10008.6.1.23", "Units of Measurement (82)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units for Real World Value Mapping (83)</summary>
        public static readonly DicomUID UnitsForRealWorldValueMapping83 = new DicomUID("1.2.840.10008.6.1.24", "Units for Real World Value Mapping (83)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Level of Significance (220)</summary>
        public static readonly DicomUID LevelOfSignificance220 = new DicomUID("1.2.840.10008.6.1.25", "Level of Significance (220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Range Concepts (221)</summary>
        public static readonly DicomUID MeasurementRangeConcepts221 = new DicomUID("1.2.840.10008.6.1.26", "Measurement Range Concepts (221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Normality Codes (222)</summary>
        public static readonly DicomUID NormalityCodes222 = new DicomUID("1.2.840.10008.6.1.27", "Normality Codes (222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Normal Range Values (223)</summary>
        public static readonly DicomUID NormalRangeValues223 = new DicomUID("1.2.840.10008.6.1.28", "Normal Range Values (223)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Selection Method (224)</summary>
        public static readonly DicomUID SelectionMethod224 = new DicomUID("1.2.840.10008.6.1.29", "Selection Method (224)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Uncertainty Concepts (225)</summary>
        public static readonly DicomUID MeasurementUncertaintyConcepts225 = new DicomUID("1.2.840.10008.6.1.30", "Measurement Uncertainty Concepts (225)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Population Statistical Descriptors (226)</summary>
        public static readonly DicomUID PopulationStatisticalDescriptors226 = new DicomUID("1.2.840.10008.6.1.31", "Population Statistical Descriptors (226)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sample Statistical Descriptors (227)</summary>
        public static readonly DicomUID SampleStatisticalDescriptors227 = new DicomUID("1.2.840.10008.6.1.32", "Sample Statistical Descriptors (227)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equation or Table (228)</summary>
        public static readonly DicomUID EquationOrTable228 = new DicomUID("1.2.840.10008.6.1.33", "Equation or Table (228)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Yes-No (230)</summary>
        public static readonly DicomUID YesNo230 = new DicomUID("1.2.840.10008.6.1.34", "Yes-No (230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Present-Absent (240)</summary>
        public static readonly DicomUID PresentAbsent240 = new DicomUID("1.2.840.10008.6.1.35", "Present-Absent (240)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Normal-Abnormal (242)</summary>
        public static readonly DicomUID NormalAbnormal242 = new DicomUID("1.2.840.10008.6.1.36", "Normal-Abnormal (242)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Laterality (244)</summary>
        public static readonly DicomUID Laterality244 = new DicomUID("1.2.840.10008.6.1.37", "Laterality (244)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Positive-Negative (250)</summary>
        public static readonly DicomUID PositiveNegative250 = new DicomUID("1.2.840.10008.6.1.38", "Positive-Negative (250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Severity of Complication (251)</summary>
        public static readonly DicomUID SeverityOfComplication251 = new DicomUID("1.2.840.10008.6.1.39", "Severity of Complication (251)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Observer Type (270)</summary>
        public static readonly DicomUID ObserverType270 = new DicomUID("1.2.840.10008.6.1.40", "Observer Type (270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Observation Subject Class (271)</summary>
        public static readonly DicomUID ObservationSubjectClass271 = new DicomUID("1.2.840.10008.6.1.41", "Observation Subject Class (271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Audio Channel Source (3000)</summary>
        public static readonly DicomUID AudioChannelSource3000 = new DicomUID("1.2.840.10008.6.1.42", "Audio Channel Source (3000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Leads (3001)</summary>
        public static readonly DicomUID ECGLeads3001 = new DicomUID("1.2.840.10008.6.1.43", "ECG Leads (3001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Waveform Sources (3003)</summary>
        public static readonly DicomUID HemodynamicWaveformSources3003 = new DicomUID("1.2.840.10008.6.1.44", "Hemodynamic Waveform Sources (3003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Anatomic Locations (3010)</summary>
        public static readonly DicomUID CardiovascularAnatomicLocations3010 = new DicomUID("1.2.840.10008.6.1.45", "Cardiovascular Anatomic Locations (3010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Anatomic Locations (3011)</summary>
        public static readonly DicomUID ElectrophysiologyAnatomicLocations3011 = new DicomUID("1.2.840.10008.6.1.46", "Electrophysiology Anatomic Locations (3011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Artery Segments (3014)</summary>
        public static readonly DicomUID CoronaryArterySegments3014 = new DicomUID("1.2.840.10008.6.1.47", "Coronary Artery Segments (3014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Arteries (3015)</summary>
        public static readonly DicomUID CoronaryArteries3015 = new DicomUID("1.2.840.10008.6.1.48", "Coronary Arteries (3015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Anatomic Location Modifiers (3019)</summary>
        public static readonly DicomUID CardiovascularAnatomicLocationModifiers3019 = new DicomUID("1.2.840.10008.6.1.49", "Cardiovascular Anatomic Location Modifiers (3019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiology Units of Measurement (Retired) (3082)</summary>
        public static readonly DicomUID CardiologyUnitsOfMeasurement3082RETIRED = new DicomUID("1.2.840.10008.6.1.50", "Cardiology Units of Measurement (Retired) (3082)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Time Synchronization Channel Types (3090)</summary>
        public static readonly DicomUID TimeSynchronizationChannelTypes3090 = new DicomUID("1.2.840.10008.6.1.51", "Time Synchronization Channel Types (3090)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Procedural State Values (3101)</summary>
        public static readonly DicomUID CardiacProceduralStateValues3101 = new DicomUID("1.2.840.10008.6.1.52", "Cardiac Procedural State Values (3101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Measurement Functions and Techniques (3240)</summary>
        public static readonly DicomUID ElectrophysiologyMeasurementFunctionsAndTechniques3240 = new DicomUID("1.2.840.10008.6.1.53", "Electrophysiology Measurement Functions and Techniques (3240)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Measurement Techniques (3241)</summary>
        public static readonly DicomUID HemodynamicMeasurementTechniques3241 = new DicomUID("1.2.840.10008.6.1.54", "Hemodynamic Measurement Techniques (3241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Catheterization Procedure Phase (3250)</summary>
        public static readonly DicomUID CatheterizationProcedurePhase3250 = new DicomUID("1.2.840.10008.6.1.55", "Catheterization Procedure Phase (3250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Procedure Phase (3254)</summary>
        public static readonly DicomUID ElectrophysiologyProcedurePhase3254 = new DicomUID("1.2.840.10008.6.1.56", "Electrophysiology Procedure Phase (3254)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Protocols (3261)</summary>
        public static readonly DicomUID StressProtocols3261 = new DicomUID("1.2.840.10008.6.1.57", "Stress Protocols (3261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Patient State Values (3262)</summary>
        public static readonly DicomUID ECGPatientStateValues3262 = new DicomUID("1.2.840.10008.6.1.58", "ECG Patient State Values (3262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrode Placement Values (3263)</summary>
        public static readonly DicomUID ElectrodePlacementValues3263 = new DicomUID("1.2.840.10008.6.1.59", "Electrode Placement Values (3263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: XYZ Electrode Placement Values (Retired) (3264)</summary>
        public static readonly DicomUID XYZElectrodePlacementValues3264RETIRED = new DicomUID("1.2.840.10008.6.1.60", "XYZ Electrode Placement Values (Retired) (3264)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Hemodynamic Physiological Challenges (3271)</summary>
        public static readonly DicomUID HemodynamicPhysiologicalChallenges3271 = new DicomUID("1.2.840.10008.6.1.61", "Hemodynamic Physiological Challenges (3271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Annotations (3335)</summary>
        public static readonly DicomUID ECGAnnotations3335 = new DicomUID("1.2.840.10008.6.1.62", "ECG Annotations (3335)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Annotations (3337)</summary>
        public static readonly DicomUID HemodynamicAnnotations3337 = new DicomUID("1.2.840.10008.6.1.63", "Hemodynamic Annotations (3337)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Annotations (3339)</summary>
        public static readonly DicomUID ElectrophysiologyAnnotations3339 = new DicomUID("1.2.840.10008.6.1.64", "Electrophysiology Annotations (3339)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Log Titles (3400)</summary>
        public static readonly DicomUID ProcedureLogTitles3400 = new DicomUID("1.2.840.10008.6.1.65", "Procedure Log Titles (3400)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Types of Log Notes (3401)</summary>
        public static readonly DicomUID TypesOfLogNotes3401 = new DicomUID("1.2.840.10008.6.1.66", "Types of Log Notes (3401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Status and Events (3402)</summary>
        public static readonly DicomUID PatientStatusAndEvents3402 = new DicomUID("1.2.840.10008.6.1.67", "Patient Status and Events (3402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Percutaneous Entry (3403)</summary>
        public static readonly DicomUID PercutaneousEntry3403 = new DicomUID("1.2.840.10008.6.1.68", "Percutaneous Entry (3403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Staff Actions (3404)</summary>
        public static readonly DicomUID StaffActions3404 = new DicomUID("1.2.840.10008.6.1.69", "Staff Actions (3404)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Action Values (3405)</summary>
        public static readonly DicomUID ProcedureActionValues3405 = new DicomUID("1.2.840.10008.6.1.70", "Procedure Action Values (3405)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-coronary Transcatheter Interventions (3406)</summary>
        public static readonly DicomUID NonCoronaryTranscatheterInterventions3406 = new DicomUID("1.2.840.10008.6.1.71", "Non-coronary Transcatheter Interventions (3406)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference to Object (3407)</summary>
        public static readonly DicomUID PurposeOfReferenceToObject3407 = new DicomUID("1.2.840.10008.6.1.72", "Purpose of Reference to Object (3407)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Actions With Consumables (3408)</summary>
        public static readonly DicomUID ActionsWithConsumables3408 = new DicomUID("1.2.840.10008.6.1.73", "Actions With Consumables (3408)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Administration of Drugs/Contrast (3409)</summary>
        public static readonly DicomUID AdministrationOfDrugsContrast3409 = new DicomUID("1.2.840.10008.6.1.74", "Administration of Drugs/Contrast (3409)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Parameters of Drugs/Contrast (3410)</summary>
        public static readonly DicomUID NumericParametersOfDrugsContrast3410 = new DicomUID("1.2.840.10008.6.1.75", "Numeric Parameters of Drugs/Contrast (3410)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intracoronary Devices (3411)</summary>
        public static readonly DicomUID IntracoronaryDevices3411 = new DicomUID("1.2.840.10008.6.1.76", "Intracoronary Devices (3411)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intervention Actions and Status (3412)</summary>
        public static readonly DicomUID InterventionActionsAndStatus3412 = new DicomUID("1.2.840.10008.6.1.77", "Intervention Actions and Status (3412)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Adverse Outcomes (3413)</summary>
        public static readonly DicomUID AdverseOutcomes3413 = new DicomUID("1.2.840.10008.6.1.78", "Adverse Outcomes (3413)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Urgency (3414)</summary>
        public static readonly DicomUID ProcedureUrgency3414 = new DicomUID("1.2.840.10008.6.1.79", "Procedure Urgency (3414)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Rhythms (3415)</summary>
        public static readonly DicomUID CardiacRhythms3415 = new DicomUID("1.2.840.10008.6.1.80", "Cardiac Rhythms (3415)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiration Rhythms (3416)</summary>
        public static readonly DicomUID RespirationRhythms3416 = new DicomUID("1.2.840.10008.6.1.81", "Respiration Rhythms (3416)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Risk (3418)</summary>
        public static readonly DicomUID LesionRisk3418 = new DicomUID("1.2.840.10008.6.1.82", "Lesion Risk (3418)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Findings Titles (3419)</summary>
        public static readonly DicomUID FindingsTitles3419 = new DicomUID("1.2.840.10008.6.1.83", "Findings Titles (3419)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Action (3421)</summary>
        public static readonly DicomUID ProcedureAction3421 = new DicomUID("1.2.840.10008.6.1.84", "Procedure Action (3421)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Use Actions (3422)</summary>
        public static readonly DicomUID DeviceUseActions3422 = new DicomUID("1.2.840.10008.6.1.85", "Device Use Actions (3422)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Device Characteristics (3423)</summary>
        public static readonly DicomUID NumericDeviceCharacteristics3423 = new DicomUID("1.2.840.10008.6.1.86", "Numeric Device Characteristics (3423)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intervention Parameters (3425)</summary>
        public static readonly DicomUID InterventionParameters3425 = new DicomUID("1.2.840.10008.6.1.87", "Intervention Parameters (3425)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Consumables Parameters (3426)</summary>
        public static readonly DicomUID ConsumablesParameters3426 = new DicomUID("1.2.840.10008.6.1.88", "Consumables Parameters (3426)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equipment Events (3427)</summary>
        public static readonly DicomUID EquipmentEvents3427 = new DicomUID("1.2.840.10008.6.1.89", "Equipment Events (3427)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Procedures (3428)</summary>
        public static readonly DicomUID ImagingProcedures3428 = new DicomUID("1.2.840.10008.6.1.90", "Imaging Procedures (3428)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Catheterization Devices (3429)</summary>
        public static readonly DicomUID CatheterizationDevices3429 = new DicomUID("1.2.840.10008.6.1.91", "Catheterization Devices (3429)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DateTime Qualifiers (3430)</summary>
        public static readonly DicomUID DateTimeQualifiers3430 = new DicomUID("1.2.840.10008.6.1.92", "DateTime Qualifiers (3430)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Peripheral Pulse Locations (3440)</summary>
        public static readonly DicomUID PeripheralPulseLocations3440 = new DicomUID("1.2.840.10008.6.1.93", "Peripheral Pulse Locations (3440)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Assessments (3441)</summary>
        public static readonly DicomUID PatientAssessments3441 = new DicomUID("1.2.840.10008.6.1.94", "Patient Assessments (3441)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Peripheral Pulse Methods (3442)</summary>
        public static readonly DicomUID PeripheralPulseMethods3442 = new DicomUID("1.2.840.10008.6.1.95", "Peripheral Pulse Methods (3442)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Skin Condition (3446)</summary>
        public static readonly DicomUID SkinCondition3446 = new DicomUID("1.2.840.10008.6.1.96", "Skin Condition (3446)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Assessment (3448)</summary>
        public static readonly DicomUID AirwayAssessment3448 = new DicomUID("1.2.840.10008.6.1.97", "Airway Assessment (3448)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calibration Objects (3451)</summary>
        public static readonly DicomUID CalibrationObjects3451 = new DicomUID("1.2.840.10008.6.1.98", "Calibration Objects (3451)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calibration Methods (3452)</summary>
        public static readonly DicomUID CalibrationMethods3452 = new DicomUID("1.2.840.10008.6.1.99", "Calibration Methods (3452)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Volume Methods (3453)</summary>
        public static readonly DicomUID CardiacVolumeMethods3453 = new DicomUID("1.2.840.10008.6.1.100", "Cardiac Volume Methods (3453)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Index Methods (3455)</summary>
        public static readonly DicomUID IndexMethods3455 = new DicomUID("1.2.840.10008.6.1.101", "Index Methods (3455)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sub-segment Methods (3456)</summary>
        public static readonly DicomUID SubSegmentMethods3456 = new DicomUID("1.2.840.10008.6.1.102", "Sub-segment Methods (3456)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contour Realignment (3458)</summary>
        public static readonly DicomUID ContourRealignment3458 = new DicomUID("1.2.840.10008.6.1.103", "Contour Realignment (3458)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Circumferential Extent (3460)</summary>
        public static readonly DicomUID CircumferentialExtent3460 = new DicomUID("1.2.840.10008.6.1.104", "Circumferential Extent (3460)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Regional Extent (3461)</summary>
        public static readonly DicomUID RegionalExtent3461 = new DicomUID("1.2.840.10008.6.1.105", "Regional Extent (3461)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chamber Identification (3462)</summary>
        public static readonly DicomUID ChamberIdentification3462 = new DicomUID("1.2.840.10008.6.1.106", "Chamber Identification (3462)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: QA Reference Methods (3465)</summary>
        public static readonly DicomUID QAReferenceMethods3465 = new DicomUID("1.2.840.10008.6.1.107", "QA Reference Methods (3465)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Plane Identification (3466)</summary>
        public static readonly DicomUID PlaneIdentification3466 = new DicomUID("1.2.840.10008.6.1.108", "Plane Identification (3466)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ejection Fraction (3467)</summary>
        public static readonly DicomUID EjectionFraction3467 = new DicomUID("1.2.840.10008.6.1.109", "Ejection Fraction (3467)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ED Volume (3468)</summary>
        public static readonly DicomUID EDVolume3468 = new DicomUID("1.2.840.10008.6.1.110", "ED Volume (3468)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ES Volume (3469)</summary>
        public static readonly DicomUID ESVolume3469 = new DicomUID("1.2.840.10008.6.1.111", "ES Volume (3469)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Lumen Cross-sectional Area Calculation Methods (3470)</summary>
        public static readonly DicomUID VesselLumenCrossSectionalAreaCalculationMethods3470 = new DicomUID("1.2.840.10008.6.1.112", "Vessel Lumen Cross-sectional Area Calculation Methods (3470)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Estimated Volumes (3471)</summary>
        public static readonly DicomUID EstimatedVolumes3471 = new DicomUID("1.2.840.10008.6.1.113", "Estimated Volumes (3471)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Contraction Phase (3472)</summary>
        public static readonly DicomUID CardiacContractionPhase3472 = new DicomUID("1.2.840.10008.6.1.114", "Cardiac Contraction Phase (3472)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Procedure Phases (3480)</summary>
        public static readonly DicomUID IVUSProcedurePhases3480 = new DicomUID("1.2.840.10008.6.1.115", "IVUS Procedure Phases (3480)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Distance Measurements (3481)</summary>
        public static readonly DicomUID IVUSDistanceMeasurements3481 = new DicomUID("1.2.840.10008.6.1.116", "IVUS Distance Measurements (3481)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Area Measurements (3482)</summary>
        public static readonly DicomUID IVUSAreaMeasurements3482 = new DicomUID("1.2.840.10008.6.1.117", "IVUS Area Measurements (3482)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Longitudinal Measurements (3483)</summary>
        public static readonly DicomUID IVUSLongitudinalMeasurements3483 = new DicomUID("1.2.840.10008.6.1.118", "IVUS Longitudinal Measurements (3483)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Indices and Ratios (3484)</summary>
        public static readonly DicomUID IVUSIndicesAndRatios3484 = new DicomUID("1.2.840.10008.6.1.119", "IVUS Indices and Ratios (3484)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Volume Measurements (3485)</summary>
        public static readonly DicomUID IVUSVolumeMeasurements3485 = new DicomUID("1.2.840.10008.6.1.120", "IVUS Volume Measurements (3485)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Measurement Sites (3486)</summary>
        public static readonly DicomUID VascularMeasurementSites3486 = new DicomUID("1.2.840.10008.6.1.121", "Vascular Measurement Sites (3486)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intravascular Volumetric Regions (3487)</summary>
        public static readonly DicomUID IntravascularVolumetricRegions3487 = new DicomUID("1.2.840.10008.6.1.122", "Intravascular Volumetric Regions (3487)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Min/Max/Mean (3488)</summary>
        public static readonly DicomUID MinMaxMean3488 = new DicomUID("1.2.840.10008.6.1.123", "Min/Max/Mean (3488)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcium Distribution (3489)</summary>
        public static readonly DicomUID CalciumDistribution3489 = new DicomUID("1.2.840.10008.6.1.124", "Calcium Distribution (3489)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Lesion Morphologies (3491)</summary>
        public static readonly DicomUID IVUSLesionMorphologies3491 = new DicomUID("1.2.840.10008.6.1.125", "IVUS Lesion Morphologies (3491)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Dissection Classifications (3492)</summary>
        public static readonly DicomUID VascularDissectionClassifications3492 = new DicomUID("1.2.840.10008.6.1.126", "Vascular Dissection Classifications (3492)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Relative Stenosis Severities (3493)</summary>
        public static readonly DicomUID IVUSRelativeStenosisSeverities3493 = new DicomUID("1.2.840.10008.6.1.127", "IVUS Relative Stenosis Severities (3493)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Non Morphological Findings (3494)</summary>
        public static readonly DicomUID IVUSNonMorphologicalFindings3494 = new DicomUID("1.2.840.10008.6.1.128", "IVUS Non Morphological Findings (3494)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Plaque Composition (3495)</summary>
        public static readonly DicomUID IVUSPlaqueComposition3495 = new DicomUID("1.2.840.10008.6.1.129", "IVUS Plaque Composition (3495)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Fiducial Points (3496)</summary>
        public static readonly DicomUID IVUSFiducialPoints3496 = new DicomUID("1.2.840.10008.6.1.130", "IVUS Fiducial Points (3496)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Arterial Morphology (3497)</summary>
        public static readonly DicomUID IVUSArterialMorphology3497 = new DicomUID("1.2.840.10008.6.1.131", "IVUS Arterial Morphology (3497)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pressure Units (3500)</summary>
        public static readonly DicomUID PressureUnits3500 = new DicomUID("1.2.840.10008.6.1.132", "Pressure Units (3500)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Resistance Units (3502)</summary>
        public static readonly DicomUID HemodynamicResistanceUnits3502 = new DicomUID("1.2.840.10008.6.1.133", "Hemodynamic Resistance Units (3502)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Indexed Hemodynamic Resistance Units (3503)</summary>
        public static readonly DicomUID IndexedHemodynamicResistanceUnits3503 = new DicomUID("1.2.840.10008.6.1.134", "Indexed Hemodynamic Resistance Units (3503)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Catheter Size Units (3510)</summary>
        public static readonly DicomUID CatheterSizeUnits3510 = new DicomUID("1.2.840.10008.6.1.135", "Catheter Size Units (3510)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Collection (3515)</summary>
        public static readonly DicomUID SpecimenCollection3515 = new DicomUID("1.2.840.10008.6.1.136", "Specimen Collection (3515)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Source Type (3520)</summary>
        public static readonly DicomUID BloodSourceType3520 = new DicomUID("1.2.840.10008.6.1.137", "Blood Source Type (3520)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Gas Pressures (3524)</summary>
        public static readonly DicomUID BloodGasPressures3524 = new DicomUID("1.2.840.10008.6.1.138", "Blood Gas Pressures (3524)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Gas Content (3525)</summary>
        public static readonly DicomUID BloodGasContent3525 = new DicomUID("1.2.840.10008.6.1.139", "Blood Gas Content (3525)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Gas Saturation (3526)</summary>
        public static readonly DicomUID BloodGasSaturation3526 = new DicomUID("1.2.840.10008.6.1.140", "Blood Gas Saturation (3526)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Base Excess (3527)</summary>
        public static readonly DicomUID BloodBaseExcess3527 = new DicomUID("1.2.840.10008.6.1.141", "Blood Base Excess (3527)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood pH (3528)</summary>
        public static readonly DicomUID BloodPH3528 = new DicomUID("1.2.840.10008.6.1.142", "Blood pH (3528)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial / Venous Content (3529)</summary>
        public static readonly DicomUID ArterialVenousContent3529 = new DicomUID("1.2.840.10008.6.1.143", "Arterial / Venous Content (3529)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Oxygen Administration Actions (3530)</summary>
        public static readonly DicomUID OxygenAdministrationActions3530 = new DicomUID("1.2.840.10008.6.1.144", "Oxygen Administration Actions (3530)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Oxygen Administration (3531)</summary>
        public static readonly DicomUID OxygenAdministration3531 = new DicomUID("1.2.840.10008.6.1.145", "Oxygen Administration (3531)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Circulatory Support Actions (3550)</summary>
        public static readonly DicomUID CirculatorySupportActions3550 = new DicomUID("1.2.840.10008.6.1.146", "Circulatory Support Actions (3550)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventilation Actions (3551)</summary>
        public static readonly DicomUID VentilationActions3551 = new DicomUID("1.2.840.10008.6.1.147", "Ventilation Actions (3551)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pacing Actions (3552)</summary>
        public static readonly DicomUID PacingActions3552 = new DicomUID("1.2.840.10008.6.1.148", "Pacing Actions (3552)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Circulatory Support (3553)</summary>
        public static readonly DicomUID CirculatorySupport3553 = new DicomUID("1.2.840.10008.6.1.149", "Circulatory Support (3553)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventilation (3554)</summary>
        public static readonly DicomUID Ventilation3554 = new DicomUID("1.2.840.10008.6.1.150", "Ventilation (3554)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pacing (3555)</summary>
        public static readonly DicomUID Pacing3555 = new DicomUID("1.2.840.10008.6.1.151", "Pacing (3555)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Pressure Methods (3560)</summary>
        public static readonly DicomUID BloodPressureMethods3560 = new DicomUID("1.2.840.10008.6.1.152", "Blood Pressure Methods (3560)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Times (3600)</summary>
        public static readonly DicomUID RelativeTimes3600 = new DicomUID("1.2.840.10008.6.1.153", "Relative Times (3600)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Patient State (3602)</summary>
        public static readonly DicomUID HemodynamicPatientState3602 = new DicomUID("1.2.840.10008.6.1.154", "Hemodynamic Patient State (3602)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Lesion Locations (3604)</summary>
        public static readonly DicomUID ArterialLesionLocations3604 = new DicomUID("1.2.840.10008.6.1.155", "Arterial Lesion Locations (3604)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Source Locations (3606)</summary>
        public static readonly DicomUID ArterialSourceLocations3606 = new DicomUID("1.2.840.10008.6.1.156", "Arterial Source Locations (3606)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Venous Source Locations (3607)</summary>
        public static readonly DicomUID VenousSourceLocations3607 = new DicomUID("1.2.840.10008.6.1.157", "Venous Source Locations (3607)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Atrial Source Locations (3608)</summary>
        public static readonly DicomUID AtrialSourceLocations3608 = new DicomUID("1.2.840.10008.6.1.158", "Atrial Source Locations (3608)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventricular Source Locations (3609)</summary>
        public static readonly DicomUID VentricularSourceLocations3609 = new DicomUID("1.2.840.10008.6.1.159", "Ventricular Source Locations (3609)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gradient Source Locations (3610)</summary>
        public static readonly DicomUID GradientSourceLocations3610 = new DicomUID("1.2.840.10008.6.1.160", "Gradient Source Locations (3610)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pressure Measurements (3611)</summary>
        public static readonly DicomUID PressureMeasurements3611 = new DicomUID("1.2.840.10008.6.1.161", "Pressure Measurements (3611)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Velocity Measurements (3612)</summary>
        public static readonly DicomUID BloodVelocityMeasurements3612 = new DicomUID("1.2.840.10008.6.1.162", "Blood Velocity Measurements (3612)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Time Measurements (3613)</summary>
        public static readonly DicomUID HemodynamicTimeMeasurements3613 = new DicomUID("1.2.840.10008.6.1.163", "Hemodynamic Time Measurements (3613)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Valve Areas, Non-mitral (3614)</summary>
        public static readonly DicomUID ValveAreasNonMitral3614 = new DicomUID("1.2.840.10008.6.1.164", "Valve Areas, Non-mitral (3614)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Valve Areas (3615)</summary>
        public static readonly DicomUID ValveAreas3615 = new DicomUID("1.2.840.10008.6.1.165", "Valve Areas (3615)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Period Measurements (3616)</summary>
        public static readonly DicomUID HemodynamicPeriodMeasurements3616 = new DicomUID("1.2.840.10008.6.1.166", "Hemodynamic Period Measurements (3616)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Valve Flows (3617)</summary>
        public static readonly DicomUID ValveFlows3617 = new DicomUID("1.2.840.10008.6.1.167", "Valve Flows (3617)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Flows (3618)</summary>
        public static readonly DicomUID HemodynamicFlows3618 = new DicomUID("1.2.840.10008.6.1.168", "Hemodynamic Flows (3618)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Resistance Measurements (3619)</summary>
        public static readonly DicomUID HemodynamicResistanceMeasurements3619 = new DicomUID("1.2.840.10008.6.1.169", "Hemodynamic Resistance Measurements (3619)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Ratios (3620)</summary>
        public static readonly DicomUID HemodynamicRatios3620 = new DicomUID("1.2.840.10008.6.1.170", "Hemodynamic Ratios (3620)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fractional Flow Reserve (3621)</summary>
        public static readonly DicomUID FractionalFlowReserve3621 = new DicomUID("1.2.840.10008.6.1.171", "Fractional Flow Reserve (3621)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Type (3627)</summary>
        public static readonly DicomUID MeasurementType3627 = new DicomUID("1.2.840.10008.6.1.172", "Measurement Type (3627)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Output Methods (3628)</summary>
        public static readonly DicomUID CardiacOutputMethods3628 = new DicomUID("1.2.840.10008.6.1.173", "Cardiac Output Methods (3628)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Intent (3629)</summary>
        public static readonly DicomUID ProcedureIntent3629 = new DicomUID("1.2.840.10008.6.1.174", "Procedure Intent (3629)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Anatomic Locations (3630)</summary>
        public static readonly DicomUID CardiovascularAnatomicLocations3630 = new DicomUID("1.2.840.10008.6.1.175", "Cardiovascular Anatomic Locations (3630)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hypertension (3640)</summary>
        public static readonly DicomUID Hypertension3640 = new DicomUID("1.2.840.10008.6.1.176", "Hypertension (3640)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Assessments (3641)</summary>
        public static readonly DicomUID HemodynamicAssessments3641 = new DicomUID("1.2.840.10008.6.1.177", "Hemodynamic Assessments (3641)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Degree Findings (3642)</summary>
        public static readonly DicomUID DegreeFindings3642 = new DicomUID("1.2.840.10008.6.1.178", "Degree Findings (3642)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Measurement Phase (3651)</summary>
        public static readonly DicomUID HemodynamicMeasurementPhase3651 = new DicomUID("1.2.840.10008.6.1.179", "Hemodynamic Measurement Phase (3651)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Body Surface Area Equations (3663)</summary>
        public static readonly DicomUID BodySurfaceAreaEquations3663 = new DicomUID("1.2.840.10008.6.1.180", "Body Surface Area Equations (3663)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Oxygen Consumption Equations and Tables (3664)</summary>
        public static readonly DicomUID OxygenConsumptionEquationsAndTables3664 = new DicomUID("1.2.840.10008.6.1.181", "Oxygen Consumption Equations and Tables (3664)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: P50 Equations (3666)</summary>
        public static readonly DicomUID P50Equations3666 = new DicomUID("1.2.840.10008.6.1.182", "P50 Equations (3666)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Framingham Scores (3667)</summary>
        public static readonly DicomUID FraminghamScores3667 = new DicomUID("1.2.840.10008.6.1.183", "Framingham Scores (3667)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Framingham Tables (3668)</summary>
        public static readonly DicomUID FraminghamTables3668 = new DicomUID("1.2.840.10008.6.1.184", "Framingham Tables (3668)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Procedure Types (3670)</summary>
        public static readonly DicomUID ECGProcedureTypes3670 = new DicomUID("1.2.840.10008.6.1.185", "ECG Procedure Types (3670)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reason for ECG Exam (3671)</summary>
        public static readonly DicomUID ReasonForECGExam3671 = new DicomUID("1.2.840.10008.6.1.186", "Reason for ECG Exam (3671)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pacemakers (3672)</summary>
        public static readonly DicomUID Pacemakers3672 = new DicomUID("1.2.840.10008.6.1.187", "Pacemakers (3672)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnosis (Retired) (3673)</summary>
        public static readonly DicomUID Diagnosis3673RETIRED = new DicomUID("1.2.840.10008.6.1.188", "Diagnosis (Retired) (3673)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Other Filters (Retired) (3675)</summary>
        public static readonly DicomUID OtherFilters3675RETIRED = new DicomUID("1.2.840.10008.6.1.189", "Other Filters (Retired) (3675)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Lead Measurement Technique (3676)</summary>
        public static readonly DicomUID LeadMeasurementTechnique3676 = new DicomUID("1.2.840.10008.6.1.190", "Lead Measurement Technique (3676)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Summary Codes ECG (3677)</summary>
        public static readonly DicomUID SummaryCodesECG3677 = new DicomUID("1.2.840.10008.6.1.191", "Summary Codes ECG (3677)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: QT Correction Algorithms (3678)</summary>
        public static readonly DicomUID QTCorrectionAlgorithms3678 = new DicomUID("1.2.840.10008.6.1.192", "QT Correction Algorithms (3678)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Morphology Descriptions (Retired) (3679)</summary>
        public static readonly DicomUID ECGMorphologyDescriptions3679RETIRED = new DicomUID("1.2.840.10008.6.1.193", "ECG Morphology Descriptions (Retired) (3679)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: ECG Lead Noise Descriptions (3680)</summary>
        public static readonly DicomUID ECGLeadNoiseDescriptions3680 = new DicomUID("1.2.840.10008.6.1.194", "ECG Lead Noise Descriptions (3680)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Lead Noise Modifiers (Retired) (3681)</summary>
        public static readonly DicomUID ECGLeadNoiseModifiers3681RETIRED = new DicomUID("1.2.840.10008.6.1.195", "ECG Lead Noise Modifiers (Retired) (3681)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Probability (Retired) (3682)</summary>
        public static readonly DicomUID Probability3682RETIRED = new DicomUID("1.2.840.10008.6.1.196", "Probability (Retired) (3682)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Modifiers (Retired) (3683)</summary>
        public static readonly DicomUID Modifiers3683RETIRED = new DicomUID("1.2.840.10008.6.1.197", "Modifiers (Retired) (3683)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Trend (Retired) (3684)</summary>
        public static readonly DicomUID Trend3684RETIRED = new DicomUID("1.2.840.10008.6.1.198", "Trend (Retired) (3684)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Conjunctive Terms (Retired) (3685)</summary>
        public static readonly DicomUID ConjunctiveTerms3685RETIRED = new DicomUID("1.2.840.10008.6.1.199", "Conjunctive Terms (Retired) (3685)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: ECG Interpretive Statements (Retired) (3686)</summary>
        public static readonly DicomUID ECGInterpretiveStatements3686RETIRED = new DicomUID("1.2.840.10008.6.1.200", "ECG Interpretive Statements (Retired) (3686)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Electrophysiology Waveform Durations (3687)</summary>
        public static readonly DicomUID ElectrophysiologyWaveformDurations3687 = new DicomUID("1.2.840.10008.6.1.201", "Electrophysiology Waveform Durations (3687)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Waveform Voltages (3688)</summary>
        public static readonly DicomUID ElectrophysiologyWaveformVoltages3688 = new DicomUID("1.2.840.10008.6.1.202", "Electrophysiology Waveform Voltages (3688)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Diagnosis (3700)</summary>
        public static readonly DicomUID CathDiagnosis3700 = new DicomUID("1.2.840.10008.6.1.203", "Cath Diagnosis (3700)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Valves and Tracts (3701)</summary>
        public static readonly DicomUID CardiacValvesAndTracts3701 = new DicomUID("1.2.840.10008.6.1.204", "Cardiac Valves and Tracts (3701)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wall Motion (3703)</summary>
        public static readonly DicomUID WallMotion3703 = new DicomUID("1.2.840.10008.6.1.205", "Wall Motion (3703)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardium Wall Morphology Findings (3704)</summary>
        public static readonly DicomUID MyocardiumWallMorphologyFindings3704 = new DicomUID("1.2.840.10008.6.1.206", "Myocardium Wall Morphology Findings (3704)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chamber Size (3705)</summary>
        public static readonly DicomUID ChamberSize3705 = new DicomUID("1.2.840.10008.6.1.207", "Chamber Size (3705)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Overall Contractility (3706)</summary>
        public static readonly DicomUID OverallContractility3706 = new DicomUID("1.2.840.10008.6.1.208", "Overall Contractility (3706)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: VSD Description (3707)</summary>
        public static readonly DicomUID VSDDescription3707 = new DicomUID("1.2.840.10008.6.1.209", "VSD Description (3707)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Aortic Root Description (3709)</summary>
        public static readonly DicomUID AorticRootDescription3709 = new DicomUID("1.2.840.10008.6.1.210", "Aortic Root Description (3709)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Dominance (3710)</summary>
        public static readonly DicomUID CoronaryDominance3710 = new DicomUID("1.2.840.10008.6.1.211", "Coronary Dominance (3710)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Valvular Abnormalities (3711)</summary>
        public static readonly DicomUID ValvularAbnormalities3711 = new DicomUID("1.2.840.10008.6.1.212", "Valvular Abnormalities (3711)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Descriptors (3712)</summary>
        public static readonly DicomUID VesselDescriptors3712 = new DicomUID("1.2.840.10008.6.1.213", "Vessel Descriptors (3712)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: TIMI Flow Characteristics (3713)</summary>
        public static readonly DicomUID TIMIFlowCharacteristics3713 = new DicomUID("1.2.840.10008.6.1.214", "TIMI Flow Characteristics (3713)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Thrombus (3714)</summary>
        public static readonly DicomUID Thrombus3714 = new DicomUID("1.2.840.10008.6.1.215", "Thrombus (3714)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Margin (3715)</summary>
        public static readonly DicomUID LesionMargin3715 = new DicomUID("1.2.840.10008.6.1.216", "Lesion Margin (3715)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Severity (3716)</summary>
        public static readonly DicomUID Severity3716 = new DicomUID("1.2.840.10008.6.1.217", "Severity (3716)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardial Wall Segments (3717)</summary>
        public static readonly DicomUID MyocardialWallSegments3717 = new DicomUID("1.2.840.10008.6.1.218", "Myocardial Wall Segments (3717)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardial Wall Segments in Projection (3718)</summary>
        public static readonly DicomUID MyocardialWallSegmentsInProjection3718 = new DicomUID("1.2.840.10008.6.1.219", "Myocardial Wall Segments in Projection (3718)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Canadian Clinical Classification (3719)</summary>
        public static readonly DicomUID CanadianClinicalClassification3719 = new DicomUID("1.2.840.10008.6.1.220", "Canadian Clinical Classification (3719)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac History Dates (Retired) (3720)</summary>
        public static readonly DicomUID CardiacHistoryDates3720RETIRED = new DicomUID("1.2.840.10008.6.1.221", "Cardiac History Dates (Retired) (3720)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Cardiovascular Surgeries (3721)</summary>
        public static readonly DicomUID CardiovascularSurgeries3721 = new DicomUID("1.2.840.10008.6.1.222", "Cardiovascular Surgeries (3721)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diabetic Therapy (3722)</summary>
        public static readonly DicomUID DiabeticTherapy3722 = new DicomUID("1.2.840.10008.6.1.223", "Diabetic Therapy (3722)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MI Types (3723)</summary>
        public static readonly DicomUID MITypes3723 = new DicomUID("1.2.840.10008.6.1.224", "MI Types (3723)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Smoking History (3724)</summary>
        public static readonly DicomUID SmokingHistory3724 = new DicomUID("1.2.840.10008.6.1.225", "Smoking History (3724)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Indications for Coronary Intervention (3726)</summary>
        public static readonly DicomUID IndicationsForCoronaryIntervention3726 = new DicomUID("1.2.840.10008.6.1.226", "Indications for Coronary Intervention (3726)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Indications for Catheterization (3727)</summary>
        public static readonly DicomUID IndicationsForCatheterization3727 = new DicomUID("1.2.840.10008.6.1.227", "Indications for Catheterization (3727)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Findings (3728)</summary>
        public static readonly DicomUID CathFindings3728 = new DicomUID("1.2.840.10008.6.1.228", "Cath Findings (3728)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Admission Status (3729)</summary>
        public static readonly DicomUID AdmissionStatus3729 = new DicomUID("1.2.840.10008.6.1.229", "Admission Status (3729)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Insurance Payor (3730)</summary>
        public static readonly DicomUID InsurancePayor3730 = new DicomUID("1.2.840.10008.6.1.230", "Insurance Payor (3730)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Primary Cause of Death (3733)</summary>
        public static readonly DicomUID PrimaryCauseOfDeath3733 = new DicomUID("1.2.840.10008.6.1.231", "Primary Cause of Death (3733)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Acute Coronary Syndrome Time Period (3735)</summary>
        public static readonly DicomUID AcuteCoronarySyndromeTimePeriod3735 = new DicomUID("1.2.840.10008.6.1.232", "Acute Coronary Syndrome Time Period (3735)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: NYHA Classification (3736)</summary>
        public static readonly DicomUID NYHAClassification3736 = new DicomUID("1.2.840.10008.6.1.233", "NYHA Classification (3736)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-invasive Test - Ischemia (3737)</summary>
        public static readonly DicomUID NonInvasiveTestIschemia3737 = new DicomUID("1.2.840.10008.6.1.234", "Non-invasive Test - Ischemia (3737)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pre-Cath Angina Type (3738)</summary>
        public static readonly DicomUID PreCathAnginaType3738 = new DicomUID("1.2.840.10008.6.1.235", "Pre-Cath Angina Type (3738)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Procedure Type (3739)</summary>
        public static readonly DicomUID CathProcedureType3739 = new DicomUID("1.2.840.10008.6.1.236", "Cath Procedure Type (3739)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Thrombolytic Administration (3740)</summary>
        public static readonly DicomUID ThrombolyticAdministration3740 = new DicomUID("1.2.840.10008.6.1.237", "Thrombolytic Administration (3740)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication Administration, Lab Visit (3741)</summary>
        public static readonly DicomUID MedicationAdministrationLabVisit3741 = new DicomUID("1.2.840.10008.6.1.238", "Medication Administration, Lab Visit (3741)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication Administration, PCI (3742)</summary>
        public static readonly DicomUID MedicationAdministrationPCI3742 = new DicomUID("1.2.840.10008.6.1.239", "Medication Administration, PCI (3742)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clopidogrel/Ticlopidine Administration (3743)</summary>
        public static readonly DicomUID ClopidogrelTiclopidineAdministration3743 = new DicomUID("1.2.840.10008.6.1.240", "Clopidogrel/Ticlopidine Administration (3743)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EF Testing Method (3744)</summary>
        public static readonly DicomUID EFTestingMethod3744 = new DicomUID("1.2.840.10008.6.1.241", "EF Testing Method (3744)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calculation Method (3745)</summary>
        public static readonly DicomUID CalculationMethod3745 = new DicomUID("1.2.840.10008.6.1.242", "Calculation Method (3745)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Percutaneous Entry Site (3746)</summary>
        public static readonly DicomUID PercutaneousEntrySite3746 = new DicomUID("1.2.840.10008.6.1.243", "Percutaneous Entry Site (3746)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Percutaneous Closure (3747)</summary>
        public static readonly DicomUID PercutaneousClosure3747 = new DicomUID("1.2.840.10008.6.1.244", "Percutaneous Closure (3747)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Angiographic EF Testing Method (3748)</summary>
        public static readonly DicomUID AngiographicEFTestingMethod3748 = new DicomUID("1.2.840.10008.6.1.245", "Angiographic EF Testing Method (3748)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PCI Procedure Result (3749)</summary>
        public static readonly DicomUID PCIProcedureResult3749 = new DicomUID("1.2.840.10008.6.1.246", "PCI Procedure Result (3749)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Previously Dilated Lesion (3750)</summary>
        public static readonly DicomUID PreviouslyDilatedLesion3750 = new DicomUID("1.2.840.10008.6.1.247", "Previously Dilated Lesion (3750)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Guidewire Crossing (3752)</summary>
        public static readonly DicomUID GuidewireCrossing3752 = new DicomUID("1.2.840.10008.6.1.248", "Guidewire Crossing (3752)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Complications (3754)</summary>
        public static readonly DicomUID VascularComplications3754 = new DicomUID("1.2.840.10008.6.1.249", "Vascular Complications (3754)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Complications (3755)</summary>
        public static readonly DicomUID CathComplications3755 = new DicomUID("1.2.840.10008.6.1.250", "Cath Complications (3755)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Patient Risk Factors (3756)</summary>
        public static readonly DicomUID CardiacPatientRiskFactors3756 = new DicomUID("1.2.840.10008.6.1.251", "Cardiac Patient Risk Factors (3756)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Diagnostic Procedures (3757)</summary>
        public static readonly DicomUID CardiacDiagnosticProcedures3757 = new DicomUID("1.2.840.10008.6.1.252", "Cardiac Diagnostic Procedures (3757)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Family History (3758)</summary>
        public static readonly DicomUID CardiovascularFamilyHistory3758 = new DicomUID("1.2.840.10008.6.1.253", "Cardiovascular Family History (3758)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hypertension Therapy (3760)</summary>
        public static readonly DicomUID HypertensionTherapy3760 = new DicomUID("1.2.840.10008.6.1.254", "Hypertension Therapy (3760)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Antilipemic Agents (3761)</summary>
        public static readonly DicomUID AntilipemicAgents3761 = new DicomUID("1.2.840.10008.6.1.255", "Antilipemic Agents (3761)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Antiarrhythmic Agents (3762)</summary>
        public static readonly DicomUID AntiarrhythmicAgents3762 = new DicomUID("1.2.840.10008.6.1.256", "Antiarrhythmic Agents (3762)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardial Infarction Therapies (3764)</summary>
        public static readonly DicomUID MyocardialInfarctionTherapies3764 = new DicomUID("1.2.840.10008.6.1.257", "Myocardial Infarction Therapies (3764)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Concern Types (3769)</summary>
        public static readonly DicomUID ConcernTypes3769 = new DicomUID("1.2.840.10008.6.1.258", "Concern Types (3769)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Problem Status (3770)</summary>
        public static readonly DicomUID ProblemStatus3770 = new DicomUID("1.2.840.10008.6.1.259", "Problem Status (3770)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Health Status (3772)</summary>
        public static readonly DicomUID HealthStatus3772 = new DicomUID("1.2.840.10008.6.1.260", "Health Status (3772)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Use Status (3773)</summary>
        public static readonly DicomUID UseStatus3773 = new DicomUID("1.2.840.10008.6.1.261", "Use Status (3773)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Social History (3774)</summary>
        public static readonly DicomUID SocialHistory3774 = new DicomUID("1.2.840.10008.6.1.262", "Social History (3774)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implanted Devices (3777)</summary>
        public static readonly DicomUID ImplantedDevices3777 = new DicomUID("1.2.840.10008.6.1.263", "Implanted Devices (3777)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Plaque Structures (3802)</summary>
        public static readonly DicomUID PlaqueStructures3802 = new DicomUID("1.2.840.10008.6.1.264", "Plaque Structures (3802)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Measurement Methods (3804)</summary>
        public static readonly DicomUID StenosisMeasurementMethods3804 = new DicomUID("1.2.840.10008.6.1.265", "Stenosis Measurement Methods (3804)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Types (3805)</summary>
        public static readonly DicomUID StenosisTypes3805 = new DicomUID("1.2.840.10008.6.1.266", "Stenosis Types (3805)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Shape (3806)</summary>
        public static readonly DicomUID StenosisShape3806 = new DicomUID("1.2.840.10008.6.1.267", "Stenosis Shape (3806)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume Measurement Methods (3807)</summary>
        public static readonly DicomUID VolumeMeasurementMethods3807 = new DicomUID("1.2.840.10008.6.1.268", "Volume Measurement Methods (3807)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Aneurysm Types (3808)</summary>
        public static readonly DicomUID AneurysmTypes3808 = new DicomUID("1.2.840.10008.6.1.269", "Aneurysm Types (3808)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Associated Conditions (3809)</summary>
        public static readonly DicomUID AssociatedConditions3809 = new DicomUID("1.2.840.10008.6.1.270", "Associated Conditions (3809)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Morphology (3810)</summary>
        public static readonly DicomUID VascularMorphology3810 = new DicomUID("1.2.840.10008.6.1.271", "Vascular Morphology (3810)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stent Findings (3813)</summary>
        public static readonly DicomUID StentFindings3813 = new DicomUID("1.2.840.10008.6.1.272", "Stent Findings (3813)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stent Composition (3814)</summary>
        public static readonly DicomUID StentComposition3814 = new DicomUID("1.2.840.10008.6.1.273", "Stent Composition (3814)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source of Vascular Finding (3815)</summary>
        public static readonly DicomUID SourceOfVascularFinding3815 = new DicomUID("1.2.840.10008.6.1.274", "Source of Vascular Finding (3815)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Sclerosis Types (3817)</summary>
        public static readonly DicomUID VascularSclerosisTypes3817 = new DicomUID("1.2.840.10008.6.1.275", "Vascular Sclerosis Types (3817)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-invasive Vascular Procedures (3820)</summary>
        public static readonly DicomUID NonInvasiveVascularProcedures3820 = new DicomUID("1.2.840.10008.6.1.276", "Non-invasive Vascular Procedures (3820)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Papillary Muscle Included/Excluded (3821)</summary>
        public static readonly DicomUID PapillaryMuscleIncludedExcluded3821 = new DicomUID("1.2.840.10008.6.1.277", "Papillary Muscle Included/Excluded (3821)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiratory Status (3823)</summary>
        public static readonly DicomUID RespiratoryStatus3823 = new DicomUID("1.2.840.10008.6.1.278", "Respiratory Status (3823)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Heart Rhythm (3826)</summary>
        public static readonly DicomUID HeartRhythm3826 = new DicomUID("1.2.840.10008.6.1.279", "Heart Rhythm (3826)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Segments (3827)</summary>
        public static readonly DicomUID VesselSegments3827 = new DicomUID("1.2.840.10008.6.1.280", "Vessel Segments (3827)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pulmonary Arteries (3829)</summary>
        public static readonly DicomUID PulmonaryArteries3829 = new DicomUID("1.2.840.10008.6.1.281", "Pulmonary Arteries (3829)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Length (3831)</summary>
        public static readonly DicomUID StenosisLength3831 = new DicomUID("1.2.840.10008.6.1.282", "Stenosis Length (3831)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Grade (3832)</summary>
        public static readonly DicomUID StenosisGrade3832 = new DicomUID("1.2.840.10008.6.1.283", "Stenosis Grade (3832)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ejection Fraction (3833)</summary>
        public static readonly DicomUID CardiacEjectionFraction3833 = new DicomUID("1.2.840.10008.6.1.284", "Cardiac Ejection Fraction (3833)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Volume Measurements (3835)</summary>
        public static readonly DicomUID CardiacVolumeMeasurements3835 = new DicomUID("1.2.840.10008.6.1.285", "Cardiac Volume Measurements (3835)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time-based Perfusion Measurements (3836)</summary>
        public static readonly DicomUID TimeBasedPerfusionMeasurements3836 = new DicomUID("1.2.840.10008.6.1.286", "Time-based Perfusion Measurements (3836)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiducial Feature (3837)</summary>
        public static readonly DicomUID FiducialFeature3837 = new DicomUID("1.2.840.10008.6.1.287", "Fiducial Feature (3837)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diameter Derivation (3838)</summary>
        public static readonly DicomUID DiameterDerivation3838 = new DicomUID("1.2.840.10008.6.1.288", "Diameter Derivation (3838)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Veins (3839)</summary>
        public static readonly DicomUID CoronaryVeins3839 = new DicomUID("1.2.840.10008.6.1.289", "Coronary Veins (3839)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pulmonary Veins (3840)</summary>
        public static readonly DicomUID PulmonaryVeins3840 = new DicomUID("1.2.840.10008.6.1.290", "Pulmonary Veins (3840)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardial Subsegment (3843)</summary>
        public static readonly DicomUID MyocardialSubsegment3843 = new DicomUID("1.2.840.10008.6.1.291", "Myocardial Subsegment (3843)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Partial View Section for Mammography (4005)</summary>
        public static readonly DicomUID PartialViewSectionForMammography4005 = new DicomUID("1.2.840.10008.6.1.292", "Partial View Section for Mammography (4005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DX Anatomy Imaged (4009)</summary>
        public static readonly DicomUID DXAnatomyImaged4009 = new DicomUID("1.2.840.10008.6.1.293", "DX Anatomy Imaged (4009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DX View (4010)</summary>
        public static readonly DicomUID DXView4010 = new DicomUID("1.2.840.10008.6.1.294", "DX View (4010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DX View Modifier (4011)</summary>
        public static readonly DicomUID DXViewModifier4011 = new DicomUID("1.2.840.10008.6.1.295", "DX View Modifier (4011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Projection Eponymous Name (4012)</summary>
        public static readonly DicomUID ProjectionEponymousName4012 = new DicomUID("1.2.840.10008.6.1.296", "Projection Eponymous Name (4012)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Region for Mammography (4013)</summary>
        public static readonly DicomUID AnatomicRegionForMammography4013 = new DicomUID("1.2.840.10008.6.1.297", "Anatomic Region for Mammography (4013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: View for Mammography (4014)</summary>
        public static readonly DicomUID ViewForMammography4014 = new DicomUID("1.2.840.10008.6.1.298", "View for Mammography (4014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: View Modifier for Mammography (4015)</summary>
        public static readonly DicomUID ViewModifierForMammography4015 = new DicomUID("1.2.840.10008.6.1.299", "View Modifier for Mammography (4015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Region for Intra-oral Radiography (4016)</summary>
        public static readonly DicomUID AnatomicRegionForIntraOralRadiography4016 = new DicomUID("1.2.840.10008.6.1.300", "Anatomic Region for Intra-oral Radiography (4016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Region Modifier for Intra-oral Radiography (4017)</summary>
        public static readonly DicomUID AnatomicRegionModifierForIntraOralRadiography4017 = new DicomUID("1.2.840.10008.6.1.301", "Anatomic Region Modifier for Intra-oral Radiography (4017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Primary Anatomic Structure for Intra-oral Radiography (Permanent Dentition - Designation of Teeth) (4018)</summary>
        public static readonly DicomUID PrimaryAnatomicStructureForIntraOralRadiographyPermanentDentitionDesignationOfTeeth4018 = new DicomUID("1.2.840.10008.6.1.302", "Primary Anatomic Structure for Intra-oral Radiography (Permanent Dentition - Designation of Teeth) (4018)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Primary Anatomic Structure for Intra-oral Radiography (Deciduous Dentition - Designation of Teeth) (4019)</summary>
        public static readonly DicomUID PrimaryAnatomicStructureForIntraOralRadiographyDeciduousDentitionDesignationOfTeeth4019 = new DicomUID("1.2.840.10008.6.1.303", "Primary Anatomic Structure for Intra-oral Radiography (Deciduous Dentition - Designation of Teeth) (4019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Radionuclide (4020)</summary>
        public static readonly DicomUID PETRadionuclide4020 = new DicomUID("1.2.840.10008.6.1.304", "PET Radionuclide (4020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Radiopharmaceutical (4021)</summary>
        public static readonly DicomUID PETRadiopharmaceutical4021 = new DicomUID("1.2.840.10008.6.1.305", "PET Radiopharmaceutical (4021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Craniofacial Anatomic Regions (4028)</summary>
        public static readonly DicomUID CraniofacialAnatomicRegions4028 = new DicomUID("1.2.840.10008.6.1.306", "Craniofacial Anatomic Regions (4028)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT, MR and PET Anatomy Imaged (4030)</summary>
        public static readonly DicomUID CTMRAndPETAnatomyImaged4030 = new DicomUID("1.2.840.10008.6.1.307", "CT, MR and PET Anatomy Imaged (4030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Common Anatomic Regions (4031)</summary>
        public static readonly DicomUID CommonAnatomicRegions4031 = new DicomUID("1.2.840.10008.6.1.308", "Common Anatomic Regions (4031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Spectroscopy Metabolites (4032)</summary>
        public static readonly DicomUID MRSpectroscopyMetabolites4032 = new DicomUID("1.2.840.10008.6.1.309", "MR Spectroscopy Metabolites (4032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Proton Spectroscopy Metabolites (4033)</summary>
        public static readonly DicomUID MRProtonSpectroscopyMetabolites4033 = new DicomUID("1.2.840.10008.6.1.310", "MR Proton Spectroscopy Metabolites (4033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Endoscopy Anatomic Regions (4040)</summary>
        public static readonly DicomUID EndoscopyAnatomicRegions4040 = new DicomUID("1.2.840.10008.6.1.311", "Endoscopy Anatomic Regions (4040)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: XA/XRF Anatomy Imaged (4042)</summary>
        public static readonly DicomUID XAXRFAnatomyImaged4042 = new DicomUID("1.2.840.10008.6.1.312", "XA/XRF Anatomy Imaged (4042)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Drug or Contrast Agent Characteristics (4050)</summary>
        public static readonly DicomUID DrugOrContrastAgentCharacteristics4050 = new DicomUID("1.2.840.10008.6.1.313", "Drug or Contrast Agent Characteristics (4050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Devices (4051)</summary>
        public static readonly DicomUID GeneralDevices4051 = new DicomUID("1.2.840.10008.6.1.314", "General Devices (4051)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Phantom Devices (4052)</summary>
        public static readonly DicomUID PhantomDevices4052 = new DicomUID("1.2.840.10008.6.1.315", "Phantom Devices (4052)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Imaging Agent (4200)</summary>
        public static readonly DicomUID OphthalmicImagingAgent4200 = new DicomUID("1.2.840.10008.6.1.316", "Ophthalmic Imaging Agent (4200)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Eye Movement Command (4201)</summary>
        public static readonly DicomUID PatientEyeMovementCommand4201 = new DicomUID("1.2.840.10008.6.1.317", "Patient Eye Movement Command (4201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Photography Acquisition Device (4202)</summary>
        public static readonly DicomUID OphthalmicPhotographyAcquisitionDevice4202 = new DicomUID("1.2.840.10008.6.1.318", "Ophthalmic Photography Acquisition Device (4202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Photography Illumination (4203)</summary>
        public static readonly DicomUID OphthalmicPhotographyIllumination4203 = new DicomUID("1.2.840.10008.6.1.319", "Ophthalmic Photography Illumination (4203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Filter (4204)</summary>
        public static readonly DicomUID OphthalmicFilter4204 = new DicomUID("1.2.840.10008.6.1.320", "Ophthalmic Filter (4204)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Lens (4205)</summary>
        public static readonly DicomUID OphthalmicLens4205 = new DicomUID("1.2.840.10008.6.1.321", "Ophthalmic Lens (4205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Channel Description (4206)</summary>
        public static readonly DicomUID OphthalmicChannelDescription4206 = new DicomUID("1.2.840.10008.6.1.322", "Ophthalmic Channel Description (4206)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Image Position (4207)</summary>
        public static readonly DicomUID OphthalmicImagePosition4207 = new DicomUID("1.2.840.10008.6.1.323", "Ophthalmic Image Position (4207)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mydriatic Agent (4208)</summary>
        public static readonly DicomUID MydriaticAgent4208 = new DicomUID("1.2.840.10008.6.1.324", "Mydriatic Agent (4208)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Anatomic Structure Imaged (4209)</summary>
        public static readonly DicomUID OphthalmicAnatomicStructureImaged4209 = new DicomUID("1.2.840.10008.6.1.325", "Ophthalmic Anatomic Structure Imaged (4209)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Tomography Acquisition Device (4210)</summary>
        public static readonly DicomUID OphthalmicTomographyAcquisitionDevice4210 = new DicomUID("1.2.840.10008.6.1.326", "Ophthalmic Tomography Acquisition Device (4210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic OCT Anatomic Structure Imaged (4211)</summary>
        public static readonly DicomUID OphthalmicOCTAnatomicStructureImaged4211 = new DicomUID("1.2.840.10008.6.1.327", "Ophthalmic OCT Anatomic Structure Imaged (4211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Languages (5000)</summary>
        public static readonly DicomUID Languages5000 = new DicomUID("1.2.840.10008.6.1.328", "Languages (5000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Countries (5001)</summary>
        public static readonly DicomUID Countries5001 = new DicomUID("1.2.840.10008.6.1.329", "Countries (5001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Overall Breast Composition (6000)</summary>
        public static readonly DicomUID OverallBreastComposition6000 = new DicomUID("1.2.840.10008.6.1.330", "Overall Breast Composition (6000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Overall Breast Composition from BI-RADS® (6001)</summary>
        public static readonly DicomUID OverallBreastCompositionFromBIRADS6001 = new DicomUID("1.2.840.10008.6.1.331", "Overall Breast Composition from BI-RADS® (6001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Change Since Last Mammogram or Prior Surgery (6002)</summary>
        public static readonly DicomUID ChangeSinceLastMammogramOrPriorSurgery6002 = new DicomUID("1.2.840.10008.6.1.332", "Change Since Last Mammogram or Prior Surgery (6002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Change Since Last Mammogram or Prior Surgery from BI-RADS® (6003)</summary>
        public static readonly DicomUID ChangeSinceLastMammogramOrPriorSurgeryFromBIRADS6003 = new DicomUID("1.2.840.10008.6.1.333", "Change Since Last Mammogram or Prior Surgery from BI-RADS® (6003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Characteristics of Shape (6004)</summary>
        public static readonly DicomUID MammographyCharacteristicsOfShape6004 = new DicomUID("1.2.840.10008.6.1.334", "Mammography Characteristics of Shape (6004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Characteristics of Shape from BI-RADS® (6005)</summary>
        public static readonly DicomUID CharacteristicsOfShapeFromBIRADS6005 = new DicomUID("1.2.840.10008.6.1.335", "Characteristics of Shape from BI-RADS® (6005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Characteristics of Margin (6006)</summary>
        public static readonly DicomUID MammographyCharacteristicsOfMargin6006 = new DicomUID("1.2.840.10008.6.1.336", "Mammography Characteristics of Margin (6006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Characteristics of Margin from BI-RADS® (6007)</summary>
        public static readonly DicomUID CharacteristicsOfMarginFromBIRADS6007 = new DicomUID("1.2.840.10008.6.1.337", "Characteristics of Margin from BI-RADS® (6007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Density Modifier (6008)</summary>
        public static readonly DicomUID DensityModifier6008 = new DicomUID("1.2.840.10008.6.1.338", "Density Modifier (6008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Density Modifier from BI-RADS® (6009)</summary>
        public static readonly DicomUID DensityModifierFromBIRADS6009 = new DicomUID("1.2.840.10008.6.1.339", "Density Modifier from BI-RADS® (6009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Calcification Types (6010)</summary>
        public static readonly DicomUID MammographyCalcificationTypes6010 = new DicomUID("1.2.840.10008.6.1.340", "Mammography Calcification Types (6010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcification Types from BI-RADS® (6011)</summary>
        public static readonly DicomUID CalcificationTypesFromBIRADS6011 = new DicomUID("1.2.840.10008.6.1.341", "Calcification Types from BI-RADS® (6011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcification Distribution Modifier (6012)</summary>
        public static readonly DicomUID CalcificationDistributionModifier6012 = new DicomUID("1.2.840.10008.6.1.342", "Calcification Distribution Modifier (6012)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcification Distribution Modifier from BI-RADS® (6013)</summary>
        public static readonly DicomUID CalcificationDistributionModifierFromBIRADS6013 = new DicomUID("1.2.840.10008.6.1.343", "Calcification Distribution Modifier from BI-RADS® (6013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Single Image Finding (6014)</summary>
        public static readonly DicomUID MammographySingleImageFinding6014 = new DicomUID("1.2.840.10008.6.1.344", "Mammography Single Image Finding (6014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Single Image Finding from BI-RADS® (6015)</summary>
        public static readonly DicomUID SingleImageFindingFromBIRADS6015 = new DicomUID("1.2.840.10008.6.1.345", "Single Image Finding from BI-RADS® (6015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Composite Feature (6016)</summary>
        public static readonly DicomUID MammographyCompositeFeature6016 = new DicomUID("1.2.840.10008.6.1.346", "Mammography Composite Feature (6016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Composite Feature from BI-RADS® (6017)</summary>
        public static readonly DicomUID CompositeFeatureFromBIRADS6017 = new DicomUID("1.2.840.10008.6.1.347", "Composite Feature from BI-RADS® (6017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clockface Location or Region (6018)</summary>
        public static readonly DicomUID ClockfaceLocationOrRegion6018 = new DicomUID("1.2.840.10008.6.1.348", "Clockface Location or Region (6018)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clockface Location or Region from BI-RADS® (6019)</summary>
        public static readonly DicomUID ClockfaceLocationOrRegionFromBIRADS6019 = new DicomUID("1.2.840.10008.6.1.349", "Clockface Location or Region from BI-RADS® (6019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quadrant Location (6020)</summary>
        public static readonly DicomUID QuadrantLocation6020 = new DicomUID("1.2.840.10008.6.1.350", "Quadrant Location (6020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quadrant Location from BI-RADS® (6021)</summary>
        public static readonly DicomUID QuadrantLocationFromBIRADS6021 = new DicomUID("1.2.840.10008.6.1.351", "Quadrant Location from BI-RADS® (6021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Side (6022)</summary>
        public static readonly DicomUID Side6022 = new DicomUID("1.2.840.10008.6.1.352", "Side (6022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Side from BI-RADS® (6023)</summary>
        public static readonly DicomUID SideFromBIRADS6023 = new DicomUID("1.2.840.10008.6.1.353", "Side from BI-RADS® (6023)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Depth (6024)</summary>
        public static readonly DicomUID Depth6024 = new DicomUID("1.2.840.10008.6.1.354", "Depth (6024)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Depth from BI-RADS® (6025)</summary>
        public static readonly DicomUID DepthFromBIRADS6025 = new DicomUID("1.2.840.10008.6.1.355", "Depth from BI-RADS® (6025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Assessment (6026)</summary>
        public static readonly DicomUID MammographyAssessment6026 = new DicomUID("1.2.840.10008.6.1.356", "Mammography Assessment (6026)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Assessment from BI-RADS® (6027)</summary>
        public static readonly DicomUID AssessmentFromBIRADS6027 = new DicomUID("1.2.840.10008.6.1.357", "Assessment from BI-RADS® (6027)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Recommended Follow-up (6028)</summary>
        public static readonly DicomUID MammographyRecommendedFollowUp6028 = new DicomUID("1.2.840.10008.6.1.358", "Mammography Recommended Follow-up (6028)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Recommended Follow-up from BI-RADS® (6029)</summary>
        public static readonly DicomUID RecommendedFollowUpFromBIRADS6029 = new DicomUID("1.2.840.10008.6.1.359", "Recommended Follow-up from BI-RADS® (6029)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Pathology Codes (6030)</summary>
        public static readonly DicomUID MammographyPathologyCodes6030 = new DicomUID("1.2.840.10008.6.1.360", "Mammography Pathology Codes (6030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Benign Pathology Codes from BI-RADS® (6031)</summary>
        public static readonly DicomUID BenignPathologyCodesFromBIRADS6031 = new DicomUID("1.2.840.10008.6.1.361", "Benign Pathology Codes from BI-RADS® (6031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: High Risk Lesions Pathology Codes from BI-RADS® (6032)</summary>
        public static readonly DicomUID HighRiskLesionsPathologyCodesFromBIRADS6032 = new DicomUID("1.2.840.10008.6.1.362", "High Risk Lesions Pathology Codes from BI-RADS® (6032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Malignant Pathology Codes from BI-RADS® (6033)</summary>
        public static readonly DicomUID MalignantPathologyCodesFromBIRADS6033 = new DicomUID("1.2.840.10008.6.1.363", "Malignant Pathology Codes from BI-RADS® (6033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intended Use of CAD Output (6034)</summary>
        public static readonly DicomUID IntendedUseOfCADOutput6034 = new DicomUID("1.2.840.10008.6.1.364", "Intended Use of CAD Output (6034)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Composite Feature Relations (6035)</summary>
        public static readonly DicomUID CompositeFeatureRelations6035 = new DicomUID("1.2.840.10008.6.1.365", "Composite Feature Relations (6035)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Scope of Feature (6036)</summary>
        public static readonly DicomUID ScopeOfFeature6036 = new DicomUID("1.2.840.10008.6.1.366", "Scope of Feature (6036)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Quantitative Temporal Difference Type (6037)</summary>
        public static readonly DicomUID MammographyQuantitativeTemporalDifferenceType6037 = new DicomUID("1.2.840.10008.6.1.367", "Mammography Quantitative Temporal Difference Type (6037)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Qualitative Temporal Difference Type (6038)</summary>
        public static readonly DicomUID MammographyQualitativeTemporalDifferenceType6038 = new DicomUID("1.2.840.10008.6.1.368", "Mammography Qualitative Temporal Difference Type (6038)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nipple Characteristic (6039)</summary>
        public static readonly DicomUID NippleCharacteristic6039 = new DicomUID("1.2.840.10008.6.1.369", "Nipple Characteristic (6039)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Object Type (6040)</summary>
        public static readonly DicomUID NonLesionObjectType6040 = new DicomUID("1.2.840.10008.6.1.370", "Non-lesion Object Type (6040)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Image Quality Finding (6041)</summary>
        public static readonly DicomUID MammographyImageQualityFinding6041 = new DicomUID("1.2.840.10008.6.1.371", "Mammography Image Quality Finding (6041)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Status of Results (6042)</summary>
        public static readonly DicomUID StatusOfResults6042 = new DicomUID("1.2.840.10008.6.1.372", "Status of Results (6042)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Types of Mammography CAD Analysis (6043)</summary>
        public static readonly DicomUID TypesOfMammographyCADAnalysis6043 = new DicomUID("1.2.840.10008.6.1.373", "Types of Mammography CAD Analysis (6043)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Types of Image Quality Assessment (6044)</summary>
        public static readonly DicomUID TypesOfImageQualityAssessment6044 = new DicomUID("1.2.840.10008.6.1.374", "Types of Image Quality Assessment (6044)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Types of Quality Control Standard (6045)</summary>
        public static readonly DicomUID MammographyTypesOfQualityControlStandard6045 = new DicomUID("1.2.840.10008.6.1.375", "Mammography Types of Quality Control Standard (6045)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Follow-up Interval (6046)</summary>
        public static readonly DicomUID UnitsOfFollowUpInterval6046 = new DicomUID("1.2.840.10008.6.1.376", "Units of Follow-up Interval (6046)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD Processing and Findings Summary (6047)</summary>
        public static readonly DicomUID CADProcessingAndFindingsSummary6047 = new DicomUID("1.2.840.10008.6.1.377", "CAD Processing and Findings Summary (6047)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD Operating Point Axis Label (6048)</summary>
        public static readonly DicomUID CADOperatingPointAxisLabel6048 = new DicomUID("1.2.840.10008.6.1.378", "CAD Operating Point Axis Label (6048)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Procedure Reported (6050)</summary>
        public static readonly DicomUID BreastProcedureReported6050 = new DicomUID("1.2.840.10008.6.1.379", "Breast Procedure Reported (6050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Procedure Reason (6051)</summary>
        public static readonly DicomUID BreastProcedureReason6051 = new DicomUID("1.2.840.10008.6.1.380", "Breast Procedure Reason (6051)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Report Section Title (6052)</summary>
        public static readonly DicomUID BreastImagingReportSectionTitle6052 = new DicomUID("1.2.840.10008.6.1.381", "Breast Imaging Report Section Title (6052)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Report Elements (6053)</summary>
        public static readonly DicomUID BreastImagingReportElements6053 = new DicomUID("1.2.840.10008.6.1.382", "Breast Imaging Report Elements (6053)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Findings (6054)</summary>
        public static readonly DicomUID BreastImagingFindings6054 = new DicomUID("1.2.840.10008.6.1.383", "Breast Imaging Findings (6054)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Clinical Finding or Indicated Problem (6055)</summary>
        public static readonly DicomUID BreastClinicalFindingOrIndicatedProblem6055 = new DicomUID("1.2.840.10008.6.1.384", "Breast Clinical Finding or Indicated Problem (6055)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Associated Findings for Breast (6056)</summary>
        public static readonly DicomUID AssociatedFindingsForBreast6056 = new DicomUID("1.2.840.10008.6.1.385", "Associated Findings for Breast (6056)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ductography Findings for Breast (6057)</summary>
        public static readonly DicomUID DuctographyFindingsForBreast6057 = new DicomUID("1.2.840.10008.6.1.386", "Ductography Findings for Breast (6057)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Modifiers for Breast (6058)</summary>
        public static readonly DicomUID ProcedureModifiersForBreast6058 = new DicomUID("1.2.840.10008.6.1.387", "Procedure Modifiers for Breast (6058)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Implant Types (6059)</summary>
        public static readonly DicomUID BreastImplantTypes6059 = new DicomUID("1.2.840.10008.6.1.388", "Breast Implant Types (6059)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Biopsy Techniques (6060)</summary>
        public static readonly DicomUID BreastBiopsyTechniques6060 = new DicomUID("1.2.840.10008.6.1.389", "Breast Biopsy Techniques (6060)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Procedure Modifiers (6061)</summary>
        public static readonly DicomUID BreastImagingProcedureModifiers6061 = new DicomUID("1.2.840.10008.6.1.390", "Breast Imaging Procedure Modifiers (6061)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interventional Procedure Complications (6062)</summary>
        public static readonly DicomUID InterventionalProcedureComplications6062 = new DicomUID("1.2.840.10008.6.1.391", "Interventional Procedure Complications (6062)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interventional Procedure Results (6063)</summary>
        public static readonly DicomUID InterventionalProcedureResults6063 = new DicomUID("1.2.840.10008.6.1.392", "Interventional Procedure Results (6063)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Findings for Breast (6064)</summary>
        public static readonly DicomUID UltrasoundFindingsForBreast6064 = new DicomUID("1.2.840.10008.6.1.393", "Ultrasound Findings for Breast (6064)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Instrument Approach (6065)</summary>
        public static readonly DicomUID InstrumentApproach6065 = new DicomUID("1.2.840.10008.6.1.394", "Instrument Approach (6065)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Target Confirmation (6066)</summary>
        public static readonly DicomUID TargetConfirmation6066 = new DicomUID("1.2.840.10008.6.1.395", "Target Confirmation (6066)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fluid Color (6067)</summary>
        public static readonly DicomUID FluidColor6067 = new DicomUID("1.2.840.10008.6.1.396", "Fluid Color (6067)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tumor Stages From AJCC (6068)</summary>
        public static readonly DicomUID TumorStagesFromAJCC6068 = new DicomUID("1.2.840.10008.6.1.397", "Tumor Stages From AJCC (6068)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nottingham Combined Histologic Grade (6069)</summary>
        public static readonly DicomUID NottinghamCombinedHistologicGrade6069 = new DicomUID("1.2.840.10008.6.1.398", "Nottingham Combined Histologic Grade (6069)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Bloom-Richardson Histologic Grade (6070)</summary>
        public static readonly DicomUID BloomRichardsonHistologicGrade6070 = new DicomUID("1.2.840.10008.6.1.399", "Bloom-Richardson Histologic Grade (6070)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Histologic Grading Method (6071)</summary>
        public static readonly DicomUID HistologicGradingMethod6071 = new DicomUID("1.2.840.10008.6.1.400", "Histologic Grading Method (6071)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Implant Findings (6072)</summary>
        public static readonly DicomUID BreastImplantFindings6072 = new DicomUID("1.2.840.10008.6.1.401", "Breast Implant Findings (6072)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gynecological Hormones (6080)</summary>
        public static readonly DicomUID GynecologicalHormones6080 = new DicomUID("1.2.840.10008.6.1.402", "Gynecological Hormones (6080)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Cancer Risk Factors (6081)</summary>
        public static readonly DicomUID BreastCancerRiskFactors6081 = new DicomUID("1.2.840.10008.6.1.403", "Breast Cancer Risk Factors (6081)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gynecological Procedures (6082)</summary>
        public static readonly DicomUID GynecologicalProcedures6082 = new DicomUID("1.2.840.10008.6.1.404", "Gynecological Procedures (6082)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedures for Breast (6083)</summary>
        public static readonly DicomUID ProceduresForBreast6083 = new DicomUID("1.2.840.10008.6.1.405", "Procedures for Breast (6083)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammoplasty Procedures (6084)</summary>
        public static readonly DicomUID MammoplastyProcedures6084 = new DicomUID("1.2.840.10008.6.1.406", "Mammoplasty Procedures (6084)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Therapies for Breast (6085)</summary>
        public static readonly DicomUID TherapiesForBreast6085 = new DicomUID("1.2.840.10008.6.1.407", "Therapies for Breast (6085)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Menopausal Phase (6086)</summary>
        public static readonly DicomUID MenopausalPhase6086 = new DicomUID("1.2.840.10008.6.1.408", "Menopausal Phase (6086)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Risk Factors (6087)</summary>
        public static readonly DicomUID GeneralRiskFactors6087 = new DicomUID("1.2.840.10008.6.1.409", "General Risk Factors (6087)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Maternal Risk Factors (6088)</summary>
        public static readonly DicomUID OBGYNMaternalRiskFactors6088 = new DicomUID("1.2.840.10008.6.1.410", "OB-GYN Maternal Risk Factors (6088)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Substances (6089)</summary>
        public static readonly DicomUID Substances6089 = new DicomUID("1.2.840.10008.6.1.411", "Substances (6089)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Usage, Exposure Amount (6090)</summary>
        public static readonly DicomUID RelativeUsageExposureAmount6090 = new DicomUID("1.2.840.10008.6.1.412", "Relative Usage, Exposure Amount (6090)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Frequency of Event Values (6091)</summary>
        public static readonly DicomUID RelativeFrequencyOfEventValues6091 = new DicomUID("1.2.840.10008.6.1.413", "Relative Frequency of Event Values (6091)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quantitative Concepts for Usage, Exposure (6092)</summary>
        public static readonly DicomUID QuantitativeConceptsForUsageExposure6092 = new DicomUID("1.2.840.10008.6.1.414", "Quantitative Concepts for Usage, Exposure (6092)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Qualitative Concepts for Usage, Exposure Amount (6093)</summary>
        public static readonly DicomUID QualitativeConceptsForUsageExposureAmount6093 = new DicomUID("1.2.840.10008.6.1.415", "Qualitative Concepts for Usage, Exposure Amount (6093)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Qualitative Concepts for Usage, Exposure Frequency (6094)</summary>
        public static readonly DicomUID QualitativeConceptsForUsageExposureFrequency6094 = new DicomUID("1.2.840.10008.6.1.416", "Qualitative Concepts for Usage, Exposure Frequency (6094)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Properties of Procedures (6095)</summary>
        public static readonly DicomUID NumericPropertiesOfProcedures6095 = new DicomUID("1.2.840.10008.6.1.417", "Numeric Properties of Procedures (6095)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pregnancy Status (6096)</summary>
        public static readonly DicomUID PregnancyStatus6096 = new DicomUID("1.2.840.10008.6.1.418", "Pregnancy Status (6096)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Side of Family (6097)</summary>
        public static readonly DicomUID SideOfFamily6097 = new DicomUID("1.2.840.10008.6.1.419", "Side of Family (6097)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Component Categories (6100)</summary>
        public static readonly DicomUID ChestComponentCategories6100 = new DicomUID("1.2.840.10008.6.1.420", "Chest Component Categories (6100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Finding or Feature (6101)</summary>
        public static readonly DicomUID ChestFindingOrFeature6101 = new DicomUID("1.2.840.10008.6.1.421", "Chest Finding or Feature (6101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Finding or Feature Modifier (6102)</summary>
        public static readonly DicomUID ChestFindingOrFeatureModifier6102 = new DicomUID("1.2.840.10008.6.1.422", "Chest Finding or Feature Modifier (6102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abnormal Lines Finding or Feature (6103)</summary>
        public static readonly DicomUID AbnormalLinesFindingOrFeature6103 = new DicomUID("1.2.840.10008.6.1.423", "Abnormal Lines Finding or Feature (6103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abnormal Opacity Finding or Feature (6104)</summary>
        public static readonly DicomUID AbnormalOpacityFindingOrFeature6104 = new DicomUID("1.2.840.10008.6.1.424", "Abnormal Opacity Finding or Feature (6104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abnormal Lucency Finding or Feature (6105)</summary>
        public static readonly DicomUID AbnormalLucencyFindingOrFeature6105 = new DicomUID("1.2.840.10008.6.1.425", "Abnormal Lucency Finding or Feature (6105)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abnormal Texture Finding or Feature (6106)</summary>
        public static readonly DicomUID AbnormalTextureFindingOrFeature6106 = new DicomUID("1.2.840.10008.6.1.426", "Abnormal Texture Finding or Feature (6106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Width Descriptor (6107)</summary>
        public static readonly DicomUID WidthDescriptor6107 = new DicomUID("1.2.840.10008.6.1.427", "Width Descriptor (6107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Anatomic Structure Abnormal Distribution (6108)</summary>
        public static readonly DicomUID ChestAnatomicStructureAbnormalDistribution6108 = new DicomUID("1.2.840.10008.6.1.428", "Chest Anatomic Structure Abnormal Distribution (6108)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiographic Anatomy Finding or Feature (6109)</summary>
        public static readonly DicomUID RadiographicAnatomyFindingOrFeature6109 = new DicomUID("1.2.840.10008.6.1.429", "Radiographic Anatomy Finding or Feature (6109)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lung Anatomy Finding or Feature (6110)</summary>
        public static readonly DicomUID LungAnatomyFindingOrFeature6110 = new DicomUID("1.2.840.10008.6.1.430", "Lung Anatomy Finding or Feature (6110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Bronchovascular Anatomy Finding or Feature (6111)</summary>
        public static readonly DicomUID BronchovascularAnatomyFindingOrFeature6111 = new DicomUID("1.2.840.10008.6.1.431", "Bronchovascular Anatomy Finding or Feature (6111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pleura Anatomy Finding or Feature (6112)</summary>
        public static readonly DicomUID PleuraAnatomyFindingOrFeature6112 = new DicomUID("1.2.840.10008.6.1.432", "Pleura Anatomy Finding or Feature (6112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mediastinum Anatomy Finding or Feature (6113)</summary>
        public static readonly DicomUID MediastinumAnatomyFindingOrFeature6113 = new DicomUID("1.2.840.10008.6.1.433", "Mediastinum Anatomy Finding or Feature (6113)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Osseous Anatomy Finding or Feature (6114)</summary>
        public static readonly DicomUID OsseousAnatomyFindingOrFeature6114 = new DicomUID("1.2.840.10008.6.1.434", "Osseous Anatomy Finding or Feature (6114)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Osseous Anatomy Modifiers (6115)</summary>
        public static readonly DicomUID OsseousAnatomyModifiers6115 = new DicomUID("1.2.840.10008.6.1.435", "Osseous Anatomy Modifiers (6115)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Muscular Anatomy (6116)</summary>
        public static readonly DicomUID MuscularAnatomy6116 = new DicomUID("1.2.840.10008.6.1.436", "Muscular Anatomy (6116)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Anatomy (6117)</summary>
        public static readonly DicomUID VascularAnatomy6117 = new DicomUID("1.2.840.10008.6.1.437", "Vascular Anatomy (6117)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Size Descriptor (6118)</summary>
        public static readonly DicomUID SizeDescriptor6118 = new DicomUID("1.2.840.10008.6.1.438", "Size Descriptor (6118)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Border Shape (6119)</summary>
        public static readonly DicomUID ChestBorderShape6119 = new DicomUID("1.2.840.10008.6.1.439", "Chest Border Shape (6119)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Border Definition (6120)</summary>
        public static readonly DicomUID ChestBorderDefinition6120 = new DicomUID("1.2.840.10008.6.1.440", "Chest Border Definition (6120)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Orientation Descriptor (6121)</summary>
        public static readonly DicomUID ChestOrientationDescriptor6121 = new DicomUID("1.2.840.10008.6.1.441", "Chest Orientation Descriptor (6121)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Content Descriptor (6122)</summary>
        public static readonly DicomUID ChestContentDescriptor6122 = new DicomUID("1.2.840.10008.6.1.442", "Chest Content Descriptor (6122)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Opacity Descriptor (6123)</summary>
        public static readonly DicomUID ChestOpacityDescriptor6123 = new DicomUID("1.2.840.10008.6.1.443", "Chest Opacity Descriptor (6123)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Location in Chest (6124)</summary>
        public static readonly DicomUID LocationInChest6124 = new DicomUID("1.2.840.10008.6.1.444", "Location in Chest (6124)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Chest Location (6125)</summary>
        public static readonly DicomUID GeneralChestLocation6125 = new DicomUID("1.2.840.10008.6.1.445", "General Chest Location (6125)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Location in Lung (6126)</summary>
        public static readonly DicomUID LocationInLung6126 = new DicomUID("1.2.840.10008.6.1.446", "Location in Lung (6126)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segment Location in Lung (6127)</summary>
        public static readonly DicomUID SegmentLocationInLung6127 = new DicomUID("1.2.840.10008.6.1.447", "Segment Location in Lung (6127)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Distribution Descriptor (6128)</summary>
        public static readonly DicomUID ChestDistributionDescriptor6128 = new DicomUID("1.2.840.10008.6.1.448", "Chest Distribution Descriptor (6128)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Site Involvement (6129)</summary>
        public static readonly DicomUID ChestSiteInvolvement6129 = new DicomUID("1.2.840.10008.6.1.449", "Chest Site Involvement (6129)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Severity Descriptor (6130)</summary>
        public static readonly DicomUID SeverityDescriptor6130 = new DicomUID("1.2.840.10008.6.1.450", "Severity Descriptor (6130)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Texture Descriptor (6131)</summary>
        public static readonly DicomUID ChestTextureDescriptor6131 = new DicomUID("1.2.840.10008.6.1.451", "Chest Texture Descriptor (6131)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Calcification Descriptor (6132)</summary>
        public static readonly DicomUID ChestCalcificationDescriptor6132 = new DicomUID("1.2.840.10008.6.1.452", "Chest Calcification Descriptor (6132)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Quantitative Temporal Difference Type (6133)</summary>
        public static readonly DicomUID ChestQuantitativeTemporalDifferenceType6133 = new DicomUID("1.2.840.10008.6.1.453", "Chest Quantitative Temporal Difference Type (6133)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Qualitative Temporal Difference Type (6134)</summary>
        public static readonly DicomUID ChestQualitativeTemporalDifferenceType6134 = new DicomUID("1.2.840.10008.6.1.454", "Chest Qualitative Temporal Difference Type (6134)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Quality Finding (6135)</summary>
        public static readonly DicomUID ImageQualityFinding6135 = new DicomUID("1.2.840.10008.6.1.455", "Image Quality Finding (6135)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Types of Quality Control Standard (6136)</summary>
        public static readonly DicomUID ChestTypesOfQualityControlStandard6136 = new DicomUID("1.2.840.10008.6.1.456", "Chest Types of Quality Control Standard (6136)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Types of CAD Analysis (6137)</summary>
        public static readonly DicomUID TypesOfCADAnalysis6137 = new DicomUID("1.2.840.10008.6.1.457", "Types of CAD Analysis (6137)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Non-lesion Object Type (6138)</summary>
        public static readonly DicomUID ChestNonLesionObjectType6138 = new DicomUID("1.2.840.10008.6.1.458", "Chest Non-lesion Object Type (6138)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Modifiers (6139)</summary>
        public static readonly DicomUID NonLesionModifiers6139 = new DicomUID("1.2.840.10008.6.1.459", "Non-lesion Modifiers (6139)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calculation Methods (6140)</summary>
        public static readonly DicomUID CalculationMethods6140 = new DicomUID("1.2.840.10008.6.1.460", "Calculation Methods (6140)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuation Coefficient Measurements (6141)</summary>
        public static readonly DicomUID AttenuationCoefficientMeasurements6141 = new DicomUID("1.2.840.10008.6.1.461", "Attenuation Coefficient Measurements (6141)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calculated Value (6142)</summary>
        public static readonly DicomUID CalculatedValue6142 = new DicomUID("1.2.840.10008.6.1.462", "Calculated Value (6142)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Response (6143)</summary>
        public static readonly DicomUID LesionResponse6143 = new DicomUID("1.2.840.10008.6.1.463", "Lesion Response (6143)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RECIST Defined Lesion Response (6144)</summary>
        public static readonly DicomUID RECISTDefinedLesionResponse6144 = new DicomUID("1.2.840.10008.6.1.464", "RECIST Defined Lesion Response (6144)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Baseline Category (6145)</summary>
        public static readonly DicomUID BaselineCategory6145 = new DicomUID("1.2.840.10008.6.1.465", "Baseline Category (6145)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Background Echotexture (6151)</summary>
        public static readonly DicomUID BackgroundEchotexture6151 = new DicomUID("1.2.840.10008.6.1.466", "Background Echotexture (6151)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Orientation (6152)</summary>
        public static readonly DicomUID Orientation6152 = new DicomUID("1.2.840.10008.6.1.467", "Orientation (6152)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Boundary (6153)</summary>
        public static readonly DicomUID LesionBoundary6153 = new DicomUID("1.2.840.10008.6.1.468", "Lesion Boundary (6153)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Pattern (6154)</summary>
        public static readonly DicomUID EchoPattern6154 = new DicomUID("1.2.840.10008.6.1.469", "Echo Pattern (6154)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Posterior Acoustic Features (6155)</summary>
        public static readonly DicomUID PosteriorAcousticFeatures6155 = new DicomUID("1.2.840.10008.6.1.470", "Posterior Acoustic Features (6155)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascularity (6157)</summary>
        public static readonly DicomUID Vascularity6157 = new DicomUID("1.2.840.10008.6.1.471", "Vascularity (6157)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Correlation to Other Findings (6158)</summary>
        public static readonly DicomUID CorrelationToOtherFindings6158 = new DicomUID("1.2.840.10008.6.1.472", "Correlation to Other Findings (6158)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Malignancy Type (6159)</summary>
        public static readonly DicomUID MalignancyType6159 = new DicomUID("1.2.840.10008.6.1.473", "Malignancy Type (6159)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Primary Tumor Assessment From AJCC (6160)</summary>
        public static readonly DicomUID BreastPrimaryTumorAssessmentFromAJCC6160 = new DicomUID("1.2.840.10008.6.1.474", "Breast Primary Tumor Assessment From AJCC (6160)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clinical Regional Lymph Node Assessment for Breast (6161)</summary>
        public static readonly DicomUID ClinicalRegionalLymphNodeAssessmentForBreast6161 = new DicomUID("1.2.840.10008.6.1.475", "Clinical Regional Lymph Node Assessment for Breast (6161)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Assessment of Metastasis for Breast (6162)</summary>
        public static readonly DicomUID AssessmentOfMetastasisForBreast6162 = new DicomUID("1.2.840.10008.6.1.476", "Assessment of Metastasis for Breast (6162)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Menstrual Cycle Phase (6163)</summary>
        public static readonly DicomUID MenstrualCyclePhase6163 = new DicomUID("1.2.840.10008.6.1.477", "Menstrual Cycle Phase (6163)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time Intervals (6164)</summary>
        public static readonly DicomUID TimeIntervals6164 = new DicomUID("1.2.840.10008.6.1.478", "Time Intervals (6164)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Linear Measurements (6165)</summary>
        public static readonly DicomUID BreastLinearMeasurements6165 = new DicomUID("1.2.840.10008.6.1.479", "Breast Linear Measurements (6165)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD Geometry Secondary Graphical Representation (6166)</summary>
        public static readonly DicomUID CADGeometrySecondaryGraphicalRepresentation6166 = new DicomUID("1.2.840.10008.6.1.480", "CAD Geometry Secondary Graphical Representation (6166)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Document Titles (7000)</summary>
        public static readonly DicomUID DiagnosticImagingReportDocumentTitles7000 = new DicomUID("1.2.840.10008.6.1.481", "Diagnostic Imaging Report Document Titles (7000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Headings (7001)</summary>
        public static readonly DicomUID DiagnosticImagingReportHeadings7001 = new DicomUID("1.2.840.10008.6.1.482", "Diagnostic Imaging Report Headings (7001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Elements (7002)</summary>
        public static readonly DicomUID DiagnosticImagingReportElements7002 = new DicomUID("1.2.840.10008.6.1.483", "Diagnostic Imaging Report Elements (7002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Purposes of Reference (7003)</summary>
        public static readonly DicomUID DiagnosticImagingReportPurposesOfReference7003 = new DicomUID("1.2.840.10008.6.1.484", "Diagnostic Imaging Report Purposes of Reference (7003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Waveform Purposes of Reference (7004)</summary>
        public static readonly DicomUID WaveformPurposesOfReference7004 = new DicomUID("1.2.840.10008.6.1.485", "Waveform Purposes of Reference (7004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contributing Equipment Purposes of Reference (7005)</summary>
        public static readonly DicomUID ContributingEquipmentPurposesOfReference7005 = new DicomUID("1.2.840.10008.6.1.486", "Contributing Equipment Purposes of Reference (7005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: SR Document Purposes of Reference (7006)</summary>
        public static readonly DicomUID SRDocumentPurposesOfReference7006 = new DicomUID("1.2.840.10008.6.1.487", "SR Document Purposes of Reference (7006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Signature Purpose (7007)</summary>
        public static readonly DicomUID SignaturePurpose7007 = new DicomUID("1.2.840.10008.6.1.488", "Signature Purpose (7007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Media Import (7008)</summary>
        public static readonly DicomUID MediaImport7008 = new DicomUID("1.2.840.10008.6.1.489", "Media Import (7008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Key Object Selection Document Title (7010)</summary>
        public static readonly DicomUID KeyObjectSelectionDocumentTitle7010 = new DicomUID("1.2.840.10008.6.1.490", "Key Object Selection Document Title (7010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Rejected for Quality Reasons (7011)</summary>
        public static readonly DicomUID RejectedForQualityReasons7011 = new DicomUID("1.2.840.10008.6.1.491", "Rejected for Quality Reasons (7011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Best in Set (7012)</summary>
        public static readonly DicomUID BestInSet7012 = new DicomUID("1.2.840.10008.6.1.492", "Best in Set (7012)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Document Titles (7020)</summary>
        public static readonly DicomUID DocumentTitles7020 = new DicomUID("1.2.840.10008.6.1.493", "Document Titles (7020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RCS Registration Method Type (7100)</summary>
        public static readonly DicomUID RCSRegistrationMethodType7100 = new DicomUID("1.2.840.10008.6.1.494", "RCS Registration Method Type (7100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Atlas Fiducials (7101)</summary>
        public static readonly DicomUID BrainAtlasFiducials7101 = new DicomUID("1.2.840.10008.6.1.495", "Brain Atlas Fiducials (7101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmentation Property Categories (7150)</summary>
        public static readonly DicomUID SegmentationPropertyCategories7150 = new DicomUID("1.2.840.10008.6.1.496", "Segmentation Property Categories (7150)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmentation Property Types (7151)</summary>
        public static readonly DicomUID SegmentationPropertyTypes7151 = new DicomUID("1.2.840.10008.6.1.497", "Segmentation Property Types (7151)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Structure Segmentation Types (7152)</summary>
        public static readonly DicomUID CardiacStructureSegmentationTypes7152 = new DicomUID("1.2.840.10008.6.1.498", "Cardiac Structure Segmentation Types (7152)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CNS Segmentation Types (7153)</summary>
        public static readonly DicomUID CNSSegmentationTypes7153 = new DicomUID("1.2.840.10008.6.1.499", "CNS Segmentation Types (7153)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominal Segmentation Types (7154)</summary>
        public static readonly DicomUID AbdominalSegmentationTypes7154 = new DicomUID("1.2.840.10008.6.1.500", "Abdominal Segmentation Types (7154)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Thoracic Segmentation Types (7155)</summary>
        public static readonly DicomUID ThoracicSegmentationTypes7155 = new DicomUID("1.2.840.10008.6.1.501", "Thoracic Segmentation Types (7155)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Segmentation Types (7156)</summary>
        public static readonly DicomUID VascularSegmentationTypes7156 = new DicomUID("1.2.840.10008.6.1.502", "Vascular Segmentation Types (7156)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Segmentation Types (7157)</summary>
        public static readonly DicomUID DeviceSegmentationTypes7157 = new DicomUID("1.2.840.10008.6.1.503", "Device Segmentation Types (7157)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Artifact Segmentation Types (7158)</summary>
        public static readonly DicomUID ArtifactSegmentationTypes7158 = new DicomUID("1.2.840.10008.6.1.504", "Artifact Segmentation Types (7158)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Segmentation Types (7159)</summary>
        public static readonly DicomUID LesionSegmentationTypes7159 = new DicomUID("1.2.840.10008.6.1.505", "Lesion Segmentation Types (7159)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pelvic Organ Segmentation Types (7160)</summary>
        public static readonly DicomUID PelvicOrganSegmentationTypes7160 = new DicomUID("1.2.840.10008.6.1.506", "Pelvic Organ Segmentation Types (7160)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physiology Segmentation Types (7161)</summary>
        public static readonly DicomUID PhysiologySegmentationTypes7161 = new DicomUID("1.2.840.10008.6.1.507", "Physiology Segmentation Types (7161)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Referenced Image Purposes of Reference (7201)</summary>
        public static readonly DicomUID ReferencedImagePurposesOfReference7201 = new DicomUID("1.2.840.10008.6.1.508", "Referenced Image Purposes of Reference (7201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source Image Purposes of Reference (7202)</summary>
        public static readonly DicomUID SourceImagePurposesOfReference7202 = new DicomUID("1.2.840.10008.6.1.509", "Source Image Purposes of Reference (7202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Derivation (7203)</summary>
        public static readonly DicomUID ImageDerivation7203 = new DicomUID("1.2.840.10008.6.1.510", "Image Derivation (7203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference to Alternate Representation (7205)</summary>
        public static readonly DicomUID PurposeOfReferenceToAlternateRepresentation7205 = new DicomUID("1.2.840.10008.6.1.511", "Purpose of Reference to Alternate Representation (7205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Related Series Purposes of Reference (7210)</summary>
        public static readonly DicomUID RelatedSeriesPurposesOfReference7210 = new DicomUID("1.2.840.10008.6.1.512", "Related Series Purposes of Reference (7210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-Frame Subset Type (7250)</summary>
        public static readonly DicomUID MultiFrameSubsetType7250 = new DicomUID("1.2.840.10008.6.1.513", "Multi-Frame Subset Type (7250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Person Roles (7450)</summary>
        public static readonly DicomUID PersonRoles7450 = new DicomUID("1.2.840.10008.6.1.514", "Person Roles (7450)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Family Member (7451)</summary>
        public static readonly DicomUID FamilyMember7451 = new DicomUID("1.2.840.10008.6.1.515", "Family Member (7451)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organizational Roles (7452)</summary>
        public static readonly DicomUID OrganizationalRoles7452 = new DicomUID("1.2.840.10008.6.1.516", "Organizational Roles (7452)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Performing Roles (7453)</summary>
        public static readonly DicomUID PerformingRoles7453 = new DicomUID("1.2.840.10008.6.1.517", "Performing Roles (7453)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Taxonomic Rank Values (7454)</summary>
        public static readonly DicomUID AnimalTaxonomicRankValues7454 = new DicomUID("1.2.840.10008.6.1.518", "Animal Taxonomic Rank Values (7454)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sex (7455)</summary>
        public static readonly DicomUID Sex7455 = new DicomUID("1.2.840.10008.6.1.519", "Sex (7455)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Measure for Age (7456)</summary>
        public static readonly DicomUID UnitsOfMeasureForAge7456 = new DicomUID("1.2.840.10008.6.1.520", "Units of Measure for Age (7456)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Linear Measurement (7460)</summary>
        public static readonly DicomUID UnitsOfLinearMeasurement7460 = new DicomUID("1.2.840.10008.6.1.521", "Units of Linear Measurement (7460)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Area Measurement (7461)</summary>
        public static readonly DicomUID UnitsOfAreaMeasurement7461 = new DicomUID("1.2.840.10008.6.1.522", "Units of Area Measurement (7461)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Volume Measurement (7462)</summary>
        public static readonly DicomUID UnitsOfVolumeMeasurement7462 = new DicomUID("1.2.840.10008.6.1.523", "Units of Volume Measurement (7462)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Linear Measurements (7470)</summary>
        public static readonly DicomUID LinearMeasurements7470 = new DicomUID("1.2.840.10008.6.1.524", "Linear Measurements (7470)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Area Measurements (7471)</summary>
        public static readonly DicomUID AreaMeasurements7471 = new DicomUID("1.2.840.10008.6.1.525", "Area Measurements (7471)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume Measurements (7472)</summary>
        public static readonly DicomUID VolumeMeasurements7472 = new DicomUID("1.2.840.10008.6.1.526", "Volume Measurements (7472)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Area Calculation Methods (7473)</summary>
        public static readonly DicomUID GeneralAreaCalculationMethods7473 = new DicomUID("1.2.840.10008.6.1.527", "General Area Calculation Methods (7473)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Volume Calculation Methods (7474)</summary>
        public static readonly DicomUID GeneralVolumeCalculationMethods7474 = new DicomUID("1.2.840.10008.6.1.528", "General Volume Calculation Methods (7474)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breed (7480)</summary>
        public static readonly DicomUID Breed7480 = new DicomUID("1.2.840.10008.6.1.529", "Breed (7480)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breed Registry (7481)</summary>
        public static readonly DicomUID BreedRegistry7481 = new DicomUID("1.2.840.10008.6.1.530", "Breed Registry (7481)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Workitem Definition (9231)</summary>
        public static readonly DicomUID WorkitemDefinition9231 = new DicomUID("1.2.840.10008.6.1.531", "Workitem Definition (9231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-DICOM Output Types (Retired) (9232)</summary>
        public static readonly DicomUID NonDICOMOutputTypes9232RETIRED = new DicomUID("1.2.840.10008.6.1.532", "Non-DICOM Output Types (Retired) (9232)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Procedure Discontinuation Reasons (9300)</summary>
        public static readonly DicomUID ProcedureDiscontinuationReasons9300 = new DicomUID("1.2.840.10008.6.1.533", "Procedure Discontinuation Reasons (9300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Scope of Accumulation (10000)</summary>
        public static readonly DicomUID ScopeOfAccumulation10000 = new DicomUID("1.2.840.10008.6.1.534", "Scope of Accumulation (10000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: UID Types (10001)</summary>
        public static readonly DicomUID UIDTypes10001 = new DicomUID("1.2.840.10008.6.1.535", "UID Types (10001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Irradiation Event Types (10002)</summary>
        public static readonly DicomUID IrradiationEventTypes10002 = new DicomUID("1.2.840.10008.6.1.536", "Irradiation Event Types (10002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equipment Plane Identification (10003)</summary>
        public static readonly DicomUID EquipmentPlaneIdentification10003 = new DicomUID("1.2.840.10008.6.1.537", "Equipment Plane Identification (10003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fluoro Modes (10004)</summary>
        public static readonly DicomUID FluoroModes10004 = new DicomUID("1.2.840.10008.6.1.538", "Fluoro Modes (10004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: X-Ray Filter Materials (10006)</summary>
        public static readonly DicomUID XRayFilterMaterials10006 = new DicomUID("1.2.840.10008.6.1.539", "X-Ray Filter Materials (10006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: X-Ray Filter Types (10007)</summary>
        public static readonly DicomUID XRayFilterTypes10007 = new DicomUID("1.2.840.10008.6.1.540", "X-Ray Filter Types (10007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dose Related Distance Measurements (10008)</summary>
        public static readonly DicomUID DoseRelatedDistanceMeasurements10008 = new DicomUID("1.2.840.10008.6.1.541", "Dose Related Distance Measurements (10008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measured/Calculated (10009)</summary>
        public static readonly DicomUID MeasuredCalculated10009 = new DicomUID("1.2.840.10008.6.1.542", "Measured/Calculated (10009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dose Measurement Devices (10010)</summary>
        public static readonly DicomUID DoseMeasurementDevices10010 = new DicomUID("1.2.840.10008.6.1.543", "Dose Measurement Devices (10010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Effective Dose Evaluation Method (10011)</summary>
        public static readonly DicomUID EffectiveDoseEvaluationMethod10011 = new DicomUID("1.2.840.10008.6.1.544", "Effective Dose Evaluation Method (10011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT Acquisition Type (10013)</summary>
        public static readonly DicomUID CTAcquisitionType10013 = new DicomUID("1.2.840.10008.6.1.545", "CT Acquisition Type (10013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contrast Imaging Technique (10014)</summary>
        public static readonly DicomUID ContrastImagingTechnique10014 = new DicomUID("1.2.840.10008.6.1.546", "Contrast Imaging Technique (10014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT Dose Reference Authorities (10015)</summary>
        public static readonly DicomUID CTDoseReferenceAuthorities10015 = new DicomUID("1.2.840.10008.6.1.547", "CT Dose Reference Authorities (10015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anode Target Material (10016)</summary>
        public static readonly DicomUID AnodeTargetMaterial10016 = new DicomUID("1.2.840.10008.6.1.548", "Anode Target Material (10016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: X-Ray Grid (10017)</summary>
        public static readonly DicomUID XRayGrid10017 = new DicomUID("1.2.840.10008.6.1.549", "X-Ray Grid (10017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Protocol Types (12001)</summary>
        public static readonly DicomUID UltrasoundProtocolTypes12001 = new DicomUID("1.2.840.10008.6.1.550", "Ultrasound Protocol Types (12001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Protocol Stage Types (12002)</summary>
        public static readonly DicomUID UltrasoundProtocolStageTypes12002 = new DicomUID("1.2.840.10008.6.1.551", "Ultrasound Protocol Stage Types (12002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Dates (12003)</summary>
        public static readonly DicomUID OBGYNDates12003 = new DicomUID("1.2.840.10008.6.1.552", "OB-GYN Dates (12003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Biometry Ratios (12004)</summary>
        public static readonly DicomUID FetalBiometryRatios12004 = new DicomUID("1.2.840.10008.6.1.553", "Fetal Biometry Ratios (12004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Biometry Measurements (12005)</summary>
        public static readonly DicomUID FetalBiometryMeasurements12005 = new DicomUID("1.2.840.10008.6.1.554", "Fetal Biometry Measurements (12005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Long Bones Biometry Measurements (12006)</summary>
        public static readonly DicomUID FetalLongBonesBiometryMeasurements12006 = new DicomUID("1.2.840.10008.6.1.555", "Fetal Long Bones Biometry Measurements (12006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Cranium (12007)</summary>
        public static readonly DicomUID FetalCranium12007 = new DicomUID("1.2.840.10008.6.1.556", "Fetal Cranium (12007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Amniotic Sac (12008)</summary>
        public static readonly DicomUID OBGYNAmnioticSac12008 = new DicomUID("1.2.840.10008.6.1.557", "OB-GYN Amniotic Sac (12008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Early Gestation Biometry Measurements (12009)</summary>
        public static readonly DicomUID EarlyGestationBiometryMeasurements12009 = new DicomUID("1.2.840.10008.6.1.558", "Early Gestation Biometry Measurements (12009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Pelvis and Uterus (12011)</summary>
        public static readonly DicomUID UltrasoundPelvisAndUterus12011 = new DicomUID("1.2.840.10008.6.1.559", "Ultrasound Pelvis and Uterus (12011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB Equations and Tables (12012)</summary>
        public static readonly DicomUID OBEquationsAndTables12012 = new DicomUID("1.2.840.10008.6.1.560", "OB Equations and Tables (12012)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gestational Age Equations and Tables (12013)</summary>
        public static readonly DicomUID GestationalAgeEquationsAndTables12013 = new DicomUID("1.2.840.10008.6.1.561", "Gestational Age Equations and Tables (12013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB Fetal Body Weight Equations and Tables (12014)</summary>
        public static readonly DicomUID OBFetalBodyWeightEquationsAndTables12014 = new DicomUID("1.2.840.10008.6.1.562", "OB Fetal Body Weight Equations and Tables (12014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Growth Equations and Tables (12015)</summary>
        public static readonly DicomUID FetalGrowthEquationsAndTables12015 = new DicomUID("1.2.840.10008.6.1.563", "Fetal Growth Equations and Tables (12015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Estimated Fetal Weight Percentile Equations and Tables (12016)</summary>
        public static readonly DicomUID EstimatedFetalWeightPercentileEquationsAndTables12016 = new DicomUID("1.2.840.10008.6.1.564", "Estimated Fetal Weight Percentile Equations and Tables (12016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Growth Distribution Rank (12017)</summary>
        public static readonly DicomUID GrowthDistributionRank12017 = new DicomUID("1.2.840.10008.6.1.565", "Growth Distribution Rank (12017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Summary (12018)</summary>
        public static readonly DicomUID OBGYNSummary12018 = new DicomUID("1.2.840.10008.6.1.566", "OB-GYN Summary (12018)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Fetus Summary (12019)</summary>
        public static readonly DicomUID OBGYNFetusSummary12019 = new DicomUID("1.2.840.10008.6.1.567", "OB-GYN Fetus Summary (12019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Summary (12101)</summary>
        public static readonly DicomUID VascularSummary12101 = new DicomUID("1.2.840.10008.6.1.568", "Vascular Summary (12101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Temporal Periods Relating to Procedure or Therapy (12102)</summary>
        public static readonly DicomUID TemporalPeriodsRelatingToProcedureOrTherapy12102 = new DicomUID("1.2.840.10008.6.1.569", "Temporal Periods Relating to Procedure or Therapy (12102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Ultrasound Anatomic Location (12103)</summary>
        public static readonly DicomUID VascularUltrasoundAnatomicLocation12103 = new DicomUID("1.2.840.10008.6.1.570", "Vascular Ultrasound Anatomic Location (12103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Extracranial Arteries (12104)</summary>
        public static readonly DicomUID ExtracranialArteries12104 = new DicomUID("1.2.840.10008.6.1.571", "Extracranial Arteries (12104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intracranial Cerebral Vessels (12105)</summary>
        public static readonly DicomUID IntracranialCerebralVessels12105 = new DicomUID("1.2.840.10008.6.1.572", "Intracranial Cerebral Vessels (12105)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intracranial Cerebral Vessels (Unilateral) (12106)</summary>
        public static readonly DicomUID IntracranialCerebralVesselsUnilateral12106 = new DicomUID("1.2.840.10008.6.1.573", "Intracranial Cerebral Vessels (Unilateral) (12106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Upper Extremity Arteries (12107)</summary>
        public static readonly DicomUID UpperExtremityArteries12107 = new DicomUID("1.2.840.10008.6.1.574", "Upper Extremity Arteries (12107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Upper Extremity Veins (12108)</summary>
        public static readonly DicomUID UpperExtremityVeins12108 = new DicomUID("1.2.840.10008.6.1.575", "Upper Extremity Veins (12108)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lower Extremity Arteries (12109)</summary>
        public static readonly DicomUID LowerExtremityArteries12109 = new DicomUID("1.2.840.10008.6.1.576", "Lower Extremity Arteries (12109)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lower Extremity Veins (12110)</summary>
        public static readonly DicomUID LowerExtremityVeins12110 = new DicomUID("1.2.840.10008.6.1.577", "Lower Extremity Veins (12110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominal Arteries (Lateral) (12111)</summary>
        public static readonly DicomUID AbdominalArteriesLateral12111 = new DicomUID("1.2.840.10008.6.1.578", "Abdominal Arteries (Lateral) (12111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominal Arteries (Unilateral) (12112)</summary>
        public static readonly DicomUID AbdominalArteriesUnilateral12112 = new DicomUID("1.2.840.10008.6.1.579", "Abdominal Arteries (Unilateral) (12112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominal Veins (Lateral) (12113)</summary>
        public static readonly DicomUID AbdominalVeinsLateral12113 = new DicomUID("1.2.840.10008.6.1.580", "Abdominal Veins (Lateral) (12113)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominal Veins (Unilateral) (12114)</summary>
        public static readonly DicomUID AbdominalVeinsUnilateral12114 = new DicomUID("1.2.840.10008.6.1.581", "Abdominal Veins (Unilateral) (12114)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Renal Vessels (12115)</summary>
        public static readonly DicomUID RenalVessels12115 = new DicomUID("1.2.840.10008.6.1.582", "Renal Vessels (12115)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Segment Modifiers (12116)</summary>
        public static readonly DicomUID VesselSegmentModifiers12116 = new DicomUID("1.2.840.10008.6.1.583", "Vessel Segment Modifiers (12116)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Branch Modifiers (12117)</summary>
        public static readonly DicomUID VesselBranchModifiers12117 = new DicomUID("1.2.840.10008.6.1.584", "Vessel Branch Modifiers (12117)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Ultrasound Property (12119)</summary>
        public static readonly DicomUID VascularUltrasoundProperty12119 = new DicomUID("1.2.840.10008.6.1.585", "Vascular Ultrasound Property (12119)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Velocity Measurements by Ultrasound (12120)</summary>
        public static readonly DicomUID BloodVelocityMeasurementsByUltrasound12120 = new DicomUID("1.2.840.10008.6.1.586", "Blood Velocity Measurements by Ultrasound (12120)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Indices and Ratios (12121)</summary>
        public static readonly DicomUID VascularIndicesAndRatios12121 = new DicomUID("1.2.840.10008.6.1.587", "Vascular Indices and Ratios (12121)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Other Vascular Properties (12122)</summary>
        public static readonly DicomUID OtherVascularProperties12122 = new DicomUID("1.2.840.10008.6.1.588", "Other Vascular Properties (12122)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Carotid Ratios (12123)</summary>
        public static readonly DicomUID CarotidRatios12123 = new DicomUID("1.2.840.10008.6.1.589", "Carotid Ratios (12123)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Renal Ratios (12124)</summary>
        public static readonly DicomUID RenalRatios12124 = new DicomUID("1.2.840.10008.6.1.590", "Renal Ratios (12124)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pelvic Vasculature Anatomical Location (12140)</summary>
        public static readonly DicomUID PelvicVasculatureAnatomicalLocation12140 = new DicomUID("1.2.840.10008.6.1.591", "Pelvic Vasculature Anatomical Location (12140)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Vasculature Anatomical Location (12141)</summary>
        public static readonly DicomUID FetalVasculatureAnatomicalLocation12141 = new DicomUID("1.2.840.10008.6.1.592", "Fetal Vasculature Anatomical Location (12141)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Left Ventricle (12200)</summary>
        public static readonly DicomUID EchocardiographyLeftVentricle12200 = new DicomUID("1.2.840.10008.6.1.593", "Echocardiography Left Ventricle (12200)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Linear (12201)</summary>
        public static readonly DicomUID LeftVentricleLinear12201 = new DicomUID("1.2.840.10008.6.1.594", "Left Ventricle Linear (12201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Volume (12202)</summary>
        public static readonly DicomUID LeftVentricleVolume12202 = new DicomUID("1.2.840.10008.6.1.595", "Left Ventricle Volume (12202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Other (12203)</summary>
        public static readonly DicomUID LeftVentricleOther12203 = new DicomUID("1.2.840.10008.6.1.596", "Left Ventricle Other (12203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Right Ventricle (12204)</summary>
        public static readonly DicomUID EchocardiographyRightVentricle12204 = new DicomUID("1.2.840.10008.6.1.597", "Echocardiography Right Ventricle (12204)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Left Atrium (12205)</summary>
        public static readonly DicomUID EchocardiographyLeftAtrium12205 = new DicomUID("1.2.840.10008.6.1.598", "Echocardiography Left Atrium (12205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Right Atrium (12206)</summary>
        public static readonly DicomUID EchocardiographyRightAtrium12206 = new DicomUID("1.2.840.10008.6.1.599", "Echocardiography Right Atrium (12206)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Mitral Valve (12207)</summary>
        public static readonly DicomUID EchocardiographyMitralValve12207 = new DicomUID("1.2.840.10008.6.1.600", "Echocardiography Mitral Valve (12207)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Tricuspid Valve (12208)</summary>
        public static readonly DicomUID EchocardiographyTricuspidValve12208 = new DicomUID("1.2.840.10008.6.1.601", "Echocardiography Tricuspid Valve (12208)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Pulmonic Valve (12209)</summary>
        public static readonly DicomUID EchocardiographyPulmonicValve12209 = new DicomUID("1.2.840.10008.6.1.602", "Echocardiography Pulmonic Valve (12209)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Pulmonary Artery (12210)</summary>
        public static readonly DicomUID EchocardiographyPulmonaryArtery12210 = new DicomUID("1.2.840.10008.6.1.603", "Echocardiography Pulmonary Artery (12210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Aortic Valve (12211)</summary>
        public static readonly DicomUID EchocardiographyAorticValve12211 = new DicomUID("1.2.840.10008.6.1.604", "Echocardiography Aortic Valve (12211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Aorta (12212)</summary>
        public static readonly DicomUID EchocardiographyAorta12212 = new DicomUID("1.2.840.10008.6.1.605", "Echocardiography Aorta (12212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Pulmonary Veins (12214)</summary>
        public static readonly DicomUID EchocardiographyPulmonaryVeins12214 = new DicomUID("1.2.840.10008.6.1.606", "Echocardiography Pulmonary Veins (12214)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Vena Cavae (12215)</summary>
        public static readonly DicomUID EchocardiographyVenaCavae12215 = new DicomUID("1.2.840.10008.6.1.607", "Echocardiography Vena Cavae (12215)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Hepatic Veins (12216)</summary>
        public static readonly DicomUID EchocardiographyHepaticVeins12216 = new DicomUID("1.2.840.10008.6.1.608", "Echocardiography Hepatic Veins (12216)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Cardiac Shunt (12217)</summary>
        public static readonly DicomUID EchocardiographyCardiacShunt12217 = new DicomUID("1.2.840.10008.6.1.609", "Echocardiography Cardiac Shunt (12217)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Congenital (12218)</summary>
        public static readonly DicomUID EchocardiographyCongenital12218 = new DicomUID("1.2.840.10008.6.1.610", "Echocardiography Congenital (12218)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pulmonary Vein Modifiers (12219)</summary>
        public static readonly DicomUID PulmonaryVeinModifiers12219 = new DicomUID("1.2.840.10008.6.1.611", "Pulmonary Vein Modifiers (12219)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Common Measurements (12220)</summary>
        public static readonly DicomUID EchocardiographyCommonMeasurements12220 = new DicomUID("1.2.840.10008.6.1.612", "Echocardiography Common Measurements (12220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Flow Direction (12221)</summary>
        public static readonly DicomUID FlowDirection12221 = new DicomUID("1.2.840.10008.6.1.613", "Flow Direction (12221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Orifice Flow Properties (12222)</summary>
        public static readonly DicomUID OrificeFlowProperties12222 = new DicomUID("1.2.840.10008.6.1.614", "Orifice Flow Properties (12222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Stroke Volume Origin (12223)</summary>
        public static readonly DicomUID EchocardiographyStrokeVolumeOrigin12223 = new DicomUID("1.2.840.10008.6.1.615", "Echocardiography Stroke Volume Origin (12223)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Image Modes (12224)</summary>
        public static readonly DicomUID UltrasoundImageModes12224 = new DicomUID("1.2.840.10008.6.1.616", "Ultrasound Image Modes (12224)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Image View (12226)</summary>
        public static readonly DicomUID EchocardiographyImageView12226 = new DicomUID("1.2.840.10008.6.1.617", "Echocardiography Image View (12226)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Measurement Method (12227)</summary>
        public static readonly DicomUID EchocardiographyMeasurementMethod12227 = new DicomUID("1.2.840.10008.6.1.618", "Echocardiography Measurement Method (12227)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Volume Methods (12228)</summary>
        public static readonly DicomUID EchocardiographyVolumeMethods12228 = new DicomUID("1.2.840.10008.6.1.619", "Echocardiography Volume Methods (12228)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Area Methods (12229)</summary>
        public static readonly DicomUID EchocardiographyAreaMethods12229 = new DicomUID("1.2.840.10008.6.1.620", "Echocardiography Area Methods (12229)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gradient Methods (12230)</summary>
        public static readonly DicomUID GradientMethods12230 = new DicomUID("1.2.840.10008.6.1.621", "Gradient Methods (12230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume Flow Methods (12231)</summary>
        public static readonly DicomUID VolumeFlowMethods12231 = new DicomUID("1.2.840.10008.6.1.622", "Volume Flow Methods (12231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardium Mass Methods (12232)</summary>
        public static readonly DicomUID MyocardiumMassMethods12232 = new DicomUID("1.2.840.10008.6.1.623", "Myocardium Mass Methods (12232)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Phase (12233)</summary>
        public static readonly DicomUID CardiacPhase12233 = new DicomUID("1.2.840.10008.6.1.624", "Cardiac Phase (12233)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiration State (12234)</summary>
        public static readonly DicomUID RespirationState12234 = new DicomUID("1.2.840.10008.6.1.625", "Respiration State (12234)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mitral Valve Anatomic Sites (12235)</summary>
        public static readonly DicomUID MitralValveAnatomicSites12235 = new DicomUID("1.2.840.10008.6.1.626", "Mitral Valve Anatomic Sites (12235)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Anatomic Sites (12236)</summary>
        public static readonly DicomUID EchoAnatomicSites12236 = new DicomUID("1.2.840.10008.6.1.627", "Echo Anatomic Sites (12236)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Anatomic Site Modifiers (12237)</summary>
        public static readonly DicomUID EchocardiographyAnatomicSiteModifiers12237 = new DicomUID("1.2.840.10008.6.1.628", "Echocardiography Anatomic Site Modifiers (12237)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wall Motion Scoring Schemes (12238)</summary>
        public static readonly DicomUID WallMotionScoringSchemes12238 = new DicomUID("1.2.840.10008.6.1.629", "Wall Motion Scoring Schemes (12238)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Output Properties (12239)</summary>
        public static readonly DicomUID CardiacOutputProperties12239 = new DicomUID("1.2.840.10008.6.1.630", "Cardiac Output Properties (12239)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Area (12240)</summary>
        public static readonly DicomUID LeftVentricleArea12240 = new DicomUID("1.2.840.10008.6.1.631", "Left Ventricle Area (12240)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tricuspid Valve Finding Sites (12241)</summary>
        public static readonly DicomUID TricuspidValveFindingSites12241 = new DicomUID("1.2.840.10008.6.1.632", "Tricuspid Valve Finding Sites (12241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Aortic Valve Finding Sites (12242)</summary>
        public static readonly DicomUID AorticValveFindingSites12242 = new DicomUID("1.2.840.10008.6.1.633", "Aortic Valve Finding Sites (12242)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Finding Sites (12243)</summary>
        public static readonly DicomUID LeftVentricleFindingSites12243 = new DicomUID("1.2.840.10008.6.1.634", "Left Ventricle Finding Sites (12243)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Congenital Finding Sites (12244)</summary>
        public static readonly DicomUID CongenitalFindingSites12244 = new DicomUID("1.2.840.10008.6.1.635", "Congenital Finding Sites (12244)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Processing Algorithm Families (7162)</summary>
        public static readonly DicomUID SurfaceProcessingAlgorithmFamilies7162 = new DicomUID("1.2.840.10008.6.1.636", "Surface Processing Algorithm Families (7162)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Test Procedure Phases (3207)</summary>
        public static readonly DicomUID StressTestProcedurePhases3207 = new DicomUID("1.2.840.10008.6.1.637", "Stress Test Procedure Phases (3207)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stages (3778)</summary>
        public static readonly DicomUID Stages3778 = new DicomUID("1.2.840.10008.6.1.638", "Stages (3778)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: S-M-L Size Descriptor (252)</summary>
        public static readonly DicomUID SMLSizeDescriptor252 = new DicomUID("1.2.840.10008.6.1.735", "S-M-L Size Descriptor (252)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Major Coronary Arteries (3016)</summary>
        public static readonly DicomUID MajorCoronaryArteries3016 = new DicomUID("1.2.840.10008.6.1.736", "Major Coronary Arteries (3016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Radioactivity (3083)</summary>
        public static readonly DicomUID UnitsOfRadioactivity3083 = new DicomUID("1.2.840.10008.6.1.737", "Units of Radioactivity (3083)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Rest-Stress (3102)</summary>
        public static readonly DicomUID RestStress3102 = new DicomUID("1.2.840.10008.6.1.738", "Rest-Stress (3102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Cardiology Protocols (3106)</summary>
        public static readonly DicomUID PETCardiologyProtocols3106 = new DicomUID("1.2.840.10008.6.1.739", "PET Cardiology Protocols (3106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Cardiology Radiopharmaceuticals (3107)</summary>
        public static readonly DicomUID PETCardiologyRadiopharmaceuticals3107 = new DicomUID("1.2.840.10008.6.1.740", "PET Cardiology Radiopharmaceuticals (3107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: NM/PET Procedures (3108)</summary>
        public static readonly DicomUID NMPETProcedures3108 = new DicomUID("1.2.840.10008.6.1.741", "NM/PET Procedures (3108)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nuclear Cardiology Protocols (3110)</summary>
        public static readonly DicomUID NuclearCardiologyProtocols3110 = new DicomUID("1.2.840.10008.6.1.742", "Nuclear Cardiology Protocols (3110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nuclear Cardiology Radiopharmaceuticals (3111)</summary>
        public static readonly DicomUID NuclearCardiologyRadiopharmaceuticals3111 = new DicomUID("1.2.840.10008.6.1.743", "Nuclear Cardiology Radiopharmaceuticals (3111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuation Correction (3112)</summary>
        public static readonly DicomUID AttenuationCorrection3112 = new DicomUID("1.2.840.10008.6.1.744", "Attenuation Correction (3112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Types of Perfusion Defects (3113)</summary>
        public static readonly DicomUID TypesOfPerfusionDefects3113 = new DicomUID("1.2.840.10008.6.1.745", "Types of Perfusion Defects (3113)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Study Quality (3114)</summary>
        public static readonly DicomUID StudyQuality3114 = new DicomUID("1.2.840.10008.6.1.746", "Study Quality (3114)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Imaging Quality Issues (3115)</summary>
        public static readonly DicomUID StressImagingQualityIssues3115 = new DicomUID("1.2.840.10008.6.1.747", "Stress Imaging Quality Issues (3115)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: NM Extracardiac Findings (3116)</summary>
        public static readonly DicomUID NMExtracardiacFindings3116 = new DicomUID("1.2.840.10008.6.1.748", "NM Extracardiac Findings (3116)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuation Correction Methods (3117)</summary>
        public static readonly DicomUID AttenuationCorrectionMethods3117 = new DicomUID("1.2.840.10008.6.1.749", "Attenuation Correction Methods (3117)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Level of Risk (3118)</summary>
        public static readonly DicomUID LevelOfRisk3118 = new DicomUID("1.2.840.10008.6.1.750", "Level of Risk (3118)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: LV Function (3119)</summary>
        public static readonly DicomUID LVFunction3119 = new DicomUID("1.2.840.10008.6.1.751", "LV Function (3119)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Findings (3120)</summary>
        public static readonly DicomUID PerfusionFindings3120 = new DicomUID("1.2.840.10008.6.1.752", "Perfusion Findings (3120)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Morphology (3121)</summary>
        public static readonly DicomUID PerfusionMorphology3121 = new DicomUID("1.2.840.10008.6.1.753", "Perfusion Morphology (3121)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventricular Enlargement (3122)</summary>
        public static readonly DicomUID VentricularEnlargement3122 = new DicomUID("1.2.840.10008.6.1.754", "Ventricular Enlargement (3122)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Test Procedure (3200)</summary>
        public static readonly DicomUID StressTestProcedure3200 = new DicomUID("1.2.840.10008.6.1.755", "Stress Test Procedure (3200)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Indications for Stress Test (3201)</summary>
        public static readonly DicomUID IndicationsForStressTest3201 = new DicomUID("1.2.840.10008.6.1.756", "Indications for Stress Test (3201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Pain (3202)</summary>
        public static readonly DicomUID ChestPain3202 = new DicomUID("1.2.840.10008.6.1.757", "Chest Pain (3202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exerciser Device (3203)</summary>
        public static readonly DicomUID ExerciserDevice3203 = new DicomUID("1.2.840.10008.6.1.758", "Exerciser Device (3203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Agents (3204)</summary>
        public static readonly DicomUID StressAgents3204 = new DicomUID("1.2.840.10008.6.1.759", "Stress Agents (3204)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Indications for Pharmacological Stress Test (3205)</summary>
        public static readonly DicomUID IndicationsForPharmacologicalStressTest3205 = new DicomUID("1.2.840.10008.6.1.760", "Indications for Pharmacological Stress Test (3205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-invasive Cardiac Imaging Procedures (3206)</summary>
        public static readonly DicomUID NonInvasiveCardiacImagingProcedures3206 = new DicomUID("1.2.840.10008.6.1.761", "Non-invasive Cardiac Imaging Procedures (3206)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Summary Codes Exercise ECG (3208)</summary>
        public static readonly DicomUID SummaryCodesExerciseECG3208 = new DicomUID("1.2.840.10008.6.1.763", "Summary Codes Exercise ECG (3208)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Summary Codes Stress Imaging (3209)</summary>
        public static readonly DicomUID SummaryCodesStressImaging3209 = new DicomUID("1.2.840.10008.6.1.764", "Summary Codes Stress Imaging (3209)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Speed of Response (3210)</summary>
        public static readonly DicomUID SpeedOfResponse3210 = new DicomUID("1.2.840.10008.6.1.765", "Speed of Response (3210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: BP Response (3211)</summary>
        public static readonly DicomUID BPResponse3211 = new DicomUID("1.2.840.10008.6.1.766", "BP Response (3211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treadmill Speed (3212)</summary>
        public static readonly DicomUID TreadmillSpeed3212 = new DicomUID("1.2.840.10008.6.1.767", "Treadmill Speed (3212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Hemodynamic Findings (3213)</summary>
        public static readonly DicomUID StressHemodynamicFindings3213 = new DicomUID("1.2.840.10008.6.1.768", "Stress Hemodynamic Findings (3213)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Finding Method (3215)</summary>
        public static readonly DicomUID PerfusionFindingMethod3215 = new DicomUID("1.2.840.10008.6.1.769", "Perfusion Finding Method (3215)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Comparison Finding (3217)</summary>
        public static readonly DicomUID ComparisonFinding3217 = new DicomUID("1.2.840.10008.6.1.770", "Comparison Finding (3217)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Symptoms (3220)</summary>
        public static readonly DicomUID StressSymptoms3220 = new DicomUID("1.2.840.10008.6.1.771", "Stress Symptoms (3220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Test Termination Reasons (3221)</summary>
        public static readonly DicomUID StressTestTerminationReasons3221 = new DicomUID("1.2.840.10008.6.1.772", "Stress Test Termination Reasons (3221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: QTc Measurements (3227)</summary>
        public static readonly DicomUID QTcMeasurements3227 = new DicomUID("1.2.840.10008.6.1.773", "QTc Measurements (3227)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Timing Measurements (3228)</summary>
        public static readonly DicomUID ECGTimingMeasurements3228 = new DicomUID("1.2.840.10008.6.1.774", "ECG Timing Measurements (3228)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Axis Measurements (3229)</summary>
        public static readonly DicomUID ECGAxisMeasurements3229 = new DicomUID("1.2.840.10008.6.1.775", "ECG Axis Measurements (3229)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Findings (3230)</summary>
        public static readonly DicomUID ECGFindings3230 = new DicomUID("1.2.840.10008.6.1.776", "ECG Findings (3230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ST Segment Findings (3231)</summary>
        public static readonly DicomUID STSegmentFindings3231 = new DicomUID("1.2.840.10008.6.1.777", "ST Segment Findings (3231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ST Segment Location (3232)</summary>
        public static readonly DicomUID STSegmentLocation3232 = new DicomUID("1.2.840.10008.6.1.778", "ST Segment Location (3232)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ST Segment Morphology (3233)</summary>
        public static readonly DicomUID STSegmentMorphology3233 = new DicomUID("1.2.840.10008.6.1.779", "ST Segment Morphology (3233)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ectopic Beat Morphology (3234)</summary>
        public static readonly DicomUID EctopicBeatMorphology3234 = new DicomUID("1.2.840.10008.6.1.780", "Ectopic Beat Morphology (3234)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Comparison Findings (3235)</summary>
        public static readonly DicomUID PerfusionComparisonFindings3235 = new DicomUID("1.2.840.10008.6.1.781", "Perfusion Comparison Findings (3235)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tolerance Comparison Findings (3236)</summary>
        public static readonly DicomUID ToleranceComparisonFindings3236 = new DicomUID("1.2.840.10008.6.1.782", "Tolerance Comparison Findings (3236)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wall Motion Comparison Findings (3237)</summary>
        public static readonly DicomUID WallMotionComparisonFindings3237 = new DicomUID("1.2.840.10008.6.1.783", "Wall Motion Comparison Findings (3237)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Scoring Scales (3238)</summary>
        public static readonly DicomUID StressScoringScales3238 = new DicomUID("1.2.840.10008.6.1.784", "Stress Scoring Scales (3238)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perceived Exertion Scales (3239)</summary>
        public static readonly DicomUID PerceivedExertionScales3239 = new DicomUID("1.2.840.10008.6.1.785", "Perceived Exertion Scales (3239)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventricle Identification (3463)</summary>
        public static readonly DicomUID VentricleIdentification3463 = new DicomUID("1.2.840.10008.6.1.786", "Ventricle Identification (3463)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon Overall Assessment (6200)</summary>
        public static readonly DicomUID ColonOverallAssessment6200 = new DicomUID("1.2.840.10008.6.1.787", "Colon Overall Assessment (6200)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon Finding or Feature (6201)</summary>
        public static readonly DicomUID ColonFindingOrFeature6201 = new DicomUID("1.2.840.10008.6.1.788", "Colon Finding or Feature (6201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon Finding or Feature Modifier (6202)</summary>
        public static readonly DicomUID ColonFindingOrFeatureModifier6202 = new DicomUID("1.2.840.10008.6.1.789", "Colon Finding or Feature Modifier (6202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon Non-lesion Object Type (6203)</summary>
        public static readonly DicomUID ColonNonLesionObjectType6203 = new DicomUID("1.2.840.10008.6.1.790", "Colon Non-lesion Object Type (6203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Non-colon Findings (6204)</summary>
        public static readonly DicomUID AnatomicNonColonFindings6204 = new DicomUID("1.2.840.10008.6.1.791", "Anatomic Non-colon Findings (6204)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clockface Location for Colon (6205)</summary>
        public static readonly DicomUID ClockfaceLocationForColon6205 = new DicomUID("1.2.840.10008.6.1.792", "Clockface Location for Colon (6205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Recumbent Patient Orientation for Colon (6206)</summary>
        public static readonly DicomUID RecumbentPatientOrientationForColon6206 = new DicomUID("1.2.840.10008.6.1.793", "Recumbent Patient Orientation for Colon (6206)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon Quantitative Temporal Difference Type (6207)</summary>
        public static readonly DicomUID ColonQuantitativeTemporalDifferenceType6207 = new DicomUID("1.2.840.10008.6.1.794", "Colon Quantitative Temporal Difference Type (6207)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon Types of Quality Control Standard (6208)</summary>
        public static readonly DicomUID ColonTypesOfQualityControlStandard6208 = new DicomUID("1.2.840.10008.6.1.795", "Colon Types of Quality Control Standard (6208)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon Morphology Descriptor (6209)</summary>
        public static readonly DicomUID ColonMorphologyDescriptor6209 = new DicomUID("1.2.840.10008.6.1.796", "Colon Morphology Descriptor (6209)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Location in Intestinal Tract (6210)</summary>
        public static readonly DicomUID LocationInIntestinalTract6210 = new DicomUID("1.2.840.10008.6.1.797", "Location in Intestinal Tract (6210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Colon CAD Material Description (6211)</summary>
        public static readonly DicomUID ColonCADMaterialDescription6211 = new DicomUID("1.2.840.10008.6.1.798", "Colon CAD Material Description (6211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calculated Value for Colon Findings (6212)</summary>
        public static readonly DicomUID CalculatedValueForColonFindings6212 = new DicomUID("1.2.840.10008.6.1.799", "Calculated Value for Colon Findings (6212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Horizontal Directions (4214)</summary>
        public static readonly DicomUID OphthalmicHorizontalDirections4214 = new DicomUID("1.2.840.10008.6.1.800", "Ophthalmic Horizontal Directions (4214)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Vertical Directions (4215)</summary>
        public static readonly DicomUID OphthalmicVerticalDirections4215 = new DicomUID("1.2.840.10008.6.1.801", "Ophthalmic Vertical Directions (4215)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Visual Acuity Type (4216)</summary>
        public static readonly DicomUID OphthalmicVisualAcuityType4216 = new DicomUID("1.2.840.10008.6.1.802", "Ophthalmic Visual Acuity Type (4216)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Pulse Waveform (3004)</summary>
        public static readonly DicomUID ArterialPulseWaveform3004 = new DicomUID("1.2.840.10008.6.1.803", "Arterial Pulse Waveform (3004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiration Waveform (3005)</summary>
        public static readonly DicomUID RespirationWaveform3005 = new DicomUID("1.2.840.10008.6.1.804", "Respiration Waveform (3005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Contrast/Bolus Agents (12030)</summary>
        public static readonly DicomUID UltrasoundContrastBolusAgents12030 = new DicomUID("1.2.840.10008.6.1.805", "Ultrasound Contrast/Bolus Agents (12030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Protocol Interval Events (12031)</summary>
        public static readonly DicomUID ProtocolIntervalEvents12031 = new DicomUID("1.2.840.10008.6.1.806", "Protocol Interval Events (12031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Transducer Scan Pattern (12032)</summary>
        public static readonly DicomUID TransducerScanPattern12032 = new DicomUID("1.2.840.10008.6.1.807", "Transducer Scan Pattern (12032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Transducer Geometry (12033)</summary>
        public static readonly DicomUID UltrasoundTransducerGeometry12033 = new DicomUID("1.2.840.10008.6.1.808", "Ultrasound Transducer Geometry (12033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Transducer Beam Steering (12034)</summary>
        public static readonly DicomUID UltrasoundTransducerBeamSteering12034 = new DicomUID("1.2.840.10008.6.1.809", "Ultrasound Transducer Beam Steering (12034)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Transducer Application (12035)</summary>
        public static readonly DicomUID UltrasoundTransducerApplication12035 = new DicomUID("1.2.840.10008.6.1.810", "Ultrasound Transducer Application (12035)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Instance Availability Status (50)</summary>
        public static readonly DicomUID InstanceAvailabilityStatus50 = new DicomUID("1.2.840.10008.6.1.811", "Instance Availability Status (50)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Modality PPS Discontinuation Reasons (9301)</summary>
        public static readonly DicomUID ModalityPPSDiscontinuationReasons9301 = new DicomUID("1.2.840.10008.6.1.812", "Modality PPS Discontinuation Reasons (9301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Media Import PPS Discontinuation Reasons (9302)</summary>
        public static readonly DicomUID MediaImportPPSDiscontinuationReasons9302 = new DicomUID("1.2.840.10008.6.1.813", "Media Import PPS Discontinuation Reasons (9302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DX Anatomy Imaged for Animals (7482)</summary>
        public static readonly DicomUID DXAnatomyImagedForAnimals7482 = new DicomUID("1.2.840.10008.6.1.814", "DX Anatomy Imaged for Animals (7482)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Common Anatomic Regions for Animals (7483)</summary>
        public static readonly DicomUID CommonAnatomicRegionsForAnimals7483 = new DicomUID("1.2.840.10008.6.1.815", "Common Anatomic Regions for Animals (7483)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DX View for Animals (7484)</summary>
        public static readonly DicomUID DXViewForAnimals7484 = new DicomUID("1.2.840.10008.6.1.816", "DX View for Animals (7484)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Institutional Departments, Units and Services (7030)</summary>
        public static readonly DicomUID InstitutionalDepartmentsUnitsAndServices7030 = new DicomUID("1.2.840.10008.6.1.817", "Institutional Departments, Units and Services (7030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference to Predecessor Report (7009)</summary>
        public static readonly DicomUID PurposeOfReferenceToPredecessorReport7009 = new DicomUID("1.2.840.10008.6.1.818", "Purpose of Reference to Predecessor Report (7009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Fixation Quality During Acquisition (4220)</summary>
        public static readonly DicomUID VisualFixationQualityDuringAcquisition4220 = new DicomUID("1.2.840.10008.6.1.819", "Visual Fixation Quality During Acquisition (4220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Fixation Quality Problem (4221)</summary>
        public static readonly DicomUID VisualFixationQualityProblem4221 = new DicomUID("1.2.840.10008.6.1.820", "Visual Fixation Quality Problem (4221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Macular Grid Problem (4222)</summary>
        public static readonly DicomUID OphthalmicMacularGridProblem4222 = new DicomUID("1.2.840.10008.6.1.821", "Ophthalmic Macular Grid Problem (4222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organizations (5002)</summary>
        public static readonly DicomUID Organizations5002 = new DicomUID("1.2.840.10008.6.1.822", "Organizations (5002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mixed Breeds (7486)</summary>
        public static readonly DicomUID MixedBreeds7486 = new DicomUID("1.2.840.10008.6.1.823", "Mixed Breeds (7486)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Broselow-Luten Pediatric Size Categories (7040)</summary>
        public static readonly DicomUID BroselowLutenPediatricSizeCategories7040 = new DicomUID("1.2.840.10008.6.1.824", "Broselow-Luten Pediatric Size Categories (7040)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CMDCTECC Calcium Scoring Patient Size Categories (7042)</summary>
        public static readonly DicomUID CMDCTECCCalciumScoringPatientSizeCategories7042 = new DicomUID("1.2.840.10008.6.1.825", "CMDCTECC Calcium Scoring Patient Size Categories (7042)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Report Titles (12245)</summary>
        public static readonly DicomUID CardiacUltrasoundReportTitles12245 = new DicomUID("1.2.840.10008.6.1.826", "Cardiac Ultrasound Report Titles (12245)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Indication for Study (12246)</summary>
        public static readonly DicomUID CardiacUltrasoundIndicationForStudy12246 = new DicomUID("1.2.840.10008.6.1.827", "Cardiac Ultrasound Indication for Study (12246)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pediatric, Fetal and Congenital Cardiac Surgical Interventions (12247)</summary>
        public static readonly DicomUID PediatricFetalAndCongenitalCardiacSurgicalInterventions12247 = new DicomUID("1.2.840.10008.6.1.828", "Pediatric, Fetal and Congenital Cardiac Surgical Interventions (12247)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Summary Codes (12248)</summary>
        public static readonly DicomUID CardiacUltrasoundSummaryCodes12248 = new DicomUID("1.2.840.10008.6.1.829", "Cardiac Ultrasound Summary Codes (12248)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Fetal Summary Codes (12249)</summary>
        public static readonly DicomUID CardiacUltrasoundFetalSummaryCodes12249 = new DicomUID("1.2.840.10008.6.1.830", "Cardiac Ultrasound Fetal Summary Codes (12249)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Common Linear Measurements (12250)</summary>
        public static readonly DicomUID CardiacUltrasoundCommonLinearMeasurements12250 = new DicomUID("1.2.840.10008.6.1.831", "Cardiac Ultrasound Common Linear Measurements (12250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Linear Valve Measurements (12251)</summary>
        public static readonly DicomUID CardiacUltrasoundLinearValveMeasurements12251 = new DicomUID("1.2.840.10008.6.1.832", "Cardiac Ultrasound Linear Valve Measurements (12251)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Cardiac Function (12252)</summary>
        public static readonly DicomUID CardiacUltrasoundCardiacFunction12252 = new DicomUID("1.2.840.10008.6.1.833", "Cardiac Ultrasound Cardiac Function (12252)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Area Measurements (12253)</summary>
        public static readonly DicomUID CardiacUltrasoundAreaMeasurements12253 = new DicomUID("1.2.840.10008.6.1.834", "Cardiac Ultrasound Area Measurements (12253)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Hemodynamic Measurements (12254)</summary>
        public static readonly DicomUID CardiacUltrasoundHemodynamicMeasurements12254 = new DicomUID("1.2.840.10008.6.1.835", "Cardiac Ultrasound Hemodynamic Measurements (12254)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Myocardium Measurements (12255)</summary>
        public static readonly DicomUID CardiacUltrasoundMyocardiumMeasurements12255 = new DicomUID("1.2.840.10008.6.1.836", "Cardiac Ultrasound Myocardium Measurements (12255)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Left Ventricle (12257)</summary>
        public static readonly DicomUID CardiacUltrasoundLeftVentricle12257 = new DicomUID("1.2.840.10008.6.1.838", "Cardiac Ultrasound Left Ventricle (12257)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Right Ventricle (12258)</summary>
        public static readonly DicomUID CardiacUltrasoundRightVentricle12258 = new DicomUID("1.2.840.10008.6.1.839", "Cardiac Ultrasound Right Ventricle (12258)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Ventricles Measurements (12259)</summary>
        public static readonly DicomUID CardiacUltrasoundVentriclesMeasurements12259 = new DicomUID("1.2.840.10008.6.1.840", "Cardiac Ultrasound Ventricles Measurements (12259)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Artery (12260)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryArtery12260 = new DicomUID("1.2.840.10008.6.1.841", "Cardiac Ultrasound Pulmonary Artery (12260)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Vein (12261)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryVein12261 = new DicomUID("1.2.840.10008.6.1.842", "Cardiac Ultrasound Pulmonary Vein (12261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Valve (12262)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryValve12262 = new DicomUID("1.2.840.10008.6.1.843", "Cardiac Ultrasound Pulmonary Valve (12262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Pulmonary Measurements (12263)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnPulmonaryMeasurements12263 = new DicomUID("1.2.840.10008.6.1.844", "Cardiac Ultrasound Venous Return Pulmonary Measurements (12263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Systemic Measurements (12264)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnSystemicMeasurements12264 = new DicomUID("1.2.840.10008.6.1.845", "Cardiac Ultrasound Venous Return Systemic Measurements (12264)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atria and Atrial Septum Measurements (12265)</summary>
        public static readonly DicomUID CardiacUltrasoundAtriaAndAtrialSeptumMeasurements12265 = new DicomUID("1.2.840.10008.6.1.846", "Cardiac Ultrasound Atria and Atrial Septum Measurements (12265)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Mitral Valve (12266)</summary>
        public static readonly DicomUID CardiacUltrasoundMitralValve12266 = new DicomUID("1.2.840.10008.6.1.847", "Cardiac Ultrasound Mitral Valve (12266)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Tricuspid Valve (12267)</summary>
        public static readonly DicomUID CardiacUltrasoundTricuspidValve12267 = new DicomUID("1.2.840.10008.6.1.848", "Cardiac Ultrasound Tricuspid Valve (12267)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atrioventricular Valves Measurements (12268)</summary>
        public static readonly DicomUID CardiacUltrasoundAtrioventricularValvesMeasurements12268 = new DicomUID("1.2.840.10008.6.1.849", "Cardiac Ultrasound Atrioventricular Valves Measurements (12268)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Interventricular Septum Measurements (12269)</summary>
        public static readonly DicomUID CardiacUltrasoundInterventricularSeptumMeasurements12269 = new DicomUID("1.2.840.10008.6.1.850", "Cardiac Ultrasound Interventricular Septum Measurements (12269)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aortic Valve (12270)</summary>
        public static readonly DicomUID CardiacUltrasoundAorticValve12270 = new DicomUID("1.2.840.10008.6.1.851", "Cardiac Ultrasound Aortic Valve (12270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Outflow Tracts Measurements (12271)</summary>
        public static readonly DicomUID CardiacUltrasoundOutflowTractsMeasurements12271 = new DicomUID("1.2.840.10008.6.1.852", "Cardiac Ultrasound Outflow Tracts Measurements (12271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Semilunar Valves, Annulate and Sinuses Measurements (12272)</summary>
        public static readonly DicomUID CardiacUltrasoundSemilunarValvesAnnulateAndSinusesMeasurements12272 = new DicomUID("1.2.840.10008.6.1.853", "Cardiac Ultrasound Semilunar Valves, Annulate and Sinuses Measurements (12272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aortic Sinotubular Junction (12273)</summary>
        public static readonly DicomUID CardiacUltrasoundAorticSinotubularJunction12273 = new DicomUID("1.2.840.10008.6.1.854", "Cardiac Ultrasound Aortic Sinotubular Junction (12273)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aorta Measurements (12274)</summary>
        public static readonly DicomUID CardiacUltrasoundAortaMeasurements12274 = new DicomUID("1.2.840.10008.6.1.855", "Cardiac Ultrasound Aorta Measurements (12274)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Coronary Arteries Measurements (12275)</summary>
        public static readonly DicomUID CardiacUltrasoundCoronaryArteriesMeasurements12275 = new DicomUID("1.2.840.10008.6.1.856", "Cardiac Ultrasound Coronary Arteries Measurements (12275)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aorto Pulmonary Connections Measurements (12276)</summary>
        public static readonly DicomUID CardiacUltrasoundAortoPulmonaryConnectionsMeasurements12276 = new DicomUID("1.2.840.10008.6.1.857", "Cardiac Ultrasound Aorto Pulmonary Connections Measurements (12276)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pericardium and Pleura Measurements (12277)</summary>
        public static readonly DicomUID CardiacUltrasoundPericardiumAndPleuraMeasurements12277 = new DicomUID("1.2.840.10008.6.1.858", "Cardiac Ultrasound Pericardium and Pleura Measurements (12277)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Fetal General Measurements (12279)</summary>
        public static readonly DicomUID CardiacUltrasoundFetalGeneralMeasurements12279 = new DicomUID("1.2.840.10008.6.1.859", "Cardiac Ultrasound Fetal General Measurements (12279)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Target Sites (12280)</summary>
        public static readonly DicomUID CardiacUltrasoundTargetSites12280 = new DicomUID("1.2.840.10008.6.1.860", "Cardiac Ultrasound Target Sites (12280)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Target Site Modifiers (12281)</summary>
        public static readonly DicomUID CardiacUltrasoundTargetSiteModifiers12281 = new DicomUID("1.2.840.10008.6.1.861", "Cardiac Ultrasound Target Site Modifiers (12281)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Systemic Finding Sites (12282)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnSystemicFindingSites12282 = new DicomUID("1.2.840.10008.6.1.862", "Cardiac Ultrasound Venous Return Systemic Finding Sites (12282)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Pulmonary Finding Sites (12283)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnPulmonaryFindingSites12283 = new DicomUID("1.2.840.10008.6.1.863", "Cardiac Ultrasound Venous Return Pulmonary Finding Sites (12283)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atria and Atrial Septum Finding Sites (12284)</summary>
        public static readonly DicomUID CardiacUltrasoundAtriaAndAtrialSeptumFindingSites12284 = new DicomUID("1.2.840.10008.6.1.864", "Cardiac Ultrasound Atria and Atrial Septum Finding Sites (12284)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atrioventricular Valves Finding Sites (12285)</summary>
        public static readonly DicomUID CardiacUltrasoundAtrioventricularValvesFindingSites12285 = new DicomUID("1.2.840.10008.6.1.865", "Cardiac Ultrasound Atrioventricular Valves Finding Sites (12285)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Interventricular Septum Finding Sites (12286)</summary>
        public static readonly DicomUID CardiacUltrasoundInterventricularSeptumFindingSites12286 = new DicomUID("1.2.840.10008.6.1.866", "Cardiac Ultrasound Interventricular Septum Finding Sites (12286)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Ventricles Finding Sites (12287)</summary>
        public static readonly DicomUID CardiacUltrasoundVentriclesFindingSites12287 = new DicomUID("1.2.840.10008.6.1.867", "Cardiac Ultrasound Ventricles Finding Sites (12287)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Outflow Tracts Finding Sites (12288)</summary>
        public static readonly DicomUID CardiacUltrasoundOutflowTractsFindingSites12288 = new DicomUID("1.2.840.10008.6.1.868", "Cardiac Ultrasound Outflow Tracts Finding Sites (12288)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Semilunar Valves, Annulus and Sinuses Finding Sites (12289)</summary>
        public static readonly DicomUID CardiacUltrasoundSemilunarValvesAnnulusAndSinusesFindingSites12289 = new DicomUID("1.2.840.10008.6.1.869", "Cardiac Ultrasound Semilunar Valves, Annulus and Sinuses Finding Sites (12289)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Arteries Finding Sites (12290)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryArteriesFindingSites12290 = new DicomUID("1.2.840.10008.6.1.870", "Cardiac Ultrasound Pulmonary Arteries Finding Sites (12290)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aorta Finding Sites (12291)</summary>
        public static readonly DicomUID CardiacUltrasoundAortaFindingSites12291 = new DicomUID("1.2.840.10008.6.1.871", "Cardiac Ultrasound Aorta Finding Sites (12291)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Coronary Arteries Finding Sites (12292)</summary>
        public static readonly DicomUID CardiacUltrasoundCoronaryArteriesFindingSites12292 = new DicomUID("1.2.840.10008.6.1.872", "Cardiac Ultrasound Coronary Arteries Finding Sites (12292)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aortopulmonary Connections Finding Sites (12293)</summary>
        public static readonly DicomUID CardiacUltrasoundAortopulmonaryConnectionsFindingSites12293 = new DicomUID("1.2.840.10008.6.1.873", "Cardiac Ultrasound Aortopulmonary Connections Finding Sites (12293)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pericardium and Pleura Finding Sites (12294)</summary>
        public static readonly DicomUID CardiacUltrasoundPericardiumAndPleuraFindingSites12294 = new DicomUID("1.2.840.10008.6.1.874", "Cardiac Ultrasound Pericardium and Pleura Finding Sites (12294)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Ultrasound Axial Measurements Type (4230)</summary>
        public static readonly DicomUID OphthalmicUltrasoundAxialMeasurementsType4230 = new DicomUID("1.2.840.10008.6.1.876", "Ophthalmic Ultrasound Axial Measurements Type (4230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lens Status (4231)</summary>
        public static readonly DicomUID LensStatus4231 = new DicomUID("1.2.840.10008.6.1.877", "Lens Status (4231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vitreous Status (4232)</summary>
        public static readonly DicomUID VitreousStatus4232 = new DicomUID("1.2.840.10008.6.1.878", "Vitreous Status (4232)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Axial Length Measurements Segment Names (4233)</summary>
        public static readonly DicomUID OphthalmicAxialLengthMeasurementsSegmentNames4233 = new DicomUID("1.2.840.10008.6.1.879", "Ophthalmic Axial Length Measurements Segment Names (4233)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Refractive Surgery Types (4234)</summary>
        public static readonly DicomUID RefractiveSurgeryTypes4234 = new DicomUID("1.2.840.10008.6.1.880", "Refractive Surgery Types (4234)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Keratometry Descriptors (4235)</summary>
        public static readonly DicomUID KeratometryDescriptors4235 = new DicomUID("1.2.840.10008.6.1.881", "Keratometry Descriptors (4235)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IOL Calculation Formula (4236)</summary>
        public static readonly DicomUID IOLCalculationFormula4236 = new DicomUID("1.2.840.10008.6.1.882", "IOL Calculation Formula (4236)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lens Constant Type (4237)</summary>
        public static readonly DicomUID LensConstantType4237 = new DicomUID("1.2.840.10008.6.1.883", "Lens Constant Type (4237)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Refractive Error Types (4238)</summary>
        public static readonly DicomUID RefractiveErrorTypes4238 = new DicomUID("1.2.840.10008.6.1.884", "Refractive Error Types (4238)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anterior Chamber Depth Definition (4239)</summary>
        public static readonly DicomUID AnteriorChamberDepthDefinition4239 = new DicomUID("1.2.840.10008.6.1.885", "Anterior Chamber Depth Definition (4239)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Measurement or Calculation Data Source (4240)</summary>
        public static readonly DicomUID OphthalmicMeasurementOrCalculationDataSource4240 = new DicomUID("1.2.840.10008.6.1.886", "Ophthalmic Measurement or Calculation Data Source (4240)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Axial Length Selection Method (4241)</summary>
        public static readonly DicomUID OphthalmicAxialLengthSelectionMethod4241 = new DicomUID("1.2.840.10008.6.1.887", "Ophthalmic Axial Length Selection Method (4241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Quality Metric Type (4243)</summary>
        public static readonly DicomUID OphthalmicQualityMetricType4243 = new DicomUID("1.2.840.10008.6.1.889", "Ophthalmic Quality Metric Type (4243)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Agent Concentration Units (4244)</summary>
        public static readonly DicomUID OphthalmicAgentConcentrationUnits4244 = new DicomUID("1.2.840.10008.6.1.890", "Ophthalmic Agent Concentration Units (4244)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Functional Condition Present During Acquisition (91)</summary>
        public static readonly DicomUID FunctionalConditionPresentDuringAcquisition91 = new DicomUID("1.2.840.10008.6.1.891", "Functional Condition Present During Acquisition (91)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Joint Position During Acquisition (92)</summary>
        public static readonly DicomUID JointPositionDuringAcquisition92 = new DicomUID("1.2.840.10008.6.1.892", "Joint Position During Acquisition (92)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Joint Positioning Method (93)</summary>
        public static readonly DicomUID JointPositioningMethod93 = new DicomUID("1.2.840.10008.6.1.893", "Joint Positioning Method (93)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physical Force Applied During Acquisition (94)</summary>
        public static readonly DicomUID PhysicalForceAppliedDuringAcquisition94 = new DicomUID("1.2.840.10008.6.1.894", "Physical Force Applied During Acquisition (94)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Control Variables Numeric (3690)</summary>
        public static readonly DicomUID ECGControlVariablesNumeric3690 = new DicomUID("1.2.840.10008.6.1.895", "ECG Control Variables Numeric (3690)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Control Variables Text (3691)</summary>
        public static readonly DicomUID ECGControlVariablesText3691 = new DicomUID("1.2.840.10008.6.1.896", "ECG Control Variables Text (3691)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: WSI Referenced Image Purposes of Reference (8120)</summary>
        public static readonly DicomUID WSIReferencedImagePurposesOfReference8120 = new DicomUID("1.2.840.10008.6.1.897", "WSI Referenced Image Purposes of Reference (8120)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Microscopy Lens Type (8121)</summary>
        public static readonly DicomUID MicroscopyLensType8121 = new DicomUID("1.2.840.10008.6.1.898", "Microscopy Lens Type (8121)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Microscopy Illuminator and Sensor Color (8122)</summary>
        public static readonly DicomUID MicroscopyIlluminatorAndSensorColor8122 = new DicomUID("1.2.840.10008.6.1.899", "Microscopy Illuminator and Sensor Color (8122)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Microscopy Illumination Method (8123)</summary>
        public static readonly DicomUID MicroscopyIlluminationMethod8123 = new DicomUID("1.2.840.10008.6.1.900", "Microscopy Illumination Method (8123)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Microscopy Filter (8124)</summary>
        public static readonly DicomUID MicroscopyFilter8124 = new DicomUID("1.2.840.10008.6.1.901", "Microscopy Filter (8124)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Microscopy Illuminator Type (8125)</summary>
        public static readonly DicomUID MicroscopyIlluminatorType8125 = new DicomUID("1.2.840.10008.6.1.902", "Microscopy Illuminator Type (8125)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Audit Event ID (400)</summary>
        public static readonly DicomUID AuditEventID400 = new DicomUID("1.2.840.10008.6.1.903", "Audit Event ID (400)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Audit Event Type Code (401)</summary>
        public static readonly DicomUID AuditEventTypeCode401 = new DicomUID("1.2.840.10008.6.1.904", "Audit Event Type Code (401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Audit Active Participant Role ID Code (402)</summary>
        public static readonly DicomUID AuditActiveParticipantRoleIDCode402 = new DicomUID("1.2.840.10008.6.1.905", "Audit Active Participant Role ID Code (402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Security Alert Type Code (403)</summary>
        public static readonly DicomUID SecurityAlertTypeCode403 = new DicomUID("1.2.840.10008.6.1.906", "Security Alert Type Code (403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Audit Participant Object ID Type Code (404)</summary>
        public static readonly DicomUID AuditParticipantObjectIDTypeCode404 = new DicomUID("1.2.840.10008.6.1.907", "Audit Participant Object ID Type Code (404)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Media Type Code (405)</summary>
        public static readonly DicomUID MediaTypeCode405 = new DicomUID("1.2.840.10008.6.1.908", "Media Type Code (405)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Test Patterns (4250)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryTestPatterns4250 = new DicomUID("1.2.840.10008.6.1.909", "Visual Field Static Perimetry Test Patterns (4250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Test Strategies (4251)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryTestStrategies4251 = new DicomUID("1.2.840.10008.6.1.910", "Visual Field Static Perimetry Test Strategies (4251)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Screening Test Modes (4252)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryScreeningTestModes4252 = new DicomUID("1.2.840.10008.6.1.911", "Visual Field Static Perimetry Screening Test Modes (4252)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Fixation Strategy (4253)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryFixationStrategy4253 = new DicomUID("1.2.840.10008.6.1.912", "Visual Field Static Perimetry Fixation Strategy (4253)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Test Analysis Results (4254)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryTestAnalysisResults4254 = new DicomUID("1.2.840.10008.6.1.913", "Visual Field Static Perimetry Test Analysis Results (4254)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Illumination Color (4255)</summary>
        public static readonly DicomUID VisualFieldIlluminationColor4255 = new DicomUID("1.2.840.10008.6.1.914", "Visual Field Illumination Color (4255)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Procedure Modifier (4256)</summary>
        public static readonly DicomUID VisualFieldProcedureModifier4256 = new DicomUID("1.2.840.10008.6.1.915", "Visual Field Procedure Modifier (4256)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Global Index Name (4257)</summary>
        public static readonly DicomUID VisualFieldGlobalIndexName4257 = new DicomUID("1.2.840.10008.6.1.916", "Visual Field Global Index Name (4257)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Component Semantics (7180)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelComponentSemantics7180 = new DicomUID("1.2.840.10008.6.1.917", "Abstract Multi-dimensional Image Model Component Semantics (7180)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Component Units (7181)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelComponentUnits7181 = new DicomUID("1.2.840.10008.6.1.918", "Abstract Multi-dimensional Image Model Component Units (7181)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Dimension Semantics (7182)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelDimensionSemantics7182 = new DicomUID("1.2.840.10008.6.1.919", "Abstract Multi-dimensional Image Model Dimension Semantics (7182)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Dimension Units (7183)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelDimensionUnits7183 = new DicomUID("1.2.840.10008.6.1.920", "Abstract Multi-dimensional Image Model Dimension Units (7183)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Axis Direction (7184)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelAxisDirection7184 = new DicomUID("1.2.840.10008.6.1.921", "Abstract Multi-dimensional Image Model Axis Direction (7184)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Axis Orientation (7185)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelAxisOrientation7185 = new DicomUID("1.2.840.10008.6.1.922", "Abstract Multi-dimensional Image Model Axis Orientation (7185)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Qualitative Dimension Sample Semantics (7186)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelQualitativeDimensionSampleSemantics7186 = new DicomUID("1.2.840.10008.6.1.923", "Abstract Multi-dimensional Image Model Qualitative Dimension Sample Semantics (7186)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Planning Methods (7320)</summary>
        public static readonly DicomUID PlanningMethods7320 = new DicomUID("1.2.840.10008.6.1.924", "Planning Methods (7320)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: De-identification Method (7050)</summary>
        public static readonly DicomUID DeIdentificationMethod7050 = new DicomUID("1.2.840.10008.6.1.925", "De-identification Method (7050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Orientation (12118)</summary>
        public static readonly DicomUID MeasurementOrientation12118 = new DicomUID("1.2.840.10008.6.1.926", "Measurement Orientation (12118)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Global Waveform Durations (3689)</summary>
        public static readonly DicomUID ECGGlobalWaveformDurations3689 = new DicomUID("1.2.840.10008.6.1.927", "ECG Global Waveform Durations (3689)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ICDs (3692)</summary>
        public static readonly DicomUID ICDs3692 = new DicomUID("1.2.840.10008.6.1.930", "ICDs (3692)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy General Workitem Definition (9241)</summary>
        public static readonly DicomUID RadiotherapyGeneralWorkitemDefinition9241 = new DicomUID("1.2.840.10008.6.1.931", "Radiotherapy General Workitem Definition (9241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Acquisition Workitem Definition (9242)</summary>
        public static readonly DicomUID RadiotherapyAcquisitionWorkitemDefinition9242 = new DicomUID("1.2.840.10008.6.1.932", "Radiotherapy Acquisition Workitem Definition (9242)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Registration Workitem Definition (9243)</summary>
        public static readonly DicomUID RadiotherapyRegistrationWorkitemDefinition9243 = new DicomUID("1.2.840.10008.6.1.933", "Radiotherapy Registration Workitem Definition (9243)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contrast Bolus Substance (3850)</summary>
        public static readonly DicomUID ContrastBolusSubstance3850 = new DicomUID("1.2.840.10008.6.1.934", "Contrast Bolus Substance (3850)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Label Types (10022)</summary>
        public static readonly DicomUID LabelTypes10022 = new DicomUID("1.2.840.10008.6.1.935", "Label Types (10022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Mapping Units for Real World Value Mapping (4260)</summary>
        public static readonly DicomUID OphthalmicMappingUnitsForRealWorldValueMapping4260 = new DicomUID("1.2.840.10008.6.1.936", "Ophthalmic Mapping Units for Real World Value Mapping (4260)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Mapping Acquisition Method (4261)</summary>
        public static readonly DicomUID OphthalmicMappingAcquisitionMethod4261 = new DicomUID("1.2.840.10008.6.1.937", "Ophthalmic Mapping Acquisition Method (4261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Retinal Thickness Definition (4262)</summary>
        public static readonly DicomUID RetinalThicknessDefinition4262 = new DicomUID("1.2.840.10008.6.1.938", "Retinal Thickness Definition (4262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Thickness Map Value Type (4263)</summary>
        public static readonly DicomUID OphthalmicThicknessMapValueType4263 = new DicomUID("1.2.840.10008.6.1.939", "Ophthalmic Thickness Map Value Type (4263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Map Purposes of Reference (4264)</summary>
        public static readonly DicomUID OphthalmicMapPurposesOfReference4264 = new DicomUID("1.2.840.10008.6.1.940", "Ophthalmic Map Purposes of Reference (4264)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Thickness Deviation Categories (4265)</summary>
        public static readonly DicomUID OphthalmicThicknessDeviationCategories4265 = new DicomUID("1.2.840.10008.6.1.941", "Ophthalmic Thickness Deviation Categories (4265)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Anatomic Structure Reference Point (4266)</summary>
        public static readonly DicomUID OphthalmicAnatomicStructureReferencePoint4266 = new DicomUID("1.2.840.10008.6.1.942", "Ophthalmic Anatomic Structure Reference Point (4266)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Synchronization Technique (3104)</summary>
        public static readonly DicomUID CardiacSynchronizationTechnique3104 = new DicomUID("1.2.840.10008.6.1.943", "Cardiac Synchronization Technique (3104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Staining Protocols (8130)</summary>
        public static readonly DicomUID StainingProtocols8130 = new DicomUID("1.2.840.10008.6.1.944", "Staining Protocols (8130)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Size Specific Dose Estimation Method for CT (10023)</summary>
        public static readonly DicomUID SizeSpecificDoseEstimationMethodForCT10023 = new DicomUID("1.2.840.10008.6.1.947", "Size Specific Dose Estimation Method for CT (10023)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pathology Imaging Protocols (8131)</summary>
        public static readonly DicomUID PathologyImagingProtocols8131 = new DicomUID("1.2.840.10008.6.1.948", "Pathology Imaging Protocols (8131)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Magnification Selection (8132)</summary>
        public static readonly DicomUID MagnificationSelection8132 = new DicomUID("1.2.840.10008.6.1.949", "Magnification Selection (8132)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tissue Selection (8133)</summary>
        public static readonly DicomUID TissueSelection8133 = new DicomUID("1.2.840.10008.6.1.950", "Tissue Selection (8133)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Region of Interest Measurement Modifiers (7464)</summary>
        public static readonly DicomUID GeneralRegionOfInterestMeasurementModifiers7464 = new DicomUID("1.2.840.10008.6.1.951", "General Region of Interest Measurement Modifiers (7464)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurements Derived From Multiple ROI Measurements (7465)</summary>
        public static readonly DicomUID MeasurementsDerivedFromMultipleROIMeasurements7465 = new DicomUID("1.2.840.10008.6.1.952", "Measurements Derived From Multiple ROI Measurements (7465)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Scan Acquisition Types (8201)</summary>
        public static readonly DicomUID SurfaceScanAcquisitionTypes8201 = new DicomUID("1.2.840.10008.6.1.953", "Surface Scan Acquisition Types (8201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Scan Mode Types (8202)</summary>
        public static readonly DicomUID SurfaceScanModeTypes8202 = new DicomUID("1.2.840.10008.6.1.954", "Surface Scan Mode Types (8202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Scan Registration Method Types (8203)</summary>
        public static readonly DicomUID SurfaceScanRegistrationMethodTypes8203 = new DicomUID("1.2.840.10008.6.1.956", "Surface Scan Registration Method Types (8203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Basic Cardiac Views (27)</summary>
        public static readonly DicomUID BasicCardiacViews27 = new DicomUID("1.2.840.10008.6.1.957", "Basic Cardiac Views (27)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT Reconstruction Algorithm (10033)</summary>
        public static readonly DicomUID CTReconstructionAlgorithm10033 = new DicomUID("1.2.840.10008.6.1.958", "CT Reconstruction Algorithm (10033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Detector Types (10030)</summary>
        public static readonly DicomUID DetectorTypes10030 = new DicomUID("1.2.840.10008.6.1.959", "Detector Types (10030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CR/DR Mechanical Configuration (10031)</summary>
        public static readonly DicomUID CRDRMechanicalConfiguration10031 = new DicomUID("1.2.840.10008.6.1.960", "CR/DR Mechanical Configuration (10031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Projection X-Ray Acquisition Device Types (10032)</summary>
        public static readonly DicomUID ProjectionXRayAcquisitionDeviceTypes10032 = new DicomUID("1.2.840.10008.6.1.961", "Projection X-Ray Acquisition Device Types (10032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Segmentation Types (7165)</summary>
        public static readonly DicomUID AbstractSegmentationTypes7165 = new DicomUID("1.2.840.10008.6.1.962", "Abstract Segmentation Types (7165)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Common Tissue Segmentation Types (7166)</summary>
        public static readonly DicomUID CommonTissueSegmentationTypes7166 = new DicomUID("1.2.840.10008.6.1.963", "Common Tissue Segmentation Types (7166)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Peripheral Nervous System Segmentation Types (7167)</summary>
        public static readonly DicomUID PeripheralNervousSystemSegmentationTypes7167 = new DicomUID("1.2.840.10008.6.1.964", "Peripheral Nervous System Segmentation Types (7167)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Corneal Topography Mapping Units for Real World Value Mapping (4267)</summary>
        public static readonly DicomUID CornealTopographyMappingUnitsForRealWorldValueMapping4267 = new DicomUID("1.2.840.10008.6.1.965", "Corneal Topography Mapping Units for Real World Value Mapping (4267)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Corneal Topography Map Value Type (4268)</summary>
        public static readonly DicomUID CornealTopographyMapValueType4268 = new DicomUID("1.2.840.10008.6.1.966", "Corneal Topography Map Value Type (4268)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Structures for Volumetric Measurements (7140)</summary>
        public static readonly DicomUID BrainStructuresForVolumetricMeasurements7140 = new DicomUID("1.2.840.10008.6.1.967", "Brain Structures for Volumetric Measurements (7140)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Dose Derivation (7220)</summary>
        public static readonly DicomUID RTDoseDerivation7220 = new DicomUID("1.2.840.10008.6.1.968", "RT Dose Derivation (7220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Dose Purpose of Reference (7221)</summary>
        public static readonly DicomUID RTDosePurposeOfReference7221 = new DicomUID("1.2.840.10008.6.1.969", "RT Dose Purpose of Reference (7221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Spectroscopy Purpose of Reference (7215)</summary>
        public static readonly DicomUID SpectroscopyPurposeOfReference7215 = new DicomUID("1.2.840.10008.6.1.970", "Spectroscopy Purpose of Reference (7215)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Scheduled Processing Parameter Concept Codes for RT Treatment (9250)</summary>
        public static readonly DicomUID ScheduledProcessingParameterConceptCodesForRTTreatment9250 = new DicomUID("1.2.840.10008.6.1.971", "Scheduled Processing Parameter Concept Codes for RT Treatment (9250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiopharmaceutical Organ Dose Reference Authority (10040)</summary>
        public static readonly DicomUID RadiopharmaceuticalOrganDoseReferenceAuthority10040 = new DicomUID("1.2.840.10008.6.1.972", "Radiopharmaceutical Organ Dose Reference Authority (10040)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source of Radioisotope Activity Information (10041)</summary>
        public static readonly DicomUID SourceOfRadioisotopeActivityInformation10041 = new DicomUID("1.2.840.10008.6.1.973", "Source of Radioisotope Activity Information (10041)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intravenous Extravasation Symptoms (10043)</summary>
        public static readonly DicomUID IntravenousExtravasationSymptoms10043 = new DicomUID("1.2.840.10008.6.1.975", "Intravenous Extravasation Symptoms (10043)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiosensitive Organs (10044)</summary>
        public static readonly DicomUID RadiosensitiveOrgans10044 = new DicomUID("1.2.840.10008.6.1.976", "Radiosensitive Organs (10044)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiopharmaceutical Patient State (10045)</summary>
        public static readonly DicomUID RadiopharmaceuticalPatientState10045 = new DicomUID("1.2.840.10008.6.1.977", "Radiopharmaceutical Patient State (10045)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: GFR Measurements (10046)</summary>
        public static readonly DicomUID GFRMeasurements10046 = new DicomUID("1.2.840.10008.6.1.978", "GFR Measurements (10046)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: GFR Measurement Methods (10047)</summary>
        public static readonly DicomUID GFRMeasurementMethods10047 = new DicomUID("1.2.840.10008.6.1.979", "GFR Measurement Methods (10047)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Evaluation Methods (8300)</summary>
        public static readonly DicomUID VisualEvaluationMethods8300 = new DicomUID("1.2.840.10008.6.1.980", "Visual Evaluation Methods (8300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Test Pattern Codes (8301)</summary>
        public static readonly DicomUID TestPatternCodes8301 = new DicomUID("1.2.840.10008.6.1.981", "Test Pattern Codes (8301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Pattern Codes (8302)</summary>
        public static readonly DicomUID MeasurementPatternCodes8302 = new DicomUID("1.2.840.10008.6.1.982", "Measurement Pattern Codes (8302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Display Device Type (8303)</summary>
        public static readonly DicomUID DisplayDeviceType8303 = new DicomUID("1.2.840.10008.6.1.983", "Display Device Type (8303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: SUV Units (85)</summary>
        public static readonly DicomUID SUVUnits85 = new DicomUID("1.2.840.10008.6.1.984", "SUV Units (85)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: T1 Measurement Methods (4100)</summary>
        public static readonly DicomUID T1MeasurementMethods4100 = new DicomUID("1.2.840.10008.6.1.985", "T1 Measurement Methods (4100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tracer Kinetic Models (4101)</summary>
        public static readonly DicomUID TracerKineticModels4101 = new DicomUID("1.2.840.10008.6.1.986", "Tracer Kinetic Models (4101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Measurement Methods (4102)</summary>
        public static readonly DicomUID PerfusionMeasurementMethods4102 = new DicomUID("1.2.840.10008.6.1.987", "Perfusion Measurement Methods (4102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Input Function Measurement Methods (4103)</summary>
        public static readonly DicomUID ArterialInputFunctionMeasurementMethods4103 = new DicomUID("1.2.840.10008.6.1.988", "Arterial Input Function Measurement Methods (4103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Bolus Arrival Time Derivation Methods (4104)</summary>
        public static readonly DicomUID BolusArrivalTimeDerivationMethods4104 = new DicomUID("1.2.840.10008.6.1.989", "Bolus Arrival Time Derivation Methods (4104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Analysis Methods (4105)</summary>
        public static readonly DicomUID PerfusionAnalysisMethods4105 = new DicomUID("1.2.840.10008.6.1.990", "Perfusion Analysis Methods (4105)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quantitative Methods used for Perfusion And Tracer Kinetic Models (4106)</summary>
        public static readonly DicomUID QuantitativeMethodsUsedForPerfusionAndTracerKineticModels4106 = new DicomUID("1.2.840.10008.6.1.991", "Quantitative Methods used for Perfusion And Tracer Kinetic Models (4106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tracer Kinetic Model Parameters (4107)</summary>
        public static readonly DicomUID TracerKineticModelParameters4107 = new DicomUID("1.2.840.10008.6.1.992", "Tracer Kinetic Model Parameters (4107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Model Parameters (4108)</summary>
        public static readonly DicomUID PerfusionModelParameters4108 = new DicomUID("1.2.840.10008.6.1.993", "Perfusion Model Parameters (4108)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model-Independent Dynamic Contrast Analysis Parameters (4109)</summary>
        public static readonly DicomUID ModelIndependentDynamicContrastAnalysisParameters4109 = new DicomUID("1.2.840.10008.6.1.994", "Model-Independent Dynamic Contrast Analysis Parameters (4109)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tracer Kinetic Modeling Covariates (4110)</summary>
        public static readonly DicomUID TracerKineticModelingCovariates4110 = new DicomUID("1.2.840.10008.6.1.995", "Tracer Kinetic Modeling Covariates (4110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contrast Characteristics (4111)</summary>
        public static readonly DicomUID ContrastCharacteristics4111 = new DicomUID("1.2.840.10008.6.1.996", "Contrast Characteristics (4111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Report Document Titles (7021)</summary>
        public static readonly DicomUID MeasurementReportDocumentTitles7021 = new DicomUID("1.2.840.10008.6.1.997", "Measurement Report Document Titles (7021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quantitative Diagnostic Imaging Procedures (100)</summary>
        public static readonly DicomUID QuantitativeDiagnosticImagingProcedures100 = new DicomUID("1.2.840.10008.6.1.998", "Quantitative Diagnostic Imaging Procedures (100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Region of Interest Measurements (7466)</summary>
        public static readonly DicomUID PETRegionOfInterestMeasurements7466 = new DicomUID("1.2.840.10008.6.1.999", "PET Region of Interest Measurements (7466)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gray Level Co-occurrence Matrix Measurements (7467)</summary>
        public static readonly DicomUID GrayLevelCoOccurrenceMatrixMeasurements7467 = new DicomUID("1.2.840.10008.6.1.1000", "Gray Level Co-occurrence Matrix Measurements (7467)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Texture Measurements (7468)</summary>
        public static readonly DicomUID TextureMeasurements7468 = new DicomUID("1.2.840.10008.6.1.1001", "Texture Measurements (7468)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time Point Types (6146)</summary>
        public static readonly DicomUID TimePointTypes6146 = new DicomUID("1.2.840.10008.6.1.1002", "Time Point Types (6146)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Generic Intensity and Size Measurements (7469)</summary>
        public static readonly DicomUID GenericIntensityAndSizeMeasurements7469 = new DicomUID("1.2.840.10008.6.1.1003", "Generic Intensity and Size Measurements (7469)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Response Criteria (6147)</summary>
        public static readonly DicomUID ResponseCriteria6147 = new DicomUID("1.2.840.10008.6.1.1004", "Response Criteria (6147)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Biometry Anatomic Sites (12020)</summary>
        public static readonly DicomUID FetalBiometryAnatomicSites12020 = new DicomUID("1.2.840.10008.6.1.1005", "Fetal Biometry Anatomic Sites (12020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Long Bone Anatomic Sites (12021)</summary>
        public static readonly DicomUID FetalLongBoneAnatomicSites12021 = new DicomUID("1.2.840.10008.6.1.1006", "Fetal Long Bone Anatomic Sites (12021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Cranium Anatomic Sites (12022)</summary>
        public static readonly DicomUID FetalCraniumAnatomicSites12022 = new DicomUID("1.2.840.10008.6.1.1007", "Fetal Cranium Anatomic Sites (12022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pelvis and Uterus Anatomic Sites (12023)</summary>
        public static readonly DicomUID PelvisAndUterusAnatomicSites12023 = new DicomUID("1.2.840.10008.6.1.1008", "Pelvis and Uterus Anatomic Sites (12023)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Parametric Map Derivation Image Purpose of Reference (7222)</summary>
        public static readonly DicomUID ParametricMapDerivationImagePurposeOfReference7222 = new DicomUID("1.2.840.10008.6.1.1009", "Parametric Map Derivation Image Purpose of Reference (7222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physical Quantity Descriptors (9000)</summary>
        public static readonly DicomUID PhysicalQuantityDescriptors9000 = new DicomUID("1.2.840.10008.6.1.1010", "Physical Quantity Descriptors (9000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lymph Node Anatomic Sites (7600)</summary>
        public static readonly DicomUID LymphNodeAnatomicSites7600 = new DicomUID("1.2.840.10008.6.1.1011", "Lymph Node Anatomic Sites (7600)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Head and Neck Cancer Anatomic Sites (7601)</summary>
        public static readonly DicomUID HeadAndNeckCancerAnatomicSites7601 = new DicomUID("1.2.840.10008.6.1.1012", "Head and Neck Cancer Anatomic Sites (7601)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiber Tracts In Brainstem (7701)</summary>
        public static readonly DicomUID FiberTractsInBrainstem7701 = new DicomUID("1.2.840.10008.6.1.1013", "Fiber Tracts In Brainstem (7701)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Projection and Thalamic Fibers (7702)</summary>
        public static readonly DicomUID ProjectionAndThalamicFibers7702 = new DicomUID("1.2.840.10008.6.1.1014", "Projection and Thalamic Fibers (7702)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Association Fibers (7703)</summary>
        public static readonly DicomUID AssociationFibers7703 = new DicomUID("1.2.840.10008.6.1.1015", "Association Fibers (7703)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Limbic System Tracts (7704)</summary>
        public static readonly DicomUID LimbicSystemTracts7704 = new DicomUID("1.2.840.10008.6.1.1016", "Limbic System Tracts (7704)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Commissural Fibers (7705)</summary>
        public static readonly DicomUID CommissuralFibers7705 = new DicomUID("1.2.840.10008.6.1.1017", "Commissural Fibers (7705)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cranial Nerves (7706)</summary>
        public static readonly DicomUID CranialNerves7706 = new DicomUID("1.2.840.10008.6.1.1018", "Cranial Nerves (7706)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Spinal Cord Fibers (7707)</summary>
        public static readonly DicomUID SpinalCordFibers7707 = new DicomUID("1.2.840.10008.6.1.1019", "Spinal Cord Fibers (7707)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tractography Anatomic Sites (7710)</summary>
        public static readonly DicomUID TractographyAnatomicSites7710 = new DicomUID("1.2.840.10008.6.1.1020", "Tractography Anatomic Sites (7710)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Primary Anatomic Structure for Intra-oral Radiography (Supernumerary Dentition - Designation of Teeth) (4025)</summary>
        public static readonly DicomUID PrimaryAnatomicStructureForIntraOralRadiographySupernumeraryDentitionDesignationOfTeeth4025 = new DicomUID("1.2.840.10008.6.1.1021", "Primary Anatomic Structure for Intra-oral Radiography (Supernumerary Dentition - Designation of Teeth) (4025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Primary Anatomic Structure for Intra-oral and Craniofacial Radiography - Teeth (4026)</summary>
        public static readonly DicomUID PrimaryAnatomicStructureForIntraOralAndCraniofacialRadiographyTeeth4026 = new DicomUID("1.2.840.10008.6.1.1022", "Primary Anatomic Structure for Intra-oral and Craniofacial Radiography - Teeth (4026)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IEC61217 Device Position Parameters (9401)</summary>
        public static readonly DicomUID IEC61217DevicePositionParameters9401 = new DicomUID("1.2.840.10008.6.1.1023", "IEC61217 Device Position Parameters (9401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IEC61217 Gantry Position Parameters (9402)</summary>
        public static readonly DicomUID IEC61217GantryPositionParameters9402 = new DicomUID("1.2.840.10008.6.1.1024", "IEC61217 Gantry Position Parameters (9402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IEC61217 Patient Support Position Parameters (9403)</summary>
        public static readonly DicomUID IEC61217PatientSupportPositionParameters9403 = new DicomUID("1.2.840.10008.6.1.1025", "IEC61217 Patient Support Position Parameters (9403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Actionable Finding Classification (7035)</summary>
        public static readonly DicomUID ActionableFindingClassification7035 = new DicomUID("1.2.840.10008.6.1.1026", "Actionable Finding Classification (7035)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Quality Assessment (7036)</summary>
        public static readonly DicomUID ImageQualityAssessment7036 = new DicomUID("1.2.840.10008.6.1.1027", "Image Quality Assessment (7036)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Summary Radiation Exposure Quantities (10050)</summary>
        public static readonly DicomUID SummaryRadiationExposureQuantities10050 = new DicomUID("1.2.840.10008.6.1.1028", "Summary Radiation Exposure Quantities (10050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wide Field Ophthalmic Photography Transformation Method (4245)</summary>
        public static readonly DicomUID WideFieldOphthalmicPhotographyTransformationMethod4245 = new DicomUID("1.2.840.10008.6.1.1029", "Wide Field Ophthalmic Photography Transformation Method (4245)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Units (84)</summary>
        public static readonly DicomUID PETUnits84 = new DicomUID("1.2.840.10008.6.1.1030", "PET Units (84)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Materials (7300)</summary>
        public static readonly DicomUID ImplantMaterials7300 = new DicomUID("1.2.840.10008.6.1.1031", "Implant Materials (7300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intervention Types (7301)</summary>
        public static readonly DicomUID InterventionTypes7301 = new DicomUID("1.2.840.10008.6.1.1032", "Intervention Types (7301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Templates View Orientations (7302)</summary>
        public static readonly DicomUID ImplantTemplatesViewOrientations7302 = new DicomUID("1.2.840.10008.6.1.1033", "Implant Templates View Orientations (7302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Templates Modified View Orientations (7303)</summary>
        public static readonly DicomUID ImplantTemplatesModifiedViewOrientations7303 = new DicomUID("1.2.840.10008.6.1.1034", "Implant Templates Modified View Orientations (7303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Target Anatomy (7304)</summary>
        public static readonly DicomUID ImplantTargetAnatomy7304 = new DicomUID("1.2.840.10008.6.1.1035", "Implant Target Anatomy (7304)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Planning Landmarks (7305)</summary>
        public static readonly DicomUID ImplantPlanningLandmarks7305 = new DicomUID("1.2.840.10008.6.1.1036", "Implant Planning Landmarks (7305)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Human Hip Implant Planning Landmarks (7306)</summary>
        public static readonly DicomUID HumanHipImplantPlanningLandmarks7306 = new DicomUID("1.2.840.10008.6.1.1037", "Human Hip Implant Planning Landmarks (7306)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Component Types (7307)</summary>
        public static readonly DicomUID ImplantComponentTypes7307 = new DicomUID("1.2.840.10008.6.1.1038", "Implant Component Types (7307)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Human Hip Implant Component Types (7308)</summary>
        public static readonly DicomUID HumanHipImplantComponentTypes7308 = new DicomUID("1.2.840.10008.6.1.1039", "Human Hip Implant Component Types (7308)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Human Trauma Implant Component Types (7309)</summary>
        public static readonly DicomUID HumanTraumaImplantComponentTypes7309 = new DicomUID("1.2.840.10008.6.1.1040", "Human Trauma Implant Component Types (7309)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Fixation Method (7310)</summary>
        public static readonly DicomUID ImplantFixationMethod7310 = new DicomUID("1.2.840.10008.6.1.1041", "Implant Fixation Method (7310)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Participating Roles (7445)</summary>
        public static readonly DicomUID DeviceParticipatingRoles7445 = new DicomUID("1.2.840.10008.6.1.1042", "Device Participating Roles (7445)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Container Types (8101)</summary>
        public static readonly DicomUID ContainerTypes8101 = new DicomUID("1.2.840.10008.6.1.1043", "Container Types (8101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Container Component Types (8102)</summary>
        public static readonly DicomUID ContainerComponentTypes8102 = new DicomUID("1.2.840.10008.6.1.1044", "Container Component Types (8102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Pathology Specimen Types (8103)</summary>
        public static readonly DicomUID AnatomicPathologySpecimenTypes8103 = new DicomUID("1.2.840.10008.6.1.1045", "Anatomic Pathology Specimen Types (8103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Tissue Specimen Types (8104)</summary>
        public static readonly DicomUID BreastTissueSpecimenTypes8104 = new DicomUID("1.2.840.10008.6.1.1046", "Breast Tissue Specimen Types (8104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Collection Procedure (8109)</summary>
        public static readonly DicomUID SpecimenCollectionProcedure8109 = new DicomUID("1.2.840.10008.6.1.1047", "Specimen Collection Procedure (8109)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Sampling Procedure (8110)</summary>
        public static readonly DicomUID SpecimenSamplingProcedure8110 = new DicomUID("1.2.840.10008.6.1.1048", "Specimen Sampling Procedure (8110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Preparation Procedure (8111)</summary>
        public static readonly DicomUID SpecimenPreparationProcedure8111 = new DicomUID("1.2.840.10008.6.1.1049", "Specimen Preparation Procedure (8111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Stains (8112)</summary>
        public static readonly DicomUID SpecimenStains8112 = new DicomUID("1.2.840.10008.6.1.1050", "Specimen Stains (8112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Preparation Steps (8113)</summary>
        public static readonly DicomUID SpecimenPreparationSteps8113 = new DicomUID("1.2.840.10008.6.1.1051", "Specimen Preparation Steps (8113)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Fixatives (8114)</summary>
        public static readonly DicomUID SpecimenFixatives8114 = new DicomUID("1.2.840.10008.6.1.1052", "Specimen Fixatives (8114)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Embedding Media (8115)</summary>
        public static readonly DicomUID SpecimenEmbeddingMedia8115 = new DicomUID("1.2.840.10008.6.1.1053", "Specimen Embedding Media (8115)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source of Projection X-Ray Dose Information (10020)</summary>
        public static readonly DicomUID SourceOfProjectionXRayDoseInformation10020 = new DicomUID("1.2.840.10008.6.1.1054", "Source of Projection X-Ray Dose Information (10020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source of CT Dose Information (10021)</summary>
        public static readonly DicomUID SourceOfCTDoseInformation10021 = new DicomUID("1.2.840.10008.6.1.1055", "Source of CT Dose Information (10021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Reference Points (10025)</summary>
        public static readonly DicomUID RadiationDoseReferencePoints10025 = new DicomUID("1.2.840.10008.6.1.1056", "Radiation Dose Reference Points (10025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volumetric View Description (501)</summary>
        public static readonly DicomUID VolumetricViewDescription501 = new DicomUID("1.2.840.10008.6.1.1057", "Volumetric View Description (501)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volumetric View Modifier (502)</summary>
        public static readonly DicomUID VolumetricViewModifier502 = new DicomUID("1.2.840.10008.6.1.1058", "Volumetric View Modifier (502)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Acquisition Value Types (7260)</summary>
        public static readonly DicomUID DiffusionAcquisitionValueTypes7260 = new DicomUID("1.2.840.10008.6.1.1059", "Diffusion Acquisition Value Types (7260)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Model Value Types (7261)</summary>
        public static readonly DicomUID DiffusionModelValueTypes7261 = new DicomUID("1.2.840.10008.6.1.1060", "Diffusion Model Value Types (7261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Tractography Algorithm Families (7262)</summary>
        public static readonly DicomUID DiffusionTractographyAlgorithmFamilies7262 = new DicomUID("1.2.840.10008.6.1.1061", "Diffusion Tractography Algorithm Families (7262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Tractography Measurement Types (7263)</summary>
        public static readonly DicomUID DiffusionTractographyMeasurementTypes7263 = new DicomUID("1.2.840.10008.6.1.1062", "Diffusion Tractography Measurement Types (7263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Research Animal Source Registries (7490)</summary>
        public static readonly DicomUID ResearchAnimalSourceRegistries7490 = new DicomUID("1.2.840.10008.6.1.1063", "Research Animal Source Registries (7490)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Yes-No Only (231)</summary>
        public static readonly DicomUID YesNoOnly231 = new DicomUID("1.2.840.10008.6.1.1064", "Yes-No Only (231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Biosafety Levels (601)</summary>
        public static readonly DicomUID BiosafetyLevels601 = new DicomUID("1.2.840.10008.6.1.1065", "Biosafety Levels (601)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Biosafety Control Reasons (602)</summary>
        public static readonly DicomUID BiosafetyControlReasons602 = new DicomUID("1.2.840.10008.6.1.1066", "Biosafety Control Reasons (602)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sex - Male Female or Both (7457)</summary>
        public static readonly DicomUID SexMaleFemaleOrBoth7457 = new DicomUID("1.2.840.10008.6.1.1067", "Sex - Male Female or Both (7457)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Room Types (603)</summary>
        public static readonly DicomUID AnimalRoomTypes603 = new DicomUID("1.2.840.10008.6.1.1068", "Animal Room Types (603)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Reuse (604)</summary>
        public static readonly DicomUID DeviceReuse604 = new DicomUID("1.2.840.10008.6.1.1069", "Device Reuse (604)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Bedding Material (605)</summary>
        public static readonly DicomUID AnimalBeddingMaterial605 = new DicomUID("1.2.840.10008.6.1.1070", "Animal Bedding Material (605)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Shelter Types (606)</summary>
        public static readonly DicomUID AnimalShelterTypes606 = new DicomUID("1.2.840.10008.6.1.1071", "Animal Shelter Types (606)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Feed Types (607)</summary>
        public static readonly DicomUID AnimalFeedTypes607 = new DicomUID("1.2.840.10008.6.1.1072", "Animal Feed Types (607)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Feed Sources (608)</summary>
        public static readonly DicomUID AnimalFeedSources608 = new DicomUID("1.2.840.10008.6.1.1073", "Animal Feed Sources (608)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Feeding Methods (609)</summary>
        public static readonly DicomUID AnimalFeedingMethods609 = new DicomUID("1.2.840.10008.6.1.1074", "Animal Feeding Methods (609)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Water Types (610)</summary>
        public static readonly DicomUID WaterTypes610 = new DicomUID("1.2.840.10008.6.1.1075", "Water Types (610)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Category Code Type for Small Animal Anesthesia (611)</summary>
        public static readonly DicomUID AnesthesiaCategoryCodeTypeForSmallAnimalAnesthesia611 = new DicomUID("1.2.840.10008.6.1.1076", "Anesthesia Category Code Type for Small Animal Anesthesia (611)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Category Code Type from Anesthesia Quality Initiative (AQI) (612)</summary>
        public static readonly DicomUID AnesthesiaCategoryCodeTypeFromAnesthesiaQualityInitiativeAQI612 = new DicomUID("1.2.840.10008.6.1.1077", "Anesthesia Category Code Type from Anesthesia Quality Initiative (AQI) (612)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Induction Code Type for Small Animal Anesthesia (613)</summary>
        public static readonly DicomUID AnesthesiaInductionCodeTypeForSmallAnimalAnesthesia613 = new DicomUID("1.2.840.10008.6.1.1078", "Anesthesia Induction Code Type for Small Animal Anesthesia (613)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Induction Code Type from Anesthesia Quality Initiative (AQI) (614)</summary>
        public static readonly DicomUID AnesthesiaInductionCodeTypeFromAnesthesiaQualityInitiativeAQI614 = new DicomUID("1.2.840.10008.6.1.1079", "Anesthesia Induction Code Type from Anesthesia Quality Initiative (AQI) (614)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Maintenance Code Type for Small Animal Anesthesia (615)</summary>
        public static readonly DicomUID AnesthesiaMaintenanceCodeTypeForSmallAnimalAnesthesia615 = new DicomUID("1.2.840.10008.6.1.1080", "Anesthesia Maintenance Code Type for Small Animal Anesthesia (615)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Maintenance Code Type from Anesthesia Quality Initiative (AQI) (616)</summary>
        public static readonly DicomUID AnesthesiaMaintenanceCodeTypeFromAnesthesiaQualityInitiativeAQI616 = new DicomUID("1.2.840.10008.6.1.1081", "Anesthesia Maintenance Code Type from Anesthesia Quality Initiative (AQI) (616)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Method Code Type for Small Animal Anesthesia (617)</summary>
        public static readonly DicomUID AirwayManagementMethodCodeTypeForSmallAnimalAnesthesia617 = new DicomUID("1.2.840.10008.6.1.1082", "Airway Management Method Code Type for Small Animal Anesthesia (617)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Method Code Type from Anesthesia Quality Initiative (AQI) (618)</summary>
        public static readonly DicomUID AirwayManagementMethodCodeTypeFromAnesthesiaQualityInitiativeAQI618 = new DicomUID("1.2.840.10008.6.1.1083", "Airway Management Method Code Type from Anesthesia Quality Initiative (AQI) (618)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Sub-Method Code Type for Small Animal Anesthesia (619)</summary>
        public static readonly DicomUID AirwayManagementSubMethodCodeTypeForSmallAnimalAnesthesia619 = new DicomUID("1.2.840.10008.6.1.1084", "Airway Management Sub-Method Code Type for Small Animal Anesthesia (619)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Sub-Method Code Type from Anesthesia Quality Initiative (AQI) (620)</summary>
        public static readonly DicomUID AirwayManagementSubMethodCodeTypeFromAnesthesiaQualityInitiativeAQI620 = new DicomUID("1.2.840.10008.6.1.1085", "Airway Management Sub-Method Code Type from Anesthesia Quality Initiative (AQI) (620)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Type of Medication for Small Animal Anesthesia (621)</summary>
        public static readonly DicomUID TypeOfMedicationForSmallAnimalAnesthesia621 = new DicomUID("1.2.840.10008.6.1.1086", "Type of Medication for Small Animal Anesthesia (621)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication Type Code Type from Anesthesia Quality Initiative (AQI) (622)</summary>
        public static readonly DicomUID MedicationTypeCodeTypeFromAnesthesiaQualityInitiativeAQI622 = new DicomUID("1.2.840.10008.6.1.1087", "Medication Type Code Type from Anesthesia Quality Initiative (AQI) (622)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication for Small Animal Anesthesia (623)</summary>
        public static readonly DicomUID MedicationForSmallAnimalAnesthesia623 = new DicomUID("1.2.840.10008.6.1.1088", "Medication for Small Animal Anesthesia (623)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Inhalational Anesthesia Agents for Small Animal Anesthesia (624)</summary>
        public static readonly DicomUID InhalationalAnesthesiaAgentsForSmallAnimalAnesthesia624 = new DicomUID("1.2.840.10008.6.1.1089", "Inhalational Anesthesia Agents for Small Animal Anesthesia (624)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Injectable Anesthesia Agents for Small Animal Anesthesia (625)</summary>
        public static readonly DicomUID InjectableAnesthesiaAgentsForSmallAnimalAnesthesia625 = new DicomUID("1.2.840.10008.6.1.1090", "Injectable Anesthesia Agents for Small Animal Anesthesia (625)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Premedication Agents for Small Animal Anesthesia (626)</summary>
        public static readonly DicomUID PremedicationAgentsForSmallAnimalAnesthesia626 = new DicomUID("1.2.840.10008.6.1.1091", "Premedication Agents for Small Animal Anesthesia (626)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Neuromuscular Blocking Agents for Small Animal Anesthesia (627)</summary>
        public static readonly DicomUID NeuromuscularBlockingAgentsForSmallAnimalAnesthesia627 = new DicomUID("1.2.840.10008.6.1.1092", "Neuromuscular Blocking Agents for Small Animal Anesthesia (627)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ancillary Medications for Small Animal Anesthesia (628)</summary>
        public static readonly DicomUID AncillaryMedicationsForSmallAnimalAnesthesia628 = new DicomUID("1.2.840.10008.6.1.1093", "Ancillary Medications for Small Animal Anesthesia (628)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Carrier Gases for Small Animal Anesthesia (629)</summary>
        public static readonly DicomUID CarrierGasesForSmallAnimalAnesthesia629 = new DicomUID("1.2.840.10008.6.1.1094", "Carrier Gases for Small Animal Anesthesia (629)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Local Anesthetics for Small Animal Anesthesia (630)</summary>
        public static readonly DicomUID LocalAnestheticsForSmallAnimalAnesthesia630 = new DicomUID("1.2.840.10008.6.1.1095", "Local Anesthetics for Small Animal Anesthesia (630)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Phase of Procedure Requiring Anesthesia (631)</summary>
        public static readonly DicomUID PhaseOfProcedureRequiringAnesthesia631 = new DicomUID("1.2.840.10008.6.1.1096", "Phase of Procedure Requiring Anesthesia (631)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Phase of Surgical Procedure Requiring Anesthesia (632)</summary>
        public static readonly DicomUID PhaseOfSurgicalProcedureRequiringAnesthesia632 = new DicomUID("1.2.840.10008.6.1.1097", "Phase of Surgical Procedure Requiring Anesthesia (632)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Phase of Imaging Procedure Requiring Anesthesia (633)</summary>
        public static readonly DicomUID PhaseOfImagingProcedureRequiringAnesthesia633 = new DicomUID("1.2.840.10008.6.1.1098", "Phase of Imaging Procedure Requiring Anesthesia (633)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Phase of Animal Handling (634)</summary>
        public static readonly DicomUID PhaseOfAnimalHandling634 = new DicomUID("1.2.840.10008.6.1.1099", "Phase of Animal Handling (634)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Heating Method (635)</summary>
        public static readonly DicomUID HeatingMethod635 = new DicomUID("1.2.840.10008.6.1.1100", "Heating Method (635)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Temperature Sensor Device Component Type for Small Animal Procedures (636)</summary>
        public static readonly DicomUID TemperatureSensorDeviceComponentTypeForSmallAnimalProcedures636 = new DicomUID("1.2.840.10008.6.1.1101", "Temperature Sensor Device Component Type for Small Animal Procedures (636)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance Types (637)</summary>
        public static readonly DicomUID ExogenousSubstanceTypes637 = new DicomUID("1.2.840.10008.6.1.1102", "Exogenous Substance Types (637)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance (638)</summary>
        public static readonly DicomUID ExogenousSubstance638 = new DicomUID("1.2.840.10008.6.1.1103", "Exogenous Substance (638)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tumor Graft Histologic Type (639)</summary>
        public static readonly DicomUID TumorGraftHistologicType639 = new DicomUID("1.2.840.10008.6.1.1104", "Tumor Graft Histologic Type (639)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fibrils (640)</summary>
        public static readonly DicomUID Fibrils640 = new DicomUID("1.2.840.10008.6.1.1105", "Fibrils (640)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Viruses (641)</summary>
        public static readonly DicomUID Viruses641 = new DicomUID("1.2.840.10008.6.1.1106", "Viruses (641)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cytokines (642)</summary>
        public static readonly DicomUID Cytokines642 = new DicomUID("1.2.840.10008.6.1.1107", "Cytokines (642)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Toxins (643)</summary>
        public static readonly DicomUID Toxins643 = new DicomUID("1.2.840.10008.6.1.1108", "Toxins (643)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance Administration Sites (644)</summary>
        public static readonly DicomUID ExogenousSubstanceAdministrationSites644 = new DicomUID("1.2.840.10008.6.1.1109", "Exogenous Substance Administration Sites (644)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance Tissue of Origin (645)</summary>
        public static readonly DicomUID ExogenousSubstanceTissueOfOrigin645 = new DicomUID("1.2.840.10008.6.1.1110", "Exogenous Substance Tissue of Origin (645)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Preclinical Small Animal Imaging Procedures (646)</summary>
        public static readonly DicomUID PreclinicalSmallAnimalImagingProcedures646 = new DicomUID("1.2.840.10008.6.1.1111", "Preclinical Small Animal Imaging Procedures (646)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Position Reference Indicator for Frame of Reference (647)</summary>
        public static readonly DicomUID PositionReferenceIndicatorForFrameOfReference647 = new DicomUID("1.2.840.10008.6.1.1112", "Position Reference Indicator for Frame of Reference (647)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Present-Absent Only (241)</summary>
        public static readonly DicomUID PresentAbsentOnly241 = new DicomUID("1.2.840.10008.6.1.1113", "Present-Absent Only (241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Water Equivalent Diameter Method (10024)</summary>
        public static readonly DicomUID WaterEquivalentDiameterMethod10024 = new DicomUID("1.2.840.10008.6.1.1114", "Water Equivalent Diameter Method (10024)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Purposes of Reference (7022)</summary>
        public static readonly DicomUID RadiotherapyPurposesOfReference7022 = new DicomUID("1.2.840.10008.6.1.1115", "Radiotherapy Purposes of Reference (7022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Content Assessment Types (701)</summary>
        public static readonly DicomUID ContentAssessmentTypes701 = new DicomUID("1.2.840.10008.6.1.1116", "Content Assessment Types (701)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Content Assessment Types (702)</summary>
        public static readonly DicomUID RTContentAssessmentTypes702 = new DicomUID("1.2.840.10008.6.1.1117", "RT Content Assessment Types (702)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Basis of Assessment (703)</summary>
        public static readonly DicomUID BasisOfAssessment703 = new DicomUID("1.2.840.10008.6.1.1118", "Basis of Assessment (703)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reader Specialty (7449)</summary>
        public static readonly DicomUID ReaderSpecialty7449 = new DicomUID("1.2.840.10008.6.1.1119", "Reader Specialty (7449)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Requested Report Types (9233)</summary>
        public static readonly DicomUID RequestedReportTypes9233 = new DicomUID("1.2.840.10008.6.1.1120", "Requested Report Types (9233)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT Transverse Plane Reference Basis (1000)</summary>
        public static readonly DicomUID CTTransversePlaneReferenceBasis1000 = new DicomUID("1.2.840.10008.6.1.1121", "CT Transverse Plane Reference Basis (1000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Reference Basis (1001)</summary>
        public static readonly DicomUID AnatomicalReferenceBasis1001 = new DicomUID("1.2.840.10008.6.1.1122", "Anatomical Reference Basis (1001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Reference Basis - Head (1002)</summary>
        public static readonly DicomUID AnatomicalReferenceBasisHead1002 = new DicomUID("1.2.840.10008.6.1.1123", "Anatomical Reference Basis - Head (1002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Reference Basis - Spine (1003)</summary>
        public static readonly DicomUID AnatomicalReferenceBasisSpine1003 = new DicomUID("1.2.840.10008.6.1.1124", "Anatomical Reference Basis - Spine (1003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Reference Basis - Chest (1004)</summary>
        public static readonly DicomUID AnatomicalReferenceBasisChest1004 = new DicomUID("1.2.840.10008.6.1.1125", "Anatomical Reference Basis - Chest (1004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Reference Basis - Abdomen/Pelvis (1005)</summary>
        public static readonly DicomUID AnatomicalReferenceBasisAbdomenPelvis1005 = new DicomUID("1.2.840.10008.6.1.1126", "Anatomical Reference Basis - Abdomen/Pelvis (1005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Reference Basis - Extremities (1006)</summary>
        public static readonly DicomUID AnatomicalReferenceBasisExtremities1006 = new DicomUID("1.2.840.10008.6.1.1127", "Anatomical Reference Basis - Extremities (1006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reference Geometry - Planes (1010)</summary>
        public static readonly DicomUID ReferenceGeometryPlanes1010 = new DicomUID("1.2.840.10008.6.1.1128", "Reference Geometry - Planes (1010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reference Geometry - Points (1011)</summary>
        public static readonly DicomUID ReferenceGeometryPoints1011 = new DicomUID("1.2.840.10008.6.1.1129", "Reference Geometry - Points (1011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Alignment Methods (1015)</summary>
        public static readonly DicomUID PatientAlignmentMethods1015 = new DicomUID("1.2.840.10008.6.1.1130", "Patient Alignment Methods (1015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contraindications For CT Imaging (1200)</summary>
        public static readonly DicomUID ContraindicationsForCTImaging1200 = new DicomUID("1.2.840.10008.6.1.1131", "Contraindications For CT Imaging (1200)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiducials Categories (7110)</summary>
        public static readonly DicomUID FiducialsCategories7110 = new DicomUID("1.2.840.10008.6.1.1132", "Fiducials Categories (7110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiducials (7111)</summary>
        public static readonly DicomUID Fiducials7111 = new DicomUID("1.2.840.10008.6.1.1133", "Fiducials (7111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-Image Source Instance Purposes of Reference (7013)</summary>
        public static readonly DicomUID NonImageSourceInstancePurposesOfReference7013 = new DicomUID("1.2.840.10008.6.1.1134", "Non-Image Source Instance Purposes of Reference (7013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Process Output (7023)</summary>
        public static readonly DicomUID RTProcessOutput7023 = new DicomUID("1.2.840.10008.6.1.1135", "RT Process Output (7023)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Process Input (7024)</summary>
        public static readonly DicomUID RTProcessInput7024 = new DicomUID("1.2.840.10008.6.1.1136", "RT Process Input (7024)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Process Input Used (7025)</summary>
        public static readonly DicomUID RTProcessInputUsed7025 = new DicomUID("1.2.840.10008.6.1.1137", "RT Process Input Used (7025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy (6300)</summary>
        public static readonly DicomUID ProstateSectorAnatomy6300 = new DicomUID("1.2.840.10008.6.1.1138", "Prostate Sector Anatomy (6300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy from PI-RADS v2 (6301)</summary>
        public static readonly DicomUID ProstateSectorAnatomyFromPIRADSV26301 = new DicomUID("1.2.840.10008.6.1.1139", "Prostate Sector Anatomy from PI-RADS v2 (6301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy from European Concensus 16 Sector (Minimal) Model (6302)</summary>
        public static readonly DicomUID ProstateSectorAnatomyFromEuropeanConcensus16SectorMinimalModel6302 = new DicomUID("1.2.840.10008.6.1.1140", "Prostate Sector Anatomy from European Concensus 16 Sector (Minimal) Model (6302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy from European Concensus 27 Sector (Optimal) Model (6303)</summary>
        public static readonly DicomUID ProstateSectorAnatomyFromEuropeanConcensus27SectorOptimalModel6303 = new DicomUID("1.2.840.10008.6.1.1141", "Prostate Sector Anatomy from European Concensus 27 Sector (Optimal) Model (6303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Selection Reasons (12301)</summary>
        public static readonly DicomUID MeasurementSelectionReasons12301 = new DicomUID("1.2.840.10008.6.1.1142", "Measurement Selection Reasons (12301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Finding Observation Types (12302)</summary>
        public static readonly DicomUID EchoFindingObservationTypes12302 = new DicomUID("1.2.840.10008.6.1.1143", "Echo Finding Observation Types (12302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Measurement Types (12303)</summary>
        public static readonly DicomUID EchoMeasurementTypes12303 = new DicomUID("1.2.840.10008.6.1.1144", "Echo Measurement Types (12303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Measured Properties (12304)</summary>
        public static readonly DicomUID EchoMeasuredProperties12304 = new DicomUID("1.2.840.10008.6.1.1145", "Echo Measured Properties (12304)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Basic Echo Anatomic Sites (12305)</summary>
        public static readonly DicomUID BasicEchoAnatomicSites12305 = new DicomUID("1.2.840.10008.6.1.1146", "Basic Echo Anatomic Sites (12305)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Flow Directions (12306)</summary>
        public static readonly DicomUID EchoFlowDirections12306 = new DicomUID("1.2.840.10008.6.1.1147", "Echo Flow Directions (12306)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Phases and Time Points (12307)</summary>
        public static readonly DicomUID CardiacPhasesAndTimePoints12307 = new DicomUID("1.2.840.10008.6.1.1148", "Cardiac Phases and Time Points (12307)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Core Echo Measurements (12300)</summary>
        public static readonly DicomUID CoreEchoMeasurements12300 = new DicomUID("1.2.840.10008.6.1.1149", "Core Echo Measurements (12300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OCT-A Processing Algorithm Families (4270)</summary>
        public static readonly DicomUID OCTAProcessingAlgorithmFamilies4270 = new DicomUID("1.2.840.10008.6.1.1150", "OCT-A Processing Algorithm Families (4270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: En Face Image Types (4271)</summary>
        public static readonly DicomUID EnFaceImageTypes4271 = new DicomUID("1.2.840.10008.6.1.1151", "En Face Image Types (4271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Opt Scan Pattern Types (4272)</summary>
        public static readonly DicomUID OptScanPatternTypes4272 = new DicomUID("1.2.840.10008.6.1.1152", "Opt Scan Pattern Types (4272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Retinal Segmentation Surfaces (4273)</summary>
        public static readonly DicomUID RetinalSegmentationSurfaces4273 = new DicomUID("1.2.840.10008.6.1.1153", "Retinal Segmentation Surfaces (4273)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organs for Radiation Dose Estimates (10060)</summary>
        public static readonly DicomUID OrgansForRadiationDoseEstimates10060 = new DicomUID("1.2.840.10008.6.1.1154", "Organs for Radiation Dose Estimates (10060)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Absorbed Radiation Dose Types (10061)</summary>
        public static readonly DicomUID AbsorbedRadiationDoseTypes10061 = new DicomUID("1.2.840.10008.6.1.1155", "Absorbed Radiation Dose Types (10061)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equivalent Radiation Dose Types (10062)</summary>
        public static readonly DicomUID EquivalentRadiationDoseTypes10062 = new DicomUID("1.2.840.10008.6.1.1156", "Equivalent Radiation Dose Types (10062)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Estimate Distribution Representation (10063)</summary>
        public static readonly DicomUID RadiationDoseEstimateDistributionRepresentation10063 = new DicomUID("1.2.840.10008.6.1.1157", "Radiation Dose Estimate Distribution Representation (10063)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Model Type (10064)</summary>
        public static readonly DicomUID PatientModelType10064 = new DicomUID("1.2.840.10008.6.1.1158", "Patient Model Type (10064)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Transport Model Type (10065)</summary>
        public static readonly DicomUID RadiationTransportModelType10065 = new DicomUID("1.2.840.10008.6.1.1159", "Radiation Transport Model Type (10065)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuator Category (10066)</summary>
        public static readonly DicomUID AttenuatorCategory10066 = new DicomUID("1.2.840.10008.6.1.1160", "Attenuator Category (10066)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Attenuator Materials (10067)</summary>
        public static readonly DicomUID RadiationAttenuatorMaterials10067 = new DicomUID("1.2.840.10008.6.1.1161", "Radiation Attenuator Materials (10067)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Estimate Method Types (10068)</summary>
        public static readonly DicomUID EstimateMethodTypes10068 = new DicomUID("1.2.840.10008.6.1.1162", "Estimate Method Types (10068)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Estimation Parameter  (10069)</summary>
        public static readonly DicomUID RadiationDoseEstimationParameter10069 = new DicomUID("1.2.840.10008.6.1.1163", "Radiation Dose Estimation Parameter  (10069)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Types (10070)</summary>
        public static readonly DicomUID RadiationDoseTypes10070 = new DicomUID("1.2.840.10008.6.1.1164", "Radiation Dose Types (10070)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Component Semantics (7270)</summary>
        public static readonly DicomUID MRDiffusionComponentSemantics7270 = new DicomUID("1.2.840.10008.6.1.1165", "MR Diffusion Component Semantics (7270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Anisotropy Indices (7271)</summary>
        public static readonly DicomUID MRDiffusionAnisotropyIndices7271 = new DicomUID("1.2.840.10008.6.1.1166", "MR Diffusion Anisotropy Indices (7271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Parameters (7272)</summary>
        public static readonly DicomUID MRDiffusionModelParameters7272 = new DicomUID("1.2.840.10008.6.1.1167", "MR Diffusion Model Parameters (7272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Models (7273)</summary>
        public static readonly DicomUID MRDiffusionModels7273 = new DicomUID("1.2.840.10008.6.1.1168", "MR Diffusion Models (7273)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Fitting Methods (7274)</summary>
        public static readonly DicomUID MRDiffusionModelFittingMethods7274 = new DicomUID("1.2.840.10008.6.1.1169", "MR Diffusion Model Fitting Methods (7274)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Specific Methods (7275)</summary>
        public static readonly DicomUID MRDiffusionModelSpecificMethods7275 = new DicomUID("1.2.840.10008.6.1.1170", "MR Diffusion Model Specific Methods (7275)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Inputs (7276)</summary>
        public static readonly DicomUID MRDiffusionModelInputs7276 = new DicomUID("1.2.840.10008.6.1.1171", "MR Diffusion Model Inputs (7276)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Units of Diffusion Rate Area Over Time (7277)</summary>
        public static readonly DicomUID UnitsOfDiffusionRateAreaOverTime7277 = new DicomUID("1.2.840.10008.6.1.1172", "Units of Diffusion Rate Area Over Time (7277)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pediatric Size Categories (7039)</summary>
        public static readonly DicomUID PediatricSizeCategories7039 = new DicomUID("1.2.840.10008.6.1.1173", "Pediatric Size Categories (7039)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcium Scoring Patient Size Categories (7041)</summary>
        public static readonly DicomUID CalciumScoringPatientSizeCategories7041 = new DicomUID("1.2.840.10008.6.1.1174", "Calcium Scoring Patient Size Categories (7041)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reason for Repeating Acquisition (10034)</summary>
        public static readonly DicomUID ReasonForRepeatingAcquisition10034 = new DicomUID("1.2.840.10008.6.1.1175", "Reason for Repeating Acquisition (10034)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Protocol Assertion Codes (800)</summary>
        public static readonly DicomUID ProtocolAssertionCodes800 = new DicomUID("1.2.840.10008.6.1.1176", "Protocol Assertion Codes (800)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapeutic Dose Measurement Devices (7026)</summary>
        public static readonly DicomUID RadiotherapeuticDoseMeasurementDevices7026 = new DicomUID("1.2.840.10008.6.1.1177", "Radiotherapeutic Dose Measurement Devices (7026)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Export Additional Information Document Titles (7014)</summary>
        public static readonly DicomUID ExportAdditionalInformationDocumentTitles7014 = new DicomUID("1.2.840.10008.6.1.1178", "Export Additional Information Document Titles (7014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Export Delay Reasons (7015)</summary>
        public static readonly DicomUID ExportDelayReasons7015 = new DicomUID("1.2.840.10008.6.1.1179", "Export Delay Reasons (7015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Level of Difficulty (7016)</summary>
        public static readonly DicomUID LevelOfDifficulty7016 = new DicomUID("1.2.840.10008.6.1.1180", "Level of Difficulty (7016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Category of Teaching Material - Imaging (7017)</summary>
        public static readonly DicomUID CategoryOfTeachingMaterialImaging7017 = new DicomUID("1.2.840.10008.6.1.1181", "Category of Teaching Material - Imaging (7017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Miscellaneous Document Titles (7018)</summary>
        public static readonly DicomUID MiscellaneousDocumentTitles7018 = new DicomUID("1.2.840.10008.6.1.1182", "Miscellaneous Document Titles (7018)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmentation Non-Image Source Purposes of Reference (7019)</summary>
        public static readonly DicomUID SegmentationNonImageSourcePurposesOfReference7019 = new DicomUID("1.2.840.10008.6.1.1183", "Segmentation Non-Image Source Purposes of Reference (7019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Longitudinal Temporal Event Types (280)</summary>
        public static readonly DicomUID LongitudinalTemporalEventTypes280 = new DicomUID("1.2.840.10008.6.1.1184", "Longitudinal Temporal Event Types (280)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Object Type - Physical Objects (6401)</summary>
        public static readonly DicomUID NonLesionObjectTypePhysicalObjects6401 = new DicomUID("1.2.840.10008.6.1.1185", "Non-lesion Object Type - Physical Objects (6401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Object Type - Substances (6402)</summary>
        public static readonly DicomUID NonLesionObjectTypeSubstances6402 = new DicomUID("1.2.840.10008.6.1.1186", "Non-lesion Object Type - Substances (6402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Object Type - Tissues (6403)</summary>
        public static readonly DicomUID NonLesionObjectTypeTissues6403 = new DicomUID("1.2.840.10008.6.1.1187", "Non-lesion Object Type - Tissues (6403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Non-lesion Object Type - Physical Objects (6404)</summary>
        public static readonly DicomUID ChestNonLesionObjectTypePhysicalObjects6404 = new DicomUID("1.2.840.10008.6.1.1188", "Chest Non-lesion Object Type - Physical Objects (6404)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Non-lesion Object Type - Tissues (6405)</summary>
        public static readonly DicomUID ChestNonLesionObjectTypeTissues6405 = new DicomUID("1.2.840.10008.6.1.1189", "Chest Non-lesion Object Type - Tissues (6405)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tissue Segmentation Property Types (7191)</summary>
        public static readonly DicomUID TissueSegmentationPropertyTypes7191 = new DicomUID("1.2.840.10008.6.1.1190", "Tissue Segmentation Property Types (7191)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Structure Segmentation Property Types (7192)</summary>
        public static readonly DicomUID AnatomicalStructureSegmentationPropertyTypes7192 = new DicomUID("1.2.840.10008.6.1.1191", "Anatomical Structure Segmentation Property Types (7192)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physical Object Segmentation Property Types (7193)</summary>
        public static readonly DicomUID PhysicalObjectSegmentationPropertyTypes7193 = new DicomUID("1.2.840.10008.6.1.1192", "Physical Object Segmentation Property Types (7193)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Morphologically Abnormal Structure Segmentation Property Types (7194)</summary>
        public static readonly DicomUID MorphologicallyAbnormalStructureSegmentationPropertyTypes7194 = new DicomUID("1.2.840.10008.6.1.1193", "Morphologically Abnormal Structure Segmentation Property Types (7194)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Function Segmentation Property Types (7195)</summary>
        public static readonly DicomUID FunctionSegmentationPropertyTypes7195 = new DicomUID("1.2.840.10008.6.1.1194", "Function Segmentation Property Types (7195)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Spatial and Relational Concept Segmentation Property Types (7196)</summary>
        public static readonly DicomUID SpatialAndRelationalConceptSegmentationPropertyTypes7196 = new DicomUID("1.2.840.10008.6.1.1195", "Spatial and Relational Concept Segmentation Property Types (7196)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Body Substance Segmentation Property Types (7197)</summary>
        public static readonly DicomUID BodySubstanceSegmentationPropertyTypes7197 = new DicomUID("1.2.840.10008.6.1.1196", "Body Substance Segmentation Property Types (7197)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Substance Segmentation Property Types (7198)</summary>
        public static readonly DicomUID SubstanceSegmentationPropertyTypes7198 = new DicomUID("1.2.840.10008.6.1.1197", "Substance Segmentation Property Types (7198)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interpretation Request Discontinuation Reasons (9303)</summary>
        public static readonly DicomUID InterpretationRequestDiscontinuationReasons9303 = new DicomUID("1.2.840.10008.6.1.1198", "Interpretation Request Discontinuation Reasons (9303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gray Level Run Length Based Features (7475)</summary>
        public static readonly DicomUID GrayLevelRunLengthBasedFeatures7475 = new DicomUID("1.2.840.10008.6.1.1199", "Gray Level Run Length Based Features (7475)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gray Level Size Zone Based Features (7476)</summary>
        public static readonly DicomUID GrayLevelSizeZoneBasedFeatures7476 = new DicomUID("1.2.840.10008.6.1.1200", "Gray Level Size Zone Based Features (7476)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Encapsulated Document Source Purposes of Reference (7060)</summary>
        public static readonly DicomUID EncapsulatedDocumentSourcePurposesOfReference7060 = new DicomUID("1.2.840.10008.6.1.1201", "Encapsulated Document Source Purposes of Reference (7060)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model Document Titles (7061)</summary>
        public static readonly DicomUID ModelDocumentTitles7061 = new DicomUID("1.2.840.10008.6.1.1202", "Model Document Titles (7061)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference to Predecessor 3D Model (7062)</summary>
        public static readonly DicomUID PurposeOfReferenceToPredecessor3DModel7062 = new DicomUID("1.2.840.10008.6.1.1203", "Purpose of Reference to Predecessor 3D Model (7062)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model Scale Units (7063)</summary>
        public static readonly DicomUID ModelScaleUnits7063 = new DicomUID("1.2.840.10008.6.1.1204", "Model Scale Units (7063)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model Usage (7064)</summary>
        public static readonly DicomUID ModelUsage7064 = new DicomUID("1.2.840.10008.6.1.1205", "Model Usage (7064)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Units (10071)</summary>
        public static readonly DicomUID RadiationDoseUnits10071 = new DicomUID("1.2.840.10008.6.1.1206", "Radiation Dose Units (10071)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Fiducials (7112)</summary>
        public static readonly DicomUID RadiotherapyFiducials7112 = new DicomUID("1.2.840.10008.6.1.1207", "Radiotherapy Fiducials (7112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-energy Relevant Materials (300)</summary>
        public static readonly DicomUID MultiEnergyRelevantMaterials300 = new DicomUID("1.2.840.10008.6.1.1208", "Multi-energy Relevant Materials (300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-energy Material Units (301)</summary>
        public static readonly DicomUID MultiEnergyMaterialUnits301 = new DicomUID("1.2.840.10008.6.1.1209", "Multi-energy Material Units (301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dosimetric Objective Types (9500)</summary>
        public static readonly DicomUID DosimetricObjectiveTypes9500 = new DicomUID("1.2.840.10008.6.1.1210", "Dosimetric Objective Types (9500)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prescription Anatomy Categories (9501)</summary>
        public static readonly DicomUID PrescriptionAnatomyCategories9501 = new DicomUID("1.2.840.10008.6.1.1211", "Prescription Anatomy Categories (9501)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Segment Annotation Categories (9502)</summary>
        public static readonly DicomUID RTSegmentAnnotationCategories9502 = new DicomUID("1.2.840.10008.6.1.1212", "RT Segment Annotation Categories (9502)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Therapeutic Role Categories (9503)</summary>
        public static readonly DicomUID RadiotherapyTherapeuticRoleCategories9503 = new DicomUID("1.2.840.10008.6.1.1213", "Radiotherapy Therapeutic Role Categories (9503)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Geometric Information (9504)</summary>
        public static readonly DicomUID RTGeometricInformation9504 = new DicomUID("1.2.840.10008.6.1.1214", "RT Geometric Information (9504)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fixation or Positioning Devices (9505)</summary>
        public static readonly DicomUID FixationOrPositioningDevices9505 = new DicomUID("1.2.840.10008.6.1.1215", "Fixation or Positioning Devices (9505)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brachytherapy Devices (9506)</summary>
        public static readonly DicomUID BrachytherapyDevices9506 = new DicomUID("1.2.840.10008.6.1.1216", "Brachytherapy Devices (9506)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: External Body Models (9507)</summary>
        public static readonly DicomUID ExternalBodyModels9507 = new DicomUID("1.2.840.10008.6.1.1217", "External Body Models (9507)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-specific Volumes (9508)</summary>
        public static readonly DicomUID NonSpecificVolumes9508 = new DicomUID("1.2.840.10008.6.1.1218", "Non-specific Volumes (9508)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference For RT Physician Intent Input (9509)</summary>
        public static readonly DicomUID PurposeOfReferenceForRTPhysicianIntentInput9509 = new DicomUID("1.2.840.10008.6.1.1219", "Purpose of Reference For RT Physician Intent Input (9509)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference For RT Treatment Planning Input (9510)</summary>
        public static readonly DicomUID PurposeOfReferenceForRTTreatmentPlanningInput9510 = new DicomUID("1.2.840.10008.6.1.1220", "Purpose of Reference For RT Treatment Planning Input (9510)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General External Radiotherapy Procedure Techniques (9511)</summary>
        public static readonly DicomUID GeneralExternalRadiotherapyProcedureTechniques9511 = new DicomUID("1.2.840.10008.6.1.1221", "General External Radiotherapy Procedure Techniques (9511)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tomotherapeutic Radiotherapy Procedure Techniques (9512)</summary>
        public static readonly DicomUID TomotherapeuticRadiotherapyProcedureTechniques9512 = new DicomUID("1.2.840.10008.6.1.1222", "Tomotherapeutic Radiotherapy Procedure Techniques (9512)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fixation Devices (9513)</summary>
        public static readonly DicomUID FixationDevices9513 = new DicomUID("1.2.840.10008.6.1.1223", "Fixation Devices (9513)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Structures For Radiotherapy (9514)</summary>
        public static readonly DicomUID AnatomicalStructuresForRadiotherapy9514 = new DicomUID("1.2.840.10008.6.1.1224", "Anatomical Structures For Radiotherapy (9514)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Patient Support Devices (9515)</summary>
        public static readonly DicomUID RTPatientSupportDevices9515 = new DicomUID("1.2.840.10008.6.1.1225", "RT Patient Support Devices (9515)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Bolus Device Types (9516)</summary>
        public static readonly DicomUID RadiotherapyBolusDeviceTypes9516 = new DicomUID("1.2.840.10008.6.1.1226", "Radiotherapy Bolus Device Types (9516)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Block Device Types (9517)</summary>
        public static readonly DicomUID RadiotherapyBlockDeviceTypes9517 = new DicomUID("1.2.840.10008.6.1.1227", "Radiotherapy Block Device Types (9517)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Accessory No-slot Holder Device Types (9518)</summary>
        public static readonly DicomUID RadiotherapyAccessoryNoSlotHolderDeviceTypes9518 = new DicomUID("1.2.840.10008.6.1.1228", "Radiotherapy Accessory No-slot Holder Device Types (9518)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Accessory Slot Holder Device Types (9519)</summary>
        public static readonly DicomUID RadiotherapyAccessorySlotHolderDeviceTypes9519 = new DicomUID("1.2.840.10008.6.1.1229", "Radiotherapy Accessory Slot Holder Device Types (9519)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmented RT Accessory Devices (9520)</summary>
        public static readonly DicomUID SegmentedRTAccessoryDevices9520 = new DicomUID("1.2.840.10008.6.1.1230", "Segmented RT Accessory Devices (9520)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Energy Unit (9521)</summary>
        public static readonly DicomUID RadiotherapyTreatmentEnergyUnit9521 = new DicomUID("1.2.840.10008.6.1.1231", "Radiotherapy Treatment Energy Unit (9521)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-source Radiotherapy Procedure Techniques (9522)</summary>
        public static readonly DicomUID MultiSourceRadiotherapyProcedureTechniques9522 = new DicomUID("1.2.840.10008.6.1.1232", "Multi-source Radiotherapy Procedure Techniques (9522)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Robotic Radiotherapy Procedure Techniques (9523)</summary>
        public static readonly DicomUID RoboticRadiotherapyProcedureTechniques9523 = new DicomUID("1.2.840.10008.6.1.1233", "Robotic Radiotherapy Procedure Techniques (9523)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Procedure Techniques (9524)</summary>
        public static readonly DicomUID RadiotherapyProcedureTechniques9524 = new DicomUID("1.2.840.10008.6.1.1234", "Radiotherapy Procedure Techniques (9524)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Therapy Particle (9525)</summary>
        public static readonly DicomUID RadiationTherapyParticle9525 = new DicomUID("1.2.840.10008.6.1.1235", "Radiation Therapy Particle (9525)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ion Therapy Particle (9526)</summary>
        public static readonly DicomUID IonTherapyParticle9526 = new DicomUID("1.2.840.10008.6.1.1236", "Ion Therapy Particle (9526)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Teletherapy Isotope (9527)</summary>
        public static readonly DicomUID TeletherapyIsotope9527 = new DicomUID("1.2.840.10008.6.1.1237", "Teletherapy Isotope (9527)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brachytherapy Isotope (9528)</summary>
        public static readonly DicomUID BrachytherapyIsotope9528 = new DicomUID("1.2.840.10008.6.1.1238", "Brachytherapy Isotope (9528)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Single Dose Dosimetric Objectives (9529)</summary>
        public static readonly DicomUID SingleDoseDosimetricObjectives9529 = new DicomUID("1.2.840.10008.6.1.1239", "Single Dose Dosimetric Objectives (9529)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Percentage and Dose Dosimetric Objectives (9530)</summary>
        public static readonly DicomUID PercentageAndDoseDosimetricObjectives9530 = new DicomUID("1.2.840.10008.6.1.1240", "Percentage and Dose Dosimetric Objectives (9530)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume and Dose Dosimetric Objectives (9531)</summary>
        public static readonly DicomUID VolumeAndDoseDosimetricObjectives9531 = new DicomUID("1.2.840.10008.6.1.1241", "Volume and Dose Dosimetric Objectives (9531)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: No-Parameter Dosimetric Objectives (9532)</summary>
        public static readonly DicomUID NoParameterDosimetricObjectives9532 = new DicomUID("1.2.840.10008.6.1.1242", "No-Parameter Dosimetric Objectives (9532)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Delivery Time Structure (9533)</summary>
        public static readonly DicomUID DeliveryTimeStructure9533 = new DicomUID("1.2.840.10008.6.1.1243", "Delivery Time Structure (9533)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Targets (9534)</summary>
        public static readonly DicomUID RadiotherapyTargets9534 = new DicomUID("1.2.840.10008.6.1.1244", "Radiotherapy Targets (9534)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Dose Calculation Roles (9535)</summary>
        public static readonly DicomUID RadiotherapyDoseCalculationRoles9535 = new DicomUID("1.2.840.10008.6.1.1245", "Radiotherapy Dose Calculation Roles (9535)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Prescribing and Segmenting Person Roles (9536)</summary>
        public static readonly DicomUID RadiotherapyPrescribingAndSegmentingPersonRoles9536 = new DicomUID("1.2.840.10008.6.1.1246", "Radiotherapy Prescribing and Segmenting Person Roles (9536)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Effective Dose Calculation Method Categories (9537)</summary>
        public static readonly DicomUID EffectiveDoseCalculationMethodCategories9537 = new DicomUID("1.2.840.10008.6.1.1247", "Effective Dose Calculation Method Categories (9537)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Transport-based Effective Dose Method Modifiers (9538)</summary>
        public static readonly DicomUID RadiationTransportBasedEffectiveDoseMethodModifiers9538 = new DicomUID("1.2.840.10008.6.1.1248", "Radiation Transport-based Effective Dose Method Modifiers (9538)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fractionation-based Effective Dose Method Modifiers (9539)</summary>
        public static readonly DicomUID FractionationBasedEffectiveDoseMethodModifiers9539 = new DicomUID("1.2.840.10008.6.1.1249", "Fractionation-based Effective Dose Method Modifiers (9539)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Adverse Events (60)</summary>
        public static readonly DicomUID ImagingAgentAdministrationAdverseEvents60 = new DicomUID("1.2.840.10008.6.1.1250", "Imaging Agent Administration Adverse Events (60)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time Relative to Procedure (61)</summary>
        public static readonly DicomUID TimeRelativeToProcedure61 = new DicomUID("1.2.840.10008.6.1.1251", "Time Relative to Procedure (61)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Phase Type (62)</summary>
        public static readonly DicomUID ImagingAgentAdministrationPhaseType62 = new DicomUID("1.2.840.10008.6.1.1252", "Imaging Agent Administration Phase Type (62)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Mode (63)</summary>
        public static readonly DicomUID ImagingAgentAdministrationMode63 = new DicomUID("1.2.840.10008.6.1.1253", "Imaging Agent Administration Mode (63)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Patient State (64)</summary>
        public static readonly DicomUID ImagingAgentAdministrationPatientState64 = new DicomUID("1.2.840.10008.6.1.1254", "Imaging Agent Administration Patient State (64)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pre-medication For Imaging Agent Administration (65)</summary>
        public static readonly DicomUID PreMedicationForImagingAgentAdministration65 = new DicomUID("1.2.840.10008.6.1.1255", "Pre-medication For Imaging Agent Administration (65)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication For Imaging Agent Administration (66)</summary>
        public static readonly DicomUID MedicationForImagingAgentAdministration66 = new DicomUID("1.2.840.10008.6.1.1256", "Medication For Imaging Agent Administration (66)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Completion Status (67)</summary>
        public static readonly DicomUID ImagingAgentAdministrationCompletionStatus67 = new DicomUID("1.2.840.10008.6.1.1257", "Imaging Agent Administration Completion Status (67)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Pharmaceutical Unit of Presentation (68)</summary>
        public static readonly DicomUID ImagingAgentAdministrationPharmaceuticalUnitOfPresentation68 = new DicomUID("1.2.840.10008.6.1.1258", "Imaging Agent Administration Pharmaceutical Unit of Presentation (68)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Consumables (69)</summary>
        public static readonly DicomUID ImagingAgentAdministrationConsumables69 = new DicomUID("1.2.840.10008.6.1.1259", "Imaging Agent Administration Consumables (69)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Flush (70)</summary>
        public static readonly DicomUID Flush70 = new DicomUID("1.2.840.10008.6.1.1260", "Flush (70)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Injector Event Type (71)</summary>
        public static readonly DicomUID ImagingAgentAdministrationInjectorEventType71 = new DicomUID("1.2.840.10008.6.1.1261", "Imaging Agent Administration Injector Event Type (71)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Step Type (72)</summary>
        public static readonly DicomUID ImagingAgentAdministrationStepType72 = new DicomUID("1.2.840.10008.6.1.1262", "Imaging Agent Administration Step Type (72)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Bolus Shaping Curves (73)</summary>
        public static readonly DicomUID BolusShapingCurves73 = new DicomUID("1.2.840.10008.6.1.1263", "Bolus Shaping Curves (73)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Consumable Catheter Type (74)</summary>
        public static readonly DicomUID ImagingAgentAdministrationConsumableCatheterType74 = new DicomUID("1.2.840.10008.6.1.1264", "Imaging Agent Administration Consumable Catheter Type (74)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Low-high-equal (75)</summary>
        public static readonly DicomUID LowHighEqual75 = new DicomUID("1.2.840.10008.6.1.1265", "Low-high-equal (75)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Type of Pre-medication (76)</summary>
        public static readonly DicomUID TypeOfPreMedication76 = new DicomUID("1.2.840.10008.6.1.1266", "Type of Pre-medication (76)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Laterality with Median (245)</summary>
        public static readonly DicomUID LateralityWithMedian245 = new DicomUID("1.2.840.10008.6.1.1267", "Laterality with Median (245)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dermatology Anatomic Sites (4029)</summary>
        public static readonly DicomUID DermatologyAnatomicSites4029 = new DicomUID("1.2.840.10008.6.1.1268", "Dermatology Anatomic Sites (4029)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quantitative Image Features (218)</summary>
        public static readonly DicomUID QuantitativeImageFeatures218 = new DicomUID("1.2.840.10008.6.1.1269", "Quantitative Image Features (218)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Global Shape Descriptors (7477)</summary>
        public static readonly DicomUID GlobalShapeDescriptors7477 = new DicomUID("1.2.840.10008.6.1.1270", "Global Shape Descriptors (7477)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intensity Histogram Features (7478)</summary>
        public static readonly DicomUID IntensityHistogramFeatures7478 = new DicomUID("1.2.840.10008.6.1.1271", "Intensity Histogram Features (7478)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Grey Level Distance Zone Based Features (7479)</summary>
        public static readonly DicomUID GreyLevelDistanceZoneBasedFeatures7479 = new DicomUID("1.2.840.10008.6.1.1272", "Grey Level Distance Zone Based Features (7479)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Neighbourhood Grey Tone Difference Based Features (7500)</summary>
        public static readonly DicomUID NeighbourhoodGreyToneDifferenceBasedFeatures7500 = new DicomUID("1.2.840.10008.6.1.1273", "Neighbourhood Grey Tone Difference Based Features (7500)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Neighbouring Grey Level Dependence Based Features (7501)</summary>
        public static readonly DicomUID NeighbouringGreyLevelDependenceBasedFeatures7501 = new DicomUID("1.2.840.10008.6.1.1274", "Neighbouring Grey Level Dependence Based Features (7501)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cornea Measurement Method Descriptors (4242)</summary>
        public static readonly DicomUID CorneaMeasurementMethodDescriptors4242 = new DicomUID("1.2.840.10008.6.1.1275", "Cornea Measurement Method Descriptors (4242)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmented Radiotherapeutic Dose Measurement Devices (7027)</summary>
        public static readonly DicomUID SegmentedRadiotherapeuticDoseMeasurementDevices7027 = new DicomUID("1.2.840.10008.6.1.1276", "Segmented Radiotherapeutic Dose Measurement Devices (7027)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clinical Course of Disease (6098)</summary>
        public static readonly DicomUID ClinicalCourseOfDisease6098 = new DicomUID("1.2.840.10008.6.1.1277", "Clinical Course of Disease (6098)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Racial Group (6099)</summary>
        public static readonly DicomUID RacialGroup6099 = new DicomUID("1.2.840.10008.6.1.1278", "Racial Group (6099)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Laterality (246)</summary>
        public static readonly DicomUID RelativeLaterality246 = new DicomUID("1.2.840.10008.6.1.1279", "Relative Laterality (246)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Lesion Segmentation Types With Necrosis (7168)</summary>
        public static readonly DicomUID BrainLesionSegmentationTypesWithNecrosis7168 = new DicomUID("1.2.840.10008.6.1.1280", "Brain Lesion Segmentation Types With Necrosis (7168)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Lesion Segmentation Types Without Necrosis (7169)</summary>
        public static readonly DicomUID BrainLesionSegmentationTypesWithoutNecrosis7169 = new DicomUID("1.2.840.10008.6.1.1281", "Brain Lesion Segmentation Types Without Necrosis (7169)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-Acquisition Modality (32)</summary>
        public static readonly DicomUID NonAcquisitionModality32 = new DicomUID("1.2.840.10008.6.1.1282", "Non-Acquisition Modality (32)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Modality (33)</summary>
        public static readonly DicomUID Modality33 = new DicomUID("1.2.840.10008.6.1.1283", "Modality (33)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Laterality Left-Right Only (247)</summary>
        public static readonly DicomUID LateralityLeftRightOnly247 = new DicomUID("1.2.840.10008.6.1.1284", "Laterality Left-Right Only (247)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Qualitative Evaluation Modifier Types (210)</summary>
        public static readonly DicomUID QualitativeEvaluationModifierTypes210 = new DicomUID("1.2.840.10008.6.1.1285", "Qualitative Evaluation Modifier Types (210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Qualitative Evaluation Modifier Values (211)</summary>
        public static readonly DicomUID QualitativeEvaluationModifierValues211 = new DicomUID("1.2.840.10008.6.1.1286", "Qualitative Evaluation Modifier Values (211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Generic Anatomic Location Modifiers (212)</summary>
        public static readonly DicomUID GenericAnatomicLocationModifiers212 = new DicomUID("1.2.840.10008.6.1.1287", "Generic Anatomic Location Modifiers (212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Beam Limiting Device Types (9541)</summary>
        public static readonly DicomUID BeamLimitingDeviceTypes9541 = new DicomUID("1.2.840.10008.6.1.1288", "Beam Limiting Device Types (9541)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Compensator Device Types (9542)</summary>
        public static readonly DicomUID CompensatorDeviceTypes9542 = new DicomUID("1.2.840.10008.6.1.1289", "Compensator Device Types (9542)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Machine Modes (9543)</summary>
        public static readonly DicomUID RadiotherapyTreatmentMachineModes9543 = new DicomUID("1.2.840.10008.6.1.1290", "Radiotherapy Treatment Machine Modes (9543)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Distance Reference Locations (9544)</summary>
        public static readonly DicomUID RadiotherapyDistanceReferenceLocations9544 = new DicomUID("1.2.840.10008.6.1.1291", "Radiotherapy Distance Reference Locations (9544)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fixed Beam Limiting Device Types (9545)</summary>
        public static readonly DicomUID FixedBeamLimitingDeviceTypes9545 = new DicomUID("1.2.840.10008.6.1.1292", "Fixed Beam Limiting Device Types (9545)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Wedge Types (9546)</summary>
        public static readonly DicomUID RadiotherapyWedgeTypes9546 = new DicomUID("1.2.840.10008.6.1.1293", "Radiotherapy Wedge Types (9546)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Beam Limiting Device Orientation Labels (9547)</summary>
        public static readonly DicomUID RTBeamLimitingDeviceOrientationLabels9547 = new DicomUID("1.2.840.10008.6.1.1294", "RT Beam Limiting Device Orientation Labels (9547)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Accessory Device Types (9548)</summary>
        public static readonly DicomUID GeneralAccessoryDeviceTypes9548 = new DicomUID("1.2.840.10008.6.1.1295", "General Accessory Device Types (9548)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Generation Mode Types (9549)</summary>
        public static readonly DicomUID RadiationGenerationModeTypes9549 = new DicomUID("1.2.840.10008.6.1.1296", "Radiation Generation Mode Types (9549)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: C-Arm Photon-Electron Delivery Rate Units (9550)</summary>
        public static readonly DicomUID CArmPhotonElectronDeliveryRateUnits9550 = new DicomUID("1.2.840.10008.6.1.1297", "C-Arm Photon-Electron Delivery Rate Units (9550)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treatment Delivery Device Types (9551)</summary>
        public static readonly DicomUID TreatmentDeliveryDeviceTypes9551 = new DicomUID("1.2.840.10008.6.1.1298", "Treatment Delivery Device Types (9551)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: C-Arm Photon-Electron Dosimeter Units (9552)</summary>
        public static readonly DicomUID CArmPhotonElectronDosimeterUnits9552 = new DicomUID("1.2.840.10008.6.1.1299", "C-Arm Photon-Electron Dosimeter Units (9552)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treatment Points (9553)</summary>
        public static readonly DicomUID TreatmentPoints9553 = new DicomUID("1.2.840.10008.6.1.1300", "Treatment Points (9553)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equipment Reference Points (9554)</summary>
        public static readonly DicomUID EquipmentReferencePoints9554 = new DicomUID("1.2.840.10008.6.1.1301", "Equipment Reference Points (9554)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Planning Person Roles (9555)</summary>
        public static readonly DicomUID RadiotherapyTreatmentPlanningPersonRoles9555 = new DicomUID("1.2.840.10008.6.1.1302", "Radiotherapy Treatment Planning Person Roles (9555)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Real Time Video Rendition Titles (7070)</summary>
        public static readonly DicomUID RealTimeVideoRenditionTitles7070 = new DicomUID("1.2.840.10008.6.1.1303", "Real Time Video Rendition Titles (7070)", DicomUidType.ContextGroupName, false);

    }
}
