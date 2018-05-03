// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Printing
{
    using System.IO;

    using Dicom.IO;
    using Dicom.Log;

    /// <summary>
    /// Color or gray scale basic image box
    /// </summary>
    public class ImageBox : DicomDataset
    {
        #region Properties and Attributes

        //border in 100th of inches
        protected const float BORDER = (float)(100 * 2 / 25.4);

        public FilmBox FilmBox { get; private set; }

        /// <summary>
        /// Basic color image box SOP
        /// </summary>
        public static readonly DicomUID ColorSOPClassUID = DicomUID.BasicColorImageBoxSOPClass;

        /// <summary>
        /// Basic gray scale image box SOP
        /// </summary>
        public static readonly DicomUID GraySOPClassUID = DicomUID.BasicGrayscaleImageBoxSOPClass;

        /// <summary>
        /// Image box SOP class UID
        /// </summary>
        public DicomUID SOPClassUID { get; private set; }

        /// <summary>
        /// Image box SOP instance UID
        /// </summary>
        public DicomUID SOPInstanceUID { get; private set; }

        /// <summary>
        /// Gets or sets the color or grayscale basic image sequence
        /// </summary>
        public DicomDataset ImageSequence
        {
            get
            {
                DicomSequence seq = null;
                if (SOPClassUID == ColorSOPClassUID || this.Contains(DicomTag.BasicColorImageSequence))
                {
                    seq = this.Get<DicomSequence>(DicomTag.BasicColorImageSequence, null);
                }
                if (seq == null)
                {
                    seq = this.Get<DicomSequence>(DicomTag.BasicGrayscaleImageSequence, null);
                }

                if (seq != null && seq.Items.Count > 0)
                {
                    return seq.Items[0];
                }

                return null;
            }
            set
            {
                if (SOPClassUID == ColorSOPClassUID)
                {
                    this.AddOrUpdate(DicomTag.BasicColorImageSequence, value);
                }
                else
                {
                    this.AddOrUpdate(DicomTag.BasicGrayscaleImageSequence, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the image on the film, based on image display format. See C.13.5.1 for specification.
        /// </summary>
        public ushort ImageBoxPosition
        {
            get
            {
                return this.Get<ushort>(DicomTag.ImageBoxPosition, 1);
            }
            set
            {
                this.AddOrUpdate(DicomTag.ImageBoxPosition, value);
            }
        }

        /// <summary>
        /// Specifies whether minimum pixel values (after VOI LUT transformation) are to printed black or white.
        /// </summary>
        /// <remarks>
        /// Enumerated Values:
        /// <list type="bullet"></list>
        /// <item>
        ///   <term>NORMAL</term>
        ///   <description>pixels shall be printed as specified by the Photometric Interpretation (0028,0004)</description>
        /// </item>
        /// <item>
        ///   <term>REVERSE</term>
        ///   <description>pixels shall be printed with the opposite polarity as specified by the Photometric 
        ///   Interpretation (0028,0004)</description>
        /// </item>
        /// 
        /// If Polarity (2020,0020) is not specified by the SCU, the SCP shall print with NORMAL polarity.
        /// </remarks>
        public string Polarity
        {
            get
            {
                return this.Get(DicomTag.Polarity, "NORMAL");
            }
            set
            {
                this.AddOrUpdate(DicomTag.Polarity, value);
            }
        }

        /// <summary>
        /// Interpolation type by which the printer magnifies or decimates the image in order to fit the image
        /// in the image box on film
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="bullet">
        /// <item><description>REPLICATE</description></item>
        /// <item><description>BILINEAR</description></item>
        /// <item><description>CUBIC</description></item>
        /// <item><description>NONE</description></item>
        /// </list>
        /// </remarks>
        public string MagnificationType
        {
            get
            {
                return this.Get(DicomTag.MagnificationType, FilmBox.MagnificationType);
            }
            set
            {
                this.AddOrUpdate(DicomTag.MagnificationType, value);
            }
        }

        /// <summary>
        /// Further specifies the type of the interpolation function. Values are defined in Conformance Statement.
        /// Only valid for Magnification Type (2010,0060) = CUBIC
        /// </summary>
        public string SmoothingType
        {
            get
            {
                return this.Get(DicomTag.SmoothingType, FilmBox.SmoothingType);
            }
            set
            {
                this.AddOrUpdate(DicomTag.SmoothingType, value);
            }
        }

        /// <summary>
        /// Maximum density of the images on the film, expressed in hundredths of OD. 
        /// If Max Density is higher than maximum printer density than Max Density is set to maximum printer density.
        /// </summary>
        public ushort MaxDensity
        {
            get
            {
                return this.Get<ushort>(DicomTag.MaxDensity, FilmBox.MaxDensity);
            }
            set
            {
                this.AddOrUpdate(DicomTag.MaxDensity, value);
            }
        }

        /// <summary>
        /// Minimum density of the images on the film, expressed in hundredths of OD. 
        /// If Min Density is lower than minimum printer density than Min Density is set to minimum printer density.
        /// </summary>
        public ushort MinDensity
        {
            get
            {
                return this.Get<ushort>(DicomTag.MinDensity, FilmBox.MinDensity);
            }
            set
            {
                this.AddOrUpdate(DicomTag.MinDensity, value);
            }
        }

        /// <summary>
        /// Character string that contains either the ID of the printer configuration table that contains a set of values for 
        /// implementation specific print parameters (e.g. perception LUT related parameters) or one or more configuration data
        /// values, encoded as characters. If there are multiple configuration data values encoded in the string, they shall be 
        /// separated by backslashes. The definition of values shall be contained in the SCP's Conformance Statement.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="">
        /// <item>
        ///   <term>CS000-CS999</term>
        ///   <description>Implementation specific curve type.</description>
        /// </item>
        /// </list>
        /// Note: It is recommended that for SCPs, CS000 represent the lowest contrast and CS999 
        /// the highest contrast levels available.
        /// </remarks>
        public string ConfigurationInformation
        {
            get
            {
                return this.Get(DicomTag.ConfigurationInformation, FilmBox.ConfigurationInformation);
            }
            set
            {
                this.AddOrUpdate(DicomTag.ConfigurationInformation, value);
            }
        }

        /// <summary>
        /// Width (x-dimension) in mm of the image to be printed. This value overrides the size that corresponds with 
        /// optimal filling of the Image Box.
        /// </summary>
        public double RequestedImageSize
        {
            get
            {
                return this.Get<double>(DicomTag.RequestedImageSize, 0.0);
            }
            set
            {
                this.AddOrUpdate(DicomTag.RequestedImageSize, value);
            }
        }

        /// <summary>
        /// Specifies whether image pixels are to be decimated or cropped if the image 
        /// rows or columns is greater than the available printable pixels in an Image Box.
        /// </summary>
        /// <remarks>
        /// Decimation  means that a magnification factor &lt;1 is applied to the image. The method 
        /// of decimation shall be that specified by Magnification Type (2010,0060) or the SCP 
        /// default if not specified.
        /// 
        /// Cropping means that some image rows and/or columns are deleted before printing.
        /// 
        /// Enumerated Values:
        /// <list type="bullet">
        /// <item>
        ///   <term>DECIMATE</term>
        ///   <description>a magnification factor &lt;1 to be applied to the image.</description>
        /// </item>
        /// <item>
        ///   <term>CROP</term>
        ///   <description>some image rows and/or columns are to be deleted before printing. The 
        ///   specific algorithm for cropping shall be described in the SCP Conformance Statement.</description>
        /// </item>
        /// <item>
        ///   <term>FAIL</term>
        ///   <description>the SCP shall not crop or decimate</description>
        /// </item>
        /// </list>
        /// </remarks>
        public string RequestedDecimateCropBehavior
        {
            get
            {
                return this.Get(DicomTag.RequestedDecimateCropBehavior, "DECIMATE");
            }
            set
            {
                this.AddOrUpdate(DicomTag.RequestedDecimateCropBehavior, value);
            }
        }

        #endregion

        #region Constructors and Initialization

        /// <summary>
        /// Construct new ImageBox for specified filmBox using specified SOP class UID and SOP instance UID
        /// </summary>
        /// <param name="filmBox">Film box in which image box should be constained.</param>
        /// <param name="sopClass">SOP Class UID for the image.</param>
        /// <param name="sopInstance">SOP instance UID for the image.</param>
        public ImageBox(FilmBox filmBox, DicomUID sopClass, DicomUID sopInstance)
        {
            this.InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            FilmBox = filmBox;
            SOPClassUID = sopClass;
            if (sopInstance == null || sopInstance.UID == string.Empty)
            {
                SOPInstanceUID = DicomUID.Generate();
            }
            else
            {
                SOPInstanceUID = sopInstance;
            }

            this.Add(DicomTag.SOPClassUID, SOPClassUID);
            this.Add(DicomTag.SOPInstanceUID, SOPInstanceUID);

        }

        /// <summary>
        /// Construct new ImageBox cloned from another imagebox.
        /// </summary>
        /// <param name="imageBox">The source ImageBox instance to clone.</param>
        /// <param name="filmBox">The film box in which the cloned image box is contained.</param>
        private ImageBox(ImageBox imageBox, FilmBox filmBox)
            : this(filmBox, imageBox.SOPClassUID, imageBox.SOPInstanceUID)
        {
            imageBox.CopyTo(this);
            this.InternalTransferSyntax = imageBox.InternalTransferSyntax;
        }

        /// <summary>
        /// Clone the current IamgeBox and return new object
        /// </summary>
        /// <returns>Cloned ImageBox</returns>
        public ImageBox Clone(FilmBox filmBox)
        {
            return new ImageBox(this, filmBox);
        }

        #endregion

        #region Load and Save

        /// <summary>
        /// Load image box for a specified <paramref name="filmBox"/> from a specified file.
        /// </summary>
        /// <param name="filmBox">Film box.</param>
        /// <param name="imageBoxFile">Name of the image box file.</param>
        /// <returns>Image box for a specified <paramref name="filmBox"/> from a file named <paramref name="imageBoxFile"/>.</returns>
        public static ImageBox Load(FilmBox filmBox, string imageBoxFile)
        {
            var file = DicomFile.Open(imageBoxFile);

            var imageBox = new ImageBox(
                filmBox,
                file.FileMetaInfo.MediaStorageSOPClassUID,
                file.FileMetaInfo.MediaStorageSOPInstanceUID);

            file.Dataset.CopyTo(imageBox);
            return imageBox;
        }

        /// <summary>
        /// Save the image box contents to file.
        /// </summary>
        /// <param name="imageBoxFile">Name of the image box file.</param>
        public void Save(string imageBoxFile)
        {
            var imageBoxDicomFile = imageBoxFile + ".dcm";
            var imageBoxTextFile = imageBoxFile + ".txt";

            using (var stream = IOManager.CreateFileReference(imageBoxTextFile).Create())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(this.WriteToString());
            }

            var file = new DicomFile(this);
            file.Save(imageBoxDicomFile);
        }

        #endregion
    }
}
