// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using FellowOakDicom.IO;
using FellowOakDicom.Log;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace FellowOakDicom.Printing
{

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
        public static readonly DicomUID ColorSOPClassUID = DicomUID.BasicColorImageBox;

        /// <summary>
        /// Basic gray scale image box SOP
        /// </summary>
        public static readonly DicomUID GraySOPClassUID = DicomUID.BasicGrayscaleImageBox;

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
                if (SOPClassUID == ColorSOPClassUID || Contains(DicomTag.BasicColorImageSequence))
                {
                    seq = TryGetSequence(DicomTag.BasicColorImageSequence, out var dummy) ? dummy : null;
                }
                if (seq == null)
                {
                    seq = TryGetSequence(DicomTag.BasicGrayscaleImageSequence, out var dummy) ? dummy : null;
                }

                return seq?.Items?.FirstOrDefault();
            }
            set
            {
                if (SOPClassUID == ColorSOPClassUID)
                {
                    AddOrUpdate(DicomTag.BasicColorImageSequence, value);
                }
                else
                {
                    AddOrUpdate(DicomTag.BasicGrayscaleImageSequence, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the image on the film, based on image display format. See C.13.5.1 for specification.
        /// </summary>
        public ushort ImageBoxPosition
        {
            get => GetSingleValueOrDefault(DicomTag.ImageBoxPosition, (ushort)1);
            set => AddOrUpdate(DicomTag.ImageBoxPosition, value);
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
            get => GetSingleValueOrDefault(DicomTag.Polarity, "NORMAL");
            set => AddOrUpdate(DicomTag.Polarity, value);
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
            get => GetSingleValueOrDefault(DicomTag.MagnificationType, FilmBox.MagnificationType);
            set => AddOrUpdate(DicomTag.MagnificationType, value);
        }

        /// <summary>
        /// Further specifies the type of the interpolation function. Values are defined in Conformance Statement.
        /// Only valid for Magnification Type (2010,0060) = CUBIC
        /// </summary>
        public string SmoothingType
        {
            get => GetSingleValueOrDefault(DicomTag.SmoothingType, FilmBox.SmoothingType);
            set => AddOrUpdate(DicomTag.SmoothingType, value);
        }

        /// <summary>
        /// Maximum density of the images on the film, expressed in hundredths of OD. 
        /// If Max Density is higher than maximum printer density than Max Density is set to maximum printer density.
        /// </summary>
        public ushort MaxDensity
        {
            get => GetSingleValueOrDefault(DicomTag.MaxDensity, FilmBox.MaxDensity);
            set => AddOrUpdate(DicomTag.MaxDensity, value);
        }

        /// <summary>
        /// Minimum density of the images on the film, expressed in hundredths of OD. 
        /// If Min Density is lower than minimum printer density than Min Density is set to minimum printer density.
        /// </summary>
        public ushort MinDensity
        {
            get => GetSingleValueOrDefault(DicomTag.MinDensity, FilmBox.MinDensity);
            set => AddOrUpdate(DicomTag.MinDensity, value);
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
            get => GetSingleValueOrDefault(DicomTag.ConfigurationInformation, FilmBox.ConfigurationInformation);
            set => AddOrUpdate(DicomTag.ConfigurationInformation, value);
        }

        /// <summary>
        /// Width (x-dimension) in mm of the image to be printed. This value overrides the size that corresponds with 
        /// optimal filling of the Image Box.
        /// </summary>
        public double RequestedImageSize
        {
            get => GetSingleValueOrDefault(DicomTag.RequestedImageSize, 0.0);
            set => AddOrUpdate(DicomTag.RequestedImageSize, value);
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
            get => GetSingleValueOrDefault(DicomTag.RequestedDecimateCropBehavior, "DECIMATE");
            set => AddOrUpdate(DicomTag.RequestedDecimateCropBehavior, value);
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
            InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            FilmBox = filmBox;
            SOPClassUID = sopClass;
            SOPInstanceUID = string.IsNullOrEmpty(sopInstance?.UID) ? DicomUID.Generate() : sopInstance;

            Add(DicomTag.SOPClassUID, SOPClassUID);
            Add(DicomTag.SOPInstanceUID, SOPInstanceUID);

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
            InternalTransferSyntax = imageBox.InternalTransferSyntax;
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

            var imageBoxFileRef = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(imageBoxTextFile);
            using var writer = new StreamWriter(imageBoxFileRef.Create());
            writer.Write(this.WriteToString());

            var file = new DicomFile(this);
            file.Save(imageBoxDicomFile);
        }

        #endregion
    }
}
