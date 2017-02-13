using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Dicom
{
    public class DicomAnonymizer
    {        
        #region Embedded types
        /// <summary>Profile options as described in DICOM PS 3.15-2011</summary>
        /// <see>ftp://medical.nema.org/medical/dicom/2011/11_15pu.pdf</see>
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

        /// <summary>Profile actions per tag as described in DICOM PS 3.15-2011</summary>
        /// <see>ftp://medical.nema.org/medical/dicom/2011/11_15pu.pdf</see>
        public enum SecurityProfileActions : byte
        {
            D = 1, // Replace with a non-zero length value that may be a dummy value and consistent with the VR
            Z = 2, // Replace with a zero length value, or a non-zero length value that may be a dummy value and consistent with the VR
            X = 4, // Remove
            K = 8, // Keep (unchanged for non-sequence attributes, cleaned for sequences)
            C = 16, // Clean, that is replace with values of similar meaning known not to contain identifying information and consistent with the VR
            U = 32  // Replace with a non-zero length UID that is internally consistent within a set of Instances            
        }

        /// <summary>Security profile container</summary>
        public class SecurityProfile : Dictionary<Regex, SecurityProfileActions> 
        {
            /// <summary>Optional. Replacement patient name (random or alias)</summary>
            public string PatientName = null;
            /// <summary>Optional. Replacement patient ID<summary>
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

                while (true) 
                {
                    var line = source.ReadLine();
                    if (line == null)
                        break;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Trim().Split(';');
                    if (parts.Length == 0)
                        continue;

                    var tag = new Regex(parts[0], RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var empty = default(char).ToString();

                    for (int i=0; i<10; i++)
                    {
                        var flag = (SecurityProfileOptions)(1 << i);

                        if ((options & flag) == flag)
                        {
                            var action = parts[i+1].FirstOrDefault().ToString();
                            if (action != empty)
                            {
                                profile[tag] = (SecurityProfileActions)Enum.Parse(typeof(SecurityProfileActions), action);
                            }
                        }
                    }
                }

                return profile;
            }

            /// <summary>De-identification map taken from DICOM PS 3.15-2011: ftp://medical.nema.org/medical/dicom/2011/11_15pu.pdf</summary>
            /// <remarks>The CSV columns are:
            /// - Tag (regex)
            /// - BasicProfile
            /// - RetainSafePrivateOption
            /// - RetainUIDsOption
            /// - RetainDeviceIdentOption
            /// - RetainPatientCharsOption
            /// - RetainLongFullDatesOption
            /// - RetainLongModifDatesOption
            /// - CleanDescOption
            /// - CleanStructdContOption
            /// - CleanGraphOption</remarks>
            /// <remarks>Not handled: 
            /// </remarks>
            private static string DefaultProfile = @"
                [0-9A-F]{3}[13579BDF],[0-9A-F]{4};X;C;;;;;;;;
                50[0-9A-F]{2},[0-9A-F]{4};X;;;;;;;;;C
                60[0-9A-F]{2},4000;X;;;;;;;;;C
                60[0-9A-F]{2},3000;X;;;;;;;;;C                
                0008,0050;Z;;;;;;;;;
                0018,4000;X;;;;;;;C;;
                0040,0555;X;;;;;;;;C;
                0008,0022;X/Z;;;;;K;C;;;
                0008,002A;X/D;;;;;K;C;;;
                0018,1400;X/D;;;;;;;C;;
                0018,9424;X;;;;;;;C;;
                0008,0032;X/Z;;;;;K;C;;;
                0040,4035;X;;;;;;;;;
                0010,21B0;X;;;;;;;C;;
                0038,0010;X;;;;;;;;;
                0038,0020;X;;;;;K;C;;;
                0008,1084;X;;;;;;;C;;
                0008,1080;X;;;;;;;C;;
                0038,0021;X;;;;;K;C;;;
                0000,1000;X;;K;;;;;;;
                0010,2110;X;;;;C;;;C;;
                4000,0010;X;;;;;;;;;
                0040,A078;X;;;;;;;;;
                0010,1081;X;;;;;;;;;
                0018,1007;X;;;K;;;;;;
                0040,0280;X;;;;;;;C;;
                0020,9161;U;;K;;;;;;;
                0040,3001;X;;;;;;;;;
                0070,0086;X;;;;;;;;;
                0070,0084;Z;;;;;;;;;
                0008,0023;Z/D;;;;;K;C;;;
                0040,A730;X;;;;;;;;C;
                0008,0033;Z/D;;;;;K;C;;;
                0008,010D;U;;K;;;;;;;
                0018,0010;Z/D;;;;;;;C;;
                0018,A003;X;;;;;;;C;;
                0010,2150;X;;;;;;;;;
                0008,9123;U;;K;;;;;;;
                0038,0300;X;;;;;;;;;
                0008,0025;X;;;;;K;C;;;
                0008,0035;X;;;;;K;C;;;
                0040,A07C;X;;;;;;;;;
                FFFC,FFFC;X;;;;;;;;;
                0008,2111;X;;;;;;;C;;
                0018,700A;X;;;K;;;;;;
                0018,1000;X/Z/D;;;K;;;;;;
                0018,1002;U;;K;K;;;;;;
                0400,0100;X;;;;;;;;;
                FFFA,FFFA;X;;;;;;;;;
                0020,9164;U;;K;;;;;;;
                0038,0040;X;;;;;;;C;;
                4008,011A;X;;;;;;;;;
                4008,0119;X;;;;;;;;;
                300A,0013;U;;K;;;;;;;
                0010,2160;X;;;;K;;;;;
                0008,0058;U;;K;;;;;;;
                0070,031A;U;;K;;;;;;;
                0040,2017;Z;;;;;;;;;
                0020,9158;X;;;;;;;C;;
                0020,0052;U;;K;;;;;;;
                0018,1008;X;;;K;;;;;;
                0018,1005;X;;;K;;;;;;
                0070,0001;D;;;;;;;;;C
                0040,4037;X;;;;;;;;;
                0040,4036;X;;;;;;;;;
                0088,0200;X;;;;;;;;;
                0008,4000;X;;;;;;;C;;
                0020,4000;X;;;;;;;C;;
                0028,4000;X;;;;;;;;;
                0040,2400;X;;;;;;;C;;
                4008,0300;X;;;;;;;C;;
                0008,0014;U;;K;;;;;;;
                0008,0081;X;;;;;;;;;
                0008,0082;X/Z/D;;;;;;;;;
                0008,0080;X/Z/D;;;;;;;;;
                0008,1040;X;;;;;;;;;
                0010,1050;X;;;;;;;;;
                0040,1011;X;;;;;;;;;
                4008,0111;X;;;;;;;;;
                4008,010C;X;;;;;;;;;
                4008,0115;X;;;;;;;C;;
                4008,0202;X;;;;;;;;;
                4008,0102;X;;;;;;;;;
                4008,010B;X;;;;;;;C;;
                4008,010A;X;;;;;;;;;
                0008,3010;U;;K;;;;;;;
                0038,0011;X;;;;;;;;;
                0010,0021;X;;;;;;;;;
                0038,0061;X;;;;;;;;;
                0028,1214;U;;K;;;;;;;
                0010,21D0;X;;;;;K;C;;;
                0400,0404;X;;;;;;;;;
                0002,0003;U;;K;;;;;;;
                0010,2000;X;;;;;;;C;;
                0010,1090;X;;;;;;;;;
                0010,1080;X;;;;;;;;;
                0400,0550;X;;;;;;;;;
                0020,3406;X;;;;;;;;;
                0020,3401;X;;;;;;;;;
                0020,3404;X;;;;;;;;;
                0008,1060;X;;;;;;;;;
                0040,1010;X;;;;;;;;;
                0010,2180;X;;;;;;;C;;
                0008,1072;X/D;;;;;;;;;
                0008,1070;X/Z/D;;;;;;;;;
                0040,2010;X;;;;;;;;;
                0040,2008;X;;;;;;;;;
                0040,2009;X;;;;;;;;;
                0400,0561;X;;;;;;;;;
                0010,1000;X;;;;;;;;;
                0010,1002;X;;;;;;;;;
                0010,1001;X;;;;;;;;;
                0008,0024;X;;;;;K;C;;;
                0008,0034;X;;;;;K;C;;;
                0028,1199;U;;K;;;;;;;
                0040,A07A;X;;;;;;;;;
                0010,1040;X;;;;;;;;;
                0010,4000;X;;;;;;;C;;
                0010,0020;Z;;;;;;;;;
                0010,2203;X/Z;;;;K;;;;;
                0038,0500;X;;;;C;;;C;;
                0040,1004;X;;;;;;;;;
                0010,1010;X;;;;K;;;;;
                0010,0030;Z;;;;;;;;;
                0010,1005;X;;;;;;;;;
                0010,0032;X;;;;;;;;;
                0038,0400;X;;;;;;;;;
                0010,0050;X;;;;;;;;;
                0010,1060;X;;;;;;;;;
                0010,0010;Z;;;;;;;;;
                0010,0101;X;;;;;;;;;
                0010,0102;X;;;;;;;;;
                0010,21F0;X;;;;;;;;;
                0010,0040;Z;;;;K;;;;;
                0010,1020;X;;;;K;;;;;
                0010,2154;X;;;;;;;;;
                0010,1030;X;;;;K;;;;;
                0040,0243;X;;;;;;;;;
                0040,0254;X;;;;;;;C;;
                0040,0253;X;;;;;;;;;
                0040,0244;X;;;;;K;C;;;
                0040,0245;X;;;;;K;C;;;
                0040,0241;X;;;K;;;;;;
                0040,4030;X;;;K;;;;;;
                0040,0242;X;;;K;;;;;;
                0040,0248;X;;;K;;;;;;
                0008,1052;X;;;;;;;;;
                0008,1050;X;;;;;;;;;
                0040,1102;X;;;;;;;;;
                0040,1101;D;;;;;;;;;
                0040,A123;D;;;;;;;;;
                0040,1103;X;;;;;;;;;
                4008,0114;X;;;;;;;;;
                0008,1062;X;;;;;;;;;
                0008,1048;X;;;;;;;;;
                0008,1049;X;;;;;;;;;
                0040,2016;Z;;;;;;;;;
                0018,1004;X;;;K;;;;;;
                0010,21C0;X;;;;K;;;;;
                0040,0012;X;;;;C;;;;;
                0018,1030;X/D;;;;;;;C;;
                0040,2001;X;;;;;;;C;;
                0032,1030;X;;;;;;;C;;
                0400,0402;X;;;;;;;;;
                3006,0024;U;;K;;;;;;;
                0040,4023;U;;K;;;;;;;
                0008,1140;X/Z/U*;;K;;;;;;;
                0038,1234;X;;;;;;;;;
                0008,1120;X;;X;;;;;;;
                0008,1111;X/Z/D;;K;;;;;;;
                0400,0403;X;;;;;;;;;
                0008,1155;U;;K;;;;;;;
                0004,1511;U;;K;;;;;;;
                0008,1110;X/Z;;K;;;;;;;
                0008,0092;X;;;;;;;;;
                0008,0096;X;;;;;;;;;
                0008,0090;Z;;;;;;;;;
                0008,0094;X;;;;;;;;;
                0010,2152;X;;;;;;;;;
                3006,00C2;U;;K;;;;;;;
                0040,0275;X;;;;;;;C;;
                0032,1070;X;;;;;;;C;;
                0040,1400;X;;;;;;;C;;
                0032,1060;X/Z;;;;;;;C;;
                0040,1001;X;;;;;;;;;
                0040,1005;X;;;;;;;;;
                0000,1001;U;;K;;;;;;;
                0032,1032;X;;;;;;;;;
                0032,1033;X;;;;;;;;;
                0010,2299;X;;;;;;;;;
                0010,2297;X;;;;;;;;;
                4008,4000;X;;;;;;;C;;
                4008,0118;X;;;;;;;;;
                4008,0042;X;;;;;;;;;
                300E,0008;X/Z;;;;;;;;;
                0040,4034;X;;;;;;;;;
                0038,001E;X;;;;;;;;;
                0040,000B;X;;;;;;;;;
                0040,0006;X;;;;;;;;;
                0040,0007;X;;;;;;;C;;
                0040,0004;X;;;;;K;C;;;
                0040,0005;X;;;;;K;C;;;
                0040,0011;X;;;K;;;;;;
                0040,0002;X;;;;;K;C;;;
                0040,0003;X;;;;;K;C;;;
                0040,0001;X;;;K;;;;;;
                0040,4027;X;;;K;;;;;;
                0040,0010;X;;;K;;;;;;
                0040,4025;X;;;K;;;;;;
                0032,1020;X;;;K;;;;;;
                0032,1021;X;;;K;;;;;;
                0008,0021;X/D;;;;;K;C;;;
                0008,103E;X;;;;;;;C;;
                0020,000E;U;;K;;;;;;;
                0008,0031;X/D;;;;;K;C;;;
                0038,0062;X;;;;;;;C;;
                0038,0060;X;;;;;;;;;
                0010,21A0;X;;;;K;;;;;
                0008,0018;U;;K;;;;;;;
                0008,2112;X/Z/U*;;K;;;;;;;
                0038,0050;X;;;;C;;;;;
                0008,1010;X/Z/D;;;K;;;;;;
                0088,0140;U;;K;;;;;;;
                0032,4000;X;;;;;;;C;;
                0008,0020;Z;;;;;K;C;;;
                0008,1030;X;;;;;;;C;;
                0020,0010;Z;;;;;;;;;
                0032,0012;X;;;;;;;;;
                0020,000D;U;;K;;;;;;;
                0008,0030;Z;;;;;K;C;;;
                0020,0200;U;;K;;;;;;;
                0040,DB0D;U;;K;;;;;;;
                0040,DB0C;U;;K;;;;;;;
                4000,4000;X;;;;;;;;;
                2030,0020;X;;;;;;;;;
                0008,0201;X;;;;;K;C;;;
                0088,0910;X;;;;;;;;;
                0088,0912;X;;;;;;;;;
                0088,0906;X;;;;;;;;;
                0088,0904;X;;;;;;;;;
                0008,1195;U;;K;;;;;;;
                0040,A124;U;;;;;;;;;
                0040,A088;Z;;;;;;;;;
                0040,A075;D;;;;;;;;;
                0040,A073;D;;;;;;;;;
                0040,A027;X;;;;;;;;;
                0038,4000;X;;;;;;;C;;";                    
        }
        #endregion

        #region Public properties
        /// <summary>Context/Output. Contains all the replaced UIDs.</summary>
        /// <remarks>Useful for consistency across a file set (multiple calls to anonymization methods)</remarks>
        public Dictionary<string, string> ReplacedUIDs { get; private set; } = new Dictionary<string, string>();

        /// <summary>The security profile for this anonymizer instance</summary>
        public SecurityProfile Profile { get; private set; }
        #endregion

        #region Public methods
        /// <summary>Public constructor</summary>
        /// <param name="profile">Optional. The security profile to be used in one or multiple anonymizations. 
        /// If not specified or null, it will use the default/internal profile</param>
        public DicomAnonymizer(SecurityProfile profile = null)
        {
            this.Profile = profile ?? SecurityProfile.LoadProfile(null, SecurityProfileOptions.BasicProfile);
        }

        /// <summary>Anonymizes a dataset witout cloning</summary>
        /// <param name="dataset">The dataset to be altered</param>
        public void AnonymizeInPlace(DicomDataset dataset)
        {
            this.PerformAnonymization(dataset, false);
        }

        /// <summary>Clones and anonymizes a dataset</summary>
        /// <param name="dataset">The dataset to be cloned and anonymized</param>
        /// <returns></returns>
        public DicomDataset Anonymize(DicomDataset dataset)
        {
            return this.PerformAnonymization(dataset, true);
        }

        /// <summary>Anonymizes the dataset of an existing Dicom file</summary>
        /// <param name="file">The file containing the dataset to be altered</param>
        public void AnonymizeInPlace(DicomFile file)
        {
            this.PerformAnonymization(file.Dataset, false);

            if (file.FileMetaInfo != null)
                file.FileMetaInfo.MediaStorageSOPInstanceUID = file.Dataset.Get<DicomUID>(DicomTag.SOPInstanceUID);
        }

        /// <summary>Creates a new Dicom file with an anonymized dataset</summary>
        /// <param name="file">The file containing the original dataset</param>
        /// <returns></returns>
        public DicomFile Anonymize(DicomFile file)
        {
            var newDataset = this.PerformAnonymization(file.Dataset, true);
            return new DicomFile(newDataset);
        }        
        #endregion

        #region Private methods
        /// <summary>Anonymizes an entire dataset</summary>
        /// <param name="dataset">The dataset to be anonymized</param>
        public DicomDataset PerformAnonymization(DicomDataset dataset, bool clone)
        {
            if (clone)
                dataset = dataset.Clone();

            var toRemove = new List<DicomItem>();
            var itemList = dataset.ToArray();

            foreach (var item in itemList)
            {
                ushort group = item.Tag.Group;
                var element = item as DicomElement;

                if (element == null)
                    continue;

                if (element.Tag.Equals(Dicom.DicomTag.PatientName) && this.Profile.PatientName != null)
                {
                    ReplaceString(dataset, element, this.Profile.PatientName);
                }
                else if (element.Tag.Equals(Dicom.DicomTag.PatientID) && this.Profile.PatientID != null)
                {
                    ReplaceString(dataset, element, this.Profile.PatientID);
                }

                var parenthesis = new [] { '(', ')' };
                var tag = item.Tag.ToString().Trim(parenthesis);
                var action = this.Profile.FirstOrDefault(pair => pair.Key.IsMatch(tag));
                if (action.Key != null)
                {
                    var VR = item.ValueRepresentation;

                    switch (action.Value)
                    {
                        case SecurityProfileActions.U: // UID
                        case SecurityProfileActions.C: // Clean
                        case SecurityProfileActions.D: // Dummy
                            if (VR == DicomVR.UI)
                                ReplaceUID(dataset, element);
                            else if (VR.IsString)
                                ReplaceString(dataset, element, "ANONYMOUS");
                            else
                                BlankElement(dataset, element);
                            break;
                        case SecurityProfileActions.K: // Keep
                            break;
                        case SecurityProfileActions.X: // Remove
                            toRemove.Add(element);
                            break;
                        case SecurityProfileActions.Z: // Zero-length
                            BlankElement(dataset, element);
                            break;
                    }
                }
            }

            dataset.Remove(item => toRemove.Contains(item));
            return dataset;
        }

        /// <summary>Blanks an element by passing to it an empty string</summary>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="element">The element to be altered</param>
        private void BlankElement(DicomDataset dataset, DicomElement element)
        {
            var stringElement = element as DicomStringElement;
            if (stringElement != null)
            {
                dataset.AddOrUpdate(element.Tag, "");
                return;
            }
            if (IsOtherElement(element))  // Replaces with an empty array
            {
                var ctor = element.GetType().GetConstructor(new Type[] { typeof(DicomTag) });
                var item = (DicomItem)ctor.Invoke(new [] { element.Tag });
                dataset.AddOrUpdate(element.Tag, item); 
                return;
            }

            Type valueType = ElementValueType(element);   // Replace with the default value
            if (valueType != null)
            {
                var ctor = element.GetType().GetConstructor(new Type[] { typeof(DicomTag), valueType });
                var item = (DicomItem)ctor.Invoke(new [] { element.Tag, Activator.CreateInstance(valueType) });
                dataset.AddOrUpdate(element.Tag, item); 
                return;
            }

            // Special date/time cases
            if (element is DicomDateTime)
            {
                dataset.AddOrUpdate(new DicomDateTime(element.Tag, DateTime.MinValue));
            }
            else if (element is DicomDate)
            {
                dataset.AddOrUpdate(new DicomDate(element.Tag, DateTime.MinValue));
            }
            else if (element is DicomTime)
            {
                dataset.AddOrUpdate(new DicomTime(element.Tag, new DicomDateRange()));
            }
        }

        /// <summary>Evaluates whether an element is of type Other*</summary>
        /// <param name="element">The element to be evaluated</param>
        /// <returns>A boolean flag indicating whether the element is of the expected type, otherwise false</returns>
        private bool IsOtherElement(DicomElement element)
        {
            var t = element.GetType();
            return t == typeof(DicomOtherByte) || t == typeof(DicomOtherDouble) || t == typeof(DicomOtherFloat) ||
                t == typeof(DicomOtherLong) || t == typeof(DicomOtherWord) || t == typeof(DicomUnknown);
        }

        /// <summary>Evaluates whether an element has a generic valueType</summary>
        /// <param name="element">The element to be evaluated</param>
        /// <returns>The data type if found, otherwise null</returns>
        private Type ElementValueType(DicomElement element)
        {
            var t = element.GetType();
            if (t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(DicomValueElement<>))
                return t.GenericTypeArguments[0];
            else
                return null;
        }

        /// <string>Replaces the content of an element</string>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="element">The element to be altered</param>
        /// <param name="newString">The replacement string</param>
        private void ReplaceString(DicomDataset dataset, DicomElement element, string newString)
        {
            dataset.AddOrUpdate<string>(element.Tag, newString);
        }

        /// <string>Replaces the content of a UID with a random one</string>
        /// <param name="dataset">Reference to the dataset</param>
        /// <param name="element">The element to be altered</param>
        /// <param name="newString">The object containing the replacement context</param>
        private void ReplaceUID(DicomDataset dataset, DicomElement element)
        {
            string rep;
            DicomUID uid;
            var old = element.Get<string>();            

            if (this.ReplacedUIDs.ContainsKey(old))
            {
                rep = this.ReplacedUIDs[old];
                uid = new DicomUID(rep, "Anonymized UID", DicomUidType.Unknown);
            }
            else
            {
                uid = DicomUIDGenerator.GenerateDerivedFromUUID();
                rep = uid.UID;
                this.ReplacedUIDs[old] = rep; 
            }

            var newItem = new DicomUniqueIdentifier(element.Tag, uid);
            dataset.AddOrUpdate(newItem);
        }
        #endregion
    }
}
