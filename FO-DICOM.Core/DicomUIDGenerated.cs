
// Copyright (c) 2012-2023 fo-dicom contributors.
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
            _uids.Add(DicomUID.EncapsulatedUncompressedExplicitVRLittleEndian.UID, DicomUID.EncapsulatedUncompressedExplicitVRLittleEndian);
            _uids.Add(DicomUID.DeflatedExplicitVRLittleEndian.UID, DicomUID.DeflatedExplicitVRLittleEndian);
            _uids.Add(DicomUID.ExplicitVRBigEndianRETIRED.UID, DicomUID.ExplicitVRBigEndianRETIRED);
            _uids.Add(DicomUID.JPEGBaseline8Bit.UID, DicomUID.JPEGBaseline8Bit);
            _uids.Add(DicomUID.JPEGExtended12Bit.UID, DicomUID.JPEGExtended12Bit);
            _uids.Add(DicomUID.JPEGExtended35RETIRED.UID, DicomUID.JPEGExtended35RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionNonHierarchical68RETIRED.UID, DicomUID.JPEGSpectralSelectionNonHierarchical68RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionNonHierarchical79RETIRED.UID, DicomUID.JPEGSpectralSelectionNonHierarchical79RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionNonHierarchical1012RETIRED.UID, DicomUID.JPEGFullProgressionNonHierarchical1012RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionNonHierarchical1113RETIRED.UID, DicomUID.JPEGFullProgressionNonHierarchical1113RETIRED);
            _uids.Add(DicomUID.JPEGLossless.UID, DicomUID.JPEGLossless);
            _uids.Add(DicomUID.JPEGLosslessNonHierarchical15RETIRED.UID, DicomUID.JPEGLosslessNonHierarchical15RETIRED);
            _uids.Add(DicomUID.JPEGExtendedHierarchical1618RETIRED.UID, DicomUID.JPEGExtendedHierarchical1618RETIRED);
            _uids.Add(DicomUID.JPEGExtendedHierarchical1719RETIRED.UID, DicomUID.JPEGExtendedHierarchical1719RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionHierarchical2022RETIRED.UID, DicomUID.JPEGSpectralSelectionHierarchical2022RETIRED);
            _uids.Add(DicomUID.JPEGSpectralSelectionHierarchical2123RETIRED.UID, DicomUID.JPEGSpectralSelectionHierarchical2123RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionHierarchical2426RETIRED.UID, DicomUID.JPEGFullProgressionHierarchical2426RETIRED);
            _uids.Add(DicomUID.JPEGFullProgressionHierarchical2527RETIRED.UID, DicomUID.JPEGFullProgressionHierarchical2527RETIRED);
            _uids.Add(DicomUID.JPEGLosslessHierarchical28RETIRED.UID, DicomUID.JPEGLosslessHierarchical28RETIRED);
            _uids.Add(DicomUID.JPEGLosslessHierarchical29RETIRED.UID, DicomUID.JPEGLosslessHierarchical29RETIRED);
            _uids.Add(DicomUID.JPEGLosslessSV1.UID, DicomUID.JPEGLosslessSV1);
            _uids.Add(DicomUID.JPEGLSLossless.UID, DicomUID.JPEGLSLossless);
            _uids.Add(DicomUID.JPEGLSNearLossless.UID, DicomUID.JPEGLSNearLossless);
            _uids.Add(DicomUID.JPEG2000Lossless.UID, DicomUID.JPEG2000Lossless);
            _uids.Add(DicomUID.JPEG2000.UID, DicomUID.JPEG2000);
            _uids.Add(DicomUID.JPEG2000MCLossless.UID, DicomUID.JPEG2000MCLossless);
            _uids.Add(DicomUID.JPEG2000MC.UID, DicomUID.JPEG2000MC);
            _uids.Add(DicomUID.JPIPReferenced.UID, DicomUID.JPIPReferenced);
            _uids.Add(DicomUID.JPIPReferencedDeflate.UID, DicomUID.JPIPReferencedDeflate);
            _uids.Add(DicomUID.MPEG2MPML.UID, DicomUID.MPEG2MPML);
            _uids.Add(DicomUID.MPEG2MPMLF.UID, DicomUID.MPEG2MPMLF);
            _uids.Add(DicomUID.MPEG2MPHL.UID, DicomUID.MPEG2MPHL);
            _uids.Add(DicomUID.MPEG2MPHLF.UID, DicomUID.MPEG2MPHLF);
            _uids.Add(DicomUID.MPEG4HP41.UID, DicomUID.MPEG4HP41);
            _uids.Add(DicomUID.MPEG4HP41F.UID, DicomUID.MPEG4HP41F);
            _uids.Add(DicomUID.MPEG4HP41BD.UID, DicomUID.MPEG4HP41BD);
            _uids.Add(DicomUID.MPEG4HP41BDF.UID, DicomUID.MPEG4HP41BDF);
            _uids.Add(DicomUID.MPEG4HP422D.UID, DicomUID.MPEG4HP422D);
            _uids.Add(DicomUID.MPEG4HP422DF.UID, DicomUID.MPEG4HP422DF);
            _uids.Add(DicomUID.MPEG4HP423D.UID, DicomUID.MPEG4HP423D);
            _uids.Add(DicomUID.MPEG4HP423DF.UID, DicomUID.MPEG4HP423DF);
            _uids.Add(DicomUID.MPEG4HP42STEREO.UID, DicomUID.MPEG4HP42STEREO);
            _uids.Add(DicomUID.MPEG4HP42STEREOF.UID, DicomUID.MPEG4HP42STEREOF);
            _uids.Add(DicomUID.HEVCMP51.UID, DicomUID.HEVCMP51);
            _uids.Add(DicomUID.HEVCM10P51.UID, DicomUID.HEVCM10P51);
            _uids.Add(DicomUID.HTJ2KLossless.UID, DicomUID.HTJ2KLossless);
            _uids.Add(DicomUID.HTJ2KLosslessRPCL.UID, DicomUID.HTJ2KLosslessRPCL);
            _uids.Add(DicomUID.HTJ2K.UID, DicomUID.HTJ2K);
            _uids.Add(DicomUID.JPIPHTJ2KReferenced.UID, DicomUID.JPIPHTJ2KReferenced);
            _uids.Add(DicomUID.JPIPHTJ2KReferencedDeflate.UID, DicomUID.JPIPHTJ2KReferencedDeflate);
            _uids.Add(DicomUID.RLELossless.UID, DicomUID.RLELossless);
            _uids.Add(DicomUID.RFC2557MIMEEncapsulationRETIRED.UID, DicomUID.RFC2557MIMEEncapsulationRETIRED);
            _uids.Add(DicomUID.XMLEncodingRETIRED.UID, DicomUID.XMLEncodingRETIRED);
            _uids.Add(DicomUID.SMPTEST211020UncompressedProgressiveActiveVideo.UID, DicomUID.SMPTEST211020UncompressedProgressiveActiveVideo);
            _uids.Add(DicomUID.SMPTEST211020UncompressedInterlacedActiveVideo.UID, DicomUID.SMPTEST211020UncompressedInterlacedActiveVideo);
            _uids.Add(DicomUID.SMPTEST211030PCMDigitalAudio.UID, DicomUID.SMPTEST211030PCMDigitalAudio);
            _uids.Add(DicomUID.MediaStorageDirectoryStorage.UID, DicomUID.MediaStorageDirectoryStorage);
            _uids.Add(DicomUID.HotIronPalette.UID, DicomUID.HotIronPalette);
            _uids.Add(DicomUID.PETPalette.UID, DicomUID.PETPalette);
            _uids.Add(DicomUID.HotMetalBluePalette.UID, DicomUID.HotMetalBluePalette);
            _uids.Add(DicomUID.PET20StepPalette.UID, DicomUID.PET20StepPalette);
            _uids.Add(DicomUID.SpringPalette.UID, DicomUID.SpringPalette);
            _uids.Add(DicomUID.SummerPalette.UID, DicomUID.SummerPalette);
            _uids.Add(DicomUID.FallPalette.UID, DicomUID.FallPalette);
            _uids.Add(DicomUID.WinterPalette.UID, DicomUID.WinterPalette);
            _uids.Add(DicomUID.BasicStudyContentNotificationRETIRED.UID, DicomUID.BasicStudyContentNotificationRETIRED);
            _uids.Add(DicomUID.Papyrus3ImplicitVRLittleEndianRETIRED.UID, DicomUID.Papyrus3ImplicitVRLittleEndianRETIRED);
            _uids.Add(DicomUID.StorageCommitmentPushModel.UID, DicomUID.StorageCommitmentPushModel);
            _uids.Add(DicomUID.StorageCommitmentPushModelInstance.UID, DicomUID.StorageCommitmentPushModelInstance);
            _uids.Add(DicomUID.StorageCommitmentPullModelRETIRED.UID, DicomUID.StorageCommitmentPullModelRETIRED);
            _uids.Add(DicomUID.StorageCommitmentPullModelInstanceRETIRED.UID, DicomUID.StorageCommitmentPullModelInstanceRETIRED);
            _uids.Add(DicomUID.ProceduralEventLogging.UID, DicomUID.ProceduralEventLogging);
            _uids.Add(DicomUID.ProceduralEventLoggingInstance.UID, DicomUID.ProceduralEventLoggingInstance);
            _uids.Add(DicomUID.SubstanceAdministrationLogging.UID, DicomUID.SubstanceAdministrationLogging);
            _uids.Add(DicomUID.SubstanceAdministrationLoggingInstance.UID, DicomUID.SubstanceAdministrationLoggingInstance);
            _uids.Add(DicomUID.DCMUID.UID, DicomUID.DCMUID);
            _uids.Add(DicomUID.DCM.UID, DicomUID.DCM);
            _uids.Add(DicomUID.MA.UID, DicomUID.MA);
            _uids.Add(DicomUID.UBERON.UID, DicomUID.UBERON);
            _uids.Add(DicomUID.ITIS_TSN.UID, DicomUID.ITIS_TSN);
            _uids.Add(DicomUID.MGI.UID, DicomUID.MGI);
            _uids.Add(DicomUID.PUBCHEM_CID.UID, DicomUID.PUBCHEM_CID);
            _uids.Add(DicomUID.DC.UID, DicomUID.DC);
            _uids.Add(DicomUID.NYUMCCG.UID, DicomUID.NYUMCCG);
            _uids.Add(DicomUID.MAYONRISBSASRG.UID, DicomUID.MAYONRISBSASRG);
            _uids.Add(DicomUID.IBSI.UID, DicomUID.IBSI);
            _uids.Add(DicomUID.RO.UID, DicomUID.RO);
            _uids.Add(DicomUID.RADELEMENT.UID, DicomUID.RADELEMENT);
            _uids.Add(DicomUID.I11.UID, DicomUID.I11);
            _uids.Add(DicomUID.UNS.UID, DicomUID.UNS);
            _uids.Add(DicomUID.RRID.UID, DicomUID.RRID);
            _uids.Add(DicomUID.DICOMApplicationContext.UID, DicomUID.DICOMApplicationContext);
            _uids.Add(DicomUID.DetachedPatientManagementRETIRED.UID, DicomUID.DetachedPatientManagementRETIRED);
            _uids.Add(DicomUID.DetachedPatientManagementMetaRETIRED.UID, DicomUID.DetachedPatientManagementMetaRETIRED);
            _uids.Add(DicomUID.DetachedVisitManagementRETIRED.UID, DicomUID.DetachedVisitManagementRETIRED);
            _uids.Add(DicomUID.DetachedStudyManagementRETIRED.UID, DicomUID.DetachedStudyManagementRETIRED);
            _uids.Add(DicomUID.StudyComponentManagementRETIRED.UID, DicomUID.StudyComponentManagementRETIRED);
            _uids.Add(DicomUID.ModalityPerformedProcedureStep.UID, DicomUID.ModalityPerformedProcedureStep);
            _uids.Add(DicomUID.ModalityPerformedProcedureStepRetrieve.UID, DicomUID.ModalityPerformedProcedureStepRetrieve);
            _uids.Add(DicomUID.ModalityPerformedProcedureStepNotification.UID, DicomUID.ModalityPerformedProcedureStepNotification);
            _uids.Add(DicomUID.DetachedResultsManagementRETIRED.UID, DicomUID.DetachedResultsManagementRETIRED);
            _uids.Add(DicomUID.DetachedResultsManagementMetaRETIRED.UID, DicomUID.DetachedResultsManagementMetaRETIRED);
            _uids.Add(DicomUID.DetachedStudyManagementMetaRETIRED.UID, DicomUID.DetachedStudyManagementMetaRETIRED);
            _uids.Add(DicomUID.DetachedInterpretationManagementRETIRED.UID, DicomUID.DetachedInterpretationManagementRETIRED);
            _uids.Add(DicomUID.Storage.UID, DicomUID.Storage);
            _uids.Add(DicomUID.BasicFilmSession.UID, DicomUID.BasicFilmSession);
            _uids.Add(DicomUID.BasicFilmBox.UID, DicomUID.BasicFilmBox);
            _uids.Add(DicomUID.BasicGrayscaleImageBox.UID, DicomUID.BasicGrayscaleImageBox);
            _uids.Add(DicomUID.BasicColorImageBox.UID, DicomUID.BasicColorImageBox);
            _uids.Add(DicomUID.ReferencedImageBoxRETIRED.UID, DicomUID.ReferencedImageBoxRETIRED);
            _uids.Add(DicomUID.BasicGrayscalePrintManagementMeta.UID, DicomUID.BasicGrayscalePrintManagementMeta);
            _uids.Add(DicomUID.ReferencedGrayscalePrintManagementMetaRETIRED.UID, DicomUID.ReferencedGrayscalePrintManagementMetaRETIRED);
            _uids.Add(DicomUID.PrintJob.UID, DicomUID.PrintJob);
            _uids.Add(DicomUID.BasicAnnotationBox.UID, DicomUID.BasicAnnotationBox);
            _uids.Add(DicomUID.Printer.UID, DicomUID.Printer);
            _uids.Add(DicomUID.PrinterConfigurationRetrieval.UID, DicomUID.PrinterConfigurationRetrieval);
            _uids.Add(DicomUID.PrinterInstance.UID, DicomUID.PrinterInstance);
            _uids.Add(DicomUID.PrinterConfigurationRetrievalInstance.UID, DicomUID.PrinterConfigurationRetrievalInstance);
            _uids.Add(DicomUID.BasicColorPrintManagementMeta.UID, DicomUID.BasicColorPrintManagementMeta);
            _uids.Add(DicomUID.ReferencedColorPrintManagementMetaRETIRED.UID, DicomUID.ReferencedColorPrintManagementMetaRETIRED);
            _uids.Add(DicomUID.VOILUTBox.UID, DicomUID.VOILUTBox);
            _uids.Add(DicomUID.PresentationLUT.UID, DicomUID.PresentationLUT);
            _uids.Add(DicomUID.ImageOverlayBoxRETIRED.UID, DicomUID.ImageOverlayBoxRETIRED);
            _uids.Add(DicomUID.BasicPrintImageOverlayBoxRETIRED.UID, DicomUID.BasicPrintImageOverlayBoxRETIRED);
            _uids.Add(DicomUID.PrintQueueInstanceRETIRED.UID, DicomUID.PrintQueueInstanceRETIRED);
            _uids.Add(DicomUID.PrintQueueManagementRETIRED.UID, DicomUID.PrintQueueManagementRETIRED);
            _uids.Add(DicomUID.StoredPrintStorageRETIRED.UID, DicomUID.StoredPrintStorageRETIRED);
            _uids.Add(DicomUID.HardcopyGrayscaleImageStorageRETIRED.UID, DicomUID.HardcopyGrayscaleImageStorageRETIRED);
            _uids.Add(DicomUID.HardcopyColorImageStorageRETIRED.UID, DicomUID.HardcopyColorImageStorageRETIRED);
            _uids.Add(DicomUID.PullPrintRequestRETIRED.UID, DicomUID.PullPrintRequestRETIRED);
            _uids.Add(DicomUID.PullStoredPrintManagementMetaRETIRED.UID, DicomUID.PullStoredPrintManagementMetaRETIRED);
            _uids.Add(DicomUID.MediaCreationManagement.UID, DicomUID.MediaCreationManagement);
            _uids.Add(DicomUID.DisplaySystem.UID, DicomUID.DisplaySystem);
            _uids.Add(DicomUID.DisplaySystemInstance.UID, DicomUID.DisplaySystemInstance);
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
            _uids.Add(DicomUID.UltrasoundMultiFrameImageStorageRetiredRETIRED.UID, DicomUID.UltrasoundMultiFrameImageStorageRetiredRETIRED);
            _uids.Add(DicomUID.UltrasoundMultiFrameImageStorage.UID, DicomUID.UltrasoundMultiFrameImageStorage);
            _uids.Add(DicomUID.MRImageStorage.UID, DicomUID.MRImageStorage);
            _uids.Add(DicomUID.EnhancedMRImageStorage.UID, DicomUID.EnhancedMRImageStorage);
            _uids.Add(DicomUID.MRSpectroscopyStorage.UID, DicomUID.MRSpectroscopyStorage);
            _uids.Add(DicomUID.EnhancedMRColorImageStorage.UID, DicomUID.EnhancedMRColorImageStorage);
            _uids.Add(DicomUID.LegacyConvertedEnhancedMRImageStorage.UID, DicomUID.LegacyConvertedEnhancedMRImageStorage);
            _uids.Add(DicomUID.NuclearMedicineImageStorageRetiredRETIRED.UID, DicomUID.NuclearMedicineImageStorageRetiredRETIRED);
            _uids.Add(DicomUID.UltrasoundImageStorageRetiredRETIRED.UID, DicomUID.UltrasoundImageStorageRetiredRETIRED);
            _uids.Add(DicomUID.UltrasoundImageStorage.UID, DicomUID.UltrasoundImageStorage);
            _uids.Add(DicomUID.EnhancedUSVolumeStorage.UID, DicomUID.EnhancedUSVolumeStorage);
            _uids.Add(DicomUID.PhotoacousticImageStorage.UID, DicomUID.PhotoacousticImageStorage);
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
            _uids.Add(DicomUID.General32bitECGWaveformStorage.UID, DicomUID.General32bitECGWaveformStorage);
            _uids.Add(DicomUID.HemodynamicWaveformStorage.UID, DicomUID.HemodynamicWaveformStorage);
            _uids.Add(DicomUID.CardiacElectrophysiologyWaveformStorage.UID, DicomUID.CardiacElectrophysiologyWaveformStorage);
            _uids.Add(DicomUID.BasicVoiceAudioWaveformStorage.UID, DicomUID.BasicVoiceAudioWaveformStorage);
            _uids.Add(DicomUID.GeneralAudioWaveformStorage.UID, DicomUID.GeneralAudioWaveformStorage);
            _uids.Add(DicomUID.ArterialPulseWaveformStorage.UID, DicomUID.ArterialPulseWaveformStorage);
            _uids.Add(DicomUID.RespiratoryWaveformStorage.UID, DicomUID.RespiratoryWaveformStorage);
            _uids.Add(DicomUID.MultichannelRespiratoryWaveformStorage.UID, DicomUID.MultichannelRespiratoryWaveformStorage);
            _uids.Add(DicomUID.RoutineScalpElectroencephalogramWaveformStorage.UID, DicomUID.RoutineScalpElectroencephalogramWaveformStorage);
            _uids.Add(DicomUID.ElectromyogramWaveformStorage.UID, DicomUID.ElectromyogramWaveformStorage);
            _uids.Add(DicomUID.ElectrooculogramWaveformStorage.UID, DicomUID.ElectrooculogramWaveformStorage);
            _uids.Add(DicomUID.SleepElectroencephalogramWaveformStorage.UID, DicomUID.SleepElectroencephalogramWaveformStorage);
            _uids.Add(DicomUID.BodyPositionWaveformStorage.UID, DicomUID.BodyPositionWaveformStorage);
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
            _uids.Add(DicomUID.VariableModalityLUTSoftcopyPresentationStateStorage.UID, DicomUID.VariableModalityLUTSoftcopyPresentationStateStorage);
            _uids.Add(DicomUID.XRayAngiographicImageStorage.UID, DicomUID.XRayAngiographicImageStorage);
            _uids.Add(DicomUID.EnhancedXAImageStorage.UID, DicomUID.EnhancedXAImageStorage);
            _uids.Add(DicomUID.XRayRadiofluoroscopicImageStorage.UID, DicomUID.XRayRadiofluoroscopicImageStorage);
            _uids.Add(DicomUID.EnhancedXRFImageStorage.UID, DicomUID.EnhancedXRFImageStorage);
            _uids.Add(DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED.UID, DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED);
            _uids.Add(DicomUID.XRay3DAngiographicImageStorage.UID, DicomUID.XRay3DAngiographicImageStorage);
            _uids.Add(DicomUID.XRay3DCraniofacialImageStorage.UID, DicomUID.XRay3DCraniofacialImageStorage);
            _uids.Add(DicomUID.BreastTomosynthesisImageStorage.UID, DicomUID.BreastTomosynthesisImageStorage);
            _uids.Add(DicomUID.BreastProjectionXRayImageStorageForPresentation.UID, DicomUID.BreastProjectionXRayImageStorageForPresentation);
            _uids.Add(DicomUID.BreastProjectionXRayImageStorageForProcessing.UID, DicomUID.BreastProjectionXRayImageStorageForProcessing);
            _uids.Add(DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForPresentation.UID, DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForPresentation);
            _uids.Add(DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForProcessing.UID, DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForProcessing);
            _uids.Add(DicomUID.NuclearMedicineImageStorage.UID, DicomUID.NuclearMedicineImageStorage);
            _uids.Add(DicomUID.ParametricMapStorage.UID, DicomUID.ParametricMapStorage);
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
            _uids.Add(DicomUID.OphthalmicOpticalCoherenceTomographyBscanVolumeAnalysisStorage.UID, DicomUID.OphthalmicOpticalCoherenceTomographyBscanVolumeAnalysisStorage);
            _uids.Add(DicomUID.VLWholeSlideMicroscopyImageStorage.UID, DicomUID.VLWholeSlideMicroscopyImageStorage);
            _uids.Add(DicomUID.DermoscopicPhotographyImageStorage.UID, DicomUID.DermoscopicPhotographyImageStorage);
            _uids.Add(DicomUID.ConfocalMicroscopyImageStorage.UID, DicomUID.ConfocalMicroscopyImageStorage);
            _uids.Add(DicomUID.ConfocalMicroscopyTiledPyramidalImageStorage.UID, DicomUID.ConfocalMicroscopyTiledPyramidalImageStorage);
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
            _uids.Add(DicomUID.EnhancedXRayRadiationDoseSRStorage.UID, DicomUID.EnhancedXRayRadiationDoseSRStorage);
            _uids.Add(DicomUID.WaveformAnnotationSRStorage.UID, DicomUID.WaveformAnnotationSRStorage);
            _uids.Add(DicomUID.ContentAssessmentResultsStorage.UID, DicomUID.ContentAssessmentResultsStorage);
            _uids.Add(DicomUID.MicroscopyBulkSimpleAnnotationsStorage.UID, DicomUID.MicroscopyBulkSimpleAnnotationsStorage);
            _uids.Add(DicomUID.EncapsulatedPDFStorage.UID, DicomUID.EncapsulatedPDFStorage);
            _uids.Add(DicomUID.EncapsulatedCDAStorage.UID, DicomUID.EncapsulatedCDAStorage);
            _uids.Add(DicomUID.EncapsulatedSTLStorage.UID, DicomUID.EncapsulatedSTLStorage);
            _uids.Add(DicomUID.EncapsulatedOBJStorage.UID, DicomUID.EncapsulatedOBJStorage);
            _uids.Add(DicomUID.EncapsulatedMTLStorage.UID, DicomUID.EncapsulatedMTLStorage);
            _uids.Add(DicomUID.PositronEmissionTomographyImageStorage.UID, DicomUID.PositronEmissionTomographyImageStorage);
            _uids.Add(DicomUID.LegacyConvertedEnhancedPETImageStorage.UID, DicomUID.LegacyConvertedEnhancedPETImageStorage);
            _uids.Add(DicomUID.StandalonePETCurveStorageRETIRED.UID, DicomUID.StandalonePETCurveStorageRETIRED);
            _uids.Add(DicomUID.EnhancedPETImageStorage.UID, DicomUID.EnhancedPETImageStorage);
            _uids.Add(DicomUID.BasicStructuredDisplayStorage.UID, DicomUID.BasicStructuredDisplayStorage);
            _uids.Add(DicomUID.CTDefinedProcedureProtocolStorage.UID, DicomUID.CTDefinedProcedureProtocolStorage);
            _uids.Add(DicomUID.CTPerformedProcedureProtocolStorage.UID, DicomUID.CTPerformedProcedureProtocolStorage);
            _uids.Add(DicomUID.ProtocolApprovalStorage.UID, DicomUID.ProtocolApprovalStorage);
            _uids.Add(DicomUID.ProtocolApprovalInformationModelFind.UID, DicomUID.ProtocolApprovalInformationModelFind);
            _uids.Add(DicomUID.ProtocolApprovalInformationModelMove.UID, DicomUID.ProtocolApprovalInformationModelMove);
            _uids.Add(DicomUID.ProtocolApprovalInformationModelGet.UID, DicomUID.ProtocolApprovalInformationModelGet);
            _uids.Add(DicomUID.XADefinedProcedureProtocolStorage.UID, DicomUID.XADefinedProcedureProtocolStorage);
            _uids.Add(DicomUID.XAPerformedProcedureProtocolStorage.UID, DicomUID.XAPerformedProcedureProtocolStorage);
            _uids.Add(DicomUID.InventoryStorage.UID, DicomUID.InventoryStorage);
            _uids.Add(DicomUID.InventoryFind.UID, DicomUID.InventoryFind);
            _uids.Add(DicomUID.InventoryMove.UID, DicomUID.InventoryMove);
            _uids.Add(DicomUID.InventoryGet.UID, DicomUID.InventoryGet);
            _uids.Add(DicomUID.InventoryCreation.UID, DicomUID.InventoryCreation);
            _uids.Add(DicomUID.RepositoryQuery.UID, DicomUID.RepositoryQuery);
            _uids.Add(DicomUID.StorageManagementInstance.UID, DicomUID.StorageManagementInstance);
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
            _uids.Add(DicomUID.TomotherapeuticRadiationStorage.UID, DicomUID.TomotherapeuticRadiationStorage);
            _uids.Add(DicomUID.RoboticArmRadiationStorage.UID, DicomUID.RoboticArmRadiationStorage);
            _uids.Add(DicomUID.RTRadiationRecordSetStorage.UID, DicomUID.RTRadiationRecordSetStorage);
            _uids.Add(DicomUID.RTRadiationSalvageRecordStorage.UID, DicomUID.RTRadiationSalvageRecordStorage);
            _uids.Add(DicomUID.TomotherapeuticRadiationRecordStorage.UID, DicomUID.TomotherapeuticRadiationRecordStorage);
            _uids.Add(DicomUID.CArmPhotonElectronRadiationRecordStorage.UID, DicomUID.CArmPhotonElectronRadiationRecordStorage);
            _uids.Add(DicomUID.RoboticRadiationRecordStorage.UID, DicomUID.RoboticRadiationRecordStorage);
            _uids.Add(DicomUID.RTRadiationSetDeliveryInstructionStorage.UID, DicomUID.RTRadiationSetDeliveryInstructionStorage);
            _uids.Add(DicomUID.RTTreatmentPreparationStorage.UID, DicomUID.RTTreatmentPreparationStorage);
            _uids.Add(DicomUID.EnhancedRTImageStorage.UID, DicomUID.EnhancedRTImageStorage);
            _uids.Add(DicomUID.EnhancedContinuousRTImageStorage.UID, DicomUID.EnhancedContinuousRTImageStorage);
            _uids.Add(DicomUID.RTPatientPositionAcquisitionInstructionStorage.UID, DicomUID.RTPatientPositionAcquisitionInstructionStorage);
            _uids.Add(DicomUID.DICOSCTImageStorage.UID, DicomUID.DICOSCTImageStorage);
            _uids.Add(DicomUID.DICOSDigitalXRayImageStorageForPresentation.UID, DicomUID.DICOSDigitalXRayImageStorageForPresentation);
            _uids.Add(DicomUID.DICOSDigitalXRayImageStorageForProcessing.UID, DicomUID.DICOSDigitalXRayImageStorageForProcessing);
            _uids.Add(DicomUID.DICOSThreatDetectionReportStorage.UID, DicomUID.DICOSThreatDetectionReportStorage);
            _uids.Add(DicomUID.DICOS2DAITStorage.UID, DicomUID.DICOS2DAITStorage);
            _uids.Add(DicomUID.DICOS3DAITStorage.UID, DicomUID.DICOS3DAITStorage);
            _uids.Add(DicomUID.DICOSQuadrupoleResonanceStorage.UID, DicomUID.DICOSQuadrupoleResonanceStorage);
            _uids.Add(DicomUID.EddyCurrentImageStorage.UID, DicomUID.EddyCurrentImageStorage);
            _uids.Add(DicomUID.EddyCurrentMultiFrameImageStorage.UID, DicomUID.EddyCurrentMultiFrameImageStorage);
            _uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelFind.UID, DicomUID.PatientRootQueryRetrieveInformationModelFind);
            _uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelMove.UID, DicomUID.PatientRootQueryRetrieveInformationModelMove);
            _uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelGet.UID, DicomUID.PatientRootQueryRetrieveInformationModelGet);
            _uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelFind.UID, DicomUID.StudyRootQueryRetrieveInformationModelFind);
            _uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelMove.UID, DicomUID.StudyRootQueryRetrieveInformationModelMove);
            _uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelGet.UID, DicomUID.StudyRootQueryRetrieveInformationModelGet);
            _uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelFindRETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelFindRETIRED);
            _uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelMoveRETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelMoveRETIRED);
            _uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelGetRETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelGetRETIRED);
            _uids.Add(DicomUID.CompositeInstanceRootRetrieveMove.UID, DicomUID.CompositeInstanceRootRetrieveMove);
            _uids.Add(DicomUID.CompositeInstanceRootRetrieveGet.UID, DicomUID.CompositeInstanceRootRetrieveGet);
            _uids.Add(DicomUID.CompositeInstanceRetrieveWithoutBulkDataGet.UID, DicomUID.CompositeInstanceRetrieveWithoutBulkDataGet);
            _uids.Add(DicomUID.DefinedProcedureProtocolInformationModelFind.UID, DicomUID.DefinedProcedureProtocolInformationModelFind);
            _uids.Add(DicomUID.DefinedProcedureProtocolInformationModelMove.UID, DicomUID.DefinedProcedureProtocolInformationModelMove);
            _uids.Add(DicomUID.DefinedProcedureProtocolInformationModelGet.UID, DicomUID.DefinedProcedureProtocolInformationModelGet);
            _uids.Add(DicomUID.ModalityWorklistInformationModelFind.UID, DicomUID.ModalityWorklistInformationModelFind);
            _uids.Add(DicomUID.GeneralPurposeWorklistManagementMetaRETIRED.UID, DicomUID.GeneralPurposeWorklistManagementMetaRETIRED);
            _uids.Add(DicomUID.GeneralPurposeWorklistInformationModelFindRETIRED.UID, DicomUID.GeneralPurposeWorklistInformationModelFindRETIRED);
            _uids.Add(DicomUID.GeneralPurposeScheduledProcedureStepRETIRED.UID, DicomUID.GeneralPurposeScheduledProcedureStepRETIRED);
            _uids.Add(DicomUID.GeneralPurposePerformedProcedureStepRETIRED.UID, DicomUID.GeneralPurposePerformedProcedureStepRETIRED);
            _uids.Add(DicomUID.InstanceAvailabilityNotification.UID, DicomUID.InstanceAvailabilityNotification);
            _uids.Add(DicomUID.RTBeamsDeliveryInstructionStorageTrialRETIRED.UID, DicomUID.RTBeamsDeliveryInstructionStorageTrialRETIRED);
            _uids.Add(DicomUID.RTConventionalMachineVerificationTrialRETIRED.UID, DicomUID.RTConventionalMachineVerificationTrialRETIRED);
            _uids.Add(DicomUID.RTIonMachineVerificationTrialRETIRED.UID, DicomUID.RTIonMachineVerificationTrialRETIRED);
            _uids.Add(DicomUID.UnifiedWorklistAndProcedureStepTrialRETIRED.UID, DicomUID.UnifiedWorklistAndProcedureStepTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepPushTrialRETIRED.UID, DicomUID.UnifiedProcedureStepPushTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepWatchTrialRETIRED.UID, DicomUID.UnifiedProcedureStepWatchTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepPullTrialRETIRED.UID, DicomUID.UnifiedProcedureStepPullTrialRETIRED);
            _uids.Add(DicomUID.UnifiedProcedureStepEventTrialRETIRED.UID, DicomUID.UnifiedProcedureStepEventTrialRETIRED);
            _uids.Add(DicomUID.UPSGlobalSubscriptionInstance.UID, DicomUID.UPSGlobalSubscriptionInstance);
            _uids.Add(DicomUID.UPSFilteredGlobalSubscriptionInstance.UID, DicomUID.UPSFilteredGlobalSubscriptionInstance);
            _uids.Add(DicomUID.UnifiedWorklistAndProcedureStep.UID, DicomUID.UnifiedWorklistAndProcedureStep);
            _uids.Add(DicomUID.UnifiedProcedureStepPush.UID, DicomUID.UnifiedProcedureStepPush);
            _uids.Add(DicomUID.UnifiedProcedureStepWatch.UID, DicomUID.UnifiedProcedureStepWatch);
            _uids.Add(DicomUID.UnifiedProcedureStepPull.UID, DicomUID.UnifiedProcedureStepPull);
            _uids.Add(DicomUID.UnifiedProcedureStepEvent.UID, DicomUID.UnifiedProcedureStepEvent);
            _uids.Add(DicomUID.UnifiedProcedureStepQuery.UID, DicomUID.UnifiedProcedureStepQuery);
            _uids.Add(DicomUID.RTBeamsDeliveryInstructionStorage.UID, DicomUID.RTBeamsDeliveryInstructionStorage);
            _uids.Add(DicomUID.RTConventionalMachineVerification.UID, DicomUID.RTConventionalMachineVerification);
            _uids.Add(DicomUID.RTIonMachineVerification.UID, DicomUID.RTIonMachineVerification);
            _uids.Add(DicomUID.RTBrachyApplicationSetupDeliveryInstructionStorage.UID, DicomUID.RTBrachyApplicationSetupDeliveryInstructionStorage);
            _uids.Add(DicomUID.GeneralRelevantPatientInformationQuery.UID, DicomUID.GeneralRelevantPatientInformationQuery);
            _uids.Add(DicomUID.BreastImagingRelevantPatientInformationQuery.UID, DicomUID.BreastImagingRelevantPatientInformationQuery);
            _uids.Add(DicomUID.CardiacRelevantPatientInformationQuery.UID, DicomUID.CardiacRelevantPatientInformationQuery);
            _uids.Add(DicomUID.HangingProtocolStorage.UID, DicomUID.HangingProtocolStorage);
            _uids.Add(DicomUID.HangingProtocolInformationModelFind.UID, DicomUID.HangingProtocolInformationModelFind);
            _uids.Add(DicomUID.HangingProtocolInformationModelMove.UID, DicomUID.HangingProtocolInformationModelMove);
            _uids.Add(DicomUID.HangingProtocolInformationModelGet.UID, DicomUID.HangingProtocolInformationModelGet);
            _uids.Add(DicomUID.ColorPaletteStorage.UID, DicomUID.ColorPaletteStorage);
            _uids.Add(DicomUID.ColorPaletteQueryRetrieveInformationModelFind.UID, DicomUID.ColorPaletteQueryRetrieveInformationModelFind);
            _uids.Add(DicomUID.ColorPaletteQueryRetrieveInformationModelMove.UID, DicomUID.ColorPaletteQueryRetrieveInformationModelMove);
            _uids.Add(DicomUID.ColorPaletteQueryRetrieveInformationModelGet.UID, DicomUID.ColorPaletteQueryRetrieveInformationModelGet);
            _uids.Add(DicomUID.ProductCharacteristicsQuery.UID, DicomUID.ProductCharacteristicsQuery);
            _uids.Add(DicomUID.SubstanceApprovalQuery.UID, DicomUID.SubstanceApprovalQuery);
            _uids.Add(DicomUID.GenericImplantTemplateStorage.UID, DicomUID.GenericImplantTemplateStorage);
            _uids.Add(DicomUID.GenericImplantTemplateInformationModelFind.UID, DicomUID.GenericImplantTemplateInformationModelFind);
            _uids.Add(DicomUID.GenericImplantTemplateInformationModelMove.UID, DicomUID.GenericImplantTemplateInformationModelMove);
            _uids.Add(DicomUID.GenericImplantTemplateInformationModelGet.UID, DicomUID.GenericImplantTemplateInformationModelGet);
            _uids.Add(DicomUID.ImplantAssemblyTemplateStorage.UID, DicomUID.ImplantAssemblyTemplateStorage);
            _uids.Add(DicomUID.ImplantAssemblyTemplateInformationModelFind.UID, DicomUID.ImplantAssemblyTemplateInformationModelFind);
            _uids.Add(DicomUID.ImplantAssemblyTemplateInformationModelMove.UID, DicomUID.ImplantAssemblyTemplateInformationModelMove);
            _uids.Add(DicomUID.ImplantAssemblyTemplateInformationModelGet.UID, DicomUID.ImplantAssemblyTemplateInformationModelGet);
            _uids.Add(DicomUID.ImplantTemplateGroupStorage.UID, DicomUID.ImplantTemplateGroupStorage);
            _uids.Add(DicomUID.ImplantTemplateGroupInformationModelFind.UID, DicomUID.ImplantTemplateGroupInformationModelFind);
            _uids.Add(DicomUID.ImplantTemplateGroupInformationModelMove.UID, DicomUID.ImplantTemplateGroupInformationModelMove);
            _uids.Add(DicomUID.ImplantTemplateGroupInformationModelGet.UID, DicomUID.ImplantTemplateGroupInformationModelGet);
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
            _uids.Add(DicomUID.UTC.UID, DicomUID.UTC);
            _uids.Add(DicomUID.AnatomicModifier2.UID, DicomUID.AnatomicModifier2);
            _uids.Add(DicomUID.AnatomicRegion4.UID, DicomUID.AnatomicRegion4);
            _uids.Add(DicomUID.TransducerApproach5.UID, DicomUID.TransducerApproach5);
            _uids.Add(DicomUID.TransducerOrientation6.UID, DicomUID.TransducerOrientation6);
            _uids.Add(DicomUID.UltrasoundBeamPath7.UID, DicomUID.UltrasoundBeamPath7);
            _uids.Add(DicomUID.AngiographicInterventionalDevice8.UID, DicomUID.AngiographicInterventionalDevice8);
            _uids.Add(DicomUID.ImageGuidedTherapeuticProcedure9.UID, DicomUID.ImageGuidedTherapeuticProcedure9);
            _uids.Add(DicomUID.InterventionalDrug10.UID, DicomUID.InterventionalDrug10);
            _uids.Add(DicomUID.AdministrationRoute11.UID, DicomUID.AdministrationRoute11);
            _uids.Add(DicomUID.ImagingContrastAgent12.UID, DicomUID.ImagingContrastAgent12);
            _uids.Add(DicomUID.ImagingContrastAgentIngredient13.UID, DicomUID.ImagingContrastAgentIngredient13);
            _uids.Add(DicomUID.RadiopharmaceuticalIsotope18.UID, DicomUID.RadiopharmaceuticalIsotope18);
            _uids.Add(DicomUID.PatientOrientation19.UID, DicomUID.PatientOrientation19);
            _uids.Add(DicomUID.PatientOrientationModifier20.UID, DicomUID.PatientOrientationModifier20);
            _uids.Add(DicomUID.PatientEquipmentRelationship21.UID, DicomUID.PatientEquipmentRelationship21);
            _uids.Add(DicomUID.CranioCaudadAngulation23.UID, DicomUID.CranioCaudadAngulation23);
            _uids.Add(DicomUID.Radiopharmaceutical25.UID, DicomUID.Radiopharmaceutical25);
            _uids.Add(DicomUID.NuclearMedicineProjection26.UID, DicomUID.NuclearMedicineProjection26);
            _uids.Add(DicomUID.AcquisitionModality29.UID, DicomUID.AcquisitionModality29);
            _uids.Add(DicomUID.DICOMDevice30.UID, DicomUID.DICOMDevice30);
            _uids.Add(DicomUID.AbstractPrior31.UID, DicomUID.AbstractPrior31);
            _uids.Add(DicomUID.NumericValueQualifier42.UID, DicomUID.NumericValueQualifier42);
            _uids.Add(DicomUID.MeasurementUnit82.UID, DicomUID.MeasurementUnit82);
            _uids.Add(DicomUID.RealWorldValueMappingUnit83.UID, DicomUID.RealWorldValueMappingUnit83);
            _uids.Add(DicomUID.SignificanceLevel220.UID, DicomUID.SignificanceLevel220);
            _uids.Add(DicomUID.MeasurementRangeConcept221.UID, DicomUID.MeasurementRangeConcept221);
            _uids.Add(DicomUID.Normality222.UID, DicomUID.Normality222);
            _uids.Add(DicomUID.NormalRangeValue223.UID, DicomUID.NormalRangeValue223);
            _uids.Add(DicomUID.SelectionMethod224.UID, DicomUID.SelectionMethod224);
            _uids.Add(DicomUID.MeasurementUncertaintyConcept225.UID, DicomUID.MeasurementUncertaintyConcept225);
            _uids.Add(DicomUID.PopulationStatisticalDescriptor226.UID, DicomUID.PopulationStatisticalDescriptor226);
            _uids.Add(DicomUID.SampleStatisticalDescriptor227.UID, DicomUID.SampleStatisticalDescriptor227);
            _uids.Add(DicomUID.EquationOrTable228.UID, DicomUID.EquationOrTable228);
            _uids.Add(DicomUID.YesNo230.UID, DicomUID.YesNo230);
            _uids.Add(DicomUID.PresentAbsent240.UID, DicomUID.PresentAbsent240);
            _uids.Add(DicomUID.NormalAbnormal242.UID, DicomUID.NormalAbnormal242);
            _uids.Add(DicomUID.Laterality244.UID, DicomUID.Laterality244);
            _uids.Add(DicomUID.PositiveNegative250.UID, DicomUID.PositiveNegative250);
            _uids.Add(DicomUID.ComplicationSeverity251.UID, DicomUID.ComplicationSeverity251);
            _uids.Add(DicomUID.ObserverType270.UID, DicomUID.ObserverType270);
            _uids.Add(DicomUID.ObservationSubjectClass271.UID, DicomUID.ObservationSubjectClass271);
            _uids.Add(DicomUID.AudioChannelSource3000.UID, DicomUID.AudioChannelSource3000);
            _uids.Add(DicomUID.ECGLead3001.UID, DicomUID.ECGLead3001);
            _uids.Add(DicomUID.HemodynamicWaveformSource3003.UID, DicomUID.HemodynamicWaveformSource3003);
            _uids.Add(DicomUID.CardiovascularAnatomicStructure3010.UID, DicomUID.CardiovascularAnatomicStructure3010);
            _uids.Add(DicomUID.ElectrophysiologyAnatomicLocation3011.UID, DicomUID.ElectrophysiologyAnatomicLocation3011);
            _uids.Add(DicomUID.CoronaryArterySegment3014.UID, DicomUID.CoronaryArterySegment3014);
            _uids.Add(DicomUID.CoronaryArtery3015.UID, DicomUID.CoronaryArtery3015);
            _uids.Add(DicomUID.CardiovascularAnatomicStructureModifier3019.UID, DicomUID.CardiovascularAnatomicStructureModifier3019);
            _uids.Add(DicomUID.CardiologyMeasurementUnit3082RETIRED.UID, DicomUID.CardiologyMeasurementUnit3082RETIRED);
            _uids.Add(DicomUID.TimeSynchronizationChannelType3090.UID, DicomUID.TimeSynchronizationChannelType3090);
            _uids.Add(DicomUID.CardiacProceduralStateValue3101.UID, DicomUID.CardiacProceduralStateValue3101);
            _uids.Add(DicomUID.ElectrophysiologyMeasurementFunctionTechnique3240.UID, DicomUID.ElectrophysiologyMeasurementFunctionTechnique3240);
            _uids.Add(DicomUID.HemodynamicMeasurementTechnique3241.UID, DicomUID.HemodynamicMeasurementTechnique3241);
            _uids.Add(DicomUID.CatheterizationProcedurePhase3250.UID, DicomUID.CatheterizationProcedurePhase3250);
            _uids.Add(DicomUID.ElectrophysiologyProcedurePhase3254.UID, DicomUID.ElectrophysiologyProcedurePhase3254);
            _uids.Add(DicomUID.StressProtocol3261.UID, DicomUID.StressProtocol3261);
            _uids.Add(DicomUID.ECGPatientStateValue3262.UID, DicomUID.ECGPatientStateValue3262);
            _uids.Add(DicomUID.ElectrodePlacementValue3263.UID, DicomUID.ElectrodePlacementValue3263);
            _uids.Add(DicomUID.XYZElectrodePlacementValues3264RETIRED.UID, DicomUID.XYZElectrodePlacementValues3264RETIRED);
            _uids.Add(DicomUID.HemodynamicPhysiologicalChallenge3271.UID, DicomUID.HemodynamicPhysiologicalChallenge3271);
            _uids.Add(DicomUID.ECGAnnotation3335.UID, DicomUID.ECGAnnotation3335);
            _uids.Add(DicomUID.HemodynamicAnnotation3337.UID, DicomUID.HemodynamicAnnotation3337);
            _uids.Add(DicomUID.ElectrophysiologyAnnotation3339.UID, DicomUID.ElectrophysiologyAnnotation3339);
            _uids.Add(DicomUID.ProcedureLogTitle3400.UID, DicomUID.ProcedureLogTitle3400);
            _uids.Add(DicomUID.LogNoteType3401.UID, DicomUID.LogNoteType3401);
            _uids.Add(DicomUID.PatientStatusAndEvent3402.UID, DicomUID.PatientStatusAndEvent3402);
            _uids.Add(DicomUID.PercutaneousEntry3403.UID, DicomUID.PercutaneousEntry3403);
            _uids.Add(DicomUID.StaffAction3404.UID, DicomUID.StaffAction3404);
            _uids.Add(DicomUID.ProcedureActionValue3405.UID, DicomUID.ProcedureActionValue3405);
            _uids.Add(DicomUID.NonCoronaryTranscatheterIntervention3406.UID, DicomUID.NonCoronaryTranscatheterIntervention3406);
            _uids.Add(DicomUID.ObjectReferencePurpose3407.UID, DicomUID.ObjectReferencePurpose3407);
            _uids.Add(DicomUID.ConsumableAction3408.UID, DicomUID.ConsumableAction3408);
            _uids.Add(DicomUID.DrugContrastAdministration3409.UID, DicomUID.DrugContrastAdministration3409);
            _uids.Add(DicomUID.DrugContrastNumericParameter3410.UID, DicomUID.DrugContrastNumericParameter3410);
            _uids.Add(DicomUID.IntracoronaryDevice3411.UID, DicomUID.IntracoronaryDevice3411);
            _uids.Add(DicomUID.InterventionActionStatus3412.UID, DicomUID.InterventionActionStatus3412);
            _uids.Add(DicomUID.AdverseOutcome3413.UID, DicomUID.AdverseOutcome3413);
            _uids.Add(DicomUID.ProcedureUrgency3414.UID, DicomUID.ProcedureUrgency3414);
            _uids.Add(DicomUID.CardiacRhythm3415.UID, DicomUID.CardiacRhythm3415);
            _uids.Add(DicomUID.RespirationRhythm3416.UID, DicomUID.RespirationRhythm3416);
            _uids.Add(DicomUID.LesionRisk3418.UID, DicomUID.LesionRisk3418);
            _uids.Add(DicomUID.FindingTitle3419.UID, DicomUID.FindingTitle3419);
            _uids.Add(DicomUID.ProcedureAction3421.UID, DicomUID.ProcedureAction3421);
            _uids.Add(DicomUID.DeviceUseAction3422.UID, DicomUID.DeviceUseAction3422);
            _uids.Add(DicomUID.NumericDeviceCharacteristic3423.UID, DicomUID.NumericDeviceCharacteristic3423);
            _uids.Add(DicomUID.InterventionParameter3425.UID, DicomUID.InterventionParameter3425);
            _uids.Add(DicomUID.ConsumablesParameter3426.UID, DicomUID.ConsumablesParameter3426);
            _uids.Add(DicomUID.EquipmentEvent3427.UID, DicomUID.EquipmentEvent3427);
            _uids.Add(DicomUID.CardiovascularImagingProcedure3428.UID, DicomUID.CardiovascularImagingProcedure3428);
            _uids.Add(DicomUID.CatheterizationDevice3429.UID, DicomUID.CatheterizationDevice3429);
            _uids.Add(DicomUID.DateTimeQualifier3430.UID, DicomUID.DateTimeQualifier3430);
            _uids.Add(DicomUID.PeripheralPulseLocation3440.UID, DicomUID.PeripheralPulseLocation3440);
            _uids.Add(DicomUID.PatientAssessment3441.UID, DicomUID.PatientAssessment3441);
            _uids.Add(DicomUID.PeripheralPulseMethod3442.UID, DicomUID.PeripheralPulseMethod3442);
            _uids.Add(DicomUID.SkinCondition3446.UID, DicomUID.SkinCondition3446);
            _uids.Add(DicomUID.AirwayAssessment3448.UID, DicomUID.AirwayAssessment3448);
            _uids.Add(DicomUID.CalibrationObject3451.UID, DicomUID.CalibrationObject3451);
            _uids.Add(DicomUID.CalibrationMethod3452.UID, DicomUID.CalibrationMethod3452);
            _uids.Add(DicomUID.CardiacVolumeMethod3453.UID, DicomUID.CardiacVolumeMethod3453);
            _uids.Add(DicomUID.IndexMethod3455.UID, DicomUID.IndexMethod3455);
            _uids.Add(DicomUID.SubSegmentMethod3456.UID, DicomUID.SubSegmentMethod3456);
            _uids.Add(DicomUID.ContourRealignment3458.UID, DicomUID.ContourRealignment3458);
            _uids.Add(DicomUID.CircumferentialExtent3460.UID, DicomUID.CircumferentialExtent3460);
            _uids.Add(DicomUID.RegionalExtent3461.UID, DicomUID.RegionalExtent3461);
            _uids.Add(DicomUID.ChamberIdentification3462.UID, DicomUID.ChamberIdentification3462);
            _uids.Add(DicomUID.QAReferenceMethod3465.UID, DicomUID.QAReferenceMethod3465);
            _uids.Add(DicomUID.PlaneIdentification3466.UID, DicomUID.PlaneIdentification3466);
            _uids.Add(DicomUID.EjectionFraction3467.UID, DicomUID.EjectionFraction3467);
            _uids.Add(DicomUID.EDVolume3468.UID, DicomUID.EDVolume3468);
            _uids.Add(DicomUID.ESVolume3469.UID, DicomUID.ESVolume3469);
            _uids.Add(DicomUID.VesselLumenCrossSectionalAreaCalculationMethod3470.UID, DicomUID.VesselLumenCrossSectionalAreaCalculationMethod3470);
            _uids.Add(DicomUID.EstimatedVolume3471.UID, DicomUID.EstimatedVolume3471);
            _uids.Add(DicomUID.CardiacContractionPhase3472.UID, DicomUID.CardiacContractionPhase3472);
            _uids.Add(DicomUID.IVUSProcedurePhase3480.UID, DicomUID.IVUSProcedurePhase3480);
            _uids.Add(DicomUID.IVUSDistanceMeasurement3481.UID, DicomUID.IVUSDistanceMeasurement3481);
            _uids.Add(DicomUID.IVUSAreaMeasurement3482.UID, DicomUID.IVUSAreaMeasurement3482);
            _uids.Add(DicomUID.IVUSLongitudinalMeasurement3483.UID, DicomUID.IVUSLongitudinalMeasurement3483);
            _uids.Add(DicomUID.IVUSIndexRatio3484.UID, DicomUID.IVUSIndexRatio3484);
            _uids.Add(DicomUID.IVUSVolumeMeasurement3485.UID, DicomUID.IVUSVolumeMeasurement3485);
            _uids.Add(DicomUID.VascularMeasurementSite3486.UID, DicomUID.VascularMeasurementSite3486);
            _uids.Add(DicomUID.IntravascularVolumetricRegion3487.UID, DicomUID.IntravascularVolumetricRegion3487);
            _uids.Add(DicomUID.MinMaxMean3488.UID, DicomUID.MinMaxMean3488);
            _uids.Add(DicomUID.CalciumDistribution3489.UID, DicomUID.CalciumDistribution3489);
            _uids.Add(DicomUID.IVUSLesionMorphology3491.UID, DicomUID.IVUSLesionMorphology3491);
            _uids.Add(DicomUID.VascularDissectionClassification3492.UID, DicomUID.VascularDissectionClassification3492);
            _uids.Add(DicomUID.IVUSRelativeStenosisSeverity3493.UID, DicomUID.IVUSRelativeStenosisSeverity3493);
            _uids.Add(DicomUID.IVUSNonMorphologicalFinding3494.UID, DicomUID.IVUSNonMorphologicalFinding3494);
            _uids.Add(DicomUID.IVUSPlaqueComposition3495.UID, DicomUID.IVUSPlaqueComposition3495);
            _uids.Add(DicomUID.IVUSFiducialPoint3496.UID, DicomUID.IVUSFiducialPoint3496);
            _uids.Add(DicomUID.IVUSArterialMorphology3497.UID, DicomUID.IVUSArterialMorphology3497);
            _uids.Add(DicomUID.PressureUnit3500.UID, DicomUID.PressureUnit3500);
            _uids.Add(DicomUID.HemodynamicResistanceUnit3502.UID, DicomUID.HemodynamicResistanceUnit3502);
            _uids.Add(DicomUID.IndexedHemodynamicResistanceUnit3503.UID, DicomUID.IndexedHemodynamicResistanceUnit3503);
            _uids.Add(DicomUID.CatheterSizeUnit3510.UID, DicomUID.CatheterSizeUnit3510);
            _uids.Add(DicomUID.SpecimenCollection3515.UID, DicomUID.SpecimenCollection3515);
            _uids.Add(DicomUID.BloodSourceType3520.UID, DicomUID.BloodSourceType3520);
            _uids.Add(DicomUID.BloodGasPressure3524.UID, DicomUID.BloodGasPressure3524);
            _uids.Add(DicomUID.BloodGasContent3525.UID, DicomUID.BloodGasContent3525);
            _uids.Add(DicomUID.BloodGasSaturation3526.UID, DicomUID.BloodGasSaturation3526);
            _uids.Add(DicomUID.BloodBaseExcess3527.UID, DicomUID.BloodBaseExcess3527);
            _uids.Add(DicomUID.BloodPH3528.UID, DicomUID.BloodPH3528);
            _uids.Add(DicomUID.ArterialVenousContent3529.UID, DicomUID.ArterialVenousContent3529);
            _uids.Add(DicomUID.OxygenAdministrationAction3530.UID, DicomUID.OxygenAdministrationAction3530);
            _uids.Add(DicomUID.OxygenAdministration3531.UID, DicomUID.OxygenAdministration3531);
            _uids.Add(DicomUID.CirculatorySupportAction3550.UID, DicomUID.CirculatorySupportAction3550);
            _uids.Add(DicomUID.VentilationAction3551.UID, DicomUID.VentilationAction3551);
            _uids.Add(DicomUID.PacingAction3552.UID, DicomUID.PacingAction3552);
            _uids.Add(DicomUID.CirculatorySupport3553.UID, DicomUID.CirculatorySupport3553);
            _uids.Add(DicomUID.Ventilation3554.UID, DicomUID.Ventilation3554);
            _uids.Add(DicomUID.Pacing3555.UID, DicomUID.Pacing3555);
            _uids.Add(DicomUID.BloodPressureMethod3560.UID, DicomUID.BloodPressureMethod3560);
            _uids.Add(DicomUID.RelativeTime3600.UID, DicomUID.RelativeTime3600);
            _uids.Add(DicomUID.HemodynamicPatientState3602.UID, DicomUID.HemodynamicPatientState3602);
            _uids.Add(DicomUID.ArterialLesionLocation3604.UID, DicomUID.ArterialLesionLocation3604);
            _uids.Add(DicomUID.ArterialSourceLocation3606.UID, DicomUID.ArterialSourceLocation3606);
            _uids.Add(DicomUID.VenousSourceLocation3607.UID, DicomUID.VenousSourceLocation3607);
            _uids.Add(DicomUID.AtrialSourceLocation3608.UID, DicomUID.AtrialSourceLocation3608);
            _uids.Add(DicomUID.VentricularSourceLocation3609.UID, DicomUID.VentricularSourceLocation3609);
            _uids.Add(DicomUID.GradientSourceLocation3610.UID, DicomUID.GradientSourceLocation3610);
            _uids.Add(DicomUID.PressureMeasurement3611.UID, DicomUID.PressureMeasurement3611);
            _uids.Add(DicomUID.BloodVelocityMeasurement3612.UID, DicomUID.BloodVelocityMeasurement3612);
            _uids.Add(DicomUID.HemodynamicTimeMeasurement3613.UID, DicomUID.HemodynamicTimeMeasurement3613);
            _uids.Add(DicomUID.NonMitralValveArea3614.UID, DicomUID.NonMitralValveArea3614);
            _uids.Add(DicomUID.ValveArea3615.UID, DicomUID.ValveArea3615);
            _uids.Add(DicomUID.HemodynamicPeriodMeasurement3616.UID, DicomUID.HemodynamicPeriodMeasurement3616);
            _uids.Add(DicomUID.ValveFlow3617.UID, DicomUID.ValveFlow3617);
            _uids.Add(DicomUID.HemodynamicFlow3618.UID, DicomUID.HemodynamicFlow3618);
            _uids.Add(DicomUID.HemodynamicResistanceMeasurement3619.UID, DicomUID.HemodynamicResistanceMeasurement3619);
            _uids.Add(DicomUID.HemodynamicRatio3620.UID, DicomUID.HemodynamicRatio3620);
            _uids.Add(DicomUID.FractionalFlowReserve3621.UID, DicomUID.FractionalFlowReserve3621);
            _uids.Add(DicomUID.MeasurementType3627.UID, DicomUID.MeasurementType3627);
            _uids.Add(DicomUID.CardiacOutputMethod3628.UID, DicomUID.CardiacOutputMethod3628);
            _uids.Add(DicomUID.ProcedureIntent3629.UID, DicomUID.ProcedureIntent3629);
            _uids.Add(DicomUID.CardiovascularAnatomicLocation3630.UID, DicomUID.CardiovascularAnatomicLocation3630);
            _uids.Add(DicomUID.Hypertension3640.UID, DicomUID.Hypertension3640);
            _uids.Add(DicomUID.HemodynamicAssessment3641.UID, DicomUID.HemodynamicAssessment3641);
            _uids.Add(DicomUID.DegreeFinding3642.UID, DicomUID.DegreeFinding3642);
            _uids.Add(DicomUID.HemodynamicMeasurementPhase3651.UID, DicomUID.HemodynamicMeasurementPhase3651);
            _uids.Add(DicomUID.BodySurfaceAreaEquation3663.UID, DicomUID.BodySurfaceAreaEquation3663);
            _uids.Add(DicomUID.OxygenConsumptionEquationTable3664.UID, DicomUID.OxygenConsumptionEquationTable3664);
            _uids.Add(DicomUID.P50Equation3666.UID, DicomUID.P50Equation3666);
            _uids.Add(DicomUID.FraminghamScore3667.UID, DicomUID.FraminghamScore3667);
            _uids.Add(DicomUID.FraminghamTable3668.UID, DicomUID.FraminghamTable3668);
            _uids.Add(DicomUID.ECGProcedureType3670.UID, DicomUID.ECGProcedureType3670);
            _uids.Add(DicomUID.ReasonForECGStudy3671.UID, DicomUID.ReasonForECGStudy3671);
            _uids.Add(DicomUID.Pacemaker3672.UID, DicomUID.Pacemaker3672);
            _uids.Add(DicomUID.Diagnosis3673RETIRED.UID, DicomUID.Diagnosis3673RETIRED);
            _uids.Add(DicomUID.OtherFilters3675RETIRED.UID, DicomUID.OtherFilters3675RETIRED);
            _uids.Add(DicomUID.LeadMeasurementTechnique3676.UID, DicomUID.LeadMeasurementTechnique3676);
            _uids.Add(DicomUID.SummaryCodesECG3677.UID, DicomUID.SummaryCodesECG3677);
            _uids.Add(DicomUID.QTCorrectionAlgorithm3678.UID, DicomUID.QTCorrectionAlgorithm3678);
            _uids.Add(DicomUID.ECGMorphologyDescription3679RETIRED.UID, DicomUID.ECGMorphologyDescription3679RETIRED);
            _uids.Add(DicomUID.ECGLeadNoiseDescription3680.UID, DicomUID.ECGLeadNoiseDescription3680);
            _uids.Add(DicomUID.ECGLeadNoiseModifier3681RETIRED.UID, DicomUID.ECGLeadNoiseModifier3681RETIRED);
            _uids.Add(DicomUID.Probability3682RETIRED.UID, DicomUID.Probability3682RETIRED);
            _uids.Add(DicomUID.Modifier3683RETIRED.UID, DicomUID.Modifier3683RETIRED);
            _uids.Add(DicomUID.Trend3684RETIRED.UID, DicomUID.Trend3684RETIRED);
            _uids.Add(DicomUID.ConjunctiveTerm3685RETIRED.UID, DicomUID.ConjunctiveTerm3685RETIRED);
            _uids.Add(DicomUID.ECGInterpretiveStatement3686RETIRED.UID, DicomUID.ECGInterpretiveStatement3686RETIRED);
            _uids.Add(DicomUID.ElectrophysiologyWaveformDuration3687.UID, DicomUID.ElectrophysiologyWaveformDuration3687);
            _uids.Add(DicomUID.ElectrophysiologyWaveformVoltage3688.UID, DicomUID.ElectrophysiologyWaveformVoltage3688);
            _uids.Add(DicomUID.CathDiagnosis3700.UID, DicomUID.CathDiagnosis3700);
            _uids.Add(DicomUID.CardiacValveTract3701.UID, DicomUID.CardiacValveTract3701);
            _uids.Add(DicomUID.WallMotion3703.UID, DicomUID.WallMotion3703);
            _uids.Add(DicomUID.MyocardiumWallMorphologyFinding3704.UID, DicomUID.MyocardiumWallMorphologyFinding3704);
            _uids.Add(DicomUID.ChamberSize3705.UID, DicomUID.ChamberSize3705);
            _uids.Add(DicomUID.OverallContractility3706.UID, DicomUID.OverallContractility3706);
            _uids.Add(DicomUID.VSDDescription3707.UID, DicomUID.VSDDescription3707);
            _uids.Add(DicomUID.AorticRootDescription3709.UID, DicomUID.AorticRootDescription3709);
            _uids.Add(DicomUID.CoronaryDominance3710.UID, DicomUID.CoronaryDominance3710);
            _uids.Add(DicomUID.ValvularAbnormality3711.UID, DicomUID.ValvularAbnormality3711);
            _uids.Add(DicomUID.VesselDescriptor3712.UID, DicomUID.VesselDescriptor3712);
            _uids.Add(DicomUID.TIMIFlowCharacteristic3713.UID, DicomUID.TIMIFlowCharacteristic3713);
            _uids.Add(DicomUID.Thrombus3714.UID, DicomUID.Thrombus3714);
            _uids.Add(DicomUID.LesionMargin3715.UID, DicomUID.LesionMargin3715);
            _uids.Add(DicomUID.Severity3716.UID, DicomUID.Severity3716);
            _uids.Add(DicomUID.LeftVentricleMyocardialWall17SegmentModel3717.UID, DicomUID.LeftVentricleMyocardialWall17SegmentModel3717);
            _uids.Add(DicomUID.MyocardialWallSegmentsInProjection3718.UID, DicomUID.MyocardialWallSegmentsInProjection3718);
            _uids.Add(DicomUID.CanadianClinicalClassification3719.UID, DicomUID.CanadianClinicalClassification3719);
            _uids.Add(DicomUID.CardiacHistoryDate3720RETIRED.UID, DicomUID.CardiacHistoryDate3720RETIRED);
            _uids.Add(DicomUID.CardiovascularSurgery3721.UID, DicomUID.CardiovascularSurgery3721);
            _uids.Add(DicomUID.DiabeticTherapy3722.UID, DicomUID.DiabeticTherapy3722);
            _uids.Add(DicomUID.MIType3723.UID, DicomUID.MIType3723);
            _uids.Add(DicomUID.SmokingHistory3724.UID, DicomUID.SmokingHistory3724);
            _uids.Add(DicomUID.CoronaryInterventionIndication3726.UID, DicomUID.CoronaryInterventionIndication3726);
            _uids.Add(DicomUID.CatheterizationIndication3727.UID, DicomUID.CatheterizationIndication3727);
            _uids.Add(DicomUID.CathFinding3728.UID, DicomUID.CathFinding3728);
            _uids.Add(DicomUID.AdmissionStatus3729.UID, DicomUID.AdmissionStatus3729);
            _uids.Add(DicomUID.InsurancePayor3730.UID, DicomUID.InsurancePayor3730);
            _uids.Add(DicomUID.PrimaryCauseOfDeath3733.UID, DicomUID.PrimaryCauseOfDeath3733);
            _uids.Add(DicomUID.AcuteCoronarySyndromeTimePeriod3735.UID, DicomUID.AcuteCoronarySyndromeTimePeriod3735);
            _uids.Add(DicomUID.NYHAClassification3736.UID, DicomUID.NYHAClassification3736);
            _uids.Add(DicomUID.IschemiaNonInvasiveTest3737.UID, DicomUID.IschemiaNonInvasiveTest3737);
            _uids.Add(DicomUID.PreCathAnginaType3738.UID, DicomUID.PreCathAnginaType3738);
            _uids.Add(DicomUID.CathProcedureType3739.UID, DicomUID.CathProcedureType3739);
            _uids.Add(DicomUID.ThrombolyticAdministration3740.UID, DicomUID.ThrombolyticAdministration3740);
            _uids.Add(DicomUID.LabVisitMedicationAdministration3741.UID, DicomUID.LabVisitMedicationAdministration3741);
            _uids.Add(DicomUID.PCIMedicationAdministration3742.UID, DicomUID.PCIMedicationAdministration3742);
            _uids.Add(DicomUID.ClopidogrelTiclopidineAdministration3743.UID, DicomUID.ClopidogrelTiclopidineAdministration3743);
            _uids.Add(DicomUID.EFTestingMethod3744.UID, DicomUID.EFTestingMethod3744);
            _uids.Add(DicomUID.CalculationMethod3745.UID, DicomUID.CalculationMethod3745);
            _uids.Add(DicomUID.PercutaneousEntrySite3746.UID, DicomUID.PercutaneousEntrySite3746);
            _uids.Add(DicomUID.PercutaneousClosure3747.UID, DicomUID.PercutaneousClosure3747);
            _uids.Add(DicomUID.AngiographicEFTestingMethod3748.UID, DicomUID.AngiographicEFTestingMethod3748);
            _uids.Add(DicomUID.PCIProcedureResult3749.UID, DicomUID.PCIProcedureResult3749);
            _uids.Add(DicomUID.PreviouslyDilatedLesion3750.UID, DicomUID.PreviouslyDilatedLesion3750);
            _uids.Add(DicomUID.GuidewireCrossing3752.UID, DicomUID.GuidewireCrossing3752);
            _uids.Add(DicomUID.VascularComplication3754.UID, DicomUID.VascularComplication3754);
            _uids.Add(DicomUID.CathComplication3755.UID, DicomUID.CathComplication3755);
            _uids.Add(DicomUID.CardiacPatientRiskFactor3756.UID, DicomUID.CardiacPatientRiskFactor3756);
            _uids.Add(DicomUID.CardiacDiagnosticProcedure3757.UID, DicomUID.CardiacDiagnosticProcedure3757);
            _uids.Add(DicomUID.CardiovascularFamilyHistory3758.UID, DicomUID.CardiovascularFamilyHistory3758);
            _uids.Add(DicomUID.HypertensionTherapy3760.UID, DicomUID.HypertensionTherapy3760);
            _uids.Add(DicomUID.AntilipemicAgent3761.UID, DicomUID.AntilipemicAgent3761);
            _uids.Add(DicomUID.AntiarrhythmicAgent3762.UID, DicomUID.AntiarrhythmicAgent3762);
            _uids.Add(DicomUID.MyocardialInfarctionTherapy3764.UID, DicomUID.MyocardialInfarctionTherapy3764);
            _uids.Add(DicomUID.ConcernType3769.UID, DicomUID.ConcernType3769);
            _uids.Add(DicomUID.ProblemStatus3770.UID, DicomUID.ProblemStatus3770);
            _uids.Add(DicomUID.HealthStatus3772.UID, DicomUID.HealthStatus3772);
            _uids.Add(DicomUID.UseStatus3773.UID, DicomUID.UseStatus3773);
            _uids.Add(DicomUID.SocialHistory3774.UID, DicomUID.SocialHistory3774);
            _uids.Add(DicomUID.ImplantedDevice3777.UID, DicomUID.ImplantedDevice3777);
            _uids.Add(DicomUID.PlaqueStructure3802.UID, DicomUID.PlaqueStructure3802);
            _uids.Add(DicomUID.StenosisMeasurementMethod3804.UID, DicomUID.StenosisMeasurementMethod3804);
            _uids.Add(DicomUID.StenosisType3805.UID, DicomUID.StenosisType3805);
            _uids.Add(DicomUID.StenosisShape3806.UID, DicomUID.StenosisShape3806);
            _uids.Add(DicomUID.VolumeMeasurementMethod3807.UID, DicomUID.VolumeMeasurementMethod3807);
            _uids.Add(DicomUID.AneurysmType3808.UID, DicomUID.AneurysmType3808);
            _uids.Add(DicomUID.AssociatedCondition3809.UID, DicomUID.AssociatedCondition3809);
            _uids.Add(DicomUID.VascularMorphology3810.UID, DicomUID.VascularMorphology3810);
            _uids.Add(DicomUID.StentFinding3813.UID, DicomUID.StentFinding3813);
            _uids.Add(DicomUID.StentComposition3814.UID, DicomUID.StentComposition3814);
            _uids.Add(DicomUID.SourceOfVascularFinding3815.UID, DicomUID.SourceOfVascularFinding3815);
            _uids.Add(DicomUID.VascularSclerosisType3817.UID, DicomUID.VascularSclerosisType3817);
            _uids.Add(DicomUID.NonInvasiveVascularProcedure3820.UID, DicomUID.NonInvasiveVascularProcedure3820);
            _uids.Add(DicomUID.PapillaryMuscleIncludedExcluded3821.UID, DicomUID.PapillaryMuscleIncludedExcluded3821);
            _uids.Add(DicomUID.RespiratoryStatus3823.UID, DicomUID.RespiratoryStatus3823);
            _uids.Add(DicomUID.HeartRhythm3826.UID, DicomUID.HeartRhythm3826);
            _uids.Add(DicomUID.VesselSegment3827.UID, DicomUID.VesselSegment3827);
            _uids.Add(DicomUID.PulmonaryArtery3829.UID, DicomUID.PulmonaryArtery3829);
            _uids.Add(DicomUID.StenosisLength3831.UID, DicomUID.StenosisLength3831);
            _uids.Add(DicomUID.StenosisGrade3832.UID, DicomUID.StenosisGrade3832);
            _uids.Add(DicomUID.CardiacEjectionFraction3833.UID, DicomUID.CardiacEjectionFraction3833);
            _uids.Add(DicomUID.CardiacVolumeMeasurement3835.UID, DicomUID.CardiacVolumeMeasurement3835);
            _uids.Add(DicomUID.TimeBasedPerfusionMeasurement3836.UID, DicomUID.TimeBasedPerfusionMeasurement3836);
            _uids.Add(DicomUID.FiducialFeature3837.UID, DicomUID.FiducialFeature3837);
            _uids.Add(DicomUID.DiameterDerivation3838.UID, DicomUID.DiameterDerivation3838);
            _uids.Add(DicomUID.CoronaryVein3839.UID, DicomUID.CoronaryVein3839);
            _uids.Add(DicomUID.PulmonaryVein3840.UID, DicomUID.PulmonaryVein3840);
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
            _uids.Add(DicomUID.CraniofacialAnatomicRegion4028.UID, DicomUID.CraniofacialAnatomicRegion4028);
            _uids.Add(DicomUID.CTMRAndPETAnatomyImaged4030.UID, DicomUID.CTMRAndPETAnatomyImaged4030);
            _uids.Add(DicomUID.CommonAnatomicRegion4031.UID, DicomUID.CommonAnatomicRegion4031);
            _uids.Add(DicomUID.MRSpectroscopyMetabolite4032.UID, DicomUID.MRSpectroscopyMetabolite4032);
            _uids.Add(DicomUID.MRProtonSpectroscopyMetabolite4033.UID, DicomUID.MRProtonSpectroscopyMetabolite4033);
            _uids.Add(DicomUID.EndoscopyAnatomicRegion4040.UID, DicomUID.EndoscopyAnatomicRegion4040);
            _uids.Add(DicomUID.XAXRFAnatomyImaged4042.UID, DicomUID.XAXRFAnatomyImaged4042);
            _uids.Add(DicomUID.DrugOrContrastAgentCharacteristic4050.UID, DicomUID.DrugOrContrastAgentCharacteristic4050);
            _uids.Add(DicomUID.GeneralDevice4051.UID, DicomUID.GeneralDevice4051);
            _uids.Add(DicomUID.PhantomDevice4052.UID, DicomUID.PhantomDevice4052);
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
            _uids.Add(DicomUID.Language5000.UID, DicomUID.Language5000);
            _uids.Add(DicomUID.Country5001.UID, DicomUID.Country5001);
            _uids.Add(DicomUID.OverallBreastComposition6000.UID, DicomUID.OverallBreastComposition6000);
            _uids.Add(DicomUID.OverallBreastCompositionFromBIRADS6001.UID, DicomUID.OverallBreastCompositionFromBIRADS6001);
            _uids.Add(DicomUID.ChangeSinceLastMammogramOrPriorSurgery6002.UID, DicomUID.ChangeSinceLastMammogramOrPriorSurgery6002);
            _uids.Add(DicomUID.ChangeSinceLastMammogramOrPriorSurgeryFromBIRADS6003.UID, DicomUID.ChangeSinceLastMammogramOrPriorSurgeryFromBIRADS6003);
            _uids.Add(DicomUID.MammographyShapeCharacteristic6004.UID, DicomUID.MammographyShapeCharacteristic6004);
            _uids.Add(DicomUID.ShapeCharacteristicFromBIRADS6005.UID, DicomUID.ShapeCharacteristicFromBIRADS6005);
            _uids.Add(DicomUID.MammographyMarginCharacteristic6006.UID, DicomUID.MammographyMarginCharacteristic6006);
            _uids.Add(DicomUID.MarginCharacteristicFromBIRADS6007.UID, DicomUID.MarginCharacteristicFromBIRADS6007);
            _uids.Add(DicomUID.DensityModifier6008.UID, DicomUID.DensityModifier6008);
            _uids.Add(DicomUID.DensityModifierFromBIRADS6009.UID, DicomUID.DensityModifierFromBIRADS6009);
            _uids.Add(DicomUID.MammographyCalcificationType6010.UID, DicomUID.MammographyCalcificationType6010);
            _uids.Add(DicomUID.CalcificationTypeFromBIRADS6011.UID, DicomUID.CalcificationTypeFromBIRADS6011);
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
            _uids.Add(DicomUID.MammographyPathologyCode6030.UID, DicomUID.MammographyPathologyCode6030);
            _uids.Add(DicomUID.BenignPathologyCodeFromBIRADS6031.UID, DicomUID.BenignPathologyCodeFromBIRADS6031);
            _uids.Add(DicomUID.HighRiskLesionPathologyCodeFromBIRADS6032.UID, DicomUID.HighRiskLesionPathologyCodeFromBIRADS6032);
            _uids.Add(DicomUID.MalignantPathologyCodeFromBIRADS6033.UID, DicomUID.MalignantPathologyCodeFromBIRADS6033);
            _uids.Add(DicomUID.CADOutputIntendedUse6034.UID, DicomUID.CADOutputIntendedUse6034);
            _uids.Add(DicomUID.CompositeFeatureRelation6035.UID, DicomUID.CompositeFeatureRelation6035);
            _uids.Add(DicomUID.FeatureScope6036.UID, DicomUID.FeatureScope6036);
            _uids.Add(DicomUID.MammographyQuantitativeTemporalDifferenceType6037.UID, DicomUID.MammographyQuantitativeTemporalDifferenceType6037);
            _uids.Add(DicomUID.MammographyQualitativeTemporalDifferenceType6038.UID, DicomUID.MammographyQualitativeTemporalDifferenceType6038);
            _uids.Add(DicomUID.NippleCharacteristic6039.UID, DicomUID.NippleCharacteristic6039);
            _uids.Add(DicomUID.NonLesionObjectType6040.UID, DicomUID.NonLesionObjectType6040);
            _uids.Add(DicomUID.MammographyImageQualityFinding6041.UID, DicomUID.MammographyImageQualityFinding6041);
            _uids.Add(DicomUID.ResultStatus6042.UID, DicomUID.ResultStatus6042);
            _uids.Add(DicomUID.MammographyCADAnalysisType6043.UID, DicomUID.MammographyCADAnalysisType6043);
            _uids.Add(DicomUID.ImageQualityAssessmentType6044.UID, DicomUID.ImageQualityAssessmentType6044);
            _uids.Add(DicomUID.MammographyQualityControlStandardType6045.UID, DicomUID.MammographyQualityControlStandardType6045);
            _uids.Add(DicomUID.FollowUpIntervalUnit6046.UID, DicomUID.FollowUpIntervalUnit6046);
            _uids.Add(DicomUID.CADProcessingAndFindingSummary6047.UID, DicomUID.CADProcessingAndFindingSummary6047);
            _uids.Add(DicomUID.CADOperatingPointAxisLabel6048.UID, DicomUID.CADOperatingPointAxisLabel6048);
            _uids.Add(DicomUID.BreastProcedureReported6050.UID, DicomUID.BreastProcedureReported6050);
            _uids.Add(DicomUID.BreastProcedureReason6051.UID, DicomUID.BreastProcedureReason6051);
            _uids.Add(DicomUID.BreastImagingReportSectionTitle6052.UID, DicomUID.BreastImagingReportSectionTitle6052);
            _uids.Add(DicomUID.BreastImagingReportElement6053.UID, DicomUID.BreastImagingReportElement6053);
            _uids.Add(DicomUID.BreastImagingFinding6054.UID, DicomUID.BreastImagingFinding6054);
            _uids.Add(DicomUID.BreastClinicalFindingOrIndicatedProblem6055.UID, DicomUID.BreastClinicalFindingOrIndicatedProblem6055);
            _uids.Add(DicomUID.AssociatedFindingForBreast6056.UID, DicomUID.AssociatedFindingForBreast6056);
            _uids.Add(DicomUID.DuctographyFindingForBreast6057.UID, DicomUID.DuctographyFindingForBreast6057);
            _uids.Add(DicomUID.ProcedureModifiersForBreast6058.UID, DicomUID.ProcedureModifiersForBreast6058);
            _uids.Add(DicomUID.BreastImplantType6059.UID, DicomUID.BreastImplantType6059);
            _uids.Add(DicomUID.BreastBiopsyTechnique6060.UID, DicomUID.BreastBiopsyTechnique6060);
            _uids.Add(DicomUID.BreastImagingProcedureModifier6061.UID, DicomUID.BreastImagingProcedureModifier6061);
            _uids.Add(DicomUID.InterventionalProcedureComplication6062.UID, DicomUID.InterventionalProcedureComplication6062);
            _uids.Add(DicomUID.InterventionalProcedureResult6063.UID, DicomUID.InterventionalProcedureResult6063);
            _uids.Add(DicomUID.UltrasoundFindingForBreast6064.UID, DicomUID.UltrasoundFindingForBreast6064);
            _uids.Add(DicomUID.InstrumentApproach6065.UID, DicomUID.InstrumentApproach6065);
            _uids.Add(DicomUID.TargetConfirmation6066.UID, DicomUID.TargetConfirmation6066);
            _uids.Add(DicomUID.FluidColor6067.UID, DicomUID.FluidColor6067);
            _uids.Add(DicomUID.TumorStagesFromAJCC6068.UID, DicomUID.TumorStagesFromAJCC6068);
            _uids.Add(DicomUID.NottinghamCombinedHistologicGrade6069.UID, DicomUID.NottinghamCombinedHistologicGrade6069);
            _uids.Add(DicomUID.BloomRichardsonHistologicGrade6070.UID, DicomUID.BloomRichardsonHistologicGrade6070);
            _uids.Add(DicomUID.HistologicGradingMethod6071.UID, DicomUID.HistologicGradingMethod6071);
            _uids.Add(DicomUID.BreastImplantFinding6072.UID, DicomUID.BreastImplantFinding6072);
            _uids.Add(DicomUID.GynecologicalHormone6080.UID, DicomUID.GynecologicalHormone6080);
            _uids.Add(DicomUID.BreastCancerRiskFactor6081.UID, DicomUID.BreastCancerRiskFactor6081);
            _uids.Add(DicomUID.GynecologicalProcedure6082.UID, DicomUID.GynecologicalProcedure6082);
            _uids.Add(DicomUID.ProceduresForBreast6083.UID, DicomUID.ProceduresForBreast6083);
            _uids.Add(DicomUID.MammoplastyProcedure6084.UID, DicomUID.MammoplastyProcedure6084);
            _uids.Add(DicomUID.TherapiesForBreast6085.UID, DicomUID.TherapiesForBreast6085);
            _uids.Add(DicomUID.MenopausalPhase6086.UID, DicomUID.MenopausalPhase6086);
            _uids.Add(DicomUID.GeneralRiskFactor6087.UID, DicomUID.GeneralRiskFactor6087);
            _uids.Add(DicomUID.OBGYNMaternalRiskFactor6088.UID, DicomUID.OBGYNMaternalRiskFactor6088);
            _uids.Add(DicomUID.Substance6089.UID, DicomUID.Substance6089);
            _uids.Add(DicomUID.RelativeUsageExposureAmount6090.UID, DicomUID.RelativeUsageExposureAmount6090);
            _uids.Add(DicomUID.RelativeFrequencyOfEventValue6091.UID, DicomUID.RelativeFrequencyOfEventValue6091);
            _uids.Add(DicomUID.UsageExposureQualitativeConcept6092.UID, DicomUID.UsageExposureQualitativeConcept6092);
            _uids.Add(DicomUID.UsageExposureAmountQualitativeConcept6093.UID, DicomUID.UsageExposureAmountQualitativeConcept6093);
            _uids.Add(DicomUID.UsageExposureFrequencyQualitativeConcept6094.UID, DicomUID.UsageExposureFrequencyQualitativeConcept6094);
            _uids.Add(DicomUID.ProcedureNumericProperty6095.UID, DicomUID.ProcedureNumericProperty6095);
            _uids.Add(DicomUID.PregnancyStatus6096.UID, DicomUID.PregnancyStatus6096);
            _uids.Add(DicomUID.SideOfFamily6097.UID, DicomUID.SideOfFamily6097);
            _uids.Add(DicomUID.ChestComponentCategory6100.UID, DicomUID.ChestComponentCategory6100);
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
            _uids.Add(DicomUID.OsseousAnatomyModifier6115.UID, DicomUID.OsseousAnatomyModifier6115);
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
            _uids.Add(DicomUID.CADAnalysisType6137.UID, DicomUID.CADAnalysisType6137);
            _uids.Add(DicomUID.ChestNonLesionObjectType6138.UID, DicomUID.ChestNonLesionObjectType6138);
            _uids.Add(DicomUID.NonLesionModifier6139.UID, DicomUID.NonLesionModifier6139);
            _uids.Add(DicomUID.CalculationMethod6140.UID, DicomUID.CalculationMethod6140);
            _uids.Add(DicomUID.AttenuationCoefficientMeasurement6141.UID, DicomUID.AttenuationCoefficientMeasurement6141);
            _uids.Add(DicomUID.CalculatedValue6142.UID, DicomUID.CalculatedValue6142);
            _uids.Add(DicomUID.LesionResponse6143.UID, DicomUID.LesionResponse6143);
            _uids.Add(DicomUID.RECISTDefinedLesionResponse6144.UID, DicomUID.RECISTDefinedLesionResponse6144);
            _uids.Add(DicomUID.BaselineCategory6145.UID, DicomUID.BaselineCategory6145);
            _uids.Add(DicomUID.BackgroundEchotexture6151.UID, DicomUID.BackgroundEchotexture6151);
            _uids.Add(DicomUID.Orientation6152.UID, DicomUID.Orientation6152);
            _uids.Add(DicomUID.LesionBoundary6153.UID, DicomUID.LesionBoundary6153);
            _uids.Add(DicomUID.EchoPattern6154.UID, DicomUID.EchoPattern6154);
            _uids.Add(DicomUID.PosteriorAcousticFeature6155.UID, DicomUID.PosteriorAcousticFeature6155);
            _uids.Add(DicomUID.Vascularity6157.UID, DicomUID.Vascularity6157);
            _uids.Add(DicomUID.CorrelationToOtherFinding6158.UID, DicomUID.CorrelationToOtherFinding6158);
            _uids.Add(DicomUID.MalignancyType6159.UID, DicomUID.MalignancyType6159);
            _uids.Add(DicomUID.BreastPrimaryTumorAssessmentFromAJCC6160.UID, DicomUID.BreastPrimaryTumorAssessmentFromAJCC6160);
            _uids.Add(DicomUID.PathologicalRegionalLymphNodeAssessmentForBreast6161.UID, DicomUID.PathologicalRegionalLymphNodeAssessmentForBreast6161);
            _uids.Add(DicomUID.AssessmentOfMetastasisForBreast6162.UID, DicomUID.AssessmentOfMetastasisForBreast6162);
            _uids.Add(DicomUID.MenstrualCyclePhase6163.UID, DicomUID.MenstrualCyclePhase6163);
            _uids.Add(DicomUID.TimeInterval6164.UID, DicomUID.TimeInterval6164);
            _uids.Add(DicomUID.BreastLinearMeasurement6165.UID, DicomUID.BreastLinearMeasurement6165);
            _uids.Add(DicomUID.CADGeometrySecondaryGraphicalRepresentation6166.UID, DicomUID.CADGeometrySecondaryGraphicalRepresentation6166);
            _uids.Add(DicomUID.DiagnosticImagingReportDocumentTitle7000.UID, DicomUID.DiagnosticImagingReportDocumentTitle7000);
            _uids.Add(DicomUID.DiagnosticImagingReportHeading7001.UID, DicomUID.DiagnosticImagingReportHeading7001);
            _uids.Add(DicomUID.DiagnosticImagingReportElement7002.UID, DicomUID.DiagnosticImagingReportElement7002);
            _uids.Add(DicomUID.DiagnosticImagingReportPurposeOfReference7003.UID, DicomUID.DiagnosticImagingReportPurposeOfReference7003);
            _uids.Add(DicomUID.WaveformPurposeOfReference7004.UID, DicomUID.WaveformPurposeOfReference7004);
            _uids.Add(DicomUID.ContributingEquipmentPurposeOfReference7005.UID, DicomUID.ContributingEquipmentPurposeOfReference7005);
            _uids.Add(DicomUID.SRDocumentPurposeOfReference7006.UID, DicomUID.SRDocumentPurposeOfReference7006);
            _uids.Add(DicomUID.SignaturePurpose7007.UID, DicomUID.SignaturePurpose7007);
            _uids.Add(DicomUID.MediaImport7008.UID, DicomUID.MediaImport7008);
            _uids.Add(DicomUID.KeyObjectSelectionDocumentTitle7010.UID, DicomUID.KeyObjectSelectionDocumentTitle7010);
            _uids.Add(DicomUID.RejectedForQualityReason7011.UID, DicomUID.RejectedForQualityReason7011);
            _uids.Add(DicomUID.BestInSet7012.UID, DicomUID.BestInSet7012);
            _uids.Add(DicomUID.DocumentTitle7020.UID, DicomUID.DocumentTitle7020);
            _uids.Add(DicomUID.RCSRegistrationMethodType7100.UID, DicomUID.RCSRegistrationMethodType7100);
            _uids.Add(DicomUID.BrainAtlasFiducial7101.UID, DicomUID.BrainAtlasFiducial7101);
            _uids.Add(DicomUID.SegmentationPropertyCategory7150.UID, DicomUID.SegmentationPropertyCategory7150);
            _uids.Add(DicomUID.SegmentationPropertyType7151.UID, DicomUID.SegmentationPropertyType7151);
            _uids.Add(DicomUID.CardiacStructureSegmentationType7152.UID, DicomUID.CardiacStructureSegmentationType7152);
            _uids.Add(DicomUID.CNSSegmentationType7153.UID, DicomUID.CNSSegmentationType7153);
            _uids.Add(DicomUID.AbdominalSegmentationType7154.UID, DicomUID.AbdominalSegmentationType7154);
            _uids.Add(DicomUID.ThoracicSegmentationType7155.UID, DicomUID.ThoracicSegmentationType7155);
            _uids.Add(DicomUID.VascularSegmentationType7156.UID, DicomUID.VascularSegmentationType7156);
            _uids.Add(DicomUID.DeviceSegmentationType7157.UID, DicomUID.DeviceSegmentationType7157);
            _uids.Add(DicomUID.ArtifactSegmentationType7158.UID, DicomUID.ArtifactSegmentationType7158);
            _uids.Add(DicomUID.LesionSegmentationType7159.UID, DicomUID.LesionSegmentationType7159);
            _uids.Add(DicomUID.PelvicOrganSegmentationType7160.UID, DicomUID.PelvicOrganSegmentationType7160);
            _uids.Add(DicomUID.PhysiologySegmentationType7161.UID, DicomUID.PhysiologySegmentationType7161);
            _uids.Add(DicomUID.ReferencedImagePurposeOfReference7201.UID, DicomUID.ReferencedImagePurposeOfReference7201);
            _uids.Add(DicomUID.SourceImagePurposeOfReference7202.UID, DicomUID.SourceImagePurposeOfReference7202);
            _uids.Add(DicomUID.ImageDerivation7203.UID, DicomUID.ImageDerivation7203);
            _uids.Add(DicomUID.PurposeOfReferenceToAlternateRepresentation7205.UID, DicomUID.PurposeOfReferenceToAlternateRepresentation7205);
            _uids.Add(DicomUID.RelatedSeriesPurposeOfReference7210.UID, DicomUID.RelatedSeriesPurposeOfReference7210);
            _uids.Add(DicomUID.MultiFrameSubsetType7250.UID, DicomUID.MultiFrameSubsetType7250);
            _uids.Add(DicomUID.PersonRole7450.UID, DicomUID.PersonRole7450);
            _uids.Add(DicomUID.FamilyMember7451.UID, DicomUID.FamilyMember7451);
            _uids.Add(DicomUID.OrganizationalRole7452.UID, DicomUID.OrganizationalRole7452);
            _uids.Add(DicomUID.PerformingRole7453.UID, DicomUID.PerformingRole7453);
            _uids.Add(DicomUID.AnimalTaxonomicRankValue7454.UID, DicomUID.AnimalTaxonomicRankValue7454);
            _uids.Add(DicomUID.Sex7455.UID, DicomUID.Sex7455);
            _uids.Add(DicomUID.AgeUnit7456.UID, DicomUID.AgeUnit7456);
            _uids.Add(DicomUID.LinearMeasurementUnit7460.UID, DicomUID.LinearMeasurementUnit7460);
            _uids.Add(DicomUID.AreaMeasurementUnit7461.UID, DicomUID.AreaMeasurementUnit7461);
            _uids.Add(DicomUID.VolumeMeasurementUnit7462.UID, DicomUID.VolumeMeasurementUnit7462);
            _uids.Add(DicomUID.LinearMeasurement7470.UID, DicomUID.LinearMeasurement7470);
            _uids.Add(DicomUID.AreaMeasurement7471.UID, DicomUID.AreaMeasurement7471);
            _uids.Add(DicomUID.VolumeMeasurement7472.UID, DicomUID.VolumeMeasurement7472);
            _uids.Add(DicomUID.GeneralAreaCalculationMethod7473.UID, DicomUID.GeneralAreaCalculationMethod7473);
            _uids.Add(DicomUID.GeneralVolumeCalculationMethod7474.UID, DicomUID.GeneralVolumeCalculationMethod7474);
            _uids.Add(DicomUID.Breed7480.UID, DicomUID.Breed7480);
            _uids.Add(DicomUID.BreedRegistry7481.UID, DicomUID.BreedRegistry7481);
            _uids.Add(DicomUID.WorkitemDefinition9231.UID, DicomUID.WorkitemDefinition9231);
            _uids.Add(DicomUID.NonDICOMOutputTypes9232RETIRED.UID, DicomUID.NonDICOMOutputTypes9232RETIRED);
            _uids.Add(DicomUID.ProcedureDiscontinuationReason9300.UID, DicomUID.ProcedureDiscontinuationReason9300);
            _uids.Add(DicomUID.ScopeOfAccumulation10000.UID, DicomUID.ScopeOfAccumulation10000);
            _uids.Add(DicomUID.UIDType10001.UID, DicomUID.UIDType10001);
            _uids.Add(DicomUID.IrradiationEventType10002.UID, DicomUID.IrradiationEventType10002);
            _uids.Add(DicomUID.EquipmentPlaneIdentification10003.UID, DicomUID.EquipmentPlaneIdentification10003);
            _uids.Add(DicomUID.FluoroMode10004.UID, DicomUID.FluoroMode10004);
            _uids.Add(DicomUID.XRayFilterMaterial10006.UID, DicomUID.XRayFilterMaterial10006);
            _uids.Add(DicomUID.XRayFilterType10007.UID, DicomUID.XRayFilterType10007);
            _uids.Add(DicomUID.DoseRelatedDistanceMeasurement10008.UID, DicomUID.DoseRelatedDistanceMeasurement10008);
            _uids.Add(DicomUID.MeasuredCalculated10009.UID, DicomUID.MeasuredCalculated10009);
            _uids.Add(DicomUID.DoseMeasurementDevice10010.UID, DicomUID.DoseMeasurementDevice10010);
            _uids.Add(DicomUID.EffectiveDoseEvaluationMethod10011.UID, DicomUID.EffectiveDoseEvaluationMethod10011);
            _uids.Add(DicomUID.CTAcquisitionType10013.UID, DicomUID.CTAcquisitionType10013);
            _uids.Add(DicomUID.ContrastImagingTechnique10014.UID, DicomUID.ContrastImagingTechnique10014);
            _uids.Add(DicomUID.CTDoseReferenceAuthority10015.UID, DicomUID.CTDoseReferenceAuthority10015);
            _uids.Add(DicomUID.AnodeTargetMaterial10016.UID, DicomUID.AnodeTargetMaterial10016);
            _uids.Add(DicomUID.XRayGrid10017.UID, DicomUID.XRayGrid10017);
            _uids.Add(DicomUID.UltrasoundProtocolType12001.UID, DicomUID.UltrasoundProtocolType12001);
            _uids.Add(DicomUID.UltrasoundProtocolStageType12002.UID, DicomUID.UltrasoundProtocolStageType12002);
            _uids.Add(DicomUID.OBGYNDate12003.UID, DicomUID.OBGYNDate12003);
            _uids.Add(DicomUID.FetalBiometryRatio12004.UID, DicomUID.FetalBiometryRatio12004);
            _uids.Add(DicomUID.FetalBiometryMeasurement12005.UID, DicomUID.FetalBiometryMeasurement12005);
            _uids.Add(DicomUID.FetalLongBonesBiometryMeasurement12006.UID, DicomUID.FetalLongBonesBiometryMeasurement12006);
            _uids.Add(DicomUID.FetalCraniumMeasurement12007.UID, DicomUID.FetalCraniumMeasurement12007);
            _uids.Add(DicomUID.OBGYNAmnioticSacMeasurement12008.UID, DicomUID.OBGYNAmnioticSacMeasurement12008);
            _uids.Add(DicomUID.EarlyGestationBiometryMeasurement12009.UID, DicomUID.EarlyGestationBiometryMeasurement12009);
            _uids.Add(DicomUID.UltrasoundPelvisAndUterusMeasurement12011.UID, DicomUID.UltrasoundPelvisAndUterusMeasurement12011);
            _uids.Add(DicomUID.OBEquationTable12012.UID, DicomUID.OBEquationTable12012);
            _uids.Add(DicomUID.GestationalAgeEquationTable12013.UID, DicomUID.GestationalAgeEquationTable12013);
            _uids.Add(DicomUID.OBFetalBodyWeightEquationTable12014.UID, DicomUID.OBFetalBodyWeightEquationTable12014);
            _uids.Add(DicomUID.FetalGrowthEquationTable12015.UID, DicomUID.FetalGrowthEquationTable12015);
            _uids.Add(DicomUID.EstimatedFetalWeightPercentileEquationTable12016.UID, DicomUID.EstimatedFetalWeightPercentileEquationTable12016);
            _uids.Add(DicomUID.GrowthDistributionRank12017.UID, DicomUID.GrowthDistributionRank12017);
            _uids.Add(DicomUID.OBGYNSummary12018.UID, DicomUID.OBGYNSummary12018);
            _uids.Add(DicomUID.OBGYNFetusSummary12019.UID, DicomUID.OBGYNFetusSummary12019);
            _uids.Add(DicomUID.VascularSummary12101.UID, DicomUID.VascularSummary12101);
            _uids.Add(DicomUID.TemporalPeriodRelatingToProcedureOrTherapy12102.UID, DicomUID.TemporalPeriodRelatingToProcedureOrTherapy12102);
            _uids.Add(DicomUID.VascularUltrasoundAnatomicLocation12103.UID, DicomUID.VascularUltrasoundAnatomicLocation12103);
            _uids.Add(DicomUID.ExtracranialArtery12104.UID, DicomUID.ExtracranialArtery12104);
            _uids.Add(DicomUID.IntracranialCerebralVessel12105.UID, DicomUID.IntracranialCerebralVessel12105);
            _uids.Add(DicomUID.IntracranialCerebralVesselUnilateral12106.UID, DicomUID.IntracranialCerebralVesselUnilateral12106);
            _uids.Add(DicomUID.UpperExtremityArtery12107.UID, DicomUID.UpperExtremityArtery12107);
            _uids.Add(DicomUID.UpperExtremityVein12108.UID, DicomUID.UpperExtremityVein12108);
            _uids.Add(DicomUID.LowerExtremityArtery12109.UID, DicomUID.LowerExtremityArtery12109);
            _uids.Add(DicomUID.LowerExtremityVein12110.UID, DicomUID.LowerExtremityVein12110);
            _uids.Add(DicomUID.AbdominopelvicArteryPaired12111.UID, DicomUID.AbdominopelvicArteryPaired12111);
            _uids.Add(DicomUID.AbdominopelvicArteryUnpaired12112.UID, DicomUID.AbdominopelvicArteryUnpaired12112);
            _uids.Add(DicomUID.AbdominopelvicVeinPaired12113.UID, DicomUID.AbdominopelvicVeinPaired12113);
            _uids.Add(DicomUID.AbdominopelvicVeinUnpaired12114.UID, DicomUID.AbdominopelvicVeinUnpaired12114);
            _uids.Add(DicomUID.RenalVessel12115.UID, DicomUID.RenalVessel12115);
            _uids.Add(DicomUID.VesselSegmentModifier12116.UID, DicomUID.VesselSegmentModifier12116);
            _uids.Add(DicomUID.VesselBranchModifier12117.UID, DicomUID.VesselBranchModifier12117);
            _uids.Add(DicomUID.VascularUltrasoundProperty12119.UID, DicomUID.VascularUltrasoundProperty12119);
            _uids.Add(DicomUID.UltrasoundBloodVelocityMeasurement12120.UID, DicomUID.UltrasoundBloodVelocityMeasurement12120);
            _uids.Add(DicomUID.VascularIndexRatio12121.UID, DicomUID.VascularIndexRatio12121);
            _uids.Add(DicomUID.OtherVascularProperty12122.UID, DicomUID.OtherVascularProperty12122);
            _uids.Add(DicomUID.CarotidRatio12123.UID, DicomUID.CarotidRatio12123);
            _uids.Add(DicomUID.RenalRatio12124.UID, DicomUID.RenalRatio12124);
            _uids.Add(DicomUID.PelvicVasculatureAnatomicalLocation12140.UID, DicomUID.PelvicVasculatureAnatomicalLocation12140);
            _uids.Add(DicomUID.FetalVasculatureAnatomicalLocation12141.UID, DicomUID.FetalVasculatureAnatomicalLocation12141);
            _uids.Add(DicomUID.EchocardiographyLeftVentricleMeasurement12200.UID, DicomUID.EchocardiographyLeftVentricleMeasurement12200);
            _uids.Add(DicomUID.LeftVentricleLinearMeasurement12201.UID, DicomUID.LeftVentricleLinearMeasurement12201);
            _uids.Add(DicomUID.LeftVentricleVolumeMeasurement12202.UID, DicomUID.LeftVentricleVolumeMeasurement12202);
            _uids.Add(DicomUID.LeftVentricleOtherMeasurement12203.UID, DicomUID.LeftVentricleOtherMeasurement12203);
            _uids.Add(DicomUID.EchocardiographyRightVentricleMeasurement12204.UID, DicomUID.EchocardiographyRightVentricleMeasurement12204);
            _uids.Add(DicomUID.EchocardiographyLeftAtriumMeasurement12205.UID, DicomUID.EchocardiographyLeftAtriumMeasurement12205);
            _uids.Add(DicomUID.EchocardiographyRightAtriumMeasurement12206.UID, DicomUID.EchocardiographyRightAtriumMeasurement12206);
            _uids.Add(DicomUID.EchocardiographyMitralValveMeasurement12207.UID, DicomUID.EchocardiographyMitralValveMeasurement12207);
            _uids.Add(DicomUID.EchocardiographyTricuspidValveMeasurement12208.UID, DicomUID.EchocardiographyTricuspidValveMeasurement12208);
            _uids.Add(DicomUID.EchocardiographyPulmonicValveMeasurement12209.UID, DicomUID.EchocardiographyPulmonicValveMeasurement12209);
            _uids.Add(DicomUID.EchocardiographyPulmonaryArteryMeasurement12210.UID, DicomUID.EchocardiographyPulmonaryArteryMeasurement12210);
            _uids.Add(DicomUID.EchocardiographyAorticValveMeasurement12211.UID, DicomUID.EchocardiographyAorticValveMeasurement12211);
            _uids.Add(DicomUID.EchocardiographyAortaMeasurement12212.UID, DicomUID.EchocardiographyAortaMeasurement12212);
            _uids.Add(DicomUID.EchocardiographyPulmonaryVeinMeasurement12214.UID, DicomUID.EchocardiographyPulmonaryVeinMeasurement12214);
            _uids.Add(DicomUID.EchocardiographyVenaCavaMeasurement12215.UID, DicomUID.EchocardiographyVenaCavaMeasurement12215);
            _uids.Add(DicomUID.EchocardiographyHepaticVeinMeasurement12216.UID, DicomUID.EchocardiographyHepaticVeinMeasurement12216);
            _uids.Add(DicomUID.EchocardiographyCardiacShuntMeasurement12217.UID, DicomUID.EchocardiographyCardiacShuntMeasurement12217);
            _uids.Add(DicomUID.EchocardiographyCongenitalAnomalyMeasurement12218.UID, DicomUID.EchocardiographyCongenitalAnomalyMeasurement12218);
            _uids.Add(DicomUID.PulmonaryVeinModifier12219.UID, DicomUID.PulmonaryVeinModifier12219);
            _uids.Add(DicomUID.EchocardiographyCommonMeasurement12220.UID, DicomUID.EchocardiographyCommonMeasurement12220);
            _uids.Add(DicomUID.FlowDirection12221.UID, DicomUID.FlowDirection12221);
            _uids.Add(DicomUID.OrificeFlowProperty12222.UID, DicomUID.OrificeFlowProperty12222);
            _uids.Add(DicomUID.EchocardiographyStrokeVolumeOrigin12223.UID, DicomUID.EchocardiographyStrokeVolumeOrigin12223);
            _uids.Add(DicomUID.UltrasoundImageMode12224.UID, DicomUID.UltrasoundImageMode12224);
            _uids.Add(DicomUID.EchocardiographyImageView12226.UID, DicomUID.EchocardiographyImageView12226);
            _uids.Add(DicomUID.EchocardiographyMeasurementMethod12227.UID, DicomUID.EchocardiographyMeasurementMethod12227);
            _uids.Add(DicomUID.EchocardiographyVolumeMethod12228.UID, DicomUID.EchocardiographyVolumeMethod12228);
            _uids.Add(DicomUID.EchocardiographyAreaMethod12229.UID, DicomUID.EchocardiographyAreaMethod12229);
            _uids.Add(DicomUID.GradientMethod12230.UID, DicomUID.GradientMethod12230);
            _uids.Add(DicomUID.VolumeFlowMethod12231.UID, DicomUID.VolumeFlowMethod12231);
            _uids.Add(DicomUID.MyocardiumMassMethod12232.UID, DicomUID.MyocardiumMassMethod12232);
            _uids.Add(DicomUID.CardiacPhase12233.UID, DicomUID.CardiacPhase12233);
            _uids.Add(DicomUID.RespirationState12234.UID, DicomUID.RespirationState12234);
            _uids.Add(DicomUID.MitralValveAnatomicSite12235.UID, DicomUID.MitralValveAnatomicSite12235);
            _uids.Add(DicomUID.EchocardiographyAnatomicSite12236.UID, DicomUID.EchocardiographyAnatomicSite12236);
            _uids.Add(DicomUID.EchocardiographyAnatomicSiteModifier12237.UID, DicomUID.EchocardiographyAnatomicSiteModifier12237);
            _uids.Add(DicomUID.WallMotionScoringScheme12238.UID, DicomUID.WallMotionScoringScheme12238);
            _uids.Add(DicomUID.CardiacOutputProperty12239.UID, DicomUID.CardiacOutputProperty12239);
            _uids.Add(DicomUID.LeftVentricleAreaMeasurement12240.UID, DicomUID.LeftVentricleAreaMeasurement12240);
            _uids.Add(DicomUID.TricuspidValveFindingSite12241.UID, DicomUID.TricuspidValveFindingSite12241);
            _uids.Add(DicomUID.AorticValveFindingSite12242.UID, DicomUID.AorticValveFindingSite12242);
            _uids.Add(DicomUID.LeftVentricleFindingSite12243.UID, DicomUID.LeftVentricleFindingSite12243);
            _uids.Add(DicomUID.CongenitalFindingSite12244.UID, DicomUID.CongenitalFindingSite12244);
            _uids.Add(DicomUID.SurfaceProcessingAlgorithmFamily7162.UID, DicomUID.SurfaceProcessingAlgorithmFamily7162);
            _uids.Add(DicomUID.StressTestProcedurePhase3207.UID, DicomUID.StressTestProcedurePhase3207);
            _uids.Add(DicomUID.Stage3778.UID, DicomUID.Stage3778);
            _uids.Add(DicomUID.SMLSizeDescriptor252.UID, DicomUID.SMLSizeDescriptor252);
            _uids.Add(DicomUID.MajorCoronaryArtery3016.UID, DicomUID.MajorCoronaryArtery3016);
            _uids.Add(DicomUID.RadioactivityUnit3083.UID, DicomUID.RadioactivityUnit3083);
            _uids.Add(DicomUID.RestStressState3102.UID, DicomUID.RestStressState3102);
            _uids.Add(DicomUID.PETCardiologyProtocol3106.UID, DicomUID.PETCardiologyProtocol3106);
            _uids.Add(DicomUID.PETCardiologyRadiopharmaceutical3107.UID, DicomUID.PETCardiologyRadiopharmaceutical3107);
            _uids.Add(DicomUID.NMPETProcedure3108.UID, DicomUID.NMPETProcedure3108);
            _uids.Add(DicomUID.NuclearCardiologyProtocol3110.UID, DicomUID.NuclearCardiologyProtocol3110);
            _uids.Add(DicomUID.NuclearCardiologyRadiopharmaceutical3111.UID, DicomUID.NuclearCardiologyRadiopharmaceutical3111);
            _uids.Add(DicomUID.AttenuationCorrection3112.UID, DicomUID.AttenuationCorrection3112);
            _uids.Add(DicomUID.PerfusionDefectType3113.UID, DicomUID.PerfusionDefectType3113);
            _uids.Add(DicomUID.StudyQuality3114.UID, DicomUID.StudyQuality3114);
            _uids.Add(DicomUID.StressImagingQualityIssue3115.UID, DicomUID.StressImagingQualityIssue3115);
            _uids.Add(DicomUID.NMExtracardiacFinding3116.UID, DicomUID.NMExtracardiacFinding3116);
            _uids.Add(DicomUID.AttenuationCorrectionMethod3117.UID, DicomUID.AttenuationCorrectionMethod3117);
            _uids.Add(DicomUID.LevelOfRisk3118.UID, DicomUID.LevelOfRisk3118);
            _uids.Add(DicomUID.LVFunction3119.UID, DicomUID.LVFunction3119);
            _uids.Add(DicomUID.PerfusionFinding3120.UID, DicomUID.PerfusionFinding3120);
            _uids.Add(DicomUID.PerfusionMorphology3121.UID, DicomUID.PerfusionMorphology3121);
            _uids.Add(DicomUID.VentricularEnlargement3122.UID, DicomUID.VentricularEnlargement3122);
            _uids.Add(DicomUID.StressTestProcedure3200.UID, DicomUID.StressTestProcedure3200);
            _uids.Add(DicomUID.IndicationsForStressTest3201.UID, DicomUID.IndicationsForStressTest3201);
            _uids.Add(DicomUID.ChestPain3202.UID, DicomUID.ChestPain3202);
            _uids.Add(DicomUID.ExerciserDevice3203.UID, DicomUID.ExerciserDevice3203);
            _uids.Add(DicomUID.StressAgent3204.UID, DicomUID.StressAgent3204);
            _uids.Add(DicomUID.IndicationsForPharmacologicalStressTest3205.UID, DicomUID.IndicationsForPharmacologicalStressTest3205);
            _uids.Add(DicomUID.NonInvasiveCardiacImagingProcedure3206.UID, DicomUID.NonInvasiveCardiacImagingProcedure3206);
            _uids.Add(DicomUID.ExerciseECGSummaryCode3208.UID, DicomUID.ExerciseECGSummaryCode3208);
            _uids.Add(DicomUID.StressImagingSummaryCode3209.UID, DicomUID.StressImagingSummaryCode3209);
            _uids.Add(DicomUID.SpeedOfResponse3210.UID, DicomUID.SpeedOfResponse3210);
            _uids.Add(DicomUID.BPResponse3211.UID, DicomUID.BPResponse3211);
            _uids.Add(DicomUID.TreadmillSpeed3212.UID, DicomUID.TreadmillSpeed3212);
            _uids.Add(DicomUID.StressHemodynamicFinding3213.UID, DicomUID.StressHemodynamicFinding3213);
            _uids.Add(DicomUID.PerfusionFindingMethod3215.UID, DicomUID.PerfusionFindingMethod3215);
            _uids.Add(DicomUID.ComparisonFinding3217.UID, DicomUID.ComparisonFinding3217);
            _uids.Add(DicomUID.StressSymptom3220.UID, DicomUID.StressSymptom3220);
            _uids.Add(DicomUID.StressTestTerminationReason3221.UID, DicomUID.StressTestTerminationReason3221);
            _uids.Add(DicomUID.QTcMeasurement3227.UID, DicomUID.QTcMeasurement3227);
            _uids.Add(DicomUID.ECGTimingMeasurement3228.UID, DicomUID.ECGTimingMeasurement3228);
            _uids.Add(DicomUID.ECGAxisMeasurement3229.UID, DicomUID.ECGAxisMeasurement3229);
            _uids.Add(DicomUID.ECGFinding3230.UID, DicomUID.ECGFinding3230);
            _uids.Add(DicomUID.STSegmentFinding3231.UID, DicomUID.STSegmentFinding3231);
            _uids.Add(DicomUID.STSegmentLocation3232.UID, DicomUID.STSegmentLocation3232);
            _uids.Add(DicomUID.STSegmentMorphology3233.UID, DicomUID.STSegmentMorphology3233);
            _uids.Add(DicomUID.EctopicBeatMorphology3234.UID, DicomUID.EctopicBeatMorphology3234);
            _uids.Add(DicomUID.PerfusionComparisonFinding3235.UID, DicomUID.PerfusionComparisonFinding3235);
            _uids.Add(DicomUID.ToleranceComparisonFinding3236.UID, DicomUID.ToleranceComparisonFinding3236);
            _uids.Add(DicomUID.WallMotionComparisonFinding3237.UID, DicomUID.WallMotionComparisonFinding3237);
            _uids.Add(DicomUID.StressScoringScale3238.UID, DicomUID.StressScoringScale3238);
            _uids.Add(DicomUID.PerceivedExertionScale3239.UID, DicomUID.PerceivedExertionScale3239);
            _uids.Add(DicomUID.VentricleIdentification3463.UID, DicomUID.VentricleIdentification3463);
            _uids.Add(DicomUID.ColonOverallAssessment6200.UID, DicomUID.ColonOverallAssessment6200);
            _uids.Add(DicomUID.ColonFindingOrFeature6201.UID, DicomUID.ColonFindingOrFeature6201);
            _uids.Add(DicomUID.ColonFindingOrFeatureModifier6202.UID, DicomUID.ColonFindingOrFeatureModifier6202);
            _uids.Add(DicomUID.ColonNonLesionObjectType6203.UID, DicomUID.ColonNonLesionObjectType6203);
            _uids.Add(DicomUID.AnatomicNonColonFinding6204.UID, DicomUID.AnatomicNonColonFinding6204);
            _uids.Add(DicomUID.ClockfaceLocationForColon6205.UID, DicomUID.ClockfaceLocationForColon6205);
            _uids.Add(DicomUID.RecumbentPatientOrientationForColon6206.UID, DicomUID.RecumbentPatientOrientationForColon6206);
            _uids.Add(DicomUID.ColonQuantitativeTemporalDifferenceType6207.UID, DicomUID.ColonQuantitativeTemporalDifferenceType6207);
            _uids.Add(DicomUID.ColonTypesOfQualityControlStandard6208.UID, DicomUID.ColonTypesOfQualityControlStandard6208);
            _uids.Add(DicomUID.ColonMorphologyDescriptor6209.UID, DicomUID.ColonMorphologyDescriptor6209);
            _uids.Add(DicomUID.LocationInIntestinalTract6210.UID, DicomUID.LocationInIntestinalTract6210);
            _uids.Add(DicomUID.ColonCADMaterialDescription6211.UID, DicomUID.ColonCADMaterialDescription6211);
            _uids.Add(DicomUID.CalculatedValueForColonFinding6212.UID, DicomUID.CalculatedValueForColonFinding6212);
            _uids.Add(DicomUID.OphthalmicHorizontalDirection4214.UID, DicomUID.OphthalmicHorizontalDirection4214);
            _uids.Add(DicomUID.OphthalmicVerticalDirection4215.UID, DicomUID.OphthalmicVerticalDirection4215);
            _uids.Add(DicomUID.OphthalmicVisualAcuityType4216.UID, DicomUID.OphthalmicVisualAcuityType4216);
            _uids.Add(DicomUID.ArterialPulseWaveform3004.UID, DicomUID.ArterialPulseWaveform3004);
            _uids.Add(DicomUID.RespirationWaveform3005.UID, DicomUID.RespirationWaveform3005);
            _uids.Add(DicomUID.UltrasoundContrastBolusAgent12030.UID, DicomUID.UltrasoundContrastBolusAgent12030);
            _uids.Add(DicomUID.ProtocolIntervalEvent12031.UID, DicomUID.ProtocolIntervalEvent12031);
            _uids.Add(DicomUID.TransducerScanPattern12032.UID, DicomUID.TransducerScanPattern12032);
            _uids.Add(DicomUID.UltrasoundTransducerGeometry12033.UID, DicomUID.UltrasoundTransducerGeometry12033);
            _uids.Add(DicomUID.UltrasoundTransducerBeamSteering12034.UID, DicomUID.UltrasoundTransducerBeamSteering12034);
            _uids.Add(DicomUID.UltrasoundTransducerApplication12035.UID, DicomUID.UltrasoundTransducerApplication12035);
            _uids.Add(DicomUID.InstanceAvailabilityStatus50.UID, DicomUID.InstanceAvailabilityStatus50);
            _uids.Add(DicomUID.ModalityPPSDiscontinuationReason9301.UID, DicomUID.ModalityPPSDiscontinuationReason9301);
            _uids.Add(DicomUID.MediaImportPPSDiscontinuationReason9302.UID, DicomUID.MediaImportPPSDiscontinuationReason9302);
            _uids.Add(DicomUID.DXAnatomyImagedForAnimal7482.UID, DicomUID.DXAnatomyImagedForAnimal7482);
            _uids.Add(DicomUID.CommonAnatomicRegionsForAnimal7483.UID, DicomUID.CommonAnatomicRegionsForAnimal7483);
            _uids.Add(DicomUID.DXViewForAnimal7484.UID, DicomUID.DXViewForAnimal7484);
            _uids.Add(DicomUID.InstitutionalDepartmentUnitService7030.UID, DicomUID.InstitutionalDepartmentUnitService7030);
            _uids.Add(DicomUID.PurposeOfReferenceToPredecessorReport7009.UID, DicomUID.PurposeOfReferenceToPredecessorReport7009);
            _uids.Add(DicomUID.VisualFixationQualityDuringAcquisition4220.UID, DicomUID.VisualFixationQualityDuringAcquisition4220);
            _uids.Add(DicomUID.VisualFixationQualityProblem4221.UID, DicomUID.VisualFixationQualityProblem4221);
            _uids.Add(DicomUID.OphthalmicMacularGridProblem4222.UID, DicomUID.OphthalmicMacularGridProblem4222);
            _uids.Add(DicomUID.Organization5002.UID, DicomUID.Organization5002);
            _uids.Add(DicomUID.MixedBreed7486.UID, DicomUID.MixedBreed7486);
            _uids.Add(DicomUID.BroselowLutenPediatricSizeCategory7040.UID, DicomUID.BroselowLutenPediatricSizeCategory7040);
            _uids.Add(DicomUID.CMDCTECCCalciumScoringPatientSizeCategory7042.UID, DicomUID.CMDCTECCCalciumScoringPatientSizeCategory7042);
            _uids.Add(DicomUID.CardiacUltrasoundReportTitle12245.UID, DicomUID.CardiacUltrasoundReportTitle12245);
            _uids.Add(DicomUID.CardiacUltrasoundIndicationForStudy12246.UID, DicomUID.CardiacUltrasoundIndicationForStudy12246);
            _uids.Add(DicomUID.PediatricFetalAndCongenitalCardiacSurgicalIntervention12247.UID, DicomUID.PediatricFetalAndCongenitalCardiacSurgicalIntervention12247);
            _uids.Add(DicomUID.CardiacUltrasoundSummaryCode12248.UID, DicomUID.CardiacUltrasoundSummaryCode12248);
            _uids.Add(DicomUID.CardiacUltrasoundFetalSummaryCode12249.UID, DicomUID.CardiacUltrasoundFetalSummaryCode12249);
            _uids.Add(DicomUID.CardiacUltrasoundCommonLinearMeasurement12250.UID, DicomUID.CardiacUltrasoundCommonLinearMeasurement12250);
            _uids.Add(DicomUID.CardiacUltrasoundLinearValveMeasurement12251.UID, DicomUID.CardiacUltrasoundLinearValveMeasurement12251);
            _uids.Add(DicomUID.CardiacUltrasoundCardiacFunction12252.UID, DicomUID.CardiacUltrasoundCardiacFunction12252);
            _uids.Add(DicomUID.CardiacUltrasoundAreaMeasurement12253.UID, DicomUID.CardiacUltrasoundAreaMeasurement12253);
            _uids.Add(DicomUID.CardiacUltrasoundHemodynamicMeasurement12254.UID, DicomUID.CardiacUltrasoundHemodynamicMeasurement12254);
            _uids.Add(DicomUID.CardiacUltrasoundMyocardiumMeasurement12255.UID, DicomUID.CardiacUltrasoundMyocardiumMeasurement12255);
            _uids.Add(DicomUID.CardiacUltrasoundLeftVentricleMeasurement12257.UID, DicomUID.CardiacUltrasoundLeftVentricleMeasurement12257);
            _uids.Add(DicomUID.CardiacUltrasoundRightVentricleMeasurement12258.UID, DicomUID.CardiacUltrasoundRightVentricleMeasurement12258);
            _uids.Add(DicomUID.CardiacUltrasoundVentriclesMeasurement12259.UID, DicomUID.CardiacUltrasoundVentriclesMeasurement12259);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryArteryMeasurement12260.UID, DicomUID.CardiacUltrasoundPulmonaryArteryMeasurement12260);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryVein12261.UID, DicomUID.CardiacUltrasoundPulmonaryVein12261);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryValveMeasurement12262.UID, DicomUID.CardiacUltrasoundPulmonaryValveMeasurement12262);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnPulmonaryMeasurement12263.UID, DicomUID.CardiacUltrasoundVenousReturnPulmonaryMeasurement12263);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnSystemicMeasurement12264.UID, DicomUID.CardiacUltrasoundVenousReturnSystemicMeasurement12264);
            _uids.Add(DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumMeasurement12265.UID, DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumMeasurement12265);
            _uids.Add(DicomUID.CardiacUltrasoundMitralValveMeasurement12266.UID, DicomUID.CardiacUltrasoundMitralValveMeasurement12266);
            _uids.Add(DicomUID.CardiacUltrasoundTricuspidValveMeasurement12267.UID, DicomUID.CardiacUltrasoundTricuspidValveMeasurement12267);
            _uids.Add(DicomUID.CardiacUltrasoundAtrioventricularValveMeasurement12268.UID, DicomUID.CardiacUltrasoundAtrioventricularValveMeasurement12268);
            _uids.Add(DicomUID.CardiacUltrasoundInterventricularSeptumMeasurement12269.UID, DicomUID.CardiacUltrasoundInterventricularSeptumMeasurement12269);
            _uids.Add(DicomUID.CardiacUltrasoundAorticValveMeasurement12270.UID, DicomUID.CardiacUltrasoundAorticValveMeasurement12270);
            _uids.Add(DicomUID.CardiacUltrasoundOutflowTractMeasurement12271.UID, DicomUID.CardiacUltrasoundOutflowTractMeasurement12271);
            _uids.Add(DicomUID.CardiacUltrasoundSemilunarValveAnnulateAndSinusMeasurement12272.UID, DicomUID.CardiacUltrasoundSemilunarValveAnnulateAndSinusMeasurement12272);
            _uids.Add(DicomUID.CardiacUltrasoundAorticSinotubularJunctionMeasurement12273.UID, DicomUID.CardiacUltrasoundAorticSinotubularJunctionMeasurement12273);
            _uids.Add(DicomUID.CardiacUltrasoundAortaMeasurement12274.UID, DicomUID.CardiacUltrasoundAortaMeasurement12274);
            _uids.Add(DicomUID.CardiacUltrasoundCoronaryArteryMeasurement12275.UID, DicomUID.CardiacUltrasoundCoronaryArteryMeasurement12275);
            _uids.Add(DicomUID.CardiacUltrasoundAortoPulmonaryConnectionMeasurement12276.UID, DicomUID.CardiacUltrasoundAortoPulmonaryConnectionMeasurement12276);
            _uids.Add(DicomUID.CardiacUltrasoundPericardiumAndPleuraMeasurement12277.UID, DicomUID.CardiacUltrasoundPericardiumAndPleuraMeasurement12277);
            _uids.Add(DicomUID.CardiacUltrasoundFetalGeneralMeasurement12279.UID, DicomUID.CardiacUltrasoundFetalGeneralMeasurement12279);
            _uids.Add(DicomUID.CardiacUltrasoundTargetSite12280.UID, DicomUID.CardiacUltrasoundTargetSite12280);
            _uids.Add(DicomUID.CardiacUltrasoundTargetSiteModifier12281.UID, DicomUID.CardiacUltrasoundTargetSiteModifier12281);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnSystemicFindingSite12282.UID, DicomUID.CardiacUltrasoundVenousReturnSystemicFindingSite12282);
            _uids.Add(DicomUID.CardiacUltrasoundVenousReturnPulmonaryFindingSite12283.UID, DicomUID.CardiacUltrasoundVenousReturnPulmonaryFindingSite12283);
            _uids.Add(DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumFindingSite12284.UID, DicomUID.CardiacUltrasoundAtriaAndAtrialSeptumFindingSite12284);
            _uids.Add(DicomUID.CardiacUltrasoundAtrioventricularValveFindingSite12285.UID, DicomUID.CardiacUltrasoundAtrioventricularValveFindingSite12285);
            _uids.Add(DicomUID.CardiacUltrasoundInterventricularSeptumFindingSite12286.UID, DicomUID.CardiacUltrasoundInterventricularSeptumFindingSite12286);
            _uids.Add(DicomUID.CardiacUltrasoundVentricleFindingSite12287.UID, DicomUID.CardiacUltrasoundVentricleFindingSite12287);
            _uids.Add(DicomUID.CardiacUltrasoundOutflowTractFindingSite12288.UID, DicomUID.CardiacUltrasoundOutflowTractFindingSite12288);
            _uids.Add(DicomUID.CardiacUltrasoundSemilunarValveAnnulusAndSinusFindingSite12289.UID, DicomUID.CardiacUltrasoundSemilunarValveAnnulusAndSinusFindingSite12289);
            _uids.Add(DicomUID.CardiacUltrasoundPulmonaryArteryFindingSite12290.UID, DicomUID.CardiacUltrasoundPulmonaryArteryFindingSite12290);
            _uids.Add(DicomUID.CardiacUltrasoundAortaFindingSite12291.UID, DicomUID.CardiacUltrasoundAortaFindingSite12291);
            _uids.Add(DicomUID.CardiacUltrasoundCoronaryArteryFindingSite12292.UID, DicomUID.CardiacUltrasoundCoronaryArteryFindingSite12292);
            _uids.Add(DicomUID.CardiacUltrasoundAortopulmonaryConnectionFindingSite12293.UID, DicomUID.CardiacUltrasoundAortopulmonaryConnectionFindingSite12293);
            _uids.Add(DicomUID.CardiacUltrasoundPericardiumAndPleuraFindingSite12294.UID, DicomUID.CardiacUltrasoundPericardiumAndPleuraFindingSite12294);
            _uids.Add(DicomUID.OphthalmicUltrasoundAxialMeasurementsType4230.UID, DicomUID.OphthalmicUltrasoundAxialMeasurementsType4230);
            _uids.Add(DicomUID.LensStatus4231.UID, DicomUID.LensStatus4231);
            _uids.Add(DicomUID.VitreousStatus4232.UID, DicomUID.VitreousStatus4232);
            _uids.Add(DicomUID.OphthalmicAxialLengthMeasurementsSegmentName4233.UID, DicomUID.OphthalmicAxialLengthMeasurementsSegmentName4233);
            _uids.Add(DicomUID.RefractiveSurgeryType4234.UID, DicomUID.RefractiveSurgeryType4234);
            _uids.Add(DicomUID.KeratometryDescriptor4235.UID, DicomUID.KeratometryDescriptor4235);
            _uids.Add(DicomUID.IOLCalculationFormula4236.UID, DicomUID.IOLCalculationFormula4236);
            _uids.Add(DicomUID.LensConstantType4237.UID, DicomUID.LensConstantType4237);
            _uids.Add(DicomUID.RefractiveErrorType4238.UID, DicomUID.RefractiveErrorType4238);
            _uids.Add(DicomUID.AnteriorChamberDepthDefinition4239.UID, DicomUID.AnteriorChamberDepthDefinition4239);
            _uids.Add(DicomUID.OphthalmicMeasurementOrCalculationDataSource4240.UID, DicomUID.OphthalmicMeasurementOrCalculationDataSource4240);
            _uids.Add(DicomUID.OphthalmicAxialLengthSelectionMethod4241.UID, DicomUID.OphthalmicAxialLengthSelectionMethod4241);
            _uids.Add(DicomUID.OphthalmicQualityMetricType4243.UID, DicomUID.OphthalmicQualityMetricType4243);
            _uids.Add(DicomUID.OphthalmicAgentConcentrationUnit4244.UID, DicomUID.OphthalmicAgentConcentrationUnit4244);
            _uids.Add(DicomUID.FunctionalConditionPresentDuringAcquisition91.UID, DicomUID.FunctionalConditionPresentDuringAcquisition91);
            _uids.Add(DicomUID.JointPositionDuringAcquisition92.UID, DicomUID.JointPositionDuringAcquisition92);
            _uids.Add(DicomUID.JointPositioningMethod93.UID, DicomUID.JointPositioningMethod93);
            _uids.Add(DicomUID.PhysicalForceAppliedDuringAcquisition94.UID, DicomUID.PhysicalForceAppliedDuringAcquisition94);
            _uids.Add(DicomUID.ECGControlNumericVariable3690.UID, DicomUID.ECGControlNumericVariable3690);
            _uids.Add(DicomUID.ECGControlTextVariable3691.UID, DicomUID.ECGControlTextVariable3691);
            _uids.Add(DicomUID.WholeSlideMicroscopyImageReferencedImagePurposeOfReference8120.UID, DicomUID.WholeSlideMicroscopyImageReferencedImagePurposeOfReference8120);
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
            _uids.Add(DicomUID.VisualFieldStaticPerimetryTestPattern4250.UID, DicomUID.VisualFieldStaticPerimetryTestPattern4250);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryTestStrategy4251.UID, DicomUID.VisualFieldStaticPerimetryTestStrategy4251);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryScreeningTestMode4252.UID, DicomUID.VisualFieldStaticPerimetryScreeningTestMode4252);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryFixationStrategy4253.UID, DicomUID.VisualFieldStaticPerimetryFixationStrategy4253);
            _uids.Add(DicomUID.VisualFieldStaticPerimetryTestAnalysisResult4254.UID, DicomUID.VisualFieldStaticPerimetryTestAnalysisResult4254);
            _uids.Add(DicomUID.VisualFieldIlluminationColor4255.UID, DicomUID.VisualFieldIlluminationColor4255);
            _uids.Add(DicomUID.VisualFieldProcedureModifier4256.UID, DicomUID.VisualFieldProcedureModifier4256);
            _uids.Add(DicomUID.VisualFieldGlobalIndexName4257.UID, DicomUID.VisualFieldGlobalIndexName4257);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelComponentSemantic7180.UID, DicomUID.AbstractMultiDimensionalImageModelComponentSemantic7180);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelComponentUnit7181.UID, DicomUID.AbstractMultiDimensionalImageModelComponentUnit7181);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelDimensionSemantic7182.UID, DicomUID.AbstractMultiDimensionalImageModelDimensionSemantic7182);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelDimensionUnit7183.UID, DicomUID.AbstractMultiDimensionalImageModelDimensionUnit7183);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelAxisDirection7184.UID, DicomUID.AbstractMultiDimensionalImageModelAxisDirection7184);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelAxisOrientation7185.UID, DicomUID.AbstractMultiDimensionalImageModelAxisOrientation7185);
            _uids.Add(DicomUID.AbstractMultiDimensionalImageModelQualitativeDimensionSampleSemantic7186.UID, DicomUID.AbstractMultiDimensionalImageModelQualitativeDimensionSampleSemantic7186);
            _uids.Add(DicomUID.PlanningMethod7320.UID, DicomUID.PlanningMethod7320);
            _uids.Add(DicomUID.DeIdentificationMethod7050.UID, DicomUID.DeIdentificationMethod7050);
            _uids.Add(DicomUID.MeasurementOrientation12118.UID, DicomUID.MeasurementOrientation12118);
            _uids.Add(DicomUID.ECGGlobalWaveformDuration3689.UID, DicomUID.ECGGlobalWaveformDuration3689);
            _uids.Add(DicomUID.ICD3692.UID, DicomUID.ICD3692);
            _uids.Add(DicomUID.RadiotherapyGeneralWorkitemDefinition9241.UID, DicomUID.RadiotherapyGeneralWorkitemDefinition9241);
            _uids.Add(DicomUID.RadiotherapyAcquisitionWorkitemDefinition9242.UID, DicomUID.RadiotherapyAcquisitionWorkitemDefinition9242);
            _uids.Add(DicomUID.RadiotherapyRegistrationWorkitemDefinition9243.UID, DicomUID.RadiotherapyRegistrationWorkitemDefinition9243);
            _uids.Add(DicomUID.ContrastBolusSubstance3850.UID, DicomUID.ContrastBolusSubstance3850);
            _uids.Add(DicomUID.LabelType10022.UID, DicomUID.LabelType10022);
            _uids.Add(DicomUID.OphthalmicMappingUnitForRealWorldValueMapping4260.UID, DicomUID.OphthalmicMappingUnitForRealWorldValueMapping4260);
            _uids.Add(DicomUID.OphthalmicMappingAcquisitionMethod4261.UID, DicomUID.OphthalmicMappingAcquisitionMethod4261);
            _uids.Add(DicomUID.RetinalThicknessDefinition4262.UID, DicomUID.RetinalThicknessDefinition4262);
            _uids.Add(DicomUID.OphthalmicThicknessMapValueType4263.UID, DicomUID.OphthalmicThicknessMapValueType4263);
            _uids.Add(DicomUID.OphthalmicMapPurposeOfReference4264.UID, DicomUID.OphthalmicMapPurposeOfReference4264);
            _uids.Add(DicomUID.OphthalmicThicknessDeviationCategory4265.UID, DicomUID.OphthalmicThicknessDeviationCategory4265);
            _uids.Add(DicomUID.OphthalmicAnatomicStructureReferencePoint4266.UID, DicomUID.OphthalmicAnatomicStructureReferencePoint4266);
            _uids.Add(DicomUID.CardiacSynchronizationTechnique3104.UID, DicomUID.CardiacSynchronizationTechnique3104);
            _uids.Add(DicomUID.StainingProtocol8130.UID, DicomUID.StainingProtocol8130);
            _uids.Add(DicomUID.SizeSpecificDoseEstimationMethodForCT10023.UID, DicomUID.SizeSpecificDoseEstimationMethodForCT10023);
            _uids.Add(DicomUID.PathologyImagingProtocol8131.UID, DicomUID.PathologyImagingProtocol8131);
            _uids.Add(DicomUID.MagnificationSelection8132.UID, DicomUID.MagnificationSelection8132);
            _uids.Add(DicomUID.TissueSelection8133.UID, DicomUID.TissueSelection8133);
            _uids.Add(DicomUID.GeneralRegionOfInterestMeasurementModifier7464.UID, DicomUID.GeneralRegionOfInterestMeasurementModifier7464);
            _uids.Add(DicomUID.MeasurementDerivedFromMultipleROIMeasurements7465.UID, DicomUID.MeasurementDerivedFromMultipleROIMeasurements7465);
            _uids.Add(DicomUID.SurfaceScanAcquisitionType8201.UID, DicomUID.SurfaceScanAcquisitionType8201);
            _uids.Add(DicomUID.SurfaceScanModeType8202.UID, DicomUID.SurfaceScanModeType8202);
            _uids.Add(DicomUID.SurfaceScanRegistrationMethodType8203.UID, DicomUID.SurfaceScanRegistrationMethodType8203);
            _uids.Add(DicomUID.BasicCardiacView27.UID, DicomUID.BasicCardiacView27);
            _uids.Add(DicomUID.CTReconstructionAlgorithm10033.UID, DicomUID.CTReconstructionAlgorithm10033);
            _uids.Add(DicomUID.DetectorType10030.UID, DicomUID.DetectorType10030);
            _uids.Add(DicomUID.CRDRMechanicalConfiguration10031.UID, DicomUID.CRDRMechanicalConfiguration10031);
            _uids.Add(DicomUID.ProjectionXRayAcquisitionDeviceType10032.UID, DicomUID.ProjectionXRayAcquisitionDeviceType10032);
            _uids.Add(DicomUID.AbstractSegmentationType7165.UID, DicomUID.AbstractSegmentationType7165);
            _uids.Add(DicomUID.CommonTissueSegmentationType7166.UID, DicomUID.CommonTissueSegmentationType7166);
            _uids.Add(DicomUID.PeripheralNervousSystemSegmentationType7167.UID, DicomUID.PeripheralNervousSystemSegmentationType7167);
            _uids.Add(DicomUID.CornealTopographyMappingUnitForRealWorldValueMapping4267.UID, DicomUID.CornealTopographyMappingUnitForRealWorldValueMapping4267);
            _uids.Add(DicomUID.CornealTopographyMapValueType4268.UID, DicomUID.CornealTopographyMapValueType4268);
            _uids.Add(DicomUID.BrainStructureForVolumetricMeasurement7140.UID, DicomUID.BrainStructureForVolumetricMeasurement7140);
            _uids.Add(DicomUID.RTDoseDerivation7220.UID, DicomUID.RTDoseDerivation7220);
            _uids.Add(DicomUID.RTDosePurposeOfReference7221.UID, DicomUID.RTDosePurposeOfReference7221);
            _uids.Add(DicomUID.SpectroscopyPurposeOfReference7215.UID, DicomUID.SpectroscopyPurposeOfReference7215);
            _uids.Add(DicomUID.ScheduledProcessingParameterConceptCodesForRTTreatment9250.UID, DicomUID.ScheduledProcessingParameterConceptCodesForRTTreatment9250);
            _uids.Add(DicomUID.RadiopharmaceuticalOrganDoseReferenceAuthority10040.UID, DicomUID.RadiopharmaceuticalOrganDoseReferenceAuthority10040);
            _uids.Add(DicomUID.SourceOfRadioisotopeActivityInformation10041.UID, DicomUID.SourceOfRadioisotopeActivityInformation10041);
            _uids.Add(DicomUID.IntravenousExtravasationSymptom10043.UID, DicomUID.IntravenousExtravasationSymptom10043);
            _uids.Add(DicomUID.RadiosensitiveOrgan10044.UID, DicomUID.RadiosensitiveOrgan10044);
            _uids.Add(DicomUID.RadiopharmaceuticalPatientState10045.UID, DicomUID.RadiopharmaceuticalPatientState10045);
            _uids.Add(DicomUID.GFRMeasurement10046.UID, DicomUID.GFRMeasurement10046);
            _uids.Add(DicomUID.GFRMeasurementMethod10047.UID, DicomUID.GFRMeasurementMethod10047);
            _uids.Add(DicomUID.VisualEvaluationMethod8300.UID, DicomUID.VisualEvaluationMethod8300);
            _uids.Add(DicomUID.TestPatternCode8301.UID, DicomUID.TestPatternCode8301);
            _uids.Add(DicomUID.MeasurementPatternCode8302.UID, DicomUID.MeasurementPatternCode8302);
            _uids.Add(DicomUID.DisplayDeviceType8303.UID, DicomUID.DisplayDeviceType8303);
            _uids.Add(DicomUID.SUVUnit85.UID, DicomUID.SUVUnit85);
            _uids.Add(DicomUID.T1MeasurementMethod4100.UID, DicomUID.T1MeasurementMethod4100);
            _uids.Add(DicomUID.TracerKineticModel4101.UID, DicomUID.TracerKineticModel4101);
            _uids.Add(DicomUID.PerfusionMeasurementMethod4102.UID, DicomUID.PerfusionMeasurementMethod4102);
            _uids.Add(DicomUID.ArterialInputFunctionMeasurementMethod4103.UID, DicomUID.ArterialInputFunctionMeasurementMethod4103);
            _uids.Add(DicomUID.BolusArrivalTimeDerivationMethod4104.UID, DicomUID.BolusArrivalTimeDerivationMethod4104);
            _uids.Add(DicomUID.PerfusionAnalysisMethod4105.UID, DicomUID.PerfusionAnalysisMethod4105);
            _uids.Add(DicomUID.QuantitativeMethodUsedForPerfusionAndTracerKineticModel4106.UID, DicomUID.QuantitativeMethodUsedForPerfusionAndTracerKineticModel4106);
            _uids.Add(DicomUID.TracerKineticModelParameter4107.UID, DicomUID.TracerKineticModelParameter4107);
            _uids.Add(DicomUID.PerfusionModelParameter4108.UID, DicomUID.PerfusionModelParameter4108);
            _uids.Add(DicomUID.ModelIndependentDynamicContrastAnalysisParameter4109.UID, DicomUID.ModelIndependentDynamicContrastAnalysisParameter4109);
            _uids.Add(DicomUID.TracerKineticModelingCovariate4110.UID, DicomUID.TracerKineticModelingCovariate4110);
            _uids.Add(DicomUID.ContrastCharacteristic4111.UID, DicomUID.ContrastCharacteristic4111);
            _uids.Add(DicomUID.MeasurementReportDocumentTitle7021.UID, DicomUID.MeasurementReportDocumentTitle7021);
            _uids.Add(DicomUID.QuantitativeDiagnosticImagingProcedure100.UID, DicomUID.QuantitativeDiagnosticImagingProcedure100);
            _uids.Add(DicomUID.PETRegionOfInterestMeasurement7466.UID, DicomUID.PETRegionOfInterestMeasurement7466);
            _uids.Add(DicomUID.GrayLevelCoOccurrenceMatrixMeasurement7467.UID, DicomUID.GrayLevelCoOccurrenceMatrixMeasurement7467);
            _uids.Add(DicomUID.TextureMeasurement7468.UID, DicomUID.TextureMeasurement7468);
            _uids.Add(DicomUID.TimePointType6146.UID, DicomUID.TimePointType6146);
            _uids.Add(DicomUID.GenericIntensityAndSizeMeasurement7469.UID, DicomUID.GenericIntensityAndSizeMeasurement7469);
            _uids.Add(DicomUID.ResponseCriteria6147.UID, DicomUID.ResponseCriteria6147);
            _uids.Add(DicomUID.FetalBiometryAnatomicSite12020.UID, DicomUID.FetalBiometryAnatomicSite12020);
            _uids.Add(DicomUID.FetalLongBoneAnatomicSite12021.UID, DicomUID.FetalLongBoneAnatomicSite12021);
            _uids.Add(DicomUID.FetalCraniumAnatomicSite12022.UID, DicomUID.FetalCraniumAnatomicSite12022);
            _uids.Add(DicomUID.PelvisAndUterusAnatomicSite12023.UID, DicomUID.PelvisAndUterusAnatomicSite12023);
            _uids.Add(DicomUID.ParametricMapDerivationImagePurposeOfReference7222.UID, DicomUID.ParametricMapDerivationImagePurposeOfReference7222);
            _uids.Add(DicomUID.PhysicalQuantityDescriptor9000.UID, DicomUID.PhysicalQuantityDescriptor9000);
            _uids.Add(DicomUID.LymphNodeAnatomicSite7600.UID, DicomUID.LymphNodeAnatomicSite7600);
            _uids.Add(DicomUID.HeadAndNeckCancerAnatomicSite7601.UID, DicomUID.HeadAndNeckCancerAnatomicSite7601);
            _uids.Add(DicomUID.FiberTractInBrainstem7701.UID, DicomUID.FiberTractInBrainstem7701);
            _uids.Add(DicomUID.ProjectionAndThalamicFiber7702.UID, DicomUID.ProjectionAndThalamicFiber7702);
            _uids.Add(DicomUID.AssociationFiber7703.UID, DicomUID.AssociationFiber7703);
            _uids.Add(DicomUID.LimbicSystemTract7704.UID, DicomUID.LimbicSystemTract7704);
            _uids.Add(DicomUID.CommissuralFiber7705.UID, DicomUID.CommissuralFiber7705);
            _uids.Add(DicomUID.CranialNerve7706.UID, DicomUID.CranialNerve7706);
            _uids.Add(DicomUID.SpinalCordFiber7707.UID, DicomUID.SpinalCordFiber7707);
            _uids.Add(DicomUID.TractographyAnatomicSite7710.UID, DicomUID.TractographyAnatomicSite7710);
            _uids.Add(DicomUID.PrimaryAnatomicStructureForIntraOralRadiographySupernumeraryDentitionDesignationOfTeeth4025.UID, DicomUID.PrimaryAnatomicStructureForIntraOralRadiographySupernumeraryDentitionDesignationOfTeeth4025);
            _uids.Add(DicomUID.PrimaryAnatomicStructureForIntraOralAndCraniofacialRadiographyTeeth4026.UID, DicomUID.PrimaryAnatomicStructureForIntraOralAndCraniofacialRadiographyTeeth4026);
            _uids.Add(DicomUID.IEC61217DevicePositionParameter9401.UID, DicomUID.IEC61217DevicePositionParameter9401);
            _uids.Add(DicomUID.IEC61217GantryPositionParameter9402.UID, DicomUID.IEC61217GantryPositionParameter9402);
            _uids.Add(DicomUID.IEC61217PatientSupportPositionParameter9403.UID, DicomUID.IEC61217PatientSupportPositionParameter9403);
            _uids.Add(DicomUID.ActionableFindingClassification7035.UID, DicomUID.ActionableFindingClassification7035);
            _uids.Add(DicomUID.ImageQualityAssessment7036.UID, DicomUID.ImageQualityAssessment7036);
            _uids.Add(DicomUID.SummaryRadiationExposureQuantity10050.UID, DicomUID.SummaryRadiationExposureQuantity10050);
            _uids.Add(DicomUID.WideFieldOphthalmicPhotographyTransformationMethod4245.UID, DicomUID.WideFieldOphthalmicPhotographyTransformationMethod4245);
            _uids.Add(DicomUID.PETUnit84.UID, DicomUID.PETUnit84);
            _uids.Add(DicomUID.ImplantMaterial7300.UID, DicomUID.ImplantMaterial7300);
            _uids.Add(DicomUID.InterventionType7301.UID, DicomUID.InterventionType7301);
            _uids.Add(DicomUID.ImplantTemplateViewOrientation7302.UID, DicomUID.ImplantTemplateViewOrientation7302);
            _uids.Add(DicomUID.ImplantTemplateModifiedViewOrientation7303.UID, DicomUID.ImplantTemplateModifiedViewOrientation7303);
            _uids.Add(DicomUID.ImplantTargetAnatomy7304.UID, DicomUID.ImplantTargetAnatomy7304);
            _uids.Add(DicomUID.ImplantPlanningLandmark7305.UID, DicomUID.ImplantPlanningLandmark7305);
            _uids.Add(DicomUID.HumanHipImplantPlanningLandmark7306.UID, DicomUID.HumanHipImplantPlanningLandmark7306);
            _uids.Add(DicomUID.ImplantComponentType7307.UID, DicomUID.ImplantComponentType7307);
            _uids.Add(DicomUID.HumanHipImplantComponentType7308.UID, DicomUID.HumanHipImplantComponentType7308);
            _uids.Add(DicomUID.HumanTraumaImplantComponentType7309.UID, DicomUID.HumanTraumaImplantComponentType7309);
            _uids.Add(DicomUID.ImplantFixationMethod7310.UID, DicomUID.ImplantFixationMethod7310);
            _uids.Add(DicomUID.DeviceParticipatingRole7445.UID, DicomUID.DeviceParticipatingRole7445);
            _uids.Add(DicomUID.ContainerType8101.UID, DicomUID.ContainerType8101);
            _uids.Add(DicomUID.ContainerComponentType8102.UID, DicomUID.ContainerComponentType8102);
            _uids.Add(DicomUID.AnatomicPathologySpecimenType8103.UID, DicomUID.AnatomicPathologySpecimenType8103);
            _uids.Add(DicomUID.BreastTissueSpecimenType8104.UID, DicomUID.BreastTissueSpecimenType8104);
            _uids.Add(DicomUID.SpecimenCollectionProcedure8109.UID, DicomUID.SpecimenCollectionProcedure8109);
            _uids.Add(DicomUID.SpecimenSamplingProcedure8110.UID, DicomUID.SpecimenSamplingProcedure8110);
            _uids.Add(DicomUID.SpecimenPreparationProcedure8111.UID, DicomUID.SpecimenPreparationProcedure8111);
            _uids.Add(DicomUID.SpecimenStain8112.UID, DicomUID.SpecimenStain8112);
            _uids.Add(DicomUID.SpecimenPreparationStep8113.UID, DicomUID.SpecimenPreparationStep8113);
            _uids.Add(DicomUID.SpecimenFixative8114.UID, DicomUID.SpecimenFixative8114);
            _uids.Add(DicomUID.SpecimenEmbeddingMedia8115.UID, DicomUID.SpecimenEmbeddingMedia8115);
            _uids.Add(DicomUID.SourceOfProjectionXRayDoseInformation10020.UID, DicomUID.SourceOfProjectionXRayDoseInformation10020);
            _uids.Add(DicomUID.SourceOfCTDoseInformation10021.UID, DicomUID.SourceOfCTDoseInformation10021);
            _uids.Add(DicomUID.RadiationDoseReferencePoint10025.UID, DicomUID.RadiationDoseReferencePoint10025);
            _uids.Add(DicomUID.VolumetricViewDescription501.UID, DicomUID.VolumetricViewDescription501);
            _uids.Add(DicomUID.VolumetricViewModifier502.UID, DicomUID.VolumetricViewModifier502);
            _uids.Add(DicomUID.DiffusionAcquisitionValueType7260.UID, DicomUID.DiffusionAcquisitionValueType7260);
            _uids.Add(DicomUID.DiffusionModelValueType7261.UID, DicomUID.DiffusionModelValueType7261);
            _uids.Add(DicomUID.DiffusionTractographyAlgorithmFamily7262.UID, DicomUID.DiffusionTractographyAlgorithmFamily7262);
            _uids.Add(DicomUID.DiffusionTractographyMeasurementType7263.UID, DicomUID.DiffusionTractographyMeasurementType7263);
            _uids.Add(DicomUID.ResearchAnimalSourceRegistry7490.UID, DicomUID.ResearchAnimalSourceRegistry7490);
            _uids.Add(DicomUID.YesNoOnly231.UID, DicomUID.YesNoOnly231);
            _uids.Add(DicomUID.BiosafetyLevel601.UID, DicomUID.BiosafetyLevel601);
            _uids.Add(DicomUID.BiosafetyControlReason602.UID, DicomUID.BiosafetyControlReason602);
            _uids.Add(DicomUID.SexMaleFemaleOrBoth7457.UID, DicomUID.SexMaleFemaleOrBoth7457);
            _uids.Add(DicomUID.AnimalRoomType603.UID, DicomUID.AnimalRoomType603);
            _uids.Add(DicomUID.DeviceReuse604.UID, DicomUID.DeviceReuse604);
            _uids.Add(DicomUID.AnimalBeddingMaterial605.UID, DicomUID.AnimalBeddingMaterial605);
            _uids.Add(DicomUID.AnimalShelterType606.UID, DicomUID.AnimalShelterType606);
            _uids.Add(DicomUID.AnimalFeedType607.UID, DicomUID.AnimalFeedType607);
            _uids.Add(DicomUID.AnimalFeedSource608.UID, DicomUID.AnimalFeedSource608);
            _uids.Add(DicomUID.AnimalFeedingMethod609.UID, DicomUID.AnimalFeedingMethod609);
            _uids.Add(DicomUID.WaterType610.UID, DicomUID.WaterType610);
            _uids.Add(DicomUID.AnesthesiaCategoryCodeTypeForSmallAnimalAnesthesia611.UID, DicomUID.AnesthesiaCategoryCodeTypeForSmallAnimalAnesthesia611);
            _uids.Add(DicomUID.AnesthesiaCategoryCodeTypeFromAnesthesiaQualityInitiative612.UID, DicomUID.AnesthesiaCategoryCodeTypeFromAnesthesiaQualityInitiative612);
            _uids.Add(DicomUID.AnesthesiaInductionCodeTypeForSmallAnimalAnesthesia613.UID, DicomUID.AnesthesiaInductionCodeTypeForSmallAnimalAnesthesia613);
            _uids.Add(DicomUID.AnesthesiaInductionCodeTypeFromAnesthesiaQualityInitiative614.UID, DicomUID.AnesthesiaInductionCodeTypeFromAnesthesiaQualityInitiative614);
            _uids.Add(DicomUID.AnesthesiaMaintenanceCodeTypeForSmallAnimalAnesthesia615.UID, DicomUID.AnesthesiaMaintenanceCodeTypeForSmallAnimalAnesthesia615);
            _uids.Add(DicomUID.AnesthesiaMaintenanceCodeTypeFromAnesthesiaQualityInitiative616.UID, DicomUID.AnesthesiaMaintenanceCodeTypeFromAnesthesiaQualityInitiative616);
            _uids.Add(DicomUID.AirwayManagementMethodCodeTypeForSmallAnimalAnesthesia617.UID, DicomUID.AirwayManagementMethodCodeTypeForSmallAnimalAnesthesia617);
            _uids.Add(DicomUID.AirwayManagementMethodCodeTypeFromAnesthesiaQualityInitiative618.UID, DicomUID.AirwayManagementMethodCodeTypeFromAnesthesiaQualityInitiative618);
            _uids.Add(DicomUID.AirwayManagementSubMethodCodeTypeForSmallAnimalAnesthesia619.UID, DicomUID.AirwayManagementSubMethodCodeTypeForSmallAnimalAnesthesia619);
            _uids.Add(DicomUID.AirwayManagementSubMethodCodeTypeFromAnesthesiaQualityInitiative620.UID, DicomUID.AirwayManagementSubMethodCodeTypeFromAnesthesiaQualityInitiative620);
            _uids.Add(DicomUID.MedicationTypeForSmallAnimalAnesthesia621.UID, DicomUID.MedicationTypeForSmallAnimalAnesthesia621);
            _uids.Add(DicomUID.MedicationTypeCodeTypeFromAnesthesiaQualityInitiative622.UID, DicomUID.MedicationTypeCodeTypeFromAnesthesiaQualityInitiative622);
            _uids.Add(DicomUID.MedicationForSmallAnimalAnesthesia623.UID, DicomUID.MedicationForSmallAnimalAnesthesia623);
            _uids.Add(DicomUID.InhalationalAnesthesiaAgentForSmallAnimalAnesthesia624.UID, DicomUID.InhalationalAnesthesiaAgentForSmallAnimalAnesthesia624);
            _uids.Add(DicomUID.InjectableAnesthesiaAgentForSmallAnimalAnesthesia625.UID, DicomUID.InjectableAnesthesiaAgentForSmallAnimalAnesthesia625);
            _uids.Add(DicomUID.PremedicationAgentForSmallAnimalAnesthesia626.UID, DicomUID.PremedicationAgentForSmallAnimalAnesthesia626);
            _uids.Add(DicomUID.NeuromuscularBlockingAgentForSmallAnimalAnesthesia627.UID, DicomUID.NeuromuscularBlockingAgentForSmallAnimalAnesthesia627);
            _uids.Add(DicomUID.AncillaryMedicationsForSmallAnimalAnesthesia628.UID, DicomUID.AncillaryMedicationsForSmallAnimalAnesthesia628);
            _uids.Add(DicomUID.CarrierGasesForSmallAnimalAnesthesia629.UID, DicomUID.CarrierGasesForSmallAnimalAnesthesia629);
            _uids.Add(DicomUID.LocalAnestheticsForSmallAnimalAnesthesia630.UID, DicomUID.LocalAnestheticsForSmallAnimalAnesthesia630);
            _uids.Add(DicomUID.ProcedurePhaseRequiringAnesthesia631.UID, DicomUID.ProcedurePhaseRequiringAnesthesia631);
            _uids.Add(DicomUID.SurgicalProcedurePhaseRequiringAnesthesia632.UID, DicomUID.SurgicalProcedurePhaseRequiringAnesthesia632);
            _uids.Add(DicomUID.PhaseOfImagingProcedureRequiringAnesthesia633RETIRED.UID, DicomUID.PhaseOfImagingProcedureRequiringAnesthesia633RETIRED);
            _uids.Add(DicomUID.AnimalHandlingPhase634.UID, DicomUID.AnimalHandlingPhase634);
            _uids.Add(DicomUID.HeatingMethod635.UID, DicomUID.HeatingMethod635);
            _uids.Add(DicomUID.TemperatureSensorDeviceComponentTypeForSmallAnimalProcedure636.UID, DicomUID.TemperatureSensorDeviceComponentTypeForSmallAnimalProcedure636);
            _uids.Add(DicomUID.ExogenousSubstanceType637.UID, DicomUID.ExogenousSubstanceType637);
            _uids.Add(DicomUID.ExogenousSubstance638.UID, DicomUID.ExogenousSubstance638);
            _uids.Add(DicomUID.TumorGraftHistologicType639.UID, DicomUID.TumorGraftHistologicType639);
            _uids.Add(DicomUID.Fibril640.UID, DicomUID.Fibril640);
            _uids.Add(DicomUID.Virus641.UID, DicomUID.Virus641);
            _uids.Add(DicomUID.Cytokine642.UID, DicomUID.Cytokine642);
            _uids.Add(DicomUID.Toxin643.UID, DicomUID.Toxin643);
            _uids.Add(DicomUID.ExogenousSubstanceAdministrationSite644.UID, DicomUID.ExogenousSubstanceAdministrationSite644);
            _uids.Add(DicomUID.ExogenousSubstanceOriginTissue645.UID, DicomUID.ExogenousSubstanceOriginTissue645);
            _uids.Add(DicomUID.PreclinicalSmallAnimalImagingProcedure646.UID, DicomUID.PreclinicalSmallAnimalImagingProcedure646);
            _uids.Add(DicomUID.PositionReferenceIndicatorForFrameOfReference647.UID, DicomUID.PositionReferenceIndicatorForFrameOfReference647);
            _uids.Add(DicomUID.PresentAbsentOnly241.UID, DicomUID.PresentAbsentOnly241);
            _uids.Add(DicomUID.WaterEquivalentDiameterMethod10024.UID, DicomUID.WaterEquivalentDiameterMethod10024);
            _uids.Add(DicomUID.RadiotherapyPurposeOfReference7022.UID, DicomUID.RadiotherapyPurposeOfReference7022);
            _uids.Add(DicomUID.ContentAssessmentType701.UID, DicomUID.ContentAssessmentType701);
            _uids.Add(DicomUID.RTContentAssessmentType702.UID, DicomUID.RTContentAssessmentType702);
            _uids.Add(DicomUID.AssessmentBasis703.UID, DicomUID.AssessmentBasis703);
            _uids.Add(DicomUID.ReaderSpecialty7449.UID, DicomUID.ReaderSpecialty7449);
            _uids.Add(DicomUID.RequestedReportType9233.UID, DicomUID.RequestedReportType9233);
            _uids.Add(DicomUID.CTTransversePlaneReferenceBasis1000.UID, DicomUID.CTTransversePlaneReferenceBasis1000);
            _uids.Add(DicomUID.AnatomicalReferenceBasis1001.UID, DicomUID.AnatomicalReferenceBasis1001);
            _uids.Add(DicomUID.AnatomicalReferenceBasisHead1002.UID, DicomUID.AnatomicalReferenceBasisHead1002);
            _uids.Add(DicomUID.AnatomicalReferenceBasisSpine1003.UID, DicomUID.AnatomicalReferenceBasisSpine1003);
            _uids.Add(DicomUID.AnatomicalReferenceBasisChest1004.UID, DicomUID.AnatomicalReferenceBasisChest1004);
            _uids.Add(DicomUID.AnatomicalReferenceBasisAbdomenPelvis1005.UID, DicomUID.AnatomicalReferenceBasisAbdomenPelvis1005);
            _uids.Add(DicomUID.AnatomicalReferenceBasisExtremity1006.UID, DicomUID.AnatomicalReferenceBasisExtremity1006);
            _uids.Add(DicomUID.ReferenceGeometryPlane1010.UID, DicomUID.ReferenceGeometryPlane1010);
            _uids.Add(DicomUID.ReferenceGeometryPoint1011.UID, DicomUID.ReferenceGeometryPoint1011);
            _uids.Add(DicomUID.PatientAlignmentMethod1015.UID, DicomUID.PatientAlignmentMethod1015);
            _uids.Add(DicomUID.ContraindicationsForCTImaging1200.UID, DicomUID.ContraindicationsForCTImaging1200);
            _uids.Add(DicomUID.FiducialCategory7110.UID, DicomUID.FiducialCategory7110);
            _uids.Add(DicomUID.Fiducial7111.UID, DicomUID.Fiducial7111);
            _uids.Add(DicomUID.NonImageSourceInstancePurposeOfReference7013.UID, DicomUID.NonImageSourceInstancePurposeOfReference7013);
            _uids.Add(DicomUID.RTProcessOutput7023.UID, DicomUID.RTProcessOutput7023);
            _uids.Add(DicomUID.RTProcessInput7024.UID, DicomUID.RTProcessInput7024);
            _uids.Add(DicomUID.RTProcessInputUsed7025.UID, DicomUID.RTProcessInputUsed7025);
            _uids.Add(DicomUID.ProstateAnatomy6300.UID, DicomUID.ProstateAnatomy6300);
            _uids.Add(DicomUID.ProstateSectorAnatomyFromPIRADSV26301.UID, DicomUID.ProstateSectorAnatomyFromPIRADSV26301);
            _uids.Add(DicomUID.ProstateSectorAnatomyFromEuropeanConcensus16SectorMinimalModel6302.UID, DicomUID.ProstateSectorAnatomyFromEuropeanConcensus16SectorMinimalModel6302);
            _uids.Add(DicomUID.ProstateSectorAnatomyFromEuropeanConcensus27SectorOptimalModel6303.UID, DicomUID.ProstateSectorAnatomyFromEuropeanConcensus27SectorOptimalModel6303);
            _uids.Add(DicomUID.MeasurementSelectionReason12301.UID, DicomUID.MeasurementSelectionReason12301);
            _uids.Add(DicomUID.EchoFindingObservationType12302.UID, DicomUID.EchoFindingObservationType12302);
            _uids.Add(DicomUID.EchoMeasurementType12303.UID, DicomUID.EchoMeasurementType12303);
            _uids.Add(DicomUID.EchoMeasuredProperty12304.UID, DicomUID.EchoMeasuredProperty12304);
            _uids.Add(DicomUID.BasicEchoAnatomicSite12305.UID, DicomUID.BasicEchoAnatomicSite12305);
            _uids.Add(DicomUID.EchoFlowDirection12306.UID, DicomUID.EchoFlowDirection12306);
            _uids.Add(DicomUID.CardiacPhaseAndTimePoint12307.UID, DicomUID.CardiacPhaseAndTimePoint12307);
            _uids.Add(DicomUID.CoreEchoMeasurement12300.UID, DicomUID.CoreEchoMeasurement12300);
            _uids.Add(DicomUID.OCTAProcessingAlgorithmFamily4270.UID, DicomUID.OCTAProcessingAlgorithmFamily4270);
            _uids.Add(DicomUID.EnFaceImageType4271.UID, DicomUID.EnFaceImageType4271);
            _uids.Add(DicomUID.OptScanPatternType4272.UID, DicomUID.OptScanPatternType4272);
            _uids.Add(DicomUID.RetinalSegmentationSurface4273.UID, DicomUID.RetinalSegmentationSurface4273);
            _uids.Add(DicomUID.OrganForRadiationDoseEstimate10060.UID, DicomUID.OrganForRadiationDoseEstimate10060);
            _uids.Add(DicomUID.AbsorbedRadiationDoseType10061.UID, DicomUID.AbsorbedRadiationDoseType10061);
            _uids.Add(DicomUID.EquivalentRadiationDoseType10062.UID, DicomUID.EquivalentRadiationDoseType10062);
            _uids.Add(DicomUID.RadiationDoseEstimateDistributionRepresentation10063.UID, DicomUID.RadiationDoseEstimateDistributionRepresentation10063);
            _uids.Add(DicomUID.PatientModelType10064.UID, DicomUID.PatientModelType10064);
            _uids.Add(DicomUID.RadiationTransportModelType10065.UID, DicomUID.RadiationTransportModelType10065);
            _uids.Add(DicomUID.AttenuatorCategory10066.UID, DicomUID.AttenuatorCategory10066);
            _uids.Add(DicomUID.RadiationAttenuatorMaterial10067.UID, DicomUID.RadiationAttenuatorMaterial10067);
            _uids.Add(DicomUID.EstimateMethodType10068.UID, DicomUID.EstimateMethodType10068);
            _uids.Add(DicomUID.RadiationDoseEstimateParameter10069.UID, DicomUID.RadiationDoseEstimateParameter10069);
            _uids.Add(DicomUID.RadiationDoseType10070.UID, DicomUID.RadiationDoseType10070);
            _uids.Add(DicomUID.MRDiffusionComponentSemantic7270.UID, DicomUID.MRDiffusionComponentSemantic7270);
            _uids.Add(DicomUID.MRDiffusionAnisotropyIndex7271.UID, DicomUID.MRDiffusionAnisotropyIndex7271);
            _uids.Add(DicomUID.MRDiffusionModelParameter7272.UID, DicomUID.MRDiffusionModelParameter7272);
            _uids.Add(DicomUID.MRDiffusionModel7273.UID, DicomUID.MRDiffusionModel7273);
            _uids.Add(DicomUID.MRDiffusionModelFittingMethod7274.UID, DicomUID.MRDiffusionModelFittingMethod7274);
            _uids.Add(DicomUID.MRDiffusionModelSpecificMethod7275.UID, DicomUID.MRDiffusionModelSpecificMethod7275);
            _uids.Add(DicomUID.MRDiffusionModelInput7276.UID, DicomUID.MRDiffusionModelInput7276);
            _uids.Add(DicomUID.DiffusionRateAreaOverTimeUnit7277.UID, DicomUID.DiffusionRateAreaOverTimeUnit7277);
            _uids.Add(DicomUID.PediatricSizeCategory7039.UID, DicomUID.PediatricSizeCategory7039);
            _uids.Add(DicomUID.CalciumScoringPatientSizeCategory7041.UID, DicomUID.CalciumScoringPatientSizeCategory7041);
            _uids.Add(DicomUID.ReasonForRepeatingAcquisition10034.UID, DicomUID.ReasonForRepeatingAcquisition10034);
            _uids.Add(DicomUID.ProtocolAssertion800.UID, DicomUID.ProtocolAssertion800);
            _uids.Add(DicomUID.RadiotherapeuticDoseMeasurementDevice7026.UID, DicomUID.RadiotherapeuticDoseMeasurementDevice7026);
            _uids.Add(DicomUID.ExportAdditionalInformationDocumentTitle7014.UID, DicomUID.ExportAdditionalInformationDocumentTitle7014);
            _uids.Add(DicomUID.ExportDelayReason7015.UID, DicomUID.ExportDelayReason7015);
            _uids.Add(DicomUID.LevelOfDifficulty7016.UID, DicomUID.LevelOfDifficulty7016);
            _uids.Add(DicomUID.CategoryOfTeachingMaterialImaging7017.UID, DicomUID.CategoryOfTeachingMaterialImaging7017);
            _uids.Add(DicomUID.MiscellaneousDocumentTitle7018.UID, DicomUID.MiscellaneousDocumentTitle7018);
            _uids.Add(DicomUID.SegmentationNonImageSourcePurposeOfReference7019.UID, DicomUID.SegmentationNonImageSourcePurposeOfReference7019);
            _uids.Add(DicomUID.LongitudinalTemporalEventType280.UID, DicomUID.LongitudinalTemporalEventType280);
            _uids.Add(DicomUID.NonLesionObjectTypePhysicalObject6401.UID, DicomUID.NonLesionObjectTypePhysicalObject6401);
            _uids.Add(DicomUID.NonLesionObjectTypeSubstance6402.UID, DicomUID.NonLesionObjectTypeSubstance6402);
            _uids.Add(DicomUID.NonLesionObjectTypeTissue6403.UID, DicomUID.NonLesionObjectTypeTissue6403);
            _uids.Add(DicomUID.ChestNonLesionObjectTypePhysicalObject6404.UID, DicomUID.ChestNonLesionObjectTypePhysicalObject6404);
            _uids.Add(DicomUID.ChestNonLesionObjectTypeTissue6405.UID, DicomUID.ChestNonLesionObjectTypeTissue6405);
            _uids.Add(DicomUID.TissueSegmentationPropertyType7191.UID, DicomUID.TissueSegmentationPropertyType7191);
            _uids.Add(DicomUID.AnatomicalStructureSegmentationPropertyType7192.UID, DicomUID.AnatomicalStructureSegmentationPropertyType7192);
            _uids.Add(DicomUID.PhysicalObjectSegmentationPropertyType7193.UID, DicomUID.PhysicalObjectSegmentationPropertyType7193);
            _uids.Add(DicomUID.MorphologicallyAbnormalStructureSegmentationPropertyType7194.UID, DicomUID.MorphologicallyAbnormalStructureSegmentationPropertyType7194);
            _uids.Add(DicomUID.FunctionSegmentationPropertyType7195.UID, DicomUID.FunctionSegmentationPropertyType7195);
            _uids.Add(DicomUID.SpatialAndRelationalConceptSegmentationPropertyType7196.UID, DicomUID.SpatialAndRelationalConceptSegmentationPropertyType7196);
            _uids.Add(DicomUID.BodySubstanceSegmentationPropertyType7197.UID, DicomUID.BodySubstanceSegmentationPropertyType7197);
            _uids.Add(DicomUID.SubstanceSegmentationPropertyType7198.UID, DicomUID.SubstanceSegmentationPropertyType7198);
            _uids.Add(DicomUID.InterpretationRequestDiscontinuationReason9303.UID, DicomUID.InterpretationRequestDiscontinuationReason9303);
            _uids.Add(DicomUID.GrayLevelRunLengthBasedFeature7475.UID, DicomUID.GrayLevelRunLengthBasedFeature7475);
            _uids.Add(DicomUID.GrayLevelSizeZoneBasedFeature7476.UID, DicomUID.GrayLevelSizeZoneBasedFeature7476);
            _uids.Add(DicomUID.EncapsulatedDocumentSourcePurposeOfReference7060.UID, DicomUID.EncapsulatedDocumentSourcePurposeOfReference7060);
            _uids.Add(DicomUID.ModelDocumentTitle7061.UID, DicomUID.ModelDocumentTitle7061);
            _uids.Add(DicomUID.PurposeOfReferenceToPredecessor3DModel7062.UID, DicomUID.PurposeOfReferenceToPredecessor3DModel7062);
            _uids.Add(DicomUID.ModelScaleUnit7063.UID, DicomUID.ModelScaleUnit7063);
            _uids.Add(DicomUID.ModelUsage7064.UID, DicomUID.ModelUsage7064);
            _uids.Add(DicomUID.RadiationDoseUnit10071.UID, DicomUID.RadiationDoseUnit10071);
            _uids.Add(DicomUID.RadiotherapyFiducial7112.UID, DicomUID.RadiotherapyFiducial7112);
            _uids.Add(DicomUID.MultiEnergyRelevantMaterial300.UID, DicomUID.MultiEnergyRelevantMaterial300);
            _uids.Add(DicomUID.MultiEnergyMaterialUnit301.UID, DicomUID.MultiEnergyMaterialUnit301);
            _uids.Add(DicomUID.DosimetricObjectiveType9500.UID, DicomUID.DosimetricObjectiveType9500);
            _uids.Add(DicomUID.PrescriptionAnatomyCategory9501.UID, DicomUID.PrescriptionAnatomyCategory9501);
            _uids.Add(DicomUID.RTSegmentAnnotationCategory9502.UID, DicomUID.RTSegmentAnnotationCategory9502);
            _uids.Add(DicomUID.RadiotherapyTherapeuticRoleCategory9503.UID, DicomUID.RadiotherapyTherapeuticRoleCategory9503);
            _uids.Add(DicomUID.RTGeometricInformation9504.UID, DicomUID.RTGeometricInformation9504);
            _uids.Add(DicomUID.FixationOrPositioningDevice9505.UID, DicomUID.FixationOrPositioningDevice9505);
            _uids.Add(DicomUID.BrachytherapyDevice9506.UID, DicomUID.BrachytherapyDevice9506);
            _uids.Add(DicomUID.ExternalBodyModel9507.UID, DicomUID.ExternalBodyModel9507);
            _uids.Add(DicomUID.NonSpecificVolume9508.UID, DicomUID.NonSpecificVolume9508);
            _uids.Add(DicomUID.PurposeOfReferenceForRTPhysicianIntentInput9509.UID, DicomUID.PurposeOfReferenceForRTPhysicianIntentInput9509);
            _uids.Add(DicomUID.PurposeOfReferenceForRTTreatmentPlanningInput9510.UID, DicomUID.PurposeOfReferenceForRTTreatmentPlanningInput9510);
            _uids.Add(DicomUID.GeneralExternalRadiotherapyProcedureTechnique9511.UID, DicomUID.GeneralExternalRadiotherapyProcedureTechnique9511);
            _uids.Add(DicomUID.TomotherapeuticRadiotherapyProcedureTechnique9512.UID, DicomUID.TomotherapeuticRadiotherapyProcedureTechnique9512);
            _uids.Add(DicomUID.FixationDevice9513.UID, DicomUID.FixationDevice9513);
            _uids.Add(DicomUID.AnatomicalStructureForRadiotherapy9514.UID, DicomUID.AnatomicalStructureForRadiotherapy9514);
            _uids.Add(DicomUID.RTPatientSupportDevice9515.UID, DicomUID.RTPatientSupportDevice9515);
            _uids.Add(DicomUID.RadiotherapyBolusDeviceType9516.UID, DicomUID.RadiotherapyBolusDeviceType9516);
            _uids.Add(DicomUID.RadiotherapyBlockDeviceType9517.UID, DicomUID.RadiotherapyBlockDeviceType9517);
            _uids.Add(DicomUID.RadiotherapyAccessoryNoSlotHolderDeviceType9518.UID, DicomUID.RadiotherapyAccessoryNoSlotHolderDeviceType9518);
            _uids.Add(DicomUID.RadiotherapyAccessorySlotHolderDeviceType9519.UID, DicomUID.RadiotherapyAccessorySlotHolderDeviceType9519);
            _uids.Add(DicomUID.SegmentedRTAccessoryDevice9520.UID, DicomUID.SegmentedRTAccessoryDevice9520);
            _uids.Add(DicomUID.RadiotherapyTreatmentEnergyUnit9521.UID, DicomUID.RadiotherapyTreatmentEnergyUnit9521);
            _uids.Add(DicomUID.MultiSourceRadiotherapyProcedureTechnique9522.UID, DicomUID.MultiSourceRadiotherapyProcedureTechnique9522);
            _uids.Add(DicomUID.RoboticRadiotherapyProcedureTechnique9523.UID, DicomUID.RoboticRadiotherapyProcedureTechnique9523);
            _uids.Add(DicomUID.RadiotherapyProcedureTechnique9524.UID, DicomUID.RadiotherapyProcedureTechnique9524);
            _uids.Add(DicomUID.RadiationTherapyParticle9525.UID, DicomUID.RadiationTherapyParticle9525);
            _uids.Add(DicomUID.IonTherapyParticle9526.UID, DicomUID.IonTherapyParticle9526);
            _uids.Add(DicomUID.TeletherapyIsotope9527.UID, DicomUID.TeletherapyIsotope9527);
            _uids.Add(DicomUID.BrachytherapyIsotope9528.UID, DicomUID.BrachytherapyIsotope9528);
            _uids.Add(DicomUID.SingleDoseDosimetricObjective9529.UID, DicomUID.SingleDoseDosimetricObjective9529);
            _uids.Add(DicomUID.PercentageAndDoseDosimetricObjective9530.UID, DicomUID.PercentageAndDoseDosimetricObjective9530);
            _uids.Add(DicomUID.VolumeAndDoseDosimetricObjective9531.UID, DicomUID.VolumeAndDoseDosimetricObjective9531);
            _uids.Add(DicomUID.NoParameterDosimetricObjective9532.UID, DicomUID.NoParameterDosimetricObjective9532);
            _uids.Add(DicomUID.DeliveryTimeStructure9533.UID, DicomUID.DeliveryTimeStructure9533);
            _uids.Add(DicomUID.RadiotherapyTarget9534.UID, DicomUID.RadiotherapyTarget9534);
            _uids.Add(DicomUID.RadiotherapyDoseCalculationRole9535.UID, DicomUID.RadiotherapyDoseCalculationRole9535);
            _uids.Add(DicomUID.RadiotherapyPrescribingAndSegmentingPersonRole9536.UID, DicomUID.RadiotherapyPrescribingAndSegmentingPersonRole9536);
            _uids.Add(DicomUID.EffectiveDoseCalculationMethodCategory9537.UID, DicomUID.EffectiveDoseCalculationMethodCategory9537);
            _uids.Add(DicomUID.RadiationTransportBasedEffectiveDoseMethodModifier9538.UID, DicomUID.RadiationTransportBasedEffectiveDoseMethodModifier9538);
            _uids.Add(DicomUID.FractionationBasedEffectiveDoseMethodModifier9539.UID, DicomUID.FractionationBasedEffectiveDoseMethodModifier9539);
            _uids.Add(DicomUID.ImagingAgentAdministrationAdverseEvent60.UID, DicomUID.ImagingAgentAdministrationAdverseEvent60);
            _uids.Add(DicomUID.TimeRelativeToProcedure61RETIRED.UID, DicomUID.TimeRelativeToProcedure61RETIRED);
            _uids.Add(DicomUID.ImagingAgentAdministrationPhaseType62.UID, DicomUID.ImagingAgentAdministrationPhaseType62);
            _uids.Add(DicomUID.ImagingAgentAdministrationMode63.UID, DicomUID.ImagingAgentAdministrationMode63);
            _uids.Add(DicomUID.ImagingAgentAdministrationPatientState64.UID, DicomUID.ImagingAgentAdministrationPatientState64);
            _uids.Add(DicomUID.ImagingAgentAdministrationPremedication65.UID, DicomUID.ImagingAgentAdministrationPremedication65);
            _uids.Add(DicomUID.ImagingAgentAdministrationMedication66.UID, DicomUID.ImagingAgentAdministrationMedication66);
            _uids.Add(DicomUID.ImagingAgentAdministrationCompletionStatus67.UID, DicomUID.ImagingAgentAdministrationCompletionStatus67);
            _uids.Add(DicomUID.ImagingAgentAdministrationPharmaceuticalPresentationUnit68.UID, DicomUID.ImagingAgentAdministrationPharmaceuticalPresentationUnit68);
            _uids.Add(DicomUID.ImagingAgentAdministrationConsumable69.UID, DicomUID.ImagingAgentAdministrationConsumable69);
            _uids.Add(DicomUID.Flush70.UID, DicomUID.Flush70);
            _uids.Add(DicomUID.ImagingAgentAdministrationInjectorEventType71.UID, DicomUID.ImagingAgentAdministrationInjectorEventType71);
            _uids.Add(DicomUID.ImagingAgentAdministrationStepType72.UID, DicomUID.ImagingAgentAdministrationStepType72);
            _uids.Add(DicomUID.BolusShapingCurve73.UID, DicomUID.BolusShapingCurve73);
            _uids.Add(DicomUID.ImagingAgentAdministrationConsumableCatheterType74.UID, DicomUID.ImagingAgentAdministrationConsumableCatheterType74);
            _uids.Add(DicomUID.LowHighOrEqual75.UID, DicomUID.LowHighOrEqual75);
            _uids.Add(DicomUID.PremedicationType76.UID, DicomUID.PremedicationType76);
            _uids.Add(DicomUID.LateralityWithMedian245.UID, DicomUID.LateralityWithMedian245);
            _uids.Add(DicomUID.DermatologyAnatomicSite4029.UID, DicomUID.DermatologyAnatomicSite4029);
            _uids.Add(DicomUID.QuantitativeImageFeature218.UID, DicomUID.QuantitativeImageFeature218);
            _uids.Add(DicomUID.GlobalShapeDescriptor7477.UID, DicomUID.GlobalShapeDescriptor7477);
            _uids.Add(DicomUID.IntensityHistogramFeature7478.UID, DicomUID.IntensityHistogramFeature7478);
            _uids.Add(DicomUID.GreyLevelDistanceZoneBasedFeature7479.UID, DicomUID.GreyLevelDistanceZoneBasedFeature7479);
            _uids.Add(DicomUID.NeighbourhoodGreyToneDifferenceBasedFeature7500.UID, DicomUID.NeighbourhoodGreyToneDifferenceBasedFeature7500);
            _uids.Add(DicomUID.NeighbouringGreyLevelDependenceBasedFeature7501.UID, DicomUID.NeighbouringGreyLevelDependenceBasedFeature7501);
            _uids.Add(DicomUID.CorneaMeasurementMethodDescriptor4242.UID, DicomUID.CorneaMeasurementMethodDescriptor4242);
            _uids.Add(DicomUID.SegmentedRadiotherapeuticDoseMeasurementDevice7027.UID, DicomUID.SegmentedRadiotherapeuticDoseMeasurementDevice7027);
            _uids.Add(DicomUID.ClinicalCourseOfDisease6098.UID, DicomUID.ClinicalCourseOfDisease6098);
            _uids.Add(DicomUID.RacialGroup6099.UID, DicomUID.RacialGroup6099);
            _uids.Add(DicomUID.RelativeLaterality246.UID, DicomUID.RelativeLaterality246);
            _uids.Add(DicomUID.BrainLesionSegmentationTypeWithNecrosis7168.UID, DicomUID.BrainLesionSegmentationTypeWithNecrosis7168);
            _uids.Add(DicomUID.BrainLesionSegmentationTypeWithoutNecrosis7169.UID, DicomUID.BrainLesionSegmentationTypeWithoutNecrosis7169);
            _uids.Add(DicomUID.NonAcquisitionModality32.UID, DicomUID.NonAcquisitionModality32);
            _uids.Add(DicomUID.Modality33.UID, DicomUID.Modality33);
            _uids.Add(DicomUID.LateralityLeftRightOnly247.UID, DicomUID.LateralityLeftRightOnly247);
            _uids.Add(DicomUID.QualitativeEvaluationModifierType210.UID, DicomUID.QualitativeEvaluationModifierType210);
            _uids.Add(DicomUID.QualitativeEvaluationModifierValue211.UID, DicomUID.QualitativeEvaluationModifierValue211);
            _uids.Add(DicomUID.GenericAnatomicLocationModifier212.UID, DicomUID.GenericAnatomicLocationModifier212);
            _uids.Add(DicomUID.BeamLimitingDeviceType9541.UID, DicomUID.BeamLimitingDeviceType9541);
            _uids.Add(DicomUID.CompensatorDeviceType9542.UID, DicomUID.CompensatorDeviceType9542);
            _uids.Add(DicomUID.RadiotherapyTreatmentMachineMode9543.UID, DicomUID.RadiotherapyTreatmentMachineMode9543);
            _uids.Add(DicomUID.RadiotherapyDistanceReferenceLocation9544.UID, DicomUID.RadiotherapyDistanceReferenceLocation9544);
            _uids.Add(DicomUID.FixedBeamLimitingDeviceType9545.UID, DicomUID.FixedBeamLimitingDeviceType9545);
            _uids.Add(DicomUID.RadiotherapyWedgeType9546.UID, DicomUID.RadiotherapyWedgeType9546);
            _uids.Add(DicomUID.RTBeamLimitingDeviceOrientationLabel9547.UID, DicomUID.RTBeamLimitingDeviceOrientationLabel9547);
            _uids.Add(DicomUID.GeneralAccessoryDeviceType9548.UID, DicomUID.GeneralAccessoryDeviceType9548);
            _uids.Add(DicomUID.RadiationGenerationModeType9549.UID, DicomUID.RadiationGenerationModeType9549);
            _uids.Add(DicomUID.CArmPhotonElectronDeliveryRateUnit9550.UID, DicomUID.CArmPhotonElectronDeliveryRateUnit9550);
            _uids.Add(DicomUID.TreatmentDeliveryDeviceType9551.UID, DicomUID.TreatmentDeliveryDeviceType9551);
            _uids.Add(DicomUID.CArmPhotonElectronDosimeterUnit9552.UID, DicomUID.CArmPhotonElectronDosimeterUnit9552);
            _uids.Add(DicomUID.TreatmentPoint9553.UID, DicomUID.TreatmentPoint9553);
            _uids.Add(DicomUID.EquipmentReferencePoint9554.UID, DicomUID.EquipmentReferencePoint9554);
            _uids.Add(DicomUID.RadiotherapyTreatmentPlanningPersonRole9555.UID, DicomUID.RadiotherapyTreatmentPlanningPersonRole9555);
            _uids.Add(DicomUID.RealTimeVideoRenditionTitle7070.UID, DicomUID.RealTimeVideoRenditionTitle7070);
            _uids.Add(DicomUID.GeometryGraphicalRepresentation219.UID, DicomUID.GeometryGraphicalRepresentation219);
            _uids.Add(DicomUID.VisualExplanation217.UID, DicomUID.VisualExplanation217);
            _uids.Add(DicomUID.ProstateSectorAnatomyFromPIRADSV216304.UID, DicomUID.ProstateSectorAnatomyFromPIRADSV216304);
            _uids.Add(DicomUID.RadiotherapyRoboticNodeSet9556.UID, DicomUID.RadiotherapyRoboticNodeSet9556);
            _uids.Add(DicomUID.TomotherapeuticDosimeterUnit9557.UID, DicomUID.TomotherapeuticDosimeterUnit9557);
            _uids.Add(DicomUID.TomotherapeuticDoseRateUnit9558.UID, DicomUID.TomotherapeuticDoseRateUnit9558);
            _uids.Add(DicomUID.RoboticDeliveryDeviceDosimeterUnit9559.UID, DicomUID.RoboticDeliveryDeviceDosimeterUnit9559);
            _uids.Add(DicomUID.RoboticDeliveryDeviceDoseRateUnit9560.UID, DicomUID.RoboticDeliveryDeviceDoseRateUnit9560);
            _uids.Add(DicomUID.AnatomicStructure8134.UID, DicomUID.AnatomicStructure8134);
            _uids.Add(DicomUID.MediastinumFindingOrFeature6148.UID, DicomUID.MediastinumFindingOrFeature6148);
            _uids.Add(DicomUID.MediastinumAnatomy6149.UID, DicomUID.MediastinumAnatomy6149);
            _uids.Add(DicomUID.VascularUltrasoundReportDocumentTitle12100.UID, DicomUID.VascularUltrasoundReportDocumentTitle12100);
            _uids.Add(DicomUID.OrganPartNonLateralized12130.UID, DicomUID.OrganPartNonLateralized12130);
            _uids.Add(DicomUID.OrganPartLateralized12131.UID, DicomUID.OrganPartLateralized12131);
            _uids.Add(DicomUID.TreatmentTerminationReason9561.UID, DicomUID.TreatmentTerminationReason9561);
            _uids.Add(DicomUID.RadiotherapyTreatmentDeliveryPersonRole9562.UID, DicomUID.RadiotherapyTreatmentDeliveryPersonRole9562);
            _uids.Add(DicomUID.RadiotherapyInterlockResolution9563.UID, DicomUID.RadiotherapyInterlockResolution9563);
            _uids.Add(DicomUID.TreatmentSessionConfirmationAssertion9564.UID, DicomUID.TreatmentSessionConfirmationAssertion9564);
            _uids.Add(DicomUID.TreatmentToleranceViolationCause9565.UID, DicomUID.TreatmentToleranceViolationCause9565);
            _uids.Add(DicomUID.ClinicalToleranceViolationType9566.UID, DicomUID.ClinicalToleranceViolationType9566);
            _uids.Add(DicomUID.MachineToleranceViolationType9567.UID, DicomUID.MachineToleranceViolationType9567);
            _uids.Add(DicomUID.RadiotherapyTreatmentInterlock9568.UID, DicomUID.RadiotherapyTreatmentInterlock9568);
            _uids.Add(DicomUID.IsocentricPatientSupportPositionParameter9569.UID, DicomUID.IsocentricPatientSupportPositionParameter9569);
            _uids.Add(DicomUID.RTOverriddenTreatmentParameter9570.UID, DicomUID.RTOverriddenTreatmentParameter9570);
            _uids.Add(DicomUID.EEGLead3030.UID, DicomUID.EEGLead3030);
            _uids.Add(DicomUID.LeadLocationNearOrInMuscle3031.UID, DicomUID.LeadLocationNearOrInMuscle3031);
            _uids.Add(DicomUID.LeadLocationNearPeripheralNerve3032.UID, DicomUID.LeadLocationNearPeripheralNerve3032);
            _uids.Add(DicomUID.EOGLead3033.UID, DicomUID.EOGLead3033);
            _uids.Add(DicomUID.BodyPositionChannel3034.UID, DicomUID.BodyPositionChannel3034);
            _uids.Add(DicomUID.EEGAnnotationNeurophysiologicEnumeration3035.UID, DicomUID.EEGAnnotationNeurophysiologicEnumeration3035);
            _uids.Add(DicomUID.EMGAnnotationNeurophysiologicalEnumeration3036.UID, DicomUID.EMGAnnotationNeurophysiologicalEnumeration3036);
            _uids.Add(DicomUID.EOGAnnotationNeurophysiologicalEnumeration3037.UID, DicomUID.EOGAnnotationNeurophysiologicalEnumeration3037);
            _uids.Add(DicomUID.PatternEvent3038.UID, DicomUID.PatternEvent3038);
            _uids.Add(DicomUID.DeviceRelatedAndEnvironmentRelatedEvent3039.UID, DicomUID.DeviceRelatedAndEnvironmentRelatedEvent3039);
            _uids.Add(DicomUID.EEGAnnotationNeurologicalMonitoringMeasurement3040.UID, DicomUID.EEGAnnotationNeurologicalMonitoringMeasurement3040);
            _uids.Add(DicomUID.OBGYNUltrasoundReportDocumentTitle12024.UID, DicomUID.OBGYNUltrasoundReportDocumentTitle12024);
            _uids.Add(DicomUID.AutomationOfMeasurement7230.UID, DicomUID.AutomationOfMeasurement7230);
            _uids.Add(DicomUID.OBGYNUltrasoundBeamPath12025.UID, DicomUID.OBGYNUltrasoundBeamPath12025);
            _uids.Add(DicomUID.AngleMeasurement7550.UID, DicomUID.AngleMeasurement7550);
            _uids.Add(DicomUID.GenericPurposeOfReferenceToImagesAndCoordinatesInMeasurement7551.UID, DicomUID.GenericPurposeOfReferenceToImagesAndCoordinatesInMeasurement7551);
            _uids.Add(DicomUID.GenericPurposeOfReferenceToImagesInMeasurement7552.UID, DicomUID.GenericPurposeOfReferenceToImagesInMeasurement7552);
            _uids.Add(DicomUID.GenericPurposeOfReferenceToCoordinatesInMeasurement7553.UID, DicomUID.GenericPurposeOfReferenceToCoordinatesInMeasurement7553);
            _uids.Add(DicomUID.FitzpatrickSkinType4401.UID, DicomUID.FitzpatrickSkinType4401);
            _uids.Add(DicomUID.HistoryOfMalignantMelanoma4402.UID, DicomUID.HistoryOfMalignantMelanoma4402);
            _uids.Add(DicomUID.HistoryOfMelanomaInSitu4403.UID, DicomUID.HistoryOfMelanomaInSitu4403);
            _uids.Add(DicomUID.HistoryOfNonMelanomaSkinCancer4404.UID, DicomUID.HistoryOfNonMelanomaSkinCancer4404);
            _uids.Add(DicomUID.SkinDisorder4405.UID, DicomUID.SkinDisorder4405);
            _uids.Add(DicomUID.PatientReportedLesionCharacteristic4406.UID, DicomUID.PatientReportedLesionCharacteristic4406);
            _uids.Add(DicomUID.LesionPalpationFinding4407.UID, DicomUID.LesionPalpationFinding4407);
            _uids.Add(DicomUID.LesionVisualFinding4408.UID, DicomUID.LesionVisualFinding4408);
            _uids.Add(DicomUID.SkinProcedure4409.UID, DicomUID.SkinProcedure4409);
            _uids.Add(DicomUID.AbdominopelvicVessel12125.UID, DicomUID.AbdominopelvicVessel12125);
            _uids.Add(DicomUID.NumericValueFailureQualifier43.UID, DicomUID.NumericValueFailureQualifier43);
            _uids.Add(DicomUID.NumericValueUnknownQualifier44.UID, DicomUID.NumericValueUnknownQualifier44);
            _uids.Add(DicomUID.CouinaudLiverSegment7170.UID, DicomUID.CouinaudLiverSegment7170);
            _uids.Add(DicomUID.LiverSegmentationType7171.UID, DicomUID.LiverSegmentationType7171);
            _uids.Add(DicomUID.ContraindicationsForXAImaging1201.UID, DicomUID.ContraindicationsForXAImaging1201);
            _uids.Add(DicomUID.NeurophysiologicStimulationMode3041.UID, DicomUID.NeurophysiologicStimulationMode3041);
            _uids.Add(DicomUID.ReportedValueType10072.UID, DicomUID.ReportedValueType10072);
            _uids.Add(DicomUID.ValueTiming10073.UID, DicomUID.ValueTiming10073);
            _uids.Add(DicomUID.RDSRFrameOfReferenceOrigin10074.UID, DicomUID.RDSRFrameOfReferenceOrigin10074);
            _uids.Add(DicomUID.MicroscopyAnnotationPropertyType8135.UID, DicomUID.MicroscopyAnnotationPropertyType8135);
            _uids.Add(DicomUID.MicroscopyMeasurementType8136.UID, DicomUID.MicroscopyMeasurementType8136);
            _uids.Add(DicomUID.ProstateReportingSystem6310.UID, DicomUID.ProstateReportingSystem6310);
            _uids.Add(DicomUID.MRSignalIntensity6311.UID, DicomUID.MRSignalIntensity6311);
            _uids.Add(DicomUID.CrossSectionalScanPlaneOrientation6312.UID, DicomUID.CrossSectionalScanPlaneOrientation6312);
            _uids.Add(DicomUID.HistoryOfProstateDisease6313.UID, DicomUID.HistoryOfProstateDisease6313);
            _uids.Add(DicomUID.ProstateMRIStudyQualityFinding6314.UID, DicomUID.ProstateMRIStudyQualityFinding6314);
            _uids.Add(DicomUID.ProstateMRISeriesQualityFinding6315.UID, DicomUID.ProstateMRISeriesQualityFinding6315);
            _uids.Add(DicomUID.MRImagingArtifact6316.UID, DicomUID.MRImagingArtifact6316);
            _uids.Add(DicomUID.ProstateDCEMRIQualityFinding6317.UID, DicomUID.ProstateDCEMRIQualityFinding6317);
            _uids.Add(DicomUID.ProstateDWIMRIQualityFinding6318.UID, DicomUID.ProstateDWIMRIQualityFinding6318);
            _uids.Add(DicomUID.AbdominalInterventionType6319.UID, DicomUID.AbdominalInterventionType6319);
            _uids.Add(DicomUID.AbdominopelvicIntervention6320.UID, DicomUID.AbdominopelvicIntervention6320);
            _uids.Add(DicomUID.ProstateCancerDiagnosticProcedure6321.UID, DicomUID.ProstateCancerDiagnosticProcedure6321);
            _uids.Add(DicomUID.ProstateCancerFamilyHistory6322.UID, DicomUID.ProstateCancerFamilyHistory6322);
            _uids.Add(DicomUID.ProstateCancerTherapy6323.UID, DicomUID.ProstateCancerTherapy6323);
            _uids.Add(DicomUID.ProstateMRIAssessment6324.UID, DicomUID.ProstateMRIAssessment6324);
            _uids.Add(DicomUID.OverallAssessmentFromPIRADS6325.UID, DicomUID.OverallAssessmentFromPIRADS6325);
            _uids.Add(DicomUID.ImageQualityControlStandard6326.UID, DicomUID.ImageQualityControlStandard6326);
            _uids.Add(DicomUID.ProstateImagingIndication6327.UID, DicomUID.ProstateImagingIndication6327);
            _uids.Add(DicomUID.PIRADSV2LesionAssessmentCategory6328.UID, DicomUID.PIRADSV2LesionAssessmentCategory6328);
            _uids.Add(DicomUID.PIRADSV2T2WIPZLesionAssessmentCategory6329.UID, DicomUID.PIRADSV2T2WIPZLesionAssessmentCategory6329);
            _uids.Add(DicomUID.PIRADSV2T2WITZLesionAssessmentCategory6330.UID, DicomUID.PIRADSV2T2WITZLesionAssessmentCategory6330);
            _uids.Add(DicomUID.PIRADSV2DWILesionAssessmentCategory6331.UID, DicomUID.PIRADSV2DWILesionAssessmentCategory6331);
            _uids.Add(DicomUID.PIRADSV2DCELesionAssessmentCategory6332.UID, DicomUID.PIRADSV2DCELesionAssessmentCategory6332);
            _uids.Add(DicomUID.mpMRIAssessmentType6333.UID, DicomUID.mpMRIAssessmentType6333);
            _uids.Add(DicomUID.mpMRIAssessmentTypeFromPIRADS6334.UID, DicomUID.mpMRIAssessmentTypeFromPIRADS6334);
            _uids.Add(DicomUID.mpMRIAssessmentValue6335.UID, DicomUID.mpMRIAssessmentValue6335);
            _uids.Add(DicomUID.MRIAbnormality6336.UID, DicomUID.MRIAbnormality6336);
            _uids.Add(DicomUID.mpMRIProstateAbnormalityFromPIRADS6337.UID, DicomUID.mpMRIProstateAbnormalityFromPIRADS6337);
            _uids.Add(DicomUID.mpMRIBenignProstateAbnormalityFromPIRADS6338.UID, DicomUID.mpMRIBenignProstateAbnormalityFromPIRADS6338);
            _uids.Add(DicomUID.MRIShapeCharacteristic6339.UID, DicomUID.MRIShapeCharacteristic6339);
            _uids.Add(DicomUID.ProstateMRIShapeCharacteristicFromPIRADS6340.UID, DicomUID.ProstateMRIShapeCharacteristicFromPIRADS6340);
            _uids.Add(DicomUID.MRIMarginCharacteristic6341.UID, DicomUID.MRIMarginCharacteristic6341);
            _uids.Add(DicomUID.ProstateMRIMarginCharacteristicFromPIRADS6342.UID, DicomUID.ProstateMRIMarginCharacteristicFromPIRADS6342);
            _uids.Add(DicomUID.MRISignalCharacteristic6343.UID, DicomUID.MRISignalCharacteristic6343);
            _uids.Add(DicomUID.ProstateMRISignalCharacteristicFromPIRADS6344.UID, DicomUID.ProstateMRISignalCharacteristicFromPIRADS6344);
            _uids.Add(DicomUID.MRIEnhancementPattern6345.UID, DicomUID.MRIEnhancementPattern6345);
            _uids.Add(DicomUID.ProstateMRIEnhancementPatternFromPIRADS6346.UID, DicomUID.ProstateMRIEnhancementPatternFromPIRADS6346);
            _uids.Add(DicomUID.ProstateMRIExtraProstaticFinding6347.UID, DicomUID.ProstateMRIExtraProstaticFinding6347);
            _uids.Add(DicomUID.ProstateMRIAssessmentOfExtraProstaticAnatomicSite6348.UID, DicomUID.ProstateMRIAssessmentOfExtraProstaticAnatomicSite6348);
            _uids.Add(DicomUID.MRCoilType6349.UID, DicomUID.MRCoilType6349);
            _uids.Add(DicomUID.EndorectalCoilFillSubstance6350.UID, DicomUID.EndorectalCoilFillSubstance6350);
            _uids.Add(DicomUID.ProstateRelationalMeasurement6351.UID, DicomUID.ProstateRelationalMeasurement6351);
            _uids.Add(DicomUID.ProstateCancerDiagnosticBloodLabMeasurement6352.UID, DicomUID.ProstateCancerDiagnosticBloodLabMeasurement6352);
            _uids.Add(DicomUID.ProstateImagingTypesOfQualityControlStandard6353.UID, DicomUID.ProstateImagingTypesOfQualityControlStandard6353);
            _uids.Add(DicomUID.UltrasoundShearWaveMeasurement12308.UID, DicomUID.UltrasoundShearWaveMeasurement12308);
            _uids.Add(DicomUID.LeftVentricleMyocardialWall16SegmentModel3780RETIRED.UID, DicomUID.LeftVentricleMyocardialWall16SegmentModel3780RETIRED);
            _uids.Add(DicomUID.LeftVentricleMyocardialWall18SegmentModel3781.UID, DicomUID.LeftVentricleMyocardialWall18SegmentModel3781);
            _uids.Add(DicomUID.LeftVentricleBasalWall6Segments3782.UID, DicomUID.LeftVentricleBasalWall6Segments3782);
            _uids.Add(DicomUID.LeftVentricleMidlevelWall6Segments3783.UID, DicomUID.LeftVentricleMidlevelWall6Segments3783);
            _uids.Add(DicomUID.LeftVentricleApicalWall4Segments3784.UID, DicomUID.LeftVentricleApicalWall4Segments3784);
            _uids.Add(DicomUID.LeftVentricleApicalWall6Segments3785.UID, DicomUID.LeftVentricleApicalWall6Segments3785);
            _uids.Add(DicomUID.PatientTreatmentPreparationMethod9571.UID, DicomUID.PatientTreatmentPreparationMethod9571);
            _uids.Add(DicomUID.PatientShieldingDevice9572.UID, DicomUID.PatientShieldingDevice9572);
            _uids.Add(DicomUID.PatientTreatmentPreparationDevice9573.UID, DicomUID.PatientTreatmentPreparationDevice9573);
            _uids.Add(DicomUID.PatientPositionDisplacementReferencePoint9574.UID, DicomUID.PatientPositionDisplacementReferencePoint9574);
            _uids.Add(DicomUID.PatientAlignmentDevice9575.UID, DicomUID.PatientAlignmentDevice9575);
            _uids.Add(DicomUID.ReasonsForRTRadiationTreatmentOmission9576.UID, DicomUID.ReasonsForRTRadiationTreatmentOmission9576);
            _uids.Add(DicomUID.PatientTreatmentPreparationProcedure9577.UID, DicomUID.PatientTreatmentPreparationProcedure9577);
            _uids.Add(DicomUID.MotionManagementSetupDevice9578.UID, DicomUID.MotionManagementSetupDevice9578);
            _uids.Add(DicomUID.CoreEchoStrainMeasurement12309.UID, DicomUID.CoreEchoStrainMeasurement12309);
            _uids.Add(DicomUID.MyocardialStrainMethod12310.UID, DicomUID.MyocardialStrainMethod12310);
            _uids.Add(DicomUID.EchoMeasuredStrainProperty12311.UID, DicomUID.EchoMeasuredStrainProperty12311);
            _uids.Add(DicomUID.AssessmentFromCADRADS3020.UID, DicomUID.AssessmentFromCADRADS3020);
            _uids.Add(DicomUID.CADRADSStenosisAssessmentModifier3021.UID, DicomUID.CADRADSStenosisAssessmentModifier3021);
            _uids.Add(DicomUID.CADRADSAssessmentModifier3022.UID, DicomUID.CADRADSAssessmentModifier3022);
            _uids.Add(DicomUID.RTSegmentMaterial9579.UID, DicomUID.RTSegmentMaterial9579);
            _uids.Add(DicomUID.VertebralAnatomicStructure7602.UID, DicomUID.VertebralAnatomicStructure7602);
            _uids.Add(DicomUID.Vertebra7603.UID, DicomUID.Vertebra7603);
            _uids.Add(DicomUID.IntervertebralDisc7604.UID, DicomUID.IntervertebralDisc7604);
            _uids.Add(DicomUID.ImagingProcedure101.UID, DicomUID.ImagingProcedure101);
            _uids.Add(DicomUID.NICIPShortCodeImagingProcedure103.UID, DicomUID.NICIPShortCodeImagingProcedure103);
            _uids.Add(DicomUID.NICIPSNOMEDImagingProcedure104.UID, DicomUID.NICIPSNOMEDImagingProcedure104);
            _uids.Add(DicomUID.ICD10PCSImagingProcedure105.UID, DicomUID.ICD10PCSImagingProcedure105);
            _uids.Add(DicomUID.ICD10PCSNuclearMedicineProcedure106.UID, DicomUID.ICD10PCSNuclearMedicineProcedure106);
            _uids.Add(DicomUID.ICD10PCSRadiationTherapyProcedure107.UID, DicomUID.ICD10PCSRadiationTherapyProcedure107);
            _uids.Add(DicomUID.RTSegmentationPropertyCategory9580.UID, DicomUID.RTSegmentationPropertyCategory9580);
            _uids.Add(DicomUID.RadiotherapyRegistrationMark9581.UID, DicomUID.RadiotherapyRegistrationMark9581);
            _uids.Add(DicomUID.RadiotherapyDoseRegion9582.UID, DicomUID.RadiotherapyDoseRegion9582);
            _uids.Add(DicomUID.AnatomicallyLocalizedLesionSegmentationType7199.UID, DicomUID.AnatomicallyLocalizedLesionSegmentationType7199);
            _uids.Add(DicomUID.ReasonForRemovalFromOperationalUse7031.UID, DicomUID.ReasonForRemovalFromOperationalUse7031);
            _uids.Add(DicomUID.GeneralUltrasoundReportDocumentTitle12320.UID, DicomUID.GeneralUltrasoundReportDocumentTitle12320);
            _uids.Add(DicomUID.ElastographySite12321.UID, DicomUID.ElastographySite12321);
            _uids.Add(DicomUID.ElastographyMeasurementSite12322.UID, DicomUID.ElastographyMeasurementSite12322);
            _uids.Add(DicomUID.UltrasoundRelevantPatientCondition12323.UID, DicomUID.UltrasoundRelevantPatientCondition12323);
            _uids.Add(DicomUID.ShearWaveDetectionMethod12324.UID, DicomUID.ShearWaveDetectionMethod12324);
            _uids.Add(DicomUID.LiverUltrasoundStudyIndication12325.UID, DicomUID.LiverUltrasoundStudyIndication12325);
            _uids.Add(DicomUID.AnalogWaveformFilter3042.UID, DicomUID.AnalogWaveformFilter3042);
            _uids.Add(DicomUID.DigitalWaveformFilter3043.UID, DicomUID.DigitalWaveformFilter3043);
            _uids.Add(DicomUID.WaveformFilterLookupTableInputFrequencyUnit3044.UID, DicomUID.WaveformFilterLookupTableInputFrequencyUnit3044);
            _uids.Add(DicomUID.WaveformFilterLookupTableOutputMagnitudeUnit3045.UID, DicomUID.WaveformFilterLookupTableOutputMagnitudeUnit3045);
            _uids.Add(DicomUID.SpecificObservationSubjectClass272.UID, DicomUID.SpecificObservationSubjectClass272);
            _uids.Add(DicomUID.MovableBeamLimitingDeviceType9540.UID, DicomUID.MovableBeamLimitingDeviceType9540);
            _uids.Add(DicomUID.RadiotherapyAcquisitionWorkItemSubtasks9260.UID, DicomUID.RadiotherapyAcquisitionWorkItemSubtasks9260);
            _uids.Add(DicomUID.PatientPositionAcquisitionRadiationSourceLocations9261.UID, DicomUID.PatientPositionAcquisitionRadiationSourceLocations9261);
            _uids.Add(DicomUID.EnergyDerivationTypes9262.UID, DicomUID.EnergyDerivationTypes9262);
            _uids.Add(DicomUID.KVImagingAcquisitionTechniques9263.UID, DicomUID.KVImagingAcquisitionTechniques9263);
            _uids.Add(DicomUID.MVImagingAcquisitionTechniques9264.UID, DicomUID.MVImagingAcquisitionTechniques9264);
            _uids.Add(DicomUID.PatientPositionAcquisitionProjectionTechniques9265.UID, DicomUID.PatientPositionAcquisitionProjectionTechniques9265);
            _uids.Add(DicomUID.PatientPositionAcquisitionCTTechniques9266.UID, DicomUID.PatientPositionAcquisitionCTTechniques9266);
            _uids.Add(DicomUID.PatientPositioningRelatedObjectPurposes9267.UID, DicomUID.PatientPositioningRelatedObjectPurposes9267);
            _uids.Add(DicomUID.PatientPositionAcquisitionDevices9268.UID, DicomUID.PatientPositionAcquisitionDevices9268);
            _uids.Add(DicomUID.RTRadiationMetersetUnits9269.UID, DicomUID.RTRadiationMetersetUnits9269);
            _uids.Add(DicomUID.AcquisitionInitiationTypes9270.UID, DicomUID.AcquisitionInitiationTypes9270);
            _uids.Add(DicomUID.RTImagePatientPositionAcquisitionDevices9271.UID, DicomUID.RTImagePatientPositionAcquisitionDevices9271);
            _uids.Add(DicomUID.PhotoacousticIlluminationMethod11001.UID, DicomUID.PhotoacousticIlluminationMethod11001);
            _uids.Add(DicomUID.AcousticCouplingMedium11002.UID, DicomUID.AcousticCouplingMedium11002);
            _uids.Add(DicomUID.UltrasoundTransducerTechnology11003.UID, DicomUID.UltrasoundTransducerTechnology11003);
            _uids.Add(DicomUID.SpeedOfSoundCorrectionMechanisms11004.UID, DicomUID.SpeedOfSoundCorrectionMechanisms11004);
            _uids.Add(DicomUID.PhotoacousticReconstructionAlgorithmFamily11005.UID, DicomUID.PhotoacousticReconstructionAlgorithmFamily11005);
            _uids.Add(DicomUID.PhotoacousticImagedProperty11006.UID, DicomUID.PhotoacousticImagedProperty11006);
            _uids.Add(DicomUID.XRayRadiationDoseProcedureTypeReported10005.UID, DicomUID.XRayRadiationDoseProcedureTypeReported10005);
            _uids.Add(DicomUID.TopicalTreatment4410.UID, DicomUID.TopicalTreatment4410);
            _uids.Add(DicomUID.LesionColor4411.UID, DicomUID.LesionColor4411);
            _uids.Add(DicomUID.SpecimenStainForConfocalMicroscopy4412.UID, DicomUID.SpecimenStainForConfocalMicroscopy4412);
            _uids.Add(DicomUID.RTROIImageAcquisitionContext9272.UID, DicomUID.RTROIImageAcquisitionContext9272);
            _uids.Add(DicomUID.LobeOfLung6170.UID, DicomUID.LobeOfLung6170);
            _uids.Add(DicomUID.ZoneOfLung6171.UID, DicomUID.ZoneOfLung6171);
            _uids.Add(DicomUID.SleepStage3046.UID, DicomUID.SleepStage3046);
            _uids.Add(DicomUID.PatientPositionAcquisitionMRTechniques9273.UID, DicomUID.PatientPositionAcquisitionMRTechniques9273);
            _uids.Add(DicomUID.RTPlanRadiotherapyProcedureTechnique9583.UID, DicomUID.RTPlanRadiotherapyProcedureTechnique9583);
            _uids.Add(DicomUID.WaveformAnnotationClassification3047.UID, DicomUID.WaveformAnnotationClassification3047);
            _uids.Add(DicomUID.WaveformAnnotationsDocumentTitle3048.UID, DicomUID.WaveformAnnotationsDocumentTitle3048);
            _uids.Add(DicomUID.EEGProcedure3049.UID, DicomUID.EEGProcedure3049);
            _uids.Add(DicomUID.PatientConsciousness3050.UID, DicomUID.PatientConsciousness3050);
        }

        ///<summary>SOP Class: Verification SOP Class</summary>
        public static readonly DicomUID Verification = new DicomUID("1.2.840.10008.1.1", "Verification SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Transfer Syntax: Implicit VR Little Endian: Default Transfer Syntax for DICOM</summary>
        public static readonly DicomUID ImplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2", "Implicit VR Little Endian: Default Transfer Syntax for DICOM", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Explicit VR Little Endian</summary>
        public static readonly DicomUID ExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1", "Explicit VR Little Endian", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Encapsulated Uncompressed Explicit VR Little Endian</summary>
        public static readonly DicomUID EncapsulatedUncompressedExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1.98", "Encapsulated Uncompressed Explicit VR Little Endian", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Deflated Explicit VR Little Endian</summary>
        public static readonly DicomUID DeflatedExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1.99", "Deflated Explicit VR Little Endian", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Explicit VR Big Endian (Retired)</summary>
        public static readonly DicomUID ExplicitVRBigEndianRETIRED = new DicomUID("1.2.840.10008.1.2.2", "Explicit VR Big Endian (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>Transfer Syntax: JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression</summary>
        public static readonly DicomUID JPEGBaseline8Bit = new DicomUID("1.2.840.10008.1.2.4.50", "JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG Extended (Process 2 &amp; 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)</summary>
        public static readonly DicomUID JPEGExtended12Bit = new DicomUID("1.2.840.10008.1.2.4.51", "JPEG Extended (Process 2 & 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)", DicomUidType.TransferSyntax, false);

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
        public static readonly DicomUID JPEGLossless = new DicomUID("1.2.840.10008.1.2.4.57", "JPEG Lossless, Non-Hierarchical (Process 14)", DicomUidType.TransferSyntax, false);

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
        public static readonly DicomUID JPEGLosslessSV1 = new DicomUID("1.2.840.10008.1.2.4.70", "JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG-LS Lossless Image Compression</summary>
        public static readonly DicomUID JPEGLSLossless = new DicomUID("1.2.840.10008.1.2.4.80", "JPEG-LS Lossless Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG-LS Lossy (Near-Lossless) Image Compression</summary>
        public static readonly DicomUID JPEGLSNearLossless = new DicomUID("1.2.840.10008.1.2.4.81", "JPEG-LS Lossy (Near-Lossless) Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Image Compression (Lossless Only)</summary>
        public static readonly DicomUID JPEG2000Lossless = new DicomUID("1.2.840.10008.1.2.4.90", "JPEG 2000 Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Image Compression</summary>
        public static readonly DicomUID JPEG2000 = new DicomUID("1.2.840.10008.1.2.4.91", "JPEG 2000 Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)</summary>
        public static readonly DicomUID JPEG2000MCLossless = new DicomUID("1.2.840.10008.1.2.4.92", "JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression</summary>
        public static readonly DicomUID JPEG2000MC = new DicomUID("1.2.840.10008.1.2.4.93", "JPEG 2000 Part 2 Multi-component Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPIP Referenced</summary>
        public static readonly DicomUID JPIPReferenced = new DicomUID("1.2.840.10008.1.2.4.94", "JPIP Referenced", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPIP Referenced Deflate</summary>
        public static readonly DicomUID JPIPReferencedDeflate = new DicomUID("1.2.840.10008.1.2.4.95", "JPIP Referenced Deflate", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG2 Main Profile / Main Level</summary>
        public static readonly DicomUID MPEG2MPML = new DicomUID("1.2.840.10008.1.2.4.100", "MPEG2 Main Profile / Main Level", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Fragmentable MPEG2 Main Profile / Main Level</summary>
        public static readonly DicomUID MPEG2MPMLF = new DicomUID("1.2.840.10008.1.2.4.100.1", "Fragmentable MPEG2 Main Profile / Main Level", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG2 Main Profile / High Level</summary>
        public static readonly DicomUID MPEG2MPHL = new DicomUID("1.2.840.10008.1.2.4.101", "MPEG2 Main Profile / High Level", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Fragmentable MPEG2 Main Profile / High Level</summary>
        public static readonly DicomUID MPEG2MPHLF = new DicomUID("1.2.840.10008.1.2.4.101.1", "Fragmentable MPEG2 Main Profile / High Level", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 High Profile / Level 4.1</summary>
        public static readonly DicomUID MPEG4HP41 = new DicomUID("1.2.840.10008.1.2.4.102", "MPEG-4 AVC/H.264 High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.1</summary>
        public static readonly DicomUID MPEG4HP41F = new DicomUID("1.2.840.10008.1.2.4.102.1", "Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1</summary>
        public static readonly DicomUID MPEG4HP41BD = new DicomUID("1.2.840.10008.1.2.4.103", "MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Fragmentable MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1</summary>
        public static readonly DicomUID MPEG4HP41BDF = new DicomUID("1.2.840.10008.1.2.4.103.1", "Fragmentable MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video</summary>
        public static readonly DicomUID MPEG4HP422D = new DicomUID("1.2.840.10008.1.2.4.104", "MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video</summary>
        public static readonly DicomUID MPEG4HP422DF = new DicomUID("1.2.840.10008.1.2.4.104.1", "Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.2 For 2D Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video</summary>
        public static readonly DicomUID MPEG4HP423D = new DicomUID("1.2.840.10008.1.2.4.105", "MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video</summary>
        public static readonly DicomUID MPEG4HP423DF = new DicomUID("1.2.840.10008.1.2.4.105.1", "Fragmentable MPEG-4 AVC/H.264 High Profile / Level 4.2 For 3D Video", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2</summary>
        public static readonly DicomUID MPEG4HP42STEREO = new DicomUID("1.2.840.10008.1.2.4.106", "MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: Fragmentable MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2</summary>
        public static readonly DicomUID MPEG4HP42STEREOF = new DicomUID("1.2.840.10008.1.2.4.106.1", "Fragmentable MPEG-4 AVC/H.264 Stereo High Profile / Level 4.2", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: HEVC/H.265 Main Profile / Level 5.1</summary>
        public static readonly DicomUID HEVCMP51 = new DicomUID("1.2.840.10008.1.2.4.107", "HEVC/H.265 Main Profile / Level 5.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: HEVC/H.265 Main 10 Profile / Level 5.1</summary>
        public static readonly DicomUID HEVCM10P51 = new DicomUID("1.2.840.10008.1.2.4.108", "HEVC/H.265 Main 10 Profile / Level 5.1", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: High-Throughput JPEG 2000 Image Compression (Lossless Only)</summary>
        public static readonly DicomUID HTJ2KLossless = new DicomUID("1.2.840.10008.1.2.4.201", "High-Throughput JPEG 2000 Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: High-Throughput JPEG 2000 with RPCL Options Image Compression (Lossless Only)</summary>
        public static readonly DicomUID HTJ2KLosslessRPCL = new DicomUID("1.2.840.10008.1.2.4.202", "High-Throughput JPEG 2000 with RPCL Options Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: High-Throughput JPEG 2000 Image Compression</summary>
        public static readonly DicomUID HTJ2K = new DicomUID("1.2.840.10008.1.2.4.203", "High-Throughput JPEG 2000 Image Compression", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPIP HTJ2K Referenced</summary>
        public static readonly DicomUID JPIPHTJ2KReferenced = new DicomUID("1.2.840.10008.1.2.4.204", "JPIP HTJ2K Referenced", DicomUidType.TransferSyntax, false);

        ///<summary>Transfer Syntax: JPIP HTJ2K Referenced Deflate</summary>
        public static readonly DicomUID JPIPHTJ2KReferencedDeflate = new DicomUID("1.2.840.10008.1.2.4.205", "JPIP HTJ2K Referenced Deflate", DicomUidType.TransferSyntax, false);

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

        ///<summary>Well-known SOP Instance: Hot Iron Color Palette SOP Instance</summary>
        public static readonly DicomUID HotIronPalette = new DicomUID("1.2.840.10008.1.5.1", "Hot Iron Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: PET Color Palette SOP Instance</summary>
        public static readonly DicomUID PETPalette = new DicomUID("1.2.840.10008.1.5.2", "PET Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Hot Metal Blue Color Palette SOP Instance</summary>
        public static readonly DicomUID HotMetalBluePalette = new DicomUID("1.2.840.10008.1.5.3", "Hot Metal Blue Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: PET 20 Step Color Palette SOP Instance</summary>
        public static readonly DicomUID PET20StepPalette = new DicomUID("1.2.840.10008.1.5.4", "PET 20 Step Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Spring Color Palette SOP Instance</summary>
        public static readonly DicomUID SpringPalette = new DicomUID("1.2.840.10008.1.5.5", "Spring Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Summer Color Palette SOP Instance</summary>
        public static readonly DicomUID SummerPalette = new DicomUID("1.2.840.10008.1.5.6", "Summer Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Fall Color Palette SOP Instance</summary>
        public static readonly DicomUID FallPalette = new DicomUID("1.2.840.10008.1.5.7", "Fall Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Winter Color Palette SOP Instance</summary>
        public static readonly DicomUID WinterPalette = new DicomUID("1.2.840.10008.1.5.8", "Winter Color Palette SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>SOP Class: Basic Study Content Notification SOP Class (Retired)</summary>
        public static readonly DicomUID BasicStudyContentNotificationRETIRED = new DicomUID("1.2.840.10008.1.9", "Basic Study Content Notification SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Transfer Syntax: Papyrus 3 Implicit VR Little Endian (Retired)</summary>
        public static readonly DicomUID Papyrus3ImplicitVRLittleEndianRETIRED = new DicomUID("1.2.840.10008.1.20", "Papyrus 3 Implicit VR Little Endian (Retired)", DicomUidType.TransferSyntax, true);

        ///<summary>SOP Class: Storage Commitment Push Model SOP Class</summary>
        public static readonly DicomUID StorageCommitmentPushModel = new DicomUID("1.2.840.10008.1.20.1", "Storage Commitment Push Model SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Storage Commitment Push Model SOP Instance</summary>
        public static readonly DicomUID StorageCommitmentPushModelInstance = new DicomUID("1.2.840.10008.1.20.1.1", "Storage Commitment Push Model SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>SOP Class: Storage Commitment Pull Model SOP Class (Retired)</summary>
        public static readonly DicomUID StorageCommitmentPullModelRETIRED = new DicomUID("1.2.840.10008.1.20.2", "Storage Commitment Pull Model SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Well-known SOP Instance: Storage Commitment Pull Model SOP Instance (Retired)</summary>
        public static readonly DicomUID StorageCommitmentPullModelInstanceRETIRED = new DicomUID("1.2.840.10008.1.20.2.1", "Storage Commitment Pull Model SOP Instance (Retired)", DicomUidType.SOPInstance, true);

        ///<summary>SOP Class: Procedural Event Logging SOP Class</summary>
        public static readonly DicomUID ProceduralEventLogging = new DicomUID("1.2.840.10008.1.40", "Procedural Event Logging SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Procedural Event Logging SOP Instance</summary>
        public static readonly DicomUID ProceduralEventLoggingInstance = new DicomUID("1.2.840.10008.1.40.1", "Procedural Event Logging SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>SOP Class: Substance Administration Logging SOP Class</summary>
        public static readonly DicomUID SubstanceAdministrationLogging = new DicomUID("1.2.840.10008.1.42", "Substance Administration Logging SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Substance Administration Logging SOP Instance</summary>
        public static readonly DicomUID SubstanceAdministrationLoggingInstance = new DicomUID("1.2.840.10008.1.42.1", "Substance Administration Logging SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>DICOM UIDs as a Coding Scheme: DICOM UID Registry</summary>
        public static readonly DicomUID DCMUID = new DicomUID("1.2.840.10008.2.6.1", "DICOM UID Registry", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: DICOM Controlled Terminology</summary>
        public static readonly DicomUID DCM = new DicomUID("1.2.840.10008.2.16.4", "DICOM Controlled Terminology", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Adult Mouse Anatomy Ontology</summary>
        public static readonly DicomUID MA = new DicomUID("1.2.840.10008.2.16.5", "Adult Mouse Anatomy Ontology", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Uberon Ontology</summary>
        public static readonly DicomUID UBERON = new DicomUID("1.2.840.10008.2.16.6", "Uberon Ontology", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Integrated Taxonomic Information System (ITIS) Taxonomic Serial Number (TSN)</summary>
        public static readonly DicomUID ITIS_TSN = new DicomUID("1.2.840.10008.2.16.7", "Integrated Taxonomic Information System (ITIS) Taxonomic Serial Number (TSN)", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Mouse Genome Initiative (MGI)</summary>
        public static readonly DicomUID MGI = new DicomUID("1.2.840.10008.2.16.8", "Mouse Genome Initiative (MGI)", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: PubChem Compound CID</summary>
        public static readonly DicomUID PUBCHEM_CID = new DicomUID("1.2.840.10008.2.16.9", "PubChem Compound CID", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Dublin Core</summary>
        public static readonly DicomUID DC = new DicomUID("1.2.840.10008.2.16.10", "Dublin Core", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: New York University Melanoma Clinical Cooperative Group</summary>
        public static readonly DicomUID NYUMCCG = new DicomUID("1.2.840.10008.2.16.11", "New York University Melanoma Clinical Cooperative Group", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Mayo Clinic Non-radiological Images Specific Body Structure Anatomical Surface Region Guide</summary>
        public static readonly DicomUID MAYONRISBSASRG = new DicomUID("1.2.840.10008.2.16.12", "Mayo Clinic Non-radiological Images Specific Body Structure Anatomical Surface Region Guide", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Image Biomarker Standardisation Initiative</summary>
        public static readonly DicomUID IBSI = new DicomUID("1.2.840.10008.2.16.13", "Image Biomarker Standardisation Initiative", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Radiomics Ontology</summary>
        public static readonly DicomUID RO = new DicomUID("1.2.840.10008.2.16.14", "Radiomics Ontology", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: RadElement</summary>
        public static readonly DicomUID RADELEMENT = new DicomUID("1.2.840.10008.2.16.15", "RadElement", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: ICD-11</summary>
        public static readonly DicomUID I11 = new DicomUID("1.2.840.10008.2.16.16", "ICD-11", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Unified numbering system (UNS) for metals and alloys</summary>
        public static readonly DicomUID UNS = new DicomUID("1.2.840.10008.2.16.17", "Unified numbering system (UNS) for metals and alloys", DicomUidType.CodingScheme, false);

        ///<summary>Coding Scheme: Research Resource Identification</summary>
        public static readonly DicomUID RRID = new DicomUID("1.2.840.10008.2.16.18", "Research Resource Identification", DicomUidType.CodingScheme, false);

        ///<summary>Application Context Name: DICOM Application Context Name</summary>
        public static readonly DicomUID DICOMApplicationContext = new DicomUID("1.2.840.10008.3.1.1.1", "DICOM Application Context Name", DicomUidType.ApplicationContextName, false);

        ///<summary>SOP Class: Detached Patient Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedPatientManagementRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.1", "Detached Patient Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Detached Patient Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedPatientManagementMetaRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.4", "Detached Patient Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Detached Visit Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedVisitManagementRETIRED = new DicomUID("1.2.840.10008.3.1.2.2.1", "Detached Visit Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Detached Study Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedStudyManagementRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.1", "Detached Study Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Study Component Management SOP Class (Retired)</summary>
        public static readonly DicomUID StudyComponentManagementRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.2", "Study Component Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Modality Performed Procedure Step SOP Class</summary>
        public static readonly DicomUID ModalityPerformedProcedureStep = new DicomUID("1.2.840.10008.3.1.2.3.3", "Modality Performed Procedure Step SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Modality Performed Procedure Step Retrieve SOP Class</summary>
        public static readonly DicomUID ModalityPerformedProcedureStepRetrieve = new DicomUID("1.2.840.10008.3.1.2.3.4", "Modality Performed Procedure Step Retrieve SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Modality Performed Procedure Step Notification SOP Class</summary>
        public static readonly DicomUID ModalityPerformedProcedureStepNotification = new DicomUID("1.2.840.10008.3.1.2.3.5", "Modality Performed Procedure Step Notification SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Detached Results Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedResultsManagementRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.1", "Detached Results Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Detached Results Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedResultsManagementMetaRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.4", "Detached Results Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>Meta SOP Class: Detached Study Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedStudyManagementMetaRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.5", "Detached Study Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Detached Interpretation Management SOP Class (Retired)</summary>
        public static readonly DicomUID DetachedInterpretationManagementRETIRED = new DicomUID("1.2.840.10008.3.1.2.6.1", "Detached Interpretation Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Service Class: Storage Service Class</summary>
        public static readonly DicomUID Storage = new DicomUID("1.2.840.10008.4.2", "Storage Service Class", DicomUidType.ServiceClass, false);

        ///<summary>SOP Class: Basic Film Session SOP Class</summary>
        public static readonly DicomUID BasicFilmSession = new DicomUID("1.2.840.10008.5.1.1.1", "Basic Film Session SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Film Box SOP Class</summary>
        public static readonly DicomUID BasicFilmBox = new DicomUID("1.2.840.10008.5.1.1.2", "Basic Film Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Grayscale Image Box SOP Class</summary>
        public static readonly DicomUID BasicGrayscaleImageBox = new DicomUID("1.2.840.10008.5.1.1.4", "Basic Grayscale Image Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Color Image Box SOP Class</summary>
        public static readonly DicomUID BasicColorImageBox = new DicomUID("1.2.840.10008.5.1.1.4.1", "Basic Color Image Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Referenced Image Box SOP Class (Retired)</summary>
        public static readonly DicomUID ReferencedImageBoxRETIRED = new DicomUID("1.2.840.10008.5.1.1.4.2", "Referenced Image Box SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Basic Grayscale Print Management Meta SOP Class</summary>
        public static readonly DicomUID BasicGrayscalePrintManagementMeta = new DicomUID("1.2.840.10008.5.1.1.9", "Basic Grayscale Print Management Meta SOP Class", DicomUidType.MetaSOPClass, false);

        ///<summary>Meta SOP Class: Referenced Grayscale Print Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID ReferencedGrayscalePrintManagementMetaRETIRED = new DicomUID("1.2.840.10008.5.1.1.9.1", "Referenced Grayscale Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Print Job SOP Class</summary>
        public static readonly DicomUID PrintJob = new DicomUID("1.2.840.10008.5.1.1.14", "Print Job SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Basic Annotation Box SOP Class</summary>
        public static readonly DicomUID BasicAnnotationBox = new DicomUID("1.2.840.10008.5.1.1.15", "Basic Annotation Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Printer SOP Class</summary>
        public static readonly DicomUID Printer = new DicomUID("1.2.840.10008.5.1.1.16", "Printer SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Printer Configuration Retrieval SOP Class</summary>
        public static readonly DicomUID PrinterConfigurationRetrieval = new DicomUID("1.2.840.10008.5.1.1.16.376", "Printer Configuration Retrieval SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Printer SOP Instance</summary>
        public static readonly DicomUID PrinterInstance = new DicomUID("1.2.840.10008.5.1.1.17", "Printer SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: Printer Configuration Retrieval SOP Instance</summary>
        public static readonly DicomUID PrinterConfigurationRetrievalInstance = new DicomUID("1.2.840.10008.5.1.1.17.376", "Printer Configuration Retrieval SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Meta SOP Class: Basic Color Print Management Meta SOP Class</summary>
        public static readonly DicomUID BasicColorPrintManagementMeta = new DicomUID("1.2.840.10008.5.1.1.18", "Basic Color Print Management Meta SOP Class", DicomUidType.MetaSOPClass, false);

        ///<summary>Meta SOP Class: Referenced Color Print Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID ReferencedColorPrintManagementMetaRETIRED = new DicomUID("1.2.840.10008.5.1.1.18.1", "Referenced Color Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: VOI LUT Box SOP Class</summary>
        public static readonly DicomUID VOILUTBox = new DicomUID("1.2.840.10008.5.1.1.22", "VOI LUT Box SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Presentation LUT SOP Class</summary>
        public static readonly DicomUID PresentationLUT = new DicomUID("1.2.840.10008.5.1.1.23", "Presentation LUT SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Image Overlay Box SOP Class (Retired)</summary>
        public static readonly DicomUID ImageOverlayBoxRETIRED = new DicomUID("1.2.840.10008.5.1.1.24", "Image Overlay Box SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Basic Print Image Overlay Box SOP Class (Retired)</summary>
        public static readonly DicomUID BasicPrintImageOverlayBoxRETIRED = new DicomUID("1.2.840.10008.5.1.1.24.1", "Basic Print Image Overlay Box SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Well-known SOP Instance: Print Queue SOP Instance (Retired)</summary>
        public static readonly DicomUID PrintQueueInstanceRETIRED = new DicomUID("1.2.840.10008.5.1.1.25", "Print Queue SOP Instance (Retired)", DicomUidType.SOPInstance, true);

        ///<summary>SOP Class: Print Queue Management SOP Class (Retired)</summary>
        public static readonly DicomUID PrintQueueManagementRETIRED = new DicomUID("1.2.840.10008.5.1.1.26", "Print Queue Management SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Stored Print Storage SOP Class (Retired)</summary>
        public static readonly DicomUID StoredPrintStorageRETIRED = new DicomUID("1.2.840.10008.5.1.1.27", "Stored Print Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Hardcopy Grayscale Image Storage SOP Class (Retired)</summary>
        public static readonly DicomUID HardcopyGrayscaleImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.1.29", "Hardcopy Grayscale Image Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Hardcopy Color Image Storage SOP Class (Retired)</summary>
        public static readonly DicomUID HardcopyColorImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.1.30", "Hardcopy Color Image Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Pull Print Request SOP Class (Retired)</summary>
        public static readonly DicomUID PullPrintRequestRETIRED = new DicomUID("1.2.840.10008.5.1.1.31", "Pull Print Request SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Meta SOP Class: Pull Stored Print Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID PullStoredPrintManagementMetaRETIRED = new DicomUID("1.2.840.10008.5.1.1.32", "Pull Stored Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: Media Creation Management SOP Class UID</summary>
        public static readonly DicomUID MediaCreationManagement = new DicomUID("1.2.840.10008.5.1.1.33", "Media Creation Management SOP Class UID", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Display System SOP Class</summary>
        public static readonly DicomUID DisplaySystem = new DicomUID("1.2.840.10008.5.1.1.40", "Display System SOP Class", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Display System SOP Instance</summary>
        public static readonly DicomUID DisplaySystemInstance = new DicomUID("1.2.840.10008.5.1.1.40.1", "Display System SOP Instance", DicomUidType.SOPInstance, false);

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
        public static readonly DicomUID UltrasoundMultiFrameImageStorageRetiredRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.3", "Ultrasound Multi-frame Image Storage (Retired)", DicomUidType.SOPClass, true);

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
        public static readonly DicomUID NuclearMedicineImageStorageRetiredRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.5", "Nuclear Medicine Image Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Ultrasound Image Storage (Retired)</summary>
        public static readonly DicomUID UltrasoundImageStorageRetiredRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.6", "Ultrasound Image Storage (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Ultrasound Image Storage</summary>
        public static readonly DicomUID UltrasoundImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.1", "Ultrasound Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced US Volume Storage</summary>
        public static readonly DicomUID EnhancedUSVolumeStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.2", "Enhanced US Volume Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Photoacoustic Image Storage </summary>
        public static readonly DicomUID PhotoacousticImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.3", "Photoacoustic Image Storage ", DicomUidType.SOPClass, false);

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

        ///<summary>SOP Class: General 32-bit ECG Waveform Storage</summary>
        public static readonly DicomUID General32bitECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.4", "General 32-bit ECG Waveform Storage", DicomUidType.SOPClass, false);

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

        ///<summary>SOP Class: Multi-channel Respiratory Waveform Storage</summary>
        public static readonly DicomUID MultichannelRespiratoryWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.6.2", "Multi-channel Respiratory Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Routine Scalp Electroencephalogram Waveform Storage</summary>
        public static readonly DicomUID RoutineScalpElectroencephalogramWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.7.1", "Routine Scalp Electroencephalogram Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Electromyogram Waveform Storage</summary>
        public static readonly DicomUID ElectromyogramWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.7.2", "Electromyogram Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Electrooculogram Waveform Storage</summary>
        public static readonly DicomUID ElectrooculogramWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.7.3", "Electrooculogram Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Sleep Electroencephalogram Waveform Storage</summary>
        public static readonly DicomUID SleepElectroencephalogramWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.7.4", "Sleep Electroencephalogram Waveform Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Body Position Waveform Storage</summary>
        public static readonly DicomUID BodyPositionWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.8.1", "Body Position Waveform Storage", DicomUidType.SOPClass, false);

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

        ///<summary>SOP Class: Variable Modality LUT Softcopy Presentation State Storage</summary>
        public static readonly DicomUID VariableModalityLUTSoftcopyPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.12", "Variable Modality LUT Softcopy Presentation State Storage", DicomUidType.SOPClass, false);

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
        public static readonly DicomUID OphthalmicOpticalCoherenceTomographyBscanVolumeAnalysisStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.8", "Ophthalmic Optical Coherence Tomography B-scan Volume Analysis Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: VL Whole Slide Microscopy Image Storage</summary>
        public static readonly DicomUID VLWholeSlideMicroscopyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.6", "VL Whole Slide Microscopy Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Dermoscopic Photography Image Storage</summary>
        public static readonly DicomUID DermoscopicPhotographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.7", "Dermoscopic Photography Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Confocal Microscopy Image Storage</summary>
        public static readonly DicomUID ConfocalMicroscopyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.8", "Confocal Microscopy Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Confocal Microscopy Tiled Pyramidal Image Storage</summary>
        public static readonly DicomUID ConfocalMicroscopyTiledPyramidalImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.9", "Confocal Microscopy Tiled Pyramidal Image Storage", DicomUidType.SOPClass, false);

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

        ///<summary>SOP Class: Enhanced X-Ray Radiation Dose SR Storage</summary>
        public static readonly DicomUID EnhancedXRayRadiationDoseSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.76", "Enhanced X-Ray Radiation Dose SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Waveform Annotation SR Storage</summary>
        public static readonly DicomUID WaveformAnnotationSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.77", "Waveform Annotation SR Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Content Assessment Results Storage</summary>
        public static readonly DicomUID ContentAssessmentResultsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.90.1", "Content Assessment Results Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Microscopy Bulk Simple Annotations Storage</summary>
        public static readonly DicomUID MicroscopyBulkSimpleAnnotationsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.91.1", "Microscopy Bulk Simple Annotations Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated PDF Storage</summary>
        public static readonly DicomUID EncapsulatedPDFStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.1", "Encapsulated PDF Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated CDA Storage</summary>
        public static readonly DicomUID EncapsulatedCDAStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.2", "Encapsulated CDA Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated STL Storage</summary>
        public static readonly DicomUID EncapsulatedSTLStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.3", "Encapsulated STL Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated OBJ Storage</summary>
        public static readonly DicomUID EncapsulatedOBJStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.4", "Encapsulated OBJ Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Encapsulated MTL Storage</summary>
        public static readonly DicomUID EncapsulatedMTLStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.5", "Encapsulated MTL Storage", DicomUidType.SOPClass, false);

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
        public static readonly DicomUID ProtocolApprovalInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.1.1.200.4", "Protocol Approval Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Protocol Approval Information Model - MOVE</summary>
        public static readonly DicomUID ProtocolApprovalInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.1.1.200.5", "Protocol Approval Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Protocol Approval Information Model - GET</summary>
        public static readonly DicomUID ProtocolApprovalInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.1.1.200.6", "Protocol Approval Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: XA Defined Procedure Protocol Storage</summary>
        public static readonly DicomUID XADefinedProcedureProtocolStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.200.7", "XA Defined Procedure Protocol Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: XA Performed Procedure Protocol Storage</summary>
        public static readonly DicomUID XAPerformedProcedureProtocolStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.200.8", "XA Performed Procedure Protocol Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Inventory Storage</summary>
        public static readonly DicomUID InventoryStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.201.1", "Inventory Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Inventory - FIND</summary>
        public static readonly DicomUID InventoryFind = new DicomUID("1.2.840.10008.5.1.4.1.1.201.2", "Inventory - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Inventory - MOVE</summary>
        public static readonly DicomUID InventoryMove = new DicomUID("1.2.840.10008.5.1.4.1.1.201.3", "Inventory - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Inventory - GET</summary>
        public static readonly DicomUID InventoryGet = new DicomUID("1.2.840.10008.5.1.4.1.1.201.4", "Inventory - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Inventory Creation</summary>
        public static readonly DicomUID InventoryCreation = new DicomUID("1.2.840.10008.5.1.4.1.1.201.5", "Inventory Creation", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Repository Query</summary>
        public static readonly DicomUID RepositoryQuery = new DicomUID("1.2.840.10008.5.1.4.1.1.201.6", "Repository Query", DicomUidType.SOPClass, false);

        ///<summary>Well-known SOP Instance: Storage Management SOP Instance</summary>
        public static readonly DicomUID StorageManagementInstance = new DicomUID("1.2.840.10008.5.1.4.1.1.201.1.1", "Storage Management SOP Instance", DicomUidType.SOPInstance, false);

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

        ///<summary>SOP Class: Tomotherapeutic Radiation Storage</summary>
        public static readonly DicomUID TomotherapeuticRadiationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.14", "Tomotherapeutic Radiation Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Robotic-Arm Radiation Storage</summary>
        public static readonly DicomUID RoboticArmRadiationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.15", "Robotic-Arm Radiation Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Radiation Record Set Storage</summary>
        public static readonly DicomUID RTRadiationRecordSetStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.16", "RT Radiation Record Set Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Radiation Salvage Record Storage</summary>
        public static readonly DicomUID RTRadiationSalvageRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.17", "RT Radiation Salvage Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Tomotherapeutic Radiation Record Storage</summary>
        public static readonly DicomUID TomotherapeuticRadiationRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.18", "Tomotherapeutic Radiation Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: C-Arm Photon-Electron Radiation Record Storage</summary>
        public static readonly DicomUID CArmPhotonElectronRadiationRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.19", "C-Arm Photon-Electron Radiation Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Robotic Radiation Record Storage</summary>
        public static readonly DicomUID RoboticRadiationRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.20", "Robotic Radiation Record Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Radiation Set Delivery Instruction Storage</summary>
        public static readonly DicomUID RTRadiationSetDeliveryInstructionStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.21", "RT Radiation Set Delivery Instruction Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Treatment Preparation Storage</summary>
        public static readonly DicomUID RTTreatmentPreparationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.22", "RT Treatment Preparation Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced RT Image Storage</summary>
        public static readonly DicomUID EnhancedRTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.23", "Enhanced RT Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Enhanced Continuous RT Image Storage</summary>
        public static readonly DicomUID EnhancedContinuousRTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.24", "Enhanced Continuous RT Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Patient Position Acquisition Instruction Storage</summary>
        public static readonly DicomUID RTPatientPositionAcquisitionInstructionStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.25", "RT Patient Position Acquisition Instruction Storage", DicomUidType.SOPClass, false);

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
        public static readonly DicomUID DICOSQuadrupoleResonanceStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.6", "DICOS Quadrupole Resonance (QR) Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Eddy Current Image Storage</summary>
        public static readonly DicomUID EddyCurrentImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.601.1", "Eddy Current Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Eddy Current Multi-frame Image Storage</summary>
        public static readonly DicomUID EddyCurrentMultiFrameImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.601.2", "Eddy Current Multi-frame Image Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient Root Query/Retrieve Information Model - FIND</summary>
        public static readonly DicomUID PatientRootQueryRetrieveInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.1.2.1.1", "Patient Root Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient Root Query/Retrieve Information Model - MOVE</summary>
        public static readonly DicomUID PatientRootQueryRetrieveInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.1.2.1.2", "Patient Root Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient Root Query/Retrieve Information Model - GET</summary>
        public static readonly DicomUID PatientRootQueryRetrieveInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.1.2.1.3", "Patient Root Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Study Root Query/Retrieve Information Model - FIND</summary>
        public static readonly DicomUID StudyRootQueryRetrieveInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.1.2.2.1", "Study Root Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Study Root Query/Retrieve Information Model - MOVE</summary>
        public static readonly DicomUID StudyRootQueryRetrieveInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.1.2.2.2", "Study Root Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Study Root Query/Retrieve Information Model - GET</summary>
        public static readonly DicomUID StudyRootQueryRetrieveInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.1.2.2.3", "Study Root Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - FIND (Retired)</summary>
        public static readonly DicomUID PatientStudyOnlyQueryRetrieveInformationModelFindRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.1", "Patient/Study Only Query/Retrieve Information Model - FIND (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - MOVE (Retired)</summary>
        public static readonly DicomUID PatientStudyOnlyQueryRetrieveInformationModelMoveRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.2", "Patient/Study Only Query/Retrieve Information Model - MOVE (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - GET (Retired)</summary>
        public static readonly DicomUID PatientStudyOnlyQueryRetrieveInformationModelGetRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.3", "Patient/Study Only Query/Retrieve Information Model - GET (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Composite Instance Root Retrieve - MOVE</summary>
        public static readonly DicomUID CompositeInstanceRootRetrieveMove = new DicomUID("1.2.840.10008.5.1.4.1.2.4.2", "Composite Instance Root Retrieve - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Composite Instance Root Retrieve - GET</summary>
        public static readonly DicomUID CompositeInstanceRootRetrieveGet = new DicomUID("1.2.840.10008.5.1.4.1.2.4.3", "Composite Instance Root Retrieve - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Composite Instance Retrieve Without Bulk Data - GET</summary>
        public static readonly DicomUID CompositeInstanceRetrieveWithoutBulkDataGet = new DicomUID("1.2.840.10008.5.1.4.1.2.5.3", "Composite Instance Retrieve Without Bulk Data - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Defined Procedure Protocol Information Model - FIND</summary>
        public static readonly DicomUID DefinedProcedureProtocolInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.20.1", "Defined Procedure Protocol Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Defined Procedure Protocol Information Model - MOVE</summary>
        public static readonly DicomUID DefinedProcedureProtocolInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.20.2", "Defined Procedure Protocol Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Defined Procedure Protocol Information Model - GET</summary>
        public static readonly DicomUID DefinedProcedureProtocolInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.20.3", "Defined Procedure Protocol Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Modality Worklist Information Model - FIND</summary>
        public static readonly DicomUID ModalityWorklistInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.31", "Modality Worklist Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>Meta SOP Class: General Purpose Worklist Management Meta SOP Class (Retired)</summary>
        public static readonly DicomUID GeneralPurposeWorklistManagementMetaRETIRED = new DicomUID("1.2.840.10008.5.1.4.32", "General Purpose Worklist Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

        ///<summary>SOP Class: General Purpose Worklist Information Model - FIND (Retired)</summary>
        public static readonly DicomUID GeneralPurposeWorklistInformationModelFindRETIRED = new DicomUID("1.2.840.10008.5.1.4.32.1", "General Purpose Worklist Information Model - FIND (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: General Purpose Scheduled Procedure Step SOP Class (Retired)</summary>
        public static readonly DicomUID GeneralPurposeScheduledProcedureStepRETIRED = new DicomUID("1.2.840.10008.5.1.4.32.2", "General Purpose Scheduled Procedure Step SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: General Purpose Performed Procedure Step SOP Class (Retired)</summary>
        public static readonly DicomUID GeneralPurposePerformedProcedureStepRETIRED = new DicomUID("1.2.840.10008.5.1.4.32.3", "General Purpose Performed Procedure Step SOP Class (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Instance Availability Notification SOP Class</summary>
        public static readonly DicomUID InstanceAvailabilityNotification = new DicomUID("1.2.840.10008.5.1.4.33", "Instance Availability Notification SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: RT Beams Delivery Instruction Storage - Trial (Retired)</summary>
        public static readonly DicomUID RTBeamsDeliveryInstructionStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.1", "RT Beams Delivery Instruction Storage - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: RT Conventional Machine Verification - Trial (Retired)</summary>
        public static readonly DicomUID RTConventionalMachineVerificationTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.2", "RT Conventional Machine Verification - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: RT Ion Machine Verification - Trial (Retired)</summary>
        public static readonly DicomUID RTIonMachineVerificationTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.3", "RT Ion Machine Verification - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Service Class: Unified Worklist and Procedure Step Service Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedWorklistAndProcedureStepTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4", "Unified Worklist and Procedure Step Service Class - Trial (Retired)", DicomUidType.ServiceClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Push SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepPushTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.1", "Unified Procedure Step - Push SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Watch SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepWatchTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.2", "Unified Procedure Step - Watch SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Pull SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepPullTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.3", "Unified Procedure Step - Pull SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>SOP Class: Unified Procedure Step - Event SOP Class - Trial (Retired)</summary>
        public static readonly DicomUID UnifiedProcedureStepEventTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.4", "Unified Procedure Step - Event SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

        ///<summary>Well-known SOP Instance: UPS Global Subscription SOP Instance</summary>
        public static readonly DicomUID UPSGlobalSubscriptionInstance = new DicomUID("1.2.840.10008.5.1.4.34.5", "UPS Global Subscription SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Well-known SOP Instance: UPS Filtered Global Subscription SOP Instance</summary>
        public static readonly DicomUID UPSFilteredGlobalSubscriptionInstance = new DicomUID("1.2.840.10008.5.1.4.34.5.1", "UPS Filtered Global Subscription SOP Instance", DicomUidType.SOPInstance, false);

        ///<summary>Service Class: Unified Worklist and Procedure Step Service Class</summary>
        public static readonly DicomUID UnifiedWorklistAndProcedureStep = new DicomUID("1.2.840.10008.5.1.4.34.6", "Unified Worklist and Procedure Step Service Class", DicomUidType.ServiceClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Push SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepPush = new DicomUID("1.2.840.10008.5.1.4.34.6.1", "Unified Procedure Step - Push SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Watch SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepWatch = new DicomUID("1.2.840.10008.5.1.4.34.6.2", "Unified Procedure Step - Watch SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Pull SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepPull = new DicomUID("1.2.840.10008.5.1.4.34.6.3", "Unified Procedure Step - Pull SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Event SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepEvent = new DicomUID("1.2.840.10008.5.1.4.34.6.4", "Unified Procedure Step - Event SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Unified Procedure Step - Query SOP Class</summary>
        public static readonly DicomUID UnifiedProcedureStepQuery = new DicomUID("1.2.840.10008.5.1.4.34.6.5", "Unified Procedure Step - Query SOP Class", DicomUidType.SOPClass, false);

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
        public static readonly DicomUID HangingProtocolInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.38.2", "Hanging Protocol Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Hanging Protocol Information Model - MOVE</summary>
        public static readonly DicomUID HangingProtocolInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.38.3", "Hanging Protocol Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Hanging Protocol Information Model - GET</summary>
        public static readonly DicomUID HangingProtocolInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.38.4", "Hanging Protocol Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Storage</summary>
        public static readonly DicomUID ColorPaletteStorage = new DicomUID("1.2.840.10008.5.1.4.39.1", "Color Palette Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Query/Retrieve Information Model - FIND</summary>
        public static readonly DicomUID ColorPaletteQueryRetrieveInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.39.2", "Color Palette Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Query/Retrieve Information Model - MOVE</summary>
        public static readonly DicomUID ColorPaletteQueryRetrieveInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.39.3", "Color Palette Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Color Palette Query/Retrieve Information Model - GET</summary>
        public static readonly DicomUID ColorPaletteQueryRetrieveInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.39.4", "Color Palette Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Product Characteristics Query SOP Class</summary>
        public static readonly DicomUID ProductCharacteristicsQuery = new DicomUID("1.2.840.10008.5.1.4.41", "Product Characteristics Query SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Substance Approval Query SOP Class</summary>
        public static readonly DicomUID SubstanceApprovalQuery = new DicomUID("1.2.840.10008.5.1.4.42", "Substance Approval Query SOP Class", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Storage</summary>
        public static readonly DicomUID GenericImplantTemplateStorage = new DicomUID("1.2.840.10008.5.1.4.43.1", "Generic Implant Template Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Information Model - FIND</summary>
        public static readonly DicomUID GenericImplantTemplateInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.43.2", "Generic Implant Template Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Information Model - MOVE</summary>
        public static readonly DicomUID GenericImplantTemplateInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.43.3", "Generic Implant Template Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Generic Implant Template Information Model - GET</summary>
        public static readonly DicomUID GenericImplantTemplateInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.43.4", "Generic Implant Template Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Storage</summary>
        public static readonly DicomUID ImplantAssemblyTemplateStorage = new DicomUID("1.2.840.10008.5.1.4.44.1", "Implant Assembly Template Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Information Model - FIND</summary>
        public static readonly DicomUID ImplantAssemblyTemplateInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.44.2", "Implant Assembly Template Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Information Model - MOVE</summary>
        public static readonly DicomUID ImplantAssemblyTemplateInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.44.3", "Implant Assembly Template Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Assembly Template Information Model - GET</summary>
        public static readonly DicomUID ImplantAssemblyTemplateInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.44.4", "Implant Assembly Template Information Model - GET", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Storage</summary>
        public static readonly DicomUID ImplantTemplateGroupStorage = new DicomUID("1.2.840.10008.5.1.4.45.1", "Implant Template Group Storage", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Information Model - FIND</summary>
        public static readonly DicomUID ImplantTemplateGroupInformationModelFind = new DicomUID("1.2.840.10008.5.1.4.45.2", "Implant Template Group Information Model - FIND", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Information Model - MOVE</summary>
        public static readonly DicomUID ImplantTemplateGroupInformationModelMove = new DicomUID("1.2.840.10008.5.1.4.45.3", "Implant Template Group Information Model - MOVE", DicomUidType.SOPClass, false);

        ///<summary>SOP Class: Implant Template Group Information Model - GET</summary>
        public static readonly DicomUID ImplantTemplateGroupInformationModelGet = new DicomUID("1.2.840.10008.5.1.4.45.4", "Implant Template Group Information Model - GET", DicomUidType.SOPClass, false);

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
        public static readonly DicomUID UTC = new DicomUID("1.2.840.10008.15.1.1", "Universal Coordinated Time", DicomUidType.FrameOfReference, false);

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

        ///<summary>Context Group Name: Angiographic Interventional Device (8)</summary>
        public static readonly DicomUID AngiographicInterventionalDevice8 = new DicomUID("1.2.840.10008.6.1.6", "Angiographic Interventional Device (8)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Guided Therapeutic Procedure (9)</summary>
        public static readonly DicomUID ImageGuidedTherapeuticProcedure9 = new DicomUID("1.2.840.10008.6.1.7", "Image Guided Therapeutic Procedure (9)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interventional Drug (10)</summary>
        public static readonly DicomUID InterventionalDrug10 = new DicomUID("1.2.840.10008.6.1.8", "Interventional Drug (10)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Administration Route (11)</summary>
        public static readonly DicomUID AdministrationRoute11 = new DicomUID("1.2.840.10008.6.1.9", "Administration Route (11)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Contrast Agent (12)</summary>
        public static readonly DicomUID ImagingContrastAgent12 = new DicomUID("1.2.840.10008.6.1.10", "Imaging Contrast Agent (12)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Contrast Agent Ingredient (13)</summary>
        public static readonly DicomUID ImagingContrastAgentIngredient13 = new DicomUID("1.2.840.10008.6.1.11", "Imaging Contrast Agent Ingredient (13)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiopharmaceutical Isotope (18)</summary>
        public static readonly DicomUID RadiopharmaceuticalIsotope18 = new DicomUID("1.2.840.10008.6.1.12", "Radiopharmaceutical Isotope (18)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Orientation (19)</summary>
        public static readonly DicomUID PatientOrientation19 = new DicomUID("1.2.840.10008.6.1.13", "Patient Orientation (19)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Orientation Modifier (20)</summary>
        public static readonly DicomUID PatientOrientationModifier20 = new DicomUID("1.2.840.10008.6.1.14", "Patient Orientation Modifier (20)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Equipment Relationship (21)</summary>
        public static readonly DicomUID PatientEquipmentRelationship21 = new DicomUID("1.2.840.10008.6.1.15", "Patient Equipment Relationship (21)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cranio-Caudad Angulation (23)</summary>
        public static readonly DicomUID CranioCaudadAngulation23 = new DicomUID("1.2.840.10008.6.1.16", "Cranio-Caudad Angulation (23)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiopharmaceutical (25)</summary>
        public static readonly DicomUID Radiopharmaceutical25 = new DicomUID("1.2.840.10008.6.1.17", "Radiopharmaceutical (25)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nuclear Medicine Projection (26)</summary>
        public static readonly DicomUID NuclearMedicineProjection26 = new DicomUID("1.2.840.10008.6.1.18", "Nuclear Medicine Projection (26)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Acquisition Modality (29)</summary>
        public static readonly DicomUID AcquisitionModality29 = new DicomUID("1.2.840.10008.6.1.19", "Acquisition Modality (29)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DICOM Device (30)</summary>
        public static readonly DicomUID DICOMDevice30 = new DicomUID("1.2.840.10008.6.1.20", "DICOM Device (30)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Prior (31)</summary>
        public static readonly DicomUID AbstractPrior31 = new DicomUID("1.2.840.10008.6.1.21", "Abstract Prior (31)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Value Qualifier (42)</summary>
        public static readonly DicomUID NumericValueQualifier42 = new DicomUID("1.2.840.10008.6.1.22", "Numeric Value Qualifier (42)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Unit (82)</summary>
        public static readonly DicomUID MeasurementUnit82 = new DicomUID("1.2.840.10008.6.1.23", "Measurement Unit (82)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Real World Value Mapping Unit (83)</summary>
        public static readonly DicomUID RealWorldValueMappingUnit83 = new DicomUID("1.2.840.10008.6.1.24", "Real World Value Mapping Unit (83)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Significance Level (220)</summary>
        public static readonly DicomUID SignificanceLevel220 = new DicomUID("1.2.840.10008.6.1.25", "Significance Level (220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Range Concept (221)</summary>
        public static readonly DicomUID MeasurementRangeConcept221 = new DicomUID("1.2.840.10008.6.1.26", "Measurement Range Concept (221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Normality (222)</summary>
        public static readonly DicomUID Normality222 = new DicomUID("1.2.840.10008.6.1.27", "Normality (222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Normal Range Value (223)</summary>
        public static readonly DicomUID NormalRangeValue223 = new DicomUID("1.2.840.10008.6.1.28", "Normal Range Value (223)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Selection Method (224)</summary>
        public static readonly DicomUID SelectionMethod224 = new DicomUID("1.2.840.10008.6.1.29", "Selection Method (224)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Uncertainty Concept (225)</summary>
        public static readonly DicomUID MeasurementUncertaintyConcept225 = new DicomUID("1.2.840.10008.6.1.30", "Measurement Uncertainty Concept (225)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Population Statistical Descriptor (226)</summary>
        public static readonly DicomUID PopulationStatisticalDescriptor226 = new DicomUID("1.2.840.10008.6.1.31", "Population Statistical Descriptor (226)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sample Statistical Descriptor (227)</summary>
        public static readonly DicomUID SampleStatisticalDescriptor227 = new DicomUID("1.2.840.10008.6.1.32", "Sample Statistical Descriptor (227)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Complication Severity (251)</summary>
        public static readonly DicomUID ComplicationSeverity251 = new DicomUID("1.2.840.10008.6.1.39", "Complication Severity (251)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Observer Type (270)</summary>
        public static readonly DicomUID ObserverType270 = new DicomUID("1.2.840.10008.6.1.40", "Observer Type (270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Observation Subject Class (271)</summary>
        public static readonly DicomUID ObservationSubjectClass271 = new DicomUID("1.2.840.10008.6.1.41", "Observation Subject Class (271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Audio Channel Source (3000)</summary>
        public static readonly DicomUID AudioChannelSource3000 = new DicomUID("1.2.840.10008.6.1.42", "Audio Channel Source (3000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Lead (3001)</summary>
        public static readonly DicomUID ECGLead3001 = new DicomUID("1.2.840.10008.6.1.43", "ECG Lead (3001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Waveform Source (3003)</summary>
        public static readonly DicomUID HemodynamicWaveformSource3003 = new DicomUID("1.2.840.10008.6.1.44", "Hemodynamic Waveform Source (3003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Anatomic Structure (3010)</summary>
        public static readonly DicomUID CardiovascularAnatomicStructure3010 = new DicomUID("1.2.840.10008.6.1.45", "Cardiovascular Anatomic Structure (3010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Anatomic Location (3011)</summary>
        public static readonly DicomUID ElectrophysiologyAnatomicLocation3011 = new DicomUID("1.2.840.10008.6.1.46", "Electrophysiology Anatomic Location (3011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Artery Segment (3014)</summary>
        public static readonly DicomUID CoronaryArterySegment3014 = new DicomUID("1.2.840.10008.6.1.47", "Coronary Artery Segment (3014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Artery (3015)</summary>
        public static readonly DicomUID CoronaryArtery3015 = new DicomUID("1.2.840.10008.6.1.48", "Coronary Artery (3015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Anatomic Structure Modifier (3019)</summary>
        public static readonly DicomUID CardiovascularAnatomicStructureModifier3019 = new DicomUID("1.2.840.10008.6.1.49", "Cardiovascular Anatomic Structure Modifier (3019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiology Measurement Unit (Retired) (3082)</summary>
        public static readonly DicomUID CardiologyMeasurementUnit3082RETIRED = new DicomUID("1.2.840.10008.6.1.50", "Cardiology Measurement Unit (Retired) (3082)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Time Synchronization Channel Type (3090)</summary>
        public static readonly DicomUID TimeSynchronizationChannelType3090 = new DicomUID("1.2.840.10008.6.1.51", "Time Synchronization Channel Type (3090)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Procedural State Value (3101)</summary>
        public static readonly DicomUID CardiacProceduralStateValue3101 = new DicomUID("1.2.840.10008.6.1.52", "Cardiac Procedural State Value (3101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Measurement Function/Technique (3240)</summary>
        public static readonly DicomUID ElectrophysiologyMeasurementFunctionTechnique3240 = new DicomUID("1.2.840.10008.6.1.53", "Electrophysiology Measurement Function/Technique (3240)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Measurement Technique (3241)</summary>
        public static readonly DicomUID HemodynamicMeasurementTechnique3241 = new DicomUID("1.2.840.10008.6.1.54", "Hemodynamic Measurement Technique (3241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Catheterization Procedure Phase (3250)</summary>
        public static readonly DicomUID CatheterizationProcedurePhase3250 = new DicomUID("1.2.840.10008.6.1.55", "Catheterization Procedure Phase (3250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Procedure Phase (3254)</summary>
        public static readonly DicomUID ElectrophysiologyProcedurePhase3254 = new DicomUID("1.2.840.10008.6.1.56", "Electrophysiology Procedure Phase (3254)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Protocol (3261)</summary>
        public static readonly DicomUID StressProtocol3261 = new DicomUID("1.2.840.10008.6.1.57", "Stress Protocol (3261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Patient State Value (3262)</summary>
        public static readonly DicomUID ECGPatientStateValue3262 = new DicomUID("1.2.840.10008.6.1.58", "ECG Patient State Value (3262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrode Placement Value (3263)</summary>
        public static readonly DicomUID ElectrodePlacementValue3263 = new DicomUID("1.2.840.10008.6.1.59", "Electrode Placement Value (3263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: XYZ Electrode Placement Values (Retired) (3264)</summary>
        public static readonly DicomUID XYZElectrodePlacementValues3264RETIRED = new DicomUID("1.2.840.10008.6.1.60", "XYZ Electrode Placement Values (Retired) (3264)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Hemodynamic Physiological Challenge (3271)</summary>
        public static readonly DicomUID HemodynamicPhysiologicalChallenge3271 = new DicomUID("1.2.840.10008.6.1.61", "Hemodynamic Physiological Challenge (3271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Annotation (3335)</summary>
        public static readonly DicomUID ECGAnnotation3335 = new DicomUID("1.2.840.10008.6.1.62", "ECG Annotation (3335)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Annotation (3337)</summary>
        public static readonly DicomUID HemodynamicAnnotation3337 = new DicomUID("1.2.840.10008.6.1.63", "Hemodynamic Annotation (3337)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Annotation (3339)</summary>
        public static readonly DicomUID ElectrophysiologyAnnotation3339 = new DicomUID("1.2.840.10008.6.1.64", "Electrophysiology Annotation (3339)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Log Title (3400)</summary>
        public static readonly DicomUID ProcedureLogTitle3400 = new DicomUID("1.2.840.10008.6.1.65", "Procedure Log Title (3400)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Log Note Type (3401)</summary>
        public static readonly DicomUID LogNoteType3401 = new DicomUID("1.2.840.10008.6.1.66", "Log Note Type (3401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Status and Event (3402)</summary>
        public static readonly DicomUID PatientStatusAndEvent3402 = new DicomUID("1.2.840.10008.6.1.67", "Patient Status and Event (3402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Percutaneous Entry (3403)</summary>
        public static readonly DicomUID PercutaneousEntry3403 = new DicomUID("1.2.840.10008.6.1.68", "Percutaneous Entry (3403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Staff Action (3404)</summary>
        public static readonly DicomUID StaffAction3404 = new DicomUID("1.2.840.10008.6.1.69", "Staff Action (3404)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Action Value (3405)</summary>
        public static readonly DicomUID ProcedureActionValue3405 = new DicomUID("1.2.840.10008.6.1.70", "Procedure Action Value (3405)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-coronary Transcatheter Intervention (3406)</summary>
        public static readonly DicomUID NonCoronaryTranscatheterIntervention3406 = new DicomUID("1.2.840.10008.6.1.71", "Non-coronary Transcatheter Intervention (3406)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Object Reference Purpose (3407)</summary>
        public static readonly DicomUID ObjectReferencePurpose3407 = new DicomUID("1.2.840.10008.6.1.72", "Object Reference Purpose (3407)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Consumable Action (3408)</summary>
        public static readonly DicomUID ConsumableAction3408 = new DicomUID("1.2.840.10008.6.1.73", "Consumable Action (3408)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Drug/Contrast Administration (3409)</summary>
        public static readonly DicomUID DrugContrastAdministration3409 = new DicomUID("1.2.840.10008.6.1.74", "Drug/Contrast Administration (3409)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Drug/Contrast Numeric Parameter (3410)</summary>
        public static readonly DicomUID DrugContrastNumericParameter3410 = new DicomUID("1.2.840.10008.6.1.75", "Drug/Contrast Numeric Parameter (3410)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intracoronary Device (3411)</summary>
        public static readonly DicomUID IntracoronaryDevice3411 = new DicomUID("1.2.840.10008.6.1.76", "Intracoronary Device (3411)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intervention Action/Status (3412)</summary>
        public static readonly DicomUID InterventionActionStatus3412 = new DicomUID("1.2.840.10008.6.1.77", "Intervention Action/Status (3412)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Adverse Outcome (3413)</summary>
        public static readonly DicomUID AdverseOutcome3413 = new DicomUID("1.2.840.10008.6.1.78", "Adverse Outcome (3413)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Urgency (3414)</summary>
        public static readonly DicomUID ProcedureUrgency3414 = new DicomUID("1.2.840.10008.6.1.79", "Procedure Urgency (3414)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Rhythm (3415)</summary>
        public static readonly DicomUID CardiacRhythm3415 = new DicomUID("1.2.840.10008.6.1.80", "Cardiac Rhythm (3415)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiration Rhythm (3416)</summary>
        public static readonly DicomUID RespirationRhythm3416 = new DicomUID("1.2.840.10008.6.1.81", "Respiration Rhythm (3416)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Risk (3418)</summary>
        public static readonly DicomUID LesionRisk3418 = new DicomUID("1.2.840.10008.6.1.82", "Lesion Risk (3418)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Finding Title (3419)</summary>
        public static readonly DicomUID FindingTitle3419 = new DicomUID("1.2.840.10008.6.1.83", "Finding Title (3419)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Action (3421)</summary>
        public static readonly DicomUID ProcedureAction3421 = new DicomUID("1.2.840.10008.6.1.84", "Procedure Action (3421)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Use Action (3422)</summary>
        public static readonly DicomUID DeviceUseAction3422 = new DicomUID("1.2.840.10008.6.1.85", "Device Use Action (3422)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Device Characteristic (3423)</summary>
        public static readonly DicomUID NumericDeviceCharacteristic3423 = new DicomUID("1.2.840.10008.6.1.86", "Numeric Device Characteristic (3423)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intervention Parameter (3425)</summary>
        public static readonly DicomUID InterventionParameter3425 = new DicomUID("1.2.840.10008.6.1.87", "Intervention Parameter (3425)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Consumables Parameter (3426)</summary>
        public static readonly DicomUID ConsumablesParameter3426 = new DicomUID("1.2.840.10008.6.1.88", "Consumables Parameter (3426)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equipment Event (3427)</summary>
        public static readonly DicomUID EquipmentEvent3427 = new DicomUID("1.2.840.10008.6.1.89", "Equipment Event (3427)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Imaging Procedure (3428)</summary>
        public static readonly DicomUID CardiovascularImagingProcedure3428 = new DicomUID("1.2.840.10008.6.1.90", "Cardiovascular Imaging Procedure (3428)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Catheterization Device (3429)</summary>
        public static readonly DicomUID CatheterizationDevice3429 = new DicomUID("1.2.840.10008.6.1.91", "Catheterization Device (3429)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DateTime Qualifier (3430)</summary>
        public static readonly DicomUID DateTimeQualifier3430 = new DicomUID("1.2.840.10008.6.1.92", "DateTime Qualifier (3430)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Peripheral Pulse Location (3440)</summary>
        public static readonly DicomUID PeripheralPulseLocation3440 = new DicomUID("1.2.840.10008.6.1.93", "Peripheral Pulse Location (3440)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Assessment (3441)</summary>
        public static readonly DicomUID PatientAssessment3441 = new DicomUID("1.2.840.10008.6.1.94", "Patient Assessment (3441)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Peripheral Pulse Method (3442)</summary>
        public static readonly DicomUID PeripheralPulseMethod3442 = new DicomUID("1.2.840.10008.6.1.95", "Peripheral Pulse Method (3442)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Skin Condition (3446)</summary>
        public static readonly DicomUID SkinCondition3446 = new DicomUID("1.2.840.10008.6.1.96", "Skin Condition (3446)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Assessment (3448)</summary>
        public static readonly DicomUID AirwayAssessment3448 = new DicomUID("1.2.840.10008.6.1.97", "Airway Assessment (3448)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calibration Object (3451)</summary>
        public static readonly DicomUID CalibrationObject3451 = new DicomUID("1.2.840.10008.6.1.98", "Calibration Object (3451)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calibration Method (3452)</summary>
        public static readonly DicomUID CalibrationMethod3452 = new DicomUID("1.2.840.10008.6.1.99", "Calibration Method (3452)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Volume Method (3453)</summary>
        public static readonly DicomUID CardiacVolumeMethod3453 = new DicomUID("1.2.840.10008.6.1.100", "Cardiac Volume Method (3453)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Index Method (3455)</summary>
        public static readonly DicomUID IndexMethod3455 = new DicomUID("1.2.840.10008.6.1.101", "Index Method (3455)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sub-segment Method (3456)</summary>
        public static readonly DicomUID SubSegmentMethod3456 = new DicomUID("1.2.840.10008.6.1.102", "Sub-segment Method (3456)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contour Realignment (3458)</summary>
        public static readonly DicomUID ContourRealignment3458 = new DicomUID("1.2.840.10008.6.1.103", "Contour Realignment (3458)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Circumferential Extent (3460)</summary>
        public static readonly DicomUID CircumferentialExtent3460 = new DicomUID("1.2.840.10008.6.1.104", "Circumferential Extent (3460)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Regional Extent (3461)</summary>
        public static readonly DicomUID RegionalExtent3461 = new DicomUID("1.2.840.10008.6.1.105", "Regional Extent (3461)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chamber Identification (3462)</summary>
        public static readonly DicomUID ChamberIdentification3462 = new DicomUID("1.2.840.10008.6.1.106", "Chamber Identification (3462)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: QA Reference Method (3465)</summary>
        public static readonly DicomUID QAReferenceMethod3465 = new DicomUID("1.2.840.10008.6.1.107", "QA Reference Method (3465)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Plane Identification (3466)</summary>
        public static readonly DicomUID PlaneIdentification3466 = new DicomUID("1.2.840.10008.6.1.108", "Plane Identification (3466)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ejection Fraction (3467)</summary>
        public static readonly DicomUID EjectionFraction3467 = new DicomUID("1.2.840.10008.6.1.109", "Ejection Fraction (3467)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ED Volume (3468)</summary>
        public static readonly DicomUID EDVolume3468 = new DicomUID("1.2.840.10008.6.1.110", "ED Volume (3468)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ES Volume (3469)</summary>
        public static readonly DicomUID ESVolume3469 = new DicomUID("1.2.840.10008.6.1.111", "ES Volume (3469)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Lumen Cross-sectional Area Calculation Method (3470)</summary>
        public static readonly DicomUID VesselLumenCrossSectionalAreaCalculationMethod3470 = new DicomUID("1.2.840.10008.6.1.112", "Vessel Lumen Cross-sectional Area Calculation Method (3470)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Estimated Volume (3471)</summary>
        public static readonly DicomUID EstimatedVolume3471 = new DicomUID("1.2.840.10008.6.1.113", "Estimated Volume (3471)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Contraction Phase (3472)</summary>
        public static readonly DicomUID CardiacContractionPhase3472 = new DicomUID("1.2.840.10008.6.1.114", "Cardiac Contraction Phase (3472)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Procedure Phase (3480)</summary>
        public static readonly DicomUID IVUSProcedurePhase3480 = new DicomUID("1.2.840.10008.6.1.115", "IVUS Procedure Phase (3480)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Distance Measurement (3481)</summary>
        public static readonly DicomUID IVUSDistanceMeasurement3481 = new DicomUID("1.2.840.10008.6.1.116", "IVUS Distance Measurement (3481)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Area Measurement (3482)</summary>
        public static readonly DicomUID IVUSAreaMeasurement3482 = new DicomUID("1.2.840.10008.6.1.117", "IVUS Area Measurement (3482)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Longitudinal Measurement (3483)</summary>
        public static readonly DicomUID IVUSLongitudinalMeasurement3483 = new DicomUID("1.2.840.10008.6.1.118", "IVUS Longitudinal Measurement (3483)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Index/Ratio (3484)</summary>
        public static readonly DicomUID IVUSIndexRatio3484 = new DicomUID("1.2.840.10008.6.1.119", "IVUS Index/Ratio (3484)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Volume Measurement (3485)</summary>
        public static readonly DicomUID IVUSVolumeMeasurement3485 = new DicomUID("1.2.840.10008.6.1.120", "IVUS Volume Measurement (3485)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Measurement Site (3486)</summary>
        public static readonly DicomUID VascularMeasurementSite3486 = new DicomUID("1.2.840.10008.6.1.121", "Vascular Measurement Site (3486)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intravascular Volumetric Region (3487)</summary>
        public static readonly DicomUID IntravascularVolumetricRegion3487 = new DicomUID("1.2.840.10008.6.1.122", "Intravascular Volumetric Region (3487)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Min/Max/Mean (3488)</summary>
        public static readonly DicomUID MinMaxMean3488 = new DicomUID("1.2.840.10008.6.1.123", "Min/Max/Mean (3488)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcium Distribution (3489)</summary>
        public static readonly DicomUID CalciumDistribution3489 = new DicomUID("1.2.840.10008.6.1.124", "Calcium Distribution (3489)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Lesion Morphology (3491)</summary>
        public static readonly DicomUID IVUSLesionMorphology3491 = new DicomUID("1.2.840.10008.6.1.125", "IVUS Lesion Morphology (3491)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Dissection Classification (3492)</summary>
        public static readonly DicomUID VascularDissectionClassification3492 = new DicomUID("1.2.840.10008.6.1.126", "Vascular Dissection Classification (3492)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Relative Stenosis Severity (3493)</summary>
        public static readonly DicomUID IVUSRelativeStenosisSeverity3493 = new DicomUID("1.2.840.10008.6.1.127", "IVUS Relative Stenosis Severity (3493)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Non Morphological Finding (3494)</summary>
        public static readonly DicomUID IVUSNonMorphologicalFinding3494 = new DicomUID("1.2.840.10008.6.1.128", "IVUS Non Morphological Finding (3494)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Plaque Composition (3495)</summary>
        public static readonly DicomUID IVUSPlaqueComposition3495 = new DicomUID("1.2.840.10008.6.1.129", "IVUS Plaque Composition (3495)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Fiducial Point (3496)</summary>
        public static readonly DicomUID IVUSFiducialPoint3496 = new DicomUID("1.2.840.10008.6.1.130", "IVUS Fiducial Point (3496)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IVUS Arterial Morphology (3497)</summary>
        public static readonly DicomUID IVUSArterialMorphology3497 = new DicomUID("1.2.840.10008.6.1.131", "IVUS Arterial Morphology (3497)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pressure Unit (3500)</summary>
        public static readonly DicomUID PressureUnit3500 = new DicomUID("1.2.840.10008.6.1.132", "Pressure Unit (3500)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Resistance Unit (3502)</summary>
        public static readonly DicomUID HemodynamicResistanceUnit3502 = new DicomUID("1.2.840.10008.6.1.133", "Hemodynamic Resistance Unit (3502)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Indexed Hemodynamic Resistance Unit (3503)</summary>
        public static readonly DicomUID IndexedHemodynamicResistanceUnit3503 = new DicomUID("1.2.840.10008.6.1.134", "Indexed Hemodynamic Resistance Unit (3503)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Catheter Size Unit (3510)</summary>
        public static readonly DicomUID CatheterSizeUnit3510 = new DicomUID("1.2.840.10008.6.1.135", "Catheter Size Unit (3510)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Collection (3515)</summary>
        public static readonly DicomUID SpecimenCollection3515 = new DicomUID("1.2.840.10008.6.1.136", "Specimen Collection (3515)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Source Type (3520)</summary>
        public static readonly DicomUID BloodSourceType3520 = new DicomUID("1.2.840.10008.6.1.137", "Blood Source Type (3520)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Gas Pressure (3524)</summary>
        public static readonly DicomUID BloodGasPressure3524 = new DicomUID("1.2.840.10008.6.1.138", "Blood Gas Pressure (3524)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Oxygen Administration Action (3530)</summary>
        public static readonly DicomUID OxygenAdministrationAction3530 = new DicomUID("1.2.840.10008.6.1.144", "Oxygen Administration Action (3530)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Oxygen Administration (3531)</summary>
        public static readonly DicomUID OxygenAdministration3531 = new DicomUID("1.2.840.10008.6.1.145", "Oxygen Administration (3531)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Circulatory Support Action (3550)</summary>
        public static readonly DicomUID CirculatorySupportAction3550 = new DicomUID("1.2.840.10008.6.1.146", "Circulatory Support Action (3550)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventilation Action (3551)</summary>
        public static readonly DicomUID VentilationAction3551 = new DicomUID("1.2.840.10008.6.1.147", "Ventilation Action (3551)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pacing Action (3552)</summary>
        public static readonly DicomUID PacingAction3552 = new DicomUID("1.2.840.10008.6.1.148", "Pacing Action (3552)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Circulatory Support (3553)</summary>
        public static readonly DicomUID CirculatorySupport3553 = new DicomUID("1.2.840.10008.6.1.149", "Circulatory Support (3553)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventilation (3554)</summary>
        public static readonly DicomUID Ventilation3554 = new DicomUID("1.2.840.10008.6.1.150", "Ventilation (3554)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pacing (3555)</summary>
        public static readonly DicomUID Pacing3555 = new DicomUID("1.2.840.10008.6.1.151", "Pacing (3555)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Pressure Method (3560)</summary>
        public static readonly DicomUID BloodPressureMethod3560 = new DicomUID("1.2.840.10008.6.1.152", "Blood Pressure Method (3560)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Time (3600)</summary>
        public static readonly DicomUID RelativeTime3600 = new DicomUID("1.2.840.10008.6.1.153", "Relative Time (3600)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Patient State (3602)</summary>
        public static readonly DicomUID HemodynamicPatientState3602 = new DicomUID("1.2.840.10008.6.1.154", "Hemodynamic Patient State (3602)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Lesion Location (3604)</summary>
        public static readonly DicomUID ArterialLesionLocation3604 = new DicomUID("1.2.840.10008.6.1.155", "Arterial Lesion Location (3604)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Source Location (3606)</summary>
        public static readonly DicomUID ArterialSourceLocation3606 = new DicomUID("1.2.840.10008.6.1.156", "Arterial Source Location (3606)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Venous Source Location (3607)</summary>
        public static readonly DicomUID VenousSourceLocation3607 = new DicomUID("1.2.840.10008.6.1.157", "Venous Source Location (3607)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Atrial Source Location (3608)</summary>
        public static readonly DicomUID AtrialSourceLocation3608 = new DicomUID("1.2.840.10008.6.1.158", "Atrial Source Location (3608)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ventricular Source Location (3609)</summary>
        public static readonly DicomUID VentricularSourceLocation3609 = new DicomUID("1.2.840.10008.6.1.159", "Ventricular Source Location (3609)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gradient Source Location (3610)</summary>
        public static readonly DicomUID GradientSourceLocation3610 = new DicomUID("1.2.840.10008.6.1.160", "Gradient Source Location (3610)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pressure Measurement (3611)</summary>
        public static readonly DicomUID PressureMeasurement3611 = new DicomUID("1.2.840.10008.6.1.161", "Pressure Measurement (3611)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Blood Velocity Measurement (3612)</summary>
        public static readonly DicomUID BloodVelocityMeasurement3612 = new DicomUID("1.2.840.10008.6.1.162", "Blood Velocity Measurement (3612)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Time Measurement (3613)</summary>
        public static readonly DicomUID HemodynamicTimeMeasurement3613 = new DicomUID("1.2.840.10008.6.1.163", "Hemodynamic Time Measurement (3613)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-mitral Valve Area (3614)</summary>
        public static readonly DicomUID NonMitralValveArea3614 = new DicomUID("1.2.840.10008.6.1.164", "Non-mitral Valve Area (3614)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Valve Area (3615)</summary>
        public static readonly DicomUID ValveArea3615 = new DicomUID("1.2.840.10008.6.1.165", "Valve Area (3615)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Period Measurement (3616)</summary>
        public static readonly DicomUID HemodynamicPeriodMeasurement3616 = new DicomUID("1.2.840.10008.6.1.166", "Hemodynamic Period Measurement (3616)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Valve Flow (3617)</summary>
        public static readonly DicomUID ValveFlow3617 = new DicomUID("1.2.840.10008.6.1.167", "Valve Flow (3617)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Flow (3618)</summary>
        public static readonly DicomUID HemodynamicFlow3618 = new DicomUID("1.2.840.10008.6.1.168", "Hemodynamic Flow (3618)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Resistance Measurement (3619)</summary>
        public static readonly DicomUID HemodynamicResistanceMeasurement3619 = new DicomUID("1.2.840.10008.6.1.169", "Hemodynamic Resistance Measurement (3619)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Ratio (3620)</summary>
        public static readonly DicomUID HemodynamicRatio3620 = new DicomUID("1.2.840.10008.6.1.170", "Hemodynamic Ratio (3620)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fractional Flow Reserve (3621)</summary>
        public static readonly DicomUID FractionalFlowReserve3621 = new DicomUID("1.2.840.10008.6.1.171", "Fractional Flow Reserve (3621)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Type (3627)</summary>
        public static readonly DicomUID MeasurementType3627 = new DicomUID("1.2.840.10008.6.1.172", "Measurement Type (3627)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Output Method (3628)</summary>
        public static readonly DicomUID CardiacOutputMethod3628 = new DicomUID("1.2.840.10008.6.1.173", "Cardiac Output Method (3628)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Intent (3629)</summary>
        public static readonly DicomUID ProcedureIntent3629 = new DicomUID("1.2.840.10008.6.1.174", "Procedure Intent (3629)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Anatomic Location (3630)</summary>
        public static readonly DicomUID CardiovascularAnatomicLocation3630 = new DicomUID("1.2.840.10008.6.1.175", "Cardiovascular Anatomic Location (3630)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hypertension (3640)</summary>
        public static readonly DicomUID Hypertension3640 = new DicomUID("1.2.840.10008.6.1.176", "Hypertension (3640)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Assessment (3641)</summary>
        public static readonly DicomUID HemodynamicAssessment3641 = new DicomUID("1.2.840.10008.6.1.177", "Hemodynamic Assessment (3641)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Degree Finding (3642)</summary>
        public static readonly DicomUID DegreeFinding3642 = new DicomUID("1.2.840.10008.6.1.178", "Degree Finding (3642)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hemodynamic Measurement Phase (3651)</summary>
        public static readonly DicomUID HemodynamicMeasurementPhase3651 = new DicomUID("1.2.840.10008.6.1.179", "Hemodynamic Measurement Phase (3651)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Body Surface Area Equation (3663)</summary>
        public static readonly DicomUID BodySurfaceAreaEquation3663 = new DicomUID("1.2.840.10008.6.1.180", "Body Surface Area Equation (3663)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Oxygen Consumption Equation/Table (3664)</summary>
        public static readonly DicomUID OxygenConsumptionEquationTable3664 = new DicomUID("1.2.840.10008.6.1.181", "Oxygen Consumption Equation/Table (3664)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: P50 Equation (3666)</summary>
        public static readonly DicomUID P50Equation3666 = new DicomUID("1.2.840.10008.6.1.182", "P50 Equation (3666)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Framingham Score (3667)</summary>
        public static readonly DicomUID FraminghamScore3667 = new DicomUID("1.2.840.10008.6.1.183", "Framingham Score (3667)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Framingham Table (3668)</summary>
        public static readonly DicomUID FraminghamTable3668 = new DicomUID("1.2.840.10008.6.1.184", "Framingham Table (3668)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Procedure Type (3670)</summary>
        public static readonly DicomUID ECGProcedureType3670 = new DicomUID("1.2.840.10008.6.1.185", "ECG Procedure Type (3670)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reason for ECG Study (3671)</summary>
        public static readonly DicomUID ReasonForECGStudy3671 = new DicomUID("1.2.840.10008.6.1.186", "Reason for ECG Study (3671)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pacemaker (3672)</summary>
        public static readonly DicomUID Pacemaker3672 = new DicomUID("1.2.840.10008.6.1.187", "Pacemaker (3672)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnosis (Retired) (3673)</summary>
        public static readonly DicomUID Diagnosis3673RETIRED = new DicomUID("1.2.840.10008.6.1.188", "Diagnosis (Retired) (3673)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Other Filters (Retired) (3675)</summary>
        public static readonly DicomUID OtherFilters3675RETIRED = new DicomUID("1.2.840.10008.6.1.189", "Other Filters (Retired) (3675)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Lead Measurement Technique (3676)</summary>
        public static readonly DicomUID LeadMeasurementTechnique3676 = new DicomUID("1.2.840.10008.6.1.190", "Lead Measurement Technique (3676)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Summary Codes ECG (3677)</summary>
        public static readonly DicomUID SummaryCodesECG3677 = new DicomUID("1.2.840.10008.6.1.191", "Summary Codes ECG (3677)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: QT Correction Algorithm (3678)</summary>
        public static readonly DicomUID QTCorrectionAlgorithm3678 = new DicomUID("1.2.840.10008.6.1.192", "QT Correction Algorithm (3678)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Morphology Description (Retired) (3679)</summary>
        public static readonly DicomUID ECGMorphologyDescription3679RETIRED = new DicomUID("1.2.840.10008.6.1.193", "ECG Morphology Description (Retired) (3679)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: ECG Lead Noise Description (3680)</summary>
        public static readonly DicomUID ECGLeadNoiseDescription3680 = new DicomUID("1.2.840.10008.6.1.194", "ECG Lead Noise Description (3680)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Lead Noise Modifier (Retired) (3681)</summary>
        public static readonly DicomUID ECGLeadNoiseModifier3681RETIRED = new DicomUID("1.2.840.10008.6.1.195", "ECG Lead Noise Modifier (Retired) (3681)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Probability (Retired) (3682)</summary>
        public static readonly DicomUID Probability3682RETIRED = new DicomUID("1.2.840.10008.6.1.196", "Probability (Retired) (3682)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Modifier (Retired) (3683)</summary>
        public static readonly DicomUID Modifier3683RETIRED = new DicomUID("1.2.840.10008.6.1.197", "Modifier (Retired) (3683)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Trend (Retired) (3684)</summary>
        public static readonly DicomUID Trend3684RETIRED = new DicomUID("1.2.840.10008.6.1.198", "Trend (Retired) (3684)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Conjunctive Term (Retired) (3685)</summary>
        public static readonly DicomUID ConjunctiveTerm3685RETIRED = new DicomUID("1.2.840.10008.6.1.199", "Conjunctive Term (Retired) (3685)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: ECG Interpretive Statement (Retired) (3686)</summary>
        public static readonly DicomUID ECGInterpretiveStatement3686RETIRED = new DicomUID("1.2.840.10008.6.1.200", "ECG Interpretive Statement (Retired) (3686)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Electrophysiology Waveform Duration (3687)</summary>
        public static readonly DicomUID ElectrophysiologyWaveformDuration3687 = new DicomUID("1.2.840.10008.6.1.201", "Electrophysiology Waveform Duration (3687)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Electrophysiology Waveform Voltage (3688)</summary>
        public static readonly DicomUID ElectrophysiologyWaveformVoltage3688 = new DicomUID("1.2.840.10008.6.1.202", "Electrophysiology Waveform Voltage (3688)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Diagnosis (3700)</summary>
        public static readonly DicomUID CathDiagnosis3700 = new DicomUID("1.2.840.10008.6.1.203", "Cath Diagnosis (3700)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Valve/Tract (3701)</summary>
        public static readonly DicomUID CardiacValveTract3701 = new DicomUID("1.2.840.10008.6.1.204", "Cardiac Valve/Tract (3701)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wall Motion (3703)</summary>
        public static readonly DicomUID WallMotion3703 = new DicomUID("1.2.840.10008.6.1.205", "Wall Motion (3703)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardium Wall Morphology Finding (3704)</summary>
        public static readonly DicomUID MyocardiumWallMorphologyFinding3704 = new DicomUID("1.2.840.10008.6.1.206", "Myocardium Wall Morphology Finding (3704)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Valvular Abnormality (3711)</summary>
        public static readonly DicomUID ValvularAbnormality3711 = new DicomUID("1.2.840.10008.6.1.212", "Valvular Abnormality (3711)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Descriptor (3712)</summary>
        public static readonly DicomUID VesselDescriptor3712 = new DicomUID("1.2.840.10008.6.1.213", "Vessel Descriptor (3712)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: TIMI Flow Characteristic (3713)</summary>
        public static readonly DicomUID TIMIFlowCharacteristic3713 = new DicomUID("1.2.840.10008.6.1.214", "TIMI Flow Characteristic (3713)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Thrombus (3714)</summary>
        public static readonly DicomUID Thrombus3714 = new DicomUID("1.2.840.10008.6.1.215", "Thrombus (3714)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Margin (3715)</summary>
        public static readonly DicomUID LesionMargin3715 = new DicomUID("1.2.840.10008.6.1.216", "Lesion Margin (3715)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Severity (3716)</summary>
        public static readonly DicomUID Severity3716 = new DicomUID("1.2.840.10008.6.1.217", "Severity (3716)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Myocardial Wall 17 Segment Model (3717)</summary>
        public static readonly DicomUID LeftVentricleMyocardialWall17SegmentModel3717 = new DicomUID("1.2.840.10008.6.1.218", "Left Ventricle Myocardial Wall 17 Segment Model (3717)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardial Wall Segments in Projection (3718)</summary>
        public static readonly DicomUID MyocardialWallSegmentsInProjection3718 = new DicomUID("1.2.840.10008.6.1.219", "Myocardial Wall Segments in Projection (3718)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Canadian Clinical Classification (3719)</summary>
        public static readonly DicomUID CanadianClinicalClassification3719 = new DicomUID("1.2.840.10008.6.1.220", "Canadian Clinical Classification (3719)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac History Date (Retired) (3720)</summary>
        public static readonly DicomUID CardiacHistoryDate3720RETIRED = new DicomUID("1.2.840.10008.6.1.221", "Cardiac History Date (Retired) (3720)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Cardiovascular Surgery (3721)</summary>
        public static readonly DicomUID CardiovascularSurgery3721 = new DicomUID("1.2.840.10008.6.1.222", "Cardiovascular Surgery (3721)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diabetic Therapy (3722)</summary>
        public static readonly DicomUID DiabeticTherapy3722 = new DicomUID("1.2.840.10008.6.1.223", "Diabetic Therapy (3722)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MI Type (3723)</summary>
        public static readonly DicomUID MIType3723 = new DicomUID("1.2.840.10008.6.1.224", "MI Type (3723)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Smoking History (3724)</summary>
        public static readonly DicomUID SmokingHistory3724 = new DicomUID("1.2.840.10008.6.1.225", "Smoking History (3724)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Intervention Indication (3726)</summary>
        public static readonly DicomUID CoronaryInterventionIndication3726 = new DicomUID("1.2.840.10008.6.1.226", "Coronary Intervention Indication (3726)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Catheterization Indication (3727)</summary>
        public static readonly DicomUID CatheterizationIndication3727 = new DicomUID("1.2.840.10008.6.1.227", "Catheterization Indication (3727)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Finding (3728)</summary>
        public static readonly DicomUID CathFinding3728 = new DicomUID("1.2.840.10008.6.1.228", "Cath Finding (3728)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Ischemia Non-invasive Test (3737)</summary>
        public static readonly DicomUID IschemiaNonInvasiveTest3737 = new DicomUID("1.2.840.10008.6.1.234", "Ischemia Non-invasive Test (3737)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pre-Cath Angina Type (3738)</summary>
        public static readonly DicomUID PreCathAnginaType3738 = new DicomUID("1.2.840.10008.6.1.235", "Pre-Cath Angina Type (3738)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Procedure Type (3739)</summary>
        public static readonly DicomUID CathProcedureType3739 = new DicomUID("1.2.840.10008.6.1.236", "Cath Procedure Type (3739)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Thrombolytic Administration (3740)</summary>
        public static readonly DicomUID ThrombolyticAdministration3740 = new DicomUID("1.2.840.10008.6.1.237", "Thrombolytic Administration (3740)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lab Visit Medication Administration (3741)</summary>
        public static readonly DicomUID LabVisitMedicationAdministration3741 = new DicomUID("1.2.840.10008.6.1.238", "Lab Visit Medication Administration (3741)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PCI Medication Administration (3742)</summary>
        public static readonly DicomUID PCIMedicationAdministration3742 = new DicomUID("1.2.840.10008.6.1.239", "PCI Medication Administration (3742)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Vascular Complication (3754)</summary>
        public static readonly DicomUID VascularComplication3754 = new DicomUID("1.2.840.10008.6.1.249", "Vascular Complication (3754)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cath Complication (3755)</summary>
        public static readonly DicomUID CathComplication3755 = new DicomUID("1.2.840.10008.6.1.250", "Cath Complication (3755)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Patient Risk Factor (3756)</summary>
        public static readonly DicomUID CardiacPatientRiskFactor3756 = new DicomUID("1.2.840.10008.6.1.251", "Cardiac Patient Risk Factor (3756)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Diagnostic Procedure (3757)</summary>
        public static readonly DicomUID CardiacDiagnosticProcedure3757 = new DicomUID("1.2.840.10008.6.1.252", "Cardiac Diagnostic Procedure (3757)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiovascular Family History (3758)</summary>
        public static readonly DicomUID CardiovascularFamilyHistory3758 = new DicomUID("1.2.840.10008.6.1.253", "Cardiovascular Family History (3758)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Hypertension Therapy (3760)</summary>
        public static readonly DicomUID HypertensionTherapy3760 = new DicomUID("1.2.840.10008.6.1.254", "Hypertension Therapy (3760)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Antilipemic Agent (3761)</summary>
        public static readonly DicomUID AntilipemicAgent3761 = new DicomUID("1.2.840.10008.6.1.255", "Antilipemic Agent (3761)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Antiarrhythmic Agent (3762)</summary>
        public static readonly DicomUID AntiarrhythmicAgent3762 = new DicomUID("1.2.840.10008.6.1.256", "Antiarrhythmic Agent (3762)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardial Infarction Therapy (3764)</summary>
        public static readonly DicomUID MyocardialInfarctionTherapy3764 = new DicomUID("1.2.840.10008.6.1.257", "Myocardial Infarction Therapy (3764)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Concern Type (3769)</summary>
        public static readonly DicomUID ConcernType3769 = new DicomUID("1.2.840.10008.6.1.258", "Concern Type (3769)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Problem Status (3770)</summary>
        public static readonly DicomUID ProblemStatus3770 = new DicomUID("1.2.840.10008.6.1.259", "Problem Status (3770)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Health Status (3772)</summary>
        public static readonly DicomUID HealthStatus3772 = new DicomUID("1.2.840.10008.6.1.260", "Health Status (3772)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Use Status (3773)</summary>
        public static readonly DicomUID UseStatus3773 = new DicomUID("1.2.840.10008.6.1.261", "Use Status (3773)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Social History (3774)</summary>
        public static readonly DicomUID SocialHistory3774 = new DicomUID("1.2.840.10008.6.1.262", "Social History (3774)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implanted Device (3777)</summary>
        public static readonly DicomUID ImplantedDevice3777 = new DicomUID("1.2.840.10008.6.1.263", "Implanted Device (3777)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Plaque Structure (3802)</summary>
        public static readonly DicomUID PlaqueStructure3802 = new DicomUID("1.2.840.10008.6.1.264", "Plaque Structure (3802)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Measurement Method (3804)</summary>
        public static readonly DicomUID StenosisMeasurementMethod3804 = new DicomUID("1.2.840.10008.6.1.265", "Stenosis Measurement Method (3804)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Type (3805)</summary>
        public static readonly DicomUID StenosisType3805 = new DicomUID("1.2.840.10008.6.1.266", "Stenosis Type (3805)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Shape (3806)</summary>
        public static readonly DicomUID StenosisShape3806 = new DicomUID("1.2.840.10008.6.1.267", "Stenosis Shape (3806)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume Measurement Method (3807)</summary>
        public static readonly DicomUID VolumeMeasurementMethod3807 = new DicomUID("1.2.840.10008.6.1.268", "Volume Measurement Method (3807)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Aneurysm Type (3808)</summary>
        public static readonly DicomUID AneurysmType3808 = new DicomUID("1.2.840.10008.6.1.269", "Aneurysm Type (3808)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Associated Condition (3809)</summary>
        public static readonly DicomUID AssociatedCondition3809 = new DicomUID("1.2.840.10008.6.1.270", "Associated Condition (3809)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Morphology (3810)</summary>
        public static readonly DicomUID VascularMorphology3810 = new DicomUID("1.2.840.10008.6.1.271", "Vascular Morphology (3810)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stent Finding (3813)</summary>
        public static readonly DicomUID StentFinding3813 = new DicomUID("1.2.840.10008.6.1.272", "Stent Finding (3813)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stent Composition (3814)</summary>
        public static readonly DicomUID StentComposition3814 = new DicomUID("1.2.840.10008.6.1.273", "Stent Composition (3814)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source of Vascular Finding (3815)</summary>
        public static readonly DicomUID SourceOfVascularFinding3815 = new DicomUID("1.2.840.10008.6.1.274", "Source of Vascular Finding (3815)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Sclerosis Type (3817)</summary>
        public static readonly DicomUID VascularSclerosisType3817 = new DicomUID("1.2.840.10008.6.1.275", "Vascular Sclerosis Type (3817)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-invasive Vascular Procedure (3820)</summary>
        public static readonly DicomUID NonInvasiveVascularProcedure3820 = new DicomUID("1.2.840.10008.6.1.276", "Non-invasive Vascular Procedure (3820)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Papillary Muscle Included/Excluded (3821)</summary>
        public static readonly DicomUID PapillaryMuscleIncludedExcluded3821 = new DicomUID("1.2.840.10008.6.1.277", "Papillary Muscle Included/Excluded (3821)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiratory Status (3823)</summary>
        public static readonly DicomUID RespiratoryStatus3823 = new DicomUID("1.2.840.10008.6.1.278", "Respiratory Status (3823)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Heart Rhythm (3826)</summary>
        public static readonly DicomUID HeartRhythm3826 = new DicomUID("1.2.840.10008.6.1.279", "Heart Rhythm (3826)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Segment (3827)</summary>
        public static readonly DicomUID VesselSegment3827 = new DicomUID("1.2.840.10008.6.1.280", "Vessel Segment (3827)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pulmonary Artery (3829)</summary>
        public static readonly DicomUID PulmonaryArtery3829 = new DicomUID("1.2.840.10008.6.1.281", "Pulmonary Artery (3829)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Length (3831)</summary>
        public static readonly DicomUID StenosisLength3831 = new DicomUID("1.2.840.10008.6.1.282", "Stenosis Length (3831)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stenosis Grade (3832)</summary>
        public static readonly DicomUID StenosisGrade3832 = new DicomUID("1.2.840.10008.6.1.283", "Stenosis Grade (3832)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ejection Fraction (3833)</summary>
        public static readonly DicomUID CardiacEjectionFraction3833 = new DicomUID("1.2.840.10008.6.1.284", "Cardiac Ejection Fraction (3833)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Volume Measurement (3835)</summary>
        public static readonly DicomUID CardiacVolumeMeasurement3835 = new DicomUID("1.2.840.10008.6.1.285", "Cardiac Volume Measurement (3835)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time-based Perfusion Measurement (3836)</summary>
        public static readonly DicomUID TimeBasedPerfusionMeasurement3836 = new DicomUID("1.2.840.10008.6.1.286", "Time-based Perfusion Measurement (3836)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiducial Feature (3837)</summary>
        public static readonly DicomUID FiducialFeature3837 = new DicomUID("1.2.840.10008.6.1.287", "Fiducial Feature (3837)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diameter Derivation (3838)</summary>
        public static readonly DicomUID DiameterDerivation3838 = new DicomUID("1.2.840.10008.6.1.288", "Diameter Derivation (3838)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Coronary Vein (3839)</summary>
        public static readonly DicomUID CoronaryVein3839 = new DicomUID("1.2.840.10008.6.1.289", "Coronary Vein (3839)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pulmonary Vein (3840)</summary>
        public static readonly DicomUID PulmonaryVein3840 = new DicomUID("1.2.840.10008.6.1.290", "Pulmonary Vein (3840)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Craniofacial Anatomic Region (4028)</summary>
        public static readonly DicomUID CraniofacialAnatomicRegion4028 = new DicomUID("1.2.840.10008.6.1.306", "Craniofacial Anatomic Region (4028)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT, MR and PET Anatomy Imaged (4030)</summary>
        public static readonly DicomUID CTMRAndPETAnatomyImaged4030 = new DicomUID("1.2.840.10008.6.1.307", "CT, MR and PET Anatomy Imaged (4030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Common Anatomic Region (4031)</summary>
        public static readonly DicomUID CommonAnatomicRegion4031 = new DicomUID("1.2.840.10008.6.1.308", "Common Anatomic Region (4031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Spectroscopy Metabolite (4032)</summary>
        public static readonly DicomUID MRSpectroscopyMetabolite4032 = new DicomUID("1.2.840.10008.6.1.309", "MR Spectroscopy Metabolite (4032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Proton Spectroscopy Metabolite (4033)</summary>
        public static readonly DicomUID MRProtonSpectroscopyMetabolite4033 = new DicomUID("1.2.840.10008.6.1.310", "MR Proton Spectroscopy Metabolite (4033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Endoscopy Anatomic Region (4040)</summary>
        public static readonly DicomUID EndoscopyAnatomicRegion4040 = new DicomUID("1.2.840.10008.6.1.311", "Endoscopy Anatomic Region (4040)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: XA/XRF Anatomy Imaged (4042)</summary>
        public static readonly DicomUID XAXRFAnatomyImaged4042 = new DicomUID("1.2.840.10008.6.1.312", "XA/XRF Anatomy Imaged (4042)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Drug or Contrast Agent Characteristic (4050)</summary>
        public static readonly DicomUID DrugOrContrastAgentCharacteristic4050 = new DicomUID("1.2.840.10008.6.1.313", "Drug or Contrast Agent Characteristic (4050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Device (4051)</summary>
        public static readonly DicomUID GeneralDevice4051 = new DicomUID("1.2.840.10008.6.1.314", "General Device (4051)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Phantom Device (4052)</summary>
        public static readonly DicomUID PhantomDevice4052 = new DicomUID("1.2.840.10008.6.1.315", "Phantom Device (4052)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Language (5000)</summary>
        public static readonly DicomUID Language5000 = new DicomUID("1.2.840.10008.6.1.328", "Language (5000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Country (5001)</summary>
        public static readonly DicomUID Country5001 = new DicomUID("1.2.840.10008.6.1.329", "Country (5001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Overall Breast Composition (6000)</summary>
        public static readonly DicomUID OverallBreastComposition6000 = new DicomUID("1.2.840.10008.6.1.330", "Overall Breast Composition (6000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Overall Breast Composition from BI-RADS® (6001)</summary>
        public static readonly DicomUID OverallBreastCompositionFromBIRADS6001 = new DicomUID("1.2.840.10008.6.1.331", "Overall Breast Composition from BI-RADS® (6001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Change Since Last Mammogram or Prior Surgery (6002)</summary>
        public static readonly DicomUID ChangeSinceLastMammogramOrPriorSurgery6002 = new DicomUID("1.2.840.10008.6.1.332", "Change Since Last Mammogram or Prior Surgery (6002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Change Since Last Mammogram or Prior Surgery from BI-RADS® (6003)</summary>
        public static readonly DicomUID ChangeSinceLastMammogramOrPriorSurgeryFromBIRADS6003 = new DicomUID("1.2.840.10008.6.1.333", "Change Since Last Mammogram or Prior Surgery from BI-RADS® (6003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Shape Characteristic (6004)</summary>
        public static readonly DicomUID MammographyShapeCharacteristic6004 = new DicomUID("1.2.840.10008.6.1.334", "Mammography Shape Characteristic (6004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Shape Characteristic from BI-RADS® (6005)</summary>
        public static readonly DicomUID ShapeCharacteristicFromBIRADS6005 = new DicomUID("1.2.840.10008.6.1.335", "Shape Characteristic from BI-RADS® (6005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Margin Characteristic (6006)</summary>
        public static readonly DicomUID MammographyMarginCharacteristic6006 = new DicomUID("1.2.840.10008.6.1.336", "Mammography Margin Characteristic (6006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Margin Characteristic from BI-RADS® (6007)</summary>
        public static readonly DicomUID MarginCharacteristicFromBIRADS6007 = new DicomUID("1.2.840.10008.6.1.337", "Margin Characteristic from BI-RADS® (6007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Density Modifier (6008)</summary>
        public static readonly DicomUID DensityModifier6008 = new DicomUID("1.2.840.10008.6.1.338", "Density Modifier (6008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Density Modifier from BI-RADS® (6009)</summary>
        public static readonly DicomUID DensityModifierFromBIRADS6009 = new DicomUID("1.2.840.10008.6.1.339", "Density Modifier from BI-RADS® (6009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Calcification Type (6010)</summary>
        public static readonly DicomUID MammographyCalcificationType6010 = new DicomUID("1.2.840.10008.6.1.340", "Mammography Calcification Type (6010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcification Type from BI-RADS® (6011)</summary>
        public static readonly DicomUID CalcificationTypeFromBIRADS6011 = new DicomUID("1.2.840.10008.6.1.341", "Calcification Type from BI-RADS® (6011)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Mammography Pathology Code (6030)</summary>
        public static readonly DicomUID MammographyPathologyCode6030 = new DicomUID("1.2.840.10008.6.1.360", "Mammography Pathology Code (6030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Benign Pathology Code from BI-RADS® (6031)</summary>
        public static readonly DicomUID BenignPathologyCodeFromBIRADS6031 = new DicomUID("1.2.840.10008.6.1.361", "Benign Pathology Code from BI-RADS® (6031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: High Risk Lesion Pathology Code from BI-RADS® (6032)</summary>
        public static readonly DicomUID HighRiskLesionPathologyCodeFromBIRADS6032 = new DicomUID("1.2.840.10008.6.1.362", "High Risk Lesion Pathology Code from BI-RADS® (6032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Malignant Pathology Code from BI-RADS® (6033)</summary>
        public static readonly DicomUID MalignantPathologyCodeFromBIRADS6033 = new DicomUID("1.2.840.10008.6.1.363", "Malignant Pathology Code from BI-RADS® (6033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD Output Intended Use (6034)</summary>
        public static readonly DicomUID CADOutputIntendedUse6034 = new DicomUID("1.2.840.10008.6.1.364", "CAD Output Intended Use (6034)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Composite Feature Relation (6035)</summary>
        public static readonly DicomUID CompositeFeatureRelation6035 = new DicomUID("1.2.840.10008.6.1.365", "Composite Feature Relation (6035)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Feature Scope (6036)</summary>
        public static readonly DicomUID FeatureScope6036 = new DicomUID("1.2.840.10008.6.1.366", "Feature Scope (6036)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Result Status (6042)</summary>
        public static readonly DicomUID ResultStatus6042 = new DicomUID("1.2.840.10008.6.1.372", "Result Status (6042)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography CAD Analysis Type (6043)</summary>
        public static readonly DicomUID MammographyCADAnalysisType6043 = new DicomUID("1.2.840.10008.6.1.373", "Mammography CAD Analysis Type (6043)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Quality Assessment Type (6044)</summary>
        public static readonly DicomUID ImageQualityAssessmentType6044 = new DicomUID("1.2.840.10008.6.1.374", "Image Quality Assessment Type (6044)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammography Quality Control Standard Type (6045)</summary>
        public static readonly DicomUID MammographyQualityControlStandardType6045 = new DicomUID("1.2.840.10008.6.1.375", "Mammography Quality Control Standard Type (6045)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Follow-up Interval Unit (6046)</summary>
        public static readonly DicomUID FollowUpIntervalUnit6046 = new DicomUID("1.2.840.10008.6.1.376", "Follow-up Interval Unit (6046)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD Processing and Finding Summary (6047)</summary>
        public static readonly DicomUID CADProcessingAndFindingSummary6047 = new DicomUID("1.2.840.10008.6.1.377", "CAD Processing and Finding Summary (6047)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD Operating Point Axis Label (6048)</summary>
        public static readonly DicomUID CADOperatingPointAxisLabel6048 = new DicomUID("1.2.840.10008.6.1.378", "CAD Operating Point Axis Label (6048)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Procedure Reported (6050)</summary>
        public static readonly DicomUID BreastProcedureReported6050 = new DicomUID("1.2.840.10008.6.1.379", "Breast Procedure Reported (6050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Procedure Reason (6051)</summary>
        public static readonly DicomUID BreastProcedureReason6051 = new DicomUID("1.2.840.10008.6.1.380", "Breast Procedure Reason (6051)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Report Section Title (6052)</summary>
        public static readonly DicomUID BreastImagingReportSectionTitle6052 = new DicomUID("1.2.840.10008.6.1.381", "Breast Imaging Report Section Title (6052)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Report Element (6053)</summary>
        public static readonly DicomUID BreastImagingReportElement6053 = new DicomUID("1.2.840.10008.6.1.382", "Breast Imaging Report Element (6053)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Finding (6054)</summary>
        public static readonly DicomUID BreastImagingFinding6054 = new DicomUID("1.2.840.10008.6.1.383", "Breast Imaging Finding (6054)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Clinical Finding or Indicated Problem (6055)</summary>
        public static readonly DicomUID BreastClinicalFindingOrIndicatedProblem6055 = new DicomUID("1.2.840.10008.6.1.384", "Breast Clinical Finding or Indicated Problem (6055)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Associated Finding for Breast (6056)</summary>
        public static readonly DicomUID AssociatedFindingForBreast6056 = new DicomUID("1.2.840.10008.6.1.385", "Associated Finding for Breast (6056)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ductography Finding for Breast (6057)</summary>
        public static readonly DicomUID DuctographyFindingForBreast6057 = new DicomUID("1.2.840.10008.6.1.386", "Ductography Finding for Breast (6057)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Modifiers for Breast (6058)</summary>
        public static readonly DicomUID ProcedureModifiersForBreast6058 = new DicomUID("1.2.840.10008.6.1.387", "Procedure Modifiers for Breast (6058)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Implant Type (6059)</summary>
        public static readonly DicomUID BreastImplantType6059 = new DicomUID("1.2.840.10008.6.1.388", "Breast Implant Type (6059)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Biopsy Technique (6060)</summary>
        public static readonly DicomUID BreastBiopsyTechnique6060 = new DicomUID("1.2.840.10008.6.1.389", "Breast Biopsy Technique (6060)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Imaging Procedure Modifier (6061)</summary>
        public static readonly DicomUID BreastImagingProcedureModifier6061 = new DicomUID("1.2.840.10008.6.1.390", "Breast Imaging Procedure Modifier (6061)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interventional Procedure Complication (6062)</summary>
        public static readonly DicomUID InterventionalProcedureComplication6062 = new DicomUID("1.2.840.10008.6.1.391", "Interventional Procedure Complication (6062)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interventional Procedure Result (6063)</summary>
        public static readonly DicomUID InterventionalProcedureResult6063 = new DicomUID("1.2.840.10008.6.1.392", "Interventional Procedure Result (6063)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Finding for Breast (6064)</summary>
        public static readonly DicomUID UltrasoundFindingForBreast6064 = new DicomUID("1.2.840.10008.6.1.393", "Ultrasound Finding for Breast (6064)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Breast Implant Finding (6072)</summary>
        public static readonly DicomUID BreastImplantFinding6072 = new DicomUID("1.2.840.10008.6.1.401", "Breast Implant Finding (6072)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gynecological Hormone (6080)</summary>
        public static readonly DicomUID GynecologicalHormone6080 = new DicomUID("1.2.840.10008.6.1.402", "Gynecological Hormone (6080)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Cancer Risk Factor (6081)</summary>
        public static readonly DicomUID BreastCancerRiskFactor6081 = new DicomUID("1.2.840.10008.6.1.403", "Breast Cancer Risk Factor (6081)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gynecological Procedure (6082)</summary>
        public static readonly DicomUID GynecologicalProcedure6082 = new DicomUID("1.2.840.10008.6.1.404", "Gynecological Procedure (6082)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedures for Breast (6083)</summary>
        public static readonly DicomUID ProceduresForBreast6083 = new DicomUID("1.2.840.10008.6.1.405", "Procedures for Breast (6083)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mammoplasty Procedure (6084)</summary>
        public static readonly DicomUID MammoplastyProcedure6084 = new DicomUID("1.2.840.10008.6.1.406", "Mammoplasty Procedure (6084)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Therapies for Breast (6085)</summary>
        public static readonly DicomUID TherapiesForBreast6085 = new DicomUID("1.2.840.10008.6.1.407", "Therapies for Breast (6085)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Menopausal Phase (6086)</summary>
        public static readonly DicomUID MenopausalPhase6086 = new DicomUID("1.2.840.10008.6.1.408", "Menopausal Phase (6086)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Risk Factor (6087)</summary>
        public static readonly DicomUID GeneralRiskFactor6087 = new DicomUID("1.2.840.10008.6.1.409", "General Risk Factor (6087)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Maternal Risk Factor (6088)</summary>
        public static readonly DicomUID OBGYNMaternalRiskFactor6088 = new DicomUID("1.2.840.10008.6.1.410", "OB-GYN Maternal Risk Factor (6088)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Substance (6089)</summary>
        public static readonly DicomUID Substance6089 = new DicomUID("1.2.840.10008.6.1.411", "Substance (6089)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Usage/Exposure Amount (6090)</summary>
        public static readonly DicomUID RelativeUsageExposureAmount6090 = new DicomUID("1.2.840.10008.6.1.412", "Relative Usage/Exposure Amount (6090)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Frequency of Event Value (6091)</summary>
        public static readonly DicomUID RelativeFrequencyOfEventValue6091 = new DicomUID("1.2.840.10008.6.1.413", "Relative Frequency of Event Value (6091)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Usage/Exposure Qualitative Concept (6092)</summary>
        public static readonly DicomUID UsageExposureQualitativeConcept6092 = new DicomUID("1.2.840.10008.6.1.414", "Usage/Exposure Qualitative Concept (6092)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Usage/Exposure/Amount Qualitative Concept (6093)</summary>
        public static readonly DicomUID UsageExposureAmountQualitativeConcept6093 = new DicomUID("1.2.840.10008.6.1.415", "Usage/Exposure/Amount Qualitative Concept (6093)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Usage/Exposure/Frequency Qualitative Concept (6094)</summary>
        public static readonly DicomUID UsageExposureFrequencyQualitativeConcept6094 = new DicomUID("1.2.840.10008.6.1.416", "Usage/Exposure/Frequency Qualitative Concept (6094)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Numeric Property (6095)</summary>
        public static readonly DicomUID ProcedureNumericProperty6095 = new DicomUID("1.2.840.10008.6.1.417", "Procedure Numeric Property (6095)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pregnancy Status (6096)</summary>
        public static readonly DicomUID PregnancyStatus6096 = new DicomUID("1.2.840.10008.6.1.418", "Pregnancy Status (6096)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Side of Family (6097)</summary>
        public static readonly DicomUID SideOfFamily6097 = new DicomUID("1.2.840.10008.6.1.419", "Side of Family (6097)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Component Category (6100)</summary>
        public static readonly DicomUID ChestComponentCategory6100 = new DicomUID("1.2.840.10008.6.1.420", "Chest Component Category (6100)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Osseous Anatomy Modifier (6115)</summary>
        public static readonly DicomUID OsseousAnatomyModifier6115 = new DicomUID("1.2.840.10008.6.1.435", "Osseous Anatomy Modifier (6115)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: CAD Analysis Type (6137)</summary>
        public static readonly DicomUID CADAnalysisType6137 = new DicomUID("1.2.840.10008.6.1.457", "CAD Analysis Type (6137)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Non-lesion Object Type (6138)</summary>
        public static readonly DicomUID ChestNonLesionObjectType6138 = new DicomUID("1.2.840.10008.6.1.458", "Chest Non-lesion Object Type (6138)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Modifier (6139)</summary>
        public static readonly DicomUID NonLesionModifier6139 = new DicomUID("1.2.840.10008.6.1.459", "Non-lesion Modifier (6139)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calculation Method (6140)</summary>
        public static readonly DicomUID CalculationMethod6140 = new DicomUID("1.2.840.10008.6.1.460", "Calculation Method (6140)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuation Coefficient Measurement (6141)</summary>
        public static readonly DicomUID AttenuationCoefficientMeasurement6141 = new DicomUID("1.2.840.10008.6.1.461", "Attenuation Coefficient Measurement (6141)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Posterior Acoustic Feature (6155)</summary>
        public static readonly DicomUID PosteriorAcousticFeature6155 = new DicomUID("1.2.840.10008.6.1.470", "Posterior Acoustic Feature (6155)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascularity (6157)</summary>
        public static readonly DicomUID Vascularity6157 = new DicomUID("1.2.840.10008.6.1.471", "Vascularity (6157)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Correlation to Other Finding (6158)</summary>
        public static readonly DicomUID CorrelationToOtherFinding6158 = new DicomUID("1.2.840.10008.6.1.472", "Correlation to Other Finding (6158)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Malignancy Type (6159)</summary>
        public static readonly DicomUID MalignancyType6159 = new DicomUID("1.2.840.10008.6.1.473", "Malignancy Type (6159)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Primary Tumor Assessment From AJCC (6160)</summary>
        public static readonly DicomUID BreastPrimaryTumorAssessmentFromAJCC6160 = new DicomUID("1.2.840.10008.6.1.474", "Breast Primary Tumor Assessment From AJCC (6160)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pathological Regional Lymph Node Assessment for Breast (6161)</summary>
        public static readonly DicomUID PathologicalRegionalLymphNodeAssessmentForBreast6161 = new DicomUID("1.2.840.10008.6.1.475", "Pathological Regional Lymph Node Assessment for Breast (6161)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Assessment of Metastasis for Breast (6162)</summary>
        public static readonly DicomUID AssessmentOfMetastasisForBreast6162 = new DicomUID("1.2.840.10008.6.1.476", "Assessment of Metastasis for Breast (6162)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Menstrual Cycle Phase (6163)</summary>
        public static readonly DicomUID MenstrualCyclePhase6163 = new DicomUID("1.2.840.10008.6.1.477", "Menstrual Cycle Phase (6163)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time Interval (6164)</summary>
        public static readonly DicomUID TimeInterval6164 = new DicomUID("1.2.840.10008.6.1.478", "Time Interval (6164)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Linear Measurement (6165)</summary>
        public static readonly DicomUID BreastLinearMeasurement6165 = new DicomUID("1.2.840.10008.6.1.479", "Breast Linear Measurement (6165)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD Geometry Secondary Graphical Representation (6166)</summary>
        public static readonly DicomUID CADGeometrySecondaryGraphicalRepresentation6166 = new DicomUID("1.2.840.10008.6.1.480", "CAD Geometry Secondary Graphical Representation (6166)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Document Title (7000)</summary>
        public static readonly DicomUID DiagnosticImagingReportDocumentTitle7000 = new DicomUID("1.2.840.10008.6.1.481", "Diagnostic Imaging Report Document Title (7000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Heading (7001)</summary>
        public static readonly DicomUID DiagnosticImagingReportHeading7001 = new DicomUID("1.2.840.10008.6.1.482", "Diagnostic Imaging Report Heading (7001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Element (7002)</summary>
        public static readonly DicomUID DiagnosticImagingReportElement7002 = new DicomUID("1.2.840.10008.6.1.483", "Diagnostic Imaging Report Element (7002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diagnostic Imaging Report Purpose of Reference (7003)</summary>
        public static readonly DicomUID DiagnosticImagingReportPurposeOfReference7003 = new DicomUID("1.2.840.10008.6.1.484", "Diagnostic Imaging Report Purpose of Reference (7003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Waveform Purpose of Reference (7004)</summary>
        public static readonly DicomUID WaveformPurposeOfReference7004 = new DicomUID("1.2.840.10008.6.1.485", "Waveform Purpose of Reference (7004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contributing Equipment Purpose of Reference (7005)</summary>
        public static readonly DicomUID ContributingEquipmentPurposeOfReference7005 = new DicomUID("1.2.840.10008.6.1.486", "Contributing Equipment Purpose of Reference (7005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: SR Document Purpose of Reference (7006)</summary>
        public static readonly DicomUID SRDocumentPurposeOfReference7006 = new DicomUID("1.2.840.10008.6.1.487", "SR Document Purpose of Reference (7006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Signature Purpose (7007)</summary>
        public static readonly DicomUID SignaturePurpose7007 = new DicomUID("1.2.840.10008.6.1.488", "Signature Purpose (7007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Media Import (7008)</summary>
        public static readonly DicomUID MediaImport7008 = new DicomUID("1.2.840.10008.6.1.489", "Media Import (7008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Key Object Selection Document Title (7010)</summary>
        public static readonly DicomUID KeyObjectSelectionDocumentTitle7010 = new DicomUID("1.2.840.10008.6.1.490", "Key Object Selection Document Title (7010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Rejected for Quality Reason (7011)</summary>
        public static readonly DicomUID RejectedForQualityReason7011 = new DicomUID("1.2.840.10008.6.1.491", "Rejected for Quality Reason (7011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Best in Set (7012)</summary>
        public static readonly DicomUID BestInSet7012 = new DicomUID("1.2.840.10008.6.1.492", "Best in Set (7012)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Document Title (7020)</summary>
        public static readonly DicomUID DocumentTitle7020 = new DicomUID("1.2.840.10008.6.1.493", "Document Title (7020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RCS Registration Method Type (7100)</summary>
        public static readonly DicomUID RCSRegistrationMethodType7100 = new DicomUID("1.2.840.10008.6.1.494", "RCS Registration Method Type (7100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Atlas Fiducial (7101)</summary>
        public static readonly DicomUID BrainAtlasFiducial7101 = new DicomUID("1.2.840.10008.6.1.495", "Brain Atlas Fiducial (7101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmentation Property Category (7150)</summary>
        public static readonly DicomUID SegmentationPropertyCategory7150 = new DicomUID("1.2.840.10008.6.1.496", "Segmentation Property Category (7150)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmentation Property Type (7151)</summary>
        public static readonly DicomUID SegmentationPropertyType7151 = new DicomUID("1.2.840.10008.6.1.497", "Segmentation Property Type (7151)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Structure Segmentation Type (7152)</summary>
        public static readonly DicomUID CardiacStructureSegmentationType7152 = new DicomUID("1.2.840.10008.6.1.498", "Cardiac Structure Segmentation Type (7152)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CNS Segmentation Type (7153)</summary>
        public static readonly DicomUID CNSSegmentationType7153 = new DicomUID("1.2.840.10008.6.1.499", "CNS Segmentation Type (7153)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominal Segmentation Type (7154)</summary>
        public static readonly DicomUID AbdominalSegmentationType7154 = new DicomUID("1.2.840.10008.6.1.500", "Abdominal Segmentation Type (7154)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Thoracic Segmentation Type (7155)</summary>
        public static readonly DicomUID ThoracicSegmentationType7155 = new DicomUID("1.2.840.10008.6.1.501", "Thoracic Segmentation Type (7155)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Segmentation Type (7156)</summary>
        public static readonly DicomUID VascularSegmentationType7156 = new DicomUID("1.2.840.10008.6.1.502", "Vascular Segmentation Type (7156)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Segmentation Type (7157)</summary>
        public static readonly DicomUID DeviceSegmentationType7157 = new DicomUID("1.2.840.10008.6.1.503", "Device Segmentation Type (7157)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Artifact Segmentation Type (7158)</summary>
        public static readonly DicomUID ArtifactSegmentationType7158 = new DicomUID("1.2.840.10008.6.1.504", "Artifact Segmentation Type (7158)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Segmentation Type (7159)</summary>
        public static readonly DicomUID LesionSegmentationType7159 = new DicomUID("1.2.840.10008.6.1.505", "Lesion Segmentation Type (7159)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pelvic Organ Segmentation Type (7160)</summary>
        public static readonly DicomUID PelvicOrganSegmentationType7160 = new DicomUID("1.2.840.10008.6.1.506", "Pelvic Organ Segmentation Type (7160)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physiology Segmentation Type (7161)</summary>
        public static readonly DicomUID PhysiologySegmentationType7161 = new DicomUID("1.2.840.10008.6.1.507", "Physiology Segmentation Type (7161)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Referenced Image Purpose of Reference (7201)</summary>
        public static readonly DicomUID ReferencedImagePurposeOfReference7201 = new DicomUID("1.2.840.10008.6.1.508", "Referenced Image Purpose of Reference (7201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source Image Purpose of Reference (7202)</summary>
        public static readonly DicomUID SourceImagePurposeOfReference7202 = new DicomUID("1.2.840.10008.6.1.509", "Source Image Purpose of Reference (7202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Derivation (7203)</summary>
        public static readonly DicomUID ImageDerivation7203 = new DicomUID("1.2.840.10008.6.1.510", "Image Derivation (7203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference to Alternate Representation (7205)</summary>
        public static readonly DicomUID PurposeOfReferenceToAlternateRepresentation7205 = new DicomUID("1.2.840.10008.6.1.511", "Purpose of Reference to Alternate Representation (7205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Related Series Purpose of Reference (7210)</summary>
        public static readonly DicomUID RelatedSeriesPurposeOfReference7210 = new DicomUID("1.2.840.10008.6.1.512", "Related Series Purpose of Reference (7210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-Frame Subset Type (7250)</summary>
        public static readonly DicomUID MultiFrameSubsetType7250 = new DicomUID("1.2.840.10008.6.1.513", "Multi-Frame Subset Type (7250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Person Role (7450)</summary>
        public static readonly DicomUID PersonRole7450 = new DicomUID("1.2.840.10008.6.1.514", "Person Role (7450)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Family Member (7451)</summary>
        public static readonly DicomUID FamilyMember7451 = new DicomUID("1.2.840.10008.6.1.515", "Family Member (7451)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organizational Role (7452)</summary>
        public static readonly DicomUID OrganizationalRole7452 = new DicomUID("1.2.840.10008.6.1.516", "Organizational Role (7452)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Performing Role (7453)</summary>
        public static readonly DicomUID PerformingRole7453 = new DicomUID("1.2.840.10008.6.1.517", "Performing Role (7453)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Taxonomic Rank Value (7454)</summary>
        public static readonly DicomUID AnimalTaxonomicRankValue7454 = new DicomUID("1.2.840.10008.6.1.518", "Animal Taxonomic Rank Value (7454)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sex (7455)</summary>
        public static readonly DicomUID Sex7455 = new DicomUID("1.2.840.10008.6.1.519", "Sex (7455)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Age Unit (7456)</summary>
        public static readonly DicomUID AgeUnit7456 = new DicomUID("1.2.840.10008.6.1.520", "Age Unit (7456)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Linear Measurement Unit (7460)</summary>
        public static readonly DicomUID LinearMeasurementUnit7460 = new DicomUID("1.2.840.10008.6.1.521", "Linear Measurement Unit (7460)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Area Measurement Unit (7461)</summary>
        public static readonly DicomUID AreaMeasurementUnit7461 = new DicomUID("1.2.840.10008.6.1.522", "Area Measurement Unit (7461)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume Measurement Unit (7462)</summary>
        public static readonly DicomUID VolumeMeasurementUnit7462 = new DicomUID("1.2.840.10008.6.1.523", "Volume Measurement Unit (7462)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Linear Measurement (7470)</summary>
        public static readonly DicomUID LinearMeasurement7470 = new DicomUID("1.2.840.10008.6.1.524", "Linear Measurement (7470)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Area Measurement (7471)</summary>
        public static readonly DicomUID AreaMeasurement7471 = new DicomUID("1.2.840.10008.6.1.525", "Area Measurement (7471)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume Measurement (7472)</summary>
        public static readonly DicomUID VolumeMeasurement7472 = new DicomUID("1.2.840.10008.6.1.526", "Volume Measurement (7472)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Area Calculation Method (7473)</summary>
        public static readonly DicomUID GeneralAreaCalculationMethod7473 = new DicomUID("1.2.840.10008.6.1.527", "General Area Calculation Method (7473)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Volume Calculation Method (7474)</summary>
        public static readonly DicomUID GeneralVolumeCalculationMethod7474 = new DicomUID("1.2.840.10008.6.1.528", "General Volume Calculation Method (7474)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breed (7480)</summary>
        public static readonly DicomUID Breed7480 = new DicomUID("1.2.840.10008.6.1.529", "Breed (7480)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breed Registry (7481)</summary>
        public static readonly DicomUID BreedRegistry7481 = new DicomUID("1.2.840.10008.6.1.530", "Breed Registry (7481)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Workitem Definition (9231)</summary>
        public static readonly DicomUID WorkitemDefinition9231 = new DicomUID("1.2.840.10008.6.1.531", "Workitem Definition (9231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-DICOM Output Types (Retired) (9232)</summary>
        public static readonly DicomUID NonDICOMOutputTypes9232RETIRED = new DicomUID("1.2.840.10008.6.1.532", "Non-DICOM Output Types (Retired) (9232)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Procedure Discontinuation Reason (9300)</summary>
        public static readonly DicomUID ProcedureDiscontinuationReason9300 = new DicomUID("1.2.840.10008.6.1.533", "Procedure Discontinuation Reason (9300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Scope of Accumulation (10000)</summary>
        public static readonly DicomUID ScopeOfAccumulation10000 = new DicomUID("1.2.840.10008.6.1.534", "Scope of Accumulation (10000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: UID Type (10001)</summary>
        public static readonly DicomUID UIDType10001 = new DicomUID("1.2.840.10008.6.1.535", "UID Type (10001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Irradiation Event Type (10002)</summary>
        public static readonly DicomUID IrradiationEventType10002 = new DicomUID("1.2.840.10008.6.1.536", "Irradiation Event Type (10002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equipment Plane Identification (10003)</summary>
        public static readonly DicomUID EquipmentPlaneIdentification10003 = new DicomUID("1.2.840.10008.6.1.537", "Equipment Plane Identification (10003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fluoro Mode (10004)</summary>
        public static readonly DicomUID FluoroMode10004 = new DicomUID("1.2.840.10008.6.1.538", "Fluoro Mode (10004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: X-Ray Filter Material (10006)</summary>
        public static readonly DicomUID XRayFilterMaterial10006 = new DicomUID("1.2.840.10008.6.1.539", "X-Ray Filter Material (10006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: X-Ray Filter Type (10007)</summary>
        public static readonly DicomUID XRayFilterType10007 = new DicomUID("1.2.840.10008.6.1.540", "X-Ray Filter Type (10007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dose Related Distance Measurement (10008)</summary>
        public static readonly DicomUID DoseRelatedDistanceMeasurement10008 = new DicomUID("1.2.840.10008.6.1.541", "Dose Related Distance Measurement (10008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measured/Calculated (10009)</summary>
        public static readonly DicomUID MeasuredCalculated10009 = new DicomUID("1.2.840.10008.6.1.542", "Measured/Calculated (10009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dose Measurement Device (10010)</summary>
        public static readonly DicomUID DoseMeasurementDevice10010 = new DicomUID("1.2.840.10008.6.1.543", "Dose Measurement Device (10010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Effective Dose Evaluation Method (10011)</summary>
        public static readonly DicomUID EffectiveDoseEvaluationMethod10011 = new DicomUID("1.2.840.10008.6.1.544", "Effective Dose Evaluation Method (10011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT Acquisition Type (10013)</summary>
        public static readonly DicomUID CTAcquisitionType10013 = new DicomUID("1.2.840.10008.6.1.545", "CT Acquisition Type (10013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contrast Imaging Technique (10014)</summary>
        public static readonly DicomUID ContrastImagingTechnique10014 = new DicomUID("1.2.840.10008.6.1.546", "Contrast Imaging Technique (10014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT Dose Reference Authority (10015)</summary>
        public static readonly DicomUID CTDoseReferenceAuthority10015 = new DicomUID("1.2.840.10008.6.1.547", "CT Dose Reference Authority (10015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anode Target Material (10016)</summary>
        public static readonly DicomUID AnodeTargetMaterial10016 = new DicomUID("1.2.840.10008.6.1.548", "Anode Target Material (10016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: X-Ray Grid (10017)</summary>
        public static readonly DicomUID XRayGrid10017 = new DicomUID("1.2.840.10008.6.1.549", "X-Ray Grid (10017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Protocol Type (12001)</summary>
        public static readonly DicomUID UltrasoundProtocolType12001 = new DicomUID("1.2.840.10008.6.1.550", "Ultrasound Protocol Type (12001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Protocol Stage Type (12002)</summary>
        public static readonly DicomUID UltrasoundProtocolStageType12002 = new DicomUID("1.2.840.10008.6.1.551", "Ultrasound Protocol Stage Type (12002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Date (12003)</summary>
        public static readonly DicomUID OBGYNDate12003 = new DicomUID("1.2.840.10008.6.1.552", "OB-GYN Date (12003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Biometry Ratio (12004)</summary>
        public static readonly DicomUID FetalBiometryRatio12004 = new DicomUID("1.2.840.10008.6.1.553", "Fetal Biometry Ratio (12004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Biometry Measurement (12005)</summary>
        public static readonly DicomUID FetalBiometryMeasurement12005 = new DicomUID("1.2.840.10008.6.1.554", "Fetal Biometry Measurement (12005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Long Bones Biometry Measurement (12006)</summary>
        public static readonly DicomUID FetalLongBonesBiometryMeasurement12006 = new DicomUID("1.2.840.10008.6.1.555", "Fetal Long Bones Biometry Measurement (12006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Cranium Measurement (12007)</summary>
        public static readonly DicomUID FetalCraniumMeasurement12007 = new DicomUID("1.2.840.10008.6.1.556", "Fetal Cranium Measurement (12007)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Amniotic Sac Measurement (12008)</summary>
        public static readonly DicomUID OBGYNAmnioticSacMeasurement12008 = new DicomUID("1.2.840.10008.6.1.557", "OB-GYN Amniotic Sac Measurement (12008)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Early Gestation Biometry Measurement (12009)</summary>
        public static readonly DicomUID EarlyGestationBiometryMeasurement12009 = new DicomUID("1.2.840.10008.6.1.558", "Early Gestation Biometry Measurement (12009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Pelvis and Uterus Measurement (12011)</summary>
        public static readonly DicomUID UltrasoundPelvisAndUterusMeasurement12011 = new DicomUID("1.2.840.10008.6.1.559", "Ultrasound Pelvis and Uterus Measurement (12011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB Equation/Table (12012)</summary>
        public static readonly DicomUID OBEquationTable12012 = new DicomUID("1.2.840.10008.6.1.560", "OB Equation/Table (12012)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gestational Age Equation/Table (12013)</summary>
        public static readonly DicomUID GestationalAgeEquationTable12013 = new DicomUID("1.2.840.10008.6.1.561", "Gestational Age Equation/Table (12013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB Fetal Body Weight Equation/Table (12014)</summary>
        public static readonly DicomUID OBFetalBodyWeightEquationTable12014 = new DicomUID("1.2.840.10008.6.1.562", "OB Fetal Body Weight Equation/Table (12014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Growth Equation/Table (12015)</summary>
        public static readonly DicomUID FetalGrowthEquationTable12015 = new DicomUID("1.2.840.10008.6.1.563", "Fetal Growth Equation/Table (12015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Estimated Fetal Weight Percentile Equation/Table (12016)</summary>
        public static readonly DicomUID EstimatedFetalWeightPercentileEquationTable12016 = new DicomUID("1.2.840.10008.6.1.564", "Estimated Fetal Weight Percentile Equation/Table (12016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Growth Distribution Rank (12017)</summary>
        public static readonly DicomUID GrowthDistributionRank12017 = new DicomUID("1.2.840.10008.6.1.565", "Growth Distribution Rank (12017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Summary (12018)</summary>
        public static readonly DicomUID OBGYNSummary12018 = new DicomUID("1.2.840.10008.6.1.566", "OB-GYN Summary (12018)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Fetus Summary (12019)</summary>
        public static readonly DicomUID OBGYNFetusSummary12019 = new DicomUID("1.2.840.10008.6.1.567", "OB-GYN Fetus Summary (12019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Summary (12101)</summary>
        public static readonly DicomUID VascularSummary12101 = new DicomUID("1.2.840.10008.6.1.568", "Vascular Summary (12101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Temporal Period Relating to Procedure or Therapy (12102)</summary>
        public static readonly DicomUID TemporalPeriodRelatingToProcedureOrTherapy12102 = new DicomUID("1.2.840.10008.6.1.569", "Temporal Period Relating to Procedure or Therapy (12102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Ultrasound Anatomic Location (12103)</summary>
        public static readonly DicomUID VascularUltrasoundAnatomicLocation12103 = new DicomUID("1.2.840.10008.6.1.570", "Vascular Ultrasound Anatomic Location (12103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Extracranial Artery (12104)</summary>
        public static readonly DicomUID ExtracranialArtery12104 = new DicomUID("1.2.840.10008.6.1.571", "Extracranial Artery (12104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intracranial Cerebral Vessel (12105)</summary>
        public static readonly DicomUID IntracranialCerebralVessel12105 = new DicomUID("1.2.840.10008.6.1.572", "Intracranial Cerebral Vessel (12105)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intracranial Cerebral Vessel (Unilateral) (12106)</summary>
        public static readonly DicomUID IntracranialCerebralVesselUnilateral12106 = new DicomUID("1.2.840.10008.6.1.573", "Intracranial Cerebral Vessel (Unilateral) (12106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Upper Extremity Artery (12107)</summary>
        public static readonly DicomUID UpperExtremityArtery12107 = new DicomUID("1.2.840.10008.6.1.574", "Upper Extremity Artery (12107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Upper Extremity Vein (12108)</summary>
        public static readonly DicomUID UpperExtremityVein12108 = new DicomUID("1.2.840.10008.6.1.575", "Upper Extremity Vein (12108)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lower Extremity Artery (12109)</summary>
        public static readonly DicomUID LowerExtremityArtery12109 = new DicomUID("1.2.840.10008.6.1.576", "Lower Extremity Artery (12109)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lower Extremity Vein (12110)</summary>
        public static readonly DicomUID LowerExtremityVein12110 = new DicomUID("1.2.840.10008.6.1.577", "Lower Extremity Vein (12110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominopelvic Artery (Paired) (12111)</summary>
        public static readonly DicomUID AbdominopelvicArteryPaired12111 = new DicomUID("1.2.840.10008.6.1.578", "Abdominopelvic Artery (Paired) (12111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominopelvic Artery (Unpaired) (12112)</summary>
        public static readonly DicomUID AbdominopelvicArteryUnpaired12112 = new DicomUID("1.2.840.10008.6.1.579", "Abdominopelvic Artery (Unpaired) (12112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominopelvic Vein (Paired) (12113)</summary>
        public static readonly DicomUID AbdominopelvicVeinPaired12113 = new DicomUID("1.2.840.10008.6.1.580", "Abdominopelvic Vein (Paired) (12113)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominopelvic Vein (Unpaired) (12114)</summary>
        public static readonly DicomUID AbdominopelvicVeinUnpaired12114 = new DicomUID("1.2.840.10008.6.1.581", "Abdominopelvic Vein (Unpaired) (12114)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Renal Vessel (12115)</summary>
        public static readonly DicomUID RenalVessel12115 = new DicomUID("1.2.840.10008.6.1.582", "Renal Vessel (12115)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Segment Modifier (12116)</summary>
        public static readonly DicomUID VesselSegmentModifier12116 = new DicomUID("1.2.840.10008.6.1.583", "Vessel Segment Modifier (12116)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vessel Branch Modifier (12117)</summary>
        public static readonly DicomUID VesselBranchModifier12117 = new DicomUID("1.2.840.10008.6.1.584", "Vessel Branch Modifier (12117)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Ultrasound Property (12119)</summary>
        public static readonly DicomUID VascularUltrasoundProperty12119 = new DicomUID("1.2.840.10008.6.1.585", "Vascular Ultrasound Property (12119)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Blood Velocity Measurement (12120)</summary>
        public static readonly DicomUID UltrasoundBloodVelocityMeasurement12120 = new DicomUID("1.2.840.10008.6.1.586", "Ultrasound Blood Velocity Measurement (12120)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Index/Ratio (12121)</summary>
        public static readonly DicomUID VascularIndexRatio12121 = new DicomUID("1.2.840.10008.6.1.587", "Vascular Index/Ratio (12121)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Other Vascular Property (12122)</summary>
        public static readonly DicomUID OtherVascularProperty12122 = new DicomUID("1.2.840.10008.6.1.588", "Other Vascular Property (12122)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Carotid Ratio (12123)</summary>
        public static readonly DicomUID CarotidRatio12123 = new DicomUID("1.2.840.10008.6.1.589", "Carotid Ratio (12123)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Renal Ratio (12124)</summary>
        public static readonly DicomUID RenalRatio12124 = new DicomUID("1.2.840.10008.6.1.590", "Renal Ratio (12124)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pelvic Vasculature Anatomical Location (12140)</summary>
        public static readonly DicomUID PelvicVasculatureAnatomicalLocation12140 = new DicomUID("1.2.840.10008.6.1.591", "Pelvic Vasculature Anatomical Location (12140)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Vasculature Anatomical Location (12141)</summary>
        public static readonly DicomUID FetalVasculatureAnatomicalLocation12141 = new DicomUID("1.2.840.10008.6.1.592", "Fetal Vasculature Anatomical Location (12141)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Left Ventricle Measurement (12200)</summary>
        public static readonly DicomUID EchocardiographyLeftVentricleMeasurement12200 = new DicomUID("1.2.840.10008.6.1.593", "Echocardiography Left Ventricle Measurement (12200)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Linear Measurement (12201)</summary>
        public static readonly DicomUID LeftVentricleLinearMeasurement12201 = new DicomUID("1.2.840.10008.6.1.594", "Left Ventricle Linear Measurement (12201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Volume Measurement (12202)</summary>
        public static readonly DicomUID LeftVentricleVolumeMeasurement12202 = new DicomUID("1.2.840.10008.6.1.595", "Left Ventricle Volume Measurement (12202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Other Measurement (12203)</summary>
        public static readonly DicomUID LeftVentricleOtherMeasurement12203 = new DicomUID("1.2.840.10008.6.1.596", "Left Ventricle Other Measurement (12203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Right Ventricle Measurement (12204)</summary>
        public static readonly DicomUID EchocardiographyRightVentricleMeasurement12204 = new DicomUID("1.2.840.10008.6.1.597", "Echocardiography Right Ventricle Measurement (12204)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Left Atrium Measurement (12205)</summary>
        public static readonly DicomUID EchocardiographyLeftAtriumMeasurement12205 = new DicomUID("1.2.840.10008.6.1.598", "Echocardiography Left Atrium Measurement (12205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Right Atrium Measurement (12206)</summary>
        public static readonly DicomUID EchocardiographyRightAtriumMeasurement12206 = new DicomUID("1.2.840.10008.6.1.599", "Echocardiography Right Atrium Measurement (12206)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Mitral Valve Measurement (12207)</summary>
        public static readonly DicomUID EchocardiographyMitralValveMeasurement12207 = new DicomUID("1.2.840.10008.6.1.600", "Echocardiography Mitral Valve Measurement (12207)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Tricuspid Valve Measurement (12208)</summary>
        public static readonly DicomUID EchocardiographyTricuspidValveMeasurement12208 = new DicomUID("1.2.840.10008.6.1.601", "Echocardiography Tricuspid Valve Measurement (12208)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Pulmonic Valve Measurement (12209)</summary>
        public static readonly DicomUID EchocardiographyPulmonicValveMeasurement12209 = new DicomUID("1.2.840.10008.6.1.602", "Echocardiography Pulmonic Valve Measurement (12209)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Pulmonary Artery Measurement (12210)</summary>
        public static readonly DicomUID EchocardiographyPulmonaryArteryMeasurement12210 = new DicomUID("1.2.840.10008.6.1.603", "Echocardiography Pulmonary Artery Measurement (12210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Aortic Valve Measurement (12211)</summary>
        public static readonly DicomUID EchocardiographyAorticValveMeasurement12211 = new DicomUID("1.2.840.10008.6.1.604", "Echocardiography Aortic Valve Measurement (12211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Aorta Measurement (12212)</summary>
        public static readonly DicomUID EchocardiographyAortaMeasurement12212 = new DicomUID("1.2.840.10008.6.1.605", "Echocardiography Aorta Measurement (12212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Pulmonary Vein Measurement (12214)</summary>
        public static readonly DicomUID EchocardiographyPulmonaryVeinMeasurement12214 = new DicomUID("1.2.840.10008.6.1.606", "Echocardiography Pulmonary Vein Measurement (12214)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Vena Cava Measurement (12215)</summary>
        public static readonly DicomUID EchocardiographyVenaCavaMeasurement12215 = new DicomUID("1.2.840.10008.6.1.607", "Echocardiography Vena Cava Measurement (12215)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Hepatic Vein Measurement (12216)</summary>
        public static readonly DicomUID EchocardiographyHepaticVeinMeasurement12216 = new DicomUID("1.2.840.10008.6.1.608", "Echocardiography Hepatic Vein Measurement (12216)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Cardiac Shunt Measurement (12217)</summary>
        public static readonly DicomUID EchocardiographyCardiacShuntMeasurement12217 = new DicomUID("1.2.840.10008.6.1.609", "Echocardiography Cardiac Shunt Measurement (12217)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Congenital Anomaly Measurement (12218)</summary>
        public static readonly DicomUID EchocardiographyCongenitalAnomalyMeasurement12218 = new DicomUID("1.2.840.10008.6.1.610", "Echocardiography Congenital Anomaly Measurement (12218)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pulmonary Vein Modifier (12219)</summary>
        public static readonly DicomUID PulmonaryVeinModifier12219 = new DicomUID("1.2.840.10008.6.1.611", "Pulmonary Vein Modifier (12219)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Common Measurement (12220)</summary>
        public static readonly DicomUID EchocardiographyCommonMeasurement12220 = new DicomUID("1.2.840.10008.6.1.612", "Echocardiography Common Measurement (12220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Flow Direction (12221)</summary>
        public static readonly DicomUID FlowDirection12221 = new DicomUID("1.2.840.10008.6.1.613", "Flow Direction (12221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Orifice Flow Property (12222)</summary>
        public static readonly DicomUID OrificeFlowProperty12222 = new DicomUID("1.2.840.10008.6.1.614", "Orifice Flow Property (12222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Stroke Volume Origin (12223)</summary>
        public static readonly DicomUID EchocardiographyStrokeVolumeOrigin12223 = new DicomUID("1.2.840.10008.6.1.615", "Echocardiography Stroke Volume Origin (12223)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Image Mode (12224)</summary>
        public static readonly DicomUID UltrasoundImageMode12224 = new DicomUID("1.2.840.10008.6.1.616", "Ultrasound Image Mode (12224)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Image View (12226)</summary>
        public static readonly DicomUID EchocardiographyImageView12226 = new DicomUID("1.2.840.10008.6.1.617", "Echocardiography Image View (12226)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Measurement Method (12227)</summary>
        public static readonly DicomUID EchocardiographyMeasurementMethod12227 = new DicomUID("1.2.840.10008.6.1.618", "Echocardiography Measurement Method (12227)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Volume Method (12228)</summary>
        public static readonly DicomUID EchocardiographyVolumeMethod12228 = new DicomUID("1.2.840.10008.6.1.619", "Echocardiography Volume Method (12228)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Area Method (12229)</summary>
        public static readonly DicomUID EchocardiographyAreaMethod12229 = new DicomUID("1.2.840.10008.6.1.620", "Echocardiography Area Method (12229)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gradient Method (12230)</summary>
        public static readonly DicomUID GradientMethod12230 = new DicomUID("1.2.840.10008.6.1.621", "Gradient Method (12230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume Flow Method (12231)</summary>
        public static readonly DicomUID VolumeFlowMethod12231 = new DicomUID("1.2.840.10008.6.1.622", "Volume Flow Method (12231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardium Mass Method (12232)</summary>
        public static readonly DicomUID MyocardiumMassMethod12232 = new DicomUID("1.2.840.10008.6.1.623", "Myocardium Mass Method (12232)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Phase (12233)</summary>
        public static readonly DicomUID CardiacPhase12233 = new DicomUID("1.2.840.10008.6.1.624", "Cardiac Phase (12233)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiration State (12234)</summary>
        public static readonly DicomUID RespirationState12234 = new DicomUID("1.2.840.10008.6.1.625", "Respiration State (12234)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mitral Valve Anatomic Site (12235)</summary>
        public static readonly DicomUID MitralValveAnatomicSite12235 = new DicomUID("1.2.840.10008.6.1.626", "Mitral Valve Anatomic Site (12235)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Anatomic Site (12236)</summary>
        public static readonly DicomUID EchocardiographyAnatomicSite12236 = new DicomUID("1.2.840.10008.6.1.627", "Echocardiography Anatomic Site (12236)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echocardiography Anatomic Site Modifier (12237)</summary>
        public static readonly DicomUID EchocardiographyAnatomicSiteModifier12237 = new DicomUID("1.2.840.10008.6.1.628", "Echocardiography Anatomic Site Modifier (12237)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wall Motion Scoring Scheme (12238)</summary>
        public static readonly DicomUID WallMotionScoringScheme12238 = new DicomUID("1.2.840.10008.6.1.629", "Wall Motion Scoring Scheme (12238)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Output Property (12239)</summary>
        public static readonly DicomUID CardiacOutputProperty12239 = new DicomUID("1.2.840.10008.6.1.630", "Cardiac Output Property (12239)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Area Measurement (12240)</summary>
        public static readonly DicomUID LeftVentricleAreaMeasurement12240 = new DicomUID("1.2.840.10008.6.1.631", "Left Ventricle Area Measurement (12240)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tricuspid Valve Finding Site (12241)</summary>
        public static readonly DicomUID TricuspidValveFindingSite12241 = new DicomUID("1.2.840.10008.6.1.632", "Tricuspid Valve Finding Site (12241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Aortic Valve Finding Site (12242)</summary>
        public static readonly DicomUID AorticValveFindingSite12242 = new DicomUID("1.2.840.10008.6.1.633", "Aortic Valve Finding Site (12242)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Finding Site (12243)</summary>
        public static readonly DicomUID LeftVentricleFindingSite12243 = new DicomUID("1.2.840.10008.6.1.634", "Left Ventricle Finding Site (12243)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Congenital Finding Site (12244)</summary>
        public static readonly DicomUID CongenitalFindingSite12244 = new DicomUID("1.2.840.10008.6.1.635", "Congenital Finding Site (12244)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Processing Algorithm Family (7162)</summary>
        public static readonly DicomUID SurfaceProcessingAlgorithmFamily7162 = new DicomUID("1.2.840.10008.6.1.636", "Surface Processing Algorithm Family (7162)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Test Procedure Phase (3207)</summary>
        public static readonly DicomUID StressTestProcedurePhase3207 = new DicomUID("1.2.840.10008.6.1.637", "Stress Test Procedure Phase (3207)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stage (3778)</summary>
        public static readonly DicomUID Stage3778 = new DicomUID("1.2.840.10008.6.1.638", "Stage (3778)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: S-M-L Size Descriptor (252)</summary>
        public static readonly DicomUID SMLSizeDescriptor252 = new DicomUID("1.2.840.10008.6.1.735", "S-M-L Size Descriptor (252)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Major Coronary Artery (3016)</summary>
        public static readonly DicomUID MajorCoronaryArtery3016 = new DicomUID("1.2.840.10008.6.1.736", "Major Coronary Artery (3016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radioactivity Unit (3083)</summary>
        public static readonly DicomUID RadioactivityUnit3083 = new DicomUID("1.2.840.10008.6.1.737", "Radioactivity Unit (3083)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Rest/Stress State (3102)</summary>
        public static readonly DicomUID RestStressState3102 = new DicomUID("1.2.840.10008.6.1.738", "Rest/Stress State (3102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Cardiology Protocol (3106)</summary>
        public static readonly DicomUID PETCardiologyProtocol3106 = new DicomUID("1.2.840.10008.6.1.739", "PET Cardiology Protocol (3106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Cardiology Radiopharmaceutical (3107)</summary>
        public static readonly DicomUID PETCardiologyRadiopharmaceutical3107 = new DicomUID("1.2.840.10008.6.1.740", "PET Cardiology Radiopharmaceutical (3107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: NM/PET Procedure (3108)</summary>
        public static readonly DicomUID NMPETProcedure3108 = new DicomUID("1.2.840.10008.6.1.741", "NM/PET Procedure (3108)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nuclear Cardiology Protocol (3110)</summary>
        public static readonly DicomUID NuclearCardiologyProtocol3110 = new DicomUID("1.2.840.10008.6.1.742", "Nuclear Cardiology Protocol (3110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Nuclear Cardiology Radiopharmaceutical (3111)</summary>
        public static readonly DicomUID NuclearCardiologyRadiopharmaceutical3111 = new DicomUID("1.2.840.10008.6.1.743", "Nuclear Cardiology Radiopharmaceutical (3111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuation Correction (3112)</summary>
        public static readonly DicomUID AttenuationCorrection3112 = new DicomUID("1.2.840.10008.6.1.744", "Attenuation Correction (3112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Defect Type (3113)</summary>
        public static readonly DicomUID PerfusionDefectType3113 = new DicomUID("1.2.840.10008.6.1.745", "Perfusion Defect Type (3113)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Study Quality (3114)</summary>
        public static readonly DicomUID StudyQuality3114 = new DicomUID("1.2.840.10008.6.1.746", "Study Quality (3114)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Imaging Quality Issue (3115)</summary>
        public static readonly DicomUID StressImagingQualityIssue3115 = new DicomUID("1.2.840.10008.6.1.747", "Stress Imaging Quality Issue (3115)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: NM Extracardiac Finding (3116)</summary>
        public static readonly DicomUID NMExtracardiacFinding3116 = new DicomUID("1.2.840.10008.6.1.748", "NM Extracardiac Finding (3116)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuation Correction Method (3117)</summary>
        public static readonly DicomUID AttenuationCorrectionMethod3117 = new DicomUID("1.2.840.10008.6.1.749", "Attenuation Correction Method (3117)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Level of Risk (3118)</summary>
        public static readonly DicomUID LevelOfRisk3118 = new DicomUID("1.2.840.10008.6.1.750", "Level of Risk (3118)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: LV Function (3119)</summary>
        public static readonly DicomUID LVFunction3119 = new DicomUID("1.2.840.10008.6.1.751", "LV Function (3119)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Finding (3120)</summary>
        public static readonly DicomUID PerfusionFinding3120 = new DicomUID("1.2.840.10008.6.1.752", "Perfusion Finding (3120)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Stress Agent (3204)</summary>
        public static readonly DicomUID StressAgent3204 = new DicomUID("1.2.840.10008.6.1.759", "Stress Agent (3204)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Indications for Pharmacological Stress Test (3205)</summary>
        public static readonly DicomUID IndicationsForPharmacologicalStressTest3205 = new DicomUID("1.2.840.10008.6.1.760", "Indications for Pharmacological Stress Test (3205)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-invasive Cardiac Imaging Procedure (3206)</summary>
        public static readonly DicomUID NonInvasiveCardiacImagingProcedure3206 = new DicomUID("1.2.840.10008.6.1.761", "Non-invasive Cardiac Imaging Procedure (3206)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exercise ECG Summary Code (3208)</summary>
        public static readonly DicomUID ExerciseECGSummaryCode3208 = new DicomUID("1.2.840.10008.6.1.763", "Exercise ECG Summary Code (3208)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Imaging Summary Code (3209)</summary>
        public static readonly DicomUID StressImagingSummaryCode3209 = new DicomUID("1.2.840.10008.6.1.764", "Stress Imaging Summary Code (3209)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Speed of Response (3210)</summary>
        public static readonly DicomUID SpeedOfResponse3210 = new DicomUID("1.2.840.10008.6.1.765", "Speed of Response (3210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: BP Response (3211)</summary>
        public static readonly DicomUID BPResponse3211 = new DicomUID("1.2.840.10008.6.1.766", "BP Response (3211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treadmill Speed (3212)</summary>
        public static readonly DicomUID TreadmillSpeed3212 = new DicomUID("1.2.840.10008.6.1.767", "Treadmill Speed (3212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Hemodynamic Finding (3213)</summary>
        public static readonly DicomUID StressHemodynamicFinding3213 = new DicomUID("1.2.840.10008.6.1.768", "Stress Hemodynamic Finding (3213)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Finding Method (3215)</summary>
        public static readonly DicomUID PerfusionFindingMethod3215 = new DicomUID("1.2.840.10008.6.1.769", "Perfusion Finding Method (3215)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Comparison Finding (3217)</summary>
        public static readonly DicomUID ComparisonFinding3217 = new DicomUID("1.2.840.10008.6.1.770", "Comparison Finding (3217)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Symptom (3220)</summary>
        public static readonly DicomUID StressSymptom3220 = new DicomUID("1.2.840.10008.6.1.771", "Stress Symptom (3220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Test Termination Reason (3221)</summary>
        public static readonly DicomUID StressTestTerminationReason3221 = new DicomUID("1.2.840.10008.6.1.772", "Stress Test Termination Reason (3221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: QTc Measurement (3227)</summary>
        public static readonly DicomUID QTcMeasurement3227 = new DicomUID("1.2.840.10008.6.1.773", "QTc Measurement (3227)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Timing Measurement (3228)</summary>
        public static readonly DicomUID ECGTimingMeasurement3228 = new DicomUID("1.2.840.10008.6.1.774", "ECG Timing Measurement (3228)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Axis Measurement (3229)</summary>
        public static readonly DicomUID ECGAxisMeasurement3229 = new DicomUID("1.2.840.10008.6.1.775", "ECG Axis Measurement (3229)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Finding (3230)</summary>
        public static readonly DicomUID ECGFinding3230 = new DicomUID("1.2.840.10008.6.1.776", "ECG Finding (3230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ST Segment Finding (3231)</summary>
        public static readonly DicomUID STSegmentFinding3231 = new DicomUID("1.2.840.10008.6.1.777", "ST Segment Finding (3231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ST Segment Location (3232)</summary>
        public static readonly DicomUID STSegmentLocation3232 = new DicomUID("1.2.840.10008.6.1.778", "ST Segment Location (3232)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ST Segment Morphology (3233)</summary>
        public static readonly DicomUID STSegmentMorphology3233 = new DicomUID("1.2.840.10008.6.1.779", "ST Segment Morphology (3233)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ectopic Beat Morphology (3234)</summary>
        public static readonly DicomUID EctopicBeatMorphology3234 = new DicomUID("1.2.840.10008.6.1.780", "Ectopic Beat Morphology (3234)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Comparison Finding (3235)</summary>
        public static readonly DicomUID PerfusionComparisonFinding3235 = new DicomUID("1.2.840.10008.6.1.781", "Perfusion Comparison Finding (3235)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tolerance Comparison Finding (3236)</summary>
        public static readonly DicomUID ToleranceComparisonFinding3236 = new DicomUID("1.2.840.10008.6.1.782", "Tolerance Comparison Finding (3236)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wall Motion Comparison Finding (3237)</summary>
        public static readonly DicomUID WallMotionComparisonFinding3237 = new DicomUID("1.2.840.10008.6.1.783", "Wall Motion Comparison Finding (3237)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Stress Scoring Scale (3238)</summary>
        public static readonly DicomUID StressScoringScale3238 = new DicomUID("1.2.840.10008.6.1.784", "Stress Scoring Scale (3238)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perceived Exertion Scale (3239)</summary>
        public static readonly DicomUID PerceivedExertionScale3239 = new DicomUID("1.2.840.10008.6.1.785", "Perceived Exertion Scale (3239)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Anatomic Non-colon Finding (6204)</summary>
        public static readonly DicomUID AnatomicNonColonFinding6204 = new DicomUID("1.2.840.10008.6.1.791", "Anatomic Non-colon Finding (6204)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Calculated Value for Colon Finding (6212)</summary>
        public static readonly DicomUID CalculatedValueForColonFinding6212 = new DicomUID("1.2.840.10008.6.1.799", "Calculated Value for Colon Finding (6212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Horizontal Direction (4214)</summary>
        public static readonly DicomUID OphthalmicHorizontalDirection4214 = new DicomUID("1.2.840.10008.6.1.800", "Ophthalmic Horizontal Direction (4214)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Vertical Direction (4215)</summary>
        public static readonly DicomUID OphthalmicVerticalDirection4215 = new DicomUID("1.2.840.10008.6.1.801", "Ophthalmic Vertical Direction (4215)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Visual Acuity Type (4216)</summary>
        public static readonly DicomUID OphthalmicVisualAcuityType4216 = new DicomUID("1.2.840.10008.6.1.802", "Ophthalmic Visual Acuity Type (4216)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Pulse Waveform (3004)</summary>
        public static readonly DicomUID ArterialPulseWaveform3004 = new DicomUID("1.2.840.10008.6.1.803", "Arterial Pulse Waveform (3004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Respiration Waveform (3005)</summary>
        public static readonly DicomUID RespirationWaveform3005 = new DicomUID("1.2.840.10008.6.1.804", "Respiration Waveform (3005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Contrast/Bolus Agent (12030)</summary>
        public static readonly DicomUID UltrasoundContrastBolusAgent12030 = new DicomUID("1.2.840.10008.6.1.805", "Ultrasound Contrast/Bolus Agent (12030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Protocol Interval Event (12031)</summary>
        public static readonly DicomUID ProtocolIntervalEvent12031 = new DicomUID("1.2.840.10008.6.1.806", "Protocol Interval Event (12031)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Modality PPS Discontinuation Reason (9301)</summary>
        public static readonly DicomUID ModalityPPSDiscontinuationReason9301 = new DicomUID("1.2.840.10008.6.1.812", "Modality PPS Discontinuation Reason (9301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Media Import PPS Discontinuation Reason (9302)</summary>
        public static readonly DicomUID MediaImportPPSDiscontinuationReason9302 = new DicomUID("1.2.840.10008.6.1.813", "Media Import PPS Discontinuation Reason (9302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DX Anatomy Imaged for Animal (7482)</summary>
        public static readonly DicomUID DXAnatomyImagedForAnimal7482 = new DicomUID("1.2.840.10008.6.1.814", "DX Anatomy Imaged for Animal (7482)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Common Anatomic Regions for Animal (7483)</summary>
        public static readonly DicomUID CommonAnatomicRegionsForAnimal7483 = new DicomUID("1.2.840.10008.6.1.815", "Common Anatomic Regions for Animal (7483)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: DX View for Animal (7484)</summary>
        public static readonly DicomUID DXViewForAnimal7484 = new DicomUID("1.2.840.10008.6.1.816", "DX View for Animal (7484)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Institutional Department/Unit/Service (7030)</summary>
        public static readonly DicomUID InstitutionalDepartmentUnitService7030 = new DicomUID("1.2.840.10008.6.1.817", "Institutional Department/Unit/Service (7030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference to Predecessor Report (7009)</summary>
        public static readonly DicomUID PurposeOfReferenceToPredecessorReport7009 = new DicomUID("1.2.840.10008.6.1.818", "Purpose of Reference to Predecessor Report (7009)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Fixation Quality During Acquisition (4220)</summary>
        public static readonly DicomUID VisualFixationQualityDuringAcquisition4220 = new DicomUID("1.2.840.10008.6.1.819", "Visual Fixation Quality During Acquisition (4220)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Fixation Quality Problem (4221)</summary>
        public static readonly DicomUID VisualFixationQualityProblem4221 = new DicomUID("1.2.840.10008.6.1.820", "Visual Fixation Quality Problem (4221)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Macular Grid Problem (4222)</summary>
        public static readonly DicomUID OphthalmicMacularGridProblem4222 = new DicomUID("1.2.840.10008.6.1.821", "Ophthalmic Macular Grid Problem (4222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organization (5002)</summary>
        public static readonly DicomUID Organization5002 = new DicomUID("1.2.840.10008.6.1.822", "Organization (5002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mixed Breed (7486)</summary>
        public static readonly DicomUID MixedBreed7486 = new DicomUID("1.2.840.10008.6.1.823", "Mixed Breed (7486)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Broselow-Luten Pediatric Size Category (7040)</summary>
        public static readonly DicomUID BroselowLutenPediatricSizeCategory7040 = new DicomUID("1.2.840.10008.6.1.824", "Broselow-Luten Pediatric Size Category (7040)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CMDCTECC Calcium Scoring Patient Size Category (7042)</summary>
        public static readonly DicomUID CMDCTECCCalciumScoringPatientSizeCategory7042 = new DicomUID("1.2.840.10008.6.1.825", "CMDCTECC Calcium Scoring Patient Size Category (7042)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Report Title (12245)</summary>
        public static readonly DicomUID CardiacUltrasoundReportTitle12245 = new DicomUID("1.2.840.10008.6.1.826", "Cardiac Ultrasound Report Title (12245)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Indication for Study (12246)</summary>
        public static readonly DicomUID CardiacUltrasoundIndicationForStudy12246 = new DicomUID("1.2.840.10008.6.1.827", "Cardiac Ultrasound Indication for Study (12246)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pediatric, Fetal and Congenital Cardiac Surgical Intervention (12247)</summary>
        public static readonly DicomUID PediatricFetalAndCongenitalCardiacSurgicalIntervention12247 = new DicomUID("1.2.840.10008.6.1.828", "Pediatric, Fetal and Congenital Cardiac Surgical Intervention (12247)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Summary Code (12248)</summary>
        public static readonly DicomUID CardiacUltrasoundSummaryCode12248 = new DicomUID("1.2.840.10008.6.1.829", "Cardiac Ultrasound Summary Code (12248)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Fetal Summary Code (12249)</summary>
        public static readonly DicomUID CardiacUltrasoundFetalSummaryCode12249 = new DicomUID("1.2.840.10008.6.1.830", "Cardiac Ultrasound Fetal Summary Code (12249)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Common Linear Measurement (12250)</summary>
        public static readonly DicomUID CardiacUltrasoundCommonLinearMeasurement12250 = new DicomUID("1.2.840.10008.6.1.831", "Cardiac Ultrasound Common Linear Measurement (12250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Linear Valve Measurement (12251)</summary>
        public static readonly DicomUID CardiacUltrasoundLinearValveMeasurement12251 = new DicomUID("1.2.840.10008.6.1.832", "Cardiac Ultrasound Linear Valve Measurement (12251)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Cardiac Function (12252)</summary>
        public static readonly DicomUID CardiacUltrasoundCardiacFunction12252 = new DicomUID("1.2.840.10008.6.1.833", "Cardiac Ultrasound Cardiac Function (12252)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Area Measurement (12253)</summary>
        public static readonly DicomUID CardiacUltrasoundAreaMeasurement12253 = new DicomUID("1.2.840.10008.6.1.834", "Cardiac Ultrasound Area Measurement (12253)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Hemodynamic Measurement (12254)</summary>
        public static readonly DicomUID CardiacUltrasoundHemodynamicMeasurement12254 = new DicomUID("1.2.840.10008.6.1.835", "Cardiac Ultrasound Hemodynamic Measurement (12254)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Myocardium Measurement (12255)</summary>
        public static readonly DicomUID CardiacUltrasoundMyocardiumMeasurement12255 = new DicomUID("1.2.840.10008.6.1.836", "Cardiac Ultrasound Myocardium Measurement (12255)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Left Ventricle Measurement (12257)</summary>
        public static readonly DicomUID CardiacUltrasoundLeftVentricleMeasurement12257 = new DicomUID("1.2.840.10008.6.1.838", "Cardiac Ultrasound Left Ventricle Measurement (12257)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Right Ventricle Measurement (12258)</summary>
        public static readonly DicomUID CardiacUltrasoundRightVentricleMeasurement12258 = new DicomUID("1.2.840.10008.6.1.839", "Cardiac Ultrasound Right Ventricle Measurement (12258)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Ventricles Measurement (12259)</summary>
        public static readonly DicomUID CardiacUltrasoundVentriclesMeasurement12259 = new DicomUID("1.2.840.10008.6.1.840", "Cardiac Ultrasound Ventricles Measurement (12259)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Artery Measurement (12260)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryArteryMeasurement12260 = new DicomUID("1.2.840.10008.6.1.841", "Cardiac Ultrasound Pulmonary Artery Measurement (12260)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Vein (12261)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryVein12261 = new DicomUID("1.2.840.10008.6.1.842", "Cardiac Ultrasound Pulmonary Vein (12261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Valve Measurement (12262)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryValveMeasurement12262 = new DicomUID("1.2.840.10008.6.1.843", "Cardiac Ultrasound Pulmonary Valve Measurement (12262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Pulmonary Measurement (12263)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnPulmonaryMeasurement12263 = new DicomUID("1.2.840.10008.6.1.844", "Cardiac Ultrasound Venous Return Pulmonary Measurement (12263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Systemic Measurement (12264)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnSystemicMeasurement12264 = new DicomUID("1.2.840.10008.6.1.845", "Cardiac Ultrasound Venous Return Systemic Measurement (12264)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atria and Atrial Septum Measurement (12265)</summary>
        public static readonly DicomUID CardiacUltrasoundAtriaAndAtrialSeptumMeasurement12265 = new DicomUID("1.2.840.10008.6.1.846", "Cardiac Ultrasound Atria and Atrial Septum Measurement (12265)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Mitral Valve Measurement (12266)</summary>
        public static readonly DicomUID CardiacUltrasoundMitralValveMeasurement12266 = new DicomUID("1.2.840.10008.6.1.847", "Cardiac Ultrasound Mitral Valve Measurement (12266)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Tricuspid Valve Measurement (12267)</summary>
        public static readonly DicomUID CardiacUltrasoundTricuspidValveMeasurement12267 = new DicomUID("1.2.840.10008.6.1.848", "Cardiac Ultrasound Tricuspid Valve Measurement (12267)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atrioventricular Valve Measurement (12268)</summary>
        public static readonly DicomUID CardiacUltrasoundAtrioventricularValveMeasurement12268 = new DicomUID("1.2.840.10008.6.1.849", "Cardiac Ultrasound Atrioventricular Valve Measurement (12268)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Interventricular Septum Measurement (12269)</summary>
        public static readonly DicomUID CardiacUltrasoundInterventricularSeptumMeasurement12269 = new DicomUID("1.2.840.10008.6.1.850", "Cardiac Ultrasound Interventricular Septum Measurement (12269)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aortic Valve Measurement (12270)</summary>
        public static readonly DicomUID CardiacUltrasoundAorticValveMeasurement12270 = new DicomUID("1.2.840.10008.6.1.851", "Cardiac Ultrasound Aortic Valve Measurement (12270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Outflow Tract Measurement (12271)</summary>
        public static readonly DicomUID CardiacUltrasoundOutflowTractMeasurement12271 = new DicomUID("1.2.840.10008.6.1.852", "Cardiac Ultrasound Outflow Tract Measurement (12271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Semilunar Valve, Annulate and Sinus Measurement (12272)</summary>
        public static readonly DicomUID CardiacUltrasoundSemilunarValveAnnulateAndSinusMeasurement12272 = new DicomUID("1.2.840.10008.6.1.853", "Cardiac Ultrasound Semilunar Valve, Annulate and Sinus Measurement (12272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aortic Sinotubular Junction Measurement (12273)</summary>
        public static readonly DicomUID CardiacUltrasoundAorticSinotubularJunctionMeasurement12273 = new DicomUID("1.2.840.10008.6.1.854", "Cardiac Ultrasound Aortic Sinotubular Junction Measurement (12273)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aorta Measurement (12274)</summary>
        public static readonly DicomUID CardiacUltrasoundAortaMeasurement12274 = new DicomUID("1.2.840.10008.6.1.855", "Cardiac Ultrasound Aorta Measurement (12274)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Coronary Artery Measurement (12275)</summary>
        public static readonly DicomUID CardiacUltrasoundCoronaryArteryMeasurement12275 = new DicomUID("1.2.840.10008.6.1.856", "Cardiac Ultrasound Coronary Artery Measurement (12275)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aorto Pulmonary Connection Measurement (12276)</summary>
        public static readonly DicomUID CardiacUltrasoundAortoPulmonaryConnectionMeasurement12276 = new DicomUID("1.2.840.10008.6.1.857", "Cardiac Ultrasound Aorto Pulmonary Connection Measurement (12276)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pericardium and Pleura Measurement (12277)</summary>
        public static readonly DicomUID CardiacUltrasoundPericardiumAndPleuraMeasurement12277 = new DicomUID("1.2.840.10008.6.1.858", "Cardiac Ultrasound Pericardium and Pleura Measurement (12277)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Fetal General Measurement (12279)</summary>
        public static readonly DicomUID CardiacUltrasoundFetalGeneralMeasurement12279 = new DicomUID("1.2.840.10008.6.1.859", "Cardiac Ultrasound Fetal General Measurement (12279)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Target Site (12280)</summary>
        public static readonly DicomUID CardiacUltrasoundTargetSite12280 = new DicomUID("1.2.840.10008.6.1.860", "Cardiac Ultrasound Target Site (12280)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Target Site Modifier (12281)</summary>
        public static readonly DicomUID CardiacUltrasoundTargetSiteModifier12281 = new DicomUID("1.2.840.10008.6.1.861", "Cardiac Ultrasound Target Site Modifier (12281)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Systemic Finding Site (12282)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnSystemicFindingSite12282 = new DicomUID("1.2.840.10008.6.1.862", "Cardiac Ultrasound Venous Return Systemic Finding Site (12282)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Venous Return Pulmonary Finding Site (12283)</summary>
        public static readonly DicomUID CardiacUltrasoundVenousReturnPulmonaryFindingSite12283 = new DicomUID("1.2.840.10008.6.1.863", "Cardiac Ultrasound Venous Return Pulmonary Finding Site (12283)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atria and Atrial Septum Finding Site (12284)</summary>
        public static readonly DicomUID CardiacUltrasoundAtriaAndAtrialSeptumFindingSite12284 = new DicomUID("1.2.840.10008.6.1.864", "Cardiac Ultrasound Atria and Atrial Septum Finding Site (12284)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Atrioventricular Valve Finding Site (12285)</summary>
        public static readonly DicomUID CardiacUltrasoundAtrioventricularValveFindingSite12285 = new DicomUID("1.2.840.10008.6.1.865", "Cardiac Ultrasound Atrioventricular Valve Finding Site (12285)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Interventricular Septum Finding Site (12286)</summary>
        public static readonly DicomUID CardiacUltrasoundInterventricularSeptumFindingSite12286 = new DicomUID("1.2.840.10008.6.1.866", "Cardiac Ultrasound Interventricular Septum Finding Site (12286)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Ventricle Finding Site (12287)</summary>
        public static readonly DicomUID CardiacUltrasoundVentricleFindingSite12287 = new DicomUID("1.2.840.10008.6.1.867", "Cardiac Ultrasound Ventricle Finding Site (12287)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Outflow Tract Finding Site (12288)</summary>
        public static readonly DicomUID CardiacUltrasoundOutflowTractFindingSite12288 = new DicomUID("1.2.840.10008.6.1.868", "Cardiac Ultrasound Outflow Tract Finding Site (12288)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Semilunar Valve, Annulus and Sinus Finding Site (12289)</summary>
        public static readonly DicomUID CardiacUltrasoundSemilunarValveAnnulusAndSinusFindingSite12289 = new DicomUID("1.2.840.10008.6.1.869", "Cardiac Ultrasound Semilunar Valve, Annulus and Sinus Finding Site (12289)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pulmonary Artery Finding Site (12290)</summary>
        public static readonly DicomUID CardiacUltrasoundPulmonaryArteryFindingSite12290 = new DicomUID("1.2.840.10008.6.1.870", "Cardiac Ultrasound Pulmonary Artery Finding Site (12290)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aorta Finding Site (12291)</summary>
        public static readonly DicomUID CardiacUltrasoundAortaFindingSite12291 = new DicomUID("1.2.840.10008.6.1.871", "Cardiac Ultrasound Aorta Finding Site (12291)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Coronary Artery Finding Site (12292)</summary>
        public static readonly DicomUID CardiacUltrasoundCoronaryArteryFindingSite12292 = new DicomUID("1.2.840.10008.6.1.872", "Cardiac Ultrasound Coronary Artery Finding Site (12292)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Aortopulmonary Connection Finding Site (12293)</summary>
        public static readonly DicomUID CardiacUltrasoundAortopulmonaryConnectionFindingSite12293 = new DicomUID("1.2.840.10008.6.1.873", "Cardiac Ultrasound Aortopulmonary Connection Finding Site (12293)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Ultrasound Pericardium and Pleura Finding Site (12294)</summary>
        public static readonly DicomUID CardiacUltrasoundPericardiumAndPleuraFindingSite12294 = new DicomUID("1.2.840.10008.6.1.874", "Cardiac Ultrasound Pericardium and Pleura Finding Site (12294)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Ultrasound Axial Measurements Type (4230)</summary>
        public static readonly DicomUID OphthalmicUltrasoundAxialMeasurementsType4230 = new DicomUID("1.2.840.10008.6.1.876", "Ophthalmic Ultrasound Axial Measurements Type (4230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lens Status (4231)</summary>
        public static readonly DicomUID LensStatus4231 = new DicomUID("1.2.840.10008.6.1.877", "Lens Status (4231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vitreous Status (4232)</summary>
        public static readonly DicomUID VitreousStatus4232 = new DicomUID("1.2.840.10008.6.1.878", "Vitreous Status (4232)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Axial Length Measurements Segment Name (4233)</summary>
        public static readonly DicomUID OphthalmicAxialLengthMeasurementsSegmentName4233 = new DicomUID("1.2.840.10008.6.1.879", "Ophthalmic Axial Length Measurements Segment Name (4233)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Refractive Surgery Type (4234)</summary>
        public static readonly DicomUID RefractiveSurgeryType4234 = new DicomUID("1.2.840.10008.6.1.880", "Refractive Surgery Type (4234)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Keratometry Descriptor (4235)</summary>
        public static readonly DicomUID KeratometryDescriptor4235 = new DicomUID("1.2.840.10008.6.1.881", "Keratometry Descriptor (4235)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IOL Calculation Formula (4236)</summary>
        public static readonly DicomUID IOLCalculationFormula4236 = new DicomUID("1.2.840.10008.6.1.882", "IOL Calculation Formula (4236)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lens Constant Type (4237)</summary>
        public static readonly DicomUID LensConstantType4237 = new DicomUID("1.2.840.10008.6.1.883", "Lens Constant Type (4237)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Refractive Error Type (4238)</summary>
        public static readonly DicomUID RefractiveErrorType4238 = new DicomUID("1.2.840.10008.6.1.884", "Refractive Error Type (4238)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anterior Chamber Depth Definition (4239)</summary>
        public static readonly DicomUID AnteriorChamberDepthDefinition4239 = new DicomUID("1.2.840.10008.6.1.885", "Anterior Chamber Depth Definition (4239)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Measurement or Calculation Data Source (4240)</summary>
        public static readonly DicomUID OphthalmicMeasurementOrCalculationDataSource4240 = new DicomUID("1.2.840.10008.6.1.886", "Ophthalmic Measurement or Calculation Data Source (4240)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Axial Length Selection Method (4241)</summary>
        public static readonly DicomUID OphthalmicAxialLengthSelectionMethod4241 = new DicomUID("1.2.840.10008.6.1.887", "Ophthalmic Axial Length Selection Method (4241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Quality Metric Type (4243)</summary>
        public static readonly DicomUID OphthalmicQualityMetricType4243 = new DicomUID("1.2.840.10008.6.1.889", "Ophthalmic Quality Metric Type (4243)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Agent Concentration Unit (4244)</summary>
        public static readonly DicomUID OphthalmicAgentConcentrationUnit4244 = new DicomUID("1.2.840.10008.6.1.890", "Ophthalmic Agent Concentration Unit (4244)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Functional Condition Present During Acquisition (91)</summary>
        public static readonly DicomUID FunctionalConditionPresentDuringAcquisition91 = new DicomUID("1.2.840.10008.6.1.891", "Functional Condition Present During Acquisition (91)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Joint Position During Acquisition (92)</summary>
        public static readonly DicomUID JointPositionDuringAcquisition92 = new DicomUID("1.2.840.10008.6.1.892", "Joint Position During Acquisition (92)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Joint Positioning Method (93)</summary>
        public static readonly DicomUID JointPositioningMethod93 = new DicomUID("1.2.840.10008.6.1.893", "Joint Positioning Method (93)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physical Force Applied During Acquisition (94)</summary>
        public static readonly DicomUID PhysicalForceAppliedDuringAcquisition94 = new DicomUID("1.2.840.10008.6.1.894", "Physical Force Applied During Acquisition (94)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Control Numeric Variable (3690)</summary>
        public static readonly DicomUID ECGControlNumericVariable3690 = new DicomUID("1.2.840.10008.6.1.895", "ECG Control Numeric Variable (3690)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Control Text Variable (3691)</summary>
        public static readonly DicomUID ECGControlTextVariable3691 = new DicomUID("1.2.840.10008.6.1.896", "ECG Control Text Variable (3691)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Whole Slide Microscopy Image Referenced Image Purpose of Reference (8120)</summary>
        public static readonly DicomUID WholeSlideMicroscopyImageReferencedImagePurposeOfReference8120 = new DicomUID("1.2.840.10008.6.1.897", "Whole Slide Microscopy Image Referenced Image Purpose of Reference (8120)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Visual Field Static Perimetry Test Pattern (4250)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryTestPattern4250 = new DicomUID("1.2.840.10008.6.1.909", "Visual Field Static Perimetry Test Pattern (4250)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Test Strategy (4251)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryTestStrategy4251 = new DicomUID("1.2.840.10008.6.1.910", "Visual Field Static Perimetry Test Strategy (4251)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Screening Test Mode (4252)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryScreeningTestMode4252 = new DicomUID("1.2.840.10008.6.1.911", "Visual Field Static Perimetry Screening Test Mode (4252)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Fixation Strategy (4253)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryFixationStrategy4253 = new DicomUID("1.2.840.10008.6.1.912", "Visual Field Static Perimetry Fixation Strategy (4253)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Static Perimetry Test Analysis Result (4254)</summary>
        public static readonly DicomUID VisualFieldStaticPerimetryTestAnalysisResult4254 = new DicomUID("1.2.840.10008.6.1.913", "Visual Field Static Perimetry Test Analysis Result (4254)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Illumination Color (4255)</summary>
        public static readonly DicomUID VisualFieldIlluminationColor4255 = new DicomUID("1.2.840.10008.6.1.914", "Visual Field Illumination Color (4255)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Procedure Modifier (4256)</summary>
        public static readonly DicomUID VisualFieldProcedureModifier4256 = new DicomUID("1.2.840.10008.6.1.915", "Visual Field Procedure Modifier (4256)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Field Global Index Name (4257)</summary>
        public static readonly DicomUID VisualFieldGlobalIndexName4257 = new DicomUID("1.2.840.10008.6.1.916", "Visual Field Global Index Name (4257)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Component Semantic (7180)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelComponentSemantic7180 = new DicomUID("1.2.840.10008.6.1.917", "Abstract Multi-dimensional Image Model Component Semantic (7180)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Component Unit (7181)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelComponentUnit7181 = new DicomUID("1.2.840.10008.6.1.918", "Abstract Multi-dimensional Image Model Component Unit (7181)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Dimension Semantic (7182)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelDimensionSemantic7182 = new DicomUID("1.2.840.10008.6.1.919", "Abstract Multi-dimensional Image Model Dimension Semantic (7182)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Dimension Unit (7183)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelDimensionUnit7183 = new DicomUID("1.2.840.10008.6.1.920", "Abstract Multi-dimensional Image Model Dimension Unit (7183)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Axis Direction (7184)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelAxisDirection7184 = new DicomUID("1.2.840.10008.6.1.921", "Abstract Multi-dimensional Image Model Axis Direction (7184)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Axis Orientation (7185)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelAxisOrientation7185 = new DicomUID("1.2.840.10008.6.1.922", "Abstract Multi-dimensional Image Model Axis Orientation (7185)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Multi-dimensional Image Model Qualitative Dimension Sample Semantic (7186)</summary>
        public static readonly DicomUID AbstractMultiDimensionalImageModelQualitativeDimensionSampleSemantic7186 = new DicomUID("1.2.840.10008.6.1.923", "Abstract Multi-dimensional Image Model Qualitative Dimension Sample Semantic (7186)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Planning Method (7320)</summary>
        public static readonly DicomUID PlanningMethod7320 = new DicomUID("1.2.840.10008.6.1.924", "Planning Method (7320)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: De-identification Method (7050)</summary>
        public static readonly DicomUID DeIdentificationMethod7050 = new DicomUID("1.2.840.10008.6.1.925", "De-identification Method (7050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Orientation (12118)</summary>
        public static readonly DicomUID MeasurementOrientation12118 = new DicomUID("1.2.840.10008.6.1.926", "Measurement Orientation (12118)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ECG Global Waveform Duration (3689)</summary>
        public static readonly DicomUID ECGGlobalWaveformDuration3689 = new DicomUID("1.2.840.10008.6.1.927", "ECG Global Waveform Duration (3689)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ICD (3692)</summary>
        public static readonly DicomUID ICD3692 = new DicomUID("1.2.840.10008.6.1.930", "ICD (3692)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy General Workitem Definition (9241)</summary>
        public static readonly DicomUID RadiotherapyGeneralWorkitemDefinition9241 = new DicomUID("1.2.840.10008.6.1.931", "Radiotherapy General Workitem Definition (9241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Acquisition Workitem Definition (9242)</summary>
        public static readonly DicomUID RadiotherapyAcquisitionWorkitemDefinition9242 = new DicomUID("1.2.840.10008.6.1.932", "Radiotherapy Acquisition Workitem Definition (9242)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Registration Workitem Definition (9243)</summary>
        public static readonly DicomUID RadiotherapyRegistrationWorkitemDefinition9243 = new DicomUID("1.2.840.10008.6.1.933", "Radiotherapy Registration Workitem Definition (9243)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contrast Bolus Substance (3850)</summary>
        public static readonly DicomUID ContrastBolusSubstance3850 = new DicomUID("1.2.840.10008.6.1.934", "Contrast Bolus Substance (3850)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Label Type (10022)</summary>
        public static readonly DicomUID LabelType10022 = new DicomUID("1.2.840.10008.6.1.935", "Label Type (10022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Mapping Unit for Real World Value Mapping (4260)</summary>
        public static readonly DicomUID OphthalmicMappingUnitForRealWorldValueMapping4260 = new DicomUID("1.2.840.10008.6.1.936", "Ophthalmic Mapping Unit for Real World Value Mapping (4260)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Mapping Acquisition Method (4261)</summary>
        public static readonly DicomUID OphthalmicMappingAcquisitionMethod4261 = new DicomUID("1.2.840.10008.6.1.937", "Ophthalmic Mapping Acquisition Method (4261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Retinal Thickness Definition (4262)</summary>
        public static readonly DicomUID RetinalThicknessDefinition4262 = new DicomUID("1.2.840.10008.6.1.938", "Retinal Thickness Definition (4262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Thickness Map Value Type (4263)</summary>
        public static readonly DicomUID OphthalmicThicknessMapValueType4263 = new DicomUID("1.2.840.10008.6.1.939", "Ophthalmic Thickness Map Value Type (4263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Map Purpose of Reference (4264)</summary>
        public static readonly DicomUID OphthalmicMapPurposeOfReference4264 = new DicomUID("1.2.840.10008.6.1.940", "Ophthalmic Map Purpose of Reference (4264)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Thickness Deviation Category (4265)</summary>
        public static readonly DicomUID OphthalmicThicknessDeviationCategory4265 = new DicomUID("1.2.840.10008.6.1.941", "Ophthalmic Thickness Deviation Category (4265)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ophthalmic Anatomic Structure Reference Point (4266)</summary>
        public static readonly DicomUID OphthalmicAnatomicStructureReferencePoint4266 = new DicomUID("1.2.840.10008.6.1.942", "Ophthalmic Anatomic Structure Reference Point (4266)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Synchronization Technique (3104)</summary>
        public static readonly DicomUID CardiacSynchronizationTechnique3104 = new DicomUID("1.2.840.10008.6.1.943", "Cardiac Synchronization Technique (3104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Staining Protocol (8130)</summary>
        public static readonly DicomUID StainingProtocol8130 = new DicomUID("1.2.840.10008.6.1.944", "Staining Protocol (8130)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Size Specific Dose Estimation Method for CT (10023)</summary>
        public static readonly DicomUID SizeSpecificDoseEstimationMethodForCT10023 = new DicomUID("1.2.840.10008.6.1.947", "Size Specific Dose Estimation Method for CT (10023)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pathology Imaging Protocol (8131)</summary>
        public static readonly DicomUID PathologyImagingProtocol8131 = new DicomUID("1.2.840.10008.6.1.948", "Pathology Imaging Protocol (8131)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Magnification Selection (8132)</summary>
        public static readonly DicomUID MagnificationSelection8132 = new DicomUID("1.2.840.10008.6.1.949", "Magnification Selection (8132)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tissue Selection (8133)</summary>
        public static readonly DicomUID TissueSelection8133 = new DicomUID("1.2.840.10008.6.1.950", "Tissue Selection (8133)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Region of Interest Measurement Modifier (7464)</summary>
        public static readonly DicomUID GeneralRegionOfInterestMeasurementModifier7464 = new DicomUID("1.2.840.10008.6.1.951", "General Region of Interest Measurement Modifier (7464)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Derived From Multiple ROI Measurements (7465)</summary>
        public static readonly DicomUID MeasurementDerivedFromMultipleROIMeasurements7465 = new DicomUID("1.2.840.10008.6.1.952", "Measurement Derived From Multiple ROI Measurements (7465)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Scan Acquisition Type (8201)</summary>
        public static readonly DicomUID SurfaceScanAcquisitionType8201 = new DicomUID("1.2.840.10008.6.1.953", "Surface Scan Acquisition Type (8201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Scan Mode Type (8202)</summary>
        public static readonly DicomUID SurfaceScanModeType8202 = new DicomUID("1.2.840.10008.6.1.954", "Surface Scan Mode Type (8202)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surface Scan Registration Method Type (8203)</summary>
        public static readonly DicomUID SurfaceScanRegistrationMethodType8203 = new DicomUID("1.2.840.10008.6.1.956", "Surface Scan Registration Method Type (8203)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Basic Cardiac View (27)</summary>
        public static readonly DicomUID BasicCardiacView27 = new DicomUID("1.2.840.10008.6.1.957", "Basic Cardiac View (27)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CT Reconstruction Algorithm (10033)</summary>
        public static readonly DicomUID CTReconstructionAlgorithm10033 = new DicomUID("1.2.840.10008.6.1.958", "CT Reconstruction Algorithm (10033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Detector Type (10030)</summary>
        public static readonly DicomUID DetectorType10030 = new DicomUID("1.2.840.10008.6.1.959", "Detector Type (10030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CR/DR Mechanical Configuration (10031)</summary>
        public static readonly DicomUID CRDRMechanicalConfiguration10031 = new DicomUID("1.2.840.10008.6.1.960", "CR/DR Mechanical Configuration (10031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Projection X-Ray Acquisition Device Type (10032)</summary>
        public static readonly DicomUID ProjectionXRayAcquisitionDeviceType10032 = new DicomUID("1.2.840.10008.6.1.961", "Projection X-Ray Acquisition Device Type (10032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abstract Segmentation Type (7165)</summary>
        public static readonly DicomUID AbstractSegmentationType7165 = new DicomUID("1.2.840.10008.6.1.962", "Abstract Segmentation Type (7165)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Common Tissue Segmentation Type (7166)</summary>
        public static readonly DicomUID CommonTissueSegmentationType7166 = new DicomUID("1.2.840.10008.6.1.963", "Common Tissue Segmentation Type (7166)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Peripheral Nervous System Segmentation Type (7167)</summary>
        public static readonly DicomUID PeripheralNervousSystemSegmentationType7167 = new DicomUID("1.2.840.10008.6.1.964", "Peripheral Nervous System Segmentation Type (7167)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Corneal Topography Mapping Unit for Real World Value Mapping (4267)</summary>
        public static readonly DicomUID CornealTopographyMappingUnitForRealWorldValueMapping4267 = new DicomUID("1.2.840.10008.6.1.965", "Corneal Topography Mapping Unit for Real World Value Mapping (4267)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Corneal Topography Map Value Type (4268)</summary>
        public static readonly DicomUID CornealTopographyMapValueType4268 = new DicomUID("1.2.840.10008.6.1.966", "Corneal Topography Map Value Type (4268)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Structure for Volumetric Measurement (7140)</summary>
        public static readonly DicomUID BrainStructureForVolumetricMeasurement7140 = new DicomUID("1.2.840.10008.6.1.967", "Brain Structure for Volumetric Measurement (7140)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Intravenous Extravasation Symptom (10043)</summary>
        public static readonly DicomUID IntravenousExtravasationSymptom10043 = new DicomUID("1.2.840.10008.6.1.975", "Intravenous Extravasation Symptom (10043)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiosensitive Organ (10044)</summary>
        public static readonly DicomUID RadiosensitiveOrgan10044 = new DicomUID("1.2.840.10008.6.1.976", "Radiosensitive Organ (10044)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiopharmaceutical Patient State (10045)</summary>
        public static readonly DicomUID RadiopharmaceuticalPatientState10045 = new DicomUID("1.2.840.10008.6.1.977", "Radiopharmaceutical Patient State (10045)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: GFR Measurement (10046)</summary>
        public static readonly DicomUID GFRMeasurement10046 = new DicomUID("1.2.840.10008.6.1.978", "GFR Measurement (10046)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: GFR Measurement Method (10047)</summary>
        public static readonly DicomUID GFRMeasurementMethod10047 = new DicomUID("1.2.840.10008.6.1.979", "GFR Measurement Method (10047)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Evaluation Method (8300)</summary>
        public static readonly DicomUID VisualEvaluationMethod8300 = new DicomUID("1.2.840.10008.6.1.980", "Visual Evaluation Method (8300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Test Pattern Code (8301)</summary>
        public static readonly DicomUID TestPatternCode8301 = new DicomUID("1.2.840.10008.6.1.981", "Test Pattern Code (8301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Pattern Code (8302)</summary>
        public static readonly DicomUID MeasurementPatternCode8302 = new DicomUID("1.2.840.10008.6.1.982", "Measurement Pattern Code (8302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Display Device Type (8303)</summary>
        public static readonly DicomUID DisplayDeviceType8303 = new DicomUID("1.2.840.10008.6.1.983", "Display Device Type (8303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: SUV Unit (85)</summary>
        public static readonly DicomUID SUVUnit85 = new DicomUID("1.2.840.10008.6.1.984", "SUV Unit (85)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: T1 Measurement Method (4100)</summary>
        public static readonly DicomUID T1MeasurementMethod4100 = new DicomUID("1.2.840.10008.6.1.985", "T1 Measurement Method (4100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tracer Kinetic Model (4101)</summary>
        public static readonly DicomUID TracerKineticModel4101 = new DicomUID("1.2.840.10008.6.1.986", "Tracer Kinetic Model (4101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Measurement Method (4102)</summary>
        public static readonly DicomUID PerfusionMeasurementMethod4102 = new DicomUID("1.2.840.10008.6.1.987", "Perfusion Measurement Method (4102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Arterial Input Function Measurement Method (4103)</summary>
        public static readonly DicomUID ArterialInputFunctionMeasurementMethod4103 = new DicomUID("1.2.840.10008.6.1.988", "Arterial Input Function Measurement Method (4103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Bolus Arrival Time Derivation Method (4104)</summary>
        public static readonly DicomUID BolusArrivalTimeDerivationMethod4104 = new DicomUID("1.2.840.10008.6.1.989", "Bolus Arrival Time Derivation Method (4104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Analysis Method (4105)</summary>
        public static readonly DicomUID PerfusionAnalysisMethod4105 = new DicomUID("1.2.840.10008.6.1.990", "Perfusion Analysis Method (4105)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quantitative Method Used for Perfusion and Tracer Kinetic Model (4106)</summary>
        public static readonly DicomUID QuantitativeMethodUsedForPerfusionAndTracerKineticModel4106 = new DicomUID("1.2.840.10008.6.1.991", "Quantitative Method Used for Perfusion and Tracer Kinetic Model (4106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tracer Kinetic Model Parameter (4107)</summary>
        public static readonly DicomUID TracerKineticModelParameter4107 = new DicomUID("1.2.840.10008.6.1.992", "Tracer Kinetic Model Parameter (4107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Perfusion Model Parameter (4108)</summary>
        public static readonly DicomUID PerfusionModelParameter4108 = new DicomUID("1.2.840.10008.6.1.993", "Perfusion Model Parameter (4108)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model-Independent Dynamic Contrast Analysis Parameter (4109)</summary>
        public static readonly DicomUID ModelIndependentDynamicContrastAnalysisParameter4109 = new DicomUID("1.2.840.10008.6.1.994", "Model-Independent Dynamic Contrast Analysis Parameter (4109)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tracer Kinetic Modeling Covariate (4110)</summary>
        public static readonly DicomUID TracerKineticModelingCovariate4110 = new DicomUID("1.2.840.10008.6.1.995", "Tracer Kinetic Modeling Covariate (4110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contrast Characteristic (4111)</summary>
        public static readonly DicomUID ContrastCharacteristic4111 = new DicomUID("1.2.840.10008.6.1.996", "Contrast Characteristic (4111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Report Document Title (7021)</summary>
        public static readonly DicomUID MeasurementReportDocumentTitle7021 = new DicomUID("1.2.840.10008.6.1.997", "Measurement Report Document Title (7021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quantitative Diagnostic Imaging Procedure (100)</summary>
        public static readonly DicomUID QuantitativeDiagnosticImagingProcedure100 = new DicomUID("1.2.840.10008.6.1.998", "Quantitative Diagnostic Imaging Procedure (100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Region of Interest Measurement (7466)</summary>
        public static readonly DicomUID PETRegionOfInterestMeasurement7466 = new DicomUID("1.2.840.10008.6.1.999", "PET Region of Interest Measurement (7466)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gray Level Co-occurrence Matrix Measurement (7467)</summary>
        public static readonly DicomUID GrayLevelCoOccurrenceMatrixMeasurement7467 = new DicomUID("1.2.840.10008.6.1.1000", "Gray Level Co-occurrence Matrix Measurement (7467)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Texture Measurement (7468)</summary>
        public static readonly DicomUID TextureMeasurement7468 = new DicomUID("1.2.840.10008.6.1.1001", "Texture Measurement (7468)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time Point Type (6146)</summary>
        public static readonly DicomUID TimePointType6146 = new DicomUID("1.2.840.10008.6.1.1002", "Time Point Type (6146)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Generic Intensity and Size Measurement (7469)</summary>
        public static readonly DicomUID GenericIntensityAndSizeMeasurement7469 = new DicomUID("1.2.840.10008.6.1.1003", "Generic Intensity and Size Measurement (7469)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Response Criteria (6147)</summary>
        public static readonly DicomUID ResponseCriteria6147 = new DicomUID("1.2.840.10008.6.1.1004", "Response Criteria (6147)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Biometry Anatomic Site (12020)</summary>
        public static readonly DicomUID FetalBiometryAnatomicSite12020 = new DicomUID("1.2.840.10008.6.1.1005", "Fetal Biometry Anatomic Site (12020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Long Bone Anatomic Site (12021)</summary>
        public static readonly DicomUID FetalLongBoneAnatomicSite12021 = new DicomUID("1.2.840.10008.6.1.1006", "Fetal Long Bone Anatomic Site (12021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fetal Cranium Anatomic Site (12022)</summary>
        public static readonly DicomUID FetalCraniumAnatomicSite12022 = new DicomUID("1.2.840.10008.6.1.1007", "Fetal Cranium Anatomic Site (12022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pelvis and Uterus Anatomic Site (12023)</summary>
        public static readonly DicomUID PelvisAndUterusAnatomicSite12023 = new DicomUID("1.2.840.10008.6.1.1008", "Pelvis and Uterus Anatomic Site (12023)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Parametric Map Derivation Image Purpose of Reference (7222)</summary>
        public static readonly DicomUID ParametricMapDerivationImagePurposeOfReference7222 = new DicomUID("1.2.840.10008.6.1.1009", "Parametric Map Derivation Image Purpose of Reference (7222)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physical Quantity Descriptor (9000)</summary>
        public static readonly DicomUID PhysicalQuantityDescriptor9000 = new DicomUID("1.2.840.10008.6.1.1010", "Physical Quantity Descriptor (9000)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lymph Node Anatomic Site (7600)</summary>
        public static readonly DicomUID LymphNodeAnatomicSite7600 = new DicomUID("1.2.840.10008.6.1.1011", "Lymph Node Anatomic Site (7600)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Head and Neck Cancer Anatomic Site (7601)</summary>
        public static readonly DicomUID HeadAndNeckCancerAnatomicSite7601 = new DicomUID("1.2.840.10008.6.1.1012", "Head and Neck Cancer Anatomic Site (7601)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiber Tract In Brainstem (7701)</summary>
        public static readonly DicomUID FiberTractInBrainstem7701 = new DicomUID("1.2.840.10008.6.1.1013", "Fiber Tract In Brainstem (7701)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Projection and Thalamic Fiber (7702)</summary>
        public static readonly DicomUID ProjectionAndThalamicFiber7702 = new DicomUID("1.2.840.10008.6.1.1014", "Projection and Thalamic Fiber (7702)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Association Fiber (7703)</summary>
        public static readonly DicomUID AssociationFiber7703 = new DicomUID("1.2.840.10008.6.1.1015", "Association Fiber (7703)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Limbic System Tract (7704)</summary>
        public static readonly DicomUID LimbicSystemTract7704 = new DicomUID("1.2.840.10008.6.1.1016", "Limbic System Tract (7704)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Commissural Fiber (7705)</summary>
        public static readonly DicomUID CommissuralFiber7705 = new DicomUID("1.2.840.10008.6.1.1017", "Commissural Fiber (7705)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cranial Nerve (7706)</summary>
        public static readonly DicomUID CranialNerve7706 = new DicomUID("1.2.840.10008.6.1.1018", "Cranial Nerve (7706)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Spinal Cord Fiber (7707)</summary>
        public static readonly DicomUID SpinalCordFiber7707 = new DicomUID("1.2.840.10008.6.1.1019", "Spinal Cord Fiber (7707)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tractography Anatomic Site (7710)</summary>
        public static readonly DicomUID TractographyAnatomicSite7710 = new DicomUID("1.2.840.10008.6.1.1020", "Tractography Anatomic Site (7710)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Primary Anatomic Structure for Intra-oral Radiography (Supernumerary Dentition - Designation of Teeth) (4025)</summary>
        public static readonly DicomUID PrimaryAnatomicStructureForIntraOralRadiographySupernumeraryDentitionDesignationOfTeeth4025 = new DicomUID("1.2.840.10008.6.1.1021", "Primary Anatomic Structure for Intra-oral Radiography (Supernumerary Dentition - Designation of Teeth) (4025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Primary Anatomic Structure for Intra-oral and Craniofacial Radiography - Teeth (4026)</summary>
        public static readonly DicomUID PrimaryAnatomicStructureForIntraOralAndCraniofacialRadiographyTeeth4026 = new DicomUID("1.2.840.10008.6.1.1022", "Primary Anatomic Structure for Intra-oral and Craniofacial Radiography - Teeth (4026)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IEC61217 Device Position Parameter (9401)</summary>
        public static readonly DicomUID IEC61217DevicePositionParameter9401 = new DicomUID("1.2.840.10008.6.1.1023", "IEC61217 Device Position Parameter (9401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IEC61217 Gantry Position Parameter (9402)</summary>
        public static readonly DicomUID IEC61217GantryPositionParameter9402 = new DicomUID("1.2.840.10008.6.1.1024", "IEC61217 Gantry Position Parameter (9402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: IEC61217 Patient Support Position Parameter (9403)</summary>
        public static readonly DicomUID IEC61217PatientSupportPositionParameter9403 = new DicomUID("1.2.840.10008.6.1.1025", "IEC61217 Patient Support Position Parameter (9403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Actionable Finding Classification (7035)</summary>
        public static readonly DicomUID ActionableFindingClassification7035 = new DicomUID("1.2.840.10008.6.1.1026", "Actionable Finding Classification (7035)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Quality Assessment (7036)</summary>
        public static readonly DicomUID ImageQualityAssessment7036 = new DicomUID("1.2.840.10008.6.1.1027", "Image Quality Assessment (7036)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Summary Radiation Exposure Quantity (10050)</summary>
        public static readonly DicomUID SummaryRadiationExposureQuantity10050 = new DicomUID("1.2.840.10008.6.1.1028", "Summary Radiation Exposure Quantity (10050)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Wide Field Ophthalmic Photography Transformation Method (4245)</summary>
        public static readonly DicomUID WideFieldOphthalmicPhotographyTransformationMethod4245 = new DicomUID("1.2.840.10008.6.1.1029", "Wide Field Ophthalmic Photography Transformation Method (4245)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PET Unit (84)</summary>
        public static readonly DicomUID PETUnit84 = new DicomUID("1.2.840.10008.6.1.1030", "PET Unit (84)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Material (7300)</summary>
        public static readonly DicomUID ImplantMaterial7300 = new DicomUID("1.2.840.10008.6.1.1031", "Implant Material (7300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intervention Type (7301)</summary>
        public static readonly DicomUID InterventionType7301 = new DicomUID("1.2.840.10008.6.1.1032", "Intervention Type (7301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Template View Orientation (7302)</summary>
        public static readonly DicomUID ImplantTemplateViewOrientation7302 = new DicomUID("1.2.840.10008.6.1.1033", "Implant Template View Orientation (7302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Template Modified View Orientation (7303)</summary>
        public static readonly DicomUID ImplantTemplateModifiedViewOrientation7303 = new DicomUID("1.2.840.10008.6.1.1034", "Implant Template Modified View Orientation (7303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Target Anatomy (7304)</summary>
        public static readonly DicomUID ImplantTargetAnatomy7304 = new DicomUID("1.2.840.10008.6.1.1035", "Implant Target Anatomy (7304)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Planning Landmark (7305)</summary>
        public static readonly DicomUID ImplantPlanningLandmark7305 = new DicomUID("1.2.840.10008.6.1.1036", "Implant Planning Landmark (7305)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Human Hip Implant Planning Landmark (7306)</summary>
        public static readonly DicomUID HumanHipImplantPlanningLandmark7306 = new DicomUID("1.2.840.10008.6.1.1037", "Human Hip Implant Planning Landmark (7306)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Component Type (7307)</summary>
        public static readonly DicomUID ImplantComponentType7307 = new DicomUID("1.2.840.10008.6.1.1038", "Implant Component Type (7307)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Human Hip Implant Component Type (7308)</summary>
        public static readonly DicomUID HumanHipImplantComponentType7308 = new DicomUID("1.2.840.10008.6.1.1039", "Human Hip Implant Component Type (7308)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Human Trauma Implant Component Type (7309)</summary>
        public static readonly DicomUID HumanTraumaImplantComponentType7309 = new DicomUID("1.2.840.10008.6.1.1040", "Human Trauma Implant Component Type (7309)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Implant Fixation Method (7310)</summary>
        public static readonly DicomUID ImplantFixationMethod7310 = new DicomUID("1.2.840.10008.6.1.1041", "Implant Fixation Method (7310)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Participating Role (7445)</summary>
        public static readonly DicomUID DeviceParticipatingRole7445 = new DicomUID("1.2.840.10008.6.1.1042", "Device Participating Role (7445)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Container Type (8101)</summary>
        public static readonly DicomUID ContainerType8101 = new DicomUID("1.2.840.10008.6.1.1043", "Container Type (8101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Container Component Type (8102)</summary>
        public static readonly DicomUID ContainerComponentType8102 = new DicomUID("1.2.840.10008.6.1.1044", "Container Component Type (8102)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Pathology Specimen Type (8103)</summary>
        public static readonly DicomUID AnatomicPathologySpecimenType8103 = new DicomUID("1.2.840.10008.6.1.1045", "Anatomic Pathology Specimen Type (8103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Breast Tissue Specimen Type (8104)</summary>
        public static readonly DicomUID BreastTissueSpecimenType8104 = new DicomUID("1.2.840.10008.6.1.1046", "Breast Tissue Specimen Type (8104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Collection Procedure (8109)</summary>
        public static readonly DicomUID SpecimenCollectionProcedure8109 = new DicomUID("1.2.840.10008.6.1.1047", "Specimen Collection Procedure (8109)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Sampling Procedure (8110)</summary>
        public static readonly DicomUID SpecimenSamplingProcedure8110 = new DicomUID("1.2.840.10008.6.1.1048", "Specimen Sampling Procedure (8110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Preparation Procedure (8111)</summary>
        public static readonly DicomUID SpecimenPreparationProcedure8111 = new DicomUID("1.2.840.10008.6.1.1049", "Specimen Preparation Procedure (8111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Stain (8112)</summary>
        public static readonly DicomUID SpecimenStain8112 = new DicomUID("1.2.840.10008.6.1.1050", "Specimen Stain (8112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Preparation Step (8113)</summary>
        public static readonly DicomUID SpecimenPreparationStep8113 = new DicomUID("1.2.840.10008.6.1.1051", "Specimen Preparation Step (8113)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Fixative (8114)</summary>
        public static readonly DicomUID SpecimenFixative8114 = new DicomUID("1.2.840.10008.6.1.1052", "Specimen Fixative (8114)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Embedding Media (8115)</summary>
        public static readonly DicomUID SpecimenEmbeddingMedia8115 = new DicomUID("1.2.840.10008.6.1.1053", "Specimen Embedding Media (8115)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source of Projection X-Ray Dose Information (10020)</summary>
        public static readonly DicomUID SourceOfProjectionXRayDoseInformation10020 = new DicomUID("1.2.840.10008.6.1.1054", "Source of Projection X-Ray Dose Information (10020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Source of CT Dose Information (10021)</summary>
        public static readonly DicomUID SourceOfCTDoseInformation10021 = new DicomUID("1.2.840.10008.6.1.1055", "Source of CT Dose Information (10021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Reference Point (10025)</summary>
        public static readonly DicomUID RadiationDoseReferencePoint10025 = new DicomUID("1.2.840.10008.6.1.1056", "Radiation Dose Reference Point (10025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volumetric View Description (501)</summary>
        public static readonly DicomUID VolumetricViewDescription501 = new DicomUID("1.2.840.10008.6.1.1057", "Volumetric View Description (501)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volumetric View Modifier (502)</summary>
        public static readonly DicomUID VolumetricViewModifier502 = new DicomUID("1.2.840.10008.6.1.1058", "Volumetric View Modifier (502)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Acquisition Value Type (7260)</summary>
        public static readonly DicomUID DiffusionAcquisitionValueType7260 = new DicomUID("1.2.840.10008.6.1.1059", "Diffusion Acquisition Value Type (7260)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Model Value Type (7261)</summary>
        public static readonly DicomUID DiffusionModelValueType7261 = new DicomUID("1.2.840.10008.6.1.1060", "Diffusion Model Value Type (7261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Tractography Algorithm Family (7262)</summary>
        public static readonly DicomUID DiffusionTractographyAlgorithmFamily7262 = new DicomUID("1.2.840.10008.6.1.1061", "Diffusion Tractography Algorithm Family (7262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Tractography Measurement Type (7263)</summary>
        public static readonly DicomUID DiffusionTractographyMeasurementType7263 = new DicomUID("1.2.840.10008.6.1.1062", "Diffusion Tractography Measurement Type (7263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Research Animal Source Registry (7490)</summary>
        public static readonly DicomUID ResearchAnimalSourceRegistry7490 = new DicomUID("1.2.840.10008.6.1.1063", "Research Animal Source Registry (7490)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Yes-No Only (231)</summary>
        public static readonly DicomUID YesNoOnly231 = new DicomUID("1.2.840.10008.6.1.1064", "Yes-No Only (231)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Biosafety Level (601)</summary>
        public static readonly DicomUID BiosafetyLevel601 = new DicomUID("1.2.840.10008.6.1.1065", "Biosafety Level (601)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Biosafety Control Reason (602)</summary>
        public static readonly DicomUID BiosafetyControlReason602 = new DicomUID("1.2.840.10008.6.1.1066", "Biosafety Control Reason (602)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sex - Male Female or Both (7457)</summary>
        public static readonly DicomUID SexMaleFemaleOrBoth7457 = new DicomUID("1.2.840.10008.6.1.1067", "Sex - Male Female or Both (7457)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Room Type (603)</summary>
        public static readonly DicomUID AnimalRoomType603 = new DicomUID("1.2.840.10008.6.1.1068", "Animal Room Type (603)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device Reuse (604)</summary>
        public static readonly DicomUID DeviceReuse604 = new DicomUID("1.2.840.10008.6.1.1069", "Device Reuse (604)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Bedding Material (605)</summary>
        public static readonly DicomUID AnimalBeddingMaterial605 = new DicomUID("1.2.840.10008.6.1.1070", "Animal Bedding Material (605)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Shelter Type (606)</summary>
        public static readonly DicomUID AnimalShelterType606 = new DicomUID("1.2.840.10008.6.1.1071", "Animal Shelter Type (606)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Feed Type (607)</summary>
        public static readonly DicomUID AnimalFeedType607 = new DicomUID("1.2.840.10008.6.1.1072", "Animal Feed Type (607)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Feed Source (608)</summary>
        public static readonly DicomUID AnimalFeedSource608 = new DicomUID("1.2.840.10008.6.1.1073", "Animal Feed Source (608)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Animal Feeding Method (609)</summary>
        public static readonly DicomUID AnimalFeedingMethod609 = new DicomUID("1.2.840.10008.6.1.1074", "Animal Feeding Method (609)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Water Type (610)</summary>
        public static readonly DicomUID WaterType610 = new DicomUID("1.2.840.10008.6.1.1075", "Water Type (610)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Category Code Type for Small Animal Anesthesia (611)</summary>
        public static readonly DicomUID AnesthesiaCategoryCodeTypeForSmallAnimalAnesthesia611 = new DicomUID("1.2.840.10008.6.1.1076", "Anesthesia Category Code Type for Small Animal Anesthesia (611)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Category Code Type from Anesthesia Quality Initiative (612)</summary>
        public static readonly DicomUID AnesthesiaCategoryCodeTypeFromAnesthesiaQualityInitiative612 = new DicomUID("1.2.840.10008.6.1.1077", "Anesthesia Category Code Type from Anesthesia Quality Initiative (612)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Induction Code Type for Small Animal Anesthesia (613)</summary>
        public static readonly DicomUID AnesthesiaInductionCodeTypeForSmallAnimalAnesthesia613 = new DicomUID("1.2.840.10008.6.1.1078", "Anesthesia Induction Code Type for Small Animal Anesthesia (613)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Induction Code Type from Anesthesia Quality Initiative (614)</summary>
        public static readonly DicomUID AnesthesiaInductionCodeTypeFromAnesthesiaQualityInitiative614 = new DicomUID("1.2.840.10008.6.1.1079", "Anesthesia Induction Code Type from Anesthesia Quality Initiative (614)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Maintenance Code Type for Small Animal Anesthesia (615)</summary>
        public static readonly DicomUID AnesthesiaMaintenanceCodeTypeForSmallAnimalAnesthesia615 = new DicomUID("1.2.840.10008.6.1.1080", "Anesthesia Maintenance Code Type for Small Animal Anesthesia (615)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anesthesia Maintenance Code Type from Anesthesia Quality Initiative (616)</summary>
        public static readonly DicomUID AnesthesiaMaintenanceCodeTypeFromAnesthesiaQualityInitiative616 = new DicomUID("1.2.840.10008.6.1.1081", "Anesthesia Maintenance Code Type from Anesthesia Quality Initiative (616)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Method Code Type for Small Animal Anesthesia (617)</summary>
        public static readonly DicomUID AirwayManagementMethodCodeTypeForSmallAnimalAnesthesia617 = new DicomUID("1.2.840.10008.6.1.1082", "Airway Management Method Code Type for Small Animal Anesthesia (617)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Method Code Type from Anesthesia Quality Initiative (618)</summary>
        public static readonly DicomUID AirwayManagementMethodCodeTypeFromAnesthesiaQualityInitiative618 = new DicomUID("1.2.840.10008.6.1.1083", "Airway Management Method Code Type from Anesthesia Quality Initiative (618)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Sub-Method Code Type for Small Animal Anesthesia (619)</summary>
        public static readonly DicomUID AirwayManagementSubMethodCodeTypeForSmallAnimalAnesthesia619 = new DicomUID("1.2.840.10008.6.1.1084", "Airway Management Sub-Method Code Type for Small Animal Anesthesia (619)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Airway Management Sub-Method Code Type from Anesthesia Quality Initiative (620)</summary>
        public static readonly DicomUID AirwayManagementSubMethodCodeTypeFromAnesthesiaQualityInitiative620 = new DicomUID("1.2.840.10008.6.1.1085", "Airway Management Sub-Method Code Type from Anesthesia Quality Initiative (620)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication Type for Small Animal Anesthesia (621)</summary>
        public static readonly DicomUID MedicationTypeForSmallAnimalAnesthesia621 = new DicomUID("1.2.840.10008.6.1.1086", "Medication Type for Small Animal Anesthesia (621)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication Type Code Type from Anesthesia Quality Initiative (622)</summary>
        public static readonly DicomUID MedicationTypeCodeTypeFromAnesthesiaQualityInitiative622 = new DicomUID("1.2.840.10008.6.1.1087", "Medication Type Code Type from Anesthesia Quality Initiative (622)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Medication for Small Animal Anesthesia (623)</summary>
        public static readonly DicomUID MedicationForSmallAnimalAnesthesia623 = new DicomUID("1.2.840.10008.6.1.1088", "Medication for Small Animal Anesthesia (623)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Inhalational Anesthesia Agent for Small Animal Anesthesia (624)</summary>
        public static readonly DicomUID InhalationalAnesthesiaAgentForSmallAnimalAnesthesia624 = new DicomUID("1.2.840.10008.6.1.1089", "Inhalational Anesthesia Agent for Small Animal Anesthesia (624)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Injectable Anesthesia Agent for Small Animal Anesthesia (625)</summary>
        public static readonly DicomUID InjectableAnesthesiaAgentForSmallAnimalAnesthesia625 = new DicomUID("1.2.840.10008.6.1.1090", "Injectable Anesthesia Agent for Small Animal Anesthesia (625)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Premedication Agent for Small Animal Anesthesia (626)</summary>
        public static readonly DicomUID PremedicationAgentForSmallAnimalAnesthesia626 = new DicomUID("1.2.840.10008.6.1.1091", "Premedication Agent for Small Animal Anesthesia (626)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Neuromuscular Blocking Agent for Small Animal Anesthesia (627)</summary>
        public static readonly DicomUID NeuromuscularBlockingAgentForSmallAnimalAnesthesia627 = new DicomUID("1.2.840.10008.6.1.1092", "Neuromuscular Blocking Agent for Small Animal Anesthesia (627)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ancillary Medications for Small Animal Anesthesia (628)</summary>
        public static readonly DicomUID AncillaryMedicationsForSmallAnimalAnesthesia628 = new DicomUID("1.2.840.10008.6.1.1093", "Ancillary Medications for Small Animal Anesthesia (628)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Carrier Gases for Small Animal Anesthesia (629)</summary>
        public static readonly DicomUID CarrierGasesForSmallAnimalAnesthesia629 = new DicomUID("1.2.840.10008.6.1.1094", "Carrier Gases for Small Animal Anesthesia (629)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Local Anesthetics for Small Animal Anesthesia (630)</summary>
        public static readonly DicomUID LocalAnestheticsForSmallAnimalAnesthesia630 = new DicomUID("1.2.840.10008.6.1.1095", "Local Anesthetics for Small Animal Anesthesia (630)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Procedure Phase Requiring Anesthesia (631)</summary>
        public static readonly DicomUID ProcedurePhaseRequiringAnesthesia631 = new DicomUID("1.2.840.10008.6.1.1096", "Procedure Phase Requiring Anesthesia (631)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Surgical Procedure Phase Requiring Anesthesia (632)</summary>
        public static readonly DicomUID SurgicalProcedurePhaseRequiringAnesthesia632 = new DicomUID("1.2.840.10008.6.1.1097", "Surgical Procedure Phase Requiring Anesthesia (632)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Phase of Imaging Procedure Requiring Anesthesia (Retired) (633)</summary>
        public static readonly DicomUID PhaseOfImagingProcedureRequiringAnesthesia633RETIRED = new DicomUID("1.2.840.10008.6.1.1098", "Phase of Imaging Procedure Requiring Anesthesia (Retired) (633)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Animal Handling Phase (634)</summary>
        public static readonly DicomUID AnimalHandlingPhase634 = new DicomUID("1.2.840.10008.6.1.1099", "Animal Handling Phase (634)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Heating Method (635)</summary>
        public static readonly DicomUID HeatingMethod635 = new DicomUID("1.2.840.10008.6.1.1100", "Heating Method (635)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Temperature Sensor Device Component Type for Small Animal Procedure (636)</summary>
        public static readonly DicomUID TemperatureSensorDeviceComponentTypeForSmallAnimalProcedure636 = new DicomUID("1.2.840.10008.6.1.1101", "Temperature Sensor Device Component Type for Small Animal Procedure (636)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance Type (637)</summary>
        public static readonly DicomUID ExogenousSubstanceType637 = new DicomUID("1.2.840.10008.6.1.1102", "Exogenous Substance Type (637)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance (638)</summary>
        public static readonly DicomUID ExogenousSubstance638 = new DicomUID("1.2.840.10008.6.1.1103", "Exogenous Substance (638)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tumor Graft Histologic Type (639)</summary>
        public static readonly DicomUID TumorGraftHistologicType639 = new DicomUID("1.2.840.10008.6.1.1104", "Tumor Graft Histologic Type (639)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fibril (640)</summary>
        public static readonly DicomUID Fibril640 = new DicomUID("1.2.840.10008.6.1.1105", "Fibril (640)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Virus (641)</summary>
        public static readonly DicomUID Virus641 = new DicomUID("1.2.840.10008.6.1.1106", "Virus (641)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cytokine (642)</summary>
        public static readonly DicomUID Cytokine642 = new DicomUID("1.2.840.10008.6.1.1107", "Cytokine (642)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Toxin (643)</summary>
        public static readonly DicomUID Toxin643 = new DicomUID("1.2.840.10008.6.1.1108", "Toxin (643)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance Administration Site (644)</summary>
        public static readonly DicomUID ExogenousSubstanceAdministrationSite644 = new DicomUID("1.2.840.10008.6.1.1109", "Exogenous Substance Administration Site (644)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Exogenous Substance Origin Tissue (645)</summary>
        public static readonly DicomUID ExogenousSubstanceOriginTissue645 = new DicomUID("1.2.840.10008.6.1.1110", "Exogenous Substance Origin Tissue (645)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Preclinical Small Animal Imaging Procedure (646)</summary>
        public static readonly DicomUID PreclinicalSmallAnimalImagingProcedure646 = new DicomUID("1.2.840.10008.6.1.1111", "Preclinical Small Animal Imaging Procedure (646)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Position Reference Indicator for Frame of Reference (647)</summary>
        public static readonly DicomUID PositionReferenceIndicatorForFrameOfReference647 = new DicomUID("1.2.840.10008.6.1.1112", "Position Reference Indicator for Frame of Reference (647)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Present-Absent Only (241)</summary>
        public static readonly DicomUID PresentAbsentOnly241 = new DicomUID("1.2.840.10008.6.1.1113", "Present-Absent Only (241)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Water Equivalent Diameter Method (10024)</summary>
        public static readonly DicomUID WaterEquivalentDiameterMethod10024 = new DicomUID("1.2.840.10008.6.1.1114", "Water Equivalent Diameter Method (10024)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Purpose of Reference (7022)</summary>
        public static readonly DicomUID RadiotherapyPurposeOfReference7022 = new DicomUID("1.2.840.10008.6.1.1115", "Radiotherapy Purpose of Reference (7022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Content Assessment Type (701)</summary>
        public static readonly DicomUID ContentAssessmentType701 = new DicomUID("1.2.840.10008.6.1.1116", "Content Assessment Type (701)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Content Assessment Type (702)</summary>
        public static readonly DicomUID RTContentAssessmentType702 = new DicomUID("1.2.840.10008.6.1.1117", "RT Content Assessment Type (702)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Assessment Basis (703)</summary>
        public static readonly DicomUID AssessmentBasis703 = new DicomUID("1.2.840.10008.6.1.1118", "Assessment Basis (703)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reader Specialty (7449)</summary>
        public static readonly DicomUID ReaderSpecialty7449 = new DicomUID("1.2.840.10008.6.1.1119", "Reader Specialty (7449)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Requested Report Type (9233)</summary>
        public static readonly DicomUID RequestedReportType9233 = new DicomUID("1.2.840.10008.6.1.1120", "Requested Report Type (9233)", DicomUidType.ContextGroupName, false);

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

        ///<summary>Context Group Name: Anatomical Reference Basis - Extremity (1006)</summary>
        public static readonly DicomUID AnatomicalReferenceBasisExtremity1006 = new DicomUID("1.2.840.10008.6.1.1127", "Anatomical Reference Basis - Extremity (1006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reference Geometry - Plane (1010)</summary>
        public static readonly DicomUID ReferenceGeometryPlane1010 = new DicomUID("1.2.840.10008.6.1.1128", "Reference Geometry - Plane (1010)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reference Geometry - Point (1011)</summary>
        public static readonly DicomUID ReferenceGeometryPoint1011 = new DicomUID("1.2.840.10008.6.1.1129", "Reference Geometry - Point (1011)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Alignment Method (1015)</summary>
        public static readonly DicomUID PatientAlignmentMethod1015 = new DicomUID("1.2.840.10008.6.1.1130", "Patient Alignment Method (1015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contraindications For CT Imaging (1200)</summary>
        public static readonly DicomUID ContraindicationsForCTImaging1200 = new DicomUID("1.2.840.10008.6.1.1131", "Contraindications For CT Imaging (1200)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiducial Category (7110)</summary>
        public static readonly DicomUID FiducialCategory7110 = new DicomUID("1.2.840.10008.6.1.1132", "Fiducial Category (7110)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fiducial (7111)</summary>
        public static readonly DicomUID Fiducial7111 = new DicomUID("1.2.840.10008.6.1.1133", "Fiducial (7111)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-Image Source Instance Purpose of Reference (7013)</summary>
        public static readonly DicomUID NonImageSourceInstancePurposeOfReference7013 = new DicomUID("1.2.840.10008.6.1.1134", "Non-Image Source Instance Purpose of Reference (7013)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Process Output (7023)</summary>
        public static readonly DicomUID RTProcessOutput7023 = new DicomUID("1.2.840.10008.6.1.1135", "RT Process Output (7023)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Process Input (7024)</summary>
        public static readonly DicomUID RTProcessInput7024 = new DicomUID("1.2.840.10008.6.1.1136", "RT Process Input (7024)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Process Input Used (7025)</summary>
        public static readonly DicomUID RTProcessInputUsed7025 = new DicomUID("1.2.840.10008.6.1.1137", "RT Process Input Used (7025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Anatomy (6300)</summary>
        public static readonly DicomUID ProstateAnatomy6300 = new DicomUID("1.2.840.10008.6.1.1138", "Prostate Anatomy (6300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy from PI-RADS v2 (6301)</summary>
        public static readonly DicomUID ProstateSectorAnatomyFromPIRADSV26301 = new DicomUID("1.2.840.10008.6.1.1139", "Prostate Sector Anatomy from PI-RADS v2 (6301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy from European Concensus 16 Sector (Minimal) Model (6302)</summary>
        public static readonly DicomUID ProstateSectorAnatomyFromEuropeanConcensus16SectorMinimalModel6302 = new DicomUID("1.2.840.10008.6.1.1140", "Prostate Sector Anatomy from European Concensus 16 Sector (Minimal) Model (6302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy from European Concensus 27 Sector (Optimal) Model (6303)</summary>
        public static readonly DicomUID ProstateSectorAnatomyFromEuropeanConcensus27SectorOptimalModel6303 = new DicomUID("1.2.840.10008.6.1.1141", "Prostate Sector Anatomy from European Concensus 27 Sector (Optimal) Model (6303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Measurement Selection Reason (12301)</summary>
        public static readonly DicomUID MeasurementSelectionReason12301 = new DicomUID("1.2.840.10008.6.1.1142", "Measurement Selection Reason (12301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Finding Observation Type (12302)</summary>
        public static readonly DicomUID EchoFindingObservationType12302 = new DicomUID("1.2.840.10008.6.1.1143", "Echo Finding Observation Type (12302)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Measurement Type (12303)</summary>
        public static readonly DicomUID EchoMeasurementType12303 = new DicomUID("1.2.840.10008.6.1.1144", "Echo Measurement Type (12303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Measured Property (12304)</summary>
        public static readonly DicomUID EchoMeasuredProperty12304 = new DicomUID("1.2.840.10008.6.1.1145", "Echo Measured Property (12304)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Basic Echo Anatomic Site (12305)</summary>
        public static readonly DicomUID BasicEchoAnatomicSite12305 = new DicomUID("1.2.840.10008.6.1.1146", "Basic Echo Anatomic Site (12305)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Flow Direction (12306)</summary>
        public static readonly DicomUID EchoFlowDirection12306 = new DicomUID("1.2.840.10008.6.1.1147", "Echo Flow Direction (12306)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cardiac Phase and Time Point (12307)</summary>
        public static readonly DicomUID CardiacPhaseAndTimePoint12307 = new DicomUID("1.2.840.10008.6.1.1148", "Cardiac Phase and Time Point (12307)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Core Echo Measurement (12300)</summary>
        public static readonly DicomUID CoreEchoMeasurement12300 = new DicomUID("1.2.840.10008.6.1.1149", "Core Echo Measurement (12300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OCT-A Processing Algorithm Family (4270)</summary>
        public static readonly DicomUID OCTAProcessingAlgorithmFamily4270 = new DicomUID("1.2.840.10008.6.1.1150", "OCT-A Processing Algorithm Family (4270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: En Face Image Type (4271)</summary>
        public static readonly DicomUID EnFaceImageType4271 = new DicomUID("1.2.840.10008.6.1.1151", "En Face Image Type (4271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Opt Scan Pattern Type (4272)</summary>
        public static readonly DicomUID OptScanPatternType4272 = new DicomUID("1.2.840.10008.6.1.1152", "Opt Scan Pattern Type (4272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Retinal Segmentation Surface (4273)</summary>
        public static readonly DicomUID RetinalSegmentationSurface4273 = new DicomUID("1.2.840.10008.6.1.1153", "Retinal Segmentation Surface (4273)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organ for Radiation Dose Estimate (10060)</summary>
        public static readonly DicomUID OrganForRadiationDoseEstimate10060 = new DicomUID("1.2.840.10008.6.1.1154", "Organ for Radiation Dose Estimate (10060)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Absorbed Radiation Dose Type (10061)</summary>
        public static readonly DicomUID AbsorbedRadiationDoseType10061 = new DicomUID("1.2.840.10008.6.1.1155", "Absorbed Radiation Dose Type (10061)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equivalent Radiation Dose Type (10062)</summary>
        public static readonly DicomUID EquivalentRadiationDoseType10062 = new DicomUID("1.2.840.10008.6.1.1156", "Equivalent Radiation Dose Type (10062)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Estimate Distribution Representation (10063)</summary>
        public static readonly DicomUID RadiationDoseEstimateDistributionRepresentation10063 = new DicomUID("1.2.840.10008.6.1.1157", "Radiation Dose Estimate Distribution Representation (10063)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Model Type (10064)</summary>
        public static readonly DicomUID PatientModelType10064 = new DicomUID("1.2.840.10008.6.1.1158", "Patient Model Type (10064)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Transport Model Type (10065)</summary>
        public static readonly DicomUID RadiationTransportModelType10065 = new DicomUID("1.2.840.10008.6.1.1159", "Radiation Transport Model Type (10065)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Attenuator Category (10066)</summary>
        public static readonly DicomUID AttenuatorCategory10066 = new DicomUID("1.2.840.10008.6.1.1160", "Attenuator Category (10066)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Attenuator Material (10067)</summary>
        public static readonly DicomUID RadiationAttenuatorMaterial10067 = new DicomUID("1.2.840.10008.6.1.1161", "Radiation Attenuator Material (10067)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Estimate Method Type (10068)</summary>
        public static readonly DicomUID EstimateMethodType10068 = new DicomUID("1.2.840.10008.6.1.1162", "Estimate Method Type (10068)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Estimate Parameter (10069)</summary>
        public static readonly DicomUID RadiationDoseEstimateParameter10069 = new DicomUID("1.2.840.10008.6.1.1163", "Radiation Dose Estimate Parameter (10069)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Type (10070)</summary>
        public static readonly DicomUID RadiationDoseType10070 = new DicomUID("1.2.840.10008.6.1.1164", "Radiation Dose Type (10070)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Component Semantic (7270)</summary>
        public static readonly DicomUID MRDiffusionComponentSemantic7270 = new DicomUID("1.2.840.10008.6.1.1165", "MR Diffusion Component Semantic (7270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Anisotropy Index (7271)</summary>
        public static readonly DicomUID MRDiffusionAnisotropyIndex7271 = new DicomUID("1.2.840.10008.6.1.1166", "MR Diffusion Anisotropy Index (7271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Parameter (7272)</summary>
        public static readonly DicomUID MRDiffusionModelParameter7272 = new DicomUID("1.2.840.10008.6.1.1167", "MR Diffusion Model Parameter (7272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model (7273)</summary>
        public static readonly DicomUID MRDiffusionModel7273 = new DicomUID("1.2.840.10008.6.1.1168", "MR Diffusion Model (7273)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Fitting Method (7274)</summary>
        public static readonly DicomUID MRDiffusionModelFittingMethod7274 = new DicomUID("1.2.840.10008.6.1.1169", "MR Diffusion Model Fitting Method (7274)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Specific Method (7275)</summary>
        public static readonly DicomUID MRDiffusionModelSpecificMethod7275 = new DicomUID("1.2.840.10008.6.1.1170", "MR Diffusion Model Specific Method (7275)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Diffusion Model Input (7276)</summary>
        public static readonly DicomUID MRDiffusionModelInput7276 = new DicomUID("1.2.840.10008.6.1.1171", "MR Diffusion Model Input (7276)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Diffusion Rate Area Over Time Unit (7277)</summary>
        public static readonly DicomUID DiffusionRateAreaOverTimeUnit7277 = new DicomUID("1.2.840.10008.6.1.1172", "Diffusion Rate Area Over Time Unit (7277)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pediatric Size Category (7039)</summary>
        public static readonly DicomUID PediatricSizeCategory7039 = new DicomUID("1.2.840.10008.6.1.1173", "Pediatric Size Category (7039)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Calcium Scoring Patient Size Category (7041)</summary>
        public static readonly DicomUID CalciumScoringPatientSizeCategory7041 = new DicomUID("1.2.840.10008.6.1.1174", "Calcium Scoring Patient Size Category (7041)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reason for Repeating Acquisition (10034)</summary>
        public static readonly DicomUID ReasonForRepeatingAcquisition10034 = new DicomUID("1.2.840.10008.6.1.1175", "Reason for Repeating Acquisition (10034)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Protocol Assertion (800)</summary>
        public static readonly DicomUID ProtocolAssertion800 = new DicomUID("1.2.840.10008.6.1.1176", "Protocol Assertion (800)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapeutic Dose Measurement Device (7026)</summary>
        public static readonly DicomUID RadiotherapeuticDoseMeasurementDevice7026 = new DicomUID("1.2.840.10008.6.1.1177", "Radiotherapeutic Dose Measurement Device (7026)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Export Additional Information Document Title (7014)</summary>
        public static readonly DicomUID ExportAdditionalInformationDocumentTitle7014 = new DicomUID("1.2.840.10008.6.1.1178", "Export Additional Information Document Title (7014)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Export Delay Reason (7015)</summary>
        public static readonly DicomUID ExportDelayReason7015 = new DicomUID("1.2.840.10008.6.1.1179", "Export Delay Reason (7015)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Level of Difficulty (7016)</summary>
        public static readonly DicomUID LevelOfDifficulty7016 = new DicomUID("1.2.840.10008.6.1.1180", "Level of Difficulty (7016)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Category of Teaching Material - Imaging (7017)</summary>
        public static readonly DicomUID CategoryOfTeachingMaterialImaging7017 = new DicomUID("1.2.840.10008.6.1.1181", "Category of Teaching Material - Imaging (7017)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Miscellaneous Document Title (7018)</summary>
        public static readonly DicomUID MiscellaneousDocumentTitle7018 = new DicomUID("1.2.840.10008.6.1.1182", "Miscellaneous Document Title (7018)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmentation Non-Image Source Purpose of Reference (7019)</summary>
        public static readonly DicomUID SegmentationNonImageSourcePurposeOfReference7019 = new DicomUID("1.2.840.10008.6.1.1183", "Segmentation Non-Image Source Purpose of Reference (7019)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Longitudinal Temporal Event Type (280)</summary>
        public static readonly DicomUID LongitudinalTemporalEventType280 = new DicomUID("1.2.840.10008.6.1.1184", "Longitudinal Temporal Event Type (280)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Object Type - Physical Object (6401)</summary>
        public static readonly DicomUID NonLesionObjectTypePhysicalObject6401 = new DicomUID("1.2.840.10008.6.1.1185", "Non-lesion Object Type - Physical Object (6401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Object Type - Substance (6402)</summary>
        public static readonly DicomUID NonLesionObjectTypeSubstance6402 = new DicomUID("1.2.840.10008.6.1.1186", "Non-lesion Object Type - Substance (6402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-lesion Object Type - Tissue (6403)</summary>
        public static readonly DicomUID NonLesionObjectTypeTissue6403 = new DicomUID("1.2.840.10008.6.1.1187", "Non-lesion Object Type - Tissue (6403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Non-lesion Object Type - Physical Object (6404)</summary>
        public static readonly DicomUID ChestNonLesionObjectTypePhysicalObject6404 = new DicomUID("1.2.840.10008.6.1.1188", "Chest Non-lesion Object Type - Physical Object (6404)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Chest Non-lesion Object Type - Tissue (6405)</summary>
        public static readonly DicomUID ChestNonLesionObjectTypeTissue6405 = new DicomUID("1.2.840.10008.6.1.1189", "Chest Non-lesion Object Type - Tissue (6405)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tissue Segmentation Property Type (7191)</summary>
        public static readonly DicomUID TissueSegmentationPropertyType7191 = new DicomUID("1.2.840.10008.6.1.1190", "Tissue Segmentation Property Type (7191)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Structure Segmentation Property Type (7192)</summary>
        public static readonly DicomUID AnatomicalStructureSegmentationPropertyType7192 = new DicomUID("1.2.840.10008.6.1.1191", "Anatomical Structure Segmentation Property Type (7192)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Physical Object Segmentation Property Type (7193)</summary>
        public static readonly DicomUID PhysicalObjectSegmentationPropertyType7193 = new DicomUID("1.2.840.10008.6.1.1192", "Physical Object Segmentation Property Type (7193)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Morphologically Abnormal Structure Segmentation Property Type (7194)</summary>
        public static readonly DicomUID MorphologicallyAbnormalStructureSegmentationPropertyType7194 = new DicomUID("1.2.840.10008.6.1.1193", "Morphologically Abnormal Structure Segmentation Property Type (7194)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Function Segmentation Property Type (7195)</summary>
        public static readonly DicomUID FunctionSegmentationPropertyType7195 = new DicomUID("1.2.840.10008.6.1.1194", "Function Segmentation Property Type (7195)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Spatial and Relational Concept Segmentation Property Type (7196)</summary>
        public static readonly DicomUID SpatialAndRelationalConceptSegmentationPropertyType7196 = new DicomUID("1.2.840.10008.6.1.1195", "Spatial and Relational Concept Segmentation Property Type (7196)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Body Substance Segmentation Property Type (7197)</summary>
        public static readonly DicomUID BodySubstanceSegmentationPropertyType7197 = new DicomUID("1.2.840.10008.6.1.1196", "Body Substance Segmentation Property Type (7197)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Substance Segmentation Property Type (7198)</summary>
        public static readonly DicomUID SubstanceSegmentationPropertyType7198 = new DicomUID("1.2.840.10008.6.1.1197", "Substance Segmentation Property Type (7198)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Interpretation Request Discontinuation Reason (9303)</summary>
        public static readonly DicomUID InterpretationRequestDiscontinuationReason9303 = new DicomUID("1.2.840.10008.6.1.1198", "Interpretation Request Discontinuation Reason (9303)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gray Level Run Length Based Feature (7475)</summary>
        public static readonly DicomUID GrayLevelRunLengthBasedFeature7475 = new DicomUID("1.2.840.10008.6.1.1199", "Gray Level Run Length Based Feature (7475)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Gray Level Size Zone Based Feature (7476)</summary>
        public static readonly DicomUID GrayLevelSizeZoneBasedFeature7476 = new DicomUID("1.2.840.10008.6.1.1200", "Gray Level Size Zone Based Feature (7476)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Encapsulated Document Source Purpose of Reference (7060)</summary>
        public static readonly DicomUID EncapsulatedDocumentSourcePurposeOfReference7060 = new DicomUID("1.2.840.10008.6.1.1201", "Encapsulated Document Source Purpose of Reference (7060)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model Document Title (7061)</summary>
        public static readonly DicomUID ModelDocumentTitle7061 = new DicomUID("1.2.840.10008.6.1.1202", "Model Document Title (7061)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference to Predecessor 3D Model (7062)</summary>
        public static readonly DicomUID PurposeOfReferenceToPredecessor3DModel7062 = new DicomUID("1.2.840.10008.6.1.1203", "Purpose of Reference to Predecessor 3D Model (7062)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model Scale Unit (7063)</summary>
        public static readonly DicomUID ModelScaleUnit7063 = new DicomUID("1.2.840.10008.6.1.1204", "Model Scale Unit (7063)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Model Usage (7064)</summary>
        public static readonly DicomUID ModelUsage7064 = new DicomUID("1.2.840.10008.6.1.1205", "Model Usage (7064)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Dose Unit (10071)</summary>
        public static readonly DicomUID RadiationDoseUnit10071 = new DicomUID("1.2.840.10008.6.1.1206", "Radiation Dose Unit (10071)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Fiducial (7112)</summary>
        public static readonly DicomUID RadiotherapyFiducial7112 = new DicomUID("1.2.840.10008.6.1.1207", "Radiotherapy Fiducial (7112)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-energy Relevant Material (300)</summary>
        public static readonly DicomUID MultiEnergyRelevantMaterial300 = new DicomUID("1.2.840.10008.6.1.1208", "Multi-energy Relevant Material (300)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-energy Material Unit (301)</summary>
        public static readonly DicomUID MultiEnergyMaterialUnit301 = new DicomUID("1.2.840.10008.6.1.1209", "Multi-energy Material Unit (301)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dosimetric Objective Type (9500)</summary>
        public static readonly DicomUID DosimetricObjectiveType9500 = new DicomUID("1.2.840.10008.6.1.1210", "Dosimetric Objective Type (9500)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prescription Anatomy Category (9501)</summary>
        public static readonly DicomUID PrescriptionAnatomyCategory9501 = new DicomUID("1.2.840.10008.6.1.1211", "Prescription Anatomy Category (9501)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Segment Annotation Category (9502)</summary>
        public static readonly DicomUID RTSegmentAnnotationCategory9502 = new DicomUID("1.2.840.10008.6.1.1212", "RT Segment Annotation Category (9502)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Therapeutic Role Category (9503)</summary>
        public static readonly DicomUID RadiotherapyTherapeuticRoleCategory9503 = new DicomUID("1.2.840.10008.6.1.1213", "Radiotherapy Therapeutic Role Category (9503)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Geometric Information (9504)</summary>
        public static readonly DicomUID RTGeometricInformation9504 = new DicomUID("1.2.840.10008.6.1.1214", "RT Geometric Information (9504)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fixation or Positioning Device (9505)</summary>
        public static readonly DicomUID FixationOrPositioningDevice9505 = new DicomUID("1.2.840.10008.6.1.1215", "Fixation or Positioning Device (9505)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brachytherapy Device (9506)</summary>
        public static readonly DicomUID BrachytherapyDevice9506 = new DicomUID("1.2.840.10008.6.1.1216", "Brachytherapy Device (9506)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: External Body Model (9507)</summary>
        public static readonly DicomUID ExternalBodyModel9507 = new DicomUID("1.2.840.10008.6.1.1217", "External Body Model (9507)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-specific Volume (9508)</summary>
        public static readonly DicomUID NonSpecificVolume9508 = new DicomUID("1.2.840.10008.6.1.1218", "Non-specific Volume (9508)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference For RT Physician Intent Input (9509)</summary>
        public static readonly DicomUID PurposeOfReferenceForRTPhysicianIntentInput9509 = new DicomUID("1.2.840.10008.6.1.1219", "Purpose of Reference For RT Physician Intent Input (9509)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Purpose of Reference For RT Treatment Planning Input (9510)</summary>
        public static readonly DicomUID PurposeOfReferenceForRTTreatmentPlanningInput9510 = new DicomUID("1.2.840.10008.6.1.1220", "Purpose of Reference For RT Treatment Planning Input (9510)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General External Radiotherapy Procedure Technique (9511)</summary>
        public static readonly DicomUID GeneralExternalRadiotherapyProcedureTechnique9511 = new DicomUID("1.2.840.10008.6.1.1221", "General External Radiotherapy Procedure Technique (9511)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tomotherapeutic Radiotherapy Procedure Technique (9512)</summary>
        public static readonly DicomUID TomotherapeuticRadiotherapyProcedureTechnique9512 = new DicomUID("1.2.840.10008.6.1.1222", "Tomotherapeutic Radiotherapy Procedure Technique (9512)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fixation Device (9513)</summary>
        public static readonly DicomUID FixationDevice9513 = new DicomUID("1.2.840.10008.6.1.1223", "Fixation Device (9513)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomical Structure For Radiotherapy (9514)</summary>
        public static readonly DicomUID AnatomicalStructureForRadiotherapy9514 = new DicomUID("1.2.840.10008.6.1.1224", "Anatomical Structure For Radiotherapy (9514)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Patient Support Device (9515)</summary>
        public static readonly DicomUID RTPatientSupportDevice9515 = new DicomUID("1.2.840.10008.6.1.1225", "RT Patient Support Device (9515)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Bolus Device Type (9516)</summary>
        public static readonly DicomUID RadiotherapyBolusDeviceType9516 = new DicomUID("1.2.840.10008.6.1.1226", "Radiotherapy Bolus Device Type (9516)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Block Device Type (9517)</summary>
        public static readonly DicomUID RadiotherapyBlockDeviceType9517 = new DicomUID("1.2.840.10008.6.1.1227", "Radiotherapy Block Device Type (9517)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Accessory No-slot Holder Device Type (9518)</summary>
        public static readonly DicomUID RadiotherapyAccessoryNoSlotHolderDeviceType9518 = new DicomUID("1.2.840.10008.6.1.1228", "Radiotherapy Accessory No-slot Holder Device Type (9518)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Accessory Slot Holder Device Type (9519)</summary>
        public static readonly DicomUID RadiotherapyAccessorySlotHolderDeviceType9519 = new DicomUID("1.2.840.10008.6.1.1229", "Radiotherapy Accessory Slot Holder Device Type (9519)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmented RT Accessory Device (9520)</summary>
        public static readonly DicomUID SegmentedRTAccessoryDevice9520 = new DicomUID("1.2.840.10008.6.1.1230", "Segmented RT Accessory Device (9520)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Energy Unit (9521)</summary>
        public static readonly DicomUID RadiotherapyTreatmentEnergyUnit9521 = new DicomUID("1.2.840.10008.6.1.1231", "Radiotherapy Treatment Energy Unit (9521)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Multi-source Radiotherapy Procedure Technique (9522)</summary>
        public static readonly DicomUID MultiSourceRadiotherapyProcedureTechnique9522 = new DicomUID("1.2.840.10008.6.1.1232", "Multi-source Radiotherapy Procedure Technique (9522)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Robotic Radiotherapy Procedure Technique (9523)</summary>
        public static readonly DicomUID RoboticRadiotherapyProcedureTechnique9523 = new DicomUID("1.2.840.10008.6.1.1233", "Robotic Radiotherapy Procedure Technique (9523)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Procedure Technique (9524)</summary>
        public static readonly DicomUID RadiotherapyProcedureTechnique9524 = new DicomUID("1.2.840.10008.6.1.1234", "Radiotherapy Procedure Technique (9524)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Therapy Particle (9525)</summary>
        public static readonly DicomUID RadiationTherapyParticle9525 = new DicomUID("1.2.840.10008.6.1.1235", "Radiation Therapy Particle (9525)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ion Therapy Particle (9526)</summary>
        public static readonly DicomUID IonTherapyParticle9526 = new DicomUID("1.2.840.10008.6.1.1236", "Ion Therapy Particle (9526)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Teletherapy Isotope (9527)</summary>
        public static readonly DicomUID TeletherapyIsotope9527 = new DicomUID("1.2.840.10008.6.1.1237", "Teletherapy Isotope (9527)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brachytherapy Isotope (9528)</summary>
        public static readonly DicomUID BrachytherapyIsotope9528 = new DicomUID("1.2.840.10008.6.1.1238", "Brachytherapy Isotope (9528)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Single Dose Dosimetric Objective (9529)</summary>
        public static readonly DicomUID SingleDoseDosimetricObjective9529 = new DicomUID("1.2.840.10008.6.1.1239", "Single Dose Dosimetric Objective (9529)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Percentage and Dose Dosimetric Objective (9530)</summary>
        public static readonly DicomUID PercentageAndDoseDosimetricObjective9530 = new DicomUID("1.2.840.10008.6.1.1240", "Percentage and Dose Dosimetric Objective (9530)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Volume and Dose Dosimetric Objective (9531)</summary>
        public static readonly DicomUID VolumeAndDoseDosimetricObjective9531 = new DicomUID("1.2.840.10008.6.1.1241", "Volume and Dose Dosimetric Objective (9531)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: No-Parameter Dosimetric Objective (9532)</summary>
        public static readonly DicomUID NoParameterDosimetricObjective9532 = new DicomUID("1.2.840.10008.6.1.1242", "No-Parameter Dosimetric Objective (9532)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Delivery Time Structure (9533)</summary>
        public static readonly DicomUID DeliveryTimeStructure9533 = new DicomUID("1.2.840.10008.6.1.1243", "Delivery Time Structure (9533)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Target (9534)</summary>
        public static readonly DicomUID RadiotherapyTarget9534 = new DicomUID("1.2.840.10008.6.1.1244", "Radiotherapy Target (9534)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Dose Calculation Role (9535)</summary>
        public static readonly DicomUID RadiotherapyDoseCalculationRole9535 = new DicomUID("1.2.840.10008.6.1.1245", "Radiotherapy Dose Calculation Role (9535)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Prescribing and Segmenting Person Role (9536)</summary>
        public static readonly DicomUID RadiotherapyPrescribingAndSegmentingPersonRole9536 = new DicomUID("1.2.840.10008.6.1.1246", "Radiotherapy Prescribing and Segmenting Person Role (9536)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Effective Dose Calculation Method Category (9537)</summary>
        public static readonly DicomUID EffectiveDoseCalculationMethodCategory9537 = new DicomUID("1.2.840.10008.6.1.1247", "Effective Dose Calculation Method Category (9537)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Transport-based Effective Dose Method Modifier (9538)</summary>
        public static readonly DicomUID RadiationTransportBasedEffectiveDoseMethodModifier9538 = new DicomUID("1.2.840.10008.6.1.1248", "Radiation Transport-based Effective Dose Method Modifier (9538)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fractionation-based Effective Dose Method Modifier (9539)</summary>
        public static readonly DicomUID FractionationBasedEffectiveDoseMethodModifier9539 = new DicomUID("1.2.840.10008.6.1.1249", "Fractionation-based Effective Dose Method Modifier (9539)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Adverse Event (60)</summary>
        public static readonly DicomUID ImagingAgentAdministrationAdverseEvent60 = new DicomUID("1.2.840.10008.6.1.1250", "Imaging Agent Administration Adverse Event (60)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Time Relative to Procedure (Retired) (61)</summary>
        public static readonly DicomUID TimeRelativeToProcedure61RETIRED = new DicomUID("1.2.840.10008.6.1.1251", "Time Relative to Procedure (Retired) (61)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Imaging Agent Administration Phase Type (62)</summary>
        public static readonly DicomUID ImagingAgentAdministrationPhaseType62 = new DicomUID("1.2.840.10008.6.1.1252", "Imaging Agent Administration Phase Type (62)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Mode (63)</summary>
        public static readonly DicomUID ImagingAgentAdministrationMode63 = new DicomUID("1.2.840.10008.6.1.1253", "Imaging Agent Administration Mode (63)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Patient State (64)</summary>
        public static readonly DicomUID ImagingAgentAdministrationPatientState64 = new DicomUID("1.2.840.10008.6.1.1254", "Imaging Agent Administration Patient State (64)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Premedication (65)</summary>
        public static readonly DicomUID ImagingAgentAdministrationPremedication65 = new DicomUID("1.2.840.10008.6.1.1255", "Imaging Agent Administration Premedication (65)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Medication (66)</summary>
        public static readonly DicomUID ImagingAgentAdministrationMedication66 = new DicomUID("1.2.840.10008.6.1.1256", "Imaging Agent Administration Medication (66)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Completion Status (67)</summary>
        public static readonly DicomUID ImagingAgentAdministrationCompletionStatus67 = new DicomUID("1.2.840.10008.6.1.1257", "Imaging Agent Administration Completion Status (67)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Pharmaceutical Presentation Unit (68)</summary>
        public static readonly DicomUID ImagingAgentAdministrationPharmaceuticalPresentationUnit68 = new DicomUID("1.2.840.10008.6.1.1258", "Imaging Agent Administration Pharmaceutical Presentation Unit (68)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Consumable (69)</summary>
        public static readonly DicomUID ImagingAgentAdministrationConsumable69 = new DicomUID("1.2.840.10008.6.1.1259", "Imaging Agent Administration Consumable (69)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Flush (70)</summary>
        public static readonly DicomUID Flush70 = new DicomUID("1.2.840.10008.6.1.1260", "Flush (70)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Injector Event Type (71)</summary>
        public static readonly DicomUID ImagingAgentAdministrationInjectorEventType71 = new DicomUID("1.2.840.10008.6.1.1261", "Imaging Agent Administration Injector Event Type (71)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Step Type (72)</summary>
        public static readonly DicomUID ImagingAgentAdministrationStepType72 = new DicomUID("1.2.840.10008.6.1.1262", "Imaging Agent Administration Step Type (72)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Bolus Shaping Curve (73)</summary>
        public static readonly DicomUID BolusShapingCurve73 = new DicomUID("1.2.840.10008.6.1.1263", "Bolus Shaping Curve (73)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Agent Administration Consumable Catheter Type (74)</summary>
        public static readonly DicomUID ImagingAgentAdministrationConsumableCatheterType74 = new DicomUID("1.2.840.10008.6.1.1264", "Imaging Agent Administration Consumable Catheter Type (74)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Low High or Equal (75)</summary>
        public static readonly DicomUID LowHighOrEqual75 = new DicomUID("1.2.840.10008.6.1.1265", "Low High or Equal (75)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Premedication Type (76)</summary>
        public static readonly DicomUID PremedicationType76 = new DicomUID("1.2.840.10008.6.1.1266", "Premedication Type (76)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Laterality with Median (245)</summary>
        public static readonly DicomUID LateralityWithMedian245 = new DicomUID("1.2.840.10008.6.1.1267", "Laterality with Median (245)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Dermatology Anatomic Site (4029)</summary>
        public static readonly DicomUID DermatologyAnatomicSite4029 = new DicomUID("1.2.840.10008.6.1.1268", "Dermatology Anatomic Site (4029)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Quantitative Image Feature (218)</summary>
        public static readonly DicomUID QuantitativeImageFeature218 = new DicomUID("1.2.840.10008.6.1.1269", "Quantitative Image Feature (218)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Global Shape Descriptor (7477)</summary>
        public static readonly DicomUID GlobalShapeDescriptor7477 = new DicomUID("1.2.840.10008.6.1.1270", "Global Shape Descriptor (7477)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intensity Histogram Feature (7478)</summary>
        public static readonly DicomUID IntensityHistogramFeature7478 = new DicomUID("1.2.840.10008.6.1.1271", "Intensity Histogram Feature (7478)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Grey Level Distance Zone Based Feature (7479)</summary>
        public static readonly DicomUID GreyLevelDistanceZoneBasedFeature7479 = new DicomUID("1.2.840.10008.6.1.1272", "Grey Level Distance Zone Based Feature (7479)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Neighbourhood Grey Tone Difference Based Feature (7500)</summary>
        public static readonly DicomUID NeighbourhoodGreyToneDifferenceBasedFeature7500 = new DicomUID("1.2.840.10008.6.1.1273", "Neighbourhood Grey Tone Difference Based Feature (7500)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Neighbouring Grey Level Dependence Based Feature (7501)</summary>
        public static readonly DicomUID NeighbouringGreyLevelDependenceBasedFeature7501 = new DicomUID("1.2.840.10008.6.1.1274", "Neighbouring Grey Level Dependence Based Feature (7501)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cornea Measurement Method Descriptor (4242)</summary>
        public static readonly DicomUID CorneaMeasurementMethodDescriptor4242 = new DicomUID("1.2.840.10008.6.1.1275", "Cornea Measurement Method Descriptor (4242)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Segmented Radiotherapeutic Dose Measurement Device (7027)</summary>
        public static readonly DicomUID SegmentedRadiotherapeuticDoseMeasurementDevice7027 = new DicomUID("1.2.840.10008.6.1.1276", "Segmented Radiotherapeutic Dose Measurement Device (7027)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clinical Course of Disease (6098)</summary>
        public static readonly DicomUID ClinicalCourseOfDisease6098 = new DicomUID("1.2.840.10008.6.1.1277", "Clinical Course of Disease (6098)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Racial Group (6099)</summary>
        public static readonly DicomUID RacialGroup6099 = new DicomUID("1.2.840.10008.6.1.1278", "Racial Group (6099)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Relative Laterality (246)</summary>
        public static readonly DicomUID RelativeLaterality246 = new DicomUID("1.2.840.10008.6.1.1279", "Relative Laterality (246)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Lesion Segmentation Type With Necrosis (7168)</summary>
        public static readonly DicomUID BrainLesionSegmentationTypeWithNecrosis7168 = new DicomUID("1.2.840.10008.6.1.1280", "Brain Lesion Segmentation Type With Necrosis (7168)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Brain Lesion Segmentation Type Without Necrosis (7169)</summary>
        public static readonly DicomUID BrainLesionSegmentationTypeWithoutNecrosis7169 = new DicomUID("1.2.840.10008.6.1.1281", "Brain Lesion Segmentation Type Without Necrosis (7169)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Non-Acquisition Modality (32)</summary>
        public static readonly DicomUID NonAcquisitionModality32 = new DicomUID("1.2.840.10008.6.1.1282", "Non-Acquisition Modality (32)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Modality (33)</summary>
        public static readonly DicomUID Modality33 = new DicomUID("1.2.840.10008.6.1.1283", "Modality (33)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Laterality Left-Right Only (247)</summary>
        public static readonly DicomUID LateralityLeftRightOnly247 = new DicomUID("1.2.840.10008.6.1.1284", "Laterality Left-Right Only (247)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Qualitative Evaluation Modifier Type (210)</summary>
        public static readonly DicomUID QualitativeEvaluationModifierType210 = new DicomUID("1.2.840.10008.6.1.1285", "Qualitative Evaluation Modifier Type (210)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Qualitative Evaluation Modifier Value (211)</summary>
        public static readonly DicomUID QualitativeEvaluationModifierValue211 = new DicomUID("1.2.840.10008.6.1.1286", "Qualitative Evaluation Modifier Value (211)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Generic Anatomic Location Modifier (212)</summary>
        public static readonly DicomUID GenericAnatomicLocationModifier212 = new DicomUID("1.2.840.10008.6.1.1287", "Generic Anatomic Location Modifier (212)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Beam Limiting Device Type (9541)</summary>
        public static readonly DicomUID BeamLimitingDeviceType9541 = new DicomUID("1.2.840.10008.6.1.1288", "Beam Limiting Device Type (9541)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Compensator Device Type (9542)</summary>
        public static readonly DicomUID CompensatorDeviceType9542 = new DicomUID("1.2.840.10008.6.1.1289", "Compensator Device Type (9542)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Machine Mode (9543)</summary>
        public static readonly DicomUID RadiotherapyTreatmentMachineMode9543 = new DicomUID("1.2.840.10008.6.1.1290", "Radiotherapy Treatment Machine Mode (9543)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Distance Reference Location (9544)</summary>
        public static readonly DicomUID RadiotherapyDistanceReferenceLocation9544 = new DicomUID("1.2.840.10008.6.1.1291", "Radiotherapy Distance Reference Location (9544)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fixed Beam Limiting Device Type (9545)</summary>
        public static readonly DicomUID FixedBeamLimitingDeviceType9545 = new DicomUID("1.2.840.10008.6.1.1292", "Fixed Beam Limiting Device Type (9545)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Wedge Type (9546)</summary>
        public static readonly DicomUID RadiotherapyWedgeType9546 = new DicomUID("1.2.840.10008.6.1.1293", "Radiotherapy Wedge Type (9546)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Beam Limiting Device Orientation Label (9547)</summary>
        public static readonly DicomUID RTBeamLimitingDeviceOrientationLabel9547 = new DicomUID("1.2.840.10008.6.1.1294", "RT Beam Limiting Device Orientation Label (9547)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Accessory Device Type (9548)</summary>
        public static readonly DicomUID GeneralAccessoryDeviceType9548 = new DicomUID("1.2.840.10008.6.1.1295", "General Accessory Device Type (9548)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiation Generation Mode Type (9549)</summary>
        public static readonly DicomUID RadiationGenerationModeType9549 = new DicomUID("1.2.840.10008.6.1.1296", "Radiation Generation Mode Type (9549)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: C-Arm Photon-Electron Delivery Rate Unit (9550)</summary>
        public static readonly DicomUID CArmPhotonElectronDeliveryRateUnit9550 = new DicomUID("1.2.840.10008.6.1.1297", "C-Arm Photon-Electron Delivery Rate Unit (9550)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treatment Delivery Device Type (9551)</summary>
        public static readonly DicomUID TreatmentDeliveryDeviceType9551 = new DicomUID("1.2.840.10008.6.1.1298", "Treatment Delivery Device Type (9551)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: C-Arm Photon-Electron Dosimeter Unit (9552)</summary>
        public static readonly DicomUID CArmPhotonElectronDosimeterUnit9552 = new DicomUID("1.2.840.10008.6.1.1299", "C-Arm Photon-Electron Dosimeter Unit (9552)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treatment Point (9553)</summary>
        public static readonly DicomUID TreatmentPoint9553 = new DicomUID("1.2.840.10008.6.1.1300", "Treatment Point (9553)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Equipment Reference Point (9554)</summary>
        public static readonly DicomUID EquipmentReferencePoint9554 = new DicomUID("1.2.840.10008.6.1.1301", "Equipment Reference Point (9554)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Planning Person Role (9555)</summary>
        public static readonly DicomUID RadiotherapyTreatmentPlanningPersonRole9555 = new DicomUID("1.2.840.10008.6.1.1302", "Radiotherapy Treatment Planning Person Role (9555)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Real Time Video Rendition Title (7070)</summary>
        public static readonly DicomUID RealTimeVideoRenditionTitle7070 = new DicomUID("1.2.840.10008.6.1.1303", "Real Time Video Rendition Title (7070)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Geometry Graphical Representation (219)</summary>
        public static readonly DicomUID GeometryGraphicalRepresentation219 = new DicomUID("1.2.840.10008.6.1.1304", "Geometry Graphical Representation (219)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Visual Explanation (217)</summary>
        public static readonly DicomUID VisualExplanation217 = new DicomUID("1.2.840.10008.6.1.1305", "Visual Explanation (217)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Sector Anatomy from PI-RADS v2.1 (6304)</summary>
        public static readonly DicomUID ProstateSectorAnatomyFromPIRADSV216304 = new DicomUID("1.2.840.10008.6.1.1306", "Prostate Sector Anatomy from PI-RADS v2.1 (6304)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Robotic Node Set (9556)</summary>
        public static readonly DicomUID RadiotherapyRoboticNodeSet9556 = new DicomUID("1.2.840.10008.6.1.1307", "Radiotherapy Robotic Node Set (9556)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tomotherapeutic Dosimeter Unit (9557)</summary>
        public static readonly DicomUID TomotherapeuticDosimeterUnit9557 = new DicomUID("1.2.840.10008.6.1.1308", "Tomotherapeutic Dosimeter Unit (9557)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Tomotherapeutic Dose Rate Unit (9558)</summary>
        public static readonly DicomUID TomotherapeuticDoseRateUnit9558 = new DicomUID("1.2.840.10008.6.1.1309", "Tomotherapeutic Dose Rate Unit (9558)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Robotic Delivery Device Dosimeter Unit (9559)</summary>
        public static readonly DicomUID RoboticDeliveryDeviceDosimeterUnit9559 = new DicomUID("1.2.840.10008.6.1.1310", "Robotic Delivery Device Dosimeter Unit (9559)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Robotic Delivery Device Dose Rate Unit (9560)</summary>
        public static readonly DicomUID RoboticDeliveryDeviceDoseRateUnit9560 = new DicomUID("1.2.840.10008.6.1.1311", "Robotic Delivery Device Dose Rate Unit (9560)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomic Structure (8134)</summary>
        public static readonly DicomUID AnatomicStructure8134 = new DicomUID("1.2.840.10008.6.1.1312", "Anatomic Structure (8134)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mediastinum Finding or Feature (6148)</summary>
        public static readonly DicomUID MediastinumFindingOrFeature6148 = new DicomUID("1.2.840.10008.6.1.1313", "Mediastinum Finding or Feature (6148)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Mediastinum Anatomy (6149)</summary>
        public static readonly DicomUID MediastinumAnatomy6149 = new DicomUID("1.2.840.10008.6.1.1314", "Mediastinum Anatomy (6149)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vascular Ultrasound Report Document Title (12100)</summary>
        public static readonly DicomUID VascularUltrasoundReportDocumentTitle12100 = new DicomUID("1.2.840.10008.6.1.1315", "Vascular Ultrasound Report Document Title (12100)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organ Part (Non-Lateralized) (12130)</summary>
        public static readonly DicomUID OrganPartNonLateralized12130 = new DicomUID("1.2.840.10008.6.1.1316", "Organ Part (Non-Lateralized) (12130)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Organ Part (Lateralized) (12131)</summary>
        public static readonly DicomUID OrganPartLateralized12131 = new DicomUID("1.2.840.10008.6.1.1317", "Organ Part (Lateralized) (12131)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treatment Termination Reason (9561)</summary>
        public static readonly DicomUID TreatmentTerminationReason9561 = new DicomUID("1.2.840.10008.6.1.1318", "Treatment Termination Reason (9561)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Delivery Person Role (9562)</summary>
        public static readonly DicomUID RadiotherapyTreatmentDeliveryPersonRole9562 = new DicomUID("1.2.840.10008.6.1.1319", "Radiotherapy Treatment Delivery Person Role (9562)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Interlock Resolution (9563)</summary>
        public static readonly DicomUID RadiotherapyInterlockResolution9563 = new DicomUID("1.2.840.10008.6.1.1320", "Radiotherapy Interlock Resolution (9563)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treatment Session Confirmation Assertion (9564)</summary>
        public static readonly DicomUID TreatmentSessionConfirmationAssertion9564 = new DicomUID("1.2.840.10008.6.1.1321", "Treatment Session Confirmation Assertion (9564)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Treatment Tolerance Violation Cause (9565)</summary>
        public static readonly DicomUID TreatmentToleranceViolationCause9565 = new DicomUID("1.2.840.10008.6.1.1322", "Treatment Tolerance Violation Cause (9565)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Clinical Tolerance Violation Type (9566)</summary>
        public static readonly DicomUID ClinicalToleranceViolationType9566 = new DicomUID("1.2.840.10008.6.1.1323", "Clinical Tolerance Violation Type (9566)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Machine Tolerance Violation Type (9567)</summary>
        public static readonly DicomUID MachineToleranceViolationType9567 = new DicomUID("1.2.840.10008.6.1.1324", "Machine Tolerance Violation Type (9567)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Treatment Interlock (9568)</summary>
        public static readonly DicomUID RadiotherapyTreatmentInterlock9568 = new DicomUID("1.2.840.10008.6.1.1325", "Radiotherapy Treatment Interlock (9568)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Isocentric Patient Support Position Parameter (9569)</summary>
        public static readonly DicomUID IsocentricPatientSupportPositionParameter9569 = new DicomUID("1.2.840.10008.6.1.1326", "Isocentric Patient Support Position Parameter (9569)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Overridden Treatment Parameter (9570)</summary>
        public static readonly DicomUID RTOverriddenTreatmentParameter9570 = new DicomUID("1.2.840.10008.6.1.1327", "RT Overridden Treatment Parameter (9570)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EEG Lead (3030)</summary>
        public static readonly DicomUID EEGLead3030 = new DicomUID("1.2.840.10008.6.1.1328", "EEG Lead (3030)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lead Location Near or in Muscle (3031)</summary>
        public static readonly DicomUID LeadLocationNearOrInMuscle3031 = new DicomUID("1.2.840.10008.6.1.1329", "Lead Location Near or in Muscle (3031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lead Location Near Peripheral Nerve (3032)</summary>
        public static readonly DicomUID LeadLocationNearPeripheralNerve3032 = new DicomUID("1.2.840.10008.6.1.1330", "Lead Location Near Peripheral Nerve (3032)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EOG Lead (3033)</summary>
        public static readonly DicomUID EOGLead3033 = new DicomUID("1.2.840.10008.6.1.1331", "EOG Lead (3033)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Body Position Channel (3034)</summary>
        public static readonly DicomUID BodyPositionChannel3034 = new DicomUID("1.2.840.10008.6.1.1332", "Body Position Channel (3034)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EEG Annotation – Neurophysiologic Enumeration (3035)</summary>
        public static readonly DicomUID EEGAnnotationNeurophysiologicEnumeration3035 = new DicomUID("1.2.840.10008.6.1.1333", "EEG Annotation – Neurophysiologic Enumeration (3035)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EMG Annotation – Neurophysiological Enumeration (3036)</summary>
        public static readonly DicomUID EMGAnnotationNeurophysiologicalEnumeration3036 = new DicomUID("1.2.840.10008.6.1.1334", "EMG Annotation – Neurophysiological Enumeration (3036)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EOG Annotation – Neurophysiological Enumeration (3037)</summary>
        public static readonly DicomUID EOGAnnotationNeurophysiologicalEnumeration3037 = new DicomUID("1.2.840.10008.6.1.1335", "EOG Annotation – Neurophysiological Enumeration (3037)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Pattern Event (3038)</summary>
        public static readonly DicomUID PatternEvent3038 = new DicomUID("1.2.840.10008.6.1.1336", "Pattern Event (3038)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Device-related and Environment-related Event (3039)</summary>
        public static readonly DicomUID DeviceRelatedAndEnvironmentRelatedEvent3039 = new DicomUID("1.2.840.10008.6.1.1337", "Device-related and Environment-related Event (3039)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EEG Annotation - Neurological Monitoring Measurement (3040)</summary>
        public static readonly DicomUID EEGAnnotationNeurologicalMonitoringMeasurement3040 = new DicomUID("1.2.840.10008.6.1.1338", "EEG Annotation - Neurological Monitoring Measurement (3040)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Ultrasound Report Document Title (12024)</summary>
        public static readonly DicomUID OBGYNUltrasoundReportDocumentTitle12024 = new DicomUID("1.2.840.10008.6.1.1339", "OB-GYN Ultrasound Report Document Title (12024)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Automation of Measurement (7230)</summary>
        public static readonly DicomUID AutomationOfMeasurement7230 = new DicomUID("1.2.840.10008.6.1.1340", "Automation of Measurement (7230)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: OB-GYN Ultrasound Beam Path (12025)</summary>
        public static readonly DicomUID OBGYNUltrasoundBeamPath12025 = new DicomUID("1.2.840.10008.6.1.1341", "OB-GYN Ultrasound Beam Path (12025)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Angle Measurement (7550)</summary>
        public static readonly DicomUID AngleMeasurement7550 = new DicomUID("1.2.840.10008.6.1.1342", "Angle Measurement (7550)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Generic Purpose of Reference to Images and Coordinates in Measurement (7551)</summary>
        public static readonly DicomUID GenericPurposeOfReferenceToImagesAndCoordinatesInMeasurement7551 = new DicomUID("1.2.840.10008.6.1.1343", "Generic Purpose of Reference to Images and Coordinates in Measurement (7551)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Generic Purpose of Reference to Images in Measurement (7552)</summary>
        public static readonly DicomUID GenericPurposeOfReferenceToImagesInMeasurement7552 = new DicomUID("1.2.840.10008.6.1.1344", "Generic Purpose of Reference to Images in Measurement (7552)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Generic Purpose of Reference to Coordinates in Measurement (7553)</summary>
        public static readonly DicomUID GenericPurposeOfReferenceToCoordinatesInMeasurement7553 = new DicomUID("1.2.840.10008.6.1.1345", "Generic Purpose of Reference to Coordinates in Measurement (7553)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Fitzpatrick Skin Type (4401)</summary>
        public static readonly DicomUID FitzpatrickSkinType4401 = new DicomUID("1.2.840.10008.6.1.1346", "Fitzpatrick Skin Type (4401)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: History of Malignant Melanoma (4402)</summary>
        public static readonly DicomUID HistoryOfMalignantMelanoma4402 = new DicomUID("1.2.840.10008.6.1.1347", "History of Malignant Melanoma (4402)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: History of Melanoma in Situ (4403)</summary>
        public static readonly DicomUID HistoryOfMelanomaInSitu4403 = new DicomUID("1.2.840.10008.6.1.1348", "History of Melanoma in Situ (4403)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: History of Non-Melanoma Skin Cancer (4404)</summary>
        public static readonly DicomUID HistoryOfNonMelanomaSkinCancer4404 = new DicomUID("1.2.840.10008.6.1.1349", "History of Non-Melanoma Skin Cancer (4404)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Skin Disorder (4405)</summary>
        public static readonly DicomUID SkinDisorder4405 = new DicomUID("1.2.840.10008.6.1.1350", "Skin Disorder (4405)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Reported Lesion Characteristic (4406)</summary>
        public static readonly DicomUID PatientReportedLesionCharacteristic4406 = new DicomUID("1.2.840.10008.6.1.1351", "Patient Reported Lesion Characteristic (4406)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Palpation Finding (4407)</summary>
        public static readonly DicomUID LesionPalpationFinding4407 = new DicomUID("1.2.840.10008.6.1.1352", "Lesion Palpation Finding (4407)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Visual Finding (4408)</summary>
        public static readonly DicomUID LesionVisualFinding4408 = new DicomUID("1.2.840.10008.6.1.1353", "Lesion Visual Finding (4408)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Skin Procedure (4409)</summary>
        public static readonly DicomUID SkinProcedure4409 = new DicomUID("1.2.840.10008.6.1.1354", "Skin Procedure (4409)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominopelvic Vessel (12125)</summary>
        public static readonly DicomUID AbdominopelvicVessel12125 = new DicomUID("1.2.840.10008.6.1.1355", "Abdominopelvic Vessel (12125)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Value Failure Qualifier (43)</summary>
        public static readonly DicomUID NumericValueFailureQualifier43 = new DicomUID("1.2.840.10008.6.1.1356", "Numeric Value Failure Qualifier (43)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Numeric Value Unknown Qualifier (44)</summary>
        public static readonly DicomUID NumericValueUnknownQualifier44 = new DicomUID("1.2.840.10008.6.1.1357", "Numeric Value Unknown Qualifier (44)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Couinaud Liver Segment (7170)</summary>
        public static readonly DicomUID CouinaudLiverSegment7170 = new DicomUID("1.2.840.10008.6.1.1358", "Couinaud Liver Segment (7170)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Liver Segmentation Type (7171)</summary>
        public static readonly DicomUID LiverSegmentationType7171 = new DicomUID("1.2.840.10008.6.1.1359", "Liver Segmentation Type (7171)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Contraindications For XA Imaging (1201)</summary>
        public static readonly DicomUID ContraindicationsForXAImaging1201 = new DicomUID("1.2.840.10008.6.1.1360", "Contraindications For XA Imaging (1201)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Neurophysiologic Stimulation Mode (3041)</summary>
        public static readonly DicomUID NeurophysiologicStimulationMode3041 = new DicomUID("1.2.840.10008.6.1.1361", "Neurophysiologic Stimulation Mode (3041)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reported Value Type (10072)</summary>
        public static readonly DicomUID ReportedValueType10072 = new DicomUID("1.2.840.10008.6.1.1362", "Reported Value Type (10072)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Value Timing (10073)</summary>
        public static readonly DicomUID ValueTiming10073 = new DicomUID("1.2.840.10008.6.1.1363", "Value Timing (10073)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RDSR Frame of Reference Origin (10074)</summary>
        public static readonly DicomUID RDSRFrameOfReferenceOrigin10074 = new DicomUID("1.2.840.10008.6.1.1364", "RDSR Frame of Reference Origin (10074)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Microscopy Annotation Property Type (8135)</summary>
        public static readonly DicomUID MicroscopyAnnotationPropertyType8135 = new DicomUID("1.2.840.10008.6.1.1365", "Microscopy Annotation Property Type (8135)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Microscopy Measurement Type (8136)</summary>
        public static readonly DicomUID MicroscopyMeasurementType8136 = new DicomUID("1.2.840.10008.6.1.1366", "Microscopy Measurement Type (8136)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Reporting System (6310)</summary>
        public static readonly DicomUID ProstateReportingSystem6310 = new DicomUID("1.2.840.10008.6.1.1367", "Prostate Reporting System (6310)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Signal Intensity (6311)</summary>
        public static readonly DicomUID MRSignalIntensity6311 = new DicomUID("1.2.840.10008.6.1.1368", "MR Signal Intensity (6311)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Cross-sectional Scan Plane Orientation (6312)</summary>
        public static readonly DicomUID CrossSectionalScanPlaneOrientation6312 = new DicomUID("1.2.840.10008.6.1.1369", "Cross-sectional Scan Plane Orientation (6312)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: History of Prostate Disease (6313)</summary>
        public static readonly DicomUID HistoryOfProstateDisease6313 = new DicomUID("1.2.840.10008.6.1.1370", "History of Prostate Disease (6313)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Study Quality Finding (6314)</summary>
        public static readonly DicomUID ProstateMRIStudyQualityFinding6314 = new DicomUID("1.2.840.10008.6.1.1371", "Prostate MRI Study Quality Finding (6314)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Series Quality Finding (6315)</summary>
        public static readonly DicomUID ProstateMRISeriesQualityFinding6315 = new DicomUID("1.2.840.10008.6.1.1372", "Prostate MRI Series Quality Finding (6315)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Imaging Artifact (6316)</summary>
        public static readonly DicomUID MRImagingArtifact6316 = new DicomUID("1.2.840.10008.6.1.1373", "MR Imaging Artifact (6316)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate DCE MRI Quality Finding (6317)</summary>
        public static readonly DicomUID ProstateDCEMRIQualityFinding6317 = new DicomUID("1.2.840.10008.6.1.1374", "Prostate DCE MRI Quality Finding (6317)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate DWI MRI Quality Finding (6318)</summary>
        public static readonly DicomUID ProstateDWIMRIQualityFinding6318 = new DicomUID("1.2.840.10008.6.1.1375", "Prostate DWI MRI Quality Finding (6318)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominal Intervention Type (6319)</summary>
        public static readonly DicomUID AbdominalInterventionType6319 = new DicomUID("1.2.840.10008.6.1.1376", "Abdominal Intervention Type (6319)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Abdominopelvic Intervention (6320)</summary>
        public static readonly DicomUID AbdominopelvicIntervention6320 = new DicomUID("1.2.840.10008.6.1.1377", "Abdominopelvic Intervention (6320)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Cancer Diagnostic Procedure (6321)</summary>
        public static readonly DicomUID ProstateCancerDiagnosticProcedure6321 = new DicomUID("1.2.840.10008.6.1.1378", "Prostate Cancer Diagnostic Procedure (6321)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Cancer Family History (6322)</summary>
        public static readonly DicomUID ProstateCancerFamilyHistory6322 = new DicomUID("1.2.840.10008.6.1.1379", "Prostate Cancer Family History (6322)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Cancer Therapy (6323)</summary>
        public static readonly DicomUID ProstateCancerTherapy6323 = new DicomUID("1.2.840.10008.6.1.1380", "Prostate Cancer Therapy (6323)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Assessment (6324)</summary>
        public static readonly DicomUID ProstateMRIAssessment6324 = new DicomUID("1.2.840.10008.6.1.1381", "Prostate MRI Assessment (6324)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Overall Assessment from PI-RADS® (6325)</summary>
        public static readonly DicomUID OverallAssessmentFromPIRADS6325 = new DicomUID("1.2.840.10008.6.1.1382", "Overall Assessment from PI-RADS® (6325)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Image Quality Control Standard (6326)</summary>
        public static readonly DicomUID ImageQualityControlStandard6326 = new DicomUID("1.2.840.10008.6.1.1383", "Image Quality Control Standard (6326)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Imaging Indication (6327)</summary>
        public static readonly DicomUID ProstateImagingIndication6327 = new DicomUID("1.2.840.10008.6.1.1384", "Prostate Imaging Indication (6327)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PI-RADS® v2 Lesion Assessment Category (6328)</summary>
        public static readonly DicomUID PIRADSV2LesionAssessmentCategory6328 = new DicomUID("1.2.840.10008.6.1.1385", "PI-RADS® v2 Lesion Assessment Category (6328)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PI-RADS® v2 T2WI PZ Lesion Assessment Category (6329)</summary>
        public static readonly DicomUID PIRADSV2T2WIPZLesionAssessmentCategory6329 = new DicomUID("1.2.840.10008.6.1.1386", "PI-RADS® v2 T2WI PZ Lesion Assessment Category (6329)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PI-RADS® v2 T2WI TZ Lesion Assessment Category (6330)</summary>
        public static readonly DicomUID PIRADSV2T2WITZLesionAssessmentCategory6330 = new DicomUID("1.2.840.10008.6.1.1387", "PI-RADS® v2 T2WI TZ Lesion Assessment Category (6330)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PI-RADS® v2 DWI Lesion Assessment Category (6331)</summary>
        public static readonly DicomUID PIRADSV2DWILesionAssessmentCategory6331 = new DicomUID("1.2.840.10008.6.1.1388", "PI-RADS® v2 DWI Lesion Assessment Category (6331)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: PI-RADS® v2 DCE Lesion Assessment Category (6332)</summary>
        public static readonly DicomUID PIRADSV2DCELesionAssessmentCategory6332 = new DicomUID("1.2.840.10008.6.1.1389", "PI-RADS® v2 DCE Lesion Assessment Category (6332)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: mpMRI Assessment Type (6333)</summary>
        public static readonly DicomUID mpMRIAssessmentType6333 = new DicomUID("1.2.840.10008.6.1.1390", "mpMRI Assessment Type (6333)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: mpMRI Assessment Type from PI-RADS® (6334)</summary>
        public static readonly DicomUID mpMRIAssessmentTypeFromPIRADS6334 = new DicomUID("1.2.840.10008.6.1.1391", "mpMRI Assessment Type from PI-RADS® (6334)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: mpMRI Assessment Value (6335)</summary>
        public static readonly DicomUID mpMRIAssessmentValue6335 = new DicomUID("1.2.840.10008.6.1.1392", "mpMRI Assessment Value (6335)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MRI Abnormality (6336)</summary>
        public static readonly DicomUID MRIAbnormality6336 = new DicomUID("1.2.840.10008.6.1.1393", "MRI Abnormality (6336)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: mpMRI Prostate Abnormality from PI-RADS® (6337)</summary>
        public static readonly DicomUID mpMRIProstateAbnormalityFromPIRADS6337 = new DicomUID("1.2.840.10008.6.1.1394", "mpMRI Prostate Abnormality from PI-RADS® (6337)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: mpMRI Benign Prostate Abnormality from PI-RADS® (6338)</summary>
        public static readonly DicomUID mpMRIBenignProstateAbnormalityFromPIRADS6338 = new DicomUID("1.2.840.10008.6.1.1395", "mpMRI Benign Prostate Abnormality from PI-RADS® (6338)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MRI Shape Characteristic (6339)</summary>
        public static readonly DicomUID MRIShapeCharacteristic6339 = new DicomUID("1.2.840.10008.6.1.1396", "MRI Shape Characteristic (6339)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Shape Characteristic from PI-RADS® (6340)</summary>
        public static readonly DicomUID ProstateMRIShapeCharacteristicFromPIRADS6340 = new DicomUID("1.2.840.10008.6.1.1397", "Prostate MRI Shape Characteristic from PI-RADS® (6340)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MRI Margin Characteristic (6341)</summary>
        public static readonly DicomUID MRIMarginCharacteristic6341 = new DicomUID("1.2.840.10008.6.1.1398", "MRI Margin Characteristic (6341)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Margin Characteristic from PI-RADS® (6342)</summary>
        public static readonly DicomUID ProstateMRIMarginCharacteristicFromPIRADS6342 = new DicomUID("1.2.840.10008.6.1.1399", "Prostate MRI Margin Characteristic from PI-RADS® (6342)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MRI Signal Characteristic (6343)</summary>
        public static readonly DicomUID MRISignalCharacteristic6343 = new DicomUID("1.2.840.10008.6.1.1400", "MRI Signal Characteristic (6343)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Signal Characteristic from PI-RADS® (6344)</summary>
        public static readonly DicomUID ProstateMRISignalCharacteristicFromPIRADS6344 = new DicomUID("1.2.840.10008.6.1.1401", "Prostate MRI Signal Characteristic from PI-RADS® (6344)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MRI Enhancement Pattern (6345)</summary>
        public static readonly DicomUID MRIEnhancementPattern6345 = new DicomUID("1.2.840.10008.6.1.1402", "MRI Enhancement Pattern (6345)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Enhancement Pattern from PI-RADS® (6346)</summary>
        public static readonly DicomUID ProstateMRIEnhancementPatternFromPIRADS6346 = new DicomUID("1.2.840.10008.6.1.1403", "Prostate MRI Enhancement Pattern from PI-RADS® (6346)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Extra-prostatic Finding (6347)</summary>
        public static readonly DicomUID ProstateMRIExtraProstaticFinding6347 = new DicomUID("1.2.840.10008.6.1.1404", "Prostate MRI Extra-prostatic Finding (6347)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate MRI Assessment of Extra-prostatic Anatomic Site (6348)</summary>
        public static readonly DicomUID ProstateMRIAssessmentOfExtraProstaticAnatomicSite6348 = new DicomUID("1.2.840.10008.6.1.1405", "Prostate MRI Assessment of Extra-prostatic Anatomic Site (6348)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MR Coil Type (6349)</summary>
        public static readonly DicomUID MRCoilType6349 = new DicomUID("1.2.840.10008.6.1.1406", "MR Coil Type (6349)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Endorectal Coil Fill Substance (6350)</summary>
        public static readonly DicomUID EndorectalCoilFillSubstance6350 = new DicomUID("1.2.840.10008.6.1.1407", "Endorectal Coil Fill Substance (6350)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Relational Measurement (6351)</summary>
        public static readonly DicomUID ProstateRelationalMeasurement6351 = new DicomUID("1.2.840.10008.6.1.1408", "Prostate Relational Measurement (6351)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Cancer Diagnostic Blood Lab Measurement (6352)</summary>
        public static readonly DicomUID ProstateCancerDiagnosticBloodLabMeasurement6352 = new DicomUID("1.2.840.10008.6.1.1409", "Prostate Cancer Diagnostic Blood Lab Measurement (6352)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Prostate Imaging Types of Quality Control Standard (6353)</summary>
        public static readonly DicomUID ProstateImagingTypesOfQualityControlStandard6353 = new DicomUID("1.2.840.10008.6.1.1410", "Prostate Imaging Types of Quality Control Standard (6353)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Shear Wave Measurement (12308)</summary>
        public static readonly DicomUID UltrasoundShearWaveMeasurement12308 = new DicomUID("1.2.840.10008.6.1.1411", "Ultrasound Shear Wave Measurement (12308)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Myocardial Wall 16 Segment Model (Retired) (3780)</summary>
        public static readonly DicomUID LeftVentricleMyocardialWall16SegmentModel3780RETIRED = new DicomUID("1.2.840.10008.6.1.1412", "Left Ventricle Myocardial Wall 16 Segment Model (Retired) (3780)", DicomUidType.ContextGroupName, true);

        ///<summary>Context Group Name: Left Ventricle Myocardial Wall 18 Segment Model (3781)</summary>
        public static readonly DicomUID LeftVentricleMyocardialWall18SegmentModel3781 = new DicomUID("1.2.840.10008.6.1.1413", "Left Ventricle Myocardial Wall 18 Segment Model (3781)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Basal Wall 6 Segments (3782)</summary>
        public static readonly DicomUID LeftVentricleBasalWall6Segments3782 = new DicomUID("1.2.840.10008.6.1.1414", "Left Ventricle Basal Wall 6 Segments (3782)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Midlevel Wall 6 Segments (3783)</summary>
        public static readonly DicomUID LeftVentricleMidlevelWall6Segments3783 = new DicomUID("1.2.840.10008.6.1.1415", "Left Ventricle Midlevel Wall 6 Segments (3783)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Apical Wall 4 Segments (3784)</summary>
        public static readonly DicomUID LeftVentricleApicalWall4Segments3784 = new DicomUID("1.2.840.10008.6.1.1416", "Left Ventricle Apical Wall 4 Segments (3784)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Left Ventricle Apical Wall 6 Segments (3785)</summary>
        public static readonly DicomUID LeftVentricleApicalWall6Segments3785 = new DicomUID("1.2.840.10008.6.1.1417", "Left Ventricle Apical Wall 6 Segments (3785)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Treatment Preparation Method (9571)</summary>
        public static readonly DicomUID PatientTreatmentPreparationMethod9571 = new DicomUID("1.2.840.10008.6.1.1418", "Patient Treatment Preparation Method (9571)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Shielding Device (9572)</summary>
        public static readonly DicomUID PatientShieldingDevice9572 = new DicomUID("1.2.840.10008.6.1.1419", "Patient Shielding Device (9572)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Treatment Preparation Device (9573)</summary>
        public static readonly DicomUID PatientTreatmentPreparationDevice9573 = new DicomUID("1.2.840.10008.6.1.1420", "Patient Treatment Preparation Device (9573)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Position Displacement Reference Point (9574)</summary>
        public static readonly DicomUID PatientPositionDisplacementReferencePoint9574 = new DicomUID("1.2.840.10008.6.1.1421", "Patient Position Displacement Reference Point (9574)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Alignment Device (9575)</summary>
        public static readonly DicomUID PatientAlignmentDevice9575 = new DicomUID("1.2.840.10008.6.1.1422", "Patient Alignment Device (9575)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reasons for RT Radiation Treatment Omission (9576)</summary>
        public static readonly DicomUID ReasonsForRTRadiationTreatmentOmission9576 = new DicomUID("1.2.840.10008.6.1.1423", "Reasons for RT Radiation Treatment Omission (9576)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Treatment Preparation Procedure (9577)</summary>
        public static readonly DicomUID PatientTreatmentPreparationProcedure9577 = new DicomUID("1.2.840.10008.6.1.1424", "Patient Treatment Preparation Procedure (9577)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Motion Management Setup Device (9578)</summary>
        public static readonly DicomUID MotionManagementSetupDevice9578 = new DicomUID("1.2.840.10008.6.1.1425", "Motion Management Setup Device (9578)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Core Echo Strain Measurement (12309)</summary>
        public static readonly DicomUID CoreEchoStrainMeasurement12309 = new DicomUID("1.2.840.10008.6.1.1426", "Core Echo Strain Measurement (12309)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Myocardial Strain Method (12310)</summary>
        public static readonly DicomUID MyocardialStrainMethod12310 = new DicomUID("1.2.840.10008.6.1.1427", "Myocardial Strain Method (12310)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Echo Measured Strain Property (12311)</summary>
        public static readonly DicomUID EchoMeasuredStrainProperty12311 = new DicomUID("1.2.840.10008.6.1.1428", "Echo Measured Strain Property (12311)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Assessment from CAD-RADS™ (3020)</summary>
        public static readonly DicomUID AssessmentFromCADRADS3020 = new DicomUID("1.2.840.10008.6.1.1429", "Assessment from CAD-RADS™ (3020)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD-RADS™ Stenosis Assessment Modifier (3021)</summary>
        public static readonly DicomUID CADRADSStenosisAssessmentModifier3021 = new DicomUID("1.2.840.10008.6.1.1430", "CAD-RADS™ Stenosis Assessment Modifier (3021)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: CAD-RADS™ Assessment Modifier (3022)</summary>
        public static readonly DicomUID CADRADSAssessmentModifier3022 = new DicomUID("1.2.840.10008.6.1.1431", "CAD-RADS™ Assessment Modifier (3022)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Segment Material (9579)</summary>
        public static readonly DicomUID RTSegmentMaterial9579 = new DicomUID("1.2.840.10008.6.1.1432", "RT Segment Material (9579)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vertebral Anatomic Structure (7602)</summary>
        public static readonly DicomUID VertebralAnatomicStructure7602 = new DicomUID("1.2.840.10008.6.1.1433", "Vertebral Anatomic Structure (7602)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Vertebra (7603)</summary>
        public static readonly DicomUID Vertebra7603 = new DicomUID("1.2.840.10008.6.1.1434", "Vertebra (7603)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Intervertebral Disc (7604)</summary>
        public static readonly DicomUID IntervertebralDisc7604 = new DicomUID("1.2.840.10008.6.1.1435", "Intervertebral Disc (7604)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Imaging Procedure (101)</summary>
        public static readonly DicomUID ImagingProcedure101 = new DicomUID("1.2.840.10008.6.1.1436", "Imaging Procedure (101)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: NICIP Short Code Imaging Procedure (103)</summary>
        public static readonly DicomUID NICIPShortCodeImagingProcedure103 = new DicomUID("1.2.840.10008.6.1.1437", "NICIP Short Code Imaging Procedure (103)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: NICIP SNOMED Imaging Procedure (104)</summary>
        public static readonly DicomUID NICIPSNOMEDImagingProcedure104 = new DicomUID("1.2.840.10008.6.1.1438", "NICIP SNOMED Imaging Procedure (104)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ICD-10-PCS Imaging Procedure (105)</summary>
        public static readonly DicomUID ICD10PCSImagingProcedure105 = new DicomUID("1.2.840.10008.6.1.1439", "ICD-10-PCS Imaging Procedure (105)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ICD-10-PCS Nuclear Medicine Procedure (106)</summary>
        public static readonly DicomUID ICD10PCSNuclearMedicineProcedure106 = new DicomUID("1.2.840.10008.6.1.1440", "ICD-10-PCS Nuclear Medicine Procedure (106)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: ICD-10-PCS Radiation Therapy Procedure (107)</summary>
        public static readonly DicomUID ICD10PCSRadiationTherapyProcedure107 = new DicomUID("1.2.840.10008.6.1.1441", "ICD-10-PCS Radiation Therapy Procedure (107)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Segmentation Property Category (9580)</summary>
        public static readonly DicomUID RTSegmentationPropertyCategory9580 = new DicomUID("1.2.840.10008.6.1.1442", "RT Segmentation Property Category (9580)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Registration Mark (9581)</summary>
        public static readonly DicomUID RadiotherapyRegistrationMark9581 = new DicomUID("1.2.840.10008.6.1.1443", "Radiotherapy Registration Mark (9581)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Dose Region (9582)</summary>
        public static readonly DicomUID RadiotherapyDoseRegion9582 = new DicomUID("1.2.840.10008.6.1.1444", "Radiotherapy Dose Region (9582)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Anatomically Localized Lesion Segmentation Type (7199)</summary>
        public static readonly DicomUID AnatomicallyLocalizedLesionSegmentationType7199 = new DicomUID("1.2.840.10008.6.1.1445", "Anatomically Localized Lesion Segmentation Type (7199)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Reason for Removal from Operational Use (7031)</summary>
        public static readonly DicomUID ReasonForRemovalFromOperationalUse7031 = new DicomUID("1.2.840.10008.6.1.1446", "Reason for Removal from Operational Use (7031)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: General Ultrasound Report Document Title (12320)</summary>
        public static readonly DicomUID GeneralUltrasoundReportDocumentTitle12320 = new DicomUID("1.2.840.10008.6.1.1447", "General Ultrasound Report Document Title (12320)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Elastography Site (12321)</summary>
        public static readonly DicomUID ElastographySite12321 = new DicomUID("1.2.840.10008.6.1.1448", "Elastography Site (12321)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Elastography Measurement Site (12322)</summary>
        public static readonly DicomUID ElastographyMeasurementSite12322 = new DicomUID("1.2.840.10008.6.1.1449", "Elastography Measurement Site (12322)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Relevant Patient Condition (12323)</summary>
        public static readonly DicomUID UltrasoundRelevantPatientCondition12323 = new DicomUID("1.2.840.10008.6.1.1450", "Ultrasound Relevant Patient Condition (12323)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Shear Wave Detection Method (12324)</summary>
        public static readonly DicomUID ShearWaveDetectionMethod12324 = new DicomUID("1.2.840.10008.6.1.1451", "Shear Wave Detection Method (12324)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Liver Ultrasound Study Indication (12325)</summary>
        public static readonly DicomUID LiverUltrasoundStudyIndication12325 = new DicomUID("1.2.840.10008.6.1.1452", "Liver Ultrasound Study Indication (12325)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Analog Waveform Filter (3042)</summary>
        public static readonly DicomUID AnalogWaveformFilter3042 = new DicomUID("1.2.840.10008.6.1.1453", "Analog Waveform Filter (3042)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Digital Waveform Filter (3043)</summary>
        public static readonly DicomUID DigitalWaveformFilter3043 = new DicomUID("1.2.840.10008.6.1.1454", "Digital Waveform Filter (3043)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Waveform Filter Lookup Table Input Frequency Unit (3044)</summary>
        public static readonly DicomUID WaveformFilterLookupTableInputFrequencyUnit3044 = new DicomUID("1.2.840.10008.6.1.1455", "Waveform Filter Lookup Table Input Frequency Unit (3044)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Waveform Filter Lookup Table Output Magnitude Unit (3045)</summary>
        public static readonly DicomUID WaveformFilterLookupTableOutputMagnitudeUnit3045 = new DicomUID("1.2.840.10008.6.1.1456", "Waveform Filter Lookup Table Output Magnitude Unit (3045)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specific Observation Subject Class (272)</summary>
        public static readonly DicomUID SpecificObservationSubjectClass272 = new DicomUID("1.2.840.10008.6.1.1457", "Specific Observation Subject Class (272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Movable Beam Limiting Device Type (9540)</summary>
        public static readonly DicomUID MovableBeamLimitingDeviceType9540 = new DicomUID("1.2.840.10008.6.1.1458", "Movable Beam Limiting Device Type (9540)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Radiotherapy Acquisition WorkItem Subtasks (9260)</summary>
        public static readonly DicomUID RadiotherapyAcquisitionWorkItemSubtasks9260 = new DicomUID("1.2.840.10008.6.1.1459", "Radiotherapy Acquisition WorkItem Subtasks (9260)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Position Acquisition Radiation Source Locations (9261)</summary>
        public static readonly DicomUID PatientPositionAcquisitionRadiationSourceLocations9261 = new DicomUID("1.2.840.10008.6.1.1460", "Patient Position Acquisition Radiation Source Locations (9261)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Energy Derivation Types (9262)</summary>
        public static readonly DicomUID EnergyDerivationTypes9262 = new DicomUID("1.2.840.10008.6.1.1461", "Energy Derivation Types (9262)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: KV Imaging Acquisition Techniques (9263)</summary>
        public static readonly DicomUID KVImagingAcquisitionTechniques9263 = new DicomUID("1.2.840.10008.6.1.1462", "KV Imaging Acquisition Techniques (9263)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: MV Imaging Acquisition Techniques (9264)</summary>
        public static readonly DicomUID MVImagingAcquisitionTechniques9264 = new DicomUID("1.2.840.10008.6.1.1463", "MV Imaging Acquisition Techniques (9264)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Position Acquisition - Projection Techniques (9265)</summary>
        public static readonly DicomUID PatientPositionAcquisitionProjectionTechniques9265 = new DicomUID("1.2.840.10008.6.1.1464", "Patient Position Acquisition - Projection Techniques (9265)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Position Acquisition – CT Techniques (9266)</summary>
        public static readonly DicomUID PatientPositionAcquisitionCTTechniques9266 = new DicomUID("1.2.840.10008.6.1.1465", "Patient Position Acquisition – CT Techniques (9266)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Positioning Related Object Purposes (9267)</summary>
        public static readonly DicomUID PatientPositioningRelatedObjectPurposes9267 = new DicomUID("1.2.840.10008.6.1.1466", "Patient Positioning Related Object Purposes (9267)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Position Acquisition Devices (9268)</summary>
        public static readonly DicomUID PatientPositionAcquisitionDevices9268 = new DicomUID("1.2.840.10008.6.1.1467", "Patient Position Acquisition Devices (9268)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Radiation Meterset Units (9269)</summary>
        public static readonly DicomUID RTRadiationMetersetUnits9269 = new DicomUID("1.2.840.10008.6.1.1468", "RT Radiation Meterset Units (9269)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Acquisition Initiation Types (9270)</summary>
        public static readonly DicomUID AcquisitionInitiationTypes9270 = new DicomUID("1.2.840.10008.6.1.1469", "Acquisition Initiation Types (9270)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Image Patient Position Acquisition Devices (9271)</summary>
        public static readonly DicomUID RTImagePatientPositionAcquisitionDevices9271 = new DicomUID("1.2.840.10008.6.1.1470", "RT Image Patient Position Acquisition Devices (9271)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Photoacoustic Illumination Method (11001)</summary>
        public static readonly DicomUID PhotoacousticIlluminationMethod11001 = new DicomUID("1.2.840.10008.6.1.1471", "Photoacoustic Illumination Method (11001)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Acoustic Coupling Medium (11002)</summary>
        public static readonly DicomUID AcousticCouplingMedium11002 = new DicomUID("1.2.840.10008.6.1.1472", "Acoustic Coupling Medium (11002)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Ultrasound Transducer Technology (11003)</summary>
        public static readonly DicomUID UltrasoundTransducerTechnology11003 = new DicomUID("1.2.840.10008.6.1.1473", "Ultrasound Transducer Technology (11003)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Speed of Sound Correction Mechanisms (11004)</summary>
        public static readonly DicomUID SpeedOfSoundCorrectionMechanisms11004 = new DicomUID("1.2.840.10008.6.1.1474", "Speed of Sound Correction Mechanisms (11004)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Photoacoustic Reconstruction Algorithm Family (11005)</summary>
        public static readonly DicomUID PhotoacousticReconstructionAlgorithmFamily11005 = new DicomUID("1.2.840.10008.6.1.1475", "Photoacoustic Reconstruction Algorithm Family (11005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Photoacoustic Imaged Property (11006)</summary>
        public static readonly DicomUID PhotoacousticImagedProperty11006 = new DicomUID("1.2.840.10008.6.1.1476", "Photoacoustic Imaged Property (11006)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: X-Ray Radiation Dose Procedure Type Reported (10005)</summary>
        public static readonly DicomUID XRayRadiationDoseProcedureTypeReported10005 = new DicomUID("1.2.840.10008.6.1.1477", "X-Ray Radiation Dose Procedure Type Reported (10005)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Topical Treatment (4410)</summary>
        public static readonly DicomUID TopicalTreatment4410 = new DicomUID("1.2.840.10008.6.1.1478", "Topical Treatment (4410)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lesion Color (4411)</summary>
        public static readonly DicomUID LesionColor4411 = new DicomUID("1.2.840.10008.6.1.1479", "Lesion Color (4411)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Specimen Stain for Confocal Microscopy (4412)</summary>
        public static readonly DicomUID SpecimenStainForConfocalMicroscopy4412 = new DicomUID("1.2.840.10008.6.1.1480", "Specimen Stain for Confocal Microscopy (4412)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT ROI Image Acquisition Context (9272)</summary>
        public static readonly DicomUID RTROIImageAcquisitionContext9272 = new DicomUID("1.2.840.10008.6.1.1481", "RT ROI Image Acquisition Context (9272)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Lobe of Lung (6170)</summary>
        public static readonly DicomUID LobeOfLung6170 = new DicomUID("1.2.840.10008.6.1.1482", "Lobe of Lung (6170)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Zone of Lung (6171)</summary>
        public static readonly DicomUID ZoneOfLung6171 = new DicomUID("1.2.840.10008.6.1.1483", "Zone of Lung (6171)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Sleep Stage (3046)</summary>
        public static readonly DicomUID SleepStage3046 = new DicomUID("1.2.840.10008.6.1.1484", "Sleep Stage (3046)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Position Acquisition – MR Techniques (9273)</summary>
        public static readonly DicomUID PatientPositionAcquisitionMRTechniques9273 = new DicomUID("1.2.840.10008.6.1.1485", "Patient Position Acquisition – MR Techniques (9273)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: RT Plan Radiotherapy Procedure Technique (9583)</summary>
        public static readonly DicomUID RTPlanRadiotherapyProcedureTechnique9583 = new DicomUID("1.2.840.10008.6.1.1486", "RT Plan Radiotherapy Procedure Technique (9583)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Waveform Annotation Classification (3047)</summary>
        public static readonly DicomUID WaveformAnnotationClassification3047 = new DicomUID("1.2.840.10008.6.1.1487", "Waveform Annotation Classification (3047)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Waveform Annotations Document Title  (3048)</summary>
        public static readonly DicomUID WaveformAnnotationsDocumentTitle3048 = new DicomUID("1.2.840.10008.6.1.1488", "Waveform Annotations Document Title  (3048)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: EEG Procedure  (3049)</summary>
        public static readonly DicomUID EEGProcedure3049 = new DicomUID("1.2.840.10008.6.1.1489", "EEG Procedure  (3049)", DicomUidType.ContextGroupName, false);

        ///<summary>Context Group Name: Patient Consciousness  (3050)</summary>
        public static readonly DicomUID PatientConsciousness3050 = new DicomUID("1.2.840.10008.6.1.1490", "Patient Consciousness  (3050)", DicomUidType.ContextGroupName, false);

    }
}
