// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FellowOakDicom.Network
{
    /// <summary>State of a DICOM status code</summary>
    public enum DicomState
    {
        /// <summary>Success.</summary>
        Success,

        /// <summary>Cancel.</summary>
        Cancel,

        /// <summary>Pending.</summary>
        Pending,

        /// <summary>Warning.</summary>
        Warning,

        /// <summary>Failure.</summary>
        Failure
    }

    /// <summary>DICOM Status</summary>
    public class DicomStatus
    {
        /// <summary>DICOM status code.</summary>
        public readonly ushort Code;

        /// <summary>State of this DICOM status code.</summary>
        public readonly DicomState State;

        /// <summary>Description.</summary>
        public readonly string Description;

        public readonly string ErrorComment = null;

        private readonly ushort _mask;

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomStatus"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="status">The status.</param>
        /// <param name="desc">The desc.</param>
        public DicomStatus(string code, DicomState status, string desc)
        {
            Code = ushort.Parse(
                code.Replace('x', '0'),
                NumberStyles.HexNumber,
                CultureInfo.InvariantCulture);

            var msb = new StringBuilder();
            msb.Append(code.ToLower());
            msb.Replace('0', 'F')
                .Replace('1', 'F')
                .Replace('2', 'F')
                .Replace('3', 'F')
                .Replace('4', 'F')
                .Replace('5', 'F')
                .Replace('6', 'F')
                .Replace('7', 'F')
                .Replace('8', 'F')
                .Replace('9', 'F')
                .Replace('a', 'F')
                .Replace('b', 'F')
                .Replace('c', 'F')
                .Replace('d', 'F')
                .Replace('e', 'F')
                .Replace('x', '0');
            _mask = ushort.Parse(
                msb.ToString(),
                NumberStyles.HexNumber,
                CultureInfo.InvariantCulture);

            State = status;
            Description = desc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomStatus"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="status">The status.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="comment">The comment.</param>
        public DicomStatus(string code, DicomState status, string desc, string comment)
            : this(code, status, desc)
        {
            ErrorComment = comment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DicomStatus"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="comment">The comment.</param>
        public DicomStatus(DicomStatus status, string comment)
            : this(string.Format("{0:x4}", status.Code), status.State, status.Description, comment)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DicomStatus"/> class.
        /// </summary>
        internal DicomStatus(ushort code, DicomStatus baseStatus)
        {
            // set the code given by param code...
            Code = code;
            // ... and copy all other values from baseStatus 
            Description = baseStatus.Description;
            ErrorComment = baseStatus.ErrorComment;
            _mask = baseStatus._mask;
            State = baseStatus.State;
        }


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (State == DicomState.Warning || State == DicomState.Failure)
            {
                return !string.IsNullOrEmpty(ErrorComment)
                    ? string.Format("{0} [{1:x4}: {2}] -> {3}", State, Code, Description, ErrorComment)
                    : string.Format("{0} [{1:x4}: {2}]", State, Code, Description);
            }
            return Description;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="s1">DICOM Status</param>
        /// <param name="s2">DICOM Status</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(DicomStatus s1, DicomStatus s2)
        {
            if (s1 is null || s2 is null)
            {
                return s1 is null && s2 is null;
            }

            return (s1.Code & s2._mask) == (s2.Code & s1._mask);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="s1">DICOM Status</param>
        /// <param name="s2">DICOM Status</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(DicomStatus s1, DicomStatus s2)
        {
            if (s1 is null || s2 is null)
            {
                return s1 is null ^ s2 is null;
            }

            return !(s1 == s2);
        }


        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        public override bool Equals(object obj) => (DicomStatus)obj == this;

        public override int GetHashCode() => HashCode.Combine(Code, (int)State, Description, ErrorComment, _mask);

        #region Static

        private static List<DicomStatus> _entries = new List<DicomStatus>();

        static DicomStatus()
        {
            ResetEntries();
        }

        /// <summary>
        /// Resets the list of known DicomStatuses to the hard-coded list.
        /// </summary>
        internal static void ResetEntries()
        {
            _entries.Clear();
            #region Load Dicom Status List

            _entries.Add(Success);
            _entries.Add(Cancel);
            _entries.Add(Pending);
            _entries.Add(Warning);
            _entries.Add(WarningClass);
            _entries.Add(AttributeListError);
            _entries.Add(AttributeValueOutOfRange);
            _entries.Add(SOPClassNotSupported);
            _entries.Add(ClassInstanceConflict);
            _entries.Add(DuplicateSOPInstance);
            _entries.Add(DuplicateInvocation);
            _entries.Add(InvalidArgumentValue);
            _entries.Add(InvalidAttributeValue);
            _entries.Add(InvalidObjectInstance);
            _entries.Add(MissingAttribute);
            _entries.Add(MissingAttributeValue);
            _entries.Add(MistypedArgument);
            _entries.Add(NoSuchArgument);
            _entries.Add(NoSuchEventType);
            _entries.Add(NoSuchObjectInstance);
            _entries.Add(NoSuchSOPClass);
            _entries.Add(ProcessingFailure);
            _entries.Add(ResourceLimitation);
            _entries.Add(UnrecognizedOperation);
            _entries.Add(NoSuchActionType);
            _entries.Add(StorageStorageOutOfResources);
            _entries.Add(StorageDataSetDoesNotMatchSOPClassError);
            _entries.Add(StorageCannotUnderstand);
            _entries.Add(StorageCoercionOfDataElements);
            _entries.Add(StorageDataSetDoesNotMatchSOPClassWarning);
            _entries.Add(StorageElementsDiscarded);
            _entries.Add(QueryRetrieveOutOfResources);
            _entries.Add(QueryRetrieveUnableToCalculateNumberOfMatches);
            _entries.Add(QueryRetrieveUnableToPerformSuboperations);
            _entries.Add(QueryRetrieveMoveDestinationUnknown);
            _entries.Add(QueryRetrieveIdentifierDoesNotMatchSOPClass);
            _entries.Add(QueryRetrieveUnableToProcess);
            _entries.Add(QueryRetrieveOptionalKeysNotSupported);
            _entries.Add(QueryRetrieveSubOpsOneOrMoreFailures);
            _entries.Add(PrintManagementMemoryAllocationNotSupported);
            _entries.Add(PrintManagementFilmSessionPrintingNotSupported);
            _entries.Add(PrintManagementFilmSessionEmptyPage);
            _entries.Add(PrintManagementFilmBoxEmptyPage);
            _entries.Add(PrintManagementImageDemagnified);
            _entries.Add(PrintManagementMinMaxDensityOutOfRange);
            _entries.Add(PrintManagementImageCropped);
            _entries.Add(PrintManagementImageDecimated);
            _entries.Add(PrintManagementFilmSessionEmpty);
            _entries.Add(PrintManagementPrintQueueFull);
            _entries.Add(PrintManagementImageLargerThanImageBox);
            _entries.Add(PrintManagementInsufficientMemoryInPrinter);
            _entries.Add(PrintManagementCombinedImageLargerThanImageBox);
            _entries.Add(PrintManagementExistingFilmBoxNotPrinted);
            _entries.Add(MediaCreationManagementDuplicateInitiateMediaCreation);
            _entries.Add(MediaCreationManagementMediaCreationRequestAlreadyCompleted);
            _entries.Add(MediaCreationManagementMediaCreationRequestAlreadyInProgress);
            _entries.Add(MediaCreationManagementCancellationDeniedForUnspecifiedReason);

            #endregion

            _entries = _entries.OrderByDescending(entry => CountBits(entry._mask)).ToList();
        }

        /// <summary>
        /// Looks up the specified code and returns a new instance
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static DicomStatus Lookup(ushort code)
        {
            foreach (DicomStatus status in _entries)
            {
                if (status.Code == (code & status._mask))
                {
                    return new DicomStatus(code, status);
                }
            }
            return ProcessingFailure;
        }

        private static int CountBits(ushort value)
        {
            int count = 0;

            for (int i = 0; i < 16; i++)
            {
                if ((value & (1 << i)) != 0)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Adds a set of known DicomStatuses.
        /// </summary>
        /// <param name="statuses">The statuses to add.</param>
        public static void AddKnownDicomStatuses(IEnumerable<DicomStatus> statuses)
        {
            _entries.AddRange(statuses);
            _entries = _entries.OrderByDescending(entry => CountBits(entry._mask)).ToList();
        }

        #region Dicom Statuses

        /// <summary>Success: Success</summary>
        public static readonly DicomStatus Success = new DicomStatus("0000", DicomState.Success, "Success");

        /// <summary>Cancel: Cancel</summary>
        public static readonly DicomStatus Cancel = new DicomStatus("FE00", DicomState.Cancel, "Cancel");

        /// <summary>Pending: Pending</summary>
        public static readonly DicomStatus Pending = new DicomStatus("FF00", DicomState.Pending, "Pending");

        /// <summary>Warning: Warning</summary>
        public static readonly DicomStatus Warning = new DicomStatus("0001", DicomState.Warning, "Warning");

        /// <summary>Warning: Warning Class</summary>
        public static readonly DicomStatus WarningClass = new DicomStatus("Bxxx", DicomState.Warning, "Warning Class");

        /// <summary>Warning: Attribute list error</summary>
        public static readonly DicomStatus AttributeListError = new DicomStatus(
            "0107",
            DicomState.Warning,
            "Attribute list error");

        /// <summary>Warning: Attribute Value Out of Range</summary>
        public static readonly DicomStatus AttributeValueOutOfRange = new DicomStatus(
            "0116",
            DicomState.Warning,
            "Attribute Value Out of Range");

        /// <summary>Failure: Refused: SOP class not supported</summary>
        public static readonly DicomStatus SOPClassNotSupported = new DicomStatus(
            "0122",
            DicomState.Failure,
            "Refused: SOP class not supported");

        /// <summary>Failure: Class-instance conflict</summary>
        public static readonly DicomStatus ClassInstanceConflict = new DicomStatus(
            "0119",
            DicomState.Failure,
            "Class-instance conflict");

        /// <summary>Failure: Duplicate SOP instance</summary>
        public static readonly DicomStatus DuplicateSOPInstance = new DicomStatus(
            "0111",
            DicomState.Failure,
            "Duplicate SOP instance");

        /// <summary>Failure: Duplicate invocation</summary>
        public static readonly DicomStatus DuplicateInvocation = new DicomStatus(
            "0210",
            DicomState.Failure,
            "Duplicate invocation");

        /// <summary>Failure: Invalid argument value</summary>
        public static readonly DicomStatus InvalidArgumentValue = new DicomStatus(
            "0115",
            DicomState.Failure,
            "Invalid argument value");

        /// <summary>Failure: Invalid attribute value</summary>
        public static readonly DicomStatus InvalidAttributeValue = new DicomStatus(
            "0106",
            DicomState.Failure,
            "Invalid attribute value");

        /// <summary>Failure: Invalid object instance</summary>
        public static readonly DicomStatus InvalidObjectInstance = new DicomStatus(
            "0117",
            DicomState.Failure,
            "Invalid object instance");

        /// <summary>Failure: Missing attribute</summary>
        public static readonly DicomStatus MissingAttribute = new DicomStatus("0120", DicomState.Failure, "Missing attribute");

        /// <summary>Failure: Missing attribute value</summary>
        public static readonly DicomStatus MissingAttributeValue = new DicomStatus(
            "0121",
            DicomState.Failure,
            "Missing attribute value");

        /// <summary>Failure: Mistyped argument</summary>
        public static readonly DicomStatus MistypedArgument = new DicomStatus("0212", DicomState.Failure, "Mistyped argument");

        /// <summary>Failure: No such argument</summary>
        public static readonly DicomStatus NoSuchArgument = new DicomStatus("0114", DicomState.Failure, "No such argument");

        /// <summary>Failure: No such event type</summary>
        public static readonly DicomStatus NoSuchEventType = new DicomStatus("0113", DicomState.Failure, "No such event type");

        /// <summary>Failure: No Such object instance</summary>
        public static readonly DicomStatus NoSuchObjectInstance = new DicomStatus(
            "0112",
            DicomState.Failure,
            "No Such object instance");

        /// <summary>Failure: No Such SOP class</summary>
        public static readonly DicomStatus NoSuchSOPClass = new DicomStatus("0118", DicomState.Failure, "No Such SOP class");

        /// <summary>Failure: Processing failure</summary>
        public static readonly DicomStatus ProcessingFailure = new DicomStatus("0110", DicomState.Failure, "Processing failure");

        /// <summary>Failure: Resource limitation</summary>
        public static readonly DicomStatus ResourceLimitation = new DicomStatus(
            "0213",
            DicomState.Failure,
            "Resource limitation");

        /// <summary>Failure: Unrecognized operation</summary>
        public static readonly DicomStatus UnrecognizedOperation = new DicomStatus(
            "0211",
            DicomState.Failure,
            "Unrecognized operation");

        /// <summary>Failure: No such action type</summary>
        public static readonly DicomStatus NoSuchActionType = new DicomStatus("0123", DicomState.Failure, "No such action type");

        /// <summary>Storage Failure: Out of Resources</summary>
        public static readonly DicomStatus StorageStorageOutOfResources = new DicomStatus(
            "A7xx",
            DicomState.Failure,
            "Out of Resources");

        /// <summary>Storage Failure: Data Set does not match SOP Class (Error)</summary>
        public static readonly DicomStatus StorageDataSetDoesNotMatchSOPClassError = new DicomStatus(
            "A9xx",
            DicomState.Failure,
            "Data Set does not match SOP Class (Error)");

        /// <summary>Storage Failure: Cannot understand</summary>
        public static readonly DicomStatus StorageCannotUnderstand = new DicomStatus(
            "Cxxx",
            DicomState.Failure,
            "Cannot understand");

        /// <summary>Storage Warning: Coercion of Data Elements</summary>
        public static readonly DicomStatus StorageCoercionOfDataElements = new DicomStatus(
            "B000",
            DicomState.Warning,
            "Coercion of Data Elements");

        /// <summary>Storage Warning: Data Set does not match SOP Class (Warning)</summary>
        public static readonly DicomStatus StorageDataSetDoesNotMatchSOPClassWarning = new DicomStatus(
            "B007",
            DicomState.Warning,
            "Data Set does not match SOP Class (Warning)");

        /// <summary>Storage Warning: Elements Discarded</summary>
        public static readonly DicomStatus StorageElementsDiscarded = new DicomStatus(
            "B006",
            DicomState.Warning,
            "Elements Discarded");

        /// <summary>QueryRetrieve Failure: Out of Resources</summary>
        public static readonly DicomStatus QueryRetrieveOutOfResources = new DicomStatus(
            "A700",
            DicomState.Failure,
            "Out of Resources");

        /// <summary>QueryRetrieve Failure: Unable to calculate number of matches</summary>
        public static readonly DicomStatus QueryRetrieveUnableToCalculateNumberOfMatches = new DicomStatus(
            "A701",
            DicomState.Failure,
            "Unable to calculate number of matches");

        /// <summary>QueryRetrieve Failure: Unable to perform suboperations</summary>
        public static readonly DicomStatus QueryRetrieveUnableToPerformSuboperations = new DicomStatus(
            "A702",
            DicomState.Failure,
            "Unable to perform suboperations");

        /// <summary>QueryRetrieve Failure: Move Destination unknown</summary>
        public static readonly DicomStatus QueryRetrieveMoveDestinationUnknown = new DicomStatus(
            "A801",
            DicomState.Failure,
            "Move Destination unknown");

        /// <summary>QueryRetrieve Failure: Identifier does not match SOP Class</summary>
        public static readonly DicomStatus QueryRetrieveIdentifierDoesNotMatchSOPClass = new DicomStatus(
            "A900",
            DicomState.Failure,
            "Identifier does not match SOP Class");

        /// <summary>QueryRetrieve Failure: Unable to process</summary>
        public static readonly DicomStatus QueryRetrieveUnableToProcess = new DicomStatus(
            "Cxxx",
            DicomState.Failure,
            "Unable to process");

        /// <summary>QueryRetrieve Pending: Optional Keys Not Supported</summary>
        public static readonly DicomStatus QueryRetrieveOptionalKeysNotSupported = new DicomStatus(
            "FF01",
            DicomState.Pending,
            "Optional Keys Not Supported");

        /// <summary>QueryRetrieve Warning: Sub-operations Complete - One or more Failures</summary>
        public static readonly DicomStatus QueryRetrieveSubOpsOneOrMoreFailures = new DicomStatus(
            "B000",
            DicomState.Warning,
            "Sub-operations Complete - One or more Failures");

        /// <summary>PrintManagement Warning: Memory allocation not supported</summary>
        public static readonly DicomStatus PrintManagementMemoryAllocationNotSupported = new DicomStatus(
            "B000",
            DicomState.Warning,
            "Memory allocation not supported");

        /// <summary>PrintManagement Warning: Film session printing (collation) is not supported</summary>
        public static readonly DicomStatus PrintManagementFilmSessionPrintingNotSupported = new DicomStatus(
            "B601",
            DicomState.Warning,
            "Film session printing (collation) is not supported");

        /// <summary>PrintManagement Warning: Film session SOP instance hierarchy does not contain image box SOP instances (empty page)</summary>
        public static readonly DicomStatus PrintManagementFilmSessionEmptyPage = new DicomStatus(
            "B602",
            DicomState.Warning,
            "Film session SOP instance hierarchy does not contain image box SOP instances (empty page)");

        /// <summary>PrintManagement Warning: Film box SOP instance hierarchy does not contain image box SOP instances (empty page)</summary>
        public static readonly DicomStatus PrintManagementFilmBoxEmptyPage = new DicomStatus(
            "B603",
            DicomState.Warning,
            "Film box SOP instance hierarchy does not contain image box SOP instances (empty page)");

        /// <summary>PrintManagement Warning: Image size is larger than image box size, the image has been demagnified</summary>
        public static readonly DicomStatus PrintManagementImageDemagnified = new DicomStatus(
            "B604",
            DicomState.Warning,
            "Image size is larger than image box size, the image has been demagnified");

        /// <summary>PrintManagement Warning: Requested min density or max density outside of printer's operating range</summary>
        public static readonly DicomStatus PrintManagementMinMaxDensityOutOfRange = new DicomStatus(
            "B605",
            DicomState.Warning,
            "Requested min density or max density outside of printer's operating range");

        /// <summary>PrintManagement Warning: Image size is larger than the image box size, the Image has been cropped to fit</summary>
        public static readonly DicomStatus PrintManagementImageCropped = new DicomStatus(
            "B609",
            DicomState.Warning,
            "Image size is larger than the image box size, the Image has been cropped to fit");

        /// <summary>PrintManagement Warning: Image size or combined print image size is larger than the image box size, image or combined print image has been decimated to fit</summary>
        public static readonly DicomStatus PrintManagementImageDecimated = new DicomStatus(
            "B60A",
            DicomState.Warning,
            "Image size or combined print image size is larger than the image box size, image or combined print image has been decimated to fit");

        /// <summary>PrintManagement Failure: Film session SOP instance hierarchy does not contain film box SOP instances</summary>
        public static readonly DicomStatus PrintManagementFilmSessionEmpty = new DicomStatus(
            "C600",
            DicomState.Failure,
            "Film session SOP instance hierarchy does not contain film box SOP instances");

        /// <summary>PrintManagement Failure: Unable to create Print Job SOP Instance; print queue is full</summary>
        public static readonly DicomStatus PrintManagementPrintQueueFull = new DicomStatus(
            "C601",
            DicomState.Failure,
            "Unable to create Print Job SOP Instance; print queue is full");

        /// <summary>PrintManagement Failure: Image size is larger than image box size</summary>
        public static readonly DicomStatus PrintManagementImageLargerThanImageBox = new DicomStatus(
            "C603",
            DicomState.Failure,
            "Image size is larger than image box size");

        /// <summary>PrintManagement Failure: Insufficient memory in printer to store the image</summary>
        public static readonly DicomStatus PrintManagementInsufficientMemoryInPrinter = new DicomStatus(
            "C605",
            DicomState.Failure,
            "Insufficient memory in printer to store the image");

        /// <summary>PrintManagement Failure: Combined Print Image size is larger than the Image Box size</summary>
        public static readonly DicomStatus PrintManagementCombinedImageLargerThanImageBox = new DicomStatus(
            "C613",
            DicomState.Failure,
            "Combined Print Image size is larger than the Image Box size");

        /// <summary>PrintManagement Failure: There is an existing film box that has not been printed and N-ACTION at the Film Session level is not supported.</summary>
        public static readonly DicomStatus PrintManagementExistingFilmBoxNotPrinted = new DicomStatus(
            "C616",
            DicomState.Failure,
            "There is an existing film box that has not been printed and N-ACTION at the Film Session level is not supported.");

        /// <summary>MediaCreationManagement Failure: Refused because an Initiate Media Creation action has already been received for this SOP Instance</summary>
        public static readonly DicomStatus MediaCreationManagementDuplicateInitiateMediaCreation = new DicomStatus(
            "A510",
            DicomState.Failure,
            "Refused because an Initiate Media Creation action has already been received for this SOP Instance");

        /// <summary>MediaCreationManagement Failure: Media creation request already completed</summary>
        public static readonly DicomStatus MediaCreationManagementMediaCreationRequestAlreadyCompleted = new DicomStatus(
            "C201",
            DicomState.Failure,
            "Media creation request already completed");

        /// <summary>MediaCreationManagement Failure: Media creation request already in progress and cannot be interrupted</summary>
        public static readonly DicomStatus MediaCreationManagementMediaCreationRequestAlreadyInProgress = new DicomStatus(
            "C202",
            DicomState.Failure,
            "Media creation request already in progress and cannot be interrupted");

        /// <summary>MediaCreationManagement Failure: Cancellation denied for unspecified reason</summary>
        public static readonly DicomStatus MediaCreationManagementCancellationDeniedForUnspecifiedReason = new DicomStatus(
            "C203",
            DicomState.Failure,
            "Cancellation denied for unspecified reason");

        #endregion

        #endregion
    }
}
