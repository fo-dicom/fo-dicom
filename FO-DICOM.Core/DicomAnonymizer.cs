// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using FellowOakDicom.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FellowOakDicom
{

    /// <summary>
    /// Class for performing anonymization actions on DICOM file or dataset based on selected confidentiality profile.
    /// </summary>
    public partial class DicomAnonymizer
    {
        #region Fields

        protected static readonly int _optionsCount = Enum.GetValues(typeof(SecurityProfileOptions)).Length;

        #endregion

        #region Embedded types

        /// <summary>Profile options as described in DICOM PS 3.15</summary>
        /// <see>http://dicom.nema.org/medical/dicom/current/output/chtml/part15/PS3.15.html</see>
        /// <remarks>The order of the flags are mapped to the profile's CSV file</remarks>
        [Flags]
        public enum SecurityProfileOptions : short
        {
            BasicProfile = 1,

            RetainSafePrivate = 2,

            RetainUIDs = 4,

            RetainDeviceIdent = 8,

            RetainInstitutionIdent = 16,

            RetainPatientChars = 32,

            RetainLongFullDates = 64,

            RetainLongModifDates = 128,

            CleanDesc = 256,

            CleanStructdCont = 512,

            CleanGraph = 1024
        }

        /// <summary>Profile actions per tag as described in DICOM PS 3.15</summary>
        /// <see>http://dicom.nema.org/medical/dicom/current/output/chtml/part15/PS3.15.html</see>
        [Flags]
        public enum SecurityProfileActions : byte
        {
            /// <summary>
            /// Replace with a non-zero length value that may be a dummy value and consistent with the VR
            /// </summary>
            D = 1,

            /// <summary>
            /// Replace with a zero length value, or a non-zero length value that may be a dummy value and consistent with the VR
            /// </summary>
            Z = 2,

            /// <summary>
            /// Remove
            /// </summary>
            X = 4,

            /// <summary>
            /// Keep (unchanged for non-sequence attributes, cleaned for sequences)
            /// </summary>
            K = 8,

            /// <summary>
            /// Clean, that is replace with values of similar meaning known not to contain identifying information and consistent with the VR
            /// </summary>
            C = 16,

            /// <summary>
            /// Replace with a non-zero length UID that is internally consistent within a set of Instances
            /// </summary>
            U = 32           
        }

        /// <summary>Security profile container</summary>
        public partial class SecurityProfile : Dictionary<Regex, SecurityProfileActions>
        {

            /// <summary>Optional. Replacement patient name (random or alias)</summary>
            public string PatientName { get; set; } = null;

            /// <summary>Optional. Replacement patient ID</summary>
            public string PatientID { get; set; } = null;

            /// <summary>
            /// Loads a security profile with the specified options
            /// </summary>
            /// <param name="source">A reader for a profile file source. If null, the default profile is loaded</param>
            /// <param name="options">The optional flags for the profile</param>
            /// <returns>A dictionary containing the security profile</returns>
            /// <exception cref="System.ArgumentException">A regular expression parsing error occurred</exception>
            /// <exception cref="System.IO.IOException">An I/O error occurs</exception>
            /// <exception cref="System.ObjectDisposedException">The TextReader is closed</exception>
            public static SecurityProfile LoadProfile(TextReader source, SecurityProfileOptions options)
            {
                var profile = new SecurityProfile();

                if (source == null)
                {
                    source = new StringReader(DefaultProfile);
                }

                string line;
                while ((line = source.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) { continue; }

                    var parts = line.Trim().Split(';');
                    if (parts.Length == 0) { continue; }

                    var tag = new Regex(parts[0], RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var empty = default(char).ToString();

                    for (var i = 0; i < _optionsCount; i++)
                    {
                        var flag = (SecurityProfileOptions)(1 << i);

                        if ((options & flag) == flag)
                        {
                            var action = parts[i + 1].ToCharArray().FirstOrDefault().ToString();
                            if (action != empty)
                            {
                                profile[tag] =
                                    (SecurityProfileActions)Enum.Parse(typeof(SecurityProfileActions), action);
                            }
                        }
                    }
                }

                return profile;
            }
        }

        #endregion

        #region Constructors

        /// <summary>Public constructor</summary>
        /// <param name="profile">Optional. The security profile to be used in one or multiple anonymizations. 
        /// If not specified or null, it will use the default/internal profile</param>
        public DicomAnonymizer(SecurityProfile profile = null)
        {
            Profile = profile ?? SecurityProfile.LoadProfile(null, SecurityProfileOptions.BasicProfile);
        }

        #endregion

        #region Public properties

        /// <summary>Context/Output. Contains all the replaced UIDs.</summary>
        /// <remarks>Useful for consistency across a file set (multiple calls to anonymization methods)</remarks>
        public virtual Dictionary<string, string> ReplacedUIDs { get; } = new Dictionary<string, string>();

        /// <summary>The security profile for this anonymizer instance</summary>
        public virtual SecurityProfile Profile { get; }

        #endregion

        #region Public methods

        /// <summary>Anonymizes a dataset witout cloning</summary>
        /// <param name="dataset">The dataset to be altered</param>
        public virtual void AnonymizeInPlace(DicomDataset dataset)
        {
            var toRemove = new List<DicomItem>();
            var itemList = dataset.ToArray();

            foreach (var item in itemList)
            {
                var parenthesis = new[] { '(', ')' };
                var tag = item.Tag.ToString().Trim(parenthesis);
                var action = Profile.FirstOrDefault(pair => pair.Key.IsMatch(tag));

                if (item is DicomSequence sequenceItem && (action.Key == null || action.Value == SecurityProfileActions.K))
                {
                    foreach (DicomDataset sqDataset in sequenceItem)
                    {
                        AnonymizeInPlace(sqDataset);
                    }
                }

                if (action.Key != null)
                {
                    var vr = item.ValueRepresentation;

                    switch (action.Value)
                    {
                        case SecurityProfileActions.U: // UID
                        case SecurityProfileActions.C: // Clean
                        case SecurityProfileActions.D: // Dummy
                            if (vr == DicomVR.UI)
                            {
                                ReplaceUID(dataset, item);
                            }
                            else if (vr.ValueType == typeof(string))
                            {
                                ReplaceString(dataset, item, "ANONYMOUS");
                            }
                            else
                            {
                                BlankItem(dataset, item, true);
                            }
                            break;
                        case SecurityProfileActions.K: // Keep
                            break;
                        case SecurityProfileActions.X: // Remove
                            toRemove.Add(item);
                            break;
                        case SecurityProfileActions.Z: // Zero-length
                            BlankItem(dataset, item, false);
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown action {action.Value}");
                    }
                }

                if (item.Tag.Equals(DicomTag.PatientName) && Profile.PatientName != null)
                {
                    ReplaceString(dataset, item, Profile.PatientName);
                }
                else if (item.Tag.Equals(DicomTag.PatientID) && Profile.PatientID != null)
                {
                    ReplaceString(dataset, item, Profile.PatientID);
                }
            }

            dataset.Remove(item => toRemove.Contains(item));
        }

        /// <summary>Anonymizes the dataset of an existing Dicom file</summary>
        /// <param name="file">The file containing the dataset to be altered</param>
        public virtual void AnonymizeInPlace(DicomFile file)
        {
            AnonymizeInPlace(file.Dataset);
            if (file.FileMetaInfo != null)
            {
                file.FileMetaInfo.MediaStorageSOPInstanceUID = file.Dataset.GetSingleValue<DicomUID>(DicomTag.SOPInstanceUID);
            }
        }

        /// <summary>Clones and anonymizes a dataset</summary>
        /// <param name="dataset">The dataset to be cloned and anonymized</param>
        /// <returns>Anonymized dataset.</returns>
        public virtual DicomDataset Anonymize(DicomDataset dataset)
        {
            var clone = dataset.Clone();
            AnonymizeInPlace(clone);
            return clone;
        }

        /// <summary>Creates a new Dicom file with an anonymized dataset</summary>
        /// <param name="file">The file containing the original dataset</param>
        /// <returns>Anonymized dataset.</returns>
        public virtual DicomFile Anonymize(DicomFile file)
        {
            var clone = file.Clone();
            AnonymizeInPlace(clone);
            return clone;
        }

        #endregion

        #region Protected methods

        /// <string>Replaces the content of a UID with a random one</string>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="item"></param>
        protected virtual void ReplaceUID(DicomDataset dataset, DicomItem item)
        {
            if (!(item is DicomElement))
            {
                return;
            }

            string rep;
            DicomUID uid;
            var old = ((DicomElement)item).Get<string>();

            if (string.IsNullOrEmpty(old))
            {
                // no need to replace empty values
                return;
            }

            if (ReplacedUIDs.ContainsKey(old))
            {
                rep = ReplacedUIDs[old];
                uid = new DicomUID(rep, "Anonymized UID", DicomUidType.Unknown);
            }
            else
            {
                uid = DicomUIDGenerator.GenerateDerivedFromUUID();
                rep = uid.UID;
                ReplacedUIDs[old] = rep;
            }

            var newItem = new DicomUniqueIdentifier(item.Tag, uid);
            dataset.AddOrUpdate(newItem);
        }

        /// <summary>Blanks an item to value suitable for the concrete item type.</summary>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="item">DICOM item subject to blanking.</param>
        /// <param name="nonZeroLength">Require that new value is non-zero length (dummy) value?</param>
        protected static void BlankItem(DicomDataset dataset, DicomItem item, bool nonZeroLength)
        {
            var tag = item.Tag;

            if (item is DicomSequence)
            {
                dataset.AddOrUpdate<DicomDataset>(DicomVR.SQ, tag);
                return;
            }

            // Special date/time cases
            if (item is DicomDateTime)
            {
                dataset.AddOrUpdate(nonZeroLength
                    ? new DicomDateTime(tag, DateTime.MinValue)
                    : new DicomDateTime(tag, Array.Empty<string>()));
                return;
            }
            if (item is DicomDate)
            {
                dataset.AddOrUpdate(nonZeroLength
                    ? new DicomDate(tag, DateTime.MinValue)
                    : new DicomDate(tag, Array.Empty<string>()));
                return;
            }
            if (item is DicomTime)
            {
                dataset.AddOrUpdate(nonZeroLength
                    ? new DicomTime(tag, DateTime.MinValue)
                    : new DicomTime(tag, Array.Empty<string>()));
                return;
            }

            if (item is DicomStringElement stringElement)
            {
                dataset.AddOrUpdate(stringElement.ValueRepresentation, tag, string.Empty);
                return;
            }

            if (IsOtherElement(item)) // Replaces with an empty array
            {
                var itemType = item.GetType();
                var updated = (DicomItem)Activator.CreateInstance(itemType, tag, EmptyBuffer.Value);
                dataset.AddOrUpdate(updated);
                return;
            }

            var valueType = ElementValueType(item); // Replace with the default value
            if (valueType != null)
            {
                var itemType = item.GetType();
                var updated = (DicomItem)Activator.CreateInstance(itemType, tag, Activator.CreateInstance(valueType));
                dataset.AddOrUpdate(updated);
            }
        }

        /// <summary>Evaluates whether a DICOM item is of type Other*</summary>
        /// <param name="item"></param>
        /// <returns>A boolean flag indicating whether the item is of the expected type, otherwise false</returns>
        protected static bool IsOtherElement(DicomItem item)
        {
            var t = item.GetType();
            return t.IsOneOf(typeof(DicomOtherByte), typeof(DicomOtherDouble), typeof(DicomOtherFloat), 
                typeof(DicomOtherLong), typeof(DicomOtherWord), typeof(DicomUnknown));
        }

        /// <summary>Evaluates whether an element has a generic valueType</summary>
        /// <param name="item"></param>
        /// <returns>The data type if found, otherwise null</returns>
        protected static Type ElementValueType(DicomItem item)
        {
            var t = item.GetType().BaseType;
            if (t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(DicomValueElement<>))
            {
                return t.GenericTypeArguments[0];
            }

            return null;
        }

        /// <string>Replaces the content of an item.</string>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="item">DICOM item for which the string value should be replaced.</param>
        /// <param name="newString">The replacement string.</param>
        protected static void ReplaceString(DicomDataset dataset, DicomItem item, string newString)
        {
            dataset.AddOrUpdate(item.ValueRepresentation, item.Tag, newString);
        }

        #endregion
    }
}
