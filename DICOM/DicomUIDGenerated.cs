using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dicom {
	public partial class DicomUID {
		private static void LoadInternalUIDs() {
			_uids.Add(DicomUID.VerificationSOPClass.UID, DicomUID.VerificationSOPClass);
			_uids.Add(DicomUID.ImplicitVRLittleEndian.UID, DicomUID.ImplicitVRLittleEndian);
			_uids.Add(DicomUID.ExplicitVRLittleEndian.UID, DicomUID.ExplicitVRLittleEndian);
			_uids.Add(DicomUID.DeflatedExplicitVRLittleEndian.UID, DicomUID.DeflatedExplicitVRLittleEndian);
			_uids.Add(DicomUID.ExplicitVRBigEndian.UID, DicomUID.ExplicitVRBigEndian);
			_uids.Add(DicomUID.MPEG2MainProfileMainLevel.UID, DicomUID.MPEG2MainProfileMainLevel);
			_uids.Add(DicomUID.JPEGBaselineProcess1.UID, DicomUID.JPEGBaselineProcess1);
			_uids.Add(DicomUID.JPEGExtendedProcess2_4.UID, DicomUID.JPEGExtendedProcess2_4);
			_uids.Add(DicomUID.JPEGExtendedProcess3_5RETIRED.UID, DicomUID.JPEGExtendedProcess3_5RETIRED);
			_uids.Add(DicomUID.JPEGSpectralSelectionNonHierarchicalProcess6_8RETIRED.UID, DicomUID.JPEGSpectralSelectionNonHierarchicalProcess6_8RETIRED);
			_uids.Add(DicomUID.JPEGSpectralSelectionNonHierarchicalProcess7_9RETIRED.UID, DicomUID.JPEGSpectralSelectionNonHierarchicalProcess7_9RETIRED);
			_uids.Add(DicomUID.JPEGFullProgressionNonHierarchicalProcess10_12RETIRED.UID, DicomUID.JPEGFullProgressionNonHierarchicalProcess10_12RETIRED);
			_uids.Add(DicomUID.JPEGFullProgressionNonHierarchicalProcess11_13RETIRED.UID, DicomUID.JPEGFullProgressionNonHierarchicalProcess11_13RETIRED);
			_uids.Add(DicomUID.JPEGLosslessNonHierarchicalProcess14.UID, DicomUID.JPEGLosslessNonHierarchicalProcess14);
			_uids.Add(DicomUID.JPEGLosslessNonHierarchicalProcess15RETIRED.UID, DicomUID.JPEGLosslessNonHierarchicalProcess15RETIRED);
			_uids.Add(DicomUID.JPEGExtendedHierarchicalProcess16_18RETIRED.UID, DicomUID.JPEGExtendedHierarchicalProcess16_18RETIRED);
			_uids.Add(DicomUID.JPEGExtendedHierarchicalProcess17_19RETIRED.UID, DicomUID.JPEGExtendedHierarchicalProcess17_19RETIRED);
			_uids.Add(DicomUID.JPEGSpectralSelectionHierarchicalProcess20_22RETIRED.UID, DicomUID.JPEGSpectralSelectionHierarchicalProcess20_22RETIRED);
			_uids.Add(DicomUID.JPEGSpectralSelectionHierarchicalProcess21_23RETIRED.UID, DicomUID.JPEGSpectralSelectionHierarchicalProcess21_23RETIRED);
			_uids.Add(DicomUID.JPEGFullProgressionHierarchicalProcess24_26RETIRED.UID, DicomUID.JPEGFullProgressionHierarchicalProcess24_26RETIRED);
			_uids.Add(DicomUID.JPEGFullProgressionHierarchicalProcess25_27RETIRED.UID, DicomUID.JPEGFullProgressionHierarchicalProcess25_27RETIRED);
			_uids.Add(DicomUID.JPEGLosslessHierarchicalProcess28RETIRED.UID, DicomUID.JPEGLosslessHierarchicalProcess28RETIRED);
			_uids.Add(DicomUID.JPEGLosslessHierarchicalProcess29RETIRED.UID, DicomUID.JPEGLosslessHierarchicalProcess29RETIRED);
			_uids.Add(DicomUID.JPEGLosslessProcess14SV1.UID, DicomUID.JPEGLosslessProcess14SV1);
			_uids.Add(DicomUID.JPEGLSLosslessImageCompression.UID, DicomUID.JPEGLSLosslessImageCompression);
			_uids.Add(DicomUID.JPEGLSLossyNearLosslessImageCompression.UID, DicomUID.JPEGLSLossyNearLosslessImageCompression);
			_uids.Add(DicomUID.JPEG2000ImageCompressionLosslessOnly.UID, DicomUID.JPEG2000ImageCompressionLosslessOnly);
			_uids.Add(DicomUID.JPEG2000ImageCompression.UID, DicomUID.JPEG2000ImageCompression);
			_uids.Add(DicomUID.JPEG2000Part2MulticomponentImageCompressionLosslessOnly.UID, DicomUID.JPEG2000Part2MulticomponentImageCompressionLosslessOnly);
			_uids.Add(DicomUID.JPEG2000Part2MulticomponentImageCompression.UID, DicomUID.JPEG2000Part2MulticomponentImageCompression);
			_uids.Add(DicomUID.JPIPReferenced.UID, DicomUID.JPIPReferenced);
			_uids.Add(DicomUID.JPIPReferencedDeflate.UID, DicomUID.JPIPReferencedDeflate);
			_uids.Add(DicomUID.RLELossless.UID, DicomUID.RLELossless);
			_uids.Add(DicomUID.RFC2557MIMEEncapsulation.UID, DicomUID.RFC2557MIMEEncapsulation);
			_uids.Add(DicomUID.XMLEncoding.UID, DicomUID.XMLEncoding);
			_uids.Add(DicomUID.StorageCommitmentPushModelSOPClass.UID, DicomUID.StorageCommitmentPushModelSOPClass);
			_uids.Add(DicomUID.StorageCommitmentPushModelSOPInstance.UID, DicomUID.StorageCommitmentPushModelSOPInstance);
			_uids.Add(DicomUID.StorageCommitmentPullModelSOPClassRETIRED.UID, DicomUID.StorageCommitmentPullModelSOPClassRETIRED);
			_uids.Add(DicomUID.StorageCommitmentPullModelSOPInstanceRETIRED.UID, DicomUID.StorageCommitmentPullModelSOPInstanceRETIRED);
			_uids.Add(DicomUID.MediaStorageDirectoryStorage.UID, DicomUID.MediaStorageDirectoryStorage);
			_uids.Add(DicomUID.TalairachBrainAtlasFrameOfReference.UID, DicomUID.TalairachBrainAtlasFrameOfReference);
			_uids.Add(DicomUID.SPM2GRAYFrameOfReference.UID, DicomUID.SPM2GRAYFrameOfReference);
			_uids.Add(DicomUID.SPM2WHITEFrameOfReference.UID, DicomUID.SPM2WHITEFrameOfReference);
			_uids.Add(DicomUID.SPM2CSFFrameOfReference.UID, DicomUID.SPM2CSFFrameOfReference);
			_uids.Add(DicomUID.SPM2BRAINMASKFrameOfReference.UID, DicomUID.SPM2BRAINMASKFrameOfReference);
			_uids.Add(DicomUID.SPM2AVG305T1FrameOfReference.UID, DicomUID.SPM2AVG305T1FrameOfReference);
			_uids.Add(DicomUID.SPM2AVG152T1FrameOfReference.UID, DicomUID.SPM2AVG152T1FrameOfReference);
			_uids.Add(DicomUID.SPM2AVG152T2FrameOfReference.UID, DicomUID.SPM2AVG152T2FrameOfReference);
			_uids.Add(DicomUID.SPM2AVG152PDFrameOfReference.UID, DicomUID.SPM2AVG152PDFrameOfReference);
			_uids.Add(DicomUID.SPM2SINGLESUBJT1FrameOfReference.UID, DicomUID.SPM2SINGLESUBJT1FrameOfReference);
			_uids.Add(DicomUID.SPM2T1FrameOfReference.UID, DicomUID.SPM2T1FrameOfReference);
			_uids.Add(DicomUID.SPM2T2FrameOfReference.UID, DicomUID.SPM2T2FrameOfReference);
			_uids.Add(DicomUID.SPM2PDFrameOfReference.UID, DicomUID.SPM2PDFrameOfReference);
			_uids.Add(DicomUID.SPM2EPIFrameOfReference.UID, DicomUID.SPM2EPIFrameOfReference);
			_uids.Add(DicomUID.SPM2FILT1FrameOfReference.UID, DicomUID.SPM2FILT1FrameOfReference);
			_uids.Add(DicomUID.SPM2PETFrameOfReference.UID, DicomUID.SPM2PETFrameOfReference);
			_uids.Add(DicomUID.SPM2TRANSMFrameOfReference.UID, DicomUID.SPM2TRANSMFrameOfReference);
			_uids.Add(DicomUID.SPM2SPECTFrameOfReference.UID, DicomUID.SPM2SPECTFrameOfReference);
			_uids.Add(DicomUID.ICBM452T1FrameOfReference.UID, DicomUID.ICBM452T1FrameOfReference);
			_uids.Add(DicomUID.ICBMSingleSubjectMRIFrameOfReference.UID, DicomUID.ICBMSingleSubjectMRIFrameOfReference);
			_uids.Add(DicomUID.ProceduralEventLoggingSOPClass.UID, DicomUID.ProceduralEventLoggingSOPClass);
			_uids.Add(DicomUID.ProceduralEventLoggingSOPInstance.UID, DicomUID.ProceduralEventLoggingSOPInstance);
			_uids.Add(DicomUID.SubstanceAdministrationLoggingSOPClass.UID, DicomUID.SubstanceAdministrationLoggingSOPClass);
			_uids.Add(DicomUID.SubstanceAdministrationLoggingSOPInstance.UID, DicomUID.SubstanceAdministrationLoggingSOPInstance);
			_uids.Add(DicomUID.BasicStudyContentNotificationSOPClassRETIRED.UID, DicomUID.BasicStudyContentNotificationSOPClassRETIRED);
			_uids.Add(DicomUID.LDAPDicomDeviceName.UID, DicomUID.LDAPDicomDeviceName);
			_uids.Add(DicomUID.LDAPDicomAssociationInitiator.UID, DicomUID.LDAPDicomAssociationInitiator);
			_uids.Add(DicomUID.LDAPDicomAssociationAcceptor.UID, DicomUID.LDAPDicomAssociationAcceptor);
			_uids.Add(DicomUID.LDAPDicomHostname.UID, DicomUID.LDAPDicomHostname);
			_uids.Add(DicomUID.LDAPDicomPort.UID, DicomUID.LDAPDicomPort);
			_uids.Add(DicomUID.LDAPDicomSOPClass.UID, DicomUID.LDAPDicomSOPClass);
			_uids.Add(DicomUID.LDAPDicomTransferRole.UID, DicomUID.LDAPDicomTransferRole);
			_uids.Add(DicomUID.LDAPDicomTransferSyntax.UID, DicomUID.LDAPDicomTransferSyntax);
			_uids.Add(DicomUID.LDAPDicomPrimaryDeviceType.UID, DicomUID.LDAPDicomPrimaryDeviceType);
			_uids.Add(DicomUID.LDAPDicomRelatedDeviceReference.UID, DicomUID.LDAPDicomRelatedDeviceReference);
			_uids.Add(DicomUID.LDAPDicomPreferredCalledAETitle.UID, DicomUID.LDAPDicomPreferredCalledAETitle);
			_uids.Add(DicomUID.LDAPDicomDescription.UID, DicomUID.LDAPDicomDescription);
			_uids.Add(DicomUID.LDAPDicomTLSCyphersuite.UID, DicomUID.LDAPDicomTLSCyphersuite);
			_uids.Add(DicomUID.LDAPDicomAuthorizedNodeCertificateReference.UID, DicomUID.LDAPDicomAuthorizedNodeCertificateReference);
			_uids.Add(DicomUID.LDAPDicomThisNodeCertificateReference.UID, DicomUID.LDAPDicomThisNodeCertificateReference);
			_uids.Add(DicomUID.LDAPDicomInstalled.UID, DicomUID.LDAPDicomInstalled);
			_uids.Add(DicomUID.LDAPDicomStationName.UID, DicomUID.LDAPDicomStationName);
			_uids.Add(DicomUID.LDAPDicomDeviceSerialNumber.UID, DicomUID.LDAPDicomDeviceSerialNumber);
			_uids.Add(DicomUID.LDAPDicomInstitutionName.UID, DicomUID.LDAPDicomInstitutionName);
			_uids.Add(DicomUID.LDAPDicomInstitutionAddress.UID, DicomUID.LDAPDicomInstitutionAddress);
			_uids.Add(DicomUID.LDAPDicomInstitutionDepartmentName.UID, DicomUID.LDAPDicomInstitutionDepartmentName);
			_uids.Add(DicomUID.LDAPDicomIssuerOfPatientID.UID, DicomUID.LDAPDicomIssuerOfPatientID);
			_uids.Add(DicomUID.LDAPDicomManufacturer.UID, DicomUID.LDAPDicomManufacturer);
			_uids.Add(DicomUID.LDAPDicomPreferredCallingAETitle.UID, DicomUID.LDAPDicomPreferredCallingAETitle);
			_uids.Add(DicomUID.LDAPDicomSupportedCharacterSet.UID, DicomUID.LDAPDicomSupportedCharacterSet);
			_uids.Add(DicomUID.LDAPDicomManufacturerModelName.UID, DicomUID.LDAPDicomManufacturerModelName);
			_uids.Add(DicomUID.LDAPDicomSoftwareVersion.UID, DicomUID.LDAPDicomSoftwareVersion);
			_uids.Add(DicomUID.LDAPDicomVendorData.UID, DicomUID.LDAPDicomVendorData);
			_uids.Add(DicomUID.LDAPDicomAETitle.UID, DicomUID.LDAPDicomAETitle);
			_uids.Add(DicomUID.LDAPDicomNetworkConnectionReference.UID, DicomUID.LDAPDicomNetworkConnectionReference);
			_uids.Add(DicomUID.LDAPDicomApplicationCluster.UID, DicomUID.LDAPDicomApplicationCluster);
			_uids.Add(DicomUID.LDAPDicomConfigurationRoot.UID, DicomUID.LDAPDicomConfigurationRoot);
			_uids.Add(DicomUID.LDAPDicomDevicesRoot.UID, DicomUID.LDAPDicomDevicesRoot);
			_uids.Add(DicomUID.LDAPDicomUniqueAETitlesRegistryRoot.UID, DicomUID.LDAPDicomUniqueAETitlesRegistryRoot);
			_uids.Add(DicomUID.LDAPDicomDevice.UID, DicomUID.LDAPDicomDevice);
			_uids.Add(DicomUID.LDAPDicomNetworkAE.UID, DicomUID.LDAPDicomNetworkAE);
			_uids.Add(DicomUID.LDAPDicomNetworkConnection.UID, DicomUID.LDAPDicomNetworkConnection);
			_uids.Add(DicomUID.LDAPDicomUniqueAETitle.UID, DicomUID.LDAPDicomUniqueAETitle);
			_uids.Add(DicomUID.LDAPDicomTransferCapability.UID, DicomUID.LDAPDicomTransferCapability);
			_uids.Add(DicomUID.DICOMControlledTerminology.UID, DicomUID.DICOMControlledTerminology);
			_uids.Add(DicomUID.DICOMUIDRegistry.UID, DicomUID.DICOMUIDRegistry);
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
			_uids.Add(DicomUID.BasicFilmSessionSOPClass.UID, DicomUID.BasicFilmSessionSOPClass);
			_uids.Add(DicomUID.PrintJobSOPClass.UID, DicomUID.PrintJobSOPClass);
			_uids.Add(DicomUID.BasicAnnotationBoxSOPClass.UID, DicomUID.BasicAnnotationBoxSOPClass);
			_uids.Add(DicomUID.PrinterSOPClass.UID, DicomUID.PrinterSOPClass);
			_uids.Add(DicomUID.PrinterConfigurationRetrievalSOPClass.UID, DicomUID.PrinterConfigurationRetrievalSOPClass);
			_uids.Add(DicomUID.PrinterSOPInstance.UID, DicomUID.PrinterSOPInstance);
			_uids.Add(DicomUID.PrinterConfigurationRetrievalSOPInstance.UID, DicomUID.PrinterConfigurationRetrievalSOPInstance);
			_uids.Add(DicomUID.BasicColorPrintManagementMetaSOPClass.UID, DicomUID.BasicColorPrintManagementMetaSOPClass);
			_uids.Add(DicomUID.ReferencedColorPrintManagementMetaSOPClassRETIRED.UID, DicomUID.ReferencedColorPrintManagementMetaSOPClassRETIRED);
			_uids.Add(DicomUID.BasicFilmBoxSOPClass.UID, DicomUID.BasicFilmBoxSOPClass);
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
			_uids.Add(DicomUID.BasicGrayscaleImageBoxSOPClass.UID, DicomUID.BasicGrayscaleImageBoxSOPClass);
			_uids.Add(DicomUID.BasicColorImageBoxSOPClass.UID, DicomUID.BasicColorImageBoxSOPClass);
			_uids.Add(DicomUID.ReferencedImageBoxSOPClassRETIRED.UID, DicomUID.ReferencedImageBoxSOPClassRETIRED);
			_uids.Add(DicomUID.BasicGrayscalePrintManagementMetaSOPClass.UID, DicomUID.BasicGrayscalePrintManagementMetaSOPClass);
			_uids.Add(DicomUID.ReferencedGrayscalePrintManagementMetaSOPClassRETIRED.UID, DicomUID.ReferencedGrayscalePrintManagementMetaSOPClassRETIRED);
			_uids.Add(DicomUID.ComputedRadiographyImageStorage.UID, DicomUID.ComputedRadiographyImageStorage);
			_uids.Add(DicomUID.DigitalXRayImageStorageForPresentation.UID, DicomUID.DigitalXRayImageStorageForPresentation);
			_uids.Add(DicomUID.DigitalXRayImageStorageForProcessing.UID, DicomUID.DigitalXRayImageStorageForProcessing);
			_uids.Add(DicomUID.DigitalMammographyXRayImageStorageForPresentation.UID, DicomUID.DigitalMammographyXRayImageStorageForPresentation);
			_uids.Add(DicomUID.DigitalMammographyXRayImageStorageForProcessing.UID, DicomUID.DigitalMammographyXRayImageStorageForProcessing);
			_uids.Add(DicomUID.DigitalIntraoralXRayImageStorageForPresentation.UID, DicomUID.DigitalIntraoralXRayImageStorageForPresentation);
			_uids.Add(DicomUID.DigitalIntraoralXRayImageStorageForProcessing.UID, DicomUID.DigitalIntraoralXRayImageStorageForProcessing);
			_uids.Add(DicomUID.StandaloneModalityLUTStorageRETIRED.UID, DicomUID.StandaloneModalityLUTStorageRETIRED);
			_uids.Add(DicomUID.EncapsulatedPDFStorage.UID, DicomUID.EncapsulatedPDFStorage);
			_uids.Add(DicomUID.EncapsulatedCDAStorage.UID, DicomUID.EncapsulatedCDAStorage);
			_uids.Add(DicomUID.StandaloneVOILUTStorageRETIRED.UID, DicomUID.StandaloneVOILUTStorageRETIRED);
			_uids.Add(DicomUID.GrayscaleSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.GrayscaleSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.ColorSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.ColorSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.PseudoColorSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.PseudoColorSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.BlendingSoftcopyPresentationStateStorageSOPClass.UID, DicomUID.BlendingSoftcopyPresentationStateStorageSOPClass);
			_uids.Add(DicomUID.XRayAngiographicImageStorage.UID, DicomUID.XRayAngiographicImageStorage);
			_uids.Add(DicomUID.EnhancedXAImageStorage.UID, DicomUID.EnhancedXAImageStorage);
			_uids.Add(DicomUID.XRayRadiofluoroscopicImageStorage.UID, DicomUID.XRayRadiofluoroscopicImageStorage);
			_uids.Add(DicomUID.EnhancedXRFImageStorage.UID, DicomUID.EnhancedXRFImageStorage);
			_uids.Add(DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED.UID, DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED);
			_uids.Add(DicomUID.PositronEmissionTomographyImageStorage.UID, DicomUID.PositronEmissionTomographyImageStorage);
			_uids.Add(DicomUID.StandalonePETCurveStorageRETIRED.UID, DicomUID.StandalonePETCurveStorageRETIRED);
			_uids.Add(DicomUID.XRay3DAngiographicImageStorage.UID, DicomUID.XRay3DAngiographicImageStorage);
			_uids.Add(DicomUID.XRay3DCraniofacialImageStorage.UID, DicomUID.XRay3DCraniofacialImageStorage);
			_uids.Add(DicomUID.CTImageStorage.UID, DicomUID.CTImageStorage);
			_uids.Add(DicomUID.EnhancedCTImageStorage.UID, DicomUID.EnhancedCTImageStorage);
			_uids.Add(DicomUID.NuclearMedicineImageStorage.UID, DicomUID.NuclearMedicineImageStorage);
			_uids.Add(DicomUID.UltrasoundMultiframeImageStorageRETIRED.UID, DicomUID.UltrasoundMultiframeImageStorageRETIRED);
			_uids.Add(DicomUID.UltrasoundMultiframeImageStorage.UID, DicomUID.UltrasoundMultiframeImageStorage);
			_uids.Add(DicomUID.MRImageStorage.UID, DicomUID.MRImageStorage);
			_uids.Add(DicomUID.EnhancedMRImageStorage.UID, DicomUID.EnhancedMRImageStorage);
			_uids.Add(DicomUID.MRSpectroscopyStorage.UID, DicomUID.MRSpectroscopyStorage);
			_uids.Add(DicomUID.RTImageStorage.UID, DicomUID.RTImageStorage);
			_uids.Add(DicomUID.RTDoseStorage.UID, DicomUID.RTDoseStorage);
			_uids.Add(DicomUID.RTStructureSetStorage.UID, DicomUID.RTStructureSetStorage);
			_uids.Add(DicomUID.RTBeamsTreatmentRecordStorage.UID, DicomUID.RTBeamsTreatmentRecordStorage);
			_uids.Add(DicomUID.RTPlanStorage.UID, DicomUID.RTPlanStorage);
			_uids.Add(DicomUID.RTBrachyTreatmentRecordStorage.UID, DicomUID.RTBrachyTreatmentRecordStorage);
			_uids.Add(DicomUID.RTTreatmentSummaryRecordStorage.UID, DicomUID.RTTreatmentSummaryRecordStorage);
			_uids.Add(DicomUID.RTIonPlanStorage.UID, DicomUID.RTIonPlanStorage);
			_uids.Add(DicomUID.RTIonBeamsTreatmentRecordStorage.UID, DicomUID.RTIonBeamsTreatmentRecordStorage);
			_uids.Add(DicomUID.NuclearMedicineImageStorageRETIRED.UID, DicomUID.NuclearMedicineImageStorageRETIRED);
			_uids.Add(DicomUID.UltrasoundImageStorageRETIRED.UID, DicomUID.UltrasoundImageStorageRETIRED);
			_uids.Add(DicomUID.UltrasoundImageStorage.UID, DicomUID.UltrasoundImageStorage);
			_uids.Add(DicomUID.RawDataStorage.UID, DicomUID.RawDataStorage);
			_uids.Add(DicomUID.SpatialRegistrationStorage.UID, DicomUID.SpatialRegistrationStorage);
			_uids.Add(DicomUID.SpatialFiducialsStorage.UID, DicomUID.SpatialFiducialsStorage);
			_uids.Add(DicomUID.DeformableSpatialRegistrationStorage.UID, DicomUID.DeformableSpatialRegistrationStorage);
			_uids.Add(DicomUID.SegmentationStorage.UID, DicomUID.SegmentationStorage);
			_uids.Add(DicomUID.RealWorldValueMappingStorage.UID, DicomUID.RealWorldValueMappingStorage);
			_uids.Add(DicomUID.SecondaryCaptureImageStorage.UID, DicomUID.SecondaryCaptureImageStorage);
			_uids.Add(DicomUID.MultiframeSingleBitSecondaryCaptureImageStorage.UID, DicomUID.MultiframeSingleBitSecondaryCaptureImageStorage);
			_uids.Add(DicomUID.MultiframeGrayscaleByteSecondaryCaptureImageStorage.UID, DicomUID.MultiframeGrayscaleByteSecondaryCaptureImageStorage);
			_uids.Add(DicomUID.MultiframeGrayscaleWordSecondaryCaptureImageStorage.UID, DicomUID.MultiframeGrayscaleWordSecondaryCaptureImageStorage);
			_uids.Add(DicomUID.MultiframeTrueColorSecondaryCaptureImageStorage.UID, DicomUID.MultiframeTrueColorSecondaryCaptureImageStorage);
			_uids.Add(DicomUID.VLImageStorageTrialRETIRED.UID, DicomUID.VLImageStorageTrialRETIRED);
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
			_uids.Add(DicomUID.VLMultiframeImageStorageTrialRETIRED.UID, DicomUID.VLMultiframeImageStorageTrialRETIRED);
			_uids.Add(DicomUID.StandaloneOverlayStorageRETIRED.UID, DicomUID.StandaloneOverlayStorageRETIRED);
			_uids.Add(DicomUID.TextSRStorageTrialRETIRED.UID, DicomUID.TextSRStorageTrialRETIRED);
			_uids.Add(DicomUID.BasicTextSRStorage.UID, DicomUID.BasicTextSRStorage);
			_uids.Add(DicomUID.AudioSRStorageTrialRETIRED.UID, DicomUID.AudioSRStorageTrialRETIRED);
			_uids.Add(DicomUID.EnhancedSRStorage.UID, DicomUID.EnhancedSRStorage);
			_uids.Add(DicomUID.DetailSRStorageTrialRETIRED.UID, DicomUID.DetailSRStorageTrialRETIRED);
			_uids.Add(DicomUID.ComprehensiveSRStorage.UID, DicomUID.ComprehensiveSRStorage);
			_uids.Add(DicomUID.ComprehensiveSRStorageTrialRETIRED.UID, DicomUID.ComprehensiveSRStorageTrialRETIRED);
			_uids.Add(DicomUID.ProcedureLogStorage.UID, DicomUID.ProcedureLogStorage);
			_uids.Add(DicomUID.MammographyCADSRStorage.UID, DicomUID.MammographyCADSRStorage);
			_uids.Add(DicomUID.KeyObjectSelectionDocumentStorage.UID, DicomUID.KeyObjectSelectionDocumentStorage);
			_uids.Add(DicomUID.ChestCADSRStorage.UID, DicomUID.ChestCADSRStorage);
			_uids.Add(DicomUID.XRayRadiationDoseSRStorage.UID, DicomUID.XRayRadiationDoseSRStorage);
			_uids.Add(DicomUID.StandaloneCurveStorageRETIRED.UID, DicomUID.StandaloneCurveStorageRETIRED);
			_uids.Add(DicomUID.WaveformStorageTrialRETIRED.UID, DicomUID.WaveformStorageTrialRETIRED);
			_uids.Add(DicomUID.TwelveLeadECGWaveformStorage.UID, DicomUID.TwelveLeadECGWaveformStorage);
			_uids.Add(DicomUID.GeneralECGWaveformStorage.UID, DicomUID.GeneralECGWaveformStorage);
			_uids.Add(DicomUID.AmbulatoryECGWaveformStorage.UID, DicomUID.AmbulatoryECGWaveformStorage);
			_uids.Add(DicomUID.HemodynamicWaveformStorage.UID, DicomUID.HemodynamicWaveformStorage);
			_uids.Add(DicomUID.CardiacElectrophysiologyWaveformStorage.UID, DicomUID.CardiacElectrophysiologyWaveformStorage);
			_uids.Add(DicomUID.BasicVoiceAudioWaveformStorage.UID, DicomUID.BasicVoiceAudioWaveformStorage);
			_uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelFIND.UID, DicomUID.PatientRootQueryRetrieveInformationModelFIND);
			_uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelMOVE.UID, DicomUID.PatientRootQueryRetrieveInformationModelMOVE);
			_uids.Add(DicomUID.PatientRootQueryRetrieveInformationModelGET.UID, DicomUID.PatientRootQueryRetrieveInformationModelGET);
			_uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelFIND.UID, DicomUID.StudyRootQueryRetrieveInformationModelFIND);
			_uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelMOVE.UID, DicomUID.StudyRootQueryRetrieveInformationModelMOVE);
			_uids.Add(DicomUID.StudyRootQueryRetrieveInformationModelGET.UID, DicomUID.StudyRootQueryRetrieveInformationModelGET);
			_uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelFINDRETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelFINDRETIRED);
			_uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelMOVERETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelMOVERETIRED);
			_uids.Add(DicomUID.PatientStudyOnlyQueryRetrieveInformationModelGETRETIRED.UID, DicomUID.PatientStudyOnlyQueryRetrieveInformationModelGETRETIRED);
			_uids.Add(DicomUID.ModalityWorklistInformationModelFIND.UID, DicomUID.ModalityWorklistInformationModelFIND);
			_uids.Add(DicomUID.GeneralPurposeWorklistManagementMetaSOPClass.UID, DicomUID.GeneralPurposeWorklistManagementMetaSOPClass);
			_uids.Add(DicomUID.GeneralPurposeWorklistInformationModelFIND.UID, DicomUID.GeneralPurposeWorklistInformationModelFIND);
			_uids.Add(DicomUID.GeneralPurposeScheduledProcedureStepSOPClass.UID, DicomUID.GeneralPurposeScheduledProcedureStepSOPClass);
			_uids.Add(DicomUID.GeneralPurposePerformedProcedureStepSOPClass.UID, DicomUID.GeneralPurposePerformedProcedureStepSOPClass);
			_uids.Add(DicomUID.InstanceAvailabilityNotificationSOPClass.UID, DicomUID.InstanceAvailabilityNotificationSOPClass);
			_uids.Add(DicomUID.RTBeamsDeliveryInstructionStorageSupplement74FrozenDraft.UID, DicomUID.RTBeamsDeliveryInstructionStorageSupplement74FrozenDraft);
			_uids.Add(DicomUID.RTConventionalMachineVerificationSupplement74FrozenDraft.UID, DicomUID.RTConventionalMachineVerificationSupplement74FrozenDraft);
			_uids.Add(DicomUID.RTIonMachineVerificationSupplement74FrozenDraft.UID, DicomUID.RTIonMachineVerificationSupplement74FrozenDraft);
			_uids.Add(DicomUID.UnifiedWorklistAndProcedureStepSOPClass.UID, DicomUID.UnifiedWorklistAndProcedureStepSOPClass);
			_uids.Add(DicomUID.UnifiedProcedureStepPushSOPClass.UID, DicomUID.UnifiedProcedureStepPushSOPClass);
			_uids.Add(DicomUID.UnifiedProcedureStepWatchSOPClass.UID, DicomUID.UnifiedProcedureStepWatchSOPClass);
			_uids.Add(DicomUID.UnifiedProcedureStepPullSOPClass.UID, DicomUID.UnifiedProcedureStepPullSOPClass);
			_uids.Add(DicomUID.UnifiedProcedureStepEventSOPClass.UID, DicomUID.UnifiedProcedureStepEventSOPClass);
			_uids.Add(DicomUID.UnifiedWorklistAndProcedureStepSOPInstance.UID, DicomUID.UnifiedWorklistAndProcedureStepSOPInstance);
			_uids.Add(DicomUID.GeneralRelevantPatientInformationQuery.UID, DicomUID.GeneralRelevantPatientInformationQuery);
			_uids.Add(DicomUID.BreastImagingRelevantPatientInformationQuery.UID, DicomUID.BreastImagingRelevantPatientInformationQuery);
			_uids.Add(DicomUID.CardiacRelevantPatientInformationQuery.UID, DicomUID.CardiacRelevantPatientInformationQuery);
			_uids.Add(DicomUID.HangingProtocolStorage.UID, DicomUID.HangingProtocolStorage);
			_uids.Add(DicomUID.HangingProtocolInformationModelFIND.UID, DicomUID.HangingProtocolInformationModelFIND);
			_uids.Add(DicomUID.HangingProtocolInformationModelMOVE.UID, DicomUID.HangingProtocolInformationModelMOVE);
			_uids.Add(DicomUID.ProductCharacteristicsQuerySOPClass.UID, DicomUID.ProductCharacteristicsQuerySOPClass);
			_uids.Add(DicomUID.SubstanceApprovalQuerySOPClass.UID, DicomUID.SubstanceApprovalQuerySOPClass);
		}

		/// <summary>SOP Class: Verification SOP Class [PS 3.4]</summary>
		public readonly static DicomUID VerificationSOPClass = new DicomUID("1.2.840.10008.1.1", "Verification SOP Class", DicomUidType.SOPClass);

		/// <summary>Transfer Syntax: Implicit VR Little Endian: Default Transfer Syntax for DICOM [PS 3.5]</summary>
		public readonly static DicomUID ImplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2", "Implicit VR Little Endian: Default Transfer Syntax for DICOM", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: Explicit VR Little Endian [PS 3.5]</summary>
		public readonly static DicomUID ExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1", "Explicit VR Little Endian", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: Deflated Explicit VR Little Endian [PS 3.5]</summary>
		public readonly static DicomUID DeflatedExplicitVRLittleEndian = new DicomUID("1.2.840.10008.1.2.1.99", "Deflated Explicit VR Little Endian", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: Explicit VR Big Endian [PS 3.5]</summary>
		public readonly static DicomUID ExplicitVRBigEndian = new DicomUID("1.2.840.10008.1.2.2", "Explicit VR Big Endian", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: MPEG2 Main Profile @ Main Level [PS 3.5]</summary>
		public readonly static DicomUID MPEG2MainProfileMainLevel = new DicomUID("1.2.840.10008.1.2.4.100", "MPEG2 Main Profile @ Main Level", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression [PS 3.5]</summary>
		public readonly static DicomUID JPEGBaselineProcess1 = new DicomUID("1.2.840.10008.1.2.4.50", "JPEG Baseline (Process 1): Default Transfer Syntax for Lossy JPEG 8 Bit Image Compression", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Extended (Process 2 &amp; 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only) [PS 3.5]</summary>
		public readonly static DicomUID JPEGExtendedProcess2_4 = new DicomUID("1.2.840.10008.1.2.4.51", "JPEG Extended (Process 2 & 4): Default Transfer Syntax for Lossy JPEG 12 Bit Image Compression (Process 4 only)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Extended (Process 3 &amp; 5) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGExtendedProcess3_5RETIRED = new DicomUID("1.2.840.10008.1.2.4.52", "JPEG Extended (Process 3 & 5)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Non-Hierarchical (Process 6 &amp; 8) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionNonHierarchicalProcess6_8RETIRED = new DicomUID("1.2.840.10008.1.2.4.53", "JPEG Spectral Selection, Non-Hierarchical (Process 6 & 8)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Non-Hierarchical (Process 7 &amp; 9) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionNonHierarchicalProcess7_9RETIRED = new DicomUID("1.2.840.10008.1.2.4.54", "JPEG Spectral Selection, Non-Hierarchical (Process 7 & 9)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Full Progression, Non-Hierarchical (Process 10 &amp; 12) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionNonHierarchicalProcess10_12RETIRED = new DicomUID("1.2.840.10008.1.2.4.55", "JPEG Full Progression, Non-Hierarchical (Process 10 & 12)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Full Progression, Non-Hierarchical (Process 11 &amp; 13) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionNonHierarchicalProcess11_13RETIRED = new DicomUID("1.2.840.10008.1.2.4.56", "JPEG Full Progression, Non-Hierarchical (Process 11 & 13)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical (Process 14) [PS 3.5]</summary>
		public readonly static DicomUID JPEGLosslessNonHierarchicalProcess14 = new DicomUID("1.2.840.10008.1.2.4.57", "JPEG Lossless, Non-Hierarchical (Process 14)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical (Process 15) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGLosslessNonHierarchicalProcess15RETIRED = new DicomUID("1.2.840.10008.1.2.4.58", "JPEG Lossless, Non-Hierarchical (Process 15)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Extended, Hierarchical (Process 16 &amp; 18) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGExtendedHierarchicalProcess16_18RETIRED = new DicomUID("1.2.840.10008.1.2.4.59", "JPEG Extended, Hierarchical (Process 16 & 18)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Extended, Hierarchical (Process 17 &amp; 19) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGExtendedHierarchicalProcess17_19RETIRED = new DicomUID("1.2.840.10008.1.2.4.60", "JPEG Extended, Hierarchical (Process 17 & 19)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Hierarchical (Process 20 &amp; 22) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionHierarchicalProcess20_22RETIRED = new DicomUID("1.2.840.10008.1.2.4.61", "JPEG Spectral Selection, Hierarchical (Process 20 & 22)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Spectral Selection, Hierarchical (Process 21 &amp; 23) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGSpectralSelectionHierarchicalProcess21_23RETIRED = new DicomUID("1.2.840.10008.1.2.4.62", "JPEG Spectral Selection, Hierarchical (Process 21 & 23)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Full Progression, Hierarchical (Process 24 &amp; 26) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionHierarchicalProcess24_26RETIRED = new DicomUID("1.2.840.10008.1.2.4.63", "JPEG Full Progression, Hierarchical (Process 24 & 26)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Full Progression, Hierarchical (Process 25 &amp; 27) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGFullProgressionHierarchicalProcess25_27RETIRED = new DicomUID("1.2.840.10008.1.2.4.64", "JPEG Full Progression, Hierarchical (Process 25 & 27)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Lossless, Hierarchical (Process 28) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGLosslessHierarchicalProcess28RETIRED = new DicomUID("1.2.840.10008.1.2.4.65", "JPEG Lossless, Hierarchical (Process 28)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Lossless, Hierarchical (Process 29) [PS 3.5] (Retired)</summary>
		public readonly static DicomUID JPEGLosslessHierarchicalProcess29RETIRED = new DicomUID("1.2.840.10008.1.2.4.66", "JPEG Lossless, Hierarchical (Process 29)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1]): Default Transfer Syntax for Lossless JPEG Image Compression [PS 3.5]</summary>
		public readonly static DicomUID JPEGLosslessProcess14SV1 = new DicomUID("1.2.840.10008.1.2.4.70", "JPEG Lossless, Non-Hierarchical, First-Order Prediction (Process 14 [Selection Value 1])", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG-LS Lossless Image Compression [PS 3.5]</summary>
		public readonly static DicomUID JPEGLSLosslessImageCompression = new DicomUID("1.2.840.10008.1.2.4.80", "JPEG-LS Lossless Image Compression", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG-LS Lossy (Near-Lossless) Image Compression [PS 3.5]</summary>
		public readonly static DicomUID JPEGLSLossyNearLosslessImageCompression = new DicomUID("1.2.840.10008.1.2.4.81", "JPEG-LS Lossy (Near-Lossless) Image Compression", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG 2000 Image Compression (Lossless Only) [PS 3.5]</summary>
		public readonly static DicomUID JPEG2000ImageCompressionLosslessOnly = new DicomUID("1.2.840.10008.1.2.4.90", "JPEG 2000 Image Compression (Lossless Only)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG 2000 Image Compression [PS 3.5]</summary>
		public readonly static DicomUID JPEG2000ImageCompression = new DicomUID("1.2.840.10008.1.2.4.91", "JPEG 2000 Image Compression", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only) [PS 3.5]</summary>
		public readonly static DicomUID JPEG2000Part2MulticomponentImageCompressionLosslessOnly = new DicomUID("1.2.840.10008.1.2.4.92", "JPEG 2000 Part 2 Multi-component Image Compression (Lossless Only)", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPEG 2000 Part 2 Multi-component Image Compression [PS 3.5]</summary>
		public readonly static DicomUID JPEG2000Part2MulticomponentImageCompression = new DicomUID("1.2.840.10008.1.2.4.93", "JPEG 2000 Part 2 Multi-component Image Compression", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPIP Referenced [PS 3.5]</summary>
		public readonly static DicomUID JPIPReferenced = new DicomUID("1.2.840.10008.1.2.4.94", "JPIP Referenced", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: JPIP Referenced Deflate [PS 3.5]</summary>
		public readonly static DicomUID JPIPReferencedDeflate = new DicomUID("1.2.840.10008.1.2.4.95", "JPIP Referenced Deflate", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: RLE Lossless [PS 3.5]</summary>
		public readonly static DicomUID RLELossless = new DicomUID("1.2.840.10008.1.2.5", "RLE Lossless", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: RFC 2557 MIME encapsulation [PS 3.10]</summary>
		public readonly static DicomUID RFC2557MIMEEncapsulation = new DicomUID("1.2.840.10008.1.2.6.1", "RFC 2557 MIME encapsulation", DicomUidType.TransferSyntax);

		/// <summary>Transfer Syntax: XML Encoding [PS 3.10]</summary>
		public readonly static DicomUID XMLEncoding = new DicomUID("1.2.840.10008.1.2.6.2", "XML Encoding", DicomUidType.TransferSyntax);

		/// <summary>SOP Class: Storage Commitment Push Model SOP Class [PS 3.4]</summary>
		public readonly static DicomUID StorageCommitmentPushModelSOPClass = new DicomUID("1.2.840.10008.1.20.1", "Storage Commitment Push Model SOP Class", DicomUidType.SOPClass);

		/// <summary>Well-known SOP Instance: Storage Commitment Push Model SOP Instance [PS 3.4]</summary>
		public readonly static DicomUID StorageCommitmentPushModelSOPInstance = new DicomUID("1.2.840.10008.1.20.1.1", "Storage Commitment Push Model SOP Instance", DicomUidType.SOPInstance);

		/// <summary>SOP Class: Storage Commitment Pull Model SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StorageCommitmentPullModelSOPClassRETIRED = new DicomUID("1.2.840.10008.1.20.2", "Storage Commitment Pull Model SOP Class", DicomUidType.SOPClass);

		/// <summary>Well-known SOP Instance: Storage Commitment Pull Model SOP Instance [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StorageCommitmentPullModelSOPInstanceRETIRED = new DicomUID("1.2.840.10008.1.20.2.1", "Storage Commitment Pull Model SOP Instance", DicomUidType.SOPInstance);

		/// <summary>SOP Class: Media Storage Directory Storage [PS 3.4]</summary>
		public readonly static DicomUID MediaStorageDirectoryStorage = new DicomUID("1.2.840.10008.1.3.10", "Media Storage Directory Storage", DicomUidType.SOPClass);

		/// <summary>Well-known frame of reference: Talairach Brain Atlas Frame of Reference</summary>
		public readonly static DicomUID TalairachBrainAtlasFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.1", "Talairach Brain Atlas Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 GRAY Frame of Reference</summary>
		public readonly static DicomUID SPM2GRAYFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.10", "SPM2 GRAY Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 WHITE Frame of Reference</summary>
		public readonly static DicomUID SPM2WHITEFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.11", "SPM2 WHITE Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 CSF Frame of Reference</summary>
		public readonly static DicomUID SPM2CSFFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.12", "SPM2 CSF Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 BRAINMASK Frame of Reference</summary>
		public readonly static DicomUID SPM2BRAINMASKFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.13", "SPM2 BRAINMASK Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 AVG305T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG305T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.14", "SPM2 AVG305T1 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 AVG152T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG152T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.15", "SPM2 AVG152T1 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 AVG152T2 Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG152T2FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.16", "SPM2 AVG152T2 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 AVG152PD Frame of Reference</summary>
		public readonly static DicomUID SPM2AVG152PDFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.17", "SPM2 AVG152PD Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 SINGLESUBJT1 Frame of Reference</summary>
		public readonly static DicomUID SPM2SINGLESUBJT1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.18", "SPM2 SINGLESUBJT1 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.2", "SPM2 T1 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 T2 Frame of Reference</summary>
		public readonly static DicomUID SPM2T2FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.3", "SPM2 T2 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 PD Frame of Reference</summary>
		public readonly static DicomUID SPM2PDFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.4", "SPM2 PD Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 EPI Frame of Reference</summary>
		public readonly static DicomUID SPM2EPIFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.5", "SPM2 EPI Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 FIL T1 Frame of Reference</summary>
		public readonly static DicomUID SPM2FILT1FrameOfReference = new DicomUID("1.2.840.10008.1.4.1.6", "SPM2 FIL T1 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 PET Frame of Reference</summary>
		public readonly static DicomUID SPM2PETFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.7", "SPM2 PET Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 TRANSM Frame of Reference</summary>
		public readonly static DicomUID SPM2TRANSMFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.8", "SPM2 TRANSM Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: SPM2 SPECT Frame of Reference</summary>
		public readonly static DicomUID SPM2SPECTFrameOfReference = new DicomUID("1.2.840.10008.1.4.1.9", "SPM2 SPECT Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: ICBM 452 T1 Frame of Reference</summary>
		public readonly static DicomUID ICBM452T1FrameOfReference = new DicomUID("1.2.840.10008.1.4.2.1", "ICBM 452 T1 Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>Well-known frame of reference: ICBM Single Subject MRI Frame of Reference</summary>
		public readonly static DicomUID ICBMSingleSubjectMRIFrameOfReference = new DicomUID("1.2.840.10008.1.4.2.2", "ICBM Single Subject MRI Frame of Reference", DicomUidType.FrameOfReference);

		/// <summary>SOP Class: Procedural Event Logging SOP Class [PS 3.4]</summary>
		public readonly static DicomUID ProceduralEventLoggingSOPClass = new DicomUID("1.2.840.10008.1.40", "Procedural Event Logging SOP Class", DicomUidType.SOPClass);

		/// <summary>Well-known SOP Instance: Procedural Event Logging SOP Instance [PS 3.4]</summary>
		public readonly static DicomUID ProceduralEventLoggingSOPInstance = new DicomUID("1.2.840.10008.1.40.1", "Procedural Event Logging SOP Instance", DicomUidType.SOPInstance);

		/// <summary>SOP Class: Substance Administration Logging SOP Class [PS 3.4]</summary>
		public readonly static DicomUID SubstanceAdministrationLoggingSOPClass = new DicomUID("1.2.840.10008.1.42", "Substance Administration Logging SOP Class", DicomUidType.SOPClass);

		/// <summary>Well-known SOP Instance: Substance Administration Logging SOP Instance [PS 3.4]</summary>
		public readonly static DicomUID SubstanceAdministrationLoggingSOPInstance = new DicomUID("1.2.840.10008.1.42.1", "Substance Administration Logging SOP Instance", DicomUidType.SOPInstance);

		/// <summary>SOP Class: Basic Study Content Notification SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID BasicStudyContentNotificationSOPClassRETIRED = new DicomUID("1.2.840.10008.1.9", "Basic Study Content Notification SOP Class", DicomUidType.SOPClass);

		/// <summary>LDAP OID: dicomDeviceName [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomDeviceName = new DicomUID("1.2.840.10008.15.0.3.1", "dicomDeviceName", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomAssociationInitiator [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomAssociationInitiator = new DicomUID("1.2.840.10008.15.0.3.10", "dicomAssociationInitiator", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomAssociationAcceptor [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomAssociationAcceptor = new DicomUID("1.2.840.10008.15.0.3.11", "dicomAssociationAcceptor", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomHostname [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomHostname = new DicomUID("1.2.840.10008.15.0.3.12", "dicomHostname", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomPort [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomPort = new DicomUID("1.2.840.10008.15.0.3.13", "dicomPort", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomSOPClass [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomSOPClass = new DicomUID("1.2.840.10008.15.0.3.14", "dicomSOPClass", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomTransferRole [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomTransferRole = new DicomUID("1.2.840.10008.15.0.3.15", "dicomTransferRole", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomTransferSyntax [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomTransferSyntax = new DicomUID("1.2.840.10008.15.0.3.16", "dicomTransferSyntax", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomPrimaryDeviceType [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomPrimaryDeviceType = new DicomUID("1.2.840.10008.15.0.3.17", "dicomPrimaryDeviceType", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomRelatedDeviceReference [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomRelatedDeviceReference = new DicomUID("1.2.840.10008.15.0.3.18", "dicomRelatedDeviceReference", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomPreferredCalledAETitle [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomPreferredCalledAETitle = new DicomUID("1.2.840.10008.15.0.3.19", "dicomPreferredCalledAETitle", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomDescription [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomDescription = new DicomUID("1.2.840.10008.15.0.3.2", "dicomDescription", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomTLSCyphersuite [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomTLSCyphersuite = new DicomUID("1.2.840.10008.15.0.3.20", "dicomTLSCyphersuite", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomAuthorizedNodeCertificateReference [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomAuthorizedNodeCertificateReference = new DicomUID("1.2.840.10008.15.0.3.21", "dicomAuthorizedNodeCertificateReference", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomThisNodeCertificateReference [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomThisNodeCertificateReference = new DicomUID("1.2.840.10008.15.0.3.22", "dicomThisNodeCertificateReference", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomInstalled [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomInstalled = new DicomUID("1.2.840.10008.15.0.3.23", "dicomInstalled", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomStationName [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomStationName = new DicomUID("1.2.840.10008.15.0.3.24", "dicomStationName", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomDeviceSerialNumber [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomDeviceSerialNumber = new DicomUID("1.2.840.10008.15.0.3.25", "dicomDeviceSerialNumber", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomInstitutionName [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomInstitutionName = new DicomUID("1.2.840.10008.15.0.3.26", "dicomInstitutionName", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomInstitutionAddress [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomInstitutionAddress = new DicomUID("1.2.840.10008.15.0.3.27", "dicomInstitutionAddress", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomInstitutionDepartmentName [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomInstitutionDepartmentName = new DicomUID("1.2.840.10008.15.0.3.28", "dicomInstitutionDepartmentName", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomIssuerOfPatientID [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomIssuerOfPatientID = new DicomUID("1.2.840.10008.15.0.3.29", "dicomIssuerOfPatientID", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomManufacturer [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomManufacturer = new DicomUID("1.2.840.10008.15.0.3.3", "dicomManufacturer", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomPreferredCallingAETitle [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomPreferredCallingAETitle = new DicomUID("1.2.840.10008.15.0.3.30", "dicomPreferredCallingAETitle", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomSupportedCharacterSet [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomSupportedCharacterSet = new DicomUID("1.2.840.10008.15.0.3.31", "dicomSupportedCharacterSet", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomManufacturerModelName [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomManufacturerModelName = new DicomUID("1.2.840.10008.15.0.3.4", "dicomManufacturerModelName", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomSoftwareVersion [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomSoftwareVersion = new DicomUID("1.2.840.10008.15.0.3.5", "dicomSoftwareVersion", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomVendorData [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomVendorData = new DicomUID("1.2.840.10008.15.0.3.6", "dicomVendorData", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomAETitle [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomAETitle = new DicomUID("1.2.840.10008.15.0.3.7", "dicomAETitle", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomNetworkConnectionReference [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomNetworkConnectionReference = new DicomUID("1.2.840.10008.15.0.3.8", "dicomNetworkConnectionReference", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomApplicationCluster [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomApplicationCluster = new DicomUID("1.2.840.10008.15.0.3.9", "dicomApplicationCluster", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomConfigurationRoot [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomConfigurationRoot = new DicomUID("1.2.840.10008.15.0.4.1", "dicomConfigurationRoot", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomDevicesRoot [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomDevicesRoot = new DicomUID("1.2.840.10008.15.0.4.2", "dicomDevicesRoot", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomUniqueAETitlesRegistryRoot [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomUniqueAETitlesRegistryRoot = new DicomUID("1.2.840.10008.15.0.4.3", "dicomUniqueAETitlesRegistryRoot", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomDevice [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomDevice = new DicomUID("1.2.840.10008.15.0.4.4", "dicomDevice", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomNetworkAE [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomNetworkAE = new DicomUID("1.2.840.10008.15.0.4.5", "dicomNetworkAE", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomNetworkConnection [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomNetworkConnection = new DicomUID("1.2.840.10008.15.0.4.6", "dicomNetworkConnection", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomUniqueAETitle [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomUniqueAETitle = new DicomUID("1.2.840.10008.15.0.4.7", "dicomUniqueAETitle", DicomUidType.LDAP);

		/// <summary>LDAP OID: dicomTransferCapability [PS 3.15]</summary>
		public readonly static DicomUID LDAPDicomTransferCapability = new DicomUID("1.2.840.10008.15.0.4.8", "dicomTransferCapability", DicomUidType.LDAP);

		/// <summary>Coding Scheme: DICOM Controlled Terminology [PS 3.16]</summary>
		public readonly static DicomUID DICOMControlledTerminology = new DicomUID("1.2.840.10008.2.16.4", "DICOM Controlled Terminology", DicomUidType.CodingScheme);

		/// <summary>DICOM UIDs as a Coding Scheme: DICOM UID Registry [PS 3.6]</summary>
		public readonly static DicomUID DICOMUIDRegistry = new DicomUID("1.2.840.10008.2.6.1", "DICOM UID Registry", DicomUidType.CodingScheme);

		/// <summary>Application Context Name: DICOM Application Context Name [PS 3.7]</summary>
		public readonly static DicomUID DICOMApplicationContextName = new DicomUID("1.2.840.10008.3.1.1.1", "DICOM Application Context Name", DicomUidType.ApplicationContextName);

		/// <summary>SOP Class: Detached Patient Management SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedPatientManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.1", "Detached Patient Management SOP Class", DicomUidType.SOPClass);

		/// <summary>Meta SOP Class: Detached Patient Management Meta SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedPatientManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.1.4", "Detached Patient Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>SOP Class: Detached Visit Management SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedVisitManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.2.1", "Detached Visit Management SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Detached Study Management SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedStudyManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.1", "Detached Study Management SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Study Component Management SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StudyComponentManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.3.2", "Study Component Management SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Modality Performed Procedure Step SOP Class [PS 3.4]</summary>
		public readonly static DicomUID ModalityPerformedProcedureStepSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.3", "Modality Performed Procedure Step SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Modality Performed Procedure Step Retrieve SOP Class [PS 3.4]</summary>
		public readonly static DicomUID ModalityPerformedProcedureStepRetrieveSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.4", "Modality Performed Procedure Step Retrieve SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Modality Performed Procedure Step Notification SOP Class [PS 3.4]</summary>
		public readonly static DicomUID ModalityPerformedProcedureStepNotificationSOPClass = new DicomUID("1.2.840.10008.3.1.2.3.5", "Modality Performed Procedure Step Notification SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Detached Results Management SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedResultsManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.1", "Detached Results Management SOP Class", DicomUidType.SOPClass);

		/// <summary>Meta SOP Class: Detached Results Management Meta SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedResultsManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.4", "Detached Results Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>Meta SOP Class: Detached Study Management Meta SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedStudyManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.5.5", "Detached Study Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>SOP Class: Detached Interpretation Management SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetachedInterpretationManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.3.1.2.6.1", "Detached Interpretation Management SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Basic Film Session SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BasicFilmSessionSOPClass = new DicomUID("1.2.840.10008.5.1.1.1", "Basic Film Session SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Print Job SOP Class [PS 3.4]</summary>
		public readonly static DicomUID PrintJobSOPClass = new DicomUID("1.2.840.10008.5.1.1.14", "Print Job SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Basic Annotation Box SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BasicAnnotationBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.15", "Basic Annotation Box SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Printer SOP Class [PS 3.4]</summary>
		public readonly static DicomUID PrinterSOPClass = new DicomUID("1.2.840.10008.5.1.1.16", "Printer SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Printer Configuration Retrieval SOP Class [PS 3.4]</summary>
		public readonly static DicomUID PrinterConfigurationRetrievalSOPClass = new DicomUID("1.2.840.10008.5.1.1.16.376", "Printer Configuration Retrieval SOP Class", DicomUidType.SOPClass);

		/// <summary>Well-known Printer SOP Instance: Printer SOP Instance [PS 3.4]</summary>
		public readonly static DicomUID PrinterSOPInstance = new DicomUID("1.2.840.10008.5.1.1.17", "Printer SOP Instance", DicomUidType.SOPInstance);

		/// <summary>Well-known Printer SOP Instance: Printer Configuration Retrieval SOP Instance [PS 3.4]</summary>
		public readonly static DicomUID PrinterConfigurationRetrievalSOPInstance = new DicomUID("1.2.840.10008.5.1.1.17.376", "Printer Configuration Retrieval SOP Instance", DicomUidType.SOPInstance);

		/// <summary>Meta SOP Class: Basic Color Print Management Meta SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BasicColorPrintManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.1.18", "Basic Color Print Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>Meta SOP Class: Referenced Color Print Management Meta SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID ReferencedColorPrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.18.1", "Referenced Color Print Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>SOP Class: Basic Film Box SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BasicFilmBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.2", "Basic Film Box SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: VOI LUT Box SOP Class [PS 3.4]</summary>
		public readonly static DicomUID VOILUTBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.22", "VOI LUT Box SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Presentation LUT SOP Class [PS 3.4]</summary>
		public readonly static DicomUID PresentationLUTSOPClass = new DicomUID("1.2.840.10008.5.1.1.23", "Presentation LUT SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Image Overlay Box SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID ImageOverlayBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.24", "Image Overlay Box SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Basic Print Image Overlay Box SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID BasicPrintImageOverlayBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.24.1", "Basic Print Image Overlay Box SOP Class", DicomUidType.SOPClass);

		/// <summary>Well-known Print Queue SOP Instance: Print Queue SOP Instance [PS 3.4] (Retired)</summary>
		public readonly static DicomUID PrintQueueSOPInstanceRETIRED = new DicomUID("1.2.840.10008.5.1.1.25", "Print Queue SOP Instance", DicomUidType.SOPInstance);

		/// <summary>SOP Class: Print Queue Management SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID PrintQueueManagementSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.26", "Print Queue Management SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Stored Print Storage SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StoredPrintStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.27", "Stored Print Storage SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Hardcopy Grayscale Image Storage SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID HardcopyGrayscaleImageStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.29", "Hardcopy Grayscale Image Storage SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Hardcopy Color Image Storage SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID HardcopyColorImageStorageSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.30", "Hardcopy Color Image Storage SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Pull Print Request SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID PullPrintRequestSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.31", "Pull Print Request SOP Class", DicomUidType.SOPClass);

		/// <summary>Meta SOP Class: Pull Stored Print Management Meta SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID PullStoredPrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.32", "Pull Stored Print Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>SOP Class: Media Creation Management SOP Class UID [PS3.4]</summary>
		public readonly static DicomUID MediaCreationManagementSOPClassUID = new DicomUID("1.2.840.10008.5.1.1.33", "Media Creation Management SOP Class UID", DicomUidType.SOPClass);

		/// <summary>SOP Class: Basic Grayscale Image Box SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BasicGrayscaleImageBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.4", "Basic Grayscale Image Box SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Basic Color Image Box SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BasicColorImageBoxSOPClass = new DicomUID("1.2.840.10008.5.1.1.4.1", "Basic Color Image Box SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Referenced Image Box SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID ReferencedImageBoxSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.4.2", "Referenced Image Box SOP Class", DicomUidType.SOPClass);

		/// <summary>Meta SOP Class: Basic Grayscale Print Management Meta SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BasicGrayscalePrintManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.1.9", "Basic Grayscale Print Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>Meta SOP Class: Referenced Grayscale Print Management Meta SOP Class [PS 3.4] (Retired)</summary>
		public readonly static DicomUID ReferencedGrayscalePrintManagementMetaSOPClassRETIRED = new DicomUID("1.2.840.10008.5.1.1.9.1", "Referenced Grayscale Print Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>SOP Class: Computed Radiography Image Storage [PS 3.4]</summary>
		public readonly static DicomUID ComputedRadiographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.1", "Computed Radiography Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Digital X-Ray Image Storage – For Presentation [PS 3.4]</summary>
		public readonly static DicomUID DigitalXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.1", "Digital X-Ray Image Storage – For Presentation", DicomUidType.SOPClass);

		/// <summary>SOP Class: Digital X-Ray Image Storage – For Processing [PS 3.4]</summary>
		public readonly static DicomUID DigitalXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.1.1", "Digital X-Ray Image Storage – For Processing", DicomUidType.SOPClass);

		/// <summary>SOP Class: Digital Mammography X-Ray Image Storage – For Presentation [PS 3.4]</summary>
		public readonly static DicomUID DigitalMammographyXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.2", "Digital Mammography X-Ray Image Storage – For Presentation", DicomUidType.SOPClass);

		/// <summary>SOP Class: Digital Mammography X-Ray Image Storage – For Processing [PS 3.4]</summary>
		public readonly static DicomUID DigitalMammographyXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.2.1", "Digital Mammography X-Ray Image Storage – For Processing", DicomUidType.SOPClass);

		/// <summary>SOP Class: Digital Intra-oral X-Ray Image Storage – For Presentation [PS 3.4]</summary>
		public readonly static DicomUID DigitalIntraoralXRayImageStorageForPresentation = new DicomUID("1.2.840.10008.5.1.4.1.1.1.3", "Digital Intra-oral X-Ray Image Storage – For Presentation", DicomUidType.SOPClass);

		/// <summary>SOP Class: Digital Intra-oral X-Ray Image Storage – For Processing [PS 3.4]</summary>
		public readonly static DicomUID DigitalIntraoralXRayImageStorageForProcessing = new DicomUID("1.2.840.10008.5.1.4.1.1.1.3.1", "Digital Intra-oral X-Ray Image Storage – For Processing", DicomUidType.SOPClass);

		/// <summary>SOP Class: Standalone Modality LUT Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StandaloneModalityLUTStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.10", "Standalone Modality LUT Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Encapsulated PDF Storage [PS 3.4]</summary>
		public readonly static DicomUID EncapsulatedPDFStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.1", "Encapsulated PDF Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Encapsulated CDA Storage [PS 3.4]</summary>
		public readonly static DicomUID EncapsulatedCDAStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.104.2", "Encapsulated CDA Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Standalone VOI LUT Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StandaloneVOILUTStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.11", "Standalone VOI LUT Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Grayscale Softcopy Presentation State Storage SOP Class [PS 3.4]</summary>
		public readonly static DicomUID GrayscaleSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.1", "Grayscale Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Color Softcopy Presentation State Storage SOP Class [PS 3.4]</summary>
		public readonly static DicomUID ColorSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.2", "Color Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Pseudo-Color Softcopy Presentation State Storage SOP Class [PS 3.4]</summary>
		public readonly static DicomUID PseudoColorSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.3", "Pseudo-Color Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Blending Softcopy Presentation State Storage SOP Class [PS 3.4]</summary>
		public readonly static DicomUID BlendingSoftcopyPresentationStateStorageSOPClass = new DicomUID("1.2.840.10008.5.1.4.1.1.11.4", "Blending Softcopy Presentation State Storage SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: X-Ray Angiographic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID XRayAngiographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.1", "X-Ray Angiographic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Enhanced XA Image Storage [PS 3.4]</summary>
		public readonly static DicomUID EnhancedXAImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.1.1", "Enhanced XA Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: X-Ray Radiofluoroscopic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID XRayRadiofluoroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.2", "X-Ray Radiofluoroscopic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Enhanced XRF Image Storage [PS 3.4]</summary>
		public readonly static DicomUID EnhancedXRFImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.12.2.1", "Enhanced XRF Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: X-Ray Angiographic Bi-Plane Image Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID XRayAngiographicBiPlaneImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.12.3", "X-Ray Angiographic Bi-Plane Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Positron Emission Tomography Image Storage [PS 3.4]</summary>
		public readonly static DicomUID PositronEmissionTomographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.128", "Positron Emission Tomography Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Standalone PET Curve Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StandalonePETCurveStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.129", "Standalone PET Curve Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: X-Ray 3D Angiographic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID XRay3DAngiographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.1", "X-Ray 3D Angiographic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: X-Ray 3D Craniofacial Image Storage [PS 3.4]</summary>
		public readonly static DicomUID XRay3DCraniofacialImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.13.1.2", "X-Ray 3D Craniofacial Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: CT Image Storage [PS 3.4]</summary>
		public readonly static DicomUID CTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.2", "CT Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Enhanced CT Image Storage [PS 3.4]</summary>
		public readonly static DicomUID EnhancedCTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.2.1", "Enhanced CT Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Nuclear Medicine Image Storage [PS 3.4]</summary>
		public readonly static DicomUID NuclearMedicineImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.20", "Nuclear Medicine Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ultrasound Multi-frame Image Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID UltrasoundMultiframeImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.3", "Ultrasound Multi-frame Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ultrasound Multi-frame Image Storage [PS 3.4]</summary>
		public readonly static DicomUID UltrasoundMultiframeImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.3.1", "Ultrasound Multi-frame Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: MR Image Storage [PS 3.4]</summary>
		public readonly static DicomUID MRImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4", "MR Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Enhanced MR Image Storage [PS 3.4]</summary>
		public readonly static DicomUID EnhancedMRImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.1", "Enhanced MR Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: MR Spectroscopy Storage [PS 3.4]</summary>
		public readonly static DicomUID MRSpectroscopyStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.4.2", "MR Spectroscopy Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Image Storage [PS 3.4]</summary>
		public readonly static DicomUID RTImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.1", "RT Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Dose Storage [PS 3.4]</summary>
		public readonly static DicomUID RTDoseStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.2", "RT Dose Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Structure Set Storage [PS 3.4]</summary>
		public readonly static DicomUID RTStructureSetStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.3", "RT Structure Set Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Beams Treatment Record Storage [PS 3.4]</summary>
		public readonly static DicomUID RTBeamsTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.4", "RT Beams Treatment Record Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Plan Storage [PS 3.4]</summary>
		public readonly static DicomUID RTPlanStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.5", "RT Plan Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Brachy Treatment Record Storage [PS 3.4]</summary>
		public readonly static DicomUID RTBrachyTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.6", "RT Brachy Treatment Record Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Treatment Summary Record Storage [PS 3.4]</summary>
		public readonly static DicomUID RTTreatmentSummaryRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.7", "RT Treatment Summary Record Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Ion Plan Storage [PS 3.4]</summary>
		public readonly static DicomUID RTIonPlanStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.8", "RT Ion Plan Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Ion Beams Treatment Record Storage [PS 3.4]</summary>
		public readonly static DicomUID RTIonBeamsTreatmentRecordStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.481.9", "RT Ion Beams Treatment Record Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Nuclear Medicine Image Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID NuclearMedicineImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.5", "Nuclear Medicine Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ultrasound Image Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID UltrasoundImageStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.6", "Ultrasound Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ultrasound Image Storage [PS 3.4]</summary>
		public readonly static DicomUID UltrasoundImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.6.1", "Ultrasound Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Raw Data Storage [PS 3.4]</summary>
		public readonly static DicomUID RawDataStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66", "Raw Data Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Spatial Registration Storage [PS 3.4]</summary>
		public readonly static DicomUID SpatialRegistrationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.1", "Spatial Registration Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Spatial Fiducials Storage [PS 3.4]</summary>
		public readonly static DicomUID SpatialFiducialsStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.2", "Spatial Fiducials Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Deformable Spatial Registration Storage [PS 3.4]</summary>
		public readonly static DicomUID DeformableSpatialRegistrationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.3", "Deformable Spatial Registration Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Segmentation Storage [PS 3.4]</summary>
		public readonly static DicomUID SegmentationStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.66.4", "Segmentation Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Real World Value Mapping Storage [PS 3.4]</summary>
		public readonly static DicomUID RealWorldValueMappingStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.67", "Real World Value Mapping Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Secondary Capture Image Storage [PS 3.4]</summary>
		public readonly static DicomUID SecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7", "Secondary Capture Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Multi-frame Single Bit Secondary Capture Image Storage [PS 3.4]</summary>
		public readonly static DicomUID MultiframeSingleBitSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.1", "Multi-frame Single Bit Secondary Capture Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Multi-frame Grayscale Byte Secondary Capture Image Storage [PS 3.4]</summary>
		public readonly static DicomUID MultiframeGrayscaleByteSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.2", "Multi-frame Grayscale Byte Secondary Capture Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Multi-frame Grayscale Word Secondary Capture Image Storage [PS 3.4]</summary>
		public readonly static DicomUID MultiframeGrayscaleWordSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.3", "Multi-frame Grayscale Word Secondary Capture Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Multi-frame True Color Secondary Capture Image Storage [PS 3.4]</summary>
		public readonly static DicomUID MultiframeTrueColorSecondaryCaptureImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.7.4", "Multi-frame True Color Secondary Capture Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: VL Image Storage - Trial [PS 3.4] (Retired)</summary>
		public readonly static DicomUID VLImageStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1", "VL Image Storage - Trial", DicomUidType.SOPClass);

		/// <summary>SOP Class: VL Endoscopic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID VLEndoscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.1", "VL Endoscopic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Video Endoscopic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID VideoEndoscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.1.1", "Video Endoscopic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: VL Microscopic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID VLMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.2", "VL Microscopic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Video Microscopic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID VideoMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.2.1", "Video Microscopic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: VL Slide-Coordinates Microscopic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID VLSlideCoordinatesMicroscopicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.3", "VL Slide-Coordinates Microscopic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: VL Photographic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID VLPhotographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.4", "VL Photographic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Video Photographic Image Storage [PS 3.4]</summary>
		public readonly static DicomUID VideoPhotographicImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.4.1", "Video Photographic Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ophthalmic Photography 8 Bit Image Storage [PS 3.4]</summary>
		public readonly static DicomUID OphthalmicPhotography8BitImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.1", "Ophthalmic Photography 8 Bit Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ophthalmic Photography 16 Bit Image Storage [PS 3.4]</summary>
		public readonly static DicomUID OphthalmicPhotography16BitImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.2", "Ophthalmic Photography 16 Bit Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Stereometric Relationship Storage [PS 3.4]</summary>
		public readonly static DicomUID StereometricRelationshipStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.3", "Stereometric Relationship Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ophthalmic Tomography Image Storage [PS 3.4]</summary>
		public readonly static DicomUID OphthalmicTomographyImageStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.77.1.5.4", "Ophthalmic Tomography Image Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: VL Multi-frame Image Storage – Trial [PS 3.4] (Retired)</summary>
		public readonly static DicomUID VLMultiframeImageStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.77.2", "VL Multi-frame Image Storage – Trial", DicomUidType.SOPClass);

		/// <summary>SOP Class: Standalone Overlay Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StandaloneOverlayStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.8", "Standalone Overlay Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Text SR Storage – Trial [PS 3.4] (Retired)</summary>
		public readonly static DicomUID TextSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.1", "Text SR Storage – Trial", DicomUidType.SOPClass);

		/// <summary>SOP Class: Basic Text SR Storage [PS 3.4]</summary>
		public readonly static DicomUID BasicTextSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.11", "Basic Text SR Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Audio SR Storage – Trial [PS 3.4] (Retired)</summary>
		public readonly static DicomUID AudioSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.2", "Audio SR Storage – Trial", DicomUidType.SOPClass);

		/// <summary>SOP Class: Enhanced SR Storage [PS 3.4]</summary>
		public readonly static DicomUID EnhancedSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.22", "Enhanced SR Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Detail SR Storage – Trial [PS 3.4] (Retired)</summary>
		public readonly static DicomUID DetailSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.3", "Detail SR Storage – Trial", DicomUidType.SOPClass);

		/// <summary>SOP Class: Comprehensive SR Storage [PS 3.4]</summary>
		public readonly static DicomUID ComprehensiveSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.33", "Comprehensive SR Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Comprehensive SR Storage – Trial [PS 3.4] (Retired)</summary>
		public readonly static DicomUID ComprehensiveSRStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.88.4", "Comprehensive SR Storage – Trial", DicomUidType.SOPClass);

		/// <summary>SOP Class: Procedure Log Storage [PS 3.4]</summary>
		public readonly static DicomUID ProcedureLogStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.40", "Procedure Log Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Mammography CAD SR Storage [PS 3.4]</summary>
		public readonly static DicomUID MammographyCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.50", "Mammography CAD SR Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Key Object Selection Document Storage [PS 3.4]</summary>
		public readonly static DicomUID KeyObjectSelectionDocumentStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.59", "Key Object Selection Document Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Chest CAD SR Storage [PS 3.4]</summary>
		public readonly static DicomUID ChestCADSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.65", "Chest CAD SR Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: X-Ray Radiation Dose SR Storage [PS 3.4]</summary>
		public readonly static DicomUID XRayRadiationDoseSRStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.88.67", "X-Ray Radiation Dose SR Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Standalone Curve Storage [PS 3.4] (Retired)</summary>
		public readonly static DicomUID StandaloneCurveStorageRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.9", "Standalone Curve Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Waveform Storage - Trial [PS 3.4] (Retired)</summary>
		public readonly static DicomUID WaveformStorageTrialRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1", "Waveform Storage - Trial", DicomUidType.SOPClass);

		/// <summary>SOP Class: 12-lead ECG Waveform Storage [PS 3.4]</summary>
		public readonly static DicomUID TwelveLeadECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.1", "12-lead ECG Waveform Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: General ECG Waveform Storage [PS 3.4]</summary>
		public readonly static DicomUID GeneralECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.2", "General ECG Waveform Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Ambulatory ECG Waveform Storage [PS 3.4]</summary>
		public readonly static DicomUID AmbulatoryECGWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.1.3", "Ambulatory ECG Waveform Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Hemodynamic Waveform Storage [PS 3.4]</summary>
		public readonly static DicomUID HemodynamicWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.2.1", "Hemodynamic Waveform Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Cardiac Electrophysiology Waveform Storage [PS 3.4]</summary>
		public readonly static DicomUID CardiacElectrophysiologyWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.3.1", "Cardiac Electrophysiology Waveform Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Basic Voice Audio Waveform Storage [PS 3.4]</summary>
		public readonly static DicomUID BasicVoiceAudioWaveformStorage = new DicomUID("1.2.840.10008.5.1.4.1.1.9.4.1", "Basic Voice Audio Waveform Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Patient Root Query/Retrieve Information Model – FIND [PS 3.4]</summary>
		public readonly static DicomUID PatientRootQueryRetrieveInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.1.2.1.1", "Patient Root Query/Retrieve Information Model – FIND", DicomUidType.SOPClass);

		/// <summary>SOP Class: Patient Root Query/Retrieve Information Model – MOVE [PS 3.4]</summary>
		public readonly static DicomUID PatientRootQueryRetrieveInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.1.2", "Patient Root Query/Retrieve Information Model – MOVE", DicomUidType.SOPClass);

		/// <summary>SOP Class: Patient Root Query/Retrieve Information Model – GET [PS 3.4]</summary>
		public readonly static DicomUID PatientRootQueryRetrieveInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.1.2.1.3", "Patient Root Query/Retrieve Information Model – GET", DicomUidType.SOPClass);

		/// <summary>SOP Class: Study Root Query/Retrieve Information Model – FIND [PS 3.4]</summary>
		public readonly static DicomUID StudyRootQueryRetrieveInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.1.2.2.1", "Study Root Query/Retrieve Information Model – FIND", DicomUidType.SOPClass);

		/// <summary>SOP Class: Study Root Query/Retrieve Information Model – MOVE [PS 3.4]</summary>
		public readonly static DicomUID StudyRootQueryRetrieveInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.1.2.2.2", "Study Root Query/Retrieve Information Model – MOVE", DicomUidType.SOPClass);

		/// <summary>SOP Class: Study Root Query/Retrieve Information Model – GET [PS 3.4]</summary>
		public readonly static DicomUID StudyRootQueryRetrieveInformationModelGET = new DicomUID("1.2.840.10008.5.1.4.1.2.2.3", "Study Root Query/Retrieve Information Model – GET", DicomUidType.SOPClass);

		/// <summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - FIND [PS 3.4] (Retired)</summary>
		public readonly static DicomUID PatientStudyOnlyQueryRetrieveInformationModelFINDRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.1", "Patient/Study Only Query/Retrieve Information Model - FIND", DicomUidType.SOPClass);

		/// <summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - MOVE [PS 3.4] (Retired)</summary>
		public readonly static DicomUID PatientStudyOnlyQueryRetrieveInformationModelMOVERETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.2", "Patient/Study Only Query/Retrieve Information Model - MOVE", DicomUidType.SOPClass);

		/// <summary>SOP Class: Patient/Study Only Query/Retrieve Information Model - GET [PS 3.4] (Retired)</summary>
		public readonly static DicomUID PatientStudyOnlyQueryRetrieveInformationModelGETRETIRED = new DicomUID("1.2.840.10008.5.1.4.1.2.3.3", "Patient/Study Only Query/Retrieve Information Model - GET", DicomUidType.SOPClass);

		/// <summary>SOP Class: Modality Worklist Information Model – FIND [PS 3.4]</summary>
		public readonly static DicomUID ModalityWorklistInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.31", "Modality Worklist Information Model – FIND", DicomUidType.SOPClass);

		/// <summary>Meta SOP Class: General Purpose Worklist Management Meta SOP Class [PS 3.4]</summary>
		public readonly static DicomUID GeneralPurposeWorklistManagementMetaSOPClass = new DicomUID("1.2.840.10008.5.1.4.32", "General Purpose Worklist Management Meta SOP Class", DicomUidType.MetaSOPClass);

		/// <summary>SOP Class: General Purpose Worklist Information Model – FIND [PS 3.4]</summary>
		public readonly static DicomUID GeneralPurposeWorklistInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.32.1", "General Purpose Worklist Information Model – FIND", DicomUidType.SOPClass);

		/// <summary>SOP Class: General Purpose Scheduled Procedure Step SOP Class [PS 3.4]</summary>
		public readonly static DicomUID GeneralPurposeScheduledProcedureStepSOPClass = new DicomUID("1.2.840.10008.5.1.4.32.2", "General Purpose Scheduled Procedure Step SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: General Purpose Performed Procedure Step SOP Class [PS 3.4]</summary>
		public readonly static DicomUID GeneralPurposePerformedProcedureStepSOPClass = new DicomUID("1.2.840.10008.5.1.4.32.3", "General Purpose Performed Procedure Step SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Instance Availability Notification SOP Class [PS 3.4]</summary>
		public readonly static DicomUID InstanceAvailabilityNotificationSOPClass = new DicomUID("1.2.840.10008.5.1.4.33", "Instance Availability Notification SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Beams Delivery Instruction Storage (Supplement 74 Frozen Draft) [PS 3.4]</summary>
		public readonly static DicomUID RTBeamsDeliveryInstructionStorageSupplement74FrozenDraft = new DicomUID("1.2.840.10008.5.1.4.34.1", "RT Beams Delivery Instruction Storage (Supplement 74 Frozen Draft)", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Conventional Machine Verification (Supplement 74 Frozen Draft) [PS 3.4]</summary>
		public readonly static DicomUID RTConventionalMachineVerificationSupplement74FrozenDraft = new DicomUID("1.2.840.10008.5.1.4.34.2", "RT Conventional Machine Verification (Supplement 74 Frozen Draft)", DicomUidType.SOPClass);

		/// <summary>SOP Class: RT Ion Machine Verification (Supplement 74 Frozen Draft) [PS 3.4]</summary>
		public readonly static DicomUID RTIonMachineVerificationSupplement74FrozenDraft = new DicomUID("1.2.840.10008.5.1.4.34.3", "RT Ion Machine Verification (Supplement 74 Frozen Draft)", DicomUidType.SOPClass);

		/// <summary>Service Class: Unified Worklist and Procedure Step Service Class [PS 3.4]</summary>
		public readonly static DicomUID UnifiedWorklistAndProcedureStepSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.4", "Unified Worklist and Procedure Step Service Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Unified Procedure Step – Push SOP Class [PS 3.4]</summary>
		public readonly static DicomUID UnifiedProcedureStepPushSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.4.1", "Unified Procedure Step – Push SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Unified Procedure Step – Watch SOP Class [PS 3.4]</summary>
		public readonly static DicomUID UnifiedProcedureStepWatchSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.4.2", "Unified Procedure Step – Watch SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Unified Procedure Step – Pull SOP Class [PS 3.4]</summary>
		public readonly static DicomUID UnifiedProcedureStepPullSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.4.3", "Unified Procedure Step – Pull SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Unified Procedure Step – Event SOP Class [PS 3.4]</summary>
		public readonly static DicomUID UnifiedProcedureStepEventSOPClass = new DicomUID("1.2.840.10008.5.1.4.34.4.4", "Unified Procedure Step – Event SOP Class", DicomUidType.SOPClass);

		/// <summary>Well-known SOP Instance: Unified Worklist and Procedure Step SOP Instance [PS 3.4]</summary>
		public readonly static DicomUID UnifiedWorklistAndProcedureStepSOPInstance = new DicomUID("1.2.840.10008.5.1.4.34.5", "Unified Worklist and Procedure Step SOP Instance", DicomUidType.SOPInstance);

		/// <summary>SOP Class: General Relevant Patient Information Query [PS 3.4]</summary>
		public readonly static DicomUID GeneralRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.1", "General Relevant Patient Information Query", DicomUidType.SOPClass);

		/// <summary>SOP Class: Breast Imaging Relevant Patient Information Query [PS 3.4]</summary>
		public readonly static DicomUID BreastImagingRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.2", "Breast Imaging Relevant Patient Information Query", DicomUidType.SOPClass);

		/// <summary>SOP Class: Cardiac Relevant Patient Information Query [PS 3.4]</summary>
		public readonly static DicomUID CardiacRelevantPatientInformationQuery = new DicomUID("1.2.840.10008.5.1.4.37.3", "Cardiac Relevant Patient Information Query", DicomUidType.SOPClass);

		/// <summary>SOP Class: Hanging Protocol Storage [PS 3.4]</summary>
		public readonly static DicomUID HangingProtocolStorage = new DicomUID("1.2.840.10008.5.1.4.38.1", "Hanging Protocol Storage", DicomUidType.SOPClass);

		/// <summary>SOP Class: Hanging Protocol Information Model – FIND [PS 3.4]</summary>
		public readonly static DicomUID HangingProtocolInformationModelFIND = new DicomUID("1.2.840.10008.5.1.4.38.2", "Hanging Protocol Information Model – FIND", DicomUidType.SOPClass);

		/// <summary>SOP Class: Hanging Protocol Information Model – MOVE [PS 3.4]</summary>
		public readonly static DicomUID HangingProtocolInformationModelMOVE = new DicomUID("1.2.840.10008.5.1.4.38.3", "Hanging Protocol Information Model – MOVE", DicomUidType.SOPClass);

		/// <summary>SOP Class: Product Characteristics Query SOP Class [PS 3.4]</summary>
		public readonly static DicomUID ProductCharacteristicsQuerySOPClass = new DicomUID("1.2.840.10008.5.1.4.41", "Product Characteristics Query SOP Class", DicomUidType.SOPClass);

		/// <summary>SOP Class: Substance Approval Query SOP Class [PS 3.4]</summary>
		public readonly static DicomUID SubstanceApprovalQuerySOPClass = new DicomUID("1.2.840.10008.5.1.4.42", "Substance Approval Query SOP Class", DicomUidType.SOPClass);
	}
}
