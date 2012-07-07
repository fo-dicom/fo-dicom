namespace Dicom {
	public partial class DicomTag {
		///<summary>(0000,0000) VR=UL VM=1 Command Group Length</summary>
		public readonly static DicomTag CommandGroupLength = new DicomTag(0x0000, 0x0000);

		///<summary>(0000,0001) VR=UL VM=1 Command Length to End (RETIRED)</summary>
		public readonly static DicomTag CommandLengthToEndRETIRED = new DicomTag(0x0000, 0x0001);

		///<summary>(0000,0002) VR=UI VM=1 Affected SOP Class UID</summary>
		public readonly static DicomTag AffectedSOPClassUID = new DicomTag(0x0000, 0x0002);

		///<summary>(0000,0003) VR=UI VM=1 Requested SOP Class UID</summary>
		public readonly static DicomTag RequestedSOPClassUID = new DicomTag(0x0000, 0x0003);

		///<summary>(0000,0010) VR=SH VM=1 Command Recognition Code (RETIRED)</summary>
		public readonly static DicomTag CommandRecognitionCodeRETIRED = new DicomTag(0x0000, 0x0010);

		///<summary>(0000,0100) VR=US VM=1 Command Field</summary>
		public readonly static DicomTag CommandField = new DicomTag(0x0000, 0x0100);

		///<summary>(0000,0110) VR=US VM=1 Message ID</summary>
		public readonly static DicomTag MessageID = new DicomTag(0x0000, 0x0110);

		///<summary>(0000,0120) VR=US VM=1 Message ID Being Responded To</summary>
		public readonly static DicomTag MessageIDBeingRespondedTo = new DicomTag(0x0000, 0x0120);

		///<summary>(0000,0200) VR=AE VM=1 Initiator (RETIRED)</summary>
		public readonly static DicomTag InitiatorRETIRED = new DicomTag(0x0000, 0x0200);

		///<summary>(0000,0300) VR=AE VM=1 Receiver (RETIRED)</summary>
		public readonly static DicomTag ReceiverRETIRED = new DicomTag(0x0000, 0x0300);

		///<summary>(0000,0400) VR=AE VM=1 Find Location (RETIRED)</summary>
		public readonly static DicomTag FindLocationRETIRED = new DicomTag(0x0000, 0x0400);

		///<summary>(0000,0600) VR=AE VM=1 Move Destination</summary>
		public readonly static DicomTag MoveDestination = new DicomTag(0x0000, 0x0600);

		///<summary>(0000,0700) VR=US VM=1 Priority</summary>
		public readonly static DicomTag Priority = new DicomTag(0x0000, 0x0700);

		///<summary>(0000,0800) VR=US VM=1 Command Data Set Type</summary>
		public readonly static DicomTag CommandDataSetType = new DicomTag(0x0000, 0x0800);

		///<summary>(0000,0850) VR=US VM=1 Number of Matches (RETIRED)</summary>
		public readonly static DicomTag NumberOfMatchesRETIRED = new DicomTag(0x0000, 0x0850);

		///<summary>(0000,0860) VR=US VM=1 Response Sequence Number (RETIRED)</summary>
		public readonly static DicomTag ResponseSequenceNumberRETIRED = new DicomTag(0x0000, 0x0860);

		///<summary>(0000,0900) VR=US VM=1 Status</summary>
		public readonly static DicomTag Status = new DicomTag(0x0000, 0x0900);

		///<summary>(0000,0901) VR=AT VM=1-n Offending Element</summary>
		public readonly static DicomTag OffendingElement = new DicomTag(0x0000, 0x0901);

		///<summary>(0000,0902) VR=LO VM=1 Error Comment</summary>
		public readonly static DicomTag ErrorComment = new DicomTag(0x0000, 0x0902);

		///<summary>(0000,0903) VR=US VM=1 Error ID</summary>
		public readonly static DicomTag ErrorID = new DicomTag(0x0000, 0x0903);

		///<summary>(0000,1000) VR=UI VM=1 Affected SOP Instance UID</summary>
		public readonly static DicomTag AffectedSOPInstanceUID = new DicomTag(0x0000, 0x1000);

		///<summary>(0000,1001) VR=UI VM=1 Requested SOP Instance UID</summary>
		public readonly static DicomTag RequestedSOPInstanceUID = new DicomTag(0x0000, 0x1001);

		///<summary>(0000,1002) VR=US VM=1 Event Type ID</summary>
		public readonly static DicomTag EventTypeID = new DicomTag(0x0000, 0x1002);

		///<summary>(0000,1005) VR=AT VM=1-n Attribute Identifier List</summary>
		public readonly static DicomTag AttributeIdentifierList = new DicomTag(0x0000, 0x1005);

		///<summary>(0000,1008) VR=US VM=1 Action Type ID</summary>
		public readonly static DicomTag ActionTypeID = new DicomTag(0x0000, 0x1008);

		///<summary>(0000,1020) VR=US VM=1 Number of Remaining Sub-operations</summary>
		public readonly static DicomTag NumberOfRemainingSuboperations = new DicomTag(0x0000, 0x1020);

		///<summary>(0000,1021) VR=US VM=1 Number of Completed Sub-operations</summary>
		public readonly static DicomTag NumberOfCompletedSuboperations = new DicomTag(0x0000, 0x1021);

		///<summary>(0000,1022) VR=US VM=1 Number of Failed Sub-operations</summary>
		public readonly static DicomTag NumberOfFailedSuboperations = new DicomTag(0x0000, 0x1022);

		///<summary>(0000,1023) VR=US VM=1 Number of Warning Sub-operations</summary>
		public readonly static DicomTag NumberOfWarningSuboperations = new DicomTag(0x0000, 0x1023);

		///<summary>(0000,1030) VR=AE VM=1 Move Originator Application Entity Title</summary>
		public readonly static DicomTag MoveOriginatorApplicationEntityTitle = new DicomTag(0x0000, 0x1030);

		///<summary>(0000,1031) VR=US VM=1 Move Originator Message ID</summary>
		public readonly static DicomTag MoveOriginatorMessageID = new DicomTag(0x0000, 0x1031);

		///<summary>(0000,4000) VR=LT VM=1 Dialog Receiver (RETIRED)</summary>
		public readonly static DicomTag DialogReceiverRETIRED = new DicomTag(0x0000, 0x4000);

		///<summary>(0000,4010) VR=LT VM=1 Terminal Type (RETIRED)</summary>
		public readonly static DicomTag TerminalTypeRETIRED = new DicomTag(0x0000, 0x4010);

		///<summary>(0000,5010) VR=SH VM=1 Message Set ID (RETIRED)</summary>
		public readonly static DicomTag MessageSetIDRETIRED = new DicomTag(0x0000, 0x5010);

		///<summary>(0000,5020) VR=SH VM=1 End Message ID (RETIRED)</summary>
		public readonly static DicomTag EndMessageIDRETIRED = new DicomTag(0x0000, 0x5020);

		///<summary>(0000,5110) VR=LT VM=1 Display Format (RETIRED)</summary>
		public readonly static DicomTag DisplayFormatRETIRED = new DicomTag(0x0000, 0x5110);

		///<summary>(0000,5120) VR=LT VM=1 Page Position ID (RETIRED)</summary>
		public readonly static DicomTag PagePositionIDRETIRED = new DicomTag(0x0000, 0x5120);

		///<summary>(0000,5130) VR=CS VM=1 Text Format ID (RETIRED)</summary>
		public readonly static DicomTag TextFormatIDRETIRED = new DicomTag(0x0000, 0x5130);

		///<summary>(0000,5140) VR=CS VM=1 Normal/Reverse (RETIRED)</summary>
		public readonly static DicomTag NormalReverseRETIRED = new DicomTag(0x0000, 0x5140);

		///<summary>(0000,5150) VR=CS VM=1 Add Gray Scale (RETIRED)</summary>
		public readonly static DicomTag AddGrayScaleRETIRED = new DicomTag(0x0000, 0x5150);

		///<summary>(0000,5160) VR=CS VM=1 Borders (RETIRED)</summary>
		public readonly static DicomTag BordersRETIRED = new DicomTag(0x0000, 0x5160);

		///<summary>(0000,5170) VR=IS VM=1 Copies (RETIRED)</summary>
		public readonly static DicomTag CopiesRETIRED = new DicomTag(0x0000, 0x5170);

		///<summary>(0000,5180) VR=CS VM=1 Command Magnification Type (RETIRED)</summary>
		public readonly static DicomTag CommandMagnificationTypeRETIRED = new DicomTag(0x0000, 0x5180);

		///<summary>(0000,5190) VR=CS VM=1 Erase (RETIRED)</summary>
		public readonly static DicomTag EraseRETIRED = new DicomTag(0x0000, 0x5190);

		///<summary>(0000,51a0) VR=CS VM=1 Print (RETIRED)</summary>
		public readonly static DicomTag PrintRETIRED = new DicomTag(0x0000, 0x51a0);

		///<summary>(0000,51b0) VR=US VM=1-n Overlays (RETIRED)</summary>
		public readonly static DicomTag OverlaysRETIRED = new DicomTag(0x0000, 0x51b0);

		///<summary>(0002,0000) VR=UL VM=1 File Meta Information Group Length</summary>
		public readonly static DicomTag FileMetaInformationGroupLength = new DicomTag(0x0002, 0x0000);

		///<summary>(0002,0001) VR=OB VM=1 File Meta Information Version</summary>
		public readonly static DicomTag FileMetaInformationVersion = new DicomTag(0x0002, 0x0001);

		///<summary>(0002,0002) VR=UI VM=1 Media Storage SOP Class UID</summary>
		public readonly static DicomTag MediaStorageSOPClassUID = new DicomTag(0x0002, 0x0002);

		///<summary>(0002,0003) VR=UI VM=1 Media Storage SOP Instance UID</summary>
		public readonly static DicomTag MediaStorageSOPInstanceUID = new DicomTag(0x0002, 0x0003);

		///<summary>(0002,0010) VR=UI VM=1 Transfer Syntax UID</summary>
		public readonly static DicomTag TransferSyntaxUID = new DicomTag(0x0002, 0x0010);

		///<summary>(0002,0012) VR=UI VM=1 Implementation Class UID</summary>
		public readonly static DicomTag ImplementationClassUID = new DicomTag(0x0002, 0x0012);

		///<summary>(0002,0013) VR=SH VM=1 Implementation Version Name</summary>
		public readonly static DicomTag ImplementationVersionName = new DicomTag(0x0002, 0x0013);

		///<summary>(0002,0016) VR=AE VM=1 Source Application Entity Title</summary>
		public readonly static DicomTag SourceApplicationEntityTitle = new DicomTag(0x0002, 0x0016);

		///<summary>(0002,0100) VR=UI VM=1 Private Information Creator UID</summary>
		public readonly static DicomTag PrivateInformationCreatorUID = new DicomTag(0x0002, 0x0100);

		///<summary>(0002,0102) VR=OB VM=1 Private Information</summary>
		public readonly static DicomTag PrivateInformation = new DicomTag(0x0002, 0x0102);

		///<summary>(0004,1130) VR=CS VM=1 File-set ID</summary>
		public readonly static DicomTag FileSetID = new DicomTag(0x0004, 0x1130);

		///<summary>(0004,1141) VR=CS VM=1-8 File-set Descriptor File ID</summary>
		public readonly static DicomTag FileSetDescriptorFileID = new DicomTag(0x0004, 0x1141);

		///<summary>(0004,1142) VR=CS VM=1 Specific Character Set of File-set Descriptor File</summary>
		public readonly static DicomTag SpecificCharacterSetOfFileSetDescriptorFile = new DicomTag(0x0004, 0x1142);

		///<summary>(0004,1200) VR=UL VM=1 Offset of the First Directory Record of the Root Directory Entity</summary>
		public readonly static DicomTag OffsetOfTheFirstDirectoryRecordOfTheRootDirectoryEntity = new DicomTag(0x0004, 0x1200);

		///<summary>(0004,1202) VR=UL VM=1 Offset of the Last Directory Record of the Root Directory Entity</summary>
		public readonly static DicomTag OffsetOfTheLastDirectoryRecordOfTheRootDirectoryEntity = new DicomTag(0x0004, 0x1202);

		///<summary>(0004,1212) VR=US VM=1 File-set Consistency Flag</summary>
		public readonly static DicomTag FileSetConsistencyFlag = new DicomTag(0x0004, 0x1212);

		///<summary>(0004,1220) VR=SQ VM=1 Directory Record Sequence</summary>
		public readonly static DicomTag DirectoryRecordSequence = new DicomTag(0x0004, 0x1220);

		///<summary>(0004,1400) VR=UL VM=1 Offset of the Next Directory Record</summary>
		public readonly static DicomTag OffsetOfTheNextDirectoryRecord = new DicomTag(0x0004, 0x1400);

		///<summary>(0004,1410) VR=US VM=1 Record In-use Flag</summary>
		public readonly static DicomTag RecordInUseFlag = new DicomTag(0x0004, 0x1410);

		///<summary>(0004,1420) VR=UL VM=1 Offset of Referenced Lower-Level Directory Entity</summary>
		public readonly static DicomTag OffsetOfReferencedLowerLevelDirectoryEntity = new DicomTag(0x0004, 0x1420);

		///<summary>(0004,1430) VR=CS VM=1 Directory Record Type</summary>
		public readonly static DicomTag DirectoryRecordType = new DicomTag(0x0004, 0x1430);

		///<summary>(0004,1432) VR=UI VM=1 Private Record UID</summary>
		public readonly static DicomTag PrivateRecordUID = new DicomTag(0x0004, 0x1432);

		///<summary>(0004,1500) VR=CS VM=1-8 Referenced File ID</summary>
		public readonly static DicomTag ReferencedFileID = new DicomTag(0x0004, 0x1500);

		///<summary>(0004,1504) VR=UL VM=1 MRDR Directory Record Offset (RETIRED)</summary>
		public readonly static DicomTag MRDRDirectoryRecordOffsetRETIRED = new DicomTag(0x0004, 0x1504);

		///<summary>(0004,1510) VR=UI VM=1 Referenced SOP Class UID in File</summary>
		public readonly static DicomTag ReferencedSOPClassUIDInFile = new DicomTag(0x0004, 0x1510);

		///<summary>(0004,1511) VR=UI VM=1 Referenced SOP Instance UID in File</summary>
		public readonly static DicomTag ReferencedSOPInstanceUIDInFile = new DicomTag(0x0004, 0x1511);

		///<summary>(0004,1512) VR=UI VM=1 Referenced Transfer Syntax UID in File</summary>
		public readonly static DicomTag ReferencedTransferSyntaxUIDInFile = new DicomTag(0x0004, 0x1512);

		///<summary>(0004,151a) VR=UI VM=1-n Referenced Related General SOP Class UID in File</summary>
		public readonly static DicomTag ReferencedRelatedGeneralSOPClassUIDInFile = new DicomTag(0x0004, 0x151a);

		///<summary>(0004,1600) VR=UL VM=1 Number of References (RETIRED)</summary>
		public readonly static DicomTag NumberOfReferencesRETIRED = new DicomTag(0x0004, 0x1600);

		///<summary>(0008,0001) VR=UL VM=1 Length to End (RETIRED)</summary>
		public readonly static DicomTag LengthToEndRETIRED = new DicomTag(0x0008, 0x0001);

		///<summary>(0008,0005) VR=CS VM=1-n Specific Character Set</summary>
		public readonly static DicomTag SpecificCharacterSet = new DicomTag(0x0008, 0x0005);

		///<summary>(0008,0006) VR=SQ VM=1 Language Code Sequence</summary>
		public readonly static DicomTag LanguageCodeSequence = new DicomTag(0x0008, 0x0006);

		///<summary>(0008,0008) VR=CS VM=2-n Image Type</summary>
		public readonly static DicomTag ImageType = new DicomTag(0x0008, 0x0008);

		///<summary>(0008,0010) VR=SH VM=1 Recognition Code (RETIRED)</summary>
		public readonly static DicomTag RecognitionCodeRETIRED = new DicomTag(0x0008, 0x0010);

		///<summary>(0008,0012) VR=DA VM=1 Instance Creation Date</summary>
		public readonly static DicomTag InstanceCreationDate = new DicomTag(0x0008, 0x0012);

		///<summary>(0008,0013) VR=TM VM=1 Instance Creation Time</summary>
		public readonly static DicomTag InstanceCreationTime = new DicomTag(0x0008, 0x0013);

		///<summary>(0008,0014) VR=UI VM=1 Instance Creator UID</summary>
		public readonly static DicomTag InstanceCreatorUID = new DicomTag(0x0008, 0x0014);

		///<summary>(0008,0016) VR=UI VM=1 SOP Class UID</summary>
		public readonly static DicomTag SOPClassUID = new DicomTag(0x0008, 0x0016);

		///<summary>(0008,0018) VR=UI VM=1 SOP Instance UID</summary>
		public readonly static DicomTag SOPInstanceUID = new DicomTag(0x0008, 0x0018);

		///<summary>(0008,001a) VR=UI VM=1-n Related General SOP Class UID</summary>
		public readonly static DicomTag RelatedGeneralSOPClassUID = new DicomTag(0x0008, 0x001a);

		///<summary>(0008,001b) VR=UI VM=1 Original Specialized SOP Class UID</summary>
		public readonly static DicomTag OriginalSpecializedSOPClassUID = new DicomTag(0x0008, 0x001b);

		///<summary>(0008,0020) VR=DA VM=1 Study Date</summary>
		public readonly static DicomTag StudyDate = new DicomTag(0x0008, 0x0020);

		///<summary>(0008,0021) VR=DA VM=1 Series Date</summary>
		public readonly static DicomTag SeriesDate = new DicomTag(0x0008, 0x0021);

		///<summary>(0008,0022) VR=DA VM=1 Acquisition Date</summary>
		public readonly static DicomTag AcquisitionDate = new DicomTag(0x0008, 0x0022);

		///<summary>(0008,0023) VR=DA VM=1 Content Date</summary>
		public readonly static DicomTag ContentDate = new DicomTag(0x0008, 0x0023);

		///<summary>(0008,0024) VR=DA VM=1 Overlay Date (RETIRED)</summary>
		public readonly static DicomTag OverlayDateRETIRED = new DicomTag(0x0008, 0x0024);

		///<summary>(0008,0025) VR=DA VM=1 Curve Date (RETIRED)</summary>
		public readonly static DicomTag CurveDateRETIRED = new DicomTag(0x0008, 0x0025);

		///<summary>(0008,002a) VR=DT VM=1 Acquisition DateTime</summary>
		public readonly static DicomTag AcquisitionDateTime = new DicomTag(0x0008, 0x002a);

		///<summary>(0008,0030) VR=TM VM=1 Study Time</summary>
		public readonly static DicomTag StudyTime = new DicomTag(0x0008, 0x0030);

		///<summary>(0008,0031) VR=TM VM=1 Series Time</summary>
		public readonly static DicomTag SeriesTime = new DicomTag(0x0008, 0x0031);

		///<summary>(0008,0032) VR=TM VM=1 Acquisition Time</summary>
		public readonly static DicomTag AcquisitionTime = new DicomTag(0x0008, 0x0032);

		///<summary>(0008,0033) VR=TM VM=1 Content Time</summary>
		public readonly static DicomTag ContentTime = new DicomTag(0x0008, 0x0033);

		///<summary>(0008,0034) VR=TM VM=1 Overlay Time (RETIRED)</summary>
		public readonly static DicomTag OverlayTimeRETIRED = new DicomTag(0x0008, 0x0034);

		///<summary>(0008,0035) VR=TM VM=1 Curve Time (RETIRED)</summary>
		public readonly static DicomTag CurveTimeRETIRED = new DicomTag(0x0008, 0x0035);

		///<summary>(0008,0040) VR=US VM=1 Data Set Type (RETIRED)</summary>
		public readonly static DicomTag DataSetTypeRETIRED = new DicomTag(0x0008, 0x0040);

		///<summary>(0008,0041) VR=LO VM=1 Data Set Subtype (RETIRED)</summary>
		public readonly static DicomTag DataSetSubtypeRETIRED = new DicomTag(0x0008, 0x0041);

		///<summary>(0008,0042) VR=CS VM=1 Nuclear Medicine Series Type (RETIRED)</summary>
		public readonly static DicomTag NuclearMedicineSeriesTypeRETIRED = new DicomTag(0x0008, 0x0042);

		///<summary>(0008,0050) VR=SH VM=1 Accession Number</summary>
		public readonly static DicomTag AccessionNumber = new DicomTag(0x0008, 0x0050);

		///<summary>(0008,0051) VR=SQ VM=1 Issuer of Accession Number Sequence</summary>
		public readonly static DicomTag IssuerOfAccessionNumberSequence = new DicomTag(0x0008, 0x0051);

		///<summary>(0008,0052) VR=CS VM=1 Query/Retrieve Level</summary>
		public readonly static DicomTag QueryRetrieveLevel = new DicomTag(0x0008, 0x0052);

		///<summary>(0008,0054) VR=AE VM=1-n Retrieve AE Title</summary>
		public readonly static DicomTag RetrieveAETitle = new DicomTag(0x0008, 0x0054);

		///<summary>(0008,0056) VR=CS VM=1 Instance Availability</summary>
		public readonly static DicomTag InstanceAvailability = new DicomTag(0x0008, 0x0056);

		///<summary>(0008,0058) VR=UI VM=1-n Failed SOP Instance UID List</summary>
		public readonly static DicomTag FailedSOPInstanceUIDList = new DicomTag(0x0008, 0x0058);

		///<summary>(0008,0060) VR=CS VM=1 Modality</summary>
		public readonly static DicomTag Modality = new DicomTag(0x0008, 0x0060);

		///<summary>(0008,0061) VR=CS VM=1-n Modalities in Study</summary>
		public readonly static DicomTag ModalitiesInStudy = new DicomTag(0x0008, 0x0061);

		///<summary>(0008,0062) VR=UI VM=1-n SOP Classes in Study</summary>
		public readonly static DicomTag SOPClassesInStudy = new DicomTag(0x0008, 0x0062);

		///<summary>(0008,0064) VR=CS VM=1 Conversion Type</summary>
		public readonly static DicomTag ConversionType = new DicomTag(0x0008, 0x0064);

		///<summary>(0008,0068) VR=CS VM=1 Presentation Intent Type</summary>
		public readonly static DicomTag PresentationIntentType = new DicomTag(0x0008, 0x0068);

		///<summary>(0008,0070) VR=LO VM=1 Manufacturer</summary>
		public readonly static DicomTag Manufacturer = new DicomTag(0x0008, 0x0070);

		///<summary>(0008,0080) VR=LO VM=1 Institution Name</summary>
		public readonly static DicomTag InstitutionName = new DicomTag(0x0008, 0x0080);

		///<summary>(0008,0081) VR=ST VM=1 Institution Address</summary>
		public readonly static DicomTag InstitutionAddress = new DicomTag(0x0008, 0x0081);

		///<summary>(0008,0082) VR=SQ VM=1 Institution Code Sequence</summary>
		public readonly static DicomTag InstitutionCodeSequence = new DicomTag(0x0008, 0x0082);

		///<summary>(0008,0090) VR=PN VM=1 Referring Physician’s Name</summary>
		public readonly static DicomTag ReferringPhysicianName = new DicomTag(0x0008, 0x0090);

		///<summary>(0008,0092) VR=ST VM=1 Referring Physician’s Address</summary>
		public readonly static DicomTag ReferringPhysicianAddress = new DicomTag(0x0008, 0x0092);

		///<summary>(0008,0094) VR=SH VM=1-n Referring Physician’s Telephone Numbers</summary>
		public readonly static DicomTag ReferringPhysicianTelephoneNumbers = new DicomTag(0x0008, 0x0094);

		///<summary>(0008,0096) VR=SQ VM=1 Referring Physician Identification Sequence</summary>
		public readonly static DicomTag ReferringPhysicianIdentificationSequence = new DicomTag(0x0008, 0x0096);

		///<summary>(0008,0100) VR=SH VM=1 Code Value</summary>
		public readonly static DicomTag CodeValue = new DicomTag(0x0008, 0x0100);

		///<summary>(0008,0102) VR=SH VM=1 Coding Scheme Designator</summary>
		public readonly static DicomTag CodingSchemeDesignator = new DicomTag(0x0008, 0x0102);

		///<summary>(0008,0103) VR=SH VM=1 Coding Scheme Version</summary>
		public readonly static DicomTag CodingSchemeVersion = new DicomTag(0x0008, 0x0103);

		///<summary>(0008,0104) VR=LO VM=1 Code Meaning</summary>
		public readonly static DicomTag CodeMeaning = new DicomTag(0x0008, 0x0104);

		///<summary>(0008,0105) VR=CS VM=1 Mapping Resource</summary>
		public readonly static DicomTag MappingResource = new DicomTag(0x0008, 0x0105);

		///<summary>(0008,0106) VR=DT VM=1 Context Group Version</summary>
		public readonly static DicomTag ContextGroupVersion = new DicomTag(0x0008, 0x0106);

		///<summary>(0008,0107) VR=DT VM=1 Context Group Local Version</summary>
		public readonly static DicomTag ContextGroupLocalVersion = new DicomTag(0x0008, 0x0107);

		///<summary>(0008,010b) VR=CS VM=1 Context Group Extension Flag</summary>
		public readonly static DicomTag ContextGroupExtensionFlag = new DicomTag(0x0008, 0x010b);

		///<summary>(0008,010c) VR=UI VM=1 Coding Scheme UID</summary>
		public readonly static DicomTag CodingSchemeUID = new DicomTag(0x0008, 0x010c);

		///<summary>(0008,010d) VR=UI VM=1 Context Group Extension Creator UID</summary>
		public readonly static DicomTag ContextGroupExtensionCreatorUID = new DicomTag(0x0008, 0x010d);

		///<summary>(0008,010f) VR=CS VM=1 Context Identifier</summary>
		public readonly static DicomTag ContextIdentifier = new DicomTag(0x0008, 0x010f);

		///<summary>(0008,0110) VR=SQ VM=1 Coding Scheme Identification Sequence</summary>
		public readonly static DicomTag CodingSchemeIdentificationSequence = new DicomTag(0x0008, 0x0110);

		///<summary>(0008,0112) VR=LO VM=1 Coding Scheme Registry</summary>
		public readonly static DicomTag CodingSchemeRegistry = new DicomTag(0x0008, 0x0112);

		///<summary>(0008,0114) VR=ST VM=1 Coding Scheme External ID</summary>
		public readonly static DicomTag CodingSchemeExternalID = new DicomTag(0x0008, 0x0114);

		///<summary>(0008,0115) VR=ST VM=1 Coding Scheme Name</summary>
		public readonly static DicomTag CodingSchemeName = new DicomTag(0x0008, 0x0115);

		///<summary>(0008,0116) VR=ST VM=1 Coding Scheme Responsible Organization</summary>
		public readonly static DicomTag CodingSchemeResponsibleOrganization = new DicomTag(0x0008, 0x0116);

		///<summary>(0008,0117) VR=UI VM=1 Context UID</summary>
		public readonly static DicomTag ContextUID = new DicomTag(0x0008, 0x0117);

		///<summary>(0008,0201) VR=SH VM=1 Timezone Offset From UTC</summary>
		public readonly static DicomTag TimezoneOffsetFromUTC = new DicomTag(0x0008, 0x0201);

		///<summary>(0008,1000) VR=AE VM=1 Network ID (RETIRED)</summary>
		public readonly static DicomTag NetworkIDRETIRED = new DicomTag(0x0008, 0x1000);

		///<summary>(0008,1010) VR=SH VM=1 Station Name</summary>
		public readonly static DicomTag StationName = new DicomTag(0x0008, 0x1010);

		///<summary>(0008,1030) VR=LO VM=1 Study Description</summary>
		public readonly static DicomTag StudyDescription = new DicomTag(0x0008, 0x1030);

		///<summary>(0008,1032) VR=SQ VM=1 Procedure Code Sequence</summary>
		public readonly static DicomTag ProcedureCodeSequence = new DicomTag(0x0008, 0x1032);

		///<summary>(0008,103e) VR=LO VM=1 Series Description</summary>
		public readonly static DicomTag SeriesDescription = new DicomTag(0x0008, 0x103e);

		///<summary>(0008,103f) VR=SQ VM=1 Series Description Code Sequence</summary>
		public readonly static DicomTag SeriesDescriptionCodeSequence = new DicomTag(0x0008, 0x103f);

		///<summary>(0008,1040) VR=LO VM=1 Institutional Department Name</summary>
		public readonly static DicomTag InstitutionalDepartmentName = new DicomTag(0x0008, 0x1040);

		///<summary>(0008,1048) VR=PN VM=1-n Physician(s) of Record</summary>
		public readonly static DicomTag PhysiciansOfRecord = new DicomTag(0x0008, 0x1048);

		///<summary>(0008,1049) VR=SQ VM=1 Physician(s) of Record Identification Sequence</summary>
		public readonly static DicomTag PhysiciansOfRecordIdentificationSequence = new DicomTag(0x0008, 0x1049);

		///<summary>(0008,1050) VR=PN VM=1-n Performing Physician’s Name</summary>
		public readonly static DicomTag PerformingPhysicianName = new DicomTag(0x0008, 0x1050);

		///<summary>(0008,1052) VR=SQ VM=1 Performing Physician Identification Sequence</summary>
		public readonly static DicomTag PerformingPhysicianIdentificationSequence = new DicomTag(0x0008, 0x1052);

		///<summary>(0008,1060) VR=PN VM=1-n Name of Physician(s) Reading Study</summary>
		public readonly static DicomTag NameOfPhysiciansReadingStudy = new DicomTag(0x0008, 0x1060);

		///<summary>(0008,1062) VR=SQ VM=1 Physician(s) Reading Study Identification Sequence</summary>
		public readonly static DicomTag PhysiciansReadingStudyIdentificationSequence = new DicomTag(0x0008, 0x1062);

		///<summary>(0008,1070) VR=PN VM=1-n Operators’ Name</summary>
		public readonly static DicomTag OperatorsName = new DicomTag(0x0008, 0x1070);

		///<summary>(0008,1072) VR=SQ VM=1 Operator Identification Sequence</summary>
		public readonly static DicomTag OperatorIdentificationSequence = new DicomTag(0x0008, 0x1072);

		///<summary>(0008,1080) VR=LO VM=1-n Admitting Diagnoses Description</summary>
		public readonly static DicomTag AdmittingDiagnosesDescription = new DicomTag(0x0008, 0x1080);

		///<summary>(0008,1084) VR=SQ VM=1 Admitting Diagnoses Code Sequence</summary>
		public readonly static DicomTag AdmittingDiagnosesCodeSequence = new DicomTag(0x0008, 0x1084);

		///<summary>(0008,1090) VR=LO VM=1 Manufacturer’s Model Name</summary>
		public readonly static DicomTag ManufacturerModelName = new DicomTag(0x0008, 0x1090);

		///<summary>(0008,1100) VR=SQ VM=1 Referenced Results Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedResultsSequenceRETIRED = new DicomTag(0x0008, 0x1100);

		///<summary>(0008,1110) VR=SQ VM=1 Referenced Study Sequence</summary>
		public readonly static DicomTag ReferencedStudySequence = new DicomTag(0x0008, 0x1110);

		///<summary>(0008,1111) VR=SQ VM=1 Referenced Performed Procedure Step Sequence</summary>
		public readonly static DicomTag ReferencedPerformedProcedureStepSequence = new DicomTag(0x0008, 0x1111);

		///<summary>(0008,1115) VR=SQ VM=1 Referenced Series Sequence</summary>
		public readonly static DicomTag ReferencedSeriesSequence = new DicomTag(0x0008, 0x1115);

		///<summary>(0008,1120) VR=SQ VM=1 Referenced Patient Sequence</summary>
		public readonly static DicomTag ReferencedPatientSequence = new DicomTag(0x0008, 0x1120);

		///<summary>(0008,1125) VR=SQ VM=1 Referenced Visit Sequence</summary>
		public readonly static DicomTag ReferencedVisitSequence = new DicomTag(0x0008, 0x1125);

		///<summary>(0008,1130) VR=SQ VM=1 Referenced Overlay Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedOverlaySequenceRETIRED = new DicomTag(0x0008, 0x1130);

		///<summary>(0008,1134) VR=SQ VM=1 Referenced Stereometric Instance Sequence</summary>
		public readonly static DicomTag ReferencedStereometricInstanceSequence = new DicomTag(0x0008, 0x1134);

		///<summary>(0008,113a) VR=SQ VM=1 Referenced Waveform Sequence</summary>
		public readonly static DicomTag ReferencedWaveformSequence = new DicomTag(0x0008, 0x113a);

		///<summary>(0008,1140) VR=SQ VM=1 Referenced Image Sequence</summary>
		public readonly static DicomTag ReferencedImageSequence = new DicomTag(0x0008, 0x1140);

		///<summary>(0008,1145) VR=SQ VM=1 Referenced Curve Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedCurveSequenceRETIRED = new DicomTag(0x0008, 0x1145);

		///<summary>(0008,114a) VR=SQ VM=1 Referenced Instance Sequence</summary>
		public readonly static DicomTag ReferencedInstanceSequence = new DicomTag(0x0008, 0x114a);

		///<summary>(0008,114b) VR=SQ VM=1 Referenced Real World Value Mapping Instance Sequence</summary>
		public readonly static DicomTag ReferencedRealWorldValueMappingInstanceSequence = new DicomTag(0x0008, 0x114b);

		///<summary>(0008,1150) VR=UI VM=1 Referenced SOP Class UID</summary>
		public readonly static DicomTag ReferencedSOPClassUID = new DicomTag(0x0008, 0x1150);

		///<summary>(0008,1155) VR=UI VM=1 Referenced SOP Instance UID</summary>
		public readonly static DicomTag ReferencedSOPInstanceUID = new DicomTag(0x0008, 0x1155);

		///<summary>(0008,115a) VR=UI VM=1-n SOP Classes Supported</summary>
		public readonly static DicomTag SOPClassesSupported = new DicomTag(0x0008, 0x115a);

		///<summary>(0008,1160) VR=IS VM=1-n Referenced Frame Number</summary>
		public readonly static DicomTag ReferencedFrameNumber = new DicomTag(0x0008, 0x1160);

		///<summary>(0008,1161) VR=UL VM=1-n Simple Frame List</summary>
		public readonly static DicomTag SimpleFrameList = new DicomTag(0x0008, 0x1161);

		///<summary>(0008,1162) VR=UL VM=3-3n Calculated Frame List</summary>
		public readonly static DicomTag CalculatedFrameList = new DicomTag(0x0008, 0x1162);

		///<summary>(0008,1163) VR=FD VM=2 Time Range</summary>
		public readonly static DicomTag TimeRange = new DicomTag(0x0008, 0x1163);

		///<summary>(0008,1164) VR=SQ VM=1 Frame Extraction Sequence</summary>
		public readonly static DicomTag FrameExtractionSequence = new DicomTag(0x0008, 0x1164);

		///<summary>(0008,1167) VR=UI VM=1 Multi-Frame Source SOP Instance UID</summary>
		public readonly static DicomTag MultiFrameSourceSOPInstanceUID = new DicomTag(0x0008, 0x1167);

		///<summary>(0008,1195) VR=UI VM=1 Transaction UID</summary>
		public readonly static DicomTag TransactionUID = new DicomTag(0x0008, 0x1195);

		///<summary>(0008,1197) VR=US VM=1 Failure Reason</summary>
		public readonly static DicomTag FailureReason = new DicomTag(0x0008, 0x1197);

		///<summary>(0008,1198) VR=SQ VM=1 Failed SOP Sequence</summary>
		public readonly static DicomTag FailedSOPSequence = new DicomTag(0x0008, 0x1198);

		///<summary>(0008,1199) VR=SQ VM=1 Referenced SOP Sequence</summary>
		public readonly static DicomTag ReferencedSOPSequence = new DicomTag(0x0008, 0x1199);

		///<summary>(0008,1200) VR=SQ VM=1 Studies Containing Other Referenced Instances Sequence</summary>
		public readonly static DicomTag StudiesContainingOtherReferencedInstancesSequence = new DicomTag(0x0008, 0x1200);

		///<summary>(0008,1250) VR=SQ VM=1 Related Series Sequence</summary>
		public readonly static DicomTag RelatedSeriesSequence = new DicomTag(0x0008, 0x1250);

		///<summary>(0008,2110) VR=CS VM=1 Lossy Image Compression (Retired) (RETIRED)</summary>
		public readonly static DicomTag LossyImageCompressionRETIRED = new DicomTag(0x0008, 0x2110);

		///<summary>(0008,2111) VR=ST VM=1 Derivation Description</summary>
		public readonly static DicomTag DerivationDescription = new DicomTag(0x0008, 0x2111);

		///<summary>(0008,2112) VR=SQ VM=1 Source Image Sequence</summary>
		public readonly static DicomTag SourceImageSequence = new DicomTag(0x0008, 0x2112);

		///<summary>(0008,2120) VR=SH VM=1 Stage Name</summary>
		public readonly static DicomTag StageName = new DicomTag(0x0008, 0x2120);

		///<summary>(0008,2122) VR=IS VM=1 Stage Number</summary>
		public readonly static DicomTag StageNumber = new DicomTag(0x0008, 0x2122);

		///<summary>(0008,2124) VR=IS VM=1 Number of Stages</summary>
		public readonly static DicomTag NumberOfStages = new DicomTag(0x0008, 0x2124);

		///<summary>(0008,2127) VR=SH VM=1 View Name</summary>
		public readonly static DicomTag ViewName = new DicomTag(0x0008, 0x2127);

		///<summary>(0008,2128) VR=IS VM=1 View Number</summary>
		public readonly static DicomTag ViewNumber = new DicomTag(0x0008, 0x2128);

		///<summary>(0008,2129) VR=IS VM=1 Number of Event Timers</summary>
		public readonly static DicomTag NumberOfEventTimers = new DicomTag(0x0008, 0x2129);

		///<summary>(0008,212a) VR=IS VM=1 Number of Views in Stage</summary>
		public readonly static DicomTag NumberOfViewsInStage = new DicomTag(0x0008, 0x212a);

		///<summary>(0008,2130) VR=DS VM=1-n Event Elapsed Time(s)</summary>
		public readonly static DicomTag EventElapsedTimes = new DicomTag(0x0008, 0x2130);

		///<summary>(0008,2132) VR=LO VM=1-n Event Timer Name(s)</summary>
		public readonly static DicomTag EventTimerNames = new DicomTag(0x0008, 0x2132);

		///<summary>(0008,2133) VR=SQ VM=1 Event Timer Sequence</summary>
		public readonly static DicomTag EventTimerSequence = new DicomTag(0x0008, 0x2133);

		///<summary>(0008,2134) VR=FD VM=1 Event Time Offset</summary>
		public readonly static DicomTag EventTimeOffset = new DicomTag(0x0008, 0x2134);

		///<summary>(0008,2135) VR=SQ VM=1 Event Code Sequence</summary>
		public readonly static DicomTag EventCodeSequence = new DicomTag(0x0008, 0x2135);

		///<summary>(0008,2142) VR=IS VM=1 Start Trim</summary>
		public readonly static DicomTag StartTrim = new DicomTag(0x0008, 0x2142);

		///<summary>(0008,2143) VR=IS VM=1 Stop Trim</summary>
		public readonly static DicomTag StopTrim = new DicomTag(0x0008, 0x2143);

		///<summary>(0008,2144) VR=IS VM=1 Recommended Display Frame Rate</summary>
		public readonly static DicomTag RecommendedDisplayFrameRate = new DicomTag(0x0008, 0x2144);

		///<summary>(0008,2200) VR=CS VM=1 Transducer Position (RETIRED)</summary>
		public readonly static DicomTag TransducerPositionRETIRED = new DicomTag(0x0008, 0x2200);

		///<summary>(0008,2204) VR=CS VM=1 Transducer Orientation (RETIRED)</summary>
		public readonly static DicomTag TransducerOrientationRETIRED = new DicomTag(0x0008, 0x2204);

		///<summary>(0008,2208) VR=CS VM=1 Anatomic Structure (RETIRED)</summary>
		public readonly static DicomTag AnatomicStructureRETIRED = new DicomTag(0x0008, 0x2208);

		///<summary>(0008,2218) VR=SQ VM=1 Anatomic Region Sequence</summary>
		public readonly static DicomTag AnatomicRegionSequence = new DicomTag(0x0008, 0x2218);

		///<summary>(0008,2220) VR=SQ VM=1 Anatomic Region Modifier Sequence</summary>
		public readonly static DicomTag AnatomicRegionModifierSequence = new DicomTag(0x0008, 0x2220);

		///<summary>(0008,2228) VR=SQ VM=1 Primary Anatomic Structure Sequence</summary>
		public readonly static DicomTag PrimaryAnatomicStructureSequence = new DicomTag(0x0008, 0x2228);

		///<summary>(0008,2229) VR=SQ VM=1 Anatomic Structure, Space or Region Sequence</summary>
		public readonly static DicomTag AnatomicStructureSpaceOrRegionSequence = new DicomTag(0x0008, 0x2229);

		///<summary>(0008,2230) VR=SQ VM=1 Primary Anatomic Structure Modifier Sequence</summary>
		public readonly static DicomTag PrimaryAnatomicStructureModifierSequence = new DicomTag(0x0008, 0x2230);

		///<summary>(0008,2240) VR=SQ VM=1 Transducer Position Sequence (RETIRED)</summary>
		public readonly static DicomTag TransducerPositionSequenceRETIRED = new DicomTag(0x0008, 0x2240);

		///<summary>(0008,2242) VR=SQ VM=1 Transducer Position Modifier Sequence (RETIRED)</summary>
		public readonly static DicomTag TransducerPositionModifierSequenceRETIRED = new DicomTag(0x0008, 0x2242);

		///<summary>(0008,2244) VR=SQ VM=1 Transducer Orientation Sequence (RETIRED)</summary>
		public readonly static DicomTag TransducerOrientationSequenceRETIRED = new DicomTag(0x0008, 0x2244);

		///<summary>(0008,2246) VR=SQ VM=1 Transducer Orientation Modifier Sequence (RETIRED)</summary>
		public readonly static DicomTag TransducerOrientationModifierSequenceRETIRED = new DicomTag(0x0008, 0x2246);

		///<summary>(0008,2251) VR=SQ VM=1 Anatomic Structure Space Or Region Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicStructureSpaceOrRegionCodeSequenceTrialRETIRED = new DicomTag(0x0008, 0x2251);

		///<summary>(0008,2253) VR=SQ VM=1 Anatomic Portal Of Entrance Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicPortalOfEntranceCodeSequenceTrialRETIRED = new DicomTag(0x0008, 0x2253);

		///<summary>(0008,2255) VR=SQ VM=1 Anatomic Approach Direction Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicApproachDirectionCodeSequenceTrialRETIRED = new DicomTag(0x0008, 0x2255);

		///<summary>(0008,2256) VR=ST VM=1 Anatomic Perspective Description (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicPerspectiveDescriptionTrialRETIRED = new DicomTag(0x0008, 0x2256);

		///<summary>(0008,2257) VR=SQ VM=1 Anatomic Perspective Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicPerspectiveCodeSequenceTrialRETIRED = new DicomTag(0x0008, 0x2257);

		///<summary>(0008,2258) VR=ST VM=1 Anatomic Location Of Examining Instrument Description (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicLocationOfExaminingInstrumentDescriptionTrialRETIRED = new DicomTag(0x0008, 0x2258);

		///<summary>(0008,2259) VR=SQ VM=1 Anatomic Location Of Examining Instrument Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicLocationOfExaminingInstrumentCodeSequenceTrialRETIRED = new DicomTag(0x0008, 0x2259);

		///<summary>(0008,225a) VR=SQ VM=1 Anatomic Structure Space Or Region Modifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag AnatomicStructureSpaceOrRegionModifierCodeSequenceTrialRETIRED = new DicomTag(0x0008, 0x225a);

		///<summary>(0008,225c) VR=SQ VM=1 OnAxis Background Anatomic Structure Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag OnAxisBackgroundAnatomicStructureCodeSequenceTrialRETIRED = new DicomTag(0x0008, 0x225c);

		///<summary>(0008,3001) VR=SQ VM=1 Alternate Representation Sequence</summary>
		public readonly static DicomTag AlternateRepresentationSequence = new DicomTag(0x0008, 0x3001);

		///<summary>(0008,3010) VR=UI VM=1 Irradiation Event UID</summary>
		public readonly static DicomTag IrradiationEventUID = new DicomTag(0x0008, 0x3010);

		///<summary>(0008,4000) VR=LT VM=1 Identifying Comments (RETIRED)</summary>
		public readonly static DicomTag IdentifyingCommentsRETIRED = new DicomTag(0x0008, 0x4000);

		///<summary>(0008,9007) VR=CS VM=4 Frame Type</summary>
		public readonly static DicomTag FrameType = new DicomTag(0x0008, 0x9007);

		///<summary>(0008,9092) VR=SQ VM=1 Referenced Image Evidence Sequence</summary>
		public readonly static DicomTag ReferencedImageEvidenceSequence = new DicomTag(0x0008, 0x9092);

		///<summary>(0008,9121) VR=SQ VM=1 Referenced Raw Data Sequence</summary>
		public readonly static DicomTag ReferencedRawDataSequence = new DicomTag(0x0008, 0x9121);

		///<summary>(0008,9123) VR=UI VM=1 Creator-Version UID</summary>
		public readonly static DicomTag CreatorVersionUID = new DicomTag(0x0008, 0x9123);

		///<summary>(0008,9124) VR=SQ VM=1 Derivation Image Sequence</summary>
		public readonly static DicomTag DerivationImageSequence = new DicomTag(0x0008, 0x9124);

		///<summary>(0008,9154) VR=SQ VM=1 Source Image Evidence Sequence</summary>
		public readonly static DicomTag SourceImageEvidenceSequence = new DicomTag(0x0008, 0x9154);

		///<summary>(0008,9205) VR=CS VM=1 Pixel Presentation</summary>
		public readonly static DicomTag PixelPresentation = new DicomTag(0x0008, 0x9205);

		///<summary>(0008,9206) VR=CS VM=1 Volumetric Properties</summary>
		public readonly static DicomTag VolumetricProperties = new DicomTag(0x0008, 0x9206);

		///<summary>(0008,9207) VR=CS VM=1 Volume Based Calculation Technique</summary>
		public readonly static DicomTag VolumeBasedCalculationTechnique = new DicomTag(0x0008, 0x9207);

		///<summary>(0008,9208) VR=CS VM=1 Complex Image Component</summary>
		public readonly static DicomTag ComplexImageComponent = new DicomTag(0x0008, 0x9208);

		///<summary>(0008,9209) VR=CS VM=1 Acquisition Contrast</summary>
		public readonly static DicomTag AcquisitionContrast = new DicomTag(0x0008, 0x9209);

		///<summary>(0008,9215) VR=SQ VM=1 Derivation Code Sequence</summary>
		public readonly static DicomTag DerivationCodeSequence = new DicomTag(0x0008, 0x9215);

		///<summary>(0008,9237) VR=SQ VM=1 Referenced Presentation State Sequence</summary>
		public readonly static DicomTag ReferencedPresentationStateSequence = new DicomTag(0x0008, 0x9237);

		///<summary>(0008,9410) VR=SQ VM=1 Referenced Other Plane Sequence</summary>
		public readonly static DicomTag ReferencedOtherPlaneSequence = new DicomTag(0x0008, 0x9410);

		///<summary>(0008,9458) VR=SQ VM=1 Frame Display Sequence</summary>
		public readonly static DicomTag FrameDisplaySequence = new DicomTag(0x0008, 0x9458);

		///<summary>(0008,9459) VR=FL VM=1 Recommended Display Frame Rate in Float</summary>
		public readonly static DicomTag RecommendedDisplayFrameRateInFloat = new DicomTag(0x0008, 0x9459);

		///<summary>(0008,9460) VR=CS VM=1 Skip Frame Range Flag</summary>
		public readonly static DicomTag SkipFrameRangeFlag = new DicomTag(0x0008, 0x9460);

		///<summary>(0010,0010) VR=PN VM=1 Patient’s Name</summary>
		public readonly static DicomTag PatientName = new DicomTag(0x0010, 0x0010);

		///<summary>(0010,0020) VR=LO VM=1 Patient ID</summary>
		public readonly static DicomTag PatientID = new DicomTag(0x0010, 0x0020);

		///<summary>(0010,0021) VR=LO VM=1 Issuer of Patient ID</summary>
		public readonly static DicomTag IssuerOfPatientID = new DicomTag(0x0010, 0x0021);

		///<summary>(0010,0022) VR=CS VM=1 Type of Patient ID</summary>
		public readonly static DicomTag TypeOfPatientID = new DicomTag(0x0010, 0x0022);

		///<summary>(0010,0024) VR=SQ VM=1 Issuer of Patient ID Qualifiers Sequence</summary>
		public readonly static DicomTag IssuerOfPatientIDQualifiersSequence = new DicomTag(0x0010, 0x0024);

		///<summary>(0010,0030) VR=DA VM=1 Patient’s Birth Date</summary>
		public readonly static DicomTag PatientBirthDate = new DicomTag(0x0010, 0x0030);

		///<summary>(0010,0032) VR=TM VM=1 Patient’s Birth Time</summary>
		public readonly static DicomTag PatientBirthTime = new DicomTag(0x0010, 0x0032);

		///<summary>(0010,0040) VR=CS VM=1 Patient’s Sex</summary>
		public readonly static DicomTag PatientSex = new DicomTag(0x0010, 0x0040);

		///<summary>(0010,0050) VR=SQ VM=1 Patient’s Insurance Plan Code Sequence</summary>
		public readonly static DicomTag PatientInsurancePlanCodeSequence = new DicomTag(0x0010, 0x0050);

		///<summary>(0010,0101) VR=SQ VM=1 Patient’s Primary Language Code Sequence</summary>
		public readonly static DicomTag PatientPrimaryLanguageCodeSequence = new DicomTag(0x0010, 0x0101);

		///<summary>(0010,0102) VR=SQ VM=1 Patient’s Primary Language Modifier Code Sequence</summary>
		public readonly static DicomTag PatientPrimaryLanguageModifierCodeSequence = new DicomTag(0x0010, 0x0102);

		///<summary>(0010,1000) VR=LO VM=1-n Other Patient IDs</summary>
		public readonly static DicomTag OtherPatientIDs = new DicomTag(0x0010, 0x1000);

		///<summary>(0010,1001) VR=PN VM=1-n Other Patient Names</summary>
		public readonly static DicomTag OtherPatientNames = new DicomTag(0x0010, 0x1001);

		///<summary>(0010,1002) VR=SQ VM=1 Other Patient IDs Sequence</summary>
		public readonly static DicomTag OtherPatientIDsSequence = new DicomTag(0x0010, 0x1002);

		///<summary>(0010,1005) VR=PN VM=1 Patient’s Birth Name</summary>
		public readonly static DicomTag PatientBirthName = new DicomTag(0x0010, 0x1005);

		///<summary>(0010,1010) VR=AS VM=1 Patient’s Age</summary>
		public readonly static DicomTag PatientAge = new DicomTag(0x0010, 0x1010);

		///<summary>(0010,1020) VR=DS VM=1 Patient’s Size</summary>
		public readonly static DicomTag PatientSize = new DicomTag(0x0010, 0x1020);

		///<summary>(0010,1021) VR=SQ VM=1 Patient’s Size Code Sequence</summary>
		public readonly static DicomTag PatientSizeCodeSequence = new DicomTag(0x0010, 0x1021);

		///<summary>(0010,1030) VR=DS VM=1 Patient’s Weight</summary>
		public readonly static DicomTag PatientWeight = new DicomTag(0x0010, 0x1030);

		///<summary>(0010,1040) VR=LO VM=1 Patient’s Address</summary>
		public readonly static DicomTag PatientAddress = new DicomTag(0x0010, 0x1040);

		///<summary>(0010,1050) VR=LO VM=1-n Insurance Plan Identification (RETIRED)</summary>
		public readonly static DicomTag InsurancePlanIdentificationRETIRED = new DicomTag(0x0010, 0x1050);

		///<summary>(0010,1060) VR=PN VM=1 Patient’s Mother’s Birth Name</summary>
		public readonly static DicomTag PatientMotherBirthName = new DicomTag(0x0010, 0x1060);

		///<summary>(0010,1080) VR=LO VM=1 Military Rank</summary>
		public readonly static DicomTag MilitaryRank = new DicomTag(0x0010, 0x1080);

		///<summary>(0010,1081) VR=LO VM=1 Branch of Service</summary>
		public readonly static DicomTag BranchOfService = new DicomTag(0x0010, 0x1081);

		///<summary>(0010,1090) VR=LO VM=1 Medical Record Locator</summary>
		public readonly static DicomTag MedicalRecordLocator = new DicomTag(0x0010, 0x1090);

		///<summary>(0010,2000) VR=LO VM=1-n Medical Alerts</summary>
		public readonly static DicomTag MedicalAlerts = new DicomTag(0x0010, 0x2000);

		///<summary>(0010,2110) VR=LO VM=1-n Allergies</summary>
		public readonly static DicomTag Allergies = new DicomTag(0x0010, 0x2110);

		///<summary>(0010,2150) VR=LO VM=1 Country of Residence</summary>
		public readonly static DicomTag CountryOfResidence = new DicomTag(0x0010, 0x2150);

		///<summary>(0010,2152) VR=LO VM=1 Region of Residence</summary>
		public readonly static DicomTag RegionOfResidence = new DicomTag(0x0010, 0x2152);

		///<summary>(0010,2154) VR=SH VM=1-n Patient’s Telephone Numbers</summary>
		public readonly static DicomTag PatientTelephoneNumbers = new DicomTag(0x0010, 0x2154);

		///<summary>(0010,2160) VR=SH VM=1 Ethnic Group</summary>
		public readonly static DicomTag EthnicGroup = new DicomTag(0x0010, 0x2160);

		///<summary>(0010,2180) VR=SH VM=1 Occupation</summary>
		public readonly static DicomTag Occupation = new DicomTag(0x0010, 0x2180);

		///<summary>(0010,21a0) VR=CS VM=1 Smoking Status</summary>
		public readonly static DicomTag SmokingStatus = new DicomTag(0x0010, 0x21a0);

		///<summary>(0010,21b0) VR=LT VM=1 Additional Patient History</summary>
		public readonly static DicomTag AdditionalPatientHistory = new DicomTag(0x0010, 0x21b0);

		///<summary>(0010,21c0) VR=US VM=1 Pregnancy Status</summary>
		public readonly static DicomTag PregnancyStatus = new DicomTag(0x0010, 0x21c0);

		///<summary>(0010,21d0) VR=DA VM=1 Last Menstrual Date</summary>
		public readonly static DicomTag LastMenstrualDate = new DicomTag(0x0010, 0x21d0);

		///<summary>(0010,21f0) VR=LO VM=1 Patient’s Religious Preference</summary>
		public readonly static DicomTag PatientReligiousPreference = new DicomTag(0x0010, 0x21f0);

		///<summary>(0010,2201) VR=LO VM=1 Patient Species Description</summary>
		public readonly static DicomTag PatientSpeciesDescription = new DicomTag(0x0010, 0x2201);

		///<summary>(0010,2202) VR=SQ VM=1 Patient Species Code Sequence</summary>
		public readonly static DicomTag PatientSpeciesCodeSequence = new DicomTag(0x0010, 0x2202);

		///<summary>(0010,2203) VR=CS VM=1 Patient’s Sex Neutered</summary>
		public readonly static DicomTag PatientSexNeutered = new DicomTag(0x0010, 0x2203);

		///<summary>(0010,2210) VR=CS VM=1 Anatomical Orientation Type</summary>
		public readonly static DicomTag AnatomicalOrientationType = new DicomTag(0x0010, 0x2210);

		///<summary>(0010,2292) VR=LO VM=1 Patient Breed Description</summary>
		public readonly static DicomTag PatientBreedDescription = new DicomTag(0x0010, 0x2292);

		///<summary>(0010,2293) VR=SQ VM=1 Patient Breed Code Sequence</summary>
		public readonly static DicomTag PatientBreedCodeSequence = new DicomTag(0x0010, 0x2293);

		///<summary>(0010,2294) VR=SQ VM=1 Breed Registration Sequence</summary>
		public readonly static DicomTag BreedRegistrationSequence = new DicomTag(0x0010, 0x2294);

		///<summary>(0010,2295) VR=LO VM=1 Breed Registration Number</summary>
		public readonly static DicomTag BreedRegistrationNumber = new DicomTag(0x0010, 0x2295);

		///<summary>(0010,2296) VR=SQ VM=1 Breed Registry Code Sequence</summary>
		public readonly static DicomTag BreedRegistryCodeSequence = new DicomTag(0x0010, 0x2296);

		///<summary>(0010,2297) VR=PN VM=1 Responsible Person</summary>
		public readonly static DicomTag ResponsiblePerson = new DicomTag(0x0010, 0x2297);

		///<summary>(0010,2298) VR=CS VM=1 Responsible Person Role</summary>
		public readonly static DicomTag ResponsiblePersonRole = new DicomTag(0x0010, 0x2298);

		///<summary>(0010,2299) VR=LO VM=1 Responsible Organization</summary>
		public readonly static DicomTag ResponsibleOrganization = new DicomTag(0x0010, 0x2299);

		///<summary>(0010,4000) VR=LT VM=1 Patient Comments</summary>
		public readonly static DicomTag PatientComments = new DicomTag(0x0010, 0x4000);

		///<summary>(0010,9431) VR=FL VM=1 Examined Body Thickness</summary>
		public readonly static DicomTag ExaminedBodyThickness = new DicomTag(0x0010, 0x9431);

		///<summary>(0012,0010) VR=LO VM=1 Clinical Trial Sponsor Name</summary>
		public readonly static DicomTag ClinicalTrialSponsorName = new DicomTag(0x0012, 0x0010);

		///<summary>(0012,0020) VR=LO VM=1 Clinical Trial Protocol ID</summary>
		public readonly static DicomTag ClinicalTrialProtocolID = new DicomTag(0x0012, 0x0020);

		///<summary>(0012,0021) VR=LO VM=1 Clinical Trial Protocol Name</summary>
		public readonly static DicomTag ClinicalTrialProtocolName = new DicomTag(0x0012, 0x0021);

		///<summary>(0012,0030) VR=LO VM=1 Clinical Trial Site ID</summary>
		public readonly static DicomTag ClinicalTrialSiteID = new DicomTag(0x0012, 0x0030);

		///<summary>(0012,0031) VR=LO VM=1 Clinical Trial Site Name</summary>
		public readonly static DicomTag ClinicalTrialSiteName = new DicomTag(0x0012, 0x0031);

		///<summary>(0012,0040) VR=LO VM=1 Clinical Trial Subject ID</summary>
		public readonly static DicomTag ClinicalTrialSubjectID = new DicomTag(0x0012, 0x0040);

		///<summary>(0012,0042) VR=LO VM=1 Clinical Trial Subject Reading ID</summary>
		public readonly static DicomTag ClinicalTrialSubjectReadingID = new DicomTag(0x0012, 0x0042);

		///<summary>(0012,0050) VR=LO VM=1 Clinical Trial Time Point ID</summary>
		public readonly static DicomTag ClinicalTrialTimePointID = new DicomTag(0x0012, 0x0050);

		///<summary>(0012,0051) VR=ST VM=1 Clinical Trial Time Point Description</summary>
		public readonly static DicomTag ClinicalTrialTimePointDescription = new DicomTag(0x0012, 0x0051);

		///<summary>(0012,0060) VR=LO VM=1 Clinical Trial Coordinating Center Name</summary>
		public readonly static DicomTag ClinicalTrialCoordinatingCenterName = new DicomTag(0x0012, 0x0060);

		///<summary>(0012,0062) VR=CS VM=1 Patient Identity Removed</summary>
		public readonly static DicomTag PatientIdentityRemoved = new DicomTag(0x0012, 0x0062);

		///<summary>(0012,0063) VR=LO VM=1-n De-identification Method</summary>
		public readonly static DicomTag DeidentificationMethod = new DicomTag(0x0012, 0x0063);

		///<summary>(0012,0064) VR=SQ VM=1 De-identification Method Code Sequence</summary>
		public readonly static DicomTag DeidentificationMethodCodeSequence = new DicomTag(0x0012, 0x0064);

		///<summary>(0012,0071) VR=LO VM=1 Clinical Trial Series ID</summary>
		public readonly static DicomTag ClinicalTrialSeriesID = new DicomTag(0x0012, 0x0071);

		///<summary>(0012,0072) VR=LO VM=1 Clinical Trial Series Description</summary>
		public readonly static DicomTag ClinicalTrialSeriesDescription = new DicomTag(0x0012, 0x0072);

		///<summary>(0012,0081) VR=LO VM=1 Clinical Trial Protocol Ethics Committee Name</summary>
		public readonly static DicomTag ClinicalTrialProtocolEthicsCommitteeName = new DicomTag(0x0012, 0x0081);

		///<summary>(0012,0082) VR=LO VM=1 Clinical Trial Protocol Ethics Committee Approval Number</summary>
		public readonly static DicomTag ClinicalTrialProtocolEthicsCommitteeApprovalNumber = new DicomTag(0x0012, 0x0082);

		///<summary>(0012,0083) VR=SQ VM=1 Consent for Clinical Trial Use Sequence</summary>
		public readonly static DicomTag ConsentForClinicalTrialUseSequence = new DicomTag(0x0012, 0x0083);

		///<summary>(0012,0084) VR=CS VM=1 Distribution Type</summary>
		public readonly static DicomTag DistributionType = new DicomTag(0x0012, 0x0084);

		///<summary>(0012,0085) VR=CS VM=1 Consent for Distribution Flag</summary>
		public readonly static DicomTag ConsentForDistributionFlag = new DicomTag(0x0012, 0x0085);

		///<summary>(0014,0023) VR=ST VM=1-n CAD File Format</summary>
		public readonly static DicomTag CADFileFormat = new DicomTag(0x0014, 0x0023);

		///<summary>(0014,0024) VR=ST VM=1-n Component Reference System</summary>
		public readonly static DicomTag ComponentReferenceSystem = new DicomTag(0x0014, 0x0024);

		///<summary>(0014,0025) VR=ST VM=1-n Component Manufacturing Procedure</summary>
		public readonly static DicomTag ComponentManufacturingProcedure = new DicomTag(0x0014, 0x0025);

		///<summary>(0014,0028) VR=ST VM=1-n Component Manufacturer</summary>
		public readonly static DicomTag ComponentManufacturer = new DicomTag(0x0014, 0x0028);

		///<summary>(0014,0030) VR=DS VM=1-n Material Thickness</summary>
		public readonly static DicomTag MaterialThickness = new DicomTag(0x0014, 0x0030);

		///<summary>(0014,0032) VR=DS VM=1-n Material Pipe Diameter</summary>
		public readonly static DicomTag MaterialPipeDiameter = new DicomTag(0x0014, 0x0032);

		///<summary>(0014,0034) VR=DS VM=1-n Material Isolation Diameter</summary>
		public readonly static DicomTag MaterialIsolationDiameter = new DicomTag(0x0014, 0x0034);

		///<summary>(0014,0042) VR=ST VM=1-n Material Grade</summary>
		public readonly static DicomTag MaterialGrade = new DicomTag(0x0014, 0x0042);

		///<summary>(0014,0044) VR=ST VM=1-n Material Properties File ID</summary>
		public readonly static DicomTag MaterialPropertiesFileID = new DicomTag(0x0014, 0x0044);

		///<summary>(0014,0045) VR=ST VM=1-n Material Properties File Format</summary>
		public readonly static DicomTag MaterialPropertiesFileFormat = new DicomTag(0x0014, 0x0045);

		///<summary>(0014,0046) VR=LT VM=1 Material Notes</summary>
		public readonly static DicomTag MaterialNotes = new DicomTag(0x0014, 0x0046);

		///<summary>(0014,0050) VR=CS VM=1 Component Shape</summary>
		public readonly static DicomTag ComponentShape = new DicomTag(0x0014, 0x0050);

		///<summary>(0014,0052) VR=CS VM=1 Curvature Type</summary>
		public readonly static DicomTag CurvatureType = new DicomTag(0x0014, 0x0052);

		///<summary>(0014,0054) VR=DS VM=1 Outer Diameter</summary>
		public readonly static DicomTag OuterDiameter = new DicomTag(0x0014, 0x0054);

		///<summary>(0014,0056) VR=DS VM=1 Inner Diameter</summary>
		public readonly static DicomTag InnerDiameter = new DicomTag(0x0014, 0x0056);

		///<summary>(0014,1010) VR=ST VM=1 Actual Environmental Conditions</summary>
		public readonly static DicomTag ActualEnvironmentalConditions = new DicomTag(0x0014, 0x1010);

		///<summary>(0014,1020) VR=DA VM=1 Expiry Date</summary>
		public readonly static DicomTag ExpiryDate = new DicomTag(0x0014, 0x1020);

		///<summary>(0014,1040) VR=ST VM=1 Environmental Conditions</summary>
		public readonly static DicomTag EnvironmentalConditions = new DicomTag(0x0014, 0x1040);

		///<summary>(0014,2002) VR=SQ VM=1 Evaluator Sequence</summary>
		public readonly static DicomTag EvaluatorSequence = new DicomTag(0x0014, 0x2002);

		///<summary>(0014,2004) VR=IS VM=1 Evaluator Number</summary>
		public readonly static DicomTag EvaluatorNumber = new DicomTag(0x0014, 0x2004);

		///<summary>(0014,2006) VR=PN VM=1 Evaluator Name</summary>
		public readonly static DicomTag EvaluatorName = new DicomTag(0x0014, 0x2006);

		///<summary>(0014,2008) VR=IS VM=1 Evaluation Attempt</summary>
		public readonly static DicomTag EvaluationAttempt = new DicomTag(0x0014, 0x2008);

		///<summary>(0014,2012) VR=SQ VM=1 Indication Sequence</summary>
		public readonly static DicomTag IndicationSequence = new DicomTag(0x0014, 0x2012);

		///<summary>(0014,2014) VR=IS VM=1 Indication Number</summary>
		public readonly static DicomTag IndicationNumber  = new DicomTag(0x0014, 0x2014);

		///<summary>(0014,2016) VR=SH VM=1 Indication Label</summary>
		public readonly static DicomTag IndicationLabel = new DicomTag(0x0014, 0x2016);

		///<summary>(0014,2018) VR=ST VM=1 Indication Description</summary>
		public readonly static DicomTag IndicationDescription = new DicomTag(0x0014, 0x2018);

		///<summary>(0014,201a) VR=CS VM=1-n Indication Type</summary>
		public readonly static DicomTag IndicationType = new DicomTag(0x0014, 0x201a);

		///<summary>(0014,201c) VR=CS VM=1 Indication Disposition</summary>
		public readonly static DicomTag IndicationDisposition = new DicomTag(0x0014, 0x201c);

		///<summary>(0014,201e) VR=SQ VM=1 Indication ROI Sequence</summary>
		public readonly static DicomTag IndicationROISequence = new DicomTag(0x0014, 0x201e);

		///<summary>(0014,2030) VR=SQ VM=1 Indication Physical Property Sequence</summary>
		public readonly static DicomTag IndicationPhysicalPropertySequence = new DicomTag(0x0014, 0x2030);

		///<summary>(0014,2032) VR=SH VM=1 Property Label</summary>
		public readonly static DicomTag PropertyLabel = new DicomTag(0x0014, 0x2032);

		///<summary>(0014,2202) VR=IS VM=1 Coordinate System Number of Axes</summary>
		public readonly static DicomTag CoordinateSystemNumberOfAxes  = new DicomTag(0x0014, 0x2202);

		///<summary>(0014,2204) VR=SQ VM=1 Coordinate System Axes Sequence</summary>
		public readonly static DicomTag CoordinateSystemAxesSequence = new DicomTag(0x0014, 0x2204);

		///<summary>(0014,2206) VR=ST VM=1 Coordinate System Axis Description</summary>
		public readonly static DicomTag CoordinateSystemAxisDescription = new DicomTag(0x0014, 0x2206);

		///<summary>(0014,2208) VR=CS VM=1 Coordinate System Data Set Mapping</summary>
		public readonly static DicomTag CoordinateSystemDataSetMapping = new DicomTag(0x0014, 0x2208);

		///<summary>(0014,220a) VR=IS VM=1 Coordinate System Axis Number</summary>
		public readonly static DicomTag CoordinateSystemAxisNumber = new DicomTag(0x0014, 0x220a);

		///<summary>(0014,220c) VR=CS VM=1 Coordinate System Axis Type</summary>
		public readonly static DicomTag CoordinateSystemAxisType = new DicomTag(0x0014, 0x220c);

		///<summary>(0014,220e) VR=CS VM=1 Coordinate System Axis Units</summary>
		public readonly static DicomTag CoordinateSystemAxisUnits = new DicomTag(0x0014, 0x220e);

		///<summary>(0014,2210) VR=OB VM=1 Coordinate System Axis Values</summary>
		public readonly static DicomTag CoordinateSystemAxisValues = new DicomTag(0x0014, 0x2210);

		///<summary>(0014,2220) VR=SQ VM=1 Coordinate System Transform Sequence</summary>
		public readonly static DicomTag CoordinateSystemTransformSequence = new DicomTag(0x0014, 0x2220);

		///<summary>(0014,2222) VR=ST VM=1 Transform Description</summary>
		public readonly static DicomTag TransformDescription = new DicomTag(0x0014, 0x2222);

		///<summary>(0014,2224) VR=IS VM=1 Transform Number of Axes</summary>
		public readonly static DicomTag TransformNumberOfAxes = new DicomTag(0x0014, 0x2224);

		///<summary>(0014,2226) VR=IS VM=1-n Transform Order of Axes</summary>
		public readonly static DicomTag TransformOrderOfAxes = new DicomTag(0x0014, 0x2226);

		///<summary>(0014,2228) VR=CS VM=1 Transformed Axis Units</summary>
		public readonly static DicomTag TransformedAxisUnits = new DicomTag(0x0014, 0x2228);

		///<summary>(0014,222a) VR=DS VM=1-n Coordinate System Transform Rotation and Scale Matrix</summary>
		public readonly static DicomTag CoordinateSystemTransformRotationAndScaleMatrix = new DicomTag(0x0014, 0x222a);

		///<summary>(0014,222c) VR=DS VM=1-n Coordinate System Transform Translation Matrix</summary>
		public readonly static DicomTag CoordinateSystemTransformTranslationMatrix = new DicomTag(0x0014, 0x222c);

		///<summary>(0014,3011) VR=DS VM=1 Internal Detector Frame Time</summary>
		public readonly static DicomTag InternalDetectorFrameTime = new DicomTag(0x0014, 0x3011);

		///<summary>(0014,3012) VR=DS VM=1 Number of Frames Integrated</summary>
		public readonly static DicomTag NumberOfFramesIntegrated = new DicomTag(0x0014, 0x3012);

		///<summary>(0014,3020) VR=SQ VM=1 Detector Temperature Sequence</summary>
		public readonly static DicomTag DetectorTemperatureSequence = new DicomTag(0x0014, 0x3020);

		///<summary>(0014,3022) VR=DS VM=1 Sensor Name</summary>
		public readonly static DicomTag SensorName = new DicomTag(0x0014, 0x3022);

		///<summary>(0014,3024) VR=DS VM=1 Horizontal Offset of Sensor</summary>
		public readonly static DicomTag HorizontalOffsetOfSensor = new DicomTag(0x0014, 0x3024);

		///<summary>(0014,3026) VR=DS VM=1 Vertical Offset of Sensor</summary>
		public readonly static DicomTag VerticalOffsetOfSensor = new DicomTag(0x0014, 0x3026);

		///<summary>(0014,3028) VR=DS VM=1 Sensor Temperature</summary>
		public readonly static DicomTag SensorTemperature = new DicomTag(0x0014, 0x3028);

		///<summary>(0014,3040) VR=SQ VM=1 Dark Current Sequence</summary>
		public readonly static DicomTag DarkCurrentSequence = new DicomTag(0x0014, 0x3040);

		///<summary>(0014,3050) VR=OB/OW VM=1 Dark Current Counts</summary>
		public readonly static DicomTag DarkCurrentCounts = new DicomTag(0x0014, 0x3050);

		///<summary>(0014,3060) VR=SQ VM=1 Gain Correction Reference Sequence</summary>
		public readonly static DicomTag GainCorrectionReferenceSequence = new DicomTag(0x0014, 0x3060);

		///<summary>(0014,3070) VR=OB/OW VM=1 Air Counts</summary>
		public readonly static DicomTag AirCounts = new DicomTag(0x0014, 0x3070);

		///<summary>(0014,3071) VR=DS VM=1 KV Used in Gain Calibration</summary>
		public readonly static DicomTag KVUsedInGainCalibration = new DicomTag(0x0014, 0x3071);

		///<summary>(0014,3072) VR=DS VM=1 MA Used in Gain Calibration</summary>
		public readonly static DicomTag MAUsedInGainCalibration = new DicomTag(0x0014, 0x3072);

		///<summary>(0014,3073) VR=DS VM=1 Number of Frames Used for Integration</summary>
		public readonly static DicomTag NumberOfFramesUsedForIntegration = new DicomTag(0x0014, 0x3073);

		///<summary>(0014,3074) VR=LO VM=1 Filter Material Used in Gain Calibration</summary>
		public readonly static DicomTag FilterMaterialUsedInGainCalibration = new DicomTag(0x0014, 0x3074);

		///<summary>(0014,3075) VR=DS VM=1 Filter Thickness Used in Gain Calibration</summary>
		public readonly static DicomTag FilterThicknessUsedInGainCalibration = new DicomTag(0x0014, 0x3075);

		///<summary>(0014,3076) VR=DA VM=1 Date of Gain Calibration</summary>
		public readonly static DicomTag DateOfGainCalibration = new DicomTag(0x0014, 0x3076);

		///<summary>(0014,3077) VR=TM VM=1 Time of Gain Calibration</summary>
		public readonly static DicomTag TimeOfGainCalibration = new DicomTag(0x0014, 0x3077);

		///<summary>(0014,3080) VR=OB VM=1 Bad Pixel Image</summary>
		public readonly static DicomTag BadPixelImage = new DicomTag(0x0014, 0x3080);

		///<summary>(0014,3099) VR=LT VM=1 Calibration Notes</summary>
		public readonly static DicomTag CalibrationNotes = new DicomTag(0x0014, 0x3099);

		///<summary>(0014,4002) VR=SQ VM=1 Pulser Equipment Sequence</summary>
		public readonly static DicomTag PulserEquipmentSequence = new DicomTag(0x0014, 0x4002);

		///<summary>(0014,4004) VR=CS VM=1 Pulser Type</summary>
		public readonly static DicomTag PulserType = new DicomTag(0x0014, 0x4004);

		///<summary>(0014,4006) VR=LT VM=1 Pulser Notes</summary>
		public readonly static DicomTag PulserNotes = new DicomTag(0x0014, 0x4006);

		///<summary>(0014,4008) VR=SQ VM=1 Receiver Equipment Sequence</summary>
		public readonly static DicomTag ReceiverEquipmentSequence = new DicomTag(0x0014, 0x4008);

		///<summary>(0014,400a) VR=CS VM=1 Amplifier Type</summary>
		public readonly static DicomTag AmplifierType = new DicomTag(0x0014, 0x400a);

		///<summary>(0014,400c) VR=LT VM=1 Receiver Notes</summary>
		public readonly static DicomTag ReceiverNotes = new DicomTag(0x0014, 0x400c);

		///<summary>(0014,400e) VR=SQ VM=1 Pre-Amplifier Equipment Sequence</summary>
		public readonly static DicomTag PreAmplifierEquipmentSequence = new DicomTag(0x0014, 0x400e);

		///<summary>(0014,400f) VR=LT VM=1 Pre-Amplifier Notes</summary>
		public readonly static DicomTag PreAmplifierNotes = new DicomTag(0x0014, 0x400f);

		///<summary>(0014,4010) VR=SQ VM=1 Transmit Transducer Sequence</summary>
		public readonly static DicomTag TransmitTransducerSequence = new DicomTag(0x0014, 0x4010);

		///<summary>(0014,4011) VR=SQ VM=1 Receive Transducer Sequence</summary>
		public readonly static DicomTag ReceiveTransducerSequence = new DicomTag(0x0014, 0x4011);

		///<summary>(0014,4012) VR=US VM=1 Number of Elements</summary>
		public readonly static DicomTag NumberOfElements = new DicomTag(0x0014, 0x4012);

		///<summary>(0014,4013) VR=CS VM=1 Element Shape</summary>
		public readonly static DicomTag ElementShape = new DicomTag(0x0014, 0x4013);

		///<summary>(0014,4014) VR=DS VM=1 Element Dimension A</summary>
		public readonly static DicomTag ElementDimensionA = new DicomTag(0x0014, 0x4014);

		///<summary>(0014,4015) VR=DS VM=1 Element Dimension B</summary>
		public readonly static DicomTag ElementDimensionB = new DicomTag(0x0014, 0x4015);

		///<summary>(0014,4016) VR=DS VM=1 Element Pitch</summary>
		public readonly static DicomTag ElementPitch = new DicomTag(0x0014, 0x4016);

		///<summary>(0014,4017) VR=DS VM=1 Measured Beam Dimension A</summary>
		public readonly static DicomTag MeasuredBeamDimensionA = new DicomTag(0x0014, 0x4017);

		///<summary>(0014,4018) VR=DS VM=1 Measured Beam Dimension B</summary>
		public readonly static DicomTag MeasuredBeamDimensionB = new DicomTag(0x0014, 0x4018);

		///<summary>(0014,4019) VR=DS VM=1 Location of Measured Beam Diameter</summary>
		public readonly static DicomTag LocationOfMeasuredBeamDiameter = new DicomTag(0x0014, 0x4019);

		///<summary>(0014,401a) VR=DS VM=1 Nominal Frequency</summary>
		public readonly static DicomTag NominalFrequency = new DicomTag(0x0014, 0x401a);

		///<summary>(0014,401b) VR=DS VM=1 Measured Center Frequency</summary>
		public readonly static DicomTag MeasuredCenterFrequency = new DicomTag(0x0014, 0x401b);

		///<summary>(0014,401c) VR=DS VM=1 Measured Bandwidth</summary>
		public readonly static DicomTag MeasuredBandwidth = new DicomTag(0x0014, 0x401c);

		///<summary>(0014,4020) VR=SQ VM=1 Pulser Settings Sequence</summary>
		public readonly static DicomTag PulserSettingsSequence = new DicomTag(0x0014, 0x4020);

		///<summary>(0014,4022) VR=DS VM=1 Pulse Width</summary>
		public readonly static DicomTag PulseWidth = new DicomTag(0x0014, 0x4022);

		///<summary>(0014,4024) VR=DS VM=1 Excitation Frequency</summary>
		public readonly static DicomTag ExcitationFrequency = new DicomTag(0x0014, 0x4024);

		///<summary>(0014,4026) VR=CS VM=1 Modulation Type</summary>
		public readonly static DicomTag ModulationType = new DicomTag(0x0014, 0x4026);

		///<summary>(0014,4028) VR=DS VM=1 Damping</summary>
		public readonly static DicomTag Damping = new DicomTag(0x0014, 0x4028);

		///<summary>(0014,4030) VR=SQ VM=1 Receiver Settings Sequence</summary>
		public readonly static DicomTag ReceiverSettingsSequence = new DicomTag(0x0014, 0x4030);

		///<summary>(0014,4031) VR=DS VM=1 Acquired Soundpath Length</summary>
		public readonly static DicomTag AcquiredSoundpathLength = new DicomTag(0x0014, 0x4031);

		///<summary>(0014,4032) VR=CS VM=1 Acquisition Compression Type</summary>
		public readonly static DicomTag AcquisitionCompressionType = new DicomTag(0x0014, 0x4032);

		///<summary>(0014,4033) VR=IS VM=1 Acquisition Sample Size</summary>
		public readonly static DicomTag AcquisitionSampleSize = new DicomTag(0x0014, 0x4033);

		///<summary>(0014,4034) VR=DS VM=1 Rectifier Smoothing</summary>
		public readonly static DicomTag RectifierSmoothing = new DicomTag(0x0014, 0x4034);

		///<summary>(0014,4035) VR=SQ VM=1 DAC Sequence</summary>
		public readonly static DicomTag DACSequence = new DicomTag(0x0014, 0x4035);

		///<summary>(0014,4036) VR=CS VM=1 DAC Type</summary>
		public readonly static DicomTag DACType = new DicomTag(0x0014, 0x4036);

		///<summary>(0014,4038) VR=DS VM=1-n DAC Gain Points</summary>
		public readonly static DicomTag DACGainPoints = new DicomTag(0x0014, 0x4038);

		///<summary>(0014,403a) VR=DS VM=1-n DAC Time Points</summary>
		public readonly static DicomTag DACTimePoints = new DicomTag(0x0014, 0x403a);

		///<summary>(0014,403c) VR=DS VM=1-n DAC Amplitude</summary>
		public readonly static DicomTag DACAmplitude = new DicomTag(0x0014, 0x403c);

		///<summary>(0014,4040) VR=SQ VM=1 Pre-Amplifier Settings Sequence</summary>
		public readonly static DicomTag PreAmplifierSettingsSequence = new DicomTag(0x0014, 0x4040);

		///<summary>(0014,4050) VR=SQ VM=1 Transmit Transducer Settings Sequence</summary>
		public readonly static DicomTag TransmitTransducerSettingsSequence = new DicomTag(0x0014, 0x4050);

		///<summary>(0014,4051) VR=SQ VM=1 Receive Transducer Settings Sequence</summary>
		public readonly static DicomTag ReceiveTransducerSettingsSequence = new DicomTag(0x0014, 0x4051);

		///<summary>(0014,4052) VR=DS VM=1 Incident Angle</summary>
		public readonly static DicomTag IncidentAngle = new DicomTag(0x0014, 0x4052);

		///<summary>(0014,4054) VR=ST VM=1 Coupling Technique</summary>
		public readonly static DicomTag CouplingTechnique = new DicomTag(0x0014, 0x4054);

		///<summary>(0014,4056) VR=ST VM=1 Coupling Medium</summary>
		public readonly static DicomTag CouplingMedium = new DicomTag(0x0014, 0x4056);

		///<summary>(0014,4057) VR=DS VM=1 Coupling Velocity</summary>
		public readonly static DicomTag CouplingVelocity = new DicomTag(0x0014, 0x4057);

		///<summary>(0014,4058) VR=DS VM=1 Crystal Center Location X</summary>
		public readonly static DicomTag CrystalCenterLocationX = new DicomTag(0x0014, 0x4058);

		///<summary>(0014,4059) VR=DS VM=1 Crystal Center Location Z</summary>
		public readonly static DicomTag CrystalCenterLocationZ = new DicomTag(0x0014, 0x4059);

		///<summary>(0014,405a) VR=DS VM=1 Sound Path Length</summary>
		public readonly static DicomTag SoundPathLength = new DicomTag(0x0014, 0x405a);

		///<summary>(0014,405c) VR=ST VM=1 Delay Law Identifier</summary>
		public readonly static DicomTag DelayLawIdentifier = new DicomTag(0x0014, 0x405c);

		///<summary>(0014,4060) VR=SQ VM=1 Gate Settings Sequence</summary>
		public readonly static DicomTag GateSettingsSequence = new DicomTag(0x0014, 0x4060);

		///<summary>(0014,4062) VR=DS VM=1 Gate Threshold</summary>
		public readonly static DicomTag GateThreshold = new DicomTag(0x0014, 0x4062);

		///<summary>(0014,4064) VR=DS VM=1 Velocity of Sound</summary>
		public readonly static DicomTag VelocityOfSound = new DicomTag(0x0014, 0x4064);

		///<summary>(0014,4070) VR=SQ VM=1 Calibration Settings Sequence</summary>
		public readonly static DicomTag CalibrationSettingsSequence = new DicomTag(0x0014, 0x4070);

		///<summary>(0014,4072) VR=ST VM=1 Calibration Procedure</summary>
		public readonly static DicomTag CalibrationProcedure = new DicomTag(0x0014, 0x4072);

		///<summary>(0014,4074) VR=SH VM=1 Procedure Version</summary>
		public readonly static DicomTag ProcedureVersion = new DicomTag(0x0014, 0x4074);

		///<summary>(0014,4076) VR=DA VM=1 Procedure Creation Date</summary>
		public readonly static DicomTag ProcedureCreationDate = new DicomTag(0x0014, 0x4076);

		///<summary>(0014,4078) VR=DA VM=1 Procedure Expiration Date</summary>
		public readonly static DicomTag ProcedureExpirationDate = new DicomTag(0x0014, 0x4078);

		///<summary>(0014,407a) VR=DA VM=1 Procedure Last Modified Date</summary>
		public readonly static DicomTag ProcedureLastModifiedDate = new DicomTag(0x0014, 0x407a);

		///<summary>(0014,407c) VR=TM VM=1-n Calibration Time</summary>
		public readonly static DicomTag CalibrationTime = new DicomTag(0x0014, 0x407c);

		///<summary>(0014,407e) VR=DA VM=1-n Calibration Date</summary>
		public readonly static DicomTag CalibrationDate = new DicomTag(0x0014, 0x407e);

		///<summary>(0014,5002) VR=IS VM=1 LINAC Energy</summary>
		public readonly static DicomTag LINACEnergy = new DicomTag(0x0014, 0x5002);

		///<summary>(0014,5004) VR=IS VM=1 LINAC Output</summary>
		public readonly static DicomTag LINACOutput = new DicomTag(0x0014, 0x5004);

		///<summary>(0018,0010) VR=LO VM=1 Contrast/Bolus Agent</summary>
		public readonly static DicomTag ContrastBolusAgent = new DicomTag(0x0018, 0x0010);

		///<summary>(0018,0012) VR=SQ VM=1 Contrast/Bolus Agent Sequence</summary>
		public readonly static DicomTag ContrastBolusAgentSequence = new DicomTag(0x0018, 0x0012);

		///<summary>(0018,0014) VR=SQ VM=1 Contrast/Bolus Administration Route Sequence</summary>
		public readonly static DicomTag ContrastBolusAdministrationRouteSequence = new DicomTag(0x0018, 0x0014);

		///<summary>(0018,0015) VR=CS VM=1 Body Part Examined</summary>
		public readonly static DicomTag BodyPartExamined = new DicomTag(0x0018, 0x0015);

		///<summary>(0018,0020) VR=CS VM=1-n Scanning Sequence</summary>
		public readonly static DicomTag ScanningSequence = new DicomTag(0x0018, 0x0020);

		///<summary>(0018,0021) VR=CS VM=1-n Sequence Variant</summary>
		public readonly static DicomTag SequenceVariant = new DicomTag(0x0018, 0x0021);

		///<summary>(0018,0022) VR=CS VM=1-n Scan Options</summary>
		public readonly static DicomTag ScanOptions = new DicomTag(0x0018, 0x0022);

		///<summary>(0018,0023) VR=CS VM=1 MR Acquisition Type</summary>
		public readonly static DicomTag MRAcquisitionType = new DicomTag(0x0018, 0x0023);

		///<summary>(0018,0024) VR=SH VM=1 Sequence Name</summary>
		public readonly static DicomTag SequenceName = new DicomTag(0x0018, 0x0024);

		///<summary>(0018,0025) VR=CS VM=1 Angio Flag</summary>
		public readonly static DicomTag AngioFlag = new DicomTag(0x0018, 0x0025);

		///<summary>(0018,0026) VR=SQ VM=1 Intervention Drug Information Sequence</summary>
		public readonly static DicomTag InterventionDrugInformationSequence = new DicomTag(0x0018, 0x0026);

		///<summary>(0018,0027) VR=TM VM=1 Intervention Drug Stop Time</summary>
		public readonly static DicomTag InterventionDrugStopTime = new DicomTag(0x0018, 0x0027);

		///<summary>(0018,0028) VR=DS VM=1 Intervention Drug Dose</summary>
		public readonly static DicomTag InterventionDrugDose = new DicomTag(0x0018, 0x0028);

		///<summary>(0018,0029) VR=SQ VM=1 Intervention Drug Code Sequence</summary>
		public readonly static DicomTag InterventionDrugCodeSequence = new DicomTag(0x0018, 0x0029);

		///<summary>(0018,002a) VR=SQ VM=1 Additional Drug Sequence</summary>
		public readonly static DicomTag AdditionalDrugSequence = new DicomTag(0x0018, 0x002a);

		///<summary>(0018,0030) VR=LO VM=1-n Radionuclide (RETIRED)</summary>
		public readonly static DicomTag RadionuclideRETIRED = new DicomTag(0x0018, 0x0030);

		///<summary>(0018,0031) VR=LO VM=1 Radiopharmaceutical</summary>
		public readonly static DicomTag Radiopharmaceutical = new DicomTag(0x0018, 0x0031);

		///<summary>(0018,0032) VR=DS VM=1 Energy Window Centerline (RETIRED)</summary>
		public readonly static DicomTag EnergyWindowCenterlineRETIRED = new DicomTag(0x0018, 0x0032);

		///<summary>(0018,0033) VR=DS VM=1-n Energy Window Total Width (RETIRED)</summary>
		public readonly static DicomTag EnergyWindowTotalWidthRETIRED = new DicomTag(0x0018, 0x0033);

		///<summary>(0018,0034) VR=LO VM=1 Intervention Drug Name</summary>
		public readonly static DicomTag InterventionDrugName = new DicomTag(0x0018, 0x0034);

		///<summary>(0018,0035) VR=TM VM=1 Intervention Drug Start Time</summary>
		public readonly static DicomTag InterventionDrugStartTime = new DicomTag(0x0018, 0x0035);

		///<summary>(0018,0036) VR=SQ VM=1 Intervention Sequence</summary>
		public readonly static DicomTag InterventionSequence = new DicomTag(0x0018, 0x0036);

		///<summary>(0018,0037) VR=CS VM=1 Therapy Type (RETIRED)</summary>
		public readonly static DicomTag TherapyTypeRETIRED = new DicomTag(0x0018, 0x0037);

		///<summary>(0018,0038) VR=CS VM=1 Intervention Status</summary>
		public readonly static DicomTag InterventionStatus = new DicomTag(0x0018, 0x0038);

		///<summary>(0018,0039) VR=CS VM=1 Therapy Description (RETIRED)</summary>
		public readonly static DicomTag TherapyDescriptionRETIRED = new DicomTag(0x0018, 0x0039);

		///<summary>(0018,003a) VR=ST VM=1 Intervention Description</summary>
		public readonly static DicomTag InterventionDescription = new DicomTag(0x0018, 0x003a);

		///<summary>(0018,0040) VR=IS VM=1 Cine Rate</summary>
		public readonly static DicomTag CineRate = new DicomTag(0x0018, 0x0040);

		///<summary>(0018,0042) VR=CS VM=1 Initial Cine Run State</summary>
		public readonly static DicomTag InitialCineRunState = new DicomTag(0x0018, 0x0042);

		///<summary>(0018,0050) VR=DS VM=1 Slice Thickness</summary>
		public readonly static DicomTag SliceThickness = new DicomTag(0x0018, 0x0050);

		///<summary>(0018,0060) VR=DS VM=1 KVP</summary>
		public readonly static DicomTag KVP = new DicomTag(0x0018, 0x0060);

		///<summary>(0018,0070) VR=IS VM=1 Counts Accumulated</summary>
		public readonly static DicomTag CountsAccumulated = new DicomTag(0x0018, 0x0070);

		///<summary>(0018,0071) VR=CS VM=1 Acquisition Termination Condition</summary>
		public readonly static DicomTag AcquisitionTerminationCondition = new DicomTag(0x0018, 0x0071);

		///<summary>(0018,0072) VR=DS VM=1 Effective Duration</summary>
		public readonly static DicomTag EffectiveDuration = new DicomTag(0x0018, 0x0072);

		///<summary>(0018,0073) VR=CS VM=1 Acquisition Start Condition</summary>
		public readonly static DicomTag AcquisitionStartCondition = new DicomTag(0x0018, 0x0073);

		///<summary>(0018,0074) VR=IS VM=1 Acquisition Start Condition Data</summary>
		public readonly static DicomTag AcquisitionStartConditionData = new DicomTag(0x0018, 0x0074);

		///<summary>(0018,0075) VR=IS VM=1 Acquisition Termination Condition Data</summary>
		public readonly static DicomTag AcquisitionTerminationConditionData = new DicomTag(0x0018, 0x0075);

		///<summary>(0018,0080) VR=DS VM=1 Repetition Time</summary>
		public readonly static DicomTag RepetitionTime = new DicomTag(0x0018, 0x0080);

		///<summary>(0018,0081) VR=DS VM=1 Echo Time</summary>
		public readonly static DicomTag EchoTime = new DicomTag(0x0018, 0x0081);

		///<summary>(0018,0082) VR=DS VM=1 Inversion Time</summary>
		public readonly static DicomTag InversionTime = new DicomTag(0x0018, 0x0082);

		///<summary>(0018,0083) VR=DS VM=1 Number of Averages</summary>
		public readonly static DicomTag NumberOfAverages = new DicomTag(0x0018, 0x0083);

		///<summary>(0018,0084) VR=DS VM=1 Imaging Frequency</summary>
		public readonly static DicomTag ImagingFrequency = new DicomTag(0x0018, 0x0084);

		///<summary>(0018,0085) VR=SH VM=1 Imaged Nucleus</summary>
		public readonly static DicomTag ImagedNucleus = new DicomTag(0x0018, 0x0085);

		///<summary>(0018,0086) VR=IS VM=1-n Echo Number(s)</summary>
		public readonly static DicomTag EchoNumbers = new DicomTag(0x0018, 0x0086);

		///<summary>(0018,0087) VR=DS VM=1 Magnetic Field Strength</summary>
		public readonly static DicomTag MagneticFieldStrength = new DicomTag(0x0018, 0x0087);

		///<summary>(0018,0088) VR=DS VM=1 Spacing Between Slices</summary>
		public readonly static DicomTag SpacingBetweenSlices = new DicomTag(0x0018, 0x0088);

		///<summary>(0018,0089) VR=IS VM=1 Number of Phase Encoding Steps</summary>
		public readonly static DicomTag NumberOfPhaseEncodingSteps = new DicomTag(0x0018, 0x0089);

		///<summary>(0018,0090) VR=DS VM=1 Data Collection Diameter</summary>
		public readonly static DicomTag DataCollectionDiameter = new DicomTag(0x0018, 0x0090);

		///<summary>(0018,0091) VR=IS VM=1 Echo Train Length</summary>
		public readonly static DicomTag EchoTrainLength = new DicomTag(0x0018, 0x0091);

		///<summary>(0018,0093) VR=DS VM=1 Percent Sampling</summary>
		public readonly static DicomTag PercentSampling = new DicomTag(0x0018, 0x0093);

		///<summary>(0018,0094) VR=DS VM=1 Percent Phase Field of View</summary>
		public readonly static DicomTag PercentPhaseFieldOfView = new DicomTag(0x0018, 0x0094);

		///<summary>(0018,0095) VR=DS VM=1 Pixel Bandwidth</summary>
		public readonly static DicomTag PixelBandwidth = new DicomTag(0x0018, 0x0095);

		///<summary>(0018,1000) VR=LO VM=1 Device Serial Number</summary>
		public readonly static DicomTag DeviceSerialNumber = new DicomTag(0x0018, 0x1000);

		///<summary>(0018,1002) VR=UI VM=1 Device UID</summary>
		public readonly static DicomTag DeviceUID = new DicomTag(0x0018, 0x1002);

		///<summary>(0018,1003) VR=LO VM=1 Device ID</summary>
		public readonly static DicomTag DeviceID = new DicomTag(0x0018, 0x1003);

		///<summary>(0018,1004) VR=LO VM=1 Plate ID</summary>
		public readonly static DicomTag PlateID = new DicomTag(0x0018, 0x1004);

		///<summary>(0018,1005) VR=LO VM=1 Generator ID</summary>
		public readonly static DicomTag GeneratorID = new DicomTag(0x0018, 0x1005);

		///<summary>(0018,1006) VR=LO VM=1 Grid ID</summary>
		public readonly static DicomTag GridID = new DicomTag(0x0018, 0x1006);

		///<summary>(0018,1007) VR=LO VM=1 Cassette ID</summary>
		public readonly static DicomTag CassetteID = new DicomTag(0x0018, 0x1007);

		///<summary>(0018,1008) VR=LO VM=1 Gantry ID</summary>
		public readonly static DicomTag GantryID = new DicomTag(0x0018, 0x1008);

		///<summary>(0018,1010) VR=LO VM=1 Secondary Capture Device ID</summary>
		public readonly static DicomTag SecondaryCaptureDeviceID = new DicomTag(0x0018, 0x1010);

		///<summary>(0018,1011) VR=LO VM=1 Hardcopy Creation Device ID (RETIRED)</summary>
		public readonly static DicomTag HardcopyCreationDeviceIDRETIRED = new DicomTag(0x0018, 0x1011);

		///<summary>(0018,1012) VR=DA VM=1 Date of Secondary Capture</summary>
		public readonly static DicomTag DateOfSecondaryCapture = new DicomTag(0x0018, 0x1012);

		///<summary>(0018,1014) VR=TM VM=1 Time of Secondary Capture</summary>
		public readonly static DicomTag TimeOfSecondaryCapture = new DicomTag(0x0018, 0x1014);

		///<summary>(0018,1016) VR=LO VM=1 Secondary Capture Device Manufacturer</summary>
		public readonly static DicomTag SecondaryCaptureDeviceManufacturer = new DicomTag(0x0018, 0x1016);

		///<summary>(0018,1017) VR=LO VM=1 Hardcopy Device Manufacturer (RETIRED)</summary>
		public readonly static DicomTag HardcopyDeviceManufacturerRETIRED = new DicomTag(0x0018, 0x1017);

		///<summary>(0018,1018) VR=LO VM=1 Secondary Capture Device Manufacturer’s Model Name</summary>
		public readonly static DicomTag SecondaryCaptureDeviceManufacturerModelName = new DicomTag(0x0018, 0x1018);

		///<summary>(0018,1019) VR=LO VM=1-n Secondary Capture Device Software Versions</summary>
		public readonly static DicomTag SecondaryCaptureDeviceSoftwareVersions = new DicomTag(0x0018, 0x1019);

		///<summary>(0018,101a) VR=LO VM=1-n Hardcopy Device Software Version (RETIRED)</summary>
		public readonly static DicomTag HardcopyDeviceSoftwareVersionRETIRED = new DicomTag(0x0018, 0x101a);

		///<summary>(0018,101b) VR=LO VM=1 Hardcopy Device Manufacturer’s Model Name (RETIRED)</summary>
		public readonly static DicomTag HardcopyDeviceManufacturerModelNameRETIRED = new DicomTag(0x0018, 0x101b);

		///<summary>(0018,1020) VR=LO VM=1-n Software Version(s)</summary>
		public readonly static DicomTag SoftwareVersions = new DicomTag(0x0018, 0x1020);

		///<summary>(0018,1022) VR=SH VM=1 Video Image Format Acquired</summary>
		public readonly static DicomTag VideoImageFormatAcquired = new DicomTag(0x0018, 0x1022);

		///<summary>(0018,1023) VR=LO VM=1 Digital Image Format Acquired</summary>
		public readonly static DicomTag DigitalImageFormatAcquired = new DicomTag(0x0018, 0x1023);

		///<summary>(0018,1030) VR=LO VM=1 Protocol Name</summary>
		public readonly static DicomTag ProtocolName = new DicomTag(0x0018, 0x1030);

		///<summary>(0018,1040) VR=LO VM=1 Contrast/Bolus Route</summary>
		public readonly static DicomTag ContrastBolusRoute = new DicomTag(0x0018, 0x1040);

		///<summary>(0018,1041) VR=DS VM=1 Contrast/Bolus Volume</summary>
		public readonly static DicomTag ContrastBolusVolume = new DicomTag(0x0018, 0x1041);

		///<summary>(0018,1042) VR=TM VM=1 Contrast/Bolus Start Time</summary>
		public readonly static DicomTag ContrastBolusStartTime = new DicomTag(0x0018, 0x1042);

		///<summary>(0018,1043) VR=TM VM=1 Contrast/Bolus Stop Time</summary>
		public readonly static DicomTag ContrastBolusStopTime = new DicomTag(0x0018, 0x1043);

		///<summary>(0018,1044) VR=DS VM=1 Contrast/Bolus Total Dose</summary>
		public readonly static DicomTag ContrastBolusTotalDose = new DicomTag(0x0018, 0x1044);

		///<summary>(0018,1045) VR=IS VM=1 Syringe Counts</summary>
		public readonly static DicomTag SyringeCounts = new DicomTag(0x0018, 0x1045);

		///<summary>(0018,1046) VR=DS VM=1-n Contrast Flow Rate</summary>
		public readonly static DicomTag ContrastFlowRate = new DicomTag(0x0018, 0x1046);

		///<summary>(0018,1047) VR=DS VM=1-n Contrast Flow Duration</summary>
		public readonly static DicomTag ContrastFlowDuration = new DicomTag(0x0018, 0x1047);

		///<summary>(0018,1048) VR=CS VM=1 Contrast/Bolus Ingredient</summary>
		public readonly static DicomTag ContrastBolusIngredient = new DicomTag(0x0018, 0x1048);

		///<summary>(0018,1049) VR=DS VM=1 Contrast/Bolus Ingredient Concentration</summary>
		public readonly static DicomTag ContrastBolusIngredientConcentration = new DicomTag(0x0018, 0x1049);

		///<summary>(0018,1050) VR=DS VM=1 Spatial Resolution</summary>
		public readonly static DicomTag SpatialResolution = new DicomTag(0x0018, 0x1050);

		///<summary>(0018,1060) VR=DS VM=1 Trigger Time</summary>
		public readonly static DicomTag TriggerTime = new DicomTag(0x0018, 0x1060);

		///<summary>(0018,1061) VR=LO VM=1 Trigger Source or Type</summary>
		public readonly static DicomTag TriggerSourceOrType = new DicomTag(0x0018, 0x1061);

		///<summary>(0018,1062) VR=IS VM=1 Nominal Interval</summary>
		public readonly static DicomTag NominalInterval = new DicomTag(0x0018, 0x1062);

		///<summary>(0018,1063) VR=DS VM=1 Frame Time</summary>
		public readonly static DicomTag FrameTime = new DicomTag(0x0018, 0x1063);

		///<summary>(0018,1064) VR=LO VM=1 Cardiac Framing Type</summary>
		public readonly static DicomTag CardiacFramingType = new DicomTag(0x0018, 0x1064);

		///<summary>(0018,1065) VR=DS VM=1-n Frame Time Vector</summary>
		public readonly static DicomTag FrameTimeVector = new DicomTag(0x0018, 0x1065);

		///<summary>(0018,1066) VR=DS VM=1 Frame Delay</summary>
		public readonly static DicomTag FrameDelay = new DicomTag(0x0018, 0x1066);

		///<summary>(0018,1067) VR=DS VM=1 Image Trigger Delay</summary>
		public readonly static DicomTag ImageTriggerDelay = new DicomTag(0x0018, 0x1067);

		///<summary>(0018,1068) VR=DS VM=1 Multiplex Group Time Offset</summary>
		public readonly static DicomTag MultiplexGroupTimeOffset = new DicomTag(0x0018, 0x1068);

		///<summary>(0018,1069) VR=DS VM=1 Trigger Time Offset</summary>
		public readonly static DicomTag TriggerTimeOffset = new DicomTag(0x0018, 0x1069);

		///<summary>(0018,106a) VR=CS VM=1 Synchronization Trigger</summary>
		public readonly static DicomTag SynchronizationTrigger = new DicomTag(0x0018, 0x106a);

		///<summary>(0018,106c) VR=US VM=2 Synchronization Channel</summary>
		public readonly static DicomTag SynchronizationChannel = new DicomTag(0x0018, 0x106c);

		///<summary>(0018,106e) VR=UL VM=1 Trigger Sample Position</summary>
		public readonly static DicomTag TriggerSamplePosition = new DicomTag(0x0018, 0x106e);

		///<summary>(0018,1070) VR=LO VM=1 Radiopharmaceutical Route</summary>
		public readonly static DicomTag RadiopharmaceuticalRoute = new DicomTag(0x0018, 0x1070);

		///<summary>(0018,1071) VR=DS VM=1 Radiopharmaceutical Volume</summary>
		public readonly static DicomTag RadiopharmaceuticalVolume = new DicomTag(0x0018, 0x1071);

		///<summary>(0018,1072) VR=TM VM=1 Radiopharmaceutical Start Time</summary>
		public readonly static DicomTag RadiopharmaceuticalStartTime = new DicomTag(0x0018, 0x1072);

		///<summary>(0018,1073) VR=TM VM=1 Radiopharmaceutical Stop Time</summary>
		public readonly static DicomTag RadiopharmaceuticalStopTime = new DicomTag(0x0018, 0x1073);

		///<summary>(0018,1074) VR=DS VM=1 Radionuclide Total Dose</summary>
		public readonly static DicomTag RadionuclideTotalDose = new DicomTag(0x0018, 0x1074);

		///<summary>(0018,1075) VR=DS VM=1 Radionuclide Half Life</summary>
		public readonly static DicomTag RadionuclideHalfLife = new DicomTag(0x0018, 0x1075);

		///<summary>(0018,1076) VR=DS VM=1 Radionuclide Positron Fraction</summary>
		public readonly static DicomTag RadionuclidePositronFraction = new DicomTag(0x0018, 0x1076);

		///<summary>(0018,1077) VR=DS VM=1 Radiopharmaceutical Specific Activity</summary>
		public readonly static DicomTag RadiopharmaceuticalSpecificActivity = new DicomTag(0x0018, 0x1077);

		///<summary>(0018,1078) VR=DT VM=1 Radiopharmaceutical Start DateTime</summary>
		public readonly static DicomTag RadiopharmaceuticalStartDateTime = new DicomTag(0x0018, 0x1078);

		///<summary>(0018,1079) VR=DT VM=1 Radiopharmaceutical Stop DateTime</summary>
		public readonly static DicomTag RadiopharmaceuticalStopDateTime = new DicomTag(0x0018, 0x1079);

		///<summary>(0018,1080) VR=CS VM=1 Beat Rejection Flag</summary>
		public readonly static DicomTag BeatRejectionFlag = new DicomTag(0x0018, 0x1080);

		///<summary>(0018,1081) VR=IS VM=1 Low R-R Value</summary>
		public readonly static DicomTag LowRRValue = new DicomTag(0x0018, 0x1081);

		///<summary>(0018,1082) VR=IS VM=1 High R-R Value</summary>
		public readonly static DicomTag HighRRValue = new DicomTag(0x0018, 0x1082);

		///<summary>(0018,1083) VR=IS VM=1 Intervals Acquired</summary>
		public readonly static DicomTag IntervalsAcquired = new DicomTag(0x0018, 0x1083);

		///<summary>(0018,1084) VR=IS VM=1 Intervals Rejected</summary>
		public readonly static DicomTag IntervalsRejected = new DicomTag(0x0018, 0x1084);

		///<summary>(0018,1085) VR=LO VM=1 PVC Rejection</summary>
		public readonly static DicomTag PVCRejection = new DicomTag(0x0018, 0x1085);

		///<summary>(0018,1086) VR=IS VM=1 Skip Beats</summary>
		public readonly static DicomTag SkipBeats = new DicomTag(0x0018, 0x1086);

		///<summary>(0018,1088) VR=IS VM=1 Heart Rate</summary>
		public readonly static DicomTag HeartRate = new DicomTag(0x0018, 0x1088);

		///<summary>(0018,1090) VR=IS VM=1 Cardiac Number of Images</summary>
		public readonly static DicomTag CardiacNumberOfImages = new DicomTag(0x0018, 0x1090);

		///<summary>(0018,1094) VR=IS VM=1 Trigger Window</summary>
		public readonly static DicomTag TriggerWindow = new DicomTag(0x0018, 0x1094);

		///<summary>(0018,1100) VR=DS VM=1 Reconstruction Diameter</summary>
		public readonly static DicomTag ReconstructionDiameter = new DicomTag(0x0018, 0x1100);

		///<summary>(0018,1110) VR=DS VM=1 Distance Source to Detector</summary>
		public readonly static DicomTag DistanceSourceToDetector = new DicomTag(0x0018, 0x1110);

		///<summary>(0018,1111) VR=DS VM=1 Distance Source to Patient</summary>
		public readonly static DicomTag DistanceSourceToPatient = new DicomTag(0x0018, 0x1111);

		///<summary>(0018,1114) VR=DS VM=1 Estimated Radiographic Magnification Factor</summary>
		public readonly static DicomTag EstimatedRadiographicMagnificationFactor = new DicomTag(0x0018, 0x1114);

		///<summary>(0018,1120) VR=DS VM=1 Gantry/Detector Tilt</summary>
		public readonly static DicomTag GantryDetectorTilt = new DicomTag(0x0018, 0x1120);

		///<summary>(0018,1121) VR=DS VM=1 Gantry/Detector Slew</summary>
		public readonly static DicomTag GantryDetectorSlew = new DicomTag(0x0018, 0x1121);

		///<summary>(0018,1130) VR=DS VM=1 Table Height</summary>
		public readonly static DicomTag TableHeight = new DicomTag(0x0018, 0x1130);

		///<summary>(0018,1131) VR=DS VM=1 Table Traverse</summary>
		public readonly static DicomTag TableTraverse = new DicomTag(0x0018, 0x1131);

		///<summary>(0018,1134) VR=CS VM=1 Table Motion</summary>
		public readonly static DicomTag TableMotion = new DicomTag(0x0018, 0x1134);

		///<summary>(0018,1135) VR=DS VM=1-n Table Vertical Increment</summary>
		public readonly static DicomTag TableVerticalIncrement = new DicomTag(0x0018, 0x1135);

		///<summary>(0018,1136) VR=DS VM=1-n Table Lateral Increment</summary>
		public readonly static DicomTag TableLateralIncrement = new DicomTag(0x0018, 0x1136);

		///<summary>(0018,1137) VR=DS VM=1-n Table Longitudinal Increment</summary>
		public readonly static DicomTag TableLongitudinalIncrement = new DicomTag(0x0018, 0x1137);

		///<summary>(0018,1138) VR=DS VM=1 Table Angle</summary>
		public readonly static DicomTag TableAngle = new DicomTag(0x0018, 0x1138);

		///<summary>(0018,113a) VR=CS VM=1 Table Type</summary>
		public readonly static DicomTag TableType = new DicomTag(0x0018, 0x113a);

		///<summary>(0018,1140) VR=CS VM=1 Rotation Direction</summary>
		public readonly static DicomTag RotationDirection = new DicomTag(0x0018, 0x1140);

		///<summary>(0018,1141) VR=DS VM=1 Angular Position (RETIRED)</summary>
		public readonly static DicomTag AngularPositionRETIRED = new DicomTag(0x0018, 0x1141);

		///<summary>(0018,1142) VR=DS VM=1-n Radial Position</summary>
		public readonly static DicomTag RadialPosition = new DicomTag(0x0018, 0x1142);

		///<summary>(0018,1143) VR=DS VM=1 Scan Arc</summary>
		public readonly static DicomTag ScanArc = new DicomTag(0x0018, 0x1143);

		///<summary>(0018,1144) VR=DS VM=1 Angular Step</summary>
		public readonly static DicomTag AngularStep = new DicomTag(0x0018, 0x1144);

		///<summary>(0018,1145) VR=DS VM=1 Center of Rotation Offset</summary>
		public readonly static DicomTag CenterOfRotationOffset = new DicomTag(0x0018, 0x1145);

		///<summary>(0018,1146) VR=DS VM=1-n Rotation Offset (RETIRED)</summary>
		public readonly static DicomTag RotationOffsetRETIRED = new DicomTag(0x0018, 0x1146);

		///<summary>(0018,1147) VR=CS VM=1 Field of View Shape</summary>
		public readonly static DicomTag FieldOfViewShape = new DicomTag(0x0018, 0x1147);

		///<summary>(0018,1149) VR=IS VM=1-2 Field of View Dimension(s)</summary>
		public readonly static DicomTag FieldOfViewDimensions = new DicomTag(0x0018, 0x1149);

		///<summary>(0018,1150) VR=IS VM=1 Exposure Time</summary>
		public readonly static DicomTag ExposureTime = new DicomTag(0x0018, 0x1150);

		///<summary>(0018,1151) VR=IS VM=1 X-Ray Tube Current</summary>
		public readonly static DicomTag XRayTubeCurrent = new DicomTag(0x0018, 0x1151);

		///<summary>(0018,1152) VR=IS VM=1 Exposure</summary>
		public readonly static DicomTag Exposure = new DicomTag(0x0018, 0x1152);

		///<summary>(0018,1153) VR=IS VM=1 Exposure in µAs</summary>
		public readonly static DicomTag ExposureInuAs = new DicomTag(0x0018, 0x1153);

		///<summary>(0018,1154) VR=DS VM=1 Average Pulse Width</summary>
		public readonly static DicomTag AveragePulseWidth = new DicomTag(0x0018, 0x1154);

		///<summary>(0018,1155) VR=CS VM=1 Radiation Setting</summary>
		public readonly static DicomTag RadiationSetting = new DicomTag(0x0018, 0x1155);

		///<summary>(0018,1156) VR=CS VM=1 Rectification Type</summary>
		public readonly static DicomTag RectificationType = new DicomTag(0x0018, 0x1156);

		///<summary>(0018,115a) VR=CS VM=1 Radiation Mode</summary>
		public readonly static DicomTag RadiationMode = new DicomTag(0x0018, 0x115a);

		///<summary>(0018,115e) VR=DS VM=1 Image and Fluoroscopy Area Dose Product</summary>
		public readonly static DicomTag ImageAndFluoroscopyAreaDoseProduct = new DicomTag(0x0018, 0x115e);

		///<summary>(0018,1160) VR=SH VM=1 Filter Type</summary>
		public readonly static DicomTag FilterType = new DicomTag(0x0018, 0x1160);

		///<summary>(0018,1161) VR=LO VM=1-n Type of Filters</summary>
		public readonly static DicomTag TypeOfFilters = new DicomTag(0x0018, 0x1161);

		///<summary>(0018,1162) VR=DS VM=1 Intensifier Size</summary>
		public readonly static DicomTag IntensifierSize = new DicomTag(0x0018, 0x1162);

		///<summary>(0018,1164) VR=DS VM=2 Imager Pixel Spacing</summary>
		public readonly static DicomTag ImagerPixelSpacing = new DicomTag(0x0018, 0x1164);

		///<summary>(0018,1166) VR=CS VM=1-n Grid</summary>
		public readonly static DicomTag Grid = new DicomTag(0x0018, 0x1166);

		///<summary>(0018,1170) VR=IS VM=1 Generator Power</summary>
		public readonly static DicomTag GeneratorPower = new DicomTag(0x0018, 0x1170);

		///<summary>(0018,1180) VR=SH VM=1 Collimator/grid Name</summary>
		public readonly static DicomTag CollimatorGridName = new DicomTag(0x0018, 0x1180);

		///<summary>(0018,1181) VR=CS VM=1 Collimator Type</summary>
		public readonly static DicomTag CollimatorType = new DicomTag(0x0018, 0x1181);

		///<summary>(0018,1182) VR=IS VM=1-2 Focal Distance</summary>
		public readonly static DicomTag FocalDistance = new DicomTag(0x0018, 0x1182);

		///<summary>(0018,1183) VR=DS VM=1-2 X Focus Center</summary>
		public readonly static DicomTag XFocusCenter = new DicomTag(0x0018, 0x1183);

		///<summary>(0018,1184) VR=DS VM=1-2 Y Focus Center</summary>
		public readonly static DicomTag YFocusCenter = new DicomTag(0x0018, 0x1184);

		///<summary>(0018,1190) VR=DS VM=1-n Focal Spot(s)</summary>
		public readonly static DicomTag FocalSpots = new DicomTag(0x0018, 0x1190);

		///<summary>(0018,1191) VR=CS VM=1 Anode Target Material</summary>
		public readonly static DicomTag AnodeTargetMaterial = new DicomTag(0x0018, 0x1191);

		///<summary>(0018,11a0) VR=DS VM=1 Body Part Thickness</summary>
		public readonly static DicomTag BodyPartThickness = new DicomTag(0x0018, 0x11a0);

		///<summary>(0018,11a2) VR=DS VM=1 Compression Force</summary>
		public readonly static DicomTag CompressionForce = new DicomTag(0x0018, 0x11a2);

		///<summary>(0018,1200) VR=DA VM=1-n Date of Last Calibration</summary>
		public readonly static DicomTag DateOfLastCalibration = new DicomTag(0x0018, 0x1200);

		///<summary>(0018,1201) VR=TM VM=1-n Time of Last Calibration</summary>
		public readonly static DicomTag TimeOfLastCalibration = new DicomTag(0x0018, 0x1201);

		///<summary>(0018,1210) VR=SH VM=1-n Convolution Kernel</summary>
		public readonly static DicomTag ConvolutionKernel = new DicomTag(0x0018, 0x1210);

		///<summary>(0018,1240) VR=IS VM=1-n Upper/Lower Pixel Values (RETIRED)</summary>
		public readonly static DicomTag UpperLowerPixelValuesRETIRED = new DicomTag(0x0018, 0x1240);

		///<summary>(0018,1242) VR=IS VM=1 Actual Frame Duration</summary>
		public readonly static DicomTag ActualFrameDuration = new DicomTag(0x0018, 0x1242);

		///<summary>(0018,1243) VR=IS VM=1 Count Rate</summary>
		public readonly static DicomTag CountRate = new DicomTag(0x0018, 0x1243);

		///<summary>(0018,1244) VR=US VM=1 Preferred Playback Sequencing</summary>
		public readonly static DicomTag PreferredPlaybackSequencing = new DicomTag(0x0018, 0x1244);

		///<summary>(0018,1250) VR=SH VM=1 Receive Coil Name</summary>
		public readonly static DicomTag ReceiveCoilName = new DicomTag(0x0018, 0x1250);

		///<summary>(0018,1251) VR=SH VM=1 Transmit Coil Name</summary>
		public readonly static DicomTag TransmitCoilName = new DicomTag(0x0018, 0x1251);

		///<summary>(0018,1260) VR=SH VM=1 Plate Type</summary>
		public readonly static DicomTag PlateType = new DicomTag(0x0018, 0x1260);

		///<summary>(0018,1261) VR=LO VM=1 Phosphor Type</summary>
		public readonly static DicomTag PhosphorType = new DicomTag(0x0018, 0x1261);

		///<summary>(0018,1300) VR=DS VM=1 Scan Velocity</summary>
		public readonly static DicomTag ScanVelocity = new DicomTag(0x0018, 0x1300);

		///<summary>(0018,1301) VR=CS VM=1-n Whole Body Technique</summary>
		public readonly static DicomTag WholeBodyTechnique = new DicomTag(0x0018, 0x1301);

		///<summary>(0018,1302) VR=IS VM=1 Scan Length</summary>
		public readonly static DicomTag ScanLength = new DicomTag(0x0018, 0x1302);

		///<summary>(0018,1310) VR=US VM=4 Acquisition Matrix</summary>
		public readonly static DicomTag AcquisitionMatrix = new DicomTag(0x0018, 0x1310);

		///<summary>(0018,1312) VR=CS VM=1 In-plane Phase Encoding Direction</summary>
		public readonly static DicomTag InPlanePhaseEncodingDirection = new DicomTag(0x0018, 0x1312);

		///<summary>(0018,1314) VR=DS VM=1 Flip Angle</summary>
		public readonly static DicomTag FlipAngle = new DicomTag(0x0018, 0x1314);

		///<summary>(0018,1315) VR=CS VM=1 Variable Flip Angle Flag</summary>
		public readonly static DicomTag VariableFlipAngleFlag = new DicomTag(0x0018, 0x1315);

		///<summary>(0018,1316) VR=DS VM=1 SAR</summary>
		public readonly static DicomTag SAR = new DicomTag(0x0018, 0x1316);

		///<summary>(0018,1318) VR=DS VM=1 dB/dt</summary>
		public readonly static DicomTag dBdt = new DicomTag(0x0018, 0x1318);

		///<summary>(0018,1400) VR=LO VM=1 Acquisition Device Processing Description</summary>
		public readonly static DicomTag AcquisitionDeviceProcessingDescription = new DicomTag(0x0018, 0x1400);

		///<summary>(0018,1401) VR=LO VM=1 Acquisition Device Processing Code</summary>
		public readonly static DicomTag AcquisitionDeviceProcessingCode = new DicomTag(0x0018, 0x1401);

		///<summary>(0018,1402) VR=CS VM=1 Cassette Orientation</summary>
		public readonly static DicomTag CassetteOrientation = new DicomTag(0x0018, 0x1402);

		///<summary>(0018,1403) VR=CS VM=1 Cassette Size</summary>
		public readonly static DicomTag CassetteSize = new DicomTag(0x0018, 0x1403);

		///<summary>(0018,1404) VR=US VM=1 Exposures on Plate</summary>
		public readonly static DicomTag ExposuresOnPlate = new DicomTag(0x0018, 0x1404);

		///<summary>(0018,1405) VR=IS VM=1 Relative X-Ray Exposure</summary>
		public readonly static DicomTag RelativeXRayExposure = new DicomTag(0x0018, 0x1405);

		///<summary>(0018,1411) VR=DS VM=1 Exposure Index</summary>
		public readonly static DicomTag ExposureIndex = new DicomTag(0x0018, 0x1411);

		///<summary>(0018,1412) VR=DS VM=1 Target Exposure Index</summary>
		public readonly static DicomTag TargetExposureIndex = new DicomTag(0x0018, 0x1412);

		///<summary>(0018,1413) VR=DS VM=1 Deviation Index</summary>
		public readonly static DicomTag DeviationIndex = new DicomTag(0x0018, 0x1413);

		///<summary>(0018,1450) VR=DS VM=1 Column Angulation</summary>
		public readonly static DicomTag ColumnAngulation = new DicomTag(0x0018, 0x1450);

		///<summary>(0018,1460) VR=DS VM=1 Tomo Layer Height</summary>
		public readonly static DicomTag TomoLayerHeight = new DicomTag(0x0018, 0x1460);

		///<summary>(0018,1470) VR=DS VM=1 Tomo Angle</summary>
		public readonly static DicomTag TomoAngle = new DicomTag(0x0018, 0x1470);

		///<summary>(0018,1480) VR=DS VM=1 Tomo Time</summary>
		public readonly static DicomTag TomoTime = new DicomTag(0x0018, 0x1480);

		///<summary>(0018,1490) VR=CS VM=1 Tomo Type</summary>
		public readonly static DicomTag TomoType = new DicomTag(0x0018, 0x1490);

		///<summary>(0018,1491) VR=CS VM=1 Tomo Class</summary>
		public readonly static DicomTag TomoClass = new DicomTag(0x0018, 0x1491);

		///<summary>(0018,1495) VR=IS VM=1 Number of Tomosynthesis Source Images</summary>
		public readonly static DicomTag NumberOfTomosynthesisSourceImages = new DicomTag(0x0018, 0x1495);

		///<summary>(0018,1500) VR=CS VM=1 Positioner Motion</summary>
		public readonly static DicomTag PositionerMotion = new DicomTag(0x0018, 0x1500);

		///<summary>(0018,1508) VR=CS VM=1 Positioner Type</summary>
		public readonly static DicomTag PositionerType = new DicomTag(0x0018, 0x1508);

		///<summary>(0018,1510) VR=DS VM=1 Positioner Primary Angle</summary>
		public readonly static DicomTag PositionerPrimaryAngle = new DicomTag(0x0018, 0x1510);

		///<summary>(0018,1511) VR=DS VM=1 Positioner Secondary Angle</summary>
		public readonly static DicomTag PositionerSecondaryAngle = new DicomTag(0x0018, 0x1511);

		///<summary>(0018,1520) VR=DS VM=1-n Positioner Primary Angle Increment</summary>
		public readonly static DicomTag PositionerPrimaryAngleIncrement = new DicomTag(0x0018, 0x1520);

		///<summary>(0018,1521) VR=DS VM=1-n Positioner Secondary Angle Increment</summary>
		public readonly static DicomTag PositionerSecondaryAngleIncrement = new DicomTag(0x0018, 0x1521);

		///<summary>(0018,1530) VR=DS VM=1 Detector Primary Angle</summary>
		public readonly static DicomTag DetectorPrimaryAngle = new DicomTag(0x0018, 0x1530);

		///<summary>(0018,1531) VR=DS VM=1 Detector Secondary Angle</summary>
		public readonly static DicomTag DetectorSecondaryAngle = new DicomTag(0x0018, 0x1531);

		///<summary>(0018,1600) VR=CS VM=1-3 Shutter Shape</summary>
		public readonly static DicomTag ShutterShape = new DicomTag(0x0018, 0x1600);

		///<summary>(0018,1602) VR=IS VM=1 Shutter Left Vertical Edge</summary>
		public readonly static DicomTag ShutterLeftVerticalEdge = new DicomTag(0x0018, 0x1602);

		///<summary>(0018,1604) VR=IS VM=1 Shutter Right Vertical Edge</summary>
		public readonly static DicomTag ShutterRightVerticalEdge = new DicomTag(0x0018, 0x1604);

		///<summary>(0018,1606) VR=IS VM=1 Shutter Upper Horizontal Edge</summary>
		public readonly static DicomTag ShutterUpperHorizontalEdge = new DicomTag(0x0018, 0x1606);

		///<summary>(0018,1608) VR=IS VM=1 Shutter Lower Horizontal Edge</summary>
		public readonly static DicomTag ShutterLowerHorizontalEdge = new DicomTag(0x0018, 0x1608);

		///<summary>(0018,1610) VR=IS VM=2 Center of Circular Shutter</summary>
		public readonly static DicomTag CenterOfCircularShutter = new DicomTag(0x0018, 0x1610);

		///<summary>(0018,1612) VR=IS VM=1 Radius of Circular Shutter</summary>
		public readonly static DicomTag RadiusOfCircularShutter = new DicomTag(0x0018, 0x1612);

		///<summary>(0018,1620) VR=IS VM=2-2n Vertices of the Polygonal Shutter</summary>
		public readonly static DicomTag VerticesOfThePolygonalShutter = new DicomTag(0x0018, 0x1620);

		///<summary>(0018,1622) VR=US VM=1 Shutter Presentation Value</summary>
		public readonly static DicomTag ShutterPresentationValue = new DicomTag(0x0018, 0x1622);

		///<summary>(0018,1623) VR=US VM=1 Shutter Overlay Group</summary>
		public readonly static DicomTag ShutterOverlayGroup = new DicomTag(0x0018, 0x1623);

		///<summary>(0018,1624) VR=US VM=3 Shutter Presentation Color CIELab Value</summary>
		public readonly static DicomTag ShutterPresentationColorCIELabValue = new DicomTag(0x0018, 0x1624);

		///<summary>(0018,1700) VR=CS VM=1-3 Collimator Shape</summary>
		public readonly static DicomTag CollimatorShape = new DicomTag(0x0018, 0x1700);

		///<summary>(0018,1702) VR=IS VM=1 Collimator Left Vertical Edge</summary>
		public readonly static DicomTag CollimatorLeftVerticalEdge = new DicomTag(0x0018, 0x1702);

		///<summary>(0018,1704) VR=IS VM=1 Collimator Right Vertical Edge</summary>
		public readonly static DicomTag CollimatorRightVerticalEdge = new DicomTag(0x0018, 0x1704);

		///<summary>(0018,1706) VR=IS VM=1 Collimator Upper Horizontal Edge</summary>
		public readonly static DicomTag CollimatorUpperHorizontalEdge = new DicomTag(0x0018, 0x1706);

		///<summary>(0018,1708) VR=IS VM=1 Collimator Lower Horizontal Edge</summary>
		public readonly static DicomTag CollimatorLowerHorizontalEdge = new DicomTag(0x0018, 0x1708);

		///<summary>(0018,1710) VR=IS VM=2 Center of Circular Collimator</summary>
		public readonly static DicomTag CenterOfCircularCollimator = new DicomTag(0x0018, 0x1710);

		///<summary>(0018,1712) VR=IS VM=1 Radius of Circular Collimator</summary>
		public readonly static DicomTag RadiusOfCircularCollimator = new DicomTag(0x0018, 0x1712);

		///<summary>(0018,1720) VR=IS VM=2-2n Vertices of the Polygonal Collimator</summary>
		public readonly static DicomTag VerticesOfThePolygonalCollimator = new DicomTag(0x0018, 0x1720);

		///<summary>(0018,1800) VR=CS VM=1 Acquisition Time Synchronized</summary>
		public readonly static DicomTag AcquisitionTimeSynchronized = new DicomTag(0x0018, 0x1800);

		///<summary>(0018,1801) VR=SH VM=1 Time Source</summary>
		public readonly static DicomTag TimeSource = new DicomTag(0x0018, 0x1801);

		///<summary>(0018,1802) VR=CS VM=1 Time Distribution Protocol</summary>
		public readonly static DicomTag TimeDistributionProtocol = new DicomTag(0x0018, 0x1802);

		///<summary>(0018,1803) VR=LO VM=1 NTP Source Address</summary>
		public readonly static DicomTag NTPSourceAddress = new DicomTag(0x0018, 0x1803);

		///<summary>(0018,2001) VR=IS VM=1-n Page Number Vector</summary>
		public readonly static DicomTag PageNumberVector = new DicomTag(0x0018, 0x2001);

		///<summary>(0018,2002) VR=SH VM=1-n Frame Label Vector</summary>
		public readonly static DicomTag FrameLabelVector = new DicomTag(0x0018, 0x2002);

		///<summary>(0018,2003) VR=DS VM=1-n Frame Primary Angle Vector</summary>
		public readonly static DicomTag FramePrimaryAngleVector = new DicomTag(0x0018, 0x2003);

		///<summary>(0018,2004) VR=DS VM=1-n Frame Secondary Angle Vector</summary>
		public readonly static DicomTag FrameSecondaryAngleVector = new DicomTag(0x0018, 0x2004);

		///<summary>(0018,2005) VR=DS VM=1-n Slice Location Vector</summary>
		public readonly static DicomTag SliceLocationVector = new DicomTag(0x0018, 0x2005);

		///<summary>(0018,2006) VR=SH VM=1-n Display Window Label Vector</summary>
		public readonly static DicomTag DisplayWindowLabelVector = new DicomTag(0x0018, 0x2006);

		///<summary>(0018,2010) VR=DS VM=2 Nominal Scanned Pixel Spacing</summary>
		public readonly static DicomTag NominalScannedPixelSpacing = new DicomTag(0x0018, 0x2010);

		///<summary>(0018,2020) VR=CS VM=1 Digitizing Device Transport Direction</summary>
		public readonly static DicomTag DigitizingDeviceTransportDirection = new DicomTag(0x0018, 0x2020);

		///<summary>(0018,2030) VR=DS VM=1 Rotation of Scanned Film</summary>
		public readonly static DicomTag RotationOfScannedFilm = new DicomTag(0x0018, 0x2030);

		///<summary>(0018,3100) VR=CS VM=1 IVUS Acquisition</summary>
		public readonly static DicomTag IVUSAcquisition = new DicomTag(0x0018, 0x3100);

		///<summary>(0018,3101) VR=DS VM=1 IVUS Pullback Rate</summary>
		public readonly static DicomTag IVUSPullbackRate = new DicomTag(0x0018, 0x3101);

		///<summary>(0018,3102) VR=DS VM=1 IVUS Gated Rate</summary>
		public readonly static DicomTag IVUSGatedRate = new DicomTag(0x0018, 0x3102);

		///<summary>(0018,3103) VR=IS VM=1 IVUS Pullback Start Frame Number</summary>
		public readonly static DicomTag IVUSPullbackStartFrameNumber = new DicomTag(0x0018, 0x3103);

		///<summary>(0018,3104) VR=IS VM=1 IVUS Pullback Stop Frame Number</summary>
		public readonly static DicomTag IVUSPullbackStopFrameNumber = new DicomTag(0x0018, 0x3104);

		///<summary>(0018,3105) VR=IS VM=1-n Lesion Number</summary>
		public readonly static DicomTag LesionNumber = new DicomTag(0x0018, 0x3105);

		///<summary>(0018,4000) VR=LT VM=1 Acquisition Comments (RETIRED)</summary>
		public readonly static DicomTag AcquisitionCommentsRETIRED = new DicomTag(0x0018, 0x4000);

		///<summary>(0018,5000) VR=SH VM=1-n Output Power</summary>
		public readonly static DicomTag OutputPower = new DicomTag(0x0018, 0x5000);

		///<summary>(0018,5010) VR=LO VM=1-n Transducer Data</summary>
		public readonly static DicomTag TransducerData = new DicomTag(0x0018, 0x5010);

		///<summary>(0018,5012) VR=DS VM=1 Focus Depth</summary>
		public readonly static DicomTag FocusDepth = new DicomTag(0x0018, 0x5012);

		///<summary>(0018,5020) VR=LO VM=1 Processing Function</summary>
		public readonly static DicomTag ProcessingFunction = new DicomTag(0x0018, 0x5020);

		///<summary>(0018,5021) VR=LO VM=1 Postprocessing Function (RETIRED)</summary>
		public readonly static DicomTag PostprocessingFunctionRETIRED = new DicomTag(0x0018, 0x5021);

		///<summary>(0018,5022) VR=DS VM=1 Mechanical Index</summary>
		public readonly static DicomTag MechanicalIndex = new DicomTag(0x0018, 0x5022);

		///<summary>(0018,5024) VR=DS VM=1 Bone Thermal Index</summary>
		public readonly static DicomTag BoneThermalIndex = new DicomTag(0x0018, 0x5024);

		///<summary>(0018,5026) VR=DS VM=1 Cranial Thermal Index</summary>
		public readonly static DicomTag CranialThermalIndex = new DicomTag(0x0018, 0x5026);

		///<summary>(0018,5027) VR=DS VM=1 Soft Tissue Thermal Index</summary>
		public readonly static DicomTag SoftTissueThermalIndex = new DicomTag(0x0018, 0x5027);

		///<summary>(0018,5028) VR=DS VM=1 Soft Tissue-focus Thermal Index</summary>
		public readonly static DicomTag SoftTissueFocusThermalIndex = new DicomTag(0x0018, 0x5028);

		///<summary>(0018,5029) VR=DS VM=1 Soft Tissue-surface Thermal Index</summary>
		public readonly static DicomTag SoftTissueSurfaceThermalIndex = new DicomTag(0x0018, 0x5029);

		///<summary>(0018,5030) VR=DS VM=1 Dynamic Range (RETIRED)</summary>
		public readonly static DicomTag DynamicRangeRETIRED = new DicomTag(0x0018, 0x5030);

		///<summary>(0018,5040) VR=DS VM=1 Total Gain (RETIRED)</summary>
		public readonly static DicomTag TotalGainRETIRED = new DicomTag(0x0018, 0x5040);

		///<summary>(0018,5050) VR=IS VM=1 Depth of Scan Field</summary>
		public readonly static DicomTag DepthOfScanField = new DicomTag(0x0018, 0x5050);

		///<summary>(0018,5100) VR=CS VM=1 Patient Position</summary>
		public readonly static DicomTag PatientPosition = new DicomTag(0x0018, 0x5100);

		///<summary>(0018,5101) VR=CS VM=1 View Position</summary>
		public readonly static DicomTag ViewPosition = new DicomTag(0x0018, 0x5101);

		///<summary>(0018,5104) VR=SQ VM=1 Projection Eponymous Name Code Sequence</summary>
		public readonly static DicomTag ProjectionEponymousNameCodeSequence = new DicomTag(0x0018, 0x5104);

		///<summary>(0018,5210) VR=DS VM=6 Image Transformation Matrix (RETIRED)</summary>
		public readonly static DicomTag ImageTransformationMatrixRETIRED = new DicomTag(0x0018, 0x5210);

		///<summary>(0018,5212) VR=DS VM=3 Image Translation Vector (RETIRED)</summary>
		public readonly static DicomTag ImageTranslationVectorRETIRED = new DicomTag(0x0018, 0x5212);

		///<summary>(0018,6000) VR=DS VM=1 Sensitivity</summary>
		public readonly static DicomTag Sensitivity = new DicomTag(0x0018, 0x6000);

		///<summary>(0018,6011) VR=SQ VM=1 Sequence of Ultrasound Regions</summary>
		public readonly static DicomTag SequenceOfUltrasoundRegions = new DicomTag(0x0018, 0x6011);

		///<summary>(0018,6012) VR=US VM=1 Region Spatial Format</summary>
		public readonly static DicomTag RegionSpatialFormat = new DicomTag(0x0018, 0x6012);

		///<summary>(0018,6014) VR=US VM=1 Region Data Type</summary>
		public readonly static DicomTag RegionDataType = new DicomTag(0x0018, 0x6014);

		///<summary>(0018,6016) VR=UL VM=1 Region Flags</summary>
		public readonly static DicomTag RegionFlags = new DicomTag(0x0018, 0x6016);

		///<summary>(0018,6018) VR=UL VM=1 Region Location Min X0</summary>
		public readonly static DicomTag RegionLocationMinX0 = new DicomTag(0x0018, 0x6018);

		///<summary>(0018,601a) VR=UL VM=1 Region Location Min Y0</summary>
		public readonly static DicomTag RegionLocationMinY0 = new DicomTag(0x0018, 0x601a);

		///<summary>(0018,601c) VR=UL VM=1 Region Location Max X1</summary>
		public readonly static DicomTag RegionLocationMaxX1 = new DicomTag(0x0018, 0x601c);

		///<summary>(0018,601e) VR=UL VM=1 Region Location Max Y1</summary>
		public readonly static DicomTag RegionLocationMaxY1 = new DicomTag(0x0018, 0x601e);

		///<summary>(0018,6020) VR=SL VM=1 Reference Pixel X0</summary>
		public readonly static DicomTag ReferencePixelX0 = new DicomTag(0x0018, 0x6020);

		///<summary>(0018,6022) VR=SL VM=1 Reference Pixel Y0</summary>
		public readonly static DicomTag ReferencePixelY0 = new DicomTag(0x0018, 0x6022);

		///<summary>(0018,6024) VR=US VM=1 Physical Units X Direction</summary>
		public readonly static DicomTag PhysicalUnitsXDirection = new DicomTag(0x0018, 0x6024);

		///<summary>(0018,6026) VR=US VM=1 Physical Units Y Direction</summary>
		public readonly static DicomTag PhysicalUnitsYDirection = new DicomTag(0x0018, 0x6026);

		///<summary>(0018,6028) VR=FD VM=1 Reference Pixel Physical Value X</summary>
		public readonly static DicomTag ReferencePixelPhysicalValueX = new DicomTag(0x0018, 0x6028);

		///<summary>(0018,602a) VR=FD VM=1 Reference Pixel Physical Value Y</summary>
		public readonly static DicomTag ReferencePixelPhysicalValueY = new DicomTag(0x0018, 0x602a);

		///<summary>(0018,602c) VR=FD VM=1 Physical Delta X</summary>
		public readonly static DicomTag PhysicalDeltaX = new DicomTag(0x0018, 0x602c);

		///<summary>(0018,602e) VR=FD VM=1 Physical Delta Y</summary>
		public readonly static DicomTag PhysicalDeltaY = new DicomTag(0x0018, 0x602e);

		///<summary>(0018,6030) VR=UL VM=1 Transducer Frequency</summary>
		public readonly static DicomTag TransducerFrequency = new DicomTag(0x0018, 0x6030);

		///<summary>(0018,6031) VR=CS VM=1 Transducer Type</summary>
		public readonly static DicomTag TransducerType = new DicomTag(0x0018, 0x6031);

		///<summary>(0018,6032) VR=UL VM=1 Pulse Repetition Frequency</summary>
		public readonly static DicomTag PulseRepetitionFrequency = new DicomTag(0x0018, 0x6032);

		///<summary>(0018,6034) VR=FD VM=1 Doppler Correction Angle</summary>
		public readonly static DicomTag DopplerCorrectionAngle = new DicomTag(0x0018, 0x6034);

		///<summary>(0018,6036) VR=FD VM=1 Steering Angle</summary>
		public readonly static DicomTag SteeringAngle = new DicomTag(0x0018, 0x6036);

		///<summary>(0018,6038) VR=UL VM=1 Doppler Sample Volume X Position (Retired) (RETIRED)</summary>
		public readonly static DicomTag DopplerSampleVolumeXPositionRETIRED = new DicomTag(0x0018, 0x6038);

		///<summary>(0018,6039) VR=SL VM=1 Doppler Sample Volume X Position</summary>
		public readonly static DicomTag DopplerSampleVolumeXPosition = new DicomTag(0x0018, 0x6039);

		///<summary>(0018,603a) VR=UL VM=1 Doppler Sample Volume Y Position (Retired) (RETIRED)</summary>
		public readonly static DicomTag DopplerSampleVolumeYPositionRETIRED = new DicomTag(0x0018, 0x603a);

		///<summary>(0018,603b) VR=SL VM=1 Doppler Sample Volume Y Position</summary>
		public readonly static DicomTag DopplerSampleVolumeYPosition = new DicomTag(0x0018, 0x603b);

		///<summary>(0018,603c) VR=UL VM=1 TM-Line Position X0 (Retired) (RETIRED)</summary>
		public readonly static DicomTag TMLinePositionX0RETIRED = new DicomTag(0x0018, 0x603c);

		///<summary>(0018,603d) VR=SL VM=1 TM-Line Position X0</summary>
		public readonly static DicomTag TMLinePositionX0 = new DicomTag(0x0018, 0x603d);

		///<summary>(0018,603e) VR=UL VM=1 TM-Line Position Y0 (Retired) (RETIRED)</summary>
		public readonly static DicomTag TMLinePositionY0RETIRED = new DicomTag(0x0018, 0x603e);

		///<summary>(0018,603f) VR=SL VM=1 TM-Line Position Y0</summary>
		public readonly static DicomTag TMLinePositionY0 = new DicomTag(0x0018, 0x603f);

		///<summary>(0018,6040) VR=UL VM=1 TM-Line Position X1 (Retired) (RETIRED)</summary>
		public readonly static DicomTag TMLinePositionX1RETIRED = new DicomTag(0x0018, 0x6040);

		///<summary>(0018,6041) VR=SL VM=1 TM-Line Position X1</summary>
		public readonly static DicomTag TMLinePositionX1 = new DicomTag(0x0018, 0x6041);

		///<summary>(0018,6042) VR=UL VM=1 TM-Line Position Y1 (Retired) (RETIRED)</summary>
		public readonly static DicomTag TMLinePositionY1RETIRED = new DicomTag(0x0018, 0x6042);

		///<summary>(0018,6043) VR=SL VM=1 TM-Line Position Y1</summary>
		public readonly static DicomTag TMLinePositionY1 = new DicomTag(0x0018, 0x6043);

		///<summary>(0018,6044) VR=US VM=1 Pixel Component Organization</summary>
		public readonly static DicomTag PixelComponentOrganization = new DicomTag(0x0018, 0x6044);

		///<summary>(0018,6046) VR=UL VM=1 Pixel Component Mask</summary>
		public readonly static DicomTag PixelComponentMask = new DicomTag(0x0018, 0x6046);

		///<summary>(0018,6048) VR=UL VM=1 Pixel Component Range Start</summary>
		public readonly static DicomTag PixelComponentRangeStart = new DicomTag(0x0018, 0x6048);

		///<summary>(0018,604a) VR=UL VM=1 Pixel Component Range Stop</summary>
		public readonly static DicomTag PixelComponentRangeStop = new DicomTag(0x0018, 0x604a);

		///<summary>(0018,604c) VR=US VM=1 Pixel Component Physical Units</summary>
		public readonly static DicomTag PixelComponentPhysicalUnits = new DicomTag(0x0018, 0x604c);

		///<summary>(0018,604e) VR=US VM=1 Pixel Component Data Type</summary>
		public readonly static DicomTag PixelComponentDataType = new DicomTag(0x0018, 0x604e);

		///<summary>(0018,6050) VR=UL VM=1 Number of Table Break Points</summary>
		public readonly static DicomTag NumberOfTableBreakPoints = new DicomTag(0x0018, 0x6050);

		///<summary>(0018,6052) VR=UL VM=1-n Table of X Break Points</summary>
		public readonly static DicomTag TableOfXBreakPoints = new DicomTag(0x0018, 0x6052);

		///<summary>(0018,6054) VR=FD VM=1-n Table of Y Break Points</summary>
		public readonly static DicomTag TableOfYBreakPoints = new DicomTag(0x0018, 0x6054);

		///<summary>(0018,6056) VR=UL VM=1 Number of Table Entries</summary>
		public readonly static DicomTag NumberOfTableEntries = new DicomTag(0x0018, 0x6056);

		///<summary>(0018,6058) VR=UL VM=1-n Table of Pixel Values</summary>
		public readonly static DicomTag TableOfPixelValues = new DicomTag(0x0018, 0x6058);

		///<summary>(0018,605a) VR=FL VM=1-n Table of Parameter Values</summary>
		public readonly static DicomTag TableOfParameterValues = new DicomTag(0x0018, 0x605a);

		///<summary>(0018,6060) VR=FL VM=1-n R Wave Time Vector</summary>
		public readonly static DicomTag RWaveTimeVector = new DicomTag(0x0018, 0x6060);

		///<summary>(0018,7000) VR=CS VM=1 Detector Conditions Nominal Flag</summary>
		public readonly static DicomTag DetectorConditionsNominalFlag = new DicomTag(0x0018, 0x7000);

		///<summary>(0018,7001) VR=DS VM=1 Detector Temperature</summary>
		public readonly static DicomTag DetectorTemperature = new DicomTag(0x0018, 0x7001);

		///<summary>(0018,7004) VR=CS VM=1 Detector Type</summary>
		public readonly static DicomTag DetectorType = new DicomTag(0x0018, 0x7004);

		///<summary>(0018,7005) VR=CS VM=1 Detector Configuration</summary>
		public readonly static DicomTag DetectorConfiguration = new DicomTag(0x0018, 0x7005);

		///<summary>(0018,7006) VR=LT VM=1 Detector Description</summary>
		public readonly static DicomTag DetectorDescription = new DicomTag(0x0018, 0x7006);

		///<summary>(0018,7008) VR=LT VM=1 Detector Mode</summary>
		public readonly static DicomTag DetectorMode = new DicomTag(0x0018, 0x7008);

		///<summary>(0018,700a) VR=SH VM=1 Detector ID</summary>
		public readonly static DicomTag DetectorID = new DicomTag(0x0018, 0x700a);

		///<summary>(0018,700c) VR=DA VM=1 Date of Last Detector Calibration</summary>
		public readonly static DicomTag DateOfLastDetectorCalibration = new DicomTag(0x0018, 0x700c);

		///<summary>(0018,700e) VR=TM VM=1 Time of Last Detector Calibration</summary>
		public readonly static DicomTag TimeOfLastDetectorCalibration = new DicomTag(0x0018, 0x700e);

		///<summary>(0018,7010) VR=IS VM=1 Exposures on Detector Since Last Calibration</summary>
		public readonly static DicomTag ExposuresOnDetectorSinceLastCalibration = new DicomTag(0x0018, 0x7010);

		///<summary>(0018,7011) VR=IS VM=1 Exposures on Detector Since Manufactured</summary>
		public readonly static DicomTag ExposuresOnDetectorSinceManufactured = new DicomTag(0x0018, 0x7011);

		///<summary>(0018,7012) VR=DS VM=1 Detector Time Since Last Exposure</summary>
		public readonly static DicomTag DetectorTimeSinceLastExposure = new DicomTag(0x0018, 0x7012);

		///<summary>(0018,7014) VR=DS VM=1 Detector Active Time</summary>
		public readonly static DicomTag DetectorActiveTime = new DicomTag(0x0018, 0x7014);

		///<summary>(0018,7016) VR=DS VM=1 Detector Activation Offset From Exposure</summary>
		public readonly static DicomTag DetectorActivationOffsetFromExposure = new DicomTag(0x0018, 0x7016);

		///<summary>(0018,701a) VR=DS VM=2 Detector Binning</summary>
		public readonly static DicomTag DetectorBinning = new DicomTag(0x0018, 0x701a);

		///<summary>(0018,7020) VR=DS VM=2 Detector Element Physical Size</summary>
		public readonly static DicomTag DetectorElementPhysicalSize = new DicomTag(0x0018, 0x7020);

		///<summary>(0018,7022) VR=DS VM=2 Detector Element Spacing</summary>
		public readonly static DicomTag DetectorElementSpacing = new DicomTag(0x0018, 0x7022);

		///<summary>(0018,7024) VR=CS VM=1 Detector Active Shape</summary>
		public readonly static DicomTag DetectorActiveShape = new DicomTag(0x0018, 0x7024);

		///<summary>(0018,7026) VR=DS VM=1-2 Detector Active Dimension(s)</summary>
		public readonly static DicomTag DetectorActiveDimensions = new DicomTag(0x0018, 0x7026);

		///<summary>(0018,7028) VR=DS VM=2 Detector Active Origin</summary>
		public readonly static DicomTag DetectorActiveOrigin = new DicomTag(0x0018, 0x7028);

		///<summary>(0018,702a) VR=LO VM=1 Detector Manufacturer Name</summary>
		public readonly static DicomTag DetectorManufacturerName = new DicomTag(0x0018, 0x702a);

		///<summary>(0018,702b) VR=LO VM=1 Detector Manufacturer’s Model Name</summary>
		public readonly static DicomTag DetectorManufacturerModelName = new DicomTag(0x0018, 0x702b);

		///<summary>(0018,7030) VR=DS VM=2 Field of View Origin</summary>
		public readonly static DicomTag FieldOfViewOrigin = new DicomTag(0x0018, 0x7030);

		///<summary>(0018,7032) VR=DS VM=1 Field of View Rotation</summary>
		public readonly static DicomTag FieldOfViewRotation = new DicomTag(0x0018, 0x7032);

		///<summary>(0018,7034) VR=CS VM=1 Field of View Horizontal Flip</summary>
		public readonly static DicomTag FieldOfViewHorizontalFlip = new DicomTag(0x0018, 0x7034);

		///<summary>(0018,7036) VR=FL VM=2 Pixel Data Area Origin Relative To FOV</summary>
		public readonly static DicomTag PixelDataAreaOriginRelativeToFOV = new DicomTag(0x0018, 0x7036);

		///<summary>(0018,7038) VR=FL VM=1 Pixel Data Area Rotation Angle Relative To FOV</summary>
		public readonly static DicomTag PixelDataAreaRotationAngleRelativeToFOV = new DicomTag(0x0018, 0x7038);

		///<summary>(0018,7040) VR=LT VM=1 Grid Absorbing Material</summary>
		public readonly static DicomTag GridAbsorbingMaterial = new DicomTag(0x0018, 0x7040);

		///<summary>(0018,7041) VR=LT VM=1 Grid Spacing Material</summary>
		public readonly static DicomTag GridSpacingMaterial = new DicomTag(0x0018, 0x7041);

		///<summary>(0018,7042) VR=DS VM=1 Grid Thickness</summary>
		public readonly static DicomTag GridThickness = new DicomTag(0x0018, 0x7042);

		///<summary>(0018,7044) VR=DS VM=1 Grid Pitch</summary>
		public readonly static DicomTag GridPitch = new DicomTag(0x0018, 0x7044);

		///<summary>(0018,7046) VR=IS VM=2 Grid Aspect Ratio</summary>
		public readonly static DicomTag GridAspectRatio = new DicomTag(0x0018, 0x7046);

		///<summary>(0018,7048) VR=DS VM=1 Grid Period</summary>
		public readonly static DicomTag GridPeriod = new DicomTag(0x0018, 0x7048);

		///<summary>(0018,704c) VR=DS VM=1 Grid Focal Distance</summary>
		public readonly static DicomTag GridFocalDistance = new DicomTag(0x0018, 0x704c);

		///<summary>(0018,7050) VR=CS VM=1-n Filter Material</summary>
		public readonly static DicomTag FilterMaterial = new DicomTag(0x0018, 0x7050);

		///<summary>(0018,7052) VR=DS VM=1-n Filter Thickness Minimum</summary>
		public readonly static DicomTag FilterThicknessMinimum = new DicomTag(0x0018, 0x7052);

		///<summary>(0018,7054) VR=DS VM=1-n Filter Thickness Maximum</summary>
		public readonly static DicomTag FilterThicknessMaximum = new DicomTag(0x0018, 0x7054);

		///<summary>(0018,7056) VR=FL VM=1-n Filter Beam Path Length Minimum</summary>
		public readonly static DicomTag FilterBeamPathLengthMinimum = new DicomTag(0x0018, 0x7056);

		///<summary>(0018,7058) VR=FL VM=1-n Filter Beam Path Length Maximum</summary>
		public readonly static DicomTag FilterBeamPathLengthMaximum = new DicomTag(0x0018, 0x7058);

		///<summary>(0018,7060) VR=CS VM=1 Exposure Control Mode</summary>
		public readonly static DicomTag ExposureControlMode = new DicomTag(0x0018, 0x7060);

		///<summary>(0018,7062) VR=LT VM=1 Exposure Control Mode Description</summary>
		public readonly static DicomTag ExposureControlModeDescription = new DicomTag(0x0018, 0x7062);

		///<summary>(0018,7064) VR=CS VM=1 Exposure Status</summary>
		public readonly static DicomTag ExposureStatus = new DicomTag(0x0018, 0x7064);

		///<summary>(0018,7065) VR=DS VM=1 Phototimer Setting</summary>
		public readonly static DicomTag PhototimerSetting = new DicomTag(0x0018, 0x7065);

		///<summary>(0018,8150) VR=DS VM=1 Exposure Time in µS</summary>
		public readonly static DicomTag ExposureTimeInuS = new DicomTag(0x0018, 0x8150);

		///<summary>(0018,8151) VR=DS VM=1 X-Ray Tube Current in µA</summary>
		public readonly static DicomTag XRayTubeCurrentInuA = new DicomTag(0x0018, 0x8151);

		///<summary>(0018,9004) VR=CS VM=1 Content Qualification</summary>
		public readonly static DicomTag ContentQualification = new DicomTag(0x0018, 0x9004);

		///<summary>(0018,9005) VR=SH VM=1 Pulse Sequence Name</summary>
		public readonly static DicomTag PulseSequenceName = new DicomTag(0x0018, 0x9005);

		///<summary>(0018,9006) VR=SQ VM=1 MR Imaging Modifier Sequence</summary>
		public readonly static DicomTag MRImagingModifierSequence = new DicomTag(0x0018, 0x9006);

		///<summary>(0018,9008) VR=CS VM=1 Echo Pulse Sequence</summary>
		public readonly static DicomTag EchoPulseSequence = new DicomTag(0x0018, 0x9008);

		///<summary>(0018,9009) VR=CS VM=1 Inversion Recovery</summary>
		public readonly static DicomTag InversionRecovery = new DicomTag(0x0018, 0x9009);

		///<summary>(0018,9010) VR=CS VM=1 Flow Compensation</summary>
		public readonly static DicomTag FlowCompensation = new DicomTag(0x0018, 0x9010);

		///<summary>(0018,9011) VR=CS VM=1 Multiple Spin Echo</summary>
		public readonly static DicomTag MultipleSpinEcho = new DicomTag(0x0018, 0x9011);

		///<summary>(0018,9012) VR=CS VM=1 Multi-planar Excitation</summary>
		public readonly static DicomTag MultiPlanarExcitation = new DicomTag(0x0018, 0x9012);

		///<summary>(0018,9014) VR=CS VM=1 Phase Contrast</summary>
		public readonly static DicomTag PhaseContrast = new DicomTag(0x0018, 0x9014);

		///<summary>(0018,9015) VR=CS VM=1 Time of Flight Contrast</summary>
		public readonly static DicomTag TimeOfFlightContrast = new DicomTag(0x0018, 0x9015);

		///<summary>(0018,9016) VR=CS VM=1 Spoiling</summary>
		public readonly static DicomTag Spoiling = new DicomTag(0x0018, 0x9016);

		///<summary>(0018,9017) VR=CS VM=1 Steady State Pulse Sequence</summary>
		public readonly static DicomTag SteadyStatePulseSequence = new DicomTag(0x0018, 0x9017);

		///<summary>(0018,9018) VR=CS VM=1 Echo Planar Pulse Sequence</summary>
		public readonly static DicomTag EchoPlanarPulseSequence = new DicomTag(0x0018, 0x9018);

		///<summary>(0018,9019) VR=FD VM=1 Tag Angle First Axis</summary>
		public readonly static DicomTag TagAngleFirstAxis = new DicomTag(0x0018, 0x9019);

		///<summary>(0018,9020) VR=CS VM=1 Magnetization Transfer</summary>
		public readonly static DicomTag MagnetizationTransfer = new DicomTag(0x0018, 0x9020);

		///<summary>(0018,9021) VR=CS VM=1 T2 Preparation</summary>
		public readonly static DicomTag T2Preparation = new DicomTag(0x0018, 0x9021);

		///<summary>(0018,9022) VR=CS VM=1 Blood Signal Nulling</summary>
		public readonly static DicomTag BloodSignalNulling = new DicomTag(0x0018, 0x9022);

		///<summary>(0018,9024) VR=CS VM=1 Saturation Recovery</summary>
		public readonly static DicomTag SaturationRecovery = new DicomTag(0x0018, 0x9024);

		///<summary>(0018,9025) VR=CS VM=1 Spectrally Selected Suppression</summary>
		public readonly static DicomTag SpectrallySelectedSuppression = new DicomTag(0x0018, 0x9025);

		///<summary>(0018,9026) VR=CS VM=1 Spectrally Selected Excitation</summary>
		public readonly static DicomTag SpectrallySelectedExcitation = new DicomTag(0x0018, 0x9026);

		///<summary>(0018,9027) VR=CS VM=1 Spatial Pre-saturation</summary>
		public readonly static DicomTag SpatialPresaturation = new DicomTag(0x0018, 0x9027);

		///<summary>(0018,9028) VR=CS VM=1 Tagging</summary>
		public readonly static DicomTag Tagging = new DicomTag(0x0018, 0x9028);

		///<summary>(0018,9029) VR=CS VM=1 Oversampling Phase</summary>
		public readonly static DicomTag OversamplingPhase = new DicomTag(0x0018, 0x9029);

		///<summary>(0018,9030) VR=FD VM=1 Tag Spacing First Dimension</summary>
		public readonly static DicomTag TagSpacingFirstDimension = new DicomTag(0x0018, 0x9030);

		///<summary>(0018,9032) VR=CS VM=1 Geometry of k-Space Traversal</summary>
		public readonly static DicomTag GeometryOfKSpaceTraversal = new DicomTag(0x0018, 0x9032);

		///<summary>(0018,9033) VR=CS VM=1 Segmented k-Space Traversal</summary>
		public readonly static DicomTag SegmentedKSpaceTraversal = new DicomTag(0x0018, 0x9033);

		///<summary>(0018,9034) VR=CS VM=1 Rectilinear Phase Encode Reordering</summary>
		public readonly static DicomTag RectilinearPhaseEncodeReordering = new DicomTag(0x0018, 0x9034);

		///<summary>(0018,9035) VR=FD VM=1 Tag Thickness</summary>
		public readonly static DicomTag TagThickness = new DicomTag(0x0018, 0x9035);

		///<summary>(0018,9036) VR=CS VM=1 Partial Fourier Direction</summary>
		public readonly static DicomTag PartialFourierDirection = new DicomTag(0x0018, 0x9036);

		///<summary>(0018,9037) VR=CS VM=1 Cardiac Synchronization Technique</summary>
		public readonly static DicomTag CardiacSynchronizationTechnique = new DicomTag(0x0018, 0x9037);

		///<summary>(0018,9041) VR=LO VM=1 Receive Coil Manufacturer Name</summary>
		public readonly static DicomTag ReceiveCoilManufacturerName = new DicomTag(0x0018, 0x9041);

		///<summary>(0018,9042) VR=SQ VM=1 MR Receive Coil Sequence</summary>
		public readonly static DicomTag MRReceiveCoilSequence = new DicomTag(0x0018, 0x9042);

		///<summary>(0018,9043) VR=CS VM=1 Receive Coil Type</summary>
		public readonly static DicomTag ReceiveCoilType = new DicomTag(0x0018, 0x9043);

		///<summary>(0018,9044) VR=CS VM=1 Quadrature Receive Coil</summary>
		public readonly static DicomTag QuadratureReceiveCoil = new DicomTag(0x0018, 0x9044);

		///<summary>(0018,9045) VR=SQ VM=1 Multi-Coil Definition Sequence</summary>
		public readonly static DicomTag MultiCoilDefinitionSequence = new DicomTag(0x0018, 0x9045);

		///<summary>(0018,9046) VR=LO VM=1 Multi-Coil Configuration</summary>
		public readonly static DicomTag MultiCoilConfiguration = new DicomTag(0x0018, 0x9046);

		///<summary>(0018,9047) VR=SH VM=1 Multi-Coil Element Name</summary>
		public readonly static DicomTag MultiCoilElementName = new DicomTag(0x0018, 0x9047);

		///<summary>(0018,9048) VR=CS VM=1 Multi-Coil Element Used</summary>
		public readonly static DicomTag MultiCoilElementUsed = new DicomTag(0x0018, 0x9048);

		///<summary>(0018,9049) VR=SQ VM=1 MR Transmit Coil Sequence</summary>
		public readonly static DicomTag MRTransmitCoilSequence = new DicomTag(0x0018, 0x9049);

		///<summary>(0018,9050) VR=LO VM=1 Transmit Coil Manufacturer Name</summary>
		public readonly static DicomTag TransmitCoilManufacturerName = new DicomTag(0x0018, 0x9050);

		///<summary>(0018,9051) VR=CS VM=1 Transmit Coil Type</summary>
		public readonly static DicomTag TransmitCoilType = new DicomTag(0x0018, 0x9051);

		///<summary>(0018,9052) VR=FD VM=1-2 Spectral Width</summary>
		public readonly static DicomTag SpectralWidth = new DicomTag(0x0018, 0x9052);

		///<summary>(0018,9053) VR=FD VM=1-2 Chemical Shift Reference</summary>
		public readonly static DicomTag ChemicalShiftReference = new DicomTag(0x0018, 0x9053);

		///<summary>(0018,9054) VR=CS VM=1 Volume Localization Technique</summary>
		public readonly static DicomTag VolumeLocalizationTechnique = new DicomTag(0x0018, 0x9054);

		///<summary>(0018,9058) VR=US VM=1 MR Acquisition Frequency Encoding Steps</summary>
		public readonly static DicomTag MRAcquisitionFrequencyEncodingSteps = new DicomTag(0x0018, 0x9058);

		///<summary>(0018,9059) VR=CS VM=1 De-coupling</summary>
		public readonly static DicomTag Decoupling = new DicomTag(0x0018, 0x9059);

		///<summary>(0018,9060) VR=CS VM=1-2 De-coupled Nucleus</summary>
		public readonly static DicomTag DecoupledNucleus = new DicomTag(0x0018, 0x9060);

		///<summary>(0018,9061) VR=FD VM=1-2 De-coupling Frequency</summary>
		public readonly static DicomTag DecouplingFrequency = new DicomTag(0x0018, 0x9061);

		///<summary>(0018,9062) VR=CS VM=1 De-coupling Method</summary>
		public readonly static DicomTag DecouplingMethod = new DicomTag(0x0018, 0x9062);

		///<summary>(0018,9063) VR=FD VM=1-2 De-coupling Chemical Shift Reference</summary>
		public readonly static DicomTag DecouplingChemicalShiftReference = new DicomTag(0x0018, 0x9063);

		///<summary>(0018,9064) VR=CS VM=1 k-space Filtering</summary>
		public readonly static DicomTag KSpaceFiltering = new DicomTag(0x0018, 0x9064);

		///<summary>(0018,9065) VR=CS VM=1-2 Time Domain Filtering</summary>
		public readonly static DicomTag TimeDomainFiltering = new DicomTag(0x0018, 0x9065);

		///<summary>(0018,9066) VR=US VM=1-2 Number of Zero Fills</summary>
		public readonly static DicomTag NumberOfZeroFills = new DicomTag(0x0018, 0x9066);

		///<summary>(0018,9067) VR=CS VM=1 Baseline Correction</summary>
		public readonly static DicomTag BaselineCorrection = new DicomTag(0x0018, 0x9067);

		///<summary>(0018,9069) VR=FD VM=1 Parallel Reduction Factor In-plane</summary>
		public readonly static DicomTag ParallelReductionFactorInPlane = new DicomTag(0x0018, 0x9069);

		///<summary>(0018,9070) VR=FD VM=1 Cardiac R-R Interval Specified</summary>
		public readonly static DicomTag CardiacRRIntervalSpecified = new DicomTag(0x0018, 0x9070);

		///<summary>(0018,9073) VR=FD VM=1 Acquisition Duration</summary>
		public readonly static DicomTag AcquisitionDuration = new DicomTag(0x0018, 0x9073);

		///<summary>(0018,9074) VR=DT VM=1 Frame Acquisition DateTime</summary>
		public readonly static DicomTag FrameAcquisitionDateTime = new DicomTag(0x0018, 0x9074);

		///<summary>(0018,9075) VR=CS VM=1 Diffusion Directionality</summary>
		public readonly static DicomTag DiffusionDirectionality = new DicomTag(0x0018, 0x9075);

		///<summary>(0018,9076) VR=SQ VM=1 Diffusion Gradient Direction Sequence</summary>
		public readonly static DicomTag DiffusionGradientDirectionSequence = new DicomTag(0x0018, 0x9076);

		///<summary>(0018,9077) VR=CS VM=1 Parallel Acquisition</summary>
		public readonly static DicomTag ParallelAcquisition = new DicomTag(0x0018, 0x9077);

		///<summary>(0018,9078) VR=CS VM=1 Parallel Acquisition Technique</summary>
		public readonly static DicomTag ParallelAcquisitionTechnique = new DicomTag(0x0018, 0x9078);

		///<summary>(0018,9079) VR=FD VM=1-n Inversion Times</summary>
		public readonly static DicomTag InversionTimes = new DicomTag(0x0018, 0x9079);

		///<summary>(0018,9080) VR=ST VM=1 Metabolite Map Description</summary>
		public readonly static DicomTag MetaboliteMapDescription = new DicomTag(0x0018, 0x9080);

		///<summary>(0018,9081) VR=CS VM=1 Partial Fourier</summary>
		public readonly static DicomTag PartialFourier = new DicomTag(0x0018, 0x9081);

		///<summary>(0018,9082) VR=FD VM=1 Effective Echo Time</summary>
		public readonly static DicomTag EffectiveEchoTime = new DicomTag(0x0018, 0x9082);

		///<summary>(0018,9083) VR=SQ VM=1 Metabolite Map Code Sequence</summary>
		public readonly static DicomTag MetaboliteMapCodeSequence = new DicomTag(0x0018, 0x9083);

		///<summary>(0018,9084) VR=SQ VM=1 Chemical Shift Sequence</summary>
		public readonly static DicomTag ChemicalShiftSequence = new DicomTag(0x0018, 0x9084);

		///<summary>(0018,9085) VR=CS VM=1 Cardiac Signal Source</summary>
		public readonly static DicomTag CardiacSignalSource = new DicomTag(0x0018, 0x9085);

		///<summary>(0018,9087) VR=FD VM=1 Diffusion b-value</summary>
		public readonly static DicomTag DiffusionBValue = new DicomTag(0x0018, 0x9087);

		///<summary>(0018,9089) VR=FD VM=3 Diffusion Gradient Orientation</summary>
		public readonly static DicomTag DiffusionGradientOrientation = new DicomTag(0x0018, 0x9089);

		///<summary>(0018,9090) VR=FD VM=3 Velocity Encoding Direction</summary>
		public readonly static DicomTag VelocityEncodingDirection = new DicomTag(0x0018, 0x9090);

		///<summary>(0018,9091) VR=FD VM=1 Velocity Encoding Minimum Value</summary>
		public readonly static DicomTag VelocityEncodingMinimumValue = new DicomTag(0x0018, 0x9091);

		///<summary>(0018,9092) VR=SQ VM=1 Velocity Encoding Acquisition Sequence</summary>
		public readonly static DicomTag VelocityEncodingAcquisitionSequence = new DicomTag(0x0018, 0x9092);

		///<summary>(0018,9093) VR=US VM=1 Number of k-Space Trajectories</summary>
		public readonly static DicomTag NumberOfKSpaceTrajectories = new DicomTag(0x0018, 0x9093);

		///<summary>(0018,9094) VR=CS VM=1 Coverage of k-Space</summary>
		public readonly static DicomTag CoverageOfKSpace = new DicomTag(0x0018, 0x9094);

		///<summary>(0018,9095) VR=UL VM=1 Spectroscopy Acquisition Phase Rows</summary>
		public readonly static DicomTag SpectroscopyAcquisitionPhaseRows = new DicomTag(0x0018, 0x9095);

		///<summary>(0018,9096) VR=FD VM=1 Parallel Reduction Factor In-plane (Retired) (RETIRED)</summary>
		public readonly static DicomTag ParallelReductionFactorInPlaneRETIRED = new DicomTag(0x0018, 0x9096);

		///<summary>(0018,9098) VR=FD VM=1-2 Transmitter Frequency</summary>
		public readonly static DicomTag TransmitterFrequency = new DicomTag(0x0018, 0x9098);

		///<summary>(0018,9100) VR=CS VM=1-2 Resonant Nucleus</summary>
		public readonly static DicomTag ResonantNucleus = new DicomTag(0x0018, 0x9100);

		///<summary>(0018,9101) VR=CS VM=1 Frequency Correction</summary>
		public readonly static DicomTag FrequencyCorrection = new DicomTag(0x0018, 0x9101);

		///<summary>(0018,9103) VR=SQ VM=1 MR Spectroscopy FOV/Geometry Sequence</summary>
		public readonly static DicomTag MRSpectroscopyFOVGeometrySequence = new DicomTag(0x0018, 0x9103);

		///<summary>(0018,9104) VR=FD VM=1 Slab Thickness</summary>
		public readonly static DicomTag SlabThickness = new DicomTag(0x0018, 0x9104);

		///<summary>(0018,9105) VR=FD VM=3 Slab Orientation</summary>
		public readonly static DicomTag SlabOrientation = new DicomTag(0x0018, 0x9105);

		///<summary>(0018,9106) VR=FD VM=3 Mid Slab Position</summary>
		public readonly static DicomTag MidSlabPosition = new DicomTag(0x0018, 0x9106);

		///<summary>(0018,9107) VR=SQ VM=1 MR Spatial Saturation Sequence</summary>
		public readonly static DicomTag MRSpatialSaturationSequence = new DicomTag(0x0018, 0x9107);

		///<summary>(0018,9112) VR=SQ VM=1 MR Timing and Related Parameters Sequence</summary>
		public readonly static DicomTag MRTimingAndRelatedParametersSequence = new DicomTag(0x0018, 0x9112);

		///<summary>(0018,9114) VR=SQ VM=1 MR Echo Sequence</summary>
		public readonly static DicomTag MREchoSequence = new DicomTag(0x0018, 0x9114);

		///<summary>(0018,9115) VR=SQ VM=1 MR Modifier Sequence</summary>
		public readonly static DicomTag MRModifierSequence = new DicomTag(0x0018, 0x9115);

		///<summary>(0018,9117) VR=SQ VM=1 MR Diffusion Sequence</summary>
		public readonly static DicomTag MRDiffusionSequence = new DicomTag(0x0018, 0x9117);

		///<summary>(0018,9118) VR=SQ VM=1 Cardiac Synchronization Sequence</summary>
		public readonly static DicomTag CardiacSynchronizationSequence = new DicomTag(0x0018, 0x9118);

		///<summary>(0018,9119) VR=SQ VM=1 MR Averages Sequence</summary>
		public readonly static DicomTag MRAveragesSequence = new DicomTag(0x0018, 0x9119);

		///<summary>(0018,9125) VR=SQ VM=1 MR FOV/Geometry Sequence</summary>
		public readonly static DicomTag MRFOVGeometrySequence = new DicomTag(0x0018, 0x9125);

		///<summary>(0018,9126) VR=SQ VM=1 Volume Localization Sequence</summary>
		public readonly static DicomTag VolumeLocalizationSequence = new DicomTag(0x0018, 0x9126);

		///<summary>(0018,9127) VR=UL VM=1 Spectroscopy Acquisition Data Columns</summary>
		public readonly static DicomTag SpectroscopyAcquisitionDataColumns = new DicomTag(0x0018, 0x9127);

		///<summary>(0018,9147) VR=CS VM=1 Diffusion Anisotropy Type</summary>
		public readonly static DicomTag DiffusionAnisotropyType = new DicomTag(0x0018, 0x9147);

		///<summary>(0018,9151) VR=DT VM=1 Frame Reference DateTime</summary>
		public readonly static DicomTag FrameReferenceDateTime = new DicomTag(0x0018, 0x9151);

		///<summary>(0018,9152) VR=SQ VM=1 MR Metabolite Map Sequence</summary>
		public readonly static DicomTag MRMetaboliteMapSequence = new DicomTag(0x0018, 0x9152);

		///<summary>(0018,9155) VR=FD VM=1 Parallel Reduction Factor out-of-plane</summary>
		public readonly static DicomTag ParallelReductionFactorOutOfPlane = new DicomTag(0x0018, 0x9155);

		///<summary>(0018,9159) VR=UL VM=1 Spectroscopy Acquisition Out-of-plane Phase Steps</summary>
		public readonly static DicomTag SpectroscopyAcquisitionOutOfPlanePhaseSteps = new DicomTag(0x0018, 0x9159);

		///<summary>(0018,9166) VR=CS VM=1 Bulk Motion Status (RETIRED)</summary>
		public readonly static DicomTag BulkMotionStatusRETIRED = new DicomTag(0x0018, 0x9166);

		///<summary>(0018,9168) VR=FD VM=1 Parallel Reduction Factor Second In-plane</summary>
		public readonly static DicomTag ParallelReductionFactorSecondInPlane = new DicomTag(0x0018, 0x9168);

		///<summary>(0018,9169) VR=CS VM=1 Cardiac Beat Rejection Technique</summary>
		public readonly static DicomTag CardiacBeatRejectionTechnique = new DicomTag(0x0018, 0x9169);

		///<summary>(0018,9170) VR=CS VM=1 Respiratory Motion Compensation Technique</summary>
		public readonly static DicomTag RespiratoryMotionCompensationTechnique = new DicomTag(0x0018, 0x9170);

		///<summary>(0018,9171) VR=CS VM=1 Respiratory Signal Source</summary>
		public readonly static DicomTag RespiratorySignalSource = new DicomTag(0x0018, 0x9171);

		///<summary>(0018,9172) VR=CS VM=1 Bulk Motion Compensation Technique</summary>
		public readonly static DicomTag BulkMotionCompensationTechnique = new DicomTag(0x0018, 0x9172);

		///<summary>(0018,9173) VR=CS VM=1 Bulk Motion Signal Source</summary>
		public readonly static DicomTag BulkMotionSignalSource = new DicomTag(0x0018, 0x9173);

		///<summary>(0018,9174) VR=CS VM=1 Applicable Safety Standard Agency</summary>
		public readonly static DicomTag ApplicableSafetyStandardAgency = new DicomTag(0x0018, 0x9174);

		///<summary>(0018,9175) VR=LO VM=1 Applicable Safety Standard Description</summary>
		public readonly static DicomTag ApplicableSafetyStandardDescription = new DicomTag(0x0018, 0x9175);

		///<summary>(0018,9176) VR=SQ VM=1 Operating Mode Sequence</summary>
		public readonly static DicomTag OperatingModeSequence = new DicomTag(0x0018, 0x9176);

		///<summary>(0018,9177) VR=CS VM=1 Operating Mode Type</summary>
		public readonly static DicomTag OperatingModeType = new DicomTag(0x0018, 0x9177);

		///<summary>(0018,9178) VR=CS VM=1 Operating Mode</summary>
		public readonly static DicomTag OperatingMode = new DicomTag(0x0018, 0x9178);

		///<summary>(0018,9179) VR=CS VM=1 Specific Absorption Rate Definition</summary>
		public readonly static DicomTag SpecificAbsorptionRateDefinition = new DicomTag(0x0018, 0x9179);

		///<summary>(0018,9180) VR=CS VM=1 Gradient Output Type</summary>
		public readonly static DicomTag GradientOutputType = new DicomTag(0x0018, 0x9180);

		///<summary>(0018,9181) VR=FD VM=1 Specific Absorption Rate Value</summary>
		public readonly static DicomTag SpecificAbsorptionRateValue = new DicomTag(0x0018, 0x9181);

		///<summary>(0018,9182) VR=FD VM=1 Gradient Output</summary>
		public readonly static DicomTag GradientOutput = new DicomTag(0x0018, 0x9182);

		///<summary>(0018,9183) VR=CS VM=1 Flow Compensation Direction</summary>
		public readonly static DicomTag FlowCompensationDirection = new DicomTag(0x0018, 0x9183);

		///<summary>(0018,9184) VR=FD VM=1 Tagging Delay</summary>
		public readonly static DicomTag TaggingDelay = new DicomTag(0x0018, 0x9184);

		///<summary>(0018,9185) VR=ST VM=1 Respiratory Motion Compensation Technique Description</summary>
		public readonly static DicomTag RespiratoryMotionCompensationTechniqueDescription = new DicomTag(0x0018, 0x9185);

		///<summary>(0018,9186) VR=SH VM=1 Respiratory Signal Source ID</summary>
		public readonly static DicomTag RespiratorySignalSourceID = new DicomTag(0x0018, 0x9186);

		///<summary>(0018,9195) VR=FD VM=1 Chemical Shift Minimum Integration Limit in Hz (RETIRED)</summary>
		public readonly static DicomTag ChemicalShiftMinimumIntegrationLimitInHzRETIRED = new DicomTag(0x0018, 0x9195);

		///<summary>(0018,9196) VR=FD VM=1 Chemical Shift Maximum Integration Limit in Hz (RETIRED)</summary>
		public readonly static DicomTag ChemicalShiftMaximumIntegrationLimitInHzRETIRED = new DicomTag(0x0018, 0x9196);

		///<summary>(0018,9197) VR=SQ VM=1 MR Velocity Encoding Sequence</summary>
		public readonly static DicomTag MRVelocityEncodingSequence = new DicomTag(0x0018, 0x9197);

		///<summary>(0018,9198) VR=CS VM=1 First Order Phase Correction</summary>
		public readonly static DicomTag FirstOrderPhaseCorrection = new DicomTag(0x0018, 0x9198);

		///<summary>(0018,9199) VR=CS VM=1 Water Referenced Phase Correction</summary>
		public readonly static DicomTag WaterReferencedPhaseCorrection = new DicomTag(0x0018, 0x9199);

		///<summary>(0018,9200) VR=CS VM=1 MR Spectroscopy Acquisition Type</summary>
		public readonly static DicomTag MRSpectroscopyAcquisitionType = new DicomTag(0x0018, 0x9200);

		///<summary>(0018,9214) VR=CS VM=1 Respiratory Cycle Position</summary>
		public readonly static DicomTag RespiratoryCyclePosition = new DicomTag(0x0018, 0x9214);

		///<summary>(0018,9217) VR=FD VM=1 Velocity Encoding Maximum Value</summary>
		public readonly static DicomTag VelocityEncodingMaximumValue = new DicomTag(0x0018, 0x9217);

		///<summary>(0018,9218) VR=FD VM=1 Tag Spacing Second Dimension</summary>
		public readonly static DicomTag TagSpacingSecondDimension = new DicomTag(0x0018, 0x9218);

		///<summary>(0018,9219) VR=SS VM=1 Tag Angle Second Axis</summary>
		public readonly static DicomTag TagAngleSecondAxis = new DicomTag(0x0018, 0x9219);

		///<summary>(0018,9220) VR=FD VM=1 Frame Acquisition Duration</summary>
		public readonly static DicomTag FrameAcquisitionDuration = new DicomTag(0x0018, 0x9220);

		///<summary>(0018,9226) VR=SQ VM=1 MR Image Frame Type Sequence</summary>
		public readonly static DicomTag MRImageFrameTypeSequence = new DicomTag(0x0018, 0x9226);

		///<summary>(0018,9227) VR=SQ VM=1 MR Spectroscopy Frame Type Sequence</summary>
		public readonly static DicomTag MRSpectroscopyFrameTypeSequence = new DicomTag(0x0018, 0x9227);

		///<summary>(0018,9231) VR=US VM=1 MR Acquisition Phase Encoding Steps in-plane</summary>
		public readonly static DicomTag MRAcquisitionPhaseEncodingStepsInPlane = new DicomTag(0x0018, 0x9231);

		///<summary>(0018,9232) VR=US VM=1 MR Acquisition Phase Encoding Steps out-of-plane</summary>
		public readonly static DicomTag MRAcquisitionPhaseEncodingStepsOutOfPlane = new DicomTag(0x0018, 0x9232);

		///<summary>(0018,9234) VR=UL VM=1 Spectroscopy Acquisition Phase Columns</summary>
		public readonly static DicomTag SpectroscopyAcquisitionPhaseColumns = new DicomTag(0x0018, 0x9234);

		///<summary>(0018,9236) VR=CS VM=1 Cardiac Cycle Position</summary>
		public readonly static DicomTag CardiacCyclePosition = new DicomTag(0x0018, 0x9236);

		///<summary>(0018,9239) VR=SQ VM=1 Specific Absorption Rate Sequence</summary>
		public readonly static DicomTag SpecificAbsorptionRateSequence = new DicomTag(0x0018, 0x9239);

		///<summary>(0018,9240) VR=US VM=1 RF Echo Train Length</summary>
		public readonly static DicomTag RFEchoTrainLength = new DicomTag(0x0018, 0x9240);

		///<summary>(0018,9241) VR=US VM=1 Gradient Echo Train Length</summary>
		public readonly static DicomTag GradientEchoTrainLength = new DicomTag(0x0018, 0x9241);

		///<summary>(0018,9250) VR=CS VM=1 Arterial Spin Labeling Contrast</summary>
		public readonly static DicomTag ArterialSpinLabelingContrast = new DicomTag(0x0018, 0x9250);

		///<summary>(0018,9251) VR=SQ VM=1 MR Arterial Spin Labeling Sequence</summary>
		public readonly static DicomTag MRArterialSpinLabelingSequence = new DicomTag(0x0018, 0x9251);

		///<summary>(0018,9252) VR=LO VM=1 ASL Technique Description</summary>
		public readonly static DicomTag ASLTechniqueDescription = new DicomTag(0x0018, 0x9252);

		///<summary>(0018,9253) VR=US VM=1 ASL Slab Number</summary>
		public readonly static DicomTag ASLSlabNumber = new DicomTag(0x0018, 0x9253);

		///<summary>(0018,9254) VR=FD VM=1 ASL Slab Thickness</summary>
		public readonly static DicomTag ASLSlabThickness = new DicomTag(0x0018, 0x9254);

		///<summary>(0018,9255) VR=FD VM=3 ASL Slab Orientation</summary>
		public readonly static DicomTag ASLSlabOrientation = new DicomTag(0x0018, 0x9255);

		///<summary>(0018,9256) VR=FD VM=3 ASL Mid Slab Position</summary>
		public readonly static DicomTag ASLMidSlabPosition = new DicomTag(0x0018, 0x9256);

		///<summary>(0018,9257) VR=CS VM=1 ASL Context</summary>
		public readonly static DicomTag ASLContext = new DicomTag(0x0018, 0x9257);

		///<summary>(0018,9258) VR=UL VM=1 ASL Pulse Train Duration</summary>
		public readonly static DicomTag ASLPulseTrainDuration = new DicomTag(0x0018, 0x9258);

		///<summary>(0018,9259) VR=CS VM=1 ASL Crusher Flag</summary>
		public readonly static DicomTag ASLCrusherFlag = new DicomTag(0x0018, 0x9259);

		///<summary>(0018,925a) VR=FD VM=1 ASL Crusher Flow</summary>
		public readonly static DicomTag ASLCrusherFlow = new DicomTag(0x0018, 0x925a);

		///<summary>(0018,925b) VR=LO VM=1 ASL Crusher Description</summary>
		public readonly static DicomTag ASLCrusherDescription = new DicomTag(0x0018, 0x925b);

		///<summary>(0018,925c) VR=CS VM=1 ASL Bolus Cut-off Flag</summary>
		public readonly static DicomTag ASLBolusCutoffFlag = new DicomTag(0x0018, 0x925c);

		///<summary>(0018,925d) VR=SQ VM=1 ASL Bolus Cut-off Timing Sequence</summary>
		public readonly static DicomTag ASLBolusCutoffTimingSequence = new DicomTag(0x0018, 0x925d);

		///<summary>(0018,925e) VR=LO VM=1 ASL Bolus Cut-off Technique</summary>
		public readonly static DicomTag ASLBolusCutoffTechnique = new DicomTag(0x0018, 0x925e);

		///<summary>(0018,925f) VR=UL VM=1 ASL Bolus Cut-off Delay Time</summary>
		public readonly static DicomTag ASLBolusCutoffDelayTime = new DicomTag(0x0018, 0x925f);

		///<summary>(0018,9260) VR=SQ VM=1 ASL Slab Sequence</summary>
		public readonly static DicomTag ASLSlabSequence = new DicomTag(0x0018, 0x9260);

		///<summary>(0018,9295) VR=FD VM=1 Chemical Shift Minimum Integration Limit in ppm</summary>
		public readonly static DicomTag ChemicalShiftMinimumIntegrationLimitInppm = new DicomTag(0x0018, 0x9295);

		///<summary>(0018,9296) VR=FD VM=1 Chemical Shift Maximum Integration Limit in ppm</summary>
		public readonly static DicomTag ChemicalShiftMaximumIntegrationLimitInppm = new DicomTag(0x0018, 0x9296);

		///<summary>(0018,9301) VR=SQ VM=1 CT Acquisition Type Sequence</summary>
		public readonly static DicomTag CTAcquisitionTypeSequence = new DicomTag(0x0018, 0x9301);

		///<summary>(0018,9302) VR=CS VM=1 Acquisition Type</summary>
		public readonly static DicomTag AcquisitionType = new DicomTag(0x0018, 0x9302);

		///<summary>(0018,9303) VR=FD VM=1 Tube Angle</summary>
		public readonly static DicomTag TubeAngle = new DicomTag(0x0018, 0x9303);

		///<summary>(0018,9304) VR=SQ VM=1 CT Acquisition Details Sequence</summary>
		public readonly static DicomTag CTAcquisitionDetailsSequence = new DicomTag(0x0018, 0x9304);

		///<summary>(0018,9305) VR=FD VM=1 Revolution Time</summary>
		public readonly static DicomTag RevolutionTime = new DicomTag(0x0018, 0x9305);

		///<summary>(0018,9306) VR=FD VM=1 Single Collimation Width</summary>
		public readonly static DicomTag SingleCollimationWidth = new DicomTag(0x0018, 0x9306);

		///<summary>(0018,9307) VR=FD VM=1 Total Collimation Width</summary>
		public readonly static DicomTag TotalCollimationWidth = new DicomTag(0x0018, 0x9307);

		///<summary>(0018,9308) VR=SQ VM=1 CT Table Dynamics Sequence</summary>
		public readonly static DicomTag CTTableDynamicsSequence = new DicomTag(0x0018, 0x9308);

		///<summary>(0018,9309) VR=FD VM=1 Table Speed</summary>
		public readonly static DicomTag TableSpeed = new DicomTag(0x0018, 0x9309);

		///<summary>(0018,9310) VR=FD VM=1 Table Feed per Rotation</summary>
		public readonly static DicomTag TableFeedPerRotation = new DicomTag(0x0018, 0x9310);

		///<summary>(0018,9311) VR=FD VM=1 Spiral Pitch Factor</summary>
		public readonly static DicomTag SpiralPitchFactor = new DicomTag(0x0018, 0x9311);

		///<summary>(0018,9312) VR=SQ VM=1 CT Geometry Sequence</summary>
		public readonly static DicomTag CTGeometrySequence = new DicomTag(0x0018, 0x9312);

		///<summary>(0018,9313) VR=FD VM=3 Data Collection Center (Patient)</summary>
		public readonly static DicomTag DataCollectionCenterPatient = new DicomTag(0x0018, 0x9313);

		///<summary>(0018,9314) VR=SQ VM=1 CT Reconstruction Sequence</summary>
		public readonly static DicomTag CTReconstructionSequence = new DicomTag(0x0018, 0x9314);

		///<summary>(0018,9315) VR=CS VM=1 Reconstruction Algorithm</summary>
		public readonly static DicomTag ReconstructionAlgorithm = new DicomTag(0x0018, 0x9315);

		///<summary>(0018,9316) VR=CS VM=1 Convolution Kernel Group</summary>
		public readonly static DicomTag ConvolutionKernelGroup = new DicomTag(0x0018, 0x9316);

		///<summary>(0018,9317) VR=FD VM=2 Reconstruction Field of View</summary>
		public readonly static DicomTag ReconstructionFieldOfView = new DicomTag(0x0018, 0x9317);

		///<summary>(0018,9318) VR=FD VM=3 Reconstruction Target Center (Patient)</summary>
		public readonly static DicomTag ReconstructionTargetCenterPatient = new DicomTag(0x0018, 0x9318);

		///<summary>(0018,9319) VR=FD VM=1 Reconstruction Angle</summary>
		public readonly static DicomTag ReconstructionAngle = new DicomTag(0x0018, 0x9319);

		///<summary>(0018,9320) VR=SH VM=1 Image Filter</summary>
		public readonly static DicomTag ImageFilter = new DicomTag(0x0018, 0x9320);

		///<summary>(0018,9321) VR=SQ VM=1 CT Exposure Sequence</summary>
		public readonly static DicomTag CTExposureSequence = new DicomTag(0x0018, 0x9321);

		///<summary>(0018,9322) VR=FD VM=2 Reconstruction Pixel Spacing</summary>
		public readonly static DicomTag ReconstructionPixelSpacing = new DicomTag(0x0018, 0x9322);

		///<summary>(0018,9323) VR=CS VM=1 Exposure Modulation Type</summary>
		public readonly static DicomTag ExposureModulationType = new DicomTag(0x0018, 0x9323);

		///<summary>(0018,9324) VR=FD VM=1 Estimated Dose Saving</summary>
		public readonly static DicomTag EstimatedDoseSaving = new DicomTag(0x0018, 0x9324);

		///<summary>(0018,9325) VR=SQ VM=1 CT X-Ray Details Sequence</summary>
		public readonly static DicomTag CTXRayDetailsSequence = new DicomTag(0x0018, 0x9325);

		///<summary>(0018,9326) VR=SQ VM=1 CT Position Sequence</summary>
		public readonly static DicomTag CTPositionSequence = new DicomTag(0x0018, 0x9326);

		///<summary>(0018,9327) VR=FD VM=1 Table Position</summary>
		public readonly static DicomTag TablePosition = new DicomTag(0x0018, 0x9327);

		///<summary>(0018,9328) VR=FD VM=1 Exposure Time in ms</summary>
		public readonly static DicomTag ExposureTimeInms = new DicomTag(0x0018, 0x9328);

		///<summary>(0018,9329) VR=SQ VM=1 CT Image Frame Type Sequence</summary>
		public readonly static DicomTag CTImageFrameTypeSequence = new DicomTag(0x0018, 0x9329);

		///<summary>(0018,9330) VR=FD VM=1 X-Ray Tube Current in mA</summary>
		public readonly static DicomTag XRayTubeCurrentInmA = new DicomTag(0x0018, 0x9330);

		///<summary>(0018,9332) VR=FD VM=1 Exposure in mAs</summary>
		public readonly static DicomTag ExposureInmAs = new DicomTag(0x0018, 0x9332);

		///<summary>(0018,9333) VR=CS VM=1 Constant Volume Flag</summary>
		public readonly static DicomTag ConstantVolumeFlag = new DicomTag(0x0018, 0x9333);

		///<summary>(0018,9334) VR=CS VM=1 Fluoroscopy Flag</summary>
		public readonly static DicomTag FluoroscopyFlag = new DicomTag(0x0018, 0x9334);

		///<summary>(0018,9335) VR=FD VM=1 Distance Source to Data Collection Center</summary>
		public readonly static DicomTag DistanceSourceToDataCollectionCenter = new DicomTag(0x0018, 0x9335);

		///<summary>(0018,9337) VR=US VM=1 Contrast/Bolus Agent Number</summary>
		public readonly static DicomTag ContrastBolusAgentNumber = new DicomTag(0x0018, 0x9337);

		///<summary>(0018,9338) VR=SQ VM=1 Contrast/Bolus Ingredient Code Sequence</summary>
		public readonly static DicomTag ContrastBolusIngredientCodeSequence = new DicomTag(0x0018, 0x9338);

		///<summary>(0018,9340) VR=SQ VM=1 Contrast Administration Profile Sequence</summary>
		public readonly static DicomTag ContrastAdministrationProfileSequence = new DicomTag(0x0018, 0x9340);

		///<summary>(0018,9341) VR=SQ VM=1 Contrast/Bolus Usage Sequence</summary>
		public readonly static DicomTag ContrastBolusUsageSequence = new DicomTag(0x0018, 0x9341);

		///<summary>(0018,9342) VR=CS VM=1 Contrast/Bolus Agent Administered</summary>
		public readonly static DicomTag ContrastBolusAgentAdministered = new DicomTag(0x0018, 0x9342);

		///<summary>(0018,9343) VR=CS VM=1 Contrast/Bolus Agent Detected</summary>
		public readonly static DicomTag ContrastBolusAgentDetected = new DicomTag(0x0018, 0x9343);

		///<summary>(0018,9344) VR=CS VM=1 Contrast/Bolus Agent Phase</summary>
		public readonly static DicomTag ContrastBolusAgentPhase = new DicomTag(0x0018, 0x9344);

		///<summary>(0018,9345) VR=FD VM=1 CTDIvol</summary>
		public readonly static DicomTag CTDIvol = new DicomTag(0x0018, 0x9345);

		///<summary>(0018,9346) VR=SQ VM=1 CTDI Phantom Type Code Sequence</summary>
		public readonly static DicomTag CTDIPhantomTypeCodeSequence = new DicomTag(0x0018, 0x9346);

		///<summary>(0018,9351) VR=FL VM=1 Calcium Scoring Mass Factor Patient</summary>
		public readonly static DicomTag CalciumScoringMassFactorPatient = new DicomTag(0x0018, 0x9351);

		///<summary>(0018,9352) VR=FL VM=3 Calcium Scoring Mass Factor Device</summary>
		public readonly static DicomTag CalciumScoringMassFactorDevice = new DicomTag(0x0018, 0x9352);

		///<summary>(0018,9353) VR=FL VM=1 Energy Weighting Factor</summary>
		public readonly static DicomTag EnergyWeightingFactor = new DicomTag(0x0018, 0x9353);

		///<summary>(0018,9360) VR=SQ VM=1 CT Additional X-Ray Source Sequence</summary>
		public readonly static DicomTag CTAdditionalXRaySourceSequence = new DicomTag(0x0018, 0x9360);

		///<summary>(0018,9401) VR=SQ VM=1 Projection Pixel Calibration Sequence</summary>
		public readonly static DicomTag ProjectionPixelCalibrationSequence = new DicomTag(0x0018, 0x9401);

		///<summary>(0018,9402) VR=FL VM=1 Distance Source to Isocenter</summary>
		public readonly static DicomTag DistanceSourceToIsocenter = new DicomTag(0x0018, 0x9402);

		///<summary>(0018,9403) VR=FL VM=1 Distance Object to Table Top</summary>
		public readonly static DicomTag DistanceObjectToTableTop = new DicomTag(0x0018, 0x9403);

		///<summary>(0018,9404) VR=FL VM=2 Object Pixel Spacing in Center of Beam</summary>
		public readonly static DicomTag ObjectPixelSpacingInCenterOfBeam = new DicomTag(0x0018, 0x9404);

		///<summary>(0018,9405) VR=SQ VM=1 Positioner Position Sequence</summary>
		public readonly static DicomTag PositionerPositionSequence = new DicomTag(0x0018, 0x9405);

		///<summary>(0018,9406) VR=SQ VM=1 Table Position Sequence</summary>
		public readonly static DicomTag TablePositionSequence = new DicomTag(0x0018, 0x9406);

		///<summary>(0018,9407) VR=SQ VM=1 Collimator Shape Sequence</summary>
		public readonly static DicomTag CollimatorShapeSequence = new DicomTag(0x0018, 0x9407);

		///<summary>(0018,9410) VR=CS VM=1 Planes in Acquisition</summary>
		public readonly static DicomTag PlanesInAcquisition = new DicomTag(0x0018, 0x9410);

		///<summary>(0018,9412) VR=SQ VM=1 XA/XRF Frame Characteristics Sequence</summary>
		public readonly static DicomTag XAXRFFrameCharacteristicsSequence = new DicomTag(0x0018, 0x9412);

		///<summary>(0018,9417) VR=SQ VM=1 Frame Acquisition Sequence</summary>
		public readonly static DicomTag FrameAcquisitionSequence = new DicomTag(0x0018, 0x9417);

		///<summary>(0018,9420) VR=CS VM=1 X-Ray Receptor Type</summary>
		public readonly static DicomTag XRayReceptorType = new DicomTag(0x0018, 0x9420);

		///<summary>(0018,9423) VR=LO VM=1 Acquisition Protocol Name</summary>
		public readonly static DicomTag AcquisitionProtocolName = new DicomTag(0x0018, 0x9423);

		///<summary>(0018,9424) VR=LT VM=1 Acquisition Protocol Description</summary>
		public readonly static DicomTag AcquisitionProtocolDescription = new DicomTag(0x0018, 0x9424);

		///<summary>(0018,9425) VR=CS VM=1 Contrast/Bolus Ingredient Opaque</summary>
		public readonly static DicomTag ContrastBolusIngredientOpaque = new DicomTag(0x0018, 0x9425);

		///<summary>(0018,9426) VR=FL VM=1 Distance Receptor Plane to Detector Housing</summary>
		public readonly static DicomTag DistanceReceptorPlaneToDetectorHousing = new DicomTag(0x0018, 0x9426);

		///<summary>(0018,9427) VR=CS VM=1 Intensifier Active Shape</summary>
		public readonly static DicomTag IntensifierActiveShape = new DicomTag(0x0018, 0x9427);

		///<summary>(0018,9428) VR=FL VM=1-2 Intensifier Active Dimension(s)</summary>
		public readonly static DicomTag IntensifierActiveDimensions = new DicomTag(0x0018, 0x9428);

		///<summary>(0018,9429) VR=FL VM=2 Physical Detector Size</summary>
		public readonly static DicomTag PhysicalDetectorSize = new DicomTag(0x0018, 0x9429);

		///<summary>(0018,9430) VR=FL VM=2 Position of Isocenter Projection</summary>
		public readonly static DicomTag PositionOfIsocenterProjection = new DicomTag(0x0018, 0x9430);

		///<summary>(0018,9432) VR=SQ VM=1 Field of View Sequence</summary>
		public readonly static DicomTag FieldOfViewSequence = new DicomTag(0x0018, 0x9432);

		///<summary>(0018,9433) VR=LO VM=1 Field of View Description</summary>
		public readonly static DicomTag FieldOfViewDescription = new DicomTag(0x0018, 0x9433);

		///<summary>(0018,9434) VR=SQ VM=1 Exposure Control Sensing Regions Sequence</summary>
		public readonly static DicomTag ExposureControlSensingRegionsSequence = new DicomTag(0x0018, 0x9434);

		///<summary>(0018,9435) VR=CS VM=1 Exposure Control Sensing Region Shape</summary>
		public readonly static DicomTag ExposureControlSensingRegionShape = new DicomTag(0x0018, 0x9435);

		///<summary>(0018,9436) VR=SS VM=1 Exposure Control Sensing Region Left Vertical Edge</summary>
		public readonly static DicomTag ExposureControlSensingRegionLeftVerticalEdge = new DicomTag(0x0018, 0x9436);

		///<summary>(0018,9437) VR=SS VM=1 Exposure Control Sensing Region Right Vertical Edge</summary>
		public readonly static DicomTag ExposureControlSensingRegionRightVerticalEdge = new DicomTag(0x0018, 0x9437);

		///<summary>(0018,9438) VR=SS VM=1 Exposure Control Sensing Region Upper Horizontal Edge</summary>
		public readonly static DicomTag ExposureControlSensingRegionUpperHorizontalEdge = new DicomTag(0x0018, 0x9438);

		///<summary>(0018,9439) VR=SS VM=1 Exposure Control Sensing Region Lower Horizontal Edge</summary>
		public readonly static DicomTag ExposureControlSensingRegionLowerHorizontalEdge = new DicomTag(0x0018, 0x9439);

		///<summary>(0018,9440) VR=SS VM=2 Center of Circular Exposure Control Sensing Region</summary>
		public readonly static DicomTag CenterOfCircularExposureControlSensingRegion = new DicomTag(0x0018, 0x9440);

		///<summary>(0018,9441) VR=US VM=1 Radius of Circular Exposure Control Sensing Region</summary>
		public readonly static DicomTag RadiusOfCircularExposureControlSensingRegion = new DicomTag(0x0018, 0x9441);

		///<summary>(0018,9442) VR=SS VM=2-n Vertices of the Polygonal Exposure Control Sensing Region</summary>
		public readonly static DicomTag VerticesOfThePolygonalExposureControlSensingRegion = new DicomTag(0x0018, 0x9442);

		///<summary>(0018,9447) VR=FL VM=1 Column Angulation (Patient)</summary>
		public readonly static DicomTag ColumnAngulationPatient = new DicomTag(0x0018, 0x9447);

		///<summary>(0018,9449) VR=FL VM=1 Beam Angle</summary>
		public readonly static DicomTag BeamAngle = new DicomTag(0x0018, 0x9449);

		///<summary>(0018,9451) VR=SQ VM=1 Frame Detector Parameters Sequence</summary>
		public readonly static DicomTag FrameDetectorParametersSequence = new DicomTag(0x0018, 0x9451);

		///<summary>(0018,9452) VR=FL VM=1 Calculated Anatomy Thickness</summary>
		public readonly static DicomTag CalculatedAnatomyThickness = new DicomTag(0x0018, 0x9452);

		///<summary>(0018,9455) VR=SQ VM=1 Calibration Sequence</summary>
		public readonly static DicomTag CalibrationSequence = new DicomTag(0x0018, 0x9455);

		///<summary>(0018,9456) VR=SQ VM=1 Object Thickness Sequence</summary>
		public readonly static DicomTag ObjectThicknessSequence = new DicomTag(0x0018, 0x9456);

		///<summary>(0018,9457) VR=CS VM=1 Plane Identification</summary>
		public readonly static DicomTag PlaneIdentification = new DicomTag(0x0018, 0x9457);

		///<summary>(0018,9461) VR=FL VM=1-2 Field of View Dimension(s) in Float</summary>
		public readonly static DicomTag FieldOfViewDimensionsInFloat = new DicomTag(0x0018, 0x9461);

		///<summary>(0018,9462) VR=SQ VM=1 Isocenter Reference System Sequence</summary>
		public readonly static DicomTag IsocenterReferenceSystemSequence = new DicomTag(0x0018, 0x9462);

		///<summary>(0018,9463) VR=FL VM=1 Positioner Isocenter Primary Angle</summary>
		public readonly static DicomTag PositionerIsocenterPrimaryAngle = new DicomTag(0x0018, 0x9463);

		///<summary>(0018,9464) VR=FL VM=1 Positioner Isocenter Secondary Angle</summary>
		public readonly static DicomTag PositionerIsocenterSecondaryAngle = new DicomTag(0x0018, 0x9464);

		///<summary>(0018,9465) VR=FL VM=1 Positioner Isocenter Detector Rotation Angle</summary>
		public readonly static DicomTag PositionerIsocenterDetectorRotationAngle = new DicomTag(0x0018, 0x9465);

		///<summary>(0018,9466) VR=FL VM=1 Table X Position to Isocenter</summary>
		public readonly static DicomTag TableXPositionToIsocenter = new DicomTag(0x0018, 0x9466);

		///<summary>(0018,9467) VR=FL VM=1 Table Y Position to Isocenter</summary>
		public readonly static DicomTag TableYPositionToIsocenter = new DicomTag(0x0018, 0x9467);

		///<summary>(0018,9468) VR=FL VM=1 Table Z Position to Isocenter</summary>
		public readonly static DicomTag TableZPositionToIsocenter = new DicomTag(0x0018, 0x9468);

		///<summary>(0018,9469) VR=FL VM=1 Table Horizontal Rotation Angle</summary>
		public readonly static DicomTag TableHorizontalRotationAngle = new DicomTag(0x0018, 0x9469);

		///<summary>(0018,9470) VR=FL VM=1 Table Head Tilt Angle</summary>
		public readonly static DicomTag TableHeadTiltAngle = new DicomTag(0x0018, 0x9470);

		///<summary>(0018,9471) VR=FL VM=1 Table Cradle Tilt Angle</summary>
		public readonly static DicomTag TableCradleTiltAngle = new DicomTag(0x0018, 0x9471);

		///<summary>(0018,9472) VR=SQ VM=1 Frame Display Shutter Sequence</summary>
		public readonly static DicomTag FrameDisplayShutterSequence = new DicomTag(0x0018, 0x9472);

		///<summary>(0018,9473) VR=FL VM=1 Acquired Image Area Dose Product</summary>
		public readonly static DicomTag AcquiredImageAreaDoseProduct = new DicomTag(0x0018, 0x9473);

		///<summary>(0018,9474) VR=CS VM=1 C-arm Positioner Tabletop Relationship</summary>
		public readonly static DicomTag CArmPositionerTabletopRelationship = new DicomTag(0x0018, 0x9474);

		///<summary>(0018,9476) VR=SQ VM=1 X-Ray Geometry Sequence</summary>
		public readonly static DicomTag XRayGeometrySequence = new DicomTag(0x0018, 0x9476);

		///<summary>(0018,9477) VR=SQ VM=1 Irradiation Event Identification Sequence</summary>
		public readonly static DicomTag IrradiationEventIdentificationSequence = new DicomTag(0x0018, 0x9477);

		///<summary>(0018,9504) VR=SQ VM=1 X-Ray 3D Frame Type Sequence</summary>
		public readonly static DicomTag XRay3DFrameTypeSequence = new DicomTag(0x0018, 0x9504);

		///<summary>(0018,9506) VR=SQ VM=1 Contributing Sources Sequence</summary>
		public readonly static DicomTag ContributingSourcesSequence = new DicomTag(0x0018, 0x9506);

		///<summary>(0018,9507) VR=SQ VM=1 X-Ray 3D Acquisition Sequence</summary>
		public readonly static DicomTag XRay3DAcquisitionSequence = new DicomTag(0x0018, 0x9507);

		///<summary>(0018,9508) VR=FL VM=1 Primary Positioner Scan Arc</summary>
		public readonly static DicomTag PrimaryPositionerScanArc = new DicomTag(0x0018, 0x9508);

		///<summary>(0018,9509) VR=FL VM=1 Secondary Positioner Scan Arc</summary>
		public readonly static DicomTag SecondaryPositionerScanArc = new DicomTag(0x0018, 0x9509);

		///<summary>(0018,9510) VR=FL VM=1 Primary Positioner Scan Start Angle</summary>
		public readonly static DicomTag PrimaryPositionerScanStartAngle = new DicomTag(0x0018, 0x9510);

		///<summary>(0018,9511) VR=FL VM=1 Secondary Positioner Scan Start Angle</summary>
		public readonly static DicomTag SecondaryPositionerScanStartAngle = new DicomTag(0x0018, 0x9511);

		///<summary>(0018,9514) VR=FL VM=1 Primary Positioner Increment</summary>
		public readonly static DicomTag PrimaryPositionerIncrement = new DicomTag(0x0018, 0x9514);

		///<summary>(0018,9515) VR=FL VM=1 Secondary Positioner Increment</summary>
		public readonly static DicomTag SecondaryPositionerIncrement = new DicomTag(0x0018, 0x9515);

		///<summary>(0018,9516) VR=DT VM=1 Start Acquisition DateTime</summary>
		public readonly static DicomTag StartAcquisitionDateTime = new DicomTag(0x0018, 0x9516);

		///<summary>(0018,9517) VR=DT VM=1 End Acquisition DateTime</summary>
		public readonly static DicomTag EndAcquisitionDateTime = new DicomTag(0x0018, 0x9517);

		///<summary>(0018,9524) VR=LO VM=1 Application Name</summary>
		public readonly static DicomTag ApplicationName = new DicomTag(0x0018, 0x9524);

		///<summary>(0018,9525) VR=LO VM=1 Application Version</summary>
		public readonly static DicomTag ApplicationVersion = new DicomTag(0x0018, 0x9525);

		///<summary>(0018,9526) VR=LO VM=1 Application Manufacturer</summary>
		public readonly static DicomTag ApplicationManufacturer = new DicomTag(0x0018, 0x9526);

		///<summary>(0018,9527) VR=CS VM=1 Algorithm Type</summary>
		public readonly static DicomTag AlgorithmType = new DicomTag(0x0018, 0x9527);

		///<summary>(0018,9528) VR=LO VM=1 Algorithm Description</summary>
		public readonly static DicomTag AlgorithmDescription = new DicomTag(0x0018, 0x9528);

		///<summary>(0018,9530) VR=SQ VM=1 X-Ray 3D Reconstruction Sequence</summary>
		public readonly static DicomTag XRay3DReconstructionSequence = new DicomTag(0x0018, 0x9530);

		///<summary>(0018,9531) VR=LO VM=1 Reconstruction Description</summary>
		public readonly static DicomTag ReconstructionDescription = new DicomTag(0x0018, 0x9531);

		///<summary>(0018,9538) VR=SQ VM=1 Per Projection Acquisition Sequence</summary>
		public readonly static DicomTag PerProjectionAcquisitionSequence = new DicomTag(0x0018, 0x9538);

		///<summary>(0018,9601) VR=SQ VM=1 Diffusion b-matrix Sequence</summary>
		public readonly static DicomTag DiffusionBMatrixSequence = new DicomTag(0x0018, 0x9601);

		///<summary>(0018,9602) VR=FD VM=1 Diffusion b-value XX</summary>
		public readonly static DicomTag DiffusionBValueXX = new DicomTag(0x0018, 0x9602);

		///<summary>(0018,9603) VR=FD VM=1 Diffusion b-value XY</summary>
		public readonly static DicomTag DiffusionBValueXY = new DicomTag(0x0018, 0x9603);

		///<summary>(0018,9604) VR=FD VM=1 Diffusion b-value XZ</summary>
		public readonly static DicomTag DiffusionBValueXZ = new DicomTag(0x0018, 0x9604);

		///<summary>(0018,9605) VR=FD VM=1 Diffusion b-value YY</summary>
		public readonly static DicomTag DiffusionBValueYY = new DicomTag(0x0018, 0x9605);

		///<summary>(0018,9606) VR=FD VM=1 Diffusion b-value YZ</summary>
		public readonly static DicomTag DiffusionBValueYZ = new DicomTag(0x0018, 0x9606);

		///<summary>(0018,9607) VR=FD VM=1 Diffusion b-value ZZ</summary>
		public readonly static DicomTag DiffusionBValueZZ = new DicomTag(0x0018, 0x9607);

		///<summary>(0018,9701) VR=DT VM=1 Decay Correction DateTime</summary>
		public readonly static DicomTag DecayCorrectionDateTime = new DicomTag(0x0018, 0x9701);

		///<summary>(0018,9715) VR=FD VM=1 Start Density Threshold</summary>
		public readonly static DicomTag StartDensityThreshold = new DicomTag(0x0018, 0x9715);

		///<summary>(0018,9716) VR=FD VM=1 Start Relative Density Difference Threshold</summary>
		public readonly static DicomTag StartRelativeDensityDifferenceThreshold = new DicomTag(0x0018, 0x9716);

		///<summary>(0018,9717) VR=FD VM=1 Start Cardiac Trigger Count Threshold</summary>
		public readonly static DicomTag StartCardiacTriggerCountThreshold = new DicomTag(0x0018, 0x9717);

		///<summary>(0018,9718) VR=FD VM=1 Start Respiratory Trigger Count Threshold</summary>
		public readonly static DicomTag StartRespiratoryTriggerCountThreshold = new DicomTag(0x0018, 0x9718);

		///<summary>(0018,9719) VR=FD VM=1 Termination Counts Threshold</summary>
		public readonly static DicomTag TerminationCountsThreshold = new DicomTag(0x0018, 0x9719);

		///<summary>(0018,9720) VR=FD VM=1 Termination Density Threshold</summary>
		public readonly static DicomTag TerminationDensityThreshold = new DicomTag(0x0018, 0x9720);

		///<summary>(0018,9721) VR=FD VM=1 Termination Relative Density Threshold</summary>
		public readonly static DicomTag TerminationRelativeDensityThreshold = new DicomTag(0x0018, 0x9721);

		///<summary>(0018,9722) VR=FD VM=1 Termination Time Threshold</summary>
		public readonly static DicomTag TerminationTimeThreshold = new DicomTag(0x0018, 0x9722);

		///<summary>(0018,9723) VR=FD VM=1 Termination Cardiac Trigger Count Threshold</summary>
		public readonly static DicomTag TerminationCardiacTriggerCountThreshold = new DicomTag(0x0018, 0x9723);

		///<summary>(0018,9724) VR=FD VM=1 Termination Respiratory Trigger Count Threshold</summary>
		public readonly static DicomTag TerminationRespiratoryTriggerCountThreshold = new DicomTag(0x0018, 0x9724);

		///<summary>(0018,9725) VR=CS VM=1 Detector Geometry</summary>
		public readonly static DicomTag DetectorGeometry = new DicomTag(0x0018, 0x9725);

		///<summary>(0018,9726) VR=FD VM=1 Transverse Detector Separation</summary>
		public readonly static DicomTag TransverseDetectorSeparation = new DicomTag(0x0018, 0x9726);

		///<summary>(0018,9727) VR=FD VM=1 Axial Detector Dimension</summary>
		public readonly static DicomTag AxialDetectorDimension = new DicomTag(0x0018, 0x9727);

		///<summary>(0018,9729) VR=US VM=1 Radiopharmaceutical Agent Number</summary>
		public readonly static DicomTag RadiopharmaceuticalAgentNumber = new DicomTag(0x0018, 0x9729);

		///<summary>(0018,9732) VR=SQ VM=1 PET Frame Acquisition Sequence</summary>
		public readonly static DicomTag PETFrameAcquisitionSequence = new DicomTag(0x0018, 0x9732);

		///<summary>(0018,9733) VR=SQ VM=1 PET Detector Motion Details Sequence</summary>
		public readonly static DicomTag PETDetectorMotionDetailsSequence = new DicomTag(0x0018, 0x9733);

		///<summary>(0018,9734) VR=SQ VM=1 PET Table Dynamics Sequence</summary>
		public readonly static DicomTag PETTableDynamicsSequence = new DicomTag(0x0018, 0x9734);

		///<summary>(0018,9735) VR=SQ VM=1 PET Position Sequence</summary>
		public readonly static DicomTag PETPositionSequence = new DicomTag(0x0018, 0x9735);

		///<summary>(0018,9736) VR=SQ VM=1 PET Frame Correction Factors Sequence</summary>
		public readonly static DicomTag PETFrameCorrectionFactorsSequence = new DicomTag(0x0018, 0x9736);

		///<summary>(0018,9737) VR=SQ VM=1 Radiopharmaceutical Usage Sequence</summary>
		public readonly static DicomTag RadiopharmaceuticalUsageSequence = new DicomTag(0x0018, 0x9737);

		///<summary>(0018,9738) VR=CS VM=1 Attenuation Correction Source</summary>
		public readonly static DicomTag AttenuationCorrectionSource = new DicomTag(0x0018, 0x9738);

		///<summary>(0018,9739) VR=US VM=1 Number of Iterations</summary>
		public readonly static DicomTag NumberOfIterations = new DicomTag(0x0018, 0x9739);

		///<summary>(0018,9740) VR=US VM=1 Number of Subsets</summary>
		public readonly static DicomTag NumberOfSubsets = new DicomTag(0x0018, 0x9740);

		///<summary>(0018,9749) VR=SQ VM=1 PET Reconstruction Sequence</summary>
		public readonly static DicomTag PETReconstructionSequence = new DicomTag(0x0018, 0x9749);

		///<summary>(0018,9751) VR=SQ VM=1 PET Frame Type Sequence</summary>
		public readonly static DicomTag PETFrameTypeSequence = new DicomTag(0x0018, 0x9751);

		///<summary>(0018,9755) VR=CS VM=1 Time of Flight Information Used</summary>
		public readonly static DicomTag TimeOfFlightInformationUsed = new DicomTag(0x0018, 0x9755);

		///<summary>(0018,9756) VR=CS VM=1 Reconstruction Type</summary>
		public readonly static DicomTag ReconstructionType = new DicomTag(0x0018, 0x9756);

		///<summary>(0018,9758) VR=CS VM=1 Decay Corrected</summary>
		public readonly static DicomTag DecayCorrected = new DicomTag(0x0018, 0x9758);

		///<summary>(0018,9759) VR=CS VM=1 Attenuation Corrected</summary>
		public readonly static DicomTag AttenuationCorrected = new DicomTag(0x0018, 0x9759);

		///<summary>(0018,9760) VR=CS VM=1 Scatter Corrected</summary>
		public readonly static DicomTag ScatterCorrected = new DicomTag(0x0018, 0x9760);

		///<summary>(0018,9761) VR=CS VM=1 Dead Time Corrected</summary>
		public readonly static DicomTag DeadTimeCorrected = new DicomTag(0x0018, 0x9761);

		///<summary>(0018,9762) VR=CS VM=1 Gantry Motion Corrected</summary>
		public readonly static DicomTag GantryMotionCorrected = new DicomTag(0x0018, 0x9762);

		///<summary>(0018,9763) VR=CS VM=1 Patient Motion Corrected</summary>
		public readonly static DicomTag PatientMotionCorrected = new DicomTag(0x0018, 0x9763);

		///<summary>(0018,9764) VR=CS VM=1 Count Loss Normalization Corrected</summary>
		public readonly static DicomTag CountLossNormalizationCorrected = new DicomTag(0x0018, 0x9764);

		///<summary>(0018,9765) VR=CS VM=1 Randoms Corrected</summary>
		public readonly static DicomTag RandomsCorrected = new DicomTag(0x0018, 0x9765);

		///<summary>(0018,9766) VR=CS VM=1 Non-uniform Radial Sampling Corrected</summary>
		public readonly static DicomTag NonUniformRadialSamplingCorrected = new DicomTag(0x0018, 0x9766);

		///<summary>(0018,9767) VR=CS VM=1 Sensitivity Calibrated</summary>
		public readonly static DicomTag SensitivityCalibrated = new DicomTag(0x0018, 0x9767);

		///<summary>(0018,9768) VR=CS VM=1 Detector Normalization Correction</summary>
		public readonly static DicomTag DetectorNormalizationCorrection = new DicomTag(0x0018, 0x9768);

		///<summary>(0018,9769) VR=CS VM=1 Iterative Reconstruction Method</summary>
		public readonly static DicomTag IterativeReconstructionMethod = new DicomTag(0x0018, 0x9769);

		///<summary>(0018,9770) VR=CS VM=1 Attenuation Correction Temporal Relationship</summary>
		public readonly static DicomTag AttenuationCorrectionTemporalRelationship = new DicomTag(0x0018, 0x9770);

		///<summary>(0018,9771) VR=SQ VM=1 Patient Physiological State Sequence</summary>
		public readonly static DicomTag PatientPhysiologicalStateSequence = new DicomTag(0x0018, 0x9771);

		///<summary>(0018,9772) VR=SQ VM=1 Patient Physiological State Code Sequence</summary>
		public readonly static DicomTag PatientPhysiologicalStateCodeSequence = new DicomTag(0x0018, 0x9772);

		///<summary>(0018,9801) VR=FD VM=1-n Depth(s) of Focus</summary>
		public readonly static DicomTag DepthsOfFocus = new DicomTag(0x0018, 0x9801);

		///<summary>(0018,9803) VR=SQ VM=1 Excluded Intervals Sequence</summary>
		public readonly static DicomTag ExcludedIntervalsSequence = new DicomTag(0x0018, 0x9803);

		///<summary>(0018,9804) VR=DT VM=1 Exclusion Start Datetime</summary>
		public readonly static DicomTag ExclusionStartDatetime = new DicomTag(0x0018, 0x9804);

		///<summary>(0018,9805) VR=FD VM=1 Exclusion Duration</summary>
		public readonly static DicomTag ExclusionDuration = new DicomTag(0x0018, 0x9805);

		///<summary>(0018,9806) VR=SQ VM=1 US Image Description Sequence</summary>
		public readonly static DicomTag USImageDescriptionSequence = new DicomTag(0x0018, 0x9806);

		///<summary>(0018,9807) VR=SQ VM=1 Image Data Type Sequence</summary>
		public readonly static DicomTag ImageDataTypeSequence = new DicomTag(0x0018, 0x9807);

		///<summary>(0018,9808) VR=CS VM=1 Data Type</summary>
		public readonly static DicomTag DataType = new DicomTag(0x0018, 0x9808);

		///<summary>(0018,9809) VR=SQ VM=1 Transducer Scan Pattern Code Sequence</summary>
		public readonly static DicomTag TransducerScanPatternCodeSequence = new DicomTag(0x0018, 0x9809);

		///<summary>(0018,980b) VR=CS VM=1 Aliased Data Type</summary>
		public readonly static DicomTag AliasedDataType = new DicomTag(0x0018, 0x980b);

		///<summary>(0018,980c) VR=CS VM=1 Position Measuring Device Used</summary>
		public readonly static DicomTag PositionMeasuringDeviceUsed = new DicomTag(0x0018, 0x980c);

		///<summary>(0018,980d) VR=SQ VM=1 Transducer Geometry Code Sequence</summary>
		public readonly static DicomTag TransducerGeometryCodeSequence = new DicomTag(0x0018, 0x980d);

		///<summary>(0018,980e) VR=SQ VM=1 Transducer Beam Steering Code Sequence</summary>
		public readonly static DicomTag TransducerBeamSteeringCodeSequence = new DicomTag(0x0018, 0x980e);

		///<summary>(0018,980f) VR=SQ VM=1 Transducer Application Code Sequence</summary>
		public readonly static DicomTag TransducerApplicationCodeSequence = new DicomTag(0x0018, 0x980f);

		///<summary>(0018,a001) VR=SQ VM=1 Contributing Equipment Sequence</summary>
		public readonly static DicomTag ContributingEquipmentSequence = new DicomTag(0x0018, 0xa001);

		///<summary>(0018,a002) VR=DT VM=1 Contribution Date Time</summary>
		public readonly static DicomTag ContributionDateTime = new DicomTag(0x0018, 0xa002);

		///<summary>(0018,a003) VR=ST VM=1 Contribution Description</summary>
		public readonly static DicomTag ContributionDescription = new DicomTag(0x0018, 0xa003);

		///<summary>(0020,000d) VR=UI VM=1 Study Instance UID</summary>
		public readonly static DicomTag StudyInstanceUID = new DicomTag(0x0020, 0x000d);

		///<summary>(0020,000e) VR=UI VM=1 Series Instance UID</summary>
		public readonly static DicomTag SeriesInstanceUID = new DicomTag(0x0020, 0x000e);

		///<summary>(0020,0010) VR=SH VM=1 Study ID</summary>
		public readonly static DicomTag StudyID = new DicomTag(0x0020, 0x0010);

		///<summary>(0020,0011) VR=IS VM=1 Series Number</summary>
		public readonly static DicomTag SeriesNumber = new DicomTag(0x0020, 0x0011);

		///<summary>(0020,0012) VR=IS VM=1 Acquisition Number</summary>
		public readonly static DicomTag AcquisitionNumber = new DicomTag(0x0020, 0x0012);

		///<summary>(0020,0013) VR=IS VM=1 Instance Number</summary>
		public readonly static DicomTag InstanceNumber = new DicomTag(0x0020, 0x0013);

		///<summary>(0020,0014) VR=IS VM=1 Isotope Number (RETIRED)</summary>
		public readonly static DicomTag IsotopeNumberRETIRED = new DicomTag(0x0020, 0x0014);

		///<summary>(0020,0015) VR=IS VM=1 Phase Number (RETIRED)</summary>
		public readonly static DicomTag PhaseNumberRETIRED = new DicomTag(0x0020, 0x0015);

		///<summary>(0020,0016) VR=IS VM=1 Interval Number (RETIRED)</summary>
		public readonly static DicomTag IntervalNumberRETIRED = new DicomTag(0x0020, 0x0016);

		///<summary>(0020,0017) VR=IS VM=1 Time Slot Number (RETIRED)</summary>
		public readonly static DicomTag TimeSlotNumberRETIRED = new DicomTag(0x0020, 0x0017);

		///<summary>(0020,0018) VR=IS VM=1 Angle Number (RETIRED)</summary>
		public readonly static DicomTag AngleNumberRETIRED = new DicomTag(0x0020, 0x0018);

		///<summary>(0020,0019) VR=IS VM=1 Item Number</summary>
		public readonly static DicomTag ItemNumber = new DicomTag(0x0020, 0x0019);

		///<summary>(0020,0020) VR=CS VM=2 Patient Orientation</summary>
		public readonly static DicomTag PatientOrientation = new DicomTag(0x0020, 0x0020);

		///<summary>(0020,0022) VR=IS VM=1 Overlay Number (RETIRED)</summary>
		public readonly static DicomTag OverlayNumberRETIRED = new DicomTag(0x0020, 0x0022);

		///<summary>(0020,0024) VR=IS VM=1 Curve Number (RETIRED)</summary>
		public readonly static DicomTag CurveNumberRETIRED = new DicomTag(0x0020, 0x0024);

		///<summary>(0020,0026) VR=IS VM=1 LUT Number (RETIRED)</summary>
		public readonly static DicomTag LUTNumberRETIRED = new DicomTag(0x0020, 0x0026);

		///<summary>(0020,0030) VR=DS VM=3 Image Position (RETIRED)</summary>
		public readonly static DicomTag ImagePositionRETIRED = new DicomTag(0x0020, 0x0030);

		///<summary>(0020,0032) VR=DS VM=3 Image Position (Patient)</summary>
		public readonly static DicomTag ImagePositionPatient = new DicomTag(0x0020, 0x0032);

		///<summary>(0020,0035) VR=DS VM=6 Image Orientation (RETIRED)</summary>
		public readonly static DicomTag ImageOrientationRETIRED = new DicomTag(0x0020, 0x0035);

		///<summary>(0020,0037) VR=DS VM=6 Image Orientation (Patient)</summary>
		public readonly static DicomTag ImageOrientationPatient = new DicomTag(0x0020, 0x0037);

		///<summary>(0020,0050) VR=DS VM=1 Location (RETIRED)</summary>
		public readonly static DicomTag LocationRETIRED = new DicomTag(0x0020, 0x0050);

		///<summary>(0020,0052) VR=UI VM=1 Frame of Reference UID</summary>
		public readonly static DicomTag FrameOfReferenceUID = new DicomTag(0x0020, 0x0052);

		///<summary>(0020,0060) VR=CS VM=1 Laterality</summary>
		public readonly static DicomTag Laterality = new DicomTag(0x0020, 0x0060);

		///<summary>(0020,0062) VR=CS VM=1 Image Laterality</summary>
		public readonly static DicomTag ImageLaterality = new DicomTag(0x0020, 0x0062);

		///<summary>(0020,0070) VR=LO VM=1 Image Geometry Type (RETIRED)</summary>
		public readonly static DicomTag ImageGeometryTypeRETIRED = new DicomTag(0x0020, 0x0070);

		///<summary>(0020,0080) VR=CS VM=1-n Masking Image (RETIRED)</summary>
		public readonly static DicomTag MaskingImageRETIRED = new DicomTag(0x0020, 0x0080);

		///<summary>(0020,00aa) VR=IS VM=1 Report Number (RETIRED)</summary>
		public readonly static DicomTag ReportNumberRETIRED = new DicomTag(0x0020, 0x00aa);

		///<summary>(0020,0100) VR=IS VM=1 Temporal Position Identifier</summary>
		public readonly static DicomTag TemporalPositionIdentifier = new DicomTag(0x0020, 0x0100);

		///<summary>(0020,0105) VR=IS VM=1 Number of Temporal Positions</summary>
		public readonly static DicomTag NumberOfTemporalPositions = new DicomTag(0x0020, 0x0105);

		///<summary>(0020,0110) VR=DS VM=1 Temporal Resolution</summary>
		public readonly static DicomTag TemporalResolution = new DicomTag(0x0020, 0x0110);

		///<summary>(0020,0200) VR=UI VM=1 Synchronization Frame of Reference UID</summary>
		public readonly static DicomTag SynchronizationFrameOfReferenceUID = new DicomTag(0x0020, 0x0200);

		///<summary>(0020,0242) VR=UI VM=1 SOP Instance UID of Concatenation Source</summary>
		public readonly static DicomTag SOPInstanceUIDOfConcatenationSource = new DicomTag(0x0020, 0x0242);

		///<summary>(0020,1000) VR=IS VM=1 Series in Study (RETIRED)</summary>
		public readonly static DicomTag SeriesInStudyRETIRED = new DicomTag(0x0020, 0x1000);

		///<summary>(0020,1001) VR=IS VM=1 Acquisitions in Series (RETIRED)</summary>
		public readonly static DicomTag AcquisitionsInSeriesRETIRED = new DicomTag(0x0020, 0x1001);

		///<summary>(0020,1002) VR=IS VM=1 Images in Acquisition</summary>
		public readonly static DicomTag ImagesInAcquisition = new DicomTag(0x0020, 0x1002);

		///<summary>(0020,1003) VR=IS VM=1 Images in Series (RETIRED)</summary>
		public readonly static DicomTag ImagesInSeriesRETIRED = new DicomTag(0x0020, 0x1003);

		///<summary>(0020,1004) VR=IS VM=1 Acquisitions in Study (RETIRED)</summary>
		public readonly static DicomTag AcquisitionsInStudyRETIRED = new DicomTag(0x0020, 0x1004);

		///<summary>(0020,1005) VR=IS VM=1 Images in Study (RETIRED)</summary>
		public readonly static DicomTag ImagesInStudyRETIRED = new DicomTag(0x0020, 0x1005);

		///<summary>(0020,1020) VR=LO VM=1-n Reference (RETIRED)</summary>
		public readonly static DicomTag ReferenceRETIRED = new DicomTag(0x0020, 0x1020);

		///<summary>(0020,1040) VR=LO VM=1 Position Reference Indicator</summary>
		public readonly static DicomTag PositionReferenceIndicator = new DicomTag(0x0020, 0x1040);

		///<summary>(0020,1041) VR=DS VM=1 Slice Location</summary>
		public readonly static DicomTag SliceLocation = new DicomTag(0x0020, 0x1041);

		///<summary>(0020,1070) VR=IS VM=1-n Other Study Numbers (RETIRED)</summary>
		public readonly static DicomTag OtherStudyNumbersRETIRED = new DicomTag(0x0020, 0x1070);

		///<summary>(0020,1200) VR=IS VM=1 Number of Patient Related Studies</summary>
		public readonly static DicomTag NumberOfPatientRelatedStudies = new DicomTag(0x0020, 0x1200);

		///<summary>(0020,1202) VR=IS VM=1 Number of Patient Related Series</summary>
		public readonly static DicomTag NumberOfPatientRelatedSeries = new DicomTag(0x0020, 0x1202);

		///<summary>(0020,1204) VR=IS VM=1 Number of Patient Related Instances</summary>
		public readonly static DicomTag NumberOfPatientRelatedInstances = new DicomTag(0x0020, 0x1204);

		///<summary>(0020,1206) VR=IS VM=1 Number of Study Related Series</summary>
		public readonly static DicomTag NumberOfStudyRelatedSeries = new DicomTag(0x0020, 0x1206);

		///<summary>(0020,1208) VR=IS VM=1 Number of Study Related Instances</summary>
		public readonly static DicomTag NumberOfStudyRelatedInstances = new DicomTag(0x0020, 0x1208);

		///<summary>(0020,1209) VR=IS VM=1 Number of Series Related Instances</summary>
		public readonly static DicomTag NumberOfSeriesRelatedInstances = new DicomTag(0x0020, 0x1209);

		///<summary>(0020,3401) VR=CS VM=1 Modifying Device ID (RETIRED)</summary>
		public readonly static DicomTag ModifyingDeviceIDRETIRED = new DicomTag(0x0020, 0x3401);

		///<summary>(0020,3402) VR=CS VM=1 Modified Image ID (RETIRED)</summary>
		public readonly static DicomTag ModifiedImageIDRETIRED = new DicomTag(0x0020, 0x3402);

		///<summary>(0020,3403) VR=DA VM=1 Modified Image Date (RETIRED)</summary>
		public readonly static DicomTag ModifiedImageDateRETIRED = new DicomTag(0x0020, 0x3403);

		///<summary>(0020,3404) VR=LO VM=1 Modifying Device Manufacturer (RETIRED)</summary>
		public readonly static DicomTag ModifyingDeviceManufacturerRETIRED = new DicomTag(0x0020, 0x3404);

		///<summary>(0020,3405) VR=TM VM=1 Modified Image Time (RETIRED)</summary>
		public readonly static DicomTag ModifiedImageTimeRETIRED = new DicomTag(0x0020, 0x3405);

		///<summary>(0020,3406) VR=LO VM=1 Modified Image Description (RETIRED)</summary>
		public readonly static DicomTag ModifiedImageDescriptionRETIRED = new DicomTag(0x0020, 0x3406);

		///<summary>(0020,4000) VR=LT VM=1 Image Comments</summary>
		public readonly static DicomTag ImageComments = new DicomTag(0x0020, 0x4000);

		///<summary>(0020,5000) VR=AT VM=1-n Original Image Identification (RETIRED)</summary>
		public readonly static DicomTag OriginalImageIdentificationRETIRED = new DicomTag(0x0020, 0x5000);

		///<summary>(0020,5002) VR=LO VM=1-n Original Image Identification Nomenclature (RETIRED)</summary>
		public readonly static DicomTag OriginalImageIdentificationNomenclatureRETIRED = new DicomTag(0x0020, 0x5002);

		///<summary>(0020,9056) VR=SH VM=1 Stack ID</summary>
		public readonly static DicomTag StackID = new DicomTag(0x0020, 0x9056);

		///<summary>(0020,9057) VR=UL VM=1 In-Stack Position Number</summary>
		public readonly static DicomTag InStackPositionNumber = new DicomTag(0x0020, 0x9057);

		///<summary>(0020,9071) VR=SQ VM=1 Frame Anatomy Sequence</summary>
		public readonly static DicomTag FrameAnatomySequence = new DicomTag(0x0020, 0x9071);

		///<summary>(0020,9072) VR=CS VM=1 Frame Laterality</summary>
		public readonly static DicomTag FrameLaterality = new DicomTag(0x0020, 0x9072);

		///<summary>(0020,9111) VR=SQ VM=1 Frame Content Sequence</summary>
		public readonly static DicomTag FrameContentSequence = new DicomTag(0x0020, 0x9111);

		///<summary>(0020,9113) VR=SQ VM=1 Plane Position Sequence</summary>
		public readonly static DicomTag PlanePositionSequence = new DicomTag(0x0020, 0x9113);

		///<summary>(0020,9116) VR=SQ VM=1 Plane Orientation Sequence</summary>
		public readonly static DicomTag PlaneOrientationSequence = new DicomTag(0x0020, 0x9116);

		///<summary>(0020,9128) VR=UL VM=1 Temporal Position Index</summary>
		public readonly static DicomTag TemporalPositionIndex = new DicomTag(0x0020, 0x9128);

		///<summary>(0020,9153) VR=FD VM=1 Nominal Cardiac Trigger Delay Time</summary>
		public readonly static DicomTag NominalCardiacTriggerDelayTime = new DicomTag(0x0020, 0x9153);

		///<summary>(0020,9154) VR=FL VM=1 Nominal Cardiac Trigger Time Prior To R-Peak</summary>
		public readonly static DicomTag NominalCardiacTriggerTimePriorToRPeak = new DicomTag(0x0020, 0x9154);

		///<summary>(0020,9155) VR=FL VM=1 Actual Cardiac Trigger Time Prior To R-Peak</summary>
		public readonly static DicomTag ActualCardiacTriggerTimePriorToRPeak = new DicomTag(0x0020, 0x9155);

		///<summary>(0020,9156) VR=US VM=1 Frame Acquisition Number</summary>
		public readonly static DicomTag FrameAcquisitionNumber = new DicomTag(0x0020, 0x9156);

		///<summary>(0020,9157) VR=UL VM=1-n Dimension Index Values</summary>
		public readonly static DicomTag DimensionIndexValues = new DicomTag(0x0020, 0x9157);

		///<summary>(0020,9158) VR=LT VM=1 Frame Comments</summary>
		public readonly static DicomTag FrameComments = new DicomTag(0x0020, 0x9158);

		///<summary>(0020,9161) VR=UI VM=1 Concatenation UID</summary>
		public readonly static DicomTag ConcatenationUID = new DicomTag(0x0020, 0x9161);

		///<summary>(0020,9162) VR=US VM=1 In-concatenation Number</summary>
		public readonly static DicomTag InConcatenationNumber = new DicomTag(0x0020, 0x9162);

		///<summary>(0020,9163) VR=US VM=1 In-concatenation Total Number</summary>
		public readonly static DicomTag InConcatenationTotalNumber = new DicomTag(0x0020, 0x9163);

		///<summary>(0020,9164) VR=UI VM=1 Dimension Organization UID</summary>
		public readonly static DicomTag DimensionOrganizationUID = new DicomTag(0x0020, 0x9164);

		///<summary>(0020,9165) VR=AT VM=1 Dimension Index Pointer</summary>
		public readonly static DicomTag DimensionIndexPointer = new DicomTag(0x0020, 0x9165);

		///<summary>(0020,9167) VR=AT VM=1 Functional Group Pointer</summary>
		public readonly static DicomTag FunctionalGroupPointer = new DicomTag(0x0020, 0x9167);

		///<summary>(0020,9213) VR=LO VM=1 Dimension Index Private Creator</summary>
		public readonly static DicomTag DimensionIndexPrivateCreator = new DicomTag(0x0020, 0x9213);

		///<summary>(0020,9221) VR=SQ VM=1 Dimension Organization Sequence</summary>
		public readonly static DicomTag DimensionOrganizationSequence = new DicomTag(0x0020, 0x9221);

		///<summary>(0020,9222) VR=SQ VM=1 Dimension Index Sequence</summary>
		public readonly static DicomTag DimensionIndexSequence = new DicomTag(0x0020, 0x9222);

		///<summary>(0020,9228) VR=UL VM=1 Concatenation Frame Offset Number</summary>
		public readonly static DicomTag ConcatenationFrameOffsetNumber = new DicomTag(0x0020, 0x9228);

		///<summary>(0020,9238) VR=LO VM=1 Functional Group Private Creator</summary>
		public readonly static DicomTag FunctionalGroupPrivateCreator = new DicomTag(0x0020, 0x9238);

		///<summary>(0020,9241) VR=FL VM=1 Nominal Percentage of Cardiac Phase</summary>
		public readonly static DicomTag NominalPercentageOfCardiacPhase = new DicomTag(0x0020, 0x9241);

		///<summary>(0020,9245) VR=FL VM=1 Nominal Percentage of Respiratory Phase</summary>
		public readonly static DicomTag NominalPercentageOfRespiratoryPhase = new DicomTag(0x0020, 0x9245);

		///<summary>(0020,9246) VR=FL VM=1 Starting Respiratory Amplitude</summary>
		public readonly static DicomTag StartingRespiratoryAmplitude = new DicomTag(0x0020, 0x9246);

		///<summary>(0020,9247) VR=CS VM=1 Starting Respiratory Phase</summary>
		public readonly static DicomTag StartingRespiratoryPhase = new DicomTag(0x0020, 0x9247);

		///<summary>(0020,9248) VR=FL VM=1 Ending Respiratory Amplitude</summary>
		public readonly static DicomTag EndingRespiratoryAmplitude = new DicomTag(0x0020, 0x9248);

		///<summary>(0020,9249) VR=CS VM=1 Ending Respiratory Phase</summary>
		public readonly static DicomTag EndingRespiratoryPhase = new DicomTag(0x0020, 0x9249);

		///<summary>(0020,9250) VR=CS VM=1 Respiratory Trigger Type</summary>
		public readonly static DicomTag RespiratoryTriggerType = new DicomTag(0x0020, 0x9250);

		///<summary>(0020,9251) VR=FD VM=1 R-R Interval Time Nominal</summary>
		public readonly static DicomTag RRIntervalTimeNominal = new DicomTag(0x0020, 0x9251);

		///<summary>(0020,9252) VR=FD VM=1 Actual Cardiac Trigger Delay Time</summary>
		public readonly static DicomTag ActualCardiacTriggerDelayTime = new DicomTag(0x0020, 0x9252);

		///<summary>(0020,9253) VR=SQ VM=1 Respiratory Synchronization Sequence</summary>
		public readonly static DicomTag RespiratorySynchronizationSequence = new DicomTag(0x0020, 0x9253);

		///<summary>(0020,9254) VR=FD VM=1 Respiratory Interval Time</summary>
		public readonly static DicomTag RespiratoryIntervalTime = new DicomTag(0x0020, 0x9254);

		///<summary>(0020,9255) VR=FD VM=1 Nominal Respiratory Trigger Delay Time</summary>
		public readonly static DicomTag NominalRespiratoryTriggerDelayTime = new DicomTag(0x0020, 0x9255);

		///<summary>(0020,9256) VR=FD VM=1 Respiratory Trigger Delay Threshold</summary>
		public readonly static DicomTag RespiratoryTriggerDelayThreshold = new DicomTag(0x0020, 0x9256);

		///<summary>(0020,9257) VR=FD VM=1 Actual Respiratory Trigger Delay Time</summary>
		public readonly static DicomTag ActualRespiratoryTriggerDelayTime = new DicomTag(0x0020, 0x9257);

		///<summary>(0020,9301) VR=FD VM=3 Image Position (Volume)</summary>
		public readonly static DicomTag ImagePositionVolume = new DicomTag(0x0020, 0x9301);

		///<summary>(0020,9302) VR=FD VM=6 Image Orientation (Volume)</summary>
		public readonly static DicomTag ImageOrientationVolume = new DicomTag(0x0020, 0x9302);

		///<summary>(0020,9307) VR=CS VM=1 Ultrasound Acquisition Geometry</summary>
		public readonly static DicomTag UltrasoundAcquisitionGeometry = new DicomTag(0x0020, 0x9307);

		///<summary>(0020,9308) VR=FD VM=3 Apex Position</summary>
		public readonly static DicomTag ApexPosition = new DicomTag(0x0020, 0x9308);

		///<summary>(0020,9309) VR=FD VM=16 Volume to Transducer Mapping Matrix</summary>
		public readonly static DicomTag VolumeToTransducerMappingMatrix = new DicomTag(0x0020, 0x9309);

		///<summary>(0020,930a) VR=FD VM=16 Volume to Table Mapping Matrix</summary>
		public readonly static DicomTag VolumeToTableMappingMatrix = new DicomTag(0x0020, 0x930a);

		///<summary>(0020,930c) VR=CS VM=1 Patient Frame of Reference Source</summary>
		public readonly static DicomTag PatientFrameOfReferenceSource = new DicomTag(0x0020, 0x930c);

		///<summary>(0020,930d) VR=FD VM=1 Temporal Position Time Offset</summary>
		public readonly static DicomTag TemporalPositionTimeOffset = new DicomTag(0x0020, 0x930d);

		///<summary>(0020,930e) VR=SQ VM=1 Plane Position (Volume) Sequence</summary>
		public readonly static DicomTag PlanePositionVolumeSequence = new DicomTag(0x0020, 0x930e);

		///<summary>(0020,930f) VR=SQ VM=1 Plane Orientation (Volume) Sequence</summary>
		public readonly static DicomTag PlaneOrientationVolumeSequence = new DicomTag(0x0020, 0x930f);

		///<summary>(0020,9310) VR=SQ VM=1 Temporal Position Sequence</summary>
		public readonly static DicomTag TemporalPositionSequence = new DicomTag(0x0020, 0x9310);

		///<summary>(0020,9311) VR=CS VM=1 Dimension Organization Type</summary>
		public readonly static DicomTag DimensionOrganizationType = new DicomTag(0x0020, 0x9311);

		///<summary>(0020,9312) VR=UI VM=1 Volume Frame of Reference UID</summary>
		public readonly static DicomTag VolumeFrameOfReferenceUID = new DicomTag(0x0020, 0x9312);

		///<summary>(0020,9313) VR=UI VM=1 Table Frame of Reference UID</summary>
		public readonly static DicomTag TableFrameOfReferenceUID = new DicomTag(0x0020, 0x9313);

		///<summary>(0020,9421) VR=LO VM=1 Dimension Description Label</summary>
		public readonly static DicomTag DimensionDescriptionLabel = new DicomTag(0x0020, 0x9421);

		///<summary>(0020,9450) VR=SQ VM=1 Patient Orientation in Frame Sequence</summary>
		public readonly static DicomTag PatientOrientationInFrameSequence = new DicomTag(0x0020, 0x9450);

		///<summary>(0020,9453) VR=LO VM=1 Frame Label</summary>
		public readonly static DicomTag FrameLabel = new DicomTag(0x0020, 0x9453);

		///<summary>(0020,9518) VR=US VM=1-n Acquisition Index</summary>
		public readonly static DicomTag AcquisitionIndex = new DicomTag(0x0020, 0x9518);

		///<summary>(0020,9529) VR=SQ VM=1 Contributing SOP Instances Reference Sequence</summary>
		public readonly static DicomTag ContributingSOPInstancesReferenceSequence = new DicomTag(0x0020, 0x9529);

		///<summary>(0020,9536) VR=US VM=1 Reconstruction Index</summary>
		public readonly static DicomTag ReconstructionIndex = new DicomTag(0x0020, 0x9536);

		///<summary>(0022,0001) VR=US VM=1 Light Path Filter Pass-Through Wavelength</summary>
		public readonly static DicomTag LightPathFilterPassThroughWavelength = new DicomTag(0x0022, 0x0001);

		///<summary>(0022,0002) VR=US VM=2 Light Path Filter Pass Band</summary>
		public readonly static DicomTag LightPathFilterPassBand = new DicomTag(0x0022, 0x0002);

		///<summary>(0022,0003) VR=US VM=1 Image Path Filter Pass-Through Wavelength</summary>
		public readonly static DicomTag ImagePathFilterPassThroughWavelength = new DicomTag(0x0022, 0x0003);

		///<summary>(0022,0004) VR=US VM=2 Image Path Filter Pass Band</summary>
		public readonly static DicomTag ImagePathFilterPassBand = new DicomTag(0x0022, 0x0004);

		///<summary>(0022,0005) VR=CS VM=1 Patient Eye Movement Commanded</summary>
		public readonly static DicomTag PatientEyeMovementCommanded = new DicomTag(0x0022, 0x0005);

		///<summary>(0022,0006) VR=SQ VM=1 Patient Eye Movement Command Code Sequence</summary>
		public readonly static DicomTag PatientEyeMovementCommandCodeSequence = new DicomTag(0x0022, 0x0006);

		///<summary>(0022,0007) VR=FL VM=1 Spherical Lens Power</summary>
		public readonly static DicomTag SphericalLensPower = new DicomTag(0x0022, 0x0007);

		///<summary>(0022,0008) VR=FL VM=1 Cylinder Lens Power</summary>
		public readonly static DicomTag CylinderLensPower = new DicomTag(0x0022, 0x0008);

		///<summary>(0022,0009) VR=FL VM=1 Cylinder Axis</summary>
		public readonly static DicomTag CylinderAxis = new DicomTag(0x0022, 0x0009);

		///<summary>(0022,000a) VR=FL VM=1 Emmetropic Magnification</summary>
		public readonly static DicomTag EmmetropicMagnification = new DicomTag(0x0022, 0x000a);

		///<summary>(0022,000b) VR=FL VM=1 Intra Ocular Pressure</summary>
		public readonly static DicomTag IntraOcularPressure = new DicomTag(0x0022, 0x000b);

		///<summary>(0022,000c) VR=FL VM=1 Horizontal Field of View</summary>
		public readonly static DicomTag HorizontalFieldOfView = new DicomTag(0x0022, 0x000c);

		///<summary>(0022,000d) VR=CS VM=1 Pupil Dilated</summary>
		public readonly static DicomTag PupilDilated = new DicomTag(0x0022, 0x000d);

		///<summary>(0022,000e) VR=FL VM=1 Degree of Dilation</summary>
		public readonly static DicomTag DegreeOfDilation = new DicomTag(0x0022, 0x000e);

		///<summary>(0022,0010) VR=FL VM=1 Stereo Baseline Angle</summary>
		public readonly static DicomTag StereoBaselineAngle = new DicomTag(0x0022, 0x0010);

		///<summary>(0022,0011) VR=FL VM=1 Stereo Baseline Displacement</summary>
		public readonly static DicomTag StereoBaselineDisplacement = new DicomTag(0x0022, 0x0011);

		///<summary>(0022,0012) VR=FL VM=1 Stereo Horizontal Pixel Offset</summary>
		public readonly static DicomTag StereoHorizontalPixelOffset = new DicomTag(0x0022, 0x0012);

		///<summary>(0022,0013) VR=FL VM=1 Stereo Vertical Pixel Offset</summary>
		public readonly static DicomTag StereoVerticalPixelOffset = new DicomTag(0x0022, 0x0013);

		///<summary>(0022,0014) VR=FL VM=1 Stereo Rotation</summary>
		public readonly static DicomTag StereoRotation = new DicomTag(0x0022, 0x0014);

		///<summary>(0022,0015) VR=SQ VM=1 Acquisition Device Type Code Sequence</summary>
		public readonly static DicomTag AcquisitionDeviceTypeCodeSequence = new DicomTag(0x0022, 0x0015);

		///<summary>(0022,0016) VR=SQ VM=1 Illumination Type Code Sequence</summary>
		public readonly static DicomTag IlluminationTypeCodeSequence = new DicomTag(0x0022, 0x0016);

		///<summary>(0022,0017) VR=SQ VM=1 Light Path Filter Type Stack Code Sequence</summary>
		public readonly static DicomTag LightPathFilterTypeStackCodeSequence = new DicomTag(0x0022, 0x0017);

		///<summary>(0022,0018) VR=SQ VM=1 Image Path Filter Type Stack Code Sequence</summary>
		public readonly static DicomTag ImagePathFilterTypeStackCodeSequence = new DicomTag(0x0022, 0x0018);

		///<summary>(0022,0019) VR=SQ VM=1 Lenses Code Sequence</summary>
		public readonly static DicomTag LensesCodeSequence = new DicomTag(0x0022, 0x0019);

		///<summary>(0022,001a) VR=SQ VM=1 Channel Description Code Sequence</summary>
		public readonly static DicomTag ChannelDescriptionCodeSequence = new DicomTag(0x0022, 0x001a);

		///<summary>(0022,001b) VR=SQ VM=1 Refractive State Sequence</summary>
		public readonly static DicomTag RefractiveStateSequence = new DicomTag(0x0022, 0x001b);

		///<summary>(0022,001c) VR=SQ VM=1 Mydriatic Agent Code Sequence</summary>
		public readonly static DicomTag MydriaticAgentCodeSequence = new DicomTag(0x0022, 0x001c);

		///<summary>(0022,001d) VR=SQ VM=1 Relative Image Position Code Sequence</summary>
		public readonly static DicomTag RelativeImagePositionCodeSequence = new DicomTag(0x0022, 0x001d);

		///<summary>(0022,001e) VR=FL VM=1 Camera Angle of View</summary>
		public readonly static DicomTag CameraAngleOfView = new DicomTag(0x0022, 0x001e);

		///<summary>(0022,0020) VR=SQ VM=1 Stereo Pairs Sequence</summary>
		public readonly static DicomTag StereoPairsSequence = new DicomTag(0x0022, 0x0020);

		///<summary>(0022,0021) VR=SQ VM=1 Left Image Sequence</summary>
		public readonly static DicomTag LeftImageSequence = new DicomTag(0x0022, 0x0021);

		///<summary>(0022,0022) VR=SQ VM=1 Right Image Sequence</summary>
		public readonly static DicomTag RightImageSequence = new DicomTag(0x0022, 0x0022);

		///<summary>(0022,0030) VR=FL VM=1 Axial Length of the Eye</summary>
		public readonly static DicomTag AxialLengthOfTheEye = new DicomTag(0x0022, 0x0030);

		///<summary>(0022,0031) VR=SQ VM=1 Ophthalmic Frame Location Sequence</summary>
		public readonly static DicomTag OphthalmicFrameLocationSequence = new DicomTag(0x0022, 0x0031);

		///<summary>(0022,0032) VR=FL VM=2-2n Reference Coordinates</summary>
		public readonly static DicomTag ReferenceCoordinates = new DicomTag(0x0022, 0x0032);

		///<summary>(0022,0035) VR=FL VM=1 Depth Spatial Resolution</summary>
		public readonly static DicomTag DepthSpatialResolution = new DicomTag(0x0022, 0x0035);

		///<summary>(0022,0036) VR=FL VM=1 Maximum Depth Distortion</summary>
		public readonly static DicomTag MaximumDepthDistortion = new DicomTag(0x0022, 0x0036);

		///<summary>(0022,0037) VR=FL VM=1 Along-scan Spatial Resolution</summary>
		public readonly static DicomTag AlongScanSpatialResolution = new DicomTag(0x0022, 0x0037);

		///<summary>(0022,0038) VR=FL VM=1 Maximum Along-scan Distortion</summary>
		public readonly static DicomTag MaximumAlongScanDistortion = new DicomTag(0x0022, 0x0038);

		///<summary>(0022,0039) VR=CS VM=1 Ophthalmic Image Orientation</summary>
		public readonly static DicomTag OphthalmicImageOrientation = new DicomTag(0x0022, 0x0039);

		///<summary>(0022,0041) VR=FL VM=1 Depth of Transverse Image</summary>
		public readonly static DicomTag DepthOfTransverseImage = new DicomTag(0x0022, 0x0041);

		///<summary>(0022,0042) VR=SQ VM=1 Mydriatic Agent Concentration Units Sequence</summary>
		public readonly static DicomTag MydriaticAgentConcentrationUnitsSequence = new DicomTag(0x0022, 0x0042);

		///<summary>(0022,0048) VR=FL VM=1 Across-scan Spatial Resolution</summary>
		public readonly static DicomTag AcrossScanSpatialResolution = new DicomTag(0x0022, 0x0048);

		///<summary>(0022,0049) VR=FL VM=1 Maximum Across-scan Distortion</summary>
		public readonly static DicomTag MaximumAcrossScanDistortion = new DicomTag(0x0022, 0x0049);

		///<summary>(0022,004e) VR=DS VM=1 Mydriatic Agent Concentration</summary>
		public readonly static DicomTag MydriaticAgentConcentration = new DicomTag(0x0022, 0x004e);

		///<summary>(0022,0055) VR=FL VM=1 Illumination Wave Length</summary>
		public readonly static DicomTag IlluminationWaveLength = new DicomTag(0x0022, 0x0055);

		///<summary>(0022,0056) VR=FL VM=1 Illumination Power</summary>
		public readonly static DicomTag IlluminationPower = new DicomTag(0x0022, 0x0056);

		///<summary>(0022,0057) VR=FL VM=1 Illumination Bandwidth</summary>
		public readonly static DicomTag IlluminationBandwidth = new DicomTag(0x0022, 0x0057);

		///<summary>(0022,0058) VR=SQ VM=1 Mydriatic Agent Sequence</summary>
		public readonly static DicomTag MydriaticAgentSequence = new DicomTag(0x0022, 0x0058);

		///<summary>(0022,1007) VR=SQ VM=1 Ophthalmic Axial Measurements Right Eye Sequence</summary>
		public readonly static DicomTag OphthalmicAxialMeasurementsRightEyeSequence = new DicomTag(0x0022, 0x1007);

		///<summary>(0022,1008) VR=SQ VM=1 Ophthalmic Axial Measurements Left Eye Sequence</summary>
		public readonly static DicomTag OphthalmicAxialMeasurementsLeftEyeSequence = new DicomTag(0x0022, 0x1008);

		///<summary>(0022,1010) VR=CS VM=1 Ophthalmic Axial Length Measurements Type</summary>
		public readonly static DicomTag OphthalmicAxialLengthMeasurementsType = new DicomTag(0x0022, 0x1010);

		///<summary>(0022,1019) VR=FL VM=1 Ophthalmic Axial Length</summary>
		public readonly static DicomTag OphthalmicAxialLength = new DicomTag(0x0022, 0x1019);

		///<summary>(0022,1024) VR=SQ VM=1 Lens Status Code Sequence</summary>
		public readonly static DicomTag LensStatusCodeSequence = new DicomTag(0x0022, 0x1024);

		///<summary>(0022,1025) VR=SQ VM=1 Vitreous Status Code Sequence</summary>
		public readonly static DicomTag VitreousStatusCodeSequence = new DicomTag(0x0022, 0x1025);

		///<summary>(0022,1028) VR=SQ VM=1 IOL Formula Code Sequence</summary>
		public readonly static DicomTag IOLFormulaCodeSequence = new DicomTag(0x0022, 0x1028);

		///<summary>(0022,1029) VR=LO VM=1 IOL Formula Detail</summary>
		public readonly static DicomTag IOLFormulaDetail = new DicomTag(0x0022, 0x1029);

		///<summary>(0022,1033) VR=FL VM=1 Keratometer Index</summary>
		public readonly static DicomTag KeratometerIndex = new DicomTag(0x0022, 0x1033);

		///<summary>(0022,1035) VR=SQ VM=1 Source of Ophthalmic Axial Length Code Sequence</summary>
		public readonly static DicomTag SourceOfOphthalmicAxialLengthCodeSequence = new DicomTag(0x0022, 0x1035);

		///<summary>(0022,1037) VR=FL VM=1 Target Refraction</summary>
		public readonly static DicomTag TargetRefraction = new DicomTag(0x0022, 0x1037);

		///<summary>(0022,1039) VR=CS VM=1 Refractive Procedure Occurred</summary>
		public readonly static DicomTag RefractiveProcedureOccurred = new DicomTag(0x0022, 0x1039);

		///<summary>(0022,1040) VR=SQ VM=1 Refractive Surgery Type Code Sequence</summary>
		public readonly static DicomTag RefractiveSurgeryTypeCodeSequence = new DicomTag(0x0022, 0x1040);

		///<summary>(0022,1044) VR=SQ VM=1 Ophthalmic Ultrasound Axial Measurements Type Code Sequence</summary>
		public readonly static DicomTag OphthalmicUltrasoundAxialMeasurementsTypeCodeSequence = new DicomTag(0x0022, 0x1044);

		///<summary>(0022,1050) VR=SQ VM=1 Ophthalmic Axial Length Measurements Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthMeasurementsSequence = new DicomTag(0x0022, 0x1050);

		///<summary>(0022,1053) VR=FL VM=1 IOL Power</summary>
		public readonly static DicomTag IOLPower = new DicomTag(0x0022, 0x1053);

		///<summary>(0022,1054) VR=FL VM=1 Predicted Refractive Error</summary>
		public readonly static DicomTag PredictedRefractiveError = new DicomTag(0x0022, 0x1054);

		///<summary>(0022,1059) VR=FL VM=1 Ophthalmic Axial Length Velocity</summary>
		public readonly static DicomTag OphthalmicAxialLengthVelocity = new DicomTag(0x0022, 0x1059);

		///<summary>(0022,1065) VR=LO VM=1 Lens Status Description</summary>
		public readonly static DicomTag LensStatusDescription = new DicomTag(0x0022, 0x1065);

		///<summary>(0022,1066) VR=LO VM=1 Vitreous Status Description</summary>
		public readonly static DicomTag VitreousStatusDescription = new DicomTag(0x0022, 0x1066);

		///<summary>(0022,1090) VR=SQ VM=1 IOL Power Sequence</summary>
		public readonly static DicomTag IOLPowerSequence = new DicomTag(0x0022, 0x1090);

		///<summary>(0022,1092) VR=SQ VM=1 Lens Constant Sequence</summary>
		public readonly static DicomTag LensConstantSequence = new DicomTag(0x0022, 0x1092);

		///<summary>(0022,1093) VR=LO VM=1 IOL Manufacturer</summary>
		public readonly static DicomTag IOLManufacturer = new DicomTag(0x0022, 0x1093);

		///<summary>(0022,1094) VR=LO VM=1 Lens Constant Description</summary>
		public readonly static DicomTag LensConstantDescription = new DicomTag(0x0022, 0x1094);

		///<summary>(0022,1096) VR=SQ VM=1 Keratometry Measurement Type Code Sequence</summary>
		public readonly static DicomTag KeratometryMeasurementTypeCodeSequence = new DicomTag(0x0022, 0x1096);

		///<summary>(0022,1100) VR=SQ VM=1 Referenced Ophthalmic Axial Measurements Sequence</summary>
		public readonly static DicomTag ReferencedOphthalmicAxialMeasurementsSequence = new DicomTag(0x0022, 0x1100);

		///<summary>(0022,1101) VR=SQ VM=1 Ophthalmic Axial Length Measurements Segment Name Code Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthMeasurementsSegmentNameCodeSequence = new DicomTag(0x0022, 0x1101);

		///<summary>(0022,1103) VR=SQ VM=1 Refractive Error Before Refractive Surgery Code Sequence</summary>
		public readonly static DicomTag RefractiveErrorBeforeRefractiveSurgeryCodeSequence = new DicomTag(0x0022, 0x1103);

		///<summary>(0022,1121) VR=FL VM=1 IOL Power For Exact Emmetropia</summary>
		public readonly static DicomTag IOLPowerForExactEmmetropia = new DicomTag(0x0022, 0x1121);

		///<summary>(0022,1122) VR=FL VM=1 IOL Power For Exact Target Refraction</summary>
		public readonly static DicomTag IOLPowerForExactTargetRefraction = new DicomTag(0x0022, 0x1122);

		///<summary>(0022,1125) VR=SQ VM=1 Anterior Chamber Depth Definition Code Sequence</summary>
		public readonly static DicomTag AnteriorChamberDepthDefinitionCodeSequence = new DicomTag(0x0022, 0x1125);

		///<summary>(0022,1130) VR=FL VM=1 Lens Thickness</summary>
		public readonly static DicomTag LensThickness = new DicomTag(0x0022, 0x1130);

		///<summary>(0022,1131) VR=FL VM=1 Anterior Chamber Depth</summary>
		public readonly static DicomTag AnteriorChamberDepth = new DicomTag(0x0022, 0x1131);

		///<summary>(0022,1132) VR=SQ VM=1 Source of Lens Thickness Data Code Sequence</summary>
		public readonly static DicomTag SourceOfLensThicknessDataCodeSequence = new DicomTag(0x0022, 0x1132);

		///<summary>(0022,1133) VR=SQ VM=1 Source of Anterior Chamber Depth Data Code Sequence</summary>
		public readonly static DicomTag SourceOfAnteriorChamberDepthDataCodeSequence = new DicomTag(0x0022, 0x1133);

		///<summary>(0022,1135) VR=SQ VM=1 Source of Refractive Error Data Code Sequence</summary>
		public readonly static DicomTag SourceOfRefractiveErrorDataCodeSequence = new DicomTag(0x0022, 0x1135);

		///<summary>(0022,1140) VR=CS VM=1 Ophthalmic Axial Length Measurement Modified</summary>
		public readonly static DicomTag OphthalmicAxialLengthMeasurementModified = new DicomTag(0x0022, 0x1140);

		///<summary>(0022,1150) VR=SQ VM=1 Ophthalmic Axial Length Data Source Code Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthDataSourceCodeSequence = new DicomTag(0x0022, 0x1150);

		///<summary>(0022,1153) VR=SQ VM=1 Ophthalmic Axial Length Acquisition Method Code Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthAcquisitionMethodCodeSequence = new DicomTag(0x0022, 0x1153);

		///<summary>(0022,1155) VR=FL VM=1 Signal to Noise Ratio</summary>
		public readonly static DicomTag SignalToNoiseRatio = new DicomTag(0x0022, 0x1155);

		///<summary>(0022,1159) VR=LO VM=1 Ophthalmic Axial Length Data Source Description</summary>
		public readonly static DicomTag OphthalmicAxialLengthDataSourceDescription = new DicomTag(0x0022, 0x1159);

		///<summary>(0022,1210) VR=SQ VM=1 Ophthalmic Axial Length Measurements Total Length Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthMeasurementsTotalLengthSequence = new DicomTag(0x0022, 0x1210);

		///<summary>(0022,1211) VR=SQ VM=1 Ophthalmic Axial Length Measurements Segmental Length Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthMeasurementsSegmentalLengthSequence = new DicomTag(0x0022, 0x1211);

		///<summary>(0022,1212) VR=SQ VM=1 Ophthalmic Axial Length Measurements Length Summation Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthMeasurementsLengthSummationSequence = new DicomTag(0x0022, 0x1212);

		///<summary>(0022,1220) VR=SQ VM=1 Ultrasound Ophthalmic Axial Length Measurements Sequence</summary>
		public readonly static DicomTag UltrasoundOphthalmicAxialLengthMeasurementsSequence = new DicomTag(0x0022, 0x1220);

		///<summary>(0022,1225) VR=SQ VM=1 Optical Ophthalmic Axial Length Measurements Sequence</summary>
		public readonly static DicomTag OpticalOphthalmicAxialLengthMeasurementsSequence = new DicomTag(0x0022, 0x1225);

		///<summary>(0022,1230) VR=SQ VM=1 Ultrasound Selected Ophthalmic Axial Length Sequence</summary>
		public readonly static DicomTag UltrasoundSelectedOphthalmicAxialLengthSequence = new DicomTag(0x0022, 0x1230);

		///<summary>(0022,1250) VR=SQ VM=1 Ophthalmic Axial Length Selection Method Code Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthSelectionMethodCodeSequence = new DicomTag(0x0022, 0x1250);

		///<summary>(0022,1255) VR=SQ VM=1 Optical Selected Ophthalmic Axial Length Sequence</summary>
		public readonly static DicomTag OpticalSelectedOphthalmicAxialLengthSequence = new DicomTag(0x0022, 0x1255);

		///<summary>(0022,1257) VR=SQ VM=1 Selected Segmental Ophthalmic Axial Length Sequence</summary>
		public readonly static DicomTag SelectedSegmentalOphthalmicAxialLengthSequence = new DicomTag(0x0022, 0x1257);

		///<summary>(0022,1260) VR=SQ VM=1 Selected Total Ophthalmic Axial Length Sequence</summary>
		public readonly static DicomTag SelectedTotalOphthalmicAxialLengthSequence = new DicomTag(0x0022, 0x1260);

		///<summary>(0022,1262) VR=SQ VM=1 Ophthalmic Axial Length Quality Metric Sequence</summary>
		public readonly static DicomTag OphthalmicAxialLengthQualityMetricSequence = new DicomTag(0x0022, 0x1262);

		///<summary>(0022,1273) VR=LO VM=1 Ophthalmic Axial  Length Quality Metric Type Description</summary>
		public readonly static DicomTag OphthalmicAxialLengthQualityMetricTypeDescription = new DicomTag(0x0022, 0x1273);

		///<summary>(0022,1300) VR=SQ VM=1 Intraocular Lens Calculations Right Eye Sequence</summary>
		public readonly static DicomTag IntraocularLensCalculationsRightEyeSequence = new DicomTag(0x0022, 0x1300);

		///<summary>(0022,1310) VR=SQ VM=1 Intraocular Lens Calculations Left Eye Sequence</summary>
		public readonly static DicomTag IntraocularLensCalculationsLeftEyeSequence = new DicomTag(0x0022, 0x1310);

		///<summary>(0022,1330) VR=SQ VM=1 Referenced Ophthalmic Axial Length Measurement QC Image Sequence</summary>
		public readonly static DicomTag ReferencedOphthalmicAxialLengthMeasurementQCImageSequence = new DicomTag(0x0022, 0x1330);

		///<summary>(0024,0010) VR=FL VM=1 Visual Field Horizontal Extent</summary>
		public readonly static DicomTag VisualFieldHorizontalExtent = new DicomTag(0x0024, 0x0010);

		///<summary>(0024,0011) VR=FL VM=1 Visual Field Vertical Extent</summary>
		public readonly static DicomTag VisualFieldVerticalExtent = new DicomTag(0x0024, 0x0011);

		///<summary>(0024,0012) VR=CS VM=1 Visual Field Shape</summary>
		public readonly static DicomTag VisualFieldShape = new DicomTag(0x0024, 0x0012);

		///<summary>(0024,0016) VR=SQ VM=1 Screening Test Mode Code Sequence</summary>
		public readonly static DicomTag ScreeningTestModeCodeSequence = new DicomTag(0x0024, 0x0016);

		///<summary>(0024,0018) VR=FL VM=1 Maximum Stimulus Luminance</summary>
		public readonly static DicomTag MaximumStimulusLuminance = new DicomTag(0x0024, 0x0018);

		///<summary>(0024,0020) VR=FL VM=1 Background Luminance</summary>
		public readonly static DicomTag BackgroundLuminance = new DicomTag(0x0024, 0x0020);

		///<summary>(0024,0021) VR=SQ VM=1 Stimulus Color Code Sequence</summary>
		public readonly static DicomTag StimulusColorCodeSequence = new DicomTag(0x0024, 0x0021);

		///<summary>(0024,0024) VR=SQ VM=1 Background Illumination Color Code Sequence</summary>
		public readonly static DicomTag BackgroundIlluminationColorCodeSequence = new DicomTag(0x0024, 0x0024);

		///<summary>(0024,0025) VR=FL VM=1 Stimulus Area</summary>
		public readonly static DicomTag StimulusArea = new DicomTag(0x0024, 0x0025);

		///<summary>(0024,0028) VR=FL VM=1 Stimulus Presentation Time</summary>
		public readonly static DicomTag StimulusPresentationTime = new DicomTag(0x0024, 0x0028);

		///<summary>(0024,0032) VR=SQ VM=1 Fixation Sequence</summary>
		public readonly static DicomTag FixationSequence = new DicomTag(0x0024, 0x0032);

		///<summary>(0024,0033) VR=SQ VM=1 Fixation Monitoring Code Sequence</summary>
		public readonly static DicomTag FixationMonitoringCodeSequence = new DicomTag(0x0024, 0x0033);

		///<summary>(0024,0034) VR=SQ VM=1 Visual Field Catch Trial Sequence</summary>
		public readonly static DicomTag VisualFieldCatchTrialSequence = new DicomTag(0x0024, 0x0034);

		///<summary>(0024,0035) VR=US VM=1 Fixation Checked Quantity</summary>
		public readonly static DicomTag FixationCheckedQuantity = new DicomTag(0x0024, 0x0035);

		///<summary>(0024,0036) VR=US VM=1 Patient Not Properly Fixated Quantity</summary>
		public readonly static DicomTag PatientNotProperlyFixatedQuantity = new DicomTag(0x0024, 0x0036);

		///<summary>(0024,0037) VR=CS VM=1 Presented Visual Stimuli Data Flag</summary>
		public readonly static DicomTag PresentedVisualStimuliDataFlag = new DicomTag(0x0024, 0x0037);

		///<summary>(0024,0038) VR=US VM=1 Number of Visual Stimuli</summary>
		public readonly static DicomTag NumberOfVisualStimuli = new DicomTag(0x0024, 0x0038);

		///<summary>(0024,0039) VR=CS VM=1 Excessive Fixation Losses Data Flag</summary>
		public readonly static DicomTag ExcessiveFixationLossesDataFlag = new DicomTag(0x0024, 0x0039);

		///<summary>(0024,0040) VR=CS VM=1 Excessive Fixation Losses</summary>
		public readonly static DicomTag ExcessiveFixationLosses = new DicomTag(0x0024, 0x0040);

		///<summary>(0024,0042) VR=US VM=1 Stimuli Retesting Quantity</summary>
		public readonly static DicomTag StimuliRetestingQuantity = new DicomTag(0x0024, 0x0042);

		///<summary>(0024,0044) VR=LT VM=1 Comments on Patient’s Performance of Visual Field</summary>
		public readonly static DicomTag CommentsOnPatientPerformanceOfVisualField = new DicomTag(0x0024, 0x0044);

		///<summary>(0024,0045) VR=CS VM=1 False Negatives Estimate Flag</summary>
		public readonly static DicomTag FalseNegativesEstimateFlag = new DicomTag(0x0024, 0x0045);

		///<summary>(0024,0046) VR=FL VM=1 False Negatives Estimate</summary>
		public readonly static DicomTag FalseNegativesEstimate = new DicomTag(0x0024, 0x0046);

		///<summary>(0024,0048) VR=US VM=1 Negative Catch Trials Quantity</summary>
		public readonly static DicomTag NegativeCatchTrialsQuantity = new DicomTag(0x0024, 0x0048);

		///<summary>(0024,0050) VR=US VM=1 False Negatives Quantity</summary>
		public readonly static DicomTag FalseNegativesQuantity = new DicomTag(0x0024, 0x0050);

		///<summary>(0024,0051) VR=CS VM=1 Excessive False Negatives Data Flag</summary>
		public readonly static DicomTag ExcessiveFalseNegativesDataFlag = new DicomTag(0x0024, 0x0051);

		///<summary>(0024,0052) VR=CS VM=1 Excessive False Negatives</summary>
		public readonly static DicomTag ExcessiveFalseNegatives = new DicomTag(0x0024, 0x0052);

		///<summary>(0024,0053) VR=CS VM=1 False Positives Estimate Flag</summary>
		public readonly static DicomTag FalsePositivesEstimateFlag = new DicomTag(0x0024, 0x0053);

		///<summary>(0024,0054) VR=FL VM=1 False Positives Estimate</summary>
		public readonly static DicomTag FalsePositivesEstimate = new DicomTag(0x0024, 0x0054);

		///<summary>(0024,0055) VR=CS VM=1 Catch Trials Data Flag</summary>
		public readonly static DicomTag CatchTrialsDataFlag = new DicomTag(0x0024, 0x0055);

		///<summary>(0024,0056) VR=US VM=1 Positive Catch Trials Quantity</summary>
		public readonly static DicomTag PositiveCatchTrialsQuantity = new DicomTag(0x0024, 0x0056);

		///<summary>(0024,0057) VR=CS VM=1 Test Point Normals Data Flag</summary>
		public readonly static DicomTag TestPointNormalsDataFlag = new DicomTag(0x0024, 0x0057);

		///<summary>(0024,0058) VR=SQ VM=1 Test Point Normals Sequence</summary>
		public readonly static DicomTag TestPointNormalsSequence = new DicomTag(0x0024, 0x0058);

		///<summary>(0024,0059) VR=CS VM=1 Global Deviation Probability Normals Flag</summary>
		public readonly static DicomTag GlobalDeviationProbabilityNormalsFlag = new DicomTag(0x0024, 0x0059);

		///<summary>(0024,0060) VR=US VM=1 False Positives Quantity</summary>
		public readonly static DicomTag FalsePositivesQuantity = new DicomTag(0x0024, 0x0060);

		///<summary>(0024,0061) VR=CS VM=1 Excessive False Positives Data Flag</summary>
		public readonly static DicomTag ExcessiveFalsePositivesDataFlag = new DicomTag(0x0024, 0x0061);

		///<summary>(0024,0062) VR=CS VM=1 Excessive False Positives</summary>
		public readonly static DicomTag ExcessiveFalsePositives = new DicomTag(0x0024, 0x0062);

		///<summary>(0024,0063) VR=CS VM=1 Visual Field Test Normals Flag</summary>
		public readonly static DicomTag VisualFieldTestNormalsFlag = new DicomTag(0x0024, 0x0063);

		///<summary>(0024,0064) VR=SQ VM=1 Results Normals Sequence</summary>
		public readonly static DicomTag ResultsNormalsSequence = new DicomTag(0x0024, 0x0064);

		///<summary>(0024,0065) VR=SQ VM=1 Age Corrected Sensitivity Deviation Algorithm Sequence</summary>
		public readonly static DicomTag AgeCorrectedSensitivityDeviationAlgorithmSequence = new DicomTag(0x0024, 0x0065);

		///<summary>(0024,0066) VR=FL VM=1 Global Deviation From Normal</summary>
		public readonly static DicomTag GlobalDeviationFromNormal = new DicomTag(0x0024, 0x0066);

		///<summary>(0024,0067) VR=SQ VM=1 Generalized Defect Sensitivity Deviation Algorithm Sequence</summary>
		public readonly static DicomTag GeneralizedDefectSensitivityDeviationAlgorithmSequence = new DicomTag(0x0024, 0x0067);

		///<summary>(0024,0068) VR=FL VM=1 Localized Deviation from Normal</summary>
		public readonly static DicomTag LocalizedDeviationfromNormal = new DicomTag(0x0024, 0x0068);

		///<summary>(0024,0069) VR=LO VM=1 Patient Reliability Indicator</summary>
		public readonly static DicomTag PatientReliabilityIndicator = new DicomTag(0x0024, 0x0069);

		///<summary>(0024,0070) VR=FL VM=1 Visual Field Mean Sensitivity</summary>
		public readonly static DicomTag VisualFieldMeanSensitivity = new DicomTag(0x0024, 0x0070);

		///<summary>(0024,0071) VR=FL VM=1 Global Deviation Probability</summary>
		public readonly static DicomTag GlobalDeviationProbability = new DicomTag(0x0024, 0x0071);

		///<summary>(0024,0072) VR=CS VM=1 Local Deviation Probability Normals Flag</summary>
		public readonly static DicomTag LocalDeviationProbabilityNormalsFlag = new DicomTag(0x0024, 0x0072);

		///<summary>(0024,0073) VR=FL VM=1 Localized Deviation Probability</summary>
		public readonly static DicomTag LocalizedDeviationProbability = new DicomTag(0x0024, 0x0073);

		///<summary>(0024,0074) VR=CS VM=1 Short Term Fluctuation Calculated</summary>
		public readonly static DicomTag ShortTermFluctuationCalculated = new DicomTag(0x0024, 0x0074);

		///<summary>(0024,0075) VR=FL VM=1 Short Term Fluctuation</summary>
		public readonly static DicomTag ShortTermFluctuation = new DicomTag(0x0024, 0x0075);

		///<summary>(0024,0076) VR=CS VM=1 Short Term Fluctuation Probability Calculated</summary>
		public readonly static DicomTag ShortTermFluctuationProbabilityCalculated = new DicomTag(0x0024, 0x0076);

		///<summary>(0024,0077) VR=FL VM=1 Short Term Fluctuation Probability</summary>
		public readonly static DicomTag ShortTermFluctuationProbability = new DicomTag(0x0024, 0x0077);

		///<summary>(0024,0078) VR=CS VM=1 Corrected Localized Deviation From Normal Calculated</summary>
		public readonly static DicomTag CorrectedLocalizedDeviationFromNormalCalculated = new DicomTag(0x0024, 0x0078);

		///<summary>(0024,0079) VR=FL VM=1 Corrected Localized Deviation From Normal</summary>
		public readonly static DicomTag CorrectedLocalizedDeviationFromNormal = new DicomTag(0x0024, 0x0079);

		///<summary>(0024,0080) VR=CS VM=1 Corrected Localized Deviation From Normal Probability Calculated</summary>
		public readonly static DicomTag CorrectedLocalizedDeviationFromNormalProbabilityCalculated = new DicomTag(0x0024, 0x0080);

		///<summary>(0024,0081) VR=FL VM=1 Corrected Localized Deviation From Normal Probability</summary>
		public readonly static DicomTag CorrectedLocalizedDeviationFromNormalProbability = new DicomTag(0x0024, 0x0081);

		///<summary>(0024,0083) VR=SQ VM=1 Global Deviation Probability Sequence</summary>
		public readonly static DicomTag GlobalDeviationProbabilitySequence = new DicomTag(0x0024, 0x0083);

		///<summary>(0024,0085) VR=SQ VM=1 Localized Deviation Probability Sequence</summary>
		public readonly static DicomTag LocalizedDeviationProbabilitySequence = new DicomTag(0x0024, 0x0085);

		///<summary>(0024,0086) VR=CS VM=1 Foveal Sensitivity Measured</summary>
		public readonly static DicomTag FovealSensitivityMeasured = new DicomTag(0x0024, 0x0086);

		///<summary>(0024,0087) VR=FL VM=1 Foveal Sensitivity</summary>
		public readonly static DicomTag FovealSensitivity = new DicomTag(0x0024, 0x0087);

		///<summary>(0024,0088) VR=FL VM=1 Visual Field Test Duration</summary>
		public readonly static DicomTag VisualFieldTestDuration = new DicomTag(0x0024, 0x0088);

		///<summary>(0024,0089) VR=SQ VM=1 Visual Field Test Point Sequence</summary>
		public readonly static DicomTag VisualFieldTestPointSequence = new DicomTag(0x0024, 0x0089);

		///<summary>(0024,0090) VR=FL VM=1 Visual Field Test Point X-Coordinate</summary>
		public readonly static DicomTag VisualFieldTestPointXCoordinate = new DicomTag(0x0024, 0x0090);

		///<summary>(0024,0091) VR=FL VM=1 Visual Field Test Point Y-Coordinate</summary>
		public readonly static DicomTag VisualFieldTestPointYCoordinate = new DicomTag(0x0024, 0x0091);

		///<summary>(0024,0092) VR=FL VM=1 Age Corrected Sensitivity Deviation Value</summary>
		public readonly static DicomTag AgeCorrectedSensitivityDeviationValue = new DicomTag(0x0024, 0x0092);

		///<summary>(0024,0093) VR=CS VM=1 Stimulus Results</summary>
		public readonly static DicomTag StimulusResults = new DicomTag(0x0024, 0x0093);

		///<summary>(0024,0094) VR=FL VM=1 Sensitivity Value</summary>
		public readonly static DicomTag SensitivityValue = new DicomTag(0x0024, 0x0094);

		///<summary>(0024,0095) VR=CS VM=1 Retest Stimulus Seen</summary>
		public readonly static DicomTag RetestStimulusSeen = new DicomTag(0x0024, 0x0095);

		///<summary>(0024,0096) VR=FL VM=1 Retest Sensitivity Value</summary>
		public readonly static DicomTag RetestSensitivityValue = new DicomTag(0x0024, 0x0096);

		///<summary>(0024,0097) VR=SQ VM=1 Visual Field Test Point Normals Sequence</summary>
		public readonly static DicomTag VisualFieldTestPointNormalsSequence = new DicomTag(0x0024, 0x0097);

		///<summary>(0024,0098) VR=FL VM=1 Quantified Defect</summary>
		public readonly static DicomTag QuantifiedDefect = new DicomTag(0x0024, 0x0098);

		///<summary>(0024,0100) VR=FL VM=1 Age Corrected Sensitivity Deviation Probability Value</summary>
		public readonly static DicomTag AgeCorrectedSensitivityDeviationProbabilityValue = new DicomTag(0x0024, 0x0100);

		///<summary>(0024,0102) VR=CS VM=1 Generalized Defect Corrected Sensitivity Deviation Flag</summary>
		public readonly static DicomTag GeneralizedDefectCorrectedSensitivityDeviationFlag  = new DicomTag(0x0024, 0x0102);

		///<summary>(0024,0103) VR=FL VM=1 Generalized Defect Corrected Sensitivity Deviation Value</summary>
		public readonly static DicomTag GeneralizedDefectCorrectedSensitivityDeviationValue  = new DicomTag(0x0024, 0x0103);

		///<summary>(0024,0104) VR=FL VM=1 Generalized Defect Corrected Sensitivity Deviation Probability Value</summary>
		public readonly static DicomTag GeneralizedDefectCorrectedSensitivityDeviationProbabilityValue = new DicomTag(0x0024, 0x0104);

		///<summary>(0024,0105) VR=FL VM=1 Minimum Sensitivity Value</summary>
		public readonly static DicomTag MinimumSensitivityValue = new DicomTag(0x0024, 0x0105);

		///<summary>(0024,0106) VR=CS VM=1 Blind Spot Localized</summary>
		public readonly static DicomTag BlindSpotLocalized = new DicomTag(0x0024, 0x0106);

		///<summary>(0024,0107) VR=FL VM=1 Blind Spot X-Coordinate</summary>
		public readonly static DicomTag BlindSpotXCoordinate = new DicomTag(0x0024, 0x0107);

		///<summary>(0024,0108) VR=FL VM=1 Blind Spot Y-Coordinate</summary>
		public readonly static DicomTag BlindSpotYCoordinate  = new DicomTag(0x0024, 0x0108);

		///<summary>(0024,0110) VR=SQ VM=1 Visual Acuity Measurement Sequence</summary>
		public readonly static DicomTag VisualAcuityMeasurementSequence = new DicomTag(0x0024, 0x0110);

		///<summary>(0024,0112) VR=SQ VM=1 Refractive Parameters Used on Patient Sequence</summary>
		public readonly static DicomTag RefractiveParametersUsedOnPatientSequence = new DicomTag(0x0024, 0x0112);

		///<summary>(0024,0113) VR=CS VM=1 Measurement Laterality</summary>
		public readonly static DicomTag MeasurementLaterality = new DicomTag(0x0024, 0x0113);

		///<summary>(0024,0114) VR=SQ VM=1 Ophthalmic Patient Clinical Information Left Eye Sequence</summary>
		public readonly static DicomTag OphthalmicPatientClinicalInformationLeftEyeSequence = new DicomTag(0x0024, 0x0114);

		///<summary>(0024,0115) VR=SQ VM=1 Ophthalmic Patient Clinical Information Right Eye Sequence</summary>
		public readonly static DicomTag OphthalmicPatientClinicalInformationRightEyeSequence = new DicomTag(0x0024, 0x0115);

		///<summary>(0024,0117) VR=CS VM=1 Foveal Point Normative Data Flag</summary>
		public readonly static DicomTag FovealPointNormativeDataFlag = new DicomTag(0x0024, 0x0117);

		///<summary>(0024,0118) VR=FL VM=1 Foveal Point Probability Value</summary>
		public readonly static DicomTag FovealPointProbabilityValue = new DicomTag(0x0024, 0x0118);

		///<summary>(0024,0120) VR=CS VM=1 Screening Baseline Measured</summary>
		public readonly static DicomTag ScreeningBaselineMeasured = new DicomTag(0x0024, 0x0120);

		///<summary>(0024,0122) VR=SQ VM=1 Screening Baseline Measured Sequence</summary>
		public readonly static DicomTag ScreeningBaselineMeasuredSequence = new DicomTag(0x0024, 0x0122);

		///<summary>(0024,0124) VR=CS VM=1 Screening Baseline Type</summary>
		public readonly static DicomTag ScreeningBaselineType = new DicomTag(0x0024, 0x0124);

		///<summary>(0024,0126) VR=FL VM=1 Screening Baseline Value</summary>
		public readonly static DicomTag ScreeningBaselineValue = new DicomTag(0x0024, 0x0126);

		///<summary>(0024,0202) VR=LO VM=1 Algorithm Source</summary>
		public readonly static DicomTag AlgorithmSource = new DicomTag(0x0024, 0x0202);

		///<summary>(0024,0306) VR=LO VM=1 Data Set Name</summary>
		public readonly static DicomTag DataSetName = new DicomTag(0x0024, 0x0306);

		///<summary>(0024,0307) VR=LO VM=1 Data Set Version</summary>
		public readonly static DicomTag DataSetVersion = new DicomTag(0x0024, 0x0307);

		///<summary>(0024,0308) VR=LO VM=1 Data Set Source</summary>
		public readonly static DicomTag DataSetSource = new DicomTag(0x0024, 0x0308);

		///<summary>(0024,0309) VR=LO VM=1 Data Set Description</summary>
		public readonly static DicomTag DataSetDescription = new DicomTag(0x0024, 0x0309);

		///<summary>(0024,0317) VR=SQ VM=1 Visual Field Test Reliability Global Index Sequence</summary>
		public readonly static DicomTag VisualFieldTestReliabilityGlobalIndexSequence = new DicomTag(0x0024, 0x0317);

		///<summary>(0024,0320) VR=SQ VM=1 Visual Field Global Results Index Sequence</summary>
		public readonly static DicomTag VisualFieldGlobalResultsIndexSequence = new DicomTag(0x0024, 0x0320);

		///<summary>(0024,0325) VR=SQ VM=1 Data Observation Sequence</summary>
		public readonly static DicomTag DataObservationSequence = new DicomTag(0x0024, 0x0325);

		///<summary>(0024,0338) VR=CS VM=1 Index Normals Flag</summary>
		public readonly static DicomTag IndexNormalsFlag = new DicomTag(0x0024, 0x0338);

		///<summary>(0024,0341) VR=FL VM=1 Index Probability</summary>
		public readonly static DicomTag IndexProbability = new DicomTag(0x0024, 0x0341);

		///<summary>(0024,0344) VR=SQ VM=1 Index Probability Sequence</summary>
		public readonly static DicomTag IndexProbabilitySequence = new DicomTag(0x0024, 0x0344);

		///<summary>(0028,0002) VR=US VM=1 Samples per Pixel</summary>
		public readonly static DicomTag SamplesPerPixel = new DicomTag(0x0028, 0x0002);

		///<summary>(0028,0003) VR=US VM=1 Samples per Pixel Used</summary>
		public readonly static DicomTag SamplesPerPixelUsed = new DicomTag(0x0028, 0x0003);

		///<summary>(0028,0004) VR=CS VM=1 Photometric Interpretation</summary>
		public readonly static DicomTag PhotometricInterpretation = new DicomTag(0x0028, 0x0004);

		///<summary>(0028,0005) VR=US VM=1 Image Dimensions (RETIRED)</summary>
		public readonly static DicomTag ImageDimensionsRETIRED = new DicomTag(0x0028, 0x0005);

		///<summary>(0028,0006) VR=US VM=1 Planar Configuration</summary>
		public readonly static DicomTag PlanarConfiguration = new DicomTag(0x0028, 0x0006);

		///<summary>(0028,0008) VR=IS VM=1 Number of Frames</summary>
		public readonly static DicomTag NumberOfFrames = new DicomTag(0x0028, 0x0008);

		///<summary>(0028,0009) VR=AT VM=1-n Frame Increment Pointer</summary>
		public readonly static DicomTag FrameIncrementPointer = new DicomTag(0x0028, 0x0009);

		///<summary>(0028,000a) VR=AT VM=1-n Frame Dimension Pointer</summary>
		public readonly static DicomTag FrameDimensionPointer = new DicomTag(0x0028, 0x000a);

		///<summary>(0028,0010) VR=US VM=1 Rows</summary>
		public readonly static DicomTag Rows = new DicomTag(0x0028, 0x0010);

		///<summary>(0028,0011) VR=US VM=1 Columns</summary>
		public readonly static DicomTag Columns = new DicomTag(0x0028, 0x0011);

		///<summary>(0028,0012) VR=US VM=1 Planes (RETIRED)</summary>
		public readonly static DicomTag PlanesRETIRED = new DicomTag(0x0028, 0x0012);

		///<summary>(0028,0014) VR=US VM=1 Ultrasound Color Data Present</summary>
		public readonly static DicomTag UltrasoundColorDataPresent = new DicomTag(0x0028, 0x0014);

		///<summary>(0028,0030) VR=DS VM=2 Pixel Spacing</summary>
		public readonly static DicomTag PixelSpacing = new DicomTag(0x0028, 0x0030);

		///<summary>(0028,0031) VR=DS VM=2 Zoom Factor</summary>
		public readonly static DicomTag ZoomFactor = new DicomTag(0x0028, 0x0031);

		///<summary>(0028,0032) VR=DS VM=2 Zoom Center</summary>
		public readonly static DicomTag ZoomCenter = new DicomTag(0x0028, 0x0032);

		///<summary>(0028,0034) VR=IS VM=2 Pixel Aspect Ratio</summary>
		public readonly static DicomTag PixelAspectRatio = new DicomTag(0x0028, 0x0034);

		///<summary>(0028,0040) VR=CS VM=1 Image Format (RETIRED)</summary>
		public readonly static DicomTag ImageFormatRETIRED = new DicomTag(0x0028, 0x0040);

		///<summary>(0028,0050) VR=LO VM=1-n Manipulated Image (RETIRED)</summary>
		public readonly static DicomTag ManipulatedImageRETIRED = new DicomTag(0x0028, 0x0050);

		///<summary>(0028,0051) VR=CS VM=1-n Corrected Image</summary>
		public readonly static DicomTag CorrectedImage = new DicomTag(0x0028, 0x0051);

		///<summary>(0028,005f) VR=LO VM=1 Compression Recognition Code (RETIRED)</summary>
		public readonly static DicomTag CompressionRecognitionCodeRETIRED = new DicomTag(0x0028, 0x005f);

		///<summary>(0028,0060) VR=CS VM=1 Compression Code (RETIRED)</summary>
		public readonly static DicomTag CompressionCodeRETIRED = new DicomTag(0x0028, 0x0060);

		///<summary>(0028,0061) VR=SH VM=1 Compression Originator (RETIRED)</summary>
		public readonly static DicomTag CompressionOriginatorRETIRED = new DicomTag(0x0028, 0x0061);

		///<summary>(0028,0062) VR=LO VM=1 Compression Label (RETIRED)</summary>
		public readonly static DicomTag CompressionLabelRETIRED = new DicomTag(0x0028, 0x0062);

		///<summary>(0028,0063) VR=SH VM=1 Compression Description (RETIRED)</summary>
		public readonly static DicomTag CompressionDescriptionRETIRED = new DicomTag(0x0028, 0x0063);

		///<summary>(0028,0065) VR=CS VM=1-n Compression Sequence (RETIRED)</summary>
		public readonly static DicomTag CompressionSequenceRETIRED = new DicomTag(0x0028, 0x0065);

		///<summary>(0028,0066) VR=AT VM=1-n Compression Step Pointers (RETIRED)</summary>
		public readonly static DicomTag CompressionStepPointersRETIRED = new DicomTag(0x0028, 0x0066);

		///<summary>(0028,0068) VR=US VM=1 Repeat Interval (RETIRED)</summary>
		public readonly static DicomTag RepeatIntervalRETIRED = new DicomTag(0x0028, 0x0068);

		///<summary>(0028,0069) VR=US VM=1 Bits Grouped (RETIRED)</summary>
		public readonly static DicomTag BitsGroupedRETIRED = new DicomTag(0x0028, 0x0069);

		///<summary>(0028,0070) VR=US VM=1-n Perimeter Table (RETIRED)</summary>
		public readonly static DicomTag PerimeterTableRETIRED = new DicomTag(0x0028, 0x0070);

		///<summary>(0028,0071) VR=US/SS VM=1 Perimeter Value (RETIRED)</summary>
		public readonly static DicomTag PerimeterValueRETIRED = new DicomTag(0x0028, 0x0071);

		///<summary>(0028,0080) VR=US VM=1 Predictor Rows (RETIRED)</summary>
		public readonly static DicomTag PredictorRowsRETIRED = new DicomTag(0x0028, 0x0080);

		///<summary>(0028,0081) VR=US VM=1 Predictor Columns (RETIRED)</summary>
		public readonly static DicomTag PredictorColumnsRETIRED = new DicomTag(0x0028, 0x0081);

		///<summary>(0028,0082) VR=US VM=1-n Predictor Constants (RETIRED)</summary>
		public readonly static DicomTag PredictorConstantsRETIRED = new DicomTag(0x0028, 0x0082);

		///<summary>(0028,0090) VR=CS VM=1 Blocked Pixels (RETIRED)</summary>
		public readonly static DicomTag BlockedPixelsRETIRED = new DicomTag(0x0028, 0x0090);

		///<summary>(0028,0091) VR=US VM=1 Block Rows (RETIRED)</summary>
		public readonly static DicomTag BlockRowsRETIRED = new DicomTag(0x0028, 0x0091);

		///<summary>(0028,0092) VR=US VM=1 Block Columns (RETIRED)</summary>
		public readonly static DicomTag BlockColumnsRETIRED = new DicomTag(0x0028, 0x0092);

		///<summary>(0028,0093) VR=US VM=1 Row Overlap (RETIRED)</summary>
		public readonly static DicomTag RowOverlapRETIRED = new DicomTag(0x0028, 0x0093);

		///<summary>(0028,0094) VR=US VM=1 Column Overlap (RETIRED)</summary>
		public readonly static DicomTag ColumnOverlapRETIRED = new DicomTag(0x0028, 0x0094);

		///<summary>(0028,0100) VR=US VM=1 Bits Allocated</summary>
		public readonly static DicomTag BitsAllocated = new DicomTag(0x0028, 0x0100);

		///<summary>(0028,0101) VR=US VM=1 Bits Stored</summary>
		public readonly static DicomTag BitsStored = new DicomTag(0x0028, 0x0101);

		///<summary>(0028,0102) VR=US VM=1 High Bit</summary>
		public readonly static DicomTag HighBit = new DicomTag(0x0028, 0x0102);

		///<summary>(0028,0103) VR=US VM=1 Pixel Representation</summary>
		public readonly static DicomTag PixelRepresentation = new DicomTag(0x0028, 0x0103);

		///<summary>(0028,0104) VR=US/SS VM=1 Smallest Valid Pixel Value (RETIRED)</summary>
		public readonly static DicomTag SmallestValidPixelValueRETIRED = new DicomTag(0x0028, 0x0104);

		///<summary>(0028,0105) VR=US/SS VM=1 Largest Valid Pixel Value (RETIRED)</summary>
		public readonly static DicomTag LargestValidPixelValueRETIRED = new DicomTag(0x0028, 0x0105);

		///<summary>(0028,0106) VR=US/SS VM=1 Smallest Image Pixel Value</summary>
		public readonly static DicomTag SmallestImagePixelValue = new DicomTag(0x0028, 0x0106);

		///<summary>(0028,0107) VR=US/SS VM=1 Largest Image Pixel Value</summary>
		public readonly static DicomTag LargestImagePixelValue = new DicomTag(0x0028, 0x0107);

		///<summary>(0028,0108) VR=US/SS VM=1 Smallest Pixel Value in Series</summary>
		public readonly static DicomTag SmallestPixelValueInSeries = new DicomTag(0x0028, 0x0108);

		///<summary>(0028,0109) VR=US/SS VM=1 Largest Pixel Value in Series</summary>
		public readonly static DicomTag LargestPixelValueInSeries = new DicomTag(0x0028, 0x0109);

		///<summary>(0028,0110) VR=US/SS VM=1 Smallest Image Pixel Value in Plane (RETIRED)</summary>
		public readonly static DicomTag SmallestImagePixelValueInPlaneRETIRED = new DicomTag(0x0028, 0x0110);

		///<summary>(0028,0111) VR=US/SS VM=1 Largest Image Pixel Value in Plane (RETIRED)</summary>
		public readonly static DicomTag LargestImagePixelValueInPlaneRETIRED = new DicomTag(0x0028, 0x0111);

		///<summary>(0028,0120) VR=US/SS VM=1 Pixel Padding Value</summary>
		public readonly static DicomTag PixelPaddingValue = new DicomTag(0x0028, 0x0120);

		///<summary>(0028,0121) VR=US/SS VM=1 Pixel Padding Range Limit</summary>
		public readonly static DicomTag PixelPaddingRangeLimit = new DicomTag(0x0028, 0x0121);

		///<summary>(0028,0200) VR=US VM=1 Image Location (RETIRED)</summary>
		public readonly static DicomTag ImageLocationRETIRED = new DicomTag(0x0028, 0x0200);

		///<summary>(0028,0300) VR=CS VM=1 Quality Control Image</summary>
		public readonly static DicomTag QualityControlImage = new DicomTag(0x0028, 0x0300);

		///<summary>(0028,0301) VR=CS VM=1 Burned In Annotation</summary>
		public readonly static DicomTag BurnedInAnnotation = new DicomTag(0x0028, 0x0301);

		///<summary>(0028,0302) VR=CS VM=1 Recognizable Visual Features</summary>
		public readonly static DicomTag RecognizableVisualFeatures = new DicomTag(0x0028, 0x0302);

		///<summary>(0028,0303) VR=CS VM=1 Longitudinal Temporal Information Modified</summary>
		public readonly static DicomTag LongitudinalTemporalInformationModified = new DicomTag(0x0028, 0x0303);

		///<summary>(0028,0400) VR=LO VM=1 Transform Label (RETIRED)</summary>
		public readonly static DicomTag TransformLabelRETIRED = new DicomTag(0x0028, 0x0400);

		///<summary>(0028,0401) VR=LO VM=1 Transform Version Number (RETIRED)</summary>
		public readonly static DicomTag TransformVersionNumberRETIRED = new DicomTag(0x0028, 0x0401);

		///<summary>(0028,0402) VR=US VM=1 Number of Transform Steps (RETIRED)</summary>
		public readonly static DicomTag NumberOfTransformStepsRETIRED = new DicomTag(0x0028, 0x0402);

		///<summary>(0028,0403) VR=LO VM=1-n Sequence of Compressed Data (RETIRED)</summary>
		public readonly static DicomTag SequenceOfCompressedDataRETIRED = new DicomTag(0x0028, 0x0403);

		///<summary>(0028,0404) VR=AT VM=1-n Details of Coefficients (RETIRED)</summary>
		public readonly static DicomTag DetailsOfCoefficientsRETIRED = new DicomTag(0x0028, 0x0404);

		///<summary>(0028,0700) VR=LO VM=1 DCT Label (RETIRED)</summary>
		public readonly static DicomTag DCTLabelRETIRED = new DicomTag(0x0028, 0x0700);

		///<summary>(0028,0701) VR=CS VM=1-n Data Block Description (RETIRED)</summary>
		public readonly static DicomTag DataBlockDescriptionRETIRED = new DicomTag(0x0028, 0x0701);

		///<summary>(0028,0702) VR=AT VM=1-n Data Block (RETIRED)</summary>
		public readonly static DicomTag DataBlockRETIRED = new DicomTag(0x0028, 0x0702);

		///<summary>(0028,0710) VR=US VM=1 Normalization Factor Format (RETIRED)</summary>
		public readonly static DicomTag NormalizationFactorFormatRETIRED = new DicomTag(0x0028, 0x0710);

		///<summary>(0028,0720) VR=US VM=1 Zonal Map Number Format (RETIRED)</summary>
		public readonly static DicomTag ZonalMapNumberFormatRETIRED = new DicomTag(0x0028, 0x0720);

		///<summary>(0028,0721) VR=AT VM=1-n Zonal Map Location (RETIRED)</summary>
		public readonly static DicomTag ZonalMapLocationRETIRED = new DicomTag(0x0028, 0x0721);

		///<summary>(0028,0722) VR=US VM=1 Zonal Map Format (RETIRED)</summary>
		public readonly static DicomTag ZonalMapFormatRETIRED = new DicomTag(0x0028, 0x0722);

		///<summary>(0028,0730) VR=US VM=1 Adaptive Map Format (RETIRED)</summary>
		public readonly static DicomTag AdaptiveMapFormatRETIRED = new DicomTag(0x0028, 0x0730);

		///<summary>(0028,0740) VR=US VM=1 Code Number Format (RETIRED)</summary>
		public readonly static DicomTag CodeNumberFormatRETIRED = new DicomTag(0x0028, 0x0740);

		///<summary>(0028,0a02) VR=CS VM=1 Pixel Spacing Calibration Type</summary>
		public readonly static DicomTag PixelSpacingCalibrationType = new DicomTag(0x0028, 0x0a02);

		///<summary>(0028,0a04) VR=LO VM=1 Pixel Spacing Calibration Description</summary>
		public readonly static DicomTag PixelSpacingCalibrationDescription = new DicomTag(0x0028, 0x0a04);

		///<summary>(0028,1040) VR=CS VM=1 Pixel Intensity Relationship</summary>
		public readonly static DicomTag PixelIntensityRelationship = new DicomTag(0x0028, 0x1040);

		///<summary>(0028,1041) VR=SS VM=1 Pixel Intensity Relationship Sign</summary>
		public readonly static DicomTag PixelIntensityRelationshipSign = new DicomTag(0x0028, 0x1041);

		///<summary>(0028,1050) VR=DS VM=1-n Window Center</summary>
		public readonly static DicomTag WindowCenter = new DicomTag(0x0028, 0x1050);

		///<summary>(0028,1051) VR=DS VM=1-n Window Width</summary>
		public readonly static DicomTag WindowWidth = new DicomTag(0x0028, 0x1051);

		///<summary>(0028,1052) VR=DS VM=1 Rescale Intercept</summary>
		public readonly static DicomTag RescaleIntercept = new DicomTag(0x0028, 0x1052);

		///<summary>(0028,1053) VR=DS VM=1 Rescale Slope</summary>
		public readonly static DicomTag RescaleSlope = new DicomTag(0x0028, 0x1053);

		///<summary>(0028,1054) VR=LO VM=1 Rescale Type</summary>
		public readonly static DicomTag RescaleType = new DicomTag(0x0028, 0x1054);

		///<summary>(0028,1055) VR=LO VM=1-n Window Center & Width Explanation</summary>
		public readonly static DicomTag WindowCenterWidthExplanation = new DicomTag(0x0028, 0x1055);

		///<summary>(0028,1056) VR=CS VM=1 VOI LUT Function</summary>
		public readonly static DicomTag VOILUTFunction = new DicomTag(0x0028, 0x1056);

		///<summary>(0028,1080) VR=CS VM=1 Gray Scale (RETIRED)</summary>
		public readonly static DicomTag GrayScaleRETIRED = new DicomTag(0x0028, 0x1080);

		///<summary>(0028,1090) VR=CS VM=1 Recommended Viewing Mode</summary>
		public readonly static DicomTag RecommendedViewingMode = new DicomTag(0x0028, 0x1090);

		///<summary>(0028,1100) VR=US/SS VM=3 Gray Lookup Table Descriptor (RETIRED)</summary>
		public readonly static DicomTag GrayLookupTableDescriptorRETIRED = new DicomTag(0x0028, 0x1100);

		///<summary>(0028,1101) VR=US/SS VM=3 Red Palette Color Lookup Table Descriptor</summary>
		public readonly static DicomTag RedPaletteColorLookupTableDescriptor = new DicomTag(0x0028, 0x1101);

		///<summary>(0028,1102) VR=US/SS VM=3 Green Palette Color Lookup Table Descriptor</summary>
		public readonly static DicomTag GreenPaletteColorLookupTableDescriptor = new DicomTag(0x0028, 0x1102);

		///<summary>(0028,1103) VR=US/SS VM=3 Blue Palette Color Lookup Table Descriptor</summary>
		public readonly static DicomTag BluePaletteColorLookupTableDescriptor = new DicomTag(0x0028, 0x1103);

		///<summary>(0028,1104) VR=US VM=3 Alpha Palette Color Lookup Table Descriptor</summary>
		public readonly static DicomTag AlphaPaletteColorLookupTableDescriptor = new DicomTag(0x0028, 0x1104);

		///<summary>(0028,1111) VR=US/SS VM=4 Large Red Palette Color Lookup Table Descriptor (RETIRED)</summary>
		public readonly static DicomTag LargeRedPaletteColorLookupTableDescriptorRETIRED = new DicomTag(0x0028, 0x1111);

		///<summary>(0028,1112) VR=US/SS VM=4 Large Green Palette Color Lookup Table Descriptor (RETIRED)</summary>
		public readonly static DicomTag LargeGreenPaletteColorLookupTableDescriptorRETIRED = new DicomTag(0x0028, 0x1112);

		///<summary>(0028,1113) VR=US/SS VM=4 Large Blue Palette Color Lookup Table Descriptor (RETIRED)</summary>
		public readonly static DicomTag LargeBluePaletteColorLookupTableDescriptorRETIRED = new DicomTag(0x0028, 0x1113);

		///<summary>(0028,1199) VR=UI VM=1 Palette Color Lookup Table UID</summary>
		public readonly static DicomTag PaletteColorLookupTableUID = new DicomTag(0x0028, 0x1199);

		///<summary>(0028,1200) VR=US/SS/OW VM=1-n Gray Lookup Table Data (RETIRED)</summary>
		public readonly static DicomTag GrayLookupTableDataRETIRED = new DicomTag(0x0028, 0x1200);

		///<summary>(0028,1201) VR=OW VM=1 Red Palette Color Lookup Table Data</summary>
		public readonly static DicomTag RedPaletteColorLookupTableData = new DicomTag(0x0028, 0x1201);

		///<summary>(0028,1202) VR=OW VM=1 Green Palette Color Lookup Table Data</summary>
		public readonly static DicomTag GreenPaletteColorLookupTableData = new DicomTag(0x0028, 0x1202);

		///<summary>(0028,1203) VR=OW VM=1 Blue Palette Color Lookup Table Data</summary>
		public readonly static DicomTag BluePaletteColorLookupTableData = new DicomTag(0x0028, 0x1203);

		///<summary>(0028,1204) VR=OW VM=1 Alpha Palette Color Lookup Table Data</summary>
		public readonly static DicomTag AlphaPaletteColorLookupTableData = new DicomTag(0x0028, 0x1204);

		///<summary>(0028,1211) VR=OW VM=1 Large Red Palette Color Lookup Table Data (RETIRED)</summary>
		public readonly static DicomTag LargeRedPaletteColorLookupTableDataRETIRED = new DicomTag(0x0028, 0x1211);

		///<summary>(0028,1212) VR=OW VM=1 Large Green Palette Color Lookup Table Data (RETIRED)</summary>
		public readonly static DicomTag LargeGreenPaletteColorLookupTableDataRETIRED = new DicomTag(0x0028, 0x1212);

		///<summary>(0028,1213) VR=OW VM=1 Large Blue Palette Color Lookup Table Data (RETIRED)</summary>
		public readonly static DicomTag LargeBluePaletteColorLookupTableDataRETIRED = new DicomTag(0x0028, 0x1213);

		///<summary>(0028,1214) VR=UI VM=1 Large Palette Color Lookup Table UID (RETIRED)</summary>
		public readonly static DicomTag LargePaletteColorLookupTableUIDRETIRED = new DicomTag(0x0028, 0x1214);

		///<summary>(0028,1221) VR=OW VM=1 Segmented Red Palette Color Lookup Table Data</summary>
		public readonly static DicomTag SegmentedRedPaletteColorLookupTableData = new DicomTag(0x0028, 0x1221);

		///<summary>(0028,1222) VR=OW VM=1 Segmented Green Palette Color Lookup Table Data</summary>
		public readonly static DicomTag SegmentedGreenPaletteColorLookupTableData = new DicomTag(0x0028, 0x1222);

		///<summary>(0028,1223) VR=OW VM=1 Segmented Blue Palette Color Lookup Table Data</summary>
		public readonly static DicomTag SegmentedBluePaletteColorLookupTableData = new DicomTag(0x0028, 0x1223);

		///<summary>(0028,1300) VR=CS VM=1 Breast Implant Present</summary>
		public readonly static DicomTag BreastImplantPresent = new DicomTag(0x0028, 0x1300);

		///<summary>(0028,1350) VR=CS VM=1 Partial View</summary>
		public readonly static DicomTag PartialView = new DicomTag(0x0028, 0x1350);

		///<summary>(0028,1351) VR=ST VM=1 Partial View Description</summary>
		public readonly static DicomTag PartialViewDescription = new DicomTag(0x0028, 0x1351);

		///<summary>(0028,1352) VR=SQ VM=1 Partial View Code Sequence</summary>
		public readonly static DicomTag PartialViewCodeSequence = new DicomTag(0x0028, 0x1352);

		///<summary>(0028,135a) VR=CS VM=1 Spatial Locations Preserved</summary>
		public readonly static DicomTag SpatialLocationsPreserved = new DicomTag(0x0028, 0x135a);

		///<summary>(0028,1401) VR=SQ VM=1 Data Frame Assignment Sequence</summary>
		public readonly static DicomTag DataFrameAssignmentSequence = new DicomTag(0x0028, 0x1401);

		///<summary>(0028,1402) VR=CS VM=1 Data Path Assignment</summary>
		public readonly static DicomTag DataPathAssignment = new DicomTag(0x0028, 0x1402);

		///<summary>(0028,1403) VR=US VM=1 Bits Mapped to Color Lookup Table</summary>
		public readonly static DicomTag BitsMappedToColorLookupTable = new DicomTag(0x0028, 0x1403);

		///<summary>(0028,1404) VR=SQ VM=1 Blending LUT 1 Sequence</summary>
		public readonly static DicomTag BlendingLUT1Sequence = new DicomTag(0x0028, 0x1404);

		///<summary>(0028,1405) VR=CS VM=1 Blending LUT 1 Transfer Function</summary>
		public readonly static DicomTag BlendingLUT1TransferFunction = new DicomTag(0x0028, 0x1405);

		///<summary>(0028,1406) VR=FD VM=1 Blending Weight Constant</summary>
		public readonly static DicomTag BlendingWeightConstant = new DicomTag(0x0028, 0x1406);

		///<summary>(0028,1407) VR=US VM=3 Blending Lookup Table Descriptor</summary>
		public readonly static DicomTag BlendingLookupTableDescriptor = new DicomTag(0x0028, 0x1407);

		///<summary>(0028,1408) VR=OW VM=1 Blending Lookup Table Data</summary>
		public readonly static DicomTag BlendingLookupTableData = new DicomTag(0x0028, 0x1408);

		///<summary>(0028,140b) VR=SQ VM=1 Enhanced Palette Color Lookup Table Sequence</summary>
		public readonly static DicomTag EnhancedPaletteColorLookupTableSequence = new DicomTag(0x0028, 0x140b);

		///<summary>(0028,140c) VR=SQ VM=1 Blending LUT 2 Sequence</summary>
		public readonly static DicomTag BlendingLUT2Sequence = new DicomTag(0x0028, 0x140c);

		///<summary>(0028,140d) VR=CS VM=1 Blending LUT 2 Transfer Function</summary>
		public readonly static DicomTag BlendingLUT2TransferFunction = new DicomTag(0x0028, 0x140d);

		///<summary>(0028,140e) VR=CS VM=1 Data Path ID</summary>
		public readonly static DicomTag DataPathID = new DicomTag(0x0028, 0x140e);

		///<summary>(0028,140f) VR=CS VM=1 RGB LUT Transfer Function</summary>
		public readonly static DicomTag RGBLUTTransferFunction = new DicomTag(0x0028, 0x140f);

		///<summary>(0028,1410) VR=CS VM=1 Alpha LUT Transfer Function</summary>
		public readonly static DicomTag AlphaLUTTransferFunction = new DicomTag(0x0028, 0x1410);

		///<summary>(0028,2000) VR=OB VM=1 ICC Profile</summary>
		public readonly static DicomTag ICCProfile = new DicomTag(0x0028, 0x2000);

		///<summary>(0028,2110) VR=CS VM=1 Lossy Image Compression</summary>
		public readonly static DicomTag LossyImageCompression = new DicomTag(0x0028, 0x2110);

		///<summary>(0028,2112) VR=DS VM=1-n Lossy Image Compression Ratio</summary>
		public readonly static DicomTag LossyImageCompressionRatio = new DicomTag(0x0028, 0x2112);

		///<summary>(0028,2114) VR=CS VM=1-n Lossy Image Compression Method</summary>
		public readonly static DicomTag LossyImageCompressionMethod = new DicomTag(0x0028, 0x2114);

		///<summary>(0028,3000) VR=SQ VM=1 Modality LUT Sequence</summary>
		public readonly static DicomTag ModalityLUTSequence = new DicomTag(0x0028, 0x3000);

		///<summary>(0028,3002) VR=US/SS VM=3 LUT Descriptor</summary>
		public readonly static DicomTag LUTDescriptor = new DicomTag(0x0028, 0x3002);

		///<summary>(0028,3003) VR=LO VM=1 LUT Explanation</summary>
		public readonly static DicomTag LUTExplanation = new DicomTag(0x0028, 0x3003);

		///<summary>(0028,3004) VR=LO VM=1 Modality LUT Type</summary>
		public readonly static DicomTag ModalityLUTType = new DicomTag(0x0028, 0x3004);

		///<summary>(0028,3006) VR=US/OW VM=1-n LUT Data</summary>
		public readonly static DicomTag LUTData = new DicomTag(0x0028, 0x3006);

		///<summary>(0028,3010) VR=SQ VM=1 VOI LUT Sequence</summary>
		public readonly static DicomTag VOILUTSequence = new DicomTag(0x0028, 0x3010);

		///<summary>(0028,3110) VR=SQ VM=1 Softcopy VOI LUT Sequence</summary>
		public readonly static DicomTag SoftcopyVOILUTSequence = new DicomTag(0x0028, 0x3110);

		///<summary>(0028,4000) VR=LT VM=1 Image Presentation Comments (RETIRED)</summary>
		public readonly static DicomTag ImagePresentationCommentsRETIRED = new DicomTag(0x0028, 0x4000);

		///<summary>(0028,5000) VR=SQ VM=1 Bi-Plane Acquisition Sequence (RETIRED)</summary>
		public readonly static DicomTag BiPlaneAcquisitionSequenceRETIRED = new DicomTag(0x0028, 0x5000);

		///<summary>(0028,6010) VR=US VM=1 Representative Frame Number</summary>
		public readonly static DicomTag RepresentativeFrameNumber = new DicomTag(0x0028, 0x6010);

		///<summary>(0028,6020) VR=US VM=1-n Frame Numbers of Interest (FOI)</summary>
		public readonly static DicomTag FrameNumbersOfInterest = new DicomTag(0x0028, 0x6020);

		///<summary>(0028,6022) VR=LO VM=1-n Frame of Interest Description</summary>
		public readonly static DicomTag FrameOfInterestDescription = new DicomTag(0x0028, 0x6022);

		///<summary>(0028,6023) VR=CS VM=1-n Frame of Interest Type</summary>
		public readonly static DicomTag FrameOfInterestType = new DicomTag(0x0028, 0x6023);

		///<summary>(0028,6030) VR=US VM=1-n Mask Pointer(s) (RETIRED)</summary>
		public readonly static DicomTag MaskPointersRETIRED = new DicomTag(0x0028, 0x6030);

		///<summary>(0028,6040) VR=US VM=1-n R Wave Pointer</summary>
		public readonly static DicomTag RWavePointer = new DicomTag(0x0028, 0x6040);

		///<summary>(0028,6100) VR=SQ VM=1 Mask Subtraction Sequence</summary>
		public readonly static DicomTag MaskSubtractionSequence = new DicomTag(0x0028, 0x6100);

		///<summary>(0028,6101) VR=CS VM=1 Mask Operation</summary>
		public readonly static DicomTag MaskOperation = new DicomTag(0x0028, 0x6101);

		///<summary>(0028,6102) VR=US VM=2-2n Applicable Frame Range</summary>
		public readonly static DicomTag ApplicableFrameRange = new DicomTag(0x0028, 0x6102);

		///<summary>(0028,6110) VR=US VM=1-n Mask Frame Numbers</summary>
		public readonly static DicomTag MaskFrameNumbers = new DicomTag(0x0028, 0x6110);

		///<summary>(0028,6112) VR=US VM=1 Contrast Frame Averaging</summary>
		public readonly static DicomTag ContrastFrameAveraging = new DicomTag(0x0028, 0x6112);

		///<summary>(0028,6114) VR=FL VM=2 Mask Sub-pixel Shift</summary>
		public readonly static DicomTag MaskSubPixelShift = new DicomTag(0x0028, 0x6114);

		///<summary>(0028,6120) VR=SS VM=1 TID Offset</summary>
		public readonly static DicomTag TIDOffset = new DicomTag(0x0028, 0x6120);

		///<summary>(0028,6190) VR=ST VM=1 Mask Operation Explanation</summary>
		public readonly static DicomTag MaskOperationExplanation = new DicomTag(0x0028, 0x6190);

		///<summary>(0028,7fe0) VR=UT VM=1 Pixel Data Provider URL</summary>
		public readonly static DicomTag PixelDataProviderURL = new DicomTag(0x0028, 0x7fe0);

		///<summary>(0028,9001) VR=UL VM=1 Data Point Rows</summary>
		public readonly static DicomTag DataPointRows = new DicomTag(0x0028, 0x9001);

		///<summary>(0028,9002) VR=UL VM=1 Data Point Columns</summary>
		public readonly static DicomTag DataPointColumns = new DicomTag(0x0028, 0x9002);

		///<summary>(0028,9003) VR=CS VM=1 Signal Domain Columns</summary>
		public readonly static DicomTag SignalDomainColumns = new DicomTag(0x0028, 0x9003);

		///<summary>(0028,9099) VR=US VM=1 Largest Monochrome Pixel Value (RETIRED)</summary>
		public readonly static DicomTag LargestMonochromePixelValueRETIRED = new DicomTag(0x0028, 0x9099);

		///<summary>(0028,9108) VR=CS VM=1 Data Representation</summary>
		public readonly static DicomTag DataRepresentation = new DicomTag(0x0028, 0x9108);

		///<summary>(0028,9110) VR=SQ VM=1 Pixel Measures Sequence</summary>
		public readonly static DicomTag PixelMeasuresSequence = new DicomTag(0x0028, 0x9110);

		///<summary>(0028,9132) VR=SQ VM=1 Frame VOI LUT Sequence</summary>
		public readonly static DicomTag FrameVOILUTSequence = new DicomTag(0x0028, 0x9132);

		///<summary>(0028,9145) VR=SQ VM=1 Pixel Value Transformation Sequence</summary>
		public readonly static DicomTag PixelValueTransformationSequence = new DicomTag(0x0028, 0x9145);

		///<summary>(0028,9235) VR=CS VM=1 Signal Domain Rows</summary>
		public readonly static DicomTag SignalDomainRows = new DicomTag(0x0028, 0x9235);

		///<summary>(0028,9411) VR=FL VM=1 Display Filter Percentage</summary>
		public readonly static DicomTag DisplayFilterPercentage = new DicomTag(0x0028, 0x9411);

		///<summary>(0028,9415) VR=SQ VM=1 Frame Pixel Shift Sequence</summary>
		public readonly static DicomTag FramePixelShiftSequence = new DicomTag(0x0028, 0x9415);

		///<summary>(0028,9416) VR=US VM=1 Subtraction Item ID</summary>
		public readonly static DicomTag SubtractionItemID = new DicomTag(0x0028, 0x9416);

		///<summary>(0028,9422) VR=SQ VM=1 Pixel Intensity Relationship LUT Sequence</summary>
		public readonly static DicomTag PixelIntensityRelationshipLUTSequence = new DicomTag(0x0028, 0x9422);

		///<summary>(0028,9443) VR=SQ VM=1 Frame Pixel Data Properties Sequence</summary>
		public readonly static DicomTag FramePixelDataPropertiesSequence = new DicomTag(0x0028, 0x9443);

		///<summary>(0028,9444) VR=CS VM=1 Geometrical Properties</summary>
		public readonly static DicomTag GeometricalProperties = new DicomTag(0x0028, 0x9444);

		///<summary>(0028,9445) VR=FL VM=1 Geometric Maximum Distortion</summary>
		public readonly static DicomTag GeometricMaximumDistortion = new DicomTag(0x0028, 0x9445);

		///<summary>(0028,9446) VR=CS VM=1-n Image Processing Applied</summary>
		public readonly static DicomTag ImageProcessingApplied = new DicomTag(0x0028, 0x9446);

		///<summary>(0028,9454) VR=CS VM=1 Mask Selection Mode</summary>
		public readonly static DicomTag MaskSelectionMode = new DicomTag(0x0028, 0x9454);

		///<summary>(0028,9474) VR=CS VM=1 LUT Function</summary>
		public readonly static DicomTag LUTFunction = new DicomTag(0x0028, 0x9474);

		///<summary>(0028,9478) VR=FL VM=1 Mask Visibility Percentage</summary>
		public readonly static DicomTag MaskVisibilityPercentage = new DicomTag(0x0028, 0x9478);

		///<summary>(0028,9501) VR=SQ VM=1 Pixel Shift Sequence</summary>
		public readonly static DicomTag PixelShiftSequence = new DicomTag(0x0028, 0x9501);

		///<summary>(0028,9502) VR=SQ VM=1 Region Pixel Shift Sequence</summary>
		public readonly static DicomTag RegionPixelShiftSequence = new DicomTag(0x0028, 0x9502);

		///<summary>(0028,9503) VR=SS VM=2-2n Vertices of the Region</summary>
		public readonly static DicomTag VerticesOfTheRegion = new DicomTag(0x0028, 0x9503);

		///<summary>(0028,9505) VR=SQ VM=1 Multi-frame Presentation Sequence</summary>
		public readonly static DicomTag MultiFramePresentationSequence = new DicomTag(0x0028, 0x9505);

		///<summary>(0028,9506) VR=US VM=2-2n Pixel Shift Frame Range</summary>
		public readonly static DicomTag PixelShiftFrameRange = new DicomTag(0x0028, 0x9506);

		///<summary>(0028,9507) VR=US VM=2-2n LUT Frame Range</summary>
		public readonly static DicomTag LUTFrameRange = new DicomTag(0x0028, 0x9507);

		///<summary>(0028,9520) VR=DS VM=16 Image to Equipment Mapping Matrix</summary>
		public readonly static DicomTag ImageToEquipmentMappingMatrix = new DicomTag(0x0028, 0x9520);

		///<summary>(0028,9537) VR=CS VM=1 Equipment Coordinate System Identification</summary>
		public readonly static DicomTag EquipmentCoordinateSystemIdentification = new DicomTag(0x0028, 0x9537);

		///<summary>(0032,000a) VR=CS VM=1 Study Status ID (RETIRED)</summary>
		public readonly static DicomTag StudyStatusIDRETIRED = new DicomTag(0x0032, 0x000a);

		///<summary>(0032,000c) VR=CS VM=1 Study Priority ID (RETIRED)</summary>
		public readonly static DicomTag StudyPriorityIDRETIRED = new DicomTag(0x0032, 0x000c);

		///<summary>(0032,0012) VR=LO VM=1 Study ID Issuer (RETIRED)</summary>
		public readonly static DicomTag StudyIDIssuerRETIRED = new DicomTag(0x0032, 0x0012);

		///<summary>(0032,0032) VR=DA VM=1 Study Verified Date (RETIRED)</summary>
		public readonly static DicomTag StudyVerifiedDateRETIRED = new DicomTag(0x0032, 0x0032);

		///<summary>(0032,0033) VR=TM VM=1 Study Verified Time (RETIRED)</summary>
		public readonly static DicomTag StudyVerifiedTimeRETIRED = new DicomTag(0x0032, 0x0033);

		///<summary>(0032,0034) VR=DA VM=1 Study Read Date (RETIRED)</summary>
		public readonly static DicomTag StudyReadDateRETIRED = new DicomTag(0x0032, 0x0034);

		///<summary>(0032,0035) VR=TM VM=1 Study Read Time (RETIRED)</summary>
		public readonly static DicomTag StudyReadTimeRETIRED = new DicomTag(0x0032, 0x0035);

		///<summary>(0032,1000) VR=DA VM=1 Scheduled Study Start Date (RETIRED)</summary>
		public readonly static DicomTag ScheduledStudyStartDateRETIRED = new DicomTag(0x0032, 0x1000);

		///<summary>(0032,1001) VR=TM VM=1 Scheduled Study Start Time (RETIRED)</summary>
		public readonly static DicomTag ScheduledStudyStartTimeRETIRED = new DicomTag(0x0032, 0x1001);

		///<summary>(0032,1010) VR=DA VM=1 Scheduled Study Stop Date (RETIRED)</summary>
		public readonly static DicomTag ScheduledStudyStopDateRETIRED = new DicomTag(0x0032, 0x1010);

		///<summary>(0032,1011) VR=TM VM=1 Scheduled Study Stop Time (RETIRED)</summary>
		public readonly static DicomTag ScheduledStudyStopTimeRETIRED = new DicomTag(0x0032, 0x1011);

		///<summary>(0032,1020) VR=LO VM=1 Scheduled Study Location (RETIRED)</summary>
		public readonly static DicomTag ScheduledStudyLocationRETIRED = new DicomTag(0x0032, 0x1020);

		///<summary>(0032,1021) VR=AE VM=1-n Scheduled Study Location AE Title (RETIRED)</summary>
		public readonly static DicomTag ScheduledStudyLocationAETitleRETIRED = new DicomTag(0x0032, 0x1021);

		///<summary>(0032,1030) VR=LO VM=1 Reason for Study (RETIRED)</summary>
		public readonly static DicomTag ReasonForStudyRETIRED = new DicomTag(0x0032, 0x1030);

		///<summary>(0032,1031) VR=SQ VM=1 Requesting Physician Identification Sequence</summary>
		public readonly static DicomTag RequestingPhysicianIdentificationSequence = new DicomTag(0x0032, 0x1031);

		///<summary>(0032,1032) VR=PN VM=1 Requesting Physician</summary>
		public readonly static DicomTag RequestingPhysician = new DicomTag(0x0032, 0x1032);

		///<summary>(0032,1033) VR=LO VM=1 Requesting Service</summary>
		public readonly static DicomTag RequestingService = new DicomTag(0x0032, 0x1033);

		///<summary>(0032,1034) VR=SQ VM=1 Requesting Service Code Sequence</summary>
		public readonly static DicomTag RequestingServiceCodeSequence = new DicomTag(0x0032, 0x1034);

		///<summary>(0032,1040) VR=DA VM=1 Study Arrival Date (RETIRED)</summary>
		public readonly static DicomTag StudyArrivalDateRETIRED = new DicomTag(0x0032, 0x1040);

		///<summary>(0032,1041) VR=TM VM=1 Study Arrival Time (RETIRED)</summary>
		public readonly static DicomTag StudyArrivalTimeRETIRED = new DicomTag(0x0032, 0x1041);

		///<summary>(0032,1050) VR=DA VM=1 Study Completion Date (RETIRED)</summary>
		public readonly static DicomTag StudyCompletionDateRETIRED = new DicomTag(0x0032, 0x1050);

		///<summary>(0032,1051) VR=TM VM=1 Study Completion Time (RETIRED)</summary>
		public readonly static DicomTag StudyCompletionTimeRETIRED = new DicomTag(0x0032, 0x1051);

		///<summary>(0032,1055) VR=CS VM=1 Study Component Status ID (RETIRED)</summary>
		public readonly static DicomTag StudyComponentStatusIDRETIRED = new DicomTag(0x0032, 0x1055);

		///<summary>(0032,1060) VR=LO VM=1 Requested Procedure Description</summary>
		public readonly static DicomTag RequestedProcedureDescription = new DicomTag(0x0032, 0x1060);

		///<summary>(0032,1064) VR=SQ VM=1 Requested Procedure Code Sequence</summary>
		public readonly static DicomTag RequestedProcedureCodeSequence = new DicomTag(0x0032, 0x1064);

		///<summary>(0032,1070) VR=LO VM=1 Requested Contrast Agent</summary>
		public readonly static DicomTag RequestedContrastAgent = new DicomTag(0x0032, 0x1070);

		///<summary>(0032,4000) VR=LT VM=1 Study Comments (RETIRED)</summary>
		public readonly static DicomTag StudyCommentsRETIRED = new DicomTag(0x0032, 0x4000);

		///<summary>(0038,0004) VR=SQ VM=1 Referenced Patient Alias Sequence</summary>
		public readonly static DicomTag ReferencedPatientAliasSequence = new DicomTag(0x0038, 0x0004);

		///<summary>(0038,0008) VR=CS VM=1 Visit Status ID</summary>
		public readonly static DicomTag VisitStatusID = new DicomTag(0x0038, 0x0008);

		///<summary>(0038,0010) VR=LO VM=1 Admission ID</summary>
		public readonly static DicomTag AdmissionID = new DicomTag(0x0038, 0x0010);

		///<summary>(0038,0011) VR=LO VM=1 Issuer of Admission ID (RETIRED)</summary>
		public readonly static DicomTag IssuerOfAdmissionIDRETIRED = new DicomTag(0x0038, 0x0011);

		///<summary>(0038,0014) VR=SQ VM=1 Issuer of Admission ID Sequence</summary>
		public readonly static DicomTag IssuerOfAdmissionIDSequence = new DicomTag(0x0038, 0x0014);

		///<summary>(0038,0016) VR=LO VM=1 Route of Admissions</summary>
		public readonly static DicomTag RouteOfAdmissions = new DicomTag(0x0038, 0x0016);

		///<summary>(0038,001a) VR=DA VM=1 Scheduled Admission Date (RETIRED)</summary>
		public readonly static DicomTag ScheduledAdmissionDateRETIRED = new DicomTag(0x0038, 0x001a);

		///<summary>(0038,001b) VR=TM VM=1 Scheduled Admission Time (RETIRED)</summary>
		public readonly static DicomTag ScheduledAdmissionTimeRETIRED = new DicomTag(0x0038, 0x001b);

		///<summary>(0038,001c) VR=DA VM=1 Scheduled Discharge Date (RETIRED)</summary>
		public readonly static DicomTag ScheduledDischargeDateRETIRED = new DicomTag(0x0038, 0x001c);

		///<summary>(0038,001d) VR=TM VM=1 Scheduled Discharge Time (RETIRED)</summary>
		public readonly static DicomTag ScheduledDischargeTimeRETIRED = new DicomTag(0x0038, 0x001d);

		///<summary>(0038,001e) VR=LO VM=1 Scheduled Patient Institution Residence (RETIRED)</summary>
		public readonly static DicomTag ScheduledPatientInstitutionResidenceRETIRED = new DicomTag(0x0038, 0x001e);

		///<summary>(0038,0020) VR=DA VM=1 Admitting Date</summary>
		public readonly static DicomTag AdmittingDate = new DicomTag(0x0038, 0x0020);

		///<summary>(0038,0021) VR=TM VM=1 Admitting Time</summary>
		public readonly static DicomTag AdmittingTime = new DicomTag(0x0038, 0x0021);

		///<summary>(0038,0030) VR=DA VM=1 Discharge Date (RETIRED)</summary>
		public readonly static DicomTag DischargeDateRETIRED = new DicomTag(0x0038, 0x0030);

		///<summary>(0038,0032) VR=TM VM=1 Discharge Time (RETIRED)</summary>
		public readonly static DicomTag DischargeTimeRETIRED = new DicomTag(0x0038, 0x0032);

		///<summary>(0038,0040) VR=LO VM=1 Discharge Diagnosis Description (RETIRED)</summary>
		public readonly static DicomTag DischargeDiagnosisDescriptionRETIRED = new DicomTag(0x0038, 0x0040);

		///<summary>(0038,0044) VR=SQ VM=1 Discharge Diagnosis Code Sequence (RETIRED)</summary>
		public readonly static DicomTag DischargeDiagnosisCodeSequenceRETIRED = new DicomTag(0x0038, 0x0044);

		///<summary>(0038,0050) VR=LO VM=1 Special Needs</summary>
		public readonly static DicomTag SpecialNeeds = new DicomTag(0x0038, 0x0050);

		///<summary>(0038,0060) VR=LO VM=1 Service Episode ID</summary>
		public readonly static DicomTag ServiceEpisodeID = new DicomTag(0x0038, 0x0060);

		///<summary>(0038,0061) VR=LO VM=1 Issuer of Service Episode ID (RETIRED)</summary>
		public readonly static DicomTag IssuerOfServiceEpisodeIDRETIRED = new DicomTag(0x0038, 0x0061);

		///<summary>(0038,0062) VR=LO VM=1 Service Episode Description</summary>
		public readonly static DicomTag ServiceEpisodeDescription = new DicomTag(0x0038, 0x0062);

		///<summary>(0038,0064) VR=SQ VM=1 Issuer of Service Episode ID Sequence</summary>
		public readonly static DicomTag IssuerOfServiceEpisodeIDSequence = new DicomTag(0x0038, 0x0064);

		///<summary>(0038,0100) VR=SQ VM=1 Pertinent Documents Sequence</summary>
		public readonly static DicomTag PertinentDocumentsSequence = new DicomTag(0x0038, 0x0100);

		///<summary>(0038,0300) VR=LO VM=1 Current Patient Location</summary>
		public readonly static DicomTag CurrentPatientLocation = new DicomTag(0x0038, 0x0300);

		///<summary>(0038,0400) VR=LO VM=1 Patient’s Institution Residence</summary>
		public readonly static DicomTag PatientInstitutionResidence = new DicomTag(0x0038, 0x0400);

		///<summary>(0038,0500) VR=LO VM=1 Patient State</summary>
		public readonly static DicomTag PatientState = new DicomTag(0x0038, 0x0500);

		///<summary>(0038,0502) VR=SQ VM=1 Patient Clinical Trial Participation Sequence</summary>
		public readonly static DicomTag PatientClinicalTrialParticipationSequence = new DicomTag(0x0038, 0x0502);

		///<summary>(0038,4000) VR=LT VM=1 Visit Comments</summary>
		public readonly static DicomTag VisitComments = new DicomTag(0x0038, 0x4000);

		///<summary>(003a,0004) VR=CS VM=1 Waveform Originality</summary>
		public readonly static DicomTag WaveformOriginality = new DicomTag(0x003a, 0x0004);

		///<summary>(003a,0005) VR=US VM=1 Number of Waveform Channels</summary>
		public readonly static DicomTag NumberOfWaveformChannels = new DicomTag(0x003a, 0x0005);

		///<summary>(003a,0010) VR=UL VM=1 Number of Waveform Samples</summary>
		public readonly static DicomTag NumberOfWaveformSamples = new DicomTag(0x003a, 0x0010);

		///<summary>(003a,001a) VR=DS VM=1 Sampling Frequency</summary>
		public readonly static DicomTag SamplingFrequency = new DicomTag(0x003a, 0x001a);

		///<summary>(003a,0020) VR=SH VM=1 Multiplex Group Label</summary>
		public readonly static DicomTag MultiplexGroupLabel = new DicomTag(0x003a, 0x0020);

		///<summary>(003a,0200) VR=SQ VM=1 Channel Definition Sequence</summary>
		public readonly static DicomTag ChannelDefinitionSequence = new DicomTag(0x003a, 0x0200);

		///<summary>(003a,0202) VR=IS VM=1 Waveform Channel Number</summary>
		public readonly static DicomTag WaveformChannelNumber = new DicomTag(0x003a, 0x0202);

		///<summary>(003a,0203) VR=SH VM=1 Channel Label</summary>
		public readonly static DicomTag ChannelLabel = new DicomTag(0x003a, 0x0203);

		///<summary>(003a,0205) VR=CS VM=1-n Channel Status</summary>
		public readonly static DicomTag ChannelStatus = new DicomTag(0x003a, 0x0205);

		///<summary>(003a,0208) VR=SQ VM=1 Channel Source Sequence</summary>
		public readonly static DicomTag ChannelSourceSequence = new DicomTag(0x003a, 0x0208);

		///<summary>(003a,0209) VR=SQ VM=1 Channel Source Modifiers Sequence</summary>
		public readonly static DicomTag ChannelSourceModifiersSequence = new DicomTag(0x003a, 0x0209);

		///<summary>(003a,020a) VR=SQ VM=1 Source Waveform Sequence</summary>
		public readonly static DicomTag SourceWaveformSequence = new DicomTag(0x003a, 0x020a);

		///<summary>(003a,020c) VR=LO VM=1 Channel Derivation Description</summary>
		public readonly static DicomTag ChannelDerivationDescription = new DicomTag(0x003a, 0x020c);

		///<summary>(003a,0210) VR=DS VM=1 Channel Sensitivity</summary>
		public readonly static DicomTag ChannelSensitivity = new DicomTag(0x003a, 0x0210);

		///<summary>(003a,0211) VR=SQ VM=1 Channel Sensitivity Units Sequence</summary>
		public readonly static DicomTag ChannelSensitivityUnitsSequence = new DicomTag(0x003a, 0x0211);

		///<summary>(003a,0212) VR=DS VM=1 Channel Sensitivity Correction Factor</summary>
		public readonly static DicomTag ChannelSensitivityCorrectionFactor = new DicomTag(0x003a, 0x0212);

		///<summary>(003a,0213) VR=DS VM=1 Channel Baseline</summary>
		public readonly static DicomTag ChannelBaseline = new DicomTag(0x003a, 0x0213);

		///<summary>(003a,0214) VR=DS VM=1 Channel Time Skew</summary>
		public readonly static DicomTag ChannelTimeSkew = new DicomTag(0x003a, 0x0214);

		///<summary>(003a,0215) VR=DS VM=1 Channel Sample Skew</summary>
		public readonly static DicomTag ChannelSampleSkew = new DicomTag(0x003a, 0x0215);

		///<summary>(003a,0218) VR=DS VM=1 Channel Offset</summary>
		public readonly static DicomTag ChannelOffset = new DicomTag(0x003a, 0x0218);

		///<summary>(003a,021a) VR=US VM=1 Waveform Bits Stored</summary>
		public readonly static DicomTag WaveformBitsStored = new DicomTag(0x003a, 0x021a);

		///<summary>(003a,0220) VR=DS VM=1 Filter Low Frequency</summary>
		public readonly static DicomTag FilterLowFrequency = new DicomTag(0x003a, 0x0220);

		///<summary>(003a,0221) VR=DS VM=1 Filter High Frequency</summary>
		public readonly static DicomTag FilterHighFrequency = new DicomTag(0x003a, 0x0221);

		///<summary>(003a,0222) VR=DS VM=1 Notch Filter Frequency</summary>
		public readonly static DicomTag NotchFilterFrequency = new DicomTag(0x003a, 0x0222);

		///<summary>(003a,0223) VR=DS VM=1 Notch Filter Bandwidth</summary>
		public readonly static DicomTag NotchFilterBandwidth = new DicomTag(0x003a, 0x0223);

		///<summary>(003a,0230) VR=FL VM=1 Waveform Data Display Scale</summary>
		public readonly static DicomTag WaveformDataDisplayScale = new DicomTag(0x003a, 0x0230);

		///<summary>(003a,0231) VR=US VM=3 Waveform Display Background CIELab Value</summary>
		public readonly static DicomTag WaveformDisplayBackgroundCIELabValue = new DicomTag(0x003a, 0x0231);

		///<summary>(003a,0240) VR=SQ VM=1 Waveform Presentation Group Sequence</summary>
		public readonly static DicomTag WaveformPresentationGroupSequence = new DicomTag(0x003a, 0x0240);

		///<summary>(003a,0241) VR=US VM=1 Presentation Group Number</summary>
		public readonly static DicomTag PresentationGroupNumber = new DicomTag(0x003a, 0x0241);

		///<summary>(003a,0242) VR=SQ VM=1 Channel Display Sequence</summary>
		public readonly static DicomTag ChannelDisplaySequence = new DicomTag(0x003a, 0x0242);

		///<summary>(003a,0244) VR=US VM=3 Channel Recommended Display CIELab Value</summary>
		public readonly static DicomTag ChannelRecommendedDisplayCIELabValue = new DicomTag(0x003a, 0x0244);

		///<summary>(003a,0245) VR=FL VM=1 Channel Position</summary>
		public readonly static DicomTag ChannelPosition = new DicomTag(0x003a, 0x0245);

		///<summary>(003a,0246) VR=CS VM=1 Display Shading Flag</summary>
		public readonly static DicomTag DisplayShadingFlag = new DicomTag(0x003a, 0x0246);

		///<summary>(003a,0247) VR=FL VM=1 Fractional Channel Display Scale</summary>
		public readonly static DicomTag FractionalChannelDisplayScale = new DicomTag(0x003a, 0x0247);

		///<summary>(003a,0248) VR=FL VM=1 Absolute Channel Display Scale</summary>
		public readonly static DicomTag AbsoluteChannelDisplayScale = new DicomTag(0x003a, 0x0248);

		///<summary>(003a,0300) VR=SQ VM=1 Multiplexed Audio Channels Description Code Sequence</summary>
		public readonly static DicomTag MultiplexedAudioChannelsDescriptionCodeSequence = new DicomTag(0x003a, 0x0300);

		///<summary>(003a,0301) VR=IS VM=1 Channel Identification Code</summary>
		public readonly static DicomTag ChannelIdentificationCode = new DicomTag(0x003a, 0x0301);

		///<summary>(003a,0302) VR=CS VM=1 Channel Mode</summary>
		public readonly static DicomTag ChannelMode = new DicomTag(0x003a, 0x0302);

		///<summary>(0040,0001) VR=AE VM=1-n Scheduled Station AE Title</summary>
		public readonly static DicomTag ScheduledStationAETitle = new DicomTag(0x0040, 0x0001);

		///<summary>(0040,0002) VR=DA VM=1 Scheduled Procedure Step Start Date</summary>
		public readonly static DicomTag ScheduledProcedureStepStartDate = new DicomTag(0x0040, 0x0002);

		///<summary>(0040,0003) VR=TM VM=1 Scheduled Procedure Step Start Time</summary>
		public readonly static DicomTag ScheduledProcedureStepStartTime = new DicomTag(0x0040, 0x0003);

		///<summary>(0040,0004) VR=DA VM=1 Scheduled Procedure Step End Date</summary>
		public readonly static DicomTag ScheduledProcedureStepEndDate = new DicomTag(0x0040, 0x0004);

		///<summary>(0040,0005) VR=TM VM=1 Scheduled Procedure Step End Time</summary>
		public readonly static DicomTag ScheduledProcedureStepEndTime = new DicomTag(0x0040, 0x0005);

		///<summary>(0040,0006) VR=PN VM=1 Scheduled Performing Physician’s Name</summary>
		public readonly static DicomTag ScheduledPerformingPhysicianName = new DicomTag(0x0040, 0x0006);

		///<summary>(0040,0007) VR=LO VM=1 Scheduled Procedure Step Description</summary>
		public readonly static DicomTag ScheduledProcedureStepDescription = new DicomTag(0x0040, 0x0007);

		///<summary>(0040,0008) VR=SQ VM=1 Scheduled Protocol Code Sequence</summary>
		public readonly static DicomTag ScheduledProtocolCodeSequence = new DicomTag(0x0040, 0x0008);

		///<summary>(0040,0009) VR=SH VM=1 Scheduled Procedure Step ID</summary>
		public readonly static DicomTag ScheduledProcedureStepID = new DicomTag(0x0040, 0x0009);

		///<summary>(0040,000a) VR=SQ VM=1 Stage Code Sequence</summary>
		public readonly static DicomTag StageCodeSequence = new DicomTag(0x0040, 0x000a);

		///<summary>(0040,000b) VR=SQ VM=1 Scheduled Performing Physician Identification Sequence</summary>
		public readonly static DicomTag ScheduledPerformingPhysicianIdentificationSequence = new DicomTag(0x0040, 0x000b);

		///<summary>(0040,0010) VR=SH VM=1-n Scheduled Station Name</summary>
		public readonly static DicomTag ScheduledStationName = new DicomTag(0x0040, 0x0010);

		///<summary>(0040,0011) VR=SH VM=1 Scheduled Procedure Step Location</summary>
		public readonly static DicomTag ScheduledProcedureStepLocation = new DicomTag(0x0040, 0x0011);

		///<summary>(0040,0012) VR=LO VM=1 Pre-Medication</summary>
		public readonly static DicomTag PreMedication = new DicomTag(0x0040, 0x0012);

		///<summary>(0040,0020) VR=CS VM=1 Scheduled Procedure Step Status</summary>
		public readonly static DicomTag ScheduledProcedureStepStatus = new DicomTag(0x0040, 0x0020);

		///<summary>(0040,0026) VR=SQ VM=1 Order Placer Identifier Sequence</summary>
		public readonly static DicomTag OrderPlacerIdentifierSequence = new DicomTag(0x0040, 0x0026);

		///<summary>(0040,0027) VR=SQ VM=1 Order Filler Identifier Sequence</summary>
		public readonly static DicomTag OrderFillerIdentifierSequence = new DicomTag(0x0040, 0x0027);

		///<summary>(0040,0031) VR=UT VM=1 Local Namespace Entity ID</summary>
		public readonly static DicomTag LocalNamespaceEntityID = new DicomTag(0x0040, 0x0031);

		///<summary>(0040,0032) VR=UT VM=1 Universal Entity ID</summary>
		public readonly static DicomTag UniversalEntityID = new DicomTag(0x0040, 0x0032);

		///<summary>(0040,0033) VR=CS VM=1 Universal Entity ID Type</summary>
		public readonly static DicomTag UniversalEntityIDType = new DicomTag(0x0040, 0x0033);

		///<summary>(0040,0035) VR=CS VM=1 Identifier Type Code</summary>
		public readonly static DicomTag IdentifierTypeCode = new DicomTag(0x0040, 0x0035);

		///<summary>(0040,0036) VR=SQ VM=1 Assigning Facility Sequence</summary>
		public readonly static DicomTag AssigningFacilitySequence = new DicomTag(0x0040, 0x0036);

		///<summary>(0040,0039) VR=SQ VM=1 Assigning Jurisdiction Code Sequence</summary>
		public readonly static DicomTag AssigningJurisdictionCodeSequence = new DicomTag(0x0040, 0x0039);

		///<summary>(0040,003a) VR=SQ VM=1 Assigning Agency or Department Code Sequence</summary>
		public readonly static DicomTag AssigningAgencyOrDepartmentCodeSequence = new DicomTag(0x0040, 0x003a);

		///<summary>(0040,0100) VR=SQ VM=1 Scheduled Procedure Step Sequence</summary>
		public readonly static DicomTag ScheduledProcedureStepSequence = new DicomTag(0x0040, 0x0100);

		///<summary>(0040,0220) VR=SQ VM=1 Referenced Non-Image Composite SOP Instance Sequence</summary>
		public readonly static DicomTag ReferencedNonImageCompositeSOPInstanceSequence = new DicomTag(0x0040, 0x0220);

		///<summary>(0040,0241) VR=AE VM=1 Performed Station AE Title</summary>
		public readonly static DicomTag PerformedStationAETitle = new DicomTag(0x0040, 0x0241);

		///<summary>(0040,0242) VR=SH VM=1 Performed Station Name</summary>
		public readonly static DicomTag PerformedStationName = new DicomTag(0x0040, 0x0242);

		///<summary>(0040,0243) VR=SH VM=1 Performed Location</summary>
		public readonly static DicomTag PerformedLocation = new DicomTag(0x0040, 0x0243);

		///<summary>(0040,0244) VR=DA VM=1 Performed Procedure Step Start Date</summary>
		public readonly static DicomTag PerformedProcedureStepStartDate = new DicomTag(0x0040, 0x0244);

		///<summary>(0040,0245) VR=TM VM=1 Performed Procedure Step Start Time</summary>
		public readonly static DicomTag PerformedProcedureStepStartTime = new DicomTag(0x0040, 0x0245);

		///<summary>(0040,0250) VR=DA VM=1 Performed Procedure Step End Date</summary>
		public readonly static DicomTag PerformedProcedureStepEndDate = new DicomTag(0x0040, 0x0250);

		///<summary>(0040,0251) VR=TM VM=1 Performed Procedure Step End Time</summary>
		public readonly static DicomTag PerformedProcedureStepEndTime = new DicomTag(0x0040, 0x0251);

		///<summary>(0040,0252) VR=CS VM=1 Performed Procedure Step Status</summary>
		public readonly static DicomTag PerformedProcedureStepStatus = new DicomTag(0x0040, 0x0252);

		///<summary>(0040,0253) VR=SH VM=1 Performed Procedure Step ID</summary>
		public readonly static DicomTag PerformedProcedureStepID = new DicomTag(0x0040, 0x0253);

		///<summary>(0040,0254) VR=LO VM=1 Performed Procedure Step Description</summary>
		public readonly static DicomTag PerformedProcedureStepDescription = new DicomTag(0x0040, 0x0254);

		///<summary>(0040,0255) VR=LO VM=1 Performed Procedure Type Description</summary>
		public readonly static DicomTag PerformedProcedureTypeDescription = new DicomTag(0x0040, 0x0255);

		///<summary>(0040,0260) VR=SQ VM=1 Performed Protocol Code Sequence</summary>
		public readonly static DicomTag PerformedProtocolCodeSequence = new DicomTag(0x0040, 0x0260);

		///<summary>(0040,0261) VR=CS VM=1 Performed Protocol Type</summary>
		public readonly static DicomTag PerformedProtocolType = new DicomTag(0x0040, 0x0261);

		///<summary>(0040,0270) VR=SQ VM=1 Scheduled Step Attributes Sequence</summary>
		public readonly static DicomTag ScheduledStepAttributesSequence = new DicomTag(0x0040, 0x0270);

		///<summary>(0040,0275) VR=SQ VM=1 Request Attributes Sequence</summary>
		public readonly static DicomTag RequestAttributesSequence = new DicomTag(0x0040, 0x0275);

		///<summary>(0040,0280) VR=ST VM=1 Comments on the Performed Procedure Step</summary>
		public readonly static DicomTag CommentsOnThePerformedProcedureStep = new DicomTag(0x0040, 0x0280);

		///<summary>(0040,0281) VR=SQ VM=1 Performed Procedure Step Discontinuation Reason Code Sequence</summary>
		public readonly static DicomTag PerformedProcedureStepDiscontinuationReasonCodeSequence = new DicomTag(0x0040, 0x0281);

		///<summary>(0040,0293) VR=SQ VM=1 Quantity Sequence</summary>
		public readonly static DicomTag QuantitySequence = new DicomTag(0x0040, 0x0293);

		///<summary>(0040,0294) VR=DS VM=1 Quantity</summary>
		public readonly static DicomTag Quantity = new DicomTag(0x0040, 0x0294);

		///<summary>(0040,0295) VR=SQ VM=1 Measuring Units Sequence</summary>
		public readonly static DicomTag MeasuringUnitsSequence = new DicomTag(0x0040, 0x0295);

		///<summary>(0040,0296) VR=SQ VM=1 Billing Item Sequence</summary>
		public readonly static DicomTag BillingItemSequence = new DicomTag(0x0040, 0x0296);

		///<summary>(0040,0300) VR=US VM=1 Total Time of Fluoroscopy</summary>
		public readonly static DicomTag TotalTimeOfFluoroscopy = new DicomTag(0x0040, 0x0300);

		///<summary>(0040,0301) VR=US VM=1 Total Number of Exposures</summary>
		public readonly static DicomTag TotalNumberOfExposures = new DicomTag(0x0040, 0x0301);

		///<summary>(0040,0302) VR=US VM=1 Entrance Dose</summary>
		public readonly static DicomTag EntranceDose = new DicomTag(0x0040, 0x0302);

		///<summary>(0040,0303) VR=US VM=1-2 Exposed Area</summary>
		public readonly static DicomTag ExposedArea = new DicomTag(0x0040, 0x0303);

		///<summary>(0040,0306) VR=DS VM=1 Distance Source to Entrance</summary>
		public readonly static DicomTag DistanceSourceToEntrance = new DicomTag(0x0040, 0x0306);

		///<summary>(0040,0307) VR=DS VM=1 Distance Source to Support (RETIRED)</summary>
		public readonly static DicomTag DistanceSourceToSupportRETIRED = new DicomTag(0x0040, 0x0307);

		///<summary>(0040,030e) VR=SQ VM=1 Exposure Dose Sequence</summary>
		public readonly static DicomTag ExposureDoseSequence = new DicomTag(0x0040, 0x030e);

		///<summary>(0040,0310) VR=ST VM=1 Comments on Radiation Dose</summary>
		public readonly static DicomTag CommentsOnRadiationDose = new DicomTag(0x0040, 0x0310);

		///<summary>(0040,0312) VR=DS VM=1 X-Ray Output</summary>
		public readonly static DicomTag XRayOutput = new DicomTag(0x0040, 0x0312);

		///<summary>(0040,0314) VR=DS VM=1 Half Value Layer</summary>
		public readonly static DicomTag HalfValueLayer = new DicomTag(0x0040, 0x0314);

		///<summary>(0040,0316) VR=DS VM=1 Organ Dose</summary>
		public readonly static DicomTag OrganDose = new DicomTag(0x0040, 0x0316);

		///<summary>(0040,0318) VR=CS VM=1 Organ Exposed</summary>
		public readonly static DicomTag OrganExposed = new DicomTag(0x0040, 0x0318);

		///<summary>(0040,0320) VR=SQ VM=1 Billing Procedure Step Sequence</summary>
		public readonly static DicomTag BillingProcedureStepSequence = new DicomTag(0x0040, 0x0320);

		///<summary>(0040,0321) VR=SQ VM=1 Film Consumption Sequence</summary>
		public readonly static DicomTag FilmConsumptionSequence = new DicomTag(0x0040, 0x0321);

		///<summary>(0040,0324) VR=SQ VM=1 Billing Supplies and Devices Sequence</summary>
		public readonly static DicomTag BillingSuppliesAndDevicesSequence = new DicomTag(0x0040, 0x0324);

		///<summary>(0040,0330) VR=SQ VM=1 Referenced Procedure Step Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedProcedureStepSequenceRETIRED = new DicomTag(0x0040, 0x0330);

		///<summary>(0040,0340) VR=SQ VM=1 Performed Series Sequence</summary>
		public readonly static DicomTag PerformedSeriesSequence = new DicomTag(0x0040, 0x0340);

		///<summary>(0040,0400) VR=LT VM=1 Comments on the Scheduled Procedure Step</summary>
		public readonly static DicomTag CommentsOnTheScheduledProcedureStep = new DicomTag(0x0040, 0x0400);

		///<summary>(0040,0440) VR=SQ VM=1 Protocol Context Sequence</summary>
		public readonly static DicomTag ProtocolContextSequence = new DicomTag(0x0040, 0x0440);

		///<summary>(0040,0441) VR=SQ VM=1 Content Item Modifier Sequence</summary>
		public readonly static DicomTag ContentItemModifierSequence = new DicomTag(0x0040, 0x0441);

		///<summary>(0040,0500) VR=SQ VM=1 Scheduled Specimen Sequence</summary>
		public readonly static DicomTag ScheduledSpecimenSequence = new DicomTag(0x0040, 0x0500);

		///<summary>(0040,050a) VR=LO VM=1 Specimen Accession Number (RETIRED)</summary>
		public readonly static DicomTag SpecimenAccessionNumberRETIRED = new DicomTag(0x0040, 0x050a);

		///<summary>(0040,0512) VR=LO VM=1 Container Identifier</summary>
		public readonly static DicomTag ContainerIdentifier = new DicomTag(0x0040, 0x0512);

		///<summary>(0040,0513) VR=SQ VM=1 Issuer of the Container Identifier Sequence</summary>
		public readonly static DicomTag IssuerOfTheContainerIdentifierSequence = new DicomTag(0x0040, 0x0513);

		///<summary>(0040,0515) VR=SQ VM=1 Alternate Container Identifier Sequence</summary>
		public readonly static DicomTag AlternateContainerIdentifierSequence = new DicomTag(0x0040, 0x0515);

		///<summary>(0040,0518) VR=SQ VM=1 Container Type Code Sequence</summary>
		public readonly static DicomTag ContainerTypeCodeSequence = new DicomTag(0x0040, 0x0518);

		///<summary>(0040,051a) VR=LO VM=1 Container Description</summary>
		public readonly static DicomTag ContainerDescription = new DicomTag(0x0040, 0x051a);

		///<summary>(0040,0520) VR=SQ VM=1 Container Component Sequence</summary>
		public readonly static DicomTag ContainerComponentSequence = new DicomTag(0x0040, 0x0520);

		///<summary>(0040,0550) VR=SQ VM=1 Specimen Sequence (RETIRED)</summary>
		public readonly static DicomTag SpecimenSequenceRETIRED = new DicomTag(0x0040, 0x0550);

		///<summary>(0040,0551) VR=LO VM=1 Specimen Identifier</summary>
		public readonly static DicomTag SpecimenIdentifier = new DicomTag(0x0040, 0x0551);

		///<summary>(0040,0552) VR=SQ VM=1 Specimen Description Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag SpecimenDescriptionSequenceTrialRETIRED = new DicomTag(0x0040, 0x0552);

		///<summary>(0040,0553) VR=ST VM=1 Specimen Description (Trial) (RETIRED)</summary>
		public readonly static DicomTag SpecimenDescriptionTrialRETIRED = new DicomTag(0x0040, 0x0553);

		///<summary>(0040,0554) VR=UI VM=1 Specimen UID</summary>
		public readonly static DicomTag SpecimenUID = new DicomTag(0x0040, 0x0554);

		///<summary>(0040,0555) VR=SQ VM=1 Acquisition Context Sequence</summary>
		public readonly static DicomTag AcquisitionContextSequence = new DicomTag(0x0040, 0x0555);

		///<summary>(0040,0556) VR=ST VM=1 Acquisition Context Description</summary>
		public readonly static DicomTag AcquisitionContextDescription = new DicomTag(0x0040, 0x0556);

		///<summary>(0040,0560) VR=SQ VM=1 Specimen Description Sequence</summary>
		public readonly static DicomTag SpecimenDescriptionSequence = new DicomTag(0x0040, 0x0560);

		///<summary>(0040,0562) VR=SQ VM=1 Issuer of the Specimen Identifier Sequence</summary>
		public readonly static DicomTag IssuerOfTheSpecimenIdentifierSequence = new DicomTag(0x0040, 0x0562);

		///<summary>(0040,059a) VR=SQ VM=1 Specimen Type Code Sequence</summary>
		public readonly static DicomTag SpecimenTypeCodeSequence = new DicomTag(0x0040, 0x059a);

		///<summary>(0040,0600) VR=LO VM=1 Specimen Short Description</summary>
		public readonly static DicomTag SpecimenShortDescription = new DicomTag(0x0040, 0x0600);

		///<summary>(0040,0602) VR=UT VM=1 Specimen Detailed Description</summary>
		public readonly static DicomTag SpecimenDetailedDescription = new DicomTag(0x0040, 0x0602);

		///<summary>(0040,0610) VR=SQ VM=1 Specimen Preparation Sequence</summary>
		public readonly static DicomTag SpecimenPreparationSequence = new DicomTag(0x0040, 0x0610);

		///<summary>(0040,0612) VR=SQ VM=1 Specimen Preparation Step Content Item Sequence</summary>
		public readonly static DicomTag SpecimenPreparationStepContentItemSequence = new DicomTag(0x0040, 0x0612);

		///<summary>(0040,0620) VR=SQ VM=1 Specimen Localization Content Item Sequence</summary>
		public readonly static DicomTag SpecimenLocalizationContentItemSequence = new DicomTag(0x0040, 0x0620);

		///<summary>(0040,06fa) VR=LO VM=1 Slide Identifier (RETIRED)</summary>
		public readonly static DicomTag SlideIdentifierRETIRED = new DicomTag(0x0040, 0x06fa);

		///<summary>(0040,071a) VR=SQ VM=1 Image Center Point Coordinates Sequence</summary>
		public readonly static DicomTag ImageCenterPointCoordinatesSequence = new DicomTag(0x0040, 0x071a);

		///<summary>(0040,072a) VR=DS VM=1 X Offset in Slide Coordinate System</summary>
		public readonly static DicomTag XOffsetInSlideCoordinateSystem = new DicomTag(0x0040, 0x072a);

		///<summary>(0040,073a) VR=DS VM=1 Y Offset in Slide Coordinate System</summary>
		public readonly static DicomTag YOffsetInSlideCoordinateSystem = new DicomTag(0x0040, 0x073a);

		///<summary>(0040,074a) VR=DS VM=1 Z Offset in Slide Coordinate System</summary>
		public readonly static DicomTag ZOffsetInSlideCoordinateSystem = new DicomTag(0x0040, 0x074a);

		///<summary>(0040,08d8) VR=SQ VM=1 Pixel Spacing Sequence (RETIRED)</summary>
		public readonly static DicomTag PixelSpacingSequenceRETIRED = new DicomTag(0x0040, 0x08d8);

		///<summary>(0040,08da) VR=SQ VM=1 Coordinate System Axis Code Sequence (RETIRED)</summary>
		public readonly static DicomTag CoordinateSystemAxisCodeSequenceRETIRED = new DicomTag(0x0040, 0x08da);

		///<summary>(0040,08ea) VR=SQ VM=1 Measurement Units Code Sequence</summary>
		public readonly static DicomTag MeasurementUnitsCodeSequence = new DicomTag(0x0040, 0x08ea);

		///<summary>(0040,09f8) VR=SQ VM=1 Vital Stain Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag VitalStainCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0x09f8);

		///<summary>(0040,1001) VR=SH VM=1 Requested Procedure ID</summary>
		public readonly static DicomTag RequestedProcedureID = new DicomTag(0x0040, 0x1001);

		///<summary>(0040,1002) VR=LO VM=1 Reason for the Requested Procedure</summary>
		public readonly static DicomTag ReasonForTheRequestedProcedure = new DicomTag(0x0040, 0x1002);

		///<summary>(0040,1003) VR=SH VM=1 Requested Procedure Priority</summary>
		public readonly static DicomTag RequestedProcedurePriority = new DicomTag(0x0040, 0x1003);

		///<summary>(0040,1004) VR=LO VM=1 Patient Transport Arrangements</summary>
		public readonly static DicomTag PatientTransportArrangements = new DicomTag(0x0040, 0x1004);

		///<summary>(0040,1005) VR=LO VM=1 Requested Procedure Location</summary>
		public readonly static DicomTag RequestedProcedureLocation = new DicomTag(0x0040, 0x1005);

		///<summary>(0040,1006) VR=SH VM=1 Placer Order Number / Procedure (RETIRED)</summary>
		public readonly static DicomTag PlacerOrderNumberProcedureRETIRED = new DicomTag(0x0040, 0x1006);

		///<summary>(0040,1007) VR=SH VM=1 Filler Order Number / Procedure (RETIRED)</summary>
		public readonly static DicomTag FillerOrderNumberProcedureRETIRED = new DicomTag(0x0040, 0x1007);

		///<summary>(0040,1008) VR=LO VM=1 Confidentiality Code</summary>
		public readonly static DicomTag ConfidentialityCode = new DicomTag(0x0040, 0x1008);

		///<summary>(0040,1009) VR=SH VM=1 Reporting Priority</summary>
		public readonly static DicomTag ReportingPriority = new DicomTag(0x0040, 0x1009);

		///<summary>(0040,100a) VR=SQ VM=1 Reason for Requested Procedure Code Sequence</summary>
		public readonly static DicomTag ReasonForRequestedProcedureCodeSequence = new DicomTag(0x0040, 0x100a);

		///<summary>(0040,1010) VR=PN VM=1-n Names of Intended Recipients of Results</summary>
		public readonly static DicomTag NamesOfIntendedRecipientsOfResults = new DicomTag(0x0040, 0x1010);

		///<summary>(0040,1011) VR=SQ VM=1 Intended Recipients of Results Identification Sequence</summary>
		public readonly static DicomTag IntendedRecipientsOfResultsIdentificationSequence = new DicomTag(0x0040, 0x1011);

		///<summary>(0040,1012) VR=SQ VM=1 Reason For Performed Procedure Code Sequence</summary>
		public readonly static DicomTag ReasonForPerformedProcedureCodeSequence = new DicomTag(0x0040, 0x1012);

		///<summary>(0040,1060) VR=LO VM=1 Requested Procedure Description (Trial) (RETIRED)</summary>
		public readonly static DicomTag RequestedProcedureDescriptionTrialRETIRED = new DicomTag(0x0040, 0x1060);

		///<summary>(0040,1101) VR=SQ VM=1 Person Identification Code Sequence</summary>
		public readonly static DicomTag PersonIdentificationCodeSequence = new DicomTag(0x0040, 0x1101);

		///<summary>(0040,1102) VR=ST VM=1 Person’s Address</summary>
		public readonly static DicomTag PersonAddress = new DicomTag(0x0040, 0x1102);

		///<summary>(0040,1103) VR=LO VM=1-n Person’s Telephone Numbers</summary>
		public readonly static DicomTag PersonTelephoneNumbers = new DicomTag(0x0040, 0x1103);

		///<summary>(0040,1400) VR=LT VM=1 Requested Procedure Comments</summary>
		public readonly static DicomTag RequestedProcedureComments = new DicomTag(0x0040, 0x1400);

		///<summary>(0040,2001) VR=LO VM=1 Reason for the Imaging Service Request (RETIRED)</summary>
		public readonly static DicomTag ReasonForTheImagingServiceRequestRETIRED = new DicomTag(0x0040, 0x2001);

		///<summary>(0040,2004) VR=DA VM=1 Issue Date of Imaging Service Request</summary>
		public readonly static DicomTag IssueDateOfImagingServiceRequest = new DicomTag(0x0040, 0x2004);

		///<summary>(0040,2005) VR=TM VM=1 Issue Time of Imaging Service Request</summary>
		public readonly static DicomTag IssueTimeOfImagingServiceRequest = new DicomTag(0x0040, 0x2005);

		///<summary>(0040,2006) VR=SH VM=1 Placer Order Number / Imaging Service Request (Retired) (RETIRED)</summary>
		public readonly static DicomTag PlacerOrderNumberImagingServiceRequestRETIRED = new DicomTag(0x0040, 0x2006);

		///<summary>(0040,2007) VR=SH VM=1 Filler Order Number / Imaging Service Request (Retired) (RETIRED)</summary>
		public readonly static DicomTag FillerOrderNumberImagingServiceRequestRETIRED = new DicomTag(0x0040, 0x2007);

		///<summary>(0040,2008) VR=PN VM=1 Order Entered By</summary>
		public readonly static DicomTag OrderEnteredBy = new DicomTag(0x0040, 0x2008);

		///<summary>(0040,2009) VR=SH VM=1 Order Enterer’s Location</summary>
		public readonly static DicomTag OrderEntererLocation = new DicomTag(0x0040, 0x2009);

		///<summary>(0040,2010) VR=SH VM=1 Order Callback Phone Number</summary>
		public readonly static DicomTag OrderCallbackPhoneNumber = new DicomTag(0x0040, 0x2010);

		///<summary>(0040,2016) VR=LO VM=1 Placer Order Number / Imaging Service Request</summary>
		public readonly static DicomTag PlacerOrderNumberImagingServiceRequest = new DicomTag(0x0040, 0x2016);

		///<summary>(0040,2017) VR=LO VM=1 Filler Order Number / Imaging Service Request</summary>
		public readonly static DicomTag FillerOrderNumberImagingServiceRequest = new DicomTag(0x0040, 0x2017);

		///<summary>(0040,2400) VR=LT VM=1 Imaging Service Request Comments</summary>
		public readonly static DicomTag ImagingServiceRequestComments = new DicomTag(0x0040, 0x2400);

		///<summary>(0040,3001) VR=LO VM=1 Confidentiality Constraint on Patient Data Description</summary>
		public readonly static DicomTag ConfidentialityConstraintOnPatientDataDescription = new DicomTag(0x0040, 0x3001);

		///<summary>(0040,4001) VR=CS VM=1 General Purpose Scheduled Procedure Step Status</summary>
		public readonly static DicomTag GeneralPurposeScheduledProcedureStepStatus = new DicomTag(0x0040, 0x4001);

		///<summary>(0040,4002) VR=CS VM=1 General Purpose Performed Procedure Step Status</summary>
		public readonly static DicomTag GeneralPurposePerformedProcedureStepStatus = new DicomTag(0x0040, 0x4002);

		///<summary>(0040,4003) VR=CS VM=1 General Purpose Scheduled Procedure Step Priority</summary>
		public readonly static DicomTag GeneralPurposeScheduledProcedureStepPriority = new DicomTag(0x0040, 0x4003);

		///<summary>(0040,4004) VR=SQ VM=1 Scheduled Processing Applications Code Sequence</summary>
		public readonly static DicomTag ScheduledProcessingApplicationsCodeSequence = new DicomTag(0x0040, 0x4004);

		///<summary>(0040,4005) VR=DT VM=1 Scheduled Procedure Step Start DateTime</summary>
		public readonly static DicomTag ScheduledProcedureStepStartDateTime = new DicomTag(0x0040, 0x4005);

		///<summary>(0040,4006) VR=CS VM=1 Multiple Copies Flag</summary>
		public readonly static DicomTag MultipleCopiesFlag = new DicomTag(0x0040, 0x4006);

		///<summary>(0040,4007) VR=SQ VM=1 Performed Processing Applications Code Sequence</summary>
		public readonly static DicomTag PerformedProcessingApplicationsCodeSequence = new DicomTag(0x0040, 0x4007);

		///<summary>(0040,4009) VR=SQ VM=1 Human Performer Code Sequence</summary>
		public readonly static DicomTag HumanPerformerCodeSequence = new DicomTag(0x0040, 0x4009);

		///<summary>(0040,4010) VR=DT VM=1 Scheduled Procedure Step Modification Date Time</summary>
		public readonly static DicomTag ScheduledProcedureStepModificationDateTime = new DicomTag(0x0040, 0x4010);

		///<summary>(0040,4011) VR=DT VM=1 Expected Completion Date Time</summary>
		public readonly static DicomTag ExpectedCompletionDateTime = new DicomTag(0x0040, 0x4011);

		///<summary>(0040,4015) VR=SQ VM=1 Resulting General Purpose Performed Procedure Steps Sequence</summary>
		public readonly static DicomTag ResultingGeneralPurposePerformedProcedureStepsSequence = new DicomTag(0x0040, 0x4015);

		///<summary>(0040,4016) VR=SQ VM=1 Referenced General Purpose Scheduled Procedure Step Sequence</summary>
		public readonly static DicomTag ReferencedGeneralPurposeScheduledProcedureStepSequence = new DicomTag(0x0040, 0x4016);

		///<summary>(0040,4018) VR=SQ VM=1 Scheduled Workitem Code Sequence</summary>
		public readonly static DicomTag ScheduledWorkitemCodeSequence = new DicomTag(0x0040, 0x4018);

		///<summary>(0040,4019) VR=SQ VM=1 Performed Workitem Code Sequence</summary>
		public readonly static DicomTag PerformedWorkitemCodeSequence = new DicomTag(0x0040, 0x4019);

		///<summary>(0040,4020) VR=CS VM=1 Input Availability Flag</summary>
		public readonly static DicomTag InputAvailabilityFlag = new DicomTag(0x0040, 0x4020);

		///<summary>(0040,4021) VR=SQ VM=1 Input Information Sequence</summary>
		public readonly static DicomTag InputInformationSequence = new DicomTag(0x0040, 0x4021);

		///<summary>(0040,4022) VR=SQ VM=1 Relevant Information Sequence</summary>
		public readonly static DicomTag RelevantInformationSequence = new DicomTag(0x0040, 0x4022);

		///<summary>(0040,4023) VR=UI VM=1 Referenced General Purpose Scheduled Procedure Step Transaction UID</summary>
		public readonly static DicomTag ReferencedGeneralPurposeScheduledProcedureStepTransactionUID = new DicomTag(0x0040, 0x4023);

		///<summary>(0040,4025) VR=SQ VM=1 Scheduled Station Name Code Sequence</summary>
		public readonly static DicomTag ScheduledStationNameCodeSequence = new DicomTag(0x0040, 0x4025);

		///<summary>(0040,4026) VR=SQ VM=1 Scheduled Station Class Code Sequence</summary>
		public readonly static DicomTag ScheduledStationClassCodeSequence = new DicomTag(0x0040, 0x4026);

		///<summary>(0040,4027) VR=SQ VM=1 Scheduled Station Geographic Location Code Sequence</summary>
		public readonly static DicomTag ScheduledStationGeographicLocationCodeSequence = new DicomTag(0x0040, 0x4027);

		///<summary>(0040,4028) VR=SQ VM=1 Performed Station Name Code Sequence</summary>
		public readonly static DicomTag PerformedStationNameCodeSequence = new DicomTag(0x0040, 0x4028);

		///<summary>(0040,4029) VR=SQ VM=1 Performed Station Class Code Sequence</summary>
		public readonly static DicomTag PerformedStationClassCodeSequence = new DicomTag(0x0040, 0x4029);

		///<summary>(0040,4030) VR=SQ VM=1 Performed Station Geographic Location Code Sequence</summary>
		public readonly static DicomTag PerformedStationGeographicLocationCodeSequence = new DicomTag(0x0040, 0x4030);

		///<summary>(0040,4031) VR=SQ VM=1 Requested Subsequent Workitem Code Sequence</summary>
		public readonly static DicomTag RequestedSubsequentWorkitemCodeSequence = new DicomTag(0x0040, 0x4031);

		///<summary>(0040,4032) VR=SQ VM=1 Non-DICOM Output Code Sequence</summary>
		public readonly static DicomTag NonDICOMOutputCodeSequence = new DicomTag(0x0040, 0x4032);

		///<summary>(0040,4033) VR=SQ VM=1 Output Information Sequence</summary>
		public readonly static DicomTag OutputInformationSequence = new DicomTag(0x0040, 0x4033);

		///<summary>(0040,4034) VR=SQ VM=1 Scheduled Human Performers Sequence</summary>
		public readonly static DicomTag ScheduledHumanPerformersSequence = new DicomTag(0x0040, 0x4034);

		///<summary>(0040,4035) VR=SQ VM=1 Actual Human Performers Sequence</summary>
		public readonly static DicomTag ActualHumanPerformersSequence = new DicomTag(0x0040, 0x4035);

		///<summary>(0040,4036) VR=LO VM=1 Human Performer’s Organization</summary>
		public readonly static DicomTag HumanPerformerOrganization = new DicomTag(0x0040, 0x4036);

		///<summary>(0040,4037) VR=PN VM=1 Human Performer’s Name</summary>
		public readonly static DicomTag HumanPerformerName = new DicomTag(0x0040, 0x4037);

		///<summary>(0040,4040) VR=CS VM=1 Raw Data Handling</summary>
		public readonly static DicomTag RawDataHandling = new DicomTag(0x0040, 0x4040);

		///<summary>(0040,4041) VR=CS VM=1 Input Readiness State</summary>
		public readonly static DicomTag InputReadinessState = new DicomTag(0x0040, 0x4041);

		///<summary>(0040,4050) VR=DT VM=1 Performed Procedure Step Start DateTime</summary>
		public readonly static DicomTag PerformedProcedureStepStartDateTime = new DicomTag(0x0040, 0x4050);

		///<summary>(0040,4051) VR=DT VM=1 Performed Procedure Step End DateTime</summary>
		public readonly static DicomTag PerformedProcedureStepEndDateTime = new DicomTag(0x0040, 0x4051);

		///<summary>(0040,4052) VR=DT VM=1 Procedure Step Cancellation DateTime</summary>
		public readonly static DicomTag ProcedureStepCancellationDateTime = new DicomTag(0x0040, 0x4052);

		///<summary>(0040,8302) VR=DS VM=1 Entrance Dose in mGy</summary>
		public readonly static DicomTag EntranceDoseInmGy = new DicomTag(0x0040, 0x8302);

		///<summary>(0040,9094) VR=SQ VM=1 Referenced Image Real World Value Mapping Sequence</summary>
		public readonly static DicomTag ReferencedImageRealWorldValueMappingSequence = new DicomTag(0x0040, 0x9094);

		///<summary>(0040,9096) VR=SQ VM=1 Real World Value Mapping Sequence</summary>
		public readonly static DicomTag RealWorldValueMappingSequence = new DicomTag(0x0040, 0x9096);

		///<summary>(0040,9098) VR=SQ VM=1 Pixel Value Mapping Code Sequence</summary>
		public readonly static DicomTag PixelValueMappingCodeSequence = new DicomTag(0x0040, 0x9098);

		///<summary>(0040,9210) VR=SH VM=1 LUT Label</summary>
		public readonly static DicomTag LUTLabel = new DicomTag(0x0040, 0x9210);

		///<summary>(0040,9211) VR=US/SS VM=1 Real World Value Last Value Mapped</summary>
		public readonly static DicomTag RealWorldValueLastValueMapped = new DicomTag(0x0040, 0x9211);

		///<summary>(0040,9212) VR=FD VM=1-n Real World Value LUT Data</summary>
		public readonly static DicomTag RealWorldValueLUTData = new DicomTag(0x0040, 0x9212);

		///<summary>(0040,9216) VR=US/SS VM=1 Real World Value First Value Mapped</summary>
		public readonly static DicomTag RealWorldValueFirstValueMapped = new DicomTag(0x0040, 0x9216);

		///<summary>(0040,9224) VR=FD VM=1 Real World Value Intercept</summary>
		public readonly static DicomTag RealWorldValueIntercept = new DicomTag(0x0040, 0x9224);

		///<summary>(0040,9225) VR=FD VM=1 Real World Value Slope</summary>
		public readonly static DicomTag RealWorldValueSlope = new DicomTag(0x0040, 0x9225);

		///<summary>(0040,a007) VR=CS VM=1 Findings Flag (Trial) (RETIRED)</summary>
		public readonly static DicomTag FindingsFlagTrialRETIRED = new DicomTag(0x0040, 0xa007);

		///<summary>(0040,a010) VR=CS VM=1 Relationship Type</summary>
		public readonly static DicomTag RelationshipType = new DicomTag(0x0040, 0xa010);

		///<summary>(0040,a020) VR=SQ VM=1 Findings Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag FindingsSequenceTrialRETIRED = new DicomTag(0x0040, 0xa020);

		///<summary>(0040,a021) VR=UI VM=1 Findings Group UID (Trial) (RETIRED)</summary>
		public readonly static DicomTag FindingsGroupUIDTrialRETIRED = new DicomTag(0x0040, 0xa021);

		///<summary>(0040,a022) VR=UI VM=1 Referenced Findings Group UID (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReferencedFindingsGroupUIDTrialRETIRED = new DicomTag(0x0040, 0xa022);

		///<summary>(0040,a023) VR=DA VM=1 Findings Group Recording Date (Trial) (RETIRED)</summary>
		public readonly static DicomTag FindingsGroupRecordingDateTrialRETIRED = new DicomTag(0x0040, 0xa023);

		///<summary>(0040,a024) VR=TM VM=1 Findings Group Recording Time (Trial) (RETIRED)</summary>
		public readonly static DicomTag FindingsGroupRecordingTimeTrialRETIRED = new DicomTag(0x0040, 0xa024);

		///<summary>(0040,a026) VR=SQ VM=1 Findings Source Category Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag FindingsSourceCategoryCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa026);

		///<summary>(0040,a027) VR=LO VM=1 Verifying Organization</summary>
		public readonly static DicomTag VerifyingOrganization = new DicomTag(0x0040, 0xa027);

		///<summary>(0040,a028) VR=SQ VM=1 Documenting Organization Identifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag DocumentingOrganizationIdentifierCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa028);

		///<summary>(0040,a030) VR=DT VM=1 Verification Date Time</summary>
		public readonly static DicomTag VerificationDateTime = new DicomTag(0x0040, 0xa030);

		///<summary>(0040,a032) VR=DT VM=1 Observation Date Time</summary>
		public readonly static DicomTag ObservationDateTime = new DicomTag(0x0040, 0xa032);

		///<summary>(0040,a040) VR=CS VM=1 Value Type</summary>
		public readonly static DicomTag ValueType = new DicomTag(0x0040, 0xa040);

		///<summary>(0040,a043) VR=SQ VM=1 Concept Name Code Sequence</summary>
		public readonly static DicomTag ConceptNameCodeSequence = new DicomTag(0x0040, 0xa043);

		///<summary>(0040,a047) VR=LO VM=1 Measurement Precision Description (Trial) (RETIRED)</summary>
		public readonly static DicomTag MeasurementPrecisionDescriptionTrialRETIRED = new DicomTag(0x0040, 0xa047);

		///<summary>(0040,a050) VR=CS VM=1 Continuity Of Content</summary>
		public readonly static DicomTag ContinuityOfContent = new DicomTag(0x0040, 0xa050);

		///<summary>(0040,a057) VR=CS VM=1-n Urgency or Priority Alerts (Trial) (RETIRED)</summary>
		public readonly static DicomTag UrgencyOrPriorityAlertsTrialRETIRED = new DicomTag(0x0040, 0xa057);

		///<summary>(0040,a060) VR=LO VM=1 Sequencing Indicator (Trial) (RETIRED)</summary>
		public readonly static DicomTag SequencingIndicatorTrialRETIRED = new DicomTag(0x0040, 0xa060);

		///<summary>(0040,a066) VR=SQ VM=1 Document Identifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag DocumentIdentifierCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa066);

		///<summary>(0040,a067) VR=PN VM=1 Document Author (Trial) (RETIRED)</summary>
		public readonly static DicomTag DocumentAuthorTrialRETIRED = new DicomTag(0x0040, 0xa067);

		///<summary>(0040,a068) VR=SQ VM=1 Document Author Identifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag DocumentAuthorIdentifierCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa068);

		///<summary>(0040,a070) VR=SQ VM=1 Identifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag IdentifierCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa070);

		///<summary>(0040,a073) VR=SQ VM=1 Verifying Observer Sequence</summary>
		public readonly static DicomTag VerifyingObserverSequence = new DicomTag(0x0040, 0xa073);

		///<summary>(0040,a074) VR=OB VM=1 Object Binary Identifier (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObjectBinaryIdentifierTrialRETIRED = new DicomTag(0x0040, 0xa074);

		///<summary>(0040,a075) VR=PN VM=1 Verifying Observer Name</summary>
		public readonly static DicomTag VerifyingObserverName = new DicomTag(0x0040, 0xa075);

		///<summary>(0040,a076) VR=SQ VM=1 Documenting Observer Identifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag DocumentingObserverIdentifierCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa076);

		///<summary>(0040,a078) VR=SQ VM=1 Author Observer Sequence</summary>
		public readonly static DicomTag AuthorObserverSequence = new DicomTag(0x0040, 0xa078);

		///<summary>(0040,a07a) VR=SQ VM=1 Participant Sequence</summary>
		public readonly static DicomTag ParticipantSequence = new DicomTag(0x0040, 0xa07a);

		///<summary>(0040,a07c) VR=SQ VM=1 Custodial Organization Sequence</summary>
		public readonly static DicomTag CustodialOrganizationSequence = new DicomTag(0x0040, 0xa07c);

		///<summary>(0040,a080) VR=CS VM=1 Participation Type</summary>
		public readonly static DicomTag ParticipationType = new DicomTag(0x0040, 0xa080);

		///<summary>(0040,a082) VR=DT VM=1 Participation DateTime</summary>
		public readonly static DicomTag ParticipationDateTime = new DicomTag(0x0040, 0xa082);

		///<summary>(0040,a084) VR=CS VM=1 Observer Type</summary>
		public readonly static DicomTag ObserverType = new DicomTag(0x0040, 0xa084);

		///<summary>(0040,a085) VR=SQ VM=1 Procedure Identifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag ProcedureIdentifierCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa085);

		///<summary>(0040,a088) VR=SQ VM=1 Verifying Observer Identification Code Sequence</summary>
		public readonly static DicomTag VerifyingObserverIdentificationCodeSequence = new DicomTag(0x0040, 0xa088);

		///<summary>(0040,a089) VR=OB VM=1 Object Directory Binary Identifier (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObjectDirectoryBinaryIdentifierTrialRETIRED = new DicomTag(0x0040, 0xa089);

		///<summary>(0040,a090) VR=SQ VM=1 Equivalent CDA Document Sequence (RETIRED)</summary>
		public readonly static DicomTag EquivalentCDADocumentSequenceRETIRED = new DicomTag(0x0040, 0xa090);

		///<summary>(0040,a0b0) VR=US VM=2-2n Referenced Waveform Channels</summary>
		public readonly static DicomTag ReferencedWaveformChannels = new DicomTag(0x0040, 0xa0b0);

		///<summary>(0040,a110) VR=DA VM=1 Date of Document or Verbal Transaction (Trial) (RETIRED)</summary>
		public readonly static DicomTag DateOfDocumentOrVerbalTransactionTrialRETIRED = new DicomTag(0x0040, 0xa110);

		///<summary>(0040,a112) VR=TM VM=1 Time of Document Creation or Verbal Transaction (Trial) (RETIRED)</summary>
		public readonly static DicomTag TimeOfDocumentCreationOrVerbalTransactionTrialRETIRED = new DicomTag(0x0040, 0xa112);

		///<summary>(0040,a120) VR=DT VM=1 DateTime</summary>
		public readonly static DicomTag DateTime = new DicomTag(0x0040, 0xa120);

		///<summary>(0040,a121) VR=DA VM=1 Date</summary>
		public readonly static DicomTag Date = new DicomTag(0x0040, 0xa121);

		///<summary>(0040,a122) VR=TM VM=1 Time</summary>
		public readonly static DicomTag Time = new DicomTag(0x0040, 0xa122);

		///<summary>(0040,a123) VR=PN VM=1 Person Name</summary>
		public readonly static DicomTag PersonName = new DicomTag(0x0040, 0xa123);

		///<summary>(0040,a124) VR=UI VM=1 UID</summary>
		public readonly static DicomTag UID = new DicomTag(0x0040, 0xa124);

		///<summary>(0040,a125) VR=CS VM=2 Report Status ID (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReportStatusIDTrialRETIRED = new DicomTag(0x0040, 0xa125);

		///<summary>(0040,a130) VR=CS VM=1 Temporal Range Type</summary>
		public readonly static DicomTag TemporalRangeType = new DicomTag(0x0040, 0xa130);

		///<summary>(0040,a132) VR=UL VM=1-n Referenced Sample Positions</summary>
		public readonly static DicomTag ReferencedSamplePositions = new DicomTag(0x0040, 0xa132);

		///<summary>(0040,a136) VR=US VM=1-n Referenced Frame Numbers</summary>
		public readonly static DicomTag ReferencedFrameNumbers = new DicomTag(0x0040, 0xa136);

		///<summary>(0040,a138) VR=DS VM=1-n Referenced Time Offsets</summary>
		public readonly static DicomTag ReferencedTimeOffsets = new DicomTag(0x0040, 0xa138);

		///<summary>(0040,a13a) VR=DT VM=1-n Referenced DateTime</summary>
		public readonly static DicomTag ReferencedDateTime = new DicomTag(0x0040, 0xa13a);

		///<summary>(0040,a160) VR=UT VM=1 Text Value</summary>
		public readonly static DicomTag TextValue = new DicomTag(0x0040, 0xa160);

		///<summary>(0040,a167) VR=SQ VM=1 Observation Category Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationCategoryCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa167);

		///<summary>(0040,a168) VR=SQ VM=1 Concept Code Sequence</summary>
		public readonly static DicomTag ConceptCodeSequence = new DicomTag(0x0040, 0xa168);

		///<summary>(0040,a16a) VR=ST VM=1 Bibliographic Citation (Trial) (RETIRED)</summary>
		public readonly static DicomTag BibliographicCitationTrialRETIRED = new DicomTag(0x0040, 0xa16a);

		///<summary>(0040,a170) VR=SQ VM=1 Purpose of Reference Code Sequence</summary>
		public readonly static DicomTag PurposeOfReferenceCodeSequence = new DicomTag(0x0040, 0xa170);

		///<summary>(0040,a171) VR=UI VM=1 Observation UID (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationUIDTrialRETIRED = new DicomTag(0x0040, 0xa171);

		///<summary>(0040,a172) VR=UI VM=1 Referenced Observation UID (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReferencedObservationUIDTrialRETIRED = new DicomTag(0x0040, 0xa172);

		///<summary>(0040,a173) VR=CS VM=1 Referenced Observation Class (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReferencedObservationClassTrialRETIRED = new DicomTag(0x0040, 0xa173);

		///<summary>(0040,a174) VR=CS VM=1 Referenced Object Observation Class (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReferencedObjectObservationClassTrialRETIRED = new DicomTag(0x0040, 0xa174);

		///<summary>(0040,a180) VR=US VM=1 Annotation Group Number</summary>
		public readonly static DicomTag AnnotationGroupNumber = new DicomTag(0x0040, 0xa180);

		///<summary>(0040,a192) VR=DA VM=1 Observation Date (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationDateTrialRETIRED = new DicomTag(0x0040, 0xa192);

		///<summary>(0040,a193) VR=TM VM=1 Observation Time (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationTimeTrialRETIRED = new DicomTag(0x0040, 0xa193);

		///<summary>(0040,a194) VR=CS VM=1 Measurement Automation (Trial) (RETIRED)</summary>
		public readonly static DicomTag MeasurementAutomationTrialRETIRED = new DicomTag(0x0040, 0xa194);

		///<summary>(0040,a195) VR=SQ VM=1 Modifier Code Sequence</summary>
		public readonly static DicomTag ModifierCodeSequence = new DicomTag(0x0040, 0xa195);

		///<summary>(0040,a224) VR=ST VM=1 Identification Description (Trial) (RETIRED)</summary>
		public readonly static DicomTag IdentificationDescriptionTrialRETIRED = new DicomTag(0x0040, 0xa224);

		///<summary>(0040,a290) VR=CS VM=1 Coordinates Set Geometric Type (Trial) (RETIRED)</summary>
		public readonly static DicomTag CoordinatesSetGeometricTypeTrialRETIRED = new DicomTag(0x0040, 0xa290);

		///<summary>(0040,a296) VR=SQ VM=1 Algorithm Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag AlgorithmCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa296);

		///<summary>(0040,a297) VR=ST VM=1 Algorithm Description (Trial) (RETIRED)</summary>
		public readonly static DicomTag AlgorithmDescriptionTrialRETIRED = new DicomTag(0x0040, 0xa297);

		///<summary>(0040,a29a) VR=SL VM=2-2n Pixel Coordinates Set (Trial) (RETIRED)</summary>
		public readonly static DicomTag PixelCoordinatesSetTrialRETIRED = new DicomTag(0x0040, 0xa29a);

		///<summary>(0040,a300) VR=SQ VM=1 Measured Value Sequence</summary>
		public readonly static DicomTag MeasuredValueSequence = new DicomTag(0x0040, 0xa300);

		///<summary>(0040,a301) VR=SQ VM=1 Numeric Value Qualifier Code Sequence</summary>
		public readonly static DicomTag NumericValueQualifierCodeSequence = new DicomTag(0x0040, 0xa301);

		///<summary>(0040,a307) VR=PN VM=1 Current Observer (Trial) (RETIRED)</summary>
		public readonly static DicomTag CurrentObserverTrialRETIRED = new DicomTag(0x0040, 0xa307);

		///<summary>(0040,a30a) VR=DS VM=1-n Numeric Value</summary>
		public readonly static DicomTag NumericValue = new DicomTag(0x0040, 0xa30a);

		///<summary>(0040,a313) VR=SQ VM=1 Referenced Accession Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReferencedAccessionSequenceTrialRETIRED = new DicomTag(0x0040, 0xa313);

		///<summary>(0040,a33a) VR=ST VM=1 Report Status Comment (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReportStatusCommentTrialRETIRED = new DicomTag(0x0040, 0xa33a);

		///<summary>(0040,a340) VR=SQ VM=1 Procedure Context Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag ProcedureContextSequenceTrialRETIRED = new DicomTag(0x0040, 0xa340);

		///<summary>(0040,a352) VR=PN VM=1 Verbal Source (Trial) (RETIRED)</summary>
		public readonly static DicomTag VerbalSourceTrialRETIRED = new DicomTag(0x0040, 0xa352);

		///<summary>(0040,a353) VR=ST VM=1 Address (Trial) (RETIRED)</summary>
		public readonly static DicomTag AddressTrialRETIRED = new DicomTag(0x0040, 0xa353);

		///<summary>(0040,a354) VR=LO VM=1 Telephone Number (Trial) (RETIRED)</summary>
		public readonly static DicomTag TelephoneNumberTrialRETIRED = new DicomTag(0x0040, 0xa354);

		///<summary>(0040,a358) VR=SQ VM=1 Verbal Source Identifier Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag VerbalSourceIdentifierCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa358);

		///<summary>(0040,a360) VR=SQ VM=1 Predecessor Documents Sequence</summary>
		public readonly static DicomTag PredecessorDocumentsSequence = new DicomTag(0x0040, 0xa360);

		///<summary>(0040,a370) VR=SQ VM=1 Referenced Request Sequence</summary>
		public readonly static DicomTag ReferencedRequestSequence = new DicomTag(0x0040, 0xa370);

		///<summary>(0040,a372) VR=SQ VM=1 Performed Procedure Code Sequence</summary>
		public readonly static DicomTag PerformedProcedureCodeSequence = new DicomTag(0x0040, 0xa372);

		///<summary>(0040,a375) VR=SQ VM=1 Current Requested Procedure Evidence Sequence</summary>
		public readonly static DicomTag CurrentRequestedProcedureEvidenceSequence = new DicomTag(0x0040, 0xa375);

		///<summary>(0040,a380) VR=SQ VM=1 Report Detail Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReportDetailSequenceTrialRETIRED = new DicomTag(0x0040, 0xa380);

		///<summary>(0040,a385) VR=SQ VM=1 Pertinent Other Evidence Sequence</summary>
		public readonly static DicomTag PertinentOtherEvidenceSequence = new DicomTag(0x0040, 0xa385);

		///<summary>(0040,a390) VR=SQ VM=1 HL7 Structured Document Reference Sequence</summary>
		public readonly static DicomTag HL7StructuredDocumentReferenceSequence = new DicomTag(0x0040, 0xa390);

		///<summary>(0040,a402) VR=UI VM=1 Observation Subject UID (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationSubjectUIDTrialRETIRED = new DicomTag(0x0040, 0xa402);

		///<summary>(0040,a403) VR=CS VM=1 Observation Subject Class (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationSubjectClassTrialRETIRED = new DicomTag(0x0040, 0xa403);

		///<summary>(0040,a404) VR=SQ VM=1 Observation Subject Type Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationSubjectTypeCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa404);

		///<summary>(0040,a491) VR=CS VM=1 Completion Flag</summary>
		public readonly static DicomTag CompletionFlag = new DicomTag(0x0040, 0xa491);

		///<summary>(0040,a492) VR=LO VM=1 Completion Flag Description</summary>
		public readonly static DicomTag CompletionFlagDescription = new DicomTag(0x0040, 0xa492);

		///<summary>(0040,a493) VR=CS VM=1 Verification Flag</summary>
		public readonly static DicomTag VerificationFlag = new DicomTag(0x0040, 0xa493);

		///<summary>(0040,a494) VR=CS VM=1 Archive Requested</summary>
		public readonly static DicomTag ArchiveRequested = new DicomTag(0x0040, 0xa494);

		///<summary>(0040,a496) VR=CS VM=1 Preliminary Flag</summary>
		public readonly static DicomTag PreliminaryFlag = new DicomTag(0x0040, 0xa496);

		///<summary>(0040,a504) VR=SQ VM=1 Content Template Sequence</summary>
		public readonly static DicomTag ContentTemplateSequence = new DicomTag(0x0040, 0xa504);

		///<summary>(0040,a525) VR=SQ VM=1 Identical Documents Sequence</summary>
		public readonly static DicomTag IdenticalDocumentsSequence = new DicomTag(0x0040, 0xa525);

		///<summary>(0040,a600) VR=CS VM=1 Observation Subject Context Flag (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObservationSubjectContextFlagTrialRETIRED = new DicomTag(0x0040, 0xa600);

		///<summary>(0040,a601) VR=CS VM=1 Observer Context Flag (Trial) (RETIRED)</summary>
		public readonly static DicomTag ObserverContextFlagTrialRETIRED = new DicomTag(0x0040, 0xa601);

		///<summary>(0040,a603) VR=CS VM=1 Procedure Context Flag (Trial) (RETIRED)</summary>
		public readonly static DicomTag ProcedureContextFlagTrialRETIRED = new DicomTag(0x0040, 0xa603);

		///<summary>(0040,a730) VR=SQ VM=1 Content Sequence</summary>
		public readonly static DicomTag ContentSequence = new DicomTag(0x0040, 0xa730);

		///<summary>(0040,a731) VR=SQ VM=1 Relationship Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag RelationshipSequenceTrialRETIRED = new DicomTag(0x0040, 0xa731);

		///<summary>(0040,a732) VR=SQ VM=1 Relationship Type Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag RelationshipTypeCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa732);

		///<summary>(0040,a744) VR=SQ VM=1 Language Code Sequence (Trial) (RETIRED)</summary>
		public readonly static DicomTag LanguageCodeSequenceTrialRETIRED = new DicomTag(0x0040, 0xa744);

		///<summary>(0040,a992) VR=ST VM=1 Uniform Resource Locator (Trial) (RETIRED)</summary>
		public readonly static DicomTag UniformResourceLocatorTrialRETIRED = new DicomTag(0x0040, 0xa992);

		///<summary>(0040,b020) VR=SQ VM=1 Waveform Annotation Sequence</summary>
		public readonly static DicomTag WaveformAnnotationSequence = new DicomTag(0x0040, 0xb020);

		///<summary>(0040,db00) VR=CS VM=1 Template Identifier</summary>
		public readonly static DicomTag TemplateIdentifier = new DicomTag(0x0040, 0xdb00);

		///<summary>(0040,db06) VR=DT VM=1 Template Version (RETIRED)</summary>
		public readonly static DicomTag TemplateVersionRETIRED = new DicomTag(0x0040, 0xdb06);

		///<summary>(0040,db07) VR=DT VM=1 Template Local Version (RETIRED)</summary>
		public readonly static DicomTag TemplateLocalVersionRETIRED = new DicomTag(0x0040, 0xdb07);

		///<summary>(0040,db0b) VR=CS VM=1 Template Extension Flag (RETIRED)</summary>
		public readonly static DicomTag TemplateExtensionFlagRETIRED = new DicomTag(0x0040, 0xdb0b);

		///<summary>(0040,db0c) VR=UI VM=1 Template Extension Organization UID (RETIRED)</summary>
		public readonly static DicomTag TemplateExtensionOrganizationUIDRETIRED = new DicomTag(0x0040, 0xdb0c);

		///<summary>(0040,db0d) VR=UI VM=1 Template Extension Creator UID (RETIRED)</summary>
		public readonly static DicomTag TemplateExtensionCreatorUIDRETIRED = new DicomTag(0x0040, 0xdb0d);

		///<summary>(0040,db73) VR=UL VM=1-n Referenced Content Item Identifier</summary>
		public readonly static DicomTag ReferencedContentItemIdentifier = new DicomTag(0x0040, 0xdb73);

		///<summary>(0040,e001) VR=ST VM=1 HL7 Instance Identifier</summary>
		public readonly static DicomTag HL7InstanceIdentifier = new DicomTag(0x0040, 0xe001);

		///<summary>(0040,e004) VR=DT VM=1 HL7 Document Effective Time</summary>
		public readonly static DicomTag HL7DocumentEffectiveTime = new DicomTag(0x0040, 0xe004);

		///<summary>(0040,e006) VR=SQ VM=1 HL7 Document Type Code Sequence</summary>
		public readonly static DicomTag HL7DocumentTypeCodeSequence = new DicomTag(0x0040, 0xe006);

		///<summary>(0040,e008) VR=SQ VM=1 Document Class Code Sequence</summary>
		public readonly static DicomTag DocumentClassCodeSequence = new DicomTag(0x0040, 0xe008);

		///<summary>(0040,e010) VR=UT VM=1 Retrieve URI</summary>
		public readonly static DicomTag RetrieveURI = new DicomTag(0x0040, 0xe010);

		///<summary>(0040,e011) VR=UI VM=1 Retrieve Location UID</summary>
		public readonly static DicomTag RetrieveLocationUID = new DicomTag(0x0040, 0xe011);

		///<summary>(0040,e020) VR=CS VM=1 Type of Instances</summary>
		public readonly static DicomTag TypeOfInstances = new DicomTag(0x0040, 0xe020);

		///<summary>(0040,e021) VR=SQ VM=1 DICOM Retrieval Sequence</summary>
		public readonly static DicomTag DICOMRetrievalSequence = new DicomTag(0x0040, 0xe021);

		///<summary>(0040,e022) VR=SQ VM=1 DICOM Media Retrieval Sequence</summary>
		public readonly static DicomTag DICOMMediaRetrievalSequence = new DicomTag(0x0040, 0xe022);

		///<summary>(0040,e023) VR=SQ VM=1 WADO Retrieval Sequence</summary>
		public readonly static DicomTag WADORetrievalSequence = new DicomTag(0x0040, 0xe023);

		///<summary>(0040,e024) VR=SQ VM=1 XDS Retrieval Sequence</summary>
		public readonly static DicomTag XDSRetrievalSequence = new DicomTag(0x0040, 0xe024);

		///<summary>(0040,e030) VR=UI VM=1 Repository Unique ID</summary>
		public readonly static DicomTag RepositoryUniqueID = new DicomTag(0x0040, 0xe030);

		///<summary>(0040,e031) VR=UI VM=1 Home Community ID</summary>
		public readonly static DicomTag HomeCommunityID = new DicomTag(0x0040, 0xe031);

		///<summary>(0042,0010) VR=ST VM=1 Document Title</summary>
		public readonly static DicomTag DocumentTitle = new DicomTag(0x0042, 0x0010);

		///<summary>(0042,0011) VR=OB VM=1 Encapsulated Document</summary>
		public readonly static DicomTag EncapsulatedDocument = new DicomTag(0x0042, 0x0011);

		///<summary>(0042,0012) VR=LO VM=1 MIME Type of Encapsulated Document</summary>
		public readonly static DicomTag MIMETypeOfEncapsulatedDocument = new DicomTag(0x0042, 0x0012);

		///<summary>(0042,0013) VR=SQ VM=1 Source Instance Sequence</summary>
		public readonly static DicomTag SourceInstanceSequence = new DicomTag(0x0042, 0x0013);

		///<summary>(0042,0014) VR=LO VM=1-n List of MIME Types</summary>
		public readonly static DicomTag ListOfMIMETypes = new DicomTag(0x0042, 0x0014);

		///<summary>(0044,0001) VR=ST VM=1 Product Package Identifier</summary>
		public readonly static DicomTag ProductPackageIdentifier = new DicomTag(0x0044, 0x0001);

		///<summary>(0044,0002) VR=CS VM=1 Substance Administration Approval</summary>
		public readonly static DicomTag SubstanceAdministrationApproval = new DicomTag(0x0044, 0x0002);

		///<summary>(0044,0003) VR=LT VM=1 Approval Status Further Description</summary>
		public readonly static DicomTag ApprovalStatusFurtherDescription = new DicomTag(0x0044, 0x0003);

		///<summary>(0044,0004) VR=DT VM=1 Approval Status DateTime</summary>
		public readonly static DicomTag ApprovalStatusDateTime = new DicomTag(0x0044, 0x0004);

		///<summary>(0044,0007) VR=SQ VM=1 Product Type Code Sequence</summary>
		public readonly static DicomTag ProductTypeCodeSequence = new DicomTag(0x0044, 0x0007);

		///<summary>(0044,0008) VR=LO VM=1-n Product Name</summary>
		public readonly static DicomTag ProductName = new DicomTag(0x0044, 0x0008);

		///<summary>(0044,0009) VR=LT VM=1 Product Description</summary>
		public readonly static DicomTag ProductDescription = new DicomTag(0x0044, 0x0009);

		///<summary>(0044,000a) VR=LO VM=1 Product Lot Identifier</summary>
		public readonly static DicomTag ProductLotIdentifier = new DicomTag(0x0044, 0x000a);

		///<summary>(0044,000b) VR=DT VM=1 Product Expiration DateTime</summary>
		public readonly static DicomTag ProductExpirationDateTime = new DicomTag(0x0044, 0x000b);

		///<summary>(0044,0010) VR=DT VM=1 Substance Administration DateTime</summary>
		public readonly static DicomTag SubstanceAdministrationDateTime = new DicomTag(0x0044, 0x0010);

		///<summary>(0044,0011) VR=LO VM=1 Substance Administration Notes</summary>
		public readonly static DicomTag SubstanceAdministrationNotes = new DicomTag(0x0044, 0x0011);

		///<summary>(0044,0012) VR=LO VM=1 Substance Administration Device ID</summary>
		public readonly static DicomTag SubstanceAdministrationDeviceID = new DicomTag(0x0044, 0x0012);

		///<summary>(0044,0013) VR=SQ VM=1 Product Parameter Sequence</summary>
		public readonly static DicomTag ProductParameterSequence = new DicomTag(0x0044, 0x0013);

		///<summary>(0044,0019) VR=SQ VM=1 Substance Administration Parameter Sequence</summary>
		public readonly static DicomTag SubstanceAdministrationParameterSequence = new DicomTag(0x0044, 0x0019);

		///<summary>(0046,0012) VR=LO VM=1 Lens Description</summary>
		public readonly static DicomTag LensDescription = new DicomTag(0x0046, 0x0012);

		///<summary>(0046,0014) VR=SQ VM=1 Right Lens Sequence</summary>
		public readonly static DicomTag RightLensSequence = new DicomTag(0x0046, 0x0014);

		///<summary>(0046,0015) VR=SQ VM=1 Left Lens Sequence</summary>
		public readonly static DicomTag LeftLensSequence = new DicomTag(0x0046, 0x0015);

		///<summary>(0046,0016) VR=SQ VM=1 Unspecified Laterality Lens Sequence</summary>
		public readonly static DicomTag UnspecifiedLateralityLensSequence = new DicomTag(0x0046, 0x0016);

		///<summary>(0046,0018) VR=SQ VM=1 Cylinder Sequence</summary>
		public readonly static DicomTag CylinderSequence = new DicomTag(0x0046, 0x0018);

		///<summary>(0046,0028) VR=SQ VM=1 Prism Sequence</summary>
		public readonly static DicomTag PrismSequence = new DicomTag(0x0046, 0x0028);

		///<summary>(0046,0030) VR=FD VM=1 Horizontal Prism Power</summary>
		public readonly static DicomTag HorizontalPrismPower = new DicomTag(0x0046, 0x0030);

		///<summary>(0046,0032) VR=CS VM=1 Horizontal Prism Base</summary>
		public readonly static DicomTag HorizontalPrismBase = new DicomTag(0x0046, 0x0032);

		///<summary>(0046,0034) VR=FD VM=1 Vertical Prism Power</summary>
		public readonly static DicomTag VerticalPrismPower = new DicomTag(0x0046, 0x0034);

		///<summary>(0046,0036) VR=CS VM=1 Vertical Prism Base</summary>
		public readonly static DicomTag VerticalPrismBase = new DicomTag(0x0046, 0x0036);

		///<summary>(0046,0038) VR=CS VM=1 Lens Segment Type</summary>
		public readonly static DicomTag LensSegmentType = new DicomTag(0x0046, 0x0038);

		///<summary>(0046,0040) VR=FD VM=1 Optical Transmittance</summary>
		public readonly static DicomTag OpticalTransmittance = new DicomTag(0x0046, 0x0040);

		///<summary>(0046,0042) VR=FD VM=1 Channel Width</summary>
		public readonly static DicomTag ChannelWidth = new DicomTag(0x0046, 0x0042);

		///<summary>(0046,0044) VR=FD VM=1 Pupil Size</summary>
		public readonly static DicomTag PupilSize = new DicomTag(0x0046, 0x0044);

		///<summary>(0046,0046) VR=FD VM=1 Corneal Size</summary>
		public readonly static DicomTag CornealSize = new DicomTag(0x0046, 0x0046);

		///<summary>(0046,0050) VR=SQ VM=1 Autorefraction Right Eye Sequence</summary>
		public readonly static DicomTag AutorefractionRightEyeSequence = new DicomTag(0x0046, 0x0050);

		///<summary>(0046,0052) VR=SQ VM=1 Autorefraction Left Eye Sequence</summary>
		public readonly static DicomTag AutorefractionLeftEyeSequence = new DicomTag(0x0046, 0x0052);

		///<summary>(0046,0060) VR=FD VM=1 Distance Pupillary Distance</summary>
		public readonly static DicomTag DistancePupillaryDistance = new DicomTag(0x0046, 0x0060);

		///<summary>(0046,0062) VR=FD VM=1 Near Pupillary Distance</summary>
		public readonly static DicomTag NearPupillaryDistance = new DicomTag(0x0046, 0x0062);

		///<summary>(0046,0063) VR=FD VM=1 Intermediate Pupillary Distance</summary>
		public readonly static DicomTag IntermediatePupillaryDistance = new DicomTag(0x0046, 0x0063);

		///<summary>(0046,0064) VR=FD VM=1 Other Pupillary Distance</summary>
		public readonly static DicomTag OtherPupillaryDistance = new DicomTag(0x0046, 0x0064);

		///<summary>(0046,0070) VR=SQ VM=1 Keratometry Right Eye Sequence</summary>
		public readonly static DicomTag KeratometryRightEyeSequence = new DicomTag(0x0046, 0x0070);

		///<summary>(0046,0071) VR=SQ VM=1 Keratometry Left Eye Sequence</summary>
		public readonly static DicomTag KeratometryLeftEyeSequence = new DicomTag(0x0046, 0x0071);

		///<summary>(0046,0074) VR=SQ VM=1 Steep Keratometric Axis Sequence</summary>
		public readonly static DicomTag SteepKeratometricAxisSequence = new DicomTag(0x0046, 0x0074);

		///<summary>(0046,0075) VR=FD VM=1 Radius of Curvature</summary>
		public readonly static DicomTag RadiusOfCurvature = new DicomTag(0x0046, 0x0075);

		///<summary>(0046,0076) VR=FD VM=1 Keratometric Power</summary>
		public readonly static DicomTag KeratometricPower = new DicomTag(0x0046, 0x0076);

		///<summary>(0046,0077) VR=FD VM=1 Keratometric Axis</summary>
		public readonly static DicomTag KeratometricAxis = new DicomTag(0x0046, 0x0077);

		///<summary>(0046,0080) VR=SQ VM=1 Flat Keratometric Axis Sequence</summary>
		public readonly static DicomTag FlatKeratometricAxisSequence = new DicomTag(0x0046, 0x0080);

		///<summary>(0046,0092) VR=CS VM=1 Background Color</summary>
		public readonly static DicomTag BackgroundColor = new DicomTag(0x0046, 0x0092);

		///<summary>(0046,0094) VR=CS VM=1 Optotype</summary>
		public readonly static DicomTag Optotype = new DicomTag(0x0046, 0x0094);

		///<summary>(0046,0095) VR=CS VM=1 Optotype Presentation</summary>
		public readonly static DicomTag OptotypePresentation = new DicomTag(0x0046, 0x0095);

		///<summary>(0046,0097) VR=SQ VM=1 Subjective Refraction Right Eye Sequence</summary>
		public readonly static DicomTag SubjectiveRefractionRightEyeSequence = new DicomTag(0x0046, 0x0097);

		///<summary>(0046,0098) VR=SQ VM=1 Subjective Refraction Left Eye Sequence</summary>
		public readonly static DicomTag SubjectiveRefractionLeftEyeSequence = new DicomTag(0x0046, 0x0098);

		///<summary>(0046,0100) VR=SQ VM=1 Add Near Sequence</summary>
		public readonly static DicomTag AddNearSequence = new DicomTag(0x0046, 0x0100);

		///<summary>(0046,0101) VR=SQ VM=1 Add Intermediate Sequence</summary>
		public readonly static DicomTag AddIntermediateSequence = new DicomTag(0x0046, 0x0101);

		///<summary>(0046,0102) VR=SQ VM=1 Add Other Sequence</summary>
		public readonly static DicomTag AddOtherSequence = new DicomTag(0x0046, 0x0102);

		///<summary>(0046,0104) VR=FD VM=1 Add Power</summary>
		public readonly static DicomTag AddPower = new DicomTag(0x0046, 0x0104);

		///<summary>(0046,0106) VR=FD VM=1 Viewing Distance</summary>
		public readonly static DicomTag ViewingDistance = new DicomTag(0x0046, 0x0106);

		///<summary>(0046,0121) VR=SQ VM=1 Visual Acuity Type Code Sequence</summary>
		public readonly static DicomTag VisualAcuityTypeCodeSequence = new DicomTag(0x0046, 0x0121);

		///<summary>(0046,0122) VR=SQ VM=1 Visual Acuity Right Eye Sequence</summary>
		public readonly static DicomTag VisualAcuityRightEyeSequence = new DicomTag(0x0046, 0x0122);

		///<summary>(0046,0123) VR=SQ VM=1 Visual Acuity Left Eye Sequence</summary>
		public readonly static DicomTag VisualAcuityLeftEyeSequence = new DicomTag(0x0046, 0x0123);

		///<summary>(0046,0124) VR=SQ VM=1 Visual Acuity Both Eyes Open Sequence</summary>
		public readonly static DicomTag VisualAcuityBothEyesOpenSequence = new DicomTag(0x0046, 0x0124);

		///<summary>(0046,0125) VR=CS VM=1 Viewing Distance Type</summary>
		public readonly static DicomTag ViewingDistanceType = new DicomTag(0x0046, 0x0125);

		///<summary>(0046,0135) VR=SS VM=2 Visual Acuity Modifiers</summary>
		public readonly static DicomTag VisualAcuityModifiers = new DicomTag(0x0046, 0x0135);

		///<summary>(0046,0137) VR=FD VM=1 Decimal Visual Acuity</summary>
		public readonly static DicomTag DecimalVisualAcuity = new DicomTag(0x0046, 0x0137);

		///<summary>(0046,0139) VR=LO VM=1 Optotype Detailed Definition</summary>
		public readonly static DicomTag OptotypeDetailedDefinition = new DicomTag(0x0046, 0x0139);

		///<summary>(0046,0145) VR=SQ VM=1 Referenced Refractive Measurements Sequence</summary>
		public readonly static DicomTag ReferencedRefractiveMeasurementsSequence = new DicomTag(0x0046, 0x0145);

		///<summary>(0046,0146) VR=FD VM=1 Sphere Power</summary>
		public readonly static DicomTag SpherePower = new DicomTag(0x0046, 0x0146);

		///<summary>(0046,0147) VR=FD VM=1 Cylinder Power</summary>
		public readonly static DicomTag CylinderPower = new DicomTag(0x0046, 0x0147);

		///<summary>(0048,0001) VR=FL VM=1 Imaged Volume Width</summary>
		public readonly static DicomTag ImagedVolumeWidth = new DicomTag(0x0048, 0x0001);

		///<summary>(0048,0002) VR=FL VM=1 Imaged Volume Height</summary>
		public readonly static DicomTag ImagedVolumeHeight = new DicomTag(0x0048, 0x0002);

		///<summary>(0048,0003) VR=FL VM=1 Imaged Volume Depth</summary>
		public readonly static DicomTag ImagedVolumeDepth = new DicomTag(0x0048, 0x0003);

		///<summary>(0048,0006) VR=UL VM=1 Total Pixel Matrix Columns</summary>
		public readonly static DicomTag TotalPixelMatrixColumns = new DicomTag(0x0048, 0x0006);

		///<summary>(0048,0007) VR=UL VM=1 Total Pixel Matrix Rows</summary>
		public readonly static DicomTag TotalPixelMatrixRows = new DicomTag(0x0048, 0x0007);

		///<summary>(0048,0008) VR=SQ VM=1 Total Pixel Matrix Origin Sequence</summary>
		public readonly static DicomTag TotalPixelMatrixOriginSequence = new DicomTag(0x0048, 0x0008);

		///<summary>(0048,0010) VR=CS VM=1 Specimen Label in Image</summary>
		public readonly static DicomTag SpecimenLabelInImage = new DicomTag(0x0048, 0x0010);

		///<summary>(0048,0011) VR=CS VM=1 Focus Method</summary>
		public readonly static DicomTag FocusMethod = new DicomTag(0x0048, 0x0011);

		///<summary>(0048,0012) VR=CS VM=1 Extended Depth of Field</summary>
		public readonly static DicomTag ExtendedDepthOfField = new DicomTag(0x0048, 0x0012);

		///<summary>(0048,0013) VR=US VM=1 Number of Focal Planes</summary>
		public readonly static DicomTag NumberOfFocalPlanes = new DicomTag(0x0048, 0x0013);

		///<summary>(0048,0014) VR=FL VM=1 Distance Between Focal Planes</summary>
		public readonly static DicomTag DistanceBetweenFocalPlanes = new DicomTag(0x0048, 0x0014);

		///<summary>(0048,0015) VR=US VM=3 Recommended Absent Pixel CIELab Value</summary>
		public readonly static DicomTag RecommendedAbsentPixelCIELabValue = new DicomTag(0x0048, 0x0015);

		///<summary>(0048,0100) VR=SQ VM=1 Illuminator Type Code Sequence</summary>
		public readonly static DicomTag IlluminatorTypeCodeSequence = new DicomTag(0x0048, 0x0100);

		///<summary>(0048,0102) VR=DS VM=6 Image Orientation (Slide)</summary>
		public readonly static DicomTag ImageOrientationSlide = new DicomTag(0x0048, 0x0102);

		///<summary>(0048,0105) VR=SQ VM=1 Optical Path Sequence</summary>
		public readonly static DicomTag OpticalPathSequence = new DicomTag(0x0048, 0x0105);

		///<summary>(0048,0106) VR=SH VM=1 Optical Path Identifier</summary>
		public readonly static DicomTag OpticalPathIdentifier = new DicomTag(0x0048, 0x0106);

		///<summary>(0048,0107) VR=ST VM=1 Optical Path Description</summary>
		public readonly static DicomTag OpticalPathDescription = new DicomTag(0x0048, 0x0107);

		///<summary>(0048,0108) VR=SQ VM=1 Illumination Color Code Sequence</summary>
		public readonly static DicomTag IlluminationColorCodeSequence = new DicomTag(0x0048, 0x0108);

		///<summary>(0048,0110) VR=SQ VM=1 Specimen Reference Sequence</summary>
		public readonly static DicomTag SpecimenReferenceSequence = new DicomTag(0x0048, 0x0110);

		///<summary>(0048,0111) VR=DS VM=1 Condenser Lens Power</summary>
		public readonly static DicomTag CondenserLensPower = new DicomTag(0x0048, 0x0111);

		///<summary>(0048,0112) VR=DS VM=1 Objective Lens Power</summary>
		public readonly static DicomTag ObjectiveLensPower = new DicomTag(0x0048, 0x0112);

		///<summary>(0048,0113) VR=DS VM=1 Objective Lens Numerical Aperture</summary>
		public readonly static DicomTag ObjectiveLensNumericalAperture = new DicomTag(0x0048, 0x0113);

		///<summary>(0048,0120) VR=SQ VM=1 Palette Color Lookup Table Sequence</summary>
		public readonly static DicomTag PaletteColorLookupTableSequence = new DicomTag(0x0048, 0x0120);

		///<summary>(0048,0200) VR=SQ VM=1 Referenced Image Navigation Sequence</summary>
		public readonly static DicomTag ReferencedImageNavigationSequence = new DicomTag(0x0048, 0x0200);

		///<summary>(0048,0201) VR=US VM=2 Top Left Hand Corner of Localizer Area</summary>
		public readonly static DicomTag TopLeftHandCornerOfLocalizerArea = new DicomTag(0x0048, 0x0201);

		///<summary>(0048,0202) VR=US VM=2 Bottom Right Hand Corner of Localizer Area</summary>
		public readonly static DicomTag BottomRightHandCornerOfLocalizerArea = new DicomTag(0x0048, 0x0202);

		///<summary>(0048,0207) VR=SQ VM=1 Optical Path Identification Sequence</summary>
		public readonly static DicomTag OpticalPathIdentificationSequence = new DicomTag(0x0048, 0x0207);

		///<summary>(0048,021a) VR=SQ VM=1 Plane Position (Slide) Sequence</summary>
		public readonly static DicomTag PlanePositionSlideSequence = new DicomTag(0x0048, 0x021a);

		///<summary>(0048,021e) VR=SL VM=1 Row Position In Total Image Pixel Matrix</summary>
		public readonly static DicomTag RowPositionInTotalImagePixelMatrix = new DicomTag(0x0048, 0x021e);

		///<summary>(0048,021f) VR=SL VM=1 Column Position In Total Image Pixel Matrix</summary>
		public readonly static DicomTag ColumnPositionInTotalImagePixelMatrix = new DicomTag(0x0048, 0x021f);

		///<summary>(0048,0301) VR=CS VM=1 Pixel Origin Interpretation</summary>
		public readonly static DicomTag PixelOriginInterpretation = new DicomTag(0x0048, 0x0301);

		///<summary>(0050,0004) VR=CS VM=1 Calibration Image</summary>
		public readonly static DicomTag CalibrationImage = new DicomTag(0x0050, 0x0004);

		///<summary>(0050,0010) VR=SQ VM=1 Device Sequence</summary>
		public readonly static DicomTag DeviceSequence = new DicomTag(0x0050, 0x0010);

		///<summary>(0050,0012) VR=SQ VM=1 Container Component Type Code Sequence</summary>
		public readonly static DicomTag ContainerComponentTypeCodeSequence = new DicomTag(0x0050, 0x0012);

		///<summary>(0050,0013) VR=FD VM=1 Container Component Thickness</summary>
		public readonly static DicomTag ContainerComponentThickness = new DicomTag(0x0050, 0x0013);

		///<summary>(0050,0014) VR=DS VM=1 Device Length</summary>
		public readonly static DicomTag DeviceLength = new DicomTag(0x0050, 0x0014);

		///<summary>(0050,0015) VR=FD VM=1 Container Component Width</summary>
		public readonly static DicomTag ContainerComponentWidth = new DicomTag(0x0050, 0x0015);

		///<summary>(0050,0016) VR=DS VM=1 Device Diameter</summary>
		public readonly static DicomTag DeviceDiameter = new DicomTag(0x0050, 0x0016);

		///<summary>(0050,0017) VR=CS VM=1 Device Diameter Units</summary>
		public readonly static DicomTag DeviceDiameterUnits = new DicomTag(0x0050, 0x0017);

		///<summary>(0050,0018) VR=DS VM=1 Device Volume</summary>
		public readonly static DicomTag DeviceVolume = new DicomTag(0x0050, 0x0018);

		///<summary>(0050,0019) VR=DS VM=1 Inter-Marker Distance</summary>
		public readonly static DicomTag InterMarkerDistance = new DicomTag(0x0050, 0x0019);

		///<summary>(0050,001a) VR=CS VM=1 Container Component Material</summary>
		public readonly static DicomTag ContainerComponentMaterial = new DicomTag(0x0050, 0x001a);

		///<summary>(0050,001b) VR=LO VM=1 Container Component ID</summary>
		public readonly static DicomTag ContainerComponentID = new DicomTag(0x0050, 0x001b);

		///<summary>(0050,001c) VR=FD VM=1 Container Component Length</summary>
		public readonly static DicomTag ContainerComponentLength = new DicomTag(0x0050, 0x001c);

		///<summary>(0050,001d) VR=FD VM=1 Container Component Diameter</summary>
		public readonly static DicomTag ContainerComponentDiameter = new DicomTag(0x0050, 0x001d);

		///<summary>(0050,001e) VR=LO VM=1 Container Component Description</summary>
		public readonly static DicomTag ContainerComponentDescription = new DicomTag(0x0050, 0x001e);

		///<summary>(0050,0020) VR=LO VM=1 Device Description</summary>
		public readonly static DicomTag DeviceDescription = new DicomTag(0x0050, 0x0020);

		///<summary>(0052,0001) VR=FL VM=1 Contrast/Bolus Ingredient Percent by Volume</summary>
		public readonly static DicomTag ContrastBolusIngredientPercentByVolume = new DicomTag(0x0052, 0x0001);

		///<summary>(0052,0002) VR=FD VM=1 OCT Focal Distance</summary>
		public readonly static DicomTag OCTFocalDistance = new DicomTag(0x0052, 0x0002);

		///<summary>(0052,0003) VR=FD VM=1 Beam Spot Size</summary>
		public readonly static DicomTag BeamSpotSize = new DicomTag(0x0052, 0x0003);

		///<summary>(0052,0004) VR=FD VM=1 Effective Refractive Index</summary>
		public readonly static DicomTag EffectiveRefractiveIndex = new DicomTag(0x0052, 0x0004);

		///<summary>(0052,0006) VR=CS VM=1 OCT Acquisition Domain</summary>
		public readonly static DicomTag OCTAcquisitionDomain = new DicomTag(0x0052, 0x0006);

		///<summary>(0052,0007) VR=FD VM=1 OCT Optical Center Wavelength</summary>
		public readonly static DicomTag OCTOpticalCenterWavelength = new DicomTag(0x0052, 0x0007);

		///<summary>(0052,0008) VR=FD VM=1 Axial Resolution</summary>
		public readonly static DicomTag AxialResolution = new DicomTag(0x0052, 0x0008);

		///<summary>(0052,0009) VR=FD VM=1 Ranging Depth</summary>
		public readonly static DicomTag RangingDepth = new DicomTag(0x0052, 0x0009);

		///<summary>(0052,0011) VR=FD VM=1 A‑line Rate</summary>
		public readonly static DicomTag ALineRate = new DicomTag(0x0052, 0x0011);

		///<summary>(0052,0012) VR=US VM=1 A‑lines Per Frame</summary>
		public readonly static DicomTag ALinesPerFrame = new DicomTag(0x0052, 0x0012);

		///<summary>(0052,0013) VR=FD VM=1 Catheter Rotational Rate</summary>
		public readonly static DicomTag CatheterRotationalRate = new DicomTag(0x0052, 0x0013);

		///<summary>(0052,0014) VR=FD VM=1 A‑line Pixel Spacing</summary>
		public readonly static DicomTag ALinePixelSpacing = new DicomTag(0x0052, 0x0014);

		///<summary>(0052,0016) VR=SQ VM=1 Mode of Percutaneous Access Sequence</summary>
		public readonly static DicomTag ModeOfPercutaneousAccessSequence = new DicomTag(0x0052, 0x0016);

		///<summary>(0052,0025) VR=SQ VM=1 Intravascular OCT Frame Type Sequence</summary>
		public readonly static DicomTag IntravascularOCTFrameTypeSequence = new DicomTag(0x0052, 0x0025);

		///<summary>(0052,0026) VR=CS VM=1 OCT Z Offset Applied</summary>
		public readonly static DicomTag OCTZOffsetApplied = new DicomTag(0x0052, 0x0026);

		///<summary>(0052,0027) VR=SQ VM=1 Intravascular Frame Content Sequence</summary>
		public readonly static DicomTag IntravascularFrameContentSequence = new DicomTag(0x0052, 0x0027);

		///<summary>(0052,0028) VR=FD VM=1 Intravascular Longitudinal Distance</summary>
		public readonly static DicomTag IntravascularLongitudinalDistance = new DicomTag(0x0052, 0x0028);

		///<summary>(0052,0029) VR=SQ VM=1 Intravascular OCT Frame Content Sequence</summary>
		public readonly static DicomTag IntravascularOCTFrameContentSequence = new DicomTag(0x0052, 0x0029);

		///<summary>(0052,0030) VR=SS VM=1 OCT Z Offset Correction</summary>
		public readonly static DicomTag OCTZOffsetCorrection = new DicomTag(0x0052, 0x0030);

		///<summary>(0052,0031) VR=CS VM=1 Catheter Direction of Rotation</summary>
		public readonly static DicomTag CatheterDirectionOfRotation = new DicomTag(0x0052, 0x0031);

		///<summary>(0052,0033) VR=FD VM=1 Seam Line Location</summary>
		public readonly static DicomTag SeamLineLocation = new DicomTag(0x0052, 0x0033);

		///<summary>(0052,0034) VR=FD VM=1 First A‑line Location</summary>
		public readonly static DicomTag FirstALineLocation = new DicomTag(0x0052, 0x0034);

		///<summary>(0052,0036) VR=US VM=1 Seam Line Index</summary>
		public readonly static DicomTag SeamLineIndex = new DicomTag(0x0052, 0x0036);

		///<summary>(0052,0038) VR=US VM=1 Number of Padded A‑lines</summary>
		public readonly static DicomTag NumberOfPaddedAlines = new DicomTag(0x0052, 0x0038);

		///<summary>(0052,0039) VR=CS VM=1 Interpolation Type</summary>
		public readonly static DicomTag InterpolationType = new DicomTag(0x0052, 0x0039);

		///<summary>(0052,003a) VR=CS VM=1 Refractive Index Applied</summary>
		public readonly static DicomTag RefractiveIndexApplied = new DicomTag(0x0052, 0x003a);

		///<summary>(0054,0011) VR=US VM=1 Number of Energy Windows</summary>
		public readonly static DicomTag NumberOfEnergyWindows = new DicomTag(0x0054, 0x0011);

		///<summary>(0054,0012) VR=SQ VM=1 Energy Window Information Sequence</summary>
		public readonly static DicomTag EnergyWindowInformationSequence = new DicomTag(0x0054, 0x0012);

		///<summary>(0054,0013) VR=SQ VM=1 Energy Window Range Sequence</summary>
		public readonly static DicomTag EnergyWindowRangeSequence = new DicomTag(0x0054, 0x0013);

		///<summary>(0054,0014) VR=DS VM=1 Energy Window Lower Limit</summary>
		public readonly static DicomTag EnergyWindowLowerLimit = new DicomTag(0x0054, 0x0014);

		///<summary>(0054,0015) VR=DS VM=1 Energy Window Upper Limit</summary>
		public readonly static DicomTag EnergyWindowUpperLimit = new DicomTag(0x0054, 0x0015);

		///<summary>(0054,0016) VR=SQ VM=1 Radiopharmaceutical Information Sequence</summary>
		public readonly static DicomTag RadiopharmaceuticalInformationSequence = new DicomTag(0x0054, 0x0016);

		///<summary>(0054,0017) VR=IS VM=1 Residual Syringe Counts</summary>
		public readonly static DicomTag ResidualSyringeCounts = new DicomTag(0x0054, 0x0017);

		///<summary>(0054,0018) VR=SH VM=1 Energy Window Name</summary>
		public readonly static DicomTag EnergyWindowName = new DicomTag(0x0054, 0x0018);

		///<summary>(0054,0020) VR=US VM=1-n Detector Vector</summary>
		public readonly static DicomTag DetectorVector = new DicomTag(0x0054, 0x0020);

		///<summary>(0054,0021) VR=US VM=1 Number of Detectors</summary>
		public readonly static DicomTag NumberOfDetectors = new DicomTag(0x0054, 0x0021);

		///<summary>(0054,0022) VR=SQ VM=1 Detector Information Sequence</summary>
		public readonly static DicomTag DetectorInformationSequence = new DicomTag(0x0054, 0x0022);

		///<summary>(0054,0030) VR=US VM=1-n Phase Vector</summary>
		public readonly static DicomTag PhaseVector = new DicomTag(0x0054, 0x0030);

		///<summary>(0054,0031) VR=US VM=1 Number of Phases</summary>
		public readonly static DicomTag NumberOfPhases = new DicomTag(0x0054, 0x0031);

		///<summary>(0054,0032) VR=SQ VM=1 Phase Information Sequence</summary>
		public readonly static DicomTag PhaseInformationSequence = new DicomTag(0x0054, 0x0032);

		///<summary>(0054,0033) VR=US VM=1 Number of Frames in Phase</summary>
		public readonly static DicomTag NumberOfFramesInPhase = new DicomTag(0x0054, 0x0033);

		///<summary>(0054,0036) VR=IS VM=1 Phase Delay</summary>
		public readonly static DicomTag PhaseDelay = new DicomTag(0x0054, 0x0036);

		///<summary>(0054,0038) VR=IS VM=1 Pause Between Frames</summary>
		public readonly static DicomTag PauseBetweenFrames = new DicomTag(0x0054, 0x0038);

		///<summary>(0054,0039) VR=CS VM=1 Phase Description</summary>
		public readonly static DicomTag PhaseDescription = new DicomTag(0x0054, 0x0039);

		///<summary>(0054,0050) VR=US VM=1-n Rotation Vector</summary>
		public readonly static DicomTag RotationVector = new DicomTag(0x0054, 0x0050);

		///<summary>(0054,0051) VR=US VM=1 Number of Rotations</summary>
		public readonly static DicomTag NumberOfRotations = new DicomTag(0x0054, 0x0051);

		///<summary>(0054,0052) VR=SQ VM=1 Rotation Information Sequence</summary>
		public readonly static DicomTag RotationInformationSequence = new DicomTag(0x0054, 0x0052);

		///<summary>(0054,0053) VR=US VM=1 Number of Frames in Rotation</summary>
		public readonly static DicomTag NumberOfFramesInRotation = new DicomTag(0x0054, 0x0053);

		///<summary>(0054,0060) VR=US VM=1-n R-R Interval Vector</summary>
		public readonly static DicomTag RRIntervalVector = new DicomTag(0x0054, 0x0060);

		///<summary>(0054,0061) VR=US VM=1 Number of R-R Intervals</summary>
		public readonly static DicomTag NumberOfRRIntervals = new DicomTag(0x0054, 0x0061);

		///<summary>(0054,0062) VR=SQ VM=1 Gated Information Sequence</summary>
		public readonly static DicomTag GatedInformationSequence = new DicomTag(0x0054, 0x0062);

		///<summary>(0054,0063) VR=SQ VM=1 Data Information Sequence</summary>
		public readonly static DicomTag DataInformationSequence = new DicomTag(0x0054, 0x0063);

		///<summary>(0054,0070) VR=US VM=1-n Time Slot Vector</summary>
		public readonly static DicomTag TimeSlotVector = new DicomTag(0x0054, 0x0070);

		///<summary>(0054,0071) VR=US VM=1 Number of Time Slots</summary>
		public readonly static DicomTag NumberOfTimeSlots = new DicomTag(0x0054, 0x0071);

		///<summary>(0054,0072) VR=SQ VM=1 Time Slot Information Sequence</summary>
		public readonly static DicomTag TimeSlotInformationSequence = new DicomTag(0x0054, 0x0072);

		///<summary>(0054,0073) VR=DS VM=1 Time Slot Time</summary>
		public readonly static DicomTag TimeSlotTime = new DicomTag(0x0054, 0x0073);

		///<summary>(0054,0080) VR=US VM=1-n Slice Vector</summary>
		public readonly static DicomTag SliceVector = new DicomTag(0x0054, 0x0080);

		///<summary>(0054,0081) VR=US VM=1 Number of Slices</summary>
		public readonly static DicomTag NumberOfSlices = new DicomTag(0x0054, 0x0081);

		///<summary>(0054,0090) VR=US VM=1-n Angular View Vector</summary>
		public readonly static DicomTag AngularViewVector = new DicomTag(0x0054, 0x0090);

		///<summary>(0054,0100) VR=US VM=1-n Time Slice Vector</summary>
		public readonly static DicomTag TimeSliceVector = new DicomTag(0x0054, 0x0100);

		///<summary>(0054,0101) VR=US VM=1 Number of Time Slices</summary>
		public readonly static DicomTag NumberOfTimeSlices = new DicomTag(0x0054, 0x0101);

		///<summary>(0054,0200) VR=DS VM=1 Start Angle</summary>
		public readonly static DicomTag StartAngle = new DicomTag(0x0054, 0x0200);

		///<summary>(0054,0202) VR=CS VM=1 Type of Detector Motion</summary>
		public readonly static DicomTag TypeOfDetectorMotion = new DicomTag(0x0054, 0x0202);

		///<summary>(0054,0210) VR=IS VM=1-n Trigger Vector</summary>
		public readonly static DicomTag TriggerVector = new DicomTag(0x0054, 0x0210);

		///<summary>(0054,0211) VR=US VM=1 Number of Triggers in Phase</summary>
		public readonly static DicomTag NumberOfTriggersInPhase = new DicomTag(0x0054, 0x0211);

		///<summary>(0054,0220) VR=SQ VM=1 View Code Sequence</summary>
		public readonly static DicomTag ViewCodeSequence = new DicomTag(0x0054, 0x0220);

		///<summary>(0054,0222) VR=SQ VM=1 View Modifier Code Sequence</summary>
		public readonly static DicomTag ViewModifierCodeSequence = new DicomTag(0x0054, 0x0222);

		///<summary>(0054,0300) VR=SQ VM=1 Radionuclide Code Sequence</summary>
		public readonly static DicomTag RadionuclideCodeSequence = new DicomTag(0x0054, 0x0300);

		///<summary>(0054,0302) VR=SQ VM=1 Administration Route Code Sequence</summary>
		public readonly static DicomTag AdministrationRouteCodeSequence = new DicomTag(0x0054, 0x0302);

		///<summary>(0054,0304) VR=SQ VM=1 Radiopharmaceutical Code Sequence</summary>
		public readonly static DicomTag RadiopharmaceuticalCodeSequence = new DicomTag(0x0054, 0x0304);

		///<summary>(0054,0306) VR=SQ VM=1 Calibration Data Sequence</summary>
		public readonly static DicomTag CalibrationDataSequence = new DicomTag(0x0054, 0x0306);

		///<summary>(0054,0308) VR=US VM=1 Energy Window Number</summary>
		public readonly static DicomTag EnergyWindowNumber = new DicomTag(0x0054, 0x0308);

		///<summary>(0054,0400) VR=SH VM=1 Image ID</summary>
		public readonly static DicomTag ImageID = new DicomTag(0x0054, 0x0400);

		///<summary>(0054,0410) VR=SQ VM=1 Patient Orientation Code Sequence</summary>
		public readonly static DicomTag PatientOrientationCodeSequence = new DicomTag(0x0054, 0x0410);

		///<summary>(0054,0412) VR=SQ VM=1 Patient Orientation Modifier Code Sequence</summary>
		public readonly static DicomTag PatientOrientationModifierCodeSequence = new DicomTag(0x0054, 0x0412);

		///<summary>(0054,0414) VR=SQ VM=1 Patient Gantry Relationship Code Sequence</summary>
		public readonly static DicomTag PatientGantryRelationshipCodeSequence = new DicomTag(0x0054, 0x0414);

		///<summary>(0054,0500) VR=CS VM=1 Slice Progression Direction</summary>
		public readonly static DicomTag SliceProgressionDirection = new DicomTag(0x0054, 0x0500);

		///<summary>(0054,1000) VR=CS VM=2 Series Type</summary>
		public readonly static DicomTag SeriesType = new DicomTag(0x0054, 0x1000);

		///<summary>(0054,1001) VR=CS VM=1 Units</summary>
		public readonly static DicomTag Units = new DicomTag(0x0054, 0x1001);

		///<summary>(0054,1002) VR=CS VM=1 Counts Source</summary>
		public readonly static DicomTag CountsSource = new DicomTag(0x0054, 0x1002);

		///<summary>(0054,1004) VR=CS VM=1 Reprojection Method</summary>
		public readonly static DicomTag ReprojectionMethod = new DicomTag(0x0054, 0x1004);

		///<summary>(0054,1006) VR=CS VM=1 SUV Type</summary>
		public readonly static DicomTag SUVType = new DicomTag(0x0054, 0x1006);

		///<summary>(0054,1100) VR=CS VM=1 Randoms Correction Method</summary>
		public readonly static DicomTag RandomsCorrectionMethod = new DicomTag(0x0054, 0x1100);

		///<summary>(0054,1101) VR=LO VM=1 Attenuation Correction Method</summary>
		public readonly static DicomTag AttenuationCorrectionMethod = new DicomTag(0x0054, 0x1101);

		///<summary>(0054,1102) VR=CS VM=1 Decay Correction</summary>
		public readonly static DicomTag DecayCorrection = new DicomTag(0x0054, 0x1102);

		///<summary>(0054,1103) VR=LO VM=1 Reconstruction Method</summary>
		public readonly static DicomTag ReconstructionMethod = new DicomTag(0x0054, 0x1103);

		///<summary>(0054,1104) VR=LO VM=1 Detector Lines of Response Used</summary>
		public readonly static DicomTag DetectorLinesOfResponseUsed = new DicomTag(0x0054, 0x1104);

		///<summary>(0054,1105) VR=LO VM=1 Scatter Correction Method</summary>
		public readonly static DicomTag ScatterCorrectionMethod = new DicomTag(0x0054, 0x1105);

		///<summary>(0054,1200) VR=DS VM=1 Axial Acceptance</summary>
		public readonly static DicomTag AxialAcceptance = new DicomTag(0x0054, 0x1200);

		///<summary>(0054,1201) VR=IS VM=2 Axial Mash</summary>
		public readonly static DicomTag AxialMash = new DicomTag(0x0054, 0x1201);

		///<summary>(0054,1202) VR=IS VM=1 Transverse Mash</summary>
		public readonly static DicomTag TransverseMash = new DicomTag(0x0054, 0x1202);

		///<summary>(0054,1203) VR=DS VM=2 Detector Element Size</summary>
		public readonly static DicomTag DetectorElementSize = new DicomTag(0x0054, 0x1203);

		///<summary>(0054,1210) VR=DS VM=1 Coincidence Window Width</summary>
		public readonly static DicomTag CoincidenceWindowWidth = new DicomTag(0x0054, 0x1210);

		///<summary>(0054,1220) VR=CS VM=1-n Secondary Counts Type</summary>
		public readonly static DicomTag SecondaryCountsType = new DicomTag(0x0054, 0x1220);

		///<summary>(0054,1300) VR=DS VM=1 Frame Reference Time</summary>
		public readonly static DicomTag FrameReferenceTime = new DicomTag(0x0054, 0x1300);

		///<summary>(0054,1310) VR=IS VM=1 Primary (Prompts) Counts Accumulated</summary>
		public readonly static DicomTag PrimaryPromptsCountsAccumulated = new DicomTag(0x0054, 0x1310);

		///<summary>(0054,1311) VR=IS VM=1-n Secondary Counts Accumulated</summary>
		public readonly static DicomTag SecondaryCountsAccumulated = new DicomTag(0x0054, 0x1311);

		///<summary>(0054,1320) VR=DS VM=1 Slice Sensitivity Factor</summary>
		public readonly static DicomTag SliceSensitivityFactor = new DicomTag(0x0054, 0x1320);

		///<summary>(0054,1321) VR=DS VM=1 Decay Factor</summary>
		public readonly static DicomTag DecayFactor = new DicomTag(0x0054, 0x1321);

		///<summary>(0054,1322) VR=DS VM=1 Dose Calibration Factor</summary>
		public readonly static DicomTag DoseCalibrationFactor = new DicomTag(0x0054, 0x1322);

		///<summary>(0054,1323) VR=DS VM=1 Scatter Fraction Factor</summary>
		public readonly static DicomTag ScatterFractionFactor = new DicomTag(0x0054, 0x1323);

		///<summary>(0054,1324) VR=DS VM=1 Dead Time Factor</summary>
		public readonly static DicomTag DeadTimeFactor = new DicomTag(0x0054, 0x1324);

		///<summary>(0054,1330) VR=US VM=1 Image Index</summary>
		public readonly static DicomTag ImageIndex = new DicomTag(0x0054, 0x1330);

		///<summary>(0054,1400) VR=CS VM=1-n Counts Included (RETIRED)</summary>
		public readonly static DicomTag CountsIncludedRETIRED = new DicomTag(0x0054, 0x1400);

		///<summary>(0054,1401) VR=CS VM=1 Dead Time Correction Flag (RETIRED)</summary>
		public readonly static DicomTag DeadTimeCorrectionFlagRETIRED = new DicomTag(0x0054, 0x1401);

		///<summary>(0060,3000) VR=SQ VM=1 Histogram Sequence</summary>
		public readonly static DicomTag HistogramSequence = new DicomTag(0x0060, 0x3000);

		///<summary>(0060,3002) VR=US VM=1 Histogram Number of Bins</summary>
		public readonly static DicomTag HistogramNumberOfBins = new DicomTag(0x0060, 0x3002);

		///<summary>(0060,3004) VR=US/SS VM=1 Histogram First Bin Value</summary>
		public readonly static DicomTag HistogramFirstBinValue = new DicomTag(0x0060, 0x3004);

		///<summary>(0060,3006) VR=US/SS VM=1 Histogram Last Bin Value</summary>
		public readonly static DicomTag HistogramLastBinValue = new DicomTag(0x0060, 0x3006);

		///<summary>(0060,3008) VR=US VM=1 Histogram Bin Width</summary>
		public readonly static DicomTag HistogramBinWidth = new DicomTag(0x0060, 0x3008);

		///<summary>(0060,3010) VR=LO VM=1 Histogram Explanation</summary>
		public readonly static DicomTag HistogramExplanation = new DicomTag(0x0060, 0x3010);

		///<summary>(0060,3020) VR=UL VM=1-n Histogram Data</summary>
		public readonly static DicomTag HistogramData = new DicomTag(0x0060, 0x3020);

		///<summary>(0062,0001) VR=CS VM=1 Segmentation Type</summary>
		public readonly static DicomTag SegmentationType = new DicomTag(0x0062, 0x0001);

		///<summary>(0062,0002) VR=SQ VM=1 Segment Sequence</summary>
		public readonly static DicomTag SegmentSequence = new DicomTag(0x0062, 0x0002);

		///<summary>(0062,0003) VR=SQ VM=1 Segmented Property Category Code Sequence</summary>
		public readonly static DicomTag SegmentedPropertyCategoryCodeSequence = new DicomTag(0x0062, 0x0003);

		///<summary>(0062,0004) VR=US VM=1 Segment Number</summary>
		public readonly static DicomTag SegmentNumber = new DicomTag(0x0062, 0x0004);

		///<summary>(0062,0005) VR=LO VM=1 Segment Label</summary>
		public readonly static DicomTag SegmentLabel = new DicomTag(0x0062, 0x0005);

		///<summary>(0062,0006) VR=ST VM=1 Segment Description</summary>
		public readonly static DicomTag SegmentDescription = new DicomTag(0x0062, 0x0006);

		///<summary>(0062,0008) VR=CS VM=1 Segment Algorithm Type</summary>
		public readonly static DicomTag SegmentAlgorithmType = new DicomTag(0x0062, 0x0008);

		///<summary>(0062,0009) VR=LO VM=1 Segment Algorithm Name</summary>
		public readonly static DicomTag SegmentAlgorithmName = new DicomTag(0x0062, 0x0009);

		///<summary>(0062,000a) VR=SQ VM=1 Segment Identification Sequence</summary>
		public readonly static DicomTag SegmentIdentificationSequence = new DicomTag(0x0062, 0x000a);

		///<summary>(0062,000b) VR=US VM=1-n Referenced Segment Number</summary>
		public readonly static DicomTag ReferencedSegmentNumber = new DicomTag(0x0062, 0x000b);

		///<summary>(0062,000c) VR=US VM=1 Recommended Display Grayscale Value</summary>
		public readonly static DicomTag RecommendedDisplayGrayscaleValue = new DicomTag(0x0062, 0x000c);

		///<summary>(0062,000d) VR=US VM=3 Recommended Display CIELab Value</summary>
		public readonly static DicomTag RecommendedDisplayCIELabValue = new DicomTag(0x0062, 0x000d);

		///<summary>(0062,000e) VR=US VM=1 Maximum Fractional Value</summary>
		public readonly static DicomTag MaximumFractionalValue = new DicomTag(0x0062, 0x000e);

		///<summary>(0062,000f) VR=SQ VM=1 Segmented Property Type Code Sequence</summary>
		public readonly static DicomTag SegmentedPropertyTypeCodeSequence = new DicomTag(0x0062, 0x000f);

		///<summary>(0062,0010) VR=CS VM=1 Segmentation Fractional Type</summary>
		public readonly static DicomTag SegmentationFractionalType = new DicomTag(0x0062, 0x0010);

		///<summary>(0064,0002) VR=SQ VM=1 Deformable Registration Sequence</summary>
		public readonly static DicomTag DeformableRegistrationSequence = new DicomTag(0x0064, 0x0002);

		///<summary>(0064,0003) VR=UI VM=1 Source Frame of Reference UID</summary>
		public readonly static DicomTag SourceFrameOfReferenceUID = new DicomTag(0x0064, 0x0003);

		///<summary>(0064,0005) VR=SQ VM=1 Deformable Registration Grid Sequence</summary>
		public readonly static DicomTag DeformableRegistrationGridSequence = new DicomTag(0x0064, 0x0005);

		///<summary>(0064,0007) VR=UL VM=3 Grid Dimensions</summary>
		public readonly static DicomTag GridDimensions = new DicomTag(0x0064, 0x0007);

		///<summary>(0064,0008) VR=FD VM=3 Grid Resolution</summary>
		public readonly static DicomTag GridResolution = new DicomTag(0x0064, 0x0008);

		///<summary>(0064,0009) VR=OF VM=1 Vector Grid Data</summary>
		public readonly static DicomTag VectorGridData = new DicomTag(0x0064, 0x0009);

		///<summary>(0064,000f) VR=SQ VM=1 Pre Deformation Matrix Registration Sequence</summary>
		public readonly static DicomTag PreDeformationMatrixRegistrationSequence = new DicomTag(0x0064, 0x000f);

		///<summary>(0064,0010) VR=SQ VM=1 Post Deformation Matrix Registration Sequence</summary>
		public readonly static DicomTag PostDeformationMatrixRegistrationSequence = new DicomTag(0x0064, 0x0010);

		///<summary>(0066,0001) VR=UL VM=1 Number of Surfaces</summary>
		public readonly static DicomTag NumberOfSurfaces = new DicomTag(0x0066, 0x0001);

		///<summary>(0066,0002) VR=SQ VM=1 Surface Sequence</summary>
		public readonly static DicomTag SurfaceSequence = new DicomTag(0x0066, 0x0002);

		///<summary>(0066,0003) VR=UL VM=1 Surface Number</summary>
		public readonly static DicomTag SurfaceNumber = new DicomTag(0x0066, 0x0003);

		///<summary>(0066,0004) VR=LT VM=1 Surface Comments</summary>
		public readonly static DicomTag SurfaceComments = new DicomTag(0x0066, 0x0004);

		///<summary>(0066,0009) VR=CS VM=1 Surface Processing</summary>
		public readonly static DicomTag SurfaceProcessing = new DicomTag(0x0066, 0x0009);

		///<summary>(0066,000a) VR=FL VM=1 Surface Processing Ratio</summary>
		public readonly static DicomTag SurfaceProcessingRatio = new DicomTag(0x0066, 0x000a);

		///<summary>(0066,000b) VR=LO VM=1 Surface Processing Description</summary>
		public readonly static DicomTag SurfaceProcessingDescription = new DicomTag(0x0066, 0x000b);

		///<summary>(0066,000c) VR=FL VM=1 Recommended Presentation Opacity</summary>
		public readonly static DicomTag RecommendedPresentationOpacity = new DicomTag(0x0066, 0x000c);

		///<summary>(0066,000d) VR=CS VM=1 Recommended Presentation Type</summary>
		public readonly static DicomTag RecommendedPresentationType = new DicomTag(0x0066, 0x000d);

		///<summary>(0066,000e) VR=CS VM=1 Finite Volume</summary>
		public readonly static DicomTag FiniteVolume = new DicomTag(0x0066, 0x000e);

		///<summary>(0066,0010) VR=CS VM=1 Manifold</summary>
		public readonly static DicomTag Manifold = new DicomTag(0x0066, 0x0010);

		///<summary>(0066,0011) VR=SQ VM=1 Surface Points Sequence</summary>
		public readonly static DicomTag SurfacePointsSequence = new DicomTag(0x0066, 0x0011);

		///<summary>(0066,0012) VR=SQ VM=1 Surface Points Normals Sequence</summary>
		public readonly static DicomTag SurfacePointsNormalsSequence = new DicomTag(0x0066, 0x0012);

		///<summary>(0066,0013) VR=SQ VM=1 Surface Mesh Primitives Sequence</summary>
		public readonly static DicomTag SurfaceMeshPrimitivesSequence = new DicomTag(0x0066, 0x0013);

		///<summary>(0066,0015) VR=UL VM=1 Number of Surface Points</summary>
		public readonly static DicomTag NumberOfSurfacePoints = new DicomTag(0x0066, 0x0015);

		///<summary>(0066,0016) VR=OF VM=1 Point Coordinates Data</summary>
		public readonly static DicomTag PointCoordinatesData = new DicomTag(0x0066, 0x0016);

		///<summary>(0066,0017) VR=FL VM=3 Point Position Accuracy</summary>
		public readonly static DicomTag PointPositionAccuracy = new DicomTag(0x0066, 0x0017);

		///<summary>(0066,0018) VR=FL VM=1 Mean Point Distance</summary>
		public readonly static DicomTag MeanPointDistance = new DicomTag(0x0066, 0x0018);

		///<summary>(0066,0019) VR=FL VM=1 Maximum Point Distance</summary>
		public readonly static DicomTag MaximumPointDistance = new DicomTag(0x0066, 0x0019);

		///<summary>(0066,001a) VR=FL VM=6 Points Bounding Box Coordinates</summary>
		public readonly static DicomTag PointsBoundingBoxCoordinates = new DicomTag(0x0066, 0x001a);

		///<summary>(0066,001b) VR=FL VM=3 Axis of Rotation</summary>
		public readonly static DicomTag AxisOfRotation = new DicomTag(0x0066, 0x001b);

		///<summary>(0066,001c) VR=FL VM=3 Center of Rotation</summary>
		public readonly static DicomTag CenterOfRotation = new DicomTag(0x0066, 0x001c);

		///<summary>(0066,001e) VR=UL VM=1 Number of Vectors</summary>
		public readonly static DicomTag NumberOfVectors = new DicomTag(0x0066, 0x001e);

		///<summary>(0066,001f) VR=US VM=1 Vector Dimensionality</summary>
		public readonly static DicomTag VectorDimensionality = new DicomTag(0x0066, 0x001f);

		///<summary>(0066,0020) VR=FL VM=1-n Vector Accuracy</summary>
		public readonly static DicomTag VectorAccuracy = new DicomTag(0x0066, 0x0020);

		///<summary>(0066,0021) VR=OF VM=1 Vector Coordinate Data</summary>
		public readonly static DicomTag VectorCoordinateData = new DicomTag(0x0066, 0x0021);

		///<summary>(0066,0023) VR=OW VM=1 Triangle Point Index List</summary>
		public readonly static DicomTag TrianglePointIndexList = new DicomTag(0x0066, 0x0023);

		///<summary>(0066,0024) VR=OW VM=1 Edge Point Index List</summary>
		public readonly static DicomTag EdgePointIndexList = new DicomTag(0x0066, 0x0024);

		///<summary>(0066,0025) VR=OW VM=1 Vertex Point Index List</summary>
		public readonly static DicomTag VertexPointIndexList = new DicomTag(0x0066, 0x0025);

		///<summary>(0066,0026) VR=SQ VM=1 Triangle Strip Sequence</summary>
		public readonly static DicomTag TriangleStripSequence = new DicomTag(0x0066, 0x0026);

		///<summary>(0066,0027) VR=SQ VM=1 Triangle Fan Sequence</summary>
		public readonly static DicomTag TriangleFanSequence = new DicomTag(0x0066, 0x0027);

		///<summary>(0066,0028) VR=SQ VM=1 Line Sequence</summary>
		public readonly static DicomTag LineSequence = new DicomTag(0x0066, 0x0028);

		///<summary>(0066,0029) VR=OW VM=1 Primitive Point Index List</summary>
		public readonly static DicomTag PrimitivePointIndexList = new DicomTag(0x0066, 0x0029);

		///<summary>(0066,002a) VR=UL VM=1 Surface Count</summary>
		public readonly static DicomTag SurfaceCount = new DicomTag(0x0066, 0x002a);

		///<summary>(0066,002b) VR=SQ VM=1 Referenced Surface Sequence</summary>
		public readonly static DicomTag ReferencedSurfaceSequence = new DicomTag(0x0066, 0x002b);

		///<summary>(0066,002c) VR=UL VM=1 Referenced Surface Number</summary>
		public readonly static DicomTag ReferencedSurfaceNumber = new DicomTag(0x0066, 0x002c);

		///<summary>(0066,002d) VR=SQ VM=1 Segment Surface Generation Algorithm Identification Sequence</summary>
		public readonly static DicomTag SegmentSurfaceGenerationAlgorithmIdentificationSequence = new DicomTag(0x0066, 0x002d);

		///<summary>(0066,002e) VR=SQ VM=1 Segment Surface Source Instance Sequence</summary>
		public readonly static DicomTag SegmentSurfaceSourceInstanceSequence = new DicomTag(0x0066, 0x002e);

		///<summary>(0066,002f) VR=SQ VM=1 Algorithm Family Code Sequence</summary>
		public readonly static DicomTag AlgorithmFamilyCodeSequence = new DicomTag(0x0066, 0x002f);

		///<summary>(0066,0030) VR=SQ VM=1 Algorithm Name Code Sequence</summary>
		public readonly static DicomTag AlgorithmNameCodeSequence = new DicomTag(0x0066, 0x0030);

		///<summary>(0066,0031) VR=LO VM=1 Algorithm Version</summary>
		public readonly static DicomTag AlgorithmVersion = new DicomTag(0x0066, 0x0031);

		///<summary>(0066,0032) VR=LT VM=1 Algorithm Parameters</summary>
		public readonly static DicomTag AlgorithmParameters = new DicomTag(0x0066, 0x0032);

		///<summary>(0066,0034) VR=SQ VM=1 Facet Sequence</summary>
		public readonly static DicomTag FacetSequence = new DicomTag(0x0066, 0x0034);

		///<summary>(0066,0035) VR=SQ VM=1 Surface Processing Algorithm Identification Sequence</summary>
		public readonly static DicomTag SurfaceProcessingAlgorithmIdentificationSequence = new DicomTag(0x0066, 0x0035);

		///<summary>(0066,0036) VR=LO VM=1 Algorithm Name</summary>
		public readonly static DicomTag AlgorithmName = new DicomTag(0x0066, 0x0036);

		///<summary>(0068,6210) VR=LO VM=1 Implant Size</summary>
		public readonly static DicomTag ImplantSize = new DicomTag(0x0068, 0x6210);

		///<summary>(0068,6221) VR=LO VM=1 Implant Template Version</summary>
		public readonly static DicomTag ImplantTemplateVersion = new DicomTag(0x0068, 0x6221);

		///<summary>(0068,6222) VR=SQ VM=1 Replaced Implant Template Sequence</summary>
		public readonly static DicomTag ReplacedImplantTemplateSequence = new DicomTag(0x0068, 0x6222);

		///<summary>(0068,6223) VR=CS VM=1 Implant Type</summary>
		public readonly static DicomTag ImplantType = new DicomTag(0x0068, 0x6223);

		///<summary>(0068,6224) VR=SQ VM=1 Derivation Implant Template Sequence</summary>
		public readonly static DicomTag DerivationImplantTemplateSequence = new DicomTag(0x0068, 0x6224);

		///<summary>(0068,6225) VR=SQ VM=1 Original Implant Template Sequence</summary>
		public readonly static DicomTag OriginalImplantTemplateSequence = new DicomTag(0x0068, 0x6225);

		///<summary>(0068,6226) VR=DT VM=1 Effective DateTime</summary>
		public readonly static DicomTag EffectiveDateTime = new DicomTag(0x0068, 0x6226);

		///<summary>(0068,6230) VR=SQ VM=1 Implant Target Anatomy Sequence</summary>
		public readonly static DicomTag ImplantTargetAnatomySequence = new DicomTag(0x0068, 0x6230);

		///<summary>(0068,6260) VR=SQ VM=1 Information From Manufacturer Sequence</summary>
		public readonly static DicomTag InformationFromManufacturerSequence = new DicomTag(0x0068, 0x6260);

		///<summary>(0068,6265) VR=SQ VM=1 Notification From Manufacturer Sequence</summary>
		public readonly static DicomTag NotificationFromManufacturerSequence = new DicomTag(0x0068, 0x6265);

		///<summary>(0068,6270) VR=DT VM=1 Information Issue DateTime</summary>
		public readonly static DicomTag InformationIssueDateTime = new DicomTag(0x0068, 0x6270);

		///<summary>(0068,6280) VR=ST VM=1 Information Summary</summary>
		public readonly static DicomTag InformationSummary = new DicomTag(0x0068, 0x6280);

		///<summary>(0068,62a0) VR=SQ VM=1 Implant Regulatory Disapproval Code Sequence</summary>
		public readonly static DicomTag ImplantRegulatoryDisapprovalCodeSequence = new DicomTag(0x0068, 0x62a0);

		///<summary>(0068,62a5) VR=FD VM=1 Overall Template Spatial Tolerance</summary>
		public readonly static DicomTag OverallTemplateSpatialTolerance = new DicomTag(0x0068, 0x62a5);

		///<summary>(0068,62c0) VR=SQ VM=1 HPGL Document Sequence</summary>
		public readonly static DicomTag HPGLDocumentSequence = new DicomTag(0x0068, 0x62c0);

		///<summary>(0068,62d0) VR=US VM=1 HPGL Document ID</summary>
		public readonly static DicomTag HPGLDocumentID = new DicomTag(0x0068, 0x62d0);

		///<summary>(0068,62d5) VR=LO VM=1 HPGL Document Label</summary>
		public readonly static DicomTag HPGLDocumentLabel = new DicomTag(0x0068, 0x62d5);

		///<summary>(0068,62e0) VR=SQ VM=1 View Orientation Code Sequence</summary>
		public readonly static DicomTag ViewOrientationCodeSequence = new DicomTag(0x0068, 0x62e0);

		///<summary>(0068,62f0) VR=FD VM=9 View Orientation Modifier</summary>
		public readonly static DicomTag ViewOrientationModifier = new DicomTag(0x0068, 0x62f0);

		///<summary>(0068,62f2) VR=FD VM=1 HPGL Document Scaling</summary>
		public readonly static DicomTag HPGLDocumentScaling = new DicomTag(0x0068, 0x62f2);

		///<summary>(0068,6300) VR=OB VM=1 HPGL Document</summary>
		public readonly static DicomTag HPGLDocument = new DicomTag(0x0068, 0x6300);

		///<summary>(0068,6310) VR=US VM=1 HPGL Contour Pen Number</summary>
		public readonly static DicomTag HPGLContourPenNumber = new DicomTag(0x0068, 0x6310);

		///<summary>(0068,6320) VR=SQ VM=1 HPGL Pen Sequence</summary>
		public readonly static DicomTag HPGLPenSequence = new DicomTag(0x0068, 0x6320);

		///<summary>(0068,6330) VR=US VM=1 HPGL Pen Number</summary>
		public readonly static DicomTag HPGLPenNumber = new DicomTag(0x0068, 0x6330);

		///<summary>(0068,6340) VR=LO VM=1 HPGL Pen Label</summary>
		public readonly static DicomTag HPGLPenLabel = new DicomTag(0x0068, 0x6340);

		///<summary>(0068,6345) VR=ST VM=1 HPGL Pen Description</summary>
		public readonly static DicomTag HPGLPenDescription = new DicomTag(0x0068, 0x6345);

		///<summary>(0068,6346) VR=FD VM=2 Recommended Rotation Point</summary>
		public readonly static DicomTag RecommendedRotationPoint = new DicomTag(0x0068, 0x6346);

		///<summary>(0068,6347) VR=FD VM=4 Bounding Rectangle</summary>
		public readonly static DicomTag BoundingRectangle = new DicomTag(0x0068, 0x6347);

		///<summary>(0068,6350) VR=US VM=1-n Implant Template 3D Model Surface Number</summary>
		public readonly static DicomTag ImplantTemplate3DModelSurfaceNumber = new DicomTag(0x0068, 0x6350);

		///<summary>(0068,6360) VR=SQ VM=1 Surface Model Description Sequence</summary>
		public readonly static DicomTag SurfaceModelDescriptionSequence = new DicomTag(0x0068, 0x6360);

		///<summary>(0068,6380) VR=LO VM=1 Surface Model Label</summary>
		public readonly static DicomTag SurfaceModelLabel = new DicomTag(0x0068, 0x6380);

		///<summary>(0068,6390) VR=FD VM=1 Surface Model Scaling Factor</summary>
		public readonly static DicomTag SurfaceModelScalingFactor = new DicomTag(0x0068, 0x6390);

		///<summary>(0068,63a0) VR=SQ VM=1 Materials Code Sequence</summary>
		public readonly static DicomTag MaterialsCodeSequence = new DicomTag(0x0068, 0x63a0);

		///<summary>(0068,63a4) VR=SQ VM=1 Coating Materials Code Sequence</summary>
		public readonly static DicomTag CoatingMaterialsCodeSequence = new DicomTag(0x0068, 0x63a4);

		///<summary>(0068,63a8) VR=SQ VM=1 Implant Type Code Sequence</summary>
		public readonly static DicomTag ImplantTypeCodeSequence = new DicomTag(0x0068, 0x63a8);

		///<summary>(0068,63ac) VR=SQ VM=1 Fixation Method Code Sequence</summary>
		public readonly static DicomTag FixationMethodCodeSequence = new DicomTag(0x0068, 0x63ac);

		///<summary>(0068,63b0) VR=SQ VM=1 Mating Feature Sets Sequence</summary>
		public readonly static DicomTag MatingFeatureSetsSequence = new DicomTag(0x0068, 0x63b0);

		///<summary>(0068,63c0) VR=US VM=1 Mating Feature Set ID</summary>
		public readonly static DicomTag MatingFeatureSetID = new DicomTag(0x0068, 0x63c0);

		///<summary>(0068,63d0) VR=LO VM=1 Mating Feature Set Label</summary>
		public readonly static DicomTag MatingFeatureSetLabel = new DicomTag(0x0068, 0x63d0);

		///<summary>(0068,63e0) VR=SQ VM=1 Mating Feature Sequence</summary>
		public readonly static DicomTag MatingFeatureSequence = new DicomTag(0x0068, 0x63e0);

		///<summary>(0068,63f0) VR=US VM=1 Mating Feature ID</summary>
		public readonly static DicomTag MatingFeatureID = new DicomTag(0x0068, 0x63f0);

		///<summary>(0068,6400) VR=SQ VM=1 Mating Feature Degree of Freedom Sequence</summary>
		public readonly static DicomTag MatingFeatureDegreeOfFreedomSequence = new DicomTag(0x0068, 0x6400);

		///<summary>(0068,6410) VR=US VM=1 Degree of Freedom ID</summary>
		public readonly static DicomTag DegreeOfFreedomID = new DicomTag(0x0068, 0x6410);

		///<summary>(0068,6420) VR=CS VM=1 Degree of Freedom Type</summary>
		public readonly static DicomTag DegreeOfFreedomType = new DicomTag(0x0068, 0x6420);

		///<summary>(0068,6430) VR=SQ VM=1 2D Mating Feature Coordinates Sequence</summary>
		public readonly static DicomTag TwoDMatingFeatureCoordinatesSequence = new DicomTag(0x0068, 0x6430);

		///<summary>(0068,6440) VR=US VM=1 Referenced HPGL Document ID</summary>
		public readonly static DicomTag ReferencedHPGLDocumentID = new DicomTag(0x0068, 0x6440);

		///<summary>(0068,6450) VR=FD VM=2 2D Mating Point</summary>
		public readonly static DicomTag TwoDMatingPoint = new DicomTag(0x0068, 0x6450);

		///<summary>(0068,6460) VR=FD VM=4 2D Mating Axes</summary>
		public readonly static DicomTag TwoDMatingAxes = new DicomTag(0x0068, 0x6460);

		///<summary>(0068,6470) VR=SQ VM=1 2D Degree of Freedom Sequence</summary>
		public readonly static DicomTag TwoDDegreeOfFreedomSequence = new DicomTag(0x0068, 0x6470);

		///<summary>(0068,6490) VR=FD VM=3 3D Degree of Freedom Axis</summary>
		public readonly static DicomTag ThreeDDegreeOfFreedomAxis = new DicomTag(0x0068, 0x6490);

		///<summary>(0068,64a0) VR=FD VM=2 Range of Freedom</summary>
		public readonly static DicomTag RangeOfFreedom = new DicomTag(0x0068, 0x64a0);

		///<summary>(0068,64c0) VR=FD VM=3 3D Mating Point</summary>
		public readonly static DicomTag ThreeDMatingPoint = new DicomTag(0x0068, 0x64c0);

		///<summary>(0068,64d0) VR=FD VM=9 3D Mating Axes</summary>
		public readonly static DicomTag ThreeDMatingAxes = new DicomTag(0x0068, 0x64d0);

		///<summary>(0068,64f0) VR=FD VM=3 2D Degree of Freedom Axis</summary>
		public readonly static DicomTag TwoDDegreeOfFreedomAxis = new DicomTag(0x0068, 0x64f0);

		///<summary>(0068,6500) VR=SQ VM=1 Planning Landmark Point Sequence</summary>
		public readonly static DicomTag PlanningLandmarkPointSequence = new DicomTag(0x0068, 0x6500);

		///<summary>(0068,6510) VR=SQ VM=1 Planning Landmark Line Sequence</summary>
		public readonly static DicomTag PlanningLandmarkLineSequence = new DicomTag(0x0068, 0x6510);

		///<summary>(0068,6520) VR=SQ VM=1 Planning Landmark Plane Sequence</summary>
		public readonly static DicomTag PlanningLandmarkPlaneSequence = new DicomTag(0x0068, 0x6520);

		///<summary>(0068,6530) VR=US VM=1 Planning Landmark ID</summary>
		public readonly static DicomTag PlanningLandmarkID = new DicomTag(0x0068, 0x6530);

		///<summary>(0068,6540) VR=LO VM=1 Planning Landmark Description</summary>
		public readonly static DicomTag PlanningLandmarkDescription = new DicomTag(0x0068, 0x6540);

		///<summary>(0068,6545) VR=SQ VM=1 Planning Landmark Identification Code Sequence</summary>
		public readonly static DicomTag PlanningLandmarkIdentificationCodeSequence = new DicomTag(0x0068, 0x6545);

		///<summary>(0068,6550) VR=SQ VM=1 2D Point Coordinates Sequence</summary>
		public readonly static DicomTag TwoDPointCoordinatesSequence = new DicomTag(0x0068, 0x6550);

		///<summary>(0068,6560) VR=FD VM=2 2D Point Coordinates</summary>
		public readonly static DicomTag TwoDPointCoordinates = new DicomTag(0x0068, 0x6560);

		///<summary>(0068,6590) VR=FD VM=3 3D Point Coordinates</summary>
		public readonly static DicomTag ThreeDPointCoordinates = new DicomTag(0x0068, 0x6590);

		///<summary>(0068,65a0) VR=SQ VM=1 2D Line Coordinates Sequence</summary>
		public readonly static DicomTag TwoDLineCoordinatesSequence = new DicomTag(0x0068, 0x65a0);

		///<summary>(0068,65b0) VR=FD VM=4 2D Line Coordinates</summary>
		public readonly static DicomTag TwoDLineCoordinates = new DicomTag(0x0068, 0x65b0);

		///<summary>(0068,65d0) VR=FD VM=6 3D Line Coordinates</summary>
		public readonly static DicomTag ThreeDLineCoordinates = new DicomTag(0x0068, 0x65d0);

		///<summary>(0068,65e0) VR=SQ VM=1 2D Plane Coordinates Sequence</summary>
		public readonly static DicomTag TwoDPlaneCoordinatesSequence = new DicomTag(0x0068, 0x65e0);

		///<summary>(0068,65f0) VR=FD VM=4 2D Plane Intersection</summary>
		public readonly static DicomTag TwoDPlaneIntersection = new DicomTag(0x0068, 0x65f0);

		///<summary>(0068,6610) VR=FD VM=3 3D Plane Origin</summary>
		public readonly static DicomTag ThreeDPlaneOrigin = new DicomTag(0x0068, 0x6610);

		///<summary>(0068,6620) VR=FD VM=3 3D Plane Normal</summary>
		public readonly static DicomTag ThreeDPlaneNormal = new DicomTag(0x0068, 0x6620);

		///<summary>(0070,0001) VR=SQ VM=1 Graphic Annotation Sequence</summary>
		public readonly static DicomTag GraphicAnnotationSequence = new DicomTag(0x0070, 0x0001);

		///<summary>(0070,0002) VR=CS VM=1 Graphic Layer</summary>
		public readonly static DicomTag GraphicLayer = new DicomTag(0x0070, 0x0002);

		///<summary>(0070,0003) VR=CS VM=1 Bounding Box Annotation Units</summary>
		public readonly static DicomTag BoundingBoxAnnotationUnits = new DicomTag(0x0070, 0x0003);

		///<summary>(0070,0004) VR=CS VM=1 Anchor Point Annotation Units</summary>
		public readonly static DicomTag AnchorPointAnnotationUnits = new DicomTag(0x0070, 0x0004);

		///<summary>(0070,0005) VR=CS VM=1 Graphic Annotation Units</summary>
		public readonly static DicomTag GraphicAnnotationUnits = new DicomTag(0x0070, 0x0005);

		///<summary>(0070,0006) VR=ST VM=1 Unformatted Text Value</summary>
		public readonly static DicomTag UnformattedTextValue = new DicomTag(0x0070, 0x0006);

		///<summary>(0070,0008) VR=SQ VM=1 Text Object Sequence</summary>
		public readonly static DicomTag TextObjectSequence = new DicomTag(0x0070, 0x0008);

		///<summary>(0070,0009) VR=SQ VM=1 Graphic Object Sequence</summary>
		public readonly static DicomTag GraphicObjectSequence = new DicomTag(0x0070, 0x0009);

		///<summary>(0070,0010) VR=FL VM=2 Bounding Box Top Left Hand Corner</summary>
		public readonly static DicomTag BoundingBoxTopLeftHandCorner = new DicomTag(0x0070, 0x0010);

		///<summary>(0070,0011) VR=FL VM=2 Bounding Box Bottom Right Hand Corner</summary>
		public readonly static DicomTag BoundingBoxBottomRightHandCorner = new DicomTag(0x0070, 0x0011);

		///<summary>(0070,0012) VR=CS VM=1 Bounding Box Text Horizontal Justification</summary>
		public readonly static DicomTag BoundingBoxTextHorizontalJustification = new DicomTag(0x0070, 0x0012);

		///<summary>(0070,0014) VR=FL VM=2 Anchor Point</summary>
		public readonly static DicomTag AnchorPoint = new DicomTag(0x0070, 0x0014);

		///<summary>(0070,0015) VR=CS VM=1 Anchor Point Visibility</summary>
		public readonly static DicomTag AnchorPointVisibility = new DicomTag(0x0070, 0x0015);

		///<summary>(0070,0020) VR=US VM=1 Graphic Dimensions</summary>
		public readonly static DicomTag GraphicDimensions = new DicomTag(0x0070, 0x0020);

		///<summary>(0070,0021) VR=US VM=1 Number of Graphic Points</summary>
		public readonly static DicomTag NumberOfGraphicPoints = new DicomTag(0x0070, 0x0021);

		///<summary>(0070,0022) VR=FL VM=2-n Graphic Data</summary>
		public readonly static DicomTag GraphicData = new DicomTag(0x0070, 0x0022);

		///<summary>(0070,0023) VR=CS VM=1 Graphic Type</summary>
		public readonly static DicomTag GraphicType = new DicomTag(0x0070, 0x0023);

		///<summary>(0070,0024) VR=CS VM=1 Graphic Filled</summary>
		public readonly static DicomTag GraphicFilled = new DicomTag(0x0070, 0x0024);

		///<summary>(0070,0040) VR=IS VM=1 Image Rotation (Retired) (RETIRED)</summary>
		public readonly static DicomTag ImageRotationRETIRED = new DicomTag(0x0070, 0x0040);

		///<summary>(0070,0041) VR=CS VM=1 Image Horizontal Flip</summary>
		public readonly static DicomTag ImageHorizontalFlip = new DicomTag(0x0070, 0x0041);

		///<summary>(0070,0042) VR=US VM=1 Image Rotation</summary>
		public readonly static DicomTag ImageRotation = new DicomTag(0x0070, 0x0042);

		///<summary>(0070,0050) VR=US VM=2 Displayed Area Top Left Hand Corner (Trial) (RETIRED)</summary>
		public readonly static DicomTag DisplayedAreaTopLeftHandCornerTrialRETIRED = new DicomTag(0x0070, 0x0050);

		///<summary>(0070,0051) VR=US VM=2 Displayed Area Bottom Right Hand Corner (Trial) (RETIRED)</summary>
		public readonly static DicomTag DisplayedAreaBottomRightHandCornerTrialRETIRED = new DicomTag(0x0070, 0x0051);

		///<summary>(0070,0052) VR=SL VM=2 Displayed Area Top Left Hand Corner</summary>
		public readonly static DicomTag DisplayedAreaTopLeftHandCorner = new DicomTag(0x0070, 0x0052);

		///<summary>(0070,0053) VR=SL VM=2 Displayed Area Bottom Right Hand Corner</summary>
		public readonly static DicomTag DisplayedAreaBottomRightHandCorner = new DicomTag(0x0070, 0x0053);

		///<summary>(0070,005a) VR=SQ VM=1 Displayed Area Selection Sequence</summary>
		public readonly static DicomTag DisplayedAreaSelectionSequence = new DicomTag(0x0070, 0x005a);

		///<summary>(0070,0060) VR=SQ VM=1 Graphic Layer Sequence</summary>
		public readonly static DicomTag GraphicLayerSequence = new DicomTag(0x0070, 0x0060);

		///<summary>(0070,0062) VR=IS VM=1 Graphic Layer Order</summary>
		public readonly static DicomTag GraphicLayerOrder = new DicomTag(0x0070, 0x0062);

		///<summary>(0070,0066) VR=US VM=1 Graphic Layer Recommended Display Grayscale Value</summary>
		public readonly static DicomTag GraphicLayerRecommendedDisplayGrayscaleValue = new DicomTag(0x0070, 0x0066);

		///<summary>(0070,0067) VR=US VM=3 Graphic Layer Recommended Display RGB Value (RETIRED)</summary>
		public readonly static DicomTag GraphicLayerRecommendedDisplayRGBValueRETIRED = new DicomTag(0x0070, 0x0067);

		///<summary>(0070,0068) VR=LO VM=1 Graphic Layer Description</summary>
		public readonly static DicomTag GraphicLayerDescription = new DicomTag(0x0070, 0x0068);

		///<summary>(0070,0080) VR=CS VM=1 Content Label</summary>
		public readonly static DicomTag ContentLabel = new DicomTag(0x0070, 0x0080);

		///<summary>(0070,0081) VR=LO VM=1 Content Description</summary>
		public readonly static DicomTag ContentDescription = new DicomTag(0x0070, 0x0081);

		///<summary>(0070,0082) VR=DA VM=1 Presentation Creation Date</summary>
		public readonly static DicomTag PresentationCreationDate = new DicomTag(0x0070, 0x0082);

		///<summary>(0070,0083) VR=TM VM=1 Presentation Creation Time</summary>
		public readonly static DicomTag PresentationCreationTime = new DicomTag(0x0070, 0x0083);

		///<summary>(0070,0084) VR=PN VM=1 Content Creator’s Name</summary>
		public readonly static DicomTag ContentCreatorName = new DicomTag(0x0070, 0x0084);

		///<summary>(0070,0086) VR=SQ VM=1 Content Creator’s Identification Code Sequence</summary>
		public readonly static DicomTag ContentCreatorIdentificationCodeSequence = new DicomTag(0x0070, 0x0086);

		///<summary>(0070,0087) VR=SQ VM=1 Alternate Content Description Sequence</summary>
		public readonly static DicomTag AlternateContentDescriptionSequence = new DicomTag(0x0070, 0x0087);

		///<summary>(0070,0100) VR=CS VM=1 Presentation Size Mode</summary>
		public readonly static DicomTag PresentationSizeMode = new DicomTag(0x0070, 0x0100);

		///<summary>(0070,0101) VR=DS VM=2 Presentation Pixel Spacing</summary>
		public readonly static DicomTag PresentationPixelSpacing = new DicomTag(0x0070, 0x0101);

		///<summary>(0070,0102) VR=IS VM=2 Presentation Pixel Aspect Ratio</summary>
		public readonly static DicomTag PresentationPixelAspectRatio = new DicomTag(0x0070, 0x0102);

		///<summary>(0070,0103) VR=FL VM=1 Presentation Pixel Magnification Ratio</summary>
		public readonly static DicomTag PresentationPixelMagnificationRatio = new DicomTag(0x0070, 0x0103);

		///<summary>(0070,0207) VR=LO VM=1 Graphic Group Label</summary>
		public readonly static DicomTag GraphicGroupLabel = new DicomTag(0x0070, 0x0207);

		///<summary>(0070,0208) VR=ST VM=1 Graphic Group Description</summary>
		public readonly static DicomTag GraphicGroupDescription = new DicomTag(0x0070, 0x0208);

		///<summary>(0070,0209) VR=SQ VM=1 Compound Graphic Sequence</summary>
		public readonly static DicomTag CompoundGraphicSequence = new DicomTag(0x0070, 0x0209);

		///<summary>(0070,0226) VR=UL VM=1 Compound Graphic Instance ID</summary>
		public readonly static DicomTag CompoundGraphicInstanceID = new DicomTag(0x0070, 0x0226);

		///<summary>(0070,0227) VR=LO VM=1 Font Name</summary>
		public readonly static DicomTag FontName = new DicomTag(0x0070, 0x0227);

		///<summary>(0070,0228) VR=CS VM=1 Font Name Type</summary>
		public readonly static DicomTag FontNameType = new DicomTag(0x0070, 0x0228);

		///<summary>(0070,0229) VR=LO VM=1 CSS Font Name</summary>
		public readonly static DicomTag CSSFontName = new DicomTag(0x0070, 0x0229);

		///<summary>(0070,0230) VR=FD VM=1 Rotation Angle</summary>
		public readonly static DicomTag RotationAngle = new DicomTag(0x0070, 0x0230);

		///<summary>(0070,0231) VR=SQ VM=1 Text Style Sequence</summary>
		public readonly static DicomTag TextStyleSequence = new DicomTag(0x0070, 0x0231);

		///<summary>(0070,0232) VR=SQ VM=1 Line Style Sequence</summary>
		public readonly static DicomTag LineStyleSequence = new DicomTag(0x0070, 0x0232);

		///<summary>(0070,0233) VR=SQ VM=1 Fill Style Sequence</summary>
		public readonly static DicomTag FillStyleSequence = new DicomTag(0x0070, 0x0233);

		///<summary>(0070,0234) VR=SQ VM=1 Graphic Group Sequence</summary>
		public readonly static DicomTag GraphicGroupSequence = new DicomTag(0x0070, 0x0234);

		///<summary>(0070,0241) VR=US VM=3 Text Color CIELab Value</summary>
		public readonly static DicomTag TextColorCIELabValue = new DicomTag(0x0070, 0x0241);

		///<summary>(0070,0242) VR=CS VM=1 Horizontal Alignment</summary>
		public readonly static DicomTag HorizontalAlignment = new DicomTag(0x0070, 0x0242);

		///<summary>(0070,0243) VR=CS VM=1 Vertical Alignment</summary>
		public readonly static DicomTag VerticalAlignment = new DicomTag(0x0070, 0x0243);

		///<summary>(0070,0244) VR=CS VM=1 Shadow Style</summary>
		public readonly static DicomTag ShadowStyle = new DicomTag(0x0070, 0x0244);

		///<summary>(0070,0245) VR=FL VM=1 Shadow Offset X</summary>
		public readonly static DicomTag ShadowOffsetX = new DicomTag(0x0070, 0x0245);

		///<summary>(0070,0246) VR=FL VM=1 Shadow Offset Y</summary>
		public readonly static DicomTag ShadowOffsetY = new DicomTag(0x0070, 0x0246);

		///<summary>(0070,0247) VR=US VM=3 Shadow Color CIELab Value</summary>
		public readonly static DicomTag ShadowColorCIELabValue = new DicomTag(0x0070, 0x0247);

		///<summary>(0070,0248) VR=CS VM=1 Underlined</summary>
		public readonly static DicomTag Underlined = new DicomTag(0x0070, 0x0248);

		///<summary>(0070,0249) VR=CS VM=1 Bold</summary>
		public readonly static DicomTag Bold = new DicomTag(0x0070, 0x0249);

		///<summary>(0070,0250) VR=CS VM=1 Italic</summary>
		public readonly static DicomTag Italic = new DicomTag(0x0070, 0x0250);

		///<summary>(0070,0251) VR=US VM=3 Pattern On Color CIELab Value</summary>
		public readonly static DicomTag PatternOnColorCIELabValue = new DicomTag(0x0070, 0x0251);

		///<summary>(0070,0252) VR=US VM=3 Pattern Off Color CIELab Value</summary>
		public readonly static DicomTag PatternOffColorCIELabValue = new DicomTag(0x0070, 0x0252);

		///<summary>(0070,0253) VR=FL VM=1 Line Thickness</summary>
		public readonly static DicomTag LineThickness = new DicomTag(0x0070, 0x0253);

		///<summary>(0070,0254) VR=CS VM=1 Line Dashing Style</summary>
		public readonly static DicomTag LineDashingStyle = new DicomTag(0x0070, 0x0254);

		///<summary>(0070,0255) VR=UL VM=1 Line Pattern</summary>
		public readonly static DicomTag LinePattern = new DicomTag(0x0070, 0x0255);

		///<summary>(0070,0256) VR=OB VM=1 Fill Pattern</summary>
		public readonly static DicomTag FillPattern = new DicomTag(0x0070, 0x0256);

		///<summary>(0070,0257) VR=CS VM=1 Fill Mode</summary>
		public readonly static DicomTag FillMode = new DicomTag(0x0070, 0x0257);

		///<summary>(0070,0258) VR=FL VM=1 Shadow Opacity</summary>
		public readonly static DicomTag ShadowOpacity = new DicomTag(0x0070, 0x0258);

		///<summary>(0070,0261) VR=FL VM=1 Gap Length</summary>
		public readonly static DicomTag GapLength = new DicomTag(0x0070, 0x0261);

		///<summary>(0070,0262) VR=FL VM=1 Diameter of Visibility</summary>
		public readonly static DicomTag DiameterOfVisibility = new DicomTag(0x0070, 0x0262);

		///<summary>(0070,0273) VR=FL VM=2 Rotation Point</summary>
		public readonly static DicomTag RotationPoint = new DicomTag(0x0070, 0x0273);

		///<summary>(0070,0274) VR=CS VM=1 Tick Alignment</summary>
		public readonly static DicomTag TickAlignment = new DicomTag(0x0070, 0x0274);

		///<summary>(0070,0278) VR=CS VM=1 Show Tick Label</summary>
		public readonly static DicomTag ShowTickLabel = new DicomTag(0x0070, 0x0278);

		///<summary>(0070,0279) VR=CS VM=1 Tick Label Alignment</summary>
		public readonly static DicomTag TickLabelAlignment = new DicomTag(0x0070, 0x0279);

		///<summary>(0070,0282) VR=CS VM=1 Compound Graphic Units</summary>
		public readonly static DicomTag CompoundGraphicUnits = new DicomTag(0x0070, 0x0282);

		///<summary>(0070,0284) VR=FL VM=1 Pattern On Opacity</summary>
		public readonly static DicomTag PatternOnOpacity = new DicomTag(0x0070, 0x0284);

		///<summary>(0070,0285) VR=FL VM=1 Pattern Off Opacity</summary>
		public readonly static DicomTag PatternOffOpacity = new DicomTag(0x0070, 0x0285);

		///<summary>(0070,0287) VR=SQ VM=1 Major Ticks Sequence</summary>
		public readonly static DicomTag MajorTicksSequence = new DicomTag(0x0070, 0x0287);

		///<summary>(0070,0288) VR=FL VM=1 Tick Position</summary>
		public readonly static DicomTag TickPosition = new DicomTag(0x0070, 0x0288);

		///<summary>(0070,0289) VR=SH VM=1 Tick Label</summary>
		public readonly static DicomTag TickLabel = new DicomTag(0x0070, 0x0289);

		///<summary>(0070,0294) VR=CS VM=1 Compound Graphic Type</summary>
		public readonly static DicomTag CompoundGraphicType = new DicomTag(0x0070, 0x0294);

		///<summary>(0070,0295) VR=UL VM=1 Graphic Group ID</summary>
		public readonly static DicomTag GraphicGroupID = new DicomTag(0x0070, 0x0295);

		///<summary>(0070,0306) VR=CS VM=1 Shape Type</summary>
		public readonly static DicomTag ShapeType = new DicomTag(0x0070, 0x0306);

		///<summary>(0070,0308) VR=SQ VM=1 Registration Sequence</summary>
		public readonly static DicomTag RegistrationSequence = new DicomTag(0x0070, 0x0308);

		///<summary>(0070,0309) VR=SQ VM=1 Matrix Registration Sequence</summary>
		public readonly static DicomTag MatrixRegistrationSequence = new DicomTag(0x0070, 0x0309);

		///<summary>(0070,030a) VR=SQ VM=1 Matrix Sequence</summary>
		public readonly static DicomTag MatrixSequence = new DicomTag(0x0070, 0x030a);

		///<summary>(0070,030c) VR=CS VM=1 Frame of Reference Transformation Matrix Type</summary>
		public readonly static DicomTag FrameOfReferenceTransformationMatrixType = new DicomTag(0x0070, 0x030c);

		///<summary>(0070,030d) VR=SQ VM=1 Registration Type Code Sequence</summary>
		public readonly static DicomTag RegistrationTypeCodeSequence = new DicomTag(0x0070, 0x030d);

		///<summary>(0070,030f) VR=ST VM=1 Fiducial Description</summary>
		public readonly static DicomTag FiducialDescription = new DicomTag(0x0070, 0x030f);

		///<summary>(0070,0310) VR=SH VM=1 Fiducial Identifier</summary>
		public readonly static DicomTag FiducialIdentifier = new DicomTag(0x0070, 0x0310);

		///<summary>(0070,0311) VR=SQ VM=1 Fiducial Identifier Code Sequence</summary>
		public readonly static DicomTag FiducialIdentifierCodeSequence = new DicomTag(0x0070, 0x0311);

		///<summary>(0070,0312) VR=FD VM=1 Contour Uncertainty Radius</summary>
		public readonly static DicomTag ContourUncertaintyRadius = new DicomTag(0x0070, 0x0312);

		///<summary>(0070,0314) VR=SQ VM=1 Used Fiducials Sequence</summary>
		public readonly static DicomTag UsedFiducialsSequence = new DicomTag(0x0070, 0x0314);

		///<summary>(0070,0318) VR=SQ VM=1 Graphic Coordinates Data Sequence</summary>
		public readonly static DicomTag GraphicCoordinatesDataSequence = new DicomTag(0x0070, 0x0318);

		///<summary>(0070,031a) VR=UI VM=1 Fiducial UID</summary>
		public readonly static DicomTag FiducialUID = new DicomTag(0x0070, 0x031a);

		///<summary>(0070,031c) VR=SQ VM=1 Fiducial Set Sequence</summary>
		public readonly static DicomTag FiducialSetSequence = new DicomTag(0x0070, 0x031c);

		///<summary>(0070,031e) VR=SQ VM=1 Fiducial Sequence</summary>
		public readonly static DicomTag FiducialSequence = new DicomTag(0x0070, 0x031e);

		///<summary>(0070,0401) VR=US VM=3 Graphic Layer Recommended Display CIELab Value</summary>
		public readonly static DicomTag GraphicLayerRecommendedDisplayCIELabValue = new DicomTag(0x0070, 0x0401);

		///<summary>(0070,0402) VR=SQ VM=1 Blending Sequence</summary>
		public readonly static DicomTag BlendingSequence = new DicomTag(0x0070, 0x0402);

		///<summary>(0070,0403) VR=FL VM=1 Relative Opacity</summary>
		public readonly static DicomTag RelativeOpacity = new DicomTag(0x0070, 0x0403);

		///<summary>(0070,0404) VR=SQ VM=1 Referenced Spatial Registration Sequence</summary>
		public readonly static DicomTag ReferencedSpatialRegistrationSequence = new DicomTag(0x0070, 0x0404);

		///<summary>(0070,0405) VR=CS VM=1 Blending Position</summary>
		public readonly static DicomTag BlendingPosition = new DicomTag(0x0070, 0x0405);

		///<summary>(0072,0002) VR=SH VM=1 Hanging Protocol Name</summary>
		public readonly static DicomTag HangingProtocolName = new DicomTag(0x0072, 0x0002);

		///<summary>(0072,0004) VR=LO VM=1 Hanging Protocol Description</summary>
		public readonly static DicomTag HangingProtocolDescription = new DicomTag(0x0072, 0x0004);

		///<summary>(0072,0006) VR=CS VM=1 Hanging Protocol Level</summary>
		public readonly static DicomTag HangingProtocolLevel = new DicomTag(0x0072, 0x0006);

		///<summary>(0072,0008) VR=LO VM=1 Hanging Protocol Creator</summary>
		public readonly static DicomTag HangingProtocolCreator = new DicomTag(0x0072, 0x0008);

		///<summary>(0072,000a) VR=DT VM=1 Hanging Protocol Creation DateTime</summary>
		public readonly static DicomTag HangingProtocolCreationDateTime = new DicomTag(0x0072, 0x000a);

		///<summary>(0072,000c) VR=SQ VM=1 Hanging Protocol Definition Sequence</summary>
		public readonly static DicomTag HangingProtocolDefinitionSequence = new DicomTag(0x0072, 0x000c);

		///<summary>(0072,000e) VR=SQ VM=1 Hanging Protocol User Identification Code Sequence</summary>
		public readonly static DicomTag HangingProtocolUserIdentificationCodeSequence = new DicomTag(0x0072, 0x000e);

		///<summary>(0072,0010) VR=LO VM=1 Hanging Protocol User Group Name</summary>
		public readonly static DicomTag HangingProtocolUserGroupName = new DicomTag(0x0072, 0x0010);

		///<summary>(0072,0012) VR=SQ VM=1 Source Hanging Protocol Sequence</summary>
		public readonly static DicomTag SourceHangingProtocolSequence = new DicomTag(0x0072, 0x0012);

		///<summary>(0072,0014) VR=US VM=1 Number of Priors Referenced</summary>
		public readonly static DicomTag NumberOfPriorsReferenced = new DicomTag(0x0072, 0x0014);

		///<summary>(0072,0020) VR=SQ VM=1 Image Sets Sequence</summary>
		public readonly static DicomTag ImageSetsSequence = new DicomTag(0x0072, 0x0020);

		///<summary>(0072,0022) VR=SQ VM=1 Image Set Selector Sequence</summary>
		public readonly static DicomTag ImageSetSelectorSequence = new DicomTag(0x0072, 0x0022);

		///<summary>(0072,0024) VR=CS VM=1 Image Set Selector Usage Flag</summary>
		public readonly static DicomTag ImageSetSelectorUsageFlag = new DicomTag(0x0072, 0x0024);

		///<summary>(0072,0026) VR=AT VM=1 Selector Attribute</summary>
		public readonly static DicomTag SelectorAttribute = new DicomTag(0x0072, 0x0026);

		///<summary>(0072,0028) VR=US VM=1 Selector Value Number</summary>
		public readonly static DicomTag SelectorValueNumber = new DicomTag(0x0072, 0x0028);

		///<summary>(0072,0030) VR=SQ VM=1 Time Based Image Sets Sequence</summary>
		public readonly static DicomTag TimeBasedImageSetsSequence = new DicomTag(0x0072, 0x0030);

		///<summary>(0072,0032) VR=US VM=1 Image Set Number</summary>
		public readonly static DicomTag ImageSetNumber = new DicomTag(0x0072, 0x0032);

		///<summary>(0072,0034) VR=CS VM=1 Image Set Selector Category</summary>
		public readonly static DicomTag ImageSetSelectorCategory = new DicomTag(0x0072, 0x0034);

		///<summary>(0072,0038) VR=US VM=2 Relative Time</summary>
		public readonly static DicomTag RelativeTime = new DicomTag(0x0072, 0x0038);

		///<summary>(0072,003a) VR=CS VM=1 Relative Time Units</summary>
		public readonly static DicomTag RelativeTimeUnits = new DicomTag(0x0072, 0x003a);

		///<summary>(0072,003c) VR=SS VM=2 Abstract Prior Value</summary>
		public readonly static DicomTag AbstractPriorValue = new DicomTag(0x0072, 0x003c);

		///<summary>(0072,003e) VR=SQ VM=1 Abstract Prior Code Sequence</summary>
		public readonly static DicomTag AbstractPriorCodeSequence = new DicomTag(0x0072, 0x003e);

		///<summary>(0072,0040) VR=LO VM=1 Image Set Label</summary>
		public readonly static DicomTag ImageSetLabel = new DicomTag(0x0072, 0x0040);

		///<summary>(0072,0050) VR=CS VM=1 Selector Attribute VR</summary>
		public readonly static DicomTag SelectorAttributeVR = new DicomTag(0x0072, 0x0050);

		///<summary>(0072,0052) VR=AT VM=1-n Selector Sequence Pointer</summary>
		public readonly static DicomTag SelectorSequencePointer = new DicomTag(0x0072, 0x0052);

		///<summary>(0072,0054) VR=LO VM=1-n Selector Sequence Pointer Private Creator</summary>
		public readonly static DicomTag SelectorSequencePointerPrivateCreator = new DicomTag(0x0072, 0x0054);

		///<summary>(0072,0056) VR=LO VM=1 Selector Attribute Private Creator</summary>
		public readonly static DicomTag SelectorAttributePrivateCreator = new DicomTag(0x0072, 0x0056);

		///<summary>(0072,0060) VR=AT VM=1-n Selector AT Value</summary>
		public readonly static DicomTag SelectorATValue = new DicomTag(0x0072, 0x0060);

		///<summary>(0072,0062) VR=CS VM=1-n Selector CS Value</summary>
		public readonly static DicomTag SelectorCSValue = new DicomTag(0x0072, 0x0062);

		///<summary>(0072,0064) VR=IS VM=1-n Selector IS Value</summary>
		public readonly static DicomTag SelectorISValue = new DicomTag(0x0072, 0x0064);

		///<summary>(0072,0066) VR=LO VM=1-n Selector LO Value</summary>
		public readonly static DicomTag SelectorLOValue = new DicomTag(0x0072, 0x0066);

		///<summary>(0072,0068) VR=LT VM=1 Selector LT Value</summary>
		public readonly static DicomTag SelectorLTValue = new DicomTag(0x0072, 0x0068);

		///<summary>(0072,006a) VR=PN VM=1-n Selector PN Value</summary>
		public readonly static DicomTag SelectorPNValue = new DicomTag(0x0072, 0x006a);

		///<summary>(0072,006c) VR=SH VM=1-n Selector SH Value</summary>
		public readonly static DicomTag SelectorSHValue = new DicomTag(0x0072, 0x006c);

		///<summary>(0072,006e) VR=ST VM=1 Selector ST Value</summary>
		public readonly static DicomTag SelectorSTValue = new DicomTag(0x0072, 0x006e);

		///<summary>(0072,0070) VR=UT VM=1 Selector UT Value</summary>
		public readonly static DicomTag SelectorUTValue = new DicomTag(0x0072, 0x0070);

		///<summary>(0072,0072) VR=DS VM=1-n Selector DS Value</summary>
		public readonly static DicomTag SelectorDSValue = new DicomTag(0x0072, 0x0072);

		///<summary>(0072,0074) VR=FD VM=1-n Selector FD Value</summary>
		public readonly static DicomTag SelectorFDValue = new DicomTag(0x0072, 0x0074);

		///<summary>(0072,0076) VR=FL VM=1-n Selector FL Value</summary>
		public readonly static DicomTag SelectorFLValue = new DicomTag(0x0072, 0x0076);

		///<summary>(0072,0078) VR=UL VM=1-n Selector UL Value</summary>
		public readonly static DicomTag SelectorULValue = new DicomTag(0x0072, 0x0078);

		///<summary>(0072,007a) VR=US VM=1-n Selector US Value</summary>
		public readonly static DicomTag SelectorUSValue = new DicomTag(0x0072, 0x007a);

		///<summary>(0072,007c) VR=SL VM=1-n Selector SL Value</summary>
		public readonly static DicomTag SelectorSLValue = new DicomTag(0x0072, 0x007c);

		///<summary>(0072,007e) VR=SS VM=1-n Selector SS Value</summary>
		public readonly static DicomTag SelectorSSValue = new DicomTag(0x0072, 0x007e);

		///<summary>(0072,0080) VR=SQ VM=1 Selector Code Sequence Value</summary>
		public readonly static DicomTag SelectorCodeSequenceValue = new DicomTag(0x0072, 0x0080);

		///<summary>(0072,0100) VR=US VM=1 Number of Screens</summary>
		public readonly static DicomTag NumberOfScreens = new DicomTag(0x0072, 0x0100);

		///<summary>(0072,0102) VR=SQ VM=1 Nominal Screen Definition Sequence</summary>
		public readonly static DicomTag NominalScreenDefinitionSequence = new DicomTag(0x0072, 0x0102);

		///<summary>(0072,0104) VR=US VM=1 Number of Vertical Pixels</summary>
		public readonly static DicomTag NumberOfVerticalPixels = new DicomTag(0x0072, 0x0104);

		///<summary>(0072,0106) VR=US VM=1 Number of Horizontal Pixels</summary>
		public readonly static DicomTag NumberOfHorizontalPixels = new DicomTag(0x0072, 0x0106);

		///<summary>(0072,0108) VR=FD VM=4 Display Environment Spatial Position</summary>
		public readonly static DicomTag DisplayEnvironmentSpatialPosition = new DicomTag(0x0072, 0x0108);

		///<summary>(0072,010a) VR=US VM=1 Screen Minimum Grayscale Bit Depth</summary>
		public readonly static DicomTag ScreenMinimumGrayscaleBitDepth = new DicomTag(0x0072, 0x010a);

		///<summary>(0072,010c) VR=US VM=1 Screen Minimum Color Bit Depth</summary>
		public readonly static DicomTag ScreenMinimumColorBitDepth = new DicomTag(0x0072, 0x010c);

		///<summary>(0072,010e) VR=US VM=1 Application Maximum Repaint Time</summary>
		public readonly static DicomTag ApplicationMaximumRepaintTime = new DicomTag(0x0072, 0x010e);

		///<summary>(0072,0200) VR=SQ VM=1 Display Sets Sequence</summary>
		public readonly static DicomTag DisplaySetsSequence = new DicomTag(0x0072, 0x0200);

		///<summary>(0072,0202) VR=US VM=1 Display Set Number</summary>
		public readonly static DicomTag DisplaySetNumber = new DicomTag(0x0072, 0x0202);

		///<summary>(0072,0203) VR=LO VM=1 Display Set Label</summary>
		public readonly static DicomTag DisplaySetLabel = new DicomTag(0x0072, 0x0203);

		///<summary>(0072,0204) VR=US VM=1 Display Set Presentation Group</summary>
		public readonly static DicomTag DisplaySetPresentationGroup = new DicomTag(0x0072, 0x0204);

		///<summary>(0072,0206) VR=LO VM=1 Display Set Presentation Group Description</summary>
		public readonly static DicomTag DisplaySetPresentationGroupDescription = new DicomTag(0x0072, 0x0206);

		///<summary>(0072,0208) VR=CS VM=1 Partial Data Display Handling</summary>
		public readonly static DicomTag PartialDataDisplayHandling = new DicomTag(0x0072, 0x0208);

		///<summary>(0072,0210) VR=SQ VM=1 Synchronized Scrolling Sequence</summary>
		public readonly static DicomTag SynchronizedScrollingSequence = new DicomTag(0x0072, 0x0210);

		///<summary>(0072,0212) VR=US VM=2-n Display Set Scrolling Group</summary>
		public readonly static DicomTag DisplaySetScrollingGroup = new DicomTag(0x0072, 0x0212);

		///<summary>(0072,0214) VR=SQ VM=1 Navigation Indicator Sequence</summary>
		public readonly static DicomTag NavigationIndicatorSequence = new DicomTag(0x0072, 0x0214);

		///<summary>(0072,0216) VR=US VM=1 Navigation Display Set</summary>
		public readonly static DicomTag NavigationDisplaySet = new DicomTag(0x0072, 0x0216);

		///<summary>(0072,0218) VR=US VM=1-n Reference Display Sets</summary>
		public readonly static DicomTag ReferenceDisplaySets = new DicomTag(0x0072, 0x0218);

		///<summary>(0072,0300) VR=SQ VM=1 Image Boxes Sequence</summary>
		public readonly static DicomTag ImageBoxesSequence = new DicomTag(0x0072, 0x0300);

		///<summary>(0072,0302) VR=US VM=1 Image Box Number</summary>
		public readonly static DicomTag ImageBoxNumber = new DicomTag(0x0072, 0x0302);

		///<summary>(0072,0304) VR=CS VM=1 Image Box Layout Type</summary>
		public readonly static DicomTag ImageBoxLayoutType = new DicomTag(0x0072, 0x0304);

		///<summary>(0072,0306) VR=US VM=1 Image Box Tile Horizontal Dimension</summary>
		public readonly static DicomTag ImageBoxTileHorizontalDimension = new DicomTag(0x0072, 0x0306);

		///<summary>(0072,0308) VR=US VM=1 Image Box Tile Vertical Dimension</summary>
		public readonly static DicomTag ImageBoxTileVerticalDimension = new DicomTag(0x0072, 0x0308);

		///<summary>(0072,0310) VR=CS VM=1 Image Box Scroll Direction</summary>
		public readonly static DicomTag ImageBoxScrollDirection = new DicomTag(0x0072, 0x0310);

		///<summary>(0072,0312) VR=CS VM=1 Image Box Small Scroll Type</summary>
		public readonly static DicomTag ImageBoxSmallScrollType = new DicomTag(0x0072, 0x0312);

		///<summary>(0072,0314) VR=US VM=1 Image Box Small Scroll Amount</summary>
		public readonly static DicomTag ImageBoxSmallScrollAmount = new DicomTag(0x0072, 0x0314);

		///<summary>(0072,0316) VR=CS VM=1 Image Box Large Scroll Type</summary>
		public readonly static DicomTag ImageBoxLargeScrollType = new DicomTag(0x0072, 0x0316);

		///<summary>(0072,0318) VR=US VM=1 Image Box Large Scroll Amount</summary>
		public readonly static DicomTag ImageBoxLargeScrollAmount = new DicomTag(0x0072, 0x0318);

		///<summary>(0072,0320) VR=US VM=1 Image Box Overlap Priority</summary>
		public readonly static DicomTag ImageBoxOverlapPriority = new DicomTag(0x0072, 0x0320);

		///<summary>(0072,0330) VR=FD VM=1 Cine Relative to Real-Time</summary>
		public readonly static DicomTag CineRelativeToRealTime = new DicomTag(0x0072, 0x0330);

		///<summary>(0072,0400) VR=SQ VM=1 Filter Operations Sequence</summary>
		public readonly static DicomTag FilterOperationsSequence = new DicomTag(0x0072, 0x0400);

		///<summary>(0072,0402) VR=CS VM=1 Filter-by Category</summary>
		public readonly static DicomTag FilterByCategory = new DicomTag(0x0072, 0x0402);

		///<summary>(0072,0404) VR=CS VM=1 Filter-by Attribute Presence</summary>
		public readonly static DicomTag FilterByAttributePresence = new DicomTag(0x0072, 0x0404);

		///<summary>(0072,0406) VR=CS VM=1 Filter-by Operator</summary>
		public readonly static DicomTag FilterByOperator = new DicomTag(0x0072, 0x0406);

		///<summary>(0072,0420) VR=US VM=3 Structured Display Background CIELab Value</summary>
		public readonly static DicomTag StructuredDisplayBackgroundCIELabValue = new DicomTag(0x0072, 0x0420);

		///<summary>(0072,0421) VR=US VM=3 Empty Image Box CIELab Value</summary>
		public readonly static DicomTag EmptyImageBoxCIELabValue = new DicomTag(0x0072, 0x0421);

		///<summary>(0072,0422) VR=SQ VM=1 Structured Display Image Box Sequence</summary>
		public readonly static DicomTag StructuredDisplayImageBoxSequence = new DicomTag(0x0072, 0x0422);

		///<summary>(0072,0424) VR=SQ VM=1 Structured Display Text Box Sequence</summary>
		public readonly static DicomTag StructuredDisplayTextBoxSequence = new DicomTag(0x0072, 0x0424);

		///<summary>(0072,0427) VR=SQ VM=1 Referenced First Frame Sequence</summary>
		public readonly static DicomTag ReferencedFirstFrameSequence = new DicomTag(0x0072, 0x0427);

		///<summary>(0072,0430) VR=SQ VM=1 Image Box Synchronization Sequence</summary>
		public readonly static DicomTag ImageBoxSynchronizationSequence = new DicomTag(0x0072, 0x0430);

		///<summary>(0072,0432) VR=US VM=2-n Synchronized Image Box List</summary>
		public readonly static DicomTag SynchronizedImageBoxList = new DicomTag(0x0072, 0x0432);

		///<summary>(0072,0434) VR=CS VM=1 Type of Synchronization</summary>
		public readonly static DicomTag TypeOfSynchronization = new DicomTag(0x0072, 0x0434);

		///<summary>(0072,0500) VR=CS VM=1 Blending Operation Type</summary>
		public readonly static DicomTag BlendingOperationType = new DicomTag(0x0072, 0x0500);

		///<summary>(0072,0510) VR=CS VM=1 Reformatting Operation Type</summary>
		public readonly static DicomTag ReformattingOperationType = new DicomTag(0x0072, 0x0510);

		///<summary>(0072,0512) VR=FD VM=1 Reformatting Thickness</summary>
		public readonly static DicomTag ReformattingThickness = new DicomTag(0x0072, 0x0512);

		///<summary>(0072,0514) VR=FD VM=1 Reformatting Interval</summary>
		public readonly static DicomTag ReformattingInterval = new DicomTag(0x0072, 0x0514);

		///<summary>(0072,0516) VR=CS VM=1 Reformatting Operation Initial View Direction</summary>
		public readonly static DicomTag ReformattingOperationInitialViewDirection = new DicomTag(0x0072, 0x0516);

		///<summary>(0072,0520) VR=CS VM=1-n 3D Rendering Type</summary>
		public readonly static DicomTag ThreeDRenderingType = new DicomTag(0x0072, 0x0520);

		///<summary>(0072,0600) VR=SQ VM=1 Sorting Operations Sequence</summary>
		public readonly static DicomTag SortingOperationsSequence = new DicomTag(0x0072, 0x0600);

		///<summary>(0072,0602) VR=CS VM=1 Sort-by Category</summary>
		public readonly static DicomTag SortByCategory = new DicomTag(0x0072, 0x0602);

		///<summary>(0072,0604) VR=CS VM=1 Sorting Direction</summary>
		public readonly static DicomTag SortingDirection = new DicomTag(0x0072, 0x0604);

		///<summary>(0072,0700) VR=CS VM=2 Display Set Patient Orientation</summary>
		public readonly static DicomTag DisplaySetPatientOrientation = new DicomTag(0x0072, 0x0700);

		///<summary>(0072,0702) VR=CS VM=1 VOI Type</summary>
		public readonly static DicomTag VOIType = new DicomTag(0x0072, 0x0702);

		///<summary>(0072,0704) VR=CS VM=1 Pseudo-Color Type</summary>
		public readonly static DicomTag PseudoColorType = new DicomTag(0x0072, 0x0704);

		///<summary>(0072,0705) VR=SQ VM=1 Pseudo-Color Palette Instance Reference Sequence</summary>
		public readonly static DicomTag PseudoColorPaletteInstanceReferenceSequence = new DicomTag(0x0072, 0x0705);

		///<summary>(0072,0706) VR=CS VM=1 Show Grayscale Inverted</summary>
		public readonly static DicomTag ShowGrayscaleInverted = new DicomTag(0x0072, 0x0706);

		///<summary>(0072,0710) VR=CS VM=1 Show Image True Size Flag</summary>
		public readonly static DicomTag ShowImageTrueSizeFlag = new DicomTag(0x0072, 0x0710);

		///<summary>(0072,0712) VR=CS VM=1 Show Graphic Annotation Flag</summary>
		public readonly static DicomTag ShowGraphicAnnotationFlag = new DicomTag(0x0072, 0x0712);

		///<summary>(0072,0714) VR=CS VM=1 Show Patient Demographics Flag</summary>
		public readonly static DicomTag ShowPatientDemographicsFlag = new DicomTag(0x0072, 0x0714);

		///<summary>(0072,0716) VR=CS VM=1 Show Acquisition Techniques Flag</summary>
		public readonly static DicomTag ShowAcquisitionTechniquesFlag = new DicomTag(0x0072, 0x0716);

		///<summary>(0072,0717) VR=CS VM=1 Display Set Horizontal Justification</summary>
		public readonly static DicomTag DisplaySetHorizontalJustification = new DicomTag(0x0072, 0x0717);

		///<summary>(0072,0718) VR=CS VM=1 Display Set Vertical Justification</summary>
		public readonly static DicomTag DisplaySetVerticalJustification = new DicomTag(0x0072, 0x0718);

		///<summary>(0074,0120) VR=FD VM=1 Continuation Start Meterset</summary>
		public readonly static DicomTag ContinuationStartMeterset = new DicomTag(0x0074, 0x0120);

		///<summary>(0074,0121) VR=FD VM=1 Continuation End Meterset</summary>
		public readonly static DicomTag ContinuationEndMeterset = new DicomTag(0x0074, 0x0121);

		///<summary>(0074,1000) VR=CS VM=1 Procedure Step State</summary>
		public readonly static DicomTag ProcedureStepState = new DicomTag(0x0074, 0x1000);

		///<summary>(0074,1002) VR=SQ VM=1 Procedure Step Progress Information Sequence</summary>
		public readonly static DicomTag ProcedureStepProgressInformationSequence = new DicomTag(0x0074, 0x1002);

		///<summary>(0074,1004) VR=DS VM=1 Procedure Step Progress</summary>
		public readonly static DicomTag ProcedureStepProgress = new DicomTag(0x0074, 0x1004);

		///<summary>(0074,1006) VR=ST VM=1 Procedure Step Progress Description</summary>
		public readonly static DicomTag ProcedureStepProgressDescription = new DicomTag(0x0074, 0x1006);

		///<summary>(0074,1008) VR=SQ VM=1 Procedure Step Communications URI Sequence</summary>
		public readonly static DicomTag ProcedureStepCommunicationsURISequence = new DicomTag(0x0074, 0x1008);

		///<summary>(0074,100a) VR=ST VM=1 Contact URI</summary>
		public readonly static DicomTag ContactURI = new DicomTag(0x0074, 0x100a);

		///<summary>(0074,100c) VR=LO VM=1 Contact Display Name</summary>
		public readonly static DicomTag ContactDisplayName = new DicomTag(0x0074, 0x100c);

		///<summary>(0074,100e) VR=SQ VM=1 Procedure Step Discontinuation Reason Code Sequence</summary>
		public readonly static DicomTag ProcedureStepDiscontinuationReasonCodeSequence = new DicomTag(0x0074, 0x100e);

		///<summary>(0074,1020) VR=SQ VM=1 Beam Task Sequence</summary>
		public readonly static DicomTag BeamTaskSequence = new DicomTag(0x0074, 0x1020);

		///<summary>(0074,1022) VR=CS VM=1 Beam Task Type</summary>
		public readonly static DicomTag BeamTaskType = new DicomTag(0x0074, 0x1022);

		///<summary>(0074,1024) VR=IS VM=1 Beam Order Index (Trial) (RETIRED)</summary>
		public readonly static DicomTag BeamOrderIndexTrialRETIRED = new DicomTag(0x0074, 0x1024);

		///<summary>(0074,1026) VR=FD VM=1 Table Top Vertical Adjusted Position</summary>
		public readonly static DicomTag TableTopVerticalAdjustedPosition = new DicomTag(0x0074, 0x1026);

		///<summary>(0074,1027) VR=FD VM=1 Table Top Longitudinal Adjusted Position</summary>
		public readonly static DicomTag TableTopLongitudinalAdjustedPosition = new DicomTag(0x0074, 0x1027);

		///<summary>(0074,1028) VR=FD VM=1 Table Top Lateral Adjusted Position</summary>
		public readonly static DicomTag TableTopLateralAdjustedPosition = new DicomTag(0x0074, 0x1028);

		///<summary>(0074,102a) VR=FD VM=1 Patient Support Adjusted Angle</summary>
		public readonly static DicomTag PatientSupportAdjustedAngle = new DicomTag(0x0074, 0x102a);

		///<summary>(0074,102b) VR=FD VM=1 Table Top Eccentric Adjusted Angle</summary>
		public readonly static DicomTag TableTopEccentricAdjustedAngle = new DicomTag(0x0074, 0x102b);

		///<summary>(0074,102c) VR=FD VM=1 Table Top Pitch Adjusted Angle</summary>
		public readonly static DicomTag TableTopPitchAdjustedAngle = new DicomTag(0x0074, 0x102c);

		///<summary>(0074,102d) VR=FD VM=1 Table Top Roll Adjusted Angle</summary>
		public readonly static DicomTag TableTopRollAdjustedAngle = new DicomTag(0x0074, 0x102d);

		///<summary>(0074,1030) VR=SQ VM=1 Delivery Verification Image Sequence</summary>
		public readonly static DicomTag DeliveryVerificationImageSequence = new DicomTag(0x0074, 0x1030);

		///<summary>(0074,1032) VR=CS VM=1 Verification Image Timing</summary>
		public readonly static DicomTag VerificationImageTiming = new DicomTag(0x0074, 0x1032);

		///<summary>(0074,1034) VR=CS VM=1 Double Exposure Flag</summary>
		public readonly static DicomTag DoubleExposureFlag = new DicomTag(0x0074, 0x1034);

		///<summary>(0074,1036) VR=CS VM=1 Double Exposure Ordering</summary>
		public readonly static DicomTag DoubleExposureOrdering = new DicomTag(0x0074, 0x1036);

		///<summary>(0074,1038) VR=DS VM=1 Double Exposure Meterset (Trial) (RETIRED)</summary>
		public readonly static DicomTag DoubleExposureMetersetTrialRETIRED = new DicomTag(0x0074, 0x1038);

		///<summary>(0074,103a) VR=DS VM=4 Double Exposure Field Delta (Trial) (RETIRED)</summary>
		public readonly static DicomTag DoubleExposureFieldDeltaTrialRETIRED = new DicomTag(0x0074, 0x103a);

		///<summary>(0074,1040) VR=SQ VM=1 Related Reference RT Image Sequence</summary>
		public readonly static DicomTag RelatedReferenceRTImageSequence = new DicomTag(0x0074, 0x1040);

		///<summary>(0074,1042) VR=SQ VM=1 General Machine Verification Sequence</summary>
		public readonly static DicomTag GeneralMachineVerificationSequence = new DicomTag(0x0074, 0x1042);

		///<summary>(0074,1044) VR=SQ VM=1 Conventional Machine Verification Sequence</summary>
		public readonly static DicomTag ConventionalMachineVerificationSequence = new DicomTag(0x0074, 0x1044);

		///<summary>(0074,1046) VR=SQ VM=1 Ion Machine Verification Sequence</summary>
		public readonly static DicomTag IonMachineVerificationSequence = new DicomTag(0x0074, 0x1046);

		///<summary>(0074,1048) VR=SQ VM=1 Failed Attributes Sequence</summary>
		public readonly static DicomTag FailedAttributesSequence = new DicomTag(0x0074, 0x1048);

		///<summary>(0074,104a) VR=SQ VM=1 Overridden Attributes Sequence</summary>
		public readonly static DicomTag OverriddenAttributesSequence = new DicomTag(0x0074, 0x104a);

		///<summary>(0074,104c) VR=SQ VM=1 Conventional Control Point Verification Sequence</summary>
		public readonly static DicomTag ConventionalControlPointVerificationSequence = new DicomTag(0x0074, 0x104c);

		///<summary>(0074,104e) VR=SQ VM=1 Ion Control Point Verification Sequence</summary>
		public readonly static DicomTag IonControlPointVerificationSequence = new DicomTag(0x0074, 0x104e);

		///<summary>(0074,1050) VR=SQ VM=1 Attribute Occurrence Sequence</summary>
		public readonly static DicomTag AttributeOccurrenceSequence = new DicomTag(0x0074, 0x1050);

		///<summary>(0074,1052) VR=AT VM=1 Attribute Occurrence Pointer</summary>
		public readonly static DicomTag AttributeOccurrencePointer = new DicomTag(0x0074, 0x1052);

		///<summary>(0074,1054) VR=UL VM=1 Attribute Item Selector</summary>
		public readonly static DicomTag AttributeItemSelector = new DicomTag(0x0074, 0x1054);

		///<summary>(0074,1056) VR=LO VM=1 Attribute Occurrence Private Creator</summary>
		public readonly static DicomTag AttributeOccurrencePrivateCreator = new DicomTag(0x0074, 0x1056);

		///<summary>(0074,1057) VR=IS VM=1-n Selector Sequence Pointer Items</summary>
		public readonly static DicomTag SelectorSequencePointerItems = new DicomTag(0x0074, 0x1057);

		///<summary>(0074,1200) VR=CS VM=1 Scheduled Procedure Step Priority</summary>
		public readonly static DicomTag ScheduledProcedureStepPriority = new DicomTag(0x0074, 0x1200);

		///<summary>(0074,1202) VR=LO VM=1 Worklist Label</summary>
		public readonly static DicomTag WorklistLabel = new DicomTag(0x0074, 0x1202);

		///<summary>(0074,1204) VR=LO VM=1 Procedure Step Label</summary>
		public readonly static DicomTag ProcedureStepLabel = new DicomTag(0x0074, 0x1204);

		///<summary>(0074,1210) VR=SQ VM=1 Scheduled Processing Parameters Sequence</summary>
		public readonly static DicomTag ScheduledProcessingParametersSequence = new DicomTag(0x0074, 0x1210);

		///<summary>(0074,1212) VR=SQ VM=1 Performed Processing Parameters Sequence</summary>
		public readonly static DicomTag PerformedProcessingParametersSequence = new DicomTag(0x0074, 0x1212);

		///<summary>(0074,1216) VR=SQ VM=1 Unified Procedure Step Performed Procedure Sequence</summary>
		public readonly static DicomTag UnifiedProcedureStepPerformedProcedureSequence = new DicomTag(0x0074, 0x1216);

		///<summary>(0074,1220) VR=SQ VM=1 Related Procedure Step Sequence (RETIRED)</summary>
		public readonly static DicomTag RelatedProcedureStepSequenceRETIRED = new DicomTag(0x0074, 0x1220);

		///<summary>(0074,1222) VR=LO VM=1 Procedure Step Relationship Type (RETIRED)</summary>
		public readonly static DicomTag ProcedureStepRelationshipTypeRETIRED = new DicomTag(0x0074, 0x1222);

		///<summary>(0074,1224) VR=SQ VM=1 Replaced Procedure Step Sequence</summary>
		public readonly static DicomTag ReplacedProcedureStepSequence = new DicomTag(0x0074, 0x1224);

		///<summary>(0074,1230) VR=LO VM=1 Deletion Lock</summary>
		public readonly static DicomTag DeletionLock = new DicomTag(0x0074, 0x1230);

		///<summary>(0074,1234) VR=AE VM=1 Receiving AE</summary>
		public readonly static DicomTag ReceivingAE = new DicomTag(0x0074, 0x1234);

		///<summary>(0074,1236) VR=AE VM=1 Requesting AE</summary>
		public readonly static DicomTag RequestingAE = new DicomTag(0x0074, 0x1236);

		///<summary>(0074,1238) VR=LT VM=1 Reason for Cancellation</summary>
		public readonly static DicomTag ReasonForCancellation = new DicomTag(0x0074, 0x1238);

		///<summary>(0074,1242) VR=CS VM=1 SCP Status</summary>
		public readonly static DicomTag SCPStatus = new DicomTag(0x0074, 0x1242);

		///<summary>(0074,1244) VR=CS VM=1 Subscription List Status</summary>
		public readonly static DicomTag SubscriptionListStatus = new DicomTag(0x0074, 0x1244);

		///<summary>(0074,1246) VR=CS VM=1 Unified Procedure Step List Status</summary>
		public readonly static DicomTag UnifiedProcedureStepListStatus = new DicomTag(0x0074, 0x1246);

		///<summary>(0074,1324) VR=UL VM=1 Beam Order Index</summary>
		public readonly static DicomTag BeamOrderIndex = new DicomTag(0x0074, 0x1324);

		///<summary>(0074,1338) VR=FD VM=1 Double Exposure Meterset</summary>
		public readonly static DicomTag DoubleExposureMeterset = new DicomTag(0x0074, 0x1338);

		///<summary>(0074,133a) VR=FD VM=4 Double Exposure Field Delta</summary>
		public readonly static DicomTag DoubleExposureFieldDelta = new DicomTag(0x0074, 0x133a);

		///<summary>(0076,0001) VR=LO VM=1 Implant Assembly Template Name</summary>
		public readonly static DicomTag ImplantAssemblyTemplateName = new DicomTag(0x0076, 0x0001);

		///<summary>(0076,0003) VR=LO VM=1 Implant Assembly Template Issuer</summary>
		public readonly static DicomTag ImplantAssemblyTemplateIssuer = new DicomTag(0x0076, 0x0003);

		///<summary>(0076,0006) VR=LO VM=1 Implant Assembly Template Version</summary>
		public readonly static DicomTag ImplantAssemblyTemplateVersion = new DicomTag(0x0076, 0x0006);

		///<summary>(0076,0008) VR=SQ VM=1 Replaced Implant Assembly Template Sequence</summary>
		public readonly static DicomTag ReplacedImplantAssemblyTemplateSequence = new DicomTag(0x0076, 0x0008);

		///<summary>(0076,000a) VR=CS VM=1 Implant Assembly Template Type</summary>
		public readonly static DicomTag ImplantAssemblyTemplateType = new DicomTag(0x0076, 0x000a);

		///<summary>(0076,000c) VR=SQ VM=1 Original Implant Assembly Template Sequence</summary>
		public readonly static DicomTag OriginalImplantAssemblyTemplateSequence = new DicomTag(0x0076, 0x000c);

		///<summary>(0076,000e) VR=SQ VM=1 Derivation Implant Assembly Template Sequence</summary>
		public readonly static DicomTag DerivationImplantAssemblyTemplateSequence = new DicomTag(0x0076, 0x000e);

		///<summary>(0076,0010) VR=SQ VM=1 Implant Assembly Template Target Anatomy Sequence</summary>
		public readonly static DicomTag ImplantAssemblyTemplateTargetAnatomySequence = new DicomTag(0x0076, 0x0010);

		///<summary>(0076,0020) VR=SQ VM=1 Procedure Type Code Sequence</summary>
		public readonly static DicomTag ProcedureTypeCodeSequence = new DicomTag(0x0076, 0x0020);

		///<summary>(0076,0030) VR=LO VM=1 Surgical Technique</summary>
		public readonly static DicomTag SurgicalTechnique = new DicomTag(0x0076, 0x0030);

		///<summary>(0076,0032) VR=SQ VM=1 Component Types Sequence</summary>
		public readonly static DicomTag ComponentTypesSequence = new DicomTag(0x0076, 0x0032);

		///<summary>(0076,0034) VR=CS VM=1 Component Type Code Sequence</summary>
		public readonly static DicomTag ComponentTypeCodeSequence = new DicomTag(0x0076, 0x0034);

		///<summary>(0076,0036) VR=CS VM=1 Exclusive Component Type</summary>
		public readonly static DicomTag ExclusiveComponentType = new DicomTag(0x0076, 0x0036);

		///<summary>(0076,0038) VR=CS VM=1 Mandatory Component Type</summary>
		public readonly static DicomTag MandatoryComponentType = new DicomTag(0x0076, 0x0038);

		///<summary>(0076,0040) VR=SQ VM=1 Component Sequence</summary>
		public readonly static DicomTag ComponentSequence = new DicomTag(0x0076, 0x0040);

		///<summary>(0076,0055) VR=US VM=1 Component ID</summary>
		public readonly static DicomTag ComponentID = new DicomTag(0x0076, 0x0055);

		///<summary>(0076,0060) VR=SQ VM=1 Component Assembly Sequence</summary>
		public readonly static DicomTag ComponentAssemblySequence = new DicomTag(0x0076, 0x0060);

		///<summary>(0076,0070) VR=US VM=1 Component 1 Referenced ID</summary>
		public readonly static DicomTag Component1ReferencedID = new DicomTag(0x0076, 0x0070);

		///<summary>(0076,0080) VR=US VM=1 Component 1 Referenced Mating Feature Set ID</summary>
		public readonly static DicomTag Component1ReferencedMatingFeatureSetID = new DicomTag(0x0076, 0x0080);

		///<summary>(0076,0090) VR=US VM=1 Component 1 Referenced Mating Feature ID</summary>
		public readonly static DicomTag Component1ReferencedMatingFeatureID = new DicomTag(0x0076, 0x0090);

		///<summary>(0076,00a0) VR=US VM=1 Component 2 Referenced ID</summary>
		public readonly static DicomTag Component2ReferencedID = new DicomTag(0x0076, 0x00a0);

		///<summary>(0076,00b0) VR=US VM=1 Component 2 Referenced Mating Feature Set ID</summary>
		public readonly static DicomTag Component2ReferencedMatingFeatureSetID = new DicomTag(0x0076, 0x00b0);

		///<summary>(0076,00c0) VR=US VM=1 Component 2 Referenced Mating Feature ID</summary>
		public readonly static DicomTag Component2ReferencedMatingFeatureID = new DicomTag(0x0076, 0x00c0);

		///<summary>(0078,0001) VR=LO VM=1 Implant Template Group Name</summary>
		public readonly static DicomTag ImplantTemplateGroupName = new DicomTag(0x0078, 0x0001);

		///<summary>(0078,0010) VR=ST VM=1 Implant Template Group Description</summary>
		public readonly static DicomTag ImplantTemplateGroupDescription = new DicomTag(0x0078, 0x0010);

		///<summary>(0078,0020) VR=LO VM=1 Implant Template Group Issuer</summary>
		public readonly static DicomTag ImplantTemplateGroupIssuer = new DicomTag(0x0078, 0x0020);

		///<summary>(0078,0024) VR=LO VM=1 Implant Template Group Version</summary>
		public readonly static DicomTag ImplantTemplateGroupVersion = new DicomTag(0x0078, 0x0024);

		///<summary>(0078,0026) VR=SQ VM=1 Replaced Implant Template Group Sequence</summary>
		public readonly static DicomTag ReplacedImplantTemplateGroupSequence = new DicomTag(0x0078, 0x0026);

		///<summary>(0078,0028) VR=SQ VM=1 Implant Template Group Target Anatomy Sequence</summary>
		public readonly static DicomTag ImplantTemplateGroupTargetAnatomySequence = new DicomTag(0x0078, 0x0028);

		///<summary>(0078,002a) VR=SQ VM=1 Implant Template Group Members Sequence</summary>
		public readonly static DicomTag ImplantTemplateGroupMembersSequence = new DicomTag(0x0078, 0x002a);

		///<summary>(0078,002e) VR=US VM=1 Implant Template Group Member ID</summary>
		public readonly static DicomTag ImplantTemplateGroupMemberID = new DicomTag(0x0078, 0x002e);

		///<summary>(0078,0050) VR=FD VM=3 3D Implant Template Group Member Matching Point</summary>
		public readonly static DicomTag ThreeDImplantTemplateGroupMemberMatchingPoint = new DicomTag(0x0078, 0x0050);

		///<summary>(0078,0060) VR=FD VM=9 3D Implant Template Group Member Matching Axes</summary>
		public readonly static DicomTag ThreeDImplantTemplateGroupMemberMatchingAxes = new DicomTag(0x0078, 0x0060);

		///<summary>(0078,0070) VR=SQ VM=1 Implant Template Group Member Matching 2D Coordinates Sequence</summary>
		public readonly static DicomTag ImplantTemplateGroupMemberMatching2DCoordinatesSequence = new DicomTag(0x0078, 0x0070);

		///<summary>(0078,0090) VR=FD VM=2 2D Implant Template Group Member Matching Point</summary>
		public readonly static DicomTag TwoDImplantTemplateGroupMemberMatchingPoint = new DicomTag(0x0078, 0x0090);

		///<summary>(0078,00a0) VR=FD VM=4 2D Implant Template Group Member Matching Axes</summary>
		public readonly static DicomTag TwoDImplantTemplateGroupMemberMatchingAxes = new DicomTag(0x0078, 0x00a0);

		///<summary>(0078,00b0) VR=SQ VM=1 Implant Template Group Variation Dimension Sequence</summary>
		public readonly static DicomTag ImplantTemplateGroupVariationDimensionSequence = new DicomTag(0x0078, 0x00b0);

		///<summary>(0078,00b2) VR=LO VM=1 Implant Template Group Variation Dimension Name</summary>
		public readonly static DicomTag ImplantTemplateGroupVariationDimensionName = new DicomTag(0x0078, 0x00b2);

		///<summary>(0078,00b4) VR=SQ VM=1 Implant Template Group Variation Dimension Rank Sequence</summary>
		public readonly static DicomTag ImplantTemplateGroupVariationDimensionRankSequence = new DicomTag(0x0078, 0x00b4);

		///<summary>(0078,00b6) VR=US VM=1 Referenced Implant Template Group Member ID</summary>
		public readonly static DicomTag ReferencedImplantTemplateGroupMemberID = new DicomTag(0x0078, 0x00b6);

		///<summary>(0078,00b8) VR=US VM=1 Implant Template Group Variation Dimension Rank</summary>
		public readonly static DicomTag ImplantTemplateGroupVariationDimensionRank = new DicomTag(0x0078, 0x00b8);

		///<summary>(0088,0130) VR=SH VM=1 Storage Media File-set ID</summary>
		public readonly static DicomTag StorageMediaFileSetID = new DicomTag(0x0088, 0x0130);

		///<summary>(0088,0140) VR=UI VM=1 Storage Media File-set UID</summary>
		public readonly static DicomTag StorageMediaFileSetUID = new DicomTag(0x0088, 0x0140);

		///<summary>(0088,0200) VR=SQ VM=1 Icon Image Sequence</summary>
		public readonly static DicomTag IconImageSequence = new DicomTag(0x0088, 0x0200);

		///<summary>(0088,0904) VR=LO VM=1 Topic Title (RETIRED)</summary>
		public readonly static DicomTag TopicTitleRETIRED = new DicomTag(0x0088, 0x0904);

		///<summary>(0088,0906) VR=ST VM=1 Topic Subject (RETIRED)</summary>
		public readonly static DicomTag TopicSubjectRETIRED = new DicomTag(0x0088, 0x0906);

		///<summary>(0088,0910) VR=LO VM=1 Topic Author (RETIRED)</summary>
		public readonly static DicomTag TopicAuthorRETIRED = new DicomTag(0x0088, 0x0910);

		///<summary>(0088,0912) VR=LO VM=1-32 Topic Keywords (RETIRED)</summary>
		public readonly static DicomTag TopicKeywordsRETIRED = new DicomTag(0x0088, 0x0912);

		///<summary>(0100,0410) VR=CS VM=1 SOP Instance Status</summary>
		public readonly static DicomTag SOPInstanceStatus = new DicomTag(0x0100, 0x0410);

		///<summary>(0100,0420) VR=DT VM=1 SOP Authorization DateTime</summary>
		public readonly static DicomTag SOPAuthorizationDateTime = new DicomTag(0x0100, 0x0420);

		///<summary>(0100,0424) VR=LT VM=1 SOP Authorization Comment</summary>
		public readonly static DicomTag SOPAuthorizationComment = new DicomTag(0x0100, 0x0424);

		///<summary>(0100,0426) VR=LO VM=1 Authorization Equipment Certification Number</summary>
		public readonly static DicomTag AuthorizationEquipmentCertificationNumber = new DicomTag(0x0100, 0x0426);

		///<summary>(0400,0005) VR=US VM=1 MAC ID Number</summary>
		public readonly static DicomTag MACIDNumber = new DicomTag(0x0400, 0x0005);

		///<summary>(0400,0010) VR=UI VM=1 MAC Calculation Transfer Syntax UID</summary>
		public readonly static DicomTag MACCalculationTransferSyntaxUID = new DicomTag(0x0400, 0x0010);

		///<summary>(0400,0015) VR=CS VM=1 MAC Algorithm</summary>
		public readonly static DicomTag MACAlgorithm = new DicomTag(0x0400, 0x0015);

		///<summary>(0400,0020) VR=AT VM=1-n Data Elements Signed</summary>
		public readonly static DicomTag DataElementsSigned = new DicomTag(0x0400, 0x0020);

		///<summary>(0400,0100) VR=UI VM=1 Digital Signature UID</summary>
		public readonly static DicomTag DigitalSignatureUID = new DicomTag(0x0400, 0x0100);

		///<summary>(0400,0105) VR=DT VM=1 Digital Signature DateTime</summary>
		public readonly static DicomTag DigitalSignatureDateTime = new DicomTag(0x0400, 0x0105);

		///<summary>(0400,0110) VR=CS VM=1 Certificate Type</summary>
		public readonly static DicomTag CertificateType = new DicomTag(0x0400, 0x0110);

		///<summary>(0400,0115) VR=OB VM=1 Certificate of Signer</summary>
		public readonly static DicomTag CertificateOfSigner = new DicomTag(0x0400, 0x0115);

		///<summary>(0400,0120) VR=OB VM=1 Signature</summary>
		public readonly static DicomTag Signature = new DicomTag(0x0400, 0x0120);

		///<summary>(0400,0305) VR=CS VM=1 Certified Timestamp Type</summary>
		public readonly static DicomTag CertifiedTimestampType = new DicomTag(0x0400, 0x0305);

		///<summary>(0400,0310) VR=OB VM=1 Certified Timestamp</summary>
		public readonly static DicomTag CertifiedTimestamp = new DicomTag(0x0400, 0x0310);

		///<summary>(0400,0401) VR=SQ VM=1 Digital Signature Purpose Code Sequence</summary>
		public readonly static DicomTag DigitalSignaturePurposeCodeSequence = new DicomTag(0x0400, 0x0401);

		///<summary>(0400,0402) VR=SQ VM=1 Referenced Digital Signature Sequence</summary>
		public readonly static DicomTag ReferencedDigitalSignatureSequence = new DicomTag(0x0400, 0x0402);

		///<summary>(0400,0403) VR=SQ VM=1 Referenced SOP Instance MAC Sequence</summary>
		public readonly static DicomTag ReferencedSOPInstanceMACSequence = new DicomTag(0x0400, 0x0403);

		///<summary>(0400,0404) VR=OB VM=1 MAC</summary>
		public readonly static DicomTag MAC = new DicomTag(0x0400, 0x0404);

		///<summary>(0400,0500) VR=SQ VM=1 Encrypted Attributes Sequence</summary>
		public readonly static DicomTag EncryptedAttributesSequence = new DicomTag(0x0400, 0x0500);

		///<summary>(0400,0510) VR=UI VM=1 Encrypted Content Transfer Syntax UID</summary>
		public readonly static DicomTag EncryptedContentTransferSyntaxUID = new DicomTag(0x0400, 0x0510);

		///<summary>(0400,0520) VR=OB VM=1 Encrypted Content</summary>
		public readonly static DicomTag EncryptedContent = new DicomTag(0x0400, 0x0520);

		///<summary>(0400,0550) VR=SQ VM=1 Modified Attributes Sequence</summary>
		public readonly static DicomTag ModifiedAttributesSequence = new DicomTag(0x0400, 0x0550);

		///<summary>(0400,0561) VR=SQ VM=1 Original Attributes Sequence</summary>
		public readonly static DicomTag OriginalAttributesSequence = new DicomTag(0x0400, 0x0561);

		///<summary>(0400,0562) VR=DT VM=1 Attribute Modification DateTime</summary>
		public readonly static DicomTag AttributeModificationDateTime = new DicomTag(0x0400, 0x0562);

		///<summary>(0400,0563) VR=LO VM=1 Modifying System</summary>
		public readonly static DicomTag ModifyingSystem = new DicomTag(0x0400, 0x0563);

		///<summary>(0400,0564) VR=LO VM=1 Source of Previous Values</summary>
		public readonly static DicomTag SourceOfPreviousValues = new DicomTag(0x0400, 0x0564);

		///<summary>(0400,0565) VR=CS VM=1 Reason for the Attribute Modification</summary>
		public readonly static DicomTag ReasonForTheAttributeModification = new DicomTag(0x0400, 0x0565);

		///<summary>(2000,0010) VR=IS VM=1 Number of Copies</summary>
		public readonly static DicomTag NumberOfCopies = new DicomTag(0x2000, 0x0010);

		///<summary>(2000,001e) VR=SQ VM=1 Printer Configuration Sequence</summary>
		public readonly static DicomTag PrinterConfigurationSequence = new DicomTag(0x2000, 0x001e);

		///<summary>(2000,0020) VR=CS VM=1 Print Priority</summary>
		public readonly static DicomTag PrintPriority = new DicomTag(0x2000, 0x0020);

		///<summary>(2000,0030) VR=CS VM=1 Medium Type</summary>
		public readonly static DicomTag MediumType = new DicomTag(0x2000, 0x0030);

		///<summary>(2000,0040) VR=CS VM=1 Film Destination</summary>
		public readonly static DicomTag FilmDestination = new DicomTag(0x2000, 0x0040);

		///<summary>(2000,0050) VR=LO VM=1 Film Session Label</summary>
		public readonly static DicomTag FilmSessionLabel = new DicomTag(0x2000, 0x0050);

		///<summary>(2000,0060) VR=IS VM=1 Memory Allocation</summary>
		public readonly static DicomTag MemoryAllocation = new DicomTag(0x2000, 0x0060);

		///<summary>(2000,0061) VR=IS VM=1 Maximum Memory Allocation</summary>
		public readonly static DicomTag MaximumMemoryAllocation = new DicomTag(0x2000, 0x0061);

		///<summary>(2000,0062) VR=CS VM=1 Color Image Printing Flag (RETIRED)</summary>
		public readonly static DicomTag ColorImagePrintingFlagRETIRED = new DicomTag(0x2000, 0x0062);

		///<summary>(2000,0063) VR=CS VM=1 Collation Flag (RETIRED)</summary>
		public readonly static DicomTag CollationFlagRETIRED = new DicomTag(0x2000, 0x0063);

		///<summary>(2000,0065) VR=CS VM=1 Annotation Flag (RETIRED)</summary>
		public readonly static DicomTag AnnotationFlagRETIRED = new DicomTag(0x2000, 0x0065);

		///<summary>(2000,0067) VR=CS VM=1 Image Overlay Flag (RETIRED)</summary>
		public readonly static DicomTag ImageOverlayFlagRETIRED = new DicomTag(0x2000, 0x0067);

		///<summary>(2000,0069) VR=CS VM=1 Presentation LUT Flag (RETIRED)</summary>
		public readonly static DicomTag PresentationLUTFlagRETIRED = new DicomTag(0x2000, 0x0069);

		///<summary>(2000,006a) VR=CS VM=1 Image Box Presentation LUT Flag (RETIRED)</summary>
		public readonly static DicomTag ImageBoxPresentationLUTFlagRETIRED = new DicomTag(0x2000, 0x006a);

		///<summary>(2000,00a0) VR=US VM=1 Memory Bit Depth</summary>
		public readonly static DicomTag MemoryBitDepth = new DicomTag(0x2000, 0x00a0);

		///<summary>(2000,00a1) VR=US VM=1 Printing Bit Depth</summary>
		public readonly static DicomTag PrintingBitDepth = new DicomTag(0x2000, 0x00a1);

		///<summary>(2000,00a2) VR=SQ VM=1 Media Installed Sequence</summary>
		public readonly static DicomTag MediaInstalledSequence = new DicomTag(0x2000, 0x00a2);

		///<summary>(2000,00a4) VR=SQ VM=1 Other Media Available Sequence</summary>
		public readonly static DicomTag OtherMediaAvailableSequence = new DicomTag(0x2000, 0x00a4);

		///<summary>(2000,00a8) VR=SQ VM=1 Supported Image Display Formats Sequence</summary>
		public readonly static DicomTag SupportedImageDisplayFormatsSequence = new DicomTag(0x2000, 0x00a8);

		///<summary>(2000,0500) VR=SQ VM=1 Referenced Film Box Sequence</summary>
		public readonly static DicomTag ReferencedFilmBoxSequence = new DicomTag(0x2000, 0x0500);

		///<summary>(2000,0510) VR=SQ VM=1 Referenced Stored Print  Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedStoredPrintSequenceRETIRED = new DicomTag(0x2000, 0x0510);

		///<summary>(2010,0010) VR=ST VM=1 Image Display Format</summary>
		public readonly static DicomTag ImageDisplayFormat = new DicomTag(0x2010, 0x0010);

		///<summary>(2010,0030) VR=CS VM=1 Annotation Display Format ID</summary>
		public readonly static DicomTag AnnotationDisplayFormatID = new DicomTag(0x2010, 0x0030);

		///<summary>(2010,0040) VR=CS VM=1 Film Orientation</summary>
		public readonly static DicomTag FilmOrientation = new DicomTag(0x2010, 0x0040);

		///<summary>(2010,0050) VR=CS VM=1 Film Size ID</summary>
		public readonly static DicomTag FilmSizeID = new DicomTag(0x2010, 0x0050);

		///<summary>(2010,0052) VR=CS VM=1 Printer Resolution ID</summary>
		public readonly static DicomTag PrinterResolutionID = new DicomTag(0x2010, 0x0052);

		///<summary>(2010,0054) VR=CS VM=1 Default Printer Resolution ID</summary>
		public readonly static DicomTag DefaultPrinterResolutionID = new DicomTag(0x2010, 0x0054);

		///<summary>(2010,0060) VR=CS VM=1 Magnification Type</summary>
		public readonly static DicomTag MagnificationType = new DicomTag(0x2010, 0x0060);

		///<summary>(2010,0080) VR=CS VM=1 Smoothing Type</summary>
		public readonly static DicomTag SmoothingType = new DicomTag(0x2010, 0x0080);

		///<summary>(2010,00a6) VR=CS VM=1 Default Magnification Type</summary>
		public readonly static DicomTag DefaultMagnificationType = new DicomTag(0x2010, 0x00a6);

		///<summary>(2010,00a7) VR=CS VM=1-n Other Magnification Types Available</summary>
		public readonly static DicomTag OtherMagnificationTypesAvailable = new DicomTag(0x2010, 0x00a7);

		///<summary>(2010,00a8) VR=CS VM=1 Default Smoothing Type</summary>
		public readonly static DicomTag DefaultSmoothingType = new DicomTag(0x2010, 0x00a8);

		///<summary>(2010,00a9) VR=CS VM=1-n Other Smoothing Types Available</summary>
		public readonly static DicomTag OtherSmoothingTypesAvailable = new DicomTag(0x2010, 0x00a9);

		///<summary>(2010,0100) VR=CS VM=1 Border Density</summary>
		public readonly static DicomTag BorderDensity = new DicomTag(0x2010, 0x0100);

		///<summary>(2010,0110) VR=CS VM=1 Empty Image Density</summary>
		public readonly static DicomTag EmptyImageDensity = new DicomTag(0x2010, 0x0110);

		///<summary>(2010,0120) VR=US VM=1 Min Density</summary>
		public readonly static DicomTag MinDensity = new DicomTag(0x2010, 0x0120);

		///<summary>(2010,0130) VR=US VM=1 Max Density</summary>
		public readonly static DicomTag MaxDensity = new DicomTag(0x2010, 0x0130);

		///<summary>(2010,0140) VR=CS VM=1 Trim</summary>
		public readonly static DicomTag Trim = new DicomTag(0x2010, 0x0140);

		///<summary>(2010,0150) VR=ST VM=1 Configuration Information</summary>
		public readonly static DicomTag ConfigurationInformation = new DicomTag(0x2010, 0x0150);

		///<summary>(2010,0152) VR=LT VM=1 Configuration Information Description</summary>
		public readonly static DicomTag ConfigurationInformationDescription = new DicomTag(0x2010, 0x0152);

		///<summary>(2010,0154) VR=IS VM=1 Maximum Collated Films</summary>
		public readonly static DicomTag MaximumCollatedFilms = new DicomTag(0x2010, 0x0154);

		///<summary>(2010,015e) VR=US VM=1 Illumination</summary>
		public readonly static DicomTag Illumination = new DicomTag(0x2010, 0x015e);

		///<summary>(2010,0160) VR=US VM=1 Reflected Ambient Light</summary>
		public readonly static DicomTag ReflectedAmbientLight = new DicomTag(0x2010, 0x0160);

		///<summary>(2010,0376) VR=DS VM=2 Printer Pixel Spacing</summary>
		public readonly static DicomTag PrinterPixelSpacing = new DicomTag(0x2010, 0x0376);

		///<summary>(2010,0500) VR=SQ VM=1 Referenced Film Session Sequence</summary>
		public readonly static DicomTag ReferencedFilmSessionSequence = new DicomTag(0x2010, 0x0500);

		///<summary>(2010,0510) VR=SQ VM=1 Referenced Image Box Sequence</summary>
		public readonly static DicomTag ReferencedImageBoxSequence = new DicomTag(0x2010, 0x0510);

		///<summary>(2010,0520) VR=SQ VM=1 Referenced Basic Annotation Box Sequence</summary>
		public readonly static DicomTag ReferencedBasicAnnotationBoxSequence = new DicomTag(0x2010, 0x0520);

		///<summary>(2020,0010) VR=US VM=1 Image Box Position</summary>
		public readonly static DicomTag ImageBoxPosition = new DicomTag(0x2020, 0x0010);

		///<summary>(2020,0020) VR=CS VM=1 Polarity</summary>
		public readonly static DicomTag Polarity = new DicomTag(0x2020, 0x0020);

		///<summary>(2020,0030) VR=DS VM=1 Requested Image Size</summary>
		public readonly static DicomTag RequestedImageSize = new DicomTag(0x2020, 0x0030);

		///<summary>(2020,0040) VR=CS VM=1 Requested Decimate/Crop Behavior</summary>
		public readonly static DicomTag RequestedDecimateCropBehavior = new DicomTag(0x2020, 0x0040);

		///<summary>(2020,0050) VR=CS VM=1 Requested Resolution ID</summary>
		public readonly static DicomTag RequestedResolutionID = new DicomTag(0x2020, 0x0050);

		///<summary>(2020,00a0) VR=CS VM=1 Requested Image Size Flag</summary>
		public readonly static DicomTag RequestedImageSizeFlag = new DicomTag(0x2020, 0x00a0);

		///<summary>(2020,00a2) VR=CS VM=1 Decimate/Crop Result</summary>
		public readonly static DicomTag DecimateCropResult = new DicomTag(0x2020, 0x00a2);

		///<summary>(2020,0110) VR=SQ VM=1 Basic Grayscale Image Sequence</summary>
		public readonly static DicomTag BasicGrayscaleImageSequence = new DicomTag(0x2020, 0x0110);

		///<summary>(2020,0111) VR=SQ VM=1 Basic Color Image Sequence</summary>
		public readonly static DicomTag BasicColorImageSequence = new DicomTag(0x2020, 0x0111);

		///<summary>(2020,0130) VR=SQ VM=1 Referenced Image Overlay Box Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedImageOverlayBoxSequenceRETIRED = new DicomTag(0x2020, 0x0130);

		///<summary>(2020,0140) VR=SQ VM=1 Referenced VOI LUT Box Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedVOILUTBoxSequenceRETIRED = new DicomTag(0x2020, 0x0140);

		///<summary>(2030,0010) VR=US VM=1 Annotation Position</summary>
		public readonly static DicomTag AnnotationPosition = new DicomTag(0x2030, 0x0010);

		///<summary>(2030,0020) VR=LO VM=1 Text String</summary>
		public readonly static DicomTag TextString = new DicomTag(0x2030, 0x0020);

		///<summary>(2040,0010) VR=SQ VM=1 Referenced Overlay Plane Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedOverlayPlaneSequenceRETIRED = new DicomTag(0x2040, 0x0010);

		///<summary>(2040,0011) VR=US VM=1-99 Referenced Overlay Plane Groups (RETIRED)</summary>
		public readonly static DicomTag ReferencedOverlayPlaneGroupsRETIRED = new DicomTag(0x2040, 0x0011);

		///<summary>(2040,0020) VR=SQ VM=1 Overlay Pixel Data Sequence (RETIRED)</summary>
		public readonly static DicomTag OverlayPixelDataSequenceRETIRED = new DicomTag(0x2040, 0x0020);

		///<summary>(2040,0060) VR=CS VM=1 Overlay Magnification Type (RETIRED)</summary>
		public readonly static DicomTag OverlayMagnificationTypeRETIRED = new DicomTag(0x2040, 0x0060);

		///<summary>(2040,0070) VR=CS VM=1 Overlay Smoothing Type (RETIRED)</summary>
		public readonly static DicomTag OverlaySmoothingTypeRETIRED = new DicomTag(0x2040, 0x0070);

		///<summary>(2040,0072) VR=CS VM=1 Overlay or Image Magnification (RETIRED)</summary>
		public readonly static DicomTag OverlayOrImageMagnificationRETIRED = new DicomTag(0x2040, 0x0072);

		///<summary>(2040,0074) VR=US VM=1 Magnify to Number of Columns (RETIRED)</summary>
		public readonly static DicomTag MagnifyToNumberOfColumnsRETIRED = new DicomTag(0x2040, 0x0074);

		///<summary>(2040,0080) VR=CS VM=1 Overlay Foreground Density (RETIRED)</summary>
		public readonly static DicomTag OverlayForegroundDensityRETIRED = new DicomTag(0x2040, 0x0080);

		///<summary>(2040,0082) VR=CS VM=1 Overlay Background Density (RETIRED)</summary>
		public readonly static DicomTag OverlayBackgroundDensityRETIRED = new DicomTag(0x2040, 0x0082);

		///<summary>(2040,0090) VR=CS VM=1 Overlay Mode (RETIRED)</summary>
		public readonly static DicomTag OverlayModeRETIRED = new DicomTag(0x2040, 0x0090);

		///<summary>(2040,0100) VR=CS VM=1 Threshold Density (RETIRED)</summary>
		public readonly static DicomTag ThresholdDensityRETIRED = new DicomTag(0x2040, 0x0100);

		///<summary>(2040,0500) VR=SQ VM=1 Referenced Image Box Sequence (Retired) (RETIRED)</summary>
		public readonly static DicomTag ReferencedImageBoxSequenceRETIRED = new DicomTag(0x2040, 0x0500);

		///<summary>(2050,0010) VR=SQ VM=1 Presentation LUT Sequence</summary>
		public readonly static DicomTag PresentationLUTSequence = new DicomTag(0x2050, 0x0010);

		///<summary>(2050,0020) VR=CS VM=1 Presentation LUT Shape</summary>
		public readonly static DicomTag PresentationLUTShape = new DicomTag(0x2050, 0x0020);

		///<summary>(2050,0500) VR=SQ VM=1 Referenced Presentation  LUT Sequence</summary>
		public readonly static DicomTag ReferencedPresentationLUTSequence = new DicomTag(0x2050, 0x0500);

		///<summary>(2100,0010) VR=SH VM=1 Print Job ID (RETIRED)</summary>
		public readonly static DicomTag PrintJobIDRETIRED = new DicomTag(0x2100, 0x0010);

		///<summary>(2100,0020) VR=CS VM=1 Execution Status</summary>
		public readonly static DicomTag ExecutionStatus = new DicomTag(0x2100, 0x0020);

		///<summary>(2100,0030) VR=CS VM=1 Execution Status Info</summary>
		public readonly static DicomTag ExecutionStatusInfo = new DicomTag(0x2100, 0x0030);

		///<summary>(2100,0040) VR=DA VM=1 Creation Date</summary>
		public readonly static DicomTag CreationDate = new DicomTag(0x2100, 0x0040);

		///<summary>(2100,0050) VR=TM VM=1 Creation Time</summary>
		public readonly static DicomTag CreationTime = new DicomTag(0x2100, 0x0050);

		///<summary>(2100,0070) VR=AE VM=1 Originator</summary>
		public readonly static DicomTag Originator = new DicomTag(0x2100, 0x0070);

		///<summary>(2100,0140) VR=AE VM=1 Destination AE (RETIRED)</summary>
		public readonly static DicomTag DestinationAERETIRED = new DicomTag(0x2100, 0x0140);

		///<summary>(2100,0160) VR=SH VM=1 Owner ID</summary>
		public readonly static DicomTag OwnerID = new DicomTag(0x2100, 0x0160);

		///<summary>(2100,0170) VR=IS VM=1 Number of Films</summary>
		public readonly static DicomTag NumberOfFilms = new DicomTag(0x2100, 0x0170);

		///<summary>(2100,0500) VR=SQ VM=1 Referenced Print Job Sequence (Pull Stored Print) (RETIRED)</summary>
		public readonly static DicomTag ReferencedPrintJobSequencePullStoredPrintRETIRED = new DicomTag(0x2100, 0x0500);

		///<summary>(2110,0010) VR=CS VM=1 Printer Status</summary>
		public readonly static DicomTag PrinterStatus = new DicomTag(0x2110, 0x0010);

		///<summary>(2110,0020) VR=CS VM=1 Printer Status Info</summary>
		public readonly static DicomTag PrinterStatusInfo = new DicomTag(0x2110, 0x0020);

		///<summary>(2110,0030) VR=LO VM=1 Printer Name</summary>
		public readonly static DicomTag PrinterName = new DicomTag(0x2110, 0x0030);

		///<summary>(2110,0099) VR=SH VM=1 Print Queue ID (RETIRED)</summary>
		public readonly static DicomTag PrintQueueIDRETIRED = new DicomTag(0x2110, 0x0099);

		///<summary>(2120,0010) VR=CS VM=1 Queue Status (RETIRED)</summary>
		public readonly static DicomTag QueueStatusRETIRED = new DicomTag(0x2120, 0x0010);

		///<summary>(2120,0050) VR=SQ VM=1 Print Job Description Sequence (RETIRED)</summary>
		public readonly static DicomTag PrintJobDescriptionSequenceRETIRED = new DicomTag(0x2120, 0x0050);

		///<summary>(2120,0070) VR=SQ VM=1 Referenced Print Job Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedPrintJobSequenceRETIRED = new DicomTag(0x2120, 0x0070);

		///<summary>(2130,0010) VR=SQ VM=1 Print Management Capabilities Sequence (RETIRED)</summary>
		public readonly static DicomTag PrintManagementCapabilitiesSequenceRETIRED = new DicomTag(0x2130, 0x0010);

		///<summary>(2130,0015) VR=SQ VM=1 Printer Characteristics Sequence (RETIRED)</summary>
		public readonly static DicomTag PrinterCharacteristicsSequenceRETIRED = new DicomTag(0x2130, 0x0015);

		///<summary>(2130,0030) VR=SQ VM=1 Film Box Content Sequence (RETIRED)</summary>
		public readonly static DicomTag FilmBoxContentSequenceRETIRED = new DicomTag(0x2130, 0x0030);

		///<summary>(2130,0040) VR=SQ VM=1 Image Box Content Sequence (RETIRED)</summary>
		public readonly static DicomTag ImageBoxContentSequenceRETIRED = new DicomTag(0x2130, 0x0040);

		///<summary>(2130,0050) VR=SQ VM=1 Annotation Content Sequence (RETIRED)</summary>
		public readonly static DicomTag AnnotationContentSequenceRETIRED = new DicomTag(0x2130, 0x0050);

		///<summary>(2130,0060) VR=SQ VM=1 Image Overlay Box Content Sequence (RETIRED)</summary>
		public readonly static DicomTag ImageOverlayBoxContentSequenceRETIRED = new DicomTag(0x2130, 0x0060);

		///<summary>(2130,0080) VR=SQ VM=1 Presentation LUT Content Sequence (RETIRED)</summary>
		public readonly static DicomTag PresentationLUTContentSequenceRETIRED = new DicomTag(0x2130, 0x0080);

		///<summary>(2130,00a0) VR=SQ VM=1 Proposed Study Sequence (RETIRED)</summary>
		public readonly static DicomTag ProposedStudySequenceRETIRED = new DicomTag(0x2130, 0x00a0);

		///<summary>(2130,00c0) VR=SQ VM=1 Original Image Sequence (RETIRED)</summary>
		public readonly static DicomTag OriginalImageSequenceRETIRED = new DicomTag(0x2130, 0x00c0);

		///<summary>(2200,0001) VR=CS VM=1 Label Using Information Extracted From Instances</summary>
		public readonly static DicomTag LabelUsingInformationExtractedFromInstances = new DicomTag(0x2200, 0x0001);

		///<summary>(2200,0002) VR=UT VM=1 Label Text</summary>
		public readonly static DicomTag LabelText = new DicomTag(0x2200, 0x0002);

		///<summary>(2200,0003) VR=CS VM=1 Label Style Selection</summary>
		public readonly static DicomTag LabelStyleSelection = new DicomTag(0x2200, 0x0003);

		///<summary>(2200,0004) VR=LT VM=1 Media Disposition</summary>
		public readonly static DicomTag MediaDisposition = new DicomTag(0x2200, 0x0004);

		///<summary>(2200,0005) VR=LT VM=1 Barcode Value</summary>
		public readonly static DicomTag BarcodeValue = new DicomTag(0x2200, 0x0005);

		///<summary>(2200,0006) VR=CS VM=1 Barcode Symbology</summary>
		public readonly static DicomTag BarcodeSymbology = new DicomTag(0x2200, 0x0006);

		///<summary>(2200,0007) VR=CS VM=1 Allow Media Splitting</summary>
		public readonly static DicomTag AllowMediaSplitting = new DicomTag(0x2200, 0x0007);

		///<summary>(2200,0008) VR=CS VM=1 Include Non-DICOM Objects</summary>
		public readonly static DicomTag IncludeNonDICOMObjects = new DicomTag(0x2200, 0x0008);

		///<summary>(2200,0009) VR=CS VM=1 Include Display Application</summary>
		public readonly static DicomTag IncludeDisplayApplication = new DicomTag(0x2200, 0x0009);

		///<summary>(2200,000a) VR=CS VM=1 Preserve Composite Instances After Media Creation</summary>
		public readonly static DicomTag PreserveCompositeInstancesAfterMediaCreation = new DicomTag(0x2200, 0x000a);

		///<summary>(2200,000b) VR=US VM=1 Total Number of Pieces of Media Created</summary>
		public readonly static DicomTag TotalNumberOfPiecesOfMediaCreated = new DicomTag(0x2200, 0x000b);

		///<summary>(2200,000c) VR=LO VM=1 Requested Media Application Profile</summary>
		public readonly static DicomTag RequestedMediaApplicationProfile = new DicomTag(0x2200, 0x000c);

		///<summary>(2200,000d) VR=SQ VM=1 Referenced Storage Media Sequence</summary>
		public readonly static DicomTag ReferencedStorageMediaSequence = new DicomTag(0x2200, 0x000d);

		///<summary>(2200,000e) VR=AT VM=1-n Failure Attributes</summary>
		public readonly static DicomTag FailureAttributes = new DicomTag(0x2200, 0x000e);

		///<summary>(2200,000f) VR=CS VM=1 Allow Lossy Compression</summary>
		public readonly static DicomTag AllowLossyCompression = new DicomTag(0x2200, 0x000f);

		///<summary>(2200,0020) VR=CS VM=1 Request Priority</summary>
		public readonly static DicomTag RequestPriority = new DicomTag(0x2200, 0x0020);

		///<summary>(3002,0002) VR=SH VM=1 RT Image Label</summary>
		public readonly static DicomTag RTImageLabel = new DicomTag(0x3002, 0x0002);

		///<summary>(3002,0003) VR=LO VM=1 RT Image Name</summary>
		public readonly static DicomTag RTImageName = new DicomTag(0x3002, 0x0003);

		///<summary>(3002,0004) VR=ST VM=1 RT Image Description</summary>
		public readonly static DicomTag RTImageDescription = new DicomTag(0x3002, 0x0004);

		///<summary>(3002,000a) VR=CS VM=1 Reported Values Origin</summary>
		public readonly static DicomTag ReportedValuesOrigin = new DicomTag(0x3002, 0x000a);

		///<summary>(3002,000c) VR=CS VM=1 RT Image Plane</summary>
		public readonly static DicomTag RTImagePlane = new DicomTag(0x3002, 0x000c);

		///<summary>(3002,000d) VR=DS VM=3 X-Ray Image Receptor Translation</summary>
		public readonly static DicomTag XRayImageReceptorTranslation = new DicomTag(0x3002, 0x000d);

		///<summary>(3002,000e) VR=DS VM=1 X-Ray Image Receptor Angle</summary>
		public readonly static DicomTag XRayImageReceptorAngle = new DicomTag(0x3002, 0x000e);

		///<summary>(3002,0010) VR=DS VM=6 RT Image Orientation</summary>
		public readonly static DicomTag RTImageOrientation = new DicomTag(0x3002, 0x0010);

		///<summary>(3002,0011) VR=DS VM=2 Image Plane Pixel Spacing</summary>
		public readonly static DicomTag ImagePlanePixelSpacing = new DicomTag(0x3002, 0x0011);

		///<summary>(3002,0012) VR=DS VM=2 RT Image Position</summary>
		public readonly static DicomTag RTImagePosition = new DicomTag(0x3002, 0x0012);

		///<summary>(3002,0020) VR=SH VM=1 Radiation Machine Name</summary>
		public readonly static DicomTag RadiationMachineName = new DicomTag(0x3002, 0x0020);

		///<summary>(3002,0022) VR=DS VM=1 Radiation Machine SAD</summary>
		public readonly static DicomTag RadiationMachineSAD = new DicomTag(0x3002, 0x0022);

		///<summary>(3002,0024) VR=DS VM=1 Radiation Machine SSD</summary>
		public readonly static DicomTag RadiationMachineSSD = new DicomTag(0x3002, 0x0024);

		///<summary>(3002,0026) VR=DS VM=1 RT Image SID</summary>
		public readonly static DicomTag RTImageSID = new DicomTag(0x3002, 0x0026);

		///<summary>(3002,0028) VR=DS VM=1 Source to Reference Object Distance</summary>
		public readonly static DicomTag SourceToReferenceObjectDistance = new DicomTag(0x3002, 0x0028);

		///<summary>(3002,0029) VR=IS VM=1 Fraction Number</summary>
		public readonly static DicomTag FractionNumber = new DicomTag(0x3002, 0x0029);

		///<summary>(3002,0030) VR=SQ VM=1 Exposure Sequence</summary>
		public readonly static DicomTag ExposureSequence = new DicomTag(0x3002, 0x0030);

		///<summary>(3002,0032) VR=DS VM=1 Meterset Exposure</summary>
		public readonly static DicomTag MetersetExposure = new DicomTag(0x3002, 0x0032);

		///<summary>(3002,0034) VR=DS VM=4 Diaphragm Position</summary>
		public readonly static DicomTag DiaphragmPosition = new DicomTag(0x3002, 0x0034);

		///<summary>(3002,0040) VR=SQ VM=1 Fluence Map Sequence</summary>
		public readonly static DicomTag FluenceMapSequence = new DicomTag(0x3002, 0x0040);

		///<summary>(3002,0041) VR=CS VM=1 Fluence Data Source</summary>
		public readonly static DicomTag FluenceDataSource = new DicomTag(0x3002, 0x0041);

		///<summary>(3002,0042) VR=DS VM=1 Fluence Data Scale</summary>
		public readonly static DicomTag FluenceDataScale = new DicomTag(0x3002, 0x0042);

		///<summary>(3002,0050) VR=SQ VM=1 Primary Fluence Mode Sequence</summary>
		public readonly static DicomTag PrimaryFluenceModeSequence = new DicomTag(0x3002, 0x0050);

		///<summary>(3002,0051) VR=CS VM=1 Fluence Mode</summary>
		public readonly static DicomTag FluenceMode = new DicomTag(0x3002, 0x0051);

		///<summary>(3002,0052) VR=SH VM=1 Fluence Mode ID</summary>
		public readonly static DicomTag FluenceModeID = new DicomTag(0x3002, 0x0052);

		///<summary>(3004,0001) VR=CS VM=1 DVH Type</summary>
		public readonly static DicomTag DVHType = new DicomTag(0x3004, 0x0001);

		///<summary>(3004,0002) VR=CS VM=1 Dose Units</summary>
		public readonly static DicomTag DoseUnits = new DicomTag(0x3004, 0x0002);

		///<summary>(3004,0004) VR=CS VM=1 Dose Type</summary>
		public readonly static DicomTag DoseType = new DicomTag(0x3004, 0x0004);

		///<summary>(3004,0006) VR=LO VM=1 Dose Comment</summary>
		public readonly static DicomTag DoseComment = new DicomTag(0x3004, 0x0006);

		///<summary>(3004,0008) VR=DS VM=3 Normalization Point</summary>
		public readonly static DicomTag NormalizationPoint = new DicomTag(0x3004, 0x0008);

		///<summary>(3004,000a) VR=CS VM=1 Dose Summation Type</summary>
		public readonly static DicomTag DoseSummationType = new DicomTag(0x3004, 0x000a);

		///<summary>(3004,000c) VR=DS VM=2-n Grid Frame Offset Vector</summary>
		public readonly static DicomTag GridFrameOffsetVector = new DicomTag(0x3004, 0x000c);

		///<summary>(3004,000e) VR=DS VM=1 Dose Grid Scaling</summary>
		public readonly static DicomTag DoseGridScaling = new DicomTag(0x3004, 0x000e);

		///<summary>(3004,0010) VR=SQ VM=1 RT Dose ROI Sequence</summary>
		public readonly static DicomTag RTDoseROISequence = new DicomTag(0x3004, 0x0010);

		///<summary>(3004,0012) VR=DS VM=1 Dose Value</summary>
		public readonly static DicomTag DoseValue = new DicomTag(0x3004, 0x0012);

		///<summary>(3004,0014) VR=CS VM=1-3 Tissue Heterogeneity Correction</summary>
		public readonly static DicomTag TissueHeterogeneityCorrection = new DicomTag(0x3004, 0x0014);

		///<summary>(3004,0040) VR=DS VM=3 DVH Normalization Point</summary>
		public readonly static DicomTag DVHNormalizationPoint = new DicomTag(0x3004, 0x0040);

		///<summary>(3004,0042) VR=DS VM=1 DVH Normalization Dose Value</summary>
		public readonly static DicomTag DVHNormalizationDoseValue = new DicomTag(0x3004, 0x0042);

		///<summary>(3004,0050) VR=SQ VM=1 DVH Sequence</summary>
		public readonly static DicomTag DVHSequence = new DicomTag(0x3004, 0x0050);

		///<summary>(3004,0052) VR=DS VM=1 DVH Dose Scaling</summary>
		public readonly static DicomTag DVHDoseScaling = new DicomTag(0x3004, 0x0052);

		///<summary>(3004,0054) VR=CS VM=1 DVH Volume Units</summary>
		public readonly static DicomTag DVHVolumeUnits = new DicomTag(0x3004, 0x0054);

		///<summary>(3004,0056) VR=IS VM=1 DVH Number of Bins</summary>
		public readonly static DicomTag DVHNumberOfBins = new DicomTag(0x3004, 0x0056);

		///<summary>(3004,0058) VR=DS VM=2-2n DVH Data</summary>
		public readonly static DicomTag DVHData = new DicomTag(0x3004, 0x0058);

		///<summary>(3004,0060) VR=SQ VM=1 DVH Referenced ROI Sequence</summary>
		public readonly static DicomTag DVHReferencedROISequence = new DicomTag(0x3004, 0x0060);

		///<summary>(3004,0062) VR=CS VM=1 DVH ROI Contribution Type</summary>
		public readonly static DicomTag DVHROIContributionType = new DicomTag(0x3004, 0x0062);

		///<summary>(3004,0070) VR=DS VM=1 DVH Minimum Dose</summary>
		public readonly static DicomTag DVHMinimumDose = new DicomTag(0x3004, 0x0070);

		///<summary>(3004,0072) VR=DS VM=1 DVH Maximum Dose</summary>
		public readonly static DicomTag DVHMaximumDose = new DicomTag(0x3004, 0x0072);

		///<summary>(3004,0074) VR=DS VM=1 DVH Mean Dose</summary>
		public readonly static DicomTag DVHMeanDose = new DicomTag(0x3004, 0x0074);

		///<summary>(3006,0002) VR=SH VM=1 Structure Set Label</summary>
		public readonly static DicomTag StructureSetLabel = new DicomTag(0x3006, 0x0002);

		///<summary>(3006,0004) VR=LO VM=1 Structure Set Name</summary>
		public readonly static DicomTag StructureSetName = new DicomTag(0x3006, 0x0004);

		///<summary>(3006,0006) VR=ST VM=1 Structure Set Description</summary>
		public readonly static DicomTag StructureSetDescription = new DicomTag(0x3006, 0x0006);

		///<summary>(3006,0008) VR=DA VM=1 Structure Set Date</summary>
		public readonly static DicomTag StructureSetDate = new DicomTag(0x3006, 0x0008);

		///<summary>(3006,0009) VR=TM VM=1 Structure Set Time</summary>
		public readonly static DicomTag StructureSetTime = new DicomTag(0x3006, 0x0009);

		///<summary>(3006,0010) VR=SQ VM=1 Referenced Frame of Reference Sequence</summary>
		public readonly static DicomTag ReferencedFrameOfReferenceSequence = new DicomTag(0x3006, 0x0010);

		///<summary>(3006,0012) VR=SQ VM=1 RT Referenced Study Sequence</summary>
		public readonly static DicomTag RTReferencedStudySequence = new DicomTag(0x3006, 0x0012);

		///<summary>(3006,0014) VR=SQ VM=1 RT Referenced Series Sequence</summary>
		public readonly static DicomTag RTReferencedSeriesSequence = new DicomTag(0x3006, 0x0014);

		///<summary>(3006,0016) VR=SQ VM=1 Contour Image Sequence</summary>
		public readonly static DicomTag ContourImageSequence = new DicomTag(0x3006, 0x0016);

		///<summary>(3006,0020) VR=SQ VM=1 Structure Set ROI Sequence</summary>
		public readonly static DicomTag StructureSetROISequence = new DicomTag(0x3006, 0x0020);

		///<summary>(3006,0022) VR=IS VM=1 ROI Number</summary>
		public readonly static DicomTag ROINumber = new DicomTag(0x3006, 0x0022);

		///<summary>(3006,0024) VR=UI VM=1 Referenced Frame of Reference UID</summary>
		public readonly static DicomTag ReferencedFrameOfReferenceUID = new DicomTag(0x3006, 0x0024);

		///<summary>(3006,0026) VR=LO VM=1 ROI Name</summary>
		public readonly static DicomTag ROIName = new DicomTag(0x3006, 0x0026);

		///<summary>(3006,0028) VR=ST VM=1 ROI Description</summary>
		public readonly static DicomTag ROIDescription = new DicomTag(0x3006, 0x0028);

		///<summary>(3006,002a) VR=IS VM=3 ROI Display Color</summary>
		public readonly static DicomTag ROIDisplayColor = new DicomTag(0x3006, 0x002a);

		///<summary>(3006,002c) VR=DS VM=1 ROI Volume</summary>
		public readonly static DicomTag ROIVolume = new DicomTag(0x3006, 0x002c);

		///<summary>(3006,0030) VR=SQ VM=1 RT Related ROI Sequence</summary>
		public readonly static DicomTag RTRelatedROISequence = new DicomTag(0x3006, 0x0030);

		///<summary>(3006,0033) VR=CS VM=1 RT ROI Relationship</summary>
		public readonly static DicomTag RTROIRelationship = new DicomTag(0x3006, 0x0033);

		///<summary>(3006,0036) VR=CS VM=1 ROI Generation Algorithm</summary>
		public readonly static DicomTag ROIGenerationAlgorithm = new DicomTag(0x3006, 0x0036);

		///<summary>(3006,0038) VR=LO VM=1 ROI Generation Description</summary>
		public readonly static DicomTag ROIGenerationDescription = new DicomTag(0x3006, 0x0038);

		///<summary>(3006,0039) VR=SQ VM=1 ROI Contour Sequence</summary>
		public readonly static DicomTag ROIContourSequence = new DicomTag(0x3006, 0x0039);

		///<summary>(3006,0040) VR=SQ VM=1 Contour Sequence</summary>
		public readonly static DicomTag ContourSequence = new DicomTag(0x3006, 0x0040);

		///<summary>(3006,0042) VR=CS VM=1 Contour Geometric Type</summary>
		public readonly static DicomTag ContourGeometricType = new DicomTag(0x3006, 0x0042);

		///<summary>(3006,0044) VR=DS VM=1 Contour Slab Thickness</summary>
		public readonly static DicomTag ContourSlabThickness = new DicomTag(0x3006, 0x0044);

		///<summary>(3006,0045) VR=DS VM=3 Contour Offset Vector</summary>
		public readonly static DicomTag ContourOffsetVector = new DicomTag(0x3006, 0x0045);

		///<summary>(3006,0046) VR=IS VM=1 Number of Contour Points</summary>
		public readonly static DicomTag NumberOfContourPoints = new DicomTag(0x3006, 0x0046);

		///<summary>(3006,0048) VR=IS VM=1 Contour Number</summary>
		public readonly static DicomTag ContourNumber = new DicomTag(0x3006, 0x0048);

		///<summary>(3006,0049) VR=IS VM=1-n Attached Contours</summary>
		public readonly static DicomTag AttachedContours = new DicomTag(0x3006, 0x0049);

		///<summary>(3006,0050) VR=DS VM=3-3n Contour Data</summary>
		public readonly static DicomTag ContourData = new DicomTag(0x3006, 0x0050);

		///<summary>(3006,0080) VR=SQ VM=1 RT ROI Observations Sequence</summary>
		public readonly static DicomTag RTROIObservationsSequence = new DicomTag(0x3006, 0x0080);

		///<summary>(3006,0082) VR=IS VM=1 Observation Number</summary>
		public readonly static DicomTag ObservationNumber = new DicomTag(0x3006, 0x0082);

		///<summary>(3006,0084) VR=IS VM=1 Referenced ROI Number</summary>
		public readonly static DicomTag ReferencedROINumber = new DicomTag(0x3006, 0x0084);

		///<summary>(3006,0085) VR=SH VM=1 ROI Observation Label</summary>
		public readonly static DicomTag ROIObservationLabel = new DicomTag(0x3006, 0x0085);

		///<summary>(3006,0086) VR=SQ VM=1 RT ROI Identification Code Sequence</summary>
		public readonly static DicomTag RTROIIdentificationCodeSequence = new DicomTag(0x3006, 0x0086);

		///<summary>(3006,0088) VR=ST VM=1 ROI Observation Description</summary>
		public readonly static DicomTag ROIObservationDescription = new DicomTag(0x3006, 0x0088);

		///<summary>(3006,00a0) VR=SQ VM=1 Related RT ROI Observations Sequence</summary>
		public readonly static DicomTag RelatedRTROIObservationsSequence = new DicomTag(0x3006, 0x00a0);

		///<summary>(3006,00a4) VR=CS VM=1 RT ROI Interpreted Type</summary>
		public readonly static DicomTag RTROIInterpretedType = new DicomTag(0x3006, 0x00a4);

		///<summary>(3006,00a6) VR=PN VM=1 ROI Interpreter</summary>
		public readonly static DicomTag ROIInterpreter = new DicomTag(0x3006, 0x00a6);

		///<summary>(3006,00b0) VR=SQ VM=1 ROI Physical Properties Sequence</summary>
		public readonly static DicomTag ROIPhysicalPropertiesSequence = new DicomTag(0x3006, 0x00b0);

		///<summary>(3006,00b2) VR=CS VM=1 ROI Physical Property</summary>
		public readonly static DicomTag ROIPhysicalProperty = new DicomTag(0x3006, 0x00b2);

		///<summary>(3006,00b4) VR=DS VM=1 ROI Physical Property Value</summary>
		public readonly static DicomTag ROIPhysicalPropertyValue = new DicomTag(0x3006, 0x00b4);

		///<summary>(3006,00b6) VR=SQ VM=1 ROI Elemental Composition Sequence</summary>
		public readonly static DicomTag ROIElementalCompositionSequence = new DicomTag(0x3006, 0x00b6);

		///<summary>(3006,00b7) VR=US VM=1 ROI Elemental Composition Atomic Number</summary>
		public readonly static DicomTag ROIElementalCompositionAtomicNumber = new DicomTag(0x3006, 0x00b7);

		///<summary>(3006,00b8) VR=FL VM=1 ROI Elemental Composition Atomic Mass Fraction</summary>
		public readonly static DicomTag ROIElementalCompositionAtomicMassFraction = new DicomTag(0x3006, 0x00b8);

		///<summary>(3006,00c0) VR=SQ VM=1 Frame of Reference Relationship Sequence</summary>
		public readonly static DicomTag FrameOfReferenceRelationshipSequence = new DicomTag(0x3006, 0x00c0);

		///<summary>(3006,00c2) VR=UI VM=1 Related Frame of Reference UID</summary>
		public readonly static DicomTag RelatedFrameOfReferenceUID = new DicomTag(0x3006, 0x00c2);

		///<summary>(3006,00c4) VR=CS VM=1 Frame of Reference Transformation Type</summary>
		public readonly static DicomTag FrameOfReferenceTransformationType = new DicomTag(0x3006, 0x00c4);

		///<summary>(3006,00c6) VR=DS VM=16 Frame of Reference Transformation Matrix</summary>
		public readonly static DicomTag FrameOfReferenceTransformationMatrix = new DicomTag(0x3006, 0x00c6);

		///<summary>(3006,00c8) VR=LO VM=1 Frame of Reference Transformation Comment</summary>
		public readonly static DicomTag FrameOfReferenceTransformationComment = new DicomTag(0x3006, 0x00c8);

		///<summary>(3008,0010) VR=SQ VM=1 Measured Dose Reference Sequence</summary>
		public readonly static DicomTag MeasuredDoseReferenceSequence = new DicomTag(0x3008, 0x0010);

		///<summary>(3008,0012) VR=ST VM=1 Measured Dose Description</summary>
		public readonly static DicomTag MeasuredDoseDescription = new DicomTag(0x3008, 0x0012);

		///<summary>(3008,0014) VR=CS VM=1 Measured Dose Type</summary>
		public readonly static DicomTag MeasuredDoseType = new DicomTag(0x3008, 0x0014);

		///<summary>(3008,0016) VR=DS VM=1 Measured Dose Value</summary>
		public readonly static DicomTag MeasuredDoseValue = new DicomTag(0x3008, 0x0016);

		///<summary>(3008,0020) VR=SQ VM=1 Treatment Session Beam Sequence</summary>
		public readonly static DicomTag TreatmentSessionBeamSequence = new DicomTag(0x3008, 0x0020);

		///<summary>(3008,0021) VR=SQ VM=1 Treatment Session Ion Beam Sequence</summary>
		public readonly static DicomTag TreatmentSessionIonBeamSequence = new DicomTag(0x3008, 0x0021);

		///<summary>(3008,0022) VR=IS VM=1 Current Fraction Number</summary>
		public readonly static DicomTag CurrentFractionNumber = new DicomTag(0x3008, 0x0022);

		///<summary>(3008,0024) VR=DA VM=1 Treatment Control Point Date</summary>
		public readonly static DicomTag TreatmentControlPointDate = new DicomTag(0x3008, 0x0024);

		///<summary>(3008,0025) VR=TM VM=1 Treatment Control Point Time</summary>
		public readonly static DicomTag TreatmentControlPointTime = new DicomTag(0x3008, 0x0025);

		///<summary>(3008,002a) VR=CS VM=1 Treatment Termination Status</summary>
		public readonly static DicomTag TreatmentTerminationStatus = new DicomTag(0x3008, 0x002a);

		///<summary>(3008,002b) VR=SH VM=1 Treatment Termination Code</summary>
		public readonly static DicomTag TreatmentTerminationCode = new DicomTag(0x3008, 0x002b);

		///<summary>(3008,002c) VR=CS VM=1 Treatment Verification Status</summary>
		public readonly static DicomTag TreatmentVerificationStatus = new DicomTag(0x3008, 0x002c);

		///<summary>(3008,0030) VR=SQ VM=1 Referenced Treatment Record Sequence</summary>
		public readonly static DicomTag ReferencedTreatmentRecordSequence = new DicomTag(0x3008, 0x0030);

		///<summary>(3008,0032) VR=DS VM=1 Specified Primary Meterset</summary>
		public readonly static DicomTag SpecifiedPrimaryMeterset = new DicomTag(0x3008, 0x0032);

		///<summary>(3008,0033) VR=DS VM=1 Specified Secondary Meterset</summary>
		public readonly static DicomTag SpecifiedSecondaryMeterset = new DicomTag(0x3008, 0x0033);

		///<summary>(3008,0036) VR=DS VM=1 Delivered Primary Meterset</summary>
		public readonly static DicomTag DeliveredPrimaryMeterset = new DicomTag(0x3008, 0x0036);

		///<summary>(3008,0037) VR=DS VM=1 Delivered Secondary Meterset</summary>
		public readonly static DicomTag DeliveredSecondaryMeterset = new DicomTag(0x3008, 0x0037);

		///<summary>(3008,003a) VR=DS VM=1 Specified Treatment Time</summary>
		public readonly static DicomTag SpecifiedTreatmentTime = new DicomTag(0x3008, 0x003a);

		///<summary>(3008,003b) VR=DS VM=1 Delivered Treatment Time</summary>
		public readonly static DicomTag DeliveredTreatmentTime = new DicomTag(0x3008, 0x003b);

		///<summary>(3008,0040) VR=SQ VM=1 Control Point Delivery Sequence</summary>
		public readonly static DicomTag ControlPointDeliverySequence = new DicomTag(0x3008, 0x0040);

		///<summary>(3008,0041) VR=SQ VM=1 Ion Control Point Delivery Sequence</summary>
		public readonly static DicomTag IonControlPointDeliverySequence = new DicomTag(0x3008, 0x0041);

		///<summary>(3008,0042) VR=DS VM=1 Specified Meterset</summary>
		public readonly static DicomTag SpecifiedMeterset = new DicomTag(0x3008, 0x0042);

		///<summary>(3008,0044) VR=DS VM=1 Delivered Meterset</summary>
		public readonly static DicomTag DeliveredMeterset = new DicomTag(0x3008, 0x0044);

		///<summary>(3008,0045) VR=FL VM=1 Meterset Rate Set</summary>
		public readonly static DicomTag MetersetRateSet = new DicomTag(0x3008, 0x0045);

		///<summary>(3008,0046) VR=FL VM=1 Meterset Rate Delivered</summary>
		public readonly static DicomTag MetersetRateDelivered = new DicomTag(0x3008, 0x0046);

		///<summary>(3008,0047) VR=FL VM=1-n Scan Spot Metersets Delivered</summary>
		public readonly static DicomTag ScanSpotMetersetsDelivered = new DicomTag(0x3008, 0x0047);

		///<summary>(3008,0048) VR=DS VM=1 Dose Rate Delivered</summary>
		public readonly static DicomTag DoseRateDelivered = new DicomTag(0x3008, 0x0048);

		///<summary>(3008,0050) VR=SQ VM=1 Treatment Summary Calculated Dose Reference Sequence</summary>
		public readonly static DicomTag TreatmentSummaryCalculatedDoseReferenceSequence = new DicomTag(0x3008, 0x0050);

		///<summary>(3008,0052) VR=DS VM=1 Cumulative Dose to Dose Reference</summary>
		public readonly static DicomTag CumulativeDoseToDoseReference = new DicomTag(0x3008, 0x0052);

		///<summary>(3008,0054) VR=DA VM=1 First Treatment Date</summary>
		public readonly static DicomTag FirstTreatmentDate = new DicomTag(0x3008, 0x0054);

		///<summary>(3008,0056) VR=DA VM=1 Most Recent Treatment Date</summary>
		public readonly static DicomTag MostRecentTreatmentDate = new DicomTag(0x3008, 0x0056);

		///<summary>(3008,005a) VR=IS VM=1 Number of Fractions Delivered</summary>
		public readonly static DicomTag NumberOfFractionsDelivered = new DicomTag(0x3008, 0x005a);

		///<summary>(3008,0060) VR=SQ VM=1 Override Sequence</summary>
		public readonly static DicomTag OverrideSequence = new DicomTag(0x3008, 0x0060);

		///<summary>(3008,0061) VR=AT VM=1 Parameter Sequence Pointer</summary>
		public readonly static DicomTag ParameterSequencePointer = new DicomTag(0x3008, 0x0061);

		///<summary>(3008,0062) VR=AT VM=1 Override Parameter Pointer</summary>
		public readonly static DicomTag OverrideParameterPointer = new DicomTag(0x3008, 0x0062);

		///<summary>(3008,0063) VR=IS VM=1 Parameter Item Index</summary>
		public readonly static DicomTag ParameterItemIndex = new DicomTag(0x3008, 0x0063);

		///<summary>(3008,0064) VR=IS VM=1 Measured Dose Reference Number</summary>
		public readonly static DicomTag MeasuredDoseReferenceNumber = new DicomTag(0x3008, 0x0064);

		///<summary>(3008,0065) VR=AT VM=1 Parameter Pointer</summary>
		public readonly static DicomTag ParameterPointer = new DicomTag(0x3008, 0x0065);

		///<summary>(3008,0066) VR=ST VM=1 Override Reason</summary>
		public readonly static DicomTag OverrideReason = new DicomTag(0x3008, 0x0066);

		///<summary>(3008,0068) VR=SQ VM=1 Corrected Parameter Sequence</summary>
		public readonly static DicomTag CorrectedParameterSequence = new DicomTag(0x3008, 0x0068);

		///<summary>(3008,006a) VR=FL VM=1 Correction Value</summary>
		public readonly static DicomTag CorrectionValue = new DicomTag(0x3008, 0x006a);

		///<summary>(3008,0070) VR=SQ VM=1 Calculated Dose Reference Sequence</summary>
		public readonly static DicomTag CalculatedDoseReferenceSequence = new DicomTag(0x3008, 0x0070);

		///<summary>(3008,0072) VR=IS VM=1 Calculated Dose Reference Number</summary>
		public readonly static DicomTag CalculatedDoseReferenceNumber = new DicomTag(0x3008, 0x0072);

		///<summary>(3008,0074) VR=ST VM=1 Calculated Dose Reference Description</summary>
		public readonly static DicomTag CalculatedDoseReferenceDescription = new DicomTag(0x3008, 0x0074);

		///<summary>(3008,0076) VR=DS VM=1 Calculated Dose Reference Dose Value</summary>
		public readonly static DicomTag CalculatedDoseReferenceDoseValue = new DicomTag(0x3008, 0x0076);

		///<summary>(3008,0078) VR=DS VM=1 Start Meterset</summary>
		public readonly static DicomTag StartMeterset = new DicomTag(0x3008, 0x0078);

		///<summary>(3008,007a) VR=DS VM=1 End Meterset</summary>
		public readonly static DicomTag EndMeterset = new DicomTag(0x3008, 0x007a);

		///<summary>(3008,0080) VR=SQ VM=1 Referenced Measured Dose Reference Sequence</summary>
		public readonly static DicomTag ReferencedMeasuredDoseReferenceSequence = new DicomTag(0x3008, 0x0080);

		///<summary>(3008,0082) VR=IS VM=1 Referenced Measured Dose Reference Number</summary>
		public readonly static DicomTag ReferencedMeasuredDoseReferenceNumber = new DicomTag(0x3008, 0x0082);

		///<summary>(3008,0090) VR=SQ VM=1 Referenced Calculated Dose Reference Sequence</summary>
		public readonly static DicomTag ReferencedCalculatedDoseReferenceSequence = new DicomTag(0x3008, 0x0090);

		///<summary>(3008,0092) VR=IS VM=1 Referenced Calculated Dose Reference Number</summary>
		public readonly static DicomTag ReferencedCalculatedDoseReferenceNumber = new DicomTag(0x3008, 0x0092);

		///<summary>(3008,00a0) VR=SQ VM=1 Beam Limiting Device Leaf Pairs Sequence</summary>
		public readonly static DicomTag BeamLimitingDeviceLeafPairsSequence = new DicomTag(0x3008, 0x00a0);

		///<summary>(3008,00b0) VR=SQ VM=1 Recorded Wedge Sequence</summary>
		public readonly static DicomTag RecordedWedgeSequence = new DicomTag(0x3008, 0x00b0);

		///<summary>(3008,00c0) VR=SQ VM=1 Recorded Compensator Sequence</summary>
		public readonly static DicomTag RecordedCompensatorSequence = new DicomTag(0x3008, 0x00c0);

		///<summary>(3008,00d0) VR=SQ VM=1 Recorded Block Sequence</summary>
		public readonly static DicomTag RecordedBlockSequence = new DicomTag(0x3008, 0x00d0);

		///<summary>(3008,00e0) VR=SQ VM=1 Treatment Summary Measured Dose Reference Sequence</summary>
		public readonly static DicomTag TreatmentSummaryMeasuredDoseReferenceSequence = new DicomTag(0x3008, 0x00e0);

		///<summary>(3008,00f0) VR=SQ VM=1 Recorded Snout Sequence</summary>
		public readonly static DicomTag RecordedSnoutSequence = new DicomTag(0x3008, 0x00f0);

		///<summary>(3008,00f2) VR=SQ VM=1 Recorded Range Shifter Sequence</summary>
		public readonly static DicomTag RecordedRangeShifterSequence = new DicomTag(0x3008, 0x00f2);

		///<summary>(3008,00f4) VR=SQ VM=1 Recorded Lateral Spreading Device Sequence</summary>
		public readonly static DicomTag RecordedLateralSpreadingDeviceSequence = new DicomTag(0x3008, 0x00f4);

		///<summary>(3008,00f6) VR=SQ VM=1 Recorded Range Modulator Sequence</summary>
		public readonly static DicomTag RecordedRangeModulatorSequence = new DicomTag(0x3008, 0x00f6);

		///<summary>(3008,0100) VR=SQ VM=1 Recorded Source Sequence</summary>
		public readonly static DicomTag RecordedSourceSequence = new DicomTag(0x3008, 0x0100);

		///<summary>(3008,0105) VR=LO VM=1 Source Serial Number</summary>
		public readonly static DicomTag SourceSerialNumber = new DicomTag(0x3008, 0x0105);

		///<summary>(3008,0110) VR=SQ VM=1 Treatment Session Application Setup Sequence</summary>
		public readonly static DicomTag TreatmentSessionApplicationSetupSequence = new DicomTag(0x3008, 0x0110);

		///<summary>(3008,0116) VR=CS VM=1 Application Setup Check</summary>
		public readonly static DicomTag ApplicationSetupCheck = new DicomTag(0x3008, 0x0116);

		///<summary>(3008,0120) VR=SQ VM=1 Recorded Brachy Accessory Device Sequence</summary>
		public readonly static DicomTag RecordedBrachyAccessoryDeviceSequence = new DicomTag(0x3008, 0x0120);

		///<summary>(3008,0122) VR=IS VM=1 Referenced Brachy Accessory Device Number</summary>
		public readonly static DicomTag ReferencedBrachyAccessoryDeviceNumber = new DicomTag(0x3008, 0x0122);

		///<summary>(3008,0130) VR=SQ VM=1 Recorded Channel Sequence</summary>
		public readonly static DicomTag RecordedChannelSequence = new DicomTag(0x3008, 0x0130);

		///<summary>(3008,0132) VR=DS VM=1 Specified Channel Total Time</summary>
		public readonly static DicomTag SpecifiedChannelTotalTime = new DicomTag(0x3008, 0x0132);

		///<summary>(3008,0134) VR=DS VM=1 Delivered Channel Total Time</summary>
		public readonly static DicomTag DeliveredChannelTotalTime = new DicomTag(0x3008, 0x0134);

		///<summary>(3008,0136) VR=IS VM=1 Specified Number of Pulses</summary>
		public readonly static DicomTag SpecifiedNumberOfPulses = new DicomTag(0x3008, 0x0136);

		///<summary>(3008,0138) VR=IS VM=1 Delivered Number of Pulses</summary>
		public readonly static DicomTag DeliveredNumberOfPulses = new DicomTag(0x3008, 0x0138);

		///<summary>(3008,013a) VR=DS VM=1 Specified Pulse Repetition Interval</summary>
		public readonly static DicomTag SpecifiedPulseRepetitionInterval = new DicomTag(0x3008, 0x013a);

		///<summary>(3008,013c) VR=DS VM=1 Delivered Pulse Repetition Interval</summary>
		public readonly static DicomTag DeliveredPulseRepetitionInterval = new DicomTag(0x3008, 0x013c);

		///<summary>(3008,0140) VR=SQ VM=1 Recorded Source Applicator Sequence</summary>
		public readonly static DicomTag RecordedSourceApplicatorSequence = new DicomTag(0x3008, 0x0140);

		///<summary>(3008,0142) VR=IS VM=1 Referenced Source Applicator Number</summary>
		public readonly static DicomTag ReferencedSourceApplicatorNumber = new DicomTag(0x3008, 0x0142);

		///<summary>(3008,0150) VR=SQ VM=1 Recorded Channel Shield Sequence</summary>
		public readonly static DicomTag RecordedChannelShieldSequence = new DicomTag(0x3008, 0x0150);

		///<summary>(3008,0152) VR=IS VM=1 Referenced Channel Shield Number</summary>
		public readonly static DicomTag ReferencedChannelShieldNumber = new DicomTag(0x3008, 0x0152);

		///<summary>(3008,0160) VR=SQ VM=1 Brachy Control Point Delivered Sequence</summary>
		public readonly static DicomTag BrachyControlPointDeliveredSequence = new DicomTag(0x3008, 0x0160);

		///<summary>(3008,0162) VR=DA VM=1 Safe Position Exit Date</summary>
		public readonly static DicomTag SafePositionExitDate = new DicomTag(0x3008, 0x0162);

		///<summary>(3008,0164) VR=TM VM=1 Safe Position Exit Time</summary>
		public readonly static DicomTag SafePositionExitTime = new DicomTag(0x3008, 0x0164);

		///<summary>(3008,0166) VR=DA VM=1 Safe Position Return Date</summary>
		public readonly static DicomTag SafePositionReturnDate = new DicomTag(0x3008, 0x0166);

		///<summary>(3008,0168) VR=TM VM=1 Safe Position Return Time</summary>
		public readonly static DicomTag SafePositionReturnTime = new DicomTag(0x3008, 0x0168);

		///<summary>(3008,0200) VR=CS VM=1 Current Treatment Status</summary>
		public readonly static DicomTag CurrentTreatmentStatus = new DicomTag(0x3008, 0x0200);

		///<summary>(3008,0202) VR=ST VM=1 Treatment Status Comment</summary>
		public readonly static DicomTag TreatmentStatusComment = new DicomTag(0x3008, 0x0202);

		///<summary>(3008,0220) VR=SQ VM=1 Fraction Group Summary Sequence</summary>
		public readonly static DicomTag FractionGroupSummarySequence = new DicomTag(0x3008, 0x0220);

		///<summary>(3008,0223) VR=IS VM=1 Referenced Fraction Number</summary>
		public readonly static DicomTag ReferencedFractionNumber = new DicomTag(0x3008, 0x0223);

		///<summary>(3008,0224) VR=CS VM=1 Fraction Group Type</summary>
		public readonly static DicomTag FractionGroupType = new DicomTag(0x3008, 0x0224);

		///<summary>(3008,0230) VR=CS VM=1 Beam Stopper Position</summary>
		public readonly static DicomTag BeamStopperPosition = new DicomTag(0x3008, 0x0230);

		///<summary>(3008,0240) VR=SQ VM=1 Fraction Status Summary Sequence</summary>
		public readonly static DicomTag FractionStatusSummarySequence = new DicomTag(0x3008, 0x0240);

		///<summary>(3008,0250) VR=DA VM=1 Treatment Date</summary>
		public readonly static DicomTag TreatmentDate = new DicomTag(0x3008, 0x0250);

		///<summary>(3008,0251) VR=TM VM=1 Treatment Time</summary>
		public readonly static DicomTag TreatmentTime = new DicomTag(0x3008, 0x0251);

		///<summary>(300a,0002) VR=SH VM=1 RT Plan Label</summary>
		public readonly static DicomTag RTPlanLabel = new DicomTag(0x300a, 0x0002);

		///<summary>(300a,0003) VR=LO VM=1 RT Plan Name</summary>
		public readonly static DicomTag RTPlanName = new DicomTag(0x300a, 0x0003);

		///<summary>(300a,0004) VR=ST VM=1 RT Plan Description</summary>
		public readonly static DicomTag RTPlanDescription = new DicomTag(0x300a, 0x0004);

		///<summary>(300a,0006) VR=DA VM=1 RT Plan Date</summary>
		public readonly static DicomTag RTPlanDate = new DicomTag(0x300a, 0x0006);

		///<summary>(300a,0007) VR=TM VM=1 RT Plan Time</summary>
		public readonly static DicomTag RTPlanTime = new DicomTag(0x300a, 0x0007);

		///<summary>(300a,0009) VR=LO VM=1-n Treatment Protocols</summary>
		public readonly static DicomTag TreatmentProtocols = new DicomTag(0x300a, 0x0009);

		///<summary>(300a,000a) VR=CS VM=1 Plan Intent</summary>
		public readonly static DicomTag PlanIntent = new DicomTag(0x300a, 0x000a);

		///<summary>(300a,000b) VR=LO VM=1-n Treatment Sites</summary>
		public readonly static DicomTag TreatmentSites = new DicomTag(0x300a, 0x000b);

		///<summary>(300a,000c) VR=CS VM=1 RT Plan Geometry</summary>
		public readonly static DicomTag RTPlanGeometry = new DicomTag(0x300a, 0x000c);

		///<summary>(300a,000e) VR=ST VM=1 Prescription Description</summary>
		public readonly static DicomTag PrescriptionDescription = new DicomTag(0x300a, 0x000e);

		///<summary>(300a,0010) VR=SQ VM=1 Dose Reference Sequence</summary>
		public readonly static DicomTag DoseReferenceSequence = new DicomTag(0x300a, 0x0010);

		///<summary>(300a,0012) VR=IS VM=1 Dose Reference Number</summary>
		public readonly static DicomTag DoseReferenceNumber = new DicomTag(0x300a, 0x0012);

		///<summary>(300a,0013) VR=UI VM=1 Dose Reference UID</summary>
		public readonly static DicomTag DoseReferenceUID = new DicomTag(0x300a, 0x0013);

		///<summary>(300a,0014) VR=CS VM=1 Dose Reference Structure Type</summary>
		public readonly static DicomTag DoseReferenceStructureType = new DicomTag(0x300a, 0x0014);

		///<summary>(300a,0015) VR=CS VM=1 Nominal Beam Energy Unit</summary>
		public readonly static DicomTag NominalBeamEnergyUnit = new DicomTag(0x300a, 0x0015);

		///<summary>(300a,0016) VR=LO VM=1 Dose Reference Description</summary>
		public readonly static DicomTag DoseReferenceDescription = new DicomTag(0x300a, 0x0016);

		///<summary>(300a,0018) VR=DS VM=3 Dose Reference Point Coordinates</summary>
		public readonly static DicomTag DoseReferencePointCoordinates = new DicomTag(0x300a, 0x0018);

		///<summary>(300a,001a) VR=DS VM=1 Nominal Prior Dose</summary>
		public readonly static DicomTag NominalPriorDose = new DicomTag(0x300a, 0x001a);

		///<summary>(300a,0020) VR=CS VM=1 Dose Reference Type</summary>
		public readonly static DicomTag DoseReferenceType = new DicomTag(0x300a, 0x0020);

		///<summary>(300a,0021) VR=DS VM=1 Constraint Weight</summary>
		public readonly static DicomTag ConstraintWeight = new DicomTag(0x300a, 0x0021);

		///<summary>(300a,0022) VR=DS VM=1 Delivery Warning Dose</summary>
		public readonly static DicomTag DeliveryWarningDose = new DicomTag(0x300a, 0x0022);

		///<summary>(300a,0023) VR=DS VM=1 Delivery Maximum Dose</summary>
		public readonly static DicomTag DeliveryMaximumDose = new DicomTag(0x300a, 0x0023);

		///<summary>(300a,0025) VR=DS VM=1 Target Minimum Dose</summary>
		public readonly static DicomTag TargetMinimumDose = new DicomTag(0x300a, 0x0025);

		///<summary>(300a,0026) VR=DS VM=1 Target Prescription Dose</summary>
		public readonly static DicomTag TargetPrescriptionDose = new DicomTag(0x300a, 0x0026);

		///<summary>(300a,0027) VR=DS VM=1 Target Maximum Dose</summary>
		public readonly static DicomTag TargetMaximumDose = new DicomTag(0x300a, 0x0027);

		///<summary>(300a,0028) VR=DS VM=1 Target Underdose Volume Fraction</summary>
		public readonly static DicomTag TargetUnderdoseVolumeFraction = new DicomTag(0x300a, 0x0028);

		///<summary>(300a,002a) VR=DS VM=1 Organ at Risk Full-volume Dose</summary>
		public readonly static DicomTag OrganAtRiskFullVolumeDose = new DicomTag(0x300a, 0x002a);

		///<summary>(300a,002b) VR=DS VM=1 Organ at Risk Limit Dose</summary>
		public readonly static DicomTag OrganAtRiskLimitDose = new DicomTag(0x300a, 0x002b);

		///<summary>(300a,002c) VR=DS VM=1 Organ at Risk Maximum Dose</summary>
		public readonly static DicomTag OrganAtRiskMaximumDose = new DicomTag(0x300a, 0x002c);

		///<summary>(300a,002d) VR=DS VM=1 Organ at Risk Overdose Volume Fraction</summary>
		public readonly static DicomTag OrganAtRiskOverdoseVolumeFraction = new DicomTag(0x300a, 0x002d);

		///<summary>(300a,0040) VR=SQ VM=1 Tolerance Table Sequence</summary>
		public readonly static DicomTag ToleranceTableSequence = new DicomTag(0x300a, 0x0040);

		///<summary>(300a,0042) VR=IS VM=1 Tolerance Table Number</summary>
		public readonly static DicomTag ToleranceTableNumber = new DicomTag(0x300a, 0x0042);

		///<summary>(300a,0043) VR=SH VM=1 Tolerance Table Label</summary>
		public readonly static DicomTag ToleranceTableLabel = new DicomTag(0x300a, 0x0043);

		///<summary>(300a,0044) VR=DS VM=1 Gantry Angle Tolerance</summary>
		public readonly static DicomTag GantryAngleTolerance = new DicomTag(0x300a, 0x0044);

		///<summary>(300a,0046) VR=DS VM=1 Beam Limiting Device Angle Tolerance</summary>
		public readonly static DicomTag BeamLimitingDeviceAngleTolerance = new DicomTag(0x300a, 0x0046);

		///<summary>(300a,0048) VR=SQ VM=1 Beam Limiting Device Tolerance Sequence</summary>
		public readonly static DicomTag BeamLimitingDeviceToleranceSequence = new DicomTag(0x300a, 0x0048);

		///<summary>(300a,004a) VR=DS VM=1 Beam Limiting Device Position Tolerance</summary>
		public readonly static DicomTag BeamLimitingDevicePositionTolerance = new DicomTag(0x300a, 0x004a);

		///<summary>(300a,004b) VR=FL VM=1 Snout Position Tolerance</summary>
		public readonly static DicomTag SnoutPositionTolerance = new DicomTag(0x300a, 0x004b);

		///<summary>(300a,004c) VR=DS VM=1 Patient Support Angle Tolerance</summary>
		public readonly static DicomTag PatientSupportAngleTolerance = new DicomTag(0x300a, 0x004c);

		///<summary>(300a,004e) VR=DS VM=1 Table Top Eccentric Angle Tolerance</summary>
		public readonly static DicomTag TableTopEccentricAngleTolerance = new DicomTag(0x300a, 0x004e);

		///<summary>(300a,004f) VR=FL VM=1 Table Top Pitch Angle Tolerance</summary>
		public readonly static DicomTag TableTopPitchAngleTolerance = new DicomTag(0x300a, 0x004f);

		///<summary>(300a,0050) VR=FL VM=1 Table Top Roll Angle Tolerance</summary>
		public readonly static DicomTag TableTopRollAngleTolerance = new DicomTag(0x300a, 0x0050);

		///<summary>(300a,0051) VR=DS VM=1 Table Top Vertical Position Tolerance</summary>
		public readonly static DicomTag TableTopVerticalPositionTolerance = new DicomTag(0x300a, 0x0051);

		///<summary>(300a,0052) VR=DS VM=1 Table Top Longitudinal Position Tolerance</summary>
		public readonly static DicomTag TableTopLongitudinalPositionTolerance = new DicomTag(0x300a, 0x0052);

		///<summary>(300a,0053) VR=DS VM=1 Table Top Lateral Position Tolerance</summary>
		public readonly static DicomTag TableTopLateralPositionTolerance = new DicomTag(0x300a, 0x0053);

		///<summary>(300a,0055) VR=CS VM=1 RT Plan Relationship</summary>
		public readonly static DicomTag RTPlanRelationship = new DicomTag(0x300a, 0x0055);

		///<summary>(300a,0070) VR=SQ VM=1 Fraction Group Sequence</summary>
		public readonly static DicomTag FractionGroupSequence = new DicomTag(0x300a, 0x0070);

		///<summary>(300a,0071) VR=IS VM=1 Fraction Group Number</summary>
		public readonly static DicomTag FractionGroupNumber = new DicomTag(0x300a, 0x0071);

		///<summary>(300a,0072) VR=LO VM=1 Fraction Group Description</summary>
		public readonly static DicomTag FractionGroupDescription = new DicomTag(0x300a, 0x0072);

		///<summary>(300a,0078) VR=IS VM=1 Number of Fractions Planned</summary>
		public readonly static DicomTag NumberOfFractionsPlanned = new DicomTag(0x300a, 0x0078);

		///<summary>(300a,0079) VR=IS VM=1 Number of Fraction Pattern Digits Per Day</summary>
		public readonly static DicomTag NumberOfFractionPatternDigitsPerDay = new DicomTag(0x300a, 0x0079);

		///<summary>(300a,007a) VR=IS VM=1 Repeat Fraction Cycle Length</summary>
		public readonly static DicomTag RepeatFractionCycleLength = new DicomTag(0x300a, 0x007a);

		///<summary>(300a,007b) VR=LT VM=1 Fraction Pattern</summary>
		public readonly static DicomTag FractionPattern = new DicomTag(0x300a, 0x007b);

		///<summary>(300a,0080) VR=IS VM=1 Number of Beams</summary>
		public readonly static DicomTag NumberOfBeams = new DicomTag(0x300a, 0x0080);

		///<summary>(300a,0082) VR=DS VM=3 Beam Dose Specification Point</summary>
		public readonly static DicomTag BeamDoseSpecificationPoint = new DicomTag(0x300a, 0x0082);

		///<summary>(300a,0084) VR=DS VM=1 Beam Dose</summary>
		public readonly static DicomTag BeamDose = new DicomTag(0x300a, 0x0084);

		///<summary>(300a,0086) VR=DS VM=1 Beam Meterset</summary>
		public readonly static DicomTag BeamMeterset = new DicomTag(0x300a, 0x0086);

		///<summary>(300a,0088) VR=FL VM=1 Beam Dose Point Depth</summary>
		public readonly static DicomTag BeamDosePointDepth = new DicomTag(0x300a, 0x0088);

		///<summary>(300a,0089) VR=FL VM=1 Beam Dose Point Equivalent Depth</summary>
		public readonly static DicomTag BeamDosePointEquivalentDepth = new DicomTag(0x300a, 0x0089);

		///<summary>(300a,008a) VR=FL VM=1 Beam Dose Point SSD</summary>
		public readonly static DicomTag BeamDosePointSSD = new DicomTag(0x300a, 0x008a);

		///<summary>(300a,00a0) VR=IS VM=1 Number of Brachy Application Setups</summary>
		public readonly static DicomTag NumberOfBrachyApplicationSetups = new DicomTag(0x300a, 0x00a0);

		///<summary>(300a,00a2) VR=DS VM=3 Brachy Application Setup Dose Specification Point</summary>
		public readonly static DicomTag BrachyApplicationSetupDoseSpecificationPoint = new DicomTag(0x300a, 0x00a2);

		///<summary>(300a,00a4) VR=DS VM=1 Brachy Application Setup Dose</summary>
		public readonly static DicomTag BrachyApplicationSetupDose = new DicomTag(0x300a, 0x00a4);

		///<summary>(300a,00b0) VR=SQ VM=1 Beam Sequence</summary>
		public readonly static DicomTag BeamSequence = new DicomTag(0x300a, 0x00b0);

		///<summary>(300a,00b2) VR=SH VM=1 Treatment Machine Name</summary>
		public readonly static DicomTag TreatmentMachineName = new DicomTag(0x300a, 0x00b2);

		///<summary>(300a,00b3) VR=CS VM=1 Primary Dosimeter Unit</summary>
		public readonly static DicomTag PrimaryDosimeterUnit = new DicomTag(0x300a, 0x00b3);

		///<summary>(300a,00b4) VR=DS VM=1 Source-Axis Distance</summary>
		public readonly static DicomTag SourceAxisDistance = new DicomTag(0x300a, 0x00b4);

		///<summary>(300a,00b6) VR=SQ VM=1 Beam Limiting Device Sequence</summary>
		public readonly static DicomTag BeamLimitingDeviceSequence = new DicomTag(0x300a, 0x00b6);

		///<summary>(300a,00b8) VR=CS VM=1 RT Beam Limiting Device Type</summary>
		public readonly static DicomTag RTBeamLimitingDeviceType = new DicomTag(0x300a, 0x00b8);

		///<summary>(300a,00ba) VR=DS VM=1 Source to Beam Limiting Device Distance</summary>
		public readonly static DicomTag SourceToBeamLimitingDeviceDistance = new DicomTag(0x300a, 0x00ba);

		///<summary>(300a,00bb) VR=FL VM=1 Isocenter to Beam Limiting Device Distance</summary>
		public readonly static DicomTag IsocenterToBeamLimitingDeviceDistance = new DicomTag(0x300a, 0x00bb);

		///<summary>(300a,00bc) VR=IS VM=1 Number of Leaf/Jaw Pairs</summary>
		public readonly static DicomTag NumberOfLeafJawPairs = new DicomTag(0x300a, 0x00bc);

		///<summary>(300a,00be) VR=DS VM=3-n Leaf Position Boundaries</summary>
		public readonly static DicomTag LeafPositionBoundaries = new DicomTag(0x300a, 0x00be);

		///<summary>(300a,00c0) VR=IS VM=1 Beam Number</summary>
		public readonly static DicomTag BeamNumber = new DicomTag(0x300a, 0x00c0);

		///<summary>(300a,00c2) VR=LO VM=1 Beam Name</summary>
		public readonly static DicomTag BeamName = new DicomTag(0x300a, 0x00c2);

		///<summary>(300a,00c3) VR=ST VM=1 Beam Description</summary>
		public readonly static DicomTag BeamDescription = new DicomTag(0x300a, 0x00c3);

		///<summary>(300a,00c4) VR=CS VM=1 Beam Type</summary>
		public readonly static DicomTag BeamType = new DicomTag(0x300a, 0x00c4);

		///<summary>(300a,00c6) VR=CS VM=1 Radiation Type</summary>
		public readonly static DicomTag RadiationType = new DicomTag(0x300a, 0x00c6);

		///<summary>(300a,00c7) VR=CS VM=1 High-Dose Technique Type</summary>
		public readonly static DicomTag HighDoseTechniqueType = new DicomTag(0x300a, 0x00c7);

		///<summary>(300a,00c8) VR=IS VM=1 Reference Image Number</summary>
		public readonly static DicomTag ReferenceImageNumber = new DicomTag(0x300a, 0x00c8);

		///<summary>(300a,00ca) VR=SQ VM=1 Planned Verification Image Sequence</summary>
		public readonly static DicomTag PlannedVerificationImageSequence = new DicomTag(0x300a, 0x00ca);

		///<summary>(300a,00cc) VR=LO VM=1-n Imaging Device-Specific Acquisition Parameters</summary>
		public readonly static DicomTag ImagingDeviceSpecificAcquisitionParameters = new DicomTag(0x300a, 0x00cc);

		///<summary>(300a,00ce) VR=CS VM=1 Treatment Delivery Type</summary>
		public readonly static DicomTag TreatmentDeliveryType = new DicomTag(0x300a, 0x00ce);

		///<summary>(300a,00d0) VR=IS VM=1 Number of Wedges</summary>
		public readonly static DicomTag NumberOfWedges = new DicomTag(0x300a, 0x00d0);

		///<summary>(300a,00d1) VR=SQ VM=1 Wedge Sequence</summary>
		public readonly static DicomTag WedgeSequence = new DicomTag(0x300a, 0x00d1);

		///<summary>(300a,00d2) VR=IS VM=1 Wedge Number</summary>
		public readonly static DicomTag WedgeNumber = new DicomTag(0x300a, 0x00d2);

		///<summary>(300a,00d3) VR=CS VM=1 Wedge Type</summary>
		public readonly static DicomTag WedgeType = new DicomTag(0x300a, 0x00d3);

		///<summary>(300a,00d4) VR=SH VM=1 Wedge ID</summary>
		public readonly static DicomTag WedgeID = new DicomTag(0x300a, 0x00d4);

		///<summary>(300a,00d5) VR=IS VM=1 Wedge Angle</summary>
		public readonly static DicomTag WedgeAngle = new DicomTag(0x300a, 0x00d5);

		///<summary>(300a,00d6) VR=DS VM=1 Wedge Factor</summary>
		public readonly static DicomTag WedgeFactor = new DicomTag(0x300a, 0x00d6);

		///<summary>(300a,00d7) VR=FL VM=1 Total Wedge Tray Water-Equivalent Thickness</summary>
		public readonly static DicomTag TotalWedgeTrayWaterEquivalentThickness = new DicomTag(0x300a, 0x00d7);

		///<summary>(300a,00d8) VR=DS VM=1 Wedge Orientation</summary>
		public readonly static DicomTag WedgeOrientation = new DicomTag(0x300a, 0x00d8);

		///<summary>(300a,00d9) VR=FL VM=1 Isocenter to Wedge Tray Distance</summary>
		public readonly static DicomTag IsocenterToWedgeTrayDistance = new DicomTag(0x300a, 0x00d9);

		///<summary>(300a,00da) VR=DS VM=1 Source to Wedge Tray Distance</summary>
		public readonly static DicomTag SourceToWedgeTrayDistance = new DicomTag(0x300a, 0x00da);

		///<summary>(300a,00db) VR=FL VM=1 Wedge Thin Edge Position</summary>
		public readonly static DicomTag WedgeThinEdgePosition = new DicomTag(0x300a, 0x00db);

		///<summary>(300a,00dc) VR=SH VM=1 Bolus ID</summary>
		public readonly static DicomTag BolusID = new DicomTag(0x300a, 0x00dc);

		///<summary>(300a,00dd) VR=ST VM=1 Bolus Description</summary>
		public readonly static DicomTag BolusDescription = new DicomTag(0x300a, 0x00dd);

		///<summary>(300a,00e0) VR=IS VM=1 Number of Compensators</summary>
		public readonly static DicomTag NumberOfCompensators = new DicomTag(0x300a, 0x00e0);

		///<summary>(300a,00e1) VR=SH VM=1 Material ID</summary>
		public readonly static DicomTag MaterialID = new DicomTag(0x300a, 0x00e1);

		///<summary>(300a,00e2) VR=DS VM=1 Total Compensator Tray Factor</summary>
		public readonly static DicomTag TotalCompensatorTrayFactor = new DicomTag(0x300a, 0x00e2);

		///<summary>(300a,00e3) VR=SQ VM=1 Compensator Sequence</summary>
		public readonly static DicomTag CompensatorSequence = new DicomTag(0x300a, 0x00e3);

		///<summary>(300a,00e4) VR=IS VM=1 Compensator Number</summary>
		public readonly static DicomTag CompensatorNumber = new DicomTag(0x300a, 0x00e4);

		///<summary>(300a,00e5) VR=SH VM=1 Compensator ID</summary>
		public readonly static DicomTag CompensatorID = new DicomTag(0x300a, 0x00e5);

		///<summary>(300a,00e6) VR=DS VM=1 Source to Compensator Tray Distance</summary>
		public readonly static DicomTag SourceToCompensatorTrayDistance = new DicomTag(0x300a, 0x00e6);

		///<summary>(300a,00e7) VR=IS VM=1 Compensator Rows</summary>
		public readonly static DicomTag CompensatorRows = new DicomTag(0x300a, 0x00e7);

		///<summary>(300a,00e8) VR=IS VM=1 Compensator Columns</summary>
		public readonly static DicomTag CompensatorColumns = new DicomTag(0x300a, 0x00e8);

		///<summary>(300a,00e9) VR=DS VM=2 Compensator Pixel Spacing</summary>
		public readonly static DicomTag CompensatorPixelSpacing = new DicomTag(0x300a, 0x00e9);

		///<summary>(300a,00ea) VR=DS VM=2 Compensator Position</summary>
		public readonly static DicomTag CompensatorPosition = new DicomTag(0x300a, 0x00ea);

		///<summary>(300a,00eb) VR=DS VM=1-n Compensator Transmission Data</summary>
		public readonly static DicomTag CompensatorTransmissionData = new DicomTag(0x300a, 0x00eb);

		///<summary>(300a,00ec) VR=DS VM=1-n Compensator Thickness Data</summary>
		public readonly static DicomTag CompensatorThicknessData = new DicomTag(0x300a, 0x00ec);

		///<summary>(300a,00ed) VR=IS VM=1 Number of Boli</summary>
		public readonly static DicomTag NumberOfBoli = new DicomTag(0x300a, 0x00ed);

		///<summary>(300a,00ee) VR=CS VM=1 Compensator Type</summary>
		public readonly static DicomTag CompensatorType = new DicomTag(0x300a, 0x00ee);

		///<summary>(300a,00f0) VR=IS VM=1 Number of Blocks</summary>
		public readonly static DicomTag NumberOfBlocks = new DicomTag(0x300a, 0x00f0);

		///<summary>(300a,00f2) VR=DS VM=1 Total Block Tray Factor</summary>
		public readonly static DicomTag TotalBlockTrayFactor = new DicomTag(0x300a, 0x00f2);

		///<summary>(300a,00f3) VR=FL VM=1 Total Block Tray Water-Equivalent Thickness</summary>
		public readonly static DicomTag TotalBlockTrayWaterEquivalentThickness = new DicomTag(0x300a, 0x00f3);

		///<summary>(300a,00f4) VR=SQ VM=1 Block Sequence</summary>
		public readonly static DicomTag BlockSequence = new DicomTag(0x300a, 0x00f4);

		///<summary>(300a,00f5) VR=SH VM=1 Block Tray ID</summary>
		public readonly static DicomTag BlockTrayID = new DicomTag(0x300a, 0x00f5);

		///<summary>(300a,00f6) VR=DS VM=1 Source to Block Tray Distance</summary>
		public readonly static DicomTag SourceToBlockTrayDistance = new DicomTag(0x300a, 0x00f6);

		///<summary>(300a,00f7) VR=FL VM=1 Isocenter to Block Tray Distance</summary>
		public readonly static DicomTag IsocenterToBlockTrayDistance = new DicomTag(0x300a, 0x00f7);

		///<summary>(300a,00f8) VR=CS VM=1 Block Type</summary>
		public readonly static DicomTag BlockType = new DicomTag(0x300a, 0x00f8);

		///<summary>(300a,00f9) VR=LO VM=1 Accessory Code</summary>
		public readonly static DicomTag AccessoryCode = new DicomTag(0x300a, 0x00f9);

		///<summary>(300a,00fa) VR=CS VM=1 Block Divergence</summary>
		public readonly static DicomTag BlockDivergence = new DicomTag(0x300a, 0x00fa);

		///<summary>(300a,00fb) VR=CS VM=1 Block Mounting Position</summary>
		public readonly static DicomTag BlockMountingPosition = new DicomTag(0x300a, 0x00fb);

		///<summary>(300a,00fc) VR=IS VM=1 Block Number</summary>
		public readonly static DicomTag BlockNumber = new DicomTag(0x300a, 0x00fc);

		///<summary>(300a,00fe) VR=LO VM=1 Block Name</summary>
		public readonly static DicomTag BlockName = new DicomTag(0x300a, 0x00fe);

		///<summary>(300a,0100) VR=DS VM=1 Block Thickness</summary>
		public readonly static DicomTag BlockThickness = new DicomTag(0x300a, 0x0100);

		///<summary>(300a,0102) VR=DS VM=1 Block Transmission</summary>
		public readonly static DicomTag BlockTransmission = new DicomTag(0x300a, 0x0102);

		///<summary>(300a,0104) VR=IS VM=1 Block Number of Points</summary>
		public readonly static DicomTag BlockNumberOfPoints = new DicomTag(0x300a, 0x0104);

		///<summary>(300a,0106) VR=DS VM=2-2n Block Data</summary>
		public readonly static DicomTag BlockData = new DicomTag(0x300a, 0x0106);

		///<summary>(300a,0107) VR=SQ VM=1 Applicator Sequence</summary>
		public readonly static DicomTag ApplicatorSequence = new DicomTag(0x300a, 0x0107);

		///<summary>(300a,0108) VR=SH VM=1 Applicator ID</summary>
		public readonly static DicomTag ApplicatorID = new DicomTag(0x300a, 0x0108);

		///<summary>(300a,0109) VR=CS VM=1 Applicator Type</summary>
		public readonly static DicomTag ApplicatorType = new DicomTag(0x300a, 0x0109);

		///<summary>(300a,010a) VR=LO VM=1 Applicator Description</summary>
		public readonly static DicomTag ApplicatorDescription = new DicomTag(0x300a, 0x010a);

		///<summary>(300a,010c) VR=DS VM=1 Cumulative Dose Reference Coefficient</summary>
		public readonly static DicomTag CumulativeDoseReferenceCoefficient = new DicomTag(0x300a, 0x010c);

		///<summary>(300a,010e) VR=DS VM=1 Final Cumulative Meterset Weight</summary>
		public readonly static DicomTag FinalCumulativeMetersetWeight = new DicomTag(0x300a, 0x010e);

		///<summary>(300a,0110) VR=IS VM=1 Number of Control Points</summary>
		public readonly static DicomTag NumberOfControlPoints = new DicomTag(0x300a, 0x0110);

		///<summary>(300a,0111) VR=SQ VM=1 Control Point Sequence</summary>
		public readonly static DicomTag ControlPointSequence = new DicomTag(0x300a, 0x0111);

		///<summary>(300a,0112) VR=IS VM=1 Control Point Index</summary>
		public readonly static DicomTag ControlPointIndex = new DicomTag(0x300a, 0x0112);

		///<summary>(300a,0114) VR=DS VM=1 Nominal Beam Energy</summary>
		public readonly static DicomTag NominalBeamEnergy = new DicomTag(0x300a, 0x0114);

		///<summary>(300a,0115) VR=DS VM=1 Dose Rate Set</summary>
		public readonly static DicomTag DoseRateSet = new DicomTag(0x300a, 0x0115);

		///<summary>(300a,0116) VR=SQ VM=1 Wedge Position Sequence</summary>
		public readonly static DicomTag WedgePositionSequence = new DicomTag(0x300a, 0x0116);

		///<summary>(300a,0118) VR=CS VM=1 Wedge Position</summary>
		public readonly static DicomTag WedgePosition = new DicomTag(0x300a, 0x0118);

		///<summary>(300a,011a) VR=SQ VM=1 Beam Limiting Device Position Sequence</summary>
		public readonly static DicomTag BeamLimitingDevicePositionSequence = new DicomTag(0x300a, 0x011a);

		///<summary>(300a,011c) VR=DS VM=2-2n Leaf/Jaw Positions</summary>
		public readonly static DicomTag LeafJawPositions = new DicomTag(0x300a, 0x011c);

		///<summary>(300a,011e) VR=DS VM=1 Gantry Angle</summary>
		public readonly static DicomTag GantryAngle = new DicomTag(0x300a, 0x011e);

		///<summary>(300a,011f) VR=CS VM=1 Gantry Rotation Direction</summary>
		public readonly static DicomTag GantryRotationDirection = new DicomTag(0x300a, 0x011f);

		///<summary>(300a,0120) VR=DS VM=1 Beam Limiting Device Angle</summary>
		public readonly static DicomTag BeamLimitingDeviceAngle = new DicomTag(0x300a, 0x0120);

		///<summary>(300a,0121) VR=CS VM=1 Beam Limiting Device Rotation Direction</summary>
		public readonly static DicomTag BeamLimitingDeviceRotationDirection = new DicomTag(0x300a, 0x0121);

		///<summary>(300a,0122) VR=DS VM=1 Patient Support Angle</summary>
		public readonly static DicomTag PatientSupportAngle = new DicomTag(0x300a, 0x0122);

		///<summary>(300a,0123) VR=CS VM=1 Patient Support Rotation Direction</summary>
		public readonly static DicomTag PatientSupportRotationDirection = new DicomTag(0x300a, 0x0123);

		///<summary>(300a,0124) VR=DS VM=1 Table Top Eccentric Axis Distance</summary>
		public readonly static DicomTag TableTopEccentricAxisDistance = new DicomTag(0x300a, 0x0124);

		///<summary>(300a,0125) VR=DS VM=1 Table Top Eccentric Angle</summary>
		public readonly static DicomTag TableTopEccentricAngle = new DicomTag(0x300a, 0x0125);

		///<summary>(300a,0126) VR=CS VM=1 Table Top Eccentric Rotation Direction</summary>
		public readonly static DicomTag TableTopEccentricRotationDirection = new DicomTag(0x300a, 0x0126);

		///<summary>(300a,0128) VR=DS VM=1 Table Top Vertical Position</summary>
		public readonly static DicomTag TableTopVerticalPosition = new DicomTag(0x300a, 0x0128);

		///<summary>(300a,0129) VR=DS VM=1 Table Top Longitudinal Position</summary>
		public readonly static DicomTag TableTopLongitudinalPosition = new DicomTag(0x300a, 0x0129);

		///<summary>(300a,012a) VR=DS VM=1 Table Top Lateral Position</summary>
		public readonly static DicomTag TableTopLateralPosition = new DicomTag(0x300a, 0x012a);

		///<summary>(300a,012c) VR=DS VM=3 Isocenter Position</summary>
		public readonly static DicomTag IsocenterPosition = new DicomTag(0x300a, 0x012c);

		///<summary>(300a,012e) VR=DS VM=3 Surface Entry Point</summary>
		public readonly static DicomTag SurfaceEntryPoint = new DicomTag(0x300a, 0x012e);

		///<summary>(300a,0130) VR=DS VM=1 Source to Surface Distance</summary>
		public readonly static DicomTag SourceToSurfaceDistance = new DicomTag(0x300a, 0x0130);

		///<summary>(300a,0134) VR=DS VM=1 Cumulative Meterset Weight</summary>
		public readonly static DicomTag CumulativeMetersetWeight = new DicomTag(0x300a, 0x0134);

		///<summary>(300a,0140) VR=FL VM=1 Table Top Pitch Angle</summary>
		public readonly static DicomTag TableTopPitchAngle = new DicomTag(0x300a, 0x0140);

		///<summary>(300a,0142) VR=CS VM=1 Table Top Pitch Rotation Direction</summary>
		public readonly static DicomTag TableTopPitchRotationDirection = new DicomTag(0x300a, 0x0142);

		///<summary>(300a,0144) VR=FL VM=1 Table Top Roll Angle</summary>
		public readonly static DicomTag TableTopRollAngle = new DicomTag(0x300a, 0x0144);

		///<summary>(300a,0146) VR=CS VM=1 Table Top Roll Rotation Direction</summary>
		public readonly static DicomTag TableTopRollRotationDirection = new DicomTag(0x300a, 0x0146);

		///<summary>(300a,0148) VR=FL VM=1 Head Fixation Angle</summary>
		public readonly static DicomTag HeadFixationAngle = new DicomTag(0x300a, 0x0148);

		///<summary>(300a,014a) VR=FL VM=1 Gantry Pitch Angle</summary>
		public readonly static DicomTag GantryPitchAngle = new DicomTag(0x300a, 0x014a);

		///<summary>(300a,014c) VR=CS VM=1 Gantry Pitch Rotation Direction</summary>
		public readonly static DicomTag GantryPitchRotationDirection = new DicomTag(0x300a, 0x014c);

		///<summary>(300a,014e) VR=FL VM=1 Gantry Pitch Angle Tolerance</summary>
		public readonly static DicomTag GantryPitchAngleTolerance = new DicomTag(0x300a, 0x014e);

		///<summary>(300a,0180) VR=SQ VM=1 Patient Setup Sequence</summary>
		public readonly static DicomTag PatientSetupSequence = new DicomTag(0x300a, 0x0180);

		///<summary>(300a,0182) VR=IS VM=1 Patient Setup Number</summary>
		public readonly static DicomTag PatientSetupNumber = new DicomTag(0x300a, 0x0182);

		///<summary>(300a,0183) VR=LO VM=1 Patient Setup Label</summary>
		public readonly static DicomTag PatientSetupLabel = new DicomTag(0x300a, 0x0183);

		///<summary>(300a,0184) VR=LO VM=1 Patient Additional Position</summary>
		public readonly static DicomTag PatientAdditionalPosition = new DicomTag(0x300a, 0x0184);

		///<summary>(300a,0190) VR=SQ VM=1 Fixation Device Sequence</summary>
		public readonly static DicomTag FixationDeviceSequence = new DicomTag(0x300a, 0x0190);

		///<summary>(300a,0192) VR=CS VM=1 Fixation Device Type</summary>
		public readonly static DicomTag FixationDeviceType = new DicomTag(0x300a, 0x0192);

		///<summary>(300a,0194) VR=SH VM=1 Fixation Device Label</summary>
		public readonly static DicomTag FixationDeviceLabel = new DicomTag(0x300a, 0x0194);

		///<summary>(300a,0196) VR=ST VM=1 Fixation Device Description</summary>
		public readonly static DicomTag FixationDeviceDescription = new DicomTag(0x300a, 0x0196);

		///<summary>(300a,0198) VR=SH VM=1 Fixation Device Position</summary>
		public readonly static DicomTag FixationDevicePosition = new DicomTag(0x300a, 0x0198);

		///<summary>(300a,0199) VR=FL VM=1 Fixation Device Pitch Angle</summary>
		public readonly static DicomTag FixationDevicePitchAngle = new DicomTag(0x300a, 0x0199);

		///<summary>(300a,019a) VR=FL VM=1 Fixation Device Roll Angle</summary>
		public readonly static DicomTag FixationDeviceRollAngle = new DicomTag(0x300a, 0x019a);

		///<summary>(300a,01a0) VR=SQ VM=1 Shielding Device Sequence</summary>
		public readonly static DicomTag ShieldingDeviceSequence = new DicomTag(0x300a, 0x01a0);

		///<summary>(300a,01a2) VR=CS VM=1 Shielding Device Type</summary>
		public readonly static DicomTag ShieldingDeviceType = new DicomTag(0x300a, 0x01a2);

		///<summary>(300a,01a4) VR=SH VM=1 Shielding Device Label</summary>
		public readonly static DicomTag ShieldingDeviceLabel = new DicomTag(0x300a, 0x01a4);

		///<summary>(300a,01a6) VR=ST VM=1 Shielding Device Description</summary>
		public readonly static DicomTag ShieldingDeviceDescription = new DicomTag(0x300a, 0x01a6);

		///<summary>(300a,01a8) VR=SH VM=1 Shielding Device Position</summary>
		public readonly static DicomTag ShieldingDevicePosition = new DicomTag(0x300a, 0x01a8);

		///<summary>(300a,01b0) VR=CS VM=1 Setup Technique</summary>
		public readonly static DicomTag SetupTechnique = new DicomTag(0x300a, 0x01b0);

		///<summary>(300a,01b2) VR=ST VM=1 Setup Technique Description</summary>
		public readonly static DicomTag SetupTechniqueDescription = new DicomTag(0x300a, 0x01b2);

		///<summary>(300a,01b4) VR=SQ VM=1 Setup Device Sequence</summary>
		public readonly static DicomTag SetupDeviceSequence = new DicomTag(0x300a, 0x01b4);

		///<summary>(300a,01b6) VR=CS VM=1 Setup Device Type</summary>
		public readonly static DicomTag SetupDeviceType = new DicomTag(0x300a, 0x01b6);

		///<summary>(300a,01b8) VR=SH VM=1 Setup Device Label</summary>
		public readonly static DicomTag SetupDeviceLabel = new DicomTag(0x300a, 0x01b8);

		///<summary>(300a,01ba) VR=ST VM=1 Setup Device Description</summary>
		public readonly static DicomTag SetupDeviceDescription = new DicomTag(0x300a, 0x01ba);

		///<summary>(300a,01bc) VR=DS VM=1 Setup Device Parameter</summary>
		public readonly static DicomTag SetupDeviceParameter = new DicomTag(0x300a, 0x01bc);

		///<summary>(300a,01d0) VR=ST VM=1 Setup Reference Description</summary>
		public readonly static DicomTag SetupReferenceDescription = new DicomTag(0x300a, 0x01d0);

		///<summary>(300a,01d2) VR=DS VM=1 Table Top Vertical Setup Displacement</summary>
		public readonly static DicomTag TableTopVerticalSetupDisplacement = new DicomTag(0x300a, 0x01d2);

		///<summary>(300a,01d4) VR=DS VM=1 Table Top Longitudinal Setup Displacement</summary>
		public readonly static DicomTag TableTopLongitudinalSetupDisplacement = new DicomTag(0x300a, 0x01d4);

		///<summary>(300a,01d6) VR=DS VM=1 Table Top Lateral Setup Displacement</summary>
		public readonly static DicomTag TableTopLateralSetupDisplacement = new DicomTag(0x300a, 0x01d6);

		///<summary>(300a,0200) VR=CS VM=1 Brachy Treatment Technique</summary>
		public readonly static DicomTag BrachyTreatmentTechnique = new DicomTag(0x300a, 0x0200);

		///<summary>(300a,0202) VR=CS VM=1 Brachy Treatment Type</summary>
		public readonly static DicomTag BrachyTreatmentType = new DicomTag(0x300a, 0x0202);

		///<summary>(300a,0206) VR=SQ VM=1 Treatment Machine Sequence</summary>
		public readonly static DicomTag TreatmentMachineSequence = new DicomTag(0x300a, 0x0206);

		///<summary>(300a,0210) VR=SQ VM=1 Source Sequence</summary>
		public readonly static DicomTag SourceSequence = new DicomTag(0x300a, 0x0210);

		///<summary>(300a,0212) VR=IS VM=1 Source Number</summary>
		public readonly static DicomTag SourceNumber = new DicomTag(0x300a, 0x0212);

		///<summary>(300a,0214) VR=CS VM=1 Source Type</summary>
		public readonly static DicomTag SourceType = new DicomTag(0x300a, 0x0214);

		///<summary>(300a,0216) VR=LO VM=1 Source Manufacturer</summary>
		public readonly static DicomTag SourceManufacturer = new DicomTag(0x300a, 0x0216);

		///<summary>(300a,0218) VR=DS VM=1 Active Source Diameter</summary>
		public readonly static DicomTag ActiveSourceDiameter = new DicomTag(0x300a, 0x0218);

		///<summary>(300a,021a) VR=DS VM=1 Active Source Length</summary>
		public readonly static DicomTag ActiveSourceLength = new DicomTag(0x300a, 0x021a);

		///<summary>(300a,0222) VR=DS VM=1 Source Encapsulation Nominal Thickness</summary>
		public readonly static DicomTag SourceEncapsulationNominalThickness = new DicomTag(0x300a, 0x0222);

		///<summary>(300a,0224) VR=DS VM=1 Source Encapsulation Nominal Transmission</summary>
		public readonly static DicomTag SourceEncapsulationNominalTransmission = new DicomTag(0x300a, 0x0224);

		///<summary>(300a,0226) VR=LO VM=1 Source Isotope Name</summary>
		public readonly static DicomTag SourceIsotopeName = new DicomTag(0x300a, 0x0226);

		///<summary>(300a,0228) VR=DS VM=1 Source Isotope Half Life</summary>
		public readonly static DicomTag SourceIsotopeHalfLife = new DicomTag(0x300a, 0x0228);

		///<summary>(300a,0229) VR=CS VM=1 Source Strength Units</summary>
		public readonly static DicomTag SourceStrengthUnits = new DicomTag(0x300a, 0x0229);

		///<summary>(300a,022a) VR=DS VM=1 Reference Air Kerma Rate</summary>
		public readonly static DicomTag ReferenceAirKermaRate = new DicomTag(0x300a, 0x022a);

		///<summary>(300a,022b) VR=DS VM=1 Source Strength</summary>
		public readonly static DicomTag SourceStrength = new DicomTag(0x300a, 0x022b);

		///<summary>(300a,022c) VR=DA VM=1 Source Strength Reference Date</summary>
		public readonly static DicomTag SourceStrengthReferenceDate = new DicomTag(0x300a, 0x022c);

		///<summary>(300a,022e) VR=TM VM=1 Source Strength Reference Time</summary>
		public readonly static DicomTag SourceStrengthReferenceTime = new DicomTag(0x300a, 0x022e);

		///<summary>(300a,0230) VR=SQ VM=1 Application Setup Sequence</summary>
		public readonly static DicomTag ApplicationSetupSequence = new DicomTag(0x300a, 0x0230);

		///<summary>(300a,0232) VR=CS VM=1 Application Setup Type</summary>
		public readonly static DicomTag ApplicationSetupType = new DicomTag(0x300a, 0x0232);

		///<summary>(300a,0234) VR=IS VM=1 Application Setup Number</summary>
		public readonly static DicomTag ApplicationSetupNumber = new DicomTag(0x300a, 0x0234);

		///<summary>(300a,0236) VR=LO VM=1 Application Setup Name</summary>
		public readonly static DicomTag ApplicationSetupName = new DicomTag(0x300a, 0x0236);

		///<summary>(300a,0238) VR=LO VM=1 Application Setup Manufacturer</summary>
		public readonly static DicomTag ApplicationSetupManufacturer = new DicomTag(0x300a, 0x0238);

		///<summary>(300a,0240) VR=IS VM=1 Template Number</summary>
		public readonly static DicomTag TemplateNumber = new DicomTag(0x300a, 0x0240);

		///<summary>(300a,0242) VR=SH VM=1 Template Type</summary>
		public readonly static DicomTag TemplateType = new DicomTag(0x300a, 0x0242);

		///<summary>(300a,0244) VR=LO VM=1 Template Name</summary>
		public readonly static DicomTag TemplateName = new DicomTag(0x300a, 0x0244);

		///<summary>(300a,0250) VR=DS VM=1 Total Reference Air Kerma</summary>
		public readonly static DicomTag TotalReferenceAirKerma = new DicomTag(0x300a, 0x0250);

		///<summary>(300a,0260) VR=SQ VM=1 Brachy Accessory Device Sequence</summary>
		public readonly static DicomTag BrachyAccessoryDeviceSequence = new DicomTag(0x300a, 0x0260);

		///<summary>(300a,0262) VR=IS VM=1 Brachy Accessory Device Number</summary>
		public readonly static DicomTag BrachyAccessoryDeviceNumber = new DicomTag(0x300a, 0x0262);

		///<summary>(300a,0263) VR=SH VM=1 Brachy Accessory Device ID</summary>
		public readonly static DicomTag BrachyAccessoryDeviceID = new DicomTag(0x300a, 0x0263);

		///<summary>(300a,0264) VR=CS VM=1 Brachy Accessory Device Type</summary>
		public readonly static DicomTag BrachyAccessoryDeviceType = new DicomTag(0x300a, 0x0264);

		///<summary>(300a,0266) VR=LO VM=1 Brachy Accessory Device Name</summary>
		public readonly static DicomTag BrachyAccessoryDeviceName = new DicomTag(0x300a, 0x0266);

		///<summary>(300a,026a) VR=DS VM=1 Brachy Accessory Device Nominal Thickness</summary>
		public readonly static DicomTag BrachyAccessoryDeviceNominalThickness = new DicomTag(0x300a, 0x026a);

		///<summary>(300a,026c) VR=DS VM=1 Brachy Accessory Device Nominal Transmission</summary>
		public readonly static DicomTag BrachyAccessoryDeviceNominalTransmission = new DicomTag(0x300a, 0x026c);

		///<summary>(300a,0280) VR=SQ VM=1 Channel Sequence</summary>
		public readonly static DicomTag ChannelSequence = new DicomTag(0x300a, 0x0280);

		///<summary>(300a,0282) VR=IS VM=1 Channel Number</summary>
		public readonly static DicomTag ChannelNumber = new DicomTag(0x300a, 0x0282);

		///<summary>(300a,0284) VR=DS VM=1 Channel Length</summary>
		public readonly static DicomTag ChannelLength = new DicomTag(0x300a, 0x0284);

		///<summary>(300a,0286) VR=DS VM=1 Channel Total Time</summary>
		public readonly static DicomTag ChannelTotalTime = new DicomTag(0x300a, 0x0286);

		///<summary>(300a,0288) VR=CS VM=1 Source Movement Type</summary>
		public readonly static DicomTag SourceMovementType = new DicomTag(0x300a, 0x0288);

		///<summary>(300a,028a) VR=IS VM=1 Number of Pulses</summary>
		public readonly static DicomTag NumberOfPulses = new DicomTag(0x300a, 0x028a);

		///<summary>(300a,028c) VR=DS VM=1 Pulse Repetition Interval</summary>
		public readonly static DicomTag PulseRepetitionInterval = new DicomTag(0x300a, 0x028c);

		///<summary>(300a,0290) VR=IS VM=1 Source Applicator Number</summary>
		public readonly static DicomTag SourceApplicatorNumber = new DicomTag(0x300a, 0x0290);

		///<summary>(300a,0291) VR=SH VM=1 Source Applicator ID</summary>
		public readonly static DicomTag SourceApplicatorID = new DicomTag(0x300a, 0x0291);

		///<summary>(300a,0292) VR=CS VM=1 Source Applicator Type</summary>
		public readonly static DicomTag SourceApplicatorType = new DicomTag(0x300a, 0x0292);

		///<summary>(300a,0294) VR=LO VM=1 Source Applicator Name</summary>
		public readonly static DicomTag SourceApplicatorName = new DicomTag(0x300a, 0x0294);

		///<summary>(300a,0296) VR=DS VM=1 Source Applicator Length</summary>
		public readonly static DicomTag SourceApplicatorLength = new DicomTag(0x300a, 0x0296);

		///<summary>(300a,0298) VR=LO VM=1 Source Applicator Manufacturer</summary>
		public readonly static DicomTag SourceApplicatorManufacturer = new DicomTag(0x300a, 0x0298);

		///<summary>(300a,029c) VR=DS VM=1 Source Applicator Wall Nominal Thickness</summary>
		public readonly static DicomTag SourceApplicatorWallNominalThickness = new DicomTag(0x300a, 0x029c);

		///<summary>(300a,029e) VR=DS VM=1 Source Applicator Wall Nominal Transmission</summary>
		public readonly static DicomTag SourceApplicatorWallNominalTransmission = new DicomTag(0x300a, 0x029e);

		///<summary>(300a,02a0) VR=DS VM=1 Source Applicator Step Size</summary>
		public readonly static DicomTag SourceApplicatorStepSize = new DicomTag(0x300a, 0x02a0);

		///<summary>(300a,02a2) VR=IS VM=1 Transfer Tube Number</summary>
		public readonly static DicomTag TransferTubeNumber = new DicomTag(0x300a, 0x02a2);

		///<summary>(300a,02a4) VR=DS VM=1 Transfer Tube Length</summary>
		public readonly static DicomTag TransferTubeLength = new DicomTag(0x300a, 0x02a4);

		///<summary>(300a,02b0) VR=SQ VM=1 Channel Shield Sequence</summary>
		public readonly static DicomTag ChannelShieldSequence = new DicomTag(0x300a, 0x02b0);

		///<summary>(300a,02b2) VR=IS VM=1 Channel Shield Number</summary>
		public readonly static DicomTag ChannelShieldNumber = new DicomTag(0x300a, 0x02b2);

		///<summary>(300a,02b3) VR=SH VM=1 Channel Shield ID</summary>
		public readonly static DicomTag ChannelShieldID = new DicomTag(0x300a, 0x02b3);

		///<summary>(300a,02b4) VR=LO VM=1 Channel Shield Name</summary>
		public readonly static DicomTag ChannelShieldName = new DicomTag(0x300a, 0x02b4);

		///<summary>(300a,02b8) VR=DS VM=1 Channel Shield Nominal Thickness</summary>
		public readonly static DicomTag ChannelShieldNominalThickness = new DicomTag(0x300a, 0x02b8);

		///<summary>(300a,02ba) VR=DS VM=1 Channel Shield Nominal Transmission</summary>
		public readonly static DicomTag ChannelShieldNominalTransmission = new DicomTag(0x300a, 0x02ba);

		///<summary>(300a,02c8) VR=DS VM=1 Final Cumulative Time Weight</summary>
		public readonly static DicomTag FinalCumulativeTimeWeight = new DicomTag(0x300a, 0x02c8);

		///<summary>(300a,02d0) VR=SQ VM=1 Brachy Control Point Sequence</summary>
		public readonly static DicomTag BrachyControlPointSequence = new DicomTag(0x300a, 0x02d0);

		///<summary>(300a,02d2) VR=DS VM=1 Control Point Relative Position</summary>
		public readonly static DicomTag ControlPointRelativePosition = new DicomTag(0x300a, 0x02d2);

		///<summary>(300a,02d4) VR=DS VM=3 Control Point 3D Position</summary>
		public readonly static DicomTag ControlPoint3DPosition = new DicomTag(0x300a, 0x02d4);

		///<summary>(300a,02d6) VR=DS VM=1 Cumulative Time Weight</summary>
		public readonly static DicomTag CumulativeTimeWeight = new DicomTag(0x300a, 0x02d6);

		///<summary>(300a,02e0) VR=CS VM=1 Compensator Divergence</summary>
		public readonly static DicomTag CompensatorDivergence = new DicomTag(0x300a, 0x02e0);

		///<summary>(300a,02e1) VR=CS VM=1 Compensator Mounting Position</summary>
		public readonly static DicomTag CompensatorMountingPosition = new DicomTag(0x300a, 0x02e1);

		///<summary>(300a,02e2) VR=DS VM=1-n Source to Compensator Distance</summary>
		public readonly static DicomTag SourceToCompensatorDistance = new DicomTag(0x300a, 0x02e2);

		///<summary>(300a,02e3) VR=FL VM=1 Total Compensator Tray Water-Equivalent Thickness</summary>
		public readonly static DicomTag TotalCompensatorTrayWaterEquivalentThickness = new DicomTag(0x300a, 0x02e3);

		///<summary>(300a,02e4) VR=FL VM=1 Isocenter to Compensator Tray Distance</summary>
		public readonly static DicomTag IsocenterToCompensatorTrayDistance = new DicomTag(0x300a, 0x02e4);

		///<summary>(300a,02e5) VR=FL VM=1 Compensator Column Offset</summary>
		public readonly static DicomTag CompensatorColumnOffset = new DicomTag(0x300a, 0x02e5);

		///<summary>(300a,02e6) VR=FL VM=1-n Isocenter to Compensator Distances</summary>
		public readonly static DicomTag IsocenterToCompensatorDistances = new DicomTag(0x300a, 0x02e6);

		///<summary>(300a,02e7) VR=FL VM=1 Compensator Relative Stopping Power Ratio</summary>
		public readonly static DicomTag CompensatorRelativeStoppingPowerRatio = new DicomTag(0x300a, 0x02e7);

		///<summary>(300a,02e8) VR=FL VM=1 Compensator Milling Tool Diameter</summary>
		public readonly static DicomTag CompensatorMillingToolDiameter = new DicomTag(0x300a, 0x02e8);

		///<summary>(300a,02ea) VR=SQ VM=1 Ion Range Compensator Sequence</summary>
		public readonly static DicomTag IonRangeCompensatorSequence = new DicomTag(0x300a, 0x02ea);

		///<summary>(300a,02eb) VR=LT VM=1 Compensator Description</summary>
		public readonly static DicomTag CompensatorDescription = new DicomTag(0x300a, 0x02eb);

		///<summary>(300a,0302) VR=IS VM=1 Radiation Mass Number</summary>
		public readonly static DicomTag RadiationMassNumber = new DicomTag(0x300a, 0x0302);

		///<summary>(300a,0304) VR=IS VM=1 Radiation Atomic Number</summary>
		public readonly static DicomTag RadiationAtomicNumber = new DicomTag(0x300a, 0x0304);

		///<summary>(300a,0306) VR=SS VM=1 Radiation Charge State</summary>
		public readonly static DicomTag RadiationChargeState = new DicomTag(0x300a, 0x0306);

		///<summary>(300a,0308) VR=CS VM=1 Scan Mode</summary>
		public readonly static DicomTag ScanMode = new DicomTag(0x300a, 0x0308);

		///<summary>(300a,030a) VR=FL VM=2 Virtual Source-Axis Distances</summary>
		public readonly static DicomTag VirtualSourceAxisDistances = new DicomTag(0x300a, 0x030a);

		///<summary>(300a,030c) VR=SQ VM=1 Snout Sequence</summary>
		public readonly static DicomTag SnoutSequence = new DicomTag(0x300a, 0x030c);

		///<summary>(300a,030d) VR=FL VM=1 Snout Position</summary>
		public readonly static DicomTag SnoutPosition = new DicomTag(0x300a, 0x030d);

		///<summary>(300a,030f) VR=SH VM=1 Snout ID</summary>
		public readonly static DicomTag SnoutID = new DicomTag(0x300a, 0x030f);

		///<summary>(300a,0312) VR=IS VM=1 Number of Range Shifters</summary>
		public readonly static DicomTag NumberOfRangeShifters = new DicomTag(0x300a, 0x0312);

		///<summary>(300a,0314) VR=SQ VM=1 Range Shifter Sequence</summary>
		public readonly static DicomTag RangeShifterSequence = new DicomTag(0x300a, 0x0314);

		///<summary>(300a,0316) VR=IS VM=1 Range Shifter Number</summary>
		public readonly static DicomTag RangeShifterNumber = new DicomTag(0x300a, 0x0316);

		///<summary>(300a,0318) VR=SH VM=1 Range Shifter ID</summary>
		public readonly static DicomTag RangeShifterID = new DicomTag(0x300a, 0x0318);

		///<summary>(300a,0320) VR=CS VM=1 Range Shifter Type</summary>
		public readonly static DicomTag RangeShifterType = new DicomTag(0x300a, 0x0320);

		///<summary>(300a,0322) VR=LO VM=1 Range Shifter Description</summary>
		public readonly static DicomTag RangeShifterDescription = new DicomTag(0x300a, 0x0322);

		///<summary>(300a,0330) VR=IS VM=1 Number of Lateral Spreading Devices</summary>
		public readonly static DicomTag NumberOfLateralSpreadingDevices = new DicomTag(0x300a, 0x0330);

		///<summary>(300a,0332) VR=SQ VM=1 Lateral Spreading Device Sequence</summary>
		public readonly static DicomTag LateralSpreadingDeviceSequence = new DicomTag(0x300a, 0x0332);

		///<summary>(300a,0334) VR=IS VM=1 Lateral Spreading Device Number</summary>
		public readonly static DicomTag LateralSpreadingDeviceNumber = new DicomTag(0x300a, 0x0334);

		///<summary>(300a,0336) VR=SH VM=1 Lateral Spreading Device ID</summary>
		public readonly static DicomTag LateralSpreadingDeviceID = new DicomTag(0x300a, 0x0336);

		///<summary>(300a,0338) VR=CS VM=1 Lateral Spreading Device Type</summary>
		public readonly static DicomTag LateralSpreadingDeviceType = new DicomTag(0x300a, 0x0338);

		///<summary>(300a,033a) VR=LO VM=1 Lateral Spreading Device Description</summary>
		public readonly static DicomTag LateralSpreadingDeviceDescription = new DicomTag(0x300a, 0x033a);

		///<summary>(300a,033c) VR=FL VM=1 Lateral Spreading Device Water Equivalent Thickness</summary>
		public readonly static DicomTag LateralSpreadingDeviceWaterEquivalentThickness = new DicomTag(0x300a, 0x033c);

		///<summary>(300a,0340) VR=IS VM=1 Number of Range Modulators</summary>
		public readonly static DicomTag NumberOfRangeModulators = new DicomTag(0x300a, 0x0340);

		///<summary>(300a,0342) VR=SQ VM=1 Range Modulator Sequence</summary>
		public readonly static DicomTag RangeModulatorSequence = new DicomTag(0x300a, 0x0342);

		///<summary>(300a,0344) VR=IS VM=1 Range Modulator Number</summary>
		public readonly static DicomTag RangeModulatorNumber = new DicomTag(0x300a, 0x0344);

		///<summary>(300a,0346) VR=SH VM=1 Range Modulator ID</summary>
		public readonly static DicomTag RangeModulatorID = new DicomTag(0x300a, 0x0346);

		///<summary>(300a,0348) VR=CS VM=1 Range Modulator Type</summary>
		public readonly static DicomTag RangeModulatorType = new DicomTag(0x300a, 0x0348);

		///<summary>(300a,034a) VR=LO VM=1 Range Modulator Description</summary>
		public readonly static DicomTag RangeModulatorDescription = new DicomTag(0x300a, 0x034a);

		///<summary>(300a,034c) VR=SH VM=1 Beam Current Modulation ID</summary>
		public readonly static DicomTag BeamCurrentModulationID = new DicomTag(0x300a, 0x034c);

		///<summary>(300a,0350) VR=CS VM=1 Patient Support Type</summary>
		public readonly static DicomTag PatientSupportType = new DicomTag(0x300a, 0x0350);

		///<summary>(300a,0352) VR=SH VM=1 Patient Support ID</summary>
		public readonly static DicomTag PatientSupportID = new DicomTag(0x300a, 0x0352);

		///<summary>(300a,0354) VR=LO VM=1 Patient Support Accessory Code</summary>
		public readonly static DicomTag PatientSupportAccessoryCode = new DicomTag(0x300a, 0x0354);

		///<summary>(300a,0356) VR=FL VM=1 Fixation Light Azimuthal Angle</summary>
		public readonly static DicomTag FixationLightAzimuthalAngle = new DicomTag(0x300a, 0x0356);

		///<summary>(300a,0358) VR=FL VM=1 Fixation Light Polar Angle</summary>
		public readonly static DicomTag FixationLightPolarAngle = new DicomTag(0x300a, 0x0358);

		///<summary>(300a,035a) VR=FL VM=1 Meterset Rate</summary>
		public readonly static DicomTag MetersetRate = new DicomTag(0x300a, 0x035a);

		///<summary>(300a,0360) VR=SQ VM=1 Range Shifter Settings Sequence</summary>
		public readonly static DicomTag RangeShifterSettingsSequence = new DicomTag(0x300a, 0x0360);

		///<summary>(300a,0362) VR=LO VM=1 Range Shifter Setting</summary>
		public readonly static DicomTag RangeShifterSetting = new DicomTag(0x300a, 0x0362);

		///<summary>(300a,0364) VR=FL VM=1 Isocenter to Range Shifter Distance</summary>
		public readonly static DicomTag IsocenterToRangeShifterDistance = new DicomTag(0x300a, 0x0364);

		///<summary>(300a,0366) VR=FL VM=1 Range Shifter Water Equivalent Thickness</summary>
		public readonly static DicomTag RangeShifterWaterEquivalentThickness = new DicomTag(0x300a, 0x0366);

		///<summary>(300a,0370) VR=SQ VM=1 Lateral Spreading Device Settings Sequence</summary>
		public readonly static DicomTag LateralSpreadingDeviceSettingsSequence = new DicomTag(0x300a, 0x0370);

		///<summary>(300a,0372) VR=LO VM=1 Lateral Spreading Device Setting</summary>
		public readonly static DicomTag LateralSpreadingDeviceSetting = new DicomTag(0x300a, 0x0372);

		///<summary>(300a,0374) VR=FL VM=1 Isocenter to Lateral Spreading Device Distance</summary>
		public readonly static DicomTag IsocenterToLateralSpreadingDeviceDistance = new DicomTag(0x300a, 0x0374);

		///<summary>(300a,0380) VR=SQ VM=1 Range Modulator Settings Sequence</summary>
		public readonly static DicomTag RangeModulatorSettingsSequence = new DicomTag(0x300a, 0x0380);

		///<summary>(300a,0382) VR=FL VM=1 Range Modulator Gating Start Value</summary>
		public readonly static DicomTag RangeModulatorGatingStartValue = new DicomTag(0x300a, 0x0382);

		///<summary>(300a,0384) VR=FL VM=1 Range Modulator Gating Stop Value</summary>
		public readonly static DicomTag RangeModulatorGatingStopValue = new DicomTag(0x300a, 0x0384);

		///<summary>(300a,0386) VR=FL VM=1 Range Modulator Gating Start Water Equivalent Thickness</summary>
		public readonly static DicomTag RangeModulatorGatingStartWaterEquivalentThickness = new DicomTag(0x300a, 0x0386);

		///<summary>(300a,0388) VR=FL VM=1 Range Modulator Gating Stop Water Equivalent Thickness</summary>
		public readonly static DicomTag RangeModulatorGatingStopWaterEquivalentThickness = new DicomTag(0x300a, 0x0388);

		///<summary>(300a,038a) VR=FL VM=1 Isocenter to Range Modulator Distance</summary>
		public readonly static DicomTag IsocenterToRangeModulatorDistance = new DicomTag(0x300a, 0x038a);

		///<summary>(300a,0390) VR=SH VM=1 Scan Spot Tune ID</summary>
		public readonly static DicomTag ScanSpotTuneID = new DicomTag(0x300a, 0x0390);

		///<summary>(300a,0392) VR=IS VM=1 Number of Scan Spot Positions</summary>
		public readonly static DicomTag NumberOfScanSpotPositions = new DicomTag(0x300a, 0x0392);

		///<summary>(300a,0394) VR=FL VM=1-n Scan Spot Position Map</summary>
		public readonly static DicomTag ScanSpotPositionMap = new DicomTag(0x300a, 0x0394);

		///<summary>(300a,0396) VR=FL VM=1-n Scan Spot Meterset Weights</summary>
		public readonly static DicomTag ScanSpotMetersetWeights = new DicomTag(0x300a, 0x0396);

		///<summary>(300a,0398) VR=FL VM=2 Scanning Spot Size</summary>
		public readonly static DicomTag ScanningSpotSize = new DicomTag(0x300a, 0x0398);

		///<summary>(300a,039a) VR=IS VM=1 Number of Paintings</summary>
		public readonly static DicomTag NumberOfPaintings = new DicomTag(0x300a, 0x039a);

		///<summary>(300a,03a0) VR=SQ VM=1 Ion Tolerance Table Sequence</summary>
		public readonly static DicomTag IonToleranceTableSequence = new DicomTag(0x300a, 0x03a0);

		///<summary>(300a,03a2) VR=SQ VM=1 Ion Beam Sequence</summary>
		public readonly static DicomTag IonBeamSequence = new DicomTag(0x300a, 0x03a2);

		///<summary>(300a,03a4) VR=SQ VM=1 Ion Beam Limiting Device Sequence</summary>
		public readonly static DicomTag IonBeamLimitingDeviceSequence = new DicomTag(0x300a, 0x03a4);

		///<summary>(300a,03a6) VR=SQ VM=1 Ion Block Sequence</summary>
		public readonly static DicomTag IonBlockSequence = new DicomTag(0x300a, 0x03a6);

		///<summary>(300a,03a8) VR=SQ VM=1 Ion Control Point Sequence</summary>
		public readonly static DicomTag IonControlPointSequence = new DicomTag(0x300a, 0x03a8);

		///<summary>(300a,03aa) VR=SQ VM=1 Ion Wedge Sequence</summary>
		public readonly static DicomTag IonWedgeSequence = new DicomTag(0x300a, 0x03aa);

		///<summary>(300a,03ac) VR=SQ VM=1 Ion Wedge Position Sequence</summary>
		public readonly static DicomTag IonWedgePositionSequence = new DicomTag(0x300a, 0x03ac);

		///<summary>(300a,0401) VR=SQ VM=1 Referenced Setup Image Sequence</summary>
		public readonly static DicomTag ReferencedSetupImageSequence = new DicomTag(0x300a, 0x0401);

		///<summary>(300a,0402) VR=ST VM=1 Setup Image Comment</summary>
		public readonly static DicomTag SetupImageComment = new DicomTag(0x300a, 0x0402);

		///<summary>(300a,0410) VR=SQ VM=1 Motion Synchronization Sequence</summary>
		public readonly static DicomTag MotionSynchronizationSequence = new DicomTag(0x300a, 0x0410);

		///<summary>(300a,0412) VR=FL VM=3 Control Point Orientation</summary>
		public readonly static DicomTag ControlPointOrientation = new DicomTag(0x300a, 0x0412);

		///<summary>(300a,0420) VR=SQ VM=1 General Accessory Sequence</summary>
		public readonly static DicomTag GeneralAccessorySequence = new DicomTag(0x300a, 0x0420);

		///<summary>(300a,0421) VR=SH VM=1 General Accessory ID</summary>
		public readonly static DicomTag GeneralAccessoryID = new DicomTag(0x300a, 0x0421);

		///<summary>(300a,0422) VR=ST VM=1 General Accessory Description</summary>
		public readonly static DicomTag GeneralAccessoryDescription = new DicomTag(0x300a, 0x0422);

		///<summary>(300a,0423) VR=CS VM=1 General Accessory Type</summary>
		public readonly static DicomTag GeneralAccessoryType = new DicomTag(0x300a, 0x0423);

		///<summary>(300a,0424) VR=IS VM=1 General Accessory Number</summary>
		public readonly static DicomTag GeneralAccessoryNumber = new DicomTag(0x300a, 0x0424);

		///<summary>(300a,0431) VR=SQ VM=1 Applicator Geometry Sequence</summary>
		public readonly static DicomTag ApplicatorGeometrySequence = new DicomTag(0x300a, 0x0431);

		///<summary>(300a,0432) VR=CS VM=1 Applicator Aperture Shape</summary>
		public readonly static DicomTag ApplicatorApertureShape = new DicomTag(0x300a, 0x0432);

		///<summary>(300a,0433) VR=FL VM=1 Applicator Opening</summary>
		public readonly static DicomTag ApplicatorOpening = new DicomTag(0x300a, 0x0433);

		///<summary>(300a,0434) VR=FL VM=1 Applicator Opening X</summary>
		public readonly static DicomTag ApplicatorOpeningX = new DicomTag(0x300a, 0x0434);

		///<summary>(300a,0435) VR=FL VM=1 Applicator Opening Y</summary>
		public readonly static DicomTag ApplicatorOpeningY = new DicomTag(0x300a, 0x0435);

		///<summary>(300a,0436) VR=FL VM=1 Source to Applicator Mounting Position Distance</summary>
		public readonly static DicomTag SourceToApplicatorMountingPositionDistance = new DicomTag(0x300a, 0x0436);

		///<summary>(300c,0002) VR=SQ VM=1 Referenced RT Plan Sequence</summary>
		public readonly static DicomTag ReferencedRTPlanSequence = new DicomTag(0x300c, 0x0002);

		///<summary>(300c,0004) VR=SQ VM=1 Referenced Beam Sequence</summary>
		public readonly static DicomTag ReferencedBeamSequence = new DicomTag(0x300c, 0x0004);

		///<summary>(300c,0006) VR=IS VM=1 Referenced Beam Number</summary>
		public readonly static DicomTag ReferencedBeamNumber = new DicomTag(0x300c, 0x0006);

		///<summary>(300c,0007) VR=IS VM=1 Referenced Reference Image Number</summary>
		public readonly static DicomTag ReferencedReferenceImageNumber = new DicomTag(0x300c, 0x0007);

		///<summary>(300c,0008) VR=DS VM=1 Start Cumulative Meterset Weight</summary>
		public readonly static DicomTag StartCumulativeMetersetWeight = new DicomTag(0x300c, 0x0008);

		///<summary>(300c,0009) VR=DS VM=1 End Cumulative Meterset Weight</summary>
		public readonly static DicomTag EndCumulativeMetersetWeight = new DicomTag(0x300c, 0x0009);

		///<summary>(300c,000a) VR=SQ VM=1 Referenced Brachy Application Setup Sequence</summary>
		public readonly static DicomTag ReferencedBrachyApplicationSetupSequence = new DicomTag(0x300c, 0x000a);

		///<summary>(300c,000c) VR=IS VM=1 Referenced Brachy Application Setup Number</summary>
		public readonly static DicomTag ReferencedBrachyApplicationSetupNumber = new DicomTag(0x300c, 0x000c);

		///<summary>(300c,000e) VR=IS VM=1 Referenced Source Number</summary>
		public readonly static DicomTag ReferencedSourceNumber = new DicomTag(0x300c, 0x000e);

		///<summary>(300c,0020) VR=SQ VM=1 Referenced Fraction Group Sequence</summary>
		public readonly static DicomTag ReferencedFractionGroupSequence = new DicomTag(0x300c, 0x0020);

		///<summary>(300c,0022) VR=IS VM=1 Referenced Fraction Group Number</summary>
		public readonly static DicomTag ReferencedFractionGroupNumber = new DicomTag(0x300c, 0x0022);

		///<summary>(300c,0040) VR=SQ VM=1 Referenced Verification Image Sequence</summary>
		public readonly static DicomTag ReferencedVerificationImageSequence = new DicomTag(0x300c, 0x0040);

		///<summary>(300c,0042) VR=SQ VM=1 Referenced Reference Image Sequence</summary>
		public readonly static DicomTag ReferencedReferenceImageSequence = new DicomTag(0x300c, 0x0042);

		///<summary>(300c,0050) VR=SQ VM=1 Referenced Dose Reference Sequence</summary>
		public readonly static DicomTag ReferencedDoseReferenceSequence = new DicomTag(0x300c, 0x0050);

		///<summary>(300c,0051) VR=IS VM=1 Referenced Dose Reference Number</summary>
		public readonly static DicomTag ReferencedDoseReferenceNumber = new DicomTag(0x300c, 0x0051);

		///<summary>(300c,0055) VR=SQ VM=1 Brachy Referenced Dose Reference Sequence</summary>
		public readonly static DicomTag BrachyReferencedDoseReferenceSequence = new DicomTag(0x300c, 0x0055);

		///<summary>(300c,0060) VR=SQ VM=1 Referenced Structure Set Sequence</summary>
		public readonly static DicomTag ReferencedStructureSetSequence = new DicomTag(0x300c, 0x0060);

		///<summary>(300c,006a) VR=IS VM=1 Referenced Patient Setup Number</summary>
		public readonly static DicomTag ReferencedPatientSetupNumber = new DicomTag(0x300c, 0x006a);

		///<summary>(300c,0080) VR=SQ VM=1 Referenced Dose Sequence</summary>
		public readonly static DicomTag ReferencedDoseSequence = new DicomTag(0x300c, 0x0080);

		///<summary>(300c,00a0) VR=IS VM=1 Referenced Tolerance Table Number</summary>
		public readonly static DicomTag ReferencedToleranceTableNumber = new DicomTag(0x300c, 0x00a0);

		///<summary>(300c,00b0) VR=SQ VM=1 Referenced Bolus Sequence</summary>
		public readonly static DicomTag ReferencedBolusSequence = new DicomTag(0x300c, 0x00b0);

		///<summary>(300c,00c0) VR=IS VM=1 Referenced Wedge Number</summary>
		public readonly static DicomTag ReferencedWedgeNumber = new DicomTag(0x300c, 0x00c0);

		///<summary>(300c,00d0) VR=IS VM=1 Referenced Compensator Number</summary>
		public readonly static DicomTag ReferencedCompensatorNumber = new DicomTag(0x300c, 0x00d0);

		///<summary>(300c,00e0) VR=IS VM=1 Referenced Block Number</summary>
		public readonly static DicomTag ReferencedBlockNumber = new DicomTag(0x300c, 0x00e0);

		///<summary>(300c,00f0) VR=IS VM=1 Referenced Control Point Index</summary>
		public readonly static DicomTag ReferencedControlPointIndex = new DicomTag(0x300c, 0x00f0);

		///<summary>(300c,00f2) VR=SQ VM=1 Referenced Control Point Sequence</summary>
		public readonly static DicomTag ReferencedControlPointSequence = new DicomTag(0x300c, 0x00f2);

		///<summary>(300c,00f4) VR=IS VM=1 Referenced Start Control Point Index</summary>
		public readonly static DicomTag ReferencedStartControlPointIndex = new DicomTag(0x300c, 0x00f4);

		///<summary>(300c,00f6) VR=IS VM=1 Referenced Stop Control Point Index</summary>
		public readonly static DicomTag ReferencedStopControlPointIndex = new DicomTag(0x300c, 0x00f6);

		///<summary>(300c,0100) VR=IS VM=1 Referenced Range Shifter Number</summary>
		public readonly static DicomTag ReferencedRangeShifterNumber = new DicomTag(0x300c, 0x0100);

		///<summary>(300c,0102) VR=IS VM=1 Referenced Lateral Spreading Device Number</summary>
		public readonly static DicomTag ReferencedLateralSpreadingDeviceNumber = new DicomTag(0x300c, 0x0102);

		///<summary>(300c,0104) VR=IS VM=1 Referenced Range Modulator Number</summary>
		public readonly static DicomTag ReferencedRangeModulatorNumber = new DicomTag(0x300c, 0x0104);

		///<summary>(300e,0002) VR=CS VM=1 Approval Status</summary>
		public readonly static DicomTag ApprovalStatus = new DicomTag(0x300e, 0x0002);

		///<summary>(300e,0004) VR=DA VM=1 Review Date</summary>
		public readonly static DicomTag ReviewDate = new DicomTag(0x300e, 0x0004);

		///<summary>(300e,0005) VR=TM VM=1 Review Time</summary>
		public readonly static DicomTag ReviewTime = new DicomTag(0x300e, 0x0005);

		///<summary>(300e,0008) VR=PN VM=1 Reviewer Name</summary>
		public readonly static DicomTag ReviewerName = new DicomTag(0x300e, 0x0008);

		///<summary>(4000,0010) VR=LT VM=1 Arbitrary (RETIRED)</summary>
		public readonly static DicomTag ArbitraryRETIRED = new DicomTag(0x4000, 0x0010);

		///<summary>(4000,4000) VR=LT VM=1 Text Comments (RETIRED)</summary>
		public readonly static DicomTag TextCommentsRETIRED = new DicomTag(0x4000, 0x4000);

		///<summary>(4008,0040) VR=SH VM=1 Results ID (RETIRED)</summary>
		public readonly static DicomTag ResultsIDRETIRED = new DicomTag(0x4008, 0x0040);

		///<summary>(4008,0042) VR=LO VM=1 Results ID Issuer (RETIRED)</summary>
		public readonly static DicomTag ResultsIDIssuerRETIRED = new DicomTag(0x4008, 0x0042);

		///<summary>(4008,0050) VR=SQ VM=1 Referenced Interpretation Sequence (RETIRED)</summary>
		public readonly static DicomTag ReferencedInterpretationSequenceRETIRED = new DicomTag(0x4008, 0x0050);

		///<summary>(4008,00ff) VR=CS VM=1 Report Production Status (Trial) (RETIRED)</summary>
		public readonly static DicomTag ReportProductionStatusTrialRETIRED = new DicomTag(0x4008, 0x00ff);

		///<summary>(4008,0100) VR=DA VM=1 Interpretation Recorded Date (RETIRED)</summary>
		public readonly static DicomTag InterpretationRecordedDateRETIRED = new DicomTag(0x4008, 0x0100);

		///<summary>(4008,0101) VR=TM VM=1 Interpretation Recorded Time (RETIRED)</summary>
		public readonly static DicomTag InterpretationRecordedTimeRETIRED = new DicomTag(0x4008, 0x0101);

		///<summary>(4008,0102) VR=PN VM=1 Interpretation Recorder (RETIRED)</summary>
		public readonly static DicomTag InterpretationRecorderRETIRED = new DicomTag(0x4008, 0x0102);

		///<summary>(4008,0103) VR=LO VM=1 Reference to Recorded Sound (RETIRED)</summary>
		public readonly static DicomTag ReferenceToRecordedSoundRETIRED = new DicomTag(0x4008, 0x0103);

		///<summary>(4008,0108) VR=DA VM=1 Interpretation Transcription Date (RETIRED)</summary>
		public readonly static DicomTag InterpretationTranscriptionDateRETIRED = new DicomTag(0x4008, 0x0108);

		///<summary>(4008,0109) VR=TM VM=1 Interpretation Transcription Time (RETIRED)</summary>
		public readonly static DicomTag InterpretationTranscriptionTimeRETIRED = new DicomTag(0x4008, 0x0109);

		///<summary>(4008,010a) VR=PN VM=1 Interpretation Transcriber (RETIRED)</summary>
		public readonly static DicomTag InterpretationTranscriberRETIRED = new DicomTag(0x4008, 0x010a);

		///<summary>(4008,010b) VR=ST VM=1 Interpretation Text (RETIRED)</summary>
		public readonly static DicomTag InterpretationTextRETIRED = new DicomTag(0x4008, 0x010b);

		///<summary>(4008,010c) VR=PN VM=1 Interpretation Author (RETIRED)</summary>
		public readonly static DicomTag InterpretationAuthorRETIRED = new DicomTag(0x4008, 0x010c);

		///<summary>(4008,0111) VR=SQ VM=1 Interpretation Approver Sequence (RETIRED)</summary>
		public readonly static DicomTag InterpretationApproverSequenceRETIRED = new DicomTag(0x4008, 0x0111);

		///<summary>(4008,0112) VR=DA VM=1 Interpretation Approval Date (RETIRED)</summary>
		public readonly static DicomTag InterpretationApprovalDateRETIRED = new DicomTag(0x4008, 0x0112);

		///<summary>(4008,0113) VR=TM VM=1 Interpretation Approval Time (RETIRED)</summary>
		public readonly static DicomTag InterpretationApprovalTimeRETIRED = new DicomTag(0x4008, 0x0113);

		///<summary>(4008,0114) VR=PN VM=1 Physician Approving Interpretation (RETIRED)</summary>
		public readonly static DicomTag PhysicianApprovingInterpretationRETIRED = new DicomTag(0x4008, 0x0114);

		///<summary>(4008,0115) VR=LT VM=1 Interpretation Diagnosis Description (RETIRED)</summary>
		public readonly static DicomTag InterpretationDiagnosisDescriptionRETIRED = new DicomTag(0x4008, 0x0115);

		///<summary>(4008,0117) VR=SQ VM=1 Interpretation Diagnosis Code Sequence (RETIRED)</summary>
		public readonly static DicomTag InterpretationDiagnosisCodeSequenceRETIRED = new DicomTag(0x4008, 0x0117);

		///<summary>(4008,0118) VR=SQ VM=1 Results Distribution List Sequence (RETIRED)</summary>
		public readonly static DicomTag ResultsDistributionListSequenceRETIRED = new DicomTag(0x4008, 0x0118);

		///<summary>(4008,0119) VR=PN VM=1 Distribution Name (RETIRED)</summary>
		public readonly static DicomTag DistributionNameRETIRED = new DicomTag(0x4008, 0x0119);

		///<summary>(4008,011a) VR=LO VM=1 Distribution Address (RETIRED)</summary>
		public readonly static DicomTag DistributionAddressRETIRED = new DicomTag(0x4008, 0x011a);

		///<summary>(4008,0200) VR=SH VM=1 Interpretation ID (RETIRED)</summary>
		public readonly static DicomTag InterpretationIDRETIRED = new DicomTag(0x4008, 0x0200);

		///<summary>(4008,0202) VR=LO VM=1 Interpretation ID Issuer (RETIRED)</summary>
		public readonly static DicomTag InterpretationIDIssuerRETIRED = new DicomTag(0x4008, 0x0202);

		///<summary>(4008,0210) VR=CS VM=1 Interpretation Type ID (RETIRED)</summary>
		public readonly static DicomTag InterpretationTypeIDRETIRED = new DicomTag(0x4008, 0x0210);

		///<summary>(4008,0212) VR=CS VM=1 Interpretation Status ID (RETIRED)</summary>
		public readonly static DicomTag InterpretationStatusIDRETIRED = new DicomTag(0x4008, 0x0212);

		///<summary>(4008,0300) VR=ST VM=1 Impressions (RETIRED)</summary>
		public readonly static DicomTag ImpressionsRETIRED = new DicomTag(0x4008, 0x0300);

		///<summary>(4008,4000) VR=ST VM=1 Results Comments (RETIRED)</summary>
		public readonly static DicomTag ResultsCommentsRETIRED = new DicomTag(0x4008, 0x4000);

		///<summary>(4010,0001) VR=CS VM=1 Low Energy Detectors</summary>
		public readonly static DicomTag LowEnergyDetectors = new DicomTag(0x4010, 0x0001);

		///<summary>(4010,0002) VR=CS VM=1 High Energy Detectors</summary>
		public readonly static DicomTag HighEnergyDetectors = new DicomTag(0x4010, 0x0002);

		///<summary>(4010,0004) VR=SQ VM=1 Detector Geometry Sequence</summary>
		public readonly static DicomTag DetectorGeometrySequence = new DicomTag(0x4010, 0x0004);

		///<summary>(4010,1001) VR=SQ VM=1 Threat ROI Voxel Sequence</summary>
		public readonly static DicomTag ThreatROIVoxelSequence = new DicomTag(0x4010, 0x1001);

		///<summary>(4010,1004) VR=FL VM=3 Threat ROI Base</summary>
		public readonly static DicomTag ThreatROIBase = new DicomTag(0x4010, 0x1004);

		///<summary>(4010,1005) VR=FL VM=3 Threat ROI Extents</summary>
		public readonly static DicomTag ThreatROIExtents = new DicomTag(0x4010, 0x1005);

		///<summary>(4010,1006) VR=OB VM=1 Threat ROI Bitmap</summary>
		public readonly static DicomTag ThreatROIBitmap = new DicomTag(0x4010, 0x1006);

		///<summary>(4010,1007) VR=SH VM=1 Route Segment ID</summary>
		public readonly static DicomTag RouteSegmentID = new DicomTag(0x4010, 0x1007);

		///<summary>(4010,1008) VR=CS VM=1 Gantry Type</summary>
		public readonly static DicomTag GantryType = new DicomTag(0x4010, 0x1008);

		///<summary>(4010,1009) VR=CS VM=1 OOI Owner Type</summary>
		public readonly static DicomTag OOIOwnerType = new DicomTag(0x4010, 0x1009);

		///<summary>(4010,100a) VR=SQ VM=1 Route Segment Sequence</summary>
		public readonly static DicomTag RouteSegmentSequence = new DicomTag(0x4010, 0x100a);

		///<summary>(4010,1010) VR=US VM=1 Potential Threat Object ID</summary>
		public readonly static DicomTag PotentialThreatObjectID = new DicomTag(0x4010, 0x1010);

		///<summary>(4010,1011) VR=SQ VM=1 Threat Sequence</summary>
		public readonly static DicomTag ThreatSequence = new DicomTag(0x4010, 0x1011);

		///<summary>(4010,1012) VR=CS VM=1 Threat Category</summary>
		public readonly static DicomTag ThreatCategory = new DicomTag(0x4010, 0x1012);

		///<summary>(4010,1013) VR=LT VM=1 Threat Category Description</summary>
		public readonly static DicomTag ThreatCategoryDescription = new DicomTag(0x4010, 0x1013);

		///<summary>(4010,1014) VR=CS VM=1 ATD Ability Assessment</summary>
		public readonly static DicomTag ATDAbilityAssessment = new DicomTag(0x4010, 0x1014);

		///<summary>(4010,1015) VR=CS VM=1 ATD Assessment Flag</summary>
		public readonly static DicomTag ATDAssessmentFlag = new DicomTag(0x4010, 0x1015);

		///<summary>(4010,1016) VR=FL VM=1 ATD Assessment Probability</summary>
		public readonly static DicomTag ATDAssessmentProbability = new DicomTag(0x4010, 0x1016);

		///<summary>(4010,1017) VR=FL VM=1 Mass</summary>
		public readonly static DicomTag Mass = new DicomTag(0x4010, 0x1017);

		///<summary>(4010,1018) VR=FL VM=1 Density</summary>
		public readonly static DicomTag Density = new DicomTag(0x4010, 0x1018);

		///<summary>(4010,1019) VR=FL VM=1 Z Effective</summary>
		public readonly static DicomTag ZEffective = new DicomTag(0x4010, 0x1019);

		///<summary>(4010,101a) VR=SH VM=1 Boarding Pass ID</summary>
		public readonly static DicomTag BoardingPassID = new DicomTag(0x4010, 0x101a);

		///<summary>(4010,101b) VR=FL VM=3 Center of Mass</summary>
		public readonly static DicomTag CenterOfMass = new DicomTag(0x4010, 0x101b);

		///<summary>(4010,101c) VR=FL VM=3 Center of PTO</summary>
		public readonly static DicomTag CenterOfPTO = new DicomTag(0x4010, 0x101c);

		///<summary>(4010,101d) VR=FL VM=6-n Bounding Polygon</summary>
		public readonly static DicomTag BoundingPolygon = new DicomTag(0x4010, 0x101d);

		///<summary>(4010,101e) VR=SH VM=1 Route Segment Start Location ID</summary>
		public readonly static DicomTag RouteSegmentStartLocationID = new DicomTag(0x4010, 0x101e);

		///<summary>(4010,101f) VR=SH VM=1 Route Segment End Location ID</summary>
		public readonly static DicomTag RouteSegmentEndLocationID = new DicomTag(0x4010, 0x101f);

		///<summary>(4010,1020) VR=CS VM=1 Route Segment Location ID Type</summary>
		public readonly static DicomTag RouteSegmentLocationIDType = new DicomTag(0x4010, 0x1020);

		///<summary>(4010,1021) VR=CS VM=1-n Abort Reason</summary>
		public readonly static DicomTag AbortReason = new DicomTag(0x4010, 0x1021);

		///<summary>(4010,1023) VR=FL VM=1 Volume of PTO</summary>
		public readonly static DicomTag VolumeOfPTO = new DicomTag(0x4010, 0x1023);

		///<summary>(4010,1024) VR=CS VM=1 Abort Flag</summary>
		public readonly static DicomTag AbortFlag = new DicomTag(0x4010, 0x1024);

		///<summary>(4010,1025) VR=DT VM=1 Route Segment Start Time</summary>
		public readonly static DicomTag RouteSegmentStartTime = new DicomTag(0x4010, 0x1025);

		///<summary>(4010,1026) VR=DT VM=1 Route Segment End Time</summary>
		public readonly static DicomTag RouteSegmentEndTime = new DicomTag(0x4010, 0x1026);

		///<summary>(4010,1027) VR=CS VM=1 TDR Type</summary>
		public readonly static DicomTag TDRType = new DicomTag(0x4010, 0x1027);

		///<summary>(4010,1028) VR=CS VM=1 International Route Segment</summary>
		public readonly static DicomTag InternationalRouteSegment = new DicomTag(0x4010, 0x1028);

		///<summary>(4010,1029) VR=LO VM=1-n Threat Detection Algorithm and Version</summary>
		public readonly static DicomTag ThreatDetectionAlgorithmandVersion = new DicomTag(0x4010, 0x1029);

		///<summary>(4010,102a) VR=SH VM=1 Assigned Location</summary>
		public readonly static DicomTag AssignedLocation = new DicomTag(0x4010, 0x102a);

		///<summary>(4010,102b) VR=DT VM=1 Alarm Decision Time</summary>
		public readonly static DicomTag AlarmDecisionTime = new DicomTag(0x4010, 0x102b);

		///<summary>(4010,1031) VR=CS VM=1 Alarm Decision</summary>
		public readonly static DicomTag AlarmDecision = new DicomTag(0x4010, 0x1031);

		///<summary>(4010,1033) VR=US VM=1 Number of Total Objects</summary>
		public readonly static DicomTag NumberOfTotalObjects = new DicomTag(0x4010, 0x1033);

		///<summary>(4010,1034) VR=US VM=1 Number of Alarm Objects</summary>
		public readonly static DicomTag NumberOfAlarmObjects = new DicomTag(0x4010, 0x1034);

		///<summary>(4010,1037) VR=SQ VM=1 PTO Representation Sequence</summary>
		public readonly static DicomTag PTORepresentationSequence = new DicomTag(0x4010, 0x1037);

		///<summary>(4010,1038) VR=SQ VM=1 ATD Assessment Sequence</summary>
		public readonly static DicomTag ATDAssessmentSequence = new DicomTag(0x4010, 0x1038);

		///<summary>(4010,1039) VR=CS VM=1 TIP Type</summary>
		public readonly static DicomTag TIPType = new DicomTag(0x4010, 0x1039);

		///<summary>(4010,103a) VR=CS VM=1 DICOS Version</summary>
		public readonly static DicomTag DICOSVersion = new DicomTag(0x4010, 0x103a);

		///<summary>(4010,1041) VR=DT VM=1 OOI Owner Creation Time</summary>
		public readonly static DicomTag OOIOwnerCreationTime = new DicomTag(0x4010, 0x1041);

		///<summary>(4010,1042) VR=CS VM=1 OOI Type</summary>
		public readonly static DicomTag OOIType = new DicomTag(0x4010, 0x1042);

		///<summary>(4010,1043) VR=FL VM=3 OOI Size</summary>
		public readonly static DicomTag OOISize = new DicomTag(0x4010, 0x1043);

		///<summary>(4010,1044) VR=CS VM=1 Acquisition Status</summary>
		public readonly static DicomTag AcquisitionStatus = new DicomTag(0x4010, 0x1044);

		///<summary>(4010,1045) VR=SQ VM=1 Basis Materials Code Sequence</summary>
		public readonly static DicomTag BasisMaterialsCodeSequence = new DicomTag(0x4010, 0x1045);

		///<summary>(4010,1046) VR=CS VM=1 Phantom Type</summary>
		public readonly static DicomTag PhantomType = new DicomTag(0x4010, 0x1046);

		///<summary>(4010,1047) VR=SQ VM=1 OOI Owner Sequence</summary>
		public readonly static DicomTag OOIOwnerSequence = new DicomTag(0x4010, 0x1047);

		///<summary>(4010,1048) VR=CS VM=1 Scan Type</summary>
		public readonly static DicomTag ScanType = new DicomTag(0x4010, 0x1048);

		///<summary>(4010,1051) VR=LO VM=1 Itinerary ID</summary>
		public readonly static DicomTag ItineraryID = new DicomTag(0x4010, 0x1051);

		///<summary>(4010,1052) VR=SH VM=1 Itinerary ID Type</summary>
		public readonly static DicomTag ItineraryIDType = new DicomTag(0x4010, 0x1052);

		///<summary>(4010,1053) VR=LO VM=1 Itinerary ID Assigning Authority</summary>
		public readonly static DicomTag ItineraryIDAssigningAuthority = new DicomTag(0x4010, 0x1053);

		///<summary>(4010,1054) VR=SH VM=1 Route ID</summary>
		public readonly static DicomTag RouteID = new DicomTag(0x4010, 0x1054);

		///<summary>(4010,1055) VR=SH VM=1 Route ID Assigning Authority</summary>
		public readonly static DicomTag RouteIDAssigningAuthority = new DicomTag(0x4010, 0x1055);

		///<summary>(4010,1056) VR=CS VM=1 Inbound  Arrival Type</summary>
		public readonly static DicomTag InboundArrivalType = new DicomTag(0x4010, 0x1056);

		///<summary>(4010,1058) VR=SH VM=1 Carrier ID</summary>
		public readonly static DicomTag CarrierID = new DicomTag(0x4010, 0x1058);

		///<summary>(4010,1059) VR=CS VM=1 Carrier ID Assigning Authority</summary>
		public readonly static DicomTag CarrierIDAssigningAuthority = new DicomTag(0x4010, 0x1059);

		///<summary>(4010,1060) VR=FL VM=3 Source Orientation</summary>
		public readonly static DicomTag SourceOrientation = new DicomTag(0x4010, 0x1060);

		///<summary>(4010,1061) VR=FL VM=3 Source Position</summary>
		public readonly static DicomTag SourcePosition = new DicomTag(0x4010, 0x1061);

		///<summary>(4010,1062) VR=FL VM=1 Belt Height</summary>
		public readonly static DicomTag BeltHeight = new DicomTag(0x4010, 0x1062);

		///<summary>(4010,1064) VR=SQ VM=1 Algorithm Routing Code Sequence</summary>
		public readonly static DicomTag AlgorithmRoutingCodeSequence = new DicomTag(0x4010, 0x1064);

		///<summary>(4010,1067) VR=CS VM=1 Transport Classification</summary>
		public readonly static DicomTag TransportClassification = new DicomTag(0x4010, 0x1067);

		///<summary>(4010,1068) VR=LT VM=1 OOI Type Descriptor</summary>
		public readonly static DicomTag OOITypeDescriptor = new DicomTag(0x4010, 0x1068);

		///<summary>(4010,1069) VR=FL VM=1 Total Processing Time</summary>
		public readonly static DicomTag TotalProcessingTime = new DicomTag(0x4010, 0x1069);

		///<summary>(4010,106c) VR=OB VM=1 Detector Calibration Data</summary>
		public readonly static DicomTag DetectorCalibrationData = new DicomTag(0x4010, 0x106c);

		///<summary>(4ffe,0001) VR=SQ VM=1 MAC Parameters Sequence</summary>
		public readonly static DicomTag MACParametersSequence = new DicomTag(0x4ffe, 0x0001);

		///<summary>(5200,9229) VR=SQ VM=1 Shared Functional Groups Sequence</summary>
		public readonly static DicomTag SharedFunctionalGroupsSequence = new DicomTag(0x5200, 0x9229);

		///<summary>(5200,9230) VR=SQ VM=1 Per-frame Functional Groups Sequence</summary>
		public readonly static DicomTag PerFrameFunctionalGroupsSequence = new DicomTag(0x5200, 0x9230);

		///<summary>(5400,0100) VR=SQ VM=1 Waveform Sequence</summary>
		public readonly static DicomTag WaveformSequence = new DicomTag(0x5400, 0x0100);

		///<summary>(5400,0110) VR=OB/OW VM=1 Channel Minimum Value</summary>
		public readonly static DicomTag ChannelMinimumValue = new DicomTag(0x5400, 0x0110);

		///<summary>(5400,0112) VR=OB/OW VM=1 Channel Maximum Value</summary>
		public readonly static DicomTag ChannelMaximumValue = new DicomTag(0x5400, 0x0112);

		///<summary>(5400,1004) VR=US VM=1 Waveform Bits Allocated</summary>
		public readonly static DicomTag WaveformBitsAllocated = new DicomTag(0x5400, 0x1004);

		///<summary>(5400,1006) VR=CS VM=1 Waveform Sample Interpretation</summary>
		public readonly static DicomTag WaveformSampleInterpretation = new DicomTag(0x5400, 0x1006);

		///<summary>(5400,100a) VR=OB/OW VM=1 Waveform Padding Value</summary>
		public readonly static DicomTag WaveformPaddingValue = new DicomTag(0x5400, 0x100a);

		///<summary>(5400,1010) VR=OB/OW VM=1 Waveform Data</summary>
		public readonly static DicomTag WaveformData = new DicomTag(0x5400, 0x1010);

		///<summary>(5600,0010) VR=OF VM=1 First Order Phase Correction Angle</summary>
		public readonly static DicomTag FirstOrderPhaseCorrectionAngle = new DicomTag(0x5600, 0x0010);

		///<summary>(5600,0020) VR=OF VM=1 Spectroscopy Data</summary>
		public readonly static DicomTag SpectroscopyData = new DicomTag(0x5600, 0x0020);

		///<summary>(7fe0,0010) VR=OW/OB VM=1 Pixel Data</summary>
		public readonly static DicomTag PixelData = new DicomTag(0x7fe0, 0x0010);

		///<summary>(7fe0,0020) VR=OW VM=1 Coefficients SDVN (RETIRED)</summary>
		public readonly static DicomTag CoefficientsSDVNRETIRED = new DicomTag(0x7fe0, 0x0020);

		///<summary>(7fe0,0030) VR=OW VM=1 Coefficients SDHN (RETIRED)</summary>
		public readonly static DicomTag CoefficientsSDHNRETIRED = new DicomTag(0x7fe0, 0x0030);

		///<summary>(7fe0,0040) VR=OW VM=1 Coefficients SDDN (RETIRED)</summary>
		public readonly static DicomTag CoefficientsSDDNRETIRED = new DicomTag(0x7fe0, 0x0040);

		///<summary>(fffa,fffa) VR=SQ VM=1 Digital Signatures Sequence</summary>
		public readonly static DicomTag DigitalSignaturesSequence = new DicomTag(0xfffa, 0xfffa);

		///<summary>(fffc,fffc) VR=OB VM=1 Data Set Trailing Padding</summary>
		public readonly static DicomTag DataSetTrailingPadding = new DicomTag(0xfffc, 0xfffc);

		///<summary>(fffe,e000) VR=NONE VM=1 Item</summary>
		public readonly static DicomTag Item = new DicomTag(0xfffe, 0xe000);

		///<summary>(fffe,e00d) VR=NONE VM=1 Item Delimitation Item</summary>
		public readonly static DicomTag ItemDelimitationItem = new DicomTag(0xfffe, 0xe00d);

		///<summary>(fffe,e0dd) VR=NONE VM=1 Sequence Delimitation Item</summary>
		public readonly static DicomTag SequenceDelimitationItem = new DicomTag(0xfffe, 0xe0dd);

		///<summary>(5000,200a) VR=UL VM=1 Total Time (RETIRED)</summary>
		public readonly static DicomTag TotalTimeRETIRED = new DicomTag(0x5000, 0x200a);

		///<summary>(5000,200c) VR=OW/OB VM=1 Audio Sample Data (RETIRED)</summary>
		public readonly static DicomTag AudioSampleDataRETIRED = new DicomTag(0x5000, 0x200c);

		///<summary>(5000,200e) VR=LT VM=1 Audio Comments (RETIRED)</summary>
		public readonly static DicomTag AudioCommentsRETIRED = new DicomTag(0x5000, 0x200e);

		///<summary>(6000,0062) VR=SH VM=1 Overlay Compression Label (RETIRED)</summary>
		public readonly static DicomTag OverlayCompressionLabelRETIRED = new DicomTag(0x6000, 0x0062);

		///<summary>(6000,0063) VR=CS VM=1 Overlay Compression Description (RETIRED)</summary>
		public readonly static DicomTag OverlayCompressionDescriptionRETIRED = new DicomTag(0x6000, 0x0063);

		///<summary>(6000,0066) VR=AT VM=1-n Overlay Compression Step Pointers (RETIRED)</summary>
		public readonly static DicomTag OverlayCompressionStepPointersRETIRED = new DicomTag(0x6000, 0x0066);

		///<summary>(6000,0102) VR=US VM=1 Overlay Bit Position</summary>
		public readonly static DicomTag OverlayBitPosition = new DicomTag(0x6000, 0x0102);

		///<summary>(6000,0110) VR=CS VM=1 Overlay Format (RETIRED)</summary>
		public readonly static DicomTag OverlayFormatRETIRED = new DicomTag(0x6000, 0x0110);

		///<summary>(6000,0200) VR=US VM=1 Overlay Location (RETIRED)</summary>
		public readonly static DicomTag OverlayLocationRETIRED = new DicomTag(0x6000, 0x0200);

		///<summary>(6000,0068) VR=US VM=1 Overlay Repeat Interval (RETIRED)</summary>
		public readonly static DicomTag OverlayRepeatIntervalRETIRED = new DicomTag(0x6000, 0x0068);

		///<summary>(6000,0069) VR=US VM=1 Overlay Bits Grouped (RETIRED)</summary>
		public readonly static DicomTag OverlayBitsGroupedRETIRED = new DicomTag(0x6000, 0x0069);

		///<summary>(6000,0100) VR=US VM=1 Overlay Bits Allocated</summary>
		public readonly static DicomTag OverlayBitsAllocated = new DicomTag(0x6000, 0x0100);

		///<summary>(6000,0061) VR=SH VM=1 Overlay Compression Originator (RETIRED)</summary>
		public readonly static DicomTag OverlayCompressionOriginatorRETIRED = new DicomTag(0x6000, 0x0061);

		///<summary>(6000,0022) VR=LO VM=1 Overlay Description</summary>
		public readonly static DicomTag OverlayDescription = new DicomTag(0x6000, 0x0022);

		///<summary>(6000,0040) VR=CS VM=1 Overlay Type</summary>
		public readonly static DicomTag OverlayType = new DicomTag(0x6000, 0x0040);

		///<summary>(6000,0012) VR=US VM=1 Overlay Planes (RETIRED)</summary>
		public readonly static DicomTag OverlayPlanesRETIRED = new DicomTag(0x6000, 0x0012);

		///<summary>(6000,0015) VR=IS VM=1 Number of Frames in Overlay</summary>
		public readonly static DicomTag NumberOfFramesInOverlay = new DicomTag(0x6000, 0x0015);

		///<summary>(6000,0045) VR=LO VM=1 Overlay Subtype</summary>
		public readonly static DicomTag OverlaySubtype = new DicomTag(0x6000, 0x0045);

		///<summary>(6000,0052) VR=US VM=1 Overlay Plane Origin (RETIRED)</summary>
		public readonly static DicomTag OverlayPlaneOriginRETIRED = new DicomTag(0x6000, 0x0052);

		///<summary>(6000,0060) VR=CS VM=1 Overlay Compression Code (RETIRED)</summary>
		public readonly static DicomTag OverlayCompressionCodeRETIRED = new DicomTag(0x6000, 0x0060);

		///<summary>(6000,0050) VR=SS VM=2 Overlay Origin</summary>
		public readonly static DicomTag OverlayOrigin = new DicomTag(0x6000, 0x0050);

		///<summary>(6000,0051) VR=US VM=1 Image Frame Origin</summary>
		public readonly static DicomTag ImageFrameOrigin = new DicomTag(0x6000, 0x0051);

		///<summary>(6000,0800) VR=CS VM=1-n Overlay Code Label (RETIRED)</summary>
		public readonly static DicomTag OverlayCodeLabelRETIRED = new DicomTag(0x6000, 0x0800);

		///<summary>(6000,1303) VR=DS VM=1 ROI Standard Deviation</summary>
		public readonly static DicomTag ROIStandardDeviation = new DicomTag(0x6000, 0x1303);

		///<summary>(6000,1500) VR=LO VM=1 Overlay Label</summary>
		public readonly static DicomTag OverlayLabel = new DicomTag(0x6000, 0x1500);

		///<summary>(6000,1302) VR=DS VM=1 ROI Mean</summary>
		public readonly static DicomTag ROIMean = new DicomTag(0x6000, 0x1302);

		///<summary>(6000,1203) VR=US VM=1-n Overlays - Blue (RETIRED)</summary>
		public readonly static DicomTag OverlaysBlueRETIRED = new DicomTag(0x6000, 0x1203);

		///<summary>(6000,1301) VR=IS VM=1 ROI Area</summary>
		public readonly static DicomTag ROIArea = new DicomTag(0x6000, 0x1301);

		///<summary>(6000,3000) VR=OB/OW VM=1 Overlay Data</summary>
		public readonly static DicomTag OverlayData = new DicomTag(0x6000, 0x3000);

		///<summary>(7f00,0020) VR=OW VM=1 Variable Coefficients SDVN (RETIRED)</summary>
		public readonly static DicomTag VariableCoefficientsSDVNRETIRED = new DicomTag(0x7f00, 0x0020);

		///<summary>(7f00,0030) VR=OW VM=1 Variable Coefficients SDHN (RETIRED)</summary>
		public readonly static DicomTag VariableCoefficientsSDHNRETIRED = new DicomTag(0x7f00, 0x0030);

		///<summary>(7f00,0011) VR=US VM=1 Variable Next Data Group (RETIRED)</summary>
		public readonly static DicomTag VariableNextDataGroupRETIRED = new DicomTag(0x7f00, 0x0011);

		///<summary>(6000,4000) VR=LT VM=1 Overlay Comments (RETIRED)</summary>
		public readonly static DicomTag OverlayCommentsRETIRED = new DicomTag(0x6000, 0x4000);

		///<summary>(7f00,0010) VR=OW/OB VM=1 Variable Pixel Data (RETIRED)</summary>
		public readonly static DicomTag VariablePixelDataRETIRED = new DicomTag(0x7f00, 0x0010);

		///<summary>(6000,1001) VR=CS VM=1 Overlay Activation Layer</summary>
		public readonly static DicomTag OverlayActivationLayer = new DicomTag(0x6000, 0x1001);

		///<summary>(6000,1100) VR=US VM=1 Overlay Descriptor - Gray (RETIRED)</summary>
		public readonly static DicomTag OverlayDescriptorGrayRETIRED = new DicomTag(0x6000, 0x1100);

		///<summary>(6000,0804) VR=US VM=1 Overlay Bits For Code Word (RETIRED)</summary>
		public readonly static DicomTag OverlayBitsForCodeWordRETIRED = new DicomTag(0x6000, 0x0804);

		///<summary>(6000,0802) VR=US VM=1 Overlay Number of Tables (RETIRED)</summary>
		public readonly static DicomTag OverlayNumberOfTablesRETIRED = new DicomTag(0x6000, 0x0802);

		///<summary>(6000,0803) VR=AT VM=1-n Overlay Code Table Location (RETIRED)</summary>
		public readonly static DicomTag OverlayCodeTableLocationRETIRED = new DicomTag(0x6000, 0x0803);

		///<summary>(6000,1101) VR=US VM=1 Overlay Descriptor - Red (RETIRED)</summary>
		public readonly static DicomTag OverlayDescriptorRedRETIRED = new DicomTag(0x6000, 0x1101);

		///<summary>(6000,1201) VR=US VM=1-n Overlays - Red (RETIRED)</summary>
		public readonly static DicomTag OverlaysRedRETIRED = new DicomTag(0x6000, 0x1201);

		///<summary>(6000,1202) VR=US VM=1-n Overlays - Green (RETIRED)</summary>
		public readonly static DicomTag OverlaysGreenRETIRED = new DicomTag(0x6000, 0x1202);

		///<summary>(6000,1200) VR=US VM=1-n Overlays - Gray (RETIRED)</summary>
		public readonly static DicomTag OverlaysGrayRETIRED = new DicomTag(0x6000, 0x1200);

		///<summary>(6000,1102) VR=US VM=1 Overlay Descriptor - Green (RETIRED)</summary>
		public readonly static DicomTag OverlayDescriptorGreenRETIRED = new DicomTag(0x6000, 0x1102);

		///<summary>(6000,1103) VR=US VM=1 Overlay Descriptor - Blue (RETIRED)</summary>
		public readonly static DicomTag OverlayDescriptorBlueRETIRED = new DicomTag(0x6000, 0x1103);

		///<summary>(5000,0105) VR=US VM=1-n Maximum Coordinate Value (RETIRED)</summary>
		public readonly static DicomTag MaximumCoordinateValueRETIRED = new DicomTag(0x5000, 0x0105);

		///<summary>(5000,0104) VR=US VM=1-n Minimum Coordinate Value (RETIRED)</summary>
		public readonly static DicomTag MinimumCoordinateValueRETIRED = new DicomTag(0x5000, 0x0104);

		///<summary>(5000,0103) VR=US VM=1 Data Value Representation (RETIRED)</summary>
		public readonly static DicomTag DataValueRepresentationRETIRED = new DicomTag(0x5000, 0x0103);

		///<summary>(5000,0112) VR=US VM=1-n Coordinate Start Value (RETIRED)</summary>
		public readonly static DicomTag CoordinateStartValueRETIRED = new DicomTag(0x5000, 0x0112);

		///<summary>(5000,0110) VR=US VM=1-n Curve Data Descriptor (RETIRED)</summary>
		public readonly static DicomTag CurveDataDescriptorRETIRED = new DicomTag(0x5000, 0x0110);

		///<summary>(5000,0106) VR=SH VM=1-n Curve Range (RETIRED)</summary>
		public readonly static DicomTag CurveRangeRETIRED = new DicomTag(0x5000, 0x0106);

		///<summary>(5000,0040) VR=SH VM=1-n Axis Labels (RETIRED)</summary>
		public readonly static DicomTag AxisLabelsRETIRED = new DicomTag(0x5000, 0x0040);

		///<summary>(5000,0010) VR=US VM=1 Number of Points (RETIRED)</summary>
		public readonly static DicomTag NumberOfPointsRETIRED = new DicomTag(0x5000, 0x0010);

		///<summary>(5000,0005) VR=US VM=1 Curve Dimensions (RETIRED)</summary>
		public readonly static DicomTag CurveDimensionsRETIRED = new DicomTag(0x5000, 0x0005);

		///<summary>(6000,0010) VR=US VM=1 Overlay Rows</summary>
		public readonly static DicomTag OverlayRows = new DicomTag(0x6000, 0x0010);

		///<summary>(5000,0030) VR=SH VM=1-n Axis Units (RETIRED)</summary>
		public readonly static DicomTag AxisUnitsRETIRED = new DicomTag(0x5000, 0x0030);

		///<summary>(5000,0022) VR=LO VM=1 Curve Description (RETIRED)</summary>
		public readonly static DicomTag CurveDescriptionRETIRED = new DicomTag(0x5000, 0x0022);

		///<summary>(5000,0020) VR=CS VM=1 Type of Data (RETIRED)</summary>
		public readonly static DicomTag TypeOfDataRETIRED = new DicomTag(0x5000, 0x0020);

		///<summary>(5000,2610) VR=US VM=1 Curve Referenced Overlay Group (RETIRED)</summary>
		public readonly static DicomTag CurveReferencedOverlayGroupRETIRED = new DicomTag(0x5000, 0x2610);

		///<summary>(5000,2600) VR=SQ VM=1 Curve Referenced Overlay Sequence (RETIRED)</summary>
		public readonly static DicomTag CurveReferencedOverlaySequenceRETIRED = new DicomTag(0x5000, 0x2600);

		///<summary>(5000,2500) VR=LO VM=1 Curve Label (RETIRED)</summary>
		public readonly static DicomTag CurveLabelRETIRED = new DicomTag(0x5000, 0x2500);

		///<summary>(6000,0011) VR=US VM=1 Overlay Columns</summary>
		public readonly static DicomTag OverlayColumns = new DicomTag(0x6000, 0x0011);

		///<summary>(7f00,0040) VR=OW VM=1 Variable Coefficients SDDN (RETIRED)</summary>
		public readonly static DicomTag VariableCoefficientsSDDNRETIRED = new DicomTag(0x7f00, 0x0040);

		///<summary>(5000,3000) VR=OW/OB VM=1 Curve Data (RETIRED)</summary>
		public readonly static DicomTag CurveDataRETIRED = new DicomTag(0x5000, 0x3000);

		///<summary>(5000,2008) VR=UL VM=1 Sample Rate (RETIRED)</summary>
		public readonly static DicomTag SampleRateRETIRED = new DicomTag(0x5000, 0x2008);

		///<summary>(5000,2000) VR=US VM=1 Audio Type (RETIRED)</summary>
		public readonly static DicomTag AudioTypeRETIRED = new DicomTag(0x5000, 0x2000);

		///<summary>(5000,1001) VR=CS VM=1 Curve Activation Layer (RETIRED)</summary>
		public readonly static DicomTag CurveActivationLayerRETIRED = new DicomTag(0x5000, 0x1001);

		///<summary>(5000,0114) VR=US VM=1-n Coordinate Step Value (RETIRED)</summary>
		public readonly static DicomTag CoordinateStepValueRETIRED = new DicomTag(0x5000, 0x0114);

		///<summary>(5000,2006) VR=UL VM=1 Number of Samples (RETIRED)</summary>
		public readonly static DicomTag NumberOfSamplesRETIRED = new DicomTag(0x5000, 0x2006);

		///<summary>(5000,2004) VR=US VM=1 Number of Channels (RETIRED)</summary>
		public readonly static DicomTag NumberOfChannelsRETIRED = new DicomTag(0x5000, 0x2004);

		///<summary>(5000,2002) VR=US VM=1 Audio Sample Format (RETIRED)</summary>
		public readonly static DicomTag AudioSampleFormatRETIRED = new DicomTag(0x5000, 0x2002);

		///<summary>(1010,0000) VR=US VM=1-n Zonal Map (RETIRED)</summary>
		public readonly static DicomTag ZonalMapRETIRED = new DicomTag(0x1010, 0x0000);

		///<summary>(1000,0001) VR=US VM=3 Run Length Triplet (RETIRED)</summary>
		public readonly static DicomTag RunLengthTripletRETIRED = new DicomTag(0x1000, 0x0001);

		///<summary>(1000,0000) VR=US VM=3 Escape Triplet (RETIRED)</summary>
		public readonly static DicomTag EscapeTripletRETIRED = new DicomTag(0x1000, 0x0000);

		///<summary>(1000,0002) VR=US VM=1 Huffman Table Size (RETIRED)</summary>
		public readonly static DicomTag HuffmanTableSizeRETIRED = new DicomTag(0x1000, 0x0002);

		///<summary>(1000,0005) VR=US VM=3 Shift Table Triplet (RETIRED)</summary>
		public readonly static DicomTag ShiftTableTripletRETIRED = new DicomTag(0x1000, 0x0005);

		///<summary>(1000,0004) VR=US VM=1 Shift Table Size (RETIRED)</summary>
		public readonly static DicomTag ShiftTableSizeRETIRED = new DicomTag(0x1000, 0x0004);

		///<summary>(1000,0003) VR=US VM=3 Huffman Table Triplet (RETIRED)</summary>
		public readonly static DicomTag HuffmanTableTripletRETIRED = new DicomTag(0x1000, 0x0003);

		///<summary>(0020,3100) VR=CS VM=1-n Source Image IDs (RETIRED)</summary>
		public readonly static DicomTag SourceImageIDsRETIRED = new DicomTag(0x0020, 0x3100);

		///<summary>(0028,0400) VR=US VM=1 Rows For Nth Order Coefficients (RETIRED)</summary>
		public readonly static DicomTag RowsForNthOrderCoefficientsRETIRED = new DicomTag(0x0028, 0x0400);

		///<summary>(0028,0401) VR=US VM=1 Columns For Nth Order Coefficients (RETIRED)</summary>
		public readonly static DicomTag ColumnsForNthOrderCoefficientsRETIRED = new DicomTag(0x0028, 0x0401);

		///<summary>(0028,0800) VR=CS VM=1-n Code Label (RETIRED)</summary>
		public readonly static DicomTag CodeLabelRETIRED = new DicomTag(0x0028, 0x0800);

		///<summary>(0028,0808) VR=AT VM=1-n Image Data Location (RETIRED)</summary>
		public readonly static DicomTag ImageDataLocationRETIRED = new DicomTag(0x0028, 0x0808);

		///<summary>(0028,0402) VR=LO VM=1-n Coefficient Coding (RETIRED)</summary>
		public readonly static DicomTag CoefficientCodingRETIRED = new DicomTag(0x0028, 0x0402);

		///<summary>(0028,0802) VR=US VM=1 Number of Tables (RETIRED)</summary>
		public readonly static DicomTag NumberOfTablesRETIRED = new DicomTag(0x0028, 0x0802);

		///<summary>(0028,0803) VR=AT VM=1-n Code Table Location (RETIRED)</summary>
		public readonly static DicomTag CodeTableLocationRETIRED = new DicomTag(0x0028, 0x0803);

		///<summary>(0028,0403) VR=AT VM=1-n Coefficient Coding Pointers (RETIRED)</summary>
		public readonly static DicomTag CoefficientCodingPointersRETIRED = new DicomTag(0x0028, 0x0403);

		///<summary>(0028,0804) VR=US VM=1 Bits For Code Word (RETIRED)</summary>
		public readonly static DicomTag BitsForCodeWordRETIRED = new DicomTag(0x0028, 0x0804);

	}
}
