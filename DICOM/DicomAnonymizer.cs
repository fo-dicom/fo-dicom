// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dicom
{
    /// <summary>
    /// Class for performing anonymization actions on DICOM file or dataset based on selected confidentiality profile.
    /// </summary>
    public partial class DicomAnonymizer
    {
        #region Fields

        private static readonly int OptionsCount = Enum.GetValues(typeof(SecurityProfileOptions)).Length;

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

            RetainPatientChars = 16,

            RetainLongFullDates = 32,

            RetainLongModifDates = 64,

            CleanDesc = 128,

            CleanStructdCont = 256,

            CleanGraph = 512
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
            public string PatientName = null;

            /// <summary>Optional. Replacement patient ID</summary>
            public string PatientID = null;

            /// <summary>
            /// Loads a security profile with the specified options
            /// </summary>
            /// <param name="source">A reader for a profile file source. If null, the default profile is loaded</param>
            /// <param name="options">The optional flags for the profile</param>
            /// <returns>A dictionary containing the security profile</returns>
            /// <exception cref="ArgumentException">A regular expression parsing error occurred</exception>
            /// <exception cref="IOException">An I/O error occurs</exception>
            /// <exception cref="ObjectDisposedException">The TextReader is closed</exception>
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
                    if (string.IsNullOrEmpty(line.Trim())) continue;

                    var parts = line.Trim().Split(';');
                    if (parts.Length == 0) continue;

                    var tag = new Regex(parts[0], RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var empty = default(char).ToString();

                    for (var i = 0; i < OptionsCount; i++)
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
        public Dictionary<string, string> ReplacedUIDs { get; } = new Dictionary<string, string>();

        /// <summary>The security profile for this anonymizer instance</summary>
        public SecurityProfile Profile { get; }

        #endregion

        #region Public methods

        /// <summary>Anonymizes a dataset witout cloning</summary>
        /// <param name="dataset">The dataset to be altered</param>
        public void AnonymizeInPlace(DicomDataset dataset)
        {
            var toRemove = new List<DicomItem>();
            var itemList = dataset.ToArray();

            foreach (var item in itemList)
            {
                var element = item as DicomElement;

                if (element == null) continue;

                var parenthesis = new[] { '(', ')' };
                var tag = item.Tag.ToString().Trim(parenthesis);
                var action = Profile.FirstOrDefault(pair => pair.Key.IsMatch(tag));
                if (action.Key != null)
                {
                    var vr = item.ValueRepresentation;

                    switch (action.Value)
                    {
                        case SecurityProfileActions.U: // UID
                        case SecurityProfileActions.C: // Clean
                        case SecurityProfileActions.D: // Dummy
                            if (vr == DicomVR.UI) ReplaceUID(dataset, element);
                            else if (vr.IsString) ReplaceString(dataset, element, "ANONYMOUS");
                            else BlankElement(dataset, element);
                            break;
                        case SecurityProfileActions.K: // Keep
                            break;
                        case SecurityProfileActions.X: // Remove
                            toRemove.Add(element);
                            break;
                        case SecurityProfileActions.Z: // Zero-length
                            BlankElement(dataset, element);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                if (element.Tag.Equals(DicomTag.PatientName) && Profile.PatientName != null)
                {
                    ReplaceString(dataset, element, Profile.PatientName);
                }
                else if (element.Tag.Equals(DicomTag.PatientID) && Profile.PatientID != null)
                {
                    ReplaceString(dataset, element, Profile.PatientID);
                }
            }

            dataset.Remove(item => toRemove.Contains(item));
        }

        /// <summary>Clones and anonymizes a dataset</summary>
        /// <param name="dataset">The dataset to be cloned and anonymized</param>
        /// <returns>Anonymized dataset.</returns>
        public DicomDataset Anonymize(DicomDataset dataset)
        {
            var clone = dataset.Clone();
            AnonymizeInPlace(clone);
            return clone;
        }

        /// <summary>Anonymizes the dataset of an existing Dicom file</summary>
        /// <param name="file">The file containing the dataset to be altered</param>
        public void AnonymizeInPlace(DicomFile file)
        {
            AnonymizeInPlace(file.Dataset);
            if (file.FileMetaInfo != null)
            {
                file.FileMetaInfo.MediaStorageSOPInstanceUID = file.Dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
            }
        }

        /// <summary>Creates a new Dicom file with an anonymized dataset</summary>
        /// <param name="file">The file containing the original dataset</param>
        /// <returns>Anonymized dataset.</returns>
        public DicomFile Anonymize(DicomFile file)
        {
            var clone = file.Clone();
            AnonymizeInPlace(clone);
            return clone;
        }

        #endregion

        #region Private methods


        /// <string>Replaces the content of a UID with a random one</string>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="element">The element to be altered</param>
        private void ReplaceUID(DicomDataset dataset, DicomElement element)
        {
            string rep;
            DicomUID uid;
            var old = element.Get<string>();

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

            var newItem = new DicomUniqueIdentifier(element.Tag, uid);
            dataset.AddOrUpdate(newItem);
        }

        /// <summary>Blanks an element by passing to it an empty string</summary>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="element">The element to be altered</param>
        private static void BlankElement(DicomDataset dataset, DicomElement element)
        {
            // Special date/time cases
            if (element is DicomDateTime)
            {
                dataset.AddOrUpdate(new DicomDateTime(element.Tag, DateTime.MinValue));
                return;
            }
            if (element is DicomDate)
            {
                dataset.AddOrUpdate(new DicomDate(element.Tag, DateTime.MinValue));
                return;
            }
            if (element is DicomTime)
            {
                dataset.AddOrUpdate(new DicomTime(element.Tag, new DicomDateRange()));
                return;
            }

            var stringElement = element as DicomStringElement;
            if (stringElement != null)
            {
                dataset.AddOrUpdate(element.Tag, string.Empty);
                return;
            }

            if (IsOtherElement(element)) // Replaces with an empty array
            {
                var ctor = GetConstructor(element, typeof(DicomTag));
                var item = (DicomItem)ctor.Invoke(new object[] { element.Tag });
                dataset.AddOrUpdate(element.Tag, item);
                return;
            }

            var valueType = ElementValueType(element); // Replace with the default value
            if (valueType != null)
            {
                var ctor = GetConstructor(element, typeof(DicomTag), valueType);
                var item = (DicomItem)ctor.Invoke(new[] { element.Tag, Activator.CreateInstance(valueType) });
                dataset.AddOrUpdate(element.Tag, item);
            }
        }

        /// <summary>Evaluates whether an element is of type Other*</summary>
        /// <param name="element">The element to be evaluated</param>
        /// <returns>A boolean flag indicating whether the element is of the expected type, otherwise false</returns>
        private static bool IsOtherElement(DicomElement element)
        {
            var t = element.GetType();
            return t == typeof(DicomOtherByte) || t == typeof(DicomOtherDouble) || t == typeof(DicomOtherFloat)
                   || t == typeof(DicomOtherLong) || t == typeof(DicomOtherWord) || t == typeof(DicomUnknown);
        }

        /// <summary>Evaluates whether an element has a generic valueType</summary>
        /// <param name="element">The element to be evaluated</param>
        /// <returns>The data type if found, otherwise null</returns>
        private static Type ElementValueType(DicomElement element)
        {
            var t = element.GetType();
#if NET35
            if (t.IsGenericType && !t.ContainsGenericParameters && t.GetGenericTypeDefinition() == typeof(DicomValueElement<>)) return t.GetGenericArguments()[0];
#else
            if (t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(DicomValueElement<>)) return t.GenericTypeArguments[0];
#endif
            return null;
        }

        /// <string>Replaces the content of an element</string>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="element">The element to be altered</param>
        /// <param name="newString">The replacement string</param>
        private static void ReplaceString(DicomDataset dataset, DicomElement element, string newString)
        {
            dataset.AddOrUpdate(element.Tag, newString);
        }

        /// <summary>
        /// Use reflection to get strongly-typed constructor info from <paramref name="element"/>.
        /// </summary>
        /// <param name="element">DICOM element for which constructor info should be obtained.</param>
        /// <param name="parameterTypes">Expected parameter types in the requested constructor.</param>
        /// <returns>Constructor info corresponding to <paramref name="element"/> and <paramref name="parameterTypes"/>.</returns>
        private static ConstructorInfo GetConstructor(DicomElement element, params Type[] parameterTypes)
        {
#if PORTABLE || NETSTANDARD || NETFX_CORE
            return element.GetType().GetTypeInfo().DeclaredConstructors.Single(
                ci =>
                    {
                        var pars = ci.GetParameters().Select(par => par.ParameterType);
                        return pars.SequenceEqual(parameterTypes);
                    });
#else
            return element.GetType().GetConstructor(parameterTypes);
#endif
        }

        #endregion
    }
}
