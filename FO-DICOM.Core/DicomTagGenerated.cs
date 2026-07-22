
// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom
{

    public partial class DicomTag
    {

        ///<summary>(0000,0000) VR=UL VM=1 Command Group Length</summary>
        public readonly static DicomTagUL CommandGroupLength = new DicomTagUL(0x0000, 0x0000);

        ///<summary>(0000,0002) VR=UI VM=1 Affected SOP Class UID</summary>
        public readonly static DicomTagUI AffectedSOPClassUID = new DicomTagUI(0x0000, 0x0002);

        ///<summary>(0000,0003) VR=UI VM=1 Requested SOP Class UID</summary>
        public readonly static DicomTagUI RequestedSOPClassUID = new DicomTagUI(0x0000, 0x0003);

        ///<summary>(0000,0100) VR=US VM=1 Command Field</summary>
        public readonly static DicomTagUS CommandField = new DicomTagUS(0x0000, 0x0100);

        ///<summary>(0000,0110) VR=US VM=1 Message ID</summary>
        public readonly static DicomTagUS MessageID = new DicomTagUS(0x0000, 0x0110);

        ///<summary>(0000,0120) VR=US VM=1 Message ID Being Responded To</summary>
        public readonly static DicomTagUS MessageIDBeingRespondedTo = new DicomTagUS(0x0000, 0x0120);

        ///<summary>(0000,0600) VR=AE VM=1 Move Destination</summary>
        public readonly static DicomTagAE MoveDestination = new DicomTagAE(0x0000, 0x0600);

        ///<summary>(0000,0700) VR=US VM=1 Priority</summary>
        public readonly static DicomTagUS Priority = new DicomTagUS(0x0000, 0x0700);

        ///<summary>(0000,0800) VR=US VM=1 Command Data Set Type</summary>
        public readonly static DicomTagUS CommandDataSetType = new DicomTagUS(0x0000, 0x0800);

        ///<summary>(0000,0900) VR=US VM=1 Status</summary>
        public readonly static DicomTagUS Status = new DicomTagUS(0x0000, 0x0900);

        ///<summary>(0000,0901) VR=AT VM=1-n Offending Element</summary>
        public readonly static DicomTagATs OffendingElement = new DicomTagATs(0x0000, 0x0901);

        ///<summary>(0000,0902) VR=LO VM=1 Error Comment</summary>
        public readonly static DicomTagLO ErrorComment = new DicomTagLO(0x0000, 0x0902);

        ///<summary>(0000,0903) VR=US VM=1 Error ID</summary>
        public readonly static DicomTagUS ErrorID = new DicomTagUS(0x0000, 0x0903);

        ///<summary>(0000,1000) VR=UI VM=1 Affected SOP Instance UID</summary>
        public readonly static DicomTagUI AffectedSOPInstanceUID = new DicomTagUI(0x0000, 0x1000);

        ///<summary>(0000,1001) VR=UI VM=1 Requested SOP Instance UID</summary>
        public readonly static DicomTagUI RequestedSOPInstanceUID = new DicomTagUI(0x0000, 0x1001);

        ///<summary>(0000,1002) VR=US VM=1 Event Type ID</summary>
        public readonly static DicomTagUS EventTypeID = new DicomTagUS(0x0000, 0x1002);

        ///<summary>(0000,1005) VR=AT VM=1-n Attribute Identifier List</summary>
        public readonly static DicomTagATs AttributeIdentifierList = new DicomTagATs(0x0000, 0x1005);

        ///<summary>(0000,1008) VR=US VM=1 Action Type ID</summary>
        public readonly static DicomTagUS ActionTypeID = new DicomTagUS(0x0000, 0x1008);

        ///<summary>(0000,1020) VR=US VM=1 Number of Remaining Sub-operations</summary>
        public readonly static DicomTagUS NumberOfRemainingSuboperations = new DicomTagUS(0x0000, 0x1020);

        ///<summary>(0000,1021) VR=US VM=1 Number of Completed Sub-operations</summary>
        public readonly static DicomTagUS NumberOfCompletedSuboperations = new DicomTagUS(0x0000, 0x1021);

        ///<summary>(0000,1022) VR=US VM=1 Number of Failed Sub-operations</summary>
        public readonly static DicomTagUS NumberOfFailedSuboperations = new DicomTagUS(0x0000, 0x1022);

        ///<summary>(0000,1023) VR=US VM=1 Number of Warning Sub-operations</summary>
        public readonly static DicomTagUS NumberOfWarningSuboperations = new DicomTagUS(0x0000, 0x1023);

        ///<summary>(0000,1030) VR=AE VM=1 Move Originator Application Entity Title</summary>
        public readonly static DicomTagAE MoveOriginatorApplicationEntityTitle = new DicomTagAE(0x0000, 0x1030);

        ///<summary>(0000,1031) VR=US VM=1 Move Originator Message ID</summary>
        public readonly static DicomTagUS MoveOriginatorMessageID = new DicomTagUS(0x0000, 0x1031);

        ///<summary>(0000,0001) VR=UL VM=1 Command Length to End</summary>
        public readonly static DicomTagUL CommandLengthToEnd = new DicomTagUL(0x0000, 0x0001);

        ///<summary>(0000,0010) VR=SH VM=1 Command Recognition Code</summary>
        public readonly static DicomTagSH CommandRecognitionCode = new DicomTagSH(0x0000, 0x0010);

        ///<summary>(0000,0200) VR=AE VM=1 Initiator</summary>
        public readonly static DicomTagAE Initiator = new DicomTagAE(0x0000, 0x0200);

        ///<summary>(0000,0300) VR=AE VM=1 Receiver</summary>
        public readonly static DicomTagAE Receiver = new DicomTagAE(0x0000, 0x0300);

        ///<summary>(0000,0400) VR=AE VM=1 Find Location</summary>
        public readonly static DicomTagAE FindLocation = new DicomTagAE(0x0000, 0x0400);

        ///<summary>(0000,0850) VR=US VM=1 Number of Matches</summary>
        public readonly static DicomTagUS NumberOfMatches = new DicomTagUS(0x0000, 0x0850);

        ///<summary>(0000,0860) VR=US VM=1 Response Sequence Number</summary>
        public readonly static DicomTagUS ResponseSequenceNumber = new DicomTagUS(0x0000, 0x0860);

        ///<summary>(0000,4000) VR=LT VM=1 Dialog Receiver</summary>
        public readonly static DicomTagLT DialogReceiver = new DicomTagLT(0x0000, 0x4000);

        ///<summary>(0000,4010) VR=LT VM=1 Terminal Type</summary>
        public readonly static DicomTagLT TerminalType = new DicomTagLT(0x0000, 0x4010);

        ///<summary>(0000,5010) VR=SH VM=1 Message Set ID</summary>
        public readonly static DicomTagSH MessageSetID = new DicomTagSH(0x0000, 0x5010);

        ///<summary>(0000,5020) VR=SH VM=1 End Message ID</summary>
        public readonly static DicomTagSH EndMessageID = new DicomTagSH(0x0000, 0x5020);

        ///<summary>(0000,5110) VR=LT VM=1 Display Format</summary>
        public readonly static DicomTagLT DisplayFormat = new DicomTagLT(0x0000, 0x5110);

        ///<summary>(0000,5120) VR=LT VM=1 Page Position ID</summary>
        public readonly static DicomTagLT PagePositionID = new DicomTagLT(0x0000, 0x5120);

        ///<summary>(0000,5130) VR=CS VM=1 Text Format ID</summary>
        public readonly static DicomTagCS TextFormatID = new DicomTagCS(0x0000, 0x5130);

        ///<summary>(0000,5140) VR=CS VM=1 Normal/Reverse</summary>
        public readonly static DicomTagCS NormalReverse = new DicomTagCS(0x0000, 0x5140);

        ///<summary>(0000,5150) VR=CS VM=1 Add Gray Scale</summary>
        public readonly static DicomTagCS AddGrayScale = new DicomTagCS(0x0000, 0x5150);

        ///<summary>(0000,5160) VR=CS VM=1 Borders</summary>
        public readonly static DicomTagCS Borders = new DicomTagCS(0x0000, 0x5160);

        ///<summary>(0000,5170) VR=IS VM=1 Copies</summary>
        public readonly static DicomTagIS Copies = new DicomTagIS(0x0000, 0x5170);

        ///<summary>(0000,5180) VR=CS VM=1 Command Magnification Type</summary>
        public readonly static DicomTagCS CommandMagnificationType = new DicomTagCS(0x0000, 0x5180);

        ///<summary>(0000,5190) VR=CS VM=1 Erase</summary>
        public readonly static DicomTagCS Erase = new DicomTagCS(0x0000, 0x5190);

        ///<summary>(0000,51A0) VR=CS VM=1 Print</summary>
        public readonly static DicomTagCS Print = new DicomTagCS(0x0000, 0x51A0);

        ///<summary>(0000,51B0) VR=US VM=1-n Overlays</summary>
        public readonly static DicomTagUSs Overlays = new DicomTagUSs(0x0000, 0x51B0);

        ///<summary>(0002,0000) VR=UL VM=1 File Meta Information Group Length</summary>
        public readonly static DicomTagUL FileMetaInformationGroupLength = new DicomTagUL(0x0002, 0x0000);

        ///<summary>(0002,0001) VR=OB VM=1 File Meta Information Version</summary>
        public readonly static DicomTagOB FileMetaInformationVersion = new DicomTagOB(0x0002, 0x0001);

        ///<summary>(0002,0002) VR=UI VM=1 Media Storage SOP Class UID</summary>
        public readonly static DicomTagUI MediaStorageSOPClassUID = new DicomTagUI(0x0002, 0x0002);

        ///<summary>(0002,0003) VR=UI VM=1 Media Storage SOP Instance UID</summary>
        public readonly static DicomTagUI MediaStorageSOPInstanceUID = new DicomTagUI(0x0002, 0x0003);

        ///<summary>(0002,0010) VR=UI VM=1 Transfer Syntax UID</summary>
        public readonly static DicomTagUI TransferSyntaxUID = new DicomTagUI(0x0002, 0x0010);

        ///<summary>(0002,0012) VR=UI VM=1 Implementation Class UID</summary>
        public readonly static DicomTagUI ImplementationClassUID = new DicomTagUI(0x0002, 0x0012);

        ///<summary>(0002,0013) VR=SH VM=1 Implementation Version Name</summary>
        public readonly static DicomTagSH ImplementationVersionName = new DicomTagSH(0x0002, 0x0013);

        ///<summary>(0002,0016) VR=AE VM=1 Source Application Entity Title</summary>
        public readonly static DicomTagAE SourceApplicationEntityTitle = new DicomTagAE(0x0002, 0x0016);

        ///<summary>(0002,0017) VR=AE VM=1 Sending Application Entity Title</summary>
        public readonly static DicomTagAE SendingApplicationEntityTitle = new DicomTagAE(0x0002, 0x0017);

        ///<summary>(0002,0018) VR=AE VM=1 Receiving Application Entity Title</summary>
        public readonly static DicomTagAE ReceivingApplicationEntityTitle = new DicomTagAE(0x0002, 0x0018);

        ///<summary>(0002,0026) VR=UR VM=1 Source Presentation Address</summary>
        public readonly static DicomTagUR SourcePresentationAddress = new DicomTagUR(0x0002, 0x0026);

        ///<summary>(0002,0027) VR=UR VM=1 Sending Presentation Address</summary>
        public readonly static DicomTagUR SendingPresentationAddress = new DicomTagUR(0x0002, 0x0027);

        ///<summary>(0002,0028) VR=UR VM=1 Receiving Presentation Address</summary>
        public readonly static DicomTagUR ReceivingPresentationAddress = new DicomTagUR(0x0002, 0x0028);

        ///<summary>(0002,0031) VR=OB VM=1 RTV Meta Information Version</summary>
        public readonly static DicomTagOB RTVMetaInformationVersion = new DicomTagOB(0x0002, 0x0031);

        ///<summary>(0002,0032) VR=UI VM=1 RTV Communication SOP Class UID</summary>
        public readonly static DicomTagUI RTVCommunicationSOPClassUID = new DicomTagUI(0x0002, 0x0032);

        ///<summary>(0002,0033) VR=UI VM=1 RTV Communication SOP Instance UID</summary>
        public readonly static DicomTagUI RTVCommunicationSOPInstanceUID = new DicomTagUI(0x0002, 0x0033);

        ///<summary>(0002,0035) VR=OB VM=1 RTV Source Identifier</summary>
        public readonly static DicomTagOB RTVSourceIdentifier = new DicomTagOB(0x0002, 0x0035);

        ///<summary>(0002,0036) VR=OB VM=1 RTV Flow Identifier</summary>
        public readonly static DicomTagOB RTVFlowIdentifier = new DicomTagOB(0x0002, 0x0036);

        ///<summary>(0002,0037) VR=UL VM=1 RTV Flow RTP Sampling Rate</summary>
        public readonly static DicomTagUL RTVFlowRTPSamplingRate = new DicomTagUL(0x0002, 0x0037);

        ///<summary>(0002,0038) VR=FD VM=1 RTV Flow Actual Frame Duration</summary>
        public readonly static DicomTagFD RTVFlowActualFrameDuration = new DicomTagFD(0x0002, 0x0038);

        ///<summary>(0002,0100) VR=UI VM=1 Private Information Creator UID</summary>
        public readonly static DicomTagUI PrivateInformationCreatorUID = new DicomTagUI(0x0002, 0x0100);

        ///<summary>(0002,0102) VR=OB VM=1 Private Information</summary>
        public readonly static DicomTagOB PrivateInformation = new DicomTagOB(0x0002, 0x0102);

        ///<summary>(0004,1130) VR=CS VM=1 File-set ID</summary>
        public readonly static DicomTagCS FileSetID = new DicomTagCS(0x0004, 0x1130);

        ///<summary>(0004,1141) VR=CS VM=1-8 File-set Descriptor File ID</summary>
        public readonly static DicomTagCSs FileSetDescriptorFileID = new DicomTagCSs(0x0004, 0x1141);

        ///<summary>(0004,1142) VR=CS VM=1 Specific Character Set of File-set Descriptor File</summary>
        public readonly static DicomTagCS SpecificCharacterSetOfFileSetDescriptorFile = new DicomTagCS(0x0004, 0x1142);

        ///<summary>(0004,1200) VR=UL VM=1 Offset of the First Directory Record of the Root Directory Entity</summary>
        public readonly static DicomTagUL OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity = new DicomTagUL(0x0004, 0x1200);

        ///<summary>(0004,1202) VR=UL VM=1 Offset of the Last Directory Record of the Root Directory Entity</summary>
        public readonly static DicomTagUL OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity = new DicomTagUL(0x0004, 0x1202);

        ///<summary>(0004,1212) VR=US VM=1 File-set Consistency Flag</summary>
        public readonly static DicomTagUS FileSetConsistencyFlag = new DicomTagUS(0x0004, 0x1212);

        ///<summary>(0004,1220) VR=SQ VM=1 Directory Record Sequence</summary>
        public readonly static DicomTagSQ DirectoryRecordSequence = new DicomTagSQ(0x0004, 0x1220);

        ///<summary>(0004,1400) VR=UL VM=1 Offset of the Next Directory Record</summary>
        public readonly static DicomTagUL OffsetOfTheNextDirectoryRecord = new DicomTagUL(0x0004, 0x1400);

        ///<summary>(0004,1410) VR=US VM=1 Record In-use Flag</summary>
        public readonly static DicomTagUS RecordInUseFlag = new DicomTagUS(0x0004, 0x1410);

        ///<summary>(0004,1420) VR=UL VM=1 Offset of Referenced Lower-Level Directory Entity</summary>
        public readonly static DicomTagUL OffsetOfReferencedLowerLevelDirectoryEntity = new DicomTagUL(0x0004, 0x1420);

        ///<summary>(0004,1430) VR=CS VM=1 Directory Record Type</summary>
        public readonly static DicomTagCS DirectoryRecordType = new DicomTagCS(0x0004, 0x1430);

        ///<summary>(0004,1432) VR=UI VM=1 Private Record UID</summary>
        public readonly static DicomTagUI PrivateRecordUID = new DicomTagUI(0x0004, 0x1432);

        ///<summary>(0004,1500) VR=CS VM=1-8 Referenced File ID</summary>
        public readonly static DicomTagCSs ReferencedFileID = new DicomTagCSs(0x0004, 0x1500);

        ///<summary>(0004,1504) VR=UL VM=1 MRDR Directory Record Offset (RETIRED)</summary>
        public readonly static DicomTagUL MRDRDirectoryRecordOffsetRETIRED = new DicomTagUL(0x0004, 0x1504);

        ///<summary>(0004,1510) VR=UI VM=1 Referenced SOP Class UID in File</summary>
        public readonly static DicomTagUI ReferencedSOPClassUIDInFile = new DicomTagUI(0x0004, 0x1510);

        ///<summary>(0004,1511) VR=UI VM=1 Referenced SOP Instance UID in File</summary>
        public readonly static DicomTagUI ReferencedSOPInstanceUIDInFile = new DicomTagUI(0x0004, 0x1511);

        ///<summary>(0004,1512) VR=UI VM=1 Referenced Transfer Syntax UID in File</summary>
        public readonly static DicomTagUI ReferencedTransferSyntaxUIDInFile = new DicomTagUI(0x0004, 0x1512);

        ///<summary>(0004,151A) VR=UI VM=1-n Referenced Related General SOP Class UID in File</summary>
        public readonly static DicomTagUIs ReferencedRelatedGeneralSOPClassUIDInFile = new DicomTagUIs(0x0004, 0x151A);

        ///<summary>(0004,1600) VR=UL VM=1 Number of References (RETIRED)</summary>
        public readonly static DicomTagUL NumberOfReferencesRETIRED = new DicomTagUL(0x0004, 0x1600);

        ///<summary>(0008,0001) VR=UL VM=1 Length to End (RETIRED)</summary>
        public readonly static DicomTagUL LengthToEndRETIRED = new DicomTagUL(0x0008, 0x0001);

        ///<summary>(0008,0005) VR=CS VM=1-n Specific Character Set</summary>
        public readonly static DicomTagCSs SpecificCharacterSet = new DicomTagCSs(0x0008, 0x0005);

        ///<summary>(0008,0006) VR=SQ VM=1 Language Code Sequence</summary>
        public readonly static DicomTagSQ LanguageCodeSequence = new DicomTagSQ(0x0008, 0x0006);

        ///<summary>(0008,0008) VR=CS VM=2-n Image Type</summary>
        public readonly static DicomTagCSs ImageType = new DicomTagCSs(0x0008, 0x0008);

        ///<summary>(0008,0010) VR=SH VM=1 Recognition Code (RETIRED)</summary>
        public readonly static DicomTagSH RecognitionCodeRETIRED = new DicomTagSH(0x0008, 0x0010);

        ///<summary>(0008,0012) VR=DA VM=1 Instance Creation Date</summary>
        public readonly static DicomTagDA InstanceCreationDate = new DicomTagDA(0x0008, 0x0012);

        ///<summary>(0008,0013) VR=TM VM=1 Instance Creation Time</summary>
        public readonly static DicomTagTM InstanceCreationTime = new DicomTagTM(0x0008, 0x0013);

        ///<summary>(0008,0014) VR=UI VM=1 Instance Creator UID</summary>
        public readonly static DicomTagUI InstanceCreatorUID = new DicomTagUI(0x0008, 0x0014);

        ///<summary>(0008,0015) VR=DT VM=1 Instance Coercion DateTime</summary>
        public readonly static DicomTagDT InstanceCoercionDateTime = new DicomTagDT(0x0008, 0x0015);

        ///<summary>(0008,0016) VR=UI VM=1 SOP Class UID</summary>
        public readonly static DicomTagUI SOPClassUID = new DicomTagUI(0x0008, 0x0016);

        ///<summary>(0008,0017) VR=UI VM=1 Acquisition UID</summary>
        public readonly static DicomTagUI AcquisitionUID = new DicomTagUI(0x0008, 0x0017);

        ///<summary>(0008,0018) VR=UI VM=1 SOP Instance UID</summary>
        public readonly static DicomTagUI SOPInstanceUID = new DicomTagUI(0x0008, 0x0018);

        ///<summary>(0008,0019) VR=UI VM=1 Pyramid UID</summary>
        public readonly static DicomTagUI PyramidUID = new DicomTagUI(0x0008, 0x0019);

        ///<summary>(0008,001A) VR=UI VM=1-n Related General SOP Class UID</summary>
        public readonly static DicomTagUIs RelatedGeneralSOPClassUID = new DicomTagUIs(0x0008, 0x001A);

        ///<summary>(0008,001B) VR=UI VM=1 Original Specialized SOP Class UID</summary>
        public readonly static DicomTagUI OriginalSpecializedSOPClassUID = new DicomTagUI(0x0008, 0x001B);

        ///<summary>(0008,001C) VR=CS VM=1 Synthetic Data</summary>
        public readonly static DicomTagCS SyntheticData = new DicomTagCS(0x0008, 0x001C);

        ///<summary>(0008,001D) VR=SQ VM=1 Sensitive Content Code Sequence</summary>
        public readonly static DicomTagSQ SensitiveContentCodeSequence = new DicomTagSQ(0x0008, 0x001D);

        ///<summary>(0008,0020) VR=DA VM=1 Study Date</summary>
        public readonly static DicomTagDA StudyDate = new DicomTagDA(0x0008, 0x0020);

        ///<summary>(0008,0021) VR=DA VM=1 Series Date</summary>
        public readonly static DicomTagDA SeriesDate = new DicomTagDA(0x0008, 0x0021);

        ///<summary>(0008,0022) VR=DA VM=1 Acquisition Date</summary>
        public readonly static DicomTagDA AcquisitionDate = new DicomTagDA(0x0008, 0x0022);

        ///<summary>(0008,0023) VR=DA VM=1 Content Date</summary>
        public readonly static DicomTagDA ContentDate = new DicomTagDA(0x0008, 0x0023);

        ///<summary>(0008,0024) VR=DA VM=1 Overlay Date (RETIRED)</summary>
        public readonly static DicomTagDA OverlayDateRETIRED = new DicomTagDA(0x0008, 0x0024);

        ///<summary>(0008,0025) VR=DA VM=1 Curve Date (RETIRED)</summary>
        public readonly static DicomTagDA CurveDateRETIRED = new DicomTagDA(0x0008, 0x0025);

        ///<summary>(0008,002A) VR=DT VM=1 Acquisition DateTime</summary>
        public readonly static DicomTagDT AcquisitionDateTime = new DicomTagDT(0x0008, 0x002A);

        ///<summary>(0008,0030) VR=TM VM=1 Study Time</summary>
        public readonly static DicomTagTM StudyTime = new DicomTagTM(0x0008, 0x0030);

        ///<summary>(0008,0031) VR=TM VM=1 Series Time</summary>
        public readonly static DicomTagTM SeriesTime = new DicomTagTM(0x0008, 0x0031);

        ///<summary>(0008,0032) VR=TM VM=1 Acquisition Time</summary>
        public readonly static DicomTagTM AcquisitionTime = new DicomTagTM(0x0008, 0x0032);

        ///<summary>(0008,0033) VR=TM VM=1 Content Time</summary>
        public readonly static DicomTagTM ContentTime = new DicomTagTM(0x0008, 0x0033);

        ///<summary>(0008,0034) VR=TM VM=1 Overlay Time (RETIRED)</summary>
        public readonly static DicomTagTM OverlayTimeRETIRED = new DicomTagTM(0x0008, 0x0034);

        ///<summary>(0008,0035) VR=TM VM=1 Curve Time (RETIRED)</summary>
        public readonly static DicomTagTM CurveTimeRETIRED = new DicomTagTM(0x0008, 0x0035);

        ///<summary>(0008,0040) VR=US VM=1 Data Set Type (RETIRED)</summary>
        public readonly static DicomTagUS DataSetTypeRETIRED = new DicomTagUS(0x0008, 0x0040);

        ///<summary>(0008,0041) VR=LO VM=1 Data Set Subtype (RETIRED)</summary>
        public readonly static DicomTagLO DataSetSubtypeRETIRED = new DicomTagLO(0x0008, 0x0041);

        ///<summary>(0008,0042) VR=CS VM=1 Nuclear Medicine Series Type (RETIRED)</summary>
        public readonly static DicomTagCS NuclearMedicineSeriesTypeRETIRED = new DicomTagCS(0x0008, 0x0042);

        ///<summary>(0008,0050) VR=SH VM=1 Accession Number</summary>
        public readonly static DicomTagSH AccessionNumber = new DicomTagSH(0x0008, 0x0050);

        ///<summary>(0008,0051) VR=SQ VM=1 Issuer of Accession Number Sequence</summary>
        public readonly static DicomTagSQ IssuerOfAccessionNumberSequence = new DicomTagSQ(0x0008, 0x0051);

        ///<summary>(0008,0052) VR=CS VM=1 Query/Retrieve Level</summary>
        public readonly static DicomTagCS QueryRetrieveLevel = new DicomTagCS(0x0008, 0x0052);

        ///<summary>(0008,0053) VR=CS VM=1 Query/Retrieve View</summary>
        public readonly static DicomTagCS QueryRetrieveView = new DicomTagCS(0x0008, 0x0053);

        ///<summary>(0008,0054) VR=AE VM=1-n Retrieve AE Title</summary>
        public readonly static DicomTagAEs RetrieveAETitle = new DicomTagAEs(0x0008, 0x0054);

        ///<summary>(0008,0055) VR=AE VM=1 Station AE Title</summary>
        public readonly static DicomTagAE StationAETitle = new DicomTagAE(0x0008, 0x0055);

        ///<summary>(0008,0056) VR=CS VM=1 Instance Availability</summary>
        public readonly static DicomTagCS InstanceAvailability = new DicomTagCS(0x0008, 0x0056);

        ///<summary>(0008,0058) VR=UI VM=1-n Failed SOP Instance UID List</summary>
        public readonly static DicomTagUIs FailedSOPInstanceUIDList = new DicomTagUIs(0x0008, 0x0058);

        ///<summary>(0008,0060) VR=CS VM=1 Modality</summary>
        public readonly static DicomTagCS Modality = new DicomTagCS(0x0008, 0x0060);

        ///<summary>(0008,0061) VR=CS VM=1-n Modalities in Study</summary>
        public readonly static DicomTagCSs ModalitiesInStudy = new DicomTagCSs(0x0008, 0x0061);

        ///<summary>(0008,0062) VR=UI VM=1-n SOP Classes in Study</summary>
        public readonly static DicomTagUIs SOPClassesInStudy = new DicomTagUIs(0x0008, 0x0062);

        ///<summary>(0008,0063) VR=SQ VM=1 Anatomic Regions in Study Code Sequence</summary>
        public readonly static DicomTagSQ AnatomicRegionsInStudyCodeSequence = new DicomTagSQ(0x0008, 0x0063);

        ///<summary>(0008,0064) VR=CS VM=1 Conversion Type</summary>
        public readonly static DicomTagCS ConversionType = new DicomTagCS(0x0008, 0x0064);

        ///<summary>(0008,0068) VR=CS VM=1 Presentation Intent Type</summary>
        public readonly static DicomTagCS PresentationIntentType = new DicomTagCS(0x0008, 0x0068);

        ///<summary>(0008,0070) VR=LO VM=1 Manufacturer</summary>
        public readonly static DicomTagLO Manufacturer = new DicomTagLO(0x0008, 0x0070);

        ///<summary>(0008,0080) VR=LO VM=1 Institution Name</summary>
        public readonly static DicomTagLO InstitutionName = new DicomTagLO(0x0008, 0x0080);

        ///<summary>(0008,0081) VR=ST VM=1 Institution Address</summary>
        public readonly static DicomTagST InstitutionAddress = new DicomTagST(0x0008, 0x0081);

        ///<summary>(0008,0082) VR=SQ VM=1 Institution Code Sequence</summary>
        public readonly static DicomTagSQ InstitutionCodeSequence = new DicomTagSQ(0x0008, 0x0082);

        ///<summary>(0008,0090) VR=PN VM=1 Referring Physician's Name</summary>
        public readonly static DicomTagPN ReferringPhysicianName = new DicomTagPN(0x0008, 0x0090);

        ///<summary>(0008,0092) VR=ST VM=1 Referring Physician's Address</summary>
        public readonly static DicomTagST ReferringPhysicianAddress = new DicomTagST(0x0008, 0x0092);

        ///<summary>(0008,0094) VR=SH VM=1-n Referring Physician's Telephone Numbers</summary>
        public readonly static DicomTagSHs ReferringPhysicianTelephoneNumbers = new DicomTagSHs(0x0008, 0x0094);

        ///<summary>(0008,0096) VR=SQ VM=1 Referring Physician Identification Sequence</summary>
        public readonly static DicomTagSQ ReferringPhysicianIdentificationSequence = new DicomTagSQ(0x0008, 0x0096);

        ///<summary>(0008,009C) VR=PN VM=1-n Consulting Physician's Name</summary>
        public readonly static DicomTagPNs ConsultingPhysicianName = new DicomTagPNs(0x0008, 0x009C);

        ///<summary>(0008,009D) VR=SQ VM=1 Consulting Physician Identification Sequence</summary>
        public readonly static DicomTagSQ ConsultingPhysicianIdentificationSequence = new DicomTagSQ(0x0008, 0x009D);

        ///<summary>(0008,0100) VR=SH VM=1 Code Value</summary>
        public readonly static DicomTagSH CodeValue = new DicomTagSH(0x0008, 0x0100);

        ///<summary>(0008,0101) VR=LO VM=1 Extended Code Value</summary>
        public readonly static DicomTagLO ExtendedCodeValue = new DicomTagLO(0x0008, 0x0101);

        ///<summary>(0008,0102) VR=SH VM=1 Coding Scheme Designator</summary>
        public readonly static DicomTagSH CodingSchemeDesignator = new DicomTagSH(0x0008, 0x0102);

        ///<summary>(0008,0103) VR=SH VM=1 Coding Scheme Version</summary>
        public readonly static DicomTagSH CodingSchemeVersion = new DicomTagSH(0x0008, 0x0103);

        ///<summary>(0008,0104) VR=LO VM=1 Code Meaning</summary>
        public readonly static DicomTagLO CodeMeaning = new DicomTagLO(0x0008, 0x0104);

        ///<summary>(0008,0105) VR=CS VM=1 Mapping Resource</summary>
        public readonly static DicomTagCS MappingResource = new DicomTagCS(0x0008, 0x0105);

        ///<summary>(0008,0106) VR=DT VM=1 Context Group Version</summary>
        public readonly static DicomTagDT ContextGroupVersion = new DicomTagDT(0x0008, 0x0106);

        ///<summary>(0008,0107) VR=DT VM=1 Context Group Local Version</summary>
        public readonly static DicomTagDT ContextGroupLocalVersion = new DicomTagDT(0x0008, 0x0107);

        ///<summary>(0008,0108) VR=LT VM=1 Extended Code Meaning</summary>
        public readonly static DicomTagLT ExtendedCodeMeaning = new DicomTagLT(0x0008, 0x0108);

        ///<summary>(0008,0109) VR=SQ VM=1 Coding Scheme Resources Sequence</summary>
        public readonly static DicomTagSQ CodingSchemeResourcesSequence = new DicomTagSQ(0x0008, 0x0109);

        ///<summary>(0008,010A) VR=CS VM=1 Coding Scheme URL Type</summary>
        public readonly static DicomTagCS CodingSchemeURLType = new DicomTagCS(0x0008, 0x010A);

        ///<summary>(0008,010B) VR=CS VM=1 Context Group Extension Flag</summary>
        public readonly static DicomTagCS ContextGroupExtensionFlag = new DicomTagCS(0x0008, 0x010B);

        ///<summary>(0008,010C) VR=UI VM=1 Coding Scheme UID</summary>
        public readonly static DicomTagUI CodingSchemeUID = new DicomTagUI(0x0008, 0x010C);

        ///<summary>(0008,010D) VR=UI VM=1 Context Group Extension Creator UID</summary>
        public readonly static DicomTagUI ContextGroupExtensionCreatorUID = new DicomTagUI(0x0008, 0x010D);

        ///<summary>(0008,010E) VR=UR VM=1 Coding Scheme URL</summary>
        public readonly static DicomTagUR CodingSchemeURL = new DicomTagUR(0x0008, 0x010E);

        ///<summary>(0008,010F) VR=CS VM=1 Context Identifier</summary>
        public readonly static DicomTagCS ContextIdentifier = new DicomTagCS(0x0008, 0x010F);

        ///<summary>(0008,0110) VR=SQ VM=1 Coding Scheme Identification Sequence</summary>
        public readonly static DicomTagSQ CodingSchemeIdentificationSequence = new DicomTagSQ(0x0008, 0x0110);

        ///<summary>(0008,0112) VR=LO VM=1 Coding Scheme Registry</summary>
        public readonly static DicomTagLO CodingSchemeRegistry = new DicomTagLO(0x0008, 0x0112);

        ///<summary>(0008,0114) VR=ST VM=1 Coding Scheme External ID</summary>
        public readonly static DicomTagST CodingSchemeExternalID = new DicomTagST(0x0008, 0x0114);

        ///<summary>(0008,0115) VR=ST VM=1 Coding Scheme Name</summary>
        public readonly static DicomTagST CodingSchemeName = new DicomTagST(0x0008, 0x0115);

        ///<summary>(0008,0116) VR=ST VM=1 Coding Scheme Responsible Organization</summary>
        public readonly static DicomTagST CodingSchemeResponsibleOrganization = new DicomTagST(0x0008, 0x0116);

        ///<summary>(0008,0117) VR=UI VM=1 Context UID</summary>
        public readonly static DicomTagUI ContextUID = new DicomTagUI(0x0008, 0x0117);

        ///<summary>(0008,0118) VR=UI VM=1 Mapping Resource UID</summary>
        public readonly static DicomTagUI MappingResourceUID = new DicomTagUI(0x0008, 0x0118);

        ///<summary>(0008,0119) VR=UC VM=1 Long Code Value</summary>
        public readonly static DicomTagUC LongCodeValue = new DicomTagUC(0x0008, 0x0119);

        ///<summary>(0008,0120) VR=UR VM=1 URN Code Value</summary>
        public readonly static DicomTagUR URNCodeValue = new DicomTagUR(0x0008, 0x0120);

        ///<summary>(0008,0121) VR=SQ VM=1 Equivalent Code Sequence</summary>
        public readonly static DicomTagSQ EquivalentCodeSequence = new DicomTagSQ(0x0008, 0x0121);

        ///<summary>(0008,0122) VR=LO VM=1 Mapping Resource Name</summary>
        public readonly static DicomTagLO MappingResourceName = new DicomTagLO(0x0008, 0x0122);

        ///<summary>(0008,0123) VR=SQ VM=1 Context Group Identification Sequence</summary>
        public readonly static DicomTagSQ ContextGroupIdentificationSequence = new DicomTagSQ(0x0008, 0x0123);

        ///<summary>(0008,0124) VR=SQ VM=1 Mapping Resource Identification Sequence</summary>
        public readonly static DicomTagSQ MappingResourceIdentificationSequence = new DicomTagSQ(0x0008, 0x0124);

        ///<summary>(0008,0201) VR=SH VM=1 Timezone Offset From UTC</summary>
        public readonly static DicomTagSH TimezoneOffsetFromUTC = new DicomTagSH(0x0008, 0x0201);

        ///<summary>(0008,0220) VR=SQ VM=1 Responsible Group Code Sequence</summary>
        public readonly static DicomTagSQ ResponsibleGroupCodeSequence = new DicomTagSQ(0x0008, 0x0220);

        ///<summary>(0008,0221) VR=CS VM=1 Equipment Modality</summary>
        public readonly static DicomTagCS EquipmentModality = new DicomTagCS(0x0008, 0x0221);

        ///<summary>(0008,0222) VR=LO VM=1 Manufacturer's Related Model Group</summary>
        public readonly static DicomTagLO ManufacturerRelatedModelGroup = new DicomTagLO(0x0008, 0x0222);

        ///<summary>(0008,0300) VR=SQ VM=1 Private Data Element Characteristics Sequence</summary>
        public readonly static DicomTagSQ PrivateDataElementCharacteristicsSequence = new DicomTagSQ(0x0008, 0x0300);

        ///<summary>(0008,0301) VR=US VM=1 Private Group Reference</summary>
        public readonly static DicomTagUS PrivateGroupReference = new DicomTagUS(0x0008, 0x0301);

        ///<summary>(0008,0302) VR=LO VM=1 Private Creator Reference</summary>
        public readonly static DicomTagLO PrivateCreatorReference = new DicomTagLO(0x0008, 0x0302);

        ///<summary>(0008,0303) VR=CS VM=1 Block Identifying Information Status</summary>
        public readonly static DicomTagCS BlockIdentifyingInformationStatus = new DicomTagCS(0x0008, 0x0303);

        ///<summary>(0008,0304) VR=US VM=1-n Nonidentifying Private Elements</summary>
        public readonly static DicomTagUSs NonidentifyingPrivateElements = new DicomTagUSs(0x0008, 0x0304);

        ///<summary>(0008,0306) VR=US VM=1-n Identifying Private Elements</summary>
        public readonly static DicomTagUSs IdentifyingPrivateElements = new DicomTagUSs(0x0008, 0x0306);

        ///<summary>(0008,0305) VR=SQ VM=1 Deidentification Action Sequence</summary>
        public readonly static DicomTagSQ DeidentificationActionSequence = new DicomTagSQ(0x0008, 0x0305);

        ///<summary>(0008,0307) VR=CS VM=1 Deidentification Action</summary>
        public readonly static DicomTagCS DeidentificationAction = new DicomTagCS(0x0008, 0x0307);

        ///<summary>(0008,0308) VR=US VM=1 Private Data Element</summary>
        public readonly static DicomTagUS PrivateDataElement = new DicomTagUS(0x0008, 0x0308);

        ///<summary>(0008,0309) VR=UL VM=1-3 Private Data Element Value Multiplicity</summary>
        public readonly static DicomTagULs PrivateDataElementValueMultiplicity = new DicomTagULs(0x0008, 0x0309);

        ///<summary>(0008,030A) VR=CS VM=1 Private Data Element Value Representation</summary>
        public readonly static DicomTagCS PrivateDataElementValueRepresentation = new DicomTagCS(0x0008, 0x030A);

        ///<summary>(0008,030B) VR=UL VM=1-2 Private Data Element Number of Items</summary>
        public readonly static DicomTagULs PrivateDataElementNumberOfItems = new DicomTagULs(0x0008, 0x030B);

        ///<summary>(0008,030C) VR=UC VM=1 Private Data Element Name</summary>
        public readonly static DicomTagUC PrivateDataElementName = new DicomTagUC(0x0008, 0x030C);

        ///<summary>(0008,030D) VR=UC VM=1 Private Data Element Keyword</summary>
        public readonly static DicomTagUC PrivateDataElementKeyword = new DicomTagUC(0x0008, 0x030D);

        ///<summary>(0008,030E) VR=UT VM=1 Private Data Element Description</summary>
        public readonly static DicomTagUT PrivateDataElementDescription = new DicomTagUT(0x0008, 0x030E);

        ///<summary>(0008,030F) VR=UT VM=1 Private Data Element Encoding</summary>
        public readonly static DicomTagUT PrivateDataElementEncoding = new DicomTagUT(0x0008, 0x030F);

        ///<summary>(0008,0310) VR=SQ VM=1 Private Data Element Definition Sequence</summary>
        public readonly static DicomTagSQ PrivateDataElementDefinitionSequence = new DicomTagSQ(0x0008, 0x0310);

        ///<summary>(0008,0400) VR=SQ VM=1 Scope of Inventory Sequence</summary>
        public readonly static DicomTagSQ ScopeOfInventorySequence = new DicomTagSQ(0x0008, 0x0400);

        ///<summary>(0008,0401) VR=LT VM=1 Inventory Purpose</summary>
        public readonly static DicomTagLT InventoryPurpose = new DicomTagLT(0x0008, 0x0401);

        ///<summary>(0008,0402) VR=LT VM=1 Inventory Instance Description</summary>
        public readonly static DicomTagLT InventoryInstanceDescription = new DicomTagLT(0x0008, 0x0402);

        ///<summary>(0008,0403) VR=CS VM=1 Inventory Level</summary>
        public readonly static DicomTagCS InventoryLevel = new DicomTagCS(0x0008, 0x0403);

        ///<summary>(0008,0404) VR=DT VM=1 Item Inventory DateTime</summary>
        public readonly static DicomTagDT ItemInventoryDateTime = new DicomTagDT(0x0008, 0x0404);

        ///<summary>(0008,0405) VR=CS VM=1 Removed from Operational Use</summary>
        public readonly static DicomTagCS RemovedFromOperationalUse = new DicomTagCS(0x0008, 0x0405);

        ///<summary>(0008,0406) VR=SQ VM=1 Reason for Removal Code Sequence</summary>
        public readonly static DicomTagSQ ReasonForRemovalCodeSequence = new DicomTagSQ(0x0008, 0x0406);

        ///<summary>(0008,0407) VR=UR VM=1 Stored Instance Base URI</summary>
        public readonly static DicomTagUR StoredInstanceBaseURI = new DicomTagUR(0x0008, 0x0407);

        ///<summary>(0008,0408) VR=UR VM=1 Folder Access URI</summary>
        public readonly static DicomTagUR FolderAccessURI = new DicomTagUR(0x0008, 0x0408);

        ///<summary>(0008,0409) VR=UR VM=1 File Access URI</summary>
        public readonly static DicomTagUR FileAccessURI = new DicomTagUR(0x0008, 0x0409);

        ///<summary>(0008,040A) VR=CS VM=1 Container File Type</summary>
        public readonly static DicomTagCS ContainerFileType = new DicomTagCS(0x0008, 0x040A);

        ///<summary>(0008,040B) VR=UR VM=1 Filename in Container</summary>
        public readonly static DicomTagUR FilenameInContainer = new DicomTagUR(0x0008, 0x040B);

        ///<summary>(0008,040C) VR=UV VM=1 File Offset in Container</summary>
        public readonly static DicomTagUV FileOffsetInContainer = new DicomTagUV(0x0008, 0x040C);

        ///<summary>(0008,040D) VR=UV VM=1 File Length in Container</summary>
        public readonly static DicomTagUV FileLengthInContainer = new DicomTagUV(0x0008, 0x040D);

        ///<summary>(0008,040E) VR=UI VM=1 Stored Instance Transfer Syntax UID</summary>
        public readonly static DicomTagUI StoredInstanceTransferSyntaxUID = new DicomTagUI(0x0008, 0x040E);

        ///<summary>(0008,040F) VR=CS VM=1-n Extended Matching Mechanisms</summary>
        public readonly static DicomTagCSs ExtendedMatchingMechanisms = new DicomTagCSs(0x0008, 0x040F);

        ///<summary>(0008,0410) VR=SQ VM=1 Range Matching Sequence</summary>
        public readonly static DicomTagSQ RangeMatchingSequence = new DicomTagSQ(0x0008, 0x0410);

        ///<summary>(0008,0411) VR=SQ VM=1 List of UID Matching Sequence</summary>
        public readonly static DicomTagSQ ListOfUIDMatchingSequence = new DicomTagSQ(0x0008, 0x0411);

        ///<summary>(0008,0412) VR=SQ VM=1 Empty Value Matching Sequence</summary>
        public readonly static DicomTagSQ EmptyValueMatchingSequence = new DicomTagSQ(0x0008, 0x0412);

        ///<summary>(0008,0413) VR=SQ VM=1 General Matching Sequence</summary>
        public readonly static DicomTagSQ GeneralMatchingSequence = new DicomTagSQ(0x0008, 0x0413);

        ///<summary>(0008,0414) VR=US VM=1 Requested Status Interval</summary>
        public readonly static DicomTagUS RequestedStatusInterval = new DicomTagUS(0x0008, 0x0414);

        ///<summary>(0008,0415) VR=CS VM=1 Retain Instances</summary>
        public readonly static DicomTagCS RetainInstances = new DicomTagCS(0x0008, 0x0415);

        ///<summary>(0008,0416) VR=DT VM=1 Expiration DateTime</summary>
        public readonly static DicomTagDT ExpirationDateTime = new DicomTagDT(0x0008, 0x0416);

        ///<summary>(0008,0417) VR=CS VM=1 Transaction Status</summary>
        public readonly static DicomTagCS TransactionStatus = new DicomTagCS(0x0008, 0x0417);

        ///<summary>(0008,0418) VR=LT VM=1 Transaction Status Comment</summary>
        public readonly static DicomTagLT TransactionStatusComment = new DicomTagLT(0x0008, 0x0418);

        ///<summary>(0008,0419) VR=SQ VM=1 File Set Access Sequence</summary>
        public readonly static DicomTagSQ FileSetAccessSequence = new DicomTagSQ(0x0008, 0x0419);

        ///<summary>(0008,041A) VR=SQ VM=1 File Access Sequence</summary>
        public readonly static DicomTagSQ FileAccessSequence = new DicomTagSQ(0x0008, 0x041A);

        ///<summary>(0008,041B) VR=OB VM=1 Record Key</summary>
        public readonly static DicomTagOB RecordKey = new DicomTagOB(0x0008, 0x041B);

        ///<summary>(0008,041C) VR=OB VM=1 Prior Record Key</summary>
        public readonly static DicomTagOB PriorRecordKey = new DicomTagOB(0x0008, 0x041C);

        ///<summary>(0008,041D) VR=SQ VM=1 Metadata Sequence</summary>
        public readonly static DicomTagSQ MetadataSequence = new DicomTagSQ(0x0008, 0x041D);

        ///<summary>(0008,041E) VR=SQ VM=1 Updated Metadata Sequence</summary>
        public readonly static DicomTagSQ UpdatedMetadataSequence = new DicomTagSQ(0x0008, 0x041E);

        ///<summary>(0008,041F) VR=DT VM=1 Study Update DateTime</summary>
        public readonly static DicomTagDT StudyUpdateDateTime = new DicomTagDT(0x0008, 0x041F);

        ///<summary>(0008,0420) VR=SQ VM=1 Inventory Access End Points Sequence</summary>
        public readonly static DicomTagSQ InventoryAccessEndPointsSequence = new DicomTagSQ(0x0008, 0x0420);

        ///<summary>(0008,0421) VR=SQ VM=1 Study Access End Points Sequence</summary>
        public readonly static DicomTagSQ StudyAccessEndPointsSequence = new DicomTagSQ(0x0008, 0x0421);

        ///<summary>(0008,0422) VR=SQ VM=1 Incorporated Inventory Instance Sequence</summary>
        public readonly static DicomTagSQ IncorporatedInventoryInstanceSequence = new DicomTagSQ(0x0008, 0x0422);

        ///<summary>(0008,0423) VR=SQ VM=1 Inventoried Studies Sequence</summary>
        public readonly static DicomTagSQ InventoriedStudiesSequence = new DicomTagSQ(0x0008, 0x0423);

        ///<summary>(0008,0424) VR=SQ VM=1 Inventoried Series Sequence</summary>
        public readonly static DicomTagSQ InventoriedSeriesSequence = new DicomTagSQ(0x0008, 0x0424);

        ///<summary>(0008,0425) VR=SQ VM=1 Inventoried Instances Sequence</summary>
        public readonly static DicomTagSQ InventoriedInstancesSequence = new DicomTagSQ(0x0008, 0x0425);

        ///<summary>(0008,0426) VR=CS VM=1 Inventory Completion Status</summary>
        public readonly static DicomTagCS InventoryCompletionStatus = new DicomTagCS(0x0008, 0x0426);

        ///<summary>(0008,0427) VR=UL VM=1 Number of Study Records in Instance</summary>
        public readonly static DicomTagUL NumberOfStudyRecordsInInstance = new DicomTagUL(0x0008, 0x0427);

        ///<summary>(0008,0428) VR=UV VM=1 Total Number of Study Records</summary>
        public readonly static DicomTagUV TotalNumberOfStudyRecords = new DicomTagUV(0x0008, 0x0428);

        ///<summary>(0008,0429) VR=UV VM=1 Maximum Number of Records</summary>
        public readonly static DicomTagUV MaximumNumberOfRecords = new DicomTagUV(0x0008, 0x0429);

        ///<summary>(0008,1000) VR=AE VM=1 Network ID (RETIRED)</summary>
        public readonly static DicomTagAE NetworkIDRETIRED = new DicomTagAE(0x0008, 0x1000);

        ///<summary>(0008,1010) VR=SH VM=1 Station Name</summary>
        public readonly static DicomTagSH StationName = new DicomTagSH(0x0008, 0x1010);

        ///<summary>(0008,1030) VR=LO VM=1 Study Description</summary>
        public readonly static DicomTagLO StudyDescription = new DicomTagLO(0x0008, 0x1030);

        ///<summary>(0008,1032) VR=SQ VM=1 Procedure Code Sequence</summary>
        public readonly static DicomTagSQ ProcedureCodeSequence = new DicomTagSQ(0x0008, 0x1032);

        ///<summary>(0008,103E) VR=LO VM=1 Series Description</summary>
        public readonly static DicomTagLO SeriesDescription = new DicomTagLO(0x0008, 0x103E);

        ///<summary>(0008,103F) VR=SQ VM=1 Series Description Code Sequence</summary>
        public readonly static DicomTagSQ SeriesDescriptionCodeSequence = new DicomTagSQ(0x0008, 0x103F);

        ///<summary>(0008,1040) VR=LO VM=1 Institutional Department Name</summary>
        public readonly static DicomTagLO InstitutionalDepartmentName = new DicomTagLO(0x0008, 0x1040);

        ///<summary>(0008,1041) VR=SQ VM=1 Institutional Department Type Code Sequence</summary>
        public readonly static DicomTagSQ InstitutionalDepartmentTypeCodeSequence = new DicomTagSQ(0x0008, 0x1041);

        ///<summary>(0008,1048) VR=PN VM=1-n Physician(s) of Record</summary>
        public readonly static DicomTagPNs PhysiciansOfRecord = new DicomTagPNs(0x0008, 0x1048);

        ///<summary>(0008,1049) VR=SQ VM=1 Physician(s) of Record Identification Sequence</summary>
        public readonly static DicomTagSQ PhysiciansOfRecordIdentificationSequence = new DicomTagSQ(0x0008, 0x1049);

        ///<summary>(0008,1050) VR=PN VM=1-n Performing Physician's Name</summary>
        public readonly static DicomTagPNs PerformingPhysicianName = new DicomTagPNs(0x0008, 0x1050);

        ///<summary>(0008,1052) VR=SQ VM=1 Performing Physician Identification Sequence</summary>
        public readonly static DicomTagSQ PerformingPhysicianIdentificationSequence = new DicomTagSQ(0x0008, 0x1052);

        ///<summary>(0008,1060) VR=PN VM=1-n Name of Physician(s) Reading Study</summary>
        public readonly static DicomTagPNs NameOfPhysiciansReadingStudy = new DicomTagPNs(0x0008, 0x1060);

        ///<summary>(0008,1062) VR=SQ VM=1 Physician(s) Reading Study Identification Sequence</summary>
        public readonly static DicomTagSQ PhysiciansReadingStudyIdentificationSequence = new DicomTagSQ(0x0008, 0x1062);

        ///<summary>(0008,1070) VR=PN VM=1-n Operators' Name</summary>
        public readonly static DicomTagPNs OperatorsName = new DicomTagPNs(0x0008, 0x1070);

        ///<summary>(0008,1072) VR=SQ VM=1 Operator Identification Sequence</summary>
        public readonly static DicomTagSQ OperatorIdentificationSequence = new DicomTagSQ(0x0008, 0x1072);

        ///<summary>(0008,1080) VR=LO VM=1-n Admitting Diagnoses Description</summary>
        public readonly static DicomTagLOs AdmittingDiagnosesDescription = new DicomTagLOs(0x0008, 0x1080);

        ///<summary>(0008,1084) VR=SQ VM=1 Admitting Diagnoses Code Sequence</summary>
        public readonly static DicomTagSQ AdmittingDiagnosesCodeSequence = new DicomTagSQ(0x0008, 0x1084);

        ///<summary>(0008,1088) VR=LO VM=1 Pyramid Description</summary>
        public readonly static DicomTagLO PyramidDescription = new DicomTagLO(0x0008, 0x1088);

        ///<summary>(0008,1090) VR=LO VM=1 Manufacturer's Model Name</summary>
        public readonly static DicomTagLO ManufacturerModelName = new DicomTagLO(0x0008, 0x1090);

        ///<summary>(0008,1100) VR=SQ VM=1 Referenced Results Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedResultsSequenceRETIRED = new DicomTagSQ(0x0008, 0x1100);

        ///<summary>(0008,1110) VR=SQ VM=1 Referenced Study Sequence</summary>
        public readonly static DicomTagSQ ReferencedStudySequence = new DicomTagSQ(0x0008, 0x1110);

        ///<summary>(0008,1111) VR=SQ VM=1 Referenced Performed Procedure Step Sequence</summary>
        public readonly static DicomTagSQ ReferencedPerformedProcedureStepSequence = new DicomTagSQ(0x0008, 0x1111);

        ///<summary>(0008,1112) VR=SQ VM=1 Referenced Instances by SOP Class Sequence</summary>
        public readonly static DicomTagSQ ReferencedInstancesBySOPClassSequence = new DicomTagSQ(0x0008, 0x1112);

        ///<summary>(0008,1115) VR=SQ VM=1 Referenced Series Sequence</summary>
        public readonly static DicomTagSQ ReferencedSeriesSequence = new DicomTagSQ(0x0008, 0x1115);

        ///<summary>(0008,1120) VR=SQ VM=1 Referenced Patient Sequence</summary>
        public readonly static DicomTagSQ ReferencedPatientSequence = new DicomTagSQ(0x0008, 0x1120);

        ///<summary>(0008,1125) VR=SQ VM=1 Referenced Visit Sequence</summary>
        public readonly static DicomTagSQ ReferencedVisitSequence = new DicomTagSQ(0x0008, 0x1125);

        ///<summary>(0008,1130) VR=SQ VM=1 Referenced Overlay Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedOverlaySequenceRETIRED = new DicomTagSQ(0x0008, 0x1130);

        ///<summary>(0008,1134) VR=SQ VM=1 Referenced Stereometric Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedStereometricInstanceSequence = new DicomTagSQ(0x0008, 0x1134);

        ///<summary>(0008,113A) VR=SQ VM=1 Referenced Waveform Sequence</summary>
        public readonly static DicomTagSQ ReferencedWaveformSequence = new DicomTagSQ(0x0008, 0x113A);

        ///<summary>(0008,1140) VR=SQ VM=1 Referenced Image Sequence</summary>
        public readonly static DicomTagSQ ReferencedImageSequence = new DicomTagSQ(0x0008, 0x1140);

        ///<summary>(0008,1145) VR=SQ VM=1 Referenced Curve Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedCurveSequenceRETIRED = new DicomTagSQ(0x0008, 0x1145);

        ///<summary>(0008,114A) VR=SQ VM=1 Referenced Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedInstanceSequence = new DicomTagSQ(0x0008, 0x114A);

        ///<summary>(0008,114B) VR=SQ VM=1 Referenced Real World Value Mapping Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedRealWorldValueMappingInstanceSequence = new DicomTagSQ(0x0008, 0x114B);

        ///<summary>(0008,114C) VR=SQ VM=1 Referenced Segmentation Sequence</summary>
        public readonly static DicomTagSQ ReferencedSegmentationSequence = new DicomTagSQ(0x0008, 0x114C);

        ///<summary>(0008,114D) VR=SQ VM=1 Referenced Surface Segmentation Sequence</summary>
        public readonly static DicomTagSQ ReferencedSurfaceSegmentationSequence = new DicomTagSQ(0x0008, 0x114D);

        ///<summary>(0008,1150) VR=UI VM=1 Referenced SOP Class UID</summary>
        public readonly static DicomTagUI ReferencedSOPClassUID = new DicomTagUI(0x0008, 0x1150);

        ///<summary>(0008,1155) VR=UI VM=1 Referenced SOP Instance UID</summary>
        public readonly static DicomTagUI ReferencedSOPInstanceUID = new DicomTagUI(0x0008, 0x1155);

        ///<summary>(0008,1156) VR=SQ VM=1 Definition Source Sequence</summary>
        public readonly static DicomTagSQ DefinitionSourceSequence = new DicomTagSQ(0x0008, 0x1156);

        ///<summary>(0008,115A) VR=UI VM=1-n SOP Classes Supported</summary>
        public readonly static DicomTagUIs SOPClassesSupported = new DicomTagUIs(0x0008, 0x115A);

        ///<summary>(0008,1160) VR=IS VM=1-n Referenced Frame Number</summary>
        public readonly static DicomTagISs ReferencedFrameNumber = new DicomTagISs(0x0008, 0x1160);

        ///<summary>(0008,1161) VR=UL VM=1-n Simple Frame List</summary>
        public readonly static DicomTagULs SimpleFrameList = new DicomTagULs(0x0008, 0x1161);

        ///<summary>(0008,1162) VR=UL VM=3-3n Calculated Frame List</summary>
        public readonly static DicomTagULs CalculatedFrameList = new DicomTagULs(0x0008, 0x1162);

        ///<summary>(0008,1163) VR=FD VM=2 Time Range</summary>
        public readonly static DicomTagFDs TimeRange = new DicomTagFDs(0x0008, 0x1163);

        ///<summary>(0008,1164) VR=SQ VM=1 Frame Extraction Sequence</summary>
        public readonly static DicomTagSQ FrameExtractionSequence = new DicomTagSQ(0x0008, 0x1164);

        ///<summary>(0008,1167) VR=UI VM=1 Multi-frame Source SOP Instance UID</summary>
        public readonly static DicomTagUI MultiFrameSourceSOPInstanceUID = new DicomTagUI(0x0008, 0x1167);

        ///<summary>(0008,1190) VR=UR VM=1 Retrieve URL</summary>
        public readonly static DicomTagUR RetrieveURL = new DicomTagUR(0x0008, 0x1190);

        ///<summary>(0008,1195) VR=UI VM=1 Transaction UID</summary>
        public readonly static DicomTagUI TransactionUID = new DicomTagUI(0x0008, 0x1195);

        ///<summary>(0008,1196) VR=US VM=1 Warning Reason</summary>
        public readonly static DicomTagUS WarningReason = new DicomTagUS(0x0008, 0x1196);

        ///<summary>(0008,1197) VR=US VM=1 Failure Reason</summary>
        public readonly static DicomTagUS FailureReason = new DicomTagUS(0x0008, 0x1197);

        ///<summary>(0008,1198) VR=SQ VM=1 Failed SOP Sequence</summary>
        public readonly static DicomTagSQ FailedSOPSequence = new DicomTagSQ(0x0008, 0x1198);

        ///<summary>(0008,1199) VR=SQ VM=1 Referenced SOP Sequence</summary>
        public readonly static DicomTagSQ ReferencedSOPSequence = new DicomTagSQ(0x0008, 0x1199);

        ///<summary>(0008,119A) VR=SQ VM=1 Other Failures Sequence</summary>
        public readonly static DicomTagSQ OtherFailuresSequence = new DicomTagSQ(0x0008, 0x119A);

        ///<summary>(0008,119B) VR=SQ VM=1 Failed Study Sequence</summary>
        public readonly static DicomTagSQ FailedStudySequence = new DicomTagSQ(0x0008, 0x119B);

        ///<summary>(0008,1200) VR=SQ VM=1 Studies Containing Other Referenced Instances Sequence</summary>
        public readonly static DicomTagSQ StudiesContainingOtherReferencedInstancesSequence = new DicomTagSQ(0x0008, 0x1200);

        ///<summary>(0008,1250) VR=SQ VM=1 Related Series Sequence</summary>
        public readonly static DicomTagSQ RelatedSeriesSequence = new DicomTagSQ(0x0008, 0x1250);

        ///<summary>(0008,1301) VR=SQ VM=1 Principal Diagnosis Code Sequence</summary>
        public readonly static DicomTagSQ PrincipalDiagnosisCodeSequence = new DicomTagSQ(0x0008, 0x1301);

        ///<summary>(0008,1302) VR=SQ VM=1 Primary Diagnosis Code Sequence</summary>
        public readonly static DicomTagSQ PrimaryDiagnosisCodeSequence = new DicomTagSQ(0x0008, 0x1302);

        ///<summary>(0008,1303) VR=SQ VM=1 Secondary Diagnoses Code Sequence</summary>
        public readonly static DicomTagSQ SecondaryDiagnosesCodeSequence = new DicomTagSQ(0x0008, 0x1303);

        ///<summary>(0008,1304) VR=SQ VM=1 Histological Diagnoses Code Sequence</summary>
        public readonly static DicomTagSQ HistologicalDiagnosesCodeSequence = new DicomTagSQ(0x0008, 0x1304);

        ///<summary>(0008,2110) VR=CS VM=1 Lossy Image Compression (Retired) (RETIRED)</summary>
        public readonly static DicomTagCS LossyImageCompressionRetiredRETIRED = new DicomTagCS(0x0008, 0x2110);

        ///<summary>(0008,2111) VR=ST VM=1 Derivation Description</summary>
        public readonly static DicomTagST DerivationDescription = new DicomTagST(0x0008, 0x2111);

        ///<summary>(0008,2112) VR=SQ VM=1 Source Image Sequence</summary>
        public readonly static DicomTagSQ SourceImageSequence = new DicomTagSQ(0x0008, 0x2112);

        ///<summary>(0008,2120) VR=SH VM=1 Stage Name</summary>
        public readonly static DicomTagSH StageName = new DicomTagSH(0x0008, 0x2120);

        ///<summary>(0008,2122) VR=IS VM=1 Stage Number</summary>
        public readonly static DicomTagIS StageNumber = new DicomTagIS(0x0008, 0x2122);

        ///<summary>(0008,2124) VR=IS VM=1 Number of Stages</summary>
        public readonly static DicomTagIS NumberOfStages = new DicomTagIS(0x0008, 0x2124);

        ///<summary>(0008,2127) VR=SH VM=1 View Name</summary>
        public readonly static DicomTagSH ViewName = new DicomTagSH(0x0008, 0x2127);

        ///<summary>(0008,2128) VR=IS VM=1 View Number</summary>
        public readonly static DicomTagIS ViewNumber = new DicomTagIS(0x0008, 0x2128);

        ///<summary>(0008,2129) VR=IS VM=1 Number of Event Timers</summary>
        public readonly static DicomTagIS NumberOfEventTimers = new DicomTagIS(0x0008, 0x2129);

        ///<summary>(0008,212A) VR=IS VM=1 Number of Views in Stage</summary>
        public readonly static DicomTagIS NumberOfViewsInStage = new DicomTagIS(0x0008, 0x212A);

        ///<summary>(0008,2130) VR=DS VM=1-n Event Elapsed Time(s)</summary>
        public readonly static DicomTagDSs EventElapsedTimes = new DicomTagDSs(0x0008, 0x2130);

        ///<summary>(0008,2132) VR=LO VM=1-n Event Timer Name(s)</summary>
        public readonly static DicomTagLOs EventTimerNames = new DicomTagLOs(0x0008, 0x2132);

        ///<summary>(0008,2133) VR=SQ VM=1 Event Timer Sequence</summary>
        public readonly static DicomTagSQ EventTimerSequence = new DicomTagSQ(0x0008, 0x2133);

        ///<summary>(0008,2134) VR=FD VM=1 Event Time Offset</summary>
        public readonly static DicomTagFD EventTimeOffset = new DicomTagFD(0x0008, 0x2134);

        ///<summary>(0008,2135) VR=SQ VM=1 Event Code Sequence</summary>
        public readonly static DicomTagSQ EventCodeSequence = new DicomTagSQ(0x0008, 0x2135);

        ///<summary>(0008,2142) VR=IS VM=1 Start Trim</summary>
        public readonly static DicomTagIS StartTrim = new DicomTagIS(0x0008, 0x2142);

        ///<summary>(0008,2143) VR=IS VM=1 Stop Trim</summary>
        public readonly static DicomTagIS StopTrim = new DicomTagIS(0x0008, 0x2143);

        ///<summary>(0008,2144) VR=IS VM=1 Recommended Display Frame Rate</summary>
        public readonly static DicomTagIS RecommendedDisplayFrameRate = new DicomTagIS(0x0008, 0x2144);

        ///<summary>(0008,2200) VR=CS VM=1 Transducer Position (RETIRED)</summary>
        public readonly static DicomTagCS TransducerPositionRETIRED = new DicomTagCS(0x0008, 0x2200);

        ///<summary>(0008,2204) VR=CS VM=1 Transducer Orientation (RETIRED)</summary>
        public readonly static DicomTagCS TransducerOrientationRETIRED = new DicomTagCS(0x0008, 0x2204);

        ///<summary>(0008,2208) VR=CS VM=1 Anatomic Structure (RETIRED)</summary>
        public readonly static DicomTagCS AnatomicStructureRETIRED = new DicomTagCS(0x0008, 0x2208);

        ///<summary>(0008,2218) VR=SQ VM=1 Anatomic Region Sequence</summary>
        public readonly static DicomTagSQ AnatomicRegionSequence = new DicomTagSQ(0x0008, 0x2218);

        ///<summary>(0008,2220) VR=SQ VM=1 Anatomic Region Modifier Sequence</summary>
        public readonly static DicomTagSQ AnatomicRegionModifierSequence = new DicomTagSQ(0x0008, 0x2220);

        ///<summary>(0008,2228) VR=SQ VM=1 Primary Anatomic Structure Sequence</summary>
        public readonly static DicomTagSQ PrimaryAnatomicStructureSequence = new DicomTagSQ(0x0008, 0x2228);

        ///<summary>(0008,2229) VR=SQ VM=1 Anatomic Structure, Space or Region Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ AnatomicStructureSpaceOrRegionSequenceRETIRED = new DicomTagSQ(0x0008, 0x2229);

        ///<summary>(0008,2230) VR=SQ VM=1 Primary Anatomic Structure Modifier Sequence</summary>
        public readonly static DicomTagSQ PrimaryAnatomicStructureModifierSequence = new DicomTagSQ(0x0008, 0x2230);

        ///<summary>(0008,2240) VR=SQ VM=1 Transducer Position Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ TransducerPositionSequenceRETIRED = new DicomTagSQ(0x0008, 0x2240);

        ///<summary>(0008,2242) VR=SQ VM=1 Transducer Position Modifier Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ TransducerPositionModifierSequenceRETIRED = new DicomTagSQ(0x0008, 0x2242);

        ///<summary>(0008,2244) VR=SQ VM=1 Transducer Orientation Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ TransducerOrientationSequenceRETIRED = new DicomTagSQ(0x0008, 0x2244);

        ///<summary>(0008,2246) VR=SQ VM=1 Transducer Orientation Modifier Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ TransducerOrientationModifierSequenceRETIRED = new DicomTagSQ(0x0008, 0x2246);

        ///<summary>(0008,2251) VR=SQ VM=1 Anatomic Structure Space Or Region Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ AnatomicStructureSpaceOrRegionCodeSequenceTrialRETIRED = new DicomTagSQ(0x0008, 0x2251);

        ///<summary>(0008,2253) VR=SQ VM=1 Anatomic Portal Of Entrance Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ AnatomicPortalOfEntranceCodeSequenceTrialRETIRED = new DicomTagSQ(0x0008, 0x2253);

        ///<summary>(0008,2255) VR=SQ VM=1 Anatomic Approach Direction Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ AnatomicApproachDirectionCodeSequenceTrialRETIRED = new DicomTagSQ(0x0008, 0x2255);

        ///<summary>(0008,2256) VR=ST VM=1 Anatomic Perspective Description (Trial) (RETIRED)</summary>
        public readonly static DicomTagST AnatomicPerspectiveDescriptionTrialRETIRED = new DicomTagST(0x0008, 0x2256);

        ///<summary>(0008,2257) VR=SQ VM=1 Anatomic Perspective Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ AnatomicPerspectiveCodeSequenceTrialRETIRED = new DicomTagSQ(0x0008, 0x2257);

        ///<summary>(0008,2258) VR=ST VM=1 Anatomic Location Of Examining Instrument Description (Trial) (RETIRED)</summary>
        public readonly static DicomTagST AnatomicLocationOfExaminingInstrumentDescriptionTrialRETIRED = new DicomTagST(0x0008, 0x2258);

        ///<summary>(0008,2259) VR=SQ VM=1 Anatomic Location Of Examining Instrument Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ AnatomicLocationOfExaminingInstrumentCodeSequenceTrialRETIRED = new DicomTagSQ(0x0008, 0x2259);

        ///<summary>(0008,225A) VR=SQ VM=1 Anatomic Structure Space Or Region Modifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ AnatomicStructureSpaceOrRegionModifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0008, 0x225A);

        ///<summary>(0008,225C) VR=SQ VM=1 On Axis Background Anatomic Structure Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ OnAxisBackgroundAnatomicStructureCodeSequenceTrialRETIRED = new DicomTagSQ(0x0008, 0x225C);

        ///<summary>(0008,3001) VR=SQ VM=1 Alternate Representation Sequence</summary>
        public readonly static DicomTagSQ AlternateRepresentationSequence = new DicomTagSQ(0x0008, 0x3001);

        ///<summary>(0008,3002) VR=UI VM=1-n Available Transfer Syntax UID</summary>
        public readonly static DicomTagUIs AvailableTransferSyntaxUID = new DicomTagUIs(0x0008, 0x3002);

        ///<summary>(0008,3010) VR=UI VM=1-n Irradiation Event UID</summary>
        public readonly static DicomTagUIs IrradiationEventUID = new DicomTagUIs(0x0008, 0x3010);

        ///<summary>(0008,3011) VR=SQ VM=1 Source Irradiation Event Sequence</summary>
        public readonly static DicomTagSQ SourceIrradiationEventSequence = new DicomTagSQ(0x0008, 0x3011);

        ///<summary>(0008,3012) VR=UI VM=1 Radiopharmaceutical Administration Event UID</summary>
        public readonly static DicomTagUI RadiopharmaceuticalAdministrationEventUID = new DicomTagUI(0x0008, 0x3012);

        ///<summary>(0008,4000) VR=LT VM=1 Identifying Comments (RETIRED)</summary>
        public readonly static DicomTagLT IdentifyingCommentsRETIRED = new DicomTagLT(0x0008, 0x4000);

        ///<summary>(0008,9007) VR=CS VM=4-5 Frame Type</summary>
        public readonly static DicomTagCSs FrameType = new DicomTagCSs(0x0008, 0x9007);

        ///<summary>(0008,9092) VR=SQ VM=1 Referenced Image Evidence Sequence</summary>
        public readonly static DicomTagSQ ReferencedImageEvidenceSequence = new DicomTagSQ(0x0008, 0x9092);

        ///<summary>(0008,9121) VR=SQ VM=1 Referenced Raw Data Sequence</summary>
        public readonly static DicomTagSQ ReferencedRawDataSequence = new DicomTagSQ(0x0008, 0x9121);

        ///<summary>(0008,9123) VR=UI VM=1 Creator-Version UID</summary>
        public readonly static DicomTagUI CreatorVersionUID = new DicomTagUI(0x0008, 0x9123);

        ///<summary>(0008,9124) VR=SQ VM=1 Derivation Image Sequence</summary>
        public readonly static DicomTagSQ DerivationImageSequence = new DicomTagSQ(0x0008, 0x9124);

        ///<summary>(0008,9154) VR=SQ VM=1 Source Image Evidence Sequence</summary>
        public readonly static DicomTagSQ SourceImageEvidenceSequence = new DicomTagSQ(0x0008, 0x9154);

        ///<summary>(0008,9205) VR=CS VM=1 Pixel Presentation</summary>
        public readonly static DicomTagCS PixelPresentation = new DicomTagCS(0x0008, 0x9205);

        ///<summary>(0008,9206) VR=CS VM=1 Volumetric Properties</summary>
        public readonly static DicomTagCS VolumetricProperties = new DicomTagCS(0x0008, 0x9206);

        ///<summary>(0008,9207) VR=CS VM=1 Volume Based Calculation Technique</summary>
        public readonly static DicomTagCS VolumeBasedCalculationTechnique = new DicomTagCS(0x0008, 0x9207);

        ///<summary>(0008,9208) VR=CS VM=1 Complex Image Component</summary>
        public readonly static DicomTagCS ComplexImageComponent = new DicomTagCS(0x0008, 0x9208);

        ///<summary>(0008,9209) VR=CS VM=1 Acquisition Contrast</summary>
        public readonly static DicomTagCS AcquisitionContrast = new DicomTagCS(0x0008, 0x9209);

        ///<summary>(0008,9215) VR=SQ VM=1 Derivation Code Sequence</summary>
        public readonly static DicomTagSQ DerivationCodeSequence = new DicomTagSQ(0x0008, 0x9215);

        ///<summary>(0008,9237) VR=SQ VM=1 Referenced Presentation State Sequence</summary>
        public readonly static DicomTagSQ ReferencedPresentationStateSequence = new DicomTagSQ(0x0008, 0x9237);

        ///<summary>(0008,9410) VR=SQ VM=1 Referenced Other Plane Sequence</summary>
        public readonly static DicomTagSQ ReferencedOtherPlaneSequence = new DicomTagSQ(0x0008, 0x9410);

        ///<summary>(0008,9458) VR=SQ VM=1 Frame Display Sequence</summary>
        public readonly static DicomTagSQ FrameDisplaySequence = new DicomTagSQ(0x0008, 0x9458);

        ///<summary>(0008,9459) VR=FL VM=1 Recommended Display Frame Rate in Float</summary>
        public readonly static DicomTagFL RecommendedDisplayFrameRateInFloat = new DicomTagFL(0x0008, 0x9459);

        ///<summary>(0008,9460) VR=CS VM=1 Skip Frame Range Flag</summary>
        public readonly static DicomTagCS SkipFrameRangeFlag = new DicomTagCS(0x0008, 0x9460);

        ///<summary>(0010,0010) VR=PN VM=1 Patient's Name</summary>
        public readonly static DicomTagPN PatientName = new DicomTagPN(0x0010, 0x0010);

        ///<summary>(0010,0011) VR=SQ VM=1 Person Names to Use Sequence</summary>
        public readonly static DicomTagSQ PersonNamesToUseSequence = new DicomTagSQ(0x0010, 0x0011);

        ///<summary>(0010,0012) VR=LT VM=1 Name to Use</summary>
        public readonly static DicomTagLT NameToUse = new DicomTagLT(0x0010, 0x0012);

        ///<summary>(0010,0013) VR=UT VM=1 Name to Use Comment</summary>
        public readonly static DicomTagUT NameToUseComment = new DicomTagUT(0x0010, 0x0013);

        ///<summary>(0010,0014) VR=SQ VM=1 Third Person Pronouns Sequence</summary>
        public readonly static DicomTagSQ ThirdPersonPronounsSequence = new DicomTagSQ(0x0010, 0x0014);

        ///<summary>(0010,0015) VR=SQ VM=1 Pronoun Code Sequence</summary>
        public readonly static DicomTagSQ PronounCodeSequence = new DicomTagSQ(0x0010, 0x0015);

        ///<summary>(0010,0016) VR=UT VM=1 Pronoun Comment</summary>
        public readonly static DicomTagUT PronounComment = new DicomTagUT(0x0010, 0x0016);

        ///<summary>(0010,0020) VR=LO VM=1 Patient ID</summary>
        public readonly static DicomTagLO PatientID = new DicomTagLO(0x0010, 0x0020);

        ///<summary>(0010,0021) VR=LO VM=1 Issuer of Patient ID</summary>
        public readonly static DicomTagLO IssuerOfPatientID = new DicomTagLO(0x0010, 0x0021);

        ///<summary>(0010,0022) VR=CS VM=1 Type of Patient ID</summary>
        public readonly static DicomTagCS TypeOfPatientID = new DicomTagCS(0x0010, 0x0022);

        ///<summary>(0010,0024) VR=SQ VM=1 Issuer of Patient ID Qualifiers Sequence</summary>
        public readonly static DicomTagSQ IssuerOfPatientIDQualifiersSequence = new DicomTagSQ(0x0010, 0x0024);

        ///<summary>(0010,0026) VR=SQ VM=1 Source Patient Group Identification Sequence</summary>
        public readonly static DicomTagSQ SourcePatientGroupIdentificationSequence = new DicomTagSQ(0x0010, 0x0026);

        ///<summary>(0010,0027) VR=SQ VM=1 Group of Patients Identification Sequence</summary>
        public readonly static DicomTagSQ GroupOfPatientsIdentificationSequence = new DicomTagSQ(0x0010, 0x0027);

        ///<summary>(0010,0028) VR=US VM=3 Subject Relative Position in Image</summary>
        public readonly static DicomTagUSs SubjectRelativePositionInImage = new DicomTagUSs(0x0010, 0x0028);

        ///<summary>(0010,0030) VR=DA VM=1 Patient's Birth Date</summary>
        public readonly static DicomTagDA PatientBirthDate = new DicomTagDA(0x0010, 0x0030);

        ///<summary>(0010,0032) VR=TM VM=1 Patient's Birth Time</summary>
        public readonly static DicomTagTM PatientBirthTime = new DicomTagTM(0x0010, 0x0032);

        ///<summary>(0010,0033) VR=LO VM=1 Patient's Birth Date in Alternative Calendar</summary>
        public readonly static DicomTagLO PatientBirthDateInAlternativeCalendar = new DicomTagLO(0x0010, 0x0033);

        ///<summary>(0010,0034) VR=LO VM=1 Patient's Death Date in Alternative Calendar</summary>
        public readonly static DicomTagLO PatientDeathDateInAlternativeCalendar = new DicomTagLO(0x0010, 0x0034);

        ///<summary>(0010,0035) VR=CS VM=1 Patient's Alternative Calendar</summary>
        public readonly static DicomTagCS PatientAlternativeCalendar = new DicomTagCS(0x0010, 0x0035);

        ///<summary>(0010,0040) VR=CS VM=1 Patient's Sex</summary>
        public readonly static DicomTagCS PatientSex = new DicomTagCS(0x0010, 0x0040);

        ///<summary>(0010,0041) VR=SQ VM=1 Gender Identity Sequence</summary>
        public readonly static DicomTagSQ GenderIdentitySequence = new DicomTagSQ(0x0010, 0x0041);

        ///<summary>(0010,0042) VR=UT VM=1 Sex Parameters for Clinical Use Category Comment</summary>
        public readonly static DicomTagUT SexParametersForClinicalUseCategoryComment = new DicomTagUT(0x0010, 0x0042);

        ///<summary>(0010,0043) VR=SQ VM=1 Sex Parameters for Clinical Use Category Sequence</summary>
        public readonly static DicomTagSQ SexParametersForClinicalUseCategorySequence = new DicomTagSQ(0x0010, 0x0043);

        ///<summary>(0010,0044) VR=SQ VM=1 Gender Identity Code Sequence</summary>
        public readonly static DicomTagSQ GenderIdentityCodeSequence = new DicomTagSQ(0x0010, 0x0044);

        ///<summary>(0010,0045) VR=UT VM=1 Gender Identity Comment</summary>
        public readonly static DicomTagUT GenderIdentityComment = new DicomTagUT(0x0010, 0x0045);

        ///<summary>(0010,0046) VR=SQ VM=1 Sex Parameters for Clinical Use Category Code Sequence</summary>
        public readonly static DicomTagSQ SexParametersForClinicalUseCategoryCodeSequence = new DicomTagSQ(0x0010, 0x0046);

        ///<summary>(0010,0047) VR=UR VM=1-n Sex Parameters for Clinical Use Category Reference</summary>
        public readonly static DicomTagURs SexParametersForClinicalUseCategoryReference = new DicomTagURs(0x0010, 0x0047);

        ///<summary>(0010,0050) VR=SQ VM=1 Patient's Insurance Plan Code Sequence</summary>
        public readonly static DicomTagSQ PatientInsurancePlanCodeSequence = new DicomTagSQ(0x0010, 0x0050);

        ///<summary>(0010,0101) VR=SQ VM=1 Patient's Primary Language Code Sequence</summary>
        public readonly static DicomTagSQ PatientPrimaryLanguageCodeSequence = new DicomTagSQ(0x0010, 0x0101);

        ///<summary>(0010,0102) VR=SQ VM=1 Patient's Primary Language Modifier Code Sequence</summary>
        public readonly static DicomTagSQ PatientPrimaryLanguageModifierCodeSequence = new DicomTagSQ(0x0010, 0x0102);

        ///<summary>(0010,0200) VR=CS VM=1 Quality Control Subject</summary>
        public readonly static DicomTagCS QualityControlSubject = new DicomTagCS(0x0010, 0x0200);

        ///<summary>(0010,0201) VR=SQ VM=1 Quality Control Subject Type Code Sequence</summary>
        public readonly static DicomTagSQ QualityControlSubjectTypeCodeSequence = new DicomTagSQ(0x0010, 0x0201);

        ///<summary>(0010,0212) VR=UC VM=1 Strain Description</summary>
        public readonly static DicomTagUC StrainDescription = new DicomTagUC(0x0010, 0x0212);

        ///<summary>(0010,0213) VR=LO VM=1 Strain Nomenclature</summary>
        public readonly static DicomTagLO StrainNomenclature = new DicomTagLO(0x0010, 0x0213);

        ///<summary>(0010,0214) VR=LO VM=1 Strain Stock Number</summary>
        public readonly static DicomTagLO StrainStockNumber = new DicomTagLO(0x0010, 0x0214);

        ///<summary>(0010,0215) VR=SQ VM=1 Strain Source Registry Code Sequence</summary>
        public readonly static DicomTagSQ StrainSourceRegistryCodeSequence = new DicomTagSQ(0x0010, 0x0215);

        ///<summary>(0010,0216) VR=SQ VM=1 Strain Stock Sequence</summary>
        public readonly static DicomTagSQ StrainStockSequence = new DicomTagSQ(0x0010, 0x0216);

        ///<summary>(0010,0217) VR=LO VM=1 Strain Source</summary>
        public readonly static DicomTagLO StrainSource = new DicomTagLO(0x0010, 0x0217);

        ///<summary>(0010,0218) VR=UT VM=1 Strain Additional Information</summary>
        public readonly static DicomTagUT StrainAdditionalInformation = new DicomTagUT(0x0010, 0x0218);

        ///<summary>(0010,0219) VR=SQ VM=1 Strain Code Sequence</summary>
        public readonly static DicomTagSQ StrainCodeSequence = new DicomTagSQ(0x0010, 0x0219);

        ///<summary>(0010,0221) VR=SQ VM=1 Genetic Modifications Sequence</summary>
        public readonly static DicomTagSQ GeneticModificationsSequence = new DicomTagSQ(0x0010, 0x0221);

        ///<summary>(0010,0222) VR=UC VM=1 Genetic Modifications Description</summary>
        public readonly static DicomTagUC GeneticModificationsDescription = new DicomTagUC(0x0010, 0x0222);

        ///<summary>(0010,0223) VR=LO VM=1 Genetic Modifications Nomenclature</summary>
        public readonly static DicomTagLO GeneticModificationsNomenclature = new DicomTagLO(0x0010, 0x0223);

        ///<summary>(0010,0229) VR=SQ VM=1 Genetic Modifications Code Sequence</summary>
        public readonly static DicomTagSQ GeneticModificationsCodeSequence = new DicomTagSQ(0x0010, 0x0229);

        ///<summary>(0010,1000) VR=LO VM=1-n Other Patient IDs (RETIRED)</summary>
        public readonly static DicomTagLOs OtherPatientIDsRETIRED = new DicomTagLOs(0x0010, 0x1000);

        ///<summary>(0010,1001) VR=PN VM=1-n Other Patient Names</summary>
        public readonly static DicomTagPNs OtherPatientNames = new DicomTagPNs(0x0010, 0x1001);

        ///<summary>(0010,1002) VR=SQ VM=1 Other Patient IDs Sequence</summary>
        public readonly static DicomTagSQ OtherPatientIDsSequence = new DicomTagSQ(0x0010, 0x1002);

        ///<summary>(0010,1005) VR=PN VM=1 Patient's Birth Name</summary>
        public readonly static DicomTagPN PatientBirthName = new DicomTagPN(0x0010, 0x1005);

        ///<summary>(0010,1010) VR=AS VM=1 Patient's Age</summary>
        public readonly static DicomTagAS PatientAge = new DicomTagAS(0x0010, 0x1010);

        ///<summary>(0010,1020) VR=DS VM=1 Patient's Size</summary>
        public readonly static DicomTagDS PatientSize = new DicomTagDS(0x0010, 0x1020);

        ///<summary>(0010,1021) VR=SQ VM=1 Patient's Size Code Sequence</summary>
        public readonly static DicomTagSQ PatientSizeCodeSequence = new DicomTagSQ(0x0010, 0x1021);

        ///<summary>(0010,1022) VR=DS VM=1 Patient's Body Mass Index</summary>
        public readonly static DicomTagDS PatientBodyMassIndex = new DicomTagDS(0x0010, 0x1022);

        ///<summary>(0010,1023) VR=DS VM=1 Measured AP Dimension</summary>
        public readonly static DicomTagDS MeasuredAPDimension = new DicomTagDS(0x0010, 0x1023);

        ///<summary>(0010,1024) VR=DS VM=1 Measured Lateral Dimension</summary>
        public readonly static DicomTagDS MeasuredLateralDimension = new DicomTagDS(0x0010, 0x1024);

        ///<summary>(0010,1030) VR=DS VM=1 Patient's Weight</summary>
        public readonly static DicomTagDS PatientWeight = new DicomTagDS(0x0010, 0x1030);

        ///<summary>(0010,1040) VR=LO VM=1 Patient's Address</summary>
        public readonly static DicomTagLO PatientAddress = new DicomTagLO(0x0010, 0x1040);

        ///<summary>(0010,1050) VR=LO VM=1-n Insurance Plan Identification (RETIRED)</summary>
        public readonly static DicomTagLOs InsurancePlanIdentificationRETIRED = new DicomTagLOs(0x0010, 0x1050);

        ///<summary>(0010,1060) VR=PN VM=1 Patient's Mother's Birth Name</summary>
        public readonly static DicomTagPN PatientMotherBirthName = new DicomTagPN(0x0010, 0x1060);

        ///<summary>(0010,1080) VR=LO VM=1 Military Rank</summary>
        public readonly static DicomTagLO MilitaryRank = new DicomTagLO(0x0010, 0x1080);

        ///<summary>(0010,1081) VR=LO VM=1 Branch of Service</summary>
        public readonly static DicomTagLO BranchOfService = new DicomTagLO(0x0010, 0x1081);

        ///<summary>(0010,1090) VR=LO VM=1 Medical Record Locator (RETIRED)</summary>
        public readonly static DicomTagLO MedicalRecordLocatorRETIRED = new DicomTagLO(0x0010, 0x1090);

        ///<summary>(0010,1100) VR=SQ VM=1 Referenced Patient Photo Sequence</summary>
        public readonly static DicomTagSQ ReferencedPatientPhotoSequence = new DicomTagSQ(0x0010, 0x1100);

        ///<summary>(0010,2000) VR=LO VM=1-n Medical Alerts</summary>
        public readonly static DicomTagLOs MedicalAlerts = new DicomTagLOs(0x0010, 0x2000);

        ///<summary>(0010,2110) VR=LO VM=1-n Allergies</summary>
        public readonly static DicomTagLOs Allergies = new DicomTagLOs(0x0010, 0x2110);

        ///<summary>(0010,2150) VR=LO VM=1 Country of Residence</summary>
        public readonly static DicomTagLO CountryOfResidence = new DicomTagLO(0x0010, 0x2150);

        ///<summary>(0010,2152) VR=LO VM=1 Region of Residence</summary>
        public readonly static DicomTagLO RegionOfResidence = new DicomTagLO(0x0010, 0x2152);

        ///<summary>(0010,2154) VR=SH VM=1-n Patient's Telephone Numbers</summary>
        public readonly static DicomTagSHs PatientTelephoneNumbers = new DicomTagSHs(0x0010, 0x2154);

        ///<summary>(0010,2155) VR=LT VM=1 Patient's Telecom Information</summary>
        public readonly static DicomTagLT PatientTelecomInformation = new DicomTagLT(0x0010, 0x2155);

        ///<summary>(0010,2160) VR=SH VM=1 Ethnic Group (RETIRED)</summary>
        public readonly static DicomTagSH EthnicGroupRETIRED = new DicomTagSH(0x0010, 0x2160);

        ///<summary>(0010,2161) VR=SQ VM=1 Ethnic Group Code Sequence</summary>
        public readonly static DicomTagSQ EthnicGroupCodeSequence = new DicomTagSQ(0x0010, 0x2161);

        ///<summary>(0010,2162) VR=UC VM=1-n Ethnic Groups</summary>
        public readonly static DicomTagUCs EthnicGroups = new DicomTagUCs(0x0010, 0x2162);

        ///<summary>(0010,2180) VR=SH VM=1 Occupation</summary>
        public readonly static DicomTagSH Occupation = new DicomTagSH(0x0010, 0x2180);

        ///<summary>(0010,21A0) VR=CS VM=1 Smoking Status</summary>
        public readonly static DicomTagCS SmokingStatus = new DicomTagCS(0x0010, 0x21A0);

        ///<summary>(0010,21B0) VR=LT VM=1 Additional Patient History</summary>
        public readonly static DicomTagLT AdditionalPatientHistory = new DicomTagLT(0x0010, 0x21B0);

        ///<summary>(0010,21C0) VR=US VM=1 Pregnancy Status</summary>
        public readonly static DicomTagUS PregnancyStatus = new DicomTagUS(0x0010, 0x21C0);

        ///<summary>(0010,21D0) VR=DA VM=1 Last Menstrual Date</summary>
        public readonly static DicomTagDA LastMenstrualDate = new DicomTagDA(0x0010, 0x21D0);

        ///<summary>(0010,21F0) VR=LO VM=1 Patient's Religious Preference</summary>
        public readonly static DicomTagLO PatientReligiousPreference = new DicomTagLO(0x0010, 0x21F0);

        ///<summary>(0010,2201) VR=LO VM=1 Patient Species Description</summary>
        public readonly static DicomTagLO PatientSpeciesDescription = new DicomTagLO(0x0010, 0x2201);

        ///<summary>(0010,2202) VR=SQ VM=1 Patient Species Code Sequence</summary>
        public readonly static DicomTagSQ PatientSpeciesCodeSequence = new DicomTagSQ(0x0010, 0x2202);

        ///<summary>(0010,2203) VR=CS VM=1 Patient's Sex Neutered</summary>
        public readonly static DicomTagCS PatientSexNeutered = new DicomTagCS(0x0010, 0x2203);

        ///<summary>(0010,2210) VR=CS VM=1 Anatomical Orientation Type</summary>
        public readonly static DicomTagCS AnatomicalOrientationType = new DicomTagCS(0x0010, 0x2210);

        ///<summary>(0010,2292) VR=LO VM=1 Patient Breed Description</summary>
        public readonly static DicomTagLO PatientBreedDescription = new DicomTagLO(0x0010, 0x2292);

        ///<summary>(0010,2293) VR=SQ VM=1 Patient Breed Code Sequence</summary>
        public readonly static DicomTagSQ PatientBreedCodeSequence = new DicomTagSQ(0x0010, 0x2293);

        ///<summary>(0010,2294) VR=SQ VM=1 Breed Registration Sequence</summary>
        public readonly static DicomTagSQ BreedRegistrationSequence = new DicomTagSQ(0x0010, 0x2294);

        ///<summary>(0010,2295) VR=LO VM=1 Breed Registration Number</summary>
        public readonly static DicomTagLO BreedRegistrationNumber = new DicomTagLO(0x0010, 0x2295);

        ///<summary>(0010,2296) VR=SQ VM=1 Breed Registry Code Sequence</summary>
        public readonly static DicomTagSQ BreedRegistryCodeSequence = new DicomTagSQ(0x0010, 0x2296);

        ///<summary>(0010,2297) VR=PN VM=1 Responsible Person</summary>
        public readonly static DicomTagPN ResponsiblePerson = new DicomTagPN(0x0010, 0x2297);

        ///<summary>(0010,2298) VR=CS VM=1 Responsible Person Role</summary>
        public readonly static DicomTagCS ResponsiblePersonRole = new DicomTagCS(0x0010, 0x2298);

        ///<summary>(0010,2299) VR=LO VM=1 Responsible Organization</summary>
        public readonly static DicomTagLO ResponsibleOrganization = new DicomTagLO(0x0010, 0x2299);

        ///<summary>(0010,4000) VR=LT VM=1 Patient Comments</summary>
        public readonly static DicomTagLT PatientComments = new DicomTagLT(0x0010, 0x4000);

        ///<summary>(0010,9431) VR=FL VM=1 Examined Body Thickness</summary>
        public readonly static DicomTagFL ExaminedBodyThickness = new DicomTagFL(0x0010, 0x9431);

        ///<summary>(0012,0010) VR=LO VM=1 Clinical Trial Sponsor Name</summary>
        public readonly static DicomTagLO ClinicalTrialSponsorName = new DicomTagLO(0x0012, 0x0010);

        ///<summary>(0012,0020) VR=LO VM=1 Clinical Trial Protocol ID</summary>
        public readonly static DicomTagLO ClinicalTrialProtocolID = new DicomTagLO(0x0012, 0x0020);

        ///<summary>(0012,0021) VR=LO VM=1 Clinical Trial Protocol Name</summary>
        public readonly static DicomTagLO ClinicalTrialProtocolName = new DicomTagLO(0x0012, 0x0021);

        ///<summary>(0012,0022) VR=LO VM=1 Issuer of Clinical Trial Protocol ID</summary>
        public readonly static DicomTagLO IssuerOfClinicalTrialProtocolID = new DicomTagLO(0x0012, 0x0022);

        ///<summary>(0012,0023) VR=SQ VM=1 Other Clinical Trial Protocol IDs Sequence</summary>
        public readonly static DicomTagSQ OtherClinicalTrialProtocolIDsSequence = new DicomTagSQ(0x0012, 0x0023);

        ///<summary>(0012,0030) VR=LO VM=1 Clinical Trial Site ID</summary>
        public readonly static DicomTagLO ClinicalTrialSiteID = new DicomTagLO(0x0012, 0x0030);

        ///<summary>(0012,0031) VR=LO VM=1 Clinical Trial Site Name</summary>
        public readonly static DicomTagLO ClinicalTrialSiteName = new DicomTagLO(0x0012, 0x0031);

        ///<summary>(0012,0032) VR=LO VM=1 Issuer of Clinical Trial Site ID</summary>
        public readonly static DicomTagLO IssuerOfClinicalTrialSiteID = new DicomTagLO(0x0012, 0x0032);

        ///<summary>(0012,0040) VR=LO VM=1 Clinical Trial Subject ID</summary>
        public readonly static DicomTagLO ClinicalTrialSubjectID = new DicomTagLO(0x0012, 0x0040);

        ///<summary>(0012,0041) VR=LO VM=1 Issuer of Clinical Trial Subject ID</summary>
        public readonly static DicomTagLO IssuerOfClinicalTrialSubjectID = new DicomTagLO(0x0012, 0x0041);

        ///<summary>(0012,0042) VR=LO VM=1 Clinical Trial Subject Reading ID</summary>
        public readonly static DicomTagLO ClinicalTrialSubjectReadingID = new DicomTagLO(0x0012, 0x0042);

        ///<summary>(0012,0043) VR=LO VM=1 Issuer of Clinical Trial Subject Reading ID</summary>
        public readonly static DicomTagLO IssuerOfClinicalTrialSubjectReadingID = new DicomTagLO(0x0012, 0x0043);

        ///<summary>(0012,0050) VR=LO VM=1 Clinical Trial Time Point ID</summary>
        public readonly static DicomTagLO ClinicalTrialTimePointID = new DicomTagLO(0x0012, 0x0050);

        ///<summary>(0012,0051) VR=ST VM=1 Clinical Trial Time Point Description</summary>
        public readonly static DicomTagST ClinicalTrialTimePointDescription = new DicomTagST(0x0012, 0x0051);

        ///<summary>(0012,0052) VR=FD VM=1 Longitudinal Temporal Offset from Event</summary>
        public readonly static DicomTagFD LongitudinalTemporalOffsetFromEvent = new DicomTagFD(0x0012, 0x0052);

        ///<summary>(0012,0053) VR=CS VM=1 Longitudinal Temporal Event Type</summary>
        public readonly static DicomTagCS LongitudinalTemporalEventType = new DicomTagCS(0x0012, 0x0053);

        ///<summary>(0012,0054) VR=SQ VM=1 Clinical Trial Time Point Type Code Sequence</summary>
        public readonly static DicomTagSQ ClinicalTrialTimePointTypeCodeSequence = new DicomTagSQ(0x0012, 0x0054);

        ///<summary>(0012,0055) VR=LO VM=1 Issuer of Clinical Trial Time Point ID</summary>
        public readonly static DicomTagLO IssuerOfClinicalTrialTimePointID = new DicomTagLO(0x0012, 0x0055);

        ///<summary>(0012,0060) VR=LO VM=1 Clinical Trial Coordinating Center Name</summary>
        public readonly static DicomTagLO ClinicalTrialCoordinatingCenterName = new DicomTagLO(0x0012, 0x0060);

        ///<summary>(0012,0062) VR=CS VM=1 Patient Identity Removed</summary>
        public readonly static DicomTagCS PatientIdentityRemoved = new DicomTagCS(0x0012, 0x0062);

        ///<summary>(0012,0063) VR=LO VM=1-n De-identification Method</summary>
        public readonly static DicomTagLOs DeidentificationMethod = new DicomTagLOs(0x0012, 0x0063);

        ///<summary>(0012,0064) VR=SQ VM=1 De-identification Method Code Sequence</summary>
        public readonly static DicomTagSQ DeidentificationMethodCodeSequence = new DicomTagSQ(0x0012, 0x0064);

        ///<summary>(0012,0071) VR=LO VM=1 Clinical Trial Series ID</summary>
        public readonly static DicomTagLO ClinicalTrialSeriesID = new DicomTagLO(0x0012, 0x0071);

        ///<summary>(0012,0072) VR=LO VM=1 Clinical Trial Series Description</summary>
        public readonly static DicomTagLO ClinicalTrialSeriesDescription = new DicomTagLO(0x0012, 0x0072);

        ///<summary>(0012,0073) VR=LO VM=1 Issuer of Clinical Trial Series ID</summary>
        public readonly static DicomTagLO IssuerOfClinicalTrialSeriesID = new DicomTagLO(0x0012, 0x0073);

        ///<summary>(0012,0081) VR=LO VM=1 Clinical Trial Protocol Ethics Committee Name</summary>
        public readonly static DicomTagLO ClinicalTrialProtocolEthicsCommitteeName = new DicomTagLO(0x0012, 0x0081);

        ///<summary>(0012,0082) VR=LO VM=1 Clinical Trial Protocol Ethics Committee Approval Number</summary>
        public readonly static DicomTagLO ClinicalTrialProtocolEthicsCommitteeApprovalNumber = new DicomTagLO(0x0012, 0x0082);

        ///<summary>(0012,0083) VR=SQ VM=1 Consent for Clinical Trial Use Sequence</summary>
        public readonly static DicomTagSQ ConsentForClinicalTrialUseSequence = new DicomTagSQ(0x0012, 0x0083);

        ///<summary>(0012,0084) VR=CS VM=1 Distribution Type</summary>
        public readonly static DicomTagCS DistributionType = new DicomTagCS(0x0012, 0x0084);

        ///<summary>(0012,0085) VR=CS VM=1 Consent for Distribution Flag</summary>
        public readonly static DicomTagCS ConsentForDistributionFlag = new DicomTagCS(0x0012, 0x0085);

        ///<summary>(0012,0086) VR=DA VM=1 Ethics Committee Approval Effectiveness Start Date</summary>
        public readonly static DicomTagDA EthicsCommitteeApprovalEffectivenessStartDate = new DicomTagDA(0x0012, 0x0086);

        ///<summary>(0012,0087) VR=DA VM=1 Ethics Committee Approval Effectiveness End Date</summary>
        public readonly static DicomTagDA EthicsCommitteeApprovalEffectivenessEndDate = new DicomTagDA(0x0012, 0x0087);

        ///<summary>(0014,0023) VR=ST VM=1 CAD File Format (RETIRED)</summary>
        public readonly static DicomTagST CADFileFormatRETIRED = new DicomTagST(0x0014, 0x0023);

        ///<summary>(0014,0024) VR=ST VM=1 Component Reference System (RETIRED)</summary>
        public readonly static DicomTagST ComponentReferenceSystemRETIRED = new DicomTagST(0x0014, 0x0024);

        ///<summary>(0014,0025) VR=ST VM=1 Component Manufacturing Procedure</summary>
        public readonly static DicomTagST ComponentManufacturingProcedure = new DicomTagST(0x0014, 0x0025);

        ///<summary>(0014,0028) VR=ST VM=1 Component Manufacturer</summary>
        public readonly static DicomTagST ComponentManufacturer = new DicomTagST(0x0014, 0x0028);

        ///<summary>(0014,0030) VR=DS VM=1-n Material Thickness</summary>
        public readonly static DicomTagDSs MaterialThickness = new DicomTagDSs(0x0014, 0x0030);

        ///<summary>(0014,0032) VR=DS VM=1-n Material Pipe Diameter</summary>
        public readonly static DicomTagDSs MaterialPipeDiameter = new DicomTagDSs(0x0014, 0x0032);

        ///<summary>(0014,0034) VR=DS VM=1-n Material Isolation Diameter</summary>
        public readonly static DicomTagDSs MaterialIsolationDiameter = new DicomTagDSs(0x0014, 0x0034);

        ///<summary>(0014,0042) VR=ST VM=1 Material Grade</summary>
        public readonly static DicomTagST MaterialGrade = new DicomTagST(0x0014, 0x0042);

        ///<summary>(0014,0044) VR=ST VM=1 Material Properties Description</summary>
        public readonly static DicomTagST MaterialPropertiesDescription = new DicomTagST(0x0014, 0x0044);

        ///<summary>(0014,0045) VR=ST VM=1 Material Properties File Format (Retired) (RETIRED)</summary>
        public readonly static DicomTagST MaterialPropertiesFileFormatRetiredRETIRED = new DicomTagST(0x0014, 0x0045);

        ///<summary>(0014,0046) VR=LT VM=1 Material Notes</summary>
        public readonly static DicomTagLT MaterialNotes = new DicomTagLT(0x0014, 0x0046);

        ///<summary>(0014,0050) VR=CS VM=1 Component Shape</summary>
        public readonly static DicomTagCS ComponentShape = new DicomTagCS(0x0014, 0x0050);

        ///<summary>(0014,0052) VR=CS VM=1 Curvature Type</summary>
        public readonly static DicomTagCS CurvatureType = new DicomTagCS(0x0014, 0x0052);

        ///<summary>(0014,0054) VR=DS VM=1 Outer Diameter</summary>
        public readonly static DicomTagDS OuterDiameter = new DicomTagDS(0x0014, 0x0054);

        ///<summary>(0014,0056) VR=DS VM=1 Inner Diameter</summary>
        public readonly static DicomTagDS InnerDiameter = new DicomTagDS(0x0014, 0x0056);

        ///<summary>(0014,0100) VR=LO VM=1-n Component Welder IDs</summary>
        public readonly static DicomTagLOs ComponentWelderIDs = new DicomTagLOs(0x0014, 0x0100);

        ///<summary>(0014,0101) VR=CS VM=1 Secondary Approval Status</summary>
        public readonly static DicomTagCS SecondaryApprovalStatus = new DicomTagCS(0x0014, 0x0101);

        ///<summary>(0014,0102) VR=DA VM=1 Secondary Review Date</summary>
        public readonly static DicomTagDA SecondaryReviewDate = new DicomTagDA(0x0014, 0x0102);

        ///<summary>(0014,0103) VR=TM VM=1 Secondary Review Time</summary>
        public readonly static DicomTagTM SecondaryReviewTime = new DicomTagTM(0x0014, 0x0103);

        ///<summary>(0014,0104) VR=PN VM=1 Secondary Reviewer Name</summary>
        public readonly static DicomTagPN SecondaryReviewerName = new DicomTagPN(0x0014, 0x0104);

        ///<summary>(0014,0105) VR=ST VM=1 Repair ID</summary>
        public readonly static DicomTagST RepairID = new DicomTagST(0x0014, 0x0105);

        ///<summary>(0014,0106) VR=SQ VM=1 Multiple Component Approval Sequence</summary>
        public readonly static DicomTagSQ MultipleComponentApprovalSequence = new DicomTagSQ(0x0014, 0x0106);

        ///<summary>(0014,0107) VR=CS VM=1-n Other Approval Status</summary>
        public readonly static DicomTagCSs OtherApprovalStatus = new DicomTagCSs(0x0014, 0x0107);

        ///<summary>(0014,0108) VR=CS VM=1-n Other Secondary Approval Status</summary>
        public readonly static DicomTagCSs OtherSecondaryApprovalStatus = new DicomTagCSs(0x0014, 0x0108);

        ///<summary>(0014,0200) VR=SQ VM=1 Data Element Label Sequence</summary>
        public readonly static DicomTagSQ DataElementLabelSequence = new DicomTagSQ(0x0014, 0x0200);

        ///<summary>(0014,0201) VR=SQ VM=1 Data Element Label Item Sequence</summary>
        public readonly static DicomTagSQ DataElementLabelItemSequence = new DicomTagSQ(0x0014, 0x0201);

        ///<summary>(0014,0202) VR=AT VM=1 Data Element</summary>
        public readonly static DicomTagAT DataElement = new DicomTagAT(0x0014, 0x0202);

        ///<summary>(0014,0203) VR=LO VM=1 Data Element Name</summary>
        public readonly static DicomTagLO DataElementName = new DicomTagLO(0x0014, 0x0203);

        ///<summary>(0014,0204) VR=LO VM=1 Data Element Description</summary>
        public readonly static DicomTagLO DataElementDescription = new DicomTagLO(0x0014, 0x0204);

        ///<summary>(0014,0205) VR=CS VM=1 Data Element Conditionality</summary>
        public readonly static DicomTagCS DataElementConditionality = new DicomTagCS(0x0014, 0x0205);

        ///<summary>(0014,0206) VR=IS VM=1 Data Element Minimum Characters</summary>
        public readonly static DicomTagIS DataElementMinimumCharacters = new DicomTagIS(0x0014, 0x0206);

        ///<summary>(0014,0207) VR=IS VM=1 Data Element Maximum Characters</summary>
        public readonly static DicomTagIS DataElementMaximumCharacters = new DicomTagIS(0x0014, 0x0207);

        ///<summary>(0014,1010) VR=ST VM=1 Actual Environmental Conditions</summary>
        public readonly static DicomTagST ActualEnvironmentalConditions = new DicomTagST(0x0014, 0x1010);

        ///<summary>(0014,1020) VR=DA VM=1 Expiry Date</summary>
        public readonly static DicomTagDA ExpiryDate = new DicomTagDA(0x0014, 0x1020);

        ///<summary>(0014,1040) VR=ST VM=1 Environmental Conditions</summary>
        public readonly static DicomTagST EnvironmentalConditions = new DicomTagST(0x0014, 0x1040);

        ///<summary>(0014,2002) VR=SQ VM=1 Evaluator Sequence</summary>
        public readonly static DicomTagSQ EvaluatorSequence = new DicomTagSQ(0x0014, 0x2002);

        ///<summary>(0014,2004) VR=IS VM=1 Evaluator Number</summary>
        public readonly static DicomTagIS EvaluatorNumber = new DicomTagIS(0x0014, 0x2004);

        ///<summary>(0014,2006) VR=PN VM=1 Evaluator Name</summary>
        public readonly static DicomTagPN EvaluatorName = new DicomTagPN(0x0014, 0x2006);

        ///<summary>(0014,2008) VR=IS VM=1 Evaluation Attempt</summary>
        public readonly static DicomTagIS EvaluationAttempt = new DicomTagIS(0x0014, 0x2008);

        ///<summary>(0014,2012) VR=SQ VM=1 Indication Sequence</summary>
        public readonly static DicomTagSQ IndicationSequence = new DicomTagSQ(0x0014, 0x2012);

        ///<summary>(0014,2014) VR=IS VM=1 Indication Number</summary>
        public readonly static DicomTagIS IndicationNumber = new DicomTagIS(0x0014, 0x2014);

        ///<summary>(0014,2016) VR=SH VM=1 Indication Label</summary>
        public readonly static DicomTagSH IndicationLabel = new DicomTagSH(0x0014, 0x2016);

        ///<summary>(0014,2018) VR=ST VM=1 Indication Description</summary>
        public readonly static DicomTagST IndicationDescription = new DicomTagST(0x0014, 0x2018);

        ///<summary>(0014,201A) VR=CS VM=1-n Indication Type</summary>
        public readonly static DicomTagCSs IndicationType = new DicomTagCSs(0x0014, 0x201A);

        ///<summary>(0014,201C) VR=CS VM=1 Indication Disposition</summary>
        public readonly static DicomTagCS IndicationDisposition = new DicomTagCS(0x0014, 0x201C);

        ///<summary>(0014,201E) VR=SQ VM=1 Indication ROI Sequence</summary>
        public readonly static DicomTagSQ IndicationROISequence = new DicomTagSQ(0x0014, 0x201E);

        ///<summary>(0014,2030) VR=SQ VM=1 Indication Physical Property Sequence</summary>
        public readonly static DicomTagSQ IndicationPhysicalPropertySequence = new DicomTagSQ(0x0014, 0x2030);

        ///<summary>(0014,2032) VR=SH VM=1 Property Label</summary>
        public readonly static DicomTagSH PropertyLabel = new DicomTagSH(0x0014, 0x2032);

        ///<summary>(0014,2202) VR=IS VM=1 Coordinate System Number of Axes</summary>
        public readonly static DicomTagIS CoordinateSystemNumberOfAxes = new DicomTagIS(0x0014, 0x2202);

        ///<summary>(0014,2204) VR=SQ VM=1 Coordinate System Axes Sequence</summary>
        public readonly static DicomTagSQ CoordinateSystemAxesSequence = new DicomTagSQ(0x0014, 0x2204);

        ///<summary>(0014,2206) VR=ST VM=1 Coordinate System Axis Description</summary>
        public readonly static DicomTagST CoordinateSystemAxisDescription = new DicomTagST(0x0014, 0x2206);

        ///<summary>(0014,2208) VR=CS VM=1 Coordinate System Data Set Mapping</summary>
        public readonly static DicomTagCS CoordinateSystemDataSetMapping = new DicomTagCS(0x0014, 0x2208);

        ///<summary>(0014,220A) VR=IS VM=1 Coordinate System Axis Number</summary>
        public readonly static DicomTagIS CoordinateSystemAxisNumber = new DicomTagIS(0x0014, 0x220A);

        ///<summary>(0014,220C) VR=CS VM=1 Coordinate System Axis Type</summary>
        public readonly static DicomTagCS CoordinateSystemAxisType = new DicomTagCS(0x0014, 0x220C);

        ///<summary>(0014,220E) VR=CS VM=1 Coordinate System Axis Units</summary>
        public readonly static DicomTagCS CoordinateSystemAxisUnits = new DicomTagCS(0x0014, 0x220E);

        ///<summary>(0014,2210) VR=OB VM=1 Coordinate System Axis Values</summary>
        public readonly static DicomTagOB CoordinateSystemAxisValues = new DicomTagOB(0x0014, 0x2210);

        ///<summary>(0014,2220) VR=SQ VM=1 Coordinate System Transform Sequence</summary>
        public readonly static DicomTagSQ CoordinateSystemTransformSequence = new DicomTagSQ(0x0014, 0x2220);

        ///<summary>(0014,2222) VR=ST VM=1 Transform Description</summary>
        public readonly static DicomTagST TransformDescription = new DicomTagST(0x0014, 0x2222);

        ///<summary>(0014,2224) VR=IS VM=1 Transform Number of Axes</summary>
        public readonly static DicomTagIS TransformNumberOfAxes = new DicomTagIS(0x0014, 0x2224);

        ///<summary>(0014,2226) VR=IS VM=1-n Transform Order of Axes</summary>
        public readonly static DicomTagISs TransformOrderOfAxes = new DicomTagISs(0x0014, 0x2226);

        ///<summary>(0014,2228) VR=CS VM=1 Transformed Axis Units</summary>
        public readonly static DicomTagCS TransformedAxisUnits = new DicomTagCS(0x0014, 0x2228);

        ///<summary>(0014,222A) VR=DS VM=1-n Coordinate System Transform Rotation and Scale Matrix</summary>
        public readonly static DicomTagDSs CoordinateSystemTransformRotationAndScaleMatrix = new DicomTagDSs(0x0014, 0x222A);

        ///<summary>(0014,222C) VR=DS VM=1-n Coordinate System Transform Translation Matrix</summary>
        public readonly static DicomTagDSs CoordinateSystemTransformTranslationMatrix = new DicomTagDSs(0x0014, 0x222C);

        ///<summary>(0014,3011) VR=DS VM=1 Internal Detector Frame Time</summary>
        public readonly static DicomTagDS InternalDetectorFrameTime = new DicomTagDS(0x0014, 0x3011);

        ///<summary>(0014,3012) VR=DS VM=1 Number of Frames Integrated</summary>
        public readonly static DicomTagDS NumberOfFramesIntegrated = new DicomTagDS(0x0014, 0x3012);

        ///<summary>(0014,3020) VR=SQ VM=1 Detector Temperature Sequence</summary>
        public readonly static DicomTagSQ DetectorTemperatureSequence = new DicomTagSQ(0x0014, 0x3020);

        ///<summary>(0014,3022) VR=ST VM=1 Sensor Name</summary>
        public readonly static DicomTagST SensorName = new DicomTagST(0x0014, 0x3022);

        ///<summary>(0014,3024) VR=DS VM=1 Horizontal Offset of Sensor</summary>
        public readonly static DicomTagDS HorizontalOffsetOfSensor = new DicomTagDS(0x0014, 0x3024);

        ///<summary>(0014,3026) VR=DS VM=1 Vertical Offset of Sensor</summary>
        public readonly static DicomTagDS VerticalOffsetOfSensor = new DicomTagDS(0x0014, 0x3026);

        ///<summary>(0014,3028) VR=DS VM=1 Sensor Temperature</summary>
        public readonly static DicomTagDS SensorTemperature = new DicomTagDS(0x0014, 0x3028);

        ///<summary>(0014,3040) VR=SQ VM=1 Dark Current Sequence</summary>
        public readonly static DicomTagSQ DarkCurrentSequence = new DicomTagSQ(0x0014, 0x3040);

        ///<summary>(0014,3050) VR=OB/OW VM=1 Dark Current Counts</summary>
        public readonly static DicomTagOBOW DarkCurrentCounts = new DicomTagOBOW(0x0014, 0x3050);

        ///<summary>(0014,3060) VR=SQ VM=1 Gain Correction Reference Sequence</summary>
        public readonly static DicomTagSQ GainCorrectionReferenceSequence = new DicomTagSQ(0x0014, 0x3060);

        ///<summary>(0014,3070) VR=OB/OW VM=1 Air Counts</summary>
        public readonly static DicomTagOBOW AirCounts = new DicomTagOBOW(0x0014, 0x3070);

        ///<summary>(0014,3071) VR=DS VM=1 KV Used in Gain Calibration</summary>
        public readonly static DicomTagDS KVUsedInGainCalibration = new DicomTagDS(0x0014, 0x3071);

        ///<summary>(0014,3072) VR=DS VM=1 MA Used in Gain Calibration</summary>
        public readonly static DicomTagDS MAUsedInGainCalibration = new DicomTagDS(0x0014, 0x3072);

        ///<summary>(0014,3073) VR=DS VM=1 Number of Frames Used for Integration</summary>
        public readonly static DicomTagDS NumberOfFramesUsedForIntegration = new DicomTagDS(0x0014, 0x3073);

        ///<summary>(0014,3074) VR=LO VM=1 Filter Material Used in Gain Calibration</summary>
        public readonly static DicomTagLO FilterMaterialUsedInGainCalibration = new DicomTagLO(0x0014, 0x3074);

        ///<summary>(0014,3075) VR=DS VM=1 Filter Thickness Used in Gain Calibration</summary>
        public readonly static DicomTagDS FilterThicknessUsedInGainCalibration = new DicomTagDS(0x0014, 0x3075);

        ///<summary>(0014,3076) VR=DA VM=1 Date of Gain Calibration</summary>
        public readonly static DicomTagDA DateOfGainCalibration = new DicomTagDA(0x0014, 0x3076);

        ///<summary>(0014,3077) VR=TM VM=1 Time of Gain Calibration</summary>
        public readonly static DicomTagTM TimeOfGainCalibration = new DicomTagTM(0x0014, 0x3077);

        ///<summary>(0014,3080) VR=OB VM=1 Bad Pixel Image</summary>
        public readonly static DicomTagOB BadPixelImage = new DicomTagOB(0x0014, 0x3080);

        ///<summary>(0014,3099) VR=LT VM=1 Calibration Notes</summary>
        public readonly static DicomTagLT CalibrationNotes = new DicomTagLT(0x0014, 0x3099);

        ///<summary>(0014,3100) VR=LT VM=1 Linearity Correction Technique</summary>
        public readonly static DicomTagLT LinearityCorrectionTechnique = new DicomTagLT(0x0014, 0x3100);

        ///<summary>(0014,3101) VR=LT VM=1 Beam Hardening Correction Technique</summary>
        public readonly static DicomTagLT BeamHardeningCorrectionTechnique = new DicomTagLT(0x0014, 0x3101);

        ///<summary>(0014,4002) VR=SQ VM=1 Pulser Equipment Sequence</summary>
        public readonly static DicomTagSQ PulserEquipmentSequence = new DicomTagSQ(0x0014, 0x4002);

        ///<summary>(0014,4004) VR=CS VM=1 Pulser Type</summary>
        public readonly static DicomTagCS PulserType = new DicomTagCS(0x0014, 0x4004);

        ///<summary>(0014,4006) VR=LT VM=1 Pulser Notes</summary>
        public readonly static DicomTagLT PulserNotes = new DicomTagLT(0x0014, 0x4006);

        ///<summary>(0014,4008) VR=SQ VM=1 Receiver Equipment Sequence</summary>
        public readonly static DicomTagSQ ReceiverEquipmentSequence = new DicomTagSQ(0x0014, 0x4008);

        ///<summary>(0014,400A) VR=CS VM=1 Amplifier Type</summary>
        public readonly static DicomTagCS AmplifierType = new DicomTagCS(0x0014, 0x400A);

        ///<summary>(0014,400C) VR=LT VM=1 Receiver Notes</summary>
        public readonly static DicomTagLT ReceiverNotes = new DicomTagLT(0x0014, 0x400C);

        ///<summary>(0014,400E) VR=SQ VM=1 Pre-Amplifier Equipment Sequence</summary>
        public readonly static DicomTagSQ PreAmplifierEquipmentSequence = new DicomTagSQ(0x0014, 0x400E);

        ///<summary>(0014,400F) VR=LT VM=1 Pre-Amplifier Notes</summary>
        public readonly static DicomTagLT PreAmplifierNotes = new DicomTagLT(0x0014, 0x400F);

        ///<summary>(0014,4010) VR=SQ VM=1 Transmit Transducer Sequence</summary>
        public readonly static DicomTagSQ TransmitTransducerSequence = new DicomTagSQ(0x0014, 0x4010);

        ///<summary>(0014,4011) VR=SQ VM=1 Receive Transducer Sequence</summary>
        public readonly static DicomTagSQ ReceiveTransducerSequence = new DicomTagSQ(0x0014, 0x4011);

        ///<summary>(0014,4012) VR=US VM=1 Number of Elements</summary>
        public readonly static DicomTagUS NumberOfElements = new DicomTagUS(0x0014, 0x4012);

        ///<summary>(0014,4013) VR=CS VM=1 Element Shape</summary>
        public readonly static DicomTagCS ElementShape = new DicomTagCS(0x0014, 0x4013);

        ///<summary>(0014,4014) VR=DS VM=1 Element Dimension A</summary>
        public readonly static DicomTagDS ElementDimensionA = new DicomTagDS(0x0014, 0x4014);

        ///<summary>(0014,4015) VR=DS VM=1 Element Dimension B</summary>
        public readonly static DicomTagDS ElementDimensionB = new DicomTagDS(0x0014, 0x4015);

        ///<summary>(0014,4016) VR=DS VM=1 Element Pitch A</summary>
        public readonly static DicomTagDS ElementPitchA = new DicomTagDS(0x0014, 0x4016);

        ///<summary>(0014,4017) VR=DS VM=1 Measured Beam Dimension A</summary>
        public readonly static DicomTagDS MeasuredBeamDimensionA = new DicomTagDS(0x0014, 0x4017);

        ///<summary>(0014,4018) VR=DS VM=1 Measured Beam Dimension B</summary>
        public readonly static DicomTagDS MeasuredBeamDimensionB = new DicomTagDS(0x0014, 0x4018);

        ///<summary>(0014,4019) VR=DS VM=1 Location of Measured Beam Diameter</summary>
        public readonly static DicomTagDS LocationOfMeasuredBeamDiameter = new DicomTagDS(0x0014, 0x4019);

        ///<summary>(0014,401A) VR=DS VM=1 Nominal Frequency</summary>
        public readonly static DicomTagDS NominalFrequency = new DicomTagDS(0x0014, 0x401A);

        ///<summary>(0014,401B) VR=DS VM=1 Measured Center Frequency</summary>
        public readonly static DicomTagDS MeasuredCenterFrequency = new DicomTagDS(0x0014, 0x401B);

        ///<summary>(0014,401C) VR=DS VM=1 Measured Bandwidth</summary>
        public readonly static DicomTagDS MeasuredBandwidth = new DicomTagDS(0x0014, 0x401C);

        ///<summary>(0014,401D) VR=DS VM=1 Element Pitch B</summary>
        public readonly static DicomTagDS ElementPitchB = new DicomTagDS(0x0014, 0x401D);

        ///<summary>(0014,4020) VR=SQ VM=1 Pulser Settings Sequence</summary>
        public readonly static DicomTagSQ PulserSettingsSequence = new DicomTagSQ(0x0014, 0x4020);

        ///<summary>(0014,4022) VR=DS VM=1 Pulse Width</summary>
        public readonly static DicomTagDS PulseWidth = new DicomTagDS(0x0014, 0x4022);

        ///<summary>(0014,4024) VR=DS VM=1 Excitation Frequency</summary>
        public readonly static DicomTagDS ExcitationFrequency = new DicomTagDS(0x0014, 0x4024);

        ///<summary>(0014,4026) VR=CS VM=1 Modulation Type</summary>
        public readonly static DicomTagCS ModulationType = new DicomTagCS(0x0014, 0x4026);

        ///<summary>(0014,4028) VR=DS VM=1 Damping</summary>
        public readonly static DicomTagDS Damping = new DicomTagDS(0x0014, 0x4028);

        ///<summary>(0014,4030) VR=SQ VM=1 Receiver Settings Sequence</summary>
        public readonly static DicomTagSQ ReceiverSettingsSequence = new DicomTagSQ(0x0014, 0x4030);

        ///<summary>(0014,4031) VR=DS VM=1 Acquired Soundpath Length</summary>
        public readonly static DicomTagDS AcquiredSoundpathLength = new DicomTagDS(0x0014, 0x4031);

        ///<summary>(0014,4032) VR=CS VM=1 Acquisition Compression Type</summary>
        public readonly static DicomTagCS AcquisitionCompressionType = new DicomTagCS(0x0014, 0x4032);

        ///<summary>(0014,4033) VR=IS VM=1 Acquisition Sample Size</summary>
        public readonly static DicomTagIS AcquisitionSampleSize = new DicomTagIS(0x0014, 0x4033);

        ///<summary>(0014,4034) VR=DS VM=1 Rectifier Smoothing</summary>
        public readonly static DicomTagDS RectifierSmoothing = new DicomTagDS(0x0014, 0x4034);

        ///<summary>(0014,4035) VR=SQ VM=1 DAC Sequence</summary>
        public readonly static DicomTagSQ DACSequence = new DicomTagSQ(0x0014, 0x4035);

        ///<summary>(0014,4036) VR=CS VM=1 DAC Type</summary>
        public readonly static DicomTagCS DACType = new DicomTagCS(0x0014, 0x4036);

        ///<summary>(0014,4038) VR=DS VM=1-n DAC Gain Points</summary>
        public readonly static DicomTagDSs DACGainPoints = new DicomTagDSs(0x0014, 0x4038);

        ///<summary>(0014,403A) VR=DS VM=1-n DAC Time Points</summary>
        public readonly static DicomTagDSs DACTimePoints = new DicomTagDSs(0x0014, 0x403A);

        ///<summary>(0014,403C) VR=DS VM=1-n DAC Amplitude</summary>
        public readonly static DicomTagDSs DACAmplitude = new DicomTagDSs(0x0014, 0x403C);

        ///<summary>(0014,4040) VR=SQ VM=1 Pre-Amplifier Settings Sequence</summary>
        public readonly static DicomTagSQ PreAmplifierSettingsSequence = new DicomTagSQ(0x0014, 0x4040);

        ///<summary>(0014,4050) VR=SQ VM=1 Transmit Transducer Settings Sequence</summary>
        public readonly static DicomTagSQ TransmitTransducerSettingsSequence = new DicomTagSQ(0x0014, 0x4050);

        ///<summary>(0014,4051) VR=SQ VM=1 Receive Transducer Settings Sequence</summary>
        public readonly static DicomTagSQ ReceiveTransducerSettingsSequence = new DicomTagSQ(0x0014, 0x4051);

        ///<summary>(0014,4052) VR=DS VM=1 Incident Angle</summary>
        public readonly static DicomTagDS IncidentAngle = new DicomTagDS(0x0014, 0x4052);

        ///<summary>(0014,4054) VR=ST VM=1 Coupling Technique</summary>
        public readonly static DicomTagST CouplingTechnique = new DicomTagST(0x0014, 0x4054);

        ///<summary>(0014,4056) VR=ST VM=1 Coupling Medium</summary>
        public readonly static DicomTagST CouplingMedium = new DicomTagST(0x0014, 0x4056);

        ///<summary>(0014,4057) VR=DS VM=1 Coupling Velocity</summary>
        public readonly static DicomTagDS CouplingVelocity = new DicomTagDS(0x0014, 0x4057);

        ///<summary>(0014,4058) VR=DS VM=1 Probe Center Location X</summary>
        public readonly static DicomTagDS ProbeCenterLocationX = new DicomTagDS(0x0014, 0x4058);

        ///<summary>(0014,4059) VR=DS VM=1 Probe Center Location Z</summary>
        public readonly static DicomTagDS ProbeCenterLocationZ = new DicomTagDS(0x0014, 0x4059);

        ///<summary>(0014,405A) VR=DS VM=1 Sound Path Length</summary>
        public readonly static DicomTagDS SoundPathLength = new DicomTagDS(0x0014, 0x405A);

        ///<summary>(0014,405C) VR=ST VM=1 Delay Law Identifier</summary>
        public readonly static DicomTagST DelayLawIdentifier = new DicomTagST(0x0014, 0x405C);

        ///<summary>(0014,4060) VR=SQ VM=1 Gate Settings Sequence</summary>
        public readonly static DicomTagSQ GateSettingsSequence = new DicomTagSQ(0x0014, 0x4060);

        ///<summary>(0014,4062) VR=DS VM=1 Gate Threshold</summary>
        public readonly static DicomTagDS GateThreshold = new DicomTagDS(0x0014, 0x4062);

        ///<summary>(0014,4064) VR=DS VM=1 Velocity of Sound</summary>
        public readonly static DicomTagDS VelocityOfSound = new DicomTagDS(0x0014, 0x4064);

        ///<summary>(0014,4070) VR=SQ VM=1 Calibration Settings Sequence</summary>
        public readonly static DicomTagSQ CalibrationSettingsSequence = new DicomTagSQ(0x0014, 0x4070);

        ///<summary>(0014,4072) VR=ST VM=1 Calibration Procedure</summary>
        public readonly static DicomTagST CalibrationProcedure = new DicomTagST(0x0014, 0x4072);

        ///<summary>(0014,4074) VR=SH VM=1 Procedure Version</summary>
        public readonly static DicomTagSH ProcedureVersion = new DicomTagSH(0x0014, 0x4074);

        ///<summary>(0014,4076) VR=DA VM=1 Procedure Creation Date</summary>
        public readonly static DicomTagDA ProcedureCreationDate = new DicomTagDA(0x0014, 0x4076);

        ///<summary>(0014,4078) VR=DA VM=1 Procedure Expiration Date</summary>
        public readonly static DicomTagDA ProcedureExpirationDate = new DicomTagDA(0x0014, 0x4078);

        ///<summary>(0014,407A) VR=DA VM=1 Procedure Last Modified Date</summary>
        public readonly static DicomTagDA ProcedureLastModifiedDate = new DicomTagDA(0x0014, 0x407A);

        ///<summary>(0014,407C) VR=TM VM=1-n Calibration Time</summary>
        public readonly static DicomTagTMs CalibrationTime = new DicomTagTMs(0x0014, 0x407C);

        ///<summary>(0014,407E) VR=DA VM=1-n Calibration Date</summary>
        public readonly static DicomTagDAs CalibrationDate = new DicomTagDAs(0x0014, 0x407E);

        ///<summary>(0014,4080) VR=SQ VM=1 Probe Drive Equipment Sequence</summary>
        public readonly static DicomTagSQ ProbeDriveEquipmentSequence = new DicomTagSQ(0x0014, 0x4080);

        ///<summary>(0014,4081) VR=CS VM=1 Drive Type</summary>
        public readonly static DicomTagCS DriveType = new DicomTagCS(0x0014, 0x4081);

        ///<summary>(0014,4082) VR=LT VM=1 Probe Drive Notes</summary>
        public readonly static DicomTagLT ProbeDriveNotes = new DicomTagLT(0x0014, 0x4082);

        ///<summary>(0014,4083) VR=SQ VM=1 Drive Probe Sequence</summary>
        public readonly static DicomTagSQ DriveProbeSequence = new DicomTagSQ(0x0014, 0x4083);

        ///<summary>(0014,4084) VR=DS VM=1 Probe Inductance</summary>
        public readonly static DicomTagDS ProbeInductance = new DicomTagDS(0x0014, 0x4084);

        ///<summary>(0014,4085) VR=DS VM=1 Probe Resistance</summary>
        public readonly static DicomTagDS ProbeResistance = new DicomTagDS(0x0014, 0x4085);

        ///<summary>(0014,4086) VR=SQ VM=1 Receive Probe Sequence</summary>
        public readonly static DicomTagSQ ReceiveProbeSequence = new DicomTagSQ(0x0014, 0x4086);

        ///<summary>(0014,4087) VR=SQ VM=1 Probe Drive Settings Sequence</summary>
        public readonly static DicomTagSQ ProbeDriveSettingsSequence = new DicomTagSQ(0x0014, 0x4087);

        ///<summary>(0014,4088) VR=DS VM=1 Bridge Resistors</summary>
        public readonly static DicomTagDS BridgeResistors = new DicomTagDS(0x0014, 0x4088);

        ///<summary>(0014,4089) VR=DS VM=1 Probe Orientation Angle</summary>
        public readonly static DicomTagDS ProbeOrientationAngle = new DicomTagDS(0x0014, 0x4089);

        ///<summary>(0014,408B) VR=DS VM=1 User Selected Gain Y</summary>
        public readonly static DicomTagDS UserSelectedGainY = new DicomTagDS(0x0014, 0x408B);

        ///<summary>(0014,408C) VR=DS VM=1 User Selected Phase</summary>
        public readonly static DicomTagDS UserSelectedPhase = new DicomTagDS(0x0014, 0x408C);

        ///<summary>(0014,408D) VR=DS VM=1 User Selected Offset X</summary>
        public readonly static DicomTagDS UserSelectedOffsetX = new DicomTagDS(0x0014, 0x408D);

        ///<summary>(0014,408E) VR=DS VM=1 User Selected Offset Y</summary>
        public readonly static DicomTagDS UserSelectedOffsetY = new DicomTagDS(0x0014, 0x408E);

        ///<summary>(0014,4091) VR=SQ VM=1 Channel Settings Sequence</summary>
        public readonly static DicomTagSQ ChannelSettingsSequence = new DicomTagSQ(0x0014, 0x4091);

        ///<summary>(0014,4092) VR=DS VM=1 Channel Threshold</summary>
        public readonly static DicomTagDS ChannelThreshold = new DicomTagDS(0x0014, 0x4092);

        ///<summary>(0014,409A) VR=SQ VM=1 Scanner Settings Sequence</summary>
        public readonly static DicomTagSQ ScannerSettingsSequence = new DicomTagSQ(0x0014, 0x409A);

        ///<summary>(0014,409B) VR=ST VM=1 Scan Procedure</summary>
        public readonly static DicomTagST ScanProcedure = new DicomTagST(0x0014, 0x409B);

        ///<summary>(0014,409C) VR=DS VM=1 Translation Rate X</summary>
        public readonly static DicomTagDS TranslationRateX = new DicomTagDS(0x0014, 0x409C);

        ///<summary>(0014,409D) VR=DS VM=1 Translation Rate Y</summary>
        public readonly static DicomTagDS TranslationRateY = new DicomTagDS(0x0014, 0x409D);

        ///<summary>(0014,409F) VR=DS VM=1 Channel Overlap</summary>
        public readonly static DicomTagDS ChannelOverlap = new DicomTagDS(0x0014, 0x409F);

        ///<summary>(0014,40A0) VR=LO VM=1-n Image Quality Indicator Type</summary>
        public readonly static DicomTagLOs ImageQualityIndicatorType = new DicomTagLOs(0x0014, 0x40A0);

        ///<summary>(0014,40A1) VR=LO VM=1-n Image Quality Indicator Material</summary>
        public readonly static DicomTagLOs ImageQualityIndicatorMaterial = new DicomTagLOs(0x0014, 0x40A1);

        ///<summary>(0014,40A2) VR=LO VM=1-n Image Quality Indicator Size</summary>
        public readonly static DicomTagLOs ImageQualityIndicatorSize = new DicomTagLOs(0x0014, 0x40A2);

        ///<summary>(0014,4101) VR=SQ VM=1 Wave Dimensions Definition Sequence</summary>
        public readonly static DicomTagSQ WaveDimensionsDefinitionSequence = new DicomTagSQ(0x0014, 0x4101);

        ///<summary>(0014,4102) VR=US VM=1 Wave Dimension Number</summary>
        public readonly static DicomTagUS WaveDimensionNumber = new DicomTagUS(0x0014, 0x4102);

        ///<summary>(0014,4103) VR=LO VM=1 Wave Dimension Description</summary>
        public readonly static DicomTagLO WaveDimensionDescription = new DicomTagLO(0x0014, 0x4103);

        ///<summary>(0014,4104) VR=US VM=1 Wave Dimension Unit</summary>
        public readonly static DicomTagUS WaveDimensionUnit = new DicomTagUS(0x0014, 0x4104);

        ///<summary>(0014,4105) VR=CS VM=1 Wave Dimension Value Type</summary>
        public readonly static DicomTagCS WaveDimensionValueType = new DicomTagCS(0x0014, 0x4105);

        ///<summary>(0014,4106) VR=SQ VM=1-n Wave Dimension Values Sequence</summary>
        public readonly static DicomTagSQs WaveDimensionValuesSequence = new DicomTagSQs(0x0014, 0x4106);

        ///<summary>(0014,4107) VR=US VM=1 Referenced Wave Dimension</summary>
        public readonly static DicomTagUS ReferencedWaveDimension = new DicomTagUS(0x0014, 0x4107);

        ///<summary>(0014,4108) VR=SL VM=1 Integer Numeric Value</summary>
        public readonly static DicomTagSL IntegerNumericValue = new DicomTagSL(0x0014, 0x4108);

        ///<summary>(0014,4109) VR=OB VM=1 Byte Numeric Value</summary>
        public readonly static DicomTagOB ByteNumericValue = new DicomTagOB(0x0014, 0x4109);

        ///<summary>(0014,410A) VR=OW VM=1 Short Numeric Value</summary>
        public readonly static DicomTagOW ShortNumericValue = new DicomTagOW(0x0014, 0x410A);

        ///<summary>(0014,410B) VR=OF VM=1 Single Precision Floating Point Numeric Value</summary>
        public readonly static DicomTagOF SinglePrecisionFloatingPointNumericValue = new DicomTagOF(0x0014, 0x410B);

        ///<summary>(0014,410C) VR=OD VM=1 Double Precision Floating Point Numeric Value</summary>
        public readonly static DicomTagOD DoublePrecisionFloatingPointNumericValue = new DicomTagOD(0x0014, 0x410C);

        ///<summary>(0014,5002) VR=IS VM=1 LINAC Energy</summary>
        public readonly static DicomTagIS LINACEnergy = new DicomTagIS(0x0014, 0x5002);

        ///<summary>(0014,5004) VR=IS VM=1 LINAC Output</summary>
        public readonly static DicomTagIS LINACOutput = new DicomTagIS(0x0014, 0x5004);

        ///<summary>(0014,5100) VR=US VM=1 Active Aperture</summary>
        public readonly static DicomTagUS ActiveAperture = new DicomTagUS(0x0014, 0x5100);

        ///<summary>(0014,5101) VR=DS VM=1 Total Aperture</summary>
        public readonly static DicomTagDS TotalAperture = new DicomTagDS(0x0014, 0x5101);

        ///<summary>(0014,5102) VR=DS VM=1 Aperture Elevation</summary>
        public readonly static DicomTagDS ApertureElevation = new DicomTagDS(0x0014, 0x5102);

        ///<summary>(0014,5103) VR=DS VM=1 Main Lobe Angle</summary>
        public readonly static DicomTagDS MainLobeAngle = new DicomTagDS(0x0014, 0x5103);

        ///<summary>(0014,5104) VR=DS VM=1 Main Roof Angle</summary>
        public readonly static DicomTagDS MainRoofAngle = new DicomTagDS(0x0014, 0x5104);

        ///<summary>(0014,5105) VR=CS VM=1 Connector Type</summary>
        public readonly static DicomTagCS ConnectorType = new DicomTagCS(0x0014, 0x5105);

        ///<summary>(0014,5106) VR=SH VM=1 Wedge Model Number</summary>
        public readonly static DicomTagSH WedgeModelNumber = new DicomTagSH(0x0014, 0x5106);

        ///<summary>(0014,5107) VR=DS VM=1 Wedge Angle Float</summary>
        public readonly static DicomTagDS WedgeAngleFloat = new DicomTagDS(0x0014, 0x5107);

        ///<summary>(0014,5108) VR=DS VM=1 Wedge Roof Angle</summary>
        public readonly static DicomTagDS WedgeRoofAngle = new DicomTagDS(0x0014, 0x5108);

        ///<summary>(0014,5109) VR=CS VM=1 Wedge Element 1 Position</summary>
        public readonly static DicomTagCS WedgeElement1Position = new DicomTagCS(0x0014, 0x5109);

        ///<summary>(0014,510A) VR=DS VM=1 Wedge Material Velocity</summary>
        public readonly static DicomTagDS WedgeMaterialVelocity = new DicomTagDS(0x0014, 0x510A);

        ///<summary>(0014,510B) VR=SH VM=1 Wedge Material</summary>
        public readonly static DicomTagSH WedgeMaterial = new DicomTagSH(0x0014, 0x510B);

        ///<summary>(0014,510C) VR=DS VM=1 Wedge Offset Z</summary>
        public readonly static DicomTagDS WedgeOffsetZ = new DicomTagDS(0x0014, 0x510C);

        ///<summary>(0014,510D) VR=DS VM=1 Wedge Origin Offset X</summary>
        public readonly static DicomTagDS WedgeOriginOffsetX = new DicomTagDS(0x0014, 0x510D);

        ///<summary>(0014,510E) VR=DS VM=1 Wedge Time Delay</summary>
        public readonly static DicomTagDS WedgeTimeDelay = new DicomTagDS(0x0014, 0x510E);

        ///<summary>(0014,510F) VR=SH VM=1 Wedge Name</summary>
        public readonly static DicomTagSH WedgeName = new DicomTagSH(0x0014, 0x510F);

        ///<summary>(0014,5110) VR=SH VM=1 Wedge Manufacturer Name</summary>
        public readonly static DicomTagSH WedgeManufacturerName = new DicomTagSH(0x0014, 0x5110);

        ///<summary>(0014,5111) VR=LO VM=1 Wedge Description</summary>
        public readonly static DicomTagLO WedgeDescription = new DicomTagLO(0x0014, 0x5111);

        ///<summary>(0014,5112) VR=DS VM=1 Nominal Beam Angle</summary>
        public readonly static DicomTagDS NominalBeamAngle = new DicomTagDS(0x0014, 0x5112);

        ///<summary>(0014,5113) VR=DS VM=1 Wedge Offset X</summary>
        public readonly static DicomTagDS WedgeOffsetX = new DicomTagDS(0x0014, 0x5113);

        ///<summary>(0014,5114) VR=DS VM=1 Wedge Offset Y</summary>
        public readonly static DicomTagDS WedgeOffsetY = new DicomTagDS(0x0014, 0x5114);

        ///<summary>(0014,5115) VR=DS VM=1 Wedge Total Length</summary>
        public readonly static DicomTagDS WedgeTotalLength = new DicomTagDS(0x0014, 0x5115);

        ///<summary>(0014,5116) VR=DS VM=1 Wedge In Contact Length</summary>
        public readonly static DicomTagDS WedgeInContactLength = new DicomTagDS(0x0014, 0x5116);

        ///<summary>(0014,5117) VR=DS VM=1 Wedge Front Gap</summary>
        public readonly static DicomTagDS WedgeFrontGap = new DicomTagDS(0x0014, 0x5117);

        ///<summary>(0014,5118) VR=DS VM=1 Wedge Total Height</summary>
        public readonly static DicomTagDS WedgeTotalHeight = new DicomTagDS(0x0014, 0x5118);

        ///<summary>(0014,5119) VR=DS VM=1 Wedge Front Height</summary>
        public readonly static DicomTagDS WedgeFrontHeight = new DicomTagDS(0x0014, 0x5119);

        ///<summary>(0014,511A) VR=DS VM=1 Wedge Rear Height</summary>
        public readonly static DicomTagDS WedgeRearHeight = new DicomTagDS(0x0014, 0x511A);

        ///<summary>(0014,511B) VR=DS VM=1 Wedge Total Width</summary>
        public readonly static DicomTagDS WedgeTotalWidth = new DicomTagDS(0x0014, 0x511B);

        ///<summary>(0014,511C) VR=DS VM=1 Wedge In Contact Width</summary>
        public readonly static DicomTagDS WedgeInContactWidth = new DicomTagDS(0x0014, 0x511C);

        ///<summary>(0014,511D) VR=DS VM=1 Wedge Chamfer Height</summary>
        public readonly static DicomTagDS WedgeChamferHeight = new DicomTagDS(0x0014, 0x511D);

        ///<summary>(0014,511E) VR=CS VM=1 Wedge Curve</summary>
        public readonly static DicomTagCS WedgeCurve = new DicomTagCS(0x0014, 0x511E);

        ///<summary>(0014,511F) VR=DS VM=1 Radius Along the Wedge</summary>
        public readonly static DicomTagDS RadiusAlongWedge = new DicomTagDS(0x0014, 0x511F);

        ///<summary>(0014,6001) VR=SQ VM=1 Thermal Camera Settings Sequence</summary>
        public readonly static DicomTagSQ ThermalCameraSettingsSequence = new DicomTagSQ(0x0014, 0x6001);

        ///<summary>(0014,6002) VR=DS VM=1 Acquisition Frame Rate</summary>
        public readonly static DicomTagDS AcquisitionFrameRate = new DicomTagDS(0x0014, 0x6002);

        ///<summary>(0014,6003) VR=DS VM=1 Integration Time</summary>
        public readonly static DicomTagDS IntegrationTime = new DicomTagDS(0x0014, 0x6003);

        ///<summary>(0014,6004) VR=DS VM=1 Number of Calibration Frames</summary>
        public readonly static DicomTagDS NumberOfCalibrationFrames = new DicomTagDS(0x0014, 0x6004);

        ///<summary>(0014,6005) VR=DS VM=1 Number of Rows in Full Acquisition Image</summary>
        public readonly static DicomTagDS NumberOfRowsInFullAcquisitionImage = new DicomTagDS(0x0014, 0x6005);

        ///<summary>(0014,6006) VR=DS VM=1 Number Of Columns in Full Acquisition Image</summary>
        public readonly static DicomTagDS NumberOfColumnsInFullAcquisitionImage = new DicomTagDS(0x0014, 0x6006);

        ///<summary>(0014,6007) VR=SQ VM=1 Thermal Source Settings Sequence</summary>
        public readonly static DicomTagSQ ThermalSourceSettingsSequence = new DicomTagSQ(0x0014, 0x6007);

        ///<summary>(0014,6008) VR=DS VM=1 Source Horizontal Pitch</summary>
        public readonly static DicomTagDS SourceHorizontalPitch = new DicomTagDS(0x0014, 0x6008);

        ///<summary>(0014,6009) VR=DS VM=1 Source Vertical Pitch</summary>
        public readonly static DicomTagDS SourceVerticalPitch = new DicomTagDS(0x0014, 0x6009);

        ///<summary>(0014,600A) VR=DS VM=1 Source Horizontal Scan Speed</summary>
        public readonly static DicomTagDS SourceHorizontalScanSpeed = new DicomTagDS(0x0014, 0x600A);

        ///<summary>(0014,600B) VR=DS VM=1 Thermal Source Modulation Frequency</summary>
        public readonly static DicomTagDS ThermalSourceModulationFrequency = new DicomTagDS(0x0014, 0x600B);

        ///<summary>(0014,600C) VR=SQ VM=1 Induction Source Setting Sequence</summary>
        public readonly static DicomTagSQ InductionSourceSettingSequence = new DicomTagSQ(0x0014, 0x600C);

        ///<summary>(0014,600D) VR=DS VM=1 Coil Frequency</summary>
        public readonly static DicomTagDS CoilFrequency = new DicomTagDS(0x0014, 0x600D);

        ///<summary>(0014,600E) VR=DS VM=1 Current Amplitude Across Coil</summary>
        public readonly static DicomTagDS CurrentAmplitudeAcrossCoil = new DicomTagDS(0x0014, 0x600E);

        ///<summary>(0014,600F) VR=SQ VM=1 Flash Source Setting Sequence</summary>
        public readonly static DicomTagSQ FlashSourceSettingSequence = new DicomTagSQ(0x0014, 0x600F);

        ///<summary>(0014,6010) VR=DS VM=1 Flash Duration</summary>
        public readonly static DicomTagDS FlashDuration = new DicomTagDS(0x0014, 0x6010);

        ///<summary>(0014,6011) VR=DS VM=1-n Flash Frame Number</summary>
        public readonly static DicomTagDSs FlashFrameNumber = new DicomTagDSs(0x0014, 0x6011);

        ///<summary>(0014,6012) VR=SQ VM=1 Laser Source Setting Sequence</summary>
        public readonly static DicomTagSQ LaserSourceSettingSequence = new DicomTagSQ(0x0014, 0x6012);

        ///<summary>(0014,6013) VR=DS VM=1 Horizontal Laser Spot Dimension</summary>
        public readonly static DicomTagDS HorizontalLaserSpotDimension = new DicomTagDS(0x0014, 0x6013);

        ///<summary>(0014,6014) VR=DS VM=1 Vertical Laser Spot Dimension</summary>
        public readonly static DicomTagDS VerticalLaserSpotDimension = new DicomTagDS(0x0014, 0x6014);

        ///<summary>(0014,6015) VR=DS VM=1 Laser Wavelength</summary>
        public readonly static DicomTagDS LaserWavelength = new DicomTagDS(0x0014, 0x6015);

        ///<summary>(0014,6016) VR=DS VM=1 Laser Power</summary>
        public readonly static DicomTagDS LaserPower = new DicomTagDS(0x0014, 0x6016);

        ///<summary>(0014,6017) VR=SQ VM=1 Forced Gas Setting Sequence</summary>
        public readonly static DicomTagSQ ForcedGasSettingSequence = new DicomTagSQ(0x0014, 0x6017);

        ///<summary>(0014,6018) VR=SQ VM=1 Vibration Source Setting Sequence</summary>
        public readonly static DicomTagSQ VibrationSourceSettingSequence = new DicomTagSQ(0x0014, 0x6018);

        ///<summary>(0014,6019) VR=DS VM=1 Vibration Excitation Frequency</summary>
        public readonly static DicomTagDS VibrationExcitationFrequency = new DicomTagDS(0x0014, 0x6019);

        ///<summary>(0014,601A) VR=DS VM=1 Vibration Excitation Voltage</summary>
        public readonly static DicomTagDS VibrationExcitationVoltage = new DicomTagDS(0x0014, 0x601A);

        ///<summary>(0014,601B) VR=CS VM=1 Thermography Data Capture Method</summary>
        public readonly static DicomTagCS ThermographyDataCaptureMethod = new DicomTagCS(0x0014, 0x601B);

        ///<summary>(0014,601C) VR=CS VM=1 Thermal Technique</summary>
        public readonly static DicomTagCS ThermalTechnique = new DicomTagCS(0x0014, 0x601C);

        ///<summary>(0014,601D) VR=SQ VM=1 Thermal Camera Core Sequence</summary>
        public readonly static DicomTagSQ ThermalCameraCoreSequence = new DicomTagSQ(0x0014, 0x601D);

        ///<summary>(0014,601E) VR=CS VM=1 Detector Wavelength Range</summary>
        public readonly static DicomTagCS DetectorWavelengthRange = new DicomTagCS(0x0014, 0x601E);

        ///<summary>(0014,601F) VR=CS VM=1 Thermal Camera Calibration Type</summary>
        public readonly static DicomTagCS ThermalCameraCalibrationType = new DicomTagCS(0x0014, 0x601F);

        ///<summary>(0014,6020) VR=UV VM=1 Acquisition Image Counter</summary>
        public readonly static DicomTagUV AcquisitionImageCounter = new DicomTagUV(0x0014, 0x6020);

        ///<summary>(0014,6021) VR=DS VM=1 Front Panel Temperature</summary>
        public readonly static DicomTagDS FrontPanelTemperature = new DicomTagDS(0x0014, 0x6021);

        ///<summary>(0014,6022) VR=DS VM=1 Air Gap Temperature</summary>
        public readonly static DicomTagDS AirGapTemperature = new DicomTagDS(0x0014, 0x6022);

        ///<summary>(0014,6023) VR=DS VM=1 Vertical Pixel Size</summary>
        public readonly static DicomTagDS VerticalPixelSize = new DicomTagDS(0x0014, 0x6023);

        ///<summary>(0014,6024) VR=DS VM=1 Horizontal Pixel Size</summary>
        public readonly static DicomTagDS HorizontalPixelSize = new DicomTagDS(0x0014, 0x6024);

        ///<summary>(0014,6025) VR=ST VM=1-n Data Streaming Protocol</summary>
        public readonly static DicomTagSTs DataStreamingProtocol = new DicomTagSTs(0x0014, 0x6025);

        ///<summary>(0014,6026) VR=SQ VM=1 Lens Sequence</summary>
        public readonly static DicomTagSQ LensSequence = new DicomTagSQ(0x0014, 0x6026);

        ///<summary>(0014,6027) VR=DS VM=1 Field of View</summary>
        public readonly static DicomTagDS FieldOfView = new DicomTagDS(0x0014, 0x6027);

        ///<summary>(0014,6028) VR=LO VM=1 Lens Filter Manufacturer</summary>
        public readonly static DicomTagLO LensFilterManufacturer = new DicomTagLO(0x0014, 0x6028);

        ///<summary>(0014,6029) VR=CS VM=1 Cutoff Filter Type</summary>
        public readonly static DicomTagCS CutoffFilterType = new DicomTagCS(0x0014, 0x6029);

        ///<summary>(0014,602A) VR=DS VM=1-n Lens Filter Cut-Off Wavelength</summary>
        public readonly static DicomTagDSs LensFilterCutOffWavelength = new DicomTagDSs(0x0014, 0x602A);

        ///<summary>(0014,602B) VR=SQ VM=1 Thermal Source Sequence</summary>
        public readonly static DicomTagSQ ThermalSourceSequence = new DicomTagSQ(0x0014, 0x602B);

        ///<summary>(0014,602C) VR=CS VM=1 Thermal Source Motion State</summary>
        public readonly static DicomTagCS ThermalSourceMotionState = new DicomTagCS(0x0014, 0x602C);

        ///<summary>(0014,602D) VR=CS VM=1 Thermal Source Motion Type</summary>
        public readonly static DicomTagCS ThermalSourceMotionType = new DicomTagCS(0x0014, 0x602D);

        ///<summary>(0014,602E) VR=SQ VM=1 Induction Heating Sequence</summary>
        public readonly static DicomTagSQ InductionHeatingSequence = new DicomTagSQ(0x0014, 0x602E);

        ///<summary>(0014,602F) VR=ST VM=1 Coil Configuration ID</summary>
        public readonly static DicomTagST CoilConfigurationID = new DicomTagST(0x0014, 0x602F);

        ///<summary>(0014,6030) VR=DS VM=1 Number of Turns in Coil</summary>
        public readonly static DicomTagDS NumberOfTurnsInCoil = new DicomTagDS(0x0014, 0x6030);

        ///<summary>(0014,6031) VR=CS VM=1 Shape of Individual Turn</summary>
        public readonly static DicomTagCS ShapeOfIndividualTurn = new DicomTagCS(0x0014, 0x6031);

        ///<summary>(0014,6032) VR=DS VM=1-n Size of Individual Turn</summary>
        public readonly static DicomTagDSs SizeOfIndividualTurn = new DicomTagDSs(0x0014, 0x6032);

        ///<summary>(0014,6033) VR=DS VM=1-n Distance Between Turns</summary>
        public readonly static DicomTagDSs DistanceBetweenTurns = new DicomTagDSs(0x0014, 0x6033);

        ///<summary>(0014,6034) VR=SQ VM=1 Flash Heating Sequence</summary>
        public readonly static DicomTagSQ FlashHeatingSequence = new DicomTagSQ(0x0014, 0x6034);

        ///<summary>(0014,6035) VR=DS VM=1 Number of Lamps</summary>
        public readonly static DicomTagDS NumberOfLamps = new DicomTagDS(0x0014, 0x6035);

        ///<summary>(0014,6036) VR=ST VM=1 Flash Synchronization Protocol</summary>
        public readonly static DicomTagST FlashSynchronizationProtocol = new DicomTagST(0x0014, 0x6036);

        ///<summary>(0014,6037) VR=CS VM=1 Flash Modification Status</summary>
        public readonly static DicomTagCS FlashModificationStatus = new DicomTagCS(0x0014, 0x6037);

        ///<summary>(0014,6038) VR=SQ VM=1 Laser Heating Sequence</summary>
        public readonly static DicomTagSQ LaserHeatingSequence = new DicomTagSQ(0x0014, 0x6038);

        ///<summary>(0014,6039) VR=LO VM=1 Laser Manufacturer</summary>
        public readonly static DicomTagLO LaserManufacturer = new DicomTagLO(0x0014, 0x6039);

        ///<summary>(0014,603A) VR=LO VM=1 Laser Model Number</summary>
        public readonly static DicomTagLO LaserModelNumber = new DicomTagLO(0x0014, 0x603A);

        ///<summary>(0014,603B) VR=ST VM=1 Laser Type Description</summary>
        public readonly static DicomTagST LaserTypeDescription = new DicomTagST(0x0014, 0x603B);

        ///<summary>(0014,603C) VR=SQ VM=1 Forced Gas Heating Sequence</summary>
        public readonly static DicomTagSQ ForcedGasHeatingSequence = new DicomTagSQ(0x0014, 0x603C);

        ///<summary>(0014,603D) VR=LO VM=1 Gas Used for Heating/Cooling Part</summary>
        public readonly static DicomTagLO GasUsedForHeatingCoolingPart = new DicomTagLO(0x0014, 0x603D);

        ///<summary>(0014,603E) VR=SQ VM=1 Vibration/Sonic Heating Sequence</summary>
        public readonly static DicomTagSQ VibrationSonicHeatingSequence = new DicomTagSQ(0x0014, 0x603E);

        ///<summary>(0014,603F) VR=LO VM=1 Probe Manufacturer</summary>
        public readonly static DicomTagLO ProbeManufacturer = new DicomTagLO(0x0014, 0x603F);

        ///<summary>(0014,6040) VR=LO VM=1 Probe Model Number</summary>
        public readonly static DicomTagLO ProbeModelNumber = new DicomTagLO(0x0014, 0x6040);

        ///<summary>(0014,6041) VR=DS VM=1 Aperture Size</summary>
        public readonly static DicomTagDS ApertureSize = new DicomTagDS(0x0014, 0x6041);

        ///<summary>(0014,6042) VR=DS VM=1 Probe Resonant Frequency</summary>
        public readonly static DicomTagDS ProbeResonantFrequency = new DicomTagDS(0x0014, 0x6042);

        ///<summary>(0014,6043) VR=UT VM=1 Heat Source Description</summary>
        public readonly static DicomTagUT HeatSourceDescription = new DicomTagUT(0x0014, 0x6043);

        ///<summary>(0014,6044) VR=CS VM=1 Surface Preparation with Optical Coating</summary>
        public readonly static DicomTagCS SurfacePreparationWithOpticalCoating = new DicomTagCS(0x0014, 0x6044);

        ///<summary>(0014,6045) VR=ST VM=1 Optical Coating Type</summary>
        public readonly static DicomTagST OpticalCoatingType = new DicomTagST(0x0014, 0x6045);

        ///<summary>(0014,6046) VR=DS VM=1 Thermal Conductivity of Exposed Surface</summary>
        public readonly static DicomTagDS ThermalConductivityOfExposedSurface = new DicomTagDS(0x0014, 0x6046);

        ///<summary>(0014,6047) VR=DS VM=1 Material Density</summary>
        public readonly static DicomTagDS MaterialDensity = new DicomTagDS(0x0014, 0x6047);

        ///<summary>(0014,6048) VR=DS VM=1 Specific Heat of Inspection Surface</summary>
        public readonly static DicomTagDS SpecificHeatOfInspectionSurface = new DicomTagDS(0x0014, 0x6048);

        ///<summary>(0014,6049) VR=DS VM=1 Emissivity of Inspection Surface</summary>
        public readonly static DicomTagDS EmissivityOfInspectionSurface = new DicomTagDS(0x0014, 0x6049);

        ///<summary>(0014,604A) VR=CS VM=1-n Electromagnetic Classification of Inspection Surface</summary>
        public readonly static DicomTagCSs ElectromagneticClassificationOfInspectionSurface = new DicomTagCSs(0x0014, 0x604A);

        ///<summary>(0014,604C) VR=DS VM=1 Moving Window Size</summary>
        public readonly static DicomTagDS MovingWindowSize = new DicomTagDS(0x0014, 0x604C);

        ///<summary>(0014,604D) VR=CS VM=1 Moving Window Type</summary>
        public readonly static DicomTagCS MovingWindowType = new DicomTagCS(0x0014, 0x604D);

        ///<summary>(0014,604E) VR=DS VM=1-n Moving Window Weights</summary>
        public readonly static DicomTagDSs MovingWindowWeights = new DicomTagDSs(0x0014, 0x604E);

        ///<summary>(0014,604F) VR=DS VM=1 Moving Window Pitch</summary>
        public readonly static DicomTagDS MovingWindowPitch = new DicomTagDS(0x0014, 0x604F);

        ///<summary>(0014,6050) VR=CS VM=1 Moving Window Padding Scheme</summary>
        public readonly static DicomTagCS MovingWindowPaddingScheme = new DicomTagCS(0x0014, 0x6050);

        ///<summary>(0014,6051) VR=DS VM=1 Moving Window Padding Length</summary>
        public readonly static DicomTagDS MovingWindowPaddingLength = new DicomTagDS(0x0014, 0x6051);

        ///<summary>(0014,6052) VR=SQ VM=1 Spatial Filtering Parameters Sequence</summary>
        public readonly static DicomTagSQ SpatialFilteringParametersSequence = new DicomTagSQ(0x0014, 0x6052);

        ///<summary>(0014,6053) VR=CS VM=1 Spatial Filtering Scheme</summary>
        public readonly static DicomTagCS SpatialFilteringScheme = new DicomTagCS(0x0014, 0x6053);

        ///<summary>(0014,6056) VR=DS VM=1 Horizontal Moving Window Size</summary>
        public readonly static DicomTagDS HorizontalMovingWindowSize = new DicomTagDS(0x0014, 0x6056);

        ///<summary>(0014,6057) VR=DS VM=1 Vertical Moving Window Size</summary>
        public readonly static DicomTagDS VerticalMovingWindowSize = new DicomTagDS(0x0014, 0x6057);

        ///<summary>(0014,6059) VR=SQ VM=1 Polynomial Fitting Sequence</summary>
        public readonly static DicomTagSQ PolynomialFittingSequence = new DicomTagSQ(0x0014, 0x6059);

        ///<summary>(0014,605A) VR=CS VM=1-n Fitting Data Type</summary>
        public readonly static DicomTagCSs FittingDataType = new DicomTagCSs(0x0014, 0x605A);

        ///<summary>(0014,605B) VR=CS VM=1 Operation on Time Axis Before Fitting</summary>
        public readonly static DicomTagCS OperationOnTimeAxisBeforeFitting = new DicomTagCS(0x0014, 0x605B);

        ///<summary>(0014,605C) VR=CS VM=1 Operation on Pixel Intensity Before Fitting</summary>
        public readonly static DicomTagCS OperationOnPixelIntensityBeforeFitting = new DicomTagCS(0x0014, 0x605C);

        ///<summary>(0014,605D) VR=DS VM=1 Order of Polynomial</summary>
        public readonly static DicomTagDS OrderOfPolynomial = new DicomTagDS(0x0014, 0x605D);

        ///<summary>(0014,605E) VR=CS VM=1 Independent Variable for Polynomial Fit</summary>
        public readonly static DicomTagCS IndependentVariableForPolynomialFit = new DicomTagCS(0x0014, 0x605E);

        ///<summary>(0014,605F) VR=DS VM=1-n PolynomialCoefficients</summary>
        public readonly static DicomTagDSs PolynomialCoefficients = new DicomTagDSs(0x0014, 0x605F);

        ///<summary>(0014,6060) VR=CS VM=1 Thermography Pixel Data Unit</summary>
        public readonly static DicomTagCS ThermographyPixelDataUnit = new DicomTagCS(0x0014, 0x6060);

        ///<summary>(0016,0001) VR=DS VM=1 White Point</summary>
        public readonly static DicomTagDS WhitePoint = new DicomTagDS(0x0016, 0x0001);

        ///<summary>(0016,0002) VR=DS VM=3 Primary Chromaticities</summary>
        public readonly static DicomTagDSs PrimaryChromaticities = new DicomTagDSs(0x0016, 0x0002);

        ///<summary>(0016,0003) VR=UT VM=1 Battery Level</summary>
        public readonly static DicomTagUT BatteryLevel = new DicomTagUT(0x0016, 0x0003);

        ///<summary>(0016,0004) VR=DS VM=1 Exposure Time in Seconds</summary>
        public readonly static DicomTagDS ExposureTimeInSeconds = new DicomTagDS(0x0016, 0x0004);

        ///<summary>(0016,0005) VR=DS VM=1 F-Number</summary>
        public readonly static DicomTagDS FNumber = new DicomTagDS(0x0016, 0x0005);

        ///<summary>(0016,0006) VR=IS VM=1 OECF Rows</summary>
        public readonly static DicomTagIS OECFRows = new DicomTagIS(0x0016, 0x0006);

        ///<summary>(0016,0007) VR=IS VM=1 OECF Columns</summary>
        public readonly static DicomTagIS OECFColumns = new DicomTagIS(0x0016, 0x0007);

        ///<summary>(0016,0008) VR=UC VM=1-n OECF Column Names</summary>
        public readonly static DicomTagUCs OECFColumnNames = new DicomTagUCs(0x0016, 0x0008);

        ///<summary>(0016,0009) VR=DS VM=1-n OECF Values</summary>
        public readonly static DicomTagDSs OECFValues = new DicomTagDSs(0x0016, 0x0009);

        ///<summary>(0016,000A) VR=IS VM=1 Spatial Frequency Response Rows</summary>
        public readonly static DicomTagIS SpatialFrequencyResponseRows = new DicomTagIS(0x0016, 0x000A);

        ///<summary>(0016,000B) VR=IS VM=1 Spatial Frequency Response Columns</summary>
        public readonly static DicomTagIS SpatialFrequencyResponseColumns = new DicomTagIS(0x0016, 0x000B);

        ///<summary>(0016,000C) VR=UC VM=1-n Spatial Frequency Response Column Names</summary>
        public readonly static DicomTagUCs SpatialFrequencyResponseColumnNames = new DicomTagUCs(0x0016, 0x000C);

        ///<summary>(0016,000D) VR=DS VM=1-n Spatial Frequency Response Values</summary>
        public readonly static DicomTagDSs SpatialFrequencyResponseValues = new DicomTagDSs(0x0016, 0x000D);

        ///<summary>(0016,000E) VR=IS VM=1 Color Filter Array Pattern Rows</summary>
        public readonly static DicomTagIS ColorFilterArrayPatternRows = new DicomTagIS(0x0016, 0x000E);

        ///<summary>(0016,000F) VR=IS VM=1 Color Filter Array Pattern Columns</summary>
        public readonly static DicomTagIS ColorFilterArrayPatternColumns = new DicomTagIS(0x0016, 0x000F);

        ///<summary>(0016,0010) VR=DS VM=1-n Color Filter Array Pattern Values</summary>
        public readonly static DicomTagDSs ColorFilterArrayPatternValues = new DicomTagDSs(0x0016, 0x0010);

        ///<summary>(0016,0011) VR=US VM=1 Flash Firing Status</summary>
        public readonly static DicomTagUS FlashFiringStatus = new DicomTagUS(0x0016, 0x0011);

        ///<summary>(0016,0012) VR=US VM=1 Flash Return Status</summary>
        public readonly static DicomTagUS FlashReturnStatus = new DicomTagUS(0x0016, 0x0012);

        ///<summary>(0016,0013) VR=US VM=1 Flash Mode</summary>
        public readonly static DicomTagUS FlashMode = new DicomTagUS(0x0016, 0x0013);

        ///<summary>(0016,0014) VR=US VM=1 Flash Function Present</summary>
        public readonly static DicomTagUS FlashFunctionPresent = new DicomTagUS(0x0016, 0x0014);

        ///<summary>(0016,0015) VR=US VM=1 Flash Red Eye Mode</summary>
        public readonly static DicomTagUS FlashRedEyeMode = new DicomTagUS(0x0016, 0x0015);

        ///<summary>(0016,0016) VR=US VM=1 Exposure Program</summary>
        public readonly static DicomTagUS ExposureProgram = new DicomTagUS(0x0016, 0x0016);

        ///<summary>(0016,0017) VR=UT VM=1 Spectral Sensitivity</summary>
        public readonly static DicomTagUT SpectralSensitivity = new DicomTagUT(0x0016, 0x0017);

        ///<summary>(0016,0018) VR=IS VM=1 Photographic Sensitivity</summary>
        public readonly static DicomTagIS PhotographicSensitivity = new DicomTagIS(0x0016, 0x0018);

        ///<summary>(0016,0019) VR=IS VM=1 Self Timer Mode</summary>
        public readonly static DicomTagIS SelfTimerMode = new DicomTagIS(0x0016, 0x0019);

        ///<summary>(0016,001A) VR=US VM=1 Sensitivity Type</summary>
        public readonly static DicomTagUS SensitivityType = new DicomTagUS(0x0016, 0x001A);

        ///<summary>(0016,001B) VR=IS VM=1 Standard Output Sensitivity</summary>
        public readonly static DicomTagIS StandardOutputSensitivity = new DicomTagIS(0x0016, 0x001B);

        ///<summary>(0016,001C) VR=IS VM=1 Recommended Exposure Index</summary>
        public readonly static DicomTagIS RecommendedExposureIndex = new DicomTagIS(0x0016, 0x001C);

        ///<summary>(0016,001D) VR=IS VM=1 ISO Speed</summary>
        public readonly static DicomTagIS ISOSpeed = new DicomTagIS(0x0016, 0x001D);

        ///<summary>(0016,001E) VR=IS VM=1 ISO Speed Latitude yyy</summary>
        public readonly static DicomTagIS ISOSpeedLatitudeyyy = new DicomTagIS(0x0016, 0x001E);

        ///<summary>(0016,001F) VR=IS VM=1 ISO Speed Latitude zzz</summary>
        public readonly static DicomTagIS ISOSpeedLatitudezzz = new DicomTagIS(0x0016, 0x001F);

        ///<summary>(0016,0020) VR=UT VM=1 EXIF Version</summary>
        public readonly static DicomTagUT EXIFVersion = new DicomTagUT(0x0016, 0x0020);

        ///<summary>(0016,0021) VR=DS VM=1 Shutter Speed Value</summary>
        public readonly static DicomTagDS ShutterSpeedValue = new DicomTagDS(0x0016, 0x0021);

        ///<summary>(0016,0022) VR=DS VM=1 Aperture Value</summary>
        public readonly static DicomTagDS ApertureValue = new DicomTagDS(0x0016, 0x0022);

        ///<summary>(0016,0023) VR=DS VM=1 Brightness Value</summary>
        public readonly static DicomTagDS BrightnessValue = new DicomTagDS(0x0016, 0x0023);

        ///<summary>(0016,0024) VR=DS VM=1 Exposure Bias Value</summary>
        public readonly static DicomTagDS ExposureBiasValue = new DicomTagDS(0x0016, 0x0024);

        ///<summary>(0016,0025) VR=DS VM=1 Max Aperture Value</summary>
        public readonly static DicomTagDS MaxApertureValue = new DicomTagDS(0x0016, 0x0025);

        ///<summary>(0016,0026) VR=DS VM=1 Subject Distance</summary>
        public readonly static DicomTagDS SubjectDistance = new DicomTagDS(0x0016, 0x0026);

        ///<summary>(0016,0027) VR=US VM=1 Metering Mode</summary>
        public readonly static DicomTagUS MeteringMode = new DicomTagUS(0x0016, 0x0027);

        ///<summary>(0016,0028) VR=US VM=1 Light Source</summary>
        public readonly static DicomTagUS LightSource = new DicomTagUS(0x0016, 0x0028);

        ///<summary>(0016,0029) VR=DS VM=1 Focal Length</summary>
        public readonly static DicomTagDS FocalLength = new DicomTagDS(0x0016, 0x0029);

        ///<summary>(0016,002A) VR=IS VM=2-4 Subject Area</summary>
        public readonly static DicomTagISs SubjectArea = new DicomTagISs(0x0016, 0x002A);

        ///<summary>(0016,002B) VR=OB VM=1 Maker Note</summary>
        public readonly static DicomTagOB MakerNote = new DicomTagOB(0x0016, 0x002B);

        ///<summary>(0016,0030) VR=DS VM=1 Temperature</summary>
        public readonly static DicomTagDS Temperature = new DicomTagDS(0x0016, 0x0030);

        ///<summary>(0016,0031) VR=DS VM=1 Humidity</summary>
        public readonly static DicomTagDS Humidity = new DicomTagDS(0x0016, 0x0031);

        ///<summary>(0016,0032) VR=DS VM=1 Pressure</summary>
        public readonly static DicomTagDS Pressure = new DicomTagDS(0x0016, 0x0032);

        ///<summary>(0016,0033) VR=DS VM=1 Water Depth</summary>
        public readonly static DicomTagDS WaterDepth = new DicomTagDS(0x0016, 0x0033);

        ///<summary>(0016,0034) VR=DS VM=1 Acceleration</summary>
        public readonly static DicomTagDS Acceleration = new DicomTagDS(0x0016, 0x0034);

        ///<summary>(0016,0035) VR=DS VM=1 Camera Elevation Angle</summary>
        public readonly static DicomTagDS CameraElevationAngle = new DicomTagDS(0x0016, 0x0035);

        ///<summary>(0016,0036) VR=DS VM=1-2 Flash Energy</summary>
        public readonly static DicomTagDSs FlashEnergy = new DicomTagDSs(0x0016, 0x0036);

        ///<summary>(0016,0037) VR=IS VM=2 Subject Location</summary>
        public readonly static DicomTagISs SubjectLocation = new DicomTagISs(0x0016, 0x0037);

        ///<summary>(0016,0038) VR=DS VM=1 Photographic Exposure Index</summary>
        public readonly static DicomTagDS PhotographicExposureIndex = new DicomTagDS(0x0016, 0x0038);

        ///<summary>(0016,0039) VR=US VM=1 Sensing Method</summary>
        public readonly static DicomTagUS SensingMethod = new DicomTagUS(0x0016, 0x0039);

        ///<summary>(0016,003A) VR=US VM=1 File Source</summary>
        public readonly static DicomTagUS FileSource = new DicomTagUS(0x0016, 0x003A);

        ///<summary>(0016,003B) VR=US VM=1 Scene Type</summary>
        public readonly static DicomTagUS SceneType = new DicomTagUS(0x0016, 0x003B);

        ///<summary>(0016,0041) VR=US VM=1 Custom Rendered</summary>
        public readonly static DicomTagUS CustomRendered = new DicomTagUS(0x0016, 0x0041);

        ///<summary>(0016,0042) VR=US VM=1 Exposure Mode</summary>
        public readonly static DicomTagUS ExposureMode = new DicomTagUS(0x0016, 0x0042);

        ///<summary>(0016,0043) VR=US VM=1 White Balance</summary>
        public readonly static DicomTagUS WhiteBalance = new DicomTagUS(0x0016, 0x0043);

        ///<summary>(0016,0044) VR=DS VM=1 Digital Zoom Ratio</summary>
        public readonly static DicomTagDS DigitalZoomRatio = new DicomTagDS(0x0016, 0x0044);

        ///<summary>(0016,0045) VR=IS VM=1 Focal Length In 35mm Film</summary>
        public readonly static DicomTagIS FocalLengthIn35mmFilm = new DicomTagIS(0x0016, 0x0045);

        ///<summary>(0016,0046) VR=US VM=1 Scene Capture Type</summary>
        public readonly static DicomTagUS SceneCaptureType = new DicomTagUS(0x0016, 0x0046);

        ///<summary>(0016,0047) VR=US VM=1 Gain Control</summary>
        public readonly static DicomTagUS GainControl = new DicomTagUS(0x0016, 0x0047);

        ///<summary>(0016,0048) VR=US VM=1 Contrast</summary>
        public readonly static DicomTagUS Contrast = new DicomTagUS(0x0016, 0x0048);

        ///<summary>(0016,0049) VR=US VM=1 Saturation</summary>
        public readonly static DicomTagUS Saturation = new DicomTagUS(0x0016, 0x0049);

        ///<summary>(0016,004A) VR=US VM=1 Sharpness</summary>
        public readonly static DicomTagUS Sharpness = new DicomTagUS(0x0016, 0x004A);

        ///<summary>(0016,004B) VR=OB VM=1 Device Setting Description</summary>
        public readonly static DicomTagOB DeviceSettingDescription = new DicomTagOB(0x0016, 0x004B);

        ///<summary>(0016,004C) VR=US VM=1 Subject Distance Range</summary>
        public readonly static DicomTagUS SubjectDistanceRange = new DicomTagUS(0x0016, 0x004C);

        ///<summary>(0016,004D) VR=UT VM=1 Camera Owner Name</summary>
        public readonly static DicomTagUT CameraOwnerName = new DicomTagUT(0x0016, 0x004D);

        ///<summary>(0016,004E) VR=DS VM=4 Lens Specification</summary>
        public readonly static DicomTagDSs LensSpecification = new DicomTagDSs(0x0016, 0x004E);

        ///<summary>(0016,004F) VR=UT VM=1 Lens Make</summary>
        public readonly static DicomTagUT LensMake = new DicomTagUT(0x0016, 0x004F);

        ///<summary>(0016,0050) VR=UT VM=1 Lens Model</summary>
        public readonly static DicomTagUT LensModel = new DicomTagUT(0x0016, 0x0050);

        ///<summary>(0016,0051) VR=UT VM=1 Lens Serial Number</summary>
        public readonly static DicomTagUT LensSerialNumber = new DicomTagUT(0x0016, 0x0051);

        ///<summary>(0016,0061) VR=CS VM=1 Interoperability Index</summary>
        public readonly static DicomTagCS InteroperabilityIndex = new DicomTagCS(0x0016, 0x0061);

        ///<summary>(0016,0062) VR=OB VM=1 Interoperability Version</summary>
        public readonly static DicomTagOB InteroperabilityVersion = new DicomTagOB(0x0016, 0x0062);

        ///<summary>(0016,0070) VR=OB VM=1 GPS Version ID</summary>
        public readonly static DicomTagOB GPSVersionID = new DicomTagOB(0x0016, 0x0070);

        ///<summary>(0016,0071) VR=CS VM=1 GPS Latitude Ref</summary>
        public readonly static DicomTagCS GPSLatitudeRef = new DicomTagCS(0x0016, 0x0071);

        ///<summary>(0016,0072) VR=DS VM=3 GPS Latitude</summary>
        public readonly static DicomTagDSs GPSLatitude = new DicomTagDSs(0x0016, 0x0072);

        ///<summary>(0016,0073) VR=CS VM=1 GPS Longitude Ref</summary>
        public readonly static DicomTagCS GPSLongitudeRef = new DicomTagCS(0x0016, 0x0073);

        ///<summary>(0016,0074) VR=DS VM=3 GPS Longitude</summary>
        public readonly static DicomTagDSs GPSLongitude = new DicomTagDSs(0x0016, 0x0074);

        ///<summary>(0016,0075) VR=US VM=1 GPS Altitude Ref</summary>
        public readonly static DicomTagUS GPSAltitudeRef = new DicomTagUS(0x0016, 0x0075);

        ///<summary>(0016,0076) VR=DS VM=1 GPS Altitude</summary>
        public readonly static DicomTagDS GPSAltitude = new DicomTagDS(0x0016, 0x0076);

        ///<summary>(0016,0077) VR=DT VM=1 GPS Time Stamp</summary>
        public readonly static DicomTagDT GPSTimeStamp = new DicomTagDT(0x0016, 0x0077);

        ///<summary>(0016,0078) VR=UT VM=1 GPS Satellites</summary>
        public readonly static DicomTagUT GPSSatellites = new DicomTagUT(0x0016, 0x0078);

        ///<summary>(0016,0079) VR=CS VM=1 GPS Status</summary>
        public readonly static DicomTagCS GPSStatus = new DicomTagCS(0x0016, 0x0079);

        ///<summary>(0016,007A) VR=CS VM=1 GPS Measure Mode</summary>
        public readonly static DicomTagCS GPSMeasureMode = new DicomTagCS(0x0016, 0x007A);

        ///<summary>(0016,007B) VR=DS VM=1 GPS DOP</summary>
        public readonly static DicomTagDS GPSDOP = new DicomTagDS(0x0016, 0x007B);

        ///<summary>(0016,007C) VR=CS VM=1 GPS Speed Ref</summary>
        public readonly static DicomTagCS GPSSpeedRef = new DicomTagCS(0x0016, 0x007C);

        ///<summary>(0016,007D) VR=DS VM=1 GPS Speed</summary>
        public readonly static DicomTagDS GPSSpeed = new DicomTagDS(0x0016, 0x007D);

        ///<summary>(0016,007E) VR=CS VM=1 GPS Track Ref</summary>
        public readonly static DicomTagCS GPSTrackRef = new DicomTagCS(0x0016, 0x007E);

        ///<summary>(0016,007F) VR=DS VM=1 GPS Track</summary>
        public readonly static DicomTagDS GPSTrack = new DicomTagDS(0x0016, 0x007F);

        ///<summary>(0016,0080) VR=CS VM=1 GPS Img Direction Ref</summary>
        public readonly static DicomTagCS GPSImgDirectionRef = new DicomTagCS(0x0016, 0x0080);

        ///<summary>(0016,0081) VR=DS VM=1 GPS Img Direction</summary>
        public readonly static DicomTagDS GPSImgDirection = new DicomTagDS(0x0016, 0x0081);

        ///<summary>(0016,0082) VR=UT VM=1 GPS Map Datum</summary>
        public readonly static DicomTagUT GPSMapDatum = new DicomTagUT(0x0016, 0x0082);

        ///<summary>(0016,0083) VR=CS VM=1 GPS Dest Latitude Ref</summary>
        public readonly static DicomTagCS GPSDestLatitudeRef = new DicomTagCS(0x0016, 0x0083);

        ///<summary>(0016,0084) VR=DS VM=3 GPS Dest Latitude</summary>
        public readonly static DicomTagDSs GPSDestLatitude = new DicomTagDSs(0x0016, 0x0084);

        ///<summary>(0016,0085) VR=CS VM=1 GPS Dest Longitude Ref</summary>
        public readonly static DicomTagCS GPSDestLongitudeRef = new DicomTagCS(0x0016, 0x0085);

        ///<summary>(0016,0086) VR=DS VM=3 GPS Dest Longitude</summary>
        public readonly static DicomTagDSs GPSDestLongitude = new DicomTagDSs(0x0016, 0x0086);

        ///<summary>(0016,0087) VR=CS VM=1 GPS Dest Bearing Ref</summary>
        public readonly static DicomTagCS GPSDestBearingRef = new DicomTagCS(0x0016, 0x0087);

        ///<summary>(0016,0088) VR=DS VM=1 GPS Dest Bearing</summary>
        public readonly static DicomTagDS GPSDestBearing = new DicomTagDS(0x0016, 0x0088);

        ///<summary>(0016,0089) VR=CS VM=1 GPS Dest Distance Ref</summary>
        public readonly static DicomTagCS GPSDestDistanceRef = new DicomTagCS(0x0016, 0x0089);

        ///<summary>(0016,008A) VR=DS VM=1 GPS Dest Distance</summary>
        public readonly static DicomTagDS GPSDestDistance = new DicomTagDS(0x0016, 0x008A);

        ///<summary>(0016,008B) VR=OB VM=1 GPS Processing Method</summary>
        public readonly static DicomTagOB GPSProcessingMethod = new DicomTagOB(0x0016, 0x008B);

        ///<summary>(0016,008C) VR=OB VM=1 GPS Area Information</summary>
        public readonly static DicomTagOB GPSAreaInformation = new DicomTagOB(0x0016, 0x008C);

        ///<summary>(0016,008D) VR=DT VM=1 GPS Date Stamp</summary>
        public readonly static DicomTagDT GPSDateStamp = new DicomTagDT(0x0016, 0x008D);

        ///<summary>(0016,008E) VR=IS VM=1 GPS Differential</summary>
        public readonly static DicomTagIS GPSDifferential = new DicomTagIS(0x0016, 0x008E);

        ///<summary>(0016,1001) VR=CS VM=1 Light Source Polarization</summary>
        public readonly static DicomTagCS LightSourcePolarization = new DicomTagCS(0x0016, 0x1001);

        ///<summary>(0016,1002) VR=DS VM=1 Emitter Color Temperature</summary>
        public readonly static DicomTagDS EmitterColorTemperature = new DicomTagDS(0x0016, 0x1002);

        ///<summary>(0016,1003) VR=CS VM=1 Contact Method</summary>
        public readonly static DicomTagCS ContactMethod = new DicomTagCS(0x0016, 0x1003);

        ///<summary>(0016,1004) VR=CS VM=1-n Immersion Media</summary>
        public readonly static DicomTagCSs ImmersionMedia = new DicomTagCSs(0x0016, 0x1004);

        ///<summary>(0016,1005) VR=DS VM=1 Optical Magnification Factor</summary>
        public readonly static DicomTagDS OpticalMagnificationFactor = new DicomTagDS(0x0016, 0x1005);

        ///<summary>(0018,0010) VR=LO VM=1 Contrast/Bolus Agent</summary>
        public readonly static DicomTagLO ContrastBolusAgent = new DicomTagLO(0x0018, 0x0010);

        ///<summary>(0018,0012) VR=SQ VM=1 Contrast/Bolus Agent Sequence</summary>
        public readonly static DicomTagSQ ContrastBolusAgentSequence = new DicomTagSQ(0x0018, 0x0012);

        ///<summary>(0018,0013) VR=FL VM=1 Contrast/Bolus T1 Relaxivity</summary>
        public readonly static DicomTagFL ContrastBolusT1Relaxivity = new DicomTagFL(0x0018, 0x0013);

        ///<summary>(0018,0014) VR=SQ VM=1 Contrast/Bolus Administration Route Sequence</summary>
        public readonly static DicomTagSQ ContrastBolusAdministrationRouteSequence = new DicomTagSQ(0x0018, 0x0014);

        ///<summary>(0018,0015) VR=CS VM=1 Body Part Examined</summary>
        public readonly static DicomTagCS BodyPartExamined = new DicomTagCS(0x0018, 0x0015);

        ///<summary>(0018,0020) VR=CS VM=1-n Scanning Sequence</summary>
        public readonly static DicomTagCSs ScanningSequence = new DicomTagCSs(0x0018, 0x0020);

        ///<summary>(0018,0021) VR=CS VM=1-n Sequence Variant</summary>
        public readonly static DicomTagCSs SequenceVariant = new DicomTagCSs(0x0018, 0x0021);

        ///<summary>(0018,0022) VR=CS VM=1-n Scan Options</summary>
        public readonly static DicomTagCSs ScanOptions = new DicomTagCSs(0x0018, 0x0022);

        ///<summary>(0018,0023) VR=CS VM=1 MR Acquisition Type</summary>
        public readonly static DicomTagCS MRAcquisitionType = new DicomTagCS(0x0018, 0x0023);

        ///<summary>(0018,0024) VR=SH VM=1 Sequence Name</summary>
        public readonly static DicomTagSH SequenceName = new DicomTagSH(0x0018, 0x0024);

        ///<summary>(0018,0025) VR=CS VM=1 Angio Flag</summary>
        public readonly static DicomTagCS AngioFlag = new DicomTagCS(0x0018, 0x0025);

        ///<summary>(0018,0026) VR=SQ VM=1 Intervention Drug Information Sequence</summary>
        public readonly static DicomTagSQ InterventionDrugInformationSequence = new DicomTagSQ(0x0018, 0x0026);

        ///<summary>(0018,0027) VR=TM VM=1 Intervention Drug Stop Time</summary>
        public readonly static DicomTagTM InterventionDrugStopTime = new DicomTagTM(0x0018, 0x0027);

        ///<summary>(0018,0028) VR=DS VM=1 Intervention Drug Dose</summary>
        public readonly static DicomTagDS InterventionDrugDose = new DicomTagDS(0x0018, 0x0028);

        ///<summary>(0018,0029) VR=SQ VM=1 Intervention Drug Code Sequence</summary>
        public readonly static DicomTagSQ InterventionDrugCodeSequence = new DicomTagSQ(0x0018, 0x0029);

        ///<summary>(0018,002A) VR=SQ VM=1 Additional Drug Sequence</summary>
        public readonly static DicomTagSQ AdditionalDrugSequence = new DicomTagSQ(0x0018, 0x002A);

        ///<summary>(0018,0030) VR=LO VM=1-n Radionuclide (RETIRED)</summary>
        public readonly static DicomTagLOs RadionuclideRETIRED = new DicomTagLOs(0x0018, 0x0030);

        ///<summary>(0018,0031) VR=LO VM=1 Radiopharmaceutical</summary>
        public readonly static DicomTagLO Radiopharmaceutical = new DicomTagLO(0x0018, 0x0031);

        ///<summary>(0018,0032) VR=DS VM=1 Energy Window Centerline (RETIRED)</summary>
        public readonly static DicomTagDS EnergyWindowCenterlineRETIRED = new DicomTagDS(0x0018, 0x0032);

        ///<summary>(0018,0033) VR=DS VM=1-n Energy Window Total Width (RETIRED)</summary>
        public readonly static DicomTagDSs EnergyWindowTotalWidthRETIRED = new DicomTagDSs(0x0018, 0x0033);

        ///<summary>(0018,0034) VR=LO VM=1 Intervention Drug Name</summary>
        public readonly static DicomTagLO InterventionDrugName = new DicomTagLO(0x0018, 0x0034);

        ///<summary>(0018,0035) VR=TM VM=1 Intervention Drug Start Time</summary>
        public readonly static DicomTagTM InterventionDrugStartTime = new DicomTagTM(0x0018, 0x0035);

        ///<summary>(0018,0036) VR=SQ VM=1 Intervention Sequence</summary>
        public readonly static DicomTagSQ InterventionSequence = new DicomTagSQ(0x0018, 0x0036);

        ///<summary>(0018,0037) VR=CS VM=1 Therapy Type (RETIRED)</summary>
        public readonly static DicomTagCS TherapyTypeRETIRED = new DicomTagCS(0x0018, 0x0037);

        ///<summary>(0018,0038) VR=CS VM=1 Intervention Status</summary>
        public readonly static DicomTagCS InterventionStatus = new DicomTagCS(0x0018, 0x0038);

        ///<summary>(0018,0039) VR=CS VM=1 Therapy Description (RETIRED)</summary>
        public readonly static DicomTagCS TherapyDescriptionRETIRED = new DicomTagCS(0x0018, 0x0039);

        ///<summary>(0018,003A) VR=ST VM=1 Intervention Description</summary>
        public readonly static DicomTagST InterventionDescription = new DicomTagST(0x0018, 0x003A);

        ///<summary>(0018,0040) VR=IS VM=1 Cine Rate</summary>
        public readonly static DicomTagIS CineRate = new DicomTagIS(0x0018, 0x0040);

        ///<summary>(0018,0042) VR=CS VM=1 Initial Cine Run State</summary>
        public readonly static DicomTagCS InitialCineRunState = new DicomTagCS(0x0018, 0x0042);

        ///<summary>(0018,0050) VR=DS VM=1 Slice Thickness</summary>
        public readonly static DicomTagDS SliceThickness = new DicomTagDS(0x0018, 0x0050);

        ///<summary>(0018,0060) VR=DS VM=1 KVP</summary>
        public readonly static DicomTagDS KVP = new DicomTagDS(0x0018, 0x0060);

        ///<summary>(0018,0070) VR=IS VM=1 Counts Accumulated</summary>
        public readonly static DicomTagIS CountsAccumulated = new DicomTagIS(0x0018, 0x0070);

        ///<summary>(0018,0071) VR=CS VM=1 Acquisition Termination Condition</summary>
        public readonly static DicomTagCS AcquisitionTerminationCondition = new DicomTagCS(0x0018, 0x0071);

        ///<summary>(0018,0072) VR=DS VM=1 Effective Duration</summary>
        public readonly static DicomTagDS EffectiveDuration = new DicomTagDS(0x0018, 0x0072);

        ///<summary>(0018,0073) VR=CS VM=1 Acquisition Start Condition</summary>
        public readonly static DicomTagCS AcquisitionStartCondition = new DicomTagCS(0x0018, 0x0073);

        ///<summary>(0018,0074) VR=IS VM=1 Acquisition Start Condition Data</summary>
        public readonly static DicomTagIS AcquisitionStartConditionData = new DicomTagIS(0x0018, 0x0074);

        ///<summary>(0018,0075) VR=IS VM=1 Acquisition Termination Condition Data</summary>
        public readonly static DicomTagIS AcquisitionTerminationConditionData = new DicomTagIS(0x0018, 0x0075);

        ///<summary>(0018,0080) VR=DS VM=1 Repetition Time</summary>
        public readonly static DicomTagDS RepetitionTime = new DicomTagDS(0x0018, 0x0080);

        ///<summary>(0018,0081) VR=DS VM=1 Echo Time</summary>
        public readonly static DicomTagDS EchoTime = new DicomTagDS(0x0018, 0x0081);

        ///<summary>(0018,0082) VR=DS VM=1 Inversion Time</summary>
        public readonly static DicomTagDS InversionTime = new DicomTagDS(0x0018, 0x0082);

        ///<summary>(0018,0083) VR=DS VM=1 Number of Averages</summary>
        public readonly static DicomTagDS NumberOfAverages = new DicomTagDS(0x0018, 0x0083);

        ///<summary>(0018,0084) VR=DS VM=1 Imaging Frequency</summary>
        public readonly static DicomTagDS ImagingFrequency = new DicomTagDS(0x0018, 0x0084);

        ///<summary>(0018,0085) VR=SH VM=1 Imaged Nucleus</summary>
        public readonly static DicomTagSH ImagedNucleus = new DicomTagSH(0x0018, 0x0085);

        ///<summary>(0018,0086) VR=IS VM=1-n Echo Number(s)</summary>
        public readonly static DicomTagISs EchoNumbers = new DicomTagISs(0x0018, 0x0086);

        ///<summary>(0018,0087) VR=DS VM=1 Magnetic Field Strength</summary>
        public readonly static DicomTagDS MagneticFieldStrength = new DicomTagDS(0x0018, 0x0087);

        ///<summary>(0018,0088) VR=DS VM=1 Spacing Between Slices</summary>
        public readonly static DicomTagDS SpacingBetweenSlices = new DicomTagDS(0x0018, 0x0088);

        ///<summary>(0018,0089) VR=IS VM=1 Number of Phase Encoding Steps</summary>
        public readonly static DicomTagIS NumberOfPhaseEncodingSteps = new DicomTagIS(0x0018, 0x0089);

        ///<summary>(0018,0090) VR=DS VM=1 Data Collection Diameter</summary>
        public readonly static DicomTagDS DataCollectionDiameter = new DicomTagDS(0x0018, 0x0090);

        ///<summary>(0018,0091) VR=IS VM=1 Echo Train Length</summary>
        public readonly static DicomTagIS EchoTrainLength = new DicomTagIS(0x0018, 0x0091);

        ///<summary>(0018,0093) VR=DS VM=1 Percent Sampling</summary>
        public readonly static DicomTagDS PercentSampling = new DicomTagDS(0x0018, 0x0093);

        ///<summary>(0018,0094) VR=DS VM=1 Percent Phase Field of View</summary>
        public readonly static DicomTagDS PercentPhaseFieldOfView = new DicomTagDS(0x0018, 0x0094);

        ///<summary>(0018,0095) VR=DS VM=1 Pixel Bandwidth</summary>
        public readonly static DicomTagDS PixelBandwidth = new DicomTagDS(0x0018, 0x0095);

        ///<summary>(0018,1000) VR=LO VM=1 Device Serial Number</summary>
        public readonly static DicomTagLO DeviceSerialNumber = new DicomTagLO(0x0018, 0x1000);

        ///<summary>(0018,1002) VR=UI VM=1 Device UID</summary>
        public readonly static DicomTagUI DeviceUID = new DicomTagUI(0x0018, 0x1002);

        ///<summary>(0018,1003) VR=LO VM=1 Device ID</summary>
        public readonly static DicomTagLO DeviceID = new DicomTagLO(0x0018, 0x1003);

        ///<summary>(0018,1004) VR=LO VM=1 Plate ID</summary>
        public readonly static DicomTagLO PlateID = new DicomTagLO(0x0018, 0x1004);

        ///<summary>(0018,1005) VR=LO VM=1 Generator ID</summary>
        public readonly static DicomTagLO GeneratorID = new DicomTagLO(0x0018, 0x1005);

        ///<summary>(0018,1006) VR=LO VM=1 Grid ID</summary>
        public readonly static DicomTagLO GridID = new DicomTagLO(0x0018, 0x1006);

        ///<summary>(0018,1007) VR=LO VM=1 Cassette ID</summary>
        public readonly static DicomTagLO CassetteID = new DicomTagLO(0x0018, 0x1007);

        ///<summary>(0018,1008) VR=LO VM=1 Gantry ID</summary>
        public readonly static DicomTagLO GantryID = new DicomTagLO(0x0018, 0x1008);

        ///<summary>(0018,1009) VR=UT VM=1 Unique Device Identifier</summary>
        public readonly static DicomTagUT UniqueDeviceIdentifier = new DicomTagUT(0x0018, 0x1009);

        ///<summary>(0018,100A) VR=SQ VM=1 UDI Sequence</summary>
        public readonly static DicomTagSQ UDISequence = new DicomTagSQ(0x0018, 0x100A);

        ///<summary>(0018,100B) VR=UI VM=1-n Manufacturer's Device Class UID</summary>
        public readonly static DicomTagUIs ManufacturerDeviceClassUID = new DicomTagUIs(0x0018, 0x100B);

        ///<summary>(0018,1010) VR=LO VM=1 Secondary Capture Device ID</summary>
        public readonly static DicomTagLO SecondaryCaptureDeviceID = new DicomTagLO(0x0018, 0x1010);

        ///<summary>(0018,1011) VR=LO VM=1 Hardcopy Creation Device ID (RETIRED)</summary>
        public readonly static DicomTagLO HardcopyCreationDeviceIDRETIRED = new DicomTagLO(0x0018, 0x1011);

        ///<summary>(0018,1012) VR=DA VM=1 Date of Secondary Capture</summary>
        public readonly static DicomTagDA DateOfSecondaryCapture = new DicomTagDA(0x0018, 0x1012);

        ///<summary>(0018,1014) VR=TM VM=1 Time of Secondary Capture</summary>
        public readonly static DicomTagTM TimeOfSecondaryCapture = new DicomTagTM(0x0018, 0x1014);

        ///<summary>(0018,1016) VR=LO VM=1 Secondary Capture Device Manufacturer</summary>
        public readonly static DicomTagLO SecondaryCaptureDeviceManufacturer = new DicomTagLO(0x0018, 0x1016);

        ///<summary>(0018,1017) VR=LO VM=1 Hardcopy Device Manufacturer (RETIRED)</summary>
        public readonly static DicomTagLO HardcopyDeviceManufacturerRETIRED = new DicomTagLO(0x0018, 0x1017);

        ///<summary>(0018,1018) VR=LO VM=1 Secondary Capture Device Manufacturer's Model Name</summary>
        public readonly static DicomTagLO SecondaryCaptureDeviceManufacturerModelName = new DicomTagLO(0x0018, 0x1018);

        ///<summary>(0018,1019) VR=LO VM=1-n Secondary Capture Device Software Versions</summary>
        public readonly static DicomTagLOs SecondaryCaptureDeviceSoftwareVersions = new DicomTagLOs(0x0018, 0x1019);

        ///<summary>(0018,101A) VR=LO VM=1-n Hardcopy Device Software Version (RETIRED)</summary>
        public readonly static DicomTagLOs HardcopyDeviceSoftwareVersionRETIRED = new DicomTagLOs(0x0018, 0x101A);

        ///<summary>(0018,101B) VR=LO VM=1 Hardcopy Device Manufacturer's Model Name (RETIRED)</summary>
        public readonly static DicomTagLO HardcopyDeviceManufacturerModelNameRETIRED = new DicomTagLO(0x0018, 0x101B);

        ///<summary>(0018,1020) VR=LO VM=1-n Software Versions</summary>
        public readonly static DicomTagLOs SoftwareVersions = new DicomTagLOs(0x0018, 0x1020);

        ///<summary>(0018,1022) VR=SH VM=1 Video Image Format Acquired</summary>
        public readonly static DicomTagSH VideoImageFormatAcquired = new DicomTagSH(0x0018, 0x1022);

        ///<summary>(0018,1023) VR=LO VM=1 Digital Image Format Acquired</summary>
        public readonly static DicomTagLO DigitalImageFormatAcquired = new DicomTagLO(0x0018, 0x1023);

        ///<summary>(0018,1030) VR=LO VM=1 Protocol Name</summary>
        public readonly static DicomTagLO ProtocolName = new DicomTagLO(0x0018, 0x1030);

        ///<summary>(0018,1040) VR=LO VM=1 Contrast/Bolus Route</summary>
        public readonly static DicomTagLO ContrastBolusRoute = new DicomTagLO(0x0018, 0x1040);

        ///<summary>(0018,1041) VR=DS VM=1 Contrast/Bolus Volume</summary>
        public readonly static DicomTagDS ContrastBolusVolume = new DicomTagDS(0x0018, 0x1041);

        ///<summary>(0018,1042) VR=TM VM=1 Contrast/Bolus Start Time</summary>
        public readonly static DicomTagTM ContrastBolusStartTime = new DicomTagTM(0x0018, 0x1042);

        ///<summary>(0018,1043) VR=TM VM=1 Contrast/Bolus Stop Time</summary>
        public readonly static DicomTagTM ContrastBolusStopTime = new DicomTagTM(0x0018, 0x1043);

        ///<summary>(0018,1044) VR=DS VM=1 Contrast/Bolus Total Dose</summary>
        public readonly static DicomTagDS ContrastBolusTotalDose = new DicomTagDS(0x0018, 0x1044);

        ///<summary>(0018,1045) VR=IS VM=1 Syringe Counts</summary>
        public readonly static DicomTagIS SyringeCounts = new DicomTagIS(0x0018, 0x1045);

        ///<summary>(0018,1046) VR=DS VM=1-n Contrast Flow Rate</summary>
        public readonly static DicomTagDSs ContrastFlowRate = new DicomTagDSs(0x0018, 0x1046);

        ///<summary>(0018,1047) VR=DS VM=1-n Contrast Flow Duration</summary>
        public readonly static DicomTagDSs ContrastFlowDuration = new DicomTagDSs(0x0018, 0x1047);

        ///<summary>(0018,1048) VR=CS VM=1 Contrast/Bolus Ingredient</summary>
        public readonly static DicomTagCS ContrastBolusIngredient = new DicomTagCS(0x0018, 0x1048);

        ///<summary>(0018,1049) VR=DS VM=1 Contrast/Bolus Ingredient Concentration</summary>
        public readonly static DicomTagDS ContrastBolusIngredientConcentration = new DicomTagDS(0x0018, 0x1049);

        ///<summary>(0018,1050) VR=DS VM=1 Spatial Resolution</summary>
        public readonly static DicomTagDS SpatialResolution = new DicomTagDS(0x0018, 0x1050);

        ///<summary>(0018,1060) VR=DS VM=1 Trigger Time</summary>
        public readonly static DicomTagDS TriggerTime = new DicomTagDS(0x0018, 0x1060);

        ///<summary>(0018,1061) VR=LO VM=1 Trigger Source or Type</summary>
        public readonly static DicomTagLO TriggerSourceOrType = new DicomTagLO(0x0018, 0x1061);

        ///<summary>(0018,1062) VR=IS VM=1 Nominal Interval</summary>
        public readonly static DicomTagIS NominalInterval = new DicomTagIS(0x0018, 0x1062);

        ///<summary>(0018,1063) VR=DS VM=1 Frame Time</summary>
        public readonly static DicomTagDS FrameTime = new DicomTagDS(0x0018, 0x1063);

        ///<summary>(0018,1064) VR=LO VM=1 Cardiac Framing Type</summary>
        public readonly static DicomTagLO CardiacFramingType = new DicomTagLO(0x0018, 0x1064);

        ///<summary>(0018,1065) VR=DS VM=1-n Frame Time Vector</summary>
        public readonly static DicomTagDSs FrameTimeVector = new DicomTagDSs(0x0018, 0x1065);

        ///<summary>(0018,1066) VR=DS VM=1 Frame Delay</summary>
        public readonly static DicomTagDS FrameDelay = new DicomTagDS(0x0018, 0x1066);

        ///<summary>(0018,1067) VR=DS VM=1 Image Trigger Delay</summary>
        public readonly static DicomTagDS ImageTriggerDelay = new DicomTagDS(0x0018, 0x1067);

        ///<summary>(0018,1068) VR=DS VM=1 Multiplex Group Time Offset</summary>
        public readonly static DicomTagDS MultiplexGroupTimeOffset = new DicomTagDS(0x0018, 0x1068);

        ///<summary>(0018,1069) VR=DS VM=1 Trigger Time Offset</summary>
        public readonly static DicomTagDS TriggerTimeOffset = new DicomTagDS(0x0018, 0x1069);

        ///<summary>(0018,106A) VR=CS VM=1 Synchronization Trigger</summary>
        public readonly static DicomTagCS SynchronizationTrigger = new DicomTagCS(0x0018, 0x106A);

        ///<summary>(0018,106C) VR=US VM=2 Synchronization Channel</summary>
        public readonly static DicomTagUSs SynchronizationChannel = new DicomTagUSs(0x0018, 0x106C);

        ///<summary>(0018,106E) VR=UL VM=1 Trigger Sample Position</summary>
        public readonly static DicomTagUL TriggerSamplePosition = new DicomTagUL(0x0018, 0x106E);

        ///<summary>(0018,1070) VR=LO VM=1 Radiopharmaceutical Route</summary>
        public readonly static DicomTagLO RadiopharmaceuticalRoute = new DicomTagLO(0x0018, 0x1070);

        ///<summary>(0018,1071) VR=DS VM=1 Radiopharmaceutical Volume</summary>
        public readonly static DicomTagDS RadiopharmaceuticalVolume = new DicomTagDS(0x0018, 0x1071);

        ///<summary>(0018,1072) VR=TM VM=1 Radiopharmaceutical Start Time</summary>
        public readonly static DicomTagTM RadiopharmaceuticalStartTime = new DicomTagTM(0x0018, 0x1072);

        ///<summary>(0018,1073) VR=TM VM=1 Radiopharmaceutical Stop Time</summary>
        public readonly static DicomTagTM RadiopharmaceuticalStopTime = new DicomTagTM(0x0018, 0x1073);

        ///<summary>(0018,1074) VR=DS VM=1 Radionuclide Total Dose</summary>
        public readonly static DicomTagDS RadionuclideTotalDose = new DicomTagDS(0x0018, 0x1074);

        ///<summary>(0018,1075) VR=DS VM=1 Radionuclide Half Life</summary>
        public readonly static DicomTagDS RadionuclideHalfLife = new DicomTagDS(0x0018, 0x1075);

        ///<summary>(0018,1076) VR=DS VM=1 Radionuclide Positron Fraction</summary>
        public readonly static DicomTagDS RadionuclidePositronFraction = new DicomTagDS(0x0018, 0x1076);

        ///<summary>(0018,1077) VR=DS VM=1 Radiopharmaceutical Specific Activity</summary>
        public readonly static DicomTagDS RadiopharmaceuticalSpecificActivity = new DicomTagDS(0x0018, 0x1077);

        ///<summary>(0018,1078) VR=DT VM=1 Radiopharmaceutical Start DateTime</summary>
        public readonly static DicomTagDT RadiopharmaceuticalStartDateTime = new DicomTagDT(0x0018, 0x1078);

        ///<summary>(0018,1079) VR=DT VM=1 Radiopharmaceutical Stop DateTime</summary>
        public readonly static DicomTagDT RadiopharmaceuticalStopDateTime = new DicomTagDT(0x0018, 0x1079);

        ///<summary>(0018,1080) VR=CS VM=1 Beat Rejection Flag</summary>
        public readonly static DicomTagCS BeatRejectionFlag = new DicomTagCS(0x0018, 0x1080);

        ///<summary>(0018,1081) VR=IS VM=1 Low R-R Value</summary>
        public readonly static DicomTagIS LowRRValue = new DicomTagIS(0x0018, 0x1081);

        ///<summary>(0018,1082) VR=IS VM=1 High R-R Value</summary>
        public readonly static DicomTagIS HighRRValue = new DicomTagIS(0x0018, 0x1082);

        ///<summary>(0018,1083) VR=IS VM=1 Intervals Acquired</summary>
        public readonly static DicomTagIS IntervalsAcquired = new DicomTagIS(0x0018, 0x1083);

        ///<summary>(0018,1084) VR=IS VM=1 Intervals Rejected</summary>
        public readonly static DicomTagIS IntervalsRejected = new DicomTagIS(0x0018, 0x1084);

        ///<summary>(0018,1085) VR=LO VM=1 PVC Rejection</summary>
        public readonly static DicomTagLO PVCRejection = new DicomTagLO(0x0018, 0x1085);

        ///<summary>(0018,1086) VR=IS VM=1 Skip Beats</summary>
        public readonly static DicomTagIS SkipBeats = new DicomTagIS(0x0018, 0x1086);

        ///<summary>(0018,1088) VR=IS VM=1 Heart Rate</summary>
        public readonly static DicomTagIS HeartRate = new DicomTagIS(0x0018, 0x1088);

        ///<summary>(0018,1090) VR=IS VM=1 Cardiac Number of Images</summary>
        public readonly static DicomTagIS CardiacNumberOfImages = new DicomTagIS(0x0018, 0x1090);

        ///<summary>(0018,1094) VR=IS VM=1 Trigger Window</summary>
        public readonly static DicomTagIS TriggerWindow = new DicomTagIS(0x0018, 0x1094);

        ///<summary>(0018,1100) VR=DS VM=1 Reconstruction Diameter</summary>
        public readonly static DicomTagDS ReconstructionDiameter = new DicomTagDS(0x0018, 0x1100);

        ///<summary>(0018,1110) VR=DS VM=1 Distance Source to Detector</summary>
        public readonly static DicomTagDS DistanceSourceToDetector = new DicomTagDS(0x0018, 0x1110);

        ///<summary>(0018,1111) VR=DS VM=1 Distance Source to Patient</summary>
        public readonly static DicomTagDS DistanceSourceToPatient = new DicomTagDS(0x0018, 0x1111);

        ///<summary>(0018,1114) VR=DS VM=1 Estimated Radiographic Magnification Factor</summary>
        public readonly static DicomTagDS EstimatedRadiographicMagnificationFactor = new DicomTagDS(0x0018, 0x1114);

        ///<summary>(0018,1120) VR=DS VM=1 Gantry/Detector Tilt</summary>
        public readonly static DicomTagDS GantryDetectorTilt = new DicomTagDS(0x0018, 0x1120);

        ///<summary>(0018,1121) VR=DS VM=1 Gantry/Detector Slew</summary>
        public readonly static DicomTagDS GantryDetectorSlew = new DicomTagDS(0x0018, 0x1121);

        ///<summary>(0018,1130) VR=DS VM=1 Table Height</summary>
        public readonly static DicomTagDS TableHeight = new DicomTagDS(0x0018, 0x1130);

        ///<summary>(0018,1131) VR=DS VM=1 Table Traverse</summary>
        public readonly static DicomTagDS TableTraverse = new DicomTagDS(0x0018, 0x1131);

        ///<summary>(0018,1134) VR=CS VM=1 Table Motion</summary>
        public readonly static DicomTagCS TableMotion = new DicomTagCS(0x0018, 0x1134);

        ///<summary>(0018,1135) VR=DS VM=1-n Table Vertical Increment</summary>
        public readonly static DicomTagDSs TableVerticalIncrement = new DicomTagDSs(0x0018, 0x1135);

        ///<summary>(0018,1136) VR=DS VM=1-n Table Lateral Increment</summary>
        public readonly static DicomTagDSs TableLateralIncrement = new DicomTagDSs(0x0018, 0x1136);

        ///<summary>(0018,1137) VR=DS VM=1-n Table Longitudinal Increment</summary>
        public readonly static DicomTagDSs TableLongitudinalIncrement = new DicomTagDSs(0x0018, 0x1137);

        ///<summary>(0018,1138) VR=DS VM=1 Table Angle</summary>
        public readonly static DicomTagDS TableAngle = new DicomTagDS(0x0018, 0x1138);

        ///<summary>(0018,113A) VR=CS VM=1 Table Type</summary>
        public readonly static DicomTagCS TableType = new DicomTagCS(0x0018, 0x113A);

        ///<summary>(0018,1140) VR=CS VM=1 Rotation Direction</summary>
        public readonly static DicomTagCS RotationDirection = new DicomTagCS(0x0018, 0x1140);

        ///<summary>(0018,1141) VR=DS VM=1 Angular Position (RETIRED)</summary>
        public readonly static DicomTagDS AngularPositionRETIRED = new DicomTagDS(0x0018, 0x1141);

        ///<summary>(0018,1142) VR=DS VM=1-n Radial Position</summary>
        public readonly static DicomTagDSs RadialPosition = new DicomTagDSs(0x0018, 0x1142);

        ///<summary>(0018,1143) VR=DS VM=1 Scan Arc</summary>
        public readonly static DicomTagDS ScanArc = new DicomTagDS(0x0018, 0x1143);

        ///<summary>(0018,1144) VR=DS VM=1 Angular Step</summary>
        public readonly static DicomTagDS AngularStep = new DicomTagDS(0x0018, 0x1144);

        ///<summary>(0018,1145) VR=DS VM=1 Center of Rotation Offset</summary>
        public readonly static DicomTagDS CenterOfRotationOffset = new DicomTagDS(0x0018, 0x1145);

        ///<summary>(0018,1146) VR=DS VM=1-n Rotation Offset (RETIRED)</summary>
        public readonly static DicomTagDSs RotationOffsetRETIRED = new DicomTagDSs(0x0018, 0x1146);

        ///<summary>(0018,1147) VR=CS VM=1 Field of View Shape</summary>
        public readonly static DicomTagCS FieldOfViewShape = new DicomTagCS(0x0018, 0x1147);

        ///<summary>(0018,1149) VR=IS VM=1-2 Field of View Dimension(s)</summary>
        public readonly static DicomTagISs FieldOfViewDimensions = new DicomTagISs(0x0018, 0x1149);

        ///<summary>(0018,1150) VR=IS VM=1 Exposure Time</summary>
        public readonly static DicomTagIS ExposureTime = new DicomTagIS(0x0018, 0x1150);

        ///<summary>(0018,1151) VR=IS VM=1 X-Ray Tube Current</summary>
        public readonly static DicomTagIS XRayTubeCurrent = new DicomTagIS(0x0018, 0x1151);

        ///<summary>(0018,1152) VR=IS VM=1 Exposure</summary>
        public readonly static DicomTagIS Exposure = new DicomTagIS(0x0018, 0x1152);

        ///<summary>(0018,1153) VR=IS VM=1 Exposure in µAs</summary>
        public readonly static DicomTagIS ExposureInuAs = new DicomTagIS(0x0018, 0x1153);

        ///<summary>(0018,1154) VR=DS VM=1 Average Pulse Width</summary>
        public readonly static DicomTagDS AveragePulseWidth = new DicomTagDS(0x0018, 0x1154);

        ///<summary>(0018,1155) VR=CS VM=1 Radiation Setting</summary>
        public readonly static DicomTagCS RadiationSetting = new DicomTagCS(0x0018, 0x1155);

        ///<summary>(0018,1156) VR=CS VM=1 Rectification Type</summary>
        public readonly static DicomTagCS RectificationType = new DicomTagCS(0x0018, 0x1156);

        ///<summary>(0018,115A) VR=CS VM=1 Radiation Mode</summary>
        public readonly static DicomTagCS RadiationMode = new DicomTagCS(0x0018, 0x115A);

        ///<summary>(0018,115E) VR=DS VM=1 Image and Fluoroscopy Area Dose Product</summary>
        public readonly static DicomTagDS ImageAndFluoroscopyAreaDoseProduct = new DicomTagDS(0x0018, 0x115E);

        ///<summary>(0018,1160) VR=SH VM=1 Filter Type</summary>
        public readonly static DicomTagSH FilterType = new DicomTagSH(0x0018, 0x1160);

        ///<summary>(0018,1161) VR=LO VM=1-n Type of Filters</summary>
        public readonly static DicomTagLOs TypeOfFilters = new DicomTagLOs(0x0018, 0x1161);

        ///<summary>(0018,1162) VR=DS VM=1 Intensifier Size</summary>
        public readonly static DicomTagDS IntensifierSize = new DicomTagDS(0x0018, 0x1162);

        ///<summary>(0018,1164) VR=DS VM=2 Imager Pixel Spacing</summary>
        public readonly static DicomTagDSs ImagerPixelSpacing = new DicomTagDSs(0x0018, 0x1164);

        ///<summary>(0018,1166) VR=CS VM=1-n Grid</summary>
        public readonly static DicomTagCSs Grid = new DicomTagCSs(0x0018, 0x1166);

        ///<summary>(0018,1170) VR=IS VM=1 Generator Power</summary>
        public readonly static DicomTagIS GeneratorPower = new DicomTagIS(0x0018, 0x1170);

        ///<summary>(0018,1180) VR=SH VM=1 Collimator/grid Name</summary>
        public readonly static DicomTagSH CollimatorGridName = new DicomTagSH(0x0018, 0x1180);

        ///<summary>(0018,1181) VR=CS VM=1 Collimator Type</summary>
        public readonly static DicomTagCS CollimatorType = new DicomTagCS(0x0018, 0x1181);

        ///<summary>(0018,1182) VR=IS VM=1-2 Focal Distance</summary>
        public readonly static DicomTagISs FocalDistance = new DicomTagISs(0x0018, 0x1182);

        ///<summary>(0018,1183) VR=DS VM=1-2 X Focus Center</summary>
        public readonly static DicomTagDSs XFocusCenter = new DicomTagDSs(0x0018, 0x1183);

        ///<summary>(0018,1184) VR=DS VM=1-2 Y Focus Center</summary>
        public readonly static DicomTagDSs YFocusCenter = new DicomTagDSs(0x0018, 0x1184);

        ///<summary>(0018,1190) VR=DS VM=1-n Focal Spot(s)</summary>
        public readonly static DicomTagDSs FocalSpots = new DicomTagDSs(0x0018, 0x1190);

        ///<summary>(0018,1191) VR=CS VM=1 Anode Target Material</summary>
        public readonly static DicomTagCS AnodeTargetMaterial = new DicomTagCS(0x0018, 0x1191);

        ///<summary>(0018,11A0) VR=DS VM=1 Body Part Thickness</summary>
        public readonly static DicomTagDS BodyPartThickness = new DicomTagDS(0x0018, 0x11A0);

        ///<summary>(0018,11A2) VR=DS VM=1 Compression Force</summary>
        public readonly static DicomTagDS CompressionForce = new DicomTagDS(0x0018, 0x11A2);

        ///<summary>(0018,11A3) VR=DS VM=1 Compression Pressure</summary>
        public readonly static DicomTagDS CompressionPressure = new DicomTagDS(0x0018, 0x11A3);

        ///<summary>(0018,11A4) VR=LO VM=1 Paddle Description</summary>
        public readonly static DicomTagLO PaddleDescription = new DicomTagLO(0x0018, 0x11A4);

        ///<summary>(0018,11A5) VR=DS VM=1 Compression Contact Area</summary>
        public readonly static DicomTagDS CompressionContactArea = new DicomTagDS(0x0018, 0x11A5);

        ///<summary>(0018,11B0) VR=LO VM=1 Acquisition Mode</summary>
        public readonly static DicomTagLO AcquisitionMode = new DicomTagLO(0x0018, 0x11B0);

        ///<summary>(0018,11B1) VR=LO VM=1 Dose Mode Name</summary>
        public readonly static DicomTagLO DoseModeName = new DicomTagLO(0x0018, 0x11B1);

        ///<summary>(0018,11B2) VR=CS VM=1 Acquired Subtraction Mask Flag</summary>
        public readonly static DicomTagCS AcquiredSubtractionMaskFlag = new DicomTagCS(0x0018, 0x11B2);

        ///<summary>(0018,11B3) VR=CS VM=1 Fluoroscopy Persistence Flag</summary>
        public readonly static DicomTagCS FluoroscopyPersistenceFlag = new DicomTagCS(0x0018, 0x11B3);

        ///<summary>(0018,11B4) VR=CS VM=1 Fluoroscopy Last Image Hold Persistence Flag</summary>
        public readonly static DicomTagCS FluoroscopyLastImageHoldPersistenceFlag = new DicomTagCS(0x0018, 0x11B4);

        ///<summary>(0018,11B5) VR=IS VM=1 Upper Limit Number Of Persistent Fluoroscopy Frames</summary>
        public readonly static DicomTagIS UpperLimitNumberOfPersistentFluoroscopyFrames = new DicomTagIS(0x0018, 0x11B5);

        ///<summary>(0018,11B6) VR=CS VM=1 Contrast/Bolus Auto Injection Trigger Flag</summary>
        public readonly static DicomTagCS ContrastBolusAutoInjectionTriggerFlag = new DicomTagCS(0x0018, 0x11B6);

        ///<summary>(0018,11B7) VR=FD VM=1 Contrast/Bolus Injection Delay</summary>
        public readonly static DicomTagFD ContrastBolusInjectionDelay = new DicomTagFD(0x0018, 0x11B7);

        ///<summary>(0018,11B8) VR=SQ VM=1 XA Acquisition Phase Details Sequence</summary>
        public readonly static DicomTagSQ XAAcquisitionPhaseDetailsSequence = new DicomTagSQ(0x0018, 0x11B8);

        ///<summary>(0018,11B9) VR=FD VM=1 XA Acquisition Frame Rate</summary>
        public readonly static DicomTagFD XAAcquisitionFrameRate = new DicomTagFD(0x0018, 0x11B9);

        ///<summary>(0018,11BA) VR=SQ VM=1 XA Plane Details Sequence</summary>
        public readonly static DicomTagSQ XAPlaneDetailsSequence = new DicomTagSQ(0x0018, 0x11BA);

        ///<summary>(0018,11BB) VR=LO VM=1 Acquisition Field of View Label</summary>
        public readonly static DicomTagLO AcquisitionFieldOfViewLabel = new DicomTagLO(0x0018, 0x11BB);

        ///<summary>(0018,11BC) VR=SQ VM=1 X-Ray Filter Details Sequence</summary>
        public readonly static DicomTagSQ XRayFilterDetailsSequence = new DicomTagSQ(0x0018, 0x11BC);

        ///<summary>(0018,11BD) VR=FD VM=1 XA Acquisition Duration</summary>
        public readonly static DicomTagFD XAAcquisitionDuration = new DicomTagFD(0x0018, 0x11BD);

        ///<summary>(0018,11BE) VR=CS VM=1 Reconstruction Pipeline Type</summary>
        public readonly static DicomTagCS ReconstructionPipelineType = new DicomTagCS(0x0018, 0x11BE);

        ///<summary>(0018,11BF) VR=SQ VM=1 Image Filter Details Sequence</summary>
        public readonly static DicomTagSQ ImageFilterDetailsSequence = new DicomTagSQ(0x0018, 0x11BF);

        ///<summary>(0018,11C0) VR=CS VM=1 Applied Mask Subtraction Flag</summary>
        public readonly static DicomTagCS AppliedMaskSubtractionFlag = new DicomTagCS(0x0018, 0x11C0);

        ///<summary>(0018,11C1) VR=SQ VM=1 Requested Series Description Code Sequence</summary>
        public readonly static DicomTagSQ RequestedSeriesDescriptionCodeSequence = new DicomTagSQ(0x0018, 0x11C1);

        ///<summary>(0018,1200) VR=DA VM=1-n Date of Last Calibration</summary>
        public readonly static DicomTagDAs DateOfLastCalibration = new DicomTagDAs(0x0018, 0x1200);

        ///<summary>(0018,1201) VR=TM VM=1-n Time of Last Calibration</summary>
        public readonly static DicomTagTMs TimeOfLastCalibration = new DicomTagTMs(0x0018, 0x1201);

        ///<summary>(0018,1202) VR=DT VM=1 DateTime of Last Calibration</summary>
        public readonly static DicomTagDT DateTimeOfLastCalibration = new DicomTagDT(0x0018, 0x1202);

        ///<summary>(0018,1203) VR=DT VM=1 Calibration DateTime</summary>
        public readonly static DicomTagDT CalibrationDateTime = new DicomTagDT(0x0018, 0x1203);

        ///<summary>(0018,1204) VR=DA VM=1 Date of Manufacture</summary>
        public readonly static DicomTagDA DateOfManufacture = new DicomTagDA(0x0018, 0x1204);

        ///<summary>(0018,1205) VR=DA VM=1 Date of Installation</summary>
        public readonly static DicomTagDA DateOfInstallation = new DicomTagDA(0x0018, 0x1205);

        ///<summary>(0018,1210) VR=SH VM=1-n Convolution Kernel</summary>
        public readonly static DicomTagSHs ConvolutionKernel = new DicomTagSHs(0x0018, 0x1210);

        ///<summary>(0018,1240) VR=IS VM=1-n Upper/Lower Pixel Values (RETIRED)</summary>
        public readonly static DicomTagISs UpperLowerPixelValuesRETIRED = new DicomTagISs(0x0018, 0x1240);

        ///<summary>(0018,1242) VR=IS VM=1 Actual Frame Duration</summary>
        public readonly static DicomTagIS ActualFrameDuration = new DicomTagIS(0x0018, 0x1242);

        ///<summary>(0018,1243) VR=IS VM=1 Count Rate</summary>
        public readonly static DicomTagIS CountRate = new DicomTagIS(0x0018, 0x1243);

        ///<summary>(0018,1244) VR=US VM=1 Preferred Playback Sequencing</summary>
        public readonly static DicomTagUS PreferredPlaybackSequencing = new DicomTagUS(0x0018, 0x1244);

        ///<summary>(0018,1250) VR=SH VM=1 Receive Coil Name</summary>
        public readonly static DicomTagSH ReceiveCoilName = new DicomTagSH(0x0018, 0x1250);

        ///<summary>(0018,1251) VR=SH VM=1 Transmit Coil Name</summary>
        public readonly static DicomTagSH TransmitCoilName = new DicomTagSH(0x0018, 0x1251);

        ///<summary>(0018,1260) VR=SH VM=1 Plate Type</summary>
        public readonly static DicomTagSH PlateType = new DicomTagSH(0x0018, 0x1260);

        ///<summary>(0018,1261) VR=LO VM=1 Phosphor Type</summary>
        public readonly static DicomTagLO PhosphorType = new DicomTagLO(0x0018, 0x1261);

        ///<summary>(0018,1271) VR=FD VM=1 Water Equivalent Diameter</summary>
        public readonly static DicomTagFD WaterEquivalentDiameter = new DicomTagFD(0x0018, 0x1271);

        ///<summary>(0018,1272) VR=SQ VM=1 Water Equivalent Diameter Calculation Method Code Sequence</summary>
        public readonly static DicomTagSQ WaterEquivalentDiameterCalculationMethodCodeSequence = new DicomTagSQ(0x0018, 0x1272);

        ///<summary>(0018,1300) VR=DS VM=1 Scan Velocity</summary>
        public readonly static DicomTagDS ScanVelocity = new DicomTagDS(0x0018, 0x1300);

        ///<summary>(0018,1301) VR=CS VM=1-n Whole Body Technique</summary>
        public readonly static DicomTagCSs WholeBodyTechnique = new DicomTagCSs(0x0018, 0x1301);

        ///<summary>(0018,1302) VR=IS VM=1 Scan Length</summary>
        public readonly static DicomTagIS ScanLength = new DicomTagIS(0x0018, 0x1302);

        ///<summary>(0018,1310) VR=US VM=4 Acquisition Matrix</summary>
        public readonly static DicomTagUSs AcquisitionMatrix = new DicomTagUSs(0x0018, 0x1310);

        ///<summary>(0018,1312) VR=CS VM=1 In-plane Phase Encoding Direction</summary>
        public readonly static DicomTagCS InPlanePhaseEncodingDirection = new DicomTagCS(0x0018, 0x1312);

        ///<summary>(0018,1314) VR=DS VM=1 Flip Angle</summary>
        public readonly static DicomTagDS FlipAngle = new DicomTagDS(0x0018, 0x1314);

        ///<summary>(0018,1315) VR=CS VM=1 Variable Flip Angle Flag</summary>
        public readonly static DicomTagCS VariableFlipAngleFlag = new DicomTagCS(0x0018, 0x1315);

        ///<summary>(0018,1316) VR=DS VM=1 SAR</summary>
        public readonly static DicomTagDS SAR = new DicomTagDS(0x0018, 0x1316);

        ///<summary>(0018,1318) VR=DS VM=1 dB/dt</summary>
        public readonly static DicomTagDS dBdt = new DicomTagDS(0x0018, 0x1318);

        ///<summary>(0018,1320) VR=FL VM=1 B1rms</summary>
        public readonly static DicomTagFL B1rms = new DicomTagFL(0x0018, 0x1320);

        ///<summary>(0018,1400) VR=LO VM=1 Acquisition Device Processing Description</summary>
        public readonly static DicomTagLO AcquisitionDeviceProcessingDescription = new DicomTagLO(0x0018, 0x1400);

        ///<summary>(0018,1401) VR=LO VM=1 Acquisition Device Processing Code</summary>
        public readonly static DicomTagLO AcquisitionDeviceProcessingCode = new DicomTagLO(0x0018, 0x1401);

        ///<summary>(0018,1402) VR=CS VM=1 Cassette Orientation</summary>
        public readonly static DicomTagCS CassetteOrientation = new DicomTagCS(0x0018, 0x1402);

        ///<summary>(0018,1403) VR=CS VM=1 Cassette Size</summary>
        public readonly static DicomTagCS CassetteSize = new DicomTagCS(0x0018, 0x1403);

        ///<summary>(0018,1404) VR=US VM=1 Exposures on Plate</summary>
        public readonly static DicomTagUS ExposuresOnPlate = new DicomTagUS(0x0018, 0x1404);

        ///<summary>(0018,1405) VR=IS VM=1 Relative X-Ray Exposure</summary>
        public readonly static DicomTagIS RelativeXRayExposure = new DicomTagIS(0x0018, 0x1405);

        ///<summary>(0018,1411) VR=DS VM=1 Exposure Index</summary>
        public readonly static DicomTagDS ExposureIndex = new DicomTagDS(0x0018, 0x1411);

        ///<summary>(0018,1412) VR=DS VM=1 Target Exposure Index</summary>
        public readonly static DicomTagDS TargetExposureIndex = new DicomTagDS(0x0018, 0x1412);

        ///<summary>(0018,1413) VR=DS VM=1 Deviation Index</summary>
        public readonly static DicomTagDS DeviationIndex = new DicomTagDS(0x0018, 0x1413);

        ///<summary>(0018,1450) VR=DS VM=1 Column Angulation</summary>
        public readonly static DicomTagDS ColumnAngulation = new DicomTagDS(0x0018, 0x1450);

        ///<summary>(0018,1460) VR=DS VM=1 Tomo Layer Height</summary>
        public readonly static DicomTagDS TomoLayerHeight = new DicomTagDS(0x0018, 0x1460);

        ///<summary>(0018,1470) VR=DS VM=1 Tomo Angle</summary>
        public readonly static DicomTagDS TomoAngle = new DicomTagDS(0x0018, 0x1470);

        ///<summary>(0018,1480) VR=DS VM=1 Tomo Time</summary>
        public readonly static DicomTagDS TomoTime = new DicomTagDS(0x0018, 0x1480);

        ///<summary>(0018,1490) VR=CS VM=1 Tomo Type</summary>
        public readonly static DicomTagCS TomoType = new DicomTagCS(0x0018, 0x1490);

        ///<summary>(0018,1491) VR=CS VM=1 Tomo Class</summary>
        public readonly static DicomTagCS TomoClass = new DicomTagCS(0x0018, 0x1491);

        ///<summary>(0018,1495) VR=IS VM=1 Number of Tomosynthesis Source Images</summary>
        public readonly static DicomTagIS NumberOfTomosynthesisSourceImages = new DicomTagIS(0x0018, 0x1495);

        ///<summary>(0018,1500) VR=CS VM=1 Positioner Motion</summary>
        public readonly static DicomTagCS PositionerMotion = new DicomTagCS(0x0018, 0x1500);

        ///<summary>(0018,1508) VR=CS VM=1 Positioner Type</summary>
        public readonly static DicomTagCS PositionerType = new DicomTagCS(0x0018, 0x1508);

        ///<summary>(0018,1510) VR=DS VM=1 Positioner Primary Angle</summary>
        public readonly static DicomTagDS PositionerPrimaryAngle = new DicomTagDS(0x0018, 0x1510);

        ///<summary>(0018,1511) VR=DS VM=1 Positioner Secondary Angle</summary>
        public readonly static DicomTagDS PositionerSecondaryAngle = new DicomTagDS(0x0018, 0x1511);

        ///<summary>(0018,1520) VR=DS VM=1-n Positioner Primary Angle Increment</summary>
        public readonly static DicomTagDSs PositionerPrimaryAngleIncrement = new DicomTagDSs(0x0018, 0x1520);

        ///<summary>(0018,1521) VR=DS VM=1-n Positioner Secondary Angle Increment</summary>
        public readonly static DicomTagDSs PositionerSecondaryAngleIncrement = new DicomTagDSs(0x0018, 0x1521);

        ///<summary>(0018,1530) VR=DS VM=1 Detector Primary Angle</summary>
        public readonly static DicomTagDS DetectorPrimaryAngle = new DicomTagDS(0x0018, 0x1530);

        ///<summary>(0018,1531) VR=DS VM=1 Detector Secondary Angle</summary>
        public readonly static DicomTagDS DetectorSecondaryAngle = new DicomTagDS(0x0018, 0x1531);

        ///<summary>(0018,1600) VR=CS VM=1-3 Shutter Shape</summary>
        public readonly static DicomTagCSs ShutterShape = new DicomTagCSs(0x0018, 0x1600);

        ///<summary>(0018,1602) VR=IS VM=1 Shutter Left Vertical Edge</summary>
        public readonly static DicomTagIS ShutterLeftVerticalEdge = new DicomTagIS(0x0018, 0x1602);

        ///<summary>(0018,1604) VR=IS VM=1 Shutter Right Vertical Edge</summary>
        public readonly static DicomTagIS ShutterRightVerticalEdge = new DicomTagIS(0x0018, 0x1604);

        ///<summary>(0018,1606) VR=IS VM=1 Shutter Upper Horizontal Edge</summary>
        public readonly static DicomTagIS ShutterUpperHorizontalEdge = new DicomTagIS(0x0018, 0x1606);

        ///<summary>(0018,1608) VR=IS VM=1 Shutter Lower Horizontal Edge</summary>
        public readonly static DicomTagIS ShutterLowerHorizontalEdge = new DicomTagIS(0x0018, 0x1608);

        ///<summary>(0018,1610) VR=IS VM=2 Center of Circular Shutter</summary>
        public readonly static DicomTagISs CenterOfCircularShutter = new DicomTagISs(0x0018, 0x1610);

        ///<summary>(0018,1612) VR=IS VM=1 Radius of Circular Shutter</summary>
        public readonly static DicomTagIS RadiusOfCircularShutter = new DicomTagIS(0x0018, 0x1612);

        ///<summary>(0018,1620) VR=IS VM=2-2n Vertices of the Polygonal Shutter</summary>
        public readonly static DicomTagISs VerticesOfThePolygonalShutter = new DicomTagISs(0x0018, 0x1620);

        ///<summary>(0018,1622) VR=US VM=1 Shutter Presentation Value</summary>
        public readonly static DicomTagUS ShutterPresentationValue = new DicomTagUS(0x0018, 0x1622);

        ///<summary>(0018,1623) VR=US VM=1 Shutter Overlay Group</summary>
        public readonly static DicomTagUS ShutterOverlayGroup = new DicomTagUS(0x0018, 0x1623);

        ///<summary>(0018,1624) VR=US VM=3 Shutter Presentation Color CIELab Value</summary>
        public readonly static DicomTagUSs ShutterPresentationColorCIELabValue = new DicomTagUSs(0x0018, 0x1624);

        ///<summary>(0018,1630) VR=CS VM=1 Outline Shape Type</summary>
        public readonly static DicomTagCS OutlineShapeType = new DicomTagCS(0x0018, 0x1630);

        ///<summary>(0018,1631) VR=FD VM=1 Outline Left Vertical Edge</summary>
        public readonly static DicomTagFD OutlineLeftVerticalEdge = new DicomTagFD(0x0018, 0x1631);

        ///<summary>(0018,1632) VR=FD VM=1 Outline Right Vertical Edge</summary>
        public readonly static DicomTagFD OutlineRightVerticalEdge = new DicomTagFD(0x0018, 0x1632);

        ///<summary>(0018,1633) VR=FD VM=1 Outline Upper Horizontal Edge</summary>
        public readonly static DicomTagFD OutlineUpperHorizontalEdge = new DicomTagFD(0x0018, 0x1633);

        ///<summary>(0018,1634) VR=FD VM=1 Outline Lower Horizontal Edge</summary>
        public readonly static DicomTagFD OutlineLowerHorizontalEdge = new DicomTagFD(0x0018, 0x1634);

        ///<summary>(0018,1635) VR=FD VM=2 Center of Circular Outline</summary>
        public readonly static DicomTagFDs CenterOfCircularOutline = new DicomTagFDs(0x0018, 0x1635);

        ///<summary>(0018,1636) VR=FD VM=1 Diameter of Circular Outline</summary>
        public readonly static DicomTagFD DiameterOfCircularOutline = new DicomTagFD(0x0018, 0x1636);

        ///<summary>(0018,1637) VR=UL VM=1 Number of Polygonal Vertices</summary>
        public readonly static DicomTagUL NumberOfPolygonalVertices = new DicomTagUL(0x0018, 0x1637);

        ///<summary>(0018,1638) VR=OF VM=1 Vertices of the Polygonal Outline</summary>
        public readonly static DicomTagOF VerticesOfThePolygonalOutline = new DicomTagOF(0x0018, 0x1638);

        ///<summary>(0018,1700) VR=CS VM=1-3 Collimator Shape</summary>
        public readonly static DicomTagCSs CollimatorShape = new DicomTagCSs(0x0018, 0x1700);

        ///<summary>(0018,1702) VR=IS VM=1 Collimator Left Vertical Edge</summary>
        public readonly static DicomTagIS CollimatorLeftVerticalEdge = new DicomTagIS(0x0018, 0x1702);

        ///<summary>(0018,1704) VR=IS VM=1 Collimator Right Vertical Edge</summary>
        public readonly static DicomTagIS CollimatorRightVerticalEdge = new DicomTagIS(0x0018, 0x1704);

        ///<summary>(0018,1706) VR=IS VM=1 Collimator Upper Horizontal Edge</summary>
        public readonly static DicomTagIS CollimatorUpperHorizontalEdge = new DicomTagIS(0x0018, 0x1706);

        ///<summary>(0018,1708) VR=IS VM=1 Collimator Lower Horizontal Edge</summary>
        public readonly static DicomTagIS CollimatorLowerHorizontalEdge = new DicomTagIS(0x0018, 0x1708);

        ///<summary>(0018,1710) VR=IS VM=2 Center of Circular Collimator</summary>
        public readonly static DicomTagISs CenterOfCircularCollimator = new DicomTagISs(0x0018, 0x1710);

        ///<summary>(0018,1712) VR=IS VM=1 Radius of Circular Collimator</summary>
        public readonly static DicomTagIS RadiusOfCircularCollimator = new DicomTagIS(0x0018, 0x1712);

        ///<summary>(0018,1720) VR=IS VM=2-2n Vertices of the Polygonal Collimator</summary>
        public readonly static DicomTagISs VerticesOfThePolygonalCollimator = new DicomTagISs(0x0018, 0x1720);

        ///<summary>(0018,1800) VR=CS VM=1 Acquisition Time Synchronized</summary>
        public readonly static DicomTagCS AcquisitionTimeSynchronized = new DicomTagCS(0x0018, 0x1800);

        ///<summary>(0018,1801) VR=SH VM=1 Time Source</summary>
        public readonly static DicomTagSH TimeSource = new DicomTagSH(0x0018, 0x1801);

        ///<summary>(0018,1802) VR=CS VM=1 Time Distribution Protocol</summary>
        public readonly static DicomTagCS TimeDistributionProtocol = new DicomTagCS(0x0018, 0x1802);

        ///<summary>(0018,1803) VR=LO VM=1 NTP Source Address</summary>
        public readonly static DicomTagLO NTPSourceAddress = new DicomTagLO(0x0018, 0x1803);

        ///<summary>(0018,2001) VR=IS VM=1-n Page Number Vector</summary>
        public readonly static DicomTagISs PageNumberVector = new DicomTagISs(0x0018, 0x2001);

        ///<summary>(0018,2002) VR=SH VM=1-n Frame Label Vector</summary>
        public readonly static DicomTagSHs FrameLabelVector = new DicomTagSHs(0x0018, 0x2002);

        ///<summary>(0018,2003) VR=DS VM=1-n Frame Primary Angle Vector</summary>
        public readonly static DicomTagDSs FramePrimaryAngleVector = new DicomTagDSs(0x0018, 0x2003);

        ///<summary>(0018,2004) VR=DS VM=1-n Frame Secondary Angle Vector</summary>
        public readonly static DicomTagDSs FrameSecondaryAngleVector = new DicomTagDSs(0x0018, 0x2004);

        ///<summary>(0018,2005) VR=DS VM=1-n Slice Location Vector</summary>
        public readonly static DicomTagDSs SliceLocationVector = new DicomTagDSs(0x0018, 0x2005);

        ///<summary>(0018,2006) VR=SH VM=1-n Display Window Label Vector</summary>
        public readonly static DicomTagSHs DisplayWindowLabelVector = new DicomTagSHs(0x0018, 0x2006);

        ///<summary>(0018,2010) VR=DS VM=2 Nominal Scanned Pixel Spacing</summary>
        public readonly static DicomTagDSs NominalScannedPixelSpacing = new DicomTagDSs(0x0018, 0x2010);

        ///<summary>(0018,2020) VR=CS VM=1 Digitizing Device Transport Direction</summary>
        public readonly static DicomTagCS DigitizingDeviceTransportDirection = new DicomTagCS(0x0018, 0x2020);

        ///<summary>(0018,2030) VR=DS VM=1 Rotation of Scanned Film</summary>
        public readonly static DicomTagDS RotationOfScannedFilm = new DicomTagDS(0x0018, 0x2030);

        ///<summary>(0018,2041) VR=SQ VM=1 Biopsy Target Sequence</summary>
        public readonly static DicomTagSQ BiopsyTargetSequence = new DicomTagSQ(0x0018, 0x2041);

        ///<summary>(0018,2042) VR=UI VM=1 Target UID</summary>
        public readonly static DicomTagUI TargetUID = new DicomTagUI(0x0018, 0x2042);

        ///<summary>(0018,2043) VR=FL VM=2 Localizing Cursor Position</summary>
        public readonly static DicomTagFLs LocalizingCursorPosition = new DicomTagFLs(0x0018, 0x2043);

        ///<summary>(0018,2044) VR=FL VM=3 Calculated Target Position</summary>
        public readonly static DicomTagFLs CalculatedTargetPosition = new DicomTagFLs(0x0018, 0x2044);

        ///<summary>(0018,2045) VR=SH VM=1 Target Label</summary>
        public readonly static DicomTagSH TargetLabel = new DicomTagSH(0x0018, 0x2045);

        ///<summary>(0018,2046) VR=FL VM=1 Displayed Z Value</summary>
        public readonly static DicomTagFL DisplayedZValue = new DicomTagFL(0x0018, 0x2046);

        ///<summary>(0018,3100) VR=CS VM=1 IVUS Acquisition</summary>
        public readonly static DicomTagCS IVUSAcquisition = new DicomTagCS(0x0018, 0x3100);

        ///<summary>(0018,3101) VR=DS VM=1 IVUS Pullback Rate</summary>
        public readonly static DicomTagDS IVUSPullbackRate = new DicomTagDS(0x0018, 0x3101);

        ///<summary>(0018,3102) VR=DS VM=1 IVUS Gated Rate</summary>
        public readonly static DicomTagDS IVUSGatedRate = new DicomTagDS(0x0018, 0x3102);

        ///<summary>(0018,3103) VR=IS VM=1 IVUS Pullback Start Frame Number</summary>
        public readonly static DicomTagIS IVUSPullbackStartFrameNumber = new DicomTagIS(0x0018, 0x3103);

        ///<summary>(0018,3104) VR=IS VM=1 IVUS Pullback Stop Frame Number</summary>
        public readonly static DicomTagIS IVUSPullbackStopFrameNumber = new DicomTagIS(0x0018, 0x3104);

        ///<summary>(0018,3105) VR=IS VM=1-n Lesion Number</summary>
        public readonly static DicomTagISs LesionNumber = new DicomTagISs(0x0018, 0x3105);

        ///<summary>(0018,4000) VR=LT VM=1 Acquisition Comments (RETIRED)</summary>
        public readonly static DicomTagLT AcquisitionCommentsRETIRED = new DicomTagLT(0x0018, 0x4000);

        ///<summary>(0018,5000) VR=SH VM=1-n Output Power</summary>
        public readonly static DicomTagSHs OutputPower = new DicomTagSHs(0x0018, 0x5000);

        ///<summary>(0018,5010) VR=LO VM=1-n Transducer Data</summary>
        public readonly static DicomTagLOs TransducerData = new DicomTagLOs(0x0018, 0x5010);

        ///<summary>(0018,5011) VR=SQ VM=1 Transducer Identification Sequence</summary>
        public readonly static DicomTagSQ TransducerIdentificationSequence = new DicomTagSQ(0x0018, 0x5011);

        ///<summary>(0018,5012) VR=DS VM=1 Focus Depth</summary>
        public readonly static DicomTagDS FocusDepth = new DicomTagDS(0x0018, 0x5012);

        ///<summary>(0018,5020) VR=LO VM=1 Processing Function</summary>
        public readonly static DicomTagLO ProcessingFunction = new DicomTagLO(0x0018, 0x5020);

        ///<summary>(0018,5021) VR=LO VM=1 Postprocessing Function (RETIRED)</summary>
        public readonly static DicomTagLO PostprocessingFunctionRETIRED = new DicomTagLO(0x0018, 0x5021);

        ///<summary>(0018,5022) VR=DS VM=1 Mechanical Index</summary>
        public readonly static DicomTagDS MechanicalIndex = new DicomTagDS(0x0018, 0x5022);

        ///<summary>(0018,5024) VR=DS VM=1 Bone Thermal Index</summary>
        public readonly static DicomTagDS BoneThermalIndex = new DicomTagDS(0x0018, 0x5024);

        ///<summary>(0018,5026) VR=DS VM=1 Cranial Thermal Index</summary>
        public readonly static DicomTagDS CranialThermalIndex = new DicomTagDS(0x0018, 0x5026);

        ///<summary>(0018,5027) VR=DS VM=1 Soft Tissue Thermal Index</summary>
        public readonly static DicomTagDS SoftTissueThermalIndex = new DicomTagDS(0x0018, 0x5027);

        ///<summary>(0018,5028) VR=DS VM=1 Soft Tissue-focus Thermal Index</summary>
        public readonly static DicomTagDS SoftTissueFocusThermalIndex = new DicomTagDS(0x0018, 0x5028);

        ///<summary>(0018,5029) VR=DS VM=1 Soft Tissue-surface Thermal Index</summary>
        public readonly static DicomTagDS SoftTissueSurfaceThermalIndex = new DicomTagDS(0x0018, 0x5029);

        ///<summary>(0018,5030) VR=DS VM=1 Dynamic Range (RETIRED)</summary>
        public readonly static DicomTagDS DynamicRangeRETIRED = new DicomTagDS(0x0018, 0x5030);

        ///<summary>(0018,5040) VR=DS VM=1 Total Gain (RETIRED)</summary>
        public readonly static DicomTagDS TotalGainRETIRED = new DicomTagDS(0x0018, 0x5040);

        ///<summary>(0018,5050) VR=IS VM=1 Depth of Scan Field</summary>
        public readonly static DicomTagIS DepthOfScanField = new DicomTagIS(0x0018, 0x5050);

        ///<summary>(0018,5100) VR=CS VM=1 Patient Position</summary>
        public readonly static DicomTagCS PatientPosition = new DicomTagCS(0x0018, 0x5100);

        ///<summary>(0018,5101) VR=CS VM=1 View Position</summary>
        public readonly static DicomTagCS ViewPosition = new DicomTagCS(0x0018, 0x5101);

        ///<summary>(0018,5104) VR=SQ VM=1 Projection Eponymous Name Code Sequence</summary>
        public readonly static DicomTagSQ ProjectionEponymousNameCodeSequence = new DicomTagSQ(0x0018, 0x5104);

        ///<summary>(0018,5210) VR=DS VM=6 Image Transformation Matrix (RETIRED)</summary>
        public readonly static DicomTagDSs ImageTransformationMatrixRETIRED = new DicomTagDSs(0x0018, 0x5210);

        ///<summary>(0018,5212) VR=DS VM=3 Image Translation Vector (RETIRED)</summary>
        public readonly static DicomTagDSs ImageTranslationVectorRETIRED = new DicomTagDSs(0x0018, 0x5212);

        ///<summary>(0018,6000) VR=DS VM=1 Sensitivity</summary>
        public readonly static DicomTagDS Sensitivity = new DicomTagDS(0x0018, 0x6000);

        ///<summary>(0018,6011) VR=SQ VM=1 Sequence of Ultrasound Regions</summary>
        public readonly static DicomTagSQ SequenceOfUltrasoundRegions = new DicomTagSQ(0x0018, 0x6011);

        ///<summary>(0018,6012) VR=US VM=1 Region Spatial Format</summary>
        public readonly static DicomTagUS RegionSpatialFormat = new DicomTagUS(0x0018, 0x6012);

        ///<summary>(0018,6014) VR=US VM=1 Region Data Type</summary>
        public readonly static DicomTagUS RegionDataType = new DicomTagUS(0x0018, 0x6014);

        ///<summary>(0018,6016) VR=UL VM=1 Region Flags</summary>
        public readonly static DicomTagUL RegionFlags = new DicomTagUL(0x0018, 0x6016);

        ///<summary>(0018,6018) VR=UL VM=1 Region Location Min X0</summary>
        public readonly static DicomTagUL RegionLocationMinX0 = new DicomTagUL(0x0018, 0x6018);

        ///<summary>(0018,601A) VR=UL VM=1 Region Location Min Y0</summary>
        public readonly static DicomTagUL RegionLocationMinY0 = new DicomTagUL(0x0018, 0x601A);

        ///<summary>(0018,601C) VR=UL VM=1 Region Location Max X1</summary>
        public readonly static DicomTagUL RegionLocationMaxX1 = new DicomTagUL(0x0018, 0x601C);

        ///<summary>(0018,601E) VR=UL VM=1 Region Location Max Y1</summary>
        public readonly static DicomTagUL RegionLocationMaxY1 = new DicomTagUL(0x0018, 0x601E);

        ///<summary>(0018,6020) VR=SL VM=1 Reference Pixel X0</summary>
        public readonly static DicomTagSL ReferencePixelX0 = new DicomTagSL(0x0018, 0x6020);

        ///<summary>(0018,6022) VR=SL VM=1 Reference Pixel Y0</summary>
        public readonly static DicomTagSL ReferencePixelY0 = new DicomTagSL(0x0018, 0x6022);

        ///<summary>(0018,6024) VR=US VM=1 Physical Units X Direction</summary>
        public readonly static DicomTagUS PhysicalUnitsXDirection = new DicomTagUS(0x0018, 0x6024);

        ///<summary>(0018,6026) VR=US VM=1 Physical Units Y Direction</summary>
        public readonly static DicomTagUS PhysicalUnitsYDirection = new DicomTagUS(0x0018, 0x6026);

        ///<summary>(0018,6028) VR=FD VM=1 Reference Pixel Physical Value X</summary>
        public readonly static DicomTagFD ReferencePixelPhysicalValueX = new DicomTagFD(0x0018, 0x6028);

        ///<summary>(0018,602A) VR=FD VM=1 Reference Pixel Physical Value Y</summary>
        public readonly static DicomTagFD ReferencePixelPhysicalValueY = new DicomTagFD(0x0018, 0x602A);

        ///<summary>(0018,602C) VR=FD VM=1 Physical Delta X</summary>
        public readonly static DicomTagFD PhysicalDeltaX = new DicomTagFD(0x0018, 0x602C);

        ///<summary>(0018,602E) VR=FD VM=1 Physical Delta Y</summary>
        public readonly static DicomTagFD PhysicalDeltaY = new DicomTagFD(0x0018, 0x602E);

        ///<summary>(0018,6030) VR=UL VM=1 Transducer Frequency</summary>
        public readonly static DicomTagUL TransducerFrequency = new DicomTagUL(0x0018, 0x6030);

        ///<summary>(0018,6031) VR=CS VM=1 Transducer Type</summary>
        public readonly static DicomTagCS TransducerType = new DicomTagCS(0x0018, 0x6031);

        ///<summary>(0018,6032) VR=UL VM=1 Pulse Repetition Frequency</summary>
        public readonly static DicomTagUL PulseRepetitionFrequency = new DicomTagUL(0x0018, 0x6032);

        ///<summary>(0018,6034) VR=FD VM=1 Doppler Correction Angle</summary>
        public readonly static DicomTagFD DopplerCorrectionAngle = new DicomTagFD(0x0018, 0x6034);

        ///<summary>(0018,6036) VR=FD VM=1 Steering Angle</summary>
        public readonly static DicomTagFD SteeringAngle = new DicomTagFD(0x0018, 0x6036);

        ///<summary>(0018,6038) VR=UL VM=1 Doppler Sample Volume X Position (Retired) (RETIRED)</summary>
        public readonly static DicomTagUL DopplerSampleVolumeXPositionRetiredRETIRED = new DicomTagUL(0x0018, 0x6038);

        ///<summary>(0018,6039) VR=SL VM=1 Doppler Sample Volume X Position</summary>
        public readonly static DicomTagSL DopplerSampleVolumeXPosition = new DicomTagSL(0x0018, 0x6039);

        ///<summary>(0018,603A) VR=UL VM=1 Doppler Sample Volume Y Position (Retired) (RETIRED)</summary>
        public readonly static DicomTagUL DopplerSampleVolumeYPositionRetiredRETIRED = new DicomTagUL(0x0018, 0x603A);

        ///<summary>(0018,603B) VR=SL VM=1 Doppler Sample Volume Y Position</summary>
        public readonly static DicomTagSL DopplerSampleVolumeYPosition = new DicomTagSL(0x0018, 0x603B);

        ///<summary>(0018,603C) VR=UL VM=1 TM-Line Position X0 (Retired) (RETIRED)</summary>
        public readonly static DicomTagUL TMLinePositionX0RetiredRETIRED = new DicomTagUL(0x0018, 0x603C);

        ///<summary>(0018,603D) VR=SL VM=1 TM-Line Position X0</summary>
        public readonly static DicomTagSL TMLinePositionX0 = new DicomTagSL(0x0018, 0x603D);

        ///<summary>(0018,603E) VR=UL VM=1 TM-Line Position Y0 (Retired) (RETIRED)</summary>
        public readonly static DicomTagUL TMLinePositionY0RetiredRETIRED = new DicomTagUL(0x0018, 0x603E);

        ///<summary>(0018,603F) VR=SL VM=1 TM-Line Position Y0</summary>
        public readonly static DicomTagSL TMLinePositionY0 = new DicomTagSL(0x0018, 0x603F);

        ///<summary>(0018,6040) VR=UL VM=1 TM-Line Position X1 (Retired) (RETIRED)</summary>
        public readonly static DicomTagUL TMLinePositionX1RetiredRETIRED = new DicomTagUL(0x0018, 0x6040);

        ///<summary>(0018,6041) VR=SL VM=1 TM-Line Position X1</summary>
        public readonly static DicomTagSL TMLinePositionX1 = new DicomTagSL(0x0018, 0x6041);

        ///<summary>(0018,6042) VR=UL VM=1 TM-Line Position Y1 (Retired) (RETIRED)</summary>
        public readonly static DicomTagUL TMLinePositionY1RetiredRETIRED = new DicomTagUL(0x0018, 0x6042);

        ///<summary>(0018,6043) VR=SL VM=1 TM-Line Position Y1</summary>
        public readonly static DicomTagSL TMLinePositionY1 = new DicomTagSL(0x0018, 0x6043);

        ///<summary>(0018,6044) VR=US VM=1 Pixel Component Organization</summary>
        public readonly static DicomTagUS PixelComponentOrganization = new DicomTagUS(0x0018, 0x6044);

        ///<summary>(0018,6046) VR=UL VM=1 Pixel Component Mask</summary>
        public readonly static DicomTagUL PixelComponentMask = new DicomTagUL(0x0018, 0x6046);

        ///<summary>(0018,6048) VR=UL VM=1 Pixel Component Range Start</summary>
        public readonly static DicomTagUL PixelComponentRangeStart = new DicomTagUL(0x0018, 0x6048);

        ///<summary>(0018,604A) VR=UL VM=1 Pixel Component Range Stop</summary>
        public readonly static DicomTagUL PixelComponentRangeStop = new DicomTagUL(0x0018, 0x604A);

        ///<summary>(0018,604C) VR=US VM=1 Pixel Component Physical Units</summary>
        public readonly static DicomTagUS PixelComponentPhysicalUnits = new DicomTagUS(0x0018, 0x604C);

        ///<summary>(0018,604E) VR=US VM=1 Pixel Component Data Type</summary>
        public readonly static DicomTagUS PixelComponentDataType = new DicomTagUS(0x0018, 0x604E);

        ///<summary>(0018,6050) VR=UL VM=1 Number of Table Break Points</summary>
        public readonly static DicomTagUL NumberOfTableBreakPoints = new DicomTagUL(0x0018, 0x6050);

        ///<summary>(0018,6052) VR=UL VM=1-n Table of X Break Points</summary>
        public readonly static DicomTagULs TableOfXBreakPoints = new DicomTagULs(0x0018, 0x6052);

        ///<summary>(0018,6054) VR=FD VM=1-n Table of Y Break Points</summary>
        public readonly static DicomTagFDs TableOfYBreakPoints = new DicomTagFDs(0x0018, 0x6054);

        ///<summary>(0018,6056) VR=UL VM=1 Number of Table Entries</summary>
        public readonly static DicomTagUL NumberOfTableEntries = new DicomTagUL(0x0018, 0x6056);

        ///<summary>(0018,6058) VR=UL VM=1-n Table of Pixel Values</summary>
        public readonly static DicomTagULs TableOfPixelValues = new DicomTagULs(0x0018, 0x6058);

        ///<summary>(0018,605A) VR=FL VM=1-n Table of Parameter Values</summary>
        public readonly static DicomTagFLs TableOfParameterValues = new DicomTagFLs(0x0018, 0x605A);

        ///<summary>(0018,6060) VR=FL VM=1-n R Wave Time Vector</summary>
        public readonly static DicomTagFLs RWaveTimeVector = new DicomTagFLs(0x0018, 0x6060);

        ///<summary>(0018,6070) VR=US VM=1 Active Image Area Overlay Group</summary>
        public readonly static DicomTagUS ActiveImageAreaOverlayGroup = new DicomTagUS(0x0018, 0x6070);

        ///<summary>(0018,7000) VR=CS VM=1 Detector Conditions Nominal Flag</summary>
        public readonly static DicomTagCS DetectorConditionsNominalFlag = new DicomTagCS(0x0018, 0x7000);

        ///<summary>(0018,7001) VR=DS VM=1 Detector Temperature</summary>
        public readonly static DicomTagDS DetectorTemperature = new DicomTagDS(0x0018, 0x7001);

        ///<summary>(0018,7004) VR=CS VM=1 Detector Type</summary>
        public readonly static DicomTagCS DetectorType = new DicomTagCS(0x0018, 0x7004);

        ///<summary>(0018,7005) VR=CS VM=1 Detector Configuration</summary>
        public readonly static DicomTagCS DetectorConfiguration = new DicomTagCS(0x0018, 0x7005);

        ///<summary>(0018,7006) VR=LT VM=1 Detector Description</summary>
        public readonly static DicomTagLT DetectorDescription = new DicomTagLT(0x0018, 0x7006);

        ///<summary>(0018,7008) VR=LT VM=1 Detector Mode</summary>
        public readonly static DicomTagLT DetectorMode = new DicomTagLT(0x0018, 0x7008);

        ///<summary>(0018,700A) VR=SH VM=1 Detector ID</summary>
        public readonly static DicomTagSH DetectorID = new DicomTagSH(0x0018, 0x700A);

        ///<summary>(0018,700C) VR=DA VM=1 Date of Last Detector Calibration</summary>
        public readonly static DicomTagDA DateOfLastDetectorCalibration = new DicomTagDA(0x0018, 0x700C);

        ///<summary>(0018,700E) VR=TM VM=1 Time of Last Detector Calibration</summary>
        public readonly static DicomTagTM TimeOfLastDetectorCalibration = new DicomTagTM(0x0018, 0x700E);

        ///<summary>(0018,7010) VR=IS VM=1 Exposures on Detector Since Last Calibration</summary>
        public readonly static DicomTagIS ExposuresOnDetectorSinceLastCalibration = new DicomTagIS(0x0018, 0x7010);

        ///<summary>(0018,7011) VR=IS VM=1 Exposures on Detector Since Manufactured</summary>
        public readonly static DicomTagIS ExposuresOnDetectorSinceManufactured = new DicomTagIS(0x0018, 0x7011);

        ///<summary>(0018,7012) VR=DS VM=1 Detector Time Since Last Exposure</summary>
        public readonly static DicomTagDS DetectorTimeSinceLastExposure = new DicomTagDS(0x0018, 0x7012);

        ///<summary>(0018,7014) VR=DS VM=1 Detector Active Time</summary>
        public readonly static DicomTagDS DetectorActiveTime = new DicomTagDS(0x0018, 0x7014);

        ///<summary>(0018,7016) VR=DS VM=1 Detector Activation Offset From Exposure</summary>
        public readonly static DicomTagDS DetectorActivationOffsetFromExposure = new DicomTagDS(0x0018, 0x7016);

        ///<summary>(0018,701A) VR=DS VM=2 Detector Binning</summary>
        public readonly static DicomTagDSs DetectorBinning = new DicomTagDSs(0x0018, 0x701A);

        ///<summary>(0018,7020) VR=DS VM=2 Detector Element Physical Size</summary>
        public readonly static DicomTagDSs DetectorElementPhysicalSize = new DicomTagDSs(0x0018, 0x7020);

        ///<summary>(0018,7022) VR=DS VM=2 Detector Element Spacing</summary>
        public readonly static DicomTagDSs DetectorElementSpacing = new DicomTagDSs(0x0018, 0x7022);

        ///<summary>(0018,7024) VR=CS VM=1 Detector Active Shape</summary>
        public readonly static DicomTagCS DetectorActiveShape = new DicomTagCS(0x0018, 0x7024);

        ///<summary>(0018,7026) VR=DS VM=1-2 Detector Active Dimension(s)</summary>
        public readonly static DicomTagDSs DetectorActiveDimensions = new DicomTagDSs(0x0018, 0x7026);

        ///<summary>(0018,7028) VR=DS VM=2 Detector Active Origin</summary>
        public readonly static DicomTagDSs DetectorActiveOrigin = new DicomTagDSs(0x0018, 0x7028);

        ///<summary>(0018,702A) VR=LO VM=1 Detector Manufacturer Name</summary>
        public readonly static DicomTagLO DetectorManufacturerName = new DicomTagLO(0x0018, 0x702A);

        ///<summary>(0018,702B) VR=LO VM=1 Detector Manufacturer's Model Name</summary>
        public readonly static DicomTagLO DetectorManufacturerModelName = new DicomTagLO(0x0018, 0x702B);

        ///<summary>(0018,7030) VR=DS VM=2 Field of View Origin</summary>
        public readonly static DicomTagDSs FieldOfViewOrigin = new DicomTagDSs(0x0018, 0x7030);

        ///<summary>(0018,7032) VR=DS VM=1 Field of View Rotation</summary>
        public readonly static DicomTagDS FieldOfViewRotation = new DicomTagDS(0x0018, 0x7032);

        ///<summary>(0018,7034) VR=CS VM=1 Field of View Horizontal Flip</summary>
        public readonly static DicomTagCS FieldOfViewHorizontalFlip = new DicomTagCS(0x0018, 0x7034);

        ///<summary>(0018,7036) VR=FL VM=2 Pixel Data Area Origin Relative To FOV</summary>
        public readonly static DicomTagFLs PixelDataAreaOriginRelativeToFOV = new DicomTagFLs(0x0018, 0x7036);

        ///<summary>(0018,7038) VR=FL VM=1 Pixel Data Area Rotation Angle Relative To FOV</summary>
        public readonly static DicomTagFL PixelDataAreaRotationAngleRelativeToFOV = new DicomTagFL(0x0018, 0x7038);

        ///<summary>(0018,7040) VR=LT VM=1 Grid Absorbing Material</summary>
        public readonly static DicomTagLT GridAbsorbingMaterial = new DicomTagLT(0x0018, 0x7040);

        ///<summary>(0018,7041) VR=LT VM=1 Grid Spacing Material</summary>
        public readonly static DicomTagLT GridSpacingMaterial = new DicomTagLT(0x0018, 0x7041);

        ///<summary>(0018,7042) VR=DS VM=1 Grid Thickness</summary>
        public readonly static DicomTagDS GridThickness = new DicomTagDS(0x0018, 0x7042);

        ///<summary>(0018,7044) VR=DS VM=1 Grid Pitch</summary>
        public readonly static DicomTagDS GridPitch = new DicomTagDS(0x0018, 0x7044);

        ///<summary>(0018,7046) VR=IS VM=2 Grid Aspect Ratio</summary>
        public readonly static DicomTagISs GridAspectRatio = new DicomTagISs(0x0018, 0x7046);

        ///<summary>(0018,7048) VR=DS VM=1 Grid Period</summary>
        public readonly static DicomTagDS GridPeriod = new DicomTagDS(0x0018, 0x7048);

        ///<summary>(0018,704C) VR=DS VM=1 Grid Focal Distance</summary>
        public readonly static DicomTagDS GridFocalDistance = new DicomTagDS(0x0018, 0x704C);

        ///<summary>(0018,7050) VR=CS VM=1-n Filter Material</summary>
        public readonly static DicomTagCSs FilterMaterial = new DicomTagCSs(0x0018, 0x7050);

        ///<summary>(0018,7052) VR=DS VM=1-n Filter Thickness Minimum</summary>
        public readonly static DicomTagDSs FilterThicknessMinimum = new DicomTagDSs(0x0018, 0x7052);

        ///<summary>(0018,7054) VR=DS VM=1-n Filter Thickness Maximum</summary>
        public readonly static DicomTagDSs FilterThicknessMaximum = new DicomTagDSs(0x0018, 0x7054);

        ///<summary>(0018,7056) VR=FL VM=1-n Filter Beam Path Length Minimum</summary>
        public readonly static DicomTagFLs FilterBeamPathLengthMinimum = new DicomTagFLs(0x0018, 0x7056);

        ///<summary>(0018,7058) VR=FL VM=1-n Filter Beam Path Length Maximum</summary>
        public readonly static DicomTagFLs FilterBeamPathLengthMaximum = new DicomTagFLs(0x0018, 0x7058);

        ///<summary>(0018,7060) VR=CS VM=1 Exposure Control Mode</summary>
        public readonly static DicomTagCS ExposureControlMode = new DicomTagCS(0x0018, 0x7060);

        ///<summary>(0018,7062) VR=LT VM=1 Exposure Control Mode Description</summary>
        public readonly static DicomTagLT ExposureControlModeDescription = new DicomTagLT(0x0018, 0x7062);

        ///<summary>(0018,7064) VR=CS VM=1 Exposure Status</summary>
        public readonly static DicomTagCS ExposureStatus = new DicomTagCS(0x0018, 0x7064);

        ///<summary>(0018,7065) VR=DS VM=1 Phototimer Setting</summary>
        public readonly static DicomTagDS PhototimerSetting = new DicomTagDS(0x0018, 0x7065);

        ///<summary>(0018,8150) VR=DS VM=1 Exposure Time in µS</summary>
        public readonly static DicomTagDS ExposureTimeInuS = new DicomTagDS(0x0018, 0x8150);

        ///<summary>(0018,8151) VR=DS VM=1 X-Ray Tube Current in µA</summary>
        public readonly static DicomTagDS XRayTubeCurrentInuA = new DicomTagDS(0x0018, 0x8151);

        ///<summary>(0018,9004) VR=CS VM=1 Content Qualification</summary>
        public readonly static DicomTagCS ContentQualification = new DicomTagCS(0x0018, 0x9004);

        ///<summary>(0018,9005) VR=SH VM=1 Pulse Sequence Name</summary>
        public readonly static DicomTagSH PulseSequenceName = new DicomTagSH(0x0018, 0x9005);

        ///<summary>(0018,9006) VR=SQ VM=1 MR Imaging Modifier Sequence</summary>
        public readonly static DicomTagSQ MRImagingModifierSequence = new DicomTagSQ(0x0018, 0x9006);

        ///<summary>(0018,9008) VR=CS VM=1 Echo Pulse Sequence</summary>
        public readonly static DicomTagCS EchoPulseSequence = new DicomTagCS(0x0018, 0x9008);

        ///<summary>(0018,9009) VR=CS VM=1 Inversion Recovery</summary>
        public readonly static DicomTagCS InversionRecovery = new DicomTagCS(0x0018, 0x9009);

        ///<summary>(0018,9010) VR=CS VM=1 Flow Compensation</summary>
        public readonly static DicomTagCS FlowCompensation = new DicomTagCS(0x0018, 0x9010);

        ///<summary>(0018,9011) VR=CS VM=1 Multiple Spin Echo</summary>
        public readonly static DicomTagCS MultipleSpinEcho = new DicomTagCS(0x0018, 0x9011);

        ///<summary>(0018,9012) VR=CS VM=1 Multi-planar Excitation</summary>
        public readonly static DicomTagCS MultiPlanarExcitation = new DicomTagCS(0x0018, 0x9012);

        ///<summary>(0018,9014) VR=CS VM=1 Phase Contrast</summary>
        public readonly static DicomTagCS PhaseContrast = new DicomTagCS(0x0018, 0x9014);

        ///<summary>(0018,9015) VR=CS VM=1 Time of Flight Contrast</summary>
        public readonly static DicomTagCS TimeOfFlightContrast = new DicomTagCS(0x0018, 0x9015);

        ///<summary>(0018,9016) VR=CS VM=1 Spoiling</summary>
        public readonly static DicomTagCS Spoiling = new DicomTagCS(0x0018, 0x9016);

        ///<summary>(0018,9017) VR=CS VM=1 Steady State Pulse Sequence</summary>
        public readonly static DicomTagCS SteadyStatePulseSequence = new DicomTagCS(0x0018, 0x9017);

        ///<summary>(0018,9018) VR=CS VM=1 Echo Planar Pulse Sequence</summary>
        public readonly static DicomTagCS EchoPlanarPulseSequence = new DicomTagCS(0x0018, 0x9018);

        ///<summary>(0018,9019) VR=FD VM=1 Tag Angle First Axis</summary>
        public readonly static DicomTagFD TagAngleFirstAxis = new DicomTagFD(0x0018, 0x9019);

        ///<summary>(0018,9020) VR=CS VM=1 Magnetization Transfer</summary>
        public readonly static DicomTagCS MagnetizationTransfer = new DicomTagCS(0x0018, 0x9020);

        ///<summary>(0018,9021) VR=CS VM=1 T2 Preparation</summary>
        public readonly static DicomTagCS T2Preparation = new DicomTagCS(0x0018, 0x9021);

        ///<summary>(0018,9022) VR=CS VM=1 Blood Signal Nulling</summary>
        public readonly static DicomTagCS BloodSignalNulling = new DicomTagCS(0x0018, 0x9022);

        ///<summary>(0018,9024) VR=CS VM=1 Saturation Recovery</summary>
        public readonly static DicomTagCS SaturationRecovery = new DicomTagCS(0x0018, 0x9024);

        ///<summary>(0018,9025) VR=CS VM=1 Spectrally Selected Suppression</summary>
        public readonly static DicomTagCS SpectrallySelectedSuppression = new DicomTagCS(0x0018, 0x9025);

        ///<summary>(0018,9026) VR=CS VM=1 Spectrally Selected Excitation</summary>
        public readonly static DicomTagCS SpectrallySelectedExcitation = new DicomTagCS(0x0018, 0x9026);

        ///<summary>(0018,9027) VR=CS VM=1 Spatial Pre-saturation</summary>
        public readonly static DicomTagCS SpatialPresaturation = new DicomTagCS(0x0018, 0x9027);

        ///<summary>(0018,9028) VR=CS VM=1 Tagging</summary>
        public readonly static DicomTagCS Tagging = new DicomTagCS(0x0018, 0x9028);

        ///<summary>(0018,9029) VR=CS VM=1 Oversampling Phase</summary>
        public readonly static DicomTagCS OversamplingPhase = new DicomTagCS(0x0018, 0x9029);

        ///<summary>(0018,9030) VR=FD VM=1 Tag Spacing First Dimension</summary>
        public readonly static DicomTagFD TagSpacingFirstDimension = new DicomTagFD(0x0018, 0x9030);

        ///<summary>(0018,9032) VR=CS VM=1 Geometry of k-Space Traversal</summary>
        public readonly static DicomTagCS GeometryOfKSpaceTraversal = new DicomTagCS(0x0018, 0x9032);

        ///<summary>(0018,9033) VR=CS VM=1 Segmented k-Space Traversal</summary>
        public readonly static DicomTagCS SegmentedKSpaceTraversal = new DicomTagCS(0x0018, 0x9033);

        ///<summary>(0018,9034) VR=CS VM=1 Rectilinear Phase Encode Reordering</summary>
        public readonly static DicomTagCS RectilinearPhaseEncodeReordering = new DicomTagCS(0x0018, 0x9034);

        ///<summary>(0018,9035) VR=FD VM=1 Tag Thickness</summary>
        public readonly static DicomTagFD TagThickness = new DicomTagFD(0x0018, 0x9035);

        ///<summary>(0018,9036) VR=CS VM=1 Partial Fourier Direction</summary>
        public readonly static DicomTagCS PartialFourierDirection = new DicomTagCS(0x0018, 0x9036);

        ///<summary>(0018,9037) VR=CS VM=1 Cardiac Synchronization Technique</summary>
        public readonly static DicomTagCS CardiacSynchronizationTechnique = new DicomTagCS(0x0018, 0x9037);

        ///<summary>(0018,9041) VR=LO VM=1 Receive Coil Manufacturer Name</summary>
        public readonly static DicomTagLO ReceiveCoilManufacturerName = new DicomTagLO(0x0018, 0x9041);

        ///<summary>(0018,9042) VR=SQ VM=1 MR Receive Coil Sequence</summary>
        public readonly static DicomTagSQ MRReceiveCoilSequence = new DicomTagSQ(0x0018, 0x9042);

        ///<summary>(0018,9043) VR=CS VM=1 Receive Coil Type</summary>
        public readonly static DicomTagCS ReceiveCoilType = new DicomTagCS(0x0018, 0x9043);

        ///<summary>(0018,9044) VR=CS VM=1 Quadrature Receive Coil</summary>
        public readonly static DicomTagCS QuadratureReceiveCoil = new DicomTagCS(0x0018, 0x9044);

        ///<summary>(0018,9045) VR=SQ VM=1 Multi-Coil Definition Sequence</summary>
        public readonly static DicomTagSQ MultiCoilDefinitionSequence = new DicomTagSQ(0x0018, 0x9045);

        ///<summary>(0018,9046) VR=LO VM=1 Multi-Coil Configuration</summary>
        public readonly static DicomTagLO MultiCoilConfiguration = new DicomTagLO(0x0018, 0x9046);

        ///<summary>(0018,9047) VR=SH VM=1 Multi-Coil Element Name</summary>
        public readonly static DicomTagSH MultiCoilElementName = new DicomTagSH(0x0018, 0x9047);

        ///<summary>(0018,9048) VR=CS VM=1 Multi-Coil Element Used</summary>
        public readonly static DicomTagCS MultiCoilElementUsed = new DicomTagCS(0x0018, 0x9048);

        ///<summary>(0018,9049) VR=SQ VM=1 MR Transmit Coil Sequence</summary>
        public readonly static DicomTagSQ MRTransmitCoilSequence = new DicomTagSQ(0x0018, 0x9049);

        ///<summary>(0018,9050) VR=LO VM=1 Transmit Coil Manufacturer Name</summary>
        public readonly static DicomTagLO TransmitCoilManufacturerName = new DicomTagLO(0x0018, 0x9050);

        ///<summary>(0018,9051) VR=CS VM=1 Transmit Coil Type</summary>
        public readonly static DicomTagCS TransmitCoilType = new DicomTagCS(0x0018, 0x9051);

        ///<summary>(0018,9052) VR=FD VM=1-2 Spectral Width</summary>
        public readonly static DicomTagFDs SpectralWidth = new DicomTagFDs(0x0018, 0x9052);

        ///<summary>(0018,9053) VR=FD VM=1-2 Chemical Shift Reference</summary>
        public readonly static DicomTagFDs ChemicalShiftReference = new DicomTagFDs(0x0018, 0x9053);

        ///<summary>(0018,9054) VR=CS VM=1 Volume Localization Technique</summary>
        public readonly static DicomTagCS VolumeLocalizationTechnique = new DicomTagCS(0x0018, 0x9054);

        ///<summary>(0018,9058) VR=US VM=1 MR Acquisition Frequency Encoding Steps</summary>
        public readonly static DicomTagUS MRAcquisitionFrequencyEncodingSteps = new DicomTagUS(0x0018, 0x9058);

        ///<summary>(0018,9059) VR=CS VM=1 De-coupling</summary>
        public readonly static DicomTagCS Decoupling = new DicomTagCS(0x0018, 0x9059);

        ///<summary>(0018,9060) VR=CS VM=1-2 De-coupled Nucleus</summary>
        public readonly static DicomTagCSs DecoupledNucleus = new DicomTagCSs(0x0018, 0x9060);

        ///<summary>(0018,9061) VR=FD VM=1-2 De-coupling Frequency</summary>
        public readonly static DicomTagFDs DecouplingFrequency = new DicomTagFDs(0x0018, 0x9061);

        ///<summary>(0018,9062) VR=CS VM=1 De-coupling Method</summary>
        public readonly static DicomTagCS DecouplingMethod = new DicomTagCS(0x0018, 0x9062);

        ///<summary>(0018,9063) VR=FD VM=1-2 De-coupling Chemical Shift Reference</summary>
        public readonly static DicomTagFDs DecouplingChemicalShiftReference = new DicomTagFDs(0x0018, 0x9063);

        ///<summary>(0018,9064) VR=CS VM=1 k-space Filtering</summary>
        public readonly static DicomTagCS KSpaceFiltering = new DicomTagCS(0x0018, 0x9064);

        ///<summary>(0018,9065) VR=CS VM=1-2 Time Domain Filtering</summary>
        public readonly static DicomTagCSs TimeDomainFiltering = new DicomTagCSs(0x0018, 0x9065);

        ///<summary>(0018,9066) VR=US VM=1-2 Number of Zero Fills</summary>
        public readonly static DicomTagUSs NumberOfZeroFills = new DicomTagUSs(0x0018, 0x9066);

        ///<summary>(0018,9067) VR=CS VM=1 Baseline Correction</summary>
        public readonly static DicomTagCS BaselineCorrection = new DicomTagCS(0x0018, 0x9067);

        ///<summary>(0018,9069) VR=FD VM=1 Parallel Reduction Factor In-plane</summary>
        public readonly static DicomTagFD ParallelReductionFactorInPlane = new DicomTagFD(0x0018, 0x9069);

        ///<summary>(0018,9070) VR=FD VM=1 Cardiac R-R Interval Specified</summary>
        public readonly static DicomTagFD CardiacRRIntervalSpecified = new DicomTagFD(0x0018, 0x9070);

        ///<summary>(0018,9073) VR=FD VM=1 Acquisition Duration</summary>
        public readonly static DicomTagFD AcquisitionDuration = new DicomTagFD(0x0018, 0x9073);

        ///<summary>(0018,9074) VR=DT VM=1 Frame Acquisition DateTime</summary>
        public readonly static DicomTagDT FrameAcquisitionDateTime = new DicomTagDT(0x0018, 0x9074);

        ///<summary>(0018,9075) VR=CS VM=1 Diffusion Directionality</summary>
        public readonly static DicomTagCS DiffusionDirectionality = new DicomTagCS(0x0018, 0x9075);

        ///<summary>(0018,9076) VR=SQ VM=1 Diffusion Gradient Direction Sequence</summary>
        public readonly static DicomTagSQ DiffusionGradientDirectionSequence = new DicomTagSQ(0x0018, 0x9076);

        ///<summary>(0018,9077) VR=CS VM=1 Parallel Acquisition</summary>
        public readonly static DicomTagCS ParallelAcquisition = new DicomTagCS(0x0018, 0x9077);

        ///<summary>(0018,9078) VR=CS VM=1 Parallel Acquisition Technique</summary>
        public readonly static DicomTagCS ParallelAcquisitionTechnique = new DicomTagCS(0x0018, 0x9078);

        ///<summary>(0018,9079) VR=FD VM=1-n Inversion Times</summary>
        public readonly static DicomTagFDs InversionTimes = new DicomTagFDs(0x0018, 0x9079);

        ///<summary>(0018,9080) VR=ST VM=1 Metabolite Map Description</summary>
        public readonly static DicomTagST MetaboliteMapDescription = new DicomTagST(0x0018, 0x9080);

        ///<summary>(0018,9081) VR=CS VM=1 Partial Fourier</summary>
        public readonly static DicomTagCS PartialFourier = new DicomTagCS(0x0018, 0x9081);

        ///<summary>(0018,9082) VR=FD VM=1 Effective Echo Time</summary>
        public readonly static DicomTagFD EffectiveEchoTime = new DicomTagFD(0x0018, 0x9082);

        ///<summary>(0018,9083) VR=SQ VM=1 Metabolite Map Code Sequence</summary>
        public readonly static DicomTagSQ MetaboliteMapCodeSequence = new DicomTagSQ(0x0018, 0x9083);

        ///<summary>(0018,9084) VR=SQ VM=1 Chemical Shift Sequence</summary>
        public readonly static DicomTagSQ ChemicalShiftSequence = new DicomTagSQ(0x0018, 0x9084);

        ///<summary>(0018,9085) VR=CS VM=1 Cardiac Signal Source</summary>
        public readonly static DicomTagCS CardiacSignalSource = new DicomTagCS(0x0018, 0x9085);

        ///<summary>(0018,9087) VR=FD VM=1 Diffusion b-value</summary>
        public readonly static DicomTagFD DiffusionBValue = new DicomTagFD(0x0018, 0x9087);

        ///<summary>(0018,9089) VR=FD VM=3 Diffusion Gradient Orientation</summary>
        public readonly static DicomTagFDs DiffusionGradientOrientation = new DicomTagFDs(0x0018, 0x9089);

        ///<summary>(0018,9090) VR=FD VM=3 Velocity Encoding Direction</summary>
        public readonly static DicomTagFDs VelocityEncodingDirection = new DicomTagFDs(0x0018, 0x9090);

        ///<summary>(0018,9091) VR=FD VM=1 Velocity Encoding Minimum Value</summary>
        public readonly static DicomTagFD VelocityEncodingMinimumValue = new DicomTagFD(0x0018, 0x9091);

        ///<summary>(0018,9092) VR=SQ VM=1 Velocity Encoding Acquisition Sequence</summary>
        public readonly static DicomTagSQ VelocityEncodingAcquisitionSequence = new DicomTagSQ(0x0018, 0x9092);

        ///<summary>(0018,9093) VR=US VM=1 Number of k-Space Trajectories</summary>
        public readonly static DicomTagUS NumberOfKSpaceTrajectories = new DicomTagUS(0x0018, 0x9093);

        ///<summary>(0018,9094) VR=CS VM=1 Coverage of k-Space</summary>
        public readonly static DicomTagCS CoverageOfKSpace = new DicomTagCS(0x0018, 0x9094);

        ///<summary>(0018,9095) VR=UL VM=1 Spectroscopy Acquisition Phase Rows</summary>
        public readonly static DicomTagUL SpectroscopyAcquisitionPhaseRows = new DicomTagUL(0x0018, 0x9095);

        ///<summary>(0018,9096) VR=FD VM=1 Parallel Reduction Factor In-plane (Retired) (RETIRED)</summary>
        public readonly static DicomTagFD ParallelReductionFactorInPlaneRetiredRETIRED = new DicomTagFD(0x0018, 0x9096);

        ///<summary>(0018,9098) VR=FD VM=1-2 Transmitter Frequency</summary>
        public readonly static DicomTagFDs TransmitterFrequency = new DicomTagFDs(0x0018, 0x9098);

        ///<summary>(0018,9100) VR=CS VM=1-2 Resonant Nucleus</summary>
        public readonly static DicomTagCSs ResonantNucleus = new DicomTagCSs(0x0018, 0x9100);

        ///<summary>(0018,9101) VR=CS VM=1 Frequency Correction</summary>
        public readonly static DicomTagCS FrequencyCorrection = new DicomTagCS(0x0018, 0x9101);

        ///<summary>(0018,9103) VR=SQ VM=1 MR Spectroscopy FOV/Geometry Sequence</summary>
        public readonly static DicomTagSQ MRSpectroscopyFOVGeometrySequence = new DicomTagSQ(0x0018, 0x9103);

        ///<summary>(0018,9104) VR=FD VM=1 Slab Thickness</summary>
        public readonly static DicomTagFD SlabThickness = new DicomTagFD(0x0018, 0x9104);

        ///<summary>(0018,9105) VR=FD VM=3 Slab Orientation</summary>
        public readonly static DicomTagFDs SlabOrientation = new DicomTagFDs(0x0018, 0x9105);

        ///<summary>(0018,9106) VR=FD VM=3 Mid Slab Position</summary>
        public readonly static DicomTagFDs MidSlabPosition = new DicomTagFDs(0x0018, 0x9106);

        ///<summary>(0018,9107) VR=SQ VM=1 MR Spatial Saturation Sequence</summary>
        public readonly static DicomTagSQ MRSpatialSaturationSequence = new DicomTagSQ(0x0018, 0x9107);

        ///<summary>(0018,9112) VR=SQ VM=1 MR Timing and Related Parameters Sequence</summary>
        public readonly static DicomTagSQ MRTimingAndRelatedParametersSequence = new DicomTagSQ(0x0018, 0x9112);

        ///<summary>(0018,9114) VR=SQ VM=1 MR Echo Sequence</summary>
        public readonly static DicomTagSQ MREchoSequence = new DicomTagSQ(0x0018, 0x9114);

        ///<summary>(0018,9115) VR=SQ VM=1 MR Modifier Sequence</summary>
        public readonly static DicomTagSQ MRModifierSequence = new DicomTagSQ(0x0018, 0x9115);

        ///<summary>(0018,9117) VR=SQ VM=1 MR Diffusion Sequence</summary>
        public readonly static DicomTagSQ MRDiffusionSequence = new DicomTagSQ(0x0018, 0x9117);

        ///<summary>(0018,9118) VR=SQ VM=1 Cardiac Synchronization Sequence</summary>
        public readonly static DicomTagSQ CardiacSynchronizationSequence = new DicomTagSQ(0x0018, 0x9118);

        ///<summary>(0018,9119) VR=SQ VM=1 MR Averages Sequence</summary>
        public readonly static DicomTagSQ MRAveragesSequence = new DicomTagSQ(0x0018, 0x9119);

        ///<summary>(0018,9125) VR=SQ VM=1 MR FOV/Geometry Sequence</summary>
        public readonly static DicomTagSQ MRFOVGeometrySequence = new DicomTagSQ(0x0018, 0x9125);

        ///<summary>(0018,9126) VR=SQ VM=1 Volume Localization Sequence</summary>
        public readonly static DicomTagSQ VolumeLocalizationSequence = new DicomTagSQ(0x0018, 0x9126);

        ///<summary>(0018,9127) VR=UL VM=1 Spectroscopy Acquisition Data Columns</summary>
        public readonly static DicomTagUL SpectroscopyAcquisitionDataColumns = new DicomTagUL(0x0018, 0x9127);

        ///<summary>(0018,9147) VR=CS VM=1 Diffusion Anisotropy Type</summary>
        public readonly static DicomTagCS DiffusionAnisotropyType = new DicomTagCS(0x0018, 0x9147);

        ///<summary>(0018,9151) VR=DT VM=1 Frame Reference DateTime</summary>
        public readonly static DicomTagDT FrameReferenceDateTime = new DicomTagDT(0x0018, 0x9151);

        ///<summary>(0018,9152) VR=SQ VM=1 MR Metabolite Map Sequence</summary>
        public readonly static DicomTagSQ MRMetaboliteMapSequence = new DicomTagSQ(0x0018, 0x9152);

        ///<summary>(0018,9155) VR=FD VM=1 Parallel Reduction Factor out-of-plane</summary>
        public readonly static DicomTagFD ParallelReductionFactorOutOfPlane = new DicomTagFD(0x0018, 0x9155);

        ///<summary>(0018,9159) VR=UL VM=1 Spectroscopy Acquisition Out-of-plane Phase Steps</summary>
        public readonly static DicomTagUL SpectroscopyAcquisitionOutOfPlanePhaseSteps = new DicomTagUL(0x0018, 0x9159);

        ///<summary>(0018,9166) VR=CS VM=1 Bulk Motion Status (RETIRED)</summary>
        public readonly static DicomTagCS BulkMotionStatusRETIRED = new DicomTagCS(0x0018, 0x9166);

        ///<summary>(0018,9168) VR=FD VM=1 Parallel Reduction Factor Second In-plane</summary>
        public readonly static DicomTagFD ParallelReductionFactorSecondInPlane = new DicomTagFD(0x0018, 0x9168);

        ///<summary>(0018,9169) VR=CS VM=1 Cardiac Beat Rejection Technique</summary>
        public readonly static DicomTagCS CardiacBeatRejectionTechnique = new DicomTagCS(0x0018, 0x9169);

        ///<summary>(0018,9170) VR=CS VM=1 Respiratory Motion Compensation Technique</summary>
        public readonly static DicomTagCS RespiratoryMotionCompensationTechnique = new DicomTagCS(0x0018, 0x9170);

        ///<summary>(0018,9171) VR=CS VM=1 Respiratory Signal Source</summary>
        public readonly static DicomTagCS RespiratorySignalSource = new DicomTagCS(0x0018, 0x9171);

        ///<summary>(0018,9172) VR=CS VM=1 Bulk Motion Compensation Technique</summary>
        public readonly static DicomTagCS BulkMotionCompensationTechnique = new DicomTagCS(0x0018, 0x9172);

        ///<summary>(0018,9173) VR=CS VM=1 Bulk Motion Signal Source</summary>
        public readonly static DicomTagCS BulkMotionSignalSource = new DicomTagCS(0x0018, 0x9173);

        ///<summary>(0018,9174) VR=CS VM=1 Applicable Safety Standard Agency</summary>
        public readonly static DicomTagCS ApplicableSafetyStandardAgency = new DicomTagCS(0x0018, 0x9174);

        ///<summary>(0018,9175) VR=LO VM=1 Applicable Safety Standard Description</summary>
        public readonly static DicomTagLO ApplicableSafetyStandardDescription = new DicomTagLO(0x0018, 0x9175);

        ///<summary>(0018,9176) VR=SQ VM=1 Operating Mode Sequence</summary>
        public readonly static DicomTagSQ OperatingModeSequence = new DicomTagSQ(0x0018, 0x9176);

        ///<summary>(0018,9177) VR=CS VM=1 Operating Mode Type</summary>
        public readonly static DicomTagCS OperatingModeType = new DicomTagCS(0x0018, 0x9177);

        ///<summary>(0018,9178) VR=CS VM=1 Operating Mode</summary>
        public readonly static DicomTagCS OperatingMode = new DicomTagCS(0x0018, 0x9178);

        ///<summary>(0018,9179) VR=CS VM=1 Specific Absorption Rate Definition</summary>
        public readonly static DicomTagCS SpecificAbsorptionRateDefinition = new DicomTagCS(0x0018, 0x9179);

        ///<summary>(0018,9180) VR=CS VM=1 Gradient Output Type</summary>
        public readonly static DicomTagCS GradientOutputType = new DicomTagCS(0x0018, 0x9180);

        ///<summary>(0018,9181) VR=FD VM=1 Specific Absorption Rate Value</summary>
        public readonly static DicomTagFD SpecificAbsorptionRateValue = new DicomTagFD(0x0018, 0x9181);

        ///<summary>(0018,9182) VR=FD VM=1 Gradient Output</summary>
        public readonly static DicomTagFD GradientOutput = new DicomTagFD(0x0018, 0x9182);

        ///<summary>(0018,9183) VR=CS VM=1 Flow Compensation Direction</summary>
        public readonly static DicomTagCS FlowCompensationDirection = new DicomTagCS(0x0018, 0x9183);

        ///<summary>(0018,9184) VR=FD VM=1 Tagging Delay</summary>
        public readonly static DicomTagFD TaggingDelay = new DicomTagFD(0x0018, 0x9184);

        ///<summary>(0018,9185) VR=ST VM=1 Respiratory Motion Compensation Technique Description</summary>
        public readonly static DicomTagST RespiratoryMotionCompensationTechniqueDescription = new DicomTagST(0x0018, 0x9185);

        ///<summary>(0018,9186) VR=SH VM=1 Respiratory Signal Source ID</summary>
        public readonly static DicomTagSH RespiratorySignalSourceID = new DicomTagSH(0x0018, 0x9186);

        ///<summary>(0018,9195) VR=FD VM=1 Chemical Shift Minimum Integration Limit in Hz (RETIRED)</summary>
        public readonly static DicomTagFD ChemicalShiftMinimumIntegrationLimitInHzRETIRED = new DicomTagFD(0x0018, 0x9195);

        ///<summary>(0018,9196) VR=FD VM=1 Chemical Shift Maximum Integration Limit in Hz (RETIRED)</summary>
        public readonly static DicomTagFD ChemicalShiftMaximumIntegrationLimitInHzRETIRED = new DicomTagFD(0x0018, 0x9196);

        ///<summary>(0018,9197) VR=SQ VM=1 MR Velocity Encoding Sequence</summary>
        public readonly static DicomTagSQ MRVelocityEncodingSequence = new DicomTagSQ(0x0018, 0x9197);

        ///<summary>(0018,9198) VR=CS VM=1 First Order Phase Correction</summary>
        public readonly static DicomTagCS FirstOrderPhaseCorrection = new DicomTagCS(0x0018, 0x9198);

        ///<summary>(0018,9199) VR=CS VM=1 Water Referenced Phase Correction</summary>
        public readonly static DicomTagCS WaterReferencedPhaseCorrection = new DicomTagCS(0x0018, 0x9199);

        ///<summary>(0018,9200) VR=CS VM=1 MR Spectroscopy Acquisition Type</summary>
        public readonly static DicomTagCS MRSpectroscopyAcquisitionType = new DicomTagCS(0x0018, 0x9200);

        ///<summary>(0018,9214) VR=CS VM=1 Respiratory Cycle Position</summary>
        public readonly static DicomTagCS RespiratoryCyclePosition = new DicomTagCS(0x0018, 0x9214);

        ///<summary>(0018,9217) VR=FD VM=1 Velocity Encoding Maximum Value</summary>
        public readonly static DicomTagFD VelocityEncodingMaximumValue = new DicomTagFD(0x0018, 0x9217);

        ///<summary>(0018,9218) VR=FD VM=1 Tag Spacing Second Dimension</summary>
        public readonly static DicomTagFD TagSpacingSecondDimension = new DicomTagFD(0x0018, 0x9218);

        ///<summary>(0018,9219) VR=SS VM=1 Tag Angle Second Axis</summary>
        public readonly static DicomTagSS TagAngleSecondAxis = new DicomTagSS(0x0018, 0x9219);

        ///<summary>(0018,9220) VR=FD VM=1 Frame Acquisition Duration</summary>
        public readonly static DicomTagFD FrameAcquisitionDuration = new DicomTagFD(0x0018, 0x9220);

        ///<summary>(0018,9226) VR=SQ VM=1 MR Image Frame Type Sequence</summary>
        public readonly static DicomTagSQ MRImageFrameTypeSequence = new DicomTagSQ(0x0018, 0x9226);

        ///<summary>(0018,9227) VR=SQ VM=1 MR Spectroscopy Frame Type Sequence</summary>
        public readonly static DicomTagSQ MRSpectroscopyFrameTypeSequence = new DicomTagSQ(0x0018, 0x9227);

        ///<summary>(0018,9231) VR=US VM=1 MR Acquisition Phase Encoding Steps in-plane</summary>
        public readonly static DicomTagUS MRAcquisitionPhaseEncodingStepsInPlane = new DicomTagUS(0x0018, 0x9231);

        ///<summary>(0018,9232) VR=US VM=1 MR Acquisition Phase Encoding Steps out-of-plane</summary>
        public readonly static DicomTagUS MRAcquisitionPhaseEncodingStepsOutOfPlane = new DicomTagUS(0x0018, 0x9232);

        ///<summary>(0018,9234) VR=UL VM=1 Spectroscopy Acquisition Phase Columns</summary>
        public readonly static DicomTagUL SpectroscopyAcquisitionPhaseColumns = new DicomTagUL(0x0018, 0x9234);

        ///<summary>(0018,9236) VR=CS VM=1 Cardiac Cycle Position</summary>
        public readonly static DicomTagCS CardiacCyclePosition = new DicomTagCS(0x0018, 0x9236);

        ///<summary>(0018,9239) VR=SQ VM=1 Specific Absorption Rate Sequence</summary>
        public readonly static DicomTagSQ SpecificAbsorptionRateSequence = new DicomTagSQ(0x0018, 0x9239);

        ///<summary>(0018,9240) VR=US VM=1 RF Echo Train Length</summary>
        public readonly static DicomTagUS RFEchoTrainLength = new DicomTagUS(0x0018, 0x9240);

        ///<summary>(0018,9241) VR=US VM=1 Gradient Echo Train Length</summary>
        public readonly static DicomTagUS GradientEchoTrainLength = new DicomTagUS(0x0018, 0x9241);

        ///<summary>(0018,9250) VR=CS VM=1 Arterial Spin Labeling Contrast</summary>
        public readonly static DicomTagCS ArterialSpinLabelingContrast = new DicomTagCS(0x0018, 0x9250);

        ///<summary>(0018,9251) VR=SQ VM=1 MR Arterial Spin Labeling Sequence</summary>
        public readonly static DicomTagSQ MRArterialSpinLabelingSequence = new DicomTagSQ(0x0018, 0x9251);

        ///<summary>(0018,9252) VR=LO VM=1 ASL Technique Description</summary>
        public readonly static DicomTagLO ASLTechniqueDescription = new DicomTagLO(0x0018, 0x9252);

        ///<summary>(0018,9253) VR=US VM=1 ASL Slab Number</summary>
        public readonly static DicomTagUS ASLSlabNumber = new DicomTagUS(0x0018, 0x9253);

        ///<summary>(0018,9254) VR=FD VM=1 ASL Slab Thickness</summary>
        public readonly static DicomTagFD ASLSlabThickness = new DicomTagFD(0x0018, 0x9254);

        ///<summary>(0018,9255) VR=FD VM=3 ASL Slab Orientation</summary>
        public readonly static DicomTagFDs ASLSlabOrientation = new DicomTagFDs(0x0018, 0x9255);

        ///<summary>(0018,9256) VR=FD VM=3 ASL Mid Slab Position</summary>
        public readonly static DicomTagFDs ASLMidSlabPosition = new DicomTagFDs(0x0018, 0x9256);

        ///<summary>(0018,9257) VR=CS VM=1 ASL Context</summary>
        public readonly static DicomTagCS ASLContext = new DicomTagCS(0x0018, 0x9257);

        ///<summary>(0018,9258) VR=UL VM=1 ASL Pulse Train Duration</summary>
        public readonly static DicomTagUL ASLPulseTrainDuration = new DicomTagUL(0x0018, 0x9258);

        ///<summary>(0018,9259) VR=CS VM=1 ASL Crusher Flag</summary>
        public readonly static DicomTagCS ASLCrusherFlag = new DicomTagCS(0x0018, 0x9259);

        ///<summary>(0018,925A) VR=FD VM=1 ASL Crusher Flow Limit</summary>
        public readonly static DicomTagFD ASLCrusherFlowLimit = new DicomTagFD(0x0018, 0x925A);

        ///<summary>(0018,925B) VR=LO VM=1 ASL Crusher Description</summary>
        public readonly static DicomTagLO ASLCrusherDescription = new DicomTagLO(0x0018, 0x925B);

        ///<summary>(0018,925C) VR=CS VM=1 ASL Bolus Cut-off Flag</summary>
        public readonly static DicomTagCS ASLBolusCutoffFlag = new DicomTagCS(0x0018, 0x925C);

        ///<summary>(0018,925D) VR=SQ VM=1 ASL Bolus Cut-off Timing Sequence</summary>
        public readonly static DicomTagSQ ASLBolusCutoffTimingSequence = new DicomTagSQ(0x0018, 0x925D);

        ///<summary>(0018,925E) VR=LO VM=1 ASL Bolus Cut-off Technique</summary>
        public readonly static DicomTagLO ASLBolusCutoffTechnique = new DicomTagLO(0x0018, 0x925E);

        ///<summary>(0018,925F) VR=UL VM=1 ASL Bolus Cut-off Delay Time</summary>
        public readonly static DicomTagUL ASLBolusCutoffDelayTime = new DicomTagUL(0x0018, 0x925F);

        ///<summary>(0018,9260) VR=SQ VM=1 ASL Slab Sequence</summary>
        public readonly static DicomTagSQ ASLSlabSequence = new DicomTagSQ(0x0018, 0x9260);

        ///<summary>(0018,9295) VR=FD VM=1 Chemical Shift Minimum Integration Limit in ppm</summary>
        public readonly static DicomTagFD ChemicalShiftMinimumIntegrationLimitInppm = new DicomTagFD(0x0018, 0x9295);

        ///<summary>(0018,9296) VR=FD VM=1 Chemical Shift Maximum Integration Limit in ppm</summary>
        public readonly static DicomTagFD ChemicalShiftMaximumIntegrationLimitInppm = new DicomTagFD(0x0018, 0x9296);

        ///<summary>(0018,9297) VR=CS VM=1 Water Reference Acquisition</summary>
        public readonly static DicomTagCS WaterReferenceAcquisition = new DicomTagCS(0x0018, 0x9297);

        ///<summary>(0018,9298) VR=IS VM=1 Echo Peak Position</summary>
        public readonly static DicomTagIS EchoPeakPosition = new DicomTagIS(0x0018, 0x9298);

        ///<summary>(0018,9301) VR=SQ VM=1 CT Acquisition Type Sequence</summary>
        public readonly static DicomTagSQ CTAcquisitionTypeSequence = new DicomTagSQ(0x0018, 0x9301);

        ///<summary>(0018,9302) VR=CS VM=1 Acquisition Type</summary>
        public readonly static DicomTagCS AcquisitionType = new DicomTagCS(0x0018, 0x9302);

        ///<summary>(0018,9303) VR=FD VM=1 Tube Angle</summary>
        public readonly static DicomTagFD TubeAngle = new DicomTagFD(0x0018, 0x9303);

        ///<summary>(0018,9304) VR=SQ VM=1 CT Acquisition Details Sequence</summary>
        public readonly static DicomTagSQ CTAcquisitionDetailsSequence = new DicomTagSQ(0x0018, 0x9304);

        ///<summary>(0018,9305) VR=FD VM=1 Revolution Time</summary>
        public readonly static DicomTagFD RevolutionTime = new DicomTagFD(0x0018, 0x9305);

        ///<summary>(0018,9306) VR=FD VM=1 Single Collimation Width</summary>
        public readonly static DicomTagFD SingleCollimationWidth = new DicomTagFD(0x0018, 0x9306);

        ///<summary>(0018,9307) VR=FD VM=1 Total Collimation Width</summary>
        public readonly static DicomTagFD TotalCollimationWidth = new DicomTagFD(0x0018, 0x9307);

        ///<summary>(0018,9308) VR=SQ VM=1 CT Table Dynamics Sequence</summary>
        public readonly static DicomTagSQ CTTableDynamicsSequence = new DicomTagSQ(0x0018, 0x9308);

        ///<summary>(0018,9309) VR=FD VM=1 Table Speed</summary>
        public readonly static DicomTagFD TableSpeed = new DicomTagFD(0x0018, 0x9309);

        ///<summary>(0018,9310) VR=FD VM=1 Table Feed per Rotation</summary>
        public readonly static DicomTagFD TableFeedPerRotation = new DicomTagFD(0x0018, 0x9310);

        ///<summary>(0018,9311) VR=FD VM=1 Spiral Pitch Factor</summary>
        public readonly static DicomTagFD SpiralPitchFactor = new DicomTagFD(0x0018, 0x9311);

        ///<summary>(0018,9312) VR=SQ VM=1 CT Geometry Sequence</summary>
        public readonly static DicomTagSQ CTGeometrySequence = new DicomTagSQ(0x0018, 0x9312);

        ///<summary>(0018,9313) VR=FD VM=3 Data Collection Center (Patient)</summary>
        public readonly static DicomTagFDs DataCollectionCenterPatient = new DicomTagFDs(0x0018, 0x9313);

        ///<summary>(0018,9314) VR=SQ VM=1 CT Reconstruction Sequence</summary>
        public readonly static DicomTagSQ CTReconstructionSequence = new DicomTagSQ(0x0018, 0x9314);

        ///<summary>(0018,9315) VR=CS VM=1 Reconstruction Algorithm</summary>
        public readonly static DicomTagCS ReconstructionAlgorithm = new DicomTagCS(0x0018, 0x9315);

        ///<summary>(0018,9316) VR=CS VM=1 Convolution Kernel Group</summary>
        public readonly static DicomTagCS ConvolutionKernelGroup = new DicomTagCS(0x0018, 0x9316);

        ///<summary>(0018,9317) VR=FD VM=2 Reconstruction Field of View</summary>
        public readonly static DicomTagFDs ReconstructionFieldOfView = new DicomTagFDs(0x0018, 0x9317);

        ///<summary>(0018,9318) VR=FD VM=3 Reconstruction Target Center (Patient)</summary>
        public readonly static DicomTagFDs ReconstructionTargetCenterPatient = new DicomTagFDs(0x0018, 0x9318);

        ///<summary>(0018,9319) VR=FD VM=1 Reconstruction Angle</summary>
        public readonly static DicomTagFD ReconstructionAngle = new DicomTagFD(0x0018, 0x9319);

        ///<summary>(0018,9320) VR=SH VM=1 Image Filter</summary>
        public readonly static DicomTagSH ImageFilter = new DicomTagSH(0x0018, 0x9320);

        ///<summary>(0018,9321) VR=SQ VM=1 CT Exposure Sequence</summary>
        public readonly static DicomTagSQ CTExposureSequence = new DicomTagSQ(0x0018, 0x9321);

        ///<summary>(0018,9322) VR=FD VM=2 Reconstruction Pixel Spacing</summary>
        public readonly static DicomTagFDs ReconstructionPixelSpacing = new DicomTagFDs(0x0018, 0x9322);

        ///<summary>(0018,9323) VR=CS VM=1-n Exposure Modulation Type</summary>
        public readonly static DicomTagCSs ExposureModulationType = new DicomTagCSs(0x0018, 0x9323);

        ///<summary>(0018,9324) VR=FD VM=1 Estimated Dose Saving (RETIRED)</summary>
        public readonly static DicomTagFD EstimatedDoseSavingRETIRED = new DicomTagFD(0x0018, 0x9324);

        ///<summary>(0018,9325) VR=SQ VM=1 CT X-Ray Details Sequence</summary>
        public readonly static DicomTagSQ CTXRayDetailsSequence = new DicomTagSQ(0x0018, 0x9325);

        ///<summary>(0018,9326) VR=SQ VM=1 CT Position Sequence</summary>
        public readonly static DicomTagSQ CTPositionSequence = new DicomTagSQ(0x0018, 0x9326);

        ///<summary>(0018,9327) VR=FD VM=1 Table Position</summary>
        public readonly static DicomTagFD TablePosition = new DicomTagFD(0x0018, 0x9327);

        ///<summary>(0018,9328) VR=FD VM=1 Exposure Time in ms</summary>
        public readonly static DicomTagFD ExposureTimeInms = new DicomTagFD(0x0018, 0x9328);

        ///<summary>(0018,9329) VR=SQ VM=1 CT Image Frame Type Sequence</summary>
        public readonly static DicomTagSQ CTImageFrameTypeSequence = new DicomTagSQ(0x0018, 0x9329);

        ///<summary>(0018,9330) VR=FD VM=1 X-Ray Tube Current in mA</summary>
        public readonly static DicomTagFD XRayTubeCurrentInmA = new DicomTagFD(0x0018, 0x9330);

        ///<summary>(0018,9332) VR=FD VM=1 Exposure in mAs</summary>
        public readonly static DicomTagFD ExposureInmAs = new DicomTagFD(0x0018, 0x9332);

        ///<summary>(0018,9333) VR=CS VM=1 Constant Volume Flag</summary>
        public readonly static DicomTagCS ConstantVolumeFlag = new DicomTagCS(0x0018, 0x9333);

        ///<summary>(0018,9334) VR=CS VM=1 Fluoroscopy Flag</summary>
        public readonly static DicomTagCS FluoroscopyFlag = new DicomTagCS(0x0018, 0x9334);

        ///<summary>(0018,9335) VR=FD VM=1 Distance Source to Data Collection Center</summary>
        public readonly static DicomTagFD DistanceSourceToDataCollectionCenter = new DicomTagFD(0x0018, 0x9335);

        ///<summary>(0018,9337) VR=US VM=1 Contrast/Bolus Agent Number</summary>
        public readonly static DicomTagUS ContrastBolusAgentNumber = new DicomTagUS(0x0018, 0x9337);

        ///<summary>(0018,9338) VR=SQ VM=1 Contrast/Bolus Ingredient Code Sequence</summary>
        public readonly static DicomTagSQ ContrastBolusIngredientCodeSequence = new DicomTagSQ(0x0018, 0x9338);

        ///<summary>(0018,9340) VR=SQ VM=1 Contrast Administration Profile Sequence</summary>
        public readonly static DicomTagSQ ContrastAdministrationProfileSequence = new DicomTagSQ(0x0018, 0x9340);

        ///<summary>(0018,9341) VR=SQ VM=1 Contrast/Bolus Usage Sequence</summary>
        public readonly static DicomTagSQ ContrastBolusUsageSequence = new DicomTagSQ(0x0018, 0x9341);

        ///<summary>(0018,9342) VR=CS VM=1 Contrast/Bolus Agent Administered</summary>
        public readonly static DicomTagCS ContrastBolusAgentAdministered = new DicomTagCS(0x0018, 0x9342);

        ///<summary>(0018,9343) VR=CS VM=1 Contrast/Bolus Agent Detected</summary>
        public readonly static DicomTagCS ContrastBolusAgentDetected = new DicomTagCS(0x0018, 0x9343);

        ///<summary>(0018,9344) VR=CS VM=1 Contrast/Bolus Agent Phase</summary>
        public readonly static DicomTagCS ContrastBolusAgentPhase = new DicomTagCS(0x0018, 0x9344);

        ///<summary>(0018,9345) VR=FD VM=1 CTDIvol</summary>
        public readonly static DicomTagFD CTDIvol = new DicomTagFD(0x0018, 0x9345);

        ///<summary>(0018,9346) VR=SQ VM=1 CTDI Phantom Type Code Sequence</summary>
        public readonly static DicomTagSQ CTDIPhantomTypeCodeSequence = new DicomTagSQ(0x0018, 0x9346);

        ///<summary>(0018,9351) VR=FL VM=1 Calcium Scoring Mass Factor Patient</summary>
        public readonly static DicomTagFL CalciumScoringMassFactorPatient = new DicomTagFL(0x0018, 0x9351);

        ///<summary>(0018,9352) VR=FL VM=3 Calcium Scoring Mass Factor Device</summary>
        public readonly static DicomTagFLs CalciumScoringMassFactorDevice = new DicomTagFLs(0x0018, 0x9352);

        ///<summary>(0018,9353) VR=FL VM=1 Energy Weighting Factor</summary>
        public readonly static DicomTagFL EnergyWeightingFactor = new DicomTagFL(0x0018, 0x9353);

        ///<summary>(0018,9360) VR=SQ VM=1 CT Additional X-Ray Source Sequence</summary>
        public readonly static DicomTagSQ CTAdditionalXRaySourceSequence = new DicomTagSQ(0x0018, 0x9360);

        ///<summary>(0018,9361) VR=CS VM=1 Multi-energy CT Acquisition</summary>
        public readonly static DicomTagCS MultienergyCTAcquisition = new DicomTagCS(0x0018, 0x9361);

        ///<summary>(0018,9362) VR=SQ VM=1 Multi-energy CT Acquisition Sequence</summary>
        public readonly static DicomTagSQ MultienergyCTAcquisitionSequence = new DicomTagSQ(0x0018, 0x9362);

        ///<summary>(0018,9363) VR=SQ VM=1 Multi-energy CT Processing Sequence</summary>
        public readonly static DicomTagSQ MultienergyCTProcessingSequence = new DicomTagSQ(0x0018, 0x9363);

        ///<summary>(0018,9364) VR=SQ VM=1 Multi-energy CT Characteristics Sequence</summary>
        public readonly static DicomTagSQ MultienergyCTCharacteristicsSequence = new DicomTagSQ(0x0018, 0x9364);

        ///<summary>(0018,9365) VR=SQ VM=1 Multi-energy CT X-Ray Source Sequence</summary>
        public readonly static DicomTagSQ MultienergyCTXRaySourceSequence = new DicomTagSQ(0x0018, 0x9365);

        ///<summary>(0018,9366) VR=US VM=1 X-Ray Source Index</summary>
        public readonly static DicomTagUS XRaySourceIndex = new DicomTagUS(0x0018, 0x9366);

        ///<summary>(0018,9367) VR=UC VM=1 X-Ray Source ID</summary>
        public readonly static DicomTagUC XRaySourceID = new DicomTagUC(0x0018, 0x9367);

        ///<summary>(0018,9368) VR=CS VM=1 Multi-energy Source Technique</summary>
        public readonly static DicomTagCS MultienergySourceTechnique = new DicomTagCS(0x0018, 0x9368);

        ///<summary>(0018,9369) VR=DT VM=1 Source Start DateTime</summary>
        public readonly static DicomTagDT SourceStartDateTime = new DicomTagDT(0x0018, 0x9369);

        ///<summary>(0018,936A) VR=DT VM=1 Source End DateTime</summary>
        public readonly static DicomTagDT SourceEndDateTime = new DicomTagDT(0x0018, 0x936A);

        ///<summary>(0018,936B) VR=US VM=1 Switching Phase Number</summary>
        public readonly static DicomTagUS SwitchingPhaseNumber = new DicomTagUS(0x0018, 0x936B);

        ///<summary>(0018,936C) VR=DS VM=1 Switching Phase Nominal Duration</summary>
        public readonly static DicomTagDS SwitchingPhaseNominalDuration = new DicomTagDS(0x0018, 0x936C);

        ///<summary>(0018,936D) VR=DS VM=1 Switching Phase Transition Duration</summary>
        public readonly static DicomTagDS SwitchingPhaseTransitionDuration = new DicomTagDS(0x0018, 0x936D);

        ///<summary>(0018,936E) VR=DS VM=1 Effective Bin Energy</summary>
        public readonly static DicomTagDS EffectiveBinEnergy = new DicomTagDS(0x0018, 0x936E);

        ///<summary>(0018,936F) VR=SQ VM=1 Multi-energy CT X-Ray Detector Sequence</summary>
        public readonly static DicomTagSQ MultienergyCTXRayDetectorSequence = new DicomTagSQ(0x0018, 0x936F);

        ///<summary>(0018,9370) VR=US VM=1 X-Ray Detector Index</summary>
        public readonly static DicomTagUS XRayDetectorIndex = new DicomTagUS(0x0018, 0x9370);

        ///<summary>(0018,9371) VR=UC VM=1 X-Ray Detector ID</summary>
        public readonly static DicomTagUC XRayDetectorID = new DicomTagUC(0x0018, 0x9371);

        ///<summary>(0018,9372) VR=CS VM=1 Multi-energy Detector Type</summary>
        public readonly static DicomTagCS MultienergyDetectorType = new DicomTagCS(0x0018, 0x9372);

        ///<summary>(0018,9373) VR=ST VM=1 X-Ray Detector Label</summary>
        public readonly static DicomTagST XRayDetectorLabel = new DicomTagST(0x0018, 0x9373);

        ///<summary>(0018,9374) VR=DS VM=1 Nominal Max Energy</summary>
        public readonly static DicomTagDS NominalMaxEnergy = new DicomTagDS(0x0018, 0x9374);

        ///<summary>(0018,9375) VR=DS VM=1 Nominal Min Energy</summary>
        public readonly static DicomTagDS NominalMinEnergy = new DicomTagDS(0x0018, 0x9375);

        ///<summary>(0018,9376) VR=US VM=1-n Referenced X-Ray Detector Index</summary>
        public readonly static DicomTagUSs ReferencedXRayDetectorIndex = new DicomTagUSs(0x0018, 0x9376);

        ///<summary>(0018,9377) VR=US VM=1-n Referenced X-Ray Source Index</summary>
        public readonly static DicomTagUSs ReferencedXRaySourceIndex = new DicomTagUSs(0x0018, 0x9377);

        ///<summary>(0018,9378) VR=US VM=1-n Referenced Path Index</summary>
        public readonly static DicomTagUSs ReferencedPathIndex = new DicomTagUSs(0x0018, 0x9378);

        ///<summary>(0018,9379) VR=SQ VM=1 Multi-energy CT Path Sequence</summary>
        public readonly static DicomTagSQ MultienergyCTPathSequence = new DicomTagSQ(0x0018, 0x9379);

        ///<summary>(0018,937A) VR=US VM=1 Multi-energy CT Path Index</summary>
        public readonly static DicomTagUS MultienergyCTPathIndex = new DicomTagUS(0x0018, 0x937A);

        ///<summary>(0018,937B) VR=UT VM=1 Multi-energy Acquisition Description</summary>
        public readonly static DicomTagUT MultienergyAcquisitionDescription = new DicomTagUT(0x0018, 0x937B);

        ///<summary>(0018,937C) VR=FD VM=1 Monoenergetic Energy Equivalent</summary>
        public readonly static DicomTagFD MonoenergeticEnergyEquivalent = new DicomTagFD(0x0018, 0x937C);

        ///<summary>(0018,937D) VR=SQ VM=1 Material Code Sequence</summary>
        public readonly static DicomTagSQ MaterialCodeSequence = new DicomTagSQ(0x0018, 0x937D);

        ///<summary>(0018,937E) VR=CS VM=1 Decomposition Method</summary>
        public readonly static DicomTagCS DecompositionMethod = new DicomTagCS(0x0018, 0x937E);

        ///<summary>(0018,937F) VR=UT VM=1 Decomposition Description</summary>
        public readonly static DicomTagUT DecompositionDescription = new DicomTagUT(0x0018, 0x937F);

        ///<summary>(0018,9380) VR=SQ VM=1 Decomposition Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ DecompositionAlgorithmIdentificationSequence = new DicomTagSQ(0x0018, 0x9380);

        ///<summary>(0018,9381) VR=SQ VM=1 Decomposition Material Sequence</summary>
        public readonly static DicomTagSQ DecompositionMaterialSequence = new DicomTagSQ(0x0018, 0x9381);

        ///<summary>(0018,9382) VR=SQ VM=1 Material Attenuation Sequence</summary>
        public readonly static DicomTagSQ MaterialAttenuationSequence = new DicomTagSQ(0x0018, 0x9382);

        ///<summary>(0018,9383) VR=DS VM=1 Photon Energy</summary>
        public readonly static DicomTagDS PhotonEnergy = new DicomTagDS(0x0018, 0x9383);

        ///<summary>(0018,9384) VR=DS VM=1 X-Ray Mass Attenuation Coefficient</summary>
        public readonly static DicomTagDS XRayMassAttenuationCoefficient = new DicomTagDS(0x0018, 0x9384);

        ///<summary>(0018,9390) VR=SQ VM=1 Metal Artifact Reduction Sequence</summary>
        public readonly static DicomTagSQ MetalArtifactReductionSequence = new DicomTagSQ(0x0018, 0x9390);

        ///<summary>(0018,9391) VR=CS VM=1 Metal Artifact Reduction Applied</summary>
        public readonly static DicomTagCS MetalArtifactReductionApplied = new DicomTagCS(0x0018, 0x9391);

        ///<summary>(0018,9392) VR=SQ VM=1 Metal Artifact Reduction Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ MetalArtifactReductionAlgorithmIdentificationSequence = new DicomTagSQ(0x0018, 0x9392);

        ///<summary>(0018,9401) VR=SQ VM=1 Projection Pixel Calibration Sequence</summary>
        public readonly static DicomTagSQ ProjectionPixelCalibrationSequence = new DicomTagSQ(0x0018, 0x9401);

        ///<summary>(0018,9402) VR=FL VM=1 Distance Source to Isocenter</summary>
        public readonly static DicomTagFL DistanceSourceToIsocenter = new DicomTagFL(0x0018, 0x9402);

        ///<summary>(0018,9403) VR=FL VM=1 Distance Object to Table Top</summary>
        public readonly static DicomTagFL DistanceObjectToTableTop = new DicomTagFL(0x0018, 0x9403);

        ///<summary>(0018,9404) VR=FL VM=2 Object Pixel Spacing in Center of Beam</summary>
        public readonly static DicomTagFLs ObjectPixelSpacingInCenterOfBeam = new DicomTagFLs(0x0018, 0x9404);

        ///<summary>(0018,9405) VR=SQ VM=1 Positioner Position Sequence</summary>
        public readonly static DicomTagSQ PositionerPositionSequence = new DicomTagSQ(0x0018, 0x9405);

        ///<summary>(0018,9406) VR=SQ VM=1 Table Position Sequence</summary>
        public readonly static DicomTagSQ TablePositionSequence = new DicomTagSQ(0x0018, 0x9406);

        ///<summary>(0018,9407) VR=SQ VM=1 Collimator Shape Sequence</summary>
        public readonly static DicomTagSQ CollimatorShapeSequence = new DicomTagSQ(0x0018, 0x9407);

        ///<summary>(0018,9410) VR=CS VM=1 Planes in Acquisition</summary>
        public readonly static DicomTagCS PlanesInAcquisition = new DicomTagCS(0x0018, 0x9410);

        ///<summary>(0018,9412) VR=SQ VM=1 XA/XRF Frame Characteristics Sequence</summary>
        public readonly static DicomTagSQ XAXRFFrameCharacteristicsSequence = new DicomTagSQ(0x0018, 0x9412);

        ///<summary>(0018,9417) VR=SQ VM=1 Frame Acquisition Sequence</summary>
        public readonly static DicomTagSQ FrameAcquisitionSequence = new DicomTagSQ(0x0018, 0x9417);

        ///<summary>(0018,9420) VR=CS VM=1 X-Ray Receptor Type</summary>
        public readonly static DicomTagCS XRayReceptorType = new DicomTagCS(0x0018, 0x9420);

        ///<summary>(0018,9423) VR=LO VM=1 Acquisition Protocol Name</summary>
        public readonly static DicomTagLO AcquisitionProtocolName = new DicomTagLO(0x0018, 0x9423);

        ///<summary>(0018,9424) VR=LT VM=1 Acquisition Protocol Description</summary>
        public readonly static DicomTagLT AcquisitionProtocolDescription = new DicomTagLT(0x0018, 0x9424);

        ///<summary>(0018,9425) VR=CS VM=1 Contrast/Bolus Ingredient Opaque</summary>
        public readonly static DicomTagCS ContrastBolusIngredientOpaque = new DicomTagCS(0x0018, 0x9425);

        ///<summary>(0018,9426) VR=FL VM=1 Distance Receptor Plane to Detector Housing</summary>
        public readonly static DicomTagFL DistanceReceptorPlaneToDetectorHousing = new DicomTagFL(0x0018, 0x9426);

        ///<summary>(0018,9427) VR=CS VM=1 Intensifier Active Shape</summary>
        public readonly static DicomTagCS IntensifierActiveShape = new DicomTagCS(0x0018, 0x9427);

        ///<summary>(0018,9428) VR=FL VM=1-2 Intensifier Active Dimension(s)</summary>
        public readonly static DicomTagFLs IntensifierActiveDimensions = new DicomTagFLs(0x0018, 0x9428);

        ///<summary>(0018,9429) VR=FL VM=2 Physical Detector Size</summary>
        public readonly static DicomTagFLs PhysicalDetectorSize = new DicomTagFLs(0x0018, 0x9429);

        ///<summary>(0018,9430) VR=FL VM=2 Position of Isocenter Projection</summary>
        public readonly static DicomTagFLs PositionOfIsocenterProjection = new DicomTagFLs(0x0018, 0x9430);

        ///<summary>(0018,9432) VR=SQ VM=1 Field of View Sequence</summary>
        public readonly static DicomTagSQ FieldOfViewSequence = new DicomTagSQ(0x0018, 0x9432);

        ///<summary>(0018,9433) VR=LO VM=1 Field of View Description</summary>
        public readonly static DicomTagLO FieldOfViewDescription = new DicomTagLO(0x0018, 0x9433);

        ///<summary>(0018,9434) VR=SQ VM=1 Exposure Control Sensing Regions Sequence</summary>
        public readonly static DicomTagSQ ExposureControlSensingRegionsSequence = new DicomTagSQ(0x0018, 0x9434);

        ///<summary>(0018,9435) VR=CS VM=1 Exposure Control Sensing Region Shape</summary>
        public readonly static DicomTagCS ExposureControlSensingRegionShape = new DicomTagCS(0x0018, 0x9435);

        ///<summary>(0018,9436) VR=SS VM=1 Exposure Control Sensing Region Left Vertical Edge</summary>
        public readonly static DicomTagSS ExposureControlSensingRegionLeftVerticalEdge = new DicomTagSS(0x0018, 0x9436);

        ///<summary>(0018,9437) VR=SS VM=1 Exposure Control Sensing Region Right Vertical Edge</summary>
        public readonly static DicomTagSS ExposureControlSensingRegionRightVerticalEdge = new DicomTagSS(0x0018, 0x9437);

        ///<summary>(0018,9438) VR=SS VM=1 Exposure Control Sensing Region Upper Horizontal Edge</summary>
        public readonly static DicomTagSS ExposureControlSensingRegionUpperHorizontalEdge = new DicomTagSS(0x0018, 0x9438);

        ///<summary>(0018,9439) VR=SS VM=1 Exposure Control Sensing Region Lower Horizontal Edge</summary>
        public readonly static DicomTagSS ExposureControlSensingRegionLowerHorizontalEdge = new DicomTagSS(0x0018, 0x9439);

        ///<summary>(0018,9440) VR=SS VM=2 Center of Circular Exposure Control Sensing Region</summary>
        public readonly static DicomTagSSs CenterOfCircularExposureControlSensingRegion = new DicomTagSSs(0x0018, 0x9440);

        ///<summary>(0018,9441) VR=US VM=1 Radius of Circular Exposure Control Sensing Region</summary>
        public readonly static DicomTagUS RadiusOfCircularExposureControlSensingRegion = new DicomTagUS(0x0018, 0x9441);

        ///<summary>(0018,9442) VR=SS VM=2-n Vertices of the Polygonal Exposure Control Sensing Region</summary>
        public readonly static DicomTagSSs VerticesOfThePolygonalExposureControlSensingRegion = new DicomTagSSs(0x0018, 0x9442);

        ///<summary>(0018,9447) VR=FL VM=1 Column Angulation (Patient)</summary>
        public readonly static DicomTagFL ColumnAngulationPatient = new DicomTagFL(0x0018, 0x9447);

        ///<summary>(0018,9449) VR=FL VM=1 Beam Angle</summary>
        public readonly static DicomTagFL BeamAngle = new DicomTagFL(0x0018, 0x9449);

        ///<summary>(0018,9451) VR=SQ VM=1 Frame Detector Parameters Sequence</summary>
        public readonly static DicomTagSQ FrameDetectorParametersSequence = new DicomTagSQ(0x0018, 0x9451);

        ///<summary>(0018,9452) VR=FL VM=1 Calculated Anatomy Thickness</summary>
        public readonly static DicomTagFL CalculatedAnatomyThickness = new DicomTagFL(0x0018, 0x9452);

        ///<summary>(0018,9455) VR=SQ VM=1 Calibration Sequence</summary>
        public readonly static DicomTagSQ CalibrationSequence = new DicomTagSQ(0x0018, 0x9455);

        ///<summary>(0018,9456) VR=SQ VM=1 Object Thickness Sequence</summary>
        public readonly static DicomTagSQ ObjectThicknessSequence = new DicomTagSQ(0x0018, 0x9456);

        ///<summary>(0018,9457) VR=CS VM=1 Plane Identification</summary>
        public readonly static DicomTagCS PlaneIdentification = new DicomTagCS(0x0018, 0x9457);

        ///<summary>(0018,9461) VR=FL VM=1-2 Field of View Dimension(s) in Float</summary>
        public readonly static DicomTagFLs FieldOfViewDimensionsInFloat = new DicomTagFLs(0x0018, 0x9461);

        ///<summary>(0018,9462) VR=SQ VM=1 Isocenter Reference System Sequence</summary>
        public readonly static DicomTagSQ IsocenterReferenceSystemSequence = new DicomTagSQ(0x0018, 0x9462);

        ///<summary>(0018,9463) VR=FL VM=1 Positioner Isocenter Primary Angle</summary>
        public readonly static DicomTagFL PositionerIsocenterPrimaryAngle = new DicomTagFL(0x0018, 0x9463);

        ///<summary>(0018,9464) VR=FL VM=1 Positioner Isocenter Secondary Angle</summary>
        public readonly static DicomTagFL PositionerIsocenterSecondaryAngle = new DicomTagFL(0x0018, 0x9464);

        ///<summary>(0018,9465) VR=FL VM=1 Positioner Isocenter Detector Rotation Angle</summary>
        public readonly static DicomTagFL PositionerIsocenterDetectorRotationAngle = new DicomTagFL(0x0018, 0x9465);

        ///<summary>(0018,9466) VR=FL VM=1 Table X Position to Isocenter</summary>
        public readonly static DicomTagFL TableXPositionToIsocenter = new DicomTagFL(0x0018, 0x9466);

        ///<summary>(0018,9467) VR=FL VM=1 Table Y Position to Isocenter</summary>
        public readonly static DicomTagFL TableYPositionToIsocenter = new DicomTagFL(0x0018, 0x9467);

        ///<summary>(0018,9468) VR=FL VM=1 Table Z Position to Isocenter</summary>
        public readonly static DicomTagFL TableZPositionToIsocenter = new DicomTagFL(0x0018, 0x9468);

        ///<summary>(0018,9469) VR=FL VM=1 Table Horizontal Rotation Angle</summary>
        public readonly static DicomTagFL TableHorizontalRotationAngle = new DicomTagFL(0x0018, 0x9469);

        ///<summary>(0018,9470) VR=FL VM=1 Table Head Tilt Angle</summary>
        public readonly static DicomTagFL TableHeadTiltAngle = new DicomTagFL(0x0018, 0x9470);

        ///<summary>(0018,9471) VR=FL VM=1 Table Cradle Tilt Angle</summary>
        public readonly static DicomTagFL TableCradleTiltAngle = new DicomTagFL(0x0018, 0x9471);

        ///<summary>(0018,9472) VR=SQ VM=1 Frame Display Shutter Sequence</summary>
        public readonly static DicomTagSQ FrameDisplayShutterSequence = new DicomTagSQ(0x0018, 0x9472);

        ///<summary>(0018,9473) VR=FL VM=1 Acquired Image Area Dose Product</summary>
        public readonly static DicomTagFL AcquiredImageAreaDoseProduct = new DicomTagFL(0x0018, 0x9473);

        ///<summary>(0018,9474) VR=CS VM=1 C-arm Positioner Tabletop Relationship</summary>
        public readonly static DicomTagCS CArmPositionerTabletopRelationship = new DicomTagCS(0x0018, 0x9474);

        ///<summary>(0018,9476) VR=SQ VM=1 X-Ray Geometry Sequence</summary>
        public readonly static DicomTagSQ XRayGeometrySequence = new DicomTagSQ(0x0018, 0x9476);

        ///<summary>(0018,9477) VR=SQ VM=1 Irradiation Event Identification Sequence</summary>
        public readonly static DicomTagSQ IrradiationEventIdentificationSequence = new DicomTagSQ(0x0018, 0x9477);

        ///<summary>(0018,9504) VR=SQ VM=1 X-Ray 3D Frame Type Sequence</summary>
        public readonly static DicomTagSQ XRay3DFrameTypeSequence = new DicomTagSQ(0x0018, 0x9504);

        ///<summary>(0018,9506) VR=SQ VM=1 Contributing Sources Sequence</summary>
        public readonly static DicomTagSQ ContributingSourcesSequence = new DicomTagSQ(0x0018, 0x9506);

        ///<summary>(0018,9507) VR=SQ VM=1 X-Ray 3D Acquisition Sequence</summary>
        public readonly static DicomTagSQ XRay3DAcquisitionSequence = new DicomTagSQ(0x0018, 0x9507);

        ///<summary>(0018,9508) VR=FL VM=1 Primary Positioner Scan Arc</summary>
        public readonly static DicomTagFL PrimaryPositionerScanArc = new DicomTagFL(0x0018, 0x9508);

        ///<summary>(0018,9509) VR=FL VM=1 Secondary Positioner Scan Arc</summary>
        public readonly static DicomTagFL SecondaryPositionerScanArc = new DicomTagFL(0x0018, 0x9509);

        ///<summary>(0018,9510) VR=FL VM=1 Primary Positioner Scan Start Angle</summary>
        public readonly static DicomTagFL PrimaryPositionerScanStartAngle = new DicomTagFL(0x0018, 0x9510);

        ///<summary>(0018,9511) VR=FL VM=1 Secondary Positioner Scan Start Angle</summary>
        public readonly static DicomTagFL SecondaryPositionerScanStartAngle = new DicomTagFL(0x0018, 0x9511);

        ///<summary>(0018,9514) VR=FL VM=1 Primary Positioner Increment</summary>
        public readonly static DicomTagFL PrimaryPositionerIncrement = new DicomTagFL(0x0018, 0x9514);

        ///<summary>(0018,9515) VR=FL VM=1 Secondary Positioner Increment</summary>
        public readonly static DicomTagFL SecondaryPositionerIncrement = new DicomTagFL(0x0018, 0x9515);

        ///<summary>(0018,9516) VR=DT VM=1 Start Acquisition DateTime</summary>
        public readonly static DicomTagDT StartAcquisitionDateTime = new DicomTagDT(0x0018, 0x9516);

        ///<summary>(0018,9517) VR=DT VM=1 End Acquisition DateTime</summary>
        public readonly static DicomTagDT EndAcquisitionDateTime = new DicomTagDT(0x0018, 0x9517);

        ///<summary>(0018,9518) VR=SS VM=1 Primary Positioner Increment Sign</summary>
        public readonly static DicomTagSS PrimaryPositionerIncrementSign = new DicomTagSS(0x0018, 0x9518);

        ///<summary>(0018,9519) VR=SS VM=1 Secondary Positioner Increment Sign</summary>
        public readonly static DicomTagSS SecondaryPositionerIncrementSign = new DicomTagSS(0x0018, 0x9519);

        ///<summary>(0018,9524) VR=LO VM=1 Application Name</summary>
        public readonly static DicomTagLO ApplicationName = new DicomTagLO(0x0018, 0x9524);

        ///<summary>(0018,9525) VR=LO VM=1 Application Version</summary>
        public readonly static DicomTagLO ApplicationVersion = new DicomTagLO(0x0018, 0x9525);

        ///<summary>(0018,9526) VR=LO VM=1 Application Manufacturer</summary>
        public readonly static DicomTagLO ApplicationManufacturer = new DicomTagLO(0x0018, 0x9526);

        ///<summary>(0018,9527) VR=CS VM=1 Algorithm Type</summary>
        public readonly static DicomTagCS AlgorithmType = new DicomTagCS(0x0018, 0x9527);

        ///<summary>(0018,9528) VR=LO VM=1 Algorithm Description</summary>
        public readonly static DicomTagLO AlgorithmDescription = new DicomTagLO(0x0018, 0x9528);

        ///<summary>(0018,9530) VR=SQ VM=1 X-Ray 3D Reconstruction Sequence</summary>
        public readonly static DicomTagSQ XRay3DReconstructionSequence = new DicomTagSQ(0x0018, 0x9530);

        ///<summary>(0018,9531) VR=LO VM=1 Reconstruction Description</summary>
        public readonly static DicomTagLO ReconstructionDescription = new DicomTagLO(0x0018, 0x9531);

        ///<summary>(0018,9538) VR=SQ VM=1 Per Projection Acquisition Sequence</summary>
        public readonly static DicomTagSQ PerProjectionAcquisitionSequence = new DicomTagSQ(0x0018, 0x9538);

        ///<summary>(0018,9541) VR=SQ VM=1 Detector Position Sequence</summary>
        public readonly static DicomTagSQ DetectorPositionSequence = new DicomTagSQ(0x0018, 0x9541);

        ///<summary>(0018,9542) VR=SQ VM=1 X-Ray Acquisition Dose Sequence</summary>
        public readonly static DicomTagSQ XRayAcquisitionDoseSequence = new DicomTagSQ(0x0018, 0x9542);

        ///<summary>(0018,9543) VR=FD VM=1 X-Ray Source Isocenter Primary Angle</summary>
        public readonly static DicomTagFD XRaySourceIsocenterPrimaryAngle = new DicomTagFD(0x0018, 0x9543);

        ///<summary>(0018,9544) VR=FD VM=1 X-Ray Source Isocenter Secondary Angle</summary>
        public readonly static DicomTagFD XRaySourceIsocenterSecondaryAngle = new DicomTagFD(0x0018, 0x9544);

        ///<summary>(0018,9545) VR=FD VM=1 Breast Support Isocenter Primary Angle</summary>
        public readonly static DicomTagFD BreastSupportIsocenterPrimaryAngle = new DicomTagFD(0x0018, 0x9545);

        ///<summary>(0018,9546) VR=FD VM=1 Breast Support Isocenter Secondary Angle</summary>
        public readonly static DicomTagFD BreastSupportIsocenterSecondaryAngle = new DicomTagFD(0x0018, 0x9546);

        ///<summary>(0018,9547) VR=FD VM=1 Breast Support X Position to Isocenter</summary>
        public readonly static DicomTagFD BreastSupportXPositionToIsocenter = new DicomTagFD(0x0018, 0x9547);

        ///<summary>(0018,9548) VR=FD VM=1 Breast Support Y Position to Isocenter</summary>
        public readonly static DicomTagFD BreastSupportYPositionToIsocenter = new DicomTagFD(0x0018, 0x9548);

        ///<summary>(0018,9549) VR=FD VM=1 Breast Support Z Position to Isocenter</summary>
        public readonly static DicomTagFD BreastSupportZPositionToIsocenter = new DicomTagFD(0x0018, 0x9549);

        ///<summary>(0018,9550) VR=FD VM=1 Detector Isocenter Primary Angle</summary>
        public readonly static DicomTagFD DetectorIsocenterPrimaryAngle = new DicomTagFD(0x0018, 0x9550);

        ///<summary>(0018,9551) VR=FD VM=1 Detector Isocenter Secondary Angle</summary>
        public readonly static DicomTagFD DetectorIsocenterSecondaryAngle = new DicomTagFD(0x0018, 0x9551);

        ///<summary>(0018,9552) VR=FD VM=1 Detector X Position to Isocenter</summary>
        public readonly static DicomTagFD DetectorXPositionToIsocenter = new DicomTagFD(0x0018, 0x9552);

        ///<summary>(0018,9553) VR=FD VM=1 Detector Y Position to Isocenter</summary>
        public readonly static DicomTagFD DetectorYPositionToIsocenter = new DicomTagFD(0x0018, 0x9553);

        ///<summary>(0018,9554) VR=FD VM=1 Detector Z Position to Isocenter</summary>
        public readonly static DicomTagFD DetectorZPositionToIsocenter = new DicomTagFD(0x0018, 0x9554);

        ///<summary>(0018,9555) VR=SQ VM=1 X-Ray Grid Sequence</summary>
        public readonly static DicomTagSQ XRayGridSequence = new DicomTagSQ(0x0018, 0x9555);

        ///<summary>(0018,9556) VR=SQ VM=1 X-Ray Filter Sequence</summary>
        public readonly static DicomTagSQ XRayFilterSequence = new DicomTagSQ(0x0018, 0x9556);

        ///<summary>(0018,9557) VR=FD VM=3 Detector Active Area TLHC Position</summary>
        public readonly static DicomTagFDs DetectorActiveAreaTLHCPosition = new DicomTagFDs(0x0018, 0x9557);

        ///<summary>(0018,9558) VR=FD VM=6 Detector Active Area Orientation</summary>
        public readonly static DicomTagFDs DetectorActiveAreaOrientation = new DicomTagFDs(0x0018, 0x9558);

        ///<summary>(0018,9559) VR=CS VM=1 Positioner Primary Angle Direction</summary>
        public readonly static DicomTagCS PositionerPrimaryAngleDirection = new DicomTagCS(0x0018, 0x9559);

        ///<summary>(0018,9601) VR=SQ VM=1 Diffusion b-matrix Sequence</summary>
        public readonly static DicomTagSQ DiffusionBMatrixSequence = new DicomTagSQ(0x0018, 0x9601);

        ///<summary>(0018,9602) VR=FD VM=1 Diffusion b-value XX</summary>
        public readonly static DicomTagFD DiffusionBValueXX = new DicomTagFD(0x0018, 0x9602);

        ///<summary>(0018,9603) VR=FD VM=1 Diffusion b-value XY</summary>
        public readonly static DicomTagFD DiffusionBValueXY = new DicomTagFD(0x0018, 0x9603);

        ///<summary>(0018,9604) VR=FD VM=1 Diffusion b-value XZ</summary>
        public readonly static DicomTagFD DiffusionBValueXZ = new DicomTagFD(0x0018, 0x9604);

        ///<summary>(0018,9605) VR=FD VM=1 Diffusion b-value YY</summary>
        public readonly static DicomTagFD DiffusionBValueYY = new DicomTagFD(0x0018, 0x9605);

        ///<summary>(0018,9606) VR=FD VM=1 Diffusion b-value YZ</summary>
        public readonly static DicomTagFD DiffusionBValueYZ = new DicomTagFD(0x0018, 0x9606);

        ///<summary>(0018,9607) VR=FD VM=1 Diffusion b-value ZZ</summary>
        public readonly static DicomTagFD DiffusionBValueZZ = new DicomTagFD(0x0018, 0x9607);

        ///<summary>(0018,9621) VR=SQ VM=1 Functional MR Sequence</summary>
        public readonly static DicomTagSQ FunctionalMRSequence = new DicomTagSQ(0x0018, 0x9621);

        ///<summary>(0018,9622) VR=CS VM=1 Functional Settling Phase Frames Present</summary>
        public readonly static DicomTagCS FunctionalSettlingPhaseFramesPresent = new DicomTagCS(0x0018, 0x9622);

        ///<summary>(0018,9623) VR=DT VM=1 Functional Sync Pulse</summary>
        public readonly static DicomTagDT FunctionalSyncPulse = new DicomTagDT(0x0018, 0x9623);

        ///<summary>(0018,9624) VR=CS VM=1 Settling Phase Frame</summary>
        public readonly static DicomTagCS SettlingPhaseFrame = new DicomTagCS(0x0018, 0x9624);

        ///<summary>(0018,9701) VR=DT VM=1 Decay Correction DateTime</summary>
        public readonly static DicomTagDT DecayCorrectionDateTime = new DicomTagDT(0x0018, 0x9701);

        ///<summary>(0018,9715) VR=FD VM=1 Start Density Threshold</summary>
        public readonly static DicomTagFD StartDensityThreshold = new DicomTagFD(0x0018, 0x9715);

        ///<summary>(0018,9716) VR=FD VM=1 Start Relative Density Difference Threshold</summary>
        public readonly static DicomTagFD StartRelativeDensityDifferenceThreshold = new DicomTagFD(0x0018, 0x9716);

        ///<summary>(0018,9717) VR=FD VM=1 Start Cardiac Trigger Count Threshold</summary>
        public readonly static DicomTagFD StartCardiacTriggerCountThreshold = new DicomTagFD(0x0018, 0x9717);

        ///<summary>(0018,9718) VR=FD VM=1 Start Respiratory Trigger Count Threshold</summary>
        public readonly static DicomTagFD StartRespiratoryTriggerCountThreshold = new DicomTagFD(0x0018, 0x9718);

        ///<summary>(0018,9719) VR=FD VM=1 Termination Counts Threshold</summary>
        public readonly static DicomTagFD TerminationCountsThreshold = new DicomTagFD(0x0018, 0x9719);

        ///<summary>(0018,9720) VR=FD VM=1 Termination Density Threshold</summary>
        public readonly static DicomTagFD TerminationDensityThreshold = new DicomTagFD(0x0018, 0x9720);

        ///<summary>(0018,9721) VR=FD VM=1 Termination Relative Density Threshold</summary>
        public readonly static DicomTagFD TerminationRelativeDensityThreshold = new DicomTagFD(0x0018, 0x9721);

        ///<summary>(0018,9722) VR=FD VM=1 Termination Time Threshold</summary>
        public readonly static DicomTagFD TerminationTimeThreshold = new DicomTagFD(0x0018, 0x9722);

        ///<summary>(0018,9723) VR=FD VM=1 Termination Cardiac Trigger Count Threshold</summary>
        public readonly static DicomTagFD TerminationCardiacTriggerCountThreshold = new DicomTagFD(0x0018, 0x9723);

        ///<summary>(0018,9724) VR=FD VM=1 Termination Respiratory Trigger Count Threshold</summary>
        public readonly static DicomTagFD TerminationRespiratoryTriggerCountThreshold = new DicomTagFD(0x0018, 0x9724);

        ///<summary>(0018,9725) VR=CS VM=1 Detector Geometry</summary>
        public readonly static DicomTagCS DetectorGeometry = new DicomTagCS(0x0018, 0x9725);

        ///<summary>(0018,9726) VR=FD VM=1 Transverse Detector Separation</summary>
        public readonly static DicomTagFD TransverseDetectorSeparation = new DicomTagFD(0x0018, 0x9726);

        ///<summary>(0018,9727) VR=FD VM=1 Axial Detector Dimension</summary>
        public readonly static DicomTagFD AxialDetectorDimension = new DicomTagFD(0x0018, 0x9727);

        ///<summary>(0018,9729) VR=US VM=1 Radiopharmaceutical Agent Number</summary>
        public readonly static DicomTagUS RadiopharmaceuticalAgentNumber = new DicomTagUS(0x0018, 0x9729);

        ///<summary>(0018,9732) VR=SQ VM=1 PET Frame Acquisition Sequence</summary>
        public readonly static DicomTagSQ PETFrameAcquisitionSequence = new DicomTagSQ(0x0018, 0x9732);

        ///<summary>(0018,9733) VR=SQ VM=1 PET Detector Motion Details Sequence</summary>
        public readonly static DicomTagSQ PETDetectorMotionDetailsSequence = new DicomTagSQ(0x0018, 0x9733);

        ///<summary>(0018,9734) VR=SQ VM=1 PET Table Dynamics Sequence</summary>
        public readonly static DicomTagSQ PETTableDynamicsSequence = new DicomTagSQ(0x0018, 0x9734);

        ///<summary>(0018,9735) VR=SQ VM=1 PET Position Sequence</summary>
        public readonly static DicomTagSQ PETPositionSequence = new DicomTagSQ(0x0018, 0x9735);

        ///<summary>(0018,9736) VR=SQ VM=1 PET Frame Correction Factors Sequence</summary>
        public readonly static DicomTagSQ PETFrameCorrectionFactorsSequence = new DicomTagSQ(0x0018, 0x9736);

        ///<summary>(0018,9737) VR=SQ VM=1 Radiopharmaceutical Usage Sequence</summary>
        public readonly static DicomTagSQ RadiopharmaceuticalUsageSequence = new DicomTagSQ(0x0018, 0x9737);

        ///<summary>(0018,9738) VR=CS VM=1 Attenuation Correction Source</summary>
        public readonly static DicomTagCS AttenuationCorrectionSource = new DicomTagCS(0x0018, 0x9738);

        ///<summary>(0018,9739) VR=US VM=1 Number of Iterations</summary>
        public readonly static DicomTagUS NumberOfIterations = new DicomTagUS(0x0018, 0x9739);

        ///<summary>(0018,9740) VR=US VM=1 Number of Subsets</summary>
        public readonly static DicomTagUS NumberOfSubsets = new DicomTagUS(0x0018, 0x9740);

        ///<summary>(0018,9749) VR=SQ VM=1 PET Reconstruction Sequence</summary>
        public readonly static DicomTagSQ PETReconstructionSequence = new DicomTagSQ(0x0018, 0x9749);

        ///<summary>(0018,9751) VR=SQ VM=1 PET Frame Type Sequence</summary>
        public readonly static DicomTagSQ PETFrameTypeSequence = new DicomTagSQ(0x0018, 0x9751);

        ///<summary>(0018,9755) VR=CS VM=1 Time of Flight Information Used</summary>
        public readonly static DicomTagCS TimeOfFlightInformationUsed = new DicomTagCS(0x0018, 0x9755);

        ///<summary>(0018,9756) VR=CS VM=1 Reconstruction Type</summary>
        public readonly static DicomTagCS ReconstructionType = new DicomTagCS(0x0018, 0x9756);

        ///<summary>(0018,9758) VR=CS VM=1 Decay Corrected</summary>
        public readonly static DicomTagCS DecayCorrected = new DicomTagCS(0x0018, 0x9758);

        ///<summary>(0018,9759) VR=CS VM=1 Attenuation Corrected</summary>
        public readonly static DicomTagCS AttenuationCorrected = new DicomTagCS(0x0018, 0x9759);

        ///<summary>(0018,9760) VR=CS VM=1 Scatter Corrected</summary>
        public readonly static DicomTagCS ScatterCorrected = new DicomTagCS(0x0018, 0x9760);

        ///<summary>(0018,9761) VR=CS VM=1 Dead Time Corrected</summary>
        public readonly static DicomTagCS DeadTimeCorrected = new DicomTagCS(0x0018, 0x9761);

        ///<summary>(0018,9762) VR=CS VM=1 Gantry Motion Corrected</summary>
        public readonly static DicomTagCS GantryMotionCorrected = new DicomTagCS(0x0018, 0x9762);

        ///<summary>(0018,9763) VR=CS VM=1 Patient Motion Corrected</summary>
        public readonly static DicomTagCS PatientMotionCorrected = new DicomTagCS(0x0018, 0x9763);

        ///<summary>(0018,9764) VR=CS VM=1 Count Loss Normalization Corrected</summary>
        public readonly static DicomTagCS CountLossNormalizationCorrected = new DicomTagCS(0x0018, 0x9764);

        ///<summary>(0018,9765) VR=CS VM=1 Randoms Corrected</summary>
        public readonly static DicomTagCS RandomsCorrected = new DicomTagCS(0x0018, 0x9765);

        ///<summary>(0018,9766) VR=CS VM=1 Non-uniform Radial Sampling Corrected</summary>
        public readonly static DicomTagCS NonUniformRadialSamplingCorrected = new DicomTagCS(0x0018, 0x9766);

        ///<summary>(0018,9767) VR=CS VM=1 Sensitivity Calibrated</summary>
        public readonly static DicomTagCS SensitivityCalibrated = new DicomTagCS(0x0018, 0x9767);

        ///<summary>(0018,9768) VR=CS VM=1 Detector Normalization Correction</summary>
        public readonly static DicomTagCS DetectorNormalizationCorrection = new DicomTagCS(0x0018, 0x9768);

        ///<summary>(0018,9769) VR=CS VM=1 Iterative Reconstruction Method</summary>
        public readonly static DicomTagCS IterativeReconstructionMethod = new DicomTagCS(0x0018, 0x9769);

        ///<summary>(0018,9770) VR=CS VM=1 Attenuation Correction Temporal Relationship</summary>
        public readonly static DicomTagCS AttenuationCorrectionTemporalRelationship = new DicomTagCS(0x0018, 0x9770);

        ///<summary>(0018,9771) VR=SQ VM=1 Patient Physiological State Sequence</summary>
        public readonly static DicomTagSQ PatientPhysiologicalStateSequence = new DicomTagSQ(0x0018, 0x9771);

        ///<summary>(0018,9772) VR=SQ VM=1 Patient Physiological State Code Sequence</summary>
        public readonly static DicomTagSQ PatientPhysiologicalStateCodeSequence = new DicomTagSQ(0x0018, 0x9772);

        ///<summary>(0018,9801) VR=FD VM=1-n Depth(s) of Focus</summary>
        public readonly static DicomTagFDs DepthsOfFocus = new DicomTagFDs(0x0018, 0x9801);

        ///<summary>(0018,9803) VR=SQ VM=1 Excluded Intervals Sequence</summary>
        public readonly static DicomTagSQ ExcludedIntervalsSequence = new DicomTagSQ(0x0018, 0x9803);

        ///<summary>(0018,9804) VR=DT VM=1 Exclusion Start DateTime</summary>
        public readonly static DicomTagDT ExclusionStartDateTime = new DicomTagDT(0x0018, 0x9804);

        ///<summary>(0018,9805) VR=FD VM=1 Exclusion Duration</summary>
        public readonly static DicomTagFD ExclusionDuration = new DicomTagFD(0x0018, 0x9805);

        ///<summary>(0018,9806) VR=SQ VM=1 US Image Description Sequence</summary>
        public readonly static DicomTagSQ USImageDescriptionSequence = new DicomTagSQ(0x0018, 0x9806);

        ///<summary>(0018,9807) VR=SQ VM=1 Image Data Type Sequence</summary>
        public readonly static DicomTagSQ ImageDataTypeSequence = new DicomTagSQ(0x0018, 0x9807);

        ///<summary>(0018,9808) VR=CS VM=1 Data Type</summary>
        public readonly static DicomTagCS DataType = new DicomTagCS(0x0018, 0x9808);

        ///<summary>(0018,9809) VR=SQ VM=1 Transducer Scan Pattern Code Sequence</summary>
        public readonly static DicomTagSQ TransducerScanPatternCodeSequence = new DicomTagSQ(0x0018, 0x9809);

        ///<summary>(0018,980B) VR=CS VM=1 Aliased Data Type</summary>
        public readonly static DicomTagCS AliasedDataType = new DicomTagCS(0x0018, 0x980B);

        ///<summary>(0018,980C) VR=CS VM=1 Position Measuring Device Used</summary>
        public readonly static DicomTagCS PositionMeasuringDeviceUsed = new DicomTagCS(0x0018, 0x980C);

        ///<summary>(0018,980D) VR=SQ VM=1 Transducer Geometry Code Sequence</summary>
        public readonly static DicomTagSQ TransducerGeometryCodeSequence = new DicomTagSQ(0x0018, 0x980D);

        ///<summary>(0018,980E) VR=SQ VM=1 Transducer Beam Steering Code Sequence</summary>
        public readonly static DicomTagSQ TransducerBeamSteeringCodeSequence = new DicomTagSQ(0x0018, 0x980E);

        ///<summary>(0018,980F) VR=SQ VM=1 Transducer Application Code Sequence</summary>
        public readonly static DicomTagSQ TransducerApplicationCodeSequence = new DicomTagSQ(0x0018, 0x980F);

        ///<summary>(0018,9810) VR=US/SS VM=1 Zero Velocity Pixel Value</summary>
        public readonly static DicomTagUSSS ZeroVelocityPixelValue = new DicomTagUSSS(0x0018, 0x9810);

        ///<summary>(0018,9821) VR=SQ VM=1 Photoacoustic Excitation Characteristics Sequence</summary>
        public readonly static DicomTagSQ PhotoacousticExcitationCharacteristicsSequence = new DicomTagSQ(0x0018, 0x9821);

        ///<summary>(0018,9822) VR=FD VM=1 Excitation Spectral Width</summary>
        public readonly static DicomTagFD ExcitationSpectralWidth = new DicomTagFD(0x0018, 0x9822);

        ///<summary>(0018,9823) VR=FD VM=1 Excitation Energy</summary>
        public readonly static DicomTagFD ExcitationEnergy = new DicomTagFD(0x0018, 0x9823);

        ///<summary>(0018,9824) VR=FD VM=1 Excitation Pulse Duration</summary>
        public readonly static DicomTagFD ExcitationPulseDuration = new DicomTagFD(0x0018, 0x9824);

        ///<summary>(0018,9825) VR=SQ VM=1 Excitation Wavelength Sequence</summary>
        public readonly static DicomTagSQ ExcitationWavelengthSequence = new DicomTagSQ(0x0018, 0x9825);

        ///<summary>(0018,9826) VR=FD VM=1 Excitation Wavelength</summary>
        public readonly static DicomTagFD ExcitationWavelength = new DicomTagFD(0x0018, 0x9826);

        ///<summary>(0018,9828) VR=CS VM=1 Illumination Translation Flag</summary>
        public readonly static DicomTagCS IlluminationTranslationFlag = new DicomTagCS(0x0018, 0x9828);

        ///<summary>(0018,9829) VR=CS VM=1 Acoustic Coupling Medium Flag</summary>
        public readonly static DicomTagCS AcousticCouplingMediumFlag = new DicomTagCS(0x0018, 0x9829);

        ///<summary>(0018,982A) VR=SQ VM=1 Acoustic Coupling Medium Code Sequence</summary>
        public readonly static DicomTagSQ AcousticCouplingMediumCodeSequence = new DicomTagSQ(0x0018, 0x982A);

        ///<summary>(0018,982B) VR=FD VM=1 Acoustic Coupling Medium Temperature</summary>
        public readonly static DicomTagFD AcousticCouplingMediumTemperature = new DicomTagFD(0x0018, 0x982B);

        ///<summary>(0018,982C) VR=SQ VM=1 Transducer Response Sequence</summary>
        public readonly static DicomTagSQ TransducerResponseSequence = new DicomTagSQ(0x0018, 0x982C);

        ///<summary>(0018,982D) VR=FD VM=1 Center Frequency</summary>
        public readonly static DicomTagFD CenterFrequency = new DicomTagFD(0x0018, 0x982D);

        ///<summary>(0018,982E) VR=FD VM=1 Fractional Bandwidth</summary>
        public readonly static DicomTagFD FractionalBandwidth = new DicomTagFD(0x0018, 0x982E);

        ///<summary>(0018,982F) VR=FD VM=1 Lower Cutoff Frequency</summary>
        public readonly static DicomTagFD LowerCutoffFrequency = new DicomTagFD(0x0018, 0x982F);

        ///<summary>(0018,9830) VR=FD VM=1 Upper Cutoff Frequency</summary>
        public readonly static DicomTagFD UpperCutoffFrequency = new DicomTagFD(0x0018, 0x9830);

        ///<summary>(0018,9831) VR=SQ VM=1 Transducer Technology Sequence</summary>
        public readonly static DicomTagSQ TransducerTechnologySequence = new DicomTagSQ(0x0018, 0x9831);

        ///<summary>(0018,9832) VR=SQ VM=1 Sound Speed Correction Mechanism Code Sequence</summary>
        public readonly static DicomTagSQ SoundSpeedCorrectionMechanismCodeSequence = new DicomTagSQ(0x0018, 0x9832);

        ///<summary>(0018,9833) VR=FD VM=1 Object Sound Speed</summary>
        public readonly static DicomTagFD ObjectSoundSpeed = new DicomTagFD(0x0018, 0x9833);

        ///<summary>(0018,9834) VR=FD VM=1 Acoustic Coupling Medium Sound Speed</summary>
        public readonly static DicomTagFD AcousticCouplingMediumSoundSpeed = new DicomTagFD(0x0018, 0x9834);

        ///<summary>(0018,9835) VR=SQ VM=1 Photoacoustic Image Frame Type Sequence</summary>
        public readonly static DicomTagSQ PhotoacousticImageFrameTypeSequence = new DicomTagSQ(0x0018, 0x9835);

        ///<summary>(0018,9836) VR=SQ VM=1 Image Data Type Code Sequence</summary>
        public readonly static DicomTagSQ ImageDataTypeCodeSequence = new DicomTagSQ(0x0018, 0x9836);

        ///<summary>(0018,9900) VR=LO VM=1 Reference Location Label</summary>
        public readonly static DicomTagLO ReferenceLocationLabel = new DicomTagLO(0x0018, 0x9900);

        ///<summary>(0018,9901) VR=UT VM=1 Reference Location Description</summary>
        public readonly static DicomTagUT ReferenceLocationDescription = new DicomTagUT(0x0018, 0x9901);

        ///<summary>(0018,9902) VR=SQ VM=1 Reference Basis Code Sequence</summary>
        public readonly static DicomTagSQ ReferenceBasisCodeSequence = new DicomTagSQ(0x0018, 0x9902);

        ///<summary>(0018,9903) VR=SQ VM=1 Reference Geometry Code Sequence</summary>
        public readonly static DicomTagSQ ReferenceGeometryCodeSequence = new DicomTagSQ(0x0018, 0x9903);

        ///<summary>(0018,9904) VR=DS VM=1 Offset Distance</summary>
        public readonly static DicomTagDS OffsetDistance = new DicomTagDS(0x0018, 0x9904);

        ///<summary>(0018,9905) VR=CS VM=1 Offset Direction</summary>
        public readonly static DicomTagCS OffsetDirection = new DicomTagCS(0x0018, 0x9905);

        ///<summary>(0018,9906) VR=SQ VM=1 Potential Scheduled Protocol Code Sequence</summary>
        public readonly static DicomTagSQ PotentialScheduledProtocolCodeSequence = new DicomTagSQ(0x0018, 0x9906);

        ///<summary>(0018,9907) VR=SQ VM=1 Potential Requested Procedure Code Sequence</summary>
        public readonly static DicomTagSQ PotentialRequestedProcedureCodeSequence = new DicomTagSQ(0x0018, 0x9907);

        ///<summary>(0018,9908) VR=UC VM=1-n Potential Reasons for Procedure</summary>
        public readonly static DicomTagUCs PotentialReasonsForProcedure = new DicomTagUCs(0x0018, 0x9908);

        ///<summary>(0018,9909) VR=SQ VM=1 Potential Reasons for Procedure Code Sequence</summary>
        public readonly static DicomTagSQ PotentialReasonsForProcedureCodeSequence = new DicomTagSQ(0x0018, 0x9909);

        ///<summary>(0018,990A) VR=UC VM=1-n Potential Diagnostic Tasks</summary>
        public readonly static DicomTagUCs PotentialDiagnosticTasks = new DicomTagUCs(0x0018, 0x990A);

        ///<summary>(0018,990B) VR=SQ VM=1 Contraindications Code Sequence</summary>
        public readonly static DicomTagSQ ContraindicationsCodeSequence = new DicomTagSQ(0x0018, 0x990B);

        ///<summary>(0018,990C) VR=SQ VM=1 Referenced Defined Protocol Sequence</summary>
        public readonly static DicomTagSQ ReferencedDefinedProtocolSequence = new DicomTagSQ(0x0018, 0x990C);

        ///<summary>(0018,990D) VR=SQ VM=1 Referenced Performed Protocol Sequence</summary>
        public readonly static DicomTagSQ ReferencedPerformedProtocolSequence = new DicomTagSQ(0x0018, 0x990D);

        ///<summary>(0018,990E) VR=SQ VM=1 Predecessor Protocol Sequence</summary>
        public readonly static DicomTagSQ PredecessorProtocolSequence = new DicomTagSQ(0x0018, 0x990E);

        ///<summary>(0018,990F) VR=UT VM=1 Protocol Planning Information</summary>
        public readonly static DicomTagUT ProtocolPlanningInformation = new DicomTagUT(0x0018, 0x990F);

        ///<summary>(0018,9910) VR=UT VM=1 Protocol Design Rationale</summary>
        public readonly static DicomTagUT ProtocolDesignRationale = new DicomTagUT(0x0018, 0x9910);

        ///<summary>(0018,9911) VR=SQ VM=1 Patient Specification Sequence</summary>
        public readonly static DicomTagSQ PatientSpecificationSequence = new DicomTagSQ(0x0018, 0x9911);

        ///<summary>(0018,9912) VR=SQ VM=1 Model Specification Sequence</summary>
        public readonly static DicomTagSQ ModelSpecificationSequence = new DicomTagSQ(0x0018, 0x9912);

        ///<summary>(0018,9913) VR=SQ VM=1 Parameters Specification Sequence</summary>
        public readonly static DicomTagSQ ParametersSpecificationSequence = new DicomTagSQ(0x0018, 0x9913);

        ///<summary>(0018,9914) VR=SQ VM=1 Instruction Sequence</summary>
        public readonly static DicomTagSQ InstructionSequence = new DicomTagSQ(0x0018, 0x9914);

        ///<summary>(0018,9915) VR=US VM=1 Instruction Index</summary>
        public readonly static DicomTagUS InstructionIndex = new DicomTagUS(0x0018, 0x9915);

        ///<summary>(0018,9916) VR=LO VM=1 Instruction Text</summary>
        public readonly static DicomTagLO InstructionText = new DicomTagLO(0x0018, 0x9916);

        ///<summary>(0018,9917) VR=UT VM=1 Instruction Description</summary>
        public readonly static DicomTagUT InstructionDescription = new DicomTagUT(0x0018, 0x9917);

        ///<summary>(0018,9918) VR=CS VM=1 Instruction Performed Flag</summary>
        public readonly static DicomTagCS InstructionPerformedFlag = new DicomTagCS(0x0018, 0x9918);

        ///<summary>(0018,9919) VR=DT VM=1 Instruction Performed DateTime</summary>
        public readonly static DicomTagDT InstructionPerformedDateTime = new DicomTagDT(0x0018, 0x9919);

        ///<summary>(0018,991A) VR=UT VM=1 Instruction Performance Comment</summary>
        public readonly static DicomTagUT InstructionPerformanceComment = new DicomTagUT(0x0018, 0x991A);

        ///<summary>(0018,991B) VR=SQ VM=1 Patient Positioning Instruction Sequence</summary>
        public readonly static DicomTagSQ PatientPositioningInstructionSequence = new DicomTagSQ(0x0018, 0x991B);

        ///<summary>(0018,991C) VR=SQ VM=1 Positioning Method Code Sequence</summary>
        public readonly static DicomTagSQ PositioningMethodCodeSequence = new DicomTagSQ(0x0018, 0x991C);

        ///<summary>(0018,991D) VR=SQ VM=1 Positioning Landmark Sequence</summary>
        public readonly static DicomTagSQ PositioningLandmarkSequence = new DicomTagSQ(0x0018, 0x991D);

        ///<summary>(0018,991E) VR=UI VM=1 Target Frame of Reference UID</summary>
        public readonly static DicomTagUI TargetFrameOfReferenceUID = new DicomTagUI(0x0018, 0x991E);

        ///<summary>(0018,991F) VR=SQ VM=1 Acquisition Protocol Element Specification Sequence</summary>
        public readonly static DicomTagSQ AcquisitionProtocolElementSpecificationSequence = new DicomTagSQ(0x0018, 0x991F);

        ///<summary>(0018,9920) VR=SQ VM=1 Acquisition Protocol Element Sequence</summary>
        public readonly static DicomTagSQ AcquisitionProtocolElementSequence = new DicomTagSQ(0x0018, 0x9920);

        ///<summary>(0018,9921) VR=US VM=1 Protocol Element Number</summary>
        public readonly static DicomTagUS ProtocolElementNumber = new DicomTagUS(0x0018, 0x9921);

        ///<summary>(0018,9922) VR=LO VM=1 Protocol Element Name</summary>
        public readonly static DicomTagLO ProtocolElementName = new DicomTagLO(0x0018, 0x9922);

        ///<summary>(0018,9923) VR=UT VM=1 Protocol Element Characteristics Summary</summary>
        public readonly static DicomTagUT ProtocolElementCharacteristicsSummary = new DicomTagUT(0x0018, 0x9923);

        ///<summary>(0018,9924) VR=UT VM=1 Protocol Element Purpose</summary>
        public readonly static DicomTagUT ProtocolElementPurpose = new DicomTagUT(0x0018, 0x9924);

        ///<summary>(0018,9930) VR=CS VM=1 Acquisition Motion</summary>
        public readonly static DicomTagCS AcquisitionMotion = new DicomTagCS(0x0018, 0x9930);

        ///<summary>(0018,9931) VR=SQ VM=1 Acquisition Start Location Sequence</summary>
        public readonly static DicomTagSQ AcquisitionStartLocationSequence = new DicomTagSQ(0x0018, 0x9931);

        ///<summary>(0018,9932) VR=SQ VM=1 Acquisition End Location Sequence</summary>
        public readonly static DicomTagSQ AcquisitionEndLocationSequence = new DicomTagSQ(0x0018, 0x9932);

        ///<summary>(0018,9933) VR=SQ VM=1 Reconstruction Protocol Element Specification Sequence</summary>
        public readonly static DicomTagSQ ReconstructionProtocolElementSpecificationSequence = new DicomTagSQ(0x0018, 0x9933);

        ///<summary>(0018,9934) VR=SQ VM=1 Reconstruction Protocol Element Sequence</summary>
        public readonly static DicomTagSQ ReconstructionProtocolElementSequence = new DicomTagSQ(0x0018, 0x9934);

        ///<summary>(0018,9935) VR=SQ VM=1 Storage Protocol Element Specification Sequence</summary>
        public readonly static DicomTagSQ StorageProtocolElementSpecificationSequence = new DicomTagSQ(0x0018, 0x9935);

        ///<summary>(0018,9936) VR=SQ VM=1 Storage Protocol Element Sequence</summary>
        public readonly static DicomTagSQ StorageProtocolElementSequence = new DicomTagSQ(0x0018, 0x9936);

        ///<summary>(0018,9937) VR=LO VM=1 Requested Series Description</summary>
        public readonly static DicomTagLO RequestedSeriesDescription = new DicomTagLO(0x0018, 0x9937);

        ///<summary>(0018,9938) VR=US VM=1-n Source Acquisition Protocol Element Number</summary>
        public readonly static DicomTagUSs SourceAcquisitionProtocolElementNumber = new DicomTagUSs(0x0018, 0x9938);

        ///<summary>(0018,9939) VR=US VM=1-n Source Acquisition Beam Number</summary>
        public readonly static DicomTagUSs SourceAcquisitionBeamNumber = new DicomTagUSs(0x0018, 0x9939);

        ///<summary>(0018,993A) VR=US VM=1-n Source Reconstruction Protocol Element Number</summary>
        public readonly static DicomTagUSs SourceReconstructionProtocolElementNumber = new DicomTagUSs(0x0018, 0x993A);

        ///<summary>(0018,993B) VR=SQ VM=1 Reconstruction Start Location Sequence</summary>
        public readonly static DicomTagSQ ReconstructionStartLocationSequence = new DicomTagSQ(0x0018, 0x993B);

        ///<summary>(0018,993C) VR=SQ VM=1 Reconstruction End Location Sequence</summary>
        public readonly static DicomTagSQ ReconstructionEndLocationSequence = new DicomTagSQ(0x0018, 0x993C);

        ///<summary>(0018,993D) VR=SQ VM=1 Reconstruction Algorithm Sequence</summary>
        public readonly static DicomTagSQ ReconstructionAlgorithmSequence = new DicomTagSQ(0x0018, 0x993D);

        ///<summary>(0018,993E) VR=SQ VM=1 Reconstruction Target Center Location Sequence</summary>
        public readonly static DicomTagSQ ReconstructionTargetCenterLocationSequence = new DicomTagSQ(0x0018, 0x993E);

        ///<summary>(0018,9941) VR=UT VM=1 Image Filter Description</summary>
        public readonly static DicomTagUT ImageFilterDescription = new DicomTagUT(0x0018, 0x9941);

        ///<summary>(0018,9942) VR=FD VM=1 CTDIvol Notification Trigger</summary>
        public readonly static DicomTagFD CTDIvolNotificationTrigger = new DicomTagFD(0x0018, 0x9942);

        ///<summary>(0018,9943) VR=FD VM=1 DLP Notification Trigger</summary>
        public readonly static DicomTagFD DLPNotificationTrigger = new DicomTagFD(0x0018, 0x9943);

        ///<summary>(0018,9944) VR=CS VM=1 Auto KVP Selection Type</summary>
        public readonly static DicomTagCS AutoKVPSelectionType = new DicomTagCS(0x0018, 0x9944);

        ///<summary>(0018,9945) VR=FD VM=1 Auto KVP Upper Bound</summary>
        public readonly static DicomTagFD AutoKVPUpperBound = new DicomTagFD(0x0018, 0x9945);

        ///<summary>(0018,9946) VR=FD VM=1 Auto KVP Lower Bound</summary>
        public readonly static DicomTagFD AutoKVPLowerBound = new DicomTagFD(0x0018, 0x9946);

        ///<summary>(0018,9947) VR=CS VM=1 Protocol Defined Patient Position</summary>
        public readonly static DicomTagCS ProtocolDefinedPatientPosition = new DicomTagCS(0x0018, 0x9947);

        ///<summary>(0018,A001) VR=SQ VM=1 Contributing Equipment Sequence</summary>
        public readonly static DicomTagSQ ContributingEquipmentSequence = new DicomTagSQ(0x0018, 0xA001);

        ///<summary>(0018,A002) VR=DT VM=1 Contribution DateTime</summary>
        public readonly static DicomTagDT ContributionDateTime = new DicomTagDT(0x0018, 0xA002);

        ///<summary>(0018,A003) VR=ST VM=1 Contribution Description</summary>
        public readonly static DicomTagST ContributionDescription = new DicomTagST(0x0018, 0xA003);

        ///<summary>(0020,000D) VR=UI VM=1 Study Instance UID</summary>
        public readonly static DicomTagUI StudyInstanceUID = new DicomTagUI(0x0020, 0x000D);

        ///<summary>(0020,000E) VR=UI VM=1 Series Instance UID</summary>
        public readonly static DicomTagUI SeriesInstanceUID = new DicomTagUI(0x0020, 0x000E);

        ///<summary>(0020,0010) VR=SH VM=1 Study ID</summary>
        public readonly static DicomTagSH StudyID = new DicomTagSH(0x0020, 0x0010);

        ///<summary>(0020,0011) VR=IS VM=1 Series Number</summary>
        public readonly static DicomTagIS SeriesNumber = new DicomTagIS(0x0020, 0x0011);

        ///<summary>(0020,0012) VR=IS VM=1 Acquisition Number</summary>
        public readonly static DicomTagIS AcquisitionNumber = new DicomTagIS(0x0020, 0x0012);

        ///<summary>(0020,0013) VR=IS VM=1 Instance Number</summary>
        public readonly static DicomTagIS InstanceNumber = new DicomTagIS(0x0020, 0x0013);

        ///<summary>(0020,0014) VR=IS VM=1 Isotope Number (RETIRED)</summary>
        public readonly static DicomTagIS IsotopeNumberRETIRED = new DicomTagIS(0x0020, 0x0014);

        ///<summary>(0020,0015) VR=IS VM=1 Phase Number (RETIRED)</summary>
        public readonly static DicomTagIS PhaseNumberRETIRED = new DicomTagIS(0x0020, 0x0015);

        ///<summary>(0020,0016) VR=IS VM=1 Interval Number (RETIRED)</summary>
        public readonly static DicomTagIS IntervalNumberRETIRED = new DicomTagIS(0x0020, 0x0016);

        ///<summary>(0020,0017) VR=IS VM=1 Time Slot Number (RETIRED)</summary>
        public readonly static DicomTagIS TimeSlotNumberRETIRED = new DicomTagIS(0x0020, 0x0017);

        ///<summary>(0020,0018) VR=IS VM=1 Angle Number (RETIRED)</summary>
        public readonly static DicomTagIS AngleNumberRETIRED = new DicomTagIS(0x0020, 0x0018);

        ///<summary>(0020,0019) VR=IS VM=1 Item Number</summary>
        public readonly static DicomTagIS ItemNumber = new DicomTagIS(0x0020, 0x0019);

        ///<summary>(0020,0020) VR=CS VM=2 Patient Orientation</summary>
        public readonly static DicomTagCSs PatientOrientation = new DicomTagCSs(0x0020, 0x0020);

        ///<summary>(0020,0022) VR=IS VM=1 Overlay Number (RETIRED)</summary>
        public readonly static DicomTagIS OverlayNumberRETIRED = new DicomTagIS(0x0020, 0x0022);

        ///<summary>(0020,0024) VR=IS VM=1 Curve Number (RETIRED)</summary>
        public readonly static DicomTagIS CurveNumberRETIRED = new DicomTagIS(0x0020, 0x0024);

        ///<summary>(0020,0026) VR=IS VM=1 LUT Number (RETIRED)</summary>
        public readonly static DicomTagIS LUTNumberRETIRED = new DicomTagIS(0x0020, 0x0026);

        ///<summary>(0020,0027) VR=LO VM=1 Pyramid Label</summary>
        public readonly static DicomTagLO PyramidLabel = new DicomTagLO(0x0020, 0x0027);

        ///<summary>(0020,0030) VR=DS VM=3 Image Position (RETIRED)</summary>
        public readonly static DicomTagDSs ImagePositionRETIRED = new DicomTagDSs(0x0020, 0x0030);

        ///<summary>(0020,0032) VR=DS VM=3 Image Position (Patient)</summary>
        public readonly static DicomTagDSs ImagePositionPatient = new DicomTagDSs(0x0020, 0x0032);

        ///<summary>(0020,0035) VR=DS VM=6 Image Orientation (RETIRED)</summary>
        public readonly static DicomTagDSs ImageOrientationRETIRED = new DicomTagDSs(0x0020, 0x0035);

        ///<summary>(0020,0037) VR=DS VM=6 Image Orientation (Patient)</summary>
        public readonly static DicomTagDSs ImageOrientationPatient = new DicomTagDSs(0x0020, 0x0037);

        ///<summary>(0020,0050) VR=DS VM=1 Location (RETIRED)</summary>
        public readonly static DicomTagDS LocationRETIRED = new DicomTagDS(0x0020, 0x0050);

        ///<summary>(0020,0052) VR=UI VM=1 Frame of Reference UID</summary>
        public readonly static DicomTagUI FrameOfReferenceUID = new DicomTagUI(0x0020, 0x0052);

        ///<summary>(0020,0060) VR=CS VM=1 Laterality</summary>
        public readonly static DicomTagCS Laterality = new DicomTagCS(0x0020, 0x0060);

        ///<summary>(0020,0062) VR=CS VM=1 Image Laterality</summary>
        public readonly static DicomTagCS ImageLaterality = new DicomTagCS(0x0020, 0x0062);

        ///<summary>(0020,0070) VR=LO VM=1 Image Geometry Type (RETIRED)</summary>
        public readonly static DicomTagLO ImageGeometryTypeRETIRED = new DicomTagLO(0x0020, 0x0070);

        ///<summary>(0020,0080) VR=CS VM=1-n Masking Image (RETIRED)</summary>
        public readonly static DicomTagCSs MaskingImageRETIRED = new DicomTagCSs(0x0020, 0x0080);

        ///<summary>(0020,00AA) VR=IS VM=1 Report Number (RETIRED)</summary>
        public readonly static DicomTagIS ReportNumberRETIRED = new DicomTagIS(0x0020, 0x00AA);

        ///<summary>(0020,0100) VR=IS VM=1 Temporal Position Identifier</summary>
        public readonly static DicomTagIS TemporalPositionIdentifier = new DicomTagIS(0x0020, 0x0100);

        ///<summary>(0020,0105) VR=IS VM=1 Number of Temporal Positions</summary>
        public readonly static DicomTagIS NumberOfTemporalPositions = new DicomTagIS(0x0020, 0x0105);

        ///<summary>(0020,0110) VR=DS VM=1 Temporal Resolution</summary>
        public readonly static DicomTagDS TemporalResolution = new DicomTagDS(0x0020, 0x0110);

        ///<summary>(0020,0200) VR=UI VM=1 Synchronization Frame of Reference UID</summary>
        public readonly static DicomTagUI SynchronizationFrameOfReferenceUID = new DicomTagUI(0x0020, 0x0200);

        ///<summary>(0020,0242) VR=UI VM=1 SOP Instance UID of Concatenation Source</summary>
        public readonly static DicomTagUI SOPInstanceUIDOfConcatenationSource = new DicomTagUI(0x0020, 0x0242);

        ///<summary>(0020,1000) VR=IS VM=1 Series in Study (RETIRED)</summary>
        public readonly static DicomTagIS SeriesInStudyRETIRED = new DicomTagIS(0x0020, 0x1000);

        ///<summary>(0020,1001) VR=IS VM=1 Acquisitions in Series (RETIRED)</summary>
        public readonly static DicomTagIS AcquisitionsInSeriesRETIRED = new DicomTagIS(0x0020, 0x1001);

        ///<summary>(0020,1002) VR=IS VM=1 Images in Acquisition</summary>
        public readonly static DicomTagIS ImagesInAcquisition = new DicomTagIS(0x0020, 0x1002);

        ///<summary>(0020,1003) VR=IS VM=1 Images in Series (RETIRED)</summary>
        public readonly static DicomTagIS ImagesInSeriesRETIRED = new DicomTagIS(0x0020, 0x1003);

        ///<summary>(0020,1004) VR=IS VM=1 Acquisitions in Study (RETIRED)</summary>
        public readonly static DicomTagIS AcquisitionsInStudyRETIRED = new DicomTagIS(0x0020, 0x1004);

        ///<summary>(0020,1005) VR=IS VM=1 Images in Study (RETIRED)</summary>
        public readonly static DicomTagIS ImagesInStudyRETIRED = new DicomTagIS(0x0020, 0x1005);

        ///<summary>(0020,1020) VR=LO VM=1-n Reference (RETIRED)</summary>
        public readonly static DicomTagLOs ReferenceRETIRED = new DicomTagLOs(0x0020, 0x1020);

        ///<summary>(0020,103F) VR=LO VM=1 Target Position Reference Indicator</summary>
        public readonly static DicomTagLO TargetPositionReferenceIndicator = new DicomTagLO(0x0020, 0x103F);

        ///<summary>(0020,1040) VR=LO VM=1 Position Reference Indicator</summary>
        public readonly static DicomTagLO PositionReferenceIndicator = new DicomTagLO(0x0020, 0x1040);

        ///<summary>(0020,1041) VR=DS VM=1 Slice Location</summary>
        public readonly static DicomTagDS SliceLocation = new DicomTagDS(0x0020, 0x1041);

        ///<summary>(0020,1070) VR=IS VM=1-n Other Study Numbers (RETIRED)</summary>
        public readonly static DicomTagISs OtherStudyNumbersRETIRED = new DicomTagISs(0x0020, 0x1070);

        ///<summary>(0020,1200) VR=IS VM=1 Number of Patient Related Studies</summary>
        public readonly static DicomTagIS NumberOfPatientRelatedStudies = new DicomTagIS(0x0020, 0x1200);

        ///<summary>(0020,1202) VR=IS VM=1 Number of Patient Related Series</summary>
        public readonly static DicomTagIS NumberOfPatientRelatedSeries = new DicomTagIS(0x0020, 0x1202);

        ///<summary>(0020,1204) VR=IS VM=1 Number of Patient Related Instances</summary>
        public readonly static DicomTagIS NumberOfPatientRelatedInstances = new DicomTagIS(0x0020, 0x1204);

        ///<summary>(0020,1206) VR=IS VM=1 Number of Study Related Series</summary>
        public readonly static DicomTagIS NumberOfStudyRelatedSeries = new DicomTagIS(0x0020, 0x1206);

        ///<summary>(0020,1208) VR=IS VM=1 Number of Study Related Instances</summary>
        public readonly static DicomTagIS NumberOfStudyRelatedInstances = new DicomTagIS(0x0020, 0x1208);

        ///<summary>(0020,1209) VR=IS VM=1 Number of Series Related Instances</summary>
        public readonly static DicomTagIS NumberOfSeriesRelatedInstances = new DicomTagIS(0x0020, 0x1209);

        ///<summary>(0020,31xx) VR=CS VM=1-n Source Image IDs (RETIRED)</summary>
        public readonly static DicomTagCSs SourceImageIDsRETIRED = new DicomTagCSs(0x0020, 0x3100);

        ///<summary>(0020,3401) VR=CS VM=1 Modifying Device ID (RETIRED)</summary>
        public readonly static DicomTagCS ModifyingDeviceIDRETIRED = new DicomTagCS(0x0020, 0x3401);

        ///<summary>(0020,3402) VR=CS VM=1 Modified Image ID (RETIRED)</summary>
        public readonly static DicomTagCS ModifiedImageIDRETIRED = new DicomTagCS(0x0020, 0x3402);

        ///<summary>(0020,3403) VR=DA VM=1 Modified Image Date (RETIRED)</summary>
        public readonly static DicomTagDA ModifiedImageDateRETIRED = new DicomTagDA(0x0020, 0x3403);

        ///<summary>(0020,3404) VR=LO VM=1 Modifying Device Manufacturer (RETIRED)</summary>
        public readonly static DicomTagLO ModifyingDeviceManufacturerRETIRED = new DicomTagLO(0x0020, 0x3404);

        ///<summary>(0020,3405) VR=TM VM=1 Modified Image Time (RETIRED)</summary>
        public readonly static DicomTagTM ModifiedImageTimeRETIRED = new DicomTagTM(0x0020, 0x3405);

        ///<summary>(0020,3406) VR=LO VM=1 Modified Image Description (RETIRED)</summary>
        public readonly static DicomTagLO ModifiedImageDescriptionRETIRED = new DicomTagLO(0x0020, 0x3406);

        ///<summary>(0020,4000) VR=LT VM=1 Image Comments</summary>
        public readonly static DicomTagLT ImageComments = new DicomTagLT(0x0020, 0x4000);

        ///<summary>(0020,5000) VR=AT VM=1-n Original Image Identification (RETIRED)</summary>
        public readonly static DicomTagATs OriginalImageIdentificationRETIRED = new DicomTagATs(0x0020, 0x5000);

        ///<summary>(0020,5002) VR=LO VM=1-n Original Image Identification Nomenclature (RETIRED)</summary>
        public readonly static DicomTagLOs OriginalImageIdentificationNomenclatureRETIRED = new DicomTagLOs(0x0020, 0x5002);

        ///<summary>(0020,9056) VR=SH VM=1 Stack ID</summary>
        public readonly static DicomTagSH StackID = new DicomTagSH(0x0020, 0x9056);

        ///<summary>(0020,9057) VR=UL VM=1 In-Stack Position Number</summary>
        public readonly static DicomTagUL InStackPositionNumber = new DicomTagUL(0x0020, 0x9057);

        ///<summary>(0020,9071) VR=SQ VM=1 Frame Anatomy Sequence</summary>
        public readonly static DicomTagSQ FrameAnatomySequence = new DicomTagSQ(0x0020, 0x9071);

        ///<summary>(0020,9072) VR=CS VM=1 Frame Laterality</summary>
        public readonly static DicomTagCS FrameLaterality = new DicomTagCS(0x0020, 0x9072);

        ///<summary>(0020,9111) VR=SQ VM=1 Frame Content Sequence</summary>
        public readonly static DicomTagSQ FrameContentSequence = new DicomTagSQ(0x0020, 0x9111);

        ///<summary>(0020,9113) VR=SQ VM=1 Plane Position Sequence</summary>
        public readonly static DicomTagSQ PlanePositionSequence = new DicomTagSQ(0x0020, 0x9113);

        ///<summary>(0020,9116) VR=SQ VM=1 Plane Orientation Sequence</summary>
        public readonly static DicomTagSQ PlaneOrientationSequence = new DicomTagSQ(0x0020, 0x9116);

        ///<summary>(0020,9128) VR=UL VM=1 Temporal Position Index</summary>
        public readonly static DicomTagUL TemporalPositionIndex = new DicomTagUL(0x0020, 0x9128);

        ///<summary>(0020,9153) VR=FD VM=1 Nominal Cardiac Trigger Delay Time</summary>
        public readonly static DicomTagFD NominalCardiacTriggerDelayTime = new DicomTagFD(0x0020, 0x9153);

        ///<summary>(0020,9154) VR=FL VM=1 Nominal Cardiac Trigger Time Prior To R-Peak</summary>
        public readonly static DicomTagFL NominalCardiacTriggerTimePriorToRPeak = new DicomTagFL(0x0020, 0x9154);

        ///<summary>(0020,9155) VR=FL VM=1 Actual Cardiac Trigger Time Prior To R-Peak</summary>
        public readonly static DicomTagFL ActualCardiacTriggerTimePriorToRPeak = new DicomTagFL(0x0020, 0x9155);

        ///<summary>(0020,9156) VR=US VM=1 Frame Acquisition Number</summary>
        public readonly static DicomTagUS FrameAcquisitionNumber = new DicomTagUS(0x0020, 0x9156);

        ///<summary>(0020,9157) VR=UL VM=1-n Dimension Index Values</summary>
        public readonly static DicomTagULs DimensionIndexValues = new DicomTagULs(0x0020, 0x9157);

        ///<summary>(0020,9158) VR=LT VM=1 Frame Comments</summary>
        public readonly static DicomTagLT FrameComments = new DicomTagLT(0x0020, 0x9158);

        ///<summary>(0020,9161) VR=UI VM=1 Concatenation UID</summary>
        public readonly static DicomTagUI ConcatenationUID = new DicomTagUI(0x0020, 0x9161);

        ///<summary>(0020,9162) VR=US VM=1 In-concatenation Number</summary>
        public readonly static DicomTagUS InConcatenationNumber = new DicomTagUS(0x0020, 0x9162);

        ///<summary>(0020,9163) VR=US VM=1 In-concatenation Total Number</summary>
        public readonly static DicomTagUS InConcatenationTotalNumber = new DicomTagUS(0x0020, 0x9163);

        ///<summary>(0020,9164) VR=UI VM=1 Dimension Organization UID</summary>
        public readonly static DicomTagUI DimensionOrganizationUID = new DicomTagUI(0x0020, 0x9164);

        ///<summary>(0020,9165) VR=AT VM=1 Dimension Index Pointer</summary>
        public readonly static DicomTagAT DimensionIndexPointer = new DicomTagAT(0x0020, 0x9165);

        ///<summary>(0020,9167) VR=AT VM=1 Functional Group Pointer</summary>
        public readonly static DicomTagAT FunctionalGroupPointer = new DicomTagAT(0x0020, 0x9167);

        ///<summary>(0020,9170) VR=SQ VM=1 Unassigned Shared Converted Attributes Sequence</summary>
        public readonly static DicomTagSQ UnassignedSharedConvertedAttributesSequence = new DicomTagSQ(0x0020, 0x9170);

        ///<summary>(0020,9171) VR=SQ VM=1 Unassigned Per-Frame Converted Attributes Sequence</summary>
        public readonly static DicomTagSQ UnassignedPerFrameConvertedAttributesSequence = new DicomTagSQ(0x0020, 0x9171);

        ///<summary>(0020,9172) VR=SQ VM=1 Conversion Source Attributes Sequence</summary>
        public readonly static DicomTagSQ ConversionSourceAttributesSequence = new DicomTagSQ(0x0020, 0x9172);

        ///<summary>(0020,9213) VR=LO VM=1 Dimension Index Private Creator</summary>
        public readonly static DicomTagLO DimensionIndexPrivateCreator = new DicomTagLO(0x0020, 0x9213);

        ///<summary>(0020,9221) VR=SQ VM=1 Dimension Organization Sequence</summary>
        public readonly static DicomTagSQ DimensionOrganizationSequence = new DicomTagSQ(0x0020, 0x9221);

        ///<summary>(0020,9222) VR=SQ VM=1 Dimension Index Sequence</summary>
        public readonly static DicomTagSQ DimensionIndexSequence = new DicomTagSQ(0x0020, 0x9222);

        ///<summary>(0020,9228) VR=UL VM=1 Concatenation Frame Offset Number</summary>
        public readonly static DicomTagUL ConcatenationFrameOffsetNumber = new DicomTagUL(0x0020, 0x9228);

        ///<summary>(0020,9238) VR=LO VM=1 Functional Group Private Creator</summary>
        public readonly static DicomTagLO FunctionalGroupPrivateCreator = new DicomTagLO(0x0020, 0x9238);

        ///<summary>(0020,9241) VR=FL VM=1 Nominal Percentage of Cardiac Phase</summary>
        public readonly static DicomTagFL NominalPercentageOfCardiacPhase = new DicomTagFL(0x0020, 0x9241);

        ///<summary>(0020,9245) VR=FL VM=1 Nominal Percentage of Respiratory Phase</summary>
        public readonly static DicomTagFL NominalPercentageOfRespiratoryPhase = new DicomTagFL(0x0020, 0x9245);

        ///<summary>(0020,9246) VR=FL VM=1 Starting Respiratory Amplitude</summary>
        public readonly static DicomTagFL StartingRespiratoryAmplitude = new DicomTagFL(0x0020, 0x9246);

        ///<summary>(0020,9247) VR=CS VM=1 Starting Respiratory Phase</summary>
        public readonly static DicomTagCS StartingRespiratoryPhase = new DicomTagCS(0x0020, 0x9247);

        ///<summary>(0020,9248) VR=FL VM=1 Ending Respiratory Amplitude</summary>
        public readonly static DicomTagFL EndingRespiratoryAmplitude = new DicomTagFL(0x0020, 0x9248);

        ///<summary>(0020,9249) VR=CS VM=1 Ending Respiratory Phase</summary>
        public readonly static DicomTagCS EndingRespiratoryPhase = new DicomTagCS(0x0020, 0x9249);

        ///<summary>(0020,9250) VR=CS VM=1 Respiratory Trigger Type</summary>
        public readonly static DicomTagCS RespiratoryTriggerType = new DicomTagCS(0x0020, 0x9250);

        ///<summary>(0020,9251) VR=FD VM=1 R-R Interval Time Nominal</summary>
        public readonly static DicomTagFD RRIntervalTimeNominal = new DicomTagFD(0x0020, 0x9251);

        ///<summary>(0020,9252) VR=FD VM=1 Actual Cardiac Trigger Delay Time</summary>
        public readonly static DicomTagFD ActualCardiacTriggerDelayTime = new DicomTagFD(0x0020, 0x9252);

        ///<summary>(0020,9253) VR=SQ VM=1 Respiratory Synchronization Sequence</summary>
        public readonly static DicomTagSQ RespiratorySynchronizationSequence = new DicomTagSQ(0x0020, 0x9253);

        ///<summary>(0020,9254) VR=FD VM=1 Respiratory Interval Time</summary>
        public readonly static DicomTagFD RespiratoryIntervalTime = new DicomTagFD(0x0020, 0x9254);

        ///<summary>(0020,9255) VR=FD VM=1 Nominal Respiratory Trigger Delay Time</summary>
        public readonly static DicomTagFD NominalRespiratoryTriggerDelayTime = new DicomTagFD(0x0020, 0x9255);

        ///<summary>(0020,9256) VR=FD VM=1 Respiratory Trigger Delay Threshold</summary>
        public readonly static DicomTagFD RespiratoryTriggerDelayThreshold = new DicomTagFD(0x0020, 0x9256);

        ///<summary>(0020,9257) VR=FD VM=1 Actual Respiratory Trigger Delay Time</summary>
        public readonly static DicomTagFD ActualRespiratoryTriggerDelayTime = new DicomTagFD(0x0020, 0x9257);

        ///<summary>(0020,9301) VR=FD VM=3 Image Position (Volume)</summary>
        public readonly static DicomTagFDs ImagePositionVolume = new DicomTagFDs(0x0020, 0x9301);

        ///<summary>(0020,9302) VR=FD VM=6 Image Orientation (Volume)</summary>
        public readonly static DicomTagFDs ImageOrientationVolume = new DicomTagFDs(0x0020, 0x9302);

        ///<summary>(0020,9307) VR=CS VM=1 Ultrasound Acquisition Geometry</summary>
        public readonly static DicomTagCS UltrasoundAcquisitionGeometry = new DicomTagCS(0x0020, 0x9307);

        ///<summary>(0020,9308) VR=FD VM=3 Apex Position</summary>
        public readonly static DicomTagFDs ApexPosition = new DicomTagFDs(0x0020, 0x9308);

        ///<summary>(0020,9309) VR=FD VM=16 Volume to Transducer Mapping Matrix</summary>
        public readonly static DicomTagFDs VolumeToTransducerMappingMatrix = new DicomTagFDs(0x0020, 0x9309);

        ///<summary>(0020,930A) VR=FD VM=16 Volume to Table Mapping Matrix</summary>
        public readonly static DicomTagFDs VolumeToTableMappingMatrix = new DicomTagFDs(0x0020, 0x930A);

        ///<summary>(0020,930B) VR=CS VM=1 Volume to Transducer Relationship</summary>
        public readonly static DicomTagCS VolumeToTransducerRelationship = new DicomTagCS(0x0020, 0x930B);

        ///<summary>(0020,930C) VR=CS VM=1 Patient Frame of Reference Source</summary>
        public readonly static DicomTagCS PatientFrameOfReferenceSource = new DicomTagCS(0x0020, 0x930C);

        ///<summary>(0020,930D) VR=FD VM=1 Temporal Position Time Offset</summary>
        public readonly static DicomTagFD TemporalPositionTimeOffset = new DicomTagFD(0x0020, 0x930D);

        ///<summary>(0020,930E) VR=SQ VM=1 Plane Position (Volume) Sequence</summary>
        public readonly static DicomTagSQ PlanePositionVolumeSequence = new DicomTagSQ(0x0020, 0x930E);

        ///<summary>(0020,930F) VR=SQ VM=1 Plane Orientation (Volume) Sequence</summary>
        public readonly static DicomTagSQ PlaneOrientationVolumeSequence = new DicomTagSQ(0x0020, 0x930F);

        ///<summary>(0020,9310) VR=SQ VM=1 Temporal Position Sequence</summary>
        public readonly static DicomTagSQ TemporalPositionSequence = new DicomTagSQ(0x0020, 0x9310);

        ///<summary>(0020,9311) VR=CS VM=1 Dimension Organization Type</summary>
        public readonly static DicomTagCS DimensionOrganizationType = new DicomTagCS(0x0020, 0x9311);

        ///<summary>(0020,9312) VR=UI VM=1 Volume Frame of Reference UID</summary>
        public readonly static DicomTagUI VolumeFrameOfReferenceUID = new DicomTagUI(0x0020, 0x9312);

        ///<summary>(0020,9313) VR=UI VM=1 Table Frame of Reference UID</summary>
        public readonly static DicomTagUI TableFrameOfReferenceUID = new DicomTagUI(0x0020, 0x9313);

        ///<summary>(0020,9421) VR=LO VM=1 Dimension Description Label</summary>
        public readonly static DicomTagLO DimensionDescriptionLabel = new DicomTagLO(0x0020, 0x9421);

        ///<summary>(0020,9450) VR=SQ VM=1 Patient Orientation in Frame Sequence</summary>
        public readonly static DicomTagSQ PatientOrientationInFrameSequence = new DicomTagSQ(0x0020, 0x9450);

        ///<summary>(0020,9453) VR=LO VM=1 Frame Label</summary>
        public readonly static DicomTagLO FrameLabel = new DicomTagLO(0x0020, 0x9453);

        ///<summary>(0020,9518) VR=US VM=1-n Acquisition Index</summary>
        public readonly static DicomTagUSs AcquisitionIndex = new DicomTagUSs(0x0020, 0x9518);

        ///<summary>(0020,9529) VR=SQ VM=1 Contributing SOP Instances Reference Sequence</summary>
        public readonly static DicomTagSQ ContributingSOPInstancesReferenceSequence = new DicomTagSQ(0x0020, 0x9529);

        ///<summary>(0020,9536) VR=US VM=1 Reconstruction Index</summary>
        public readonly static DicomTagUS ReconstructionIndex = new DicomTagUS(0x0020, 0x9536);

        ///<summary>(0022,0001) VR=US VM=1 Light Path Filter Pass-Through Wavelength</summary>
        public readonly static DicomTagUS LightPathFilterPassThroughWavelength = new DicomTagUS(0x0022, 0x0001);

        ///<summary>(0022,0002) VR=US VM=2 Light Path Filter Pass Band</summary>
        public readonly static DicomTagUSs LightPathFilterPassBand = new DicomTagUSs(0x0022, 0x0002);

        ///<summary>(0022,0003) VR=US VM=1 Image Path Filter Pass-Through Wavelength</summary>
        public readonly static DicomTagUS ImagePathFilterPassThroughWavelength = new DicomTagUS(0x0022, 0x0003);

        ///<summary>(0022,0004) VR=US VM=2 Image Path Filter Pass Band</summary>
        public readonly static DicomTagUSs ImagePathFilterPassBand = new DicomTagUSs(0x0022, 0x0004);

        ///<summary>(0022,0005) VR=CS VM=1 Patient Eye Movement Commanded</summary>
        public readonly static DicomTagCS PatientEyeMovementCommanded = new DicomTagCS(0x0022, 0x0005);

        ///<summary>(0022,0006) VR=SQ VM=1 Patient Eye Movement Command Code Sequence</summary>
        public readonly static DicomTagSQ PatientEyeMovementCommandCodeSequence = new DicomTagSQ(0x0022, 0x0006);

        ///<summary>(0022,0007) VR=FL VM=1 Spherical Lens Power</summary>
        public readonly static DicomTagFL SphericalLensPower = new DicomTagFL(0x0022, 0x0007);

        ///<summary>(0022,0008) VR=FL VM=1 Cylinder Lens Power</summary>
        public readonly static DicomTagFL CylinderLensPower = new DicomTagFL(0x0022, 0x0008);

        ///<summary>(0022,0009) VR=FL VM=1 Cylinder Axis</summary>
        public readonly static DicomTagFL CylinderAxis = new DicomTagFL(0x0022, 0x0009);

        ///<summary>(0022,000A) VR=FL VM=1 Emmetropic Magnification</summary>
        public readonly static DicomTagFL EmmetropicMagnification = new DicomTagFL(0x0022, 0x000A);

        ///<summary>(0022,000B) VR=FL VM=1 Intra Ocular Pressure</summary>
        public readonly static DicomTagFL IntraOcularPressure = new DicomTagFL(0x0022, 0x000B);

        ///<summary>(0022,000C) VR=FL VM=1 Horizontal Field of View</summary>
        public readonly static DicomTagFL HorizontalFieldOfView = new DicomTagFL(0x0022, 0x000C);

        ///<summary>(0022,000D) VR=CS VM=1 Pupil Dilated</summary>
        public readonly static DicomTagCS PupilDilated = new DicomTagCS(0x0022, 0x000D);

        ///<summary>(0022,000E) VR=FL VM=1 Degree of Dilation</summary>
        public readonly static DicomTagFL DegreeOfDilation = new DicomTagFL(0x0022, 0x000E);

        ///<summary>(0022,000F) VR=FD VM=1 Vertex Distance</summary>
        public readonly static DicomTagFD VertexDistance = new DicomTagFD(0x0022, 0x000F);

        ///<summary>(0022,0010) VR=FL VM=1 Stereo Baseline Angle</summary>
        public readonly static DicomTagFL StereoBaselineAngle = new DicomTagFL(0x0022, 0x0010);

        ///<summary>(0022,0011) VR=FL VM=1 Stereo Baseline Displacement</summary>
        public readonly static DicomTagFL StereoBaselineDisplacement = new DicomTagFL(0x0022, 0x0011);

        ///<summary>(0022,0012) VR=FL VM=1 Stereo Horizontal Pixel Offset</summary>
        public readonly static DicomTagFL StereoHorizontalPixelOffset = new DicomTagFL(0x0022, 0x0012);

        ///<summary>(0022,0013) VR=FL VM=1 Stereo Vertical Pixel Offset</summary>
        public readonly static DicomTagFL StereoVerticalPixelOffset = new DicomTagFL(0x0022, 0x0013);

        ///<summary>(0022,0014) VR=FL VM=1 Stereo Rotation</summary>
        public readonly static DicomTagFL StereoRotation = new DicomTagFL(0x0022, 0x0014);

        ///<summary>(0022,0015) VR=SQ VM=1 Acquisition Device Type Code Sequence</summary>
        public readonly static DicomTagSQ AcquisitionDeviceTypeCodeSequence = new DicomTagSQ(0x0022, 0x0015);

        ///<summary>(0022,0016) VR=SQ VM=1 Illumination Type Code Sequence</summary>
        public readonly static DicomTagSQ IlluminationTypeCodeSequence = new DicomTagSQ(0x0022, 0x0016);

        ///<summary>(0022,0017) VR=SQ VM=1 Light Path Filter Type Stack Code Sequence</summary>
        public readonly static DicomTagSQ LightPathFilterTypeStackCodeSequence = new DicomTagSQ(0x0022, 0x0017);

        ///<summary>(0022,0018) VR=SQ VM=1 Image Path Filter Type Stack Code Sequence</summary>
        public readonly static DicomTagSQ ImagePathFilterTypeStackCodeSequence = new DicomTagSQ(0x0022, 0x0018);

        ///<summary>(0022,0019) VR=SQ VM=1 Lenses Code Sequence</summary>
        public readonly static DicomTagSQ LensesCodeSequence = new DicomTagSQ(0x0022, 0x0019);

        ///<summary>(0022,001A) VR=SQ VM=1 Channel Description Code Sequence</summary>
        public readonly static DicomTagSQ ChannelDescriptionCodeSequence = new DicomTagSQ(0x0022, 0x001A);

        ///<summary>(0022,001B) VR=SQ VM=1 Refractive State Sequence</summary>
        public readonly static DicomTagSQ RefractiveStateSequence = new DicomTagSQ(0x0022, 0x001B);

        ///<summary>(0022,001C) VR=SQ VM=1 Mydriatic Agent Code Sequence</summary>
        public readonly static DicomTagSQ MydriaticAgentCodeSequence = new DicomTagSQ(0x0022, 0x001C);

        ///<summary>(0022,001D) VR=SQ VM=1 Relative Image Position Code Sequence</summary>
        public readonly static DicomTagSQ RelativeImagePositionCodeSequence = new DicomTagSQ(0x0022, 0x001D);

        ///<summary>(0022,001E) VR=FL VM=1 Camera Angle of View</summary>
        public readonly static DicomTagFL CameraAngleOfView = new DicomTagFL(0x0022, 0x001E);

        ///<summary>(0022,0020) VR=SQ VM=1 Stereo Pairs Sequence</summary>
        public readonly static DicomTagSQ StereoPairsSequence = new DicomTagSQ(0x0022, 0x0020);

        ///<summary>(0022,0021) VR=SQ VM=1 Left Image Sequence</summary>
        public readonly static DicomTagSQ LeftImageSequence = new DicomTagSQ(0x0022, 0x0021);

        ///<summary>(0022,0022) VR=SQ VM=1 Right Image Sequence</summary>
        public readonly static DicomTagSQ RightImageSequence = new DicomTagSQ(0x0022, 0x0022);

        ///<summary>(0022,0028) VR=CS VM=1 Stereo Pairs Present</summary>
        public readonly static DicomTagCS StereoPairsPresent = new DicomTagCS(0x0022, 0x0028);

        ///<summary>(0022,0030) VR=FL VM=1 Axial Length of the Eye</summary>
        public readonly static DicomTagFL AxialLengthOfTheEye = new DicomTagFL(0x0022, 0x0030);

        ///<summary>(0022,0031) VR=SQ VM=1 Ophthalmic Frame Location Sequence</summary>
        public readonly static DicomTagSQ OphthalmicFrameLocationSequence = new DicomTagSQ(0x0022, 0x0031);

        ///<summary>(0022,0032) VR=FL VM=2-2n Reference Coordinates</summary>
        public readonly static DicomTagFLs ReferenceCoordinates = new DicomTagFLs(0x0022, 0x0032);

        ///<summary>(0022,0035) VR=FL VM=1 Depth Spatial Resolution</summary>
        public readonly static DicomTagFL DepthSpatialResolution = new DicomTagFL(0x0022, 0x0035);

        ///<summary>(0022,0036) VR=FL VM=1 Maximum Depth Distortion</summary>
        public readonly static DicomTagFL MaximumDepthDistortion = new DicomTagFL(0x0022, 0x0036);

        ///<summary>(0022,0037) VR=FL VM=1 Along-scan Spatial Resolution</summary>
        public readonly static DicomTagFL AlongScanSpatialResolution = new DicomTagFL(0x0022, 0x0037);

        ///<summary>(0022,0038) VR=FL VM=1 Maximum Along-scan Distortion</summary>
        public readonly static DicomTagFL MaximumAlongScanDistortion = new DicomTagFL(0x0022, 0x0038);

        ///<summary>(0022,0039) VR=CS VM=1 Ophthalmic Image Orientation</summary>
        public readonly static DicomTagCS OphthalmicImageOrientation = new DicomTagCS(0x0022, 0x0039);

        ///<summary>(0022,0041) VR=FL VM=1 Depth of Transverse Image</summary>
        public readonly static DicomTagFL DepthOfTransverseImage = new DicomTagFL(0x0022, 0x0041);

        ///<summary>(0022,0042) VR=SQ VM=1 Mydriatic Agent Concentration Units Sequence</summary>
        public readonly static DicomTagSQ MydriaticAgentConcentrationUnitsSequence = new DicomTagSQ(0x0022, 0x0042);

        ///<summary>(0022,0048) VR=FL VM=1 Across-scan Spatial Resolution</summary>
        public readonly static DicomTagFL AcrossScanSpatialResolution = new DicomTagFL(0x0022, 0x0048);

        ///<summary>(0022,0049) VR=FL VM=1 Maximum Across-scan Distortion</summary>
        public readonly static DicomTagFL MaximumAcrossScanDistortion = new DicomTagFL(0x0022, 0x0049);

        ///<summary>(0022,004E) VR=DS VM=1 Mydriatic Agent Concentration</summary>
        public readonly static DicomTagDS MydriaticAgentConcentration = new DicomTagDS(0x0022, 0x004E);

        ///<summary>(0022,0055) VR=FL VM=1 Illumination Wave Length</summary>
        public readonly static DicomTagFL IlluminationWaveLength = new DicomTagFL(0x0022, 0x0055);

        ///<summary>(0022,0056) VR=FL VM=1 Illumination Power</summary>
        public readonly static DicomTagFL IlluminationPower = new DicomTagFL(0x0022, 0x0056);

        ///<summary>(0022,0057) VR=FL VM=1 Illumination Bandwidth</summary>
        public readonly static DicomTagFL IlluminationBandwidth = new DicomTagFL(0x0022, 0x0057);

        ///<summary>(0022,0058) VR=SQ VM=1 Mydriatic Agent Sequence</summary>
        public readonly static DicomTagSQ MydriaticAgentSequence = new DicomTagSQ(0x0022, 0x0058);

        ///<summary>(0022,1007) VR=SQ VM=1 Ophthalmic Axial Measurements Right Eye Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialMeasurementsRightEyeSequence = new DicomTagSQ(0x0022, 0x1007);

        ///<summary>(0022,1008) VR=SQ VM=1 Ophthalmic Axial Measurements Left Eye Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialMeasurementsLeftEyeSequence = new DicomTagSQ(0x0022, 0x1008);

        ///<summary>(0022,1009) VR=CS VM=1 Ophthalmic Axial Measurements Device Type</summary>
        public readonly static DicomTagCS OphthalmicAxialMeasurementsDeviceType = new DicomTagCS(0x0022, 0x1009);

        ///<summary>(0022,1010) VR=CS VM=1 Ophthalmic Axial Length Measurements Type</summary>
        public readonly static DicomTagCS OphthalmicAxialLengthMeasurementsType = new DicomTagCS(0x0022, 0x1010);

        ///<summary>(0022,1012) VR=SQ VM=1 Ophthalmic Axial Length Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthSequence = new DicomTagSQ(0x0022, 0x1012);

        ///<summary>(0022,1019) VR=FL VM=1 Ophthalmic Axial Length</summary>
        public readonly static DicomTagFL OphthalmicAxialLength = new DicomTagFL(0x0022, 0x1019);

        ///<summary>(0022,1024) VR=SQ VM=1 Lens Status Code Sequence</summary>
        public readonly static DicomTagSQ LensStatusCodeSequence = new DicomTagSQ(0x0022, 0x1024);

        ///<summary>(0022,1025) VR=SQ VM=1 Vitreous Status Code Sequence</summary>
        public readonly static DicomTagSQ VitreousStatusCodeSequence = new DicomTagSQ(0x0022, 0x1025);

        ///<summary>(0022,1028) VR=SQ VM=1 IOL Formula Code Sequence</summary>
        public readonly static DicomTagSQ IOLFormulaCodeSequence = new DicomTagSQ(0x0022, 0x1028);

        ///<summary>(0022,1029) VR=LO VM=1 IOL Formula Detail</summary>
        public readonly static DicomTagLO IOLFormulaDetail = new DicomTagLO(0x0022, 0x1029);

        ///<summary>(0022,1033) VR=FL VM=1 Keratometer Index</summary>
        public readonly static DicomTagFL KeratometerIndex = new DicomTagFL(0x0022, 0x1033);

        ///<summary>(0022,1035) VR=SQ VM=1 Source of Ophthalmic Axial Length Code Sequence</summary>
        public readonly static DicomTagSQ SourceOfOphthalmicAxialLengthCodeSequence = new DicomTagSQ(0x0022, 0x1035);

        ///<summary>(0022,1036) VR=SQ VM=1 Source of Corneal Size Data Code Sequence</summary>
        public readonly static DicomTagSQ SourceOfCornealSizeDataCodeSequence = new DicomTagSQ(0x0022, 0x1036);

        ///<summary>(0022,1037) VR=FL VM=1 Target Refraction</summary>
        public readonly static DicomTagFL TargetRefraction = new DicomTagFL(0x0022, 0x1037);

        ///<summary>(0022,1039) VR=CS VM=1 Refractive Procedure Occurred</summary>
        public readonly static DicomTagCS RefractiveProcedureOccurred = new DicomTagCS(0x0022, 0x1039);

        ///<summary>(0022,1040) VR=SQ VM=1 Refractive Surgery Type Code Sequence</summary>
        public readonly static DicomTagSQ RefractiveSurgeryTypeCodeSequence = new DicomTagSQ(0x0022, 0x1040);

        ///<summary>(0022,1044) VR=SQ VM=1 Ophthalmic Ultrasound Method Code Sequence</summary>
        public readonly static DicomTagSQ OphthalmicUltrasoundMethodCodeSequence = new DicomTagSQ(0x0022, 0x1044);

        ///<summary>(0022,1045) VR=SQ VM=1 Surgically Induced Astigmatism Sequence</summary>
        public readonly static DicomTagSQ SurgicallyInducedAstigmatismSequence = new DicomTagSQ(0x0022, 0x1045);

        ///<summary>(0022,1046) VR=CS VM=1 Type of Optical Correction</summary>
        public readonly static DicomTagCS TypeOfOpticalCorrection = new DicomTagCS(0x0022, 0x1046);

        ///<summary>(0022,1047) VR=SQ VM=1 Toric IOL Power Sequence</summary>
        public readonly static DicomTagSQ ToricIOLPowerSequence = new DicomTagSQ(0x0022, 0x1047);

        ///<summary>(0022,1048) VR=SQ VM=1 Predicted Toric Error Sequence</summary>
        public readonly static DicomTagSQ PredictedToricErrorSequence = new DicomTagSQ(0x0022, 0x1048);

        ///<summary>(0022,1049) VR=CS VM=1 Pre-Selected for Implantation</summary>
        public readonly static DicomTagCS PreSelectedForImplantation = new DicomTagCS(0x0022, 0x1049);

        ///<summary>(0022,104A) VR=SQ VM=1 Toric IOL Power for Exact Emmetropia Sequence</summary>
        public readonly static DicomTagSQ ToricIOLPowerForExactEmmetropiaSequence = new DicomTagSQ(0x0022, 0x104A);

        ///<summary>(0022,104B) VR=SQ VM=1 Toric IOL Power for Exact Target Refraction Sequence</summary>
        public readonly static DicomTagSQ ToricIOLPowerForExactTargetRefractionSequence = new DicomTagSQ(0x0022, 0x104B);

        ///<summary>(0022,1050) VR=SQ VM=1 Ophthalmic Axial Length Measurements Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthMeasurementsSequence = new DicomTagSQ(0x0022, 0x1050);

        ///<summary>(0022,1053) VR=FL VM=1 IOL Power</summary>
        public readonly static DicomTagFL IOLPower = new DicomTagFL(0x0022, 0x1053);

        ///<summary>(0022,1054) VR=FL VM=1 Predicted Refractive Error</summary>
        public readonly static DicomTagFL PredictedRefractiveError = new DicomTagFL(0x0022, 0x1054);

        ///<summary>(0022,1059) VR=FL VM=1 Ophthalmic Axial Length Velocity</summary>
        public readonly static DicomTagFL OphthalmicAxialLengthVelocity = new DicomTagFL(0x0022, 0x1059);

        ///<summary>(0022,1065) VR=LO VM=1 Lens Status Description</summary>
        public readonly static DicomTagLO LensStatusDescription = new DicomTagLO(0x0022, 0x1065);

        ///<summary>(0022,1066) VR=LO VM=1 Vitreous Status Description</summary>
        public readonly static DicomTagLO VitreousStatusDescription = new DicomTagLO(0x0022, 0x1066);

        ///<summary>(0022,1090) VR=SQ VM=1 IOL Power Sequence</summary>
        public readonly static DicomTagSQ IOLPowerSequence = new DicomTagSQ(0x0022, 0x1090);

        ///<summary>(0022,1092) VR=SQ VM=1 Lens Constant Sequence</summary>
        public readonly static DicomTagSQ LensConstantSequence = new DicomTagSQ(0x0022, 0x1092);

        ///<summary>(0022,1093) VR=LO VM=1 IOL Manufacturer</summary>
        public readonly static DicomTagLO IOLManufacturer = new DicomTagLO(0x0022, 0x1093);

        ///<summary>(0022,1094) VR=LO VM=1 Lens Constant Description (RETIRED)</summary>
        public readonly static DicomTagLO LensConstantDescriptionRETIRED = new DicomTagLO(0x0022, 0x1094);

        ///<summary>(0022,1095) VR=LO VM=1 Implant Name</summary>
        public readonly static DicomTagLO ImplantName = new DicomTagLO(0x0022, 0x1095);

        ///<summary>(0022,1096) VR=SQ VM=1 Keratometry Measurement Type Code Sequence</summary>
        public readonly static DicomTagSQ KeratometryMeasurementTypeCodeSequence = new DicomTagSQ(0x0022, 0x1096);

        ///<summary>(0022,1097) VR=LO VM=1 Implant Part Number</summary>
        public readonly static DicomTagLO ImplantPartNumber = new DicomTagLO(0x0022, 0x1097);

        ///<summary>(0022,1100) VR=SQ VM=1 Referenced Ophthalmic Axial Measurements Sequence</summary>
        public readonly static DicomTagSQ ReferencedOphthalmicAxialMeasurementsSequence = new DicomTagSQ(0x0022, 0x1100);

        ///<summary>(0022,1101) VR=SQ VM=1 Ophthalmic Axial Length Measurements Segment Name Code Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthMeasurementsSegmentNameCodeSequence = new DicomTagSQ(0x0022, 0x1101);

        ///<summary>(0022,1103) VR=SQ VM=1 Refractive Error Before Refractive Surgery Code Sequence</summary>
        public readonly static DicomTagSQ RefractiveErrorBeforeRefractiveSurgeryCodeSequence = new DicomTagSQ(0x0022, 0x1103);

        ///<summary>(0022,1121) VR=FL VM=1 IOL Power For Exact Emmetropia</summary>
        public readonly static DicomTagFL IOLPowerForExactEmmetropia = new DicomTagFL(0x0022, 0x1121);

        ///<summary>(0022,1122) VR=FL VM=1 IOL Power For Exact Target Refraction</summary>
        public readonly static DicomTagFL IOLPowerForExactTargetRefraction = new DicomTagFL(0x0022, 0x1122);

        ///<summary>(0022,1125) VR=SQ VM=1 Anterior Chamber Depth Definition Code Sequence</summary>
        public readonly static DicomTagSQ AnteriorChamberDepthDefinitionCodeSequence = new DicomTagSQ(0x0022, 0x1125);

        ///<summary>(0022,1127) VR=SQ VM=1 Lens Thickness Sequence</summary>
        public readonly static DicomTagSQ LensThicknessSequence = new DicomTagSQ(0x0022, 0x1127);

        ///<summary>(0022,1128) VR=SQ VM=1 Anterior Chamber Depth Sequence</summary>
        public readonly static DicomTagSQ AnteriorChamberDepthSequence = new DicomTagSQ(0x0022, 0x1128);

        ///<summary>(0022,112A) VR=SQ VM=1 Calculation Comment Sequence</summary>
        public readonly static DicomTagSQ CalculationCommentSequence = new DicomTagSQ(0x0022, 0x112A);

        ///<summary>(0022,112B) VR=CS VM=1 Calculation Comment Type</summary>
        public readonly static DicomTagCS CalculationCommentType = new DicomTagCS(0x0022, 0x112B);

        ///<summary>(0022,112C) VR=LT VM=1 Calculation Comment</summary>
        public readonly static DicomTagLT CalculationComment = new DicomTagLT(0x0022, 0x112C);

        ///<summary>(0022,1130) VR=FL VM=1 Lens Thickness</summary>
        public readonly static DicomTagFL LensThickness = new DicomTagFL(0x0022, 0x1130);

        ///<summary>(0022,1131) VR=FL VM=1 Anterior Chamber Depth</summary>
        public readonly static DicomTagFL AnteriorChamberDepth = new DicomTagFL(0x0022, 0x1131);

        ///<summary>(0022,1132) VR=SQ VM=1 Source of Lens Thickness Data Code Sequence</summary>
        public readonly static DicomTagSQ SourceOfLensThicknessDataCodeSequence = new DicomTagSQ(0x0022, 0x1132);

        ///<summary>(0022,1133) VR=SQ VM=1 Source of Anterior Chamber Depth Data Code Sequence</summary>
        public readonly static DicomTagSQ SourceOfAnteriorChamberDepthDataCodeSequence = new DicomTagSQ(0x0022, 0x1133);

        ///<summary>(0022,1134) VR=SQ VM=1 Source of Refractive Measurements Sequence</summary>
        public readonly static DicomTagSQ SourceOfRefractiveMeasurementsSequence = new DicomTagSQ(0x0022, 0x1134);

        ///<summary>(0022,1135) VR=SQ VM=1 Source of Refractive Measurements Code Sequence</summary>
        public readonly static DicomTagSQ SourceOfRefractiveMeasurementsCodeSequence = new DicomTagSQ(0x0022, 0x1135);

        ///<summary>(0022,1140) VR=CS VM=1 Ophthalmic Axial Length Measurement Modified</summary>
        public readonly static DicomTagCS OphthalmicAxialLengthMeasurementModified = new DicomTagCS(0x0022, 0x1140);

        ///<summary>(0022,1150) VR=SQ VM=1 Ophthalmic Axial Length Data Source Code Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthDataSourceCodeSequence = new DicomTagSQ(0x0022, 0x1150);

        ///<summary>(0022,1153) VR=SQ VM=1 Ophthalmic Axial Length Acquisition Method Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthAcquisitionMethodCodeSequenceRETIRED = new DicomTagSQ(0x0022, 0x1153);

        ///<summary>(0022,1155) VR=FL VM=1 Signal to Noise Ratio</summary>
        public readonly static DicomTagFL SignalToNoiseRatio = new DicomTagFL(0x0022, 0x1155);

        ///<summary>(0022,1159) VR=LO VM=1 Ophthalmic Axial Length Data Source Description</summary>
        public readonly static DicomTagLO OphthalmicAxialLengthDataSourceDescription = new DicomTagLO(0x0022, 0x1159);

        ///<summary>(0022,1210) VR=SQ VM=1 Ophthalmic Axial Length Measurements Total Length Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthMeasurementsTotalLengthSequence = new DicomTagSQ(0x0022, 0x1210);

        ///<summary>(0022,1211) VR=SQ VM=1 Ophthalmic Axial Length Measurements Segmental Length Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthMeasurementsSegmentalLengthSequence = new DicomTagSQ(0x0022, 0x1211);

        ///<summary>(0022,1212) VR=SQ VM=1 Ophthalmic Axial Length Measurements Length Summation Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthMeasurementsLengthSummationSequence = new DicomTagSQ(0x0022, 0x1212);

        ///<summary>(0022,1220) VR=SQ VM=1 Ultrasound Ophthalmic Axial Length Measurements Sequence</summary>
        public readonly static DicomTagSQ UltrasoundOphthalmicAxialLengthMeasurementsSequence = new DicomTagSQ(0x0022, 0x1220);

        ///<summary>(0022,1225) VR=SQ VM=1 Optical Ophthalmic Axial Length Measurements Sequence</summary>
        public readonly static DicomTagSQ OpticalOphthalmicAxialLengthMeasurementsSequence = new DicomTagSQ(0x0022, 0x1225);

        ///<summary>(0022,1230) VR=SQ VM=1 Ultrasound Selected Ophthalmic Axial Length Sequence</summary>
        public readonly static DicomTagSQ UltrasoundSelectedOphthalmicAxialLengthSequence = new DicomTagSQ(0x0022, 0x1230);

        ///<summary>(0022,1250) VR=SQ VM=1 Ophthalmic Axial Length Selection Method Code Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthSelectionMethodCodeSequence = new DicomTagSQ(0x0022, 0x1250);

        ///<summary>(0022,1255) VR=SQ VM=1 Optical Selected Ophthalmic Axial Length Sequence</summary>
        public readonly static DicomTagSQ OpticalSelectedOphthalmicAxialLengthSequence = new DicomTagSQ(0x0022, 0x1255);

        ///<summary>(0022,1257) VR=SQ VM=1 Selected Segmental Ophthalmic Axial Length Sequence</summary>
        public readonly static DicomTagSQ SelectedSegmentalOphthalmicAxialLengthSequence = new DicomTagSQ(0x0022, 0x1257);

        ///<summary>(0022,1260) VR=SQ VM=1 Selected Total Ophthalmic Axial Length Sequence</summary>
        public readonly static DicomTagSQ SelectedTotalOphthalmicAxialLengthSequence = new DicomTagSQ(0x0022, 0x1260);

        ///<summary>(0022,1262) VR=SQ VM=1 Ophthalmic Axial Length Quality Metric Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthQualityMetricSequence = new DicomTagSQ(0x0022, 0x1262);

        ///<summary>(0022,1265) VR=SQ VM=1 Ophthalmic Axial Length Quality Metric Type Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ OphthalmicAxialLengthQualityMetricTypeCodeSequenceRETIRED = new DicomTagSQ(0x0022, 0x1265);

        ///<summary>(0022,1273) VR=LO VM=1 Ophthalmic Axial Length Quality Metric Type Description (RETIRED)</summary>
        public readonly static DicomTagLO OphthalmicAxialLengthQualityMetricTypeDescriptionRETIRED = new DicomTagLO(0x0022, 0x1273);

        ///<summary>(0022,1300) VR=SQ VM=1 Intraocular Lens Calculations Right Eye Sequence</summary>
        public readonly static DicomTagSQ IntraocularLensCalculationsRightEyeSequence = new DicomTagSQ(0x0022, 0x1300);

        ///<summary>(0022,1310) VR=SQ VM=1 Intraocular Lens Calculations Left Eye Sequence</summary>
        public readonly static DicomTagSQ IntraocularLensCalculationsLeftEyeSequence = new DicomTagSQ(0x0022, 0x1310);

        ///<summary>(0022,1330) VR=SQ VM=1 Referenced Ophthalmic Axial Length Measurement QC Image Sequence</summary>
        public readonly static DicomTagSQ ReferencedOphthalmicAxialLengthMeasurementQCImageSequence = new DicomTagSQ(0x0022, 0x1330);

        ///<summary>(0022,1415) VR=CS VM=1 Ophthalmic Mapping Device Type</summary>
        public readonly static DicomTagCS OphthalmicMappingDeviceType = new DicomTagCS(0x0022, 0x1415);

        ///<summary>(0022,1420) VR=SQ VM=1 Acquisition Method Code Sequence</summary>
        public readonly static DicomTagSQ AcquisitionMethodCodeSequence = new DicomTagSQ(0x0022, 0x1420);

        ///<summary>(0022,1423) VR=SQ VM=1 Acquisition Method Algorithm Sequence</summary>
        public readonly static DicomTagSQ AcquisitionMethodAlgorithmSequence = new DicomTagSQ(0x0022, 0x1423);

        ///<summary>(0022,1436) VR=SQ VM=1 Ophthalmic Thickness Map Type Code Sequence</summary>
        public readonly static DicomTagSQ OphthalmicThicknessMapTypeCodeSequence = new DicomTagSQ(0x0022, 0x1436);

        ///<summary>(0022,1443) VR=SQ VM=1 Ophthalmic Thickness Mapping Normals Sequence</summary>
        public readonly static DicomTagSQ OphthalmicThicknessMappingNormalsSequence = new DicomTagSQ(0x0022, 0x1443);

        ///<summary>(0022,1445) VR=SQ VM=1 Retinal Thickness Definition Code Sequence</summary>
        public readonly static DicomTagSQ RetinalThicknessDefinitionCodeSequence = new DicomTagSQ(0x0022, 0x1445);

        ///<summary>(0022,1450) VR=SQ VM=1 Pixel Value Mapping to Coded Concept Sequence</summary>
        public readonly static DicomTagSQ PixelValueMappingToCodedConceptSequence = new DicomTagSQ(0x0022, 0x1450);

        ///<summary>(0022,1452) VR=US/SS VM=1 Mapped Pixel Value</summary>
        public readonly static DicomTagUSSS MappedPixelValue = new DicomTagUSSS(0x0022, 0x1452);

        ///<summary>(0022,1454) VR=LO VM=1 Pixel Value Mapping Explanation</summary>
        public readonly static DicomTagLO PixelValueMappingExplanation = new DicomTagLO(0x0022, 0x1454);

        ///<summary>(0022,1458) VR=SQ VM=1 Ophthalmic Thickness Map Quality Threshold Sequence</summary>
        public readonly static DicomTagSQ OphthalmicThicknessMapQualityThresholdSequence = new DicomTagSQ(0x0022, 0x1458);

        ///<summary>(0022,1460) VR=FL VM=1 Ophthalmic Thickness Map Threshold Quality Rating</summary>
        public readonly static DicomTagFL OphthalmicThicknessMapThresholdQualityRating = new DicomTagFL(0x0022, 0x1460);

        ///<summary>(0022,1463) VR=FL VM=2 Anatomic Structure Reference Point</summary>
        public readonly static DicomTagFLs AnatomicStructureReferencePoint = new DicomTagFLs(0x0022, 0x1463);

        ///<summary>(0022,1465) VR=SQ VM=1 Registration to Localizer Sequence</summary>
        public readonly static DicomTagSQ RegistrationToLocalizerSequence = new DicomTagSQ(0x0022, 0x1465);

        ///<summary>(0022,1466) VR=CS VM=1 Registered Localizer Units</summary>
        public readonly static DicomTagCS RegisteredLocalizerUnits = new DicomTagCS(0x0022, 0x1466);

        ///<summary>(0022,1467) VR=FL VM=2 Registered Localizer Top Left Hand Corner</summary>
        public readonly static DicomTagFLs RegisteredLocalizerTopLeftHandCorner = new DicomTagFLs(0x0022, 0x1467);

        ///<summary>(0022,1468) VR=FL VM=2 Registered Localizer Bottom Right Hand Corner</summary>
        public readonly static DicomTagFLs RegisteredLocalizerBottomRightHandCorner = new DicomTagFLs(0x0022, 0x1468);

        ///<summary>(0022,1470) VR=SQ VM=1 Ophthalmic Thickness Map Quality Rating Sequence</summary>
        public readonly static DicomTagSQ OphthalmicThicknessMapQualityRatingSequence = new DicomTagSQ(0x0022, 0x1470);

        ///<summary>(0022,1472) VR=SQ VM=1 Relevant OPT Attributes Sequence</summary>
        public readonly static DicomTagSQ RelevantOPTAttributesSequence = new DicomTagSQ(0x0022, 0x1472);

        ///<summary>(0022,1512) VR=SQ VM=1 Transformation Method Code Sequence</summary>
        public readonly static DicomTagSQ TransformationMethodCodeSequence = new DicomTagSQ(0x0022, 0x1512);

        ///<summary>(0022,1513) VR=SQ VM=1 Transformation Algorithm Sequence</summary>
        public readonly static DicomTagSQ TransformationAlgorithmSequence = new DicomTagSQ(0x0022, 0x1513);

        ///<summary>(0022,1515) VR=CS VM=1 Ophthalmic Axial Length Method</summary>
        public readonly static DicomTagCS OphthalmicAxialLengthMethod = new DicomTagCS(0x0022, 0x1515);

        ///<summary>(0022,1517) VR=FL VM=1 Ophthalmic FOV</summary>
        public readonly static DicomTagFL OphthalmicFOV = new DicomTagFL(0x0022, 0x1517);

        ///<summary>(0022,1518) VR=SQ VM=1 Two Dimensional to Three Dimensional Map Sequence</summary>
        public readonly static DicomTagSQ TwoDimensionalToThreeDimensionalMapSequence = new DicomTagSQ(0x0022, 0x1518);

        ///<summary>(0022,1525) VR=SQ VM=1 Wide Field Ophthalmic Photography Quality Rating Sequence</summary>
        public readonly static DicomTagSQ WideFieldOphthalmicPhotographyQualityRatingSequence = new DicomTagSQ(0x0022, 0x1525);

        ///<summary>(0022,1526) VR=SQ VM=1 Wide Field Ophthalmic Photography Quality Threshold Sequence</summary>
        public readonly static DicomTagSQ WideFieldOphthalmicPhotographyQualityThresholdSequence = new DicomTagSQ(0x0022, 0x1526);

        ///<summary>(0022,1527) VR=FL VM=1 Wide Field Ophthalmic Photography Threshold Quality Rating</summary>
        public readonly static DicomTagFL WideFieldOphthalmicPhotographyThresholdQualityRating = new DicomTagFL(0x0022, 0x1527);

        ///<summary>(0022,1528) VR=FL VM=1 X Coordinates Center Pixel View Angle</summary>
        public readonly static DicomTagFL XCoordinatesCenterPixelViewAngle = new DicomTagFL(0x0022, 0x1528);

        ///<summary>(0022,1529) VR=FL VM=1 Y Coordinates Center Pixel View Angle</summary>
        public readonly static DicomTagFL YCoordinatesCenterPixelViewAngle = new DicomTagFL(0x0022, 0x1529);

        ///<summary>(0022,1530) VR=UL VM=1 Number of Map Points</summary>
        public readonly static DicomTagUL NumberOfMapPoints = new DicomTagUL(0x0022, 0x1530);

        ///<summary>(0022,1531) VR=OF VM=1 Two Dimensional to Three Dimensional Map Data</summary>
        public readonly static DicomTagOF TwoDimensionalToThreeDimensionalMapData = new DicomTagOF(0x0022, 0x1531);

        ///<summary>(0022,1612) VR=SQ VM=1 Derivation Algorithm Sequence</summary>
        public readonly static DicomTagSQ DerivationAlgorithmSequence = new DicomTagSQ(0x0022, 0x1612);

        ///<summary>(0022,1615) VR=SQ VM=1 Ophthalmic Image Type Code Sequence</summary>
        public readonly static DicomTagSQ OphthalmicImageTypeCodeSequence = new DicomTagSQ(0x0022, 0x1615);

        ///<summary>(0022,1616) VR=LO VM=1 Ophthalmic Image Type Description</summary>
        public readonly static DicomTagLO OphthalmicImageTypeDescription = new DicomTagLO(0x0022, 0x1616);

        ///<summary>(0022,1618) VR=SQ VM=1 Scan Pattern Type Code Sequence</summary>
        public readonly static DicomTagSQ ScanPatternTypeCodeSequence = new DicomTagSQ(0x0022, 0x1618);

        ///<summary>(0022,1620) VR=SQ VM=1 Referenced Surface Mesh Identification Sequence</summary>
        public readonly static DicomTagSQ ReferencedSurfaceMeshIdentificationSequence = new DicomTagSQ(0x0022, 0x1620);

        ///<summary>(0022,1622) VR=CS VM=1 Ophthalmic Volumetric Properties Flag</summary>
        public readonly static DicomTagCS OphthalmicVolumetricPropertiesFlag = new DicomTagCS(0x0022, 0x1622);

        ///<summary>(0022,1623) VR=FL VM=1 Ophthalmic Anatomic Reference Point Frame Coordinate</summary>
        public readonly static DicomTagFL OphthalmicAnatomicReferencePointFrameCoordinate = new DicomTagFL(0x0022, 0x1623);

        ///<summary>(0022,1624) VR=FL VM=1 Ophthalmic Anatomic Reference Point X-Coordinate</summary>
        public readonly static DicomTagFL OphthalmicAnatomicReferencePointXCoordinate = new DicomTagFL(0x0022, 0x1624);

        ///<summary>(0022,1626) VR=FL VM=1 Ophthalmic Anatomic Reference Point Y-Coordinate</summary>
        public readonly static DicomTagFL OphthalmicAnatomicReferencePointYCoordinate = new DicomTagFL(0x0022, 0x1626);

        ///<summary>(0022,1627) VR=SQ VM=1 Ophthalmic En Face Volume Descriptor Sequence</summary>
        public readonly static DicomTagSQ OphthalmicEnFaceVolumeDescriptorSequence = new DicomTagSQ(0x0022, 0x1627);

        ///<summary>(0022,1628) VR=SQ VM=1 Ophthalmic En Face Image Quality Rating Sequence</summary>
        public readonly static DicomTagSQ OphthalmicEnFaceImageQualityRatingSequence = new DicomTagSQ(0x0022, 0x1628);

        ///<summary>(0022,1629) VR=CS VM=1 Ophthalmic En Face Volume Descriptor Scope</summary>
        public readonly static DicomTagCS OphthalmicEnFaceVolumeDescriptorScope = new DicomTagCS(0x0022, 0x1629);

        ///<summary>(0022,1630) VR=DS VM=1 Quality Threshold</summary>
        public readonly static DicomTagDS QualityThreshold = new DicomTagDS(0x0022, 0x1630);

        ///<summary>(0022,1632) VR=SQ VM=1 Ophthalmic Anatomic Reference Point Sequence</summary>
        public readonly static DicomTagSQ OphthalmicAnatomicReferencePointSequence = new DicomTagSQ(0x0022, 0x1632);

        ///<summary>(0022,1633) VR=CS VM=1 Ophthalmic Anatomic Reference Point Localization Type</summary>
        public readonly static DicomTagCS OphthalmicAnatomicReferencePointLocalizationType = new DicomTagCS(0x0022, 0x1633);

        ///<summary>(0022,1634) VR=IS VM=1 Primary Anatomic Structure Item Index</summary>
        public readonly static DicomTagIS PrimaryAnatomicStructureItemIndex = new DicomTagIS(0x0022, 0x1634);

        ///<summary>(0022,1640) VR=SQ VM=1 OCT B-scan Analysis Acquisition Parameters Sequence</summary>
        public readonly static DicomTagSQ OCTBscanAnalysisAcquisitionParametersSequence = new DicomTagSQ(0x0022, 0x1640);

        ///<summary>(0022,1642) VR=UL VM=1 Number of B-scans Per Frame</summary>
        public readonly static DicomTagUL NumberOfBscansPerFrame = new DicomTagUL(0x0022, 0x1642);

        ///<summary>(0022,1643) VR=FL VM=1 B-scan Slab Thickness</summary>
        public readonly static DicomTagFL BscanSlabThickness = new DicomTagFL(0x0022, 0x1643);

        ///<summary>(0022,1644) VR=FL VM=1 Distance Between B-scan Slabs</summary>
        public readonly static DicomTagFL DistanceBetweenBscanSlabs = new DicomTagFL(0x0022, 0x1644);

        ///<summary>(0022,1645) VR=FL VM=1 B-scan Cycle Time</summary>
        public readonly static DicomTagFL BscanCycleTime = new DicomTagFL(0x0022, 0x1645);

        ///<summary>(0022,1646) VR=FL VM=1-n B-scan Cycle Time Vector</summary>
        public readonly static DicomTagFLs BscanCycleTimeVector = new DicomTagFLs(0x0022, 0x1646);

        ///<summary>(0022,1649) VR=FL VM=1 A-scan Rate</summary>
        public readonly static DicomTagFL AscanRate = new DicomTagFL(0x0022, 0x1649);

        ///<summary>(0022,1650) VR=FL VM=1 B-scan Rate</summary>
        public readonly static DicomTagFL BscanRate = new DicomTagFL(0x0022, 0x1650);

        ///<summary>(0022,1658) VR=UL VM=1 Surface Mesh Z-Pixel Offset</summary>
        public readonly static DicomTagUL SurfaceMeshZPixelOffset = new DicomTagUL(0x0022, 0x1658);

        ///<summary>(0024,0010) VR=FL VM=1 Visual Field Horizontal Extent</summary>
        public readonly static DicomTagFL VisualFieldHorizontalExtent = new DicomTagFL(0x0024, 0x0010);

        ///<summary>(0024,0011) VR=FL VM=1 Visual Field Vertical Extent</summary>
        public readonly static DicomTagFL VisualFieldVerticalExtent = new DicomTagFL(0x0024, 0x0011);

        ///<summary>(0024,0012) VR=CS VM=1 Visual Field Shape</summary>
        public readonly static DicomTagCS VisualFieldShape = new DicomTagCS(0x0024, 0x0012);

        ///<summary>(0024,0016) VR=SQ VM=1 Screening Test Mode Code Sequence</summary>
        public readonly static DicomTagSQ ScreeningTestModeCodeSequence = new DicomTagSQ(0x0024, 0x0016);

        ///<summary>(0024,0018) VR=FL VM=1 Maximum Stimulus Luminance</summary>
        public readonly static DicomTagFL MaximumStimulusLuminance = new DicomTagFL(0x0024, 0x0018);

        ///<summary>(0024,0020) VR=FL VM=1 Background Luminance</summary>
        public readonly static DicomTagFL BackgroundLuminance = new DicomTagFL(0x0024, 0x0020);

        ///<summary>(0024,0021) VR=SQ VM=1 Stimulus Color Code Sequence</summary>
        public readonly static DicomTagSQ StimulusColorCodeSequence = new DicomTagSQ(0x0024, 0x0021);

        ///<summary>(0024,0024) VR=SQ VM=1 Background Illumination Color Code Sequence</summary>
        public readonly static DicomTagSQ BackgroundIlluminationColorCodeSequence = new DicomTagSQ(0x0024, 0x0024);

        ///<summary>(0024,0025) VR=FL VM=1 Stimulus Area</summary>
        public readonly static DicomTagFL StimulusArea = new DicomTagFL(0x0024, 0x0025);

        ///<summary>(0024,0028) VR=FL VM=1 Stimulus Presentation Time</summary>
        public readonly static DicomTagFL StimulusPresentationTime = new DicomTagFL(0x0024, 0x0028);

        ///<summary>(0024,0032) VR=SQ VM=1 Fixation Sequence</summary>
        public readonly static DicomTagSQ FixationSequence = new DicomTagSQ(0x0024, 0x0032);

        ///<summary>(0024,0033) VR=SQ VM=1 Fixation Monitoring Code Sequence</summary>
        public readonly static DicomTagSQ FixationMonitoringCodeSequence = new DicomTagSQ(0x0024, 0x0033);

        ///<summary>(0024,0034) VR=SQ VM=1 Visual Field Catch Trial Sequence</summary>
        public readonly static DicomTagSQ VisualFieldCatchTrialSequence = new DicomTagSQ(0x0024, 0x0034);

        ///<summary>(0024,0035) VR=US VM=1 Fixation Checked Quantity</summary>
        public readonly static DicomTagUS FixationCheckedQuantity = new DicomTagUS(0x0024, 0x0035);

        ///<summary>(0024,0036) VR=US VM=1 Patient Not Properly Fixated Quantity</summary>
        public readonly static DicomTagUS PatientNotProperlyFixatedQuantity = new DicomTagUS(0x0024, 0x0036);

        ///<summary>(0024,0037) VR=CS VM=1 Presented Visual Stimuli Data Flag</summary>
        public readonly static DicomTagCS PresentedVisualStimuliDataFlag = new DicomTagCS(0x0024, 0x0037);

        ///<summary>(0024,0038) VR=US VM=1 Number of Visual Stimuli</summary>
        public readonly static DicomTagUS NumberOfVisualStimuli = new DicomTagUS(0x0024, 0x0038);

        ///<summary>(0024,0039) VR=CS VM=1 Excessive Fixation Losses Data Flag</summary>
        public readonly static DicomTagCS ExcessiveFixationLossesDataFlag = new DicomTagCS(0x0024, 0x0039);

        ///<summary>(0024,0040) VR=CS VM=1 Excessive Fixation Losses</summary>
        public readonly static DicomTagCS ExcessiveFixationLosses = new DicomTagCS(0x0024, 0x0040);

        ///<summary>(0024,0042) VR=US VM=1 Stimuli Retesting Quantity</summary>
        public readonly static DicomTagUS StimuliRetestingQuantity = new DicomTagUS(0x0024, 0x0042);

        ///<summary>(0024,0044) VR=LT VM=1 Comments on Patient's Performance of Visual Field</summary>
        public readonly static DicomTagLT CommentsOnPatientPerformanceOfVisualField = new DicomTagLT(0x0024, 0x0044);

        ///<summary>(0024,0045) VR=CS VM=1 False Negatives Estimate Flag</summary>
        public readonly static DicomTagCS FalseNegativesEstimateFlag = new DicomTagCS(0x0024, 0x0045);

        ///<summary>(0024,0046) VR=FL VM=1 False Negatives Estimate</summary>
        public readonly static DicomTagFL FalseNegativesEstimate = new DicomTagFL(0x0024, 0x0046);

        ///<summary>(0024,0048) VR=US VM=1 Negative Catch Trials Quantity</summary>
        public readonly static DicomTagUS NegativeCatchTrialsQuantity = new DicomTagUS(0x0024, 0x0048);

        ///<summary>(0024,0050) VR=US VM=1 False Negatives Quantity</summary>
        public readonly static DicomTagUS FalseNegativesQuantity = new DicomTagUS(0x0024, 0x0050);

        ///<summary>(0024,0051) VR=CS VM=1 Excessive False Negatives Data Flag</summary>
        public readonly static DicomTagCS ExcessiveFalseNegativesDataFlag = new DicomTagCS(0x0024, 0x0051);

        ///<summary>(0024,0052) VR=CS VM=1 Excessive False Negatives</summary>
        public readonly static DicomTagCS ExcessiveFalseNegatives = new DicomTagCS(0x0024, 0x0052);

        ///<summary>(0024,0053) VR=CS VM=1 False Positives Estimate Flag</summary>
        public readonly static DicomTagCS FalsePositivesEstimateFlag = new DicomTagCS(0x0024, 0x0053);

        ///<summary>(0024,0054) VR=FL VM=1 False Positives Estimate</summary>
        public readonly static DicomTagFL FalsePositivesEstimate = new DicomTagFL(0x0024, 0x0054);

        ///<summary>(0024,0055) VR=CS VM=1 Catch Trials Data Flag</summary>
        public readonly static DicomTagCS CatchTrialsDataFlag = new DicomTagCS(0x0024, 0x0055);

        ///<summary>(0024,0056) VR=US VM=1 Positive Catch Trials Quantity</summary>
        public readonly static DicomTagUS PositiveCatchTrialsQuantity = new DicomTagUS(0x0024, 0x0056);

        ///<summary>(0024,0057) VR=CS VM=1 Test Point Normals Data Flag</summary>
        public readonly static DicomTagCS TestPointNormalsDataFlag = new DicomTagCS(0x0024, 0x0057);

        ///<summary>(0024,0058) VR=SQ VM=1 Test Point Normals Sequence</summary>
        public readonly static DicomTagSQ TestPointNormalsSequence = new DicomTagSQ(0x0024, 0x0058);

        ///<summary>(0024,0059) VR=CS VM=1 Global Deviation Probability Normals Flag</summary>
        public readonly static DicomTagCS GlobalDeviationProbabilityNormalsFlag = new DicomTagCS(0x0024, 0x0059);

        ///<summary>(0024,0060) VR=US VM=1 False Positives Quantity</summary>
        public readonly static DicomTagUS FalsePositivesQuantity = new DicomTagUS(0x0024, 0x0060);

        ///<summary>(0024,0061) VR=CS VM=1 Excessive False Positives Data Flag</summary>
        public readonly static DicomTagCS ExcessiveFalsePositivesDataFlag = new DicomTagCS(0x0024, 0x0061);

        ///<summary>(0024,0062) VR=CS VM=1 Excessive False Positives</summary>
        public readonly static DicomTagCS ExcessiveFalsePositives = new DicomTagCS(0x0024, 0x0062);

        ///<summary>(0024,0063) VR=CS VM=1 Visual Field Test Normals Flag</summary>
        public readonly static DicomTagCS VisualFieldTestNormalsFlag = new DicomTagCS(0x0024, 0x0063);

        ///<summary>(0024,0064) VR=SQ VM=1 Results Normals Sequence</summary>
        public readonly static DicomTagSQ ResultsNormalsSequence = new DicomTagSQ(0x0024, 0x0064);

        ///<summary>(0024,0065) VR=SQ VM=1 Age Corrected Sensitivity Deviation Algorithm Sequence</summary>
        public readonly static DicomTagSQ AgeCorrectedSensitivityDeviationAlgorithmSequence = new DicomTagSQ(0x0024, 0x0065);

        ///<summary>(0024,0066) VR=FL VM=1 Global Deviation From Normal</summary>
        public readonly static DicomTagFL GlobalDeviationFromNormal = new DicomTagFL(0x0024, 0x0066);

        ///<summary>(0024,0067) VR=SQ VM=1 Generalized Defect Sensitivity Deviation Algorithm Sequence</summary>
        public readonly static DicomTagSQ GeneralizedDefectSensitivityDeviationAlgorithmSequence = new DicomTagSQ(0x0024, 0x0067);

        ///<summary>(0024,0068) VR=FL VM=1 Localized Deviation From Normal</summary>
        public readonly static DicomTagFL LocalizedDeviationFromNormal = new DicomTagFL(0x0024, 0x0068);

        ///<summary>(0024,0069) VR=LO VM=1 Patient Reliability Indicator</summary>
        public readonly static DicomTagLO PatientReliabilityIndicator = new DicomTagLO(0x0024, 0x0069);

        ///<summary>(0024,0070) VR=FL VM=1 Visual Field Mean Sensitivity</summary>
        public readonly static DicomTagFL VisualFieldMeanSensitivity = new DicomTagFL(0x0024, 0x0070);

        ///<summary>(0024,0071) VR=FL VM=1 Global Deviation Probability</summary>
        public readonly static DicomTagFL GlobalDeviationProbability = new DicomTagFL(0x0024, 0x0071);

        ///<summary>(0024,0072) VR=CS VM=1 Local Deviation Probability Normals Flag</summary>
        public readonly static DicomTagCS LocalDeviationProbabilityNormalsFlag = new DicomTagCS(0x0024, 0x0072);

        ///<summary>(0024,0073) VR=FL VM=1 Localized Deviation Probability</summary>
        public readonly static DicomTagFL LocalizedDeviationProbability = new DicomTagFL(0x0024, 0x0073);

        ///<summary>(0024,0074) VR=CS VM=1 Short Term Fluctuation Calculated</summary>
        public readonly static DicomTagCS ShortTermFluctuationCalculated = new DicomTagCS(0x0024, 0x0074);

        ///<summary>(0024,0075) VR=FL VM=1 Short Term Fluctuation</summary>
        public readonly static DicomTagFL ShortTermFluctuation = new DicomTagFL(0x0024, 0x0075);

        ///<summary>(0024,0076) VR=CS VM=1 Short Term Fluctuation Probability Calculated</summary>
        public readonly static DicomTagCS ShortTermFluctuationProbabilityCalculated = new DicomTagCS(0x0024, 0x0076);

        ///<summary>(0024,0077) VR=FL VM=1 Short Term Fluctuation Probability</summary>
        public readonly static DicomTagFL ShortTermFluctuationProbability = new DicomTagFL(0x0024, 0x0077);

        ///<summary>(0024,0078) VR=CS VM=1 Corrected Localized Deviation From Normal Calculated</summary>
        public readonly static DicomTagCS CorrectedLocalizedDeviationFromNormalCalculated = new DicomTagCS(0x0024, 0x0078);

        ///<summary>(0024,0079) VR=FL VM=1 Corrected Localized Deviation From Normal</summary>
        public readonly static DicomTagFL CorrectedLocalizedDeviationFromNormal = new DicomTagFL(0x0024, 0x0079);

        ///<summary>(0024,0080) VR=CS VM=1 Corrected Localized Deviation From Normal Probability Calculated</summary>
        public readonly static DicomTagCS CorrectedLocalizedDeviationFromNormalProbabilityCalculated = new DicomTagCS(0x0024, 0x0080);

        ///<summary>(0024,0081) VR=FL VM=1 Corrected Localized Deviation From Normal Probability</summary>
        public readonly static DicomTagFL CorrectedLocalizedDeviationFromNormalProbability = new DicomTagFL(0x0024, 0x0081);

        ///<summary>(0024,0083) VR=SQ VM=1 Global Deviation Probability Sequence</summary>
        public readonly static DicomTagSQ GlobalDeviationProbabilitySequence = new DicomTagSQ(0x0024, 0x0083);

        ///<summary>(0024,0085) VR=SQ VM=1 Localized Deviation Probability Sequence</summary>
        public readonly static DicomTagSQ LocalizedDeviationProbabilitySequence = new DicomTagSQ(0x0024, 0x0085);

        ///<summary>(0024,0086) VR=CS VM=1 Foveal Sensitivity Measured</summary>
        public readonly static DicomTagCS FovealSensitivityMeasured = new DicomTagCS(0x0024, 0x0086);

        ///<summary>(0024,0087) VR=FL VM=1 Foveal Sensitivity</summary>
        public readonly static DicomTagFL FovealSensitivity = new DicomTagFL(0x0024, 0x0087);

        ///<summary>(0024,0088) VR=FL VM=1 Visual Field Test Duration</summary>
        public readonly static DicomTagFL VisualFieldTestDuration = new DicomTagFL(0x0024, 0x0088);

        ///<summary>(0024,0089) VR=SQ VM=1 Visual Field Test Point Sequence</summary>
        public readonly static DicomTagSQ VisualFieldTestPointSequence = new DicomTagSQ(0x0024, 0x0089);

        ///<summary>(0024,0090) VR=FL VM=1 Visual Field Test Point X-Coordinate</summary>
        public readonly static DicomTagFL VisualFieldTestPointXCoordinate = new DicomTagFL(0x0024, 0x0090);

        ///<summary>(0024,0091) VR=FL VM=1 Visual Field Test Point Y-Coordinate</summary>
        public readonly static DicomTagFL VisualFieldTestPointYCoordinate = new DicomTagFL(0x0024, 0x0091);

        ///<summary>(0024,0092) VR=FL VM=1 Age Corrected Sensitivity Deviation Value</summary>
        public readonly static DicomTagFL AgeCorrectedSensitivityDeviationValue = new DicomTagFL(0x0024, 0x0092);

        ///<summary>(0024,0093) VR=CS VM=1 Stimulus Results</summary>
        public readonly static DicomTagCS StimulusResults = new DicomTagCS(0x0024, 0x0093);

        ///<summary>(0024,0094) VR=FL VM=1 Sensitivity Value</summary>
        public readonly static DicomTagFL SensitivityValue = new DicomTagFL(0x0024, 0x0094);

        ///<summary>(0024,0095) VR=CS VM=1 Retest Stimulus Seen</summary>
        public readonly static DicomTagCS RetestStimulusSeen = new DicomTagCS(0x0024, 0x0095);

        ///<summary>(0024,0096) VR=FL VM=1 Retest Sensitivity Value</summary>
        public readonly static DicomTagFL RetestSensitivityValue = new DicomTagFL(0x0024, 0x0096);

        ///<summary>(0024,0097) VR=SQ VM=1 Visual Field Test Point Normals Sequence</summary>
        public readonly static DicomTagSQ VisualFieldTestPointNormalsSequence = new DicomTagSQ(0x0024, 0x0097);

        ///<summary>(0024,0098) VR=FL VM=1 Quantified Defect</summary>
        public readonly static DicomTagFL QuantifiedDefect = new DicomTagFL(0x0024, 0x0098);

        ///<summary>(0024,0100) VR=FL VM=1 Age Corrected Sensitivity Deviation Probability Value</summary>
        public readonly static DicomTagFL AgeCorrectedSensitivityDeviationProbabilityValue = new DicomTagFL(0x0024, 0x0100);

        ///<summary>(0024,0102) VR=CS VM=1 Generalized Defect Corrected Sensitivity Deviation Flag</summary>
        public readonly static DicomTagCS GeneralizedDefectCorrectedSensitivityDeviationFlag = new DicomTagCS(0x0024, 0x0102);

        ///<summary>(0024,0103) VR=FL VM=1 Generalized Defect Corrected Sensitivity Deviation Value</summary>
        public readonly static DicomTagFL GeneralizedDefectCorrectedSensitivityDeviationValue = new DicomTagFL(0x0024, 0x0103);

        ///<summary>(0024,0104) VR=FL VM=1 Generalized Defect Corrected Sensitivity Deviation Probability Value</summary>
        public readonly static DicomTagFL GeneralizedDefectCorrectedSensitivityDeviationProbabilityValue = new DicomTagFL(0x0024, 0x0104);

        ///<summary>(0024,0105) VR=FL VM=1 Minimum Sensitivity Value</summary>
        public readonly static DicomTagFL MinimumSensitivityValue = new DicomTagFL(0x0024, 0x0105);

        ///<summary>(0024,0106) VR=CS VM=1 Blind Spot Localized</summary>
        public readonly static DicomTagCS BlindSpotLocalized = new DicomTagCS(0x0024, 0x0106);

        ///<summary>(0024,0107) VR=FL VM=1 Blind Spot X-Coordinate</summary>
        public readonly static DicomTagFL BlindSpotXCoordinate = new DicomTagFL(0x0024, 0x0107);

        ///<summary>(0024,0108) VR=FL VM=1 Blind Spot Y-Coordinate</summary>
        public readonly static DicomTagFL BlindSpotYCoordinate = new DicomTagFL(0x0024, 0x0108);

        ///<summary>(0024,0110) VR=SQ VM=1 Visual Acuity Measurement Sequence</summary>
        public readonly static DicomTagSQ VisualAcuityMeasurementSequence = new DicomTagSQ(0x0024, 0x0110);

        ///<summary>(0024,0112) VR=SQ VM=1 Refractive Parameters Used on Patient Sequence</summary>
        public readonly static DicomTagSQ RefractiveParametersUsedOnPatientSequence = new DicomTagSQ(0x0024, 0x0112);

        ///<summary>(0024,0113) VR=CS VM=1 Measurement Laterality</summary>
        public readonly static DicomTagCS MeasurementLaterality = new DicomTagCS(0x0024, 0x0113);

        ///<summary>(0024,0114) VR=SQ VM=1 Ophthalmic Patient Clinical Information Left Eye Sequence</summary>
        public readonly static DicomTagSQ OphthalmicPatientClinicalInformationLeftEyeSequence = new DicomTagSQ(0x0024, 0x0114);

        ///<summary>(0024,0115) VR=SQ VM=1 Ophthalmic Patient Clinical Information Right Eye Sequence</summary>
        public readonly static DicomTagSQ OphthalmicPatientClinicalInformationRightEyeSequence = new DicomTagSQ(0x0024, 0x0115);

        ///<summary>(0024,0117) VR=CS VM=1 Foveal Point Normative Data Flag</summary>
        public readonly static DicomTagCS FovealPointNormativeDataFlag = new DicomTagCS(0x0024, 0x0117);

        ///<summary>(0024,0118) VR=FL VM=1 Foveal Point Probability Value</summary>
        public readonly static DicomTagFL FovealPointProbabilityValue = new DicomTagFL(0x0024, 0x0118);

        ///<summary>(0024,0120) VR=CS VM=1 Screening Baseline Measured</summary>
        public readonly static DicomTagCS ScreeningBaselineMeasured = new DicomTagCS(0x0024, 0x0120);

        ///<summary>(0024,0122) VR=SQ VM=1 Screening Baseline Measured Sequence</summary>
        public readonly static DicomTagSQ ScreeningBaselineMeasuredSequence = new DicomTagSQ(0x0024, 0x0122);

        ///<summary>(0024,0124) VR=CS VM=1 Screening Baseline Type</summary>
        public readonly static DicomTagCS ScreeningBaselineType = new DicomTagCS(0x0024, 0x0124);

        ///<summary>(0024,0126) VR=FL VM=1 Screening Baseline Value</summary>
        public readonly static DicomTagFL ScreeningBaselineValue = new DicomTagFL(0x0024, 0x0126);

        ///<summary>(0024,0202) VR=LO VM=1 Algorithm Source</summary>
        public readonly static DicomTagLO AlgorithmSource = new DicomTagLO(0x0024, 0x0202);

        ///<summary>(0024,0306) VR=LO VM=1 Data Set Name</summary>
        public readonly static DicomTagLO DataSetName = new DicomTagLO(0x0024, 0x0306);

        ///<summary>(0024,0307) VR=LO VM=1 Data Set Version</summary>
        public readonly static DicomTagLO DataSetVersion = new DicomTagLO(0x0024, 0x0307);

        ///<summary>(0024,0308) VR=LO VM=1 Data Set Source</summary>
        public readonly static DicomTagLO DataSetSource = new DicomTagLO(0x0024, 0x0308);

        ///<summary>(0024,0309) VR=LO VM=1 Data Set Description</summary>
        public readonly static DicomTagLO DataSetDescription = new DicomTagLO(0x0024, 0x0309);

        ///<summary>(0024,0317) VR=SQ VM=1 Visual Field Test Reliability Global Index Sequence</summary>
        public readonly static DicomTagSQ VisualFieldTestReliabilityGlobalIndexSequence = new DicomTagSQ(0x0024, 0x0317);

        ///<summary>(0024,0320) VR=SQ VM=1 Visual Field Global Results Index Sequence</summary>
        public readonly static DicomTagSQ VisualFieldGlobalResultsIndexSequence = new DicomTagSQ(0x0024, 0x0320);

        ///<summary>(0024,0325) VR=SQ VM=1 Data Observation Sequence</summary>
        public readonly static DicomTagSQ DataObservationSequence = new DicomTagSQ(0x0024, 0x0325);

        ///<summary>(0024,0338) VR=CS VM=1 Index Normals Flag</summary>
        public readonly static DicomTagCS IndexNormalsFlag = new DicomTagCS(0x0024, 0x0338);

        ///<summary>(0024,0341) VR=FL VM=1 Index Probability</summary>
        public readonly static DicomTagFL IndexProbability = new DicomTagFL(0x0024, 0x0341);

        ///<summary>(0024,0344) VR=SQ VM=1 Index Probability Sequence</summary>
        public readonly static DicomTagSQ IndexProbabilitySequence = new DicomTagSQ(0x0024, 0x0344);

        ///<summary>(0028,0002) VR=US VM=1 Samples per Pixel</summary>
        public readonly static DicomTagUS SamplesPerPixel = new DicomTagUS(0x0028, 0x0002);

        ///<summary>(0028,0003) VR=US VM=1 Samples per Pixel Used</summary>
        public readonly static DicomTagUS SamplesPerPixelUsed = new DicomTagUS(0x0028, 0x0003);

        ///<summary>(0028,0004) VR=CS VM=1 Photometric Interpretation</summary>
        public readonly static DicomTagCS PhotometricInterpretation = new DicomTagCS(0x0028, 0x0004);

        ///<summary>(0028,0005) VR=US VM=1 Image Dimensions (RETIRED)</summary>
        public readonly static DicomTagUS ImageDimensionsRETIRED = new DicomTagUS(0x0028, 0x0005);

        ///<summary>(0028,0006) VR=US VM=1 Planar Configuration</summary>
        public readonly static DicomTagUS PlanarConfiguration = new DicomTagUS(0x0028, 0x0006);

        ///<summary>(0028,0008) VR=IS VM=1 Number of Frames</summary>
        public readonly static DicomTagIS NumberOfFrames = new DicomTagIS(0x0028, 0x0008);

        ///<summary>(0028,0009) VR=AT VM=1-n Frame Increment Pointer</summary>
        public readonly static DicomTagATs FrameIncrementPointer = new DicomTagATs(0x0028, 0x0009);

        ///<summary>(0028,000A) VR=AT VM=1-n Frame Dimension Pointer</summary>
        public readonly static DicomTagATs FrameDimensionPointer = new DicomTagATs(0x0028, 0x000A);

        ///<summary>(0028,0010) VR=US VM=1 Rows</summary>
        public readonly static DicomTagUS Rows = new DicomTagUS(0x0028, 0x0010);

        ///<summary>(0028,0011) VR=US VM=1 Columns</summary>
        public readonly static DicomTagUS Columns = new DicomTagUS(0x0028, 0x0011);

        ///<summary>(0028,0012) VR=US VM=1 Planes (RETIRED)</summary>
        public readonly static DicomTagUS PlanesRETIRED = new DicomTagUS(0x0028, 0x0012);

        ///<summary>(0028,0014) VR=US VM=1 Ultrasound Color Data Present</summary>
        public readonly static DicomTagUS UltrasoundColorDataPresent = new DicomTagUS(0x0028, 0x0014);

        ///<summary>(0028,0030) VR=DS VM=2 Pixel Spacing</summary>
        public readonly static DicomTagDSs PixelSpacing = new DicomTagDSs(0x0028, 0x0030);

        ///<summary>(0028,0031) VR=DS VM=2 Zoom Factor</summary>
        public readonly static DicomTagDSs ZoomFactor = new DicomTagDSs(0x0028, 0x0031);

        ///<summary>(0028,0032) VR=DS VM=2 Zoom Center</summary>
        public readonly static DicomTagDSs ZoomCenter = new DicomTagDSs(0x0028, 0x0032);

        ///<summary>(0028,0034) VR=IS VM=2 Pixel Aspect Ratio</summary>
        public readonly static DicomTagISs PixelAspectRatio = new DicomTagISs(0x0028, 0x0034);

        ///<summary>(0028,0040) VR=CS VM=1 Image Format (RETIRED)</summary>
        public readonly static DicomTagCS ImageFormatRETIRED = new DicomTagCS(0x0028, 0x0040);

        ///<summary>(0028,0050) VR=LO VM=1-n Manipulated Image (RETIRED)</summary>
        public readonly static DicomTagLOs ManipulatedImageRETIRED = new DicomTagLOs(0x0028, 0x0050);

        ///<summary>(0028,0051) VR=CS VM=1-n Corrected Image</summary>
        public readonly static DicomTagCSs CorrectedImage = new DicomTagCSs(0x0028, 0x0051);

        ///<summary>(0028,005F) VR=LO VM=1 Compression Recognition Code (RETIRED)</summary>
        public readonly static DicomTagLO CompressionRecognitionCodeRETIRED = new DicomTagLO(0x0028, 0x005F);

        ///<summary>(0028,0060) VR=CS VM=1 Compression Code (RETIRED)</summary>
        public readonly static DicomTagCS CompressionCodeRETIRED = new DicomTagCS(0x0028, 0x0060);

        ///<summary>(0028,0061) VR=SH VM=1 Compression Originator (RETIRED)</summary>
        public readonly static DicomTagSH CompressionOriginatorRETIRED = new DicomTagSH(0x0028, 0x0061);

        ///<summary>(0028,0062) VR=LO VM=1 Compression Label (RETIRED)</summary>
        public readonly static DicomTagLO CompressionLabelRETIRED = new DicomTagLO(0x0028, 0x0062);

        ///<summary>(0028,0063) VR=SH VM=1 Compression Description (RETIRED)</summary>
        public readonly static DicomTagSH CompressionDescriptionRETIRED = new DicomTagSH(0x0028, 0x0063);

        ///<summary>(0028,0065) VR=CS VM=1-n Compression Sequence (RETIRED)</summary>
        public readonly static DicomTagCSs CompressionSequenceRETIRED = new DicomTagCSs(0x0028, 0x0065);

        ///<summary>(0028,0066) VR=AT VM=1-n Compression Step Pointers (RETIRED)</summary>
        public readonly static DicomTagATs CompressionStepPointersRETIRED = new DicomTagATs(0x0028, 0x0066);

        ///<summary>(0028,0068) VR=US VM=1 Repeat Interval (RETIRED)</summary>
        public readonly static DicomTagUS RepeatIntervalRETIRED = new DicomTagUS(0x0028, 0x0068);

        ///<summary>(0028,0069) VR=US VM=1 Bits Grouped (RETIRED)</summary>
        public readonly static DicomTagUS BitsGroupedRETIRED = new DicomTagUS(0x0028, 0x0069);

        ///<summary>(0028,0070) VR=US VM=1-n Perimeter Table (RETIRED)</summary>
        public readonly static DicomTagUSs PerimeterTableRETIRED = new DicomTagUSs(0x0028, 0x0070);

        ///<summary>(0028,0071) VR=US/SS VM=1 Perimeter Value (RETIRED)</summary>
        public readonly static DicomTagUSSS PerimeterValueRETIRED = new DicomTagUSSS(0x0028, 0x0071);

        ///<summary>(0028,0080) VR=US VM=1 Predictor Rows (RETIRED)</summary>
        public readonly static DicomTagUS PredictorRowsRETIRED = new DicomTagUS(0x0028, 0x0080);

        ///<summary>(0028,0081) VR=US VM=1 Predictor Columns (RETIRED)</summary>
        public readonly static DicomTagUS PredictorColumnsRETIRED = new DicomTagUS(0x0028, 0x0081);

        ///<summary>(0028,0082) VR=US VM=1-n Predictor Constants (RETIRED)</summary>
        public readonly static DicomTagUSs PredictorConstantsRETIRED = new DicomTagUSs(0x0028, 0x0082);

        ///<summary>(0028,0090) VR=CS VM=1 Blocked Pixels (RETIRED)</summary>
        public readonly static DicomTagCS BlockedPixelsRETIRED = new DicomTagCS(0x0028, 0x0090);

        ///<summary>(0028,0091) VR=US VM=1 Block Rows (RETIRED)</summary>
        public readonly static DicomTagUS BlockRowsRETIRED = new DicomTagUS(0x0028, 0x0091);

        ///<summary>(0028,0092) VR=US VM=1 Block Columns (RETIRED)</summary>
        public readonly static DicomTagUS BlockColumnsRETIRED = new DicomTagUS(0x0028, 0x0092);

        ///<summary>(0028,0093) VR=US VM=1 Row Overlap (RETIRED)</summary>
        public readonly static DicomTagUS RowOverlapRETIRED = new DicomTagUS(0x0028, 0x0093);

        ///<summary>(0028,0094) VR=US VM=1 Column Overlap (RETIRED)</summary>
        public readonly static DicomTagUS ColumnOverlapRETIRED = new DicomTagUS(0x0028, 0x0094);

        ///<summary>(0028,0100) VR=US VM=1 Bits Allocated</summary>
        public readonly static DicomTagUS BitsAllocated = new DicomTagUS(0x0028, 0x0100);

        ///<summary>(0028,0101) VR=US VM=1 Bits Stored</summary>
        public readonly static DicomTagUS BitsStored = new DicomTagUS(0x0028, 0x0101);

        ///<summary>(0028,0102) VR=US VM=1 High Bit</summary>
        public readonly static DicomTagUS HighBit = new DicomTagUS(0x0028, 0x0102);

        ///<summary>(0028,0103) VR=US VM=1 Pixel Representation</summary>
        public readonly static DicomTagUS PixelRepresentation = new DicomTagUS(0x0028, 0x0103);

        ///<summary>(0028,0104) VR=US/SS VM=1 Smallest Valid Pixel Value (RETIRED)</summary>
        public readonly static DicomTagUSSS SmallestValidPixelValueRETIRED = new DicomTagUSSS(0x0028, 0x0104);

        ///<summary>(0028,0105) VR=US/SS VM=1 Largest Valid Pixel Value (RETIRED)</summary>
        public readonly static DicomTagUSSS LargestValidPixelValueRETIRED = new DicomTagUSSS(0x0028, 0x0105);

        ///<summary>(0028,0106) VR=US/SS VM=1 Smallest Image Pixel Value</summary>
        public readonly static DicomTagUSSS SmallestImagePixelValue = new DicomTagUSSS(0x0028, 0x0106);

        ///<summary>(0028,0107) VR=US/SS VM=1 Largest Image Pixel Value</summary>
        public readonly static DicomTagUSSS LargestImagePixelValue = new DicomTagUSSS(0x0028, 0x0107);

        ///<summary>(0028,0108) VR=US/SS VM=1 Smallest Pixel Value in Series</summary>
        public readonly static DicomTagUSSS SmallestPixelValueInSeries = new DicomTagUSSS(0x0028, 0x0108);

        ///<summary>(0028,0109) VR=US/SS VM=1 Largest Pixel Value in Series</summary>
        public readonly static DicomTagUSSS LargestPixelValueInSeries = new DicomTagUSSS(0x0028, 0x0109);

        ///<summary>(0028,0110) VR=US/SS VM=1 Smallest Image Pixel Value in Plane (RETIRED)</summary>
        public readonly static DicomTagUSSS SmallestImagePixelValueInPlaneRETIRED = new DicomTagUSSS(0x0028, 0x0110);

        ///<summary>(0028,0111) VR=US/SS VM=1 Largest Image Pixel Value in Plane (RETIRED)</summary>
        public readonly static DicomTagUSSS LargestImagePixelValueInPlaneRETIRED = new DicomTagUSSS(0x0028, 0x0111);

        ///<summary>(0028,0120) VR=US/SS VM=1 Pixel Padding Value</summary>
        public readonly static DicomTagUSSS PixelPaddingValue = new DicomTagUSSS(0x0028, 0x0120);

        ///<summary>(0028,0121) VR=US/SS VM=1 Pixel Padding Range Limit</summary>
        public readonly static DicomTagUSSS PixelPaddingRangeLimit = new DicomTagUSSS(0x0028, 0x0121);

        ///<summary>(0028,0122) VR=FL VM=1 Float Pixel Padding Value</summary>
        public readonly static DicomTagFL FloatPixelPaddingValue = new DicomTagFL(0x0028, 0x0122);

        ///<summary>(0028,0123) VR=FD VM=1 Double Float Pixel Padding Value</summary>
        public readonly static DicomTagFD DoubleFloatPixelPaddingValue = new DicomTagFD(0x0028, 0x0123);

        ///<summary>(0028,0124) VR=FL VM=1 Float Pixel Padding Range Limit</summary>
        public readonly static DicomTagFL FloatPixelPaddingRangeLimit = new DicomTagFL(0x0028, 0x0124);

        ///<summary>(0028,0125) VR=FD VM=1 Double Float Pixel Padding Range Limit</summary>
        public readonly static DicomTagFD DoubleFloatPixelPaddingRangeLimit = new DicomTagFD(0x0028, 0x0125);

        ///<summary>(0028,0200) VR=US VM=1 Image Location (RETIRED)</summary>
        public readonly static DicomTagUS ImageLocationRETIRED = new DicomTagUS(0x0028, 0x0200);

        ///<summary>(0028,0300) VR=CS VM=1 Quality Control Image</summary>
        public readonly static DicomTagCS QualityControlImage = new DicomTagCS(0x0028, 0x0300);

        ///<summary>(0028,0301) VR=CS VM=1 Burned In Annotation</summary>
        public readonly static DicomTagCS BurnedInAnnotation = new DicomTagCS(0x0028, 0x0301);

        ///<summary>(0028,0302) VR=CS VM=1 Recognizable Visual Features</summary>
        public readonly static DicomTagCS RecognizableVisualFeatures = new DicomTagCS(0x0028, 0x0302);

        ///<summary>(0028,0303) VR=CS VM=1 Longitudinal Temporal Information Modified</summary>
        public readonly static DicomTagCS LongitudinalTemporalInformationModified = new DicomTagCS(0x0028, 0x0303);

        ///<summary>(0028,0304) VR=UI VM=1 Referenced Color Palette Instance UID</summary>
        public readonly static DicomTagUI ReferencedColorPaletteInstanceUID = new DicomTagUI(0x0028, 0x0304);

        ///<summary>(0028,0400) VR=LO VM=1 Transform Label (RETIRED)</summary>
        public readonly static DicomTagLO TransformLabelRETIRED = new DicomTagLO(0x0028, 0x0400);

        ///<summary>(0028,0401) VR=LO VM=1 Transform Version Number (RETIRED)</summary>
        public readonly static DicomTagLO TransformVersionNumberRETIRED = new DicomTagLO(0x0028, 0x0401);

        ///<summary>(0028,0402) VR=US VM=1 Number of Transform Steps (RETIRED)</summary>
        public readonly static DicomTagUS NumberOfTransformStepsRETIRED = new DicomTagUS(0x0028, 0x0402);

        ///<summary>(0028,0403) VR=LO VM=1-n Sequence of Compressed Data (RETIRED)</summary>
        public readonly static DicomTagLOs SequenceOfCompressedDataRETIRED = new DicomTagLOs(0x0028, 0x0403);

        ///<summary>(0028,0404) VR=AT VM=1-n Details of Coefficients (RETIRED)</summary>
        public readonly static DicomTagATs DetailsOfCoefficientsRETIRED = new DicomTagATs(0x0028, 0x0404);

        ///<summary>(0028,04x0) VR=US VM=1 Rows For Nth Order Coefficients (RETIRED)</summary>
        public readonly static DicomTagUS RowsForNthOrderCoefficientsRETIRED = new DicomTagUS(0x0028, 0x0400);

        ///<summary>(0028,04x1) VR=US VM=1 Columns For Nth Order Coefficients (RETIRED)</summary>
        public readonly static DicomTagUS ColumnsForNthOrderCoefficientsRETIRED = new DicomTagUS(0x0028, 0x0401);

        ///<summary>(0028,04x2) VR=LO VM=1-n Coefficient Coding (RETIRED)</summary>
        public readonly static DicomTagLOs CoefficientCodingRETIRED = new DicomTagLOs(0x0028, 0x0402);

        ///<summary>(0028,04x3) VR=AT VM=1-n Coefficient Coding Pointers (RETIRED)</summary>
        public readonly static DicomTagATs CoefficientCodingPointersRETIRED = new DicomTagATs(0x0028, 0x0403);

        ///<summary>(0028,0700) VR=LO VM=1 DCT Label (RETIRED)</summary>
        public readonly static DicomTagLO DCTLabelRETIRED = new DicomTagLO(0x0028, 0x0700);

        ///<summary>(0028,0701) VR=CS VM=1-n Data Block Description (RETIRED)</summary>
        public readonly static DicomTagCSs DataBlockDescriptionRETIRED = new DicomTagCSs(0x0028, 0x0701);

        ///<summary>(0028,0702) VR=AT VM=1-n Data Block (RETIRED)</summary>
        public readonly static DicomTagATs DataBlockRETIRED = new DicomTagATs(0x0028, 0x0702);

        ///<summary>(0028,0710) VR=US VM=1 Normalization Factor Format (RETIRED)</summary>
        public readonly static DicomTagUS NormalizationFactorFormatRETIRED = new DicomTagUS(0x0028, 0x0710);

        ///<summary>(0028,0720) VR=US VM=1 Zonal Map Number Format (RETIRED)</summary>
        public readonly static DicomTagUS ZonalMapNumberFormatRETIRED = new DicomTagUS(0x0028, 0x0720);

        ///<summary>(0028,0721) VR=AT VM=1-n Zonal Map Location (RETIRED)</summary>
        public readonly static DicomTagATs ZonalMapLocationRETIRED = new DicomTagATs(0x0028, 0x0721);

        ///<summary>(0028,0722) VR=US VM=1 Zonal Map Format (RETIRED)</summary>
        public readonly static DicomTagUS ZonalMapFormatRETIRED = new DicomTagUS(0x0028, 0x0722);

        ///<summary>(0028,0730) VR=US VM=1 Adaptive Map Format (RETIRED)</summary>
        public readonly static DicomTagUS AdaptiveMapFormatRETIRED = new DicomTagUS(0x0028, 0x0730);

        ///<summary>(0028,0740) VR=US VM=1 Code Number Format (RETIRED)</summary>
        public readonly static DicomTagUS CodeNumberFormatRETIRED = new DicomTagUS(0x0028, 0x0740);

        ///<summary>(0028,08x0) VR=CS VM=1-n Code Label (RETIRED)</summary>
        public readonly static DicomTagCSs CodeLabelRETIRED = new DicomTagCSs(0x0028, 0x0800);

        ///<summary>(0028,08x2) VR=US VM=1 Number of Tables (RETIRED)</summary>
        public readonly static DicomTagUS NumberOfTablesRETIRED = new DicomTagUS(0x0028, 0x0802);

        ///<summary>(0028,08x3) VR=AT VM=1-n Code Table Location (RETIRED)</summary>
        public readonly static DicomTagATs CodeTableLocationRETIRED = new DicomTagATs(0x0028, 0x0803);

        ///<summary>(0028,08x4) VR=US VM=1 Bits For Code Word (RETIRED)</summary>
        public readonly static DicomTagUS BitsForCodeWordRETIRED = new DicomTagUS(0x0028, 0x0804);

        ///<summary>(0028,08x8) VR=AT VM=1-n Image Data Location (RETIRED)</summary>
        public readonly static DicomTagATs ImageDataLocationRETIRED = new DicomTagATs(0x0028, 0x0808);

        ///<summary>(0028,0A02) VR=CS VM=1 Pixel Spacing Calibration Type</summary>
        public readonly static DicomTagCS PixelSpacingCalibrationType = new DicomTagCS(0x0028, 0x0A02);

        ///<summary>(0028,0A04) VR=LO VM=1 Pixel Spacing Calibration Description</summary>
        public readonly static DicomTagLO PixelSpacingCalibrationDescription = new DicomTagLO(0x0028, 0x0A04);

        ///<summary>(0028,1040) VR=CS VM=1 Pixel Intensity Relationship</summary>
        public readonly static DicomTagCS PixelIntensityRelationship = new DicomTagCS(0x0028, 0x1040);

        ///<summary>(0028,1041) VR=SS VM=1 Pixel Intensity Relationship Sign</summary>
        public readonly static DicomTagSS PixelIntensityRelationshipSign = new DicomTagSS(0x0028, 0x1041);

        ///<summary>(0028,1050) VR=DS VM=1-n Window Center</summary>
        public readonly static DicomTagDSs WindowCenter = new DicomTagDSs(0x0028, 0x1050);

        ///<summary>(0028,1051) VR=DS VM=1-n Window Width</summary>
        public readonly static DicomTagDSs WindowWidth = new DicomTagDSs(0x0028, 0x1051);

        ///<summary>(0028,1052) VR=DS VM=1 Rescale Intercept</summary>
        public readonly static DicomTagDS RescaleIntercept = new DicomTagDS(0x0028, 0x1052);

        ///<summary>(0028,1053) VR=DS VM=1 Rescale Slope</summary>
        public readonly static DicomTagDS RescaleSlope = new DicomTagDS(0x0028, 0x1053);

        ///<summary>(0028,1054) VR=LO VM=1 Rescale Type</summary>
        public readonly static DicomTagLO RescaleType = new DicomTagLO(0x0028, 0x1054);

        ///<summary>(0028,1055) VR=LO VM=1-n Window Center &amp; Width Explanation</summary>
        public readonly static DicomTagLOs WindowCenterWidthExplanation = new DicomTagLOs(0x0028, 0x1055);

        ///<summary>(0028,1056) VR=CS VM=1 VOI LUT Function</summary>
        public readonly static DicomTagCS VOILUTFunction = new DicomTagCS(0x0028, 0x1056);

        ///<summary>(0028,1080) VR=CS VM=1 Gray Scale (RETIRED)</summary>
        public readonly static DicomTagCS GrayScaleRETIRED = new DicomTagCS(0x0028, 0x1080);

        ///<summary>(0028,1090) VR=CS VM=1 Recommended Viewing Mode</summary>
        public readonly static DicomTagCS RecommendedViewingMode = new DicomTagCS(0x0028, 0x1090);

        ///<summary>(0028,1100) VR=US/SS VM=3 Gray Lookup Table Descriptor (RETIRED)</summary>
        public readonly static DicomTagUSSSs GrayLookupTableDescriptorRETIRED = new DicomTagUSSSs(0x0028, 0x1100);

        ///<summary>(0028,1101) VR=US/SS VM=3 Red Palette Color Lookup Table Descriptor</summary>
        public readonly static DicomTagUSSSs RedPaletteColorLookupTableDescriptor = new DicomTagUSSSs(0x0028, 0x1101);

        ///<summary>(0028,1102) VR=US/SS VM=3 Green Palette Color Lookup Table Descriptor</summary>
        public readonly static DicomTagUSSSs GreenPaletteColorLookupTableDescriptor = new DicomTagUSSSs(0x0028, 0x1102);

        ///<summary>(0028,1103) VR=US/SS VM=3 Blue Palette Color Lookup Table Descriptor</summary>
        public readonly static DicomTagUSSSs BluePaletteColorLookupTableDescriptor = new DicomTagUSSSs(0x0028, 0x1103);

        ///<summary>(0028,1104) VR=US VM=3 Alpha Palette Color Lookup Table Descriptor</summary>
        public readonly static DicomTagUSs AlphaPaletteColorLookupTableDescriptor = new DicomTagUSs(0x0028, 0x1104);

        ///<summary>(0028,1111) VR=US/SS VM=4 Large Red Palette Color Lookup Table Descriptor (RETIRED)</summary>
        public readonly static DicomTagUSSSs LargeRedPaletteColorLookupTableDescriptorRETIRED = new DicomTagUSSSs(0x0028, 0x1111);

        ///<summary>(0028,1112) VR=US/SS VM=4 Large Green Palette Color Lookup Table Descriptor (RETIRED)</summary>
        public readonly static DicomTagUSSSs LargeGreenPaletteColorLookupTableDescriptorRETIRED = new DicomTagUSSSs(0x0028, 0x1112);

        ///<summary>(0028,1113) VR=US/SS VM=4 Large Blue Palette Color Lookup Table Descriptor (RETIRED)</summary>
        public readonly static DicomTagUSSSs LargeBluePaletteColorLookupTableDescriptorRETIRED = new DicomTagUSSSs(0x0028, 0x1113);

        ///<summary>(0028,1199) VR=UI VM=1 Palette Color Lookup Table UID</summary>
        public readonly static DicomTagUI PaletteColorLookupTableUID = new DicomTagUI(0x0028, 0x1199);

        ///<summary>(0028,1200) VR=US/SS/OW VM=1-n or 1 Gray Lookup Table Data (RETIRED)</summary>
        public readonly static DicomTagUSSSOWs GrayLookupTableDataRETIRED = new DicomTagUSSSOWs(0x0028, 0x1200);

        ///<summary>(0028,1201) VR=OW VM=1 Red Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW RedPaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1201);

        ///<summary>(0028,1202) VR=OW VM=1 Green Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW GreenPaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1202);

        ///<summary>(0028,1203) VR=OW VM=1 Blue Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW BluePaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1203);

        ///<summary>(0028,1204) VR=OW VM=1 Alpha Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW AlphaPaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1204);

        ///<summary>(0028,1211) VR=OW VM=1 Large Red Palette Color Lookup Table Data (RETIRED)</summary>
        public readonly static DicomTagOW LargeRedPaletteColorLookupTableDataRETIRED = new DicomTagOW(0x0028, 0x1211);

        ///<summary>(0028,1212) VR=OW VM=1 Large Green Palette Color Lookup Table Data (RETIRED)</summary>
        public readonly static DicomTagOW LargeGreenPaletteColorLookupTableDataRETIRED = new DicomTagOW(0x0028, 0x1212);

        ///<summary>(0028,1213) VR=OW VM=1 Large Blue Palette Color Lookup Table Data (RETIRED)</summary>
        public readonly static DicomTagOW LargeBluePaletteColorLookupTableDataRETIRED = new DicomTagOW(0x0028, 0x1213);

        ///<summary>(0028,1214) VR=UI VM=1 Large Palette Color Lookup Table UID (RETIRED)</summary>
        public readonly static DicomTagUI LargePaletteColorLookupTableUIDRETIRED = new DicomTagUI(0x0028, 0x1214);

        ///<summary>(0028,1221) VR=OW VM=1 Segmented Red Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW SegmentedRedPaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1221);

        ///<summary>(0028,1222) VR=OW VM=1 Segmented Green Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW SegmentedGreenPaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1222);

        ///<summary>(0028,1223) VR=OW VM=1 Segmented Blue Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW SegmentedBluePaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1223);

        ///<summary>(0028,1224) VR=OW VM=1 Segmented Alpha Palette Color Lookup Table Data</summary>
        public readonly static DicomTagOW SegmentedAlphaPaletteColorLookupTableData = new DicomTagOW(0x0028, 0x1224);

        ///<summary>(0028,1230) VR=SQ VM=1 Stored Value Color Range Sequence</summary>
        public readonly static DicomTagSQ StoredValueColorRangeSequence = new DicomTagSQ(0x0028, 0x1230);

        ///<summary>(0028,1231) VR=FD VM=1 Minimum Stored Value Mapped</summary>
        public readonly static DicomTagFD MinimumStoredValueMapped = new DicomTagFD(0x0028, 0x1231);

        ///<summary>(0028,1232) VR=FD VM=1 Maximum Stored Value Mapped</summary>
        public readonly static DicomTagFD MaximumStoredValueMapped = new DicomTagFD(0x0028, 0x1232);

        ///<summary>(0028,1300) VR=CS VM=1 Breast Implant Present</summary>
        public readonly static DicomTagCS BreastImplantPresent = new DicomTagCS(0x0028, 0x1300);

        ///<summary>(0028,1350) VR=CS VM=1 Partial View</summary>
        public readonly static DicomTagCS PartialView = new DicomTagCS(0x0028, 0x1350);

        ///<summary>(0028,1351) VR=ST VM=1 Partial View Description</summary>
        public readonly static DicomTagST PartialViewDescription = new DicomTagST(0x0028, 0x1351);

        ///<summary>(0028,1352) VR=SQ VM=1 Partial View Code Sequence</summary>
        public readonly static DicomTagSQ PartialViewCodeSequence = new DicomTagSQ(0x0028, 0x1352);

        ///<summary>(0028,135A) VR=CS VM=1 Spatial Locations Preserved</summary>
        public readonly static DicomTagCS SpatialLocationsPreserved = new DicomTagCS(0x0028, 0x135A);

        ///<summary>(0028,1401) VR=SQ VM=1 Data Frame Assignment Sequence</summary>
        public readonly static DicomTagSQ DataFrameAssignmentSequence = new DicomTagSQ(0x0028, 0x1401);

        ///<summary>(0028,1402) VR=CS VM=1 Data Path Assignment</summary>
        public readonly static DicomTagCS DataPathAssignment = new DicomTagCS(0x0028, 0x1402);

        ///<summary>(0028,1403) VR=US VM=1 Bits Mapped to Color Lookup Table</summary>
        public readonly static DicomTagUS BitsMappedToColorLookupTable = new DicomTagUS(0x0028, 0x1403);

        ///<summary>(0028,1404) VR=SQ VM=1 Blending LUT 1 Sequence</summary>
        public readonly static DicomTagSQ BlendingLUT1Sequence = new DicomTagSQ(0x0028, 0x1404);

        ///<summary>(0028,1405) VR=CS VM=1 Blending LUT 1 Transfer Function</summary>
        public readonly static DicomTagCS BlendingLUT1TransferFunction = new DicomTagCS(0x0028, 0x1405);

        ///<summary>(0028,1406) VR=FD VM=1 Blending Weight Constant</summary>
        public readonly static DicomTagFD BlendingWeightConstant = new DicomTagFD(0x0028, 0x1406);

        ///<summary>(0028,1407) VR=US VM=3 Blending Lookup Table Descriptor</summary>
        public readonly static DicomTagUSs BlendingLookupTableDescriptor = new DicomTagUSs(0x0028, 0x1407);

        ///<summary>(0028,1408) VR=OW VM=1 Blending Lookup Table Data</summary>
        public readonly static DicomTagOW BlendingLookupTableData = new DicomTagOW(0x0028, 0x1408);

        ///<summary>(0028,140B) VR=SQ VM=1 Enhanced Palette Color Lookup Table Sequence</summary>
        public readonly static DicomTagSQ EnhancedPaletteColorLookupTableSequence = new DicomTagSQ(0x0028, 0x140B);

        ///<summary>(0028,140C) VR=SQ VM=1 Blending LUT 2 Sequence</summary>
        public readonly static DicomTagSQ BlendingLUT2Sequence = new DicomTagSQ(0x0028, 0x140C);

        ///<summary>(0028,140D) VR=CS VM=1 Blending LUT 2 Transfer Function</summary>
        public readonly static DicomTagCS BlendingLUT2TransferFunction = new DicomTagCS(0x0028, 0x140D);

        ///<summary>(0028,140E) VR=CS VM=1 Data Path ID</summary>
        public readonly static DicomTagCS DataPathID = new DicomTagCS(0x0028, 0x140E);

        ///<summary>(0028,140F) VR=CS VM=1 RGB LUT Transfer Function</summary>
        public readonly static DicomTagCS RGBLUTTransferFunction = new DicomTagCS(0x0028, 0x140F);

        ///<summary>(0028,1410) VR=CS VM=1 Alpha LUT Transfer Function</summary>
        public readonly static DicomTagCS AlphaLUTTransferFunction = new DicomTagCS(0x0028, 0x1410);

        ///<summary>(0028,2000) VR=OB VM=1 ICC Profile</summary>
        public readonly static DicomTagOB ICCProfile = new DicomTagOB(0x0028, 0x2000);

        ///<summary>(0028,2002) VR=CS VM=1 Color Space</summary>
        public readonly static DicomTagCS ColorSpace = new DicomTagCS(0x0028, 0x2002);

        ///<summary>(0028,2110) VR=CS VM=1 Lossy Image Compression</summary>
        public readonly static DicomTagCS LossyImageCompression = new DicomTagCS(0x0028, 0x2110);

        ///<summary>(0028,2112) VR=DS VM=1-n Lossy Image Compression Ratio</summary>
        public readonly static DicomTagDSs LossyImageCompressionRatio = new DicomTagDSs(0x0028, 0x2112);

        ///<summary>(0028,2114) VR=CS VM=1-n Lossy Image Compression Method</summary>
        public readonly static DicomTagCSs LossyImageCompressionMethod = new DicomTagCSs(0x0028, 0x2114);

        ///<summary>(0028,3000) VR=SQ VM=1 Modality LUT Sequence</summary>
        public readonly static DicomTagSQ ModalityLUTSequence = new DicomTagSQ(0x0028, 0x3000);

        ///<summary>(0028,3001) VR=SQ VM=1 Variable Modality LUT Sequence</summary>
        public readonly static DicomTagSQ VariableModalityLUTSequence = new DicomTagSQ(0x0028, 0x3001);

        ///<summary>(0028,3002) VR=US/SS VM=3 LUT Descriptor</summary>
        public readonly static DicomTagUSSSs LUTDescriptor = new DicomTagUSSSs(0x0028, 0x3002);

        ///<summary>(0028,3003) VR=LO VM=1 LUT Explanation</summary>
        public readonly static DicomTagLO LUTExplanation = new DicomTagLO(0x0028, 0x3003);

        ///<summary>(0028,3004) VR=LO VM=1 Modality LUT Type</summary>
        public readonly static DicomTagLO ModalityLUTType = new DicomTagLO(0x0028, 0x3004);

        ///<summary>(0028,3006) VR=US/OW VM=1-n or 1 LUT Data</summary>
        public readonly static DicomTagUSOWs LUTData = new DicomTagUSOWs(0x0028, 0x3006);

        ///<summary>(0028,3010) VR=SQ VM=1 VOI LUT Sequence</summary>
        public readonly static DicomTagSQ VOILUTSequence = new DicomTagSQ(0x0028, 0x3010);

        ///<summary>(0028,3110) VR=SQ VM=1 Softcopy VOI LUT Sequence</summary>
        public readonly static DicomTagSQ SoftcopyVOILUTSequence = new DicomTagSQ(0x0028, 0x3110);

        ///<summary>(0028,4000) VR=LT VM=1 Image Presentation Comments (RETIRED)</summary>
        public readonly static DicomTagLT ImagePresentationCommentsRETIRED = new DicomTagLT(0x0028, 0x4000);

        ///<summary>(0028,5000) VR=SQ VM=1 Bi-Plane Acquisition Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ BiPlaneAcquisitionSequenceRETIRED = new DicomTagSQ(0x0028, 0x5000);

        ///<summary>(0028,6010) VR=US VM=1 Representative Frame Number</summary>
        public readonly static DicomTagUS RepresentativeFrameNumber = new DicomTagUS(0x0028, 0x6010);

        ///<summary>(0028,6020) VR=US VM=1-n Frame Numbers of Interest (FOI)</summary>
        public readonly static DicomTagUSs FrameNumbersOfInterest = new DicomTagUSs(0x0028, 0x6020);

        ///<summary>(0028,6022) VR=LO VM=1-n Frame of Interest Description</summary>
        public readonly static DicomTagLOs FrameOfInterestDescription = new DicomTagLOs(0x0028, 0x6022);

        ///<summary>(0028,6023) VR=CS VM=1-n Frame of Interest Type</summary>
        public readonly static DicomTagCSs FrameOfInterestType = new DicomTagCSs(0x0028, 0x6023);

        ///<summary>(0028,6030) VR=US VM=1-n Mask Pointer(s) (RETIRED)</summary>
        public readonly static DicomTagUSs MaskPointersRETIRED = new DicomTagUSs(0x0028, 0x6030);

        ///<summary>(0028,6040) VR=US VM=1-n R Wave Pointer</summary>
        public readonly static DicomTagUSs RWavePointer = new DicomTagUSs(0x0028, 0x6040);

        ///<summary>(0028,6100) VR=SQ VM=1 Mask Subtraction Sequence</summary>
        public readonly static DicomTagSQ MaskSubtractionSequence = new DicomTagSQ(0x0028, 0x6100);

        ///<summary>(0028,6101) VR=CS VM=1 Mask Operation</summary>
        public readonly static DicomTagCS MaskOperation = new DicomTagCS(0x0028, 0x6101);

        ///<summary>(0028,6102) VR=US VM=2-2n Applicable Frame Range</summary>
        public readonly static DicomTagUSs ApplicableFrameRange = new DicomTagUSs(0x0028, 0x6102);

        ///<summary>(0028,6110) VR=US VM=1-n Mask Frame Numbers</summary>
        public readonly static DicomTagUSs MaskFrameNumbers = new DicomTagUSs(0x0028, 0x6110);

        ///<summary>(0028,6112) VR=US VM=1 Contrast Frame Averaging</summary>
        public readonly static DicomTagUS ContrastFrameAveraging = new DicomTagUS(0x0028, 0x6112);

        ///<summary>(0028,6114) VR=FL VM=2 Mask Sub-pixel Shift</summary>
        public readonly static DicomTagFLs MaskSubPixelShift = new DicomTagFLs(0x0028, 0x6114);

        ///<summary>(0028,6120) VR=SS VM=1 TID Offset</summary>
        public readonly static DicomTagSS TIDOffset = new DicomTagSS(0x0028, 0x6120);

        ///<summary>(0028,6190) VR=ST VM=1 Mask Operation Explanation</summary>
        public readonly static DicomTagST MaskOperationExplanation = new DicomTagST(0x0028, 0x6190);

        ///<summary>(0028,7000) VR=SQ VM=1 Equipment Administrator Sequence</summary>
        public readonly static DicomTagSQ EquipmentAdministratorSequence = new DicomTagSQ(0x0028, 0x7000);

        ///<summary>(0028,7001) VR=US VM=1 Number of Display Subsystems</summary>
        public readonly static DicomTagUS NumberOfDisplaySubsystems = new DicomTagUS(0x0028, 0x7001);

        ///<summary>(0028,7002) VR=US VM=1 Current Configuration ID</summary>
        public readonly static DicomTagUS CurrentConfigurationID = new DicomTagUS(0x0028, 0x7002);

        ///<summary>(0028,7003) VR=US VM=1 Display Subsystem ID</summary>
        public readonly static DicomTagUS DisplaySubsystemID = new DicomTagUS(0x0028, 0x7003);

        ///<summary>(0028,7004) VR=SH VM=1 Display Subsystem Name</summary>
        public readonly static DicomTagSH DisplaySubsystemName = new DicomTagSH(0x0028, 0x7004);

        ///<summary>(0028,7005) VR=LO VM=1 Display Subsystem Description</summary>
        public readonly static DicomTagLO DisplaySubsystemDescription = new DicomTagLO(0x0028, 0x7005);

        ///<summary>(0028,7006) VR=CS VM=1 System Status</summary>
        public readonly static DicomTagCS SystemStatus = new DicomTagCS(0x0028, 0x7006);

        ///<summary>(0028,7007) VR=LO VM=1 System Status Comment</summary>
        public readonly static DicomTagLO SystemStatusComment = new DicomTagLO(0x0028, 0x7007);

        ///<summary>(0028,7008) VR=SQ VM=1 Target Luminance Characteristics Sequence</summary>
        public readonly static DicomTagSQ TargetLuminanceCharacteristicsSequence = new DicomTagSQ(0x0028, 0x7008);

        ///<summary>(0028,7009) VR=US VM=1 Luminance Characteristics ID</summary>
        public readonly static DicomTagUS LuminanceCharacteristicsID = new DicomTagUS(0x0028, 0x7009);

        ///<summary>(0028,700A) VR=SQ VM=1 Display Subsystem Configuration Sequence</summary>
        public readonly static DicomTagSQ DisplaySubsystemConfigurationSequence = new DicomTagSQ(0x0028, 0x700A);

        ///<summary>(0028,700B) VR=US VM=1 Configuration ID</summary>
        public readonly static DicomTagUS ConfigurationID = new DicomTagUS(0x0028, 0x700B);

        ///<summary>(0028,700C) VR=SH VM=1 Configuration Name</summary>
        public readonly static DicomTagSH ConfigurationName = new DicomTagSH(0x0028, 0x700C);

        ///<summary>(0028,700D) VR=LO VM=1 Configuration Description</summary>
        public readonly static DicomTagLO ConfigurationDescription = new DicomTagLO(0x0028, 0x700D);

        ///<summary>(0028,700E) VR=US VM=1 Referenced Target Luminance Characteristics ID</summary>
        public readonly static DicomTagUS ReferencedTargetLuminanceCharacteristicsID = new DicomTagUS(0x0028, 0x700E);

        ///<summary>(0028,700F) VR=SQ VM=1 QA Results Sequence</summary>
        public readonly static DicomTagSQ QAResultsSequence = new DicomTagSQ(0x0028, 0x700F);

        ///<summary>(0028,7010) VR=SQ VM=1 Display Subsystem QA Results Sequence</summary>
        public readonly static DicomTagSQ DisplaySubsystemQAResultsSequence = new DicomTagSQ(0x0028, 0x7010);

        ///<summary>(0028,7011) VR=SQ VM=1 Configuration QA Results Sequence</summary>
        public readonly static DicomTagSQ ConfigurationQAResultsSequence = new DicomTagSQ(0x0028, 0x7011);

        ///<summary>(0028,7012) VR=SQ VM=1 Measurement Equipment Sequence</summary>
        public readonly static DicomTagSQ MeasurementEquipmentSequence = new DicomTagSQ(0x0028, 0x7012);

        ///<summary>(0028,7013) VR=CS VM=1-n Measurement Functions</summary>
        public readonly static DicomTagCSs MeasurementFunctions = new DicomTagCSs(0x0028, 0x7013);

        ///<summary>(0028,7014) VR=CS VM=1 Measurement Equipment Type</summary>
        public readonly static DicomTagCS MeasurementEquipmentType = new DicomTagCS(0x0028, 0x7014);

        ///<summary>(0028,7015) VR=SQ VM=1 Visual Evaluation Result Sequence</summary>
        public readonly static DicomTagSQ VisualEvaluationResultSequence = new DicomTagSQ(0x0028, 0x7015);

        ///<summary>(0028,7016) VR=SQ VM=1 Display Calibration Result Sequence</summary>
        public readonly static DicomTagSQ DisplayCalibrationResultSequence = new DicomTagSQ(0x0028, 0x7016);

        ///<summary>(0028,7017) VR=US VM=1 DDL Value</summary>
        public readonly static DicomTagUS DDLValue = new DicomTagUS(0x0028, 0x7017);

        ///<summary>(0028,7018) VR=FL VM=2 CIExy White Point</summary>
        public readonly static DicomTagFLs CIExyWhitePoint = new DicomTagFLs(0x0028, 0x7018);

        ///<summary>(0028,7019) VR=CS VM=1 Display Function Type</summary>
        public readonly static DicomTagCS DisplayFunctionType = new DicomTagCS(0x0028, 0x7019);

        ///<summary>(0028,701A) VR=FL VM=1 Gamma Value</summary>
        public readonly static DicomTagFL GammaValue = new DicomTagFL(0x0028, 0x701A);

        ///<summary>(0028,701B) VR=US VM=1 Number of Luminance Points</summary>
        public readonly static DicomTagUS NumberOfLuminancePoints = new DicomTagUS(0x0028, 0x701B);

        ///<summary>(0028,701C) VR=SQ VM=1 Luminance Response Sequence</summary>
        public readonly static DicomTagSQ LuminanceResponseSequence = new DicomTagSQ(0x0028, 0x701C);

        ///<summary>(0028,701D) VR=FL VM=1 Target Minimum Luminance</summary>
        public readonly static DicomTagFL TargetMinimumLuminance = new DicomTagFL(0x0028, 0x701D);

        ///<summary>(0028,701E) VR=FL VM=1 Target Maximum Luminance</summary>
        public readonly static DicomTagFL TargetMaximumLuminance = new DicomTagFL(0x0028, 0x701E);

        ///<summary>(0028,701F) VR=FL VM=1 Luminance Value</summary>
        public readonly static DicomTagFL LuminanceValue = new DicomTagFL(0x0028, 0x701F);

        ///<summary>(0028,7020) VR=LO VM=1 Luminance Response Description</summary>
        public readonly static DicomTagLO LuminanceResponseDescription = new DicomTagLO(0x0028, 0x7020);

        ///<summary>(0028,7021) VR=CS VM=1 White Point Flag</summary>
        public readonly static DicomTagCS WhitePointFlag = new DicomTagCS(0x0028, 0x7021);

        ///<summary>(0028,7022) VR=SQ VM=1 Display Device Type Code Sequence</summary>
        public readonly static DicomTagSQ DisplayDeviceTypeCodeSequence = new DicomTagSQ(0x0028, 0x7022);

        ///<summary>(0028,7023) VR=SQ VM=1 Display Subsystem Sequence</summary>
        public readonly static DicomTagSQ DisplaySubsystemSequence = new DicomTagSQ(0x0028, 0x7023);

        ///<summary>(0028,7024) VR=SQ VM=1 Luminance Result Sequence</summary>
        public readonly static DicomTagSQ LuminanceResultSequence = new DicomTagSQ(0x0028, 0x7024);

        ///<summary>(0028,7025) VR=CS VM=1 Ambient Light Value Source</summary>
        public readonly static DicomTagCS AmbientLightValueSource = new DicomTagCS(0x0028, 0x7025);

        ///<summary>(0028,7026) VR=CS VM=1-n Measured Characteristics</summary>
        public readonly static DicomTagCSs MeasuredCharacteristics = new DicomTagCSs(0x0028, 0x7026);

        ///<summary>(0028,7027) VR=SQ VM=1 Luminance Uniformity Result Sequence</summary>
        public readonly static DicomTagSQ LuminanceUniformityResultSequence = new DicomTagSQ(0x0028, 0x7027);

        ///<summary>(0028,7028) VR=SQ VM=1 Visual Evaluation Test Sequence</summary>
        public readonly static DicomTagSQ VisualEvaluationTestSequence = new DicomTagSQ(0x0028, 0x7028);

        ///<summary>(0028,7029) VR=CS VM=1 Test Result</summary>
        public readonly static DicomTagCS TestResult = new DicomTagCS(0x0028, 0x7029);

        ///<summary>(0028,702A) VR=LO VM=1 Test Result Comment</summary>
        public readonly static DicomTagLO TestResultComment = new DicomTagLO(0x0028, 0x702A);

        ///<summary>(0028,702B) VR=CS VM=1 Test Image Validation</summary>
        public readonly static DicomTagCS TestImageValidation = new DicomTagCS(0x0028, 0x702B);

        ///<summary>(0028,702C) VR=SQ VM=1 Test Pattern Code Sequence</summary>
        public readonly static DicomTagSQ TestPatternCodeSequence = new DicomTagSQ(0x0028, 0x702C);

        ///<summary>(0028,702D) VR=SQ VM=1 Measurement Pattern Code Sequence</summary>
        public readonly static DicomTagSQ MeasurementPatternCodeSequence = new DicomTagSQ(0x0028, 0x702D);

        ///<summary>(0028,702E) VR=SQ VM=1 Visual Evaluation Method Code Sequence</summary>
        public readonly static DicomTagSQ VisualEvaluationMethodCodeSequence = new DicomTagSQ(0x0028, 0x702E);

        ///<summary>(0028,7FE0) VR=UR VM=1 Pixel Data Provider URL</summary>
        public readonly static DicomTagUR PixelDataProviderURL = new DicomTagUR(0x0028, 0x7FE0);

        ///<summary>(0028,9001) VR=UL VM=1 Data Point Rows</summary>
        public readonly static DicomTagUL DataPointRows = new DicomTagUL(0x0028, 0x9001);

        ///<summary>(0028,9002) VR=UL VM=1 Data Point Columns</summary>
        public readonly static DicomTagUL DataPointColumns = new DicomTagUL(0x0028, 0x9002);

        ///<summary>(0028,9003) VR=CS VM=1 Signal Domain Columns</summary>
        public readonly static DicomTagCS SignalDomainColumns = new DicomTagCS(0x0028, 0x9003);

        ///<summary>(0028,9099) VR=US VM=1 Largest Monochrome Pixel Value (RETIRED)</summary>
        public readonly static DicomTagUS LargestMonochromePixelValueRETIRED = new DicomTagUS(0x0028, 0x9099);

        ///<summary>(0028,9108) VR=CS VM=1 Data Representation</summary>
        public readonly static DicomTagCS DataRepresentation = new DicomTagCS(0x0028, 0x9108);

        ///<summary>(0028,9110) VR=SQ VM=1 Pixel Measures Sequence</summary>
        public readonly static DicomTagSQ PixelMeasuresSequence = new DicomTagSQ(0x0028, 0x9110);

        ///<summary>(0028,9132) VR=SQ VM=1 Frame VOI LUT Sequence</summary>
        public readonly static DicomTagSQ FrameVOILUTSequence = new DicomTagSQ(0x0028, 0x9132);

        ///<summary>(0028,9145) VR=SQ VM=1 Pixel Value Transformation Sequence</summary>
        public readonly static DicomTagSQ PixelValueTransformationSequence = new DicomTagSQ(0x0028, 0x9145);

        ///<summary>(0028,9235) VR=CS VM=1 Signal Domain Rows</summary>
        public readonly static DicomTagCS SignalDomainRows = new DicomTagCS(0x0028, 0x9235);

        ///<summary>(0028,9411) VR=FL VM=1 Display Filter Percentage</summary>
        public readonly static DicomTagFL DisplayFilterPercentage = new DicomTagFL(0x0028, 0x9411);

        ///<summary>(0028,9415) VR=SQ VM=1 Frame Pixel Shift Sequence</summary>
        public readonly static DicomTagSQ FramePixelShiftSequence = new DicomTagSQ(0x0028, 0x9415);

        ///<summary>(0028,9416) VR=US VM=1 Subtraction Item ID</summary>
        public readonly static DicomTagUS SubtractionItemID = new DicomTagUS(0x0028, 0x9416);

        ///<summary>(0028,9422) VR=SQ VM=1 Pixel Intensity Relationship LUT Sequence</summary>
        public readonly static DicomTagSQ PixelIntensityRelationshipLUTSequence = new DicomTagSQ(0x0028, 0x9422);

        ///<summary>(0028,9443) VR=SQ VM=1 Frame Pixel Data Properties Sequence</summary>
        public readonly static DicomTagSQ FramePixelDataPropertiesSequence = new DicomTagSQ(0x0028, 0x9443);

        ///<summary>(0028,9444) VR=CS VM=1 Geometrical Properties</summary>
        public readonly static DicomTagCS GeometricalProperties = new DicomTagCS(0x0028, 0x9444);

        ///<summary>(0028,9445) VR=FL VM=1 Geometric Maximum Distortion</summary>
        public readonly static DicomTagFL GeometricMaximumDistortion = new DicomTagFL(0x0028, 0x9445);

        ///<summary>(0028,9446) VR=CS VM=1-n Image Processing Applied</summary>
        public readonly static DicomTagCSs ImageProcessingApplied = new DicomTagCSs(0x0028, 0x9446);

        ///<summary>(0028,9454) VR=CS VM=1 Mask Selection Mode</summary>
        public readonly static DicomTagCS MaskSelectionMode = new DicomTagCS(0x0028, 0x9454);

        ///<summary>(0028,9474) VR=CS VM=1 LUT Function</summary>
        public readonly static DicomTagCS LUTFunction = new DicomTagCS(0x0028, 0x9474);

        ///<summary>(0028,9478) VR=FL VM=1 Mask Visibility Percentage</summary>
        public readonly static DicomTagFL MaskVisibilityPercentage = new DicomTagFL(0x0028, 0x9478);

        ///<summary>(0028,9501) VR=SQ VM=1 Pixel Shift Sequence</summary>
        public readonly static DicomTagSQ PixelShiftSequence = new DicomTagSQ(0x0028, 0x9501);

        ///<summary>(0028,9502) VR=SQ VM=1 Region Pixel Shift Sequence</summary>
        public readonly static DicomTagSQ RegionPixelShiftSequence = new DicomTagSQ(0x0028, 0x9502);

        ///<summary>(0028,9503) VR=SS VM=2-2n Vertices of the Region</summary>
        public readonly static DicomTagSSs VerticesOfTheRegion = new DicomTagSSs(0x0028, 0x9503);

        ///<summary>(0028,9505) VR=SQ VM=1 Multi-frame Presentation Sequence</summary>
        public readonly static DicomTagSQ MultiFramePresentationSequence = new DicomTagSQ(0x0028, 0x9505);

        ///<summary>(0028,9506) VR=US VM=2-2n Pixel Shift Frame Range</summary>
        public readonly static DicomTagUSs PixelShiftFrameRange = new DicomTagUSs(0x0028, 0x9506);

        ///<summary>(0028,9507) VR=US VM=2-2n LUT Frame Range</summary>
        public readonly static DicomTagUSs LUTFrameRange = new DicomTagUSs(0x0028, 0x9507);

        ///<summary>(0028,9520) VR=DS VM=16 Image to Equipment Mapping Matrix</summary>
        public readonly static DicomTagDSs ImageToEquipmentMappingMatrix = new DicomTagDSs(0x0028, 0x9520);

        ///<summary>(0028,9537) VR=CS VM=1 Equipment Coordinate System Identification</summary>
        public readonly static DicomTagCS EquipmentCoordinateSystemIdentification = new DicomTagCS(0x0028, 0x9537);

        ///<summary>(0032,000A) VR=CS VM=1 Study Status ID (RETIRED)</summary>
        public readonly static DicomTagCS StudyStatusIDRETIRED = new DicomTagCS(0x0032, 0x000A);

        ///<summary>(0032,000C) VR=CS VM=1 Study Priority ID (RETIRED)</summary>
        public readonly static DicomTagCS StudyPriorityIDRETIRED = new DicomTagCS(0x0032, 0x000C);

        ///<summary>(0032,0012) VR=LO VM=1 Study ID Issuer (RETIRED)</summary>
        public readonly static DicomTagLO StudyIDIssuerRETIRED = new DicomTagLO(0x0032, 0x0012);

        ///<summary>(0032,0032) VR=DA VM=1 Study Verified Date (RETIRED)</summary>
        public readonly static DicomTagDA StudyVerifiedDateRETIRED = new DicomTagDA(0x0032, 0x0032);

        ///<summary>(0032,0033) VR=TM VM=1 Study Verified Time (RETIRED)</summary>
        public readonly static DicomTagTM StudyVerifiedTimeRETIRED = new DicomTagTM(0x0032, 0x0033);

        ///<summary>(0032,0034) VR=DA VM=1 Study Read Date (RETIRED)</summary>
        public readonly static DicomTagDA StudyReadDateRETIRED = new DicomTagDA(0x0032, 0x0034);

        ///<summary>(0032,0035) VR=TM VM=1 Study Read Time (RETIRED)</summary>
        public readonly static DicomTagTM StudyReadTimeRETIRED = new DicomTagTM(0x0032, 0x0035);

        ///<summary>(0032,1000) VR=DA VM=1 Scheduled Study Start Date (RETIRED)</summary>
        public readonly static DicomTagDA ScheduledStudyStartDateRETIRED = new DicomTagDA(0x0032, 0x1000);

        ///<summary>(0032,1001) VR=TM VM=1 Scheduled Study Start Time (RETIRED)</summary>
        public readonly static DicomTagTM ScheduledStudyStartTimeRETIRED = new DicomTagTM(0x0032, 0x1001);

        ///<summary>(0032,1010) VR=DA VM=1 Scheduled Study Stop Date (RETIRED)</summary>
        public readonly static DicomTagDA ScheduledStudyStopDateRETIRED = new DicomTagDA(0x0032, 0x1010);

        ///<summary>(0032,1011) VR=TM VM=1 Scheduled Study Stop Time (RETIRED)</summary>
        public readonly static DicomTagTM ScheduledStudyStopTimeRETIRED = new DicomTagTM(0x0032, 0x1011);

        ///<summary>(0032,1020) VR=LO VM=1 Scheduled Study Location (RETIRED)</summary>
        public readonly static DicomTagLO ScheduledStudyLocationRETIRED = new DicomTagLO(0x0032, 0x1020);

        ///<summary>(0032,1021) VR=AE VM=1-n Scheduled Study Location AE Title (RETIRED)</summary>
        public readonly static DicomTagAEs ScheduledStudyLocationAETitleRETIRED = new DicomTagAEs(0x0032, 0x1021);

        ///<summary>(0032,1030) VR=LO VM=1 Reason for Study (RETIRED)</summary>
        public readonly static DicomTagLO ReasonForStudyRETIRED = new DicomTagLO(0x0032, 0x1030);

        ///<summary>(0032,1031) VR=SQ VM=1 Requesting Physician Identification Sequence</summary>
        public readonly static DicomTagSQ RequestingPhysicianIdentificationSequence = new DicomTagSQ(0x0032, 0x1031);

        ///<summary>(0032,1032) VR=PN VM=1 Requesting Physician</summary>
        public readonly static DicomTagPN RequestingPhysician = new DicomTagPN(0x0032, 0x1032);

        ///<summary>(0032,1033) VR=LO VM=1 Requesting Service</summary>
        public readonly static DicomTagLO RequestingService = new DicomTagLO(0x0032, 0x1033);

        ///<summary>(0032,1034) VR=SQ VM=1 Requesting Service Code Sequence</summary>
        public readonly static DicomTagSQ RequestingServiceCodeSequence = new DicomTagSQ(0x0032, 0x1034);

        ///<summary>(0032,1040) VR=DA VM=1 Study Arrival Date (RETIRED)</summary>
        public readonly static DicomTagDA StudyArrivalDateRETIRED = new DicomTagDA(0x0032, 0x1040);

        ///<summary>(0032,1041) VR=TM VM=1 Study Arrival Time (RETIRED)</summary>
        public readonly static DicomTagTM StudyArrivalTimeRETIRED = new DicomTagTM(0x0032, 0x1041);

        ///<summary>(0032,1050) VR=DA VM=1 Study Completion Date (RETIRED)</summary>
        public readonly static DicomTagDA StudyCompletionDateRETIRED = new DicomTagDA(0x0032, 0x1050);

        ///<summary>(0032,1051) VR=TM VM=1 Study Completion Time (RETIRED)</summary>
        public readonly static DicomTagTM StudyCompletionTimeRETIRED = new DicomTagTM(0x0032, 0x1051);

        ///<summary>(0032,1055) VR=CS VM=1 Study Component Status ID (RETIRED)</summary>
        public readonly static DicomTagCS StudyComponentStatusIDRETIRED = new DicomTagCS(0x0032, 0x1055);

        ///<summary>(0032,1060) VR=LO VM=1 Requested Procedure Description</summary>
        public readonly static DicomTagLO RequestedProcedureDescription = new DicomTagLO(0x0032, 0x1060);

        ///<summary>(0032,1064) VR=SQ VM=1 Requested Procedure Code Sequence</summary>
        public readonly static DicomTagSQ RequestedProcedureCodeSequence = new DicomTagSQ(0x0032, 0x1064);

        ///<summary>(0032,1065) VR=SQ VM=1 Requested Laterality Code Sequence</summary>
        public readonly static DicomTagSQ RequestedLateralityCodeSequence = new DicomTagSQ(0x0032, 0x1065);

        ///<summary>(0032,1066) VR=UT VM=1 Reason for Visit</summary>
        public readonly static DicomTagUT ReasonForVisit = new DicomTagUT(0x0032, 0x1066);

        ///<summary>(0032,1067) VR=SQ VM=1 Reason for Visit Code Sequence</summary>
        public readonly static DicomTagSQ ReasonForVisitCodeSequence = new DicomTagSQ(0x0032, 0x1067);

        ///<summary>(0032,1070) VR=LO VM=1 Requested Contrast Agent</summary>
        public readonly static DicomTagLO RequestedContrastAgent = new DicomTagLO(0x0032, 0x1070);

        ///<summary>(0032,4000) VR=LT VM=1 Study Comments (RETIRED)</summary>
        public readonly static DicomTagLT StudyCommentsRETIRED = new DicomTagLT(0x0032, 0x4000);

        ///<summary>(0034,0001) VR=SQ VM=1 Flow Identifier Sequence</summary>
        public readonly static DicomTagSQ FlowIdentifierSequence = new DicomTagSQ(0x0034, 0x0001);

        ///<summary>(0034,0002) VR=OB VM=1 Flow Identifier</summary>
        public readonly static DicomTagOB FlowIdentifier = new DicomTagOB(0x0034, 0x0002);

        ///<summary>(0034,0003) VR=UI VM=1 Flow Transfer Syntax UID</summary>
        public readonly static DicomTagUI FlowTransferSyntaxUID = new DicomTagUI(0x0034, 0x0003);

        ///<summary>(0034,0004) VR=UL VM=1 Flow RTP Sampling Rate</summary>
        public readonly static DicomTagUL FlowRTPSamplingRate = new DicomTagUL(0x0034, 0x0004);

        ///<summary>(0034,0005) VR=OB VM=1 Source Identifier</summary>
        public readonly static DicomTagOB SourceIdentifier = new DicomTagOB(0x0034, 0x0005);

        ///<summary>(0034,0007) VR=OB VM=1 Frame Origin Timestamp</summary>
        public readonly static DicomTagOB FrameOriginTimestamp = new DicomTagOB(0x0034, 0x0007);

        ///<summary>(0034,0008) VR=CS VM=1 Includes Imaging Subject</summary>
        public readonly static DicomTagCS IncludesImagingSubject = new DicomTagCS(0x0034, 0x0008);

        ///<summary>(0034,0009) VR=SQ VM=1 Frame Usefulness Group Sequence</summary>
        public readonly static DicomTagSQ FrameUsefulnessGroupSequence = new DicomTagSQ(0x0034, 0x0009);

        ///<summary>(0034,000A) VR=SQ VM=1 Real-Time Bulk Data Flow Sequence</summary>
        public readonly static DicomTagSQ RealTimeBulkDataFlowSequence = new DicomTagSQ(0x0034, 0x000A);

        ///<summary>(0034,000B) VR=SQ VM=1 Camera Position Group Sequence</summary>
        public readonly static DicomTagSQ CameraPositionGroupSequence = new DicomTagSQ(0x0034, 0x000B);

        ///<summary>(0034,000C) VR=CS VM=1 Includes Information</summary>
        public readonly static DicomTagCS IncludesInformation = new DicomTagCS(0x0034, 0x000C);

        ///<summary>(0034,000D) VR=SQ VM=1 Time of Frame Group Sequence</summary>
        public readonly static DicomTagSQ TimeOfFrameGroupSequence = new DicomTagSQ(0x0034, 0x000D);

        ///<summary>(0038,0004) VR=SQ VM=1 Referenced Patient Alias Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedPatientAliasSequenceRETIRED = new DicomTagSQ(0x0038, 0x0004);

        ///<summary>(0038,0008) VR=CS VM=1 Visit Status ID</summary>
        public readonly static DicomTagCS VisitStatusID = new DicomTagCS(0x0038, 0x0008);

        ///<summary>(0038,0010) VR=LO VM=1 Admission ID</summary>
        public readonly static DicomTagLO AdmissionID = new DicomTagLO(0x0038, 0x0010);

        ///<summary>(0038,0011) VR=LO VM=1 Issuer of Admission ID (RETIRED)</summary>
        public readonly static DicomTagLO IssuerOfAdmissionIDRETIRED = new DicomTagLO(0x0038, 0x0011);

        ///<summary>(0038,0014) VR=SQ VM=1 Issuer of Admission ID Sequence</summary>
        public readonly static DicomTagSQ IssuerOfAdmissionIDSequence = new DicomTagSQ(0x0038, 0x0014);

        ///<summary>(0038,0016) VR=LO VM=1 Route of Admissions</summary>
        public readonly static DicomTagLO RouteOfAdmissions = new DicomTagLO(0x0038, 0x0016);

        ///<summary>(0038,001A) VR=DA VM=1 Scheduled Admission Date (RETIRED)</summary>
        public readonly static DicomTagDA ScheduledAdmissionDateRETIRED = new DicomTagDA(0x0038, 0x001A);

        ///<summary>(0038,001B) VR=TM VM=1 Scheduled Admission Time (RETIRED)</summary>
        public readonly static DicomTagTM ScheduledAdmissionTimeRETIRED = new DicomTagTM(0x0038, 0x001B);

        ///<summary>(0038,001C) VR=DA VM=1 Scheduled Discharge Date (RETIRED)</summary>
        public readonly static DicomTagDA ScheduledDischargeDateRETIRED = new DicomTagDA(0x0038, 0x001C);

        ///<summary>(0038,001D) VR=TM VM=1 Scheduled Discharge Time (RETIRED)</summary>
        public readonly static DicomTagTM ScheduledDischargeTimeRETIRED = new DicomTagTM(0x0038, 0x001D);

        ///<summary>(0038,001E) VR=LO VM=1 Scheduled Patient Institution Residence (RETIRED)</summary>
        public readonly static DicomTagLO ScheduledPatientInstitutionResidenceRETIRED = new DicomTagLO(0x0038, 0x001E);

        ///<summary>(0038,0020) VR=DA VM=1 Admitting Date</summary>
        public readonly static DicomTagDA AdmittingDate = new DicomTagDA(0x0038, 0x0020);

        ///<summary>(0038,0021) VR=TM VM=1 Admitting Time</summary>
        public readonly static DicomTagTM AdmittingTime = new DicomTagTM(0x0038, 0x0021);

        ///<summary>(0038,0030) VR=DA VM=1 Discharge Date (RETIRED)</summary>
        public readonly static DicomTagDA DischargeDateRETIRED = new DicomTagDA(0x0038, 0x0030);

        ///<summary>(0038,0032) VR=TM VM=1 Discharge Time (RETIRED)</summary>
        public readonly static DicomTagTM DischargeTimeRETIRED = new DicomTagTM(0x0038, 0x0032);

        ///<summary>(0038,0040) VR=LO VM=1 Discharge Diagnosis Description (RETIRED)</summary>
        public readonly static DicomTagLO DischargeDiagnosisDescriptionRETIRED = new DicomTagLO(0x0038, 0x0040);

        ///<summary>(0038,0044) VR=SQ VM=1 Discharge Diagnosis Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ DischargeDiagnosisCodeSequenceRETIRED = new DicomTagSQ(0x0038, 0x0044);

        ///<summary>(0038,0050) VR=LO VM=1 Special Needs</summary>
        public readonly static DicomTagLO SpecialNeeds = new DicomTagLO(0x0038, 0x0050);

        ///<summary>(0038,0060) VR=LO VM=1 Service Episode ID</summary>
        public readonly static DicomTagLO ServiceEpisodeID = new DicomTagLO(0x0038, 0x0060);

        ///<summary>(0038,0061) VR=LO VM=1 Issuer of Service Episode ID (RETIRED)</summary>
        public readonly static DicomTagLO IssuerOfServiceEpisodeIDRETIRED = new DicomTagLO(0x0038, 0x0061);

        ///<summary>(0038,0062) VR=LO VM=1 Service Episode Description</summary>
        public readonly static DicomTagLO ServiceEpisodeDescription = new DicomTagLO(0x0038, 0x0062);

        ///<summary>(0038,0064) VR=SQ VM=1 Issuer of Service Episode ID Sequence</summary>
        public readonly static DicomTagSQ IssuerOfServiceEpisodeIDSequence = new DicomTagSQ(0x0038, 0x0064);

        ///<summary>(0038,0100) VR=SQ VM=1 Pertinent Documents Sequence</summary>
        public readonly static DicomTagSQ PertinentDocumentsSequence = new DicomTagSQ(0x0038, 0x0100);

        ///<summary>(0038,0101) VR=SQ VM=1 Pertinent Resources Sequence</summary>
        public readonly static DicomTagSQ PertinentResourcesSequence = new DicomTagSQ(0x0038, 0x0101);

        ///<summary>(0038,0102) VR=LO VM=1 Resource Description</summary>
        public readonly static DicomTagLO ResourceDescription = new DicomTagLO(0x0038, 0x0102);

        ///<summary>(0038,0300) VR=LO VM=1 Current Patient Location</summary>
        public readonly static DicomTagLO CurrentPatientLocation = new DicomTagLO(0x0038, 0x0300);

        ///<summary>(0038,0400) VR=LO VM=1 Patient's Institution Residence</summary>
        public readonly static DicomTagLO PatientInstitutionResidence = new DicomTagLO(0x0038, 0x0400);

        ///<summary>(0038,0500) VR=LO VM=1 Patient State</summary>
        public readonly static DicomTagLO PatientState = new DicomTagLO(0x0038, 0x0500);

        ///<summary>(0038,0502) VR=SQ VM=1 Patient Clinical Trial Participation Sequence</summary>
        public readonly static DicomTagSQ PatientClinicalTrialParticipationSequence = new DicomTagSQ(0x0038, 0x0502);

        ///<summary>(0038,4000) VR=LT VM=1 Visit Comments</summary>
        public readonly static DicomTagLT VisitComments = new DicomTagLT(0x0038, 0x4000);

        ///<summary>(003A,0004) VR=CS VM=1 Waveform Originality</summary>
        public readonly static DicomTagCS WaveformOriginality = new DicomTagCS(0x003A, 0x0004);

        ///<summary>(003A,0005) VR=US VM=1 Number of Waveform Channels</summary>
        public readonly static DicomTagUS NumberOfWaveformChannels = new DicomTagUS(0x003A, 0x0005);

        ///<summary>(003A,0010) VR=UL VM=1 Number of Waveform Samples</summary>
        public readonly static DicomTagUL NumberOfWaveformSamples = new DicomTagUL(0x003A, 0x0010);

        ///<summary>(003A,001A) VR=DS VM=1 Sampling Frequency</summary>
        public readonly static DicomTagDS SamplingFrequency = new DicomTagDS(0x003A, 0x001A);

        ///<summary>(003A,0020) VR=SH VM=1 Multiplex Group Label</summary>
        public readonly static DicomTagSH MultiplexGroupLabel = new DicomTagSH(0x003A, 0x0020);

        ///<summary>(003A,0200) VR=SQ VM=1 Channel Definition Sequence</summary>
        public readonly static DicomTagSQ ChannelDefinitionSequence = new DicomTagSQ(0x003A, 0x0200);

        ///<summary>(003A,0202) VR=IS VM=1 Waveform Channel Number</summary>
        public readonly static DicomTagIS WaveformChannelNumber = new DicomTagIS(0x003A, 0x0202);

        ///<summary>(003A,0203) VR=SH VM=1 Channel Label</summary>
        public readonly static DicomTagSH ChannelLabel = new DicomTagSH(0x003A, 0x0203);

        ///<summary>(003A,0205) VR=CS VM=1-n Channel Status</summary>
        public readonly static DicomTagCSs ChannelStatus = new DicomTagCSs(0x003A, 0x0205);

        ///<summary>(003A,0208) VR=SQ VM=1 Channel Source Sequence</summary>
        public readonly static DicomTagSQ ChannelSourceSequence = new DicomTagSQ(0x003A, 0x0208);

        ///<summary>(003A,0209) VR=SQ VM=1 Channel Source Modifiers Sequence</summary>
        public readonly static DicomTagSQ ChannelSourceModifiersSequence = new DicomTagSQ(0x003A, 0x0209);

        ///<summary>(003A,020A) VR=SQ VM=1 Source Waveform Sequence</summary>
        public readonly static DicomTagSQ SourceWaveformSequence = new DicomTagSQ(0x003A, 0x020A);

        ///<summary>(003A,020C) VR=LO VM=1 Channel Derivation Description</summary>
        public readonly static DicomTagLO ChannelDerivationDescription = new DicomTagLO(0x003A, 0x020C);

        ///<summary>(003A,0210) VR=DS VM=1 Channel Sensitivity</summary>
        public readonly static DicomTagDS ChannelSensitivity = new DicomTagDS(0x003A, 0x0210);

        ///<summary>(003A,0211) VR=SQ VM=1 Channel Sensitivity Units Sequence</summary>
        public readonly static DicomTagSQ ChannelSensitivityUnitsSequence = new DicomTagSQ(0x003A, 0x0211);

        ///<summary>(003A,0212) VR=DS VM=1 Channel Sensitivity Correction Factor</summary>
        public readonly static DicomTagDS ChannelSensitivityCorrectionFactor = new DicomTagDS(0x003A, 0x0212);

        ///<summary>(003A,0213) VR=DS VM=1 Channel Baseline</summary>
        public readonly static DicomTagDS ChannelBaseline = new DicomTagDS(0x003A, 0x0213);

        ///<summary>(003A,0214) VR=DS VM=1 Channel Time Skew</summary>
        public readonly static DicomTagDS ChannelTimeSkew = new DicomTagDS(0x003A, 0x0214);

        ///<summary>(003A,0215) VR=DS VM=1 Channel Sample Skew</summary>
        public readonly static DicomTagDS ChannelSampleSkew = new DicomTagDS(0x003A, 0x0215);

        ///<summary>(003A,0218) VR=DS VM=1 Channel Offset</summary>
        public readonly static DicomTagDS ChannelOffset = new DicomTagDS(0x003A, 0x0218);

        ///<summary>(003A,021A) VR=US VM=1 Waveform Bits Stored</summary>
        public readonly static DicomTagUS WaveformBitsStored = new DicomTagUS(0x003A, 0x021A);

        ///<summary>(003A,0220) VR=DS VM=1 Filter Low Frequency</summary>
        public readonly static DicomTagDS FilterLowFrequency = new DicomTagDS(0x003A, 0x0220);

        ///<summary>(003A,0221) VR=DS VM=1 Filter High Frequency</summary>
        public readonly static DicomTagDS FilterHighFrequency = new DicomTagDS(0x003A, 0x0221);

        ///<summary>(003A,0222) VR=DS VM=1 Notch Filter Frequency</summary>
        public readonly static DicomTagDS NotchFilterFrequency = new DicomTagDS(0x003A, 0x0222);

        ///<summary>(003A,0223) VR=DS VM=1 Notch Filter Bandwidth</summary>
        public readonly static DicomTagDS NotchFilterBandwidth = new DicomTagDS(0x003A, 0x0223);

        ///<summary>(003A,0230) VR=FL VM=1 Waveform Data Display Scale</summary>
        public readonly static DicomTagFL WaveformDataDisplayScale = new DicomTagFL(0x003A, 0x0230);

        ///<summary>(003A,0231) VR=US VM=3 Waveform Display Background CIELab Value</summary>
        public readonly static DicomTagUSs WaveformDisplayBackgroundCIELabValue = new DicomTagUSs(0x003A, 0x0231);

        ///<summary>(003A,0240) VR=SQ VM=1 Waveform Presentation Group Sequence</summary>
        public readonly static DicomTagSQ WaveformPresentationGroupSequence = new DicomTagSQ(0x003A, 0x0240);

        ///<summary>(003A,0241) VR=US VM=1 Presentation Group Number</summary>
        public readonly static DicomTagUS PresentationGroupNumber = new DicomTagUS(0x003A, 0x0241);

        ///<summary>(003A,0242) VR=SQ VM=1 Channel Display Sequence</summary>
        public readonly static DicomTagSQ ChannelDisplaySequence = new DicomTagSQ(0x003A, 0x0242);

        ///<summary>(003A,0244) VR=US VM=3 Channel Recommended Display CIELab Value</summary>
        public readonly static DicomTagUSs ChannelRecommendedDisplayCIELabValue = new DicomTagUSs(0x003A, 0x0244);

        ///<summary>(003A,0245) VR=FL VM=1 Channel Position</summary>
        public readonly static DicomTagFL ChannelPosition = new DicomTagFL(0x003A, 0x0245);

        ///<summary>(003A,0246) VR=CS VM=1 Display Shading Flag</summary>
        public readonly static DicomTagCS DisplayShadingFlag = new DicomTagCS(0x003A, 0x0246);

        ///<summary>(003A,0247) VR=FL VM=1 Fractional Channel Display Scale</summary>
        public readonly static DicomTagFL FractionalChannelDisplayScale = new DicomTagFL(0x003A, 0x0247);

        ///<summary>(003A,0248) VR=FL VM=1 Absolute Channel Display Scale</summary>
        public readonly static DicomTagFL AbsoluteChannelDisplayScale = new DicomTagFL(0x003A, 0x0248);

        ///<summary>(003A,0300) VR=SQ VM=1 Multiplexed Audio Channels Description Code Sequence</summary>
        public readonly static DicomTagSQ MultiplexedAudioChannelsDescriptionCodeSequence = new DicomTagSQ(0x003A, 0x0300);

        ///<summary>(003A,0301) VR=IS VM=1 Channel Identification Code</summary>
        public readonly static DicomTagIS ChannelIdentificationCode = new DicomTagIS(0x003A, 0x0301);

        ///<summary>(003A,0302) VR=CS VM=1 Channel Mode</summary>
        public readonly static DicomTagCS ChannelMode = new DicomTagCS(0x003A, 0x0302);

        ///<summary>(003A,0310) VR=UI VM=1 Multiplex Group UID</summary>
        public readonly static DicomTagUI MultiplexGroupUID = new DicomTagUI(0x003A, 0x0310);

        ///<summary>(003A,0311) VR=DS VM=1 Powerline Frequency</summary>
        public readonly static DicomTagDS PowerlineFrequency = new DicomTagDS(0x003A, 0x0311);

        ///<summary>(003A,0312) VR=SQ VM=1 Channel Impedance Sequence</summary>
        public readonly static DicomTagSQ ChannelImpedanceSequence = new DicomTagSQ(0x003A, 0x0312);

        ///<summary>(003A,0313) VR=DS VM=1 Impedance Value</summary>
        public readonly static DicomTagDS ImpedanceValue = new DicomTagDS(0x003A, 0x0313);

        ///<summary>(003A,0314) VR=DT VM=1 Impedance Measurement DateTime</summary>
        public readonly static DicomTagDT ImpedanceMeasurementDateTime = new DicomTagDT(0x003A, 0x0314);

        ///<summary>(003A,0315) VR=DS VM=1 Impedance Measurement Frequency</summary>
        public readonly static DicomTagDS ImpedanceMeasurementFrequency = new DicomTagDS(0x003A, 0x0315);

        ///<summary>(003A,0316) VR=CS VM=1 Impedance Measurement Current Type</summary>
        public readonly static DicomTagCS ImpedanceMeasurementCurrentType = new DicomTagCS(0x003A, 0x0316);

        ///<summary>(003A,0317) VR=CS VM=1 Waveform Amplifier Type</summary>
        public readonly static DicomTagCS WaveformAmplifierType = new DicomTagCS(0x003A, 0x0317);

        ///<summary>(003A,0318) VR=SQ VM=1 Filter Low Frequency Characteristics Sequence</summary>
        public readonly static DicomTagSQ FilterLowFrequencyCharacteristicsSequence = new DicomTagSQ(0x003A, 0x0318);

        ///<summary>(003A,0319) VR=SQ VM=1 Filter High Frequency Characteristics Sequence</summary>
        public readonly static DicomTagSQ FilterHighFrequencyCharacteristicsSequence = new DicomTagSQ(0x003A, 0x0319);

        ///<summary>(003A,0320) VR=SQ VM=1 Summarized Filter Lookup Table Sequence</summary>
        public readonly static DicomTagSQ SummarizedFilterLookupTableSequence = new DicomTagSQ(0x003A, 0x0320);

        ///<summary>(003A,0321) VR=SQ VM=1 Notch Filter Characteristics Sequence</summary>
        public readonly static DicomTagSQ NotchFilterCharacteristicsSequence = new DicomTagSQ(0x003A, 0x0321);

        ///<summary>(003A,0322) VR=CS VM=1 Waveform Filter Type</summary>
        public readonly static DicomTagCS WaveformFilterType = new DicomTagCS(0x003A, 0x0322);

        ///<summary>(003A,0323) VR=SQ VM=1 Analog Filter Characteristics Sequence</summary>
        public readonly static DicomTagSQ AnalogFilterCharacteristicsSequence = new DicomTagSQ(0x003A, 0x0323);

        ///<summary>(003A,0324) VR=DS VM=1 Analog Filter Roll Off </summary>
        public readonly static DicomTagDS AnalogFilterRollOff = new DicomTagDS(0x003A, 0x0324);

        ///<summary>(003A,0325) VR=SQ VM=1 Analog Filter Type Code Sequence</summary>
        public readonly static DicomTagSQ AnalogFilterTypeCodeSequence = new DicomTagSQ(0x003A, 0x0325);

        ///<summary>(003A,0326) VR=SQ VM=1 Digital Filter Characteristics Sequence</summary>
        public readonly static DicomTagSQ DigitalFilterCharacteristicsSequence = new DicomTagSQ(0x003A, 0x0326);

        ///<summary>(003A,0327) VR=IS VM=1 Digital Filter Order</summary>
        public readonly static DicomTagIS DigitalFilterOrder = new DicomTagIS(0x003A, 0x0327);

        ///<summary>(003A,0328) VR=SQ VM=1 Digital Filter Type Code Sequence</summary>
        public readonly static DicomTagSQ DigitalFilterTypeCodeSequence = new DicomTagSQ(0x003A, 0x0328);

        ///<summary>(003A,0329) VR=ST VM=1 Waveform Filter Description</summary>
        public readonly static DicomTagST WaveformFilterDescription = new DicomTagST(0x003A, 0x0329);

        ///<summary>(003A,032A) VR=SQ VM=1 Filter Lookup Table Sequence</summary>
        public readonly static DicomTagSQ FilterLookupTableSequence = new DicomTagSQ(0x003A, 0x032A);

        ///<summary>(003A,032B) VR=ST VM=1 Filter Lookup Table Description</summary>
        public readonly static DicomTagST FilterLookupTableDescription = new DicomTagST(0x003A, 0x032B);

        ///<summary>(003A,032C) VR=SQ VM=1 Frequency Encoding Code Sequence</summary>
        public readonly static DicomTagSQ FrequencyEncodingCodeSequence = new DicomTagSQ(0x003A, 0x032C);

        ///<summary>(003A,032D) VR=SQ VM=1 Magnitude Encoding Code Sequence</summary>
        public readonly static DicomTagSQ MagnitudeEncodingCodeSequence = new DicomTagSQ(0x003A, 0x032D);

        ///<summary>(003A,032E) VR=OD VM=1 Filter Lookup Table Data</summary>
        public readonly static DicomTagOD FilterLookupTableData = new DicomTagOD(0x003A, 0x032E);

        ///<summary>(0040,0001) VR=AE VM=1-n Scheduled Station AE Title</summary>
        public readonly static DicomTagAEs ScheduledStationAETitle = new DicomTagAEs(0x0040, 0x0001);

        ///<summary>(0040,0002) VR=DA VM=1 Scheduled Procedure Step Start Date</summary>
        public readonly static DicomTagDA ScheduledProcedureStepStartDate = new DicomTagDA(0x0040, 0x0002);

        ///<summary>(0040,0003) VR=TM VM=1 Scheduled Procedure Step Start Time</summary>
        public readonly static DicomTagTM ScheduledProcedureStepStartTime = new DicomTagTM(0x0040, 0x0003);

        ///<summary>(0040,0004) VR=DA VM=1 Scheduled Procedure Step End Date</summary>
        public readonly static DicomTagDA ScheduledProcedureStepEndDate = new DicomTagDA(0x0040, 0x0004);

        ///<summary>(0040,0005) VR=TM VM=1 Scheduled Procedure Step End Time</summary>
        public readonly static DicomTagTM ScheduledProcedureStepEndTime = new DicomTagTM(0x0040, 0x0005);

        ///<summary>(0040,0006) VR=PN VM=1 Scheduled Performing Physician's Name</summary>
        public readonly static DicomTagPN ScheduledPerformingPhysicianName = new DicomTagPN(0x0040, 0x0006);

        ///<summary>(0040,0007) VR=LO VM=1 Scheduled Procedure Step Description</summary>
        public readonly static DicomTagLO ScheduledProcedureStepDescription = new DicomTagLO(0x0040, 0x0007);

        ///<summary>(0040,0008) VR=SQ VM=1 Scheduled Protocol Code Sequence</summary>
        public readonly static DicomTagSQ ScheduledProtocolCodeSequence = new DicomTagSQ(0x0040, 0x0008);

        ///<summary>(0040,0009) VR=SH VM=1 Scheduled Procedure Step ID</summary>
        public readonly static DicomTagSH ScheduledProcedureStepID = new DicomTagSH(0x0040, 0x0009);

        ///<summary>(0040,000A) VR=SQ VM=1 Stage Code Sequence</summary>
        public readonly static DicomTagSQ StageCodeSequence = new DicomTagSQ(0x0040, 0x000A);

        ///<summary>(0040,000B) VR=SQ VM=1 Scheduled Performing Physician Identification Sequence</summary>
        public readonly static DicomTagSQ ScheduledPerformingPhysicianIdentificationSequence = new DicomTagSQ(0x0040, 0x000B);

        ///<summary>(0040,0010) VR=SH VM=1-n Scheduled Station Name</summary>
        public readonly static DicomTagSHs ScheduledStationName = new DicomTagSHs(0x0040, 0x0010);

        ///<summary>(0040,0011) VR=SH VM=1 Scheduled Procedure Step Location</summary>
        public readonly static DicomTagSH ScheduledProcedureStepLocation = new DicomTagSH(0x0040, 0x0011);

        ///<summary>(0040,0012) VR=LO VM=1 Pre-Medication</summary>
        public readonly static DicomTagLO PreMedication = new DicomTagLO(0x0040, 0x0012);

        ///<summary>(0040,0020) VR=CS VM=1 Scheduled Procedure Step Status</summary>
        public readonly static DicomTagCS ScheduledProcedureStepStatus = new DicomTagCS(0x0040, 0x0020);

        ///<summary>(0040,0026) VR=SQ VM=1 Order Placer Identifier Sequence</summary>
        public readonly static DicomTagSQ OrderPlacerIdentifierSequence = new DicomTagSQ(0x0040, 0x0026);

        ///<summary>(0040,0027) VR=SQ VM=1 Order Filler Identifier Sequence</summary>
        public readonly static DicomTagSQ OrderFillerIdentifierSequence = new DicomTagSQ(0x0040, 0x0027);

        ///<summary>(0040,0031) VR=UT VM=1 Local Namespace Entity ID</summary>
        public readonly static DicomTagUT LocalNamespaceEntityID = new DicomTagUT(0x0040, 0x0031);

        ///<summary>(0040,0032) VR=UT VM=1 Universal Entity ID</summary>
        public readonly static DicomTagUT UniversalEntityID = new DicomTagUT(0x0040, 0x0032);

        ///<summary>(0040,0033) VR=CS VM=1 Universal Entity ID Type</summary>
        public readonly static DicomTagCS UniversalEntityIDType = new DicomTagCS(0x0040, 0x0033);

        ///<summary>(0040,0035) VR=CS VM=1 Identifier Type Code</summary>
        public readonly static DicomTagCS IdentifierTypeCode = new DicomTagCS(0x0040, 0x0035);

        ///<summary>(0040,0036) VR=SQ VM=1 Assigning Facility Sequence</summary>
        public readonly static DicomTagSQ AssigningFacilitySequence = new DicomTagSQ(0x0040, 0x0036);

        ///<summary>(0040,0039) VR=SQ VM=1 Assigning Jurisdiction Code Sequence</summary>
        public readonly static DicomTagSQ AssigningJurisdictionCodeSequence = new DicomTagSQ(0x0040, 0x0039);

        ///<summary>(0040,003A) VR=SQ VM=1 Assigning Agency or Department Code Sequence</summary>
        public readonly static DicomTagSQ AssigningAgencyOrDepartmentCodeSequence = new DicomTagSQ(0x0040, 0x003A);

        ///<summary>(0040,0100) VR=SQ VM=1 Scheduled Procedure Step Sequence</summary>
        public readonly static DicomTagSQ ScheduledProcedureStepSequence = new DicomTagSQ(0x0040, 0x0100);

        ///<summary>(0040,0220) VR=SQ VM=1 Referenced Non-Image Composite SOP Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedNonImageCompositeSOPInstanceSequence = new DicomTagSQ(0x0040, 0x0220);

        ///<summary>(0040,0241) VR=AE VM=1 Performed Station AE Title</summary>
        public readonly static DicomTagAE PerformedStationAETitle = new DicomTagAE(0x0040, 0x0241);

        ///<summary>(0040,0242) VR=SH VM=1 Performed Station Name</summary>
        public readonly static DicomTagSH PerformedStationName = new DicomTagSH(0x0040, 0x0242);

        ///<summary>(0040,0243) VR=SH VM=1 Performed Location</summary>
        public readonly static DicomTagSH PerformedLocation = new DicomTagSH(0x0040, 0x0243);

        ///<summary>(0040,0244) VR=DA VM=1 Performed Procedure Step Start Date</summary>
        public readonly static DicomTagDA PerformedProcedureStepStartDate = new DicomTagDA(0x0040, 0x0244);

        ///<summary>(0040,0245) VR=TM VM=1 Performed Procedure Step Start Time</summary>
        public readonly static DicomTagTM PerformedProcedureStepStartTime = new DicomTagTM(0x0040, 0x0245);

        ///<summary>(0040,0250) VR=DA VM=1 Performed Procedure Step End Date</summary>
        public readonly static DicomTagDA PerformedProcedureStepEndDate = new DicomTagDA(0x0040, 0x0250);

        ///<summary>(0040,0251) VR=TM VM=1 Performed Procedure Step End Time</summary>
        public readonly static DicomTagTM PerformedProcedureStepEndTime = new DicomTagTM(0x0040, 0x0251);

        ///<summary>(0040,0252) VR=CS VM=1 Performed Procedure Step Status</summary>
        public readonly static DicomTagCS PerformedProcedureStepStatus = new DicomTagCS(0x0040, 0x0252);

        ///<summary>(0040,0253) VR=SH VM=1 Performed Procedure Step ID</summary>
        public readonly static DicomTagSH PerformedProcedureStepID = new DicomTagSH(0x0040, 0x0253);

        ///<summary>(0040,0254) VR=LO VM=1 Performed Procedure Step Description</summary>
        public readonly static DicomTagLO PerformedProcedureStepDescription = new DicomTagLO(0x0040, 0x0254);

        ///<summary>(0040,0255) VR=LO VM=1 Performed Procedure Type Description</summary>
        public readonly static DicomTagLO PerformedProcedureTypeDescription = new DicomTagLO(0x0040, 0x0255);

        ///<summary>(0040,0260) VR=SQ VM=1 Performed Protocol Code Sequence</summary>
        public readonly static DicomTagSQ PerformedProtocolCodeSequence = new DicomTagSQ(0x0040, 0x0260);

        ///<summary>(0040,0261) VR=CS VM=1 Performed Protocol Type</summary>
        public readonly static DicomTagCS PerformedProtocolType = new DicomTagCS(0x0040, 0x0261);

        ///<summary>(0040,0270) VR=SQ VM=1 Scheduled Step Attributes Sequence</summary>
        public readonly static DicomTagSQ ScheduledStepAttributesSequence = new DicomTagSQ(0x0040, 0x0270);

        ///<summary>(0040,0275) VR=SQ VM=1 Request Attributes Sequence</summary>
        public readonly static DicomTagSQ RequestAttributesSequence = new DicomTagSQ(0x0040, 0x0275);

        ///<summary>(0040,0280) VR=ST VM=1 Comments on the Performed Procedure Step</summary>
        public readonly static DicomTagST CommentsOnThePerformedProcedureStep = new DicomTagST(0x0040, 0x0280);

        ///<summary>(0040,0281) VR=SQ VM=1 Performed Procedure Step Discontinuation Reason Code Sequence</summary>
        public readonly static DicomTagSQ PerformedProcedureStepDiscontinuationReasonCodeSequence = new DicomTagSQ(0x0040, 0x0281);

        ///<summary>(0040,0293) VR=SQ VM=1 Quantity Sequence</summary>
        public readonly static DicomTagSQ QuantitySequence = new DicomTagSQ(0x0040, 0x0293);

        ///<summary>(0040,0294) VR=DS VM=1 Quantity</summary>
        public readonly static DicomTagDS Quantity = new DicomTagDS(0x0040, 0x0294);

        ///<summary>(0040,0295) VR=SQ VM=1 Measuring Units Sequence</summary>
        public readonly static DicomTagSQ MeasuringUnitsSequence = new DicomTagSQ(0x0040, 0x0295);

        ///<summary>(0040,0296) VR=SQ VM=1 Billing Item Sequence</summary>
        public readonly static DicomTagSQ BillingItemSequence = new DicomTagSQ(0x0040, 0x0296);

        ///<summary>(0040,0300) VR=US VM=1 Total Time of Fluoroscopy (RETIRED)</summary>
        public readonly static DicomTagUS TotalTimeOfFluoroscopyRETIRED = new DicomTagUS(0x0040, 0x0300);

        ///<summary>(0040,0301) VR=US VM=1 Total Number of Exposures (RETIRED)</summary>
        public readonly static DicomTagUS TotalNumberOfExposuresRETIRED = new DicomTagUS(0x0040, 0x0301);

        ///<summary>(0040,0302) VR=US VM=1 Entrance Dose</summary>
        public readonly static DicomTagUS EntranceDose = new DicomTagUS(0x0040, 0x0302);

        ///<summary>(0040,0303) VR=US VM=1-2 Exposed Area</summary>
        public readonly static DicomTagUSs ExposedArea = new DicomTagUSs(0x0040, 0x0303);

        ///<summary>(0040,0306) VR=DS VM=1 Distance Source to Entrance</summary>
        public readonly static DicomTagDS DistanceSourceToEntrance = new DicomTagDS(0x0040, 0x0306);

        ///<summary>(0040,0307) VR=DS VM=1 Distance Source to Support (RETIRED)</summary>
        public readonly static DicomTagDS DistanceSourceToSupportRETIRED = new DicomTagDS(0x0040, 0x0307);

        ///<summary>(0040,030E) VR=SQ VM=1 Exposure Dose Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ExposureDoseSequenceRETIRED = new DicomTagSQ(0x0040, 0x030E);

        ///<summary>(0040,0310) VR=ST VM=1 Comments on Radiation Dose</summary>
        public readonly static DicomTagST CommentsOnRadiationDose = new DicomTagST(0x0040, 0x0310);

        ///<summary>(0040,0312) VR=DS VM=1 X-Ray Output</summary>
        public readonly static DicomTagDS XRayOutput = new DicomTagDS(0x0040, 0x0312);

        ///<summary>(0040,0314) VR=DS VM=1 Half Value Layer</summary>
        public readonly static DicomTagDS HalfValueLayer = new DicomTagDS(0x0040, 0x0314);

        ///<summary>(0040,0316) VR=DS VM=1 Organ Dose</summary>
        public readonly static DicomTagDS OrganDose = new DicomTagDS(0x0040, 0x0316);

        ///<summary>(0040,0318) VR=CS VM=1 Organ Exposed</summary>
        public readonly static DicomTagCS OrganExposed = new DicomTagCS(0x0040, 0x0318);

        ///<summary>(0040,0320) VR=SQ VM=1 Billing Procedure Step Sequence</summary>
        public readonly static DicomTagSQ BillingProcedureStepSequence = new DicomTagSQ(0x0040, 0x0320);

        ///<summary>(0040,0321) VR=SQ VM=1 Film Consumption Sequence</summary>
        public readonly static DicomTagSQ FilmConsumptionSequence = new DicomTagSQ(0x0040, 0x0321);

        ///<summary>(0040,0324) VR=SQ VM=1 Billing Supplies and Devices Sequence</summary>
        public readonly static DicomTagSQ BillingSuppliesAndDevicesSequence = new DicomTagSQ(0x0040, 0x0324);

        ///<summary>(0040,0330) VR=SQ VM=1 Referenced Procedure Step Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedProcedureStepSequenceRETIRED = new DicomTagSQ(0x0040, 0x0330);

        ///<summary>(0040,0340) VR=SQ VM=1 Performed Series Sequence</summary>
        public readonly static DicomTagSQ PerformedSeriesSequence = new DicomTagSQ(0x0040, 0x0340);

        ///<summary>(0040,0400) VR=LT VM=1 Comments on the Scheduled Procedure Step</summary>
        public readonly static DicomTagLT CommentsOnTheScheduledProcedureStep = new DicomTagLT(0x0040, 0x0400);

        ///<summary>(0040,0440) VR=SQ VM=1 Protocol Context Sequence</summary>
        public readonly static DicomTagSQ ProtocolContextSequence = new DicomTagSQ(0x0040, 0x0440);

        ///<summary>(0040,0441) VR=SQ VM=1 Content Item Modifier Sequence</summary>
        public readonly static DicomTagSQ ContentItemModifierSequence = new DicomTagSQ(0x0040, 0x0441);

        ///<summary>(0040,0500) VR=SQ VM=1 Scheduled Specimen Sequence</summary>
        public readonly static DicomTagSQ ScheduledSpecimenSequence = new DicomTagSQ(0x0040, 0x0500);

        ///<summary>(0040,050A) VR=LO VM=1 Specimen Accession Number (RETIRED)</summary>
        public readonly static DicomTagLO SpecimenAccessionNumberRETIRED = new DicomTagLO(0x0040, 0x050A);

        ///<summary>(0040,0512) VR=LO VM=1 Container Identifier</summary>
        public readonly static DicomTagLO ContainerIdentifier = new DicomTagLO(0x0040, 0x0512);

        ///<summary>(0040,0513) VR=SQ VM=1 Issuer of the Container Identifier Sequence</summary>
        public readonly static DicomTagSQ IssuerOfTheContainerIdentifierSequence = new DicomTagSQ(0x0040, 0x0513);

        ///<summary>(0040,0515) VR=SQ VM=1 Alternate Container Identifier Sequence</summary>
        public readonly static DicomTagSQ AlternateContainerIdentifierSequence = new DicomTagSQ(0x0040, 0x0515);

        ///<summary>(0040,0518) VR=SQ VM=1 Container Type Code Sequence</summary>
        public readonly static DicomTagSQ ContainerTypeCodeSequence = new DicomTagSQ(0x0040, 0x0518);

        ///<summary>(0040,051A) VR=LO VM=1 Container Description</summary>
        public readonly static DicomTagLO ContainerDescription = new DicomTagLO(0x0040, 0x051A);

        ///<summary>(0040,0520) VR=SQ VM=1 Container Component Sequence</summary>
        public readonly static DicomTagSQ ContainerComponentSequence = new DicomTagSQ(0x0040, 0x0520);

        ///<summary>(0040,0550) VR=SQ VM=1 Specimen Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ SpecimenSequenceRETIRED = new DicomTagSQ(0x0040, 0x0550);

        ///<summary>(0040,0551) VR=LO VM=1 Specimen Identifier</summary>
        public readonly static DicomTagLO SpecimenIdentifier = new DicomTagLO(0x0040, 0x0551);

        ///<summary>(0040,0552) VR=SQ VM=1 Specimen Description Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ SpecimenDescriptionSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0x0552);

        ///<summary>(0040,0553) VR=ST VM=1 Specimen Description (Trial) (RETIRED)</summary>
        public readonly static DicomTagST SpecimenDescriptionTrialRETIRED = new DicomTagST(0x0040, 0x0553);

        ///<summary>(0040,0554) VR=UI VM=1 Specimen UID</summary>
        public readonly static DicomTagUI SpecimenUID = new DicomTagUI(0x0040, 0x0554);

        ///<summary>(0040,0555) VR=SQ VM=1 Acquisition Context Sequence</summary>
        public readonly static DicomTagSQ AcquisitionContextSequence = new DicomTagSQ(0x0040, 0x0555);

        ///<summary>(0040,0556) VR=ST VM=1 Acquisition Context Description</summary>
        public readonly static DicomTagST AcquisitionContextDescription = new DicomTagST(0x0040, 0x0556);

        ///<summary>(0040,059A) VR=SQ VM=1 Specimen Type Code Sequence</summary>
        public readonly static DicomTagSQ SpecimenTypeCodeSequence = new DicomTagSQ(0x0040, 0x059A);

        ///<summary>(0040,0560) VR=SQ VM=1 Specimen Description Sequence</summary>
        public readonly static DicomTagSQ SpecimenDescriptionSequence = new DicomTagSQ(0x0040, 0x0560);

        ///<summary>(0040,0562) VR=SQ VM=1 Issuer of the Specimen Identifier Sequence</summary>
        public readonly static DicomTagSQ IssuerOfTheSpecimenIdentifierSequence = new DicomTagSQ(0x0040, 0x0562);

        ///<summary>(0040,0600) VR=LO VM=1 Specimen Short Description</summary>
        public readonly static DicomTagLO SpecimenShortDescription = new DicomTagLO(0x0040, 0x0600);

        ///<summary>(0040,0602) VR=UT VM=1 Specimen Detailed Description</summary>
        public readonly static DicomTagUT SpecimenDetailedDescription = new DicomTagUT(0x0040, 0x0602);

        ///<summary>(0040,0610) VR=SQ VM=1 Specimen Preparation Sequence</summary>
        public readonly static DicomTagSQ SpecimenPreparationSequence = new DicomTagSQ(0x0040, 0x0610);

        ///<summary>(0040,0612) VR=SQ VM=1 Specimen Preparation Step Content Item Sequence</summary>
        public readonly static DicomTagSQ SpecimenPreparationStepContentItemSequence = new DicomTagSQ(0x0040, 0x0612);

        ///<summary>(0040,0620) VR=SQ VM=1 Specimen Localization Content Item Sequence</summary>
        public readonly static DicomTagSQ SpecimenLocalizationContentItemSequence = new DicomTagSQ(0x0040, 0x0620);

        ///<summary>(0040,06FA) VR=LO VM=1 Slide Identifier (RETIRED)</summary>
        public readonly static DicomTagLO SlideIdentifierRETIRED = new DicomTagLO(0x0040, 0x06FA);

        ///<summary>(0040,0710) VR=SQ VM=1 Whole Slide Microscopy Image Frame Type Sequence</summary>
        public readonly static DicomTagSQ WholeSlideMicroscopyImageFrameTypeSequence = new DicomTagSQ(0x0040, 0x0710);

        ///<summary>(0040,071A) VR=SQ VM=1 Image Center Point Coordinates Sequence</summary>
        public readonly static DicomTagSQ ImageCenterPointCoordinatesSequence = new DicomTagSQ(0x0040, 0x071A);

        ///<summary>(0040,072A) VR=DS VM=1 X Offset in Slide Coordinate System</summary>
        public readonly static DicomTagDS XOffsetInSlideCoordinateSystem = new DicomTagDS(0x0040, 0x072A);

        ///<summary>(0040,073A) VR=DS VM=1 Y Offset in Slide Coordinate System</summary>
        public readonly static DicomTagDS YOffsetInSlideCoordinateSystem = new DicomTagDS(0x0040, 0x073A);

        ///<summary>(0040,074A) VR=DS VM=1 Z Offset in Slide Coordinate System</summary>
        public readonly static DicomTagDS ZOffsetInSlideCoordinateSystem = new DicomTagDS(0x0040, 0x074A);

        ///<summary>(0040,08D8) VR=SQ VM=1 Pixel Spacing Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ PixelSpacingSequenceRETIRED = new DicomTagSQ(0x0040, 0x08D8);

        ///<summary>(0040,08DA) VR=SQ VM=1 Coordinate System Axis Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ CoordinateSystemAxisCodeSequenceRETIRED = new DicomTagSQ(0x0040, 0x08DA);

        ///<summary>(0040,08EA) VR=SQ VM=1 Measurement Units Code Sequence</summary>
        public readonly static DicomTagSQ MeasurementUnitsCodeSequence = new DicomTagSQ(0x0040, 0x08EA);

        ///<summary>(0040,09F8) VR=SQ VM=1 Vital Stain Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ VitalStainCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0x09F8);

        ///<summary>(0040,1001) VR=SH VM=1 Requested Procedure ID</summary>
        public readonly static DicomTagSH RequestedProcedureID = new DicomTagSH(0x0040, 0x1001);

        ///<summary>(0040,1002) VR=LO VM=1 Reason for the Requested Procedure</summary>
        public readonly static DicomTagLO ReasonForTheRequestedProcedure = new DicomTagLO(0x0040, 0x1002);

        ///<summary>(0040,1003) VR=SH VM=1 Requested Procedure Priority</summary>
        public readonly static DicomTagSH RequestedProcedurePriority = new DicomTagSH(0x0040, 0x1003);

        ///<summary>(0040,1004) VR=LO VM=1 Patient Transport Arrangements</summary>
        public readonly static DicomTagLO PatientTransportArrangements = new DicomTagLO(0x0040, 0x1004);

        ///<summary>(0040,1005) VR=LO VM=1 Requested Procedure Location</summary>
        public readonly static DicomTagLO RequestedProcedureLocation = new DicomTagLO(0x0040, 0x1005);

        ///<summary>(0040,1006) VR=SH VM=1 Placer Order Number / Procedure (RETIRED)</summary>
        public readonly static DicomTagSH PlacerOrderNumberProcedureRETIRED = new DicomTagSH(0x0040, 0x1006);

        ///<summary>(0040,1007) VR=SH VM=1 Filler Order Number / Procedure (RETIRED)</summary>
        public readonly static DicomTagSH FillerOrderNumberProcedureRETIRED = new DicomTagSH(0x0040, 0x1007);

        ///<summary>(0040,1008) VR=LO VM=1 Confidentiality Code</summary>
        public readonly static DicomTagLO ConfidentialityCode = new DicomTagLO(0x0040, 0x1008);

        ///<summary>(0040,1009) VR=SH VM=1 Reporting Priority</summary>
        public readonly static DicomTagSH ReportingPriority = new DicomTagSH(0x0040, 0x1009);

        ///<summary>(0040,100A) VR=SQ VM=1 Reason for Requested Procedure Code Sequence</summary>
        public readonly static DicomTagSQ ReasonForRequestedProcedureCodeSequence = new DicomTagSQ(0x0040, 0x100A);

        ///<summary>(0040,1010) VR=PN VM=1-n Names of Intended Recipients of Results</summary>
        public readonly static DicomTagPNs NamesOfIntendedRecipientsOfResults = new DicomTagPNs(0x0040, 0x1010);

        ///<summary>(0040,1011) VR=SQ VM=1 Intended Recipients of Results Identification Sequence</summary>
        public readonly static DicomTagSQ IntendedRecipientsOfResultsIdentificationSequence = new DicomTagSQ(0x0040, 0x1011);

        ///<summary>(0040,1012) VR=SQ VM=1 Reason For Performed Procedure Code Sequence</summary>
        public readonly static DicomTagSQ ReasonForPerformedProcedureCodeSequence = new DicomTagSQ(0x0040, 0x1012);

        ///<summary>(0040,1060) VR=LO VM=1 Requested Procedure Description (Trial) (RETIRED)</summary>
        public readonly static DicomTagLO RequestedProcedureDescriptionTrialRETIRED = new DicomTagLO(0x0040, 0x1060);

        ///<summary>(0040,1101) VR=SQ VM=1 Person Identification Code Sequence</summary>
        public readonly static DicomTagSQ PersonIdentificationCodeSequence = new DicomTagSQ(0x0040, 0x1101);

        ///<summary>(0040,1102) VR=ST VM=1 Person's Address</summary>
        public readonly static DicomTagST PersonAddress = new DicomTagST(0x0040, 0x1102);

        ///<summary>(0040,1103) VR=LO VM=1-n Person's Telephone Numbers</summary>
        public readonly static DicomTagLOs PersonTelephoneNumbers = new DicomTagLOs(0x0040, 0x1103);

        ///<summary>(0040,1104) VR=LT VM=1 Person's Telecom Information</summary>
        public readonly static DicomTagLT PersonTelecomInformation = new DicomTagLT(0x0040, 0x1104);

        ///<summary>(0040,1400) VR=LT VM=1 Requested Procedure Comments</summary>
        public readonly static DicomTagLT RequestedProcedureComments = new DicomTagLT(0x0040, 0x1400);

        ///<summary>(0040,2001) VR=LO VM=1 Reason for the Imaging Service Request (RETIRED)</summary>
        public readonly static DicomTagLO ReasonForTheImagingServiceRequestRETIRED = new DicomTagLO(0x0040, 0x2001);

        ///<summary>(0040,2004) VR=DA VM=1 Issue Date of Imaging Service Request</summary>
        public readonly static DicomTagDA IssueDateOfImagingServiceRequest = new DicomTagDA(0x0040, 0x2004);

        ///<summary>(0040,2005) VR=TM VM=1 Issue Time of Imaging Service Request</summary>
        public readonly static DicomTagTM IssueTimeOfImagingServiceRequest = new DicomTagTM(0x0040, 0x2005);

        ///<summary>(0040,2006) VR=SH VM=1 Placer Order Number / Imaging Service Request (Retired) (RETIRED)</summary>
        public readonly static DicomTagSH PlacerOrderNumberImagingServiceRequestRetiredRETIRED = new DicomTagSH(0x0040, 0x2006);

        ///<summary>(0040,2007) VR=SH VM=1 Filler Order Number / Imaging Service Request (Retired) (RETIRED)</summary>
        public readonly static DicomTagSH FillerOrderNumberImagingServiceRequestRetiredRETIRED = new DicomTagSH(0x0040, 0x2007);

        ///<summary>(0040,2008) VR=PN VM=1 Order Entered By</summary>
        public readonly static DicomTagPN OrderEnteredBy = new DicomTagPN(0x0040, 0x2008);

        ///<summary>(0040,2009) VR=SH VM=1 Order Enterer's Location</summary>
        public readonly static DicomTagSH OrderEntererLocation = new DicomTagSH(0x0040, 0x2009);

        ///<summary>(0040,2010) VR=SH VM=1 Order Callback Phone Number</summary>
        public readonly static DicomTagSH OrderCallbackPhoneNumber = new DicomTagSH(0x0040, 0x2010);

        ///<summary>(0040,2011) VR=LT VM=1 Order Callback Telecom Information</summary>
        public readonly static DicomTagLT OrderCallbackTelecomInformation = new DicomTagLT(0x0040, 0x2011);

        ///<summary>(0040,2016) VR=LO VM=1 Placer Order Number / Imaging Service Request</summary>
        public readonly static DicomTagLO PlacerOrderNumberImagingServiceRequest = new DicomTagLO(0x0040, 0x2016);

        ///<summary>(0040,2017) VR=LO VM=1 Filler Order Number / Imaging Service Request</summary>
        public readonly static DicomTagLO FillerOrderNumberImagingServiceRequest = new DicomTagLO(0x0040, 0x2017);

        ///<summary>(0040,2400) VR=LT VM=1 Imaging Service Request Comments</summary>
        public readonly static DicomTagLT ImagingServiceRequestComments = new DicomTagLT(0x0040, 0x2400);

        ///<summary>(0040,3001) VR=LO VM=1 Confidentiality Constraint on Patient Data Description</summary>
        public readonly static DicomTagLO ConfidentialityConstraintOnPatientDataDescription = new DicomTagLO(0x0040, 0x3001);

        ///<summary>(0040,4001) VR=CS VM=1 General Purpose Scheduled Procedure Step Status (RETIRED)</summary>
        public readonly static DicomTagCS GeneralPurposeScheduledProcedureStepStatusRETIRED = new DicomTagCS(0x0040, 0x4001);

        ///<summary>(0040,4002) VR=CS VM=1 General Purpose Performed Procedure Step Status (RETIRED)</summary>
        public readonly static DicomTagCS GeneralPurposePerformedProcedureStepStatusRETIRED = new DicomTagCS(0x0040, 0x4002);

        ///<summary>(0040,4003) VR=CS VM=1 General Purpose Scheduled Procedure Step Priority (RETIRED)</summary>
        public readonly static DicomTagCS GeneralPurposeScheduledProcedureStepPriorityRETIRED = new DicomTagCS(0x0040, 0x4003);

        ///<summary>(0040,4004) VR=SQ VM=1 Scheduled Processing Applications Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ScheduledProcessingApplicationsCodeSequenceRETIRED = new DicomTagSQ(0x0040, 0x4004);

        ///<summary>(0040,4005) VR=DT VM=1 Scheduled Procedure Step Start DateTime</summary>
        public readonly static DicomTagDT ScheduledProcedureStepStartDateTime = new DicomTagDT(0x0040, 0x4005);

        ///<summary>(0040,4006) VR=CS VM=1 Multiple Copies Flag (RETIRED)</summary>
        public readonly static DicomTagCS MultipleCopiesFlagRETIRED = new DicomTagCS(0x0040, 0x4006);

        ///<summary>(0040,4007) VR=SQ VM=1 Performed Processing Applications Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ PerformedProcessingApplicationsCodeSequenceRETIRED = new DicomTagSQ(0x0040, 0x4007);

        ///<summary>(0040,4008) VR=DT VM=1 Scheduled Procedure Step Expiration DateTime</summary>
        public readonly static DicomTagDT ScheduledProcedureStepExpirationDateTime = new DicomTagDT(0x0040, 0x4008);

        ///<summary>(0040,4009) VR=SQ VM=1 Human Performer Code Sequence</summary>
        public readonly static DicomTagSQ HumanPerformerCodeSequence = new DicomTagSQ(0x0040, 0x4009);

        ///<summary>(0040,4010) VR=DT VM=1 Scheduled Procedure Step Modification DateTime</summary>
        public readonly static DicomTagDT ScheduledProcedureStepModificationDateTime = new DicomTagDT(0x0040, 0x4010);

        ///<summary>(0040,4011) VR=DT VM=1 Expected Completion DateTime</summary>
        public readonly static DicomTagDT ExpectedCompletionDateTime = new DicomTagDT(0x0040, 0x4011);

        ///<summary>(0040,4015) VR=SQ VM=1 Resulting General Purpose Performed Procedure Steps Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ResultingGeneralPurposePerformedProcedureStepsSequenceRETIRED = new DicomTagSQ(0x0040, 0x4015);

        ///<summary>(0040,4016) VR=SQ VM=1 Referenced General Purpose Scheduled Procedure Step Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedGeneralPurposeScheduledProcedureStepSequenceRETIRED = new DicomTagSQ(0x0040, 0x4016);

        ///<summary>(0040,4018) VR=SQ VM=1 Scheduled Workitem Code Sequence</summary>
        public readonly static DicomTagSQ ScheduledWorkitemCodeSequence = new DicomTagSQ(0x0040, 0x4018);

        ///<summary>(0040,4019) VR=SQ VM=1 Performed Workitem Code Sequence</summary>
        public readonly static DicomTagSQ PerformedWorkitemCodeSequence = new DicomTagSQ(0x0040, 0x4019);

        ///<summary>(0040,4020) VR=CS VM=1 Input Availability Flag (RETIRED)</summary>
        public readonly static DicomTagCS InputAvailabilityFlagRETIRED = new DicomTagCS(0x0040, 0x4020);

        ///<summary>(0040,4021) VR=SQ VM=1 Input Information Sequence</summary>
        public readonly static DicomTagSQ InputInformationSequence = new DicomTagSQ(0x0040, 0x4021);

        ///<summary>(0040,4022) VR=SQ VM=1 Relevant Information Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ RelevantInformationSequenceRETIRED = new DicomTagSQ(0x0040, 0x4022);

        ///<summary>(0040,4023) VR=UI VM=1 Referenced General Purpose Scheduled Procedure Step Transaction UID (RETIRED)</summary>
        public readonly static DicomTagUI ReferencedGeneralPurposeScheduledProcedureStepTransactionUIDRETIRED = new DicomTagUI(0x0040, 0x4023);

        ///<summary>(0040,4025) VR=SQ VM=1 Scheduled Station Name Code Sequence</summary>
        public readonly static DicomTagSQ ScheduledStationNameCodeSequence = new DicomTagSQ(0x0040, 0x4025);

        ///<summary>(0040,4026) VR=SQ VM=1 Scheduled Station Class Code Sequence</summary>
        public readonly static DicomTagSQ ScheduledStationClassCodeSequence = new DicomTagSQ(0x0040, 0x4026);

        ///<summary>(0040,4027) VR=SQ VM=1 Scheduled Station Geographic Location Code Sequence</summary>
        public readonly static DicomTagSQ ScheduledStationGeographicLocationCodeSequence = new DicomTagSQ(0x0040, 0x4027);

        ///<summary>(0040,4028) VR=SQ VM=1 Performed Station Name Code Sequence</summary>
        public readonly static DicomTagSQ PerformedStationNameCodeSequence = new DicomTagSQ(0x0040, 0x4028);

        ///<summary>(0040,4029) VR=SQ VM=1 Performed Station Class Code Sequence</summary>
        public readonly static DicomTagSQ PerformedStationClassCodeSequence = new DicomTagSQ(0x0040, 0x4029);

        ///<summary>(0040,4030) VR=SQ VM=1 Performed Station Geographic Location Code Sequence</summary>
        public readonly static DicomTagSQ PerformedStationGeographicLocationCodeSequence = new DicomTagSQ(0x0040, 0x4030);

        ///<summary>(0040,4031) VR=SQ VM=1 Requested Subsequent Workitem Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ RequestedSubsequentWorkitemCodeSequenceRETIRED = new DicomTagSQ(0x0040, 0x4031);

        ///<summary>(0040,4032) VR=SQ VM=1 Non-DICOM Output Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ NonDICOMOutputCodeSequenceRETIRED = new DicomTagSQ(0x0040, 0x4032);

        ///<summary>(0040,4033) VR=SQ VM=1 Output Information Sequence</summary>
        public readonly static DicomTagSQ OutputInformationSequence = new DicomTagSQ(0x0040, 0x4033);

        ///<summary>(0040,4034) VR=SQ VM=1 Scheduled Human Performers Sequence</summary>
        public readonly static DicomTagSQ ScheduledHumanPerformersSequence = new DicomTagSQ(0x0040, 0x4034);

        ///<summary>(0040,4035) VR=SQ VM=1 Actual Human Performers Sequence</summary>
        public readonly static DicomTagSQ ActualHumanPerformersSequence = new DicomTagSQ(0x0040, 0x4035);

        ///<summary>(0040,4036) VR=LO VM=1 Human Performer's Organization</summary>
        public readonly static DicomTagLO HumanPerformerOrganization = new DicomTagLO(0x0040, 0x4036);

        ///<summary>(0040,4037) VR=PN VM=1 Human Performer's Name</summary>
        public readonly static DicomTagPN HumanPerformerName = new DicomTagPN(0x0040, 0x4037);

        ///<summary>(0040,4040) VR=CS VM=1 Raw Data Handling</summary>
        public readonly static DicomTagCS RawDataHandling = new DicomTagCS(0x0040, 0x4040);

        ///<summary>(0040,4041) VR=CS VM=1 Input Readiness State</summary>
        public readonly static DicomTagCS InputReadinessState = new DicomTagCS(0x0040, 0x4041);

        ///<summary>(0040,4050) VR=DT VM=1 Performed Procedure Step Start DateTime</summary>
        public readonly static DicomTagDT PerformedProcedureStepStartDateTime = new DicomTagDT(0x0040, 0x4050);

        ///<summary>(0040,4051) VR=DT VM=1 Performed Procedure Step End DateTime</summary>
        public readonly static DicomTagDT PerformedProcedureStepEndDateTime = new DicomTagDT(0x0040, 0x4051);

        ///<summary>(0040,4052) VR=DT VM=1 Procedure Step Cancellation DateTime</summary>
        public readonly static DicomTagDT ProcedureStepCancellationDateTime = new DicomTagDT(0x0040, 0x4052);

        ///<summary>(0040,4070) VR=SQ VM=1 Output Destination Sequence</summary>
        public readonly static DicomTagSQ OutputDestinationSequence = new DicomTagSQ(0x0040, 0x4070);

        ///<summary>(0040,4071) VR=SQ VM=1 DICOM Storage Sequence</summary>
        public readonly static DicomTagSQ DICOMStorageSequence = new DicomTagSQ(0x0040, 0x4071);

        ///<summary>(0040,4072) VR=SQ VM=1 STOW-RS Storage Sequence</summary>
        public readonly static DicomTagSQ STOWRSStorageSequence = new DicomTagSQ(0x0040, 0x4072);

        ///<summary>(0040,4073) VR=UR VM=1 Storage URL</summary>
        public readonly static DicomTagUR StorageURL = new DicomTagUR(0x0040, 0x4073);

        ///<summary>(0040,4074) VR=SQ VM=1 XDS Storage Sequence</summary>
        public readonly static DicomTagSQ XDSStorageSequence = new DicomTagSQ(0x0040, 0x4074);

        ///<summary>(0040,8302) VR=DS VM=1 Entrance Dose in mGy</summary>
        public readonly static DicomTagDS EntranceDoseInmGy = new DicomTagDS(0x0040, 0x8302);

        ///<summary>(0040,8303) VR=CS VM=1 Entrance Dose Derivation</summary>
        public readonly static DicomTagCS EntranceDoseDerivation = new DicomTagCS(0x0040, 0x8303);

        ///<summary>(0040,9092) VR=SQ VM=1 Parametric Map Frame Type Sequence</summary>
        public readonly static DicomTagSQ ParametricMapFrameTypeSequence = new DicomTagSQ(0x0040, 0x9092);

        ///<summary>(0040,9094) VR=SQ VM=1 Referenced Image Real World Value Mapping Sequence</summary>
        public readonly static DicomTagSQ ReferencedImageRealWorldValueMappingSequence = new DicomTagSQ(0x0040, 0x9094);

        ///<summary>(0040,9096) VR=SQ VM=1 Real World Value Mapping Sequence</summary>
        public readonly static DicomTagSQ RealWorldValueMappingSequence = new DicomTagSQ(0x0040, 0x9096);

        ///<summary>(0040,9098) VR=SQ VM=1 Pixel Value Mapping Code Sequence</summary>
        public readonly static DicomTagSQ PixelValueMappingCodeSequence = new DicomTagSQ(0x0040, 0x9098);

        ///<summary>(0040,9210) VR=SH VM=1 LUT Label</summary>
        public readonly static DicomTagSH LUTLabel = new DicomTagSH(0x0040, 0x9210);

        ///<summary>(0040,9211) VR=US/SS VM=1 Real World Value Last Value Mapped</summary>
        public readonly static DicomTagUSSS RealWorldValueLastValueMapped = new DicomTagUSSS(0x0040, 0x9211);

        ///<summary>(0040,9212) VR=FD VM=1-n Real World Value LUT Data</summary>
        public readonly static DicomTagFDs RealWorldValueLUTData = new DicomTagFDs(0x0040, 0x9212);

        ///<summary>(0040,9213) VR=FD VM=1 Double Float Real World Value Last Value Mapped</summary>
        public readonly static DicomTagFD DoubleFloatRealWorldValueLastValueMapped = new DicomTagFD(0x0040, 0x9213);

        ///<summary>(0040,9214) VR=FD VM=1 Double Float Real World Value First Value Mapped</summary>
        public readonly static DicomTagFD DoubleFloatRealWorldValueFirstValueMapped = new DicomTagFD(0x0040, 0x9214);

        ///<summary>(0040,9216) VR=US/SS VM=1 Real World Value First Value Mapped</summary>
        public readonly static DicomTagUSSS RealWorldValueFirstValueMapped = new DicomTagUSSS(0x0040, 0x9216);

        ///<summary>(0040,9220) VR=SQ VM=1 Quantity Definition Sequence</summary>
        public readonly static DicomTagSQ QuantityDefinitionSequence = new DicomTagSQ(0x0040, 0x9220);

        ///<summary>(0040,9224) VR=FD VM=1 Real World Value Intercept</summary>
        public readonly static DicomTagFD RealWorldValueIntercept = new DicomTagFD(0x0040, 0x9224);

        ///<summary>(0040,9225) VR=FD VM=1 Real World Value Slope</summary>
        public readonly static DicomTagFD RealWorldValueSlope = new DicomTagFD(0x0040, 0x9225);

        ///<summary>(0040,A007) VR=CS VM=1 Findings Flag (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS FindingsFlagTrialRETIRED = new DicomTagCS(0x0040, 0xA007);

        ///<summary>(0040,A010) VR=CS VM=1 Relationship Type</summary>
        public readonly static DicomTagCS RelationshipType = new DicomTagCS(0x0040, 0xA010);

        ///<summary>(0040,A020) VR=SQ VM=1 Findings Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ FindingsSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA020);

        ///<summary>(0040,A021) VR=UI VM=1 Findings Group UID (Trial) (RETIRED)</summary>
        public readonly static DicomTagUI FindingsGroupUIDTrialRETIRED = new DicomTagUI(0x0040, 0xA021);

        ///<summary>(0040,A022) VR=UI VM=1 Referenced Findings Group UID (Trial) (RETIRED)</summary>
        public readonly static DicomTagUI ReferencedFindingsGroupUIDTrialRETIRED = new DicomTagUI(0x0040, 0xA022);

        ///<summary>(0040,A023) VR=DA VM=1 Findings Group Recording Date (Trial) (RETIRED)</summary>
        public readonly static DicomTagDA FindingsGroupRecordingDateTrialRETIRED = new DicomTagDA(0x0040, 0xA023);

        ///<summary>(0040,A024) VR=TM VM=1 Findings Group Recording Time (Trial) (RETIRED)</summary>
        public readonly static DicomTagTM FindingsGroupRecordingTimeTrialRETIRED = new DicomTagTM(0x0040, 0xA024);

        ///<summary>(0040,A026) VR=SQ VM=1 Findings Source Category Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ FindingsSourceCategoryCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA026);

        ///<summary>(0040,A027) VR=LO VM=1 Verifying Organization</summary>
        public readonly static DicomTagLO VerifyingOrganization = new DicomTagLO(0x0040, 0xA027);

        ///<summary>(0040,A028) VR=SQ VM=1 Documenting Organization Identifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ DocumentingOrganizationIdentifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA028);

        ///<summary>(0040,A030) VR=DT VM=1 Verification DateTime</summary>
        public readonly static DicomTagDT VerificationDateTime = new DicomTagDT(0x0040, 0xA030);

        ///<summary>(0040,A032) VR=DT VM=1 Observation DateTime</summary>
        public readonly static DicomTagDT ObservationDateTime = new DicomTagDT(0x0040, 0xA032);

        ///<summary>(0040,A033) VR=DT VM=1 Observation Start DateTime</summary>
        public readonly static DicomTagDT ObservationStartDateTime = new DicomTagDT(0x0040, 0xA033);

        ///<summary>(0040,A034) VR=DT VM=1 Effective Start DateTime</summary>
        public readonly static DicomTagDT EffectiveStartDateTime = new DicomTagDT(0x0040, 0xA034);

        ///<summary>(0040,A035) VR=DT VM=1 Effective Stop DateTime</summary>
        public readonly static DicomTagDT EffectiveStopDateTime = new DicomTagDT(0x0040, 0xA035);

        ///<summary>(0040,A040) VR=CS VM=1 Value Type</summary>
        public readonly static DicomTagCS ValueType = new DicomTagCS(0x0040, 0xA040);

        ///<summary>(0040,A043) VR=SQ VM=1 Concept Name Code Sequence</summary>
        public readonly static DicomTagSQ ConceptNameCodeSequence = new DicomTagSQ(0x0040, 0xA043);

        ///<summary>(0040,A047) VR=LO VM=1 Measurement Precision Description (Trial) (RETIRED)</summary>
        public readonly static DicomTagLO MeasurementPrecisionDescriptionTrialRETIRED = new DicomTagLO(0x0040, 0xA047);

        ///<summary>(0040,A050) VR=CS VM=1 Continuity Of Content</summary>
        public readonly static DicomTagCS ContinuityOfContent = new DicomTagCS(0x0040, 0xA050);

        ///<summary>(0040,A057) VR=CS VM=1-n Urgency or Priority Alerts (Trial) (RETIRED)</summary>
        public readonly static DicomTagCSs UrgencyOrPriorityAlertsTrialRETIRED = new DicomTagCSs(0x0040, 0xA057);

        ///<summary>(0040,A060) VR=LO VM=1 Sequencing Indicator (Trial) (RETIRED)</summary>
        public readonly static DicomTagLO SequencingIndicatorTrialRETIRED = new DicomTagLO(0x0040, 0xA060);

        ///<summary>(0040,A066) VR=SQ VM=1 Document Identifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ DocumentIdentifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA066);

        ///<summary>(0040,A067) VR=PN VM=1 Document Author (Trial) (RETIRED)</summary>
        public readonly static DicomTagPN DocumentAuthorTrialRETIRED = new DicomTagPN(0x0040, 0xA067);

        ///<summary>(0040,A068) VR=SQ VM=1 Document Author Identifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ DocumentAuthorIdentifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA068);

        ///<summary>(0040,A070) VR=SQ VM=1 Identifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ IdentifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA070);

        ///<summary>(0040,A073) VR=SQ VM=1 Verifying Observer Sequence</summary>
        public readonly static DicomTagSQ VerifyingObserverSequence = new DicomTagSQ(0x0040, 0xA073);

        ///<summary>(0040,A074) VR=OB VM=1 Object Binary Identifier (Trial) (RETIRED)</summary>
        public readonly static DicomTagOB ObjectBinaryIdentifierTrialRETIRED = new DicomTagOB(0x0040, 0xA074);

        ///<summary>(0040,A075) VR=PN VM=1 Verifying Observer Name</summary>
        public readonly static DicomTagPN VerifyingObserverName = new DicomTagPN(0x0040, 0xA075);

        ///<summary>(0040,A076) VR=SQ VM=1 Documenting Observer Identifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ DocumentingObserverIdentifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA076);

        ///<summary>(0040,A078) VR=SQ VM=1 Author Observer Sequence</summary>
        public readonly static DicomTagSQ AuthorObserverSequence = new DicomTagSQ(0x0040, 0xA078);

        ///<summary>(0040,A07A) VR=SQ VM=1 Participant Sequence</summary>
        public readonly static DicomTagSQ ParticipantSequence = new DicomTagSQ(0x0040, 0xA07A);

        ///<summary>(0040,A07C) VR=SQ VM=1 Custodial Organization Sequence</summary>
        public readonly static DicomTagSQ CustodialOrganizationSequence = new DicomTagSQ(0x0040, 0xA07C);

        ///<summary>(0040,A080) VR=CS VM=1 Participation Type</summary>
        public readonly static DicomTagCS ParticipationType = new DicomTagCS(0x0040, 0xA080);

        ///<summary>(0040,A082) VR=DT VM=1 Participation DateTime</summary>
        public readonly static DicomTagDT ParticipationDateTime = new DicomTagDT(0x0040, 0xA082);

        ///<summary>(0040,A084) VR=CS VM=1 Observer Type</summary>
        public readonly static DicomTagCS ObserverType = new DicomTagCS(0x0040, 0xA084);

        ///<summary>(0040,A085) VR=SQ VM=1 Procedure Identifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ ProcedureIdentifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA085);

        ///<summary>(0040,A088) VR=SQ VM=1 Verifying Observer Identification Code Sequence</summary>
        public readonly static DicomTagSQ VerifyingObserverIdentificationCodeSequence = new DicomTagSQ(0x0040, 0xA088);

        ///<summary>(0040,A089) VR=OB VM=1 Object Directory Binary Identifier (Trial) (RETIRED)</summary>
        public readonly static DicomTagOB ObjectDirectoryBinaryIdentifierTrialRETIRED = new DicomTagOB(0x0040, 0xA089);

        ///<summary>(0040,A090) VR=SQ VM=1 Equivalent CDA Document Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ EquivalentCDADocumentSequenceRETIRED = new DicomTagSQ(0x0040, 0xA090);

        ///<summary>(0040,A0B0) VR=US VM=2-2n Referenced Waveform Channels</summary>
        public readonly static DicomTagUSs ReferencedWaveformChannels = new DicomTagUSs(0x0040, 0xA0B0);

        ///<summary>(0040,A110) VR=DA VM=1 Date of Document or Verbal Transaction (Trial) (RETIRED)</summary>
        public readonly static DicomTagDA DateOfDocumentOrVerbalTransactionTrialRETIRED = new DicomTagDA(0x0040, 0xA110);

        ///<summary>(0040,A112) VR=TM VM=1 Time of Document Creation or Verbal Transaction (Trial) (RETIRED)</summary>
        public readonly static DicomTagTM TimeOfDocumentCreationOrVerbalTransactionTrialRETIRED = new DicomTagTM(0x0040, 0xA112);

        ///<summary>(0040,A120) VR=DT VM=1 DateTime</summary>
        public readonly static DicomTagDT DateTime = new DicomTagDT(0x0040, 0xA120);

        ///<summary>(0040,A121) VR=DA VM=1 Date</summary>
        public readonly static DicomTagDA Date = new DicomTagDA(0x0040, 0xA121);

        ///<summary>(0040,A122) VR=TM VM=1 Time</summary>
        public readonly static DicomTagTM Time = new DicomTagTM(0x0040, 0xA122);

        ///<summary>(0040,A123) VR=PN VM=1 Person Name</summary>
        public readonly static DicomTagPN PersonName = new DicomTagPN(0x0040, 0xA123);

        ///<summary>(0040,A124) VR=UI VM=1 UID</summary>
        public readonly static DicomTagUI UID = new DicomTagUI(0x0040, 0xA124);

        ///<summary>(0040,A125) VR=CS VM=2 Report Status ID (Trial) (RETIRED)</summary>
        public readonly static DicomTagCSs ReportStatusIDTrialRETIRED = new DicomTagCSs(0x0040, 0xA125);

        ///<summary>(0040,A130) VR=CS VM=1 Temporal Range Type</summary>
        public readonly static DicomTagCS TemporalRangeType = new DicomTagCS(0x0040, 0xA130);

        ///<summary>(0040,A132) VR=UL VM=1-n Referenced Sample Positions</summary>
        public readonly static DicomTagULs ReferencedSamplePositions = new DicomTagULs(0x0040, 0xA132);

        ///<summary>(0040,A136) VR=US VM=1-n Referenced Frame Numbers (RETIRED)</summary>
        public readonly static DicomTagUSs ReferencedFrameNumbersRETIRED = new DicomTagUSs(0x0040, 0xA136);

        ///<summary>(0040,A138) VR=DS VM=1-n Referenced Time Offsets</summary>
        public readonly static DicomTagDSs ReferencedTimeOffsets = new DicomTagDSs(0x0040, 0xA138);

        ///<summary>(0040,A13A) VR=DT VM=1-n Referenced DateTime</summary>
        public readonly static DicomTagDTs ReferencedDateTime = new DicomTagDTs(0x0040, 0xA13A);

        ///<summary>(0040,A160) VR=UT VM=1 Text Value</summary>
        public readonly static DicomTagUT TextValue = new DicomTagUT(0x0040, 0xA160);

        ///<summary>(0040,A161) VR=FD VM=1-n Floating Point Value</summary>
        public readonly static DicomTagFDs FloatingPointValue = new DicomTagFDs(0x0040, 0xA161);

        ///<summary>(0040,A162) VR=SL VM=1-n Rational Numerator Value</summary>
        public readonly static DicomTagSLs RationalNumeratorValue = new DicomTagSLs(0x0040, 0xA162);

        ///<summary>(0040,A163) VR=UL VM=1-n Rational Denominator Value</summary>
        public readonly static DicomTagULs RationalDenominatorValue = new DicomTagULs(0x0040, 0xA163);

        ///<summary>(0040,A167) VR=SQ VM=1 Observation Category Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ ObservationCategoryCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA167);

        ///<summary>(0040,A168) VR=SQ VM=1 Concept Code Sequence</summary>
        public readonly static DicomTagSQ ConceptCodeSequence = new DicomTagSQ(0x0040, 0xA168);

        ///<summary>(0040,A16A) VR=ST VM=1 Bibliographic Citation (Trial) (RETIRED)</summary>
        public readonly static DicomTagST BibliographicCitationTrialRETIRED = new DicomTagST(0x0040, 0xA16A);

        ///<summary>(0040,A170) VR=SQ VM=1 Purpose of Reference Code Sequence</summary>
        public readonly static DicomTagSQ PurposeOfReferenceCodeSequence = new DicomTagSQ(0x0040, 0xA170);

        ///<summary>(0040,A171) VR=UI VM=1 Observation UID</summary>
        public readonly static DicomTagUI ObservationUID = new DicomTagUI(0x0040, 0xA171);

        ///<summary>(0040,A172) VR=UI VM=1 Referenced Observation UID (Trial) (RETIRED)</summary>
        public readonly static DicomTagUI ReferencedObservationUIDTrialRETIRED = new DicomTagUI(0x0040, 0xA172);

        ///<summary>(0040,A173) VR=CS VM=1 Referenced Observation Class (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS ReferencedObservationClassTrialRETIRED = new DicomTagCS(0x0040, 0xA173);

        ///<summary>(0040,A174) VR=CS VM=1 Referenced Object Observation Class (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS ReferencedObjectObservationClassTrialRETIRED = new DicomTagCS(0x0040, 0xA174);

        ///<summary>(0040,A180) VR=US VM=1 Annotation Group Number</summary>
        public readonly static DicomTagUS AnnotationGroupNumber = new DicomTagUS(0x0040, 0xA180);

        ///<summary>(0040,A192) VR=DA VM=1 Observation Date (Trial) (RETIRED)</summary>
        public readonly static DicomTagDA ObservationDateTrialRETIRED = new DicomTagDA(0x0040, 0xA192);

        ///<summary>(0040,A193) VR=TM VM=1 Observation Time (Trial) (RETIRED)</summary>
        public readonly static DicomTagTM ObservationTimeTrialRETIRED = new DicomTagTM(0x0040, 0xA193);

        ///<summary>(0040,A194) VR=CS VM=1 Measurement Automation (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS MeasurementAutomationTrialRETIRED = new DicomTagCS(0x0040, 0xA194);

        ///<summary>(0040,A195) VR=SQ VM=1 Modifier Code Sequence</summary>
        public readonly static DicomTagSQ ModifierCodeSequence = new DicomTagSQ(0x0040, 0xA195);

        ///<summary>(0040,A224) VR=ST VM=1 Identification Description (Trial) (RETIRED)</summary>
        public readonly static DicomTagST IdentificationDescriptionTrialRETIRED = new DicomTagST(0x0040, 0xA224);

        ///<summary>(0040,A290) VR=CS VM=1 Coordinates Set Geometric Type (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS CoordinatesSetGeometricTypeTrialRETIRED = new DicomTagCS(0x0040, 0xA290);

        ///<summary>(0040,A296) VR=SQ VM=1 Algorithm Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ AlgorithmCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA296);

        ///<summary>(0040,A297) VR=ST VM=1 Algorithm Description (Trial) (RETIRED)</summary>
        public readonly static DicomTagST AlgorithmDescriptionTrialRETIRED = new DicomTagST(0x0040, 0xA297);

        ///<summary>(0040,A29A) VR=SL VM=2-2n Pixel Coordinates Set (Trial) (RETIRED)</summary>
        public readonly static DicomTagSLs PixelCoordinatesSetTrialRETIRED = new DicomTagSLs(0x0040, 0xA29A);

        ///<summary>(0040,A300) VR=SQ VM=1 Measured Value Sequence</summary>
        public readonly static DicomTagSQ MeasuredValueSequence = new DicomTagSQ(0x0040, 0xA300);

        ///<summary>(0040,A301) VR=SQ VM=1 Numeric Value Qualifier Code Sequence</summary>
        public readonly static DicomTagSQ NumericValueQualifierCodeSequence = new DicomTagSQ(0x0040, 0xA301);

        ///<summary>(0040,A307) VR=PN VM=1 Current Observer (Trial) (RETIRED)</summary>
        public readonly static DicomTagPN CurrentObserverTrialRETIRED = new DicomTagPN(0x0040, 0xA307);

        ///<summary>(0040,A30A) VR=DS VM=1-n Numeric Value</summary>
        public readonly static DicomTagDSs NumericValue = new DicomTagDSs(0x0040, 0xA30A);

        ///<summary>(0040,A313) VR=SQ VM=1 Referenced Accession Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedAccessionSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA313);

        ///<summary>(0040,A33A) VR=ST VM=1 Report Status Comment (Trial) (RETIRED)</summary>
        public readonly static DicomTagST ReportStatusCommentTrialRETIRED = new DicomTagST(0x0040, 0xA33A);

        ///<summary>(0040,A340) VR=SQ VM=1 Procedure Context Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ ProcedureContextSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA340);

        ///<summary>(0040,A352) VR=PN VM=1 Verbal Source (Trial) (RETIRED)</summary>
        public readonly static DicomTagPN VerbalSourceTrialRETIRED = new DicomTagPN(0x0040, 0xA352);

        ///<summary>(0040,A353) VR=ST VM=1 Address (Trial) (RETIRED)</summary>
        public readonly static DicomTagST AddressTrialRETIRED = new DicomTagST(0x0040, 0xA353);

        ///<summary>(0040,A354) VR=LO VM=1 Telephone Number (Trial) (RETIRED)</summary>
        public readonly static DicomTagLO TelephoneNumberTrialRETIRED = new DicomTagLO(0x0040, 0xA354);

        ///<summary>(0040,A358) VR=SQ VM=1 Verbal Source Identifier Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ VerbalSourceIdentifierCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA358);

        ///<summary>(0040,A360) VR=SQ VM=1 Predecessor Documents Sequence</summary>
        public readonly static DicomTagSQ PredecessorDocumentsSequence = new DicomTagSQ(0x0040, 0xA360);

        ///<summary>(0040,A370) VR=SQ VM=1 Referenced Request Sequence</summary>
        public readonly static DicomTagSQ ReferencedRequestSequence = new DicomTagSQ(0x0040, 0xA370);

        ///<summary>(0040,A372) VR=SQ VM=1 Performed Procedure Code Sequence</summary>
        public readonly static DicomTagSQ PerformedProcedureCodeSequence = new DicomTagSQ(0x0040, 0xA372);

        ///<summary>(0040,A375) VR=SQ VM=1 Current Requested Procedure Evidence Sequence</summary>
        public readonly static DicomTagSQ CurrentRequestedProcedureEvidenceSequence = new DicomTagSQ(0x0040, 0xA375);

        ///<summary>(0040,A380) VR=SQ VM=1 Report Detail Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ ReportDetailSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA380);

        ///<summary>(0040,A385) VR=SQ VM=1 Pertinent Other Evidence Sequence</summary>
        public readonly static DicomTagSQ PertinentOtherEvidenceSequence = new DicomTagSQ(0x0040, 0xA385);

        ///<summary>(0040,A390) VR=SQ VM=1 HL7 Structured Document Reference Sequence</summary>
        public readonly static DicomTagSQ HL7StructuredDocumentReferenceSequence = new DicomTagSQ(0x0040, 0xA390);

        ///<summary>(0040,A402) VR=UI VM=1 Observation Subject UID (Trial) (RETIRED)</summary>
        public readonly static DicomTagUI ObservationSubjectUIDTrialRETIRED = new DicomTagUI(0x0040, 0xA402);

        ///<summary>(0040,A403) VR=CS VM=1 Observation Subject Class (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS ObservationSubjectClassTrialRETIRED = new DicomTagCS(0x0040, 0xA403);

        ///<summary>(0040,A404) VR=SQ VM=1 Observation Subject Type Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ ObservationSubjectTypeCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA404);

        ///<summary>(0040,A491) VR=CS VM=1 Completion Flag</summary>
        public readonly static DicomTagCS CompletionFlag = new DicomTagCS(0x0040, 0xA491);

        ///<summary>(0040,A492) VR=LO VM=1 Completion Flag Description</summary>
        public readonly static DicomTagLO CompletionFlagDescription = new DicomTagLO(0x0040, 0xA492);

        ///<summary>(0040,A493) VR=CS VM=1 Verification Flag</summary>
        public readonly static DicomTagCS VerificationFlag = new DicomTagCS(0x0040, 0xA493);

        ///<summary>(0040,A494) VR=CS VM=1 Archive Requested</summary>
        public readonly static DicomTagCS ArchiveRequested = new DicomTagCS(0x0040, 0xA494);

        ///<summary>(0040,A496) VR=CS VM=1 Preliminary Flag</summary>
        public readonly static DicomTagCS PreliminaryFlag = new DicomTagCS(0x0040, 0xA496);

        ///<summary>(0040,A504) VR=SQ VM=1 Content Template Sequence</summary>
        public readonly static DicomTagSQ ContentTemplateSequence = new DicomTagSQ(0x0040, 0xA504);

        ///<summary>(0040,A525) VR=SQ VM=1 Identical Documents Sequence</summary>
        public readonly static DicomTagSQ IdenticalDocumentsSequence = new DicomTagSQ(0x0040, 0xA525);

        ///<summary>(0040,A600) VR=CS VM=1 Observation Subject Context Flag (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS ObservationSubjectContextFlagTrialRETIRED = new DicomTagCS(0x0040, 0xA600);

        ///<summary>(0040,A601) VR=CS VM=1 Observer Context Flag (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS ObserverContextFlagTrialRETIRED = new DicomTagCS(0x0040, 0xA601);

        ///<summary>(0040,A603) VR=CS VM=1 Procedure Context Flag (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS ProcedureContextFlagTrialRETIRED = new DicomTagCS(0x0040, 0xA603);

        ///<summary>(0040,A730) VR=SQ VM=1 Content Sequence</summary>
        public readonly static DicomTagSQ ContentSequence = new DicomTagSQ(0x0040, 0xA730);

        ///<summary>(0040,A731) VR=SQ VM=1 Relationship Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ RelationshipSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA731);

        ///<summary>(0040,A732) VR=SQ VM=1 Relationship Type Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ RelationshipTypeCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA732);

        ///<summary>(0040,A744) VR=SQ VM=1 Language Code Sequence (Trial) (RETIRED)</summary>
        public readonly static DicomTagSQ LanguageCodeSequenceTrialRETIRED = new DicomTagSQ(0x0040, 0xA744);

        ///<summary>(0040,A801) VR=SQ VM=1 Tabulated Values Sequence</summary>
        public readonly static DicomTagSQ TabulatedValuesSequence = new DicomTagSQ(0x0040, 0xA801);

        ///<summary>(0040,A802) VR=UL VM=1 Number of Table Rows</summary>
        public readonly static DicomTagUL NumberOfTableRows = new DicomTagUL(0x0040, 0xA802);

        ///<summary>(0040,A803) VR=UL VM=1 Number of Table Columns</summary>
        public readonly static DicomTagUL NumberOfTableColumns = new DicomTagUL(0x0040, 0xA803);

        ///<summary>(0040,A804) VR=UL VM=1 Table Row Number</summary>
        public readonly static DicomTagUL TableRowNumber = new DicomTagUL(0x0040, 0xA804);

        ///<summary>(0040,A805) VR=UL VM=1 Table Column Number</summary>
        public readonly static DicomTagUL TableColumnNumber = new DicomTagUL(0x0040, 0xA805);

        ///<summary>(0040,A806) VR=SQ VM=1 Table Row Definition Sequence</summary>
        public readonly static DicomTagSQ TableRowDefinitionSequence = new DicomTagSQ(0x0040, 0xA806);

        ///<summary>(0040,A807) VR=SQ VM=1 Table Column Definition Sequence</summary>
        public readonly static DicomTagSQ TableColumnDefinitionSequence = new DicomTagSQ(0x0040, 0xA807);

        ///<summary>(0040,A808) VR=SQ VM=1 Cell Values Sequence</summary>
        public readonly static DicomTagSQ CellValuesSequence = new DicomTagSQ(0x0040, 0xA808);

        ///<summary>(0040,A992) VR=ST VM=1 Uniform Resource Locator (Trial) (RETIRED)</summary>
        public readonly static DicomTagST UniformResourceLocatorTrialRETIRED = new DicomTagST(0x0040, 0xA992);

        ///<summary>(0040,B020) VR=SQ VM=1 Waveform Annotation Sequence</summary>
        public readonly static DicomTagSQ WaveformAnnotationSequence = new DicomTagSQ(0x0040, 0xB020);

        ///<summary>(0040,B030) VR=SQ VM=1 Structured Waveform Annotation Sequence</summary>
        public readonly static DicomTagSQ StructuredWaveformAnnotationSequence = new DicomTagSQ(0x0040, 0xB030);

        ///<summary>(0040,B031) VR=SQ VM=1 Waveform Annotation Display Selection Sequence</summary>
        public readonly static DicomTagSQ WaveformAnnotationDisplaySelectionSequence = new DicomTagSQ(0x0040, 0xB031);

        ///<summary>(0040,B032) VR=US VM=1 Referenced Montage Index</summary>
        public readonly static DicomTagUS ReferencedMontageIndex = new DicomTagUS(0x0040, 0xB032);

        ///<summary>(0040,B033) VR=SQ VM=1 Waveform Textual Annotation Sequence</summary>
        public readonly static DicomTagSQ WaveformTextualAnnotationSequence = new DicomTagSQ(0x0040, 0xB033);

        ///<summary>(0040,B034) VR=DT VM=1 Annotation DateTime</summary>
        public readonly static DicomTagDT AnnotationDateTime = new DicomTagDT(0x0040, 0xB034);

        ///<summary>(0040,B035) VR=SQ VM=1 Displayed Waveform Segment Sequence</summary>
        public readonly static DicomTagSQ DisplayedWaveformSegmentSequence = new DicomTagSQ(0x0040, 0xB035);

        ///<summary>(0040,B036) VR=DT VM=1 Segment Definition DateTime</summary>
        public readonly static DicomTagDT SegmentDefinitionDateTime = new DicomTagDT(0x0040, 0xB036);

        ///<summary>(0040,B037) VR=SQ VM=1 Montage Activation Sequence</summary>
        public readonly static DicomTagSQ MontageActivationSequence = new DicomTagSQ(0x0040, 0xB037);

        ///<summary>(0040,B038) VR=DS VM=1 Montage Activation Time Offset</summary>
        public readonly static DicomTagDS MontageActivationTimeOffset = new DicomTagDS(0x0040, 0xB038);

        ///<summary>(0040,B039) VR=SQ VM=1 Waveform Montage Sequence</summary>
        public readonly static DicomTagSQ WaveformMontageSequence = new DicomTagSQ(0x0040, 0xB039);

        ///<summary>(0040,B03A) VR=IS VM=1 Referenced Montage Channel Number</summary>
        public readonly static DicomTagIS ReferencedMontageChannelNumber = new DicomTagIS(0x0040, 0xB03A);

        ///<summary>(0040,B03B) VR=LT VM=1 Montage Name</summary>
        public readonly static DicomTagLT MontageName = new DicomTagLT(0x0040, 0xB03B);

        ///<summary>(0040,B03C) VR=SQ VM=1 Montage Channel Sequence</summary>
        public readonly static DicomTagSQ MontageChannelSequence = new DicomTagSQ(0x0040, 0xB03C);

        ///<summary>(0040,B03D) VR=US VM=1 Montage Index</summary>
        public readonly static DicomTagUS MontageIndex = new DicomTagUS(0x0040, 0xB03D);

        ///<summary>(0040,B03E) VR=IS VM=1 Montage Channel Number</summary>
        public readonly static DicomTagIS MontageChannelNumber = new DicomTagIS(0x0040, 0xB03E);

        ///<summary>(0040,B03F) VR=LO VM=1 Montage Channel Label</summary>
        public readonly static DicomTagLO MontageChannelLabel = new DicomTagLO(0x0040, 0xB03F);

        ///<summary>(0040,B040) VR=SQ VM=1 Montage Channel Source Code Sequence</summary>
        public readonly static DicomTagSQ MontageChannelSourceCodeSequence = new DicomTagSQ(0x0040, 0xB040);

        ///<summary>(0040,B041) VR=SQ VM=1 Contributing Channel Sources Sequence</summary>
        public readonly static DicomTagSQ ContributingChannelSourcesSequence = new DicomTagSQ(0x0040, 0xB041);

        ///<summary>(0040,B042) VR=FL VM=1 Channel Weight</summary>
        public readonly static DicomTagFL ChannelWeight = new DicomTagFL(0x0040, 0xB042);

        ///<summary>(0040,DB00) VR=CS VM=1 Template Identifier</summary>
        public readonly static DicomTagCS TemplateIdentifier = new DicomTagCS(0x0040, 0xDB00);

        ///<summary>(0040,DB06) VR=DT VM=1 Template Version (RETIRED)</summary>
        public readonly static DicomTagDT TemplateVersionRETIRED = new DicomTagDT(0x0040, 0xDB06);

        ///<summary>(0040,DB07) VR=DT VM=1 Template Local Version (RETIRED)</summary>
        public readonly static DicomTagDT TemplateLocalVersionRETIRED = new DicomTagDT(0x0040, 0xDB07);

        ///<summary>(0040,DB0B) VR=CS VM=1 Template Extension Flag (RETIRED)</summary>
        public readonly static DicomTagCS TemplateExtensionFlagRETIRED = new DicomTagCS(0x0040, 0xDB0B);

        ///<summary>(0040,DB0C) VR=UI VM=1 Template Extension Organization UID (RETIRED)</summary>
        public readonly static DicomTagUI TemplateExtensionOrganizationUIDRETIRED = new DicomTagUI(0x0040, 0xDB0C);

        ///<summary>(0040,DB0D) VR=UI VM=1 Template Extension Creator UID (RETIRED)</summary>
        public readonly static DicomTagUI TemplateExtensionCreatorUIDRETIRED = new DicomTagUI(0x0040, 0xDB0D);

        ///<summary>(0040,DB73) VR=UL VM=1-n Referenced Content Item Identifier</summary>
        public readonly static DicomTagULs ReferencedContentItemIdentifier = new DicomTagULs(0x0040, 0xDB73);

        ///<summary>(0040,E001) VR=ST VM=1 HL7 Instance Identifier</summary>
        public readonly static DicomTagST HL7InstanceIdentifier = new DicomTagST(0x0040, 0xE001);

        ///<summary>(0040,E004) VR=DT VM=1 HL7 Document Effective Time</summary>
        public readonly static DicomTagDT HL7DocumentEffectiveTime = new DicomTagDT(0x0040, 0xE004);

        ///<summary>(0040,E006) VR=SQ VM=1 HL7 Document Type Code Sequence</summary>
        public readonly static DicomTagSQ HL7DocumentTypeCodeSequence = new DicomTagSQ(0x0040, 0xE006);

        ///<summary>(0040,E008) VR=SQ VM=1 Document Class Code Sequence</summary>
        public readonly static DicomTagSQ DocumentClassCodeSequence = new DicomTagSQ(0x0040, 0xE008);

        ///<summary>(0040,E010) VR=UR VM=1 Retrieve URI</summary>
        public readonly static DicomTagUR RetrieveURI = new DicomTagUR(0x0040, 0xE010);

        ///<summary>(0040,E011) VR=UI VM=1 Retrieve Location UID</summary>
        public readonly static DicomTagUI RetrieveLocationUID = new DicomTagUI(0x0040, 0xE011);

        ///<summary>(0040,E020) VR=CS VM=1 Type of Instances</summary>
        public readonly static DicomTagCS TypeOfInstances = new DicomTagCS(0x0040, 0xE020);

        ///<summary>(0040,E021) VR=SQ VM=1 DICOM Retrieval Sequence</summary>
        public readonly static DicomTagSQ DICOMRetrievalSequence = new DicomTagSQ(0x0040, 0xE021);

        ///<summary>(0040,E022) VR=SQ VM=1 DICOM Media Retrieval Sequence</summary>
        public readonly static DicomTagSQ DICOMMediaRetrievalSequence = new DicomTagSQ(0x0040, 0xE022);

        ///<summary>(0040,E023) VR=SQ VM=1 WADO Retrieval Sequence</summary>
        public readonly static DicomTagSQ WADORetrievalSequence = new DicomTagSQ(0x0040, 0xE023);

        ///<summary>(0040,E024) VR=SQ VM=1 XDS Retrieval Sequence</summary>
        public readonly static DicomTagSQ XDSRetrievalSequence = new DicomTagSQ(0x0040, 0xE024);

        ///<summary>(0040,E025) VR=SQ VM=1 WADO-RS Retrieval Sequence</summary>
        public readonly static DicomTagSQ WADORSRetrievalSequence = new DicomTagSQ(0x0040, 0xE025);

        ///<summary>(0040,E030) VR=UI VM=1 Repository Unique ID</summary>
        public readonly static DicomTagUI RepositoryUniqueID = new DicomTagUI(0x0040, 0xE030);

        ///<summary>(0040,E031) VR=UI VM=1 Home Community ID</summary>
        public readonly static DicomTagUI HomeCommunityID = new DicomTagUI(0x0040, 0xE031);

        ///<summary>(0042,0010) VR=ST VM=1 Document Title</summary>
        public readonly static DicomTagST DocumentTitle = new DicomTagST(0x0042, 0x0010);

        ///<summary>(0042,0011) VR=OB VM=1 Encapsulated Document</summary>
        public readonly static DicomTagOB EncapsulatedDocument = new DicomTagOB(0x0042, 0x0011);

        ///<summary>(0042,0012) VR=LO VM=1 MIME Type of Encapsulated Document</summary>
        public readonly static DicomTagLO MIMETypeOfEncapsulatedDocument = new DicomTagLO(0x0042, 0x0012);

        ///<summary>(0042,0013) VR=SQ VM=1 Source Instance Sequence</summary>
        public readonly static DicomTagSQ SourceInstanceSequence = new DicomTagSQ(0x0042, 0x0013);

        ///<summary>(0042,0014) VR=LO VM=1-n List of MIME Types</summary>
        public readonly static DicomTagLOs ListOfMIMETypes = new DicomTagLOs(0x0042, 0x0014);

        ///<summary>(0042,0015) VR=UL VM=1 Encapsulated Document Length</summary>
        public readonly static DicomTagUL EncapsulatedDocumentLength = new DicomTagUL(0x0042, 0x0015);

        ///<summary>(0044,0001) VR=ST VM=1 Product Package Identifier</summary>
        public readonly static DicomTagST ProductPackageIdentifier = new DicomTagST(0x0044, 0x0001);

        ///<summary>(0044,0002) VR=CS VM=1 Substance Administration Approval</summary>
        public readonly static DicomTagCS SubstanceAdministrationApproval = new DicomTagCS(0x0044, 0x0002);

        ///<summary>(0044,0003) VR=LT VM=1 Approval Status Further Description</summary>
        public readonly static DicomTagLT ApprovalStatusFurtherDescription = new DicomTagLT(0x0044, 0x0003);

        ///<summary>(0044,0004) VR=DT VM=1 Approval Status DateTime</summary>
        public readonly static DicomTagDT ApprovalStatusDateTime = new DicomTagDT(0x0044, 0x0004);

        ///<summary>(0044,0007) VR=SQ VM=1 Product Type Code Sequence</summary>
        public readonly static DicomTagSQ ProductTypeCodeSequence = new DicomTagSQ(0x0044, 0x0007);

        ///<summary>(0044,0008) VR=LO VM=1-n Product Name</summary>
        public readonly static DicomTagLOs ProductName = new DicomTagLOs(0x0044, 0x0008);

        ///<summary>(0044,0009) VR=LT VM=1 Product Description</summary>
        public readonly static DicomTagLT ProductDescription = new DicomTagLT(0x0044, 0x0009);

        ///<summary>(0044,000A) VR=LO VM=1 Product Lot Identifier</summary>
        public readonly static DicomTagLO ProductLotIdentifier = new DicomTagLO(0x0044, 0x000A);

        ///<summary>(0044,000B) VR=DT VM=1 Product Expiration DateTime</summary>
        public readonly static DicomTagDT ProductExpirationDateTime = new DicomTagDT(0x0044, 0x000B);

        ///<summary>(0044,0010) VR=DT VM=1 Substance Administration DateTime</summary>
        public readonly static DicomTagDT SubstanceAdministrationDateTime = new DicomTagDT(0x0044, 0x0010);

        ///<summary>(0044,0011) VR=LO VM=1 Substance Administration Notes</summary>
        public readonly static DicomTagLO SubstanceAdministrationNotes = new DicomTagLO(0x0044, 0x0011);

        ///<summary>(0044,0012) VR=LO VM=1 Substance Administration Device ID</summary>
        public readonly static DicomTagLO SubstanceAdministrationDeviceID = new DicomTagLO(0x0044, 0x0012);

        ///<summary>(0044,0013) VR=SQ VM=1 Product Parameter Sequence</summary>
        public readonly static DicomTagSQ ProductParameterSequence = new DicomTagSQ(0x0044, 0x0013);

        ///<summary>(0044,0019) VR=SQ VM=1 Substance Administration Parameter Sequence</summary>
        public readonly static DicomTagSQ SubstanceAdministrationParameterSequence = new DicomTagSQ(0x0044, 0x0019);

        ///<summary>(0044,0100) VR=SQ VM=1 Approval Sequence</summary>
        public readonly static DicomTagSQ ApprovalSequence = new DicomTagSQ(0x0044, 0x0100);

        ///<summary>(0044,0101) VR=SQ VM=1 Assertion Code Sequence</summary>
        public readonly static DicomTagSQ AssertionCodeSequence = new DicomTagSQ(0x0044, 0x0101);

        ///<summary>(0044,0102) VR=UI VM=1 Assertion UID</summary>
        public readonly static DicomTagUI AssertionUID = new DicomTagUI(0x0044, 0x0102);

        ///<summary>(0044,0103) VR=SQ VM=1 Asserter Identification Sequence</summary>
        public readonly static DicomTagSQ AsserterIdentificationSequence = new DicomTagSQ(0x0044, 0x0103);

        ///<summary>(0044,0104) VR=DT VM=1 Assertion DateTime</summary>
        public readonly static DicomTagDT AssertionDateTime = new DicomTagDT(0x0044, 0x0104);

        ///<summary>(0044,0105) VR=DT VM=1 Assertion Expiration DateTime</summary>
        public readonly static DicomTagDT AssertionExpirationDateTime = new DicomTagDT(0x0044, 0x0105);

        ///<summary>(0044,0106) VR=UT VM=1 Assertion Comments</summary>
        public readonly static DicomTagUT AssertionComments = new DicomTagUT(0x0044, 0x0106);

        ///<summary>(0044,0107) VR=SQ VM=1 Related Assertion Sequence</summary>
        public readonly static DicomTagSQ RelatedAssertionSequence = new DicomTagSQ(0x0044, 0x0107);

        ///<summary>(0044,0108) VR=UI VM=1 Referenced Assertion UID</summary>
        public readonly static DicomTagUI ReferencedAssertionUID = new DicomTagUI(0x0044, 0x0108);

        ///<summary>(0044,0109) VR=SQ VM=1 Approval Subject Sequence</summary>
        public readonly static DicomTagSQ ApprovalSubjectSequence = new DicomTagSQ(0x0044, 0x0109);

        ///<summary>(0044,010A) VR=SQ VM=1 Organizational Role Code Sequence</summary>
        public readonly static DicomTagSQ OrganizationalRoleCodeSequence = new DicomTagSQ(0x0044, 0x010A);

        ///<summary>(0044,0110) VR=SQ VM=1 RT Assertions Sequence</summary>
        public readonly static DicomTagSQ RTAssertionsSequence = new DicomTagSQ(0x0044, 0x0110);

        ///<summary>(0046,0012) VR=LO VM=1 Lens Description</summary>
        public readonly static DicomTagLO LensDescription = new DicomTagLO(0x0046, 0x0012);

        ///<summary>(0046,0014) VR=SQ VM=1 Right Lens Sequence</summary>
        public readonly static DicomTagSQ RightLensSequence = new DicomTagSQ(0x0046, 0x0014);

        ///<summary>(0046,0015) VR=SQ VM=1 Left Lens Sequence</summary>
        public readonly static DicomTagSQ LeftLensSequence = new DicomTagSQ(0x0046, 0x0015);

        ///<summary>(0046,0016) VR=SQ VM=1 Unspecified Laterality Lens Sequence</summary>
        public readonly static DicomTagSQ UnspecifiedLateralityLensSequence = new DicomTagSQ(0x0046, 0x0016);

        ///<summary>(0046,0018) VR=SQ VM=1 Cylinder Sequence</summary>
        public readonly static DicomTagSQ CylinderSequence = new DicomTagSQ(0x0046, 0x0018);

        ///<summary>(0046,0028) VR=SQ VM=1 Prism Sequence</summary>
        public readonly static DicomTagSQ PrismSequence = new DicomTagSQ(0x0046, 0x0028);

        ///<summary>(0046,0030) VR=FD VM=1 Horizontal Prism Power</summary>
        public readonly static DicomTagFD HorizontalPrismPower = new DicomTagFD(0x0046, 0x0030);

        ///<summary>(0046,0032) VR=CS VM=1 Horizontal Prism Base</summary>
        public readonly static DicomTagCS HorizontalPrismBase = new DicomTagCS(0x0046, 0x0032);

        ///<summary>(0046,0034) VR=FD VM=1 Vertical Prism Power</summary>
        public readonly static DicomTagFD VerticalPrismPower = new DicomTagFD(0x0046, 0x0034);

        ///<summary>(0046,0036) VR=CS VM=1 Vertical Prism Base</summary>
        public readonly static DicomTagCS VerticalPrismBase = new DicomTagCS(0x0046, 0x0036);

        ///<summary>(0046,0038) VR=CS VM=1 Lens Segment Type</summary>
        public readonly static DicomTagCS LensSegmentType = new DicomTagCS(0x0046, 0x0038);

        ///<summary>(0046,0040) VR=FD VM=1 Optical Transmittance</summary>
        public readonly static DicomTagFD OpticalTransmittance = new DicomTagFD(0x0046, 0x0040);

        ///<summary>(0046,0042) VR=FD VM=1 Channel Width</summary>
        public readonly static DicomTagFD ChannelWidth = new DicomTagFD(0x0046, 0x0042);

        ///<summary>(0046,0044) VR=FD VM=1 Pupil Size</summary>
        public readonly static DicomTagFD PupilSize = new DicomTagFD(0x0046, 0x0044);

        ///<summary>(0046,0046) VR=FD VM=1 Corneal Size</summary>
        public readonly static DicomTagFD CornealSize = new DicomTagFD(0x0046, 0x0046);

        ///<summary>(0046,0047) VR=SQ VM=1 Corneal Size Sequence</summary>
        public readonly static DicomTagSQ CornealSizeSequence = new DicomTagSQ(0x0046, 0x0047);

        ///<summary>(0046,0050) VR=SQ VM=1 Autorefraction Right Eye Sequence</summary>
        public readonly static DicomTagSQ AutorefractionRightEyeSequence = new DicomTagSQ(0x0046, 0x0050);

        ///<summary>(0046,0052) VR=SQ VM=1 Autorefraction Left Eye Sequence</summary>
        public readonly static DicomTagSQ AutorefractionLeftEyeSequence = new DicomTagSQ(0x0046, 0x0052);

        ///<summary>(0046,0060) VR=FD VM=1 Distance Pupillary Distance</summary>
        public readonly static DicomTagFD DistancePupillaryDistance = new DicomTagFD(0x0046, 0x0060);

        ///<summary>(0046,0062) VR=FD VM=1 Near Pupillary Distance</summary>
        public readonly static DicomTagFD NearPupillaryDistance = new DicomTagFD(0x0046, 0x0062);

        ///<summary>(0046,0063) VR=FD VM=1 Intermediate Pupillary Distance</summary>
        public readonly static DicomTagFD IntermediatePupillaryDistance = new DicomTagFD(0x0046, 0x0063);

        ///<summary>(0046,0064) VR=FD VM=1 Other Pupillary Distance</summary>
        public readonly static DicomTagFD OtherPupillaryDistance = new DicomTagFD(0x0046, 0x0064);

        ///<summary>(0046,0070) VR=SQ VM=1 Keratometry Right Eye Sequence</summary>
        public readonly static DicomTagSQ KeratometryRightEyeSequence = new DicomTagSQ(0x0046, 0x0070);

        ///<summary>(0046,0071) VR=SQ VM=1 Keratometry Left Eye Sequence</summary>
        public readonly static DicomTagSQ KeratometryLeftEyeSequence = new DicomTagSQ(0x0046, 0x0071);

        ///<summary>(0046,0074) VR=SQ VM=1 Steep Keratometric Axis Sequence</summary>
        public readonly static DicomTagSQ SteepKeratometricAxisSequence = new DicomTagSQ(0x0046, 0x0074);

        ///<summary>(0046,0075) VR=FD VM=1 Radius of Curvature</summary>
        public readonly static DicomTagFD RadiusOfCurvature = new DicomTagFD(0x0046, 0x0075);

        ///<summary>(0046,0076) VR=FD VM=1 Keratometric Power</summary>
        public readonly static DicomTagFD KeratometricPower = new DicomTagFD(0x0046, 0x0076);

        ///<summary>(0046,0077) VR=FD VM=1 Keratometric Axis</summary>
        public readonly static DicomTagFD KeratometricAxis = new DicomTagFD(0x0046, 0x0077);

        ///<summary>(0046,0080) VR=SQ VM=1 Flat Keratometric Axis Sequence</summary>
        public readonly static DicomTagSQ FlatKeratometricAxisSequence = new DicomTagSQ(0x0046, 0x0080);

        ///<summary>(0046,0092) VR=CS VM=1 Background Color</summary>
        public readonly static DicomTagCS BackgroundColor = new DicomTagCS(0x0046, 0x0092);

        ///<summary>(0046,0094) VR=CS VM=1 Optotype</summary>
        public readonly static DicomTagCS Optotype = new DicomTagCS(0x0046, 0x0094);

        ///<summary>(0046,0095) VR=CS VM=1 Optotype Presentation</summary>
        public readonly static DicomTagCS OptotypePresentation = new DicomTagCS(0x0046, 0x0095);

        ///<summary>(0046,0097) VR=SQ VM=1 Subjective Refraction Right Eye Sequence</summary>
        public readonly static DicomTagSQ SubjectiveRefractionRightEyeSequence = new DicomTagSQ(0x0046, 0x0097);

        ///<summary>(0046,0098) VR=SQ VM=1 Subjective Refraction Left Eye Sequence</summary>
        public readonly static DicomTagSQ SubjectiveRefractionLeftEyeSequence = new DicomTagSQ(0x0046, 0x0098);

        ///<summary>(0046,0100) VR=SQ VM=1 Add Near Sequence</summary>
        public readonly static DicomTagSQ AddNearSequence = new DicomTagSQ(0x0046, 0x0100);

        ///<summary>(0046,0101) VR=SQ VM=1 Add Intermediate Sequence</summary>
        public readonly static DicomTagSQ AddIntermediateSequence = new DicomTagSQ(0x0046, 0x0101);

        ///<summary>(0046,0102) VR=SQ VM=1 Add Other Sequence</summary>
        public readonly static DicomTagSQ AddOtherSequence = new DicomTagSQ(0x0046, 0x0102);

        ///<summary>(0046,0104) VR=FD VM=1 Add Power</summary>
        public readonly static DicomTagFD AddPower = new DicomTagFD(0x0046, 0x0104);

        ///<summary>(0046,0106) VR=FD VM=1 Viewing Distance</summary>
        public readonly static DicomTagFD ViewingDistance = new DicomTagFD(0x0046, 0x0106);

        ///<summary>(0046,0110) VR=SQ VM=1 Cornea Measurements Sequence</summary>
        public readonly static DicomTagSQ CorneaMeasurementsSequence = new DicomTagSQ(0x0046, 0x0110);

        ///<summary>(0046,0111) VR=SQ VM=1 Source of Cornea Measurement Data Code Sequence</summary>
        public readonly static DicomTagSQ SourceOfCorneaMeasurementDataCodeSequence = new DicomTagSQ(0x0046, 0x0111);

        ///<summary>(0046,0112) VR=SQ VM=1 Steep Corneal Axis Sequence</summary>
        public readonly static DicomTagSQ SteepCornealAxisSequence = new DicomTagSQ(0x0046, 0x0112);

        ///<summary>(0046,0113) VR=SQ VM=1 Flat Corneal Axis Sequence</summary>
        public readonly static DicomTagSQ FlatCornealAxisSequence = new DicomTagSQ(0x0046, 0x0113);

        ///<summary>(0046,0114) VR=FD VM=1 Corneal Power</summary>
        public readonly static DicomTagFD CornealPower = new DicomTagFD(0x0046, 0x0114);

        ///<summary>(0046,0115) VR=FD VM=1 Corneal Axis</summary>
        public readonly static DicomTagFD CornealAxis = new DicomTagFD(0x0046, 0x0115);

        ///<summary>(0046,0116) VR=SQ VM=1 Cornea Measurement Method Code Sequence</summary>
        public readonly static DicomTagSQ CorneaMeasurementMethodCodeSequence = new DicomTagSQ(0x0046, 0x0116);

        ///<summary>(0046,0117) VR=FL VM=1 Refractive Index of Cornea</summary>
        public readonly static DicomTagFL RefractiveIndexOfCornea = new DicomTagFL(0x0046, 0x0117);

        ///<summary>(0046,0118) VR=FL VM=1 Refractive Index of Aqueous Humor</summary>
        public readonly static DicomTagFL RefractiveIndexOfAqueousHumor = new DicomTagFL(0x0046, 0x0118);

        ///<summary>(0046,0121) VR=SQ VM=1 Visual Acuity Type Code Sequence</summary>
        public readonly static DicomTagSQ VisualAcuityTypeCodeSequence = new DicomTagSQ(0x0046, 0x0121);

        ///<summary>(0046,0122) VR=SQ VM=1 Visual Acuity Right Eye Sequence</summary>
        public readonly static DicomTagSQ VisualAcuityRightEyeSequence = new DicomTagSQ(0x0046, 0x0122);

        ///<summary>(0046,0123) VR=SQ VM=1 Visual Acuity Left Eye Sequence</summary>
        public readonly static DicomTagSQ VisualAcuityLeftEyeSequence = new DicomTagSQ(0x0046, 0x0123);

        ///<summary>(0046,0124) VR=SQ VM=1 Visual Acuity Both Eyes Open Sequence</summary>
        public readonly static DicomTagSQ VisualAcuityBothEyesOpenSequence = new DicomTagSQ(0x0046, 0x0124);

        ///<summary>(0046,0125) VR=CS VM=1 Viewing Distance Type</summary>
        public readonly static DicomTagCS ViewingDistanceType = new DicomTagCS(0x0046, 0x0125);

        ///<summary>(0046,0135) VR=SS VM=2 Visual Acuity Modifiers</summary>
        public readonly static DicomTagSSs VisualAcuityModifiers = new DicomTagSSs(0x0046, 0x0135);

        ///<summary>(0046,0137) VR=FD VM=1 Decimal Visual Acuity</summary>
        public readonly static DicomTagFD DecimalVisualAcuity = new DicomTagFD(0x0046, 0x0137);

        ///<summary>(0046,0139) VR=LO VM=1 Optotype Detailed Definition</summary>
        public readonly static DicomTagLO OptotypeDetailedDefinition = new DicomTagLO(0x0046, 0x0139);

        ///<summary>(0046,0145) VR=SQ VM=1 Referenced Refractive Measurements Sequence</summary>
        public readonly static DicomTagSQ ReferencedRefractiveMeasurementsSequence = new DicomTagSQ(0x0046, 0x0145);

        ///<summary>(0046,0146) VR=FD VM=1 Sphere Power</summary>
        public readonly static DicomTagFD SpherePower = new DicomTagFD(0x0046, 0x0146);

        ///<summary>(0046,0147) VR=FD VM=1 Cylinder Power</summary>
        public readonly static DicomTagFD CylinderPower = new DicomTagFD(0x0046, 0x0147);

        ///<summary>(0046,0201) VR=CS VM=1 Corneal Topography Surface</summary>
        public readonly static DicomTagCS CornealTopographySurface = new DicomTagCS(0x0046, 0x0201);

        ///<summary>(0046,0202) VR=FL VM=2 Corneal Vertex Location</summary>
        public readonly static DicomTagFLs CornealVertexLocation = new DicomTagFLs(0x0046, 0x0202);

        ///<summary>(0046,0203) VR=FL VM=1 Pupil Centroid X-Coordinate</summary>
        public readonly static DicomTagFL PupilCentroidXCoordinate = new DicomTagFL(0x0046, 0x0203);

        ///<summary>(0046,0204) VR=FL VM=1 Pupil Centroid Y-Coordinate</summary>
        public readonly static DicomTagFL PupilCentroidYCoordinate = new DicomTagFL(0x0046, 0x0204);

        ///<summary>(0046,0205) VR=FL VM=1 Equivalent Pupil Radius</summary>
        public readonly static DicomTagFL EquivalentPupilRadius = new DicomTagFL(0x0046, 0x0205);

        ///<summary>(0046,0207) VR=SQ VM=1 Corneal Topography Map Type Code Sequence</summary>
        public readonly static DicomTagSQ CornealTopographyMapTypeCodeSequence = new DicomTagSQ(0x0046, 0x0207);

        ///<summary>(0046,0208) VR=IS VM=2-2n Vertices of the Outline of Pupil</summary>
        public readonly static DicomTagISs VerticesOfTheOutlineOfPupil = new DicomTagISs(0x0046, 0x0208);

        ///<summary>(0046,0210) VR=SQ VM=1 Corneal Topography Mapping Normals Sequence</summary>
        public readonly static DicomTagSQ CornealTopographyMappingNormalsSequence = new DicomTagSQ(0x0046, 0x0210);

        ///<summary>(0046,0211) VR=SQ VM=1 Maximum Corneal Curvature Sequence</summary>
        public readonly static DicomTagSQ MaximumCornealCurvatureSequence = new DicomTagSQ(0x0046, 0x0211);

        ///<summary>(0046,0212) VR=FL VM=1 Maximum Corneal Curvature</summary>
        public readonly static DicomTagFL MaximumCornealCurvature = new DicomTagFL(0x0046, 0x0212);

        ///<summary>(0046,0213) VR=FL VM=2 Maximum Corneal Curvature Location</summary>
        public readonly static DicomTagFLs MaximumCornealCurvatureLocation = new DicomTagFLs(0x0046, 0x0213);

        ///<summary>(0046,0215) VR=SQ VM=1 Minimum Keratometric Sequence</summary>
        public readonly static DicomTagSQ MinimumKeratometricSequence = new DicomTagSQ(0x0046, 0x0215);

        ///<summary>(0046,0218) VR=SQ VM=1 Simulated Keratometric Cylinder Sequence</summary>
        public readonly static DicomTagSQ SimulatedKeratometricCylinderSequence = new DicomTagSQ(0x0046, 0x0218);

        ///<summary>(0046,0220) VR=FL VM=1 Average Corneal Power</summary>
        public readonly static DicomTagFL AverageCornealPower = new DicomTagFL(0x0046, 0x0220);

        ///<summary>(0046,0224) VR=FL VM=1 Corneal I-S Value</summary>
        public readonly static DicomTagFL CornealISValue = new DicomTagFL(0x0046, 0x0224);

        ///<summary>(0046,0227) VR=FL VM=1 Analyzed Area</summary>
        public readonly static DicomTagFL AnalyzedArea = new DicomTagFL(0x0046, 0x0227);

        ///<summary>(0046,0230) VR=FL VM=1 Surface Regularity Index</summary>
        public readonly static DicomTagFL SurfaceRegularityIndex = new DicomTagFL(0x0046, 0x0230);

        ///<summary>(0046,0232) VR=FL VM=1 Surface Asymmetry Index</summary>
        public readonly static DicomTagFL SurfaceAsymmetryIndex = new DicomTagFL(0x0046, 0x0232);

        ///<summary>(0046,0234) VR=FL VM=1 Corneal Eccentricity Index</summary>
        public readonly static DicomTagFL CornealEccentricityIndex = new DicomTagFL(0x0046, 0x0234);

        ///<summary>(0046,0236) VR=FL VM=1 Keratoconus Prediction Index</summary>
        public readonly static DicomTagFL KeratoconusPredictionIndex = new DicomTagFL(0x0046, 0x0236);

        ///<summary>(0046,0238) VR=FL VM=1 Decimal Potential Visual Acuity</summary>
        public readonly static DicomTagFL DecimalPotentialVisualAcuity = new DicomTagFL(0x0046, 0x0238);

        ///<summary>(0046,0242) VR=CS VM=1 Corneal Topography Map Quality Evaluation</summary>
        public readonly static DicomTagCS CornealTopographyMapQualityEvaluation = new DicomTagCS(0x0046, 0x0242);

        ///<summary>(0046,0244) VR=SQ VM=1 Source Image Corneal Processed Data Sequence</summary>
        public readonly static DicomTagSQ SourceImageCornealProcessedDataSequence = new DicomTagSQ(0x0046, 0x0244);

        ///<summary>(0046,0247) VR=FL VM=3 Corneal Point Location</summary>
        public readonly static DicomTagFLs CornealPointLocation = new DicomTagFLs(0x0046, 0x0247);

        ///<summary>(0046,0248) VR=CS VM=1 Corneal Point Estimated</summary>
        public readonly static DicomTagCS CornealPointEstimated = new DicomTagCS(0x0046, 0x0248);

        ///<summary>(0046,0249) VR=FL VM=1 Axial Power</summary>
        public readonly static DicomTagFL AxialPower = new DicomTagFL(0x0046, 0x0249);

        ///<summary>(0046,0250) VR=FL VM=1 Tangential Power</summary>
        public readonly static DicomTagFL TangentialPower = new DicomTagFL(0x0046, 0x0250);

        ///<summary>(0046,0251) VR=FL VM=1 Refractive Power</summary>
        public readonly static DicomTagFL RefractivePower = new DicomTagFL(0x0046, 0x0251);

        ///<summary>(0046,0252) VR=FL VM=1 Relative Elevation</summary>
        public readonly static DicomTagFL RelativeElevation = new DicomTagFL(0x0046, 0x0252);

        ///<summary>(0046,0253) VR=FL VM=1 Corneal Wavefront</summary>
        public readonly static DicomTagFL CornealWavefront = new DicomTagFL(0x0046, 0x0253);

        ///<summary>(0048,0001) VR=FL VM=1 Imaged Volume Width</summary>
        public readonly static DicomTagFL ImagedVolumeWidth = new DicomTagFL(0x0048, 0x0001);

        ///<summary>(0048,0002) VR=FL VM=1 Imaged Volume Height</summary>
        public readonly static DicomTagFL ImagedVolumeHeight = new DicomTagFL(0x0048, 0x0002);

        ///<summary>(0048,0003) VR=FL VM=1 Imaged Volume Depth</summary>
        public readonly static DicomTagFL ImagedVolumeDepth = new DicomTagFL(0x0048, 0x0003);

        ///<summary>(0048,0006) VR=UL VM=1 Total Pixel Matrix Columns</summary>
        public readonly static DicomTagUL TotalPixelMatrixColumns = new DicomTagUL(0x0048, 0x0006);

        ///<summary>(0048,0007) VR=UL VM=1 Total Pixel Matrix Rows</summary>
        public readonly static DicomTagUL TotalPixelMatrixRows = new DicomTagUL(0x0048, 0x0007);

        ///<summary>(0048,0008) VR=SQ VM=1 Total Pixel Matrix Origin Sequence</summary>
        public readonly static DicomTagSQ TotalPixelMatrixOriginSequence = new DicomTagSQ(0x0048, 0x0008);

        ///<summary>(0048,0010) VR=CS VM=1 Specimen Label in Image</summary>
        public readonly static DicomTagCS SpecimenLabelInImage = new DicomTagCS(0x0048, 0x0010);

        ///<summary>(0048,0011) VR=CS VM=1 Focus Method</summary>
        public readonly static DicomTagCS FocusMethod = new DicomTagCS(0x0048, 0x0011);

        ///<summary>(0048,0012) VR=CS VM=1 Extended Depth of Field</summary>
        public readonly static DicomTagCS ExtendedDepthOfField = new DicomTagCS(0x0048, 0x0012);

        ///<summary>(0048,0013) VR=US VM=1 Number of Focal Planes</summary>
        public readonly static DicomTagUS NumberOfFocalPlanes = new DicomTagUS(0x0048, 0x0013);

        ///<summary>(0048,0014) VR=FL VM=1 Distance Between Focal Planes</summary>
        public readonly static DicomTagFL DistanceBetweenFocalPlanes = new DicomTagFL(0x0048, 0x0014);

        ///<summary>(0048,0015) VR=US VM=3 Recommended Absent Pixel CIELab Value</summary>
        public readonly static DicomTagUSs RecommendedAbsentPixelCIELabValue = new DicomTagUSs(0x0048, 0x0015);

        ///<summary>(0048,0100) VR=SQ VM=1 Illuminator Type Code Sequence</summary>
        public readonly static DicomTagSQ IlluminatorTypeCodeSequence = new DicomTagSQ(0x0048, 0x0100);

        ///<summary>(0048,0102) VR=DS VM=6 Image Orientation (Slide)</summary>
        public readonly static DicomTagDSs ImageOrientationSlide = new DicomTagDSs(0x0048, 0x0102);

        ///<summary>(0048,0105) VR=SQ VM=1 Optical Path Sequence</summary>
        public readonly static DicomTagSQ OpticalPathSequence = new DicomTagSQ(0x0048, 0x0105);

        ///<summary>(0048,0106) VR=SH VM=1 Optical Path Identifier</summary>
        public readonly static DicomTagSH OpticalPathIdentifier = new DicomTagSH(0x0048, 0x0106);

        ///<summary>(0048,0107) VR=ST VM=1 Optical Path Description</summary>
        public readonly static DicomTagST OpticalPathDescription = new DicomTagST(0x0048, 0x0107);

        ///<summary>(0048,0108) VR=SQ VM=1 Illumination Color Code Sequence</summary>
        public readonly static DicomTagSQ IlluminationColorCodeSequence = new DicomTagSQ(0x0048, 0x0108);

        ///<summary>(0048,0110) VR=SQ VM=1 Specimen Reference Sequence</summary>
        public readonly static DicomTagSQ SpecimenReferenceSequence = new DicomTagSQ(0x0048, 0x0110);

        ///<summary>(0048,0111) VR=DS VM=1 Condenser Lens Power</summary>
        public readonly static DicomTagDS CondenserLensPower = new DicomTagDS(0x0048, 0x0111);

        ///<summary>(0048,0112) VR=DS VM=1 Objective Lens Power</summary>
        public readonly static DicomTagDS ObjectiveLensPower = new DicomTagDS(0x0048, 0x0112);

        ///<summary>(0048,0113) VR=DS VM=1 Objective Lens Numerical Aperture</summary>
        public readonly static DicomTagDS ObjectiveLensNumericalAperture = new DicomTagDS(0x0048, 0x0113);

        ///<summary>(0048,0114) VR=CS VM=1 Confocal Mode</summary>
        public readonly static DicomTagCS ConfocalMode = new DicomTagCS(0x0048, 0x0114);

        ///<summary>(0048,0115) VR=CS VM=1 Tissue Location</summary>
        public readonly static DicomTagCS TissueLocation = new DicomTagCS(0x0048, 0x0115);

        ///<summary>(0048,0116) VR=SQ VM=1 Confocal Microscopy Image Frame Type Sequence</summary>
        public readonly static DicomTagSQ ConfocalMicroscopyImageFrameTypeSequence = new DicomTagSQ(0x0048, 0x0116);

        ///<summary>(0048,0117) VR=FD VM=1 Image Acquisition Depth</summary>
        public readonly static DicomTagFD ImageAcquisitionDepth = new DicomTagFD(0x0048, 0x0117);

        ///<summary>(0048,0120) VR=SQ VM=1 Palette Color Lookup Table Sequence</summary>
        public readonly static DicomTagSQ PaletteColorLookupTableSequence = new DicomTagSQ(0x0048, 0x0120);

        ///<summary>(0048,0200) VR=SQ VM=1 Referenced Image Navigation Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedImageNavigationSequenceRETIRED = new DicomTagSQ(0x0048, 0x0200);

        ///<summary>(0048,0201) VR=US VM=2 Top Left Hand Corner of Localizer Area (RETIRED)</summary>
        public readonly static DicomTagUSs TopLeftHandCornerOfLocalizerAreaRETIRED = new DicomTagUSs(0x0048, 0x0201);

        ///<summary>(0048,0202) VR=US VM=2 Bottom Right Hand Corner of Localizer Area (RETIRED)</summary>
        public readonly static DicomTagUSs BottomRightHandCornerOfLocalizerAreaRETIRED = new DicomTagUSs(0x0048, 0x0202);

        ///<summary>(0048,0207) VR=SQ VM=1 Optical Path Identification Sequence</summary>
        public readonly static DicomTagSQ OpticalPathIdentificationSequence = new DicomTagSQ(0x0048, 0x0207);

        ///<summary>(0048,021A) VR=SQ VM=1 Plane Position (Slide) Sequence</summary>
        public readonly static DicomTagSQ PlanePositionSlideSequence = new DicomTagSQ(0x0048, 0x021A);

        ///<summary>(0048,021E) VR=SL VM=1 Column Position In Total Image Pixel Matrix</summary>
        public readonly static DicomTagSL ColumnPositionInTotalImagePixelMatrix = new DicomTagSL(0x0048, 0x021E);

        ///<summary>(0048,021F) VR=SL VM=1 Row Position In Total Image Pixel Matrix</summary>
        public readonly static DicomTagSL RowPositionInTotalImagePixelMatrix = new DicomTagSL(0x0048, 0x021F);

        ///<summary>(0048,0301) VR=CS VM=1 Pixel Origin Interpretation</summary>
        public readonly static DicomTagCS PixelOriginInterpretation = new DicomTagCS(0x0048, 0x0301);

        ///<summary>(0048,0302) VR=UL VM=1 Number of Optical Paths</summary>
        public readonly static DicomTagUL NumberOfOpticalPaths = new DicomTagUL(0x0048, 0x0302);

        ///<summary>(0048,0303) VR=UL VM=1 Total Pixel Matrix Focal Planes</summary>
        public readonly static DicomTagUL TotalPixelMatrixFocalPlanes = new DicomTagUL(0x0048, 0x0303);

        ///<summary>(0048,0304) VR=CS VM=1 Tiles Overlap</summary>
        public readonly static DicomTagCS TilesOverlap = new DicomTagCS(0x0048, 0x0304);

        ///<summary>(0050,0004) VR=CS VM=1 Calibration Image</summary>
        public readonly static DicomTagCS CalibrationImage = new DicomTagCS(0x0050, 0x0004);

        ///<summary>(0050,0010) VR=SQ VM=1 Device Sequence</summary>
        public readonly static DicomTagSQ DeviceSequence = new DicomTagSQ(0x0050, 0x0010);

        ///<summary>(0050,0012) VR=SQ VM=1 Container Component Type Code Sequence</summary>
        public readonly static DicomTagSQ ContainerComponentTypeCodeSequence = new DicomTagSQ(0x0050, 0x0012);

        ///<summary>(0050,0013) VR=FD VM=1 Container Component Thickness</summary>
        public readonly static DicomTagFD ContainerComponentThickness = new DicomTagFD(0x0050, 0x0013);

        ///<summary>(0050,0014) VR=DS VM=1 Device Length</summary>
        public readonly static DicomTagDS DeviceLength = new DicomTagDS(0x0050, 0x0014);

        ///<summary>(0050,0015) VR=FD VM=1 Container Component Width</summary>
        public readonly static DicomTagFD ContainerComponentWidth = new DicomTagFD(0x0050, 0x0015);

        ///<summary>(0050,0016) VR=DS VM=1 Device Diameter</summary>
        public readonly static DicomTagDS DeviceDiameter = new DicomTagDS(0x0050, 0x0016);

        ///<summary>(0050,0017) VR=CS VM=1 Device Diameter Units</summary>
        public readonly static DicomTagCS DeviceDiameterUnits = new DicomTagCS(0x0050, 0x0017);

        ///<summary>(0050,0018) VR=DS VM=1 Device Volume</summary>
        public readonly static DicomTagDS DeviceVolume = new DicomTagDS(0x0050, 0x0018);

        ///<summary>(0050,0019) VR=DS VM=1 Inter-Marker Distance</summary>
        public readonly static DicomTagDS InterMarkerDistance = new DicomTagDS(0x0050, 0x0019);

        ///<summary>(0050,001A) VR=CS VM=1 Container Component Material</summary>
        public readonly static DicomTagCS ContainerComponentMaterial = new DicomTagCS(0x0050, 0x001A);

        ///<summary>(0050,001B) VR=LO VM=1 Container Component ID</summary>
        public readonly static DicomTagLO ContainerComponentID = new DicomTagLO(0x0050, 0x001B);

        ///<summary>(0050,001C) VR=FD VM=1 Container Component Length</summary>
        public readonly static DicomTagFD ContainerComponentLength = new DicomTagFD(0x0050, 0x001C);

        ///<summary>(0050,001D) VR=FD VM=1 Container Component Diameter</summary>
        public readonly static DicomTagFD ContainerComponentDiameter = new DicomTagFD(0x0050, 0x001D);

        ///<summary>(0050,001E) VR=LO VM=1 Container Component Description</summary>
        public readonly static DicomTagLO ContainerComponentDescription = new DicomTagLO(0x0050, 0x001E);

        ///<summary>(0050,0020) VR=LO VM=1 Device Description</summary>
        public readonly static DicomTagLO DeviceDescription = new DicomTagLO(0x0050, 0x0020);

        ///<summary>(0050,0021) VR=ST VM=1 Long Device Description</summary>
        public readonly static DicomTagST LongDeviceDescription = new DicomTagST(0x0050, 0x0021);

        ///<summary>(0052,0001) VR=FL VM=1 Contrast/Bolus Ingredient Percent by Volume</summary>
        public readonly static DicomTagFL ContrastBolusIngredientPercentByVolume = new DicomTagFL(0x0052, 0x0001);

        ///<summary>(0052,0002) VR=FD VM=1 OCT Focal Distance</summary>
        public readonly static DicomTagFD OCTFocalDistance = new DicomTagFD(0x0052, 0x0002);

        ///<summary>(0052,0003) VR=FD VM=1 Beam Spot Size</summary>
        public readonly static DicomTagFD BeamSpotSize = new DicomTagFD(0x0052, 0x0003);

        ///<summary>(0052,0004) VR=FD VM=1 Effective Refractive Index</summary>
        public readonly static DicomTagFD EffectiveRefractiveIndex = new DicomTagFD(0x0052, 0x0004);

        ///<summary>(0052,0006) VR=CS VM=1 OCT Acquisition Domain</summary>
        public readonly static DicomTagCS OCTAcquisitionDomain = new DicomTagCS(0x0052, 0x0006);

        ///<summary>(0052,0007) VR=FD VM=1 OCT Optical Center Wavelength</summary>
        public readonly static DicomTagFD OCTOpticalCenterWavelength = new DicomTagFD(0x0052, 0x0007);

        ///<summary>(0052,0008) VR=FD VM=1 Axial Resolution</summary>
        public readonly static DicomTagFD AxialResolution = new DicomTagFD(0x0052, 0x0008);

        ///<summary>(0052,0009) VR=FD VM=1 Ranging Depth</summary>
        public readonly static DicomTagFD RangingDepth = new DicomTagFD(0x0052, 0x0009);

        ///<summary>(0052,0011) VR=FD VM=1 A-line Rate</summary>
        public readonly static DicomTagFD ALineRate = new DicomTagFD(0x0052, 0x0011);

        ///<summary>(0052,0012) VR=US VM=1 A-lines Per Frame</summary>
        public readonly static DicomTagUS ALinesPerFrame = new DicomTagUS(0x0052, 0x0012);

        ///<summary>(0052,0013) VR=FD VM=1 Catheter Rotational Rate</summary>
        public readonly static DicomTagFD CatheterRotationalRate = new DicomTagFD(0x0052, 0x0013);

        ///<summary>(0052,0014) VR=FD VM=1 A-line Pixel Spacing</summary>
        public readonly static DicomTagFD ALinePixelSpacing = new DicomTagFD(0x0052, 0x0014);

        ///<summary>(0052,0016) VR=SQ VM=1 Mode of Percutaneous Access Sequence</summary>
        public readonly static DicomTagSQ ModeOfPercutaneousAccessSequence = new DicomTagSQ(0x0052, 0x0016);

        ///<summary>(0052,0025) VR=SQ VM=1 Intravascular OCT Frame Type Sequence</summary>
        public readonly static DicomTagSQ IntravascularOCTFrameTypeSequence = new DicomTagSQ(0x0052, 0x0025);

        ///<summary>(0052,0026) VR=CS VM=1 OCT Z Offset Applied</summary>
        public readonly static DicomTagCS OCTZOffsetApplied = new DicomTagCS(0x0052, 0x0026);

        ///<summary>(0052,0027) VR=SQ VM=1 Intravascular Frame Content Sequence</summary>
        public readonly static DicomTagSQ IntravascularFrameContentSequence = new DicomTagSQ(0x0052, 0x0027);

        ///<summary>(0052,0028) VR=FD VM=1 Intravascular Longitudinal Distance</summary>
        public readonly static DicomTagFD IntravascularLongitudinalDistance = new DicomTagFD(0x0052, 0x0028);

        ///<summary>(0052,0029) VR=SQ VM=1 Intravascular OCT Frame Content Sequence</summary>
        public readonly static DicomTagSQ IntravascularOCTFrameContentSequence = new DicomTagSQ(0x0052, 0x0029);

        ///<summary>(0052,0030) VR=SS VM=1 OCT Z Offset Correction</summary>
        public readonly static DicomTagSS OCTZOffsetCorrection = new DicomTagSS(0x0052, 0x0030);

        ///<summary>(0052,0031) VR=CS VM=1 Catheter Direction of Rotation</summary>
        public readonly static DicomTagCS CatheterDirectionOfRotation = new DicomTagCS(0x0052, 0x0031);

        ///<summary>(0052,0033) VR=FD VM=1 Seam Line Location</summary>
        public readonly static DicomTagFD SeamLineLocation = new DicomTagFD(0x0052, 0x0033);

        ///<summary>(0052,0034) VR=FD VM=1 First A-line Location</summary>
        public readonly static DicomTagFD FirstALineLocation = new DicomTagFD(0x0052, 0x0034);

        ///<summary>(0052,0036) VR=US VM=1 Seam Line Index</summary>
        public readonly static DicomTagUS SeamLineIndex = new DicomTagUS(0x0052, 0x0036);

        ///<summary>(0052,0038) VR=US VM=1 Number of Padded A-lines</summary>
        public readonly static DicomTagUS NumberOfPaddedALines = new DicomTagUS(0x0052, 0x0038);

        ///<summary>(0052,0039) VR=CS VM=1 Interpolation Type</summary>
        public readonly static DicomTagCS InterpolationType = new DicomTagCS(0x0052, 0x0039);

        ///<summary>(0052,003A) VR=CS VM=1 Refractive Index Applied</summary>
        public readonly static DicomTagCS RefractiveIndexApplied = new DicomTagCS(0x0052, 0x003A);

        ///<summary>(0054,0010) VR=US VM=1-n Energy Window Vector</summary>
        public readonly static DicomTagUSs EnergyWindowVector = new DicomTagUSs(0x0054, 0x0010);

        ///<summary>(0054,0011) VR=US VM=1 Number of Energy Windows</summary>
        public readonly static DicomTagUS NumberOfEnergyWindows = new DicomTagUS(0x0054, 0x0011);

        ///<summary>(0054,0012) VR=SQ VM=1 Energy Window Information Sequence</summary>
        public readonly static DicomTagSQ EnergyWindowInformationSequence = new DicomTagSQ(0x0054, 0x0012);

        ///<summary>(0054,0013) VR=SQ VM=1 Energy Window Range Sequence</summary>
        public readonly static DicomTagSQ EnergyWindowRangeSequence = new DicomTagSQ(0x0054, 0x0013);

        ///<summary>(0054,0014) VR=DS VM=1 Energy Window Lower Limit</summary>
        public readonly static DicomTagDS EnergyWindowLowerLimit = new DicomTagDS(0x0054, 0x0014);

        ///<summary>(0054,0015) VR=DS VM=1 Energy Window Upper Limit</summary>
        public readonly static DicomTagDS EnergyWindowUpperLimit = new DicomTagDS(0x0054, 0x0015);

        ///<summary>(0054,0016) VR=SQ VM=1 Radiopharmaceutical Information Sequence</summary>
        public readonly static DicomTagSQ RadiopharmaceuticalInformationSequence = new DicomTagSQ(0x0054, 0x0016);

        ///<summary>(0054,0017) VR=IS VM=1 Residual Syringe Counts</summary>
        public readonly static DicomTagIS ResidualSyringeCounts = new DicomTagIS(0x0054, 0x0017);

        ///<summary>(0054,0018) VR=SH VM=1 Energy Window Name</summary>
        public readonly static DicomTagSH EnergyWindowName = new DicomTagSH(0x0054, 0x0018);

        ///<summary>(0054,0020) VR=US VM=1-n Detector Vector</summary>
        public readonly static DicomTagUSs DetectorVector = new DicomTagUSs(0x0054, 0x0020);

        ///<summary>(0054,0021) VR=US VM=1 Number of Detectors</summary>
        public readonly static DicomTagUS NumberOfDetectors = new DicomTagUS(0x0054, 0x0021);

        ///<summary>(0054,0022) VR=SQ VM=1 Detector Information Sequence</summary>
        public readonly static DicomTagSQ DetectorInformationSequence = new DicomTagSQ(0x0054, 0x0022);

        ///<summary>(0054,0030) VR=US VM=1-n Phase Vector</summary>
        public readonly static DicomTagUSs PhaseVector = new DicomTagUSs(0x0054, 0x0030);

        ///<summary>(0054,0031) VR=US VM=1 Number of Phases</summary>
        public readonly static DicomTagUS NumberOfPhases = new DicomTagUS(0x0054, 0x0031);

        ///<summary>(0054,0032) VR=SQ VM=1 Phase Information Sequence</summary>
        public readonly static DicomTagSQ PhaseInformationSequence = new DicomTagSQ(0x0054, 0x0032);

        ///<summary>(0054,0033) VR=US VM=1 Number of Frames in Phase</summary>
        public readonly static DicomTagUS NumberOfFramesInPhase = new DicomTagUS(0x0054, 0x0033);

        ///<summary>(0054,0036) VR=IS VM=1 Phase Delay</summary>
        public readonly static DicomTagIS PhaseDelay = new DicomTagIS(0x0054, 0x0036);

        ///<summary>(0054,0038) VR=IS VM=1 Pause Between Frames</summary>
        public readonly static DicomTagIS PauseBetweenFrames = new DicomTagIS(0x0054, 0x0038);

        ///<summary>(0054,0039) VR=CS VM=1 Phase Description</summary>
        public readonly static DicomTagCS PhaseDescription = new DicomTagCS(0x0054, 0x0039);

        ///<summary>(0054,0050) VR=US VM=1-n Rotation Vector</summary>
        public readonly static DicomTagUSs RotationVector = new DicomTagUSs(0x0054, 0x0050);

        ///<summary>(0054,0051) VR=US VM=1 Number of Rotations</summary>
        public readonly static DicomTagUS NumberOfRotations = new DicomTagUS(0x0054, 0x0051);

        ///<summary>(0054,0052) VR=SQ VM=1 Rotation Information Sequence</summary>
        public readonly static DicomTagSQ RotationInformationSequence = new DicomTagSQ(0x0054, 0x0052);

        ///<summary>(0054,0053) VR=US VM=1 Number of Frames in Rotation</summary>
        public readonly static DicomTagUS NumberOfFramesInRotation = new DicomTagUS(0x0054, 0x0053);

        ///<summary>(0054,0060) VR=US VM=1-n R-R Interval Vector</summary>
        public readonly static DicomTagUSs RRIntervalVector = new DicomTagUSs(0x0054, 0x0060);

        ///<summary>(0054,0061) VR=US VM=1 Number of R-R Intervals</summary>
        public readonly static DicomTagUS NumberOfRRIntervals = new DicomTagUS(0x0054, 0x0061);

        ///<summary>(0054,0062) VR=SQ VM=1 Gated Information Sequence</summary>
        public readonly static DicomTagSQ GatedInformationSequence = new DicomTagSQ(0x0054, 0x0062);

        ///<summary>(0054,0063) VR=SQ VM=1 Data Information Sequence</summary>
        public readonly static DicomTagSQ DataInformationSequence = new DicomTagSQ(0x0054, 0x0063);

        ///<summary>(0054,0070) VR=US VM=1-n Time Slot Vector</summary>
        public readonly static DicomTagUSs TimeSlotVector = new DicomTagUSs(0x0054, 0x0070);

        ///<summary>(0054,0071) VR=US VM=1 Number of Time Slots</summary>
        public readonly static DicomTagUS NumberOfTimeSlots = new DicomTagUS(0x0054, 0x0071);

        ///<summary>(0054,0072) VR=SQ VM=1 Time Slot Information Sequence</summary>
        public readonly static DicomTagSQ TimeSlotInformationSequence = new DicomTagSQ(0x0054, 0x0072);

        ///<summary>(0054,0073) VR=DS VM=1 Time Slot Time</summary>
        public readonly static DicomTagDS TimeSlotTime = new DicomTagDS(0x0054, 0x0073);

        ///<summary>(0054,0080) VR=US VM=1-n Slice Vector</summary>
        public readonly static DicomTagUSs SliceVector = new DicomTagUSs(0x0054, 0x0080);

        ///<summary>(0054,0081) VR=US VM=1 Number of Slices</summary>
        public readonly static DicomTagUS NumberOfSlices = new DicomTagUS(0x0054, 0x0081);

        ///<summary>(0054,0090) VR=US VM=1-n Angular View Vector</summary>
        public readonly static DicomTagUSs AngularViewVector = new DicomTagUSs(0x0054, 0x0090);

        ///<summary>(0054,0100) VR=US VM=1-n Time Slice Vector</summary>
        public readonly static DicomTagUSs TimeSliceVector = new DicomTagUSs(0x0054, 0x0100);

        ///<summary>(0054,0101) VR=US VM=1 Number of Time Slices</summary>
        public readonly static DicomTagUS NumberOfTimeSlices = new DicomTagUS(0x0054, 0x0101);

        ///<summary>(0054,0200) VR=DS VM=1 Start Angle</summary>
        public readonly static DicomTagDS StartAngle = new DicomTagDS(0x0054, 0x0200);

        ///<summary>(0054,0202) VR=CS VM=1 Type of Detector Motion</summary>
        public readonly static DicomTagCS TypeOfDetectorMotion = new DicomTagCS(0x0054, 0x0202);

        ///<summary>(0054,0210) VR=IS VM=1-n Trigger Vector</summary>
        public readonly static DicomTagISs TriggerVector = new DicomTagISs(0x0054, 0x0210);

        ///<summary>(0054,0211) VR=US VM=1 Number of Triggers in Phase</summary>
        public readonly static DicomTagUS NumberOfTriggersInPhase = new DicomTagUS(0x0054, 0x0211);

        ///<summary>(0054,0220) VR=SQ VM=1 View Code Sequence</summary>
        public readonly static DicomTagSQ ViewCodeSequence = new DicomTagSQ(0x0054, 0x0220);

        ///<summary>(0054,0222) VR=SQ VM=1 View Modifier Code Sequence</summary>
        public readonly static DicomTagSQ ViewModifierCodeSequence = new DicomTagSQ(0x0054, 0x0222);

        ///<summary>(0054,0300) VR=SQ VM=1 Radionuclide Code Sequence</summary>
        public readonly static DicomTagSQ RadionuclideCodeSequence = new DicomTagSQ(0x0054, 0x0300);

        ///<summary>(0054,0302) VR=SQ VM=1 Administration Route Code Sequence</summary>
        public readonly static DicomTagSQ AdministrationRouteCodeSequence = new DicomTagSQ(0x0054, 0x0302);

        ///<summary>(0054,0304) VR=SQ VM=1 Radiopharmaceutical Code Sequence</summary>
        public readonly static DicomTagSQ RadiopharmaceuticalCodeSequence = new DicomTagSQ(0x0054, 0x0304);

        ///<summary>(0054,0306) VR=SQ VM=1 Calibration Data Sequence</summary>
        public readonly static DicomTagSQ CalibrationDataSequence = new DicomTagSQ(0x0054, 0x0306);

        ///<summary>(0054,0308) VR=US VM=1 Energy Window Number</summary>
        public readonly static DicomTagUS EnergyWindowNumber = new DicomTagUS(0x0054, 0x0308);

        ///<summary>(0054,0400) VR=SH VM=1 Image ID</summary>
        public readonly static DicomTagSH ImageID = new DicomTagSH(0x0054, 0x0400);

        ///<summary>(0054,0410) VR=SQ VM=1 Patient Orientation Code Sequence</summary>
        public readonly static DicomTagSQ PatientOrientationCodeSequence = new DicomTagSQ(0x0054, 0x0410);

        ///<summary>(0054,0412) VR=SQ VM=1 Patient Orientation Modifier Code Sequence</summary>
        public readonly static DicomTagSQ PatientOrientationModifierCodeSequence = new DicomTagSQ(0x0054, 0x0412);

        ///<summary>(0054,0414) VR=SQ VM=1 Patient Gantry Relationship Code Sequence</summary>
        public readonly static DicomTagSQ PatientGantryRelationshipCodeSequence = new DicomTagSQ(0x0054, 0x0414);

        ///<summary>(0054,0500) VR=CS VM=1 Slice Progression Direction</summary>
        public readonly static DicomTagCS SliceProgressionDirection = new DicomTagCS(0x0054, 0x0500);

        ///<summary>(0054,0501) VR=CS VM=1 Scan Progression Direction</summary>
        public readonly static DicomTagCS ScanProgressionDirection = new DicomTagCS(0x0054, 0x0501);

        ///<summary>(0054,1000) VR=CS VM=2 Series Type</summary>
        public readonly static DicomTagCSs SeriesType = new DicomTagCSs(0x0054, 0x1000);

        ///<summary>(0054,1001) VR=CS VM=1 Units</summary>
        public readonly static DicomTagCS Units = new DicomTagCS(0x0054, 0x1001);

        ///<summary>(0054,1002) VR=CS VM=1 Counts Source</summary>
        public readonly static DicomTagCS CountsSource = new DicomTagCS(0x0054, 0x1002);

        ///<summary>(0054,1004) VR=CS VM=1 Reprojection Method</summary>
        public readonly static DicomTagCS ReprojectionMethod = new DicomTagCS(0x0054, 0x1004);

        ///<summary>(0054,1006) VR=CS VM=1 SUV Type</summary>
        public readonly static DicomTagCS SUVType = new DicomTagCS(0x0054, 0x1006);

        ///<summary>(0054,1100) VR=CS VM=1 Randoms Correction Method</summary>
        public readonly static DicomTagCS RandomsCorrectionMethod = new DicomTagCS(0x0054, 0x1100);

        ///<summary>(0054,1101) VR=LO VM=1 Attenuation Correction Method</summary>
        public readonly static DicomTagLO AttenuationCorrectionMethod = new DicomTagLO(0x0054, 0x1101);

        ///<summary>(0054,1102) VR=CS VM=1 Decay Correction</summary>
        public readonly static DicomTagCS DecayCorrection = new DicomTagCS(0x0054, 0x1102);

        ///<summary>(0054,1103) VR=LO VM=1 Reconstruction Method</summary>
        public readonly static DicomTagLO ReconstructionMethod = new DicomTagLO(0x0054, 0x1103);

        ///<summary>(0054,1104) VR=LO VM=1 Detector Lines of Response Used</summary>
        public readonly static DicomTagLO DetectorLinesOfResponseUsed = new DicomTagLO(0x0054, 0x1104);

        ///<summary>(0054,1105) VR=LO VM=1 Scatter Correction Method</summary>
        public readonly static DicomTagLO ScatterCorrectionMethod = new DicomTagLO(0x0054, 0x1105);

        ///<summary>(0054,1200) VR=DS VM=1 Axial Acceptance</summary>
        public readonly static DicomTagDS AxialAcceptance = new DicomTagDS(0x0054, 0x1200);

        ///<summary>(0054,1201) VR=IS VM=2 Axial Mash</summary>
        public readonly static DicomTagISs AxialMash = new DicomTagISs(0x0054, 0x1201);

        ///<summary>(0054,1202) VR=IS VM=1 Transverse Mash</summary>
        public readonly static DicomTagIS TransverseMash = new DicomTagIS(0x0054, 0x1202);

        ///<summary>(0054,1203) VR=DS VM=2 Detector Element Size</summary>
        public readonly static DicomTagDSs DetectorElementSize = new DicomTagDSs(0x0054, 0x1203);

        ///<summary>(0054,1210) VR=DS VM=1 Coincidence Window Width</summary>
        public readonly static DicomTagDS CoincidenceWindowWidth = new DicomTagDS(0x0054, 0x1210);

        ///<summary>(0054,1220) VR=CS VM=1-n Secondary Counts Type</summary>
        public readonly static DicomTagCSs SecondaryCountsType = new DicomTagCSs(0x0054, 0x1220);

        ///<summary>(0054,1300) VR=DS VM=1 Frame Reference Time</summary>
        public readonly static DicomTagDS FrameReferenceTime = new DicomTagDS(0x0054, 0x1300);

        ///<summary>(0054,1310) VR=IS VM=1 Primary (Prompts) Counts Accumulated</summary>
        public readonly static DicomTagIS PrimaryPromptsCountsAccumulated = new DicomTagIS(0x0054, 0x1310);

        ///<summary>(0054,1311) VR=IS VM=1-n Secondary Counts Accumulated</summary>
        public readonly static DicomTagISs SecondaryCountsAccumulated = new DicomTagISs(0x0054, 0x1311);

        ///<summary>(0054,1320) VR=DS VM=1 Slice Sensitivity Factor</summary>
        public readonly static DicomTagDS SliceSensitivityFactor = new DicomTagDS(0x0054, 0x1320);

        ///<summary>(0054,1321) VR=DS VM=1 Decay Factor</summary>
        public readonly static DicomTagDS DecayFactor = new DicomTagDS(0x0054, 0x1321);

        ///<summary>(0054,1322) VR=DS VM=1 Dose Calibration Factor</summary>
        public readonly static DicomTagDS DoseCalibrationFactor = new DicomTagDS(0x0054, 0x1322);

        ///<summary>(0054,1323) VR=DS VM=1 Scatter Fraction Factor</summary>
        public readonly static DicomTagDS ScatterFractionFactor = new DicomTagDS(0x0054, 0x1323);

        ///<summary>(0054,1324) VR=DS VM=1 Dead Time Factor</summary>
        public readonly static DicomTagDS DeadTimeFactor = new DicomTagDS(0x0054, 0x1324);

        ///<summary>(0054,1330) VR=US VM=1 Image Index</summary>
        public readonly static DicomTagUS ImageIndex = new DicomTagUS(0x0054, 0x1330);

        ///<summary>(0054,1400) VR=CS VM=1-n Counts Included (RETIRED)</summary>
        public readonly static DicomTagCSs CountsIncludedRETIRED = new DicomTagCSs(0x0054, 0x1400);

        ///<summary>(0054,1401) VR=CS VM=1 Dead Time Correction Flag (RETIRED)</summary>
        public readonly static DicomTagCS DeadTimeCorrectionFlagRETIRED = new DicomTagCS(0x0054, 0x1401);

        ///<summary>(0060,3000) VR=SQ VM=1 Histogram Sequence</summary>
        public readonly static DicomTagSQ HistogramSequence = new DicomTagSQ(0x0060, 0x3000);

        ///<summary>(0060,3002) VR=US VM=1 Histogram Number of Bins</summary>
        public readonly static DicomTagUS HistogramNumberOfBins = new DicomTagUS(0x0060, 0x3002);

        ///<summary>(0060,3004) VR=US/SS VM=1 Histogram First Bin Value</summary>
        public readonly static DicomTagUSSS HistogramFirstBinValue = new DicomTagUSSS(0x0060, 0x3004);

        ///<summary>(0060,3006) VR=US/SS VM=1 Histogram Last Bin Value</summary>
        public readonly static DicomTagUSSS HistogramLastBinValue = new DicomTagUSSS(0x0060, 0x3006);

        ///<summary>(0060,3008) VR=US VM=1 Histogram Bin Width</summary>
        public readonly static DicomTagUS HistogramBinWidth = new DicomTagUS(0x0060, 0x3008);

        ///<summary>(0060,3010) VR=LO VM=1 Histogram Explanation</summary>
        public readonly static DicomTagLO HistogramExplanation = new DicomTagLO(0x0060, 0x3010);

        ///<summary>(0060,3020) VR=UL VM=1-n Histogram Data</summary>
        public readonly static DicomTagULs HistogramData = new DicomTagULs(0x0060, 0x3020);

        ///<summary>(0062,0001) VR=CS VM=1 Segmentation Type</summary>
        public readonly static DicomTagCS SegmentationType = new DicomTagCS(0x0062, 0x0001);

        ///<summary>(0062,0002) VR=SQ VM=1 Segment Sequence</summary>
        public readonly static DicomTagSQ SegmentSequence = new DicomTagSQ(0x0062, 0x0002);

        ///<summary>(0062,0003) VR=SQ VM=1 Segmented Property Category Code Sequence</summary>
        public readonly static DicomTagSQ SegmentedPropertyCategoryCodeSequence = new DicomTagSQ(0x0062, 0x0003);

        ///<summary>(0062,0004) VR=US VM=1 Segment Number</summary>
        public readonly static DicomTagUS SegmentNumber = new DicomTagUS(0x0062, 0x0004);

        ///<summary>(0062,0005) VR=LO VM=1 Segment Label</summary>
        public readonly static DicomTagLO SegmentLabel = new DicomTagLO(0x0062, 0x0005);

        ///<summary>(0062,0006) VR=ST VM=1 Segment Description</summary>
        public readonly static DicomTagST SegmentDescription = new DicomTagST(0x0062, 0x0006);

        ///<summary>(0062,0007) VR=SQ VM=1 Segmentation Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ SegmentationAlgorithmIdentificationSequence = new DicomTagSQ(0x0062, 0x0007);

        ///<summary>(0062,0008) VR=CS VM=1 Segment Algorithm Type</summary>
        public readonly static DicomTagCS SegmentAlgorithmType = new DicomTagCS(0x0062, 0x0008);

        ///<summary>(0062,0009) VR=LO VM=1-n Segment Algorithm Name</summary>
        public readonly static DicomTagLOs SegmentAlgorithmName = new DicomTagLOs(0x0062, 0x0009);

        ///<summary>(0062,000A) VR=SQ VM=1 Segment Identification Sequence</summary>
        public readonly static DicomTagSQ SegmentIdentificationSequence = new DicomTagSQ(0x0062, 0x000A);

        ///<summary>(0062,000B) VR=US VM=1-n Referenced Segment Number</summary>
        public readonly static DicomTagUSs ReferencedSegmentNumber = new DicomTagUSs(0x0062, 0x000B);

        ///<summary>(0062,000C) VR=US VM=1 Recommended Display Grayscale Value</summary>
        public readonly static DicomTagUS RecommendedDisplayGrayscaleValue = new DicomTagUS(0x0062, 0x000C);

        ///<summary>(0062,000D) VR=US VM=3 Recommended Display CIELab Value</summary>
        public readonly static DicomTagUSs RecommendedDisplayCIELabValue = new DicomTagUSs(0x0062, 0x000D);

        ///<summary>(0062,000E) VR=US VM=1 Maximum Fractional Value</summary>
        public readonly static DicomTagUS MaximumFractionalValue = new DicomTagUS(0x0062, 0x000E);

        ///<summary>(0062,000F) VR=SQ VM=1 Segmented Property Type Code Sequence</summary>
        public readonly static DicomTagSQ SegmentedPropertyTypeCodeSequence = new DicomTagSQ(0x0062, 0x000F);

        ///<summary>(0062,0010) VR=CS VM=1 Segmentation Fractional Type</summary>
        public readonly static DicomTagCS SegmentationFractionalType = new DicomTagCS(0x0062, 0x0010);

        ///<summary>(0062,0011) VR=SQ VM=1 Segmented Property Type Modifier Code Sequence</summary>
        public readonly static DicomTagSQ SegmentedPropertyTypeModifierCodeSequence = new DicomTagSQ(0x0062, 0x0011);

        ///<summary>(0062,0012) VR=SQ VM=1 Used Segments Sequence</summary>
        public readonly static DicomTagSQ UsedSegmentsSequence = new DicomTagSQ(0x0062, 0x0012);

        ///<summary>(0062,0013) VR=CS VM=1 Segments Overlap</summary>
        public readonly static DicomTagCS SegmentsOverlap = new DicomTagCS(0x0062, 0x0013);

        ///<summary>(0062,0020) VR=UT VM=1 Tracking ID</summary>
        public readonly static DicomTagUT TrackingID = new DicomTagUT(0x0062, 0x0020);

        ///<summary>(0062,0021) VR=UI VM=1 Tracking UID</summary>
        public readonly static DicomTagUI TrackingUID = new DicomTagUI(0x0062, 0x0021);

        ///<summary>(0064,0002) VR=SQ VM=1 Deformable Registration Sequence</summary>
        public readonly static DicomTagSQ DeformableRegistrationSequence = new DicomTagSQ(0x0064, 0x0002);

        ///<summary>(0064,0003) VR=UI VM=1 Source Frame of Reference UID</summary>
        public readonly static DicomTagUI SourceFrameOfReferenceUID = new DicomTagUI(0x0064, 0x0003);

        ///<summary>(0064,0005) VR=SQ VM=1 Deformable Registration Grid Sequence</summary>
        public readonly static DicomTagSQ DeformableRegistrationGridSequence = new DicomTagSQ(0x0064, 0x0005);

        ///<summary>(0064,0007) VR=UL VM=3 Grid Dimensions</summary>
        public readonly static DicomTagULs GridDimensions = new DicomTagULs(0x0064, 0x0007);

        ///<summary>(0064,0008) VR=FD VM=3 Grid Resolution</summary>
        public readonly static DicomTagFDs GridResolution = new DicomTagFDs(0x0064, 0x0008);

        ///<summary>(0064,0009) VR=OF VM=1 Vector Grid Data</summary>
        public readonly static DicomTagOF VectorGridData = new DicomTagOF(0x0064, 0x0009);

        ///<summary>(0064,000F) VR=SQ VM=1 Pre Deformation Matrix Registration Sequence</summary>
        public readonly static DicomTagSQ PreDeformationMatrixRegistrationSequence = new DicomTagSQ(0x0064, 0x000F);

        ///<summary>(0064,0010) VR=SQ VM=1 Post Deformation Matrix Registration Sequence</summary>
        public readonly static DicomTagSQ PostDeformationMatrixRegistrationSequence = new DicomTagSQ(0x0064, 0x0010);

        ///<summary>(0066,0001) VR=UL VM=1 Number of Surfaces</summary>
        public readonly static DicomTagUL NumberOfSurfaces = new DicomTagUL(0x0066, 0x0001);

        ///<summary>(0066,0002) VR=SQ VM=1 Surface Sequence</summary>
        public readonly static DicomTagSQ SurfaceSequence = new DicomTagSQ(0x0066, 0x0002);

        ///<summary>(0066,0003) VR=UL VM=1 Surface Number</summary>
        public readonly static DicomTagUL SurfaceNumber = new DicomTagUL(0x0066, 0x0003);

        ///<summary>(0066,0004) VR=LT VM=1 Surface Comments</summary>
        public readonly static DicomTagLT SurfaceComments = new DicomTagLT(0x0066, 0x0004);

        ///<summary>(0066,0005) VR=FL VM=1 Surface Offset</summary>
        public readonly static DicomTagFL SurfaceOffset = new DicomTagFL(0x0066, 0x0005);

        ///<summary>(0066,0009) VR=CS VM=1 Surface Processing</summary>
        public readonly static DicomTagCS SurfaceProcessing = new DicomTagCS(0x0066, 0x0009);

        ///<summary>(0066,000A) VR=FL VM=1 Surface Processing Ratio</summary>
        public readonly static DicomTagFL SurfaceProcessingRatio = new DicomTagFL(0x0066, 0x000A);

        ///<summary>(0066,000B) VR=LO VM=1 Surface Processing Description</summary>
        public readonly static DicomTagLO SurfaceProcessingDescription = new DicomTagLO(0x0066, 0x000B);

        ///<summary>(0066,000C) VR=FL VM=1 Recommended Presentation Opacity</summary>
        public readonly static DicomTagFL RecommendedPresentationOpacity = new DicomTagFL(0x0066, 0x000C);

        ///<summary>(0066,000D) VR=CS VM=1 Recommended Presentation Type</summary>
        public readonly static DicomTagCS RecommendedPresentationType = new DicomTagCS(0x0066, 0x000D);

        ///<summary>(0066,000E) VR=CS VM=1 Finite Volume</summary>
        public readonly static DicomTagCS FiniteVolume = new DicomTagCS(0x0066, 0x000E);

        ///<summary>(0066,0010) VR=CS VM=1 Manifold</summary>
        public readonly static DicomTagCS Manifold = new DicomTagCS(0x0066, 0x0010);

        ///<summary>(0066,0011) VR=SQ VM=1 Surface Points Sequence</summary>
        public readonly static DicomTagSQ SurfacePointsSequence = new DicomTagSQ(0x0066, 0x0011);

        ///<summary>(0066,0012) VR=SQ VM=1 Surface Points Normals Sequence</summary>
        public readonly static DicomTagSQ SurfacePointsNormalsSequence = new DicomTagSQ(0x0066, 0x0012);

        ///<summary>(0066,0013) VR=SQ VM=1 Surface Mesh Primitives Sequence</summary>
        public readonly static DicomTagSQ SurfaceMeshPrimitivesSequence = new DicomTagSQ(0x0066, 0x0013);

        ///<summary>(0066,0015) VR=UL VM=1 Number of Surface Points</summary>
        public readonly static DicomTagUL NumberOfSurfacePoints = new DicomTagUL(0x0066, 0x0015);

        ///<summary>(0066,0016) VR=OF VM=1 Point Coordinates Data</summary>
        public readonly static DicomTagOF PointCoordinatesData = new DicomTagOF(0x0066, 0x0016);

        ///<summary>(0066,0017) VR=FL VM=3 Point Position Accuracy</summary>
        public readonly static DicomTagFLs PointPositionAccuracy = new DicomTagFLs(0x0066, 0x0017);

        ///<summary>(0066,0018) VR=FL VM=1 Mean Point Distance</summary>
        public readonly static DicomTagFL MeanPointDistance = new DicomTagFL(0x0066, 0x0018);

        ///<summary>(0066,0019) VR=FL VM=1 Maximum Point Distance</summary>
        public readonly static DicomTagFL MaximumPointDistance = new DicomTagFL(0x0066, 0x0019);

        ///<summary>(0066,001A) VR=FL VM=6 Points Bounding Box Coordinates</summary>
        public readonly static DicomTagFLs PointsBoundingBoxCoordinates = new DicomTagFLs(0x0066, 0x001A);

        ///<summary>(0066,001B) VR=FL VM=3 Axis of Rotation</summary>
        public readonly static DicomTagFLs AxisOfRotation = new DicomTagFLs(0x0066, 0x001B);

        ///<summary>(0066,001C) VR=FL VM=3 Center of Rotation</summary>
        public readonly static DicomTagFLs CenterOfRotation = new DicomTagFLs(0x0066, 0x001C);

        ///<summary>(0066,001E) VR=UL VM=1 Number of Vectors</summary>
        public readonly static DicomTagUL NumberOfVectors = new DicomTagUL(0x0066, 0x001E);

        ///<summary>(0066,001F) VR=US VM=1 Vector Dimensionality</summary>
        public readonly static DicomTagUS VectorDimensionality = new DicomTagUS(0x0066, 0x001F);

        ///<summary>(0066,0020) VR=FL VM=1-n Vector Accuracy</summary>
        public readonly static DicomTagFLs VectorAccuracy = new DicomTagFLs(0x0066, 0x0020);

        ///<summary>(0066,0021) VR=OF VM=1 Vector Coordinate Data</summary>
        public readonly static DicomTagOF VectorCoordinateData = new DicomTagOF(0x0066, 0x0021);

        ///<summary>(0066,0022) VR=OD VM=1 Double Point Coordinates Data</summary>
        public readonly static DicomTagOD DoublePointCoordinatesData = new DicomTagOD(0x0066, 0x0022);

        ///<summary>(0066,0023) VR=OW VM=1 Triangle Point Index List (RETIRED)</summary>
        public readonly static DicomTagOW TrianglePointIndexListRETIRED = new DicomTagOW(0x0066, 0x0023);

        ///<summary>(0066,0024) VR=OW VM=1 Edge Point Index List (RETIRED)</summary>
        public readonly static DicomTagOW EdgePointIndexListRETIRED = new DicomTagOW(0x0066, 0x0024);

        ///<summary>(0066,0025) VR=OW VM=1 Vertex Point Index List (RETIRED)</summary>
        public readonly static DicomTagOW VertexPointIndexListRETIRED = new DicomTagOW(0x0066, 0x0025);

        ///<summary>(0066,0026) VR=SQ VM=1 Triangle Strip Sequence</summary>
        public readonly static DicomTagSQ TriangleStripSequence = new DicomTagSQ(0x0066, 0x0026);

        ///<summary>(0066,0027) VR=SQ VM=1 Triangle Fan Sequence</summary>
        public readonly static DicomTagSQ TriangleFanSequence = new DicomTagSQ(0x0066, 0x0027);

        ///<summary>(0066,0028) VR=SQ VM=1 Line Sequence</summary>
        public readonly static DicomTagSQ LineSequence = new DicomTagSQ(0x0066, 0x0028);

        ///<summary>(0066,0029) VR=OW VM=1 Primitive Point Index List (RETIRED)</summary>
        public readonly static DicomTagOW PrimitivePointIndexListRETIRED = new DicomTagOW(0x0066, 0x0029);

        ///<summary>(0066,002A) VR=UL VM=1 Surface Count</summary>
        public readonly static DicomTagUL SurfaceCount = new DicomTagUL(0x0066, 0x002A);

        ///<summary>(0066,002B) VR=SQ VM=1 Referenced Surface Sequence</summary>
        public readonly static DicomTagSQ ReferencedSurfaceSequence = new DicomTagSQ(0x0066, 0x002B);

        ///<summary>(0066,002C) VR=UL VM=1 Referenced Surface Number</summary>
        public readonly static DicomTagUL ReferencedSurfaceNumber = new DicomTagUL(0x0066, 0x002C);

        ///<summary>(0066,002D) VR=SQ VM=1 Segment Surface Generation Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ SegmentSurfaceGenerationAlgorithmIdentificationSequence = new DicomTagSQ(0x0066, 0x002D);

        ///<summary>(0066,002E) VR=SQ VM=1 Segment Surface Source Instance Sequence</summary>
        public readonly static DicomTagSQ SegmentSurfaceSourceInstanceSequence = new DicomTagSQ(0x0066, 0x002E);

        ///<summary>(0066,002F) VR=SQ VM=1 Algorithm Family Code Sequence</summary>
        public readonly static DicomTagSQ AlgorithmFamilyCodeSequence = new DicomTagSQ(0x0066, 0x002F);

        ///<summary>(0066,0030) VR=SQ VM=1 Algorithm Name Code Sequence</summary>
        public readonly static DicomTagSQ AlgorithmNameCodeSequence = new DicomTagSQ(0x0066, 0x0030);

        ///<summary>(0066,0031) VR=LO VM=1 Algorithm Version</summary>
        public readonly static DicomTagLO AlgorithmVersion = new DicomTagLO(0x0066, 0x0031);

        ///<summary>(0066,0032) VR=LT VM=1 Algorithm Parameters</summary>
        public readonly static DicomTagLT AlgorithmParameters = new DicomTagLT(0x0066, 0x0032);

        ///<summary>(0066,0034) VR=SQ VM=1 Facet Sequence</summary>
        public readonly static DicomTagSQ FacetSequence = new DicomTagSQ(0x0066, 0x0034);

        ///<summary>(0066,0035) VR=SQ VM=1 Surface Processing Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ SurfaceProcessingAlgorithmIdentificationSequence = new DicomTagSQ(0x0066, 0x0035);

        ///<summary>(0066,0036) VR=LO VM=1 Algorithm Name</summary>
        public readonly static DicomTagLO AlgorithmName = new DicomTagLO(0x0066, 0x0036);

        ///<summary>(0066,0037) VR=FL VM=1 Recommended Point Radius</summary>
        public readonly static DicomTagFL RecommendedPointRadius = new DicomTagFL(0x0066, 0x0037);

        ///<summary>(0066,0038) VR=FL VM=1 Recommended Line Thickness</summary>
        public readonly static DicomTagFL RecommendedLineThickness = new DicomTagFL(0x0066, 0x0038);

        ///<summary>(0066,0040) VR=OL VM=1 Long Primitive Point Index List</summary>
        public readonly static DicomTagOL LongPrimitivePointIndexList = new DicomTagOL(0x0066, 0x0040);

        ///<summary>(0066,0041) VR=OL VM=1 Long Triangle Point Index List</summary>
        public readonly static DicomTagOL LongTrianglePointIndexList = new DicomTagOL(0x0066, 0x0041);

        ///<summary>(0066,0042) VR=OL VM=1 Long Edge Point Index List</summary>
        public readonly static DicomTagOL LongEdgePointIndexList = new DicomTagOL(0x0066, 0x0042);

        ///<summary>(0066,0043) VR=OL VM=1 Long Vertex Point Index List</summary>
        public readonly static DicomTagOL LongVertexPointIndexList = new DicomTagOL(0x0066, 0x0043);

        ///<summary>(0066,0101) VR=SQ VM=1 Track Set Sequence</summary>
        public readonly static DicomTagSQ TrackSetSequence = new DicomTagSQ(0x0066, 0x0101);

        ///<summary>(0066,0102) VR=SQ VM=1 Track Sequence</summary>
        public readonly static DicomTagSQ TrackSequence = new DicomTagSQ(0x0066, 0x0102);

        ///<summary>(0066,0103) VR=OW VM=1 Recommended Display CIELab Value List</summary>
        public readonly static DicomTagOW RecommendedDisplayCIELabValueList = new DicomTagOW(0x0066, 0x0103);

        ///<summary>(0066,0104) VR=SQ VM=1 Tracking Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ TrackingAlgorithmIdentificationSequence = new DicomTagSQ(0x0066, 0x0104);

        ///<summary>(0066,0105) VR=UL VM=1 Track Set Number</summary>
        public readonly static DicomTagUL TrackSetNumber = new DicomTagUL(0x0066, 0x0105);

        ///<summary>(0066,0106) VR=LO VM=1 Track Set Label</summary>
        public readonly static DicomTagLO TrackSetLabel = new DicomTagLO(0x0066, 0x0106);

        ///<summary>(0066,0107) VR=UT VM=1 Track Set Description</summary>
        public readonly static DicomTagUT TrackSetDescription = new DicomTagUT(0x0066, 0x0107);

        ///<summary>(0066,0108) VR=SQ VM=1 Track Set Anatomical Type Code Sequence</summary>
        public readonly static DicomTagSQ TrackSetAnatomicalTypeCodeSequence = new DicomTagSQ(0x0066, 0x0108);

        ///<summary>(0066,0121) VR=SQ VM=1 Measurements Sequence</summary>
        public readonly static DicomTagSQ MeasurementsSequence = new DicomTagSQ(0x0066, 0x0121);

        ///<summary>(0066,0124) VR=SQ VM=1 Track Set Statistics Sequence</summary>
        public readonly static DicomTagSQ TrackSetStatisticsSequence = new DicomTagSQ(0x0066, 0x0124);

        ///<summary>(0066,0125) VR=OF VM=1 Floating Point Values</summary>
        public readonly static DicomTagOF FloatingPointValues = new DicomTagOF(0x0066, 0x0125);

        ///<summary>(0066,0129) VR=OL VM=1 Track Point Index List</summary>
        public readonly static DicomTagOL TrackPointIndexList = new DicomTagOL(0x0066, 0x0129);

        ///<summary>(0066,0130) VR=SQ VM=1 Track Statistics Sequence</summary>
        public readonly static DicomTagSQ TrackStatisticsSequence = new DicomTagSQ(0x0066, 0x0130);

        ///<summary>(0066,0132) VR=SQ VM=1 Measurement Values Sequence</summary>
        public readonly static DicomTagSQ MeasurementValuesSequence = new DicomTagSQ(0x0066, 0x0132);

        ///<summary>(0066,0133) VR=SQ VM=1 Diffusion Acquisition Code Sequence</summary>
        public readonly static DicomTagSQ DiffusionAcquisitionCodeSequence = new DicomTagSQ(0x0066, 0x0133);

        ///<summary>(0066,0134) VR=SQ VM=1 Diffusion Model Code Sequence</summary>
        public readonly static DicomTagSQ DiffusionModelCodeSequence = new DicomTagSQ(0x0066, 0x0134);

        ///<summary>(0068,6210) VR=LO VM=1 Implant Size</summary>
        public readonly static DicomTagLO ImplantSize = new DicomTagLO(0x0068, 0x6210);

        ///<summary>(0068,6221) VR=LO VM=1 Implant Template Version</summary>
        public readonly static DicomTagLO ImplantTemplateVersion = new DicomTagLO(0x0068, 0x6221);

        ///<summary>(0068,6222) VR=SQ VM=1 Replaced Implant Template Sequence</summary>
        public readonly static DicomTagSQ ReplacedImplantTemplateSequence = new DicomTagSQ(0x0068, 0x6222);

        ///<summary>(0068,6223) VR=CS VM=1 Implant Type</summary>
        public readonly static DicomTagCS ImplantType = new DicomTagCS(0x0068, 0x6223);

        ///<summary>(0068,6224) VR=SQ VM=1 Derivation Implant Template Sequence</summary>
        public readonly static DicomTagSQ DerivationImplantTemplateSequence = new DicomTagSQ(0x0068, 0x6224);

        ///<summary>(0068,6225) VR=SQ VM=1 Original Implant Template Sequence</summary>
        public readonly static DicomTagSQ OriginalImplantTemplateSequence = new DicomTagSQ(0x0068, 0x6225);

        ///<summary>(0068,6226) VR=DT VM=1 Effective DateTime</summary>
        public readonly static DicomTagDT EffectiveDateTime = new DicomTagDT(0x0068, 0x6226);

        ///<summary>(0068,6230) VR=SQ VM=1 Implant Target Anatomy Sequence</summary>
        public readonly static DicomTagSQ ImplantTargetAnatomySequence = new DicomTagSQ(0x0068, 0x6230);

        ///<summary>(0068,6260) VR=SQ VM=1 Information From Manufacturer Sequence</summary>
        public readonly static DicomTagSQ InformationFromManufacturerSequence = new DicomTagSQ(0x0068, 0x6260);

        ///<summary>(0068,6265) VR=SQ VM=1 Notification From Manufacturer Sequence</summary>
        public readonly static DicomTagSQ NotificationFromManufacturerSequence = new DicomTagSQ(0x0068, 0x6265);

        ///<summary>(0068,6270) VR=DT VM=1 Information Issue DateTime</summary>
        public readonly static DicomTagDT InformationIssueDateTime = new DicomTagDT(0x0068, 0x6270);

        ///<summary>(0068,6280) VR=ST VM=1 Information Summary</summary>
        public readonly static DicomTagST InformationSummary = new DicomTagST(0x0068, 0x6280);

        ///<summary>(0068,62A0) VR=SQ VM=1 Implant Regulatory Disapproval Code Sequence</summary>
        public readonly static DicomTagSQ ImplantRegulatoryDisapprovalCodeSequence = new DicomTagSQ(0x0068, 0x62A0);

        ///<summary>(0068,62A5) VR=FD VM=1 Overall Template Spatial Tolerance</summary>
        public readonly static DicomTagFD OverallTemplateSpatialTolerance = new DicomTagFD(0x0068, 0x62A5);

        ///<summary>(0068,62C0) VR=SQ VM=1 HPGL Document Sequence</summary>
        public readonly static DicomTagSQ HPGLDocumentSequence = new DicomTagSQ(0x0068, 0x62C0);

        ///<summary>(0068,62D0) VR=US VM=1 HPGL Document ID</summary>
        public readonly static DicomTagUS HPGLDocumentID = new DicomTagUS(0x0068, 0x62D0);

        ///<summary>(0068,62D5) VR=LO VM=1 HPGL Document Label</summary>
        public readonly static DicomTagLO HPGLDocumentLabel = new DicomTagLO(0x0068, 0x62D5);

        ///<summary>(0068,62E0) VR=SQ VM=1 View Orientation Code Sequence</summary>
        public readonly static DicomTagSQ ViewOrientationCodeSequence = new DicomTagSQ(0x0068, 0x62E0);

        ///<summary>(0068,62F0) VR=SQ VM=1 View Orientation Modifier Code Sequence</summary>
        public readonly static DicomTagSQ ViewOrientationModifierCodeSequence = new DicomTagSQ(0x0068, 0x62F0);

        ///<summary>(0068,62F2) VR=FD VM=1 HPGL Document Scaling</summary>
        public readonly static DicomTagFD HPGLDocumentScaling = new DicomTagFD(0x0068, 0x62F2);

        ///<summary>(0068,6300) VR=OB VM=1 HPGL Document</summary>
        public readonly static DicomTagOB HPGLDocument = new DicomTagOB(0x0068, 0x6300);

        ///<summary>(0068,6310) VR=US VM=1 HPGL Contour Pen Number</summary>
        public readonly static DicomTagUS HPGLContourPenNumber = new DicomTagUS(0x0068, 0x6310);

        ///<summary>(0068,6320) VR=SQ VM=1 HPGL Pen Sequence</summary>
        public readonly static DicomTagSQ HPGLPenSequence = new DicomTagSQ(0x0068, 0x6320);

        ///<summary>(0068,6330) VR=US VM=1 HPGL Pen Number</summary>
        public readonly static DicomTagUS HPGLPenNumber = new DicomTagUS(0x0068, 0x6330);

        ///<summary>(0068,6340) VR=LO VM=1 HPGL Pen Label</summary>
        public readonly static DicomTagLO HPGLPenLabel = new DicomTagLO(0x0068, 0x6340);

        ///<summary>(0068,6345) VR=ST VM=1 HPGL Pen Description</summary>
        public readonly static DicomTagST HPGLPenDescription = new DicomTagST(0x0068, 0x6345);

        ///<summary>(0068,6346) VR=FD VM=2 Recommended Rotation Point</summary>
        public readonly static DicomTagFDs RecommendedRotationPoint = new DicomTagFDs(0x0068, 0x6346);

        ///<summary>(0068,6347) VR=FD VM=4 Bounding Rectangle</summary>
        public readonly static DicomTagFDs BoundingRectangle = new DicomTagFDs(0x0068, 0x6347);

        ///<summary>(0068,6350) VR=US VM=1-n Implant Template 3D Model Surface Number</summary>
        public readonly static DicomTagUSs ImplantTemplate3DModelSurfaceNumber = new DicomTagUSs(0x0068, 0x6350);

        ///<summary>(0068,6360) VR=SQ VM=1 Surface Model Description Sequence</summary>
        public readonly static DicomTagSQ SurfaceModelDescriptionSequence = new DicomTagSQ(0x0068, 0x6360);

        ///<summary>(0068,6380) VR=LO VM=1 Surface Model Label</summary>
        public readonly static DicomTagLO SurfaceModelLabel = new DicomTagLO(0x0068, 0x6380);

        ///<summary>(0068,6390) VR=FD VM=1 Surface Model Scaling Factor</summary>
        public readonly static DicomTagFD SurfaceModelScalingFactor = new DicomTagFD(0x0068, 0x6390);

        ///<summary>(0068,63A0) VR=SQ VM=1 Materials Code Sequence</summary>
        public readonly static DicomTagSQ MaterialsCodeSequence = new DicomTagSQ(0x0068, 0x63A0);

        ///<summary>(0068,63A4) VR=SQ VM=1 Coating Materials Code Sequence</summary>
        public readonly static DicomTagSQ CoatingMaterialsCodeSequence = new DicomTagSQ(0x0068, 0x63A4);

        ///<summary>(0068,63A8) VR=SQ VM=1 Implant Type Code Sequence</summary>
        public readonly static DicomTagSQ ImplantTypeCodeSequence = new DicomTagSQ(0x0068, 0x63A8);

        ///<summary>(0068,63AC) VR=SQ VM=1 Fixation Method Code Sequence</summary>
        public readonly static DicomTagSQ FixationMethodCodeSequence = new DicomTagSQ(0x0068, 0x63AC);

        ///<summary>(0068,63B0) VR=SQ VM=1 Mating Feature Sets Sequence</summary>
        public readonly static DicomTagSQ MatingFeatureSetsSequence = new DicomTagSQ(0x0068, 0x63B0);

        ///<summary>(0068,63C0) VR=US VM=1 Mating Feature Set ID</summary>
        public readonly static DicomTagUS MatingFeatureSetID = new DicomTagUS(0x0068, 0x63C0);

        ///<summary>(0068,63D0) VR=LO VM=1 Mating Feature Set Label</summary>
        public readonly static DicomTagLO MatingFeatureSetLabel = new DicomTagLO(0x0068, 0x63D0);

        ///<summary>(0068,63E0) VR=SQ VM=1 Mating Feature Sequence</summary>
        public readonly static DicomTagSQ MatingFeatureSequence = new DicomTagSQ(0x0068, 0x63E0);

        ///<summary>(0068,63F0) VR=US VM=1 Mating Feature ID</summary>
        public readonly static DicomTagUS MatingFeatureID = new DicomTagUS(0x0068, 0x63F0);

        ///<summary>(0068,6400) VR=SQ VM=1 Mating Feature Degree of Freedom Sequence</summary>
        public readonly static DicomTagSQ MatingFeatureDegreeOfFreedomSequence = new DicomTagSQ(0x0068, 0x6400);

        ///<summary>(0068,6410) VR=US VM=1 Degree of Freedom ID</summary>
        public readonly static DicomTagUS DegreeOfFreedomID = new DicomTagUS(0x0068, 0x6410);

        ///<summary>(0068,6420) VR=CS VM=1 Degree of Freedom Type</summary>
        public readonly static DicomTagCS DegreeOfFreedomType = new DicomTagCS(0x0068, 0x6420);

        ///<summary>(0068,6430) VR=SQ VM=1 2D Mating Feature Coordinates Sequence</summary>
        public readonly static DicomTagSQ TwoDMatingFeatureCoordinatesSequence = new DicomTagSQ(0x0068, 0x6430);

        ///<summary>(0068,6440) VR=US VM=1 Referenced HPGL Document ID</summary>
        public readonly static DicomTagUS ReferencedHPGLDocumentID = new DicomTagUS(0x0068, 0x6440);

        ///<summary>(0068,6450) VR=FD VM=2 2D Mating Point</summary>
        public readonly static DicomTagFDs TwoDMatingPoint = new DicomTagFDs(0x0068, 0x6450);

        ///<summary>(0068,6460) VR=FD VM=4 2D Mating Axes</summary>
        public readonly static DicomTagFDs TwoDMatingAxes = new DicomTagFDs(0x0068, 0x6460);

        ///<summary>(0068,6470) VR=SQ VM=1 2D Degree of Freedom Sequence</summary>
        public readonly static DicomTagSQ TwoDDegreeOfFreedomSequence = new DicomTagSQ(0x0068, 0x6470);

        ///<summary>(0068,6490) VR=FD VM=3 3D Degree of Freedom Axis</summary>
        public readonly static DicomTagFDs ThreeDDegreeOfFreedomAxis = new DicomTagFDs(0x0068, 0x6490);

        ///<summary>(0068,64A0) VR=FD VM=2 Range of Freedom</summary>
        public readonly static DicomTagFDs RangeOfFreedom = new DicomTagFDs(0x0068, 0x64A0);

        ///<summary>(0068,64C0) VR=FD VM=3 3D Mating Point</summary>
        public readonly static DicomTagFDs ThreeDMatingPoint = new DicomTagFDs(0x0068, 0x64C0);

        ///<summary>(0068,64D0) VR=FD VM=9 3D Mating Axes</summary>
        public readonly static DicomTagFDs ThreeDMatingAxes = new DicomTagFDs(0x0068, 0x64D0);

        ///<summary>(0068,64F0) VR=FD VM=3 2D Degree of Freedom Axis</summary>
        public readonly static DicomTagFDs TwoDDegreeOfFreedomAxis = new DicomTagFDs(0x0068, 0x64F0);

        ///<summary>(0068,6500) VR=SQ VM=1 Planning Landmark Point Sequence</summary>
        public readonly static DicomTagSQ PlanningLandmarkPointSequence = new DicomTagSQ(0x0068, 0x6500);

        ///<summary>(0068,6510) VR=SQ VM=1 Planning Landmark Line Sequence</summary>
        public readonly static DicomTagSQ PlanningLandmarkLineSequence = new DicomTagSQ(0x0068, 0x6510);

        ///<summary>(0068,6520) VR=SQ VM=1 Planning Landmark Plane Sequence</summary>
        public readonly static DicomTagSQ PlanningLandmarkPlaneSequence = new DicomTagSQ(0x0068, 0x6520);

        ///<summary>(0068,6530) VR=US VM=1 Planning Landmark ID</summary>
        public readonly static DicomTagUS PlanningLandmarkID = new DicomTagUS(0x0068, 0x6530);

        ///<summary>(0068,6540) VR=LO VM=1 Planning Landmark Description</summary>
        public readonly static DicomTagLO PlanningLandmarkDescription = new DicomTagLO(0x0068, 0x6540);

        ///<summary>(0068,6545) VR=SQ VM=1 Planning Landmark Identification Code Sequence</summary>
        public readonly static DicomTagSQ PlanningLandmarkIdentificationCodeSequence = new DicomTagSQ(0x0068, 0x6545);

        ///<summary>(0068,6550) VR=SQ VM=1 2D Point Coordinates Sequence</summary>
        public readonly static DicomTagSQ TwoDPointCoordinatesSequence = new DicomTagSQ(0x0068, 0x6550);

        ///<summary>(0068,6560) VR=FD VM=2 2D Point Coordinates</summary>
        public readonly static DicomTagFDs TwoDPointCoordinates = new DicomTagFDs(0x0068, 0x6560);

        ///<summary>(0068,6590) VR=FD VM=3 3D Point Coordinates</summary>
        public readonly static DicomTagFDs ThreeDPointCoordinates = new DicomTagFDs(0x0068, 0x6590);

        ///<summary>(0068,65A0) VR=SQ VM=1 2D Line Coordinates Sequence</summary>
        public readonly static DicomTagSQ TwoDLineCoordinatesSequence = new DicomTagSQ(0x0068, 0x65A0);

        ///<summary>(0068,65B0) VR=FD VM=4 2D Line Coordinates</summary>
        public readonly static DicomTagFDs TwoDLineCoordinates = new DicomTagFDs(0x0068, 0x65B0);

        ///<summary>(0068,65D0) VR=FD VM=6 3D Line Coordinates</summary>
        public readonly static DicomTagFDs ThreeDLineCoordinates = new DicomTagFDs(0x0068, 0x65D0);

        ///<summary>(0068,65E0) VR=SQ VM=1 2D Plane Coordinates Sequence</summary>
        public readonly static DicomTagSQ TwoDPlaneCoordinatesSequence = new DicomTagSQ(0x0068, 0x65E0);

        ///<summary>(0068,65F0) VR=FD VM=4 2D Plane Intersection</summary>
        public readonly static DicomTagFDs TwoDPlaneIntersection = new DicomTagFDs(0x0068, 0x65F0);

        ///<summary>(0068,6610) VR=FD VM=3 3D Plane Origin</summary>
        public readonly static DicomTagFDs ThreeDPlaneOrigin = new DicomTagFDs(0x0068, 0x6610);

        ///<summary>(0068,6620) VR=FD VM=3 3D Plane Normal</summary>
        public readonly static DicomTagFDs ThreeDPlaneNormal = new DicomTagFDs(0x0068, 0x6620);

        ///<summary>(0068,7001) VR=CS VM=1 Model Modification</summary>
        public readonly static DicomTagCS ModelModification = new DicomTagCS(0x0068, 0x7001);

        ///<summary>(0068,7002) VR=CS VM=1 Model Mirroring</summary>
        public readonly static DicomTagCS ModelMirroring = new DicomTagCS(0x0068, 0x7002);

        ///<summary>(0068,7003) VR=SQ VM=1 Model Usage Code Sequence</summary>
        public readonly static DicomTagSQ ModelUsageCodeSequence = new DicomTagSQ(0x0068, 0x7003);

        ///<summary>(0068,7004) VR=UI VM=1 Model Group UID</summary>
        public readonly static DicomTagUI ModelGroupUID = new DicomTagUI(0x0068, 0x7004);

        ///<summary>(0068,7005) VR=UR VM=1 Relative URI Reference Within Encapsulated Document</summary>
        public readonly static DicomTagUR RelativeURIReferenceWithinEncapsulatedDocument = new DicomTagUR(0x0068, 0x7005);

        ///<summary>(006A,0001) VR=CS VM=1 Annotation Coordinate Type</summary>
        public readonly static DicomTagCS AnnotationCoordinateType = new DicomTagCS(0x006A, 0x0001);

        ///<summary>(006A,0002) VR=SQ VM=1 Annotation Group Sequence</summary>
        public readonly static DicomTagSQ AnnotationGroupSequence = new DicomTagSQ(0x006A, 0x0002);

        ///<summary>(006A,0003) VR=UI VM=1 Annotation Group UID</summary>
        public readonly static DicomTagUI AnnotationGroupUID = new DicomTagUI(0x006A, 0x0003);

        ///<summary>(006A,0005) VR=LO VM=1 Annotation Group Label</summary>
        public readonly static DicomTagLO AnnotationGroupLabel = new DicomTagLO(0x006A, 0x0005);

        ///<summary>(006A,0006) VR=UT VM=1 Annotation Group Description</summary>
        public readonly static DicomTagUT AnnotationGroupDescription = new DicomTagUT(0x006A, 0x0006);

        ///<summary>(006A,0007) VR=CS VM=1 Annotation Group Generation Type</summary>
        public readonly static DicomTagCS AnnotationGroupGenerationType = new DicomTagCS(0x006A, 0x0007);

        ///<summary>(006A,0008) VR=SQ VM=1 Annotation Group Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ AnnotationGroupAlgorithmIdentificationSequence = new DicomTagSQ(0x006A, 0x0008);

        ///<summary>(006A,0009) VR=SQ VM=1 Annotation Property Category Code Sequence</summary>
        public readonly static DicomTagSQ AnnotationPropertyCategoryCodeSequence = new DicomTagSQ(0x006A, 0x0009);

        ///<summary>(006A,000A) VR=SQ VM=1 Annotation Property Type Code Sequence</summary>
        public readonly static DicomTagSQ AnnotationPropertyTypeCodeSequence = new DicomTagSQ(0x006A, 0x000A);

        ///<summary>(006A,000B) VR=SQ VM=1 Annotation Property Type Modifier Code Sequence</summary>
        public readonly static DicomTagSQ AnnotationPropertyTypeModifierCodeSequence = new DicomTagSQ(0x006A, 0x000B);

        ///<summary>(006A,000C) VR=UL VM=1 Number of Annotations</summary>
        public readonly static DicomTagUL NumberOfAnnotations = new DicomTagUL(0x006A, 0x000C);

        ///<summary>(006A,000D) VR=CS VM=1 Annotation Applies to All Optical Paths</summary>
        public readonly static DicomTagCS AnnotationAppliesToAllOpticalPaths = new DicomTagCS(0x006A, 0x000D);

        ///<summary>(006A,000E) VR=SH VM=1-n Referenced Optical Path Identifier</summary>
        public readonly static DicomTagSHs ReferencedOpticalPathIdentifier = new DicomTagSHs(0x006A, 0x000E);

        ///<summary>(006A,000F) VR=CS VM=1 Annotation Applies to All Z Planes</summary>
        public readonly static DicomTagCS AnnotationAppliesToAllZPlanes = new DicomTagCS(0x006A, 0x000F);

        ///<summary>(006A,0010) VR=FD VM=1-n Common Z Coordinate Value</summary>
        public readonly static DicomTagFDs CommonZCoordinateValue = new DicomTagFDs(0x006A, 0x0010);

        ///<summary>(006A,0011) VR=OL VM=1 Annotation Index List</summary>
        public readonly static DicomTagOL AnnotationIndexList = new DicomTagOL(0x006A, 0x0011);

        ///<summary>(0070,0001) VR=SQ VM=1 Graphic Annotation Sequence</summary>
        public readonly static DicomTagSQ GraphicAnnotationSequence = new DicomTagSQ(0x0070, 0x0001);

        ///<summary>(0070,0002) VR=CS VM=1 Graphic Layer</summary>
        public readonly static DicomTagCS GraphicLayer = new DicomTagCS(0x0070, 0x0002);

        ///<summary>(0070,0003) VR=CS VM=1 Bounding Box Annotation Units</summary>
        public readonly static DicomTagCS BoundingBoxAnnotationUnits = new DicomTagCS(0x0070, 0x0003);

        ///<summary>(0070,0004) VR=CS VM=1 Anchor Point Annotation Units</summary>
        public readonly static DicomTagCS AnchorPointAnnotationUnits = new DicomTagCS(0x0070, 0x0004);

        ///<summary>(0070,0005) VR=CS VM=1 Graphic Annotation Units</summary>
        public readonly static DicomTagCS GraphicAnnotationUnits = new DicomTagCS(0x0070, 0x0005);

        ///<summary>(0070,0006) VR=ST VM=1 Unformatted Text Value</summary>
        public readonly static DicomTagST UnformattedTextValue = new DicomTagST(0x0070, 0x0006);

        ///<summary>(0070,0008) VR=SQ VM=1 Text Object Sequence</summary>
        public readonly static DicomTagSQ TextObjectSequence = new DicomTagSQ(0x0070, 0x0008);

        ///<summary>(0070,0009) VR=SQ VM=1 Graphic Object Sequence</summary>
        public readonly static DicomTagSQ GraphicObjectSequence = new DicomTagSQ(0x0070, 0x0009);

        ///<summary>(0070,0010) VR=FL VM=2 Bounding Box Top Left Hand Corner</summary>
        public readonly static DicomTagFLs BoundingBoxTopLeftHandCorner = new DicomTagFLs(0x0070, 0x0010);

        ///<summary>(0070,0011) VR=FL VM=2 Bounding Box Bottom Right Hand Corner</summary>
        public readonly static DicomTagFLs BoundingBoxBottomRightHandCorner = new DicomTagFLs(0x0070, 0x0011);

        ///<summary>(0070,0012) VR=CS VM=1 Bounding Box Text Horizontal Justification</summary>
        public readonly static DicomTagCS BoundingBoxTextHorizontalJustification = new DicomTagCS(0x0070, 0x0012);

        ///<summary>(0070,0014) VR=FL VM=2 Anchor Point</summary>
        public readonly static DicomTagFLs AnchorPoint = new DicomTagFLs(0x0070, 0x0014);

        ///<summary>(0070,0015) VR=CS VM=1 Anchor Point Visibility</summary>
        public readonly static DicomTagCS AnchorPointVisibility = new DicomTagCS(0x0070, 0x0015);

        ///<summary>(0070,0020) VR=US VM=1 Graphic Dimensions</summary>
        public readonly static DicomTagUS GraphicDimensions = new DicomTagUS(0x0070, 0x0020);

        ///<summary>(0070,0021) VR=US VM=1 Number of Graphic Points</summary>
        public readonly static DicomTagUS NumberOfGraphicPoints = new DicomTagUS(0x0070, 0x0021);

        ///<summary>(0070,0022) VR=FL VM=2-n Graphic Data</summary>
        public readonly static DicomTagFLs GraphicData = new DicomTagFLs(0x0070, 0x0022);

        ///<summary>(0070,0023) VR=CS VM=1 Graphic Type</summary>
        public readonly static DicomTagCS GraphicType = new DicomTagCS(0x0070, 0x0023);

        ///<summary>(0070,0024) VR=CS VM=1 Graphic Filled</summary>
        public readonly static DicomTagCS GraphicFilled = new DicomTagCS(0x0070, 0x0024);

        ///<summary>(0070,0040) VR=IS VM=1 Image Rotation (Retired) (RETIRED)</summary>
        public readonly static DicomTagIS ImageRotationRetiredRETIRED = new DicomTagIS(0x0070, 0x0040);

        ///<summary>(0070,0041) VR=CS VM=1 Image Horizontal Flip</summary>
        public readonly static DicomTagCS ImageHorizontalFlip = new DicomTagCS(0x0070, 0x0041);

        ///<summary>(0070,0042) VR=US VM=1 Image Rotation</summary>
        public readonly static DicomTagUS ImageRotation = new DicomTagUS(0x0070, 0x0042);

        ///<summary>(0070,0050) VR=US VM=2 Displayed Area Top Left Hand Corner (Trial) (RETIRED)</summary>
        public readonly static DicomTagUSs DisplayedAreaTopLeftHandCornerTrialRETIRED = new DicomTagUSs(0x0070, 0x0050);

        ///<summary>(0070,0051) VR=US VM=2 Displayed Area Bottom Right Hand Corner (Trial) (RETIRED)</summary>
        public readonly static DicomTagUSs DisplayedAreaBottomRightHandCornerTrialRETIRED = new DicomTagUSs(0x0070, 0x0051);

        ///<summary>(0070,0052) VR=SL VM=2 Displayed Area Top Left Hand Corner</summary>
        public readonly static DicomTagSLs DisplayedAreaTopLeftHandCorner = new DicomTagSLs(0x0070, 0x0052);

        ///<summary>(0070,0053) VR=SL VM=2 Displayed Area Bottom Right Hand Corner</summary>
        public readonly static DicomTagSLs DisplayedAreaBottomRightHandCorner = new DicomTagSLs(0x0070, 0x0053);

        ///<summary>(0070,005A) VR=SQ VM=1 Displayed Area Selection Sequence</summary>
        public readonly static DicomTagSQ DisplayedAreaSelectionSequence = new DicomTagSQ(0x0070, 0x005A);

        ///<summary>(0070,0060) VR=SQ VM=1 Graphic Layer Sequence</summary>
        public readonly static DicomTagSQ GraphicLayerSequence = new DicomTagSQ(0x0070, 0x0060);

        ///<summary>(0070,0062) VR=IS VM=1 Graphic Layer Order</summary>
        public readonly static DicomTagIS GraphicLayerOrder = new DicomTagIS(0x0070, 0x0062);

        ///<summary>(0070,0066) VR=US VM=1 Graphic Layer Recommended Display Grayscale Value</summary>
        public readonly static DicomTagUS GraphicLayerRecommendedDisplayGrayscaleValue = new DicomTagUS(0x0070, 0x0066);

        ///<summary>(0070,0067) VR=US VM=3 Graphic Layer Recommended Display RGB Value (RETIRED)</summary>
        public readonly static DicomTagUSs GraphicLayerRecommendedDisplayRGBValueRETIRED = new DicomTagUSs(0x0070, 0x0067);

        ///<summary>(0070,0068) VR=LO VM=1 Graphic Layer Description</summary>
        public readonly static DicomTagLO GraphicLayerDescription = new DicomTagLO(0x0070, 0x0068);

        ///<summary>(0070,0080) VR=CS VM=1 Content Label</summary>
        public readonly static DicomTagCS ContentLabel = new DicomTagCS(0x0070, 0x0080);

        ///<summary>(0070,0081) VR=LO VM=1 Content Description</summary>
        public readonly static DicomTagLO ContentDescription = new DicomTagLO(0x0070, 0x0081);

        ///<summary>(0070,0082) VR=DA VM=1 Presentation Creation Date</summary>
        public readonly static DicomTagDA PresentationCreationDate = new DicomTagDA(0x0070, 0x0082);

        ///<summary>(0070,0083) VR=TM VM=1 Presentation Creation Time</summary>
        public readonly static DicomTagTM PresentationCreationTime = new DicomTagTM(0x0070, 0x0083);

        ///<summary>(0070,0084) VR=PN VM=1 Content Creator's Name</summary>
        public readonly static DicomTagPN ContentCreatorName = new DicomTagPN(0x0070, 0x0084);

        ///<summary>(0070,0086) VR=SQ VM=1 Content Creator's Identification Code Sequence</summary>
        public readonly static DicomTagSQ ContentCreatorIdentificationCodeSequence = new DicomTagSQ(0x0070, 0x0086);

        ///<summary>(0070,0087) VR=SQ VM=1 Alternate Content Description Sequence</summary>
        public readonly static DicomTagSQ AlternateContentDescriptionSequence = new DicomTagSQ(0x0070, 0x0087);

        ///<summary>(0070,0100) VR=CS VM=1 Presentation Size Mode</summary>
        public readonly static DicomTagCS PresentationSizeMode = new DicomTagCS(0x0070, 0x0100);

        ///<summary>(0070,0101) VR=DS VM=2 Presentation Pixel Spacing</summary>
        public readonly static DicomTagDSs PresentationPixelSpacing = new DicomTagDSs(0x0070, 0x0101);

        ///<summary>(0070,0102) VR=IS VM=2 Presentation Pixel Aspect Ratio</summary>
        public readonly static DicomTagISs PresentationPixelAspectRatio = new DicomTagISs(0x0070, 0x0102);

        ///<summary>(0070,0103) VR=FL VM=1 Presentation Pixel Magnification Ratio</summary>
        public readonly static DicomTagFL PresentationPixelMagnificationRatio = new DicomTagFL(0x0070, 0x0103);

        ///<summary>(0070,0207) VR=LO VM=1 Graphic Group Label</summary>
        public readonly static DicomTagLO GraphicGroupLabel = new DicomTagLO(0x0070, 0x0207);

        ///<summary>(0070,0208) VR=ST VM=1 Graphic Group Description</summary>
        public readonly static DicomTagST GraphicGroupDescription = new DicomTagST(0x0070, 0x0208);

        ///<summary>(0070,0209) VR=SQ VM=1 Compound Graphic Sequence</summary>
        public readonly static DicomTagSQ CompoundGraphicSequence = new DicomTagSQ(0x0070, 0x0209);

        ///<summary>(0070,0226) VR=UL VM=1 Compound Graphic Instance ID</summary>
        public readonly static DicomTagUL CompoundGraphicInstanceID = new DicomTagUL(0x0070, 0x0226);

        ///<summary>(0070,0227) VR=LO VM=1 Font Name</summary>
        public readonly static DicomTagLO FontName = new DicomTagLO(0x0070, 0x0227);

        ///<summary>(0070,0228) VR=CS VM=1 Font Name Type</summary>
        public readonly static DicomTagCS FontNameType = new DicomTagCS(0x0070, 0x0228);

        ///<summary>(0070,0229) VR=LO VM=1 CSS Font Name</summary>
        public readonly static DicomTagLO CSSFontName = new DicomTagLO(0x0070, 0x0229);

        ///<summary>(0070,0230) VR=FD VM=1 Rotation Angle</summary>
        public readonly static DicomTagFD RotationAngle = new DicomTagFD(0x0070, 0x0230);

        ///<summary>(0070,0231) VR=SQ VM=1 Text Style Sequence</summary>
        public readonly static DicomTagSQ TextStyleSequence = new DicomTagSQ(0x0070, 0x0231);

        ///<summary>(0070,0232) VR=SQ VM=1 Line Style Sequence</summary>
        public readonly static DicomTagSQ LineStyleSequence = new DicomTagSQ(0x0070, 0x0232);

        ///<summary>(0070,0233) VR=SQ VM=1 Fill Style Sequence</summary>
        public readonly static DicomTagSQ FillStyleSequence = new DicomTagSQ(0x0070, 0x0233);

        ///<summary>(0070,0234) VR=SQ VM=1 Graphic Group Sequence</summary>
        public readonly static DicomTagSQ GraphicGroupSequence = new DicomTagSQ(0x0070, 0x0234);

        ///<summary>(0070,0241) VR=US VM=3 Text Color CIELab Value</summary>
        public readonly static DicomTagUSs TextColorCIELabValue = new DicomTagUSs(0x0070, 0x0241);

        ///<summary>(0070,0242) VR=CS VM=1 Horizontal Alignment</summary>
        public readonly static DicomTagCS HorizontalAlignment = new DicomTagCS(0x0070, 0x0242);

        ///<summary>(0070,0243) VR=CS VM=1 Vertical Alignment</summary>
        public readonly static DicomTagCS VerticalAlignment = new DicomTagCS(0x0070, 0x0243);

        ///<summary>(0070,0244) VR=CS VM=1 Shadow Style</summary>
        public readonly static DicomTagCS ShadowStyle = new DicomTagCS(0x0070, 0x0244);

        ///<summary>(0070,0245) VR=FL VM=1 Shadow Offset X</summary>
        public readonly static DicomTagFL ShadowOffsetX = new DicomTagFL(0x0070, 0x0245);

        ///<summary>(0070,0246) VR=FL VM=1 Shadow Offset Y</summary>
        public readonly static DicomTagFL ShadowOffsetY = new DicomTagFL(0x0070, 0x0246);

        ///<summary>(0070,0247) VR=US VM=3 Shadow Color CIELab Value</summary>
        public readonly static DicomTagUSs ShadowColorCIELabValue = new DicomTagUSs(0x0070, 0x0247);

        ///<summary>(0070,0248) VR=CS VM=1 Underlined</summary>
        public readonly static DicomTagCS Underlined = new DicomTagCS(0x0070, 0x0248);

        ///<summary>(0070,0249) VR=CS VM=1 Bold</summary>
        public readonly static DicomTagCS Bold = new DicomTagCS(0x0070, 0x0249);

        ///<summary>(0070,0250) VR=CS VM=1 Italic</summary>
        public readonly static DicomTagCS Italic = new DicomTagCS(0x0070, 0x0250);

        ///<summary>(0070,0251) VR=US VM=3 Pattern On Color CIELab Value</summary>
        public readonly static DicomTagUSs PatternOnColorCIELabValue = new DicomTagUSs(0x0070, 0x0251);

        ///<summary>(0070,0252) VR=US VM=3 Pattern Off Color CIELab Value</summary>
        public readonly static DicomTagUSs PatternOffColorCIELabValue = new DicomTagUSs(0x0070, 0x0252);

        ///<summary>(0070,0253) VR=FL VM=1 Line Thickness</summary>
        public readonly static DicomTagFL LineThickness = new DicomTagFL(0x0070, 0x0253);

        ///<summary>(0070,0254) VR=CS VM=1 Line Dashing Style</summary>
        public readonly static DicomTagCS LineDashingStyle = new DicomTagCS(0x0070, 0x0254);

        ///<summary>(0070,0255) VR=UL VM=1 Line Pattern</summary>
        public readonly static DicomTagUL LinePattern = new DicomTagUL(0x0070, 0x0255);

        ///<summary>(0070,0256) VR=OB VM=1 Fill Pattern</summary>
        public readonly static DicomTagOB FillPattern = new DicomTagOB(0x0070, 0x0256);

        ///<summary>(0070,0257) VR=CS VM=1 Fill Mode</summary>
        public readonly static DicomTagCS FillMode = new DicomTagCS(0x0070, 0x0257);

        ///<summary>(0070,0258) VR=FL VM=1 Shadow Opacity</summary>
        public readonly static DicomTagFL ShadowOpacity = new DicomTagFL(0x0070, 0x0258);

        ///<summary>(0070,0261) VR=FL VM=1 Gap Length</summary>
        public readonly static DicomTagFL GapLength = new DicomTagFL(0x0070, 0x0261);

        ///<summary>(0070,0262) VR=FL VM=1 Diameter of Visibility</summary>
        public readonly static DicomTagFL DiameterOfVisibility = new DicomTagFL(0x0070, 0x0262);

        ///<summary>(0070,0273) VR=FL VM=2 Rotation Point</summary>
        public readonly static DicomTagFLs RotationPoint = new DicomTagFLs(0x0070, 0x0273);

        ///<summary>(0070,0274) VR=CS VM=1 Tick Alignment</summary>
        public readonly static DicomTagCS TickAlignment = new DicomTagCS(0x0070, 0x0274);

        ///<summary>(0070,0278) VR=CS VM=1 Show Tick Label</summary>
        public readonly static DicomTagCS ShowTickLabel = new DicomTagCS(0x0070, 0x0278);

        ///<summary>(0070,0279) VR=CS VM=1 Tick Label Alignment</summary>
        public readonly static DicomTagCS TickLabelAlignment = new DicomTagCS(0x0070, 0x0279);

        ///<summary>(0070,0282) VR=CS VM=1 Compound Graphic Units</summary>
        public readonly static DicomTagCS CompoundGraphicUnits = new DicomTagCS(0x0070, 0x0282);

        ///<summary>(0070,0284) VR=FL VM=1 Pattern On Opacity</summary>
        public readonly static DicomTagFL PatternOnOpacity = new DicomTagFL(0x0070, 0x0284);

        ///<summary>(0070,0285) VR=FL VM=1 Pattern Off Opacity</summary>
        public readonly static DicomTagFL PatternOffOpacity = new DicomTagFL(0x0070, 0x0285);

        ///<summary>(0070,0287) VR=SQ VM=1 Major Ticks Sequence</summary>
        public readonly static DicomTagSQ MajorTicksSequence = new DicomTagSQ(0x0070, 0x0287);

        ///<summary>(0070,0288) VR=FL VM=1 Tick Position</summary>
        public readonly static DicomTagFL TickPosition = new DicomTagFL(0x0070, 0x0288);

        ///<summary>(0070,0289) VR=SH VM=1 Tick Label</summary>
        public readonly static DicomTagSH TickLabel = new DicomTagSH(0x0070, 0x0289);

        ///<summary>(0070,0294) VR=CS VM=1 Compound Graphic Type</summary>
        public readonly static DicomTagCS CompoundGraphicType = new DicomTagCS(0x0070, 0x0294);

        ///<summary>(0070,0295) VR=UL VM=1 Graphic Group ID</summary>
        public readonly static DicomTagUL GraphicGroupID = new DicomTagUL(0x0070, 0x0295);

        ///<summary>(0070,0306) VR=CS VM=1 Shape Type</summary>
        public readonly static DicomTagCS ShapeType = new DicomTagCS(0x0070, 0x0306);

        ///<summary>(0070,0308) VR=SQ VM=1 Registration Sequence</summary>
        public readonly static DicomTagSQ RegistrationSequence = new DicomTagSQ(0x0070, 0x0308);

        ///<summary>(0070,0309) VR=SQ VM=1 Matrix Registration Sequence</summary>
        public readonly static DicomTagSQ MatrixRegistrationSequence = new DicomTagSQ(0x0070, 0x0309);

        ///<summary>(0070,030A) VR=SQ VM=1 Matrix Sequence</summary>
        public readonly static DicomTagSQ MatrixSequence = new DicomTagSQ(0x0070, 0x030A);

        ///<summary>(0070,030B) VR=FD VM=16 Frame of Reference to Displayed Coordinate System Transformation Matrix</summary>
        public readonly static DicomTagFDs FrameOfReferenceToDisplayedCoordinateSystemTransformationMatrix = new DicomTagFDs(0x0070, 0x030B);

        ///<summary>(0070,030C) VR=CS VM=1 Frame of Reference Transformation Matrix Type</summary>
        public readonly static DicomTagCS FrameOfReferenceTransformationMatrixType = new DicomTagCS(0x0070, 0x030C);

        ///<summary>(0070,030D) VR=SQ VM=1 Registration Type Code Sequence</summary>
        public readonly static DicomTagSQ RegistrationTypeCodeSequence = new DicomTagSQ(0x0070, 0x030D);

        ///<summary>(0070,030F) VR=ST VM=1 Fiducial Description</summary>
        public readonly static DicomTagST FiducialDescription = new DicomTagST(0x0070, 0x030F);

        ///<summary>(0070,0310) VR=SH VM=1 Fiducial Identifier</summary>
        public readonly static DicomTagSH FiducialIdentifier = new DicomTagSH(0x0070, 0x0310);

        ///<summary>(0070,0311) VR=SQ VM=1 Fiducial Identifier Code Sequence</summary>
        public readonly static DicomTagSQ FiducialIdentifierCodeSequence = new DicomTagSQ(0x0070, 0x0311);

        ///<summary>(0070,0312) VR=FD VM=1 Contour Uncertainty Radius</summary>
        public readonly static DicomTagFD ContourUncertaintyRadius = new DicomTagFD(0x0070, 0x0312);

        ///<summary>(0070,0314) VR=SQ VM=1 Used Fiducials Sequence</summary>
        public readonly static DicomTagSQ UsedFiducialsSequence = new DicomTagSQ(0x0070, 0x0314);

        ///<summary>(0070,0315) VR=SQ VM=1 Used RT Structure Set ROI Sequence</summary>
        public readonly static DicomTagSQ UsedRTStructureSetROISequence = new DicomTagSQ(0x0070, 0x0315);

        ///<summary>(0070,0318) VR=SQ VM=1 Graphic Coordinates Data Sequence</summary>
        public readonly static DicomTagSQ GraphicCoordinatesDataSequence = new DicomTagSQ(0x0070, 0x0318);

        ///<summary>(0070,031A) VR=UI VM=1 Fiducial UID</summary>
        public readonly static DicomTagUI FiducialUID = new DicomTagUI(0x0070, 0x031A);

        ///<summary>(0070,031B) VR=UI VM=1 Referenced Fiducial UID</summary>
        public readonly static DicomTagUI ReferencedFiducialUID = new DicomTagUI(0x0070, 0x031B);

        ///<summary>(0070,031C) VR=SQ VM=1 Fiducial Set Sequence</summary>
        public readonly static DicomTagSQ FiducialSetSequence = new DicomTagSQ(0x0070, 0x031C);

        ///<summary>(0070,031E) VR=SQ VM=1 Fiducial Sequence</summary>
        public readonly static DicomTagSQ FiducialSequence = new DicomTagSQ(0x0070, 0x031E);

        ///<summary>(0070,031F) VR=SQ VM=1 Fiducials Property Category Code Sequence</summary>
        public readonly static DicomTagSQ FiducialsPropertyCategoryCodeSequence = new DicomTagSQ(0x0070, 0x031F);

        ///<summary>(0070,0401) VR=US VM=3 Graphic Layer Recommended Display CIELab Value</summary>
        public readonly static DicomTagUSs GraphicLayerRecommendedDisplayCIELabValue = new DicomTagUSs(0x0070, 0x0401);

        ///<summary>(0070,0402) VR=SQ VM=1 Blending Sequence</summary>
        public readonly static DicomTagSQ BlendingSequence = new DicomTagSQ(0x0070, 0x0402);

        ///<summary>(0070,0403) VR=FL VM=1 Relative Opacity</summary>
        public readonly static DicomTagFL RelativeOpacity = new DicomTagFL(0x0070, 0x0403);

        ///<summary>(0070,0404) VR=SQ VM=1 Referenced Spatial Registration Sequence</summary>
        public readonly static DicomTagSQ ReferencedSpatialRegistrationSequence = new DicomTagSQ(0x0070, 0x0404);

        ///<summary>(0070,0405) VR=CS VM=1 Blending Position</summary>
        public readonly static DicomTagCS BlendingPosition = new DicomTagCS(0x0070, 0x0405);

        ///<summary>(0070,1101) VR=UI VM=1 Presentation Display Collection UID</summary>
        public readonly static DicomTagUI PresentationDisplayCollectionUID = new DicomTagUI(0x0070, 0x1101);

        ///<summary>(0070,1102) VR=UI VM=1 Presentation Sequence Collection UID</summary>
        public readonly static DicomTagUI PresentationSequenceCollectionUID = new DicomTagUI(0x0070, 0x1102);

        ///<summary>(0070,1103) VR=US VM=1 Presentation Sequence Position Index</summary>
        public readonly static DicomTagUS PresentationSequencePositionIndex = new DicomTagUS(0x0070, 0x1103);

        ///<summary>(0070,1104) VR=SQ VM=1 Rendered Image Reference Sequence</summary>
        public readonly static DicomTagSQ RenderedImageReferenceSequence = new DicomTagSQ(0x0070, 0x1104);

        ///<summary>(0070,1201) VR=SQ VM=1 Volumetric Presentation State Input Sequence</summary>
        public readonly static DicomTagSQ VolumetricPresentationStateInputSequence = new DicomTagSQ(0x0070, 0x1201);

        ///<summary>(0070,1202) VR=CS VM=1 Presentation Input Type</summary>
        public readonly static DicomTagCS PresentationInputType = new DicomTagCS(0x0070, 0x1202);

        ///<summary>(0070,1203) VR=US VM=1 Input Sequence Position Index</summary>
        public readonly static DicomTagUS InputSequencePositionIndex = new DicomTagUS(0x0070, 0x1203);

        ///<summary>(0070,1204) VR=CS VM=1 Crop</summary>
        public readonly static DicomTagCS Crop = new DicomTagCS(0x0070, 0x1204);

        ///<summary>(0070,1205) VR=US VM=1-n Cropping Specification Index</summary>
        public readonly static DicomTagUSs CroppingSpecificationIndex = new DicomTagUSs(0x0070, 0x1205);

        ///<summary>(0070,1206) VR=CS VM=1 Compositing Method (RETIRED)</summary>
        public readonly static DicomTagCS CompositingMethodRETIRED = new DicomTagCS(0x0070, 0x1206);

        ///<summary>(0070,1207) VR=US VM=1 Volumetric Presentation Input Number</summary>
        public readonly static DicomTagUS VolumetricPresentationInputNumber = new DicomTagUS(0x0070, 0x1207);

        ///<summary>(0070,1208) VR=CS VM=1 Image Volume Geometry</summary>
        public readonly static DicomTagCS ImageVolumeGeometry = new DicomTagCS(0x0070, 0x1208);

        ///<summary>(0070,1209) VR=UI VM=1 Volumetric Presentation Input Set UID</summary>
        public readonly static DicomTagUI VolumetricPresentationInputSetUID = new DicomTagUI(0x0070, 0x1209);

        ///<summary>(0070,120A) VR=SQ VM=1 Volumetric Presentation Input Set Sequence</summary>
        public readonly static DicomTagSQ VolumetricPresentationInputSetSequence = new DicomTagSQ(0x0070, 0x120A);

        ///<summary>(0070,120B) VR=CS VM=1 Global Crop</summary>
        public readonly static DicomTagCS GlobalCrop = new DicomTagCS(0x0070, 0x120B);

        ///<summary>(0070,120C) VR=US VM=1-n Global Cropping Specification Index</summary>
        public readonly static DicomTagUSs GlobalCroppingSpecificationIndex = new DicomTagUSs(0x0070, 0x120C);

        ///<summary>(0070,120D) VR=CS VM=1 Rendering Method</summary>
        public readonly static DicomTagCS RenderingMethod = new DicomTagCS(0x0070, 0x120D);

        ///<summary>(0070,1301) VR=SQ VM=1 Volume Cropping Sequence</summary>
        public readonly static DicomTagSQ VolumeCroppingSequence = new DicomTagSQ(0x0070, 0x1301);

        ///<summary>(0070,1302) VR=CS VM=1 Volume Cropping Method</summary>
        public readonly static DicomTagCS VolumeCroppingMethod = new DicomTagCS(0x0070, 0x1302);

        ///<summary>(0070,1303) VR=FD VM=6 Bounding Box Crop</summary>
        public readonly static DicomTagFDs BoundingBoxCrop = new DicomTagFDs(0x0070, 0x1303);

        ///<summary>(0070,1304) VR=SQ VM=1 Oblique Cropping Plane Sequence</summary>
        public readonly static DicomTagSQ ObliqueCroppingPlaneSequence = new DicomTagSQ(0x0070, 0x1304);

        ///<summary>(0070,1305) VR=FD VM=4 Plane</summary>
        public readonly static DicomTagFDs Plane = new DicomTagFDs(0x0070, 0x1305);

        ///<summary>(0070,1306) VR=FD VM=3 Plane Normal</summary>
        public readonly static DicomTagFDs PlaneNormal = new DicomTagFDs(0x0070, 0x1306);

        ///<summary>(0070,1309) VR=US VM=1 Cropping Specification Number</summary>
        public readonly static DicomTagUS CroppingSpecificationNumber = new DicomTagUS(0x0070, 0x1309);

        ///<summary>(0070,1501) VR=CS VM=1 Multi-Planar Reconstruction Style</summary>
        public readonly static DicomTagCS MultiPlanarReconstructionStyle = new DicomTagCS(0x0070, 0x1501);

        ///<summary>(0070,1502) VR=CS VM=1 MPR Thickness Type</summary>
        public readonly static DicomTagCS MPRThicknessType = new DicomTagCS(0x0070, 0x1502);

        ///<summary>(0070,1503) VR=FD VM=1 MPR Slab Thickness</summary>
        public readonly static DicomTagFD MPRSlabThickness = new DicomTagFD(0x0070, 0x1503);

        ///<summary>(0070,1505) VR=FD VM=3 MPR Top Left Hand Corner</summary>
        public readonly static DicomTagFDs MPRTopLeftHandCorner = new DicomTagFDs(0x0070, 0x1505);

        ///<summary>(0070,1507) VR=FD VM=3 MPR View Width Direction</summary>
        public readonly static DicomTagFDs MPRViewWidthDirection = new DicomTagFDs(0x0070, 0x1507);

        ///<summary>(0070,1508) VR=FD VM=1 MPR View Width</summary>
        public readonly static DicomTagFD MPRViewWidth = new DicomTagFD(0x0070, 0x1508);

        ///<summary>(0070,150C) VR=UL VM=1 Number of Volumetric Curve Points</summary>
        public readonly static DicomTagUL NumberOfVolumetricCurvePoints = new DicomTagUL(0x0070, 0x150C);

        ///<summary>(0070,150D) VR=OD VM=1 Volumetric Curve Points</summary>
        public readonly static DicomTagOD VolumetricCurvePoints = new DicomTagOD(0x0070, 0x150D);

        ///<summary>(0070,1511) VR=FD VM=3 MPR View Height Direction</summary>
        public readonly static DicomTagFDs MPRViewHeightDirection = new DicomTagFDs(0x0070, 0x1511);

        ///<summary>(0070,1512) VR=FD VM=1 MPR View Height</summary>
        public readonly static DicomTagFD MPRViewHeight = new DicomTagFD(0x0070, 0x1512);

        ///<summary>(0070,1602) VR=CS VM=1 Render Projection</summary>
        public readonly static DicomTagCS RenderProjection = new DicomTagCS(0x0070, 0x1602);

        ///<summary>(0070,1603) VR=FD VM=3 Viewpoint Position</summary>
        public readonly static DicomTagFDs ViewpointPosition = new DicomTagFDs(0x0070, 0x1603);

        ///<summary>(0070,1604) VR=FD VM=3 Viewpoint LookAt Point</summary>
        public readonly static DicomTagFDs ViewpointLookAtPoint = new DicomTagFDs(0x0070, 0x1604);

        ///<summary>(0070,1605) VR=FD VM=3 Viewpoint Up Direction</summary>
        public readonly static DicomTagFDs ViewpointUpDirection = new DicomTagFDs(0x0070, 0x1605);

        ///<summary>(0070,1606) VR=FD VM=6 Render Field of View</summary>
        public readonly static DicomTagFDs RenderFieldOfView = new DicomTagFDs(0x0070, 0x1606);

        ///<summary>(0070,1607) VR=FD VM=1 Sampling Step Size</summary>
        public readonly static DicomTagFD SamplingStepSize = new DicomTagFD(0x0070, 0x1607);

        ///<summary>(0070,1701) VR=CS VM=1 Shading Style</summary>
        public readonly static DicomTagCS ShadingStyle = new DicomTagCS(0x0070, 0x1701);

        ///<summary>(0070,1702) VR=FD VM=1 Ambient Reflection Intensity</summary>
        public readonly static DicomTagFD AmbientReflectionIntensity = new DicomTagFD(0x0070, 0x1702);

        ///<summary>(0070,1703) VR=FD VM=3 Light Direction</summary>
        public readonly static DicomTagFDs LightDirection = new DicomTagFDs(0x0070, 0x1703);

        ///<summary>(0070,1704) VR=FD VM=1 Diffuse Reflection Intensity</summary>
        public readonly static DicomTagFD DiffuseReflectionIntensity = new DicomTagFD(0x0070, 0x1704);

        ///<summary>(0070,1705) VR=FD VM=1 Specular Reflection Intensity</summary>
        public readonly static DicomTagFD SpecularReflectionIntensity = new DicomTagFD(0x0070, 0x1705);

        ///<summary>(0070,1706) VR=FD VM=1 Shininess</summary>
        public readonly static DicomTagFD Shininess = new DicomTagFD(0x0070, 0x1706);

        ///<summary>(0070,1801) VR=SQ VM=1 Presentation State Classification Component Sequence</summary>
        public readonly static DicomTagSQ PresentationStateClassificationComponentSequence = new DicomTagSQ(0x0070, 0x1801);

        ///<summary>(0070,1802) VR=CS VM=1 Component Type</summary>
        public readonly static DicomTagCS ComponentType = new DicomTagCS(0x0070, 0x1802);

        ///<summary>(0070,1803) VR=SQ VM=1 Component Input Sequence</summary>
        public readonly static DicomTagSQ ComponentInputSequence = new DicomTagSQ(0x0070, 0x1803);

        ///<summary>(0070,1804) VR=US VM=1 Volumetric Presentation Input Index</summary>
        public readonly static DicomTagUS VolumetricPresentationInputIndex = new DicomTagUS(0x0070, 0x1804);

        ///<summary>(0070,1805) VR=SQ VM=1 Presentation State Compositor Component Sequence</summary>
        public readonly static DicomTagSQ PresentationStateCompositorComponentSequence = new DicomTagSQ(0x0070, 0x1805);

        ///<summary>(0070,1806) VR=SQ VM=1 Weighting Transfer Function Sequence</summary>
        public readonly static DicomTagSQ WeightingTransferFunctionSequence = new DicomTagSQ(0x0070, 0x1806);

        ///<summary>(0070,1807) VR=US VM=3 Weighting Lookup Table Descriptor (RETIRED)</summary>
        public readonly static DicomTagUSs WeightingLookupTableDescriptorRETIRED = new DicomTagUSs(0x0070, 0x1807);

        ///<summary>(0070,1808) VR=OB VM=1 Weighting Lookup Table Data (RETIRED)</summary>
        public readonly static DicomTagOB WeightingLookupTableDataRETIRED = new DicomTagOB(0x0070, 0x1808);

        ///<summary>(0070,1901) VR=SQ VM=1 Volumetric Annotation Sequence</summary>
        public readonly static DicomTagSQ VolumetricAnnotationSequence = new DicomTagSQ(0x0070, 0x1901);

        ///<summary>(0070,1903) VR=SQ VM=1 Referenced Structured Context Sequence</summary>
        public readonly static DicomTagSQ ReferencedStructuredContextSequence = new DicomTagSQ(0x0070, 0x1903);

        ///<summary>(0070,1904) VR=UI VM=1 Referenced Content Item</summary>
        public readonly static DicomTagUI ReferencedContentItem = new DicomTagUI(0x0070, 0x1904);

        ///<summary>(0070,1905) VR=SQ VM=1 Volumetric Presentation Input Annotation Sequence</summary>
        public readonly static DicomTagSQ VolumetricPresentationInputAnnotationSequence = new DicomTagSQ(0x0070, 0x1905);

        ///<summary>(0070,1907) VR=CS VM=1 Annotation Clipping</summary>
        public readonly static DicomTagCS AnnotationClipping = new DicomTagCS(0x0070, 0x1907);

        ///<summary>(0070,1A01) VR=CS VM=1 Presentation Animation Style</summary>
        public readonly static DicomTagCS PresentationAnimationStyle = new DicomTagCS(0x0070, 0x1A01);

        ///<summary>(0070,1A03) VR=FD VM=1 Recommended Animation Rate</summary>
        public readonly static DicomTagFD RecommendedAnimationRate = new DicomTagFD(0x0070, 0x1A03);

        ///<summary>(0070,1A04) VR=SQ VM=1 Animation Curve Sequence</summary>
        public readonly static DicomTagSQ AnimationCurveSequence = new DicomTagSQ(0x0070, 0x1A04);

        ///<summary>(0070,1A05) VR=FD VM=1 Animation Step Size</summary>
        public readonly static DicomTagFD AnimationStepSize = new DicomTagFD(0x0070, 0x1A05);

        ///<summary>(0070,1A06) VR=FD VM=1 Swivel Range</summary>
        public readonly static DicomTagFD SwivelRange = new DicomTagFD(0x0070, 0x1A06);

        ///<summary>(0070,1A07) VR=OD VM=1 Volumetric Curve Up Directions</summary>
        public readonly static DicomTagOD VolumetricCurveUpDirections = new DicomTagOD(0x0070, 0x1A07);

        ///<summary>(0070,1A08) VR=SQ VM=1 Volume Stream Sequence</summary>
        public readonly static DicomTagSQ VolumeStreamSequence = new DicomTagSQ(0x0070, 0x1A08);

        ///<summary>(0070,1A09) VR=LO VM=1 RGBA Transfer Function Description</summary>
        public readonly static DicomTagLO RGBATransferFunctionDescription = new DicomTagLO(0x0070, 0x1A09);

        ///<summary>(0070,1B01) VR=SQ VM=1 Advanced Blending Sequence</summary>
        public readonly static DicomTagSQ AdvancedBlendingSequence = new DicomTagSQ(0x0070, 0x1B01);

        ///<summary>(0070,1B02) VR=US VM=1 Blending Input Number</summary>
        public readonly static DicomTagUS BlendingInputNumber = new DicomTagUS(0x0070, 0x1B02);

        ///<summary>(0070,1B03) VR=SQ VM=1 Blending Display Input Sequence</summary>
        public readonly static DicomTagSQ BlendingDisplayInputSequence = new DicomTagSQ(0x0070, 0x1B03);

        ///<summary>(0070,1B04) VR=SQ VM=1 Blending Display Sequence</summary>
        public readonly static DicomTagSQ BlendingDisplaySequence = new DicomTagSQ(0x0070, 0x1B04);

        ///<summary>(0070,1B06) VR=CS VM=1 Blending Mode</summary>
        public readonly static DicomTagCS BlendingMode = new DicomTagCS(0x0070, 0x1B06);

        ///<summary>(0070,1B07) VR=CS VM=1 Time Series Blending</summary>
        public readonly static DicomTagCS TimeSeriesBlending = new DicomTagCS(0x0070, 0x1B07);

        ///<summary>(0070,1B08) VR=CS VM=1 Geometry for Display</summary>
        public readonly static DicomTagCS GeometryForDisplay = new DicomTagCS(0x0070, 0x1B08);

        ///<summary>(0070,1B11) VR=SQ VM=1 Threshold Sequence</summary>
        public readonly static DicomTagSQ ThresholdSequence = new DicomTagSQ(0x0070, 0x1B11);

        ///<summary>(0070,1B12) VR=SQ VM=1 Threshold Value Sequence</summary>
        public readonly static DicomTagSQ ThresholdValueSequence = new DicomTagSQ(0x0070, 0x1B12);

        ///<summary>(0070,1B13) VR=CS VM=1 Threshold Type</summary>
        public readonly static DicomTagCS ThresholdType = new DicomTagCS(0x0070, 0x1B13);

        ///<summary>(0070,1B14) VR=FD VM=1 Threshold Value</summary>
        public readonly static DicomTagFD ThresholdValue = new DicomTagFD(0x0070, 0x1B14);

        ///<summary>(0072,0002) VR=SH VM=1 Hanging Protocol Name</summary>
        public readonly static DicomTagSH HangingProtocolName = new DicomTagSH(0x0072, 0x0002);

        ///<summary>(0072,0004) VR=LO VM=1 Hanging Protocol Description</summary>
        public readonly static DicomTagLO HangingProtocolDescription = new DicomTagLO(0x0072, 0x0004);

        ///<summary>(0072,0006) VR=CS VM=1 Hanging Protocol Level</summary>
        public readonly static DicomTagCS HangingProtocolLevel = new DicomTagCS(0x0072, 0x0006);

        ///<summary>(0072,0008) VR=LO VM=1 Hanging Protocol Creator</summary>
        public readonly static DicomTagLO HangingProtocolCreator = new DicomTagLO(0x0072, 0x0008);

        ///<summary>(0072,000A) VR=DT VM=1 Hanging Protocol Creation DateTime</summary>
        public readonly static DicomTagDT HangingProtocolCreationDateTime = new DicomTagDT(0x0072, 0x000A);

        ///<summary>(0072,000C) VR=SQ VM=1 Hanging Protocol Definition Sequence</summary>
        public readonly static DicomTagSQ HangingProtocolDefinitionSequence = new DicomTagSQ(0x0072, 0x000C);

        ///<summary>(0072,000E) VR=SQ VM=1 Hanging Protocol User Identification Code Sequence</summary>
        public readonly static DicomTagSQ HangingProtocolUserIdentificationCodeSequence = new DicomTagSQ(0x0072, 0x000E);

        ///<summary>(0072,0010) VR=LO VM=1 Hanging Protocol User Group Name</summary>
        public readonly static DicomTagLO HangingProtocolUserGroupName = new DicomTagLO(0x0072, 0x0010);

        ///<summary>(0072,0012) VR=SQ VM=1 Source Hanging Protocol Sequence</summary>
        public readonly static DicomTagSQ SourceHangingProtocolSequence = new DicomTagSQ(0x0072, 0x0012);

        ///<summary>(0072,0014) VR=US VM=1 Number of Priors Referenced</summary>
        public readonly static DicomTagUS NumberOfPriorsReferenced = new DicomTagUS(0x0072, 0x0014);

        ///<summary>(0072,0020) VR=SQ VM=1 Image Sets Sequence</summary>
        public readonly static DicomTagSQ ImageSetsSequence = new DicomTagSQ(0x0072, 0x0020);

        ///<summary>(0072,0022) VR=SQ VM=1 Image Set Selector Sequence</summary>
        public readonly static DicomTagSQ ImageSetSelectorSequence = new DicomTagSQ(0x0072, 0x0022);

        ///<summary>(0072,0024) VR=CS VM=1 Image Set Selector Usage Flag</summary>
        public readonly static DicomTagCS ImageSetSelectorUsageFlag = new DicomTagCS(0x0072, 0x0024);

        ///<summary>(0072,0026) VR=AT VM=1 Selector Attribute</summary>
        public readonly static DicomTagAT SelectorAttribute = new DicomTagAT(0x0072, 0x0026);

        ///<summary>(0072,0028) VR=US VM=1 Selector Value Number</summary>
        public readonly static DicomTagUS SelectorValueNumber = new DicomTagUS(0x0072, 0x0028);

        ///<summary>(0072,0030) VR=SQ VM=1 Time Based Image Sets Sequence</summary>
        public readonly static DicomTagSQ TimeBasedImageSetsSequence = new DicomTagSQ(0x0072, 0x0030);

        ///<summary>(0072,0032) VR=US VM=1 Image Set Number</summary>
        public readonly static DicomTagUS ImageSetNumber = new DicomTagUS(0x0072, 0x0032);

        ///<summary>(0072,0034) VR=CS VM=1 Image Set Selector Category</summary>
        public readonly static DicomTagCS ImageSetSelectorCategory = new DicomTagCS(0x0072, 0x0034);

        ///<summary>(0072,0038) VR=US VM=2 Relative Time</summary>
        public readonly static DicomTagUSs RelativeTime = new DicomTagUSs(0x0072, 0x0038);

        ///<summary>(0072,003A) VR=CS VM=1 Relative Time Units</summary>
        public readonly static DicomTagCS RelativeTimeUnits = new DicomTagCS(0x0072, 0x003A);

        ///<summary>(0072,003C) VR=SS VM=2 Abstract Prior Value</summary>
        public readonly static DicomTagSSs AbstractPriorValue = new DicomTagSSs(0x0072, 0x003C);

        ///<summary>(0072,003E) VR=SQ VM=1 Abstract Prior Code Sequence</summary>
        public readonly static DicomTagSQ AbstractPriorCodeSequence = new DicomTagSQ(0x0072, 0x003E);

        ///<summary>(0072,0040) VR=LO VM=1 Image Set Label</summary>
        public readonly static DicomTagLO ImageSetLabel = new DicomTagLO(0x0072, 0x0040);

        ///<summary>(0072,0050) VR=CS VM=1 Selector Attribute VR</summary>
        public readonly static DicomTagCS SelectorAttributeVR = new DicomTagCS(0x0072, 0x0050);

        ///<summary>(0072,0052) VR=AT VM=1-n Selector Sequence Pointer</summary>
        public readonly static DicomTagATs SelectorSequencePointer = new DicomTagATs(0x0072, 0x0052);

        ///<summary>(0072,0054) VR=LO VM=1-n Selector Sequence Pointer Private Creator</summary>
        public readonly static DicomTagLOs SelectorSequencePointerPrivateCreator = new DicomTagLOs(0x0072, 0x0054);

        ///<summary>(0072,0056) VR=LO VM=1 Selector Attribute Private Creator</summary>
        public readonly static DicomTagLO SelectorAttributePrivateCreator = new DicomTagLO(0x0072, 0x0056);

        ///<summary>(0072,005E) VR=AE VM=1-n Selector AE Value</summary>
        public readonly static DicomTagAEs SelectorAEValue = new DicomTagAEs(0x0072, 0x005E);

        ///<summary>(0072,005F) VR=AS VM=1-n Selector AS Value</summary>
        public readonly static DicomTagASs SelectorASValue = new DicomTagASs(0x0072, 0x005F);

        ///<summary>(0072,0060) VR=AT VM=1-n Selector AT Value</summary>
        public readonly static DicomTagATs SelectorATValue = new DicomTagATs(0x0072, 0x0060);

        ///<summary>(0072,0061) VR=DA VM=1-n Selector DA Value</summary>
        public readonly static DicomTagDAs SelectorDAValue = new DicomTagDAs(0x0072, 0x0061);

        ///<summary>(0072,0062) VR=CS VM=1-n Selector CS Value</summary>
        public readonly static DicomTagCSs SelectorCSValue = new DicomTagCSs(0x0072, 0x0062);

        ///<summary>(0072,0063) VR=DT VM=1-n Selector DT Value</summary>
        public readonly static DicomTagDTs SelectorDTValue = new DicomTagDTs(0x0072, 0x0063);

        ///<summary>(0072,0064) VR=IS VM=1-n Selector IS Value</summary>
        public readonly static DicomTagISs SelectorISValue = new DicomTagISs(0x0072, 0x0064);

        ///<summary>(0072,0065) VR=OB VM=1 Selector OB Value</summary>
        public readonly static DicomTagOB SelectorOBValue = new DicomTagOB(0x0072, 0x0065);

        ///<summary>(0072,0066) VR=LO VM=1-n Selector LO Value</summary>
        public readonly static DicomTagLOs SelectorLOValue = new DicomTagLOs(0x0072, 0x0066);

        ///<summary>(0072,0067) VR=OF VM=1 Selector OF Value</summary>
        public readonly static DicomTagOF SelectorOFValue = new DicomTagOF(0x0072, 0x0067);

        ///<summary>(0072,0068) VR=LT VM=1 Selector LT Value</summary>
        public readonly static DicomTagLT SelectorLTValue = new DicomTagLT(0x0072, 0x0068);

        ///<summary>(0072,0069) VR=OW VM=1 Selector OW Value</summary>
        public readonly static DicomTagOW SelectorOWValue = new DicomTagOW(0x0072, 0x0069);

        ///<summary>(0072,006A) VR=PN VM=1-n Selector PN Value</summary>
        public readonly static DicomTagPNs SelectorPNValue = new DicomTagPNs(0x0072, 0x006A);

        ///<summary>(0072,006B) VR=TM VM=1-n Selector TM Value</summary>
        public readonly static DicomTagTMs SelectorTMValue = new DicomTagTMs(0x0072, 0x006B);

        ///<summary>(0072,006C) VR=SH VM=1-n Selector SH Value</summary>
        public readonly static DicomTagSHs SelectorSHValue = new DicomTagSHs(0x0072, 0x006C);

        ///<summary>(0072,006D) VR=UN VM=1 Selector UN Value</summary>
        public readonly static DicomTagUN SelectorUNValue = new DicomTagUN(0x0072, 0x006D);

        ///<summary>(0072,006E) VR=ST VM=1 Selector ST Value</summary>
        public readonly static DicomTagST SelectorSTValue = new DicomTagST(0x0072, 0x006E);

        ///<summary>(0072,006F) VR=UC VM=1-n Selector UC Value</summary>
        public readonly static DicomTagUCs SelectorUCValue = new DicomTagUCs(0x0072, 0x006F);

        ///<summary>(0072,0070) VR=UT VM=1 Selector UT Value</summary>
        public readonly static DicomTagUT SelectorUTValue = new DicomTagUT(0x0072, 0x0070);

        ///<summary>(0072,0071) VR=UR VM=1 Selector UR Value</summary>
        public readonly static DicomTagUR SelectorURValue = new DicomTagUR(0x0072, 0x0071);

        ///<summary>(0072,0072) VR=DS VM=1-n Selector DS Value</summary>
        public readonly static DicomTagDSs SelectorDSValue = new DicomTagDSs(0x0072, 0x0072);

        ///<summary>(0072,0073) VR=OD VM=1 Selector OD Value</summary>
        public readonly static DicomTagOD SelectorODValue = new DicomTagOD(0x0072, 0x0073);

        ///<summary>(0072,0074) VR=FD VM=1-n Selector FD Value</summary>
        public readonly static DicomTagFDs SelectorFDValue = new DicomTagFDs(0x0072, 0x0074);

        ///<summary>(0072,0075) VR=OL VM=1 Selector OL Value</summary>
        public readonly static DicomTagOL SelectorOLValue = new DicomTagOL(0x0072, 0x0075);

        ///<summary>(0072,0076) VR=FL VM=1-n Selector FL Value</summary>
        public readonly static DicomTagFLs SelectorFLValue = new DicomTagFLs(0x0072, 0x0076);

        ///<summary>(0072,0078) VR=UL VM=1-n Selector UL Value</summary>
        public readonly static DicomTagULs SelectorULValue = new DicomTagULs(0x0072, 0x0078);

        ///<summary>(0072,007A) VR=US VM=1-n Selector US Value</summary>
        public readonly static DicomTagUSs SelectorUSValue = new DicomTagUSs(0x0072, 0x007A);

        ///<summary>(0072,007C) VR=SL VM=1-n Selector SL Value</summary>
        public readonly static DicomTagSLs SelectorSLValue = new DicomTagSLs(0x0072, 0x007C);

        ///<summary>(0072,007E) VR=SS VM=1-n Selector SS Value</summary>
        public readonly static DicomTagSSs SelectorSSValue = new DicomTagSSs(0x0072, 0x007E);

        ///<summary>(0072,007F) VR=UI VM=1-n Selector UI Value</summary>
        public readonly static DicomTagUIs SelectorUIValue = new DicomTagUIs(0x0072, 0x007F);

        ///<summary>(0072,0080) VR=SQ VM=1 Selector Code Sequence Value</summary>
        public readonly static DicomTagSQ SelectorCodeSequenceValue = new DicomTagSQ(0x0072, 0x0080);

        ///<summary>(0072,0081) VR=OV VM=1 Selector OV Value</summary>
        public readonly static DicomTagOV SelectorOVValue = new DicomTagOV(0x0072, 0x0081);

        ///<summary>(0072,0082) VR=SV VM=1-n Selector SV Value</summary>
        public readonly static DicomTagSVs SelectorSVValue = new DicomTagSVs(0x0072, 0x0082);

        ///<summary>(0072,0083) VR=UV VM=1-n Selector UV Value</summary>
        public readonly static DicomTagUVs SelectorUVValue = new DicomTagUVs(0x0072, 0x0083);

        ///<summary>(0072,0100) VR=US VM=1 Number of Screens</summary>
        public readonly static DicomTagUS NumberOfScreens = new DicomTagUS(0x0072, 0x0100);

        ///<summary>(0072,0102) VR=SQ VM=1 Nominal Screen Definition Sequence</summary>
        public readonly static DicomTagSQ NominalScreenDefinitionSequence = new DicomTagSQ(0x0072, 0x0102);

        ///<summary>(0072,0104) VR=US VM=1 Number of Vertical Pixels</summary>
        public readonly static DicomTagUS NumberOfVerticalPixels = new DicomTagUS(0x0072, 0x0104);

        ///<summary>(0072,0106) VR=US VM=1 Number of Horizontal Pixels</summary>
        public readonly static DicomTagUS NumberOfHorizontalPixels = new DicomTagUS(0x0072, 0x0106);

        ///<summary>(0072,0108) VR=FD VM=4 Display Environment Spatial Position</summary>
        public readonly static DicomTagFDs DisplayEnvironmentSpatialPosition = new DicomTagFDs(0x0072, 0x0108);

        ///<summary>(0072,010A) VR=US VM=1 Screen Minimum Grayscale Bit Depth</summary>
        public readonly static DicomTagUS ScreenMinimumGrayscaleBitDepth = new DicomTagUS(0x0072, 0x010A);

        ///<summary>(0072,010C) VR=US VM=1 Screen Minimum Color Bit Depth</summary>
        public readonly static DicomTagUS ScreenMinimumColorBitDepth = new DicomTagUS(0x0072, 0x010C);

        ///<summary>(0072,010E) VR=US VM=1 Application Maximum Repaint Time</summary>
        public readonly static DicomTagUS ApplicationMaximumRepaintTime = new DicomTagUS(0x0072, 0x010E);

        ///<summary>(0072,0200) VR=SQ VM=1 Display Sets Sequence</summary>
        public readonly static DicomTagSQ DisplaySetsSequence = new DicomTagSQ(0x0072, 0x0200);

        ///<summary>(0072,0202) VR=US VM=1 Display Set Number</summary>
        public readonly static DicomTagUS DisplaySetNumber = new DicomTagUS(0x0072, 0x0202);

        ///<summary>(0072,0203) VR=LO VM=1 Display Set Label</summary>
        public readonly static DicomTagLO DisplaySetLabel = new DicomTagLO(0x0072, 0x0203);

        ///<summary>(0072,0204) VR=US VM=1 Display Set Presentation Group</summary>
        public readonly static DicomTagUS DisplaySetPresentationGroup = new DicomTagUS(0x0072, 0x0204);

        ///<summary>(0072,0206) VR=LO VM=1 Display Set Presentation Group Description</summary>
        public readonly static DicomTagLO DisplaySetPresentationGroupDescription = new DicomTagLO(0x0072, 0x0206);

        ///<summary>(0072,0208) VR=CS VM=1 Partial Data Display Handling</summary>
        public readonly static DicomTagCS PartialDataDisplayHandling = new DicomTagCS(0x0072, 0x0208);

        ///<summary>(0072,0210) VR=SQ VM=1 Synchronized Scrolling Sequence</summary>
        public readonly static DicomTagSQ SynchronizedScrollingSequence = new DicomTagSQ(0x0072, 0x0210);

        ///<summary>(0072,0212) VR=US VM=2-n Display Set Scrolling Group</summary>
        public readonly static DicomTagUSs DisplaySetScrollingGroup = new DicomTagUSs(0x0072, 0x0212);

        ///<summary>(0072,0214) VR=SQ VM=1 Navigation Indicator Sequence</summary>
        public readonly static DicomTagSQ NavigationIndicatorSequence = new DicomTagSQ(0x0072, 0x0214);

        ///<summary>(0072,0216) VR=US VM=1 Navigation Display Set</summary>
        public readonly static DicomTagUS NavigationDisplaySet = new DicomTagUS(0x0072, 0x0216);

        ///<summary>(0072,0218) VR=US VM=1-n Reference Display Sets</summary>
        public readonly static DicomTagUSs ReferenceDisplaySets = new DicomTagUSs(0x0072, 0x0218);

        ///<summary>(0072,0300) VR=SQ VM=1 Image Boxes Sequence</summary>
        public readonly static DicomTagSQ ImageBoxesSequence = new DicomTagSQ(0x0072, 0x0300);

        ///<summary>(0072,0302) VR=US VM=1 Image Box Number</summary>
        public readonly static DicomTagUS ImageBoxNumber = new DicomTagUS(0x0072, 0x0302);

        ///<summary>(0072,0304) VR=CS VM=1 Image Box Layout Type</summary>
        public readonly static DicomTagCS ImageBoxLayoutType = new DicomTagCS(0x0072, 0x0304);

        ///<summary>(0072,0306) VR=US VM=1 Image Box Tile Horizontal Dimension</summary>
        public readonly static DicomTagUS ImageBoxTileHorizontalDimension = new DicomTagUS(0x0072, 0x0306);

        ///<summary>(0072,0308) VR=US VM=1 Image Box Tile Vertical Dimension</summary>
        public readonly static DicomTagUS ImageBoxTileVerticalDimension = new DicomTagUS(0x0072, 0x0308);

        ///<summary>(0072,0310) VR=CS VM=1 Image Box Scroll Direction</summary>
        public readonly static DicomTagCS ImageBoxScrollDirection = new DicomTagCS(0x0072, 0x0310);

        ///<summary>(0072,0312) VR=CS VM=1 Image Box Small Scroll Type</summary>
        public readonly static DicomTagCS ImageBoxSmallScrollType = new DicomTagCS(0x0072, 0x0312);

        ///<summary>(0072,0314) VR=US VM=1 Image Box Small Scroll Amount</summary>
        public readonly static DicomTagUS ImageBoxSmallScrollAmount = new DicomTagUS(0x0072, 0x0314);

        ///<summary>(0072,0316) VR=CS VM=1 Image Box Large Scroll Type</summary>
        public readonly static DicomTagCS ImageBoxLargeScrollType = new DicomTagCS(0x0072, 0x0316);

        ///<summary>(0072,0318) VR=US VM=1 Image Box Large Scroll Amount</summary>
        public readonly static DicomTagUS ImageBoxLargeScrollAmount = new DicomTagUS(0x0072, 0x0318);

        ///<summary>(0072,0320) VR=US VM=1 Image Box Overlap Priority</summary>
        public readonly static DicomTagUS ImageBoxOverlapPriority = new DicomTagUS(0x0072, 0x0320);

        ///<summary>(0072,0330) VR=FD VM=1 Cine Relative to Real-Time</summary>
        public readonly static DicomTagFD CineRelativeToRealTime = new DicomTagFD(0x0072, 0x0330);

        ///<summary>(0072,0400) VR=SQ VM=1 Filter Operations Sequence</summary>
        public readonly static DicomTagSQ FilterOperationsSequence = new DicomTagSQ(0x0072, 0x0400);

        ///<summary>(0072,0402) VR=CS VM=1 Filter-by Category</summary>
        public readonly static DicomTagCS FilterByCategory = new DicomTagCS(0x0072, 0x0402);

        ///<summary>(0072,0404) VR=CS VM=1 Filter-by Attribute Presence</summary>
        public readonly static DicomTagCS FilterByAttributePresence = new DicomTagCS(0x0072, 0x0404);

        ///<summary>(0072,0406) VR=CS VM=1 Filter-by Operator</summary>
        public readonly static DicomTagCS FilterByOperator = new DicomTagCS(0x0072, 0x0406);

        ///<summary>(0072,0420) VR=US VM=3 Structured Display Background CIELab Value</summary>
        public readonly static DicomTagUSs StructuredDisplayBackgroundCIELabValue = new DicomTagUSs(0x0072, 0x0420);

        ///<summary>(0072,0421) VR=US VM=3 Empty Image Box CIELab Value</summary>
        public readonly static DicomTagUSs EmptyImageBoxCIELabValue = new DicomTagUSs(0x0072, 0x0421);

        ///<summary>(0072,0422) VR=SQ VM=1 Structured Display Image Box Sequence</summary>
        public readonly static DicomTagSQ StructuredDisplayImageBoxSequence = new DicomTagSQ(0x0072, 0x0422);

        ///<summary>(0072,0424) VR=SQ VM=1 Structured Display Text Box Sequence</summary>
        public readonly static DicomTagSQ StructuredDisplayTextBoxSequence = new DicomTagSQ(0x0072, 0x0424);

        ///<summary>(0072,0427) VR=SQ VM=1 Referenced First Frame Sequence</summary>
        public readonly static DicomTagSQ ReferencedFirstFrameSequence = new DicomTagSQ(0x0072, 0x0427);

        ///<summary>(0072,0430) VR=SQ VM=1 Image Box Synchronization Sequence</summary>
        public readonly static DicomTagSQ ImageBoxSynchronizationSequence = new DicomTagSQ(0x0072, 0x0430);

        ///<summary>(0072,0432) VR=US VM=2-n Synchronized Image Box List</summary>
        public readonly static DicomTagUSs SynchronizedImageBoxList = new DicomTagUSs(0x0072, 0x0432);

        ///<summary>(0072,0434) VR=CS VM=1 Type of Synchronization</summary>
        public readonly static DicomTagCS TypeOfSynchronization = new DicomTagCS(0x0072, 0x0434);

        ///<summary>(0072,0500) VR=CS VM=1 Blending Operation Type</summary>
        public readonly static DicomTagCS BlendingOperationType = new DicomTagCS(0x0072, 0x0500);

        ///<summary>(0072,0510) VR=CS VM=1 Reformatting Operation Type</summary>
        public readonly static DicomTagCS ReformattingOperationType = new DicomTagCS(0x0072, 0x0510);

        ///<summary>(0072,0512) VR=FD VM=1 Reformatting Thickness</summary>
        public readonly static DicomTagFD ReformattingThickness = new DicomTagFD(0x0072, 0x0512);

        ///<summary>(0072,0514) VR=FD VM=1 Reformatting Interval</summary>
        public readonly static DicomTagFD ReformattingInterval = new DicomTagFD(0x0072, 0x0514);

        ///<summary>(0072,0516) VR=CS VM=1 Reformatting Operation Initial View Direction</summary>
        public readonly static DicomTagCS ReformattingOperationInitialViewDirection = new DicomTagCS(0x0072, 0x0516);

        ///<summary>(0072,0520) VR=CS VM=1-n 3D Rendering Type</summary>
        public readonly static DicomTagCSs ThreeDRenderingType = new DicomTagCSs(0x0072, 0x0520);

        ///<summary>(0072,0600) VR=SQ VM=1 Sorting Operations Sequence</summary>
        public readonly static DicomTagSQ SortingOperationsSequence = new DicomTagSQ(0x0072, 0x0600);

        ///<summary>(0072,0602) VR=CS VM=1 Sort-by Category</summary>
        public readonly static DicomTagCS SortByCategory = new DicomTagCS(0x0072, 0x0602);

        ///<summary>(0072,0604) VR=CS VM=1 Sorting Direction</summary>
        public readonly static DicomTagCS SortingDirection = new DicomTagCS(0x0072, 0x0604);

        ///<summary>(0072,0700) VR=CS VM=2 Display Set Patient Orientation</summary>
        public readonly static DicomTagCSs DisplaySetPatientOrientation = new DicomTagCSs(0x0072, 0x0700);

        ///<summary>(0072,0702) VR=CS VM=1 VOI Type</summary>
        public readonly static DicomTagCS VOIType = new DicomTagCS(0x0072, 0x0702);

        ///<summary>(0072,0704) VR=CS VM=1 Pseudo-Color Type</summary>
        public readonly static DicomTagCS PseudoColorType = new DicomTagCS(0x0072, 0x0704);

        ///<summary>(0072,0705) VR=SQ VM=1 Pseudo-Color Palette Instance Reference Sequence</summary>
        public readonly static DicomTagSQ PseudoColorPaletteInstanceReferenceSequence = new DicomTagSQ(0x0072, 0x0705);

        ///<summary>(0072,0706) VR=CS VM=1 Show Grayscale Inverted</summary>
        public readonly static DicomTagCS ShowGrayscaleInverted = new DicomTagCS(0x0072, 0x0706);

        ///<summary>(0072,0710) VR=CS VM=1 Show Image True Size Flag</summary>
        public readonly static DicomTagCS ShowImageTrueSizeFlag = new DicomTagCS(0x0072, 0x0710);

        ///<summary>(0072,0712) VR=CS VM=1 Show Graphic Annotation Flag</summary>
        public readonly static DicomTagCS ShowGraphicAnnotationFlag = new DicomTagCS(0x0072, 0x0712);

        ///<summary>(0072,0714) VR=CS VM=1 Show Patient Demographics Flag</summary>
        public readonly static DicomTagCS ShowPatientDemographicsFlag = new DicomTagCS(0x0072, 0x0714);

        ///<summary>(0072,0716) VR=CS VM=1 Show Acquisition Techniques Flag</summary>
        public readonly static DicomTagCS ShowAcquisitionTechniquesFlag = new DicomTagCS(0x0072, 0x0716);

        ///<summary>(0072,0717) VR=CS VM=1 Display Set Horizontal Justification</summary>
        public readonly static DicomTagCS DisplaySetHorizontalJustification = new DicomTagCS(0x0072, 0x0717);

        ///<summary>(0072,0718) VR=CS VM=1 Display Set Vertical Justification</summary>
        public readonly static DicomTagCS DisplaySetVerticalJustification = new DicomTagCS(0x0072, 0x0718);

        ///<summary>(0074,0120) VR=FD VM=1 Continuation Start Meterset</summary>
        public readonly static DicomTagFD ContinuationStartMeterset = new DicomTagFD(0x0074, 0x0120);

        ///<summary>(0074,0121) VR=FD VM=1 Continuation End Meterset</summary>
        public readonly static DicomTagFD ContinuationEndMeterset = new DicomTagFD(0x0074, 0x0121);

        ///<summary>(0074,1000) VR=CS VM=1 Procedure Step State</summary>
        public readonly static DicomTagCS ProcedureStepState = new DicomTagCS(0x0074, 0x1000);

        ///<summary>(0074,1002) VR=SQ VM=1 Procedure Step Progress Information Sequence</summary>
        public readonly static DicomTagSQ ProcedureStepProgressInformationSequence = new DicomTagSQ(0x0074, 0x1002);

        ///<summary>(0074,1004) VR=DS VM=1 Procedure Step Progress</summary>
        public readonly static DicomTagDS ProcedureStepProgress = new DicomTagDS(0x0074, 0x1004);

        ///<summary>(0074,1006) VR=ST VM=1 Procedure Step Progress Description</summary>
        public readonly static DicomTagST ProcedureStepProgressDescription = new DicomTagST(0x0074, 0x1006);

        ///<summary>(0074,1007) VR=SQ VM=1 Procedure Step Progress Parameters Sequence</summary>
        public readonly static DicomTagSQ ProcedureStepProgressParametersSequence = new DicomTagSQ(0x0074, 0x1007);

        ///<summary>(0074,1008) VR=SQ VM=1 Procedure Step Communications URI Sequence</summary>
        public readonly static DicomTagSQ ProcedureStepCommunicationsURISequence = new DicomTagSQ(0x0074, 0x1008);

        ///<summary>(0074,100A) VR=UR VM=1 Contact URI</summary>
        public readonly static DicomTagUR ContactURI = new DicomTagUR(0x0074, 0x100A);

        ///<summary>(0074,100C) VR=LO VM=1 Contact Display Name</summary>
        public readonly static DicomTagLO ContactDisplayName = new DicomTagLO(0x0074, 0x100C);

        ///<summary>(0074,100E) VR=SQ VM=1 Procedure Step Discontinuation Reason Code Sequence</summary>
        public readonly static DicomTagSQ ProcedureStepDiscontinuationReasonCodeSequence = new DicomTagSQ(0x0074, 0x100E);

        ///<summary>(0074,1020) VR=SQ VM=1 Beam Task Sequence</summary>
        public readonly static DicomTagSQ BeamTaskSequence = new DicomTagSQ(0x0074, 0x1020);

        ///<summary>(0074,1022) VR=CS VM=1 Beam Task Type</summary>
        public readonly static DicomTagCS BeamTaskType = new DicomTagCS(0x0074, 0x1022);

        ///<summary>(0074,1024) VR=IS VM=1 Beam Order Index (Trial) (RETIRED)</summary>
        public readonly static DicomTagIS BeamOrderIndexTrialRETIRED = new DicomTagIS(0x0074, 0x1024);

        ///<summary>(0074,1025) VR=CS VM=1 Autosequence Flag</summary>
        public readonly static DicomTagCS AutosequenceFlag = new DicomTagCS(0x0074, 0x1025);

        ///<summary>(0074,1026) VR=FD VM=1 Table Top Vertical Adjusted Position</summary>
        public readonly static DicomTagFD TableTopVerticalAdjustedPosition = new DicomTagFD(0x0074, 0x1026);

        ///<summary>(0074,1027) VR=FD VM=1 Table Top Longitudinal Adjusted Position</summary>
        public readonly static DicomTagFD TableTopLongitudinalAdjustedPosition = new DicomTagFD(0x0074, 0x1027);

        ///<summary>(0074,1028) VR=FD VM=1 Table Top Lateral Adjusted Position</summary>
        public readonly static DicomTagFD TableTopLateralAdjustedPosition = new DicomTagFD(0x0074, 0x1028);

        ///<summary>(0074,102A) VR=FD VM=1 Patient Support Adjusted Angle</summary>
        public readonly static DicomTagFD PatientSupportAdjustedAngle = new DicomTagFD(0x0074, 0x102A);

        ///<summary>(0074,102B) VR=FD VM=1 Table Top Eccentric Adjusted Angle</summary>
        public readonly static DicomTagFD TableTopEccentricAdjustedAngle = new DicomTagFD(0x0074, 0x102B);

        ///<summary>(0074,102C) VR=FD VM=1 Table Top Pitch Adjusted Angle</summary>
        public readonly static DicomTagFD TableTopPitchAdjustedAngle = new DicomTagFD(0x0074, 0x102C);

        ///<summary>(0074,102D) VR=FD VM=1 Table Top Roll Adjusted Angle</summary>
        public readonly static DicomTagFD TableTopRollAdjustedAngle = new DicomTagFD(0x0074, 0x102D);

        ///<summary>(0074,1030) VR=SQ VM=1 Delivery Verification Image Sequence</summary>
        public readonly static DicomTagSQ DeliveryVerificationImageSequence = new DicomTagSQ(0x0074, 0x1030);

        ///<summary>(0074,1032) VR=CS VM=1 Verification Image Timing</summary>
        public readonly static DicomTagCS VerificationImageTiming = new DicomTagCS(0x0074, 0x1032);

        ///<summary>(0074,1034) VR=CS VM=1 Double Exposure Flag</summary>
        public readonly static DicomTagCS DoubleExposureFlag = new DicomTagCS(0x0074, 0x1034);

        ///<summary>(0074,1036) VR=CS VM=1 Double Exposure Ordering</summary>
        public readonly static DicomTagCS DoubleExposureOrdering = new DicomTagCS(0x0074, 0x1036);

        ///<summary>(0074,1038) VR=DS VM=1 Double Exposure Meterset (Trial) (RETIRED)</summary>
        public readonly static DicomTagDS DoubleExposureMetersetTrialRETIRED = new DicomTagDS(0x0074, 0x1038);

        ///<summary>(0074,103A) VR=DS VM=4 Double Exposure Field Delta (Trial) (RETIRED)</summary>
        public readonly static DicomTagDSs DoubleExposureFieldDeltaTrialRETIRED = new DicomTagDSs(0x0074, 0x103A);

        ///<summary>(0074,1040) VR=SQ VM=1 Related Reference RT Image Sequence</summary>
        public readonly static DicomTagSQ RelatedReferenceRTImageSequence = new DicomTagSQ(0x0074, 0x1040);

        ///<summary>(0074,1042) VR=SQ VM=1 General Machine Verification Sequence</summary>
        public readonly static DicomTagSQ GeneralMachineVerificationSequence = new DicomTagSQ(0x0074, 0x1042);

        ///<summary>(0074,1044) VR=SQ VM=1 Conventional Machine Verification Sequence</summary>
        public readonly static DicomTagSQ ConventionalMachineVerificationSequence = new DicomTagSQ(0x0074, 0x1044);

        ///<summary>(0074,1046) VR=SQ VM=1 Ion Machine Verification Sequence</summary>
        public readonly static DicomTagSQ IonMachineVerificationSequence = new DicomTagSQ(0x0074, 0x1046);

        ///<summary>(0074,1048) VR=SQ VM=1 Failed Attributes Sequence</summary>
        public readonly static DicomTagSQ FailedAttributesSequence = new DicomTagSQ(0x0074, 0x1048);

        ///<summary>(0074,104A) VR=SQ VM=1 Overridden Attributes Sequence</summary>
        public readonly static DicomTagSQ OverriddenAttributesSequence = new DicomTagSQ(0x0074, 0x104A);

        ///<summary>(0074,104C) VR=SQ VM=1 Conventional Control Point Verification Sequence</summary>
        public readonly static DicomTagSQ ConventionalControlPointVerificationSequence = new DicomTagSQ(0x0074, 0x104C);

        ///<summary>(0074,104E) VR=SQ VM=1 Ion Control Point Verification Sequence</summary>
        public readonly static DicomTagSQ IonControlPointVerificationSequence = new DicomTagSQ(0x0074, 0x104E);

        ///<summary>(0074,1050) VR=SQ VM=1 Attribute Occurrence Sequence</summary>
        public readonly static DicomTagSQ AttributeOccurrenceSequence = new DicomTagSQ(0x0074, 0x1050);

        ///<summary>(0074,1052) VR=AT VM=1 Attribute Occurrence Pointer</summary>
        public readonly static DicomTagAT AttributeOccurrencePointer = new DicomTagAT(0x0074, 0x1052);

        ///<summary>(0074,1054) VR=UL VM=1 Attribute Item Selector</summary>
        public readonly static DicomTagUL AttributeItemSelector = new DicomTagUL(0x0074, 0x1054);

        ///<summary>(0074,1056) VR=LO VM=1 Attribute Occurrence Private Creator</summary>
        public readonly static DicomTagLO AttributeOccurrencePrivateCreator = new DicomTagLO(0x0074, 0x1056);

        ///<summary>(0074,1057) VR=IS VM=1-n Selector Sequence Pointer Items</summary>
        public readonly static DicomTagISs SelectorSequencePointerItems = new DicomTagISs(0x0074, 0x1057);

        ///<summary>(0074,1200) VR=CS VM=1 Scheduled Procedure Step Priority</summary>
        public readonly static DicomTagCS ScheduledProcedureStepPriority = new DicomTagCS(0x0074, 0x1200);

        ///<summary>(0074,1202) VR=LO VM=1 Worklist Label</summary>
        public readonly static DicomTagLO WorklistLabel = new DicomTagLO(0x0074, 0x1202);

        ///<summary>(0074,1204) VR=LO VM=1 Procedure Step Label</summary>
        public readonly static DicomTagLO ProcedureStepLabel = new DicomTagLO(0x0074, 0x1204);

        ///<summary>(0074,1210) VR=SQ VM=1 Scheduled Processing Parameters Sequence</summary>
        public readonly static DicomTagSQ ScheduledProcessingParametersSequence = new DicomTagSQ(0x0074, 0x1210);

        ///<summary>(0074,1212) VR=SQ VM=1 Performed Processing Parameters Sequence</summary>
        public readonly static DicomTagSQ PerformedProcessingParametersSequence = new DicomTagSQ(0x0074, 0x1212);

        ///<summary>(0074,1216) VR=SQ VM=1 Unified Procedure Step Performed Procedure Sequence</summary>
        public readonly static DicomTagSQ UnifiedProcedureStepPerformedProcedureSequence = new DicomTagSQ(0x0074, 0x1216);

        ///<summary>(0074,1220) VR=SQ VM=1 Related Procedure Step Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ RelatedProcedureStepSequenceRETIRED = new DicomTagSQ(0x0074, 0x1220);

        ///<summary>(0074,1222) VR=LO VM=1 Procedure Step Relationship Type (RETIRED)</summary>
        public readonly static DicomTagLO ProcedureStepRelationshipTypeRETIRED = new DicomTagLO(0x0074, 0x1222);

        ///<summary>(0074,1224) VR=SQ VM=1 Replaced Procedure Step Sequence</summary>
        public readonly static DicomTagSQ ReplacedProcedureStepSequence = new DicomTagSQ(0x0074, 0x1224);

        ///<summary>(0074,1230) VR=LO VM=1 Deletion Lock</summary>
        public readonly static DicomTagLO DeletionLock = new DicomTagLO(0x0074, 0x1230);

        ///<summary>(0074,1234) VR=AE VM=1 Receiving AE</summary>
        public readonly static DicomTagAE ReceivingAE = new DicomTagAE(0x0074, 0x1234);

        ///<summary>(0074,1236) VR=AE VM=1 Requesting AE</summary>
        public readonly static DicomTagAE RequestingAE = new DicomTagAE(0x0074, 0x1236);

        ///<summary>(0074,1238) VR=LT VM=1 Reason for Cancellation</summary>
        public readonly static DicomTagLT ReasonForCancellation = new DicomTagLT(0x0074, 0x1238);

        ///<summary>(0074,1242) VR=CS VM=1 SCP Status</summary>
        public readonly static DicomTagCS SCPStatus = new DicomTagCS(0x0074, 0x1242);

        ///<summary>(0074,1244) VR=CS VM=1 Subscription List Status</summary>
        public readonly static DicomTagCS SubscriptionListStatus = new DicomTagCS(0x0074, 0x1244);

        ///<summary>(0074,1246) VR=CS VM=1 Unified Procedure Step List Status</summary>
        public readonly static DicomTagCS UnifiedProcedureStepListStatus = new DicomTagCS(0x0074, 0x1246);

        ///<summary>(0074,1324) VR=UL VM=1 Beam Order Index</summary>
        public readonly static DicomTagUL BeamOrderIndex = new DicomTagUL(0x0074, 0x1324);

        ///<summary>(0074,1338) VR=FD VM=1 Double Exposure Meterset</summary>
        public readonly static DicomTagFD DoubleExposureMeterset = new DicomTagFD(0x0074, 0x1338);

        ///<summary>(0074,133A) VR=FD VM=4 Double Exposure Field Delta</summary>
        public readonly static DicomTagFDs DoubleExposureFieldDelta = new DicomTagFDs(0x0074, 0x133A);

        ///<summary>(0074,1401) VR=SQ VM=1 Brachy Task Sequence</summary>
        public readonly static DicomTagSQ BrachyTaskSequence = new DicomTagSQ(0x0074, 0x1401);

        ///<summary>(0074,1402) VR=DS VM=1 Continuation Start Total Reference Air Kerma</summary>
        public readonly static DicomTagDS ContinuationStartTotalReferenceAirKerma = new DicomTagDS(0x0074, 0x1402);

        ///<summary>(0074,1403) VR=DS VM=1 Continuation End Total Reference Air Kerma</summary>
        public readonly static DicomTagDS ContinuationEndTotalReferenceAirKerma = new DicomTagDS(0x0074, 0x1403);

        ///<summary>(0074,1404) VR=IS VM=1 Continuation Pulse Number</summary>
        public readonly static DicomTagIS ContinuationPulseNumber = new DicomTagIS(0x0074, 0x1404);

        ///<summary>(0074,1405) VR=SQ VM=1 Channel Delivery Order Sequence</summary>
        public readonly static DicomTagSQ ChannelDeliveryOrderSequence = new DicomTagSQ(0x0074, 0x1405);

        ///<summary>(0074,1406) VR=IS VM=1 Referenced Channel Number</summary>
        public readonly static DicomTagIS ReferencedChannelNumber = new DicomTagIS(0x0074, 0x1406);

        ///<summary>(0074,1407) VR=DS VM=1 Start Cumulative Time Weight</summary>
        public readonly static DicomTagDS StartCumulativeTimeWeight = new DicomTagDS(0x0074, 0x1407);

        ///<summary>(0074,1408) VR=DS VM=1 End Cumulative Time Weight</summary>
        public readonly static DicomTagDS EndCumulativeTimeWeight = new DicomTagDS(0x0074, 0x1408);

        ///<summary>(0074,1409) VR=SQ VM=1 Omitted Channel Sequence</summary>
        public readonly static DicomTagSQ OmittedChannelSequence = new DicomTagSQ(0x0074, 0x1409);

        ///<summary>(0074,140A) VR=CS VM=1 Reason for Channel Omission</summary>
        public readonly static DicomTagCS ReasonForChannelOmission = new DicomTagCS(0x0074, 0x140A);

        ///<summary>(0074,140B) VR=LO VM=1 Reason for Channel Omission Description</summary>
        public readonly static DicomTagLO ReasonForChannelOmissionDescription = new DicomTagLO(0x0074, 0x140B);

        ///<summary>(0074,140C) VR=IS VM=1 Channel Delivery Order Index</summary>
        public readonly static DicomTagIS ChannelDeliveryOrderIndex = new DicomTagIS(0x0074, 0x140C);

        ///<summary>(0074,140D) VR=SQ VM=1 Channel Delivery Continuation Sequence</summary>
        public readonly static DicomTagSQ ChannelDeliveryContinuationSequence = new DicomTagSQ(0x0074, 0x140D);

        ///<summary>(0074,140E) VR=SQ VM=1 Omitted Application Setup Sequence</summary>
        public readonly static DicomTagSQ OmittedApplicationSetupSequence = new DicomTagSQ(0x0074, 0x140E);

        ///<summary>(0076,0001) VR=LO VM=1 Implant Assembly Template Name</summary>
        public readonly static DicomTagLO ImplantAssemblyTemplateName = new DicomTagLO(0x0076, 0x0001);

        ///<summary>(0076,0003) VR=LO VM=1 Implant Assembly Template Issuer</summary>
        public readonly static DicomTagLO ImplantAssemblyTemplateIssuer = new DicomTagLO(0x0076, 0x0003);

        ///<summary>(0076,0006) VR=LO VM=1 Implant Assembly Template Version</summary>
        public readonly static DicomTagLO ImplantAssemblyTemplateVersion = new DicomTagLO(0x0076, 0x0006);

        ///<summary>(0076,0008) VR=SQ VM=1 Replaced Implant Assembly Template Sequence</summary>
        public readonly static DicomTagSQ ReplacedImplantAssemblyTemplateSequence = new DicomTagSQ(0x0076, 0x0008);

        ///<summary>(0076,000A) VR=CS VM=1 Implant Assembly Template Type</summary>
        public readonly static DicomTagCS ImplantAssemblyTemplateType = new DicomTagCS(0x0076, 0x000A);

        ///<summary>(0076,000C) VR=SQ VM=1 Original Implant Assembly Template Sequence</summary>
        public readonly static DicomTagSQ OriginalImplantAssemblyTemplateSequence = new DicomTagSQ(0x0076, 0x000C);

        ///<summary>(0076,000E) VR=SQ VM=1 Derivation Implant Assembly Template Sequence</summary>
        public readonly static DicomTagSQ DerivationImplantAssemblyTemplateSequence = new DicomTagSQ(0x0076, 0x000E);

        ///<summary>(0076,0010) VR=SQ VM=1 Implant Assembly Template Target Anatomy Sequence</summary>
        public readonly static DicomTagSQ ImplantAssemblyTemplateTargetAnatomySequence = new DicomTagSQ(0x0076, 0x0010);

        ///<summary>(0076,0020) VR=SQ VM=1 Procedure Type Code Sequence</summary>
        public readonly static DicomTagSQ ProcedureTypeCodeSequence = new DicomTagSQ(0x0076, 0x0020);

        ///<summary>(0076,0030) VR=LO VM=1 Surgical Technique</summary>
        public readonly static DicomTagLO SurgicalTechnique = new DicomTagLO(0x0076, 0x0030);

        ///<summary>(0076,0032) VR=SQ VM=1 Component Types Sequence</summary>
        public readonly static DicomTagSQ ComponentTypesSequence = new DicomTagSQ(0x0076, 0x0032);

        ///<summary>(0076,0034) VR=SQ VM=1 Component Type Code Sequence</summary>
        public readonly static DicomTagSQ ComponentTypeCodeSequence = new DicomTagSQ(0x0076, 0x0034);

        ///<summary>(0076,0036) VR=CS VM=1 Exclusive Component Type</summary>
        public readonly static DicomTagCS ExclusiveComponentType = new DicomTagCS(0x0076, 0x0036);

        ///<summary>(0076,0038) VR=CS VM=1 Mandatory Component Type</summary>
        public readonly static DicomTagCS MandatoryComponentType = new DicomTagCS(0x0076, 0x0038);

        ///<summary>(0076,0040) VR=SQ VM=1 Component Sequence</summary>
        public readonly static DicomTagSQ ComponentSequence = new DicomTagSQ(0x0076, 0x0040);

        ///<summary>(0076,0055) VR=US VM=1 Component ID</summary>
        public readonly static DicomTagUS ComponentID = new DicomTagUS(0x0076, 0x0055);

        ///<summary>(0076,0060) VR=SQ VM=1 Component Assembly Sequence</summary>
        public readonly static DicomTagSQ ComponentAssemblySequence = new DicomTagSQ(0x0076, 0x0060);

        ///<summary>(0076,0070) VR=US VM=1 Component 1 Referenced ID</summary>
        public readonly static DicomTagUS Component1ReferencedID = new DicomTagUS(0x0076, 0x0070);

        ///<summary>(0076,0080) VR=US VM=1 Component 1 Referenced Mating Feature Set ID</summary>
        public readonly static DicomTagUS Component1ReferencedMatingFeatureSetID = new DicomTagUS(0x0076, 0x0080);

        ///<summary>(0076,0090) VR=US VM=1 Component 1 Referenced Mating Feature ID</summary>
        public readonly static DicomTagUS Component1ReferencedMatingFeatureID = new DicomTagUS(0x0076, 0x0090);

        ///<summary>(0076,00A0) VR=US VM=1 Component 2 Referenced ID</summary>
        public readonly static DicomTagUS Component2ReferencedID = new DicomTagUS(0x0076, 0x00A0);

        ///<summary>(0076,00B0) VR=US VM=1 Component 2 Referenced Mating Feature Set ID</summary>
        public readonly static DicomTagUS Component2ReferencedMatingFeatureSetID = new DicomTagUS(0x0076, 0x00B0);

        ///<summary>(0076,00C0) VR=US VM=1 Component 2 Referenced Mating Feature ID</summary>
        public readonly static DicomTagUS Component2ReferencedMatingFeatureID = new DicomTagUS(0x0076, 0x00C0);

        ///<summary>(0078,0001) VR=LO VM=1 Implant Template Group Name</summary>
        public readonly static DicomTagLO ImplantTemplateGroupName = new DicomTagLO(0x0078, 0x0001);

        ///<summary>(0078,0010) VR=ST VM=1 Implant Template Group Description</summary>
        public readonly static DicomTagST ImplantTemplateGroupDescription = new DicomTagST(0x0078, 0x0010);

        ///<summary>(0078,0020) VR=LO VM=1 Implant Template Group Issuer</summary>
        public readonly static DicomTagLO ImplantTemplateGroupIssuer = new DicomTagLO(0x0078, 0x0020);

        ///<summary>(0078,0024) VR=LO VM=1 Implant Template Group Version</summary>
        public readonly static DicomTagLO ImplantTemplateGroupVersion = new DicomTagLO(0x0078, 0x0024);

        ///<summary>(0078,0026) VR=SQ VM=1 Replaced Implant Template Group Sequence</summary>
        public readonly static DicomTagSQ ReplacedImplantTemplateGroupSequence = new DicomTagSQ(0x0078, 0x0026);

        ///<summary>(0078,0028) VR=SQ VM=1 Implant Template Group Target Anatomy Sequence</summary>
        public readonly static DicomTagSQ ImplantTemplateGroupTargetAnatomySequence = new DicomTagSQ(0x0078, 0x0028);

        ///<summary>(0078,002A) VR=SQ VM=1 Implant Template Group Members Sequence</summary>
        public readonly static DicomTagSQ ImplantTemplateGroupMembersSequence = new DicomTagSQ(0x0078, 0x002A);

        ///<summary>(0078,002E) VR=US VM=1 Implant Template Group Member ID</summary>
        public readonly static DicomTagUS ImplantTemplateGroupMemberID = new DicomTagUS(0x0078, 0x002E);

        ///<summary>(0078,0050) VR=FD VM=3 3D Implant Template Group Member Matching Point</summary>
        public readonly static DicomTagFDs ThreeDImplantTemplateGroupMemberMatchingPoint = new DicomTagFDs(0x0078, 0x0050);

        ///<summary>(0078,0060) VR=FD VM=9 3D Implant Template Group Member Matching Axes</summary>
        public readonly static DicomTagFDs ThreeDImplantTemplateGroupMemberMatchingAxes = new DicomTagFDs(0x0078, 0x0060);

        ///<summary>(0078,0070) VR=SQ VM=1 Implant Template Group Member Matching 2D Coordinates Sequence</summary>
        public readonly static DicomTagSQ ImplantTemplateGroupMemberMatching2DCoordinatesSequence = new DicomTagSQ(0x0078, 0x0070);

        ///<summary>(0078,0090) VR=FD VM=2 2D Implant Template Group Member Matching Point</summary>
        public readonly static DicomTagFDs TwoDImplantTemplateGroupMemberMatchingPoint = new DicomTagFDs(0x0078, 0x0090);

        ///<summary>(0078,00A0) VR=FD VM=4 2D Implant Template Group Member Matching Axes</summary>
        public readonly static DicomTagFDs TwoDImplantTemplateGroupMemberMatchingAxes = new DicomTagFDs(0x0078, 0x00A0);

        ///<summary>(0078,00B0) VR=SQ VM=1 Implant Template Group Variation Dimension Sequence</summary>
        public readonly static DicomTagSQ ImplantTemplateGroupVariationDimensionSequence = new DicomTagSQ(0x0078, 0x00B0);

        ///<summary>(0078,00B2) VR=LO VM=1 Implant Template Group Variation Dimension Name</summary>
        public readonly static DicomTagLO ImplantTemplateGroupVariationDimensionName = new DicomTagLO(0x0078, 0x00B2);

        ///<summary>(0078,00B4) VR=SQ VM=1 Implant Template Group Variation Dimension Rank Sequence</summary>
        public readonly static DicomTagSQ ImplantTemplateGroupVariationDimensionRankSequence = new DicomTagSQ(0x0078, 0x00B4);

        ///<summary>(0078,00B6) VR=US VM=1 Referenced Implant Template Group Member ID</summary>
        public readonly static DicomTagUS ReferencedImplantTemplateGroupMemberID = new DicomTagUS(0x0078, 0x00B6);

        ///<summary>(0078,00B8) VR=US VM=1 Implant Template Group Variation Dimension Rank</summary>
        public readonly static DicomTagUS ImplantTemplateGroupVariationDimensionRank = new DicomTagUS(0x0078, 0x00B8);

        ///<summary>(0080,0001) VR=SQ VM=1 Surface Scan Acquisition Type Code Sequence</summary>
        public readonly static DicomTagSQ SurfaceScanAcquisitionTypeCodeSequence = new DicomTagSQ(0x0080, 0x0001);

        ///<summary>(0080,0002) VR=SQ VM=1 Surface Scan Mode Code Sequence</summary>
        public readonly static DicomTagSQ SurfaceScanModeCodeSequence = new DicomTagSQ(0x0080, 0x0002);

        ///<summary>(0080,0003) VR=SQ VM=1 Registration Method Code Sequence</summary>
        public readonly static DicomTagSQ RegistrationMethodCodeSequence = new DicomTagSQ(0x0080, 0x0003);

        ///<summary>(0080,0004) VR=FD VM=1 Shot Duration Time</summary>
        public readonly static DicomTagFD ShotDurationTime = new DicomTagFD(0x0080, 0x0004);

        ///<summary>(0080,0005) VR=FD VM=1 Shot Offset Time</summary>
        public readonly static DicomTagFD ShotOffsetTime = new DicomTagFD(0x0080, 0x0005);

        ///<summary>(0080,0006) VR=US VM=1-n Surface Point Presentation Value Data</summary>
        public readonly static DicomTagUSs SurfacePointPresentationValueData = new DicomTagUSs(0x0080, 0x0006);

        ///<summary>(0080,0007) VR=US VM=3-3n Surface Point Color CIELab Value Data</summary>
        public readonly static DicomTagUSs SurfacePointColorCIELabValueData = new DicomTagUSs(0x0080, 0x0007);

        ///<summary>(0080,0008) VR=SQ VM=1 UV Mapping Sequence</summary>
        public readonly static DicomTagSQ UVMappingSequence = new DicomTagSQ(0x0080, 0x0008);

        ///<summary>(0080,0009) VR=SH VM=1 Texture Label</summary>
        public readonly static DicomTagSH TextureLabel = new DicomTagSH(0x0080, 0x0009);

        ///<summary>(0080,0010) VR=OF VM=1 U Value Data</summary>
        public readonly static DicomTagOF UValueData = new DicomTagOF(0x0080, 0x0010);

        ///<summary>(0080,0011) VR=OF VM=1 V Value Data</summary>
        public readonly static DicomTagOF VValueData = new DicomTagOF(0x0080, 0x0011);

        ///<summary>(0080,0012) VR=SQ VM=1 Referenced Texture Sequence</summary>
        public readonly static DicomTagSQ ReferencedTextureSequence = new DicomTagSQ(0x0080, 0x0012);

        ///<summary>(0080,0013) VR=SQ VM=1 Referenced Surface Data Sequence</summary>
        public readonly static DicomTagSQ ReferencedSurfaceDataSequence = new DicomTagSQ(0x0080, 0x0013);

        ///<summary>(0082,0001) VR=CS VM=1 Assessment Summary</summary>
        public readonly static DicomTagCS AssessmentSummary = new DicomTagCS(0x0082, 0x0001);

        ///<summary>(0082,0003) VR=UT VM=1 Assessment Summary Description</summary>
        public readonly static DicomTagUT AssessmentSummaryDescription = new DicomTagUT(0x0082, 0x0003);

        ///<summary>(0082,0004) VR=SQ VM=1 Assessed SOP Instance Sequence</summary>
        public readonly static DicomTagSQ AssessedSOPInstanceSequence = new DicomTagSQ(0x0082, 0x0004);

        ///<summary>(0082,0005) VR=SQ VM=1 Referenced Comparison SOP Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedComparisonSOPInstanceSequence = new DicomTagSQ(0x0082, 0x0005);

        ///<summary>(0082,0006) VR=UL VM=1 Number of Assessment Observations</summary>
        public readonly static DicomTagUL NumberOfAssessmentObservations = new DicomTagUL(0x0082, 0x0006);

        ///<summary>(0082,0007) VR=SQ VM=1 Assessment Observations Sequence</summary>
        public readonly static DicomTagSQ AssessmentObservationsSequence = new DicomTagSQ(0x0082, 0x0007);

        ///<summary>(0082,0008) VR=CS VM=1 Observation Significance</summary>
        public readonly static DicomTagCS ObservationSignificance = new DicomTagCS(0x0082, 0x0008);

        ///<summary>(0082,000A) VR=UT VM=1 Observation Description</summary>
        public readonly static DicomTagUT ObservationDescription = new DicomTagUT(0x0082, 0x000A);

        ///<summary>(0082,000C) VR=SQ VM=1 Structured Constraint Observation Sequence</summary>
        public readonly static DicomTagSQ StructuredConstraintObservationSequence = new DicomTagSQ(0x0082, 0x000C);

        ///<summary>(0082,0010) VR=SQ VM=1 Assessed Attribute Value Sequence</summary>
        public readonly static DicomTagSQ AssessedAttributeValueSequence = new DicomTagSQ(0x0082, 0x0010);

        ///<summary>(0082,0016) VR=LO VM=1 Assessment Set ID</summary>
        public readonly static DicomTagLO AssessmentSetID = new DicomTagLO(0x0082, 0x0016);

        ///<summary>(0082,0017) VR=SQ VM=1 Assessment Requester Sequence</summary>
        public readonly static DicomTagSQ AssessmentRequesterSequence = new DicomTagSQ(0x0082, 0x0017);

        ///<summary>(0082,0018) VR=LO VM=1 Selector Attribute Name</summary>
        public readonly static DicomTagLO SelectorAttributeName = new DicomTagLO(0x0082, 0x0018);

        ///<summary>(0082,0019) VR=LO VM=1 Selector Attribute Keyword</summary>
        public readonly static DicomTagLO SelectorAttributeKeyword = new DicomTagLO(0x0082, 0x0019);

        ///<summary>(0082,0021) VR=SQ VM=1 Assessment Type Code Sequence</summary>
        public readonly static DicomTagSQ AssessmentTypeCodeSequence = new DicomTagSQ(0x0082, 0x0021);

        ///<summary>(0082,0022) VR=SQ VM=1 Observation Basis Code Sequence</summary>
        public readonly static DicomTagSQ ObservationBasisCodeSequence = new DicomTagSQ(0x0082, 0x0022);

        ///<summary>(0082,0023) VR=LO VM=1 Assessment Label</summary>
        public readonly static DicomTagLO AssessmentLabel = new DicomTagLO(0x0082, 0x0023);

        ///<summary>(0082,0032) VR=CS VM=1 Constraint Type</summary>
        public readonly static DicomTagCS ConstraintType = new DicomTagCS(0x0082, 0x0032);

        ///<summary>(0082,0033) VR=UT VM=1 Specification Selection Guidance</summary>
        public readonly static DicomTagUT SpecificationSelectionGuidance = new DicomTagUT(0x0082, 0x0033);

        ///<summary>(0082,0034) VR=SQ VM=1 Constraint Value Sequence</summary>
        public readonly static DicomTagSQ ConstraintValueSequence = new DicomTagSQ(0x0082, 0x0034);

        ///<summary>(0082,0035) VR=SQ VM=1 Recommended Default Value Sequence</summary>
        public readonly static DicomTagSQ RecommendedDefaultValueSequence = new DicomTagSQ(0x0082, 0x0035);

        ///<summary>(0082,0036) VR=CS VM=1 Constraint Violation Significance</summary>
        public readonly static DicomTagCS ConstraintViolationSignificance = new DicomTagCS(0x0082, 0x0036);

        ///<summary>(0082,0037) VR=UT VM=1 Constraint Violation Condition</summary>
        public readonly static DicomTagUT ConstraintViolationCondition = new DicomTagUT(0x0082, 0x0037);

        ///<summary>(0082,0038) VR=CS VM=1 Modifiable Constraint Flag</summary>
        public readonly static DicomTagCS ModifiableConstraintFlag = new DicomTagCS(0x0082, 0x0038);

        ///<summary>(0088,0130) VR=SH VM=1 Storage Media File-set ID</summary>
        public readonly static DicomTagSH StorageMediaFileSetID = new DicomTagSH(0x0088, 0x0130);

        ///<summary>(0088,0140) VR=UI VM=1 Storage Media File-set UID</summary>
        public readonly static DicomTagUI StorageMediaFileSetUID = new DicomTagUI(0x0088, 0x0140);

        ///<summary>(0088,0200) VR=SQ VM=1 Icon Image Sequence</summary>
        public readonly static DicomTagSQ IconImageSequence = new DicomTagSQ(0x0088, 0x0200);

        ///<summary>(0088,0904) VR=LO VM=1 Topic Title (RETIRED)</summary>
        public readonly static DicomTagLO TopicTitleRETIRED = new DicomTagLO(0x0088, 0x0904);

        ///<summary>(0088,0906) VR=ST VM=1 Topic Subject (RETIRED)</summary>
        public readonly static DicomTagST TopicSubjectRETIRED = new DicomTagST(0x0088, 0x0906);

        ///<summary>(0088,0910) VR=LO VM=1 Topic Author (RETIRED)</summary>
        public readonly static DicomTagLO TopicAuthorRETIRED = new DicomTagLO(0x0088, 0x0910);

        ///<summary>(0088,0912) VR=LO VM=1-32 Topic Keywords (RETIRED)</summary>
        public readonly static DicomTagLOs TopicKeywordsRETIRED = new DicomTagLOs(0x0088, 0x0912);

        ///<summary>(0100,0410) VR=CS VM=1 SOP Instance Status</summary>
        public readonly static DicomTagCS SOPInstanceStatus = new DicomTagCS(0x0100, 0x0410);

        ///<summary>(0100,0420) VR=DT VM=1 SOP Authorization DateTime</summary>
        public readonly static DicomTagDT SOPAuthorizationDateTime = new DicomTagDT(0x0100, 0x0420);

        ///<summary>(0100,0424) VR=LT VM=1 SOP Authorization Comment</summary>
        public readonly static DicomTagLT SOPAuthorizationComment = new DicomTagLT(0x0100, 0x0424);

        ///<summary>(0100,0426) VR=LO VM=1 Authorization Equipment Certification Number</summary>
        public readonly static DicomTagLO AuthorizationEquipmentCertificationNumber = new DicomTagLO(0x0100, 0x0426);

        ///<summary>(0400,0005) VR=US VM=1 MAC ID Number</summary>
        public readonly static DicomTagUS MACIDNumber = new DicomTagUS(0x0400, 0x0005);

        ///<summary>(0400,0010) VR=UI VM=1 MAC Calculation Transfer Syntax UID</summary>
        public readonly static DicomTagUI MACCalculationTransferSyntaxUID = new DicomTagUI(0x0400, 0x0010);

        ///<summary>(0400,0015) VR=CS VM=1 MAC Algorithm</summary>
        public readonly static DicomTagCS MACAlgorithm = new DicomTagCS(0x0400, 0x0015);

        ///<summary>(0400,0020) VR=AT VM=1-n Data Elements Signed</summary>
        public readonly static DicomTagATs DataElementsSigned = new DicomTagATs(0x0400, 0x0020);

        ///<summary>(0400,0100) VR=UI VM=1 Digital Signature UID</summary>
        public readonly static DicomTagUI DigitalSignatureUID = new DicomTagUI(0x0400, 0x0100);

        ///<summary>(0400,0105) VR=DT VM=1 Digital Signature DateTime</summary>
        public readonly static DicomTagDT DigitalSignatureDateTime = new DicomTagDT(0x0400, 0x0105);

        ///<summary>(0400,0110) VR=CS VM=1 Certificate Type</summary>
        public readonly static DicomTagCS CertificateType = new DicomTagCS(0x0400, 0x0110);

        ///<summary>(0400,0115) VR=OB VM=1 Certificate of Signer</summary>
        public readonly static DicomTagOB CertificateOfSigner = new DicomTagOB(0x0400, 0x0115);

        ///<summary>(0400,0120) VR=OB VM=1 Signature</summary>
        public readonly static DicomTagOB Signature = new DicomTagOB(0x0400, 0x0120);

        ///<summary>(0400,0305) VR=CS VM=1 Certified Timestamp Type</summary>
        public readonly static DicomTagCS CertifiedTimestampType = new DicomTagCS(0x0400, 0x0305);

        ///<summary>(0400,0310) VR=OB VM=1 Certified Timestamp</summary>
        public readonly static DicomTagOB CertifiedTimestamp = new DicomTagOB(0x0400, 0x0310);

        ///<summary>(0400,0401) VR=SQ VM=1 Digital Signature Purpose Code Sequence</summary>
        public readonly static DicomTagSQ DigitalSignaturePurposeCodeSequence = new DicomTagSQ(0x0400, 0x0401);

        ///<summary>(0400,0402) VR=SQ VM=1 Referenced Digital Signature Sequence</summary>
        public readonly static DicomTagSQ ReferencedDigitalSignatureSequence = new DicomTagSQ(0x0400, 0x0402);

        ///<summary>(0400,0403) VR=SQ VM=1 Referenced SOP Instance MAC Sequence</summary>
        public readonly static DicomTagSQ ReferencedSOPInstanceMACSequence = new DicomTagSQ(0x0400, 0x0403);

        ///<summary>(0400,0404) VR=OB VM=1 MAC</summary>
        public readonly static DicomTagOB MAC = new DicomTagOB(0x0400, 0x0404);

        ///<summary>(0400,0500) VR=SQ VM=1 Encrypted Attributes Sequence</summary>
        public readonly static DicomTagSQ EncryptedAttributesSequence = new DicomTagSQ(0x0400, 0x0500);

        ///<summary>(0400,0510) VR=UI VM=1 Encrypted Content Transfer Syntax UID</summary>
        public readonly static DicomTagUI EncryptedContentTransferSyntaxUID = new DicomTagUI(0x0400, 0x0510);

        ///<summary>(0400,0520) VR=OB VM=1 Encrypted Content</summary>
        public readonly static DicomTagOB EncryptedContent = new DicomTagOB(0x0400, 0x0520);

        ///<summary>(0400,0550) VR=SQ VM=1 Modified Attributes Sequence</summary>
        public readonly static DicomTagSQ ModifiedAttributesSequence = new DicomTagSQ(0x0400, 0x0550);

        ///<summary>(0400,0551) VR=SQ VM=1 Nonconforming Modified Attributes Sequence</summary>
        public readonly static DicomTagSQ NonconformingModifiedAttributesSequence = new DicomTagSQ(0x0400, 0x0551);

        ///<summary>(0400,0552) VR=OB VM=1 Nonconforming Data Element Value</summary>
        public readonly static DicomTagOB NonconformingDataElementValue = new DicomTagOB(0x0400, 0x0552);

        ///<summary>(0400,0561) VR=SQ VM=1 Original Attributes Sequence</summary>
        public readonly static DicomTagSQ OriginalAttributesSequence = new DicomTagSQ(0x0400, 0x0561);

        ///<summary>(0400,0562) VR=DT VM=1 Attribute Modification DateTime</summary>
        public readonly static DicomTagDT AttributeModificationDateTime = new DicomTagDT(0x0400, 0x0562);

        ///<summary>(0400,0563) VR=LO VM=1 Modifying System</summary>
        public readonly static DicomTagLO ModifyingSystem = new DicomTagLO(0x0400, 0x0563);

        ///<summary>(0400,0564) VR=LO VM=1 Source of Previous Values</summary>
        public readonly static DicomTagLO SourceOfPreviousValues = new DicomTagLO(0x0400, 0x0564);

        ///<summary>(0400,0565) VR=CS VM=1 Reason for the Attribute Modification</summary>
        public readonly static DicomTagCS ReasonForTheAttributeModification = new DicomTagCS(0x0400, 0x0565);

        ///<summary>(0400,0600) VR=CS VM=1 Instance Origin Status</summary>
        public readonly static DicomTagCS InstanceOriginStatus = new DicomTagCS(0x0400, 0x0600);

        ///<summary>(1000,xxx0) VR=US VM=3 Escape Triplet (RETIRED)</summary>
        public readonly static DicomTagUSs EscapeTripletRETIRED = new DicomTagUSs(0x1000, 0x0000);

        ///<summary>(1000,xxx1) VR=US VM=3 Run Length Triplet (RETIRED)</summary>
        public readonly static DicomTagUSs RunLengthTripletRETIRED = new DicomTagUSs(0x1000, 0x0001);

        ///<summary>(1000,xxx2) VR=US VM=1 Huffman Table Size (RETIRED)</summary>
        public readonly static DicomTagUS HuffmanTableSizeRETIRED = new DicomTagUS(0x1000, 0x0002);

        ///<summary>(1000,xxx3) VR=US VM=3 Huffman Table Triplet (RETIRED)</summary>
        public readonly static DicomTagUSs HuffmanTableTripletRETIRED = new DicomTagUSs(0x1000, 0x0003);

        ///<summary>(1000,xxx4) VR=US VM=1 Shift Table Size (RETIRED)</summary>
        public readonly static DicomTagUS ShiftTableSizeRETIRED = new DicomTagUS(0x1000, 0x0004);

        ///<summary>(1000,xxx5) VR=US VM=3 Shift Table Triplet (RETIRED)</summary>
        public readonly static DicomTagUSs ShiftTableTripletRETIRED = new DicomTagUSs(0x1000, 0x0005);

        ///<summary>(1010,xxxx) VR=US VM=1-n Zonal Map (RETIRED)</summary>
        public readonly static DicomTagUSs ZonalMapRETIRED = new DicomTagUSs(0x1010, 0x0000);

        ///<summary>(2000,0010) VR=IS VM=1 Number of Copies</summary>
        public readonly static DicomTagIS NumberOfCopies = new DicomTagIS(0x2000, 0x0010);

        ///<summary>(2000,001E) VR=SQ VM=1 Printer Configuration Sequence</summary>
        public readonly static DicomTagSQ PrinterConfigurationSequence = new DicomTagSQ(0x2000, 0x001E);

        ///<summary>(2000,0020) VR=CS VM=1 Print Priority</summary>
        public readonly static DicomTagCS PrintPriority = new DicomTagCS(0x2000, 0x0020);

        ///<summary>(2000,0030) VR=CS VM=1 Medium Type</summary>
        public readonly static DicomTagCS MediumType = new DicomTagCS(0x2000, 0x0030);

        ///<summary>(2000,0040) VR=CS VM=1 Film Destination</summary>
        public readonly static DicomTagCS FilmDestination = new DicomTagCS(0x2000, 0x0040);

        ///<summary>(2000,0050) VR=LO VM=1 Film Session Label</summary>
        public readonly static DicomTagLO FilmSessionLabel = new DicomTagLO(0x2000, 0x0050);

        ///<summary>(2000,0060) VR=IS VM=1 Memory Allocation</summary>
        public readonly static DicomTagIS MemoryAllocation = new DicomTagIS(0x2000, 0x0060);

        ///<summary>(2000,0061) VR=IS VM=1 Maximum Memory Allocation</summary>
        public readonly static DicomTagIS MaximumMemoryAllocation = new DicomTagIS(0x2000, 0x0061);

        ///<summary>(2000,0062) VR=CS VM=1 Color Image Printing Flag (RETIRED)</summary>
        public readonly static DicomTagCS ColorImagePrintingFlagRETIRED = new DicomTagCS(0x2000, 0x0062);

        ///<summary>(2000,0063) VR=CS VM=1 Collation Flag (RETIRED)</summary>
        public readonly static DicomTagCS CollationFlagRETIRED = new DicomTagCS(0x2000, 0x0063);

        ///<summary>(2000,0065) VR=CS VM=1 Annotation Flag (RETIRED)</summary>
        public readonly static DicomTagCS AnnotationFlagRETIRED = new DicomTagCS(0x2000, 0x0065);

        ///<summary>(2000,0067) VR=CS VM=1 Image Overlay Flag (RETIRED)</summary>
        public readonly static DicomTagCS ImageOverlayFlagRETIRED = new DicomTagCS(0x2000, 0x0067);

        ///<summary>(2000,0069) VR=CS VM=1 Presentation LUT Flag (RETIRED)</summary>
        public readonly static DicomTagCS PresentationLUTFlagRETIRED = new DicomTagCS(0x2000, 0x0069);

        ///<summary>(2000,006A) VR=CS VM=1 Image Box Presentation LUT Flag (RETIRED)</summary>
        public readonly static DicomTagCS ImageBoxPresentationLUTFlagRETIRED = new DicomTagCS(0x2000, 0x006A);

        ///<summary>(2000,00A0) VR=US VM=1 Memory Bit Depth</summary>
        public readonly static DicomTagUS MemoryBitDepth = new DicomTagUS(0x2000, 0x00A0);

        ///<summary>(2000,00A1) VR=US VM=1 Printing Bit Depth</summary>
        public readonly static DicomTagUS PrintingBitDepth = new DicomTagUS(0x2000, 0x00A1);

        ///<summary>(2000,00A2) VR=SQ VM=1 Media Installed Sequence</summary>
        public readonly static DicomTagSQ MediaInstalledSequence = new DicomTagSQ(0x2000, 0x00A2);

        ///<summary>(2000,00A4) VR=SQ VM=1 Other Media Available Sequence</summary>
        public readonly static DicomTagSQ OtherMediaAvailableSequence = new DicomTagSQ(0x2000, 0x00A4);

        ///<summary>(2000,00A8) VR=SQ VM=1 Supported Image Display Formats Sequence</summary>
        public readonly static DicomTagSQ SupportedImageDisplayFormatsSequence = new DicomTagSQ(0x2000, 0x00A8);

        ///<summary>(2000,0500) VR=SQ VM=1 Referenced Film Box Sequence</summary>
        public readonly static DicomTagSQ ReferencedFilmBoxSequence = new DicomTagSQ(0x2000, 0x0500);

        ///<summary>(2000,0510) VR=SQ VM=1 Referenced Stored Print Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedStoredPrintSequenceRETIRED = new DicomTagSQ(0x2000, 0x0510);

        ///<summary>(2010,0010) VR=ST VM=1 Image Display Format</summary>
        public readonly static DicomTagST ImageDisplayFormat = new DicomTagST(0x2010, 0x0010);

        ///<summary>(2010,0030) VR=CS VM=1 Annotation Display Format ID</summary>
        public readonly static DicomTagCS AnnotationDisplayFormatID = new DicomTagCS(0x2010, 0x0030);

        ///<summary>(2010,0040) VR=CS VM=1 Film Orientation</summary>
        public readonly static DicomTagCS FilmOrientation = new DicomTagCS(0x2010, 0x0040);

        ///<summary>(2010,0050) VR=CS VM=1 Film Size ID</summary>
        public readonly static DicomTagCS FilmSizeID = new DicomTagCS(0x2010, 0x0050);

        ///<summary>(2010,0052) VR=CS VM=1 Printer Resolution ID</summary>
        public readonly static DicomTagCS PrinterResolutionID = new DicomTagCS(0x2010, 0x0052);

        ///<summary>(2010,0054) VR=CS VM=1 Default Printer Resolution ID</summary>
        public readonly static DicomTagCS DefaultPrinterResolutionID = new DicomTagCS(0x2010, 0x0054);

        ///<summary>(2010,0060) VR=CS VM=1 Magnification Type</summary>
        public readonly static DicomTagCS MagnificationType = new DicomTagCS(0x2010, 0x0060);

        ///<summary>(2010,0080) VR=CS VM=1 Smoothing Type</summary>
        public readonly static DicomTagCS SmoothingType = new DicomTagCS(0x2010, 0x0080);

        ///<summary>(2010,00A6) VR=CS VM=1 Default Magnification Type</summary>
        public readonly static DicomTagCS DefaultMagnificationType = new DicomTagCS(0x2010, 0x00A6);

        ///<summary>(2010,00A7) VR=CS VM=1-n Other Magnification Types Available</summary>
        public readonly static DicomTagCSs OtherMagnificationTypesAvailable = new DicomTagCSs(0x2010, 0x00A7);

        ///<summary>(2010,00A8) VR=CS VM=1 Default Smoothing Type</summary>
        public readonly static DicomTagCS DefaultSmoothingType = new DicomTagCS(0x2010, 0x00A8);

        ///<summary>(2010,00A9) VR=CS VM=1-n Other Smoothing Types Available</summary>
        public readonly static DicomTagCSs OtherSmoothingTypesAvailable = new DicomTagCSs(0x2010, 0x00A9);

        ///<summary>(2010,0100) VR=CS VM=1 Border Density</summary>
        public readonly static DicomTagCS BorderDensity = new DicomTagCS(0x2010, 0x0100);

        ///<summary>(2010,0110) VR=CS VM=1 Empty Image Density</summary>
        public readonly static DicomTagCS EmptyImageDensity = new DicomTagCS(0x2010, 0x0110);

        ///<summary>(2010,0120) VR=US VM=1 Min Density</summary>
        public readonly static DicomTagUS MinDensity = new DicomTagUS(0x2010, 0x0120);

        ///<summary>(2010,0130) VR=US VM=1 Max Density</summary>
        public readonly static DicomTagUS MaxDensity = new DicomTagUS(0x2010, 0x0130);

        ///<summary>(2010,0140) VR=CS VM=1 Trim</summary>
        public readonly static DicomTagCS Trim = new DicomTagCS(0x2010, 0x0140);

        ///<summary>(2010,0150) VR=ST VM=1 Configuration Information</summary>
        public readonly static DicomTagST ConfigurationInformation = new DicomTagST(0x2010, 0x0150);

        ///<summary>(2010,0152) VR=LT VM=1 Configuration Information Description</summary>
        public readonly static DicomTagLT ConfigurationInformationDescription = new DicomTagLT(0x2010, 0x0152);

        ///<summary>(2010,0154) VR=IS VM=1 Maximum Collated Films</summary>
        public readonly static DicomTagIS MaximumCollatedFilms = new DicomTagIS(0x2010, 0x0154);

        ///<summary>(2010,015E) VR=US VM=1 Illumination</summary>
        public readonly static DicomTagUS Illumination = new DicomTagUS(0x2010, 0x015E);

        ///<summary>(2010,0160) VR=US VM=1 Reflected Ambient Light</summary>
        public readonly static DicomTagUS ReflectedAmbientLight = new DicomTagUS(0x2010, 0x0160);

        ///<summary>(2010,0376) VR=DS VM=2 Printer Pixel Spacing</summary>
        public readonly static DicomTagDSs PrinterPixelSpacing = new DicomTagDSs(0x2010, 0x0376);

        ///<summary>(2010,0500) VR=SQ VM=1 Referenced Film Session Sequence</summary>
        public readonly static DicomTagSQ ReferencedFilmSessionSequence = new DicomTagSQ(0x2010, 0x0500);

        ///<summary>(2010,0510) VR=SQ VM=1 Referenced Image Box Sequence</summary>
        public readonly static DicomTagSQ ReferencedImageBoxSequence = new DicomTagSQ(0x2010, 0x0510);

        ///<summary>(2010,0520) VR=SQ VM=1 Referenced Basic Annotation Box Sequence</summary>
        public readonly static DicomTagSQ ReferencedBasicAnnotationBoxSequence = new DicomTagSQ(0x2010, 0x0520);

        ///<summary>(2020,0010) VR=US VM=1 Image Box Position</summary>
        public readonly static DicomTagUS ImageBoxPosition = new DicomTagUS(0x2020, 0x0010);

        ///<summary>(2020,0020) VR=CS VM=1 Polarity</summary>
        public readonly static DicomTagCS Polarity = new DicomTagCS(0x2020, 0x0020);

        ///<summary>(2020,0030) VR=DS VM=1 Requested Image Size</summary>
        public readonly static DicomTagDS RequestedImageSize = new DicomTagDS(0x2020, 0x0030);

        ///<summary>(2020,0040) VR=CS VM=1 Requested Decimate/Crop Behavior</summary>
        public readonly static DicomTagCS RequestedDecimateCropBehavior = new DicomTagCS(0x2020, 0x0040);

        ///<summary>(2020,0050) VR=CS VM=1 Requested Resolution ID</summary>
        public readonly static DicomTagCS RequestedResolutionID = new DicomTagCS(0x2020, 0x0050);

        ///<summary>(2020,00A0) VR=CS VM=1 Requested Image Size Flag</summary>
        public readonly static DicomTagCS RequestedImageSizeFlag = new DicomTagCS(0x2020, 0x00A0);

        ///<summary>(2020,00A2) VR=CS VM=1 Decimate/Crop Result</summary>
        public readonly static DicomTagCS DecimateCropResult = new DicomTagCS(0x2020, 0x00A2);

        ///<summary>(2020,0110) VR=SQ VM=1 Basic Grayscale Image Sequence</summary>
        public readonly static DicomTagSQ BasicGrayscaleImageSequence = new DicomTagSQ(0x2020, 0x0110);

        ///<summary>(2020,0111) VR=SQ VM=1 Basic Color Image Sequence</summary>
        public readonly static DicomTagSQ BasicColorImageSequence = new DicomTagSQ(0x2020, 0x0111);

        ///<summary>(2020,0130) VR=SQ VM=1 Referenced Image Overlay Box Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedImageOverlayBoxSequenceRETIRED = new DicomTagSQ(0x2020, 0x0130);

        ///<summary>(2020,0140) VR=SQ VM=1 Referenced VOI LUT Box Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedVOILUTBoxSequenceRETIRED = new DicomTagSQ(0x2020, 0x0140);

        ///<summary>(2030,0010) VR=US VM=1 Annotation Position</summary>
        public readonly static DicomTagUS AnnotationPosition = new DicomTagUS(0x2030, 0x0010);

        ///<summary>(2030,0020) VR=LO VM=1 Text String</summary>
        public readonly static DicomTagLO TextString = new DicomTagLO(0x2030, 0x0020);

        ///<summary>(2040,0010) VR=SQ VM=1 Referenced Overlay Plane Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedOverlayPlaneSequenceRETIRED = new DicomTagSQ(0x2040, 0x0010);

        ///<summary>(2040,0011) VR=US VM=1-99 Referenced Overlay Plane Groups (RETIRED)</summary>
        public readonly static DicomTagUSs ReferencedOverlayPlaneGroupsRETIRED = new DicomTagUSs(0x2040, 0x0011);

        ///<summary>(2040,0020) VR=SQ VM=1 Overlay Pixel Data Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ OverlayPixelDataSequenceRETIRED = new DicomTagSQ(0x2040, 0x0020);

        ///<summary>(2040,0060) VR=CS VM=1 Overlay Magnification Type (RETIRED)</summary>
        public readonly static DicomTagCS OverlayMagnificationTypeRETIRED = new DicomTagCS(0x2040, 0x0060);

        ///<summary>(2040,0070) VR=CS VM=1 Overlay Smoothing Type (RETIRED)</summary>
        public readonly static DicomTagCS OverlaySmoothingTypeRETIRED = new DicomTagCS(0x2040, 0x0070);

        ///<summary>(2040,0072) VR=CS VM=1 Overlay or Image Magnification (RETIRED)</summary>
        public readonly static DicomTagCS OverlayOrImageMagnificationRETIRED = new DicomTagCS(0x2040, 0x0072);

        ///<summary>(2040,0074) VR=US VM=1 Magnify to Number of Columns (RETIRED)</summary>
        public readonly static DicomTagUS MagnifyToNumberOfColumnsRETIRED = new DicomTagUS(0x2040, 0x0074);

        ///<summary>(2040,0080) VR=CS VM=1 Overlay Foreground Density (RETIRED)</summary>
        public readonly static DicomTagCS OverlayForegroundDensityRETIRED = new DicomTagCS(0x2040, 0x0080);

        ///<summary>(2040,0082) VR=CS VM=1 Overlay Background Density (RETIRED)</summary>
        public readonly static DicomTagCS OverlayBackgroundDensityRETIRED = new DicomTagCS(0x2040, 0x0082);

        ///<summary>(2040,0090) VR=CS VM=1 Overlay Mode (RETIRED)</summary>
        public readonly static DicomTagCS OverlayModeRETIRED = new DicomTagCS(0x2040, 0x0090);

        ///<summary>(2040,0100) VR=CS VM=1 Threshold Density (RETIRED)</summary>
        public readonly static DicomTagCS ThresholdDensityRETIRED = new DicomTagCS(0x2040, 0x0100);

        ///<summary>(2040,0500) VR=SQ VM=1 Referenced Image Box Sequence (Retired) (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedImageBoxSequenceRetiredRETIRED = new DicomTagSQ(0x2040, 0x0500);

        ///<summary>(2050,0010) VR=SQ VM=1 Presentation LUT Sequence</summary>
        public readonly static DicomTagSQ PresentationLUTSequence = new DicomTagSQ(0x2050, 0x0010);

        ///<summary>(2050,0020) VR=CS VM=1 Presentation LUT Shape</summary>
        public readonly static DicomTagCS PresentationLUTShape = new DicomTagCS(0x2050, 0x0020);

        ///<summary>(2050,0500) VR=SQ VM=1 Referenced Presentation LUT Sequence</summary>
        public readonly static DicomTagSQ ReferencedPresentationLUTSequence = new DicomTagSQ(0x2050, 0x0500);

        ///<summary>(2100,0010) VR=SH VM=1 Print Job ID (RETIRED)</summary>
        public readonly static DicomTagSH PrintJobIDRETIRED = new DicomTagSH(0x2100, 0x0010);

        ///<summary>(2100,0020) VR=CS VM=1 Execution Status</summary>
        public readonly static DicomTagCS ExecutionStatus = new DicomTagCS(0x2100, 0x0020);

        ///<summary>(2100,0030) VR=CS VM=1 Execution Status Info</summary>
        public readonly static DicomTagCS ExecutionStatusInfo = new DicomTagCS(0x2100, 0x0030);

        ///<summary>(2100,0040) VR=DA VM=1 Creation Date</summary>
        public readonly static DicomTagDA CreationDate = new DicomTagDA(0x2100, 0x0040);

        ///<summary>(2100,0050) VR=TM VM=1 Creation Time</summary>
        public readonly static DicomTagTM CreationTime = new DicomTagTM(0x2100, 0x0050);

        ///<summary>(2100,0070) VR=AE VM=1 Originator</summary>
        public readonly static DicomTagAE Originator = new DicomTagAE(0x2100, 0x0070);

        ///<summary>(2100,0140) VR=AE VM=1 Destination AE</summary>
        public readonly static DicomTagAE DestinationAE = new DicomTagAE(0x2100, 0x0140);

        ///<summary>(2100,0160) VR=SH VM=1 Owner ID</summary>
        public readonly static DicomTagSH OwnerID = new DicomTagSH(0x2100, 0x0160);

        ///<summary>(2100,0170) VR=IS VM=1 Number of Films</summary>
        public readonly static DicomTagIS NumberOfFilms = new DicomTagIS(0x2100, 0x0170);

        ///<summary>(2100,0500) VR=SQ VM=1 Referenced Print Job Sequence (Pull Stored Print) (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedPrintJobSequencePullStoredPrintRETIRED = new DicomTagSQ(0x2100, 0x0500);

        ///<summary>(2110,0010) VR=CS VM=1 Printer Status</summary>
        public readonly static DicomTagCS PrinterStatus = new DicomTagCS(0x2110, 0x0010);

        ///<summary>(2110,0020) VR=CS VM=1 Printer Status Info</summary>
        public readonly static DicomTagCS PrinterStatusInfo = new DicomTagCS(0x2110, 0x0020);

        ///<summary>(2110,0030) VR=LO VM=1 Printer Name</summary>
        public readonly static DicomTagLO PrinterName = new DicomTagLO(0x2110, 0x0030);

        ///<summary>(2110,0099) VR=SH VM=1 Print Queue ID (RETIRED)</summary>
        public readonly static DicomTagSH PrintQueueIDRETIRED = new DicomTagSH(0x2110, 0x0099);

        ///<summary>(2120,0010) VR=CS VM=1 Queue Status (RETIRED)</summary>
        public readonly static DicomTagCS QueueStatusRETIRED = new DicomTagCS(0x2120, 0x0010);

        ///<summary>(2120,0050) VR=SQ VM=1 Print Job Description Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ PrintJobDescriptionSequenceRETIRED = new DicomTagSQ(0x2120, 0x0050);

        ///<summary>(2120,0070) VR=SQ VM=1 Referenced Print Job Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedPrintJobSequenceRETIRED = new DicomTagSQ(0x2120, 0x0070);

        ///<summary>(2130,0010) VR=SQ VM=1 Print Management Capabilities Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ PrintManagementCapabilitiesSequenceRETIRED = new DicomTagSQ(0x2130, 0x0010);

        ///<summary>(2130,0015) VR=SQ VM=1 Printer Characteristics Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ PrinterCharacteristicsSequenceRETIRED = new DicomTagSQ(0x2130, 0x0015);

        ///<summary>(2130,0030) VR=SQ VM=1 Film Box Content Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ FilmBoxContentSequenceRETIRED = new DicomTagSQ(0x2130, 0x0030);

        ///<summary>(2130,0040) VR=SQ VM=1 Image Box Content Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ImageBoxContentSequenceRETIRED = new DicomTagSQ(0x2130, 0x0040);

        ///<summary>(2130,0050) VR=SQ VM=1 Annotation Content Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ AnnotationContentSequenceRETIRED = new DicomTagSQ(0x2130, 0x0050);

        ///<summary>(2130,0060) VR=SQ VM=1 Image Overlay Box Content Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ImageOverlayBoxContentSequenceRETIRED = new DicomTagSQ(0x2130, 0x0060);

        ///<summary>(2130,0080) VR=SQ VM=1 Presentation LUT Content Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ PresentationLUTContentSequenceRETIRED = new DicomTagSQ(0x2130, 0x0080);

        ///<summary>(2130,00A0) VR=SQ VM=1 Proposed Study Sequence</summary>
        public readonly static DicomTagSQ ProposedStudySequence = new DicomTagSQ(0x2130, 0x00A0);

        ///<summary>(2130,00C0) VR=SQ VM=1 Original Image Sequence</summary>
        public readonly static DicomTagSQ OriginalImageSequence = new DicomTagSQ(0x2130, 0x00C0);

        ///<summary>(2200,0001) VR=CS VM=1 Label Using Information Extracted From Instances</summary>
        public readonly static DicomTagCS LabelUsingInformationExtractedFromInstances = new DicomTagCS(0x2200, 0x0001);

        ///<summary>(2200,0002) VR=UT VM=1 Label Text</summary>
        public readonly static DicomTagUT LabelText = new DicomTagUT(0x2200, 0x0002);

        ///<summary>(2200,0003) VR=CS VM=1 Label Style Selection</summary>
        public readonly static DicomTagCS LabelStyleSelection = new DicomTagCS(0x2200, 0x0003);

        ///<summary>(2200,0004) VR=LT VM=1 Media Disposition</summary>
        public readonly static DicomTagLT MediaDisposition = new DicomTagLT(0x2200, 0x0004);

        ///<summary>(2200,0005) VR=LT VM=1 Barcode Value</summary>
        public readonly static DicomTagLT BarcodeValue = new DicomTagLT(0x2200, 0x0005);

        ///<summary>(2200,0006) VR=CS VM=1 Barcode Symbology</summary>
        public readonly static DicomTagCS BarcodeSymbology = new DicomTagCS(0x2200, 0x0006);

        ///<summary>(2200,0007) VR=CS VM=1 Allow Media Splitting</summary>
        public readonly static DicomTagCS AllowMediaSplitting = new DicomTagCS(0x2200, 0x0007);

        ///<summary>(2200,0008) VR=CS VM=1 Include Non-DICOM Objects</summary>
        public readonly static DicomTagCS IncludeNonDICOMObjects = new DicomTagCS(0x2200, 0x0008);

        ///<summary>(2200,0009) VR=CS VM=1 Include Display Application</summary>
        public readonly static DicomTagCS IncludeDisplayApplication = new DicomTagCS(0x2200, 0x0009);

        ///<summary>(2200,000A) VR=CS VM=1 Preserve Composite Instances After Media Creation</summary>
        public readonly static DicomTagCS PreserveCompositeInstancesAfterMediaCreation = new DicomTagCS(0x2200, 0x000A);

        ///<summary>(2200,000B) VR=US VM=1 Total Number of Pieces of Media Created</summary>
        public readonly static DicomTagUS TotalNumberOfPiecesOfMediaCreated = new DicomTagUS(0x2200, 0x000B);

        ///<summary>(2200,000C) VR=LO VM=1 Requested Media Application Profile</summary>
        public readonly static DicomTagLO RequestedMediaApplicationProfile = new DicomTagLO(0x2200, 0x000C);

        ///<summary>(2200,000D) VR=SQ VM=1 Referenced Storage Media Sequence</summary>
        public readonly static DicomTagSQ ReferencedStorageMediaSequence = new DicomTagSQ(0x2200, 0x000D);

        ///<summary>(2200,000E) VR=AT VM=1-n Failure Attributes</summary>
        public readonly static DicomTagATs FailureAttributes = new DicomTagATs(0x2200, 0x000E);

        ///<summary>(2200,000F) VR=CS VM=1 Allow Lossy Compression</summary>
        public readonly static DicomTagCS AllowLossyCompression = new DicomTagCS(0x2200, 0x000F);

        ///<summary>(2200,0020) VR=CS VM=1 Request Priority</summary>
        public readonly static DicomTagCS RequestPriority = new DicomTagCS(0x2200, 0x0020);

        ///<summary>(3002,0002) VR=SH VM=1 RT Image Label</summary>
        public readonly static DicomTagSH RTImageLabel = new DicomTagSH(0x3002, 0x0002);

        ///<summary>(3002,0003) VR=LO VM=1 RT Image Name</summary>
        public readonly static DicomTagLO RTImageName = new DicomTagLO(0x3002, 0x0003);

        ///<summary>(3002,0004) VR=ST VM=1 RT Image Description</summary>
        public readonly static DicomTagST RTImageDescription = new DicomTagST(0x3002, 0x0004);

        ///<summary>(3002,000A) VR=CS VM=1 Reported Values Origin</summary>
        public readonly static DicomTagCS ReportedValuesOrigin = new DicomTagCS(0x3002, 0x000A);

        ///<summary>(3002,000C) VR=CS VM=1 RT Image Plane</summary>
        public readonly static DicomTagCS RTImagePlane = new DicomTagCS(0x3002, 0x000C);

        ///<summary>(3002,000D) VR=DS VM=3 X-Ray Image Receptor Translation</summary>
        public readonly static DicomTagDSs XRayImageReceptorTranslation = new DicomTagDSs(0x3002, 0x000D);

        ///<summary>(3002,000E) VR=DS VM=1 X-Ray Image Receptor Angle</summary>
        public readonly static DicomTagDS XRayImageReceptorAngle = new DicomTagDS(0x3002, 0x000E);

        ///<summary>(3002,0010) VR=DS VM=6 RT Image Orientation</summary>
        public readonly static DicomTagDSs RTImageOrientation = new DicomTagDSs(0x3002, 0x0010);

        ///<summary>(3002,0011) VR=DS VM=2 Image Plane Pixel Spacing</summary>
        public readonly static DicomTagDSs ImagePlanePixelSpacing = new DicomTagDSs(0x3002, 0x0011);

        ///<summary>(3002,0012) VR=DS VM=2 RT Image Position</summary>
        public readonly static DicomTagDSs RTImagePosition = new DicomTagDSs(0x3002, 0x0012);

        ///<summary>(3002,0020) VR=SH VM=1 Radiation Machine Name</summary>
        public readonly static DicomTagSH RadiationMachineName = new DicomTagSH(0x3002, 0x0020);

        ///<summary>(3002,0022) VR=DS VM=1 Radiation Machine SAD</summary>
        public readonly static DicomTagDS RadiationMachineSAD = new DicomTagDS(0x3002, 0x0022);

        ///<summary>(3002,0024) VR=DS VM=1 Radiation Machine SSD</summary>
        public readonly static DicomTagDS RadiationMachineSSD = new DicomTagDS(0x3002, 0x0024);

        ///<summary>(3002,0026) VR=DS VM=1 RT Image SID</summary>
        public readonly static DicomTagDS RTImageSID = new DicomTagDS(0x3002, 0x0026);

        ///<summary>(3002,0028) VR=DS VM=1 Source to Reference Object Distance</summary>
        public readonly static DicomTagDS SourceToReferenceObjectDistance = new DicomTagDS(0x3002, 0x0028);

        ///<summary>(3002,0029) VR=IS VM=1 Fraction Number</summary>
        public readonly static DicomTagIS FractionNumber = new DicomTagIS(0x3002, 0x0029);

        ///<summary>(3002,0030) VR=SQ VM=1 Exposure Sequence</summary>
        public readonly static DicomTagSQ ExposureSequence = new DicomTagSQ(0x3002, 0x0030);

        ///<summary>(3002,0032) VR=DS VM=1 Meterset Exposure</summary>
        public readonly static DicomTagDS MetersetExposure = new DicomTagDS(0x3002, 0x0032);

        ///<summary>(3002,0034) VR=DS VM=4 Diaphragm Position</summary>
        public readonly static DicomTagDSs DiaphragmPosition = new DicomTagDSs(0x3002, 0x0034);

        ///<summary>(3002,0040) VR=SQ VM=1 Fluence Map Sequence</summary>
        public readonly static DicomTagSQ FluenceMapSequence = new DicomTagSQ(0x3002, 0x0040);

        ///<summary>(3002,0041) VR=CS VM=1 Fluence Data Source</summary>
        public readonly static DicomTagCS FluenceDataSource = new DicomTagCS(0x3002, 0x0041);

        ///<summary>(3002,0042) VR=DS VM=1 Fluence Data Scale</summary>
        public readonly static DicomTagDS FluenceDataScale = new DicomTagDS(0x3002, 0x0042);

        ///<summary>(3002,0050) VR=SQ VM=1 Primary Fluence Mode Sequence</summary>
        public readonly static DicomTagSQ PrimaryFluenceModeSequence = new DicomTagSQ(0x3002, 0x0050);

        ///<summary>(3002,0051) VR=CS VM=1 Fluence Mode</summary>
        public readonly static DicomTagCS FluenceMode = new DicomTagCS(0x3002, 0x0051);

        ///<summary>(3002,0052) VR=SH VM=1 Fluence Mode ID</summary>
        public readonly static DicomTagSH FluenceModeID = new DicomTagSH(0x3002, 0x0052);

        ///<summary>(3002,0100) VR=IS VM=1 Selected Frame Number</summary>
        public readonly static DicomTagIS SelectedFrameNumber = new DicomTagIS(0x3002, 0x0100);

        ///<summary>(3002,0101) VR=SQ VM=1 Selected Frame Functional Groups Sequence</summary>
        public readonly static DicomTagSQ SelectedFrameFunctionalGroupsSequence = new DicomTagSQ(0x3002, 0x0101);

        ///<summary>(3002,0102) VR=SQ VM=1 RT Image Frame General Content Sequence</summary>
        public readonly static DicomTagSQ RTImageFrameGeneralContentSequence = new DicomTagSQ(0x3002, 0x0102);

        ///<summary>(3002,0103) VR=SQ VM=1 RT Image Frame Context Sequence</summary>
        public readonly static DicomTagSQ RTImageFrameContextSequence = new DicomTagSQ(0x3002, 0x0103);

        ///<summary>(3002,0104) VR=SQ VM=1 RT Image Scope Sequence</summary>
        public readonly static DicomTagSQ RTImageScopeSequence = new DicomTagSQ(0x3002, 0x0104);

        ///<summary>(3002,0105) VR=CS VM=1 Beam Modifier Coordinates Presence Flag</summary>
        public readonly static DicomTagCS BeamModifierCoordinatesPresenceFlag = new DicomTagCS(0x3002, 0x0105);

        ///<summary>(3002,0106) VR=FD VM=1 Start Cumulative Meterset</summary>
        public readonly static DicomTagFD StartCumulativeMeterset = new DicomTagFD(0x3002, 0x0106);

        ///<summary>(3002,0107) VR=FD VM=1 Stop Cumulative Meterset</summary>
        public readonly static DicomTagFD StopCumulativeMeterset = new DicomTagFD(0x3002, 0x0107);

        ///<summary>(3002,0108) VR=SQ VM=1 RT Acquisition Patient Position Sequence</summary>
        public readonly static DicomTagSQ RTAcquisitionPatientPositionSequence = new DicomTagSQ(0x3002, 0x0108);

        ///<summary>(3002,0109) VR=SQ VM=1 RT Image Frame Imaging Device Position Sequence</summary>
        public readonly static DicomTagSQ RTImageFrameImagingDevicePositionSequence = new DicomTagSQ(0x3002, 0x0109);

        ///<summary>(3002,010A) VR=SQ VM=1 RT Image Frame kV Radiation Acquisition Sequence</summary>
        public readonly static DicomTagSQ RTImageFramekVRadiationAcquisitionSequence = new DicomTagSQ(0x3002, 0x010A);

        ///<summary>(3002,010B) VR=SQ VM=1 RT Image Frame MV Radiation Acquisition Sequence</summary>
        public readonly static DicomTagSQ RTImageFrameMVRadiationAcquisitionSequence = new DicomTagSQ(0x3002, 0x010B);

        ///<summary>(3002,010C) VR=SQ VM=1 RT Image Frame Radiation Acquisition Sequence</summary>
        public readonly static DicomTagSQ RTImageFrameRadiationAcquisitionSequence = new DicomTagSQ(0x3002, 0x010C);

        ///<summary>(3002,010D) VR=SQ VM=1 Imaging Source Position Sequence</summary>
        public readonly static DicomTagSQ ImagingSourcePositionSequence = new DicomTagSQ(0x3002, 0x010D);

        ///<summary>(3002,010E) VR=SQ VM=1 Image Receptor Position Sequence</summary>
        public readonly static DicomTagSQ ImageReceptorPositionSequence = new DicomTagSQ(0x3002, 0x010E);

        ///<summary>(3002,010F) VR=FD VM=16 Device Position to Equipment Mapping Matrix</summary>
        public readonly static DicomTagFDs DevicePositionToEquipmentMappingMatrix = new DicomTagFDs(0x3002, 0x010F);

        ///<summary>(3002,0110) VR=SQ VM=1 Device Position Parameter Sequence</summary>
        public readonly static DicomTagSQ DevicePositionParameterSequence = new DicomTagSQ(0x3002, 0x0110);

        ///<summary>(3002,0111) VR=CS VM=1 Imaging Source Location Specification Type</summary>
        public readonly static DicomTagCS ImagingSourceLocationSpecificationType = new DicomTagCS(0x3002, 0x0111);

        ///<summary>(3002,0112) VR=SQ VM=1 Imaging Device Location Matrix Sequence</summary>
        public readonly static DicomTagSQ ImagingDeviceLocationMatrixSequence = new DicomTagSQ(0x3002, 0x0112);

        ///<summary>(3002,0113) VR=SQ VM=1 Imaging Device Location Parameter Sequence</summary>
        public readonly static DicomTagSQ ImagingDeviceLocationParameterSequence = new DicomTagSQ(0x3002, 0x0113);

        ///<summary>(3002,0114) VR=SQ VM=1 Imaging Aperture Sequence</summary>
        public readonly static DicomTagSQ ImagingApertureSequence = new DicomTagSQ(0x3002, 0x0114);

        ///<summary>(3002,0115) VR=CS VM=1 Imaging Aperture Specification Type</summary>
        public readonly static DicomTagCS ImagingApertureSpecificationType = new DicomTagCS(0x3002, 0x0115);

        ///<summary>(3002,0116) VR=US VM=1 Number of Acquisition Devices</summary>
        public readonly static DicomTagUS NumberOfAcquisitionDevices = new DicomTagUS(0x3002, 0x0116);

        ///<summary>(3002,0117) VR=SQ VM=1 Acquisition Device Sequence</summary>
        public readonly static DicomTagSQ AcquisitionDeviceSequence = new DicomTagSQ(0x3002, 0x0117);

        ///<summary>(3002,0118) VR=SQ VM=1 Acquisition Task Sequence</summary>
        public readonly static DicomTagSQ AcquisitionTaskSequence = new DicomTagSQ(0x3002, 0x0118);

        ///<summary>(3002,0119) VR=SQ VM=1 Acquisition Task Workitem Code Sequence</summary>
        public readonly static DicomTagSQ AcquisitionTaskWorkitemCodeSequence = new DicomTagSQ(0x3002, 0x0119);

        ///<summary>(3002,011A) VR=SQ VM=1 Acquisition Subtask Sequence</summary>
        public readonly static DicomTagSQ AcquisitionSubtaskSequence = new DicomTagSQ(0x3002, 0x011A);

        ///<summary>(3002,011B) VR=SQ VM=1 Subtask Workitem Code Sequence</summary>
        public readonly static DicomTagSQ SubtaskWorkitemCodeSequence = new DicomTagSQ(0x3002, 0x011B);

        ///<summary>(3002,011C) VR=US VM=1 Acquisition Task Index</summary>
        public readonly static DicomTagUS AcquisitionTaskIndex = new DicomTagUS(0x3002, 0x011C);

        ///<summary>(3002,011D) VR=US VM=1 Acquisition Subtask Index</summary>
        public readonly static DicomTagUS AcquisitionSubtaskIndex = new DicomTagUS(0x3002, 0x011D);

        ///<summary>(3002,011E) VR=SQ VM=1 Referenced Baseline Parameters RT Radiation Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedBaselineParametersRTRadiationInstanceSequence = new DicomTagSQ(0x3002, 0x011E);

        ///<summary>(3002,011F) VR=SQ VM=1 Position Acquisition Template Identification Sequence</summary>
        public readonly static DicomTagSQ PositionAcquisitionTemplateIdentificationSequence = new DicomTagSQ(0x3002, 0x011F);

        ///<summary>(3002,0120) VR=ST VM=1 Position Acquisition Template ID</summary>
        public readonly static DicomTagST PositionAcquisitionTemplateID = new DicomTagST(0x3002, 0x0120);

        ///<summary>(3002,0121) VR=LO VM=1 Position Acquisition Template Name</summary>
        public readonly static DicomTagLO PositionAcquisitionTemplateName = new DicomTagLO(0x3002, 0x0121);

        ///<summary>(3002,0122) VR=SQ VM=1 Position Acquisition Template Code Sequence</summary>
        public readonly static DicomTagSQ PositionAcquisitionTemplateCodeSequence = new DicomTagSQ(0x3002, 0x0122);

        ///<summary>(3002,0123) VR=LT VM=1 Position Acquisition Template Description</summary>
        public readonly static DicomTagLT PositionAcquisitionTemplateDescription = new DicomTagLT(0x3002, 0x0123);

        ///<summary>(3002,0124) VR=SQ VM=1 Acquisition Task Applicability Sequence</summary>
        public readonly static DicomTagSQ AcquisitionTaskApplicabilitySequence = new DicomTagSQ(0x3002, 0x0124);

        ///<summary>(3002,0125) VR=SQ VM=1 Projection Imaging Acquisition Parameter Sequence</summary>
        public readonly static DicomTagSQ ProjectionImagingAcquisitionParameterSequence = new DicomTagSQ(0x3002, 0x0125);

        ///<summary>(3002,0126) VR=SQ VM=1 CT Imaging Acquisition Parameter Sequence</summary>
        public readonly static DicomTagSQ CTImagingAcquisitionParameterSequence = new DicomTagSQ(0x3002, 0x0126);

        ///<summary>(3002,0127) VR=SQ VM=1 KV Imaging Generation Parameters Sequence</summary>
        public readonly static DicomTagSQ KVImagingGenerationParametersSequence = new DicomTagSQ(0x3002, 0x0127);

        ///<summary>(3002,0128) VR=SQ VM=1 MV Imaging Generation Parameters Sequence</summary>
        public readonly static DicomTagSQ MVImagingGenerationParametersSequence = new DicomTagSQ(0x3002, 0x0128);

        ///<summary>(3002,0129) VR=CS VM=1 Acquisition Signal Type</summary>
        public readonly static DicomTagCS AcquisitionSignalType = new DicomTagCS(0x3002, 0x0129);

        ///<summary>(3002,012A) VR=CS VM=1 Acquisition Method</summary>
        public readonly static DicomTagCS AcquisitionMethod = new DicomTagCS(0x3002, 0x012A);

        ///<summary>(3002,012B) VR=SQ VM=1 Scan Start Position Sequence</summary>
        public readonly static DicomTagSQ ScanStartPositionSequence = new DicomTagSQ(0x3002, 0x012B);

        ///<summary>(3002,012C) VR=SQ VM=1 Scan Stop Position Sequence</summary>
        public readonly static DicomTagSQ ScanStopPositionSequence = new DicomTagSQ(0x3002, 0x012C);

        ///<summary>(3002,012D) VR=FD VM=1 Imaging Source to Beam Modifier Definition Plane Distance</summary>
        public readonly static DicomTagFD ImagingSourceToBeamModifierDefinitionPlaneDistance = new DicomTagFD(0x3002, 0x012D);

        ///<summary>(3002,012E) VR=CS VM=1 Scan Arc Type</summary>
        public readonly static DicomTagCS ScanArcType = new DicomTagCS(0x3002, 0x012E);

        ///<summary>(3002,012F) VR=CS VM=1 Detector Positioning Type</summary>
        public readonly static DicomTagCS DetectorPositioningType = new DicomTagCS(0x3002, 0x012F);

        ///<summary>(3002,0130) VR=SQ VM=1 Additional RT Accessory Device Sequence</summary>
        public readonly static DicomTagSQ AdditionalRTAccessoryDeviceSequence = new DicomTagSQ(0x3002, 0x0130);

        ///<summary>(3002,0131) VR=SQ VM=1 Device-Specific Acquisition Parameter Sequence</summary>
        public readonly static DicomTagSQ DeviceSpecificAcquisitionParameterSequence = new DicomTagSQ(0x3002, 0x0131);

        ///<summary>(3002,0132) VR=SQ VM=1 Referenced Position Reference Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedPositionReferenceInstanceSequence = new DicomTagSQ(0x3002, 0x0132);

        ///<summary>(3002,0133) VR=SQ VM=1 Energy Derivation Code Sequence</summary>
        public readonly static DicomTagSQ EnergyDerivationCodeSequence = new DicomTagSQ(0x3002, 0x0133);

        ///<summary>(3002,0134) VR=FD VM=1 Maximum Cumulative Meterset Exposure</summary>
        public readonly static DicomTagFD MaximumCumulativeMetersetExposure = new DicomTagFD(0x3002, 0x0134);

        ///<summary>(3002,0135) VR=SQ VM=1 Acquisition Initiation Sequence</summary>
        public readonly static DicomTagSQ AcquisitionInitiationSequence = new DicomTagSQ(0x3002, 0x0135);

        ///<summary>(3002,0136) VR=SQ VM=1 RT Cone-Beam Imaging Geometry Sequence</summary>
        public readonly static DicomTagSQ RTConeBeamImagingGeometrySequence = new DicomTagSQ(0x3002, 0x0136);

        ///<summary>(3004,0001) VR=CS VM=1 DVH Type</summary>
        public readonly static DicomTagCS DVHType = new DicomTagCS(0x3004, 0x0001);

        ///<summary>(3004,0002) VR=CS VM=1 Dose Units</summary>
        public readonly static DicomTagCS DoseUnits = new DicomTagCS(0x3004, 0x0002);

        ///<summary>(3004,0004) VR=CS VM=1 Dose Type</summary>
        public readonly static DicomTagCS DoseType = new DicomTagCS(0x3004, 0x0004);

        ///<summary>(3004,0005) VR=CS VM=1 Spatial Transform of Dose</summary>
        public readonly static DicomTagCS SpatialTransformOfDose = new DicomTagCS(0x3004, 0x0005);

        ///<summary>(3004,0006) VR=LO VM=1 Dose Comment</summary>
        public readonly static DicomTagLO DoseComment = new DicomTagLO(0x3004, 0x0006);

        ///<summary>(3004,0008) VR=DS VM=3 Normalization Point</summary>
        public readonly static DicomTagDSs NormalizationPoint = new DicomTagDSs(0x3004, 0x0008);

        ///<summary>(3004,000A) VR=CS VM=1 Dose Summation Type</summary>
        public readonly static DicomTagCS DoseSummationType = new DicomTagCS(0x3004, 0x000A);

        ///<summary>(3004,000C) VR=DS VM=2-n Grid Frame Offset Vector</summary>
        public readonly static DicomTagDSs GridFrameOffsetVector = new DicomTagDSs(0x3004, 0x000C);

        ///<summary>(3004,000E) VR=DS VM=1 Dose Grid Scaling</summary>
        public readonly static DicomTagDS DoseGridScaling = new DicomTagDS(0x3004, 0x000E);

        ///<summary>(3004,0010) VR=SQ VM=1 RT Dose ROI Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ RTDoseROISequenceRETIRED = new DicomTagSQ(0x3004, 0x0010);

        ///<summary>(3004,0012) VR=DS VM=1 Dose Value</summary>
        public readonly static DicomTagDS DoseValue = new DicomTagDS(0x3004, 0x0012);

        ///<summary>(3004,0014) VR=CS VM=1-3 Tissue Heterogeneity Correction</summary>
        public readonly static DicomTagCSs TissueHeterogeneityCorrection = new DicomTagCSs(0x3004, 0x0014);

        ///<summary>(3004,0016) VR=SQ VM=1 Recommended Isodose Level Sequence</summary>
        public readonly static DicomTagSQ RecommendedIsodoseLevelSequence = new DicomTagSQ(0x3004, 0x0016);

        ///<summary>(3004,0020) VR=SQ VM=1 Dose Unit Code Sequence</summary>
        public readonly static DicomTagSQ DoseUnitCodeSequence = new DicomTagSQ(0x3004, 0x0020);

        ///<summary>(3004,0021) VR=SQ VM=1 RT Dose Interpreted Type Code Sequence</summary>
        public readonly static DicomTagSQ RTDoseInterpretedTypeCodeSequence = new DicomTagSQ(0x3004, 0x0021);

        ///<summary>(3004,0022) VR=SQ VM=1 RT Dose Interpreted Type Code Modifier Sequence</summary>
        public readonly static DicomTagSQ RTDoseInterpretedTypeCodeModifierSequence = new DicomTagSQ(0x3004, 0x0022);

        ///<summary>(3004,0023) VR=SQ VM=1 Dose Radiobiological Interpretation Sequence</summary>
        public readonly static DicomTagSQ DoseRadiobiologicalInterpretationSequence = new DicomTagSQ(0x3004, 0x0023);

        ///<summary>(3004,0024) VR=SQ VM=1 RT Dose Intent Code Sequence</summary>
        public readonly static DicomTagSQ RTDoseIntentCodeSequence = new DicomTagSQ(0x3004, 0x0024);

        ///<summary>(3004,0040) VR=DS VM=3 DVH Normalization Point</summary>
        public readonly static DicomTagDSs DVHNormalizationPoint = new DicomTagDSs(0x3004, 0x0040);

        ///<summary>(3004,0042) VR=DS VM=1 DVH Normalization Dose Value</summary>
        public readonly static DicomTagDS DVHNormalizationDoseValue = new DicomTagDS(0x3004, 0x0042);

        ///<summary>(3004,0050) VR=SQ VM=1 DVH Sequence</summary>
        public readonly static DicomTagSQ DVHSequence = new DicomTagSQ(0x3004, 0x0050);

        ///<summary>(3004,0052) VR=DS VM=1 DVH Dose Scaling</summary>
        public readonly static DicomTagDS DVHDoseScaling = new DicomTagDS(0x3004, 0x0052);

        ///<summary>(3004,0054) VR=CS VM=1 DVH Volume Units</summary>
        public readonly static DicomTagCS DVHVolumeUnits = new DicomTagCS(0x3004, 0x0054);

        ///<summary>(3004,0056) VR=IS VM=1 DVH Number of Bins</summary>
        public readonly static DicomTagIS DVHNumberOfBins = new DicomTagIS(0x3004, 0x0056);

        ///<summary>(3004,0058) VR=DS VM=2-2n DVH Data</summary>
        public readonly static DicomTagDSs DVHData = new DicomTagDSs(0x3004, 0x0058);

        ///<summary>(3004,0060) VR=SQ VM=1 DVH Referenced ROI Sequence</summary>
        public readonly static DicomTagSQ DVHReferencedROISequence = new DicomTagSQ(0x3004, 0x0060);

        ///<summary>(3004,0062) VR=CS VM=1 DVH ROI Contribution Type</summary>
        public readonly static DicomTagCS DVHROIContributionType = new DicomTagCS(0x3004, 0x0062);

        ///<summary>(3004,0070) VR=DS VM=1 DVH Minimum Dose</summary>
        public readonly static DicomTagDS DVHMinimumDose = new DicomTagDS(0x3004, 0x0070);

        ///<summary>(3004,0072) VR=DS VM=1 DVH Maximum Dose</summary>
        public readonly static DicomTagDS DVHMaximumDose = new DicomTagDS(0x3004, 0x0072);

        ///<summary>(3004,0074) VR=DS VM=1 DVH Mean Dose</summary>
        public readonly static DicomTagDS DVHMeanDose = new DicomTagDS(0x3004, 0x0074);

        ///<summary>(3004,0080) VR=SQ VM=1 Dose Calculation Model Sequence</summary>
        public readonly static DicomTagSQ DoseCalculationModelSequence = new DicomTagSQ(0x3004, 0x0080);

        ///<summary>(3004,0081) VR=SQ VM=1 Dose Calculation Algorithm Sequence</summary>
        public readonly static DicomTagSQ DoseCalculationAlgorithmSequence = new DicomTagSQ(0x3004, 0x0081);

        ///<summary>(3004,0082) VR=CS VM=1 Commissioning Status</summary>
        public readonly static DicomTagCS CommissioningStatus = new DicomTagCS(0x3004, 0x0082);

        ///<summary>(3004,0083) VR=SQ VM=1 Dose Calculation Model Parameter Sequence</summary>
        public readonly static DicomTagSQ DoseCalculationModelParameterSequence = new DicomTagSQ(0x3004, 0x0083);

        ///<summary>(3004,0084) VR=CS VM=1 Dose Deposition Calculation Medium</summary>
        public readonly static DicomTagCS DoseDepositionCalculationMedium = new DicomTagCS(0x3004, 0x0084);

        ///<summary>(3006,0002) VR=SH VM=1 Structure Set Label</summary>
        public readonly static DicomTagSH StructureSetLabel = new DicomTagSH(0x3006, 0x0002);

        ///<summary>(3006,0004) VR=LO VM=1 Structure Set Name</summary>
        public readonly static DicomTagLO StructureSetName = new DicomTagLO(0x3006, 0x0004);

        ///<summary>(3006,0006) VR=ST VM=1 Structure Set Description</summary>
        public readonly static DicomTagST StructureSetDescription = new DicomTagST(0x3006, 0x0006);

        ///<summary>(3006,0008) VR=DA VM=1 Structure Set Date</summary>
        public readonly static DicomTagDA StructureSetDate = new DicomTagDA(0x3006, 0x0008);

        ///<summary>(3006,0009) VR=TM VM=1 Structure Set Time</summary>
        public readonly static DicomTagTM StructureSetTime = new DicomTagTM(0x3006, 0x0009);

        ///<summary>(3006,0010) VR=SQ VM=1 Referenced Frame of Reference Sequence</summary>
        public readonly static DicomTagSQ ReferencedFrameOfReferenceSequence = new DicomTagSQ(0x3006, 0x0010);

        ///<summary>(3006,0012) VR=SQ VM=1 RT Referenced Study Sequence</summary>
        public readonly static DicomTagSQ RTReferencedStudySequence = new DicomTagSQ(0x3006, 0x0012);

        ///<summary>(3006,0014) VR=SQ VM=1 RT Referenced Series Sequence</summary>
        public readonly static DicomTagSQ RTReferencedSeriesSequence = new DicomTagSQ(0x3006, 0x0014);

        ///<summary>(3006,0016) VR=SQ VM=1 Contour Image Sequence</summary>
        public readonly static DicomTagSQ ContourImageSequence = new DicomTagSQ(0x3006, 0x0016);

        ///<summary>(3006,0018) VR=SQ VM=1 Predecessor Structure Set Sequence</summary>
        public readonly static DicomTagSQ PredecessorStructureSetSequence = new DicomTagSQ(0x3006, 0x0018);

        ///<summary>(3006,0020) VR=SQ VM=1 Structure Set ROI Sequence</summary>
        public readonly static DicomTagSQ StructureSetROISequence = new DicomTagSQ(0x3006, 0x0020);

        ///<summary>(3006,0022) VR=IS VM=1 ROI Number</summary>
        public readonly static DicomTagIS ROINumber = new DicomTagIS(0x3006, 0x0022);

        ///<summary>(3006,0024) VR=UI VM=1 Referenced Frame of Reference UID</summary>
        public readonly static DicomTagUI ReferencedFrameOfReferenceUID = new DicomTagUI(0x3006, 0x0024);

        ///<summary>(3006,0026) VR=LO VM=1 ROI Name</summary>
        public readonly static DicomTagLO ROIName = new DicomTagLO(0x3006, 0x0026);

        ///<summary>(3006,0028) VR=ST VM=1 ROI Description</summary>
        public readonly static DicomTagST ROIDescription = new DicomTagST(0x3006, 0x0028);

        ///<summary>(3006,002A) VR=IS VM=3 ROI Display Color</summary>
        public readonly static DicomTagISs ROIDisplayColor = new DicomTagISs(0x3006, 0x002A);

        ///<summary>(3006,002C) VR=DS VM=1 ROI Volume</summary>
        public readonly static DicomTagDS ROIVolume = new DicomTagDS(0x3006, 0x002C);

        ///<summary>(3006,002D) VR=DT VM=1 ROI DateTime</summary>
        public readonly static DicomTagDT ROIDateTime = new DicomTagDT(0x3006, 0x002D);

        ///<summary>(3006,002E) VR=DT VM=1 ROI Observation DateTime</summary>
        public readonly static DicomTagDT ROIObservationDateTime = new DicomTagDT(0x3006, 0x002E);

        ///<summary>(3006,0030) VR=SQ VM=1 RT Related ROI Sequence</summary>
        public readonly static DicomTagSQ RTRelatedROISequence = new DicomTagSQ(0x3006, 0x0030);

        ///<summary>(3006,0033) VR=CS VM=1 RT ROI Relationship</summary>
        public readonly static DicomTagCS RTROIRelationship = new DicomTagCS(0x3006, 0x0033);

        ///<summary>(3006,0036) VR=CS VM=1 ROI Generation Algorithm</summary>
        public readonly static DicomTagCS ROIGenerationAlgorithm = new DicomTagCS(0x3006, 0x0036);

        ///<summary>(3006,0037) VR=SQ VM=1 ROI Derivation Algorithm Identification Sequence</summary>
        public readonly static DicomTagSQ ROIDerivationAlgorithmIdentificationSequence = new DicomTagSQ(0x3006, 0x0037);

        ///<summary>(3006,0038) VR=LO VM=1 ROI Generation Description</summary>
        public readonly static DicomTagLO ROIGenerationDescription = new DicomTagLO(0x3006, 0x0038);

        ///<summary>(3006,0039) VR=SQ VM=1 ROI Contour Sequence</summary>
        public readonly static DicomTagSQ ROIContourSequence = new DicomTagSQ(0x3006, 0x0039);

        ///<summary>(3006,0040) VR=SQ VM=1 Contour Sequence</summary>
        public readonly static DicomTagSQ ContourSequence = new DicomTagSQ(0x3006, 0x0040);

        ///<summary>(3006,0042) VR=CS VM=1 Contour Geometric Type</summary>
        public readonly static DicomTagCS ContourGeometricType = new DicomTagCS(0x3006, 0x0042);

        ///<summary>(3006,0044) VR=DS VM=1 Contour Slab Thickness (RETIRED)</summary>
        public readonly static DicomTagDS ContourSlabThicknessRETIRED = new DicomTagDS(0x3006, 0x0044);

        ///<summary>(3006,0045) VR=DS VM=3 Contour Offset Vector (RETIRED)</summary>
        public readonly static DicomTagDSs ContourOffsetVectorRETIRED = new DicomTagDSs(0x3006, 0x0045);

        ///<summary>(3006,0046) VR=IS VM=1 Number of Contour Points</summary>
        public readonly static DicomTagIS NumberOfContourPoints = new DicomTagIS(0x3006, 0x0046);

        ///<summary>(3006,0048) VR=IS VM=1 Contour Number</summary>
        public readonly static DicomTagIS ContourNumber = new DicomTagIS(0x3006, 0x0048);

        ///<summary>(3006,0049) VR=IS VM=1-n Attached Contours (RETIRED)</summary>
        public readonly static DicomTagISs AttachedContoursRETIRED = new DicomTagISs(0x3006, 0x0049);

        ///<summary>(3006,004A) VR=SQ VM=1 Source Pixel Planes Characteristics Sequence</summary>
        public readonly static DicomTagSQ SourcePixelPlanesCharacteristicsSequence = new DicomTagSQ(0x3006, 0x004A);

        ///<summary>(3006,004B) VR=SQ VM=1 Source Series Sequence</summary>
        public readonly static DicomTagSQ SourceSeriesSequence = new DicomTagSQ(0x3006, 0x004B);

        ///<summary>(3006,004C) VR=SQ VM=1 Source Series Information Sequence</summary>
        public readonly static DicomTagSQ SourceSeriesInformationSequence = new DicomTagSQ(0x3006, 0x004C);

        ///<summary>(3006,004D) VR=SQ VM=1 ROI Creator Sequence</summary>
        public readonly static DicomTagSQ ROICreatorSequence = new DicomTagSQ(0x3006, 0x004D);

        ///<summary>(3006,004E) VR=SQ VM=1 ROI Interpreter Sequence</summary>
        public readonly static DicomTagSQ ROIInterpreterSequence = new DicomTagSQ(0x3006, 0x004E);

        ///<summary>(3006,004F) VR=SQ VM=1 ROI Observation Context Code Sequence</summary>
        public readonly static DicomTagSQ ROIObservationContextCodeSequence = new DicomTagSQ(0x3006, 0x004F);

        ///<summary>(3006,0050) VR=DS VM=3-3n Contour Data</summary>
        public readonly static DicomTagDSs ContourData = new DicomTagDSs(0x3006, 0x0050);

        ///<summary>(3006,0080) VR=SQ VM=1 RT ROI Observations Sequence</summary>
        public readonly static DicomTagSQ RTROIObservationsSequence = new DicomTagSQ(0x3006, 0x0080);

        ///<summary>(3006,0082) VR=IS VM=1 Observation Number</summary>
        public readonly static DicomTagIS ObservationNumber = new DicomTagIS(0x3006, 0x0082);

        ///<summary>(3006,0084) VR=IS VM=1 Referenced ROI Number</summary>
        public readonly static DicomTagIS ReferencedROINumber = new DicomTagIS(0x3006, 0x0084);

        ///<summary>(3006,0085) VR=SH VM=1 ROI Observation Label (RETIRED)</summary>
        public readonly static DicomTagSH ROIObservationLabelRETIRED = new DicomTagSH(0x3006, 0x0085);

        ///<summary>(3006,0086) VR=SQ VM=1 RT ROI Identification Code Sequence</summary>
        public readonly static DicomTagSQ RTROIIdentificationCodeSequence = new DicomTagSQ(0x3006, 0x0086);

        ///<summary>(3006,0088) VR=ST VM=1 ROI Observation Description (RETIRED)</summary>
        public readonly static DicomTagST ROIObservationDescriptionRETIRED = new DicomTagST(0x3006, 0x0088);

        ///<summary>(3006,00A0) VR=SQ VM=1 Related RT ROI Observations Sequence</summary>
        public readonly static DicomTagSQ RelatedRTROIObservationsSequence = new DicomTagSQ(0x3006, 0x00A0);

        ///<summary>(3006,00A4) VR=CS VM=1 RT ROI Interpreted Type</summary>
        public readonly static DicomTagCS RTROIInterpretedType = new DicomTagCS(0x3006, 0x00A4);

        ///<summary>(3006,00A6) VR=PN VM=1 ROI Interpreter</summary>
        public readonly static DicomTagPN ROIInterpreter = new DicomTagPN(0x3006, 0x00A6);

        ///<summary>(3006,00B0) VR=SQ VM=1 ROI Physical Properties Sequence</summary>
        public readonly static DicomTagSQ ROIPhysicalPropertiesSequence = new DicomTagSQ(0x3006, 0x00B0);

        ///<summary>(3006,00B2) VR=CS VM=1 ROI Physical Property</summary>
        public readonly static DicomTagCS ROIPhysicalProperty = new DicomTagCS(0x3006, 0x00B2);

        ///<summary>(3006,00B4) VR=DS VM=1 ROI Physical Property Value</summary>
        public readonly static DicomTagDS ROIPhysicalPropertyValue = new DicomTagDS(0x3006, 0x00B4);

        ///<summary>(3006,00B6) VR=SQ VM=1 ROI Elemental Composition Sequence</summary>
        public readonly static DicomTagSQ ROIElementalCompositionSequence = new DicomTagSQ(0x3006, 0x00B6);

        ///<summary>(3006,00B7) VR=US VM=1 ROI Elemental Composition Atomic Number</summary>
        public readonly static DicomTagUS ROIElementalCompositionAtomicNumber = new DicomTagUS(0x3006, 0x00B7);

        ///<summary>(3006,00B8) VR=FL VM=1 ROI Elemental Composition Atomic Mass Fraction</summary>
        public readonly static DicomTagFL ROIElementalCompositionAtomicMassFraction = new DicomTagFL(0x3006, 0x00B8);

        ///<summary>(3006,00B9) VR=SQ VM=1 Additional RT ROI Identification Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ AdditionalRTROIIdentificationCodeSequenceRETIRED = new DicomTagSQ(0x3006, 0x00B9);

        ///<summary>(3006,00C0) VR=SQ VM=1 Frame of Reference Relationship Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ FrameOfReferenceRelationshipSequenceRETIRED = new DicomTagSQ(0x3006, 0x00C0);

        ///<summary>(3006,00C2) VR=UI VM=1 Related Frame of Reference UID (RETIRED)</summary>
        public readonly static DicomTagUI RelatedFrameOfReferenceUIDRETIRED = new DicomTagUI(0x3006, 0x00C2);

        ///<summary>(3006,00C4) VR=CS VM=1 Frame of Reference Transformation Type (RETIRED)</summary>
        public readonly static DicomTagCS FrameOfReferenceTransformationTypeRETIRED = new DicomTagCS(0x3006, 0x00C4);

        ///<summary>(3006,00C6) VR=DS VM=16 Frame of Reference Transformation Matrix</summary>
        public readonly static DicomTagDSs FrameOfReferenceTransformationMatrix = new DicomTagDSs(0x3006, 0x00C6);

        ///<summary>(3006,00C8) VR=LO VM=1 Frame of Reference Transformation Comment</summary>
        public readonly static DicomTagLO FrameOfReferenceTransformationComment = new DicomTagLO(0x3006, 0x00C8);

        ///<summary>(3006,00C9) VR=SQ VM=1 Patient Location Coordinates Sequence</summary>
        public readonly static DicomTagSQ PatientLocationCoordinatesSequence = new DicomTagSQ(0x3006, 0x00C9);

        ///<summary>(3006,00CA) VR=SQ VM=1 Patient Location Coordinates Code Sequence</summary>
        public readonly static DicomTagSQ PatientLocationCoordinatesCodeSequence = new DicomTagSQ(0x3006, 0x00CA);

        ///<summary>(3006,00CB) VR=SQ VM=1 Patient Support Position Sequence</summary>
        public readonly static DicomTagSQ PatientSupportPositionSequence = new DicomTagSQ(0x3006, 0x00CB);

        ///<summary>(3008,0010) VR=SQ VM=1 Measured Dose Reference Sequence</summary>
        public readonly static DicomTagSQ MeasuredDoseReferenceSequence = new DicomTagSQ(0x3008, 0x0010);

        ///<summary>(3008,0012) VR=ST VM=1 Measured Dose Description</summary>
        public readonly static DicomTagST MeasuredDoseDescription = new DicomTagST(0x3008, 0x0012);

        ///<summary>(3008,0014) VR=CS VM=1 Measured Dose Type</summary>
        public readonly static DicomTagCS MeasuredDoseType = new DicomTagCS(0x3008, 0x0014);

        ///<summary>(3008,0016) VR=DS VM=1 Measured Dose Value</summary>
        public readonly static DicomTagDS MeasuredDoseValue = new DicomTagDS(0x3008, 0x0016);

        ///<summary>(3008,0020) VR=SQ VM=1 Treatment Session Beam Sequence</summary>
        public readonly static DicomTagSQ TreatmentSessionBeamSequence = new DicomTagSQ(0x3008, 0x0020);

        ///<summary>(3008,0021) VR=SQ VM=1 Treatment Session Ion Beam Sequence</summary>
        public readonly static DicomTagSQ TreatmentSessionIonBeamSequence = new DicomTagSQ(0x3008, 0x0021);

        ///<summary>(3008,0022) VR=IS VM=1 Current Fraction Number</summary>
        public readonly static DicomTagIS CurrentFractionNumber = new DicomTagIS(0x3008, 0x0022);

        ///<summary>(3008,0024) VR=DA VM=1 Treatment Control Point Date</summary>
        public readonly static DicomTagDA TreatmentControlPointDate = new DicomTagDA(0x3008, 0x0024);

        ///<summary>(3008,0025) VR=TM VM=1 Treatment Control Point Time</summary>
        public readonly static DicomTagTM TreatmentControlPointTime = new DicomTagTM(0x3008, 0x0025);

        ///<summary>(3008,002A) VR=CS VM=1 Treatment Termination Status</summary>
        public readonly static DicomTagCS TreatmentTerminationStatus = new DicomTagCS(0x3008, 0x002A);

        ///<summary>(3008,002B) VR=SH VM=1 Treatment Termination Code (RETIRED)</summary>
        public readonly static DicomTagSH TreatmentTerminationCodeRETIRED = new DicomTagSH(0x3008, 0x002B);

        ///<summary>(3008,002C) VR=CS VM=1 Treatment Verification Status</summary>
        public readonly static DicomTagCS TreatmentVerificationStatus = new DicomTagCS(0x3008, 0x002C);

        ///<summary>(3008,0030) VR=SQ VM=1 Referenced Treatment Record Sequence</summary>
        public readonly static DicomTagSQ ReferencedTreatmentRecordSequence = new DicomTagSQ(0x3008, 0x0030);

        ///<summary>(3008,0032) VR=DS VM=1 Specified Primary Meterset</summary>
        public readonly static DicomTagDS SpecifiedPrimaryMeterset = new DicomTagDS(0x3008, 0x0032);

        ///<summary>(3008,0033) VR=DS VM=1 Specified Secondary Meterset</summary>
        public readonly static DicomTagDS SpecifiedSecondaryMeterset = new DicomTagDS(0x3008, 0x0033);

        ///<summary>(3008,0036) VR=DS VM=1 Delivered Primary Meterset</summary>
        public readonly static DicomTagDS DeliveredPrimaryMeterset = new DicomTagDS(0x3008, 0x0036);

        ///<summary>(3008,0037) VR=DS VM=1 Delivered Secondary Meterset</summary>
        public readonly static DicomTagDS DeliveredSecondaryMeterset = new DicomTagDS(0x3008, 0x0037);

        ///<summary>(3008,003A) VR=DS VM=1 Specified Treatment Time</summary>
        public readonly static DicomTagDS SpecifiedTreatmentTime = new DicomTagDS(0x3008, 0x003A);

        ///<summary>(3008,003B) VR=DS VM=1 Delivered Treatment Time</summary>
        public readonly static DicomTagDS DeliveredTreatmentTime = new DicomTagDS(0x3008, 0x003B);

        ///<summary>(3008,0040) VR=SQ VM=1 Control Point Delivery Sequence</summary>
        public readonly static DicomTagSQ ControlPointDeliverySequence = new DicomTagSQ(0x3008, 0x0040);

        ///<summary>(3008,0041) VR=SQ VM=1 Ion Control Point Delivery Sequence</summary>
        public readonly static DicomTagSQ IonControlPointDeliverySequence = new DicomTagSQ(0x3008, 0x0041);

        ///<summary>(3008,0042) VR=DS VM=1 Specified Meterset</summary>
        public readonly static DicomTagDS SpecifiedMeterset = new DicomTagDS(0x3008, 0x0042);

        ///<summary>(3008,0044) VR=DS VM=1 Delivered Meterset</summary>
        public readonly static DicomTagDS DeliveredMeterset = new DicomTagDS(0x3008, 0x0044);

        ///<summary>(3008,0045) VR=FL VM=1 Meterset Rate Set</summary>
        public readonly static DicomTagFL MetersetRateSet = new DicomTagFL(0x3008, 0x0045);

        ///<summary>(3008,0046) VR=FL VM=1 Meterset Rate Delivered</summary>
        public readonly static DicomTagFL MetersetRateDelivered = new DicomTagFL(0x3008, 0x0046);

        ///<summary>(3008,0047) VR=FL VM=1-n Scan Spot Metersets Delivered</summary>
        public readonly static DicomTagFLs ScanSpotMetersetsDelivered = new DicomTagFLs(0x3008, 0x0047);

        ///<summary>(3008,0048) VR=DS VM=1 Dose Rate Delivered</summary>
        public readonly static DicomTagDS DoseRateDelivered = new DicomTagDS(0x3008, 0x0048);

        ///<summary>(3008,0050) VR=SQ VM=1 Treatment Summary Calculated Dose Reference Sequence</summary>
        public readonly static DicomTagSQ TreatmentSummaryCalculatedDoseReferenceSequence = new DicomTagSQ(0x3008, 0x0050);

        ///<summary>(3008,0052) VR=DS VM=1 Cumulative Dose to Dose Reference</summary>
        public readonly static DicomTagDS CumulativeDoseToDoseReference = new DicomTagDS(0x3008, 0x0052);

        ///<summary>(3008,0054) VR=DA VM=1 First Treatment Date</summary>
        public readonly static DicomTagDA FirstTreatmentDate = new DicomTagDA(0x3008, 0x0054);

        ///<summary>(3008,0056) VR=DA VM=1 Most Recent Treatment Date</summary>
        public readonly static DicomTagDA MostRecentTreatmentDate = new DicomTagDA(0x3008, 0x0056);

        ///<summary>(3008,005A) VR=IS VM=1 Number of Fractions Delivered</summary>
        public readonly static DicomTagIS NumberOfFractionsDelivered = new DicomTagIS(0x3008, 0x005A);

        ///<summary>(3008,0060) VR=SQ VM=1 Override Sequence</summary>
        public readonly static DicomTagSQ OverrideSequence = new DicomTagSQ(0x3008, 0x0060);

        ///<summary>(3008,0061) VR=AT VM=1 Parameter Sequence Pointer</summary>
        public readonly static DicomTagAT ParameterSequencePointer = new DicomTagAT(0x3008, 0x0061);

        ///<summary>(3008,0062) VR=AT VM=1 Override Parameter Pointer</summary>
        public readonly static DicomTagAT OverrideParameterPointer = new DicomTagAT(0x3008, 0x0062);

        ///<summary>(3008,0063) VR=IS VM=1 Parameter Item Index</summary>
        public readonly static DicomTagIS ParameterItemIndex = new DicomTagIS(0x3008, 0x0063);

        ///<summary>(3008,0064) VR=IS VM=1 Measured Dose Reference Number</summary>
        public readonly static DicomTagIS MeasuredDoseReferenceNumber = new DicomTagIS(0x3008, 0x0064);

        ///<summary>(3008,0065) VR=AT VM=1 Parameter Pointer</summary>
        public readonly static DicomTagAT ParameterPointer = new DicomTagAT(0x3008, 0x0065);

        ///<summary>(3008,0066) VR=ST VM=1 Override Reason</summary>
        public readonly static DicomTagST OverrideReason = new DicomTagST(0x3008, 0x0066);

        ///<summary>(3008,0067) VR=US VM=1 Parameter Value Number</summary>
        public readonly static DicomTagUS ParameterValueNumber = new DicomTagUS(0x3008, 0x0067);

        ///<summary>(3008,0068) VR=SQ VM=1 Corrected Parameter Sequence</summary>
        public readonly static DicomTagSQ CorrectedParameterSequence = new DicomTagSQ(0x3008, 0x0068);

        ///<summary>(3008,006A) VR=FL VM=1 Correction Value</summary>
        public readonly static DicomTagFL CorrectionValue = new DicomTagFL(0x3008, 0x006A);

        ///<summary>(3008,0070) VR=SQ VM=1 Calculated Dose Reference Sequence</summary>
        public readonly static DicomTagSQ CalculatedDoseReferenceSequence = new DicomTagSQ(0x3008, 0x0070);

        ///<summary>(3008,0072) VR=IS VM=1 Calculated Dose Reference Number</summary>
        public readonly static DicomTagIS CalculatedDoseReferenceNumber = new DicomTagIS(0x3008, 0x0072);

        ///<summary>(3008,0074) VR=ST VM=1 Calculated Dose Reference Description</summary>
        public readonly static DicomTagST CalculatedDoseReferenceDescription = new DicomTagST(0x3008, 0x0074);

        ///<summary>(3008,0076) VR=DS VM=1 Calculated Dose Reference Dose Value</summary>
        public readonly static DicomTagDS CalculatedDoseReferenceDoseValue = new DicomTagDS(0x3008, 0x0076);

        ///<summary>(3008,0078) VR=DS VM=1 Start Meterset</summary>
        public readonly static DicomTagDS StartMeterset = new DicomTagDS(0x3008, 0x0078);

        ///<summary>(3008,007A) VR=DS VM=1 End Meterset</summary>
        public readonly static DicomTagDS EndMeterset = new DicomTagDS(0x3008, 0x007A);

        ///<summary>(3008,0080) VR=SQ VM=1 Referenced Measured Dose Reference Sequence</summary>
        public readonly static DicomTagSQ ReferencedMeasuredDoseReferenceSequence = new DicomTagSQ(0x3008, 0x0080);

        ///<summary>(3008,0082) VR=IS VM=1 Referenced Measured Dose Reference Number</summary>
        public readonly static DicomTagIS ReferencedMeasuredDoseReferenceNumber = new DicomTagIS(0x3008, 0x0082);

        ///<summary>(3008,0090) VR=SQ VM=1 Referenced Calculated Dose Reference Sequence</summary>
        public readonly static DicomTagSQ ReferencedCalculatedDoseReferenceSequence = new DicomTagSQ(0x3008, 0x0090);

        ///<summary>(3008,0092) VR=IS VM=1 Referenced Calculated Dose Reference Number</summary>
        public readonly static DicomTagIS ReferencedCalculatedDoseReferenceNumber = new DicomTagIS(0x3008, 0x0092);

        ///<summary>(3008,00A0) VR=SQ VM=1 Beam Limiting Device Leaf Pairs Sequence</summary>
        public readonly static DicomTagSQ BeamLimitingDeviceLeafPairsSequence = new DicomTagSQ(0x3008, 0x00A0);

        ///<summary>(3008,00A1) VR=SQ VM=1 Enhanced RT Beam Limiting Device Sequence</summary>
        public readonly static DicomTagSQ EnhancedRTBeamLimitingDeviceSequence = new DicomTagSQ(0x3008, 0x00A1);

        ///<summary>(3008,00A2) VR=SQ VM=1 Enhanced RT Beam Limiting Opening Sequence</summary>
        public readonly static DicomTagSQ EnhancedRTBeamLimitingOpeningSequence = new DicomTagSQ(0x3008, 0x00A2);

        ///<summary>(3008,00A3) VR=CS VM=1 Enhanced RT Beam Limiting Device Definition Flag</summary>
        public readonly static DicomTagCS EnhancedRTBeamLimitingDeviceDefinitionFlag = new DicomTagCS(0x3008, 0x00A3);

        ///<summary>(3008,00A4) VR=FD VM=2-2n Parallel RT Beam Delimiter Opening Extents</summary>
        public readonly static DicomTagFDs ParallelRTBeamDelimiterOpeningExtents = new DicomTagFDs(0x3008, 0x00A4);

        ///<summary>(3008,00B0) VR=SQ VM=1 Recorded Wedge Sequence</summary>
        public readonly static DicomTagSQ RecordedWedgeSequence = new DicomTagSQ(0x3008, 0x00B0);

        ///<summary>(3008,00C0) VR=SQ VM=1 Recorded Compensator Sequence</summary>
        public readonly static DicomTagSQ RecordedCompensatorSequence = new DicomTagSQ(0x3008, 0x00C0);

        ///<summary>(3008,00D0) VR=SQ VM=1 Recorded Block Sequence</summary>
        public readonly static DicomTagSQ RecordedBlockSequence = new DicomTagSQ(0x3008, 0x00D0);

        ///<summary>(3008,00D1) VR=SQ VM=1 Recorded Block Slab Sequence</summary>
        public readonly static DicomTagSQ RecordedBlockSlabSequence = new DicomTagSQ(0x3008, 0x00D1);

        ///<summary>(3008,00E0) VR=SQ VM=1 Treatment Summary Measured Dose Reference Sequence</summary>
        public readonly static DicomTagSQ TreatmentSummaryMeasuredDoseReferenceSequence = new DicomTagSQ(0x3008, 0x00E0);

        ///<summary>(3008,00F0) VR=SQ VM=1 Recorded Snout Sequence</summary>
        public readonly static DicomTagSQ RecordedSnoutSequence = new DicomTagSQ(0x3008, 0x00F0);

        ///<summary>(3008,00F2) VR=SQ VM=1 Recorded Range Shifter Sequence</summary>
        public readonly static DicomTagSQ RecordedRangeShifterSequence = new DicomTagSQ(0x3008, 0x00F2);

        ///<summary>(3008,00F4) VR=SQ VM=1 Recorded Lateral Spreading Device Sequence</summary>
        public readonly static DicomTagSQ RecordedLateralSpreadingDeviceSequence = new DicomTagSQ(0x3008, 0x00F4);

        ///<summary>(3008,00F6) VR=SQ VM=1 Recorded Range Modulator Sequence</summary>
        public readonly static DicomTagSQ RecordedRangeModulatorSequence = new DicomTagSQ(0x3008, 0x00F6);

        ///<summary>(3008,0100) VR=SQ VM=1 Recorded Source Sequence</summary>
        public readonly static DicomTagSQ RecordedSourceSequence = new DicomTagSQ(0x3008, 0x0100);

        ///<summary>(3008,0105) VR=LO VM=1 Source Serial Number</summary>
        public readonly static DicomTagLO SourceSerialNumber = new DicomTagLO(0x3008, 0x0105);

        ///<summary>(3008,0110) VR=SQ VM=1 Treatment Session Application Setup Sequence</summary>
        public readonly static DicomTagSQ TreatmentSessionApplicationSetupSequence = new DicomTagSQ(0x3008, 0x0110);

        ///<summary>(3008,0116) VR=CS VM=1 Application Setup Check</summary>
        public readonly static DicomTagCS ApplicationSetupCheck = new DicomTagCS(0x3008, 0x0116);

        ///<summary>(3008,0120) VR=SQ VM=1 Recorded Brachy Accessory Device Sequence</summary>
        public readonly static DicomTagSQ RecordedBrachyAccessoryDeviceSequence = new DicomTagSQ(0x3008, 0x0120);

        ///<summary>(3008,0122) VR=IS VM=1 Referenced Brachy Accessory Device Number</summary>
        public readonly static DicomTagIS ReferencedBrachyAccessoryDeviceNumber = new DicomTagIS(0x3008, 0x0122);

        ///<summary>(3008,0130) VR=SQ VM=1 Recorded Channel Sequence</summary>
        public readonly static DicomTagSQ RecordedChannelSequence = new DicomTagSQ(0x3008, 0x0130);

        ///<summary>(3008,0132) VR=DS VM=1 Specified Channel Total Time</summary>
        public readonly static DicomTagDS SpecifiedChannelTotalTime = new DicomTagDS(0x3008, 0x0132);

        ///<summary>(3008,0134) VR=DS VM=1 Delivered Channel Total Time</summary>
        public readonly static DicomTagDS DeliveredChannelTotalTime = new DicomTagDS(0x3008, 0x0134);

        ///<summary>(3008,0136) VR=IS VM=1 Specified Number of Pulses</summary>
        public readonly static DicomTagIS SpecifiedNumberOfPulses = new DicomTagIS(0x3008, 0x0136);

        ///<summary>(3008,0138) VR=IS VM=1 Delivered Number of Pulses</summary>
        public readonly static DicomTagIS DeliveredNumberOfPulses = new DicomTagIS(0x3008, 0x0138);

        ///<summary>(3008,013A) VR=DS VM=1 Specified Pulse Repetition Interval</summary>
        public readonly static DicomTagDS SpecifiedPulseRepetitionInterval = new DicomTagDS(0x3008, 0x013A);

        ///<summary>(3008,013C) VR=DS VM=1 Delivered Pulse Repetition Interval</summary>
        public readonly static DicomTagDS DeliveredPulseRepetitionInterval = new DicomTagDS(0x3008, 0x013C);

        ///<summary>(3008,0140) VR=SQ VM=1 Recorded Source Applicator Sequence</summary>
        public readonly static DicomTagSQ RecordedSourceApplicatorSequence = new DicomTagSQ(0x3008, 0x0140);

        ///<summary>(3008,0142) VR=IS VM=1 Referenced Source Applicator Number</summary>
        public readonly static DicomTagIS ReferencedSourceApplicatorNumber = new DicomTagIS(0x3008, 0x0142);

        ///<summary>(3008,0150) VR=SQ VM=1 Recorded Channel Shield Sequence</summary>
        public readonly static DicomTagSQ RecordedChannelShieldSequence = new DicomTagSQ(0x3008, 0x0150);

        ///<summary>(3008,0152) VR=IS VM=1 Referenced Channel Shield Number</summary>
        public readonly static DicomTagIS ReferencedChannelShieldNumber = new DicomTagIS(0x3008, 0x0152);

        ///<summary>(3008,0160) VR=SQ VM=1 Brachy Control Point Delivered Sequence</summary>
        public readonly static DicomTagSQ BrachyControlPointDeliveredSequence = new DicomTagSQ(0x3008, 0x0160);

        ///<summary>(3008,0162) VR=DA VM=1 Safe Position Exit Date</summary>
        public readonly static DicomTagDA SafePositionExitDate = new DicomTagDA(0x3008, 0x0162);

        ///<summary>(3008,0164) VR=TM VM=1 Safe Position Exit Time</summary>
        public readonly static DicomTagTM SafePositionExitTime = new DicomTagTM(0x3008, 0x0164);

        ///<summary>(3008,0166) VR=DA VM=1 Safe Position Return Date</summary>
        public readonly static DicomTagDA SafePositionReturnDate = new DicomTagDA(0x3008, 0x0166);

        ///<summary>(3008,0168) VR=TM VM=1 Safe Position Return Time</summary>
        public readonly static DicomTagTM SafePositionReturnTime = new DicomTagTM(0x3008, 0x0168);

        ///<summary>(3008,0171) VR=SQ VM=1 Pulse Specific Brachy Control Point Delivered Sequence</summary>
        public readonly static DicomTagSQ PulseSpecificBrachyControlPointDeliveredSequence = new DicomTagSQ(0x3008, 0x0171);

        ///<summary>(3008,0172) VR=US VM=1 Pulse Number</summary>
        public readonly static DicomTagUS PulseNumber = new DicomTagUS(0x3008, 0x0172);

        ///<summary>(3008,0173) VR=SQ VM=1 Brachy Pulse Control Point Delivered Sequence</summary>
        public readonly static DicomTagSQ BrachyPulseControlPointDeliveredSequence = new DicomTagSQ(0x3008, 0x0173);

        ///<summary>(3008,0200) VR=CS VM=1 Current Treatment Status</summary>
        public readonly static DicomTagCS CurrentTreatmentStatus = new DicomTagCS(0x3008, 0x0200);

        ///<summary>(3008,0202) VR=ST VM=1 Treatment Status Comment</summary>
        public readonly static DicomTagST TreatmentStatusComment = new DicomTagST(0x3008, 0x0202);

        ///<summary>(3008,0220) VR=SQ VM=1 Fraction Group Summary Sequence</summary>
        public readonly static DicomTagSQ FractionGroupSummarySequence = new DicomTagSQ(0x3008, 0x0220);

        ///<summary>(3008,0223) VR=IS VM=1 Referenced Fraction Number</summary>
        public readonly static DicomTagIS ReferencedFractionNumber = new DicomTagIS(0x3008, 0x0223);

        ///<summary>(3008,0224) VR=CS VM=1 Fraction Group Type</summary>
        public readonly static DicomTagCS FractionGroupType = new DicomTagCS(0x3008, 0x0224);

        ///<summary>(3008,0230) VR=CS VM=1 Beam Stopper Position</summary>
        public readonly static DicomTagCS BeamStopperPosition = new DicomTagCS(0x3008, 0x0230);

        ///<summary>(3008,0240) VR=SQ VM=1 Fraction Status Summary Sequence</summary>
        public readonly static DicomTagSQ FractionStatusSummarySequence = new DicomTagSQ(0x3008, 0x0240);

        ///<summary>(3008,0250) VR=DA VM=1 Treatment Date</summary>
        public readonly static DicomTagDA TreatmentDate = new DicomTagDA(0x3008, 0x0250);

        ///<summary>(3008,0251) VR=TM VM=1 Treatment Time</summary>
        public readonly static DicomTagTM TreatmentTime = new DicomTagTM(0x3008, 0x0251);

        ///<summary>(300A,0002) VR=SH VM=1 RT Plan Label</summary>
        public readonly static DicomTagSH RTPlanLabel = new DicomTagSH(0x300A, 0x0002);

        ///<summary>(300A,0003) VR=LO VM=1 RT Plan Name</summary>
        public readonly static DicomTagLO RTPlanName = new DicomTagLO(0x300A, 0x0003);

        ///<summary>(300A,0004) VR=ST VM=1 RT Plan Description</summary>
        public readonly static DicomTagST RTPlanDescription = new DicomTagST(0x300A, 0x0004);

        ///<summary>(300A,0006) VR=DA VM=1 RT Plan Date</summary>
        public readonly static DicomTagDA RTPlanDate = new DicomTagDA(0x300A, 0x0006);

        ///<summary>(300A,0007) VR=TM VM=1 RT Plan Time</summary>
        public readonly static DicomTagTM RTPlanTime = new DicomTagTM(0x300A, 0x0007);

        ///<summary>(300A,0009) VR=LO VM=1-n Treatment Protocols</summary>
        public readonly static DicomTagLOs TreatmentProtocols = new DicomTagLOs(0x300A, 0x0009);

        ///<summary>(300A,000A) VR=CS VM=1 Plan Intent</summary>
        public readonly static DicomTagCS PlanIntent = new DicomTagCS(0x300A, 0x000A);

        ///<summary>(300A,000B) VR=LO VM=1-n Treatment Sites (RETIRED)</summary>
        public readonly static DicomTagLOs TreatmentSitesRETIRED = new DicomTagLOs(0x300A, 0x000B);

        ///<summary>(300A,000C) VR=CS VM=1 RT Plan Geometry</summary>
        public readonly static DicomTagCS RTPlanGeometry = new DicomTagCS(0x300A, 0x000C);

        ///<summary>(300A,000E) VR=ST VM=1 Prescription Description</summary>
        public readonly static DicomTagST PrescriptionDescription = new DicomTagST(0x300A, 0x000E);

        ///<summary>(300A,0010) VR=SQ VM=1 Dose Reference Sequence</summary>
        public readonly static DicomTagSQ DoseReferenceSequence = new DicomTagSQ(0x300A, 0x0010);

        ///<summary>(300A,0012) VR=IS VM=1 Dose Reference Number</summary>
        public readonly static DicomTagIS DoseReferenceNumber = new DicomTagIS(0x300A, 0x0012);

        ///<summary>(300A,0013) VR=UI VM=1 Dose Reference UID</summary>
        public readonly static DicomTagUI DoseReferenceUID = new DicomTagUI(0x300A, 0x0013);

        ///<summary>(300A,0014) VR=CS VM=1 Dose Reference Structure Type</summary>
        public readonly static DicomTagCS DoseReferenceStructureType = new DicomTagCS(0x300A, 0x0014);

        ///<summary>(300A,0015) VR=CS VM=1 Nominal Beam Energy Unit</summary>
        public readonly static DicomTagCS NominalBeamEnergyUnit = new DicomTagCS(0x300A, 0x0015);

        ///<summary>(300A,0016) VR=LO VM=1 Dose Reference Description</summary>
        public readonly static DicomTagLO DoseReferenceDescription = new DicomTagLO(0x300A, 0x0016);

        ///<summary>(300A,0018) VR=DS VM=3 Dose Reference Point Coordinates</summary>
        public readonly static DicomTagDSs DoseReferencePointCoordinates = new DicomTagDSs(0x300A, 0x0018);

        ///<summary>(300A,001A) VR=DS VM=1 Nominal Prior Dose</summary>
        public readonly static DicomTagDS NominalPriorDose = new DicomTagDS(0x300A, 0x001A);

        ///<summary>(300A,0020) VR=CS VM=1 Dose Reference Type</summary>
        public readonly static DicomTagCS DoseReferenceType = new DicomTagCS(0x300A, 0x0020);

        ///<summary>(300A,0021) VR=DS VM=1 Constraint Weight</summary>
        public readonly static DicomTagDS ConstraintWeight = new DicomTagDS(0x300A, 0x0021);

        ///<summary>(300A,0022) VR=DS VM=1 Delivery Warning Dose</summary>
        public readonly static DicomTagDS DeliveryWarningDose = new DicomTagDS(0x300A, 0x0022);

        ///<summary>(300A,0023) VR=DS VM=1 Delivery Maximum Dose</summary>
        public readonly static DicomTagDS DeliveryMaximumDose = new DicomTagDS(0x300A, 0x0023);

        ///<summary>(300A,0025) VR=DS VM=1 Target Minimum Dose</summary>
        public readonly static DicomTagDS TargetMinimumDose = new DicomTagDS(0x300A, 0x0025);

        ///<summary>(300A,0026) VR=DS VM=1 Target Prescription Dose</summary>
        public readonly static DicomTagDS TargetPrescriptionDose = new DicomTagDS(0x300A, 0x0026);

        ///<summary>(300A,0027) VR=DS VM=1 Target Maximum Dose</summary>
        public readonly static DicomTagDS TargetMaximumDose = new DicomTagDS(0x300A, 0x0027);

        ///<summary>(300A,0028) VR=DS VM=1 Target Underdose Volume Fraction</summary>
        public readonly static DicomTagDS TargetUnderdoseVolumeFraction = new DicomTagDS(0x300A, 0x0028);

        ///<summary>(300A,002A) VR=DS VM=1 Organ at Risk Full-volume Dose</summary>
        public readonly static DicomTagDS OrganAtRiskFullVolumeDose = new DicomTagDS(0x300A, 0x002A);

        ///<summary>(300A,002B) VR=DS VM=1 Organ at Risk Limit Dose</summary>
        public readonly static DicomTagDS OrganAtRiskLimitDose = new DicomTagDS(0x300A, 0x002B);

        ///<summary>(300A,002C) VR=DS VM=1 Organ at Risk Maximum Dose</summary>
        public readonly static DicomTagDS OrganAtRiskMaximumDose = new DicomTagDS(0x300A, 0x002C);

        ///<summary>(300A,002D) VR=DS VM=1 Organ at Risk Overdose Volume Fraction</summary>
        public readonly static DicomTagDS OrganAtRiskOverdoseVolumeFraction = new DicomTagDS(0x300A, 0x002D);

        ///<summary>(300A,0040) VR=SQ VM=1 Tolerance Table Sequence</summary>
        public readonly static DicomTagSQ ToleranceTableSequence = new DicomTagSQ(0x300A, 0x0040);

        ///<summary>(300A,0042) VR=IS VM=1 Tolerance Table Number</summary>
        public readonly static DicomTagIS ToleranceTableNumber = new DicomTagIS(0x300A, 0x0042);

        ///<summary>(300A,0043) VR=SH VM=1 Tolerance Table Label</summary>
        public readonly static DicomTagSH ToleranceTableLabel = new DicomTagSH(0x300A, 0x0043);

        ///<summary>(300A,0044) VR=DS VM=1 Gantry Angle Tolerance</summary>
        public readonly static DicomTagDS GantryAngleTolerance = new DicomTagDS(0x300A, 0x0044);

        ///<summary>(300A,0046) VR=DS VM=1 Beam Limiting Device Angle Tolerance</summary>
        public readonly static DicomTagDS BeamLimitingDeviceAngleTolerance = new DicomTagDS(0x300A, 0x0046);

        ///<summary>(300A,0048) VR=SQ VM=1 Beam Limiting Device Tolerance Sequence</summary>
        public readonly static DicomTagSQ BeamLimitingDeviceToleranceSequence = new DicomTagSQ(0x300A, 0x0048);

        ///<summary>(300A,004A) VR=DS VM=1 Beam Limiting Device Position Tolerance</summary>
        public readonly static DicomTagDS BeamLimitingDevicePositionTolerance = new DicomTagDS(0x300A, 0x004A);

        ///<summary>(300A,004B) VR=FL VM=1 Snout Position Tolerance</summary>
        public readonly static DicomTagFL SnoutPositionTolerance = new DicomTagFL(0x300A, 0x004B);

        ///<summary>(300A,004C) VR=DS VM=1 Patient Support Angle Tolerance</summary>
        public readonly static DicomTagDS PatientSupportAngleTolerance = new DicomTagDS(0x300A, 0x004C);

        ///<summary>(300A,004E) VR=DS VM=1 Table Top Eccentric Angle Tolerance</summary>
        public readonly static DicomTagDS TableTopEccentricAngleTolerance = new DicomTagDS(0x300A, 0x004E);

        ///<summary>(300A,004F) VR=FL VM=1 Table Top Pitch Angle Tolerance</summary>
        public readonly static DicomTagFL TableTopPitchAngleTolerance = new DicomTagFL(0x300A, 0x004F);

        ///<summary>(300A,0050) VR=FL VM=1 Table Top Roll Angle Tolerance</summary>
        public readonly static DicomTagFL TableTopRollAngleTolerance = new DicomTagFL(0x300A, 0x0050);

        ///<summary>(300A,0051) VR=DS VM=1 Table Top Vertical Position Tolerance</summary>
        public readonly static DicomTagDS TableTopVerticalPositionTolerance = new DicomTagDS(0x300A, 0x0051);

        ///<summary>(300A,0052) VR=DS VM=1 Table Top Longitudinal Position Tolerance</summary>
        public readonly static DicomTagDS TableTopLongitudinalPositionTolerance = new DicomTagDS(0x300A, 0x0052);

        ///<summary>(300A,0053) VR=DS VM=1 Table Top Lateral Position Tolerance</summary>
        public readonly static DicomTagDS TableTopLateralPositionTolerance = new DicomTagDS(0x300A, 0x0053);

        ///<summary>(300A,0054) VR=UI VM=1 Table Top Position Alignment UID</summary>
        public readonly static DicomTagUI TableTopPositionAlignmentUID = new DicomTagUI(0x300A, 0x0054);

        ///<summary>(300A,0055) VR=CS VM=1 RT Plan Relationship</summary>
        public readonly static DicomTagCS RTPlanRelationship = new DicomTagCS(0x300A, 0x0055);

        ///<summary>(300A,0070) VR=SQ VM=1 Fraction Group Sequence</summary>
        public readonly static DicomTagSQ FractionGroupSequence = new DicomTagSQ(0x300A, 0x0070);

        ///<summary>(300A,0071) VR=IS VM=1 Fraction Group Number</summary>
        public readonly static DicomTagIS FractionGroupNumber = new DicomTagIS(0x300A, 0x0071);

        ///<summary>(300A,0072) VR=LO VM=1 Fraction Group Description</summary>
        public readonly static DicomTagLO FractionGroupDescription = new DicomTagLO(0x300A, 0x0072);

        ///<summary>(300A,0078) VR=IS VM=1 Number of Fractions Planned</summary>
        public readonly static DicomTagIS NumberOfFractionsPlanned = new DicomTagIS(0x300A, 0x0078);

        ///<summary>(300A,0079) VR=IS VM=1 Number of Fraction Pattern Digits Per Day</summary>
        public readonly static DicomTagIS NumberOfFractionPatternDigitsPerDay = new DicomTagIS(0x300A, 0x0079);

        ///<summary>(300A,007A) VR=IS VM=1 Repeat Fraction Cycle Length</summary>
        public readonly static DicomTagIS RepeatFractionCycleLength = new DicomTagIS(0x300A, 0x007A);

        ///<summary>(300A,007B) VR=LT VM=1 Fraction Pattern</summary>
        public readonly static DicomTagLT FractionPattern = new DicomTagLT(0x300A, 0x007B);

        ///<summary>(300A,0080) VR=IS VM=1 Number of Beams</summary>
        public readonly static DicomTagIS NumberOfBeams = new DicomTagIS(0x300A, 0x0080);

        ///<summary>(300A,0082) VR=DS VM=3 Beam Dose Specification Point (RETIRED)</summary>
        public readonly static DicomTagDSs BeamDoseSpecificationPointRETIRED = new DicomTagDSs(0x300A, 0x0082);

        ///<summary>(300A,0083) VR=UI VM=1 Referenced Dose Reference UID</summary>
        public readonly static DicomTagUI ReferencedDoseReferenceUID = new DicomTagUI(0x300A, 0x0083);

        ///<summary>(300A,0084) VR=DS VM=1 Beam Dose</summary>
        public readonly static DicomTagDS BeamDose = new DicomTagDS(0x300A, 0x0084);

        ///<summary>(300A,0086) VR=DS VM=1 Beam Meterset</summary>
        public readonly static DicomTagDS BeamMeterset = new DicomTagDS(0x300A, 0x0086);

        ///<summary>(300A,0088) VR=FL VM=1 Beam Dose Point Depth</summary>
        public readonly static DicomTagFL BeamDosePointDepth = new DicomTagFL(0x300A, 0x0088);

        ///<summary>(300A,0089) VR=FL VM=1 Beam Dose Point Equivalent Depth</summary>
        public readonly static DicomTagFL BeamDosePointEquivalentDepth = new DicomTagFL(0x300A, 0x0089);

        ///<summary>(300A,008A) VR=FL VM=1 Beam Dose Point SSD</summary>
        public readonly static DicomTagFL BeamDosePointSSD = new DicomTagFL(0x300A, 0x008A);

        ///<summary>(300A,008B) VR=CS VM=1 Beam Dose Meaning</summary>
        public readonly static DicomTagCS BeamDoseMeaning = new DicomTagCS(0x300A, 0x008B);

        ///<summary>(300A,008C) VR=SQ VM=1 Beam Dose Verification Control Point Sequence</summary>
        public readonly static DicomTagSQ BeamDoseVerificationControlPointSequence = new DicomTagSQ(0x300A, 0x008C);

        ///<summary>(300A,008D) VR=FL VM=1 Average Beam Dose Point Depth (RETIRED)</summary>
        public readonly static DicomTagFL AverageBeamDosePointDepthRETIRED = new DicomTagFL(0x300A, 0x008D);

        ///<summary>(300A,008E) VR=FL VM=1 Average Beam Dose Point Equivalent Depth (RETIRED)</summary>
        public readonly static DicomTagFL AverageBeamDosePointEquivalentDepthRETIRED = new DicomTagFL(0x300A, 0x008E);

        ///<summary>(300A,008F) VR=FL VM=1 Average Beam Dose Point SSD (RETIRED)</summary>
        public readonly static DicomTagFL AverageBeamDosePointSSDRETIRED = new DicomTagFL(0x300A, 0x008F);

        ///<summary>(300A,0090) VR=CS VM=1 Beam Dose Type</summary>
        public readonly static DicomTagCS BeamDoseType = new DicomTagCS(0x300A, 0x0090);

        ///<summary>(300A,0091) VR=DS VM=1 Alternate Beam Dose</summary>
        public readonly static DicomTagDS AlternateBeamDose = new DicomTagDS(0x300A, 0x0091);

        ///<summary>(300A,0092) VR=CS VM=1 Alternate Beam Dose Type</summary>
        public readonly static DicomTagCS AlternateBeamDoseType = new DicomTagCS(0x300A, 0x0092);

        ///<summary>(300A,0093) VR=CS VM=1 Depth Value Averaging Flag</summary>
        public readonly static DicomTagCS DepthValueAveragingFlag = new DicomTagCS(0x300A, 0x0093);

        ///<summary>(300A,0094) VR=DS VM=1 Beam Dose Point Source to External Contour Distance</summary>
        public readonly static DicomTagDS BeamDosePointSourceToExternalContourDistance = new DicomTagDS(0x300A, 0x0094);

        ///<summary>(300A,00A0) VR=IS VM=1 Number of Brachy Application Setups</summary>
        public readonly static DicomTagIS NumberOfBrachyApplicationSetups = new DicomTagIS(0x300A, 0x00A0);

        ///<summary>(300A,00A2) VR=DS VM=3 Brachy Application Setup Dose Specification Point</summary>
        public readonly static DicomTagDSs BrachyApplicationSetupDoseSpecificationPoint = new DicomTagDSs(0x300A, 0x00A2);

        ///<summary>(300A,00A4) VR=DS VM=1 Brachy Application Setup Dose</summary>
        public readonly static DicomTagDS BrachyApplicationSetupDose = new DicomTagDS(0x300A, 0x00A4);

        ///<summary>(300A,00B0) VR=SQ VM=1 Beam Sequence</summary>
        public readonly static DicomTagSQ BeamSequence = new DicomTagSQ(0x300A, 0x00B0);

        ///<summary>(300A,00B2) VR=SH VM=1 Treatment Machine Name</summary>
        public readonly static DicomTagSH TreatmentMachineName = new DicomTagSH(0x300A, 0x00B2);

        ///<summary>(300A,00B3) VR=CS VM=1 Primary Dosimeter Unit</summary>
        public readonly static DicomTagCS PrimaryDosimeterUnit = new DicomTagCS(0x300A, 0x00B3);

        ///<summary>(300A,00B4) VR=DS VM=1 Source-Axis Distance</summary>
        public readonly static DicomTagDS SourceAxisDistance = new DicomTagDS(0x300A, 0x00B4);

        ///<summary>(300A,00B6) VR=SQ VM=1 Beam Limiting Device Sequence</summary>
        public readonly static DicomTagSQ BeamLimitingDeviceSequence = new DicomTagSQ(0x300A, 0x00B6);

        ///<summary>(300A,00B8) VR=CS VM=1 RT Beam Limiting Device Type</summary>
        public readonly static DicomTagCS RTBeamLimitingDeviceType = new DicomTagCS(0x300A, 0x00B8);

        ///<summary>(300A,00BA) VR=DS VM=1 Source to Beam Limiting Device Distance</summary>
        public readonly static DicomTagDS SourceToBeamLimitingDeviceDistance = new DicomTagDS(0x300A, 0x00BA);

        ///<summary>(300A,00BB) VR=FL VM=1 Isocenter to Beam Limiting Device Distance</summary>
        public readonly static DicomTagFL IsocenterToBeamLimitingDeviceDistance = new DicomTagFL(0x300A, 0x00BB);

        ///<summary>(300A,00BC) VR=IS VM=1 Number of Leaf/Jaw Pairs</summary>
        public readonly static DicomTagIS NumberOfLeafJawPairs = new DicomTagIS(0x300A, 0x00BC);

        ///<summary>(300A,00BE) VR=DS VM=3-n Leaf Position Boundaries</summary>
        public readonly static DicomTagDSs LeafPositionBoundaries = new DicomTagDSs(0x300A, 0x00BE);

        ///<summary>(300A,00C0) VR=IS VM=1 Beam Number</summary>
        public readonly static DicomTagIS BeamNumber = new DicomTagIS(0x300A, 0x00C0);

        ///<summary>(300A,00C2) VR=LO VM=1 Beam Name</summary>
        public readonly static DicomTagLO BeamName = new DicomTagLO(0x300A, 0x00C2);

        ///<summary>(300A,00C3) VR=ST VM=1 Beam Description</summary>
        public readonly static DicomTagST BeamDescription = new DicomTagST(0x300A, 0x00C3);

        ///<summary>(300A,00C4) VR=CS VM=1 Beam Type</summary>
        public readonly static DicomTagCS BeamType = new DicomTagCS(0x300A, 0x00C4);

        ///<summary>(300A,00C5) VR=FD VM=1 Beam Delivery Duration Limit</summary>
        public readonly static DicomTagFD BeamDeliveryDurationLimit = new DicomTagFD(0x300A, 0x00C5);

        ///<summary>(300A,00C6) VR=CS VM=1 Radiation Type</summary>
        public readonly static DicomTagCS RadiationType = new DicomTagCS(0x300A, 0x00C6);

        ///<summary>(300A,00C7) VR=CS VM=1 High-Dose Technique Type</summary>
        public readonly static DicomTagCS HighDoseTechniqueType = new DicomTagCS(0x300A, 0x00C7);

        ///<summary>(300A,00C8) VR=IS VM=1 Reference Image Number</summary>
        public readonly static DicomTagIS ReferenceImageNumber = new DicomTagIS(0x300A, 0x00C8);

        ///<summary>(300A,00CA) VR=SQ VM=1 Planned Verification Image Sequence</summary>
        public readonly static DicomTagSQ PlannedVerificationImageSequence = new DicomTagSQ(0x300A, 0x00CA);

        ///<summary>(300A,00CC) VR=LO VM=1-n Imaging Device-Specific Acquisition Parameters</summary>
        public readonly static DicomTagLOs ImagingDeviceSpecificAcquisitionParameters = new DicomTagLOs(0x300A, 0x00CC);

        ///<summary>(300A,00CE) VR=CS VM=1 Treatment Delivery Type</summary>
        public readonly static DicomTagCS TreatmentDeliveryType = new DicomTagCS(0x300A, 0x00CE);

        ///<summary>(300A,00D0) VR=IS VM=1 Number of Wedges</summary>
        public readonly static DicomTagIS NumberOfWedges = new DicomTagIS(0x300A, 0x00D0);

        ///<summary>(300A,00D1) VR=SQ VM=1 Wedge Sequence</summary>
        public readonly static DicomTagSQ WedgeSequence = new DicomTagSQ(0x300A, 0x00D1);

        ///<summary>(300A,00D2) VR=IS VM=1 Wedge Number</summary>
        public readonly static DicomTagIS WedgeNumber = new DicomTagIS(0x300A, 0x00D2);

        ///<summary>(300A,00D3) VR=CS VM=1 Wedge Type</summary>
        public readonly static DicomTagCS WedgeType = new DicomTagCS(0x300A, 0x00D3);

        ///<summary>(300A,00D4) VR=SH VM=1 Wedge ID</summary>
        public readonly static DicomTagSH WedgeID = new DicomTagSH(0x300A, 0x00D4);

        ///<summary>(300A,00D5) VR=IS VM=1 Wedge Angle</summary>
        public readonly static DicomTagIS WedgeAngle = new DicomTagIS(0x300A, 0x00D5);

        ///<summary>(300A,00D6) VR=DS VM=1 Wedge Factor</summary>
        public readonly static DicomTagDS WedgeFactor = new DicomTagDS(0x300A, 0x00D6);

        ///<summary>(300A,00D7) VR=FL VM=1 Total Wedge Tray Water-Equivalent Thickness</summary>
        public readonly static DicomTagFL TotalWedgeTrayWaterEquivalentThickness = new DicomTagFL(0x300A, 0x00D7);

        ///<summary>(300A,00D8) VR=DS VM=1 Wedge Orientation</summary>
        public readonly static DicomTagDS WedgeOrientation = new DicomTagDS(0x300A, 0x00D8);

        ///<summary>(300A,00D9) VR=FL VM=1 Isocenter to Wedge Tray Distance</summary>
        public readonly static DicomTagFL IsocenterToWedgeTrayDistance = new DicomTagFL(0x300A, 0x00D9);

        ///<summary>(300A,00DA) VR=DS VM=1 Source to Wedge Tray Distance</summary>
        public readonly static DicomTagDS SourceToWedgeTrayDistance = new DicomTagDS(0x300A, 0x00DA);

        ///<summary>(300A,00DB) VR=FL VM=1 Wedge Thin Edge Position</summary>
        public readonly static DicomTagFL WedgeThinEdgePosition = new DicomTagFL(0x300A, 0x00DB);

        ///<summary>(300A,00DC) VR=SH VM=1 Bolus ID</summary>
        public readonly static DicomTagSH BolusID = new DicomTagSH(0x300A, 0x00DC);

        ///<summary>(300A,00DD) VR=ST VM=1 Bolus Description</summary>
        public readonly static DicomTagST BolusDescription = new DicomTagST(0x300A, 0x00DD);

        ///<summary>(300A,00DE) VR=DS VM=1 Effective Wedge Angle</summary>
        public readonly static DicomTagDS EffectiveWedgeAngle = new DicomTagDS(0x300A, 0x00DE);

        ///<summary>(300A,00E0) VR=IS VM=1 Number of Compensators</summary>
        public readonly static DicomTagIS NumberOfCompensators = new DicomTagIS(0x300A, 0x00E0);

        ///<summary>(300A,00E1) VR=SH VM=1 Material ID</summary>
        public readonly static DicomTagSH MaterialID = new DicomTagSH(0x300A, 0x00E1);

        ///<summary>(300A,00E2) VR=DS VM=1 Total Compensator Tray Factor</summary>
        public readonly static DicomTagDS TotalCompensatorTrayFactor = new DicomTagDS(0x300A, 0x00E2);

        ///<summary>(300A,00E3) VR=SQ VM=1 Compensator Sequence</summary>
        public readonly static DicomTagSQ CompensatorSequence = new DicomTagSQ(0x300A, 0x00E3);

        ///<summary>(300A,00E4) VR=IS VM=1 Compensator Number</summary>
        public readonly static DicomTagIS CompensatorNumber = new DicomTagIS(0x300A, 0x00E4);

        ///<summary>(300A,00E5) VR=SH VM=1 Compensator ID</summary>
        public readonly static DicomTagSH CompensatorID = new DicomTagSH(0x300A, 0x00E5);

        ///<summary>(300A,00E6) VR=DS VM=1 Source to Compensator Tray Distance</summary>
        public readonly static DicomTagDS SourceToCompensatorTrayDistance = new DicomTagDS(0x300A, 0x00E6);

        ///<summary>(300A,00E7) VR=IS VM=1 Compensator Rows</summary>
        public readonly static DicomTagIS CompensatorRows = new DicomTagIS(0x300A, 0x00E7);

        ///<summary>(300A,00E8) VR=IS VM=1 Compensator Columns</summary>
        public readonly static DicomTagIS CompensatorColumns = new DicomTagIS(0x300A, 0x00E8);

        ///<summary>(300A,00E9) VR=DS VM=2 Compensator Pixel Spacing</summary>
        public readonly static DicomTagDSs CompensatorPixelSpacing = new DicomTagDSs(0x300A, 0x00E9);

        ///<summary>(300A,00EA) VR=DS VM=2 Compensator Position</summary>
        public readonly static DicomTagDSs CompensatorPosition = new DicomTagDSs(0x300A, 0x00EA);

        ///<summary>(300A,00EB) VR=DS VM=1-n Compensator Transmission Data</summary>
        public readonly static DicomTagDSs CompensatorTransmissionData = new DicomTagDSs(0x300A, 0x00EB);

        ///<summary>(300A,00EC) VR=DS VM=1-n Compensator Thickness Data</summary>
        public readonly static DicomTagDSs CompensatorThicknessData = new DicomTagDSs(0x300A, 0x00EC);

        ///<summary>(300A,00ED) VR=IS VM=1 Number of Boli</summary>
        public readonly static DicomTagIS NumberOfBoli = new DicomTagIS(0x300A, 0x00ED);

        ///<summary>(300A,00EE) VR=CS VM=1 Compensator Type</summary>
        public readonly static DicomTagCS CompensatorType = new DicomTagCS(0x300A, 0x00EE);

        ///<summary>(300A,00EF) VR=SH VM=1 Compensator Tray ID</summary>
        public readonly static DicomTagSH CompensatorTrayID = new DicomTagSH(0x300A, 0x00EF);

        ///<summary>(300A,00F0) VR=IS VM=1 Number of Blocks</summary>
        public readonly static DicomTagIS NumberOfBlocks = new DicomTagIS(0x300A, 0x00F0);

        ///<summary>(300A,00F2) VR=DS VM=1 Total Block Tray Factor</summary>
        public readonly static DicomTagDS TotalBlockTrayFactor = new DicomTagDS(0x300A, 0x00F2);

        ///<summary>(300A,00F3) VR=FL VM=1 Total Block Tray Water-Equivalent Thickness</summary>
        public readonly static DicomTagFL TotalBlockTrayWaterEquivalentThickness = new DicomTagFL(0x300A, 0x00F3);

        ///<summary>(300A,00F4) VR=SQ VM=1 Block Sequence</summary>
        public readonly static DicomTagSQ BlockSequence = new DicomTagSQ(0x300A, 0x00F4);

        ///<summary>(300A,00F5) VR=SH VM=1 Block Tray ID</summary>
        public readonly static DicomTagSH BlockTrayID = new DicomTagSH(0x300A, 0x00F5);

        ///<summary>(300A,00F6) VR=DS VM=1 Source to Block Tray Distance</summary>
        public readonly static DicomTagDS SourceToBlockTrayDistance = new DicomTagDS(0x300A, 0x00F6);

        ///<summary>(300A,00F7) VR=FL VM=1 Isocenter to Block Tray Distance</summary>
        public readonly static DicomTagFL IsocenterToBlockTrayDistance = new DicomTagFL(0x300A, 0x00F7);

        ///<summary>(300A,00F8) VR=CS VM=1 Block Type</summary>
        public readonly static DicomTagCS BlockType = new DicomTagCS(0x300A, 0x00F8);

        ///<summary>(300A,00F9) VR=LO VM=1 Accessory Code</summary>
        public readonly static DicomTagLO AccessoryCode = new DicomTagLO(0x300A, 0x00F9);

        ///<summary>(300A,00FA) VR=CS VM=1 Block Divergence</summary>
        public readonly static DicomTagCS BlockDivergence = new DicomTagCS(0x300A, 0x00FA);

        ///<summary>(300A,00FB) VR=CS VM=1 Block Mounting Position</summary>
        public readonly static DicomTagCS BlockMountingPosition = new DicomTagCS(0x300A, 0x00FB);

        ///<summary>(300A,00FC) VR=IS VM=1 Block Number</summary>
        public readonly static DicomTagIS BlockNumber = new DicomTagIS(0x300A, 0x00FC);

        ///<summary>(300A,00FE) VR=LO VM=1 Block Name</summary>
        public readonly static DicomTagLO BlockName = new DicomTagLO(0x300A, 0x00FE);

        ///<summary>(300A,0100) VR=DS VM=1 Block Thickness</summary>
        public readonly static DicomTagDS BlockThickness = new DicomTagDS(0x300A, 0x0100);

        ///<summary>(300A,0102) VR=DS VM=1 Block Transmission</summary>
        public readonly static DicomTagDS BlockTransmission = new DicomTagDS(0x300A, 0x0102);

        ///<summary>(300A,0104) VR=IS VM=1 Block Number of Points</summary>
        public readonly static DicomTagIS BlockNumberOfPoints = new DicomTagIS(0x300A, 0x0104);

        ///<summary>(300A,0106) VR=DS VM=2-2n Block Data</summary>
        public readonly static DicomTagDSs BlockData = new DicomTagDSs(0x300A, 0x0106);

        ///<summary>(300A,0107) VR=SQ VM=1 Applicator Sequence</summary>
        public readonly static DicomTagSQ ApplicatorSequence = new DicomTagSQ(0x300A, 0x0107);

        ///<summary>(300A,0108) VR=SH VM=1 Applicator ID</summary>
        public readonly static DicomTagSH ApplicatorID = new DicomTagSH(0x300A, 0x0108);

        ///<summary>(300A,0109) VR=CS VM=1 Applicator Type</summary>
        public readonly static DicomTagCS ApplicatorType = new DicomTagCS(0x300A, 0x0109);

        ///<summary>(300A,010A) VR=LO VM=1 Applicator Description</summary>
        public readonly static DicomTagLO ApplicatorDescription = new DicomTagLO(0x300A, 0x010A);

        ///<summary>(300A,010C) VR=DS VM=1 Cumulative Dose Reference Coefficient</summary>
        public readonly static DicomTagDS CumulativeDoseReferenceCoefficient = new DicomTagDS(0x300A, 0x010C);

        ///<summary>(300A,010E) VR=DS VM=1 Final Cumulative Meterset Weight</summary>
        public readonly static DicomTagDS FinalCumulativeMetersetWeight = new DicomTagDS(0x300A, 0x010E);

        ///<summary>(300A,0110) VR=IS VM=1 Number of Control Points</summary>
        public readonly static DicomTagIS NumberOfControlPoints = new DicomTagIS(0x300A, 0x0110);

        ///<summary>(300A,0111) VR=SQ VM=1 Control Point Sequence</summary>
        public readonly static DicomTagSQ ControlPointSequence = new DicomTagSQ(0x300A, 0x0111);

        ///<summary>(300A,0112) VR=IS VM=1 Control Point Index</summary>
        public readonly static DicomTagIS ControlPointIndex = new DicomTagIS(0x300A, 0x0112);

        ///<summary>(300A,0114) VR=DS VM=1 Nominal Beam Energy</summary>
        public readonly static DicomTagDS NominalBeamEnergy = new DicomTagDS(0x300A, 0x0114);

        ///<summary>(300A,0115) VR=DS VM=1 Dose Rate Set</summary>
        public readonly static DicomTagDS DoseRateSet = new DicomTagDS(0x300A, 0x0115);

        ///<summary>(300A,0116) VR=SQ VM=1 Wedge Position Sequence</summary>
        public readonly static DicomTagSQ WedgePositionSequence = new DicomTagSQ(0x300A, 0x0116);

        ///<summary>(300A,0118) VR=CS VM=1 Wedge Position</summary>
        public readonly static DicomTagCS WedgePosition = new DicomTagCS(0x300A, 0x0118);

        ///<summary>(300A,011A) VR=SQ VM=1 Beam Limiting Device Position Sequence</summary>
        public readonly static DicomTagSQ BeamLimitingDevicePositionSequence = new DicomTagSQ(0x300A, 0x011A);

        ///<summary>(300A,011C) VR=DS VM=2-2n Leaf/Jaw Positions</summary>
        public readonly static DicomTagDSs LeafJawPositions = new DicomTagDSs(0x300A, 0x011C);

        ///<summary>(300A,011E) VR=DS VM=1 Gantry Angle</summary>
        public readonly static DicomTagDS GantryAngle = new DicomTagDS(0x300A, 0x011E);

        ///<summary>(300A,011F) VR=CS VM=1 Gantry Rotation Direction</summary>
        public readonly static DicomTagCS GantryRotationDirection = new DicomTagCS(0x300A, 0x011F);

        ///<summary>(300A,0120) VR=DS VM=1 Beam Limiting Device Angle</summary>
        public readonly static DicomTagDS BeamLimitingDeviceAngle = new DicomTagDS(0x300A, 0x0120);

        ///<summary>(300A,0121) VR=CS VM=1 Beam Limiting Device Rotation Direction</summary>
        public readonly static DicomTagCS BeamLimitingDeviceRotationDirection = new DicomTagCS(0x300A, 0x0121);

        ///<summary>(300A,0122) VR=DS VM=1 Patient Support Angle</summary>
        public readonly static DicomTagDS PatientSupportAngle = new DicomTagDS(0x300A, 0x0122);

        ///<summary>(300A,0123) VR=CS VM=1 Patient Support Rotation Direction</summary>
        public readonly static DicomTagCS PatientSupportRotationDirection = new DicomTagCS(0x300A, 0x0123);

        ///<summary>(300A,0124) VR=DS VM=1 Table Top Eccentric Axis Distance</summary>
        public readonly static DicomTagDS TableTopEccentricAxisDistance = new DicomTagDS(0x300A, 0x0124);

        ///<summary>(300A,0125) VR=DS VM=1 Table Top Eccentric Angle</summary>
        public readonly static DicomTagDS TableTopEccentricAngle = new DicomTagDS(0x300A, 0x0125);

        ///<summary>(300A,0126) VR=CS VM=1 Table Top Eccentric Rotation Direction</summary>
        public readonly static DicomTagCS TableTopEccentricRotationDirection = new DicomTagCS(0x300A, 0x0126);

        ///<summary>(300A,0128) VR=DS VM=1 Table Top Vertical Position</summary>
        public readonly static DicomTagDS TableTopVerticalPosition = new DicomTagDS(0x300A, 0x0128);

        ///<summary>(300A,0129) VR=DS VM=1 Table Top Longitudinal Position</summary>
        public readonly static DicomTagDS TableTopLongitudinalPosition = new DicomTagDS(0x300A, 0x0129);

        ///<summary>(300A,012A) VR=DS VM=1 Table Top Lateral Position</summary>
        public readonly static DicomTagDS TableTopLateralPosition = new DicomTagDS(0x300A, 0x012A);

        ///<summary>(300A,012C) VR=DS VM=3 Isocenter Position</summary>
        public readonly static DicomTagDSs IsocenterPosition = new DicomTagDSs(0x300A, 0x012C);

        ///<summary>(300A,012E) VR=DS VM=3 Surface Entry Point</summary>
        public readonly static DicomTagDSs SurfaceEntryPoint = new DicomTagDSs(0x300A, 0x012E);

        ///<summary>(300A,0130) VR=DS VM=1 Source to Surface Distance</summary>
        public readonly static DicomTagDS SourceToSurfaceDistance = new DicomTagDS(0x300A, 0x0130);

        ///<summary>(300A,0131) VR=FL VM=1 Average Beam Dose Point Source to External Contour Distance</summary>
        public readonly static DicomTagFL AverageBeamDosePointSourceToExternalContourDistance = new DicomTagFL(0x300A, 0x0131);

        ///<summary>(300A,0132) VR=FL VM=1 Source to External Contour Distance</summary>
        public readonly static DicomTagFL SourceToExternalContourDistance = new DicomTagFL(0x300A, 0x0132);

        ///<summary>(300A,0133) VR=FL VM=3 External Contour Entry Point</summary>
        public readonly static DicomTagFLs ExternalContourEntryPoint = new DicomTagFLs(0x300A, 0x0133);

        ///<summary>(300A,0134) VR=DS VM=1 Cumulative Meterset Weight</summary>
        public readonly static DicomTagDS CumulativeMetersetWeight = new DicomTagDS(0x300A, 0x0134);

        ///<summary>(300A,0140) VR=FL VM=1 Table Top Pitch Angle</summary>
        public readonly static DicomTagFL TableTopPitchAngle = new DicomTagFL(0x300A, 0x0140);

        ///<summary>(300A,0142) VR=CS VM=1 Table Top Pitch Rotation Direction</summary>
        public readonly static DicomTagCS TableTopPitchRotationDirection = new DicomTagCS(0x300A, 0x0142);

        ///<summary>(300A,0144) VR=FL VM=1 Table Top Roll Angle</summary>
        public readonly static DicomTagFL TableTopRollAngle = new DicomTagFL(0x300A, 0x0144);

        ///<summary>(300A,0146) VR=CS VM=1 Table Top Roll Rotation Direction</summary>
        public readonly static DicomTagCS TableTopRollRotationDirection = new DicomTagCS(0x300A, 0x0146);

        ///<summary>(300A,0148) VR=FL VM=1 Head Fixation Angle</summary>
        public readonly static DicomTagFL HeadFixationAngle = new DicomTagFL(0x300A, 0x0148);

        ///<summary>(300A,014A) VR=FL VM=1 Gantry Pitch Angle</summary>
        public readonly static DicomTagFL GantryPitchAngle = new DicomTagFL(0x300A, 0x014A);

        ///<summary>(300A,014C) VR=CS VM=1 Gantry Pitch Rotation Direction</summary>
        public readonly static DicomTagCS GantryPitchRotationDirection = new DicomTagCS(0x300A, 0x014C);

        ///<summary>(300A,014E) VR=FL VM=1 Gantry Pitch Angle Tolerance</summary>
        public readonly static DicomTagFL GantryPitchAngleTolerance = new DicomTagFL(0x300A, 0x014E);

        ///<summary>(300A,0150) VR=CS VM=1 Fixation Eye</summary>
        public readonly static DicomTagCS FixationEye = new DicomTagCS(0x300A, 0x0150);

        ///<summary>(300A,0151) VR=DS VM=1 Chair Head Frame Position</summary>
        public readonly static DicomTagDS ChairHeadFramePosition = new DicomTagDS(0x300A, 0x0151);

        ///<summary>(300A,0152) VR=DS VM=1 Head Fixation Angle Tolerance</summary>
        public readonly static DicomTagDS HeadFixationAngleTolerance = new DicomTagDS(0x300A, 0x0152);

        ///<summary>(300A,0153) VR=DS VM=1 Chair Head Frame Position Tolerance</summary>
        public readonly static DicomTagDS ChairHeadFramePositionTolerance = new DicomTagDS(0x300A, 0x0153);

        ///<summary>(300A,0154) VR=DS VM=1 Fixation Light Azimuthal Angle Tolerance</summary>
        public readonly static DicomTagDS FixationLightAzimuthalAngleTolerance = new DicomTagDS(0x300A, 0x0154);

        ///<summary>(300A,0155) VR=DS VM=1 Fixation Light Polar Angle Tolerance</summary>
        public readonly static DicomTagDS FixationLightPolarAngleTolerance = new DicomTagDS(0x300A, 0x0155);

        ///<summary>(300A,0180) VR=SQ VM=1 Patient Setup Sequence</summary>
        public readonly static DicomTagSQ PatientSetupSequence = new DicomTagSQ(0x300A, 0x0180);

        ///<summary>(300A,0182) VR=IS VM=1 Patient Setup Number</summary>
        public readonly static DicomTagIS PatientSetupNumber = new DicomTagIS(0x300A, 0x0182);

        ///<summary>(300A,0183) VR=LO VM=1 Patient Setup Label</summary>
        public readonly static DicomTagLO PatientSetupLabel = new DicomTagLO(0x300A, 0x0183);

        ///<summary>(300A,0184) VR=LO VM=1 Patient Additional Position</summary>
        public readonly static DicomTagLO PatientAdditionalPosition = new DicomTagLO(0x300A, 0x0184);

        ///<summary>(300A,0190) VR=SQ VM=1 Fixation Device Sequence</summary>
        public readonly static DicomTagSQ FixationDeviceSequence = new DicomTagSQ(0x300A, 0x0190);

        ///<summary>(300A,0192) VR=CS VM=1 Fixation Device Type</summary>
        public readonly static DicomTagCS FixationDeviceType = new DicomTagCS(0x300A, 0x0192);

        ///<summary>(300A,0194) VR=SH VM=1 Fixation Device Label</summary>
        public readonly static DicomTagSH FixationDeviceLabel = new DicomTagSH(0x300A, 0x0194);

        ///<summary>(300A,0196) VR=ST VM=1 Fixation Device Description</summary>
        public readonly static DicomTagST FixationDeviceDescription = new DicomTagST(0x300A, 0x0196);

        ///<summary>(300A,0198) VR=SH VM=1 Fixation Device Position</summary>
        public readonly static DicomTagSH FixationDevicePosition = new DicomTagSH(0x300A, 0x0198);

        ///<summary>(300A,0199) VR=FL VM=1 Fixation Device Pitch Angle</summary>
        public readonly static DicomTagFL FixationDevicePitchAngle = new DicomTagFL(0x300A, 0x0199);

        ///<summary>(300A,019A) VR=FL VM=1 Fixation Device Roll Angle</summary>
        public readonly static DicomTagFL FixationDeviceRollAngle = new DicomTagFL(0x300A, 0x019A);

        ///<summary>(300A,01A0) VR=SQ VM=1 Shielding Device Sequence</summary>
        public readonly static DicomTagSQ ShieldingDeviceSequence = new DicomTagSQ(0x300A, 0x01A0);

        ///<summary>(300A,01A2) VR=CS VM=1 Shielding Device Type</summary>
        public readonly static DicomTagCS ShieldingDeviceType = new DicomTagCS(0x300A, 0x01A2);

        ///<summary>(300A,01A4) VR=SH VM=1 Shielding Device Label</summary>
        public readonly static DicomTagSH ShieldingDeviceLabel = new DicomTagSH(0x300A, 0x01A4);

        ///<summary>(300A,01A6) VR=ST VM=1 Shielding Device Description</summary>
        public readonly static DicomTagST ShieldingDeviceDescription = new DicomTagST(0x300A, 0x01A6);

        ///<summary>(300A,01A8) VR=SH VM=1 Shielding Device Position</summary>
        public readonly static DicomTagSH ShieldingDevicePosition = new DicomTagSH(0x300A, 0x01A8);

        ///<summary>(300A,01B0) VR=CS VM=1 Setup Technique</summary>
        public readonly static DicomTagCS SetupTechnique = new DicomTagCS(0x300A, 0x01B0);

        ///<summary>(300A,01B2) VR=ST VM=1 Setup Technique Description</summary>
        public readonly static DicomTagST SetupTechniqueDescription = new DicomTagST(0x300A, 0x01B2);

        ///<summary>(300A,01B4) VR=SQ VM=1 Setup Device Sequence</summary>
        public readonly static DicomTagSQ SetupDeviceSequence = new DicomTagSQ(0x300A, 0x01B4);

        ///<summary>(300A,01B6) VR=CS VM=1 Setup Device Type</summary>
        public readonly static DicomTagCS SetupDeviceType = new DicomTagCS(0x300A, 0x01B6);

        ///<summary>(300A,01B8) VR=SH VM=1 Setup Device Label</summary>
        public readonly static DicomTagSH SetupDeviceLabel = new DicomTagSH(0x300A, 0x01B8);

        ///<summary>(300A,01BA) VR=ST VM=1 Setup Device Description</summary>
        public readonly static DicomTagST SetupDeviceDescription = new DicomTagST(0x300A, 0x01BA);

        ///<summary>(300A,01BC) VR=DS VM=1 Setup Device Parameter</summary>
        public readonly static DicomTagDS SetupDeviceParameter = new DicomTagDS(0x300A, 0x01BC);

        ///<summary>(300A,01D0) VR=ST VM=1 Setup Reference Description</summary>
        public readonly static DicomTagST SetupReferenceDescription = new DicomTagST(0x300A, 0x01D0);

        ///<summary>(300A,01D2) VR=DS VM=1 Table Top Vertical Setup Displacement</summary>
        public readonly static DicomTagDS TableTopVerticalSetupDisplacement = new DicomTagDS(0x300A, 0x01D2);

        ///<summary>(300A,01D4) VR=DS VM=1 Table Top Longitudinal Setup Displacement</summary>
        public readonly static DicomTagDS TableTopLongitudinalSetupDisplacement = new DicomTagDS(0x300A, 0x01D4);

        ///<summary>(300A,01D6) VR=DS VM=1 Table Top Lateral Setup Displacement</summary>
        public readonly static DicomTagDS TableTopLateralSetupDisplacement = new DicomTagDS(0x300A, 0x01D6);

        ///<summary>(300A,0200) VR=CS VM=1 Brachy Treatment Technique</summary>
        public readonly static DicomTagCS BrachyTreatmentTechnique = new DicomTagCS(0x300A, 0x0200);

        ///<summary>(300A,0202) VR=CS VM=1 Brachy Treatment Type</summary>
        public readonly static DicomTagCS BrachyTreatmentType = new DicomTagCS(0x300A, 0x0202);

        ///<summary>(300A,0206) VR=SQ VM=1 Treatment Machine Sequence</summary>
        public readonly static DicomTagSQ TreatmentMachineSequence = new DicomTagSQ(0x300A, 0x0206);

        ///<summary>(300A,0210) VR=SQ VM=1 Source Sequence</summary>
        public readonly static DicomTagSQ SourceSequence = new DicomTagSQ(0x300A, 0x0210);

        ///<summary>(300A,0212) VR=IS VM=1 Source Number</summary>
        public readonly static DicomTagIS SourceNumber = new DicomTagIS(0x300A, 0x0212);

        ///<summary>(300A,0214) VR=CS VM=1 Source Type</summary>
        public readonly static DicomTagCS SourceType = new DicomTagCS(0x300A, 0x0214);

        ///<summary>(300A,0216) VR=LO VM=1 Source Manufacturer</summary>
        public readonly static DicomTagLO SourceManufacturer = new DicomTagLO(0x300A, 0x0216);

        ///<summary>(300A,0218) VR=DS VM=1 Active Source Diameter</summary>
        public readonly static DicomTagDS ActiveSourceDiameter = new DicomTagDS(0x300A, 0x0218);

        ///<summary>(300A,021A) VR=DS VM=1 Active Source Length</summary>
        public readonly static DicomTagDS ActiveSourceLength = new DicomTagDS(0x300A, 0x021A);

        ///<summary>(300A,021B) VR=SH VM=1 Source Model ID</summary>
        public readonly static DicomTagSH SourceModelID = new DicomTagSH(0x300A, 0x021B);

        ///<summary>(300A,021C) VR=LO VM=1 Source Description</summary>
        public readonly static DicomTagLO SourceDescription = new DicomTagLO(0x300A, 0x021C);

        ///<summary>(300A,0222) VR=DS VM=1 Source Encapsulation Nominal Thickness</summary>
        public readonly static DicomTagDS SourceEncapsulationNominalThickness = new DicomTagDS(0x300A, 0x0222);

        ///<summary>(300A,0224) VR=DS VM=1 Source Encapsulation Nominal Transmission</summary>
        public readonly static DicomTagDS SourceEncapsulationNominalTransmission = new DicomTagDS(0x300A, 0x0224);

        ///<summary>(300A,0226) VR=LO VM=1 Source Isotope Name</summary>
        public readonly static DicomTagLO SourceIsotopeName = new DicomTagLO(0x300A, 0x0226);

        ///<summary>(300A,0228) VR=DS VM=1 Source Isotope Half Life</summary>
        public readonly static DicomTagDS SourceIsotopeHalfLife = new DicomTagDS(0x300A, 0x0228);

        ///<summary>(300A,0229) VR=CS VM=1 Source Strength Units</summary>
        public readonly static DicomTagCS SourceStrengthUnits = new DicomTagCS(0x300A, 0x0229);

        ///<summary>(300A,022A) VR=DS VM=1 Reference Air Kerma Rate</summary>
        public readonly static DicomTagDS ReferenceAirKermaRate = new DicomTagDS(0x300A, 0x022A);

        ///<summary>(300A,022B) VR=DS VM=1 Source Strength</summary>
        public readonly static DicomTagDS SourceStrength = new DicomTagDS(0x300A, 0x022B);

        ///<summary>(300A,022C) VR=DA VM=1 Source Strength Reference Date</summary>
        public readonly static DicomTagDA SourceStrengthReferenceDate = new DicomTagDA(0x300A, 0x022C);

        ///<summary>(300A,022E) VR=TM VM=1 Source Strength Reference Time</summary>
        public readonly static DicomTagTM SourceStrengthReferenceTime = new DicomTagTM(0x300A, 0x022E);

        ///<summary>(300A,0230) VR=SQ VM=1 Application Setup Sequence</summary>
        public readonly static DicomTagSQ ApplicationSetupSequence = new DicomTagSQ(0x300A, 0x0230);

        ///<summary>(300A,0232) VR=CS VM=1 Application Setup Type</summary>
        public readonly static DicomTagCS ApplicationSetupType = new DicomTagCS(0x300A, 0x0232);

        ///<summary>(300A,0234) VR=IS VM=1 Application Setup Number</summary>
        public readonly static DicomTagIS ApplicationSetupNumber = new DicomTagIS(0x300A, 0x0234);

        ///<summary>(300A,0236) VR=LO VM=1 Application Setup Name</summary>
        public readonly static DicomTagLO ApplicationSetupName = new DicomTagLO(0x300A, 0x0236);

        ///<summary>(300A,0238) VR=LO VM=1 Application Setup Manufacturer</summary>
        public readonly static DicomTagLO ApplicationSetupManufacturer = new DicomTagLO(0x300A, 0x0238);

        ///<summary>(300A,0240) VR=IS VM=1 Template Number</summary>
        public readonly static DicomTagIS TemplateNumber = new DicomTagIS(0x300A, 0x0240);

        ///<summary>(300A,0242) VR=SH VM=1 Template Type</summary>
        public readonly static DicomTagSH TemplateType = new DicomTagSH(0x300A, 0x0242);

        ///<summary>(300A,0244) VR=LO VM=1 Template Name</summary>
        public readonly static DicomTagLO TemplateName = new DicomTagLO(0x300A, 0x0244);

        ///<summary>(300A,0250) VR=DS VM=1 Total Reference Air Kerma</summary>
        public readonly static DicomTagDS TotalReferenceAirKerma = new DicomTagDS(0x300A, 0x0250);

        ///<summary>(300A,0260) VR=SQ VM=1 Brachy Accessory Device Sequence</summary>
        public readonly static DicomTagSQ BrachyAccessoryDeviceSequence = new DicomTagSQ(0x300A, 0x0260);

        ///<summary>(300A,0262) VR=IS VM=1 Brachy Accessory Device Number</summary>
        public readonly static DicomTagIS BrachyAccessoryDeviceNumber = new DicomTagIS(0x300A, 0x0262);

        ///<summary>(300A,0263) VR=SH VM=1 Brachy Accessory Device ID</summary>
        public readonly static DicomTagSH BrachyAccessoryDeviceID = new DicomTagSH(0x300A, 0x0263);

        ///<summary>(300A,0264) VR=CS VM=1 Brachy Accessory Device Type</summary>
        public readonly static DicomTagCS BrachyAccessoryDeviceType = new DicomTagCS(0x300A, 0x0264);

        ///<summary>(300A,0266) VR=LO VM=1 Brachy Accessory Device Name</summary>
        public readonly static DicomTagLO BrachyAccessoryDeviceName = new DicomTagLO(0x300A, 0x0266);

        ///<summary>(300A,026A) VR=DS VM=1 Brachy Accessory Device Nominal Thickness</summary>
        public readonly static DicomTagDS BrachyAccessoryDeviceNominalThickness = new DicomTagDS(0x300A, 0x026A);

        ///<summary>(300A,026C) VR=DS VM=1 Brachy Accessory Device Nominal Transmission</summary>
        public readonly static DicomTagDS BrachyAccessoryDeviceNominalTransmission = new DicomTagDS(0x300A, 0x026C);

        ///<summary>(300A,0271) VR=DS VM=1 Channel Effective Length</summary>
        public readonly static DicomTagDS ChannelEffectiveLength = new DicomTagDS(0x300A, 0x0271);

        ///<summary>(300A,0272) VR=DS VM=1 Channel Inner Length</summary>
        public readonly static DicomTagDS ChannelInnerLength = new DicomTagDS(0x300A, 0x0272);

        ///<summary>(300A,0273) VR=SH VM=1 Afterloader Channel ID</summary>
        public readonly static DicomTagSH AfterloaderChannelID = new DicomTagSH(0x300A, 0x0273);

        ///<summary>(300A,0274) VR=DS VM=1 Source Applicator Tip Length</summary>
        public readonly static DicomTagDS SourceApplicatorTipLength = new DicomTagDS(0x300A, 0x0274);

        ///<summary>(300A,0280) VR=SQ VM=1 Channel Sequence</summary>
        public readonly static DicomTagSQ ChannelSequence = new DicomTagSQ(0x300A, 0x0280);

        ///<summary>(300A,0282) VR=IS VM=1 Channel Number</summary>
        public readonly static DicomTagIS ChannelNumber = new DicomTagIS(0x300A, 0x0282);

        ///<summary>(300A,0284) VR=DS VM=1 Channel Length</summary>
        public readonly static DicomTagDS ChannelLength = new DicomTagDS(0x300A, 0x0284);

        ///<summary>(300A,0286) VR=DS VM=1 Channel Total Time</summary>
        public readonly static DicomTagDS ChannelTotalTime = new DicomTagDS(0x300A, 0x0286);

        ///<summary>(300A,0288) VR=CS VM=1 Source Movement Type</summary>
        public readonly static DicomTagCS SourceMovementType = new DicomTagCS(0x300A, 0x0288);

        ///<summary>(300A,028A) VR=IS VM=1 Number of Pulses</summary>
        public readonly static DicomTagIS NumberOfPulses = new DicomTagIS(0x300A, 0x028A);

        ///<summary>(300A,028C) VR=DS VM=1 Pulse Repetition Interval</summary>
        public readonly static DicomTagDS PulseRepetitionInterval = new DicomTagDS(0x300A, 0x028C);

        ///<summary>(300A,0290) VR=IS VM=1 Source Applicator Number</summary>
        public readonly static DicomTagIS SourceApplicatorNumber = new DicomTagIS(0x300A, 0x0290);

        ///<summary>(300A,0291) VR=SH VM=1 Source Applicator ID</summary>
        public readonly static DicomTagSH SourceApplicatorID = new DicomTagSH(0x300A, 0x0291);

        ///<summary>(300A,0292) VR=CS VM=1 Source Applicator Type</summary>
        public readonly static DicomTagCS SourceApplicatorType = new DicomTagCS(0x300A, 0x0292);

        ///<summary>(300A,0294) VR=LO VM=1 Source Applicator Name</summary>
        public readonly static DicomTagLO SourceApplicatorName = new DicomTagLO(0x300A, 0x0294);

        ///<summary>(300A,0296) VR=DS VM=1 Source Applicator Length</summary>
        public readonly static DicomTagDS SourceApplicatorLength = new DicomTagDS(0x300A, 0x0296);

        ///<summary>(300A,0298) VR=LO VM=1 Source Applicator Manufacturer</summary>
        public readonly static DicomTagLO SourceApplicatorManufacturer = new DicomTagLO(0x300A, 0x0298);

        ///<summary>(300A,029C) VR=DS VM=1 Source Applicator Wall Nominal Thickness</summary>
        public readonly static DicomTagDS SourceApplicatorWallNominalThickness = new DicomTagDS(0x300A, 0x029C);

        ///<summary>(300A,029E) VR=DS VM=1 Source Applicator Wall Nominal Transmission</summary>
        public readonly static DicomTagDS SourceApplicatorWallNominalTransmission = new DicomTagDS(0x300A, 0x029E);

        ///<summary>(300A,02A0) VR=DS VM=1 Source Applicator Step Size</summary>
        public readonly static DicomTagDS SourceApplicatorStepSize = new DicomTagDS(0x300A, 0x02A0);

        ///<summary>(300A,02A1) VR=IS VM=1 Applicator Shape Referenced ROI Number</summary>
        public readonly static DicomTagIS ApplicatorShapeReferencedROINumber = new DicomTagIS(0x300A, 0x02A1);

        ///<summary>(300A,02A2) VR=IS VM=1 Transfer Tube Number</summary>
        public readonly static DicomTagIS TransferTubeNumber = new DicomTagIS(0x300A, 0x02A2);

        ///<summary>(300A,02A4) VR=DS VM=1 Transfer Tube Length</summary>
        public readonly static DicomTagDS TransferTubeLength = new DicomTagDS(0x300A, 0x02A4);

        ///<summary>(300A,02B0) VR=SQ VM=1 Channel Shield Sequence</summary>
        public readonly static DicomTagSQ ChannelShieldSequence = new DicomTagSQ(0x300A, 0x02B0);

        ///<summary>(300A,02B2) VR=IS VM=1 Channel Shield Number</summary>
        public readonly static DicomTagIS ChannelShieldNumber = new DicomTagIS(0x300A, 0x02B2);

        ///<summary>(300A,02B3) VR=SH VM=1 Channel Shield ID</summary>
        public readonly static DicomTagSH ChannelShieldID = new DicomTagSH(0x300A, 0x02B3);

        ///<summary>(300A,02B4) VR=LO VM=1 Channel Shield Name</summary>
        public readonly static DicomTagLO ChannelShieldName = new DicomTagLO(0x300A, 0x02B4);

        ///<summary>(300A,02B8) VR=DS VM=1 Channel Shield Nominal Thickness</summary>
        public readonly static DicomTagDS ChannelShieldNominalThickness = new DicomTagDS(0x300A, 0x02B8);

        ///<summary>(300A,02BA) VR=DS VM=1 Channel Shield Nominal Transmission</summary>
        public readonly static DicomTagDS ChannelShieldNominalTransmission = new DicomTagDS(0x300A, 0x02BA);

        ///<summary>(300A,02C8) VR=DS VM=1 Final Cumulative Time Weight</summary>
        public readonly static DicomTagDS FinalCumulativeTimeWeight = new DicomTagDS(0x300A, 0x02C8);

        ///<summary>(300A,02D0) VR=SQ VM=1 Brachy Control Point Sequence</summary>
        public readonly static DicomTagSQ BrachyControlPointSequence = new DicomTagSQ(0x300A, 0x02D0);

        ///<summary>(300A,02D2) VR=DS VM=1 Control Point Relative Position</summary>
        public readonly static DicomTagDS ControlPointRelativePosition = new DicomTagDS(0x300A, 0x02D2);

        ///<summary>(300A,02D4) VR=DS VM=3 Control Point 3D Position</summary>
        public readonly static DicomTagDSs ControlPoint3DPosition = new DicomTagDSs(0x300A, 0x02D4);

        ///<summary>(300A,02D6) VR=DS VM=1 Cumulative Time Weight</summary>
        public readonly static DicomTagDS CumulativeTimeWeight = new DicomTagDS(0x300A, 0x02D6);

        ///<summary>(300A,02E0) VR=CS VM=1 Compensator Divergence</summary>
        public readonly static DicomTagCS CompensatorDivergence = new DicomTagCS(0x300A, 0x02E0);

        ///<summary>(300A,02E1) VR=CS VM=1 Compensator Mounting Position</summary>
        public readonly static DicomTagCS CompensatorMountingPosition = new DicomTagCS(0x300A, 0x02E1);

        ///<summary>(300A,02E2) VR=DS VM=1-n Source to Compensator Distance</summary>
        public readonly static DicomTagDSs SourceToCompensatorDistance = new DicomTagDSs(0x300A, 0x02E2);

        ///<summary>(300A,02E3) VR=FL VM=1 Total Compensator Tray Water-Equivalent Thickness</summary>
        public readonly static DicomTagFL TotalCompensatorTrayWaterEquivalentThickness = new DicomTagFL(0x300A, 0x02E3);

        ///<summary>(300A,02E4) VR=FL VM=1 Isocenter to Compensator Tray Distance</summary>
        public readonly static DicomTagFL IsocenterToCompensatorTrayDistance = new DicomTagFL(0x300A, 0x02E4);

        ///<summary>(300A,02E5) VR=FL VM=1 Compensator Column Offset</summary>
        public readonly static DicomTagFL CompensatorColumnOffset = new DicomTagFL(0x300A, 0x02E5);

        ///<summary>(300A,02E6) VR=FL VM=1-n Isocenter to Compensator Distances</summary>
        public readonly static DicomTagFLs IsocenterToCompensatorDistances = new DicomTagFLs(0x300A, 0x02E6);

        ///<summary>(300A,02E7) VR=FL VM=1 Compensator Relative Stopping Power Ratio</summary>
        public readonly static DicomTagFL CompensatorRelativeStoppingPowerRatio = new DicomTagFL(0x300A, 0x02E7);

        ///<summary>(300A,02E8) VR=FL VM=1 Compensator Milling Tool Diameter</summary>
        public readonly static DicomTagFL CompensatorMillingToolDiameter = new DicomTagFL(0x300A, 0x02E8);

        ///<summary>(300A,02EA) VR=SQ VM=1 Ion Range Compensator Sequence</summary>
        public readonly static DicomTagSQ IonRangeCompensatorSequence = new DicomTagSQ(0x300A, 0x02EA);

        ///<summary>(300A,02EB) VR=LT VM=1 Compensator Description</summary>
        public readonly static DicomTagLT CompensatorDescription = new DicomTagLT(0x300A, 0x02EB);

        ///<summary>(300A,02EC) VR=CS VM=1 Compensator Surface Representation Flag</summary>
        public readonly static DicomTagCS CompensatorSurfaceRepresentationFlag = new DicomTagCS(0x300A, 0x02EC);

        ///<summary>(300A,0302) VR=IS VM=1 Radiation Mass Number</summary>
        public readonly static DicomTagIS RadiationMassNumber = new DicomTagIS(0x300A, 0x0302);

        ///<summary>(300A,0304) VR=IS VM=1 Radiation Atomic Number</summary>
        public readonly static DicomTagIS RadiationAtomicNumber = new DicomTagIS(0x300A, 0x0304);

        ///<summary>(300A,0306) VR=SS VM=1 Radiation Charge State</summary>
        public readonly static DicomTagSS RadiationChargeState = new DicomTagSS(0x300A, 0x0306);

        ///<summary>(300A,0308) VR=CS VM=1 Scan Mode</summary>
        public readonly static DicomTagCS ScanMode = new DicomTagCS(0x300A, 0x0308);

        ///<summary>(300A,0309) VR=CS VM=1 Modulated Scan Mode Type</summary>
        public readonly static DicomTagCS ModulatedScanModeType = new DicomTagCS(0x300A, 0x0309);

        ///<summary>(300A,030A) VR=FL VM=2 Virtual Source-Axis Distances</summary>
        public readonly static DicomTagFLs VirtualSourceAxisDistances = new DicomTagFLs(0x300A, 0x030A);

        ///<summary>(300A,030C) VR=SQ VM=1 Snout Sequence</summary>
        public readonly static DicomTagSQ SnoutSequence = new DicomTagSQ(0x300A, 0x030C);

        ///<summary>(300A,030D) VR=FL VM=1 Snout Position</summary>
        public readonly static DicomTagFL SnoutPosition = new DicomTagFL(0x300A, 0x030D);

        ///<summary>(300A,030F) VR=SH VM=1 Snout ID</summary>
        public readonly static DicomTagSH SnoutID = new DicomTagSH(0x300A, 0x030F);

        ///<summary>(300A,0312) VR=IS VM=1 Number of Range Shifters</summary>
        public readonly static DicomTagIS NumberOfRangeShifters = new DicomTagIS(0x300A, 0x0312);

        ///<summary>(300A,0314) VR=SQ VM=1 Range Shifter Sequence</summary>
        public readonly static DicomTagSQ RangeShifterSequence = new DicomTagSQ(0x300A, 0x0314);

        ///<summary>(300A,0316) VR=IS VM=1 Range Shifter Number</summary>
        public readonly static DicomTagIS RangeShifterNumber = new DicomTagIS(0x300A, 0x0316);

        ///<summary>(300A,0318) VR=SH VM=1 Range Shifter ID</summary>
        public readonly static DicomTagSH RangeShifterID = new DicomTagSH(0x300A, 0x0318);

        ///<summary>(300A,0320) VR=CS VM=1 Range Shifter Type</summary>
        public readonly static DicomTagCS RangeShifterType = new DicomTagCS(0x300A, 0x0320);

        ///<summary>(300A,0322) VR=LO VM=1 Range Shifter Description</summary>
        public readonly static DicomTagLO RangeShifterDescription = new DicomTagLO(0x300A, 0x0322);

        ///<summary>(300A,0330) VR=IS VM=1 Number of Lateral Spreading Devices</summary>
        public readonly static DicomTagIS NumberOfLateralSpreadingDevices = new DicomTagIS(0x300A, 0x0330);

        ///<summary>(300A,0332) VR=SQ VM=1 Lateral Spreading Device Sequence</summary>
        public readonly static DicomTagSQ LateralSpreadingDeviceSequence = new DicomTagSQ(0x300A, 0x0332);

        ///<summary>(300A,0334) VR=IS VM=1 Lateral Spreading Device Number</summary>
        public readonly static DicomTagIS LateralSpreadingDeviceNumber = new DicomTagIS(0x300A, 0x0334);

        ///<summary>(300A,0336) VR=SH VM=1 Lateral Spreading Device ID</summary>
        public readonly static DicomTagSH LateralSpreadingDeviceID = new DicomTagSH(0x300A, 0x0336);

        ///<summary>(300A,0338) VR=CS VM=1 Lateral Spreading Device Type</summary>
        public readonly static DicomTagCS LateralSpreadingDeviceType = new DicomTagCS(0x300A, 0x0338);

        ///<summary>(300A,033A) VR=LO VM=1 Lateral Spreading Device Description</summary>
        public readonly static DicomTagLO LateralSpreadingDeviceDescription = new DicomTagLO(0x300A, 0x033A);

        ///<summary>(300A,033C) VR=FL VM=1 Lateral Spreading Device Water Equivalent Thickness</summary>
        public readonly static DicomTagFL LateralSpreadingDeviceWaterEquivalentThickness = new DicomTagFL(0x300A, 0x033C);

        ///<summary>(300A,0340) VR=IS VM=1 Number of Range Modulators</summary>
        public readonly static DicomTagIS NumberOfRangeModulators = new DicomTagIS(0x300A, 0x0340);

        ///<summary>(300A,0342) VR=SQ VM=1 Range Modulator Sequence</summary>
        public readonly static DicomTagSQ RangeModulatorSequence = new DicomTagSQ(0x300A, 0x0342);

        ///<summary>(300A,0344) VR=IS VM=1 Range Modulator Number</summary>
        public readonly static DicomTagIS RangeModulatorNumber = new DicomTagIS(0x300A, 0x0344);

        ///<summary>(300A,0346) VR=SH VM=1 Range Modulator ID</summary>
        public readonly static DicomTagSH RangeModulatorID = new DicomTagSH(0x300A, 0x0346);

        ///<summary>(300A,0348) VR=CS VM=1 Range Modulator Type</summary>
        public readonly static DicomTagCS RangeModulatorType = new DicomTagCS(0x300A, 0x0348);

        ///<summary>(300A,034A) VR=LO VM=1 Range Modulator Description</summary>
        public readonly static DicomTagLO RangeModulatorDescription = new DicomTagLO(0x300A, 0x034A);

        ///<summary>(300A,034C) VR=SH VM=1 Beam Current Modulation ID</summary>
        public readonly static DicomTagSH BeamCurrentModulationID = new DicomTagSH(0x300A, 0x034C);

        ///<summary>(300A,0350) VR=CS VM=1 Patient Support Type</summary>
        public readonly static DicomTagCS PatientSupportType = new DicomTagCS(0x300A, 0x0350);

        ///<summary>(300A,0352) VR=SH VM=1 Patient Support ID</summary>
        public readonly static DicomTagSH PatientSupportID = new DicomTagSH(0x300A, 0x0352);

        ///<summary>(300A,0354) VR=LO VM=1 Patient Support Accessory Code</summary>
        public readonly static DicomTagLO PatientSupportAccessoryCode = new DicomTagLO(0x300A, 0x0354);

        ///<summary>(300A,0355) VR=LO VM=1 Tray Accessory Code</summary>
        public readonly static DicomTagLO TrayAccessoryCode = new DicomTagLO(0x300A, 0x0355);

        ///<summary>(300A,0356) VR=FL VM=1 Fixation Light Azimuthal Angle</summary>
        public readonly static DicomTagFL FixationLightAzimuthalAngle = new DicomTagFL(0x300A, 0x0356);

        ///<summary>(300A,0358) VR=FL VM=1 Fixation Light Polar Angle</summary>
        public readonly static DicomTagFL FixationLightPolarAngle = new DicomTagFL(0x300A, 0x0358);

        ///<summary>(300A,035A) VR=FL VM=1 Meterset Rate</summary>
        public readonly static DicomTagFL MetersetRate = new DicomTagFL(0x300A, 0x035A);

        ///<summary>(300A,0360) VR=SQ VM=1 Range Shifter Settings Sequence</summary>
        public readonly static DicomTagSQ RangeShifterSettingsSequence = new DicomTagSQ(0x300A, 0x0360);

        ///<summary>(300A,0362) VR=LO VM=1 Range Shifter Setting</summary>
        public readonly static DicomTagLO RangeShifterSetting = new DicomTagLO(0x300A, 0x0362);

        ///<summary>(300A,0364) VR=FL VM=1 Isocenter to Range Shifter Distance</summary>
        public readonly static DicomTagFL IsocenterToRangeShifterDistance = new DicomTagFL(0x300A, 0x0364);

        ///<summary>(300A,0366) VR=FL VM=1 Range Shifter Water Equivalent Thickness</summary>
        public readonly static DicomTagFL RangeShifterWaterEquivalentThickness = new DicomTagFL(0x300A, 0x0366);

        ///<summary>(300A,0370) VR=SQ VM=1 Lateral Spreading Device Settings Sequence</summary>
        public readonly static DicomTagSQ LateralSpreadingDeviceSettingsSequence = new DicomTagSQ(0x300A, 0x0370);

        ///<summary>(300A,0372) VR=LO VM=1 Lateral Spreading Device Setting</summary>
        public readonly static DicomTagLO LateralSpreadingDeviceSetting = new DicomTagLO(0x300A, 0x0372);

        ///<summary>(300A,0374) VR=FL VM=1 Isocenter to Lateral Spreading Device Distance</summary>
        public readonly static DicomTagFL IsocenterToLateralSpreadingDeviceDistance = new DicomTagFL(0x300A, 0x0374);

        ///<summary>(300A,0380) VR=SQ VM=1 Range Modulator Settings Sequence</summary>
        public readonly static DicomTagSQ RangeModulatorSettingsSequence = new DicomTagSQ(0x300A, 0x0380);

        ///<summary>(300A,0382) VR=FL VM=1 Range Modulator Gating Start Value</summary>
        public readonly static DicomTagFL RangeModulatorGatingStartValue = new DicomTagFL(0x300A, 0x0382);

        ///<summary>(300A,0384) VR=FL VM=1 Range Modulator Gating Stop Value</summary>
        public readonly static DicomTagFL RangeModulatorGatingStopValue = new DicomTagFL(0x300A, 0x0384);

        ///<summary>(300A,0386) VR=FL VM=1 Range Modulator Gating Start Water Equivalent Thickness</summary>
        public readonly static DicomTagFL RangeModulatorGatingStartWaterEquivalentThickness = new DicomTagFL(0x300A, 0x0386);

        ///<summary>(300A,0388) VR=FL VM=1 Range Modulator Gating Stop Water Equivalent Thickness</summary>
        public readonly static DicomTagFL RangeModulatorGatingStopWaterEquivalentThickness = new DicomTagFL(0x300A, 0x0388);

        ///<summary>(300A,038A) VR=FL VM=1 Isocenter to Range Modulator Distance</summary>
        public readonly static DicomTagFL IsocenterToRangeModulatorDistance = new DicomTagFL(0x300A, 0x038A);

        ///<summary>(300A,038F) VR=FL VM=1-n Scan Spot Time Offset</summary>
        public readonly static DicomTagFLs ScanSpotTimeOffset = new DicomTagFLs(0x300A, 0x038F);

        ///<summary>(300A,0390) VR=SH VM=1 Scan Spot Tune ID</summary>
        public readonly static DicomTagSH ScanSpotTuneID = new DicomTagSH(0x300A, 0x0390);

        ///<summary>(300A,0391) VR=IS VM=1-n Scan Spot Prescribed Indices</summary>
        public readonly static DicomTagISs ScanSpotPrescribedIndices = new DicomTagISs(0x300A, 0x0391);

        ///<summary>(300A,0392) VR=IS VM=1 Number of Scan Spot Positions</summary>
        public readonly static DicomTagIS NumberOfScanSpotPositions = new DicomTagIS(0x300A, 0x0392);

        ///<summary>(300A,0393) VR=CS VM=1 Scan Spot Reordered</summary>
        public readonly static DicomTagCS ScanSpotReordered = new DicomTagCS(0x300A, 0x0393);

        ///<summary>(300A,0394) VR=FL VM=1-n Scan Spot Position Map</summary>
        public readonly static DicomTagFLs ScanSpotPositionMap = new DicomTagFLs(0x300A, 0x0394);

        ///<summary>(300A,0395) VR=CS VM=1 Scan Spot Reordering Allowed</summary>
        public readonly static DicomTagCS ScanSpotReorderingAllowed = new DicomTagCS(0x300A, 0x0395);

        ///<summary>(300A,0396) VR=FL VM=1-n Scan Spot Meterset Weights</summary>
        public readonly static DicomTagFLs ScanSpotMetersetWeights = new DicomTagFLs(0x300A, 0x0396);

        ///<summary>(300A,0398) VR=FL VM=2 Scanning Spot Size</summary>
        public readonly static DicomTagFLs ScanningSpotSize = new DicomTagFLs(0x300A, 0x0398);

        ///<summary>(300A,0399) VR=FL VM=2-2n Scan Spot Sizes Delivered</summary>
        public readonly static DicomTagFLs ScanSpotSizesDelivered = new DicomTagFLs(0x300A, 0x0399);

        ///<summary>(300A,039A) VR=IS VM=1 Number of Paintings</summary>
        public readonly static DicomTagIS NumberOfPaintings = new DicomTagIS(0x300A, 0x039A);

        ///<summary>(300A,039B) VR=FL VM=1-n Scan Spot Gantry Angles</summary>
        public readonly static DicomTagFLs ScanSpotGantryAngles = new DicomTagFLs(0x300A, 0x039B);

        ///<summary>(300A,039C) VR=FL VM=1-n Scan Spot Patient Support Angles</summary>
        public readonly static DicomTagFLs ScanSpotPatientSupportAngles = new DicomTagFLs(0x300A, 0x039C);

        ///<summary>(300A,03A0) VR=SQ VM=1 Ion Tolerance Table Sequence</summary>
        public readonly static DicomTagSQ IonToleranceTableSequence = new DicomTagSQ(0x300A, 0x03A0);

        ///<summary>(300A,03A2) VR=SQ VM=1 Ion Beam Sequence</summary>
        public readonly static DicomTagSQ IonBeamSequence = new DicomTagSQ(0x300A, 0x03A2);

        ///<summary>(300A,03A4) VR=SQ VM=1 Ion Beam Limiting Device Sequence</summary>
        public readonly static DicomTagSQ IonBeamLimitingDeviceSequence = new DicomTagSQ(0x300A, 0x03A4);

        ///<summary>(300A,03A6) VR=SQ VM=1 Ion Block Sequence</summary>
        public readonly static DicomTagSQ IonBlockSequence = new DicomTagSQ(0x300A, 0x03A6);

        ///<summary>(300A,03A8) VR=SQ VM=1 Ion Control Point Sequence</summary>
        public readonly static DicomTagSQ IonControlPointSequence = new DicomTagSQ(0x300A, 0x03A8);

        ///<summary>(300A,03AA) VR=SQ VM=1 Ion Wedge Sequence</summary>
        public readonly static DicomTagSQ IonWedgeSequence = new DicomTagSQ(0x300A, 0x03AA);

        ///<summary>(300A,03AC) VR=SQ VM=1 Ion Wedge Position Sequence</summary>
        public readonly static DicomTagSQ IonWedgePositionSequence = new DicomTagSQ(0x300A, 0x03AC);

        ///<summary>(300A,0401) VR=SQ VM=1 Referenced Setup Image Sequence</summary>
        public readonly static DicomTagSQ ReferencedSetupImageSequence = new DicomTagSQ(0x300A, 0x0401);

        ///<summary>(300A,0402) VR=ST VM=1 Setup Image Comment</summary>
        public readonly static DicomTagST SetupImageComment = new DicomTagST(0x300A, 0x0402);

        ///<summary>(300A,0410) VR=SQ VM=1 Motion Synchronization Sequence</summary>
        public readonly static DicomTagSQ MotionSynchronizationSequence = new DicomTagSQ(0x300A, 0x0410);

        ///<summary>(300A,0412) VR=FL VM=3 Control Point Orientation</summary>
        public readonly static DicomTagFLs ControlPointOrientation = new DicomTagFLs(0x300A, 0x0412);

        ///<summary>(300A,0420) VR=SQ VM=1 General Accessory Sequence</summary>
        public readonly static DicomTagSQ GeneralAccessorySequence = new DicomTagSQ(0x300A, 0x0420);

        ///<summary>(300A,0421) VR=SH VM=1 General Accessory ID</summary>
        public readonly static DicomTagSH GeneralAccessoryID = new DicomTagSH(0x300A, 0x0421);

        ///<summary>(300A,0422) VR=ST VM=1 General Accessory Description</summary>
        public readonly static DicomTagST GeneralAccessoryDescription = new DicomTagST(0x300A, 0x0422);

        ///<summary>(300A,0423) VR=CS VM=1 General Accessory Type</summary>
        public readonly static DicomTagCS GeneralAccessoryType = new DicomTagCS(0x300A, 0x0423);

        ///<summary>(300A,0424) VR=IS VM=1 General Accessory Number</summary>
        public readonly static DicomTagIS GeneralAccessoryNumber = new DicomTagIS(0x300A, 0x0424);

        ///<summary>(300A,0425) VR=FL VM=1 Source to General Accessory Distance</summary>
        public readonly static DicomTagFL SourceToGeneralAccessoryDistance = new DicomTagFL(0x300A, 0x0425);

        ///<summary>(300A,0426) VR=DS VM=1 Isocenter to General Accessory Distance</summary>
        public readonly static DicomTagDS IsocenterToGeneralAccessoryDistance = new DicomTagDS(0x300A, 0x0426);

        ///<summary>(300A,0431) VR=SQ VM=1 Applicator Geometry Sequence</summary>
        public readonly static DicomTagSQ ApplicatorGeometrySequence = new DicomTagSQ(0x300A, 0x0431);

        ///<summary>(300A,0432) VR=CS VM=1 Applicator Aperture Shape</summary>
        public readonly static DicomTagCS ApplicatorApertureShape = new DicomTagCS(0x300A, 0x0432);

        ///<summary>(300A,0433) VR=FL VM=1 Applicator Opening</summary>
        public readonly static DicomTagFL ApplicatorOpening = new DicomTagFL(0x300A, 0x0433);

        ///<summary>(300A,0434) VR=FL VM=1 Applicator Opening X</summary>
        public readonly static DicomTagFL ApplicatorOpeningX = new DicomTagFL(0x300A, 0x0434);

        ///<summary>(300A,0435) VR=FL VM=1 Applicator Opening Y</summary>
        public readonly static DicomTagFL ApplicatorOpeningY = new DicomTagFL(0x300A, 0x0435);

        ///<summary>(300A,0436) VR=FL VM=1 Source to Applicator Mounting Position Distance</summary>
        public readonly static DicomTagFL SourceToApplicatorMountingPositionDistance = new DicomTagFL(0x300A, 0x0436);

        ///<summary>(300A,0440) VR=IS VM=1 Number of Block Slab Items</summary>
        public readonly static DicomTagIS NumberOfBlockSlabItems = new DicomTagIS(0x300A, 0x0440);

        ///<summary>(300A,0441) VR=SQ VM=1 Block Slab Sequence</summary>
        public readonly static DicomTagSQ BlockSlabSequence = new DicomTagSQ(0x300A, 0x0441);

        ///<summary>(300A,0442) VR=DS VM=1 Block Slab Thickness</summary>
        public readonly static DicomTagDS BlockSlabThickness = new DicomTagDS(0x300A, 0x0442);

        ///<summary>(300A,0443) VR=US VM=1 Block Slab Number</summary>
        public readonly static DicomTagUS BlockSlabNumber = new DicomTagUS(0x300A, 0x0443);

        ///<summary>(300A,0450) VR=SQ VM=1 Device Motion Control Sequence</summary>
        public readonly static DicomTagSQ DeviceMotionControlSequence = new DicomTagSQ(0x300A, 0x0450);

        ///<summary>(300A,0451) VR=CS VM=1 Device Motion Execution Mode</summary>
        public readonly static DicomTagCS DeviceMotionExecutionMode = new DicomTagCS(0x300A, 0x0451);

        ///<summary>(300A,0452) VR=CS VM=1 Device Motion Observation Mode</summary>
        public readonly static DicomTagCS DeviceMotionObservationMode = new DicomTagCS(0x300A, 0x0452);

        ///<summary>(300A,0453) VR=SQ VM=1 Device Motion Parameter Code Sequence</summary>
        public readonly static DicomTagSQ DeviceMotionParameterCodeSequence = new DicomTagSQ(0x300A, 0x0453);

        ///<summary>(300A,0501) VR=FL VM=1 Distal Depth Fraction</summary>
        public readonly static DicomTagFL DistalDepthFraction = new DicomTagFL(0x300A, 0x0501);

        ///<summary>(300A,0502) VR=FL VM=1 Distal Depth</summary>
        public readonly static DicomTagFL DistalDepth = new DicomTagFL(0x300A, 0x0502);

        ///<summary>(300A,0503) VR=FL VM=2 Nominal Range Modulation Fractions</summary>
        public readonly static DicomTagFLs NominalRangeModulationFractions = new DicomTagFLs(0x300A, 0x0503);

        ///<summary>(300A,0504) VR=FL VM=2 Nominal Range Modulated Region Depths</summary>
        public readonly static DicomTagFLs NominalRangeModulatedRegionDepths = new DicomTagFLs(0x300A, 0x0504);

        ///<summary>(300A,0505) VR=SQ VM=1 Depth Dose Parameters Sequence</summary>
        public readonly static DicomTagSQ DepthDoseParametersSequence = new DicomTagSQ(0x300A, 0x0505);

        ///<summary>(300A,0506) VR=SQ VM=1 Delivered Depth Dose Parameters Sequence</summary>
        public readonly static DicomTagSQ DeliveredDepthDoseParametersSequence = new DicomTagSQ(0x300A, 0x0506);

        ///<summary>(300A,0507) VR=FL VM=1 Delivered Distal Depth Fraction</summary>
        public readonly static DicomTagFL DeliveredDistalDepthFraction = new DicomTagFL(0x300A, 0x0507);

        ///<summary>(300A,0508) VR=FL VM=1 Delivered Distal Depth</summary>
        public readonly static DicomTagFL DeliveredDistalDepth = new DicomTagFL(0x300A, 0x0508);

        ///<summary>(300A,0509) VR=FL VM=2 Delivered Nominal Range Modulation Fractions</summary>
        public readonly static DicomTagFLs DeliveredNominalRangeModulationFractions = new DicomTagFLs(0x300A, 0x0509);

        ///<summary>(300A,0510) VR=FL VM=2 Delivered Nominal Range Modulated Region Depths</summary>
        public readonly static DicomTagFLs DeliveredNominalRangeModulatedRegionDepths = new DicomTagFLs(0x300A, 0x0510);

        ///<summary>(300A,0511) VR=CS VM=1 Delivered Reference Dose Definition</summary>
        public readonly static DicomTagCS DeliveredReferenceDoseDefinition = new DicomTagCS(0x300A, 0x0511);

        ///<summary>(300A,0512) VR=CS VM=1 Reference Dose Definition</summary>
        public readonly static DicomTagCS ReferenceDoseDefinition = new DicomTagCS(0x300A, 0x0512);

        ///<summary>(300A,0600) VR=US VM=1 RT Control Point Index</summary>
        public readonly static DicomTagUS RTControlPointIndex = new DicomTagUS(0x300A, 0x0600);

        ///<summary>(300A,0601) VR=US VM=1 Radiation Generation Mode Index</summary>
        public readonly static DicomTagUS RadiationGenerationModeIndex = new DicomTagUS(0x300A, 0x0601);

        ///<summary>(300A,0602) VR=US VM=1 Referenced Defined Device Index</summary>
        public readonly static DicomTagUS ReferencedDefinedDeviceIndex = new DicomTagUS(0x300A, 0x0602);

        ///<summary>(300A,0603) VR=US VM=1 Radiation Dose Identification Index</summary>
        public readonly static DicomTagUS RadiationDoseIdentificationIndex = new DicomTagUS(0x300A, 0x0603);

        ///<summary>(300A,0604) VR=US VM=1 Number of RT Control Points</summary>
        public readonly static DicomTagUS NumberOfRTControlPoints = new DicomTagUS(0x300A, 0x0604);

        ///<summary>(300A,0605) VR=US VM=1 Referenced Radiation Generation Mode Index</summary>
        public readonly static DicomTagUS ReferencedRadiationGenerationModeIndex = new DicomTagUS(0x300A, 0x0605);

        ///<summary>(300A,0606) VR=US VM=1 Treatment Position Index</summary>
        public readonly static DicomTagUS TreatmentPositionIndex = new DicomTagUS(0x300A, 0x0606);

        ///<summary>(300A,0607) VR=US VM=1 Referenced Device Index</summary>
        public readonly static DicomTagUS ReferencedDeviceIndex = new DicomTagUS(0x300A, 0x0607);

        ///<summary>(300A,0608) VR=LO VM=1 Treatment Position Group Label</summary>
        public readonly static DicomTagLO TreatmentPositionGroupLabel = new DicomTagLO(0x300A, 0x0608);

        ///<summary>(300A,0609) VR=UI VM=1 Treatment Position Group UID</summary>
        public readonly static DicomTagUI TreatmentPositionGroupUID = new DicomTagUI(0x300A, 0x0609);

        ///<summary>(300A,060A) VR=SQ VM=1 Treatment Position Group Sequence</summary>
        public readonly static DicomTagSQ TreatmentPositionGroupSequence = new DicomTagSQ(0x300A, 0x060A);

        ///<summary>(300A,060B) VR=US VM=1 Referenced Treatment Position Index</summary>
        public readonly static DicomTagUS ReferencedTreatmentPositionIndex = new DicomTagUS(0x300A, 0x060B);

        ///<summary>(300A,060C) VR=US VM=1 Referenced Radiation Dose Identification Index</summary>
        public readonly static DicomTagUS ReferencedRadiationDoseIdentificationIndex = new DicomTagUS(0x300A, 0x060C);

        ///<summary>(300A,060D) VR=FD VM=1 RT Accessory Holder Water-Equivalent Thickness</summary>
        public readonly static DicomTagFD RTAccessoryHolderWaterEquivalentThickness = new DicomTagFD(0x300A, 0x060D);

        ///<summary>(300A,060E) VR=US VM=1 Referenced RT Accessory Holder Device Index</summary>
        public readonly static DicomTagUS ReferencedRTAccessoryHolderDeviceIndex = new DicomTagUS(0x300A, 0x060E);

        ///<summary>(300A,060F) VR=CS VM=1 RT Accessory Holder Slot Existence Flag</summary>
        public readonly static DicomTagCS RTAccessoryHolderSlotExistenceFlag = new DicomTagCS(0x300A, 0x060F);

        ///<summary>(300A,0610) VR=SQ VM=1 RT Accessory Holder Slot Sequence</summary>
        public readonly static DicomTagSQ RTAccessoryHolderSlotSequence = new DicomTagSQ(0x300A, 0x0610);

        ///<summary>(300A,0611) VR=LO VM=1 RT Accessory Holder Slot ID</summary>
        public readonly static DicomTagLO RTAccessoryHolderSlotID = new DicomTagLO(0x300A, 0x0611);

        ///<summary>(300A,0612) VR=FD VM=1 RT Accessory Holder Slot Distance</summary>
        public readonly static DicomTagFD RTAccessoryHolderSlotDistance = new DicomTagFD(0x300A, 0x0612);

        ///<summary>(300A,0613) VR=FD VM=1 RT Accessory Slot Distance</summary>
        public readonly static DicomTagFD RTAccessorySlotDistance = new DicomTagFD(0x300A, 0x0613);

        ///<summary>(300A,0614) VR=SQ VM=1 RT Accessory Holder Definition Sequence</summary>
        public readonly static DicomTagSQ RTAccessoryHolderDefinitionSequence = new DicomTagSQ(0x300A, 0x0614);

        ///<summary>(300A,0615) VR=LO VM=1 RT Accessory Device Slot ID</summary>
        public readonly static DicomTagLO RTAccessoryDeviceSlotID = new DicomTagLO(0x300A, 0x0615);

        ///<summary>(300A,0616) VR=SQ VM=1 RT Radiation Sequence</summary>
        public readonly static DicomTagSQ RTRadiationSequence = new DicomTagSQ(0x300A, 0x0616);

        ///<summary>(300A,0617) VR=SQ VM=1 Radiation Dose Sequence</summary>
        public readonly static DicomTagSQ RadiationDoseSequence = new DicomTagSQ(0x300A, 0x0617);

        ///<summary>(300A,0618) VR=SQ VM=1 Radiation Dose Identification Sequence</summary>
        public readonly static DicomTagSQ RadiationDoseIdentificationSequence = new DicomTagSQ(0x300A, 0x0618);

        ///<summary>(300A,0619) VR=LO VM=1 Radiation Dose Identification Label</summary>
        public readonly static DicomTagLO RadiationDoseIdentificationLabel = new DicomTagLO(0x300A, 0x0619);

        ///<summary>(300A,061A) VR=CS VM=1 Reference Dose Type</summary>
        public readonly static DicomTagCS ReferenceDoseType = new DicomTagCS(0x300A, 0x061A);

        ///<summary>(300A,061B) VR=CS VM=1 Primary Dose Value Indicator</summary>
        public readonly static DicomTagCS PrimaryDoseValueIndicator = new DicomTagCS(0x300A, 0x061B);

        ///<summary>(300A,061C) VR=SQ VM=1 Dose Values Sequence</summary>
        public readonly static DicomTagSQ DoseValuesSequence = new DicomTagSQ(0x300A, 0x061C);

        ///<summary>(300A,061D) VR=CS VM=1-n Dose Value Purpose</summary>
        public readonly static DicomTagCSs DoseValuePurpose = new DicomTagCSs(0x300A, 0x061D);

        ///<summary>(300A,061E) VR=FD VM=3 Reference Dose Point Coordinates</summary>
        public readonly static DicomTagFDs ReferenceDosePointCoordinates = new DicomTagFDs(0x300A, 0x061E);

        ///<summary>(300A,061F) VR=SQ VM=1 Radiation Dose Values Parameters Sequence</summary>
        public readonly static DicomTagSQ RadiationDoseValuesParametersSequence = new DicomTagSQ(0x300A, 0x061F);

        ///<summary>(300A,0620) VR=SQ VM=1 Meterset to Dose Mapping Sequence</summary>
        public readonly static DicomTagSQ MetersetToDoseMappingSequence = new DicomTagSQ(0x300A, 0x0620);

        ///<summary>(300A,0621) VR=SQ VM=1 Expected In-Vivo Measurement Values Sequence</summary>
        public readonly static DicomTagSQ ExpectedInVivoMeasurementValuesSequence = new DicomTagSQ(0x300A, 0x0621);

        ///<summary>(300A,0622) VR=US VM=1 Expected In-Vivo Measurement Value Index</summary>
        public readonly static DicomTagUS ExpectedInVivoMeasurementValueIndex = new DicomTagUS(0x300A, 0x0622);

        ///<summary>(300A,0623) VR=LO VM=1 Radiation Dose In-Vivo Measurement Label</summary>
        public readonly static DicomTagLO RadiationDoseInVivoMeasurementLabel = new DicomTagLO(0x300A, 0x0623);

        ///<summary>(300A,0624) VR=FD VM=2 Radiation Dose Central Axis Displacement</summary>
        public readonly static DicomTagFDs RadiationDoseCentralAxisDisplacement = new DicomTagFDs(0x300A, 0x0624);

        ///<summary>(300A,0625) VR=FD VM=1 Radiation Dose Value</summary>
        public readonly static DicomTagFD RadiationDoseValue = new DicomTagFD(0x300A, 0x0625);

        ///<summary>(300A,0626) VR=FD VM=1 Radiation Dose Source to Skin Distance</summary>
        public readonly static DicomTagFD RadiationDoseSourceToSkinDistance = new DicomTagFD(0x300A, 0x0626);

        ///<summary>(300A,0627) VR=FD VM=3 Radiation Dose Measurement Point Coordinates</summary>
        public readonly static DicomTagFDs RadiationDoseMeasurementPointCoordinates = new DicomTagFDs(0x300A, 0x0627);

        ///<summary>(300A,0628) VR=FD VM=1 Radiation Dose Source to External Contour Distance</summary>
        public readonly static DicomTagFD RadiationDoseSourceToExternalContourDistance = new DicomTagFD(0x300A, 0x0628);

        ///<summary>(300A,0629) VR=SQ VM=1 RT Tolerance Set Sequence</summary>
        public readonly static DicomTagSQ RTToleranceSetSequence = new DicomTagSQ(0x300A, 0x0629);

        ///<summary>(300A,062A) VR=LO VM=1 RT Tolerance Set Label</summary>
        public readonly static DicomTagLO RTToleranceSetLabel = new DicomTagLO(0x300A, 0x062A);

        ///<summary>(300A,062B) VR=SQ VM=1 Attribute Tolerance Values Sequence</summary>
        public readonly static DicomTagSQ AttributeToleranceValuesSequence = new DicomTagSQ(0x300A, 0x062B);

        ///<summary>(300A,062C) VR=FD VM=1 Tolerance Value</summary>
        public readonly static DicomTagFD ToleranceValue = new DicomTagFD(0x300A, 0x062C);

        ///<summary>(300A,062D) VR=SQ VM=1 Patient Support Position Tolerance Sequence</summary>
        public readonly static DicomTagSQ PatientSupportPositionToleranceSequence = new DicomTagSQ(0x300A, 0x062D);

        ///<summary>(300A,062E) VR=FD VM=1 Treatment Time Limit</summary>
        public readonly static DicomTagFD TreatmentTimeLimit = new DicomTagFD(0x300A, 0x062E);

        ///<summary>(300A,062F) VR=SQ VM=1 C-Arm Photon-Electron Control Point Sequence</summary>
        public readonly static DicomTagSQ CArmPhotonElectronControlPointSequence = new DicomTagSQ(0x300A, 0x062F);

        ///<summary>(300A,0630) VR=SQ VM=1 Referenced RT Radiation Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTRadiationSequence = new DicomTagSQ(0x300A, 0x0630);

        ///<summary>(300A,0631) VR=SQ VM=1 Referenced RT Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTInstanceSequence = new DicomTagSQ(0x300A, 0x0631);

        ///<summary>(300A,0632) VR=SQ VM=1 Referenced RT Patient Setup Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedRTPatientSetupSequenceRETIRED = new DicomTagSQ(0x300A, 0x0632);

        ///<summary>(300A,0634) VR=FD VM=1 Source to Patient Surface Distance</summary>
        public readonly static DicomTagFD SourceToPatientSurfaceDistance = new DicomTagFD(0x300A, 0x0634);

        ///<summary>(300A,0635) VR=SQ VM=1 Treatment Machine Special Mode Code Sequence</summary>
        public readonly static DicomTagSQ TreatmentMachineSpecialModeCodeSequence = new DicomTagSQ(0x300A, 0x0635);

        ///<summary>(300A,0636) VR=US VM=1 Intended Number of Fractions</summary>
        public readonly static DicomTagUS IntendedNumberOfFractions = new DicomTagUS(0x300A, 0x0636);

        ///<summary>(300A,0637) VR=CS VM=1 RT Radiation Set Intent</summary>
        public readonly static DicomTagCS RTRadiationSetIntent = new DicomTagCS(0x300A, 0x0637);

        ///<summary>(300A,0638) VR=CS VM=1 RT Radiation Physical and Geometric Content Detail Flag</summary>
        public readonly static DicomTagCS RTRadiationPhysicalAndGeometricContentDetailFlag = new DicomTagCS(0x300A, 0x0638);

        ///<summary>(300A,0639) VR=CS VM=1 RT Record Flag</summary>
        public readonly static DicomTagCS RTRecordFlag = new DicomTagCS(0x300A, 0x0639);

        ///<summary>(300A,063A) VR=SQ VM=1 Treatment Device Identification Sequence</summary>
        public readonly static DicomTagSQ TreatmentDeviceIdentificationSequence = new DicomTagSQ(0x300A, 0x063A);

        ///<summary>(300A,063B) VR=SQ VM=1 Referenced RT Physician Intent Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTPhysicianIntentSequence = new DicomTagSQ(0x300A, 0x063B);

        ///<summary>(300A,063C) VR=FD VM=1 Cumulative Meterset</summary>
        public readonly static DicomTagFD CumulativeMeterset = new DicomTagFD(0x300A, 0x063C);

        ///<summary>(300A,063D) VR=FD VM=1 Delivery Rate</summary>
        public readonly static DicomTagFD DeliveryRate = new DicomTagFD(0x300A, 0x063D);

        ///<summary>(300A,063E) VR=SQ VM=1 Delivery Rate Unit Sequence</summary>
        public readonly static DicomTagSQ DeliveryRateUnitSequence = new DicomTagSQ(0x300A, 0x063E);

        ///<summary>(300A,063F) VR=SQ VM=1 Treatment Position Sequence</summary>
        public readonly static DicomTagSQ TreatmentPositionSequence = new DicomTagSQ(0x300A, 0x063F);

        ///<summary>(300A,0640) VR=FD VM=1 Radiation Source-Axis Distance</summary>
        public readonly static DicomTagFD RadiationSourceAxisDistance = new DicomTagFD(0x300A, 0x0640);

        ///<summary>(300A,0641) VR=US VM=1 Number of RT Beam Limiting Devices</summary>
        public readonly static DicomTagUS NumberOfRTBeamLimitingDevices = new DicomTagUS(0x300A, 0x0641);

        ///<summary>(300A,0642) VR=FD VM=1 RT Beam Limiting Device Proximal Distance</summary>
        public readonly static DicomTagFD RTBeamLimitingDeviceProximalDistance = new DicomTagFD(0x300A, 0x0642);

        ///<summary>(300A,0643) VR=FD VM=1 RT Beam Limiting Device Distal Distance</summary>
        public readonly static DicomTagFD RTBeamLimitingDeviceDistalDistance = new DicomTagFD(0x300A, 0x0643);

        ///<summary>(300A,0644) VR=SQ VM=1 Parallel RT Beam Delimiter Device Orientation Label Code Sequence</summary>
        public readonly static DicomTagSQ ParallelRTBeamDelimiterDeviceOrientationLabelCodeSequence = new DicomTagSQ(0x300A, 0x0644);

        ///<summary>(300A,0645) VR=FD VM=1 Beam Modifier Orientation Angle</summary>
        public readonly static DicomTagFD BeamModifierOrientationAngle = new DicomTagFD(0x300A, 0x0645);

        ///<summary>(300A,0646) VR=SQ VM=1 Fixed RT Beam Delimiter Device Sequence</summary>
        public readonly static DicomTagSQ FixedRTBeamDelimiterDeviceSequence = new DicomTagSQ(0x300A, 0x0646);

        ///<summary>(300A,0647) VR=SQ VM=1 Parallel RT Beam Delimiter Device Sequence</summary>
        public readonly static DicomTagSQ ParallelRTBeamDelimiterDeviceSequence = new DicomTagSQ(0x300A, 0x0647);

        ///<summary>(300A,0648) VR=US VM=1 Number of Parallel RT Beam Delimiters</summary>
        public readonly static DicomTagUS NumberOfParallelRTBeamDelimiters = new DicomTagUS(0x300A, 0x0648);

        ///<summary>(300A,0649) VR=FD VM=2-n Parallel RT Beam Delimiter Boundaries</summary>
        public readonly static DicomTagFDs ParallelRTBeamDelimiterBoundaries = new DicomTagFDs(0x300A, 0x0649);

        ///<summary>(300A,064A) VR=FD VM=2-n Parallel RT Beam Delimiter Positions</summary>
        public readonly static DicomTagFDs ParallelRTBeamDelimiterPositions = new DicomTagFDs(0x300A, 0x064A);

        ///<summary>(300A,064B) VR=FD VM=2 RT Beam Limiting Device Offset</summary>
        public readonly static DicomTagFDs RTBeamLimitingDeviceOffset = new DicomTagFDs(0x300A, 0x064B);

        ///<summary>(300A,064C) VR=SQ VM=1 RT Beam Delimiter Geometry Sequence</summary>
        public readonly static DicomTagSQ RTBeamDelimiterGeometrySequence = new DicomTagSQ(0x300A, 0x064C);

        ///<summary>(300A,064D) VR=SQ VM=1 RT Beam Limiting Device Definition Sequence</summary>
        public readonly static DicomTagSQ RTBeamLimitingDeviceDefinitionSequence = new DicomTagSQ(0x300A, 0x064D);

        ///<summary>(300A,064E) VR=CS VM=1 Parallel RT Beam Delimiter Opening Mode</summary>
        public readonly static DicomTagCS ParallelRTBeamDelimiterOpeningMode = new DicomTagCS(0x300A, 0x064E);

        ///<summary>(300A,064F) VR=CS VM=1-n Parallel RT Beam Delimiter Leaf Mounting Side</summary>
        public readonly static DicomTagCSs ParallelRTBeamDelimiterLeafMountingSide = new DicomTagCSs(0x300A, 0x064F);

        ///<summary>(300A,0650) VR=UI VM=1 Patient Setup UID (RETIRED)</summary>
        public readonly static DicomTagUI PatientSetupUIDRETIRED = new DicomTagUI(0x300A, 0x0650);

        ///<summary>(300A,0651) VR=SQ VM=1 Wedge Definition Sequence</summary>
        public readonly static DicomTagSQ WedgeDefinitionSequence = new DicomTagSQ(0x300A, 0x0651);

        ///<summary>(300A,0652) VR=FD VM=1 Radiation Beam Wedge Angle</summary>
        public readonly static DicomTagFD RadiationBeamWedgeAngle = new DicomTagFD(0x300A, 0x0652);

        ///<summary>(300A,0653) VR=FD VM=1 Radiation Beam Wedge Thin Edge Distance</summary>
        public readonly static DicomTagFD RadiationBeamWedgeThinEdgeDistance = new DicomTagFD(0x300A, 0x0653);

        ///<summary>(300A,0654) VR=FD VM=1 Radiation Beam Effective Wedge Angle</summary>
        public readonly static DicomTagFD RadiationBeamEffectiveWedgeAngle = new DicomTagFD(0x300A, 0x0654);

        ///<summary>(300A,0655) VR=US VM=1 Number of Wedge Positions</summary>
        public readonly static DicomTagUS NumberOfWedgePositions = new DicomTagUS(0x300A, 0x0655);

        ///<summary>(300A,0656) VR=SQ VM=1 RT Beam Limiting Device Opening Sequence</summary>
        public readonly static DicomTagSQ RTBeamLimitingDeviceOpeningSequence = new DicomTagSQ(0x300A, 0x0656);

        ///<summary>(300A,0657) VR=US VM=1 Number of RT Beam Limiting Device Openings</summary>
        public readonly static DicomTagUS NumberOfRTBeamLimitingDeviceOpenings = new DicomTagUS(0x300A, 0x0657);

        ///<summary>(300A,0658) VR=SQ VM=1 Radiation Dosimeter Unit Sequence</summary>
        public readonly static DicomTagSQ RadiationDosimeterUnitSequence = new DicomTagSQ(0x300A, 0x0658);

        ///<summary>(300A,0659) VR=SQ VM=1 RT Device Distance Reference Location Code Sequence</summary>
        public readonly static DicomTagSQ RTDeviceDistanceReferenceLocationCodeSequence = new DicomTagSQ(0x300A, 0x0659);

        ///<summary>(300A,065A) VR=SQ VM=1 Radiation Device Configuration and Commissioning Key Sequence</summary>
        public readonly static DicomTagSQ RadiationDeviceConfigurationAndCommissioningKeySequence = new DicomTagSQ(0x300A, 0x065A);

        ///<summary>(300A,065B) VR=SQ VM=1 Patient Support Position Parameter Sequence</summary>
        public readonly static DicomTagSQ PatientSupportPositionParameterSequence = new DicomTagSQ(0x300A, 0x065B);

        ///<summary>(300A,065C) VR=CS VM=1 Patient Support Position Specification Method</summary>
        public readonly static DicomTagCS PatientSupportPositionSpecificationMethod = new DicomTagCS(0x300A, 0x065C);

        ///<summary>(300A,065D) VR=SQ VM=1 Patient Support Position Device Parameter Sequence</summary>
        public readonly static DicomTagSQ PatientSupportPositionDeviceParameterSequence = new DicomTagSQ(0x300A, 0x065D);

        ///<summary>(300A,065E) VR=US VM=1 Device Order Index</summary>
        public readonly static DicomTagUS DeviceOrderIndex = new DicomTagUS(0x300A, 0x065E);

        ///<summary>(300A,065F) VR=US VM=1 Patient Support Position Parameter Order Index</summary>
        public readonly static DicomTagUS PatientSupportPositionParameterOrderIndex = new DicomTagUS(0x300A, 0x065F);

        ///<summary>(300A,0660) VR=SQ VM=1 Patient Support Position Device Tolerance Sequence</summary>
        public readonly static DicomTagSQ PatientSupportPositionDeviceToleranceSequence = new DicomTagSQ(0x300A, 0x0660);

        ///<summary>(300A,0661) VR=US VM=1 Patient Support Position Tolerance Order Index</summary>
        public readonly static DicomTagUS PatientSupportPositionToleranceOrderIndex = new DicomTagUS(0x300A, 0x0661);

        ///<summary>(300A,0662) VR=SQ VM=1 Compensator Definition Sequence</summary>
        public readonly static DicomTagSQ CompensatorDefinitionSequence = new DicomTagSQ(0x300A, 0x0662);

        ///<summary>(300A,0663) VR=CS VM=1 Compensator Map Orientation</summary>
        public readonly static DicomTagCS CompensatorMapOrientation = new DicomTagCS(0x300A, 0x0663);

        ///<summary>(300A,0664) VR=OF VM=1 Compensator Proximal Thickness Map</summary>
        public readonly static DicomTagOF CompensatorProximalThicknessMap = new DicomTagOF(0x300A, 0x0664);

        ///<summary>(300A,0665) VR=OF VM=1 Compensator Distal Thickness Map</summary>
        public readonly static DicomTagOF CompensatorDistalThicknessMap = new DicomTagOF(0x300A, 0x0665);

        ///<summary>(300A,0666) VR=FD VM=1 Compensator Base Plane Offset</summary>
        public readonly static DicomTagFD CompensatorBasePlaneOffset = new DicomTagFD(0x300A, 0x0666);

        ///<summary>(300A,0667) VR=SQ VM=1 Compensator Shape Fabrication Code Sequence</summary>
        public readonly static DicomTagSQ CompensatorShapeFabricationCodeSequence = new DicomTagSQ(0x300A, 0x0667);

        ///<summary>(300A,0668) VR=SQ VM=1 Compensator Shape Sequence</summary>
        public readonly static DicomTagSQ CompensatorShapeSequence = new DicomTagSQ(0x300A, 0x0668);

        ///<summary>(300A,0669) VR=FD VM=1 Radiation Beam Compensator Milling Tool Diameter</summary>
        public readonly static DicomTagFD RadiationBeamCompensatorMillingToolDiameter = new DicomTagFD(0x300A, 0x0669);

        ///<summary>(300A,066A) VR=SQ VM=1 Block Definition Sequence</summary>
        public readonly static DicomTagSQ BlockDefinitionSequence = new DicomTagSQ(0x300A, 0x066A);

        ///<summary>(300A,066B) VR=OF VM=1 Block Edge Data</summary>
        public readonly static DicomTagOF BlockEdgeData = new DicomTagOF(0x300A, 0x066B);

        ///<summary>(300A,066C) VR=CS VM=1 Block Orientation</summary>
        public readonly static DicomTagCS BlockOrientation = new DicomTagCS(0x300A, 0x066C);

        ///<summary>(300A,066D) VR=FD VM=1 Radiation Beam Block Thickness</summary>
        public readonly static DicomTagFD RadiationBeamBlockThickness = new DicomTagFD(0x300A, 0x066D);

        ///<summary>(300A,066E) VR=FD VM=1 Radiation Beam Block Slab Thickness</summary>
        public readonly static DicomTagFD RadiationBeamBlockSlabThickness = new DicomTagFD(0x300A, 0x066E);

        ///<summary>(300A,066F) VR=SQ VM=1 Block Edge Data Sequence</summary>
        public readonly static DicomTagSQ BlockEdgeDataSequence = new DicomTagSQ(0x300A, 0x066F);

        ///<summary>(300A,0670) VR=US VM=1 Number of RT Accessory Holders</summary>
        public readonly static DicomTagUS NumberOfRTAccessoryHolders = new DicomTagUS(0x300A, 0x0670);

        ///<summary>(300A,0671) VR=SQ VM=1 General Accessory Definition Sequence</summary>
        public readonly static DicomTagSQ GeneralAccessoryDefinitionSequence = new DicomTagSQ(0x300A, 0x0671);

        ///<summary>(300A,0672) VR=US VM=1 Number of General Accessories</summary>
        public readonly static DicomTagUS NumberOfGeneralAccessories = new DicomTagUS(0x300A, 0x0672);

        ///<summary>(300A,0673) VR=SQ VM=1 Bolus Definition Sequence</summary>
        public readonly static DicomTagSQ BolusDefinitionSequence = new DicomTagSQ(0x300A, 0x0673);

        ///<summary>(300A,0674) VR=US VM=1 Number of Boluses</summary>
        public readonly static DicomTagUS NumberOfBoluses = new DicomTagUS(0x300A, 0x0674);

        ///<summary>(300A,0675) VR=UI VM=1 Equipment Frame of Reference UID</summary>
        public readonly static DicomTagUI EquipmentFrameOfReferenceUID = new DicomTagUI(0x300A, 0x0675);

        ///<summary>(300A,0676) VR=ST VM=1 Equipment Frame of Reference Description</summary>
        public readonly static DicomTagST EquipmentFrameOfReferenceDescription = new DicomTagST(0x300A, 0x0676);

        ///<summary>(300A,0677) VR=SQ VM=1 Equipment Reference Point Coordinates Sequence</summary>
        public readonly static DicomTagSQ EquipmentReferencePointCoordinatesSequence = new DicomTagSQ(0x300A, 0x0677);

        ///<summary>(300A,0678) VR=SQ VM=1 Equipment Reference Point Code Sequence</summary>
        public readonly static DicomTagSQ EquipmentReferencePointCodeSequence = new DicomTagSQ(0x300A, 0x0678);

        ///<summary>(300A,0679) VR=FD VM=1 RT Beam Limiting Device Angle</summary>
        public readonly static DicomTagFD RTBeamLimitingDeviceAngle = new DicomTagFD(0x300A, 0x0679);

        ///<summary>(300A,067A) VR=FD VM=1 Source Roll Angle</summary>
        public readonly static DicomTagFD SourceRollAngle = new DicomTagFD(0x300A, 0x067A);

        ///<summary>(300A,067B) VR=SQ VM=1 Radiation GenerationMode Sequence</summary>
        public readonly static DicomTagSQ RadiationGenerationModeSequence = new DicomTagSQ(0x300A, 0x067B);

        ///<summary>(300A,067C) VR=SH VM=1 Radiation GenerationMode Label</summary>
        public readonly static DicomTagSH RadiationGenerationModeLabel = new DicomTagSH(0x300A, 0x067C);

        ///<summary>(300A,067D) VR=ST VM=1 Radiation GenerationMode Description</summary>
        public readonly static DicomTagST RadiationGenerationModeDescription = new DicomTagST(0x300A, 0x067D);

        ///<summary>(300A,067E) VR=SQ VM=1 Radiation GenerationMode Machine Code Sequence</summary>
        public readonly static DicomTagSQ RadiationGenerationModeMachineCodeSequence = new DicomTagSQ(0x300A, 0x067E);

        ///<summary>(300A,067F) VR=SQ VM=1 Radiation Type Code Sequence</summary>
        public readonly static DicomTagSQ RadiationTypeCodeSequence = new DicomTagSQ(0x300A, 0x067F);

        ///<summary>(300A,0680) VR=DS VM=1 Nominal Energy</summary>
        public readonly static DicomTagDS NominalEnergy = new DicomTagDS(0x300A, 0x0680);

        ///<summary>(300A,0681) VR=DS VM=1 Minimum Nominal Energy</summary>
        public readonly static DicomTagDS MinimumNominalEnergy = new DicomTagDS(0x300A, 0x0681);

        ///<summary>(300A,0682) VR=DS VM=1 Maximum Nominal Energy</summary>
        public readonly static DicomTagDS MaximumNominalEnergy = new DicomTagDS(0x300A, 0x0682);

        ///<summary>(300A,0683) VR=SQ VM=1 Radiation Fluence Modifier Code Sequence</summary>
        public readonly static DicomTagSQ RadiationFluenceModifierCodeSequence = new DicomTagSQ(0x300A, 0x0683);

        ///<summary>(300A,0684) VR=SQ VM=1 Energy Unit Code Sequence</summary>
        public readonly static DicomTagSQ EnergyUnitCodeSequence = new DicomTagSQ(0x300A, 0x0684);

        ///<summary>(300A,0685) VR=US VM=1 Number of Radiation GenerationModes</summary>
        public readonly static DicomTagUS NumberOfRadiationGenerationModes = new DicomTagUS(0x300A, 0x0685);

        ///<summary>(300A,0686) VR=SQ VM=1 Patient Support Devices Sequence</summary>
        public readonly static DicomTagSQ PatientSupportDevicesSequence = new DicomTagSQ(0x300A, 0x0686);

        ///<summary>(300A,0687) VR=US VM=1 Number of Patient Support Devices</summary>
        public readonly static DicomTagUS NumberOfPatientSupportDevices = new DicomTagUS(0x300A, 0x0687);

        ///<summary>(300A,0688) VR=FD VM=1 RT Beam Modifier Definition Distance</summary>
        public readonly static DicomTagFD RTBeamModifierDefinitionDistance = new DicomTagFD(0x300A, 0x0688);

        ///<summary>(300A,0689) VR=SQ VM=1 Beam Area Limit Sequence</summary>
        public readonly static DicomTagSQ BeamAreaLimitSequence = new DicomTagSQ(0x300A, 0x0689);

        ///<summary>(300A,068A) VR=SQ VM=1 Referenced RT Prescription Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTPrescriptionSequence = new DicomTagSQ(0x300A, 0x068A);

        ///<summary>(300A,068B) VR=CS VM=1 Dose Value Interpretation</summary>
        public readonly static DicomTagCS DoseValueInterpretation = new DicomTagCS(0x300A, 0x068B);

        ///<summary>(300A,0700) VR=UI VM=1 Treatment Session UID</summary>
        public readonly static DicomTagUI TreatmentSessionUID = new DicomTagUI(0x300A, 0x0700);

        ///<summary>(300A,0701) VR=CS VM=1 RT Radiation Usage</summary>
        public readonly static DicomTagCS RTRadiationUsage = new DicomTagCS(0x300A, 0x0701);

        ///<summary>(300A,0702) VR=SQ VM=1 Referenced RT Radiation Set Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTRadiationSetSequence = new DicomTagSQ(0x300A, 0x0702);

        ///<summary>(300A,0703) VR=SQ VM=1 Referenced RT Radiation Record Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTRadiationRecordSequence = new DicomTagSQ(0x300A, 0x0703);

        ///<summary>(300A,0704) VR=US VM=1 RT Radiation Set Delivery Number</summary>
        public readonly static DicomTagUS RTRadiationSetDeliveryNumber = new DicomTagUS(0x300A, 0x0704);

        ///<summary>(300A,0705) VR=US VM=1 Clinical Fraction Number</summary>
        public readonly static DicomTagUS ClinicalFractionNumber = new DicomTagUS(0x300A, 0x0705);

        ///<summary>(300A,0706) VR=CS VM=1 RT Treatment Fraction Completion Status</summary>
        public readonly static DicomTagCS RTTreatmentFractionCompletionStatus = new DicomTagCS(0x300A, 0x0706);

        ///<summary>(300A,0707) VR=CS VM=1 RT Radiation Set Usage</summary>
        public readonly static DicomTagCS RTRadiationSetUsage = new DicomTagCS(0x300A, 0x0707);

        ///<summary>(300A,0708) VR=CS VM=1 Treatment Delivery Continuation Flag</summary>
        public readonly static DicomTagCS TreatmentDeliveryContinuationFlag = new DicomTagCS(0x300A, 0x0708);

        ///<summary>(300A,0709) VR=CS VM=1 Treatment Record Content Origin</summary>
        public readonly static DicomTagCS TreatmentRecordContentOrigin = new DicomTagCS(0x300A, 0x0709);

        ///<summary>(300A,0714) VR=CS VM=1 RT Treatment Termination Status</summary>
        public readonly static DicomTagCS RTTreatmentTerminationStatus = new DicomTagCS(0x300A, 0x0714);

        ///<summary>(300A,0715) VR=SQ VM=1 RT Treatment Termination Reason Code Sequence</summary>
        public readonly static DicomTagSQ RTTreatmentTerminationReasonCodeSequence = new DicomTagSQ(0x300A, 0x0715);

        ///<summary>(300A,0716) VR=SQ VM=1 Machine-Specific Treatment Termination Code Sequence</summary>
        public readonly static DicomTagSQ MachineSpecificTreatmentTerminationCodeSequence = new DicomTagSQ(0x300A, 0x0716);

        ///<summary>(300A,0722) VR=SQ VM=1 RT Radiation Salvage Record Control Point Sequence</summary>
        public readonly static DicomTagSQ RTRadiationSalvageRecordControlPointSequence = new DicomTagSQ(0x300A, 0x0722);

        ///<summary>(300A,0723) VR=CS VM=1 Starting Meterset Value Known Flag</summary>
        public readonly static DicomTagCS StartingMetersetValueKnownFlag = new DicomTagCS(0x300A, 0x0723);

        ///<summary>(300A,0730) VR=ST VM=1 Treatment Termination Description</summary>
        public readonly static DicomTagST TreatmentTerminationDescription = new DicomTagST(0x300A, 0x0730);

        ///<summary>(300A,0731) VR=SQ VM=1 Treatment Tolerance Violation Sequence</summary>
        public readonly static DicomTagSQ TreatmentToleranceViolationSequence = new DicomTagSQ(0x300A, 0x0731);

        ///<summary>(300A,0732) VR=CS VM=1 Treatment Tolerance Violation Category</summary>
        public readonly static DicomTagCS TreatmentToleranceViolationCategory = new DicomTagCS(0x300A, 0x0732);

        ///<summary>(300A,0733) VR=SQ VM=1 Treatment Tolerance Violation Attribute Sequence</summary>
        public readonly static DicomTagSQ TreatmentToleranceViolationAttributeSequence = new DicomTagSQ(0x300A, 0x0733);

        ///<summary>(300A,0734) VR=ST VM=1 Treatment Tolerance Violation Description</summary>
        public readonly static DicomTagST TreatmentToleranceViolationDescription = new DicomTagST(0x300A, 0x0734);

        ///<summary>(300A,0735) VR=ST VM=1 Treatment Tolerance Violation Identification</summary>
        public readonly static DicomTagST TreatmentToleranceViolationIdentification = new DicomTagST(0x300A, 0x0735);

        ///<summary>(300A,0736) VR=DT VM=1 Treatment Tolerance Violation DateTime</summary>
        public readonly static DicomTagDT TreatmentToleranceViolationDateTime = new DicomTagDT(0x300A, 0x0736);

        ///<summary>(300A,073A) VR=DT VM=1 Recorded RT Control Point DateTime</summary>
        public readonly static DicomTagDT RecordedRTControlPointDateTime = new DicomTagDT(0x300A, 0x073A);

        ///<summary>(300A,073B) VR=US VM=1 Referenced Radiation RT Control Point Index</summary>
        public readonly static DicomTagUS ReferencedRadiationRTControlPointIndex = new DicomTagUS(0x300A, 0x073B);

        ///<summary>(300A,073E) VR=SQ VM=1 Alternate Value Sequence</summary>
        public readonly static DicomTagSQ AlternateValueSequence = new DicomTagSQ(0x300A, 0x073E);

        ///<summary>(300A,073F) VR=SQ VM=1 Confirmation Sequence</summary>
        public readonly static DicomTagSQ ConfirmationSequence = new DicomTagSQ(0x300A, 0x073F);

        ///<summary>(300A,0740) VR=SQ VM=1 Interlock Sequence</summary>
        public readonly static DicomTagSQ InterlockSequence = new DicomTagSQ(0x300A, 0x0740);

        ///<summary>(300A,0741) VR=DT VM=1 Interlock DateTime</summary>
        public readonly static DicomTagDT InterlockDateTime = new DicomTagDT(0x300A, 0x0741);

        ///<summary>(300A,0742) VR=ST VM=1 Interlock Description</summary>
        public readonly static DicomTagST InterlockDescription = new DicomTagST(0x300A, 0x0742);

        ///<summary>(300A,0743) VR=SQ VM=1 Interlock Originating Device Sequence</summary>
        public readonly static DicomTagSQ InterlockOriginatingDeviceSequence = new DicomTagSQ(0x300A, 0x0743);

        ///<summary>(300A,0744) VR=SQ VM=1 Interlock Code Sequence</summary>
        public readonly static DicomTagSQ InterlockCodeSequence = new DicomTagSQ(0x300A, 0x0744);

        ///<summary>(300A,0745) VR=SQ VM=1 Interlock Resolution Code Sequence</summary>
        public readonly static DicomTagSQ InterlockResolutionCodeSequence = new DicomTagSQ(0x300A, 0x0745);

        ///<summary>(300A,0746) VR=SQ VM=1 Interlock Resolution User Sequence</summary>
        public readonly static DicomTagSQ InterlockResolutionUserSequence = new DicomTagSQ(0x300A, 0x0746);

        ///<summary>(300A,0760) VR=DT VM=1 Override DateTime</summary>
        public readonly static DicomTagDT OverrideDateTime = new DicomTagDT(0x300A, 0x0760);

        ///<summary>(300A,0761) VR=SQ VM=1 Treatment Tolerance Violation Type Code Sequence</summary>
        public readonly static DicomTagSQ TreatmentToleranceViolationTypeCodeSequence = new DicomTagSQ(0x300A, 0x0761);

        ///<summary>(300A,0762) VR=SQ VM=1 Treatment Tolerance Violation Cause Code Sequence</summary>
        public readonly static DicomTagSQ TreatmentToleranceViolationCauseCodeSequence = new DicomTagSQ(0x300A, 0x0762);

        ///<summary>(300A,0772) VR=SQ VM=1 Measured Meterset to Dose Mapping Sequence</summary>
        public readonly static DicomTagSQ MeasuredMetersetToDoseMappingSequence = new DicomTagSQ(0x300A, 0x0772);

        ///<summary>(300A,0773) VR=US VM=1 Referenced Expected In-Vivo Measurement Value Index</summary>
        public readonly static DicomTagUS ReferencedExpectedInVivoMeasurementValueIndex = new DicomTagUS(0x300A, 0x0773);

        ///<summary>(300A,0774) VR=SQ VM=1 Dose Measurement Device Code Sequence</summary>
        public readonly static DicomTagSQ DoseMeasurementDeviceCodeSequence = new DicomTagSQ(0x300A, 0x0774);

        ///<summary>(300A,0780) VR=SQ VM=1 Additional Parameter Recording Instance Sequence</summary>
        public readonly static DicomTagSQ AdditionalParameterRecordingInstanceSequence = new DicomTagSQ(0x300A, 0x0780);

        ///<summary>(300A,0783) VR=ST VM=1 Interlock Origin Description</summary>
        public readonly static DicomTagST InterlockOriginDescription = new DicomTagST(0x300A, 0x0783);

        ///<summary>(300A,0784) VR=SQ VM=1 RT Patient Position Scope Sequence</summary>
        public readonly static DicomTagSQ RTPatientPositionScopeSequence = new DicomTagSQ(0x300A, 0x0784);

        ///<summary>(300A,0785) VR=UI VM=1 Referenced Treatment Position Group UID</summary>
        public readonly static DicomTagUI ReferencedTreatmentPositionGroupUID = new DicomTagUI(0x300A, 0x0785);

        ///<summary>(300A,0786) VR=US VM=1 Radiation Order Index</summary>
        public readonly static DicomTagUS RadiationOrderIndex = new DicomTagUS(0x300A, 0x0786);

        ///<summary>(300A,0787) VR=SQ VM=1 Omitted Radiation Sequence</summary>
        public readonly static DicomTagSQ OmittedRadiationSequence = new DicomTagSQ(0x300A, 0x0787);

        ///<summary>(300A,0788) VR=SQ VM=1 Reason for Omission Code Sequence</summary>
        public readonly static DicomTagSQ ReasonForOmissionCodeSequence = new DicomTagSQ(0x300A, 0x0788);

        ///<summary>(300A,0789) VR=SQ VM=1 RT Delivery Start Patient Position Sequence</summary>
        public readonly static DicomTagSQ RTDeliveryStartPatientPositionSequence = new DicomTagSQ(0x300A, 0x0789);

        ///<summary>(300A,078A) VR=SQ VM=1 RT Treatment Preparation Patient Position Sequence</summary>
        public readonly static DicomTagSQ RTTreatmentPreparationPatientPositionSequence = new DicomTagSQ(0x300A, 0x078A);

        ///<summary>(300A,078B) VR=SQ VM=1 Referenced RT Treatment Preparation Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTTreatmentPreparationSequence = new DicomTagSQ(0x300A, 0x078B);

        ///<summary>(300A,078C) VR=SQ VM=1 Referenced Patient Setup Photo Sequence</summary>
        public readonly static DicomTagSQ ReferencedPatientSetupPhotoSequence = new DicomTagSQ(0x300A, 0x078C);

        ///<summary>(300A,078D) VR=SQ VM=1 Patient Treatment Preparation Method Code Sequence</summary>
        public readonly static DicomTagSQ PatientTreatmentPreparationMethodCodeSequence = new DicomTagSQ(0x300A, 0x078D);

        ///<summary>(300A,078E) VR=LT VM=1 Patient Treatment Preparation Procedure Parameter Description</summary>
        public readonly static DicomTagLT PatientTreatmentPreparationProcedureParameterDescription = new DicomTagLT(0x300A, 0x078E);

        ///<summary>(300A,078F) VR=SQ VM=1 Patient Treatment Preparation Device Sequence</summary>
        public readonly static DicomTagSQ PatientTreatmentPreparationDeviceSequence = new DicomTagSQ(0x300A, 0x078F);

        ///<summary>(300A,0790) VR=SQ VM=1 Patient Treatment Preparation Procedure Sequence </summary>
        public readonly static DicomTagSQ PatientTreatmentPreparationProcedureSequence = new DicomTagSQ(0x300A, 0x0790);

        ///<summary>(300A,0791) VR=SQ VM=1 Patient Treatment Preparation Procedure Code Sequence</summary>
        public readonly static DicomTagSQ PatientTreatmentPreparationProcedureCodeSequence = new DicomTagSQ(0x300A, 0x0791);

        ///<summary>(300A,0792) VR=LT VM=1 Patient Treatment Preparation Method Description</summary>
        public readonly static DicomTagLT PatientTreatmentPreparationMethodDescription = new DicomTagLT(0x300A, 0x0792);

        ///<summary>(300A,0793) VR=SQ VM=1 Patient Treatment Preparation Procedure Parameter Sequence</summary>
        public readonly static DicomTagSQ PatientTreatmentPreparationProcedureParameterSequence = new DicomTagSQ(0x300A, 0x0793);

        ///<summary>(300A,0794) VR=LT VM=1 Patient Setup Photo Description</summary>
        public readonly static DicomTagLT PatientSetupPhotoDescription = new DicomTagLT(0x300A, 0x0794);

        ///<summary>(300A,0795) VR=US VM=1 Patient Treatment Preparation Procedure Index</summary>
        public readonly static DicomTagUS PatientTreatmentPreparationProcedureIndex = new DicomTagUS(0x300A, 0x0795);

        ///<summary>(300A,0796) VR=US VM=1 Referenced Patient Setup Procedure Index</summary>
        public readonly static DicomTagUS ReferencedPatientSetupProcedureIndex = new DicomTagUS(0x300A, 0x0796);

        ///<summary>(300A,0797) VR=SQ VM=1 RT Radiation Task Sequence</summary>
        public readonly static DicomTagSQ RTRadiationTaskSequence = new DicomTagSQ(0x300A, 0x0797);

        ///<summary>(300A,0798) VR=SQ VM=1 RT Patient Position Displacement Sequence</summary>
        public readonly static DicomTagSQ RTPatientPositionDisplacementSequence = new DicomTagSQ(0x300A, 0x0798);

        ///<summary>(300A,0799) VR=SQ VM=1 RT Patient Position Sequence</summary>
        public readonly static DicomTagSQ RTPatientPositionSequence = new DicomTagSQ(0x300A, 0x0799);

        ///<summary>(300A,079A) VR=LO VM=1 Displacement Reference Label</summary>
        public readonly static DicomTagLO DisplacementReferenceLabel = new DicomTagLO(0x300A, 0x079A);

        ///<summary>(300A,079B) VR=FD VM=16 Displacement Matrix</summary>
        public readonly static DicomTagFDs DisplacementMatrix = new DicomTagFDs(0x300A, 0x079B);

        ///<summary>(300A,079C) VR=SQ VM=1 Patient Support Displacement Sequence</summary>
        public readonly static DicomTagSQ PatientSupportDisplacementSequence = new DicomTagSQ(0x300A, 0x079C);

        ///<summary>(300A,079D) VR=SQ VM=1 Displacement Reference Location Code Sequence</summary>
        public readonly static DicomTagSQ DisplacementReferenceLocationCodeSequence = new DicomTagSQ(0x300A, 0x079D);

        ///<summary>(300A,079E) VR=CS VM=1 RT Radiation Set Delivery Usage</summary>
        public readonly static DicomTagCS RTRadiationSetDeliveryUsage = new DicomTagCS(0x300A, 0x079E);

        ///<summary>(300A,079F) VR=SQ VM=1 Patient Treatment Preparation Sequence</summary>
        public readonly static DicomTagSQ PatientTreatmentPreparationSequence = new DicomTagSQ(0x300A, 0x079F);

        ///<summary>(300A,07A0) VR=SQ VM=1 Patient to Equipment Relationship Sequence</summary>
        public readonly static DicomTagSQ PatientToEquipmentRelationshipSequence = new DicomTagSQ(0x300A, 0x07A0);

        ///<summary>(300A,07A1) VR=SQ VM=1 Imaging Equipment to Treatment Delivery Device Relationship Sequence</summary>
        public readonly static DicomTagSQ ImagingEquipmentToTreatmentDeliveryDeviceRelationshipSequence = new DicomTagSQ(0x300A, 0x07A1);

        ///<summary>(300C,0002) VR=SQ VM=1 Referenced RT Plan Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTPlanSequence = new DicomTagSQ(0x300C, 0x0002);

        ///<summary>(300C,0004) VR=SQ VM=1 Referenced Beam Sequence</summary>
        public readonly static DicomTagSQ ReferencedBeamSequence = new DicomTagSQ(0x300C, 0x0004);

        ///<summary>(300C,0006) VR=IS VM=1 Referenced Beam Number</summary>
        public readonly static DicomTagIS ReferencedBeamNumber = new DicomTagIS(0x300C, 0x0006);

        ///<summary>(300C,0007) VR=IS VM=1 Referenced Reference Image Number</summary>
        public readonly static DicomTagIS ReferencedReferenceImageNumber = new DicomTagIS(0x300C, 0x0007);

        ///<summary>(300C,0008) VR=DS VM=1 Start Cumulative Meterset Weight</summary>
        public readonly static DicomTagDS StartCumulativeMetersetWeight = new DicomTagDS(0x300C, 0x0008);

        ///<summary>(300C,0009) VR=DS VM=1 End Cumulative Meterset Weight</summary>
        public readonly static DicomTagDS EndCumulativeMetersetWeight = new DicomTagDS(0x300C, 0x0009);

        ///<summary>(300C,000A) VR=SQ VM=1 Referenced Brachy Application Setup Sequence</summary>
        public readonly static DicomTagSQ ReferencedBrachyApplicationSetupSequence = new DicomTagSQ(0x300C, 0x000A);

        ///<summary>(300C,000C) VR=IS VM=1 Referenced Brachy Application Setup Number</summary>
        public readonly static DicomTagIS ReferencedBrachyApplicationSetupNumber = new DicomTagIS(0x300C, 0x000C);

        ///<summary>(300C,000E) VR=IS VM=1 Referenced Source Number</summary>
        public readonly static DicomTagIS ReferencedSourceNumber = new DicomTagIS(0x300C, 0x000E);

        ///<summary>(300C,0020) VR=SQ VM=1 Referenced Fraction Group Sequence</summary>
        public readonly static DicomTagSQ ReferencedFractionGroupSequence = new DicomTagSQ(0x300C, 0x0020);

        ///<summary>(300C,0022) VR=IS VM=1 Referenced Fraction Group Number</summary>
        public readonly static DicomTagIS ReferencedFractionGroupNumber = new DicomTagIS(0x300C, 0x0022);

        ///<summary>(300C,0040) VR=SQ VM=1 Referenced Verification Image Sequence</summary>
        public readonly static DicomTagSQ ReferencedVerificationImageSequence = new DicomTagSQ(0x300C, 0x0040);

        ///<summary>(300C,0042) VR=SQ VM=1 Referenced Reference Image Sequence</summary>
        public readonly static DicomTagSQ ReferencedReferenceImageSequence = new DicomTagSQ(0x300C, 0x0042);

        ///<summary>(300C,0050) VR=SQ VM=1 Referenced Dose Reference Sequence</summary>
        public readonly static DicomTagSQ ReferencedDoseReferenceSequence = new DicomTagSQ(0x300C, 0x0050);

        ///<summary>(300C,0051) VR=IS VM=1 Referenced Dose Reference Number</summary>
        public readonly static DicomTagIS ReferencedDoseReferenceNumber = new DicomTagIS(0x300C, 0x0051);

        ///<summary>(300C,0055) VR=SQ VM=1 Brachy Referenced Dose Reference Sequence</summary>
        public readonly static DicomTagSQ BrachyReferencedDoseReferenceSequence = new DicomTagSQ(0x300C, 0x0055);

        ///<summary>(300C,0060) VR=SQ VM=1 Referenced Structure Set Sequence</summary>
        public readonly static DicomTagSQ ReferencedStructureSetSequence = new DicomTagSQ(0x300C, 0x0060);

        ///<summary>(300C,006A) VR=IS VM=1 Referenced Patient Setup Number</summary>
        public readonly static DicomTagIS ReferencedPatientSetupNumber = new DicomTagIS(0x300C, 0x006A);

        ///<summary>(300C,0080) VR=SQ VM=1 Referenced Dose Sequence</summary>
        public readonly static DicomTagSQ ReferencedDoseSequence = new DicomTagSQ(0x300C, 0x0080);

        ///<summary>(300C,00A0) VR=IS VM=1 Referenced Tolerance Table Number</summary>
        public readonly static DicomTagIS ReferencedToleranceTableNumber = new DicomTagIS(0x300C, 0x00A0);

        ///<summary>(300C,00B0) VR=SQ VM=1 Referenced Bolus Sequence</summary>
        public readonly static DicomTagSQ ReferencedBolusSequence = new DicomTagSQ(0x300C, 0x00B0);

        ///<summary>(300C,00C0) VR=IS VM=1 Referenced Wedge Number</summary>
        public readonly static DicomTagIS ReferencedWedgeNumber = new DicomTagIS(0x300C, 0x00C0);

        ///<summary>(300C,00D0) VR=IS VM=1 Referenced Compensator Number</summary>
        public readonly static DicomTagIS ReferencedCompensatorNumber = new DicomTagIS(0x300C, 0x00D0);

        ///<summary>(300C,00E0) VR=IS VM=1 Referenced Block Number</summary>
        public readonly static DicomTagIS ReferencedBlockNumber = new DicomTagIS(0x300C, 0x00E0);

        ///<summary>(300C,00F0) VR=IS VM=1 Referenced Control Point Index</summary>
        public readonly static DicomTagIS ReferencedControlPointIndex = new DicomTagIS(0x300C, 0x00F0);

        ///<summary>(300C,00F2) VR=SQ VM=1 Referenced Control Point Sequence</summary>
        public readonly static DicomTagSQ ReferencedControlPointSequence = new DicomTagSQ(0x300C, 0x00F2);

        ///<summary>(300C,00F4) VR=IS VM=1 Referenced Start Control Point Index</summary>
        public readonly static DicomTagIS ReferencedStartControlPointIndex = new DicomTagIS(0x300C, 0x00F4);

        ///<summary>(300C,00F6) VR=IS VM=1 Referenced Stop Control Point Index</summary>
        public readonly static DicomTagIS ReferencedStopControlPointIndex = new DicomTagIS(0x300C, 0x00F6);

        ///<summary>(300C,0100) VR=IS VM=1 Referenced Range Shifter Number</summary>
        public readonly static DicomTagIS ReferencedRangeShifterNumber = new DicomTagIS(0x300C, 0x0100);

        ///<summary>(300C,0102) VR=IS VM=1 Referenced Lateral Spreading Device Number</summary>
        public readonly static DicomTagIS ReferencedLateralSpreadingDeviceNumber = new DicomTagIS(0x300C, 0x0102);

        ///<summary>(300C,0104) VR=IS VM=1 Referenced Range Modulator Number</summary>
        public readonly static DicomTagIS ReferencedRangeModulatorNumber = new DicomTagIS(0x300C, 0x0104);

        ///<summary>(300C,0111) VR=SQ VM=1 Omitted Beam Task Sequence</summary>
        public readonly static DicomTagSQ OmittedBeamTaskSequence = new DicomTagSQ(0x300C, 0x0111);

        ///<summary>(300C,0112) VR=CS VM=1 Reason for Omission</summary>
        public readonly static DicomTagCS ReasonForOmission = new DicomTagCS(0x300C, 0x0112);

        ///<summary>(300C,0113) VR=LO VM=1 Reason for Omission Description</summary>
        public readonly static DicomTagLO ReasonForOmissionDescription = new DicomTagLO(0x300C, 0x0113);

        ///<summary>(300C,0114) VR=SQ VM=1 Prescription Overview Sequence</summary>
        public readonly static DicomTagSQ PrescriptionOverviewSequence = new DicomTagSQ(0x300C, 0x0114);

        ///<summary>(300C,0115) VR=FL VM=1 Total Prescription Dose</summary>
        public readonly static DicomTagFL TotalPrescriptionDose = new DicomTagFL(0x300C, 0x0115);

        ///<summary>(300C,0116) VR=SQ VM=1 Plan Overview Sequence</summary>
        public readonly static DicomTagSQ PlanOverviewSequence = new DicomTagSQ(0x300C, 0x0116);

        ///<summary>(300C,0117) VR=US VM=1 Plan Overview Index</summary>
        public readonly static DicomTagUS PlanOverviewIndex = new DicomTagUS(0x300C, 0x0117);

        ///<summary>(300C,0118) VR=US VM=1 Referenced Plan Overview Index</summary>
        public readonly static DicomTagUS ReferencedPlanOverviewIndex = new DicomTagUS(0x300C, 0x0118);

        ///<summary>(300C,0119) VR=US VM=1 Number of Fractions Included</summary>
        public readonly static DicomTagUS NumberOfFractionsIncluded = new DicomTagUS(0x300C, 0x0119);

        ///<summary>(300C,0120) VR=SQ VM=1 Dose Calibration Conditions Sequence</summary>
        public readonly static DicomTagSQ DoseCalibrationConditionsSequence = new DicomTagSQ(0x300C, 0x0120);

        ///<summary>(300C,0121) VR=FD VM=1 Absorbed Dose to Meterset Ratio</summary>
        public readonly static DicomTagFD AbsorbedDoseToMetersetRatio = new DicomTagFD(0x300C, 0x0121);

        ///<summary>(300C,0122) VR=FD VM=2 Delineated Radiation Field Size</summary>
        public readonly static DicomTagFDs DelineatedRadiationFieldSize = new DicomTagFDs(0x300C, 0x0122);

        ///<summary>(300C,0123) VR=CS VM=1 Dose Calibration Conditions Verified Flag</summary>
        public readonly static DicomTagCS DoseCalibrationConditionsVerifiedFlag = new DicomTagCS(0x300C, 0x0123);

        ///<summary>(300C,0124) VR=FD VM=1 Calibration Reference Point Depth</summary>
        public readonly static DicomTagFD CalibrationReferencePointDepth = new DicomTagFD(0x300C, 0x0124);

        ///<summary>(300C,0125) VR=SQ VM=1 Gating Beam Hold Transition Sequence</summary>
        public readonly static DicomTagSQ GatingBeamHoldTransitionSequence = new DicomTagSQ(0x300C, 0x0125);

        ///<summary>(300C,0126) VR=CS VM=1 Beam Hold Transition</summary>
        public readonly static DicomTagCS BeamHoldTransition = new DicomTagCS(0x300C, 0x0126);

        ///<summary>(300C,0127) VR=DT VM=1 Beam Hold Transition DateTime</summary>
        public readonly static DicomTagDT BeamHoldTransitionDateTime = new DicomTagDT(0x300C, 0x0127);

        ///<summary>(300C,0128) VR=SQ VM=1 Beam Hold Originating Device Sequence</summary>
        public readonly static DicomTagSQ BeamHoldOriginatingDeviceSequence = new DicomTagSQ(0x300C, 0x0128);

        ///<summary>(300C,0129) VR=CS VM=1 Beam Hold Transition Trigger Source</summary>
        public readonly static DicomTagCS BeamHoldTransitionTriggerSource = new DicomTagCS(0x300C, 0x0129);

        ///<summary>(300E,0002) VR=CS VM=1 Approval Status</summary>
        public readonly static DicomTagCS ApprovalStatus = new DicomTagCS(0x300E, 0x0002);

        ///<summary>(300E,0004) VR=DA VM=1 Review Date</summary>
        public readonly static DicomTagDA ReviewDate = new DicomTagDA(0x300E, 0x0004);

        ///<summary>(300E,0005) VR=TM VM=1 Review Time</summary>
        public readonly static DicomTagTM ReviewTime = new DicomTagTM(0x300E, 0x0005);

        ///<summary>(300E,0008) VR=PN VM=1 Reviewer Name</summary>
        public readonly static DicomTagPN ReviewerName = new DicomTagPN(0x300E, 0x0008);

        ///<summary>(3010,0001) VR=SQ VM=1 Radiobiological Dose Effect Sequence</summary>
        public readonly static DicomTagSQ RadiobiologicalDoseEffectSequence = new DicomTagSQ(0x3010, 0x0001);

        ///<summary>(3010,0002) VR=CS VM=1 Radiobiological Dose Effect Flag</summary>
        public readonly static DicomTagCS RadiobiologicalDoseEffectFlag = new DicomTagCS(0x3010, 0x0002);

        ///<summary>(3010,0003) VR=SQ VM=1 Effective Dose Calculation Method Category Code Sequence</summary>
        public readonly static DicomTagSQ EffectiveDoseCalculationMethodCategoryCodeSequence = new DicomTagSQ(0x3010, 0x0003);

        ///<summary>(3010,0004) VR=SQ VM=1 Effective Dose Calculation Method Code Sequence</summary>
        public readonly static DicomTagSQ EffectiveDoseCalculationMethodCodeSequence = new DicomTagSQ(0x3010, 0x0004);

        ///<summary>(3010,0005) VR=LO VM=1 Effective Dose Calculation Method Description</summary>
        public readonly static DicomTagLO EffectiveDoseCalculationMethodDescription = new DicomTagLO(0x3010, 0x0005);

        ///<summary>(3010,0006) VR=UI VM=1 Conceptual Volume UID</summary>
        public readonly static DicomTagUI ConceptualVolumeUID = new DicomTagUI(0x3010, 0x0006);

        ///<summary>(3010,0007) VR=SQ VM=1 Originating SOP Instance Reference Sequence</summary>
        public readonly static DicomTagSQ OriginatingSOPInstanceReferenceSequence = new DicomTagSQ(0x3010, 0x0007);

        ///<summary>(3010,0008) VR=SQ VM=1 Conceptual Volume Constituent Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeConstituentSequence = new DicomTagSQ(0x3010, 0x0008);

        ///<summary>(3010,0009) VR=SQ VM=1 Equivalent Conceptual Volume Instance Reference Sequence</summary>
        public readonly static DicomTagSQ EquivalentConceptualVolumeInstanceReferenceSequence = new DicomTagSQ(0x3010, 0x0009);

        ///<summary>(3010,000A) VR=SQ VM=1 Equivalent Conceptual Volumes Sequence</summary>
        public readonly static DicomTagSQ EquivalentConceptualVolumesSequence = new DicomTagSQ(0x3010, 0x000A);

        ///<summary>(3010,000B) VR=UI VM=1 Referenced Conceptual Volume UID</summary>
        public readonly static DicomTagUI ReferencedConceptualVolumeUID = new DicomTagUI(0x3010, 0x000B);

        ///<summary>(3010,000C) VR=UT VM=1 Conceptual Volume Combination Expression</summary>
        public readonly static DicomTagUT ConceptualVolumeCombinationExpression = new DicomTagUT(0x3010, 0x000C);

        ///<summary>(3010,000D) VR=US VM=1 Conceptual Volume Constituent Index</summary>
        public readonly static DicomTagUS ConceptualVolumeConstituentIndex = new DicomTagUS(0x3010, 0x000D);

        ///<summary>(3010,000E) VR=CS VM=1 Conceptual Volume Combination Flag</summary>
        public readonly static DicomTagCS ConceptualVolumeCombinationFlag = new DicomTagCS(0x3010, 0x000E);

        ///<summary>(3010,000F) VR=ST VM=1 Conceptual Volume Combination Description</summary>
        public readonly static DicomTagST ConceptualVolumeCombinationDescription = new DicomTagST(0x3010, 0x000F);

        ///<summary>(3010,0010) VR=CS VM=1 Conceptual Volume Segmentation Defined Flag</summary>
        public readonly static DicomTagCS ConceptualVolumeSegmentationDefinedFlag = new DicomTagCS(0x3010, 0x0010);

        ///<summary>(3010,0011) VR=SQ VM=1 Conceptual Volume Segmentation Reference Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeSegmentationReferenceSequence = new DicomTagSQ(0x3010, 0x0011);

        ///<summary>(3010,0012) VR=SQ VM=1 Conceptual Volume Constituent Segmentation Reference Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeConstituentSegmentationReferenceSequence = new DicomTagSQ(0x3010, 0x0012);

        ///<summary>(3010,0013) VR=UI VM=1 Constituent Conceptual Volume UID</summary>
        public readonly static DicomTagUI ConstituentConceptualVolumeUID = new DicomTagUI(0x3010, 0x0013);

        ///<summary>(3010,0014) VR=SQ VM=1 Derivation Conceptual Volume Sequence</summary>
        public readonly static DicomTagSQ DerivationConceptualVolumeSequence = new DicomTagSQ(0x3010, 0x0014);

        ///<summary>(3010,0015) VR=UI VM=1 Source Conceptual Volume UID</summary>
        public readonly static DicomTagUI SourceConceptualVolumeUID = new DicomTagUI(0x3010, 0x0015);

        ///<summary>(3010,0016) VR=SQ VM=1 Conceptual Volume Derivation Algorithm Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeDerivationAlgorithmSequence = new DicomTagSQ(0x3010, 0x0016);

        ///<summary>(3010,0017) VR=ST VM=1 Conceptual Volume Description</summary>
        public readonly static DicomTagST ConceptualVolumeDescription = new DicomTagST(0x3010, 0x0017);

        ///<summary>(3010,0018) VR=SQ VM=1 Source Conceptual Volume Sequence</summary>
        public readonly static DicomTagSQ SourceConceptualVolumeSequence = new DicomTagSQ(0x3010, 0x0018);

        ///<summary>(3010,0019) VR=SQ VM=1 Author Identification Sequence</summary>
        public readonly static DicomTagSQ AuthorIdentificationSequence = new DicomTagSQ(0x3010, 0x0019);

        ///<summary>(3010,001A) VR=LO VM=1 Manufacturer's Model Version</summary>
        public readonly static DicomTagLO ManufacturerModelVersion = new DicomTagLO(0x3010, 0x001A);

        ///<summary>(3010,001B) VR=UC VM=1 Device Alternate Identifier</summary>
        public readonly static DicomTagUC DeviceAlternateIdentifier = new DicomTagUC(0x3010, 0x001B);

        ///<summary>(3010,001C) VR=CS VM=1 Device Alternate Identifier Type</summary>
        public readonly static DicomTagCS DeviceAlternateIdentifierType = new DicomTagCS(0x3010, 0x001C);

        ///<summary>(3010,001D) VR=LT VM=1 Device Alternate Identifier Format</summary>
        public readonly static DicomTagLT DeviceAlternateIdentifierFormat = new DicomTagLT(0x3010, 0x001D);

        ///<summary>(3010,001E) VR=LO VM=1 Segmentation Creation Template Label</summary>
        public readonly static DicomTagLO SegmentationCreationTemplateLabel = new DicomTagLO(0x3010, 0x001E);

        ///<summary>(3010,001F) VR=UI VM=1 Segmentation Template UID</summary>
        public readonly static DicomTagUI SegmentationTemplateUID = new DicomTagUI(0x3010, 0x001F);

        ///<summary>(3010,0020) VR=US VM=1 Referenced Segment Reference Index</summary>
        public readonly static DicomTagUS ReferencedSegmentReferenceIndex = new DicomTagUS(0x3010, 0x0020);

        ///<summary>(3010,0021) VR=SQ VM=1 Segment Reference Sequence</summary>
        public readonly static DicomTagSQ SegmentReferenceSequence = new DicomTagSQ(0x3010, 0x0021);

        ///<summary>(3010,0022) VR=US VM=1 Segment Reference Index</summary>
        public readonly static DicomTagUS SegmentReferenceIndex = new DicomTagUS(0x3010, 0x0022);

        ///<summary>(3010,0023) VR=SQ VM=1 Direct Segment Reference Sequence</summary>
        public readonly static DicomTagSQ DirectSegmentReferenceSequence = new DicomTagSQ(0x3010, 0x0023);

        ///<summary>(3010,0024) VR=SQ VM=1 Combination Segment Reference Sequence</summary>
        public readonly static DicomTagSQ CombinationSegmentReferenceSequence = new DicomTagSQ(0x3010, 0x0024);

        ///<summary>(3010,0025) VR=SQ VM=1 Conceptual Volume Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeSequence = new DicomTagSQ(0x3010, 0x0025);

        ///<summary>(3010,0026) VR=SQ VM=1 Segmented RT Accessory Device Sequence</summary>
        public readonly static DicomTagSQ SegmentedRTAccessoryDeviceSequence = new DicomTagSQ(0x3010, 0x0026);

        ///<summary>(3010,0027) VR=SQ VM=1 Segment Characteristics Sequence</summary>
        public readonly static DicomTagSQ SegmentCharacteristicsSequence = new DicomTagSQ(0x3010, 0x0027);

        ///<summary>(3010,0028) VR=SQ VM=1 Related Segment Characteristics Sequence</summary>
        public readonly static DicomTagSQ RelatedSegmentCharacteristicsSequence = new DicomTagSQ(0x3010, 0x0028);

        ///<summary>(3010,0029) VR=US VM=1 Segment Characteristics Precedence</summary>
        public readonly static DicomTagUS SegmentCharacteristicsPrecedence = new DicomTagUS(0x3010, 0x0029);

        ///<summary>(3010,002A) VR=SQ VM=1 RT Segment Annotation Sequence</summary>
        public readonly static DicomTagSQ RTSegmentAnnotationSequence = new DicomTagSQ(0x3010, 0x002A);

        ///<summary>(3010,002B) VR=SQ VM=1 Segment Annotation Category Code Sequence</summary>
        public readonly static DicomTagSQ SegmentAnnotationCategoryCodeSequence = new DicomTagSQ(0x3010, 0x002B);

        ///<summary>(3010,002C) VR=SQ VM=1 Segment Annotation Type Code Sequence</summary>
        public readonly static DicomTagSQ SegmentAnnotationTypeCodeSequence = new DicomTagSQ(0x3010, 0x002C);

        ///<summary>(3010,002D) VR=LO VM=1 Device Label</summary>
        public readonly static DicomTagLO DeviceLabel = new DicomTagLO(0x3010, 0x002D);

        ///<summary>(3010,002E) VR=SQ VM=1 Device Type Code Sequence</summary>
        public readonly static DicomTagSQ DeviceTypeCodeSequence = new DicomTagSQ(0x3010, 0x002E);

        ///<summary>(3010,002F) VR=SQ VM=1 Segment Annotation Type Modifier Code Sequence</summary>
        public readonly static DicomTagSQ SegmentAnnotationTypeModifierCodeSequence = new DicomTagSQ(0x3010, 0x002F);

        ///<summary>(3010,0030) VR=SQ VM=1 Patient Equipment Relationship Code Sequence</summary>
        public readonly static DicomTagSQ PatientEquipmentRelationshipCodeSequence = new DicomTagSQ(0x3010, 0x0030);

        ///<summary>(3010,0031) VR=UI VM=1 Referenced Fiducials UID</summary>
        public readonly static DicomTagUI ReferencedFiducialsUID = new DicomTagUI(0x3010, 0x0031);

        ///<summary>(3010,0032) VR=SQ VM=1 Patient Treatment Orientation Sequence</summary>
        public readonly static DicomTagSQ PatientTreatmentOrientationSequence = new DicomTagSQ(0x3010, 0x0032);

        ///<summary>(3010,0033) VR=SH VM=1 User Content Label</summary>
        public readonly static DicomTagSH UserContentLabel = new DicomTagSH(0x3010, 0x0033);

        ///<summary>(3010,0034) VR=LO VM=1 User Content Long Label</summary>
        public readonly static DicomTagLO UserContentLongLabel = new DicomTagLO(0x3010, 0x0034);

        ///<summary>(3010,0035) VR=SH VM=1 Entity Label</summary>
        public readonly static DicomTagSH EntityLabel = new DicomTagSH(0x3010, 0x0035);

        ///<summary>(3010,0036) VR=LO VM=1 Entity Name</summary>
        public readonly static DicomTagLO EntityName = new DicomTagLO(0x3010, 0x0036);

        ///<summary>(3010,0037) VR=ST VM=1 Entity Description</summary>
        public readonly static DicomTagST EntityDescription = new DicomTagST(0x3010, 0x0037);

        ///<summary>(3010,0038) VR=LO VM=1 Entity Long Label</summary>
        public readonly static DicomTagLO EntityLongLabel = new DicomTagLO(0x3010, 0x0038);

        ///<summary>(3010,0039) VR=US VM=1 Device Index</summary>
        public readonly static DicomTagUS DeviceIndex = new DicomTagUS(0x3010, 0x0039);

        ///<summary>(3010,003A) VR=US VM=1 RT Treatment Phase Index</summary>
        public readonly static DicomTagUS RTTreatmentPhaseIndex = new DicomTagUS(0x3010, 0x003A);

        ///<summary>(3010,003B) VR=UI VM=1 RT Treatment Phase UID</summary>
        public readonly static DicomTagUI RTTreatmentPhaseUID = new DicomTagUI(0x3010, 0x003B);

        ///<summary>(3010,003C) VR=US VM=1 RT Prescription Index</summary>
        public readonly static DicomTagUS RTPrescriptionIndex = new DicomTagUS(0x3010, 0x003C);

        ///<summary>(3010,003D) VR=US VM=1 RT Segment Annotation Index</summary>
        public readonly static DicomTagUS RTSegmentAnnotationIndex = new DicomTagUS(0x3010, 0x003D);

        ///<summary>(3010,003E) VR=US VM=1 Basis RT Treatment Phase Index</summary>
        public readonly static DicomTagUS BasisRTTreatmentPhaseIndex = new DicomTagUS(0x3010, 0x003E);

        ///<summary>(3010,003F) VR=US VM=1 Related RT Treatment Phase Index</summary>
        public readonly static DicomTagUS RelatedRTTreatmentPhaseIndex = new DicomTagUS(0x3010, 0x003F);

        ///<summary>(3010,0040) VR=US VM=1 Referenced RT Treatment Phase Index</summary>
        public readonly static DicomTagUS ReferencedRTTreatmentPhaseIndex = new DicomTagUS(0x3010, 0x0040);

        ///<summary>(3010,0041) VR=US VM=1 Referenced RT Prescription Index</summary>
        public readonly static DicomTagUS ReferencedRTPrescriptionIndex = new DicomTagUS(0x3010, 0x0041);

        ///<summary>(3010,0042) VR=US VM=1 Referenced Parent RT Prescription Index</summary>
        public readonly static DicomTagUS ReferencedParentRTPrescriptionIndex = new DicomTagUS(0x3010, 0x0042);

        ///<summary>(3010,0043) VR=ST VM=1 Manufacturer's Device Identifier</summary>
        public readonly static DicomTagST ManufacturerDeviceIdentifier = new DicomTagST(0x3010, 0x0043);

        ///<summary>(3010,0044) VR=SQ VM=1 Instance-Level Referenced Performed Procedure Step Sequence</summary>
        public readonly static DicomTagSQ InstanceLevelReferencedPerformedProcedureStepSequence = new DicomTagSQ(0x3010, 0x0044);

        ///<summary>(3010,0045) VR=CS VM=1 RT Treatment Phase Intent Presence Flag</summary>
        public readonly static DicomTagCS RTTreatmentPhaseIntentPresenceFlag = new DicomTagCS(0x3010, 0x0045);

        ///<summary>(3010,0046) VR=CS VM=1 Radiotherapy Treatment Type</summary>
        public readonly static DicomTagCS RadiotherapyTreatmentType = new DicomTagCS(0x3010, 0x0046);

        ///<summary>(3010,0047) VR=CS VM=1-n Teletherapy Radiation Type</summary>
        public readonly static DicomTagCSs TeletherapyRadiationType = new DicomTagCSs(0x3010, 0x0047);

        ///<summary>(3010,0048) VR=CS VM=1-n Brachytherapy Source Type</summary>
        public readonly static DicomTagCSs BrachytherapySourceType = new DicomTagCSs(0x3010, 0x0048);

        ///<summary>(3010,0049) VR=SQ VM=1 Referenced RT Treatment Phase Sequence</summary>
        public readonly static DicomTagSQ ReferencedRTTreatmentPhaseSequence = new DicomTagSQ(0x3010, 0x0049);

        ///<summary>(3010,004A) VR=SQ VM=1 Referenced Direct Segment Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedDirectSegmentInstanceSequence = new DicomTagSQ(0x3010, 0x004A);

        ///<summary>(3010,004B) VR=SQ VM=1 Intended RT Treatment Phase Sequence</summary>
        public readonly static DicomTagSQ IntendedRTTreatmentPhaseSequence = new DicomTagSQ(0x3010, 0x004B);

        ///<summary>(3010,004C) VR=DA VM=1 Intended Phase Start Date</summary>
        public readonly static DicomTagDA IntendedPhaseStartDate = new DicomTagDA(0x3010, 0x004C);

        ///<summary>(3010,004D) VR=DA VM=1 Intended Phase End Date</summary>
        public readonly static DicomTagDA IntendedPhaseEndDate = new DicomTagDA(0x3010, 0x004D);

        ///<summary>(3010,004E) VR=SQ VM=1 RT Treatment Phase Interval Sequence</summary>
        public readonly static DicomTagSQ RTTreatmentPhaseIntervalSequence = new DicomTagSQ(0x3010, 0x004E);

        ///<summary>(3010,004F) VR=CS VM=1 Temporal Relationship Interval Anchor</summary>
        public readonly static DicomTagCS TemporalRelationshipIntervalAnchor = new DicomTagCS(0x3010, 0x004F);

        ///<summary>(3010,0050) VR=FD VM=1 Minimum Number of Interval Days</summary>
        public readonly static DicomTagFD MinimumNumberOfIntervalDays = new DicomTagFD(0x3010, 0x0050);

        ///<summary>(3010,0051) VR=FD VM=1 Maximum Number of Interval Days</summary>
        public readonly static DicomTagFD MaximumNumberOfIntervalDays = new DicomTagFD(0x3010, 0x0051);

        ///<summary>(3010,0052) VR=UI VM=1-n Pertinent SOP Classes in Study</summary>
        public readonly static DicomTagUIs PertinentSOPClassesInStudy = new DicomTagUIs(0x3010, 0x0052);

        ///<summary>(3010,0053) VR=UI VM=1-n Pertinent SOP Classes in Series</summary>
        public readonly static DicomTagUIs PertinentSOPClassesInSeries = new DicomTagUIs(0x3010, 0x0053);

        ///<summary>(3010,0054) VR=LO VM=1 RT Prescription Label</summary>
        public readonly static DicomTagLO RTPrescriptionLabel = new DicomTagLO(0x3010, 0x0054);

        ///<summary>(3010,0055) VR=SQ VM=1 RT Physician Intent Predecessor Sequence</summary>
        public readonly static DicomTagSQ RTPhysicianIntentPredecessorSequence = new DicomTagSQ(0x3010, 0x0055);

        ///<summary>(3010,0056) VR=LO VM=1 RT Treatment Approach Label</summary>
        public readonly static DicomTagLO RTTreatmentApproachLabel = new DicomTagLO(0x3010, 0x0056);

        ///<summary>(3010,0057) VR=SQ VM=1 RT Physician Intent Sequence</summary>
        public readonly static DicomTagSQ RTPhysicianIntentSequence = new DicomTagSQ(0x3010, 0x0057);

        ///<summary>(3010,0058) VR=US VM=1 RT Physician Intent Index</summary>
        public readonly static DicomTagUS RTPhysicianIntentIndex = new DicomTagUS(0x3010, 0x0058);

        ///<summary>(3010,0059) VR=CS VM=1 RT Treatment Intent Type</summary>
        public readonly static DicomTagCS RTTreatmentIntentType = new DicomTagCS(0x3010, 0x0059);

        ///<summary>(3010,005A) VR=UT VM=1 RT Physician Intent Narrative</summary>
        public readonly static DicomTagUT RTPhysicianIntentNarrative = new DicomTagUT(0x3010, 0x005A);

        ///<summary>(3010,005B) VR=SQ VM=1 RT Protocol Code Sequence</summary>
        public readonly static DicomTagSQ RTProtocolCodeSequence = new DicomTagSQ(0x3010, 0x005B);

        ///<summary>(3010,005C) VR=ST VM=1 Reason for Superseding</summary>
        public readonly static DicomTagST ReasonForSuperseding = new DicomTagST(0x3010, 0x005C);

        ///<summary>(3010,005D) VR=SQ VM=1 RT Diagnosis Code Sequence</summary>
        public readonly static DicomTagSQ RTDiagnosisCodeSequence = new DicomTagSQ(0x3010, 0x005D);

        ///<summary>(3010,005E) VR=US VM=1 Referenced RT Physician Intent Index</summary>
        public readonly static DicomTagUS ReferencedRTPhysicianIntentIndex = new DicomTagUS(0x3010, 0x005E);

        ///<summary>(3010,005F) VR=SQ VM=1 RT Physician Intent Input Instance Sequence</summary>
        public readonly static DicomTagSQ RTPhysicianIntentInputInstanceSequence = new DicomTagSQ(0x3010, 0x005F);

        ///<summary>(3010,0060) VR=SQ VM=1 RT Anatomic Prescription Sequence</summary>
        public readonly static DicomTagSQ RTAnatomicPrescriptionSequence = new DicomTagSQ(0x3010, 0x0060);

        ///<summary>(3010,0061) VR=UT VM=1 Prior Treatment Dose Description</summary>
        public readonly static DicomTagUT PriorTreatmentDoseDescription = new DicomTagUT(0x3010, 0x0061);

        ///<summary>(3010,0062) VR=SQ VM=1 Prior Treatment Reference Sequence</summary>
        public readonly static DicomTagSQ PriorTreatmentReferenceSequence = new DicomTagSQ(0x3010, 0x0062);

        ///<summary>(3010,0063) VR=CS VM=1 Dosimetric Objective Evaluation Scope</summary>
        public readonly static DicomTagCS DosimetricObjectiveEvaluationScope = new DicomTagCS(0x3010, 0x0063);

        ///<summary>(3010,0064) VR=SQ VM=1 Therapeutic Role Category Code Sequence</summary>
        public readonly static DicomTagSQ TherapeuticRoleCategoryCodeSequence = new DicomTagSQ(0x3010, 0x0064);

        ///<summary>(3010,0065) VR=SQ VM=1 Therapeutic Role Type Code Sequence</summary>
        public readonly static DicomTagSQ TherapeuticRoleTypeCodeSequence = new DicomTagSQ(0x3010, 0x0065);

        ///<summary>(3010,0066) VR=US VM=1 Conceptual Volume Optimization Precedence</summary>
        public readonly static DicomTagUS ConceptualVolumeOptimizationPrecedence = new DicomTagUS(0x3010, 0x0066);

        ///<summary>(3010,0067) VR=SQ VM=1 Conceptual Volume Category Code Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeCategoryCodeSequence = new DicomTagSQ(0x3010, 0x0067);

        ///<summary>(3010,0068) VR=CS VM=1 Conceptual Volume Blocking Constraint</summary>
        public readonly static DicomTagCS ConceptualVolumeBlockingConstraint = new DicomTagCS(0x3010, 0x0068);

        ///<summary>(3010,0069) VR=SQ VM=1 Conceptual Volume Type Code Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeTypeCodeSequence = new DicomTagSQ(0x3010, 0x0069);

        ///<summary>(3010,006A) VR=SQ VM=1 Conceptual Volume Type Modifier Code Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeTypeModifierCodeSequence = new DicomTagSQ(0x3010, 0x006A);

        ///<summary>(3010,006B) VR=SQ VM=1 RT Prescription Sequence</summary>
        public readonly static DicomTagSQ RTPrescriptionSequence = new DicomTagSQ(0x3010, 0x006B);

        ///<summary>(3010,006C) VR=SQ VM=1 Dosimetric Objective Sequence</summary>
        public readonly static DicomTagSQ DosimetricObjectiveSequence = new DicomTagSQ(0x3010, 0x006C);

        ///<summary>(3010,006D) VR=SQ VM=1 Dosimetric Objective Type Code Sequence</summary>
        public readonly static DicomTagSQ DosimetricObjectiveTypeCodeSequence = new DicomTagSQ(0x3010, 0x006D);

        ///<summary>(3010,006E) VR=UI VM=1 Dosimetric Objective UID</summary>
        public readonly static DicomTagUI DosimetricObjectiveUID = new DicomTagUI(0x3010, 0x006E);

        ///<summary>(3010,006F) VR=UI VM=1 Referenced Dosimetric Objective UID</summary>
        public readonly static DicomTagUI ReferencedDosimetricObjectiveUID = new DicomTagUI(0x3010, 0x006F);

        ///<summary>(3010,0070) VR=SQ VM=1 Dosimetric Objective Parameter Sequence</summary>
        public readonly static DicomTagSQ DosimetricObjectiveParameterSequence = new DicomTagSQ(0x3010, 0x0070);

        ///<summary>(3010,0071) VR=SQ VM=1 Referenced Dosimetric Objectives Sequence</summary>
        public readonly static DicomTagSQ ReferencedDosimetricObjectivesSequence = new DicomTagSQ(0x3010, 0x0071);

        ///<summary>(3010,0073) VR=CS VM=1 Absolute Dosimetric Objective Flag</summary>
        public readonly static DicomTagCS AbsoluteDosimetricObjectiveFlag = new DicomTagCS(0x3010, 0x0073);

        ///<summary>(3010,0074) VR=FD VM=1 Dosimetric Objective Weight</summary>
        public readonly static DicomTagFD DosimetricObjectiveWeight = new DicomTagFD(0x3010, 0x0074);

        ///<summary>(3010,0075) VR=CS VM=1 Dosimetric Objective Purpose</summary>
        public readonly static DicomTagCS DosimetricObjectivePurpose = new DicomTagCS(0x3010, 0x0075);

        ///<summary>(3010,0076) VR=SQ VM=1 Planning Input Information Sequence</summary>
        public readonly static DicomTagSQ PlanningInputInformationSequence = new DicomTagSQ(0x3010, 0x0076);

        ///<summary>(3010,0077) VR=LO VM=1 Treatment Site</summary>
        public readonly static DicomTagLO TreatmentSite = new DicomTagLO(0x3010, 0x0077);

        ///<summary>(3010,0078) VR=SQ VM=1 Treatment Site Code Sequence</summary>
        public readonly static DicomTagSQ TreatmentSiteCodeSequence = new DicomTagSQ(0x3010, 0x0078);

        ///<summary>(3010,0079) VR=SQ VM=1 Fraction Pattern Sequence</summary>
        public readonly static DicomTagSQ FractionPatternSequence = new DicomTagSQ(0x3010, 0x0079);

        ///<summary>(3010,007A) VR=UT VM=1 Treatment Technique Notes</summary>
        public readonly static DicomTagUT TreatmentTechniqueNotes = new DicomTagUT(0x3010, 0x007A);

        ///<summary>(3010,007B) VR=UT VM=1 Prescription Notes</summary>
        public readonly static DicomTagUT PrescriptionNotes = new DicomTagUT(0x3010, 0x007B);

        ///<summary>(3010,007C) VR=IS VM=1 Number of Interval Fractions</summary>
        public readonly static DicomTagIS NumberOfIntervalFractions = new DicomTagIS(0x3010, 0x007C);

        ///<summary>(3010,007D) VR=US VM=1 Number of Fractions</summary>
        public readonly static DicomTagUS NumberOfFractions = new DicomTagUS(0x3010, 0x007D);

        ///<summary>(3010,007E) VR=US VM=1 Intended Delivery Duration</summary>
        public readonly static DicomTagUS IntendedDeliveryDuration = new DicomTagUS(0x3010, 0x007E);

        ///<summary>(3010,007F) VR=UT VM=1 Fractionation Notes</summary>
        public readonly static DicomTagUT FractionationNotes = new DicomTagUT(0x3010, 0x007F);

        ///<summary>(3010,0080) VR=SQ VM=1 RT Treatment Technique Code Sequence</summary>
        public readonly static DicomTagSQ RTTreatmentTechniqueCodeSequence = new DicomTagSQ(0x3010, 0x0080);

        ///<summary>(3010,0081) VR=SQ VM=1 Prescription Notes Sequence</summary>
        public readonly static DicomTagSQ PrescriptionNotesSequence = new DicomTagSQ(0x3010, 0x0081);

        ///<summary>(3010,0082) VR=SQ VM=1 Fraction-Based Relationship Sequence</summary>
        public readonly static DicomTagSQ FractionBasedRelationshipSequence = new DicomTagSQ(0x3010, 0x0082);

        ///<summary>(3010,0083) VR=CS VM=1 Fraction-Based Relationship Interval Anchor</summary>
        public readonly static DicomTagCS FractionBasedRelationshipIntervalAnchor = new DicomTagCS(0x3010, 0x0083);

        ///<summary>(3010,0084) VR=FD VM=1 Minimum Hours between Fractions</summary>
        public readonly static DicomTagFD MinimumHoursBetweenFractions = new DicomTagFD(0x3010, 0x0084);

        ///<summary>(3010,0085) VR=TM VM=1-n Intended Fraction Start Time</summary>
        public readonly static DicomTagTMs IntendedFractionStartTime = new DicomTagTMs(0x3010, 0x0085);

        ///<summary>(3010,0086) VR=LT VM=1 Intended Start Day of Week</summary>
        public readonly static DicomTagLT IntendedStartDayOfWeek = new DicomTagLT(0x3010, 0x0086);

        ///<summary>(3010,0087) VR=SQ VM=1 Weekday Fraction Pattern Sequence</summary>
        public readonly static DicomTagSQ WeekdayFractionPatternSequence = new DicomTagSQ(0x3010, 0x0087);

        ///<summary>(3010,0088) VR=SQ VM=1 Delivery Time Structure Code Sequence</summary>
        public readonly static DicomTagSQ DeliveryTimeStructureCodeSequence = new DicomTagSQ(0x3010, 0x0088);

        ///<summary>(3010,0089) VR=SQ VM=1 Treatment Site Modifier Code Sequence</summary>
        public readonly static DicomTagSQ TreatmentSiteModifierCodeSequence = new DicomTagSQ(0x3010, 0x0089);

        ///<summary>(3010,0090) VR=CS VM=1 Robotic Base Location Indicator (RETIRED)</summary>
        public readonly static DicomTagCS RoboticBaseLocationIndicatorRETIRED = new DicomTagCS(0x3010, 0x0090);

        ///<summary>(3010,0091) VR=SQ VM=1 Robotic Path Node Set Code Sequence</summary>
        public readonly static DicomTagSQ RoboticPathNodeSetCodeSequence = new DicomTagSQ(0x3010, 0x0091);

        ///<summary>(3010,0092) VR=UL VM=1 Robotic Node Identifier</summary>
        public readonly static DicomTagUL RoboticNodeIdentifier = new DicomTagUL(0x3010, 0x0092);

        ///<summary>(3010,0093) VR=FD VM=3 RT Treatment Source Coordinates</summary>
        public readonly static DicomTagFDs RTTreatmentSourceCoordinates = new DicomTagFDs(0x3010, 0x0093);

        ///<summary>(3010,0094) VR=FD VM=1 Radiation Source Coordinate SystemYaw Angle</summary>
        public readonly static DicomTagFD RadiationSourceCoordinateSystemYawAngle = new DicomTagFD(0x3010, 0x0094);

        ///<summary>(3010,0095) VR=FD VM=1 Radiation Source Coordinate SystemRoll Angle</summary>
        public readonly static DicomTagFD RadiationSourceCoordinateSystemRollAngle = new DicomTagFD(0x3010, 0x0095);

        ///<summary>(3010,0096) VR=FD VM=1 Radiation Source Coordinate System Pitch Angle</summary>
        public readonly static DicomTagFD RadiationSourceCoordinateSystemPitchAngle = new DicomTagFD(0x3010, 0x0096);

        ///<summary>(3010,0097) VR=SQ VM=1 Robotic Path Control Point Sequence</summary>
        public readonly static DicomTagSQ RoboticPathControlPointSequence = new DicomTagSQ(0x3010, 0x0097);

        ///<summary>(3010,0098) VR=SQ VM=1 Tomotherapeutic Control Point Sequence</summary>
        public readonly static DicomTagSQ TomotherapeuticControlPointSequence = new DicomTagSQ(0x3010, 0x0098);

        ///<summary>(3010,0099) VR=FD VM=1-n Tomotherapeutic Leaf Open Durations</summary>
        public readonly static DicomTagFDs TomotherapeuticLeafOpenDurations = new DicomTagFDs(0x3010, 0x0099);

        ///<summary>(3010,009A) VR=FD VM=1-n Tomotherapeutic Leaf Initial Closed Durations</summary>
        public readonly static DicomTagFDs TomotherapeuticLeafInitialClosedDurations = new DicomTagFDs(0x3010, 0x009A);

        ///<summary>(3010,00A0) VR=SQ VM=1 Conceptual Volume Identification Sequence</summary>
        public readonly static DicomTagSQ ConceptualVolumeIdentificationSequence = new DicomTagSQ(0x3010, 0x00A0);

        ///<summary>(4000,0010) VR=LT VM=1 Arbitrary (RETIRED)</summary>
        public readonly static DicomTagLT ArbitraryRETIRED = new DicomTagLT(0x4000, 0x0010);

        ///<summary>(4000,4000) VR=LT VM=1 Text Comments (RETIRED)</summary>
        public readonly static DicomTagLT TextCommentsRETIRED = new DicomTagLT(0x4000, 0x4000);

        ///<summary>(4008,0040) VR=SH VM=1 Results ID (RETIRED)</summary>
        public readonly static DicomTagSH ResultsIDRETIRED = new DicomTagSH(0x4008, 0x0040);

        ///<summary>(4008,0042) VR=LO VM=1 Results ID Issuer (RETIRED)</summary>
        public readonly static DicomTagLO ResultsIDIssuerRETIRED = new DicomTagLO(0x4008, 0x0042);

        ///<summary>(4008,0050) VR=SQ VM=1 Referenced Interpretation Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ReferencedInterpretationSequenceRETIRED = new DicomTagSQ(0x4008, 0x0050);

        ///<summary>(4008,00FF) VR=CS VM=1 Report Production Status (Trial) (RETIRED)</summary>
        public readonly static DicomTagCS ReportProductionStatusTrialRETIRED = new DicomTagCS(0x4008, 0x00FF);

        ///<summary>(4008,0100) VR=DA VM=1 Interpretation Recorded Date (RETIRED)</summary>
        public readonly static DicomTagDA InterpretationRecordedDateRETIRED = new DicomTagDA(0x4008, 0x0100);

        ///<summary>(4008,0101) VR=TM VM=1 Interpretation Recorded Time (RETIRED)</summary>
        public readonly static DicomTagTM InterpretationRecordedTimeRETIRED = new DicomTagTM(0x4008, 0x0101);

        ///<summary>(4008,0102) VR=PN VM=1 Interpretation Recorder (RETIRED)</summary>
        public readonly static DicomTagPN InterpretationRecorderRETIRED = new DicomTagPN(0x4008, 0x0102);

        ///<summary>(4008,0103) VR=LO VM=1 Reference to Recorded Sound (RETIRED)</summary>
        public readonly static DicomTagLO ReferenceToRecordedSoundRETIRED = new DicomTagLO(0x4008, 0x0103);

        ///<summary>(4008,0108) VR=DA VM=1 Interpretation Transcription Date (RETIRED)</summary>
        public readonly static DicomTagDA InterpretationTranscriptionDateRETIRED = new DicomTagDA(0x4008, 0x0108);

        ///<summary>(4008,0109) VR=TM VM=1 Interpretation Transcription Time (RETIRED)</summary>
        public readonly static DicomTagTM InterpretationTranscriptionTimeRETIRED = new DicomTagTM(0x4008, 0x0109);

        ///<summary>(4008,010A) VR=PN VM=1 Interpretation Transcriber (RETIRED)</summary>
        public readonly static DicomTagPN InterpretationTranscriberRETIRED = new DicomTagPN(0x4008, 0x010A);

        ///<summary>(4008,010B) VR=ST VM=1 Interpretation Text (RETIRED)</summary>
        public readonly static DicomTagST InterpretationTextRETIRED = new DicomTagST(0x4008, 0x010B);

        ///<summary>(4008,010C) VR=PN VM=1 Interpretation Author (RETIRED)</summary>
        public readonly static DicomTagPN InterpretationAuthorRETIRED = new DicomTagPN(0x4008, 0x010C);

        ///<summary>(4008,0111) VR=SQ VM=1 Interpretation Approver Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ InterpretationApproverSequenceRETIRED = new DicomTagSQ(0x4008, 0x0111);

        ///<summary>(4008,0112) VR=DA VM=1 Interpretation Approval Date (RETIRED)</summary>
        public readonly static DicomTagDA InterpretationApprovalDateRETIRED = new DicomTagDA(0x4008, 0x0112);

        ///<summary>(4008,0113) VR=TM VM=1 Interpretation Approval Time (RETIRED)</summary>
        public readonly static DicomTagTM InterpretationApprovalTimeRETIRED = new DicomTagTM(0x4008, 0x0113);

        ///<summary>(4008,0114) VR=PN VM=1 Physician Approving Interpretation (RETIRED)</summary>
        public readonly static DicomTagPN PhysicianApprovingInterpretationRETIRED = new DicomTagPN(0x4008, 0x0114);

        ///<summary>(4008,0115) VR=LT VM=1 Interpretation Diagnosis Description (RETIRED)</summary>
        public readonly static DicomTagLT InterpretationDiagnosisDescriptionRETIRED = new DicomTagLT(0x4008, 0x0115);

        ///<summary>(4008,0117) VR=SQ VM=1 Interpretation Diagnosis Code Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ InterpretationDiagnosisCodeSequenceRETIRED = new DicomTagSQ(0x4008, 0x0117);

        ///<summary>(4008,0118) VR=SQ VM=1 Results Distribution List Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ ResultsDistributionListSequenceRETIRED = new DicomTagSQ(0x4008, 0x0118);

        ///<summary>(4008,0119) VR=PN VM=1 Distribution Name (RETIRED)</summary>
        public readonly static DicomTagPN DistributionNameRETIRED = new DicomTagPN(0x4008, 0x0119);

        ///<summary>(4008,011A) VR=LO VM=1 Distribution Address (RETIRED)</summary>
        public readonly static DicomTagLO DistributionAddressRETIRED = new DicomTagLO(0x4008, 0x011A);

        ///<summary>(4008,0200) VR=SH VM=1 Interpretation ID (RETIRED)</summary>
        public readonly static DicomTagSH InterpretationIDRETIRED = new DicomTagSH(0x4008, 0x0200);

        ///<summary>(4008,0202) VR=LO VM=1 Interpretation ID Issuer (RETIRED)</summary>
        public readonly static DicomTagLO InterpretationIDIssuerRETIRED = new DicomTagLO(0x4008, 0x0202);

        ///<summary>(4008,0210) VR=CS VM=1 Interpretation Type ID (RETIRED)</summary>
        public readonly static DicomTagCS InterpretationTypeIDRETIRED = new DicomTagCS(0x4008, 0x0210);

        ///<summary>(4008,0212) VR=CS VM=1 Interpretation Status ID (RETIRED)</summary>
        public readonly static DicomTagCS InterpretationStatusIDRETIRED = new DicomTagCS(0x4008, 0x0212);

        ///<summary>(4008,0300) VR=ST VM=1 Impressions (RETIRED)</summary>
        public readonly static DicomTagST ImpressionsRETIRED = new DicomTagST(0x4008, 0x0300);

        ///<summary>(4008,4000) VR=ST VM=1 Results Comments (RETIRED)</summary>
        public readonly static DicomTagST ResultsCommentsRETIRED = new DicomTagST(0x4008, 0x4000);

        ///<summary>(4010,0001) VR=CS VM=1 Low Energy Detectors</summary>
        public readonly static DicomTagCS LowEnergyDetectors = new DicomTagCS(0x4010, 0x0001);

        ///<summary>(4010,0002) VR=CS VM=1 High Energy Detectors</summary>
        public readonly static DicomTagCS HighEnergyDetectors = new DicomTagCS(0x4010, 0x0002);

        ///<summary>(4010,0004) VR=SQ VM=1 Detector Geometry Sequence</summary>
        public readonly static DicomTagSQ DetectorGeometrySequence = new DicomTagSQ(0x4010, 0x0004);

        ///<summary>(4010,1001) VR=SQ VM=1 Threat ROI Voxel Sequence</summary>
        public readonly static DicomTagSQ ThreatROIVoxelSequence = new DicomTagSQ(0x4010, 0x1001);

        ///<summary>(4010,1004) VR=FL VM=3 Threat ROI Base</summary>
        public readonly static DicomTagFLs ThreatROIBase = new DicomTagFLs(0x4010, 0x1004);

        ///<summary>(4010,1005) VR=FL VM=3 Threat ROI Extents</summary>
        public readonly static DicomTagFLs ThreatROIExtents = new DicomTagFLs(0x4010, 0x1005);

        ///<summary>(4010,1006) VR=OB VM=1 Threat ROI Bitmap</summary>
        public readonly static DicomTagOB ThreatROIBitmap = new DicomTagOB(0x4010, 0x1006);

        ///<summary>(4010,1007) VR=SH VM=1 Route Segment ID</summary>
        public readonly static DicomTagSH RouteSegmentID = new DicomTagSH(0x4010, 0x1007);

        ///<summary>(4010,1008) VR=CS VM=1 Gantry Type</summary>
        public readonly static DicomTagCS GantryType = new DicomTagCS(0x4010, 0x1008);

        ///<summary>(4010,1009) VR=CS VM=1 OOI Owner Type</summary>
        public readonly static DicomTagCS OOIOwnerType = new DicomTagCS(0x4010, 0x1009);

        ///<summary>(4010,100A) VR=SQ VM=1 Route Segment Sequence</summary>
        public readonly static DicomTagSQ RouteSegmentSequence = new DicomTagSQ(0x4010, 0x100A);

        ///<summary>(4010,1010) VR=US VM=1 Potential Threat Object ID</summary>
        public readonly static DicomTagUS PotentialThreatObjectID = new DicomTagUS(0x4010, 0x1010);

        ///<summary>(4010,1011) VR=SQ VM=1 Threat Sequence</summary>
        public readonly static DicomTagSQ ThreatSequence = new DicomTagSQ(0x4010, 0x1011);

        ///<summary>(4010,1012) VR=CS VM=1 Threat Category</summary>
        public readonly static DicomTagCS ThreatCategory = new DicomTagCS(0x4010, 0x1012);

        ///<summary>(4010,1013) VR=LT VM=1 Threat Category Description</summary>
        public readonly static DicomTagLT ThreatCategoryDescription = new DicomTagLT(0x4010, 0x1013);

        ///<summary>(4010,1014) VR=CS VM=1 ATD Ability Assessment</summary>
        public readonly static DicomTagCS ATDAbilityAssessment = new DicomTagCS(0x4010, 0x1014);

        ///<summary>(4010,1015) VR=CS VM=1 ATD Assessment Flag</summary>
        public readonly static DicomTagCS ATDAssessmentFlag = new DicomTagCS(0x4010, 0x1015);

        ///<summary>(4010,1016) VR=FL VM=1 ATD Assessment Probability</summary>
        public readonly static DicomTagFL ATDAssessmentProbability = new DicomTagFL(0x4010, 0x1016);

        ///<summary>(4010,1017) VR=FL VM=1 Mass</summary>
        public readonly static DicomTagFL Mass = new DicomTagFL(0x4010, 0x1017);

        ///<summary>(4010,1018) VR=FL VM=1 Density</summary>
        public readonly static DicomTagFL Density = new DicomTagFL(0x4010, 0x1018);

        ///<summary>(4010,1019) VR=FL VM=1 Z Effective</summary>
        public readonly static DicomTagFL ZEffective = new DicomTagFL(0x4010, 0x1019);

        ///<summary>(4010,101A) VR=SH VM=1 Boarding Pass ID</summary>
        public readonly static DicomTagSH BoardingPassID = new DicomTagSH(0x4010, 0x101A);

        ///<summary>(4010,101B) VR=FL VM=3 Center of Mass</summary>
        public readonly static DicomTagFLs CenterOfMass = new DicomTagFLs(0x4010, 0x101B);

        ///<summary>(4010,101C) VR=FL VM=3 Center of PTO</summary>
        public readonly static DicomTagFLs CenterOfPTO = new DicomTagFLs(0x4010, 0x101C);

        ///<summary>(4010,101D) VR=FL VM=6-n Bounding Polygon</summary>
        public readonly static DicomTagFLs BoundingPolygon = new DicomTagFLs(0x4010, 0x101D);

        ///<summary>(4010,101E) VR=SH VM=1 Route Segment Start Location ID</summary>
        public readonly static DicomTagSH RouteSegmentStartLocationID = new DicomTagSH(0x4010, 0x101E);

        ///<summary>(4010,101F) VR=SH VM=1 Route Segment End Location ID</summary>
        public readonly static DicomTagSH RouteSegmentEndLocationID = new DicomTagSH(0x4010, 0x101F);

        ///<summary>(4010,1020) VR=CS VM=1 Route Segment Location ID Type</summary>
        public readonly static DicomTagCS RouteSegmentLocationIDType = new DicomTagCS(0x4010, 0x1020);

        ///<summary>(4010,1021) VR=CS VM=1-n Abort Reason</summary>
        public readonly static DicomTagCSs AbortReason = new DicomTagCSs(0x4010, 0x1021);

        ///<summary>(4010,1023) VR=FL VM=1 Volume of PTO</summary>
        public readonly static DicomTagFL VolumeOfPTO = new DicomTagFL(0x4010, 0x1023);

        ///<summary>(4010,1024) VR=CS VM=1 Abort Flag</summary>
        public readonly static DicomTagCS AbortFlag = new DicomTagCS(0x4010, 0x1024);

        ///<summary>(4010,1025) VR=DT VM=1 Route Segment Start Time</summary>
        public readonly static DicomTagDT RouteSegmentStartTime = new DicomTagDT(0x4010, 0x1025);

        ///<summary>(4010,1026) VR=DT VM=1 Route Segment End Time</summary>
        public readonly static DicomTagDT RouteSegmentEndTime = new DicomTagDT(0x4010, 0x1026);

        ///<summary>(4010,1027) VR=CS VM=1 TDR Type</summary>
        public readonly static DicomTagCS TDRType = new DicomTagCS(0x4010, 0x1027);

        ///<summary>(4010,1028) VR=CS VM=1 International Route Segment</summary>
        public readonly static DicomTagCS InternationalRouteSegment = new DicomTagCS(0x4010, 0x1028);

        ///<summary>(4010,1029) VR=LO VM=1-n Threat Detection Algorithm and Version</summary>
        public readonly static DicomTagLOs ThreatDetectionAlgorithmAndVersion = new DicomTagLOs(0x4010, 0x1029);

        ///<summary>(4010,102A) VR=SH VM=1 Assigned Location</summary>
        public readonly static DicomTagSH AssignedLocation = new DicomTagSH(0x4010, 0x102A);

        ///<summary>(4010,102B) VR=DT VM=1 Alarm Decision Time</summary>
        public readonly static DicomTagDT AlarmDecisionTime = new DicomTagDT(0x4010, 0x102B);

        ///<summary>(4010,1031) VR=CS VM=1 Alarm Decision</summary>
        public readonly static DicomTagCS AlarmDecision = new DicomTagCS(0x4010, 0x1031);

        ///<summary>(4010,1033) VR=US VM=1 Number of Total Objects</summary>
        public readonly static DicomTagUS NumberOfTotalObjects = new DicomTagUS(0x4010, 0x1033);

        ///<summary>(4010,1034) VR=US VM=1 Number of Alarm Objects</summary>
        public readonly static DicomTagUS NumberOfAlarmObjects = new DicomTagUS(0x4010, 0x1034);

        ///<summary>(4010,1037) VR=SQ VM=1 PTO Representation Sequence</summary>
        public readonly static DicomTagSQ PTORepresentationSequence = new DicomTagSQ(0x4010, 0x1037);

        ///<summary>(4010,1038) VR=SQ VM=1 ATD Assessment Sequence</summary>
        public readonly static DicomTagSQ ATDAssessmentSequence = new DicomTagSQ(0x4010, 0x1038);

        ///<summary>(4010,1039) VR=CS VM=1 TIP Type</summary>
        public readonly static DicomTagCS TIPType = new DicomTagCS(0x4010, 0x1039);

        ///<summary>(4010,103A) VR=CS VM=1 DICOS Version</summary>
        public readonly static DicomTagCS DICOSVersion = new DicomTagCS(0x4010, 0x103A);

        ///<summary>(4010,1041) VR=DT VM=1 OOI Owner Creation Time</summary>
        public readonly static DicomTagDT OOIOwnerCreationTime = new DicomTagDT(0x4010, 0x1041);

        ///<summary>(4010,1042) VR=CS VM=1 OOI Type</summary>
        public readonly static DicomTagCS OOIType = new DicomTagCS(0x4010, 0x1042);

        ///<summary>(4010,1043) VR=FL VM=3 OOI Size</summary>
        public readonly static DicomTagFLs OOISize = new DicomTagFLs(0x4010, 0x1043);

        ///<summary>(4010,1044) VR=CS VM=1 Acquisition Status</summary>
        public readonly static DicomTagCS AcquisitionStatus = new DicomTagCS(0x4010, 0x1044);

        ///<summary>(4010,1045) VR=SQ VM=1 Basis Materials Code Sequence</summary>
        public readonly static DicomTagSQ BasisMaterialsCodeSequence = new DicomTagSQ(0x4010, 0x1045);

        ///<summary>(4010,1046) VR=CS VM=1 Phantom Type</summary>
        public readonly static DicomTagCS PhantomType = new DicomTagCS(0x4010, 0x1046);

        ///<summary>(4010,1047) VR=SQ VM=1 OOI Owner Sequence</summary>
        public readonly static DicomTagSQ OOIOwnerSequence = new DicomTagSQ(0x4010, 0x1047);

        ///<summary>(4010,1048) VR=CS VM=1 Scan Type</summary>
        public readonly static DicomTagCS ScanType = new DicomTagCS(0x4010, 0x1048);

        ///<summary>(4010,1051) VR=LO VM=1 Itinerary ID</summary>
        public readonly static DicomTagLO ItineraryID = new DicomTagLO(0x4010, 0x1051);

        ///<summary>(4010,1052) VR=SH VM=1 Itinerary ID Type</summary>
        public readonly static DicomTagSH ItineraryIDType = new DicomTagSH(0x4010, 0x1052);

        ///<summary>(4010,1053) VR=LO VM=1 Itinerary ID Assigning Authority</summary>
        public readonly static DicomTagLO ItineraryIDAssigningAuthority = new DicomTagLO(0x4010, 0x1053);

        ///<summary>(4010,1054) VR=SH VM=1 Route ID</summary>
        public readonly static DicomTagSH RouteID = new DicomTagSH(0x4010, 0x1054);

        ///<summary>(4010,1055) VR=SH VM=1 Route ID Assigning Authority</summary>
        public readonly static DicomTagSH RouteIDAssigningAuthority = new DicomTagSH(0x4010, 0x1055);

        ///<summary>(4010,1056) VR=CS VM=1 Inbound Arrival Type</summary>
        public readonly static DicomTagCS InboundArrivalType = new DicomTagCS(0x4010, 0x1056);

        ///<summary>(4010,1058) VR=SH VM=1 Carrier ID</summary>
        public readonly static DicomTagSH CarrierID = new DicomTagSH(0x4010, 0x1058);

        ///<summary>(4010,1059) VR=CS VM=1 Carrier ID Assigning Authority</summary>
        public readonly static DicomTagCS CarrierIDAssigningAuthority = new DicomTagCS(0x4010, 0x1059);

        ///<summary>(4010,1060) VR=FL VM=3 Source Orientation</summary>
        public readonly static DicomTagFLs SourceOrientation = new DicomTagFLs(0x4010, 0x1060);

        ///<summary>(4010,1061) VR=FL VM=3 Source Position</summary>
        public readonly static DicomTagFLs SourcePosition = new DicomTagFLs(0x4010, 0x1061);

        ///<summary>(4010,1062) VR=FL VM=1 Belt Height</summary>
        public readonly static DicomTagFL BeltHeight = new DicomTagFL(0x4010, 0x1062);

        ///<summary>(4010,1064) VR=SQ VM=1 Algorithm Routing Code Sequence</summary>
        public readonly static DicomTagSQ AlgorithmRoutingCodeSequence = new DicomTagSQ(0x4010, 0x1064);

        ///<summary>(4010,1067) VR=CS VM=1 Transport Classification</summary>
        public readonly static DicomTagCS TransportClassification = new DicomTagCS(0x4010, 0x1067);

        ///<summary>(4010,1068) VR=LT VM=1 OOI Type Descriptor</summary>
        public readonly static DicomTagLT OOITypeDescriptor = new DicomTagLT(0x4010, 0x1068);

        ///<summary>(4010,1069) VR=FL VM=1 Total Processing Time</summary>
        public readonly static DicomTagFL TotalProcessingTime = new DicomTagFL(0x4010, 0x1069);

        ///<summary>(4010,106C) VR=OB VM=1 Detector Calibration Data</summary>
        public readonly static DicomTagOB DetectorCalibrationData = new DicomTagOB(0x4010, 0x106C);

        ///<summary>(4010,106D) VR=CS VM=1 Additional Screening Performed</summary>
        public readonly static DicomTagCS AdditionalScreeningPerformed = new DicomTagCS(0x4010, 0x106D);

        ///<summary>(4010,106E) VR=CS VM=1 Additional Inspection Selection Criteria</summary>
        public readonly static DicomTagCS AdditionalInspectionSelectionCriteria = new DicomTagCS(0x4010, 0x106E);

        ///<summary>(4010,106F) VR=SQ VM=1 Additional Inspection Method Sequence</summary>
        public readonly static DicomTagSQ AdditionalInspectionMethodSequence = new DicomTagSQ(0x4010, 0x106F);

        ///<summary>(4010,1070) VR=CS VM=1 AIT Device Type</summary>
        public readonly static DicomTagCS AITDeviceType = new DicomTagCS(0x4010, 0x1070);

        ///<summary>(4010,1071) VR=SQ VM=1 QR Measurements Sequence</summary>
        public readonly static DicomTagSQ QRMeasurementsSequence = new DicomTagSQ(0x4010, 0x1071);

        ///<summary>(4010,1072) VR=SQ VM=1 Target Material Sequence</summary>
        public readonly static DicomTagSQ TargetMaterialSequence = new DicomTagSQ(0x4010, 0x1072);

        ///<summary>(4010,1073) VR=FD VM=1 SNR Threshold</summary>
        public readonly static DicomTagFD SNRThreshold = new DicomTagFD(0x4010, 0x1073);

        ///<summary>(4010,1075) VR=DS VM=1 Image Scale Representation</summary>
        public readonly static DicomTagDS ImageScaleRepresentation = new DicomTagDS(0x4010, 0x1075);

        ///<summary>(4010,1076) VR=SQ VM=1 Referenced PTO Sequence</summary>
        public readonly static DicomTagSQ ReferencedPTOSequence = new DicomTagSQ(0x4010, 0x1076);

        ///<summary>(4010,1077) VR=SQ VM=1 Referenced TDR Instance Sequence</summary>
        public readonly static DicomTagSQ ReferencedTDRInstanceSequence = new DicomTagSQ(0x4010, 0x1077);

        ///<summary>(4010,1078) VR=ST VM=1 PTO Location Description</summary>
        public readonly static DicomTagST PTOLocationDescription = new DicomTagST(0x4010, 0x1078);

        ///<summary>(4010,1079) VR=SQ VM=1 Anomaly Locator Indicator Sequence</summary>
        public readonly static DicomTagSQ AnomalyLocatorIndicatorSequence = new DicomTagSQ(0x4010, 0x1079);

        ///<summary>(4010,107A) VR=FL VM=3 Anomaly Locator Indicator</summary>
        public readonly static DicomTagFLs AnomalyLocatorIndicator = new DicomTagFLs(0x4010, 0x107A);

        ///<summary>(4010,107B) VR=SQ VM=1 PTO Region Sequence</summary>
        public readonly static DicomTagSQ PTORegionSequence = new DicomTagSQ(0x4010, 0x107B);

        ///<summary>(4010,107C) VR=CS VM=1 Inspection Selection Criteria</summary>
        public readonly static DicomTagCS InspectionSelectionCriteria = new DicomTagCS(0x4010, 0x107C);

        ///<summary>(4010,107D) VR=SQ VM=1 Secondary Inspection Method Sequence</summary>
        public readonly static DicomTagSQ SecondaryInspectionMethodSequence = new DicomTagSQ(0x4010, 0x107D);

        ///<summary>(4010,107E) VR=DS VM=6 PRCS to RCS Orientation</summary>
        public readonly static DicomTagDSs PRCSToRCSOrientation = new DicomTagDSs(0x4010, 0x107E);

        ///<summary>(4FFE,0001) VR=SQ VM=1 MAC Parameters Sequence</summary>
        public readonly static DicomTagSQ MACParametersSequence = new DicomTagSQ(0x4FFE, 0x0001);

        ///<summary>(50xx,0005) VR=US VM=1 Curve Dimensions (RETIRED)</summary>
        public readonly static DicomTagUS CurveDimensionsRETIRED = new DicomTagUS(0x5000, 0x0005);

        ///<summary>(50xx,0010) VR=US VM=1 Number of Points (RETIRED)</summary>
        public readonly static DicomTagUS NumberOfPointsRETIRED = new DicomTagUS(0x5000, 0x0010);

        ///<summary>(50xx,0020) VR=CS VM=1 Type of Data (RETIRED)</summary>
        public readonly static DicomTagCS TypeOfDataRETIRED = new DicomTagCS(0x5000, 0x0020);

        ///<summary>(50xx,0022) VR=LO VM=1 Curve Description (RETIRED)</summary>
        public readonly static DicomTagLO CurveDescriptionRETIRED = new DicomTagLO(0x5000, 0x0022);

        ///<summary>(50xx,0030) VR=SH VM=1-n Axis Units (RETIRED)</summary>
        public readonly static DicomTagSHs AxisUnitsRETIRED = new DicomTagSHs(0x5000, 0x0030);

        ///<summary>(50xx,0040) VR=SH VM=1-n Axis Labels (RETIRED)</summary>
        public readonly static DicomTagSHs AxisLabelsRETIRED = new DicomTagSHs(0x5000, 0x0040);

        ///<summary>(50xx,0103) VR=US VM=1 Data Value Representation (RETIRED)</summary>
        public readonly static DicomTagUS DataValueRepresentationRETIRED = new DicomTagUS(0x5000, 0x0103);

        ///<summary>(50xx,0104) VR=US VM=1-n Minimum Coordinate Value (RETIRED)</summary>
        public readonly static DicomTagUSs MinimumCoordinateValueRETIRED = new DicomTagUSs(0x5000, 0x0104);

        ///<summary>(50xx,0105) VR=US VM=1-n Maximum Coordinate Value (RETIRED)</summary>
        public readonly static DicomTagUSs MaximumCoordinateValueRETIRED = new DicomTagUSs(0x5000, 0x0105);

        ///<summary>(50xx,0106) VR=SH VM=1-n Curve Range (RETIRED)</summary>
        public readonly static DicomTagSHs CurveRangeRETIRED = new DicomTagSHs(0x5000, 0x0106);

        ///<summary>(50xx,0110) VR=US VM=1-n Curve Data Descriptor (RETIRED)</summary>
        public readonly static DicomTagUSs CurveDataDescriptorRETIRED = new DicomTagUSs(0x5000, 0x0110);

        ///<summary>(50xx,0112) VR=US VM=1-n Coordinate Start Value (RETIRED)</summary>
        public readonly static DicomTagUSs CoordinateStartValueRETIRED = new DicomTagUSs(0x5000, 0x0112);

        ///<summary>(50xx,0114) VR=US VM=1-n Coordinate Step Value (RETIRED)</summary>
        public readonly static DicomTagUSs CoordinateStepValueRETIRED = new DicomTagUSs(0x5000, 0x0114);

        ///<summary>(50xx,1001) VR=CS VM=1 Curve Activation Layer (RETIRED)</summary>
        public readonly static DicomTagCS CurveActivationLayerRETIRED = new DicomTagCS(0x5000, 0x1001);

        ///<summary>(50xx,2000) VR=US VM=1 Audio Type (RETIRED)</summary>
        public readonly static DicomTagUS AudioTypeRETIRED = new DicomTagUS(0x5000, 0x2000);

        ///<summary>(50xx,2002) VR=US VM=1 Audio Sample Format (RETIRED)</summary>
        public readonly static DicomTagUS AudioSampleFormatRETIRED = new DicomTagUS(0x5000, 0x2002);

        ///<summary>(50xx,2004) VR=US VM=1 Number of Channels (RETIRED)</summary>
        public readonly static DicomTagUS NumberOfChannelsRETIRED = new DicomTagUS(0x5000, 0x2004);

        ///<summary>(50xx,2006) VR=UL VM=1 Number of Samples (RETIRED)</summary>
        public readonly static DicomTagUL NumberOfSamplesRETIRED = new DicomTagUL(0x5000, 0x2006);

        ///<summary>(50xx,2008) VR=UL VM=1 Sample Rate (RETIRED)</summary>
        public readonly static DicomTagUL SampleRateRETIRED = new DicomTagUL(0x5000, 0x2008);

        ///<summary>(50xx,200A) VR=UL VM=1 Total Time (RETIRED)</summary>
        public readonly static DicomTagUL TotalTimeRETIRED = new DicomTagUL(0x5000, 0x200A);

        ///<summary>(50xx,200C) VR=OB/OW VM=1 Audio Sample Data (RETIRED)</summary>
        public readonly static DicomTagOBOW AudioSampleDataRETIRED = new DicomTagOBOW(0x5000, 0x200C);

        ///<summary>(50xx,200E) VR=LT VM=1 Audio Comments (RETIRED)</summary>
        public readonly static DicomTagLT AudioCommentsRETIRED = new DicomTagLT(0x5000, 0x200E);

        ///<summary>(50xx,2500) VR=LO VM=1 Curve Label (RETIRED)</summary>
        public readonly static DicomTagLO CurveLabelRETIRED = new DicomTagLO(0x5000, 0x2500);

        ///<summary>(50xx,2600) VR=SQ VM=1 Curve Referenced Overlay Sequence (RETIRED)</summary>
        public readonly static DicomTagSQ CurveReferencedOverlaySequenceRETIRED = new DicomTagSQ(0x5000, 0x2600);

        ///<summary>(50xx,2610) VR=US VM=1 Curve Referenced Overlay Group (RETIRED)</summary>
        public readonly static DicomTagUS CurveReferencedOverlayGroupRETIRED = new DicomTagUS(0x5000, 0x2610);

        ///<summary>(50xx,3000) VR=OB/OW VM=1 Curve Data (RETIRED)</summary>
        public readonly static DicomTagOBOW CurveDataRETIRED = new DicomTagOBOW(0x5000, 0x3000);

        ///<summary>(5200,9229) VR=SQ VM=1 Shared Functional Groups Sequence</summary>
        public readonly static DicomTagSQ SharedFunctionalGroupsSequence = new DicomTagSQ(0x5200, 0x9229);

        ///<summary>(5200,9230) VR=SQ VM=1 Per-Frame Functional Groups Sequence</summary>
        public readonly static DicomTagSQ PerFrameFunctionalGroupsSequence = new DicomTagSQ(0x5200, 0x9230);

        ///<summary>(5400,0100) VR=SQ VM=1 Waveform Sequence</summary>
        public readonly static DicomTagSQ WaveformSequence = new DicomTagSQ(0x5400, 0x0100);

        ///<summary>(5400,0110) VR=OB/OW VM=1 Channel Minimum Value</summary>
        public readonly static DicomTagOBOW ChannelMinimumValue = new DicomTagOBOW(0x5400, 0x0110);

        ///<summary>(5400,0112) VR=OB/OW VM=1 Channel Maximum Value</summary>
        public readonly static DicomTagOBOW ChannelMaximumValue = new DicomTagOBOW(0x5400, 0x0112);

        ///<summary>(5400,1004) VR=US VM=1 Waveform Bits Allocated</summary>
        public readonly static DicomTagUS WaveformBitsAllocated = new DicomTagUS(0x5400, 0x1004);

        ///<summary>(5400,1006) VR=CS VM=1 Waveform Sample Interpretation</summary>
        public readonly static DicomTagCS WaveformSampleInterpretation = new DicomTagCS(0x5400, 0x1006);

        ///<summary>(5400,100A) VR=OB/OW VM=1 Waveform Padding Value</summary>
        public readonly static DicomTagOBOW WaveformPaddingValue = new DicomTagOBOW(0x5400, 0x100A);

        ///<summary>(5400,1010) VR=OB/OW VM=1 Waveform Data</summary>
        public readonly static DicomTagOBOW WaveformData = new DicomTagOBOW(0x5400, 0x1010);

        ///<summary>(5600,0010) VR=OF VM=1 First Order Phase Correction Angle</summary>
        public readonly static DicomTagOF FirstOrderPhaseCorrectionAngle = new DicomTagOF(0x5600, 0x0010);

        ///<summary>(5600,0020) VR=OF VM=1 Spectroscopy Data</summary>
        public readonly static DicomTagOF SpectroscopyData = new DicomTagOF(0x5600, 0x0020);

        ///<summary>(60xx,0010) VR=US VM=1 Overlay Rows</summary>
        public readonly static DicomTagUS OverlayRows = new DicomTagUS(0x6000, 0x0010);

        ///<summary>(60xx,0011) VR=US VM=1 Overlay Columns</summary>
        public readonly static DicomTagUS OverlayColumns = new DicomTagUS(0x6000, 0x0011);

        ///<summary>(60xx,0012) VR=US VM=1 Overlay Planes (RETIRED)</summary>
        public readonly static DicomTagUS OverlayPlanesRETIRED = new DicomTagUS(0x6000, 0x0012);

        ///<summary>(60xx,0015) VR=IS VM=1 Number of Frames in Overlay</summary>
        public readonly static DicomTagIS NumberOfFramesInOverlay = new DicomTagIS(0x6000, 0x0015);

        ///<summary>(60xx,0022) VR=LO VM=1 Overlay Description</summary>
        public readonly static DicomTagLO OverlayDescription = new DicomTagLO(0x6000, 0x0022);

        ///<summary>(60xx,0040) VR=CS VM=1 Overlay Type</summary>
        public readonly static DicomTagCS OverlayType = new DicomTagCS(0x6000, 0x0040);

        ///<summary>(60xx,0045) VR=LO VM=1 Overlay Subtype</summary>
        public readonly static DicomTagLO OverlaySubtype = new DicomTagLO(0x6000, 0x0045);

        ///<summary>(60xx,0050) VR=SS VM=2 Overlay Origin</summary>
        public readonly static DicomTagSSs OverlayOrigin = new DicomTagSSs(0x6000, 0x0050);

        ///<summary>(60xx,0051) VR=US VM=1 Image Frame Origin</summary>
        public readonly static DicomTagUS ImageFrameOrigin = new DicomTagUS(0x6000, 0x0051);

        ///<summary>(60xx,0052) VR=US VM=1 Overlay Plane Origin (RETIRED)</summary>
        public readonly static DicomTagUS OverlayPlaneOriginRETIRED = new DicomTagUS(0x6000, 0x0052);

        ///<summary>(60xx,0060) VR=CS VM=1 Overlay Compression Code (RETIRED)</summary>
        public readonly static DicomTagCS OverlayCompressionCodeRETIRED = new DicomTagCS(0x6000, 0x0060);

        ///<summary>(60xx,0061) VR=SH VM=1 Overlay Compression Originator (RETIRED)</summary>
        public readonly static DicomTagSH OverlayCompressionOriginatorRETIRED = new DicomTagSH(0x6000, 0x0061);

        ///<summary>(60xx,0062) VR=SH VM=1 Overlay Compression Label (RETIRED)</summary>
        public readonly static DicomTagSH OverlayCompressionLabelRETIRED = new DicomTagSH(0x6000, 0x0062);

        ///<summary>(60xx,0063) VR=CS VM=1 Overlay Compression Description (RETIRED)</summary>
        public readonly static DicomTagCS OverlayCompressionDescriptionRETIRED = new DicomTagCS(0x6000, 0x0063);

        ///<summary>(60xx,0066) VR=AT VM=1-n Overlay Compression Step Pointers (RETIRED)</summary>
        public readonly static DicomTagATs OverlayCompressionStepPointersRETIRED = new DicomTagATs(0x6000, 0x0066);

        ///<summary>(60xx,0068) VR=US VM=1 Overlay Repeat Interval (RETIRED)</summary>
        public readonly static DicomTagUS OverlayRepeatIntervalRETIRED = new DicomTagUS(0x6000, 0x0068);

        ///<summary>(60xx,0069) VR=US VM=1 Overlay Bits Grouped (RETIRED)</summary>
        public readonly static DicomTagUS OverlayBitsGroupedRETIRED = new DicomTagUS(0x6000, 0x0069);

        ///<summary>(60xx,0100) VR=US VM=1 Overlay Bits Allocated</summary>
        public readonly static DicomTagUS OverlayBitsAllocated = new DicomTagUS(0x6000, 0x0100);

        ///<summary>(60xx,0102) VR=US VM=1 Overlay Bit Position</summary>
        public readonly static DicomTagUS OverlayBitPosition = new DicomTagUS(0x6000, 0x0102);

        ///<summary>(60xx,0110) VR=CS VM=1 Overlay Format (RETIRED)</summary>
        public readonly static DicomTagCS OverlayFormatRETIRED = new DicomTagCS(0x6000, 0x0110);

        ///<summary>(60xx,0200) VR=US VM=1 Overlay Location (RETIRED)</summary>
        public readonly static DicomTagUS OverlayLocationRETIRED = new DicomTagUS(0x6000, 0x0200);

        ///<summary>(60xx,0800) VR=CS VM=1-n Overlay Code Label (RETIRED)</summary>
        public readonly static DicomTagCSs OverlayCodeLabelRETIRED = new DicomTagCSs(0x6000, 0x0800);

        ///<summary>(60xx,0802) VR=US VM=1 Overlay Number of Tables (RETIRED)</summary>
        public readonly static DicomTagUS OverlayNumberOfTablesRETIRED = new DicomTagUS(0x6000, 0x0802);

        ///<summary>(60xx,0803) VR=AT VM=1-n Overlay Code Table Location (RETIRED)</summary>
        public readonly static DicomTagATs OverlayCodeTableLocationRETIRED = new DicomTagATs(0x6000, 0x0803);

        ///<summary>(60xx,0804) VR=US VM=1 Overlay Bits For Code Word (RETIRED)</summary>
        public readonly static DicomTagUS OverlayBitsForCodeWordRETIRED = new DicomTagUS(0x6000, 0x0804);

        ///<summary>(60xx,1001) VR=CS VM=1 Overlay Activation Layer</summary>
        public readonly static DicomTagCS OverlayActivationLayer = new DicomTagCS(0x6000, 0x1001);

        ///<summary>(60xx,1100) VR=US VM=1 Overlay Descriptor - Gray (RETIRED)</summary>
        public readonly static DicomTagUS OverlayDescriptorGrayRETIRED = new DicomTagUS(0x6000, 0x1100);

        ///<summary>(60xx,1101) VR=US VM=1 Overlay Descriptor - Red (RETIRED)</summary>
        public readonly static DicomTagUS OverlayDescriptorRedRETIRED = new DicomTagUS(0x6000, 0x1101);

        ///<summary>(60xx,1102) VR=US VM=1 Overlay Descriptor - Green (RETIRED)</summary>
        public readonly static DicomTagUS OverlayDescriptorGreenRETIRED = new DicomTagUS(0x6000, 0x1102);

        ///<summary>(60xx,1103) VR=US VM=1 Overlay Descriptor - Blue (RETIRED)</summary>
        public readonly static DicomTagUS OverlayDescriptorBlueRETIRED = new DicomTagUS(0x6000, 0x1103);

        ///<summary>(60xx,1200) VR=US VM=1-n Overlays - Gray (RETIRED)</summary>
        public readonly static DicomTagUSs OverlaysGrayRETIRED = new DicomTagUSs(0x6000, 0x1200);

        ///<summary>(60xx,1201) VR=US VM=1-n Overlays - Red (RETIRED)</summary>
        public readonly static DicomTagUSs OverlaysRedRETIRED = new DicomTagUSs(0x6000, 0x1201);

        ///<summary>(60xx,1202) VR=US VM=1-n Overlays - Green (RETIRED)</summary>
        public readonly static DicomTagUSs OverlaysGreenRETIRED = new DicomTagUSs(0x6000, 0x1202);

        ///<summary>(60xx,1203) VR=US VM=1-n Overlays - Blue (RETIRED)</summary>
        public readonly static DicomTagUSs OverlaysBlueRETIRED = new DicomTagUSs(0x6000, 0x1203);

        ///<summary>(60xx,1301) VR=IS VM=1 ROI Area</summary>
        public readonly static DicomTagIS ROIArea = new DicomTagIS(0x6000, 0x1301);

        ///<summary>(60xx,1302) VR=DS VM=1 ROI Mean</summary>
        public readonly static DicomTagDS ROIMean = new DicomTagDS(0x6000, 0x1302);

        ///<summary>(60xx,1303) VR=DS VM=1 ROI Standard Deviation</summary>
        public readonly static DicomTagDS ROIStandardDeviation = new DicomTagDS(0x6000, 0x1303);

        ///<summary>(60xx,1500) VR=LO VM=1 Overlay Label</summary>
        public readonly static DicomTagLO OverlayLabel = new DicomTagLO(0x6000, 0x1500);

        ///<summary>(60xx,3000) VR=OB/OW VM=1 Overlay Data</summary>
        public readonly static DicomTagOBOW OverlayData = new DicomTagOBOW(0x6000, 0x3000);

        ///<summary>(60xx,4000) VR=LT VM=1 Overlay Comments (RETIRED)</summary>
        public readonly static DicomTagLT OverlayCommentsRETIRED = new DicomTagLT(0x6000, 0x4000);

        ///<summary>(7FE0,0001) VR=OV VM=1 Extended Offset Table</summary>
        public readonly static DicomTagOV ExtendedOffsetTable = new DicomTagOV(0x7FE0, 0x0001);

        ///<summary>(7FE0,0002) VR=OV VM=1 Extended Offset Table Lengths</summary>
        public readonly static DicomTagOV ExtendedOffsetTableLengths = new DicomTagOV(0x7FE0, 0x0002);

        ///<summary>(7FE0,0003) VR=UV VM=1 Encapsulated Pixel Data Value Total Length</summary>
        public readonly static DicomTagUV EncapsulatedPixelDataValueTotalLength = new DicomTagUV(0x7FE0, 0x0003);

        ///<summary>(7FE0,0008) VR=OF VM=1 Float Pixel Data</summary>
        public readonly static DicomTagOF FloatPixelData = new DicomTagOF(0x7FE0, 0x0008);

        ///<summary>(7FE0,0009) VR=OD VM=1 Double Float Pixel Data</summary>
        public readonly static DicomTagOD DoubleFloatPixelData = new DicomTagOD(0x7FE0, 0x0009);

        ///<summary>(7FE0,0010) VR=OB/OW VM=1 Pixel Data</summary>
        public readonly static DicomTagOBOW PixelData = new DicomTagOBOW(0x7FE0, 0x0010);

        ///<summary>(7FE0,0020) VR=OW VM=1 Coefficients SDVN (RETIRED)</summary>
        public readonly static DicomTagOW CoefficientsSDVNRETIRED = new DicomTagOW(0x7FE0, 0x0020);

        ///<summary>(7FE0,0030) VR=OW VM=1 Coefficients SDHN (RETIRED)</summary>
        public readonly static DicomTagOW CoefficientsSDHNRETIRED = new DicomTagOW(0x7FE0, 0x0030);

        ///<summary>(7FE0,0040) VR=OW VM=1 Coefficients SDDN (RETIRED)</summary>
        public readonly static DicomTagOW CoefficientsSDDNRETIRED = new DicomTagOW(0x7FE0, 0x0040);

        ///<summary>(7Fxx,0010) VR=OB/OW VM=1 Variable Pixel Data (RETIRED)</summary>
        public readonly static DicomTagOBOW VariablePixelDataRETIRED = new DicomTagOBOW(0x7F00, 0x0010);

        ///<summary>(7Fxx,0011) VR=US VM=1 Variable Next Data Group (RETIRED)</summary>
        public readonly static DicomTagUS VariableNextDataGroupRETIRED = new DicomTagUS(0x7F00, 0x0011);

        ///<summary>(7Fxx,0020) VR=OW VM=1 Variable Coefficients SDVN (RETIRED)</summary>
        public readonly static DicomTagOW VariableCoefficientsSDVNRETIRED = new DicomTagOW(0x7F00, 0x0020);

        ///<summary>(7Fxx,0030) VR=OW VM=1 Variable Coefficients SDHN (RETIRED)</summary>
        public readonly static DicomTagOW VariableCoefficientsSDHNRETIRED = new DicomTagOW(0x7F00, 0x0030);

        ///<summary>(7Fxx,0040) VR=OW VM=1 Variable Coefficients SDDN (RETIRED)</summary>
        public readonly static DicomTagOW VariableCoefficientsSDDNRETIRED = new DicomTagOW(0x7F00, 0x0040);

        ///<summary>(FFFA,FFFA) VR=SQ VM=1 Digital Signatures Sequence</summary>
        public readonly static DicomTagSQ DigitalSignaturesSequence = new DicomTagSQ(0xFFFA, 0xFFFA);

        ///<summary>(FFFC,FFFC) VR=OB VM=1 Data Set Trailing Padding</summary>
        public readonly static DicomTagOB DataSetTrailingPadding = new DicomTagOB(0xFFFC, 0xFFFC);

        ///<summary>(FFFE,E000) VR=NONE VM=1 Item</summary>
        public readonly static DicomTagNONE Item = new DicomTagNONE(0xFFFE, 0xE000);

        ///<summary>(FFFE,E00D) VR=NONE VM=1 Item Delimitation Item</summary>
        public readonly static DicomTagNONE ItemDelimitationItem = new DicomTagNONE(0xFFFE, 0xE00D);

        ///<summary>(FFFE,E0DD) VR=NONE VM=1 Sequence Delimitation Item</summary>
        public readonly static DicomTagNONE SequenceDelimitationItem = new DicomTagNONE(0xFFFE, 0xE0DD);

        ///<summary>(0006,0001) VR=SQ VM=1 Current Frame Functional Groups Sequence</summary>
        public readonly static DicomTagSQ CurrentFrameFunctionalGroupsSequence = new DicomTagSQ(0x0006, 0x0001);

    }
}
