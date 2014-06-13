using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public partial class DicomUID {
		private static void LoadInternalUIDs() {
			_uids.Add(DicomUID.Verification.UID, DicomUID.Verification);
			_uids.Add(DicomUID.ImplicitVRLittleEndian.UID, DicomUID.ImplicitVRLittleEndian);
			_uids.Add(DicomUID.ExplicitVRLittleEndian.UID, DicomUID.ExplicitVRLittleEndian);
			_uids.Add(DicomUID.DeflatedExplicitVRLittleEndian.UID, DicomUID.DeflatedExplicitVRLittleEndian);
			_uids.Add(DicomUID.ExplicitVRBigEndian.UID, DicomUID.ExplicitVRBigEndian);
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
			_uids.Add(DicomUID.RLELossless.UID, DicomUID.RLELossless);
			_uids.Add(DicomUID.RFC2557MIMEEncapsulation.UID, DicomUID.RFC2557MIMEEncapsulation);
			_uids.Add(DicomUID.XMLEncoding.UID, DicomUID.XMLEncoding);
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
			_uids.Add(DicomUID.HotIronColorPaletteSOPInstance.UID, DicomUID.HotIronColorPaletteSOPInstance);
			_uids.Add(DicomUID.PETColorPaletteSOPInstance.UID, DicomUID.PETColorPaletteSOPInstance);
			_uids.Add(DicomUID.HotMetalBlueColorPaletteSOPInstance.UID, DicomUID.HotMetalBlueColorPaletteSOPInstance);
			_uids.Add(DicomUID.PET20StepColorPaletteSOPInstance.UID, DicomUID.PET20StepColorPaletteSOPInstance);
			_uids.Add(DicomUID.BasicStudyContentNotificationSOPClassRETIRED.UID, DicomUID.BasicStudyContentNotificationSOPClassRETIRED);
			_uids.Add(DicomUID.StorageCommitmentPushModelSOPClass.UID, DicomUID.StorageCommitmentPushModelSOPClass);
			_uids.Add(DicomUID.StorageCommitmentPushModelSOPInstance.UID, DicomUID.StorageCommitmentPushModelSOPInstance);
			_uids.Add(DicomUID.StorageCommitmentPullModelSOPClass.UID, DicomUID.StorageCommitmentPullModelSOPClass);
			_uids.Add(DicomUID.StorageCommitmentPullModelSOPInstanceRETIRED.UID, DicomUID.StorageCommitmentPullModelSOPInstanceRETIRED);
			_uids.Add(DicomUID.ProceduralEventLoggingSOPClass.UID, DicomUID.ProceduralEventLoggingSOPClass);
			_uids.Add(DicomUID.ProceduralEventLoggingSOPInstance.UID, DicomUID.ProceduralEventLoggingSOPInstance);
			_uids.Add(DicomUID.SubstanceAdministrationLoggingSOPClass.UID, DicomUID.SubstanceAdministrationLoggingSOPClass);
			_uids.Add(DicomUID.SubstanceAdministrationLoggingSOPInstance.UID, DicomUID.SubstanceAdministrationLoggingSOPInstance);
			_uids.Add(DicomUID.DICOMUIDRegistry.UID, DicomUID.DICOMUIDRegistry);
			_uids.Add(DicomUID.DICOMControlledTerminology.UID, DicomUID.DICOMControlledTerminology);
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
			_uids.Add(DicomUID.ComputedRadiographyImageStorage.UID, DicomUID.ComputedRadiographyImageStorage);
			_uids.Add(DicomUID.DigitalXRayImageStorageForPresentation.UID, DicomUID.DigitalXRayImageStorageForPresentation);
			_uids.Add(DicomUID.DigitalXRayImageStorageForProcessing.UID, DicomUID.DigitalXRayImageStorageForProcessing);
			_uids.Add(DicomUID.DigitalMammographyXRayImageStorageForPresentation.UID, DicomUID.DigitalMammographyXRayImageStorageForPresentation);
			_uids.Add(DicomUID.DigitalMammographyXRayImageStorageForProcessing.UID, DicomUID.DigitalMammographyXRayImageStorageForProcessing);
			_uids.Add(DicomUID.DigitalIntraOralXRayImageStorageForPresentation.UID, DicomUID.DigitalIntraOralXRayImageStorageForPresentation);
			_uids.Add(DicomUID.DigitalIntraOralXRayImageStorageForProcessing.UID, DicomUID.DigitalIntraOralXRayImageStorageForProcessing);
			_uids.Add(DicomUID.CTImageStorage.UID, DicomUID.CTImageStorage);
			_uids.Add(DicomUID.EnhancedCTImageStorage.UID, DicomUID.EnhancedCTImageStorage);
			_uids.Add(DicomUID.UltrasoundMultiFrameImageStorageRETIRED.UID, DicomUID.UltrasoundMultiFrameImageStorageRETIRED);
			_uids.Add(DicomUID.UltrasoundMultiFrameImageStorage.UID, DicomUID.UltrasoundMultiFrameImageStorage);
			_uids.Add(DicomUID.MRImageStorage.UID, DicomUID.MRImageStorage);
			_uids.Add(DicomUID.EnhancedMRImageStorage.UID, DicomUID.EnhancedMRImageStorage);
			_uids.Add(DicomUID.MRSpectroscopyStorage.UID, DicomUID.MRSpectroscopyStorage);
			_uids.Add(DicomUID.EnhancedMRColorImageStorage.UID, DicomUID.EnhancedMRColorImageStorage);
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
			_uids.Add(DicomUID.GrayscaleSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.GrayscaleSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.ColorSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.ColorSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.PseudoColorSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.PseudoColorSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.BlendingSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.BlendingSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.XAXRFGrayscaleSoftcopyPresentationStateStorage.UID, DicomUID.XAXRFGrayscaleSoftcopyPresentationStateStorage);
			_uids.Add(DicomUID.XRayAngiographicImageStorage.UID, DicomUID.XRayAngiographicImageStorage);
			_uids.Add(DicomUID.EnhancedXAImageStorage.UID, DicomUID.EnhancedXAImageStorage);
			_uids.Add(DicomUID.XRayRadiofluoroscopicImageStorage.UID, DicomUID.XRayRadiofluoroscopicImageStorage);
			_uids.Add(DicomUID.EnhancedXRFImageStorage.UID, DicomUID.EnhancedXRFImageStorage);
			_uids.Add(DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED.UID, DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED);
			_uids.Add(DicomUID.XRay3DAngiographicImageStorage.UID, DicomUID.XRay3DAngiographicImageStorage);
			_uids.Add(DicomUID.XRay3DCraniofacialImageStorage.UID, DicomUID.XRay3DCraniofacialImageStorage);
			_uids.Add(DicomUID.BreastTomosynthesisImageStorage.UID, DicomUID.BreastTomosynthesisImageStorage);
			_uids.Add(DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForPresentation.UID, DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForPresentation);
			_uids.Add(DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForProcessing.UID, DicomUID.IntravascularOpticalCoherenceTomographyImageStorageForProcessing);
			_uids.Add(DicomUID.NuclearMedicineImageStorage.UID, DicomUID.NuclearMedicineImageStorage);
			_uids.Add(DicomUID.RawDataStorage.UID, DicomUID.RawDataStorage);
			_uids.Add(DicomUID.SpatialRegistrationStorage.UID, DicomUID.SpatialRegistrationStorage);
			_uids.Add(DicomUID.SpatialFiducialsStorage.UID, DicomUID.SpatialFiducialsStorage);
			_uids.Add(DicomUID.DeformableSpatialRegistrationStorage.UID, DicomUID.DeformableSpatialRegistrationStorage);
			_uids.Add(DicomUID.SegmentationStorage.UID, DicomUID.SegmentationStorage);
			_uids.Add(DicomUID.SurfaceSegmentationStorage.UID, DicomUID.SurfaceSegmentationStorage);
			_uids.Add(DicomUID.RealWorldValueMappingStorage.UID, DicomUID.RealWorldValueMappingStorage);
			_uids.Add(DicomUID.VLImageStorageTrial.UID, DicomUID.VLImageStorageTrial);
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
			_uids.Add(DicomUID.TextSRStorageTrialRETIRED.UID, DicomUID.TextSRStorageTrialRETIRED);
			_uids.Add(DicomUID.AudioSRStorageTrialRETIRED.UID, DicomUID.AudioSRStorageTrialRETIRED);
			_uids.Add(DicomUID.DetailSRStorageTrialRETIRED.UID, DicomUID.DetailSRStorageTrialRETIRED);
			_uids.Add(DicomUID.ComprehensiveSRStorageTrialRETIRED.UID, DicomUID.ComprehensiveSRStorageTrialRETIRED);
			_uids.Add(DicomUID.BasicTextSRStorage.UID, DicomUID.BasicTextSRStorage);
			_uids.Add(DicomUID.EnhancedSRStorage.UID, DicomUID.EnhancedSRStorage);
			_uids.Add(DicomUID.ComprehensiveSRStorage.UID, DicomUID.ComprehensiveSRStorage);
			_uids.Add(DicomUID.ProcedureLogStorage.UID, DicomUID.ProcedureLogStorage);
			_uids.Add(DicomUID.MammographyCADSRStorage.UID, DicomUID.MammographyCADSRStorage);
			_uids.Add(DicomUID.KeyObjectSelectionDocumentStorage.UID, DicomUID.KeyObjectSelectionDocumentStorage);
			_uids.Add(DicomUID.ChestCADSRStorage.UID, DicomUID.ChestCADSRStorage);
			_uids.Add(DicomUID.XRayRadiationDoseSRStorage.UID, DicomUID.XRayRadiationDoseSRStorage);
			_uids.Add(DicomUID.ColonCADSRStorage.UID, DicomUID.ColonCADSRStorage);
			_uids.Add(DicomUID.ImplantationPlanSRStorage.UID, DicomUID.ImplantationPlanSRStorage);
			_uids.Add(DicomUID.EncapsulatedPDFStorage.UID, DicomUID.EncapsulatedPDFStorage);
			_uids.Add(DicomUID.EncapsulatedCDAStorage.UID, DicomUID.EncapsulatedCDAStorage);
			_uids.Add(DicomUID.PositronEmissionTomographyImageStorage.UID, DicomUID.PositronEmissionTomographyImageStorage);
			_uids.Add(DicomUID.StandalonePETCurveStorageRETIRED.UID, DicomUID.StandalonePETCurveStorageRETIRED);
			_uids.Add(DicomUID.EnhancedPETImageStorage.UID, DicomUID.EnhancedPETImageStorage);
			_uids.Add(DicomUID.BasicStructuredDisplayStorage.UID, DicomUID.BasicStructuredDisplayStorage);
			_uids.Add(DicomUID.RTImageStorage.UID, DicomUID.RTImageStorage);
			_uids.Add(DicomUID.RTDoseStorage.UID, DicomUID.RTDoseStorage);
			_uids.Add(DicomUID.RTStructureSetStorage.UID, DicomUID.RTStructureSetStorage);
			_uids.Add(DicomUID.RTBeamsTreatmentRecordStorage.UID, DicomUID.RTBeamsTreatmentRecordStorage);
			_uids.Add(DicomUID.RTPlanStorage.UID, DicomUID.RTPlanStorage);
			_uids.Add(DicomUID.RTBrachyTreatmentRecordStorage.UID, DicomUID.RTBrachyTreatmentRecordStorage);
			_uids.Add(DicomUID.RTTreatmentSummaryRecordStorage.UID, DicomUID.RTTreatmentSummaryRecordStorage);
			_uids.Add(DicomUID.RTIonPlanStorage.UID, DicomUID.RTIonPlanStorage);
			_uids.Add(DicomUID.RTIonBeamsTreatmentRecordStorage.UID, DicomUID.RTIonBeamsTreatmentRecordStorage);
			_uids.Add(DicomUID.DICOSCTImageStorage.UID, DicomUID.DICOSCTImageStorage);
			_uids.Add(DicomUID.DICOSDigitalXRayImageStorageForPresentation.UID, DicomUID.DICOSDigitalXRayImageStorageForPresentation);
			_uids.Add(DicomUID.DICOSDigitalXRayImageStorageForProcessing.UID, DicomUID.DICOSDigitalXRayImageStorageForProcessing);
			_uids.Add(DicomUID.DICOSThreatDetectionReportStorage.UID, DicomUID.DICOSThreatDetectionReportStorage);
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
			_uids.Add(DicomUID.ModalityWorklistInformationModelFIND.UID, DicomUID.ModalityWorklistInformationModelFIND);
			_uids.Add(DicomUID.GeneralPurposeWorklistInformationModelFIND.UID, DicomUID.GeneralPurposeWorklistInformationModelFIND);
			_uids.Add(DicomUID.GeneralPurposeScheduledProcedureStepSOPClass.UID, DicomUID.GeneralPurposeScheduledProcedureStepSOPClass);
			_uids.Add(DicomUID.GeneralPurposePerformedProcedureStepSOPClass.UID, DicomUID.GeneralPurposePerformedProcedureStepSOPClass);
			_uids.Add(DicomUID.GeneralPurposeWorklistManagementMetaSOPClass.UID, DicomUID.GeneralPurposeWorklistManagementMetaSOPClass);
			_uids.Add(DicomUID.InstanceAvailabilityNotificationSOPClass.UID, DicomUID.InstanceAvailabilityNotificationSOPClass);
			_uids.Add(DicomUID.RTBeamsDeliveryInstructionStorageTrialRETIRED.UID, DicomUID.RTBeamsDeliveryInstructionStorageTrialRETIRED);
			_uids.Add(DicomUID.RTConventionalMachineVerificationTrialRETIRED.UID, DicomUID.RTConventionalMachineVerificationTrialRETIRED);
			_uids.Add(DicomUID.RTIonMachineVerificationTrialRETIRED.UID, DicomUID.RTIonMachineVerificationTrialRETIRED);
			_uids.Add(DicomUID.UnifiedWorklistAndProcedureStepServiceClassTrialRETIRED.UID, DicomUID.UnifiedWorklistAndProcedureStepServiceClassTrialRETIRED);
			_uids.Add(DicomUID.UnifiedProcedureStepPushSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepPushSOPClassTrialRETIRED);
			_uids.Add(DicomUID.UnifiedProcedureStepWatchSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepWatchSOPClassTrialRETIRED);
			_uids.Add(DicomUID.UnifiedProcedureStepPullSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepPullSOPClassTrialRETIRED);
			_uids.Add(DicomUID.UnifiedProcedureStepEventSOPClassTrialRETIRED.UID, DicomUID.UnifiedProcedureStepEventSOPClassTrialRETIRED);
			_uids.Add(DicomUID.UnifiedWorklistAndProcedureStepSOPInstance.UID, DicomUID.UnifiedWorklistAndProcedureStepSOPInstance);
			_uids.Add(DicomUID.UnifiedWorklistAndProcedureStepServiceClass.UID, DicomUID.UnifiedWorklistAndProcedureStepServiceClass);
			_uids.Add(DicomUID.UnifiedProcedureStepPushSOPClass.UID, DicomUID.UnifiedProcedureStepPushSOPClass);
			_uids.Add(DicomUID.UnifiedProcedureStepWatchSOPClass.UID, DicomUID.UnifiedProcedureStepWatchSOPClass);
			_uids.Add(DicomUID.UnifiedProcedureStepPullSOPClass.UID, DicomUID.UnifiedProcedureStepPullSOPClass);
			_uids.Add(DicomUID.UnifiedProcedureStepEventSOPClass.UID, DicomUID.UnifiedProcedureStepEventSOPClass);
			_uids.Add(DicomUID.RTBeamsDeliveryInstructionStorage.UID, DicomUID.RTBeamsDeliveryInstructionStorage);
			_uids.Add(DicomUID.RTConventionalMachineVerification.UID, DicomUID.RTConventionalMachineVerification);
			_uids.Add(DicomUID.RTIonMachineVerification.UID, DicomUID.RTIonMachineVerification);
			_uids.Add(DicomUID.GeneralRelevantPatientInformationQuery.UID, DicomUID.GeneralRelevantPatientInformationQuery);
			_uids.Add(DicomUID.BreastImagingRelevantPatientInformationQuery.UID, DicomUID.BreastImagingRelevantPatientInformationQuery);
			_uids.Add(DicomUID.CardiacRelevantPatientInformationQuery.UID, DicomUID.CardiacRelevantPatientInformationQuery);
			_uids.Add(DicomUID.HangingProtocolStorage.UID, DicomUID.HangingProtocolStorage);
			_uids.Add(DicomUID.HangingProtocolInformationModelFIND.UID, DicomUID.HangingProtocolInformationModelFIND);
			_uids.Add(DicomUID.HangingProtocolInformationModelMOVE.UID, DicomUID.HangingProtocolInformationModelMOVE);
			_uids.Add(DicomUID.HangingProtocolInformationModelGET.UID, DicomUID.HangingProtocolInformationModelGET);
			_uids.Add(DicomUID.ColorPaletteStorage.UID, DicomUID.ColorPaletteStorage);
			_uids.Add(DicomUID.ColorPaletteInformationModelFIND.UID, DicomUID.ColorPaletteInformationModelFIND);
			_uids.Add(DicomUID.ColorPaletteInformationModelMOVE.UID, DicomUID.ColorPaletteInformationModelMOVE);
			_uids.Add(DicomUID.ColorPaletteInformationModelGET.UID, DicomUID.ColorPaletteInformationModelGET);
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
			_uids.Add(DicomUID.PatientGantryRelationship21.UID, DicomUID.PatientGantryRelationship21);
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
			_uids.Add(DicomUID.CardiologyUnitsOfMeasurement3082.UID, DicomUID.CardiologyUnitsOfMeasurement3082);
			_uids.Add(DicomUID.TimeSynchronizationChannelTypes3090.UID, DicomUID.TimeSynchronizationChannelTypes3090);
			_uids.Add(DicomUID.NMProceduralStateValues3101.UID, DicomUID.NMProceduralStateValues3101);
			_uids.Add(DicomUID.ElectrophysiologyMeasurementFunctionsAndTechniques3240.UID, DicomUID.ElectrophysiologyMeasurementFunctionsAndTechniques3240);
			_uids.Add(DicomUID.HemodynamicMeasurementTechniques3241.UID, DicomUID.HemodynamicMeasurementTechniques3241);
			_uids.Add(DicomUID.CatheterizationProcedurePhase3250.UID, DicomUID.CatheterizationProcedurePhase3250);
			_uids.Add(DicomUID.ElectrophysiologyProcedurePhase3254.UID, DicomUID.ElectrophysiologyProcedurePhase3254);
			_uids.Add(DicomUID.StressProtocols3261.UID, DicomUID.StressProtocols3261);
			_uids.Add(DicomUID.ECGPatientStateValues3262.UID, DicomUID.ECGPatientStateValues3262);
			_uids.Add(DicomUID.ElectrodePlacementValues3263.UID, DicomUID.ElectrodePlacementValues3263);
			_uids.Add(DicomUID.XYZElectrodePlacementValues3264.UID, DicomUID.XYZElectrodePlacementValues3264);
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
			_uids.Add(DicomUID.CircumferentialExtenT3460.UID, DicomUID.CircumferentialExtenT3460);
			_uids.Add(DicomUID.RegionalExtent3461.UID, DicomUID.RegionalExtent3461);
			_uids.Add(DicomUID.ChamberIdentification3462.UID, DicomUID.ChamberIdentification3462);
			_uids.Add(DicomUID.QAReferenceMethodS3465.UID, DicomUID.QAReferenceMethodS3465);
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
			_uids.Add(DicomUID.Diagnosis3673.UID, DicomUID.Diagnosis3673);
			_uids.Add(DicomUID.OtherFilters3675.UID, DicomUID.OtherFilters3675);
			_uids.Add(DicomUID.LeadMeasurementTechnique3676.UID, DicomUID.LeadMeasurementTechnique3676);
			_uids.Add(DicomUID.SummaryCodesECG3677.UID, DicomUID.SummaryCodesECG3677);
			_uids.Add(DicomUID.QTCorrectionAlgorithms3678.UID, DicomUID.QTCorrectionAlgorithms3678);
			_uids.Add(DicomUID.ECGMorphologyDescriptions3679.UID, DicomUID.ECGMorphologyDescriptions3679);
			_uids.Add(DicomUID.ECGLeadNoiseDescriptions3680.UID, DicomUID.ECGLeadNoiseDescriptions3680);
			_uids.Add(DicomUID.ECGLeadNoiseModifiers3681.UID, DicomUID.ECGLeadNoiseModifiers3681);
			_uids.Add(DicomUID.Probability3682.UID, DicomUID.Probability3682);
			_uids.Add(DicomUID.Modifiers3683.UID, DicomUID.Modifiers3683);
			_uids.Add(DicomUID.Trend3684.UID, DicomUID.Trend3684);
			_uids.Add(DicomUID.ConjunctiveTerms3685.UID, DicomUID.ConjunctiveTerms3685);
			_uids.Add(DicomUID.ECGInterpretiveStatements3686.UID, DicomUID.ECGInterpretiveStatements3686);
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
			_uids.Add(DicomUID.CardiacHistoryDatesRetired3720.UID, DicomUID.CardiacHistoryDatesRetired3720);
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
			_uids.Add(DicomUID.CTAndMRAnatomyImaged4030.UID, DicomUID.CTAndMRAnatomyImaged4030);
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
			_uids.Add(DicomUID.QuaLItativeConceptsForUsageExposureFrequency6094.UID, DicomUID.QuaLItativeConceptsForUsageExposureFrequency6094);
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
			_uids.Add(DicomUID.QualitativeTemporalDifferenceType6134.UID, DicomUID.QualitativeTemporalDifferenceType6134);
			_uids.Add(DicomUID.ImageQualityFinding6135.UID, DicomUID.ImageQualityFinding6135);
			_uids.Add(DicomUID.ChestTypesOfQualityControlStandard6136.UID, DicomUID.ChestTypesOfQualityControlStandard6136);
			_uids.Add(DicomUID.TypesOfCADAnalysis6137.UID, DicomUID.TypesOfCADAnalysis6137);
			_uids.Add(DicomUID.ChestNonLesionObjectType6138.UID, DicomUID.ChestNonLesionObjectType6138);
			_uids.Add(DicomUID.NonLesionModifiers6139.UID, DicomUID.NonLesionModifiers6139);
			_uids.Add(DicomUID.CalculationMethods6140.UID, DicomUID.CalculationMethods6140);
			_uids.Add(DicomUID.AttenuationCoefficientMeasurements6141.UID, DicomUID.AttenuationCoefficientMeasurements6141);
			_uids.Add(DicomUID.CalculatedValue6142.UID, DicomUID.CalculatedValue6142);
			_uids.Add(DicomUID.ResponseCriteria6143.UID, DicomUID.ResponseCriteria6143);
			_uids.Add(DicomUID.RECISTResponseCriteria6144.UID, DicomUID.RECISTResponseCriteria6144);
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
			_uids.Add(DicomUID.BrainTissueSegmentationTypes7153.UID, DicomUID.BrainTissueSegmentationTypes7153);
			_uids.Add(DicomUID.AbdominalOrganSegmentationTypes7154.UID, DicomUID.AbdominalOrganSegmentationTypes7154);
			_uids.Add(DicomUID.ThoracicTissueSegmentationTypes7155.UID, DicomUID.ThoracicTissueSegmentationTypes7155);
			_uids.Add(DicomUID.VascularTissueSegmentationTypes7156.UID, DicomUID.VascularTissueSegmentationTypes7156);
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
			_uids.Add(DicomUID.Species7454.UID, DicomUID.Species7454);
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
			_uids.Add(DicomUID.GeneralPurposeWorkitemDefinition9231.UID, DicomUID.GeneralPurposeWorkitemDefinition9231);
			_uids.Add(DicomUID.NonDICOMOutputTypes9232.UID, DicomUID.NonDICOMOutputTypes9232);
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
			_uids.Add(DicomUID.AttenuationCoefficientDescriptors6211.UID, DicomUID.AttenuationCoefficientDescriptors6211);
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
			_uids.Add(DicomUID.CalciumScoringPatientSizeCategories7042.UID, DicomUID.CalciumScoringPatientSizeCategories7042);
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
			_uids.Add(DicomUID.CardiacUltrasoundCommonLinearFlowMeasurements12256.UID, DicomUID.CardiacUltrasoundCommonLinearFlowMeasurements12256);
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
			_uids.Add(DicomUID.CardiacUltrasoundSemilunarValvesAnnulusAndSinusesMeasurements12272.UID, DicomUID.CardiacUltrasoundSemilunarValvesAnnulusAndSinusesMeasurements12272);
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
			_uids.Add(DicomUID.CardiacUltrasoundAtrioventricularValvesFindiingSites12285.UID, DicomUID.CardiacUltrasoundAtrioventricularValvesFindiingSites12285);
			_uids.Add(DicomUID.CardiacUltrasoundInterventricularSeptumFindingSites12286.UID, DicomUID.CardiacUltrasoundInterventricularSeptumFindingSites12286);
			_uids.Add(DicomUID.CardiacUltrasoundVentriclesFindingSites12287.UID, DicomUID.CardiacUltrasoundVentriclesFindingSites12287);
			_uids.Add(DicomUID.CardiacUltrasoundOutflowTractsFindingSites12288.UID, DicomUID.CardiacUltrasoundOutflowTractsFindingSites12288);
			_uids.Add(DicomUID.CardiacUltrasoundSemilunarValvesAnnulusAndSinusesFindingSites12289.UID, DicomUID.CardiacUltrasoundSemilunarValvesAnnulusAndSinusesFindingSites12289);
			_uids.Add(DicomUID.CardiacUltrasoundPulmonaryArteriesFindingSites12290.UID, DicomUID.CardiacUltrasoundPulmonaryArteriesFindingSites12290);
			_uids.Add(DicomUID.CardiacUltrasoundAortaFindingSites12291.UID, DicomUID.CardiacUltrasoundAortaFindingSites12291);
			_uids.Add(DicomUID.CardiacUltrasoundCoronaryArteriesFindingSites12292.UID, DicomUID.CardiacUltrasoundCoronaryArteriesFindingSites12292);
			_uids.Add(DicomUID.CardiacUltrasoundAortoPulmonaryConnectionsFindingSites12293.UID, DicomUID.CardiacUltrasoundAortoPulmonaryConnectionsFindingSites12293);
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
			_uids.Add(DicomUID.OphthalmicAxialLengthQualityMetricType4243.UID, DicomUID.OphthalmicAxialLengthQualityMetricType4243);
			_uids.Add(DicomUID.OphthalmicAgentConcentrationUnits4244.UID, DicomUID.OphthalmicAgentConcentrationUnits4244);
			_uids.Add(DicomUID.FunctionalConditionPresentDuringAcquisition91.UID, DicomUID.FunctionalConditionPresentDuringAcquisition91);
			_uids.Add(DicomUID.JointPositionDuringAcquisition92.UID, DicomUID.JointPositionDuringAcquisition92);
			_uids.Add(DicomUID.JointPositioningMethod93.UID, DicomUID.JointPositioningMethod93);
			_uids.Add(DicomUID.PhysicalForceAppliedDuringAcquisition94.UID, DicomUID.PhysicalForceAppliedDuringAcquisition94);
			_uids.Add(DicomUID.ECGControlVariablesNumeric3690.UID, DicomUID.ECGControlVariablesNumeric3690);
			_uids.Add(DicomUID.ECGControlVariablesText3691.UID, DicomUID.ECGControlVariablesText3691);
			_uids.Add(DicomUID.WSIReferencedImagePurposesOfReference8120.UID, DicomUID.WSIReferencedImagePurposesOfReference8120);
			_uids.Add(DicomUID.WSIMicroscopyLensType8121.UID, DicomUID.WSIMicroscopyLensType8121);
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
			_uids.Add(DicomUID.IntravascularOCTFlushAgent3850.UID, DicomUID.IntravascularOCTFlushAgent3850);
		}
		/// <summary>SOP Class: Verification</summary>
		public readonly static DicomUID Verification = new DicomUID("1.2.840.10008.1.1", "Verification", DicomUidType.SOPClass, false);

		/// <summary>Transfer Syntax: Implicit VR Little Endian</summary>
		public readonly static DicomUID ImplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2", "Implicit VR Little Endian", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: Explicit VR Little Endian</summary>
		public readonly static DicomUID ExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1", "Explicit VR Little Endian", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: Deflated Explicit VR Little Endian</summary>
		public readonly static DicomUID DeflatedExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1.99", "Deflated Explicit VR Little Endian", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: Explicit VR Big Endian</summary>
		public readonly static DicomUID ExplicitVRBigEndian = new DicomUID("1.2.840.10008.1.2.2", "Explicit VR Big Endian", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG Baseline (Process 1)</summary>
		public readonly static DicomUID JPEGBaseline1 = new DicomUID("1.2.840.10008.1.2.4.50", "JPEG Baseline (Process 1)", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG Extended (Process 2 & 4)</summary>
		public readonly static DicomUID JPEGExtended24 = new DicomUID("1.2.840.10008.1.2.4.51", "JPEG Extended (Process 2 & 4)", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG Extended (Process 3 & 5) (Retired)</summary>
		public readonly static DicomUID JPEGExtended35RETIRED = new DicomUID("1.2.840.10008.1.2.4.52", "JPEG Extended (Process 3 & 5) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Non-Hierarchical (Process 6 & 8) (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionNonHierarchical68RETIRED = new DicomUID("1.2.840.10008.1.2.4.53", "JPEG Spectral Selection, Non-Hierarchical (Process 6 & 8) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Non-Hierarchical (Process 7 & 9) (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionNonHierarchical79RETIRED = new DicomUID("1.2.840.10008.1.2.4.54", "JPEG Spectral Selection, Non-Hierarchical (Process 7 & 9) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Full Progression, Non-Hierarchical (Process 10 & 12) (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionNonHierarchical1012RETIRED = new DicomUID("1.2.840.10008.1.2.4.55", "JPEG Full Progression, Non-Hierarchical (Process 10 & 12) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Full Progression, Non-Hierarchical (Process 11 & 13) (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionNonHierarchical1113RETIRED = new DicomUID("1.2.840.10008.1.2.4.56", "JPEG Full Progression, Non-Hierarchical (Process 11 & 13) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical (Process 14)</summary>
		public readonly static DicomUID JPEGLosslessNonHierarchical14 = new DicomUID("1.2.840.10008.1.2.4.57", "JPEG Lossless, Non-Hierarchical (Process 14)", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical (Process 15) (Retired)</summary>
		public readonly static DicomUID JPEGLosslessNonHierarchical15RETIRED = new DicomUID("1.2.840.10008.1.2.4.58", "JPEG Lossless, Non-Hierarchical (Process 15) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Extended, Hierarchical (Process 16 & 18) (Retired)</summary>
		public readonly static DicomUID JPEGExtendedHierarchical1618RETIRED = new DicomUID("1.2.840.10008.1.2.4.59", "JPEG Extended, Hierarchical (Process 16 & 18) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Extended, Hierarchical (Process 17 & 19) (Retired)</summary>
		public readonly static DicomUID JPEGExtendedHierarchical1719RETIRED = new DicomUID("1.2.840.10008.1.2.4.60", "JPEG Extended, Hierarchical (Process 17 & 19) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Hierarchical (Process 20 & 22) (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionHierarchical2022RETIRED = new DicomUID("1.2.840.10008.1.2.4.61", "JPEG Spectral Selection, Hierarchical (Process 20 & 22) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Hierarchical (Process 21 & 23) (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionHierarchical2123RETIRED = new DicomUID("1.2.840.10008.1.2.4.62", "JPEG Spectral Selection, Hierarchical (Process 21 & 23) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Full Progression, Hierarchical (Process 24 & 26) (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionHierarchical2426RETIRED = new DicomUID("1.2.840.10008.1.2.4.63", "JPEG Full Progression, Hierarchical (Process 24 & 26) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Full Progression, Hierarchical (Process 25 & 27) (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionHierarchical2527RETIRED = new DicomUID("1.2.840.10008.1.2.4.64", "JPEG Full Progression, Hierarchical (Process 25 & 27) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Lossless, Hierarchical (Process 28) (Retired)</summary>
		public readonly static DicomUID JPEGLosslessHierarchical28RETIRED = new DicomUID("1.2.840.10008.1.2.4.65", "JPEG Lossless, Hierarchical (Process 28) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Lossless, Hierarchical (Process 29) (Retired)</summary>
		public readonly static DicomUID JPEGLosslessHierarchical29RETIRED = new DicomUID("1.2.840.10008.1.2.4.66", "JPEG Lossless, Hierarchical (Process 29) (Retired)", DicomUidType.TransferSyntax, true);

		/// <summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1])</summary>
		public readonly static DicomUID JPEGLossless = new DicomUID("1.2.840.10008.1.2.4.70", "JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1])", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG-LS Lossless Image Compression</summary>
		public readonly static DicomUID JPEGLSLossless = new DicomUID("1.2.840.10008.1.2.4.80", "JPEG-LS Lossless Image Compression", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG-LS Lossy (Near-Lossless) Image Compression</summary>
		public readonly static DicomUID JPEGLSLossyNearLossless = new DicomUID("1.2.840.10008.1.2.4.81", "JPEG-LS Lossy (Near-Lossless) Image Compression", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG 2000 Image Compression (Lossless Only)</summary>
		public readonly static DicomUID JPEG2000LosslessOnly = new DicomUID("1.2.840.10008.1.2.4.90", "JPEG 2000 Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG 2000 Image Compression</summary>
		public readonly static DicomUID JPEG2000 = new DicomUID("1.2.840.10008.1.2.4.91", "JPEG 2000 Image Compression", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)</summary>
		public readonly static DicomUID JPEG2000Part2MultiComponentLosslessOnly = new DicomUID("1.2.840.10008.1.2.4.92", "JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression</summary>
		public readonly static DicomUID JPEG2000Part2MultiComponent = new DicomUID("1.2.840.10008.1.2.4.93", "JPEG 2000 Part 2 Multi-component Image Compression", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPIP Referenced</summary>
		public readonly static DicomUID JPIPReferenced = new DicomUID("1.2.840.10008.1.2.4.94", "JPIP Referenced", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: JPIP Referenced Deflate</summary>
		public readonly static DicomUID JPIPReferencedDeflate = new DicomUID("1.2.840.10008.1.2.4.95", "JPIP Referenced Deflate", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: MPEG2 Main Profile @ Main Level</summary>
		public readonly static DicomUID MPEG2 = new DicomUID("1.2.840.10008.1.2.4.100", "MPEG2 Main Profile @ Main Level", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: MPEG2 Main Profile @ High Level</summary>
		public readonly static DicomUID MPEG2MainProfileHighLevel = new DicomUID("1.2.840.10008.1.2.4.101", "MPEG2 Main Profile @ High Level", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: MPEG-4 AVC/H.264 High Profile / Level 4.1</summary>
		public readonly static DicomUID MPEG4AVCH264HighProfileLevel41 = new DicomUID("1.2.840.10008.1.2.4.102", "MPEG-4 AVC/H.264 High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1</summary>
		public readonly static DicomUID MPEG4AVCH264BDCompatibleHighProfileLevel41 = new DicomUID("1.2.840.10008.1.2.4.103", "MPEG-4 AVC/H.264 BD-compatible High Profile / Level 4.1", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: RLE Lossless</summary>
		public readonly static DicomUID RLELossless = new DicomUID("1.2.840.10008.1.2.5", "RLE Lossless", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: RFC 2557 MIME encapsulation</summary>
		public readonly static DicomUID RFC2557MIMEEncapsulation = new DicomUID("1.2.840.10008.1.2.6.1", "RFC 2557 MIME encapsulation", DicomUidType.TransferSyntax, false);

		/// <summary>Transfer Syntax: XML Encoding</summary>
		public readonly static DicomUID XMLEncoding = new DicomUID("1.2.840.10008.1.2.6.2", "XML Encoding", DicomUidType.TransferSyntax, false);

		/// <summary>SOP Class: Media Storage Directory Storage</summary>
		public readonly static DicomUID MediaStorageDirectoryStorage = new DicomUID("1.2.840.10008.1.3.10", "Media Storage Directory Storage", DicomUidType.SOPClass, false);

		/// <summary>Well-known frame of reference: Talairach Brain Atlas Frame of Reference</summary>
		public readonly static DicomUID TalairachBrainAtlasFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.1", "Talairach Brain Atlas Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.2", "SPM2 T1 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 T2 Frame of Reference</summary>
		public readonly static DicomUID SPM2T2FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.3", "SPM2 T2 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 PD Frame of Reference</summary>
		public readonly static DicomUID SPM2PDFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.4", "SPM2 PD Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 EPI Frame of Reference</summary>
		public readonly static DicomUID SPM2EPIFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.5", "SPM2 EPI Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 FIL T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2FILT1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.6", "SPM2 FIL T1 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 PET Frame of Reference</summary>
		public readonly static DicomUID SPM2PETFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.7", "SPM2 PET Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 TRANSM Frame of Reference</summary>
		public readonly static DicomUID SPM2TRANSMFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.8", "SPM2 TRANSM Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 SPECT Frame of Reference</summary>
		public readonly static DicomUID SPM2SPECTFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.9", "SPM2 SPECT Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 GRAY Frame of Reference</summary>
		public readonly static DicomUID SPM2GRAYFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.10", "SPM2 GRAY Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 WHITE Frame of Reference</summary>
		public readonly static DicomUID SPM2WHITEFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.11", "SPM2 WHITE Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 CSF Frame of Reference</summary>
		public readonly static DicomUID SPM2CSFFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.12", "SPM2 CSF Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 BRAINMASK Frame of Reference</summary>
		public readonly static DicomUID SPM2BRAINMASKFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.13", "SPM2 BRAINMASK Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 AVG305T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG305T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.14", "SPM2 AVG305T1 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 AVG152T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG152T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.15", "SPM2 AVG152T1 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 AVG152T2 Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG152T2FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.16", "SPM2 AVG152T2 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 AVG152PD Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG152PDFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.17", "SPM2 AVG152PD Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: SPM2 SINGLESUBJT1 Frame of Reference</summary>
		public readonly static DicomUID SPM2SINGLESUBJT1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.18", "SPM2 SINGLESUBJT1 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: ICBM 452 T1 Frame of Reference</summary>
		public readonly static DicomUID ICBM452T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.2.1", "ICBM 452 T1 Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known frame of reference: ICBM Single Subject MRI Frame of Reference</summary>
		public readonly static DicomUID ICBMSingleSubjectMRIFrameOfReference = new DicomUID("1.2.840.10008.1.4.2.2", "ICBM Single Subject MRI Frame of Reference", DicomUidType.FrameOfReference, false);

		/// <summary>Well-known SOP Instance: Hot Iron Color Palette SOP Instance</summary>
		public readonly static DicomUID HotIronColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.1", "Hot Iron Color Palette SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>Well-known SOP Instance: PET Color Palette SOP Instance</summary>
		public readonly static DicomUID PETColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.2", "PET Color Palette SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>Well-known SOP Instance: Hot Metal Blue Color Palette SOP Instance</summary>
		public readonly static DicomUID HotMetalBlueColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.3", "Hot Metal Blue Color Palette SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>Well-known SOP Instance: PET 20 Step Color Palette SOP Instance</summary>
		public readonly static DicomUID PET20StepColorPaletteSOPInstance = new DicomUID("1.2.840.10008.1.5.4", "PET 20 Step Color Palette SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>SOP Class: Basic Study Content Notification SOP Class (Retired)</summary>
		public readonly static DicomUID BasicStudyContentNotificationSOPClassRETIRED = new DicomUID("1.2.840.10008.1.9", "Basic Study Content Notification SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Storage Commitment Push Model SOP Class</summary>
		public readonly static DicomUID StorageCommitmentPushModelSOPClass = new DicomUID("1.2.840.10008.1.20.1", "Storage Commitment Push Model SOP Class", DicomUidType.SOPClass, false);

		/// <summary>Well-known SOP Instance: Storage Commitment Push Model SOP Instance</summary>
		public readonly static DicomUID StorageCommitmentPushModelSOPInstance = new DicomUID("1.2.840.10008.1.20.1.1", "Storage Commitment Push Model SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>SOP Class: Storage Commitment Pull Model SOP Class (Retired)</summary>
		public readonly static DicomUID StorageCommitmentPullModelSOPClass = new DicomUID("1.2.840.10008.1.20.2", "Storage Commitment Pull Model SOP Class (Retired)", DicomUidType.SOPClass, false);

		/// <summary>Well-known SOP Instance: Storage Commitment Pull Model SOP Instance (Retired)</summary>
		public readonly static DicomUID StorageCommitmentPullModelSOPInstanceRETIRED = new DicomUID("1.2.840.10008.1.20.2.1", "Storage Commitment Pull Model SOP Instance (Retired)", DicomUidType.SOPInstance, true);

		/// <summary>SOP Class: Procedural Event Logging SOP Class</summary>
		public readonly static DicomUID ProceduralEventLoggingSOPClass = new DicomUID("1.2.840.10008.1.40", "Procedural Event Logging SOP Class", DicomUidType.SOPClass, false);

		/// <summary>Well-known SOP Instance: Procedural Event Logging SOP Instance</summary>
		public readonly static DicomUID ProceduralEventLoggingSOPInstance = new DicomUID("1.2.840.10008.1.40.1", "Procedural Event Logging SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>SOP Class: Substance Administration Logging SOP Class</summary>
		public readonly static DicomUID SubstanceAdministrationLoggingSOPClass = new DicomUID("1.2.840.10008.1.42", "Substance Administration Logging SOP Class", DicomUidType.SOPClass, false);

		/// <summary>Well-known SOP Instance: Substance Administration Logging SOP Instance</summary>
		public readonly static DicomUID SubstanceAdministrationLoggingSOPInstance = new DicomUID("1.2.840.10008.1.42.1", "Substance Administration Logging SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>DICOM UIDs as a Coding Scheme: DICOM UID Registry</summary>
		public readonly static DicomUID DICOMUIDRegistry = new DicomUID("1.2.840.10008.2.6.1", "DICOM UID Registry", DicomUidType.CodingScheme, false);

		/// <summary>Coding Scheme: DICOM Controlled Terminology</summary>
		public readonly static DicomUID DICOMControlledTerminology = new DicomUID("1.2.840.10008.2.16.4", "DICOM Controlled Terminology", DicomUidType.CodingScheme, false);

		/// <summary>Application Context Name: DICOM Application Context Name</summary>
		public readonly static DicomUID DICOMApplicationContextName = new DicomUID("1.2.840.10008.3.1.1.1", "DICOM Application Context Name", DicomUidType.ApplicationContextName, false);

		/// <summary>SOP Class: Detached Patient Management SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedPatientManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.1", "Detached Patient Management SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Meta SOP Class: Detached Patient Management Meta SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedPatientManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.4", "Detached Patient Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

		/// <summary>SOP Class: Detached Visit Management SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedVisitManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.2.1", "Detached Visit Management SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Detached Study Management SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedStudyManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.1", "Detached Study Management SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Study Component Management SOP Class (Retired)</summary>
		public readonly static DicomUID StudyComponentManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.2", "Study Component Management SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Modality Performed Procedure Step SOP Class</summary>
		public readonly static DicomUID ModalityPerformedProcedureStepSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.3", "Modality Performed Procedure Step SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Modality Performed Procedure Step Retrieve SOP Class</summary>
		public readonly static DicomUID ModalityPerformedProcedureStepRetrieveSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.4", "Modality Performed Procedure Step Retrieve SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Modality Performed Procedure Step Notification SOP Class</summary>
		public readonly static DicomUID ModalityPerformedProcedureStepNotificationSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.5", "Modality Performed Procedure Step Notification SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Detached Results Management SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedResultsManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.1", "Detached Results Management SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Meta SOP Class: Detached Results Management Meta SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedResultsManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.4", "Detached Results Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

		/// <summary>Meta SOP Class: Detached Study Management Meta SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedStudyManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.5", "Detached Study Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

		/// <summary>SOP Class: Detached Interpretation Management SOP Class (Retired)</summary>
		public readonly static DicomUID DetachedInterpretationManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.6.1", "Detached Interpretation Management SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Service Class: Storage Service Class</summary>
		public readonly static DicomUID StorageServiceClass = new DicomUID("1.2.840.10008.4.2", "Storage Service Class", DicomUidType.ServiceClass, false);

		/// <summary>SOP Class: Basic Film Session SOP Class</summary>
		public readonly static DicomUID BasicFilmSessionSOPClass = new DicomUID("1.2.840.10008.5.1.1.1", "Basic Film Session SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Basic Film Box SOP Class</summary>
		public readonly static DicomUID BasicFilmBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.2", "Basic Film Box SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Basic Grayscale Image Box SOP Class</summary>
		public readonly static DicomUID BasicGrayscaleImageBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.4", "Basic Grayscale Image Box SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Basic Color Image Box SOP Class</summary>
		public readonly static DicomUID BasicColorImageBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.4.1", "Basic Color Image Box SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Referenced Image Box SOP Class (Retired)</summary>
		public readonly static DicomUID ReferencedImageBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.4.2", "Referenced Image Box SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Meta SOP Class: Basic Grayscale Print Management Meta SOP Class</summary>
		public readonly static DicomUID BasicGrayscalePrintManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.1.9", "Basic Grayscale Print Management Meta SOP Class", DicomUidType.MetaSOPClass, false);

		/// <summary>Meta SOP Class: Referenced Grayscale Print Management Meta SOP Class (Retired)</summary>
		public readonly static DicomUID ReferencedGrayscalePrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.9.1", "Referenced Grayscale Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

		/// <summary>SOP Class: Print Job SOP Class</summary>
		public readonly static DicomUID PrintJobSOPClass = new DicomUID("1.2.840.10008.5.1.1.14", "Print Job SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Basic Annotation Box SOP Class</summary>
		public readonly static DicomUID BasicAnnotationBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.15", "Basic Annotation Box SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Printer SOP Class</summary>
		public readonly static DicomUID PrinterSOPClass = new DicomUID("1.2.840.10008.5.1.1.16", "Printer SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Printer Configuration Retrieval SOP Class</summary>
		public readonly static DicomUID PrinterConfigurationRetrievalSOPClass = new DicomUID("1.2.840.10008.5.1.1.16.376", "Printer Configuration Retrieval SOP Class", DicomUidType.SOPClass, false);

		/// <summary>Well-known Printer SOP Instance: Printer SOP Instance</summary>
		public readonly static DicomUID PrinterSOPInstance = new DicomUID("1.2.840.10008.5.1.1.17", "Printer SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>Well-known Printer SOP Instance: Printer Configuration Retrieval SOP Instance</summary>
		public readonly static DicomUID PrinterConfigurationRetrievalSOPInstance = new DicomUID("1.2.840.10008.5.1.1.17.376", "Printer Configuration Retrieval SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>Meta SOP Class: Basic Color Print Management Meta SOP Class</summary>
		public readonly static DicomUID BasicColorPrintManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.1.18", "Basic Color Print Management Meta SOP Class", DicomUidType.MetaSOPClass, false);

		/// <summary>Meta SOP Class: Referenced Color Print Management Meta SOP Class (Retired)</summary>
		public readonly static DicomUID ReferencedColorPrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.18.1", "Referenced Color Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

		/// <summary>SOP Class: VOI LUT Box SOP Class</summary>
		public readonly static DicomUID VOILUTBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.22", "VOI LUT Box SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Presentation LUT SOP Class</summary>
		public readonly static DicomUID PresentationLUTSOPClass = new DicomUID("1.2.840.10008.5.1.1.23", "Presentation LUT SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Image Overlay Box SOP Class (Retired)</summary>
		public readonly static DicomUID ImageOverlayBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.24", "Image Overlay Box SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Basic Print Image Overlay Box SOP Class (Retired)</summary>
		public readonly static DicomUID BasicPrintImageOverlayBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.24.1", "Basic Print Image Overlay Box SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Well-known Print Queue SOP Instance: Print Queue SOP Instance (Retired)</summary>
		public readonly static DicomUID PrintQueueSOPInstanceRETIRED = new DicomUID("1.2.840.10008.5.1.1.25", "Print Queue SOP Instance (Retired)", DicomUidType.SOPInstance, true);

		/// <summary>SOP Class: Print Queue Management SOP Class (Retired)</summary>
		public readonly static DicomUID PrintQueueManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.26", "Print Queue Management SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Stored Print Storage SOP Class (Retired)</summary>
		public readonly static DicomUID StoredPrintStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.27", "Stored Print Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Hardcopy Grayscale Image Storage SOP Class (Retired)</summary>
		public readonly static DicomUID HardcopyGrayscaleImageStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.29", "Hardcopy Grayscale Image Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Hardcopy Color Image Storage SOP Class (Retired)</summary>
		public readonly static DicomUID HardcopyColorImageStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.30", "Hardcopy Color Image Storage SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Pull Print Request SOP Class (Retired)</summary>
		public readonly static DicomUID PullPrintRequestSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.31", "Pull Print Request SOP Class (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Meta SOP Class: Pull Stored Print Management Meta SOP Class (Retired)</summary>
		public readonly static DicomUID PullStoredPrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.32", "Pull Stored Print Management Meta SOP Class (Retired)", DicomUidType.MetaSOPClass, true);

		/// <summary>SOP Class: Media Creation Management SOP Class UID</summary>
		public readonly static DicomUID MediaCreationManagementSOPClassUID = new DicomUID("1.2.840.10008.5.1.1.33", "Media Creation Management SOP Class UID", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Computed Radiography Image Storage</summary>
		public readonly static DicomUID ComputedRadiographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.1", "Computed Radiography Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Digital X-Ray Image Storage - For Presentation</summary>
		public readonly static DicomUID DigitalXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.1", "Digital X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Digital X-Ray Image Storage - For Processing</summary>
		public readonly static DicomUID DigitalXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.1.1", "Digital X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Digital Mammography X-Ray Image Storage - For Presentation</summary>
		public readonly static DicomUID DigitalMammographyXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.2", "Digital Mammography X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Digital Mammography X-Ray Image Storage - For Processing</summary>
		public readonly static DicomUID DigitalMammographyXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.2.1", "Digital Mammography X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Digital Intra-oral X-Ray Image Storage - For Presentation</summary>
		public readonly static DicomUID DigitalIntraOralXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.3", "Digital Intra-oral X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Digital Intra-oral X-Ray Image Storage - For Processing</summary>
		public readonly static DicomUID DigitalIntraOralXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.3.1", "Digital Intra-oral X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: CT Image Storage</summary>
		public readonly static DicomUID CTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.2", "CT Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Enhanced CT Image Storage</summary>
		public readonly static DicomUID EnhancedCTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.2.1", "Enhanced CT Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Ultrasound Multi-frame Image Storage (Retired)</summary>
		public readonly static DicomUID UltrasoundMultiFrameImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.3", "Ultrasound Multi-frame Image Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Ultrasound Multi-frame Image Storage</summary>
		public readonly static DicomUID UltrasoundMultiFrameImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.3.1", "Ultrasound Multi-frame Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: MR Image Storage</summary>
		public readonly static DicomUID MRImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4", "MR Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Enhanced MR Image Storage</summary>
		public readonly static DicomUID EnhancedMRImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.1", "Enhanced MR Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: MR Spectroscopy Storage</summary>
		public readonly static DicomUID MRSpectroscopyStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.2", "MR Spectroscopy Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Enhanced MR Color Image Storage</summary>
		public readonly static DicomUID EnhancedMRColorImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.3", "Enhanced MR Color Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Nuclear Medicine Image Storage (Retired)</summary>
		public readonly static DicomUID NuclearMedicineImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.5", "Nuclear Medicine Image Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Ultrasound Image Storage (Retired)</summary>
		public readonly static DicomUID UltrasoundImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.6", "Ultrasound Image Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Ultrasound Image Storage</summary>
		public readonly static DicomUID UltrasoundImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.1", "Ultrasound Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Enhanced US Volume Storage</summary>
		public readonly static DicomUID EnhancedUSVolumeStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.2", "Enhanced US Volume Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Secondary Capture Image Storage</summary>
		public readonly static DicomUID SecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7", "Secondary Capture Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Multi-frame Single Bit Secondary Capture Image Storage</summary>
		public readonly static DicomUID MultiFrameSingleBitSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.1", "Multi-frame Single Bit Secondary Capture Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Multi-frame Grayscale Byte Secondary Capture Image Storage</summary>
		public readonly static DicomUID MultiFrameGrayscaleByteSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.2", "Multi-frame Grayscale Byte Secondary Capture Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Multi-frame Grayscale Word Secondary Capture Image Storage</summary>
		public readonly static DicomUID MultiFrameGrayscaleWordSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.3", "Multi-frame Grayscale Word Secondary Capture Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Multi-frame True Color Secondary Capture Image Storage</summary>
		public readonly static DicomUID MultiFrameTrueColorSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.4", "Multi-frame True Color Secondary Capture Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Standalone Overlay Storage (Retired)</summary>
		public readonly static DicomUID StandaloneOverlayStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.8", "Standalone Overlay Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Standalone Curve Storage (Retired)</summary>
		public readonly static DicomUID StandaloneCurveStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.9", "Standalone Curve Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Waveform Storage - Trial (Retired)</summary>
		public readonly static DicomUID WaveformStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1", "Waveform Storage - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: 12-lead ECG Waveform Storage</summary>
		public readonly static DicomUID TwelveLeadECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.1", "12-lead ECG Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: General ECG Waveform Storage</summary>
		public readonly static DicomUID GeneralECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.2", "General ECG Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Ambulatory ECG Waveform Storage</summary>
		public readonly static DicomUID AmbulatoryECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.3", "Ambulatory ECG Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Hemodynamic Waveform Storage</summary>
		public readonly static DicomUID HemodynamicWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.2.1", "Hemodynamic Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Cardiac Electrophysiology Waveform Storage</summary>
		public readonly static DicomUID CardiacElectrophysiologyWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.3.1", "Cardiac Electrophysiology Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Basic Voice Audio Waveform Storage</summary>
		public readonly static DicomUID BasicVoiceAudioWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.4.1", "Basic Voice Audio Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: General Audio Waveform Storage</summary>
		public readonly static DicomUID GeneralAudioWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.4.2", "General Audio Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Arterial Pulse Waveform Storage</summary>
		public readonly static DicomUID ArterialPulseWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.5.1", "Arterial Pulse Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Respiratory Waveform Storage</summary>
		public readonly static DicomUID RespiratoryWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.6.1", "Respiratory Waveform Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Standalone Modality LUT Storage (Retired)</summary>
		public readonly static DicomUID StandaloneModalityLUTStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.10", "Standalone Modality LUT Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Standalone VOI LUT Storage (Retired)</summary>
		public readonly static DicomUID StandaloneVOILUTStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.11", "Standalone VOI LUT Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Grayscale Softcopy Presentation State Storage SOP Class</summary>
		public readonly static DicomUID GrayscaleSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.1", "Grayscale Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Color Softcopy Presentation State Storage SOP Class</summary>
		public readonly static DicomUID ColorSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.2", "Color Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Pseudo-Color Softcopy Presentation State Storage SOP Class</summary>
		public readonly static DicomUID PseudoColorSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.3", "Pseudo-Color Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Blending Softcopy Presentation State Storage SOP Class</summary>
		public readonly static DicomUID BlendingSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.4", "Blending Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: XA/XRF Grayscale Softcopy Presentation State Storage</summary>
		public readonly static DicomUID XAXRFGrayscaleSoftcopyPresentationStateStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.11.5", "XA/XRF Grayscale Softcopy Presentation State Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: X-Ray Angiographic Image Storage</summary>
		public readonly static DicomUID XRayAngiographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.1", "X-Ray Angiographic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Enhanced XA Image Storage</summary>
		public readonly static DicomUID EnhancedXAImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.1.1", "Enhanced XA Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: X-Ray Radiofluoroscopic Image Storage</summary>
		public readonly static DicomUID XRayRadiofluoroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.2", "X-Ray Radiofluoroscopic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Enhanced XRF Image Storage</summary>
		public readonly static DicomUID EnhancedXRFImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.2.1", "Enhanced XRF Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: X-Ray Angiographic Bi-Plane Image Storage (Retired)</summary>
		public readonly static DicomUID XRayAngiographicBiPlaneImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.12.3", "X-Ray Angiographic Bi-Plane Image Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: X-Ray 3D Angiographic Image Storage</summary>
		public readonly static DicomUID XRay3DAngiographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.1", "X-Ray 3D Angiographic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: X-Ray 3D Craniofacial Image Storage</summary>
		public readonly static DicomUID XRay3DCraniofacialImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.2", "X-Ray 3D Craniofacial Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Breast Tomosynthesis Image Storage</summary>
		public readonly static DicomUID BreastTomosynthesisImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.3", "Breast Tomosynthesis Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Intravascular Optical Coherence Tomography Image Storage - For Presentation</summary>
		public readonly static DicomUID IntravascularOpticalCoherenceTomographyImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.14.1", "Intravascular Optical Coherence Tomography Image Storage - For Presentation", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Intravascular Optical Coherence Tomography Image Storage - For Processing</summary>
		public readonly static DicomUID IntravascularOpticalCoherenceTomographyImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.14.2", "Intravascular Optical Coherence Tomography Image Storage - For Processing", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Nuclear Medicine Image Storage</summary>
		public readonly static DicomUID NuclearMedicineImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.20", "Nuclear Medicine Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Raw Data Storage</summary>
		public readonly static DicomUID RawDataStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66", "Raw Data Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Spatial Registration Storage</summary>
		public readonly static DicomUID SpatialRegistrationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.1", "Spatial Registration Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Spatial Fiducials Storage</summary>
		public readonly static DicomUID SpatialFiducialsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.2", "Spatial Fiducials Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Deformable Spatial Registration Storage</summary>
		public readonly static DicomUID DeformableSpatialRegistrationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.3", "Deformable Spatial Registration Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Segmentation Storage</summary>
		public readonly static DicomUID SegmentationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.4", "Segmentation Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Surface Segmentation Storage</summary>
		public readonly static DicomUID SurfaceSegmentationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.5", "Surface Segmentation Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Real World Value Mapping Storage</summary>
		public readonly static DicomUID RealWorldValueMappingStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.67", "Real World Value Mapping Storage", DicomUidType.SOPClass, false);

		/// <summary>: VL Image Storage - Trial (Retired)</summary>
		public readonly static DicomUID VLImageStorageTrial = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1", "VL Image Storage - Trial (Retired)", DicomUidType.Unknown, false);

		/// <summary>: VL Multi-frame Image Storage - Trial (Retired)</summary>
		public readonly static DicomUID VLMultiFrameImageStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.77.2", "VL Multi-frame Image Storage - Trial (Retired)", DicomUidType.Unknown, true);

		/// <summary>SOP Class: VL Endoscopic Image Storage</summary>
		public readonly static DicomUID VLEndoscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.1", "VL Endoscopic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Video Endoscopic Image Storage</summary>
		public readonly static DicomUID VideoEndoscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.1.1", "Video Endoscopic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: VL Microscopic Image Storage</summary>
		public readonly static DicomUID VLMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.2", "VL Microscopic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Video Microscopic Image Storage</summary>
		public readonly static DicomUID VideoMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.2.1", "Video Microscopic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: VL Slide-Coordinates Microscopic Image Storage</summary>
		public readonly static DicomUID VLSlideCoordinatesMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.3", "VL Slide-Coordinates Microscopic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: VL Photographic Image Storage</summary>
		public readonly static DicomUID VLPhotographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.4", "VL Photographic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Video Photographic Image Storage</summary>
		public readonly static DicomUID VideoPhotographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.4.1", "Video Photographic Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Ophthalmic Photography 8 Bit Image Storage</summary>
		public readonly static DicomUID OphthalmicPhotography8BitImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.1", "Ophthalmic Photography 8 Bit Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Ophthalmic Photography 16 Bit Image Storage</summary>
		public readonly static DicomUID OphthalmicPhotography16BitImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.2", "Ophthalmic Photography 16 Bit Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Stereometric Relationship Storage</summary>
		public readonly static DicomUID StereometricRelationshipStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.3", "Stereometric Relationship Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Ophthalmic Tomography Image Storage</summary>
		public readonly static DicomUID OphthalmicTomographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.4", "Ophthalmic Tomography Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: VL Whole Slide Microscopy Image Storage</summary>
		public readonly static DicomUID VLWholeSlideMicroscopyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.6", "VL Whole Slide Microscopy Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Lensometry Measurements Storage</summary>
		public readonly static DicomUID LensometryMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.1", "Lensometry Measurements Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Autorefraction Measurements Storage</summary>
		public readonly static DicomUID AutorefractionMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.2", "Autorefraction Measurements Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Keratometry Measurements Storage</summary>
		public readonly static DicomUID KeratometryMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.3", "Keratometry Measurements Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Subjective Refraction Measurements Storage</summary>
		public readonly static DicomUID SubjectiveRefractionMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.4", "Subjective Refraction Measurements Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Visual Acuity Measurements Storage</summary>
		public readonly static DicomUID VisualAcuityMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.5", "Visual Acuity Measurements Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Spectacle Prescription Report Storage</summary>
		public readonly static DicomUID SpectaclePrescriptionReportStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.6", "Spectacle Prescription Report Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Ophthalmic Axial Measurements Storage</summary>
		public readonly static DicomUID OphthalmicAxialMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.7", "Ophthalmic Axial Measurements Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Intraocular Lens Calculations Storage</summary>
		public readonly static DicomUID IntraocularLensCalculationsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.78.8", "Intraocular Lens Calculations Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Macular Grid Thickness and Volume Report Storage</summary>
		public readonly static DicomUID MacularGridThicknessAndVolumeReportStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.79.1", "Macular Grid Thickness and Volume Report Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Ophthalmic Visual Field Static Perimetry Measurements Storage</summary>
		public readonly static DicomUID OphthalmicVisualFieldStaticPerimetryMeasurementsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.80.1", "Ophthalmic Visual Field Static Perimetry Measurements Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Text SR Storage - Trial (Retired)</summary>
		public readonly static DicomUID TextSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.1", "Text SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Audio SR Storage - Trial (Retired)</summary>
		public readonly static DicomUID AudioSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.2", "Audio SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Detail SR Storage - Trial (Retired)</summary>
		public readonly static DicomUID DetailSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.3", "Detail SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Comprehensive SR Storage - Trial (Retired)</summary>
		public readonly static DicomUID ComprehensiveSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.4", "Comprehensive SR Storage - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Basic Text SR Storage</summary>
		public readonly static DicomUID BasicTextSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.11", "Basic Text SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Enhanced SR Storage</summary>
		public readonly static DicomUID EnhancedSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.22", "Enhanced SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Comprehensive SR Storage</summary>
		public readonly static DicomUID ComprehensiveSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.33", "Comprehensive SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Procedure Log Storage</summary>
		public readonly static DicomUID ProcedureLogStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.40", "Procedure Log Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Mammography CAD SR Storage</summary>
		public readonly static DicomUID MammographyCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.50", "Mammography CAD SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Key Object Selection Document Storage</summary>
		public readonly static DicomUID KeyObjectSelectionDocumentStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.59", "Key Object Selection Document Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Chest CAD SR Storage</summary>
		public readonly static DicomUID ChestCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.65", "Chest CAD SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: X-Ray Radiation Dose SR Storage</summary>
		public readonly static DicomUID XRayRadiationDoseSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.67", "X-Ray Radiation Dose SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Colon CAD SR Storage</summary>
		public readonly static DicomUID ColonCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.69", "Colon CAD SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implantation Plan SR Storage</summary>
		public readonly static DicomUID ImplantationPlanSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.70", "Implantation Plan SR Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Encapsulated PDF Storage</summary>
		public readonly static DicomUID EncapsulatedPDFStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.1", "Encapsulated PDF Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Encapsulated CDA Storage</summary>
		public readonly static DicomUID EncapsulatedCDAStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.2", "Encapsulated CDA Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Positron Emission Tomography Image Storage</summary>
		public readonly static DicomUID PositronEmissionTomographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.128", "Positron Emission Tomography Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Standalone PET Curve Storage (Retired)</summary>
		public readonly static DicomUID StandalonePETCurveStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.129", "Standalone PET Curve Storage (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Enhanced PET Image Storage</summary>
		public readonly static DicomUID EnhancedPETImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.130", "Enhanced PET Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Basic Structured Display Storage</summary>
		public readonly static DicomUID BasicStructuredDisplayStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.131", "Basic Structured Display Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Image Storage</summary>
		public readonly static DicomUID RTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.1", "RT Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Dose Storage</summary>
		public readonly static DicomUID RTDoseStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.2", "RT Dose Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Structure Set Storage</summary>
		public readonly static DicomUID RTStructureSetStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.3", "RT Structure Set Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Beams Treatment Record Storage</summary>
		public readonly static DicomUID RTBeamsTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.4", "RT Beams Treatment Record Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Plan Storage</summary>
		public readonly static DicomUID RTPlanStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.5", "RT Plan Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Brachy Treatment Record Storage</summary>
		public readonly static DicomUID RTBrachyTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.6", "RT Brachy Treatment Record Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Treatment Summary Record Storage</summary>
		public readonly static DicomUID RTTreatmentSummaryRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.7", "RT Treatment Summary Record Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Ion Plan Storage</summary>
		public readonly static DicomUID RTIonPlanStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.8", "RT Ion Plan Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Ion Beams Treatment Record Storage</summary>
		public readonly static DicomUID RTIonBeamsTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.9", "RT Ion Beams Treatment Record Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: DICOS CT Image Storage</summary>
		public readonly static DicomUID DICOSCTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.1", "DICOS CT Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: DICOS Digital X-Ray Image Storage - For Presentation</summary>
		public readonly static DicomUID DICOSDigitalXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.501.2.1", "DICOS Digital X-Ray Image Storage - For Presentation", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: DICOS Digital X-Ray Image Storage - For Processing</summary>
		public readonly static DicomUID DICOSDigitalXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.501.2.2", "DICOS Digital X-Ray Image Storage - For Processing", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: DICOS Threat Detection Report Storage</summary>
		public readonly static DicomUID DICOSThreatDetectionReportStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.501.3", "DICOS Threat Detection Report Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Eddy Current Image Storage</summary>
		public readonly static DicomUID EddyCurrentImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.601.1", "Eddy Current Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Eddy Current Multi-frame Image Storage</summary>
		public readonly static DicomUID EddyCurrentMultiFrameImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.601.2", "Eddy Current Multi-frame Image Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Patient Root Query/Retrieve Information Model - FIND</summary>
		public readonly static DicomUID PatientRootQueryRetrieveInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.1.2.1.1", "Patient Root Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Patient Root Query/Retrieve Information Model - MOVE</summary>
		public readonly static DicomUID PatientRootQueryRetrieveInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.1.2", "Patient Root Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Patient Root Query/Retrieve Information Model - GET</summary>
		public readonly static DicomUID PatientRootQueryRetrieveInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.1.2.1.3", "Patient Root Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Study Root Query/Retrieve Information Model - FIND</summary>
		public readonly static DicomUID StudyRootQueryRetrieveInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.1.2.2.1", "Study Root Query/Retrieve Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Study Root Query/Retrieve Information Model - MOVE</summary>
		public readonly static DicomUID StudyRootQueryRetrieveInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.2.2", "Study Root Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Study Root Query/Retrieve Information Model - GET</summary>
		public readonly static DicomUID StudyRootQueryRetrieveInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.1.2.2.3", "Study Root Query/Retrieve Information Model - GET", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - FIND (Retired)</summary>
		public readonly static DicomUID PatientStudyOnlyQueryRetrieveInformationModelFINDRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.1", "Patient/Study Only Query/Retrieve Information Model - FIND (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - MOVE (Retired)</summary>
		public readonly static DicomUID PatientStudyOnlyQueryRetrieveInformationModelMOVERETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.2", "Patient/Study Only Query/Retrieve Information Model - MOVE (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - GET (Retired)</summary>
		public readonly static DicomUID PatientStudyOnlyQueryRetrieveInformationModelGETRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.3", "Patient/Study Only Query/Retrieve Information Model - GET (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Composite Instance Root Retrieve - MOVE</summary>
		public readonly static DicomUID CompositeInstanceRootRetrieveMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.4.2", "Composite Instance Root Retrieve - MOVE", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Composite Instance Root Retrieve - GET</summary>
		public readonly static DicomUID CompositeInstanceRootRetrieveGET = new DicomUID("1.2.840.10008.5.1.4.1.2.4.3", "Composite Instance Root Retrieve - GET", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Composite Instance Retrieve Without Bulk Data - GET</summary>
		public readonly static DicomUID CompositeInstanceRetrieveWithoutBulkDataGET = new DicomUID("1.2.840.10008.5.1.4.1.2.5.3", "Composite Instance Retrieve Without Bulk Data - GET", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Modality Worklist Information Model - FIND</summary>
		public readonly static DicomUID ModalityWorklistInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.31", "Modality Worklist Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: General Purpose Worklist Information Model - FIND</summary>
		public readonly static DicomUID GeneralPurposeWorklistInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.32.1", "General Purpose Worklist Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: General Purpose Scheduled Procedure Step SOP Class</summary>
		public readonly static DicomUID GeneralPurposeScheduledProcedureStepSOPClass = new DicomUID("1.2.840.10008.5.1.4.32.2", "General Purpose Scheduled Procedure Step SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: General Purpose Performed Procedure Step SOP Class</summary>
		public readonly static DicomUID GeneralPurposePerformedProcedureStepSOPClass = new DicomUID("1.2.840.10008.5.1.4.32.3", "General Purpose Performed Procedure Step SOP Class", DicomUidType.SOPClass, false);

		/// <summary>Meta SOP Class: General Purpose Worklist Management Meta SOP Class</summary>
		public readonly static DicomUID GeneralPurposeWorklistManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.4.32", "General Purpose Worklist Management Meta SOP Class", DicomUidType.MetaSOPClass, false);

		/// <summary>SOP Class: Instance Availability Notification SOP Class</summary>
		public readonly static DicomUID InstanceAvailabilityNotificationSOPClass = new DicomUID("1.2.840.10008.5.1.4.33", "Instance Availability Notification SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Beams Delivery Instruction Storage - Trial (Retired)</summary>
		public readonly static DicomUID RTBeamsDeliveryInstructionStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.1", "RT Beams Delivery Instruction Storage - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: RT Conventional Machine Verification - Trial (Retired)</summary>
		public readonly static DicomUID RTConventionalMachineVerificationTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.2", "RT Conventional Machine Verification - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: RT Ion Machine Verification - Trial (Retired)</summary>
		public readonly static DicomUID RTIonMachineVerificationTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.3", "RT Ion Machine Verification - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Service Class: Unified Worklist and Procedure Step Service Class - Trial (Retired)</summary>
		public readonly static DicomUID UnifiedWorklistAndProcedureStepServiceClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4", "Unified Worklist and Procedure Step Service Class - Trial (Retired)", DicomUidType.ServiceClass, true);

		/// <summary>SOP Class: Unified Procedure Step - Push SOP Class - Trial (Retired)</summary>
		public readonly static DicomUID UnifiedProcedureStepPushSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.1", "Unified Procedure Step - Push SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Unified Procedure Step - Watch SOP Class - Trial (Retired)</summary>
		public readonly static DicomUID UnifiedProcedureStepWatchSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.2", "Unified Procedure Step - Watch SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Unified Procedure Step - Pull SOP Class - Trial (Retired)</summary>
		public readonly static DicomUID UnifiedProcedureStepPullSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.3", "Unified Procedure Step - Pull SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>SOP Class: Unified Procedure Step - Event SOP Class - Trial (Retired)</summary>
		public readonly static DicomUID UnifiedProcedureStepEventSOPClassTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.34.4.4", "Unified Procedure Step - Event SOP Class - Trial (Retired)", DicomUidType.SOPClass, true);

		/// <summary>Well-known SOP Instance: Unified Worklist and Procedure Step SOP Instance</summary>
		public readonly static DicomUID UnifiedWorklistAndProcedureStepSOPInstance = new DicomUID("1.2.840.10008.5.1.4.34.5", "Unified Worklist and Procedure Step SOP Instance", DicomUidType.SOPInstance, false);

		/// <summary>Service Class: Unified Worklist and Procedure Step Service Class</summary>
		public readonly static DicomUID UnifiedWorklistAndProcedureStepServiceClass = new DicomUID("1.2.840.10008.5.1.4.34.6", "Unified Worklist and Procedure Step Service Class", DicomUidType.ServiceClass, false);

		/// <summary>SOP Class: Unified Procedure Step - Push SOP Class</summary>
		public readonly static DicomUID UnifiedProcedureStepPushSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.1", "Unified Procedure Step - Push SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Unified Procedure Step - Watch SOP Class</summary>
		public readonly static DicomUID UnifiedProcedureStepWatchSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.2", "Unified Procedure Step - Watch SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Unified Procedure Step - Pull SOP Class</summary>
		public readonly static DicomUID UnifiedProcedureStepPullSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.3", "Unified Procedure Step - Pull SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Unified Procedure Step - Event SOP Class</summary>
		public readonly static DicomUID UnifiedProcedureStepEventSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.6.4", "Unified Procedure Step - Event SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Beams Delivery Instruction Storage</summary>
		public readonly static DicomUID RTBeamsDeliveryInstructionStorage = new DicomUID("1.2.840.10008.5.1.4.34.7", "RT Beams Delivery Instruction Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Conventional Machine Verification</summary>
		public readonly static DicomUID RTConventionalMachineVerification = new DicomUID("1.2.840.10008.5.1.4.34.8", "RT Conventional Machine Verification", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: RT Ion Machine Verification</summary>
		public readonly static DicomUID RTIonMachineVerification = new DicomUID("1.2.840.10008.5.1.4.34.9", "RT Ion Machine Verification", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: General Relevant Patient Information Query</summary>
		public readonly static DicomUID GeneralRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.1", "General Relevant Patient Information Query", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Breast Imaging Relevant Patient Information Query</summary>
		public readonly static DicomUID BreastImagingRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.2", "Breast Imaging Relevant Patient Information Query", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Cardiac Relevant Patient Information Query</summary>
		public readonly static DicomUID CardiacRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.3", "Cardiac Relevant Patient Information Query", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Hanging Protocol Storage</summary>
		public readonly static DicomUID HangingProtocolStorage = new DicomUID("1.2.840.10008.5.1.4.38.1", "Hanging Protocol Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Hanging Protocol Information Model - FIND</summary>
		public readonly static DicomUID HangingProtocolInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.38.2", "Hanging Protocol Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Hanging Protocol Information Model - MOVE</summary>
		public readonly static DicomUID HangingProtocolInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.38.3", "Hanging Protocol Information Model - MOVE", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Hanging Protocol Information Model - GET</summary>
		public readonly static DicomUID HangingProtocolInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.38.4", "Hanging Protocol Information Model - GET", DicomUidType.SOPClass, false);

		/// <summary>Transfer: Color Palette Storage</summary>
		public readonly static DicomUID ColorPaletteStorage = new DicomUID("1.2.840.10008.5.1.4.39.1", "Color Palette Storage", DicomUidType.TransferSyntax, false);

		/// <summary>Query/Retrieve: Color Palette Information Model - FIND</summary>
		public readonly static DicomUID ColorPaletteInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.39.2", "Color Palette Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>Query/Retrieve: Color Palette Information Model - MOVE</summary>
		public readonly static DicomUID ColorPaletteInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.39.3", "Color Palette Information Model - MOVE", DicomUidType.SOPClass, false);

		/// <summary>Query/Retrieve: Color Palette Information Model - GET</summary>
		public readonly static DicomUID ColorPaletteInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.39.4", "Color Palette Information Model - GET", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Product Characteristics Query SOP Class</summary>
		public readonly static DicomUID ProductCharacteristicsQuerySOPClass = new DicomUID("1.2.840.10008.5.1.4.41", "Product Characteristics Query SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Substance Approval Query SOP Class</summary>
		public readonly static DicomUID SubstanceApprovalQuerySOPClass = new DicomUID("1.2.840.10008.5.1.4.42", "Substance Approval Query SOP Class", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Generic Implant Template Storage</summary>
		public readonly static DicomUID GenericImplantTemplateStorage = new DicomUID("1.2.840.10008.5.1.4.43.1", "Generic Implant Template Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Generic Implant Template Information Model - FIND</summary>
		public readonly static DicomUID GenericImplantTemplateInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.43.2", "Generic Implant Template Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Generic Implant Template Information Model - MOVE</summary>
		public readonly static DicomUID GenericImplantTemplateInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.43.3", "Generic Implant Template Information Model - MOVE", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Generic Implant Template Information Model - GET</summary>
		public readonly static DicomUID GenericImplantTemplateInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.43.4", "Generic Implant Template Information Model - GET", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Assembly Template Storage</summary>
		public readonly static DicomUID ImplantAssemblyTemplateStorage = new DicomUID("1.2.840.10008.5.1.4.44.1", "Implant Assembly Template Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Assembly Template Information Model - FIND</summary>
		public readonly static DicomUID ImplantAssemblyTemplateInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.44.2", "Implant Assembly Template Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Assembly Template Information Model - MOVE</summary>
		public readonly static DicomUID ImplantAssemblyTemplateInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.44.3", "Implant Assembly Template Information Model - MOVE", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Assembly Template Information Model - GET</summary>
		public readonly static DicomUID ImplantAssemblyTemplateInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.44.4", "Implant Assembly Template Information Model - GET", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Template Group Storage</summary>
		public readonly static DicomUID ImplantTemplateGroupStorage = new DicomUID("1.2.840.10008.5.1.4.45.1", "Implant Template Group Storage", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Template Group Information Model - FIND</summary>
		public readonly static DicomUID ImplantTemplateGroupInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.45.2", "Implant Template Group Information Model - FIND", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Template Group Information Model - MOVE</summary>
		public readonly static DicomUID ImplantTemplateGroupInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.45.3", "Implant Template Group Information Model - MOVE", DicomUidType.SOPClass, false);

		/// <summary>SOP Class: Implant Template Group Information Model - GET</summary>
		public readonly static DicomUID ImplantTemplateGroupInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.45.4", "Implant Template Group Information Model - GET", DicomUidType.SOPClass, false);

		/// <summary>Application Hosting Model: Native DICOM Model</summary>
		public readonly static DicomUID NativeDICOMModel = new DicomUID("1.2.840.10008.7.1.1", "Native DICOM Model", DicomUidType.ApplicationHostingModel, false);

		/// <summary>Application Hosting Model: Abstract Multi-Dimensional Image Model</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModel = new DicomUID("1.2.840.10008.7.1.2", "Abstract Multi-Dimensional Image Model", DicomUidType.ApplicationHostingModel, false);

		/// <summary>LDAP OID: dicomDeviceName</summary>
		public readonly static DicomUID dicomDeviceName = new DicomUID("1.2.840.10008.15.0.3.1", "dicomDeviceName", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomDescription</summary>
		public readonly static DicomUID dicomDescription = new DicomUID("1.2.840.10008.15.0.3.2", "dicomDescription", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomManufacturer</summary>
		public readonly static DicomUID dicomManufacturer = new DicomUID("1.2.840.10008.15.0.3.3", "dicomManufacturer", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomManufacturerModelName</summary>
		public readonly static DicomUID dicomManufacturerModelName = new DicomUID("1.2.840.10008.15.0.3.4", "dicomManufacturerModelName", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomSoftwareVersion</summary>
		public readonly static DicomUID dicomSoftwareVersion = new DicomUID("1.2.840.10008.15.0.3.5", "dicomSoftwareVersion", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomVendorData</summary>
		public readonly static DicomUID dicomVendorData = new DicomUID("1.2.840.10008.15.0.3.6", "dicomVendorData", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomAETitle</summary>
		public readonly static DicomUID dicomAETitle = new DicomUID("1.2.840.10008.15.0.3.7", "dicomAETitle", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomNetworkConnectionReference</summary>
		public readonly static DicomUID dicomNetworkConnectionReference = new DicomUID("1.2.840.10008.15.0.3.8", "dicomNetworkConnectionReference", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomApplicationCluster</summary>
		public readonly static DicomUID dicomApplicationCluster = new DicomUID("1.2.840.10008.15.0.3.9", "dicomApplicationCluster", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomAssociationInitiator</summary>
		public readonly static DicomUID dicomAssociationInitiator = new DicomUID("1.2.840.10008.15.0.3.10", "dicomAssociationInitiator", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomAssociationAcceptor</summary>
		public readonly static DicomUID dicomAssociationAcceptor = new DicomUID("1.2.840.10008.15.0.3.11", "dicomAssociationAcceptor", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomHostname</summary>
		public readonly static DicomUID dicomHostname = new DicomUID("1.2.840.10008.15.0.3.12", "dicomHostname", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomPort</summary>
		public readonly static DicomUID dicomPort = new DicomUID("1.2.840.10008.15.0.3.13", "dicomPort", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomSOPClass</summary>
		public readonly static DicomUID dicomSOPClass = new DicomUID("1.2.840.10008.15.0.3.14", "dicomSOPClass", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomTransferRole</summary>
		public readonly static DicomUID dicomTransferRole = new DicomUID("1.2.840.10008.15.0.3.15", "dicomTransferRole", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomTransferSyntax</summary>
		public readonly static DicomUID dicomTransferSyntax = new DicomUID("1.2.840.10008.15.0.3.16", "dicomTransferSyntax", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomPrimaryDeviceType</summary>
		public readonly static DicomUID dicomPrimaryDeviceType = new DicomUID("1.2.840.10008.15.0.3.17", "dicomPrimaryDeviceType", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomRelatedDeviceReference</summary>
		public readonly static DicomUID dicomRelatedDeviceReference = new DicomUID("1.2.840.10008.15.0.3.18", "dicomRelatedDeviceReference", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomPreferredCalledAETitle</summary>
		public readonly static DicomUID dicomPreferredCalledAETitle = new DicomUID("1.2.840.10008.15.0.3.19", "dicomPreferredCalledAETitle", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomTLSCyphersuite</summary>
		public readonly static DicomUID dicomTLSCyphersuite = new DicomUID("1.2.840.10008.15.0.3.20", "dicomTLSCyphersuite", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomAuthorizedNodeCertificateReference</summary>
		public readonly static DicomUID dicomAuthorizedNodeCertificateReference = new DicomUID("1.2.840.10008.15.0.3.21", "dicomAuthorizedNodeCertificateReference", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomThisNodeCertificateReference</summary>
		public readonly static DicomUID dicomThisNodeCertificateReference = new DicomUID("1.2.840.10008.15.0.3.22", "dicomThisNodeCertificateReference", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomInstalled</summary>
		public readonly static DicomUID dicomInstalled = new DicomUID("1.2.840.10008.15.0.3.23", "dicomInstalled", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomStationName</summary>
		public readonly static DicomUID dicomStationName = new DicomUID("1.2.840.10008.15.0.3.24", "dicomStationName", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomDeviceSerialNumber</summary>
		public readonly static DicomUID dicomDeviceSerialNumber = new DicomUID("1.2.840.10008.15.0.3.25", "dicomDeviceSerialNumber", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomInstitutionName</summary>
		public readonly static DicomUID dicomInstitutionName = new DicomUID("1.2.840.10008.15.0.3.26", "dicomInstitutionName", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomInstitutionAddress</summary>
		public readonly static DicomUID dicomInstitutionAddress = new DicomUID("1.2.840.10008.15.0.3.27", "dicomInstitutionAddress", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomInstitutionDepartmentName</summary>
		public readonly static DicomUID dicomInstitutionDepartmentName = new DicomUID("1.2.840.10008.15.0.3.28", "dicomInstitutionDepartmentName", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomIssuerOfPatientID</summary>
		public readonly static DicomUID dicomIssuerOfPatientID = new DicomUID("1.2.840.10008.15.0.3.29", "dicomIssuerOfPatientID", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomPreferredCallingAETitle</summary>
		public readonly static DicomUID dicomPreferredCallingAETitle = new DicomUID("1.2.840.10008.15.0.3.30", "dicomPreferredCallingAETitle", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomSupportedCharacterSet</summary>
		public readonly static DicomUID dicomSupportedCharacterSet = new DicomUID("1.2.840.10008.15.0.3.31", "dicomSupportedCharacterSet", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomConfigurationRoot</summary>
		public readonly static DicomUID dicomConfigurationRoot = new DicomUID("1.2.840.10008.15.0.4.1", "dicomConfigurationRoot", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomDevicesRoot</summary>
		public readonly static DicomUID dicomDevicesRoot = new DicomUID("1.2.840.10008.15.0.4.2", "dicomDevicesRoot", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomUniqueAETitlesRegistryRoot</summary>
		public readonly static DicomUID dicomUniqueAETitlesRegistryRoot = new DicomUID("1.2.840.10008.15.0.4.3", "dicomUniqueAETitlesRegistryRoot", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomDevice</summary>
		public readonly static DicomUID dicomDevice = new DicomUID("1.2.840.10008.15.0.4.4", "dicomDevice", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomNetworkAE</summary>
		public readonly static DicomUID dicomNetworkAE = new DicomUID("1.2.840.10008.15.0.4.5", "dicomNetworkAE", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomNetworkConnection</summary>
		public readonly static DicomUID dicomNetworkConnection = new DicomUID("1.2.840.10008.15.0.4.6", "dicomNetworkConnection", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomUniqueAETitle</summary>
		public readonly static DicomUID dicomUniqueAETitle = new DicomUID("1.2.840.10008.15.0.4.7", "dicomUniqueAETitle", DicomUidType.LDAP, false);

		/// <summary>LDAP OID: dicomTransferCapability</summary>
		public readonly static DicomUID dicomTransferCapability = new DicomUID("1.2.840.10008.15.0.4.8", "dicomTransferCapability", DicomUidType.LDAP, false);

		/// <summary>Synchronization Frame of Reference: Universal Coordinated Time</summary>
		public readonly static DicomUID UniversalCoordinatedTime = new DicomUID("1.2.840.10008.15.1.1", "Universal Coordinated Time", DicomUidType.FrameOfReference, false);

		/// <summary>Context Group Name: Anatomic Modifier (2)</summary>
		public readonly static DicomUID AnatomicModifier2 = new DicomUID("1.2.840.10008.6.1.1", "Anatomic Modifier (2)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Anatomic Region (4)</summary>
		public readonly static DicomUID AnatomicRegion4 = new DicomUID("1.2.840.10008.6.1.2", "Anatomic Region (4)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Transducer Approach (5)</summary>
		public readonly static DicomUID TransducerApproach5 = new DicomUID("1.2.840.10008.6.1.3", "Transducer Approach (5)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Transducer Orientation (6)</summary>
		public readonly static DicomUID TransducerOrientation6 = new DicomUID("1.2.840.10008.6.1.4", "Transducer Orientation (6)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Beam Path (7)</summary>
		public readonly static DicomUID UltrasoundBeamPath7 = new DicomUID("1.2.840.10008.6.1.5", "Ultrasound Beam Path (7)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Angiographic Interventional Devices (8)</summary>
		public readonly static DicomUID AngiographicInterventionalDevices8 = new DicomUID("1.2.840.10008.6.1.6", "Angiographic Interventional Devices (8)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Image Guided Therapeutic Procedures (9)</summary>
		public readonly static DicomUID ImageGuidedTherapeuticProcedures9 = new DicomUID("1.2.840.10008.6.1.7", "Image Guided Therapeutic Procedures (9)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Interventional Drug (10)</summary>
		public readonly static DicomUID InterventionalDrug10 = new DicomUID("1.2.840.10008.6.1.8", "Interventional Drug (10)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Route of Administration (11)</summary>
		public readonly static DicomUID RouteOfAdministration11 = new DicomUID("1.2.840.10008.6.1.9", "Route of Administration (11)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Radiographic Contrast Agent (12)</summary>
		public readonly static DicomUID RadiographicContrastAgent12 = new DicomUID("1.2.840.10008.6.1.10", "Radiographic Contrast Agent (12)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Radiographic Contrast Agent Ingredient (13)</summary>
		public readonly static DicomUID RadiographicContrastAgentIngredient13 = new DicomUID("1.2.840.10008.6.1.11", "Radiographic Contrast Agent Ingredient (13)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Isotopes in Radiopharmaceuticals (18)</summary>
		public readonly static DicomUID IsotopesInRadiopharmaceuticals18 = new DicomUID("1.2.840.10008.6.1.12", "Isotopes in Radiopharmaceuticals (18)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Patient Orientation (19)</summary>
		public readonly static DicomUID PatientOrientation19 = new DicomUID("1.2.840.10008.6.1.13", "Patient Orientation (19)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Patient Orientation Modifier (20)</summary>
		public readonly static DicomUID PatientOrientationModifier20 = new DicomUID("1.2.840.10008.6.1.14", "Patient Orientation Modifier (20)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Patient Gantry Relationship (21)</summary>
		public readonly static DicomUID PatientGantryRelationship21 = new DicomUID("1.2.840.10008.6.1.15", "Patient Gantry Relationship (21)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cranio-caudad Angulation (23)</summary>
		public readonly static DicomUID CranioCaudadAngulation23 = new DicomUID("1.2.840.10008.6.1.16", "Cranio-caudad Angulation (23)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Radiopharmaceuticals (25)</summary>
		public readonly static DicomUID Radiopharmaceuticals25 = new DicomUID("1.2.840.10008.6.1.17", "Radiopharmaceuticals (25)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Nuclear Medicine Projections (26)</summary>
		public readonly static DicomUID NuclearMedicineProjections26 = new DicomUID("1.2.840.10008.6.1.18", "Nuclear Medicine Projections (26)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Acquisition Modality (29)</summary>
		public readonly static DicomUID AcquisitionModality29 = new DicomUID("1.2.840.10008.6.1.19", "Acquisition Modality (29)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: DICOM Devices (30)</summary>
		public readonly static DicomUID DICOMDevices30 = new DicomUID("1.2.840.10008.6.1.20", "DICOM Devices (30)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Priors (31)</summary>
		public readonly static DicomUID AbstractPriors31 = new DicomUID("1.2.840.10008.6.1.21", "Abstract Priors (31)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Numeric Value Qualifier (42)</summary>
		public readonly static DicomUID NumericValueQualifier42 = new DicomUID("1.2.840.10008.6.1.22", "Numeric Value Qualifier (42)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units of Measurement (82)</summary>
		public readonly static DicomUID UnitsOfMeasurement82 = new DicomUID("1.2.840.10008.6.1.23", "Units of Measurement (82)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units for Real World Value Mapping (83)</summary>
		public readonly static DicomUID UnitsForRealWorldValueMapping83 = new DicomUID("1.2.840.10008.6.1.24", "Units for Real World Value Mapping (83)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Level of Significance (220)</summary>
		public readonly static DicomUID LevelOfSignificance220 = new DicomUID("1.2.840.10008.6.1.25", "Level of Significance (220)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Measurement Range Concepts (221)</summary>
		public readonly static DicomUID MeasurementRangeConcepts221 = new DicomUID("1.2.840.10008.6.1.26", "Measurement Range Concepts (221)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Normality Codes (222)</summary>
		public readonly static DicomUID NormalityCodes222 = new DicomUID("1.2.840.10008.6.1.27", "Normality Codes (222)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Normal Range Values (223)</summary>
		public readonly static DicomUID NormalRangeValues223 = new DicomUID("1.2.840.10008.6.1.28", "Normal Range Values (223)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Selection Method (224)</summary>
		public readonly static DicomUID SelectionMethod224 = new DicomUID("1.2.840.10008.6.1.29", "Selection Method (224)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Measurement Uncertainty Concepts (225)</summary>
		public readonly static DicomUID MeasurementUncertaintyConcepts225 = new DicomUID("1.2.840.10008.6.1.30", "Measurement Uncertainty Concepts (225)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Population Statistical Descriptors (226)</summary>
		public readonly static DicomUID PopulationStatisticalDescriptors226 = new DicomUID("1.2.840.10008.6.1.31", "Population Statistical Descriptors (226)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Sample Statistical Descriptors (227)</summary>
		public readonly static DicomUID SampleStatisticalDescriptors227 = new DicomUID("1.2.840.10008.6.1.32", "Sample Statistical Descriptors (227)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Equation or Table (228)</summary>
		public readonly static DicomUID EquationOrTable228 = new DicomUID("1.2.840.10008.6.1.33", "Equation or Table (228)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Yes-No (230)</summary>
		public readonly static DicomUID YesNo230 = new DicomUID("1.2.840.10008.6.1.34", "Yes-No (230)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Present-Absent (240)</summary>
		public readonly static DicomUID PresentAbsent240 = new DicomUID("1.2.840.10008.6.1.35", "Present-Absent (240)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Normal-Abnormal (242)</summary>
		public readonly static DicomUID NormalAbnormal242 = new DicomUID("1.2.840.10008.6.1.36", "Normal-Abnormal (242)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Laterality (244)</summary>
		public readonly static DicomUID Laterality244 = new DicomUID("1.2.840.10008.6.1.37", "Laterality (244)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Positive-Negative (250)</summary>
		public readonly static DicomUID PositiveNegative250 = new DicomUID("1.2.840.10008.6.1.38", "Positive-Negative (250)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Severity of Complication (251)</summary>
		public readonly static DicomUID SeverityOfComplication251 = new DicomUID("1.2.840.10008.6.1.39", "Severity of Complication (251)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Observer Type (270)</summary>
		public readonly static DicomUID ObserverType270 = new DicomUID("1.2.840.10008.6.1.40", "Observer Type (270)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Observation Subject Class (271)</summary>
		public readonly static DicomUID ObservationSubjectClass271 = new DicomUID("1.2.840.10008.6.1.41", "Observation Subject Class (271)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Audio Channel Source (3000)</summary>
		public readonly static DicomUID AudioChannelSource3000 = new DicomUID("1.2.840.10008.6.1.42", "Audio Channel Source (3000)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Leads (3001)</summary>
		public readonly static DicomUID ECGLeads3001 = new DicomUID("1.2.840.10008.6.1.43", "ECG Leads (3001)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Waveform Sources (3003)</summary>
		public readonly static DicomUID HemodynamicWaveformSources3003 = new DicomUID("1.2.840.10008.6.1.44", "Hemodynamic Waveform Sources (3003)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiovascular Anatomic Locations (3010)</summary>
		public readonly static DicomUID CardiovascularAnatomicLocations3010 = new DicomUID("1.2.840.10008.6.1.45", "Cardiovascular Anatomic Locations (3010)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Electrophysiology Anatomic Locations (3011)</summary>
		public readonly static DicomUID ElectrophysiologyAnatomicLocations3011 = new DicomUID("1.2.840.10008.6.1.46", "Electrophysiology Anatomic Locations (3011)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Coronary Artery Segments (3014)</summary>
		public readonly static DicomUID CoronaryArterySegments3014 = new DicomUID("1.2.840.10008.6.1.47", "Coronary Artery Segments (3014)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Coronary Arteries (3015)</summary>
		public readonly static DicomUID CoronaryArteries3015 = new DicomUID("1.2.840.10008.6.1.48", "Coronary Arteries (3015)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiovascular Anatomic Location Modifiers (3019)</summary>
		public readonly static DicomUID CardiovascularAnatomicLocationModifiers3019 = new DicomUID("1.2.840.10008.6.1.49", "Cardiovascular Anatomic Location Modifiers (3019)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiology Units of Measurement (3082)</summary>
		public readonly static DicomUID CardiologyUnitsOfMeasurement3082 = new DicomUID("1.2.840.10008.6.1.50", "Cardiology Units of Measurement (3082)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Time Synchronization Channel Types (3090)</summary>
		public readonly static DicomUID TimeSynchronizationChannelTypes3090 = new DicomUID("1.2.840.10008.6.1.51", "Time Synchronization Channel Types (3090)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: NM Procedural State Values (3101)</summary>
		public readonly static DicomUID NMProceduralStateValues3101 = new DicomUID("1.2.840.10008.6.1.52", "NM Procedural State Values (3101)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Electrophysiology Measurement Functions and Techniques (3240)</summary>
		public readonly static DicomUID ElectrophysiologyMeasurementFunctionsAndTechniques3240 = new DicomUID("1.2.840.10008.6.1.53", "Electrophysiology Measurement Functions and Techniques (3240)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Measurement Techniques (3241)</summary>
		public readonly static DicomUID HemodynamicMeasurementTechniques3241 = new DicomUID("1.2.840.10008.6.1.54", "Hemodynamic Measurement Techniques (3241)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Catheterization Procedure Phase (3250)</summary>
		public readonly static DicomUID CatheterizationProcedurePhase3250 = new DicomUID("1.2.840.10008.6.1.55", "Catheterization Procedure Phase (3250)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Electrophysiology Procedure Phase (3254)</summary>
		public readonly static DicomUID ElectrophysiologyProcedurePhase3254 = new DicomUID("1.2.840.10008.6.1.56", "Electrophysiology Procedure Phase (3254)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Protocols (3261)</summary>
		public readonly static DicomUID StressProtocols3261 = new DicomUID("1.2.840.10008.6.1.57", "Stress Protocols (3261)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Patient State Values (3262)</summary>
		public readonly static DicomUID ECGPatientStateValues3262 = new DicomUID("1.2.840.10008.6.1.58", "ECG Patient State Values (3262)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Electrode Placement Values (3263)</summary>
		public readonly static DicomUID ElectrodePlacementValues3263 = new DicomUID("1.2.840.10008.6.1.59", "Electrode Placement Values (3263)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: XYZ Electrode Placement Values (3264)</summary>
		public readonly static DicomUID XYZElectrodePlacementValues3264 = new DicomUID("1.2.840.10008.6.1.60", "XYZ Electrode Placement Values (3264)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Physiological Challenges (3271)</summary>
		public readonly static DicomUID HemodynamicPhysiologicalChallenges3271 = new DicomUID("1.2.840.10008.6.1.61", "Hemodynamic Physiological Challenges (3271)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Annotations (3335)</summary>
		public readonly static DicomUID ECGAnnotations3335 = new DicomUID("1.2.840.10008.6.1.62", "ECG Annotations (3335)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Annotations (3337)</summary>
		public readonly static DicomUID HemodynamicAnnotations3337 = new DicomUID("1.2.840.10008.6.1.63", "Hemodynamic Annotations (3337)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Electrophysiology Annotations (3339)</summary>
		public readonly static DicomUID ElectrophysiologyAnnotations3339 = new DicomUID("1.2.840.10008.6.1.64", "Electrophysiology Annotations (3339)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedure Log Titles (3400)</summary>
		public readonly static DicomUID ProcedureLogTitles3400 = new DicomUID("1.2.840.10008.6.1.65", "Procedure Log Titles (3400)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Types of Log Notes (3401)</summary>
		public readonly static DicomUID TypesOfLogNotes3401 = new DicomUID("1.2.840.10008.6.1.66", "Types of Log Notes (3401)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Patient Status and Events (3402)</summary>
		public readonly static DicomUID PatientStatusAndEvents3402 = new DicomUID("1.2.840.10008.6.1.67", "Patient Status and Events (3402)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Percutaneous Entry (3403)</summary>
		public readonly static DicomUID PercutaneousEntry3403 = new DicomUID("1.2.840.10008.6.1.68", "Percutaneous Entry (3403)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Staff Actions (3404)</summary>
		public readonly static DicomUID StaffActions3404 = new DicomUID("1.2.840.10008.6.1.69", "Staff Actions (3404)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedure Action Values (3405)</summary>
		public readonly static DicomUID ProcedureActionValues3405 = new DicomUID("1.2.840.10008.6.1.70", "Procedure Action Values (3405)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Non-Coronary Transcatheter Interventions (3406)</summary>
		public readonly static DicomUID NonCoronaryTranscatheterInterventions3406 = new DicomUID("1.2.840.10008.6.1.71", "Non-Coronary Transcatheter Interventions (3406)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Purpose of Reference to Object (3407)</summary>
		public readonly static DicomUID PurposeOfReferenceToObject3407 = new DicomUID("1.2.840.10008.6.1.72", "Purpose of Reference to Object (3407)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Actions with Consumables (3408)</summary>
		public readonly static DicomUID ActionsWithConsumables3408 = new DicomUID("1.2.840.10008.6.1.73", "Actions with Consumables (3408)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Administration of Drugs/Contrast (3409)</summary>
		public readonly static DicomUID AdministrationOfDrugsContrast3409 = new DicomUID("1.2.840.10008.6.1.74", "Administration of Drugs/Contrast (3409)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Numeric Parameters of Drugs/Contrast (3410)</summary>
		public readonly static DicomUID NumericParametersOfDrugsContrast3410 = new DicomUID("1.2.840.10008.6.1.75", "Numeric Parameters of Drugs/Contrast (3410)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intracoronary Devices (3411)</summary>
		public readonly static DicomUID IntracoronaryDevices3411 = new DicomUID("1.2.840.10008.6.1.76", "Intracoronary Devices (3411)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intervention Actions and Status (3412)</summary>
		public readonly static DicomUID InterventionActionsAndStatus3412 = new DicomUID("1.2.840.10008.6.1.77", "Intervention Actions and Status (3412)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Adverse Outcomes (3413)</summary>
		public readonly static DicomUID AdverseOutcomes3413 = new DicomUID("1.2.840.10008.6.1.78", "Adverse Outcomes (3413)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedure Urgency (3414)</summary>
		public readonly static DicomUID ProcedureUrgency3414 = new DicomUID("1.2.840.10008.6.1.79", "Procedure Urgency (3414)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Rhythms (3415)</summary>
		public readonly static DicomUID CardiacRhythms3415 = new DicomUID("1.2.840.10008.6.1.80", "Cardiac Rhythms (3415)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Respiration Rhythms (3416)</summary>
		public readonly static DicomUID RespirationRhythms3416 = new DicomUID("1.2.840.10008.6.1.81", "Respiration Rhythms (3416)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lesion Risk (3418)</summary>
		public readonly static DicomUID LesionRisk3418 = new DicomUID("1.2.840.10008.6.1.82", "Lesion Risk (3418)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Findings Titles (3419)</summary>
		public readonly static DicomUID FindingsTitles3419 = new DicomUID("1.2.840.10008.6.1.83", "Findings Titles (3419)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedure Action (3421)</summary>
		public readonly static DicomUID ProcedureAction3421 = new DicomUID("1.2.840.10008.6.1.84", "Procedure Action (3421)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Device Use Actions (3422)</summary>
		public readonly static DicomUID DeviceUseActions3422 = new DicomUID("1.2.840.10008.6.1.85", "Device Use Actions (3422)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Numeric Device Characteristics (3423)</summary>
		public readonly static DicomUID NumericDeviceCharacteristics3423 = new DicomUID("1.2.840.10008.6.1.86", "Numeric Device Characteristics (3423)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intervention Parameters (3425)</summary>
		public readonly static DicomUID InterventionParameters3425 = new DicomUID("1.2.840.10008.6.1.87", "Intervention Parameters (3425)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Consumables Parameters (3426)</summary>
		public readonly static DicomUID ConsumablesParameters3426 = new DicomUID("1.2.840.10008.6.1.88", "Consumables Parameters (3426)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Equipment Events (3427)</summary>
		public readonly static DicomUID EquipmentEvents3427 = new DicomUID("1.2.840.10008.6.1.89", "Equipment Events (3427)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Imaging Procedures (3428)</summary>
		public readonly static DicomUID ImagingProcedures3428 = new DicomUID("1.2.840.10008.6.1.90", "Imaging Procedures (3428)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Catheterization Devices (3429)</summary>
		public readonly static DicomUID CatheterizationDevices3429 = new DicomUID("1.2.840.10008.6.1.91", "Catheterization Devices (3429)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: DateTime Qualifiers (3430)</summary>
		public readonly static DicomUID DateTimeQualifiers3430 = new DicomUID("1.2.840.10008.6.1.92", "DateTime Qualifiers (3430)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Peripheral Pulse Locations (3440)</summary>
		public readonly static DicomUID PeripheralPulseLocations3440 = new DicomUID("1.2.840.10008.6.1.93", "Peripheral Pulse Locations (3440)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Patient assessments (3441)</summary>
		public readonly static DicomUID PatientAssessments3441 = new DicomUID("1.2.840.10008.6.1.94", "Patient assessments (3441)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Peripheral Pulse Methods (3442)</summary>
		public readonly static DicomUID PeripheralPulseMethods3442 = new DicomUID("1.2.840.10008.6.1.95", "Peripheral Pulse Methods (3442)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Skin Condition (3446)</summary>
		public readonly static DicomUID SkinCondition3446 = new DicomUID("1.2.840.10008.6.1.96", "Skin Condition (3446)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Airway Assessment (3448)</summary>
		public readonly static DicomUID AirwayAssessment3448 = new DicomUID("1.2.840.10008.6.1.97", "Airway Assessment (3448)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calibration Objects (3451)</summary>
		public readonly static DicomUID CalibrationObjects3451 = new DicomUID("1.2.840.10008.6.1.98", "Calibration Objects (3451)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calibration Methods (3452)</summary>
		public readonly static DicomUID CalibrationMethods3452 = new DicomUID("1.2.840.10008.6.1.99", "Calibration Methods (3452)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Volume Methods (3453)</summary>
		public readonly static DicomUID CardiacVolumeMethods3453 = new DicomUID("1.2.840.10008.6.1.100", "Cardiac Volume Methods (3453)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Index Methods (3455)</summary>
		public readonly static DicomUID IndexMethods3455 = new DicomUID("1.2.840.10008.6.1.101", "Index Methods (3455)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Sub-segment Methods (3456)</summary>
		public readonly static DicomUID SubSegmentMethods3456 = new DicomUID("1.2.840.10008.6.1.102", "Sub-segment Methods (3456)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Contour Realignment (3458)</summary>
		public readonly static DicomUID ContourRealignment3458 = new DicomUID("1.2.840.10008.6.1.103", "Contour Realignment (3458)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Circumferential ExtenT (3460)</summary>
		public readonly static DicomUID CircumferentialExtenT3460 = new DicomUID("1.2.840.10008.6.1.104", "Circumferential ExtenT (3460)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Regional Extent (3461)</summary>
		public readonly static DicomUID RegionalExtent3461 = new DicomUID("1.2.840.10008.6.1.105", "Regional Extent (3461)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chamber Identification (3462)</summary>
		public readonly static DicomUID ChamberIdentification3462 = new DicomUID("1.2.840.10008.6.1.106", "Chamber Identification (3462)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: QA Reference MethodS (3465)</summary>
		public readonly static DicomUID QAReferenceMethodS3465 = new DicomUID("1.2.840.10008.6.1.107", "QA Reference MethodS (3465)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Plane Identification (3466)</summary>
		public readonly static DicomUID PlaneIdentification3466 = new DicomUID("1.2.840.10008.6.1.108", "Plane Identification (3466)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ejection Fraction (3467)</summary>
		public readonly static DicomUID EjectionFraction3467 = new DicomUID("1.2.840.10008.6.1.109", "Ejection Fraction (3467)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ED Volume (3468)</summary>
		public readonly static DicomUID EDVolume3468 = new DicomUID("1.2.840.10008.6.1.110", "ED Volume (3468)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ES Volume (3469)</summary>
		public readonly static DicomUID ESVolume3469 = new DicomUID("1.2.840.10008.6.1.111", "ES Volume (3469)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vessel Lumen Cross-Sectional Area Calculation Methods (3470)</summary>
		public readonly static DicomUID VesselLumenCrossSectionalAreaCalculationMethods3470 = new DicomUID("1.2.840.10008.6.1.112", "Vessel Lumen Cross-Sectional Area Calculation Methods (3470)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Estimated Volumes (3471)</summary>
		public readonly static DicomUID EstimatedVolumes3471 = new DicomUID("1.2.840.10008.6.1.113", "Estimated Volumes (3471)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Contraction Phase (3472)</summary>
		public readonly static DicomUID CardiacContractionPhase3472 = new DicomUID("1.2.840.10008.6.1.114", "Cardiac Contraction Phase (3472)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Procedure Phases (3480)</summary>
		public readonly static DicomUID IVUSProcedurePhases3480 = new DicomUID("1.2.840.10008.6.1.115", "IVUS Procedure Phases (3480)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Distance Measurements (3481)</summary>
		public readonly static DicomUID IVUSDistanceMeasurements3481 = new DicomUID("1.2.840.10008.6.1.116", "IVUS Distance Measurements (3481)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Area Measurements (3482)</summary>
		public readonly static DicomUID IVUSAreaMeasurements3482 = new DicomUID("1.2.840.10008.6.1.117", "IVUS Area Measurements (3482)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Longitudinal Measurements (3483)</summary>
		public readonly static DicomUID IVUSLongitudinalMeasurements3483 = new DicomUID("1.2.840.10008.6.1.118", "IVUS Longitudinal Measurements (3483)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Indices and Ratios (3484)</summary>
		public readonly static DicomUID IVUSIndicesAndRatios3484 = new DicomUID("1.2.840.10008.6.1.119", "IVUS Indices and Ratios (3484)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Volume Measurements (3485)</summary>
		public readonly static DicomUID IVUSVolumeMeasurements3485 = new DicomUID("1.2.840.10008.6.1.120", "IVUS Volume Measurements (3485)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Measurement Sites (3486)</summary>
		public readonly static DicomUID VascularMeasurementSites3486 = new DicomUID("1.2.840.10008.6.1.121", "Vascular Measurement Sites (3486)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intravascular Volumetric Regions (3487)</summary>
		public readonly static DicomUID IntravascularVolumetricRegions3487 = new DicomUID("1.2.840.10008.6.1.122", "Intravascular Volumetric Regions (3487)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Min/Max/Mean (3488)</summary>
		public readonly static DicomUID MinMaxMean3488 = new DicomUID("1.2.840.10008.6.1.123", "Min/Max/Mean (3488)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calcium Distribution (3489)</summary>
		public readonly static DicomUID CalciumDistribution3489 = new DicomUID("1.2.840.10008.6.1.124", "Calcium Distribution (3489)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Lesion Morphologies (3491)</summary>
		public readonly static DicomUID IVUSLesionMorphologies3491 = new DicomUID("1.2.840.10008.6.1.125", "IVUS Lesion Morphologies (3491)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Dissection Classifications (3492)</summary>
		public readonly static DicomUID VascularDissectionClassifications3492 = new DicomUID("1.2.840.10008.6.1.126", "Vascular Dissection Classifications (3492)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Relative Stenosis Severities (3493)</summary>
		public readonly static DicomUID IVUSRelativeStenosisSeverities3493 = new DicomUID("1.2.840.10008.6.1.127", "IVUS Relative Stenosis Severities (3493)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Non Morphological Findings (3494)</summary>
		public readonly static DicomUID IVUSNonMorphologicalFindings3494 = new DicomUID("1.2.840.10008.6.1.128", "IVUS Non Morphological Findings (3494)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Plaque Composition (3495)</summary>
		public readonly static DicomUID IVUSPlaqueComposition3495 = new DicomUID("1.2.840.10008.6.1.129", "IVUS Plaque Composition (3495)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Fiducial Points (3496)</summary>
		public readonly static DicomUID IVUSFiducialPoints3496 = new DicomUID("1.2.840.10008.6.1.130", "IVUS Fiducial Points (3496)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IVUS Arterial Morphology (3497)</summary>
		public readonly static DicomUID IVUSArterialMorphology3497 = new DicomUID("1.2.840.10008.6.1.131", "IVUS Arterial Morphology (3497)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pressure Units (3500)</summary>
		public readonly static DicomUID PressureUnits3500 = new DicomUID("1.2.840.10008.6.1.132", "Pressure Units (3500)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Resistance Units (3502)</summary>
		public readonly static DicomUID HemodynamicResistanceUnits3502 = new DicomUID("1.2.840.10008.6.1.133", "Hemodynamic Resistance Units (3502)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Indexed Hemodynamic Resistance Units (3503)</summary>
		public readonly static DicomUID IndexedHemodynamicResistanceUnits3503 = new DicomUID("1.2.840.10008.6.1.134", "Indexed Hemodynamic Resistance Units (3503)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Catheter Size Units (3510)</summary>
		public readonly static DicomUID CatheterSizeUnits3510 = new DicomUID("1.2.840.10008.6.1.135", "Catheter Size Units (3510)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Specimen Collection (3515)</summary>
		public readonly static DicomUID SpecimenCollection3515 = new DicomUID("1.2.840.10008.6.1.136", "Specimen Collection (3515)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Source Type (3520)</summary>
		public readonly static DicomUID BloodSourceType3520 = new DicomUID("1.2.840.10008.6.1.137", "Blood Source Type (3520)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Gas Pressures (3524)</summary>
		public readonly static DicomUID BloodGasPressures3524 = new DicomUID("1.2.840.10008.6.1.138", "Blood Gas Pressures (3524)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Gas Content (3525)</summary>
		public readonly static DicomUID BloodGasContent3525 = new DicomUID("1.2.840.10008.6.1.139", "Blood Gas Content (3525)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Gas Saturation (3526)</summary>
		public readonly static DicomUID BloodGasSaturation3526 = new DicomUID("1.2.840.10008.6.1.140", "Blood Gas Saturation (3526)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Base Excess (3527)</summary>
		public readonly static DicomUID BloodBaseExcess3527 = new DicomUID("1.2.840.10008.6.1.141", "Blood Base Excess (3527)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood pH (3528)</summary>
		public readonly static DicomUID BloodPH3528 = new DicomUID("1.2.840.10008.6.1.142", "Blood pH (3528)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Arterial / Venous Content (3529)</summary>
		public readonly static DicomUID ArterialVenousContent3529 = new DicomUID("1.2.840.10008.6.1.143", "Arterial / Venous Content (3529)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Oxygen Administration Actions (3530)</summary>
		public readonly static DicomUID OxygenAdministrationActions3530 = new DicomUID("1.2.840.10008.6.1.144", "Oxygen Administration Actions (3530)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Oxygen Administration (3531)</summary>
		public readonly static DicomUID OxygenAdministration3531 = new DicomUID("1.2.840.10008.6.1.145", "Oxygen Administration (3531)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Circulatory Support Actions (3550)</summary>
		public readonly static DicomUID CirculatorySupportActions3550 = new DicomUID("1.2.840.10008.6.1.146", "Circulatory Support Actions (3550)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ventilation Actions (3551)</summary>
		public readonly static DicomUID VentilationActions3551 = new DicomUID("1.2.840.10008.6.1.147", "Ventilation Actions (3551)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pacing Actions (3552)</summary>
		public readonly static DicomUID PacingActions3552 = new DicomUID("1.2.840.10008.6.1.148", "Pacing Actions (3552)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Circulatory Support (3553)</summary>
		public readonly static DicomUID CirculatorySupport3553 = new DicomUID("1.2.840.10008.6.1.149", "Circulatory Support (3553)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ventilation (3554)</summary>
		public readonly static DicomUID Ventilation3554 = new DicomUID("1.2.840.10008.6.1.150", "Ventilation (3554)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pacing (3555)</summary>
		public readonly static DicomUID Pacing3555 = new DicomUID("1.2.840.10008.6.1.151", "Pacing (3555)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Pressure Methods (3560)</summary>
		public readonly static DicomUID BloodPressureMethods3560 = new DicomUID("1.2.840.10008.6.1.152", "Blood Pressure Methods (3560)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Relative times (3600)</summary>
		public readonly static DicomUID RelativeTimes3600 = new DicomUID("1.2.840.10008.6.1.153", "Relative times (3600)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Patient State (3602)</summary>
		public readonly static DicomUID HemodynamicPatientState3602 = new DicomUID("1.2.840.10008.6.1.154", "Hemodynamic Patient State (3602)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Arterial lesion locations (3604)</summary>
		public readonly static DicomUID ArterialLesionLocations3604 = new DicomUID("1.2.840.10008.6.1.155", "Arterial lesion locations (3604)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Arterial source locations (3606)</summary>
		public readonly static DicomUID ArterialSourceLocations3606 = new DicomUID("1.2.840.10008.6.1.156", "Arterial source locations (3606)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Venous Source locations (3607)</summary>
		public readonly static DicomUID VenousSourceLocations3607 = new DicomUID("1.2.840.10008.6.1.157", "Venous Source locations (3607)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Atrial source locations (3608)</summary>
		public readonly static DicomUID AtrialSourceLocations3608 = new DicomUID("1.2.840.10008.6.1.158", "Atrial source locations (3608)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ventricular source locations (3609)</summary>
		public readonly static DicomUID VentricularSourceLocations3609 = new DicomUID("1.2.840.10008.6.1.159", "Ventricular source locations (3609)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Gradient Source Locations (3610)</summary>
		public readonly static DicomUID GradientSourceLocations3610 = new DicomUID("1.2.840.10008.6.1.160", "Gradient Source Locations (3610)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pressure Measurements (3611)</summary>
		public readonly static DicomUID PressureMeasurements3611 = new DicomUID("1.2.840.10008.6.1.161", "Pressure Measurements (3611)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Velocity Measurements (3612)</summary>
		public readonly static DicomUID BloodVelocityMeasurements3612 = new DicomUID("1.2.840.10008.6.1.162", "Blood Velocity Measurements (3612)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Time Measurements (3613)</summary>
		public readonly static DicomUID HemodynamicTimeMeasurements3613 = new DicomUID("1.2.840.10008.6.1.163", "Hemodynamic Time Measurements (3613)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Valve Areas, non-Mitral (3614)</summary>
		public readonly static DicomUID ValveAreasNonMitral3614 = new DicomUID("1.2.840.10008.6.1.164", "Valve Areas, non-Mitral (3614)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Valve Areas (3615)</summary>
		public readonly static DicomUID ValveAreas3615 = new DicomUID("1.2.840.10008.6.1.165", "Valve Areas (3615)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Period Measurements (3616)</summary>
		public readonly static DicomUID HemodynamicPeriodMeasurements3616 = new DicomUID("1.2.840.10008.6.1.166", "Hemodynamic Period Measurements (3616)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Valve Flows (3617)</summary>
		public readonly static DicomUID ValveFlows3617 = new DicomUID("1.2.840.10008.6.1.167", "Valve Flows (3617)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Flows (3618)</summary>
		public readonly static DicomUID HemodynamicFlows3618 = new DicomUID("1.2.840.10008.6.1.168", "Hemodynamic Flows (3618)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Resistance Measurements (3619)</summary>
		public readonly static DicomUID HemodynamicResistanceMeasurements3619 = new DicomUID("1.2.840.10008.6.1.169", "Hemodynamic Resistance Measurements (3619)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Ratios (3620)</summary>
		public readonly static DicomUID HemodynamicRatios3620 = new DicomUID("1.2.840.10008.6.1.170", "Hemodynamic Ratios (3620)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fractional Flow Reserve (3621)</summary>
		public readonly static DicomUID FractionalFlowReserve3621 = new DicomUID("1.2.840.10008.6.1.171", "Fractional Flow Reserve (3621)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Measurement Type (3627)</summary>
		public readonly static DicomUID MeasurementType3627 = new DicomUID("1.2.840.10008.6.1.172", "Measurement Type (3627)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Output Methods (3628)</summary>
		public readonly static DicomUID CardiacOutputMethods3628 = new DicomUID("1.2.840.10008.6.1.173", "Cardiac Output Methods (3628)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedure Intent (3629)</summary>
		public readonly static DicomUID ProcedureIntent3629 = new DicomUID("1.2.840.10008.6.1.174", "Procedure Intent (3629)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiovascular Anatomic Locations (3630)</summary>
		public readonly static DicomUID CardiovascularAnatomicLocations3630 = new DicomUID("1.2.840.10008.6.1.175", "Cardiovascular Anatomic Locations (3630)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hypertension (3640)</summary>
		public readonly static DicomUID Hypertension3640 = new DicomUID("1.2.840.10008.6.1.176", "Hypertension (3640)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Assessments (3641)</summary>
		public readonly static DicomUID HemodynamicAssessments3641 = new DicomUID("1.2.840.10008.6.1.177", "Hemodynamic Assessments (3641)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Degree Findings (3642)</summary>
		public readonly static DicomUID DegreeFindings3642 = new DicomUID("1.2.840.10008.6.1.178", "Degree Findings (3642)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hemodynamic Measurement Phase (3651)</summary>
		public readonly static DicomUID HemodynamicMeasurementPhase3651 = new DicomUID("1.2.840.10008.6.1.179", "Hemodynamic Measurement Phase (3651)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Body Surface Area Equations (3663)</summary>
		public readonly static DicomUID BodySurfaceAreaEquations3663 = new DicomUID("1.2.840.10008.6.1.180", "Body Surface Area Equations (3663)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Oxygen Consumption Equations and Tables (3664)</summary>
		public readonly static DicomUID OxygenConsumptionEquationsAndTables3664 = new DicomUID("1.2.840.10008.6.1.181", "Oxygen Consumption Equations and Tables (3664)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: P50 Equations (3666)</summary>
		public readonly static DicomUID P50Equations3666 = new DicomUID("1.2.840.10008.6.1.182", "P50 Equations (3666)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Framingham Scores (3667)</summary>
		public readonly static DicomUID FraminghamScores3667 = new DicomUID("1.2.840.10008.6.1.183", "Framingham Scores (3667)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Framingham Tables (3668)</summary>
		public readonly static DicomUID FraminghamTables3668 = new DicomUID("1.2.840.10008.6.1.184", "Framingham Tables (3668)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Procedure Types (3670)</summary>
		public readonly static DicomUID ECGProcedureTypes3670 = new DicomUID("1.2.840.10008.6.1.185", "ECG Procedure Types (3670)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Reason for ECG Exam (3671)</summary>
		public readonly static DicomUID ReasonForECGExam3671 = new DicomUID("1.2.840.10008.6.1.186", "Reason for ECG Exam (3671)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pacemakers (3672)</summary>
		public readonly static DicomUID Pacemakers3672 = new DicomUID("1.2.840.10008.6.1.187", "Pacemakers (3672)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Diagnosis (3673)</summary>
		public readonly static DicomUID Diagnosis3673 = new DicomUID("1.2.840.10008.6.1.188", "Diagnosis (3673)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Other Filters (3675)</summary>
		public readonly static DicomUID OtherFilters3675 = new DicomUID("1.2.840.10008.6.1.189", "Other Filters (3675)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lead Measurement Technique (3676)</summary>
		public readonly static DicomUID LeadMeasurementTechnique3676 = new DicomUID("1.2.840.10008.6.1.190", "Lead Measurement Technique (3676)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Summary Codes ECG (3677)</summary>
		public readonly static DicomUID SummaryCodesECG3677 = new DicomUID("1.2.840.10008.6.1.191", "Summary Codes ECG (3677)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: QT Correction Algorithms (3678)</summary>
		public readonly static DicomUID QTCorrectionAlgorithms3678 = new DicomUID("1.2.840.10008.6.1.192", "QT Correction Algorithms (3678)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Morphology Descriptions (3679)</summary>
		public readonly static DicomUID ECGMorphologyDescriptions3679 = new DicomUID("1.2.840.10008.6.1.193", "ECG Morphology Descriptions (3679)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Lead Noise Descriptions (3680)</summary>
		public readonly static DicomUID ECGLeadNoiseDescriptions3680 = new DicomUID("1.2.840.10008.6.1.194", "ECG Lead Noise Descriptions (3680)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Lead Noise Modifiers (3681)</summary>
		public readonly static DicomUID ECGLeadNoiseModifiers3681 = new DicomUID("1.2.840.10008.6.1.195", "ECG Lead Noise Modifiers (3681)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Probability (3682)</summary>
		public readonly static DicomUID Probability3682 = new DicomUID("1.2.840.10008.6.1.196", "Probability (3682)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Modifiers (3683)</summary>
		public readonly static DicomUID Modifiers3683 = new DicomUID("1.2.840.10008.6.1.197", "Modifiers (3683)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Trend (3684)</summary>
		public readonly static DicomUID Trend3684 = new DicomUID("1.2.840.10008.6.1.198", "Trend (3684)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Conjunctive Terms (3685)</summary>
		public readonly static DicomUID ConjunctiveTerms3685 = new DicomUID("1.2.840.10008.6.1.199", "Conjunctive Terms (3685)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Interpretive Statements (3686)</summary>
		public readonly static DicomUID ECGInterpretiveStatements3686 = new DicomUID("1.2.840.10008.6.1.200", "ECG Interpretive Statements (3686)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Electrophysiology Waveform Durations (3687)</summary>
		public readonly static DicomUID ElectrophysiologyWaveformDurations3687 = new DicomUID("1.2.840.10008.6.1.201", "Electrophysiology Waveform Durations (3687)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Electrophysiology Waveform Voltages (3688)</summary>
		public readonly static DicomUID ElectrophysiologyWaveformVoltages3688 = new DicomUID("1.2.840.10008.6.1.202", "Electrophysiology Waveform Voltages (3688)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cath Diagnosis (3700)</summary>
		public readonly static DicomUID CathDiagnosis3700 = new DicomUID("1.2.840.10008.6.1.203", "Cath Diagnosis (3700)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Valves and Tracts (3701)</summary>
		public readonly static DicomUID CardiacValvesAndTracts3701 = new DicomUID("1.2.840.10008.6.1.204", "Cardiac Valves and Tracts (3701)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Wall Motion (3703)</summary>
		public readonly static DicomUID WallMotion3703 = new DicomUID("1.2.840.10008.6.1.205", "Wall Motion (3703)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Myocardium Wall Morphology Findings (3704)</summary>
		public readonly static DicomUID MyocardiumWallMorphologyFindings3704 = new DicomUID("1.2.840.10008.6.1.206", "Myocardium Wall Morphology Findings (3704)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chamber Size (3705)</summary>
		public readonly static DicomUID ChamberSize3705 = new DicomUID("1.2.840.10008.6.1.207", "Chamber Size (3705)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Overall Contractility (3706)</summary>
		public readonly static DicomUID OverallContractility3706 = new DicomUID("1.2.840.10008.6.1.208", "Overall Contractility (3706)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: VSD Description (3707)</summary>
		public readonly static DicomUID VSDDescription3707 = new DicomUID("1.2.840.10008.6.1.209", "VSD Description (3707)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Aortic Root Description (3709)</summary>
		public readonly static DicomUID AorticRootDescription3709 = new DicomUID("1.2.840.10008.6.1.210", "Aortic Root Description (3709)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Coronary Dominance (3710)</summary>
		public readonly static DicomUID CoronaryDominance3710 = new DicomUID("1.2.840.10008.6.1.211", "Coronary Dominance (3710)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Valvular Abnormalities (3711)</summary>
		public readonly static DicomUID ValvularAbnormalities3711 = new DicomUID("1.2.840.10008.6.1.212", "Valvular Abnormalities (3711)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vessel Descriptors (3712)</summary>
		public readonly static DicomUID VesselDescriptors3712 = new DicomUID("1.2.840.10008.6.1.213", "Vessel Descriptors (3712)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: TIMI Flow Characteristics (3713)</summary>
		public readonly static DicomUID TIMIFlowCharacteristics3713 = new DicomUID("1.2.840.10008.6.1.214", "TIMI Flow Characteristics (3713)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Thrombus (3714)</summary>
		public readonly static DicomUID Thrombus3714 = new DicomUID("1.2.840.10008.6.1.215", "Thrombus (3714)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lesion Margin (3715)</summary>
		public readonly static DicomUID LesionMargin3715 = new DicomUID("1.2.840.10008.6.1.216", "Lesion Margin (3715)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Severity (3716)</summary>
		public readonly static DicomUID Severity3716 = new DicomUID("1.2.840.10008.6.1.217", "Severity (3716)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Myocardial Wall Segments (3717)</summary>
		public readonly static DicomUID MyocardialWallSegments3717 = new DicomUID("1.2.840.10008.6.1.218", "Myocardial Wall Segments (3717)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Myocardial Wall Segments in Projection (3718)</summary>
		public readonly static DicomUID MyocardialWallSegmentsInProjection3718 = new DicomUID("1.2.840.10008.6.1.219", "Myocardial Wall Segments in Projection (3718)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Canadian Clinical Classification (3719)</summary>
		public readonly static DicomUID CanadianClinicalClassification3719 = new DicomUID("1.2.840.10008.6.1.220", "Canadian Clinical Classification (3719)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac History Dates (Retired) (3720)</summary>
		public readonly static DicomUID CardiacHistoryDatesRetired3720 = new DicomUID("1.2.840.10008.6.1.221", "Cardiac History Dates (Retired) (3720)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiovascular Surgeries (3721)</summary>
		public readonly static DicomUID CardiovascularSurgeries3721 = new DicomUID("1.2.840.10008.6.1.222", "Cardiovascular Surgeries (3721)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Diabetic Therapy (3722)</summary>
		public readonly static DicomUID DiabeticTherapy3722 = new DicomUID("1.2.840.10008.6.1.223", "Diabetic Therapy (3722)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: MI Types (3723)</summary>
		public readonly static DicomUID MITypes3723 = new DicomUID("1.2.840.10008.6.1.224", "MI Types (3723)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Smoking History (3724)</summary>
		public readonly static DicomUID SmokingHistory3724 = new DicomUID("1.2.840.10008.6.1.225", "Smoking History (3724)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Indications for Coronary Intervention (3726)</summary>
		public readonly static DicomUID IndicationsForCoronaryIntervention3726 = new DicomUID("1.2.840.10008.6.1.226", "Indications for Coronary Intervention (3726)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Indications for Catheterization (3727)</summary>
		public readonly static DicomUID IndicationsForCatheterization3727 = new DicomUID("1.2.840.10008.6.1.227", "Indications for Catheterization (3727)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cath Findings (3728)</summary>
		public readonly static DicomUID CathFindings3728 = new DicomUID("1.2.840.10008.6.1.228", "Cath Findings (3728)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Admission Status (3729)</summary>
		public readonly static DicomUID AdmissionStatus3729 = new DicomUID("1.2.840.10008.6.1.229", "Admission Status (3729)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Insurance Payor (3730)</summary>
		public readonly static DicomUID InsurancePayor3730 = new DicomUID("1.2.840.10008.6.1.230", "Insurance Payor (3730)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Primary Cause of Death (3733)</summary>
		public readonly static DicomUID PrimaryCauseOfDeath3733 = new DicomUID("1.2.840.10008.6.1.231", "Primary Cause of Death (3733)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Acute Coronary Syndrome Time Period (3735)</summary>
		public readonly static DicomUID AcuteCoronarySyndromeTimePeriod3735 = new DicomUID("1.2.840.10008.6.1.232", "Acute Coronary Syndrome Time Period (3735)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: NYHA Classification (3736)</summary>
		public readonly static DicomUID NYHAClassification3736 = new DicomUID("1.2.840.10008.6.1.233", "NYHA Classification (3736)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Non-Invasive Test - Ischemia (3737)</summary>
		public readonly static DicomUID NonInvasiveTestIschemia3737 = new DicomUID("1.2.840.10008.6.1.234", "Non-Invasive Test - Ischemia (3737)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pre-Cath Angina Type (3738)</summary>
		public readonly static DicomUID PreCathAnginaType3738 = new DicomUID("1.2.840.10008.6.1.235", "Pre-Cath Angina Type (3738)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cath Procedure Type (3739)</summary>
		public readonly static DicomUID CathProcedureType3739 = new DicomUID("1.2.840.10008.6.1.236", "Cath Procedure Type (3739)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Thrombolytic Administration (3740)</summary>
		public readonly static DicomUID ThrombolyticAdministration3740 = new DicomUID("1.2.840.10008.6.1.237", "Thrombolytic Administration (3740)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Medication Administration, Lab Visit (3741)</summary>
		public readonly static DicomUID MedicationAdministrationLabVisit3741 = new DicomUID("1.2.840.10008.6.1.238", "Medication Administration, Lab Visit (3741)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Medication Administration, PCI (3742)</summary>
		public readonly static DicomUID MedicationAdministrationPCI3742 = new DicomUID("1.2.840.10008.6.1.239", "Medication Administration, PCI (3742)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Clopidogrel/Ticlopidine Administration (3743)</summary>
		public readonly static DicomUID ClopidogrelTiclopidineAdministration3743 = new DicomUID("1.2.840.10008.6.1.240", "Clopidogrel/Ticlopidine Administration (3743)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: EF Testing Method (3744)</summary>
		public readonly static DicomUID EFTestingMethod3744 = new DicomUID("1.2.840.10008.6.1.241", "EF Testing Method (3744)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calculation Method (3745)</summary>
		public readonly static DicomUID CalculationMethod3745 = new DicomUID("1.2.840.10008.6.1.242", "Calculation Method (3745)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Percutaneous Entry Site (3746)</summary>
		public readonly static DicomUID PercutaneousEntrySite3746 = new DicomUID("1.2.840.10008.6.1.243", "Percutaneous Entry Site (3746)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Percutaneous Closure (3747)</summary>
		public readonly static DicomUID PercutaneousClosure3747 = new DicomUID("1.2.840.10008.6.1.244", "Percutaneous Closure (3747)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Angiographic EF Testing Method (3748)</summary>
		public readonly static DicomUID AngiographicEFTestingMethod3748 = new DicomUID("1.2.840.10008.6.1.245", "Angiographic EF Testing Method (3748)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: PCI Procedure Result (3749)</summary>
		public readonly static DicomUID PCIProcedureResult3749 = new DicomUID("1.2.840.10008.6.1.246", "PCI Procedure Result (3749)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Previously Dilated Lesion (3750)</summary>
		public readonly static DicomUID PreviouslyDilatedLesion3750 = new DicomUID("1.2.840.10008.6.1.247", "Previously Dilated Lesion (3750)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Guidewire Crossing (3752)</summary>
		public readonly static DicomUID GuidewireCrossing3752 = new DicomUID("1.2.840.10008.6.1.248", "Guidewire Crossing (3752)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Complications (3754)</summary>
		public readonly static DicomUID VascularComplications3754 = new DicomUID("1.2.840.10008.6.1.249", "Vascular Complications (3754)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cath Complications (3755)</summary>
		public readonly static DicomUID CathComplications3755 = new DicomUID("1.2.840.10008.6.1.250", "Cath Complications (3755)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Patient Risk Factors (3756)</summary>
		public readonly static DicomUID CardiacPatientRiskFactors3756 = new DicomUID("1.2.840.10008.6.1.251", "Cardiac Patient Risk Factors (3756)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Diagnostic Procedures (3757)</summary>
		public readonly static DicomUID CardiacDiagnosticProcedures3757 = new DicomUID("1.2.840.10008.6.1.252", "Cardiac Diagnostic Procedures (3757)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiovascular Family History (3758)</summary>
		public readonly static DicomUID CardiovascularFamilyHistory3758 = new DicomUID("1.2.840.10008.6.1.253", "Cardiovascular Family History (3758)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Hypertension Therapy (3760)</summary>
		public readonly static DicomUID HypertensionTherapy3760 = new DicomUID("1.2.840.10008.6.1.254", "Hypertension Therapy (3760)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Antilipemic agents (3761)</summary>
		public readonly static DicomUID AntilipemicAgents3761 = new DicomUID("1.2.840.10008.6.1.255", "Antilipemic agents (3761)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Antiarrhythmic agents (3762)</summary>
		public readonly static DicomUID AntiarrhythmicAgents3762 = new DicomUID("1.2.840.10008.6.1.256", "Antiarrhythmic agents (3762)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Myocardial Infarction Therapies (3764)</summary>
		public readonly static DicomUID MyocardialInfarctionTherapies3764 = new DicomUID("1.2.840.10008.6.1.257", "Myocardial Infarction Therapies (3764)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Concern Types (3769)</summary>
		public readonly static DicomUID ConcernTypes3769 = new DicomUID("1.2.840.10008.6.1.258", "Concern Types (3769)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Problem Status (3770)</summary>
		public readonly static DicomUID ProblemStatus3770 = new DicomUID("1.2.840.10008.6.1.259", "Problem Status (3770)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Health Status (3772)</summary>
		public readonly static DicomUID HealthStatus3772 = new DicomUID("1.2.840.10008.6.1.260", "Health Status (3772)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Use Status (3773)</summary>
		public readonly static DicomUID UseStatus3773 = new DicomUID("1.2.840.10008.6.1.261", "Use Status (3773)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Social History (3774)</summary>
		public readonly static DicomUID SocialHistory3774 = new DicomUID("1.2.840.10008.6.1.262", "Social History (3774)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Implanted Devices (3777)</summary>
		public readonly static DicomUID ImplantedDevices3777 = new DicomUID("1.2.840.10008.6.1.263", "Implanted Devices (3777)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Plaque Structures (3802)</summary>
		public readonly static DicomUID PlaqueStructures3802 = new DicomUID("1.2.840.10008.6.1.264", "Plaque Structures (3802)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stenosis Measurement Methods (3804)</summary>
		public readonly static DicomUID StenosisMeasurementMethods3804 = new DicomUID("1.2.840.10008.6.1.265", "Stenosis Measurement Methods (3804)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stenosis Types (3805)</summary>
		public readonly static DicomUID StenosisTypes3805 = new DicomUID("1.2.840.10008.6.1.266", "Stenosis Types (3805)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stenosis Shape (3806)</summary>
		public readonly static DicomUID StenosisShape3806 = new DicomUID("1.2.840.10008.6.1.267", "Stenosis Shape (3806)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Volume Measurement Methods (3807)</summary>
		public readonly static DicomUID VolumeMeasurementMethods3807 = new DicomUID("1.2.840.10008.6.1.268", "Volume Measurement Methods (3807)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Aneurysm Types (3808)</summary>
		public readonly static DicomUID AneurysmTypes3808 = new DicomUID("1.2.840.10008.6.1.269", "Aneurysm Types (3808)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Associated Conditions (3809)</summary>
		public readonly static DicomUID AssociatedConditions3809 = new DicomUID("1.2.840.10008.6.1.270", "Associated Conditions (3809)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Morphology (3810)</summary>
		public readonly static DicomUID VascularMorphology3810 = new DicomUID("1.2.840.10008.6.1.271", "Vascular Morphology (3810)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stent Findings (3813)</summary>
		public readonly static DicomUID StentFindings3813 = new DicomUID("1.2.840.10008.6.1.272", "Stent Findings (3813)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stent Composition (3814)</summary>
		public readonly static DicomUID StentComposition3814 = new DicomUID("1.2.840.10008.6.1.273", "Stent Composition (3814)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Source of Vascular Finding (3815)</summary>
		public readonly static DicomUID SourceOfVascularFinding3815 = new DicomUID("1.2.840.10008.6.1.274", "Source of Vascular Finding (3815)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Sclerosis Types (3817)</summary>
		public readonly static DicomUID VascularSclerosisTypes3817 = new DicomUID("1.2.840.10008.6.1.275", "Vascular Sclerosis Types (3817)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Non-invasive Vascular Procedures (3820)</summary>
		public readonly static DicomUID NonInvasiveVascularProcedures3820 = new DicomUID("1.2.840.10008.6.1.276", "Non-invasive Vascular Procedures (3820)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Papillary Muscle Included/Excluded (3821)</summary>
		public readonly static DicomUID PapillaryMuscleIncludedExcluded3821 = new DicomUID("1.2.840.10008.6.1.277", "Papillary Muscle Included/Excluded (3821)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Respiratory Status (3823)</summary>
		public readonly static DicomUID RespiratoryStatus3823 = new DicomUID("1.2.840.10008.6.1.278", "Respiratory Status (3823)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Heart Rhythm (3826)</summary>
		public readonly static DicomUID HeartRhythm3826 = new DicomUID("1.2.840.10008.6.1.279", "Heart Rhythm (3826)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vessel Segments (3827)</summary>
		public readonly static DicomUID VesselSegments3827 = new DicomUID("1.2.840.10008.6.1.280", "Vessel Segments (3827)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pulmonary Arteries (3829)</summary>
		public readonly static DicomUID PulmonaryArteries3829 = new DicomUID("1.2.840.10008.6.1.281", "Pulmonary Arteries (3829)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stenosis Length (3831)</summary>
		public readonly static DicomUID StenosisLength3831 = new DicomUID("1.2.840.10008.6.1.282", "Stenosis Length (3831)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stenosis Grade (3832)</summary>
		public readonly static DicomUID StenosisGrade3832 = new DicomUID("1.2.840.10008.6.1.283", "Stenosis Grade (3832)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ejection Fraction (3833)</summary>
		public readonly static DicomUID CardiacEjectionFraction3833 = new DicomUID("1.2.840.10008.6.1.284", "Cardiac Ejection Fraction (3833)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Volume Measurements (3835)</summary>
		public readonly static DicomUID CardiacVolumeMeasurements3835 = new DicomUID("1.2.840.10008.6.1.285", "Cardiac Volume Measurements (3835)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Time-based Perfusion Measurements (3836)</summary>
		public readonly static DicomUID TimeBasedPerfusionMeasurements3836 = new DicomUID("1.2.840.10008.6.1.286", "Time-based Perfusion Measurements (3836)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fiducial Feature (3837)</summary>
		public readonly static DicomUID FiducialFeature3837 = new DicomUID("1.2.840.10008.6.1.287", "Fiducial Feature (3837)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Diameter Derivation (3838)</summary>
		public readonly static DicomUID DiameterDerivation3838 = new DicomUID("1.2.840.10008.6.1.288", "Diameter Derivation (3838)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Coronary Veins (3839)</summary>
		public readonly static DicomUID CoronaryVeins3839 = new DicomUID("1.2.840.10008.6.1.289", "Coronary Veins (3839)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pulmonary Veins (3840)</summary>
		public readonly static DicomUID PulmonaryVeins3840 = new DicomUID("1.2.840.10008.6.1.290", "Pulmonary Veins (3840)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Myocardial Subsegment (3843)</summary>
		public readonly static DicomUID MyocardialSubsegment3843 = new DicomUID("1.2.840.10008.6.1.291", "Myocardial Subsegment (3843)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Partial View Section for Mammography (4005)</summary>
		public readonly static DicomUID PartialViewSectionForMammography4005 = new DicomUID("1.2.840.10008.6.1.292", "Partial View Section for Mammography (4005)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: DX Anatomy Imaged (4009)</summary>
		public readonly static DicomUID DXAnatomyImaged4009 = new DicomUID("1.2.840.10008.6.1.293", "DX Anatomy Imaged (4009)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: DX View (4010)</summary>
		public readonly static DicomUID DXView4010 = new DicomUID("1.2.840.10008.6.1.294", "DX View (4010)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: DX View Modifier (4011)</summary>
		public readonly static DicomUID DXViewModifier4011 = new DicomUID("1.2.840.10008.6.1.295", "DX View Modifier (4011)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Projection Eponymous Name (4012)</summary>
		public readonly static DicomUID ProjectionEponymousName4012 = new DicomUID("1.2.840.10008.6.1.296", "Projection Eponymous Name (4012)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Anatomic Region for Mammography (4013)</summary>
		public readonly static DicomUID AnatomicRegionForMammography4013 = new DicomUID("1.2.840.10008.6.1.297", "Anatomic Region for Mammography (4013)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: View for Mammography (4014)</summary>
		public readonly static DicomUID ViewForMammography4014 = new DicomUID("1.2.840.10008.6.1.298", "View for Mammography (4014)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: View Modifier for Mammography (4015)</summary>
		public readonly static DicomUID ViewModifierForMammography4015 = new DicomUID("1.2.840.10008.6.1.299", "View Modifier for Mammography (4015)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Anatomic Region for Intra-oral Radiography (4016)</summary>
		public readonly static DicomUID AnatomicRegionForIntraOralRadiography4016 = new DicomUID("1.2.840.10008.6.1.300", "Anatomic Region for Intra-oral Radiography (4016)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Anatomic Region Modifier for Intra-oral Radiography (4017)</summary>
		public readonly static DicomUID AnatomicRegionModifierForIntraOralRadiography4017 = new DicomUID("1.2.840.10008.6.1.301", "Anatomic Region Modifier for Intra-oral Radiography (4017)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Primary Anatomic Structure for Intra-oral Radiography (Permanent Dentition - Designation of Teeth) (4018)</summary>
		public readonly static DicomUID PrimaryAnatomicStructureForIntraOralRadiographyPermanentDentitionDesignationOfTeeth4018 = new DicomUID("1.2.840.10008.6.1.302", "Primary Anatomic Structure for Intra-oral Radiography (Permanent Dentition - Designation of Teeth) (4018)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Primary Anatomic Structure for Intra-oral Radiography (Deciduous Dentition - Designation of Teeth) (4019)</summary>
		public readonly static DicomUID PrimaryAnatomicStructureForIntraOralRadiographyDeciduousDentitionDesignationOfTeeth4019 = new DicomUID("1.2.840.10008.6.1.303", "Primary Anatomic Structure for Intra-oral Radiography (Deciduous Dentition - Designation of Teeth) (4019)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: PET Radionuclide (4020)</summary>
		public readonly static DicomUID PETRadionuclide4020 = new DicomUID("1.2.840.10008.6.1.304", "PET Radionuclide (4020)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: PET Radiopharmaceutical (4021)</summary>
		public readonly static DicomUID PETRadiopharmaceutical4021 = new DicomUID("1.2.840.10008.6.1.305", "PET Radiopharmaceutical (4021)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Craniofacial Anatomic Regions (4028)</summary>
		public readonly static DicomUID CraniofacialAnatomicRegions4028 = new DicomUID("1.2.840.10008.6.1.306", "Craniofacial Anatomic Regions (4028)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: CT and MR Anatomy Imaged (4030)</summary>
		public readonly static DicomUID CTAndMRAnatomyImaged4030 = new DicomUID("1.2.840.10008.6.1.307", "CT and MR Anatomy Imaged (4030)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Common Anatomic Regions (4031)</summary>
		public readonly static DicomUID CommonAnatomicRegions4031 = new DicomUID("1.2.840.10008.6.1.308", "Common Anatomic Regions (4031)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: MR Spectroscopy Metabolites (4032)</summary>
		public readonly static DicomUID MRSpectroscopyMetabolites4032 = new DicomUID("1.2.840.10008.6.1.309", "MR Spectroscopy Metabolites (4032)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: MR Proton Spectroscopy Metabolites (4033)</summary>
		public readonly static DicomUID MRProtonSpectroscopyMetabolites4033 = new DicomUID("1.2.840.10008.6.1.310", "MR Proton Spectroscopy Metabolites (4033)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Endoscopy Anatomic Regions (4040)</summary>
		public readonly static DicomUID EndoscopyAnatomicRegions4040 = new DicomUID("1.2.840.10008.6.1.311", "Endoscopy Anatomic Regions (4040)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: XA/XRF Anatomy Imaged (4042)</summary>
		public readonly static DicomUID XAXRFAnatomyImaged4042 = new DicomUID("1.2.840.10008.6.1.312", "XA/XRF Anatomy Imaged (4042)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Drug or Contrast Agent Characteristics (4050)</summary>
		public readonly static DicomUID DrugOrContrastAgentCharacteristics4050 = new DicomUID("1.2.840.10008.6.1.313", "Drug or Contrast Agent Characteristics (4050)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: General Devices (4051)</summary>
		public readonly static DicomUID GeneralDevices4051 = new DicomUID("1.2.840.10008.6.1.314", "General Devices (4051)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Phantom Devices (4052)</summary>
		public readonly static DicomUID PhantomDevices4052 = new DicomUID("1.2.840.10008.6.1.315", "Phantom Devices (4052)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Imaging Agent (4200)</summary>
		public readonly static DicomUID OphthalmicImagingAgent4200 = new DicomUID("1.2.840.10008.6.1.316", "Ophthalmic Imaging Agent (4200)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Patient Eye Movement Command (4201)</summary>
		public readonly static DicomUID PatientEyeMovementCommand4201 = new DicomUID("1.2.840.10008.6.1.317", "Patient Eye Movement Command (4201)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Photography Acquisition Device (4202)</summary>
		public readonly static DicomUID OphthalmicPhotographyAcquisitionDevice4202 = new DicomUID("1.2.840.10008.6.1.318", "Ophthalmic Photography Acquisition Device (4202)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Photography Illumination (4203)</summary>
		public readonly static DicomUID OphthalmicPhotographyIllumination4203 = new DicomUID("1.2.840.10008.6.1.319", "Ophthalmic Photography Illumination (4203)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Filter (4204)</summary>
		public readonly static DicomUID OphthalmicFilter4204 = new DicomUID("1.2.840.10008.6.1.320", "Ophthalmic Filter (4204)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Lens (4205)</summary>
		public readonly static DicomUID OphthalmicLens4205 = new DicomUID("1.2.840.10008.6.1.321", "Ophthalmic Lens (4205)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Channel Description (4206)</summary>
		public readonly static DicomUID OphthalmicChannelDescription4206 = new DicomUID("1.2.840.10008.6.1.322", "Ophthalmic Channel Description (4206)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Image Position (4207)</summary>
		public readonly static DicomUID OphthalmicImagePosition4207 = new DicomUID("1.2.840.10008.6.1.323", "Ophthalmic Image Position (4207)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mydriatic Agent (4208)</summary>
		public readonly static DicomUID MydriaticAgent4208 = new DicomUID("1.2.840.10008.6.1.324", "Mydriatic Agent (4208)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Anatomic Structure Imaged (4209)</summary>
		public readonly static DicomUID OphthalmicAnatomicStructureImaged4209 = new DicomUID("1.2.840.10008.6.1.325", "Ophthalmic Anatomic Structure Imaged (4209)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Tomography Acquisition Device (4210)</summary>
		public readonly static DicomUID OphthalmicTomographyAcquisitionDevice4210 = new DicomUID("1.2.840.10008.6.1.326", "Ophthalmic Tomography Acquisition Device (4210)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic OCT Anatomic Structure Imaged (4211)</summary>
		public readonly static DicomUID OphthalmicOCTAnatomicStructureImaged4211 = new DicomUID("1.2.840.10008.6.1.327", "Ophthalmic OCT Anatomic Structure Imaged (4211)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Languages (5000)</summary>
		public readonly static DicomUID Languages5000 = new DicomUID("1.2.840.10008.6.1.328", "Languages (5000)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Countries (5001)</summary>
		public readonly static DicomUID Countries5001 = new DicomUID("1.2.840.10008.6.1.329", "Countries (5001)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Overall Breast Composition (6000)</summary>
		public readonly static DicomUID OverallBreastComposition6000 = new DicomUID("1.2.840.10008.6.1.330", "Overall Breast Composition (6000)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Overall Breast Composition from BI-RADS® (6001)</summary>
		public readonly static DicomUID OverallBreastCompositionFromBIRADS6001 = new DicomUID("1.2.840.10008.6.1.331", "Overall Breast Composition from BI-RADS® (6001)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Change Since Last Mammogram or Prior Surgery (6002)</summary>
		public readonly static DicomUID ChangeSinceLastMammogramOrPriorSurgery6002 = new DicomUID("1.2.840.10008.6.1.332", "Change Since Last Mammogram or Prior Surgery (6002)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Change Since Last Mammogram or Prior Surgery from BI-RADS® (6003)</summary>
		public readonly static DicomUID ChangeSinceLastMammogramOrPriorSurgeryFromBIRADS6003 = new DicomUID("1.2.840.10008.6.1.333", "Change Since Last Mammogram or Prior Surgery from BI-RADS® (6003)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Characteristics of Shape (6004)</summary>
		public readonly static DicomUID MammographyCharacteristicsOfShape6004 = new DicomUID("1.2.840.10008.6.1.334", "Mammography Characteristics of Shape (6004)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Characteristics of Shape from BI-RADS® (6005)</summary>
		public readonly static DicomUID CharacteristicsOfShapeFromBIRADS6005 = new DicomUID("1.2.840.10008.6.1.335", "Characteristics of Shape from BI-RADS® (6005)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Characteristics of Margin (6006)</summary>
		public readonly static DicomUID MammographyCharacteristicsOfMargin6006 = new DicomUID("1.2.840.10008.6.1.336", "Mammography Characteristics of Margin (6006)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Characteristics of Margin from BI-RADS® (6007)</summary>
		public readonly static DicomUID CharacteristicsOfMarginFromBIRADS6007 = new DicomUID("1.2.840.10008.6.1.337", "Characteristics of Margin from BI-RADS® (6007)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Density Modifier (6008)</summary>
		public readonly static DicomUID DensityModifier6008 = new DicomUID("1.2.840.10008.6.1.338", "Density Modifier (6008)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Density Modifier from BI-RADS® (6009)</summary>
		public readonly static DicomUID DensityModifierFromBIRADS6009 = new DicomUID("1.2.840.10008.6.1.339", "Density Modifier from BI-RADS® (6009)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Calcification Types (6010)</summary>
		public readonly static DicomUID MammographyCalcificationTypes6010 = new DicomUID("1.2.840.10008.6.1.340", "Mammography Calcification Types (6010)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calcification Types from BI-RADS® (6011)</summary>
		public readonly static DicomUID CalcificationTypesFromBIRADS6011 = new DicomUID("1.2.840.10008.6.1.341", "Calcification Types from BI-RADS® (6011)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calcification Distribution Modifier (6012)</summary>
		public readonly static DicomUID CalcificationDistributionModifier6012 = new DicomUID("1.2.840.10008.6.1.342", "Calcification Distribution Modifier (6012)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calcification Distribution Modifier from BI-RADS® (6013)</summary>
		public readonly static DicomUID CalcificationDistributionModifierFromBIRADS6013 = new DicomUID("1.2.840.10008.6.1.343", "Calcification Distribution Modifier from BI-RADS® (6013)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Single Image Finding (6014)</summary>
		public readonly static DicomUID MammographySingleImageFinding6014 = new DicomUID("1.2.840.10008.6.1.344", "Mammography Single Image Finding (6014)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Single Image Finding from BI-RADS® (6015)</summary>
		public readonly static DicomUID SingleImageFindingFromBIRADS6015 = new DicomUID("1.2.840.10008.6.1.345", "Single Image Finding from BI-RADS® (6015)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Composite Feature (6016)</summary>
		public readonly static DicomUID MammographyCompositeFeature6016 = new DicomUID("1.2.840.10008.6.1.346", "Mammography Composite Feature (6016)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Composite Feature from BI-RADS® (6017)</summary>
		public readonly static DicomUID CompositeFeatureFromBIRADS6017 = new DicomUID("1.2.840.10008.6.1.347", "Composite Feature from BI-RADS® (6017)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Clockface Location or Region (6018)</summary>
		public readonly static DicomUID ClockfaceLocationOrRegion6018 = new DicomUID("1.2.840.10008.6.1.348", "Clockface Location or Region (6018)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Clockface Location or Region from BI-RADS® (6019)</summary>
		public readonly static DicomUID ClockfaceLocationOrRegionFromBIRADS6019 = new DicomUID("1.2.840.10008.6.1.349", "Clockface Location or Region from BI-RADS® (6019)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Quadrant Location (6020)</summary>
		public readonly static DicomUID QuadrantLocation6020 = new DicomUID("1.2.840.10008.6.1.350", "Quadrant Location (6020)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Quadrant Location from BI-RADS® (6021)</summary>
		public readonly static DicomUID QuadrantLocationFromBIRADS6021 = new DicomUID("1.2.840.10008.6.1.351", "Quadrant Location from BI-RADS® (6021)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Side (6022)</summary>
		public readonly static DicomUID Side6022 = new DicomUID("1.2.840.10008.6.1.352", "Side (6022)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Side from BI-RADS® (6023)</summary>
		public readonly static DicomUID SideFromBIRADS6023 = new DicomUID("1.2.840.10008.6.1.353", "Side from BI-RADS® (6023)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Depth (6024)</summary>
		public readonly static DicomUID Depth6024 = new DicomUID("1.2.840.10008.6.1.354", "Depth (6024)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Depth from BI-RADS® (6025)</summary>
		public readonly static DicomUID DepthFromBIRADS6025 = new DicomUID("1.2.840.10008.6.1.355", "Depth from BI-RADS® (6025)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Assessment (6026)</summary>
		public readonly static DicomUID MammographyAssessment6026 = new DicomUID("1.2.840.10008.6.1.356", "Mammography Assessment (6026)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Assessment from BI-RADS® (6027)</summary>
		public readonly static DicomUID AssessmentFromBIRADS6027 = new DicomUID("1.2.840.10008.6.1.357", "Assessment from BI-RADS® (6027)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Recommended Follow-up (6028)</summary>
		public readonly static DicomUID MammographyRecommendedFollowUp6028 = new DicomUID("1.2.840.10008.6.1.358", "Mammography Recommended Follow-up (6028)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Recommended Follow-up from BI-RADS® (6029)</summary>
		public readonly static DicomUID RecommendedFollowUpFromBIRADS6029 = new DicomUID("1.2.840.10008.6.1.359", "Recommended Follow-up from BI-RADS® (6029)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Pathology Codes (6030)</summary>
		public readonly static DicomUID MammographyPathologyCodes6030 = new DicomUID("1.2.840.10008.6.1.360", "Mammography Pathology Codes (6030)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Benign Pathology Codes from BI-RADS® (6031)</summary>
		public readonly static DicomUID BenignPathologyCodesFromBIRADS6031 = new DicomUID("1.2.840.10008.6.1.361", "Benign Pathology Codes from BI-RADS® (6031)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: High Risk Lesions Pathology Codes from BI-RADS® (6032)</summary>
		public readonly static DicomUID HighRiskLesionsPathologyCodesFromBIRADS6032 = new DicomUID("1.2.840.10008.6.1.362", "High Risk Lesions Pathology Codes from BI-RADS® (6032)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Malignant Pathology Codes from BI-RADS® (6033)</summary>
		public readonly static DicomUID MalignantPathologyCodesFromBIRADS6033 = new DicomUID("1.2.840.10008.6.1.363", "Malignant Pathology Codes from BI-RADS® (6033)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intended Use of CAD Output (6034)</summary>
		public readonly static DicomUID IntendedUseOfCADOutput6034 = new DicomUID("1.2.840.10008.6.1.364", "Intended Use of CAD Output (6034)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Composite Feature Relations (6035)</summary>
		public readonly static DicomUID CompositeFeatureRelations6035 = new DicomUID("1.2.840.10008.6.1.365", "Composite Feature Relations (6035)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Scope of Feature (6036)</summary>
		public readonly static DicomUID ScopeOfFeature6036 = new DicomUID("1.2.840.10008.6.1.366", "Scope of Feature (6036)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Quantitative Temporal Difference Type (6037)</summary>
		public readonly static DicomUID MammographyQuantitativeTemporalDifferenceType6037 = new DicomUID("1.2.840.10008.6.1.367", "Mammography Quantitative Temporal Difference Type (6037)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Qualitative Temporal Difference Type (6038)</summary>
		public readonly static DicomUID MammographyQualitativeTemporalDifferenceType6038 = new DicomUID("1.2.840.10008.6.1.368", "Mammography Qualitative Temporal Difference Type (6038)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Nipple Characteristic (6039)</summary>
		public readonly static DicomUID NippleCharacteristic6039 = new DicomUID("1.2.840.10008.6.1.369", "Nipple Characteristic (6039)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Non-Lesion Object Type (6040)</summary>
		public readonly static DicomUID NonLesionObjectType6040 = new DicomUID("1.2.840.10008.6.1.370", "Non-Lesion Object Type (6040)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Image Quality Finding (6041)</summary>
		public readonly static DicomUID MammographyImageQualityFinding6041 = new DicomUID("1.2.840.10008.6.1.371", "Mammography Image Quality Finding (6041)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Status of Results (6042)</summary>
		public readonly static DicomUID StatusOfResults6042 = new DicomUID("1.2.840.10008.6.1.372", "Status of Results (6042)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Types of Mammography CAD Analysis (6043)</summary>
		public readonly static DicomUID TypesOfMammographyCADAnalysis6043 = new DicomUID("1.2.840.10008.6.1.373", "Types of Mammography CAD Analysis (6043)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Types of Image Quality Assessment (6044)</summary>
		public readonly static DicomUID TypesOfImageQualityAssessment6044 = new DicomUID("1.2.840.10008.6.1.374", "Types of Image Quality Assessment (6044)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammography Types of Quality Control Standard (6045)</summary>
		public readonly static DicomUID MammographyTypesOfQualityControlStandard6045 = new DicomUID("1.2.840.10008.6.1.375", "Mammography Types of Quality Control Standard (6045)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units of Follow-up Interval (6046)</summary>
		public readonly static DicomUID UnitsOfFollowUpInterval6046 = new DicomUID("1.2.840.10008.6.1.376", "Units of Follow-up Interval (6046)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: CAD Processing and Findings Summary (6047)</summary>
		public readonly static DicomUID CADProcessingAndFindingsSummary6047 = new DicomUID("1.2.840.10008.6.1.377", "CAD Processing and Findings Summary (6047)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: CAD Operating Point Axis Label (6048)</summary>
		public readonly static DicomUID CADOperatingPointAxisLabel6048 = new DicomUID("1.2.840.10008.6.1.378", "CAD Operating Point Axis Label (6048)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Procedure Reported (6050)</summary>
		public readonly static DicomUID BreastProcedureReported6050 = new DicomUID("1.2.840.10008.6.1.379", "Breast Procedure Reported (6050)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Procedure Reason (6051)</summary>
		public readonly static DicomUID BreastProcedureReason6051 = new DicomUID("1.2.840.10008.6.1.380", "Breast Procedure Reason (6051)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Imaging Report section title (6052)</summary>
		public readonly static DicomUID BreastImagingReportSectionTitle6052 = new DicomUID("1.2.840.10008.6.1.381", "Breast Imaging Report section title (6052)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Imaging Report Elements (6053)</summary>
		public readonly static DicomUID BreastImagingReportElements6053 = new DicomUID("1.2.840.10008.6.1.382", "Breast Imaging Report Elements (6053)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Imaging Findings (6054)</summary>
		public readonly static DicomUID BreastImagingFindings6054 = new DicomUID("1.2.840.10008.6.1.383", "Breast Imaging Findings (6054)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Clinical Finding or Indicated Problem (6055)</summary>
		public readonly static DicomUID BreastClinicalFindingOrIndicatedProblem6055 = new DicomUID("1.2.840.10008.6.1.384", "Breast Clinical Finding or Indicated Problem (6055)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Associated Findings for Breast (6056)</summary>
		public readonly static DicomUID AssociatedFindingsForBreast6056 = new DicomUID("1.2.840.10008.6.1.385", "Associated Findings for Breast (6056)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ductography Findings for Breast (6057)</summary>
		public readonly static DicomUID DuctographyFindingsForBreast6057 = new DicomUID("1.2.840.10008.6.1.386", "Ductography Findings for Breast (6057)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedure Modifiers for Breast (6058)</summary>
		public readonly static DicomUID ProcedureModifiersForBreast6058 = new DicomUID("1.2.840.10008.6.1.387", "Procedure Modifiers for Breast (6058)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Implant Types (6059)</summary>
		public readonly static DicomUID BreastImplantTypes6059 = new DicomUID("1.2.840.10008.6.1.388", "Breast Implant Types (6059)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Biopsy Techniques (6060)</summary>
		public readonly static DicomUID BreastBiopsyTechniques6060 = new DicomUID("1.2.840.10008.6.1.389", "Breast Biopsy Techniques (6060)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Imaging Procedure Modifiers (6061)</summary>
		public readonly static DicomUID BreastImagingProcedureModifiers6061 = new DicomUID("1.2.840.10008.6.1.390", "Breast Imaging Procedure Modifiers (6061)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Interventional Procedure Complications (6062)</summary>
		public readonly static DicomUID InterventionalProcedureComplications6062 = new DicomUID("1.2.840.10008.6.1.391", "Interventional Procedure Complications (6062)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Interventional Procedure Results (6063)</summary>
		public readonly static DicomUID InterventionalProcedureResults6063 = new DicomUID("1.2.840.10008.6.1.392", "Interventional Procedure Results (6063)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Findings for Breast (6064)</summary>
		public readonly static DicomUID UltrasoundFindingsForBreast6064 = new DicomUID("1.2.840.10008.6.1.393", "Ultrasound Findings for Breast (6064)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Instrument Approach (6065)</summary>
		public readonly static DicomUID InstrumentApproach6065 = new DicomUID("1.2.840.10008.6.1.394", "Instrument Approach (6065)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Target Confirmation (6066)</summary>
		public readonly static DicomUID TargetConfirmation6066 = new DicomUID("1.2.840.10008.6.1.395", "Target Confirmation (6066)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fluid Color (6067)</summary>
		public readonly static DicomUID FluidColor6067 = new DicomUID("1.2.840.10008.6.1.396", "Fluid Color (6067)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Tumor Stages from AJCC (6068)</summary>
		public readonly static DicomUID TumorStagesFromAJCC6068 = new DicomUID("1.2.840.10008.6.1.397", "Tumor Stages from AJCC (6068)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Nottingham Combined Histologic Grade (6069)</summary>
		public readonly static DicomUID NottinghamCombinedHistologicGrade6069 = new DicomUID("1.2.840.10008.6.1.398", "Nottingham Combined Histologic Grade (6069)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Bloom-Richardson Histologic Grade (6070)</summary>
		public readonly static DicomUID BloomRichardsonHistologicGrade6070 = new DicomUID("1.2.840.10008.6.1.399", "Bloom-Richardson Histologic Grade (6070)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Histologic Grading Method (6071)</summary>
		public readonly static DicomUID HistologicGradingMethod6071 = new DicomUID("1.2.840.10008.6.1.400", "Histologic Grading Method (6071)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Implant Findings (6072)</summary>
		public readonly static DicomUID BreastImplantFindings6072 = new DicomUID("1.2.840.10008.6.1.401", "Breast Implant Findings (6072)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Gynecological Hormones (6080)</summary>
		public readonly static DicomUID GynecologicalHormones6080 = new DicomUID("1.2.840.10008.6.1.402", "Gynecological Hormones (6080)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Cancer Risk Factors (6081)</summary>
		public readonly static DicomUID BreastCancerRiskFactors6081 = new DicomUID("1.2.840.10008.6.1.403", "Breast Cancer Risk Factors (6081)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Gynecological Procedures (6082)</summary>
		public readonly static DicomUID GynecologicalProcedures6082 = new DicomUID("1.2.840.10008.6.1.404", "Gynecological Procedures (6082)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedures for Breast (6083)</summary>
		public readonly static DicomUID ProceduresForBreast6083 = new DicomUID("1.2.840.10008.6.1.405", "Procedures for Breast (6083)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mammoplasty Procedures (6084)</summary>
		public readonly static DicomUID MammoplastyProcedures6084 = new DicomUID("1.2.840.10008.6.1.406", "Mammoplasty Procedures (6084)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Therapies for Breast (6085)</summary>
		public readonly static DicomUID TherapiesForBreast6085 = new DicomUID("1.2.840.10008.6.1.407", "Therapies for Breast (6085)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Menopausal Phase (6086)</summary>
		public readonly static DicomUID MenopausalPhase6086 = new DicomUID("1.2.840.10008.6.1.408", "Menopausal Phase (6086)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: General Risk Factors (6087)</summary>
		public readonly static DicomUID GeneralRiskFactors6087 = new DicomUID("1.2.840.10008.6.1.409", "General Risk Factors (6087)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: OB-GYN Maternal Risk Factors (6088)</summary>
		public readonly static DicomUID OBGYNMaternalRiskFactors6088 = new DicomUID("1.2.840.10008.6.1.410", "OB-GYN Maternal Risk Factors (6088)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Substances (6089)</summary>
		public readonly static DicomUID Substances6089 = new DicomUID("1.2.840.10008.6.1.411", "Substances (6089)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Relative Usage, Exposure Amount (6090)</summary>
		public readonly static DicomUID RelativeUsageExposureAmount6090 = new DicomUID("1.2.840.10008.6.1.412", "Relative Usage, Exposure Amount (6090)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Relative Frequency of Event Values (6091)</summary>
		public readonly static DicomUID RelativeFrequencyOfEventValues6091 = new DicomUID("1.2.840.10008.6.1.413", "Relative Frequency of Event Values (6091)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Quantitative Concepts for Usage, Exposure (6092)</summary>
		public readonly static DicomUID QuantitativeConceptsForUsageExposure6092 = new DicomUID("1.2.840.10008.6.1.414", "Quantitative Concepts for Usage, Exposure (6092)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Qualitative Concepts for Usage, Exposure Amount (6093)</summary>
		public readonly static DicomUID QualitativeConceptsForUsageExposureAmount6093 = new DicomUID("1.2.840.10008.6.1.415", "Qualitative Concepts for Usage, Exposure Amount (6093)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: QuaLItative Concepts for Usage, Exposure Frequency (6094)</summary>
		public readonly static DicomUID QuaLItativeConceptsForUsageExposureFrequency6094 = new DicomUID("1.2.840.10008.6.1.416", "QuaLItative Concepts for Usage, Exposure Frequency (6094)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Numeric Properties of Procedures (6095)</summary>
		public readonly static DicomUID NumericPropertiesOfProcedures6095 = new DicomUID("1.2.840.10008.6.1.417", "Numeric Properties of Procedures (6095)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pregnancy Status (6096)</summary>
		public readonly static DicomUID PregnancyStatus6096 = new DicomUID("1.2.840.10008.6.1.418", "Pregnancy Status (6096)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Side of Family (6097)</summary>
		public readonly static DicomUID SideOfFamily6097 = new DicomUID("1.2.840.10008.6.1.419", "Side of Family (6097)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Component Categories (6100)</summary>
		public readonly static DicomUID ChestComponentCategories6100 = new DicomUID("1.2.840.10008.6.1.420", "Chest Component Categories (6100)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Finding or Feature (6101)</summary>
		public readonly static DicomUID ChestFindingOrFeature6101 = new DicomUID("1.2.840.10008.6.1.421", "Chest Finding or Feature (6101)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Finding or Feature Modifier (6102)</summary>
		public readonly static DicomUID ChestFindingOrFeatureModifier6102 = new DicomUID("1.2.840.10008.6.1.422", "Chest Finding or Feature Modifier (6102)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abnormal Lines Finding or Feature (6103)</summary>
		public readonly static DicomUID AbnormalLinesFindingOrFeature6103 = new DicomUID("1.2.840.10008.6.1.423", "Abnormal Lines Finding or Feature (6103)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abnormal Opacity Finding or Feature (6104)</summary>
		public readonly static DicomUID AbnormalOpacityFindingOrFeature6104 = new DicomUID("1.2.840.10008.6.1.424", "Abnormal Opacity Finding or Feature (6104)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abnormal Lucency Finding or Feature (6105)</summary>
		public readonly static DicomUID AbnormalLucencyFindingOrFeature6105 = new DicomUID("1.2.840.10008.6.1.425", "Abnormal Lucency Finding or Feature (6105)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abnormal Texture Finding or Feature (6106)</summary>
		public readonly static DicomUID AbnormalTextureFindingOrFeature6106 = new DicomUID("1.2.840.10008.6.1.426", "Abnormal Texture Finding or Feature (6106)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Width Descriptor (6107)</summary>
		public readonly static DicomUID WidthDescriptor6107 = new DicomUID("1.2.840.10008.6.1.427", "Width Descriptor (6107)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Anatomic Structure Abnormal Distribution (6108)</summary>
		public readonly static DicomUID ChestAnatomicStructureAbnormalDistribution6108 = new DicomUID("1.2.840.10008.6.1.428", "Chest Anatomic Structure Abnormal Distribution (6108)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Radiographic Anatomy Finding or Feature (6109)</summary>
		public readonly static DicomUID RadiographicAnatomyFindingOrFeature6109 = new DicomUID("1.2.840.10008.6.1.429", "Radiographic Anatomy Finding or Feature (6109)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lung Anatomy Finding or Feature (6110)</summary>
		public readonly static DicomUID LungAnatomyFindingOrFeature6110 = new DicomUID("1.2.840.10008.6.1.430", "Lung Anatomy Finding or Feature (6110)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Bronchovascular Anatomy Finding or Feature (6111)</summary>
		public readonly static DicomUID BronchovascularAnatomyFindingOrFeature6111 = new DicomUID("1.2.840.10008.6.1.431", "Bronchovascular Anatomy Finding or Feature (6111)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pleura Anatomy Finding or Feature (6112)</summary>
		public readonly static DicomUID PleuraAnatomyFindingOrFeature6112 = new DicomUID("1.2.840.10008.6.1.432", "Pleura Anatomy Finding or Feature (6112)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mediastinum Anatomy Finding or Feature (6113)</summary>
		public readonly static DicomUID MediastinumAnatomyFindingOrFeature6113 = new DicomUID("1.2.840.10008.6.1.433", "Mediastinum Anatomy Finding or Feature (6113)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Osseous Anatomy Finding or Feature (6114)</summary>
		public readonly static DicomUID OsseousAnatomyFindingOrFeature6114 = new DicomUID("1.2.840.10008.6.1.434", "Osseous Anatomy Finding or Feature (6114)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Osseous Anatomy Modifiers (6115)</summary>
		public readonly static DicomUID OsseousAnatomyModifiers6115 = new DicomUID("1.2.840.10008.6.1.435", "Osseous Anatomy Modifiers (6115)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Muscular Anatomy (6116)</summary>
		public readonly static DicomUID MuscularAnatomy6116 = new DicomUID("1.2.840.10008.6.1.436", "Muscular Anatomy (6116)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Anatomy (6117)</summary>
		public readonly static DicomUID VascularAnatomy6117 = new DicomUID("1.2.840.10008.6.1.437", "Vascular Anatomy (6117)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Size Descriptor (6118)</summary>
		public readonly static DicomUID SizeDescriptor6118 = new DicomUID("1.2.840.10008.6.1.438", "Size Descriptor (6118)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Border Shape (6119)</summary>
		public readonly static DicomUID ChestBorderShape6119 = new DicomUID("1.2.840.10008.6.1.439", "Chest Border Shape (6119)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Border Definition (6120)</summary>
		public readonly static DicomUID ChestBorderDefinition6120 = new DicomUID("1.2.840.10008.6.1.440", "Chest Border Definition (6120)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Orientation Descriptor (6121)</summary>
		public readonly static DicomUID ChestOrientationDescriptor6121 = new DicomUID("1.2.840.10008.6.1.441", "Chest Orientation Descriptor (6121)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Content Descriptor (6122)</summary>
		public readonly static DicomUID ChestContentDescriptor6122 = new DicomUID("1.2.840.10008.6.1.442", "Chest Content Descriptor (6122)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Opacity Descriptor (6123)</summary>
		public readonly static DicomUID ChestOpacityDescriptor6123 = new DicomUID("1.2.840.10008.6.1.443", "Chest Opacity Descriptor (6123)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Location in Chest (6124)</summary>
		public readonly static DicomUID LocationInChest6124 = new DicomUID("1.2.840.10008.6.1.444", "Location in Chest (6124)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: General Chest Location (6125)</summary>
		public readonly static DicomUID GeneralChestLocation6125 = new DicomUID("1.2.840.10008.6.1.445", "General Chest Location (6125)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Location in Lung (6126)</summary>
		public readonly static DicomUID LocationInLung6126 = new DicomUID("1.2.840.10008.6.1.446", "Location in Lung (6126)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Segment Location in Lung (6127)</summary>
		public readonly static DicomUID SegmentLocationInLung6127 = new DicomUID("1.2.840.10008.6.1.447", "Segment Location in Lung (6127)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Distribution Descriptor (6128)</summary>
		public readonly static DicomUID ChestDistributionDescriptor6128 = new DicomUID("1.2.840.10008.6.1.448", "Chest Distribution Descriptor (6128)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Site Involvement (6129)</summary>
		public readonly static DicomUID ChestSiteInvolvement6129 = new DicomUID("1.2.840.10008.6.1.449", "Chest Site Involvement (6129)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Severity Descriptor (6130)</summary>
		public readonly static DicomUID SeverityDescriptor6130 = new DicomUID("1.2.840.10008.6.1.450", "Severity Descriptor (6130)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Texture Descriptor (6131)</summary>
		public readonly static DicomUID ChestTextureDescriptor6131 = new DicomUID("1.2.840.10008.6.1.451", "Chest Texture Descriptor (6131)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Calcification Descriptor (6132)</summary>
		public readonly static DicomUID ChestCalcificationDescriptor6132 = new DicomUID("1.2.840.10008.6.1.452", "Chest Calcification Descriptor (6132)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Quantitative Temporal Difference Type (6133)</summary>
		public readonly static DicomUID ChestQuantitativeTemporalDifferenceType6133 = new DicomUID("1.2.840.10008.6.1.453", "Chest Quantitative Temporal Difference Type (6133)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Qualitative Temporal Difference Type (6134)</summary>
		public readonly static DicomUID QualitativeTemporalDifferenceType6134 = new DicomUID("1.2.840.10008.6.1.454", "Qualitative Temporal Difference Type (6134)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Image Quality Finding (6135)</summary>
		public readonly static DicomUID ImageQualityFinding6135 = new DicomUID("1.2.840.10008.6.1.455", "Image Quality Finding (6135)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Types of Quality Control Standard (6136)</summary>
		public readonly static DicomUID ChestTypesOfQualityControlStandard6136 = new DicomUID("1.2.840.10008.6.1.456", "Chest Types of Quality Control Standard (6136)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Types of CAD Analysis (6137)</summary>
		public readonly static DicomUID TypesOfCADAnalysis6137 = new DicomUID("1.2.840.10008.6.1.457", "Types of CAD Analysis (6137)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Non-Lesion Object Type (6138)</summary>
		public readonly static DicomUID ChestNonLesionObjectType6138 = new DicomUID("1.2.840.10008.6.1.458", "Chest Non-Lesion Object Type (6138)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Non-Lesion Modifiers (6139)</summary>
		public readonly static DicomUID NonLesionModifiers6139 = new DicomUID("1.2.840.10008.6.1.459", "Non-Lesion Modifiers (6139)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calculation Methods (6140)</summary>
		public readonly static DicomUID CalculationMethods6140 = new DicomUID("1.2.840.10008.6.1.460", "Calculation Methods (6140)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Attenuation Coefficient Measurements (6141)</summary>
		public readonly static DicomUID AttenuationCoefficientMeasurements6141 = new DicomUID("1.2.840.10008.6.1.461", "Attenuation Coefficient Measurements (6141)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calculated Value (6142)</summary>
		public readonly static DicomUID CalculatedValue6142 = new DicomUID("1.2.840.10008.6.1.462", "Calculated Value (6142)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Response Criteria (6143)</summary>
		public readonly static DicomUID ResponseCriteria6143 = new DicomUID("1.2.840.10008.6.1.463", "Response Criteria (6143)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: RECIST Response Criteria (6144)</summary>
		public readonly static DicomUID RECISTResponseCriteria6144 = new DicomUID("1.2.840.10008.6.1.464", "RECIST Response Criteria (6144)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Baseline Category (6145)</summary>
		public readonly static DicomUID BaselineCategory6145 = new DicomUID("1.2.840.10008.6.1.465", "Baseline Category (6145)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Background echotexture (6151)</summary>
		public readonly static DicomUID BackgroundEchotexture6151 = new DicomUID("1.2.840.10008.6.1.466", "Background echotexture (6151)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Orientation (6152)</summary>
		public readonly static DicomUID Orientation6152 = new DicomUID("1.2.840.10008.6.1.467", "Orientation (6152)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lesion boundary (6153)</summary>
		public readonly static DicomUID LesionBoundary6153 = new DicomUID("1.2.840.10008.6.1.468", "Lesion boundary (6153)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echo pattern (6154)</summary>
		public readonly static DicomUID EchoPattern6154 = new DicomUID("1.2.840.10008.6.1.469", "Echo pattern (6154)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Posterior acoustic features (6155)</summary>
		public readonly static DicomUID PosteriorAcousticFeatures6155 = new DicomUID("1.2.840.10008.6.1.470", "Posterior acoustic features (6155)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascularity (6157)</summary>
		public readonly static DicomUID Vascularity6157 = new DicomUID("1.2.840.10008.6.1.471", "Vascularity (6157)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Correlation to Other Findings (6158)</summary>
		public readonly static DicomUID CorrelationToOtherFindings6158 = new DicomUID("1.2.840.10008.6.1.472", "Correlation to Other Findings (6158)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Malignancy Type (6159)</summary>
		public readonly static DicomUID MalignancyType6159 = new DicomUID("1.2.840.10008.6.1.473", "Malignancy Type (6159)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Primary Tumor Assessment from AJCC (6160)</summary>
		public readonly static DicomUID BreastPrimaryTumorAssessmentFromAJCC6160 = new DicomUID("1.2.840.10008.6.1.474", "Breast Primary Tumor Assessment from AJCC (6160)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Clinical Regional Lymph Node Assessment for Breast (6161)</summary>
		public readonly static DicomUID ClinicalRegionalLymphNodeAssessmentForBreast6161 = new DicomUID("1.2.840.10008.6.1.475", "Clinical Regional Lymph Node Assessment for Breast (6161)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Assessment of Metastasis for Breast (6162)</summary>
		public readonly static DicomUID AssessmentOfMetastasisForBreast6162 = new DicomUID("1.2.840.10008.6.1.476", "Assessment of Metastasis for Breast (6162)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Menstrual Cycle Phase (6163)</summary>
		public readonly static DicomUID MenstrualCyclePhase6163 = new DicomUID("1.2.840.10008.6.1.477", "Menstrual Cycle Phase (6163)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Time Intervals (6164)</summary>
		public readonly static DicomUID TimeIntervals6164 = new DicomUID("1.2.840.10008.6.1.478", "Time Intervals (6164)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breast Linear Measurements (6165)</summary>
		public readonly static DicomUID BreastLinearMeasurements6165 = new DicomUID("1.2.840.10008.6.1.479", "Breast Linear Measurements (6165)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: CAD Geometry Secondary Graphical Representation (6166)</summary>
		public readonly static DicomUID CADGeometrySecondaryGraphicalRepresentation6166 = new DicomUID("1.2.840.10008.6.1.480", "CAD Geometry Secondary Graphical Representation (6166)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Diagnostic Imaging Report Document Titles (7000)</summary>
		public readonly static DicomUID DiagnosticImagingReportDocumentTitles7000 = new DicomUID("1.2.840.10008.6.1.481", "Diagnostic Imaging Report Document Titles (7000)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Diagnostic Imaging Report Headings (7001)</summary>
		public readonly static DicomUID DiagnosticImagingReportHeadings7001 = new DicomUID("1.2.840.10008.6.1.482", "Diagnostic Imaging Report Headings (7001)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Diagnostic Imaging Report Elements (7002)</summary>
		public readonly static DicomUID DiagnosticImagingReportElements7002 = new DicomUID("1.2.840.10008.6.1.483", "Diagnostic Imaging Report Elements (7002)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Diagnostic Imaging Report Purposes of Reference (7003)</summary>
		public readonly static DicomUID DiagnosticImagingReportPurposesOfReference7003 = new DicomUID("1.2.840.10008.6.1.484", "Diagnostic Imaging Report Purposes of Reference (7003)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Waveform Purposes of Reference (7004)</summary>
		public readonly static DicomUID WaveformPurposesOfReference7004 = new DicomUID("1.2.840.10008.6.1.485", "Waveform Purposes of Reference (7004)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Contributing Equipment Purposes of Reference (7005)</summary>
		public readonly static DicomUID ContributingEquipmentPurposesOfReference7005 = new DicomUID("1.2.840.10008.6.1.486", "Contributing Equipment Purposes of Reference (7005)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: SR Document Purposes of Reference (7006)</summary>
		public readonly static DicomUID SRDocumentPurposesOfReference7006 = new DicomUID("1.2.840.10008.6.1.487", "SR Document Purposes of Reference (7006)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Signature Purpose (7007)</summary>
		public readonly static DicomUID SignaturePurpose7007 = new DicomUID("1.2.840.10008.6.1.488", "Signature Purpose (7007)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Media Import (7008)</summary>
		public readonly static DicomUID MediaImport7008 = new DicomUID("1.2.840.10008.6.1.489", "Media Import (7008)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Key Object Selection Document Title (7010)</summary>
		public readonly static DicomUID KeyObjectSelectionDocumentTitle7010 = new DicomUID("1.2.840.10008.6.1.490", "Key Object Selection Document Title (7010)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Rejected for Quality Reasons (7011)</summary>
		public readonly static DicomUID RejectedForQualityReasons7011 = new DicomUID("1.2.840.10008.6.1.491", "Rejected for Quality Reasons (7011)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Best In Set (7012)</summary>
		public readonly static DicomUID BestInSet7012 = new DicomUID("1.2.840.10008.6.1.492", "Best In Set (7012)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Document Titles (7020)</summary>
		public readonly static DicomUID DocumentTitles7020 = new DicomUID("1.2.840.10008.6.1.493", "Document Titles (7020)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: RCS Registration Method Type (7100)</summary>
		public readonly static DicomUID RCSRegistrationMethodType7100 = new DicomUID("1.2.840.10008.6.1.494", "RCS Registration Method Type (7100)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Brain Atlas Fiducials (7101)</summary>
		public readonly static DicomUID BrainAtlasFiducials7101 = new DicomUID("1.2.840.10008.6.1.495", "Brain Atlas Fiducials (7101)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Segmentation Property Categories (7150)</summary>
		public readonly static DicomUID SegmentationPropertyCategories7150 = new DicomUID("1.2.840.10008.6.1.496", "Segmentation Property Categories (7150)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Segmentation Property Types (7151)</summary>
		public readonly static DicomUID SegmentationPropertyTypes7151 = new DicomUID("1.2.840.10008.6.1.497", "Segmentation Property Types (7151)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Structure Segmentation Types (7152)</summary>
		public readonly static DicomUID CardiacStructureSegmentationTypes7152 = new DicomUID("1.2.840.10008.6.1.498", "Cardiac Structure Segmentation Types (7152)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Brain Tissue Segmentation Types (7153)</summary>
		public readonly static DicomUID BrainTissueSegmentationTypes7153 = new DicomUID("1.2.840.10008.6.1.499", "Brain Tissue Segmentation Types (7153)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abdominal Organ Segmentation Types (7154)</summary>
		public readonly static DicomUID AbdominalOrganSegmentationTypes7154 = new DicomUID("1.2.840.10008.6.1.500", "Abdominal Organ Segmentation Types (7154)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Thoracic Tissue Segmentation Types (7155)</summary>
		public readonly static DicomUID ThoracicTissueSegmentationTypes7155 = new DicomUID("1.2.840.10008.6.1.501", "Thoracic Tissue Segmentation Types (7155)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Tissue Segmentation Types (7156)</summary>
		public readonly static DicomUID VascularTissueSegmentationTypes7156 = new DicomUID("1.2.840.10008.6.1.502", "Vascular Tissue Segmentation Types (7156)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Device Segmentation Types (7157)</summary>
		public readonly static DicomUID DeviceSegmentationTypes7157 = new DicomUID("1.2.840.10008.6.1.503", "Device Segmentation Types (7157)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Artifact Segmentation Types (7158)</summary>
		public readonly static DicomUID ArtifactSegmentationTypes7158 = new DicomUID("1.2.840.10008.6.1.504", "Artifact Segmentation Types (7158)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lesion Segmentation Types (7159)</summary>
		public readonly static DicomUID LesionSegmentationTypes7159 = new DicomUID("1.2.840.10008.6.1.505", "Lesion Segmentation Types (7159)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pelvic Organ Segmentation Types (7160)</summary>
		public readonly static DicomUID PelvicOrganSegmentationTypes7160 = new DicomUID("1.2.840.10008.6.1.506", "Pelvic Organ Segmentation Types (7160)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Physiology Segmentation Types (7161)</summary>
		public readonly static DicomUID PhysiologySegmentationTypes7161 = new DicomUID("1.2.840.10008.6.1.507", "Physiology Segmentation Types (7161)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Referenced Image Purposes of Reference (7201)</summary>
		public readonly static DicomUID ReferencedImagePurposesOfReference7201 = new DicomUID("1.2.840.10008.6.1.508", "Referenced Image Purposes of Reference (7201)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Source Image Purposes of Reference (7202)</summary>
		public readonly static DicomUID SourceImagePurposesOfReference7202 = new DicomUID("1.2.840.10008.6.1.509", "Source Image Purposes of Reference (7202)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Image Derivation (7203)</summary>
		public readonly static DicomUID ImageDerivation7203 = new DicomUID("1.2.840.10008.6.1.510", "Image Derivation (7203)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Purpose Of Reference to Alternate Representation (7205)</summary>
		public readonly static DicomUID PurposeOfReferenceToAlternateRepresentation7205 = new DicomUID("1.2.840.10008.6.1.511", "Purpose Of Reference to Alternate Representation (7205)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Related Series Purposes Of Reference (7210)</summary>
		public readonly static DicomUID RelatedSeriesPurposesOfReference7210 = new DicomUID("1.2.840.10008.6.1.512", "Related Series Purposes Of Reference (7210)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Multi-frame Subset Type (7250)</summary>
		public readonly static DicomUID MultiFrameSubsetType7250 = new DicomUID("1.2.840.10008.6.1.513", "Multi-frame Subset Type (7250)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Person Roles (7450)</summary>
		public readonly static DicomUID PersonRoles7450 = new DicomUID("1.2.840.10008.6.1.514", "Person Roles (7450)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Family Member (7451)</summary>
		public readonly static DicomUID FamilyMember7451 = new DicomUID("1.2.840.10008.6.1.515", "Family Member (7451)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Organizational Roles (7452)</summary>
		public readonly static DicomUID OrganizationalRoles7452 = new DicomUID("1.2.840.10008.6.1.516", "Organizational Roles (7452)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Performing Roles (7453)</summary>
		public readonly static DicomUID PerformingRoles7453 = new DicomUID("1.2.840.10008.6.1.517", "Performing Roles (7453)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Species (7454)</summary>
		public readonly static DicomUID Species7454 = new DicomUID("1.2.840.10008.6.1.518", "Species (7454)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Sex (7455)</summary>
		public readonly static DicomUID Sex7455 = new DicomUID("1.2.840.10008.6.1.519", "Sex (7455)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units of Measure for Age (7456)</summary>
		public readonly static DicomUID UnitsOfMeasureForAge7456 = new DicomUID("1.2.840.10008.6.1.520", "Units of Measure for Age (7456)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units of Linear Measurement (7460)</summary>
		public readonly static DicomUID UnitsOfLinearMeasurement7460 = new DicomUID("1.2.840.10008.6.1.521", "Units of Linear Measurement (7460)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units of Area Measurement (7461)</summary>
		public readonly static DicomUID UnitsOfAreaMeasurement7461 = new DicomUID("1.2.840.10008.6.1.522", "Units of Area Measurement (7461)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units of Volume Measurement (7462)</summary>
		public readonly static DicomUID UnitsOfVolumeMeasurement7462 = new DicomUID("1.2.840.10008.6.1.523", "Units of Volume Measurement (7462)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Linear Measurements (7470)</summary>
		public readonly static DicomUID LinearMeasurements7470 = new DicomUID("1.2.840.10008.6.1.524", "Linear Measurements (7470)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Area Measurements (7471)</summary>
		public readonly static DicomUID AreaMeasurements7471 = new DicomUID("1.2.840.10008.6.1.525", "Area Measurements (7471)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Volume Measurements (7472)</summary>
		public readonly static DicomUID VolumeMeasurements7472 = new DicomUID("1.2.840.10008.6.1.526", "Volume Measurements (7472)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: General Area Calculation Methods (7473)</summary>
		public readonly static DicomUID GeneralAreaCalculationMethods7473 = new DicomUID("1.2.840.10008.6.1.527", "General Area Calculation Methods (7473)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: General Volume Calculation Methods (7474)</summary>
		public readonly static DicomUID GeneralVolumeCalculationMethods7474 = new DicomUID("1.2.840.10008.6.1.528", "General Volume Calculation Methods (7474)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breed (7480)</summary>
		public readonly static DicomUID Breed7480 = new DicomUID("1.2.840.10008.6.1.529", "Breed (7480)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Breed Registry (7481)</summary>
		public readonly static DicomUID BreedRegistry7481 = new DicomUID("1.2.840.10008.6.1.530", "Breed Registry (7481)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: General Purpose Workitem Definition (9231)</summary>
		public readonly static DicomUID GeneralPurposeWorkitemDefinition9231 = new DicomUID("1.2.840.10008.6.1.531", "General Purpose Workitem Definition (9231)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Non-DICOM Output Types (9232)</summary>
		public readonly static DicomUID NonDICOMOutputTypes9232 = new DicomUID("1.2.840.10008.6.1.532", "Non-DICOM Output Types (9232)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Procedure Discontinuation Reasons (9300)</summary>
		public readonly static DicomUID ProcedureDiscontinuationReasons9300 = new DicomUID("1.2.840.10008.6.1.533", "Procedure Discontinuation Reasons (9300)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Scope of Accumulation (10000)</summary>
		public readonly static DicomUID ScopeOfAccumulation10000 = new DicomUID("1.2.840.10008.6.1.534", "Scope of Accumulation (10000)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: UID Types (10001)</summary>
		public readonly static DicomUID UIDTypes10001 = new DicomUID("1.2.840.10008.6.1.535", "UID Types (10001)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Irradiation Event Types (10002)</summary>
		public readonly static DicomUID IrradiationEventTypes10002 = new DicomUID("1.2.840.10008.6.1.536", "Irradiation Event Types (10002)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Equipment Plane Identification (10003)</summary>
		public readonly static DicomUID EquipmentPlaneIdentification10003 = new DicomUID("1.2.840.10008.6.1.537", "Equipment Plane Identification (10003)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fluoro Modes (10004)</summary>
		public readonly static DicomUID FluoroModes10004 = new DicomUID("1.2.840.10008.6.1.538", "Fluoro Modes (10004)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: X-Ray Filter Materials (10006)</summary>
		public readonly static DicomUID XRayFilterMaterials10006 = new DicomUID("1.2.840.10008.6.1.539", "X-Ray Filter Materials (10006)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: X-Ray Filter Types (10007)</summary>
		public readonly static DicomUID XRayFilterTypes10007 = new DicomUID("1.2.840.10008.6.1.540", "X-Ray Filter Types (10007)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Dose Related Distance Measurements (10008)</summary>
		public readonly static DicomUID DoseRelatedDistanceMeasurements10008 = new DicomUID("1.2.840.10008.6.1.541", "Dose Related Distance Measurements (10008)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Measured/Calculated (10009)</summary>
		public readonly static DicomUID MeasuredCalculated10009 = new DicomUID("1.2.840.10008.6.1.542", "Measured/Calculated (10009)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Dose Measurement Devices (10010)</summary>
		public readonly static DicomUID DoseMeasurementDevices10010 = new DicomUID("1.2.840.10008.6.1.543", "Dose Measurement Devices (10010)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Effective Dose Evaluation Method (10011)</summary>
		public readonly static DicomUID EffectiveDoseEvaluationMethod10011 = new DicomUID("1.2.840.10008.6.1.544", "Effective Dose Evaluation Method (10011)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: CT Acquisition Type (10013)</summary>
		public readonly static DicomUID CTAcquisitionType10013 = new DicomUID("1.2.840.10008.6.1.545", "CT Acquisition Type (10013)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Contrast Imaging Technique (10014)</summary>
		public readonly static DicomUID ContrastImagingTechnique10014 = new DicomUID("1.2.840.10008.6.1.546", "Contrast Imaging Technique (10014)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: CT Dose Reference Authorities (10015)</summary>
		public readonly static DicomUID CTDoseReferenceAuthorities10015 = new DicomUID("1.2.840.10008.6.1.547", "CT Dose Reference Authorities (10015)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Anode Target Material (10016)</summary>
		public readonly static DicomUID AnodeTargetMaterial10016 = new DicomUID("1.2.840.10008.6.1.548", "Anode Target Material (10016)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: X-Ray Grid (10017)</summary>
		public readonly static DicomUID XRayGrid10017 = new DicomUID("1.2.840.10008.6.1.549", "X-Ray Grid (10017)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Protocol Types (12001)</summary>
		public readonly static DicomUID UltrasoundProtocolTypes12001 = new DicomUID("1.2.840.10008.6.1.550", "Ultrasound Protocol Types (12001)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Protocol Stage Types (12002)</summary>
		public readonly static DicomUID UltrasoundProtocolStageTypes12002 = new DicomUID("1.2.840.10008.6.1.551", "Ultrasound Protocol Stage Types (12002)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: OB-GYN Dates (12003)</summary>
		public readonly static DicomUID OBGYNDates12003 = new DicomUID("1.2.840.10008.6.1.552", "OB-GYN Dates (12003)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fetal Biometry Ratios (12004)</summary>
		public readonly static DicomUID FetalBiometryRatios12004 = new DicomUID("1.2.840.10008.6.1.553", "Fetal Biometry Ratios (12004)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fetal Biometry Measurements (12005)</summary>
		public readonly static DicomUID FetalBiometryMeasurements12005 = new DicomUID("1.2.840.10008.6.1.554", "Fetal Biometry Measurements (12005)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fetal Long Bones Biometry Measurements (12006)</summary>
		public readonly static DicomUID FetalLongBonesBiometryMeasurements12006 = new DicomUID("1.2.840.10008.6.1.555", "Fetal Long Bones Biometry Measurements (12006)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fetal Cranium (12007)</summary>
		public readonly static DicomUID FetalCranium12007 = new DicomUID("1.2.840.10008.6.1.556", "Fetal Cranium (12007)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: OB-GYN Amniotic Sac (12008)</summary>
		public readonly static DicomUID OBGYNAmnioticSac12008 = new DicomUID("1.2.840.10008.6.1.557", "OB-GYN Amniotic Sac (12008)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Early Gestation Biometry Measurements (12009)</summary>
		public readonly static DicomUID EarlyGestationBiometryMeasurements12009 = new DicomUID("1.2.840.10008.6.1.558", "Early Gestation Biometry Measurements (12009)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Pelvis and Uterus (12011)</summary>
		public readonly static DicomUID UltrasoundPelvisAndUterus12011 = new DicomUID("1.2.840.10008.6.1.559", "Ultrasound Pelvis and Uterus (12011)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: OB Equations and Tables (12012)</summary>
		public readonly static DicomUID OBEquationsAndTables12012 = new DicomUID("1.2.840.10008.6.1.560", "OB Equations and Tables (12012)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Gestational Age Equations and Tables (12013)</summary>
		public readonly static DicomUID GestationalAgeEquationsAndTables12013 = new DicomUID("1.2.840.10008.6.1.561", "Gestational Age Equations and Tables (12013)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: OB Fetal Body Weight Equations and Tables (12014)</summary>
		public readonly static DicomUID OBFetalBodyWeightEquationsAndTables12014 = new DicomUID("1.2.840.10008.6.1.562", "OB Fetal Body Weight Equations and Tables (12014)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fetal Growth Equations and Tables (12015)</summary>
		public readonly static DicomUID FetalGrowthEquationsAndTables12015 = new DicomUID("1.2.840.10008.6.1.563", "Fetal Growth Equations and Tables (12015)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Estimated Fetal Weight Percentile Equations and Tables (12016)</summary>
		public readonly static DicomUID EstimatedFetalWeightPercentileEquationsAndTables12016 = new DicomUID("1.2.840.10008.6.1.564", "Estimated Fetal Weight Percentile Equations and Tables (12016)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Growth Distribution Rank (12017)</summary>
		public readonly static DicomUID GrowthDistributionRank12017 = new DicomUID("1.2.840.10008.6.1.565", "Growth Distribution Rank (12017)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: OB-GYN Summary (12018)</summary>
		public readonly static DicomUID OBGYNSummary12018 = new DicomUID("1.2.840.10008.6.1.566", "OB-GYN Summary (12018)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: OB-GYN Fetus Summary (12019)</summary>
		public readonly static DicomUID OBGYNFetusSummary12019 = new DicomUID("1.2.840.10008.6.1.567", "OB-GYN Fetus Summary (12019)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Summary (12101)</summary>
		public readonly static DicomUID VascularSummary12101 = new DicomUID("1.2.840.10008.6.1.568", "Vascular Summary (12101)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Temporal Periods Relating to Procedure or Therapy (12102)</summary>
		public readonly static DicomUID TemporalPeriodsRelatingToProcedureOrTherapy12102 = new DicomUID("1.2.840.10008.6.1.569", "Temporal Periods Relating to Procedure or Therapy (12102)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Ultrasound Anatomic Location (12103)</summary>
		public readonly static DicomUID VascularUltrasoundAnatomicLocation12103 = new DicomUID("1.2.840.10008.6.1.570", "Vascular Ultrasound Anatomic Location (12103)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Extracranial Arteries (12104)</summary>
		public readonly static DicomUID ExtracranialArteries12104 = new DicomUID("1.2.840.10008.6.1.571", "Extracranial Arteries (12104)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intracranial Cerebral Vessels (12105)</summary>
		public readonly static DicomUID IntracranialCerebralVessels12105 = new DicomUID("1.2.840.10008.6.1.572", "Intracranial Cerebral Vessels (12105)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intracranial Cerebral Vessels (unilateral) (12106)</summary>
		public readonly static DicomUID IntracranialCerebralVesselsUnilateral12106 = new DicomUID("1.2.840.10008.6.1.573", "Intracranial Cerebral Vessels (unilateral) (12106)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Upper Extremity Arteries (12107)</summary>
		public readonly static DicomUID UpperExtremityArteries12107 = new DicomUID("1.2.840.10008.6.1.574", "Upper Extremity Arteries (12107)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Upper Extremity Veins (12108)</summary>
		public readonly static DicomUID UpperExtremityVeins12108 = new DicomUID("1.2.840.10008.6.1.575", "Upper Extremity Veins (12108)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lower Extremity Arteries (12109)</summary>
		public readonly static DicomUID LowerExtremityArteries12109 = new DicomUID("1.2.840.10008.6.1.576", "Lower Extremity Arteries (12109)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lower Extremity Veins (12110)</summary>
		public readonly static DicomUID LowerExtremityVeins12110 = new DicomUID("1.2.840.10008.6.1.577", "Lower Extremity Veins (12110)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abdominal Arteries (lateral) (12111)</summary>
		public readonly static DicomUID AbdominalArteriesLateral12111 = new DicomUID("1.2.840.10008.6.1.578", "Abdominal Arteries (lateral) (12111)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abdominal Arteries (unilateral) (12112)</summary>
		public readonly static DicomUID AbdominalArteriesUnilateral12112 = new DicomUID("1.2.840.10008.6.1.579", "Abdominal Arteries (unilateral) (12112)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abdominal Veins (lateral) (12113)</summary>
		public readonly static DicomUID AbdominalVeinsLateral12113 = new DicomUID("1.2.840.10008.6.1.580", "Abdominal Veins (lateral) (12113)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abdominal Veins (unilateral) (12114)</summary>
		public readonly static DicomUID AbdominalVeinsUnilateral12114 = new DicomUID("1.2.840.10008.6.1.581", "Abdominal Veins (unilateral) (12114)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Renal Vessels (12115)</summary>
		public readonly static DicomUID RenalVessels12115 = new DicomUID("1.2.840.10008.6.1.582", "Renal Vessels (12115)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vessel Segment Modifiers (12116)</summary>
		public readonly static DicomUID VesselSegmentModifiers12116 = new DicomUID("1.2.840.10008.6.1.583", "Vessel Segment Modifiers (12116)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vessel Branch Modifiers (12117)</summary>
		public readonly static DicomUID VesselBranchModifiers12117 = new DicomUID("1.2.840.10008.6.1.584", "Vessel Branch Modifiers (12117)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Ultrasound Property (12119)</summary>
		public readonly static DicomUID VascularUltrasoundProperty12119 = new DicomUID("1.2.840.10008.6.1.585", "Vascular Ultrasound Property (12119)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Blood Velocity Measurements by Ultrasound (12120)</summary>
		public readonly static DicomUID BloodVelocityMeasurementsByUltrasound12120 = new DicomUID("1.2.840.10008.6.1.586", "Blood Velocity Measurements by Ultrasound (12120)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vascular Indices and Ratios (12121)</summary>
		public readonly static DicomUID VascularIndicesAndRatios12121 = new DicomUID("1.2.840.10008.6.1.587", "Vascular Indices and Ratios (12121)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Other Vascular Properties (12122)</summary>
		public readonly static DicomUID OtherVascularProperties12122 = new DicomUID("1.2.840.10008.6.1.588", "Other Vascular Properties (12122)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Carotid Ratios (12123)</summary>
		public readonly static DicomUID CarotidRatios12123 = new DicomUID("1.2.840.10008.6.1.589", "Carotid Ratios (12123)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Renal Ratios (12124)</summary>
		public readonly static DicomUID RenalRatios12124 = new DicomUID("1.2.840.10008.6.1.590", "Renal Ratios (12124)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pelvic Vasculature Anatomical Location (12140)</summary>
		public readonly static DicomUID PelvicVasculatureAnatomicalLocation12140 = new DicomUID("1.2.840.10008.6.1.591", "Pelvic Vasculature Anatomical Location (12140)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Fetal Vasculature Anatomical Location (12141)</summary>
		public readonly static DicomUID FetalVasculatureAnatomicalLocation12141 = new DicomUID("1.2.840.10008.6.1.592", "Fetal Vasculature Anatomical Location (12141)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Left Ventricle (12200)</summary>
		public readonly static DicomUID EchocardiographyLeftVentricle12200 = new DicomUID("1.2.840.10008.6.1.593", "Echocardiography Left Ventricle (12200)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Left Ventricle Linear (12201)</summary>
		public readonly static DicomUID LeftVentricleLinear12201 = new DicomUID("1.2.840.10008.6.1.594", "Left Ventricle Linear (12201)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Left Ventricle Volume (12202)</summary>
		public readonly static DicomUID LeftVentricleVolume12202 = new DicomUID("1.2.840.10008.6.1.595", "Left Ventricle Volume (12202)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Left Ventricle Other (12203)</summary>
		public readonly static DicomUID LeftVentricleOther12203 = new DicomUID("1.2.840.10008.6.1.596", "Left Ventricle Other (12203)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Right Ventricle (12204)</summary>
		public readonly static DicomUID EchocardiographyRightVentricle12204 = new DicomUID("1.2.840.10008.6.1.597", "Echocardiography Right Ventricle (12204)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Left Atrium (12205)</summary>
		public readonly static DicomUID EchocardiographyLeftAtrium12205 = new DicomUID("1.2.840.10008.6.1.598", "Echocardiography Left Atrium (12205)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Right Atrium (12206)</summary>
		public readonly static DicomUID EchocardiographyRightAtrium12206 = new DicomUID("1.2.840.10008.6.1.599", "Echocardiography Right Atrium (12206)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Mitral Valve (12207)</summary>
		public readonly static DicomUID EchocardiographyMitralValve12207 = new DicomUID("1.2.840.10008.6.1.600", "Echocardiography Mitral Valve (12207)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Tricuspid Valve (12208)</summary>
		public readonly static DicomUID EchocardiographyTricuspidValve12208 = new DicomUID("1.2.840.10008.6.1.601", "Echocardiography Tricuspid Valve (12208)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Pulmonic Valve (12209)</summary>
		public readonly static DicomUID EchocardiographyPulmonicValve12209 = new DicomUID("1.2.840.10008.6.1.602", "Echocardiography Pulmonic Valve (12209)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Pulmonary Artery (12210)</summary>
		public readonly static DicomUID EchocardiographyPulmonaryArtery12210 = new DicomUID("1.2.840.10008.6.1.603", "Echocardiography Pulmonary Artery (12210)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Aortic Valve (12211)</summary>
		public readonly static DicomUID EchocardiographyAorticValve12211 = new DicomUID("1.2.840.10008.6.1.604", "Echocardiography Aortic Valve (12211)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Aorta (12212)</summary>
		public readonly static DicomUID EchocardiographyAorta12212 = new DicomUID("1.2.840.10008.6.1.605", "Echocardiography Aorta (12212)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Pulmonary Veins (12214)</summary>
		public readonly static DicomUID EchocardiographyPulmonaryVeins12214 = new DicomUID("1.2.840.10008.6.1.606", "Echocardiography Pulmonary Veins (12214)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Vena Cavae (12215)</summary>
		public readonly static DicomUID EchocardiographyVenaCavae12215 = new DicomUID("1.2.840.10008.6.1.607", "Echocardiography Vena Cavae (12215)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Hepatic Veins (12216)</summary>
		public readonly static DicomUID EchocardiographyHepaticVeins12216 = new DicomUID("1.2.840.10008.6.1.608", "Echocardiography Hepatic Veins (12216)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Cardiac Shunt (12217)</summary>
		public readonly static DicomUID EchocardiographyCardiacShunt12217 = new DicomUID("1.2.840.10008.6.1.609", "Echocardiography Cardiac Shunt (12217)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Congenital (12218)</summary>
		public readonly static DicomUID EchocardiographyCongenital12218 = new DicomUID("1.2.840.10008.6.1.610", "Echocardiography Congenital (12218)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pulmonary Vein Modifiers (12219)</summary>
		public readonly static DicomUID PulmonaryVeinModifiers12219 = new DicomUID("1.2.840.10008.6.1.611", "Pulmonary Vein Modifiers (12219)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Common Measurements (12220)</summary>
		public readonly static DicomUID EchocardiographyCommonMeasurements12220 = new DicomUID("1.2.840.10008.6.1.612", "Echocardiography Common Measurements (12220)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Flow Direction (12221)</summary>
		public readonly static DicomUID FlowDirection12221 = new DicomUID("1.2.840.10008.6.1.613", "Flow Direction (12221)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Orifice Flow Properties (12222)</summary>
		public readonly static DicomUID OrificeFlowProperties12222 = new DicomUID("1.2.840.10008.6.1.614", "Orifice Flow Properties (12222)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Stroke Volume Origin (12223)</summary>
		public readonly static DicomUID EchocardiographyStrokeVolumeOrigin12223 = new DicomUID("1.2.840.10008.6.1.615", "Echocardiography Stroke Volume Origin (12223)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Image Modes (12224)</summary>
		public readonly static DicomUID UltrasoundImageModes12224 = new DicomUID("1.2.840.10008.6.1.616", "Ultrasound Image Modes (12224)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Image View (12226)</summary>
		public readonly static DicomUID EchocardiographyImageView12226 = new DicomUID("1.2.840.10008.6.1.617", "Echocardiography Image View (12226)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Measurement Method (12227)</summary>
		public readonly static DicomUID EchocardiographyMeasurementMethod12227 = new DicomUID("1.2.840.10008.6.1.618", "Echocardiography Measurement Method (12227)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Volume Methods (12228)</summary>
		public readonly static DicomUID EchocardiographyVolumeMethods12228 = new DicomUID("1.2.840.10008.6.1.619", "Echocardiography Volume Methods (12228)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Area Methods (12229)</summary>
		public readonly static DicomUID EchocardiographyAreaMethods12229 = new DicomUID("1.2.840.10008.6.1.620", "Echocardiography Area Methods (12229)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Gradient Methods (12230)</summary>
		public readonly static DicomUID GradientMethods12230 = new DicomUID("1.2.840.10008.6.1.621", "Gradient Methods (12230)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Volume Flow Methods (12231)</summary>
		public readonly static DicomUID VolumeFlowMethods12231 = new DicomUID("1.2.840.10008.6.1.622", "Volume Flow Methods (12231)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Myocardium Mass Methods (12232)</summary>
		public readonly static DicomUID MyocardiumMassMethods12232 = new DicomUID("1.2.840.10008.6.1.623", "Myocardium Mass Methods (12232)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Phase (12233)</summary>
		public readonly static DicomUID CardiacPhase12233 = new DicomUID("1.2.840.10008.6.1.624", "Cardiac Phase (12233)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Respiration State (12234)</summary>
		public readonly static DicomUID RespirationState12234 = new DicomUID("1.2.840.10008.6.1.625", "Respiration State (12234)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mitral Valve Anatomic Sites (12235)</summary>
		public readonly static DicomUID MitralValveAnatomicSites12235 = new DicomUID("1.2.840.10008.6.1.626", "Mitral Valve Anatomic Sites (12235)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echo Anatomic Sites (12236)</summary>
		public readonly static DicomUID EchoAnatomicSites12236 = new DicomUID("1.2.840.10008.6.1.627", "Echo Anatomic Sites (12236)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Echocardiography Anatomic Site Modifiers (12237)</summary>
		public readonly static DicomUID EchocardiographyAnatomicSiteModifiers12237 = new DicomUID("1.2.840.10008.6.1.628", "Echocardiography Anatomic Site Modifiers (12237)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Wall Motion Scoring Schemes (12238)</summary>
		public readonly static DicomUID WallMotionScoringSchemes12238 = new DicomUID("1.2.840.10008.6.1.629", "Wall Motion Scoring Schemes (12238)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Output Properties (12239)</summary>
		public readonly static DicomUID CardiacOutputProperties12239 = new DicomUID("1.2.840.10008.6.1.630", "Cardiac Output Properties (12239)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Left Ventricle Area (12240)</summary>
		public readonly static DicomUID LeftVentricleArea12240 = new DicomUID("1.2.840.10008.6.1.631", "Left Ventricle Area (12240)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Tricuspid Valve Finding Sites (12241)</summary>
		public readonly static DicomUID TricuspidValveFindingSites12241 = new DicomUID("1.2.840.10008.6.1.632", "Tricuspid Valve Finding Sites (12241)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Aortic Valve Finding Sites (12242)</summary>
		public readonly static DicomUID AorticValveFindingSites12242 = new DicomUID("1.2.840.10008.6.1.633", "Aortic Valve Finding Sites (12242)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Left Ventricle Finding Sites (12243)</summary>
		public readonly static DicomUID LeftVentricleFindingSites12243 = new DicomUID("1.2.840.10008.6.1.634", "Left Ventricle Finding Sites (12243)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Congenital Finding Sites (12244)</summary>
		public readonly static DicomUID CongenitalFindingSites12244 = new DicomUID("1.2.840.10008.6.1.635", "Congenital Finding Sites (12244)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Surface Processing Algorithm Families (7162)</summary>
		public readonly static DicomUID SurfaceProcessingAlgorithmFamilies7162 = new DicomUID("1.2.840.10008.6.1.636", "Surface Processing Algorithm Families (7162)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Test Procedure Phases (3207)</summary>
		public readonly static DicomUID StressTestProcedurePhases3207 = new DicomUID("1.2.840.10008.6.1.637", "Stress Test Procedure Phases (3207)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stages (3778)</summary>
		public readonly static DicomUID Stages3778 = new DicomUID("1.2.840.10008.6.1.638", "Stages (3778)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: S-M-L Size Descriptor (252)</summary>
		public readonly static DicomUID SMLSizeDescriptor252 = new DicomUID("1.2.840.10008.6.1.735", "S-M-L Size Descriptor (252)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Major Coronary Arteries (3016)</summary>
		public readonly static DicomUID MajorCoronaryArteries3016 = new DicomUID("1.2.840.10008.6.1.736", "Major Coronary Arteries (3016)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Units of Radioactivity (3083)</summary>
		public readonly static DicomUID UnitsOfRadioactivity3083 = new DicomUID("1.2.840.10008.6.1.737", "Units of Radioactivity (3083)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Rest-Stress (3102)</summary>
		public readonly static DicomUID RestStress3102 = new DicomUID("1.2.840.10008.6.1.738", "Rest-Stress (3102)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: PET Cardiology Protocols (3106)</summary>
		public readonly static DicomUID PETCardiologyProtocols3106 = new DicomUID("1.2.840.10008.6.1.739", "PET Cardiology Protocols (3106)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: PET Cardiology Radiopharmaceuticals (3107)</summary>
		public readonly static DicomUID PETCardiologyRadiopharmaceuticals3107 = new DicomUID("1.2.840.10008.6.1.740", "PET Cardiology Radiopharmaceuticals (3107)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: NM/PET Procedures (3108)</summary>
		public readonly static DicomUID NMPETProcedures3108 = new DicomUID("1.2.840.10008.6.1.741", "NM/PET Procedures (3108)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Nuclear Cardiology Protocols (3110)</summary>
		public readonly static DicomUID NuclearCardiologyProtocols3110 = new DicomUID("1.2.840.10008.6.1.742", "Nuclear Cardiology Protocols (3110)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Nuclear Cardiology Radiopharmaceuticals (3111)</summary>
		public readonly static DicomUID NuclearCardiologyRadiopharmaceuticals3111 = new DicomUID("1.2.840.10008.6.1.743", "Nuclear Cardiology Radiopharmaceuticals (3111)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Attenuation Correction (3112)</summary>
		public readonly static DicomUID AttenuationCorrection3112 = new DicomUID("1.2.840.10008.6.1.744", "Attenuation Correction (3112)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Types of Perfusion Defects (3113)</summary>
		public readonly static DicomUID TypesOfPerfusionDefects3113 = new DicomUID("1.2.840.10008.6.1.745", "Types of Perfusion Defects (3113)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Study Quality (3114)</summary>
		public readonly static DicomUID StudyQuality3114 = new DicomUID("1.2.840.10008.6.1.746", "Study Quality (3114)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Imaging Quality Issues (3115)</summary>
		public readonly static DicomUID StressImagingQualityIssues3115 = new DicomUID("1.2.840.10008.6.1.747", "Stress Imaging Quality Issues (3115)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: NM Extracardiac Findings (3116)</summary>
		public readonly static DicomUID NMExtracardiacFindings3116 = new DicomUID("1.2.840.10008.6.1.748", "NM Extracardiac Findings (3116)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Attenuation Correction Methods (3117)</summary>
		public readonly static DicomUID AttenuationCorrectionMethods3117 = new DicomUID("1.2.840.10008.6.1.749", "Attenuation Correction Methods (3117)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Level of Risk (3118)</summary>
		public readonly static DicomUID LevelOfRisk3118 = new DicomUID("1.2.840.10008.6.1.750", "Level of Risk (3118)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: LV Function (3119)</summary>
		public readonly static DicomUID LVFunction3119 = new DicomUID("1.2.840.10008.6.1.751", "LV Function (3119)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Perfusion Findings (3120)</summary>
		public readonly static DicomUID PerfusionFindings3120 = new DicomUID("1.2.840.10008.6.1.752", "Perfusion Findings (3120)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Perfusion Morphology (3121)</summary>
		public readonly static DicomUID PerfusionMorphology3121 = new DicomUID("1.2.840.10008.6.1.753", "Perfusion Morphology (3121)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ventricular Enlargement (3122)</summary>
		public readonly static DicomUID VentricularEnlargement3122 = new DicomUID("1.2.840.10008.6.1.754", "Ventricular Enlargement (3122)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Test Procedure (3200)</summary>
		public readonly static DicomUID StressTestProcedure3200 = new DicomUID("1.2.840.10008.6.1.755", "Stress Test Procedure (3200)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Indications for Stress Test (3201)</summary>
		public readonly static DicomUID IndicationsForStressTest3201 = new DicomUID("1.2.840.10008.6.1.756", "Indications for Stress Test (3201)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Chest Pain (3202)</summary>
		public readonly static DicomUID ChestPain3202 = new DicomUID("1.2.840.10008.6.1.757", "Chest Pain (3202)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Exerciser Device (3203)</summary>
		public readonly static DicomUID ExerciserDevice3203 = new DicomUID("1.2.840.10008.6.1.758", "Exerciser Device (3203)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Agents (3204)</summary>
		public readonly static DicomUID StressAgents3204 = new DicomUID("1.2.840.10008.6.1.759", "Stress Agents (3204)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Indications for Pharmacological Stress Test (3205)</summary>
		public readonly static DicomUID IndicationsForPharmacologicalStressTest3205 = new DicomUID("1.2.840.10008.6.1.760", "Indications for Pharmacological Stress Test (3205)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Non-invasive Cardiac Imaging Procedures (3206)</summary>
		public readonly static DicomUID NonInvasiveCardiacImagingProcedures3206 = new DicomUID("1.2.840.10008.6.1.761", "Non-invasive Cardiac Imaging Procedures (3206)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Summary Codes Exercise ECG (3208)</summary>
		public readonly static DicomUID SummaryCodesExerciseECG3208 = new DicomUID("1.2.840.10008.6.1.763", "Summary Codes Exercise ECG (3208)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Summary Codes Stress Imaging (3209)</summary>
		public readonly static DicomUID SummaryCodesStressImaging3209 = new DicomUID("1.2.840.10008.6.1.764", "Summary Codes Stress Imaging (3209)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Speed of Response (3210)</summary>
		public readonly static DicomUID SpeedOfResponse3210 = new DicomUID("1.2.840.10008.6.1.765", "Speed of Response (3210)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: BP Response (3211)</summary>
		public readonly static DicomUID BPResponse3211 = new DicomUID("1.2.840.10008.6.1.766", "BP Response (3211)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Treadmill Speed (3212)</summary>
		public readonly static DicomUID TreadmillSpeed3212 = new DicomUID("1.2.840.10008.6.1.767", "Treadmill Speed (3212)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Hemodynamic Findings (3213)</summary>
		public readonly static DicomUID StressHemodynamicFindings3213 = new DicomUID("1.2.840.10008.6.1.768", "Stress Hemodynamic Findings (3213)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Perfusion Finding Method (3215)</summary>
		public readonly static DicomUID PerfusionFindingMethod3215 = new DicomUID("1.2.840.10008.6.1.769", "Perfusion Finding Method (3215)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Comparison Finding (3217)</summary>
		public readonly static DicomUID ComparisonFinding3217 = new DicomUID("1.2.840.10008.6.1.770", "Comparison Finding (3217)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Symptoms (3220)</summary>
		public readonly static DicomUID StressSymptoms3220 = new DicomUID("1.2.840.10008.6.1.771", "Stress Symptoms (3220)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Test Termination Reasons (3221)</summary>
		public readonly static DicomUID StressTestTerminationReasons3221 = new DicomUID("1.2.840.10008.6.1.772", "Stress Test Termination Reasons (3221)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: QTc Measurements (3227)</summary>
		public readonly static DicomUID QTcMeasurements3227 = new DicomUID("1.2.840.10008.6.1.773", "QTc Measurements (3227)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Timing Measurements (3228)</summary>
		public readonly static DicomUID ECGTimingMeasurements3228 = new DicomUID("1.2.840.10008.6.1.774", "ECG Timing Measurements (3228)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Axis Measurements (3229)</summary>
		public readonly static DicomUID ECGAxisMeasurements3229 = new DicomUID("1.2.840.10008.6.1.775", "ECG Axis Measurements (3229)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Findings (3230)</summary>
		public readonly static DicomUID ECGFindings3230 = new DicomUID("1.2.840.10008.6.1.776", "ECG Findings (3230)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ST Segment Findings (3231)</summary>
		public readonly static DicomUID STSegmentFindings3231 = new DicomUID("1.2.840.10008.6.1.777", "ST Segment Findings (3231)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ST Segment Location (3232)</summary>
		public readonly static DicomUID STSegmentLocation3232 = new DicomUID("1.2.840.10008.6.1.778", "ST Segment Location (3232)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ST Segment Morphology (3233)</summary>
		public readonly static DicomUID STSegmentMorphology3233 = new DicomUID("1.2.840.10008.6.1.779", "ST Segment Morphology (3233)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ectopic Beat Morphology (3234)</summary>
		public readonly static DicomUID EctopicBeatMorphology3234 = new DicomUID("1.2.840.10008.6.1.780", "Ectopic Beat Morphology (3234)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Perfusion Comparison Findings (3235)</summary>
		public readonly static DicomUID PerfusionComparisonFindings3235 = new DicomUID("1.2.840.10008.6.1.781", "Perfusion Comparison Findings (3235)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Tolerance Comparison Findings (3236)</summary>
		public readonly static DicomUID ToleranceComparisonFindings3236 = new DicomUID("1.2.840.10008.6.1.782", "Tolerance Comparison Findings (3236)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Wall Motion Comparison Findings (3237)</summary>
		public readonly static DicomUID WallMotionComparisonFindings3237 = new DicomUID("1.2.840.10008.6.1.783", "Wall Motion Comparison Findings (3237)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Stress Scoring Scales (3238)</summary>
		public readonly static DicomUID StressScoringScales3238 = new DicomUID("1.2.840.10008.6.1.784", "Stress Scoring Scales (3238)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Perceived Exertion Scales (3239)</summary>
		public readonly static DicomUID PerceivedExertionScales3239 = new DicomUID("1.2.840.10008.6.1.785", "Perceived Exertion Scales (3239)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ventricle Identification (3463)</summary>
		public readonly static DicomUID VentricleIdentification3463 = new DicomUID("1.2.840.10008.6.1.786", "Ventricle Identification (3463)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Colon Overall Assessment (6200)</summary>
		public readonly static DicomUID ColonOverallAssessment6200 = new DicomUID("1.2.840.10008.6.1.787", "Colon Overall Assessment (6200)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Colon Finding or Feature (6201)</summary>
		public readonly static DicomUID ColonFindingOrFeature6201 = new DicomUID("1.2.840.10008.6.1.788", "Colon Finding or Feature (6201)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Colon Finding or Feature Modifier (6202)</summary>
		public readonly static DicomUID ColonFindingOrFeatureModifier6202 = new DicomUID("1.2.840.10008.6.1.789", "Colon Finding or Feature Modifier (6202)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Colon Non-Lesion Object Type (6203)</summary>
		public readonly static DicomUID ColonNonLesionObjectType6203 = new DicomUID("1.2.840.10008.6.1.790", "Colon Non-Lesion Object Type (6203)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Anatomic Non-Colon Findings (6204)</summary>
		public readonly static DicomUID AnatomicNonColonFindings6204 = new DicomUID("1.2.840.10008.6.1.791", "Anatomic Non-Colon Findings (6204)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Clockface Location for Colon (6205)</summary>
		public readonly static DicomUID ClockfaceLocationForColon6205 = new DicomUID("1.2.840.10008.6.1.792", "Clockface Location for Colon (6205)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Recumbent Patient Orientation for Colon (6206)</summary>
		public readonly static DicomUID RecumbentPatientOrientationForColon6206 = new DicomUID("1.2.840.10008.6.1.793", "Recumbent Patient Orientation for Colon (6206)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Colon Quantitative Temporal Difference Type (6207)</summary>
		public readonly static DicomUID ColonQuantitativeTemporalDifferenceType6207 = new DicomUID("1.2.840.10008.6.1.794", "Colon Quantitative Temporal Difference Type (6207)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Colon Types of Quality Control Standard (6208)</summary>
		public readonly static DicomUID ColonTypesOfQualityControlStandard6208 = new DicomUID("1.2.840.10008.6.1.795", "Colon Types of Quality Control Standard (6208)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Colon Morphology Descriptor (6209)</summary>
		public readonly static DicomUID ColonMorphologyDescriptor6209 = new DicomUID("1.2.840.10008.6.1.796", "Colon Morphology Descriptor (6209)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Location in Intestinal Tract (6210)</summary>
		public readonly static DicomUID LocationInIntestinalTract6210 = new DicomUID("1.2.840.10008.6.1.797", "Location in Intestinal Tract (6210)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Attenuation Coefficient Descriptors (6211)</summary>
		public readonly static DicomUID AttenuationCoefficientDescriptors6211 = new DicomUID("1.2.840.10008.6.1.798", "Attenuation Coefficient Descriptors (6211)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calculated Value for Colon Findings (6212)</summary>
		public readonly static DicomUID CalculatedValueForColonFindings6212 = new DicomUID("1.2.840.10008.6.1.799", "Calculated Value for Colon Findings (6212)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Horizontal Directions (4214)</summary>
		public readonly static DicomUID OphthalmicHorizontalDirections4214 = new DicomUID("1.2.840.10008.6.1.800", "Ophthalmic Horizontal Directions (4214)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Vertical Directions (4215)</summary>
		public readonly static DicomUID OphthalmicVerticalDirections4215 = new DicomUID("1.2.840.10008.6.1.801", "Ophthalmic Vertical Directions (4215)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Visual Acuity Type (4216)</summary>
		public readonly static DicomUID OphthalmicVisualAcuityType4216 = new DicomUID("1.2.840.10008.6.1.802", "Ophthalmic Visual Acuity Type (4216)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Arterial Pulse Waveform (3004)</summary>
		public readonly static DicomUID ArterialPulseWaveform3004 = new DicomUID("1.2.840.10008.6.1.803", "Arterial Pulse Waveform (3004)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Respiration Waveform (3005)</summary>
		public readonly static DicomUID RespirationWaveform3005 = new DicomUID("1.2.840.10008.6.1.804", "Respiration Waveform (3005)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Contrast/Bolus Agents (12030)</summary>
		public readonly static DicomUID UltrasoundContrastBolusAgents12030 = new DicomUID("1.2.840.10008.6.1.805", "Ultrasound Contrast/Bolus Agents (12030)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Protocol Interval Events (12031)</summary>
		public readonly static DicomUID ProtocolIntervalEvents12031 = new DicomUID("1.2.840.10008.6.1.806", "Protocol Interval Events (12031)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Transducer Scan Pattern (12032)</summary>
		public readonly static DicomUID TransducerScanPattern12032 = new DicomUID("1.2.840.10008.6.1.807", "Transducer Scan Pattern (12032)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Transducer Geometry (12033)</summary>
		public readonly static DicomUID UltrasoundTransducerGeometry12033 = new DicomUID("1.2.840.10008.6.1.808", "Ultrasound Transducer Geometry (12033)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Transducer Beam Steering (12034)</summary>
		public readonly static DicomUID UltrasoundTransducerBeamSteering12034 = new DicomUID("1.2.840.10008.6.1.809", "Ultrasound Transducer Beam Steering (12034)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ultrasound Transducer Application (12035)</summary>
		public readonly static DicomUID UltrasoundTransducerApplication12035 = new DicomUID("1.2.840.10008.6.1.810", "Ultrasound Transducer Application (12035)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Instance Availability Status (50)</summary>
		public readonly static DicomUID InstanceAvailabilityStatus50 = new DicomUID("1.2.840.10008.6.1.811", "Instance Availability Status (50)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Modality PPS Discontinuation Reasons (9301)</summary>
		public readonly static DicomUID ModalityPPSDiscontinuationReasons9301 = new DicomUID("1.2.840.10008.6.1.812", "Modality PPS Discontinuation Reasons (9301)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Media Import PPS Discontinuation Reasons (9302)</summary>
		public readonly static DicomUID MediaImportPPSDiscontinuationReasons9302 = new DicomUID("1.2.840.10008.6.1.813", "Media Import PPS Discontinuation Reasons (9302)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: DX Anatomy Imaged for Animals (7482)</summary>
		public readonly static DicomUID DXAnatomyImagedForAnimals7482 = new DicomUID("1.2.840.10008.6.1.814", "DX Anatomy Imaged for Animals (7482)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Common Anatomic Regions for Animals (7483)</summary>
		public readonly static DicomUID CommonAnatomicRegionsForAnimals7483 = new DicomUID("1.2.840.10008.6.1.815", "Common Anatomic Regions for Animals (7483)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: DX View for Animals (7484)</summary>
		public readonly static DicomUID DXViewForAnimals7484 = new DicomUID("1.2.840.10008.6.1.816", "DX View for Animals (7484)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Institutional Departments, Units and Services (7030)</summary>
		public readonly static DicomUID InstitutionalDepartmentsUnitsAndServices7030 = new DicomUID("1.2.840.10008.6.1.817", "Institutional Departments, Units and Services (7030)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Purpose Of Reference to Predecessor Report (7009)</summary>
		public readonly static DicomUID PurposeOfReferenceToPredecessorReport7009 = new DicomUID("1.2.840.10008.6.1.818", "Purpose Of Reference to Predecessor Report (7009)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Fixation Quality During Acquisition (4220)</summary>
		public readonly static DicomUID VisualFixationQualityDuringAcquisition4220 = new DicomUID("1.2.840.10008.6.1.819", "Visual Fixation Quality During Acquisition (4220)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Fixation Quality Problem (4221)</summary>
		public readonly static DicomUID VisualFixationQualityProblem4221 = new DicomUID("1.2.840.10008.6.1.820", "Visual Fixation Quality Problem (4221)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Macular Grid Problem (4222)</summary>
		public readonly static DicomUID OphthalmicMacularGridProblem4222 = new DicomUID("1.2.840.10008.6.1.821", "Ophthalmic Macular Grid Problem (4222)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Organizations (5002)</summary>
		public readonly static DicomUID Organizations5002 = new DicomUID("1.2.840.10008.6.1.822", "Organizations (5002)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Mixed Breeds (7486)</summary>
		public readonly static DicomUID MixedBreeds7486 = new DicomUID("1.2.840.10008.6.1.823", "Mixed Breeds (7486)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Broselow-Luten Pediatric Size Categories (7040)</summary>
		public readonly static DicomUID BroselowLutenPediatricSizeCategories7040 = new DicomUID("1.2.840.10008.6.1.824", "Broselow-Luten Pediatric Size Categories (7040)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Calcium Scoring Patient Size Categories (7042)</summary>
		public readonly static DicomUID CalciumScoringPatientSizeCategories7042 = new DicomUID("1.2.840.10008.6.1.825", "Calcium Scoring Patient Size Categories (7042)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Report Titles (12245)</summary>
		public readonly static DicomUID CardiacUltrasoundReportTitles12245 = new DicomUID("1.2.840.10008.6.1.826", "Cardiac Ultrasound Report Titles (12245)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Indication for Study (12246)</summary>
		public readonly static DicomUID CardiacUltrasoundIndicationForStudy12246 = new DicomUID("1.2.840.10008.6.1.827", "Cardiac Ultrasound Indication for Study (12246)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Pediatric, Fetal and Congenital Cardiac Surgical Interventions (12247)</summary>
		public readonly static DicomUID PediatricFetalAndCongenitalCardiacSurgicalInterventions12247 = new DicomUID("1.2.840.10008.6.1.828", "Pediatric, Fetal and Congenital Cardiac Surgical Interventions (12247)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Summary Codes (12248)</summary>
		public readonly static DicomUID CardiacUltrasoundSummaryCodes12248 = new DicomUID("1.2.840.10008.6.1.829", "Cardiac Ultrasound Summary Codes (12248)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Fetal Summary Codes (12249)</summary>
		public readonly static DicomUID CardiacUltrasoundFetalSummaryCodes12249 = new DicomUID("1.2.840.10008.6.1.830", "Cardiac Ultrasound Fetal Summary Codes (12249)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Common Linear Measurements (12250)</summary>
		public readonly static DicomUID CardiacUltrasoundCommonLinearMeasurements12250 = new DicomUID("1.2.840.10008.6.1.831", "Cardiac Ultrasound Common Linear Measurements (12250)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Linear Valve Measurements (12251)</summary>
		public readonly static DicomUID CardiacUltrasoundLinearValveMeasurements12251 = new DicomUID("1.2.840.10008.6.1.832", "Cardiac Ultrasound Linear Valve Measurements (12251)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Cardiac Function (12252)</summary>
		public readonly static DicomUID CardiacUltrasoundCardiacFunction12252 = new DicomUID("1.2.840.10008.6.1.833", "Cardiac Ultrasound Cardiac Function (12252)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Area Measurements (12253)</summary>
		public readonly static DicomUID CardiacUltrasoundAreaMeasurements12253 = new DicomUID("1.2.840.10008.6.1.834", "Cardiac Ultrasound Area Measurements (12253)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Hemodynamic Measurements (12254)</summary>
		public readonly static DicomUID CardiacUltrasoundHemodynamicMeasurements12254 = new DicomUID("1.2.840.10008.6.1.835", "Cardiac Ultrasound Hemodynamic Measurements (12254)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Myocardium Measurements (12255)</summary>
		public readonly static DicomUID CardiacUltrasoundMyocardiumMeasurements12255 = new DicomUID("1.2.840.10008.6.1.836", "Cardiac Ultrasound Myocardium Measurements (12255)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Common Linear Flow Measurements (12256)</summary>
		public readonly static DicomUID CardiacUltrasoundCommonLinearFlowMeasurements12256 = new DicomUID("1.2.840.10008.6.1.837", "Cardiac Ultrasound Common Linear Flow Measurements (12256)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Left Ventricle (12257)</summary>
		public readonly static DicomUID CardiacUltrasoundLeftVentricle12257 = new DicomUID("1.2.840.10008.6.1.838", "Cardiac Ultrasound Left Ventricle (12257)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Right Ventricle (12258)</summary>
		public readonly static DicomUID CardiacUltrasoundRightVentricle12258 = new DicomUID("1.2.840.10008.6.1.839", "Cardiac Ultrasound Right Ventricle (12258)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Ventricles Measurements (12259)</summary>
		public readonly static DicomUID CardiacUltrasoundVentriclesMeasurements12259 = new DicomUID("1.2.840.10008.6.1.840", "Cardiac Ultrasound Ventricles Measurements (12259)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Pulmonary Artery (12260)</summary>
		public readonly static DicomUID CardiacUltrasoundPulmonaryArtery12260 = new DicomUID("1.2.840.10008.6.1.841", "Cardiac Ultrasound Pulmonary Artery (12260)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Pulmonary Vein (12261)</summary>
		public readonly static DicomUID CardiacUltrasoundPulmonaryVein12261 = new DicomUID("1.2.840.10008.6.1.842", "Cardiac Ultrasound Pulmonary Vein (12261)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Pulmonary Valve (12262)</summary>
		public readonly static DicomUID CardiacUltrasoundPulmonaryValve12262 = new DicomUID("1.2.840.10008.6.1.843", "Cardiac Ultrasound Pulmonary Valve (12262)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Venous Return Pulmonary Measurements (12263)</summary>
		public readonly static DicomUID CardiacUltrasoundVenousReturnPulmonaryMeasurements12263 = new DicomUID("1.2.840.10008.6.1.844", "Cardiac Ultrasound Venous Return Pulmonary Measurements (12263)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Venous Return Systemic Measurements (12264)</summary>
		public readonly static DicomUID CardiacUltrasoundVenousReturnSystemicMeasurements12264 = new DicomUID("1.2.840.10008.6.1.845", "Cardiac Ultrasound Venous Return Systemic Measurements (12264)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Atria and Atrial Septum Measurements (12265)</summary>
		public readonly static DicomUID CardiacUltrasoundAtriaAndAtrialSeptumMeasurements12265 = new DicomUID("1.2.840.10008.6.1.846", "Cardiac Ultrasound Atria and Atrial Septum Measurements (12265)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Mitral Valve (12266)</summary>
		public readonly static DicomUID CardiacUltrasoundMitralValve12266 = new DicomUID("1.2.840.10008.6.1.847", "Cardiac Ultrasound Mitral Valve (12266)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Tricuspid Valve (12267)</summary>
		public readonly static DicomUID CardiacUltrasoundTricuspidValve12267 = new DicomUID("1.2.840.10008.6.1.848", "Cardiac Ultrasound Tricuspid Valve (12267)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Atrioventricular Valves Measurements (12268)</summary>
		public readonly static DicomUID CardiacUltrasoundAtrioventricularValvesMeasurements12268 = new DicomUID("1.2.840.10008.6.1.849", "Cardiac Ultrasound Atrioventricular Valves Measurements (12268)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Interventricular Septum Measurements (12269)</summary>
		public readonly static DicomUID CardiacUltrasoundInterventricularSeptumMeasurements12269 = new DicomUID("1.2.840.10008.6.1.850", "Cardiac Ultrasound Interventricular Septum Measurements (12269)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Aortic Valve (12270)</summary>
		public readonly static DicomUID CardiacUltrasoundAorticValve12270 = new DicomUID("1.2.840.10008.6.1.851", "Cardiac Ultrasound Aortic Valve (12270)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Outflow Tracts Measurements (12271)</summary>
		public readonly static DicomUID CardiacUltrasoundOutflowTractsMeasurements12271 = new DicomUID("1.2.840.10008.6.1.852", "Cardiac Ultrasound Outflow Tracts Measurements (12271)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Semilunar Valves, Annulus and Sinuses Measurements (12272)</summary>
		public readonly static DicomUID CardiacUltrasoundSemilunarValvesAnnulusAndSinusesMeasurements12272 = new DicomUID("1.2.840.10008.6.1.853", "Cardiac Ultrasound Semilunar Valves, Annulus and Sinuses Measurements (12272)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Aortic Sinotubular Junction (12273)</summary>
		public readonly static DicomUID CardiacUltrasoundAorticSinotubularJunction12273 = new DicomUID("1.2.840.10008.6.1.854", "Cardiac Ultrasound Aortic Sinotubular Junction (12273)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Aorta Measurements (12274)</summary>
		public readonly static DicomUID CardiacUltrasoundAortaMeasurements12274 = new DicomUID("1.2.840.10008.6.1.855", "Cardiac Ultrasound Aorta Measurements (12274)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Coronary Arteries Measurements (12275)</summary>
		public readonly static DicomUID CardiacUltrasoundCoronaryArteriesMeasurements12275 = new DicomUID("1.2.840.10008.6.1.856", "Cardiac Ultrasound Coronary Arteries Measurements (12275)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Aorto Pulmonary Connections Measurements (12276)</summary>
		public readonly static DicomUID CardiacUltrasoundAortoPulmonaryConnectionsMeasurements12276 = new DicomUID("1.2.840.10008.6.1.857", "Cardiac Ultrasound Aorto Pulmonary Connections Measurements (12276)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Pericardium and Pleura Measurements (12277)</summary>
		public readonly static DicomUID CardiacUltrasoundPericardiumAndPleuraMeasurements12277 = new DicomUID("1.2.840.10008.6.1.858", "Cardiac Ultrasound Pericardium and Pleura Measurements (12277)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Fetal General Measurements (12279)</summary>
		public readonly static DicomUID CardiacUltrasoundFetalGeneralMeasurements12279 = new DicomUID("1.2.840.10008.6.1.859", "Cardiac Ultrasound Fetal General Measurements (12279)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Target Sites (12280)</summary>
		public readonly static DicomUID CardiacUltrasoundTargetSites12280 = new DicomUID("1.2.840.10008.6.1.860", "Cardiac Ultrasound Target Sites (12280)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Target Site Modifiers (12281)</summary>
		public readonly static DicomUID CardiacUltrasoundTargetSiteModifiers12281 = new DicomUID("1.2.840.10008.6.1.861", "Cardiac Ultrasound Target Site Modifiers (12281)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Venous Return Systemic Finding Sites (12282)</summary>
		public readonly static DicomUID CardiacUltrasoundVenousReturnSystemicFindingSites12282 = new DicomUID("1.2.840.10008.6.1.862", "Cardiac Ultrasound Venous Return Systemic Finding Sites (12282)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Venous Return Pulmonary Finding Sites (12283)</summary>
		public readonly static DicomUID CardiacUltrasoundVenousReturnPulmonaryFindingSites12283 = new DicomUID("1.2.840.10008.6.1.863", "Cardiac Ultrasound Venous Return Pulmonary Finding Sites (12283)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Atria and Atrial Septum Finding Sites (12284)</summary>
		public readonly static DicomUID CardiacUltrasoundAtriaAndAtrialSeptumFindingSites12284 = new DicomUID("1.2.840.10008.6.1.864", "Cardiac Ultrasound Atria and Atrial Septum Finding Sites (12284)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Atrioventricular Valves Findiing Sites (12285)</summary>
		public readonly static DicomUID CardiacUltrasoundAtrioventricularValvesFindiingSites12285 = new DicomUID("1.2.840.10008.6.1.865", "Cardiac Ultrasound Atrioventricular Valves Findiing Sites (12285)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Interventricular Septum Finding Sites (12286)</summary>
		public readonly static DicomUID CardiacUltrasoundInterventricularSeptumFindingSites12286 = new DicomUID("1.2.840.10008.6.1.866", "Cardiac Ultrasound Interventricular Septum Finding Sites (12286)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Ventricles Finding Sites (12287)</summary>
		public readonly static DicomUID CardiacUltrasoundVentriclesFindingSites12287 = new DicomUID("1.2.840.10008.6.1.867", "Cardiac Ultrasound Ventricles Finding Sites (12287)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Outflow Tracts Finding Sites (12288)</summary>
		public readonly static DicomUID CardiacUltrasoundOutflowTractsFindingSites12288 = new DicomUID("1.2.840.10008.6.1.868", "Cardiac Ultrasound Outflow Tracts Finding Sites (12288)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Semilunar Valves, Annulus and Sinuses Finding Sites (12289)</summary>
		public readonly static DicomUID CardiacUltrasoundSemilunarValvesAnnulusAndSinusesFindingSites12289 = new DicomUID("1.2.840.10008.6.1.869", "Cardiac Ultrasound Semilunar Valves, Annulus and Sinuses Finding Sites (12289)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Pulmonary Arteries Finding Sites (12290)</summary>
		public readonly static DicomUID CardiacUltrasoundPulmonaryArteriesFindingSites12290 = new DicomUID("1.2.840.10008.6.1.870", "Cardiac Ultrasound Pulmonary Arteries Finding Sites (12290)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Aorta Finding Sites (12291)</summary>
		public readonly static DicomUID CardiacUltrasoundAortaFindingSites12291 = new DicomUID("1.2.840.10008.6.1.871", "Cardiac Ultrasound Aorta Finding Sites (12291)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Coronary Arteries Finding Sites (12292)</summary>
		public readonly static DicomUID CardiacUltrasoundCoronaryArteriesFindingSites12292 = new DicomUID("1.2.840.10008.6.1.872", "Cardiac Ultrasound Coronary Arteries Finding Sites (12292)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Aorto Pulmonary Connections Finding Sites (12293)</summary>
		public readonly static DicomUID CardiacUltrasoundAortoPulmonaryConnectionsFindingSites12293 = new DicomUID("1.2.840.10008.6.1.873", "Cardiac Ultrasound Aorto Pulmonary Connections Finding Sites (12293)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Cardiac Ultrasound Pericardium and Pleura Finding Sites (12294)</summary>
		public readonly static DicomUID CardiacUltrasoundPericardiumAndPleuraFindingSites12294 = new DicomUID("1.2.840.10008.6.1.874", "Cardiac Ultrasound Pericardium and Pleura Finding Sites (12294)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Ultrasound Axial Measurements Type (4230)</summary>
		public readonly static DicomUID OphthalmicUltrasoundAxialMeasurementsType4230 = new DicomUID("1.2.840.10008.6.1.876", "Ophthalmic Ultrasound Axial Measurements Type (4230)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lens Status (4231)</summary>
		public readonly static DicomUID LensStatus4231 = new DicomUID("1.2.840.10008.6.1.877", "Lens Status (4231)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Vitreous Status (4232)</summary>
		public readonly static DicomUID VitreousStatus4232 = new DicomUID("1.2.840.10008.6.1.878", "Vitreous Status (4232)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Axial Length Measurements Segment Names (4233)</summary>
		public readonly static DicomUID OphthalmicAxialLengthMeasurementsSegmentNames4233 = new DicomUID("1.2.840.10008.6.1.879", "Ophthalmic Axial Length Measurements Segment Names (4233)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Refractive Surgery Types (4234)</summary>
		public readonly static DicomUID RefractiveSurgeryTypes4234 = new DicomUID("1.2.840.10008.6.1.880", "Refractive Surgery Types (4234)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Keratometry Descriptors (4235)</summary>
		public readonly static DicomUID KeratometryDescriptors4235 = new DicomUID("1.2.840.10008.6.1.881", "Keratometry Descriptors (4235)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: IOL Calculation Formula (4236)</summary>
		public readonly static DicomUID IOLCalculationFormula4236 = new DicomUID("1.2.840.10008.6.1.882", "IOL Calculation Formula (4236)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Lens Constant Type (4237)</summary>
		public readonly static DicomUID LensConstantType4237 = new DicomUID("1.2.840.10008.6.1.883", "Lens Constant Type (4237)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Refractive Error Types (4238)</summary>
		public readonly static DicomUID RefractiveErrorTypes4238 = new DicomUID("1.2.840.10008.6.1.884", "Refractive Error Types (4238)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Anterior Chamber Depth Definition (4239)</summary>
		public readonly static DicomUID AnteriorChamberDepthDefinition4239 = new DicomUID("1.2.840.10008.6.1.885", "Anterior Chamber Depth Definition (4239)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Measurement or Calculation Data Source (4240)</summary>
		public readonly static DicomUID OphthalmicMeasurementOrCalculationDataSource4240 = new DicomUID("1.2.840.10008.6.1.886", "Ophthalmic Measurement or Calculation Data Source (4240)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Axial Length Selection Method (4241)</summary>
		public readonly static DicomUID OphthalmicAxialLengthSelectionMethod4241 = new DicomUID("1.2.840.10008.6.1.887", "Ophthalmic Axial Length Selection Method (4241)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Axial Length Quality Metric Type (4243)</summary>
		public readonly static DicomUID OphthalmicAxialLengthQualityMetricType4243 = new DicomUID("1.2.840.10008.6.1.889", "Ophthalmic Axial Length Quality Metric Type (4243)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Ophthalmic Agent Concentration Units (4244)</summary>
		public readonly static DicomUID OphthalmicAgentConcentrationUnits4244 = new DicomUID("1.2.840.10008.6.1.890", "Ophthalmic Agent Concentration Units (4244)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Functional condition present during acquisition (91)</summary>
		public readonly static DicomUID FunctionalConditionPresentDuringAcquisition91 = new DicomUID("1.2.840.10008.6.1.891", "Functional condition present during acquisition (91)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Joint position during acquisition (92)</summary>
		public readonly static DicomUID JointPositionDuringAcquisition92 = new DicomUID("1.2.840.10008.6.1.892", "Joint position during acquisition (92)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Joint positioning method (93)</summary>
		public readonly static DicomUID JointPositioningMethod93 = new DicomUID("1.2.840.10008.6.1.893", "Joint positioning method (93)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Physical force applied during acquisition (94)</summary>
		public readonly static DicomUID PhysicalForceAppliedDuringAcquisition94 = new DicomUID("1.2.840.10008.6.1.894", "Physical force applied during acquisition (94)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Control Variables Numeric (3690)</summary>
		public readonly static DicomUID ECGControlVariablesNumeric3690 = new DicomUID("1.2.840.10008.6.1.895", "ECG Control Variables Numeric (3690)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Control Variables Text (3691)</summary>
		public readonly static DicomUID ECGControlVariablesText3691 = new DicomUID("1.2.840.10008.6.1.896", "ECG Control Variables Text (3691)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: WSI Referenced Image Purposes of Reference (8120)</summary>
		public readonly static DicomUID WSIReferencedImagePurposesOfReference8120 = new DicomUID("1.2.840.10008.6.1.897", "WSI Referenced Image Purposes of Reference (8120)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: WSI Microscopy Lens Type (8121)</summary>
		public readonly static DicomUID WSIMicroscopyLensType8121 = new DicomUID("1.2.840.10008.6.1.898", "WSI Microscopy Lens Type (8121)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Microscopy Illuminator and Sensor Color (8122)</summary>
		public readonly static DicomUID MicroscopyIlluminatorAndSensorColor8122 = new DicomUID("1.2.840.10008.6.1.899", "Microscopy Illuminator and Sensor Color (8122)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Microscopy Illumination Method (8123)</summary>
		public readonly static DicomUID MicroscopyIlluminationMethod8123 = new DicomUID("1.2.840.10008.6.1.900", "Microscopy Illumination Method (8123)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Microscopy Filter (8124)</summary>
		public readonly static DicomUID MicroscopyFilter8124 = new DicomUID("1.2.840.10008.6.1.901", "Microscopy Filter (8124)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Microscopy Illuminator Type (8125)</summary>
		public readonly static DicomUID MicroscopyIlluminatorType8125 = new DicomUID("1.2.840.10008.6.1.902", "Microscopy Illuminator Type (8125)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Audit Event ID (400)</summary>
		public readonly static DicomUID AuditEventID400 = new DicomUID("1.2.840.10008.6.1.903", "Audit Event ID (400)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Audit Event Type Code (401)</summary>
		public readonly static DicomUID AuditEventTypeCode401 = new DicomUID("1.2.840.10008.6.1.904", "Audit Event Type Code (401)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Audit Active Participant Role ID Code (402)</summary>
		public readonly static DicomUID AuditActiveParticipantRoleIDCode402 = new DicomUID("1.2.840.10008.6.1.905", "Audit Active Participant Role ID Code (402)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Security Alert Type Code (403)</summary>
		public readonly static DicomUID SecurityAlertTypeCode403 = new DicomUID("1.2.840.10008.6.1.906", "Security Alert Type Code (403)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Audit Participant Object ID Type Code (404)</summary>
		public readonly static DicomUID AuditParticipantObjectIDTypeCode404 = new DicomUID("1.2.840.10008.6.1.907", "Audit Participant Object ID Type Code (404)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Media Type Code (405)</summary>
		public readonly static DicomUID MediaTypeCode405 = new DicomUID("1.2.840.10008.6.1.908", "Media Type Code (405)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Static Perimetry Test Patterns (4250)</summary>
		public readonly static DicomUID VisualFieldStaticPerimetryTestPatterns4250 = new DicomUID("1.2.840.10008.6.1.909", "Visual Field Static Perimetry Test Patterns (4250)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Static Perimetry Test Strategies (4251)</summary>
		public readonly static DicomUID VisualFieldStaticPerimetryTestStrategies4251 = new DicomUID("1.2.840.10008.6.1.910", "Visual Field Static Perimetry Test Strategies (4251)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Static Perimetry Screening Test Modes (4252)</summary>
		public readonly static DicomUID VisualFieldStaticPerimetryScreeningTestModes4252 = new DicomUID("1.2.840.10008.6.1.911", "Visual Field Static Perimetry Screening Test Modes (4252)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Static Perimetry Fixation Strategy (4253)</summary>
		public readonly static DicomUID VisualFieldStaticPerimetryFixationStrategy4253 = new DicomUID("1.2.840.10008.6.1.912", "Visual Field Static Perimetry Fixation Strategy (4253)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Static Perimetry Test Analysis Results (4254)</summary>
		public readonly static DicomUID VisualFieldStaticPerimetryTestAnalysisResults4254 = new DicomUID("1.2.840.10008.6.1.913", "Visual Field Static Perimetry Test Analysis Results (4254)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Illumination Color (4255)</summary>
		public readonly static DicomUID VisualFieldIlluminationColor4255 = new DicomUID("1.2.840.10008.6.1.914", "Visual Field Illumination Color (4255)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Procedure Modifier (4256)</summary>
		public readonly static DicomUID VisualFieldProcedureModifier4256 = new DicomUID("1.2.840.10008.6.1.915", "Visual Field Procedure Modifier (4256)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Visual Field Global Index Name (4257)</summary>
		public readonly static DicomUID VisualFieldGlobalIndexName4257 = new DicomUID("1.2.840.10008.6.1.916", "Visual Field Global Index Name (4257)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Multi-Dimensional Image Model Component Semantics (7180)</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModelComponentSemantics7180 = new DicomUID("1.2.840.10008.6.1.917", "Abstract Multi-Dimensional Image Model Component Semantics (7180)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Multi-Dimensional Image Model Component Units (7181)</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModelComponentUnits7181 = new DicomUID("1.2.840.10008.6.1.918", "Abstract Multi-Dimensional Image Model Component Units (7181)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Multi-Dimensional Image Model Dimension Semantics (7182)</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModelDimensionSemantics7182 = new DicomUID("1.2.840.10008.6.1.919", "Abstract Multi-Dimensional Image Model Dimension Semantics (7182)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Multi-Dimensional Image Model Dimension Units (7183)</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModelDimensionUnits7183 = new DicomUID("1.2.840.10008.6.1.920", "Abstract Multi-Dimensional Image Model Dimension Units (7183)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Multi-Dimensional Image Model Axis Direction (7184)</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModelAxisDirection7184 = new DicomUID("1.2.840.10008.6.1.921", "Abstract Multi-Dimensional Image Model Axis Direction (7184)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Multi-Dimensional Image Model Axis Orientation (7185)</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModelAxisOrientation7185 = new DicomUID("1.2.840.10008.6.1.922", "Abstract Multi-Dimensional Image Model Axis Orientation (7185)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Abstract Multi-Dimensional Image Model Qualitative Dimension Sample Semantics (7186)</summary>
		public readonly static DicomUID AbstractMultiDimensionalImageModelQualitativeDimensionSampleSemantics7186 = new DicomUID("1.2.840.10008.6.1.923", "Abstract Multi-Dimensional Image Model Qualitative Dimension Sample Semantics (7186)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Planning Methods (7320)</summary>
		public readonly static DicomUID PlanningMethods7320 = new DicomUID("1.2.840.10008.6.1.924", "Planning Methods (7320)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: De-identification Method (7050)</summary>
		public readonly static DicomUID DeIdentificationMethod7050 = new DicomUID("1.2.840.10008.6.1.925", "De-identification Method (7050)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Measurement Orientation (12118)</summary>
		public readonly static DicomUID MeasurementOrientation12118 = new DicomUID("1.2.840.10008.6.1.926", "Measurement Orientation (12118)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ECG Global Waveform Durations (3689)</summary>
		public readonly static DicomUID ECGGlobalWaveformDurations3689 = new DicomUID("1.2.840.10008.6.1.927", "ECG Global Waveform Durations (3689)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: ICDs (3692)</summary>
		public readonly static DicomUID ICDs3692 = new DicomUID("1.2.840.10008.6.1.930", "ICDs (3692)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Radiotherapy General Workitem Definition (9241)</summary>
		public readonly static DicomUID RadiotherapyGeneralWorkitemDefinition9241 = new DicomUID("1.2.840.10008.6.1.931", "Radiotherapy General Workitem Definition (9241)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Radiotherapy Acquisition Workitem Definition (9242)</summary>
		public readonly static DicomUID RadiotherapyAcquisitionWorkitemDefinition9242 = new DicomUID("1.2.840.10008.6.1.932", "Radiotherapy Acquisition Workitem Definition (9242)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Radiotherapy Registration Workitem Definition (9243)</summary>
		public readonly static DicomUID RadiotherapyRegistrationWorkitemDefinition9243 = new DicomUID("1.2.840.10008.6.1.933", "Radiotherapy Registration Workitem Definition (9243)", DicomUidType.ContextGroupName, false);

		/// <summary>Context Group Name: Intravascular OCT Flush Agent (3850)</summary>
		public readonly static DicomUID IntravascularOCTFlushAgent3850 = new DicomUID("1.2.840.10008.6.1.934", "Intravascular OCT Flush Agent (3850)", DicomUidType.ContextGroupName, false);
	}
}