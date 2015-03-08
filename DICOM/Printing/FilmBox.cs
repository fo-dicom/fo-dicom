using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Dicom.Log;


namespace Dicom.Printing
{
    /// <summary>
    /// Basic film box
    /// </summary>
    public class FilmBox : DicomDataset
    {
        #region Properites and Attributes

        private readonly FilmSession _filmSession = null;
        public FilmSession FilmSession
        {
            get { return _filmSession; }
        }
        /// <summary>
        /// Basic film box SOP class UID
        /// </summary>
        public static readonly DicomUID SOPClassUID = DicomUID.BasicFilmBoxSOPClass;

        /// <summary>
        /// Basic film box SOP instance UID
        /// </summary>
        public DicomUID SOPInstanceUID { get; private set; }

        /// <summary>
        /// Basic image boxes list
        /// </summary>
        public IList<ImageBox> BasicImageBoxes { get; private set; }

        /// <summary>
        /// Type of image display format.
        /// </summary>
        /// <remarks>
        /// Enumerated Values:
        /// <list type="bullet">
        /// <item>
        ///   <term>STANDARD\C,R</term>
        ///   <description>
        ///   film contains equal size rectangular image boxes with R rows of image 
        ///   boxes and C columns of image boxes; C and R are integers.
        ///   </description>
        /// </item>
        /// <item>
        ///   <term>ROW\R1,R2,R3, etc.</term>
        ///   <description>
        ///   film contains rows with equal size rectangular image boxes with R1 
        ///   image boxes in the first row, R2 image boxes in second row, R3 image boxes in third 
        ///   row, etc.; R1, R2, R3, etc. are integers.
        ///   </description>
        /// </item>
        /// <item>
        ///   <term>COL\C1,C2,C3, etc.</term>
        ///   <description>
        ///   film contains columns with equal size rectangular image boxes with C1 
        ///   image boxes in the first column, C2 image boxes in second column, C3 image boxes in 
        ///   third column, etc.; C1, C2, C3, etc. are integers.
        ///   </description>
        /// </item>
        /// <item>
        ///   <term>SLIDE</term>
        ///   <description>
        ///   film contains 35mm slides; the number of slides for a particular film 
        ///   size is configuration dependent.
        ///   </description>
        /// </item>
        /// <item>
        ///   <term>SUPERSLIDE</term>
        ///   <description>
        ///   film contains 40mm slides; the number of slides for a particular film 
        ///   size is configuration dependent.
        ///   </description>
        /// </item>
        /// <item>
        ///   <term>CUSTOM\i</term>
        ///   <description>
        ///   film contains a customized ordering of rectangular image boxes; i identifies 
        ///   the image display format; the definition of the image display formats is defined in the 
        ///   Conformance Statement; i is an integer
        ///   .</description>
        /// </item>
        /// </list>
        /// </remarks>
        public string ImageDisplayFormat
        {
            get { return this.Get<string>(DicomTag.ImageDisplayFormat); }
            set { this.Add(DicomTag.ImageDisplayFormat, value); }
        }

        /// <summary>
        /// Film orientation.
        /// </summary>
        /// <remarks>
        /// Enumerated Values:
        /// <list type="bullet">
        /// <item>
        ///   <term>PORTRAIT</term>
        ///   <description>vertical film position</description>
        /// </item>
        /// <item>
        ///   <term>LANDSCAPE</term>
        ///   <description>horizontal film position</description>
        /// </item>
        /// </list>
        /// </remarks>
        public string FilmOrienation
        {
            get { return this.Get<string>(DicomTag.FilmOrientation, "PORTRAIT"); }
            set { this.Add(DicomTag.FilmOrientation, value); }
        }

        /// <summary>
        /// Film size identification.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="bullet">
        /// <item><description>8INX10IN</description></item>
        /// <item><description>8_5INX11IN</description></item>
        /// <item><description>10INX12IN</description></item>
        /// <item><description>10INX14IN</description></item>
        /// <item><description>11INX14IN</description></item>
        /// <item><description>11INX17IN</description></item>
        /// <item><description>14INX14IN</description></item>
        /// <item><description>14INX17IN</description></item>
        /// <item><description>24CMX24CM</description></item>
        /// <item><description>24CMX30CM</description></item>
        /// <item><description>A4</description></item>
        /// <item><description>A3</description></item>
        /// </list>
        /// 
        /// Notes:
        /// 10INX14IN corresponds with 25.7CMX36.4CM
        /// A4 corresponds with 210 x 297 millimeters
        /// A3 corresponds with 297 x 420 millimeters
        /// </remarks>
        public string FilmSizeID
        {
            get { return this.Get<string>(DicomTag.FilmSizeID, "A4"); }
            set { this.Add(DicomTag.FilmSizeID, value); }
        }

        /// <summary>
        /// Interpolation type by which the printer magnifies or decimates the image 
        /// in order to fit the image in the image box on film.
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
            get { return this.Get<string>(DicomTag.MagnificationType, "BILINEAR"); }
            set { this.Add(DicomTag.MagnificationType, value); }
        }

        /// <summary>
        /// Maximum density of the images on the film, expressed in hundredths of OD. If Max Density is higher than 
        /// maximum printer density than Max Density is set to maximum printer density.
        /// </summary>
        public ushort MaxDensity
        {
            get { return this.Get<ushort>(DicomTag.MaxDensity, 0); }
            set { this.Add(DicomTag.MaxDensity, value); }
        }


        /// <summary>
        /// Minimum density of the images on the film, expressed in hundredths of OD. If Min Density is lower than 
        /// minimum printer density than Min Density is set to minimum printer density.
        /// </summary>
        public ushort MinDensity
        {
            get { return this.Get<ushort>(DicomTag.MinDensity, 0); }
            set { this.Add(DicomTag.MinDensity, value); }
        }

        /// <summary>
        /// Character string that contains either the ID of the printer configuration 
        /// table that contains a set of values for implementation specific print parameters 
        /// (e.g. perception LUT related parameters) or one or more configuration data values, 
        /// encoded as characters. If there are multiple configuration data values encoded in 
        /// the string, they shall be separated by backslashes. The definition of values shall 
        /// be contained in the SCP's Conformance Statement.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="">
        /// <item>
        ///   <term>CS000-CS999</term>
        ///   <description>Implementation specific curve type.</description></item>
        /// </list>
        /// 
        /// Note: It is recommended that for SCPs, CS000 represent the lowest contrast and CS999 
        /// the highest contrast levels available.
        /// </remarks>
        public string ConfigurationInformation
        {
            get { return this.Get(DicomTag.ConfigurationInformation, string.Empty); }
            set { this.Add(DicomTag.ConfigurationInformation, value); }
        }

        /// <summary>
        /// Identification of annotation display format. The definition of the annotation 
        /// display formats and the annotation box position sequence are defined in the Conformance 
        /// Statement.
        /// </summary>
        public string AnnotationDisplayFormatID
        {
            get { return this.Get(DicomTag.AnnotationDisplayFormatID, string.Empty); }
            set { this.Add(DicomTag.AnnotationDisplayFormatID, value); }
        }

        /// <summary>
        /// Further specifies the type of the interpolation function. Values are defined in Conformance Statement.
        /// Only valid for Magnification Type (2010,0060) = CUBIC
        /// </summary>
        public string SmoothingType
        {
            get { return this.Get(DicomTag.SmoothingType, string.Empty); }
            set { this.Add(DicomTag.SmoothingType, value); }
        }

        /// <summary>
        /// Density of the film areas surrounding and between images on the film.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="bullet">
        /// <item><description>BLACK</description></item>
        /// <item><description>WHITE</description></item>
        /// <item>
        /// <description>
        /// i where i represents the desired density in hundredths of OD (e.g. 150 corresponds with 1.5 OD)
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public string BorderDensity
        {
            get { return this.Get(DicomTag.BorderDensity, "BLACK"); }
            set { this.Add(DicomTag.BorderDensity, value); }
        }


        /// <summary>
        /// Density of the image box area on the film that contains no image.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="bullet">
        /// <item><description>BLACK</description></item>
        /// <item><description>WHITE</description></item>
        /// <item><description>
        /// i where i represents the desired density in hundredths of OD 
        /// (e.g. 150 corresponds with 1.5 OD)
        /// </description></item>
        /// </list>
        /// </remarks>
        public string EmptyImageDensity
        {
            get { return this.Get(DicomTag.EmptyImageDensity, "BLACK"); }
            set { this.Add(DicomTag.EmptyImageDensity, value); }
        }

        /// <summary>
        /// Specifies whether a trim box shall be printed surrounding each image on the film.
        /// </summary>
        /// <remarks>
        /// Enumerated Values:
        /// <list type="bullet">
        /// <item><description>YES</description></item>
        /// <item><description>NO</description></item>
        /// </list>
        /// </remarks>
        public string Trim
        {
            get { return this.Get(DicomTag.Trim, "NO"); }
            set { this.Add(DicomTag.Trim, value); }
        }

        /// <summary>
        /// Luminance of lightbox illuminating a piece of transmissive film, or for the case of reflective media, 
        /// luminance obtainable from diffuse reflection of the illumination present. Expressed as L0, in candelas
        /// per square meter (cd/m2).
        /// </summary>
        public ushort Illumination
        {
            get { return this.Get<ushort>(DicomTag.Illumination, 0); }
            set { this.Add(DicomTag.Illumination, value); }
        }

        /// <summary>
        /// For transmissive film, luminance contribution due to reflected ambient light. 
        /// Expressed as La, in candelas per square meter (cd/m2).
        /// </summary>
        public ushort ReflectedAmbientLight
        {
            get { return this.Get<ushort>(DicomTag.ReflectedAmbientLight, 0); }
            set { this.Add(DicomTag.ReflectedAmbientLight, value); }
        }

        /// <summary>
        /// Specifies the resolution at which images in this Film Box are to be printed.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="bullet">
        /// <item>
        ///   <term>STANDARD</term>
        ///   <description>approximately 4k x 5k printable pixels on a 14 x 17 inch film</description>
        /// </item>
        /// <item>
        ///   <term>HIGH</term>
        ///   <description>Approximately twice the resolution of STANDARD.</description>
        /// </item>
        /// </list>
        /// </remarks>
        public string RequestedResolutionID
        {
            get { return this.Get(DicomTag.RequestedResolutionID, "STANDARD"); }
            set { this.Add(DicomTag.RequestedResolutionID, value); }
        }

        public DicomSequence ReferencedPresentationLutSequence
        {
            get
            {
                return this.Get<DicomSequence>(DicomTag.ReferencedPresentationLUTSequence);
            }
            set
            {
                this.Add(value);
            }
        }

        public PresentationLut PresentationLut
        {
            get
            {
                if (ReferencedPresentationLutSequence != null && ReferencedPresentationLutSequence.Items.Count > 0)
                {
                    var sopInstanceUid = ReferencedPresentationLutSequence.Items[0].Get<DicomUID>(DicomTag.ReferencedSOPInstanceUID);
                    return _filmSession.FindPresentationLut(sopInstanceUid);
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Constructors and Initialization
        /// <summary>
        /// Create baisc film box for specified film session
        /// </summary>
        /// <param name="filmSession">Film session instance</param>
        /// <param name="sopInstance">Basic film box SOP Instance UID</param>
        public FilmBox(FilmSession filmSession, DicomUID sopInstance, DicomTransferSyntax transferSyntax)
            : base()
        {
            this.InternalTransferSyntax = transferSyntax;
            _filmSession = filmSession;
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


            BasicImageBoxes = new List<ImageBox>();
        }

        public FilmBox(FilmSession filmSession, DicomUID sopInstance, DicomDataset dataset)
            : this(filmSession, sopInstance, dataset.InternalTransferSyntax)
        {
            dataset.CopyTo(this);
            this.InternalTransferSyntax = dataset.InternalTransferSyntax;
        }

        /// <summary>
        /// Create basic film box clone for specfifed film box instance
        /// </summary>
        /// <param name="filmBox">Basic film box instance to clone</param>
        private FilmBox(FilmBox filmBox)
            : this(filmBox._filmSession.CloneFilmSession(), filmBox.SOPInstanceUID, filmBox)
        {
            foreach (var imageBox in filmBox.BasicImageBoxes)
            {
                BasicImageBoxes.Add(imageBox.Clone(this));
            }
        }

        /// <summary>
        /// Iniitalize the film box dataset attributes to defaults
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            //initialization
            this.Add(new DicomSequence(DicomTag.ReferencedImageBoxSequence));

            if (!this.Contains(DicomTag.FilmOrientation))
            {
                FilmOrienation = "PORTRAIT";
            }
            if (!this.Contains(DicomTag.FilmSizeID))
            {
                FilmSizeID = "A4";
            }
            if (!this.Contains(DicomTag.MagnificationType))
            {
                MagnificationType = "BILINEAR";
            }
            if (!this.Contains(DicomTag.MaxDensity))
            {
                MaxDensity = 0;
            }
            if (!this.Contains(DicomTag.MinDensity))
            {
                MinDensity = 0;
            }
            if (!this.Contains(DicomTag.BorderDensity))
            {
                BorderDensity = "BLACK";
            }
            if (!this.Contains(DicomTag.EmptyImageDensity))
            {
                EmptyImageDensity = "BLACK";
            }
            if (!this.Contains(DicomTag.Trim))
            {
                Trim = "NO";
            }
            if (!this.Contains(DicomTag.RequestedResolutionID))
            {
                RequestedResolutionID = "STANDARD";
            }

            try
            {
                if (string.IsNullOrEmpty(ImageDisplayFormat))
                {
                    //Log.Logger.Error("No display format present in N-CREATE Basic Film Box dataset");
                    return false;
                }

                //Core.Logger.Info("Applying display format {0} for film box {1}", ImageDisplayFormat, SOPInstanceUID);

                var parts = ImageDisplayFormat.Split('\\');

                if (parts[0] == "STANDARD" && parts.Length == 2)
                {
                    parts = parts[1].Split(',');
                    if (parts.Length == 2)
                    {
                        int row = int.Parse(parts[0]);
                        int col = int.Parse(parts[1]);

                        for (int r = 0; r < row; r++)
                        {
                            for (int c = 0; c < col; c++)
                            {
                                CreateImageBox();
                            }
                        }

                        return true;
                    }
                }
                else if ((parts[0] == "ROW" || parts[0] == "COL") && parts.Length == 2)
                {
                    parts = parts[1].Split(',');

                    foreach (var part in parts)
                    {
                        int count = int.Parse(part);
                        for (int i = 0; i < count; i++)
                        {
                            CreateImageBox();
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                //Core.Logger.Error("FilmBox.Initialize", ex);
            }
            //Core.Logger.Error("Unsupported image display format \"{0}\"", ImageDisplayFormat);
            return false;
        }

        /// <summary>
        /// Create clone of basic film box instanct
        /// </summary>
        /// <returns>The cloned basic film box instance</returns>
        public FilmBox Clone()
        {
            return new FilmBox(this);
        }

        #endregion

        #region ImageBox Manipulation

        /// <summary>
        /// Find specific basic image box using its SOP instance UID
        /// </summary>
        /// <param name="sopInstance">Image box SOP instance UID</param>
        /// <returns>The basic image box instance for specified SOP instance UID or null if no matches found</returns>
        public ImageBox FindImageBox(DicomUID sopInstance)
        {
            return BasicImageBoxes.FirstOrDefault(i => i.SOPInstanceUID.Equals(sopInstance));
        }

        private void CreateImageBox()
        {
            DicomUID classUid = DicomUID.BasicGrayscaleImageBoxSOPClass;

            if (_filmSession.IsColor)
            {
                classUid = DicomUID.BasicColorImageBoxSOPClass;
            }

            DicomUID sopInstance = new DicomUID(string.Format("{0}.{1}", SOPInstanceUID.UID, BasicImageBoxes.Count + 1), SOPInstanceUID.Name, SOPInstanceUID.Type);

            var imageBox = new ImageBox(this, classUid, sopInstance);
            imageBox.ImageBoxPosition = (ushort)(BasicImageBoxes.Count + 1);

            BasicImageBoxes.Add(imageBox);

            var item = new DicomDataset();
            item.Add(DicomTag.ReferencedSOPClassUID,classUid);
            item.Add(DicomTag.ReferencedSOPInstanceUID,sopInstance);

            var seq = Get<DicomSequence>(DicomTag.ReferencedImageBoxSequence);
            seq.Items.Add(item);
        }

        public bool IsColor()
        {
            return BasicImageBoxes.FirstOrDefault(i => i.SOPClassUID == ImageBox.ColorSOPClassUID) != null;
        }
        #endregion

        #region Printing Methods

        public SizeF GetSizeInInch()
        {
            const float CM_PER_INCH = 2.54f;
            var filmSizeId = FilmSizeID;
            if (filmSizeId.Contains("IN"))
            {
                var parts = filmSizeId.Split(new[] { "IN" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    var width = parts[0].Replace('_', '.');
                    var height = parts[1].TrimStart('X').Replace('_', '.');

                    return new SizeF(float.Parse(width), float.Parse(height));
                }
            }
            else if (filmSizeId.Contains("CM"))
            {
                var parts = filmSizeId.Split(new[] { "CM" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    var width = parts[0].Replace('_', '.');
                    var height = parts[1].TrimStart('X').Replace('_', '.');

                    return new SizeF(float.Parse(width) / CM_PER_INCH, float.Parse(height) / CM_PER_INCH);
                }
            }
            else if (filmSizeId == "A3")
            {
                return new SizeF(29.7f / CM_PER_INCH, 42.0f / CM_PER_INCH);
            }

            return new SizeF(210 / 2.54f, 297 / 2.54f);
        }

        public void Print(Graphics graphics, Rectangle marginBounds, int imageResolution)
        {
            var parts = this.ImageDisplayFormat.Split('\\', ',');

            if (parts.Length > 0)
            {
                RectangleF[] boxes = null;
                if (parts[0] == "STANDARD")
                {
                    boxes = PrintStandardFormat(parts, marginBounds);
                }
                else if (parts[0] == "ROW")
                {
                    boxes = PrintRowFormat(parts, marginBounds);
                }
                else if (parts[0] == "COL")
                {
                    boxes = PrintColumnFormat(parts, marginBounds);
                }
                else
                {
                    throw new ArgumentException(string.Format("ImageDisplayFormat {0} invalid", this.ImageDisplayFormat),
                        "ImageDisplayFormat");
                }

                for (int i = 0; i < BasicImageBoxes.Count; i++)
                {
                    BasicImageBoxes[i].Print(graphics, boxes[i], imageResolution);
                }
            }
        }

        protected RectangleF[] PrintColumnFormat(string[] parts, RectangleF marginBounds)
        {
            if (parts.Length >= 2)
            {
                int colsCount = parts.Length - 1;

                SizeF boxSize = new SizeF(marginBounds.Width / (float)colsCount, 0);

                var boxes = new List<RectangleF>();

                for (int c = 0; c < colsCount; c++)
                {
                    int rowsCount = int.Parse(parts[c + 1]);

                    boxSize.Height = marginBounds.Height / (float)rowsCount;

                    for (int r = 0; r < rowsCount; r++)
                    {
                        boxes.Add(new RectangleF
                        {
                            X = marginBounds.X + c * boxSize.Width,
                            Y = marginBounds.Y + r * boxSize.Height,
                            Width = boxSize.Width,
                            Height = boxSize.Height
                        });
                    }
                }
                return boxes.ToArray();
            }
            else
            {
                throw new ArgumentException(string.Format("ImageDisplayFormat {0} invalid", this.ImageDisplayFormat),
                    "ImageDisplayFormat");
            }
        }

        protected RectangleF[] PrintRowFormat(string[] parts, RectangleF marginBounds)
        {
            if (parts.Length >= 2)
            {
                int rowsCount = parts.Length - 1;

                SizeF boxSize = new SizeF(0, marginBounds.Height / (float)rowsCount);

                var boxes = new List<RectangleF>();

                for (int r = 0; r < rowsCount; r++)
                {
                    int colsCount = int.Parse(parts[r + 1]);

                    boxSize.Width = marginBounds.Width / (float)colsCount;

                    for (int c = 0; c < colsCount; c++)
                    {
                        boxes.Add(new RectangleF
                        {
                            X = marginBounds.X + c * boxSize.Width,
                            Y = marginBounds.Y + r * boxSize.Height,
                            Width = boxSize.Width,
                            Height = boxSize.Height
                        });
                    }
                }
                return boxes.ToArray();
            }
            else
            {
                throw new ArgumentException(string.Format("ImageDisplayFormat {0} invalid", this.ImageDisplayFormat),
                    "ImageDisplayFormat");
            }
        }

        protected RectangleF[] PrintStandardFormat(string[] parts, RectangleF marginBounds)
        {
            if (parts.Length >= 3)
            {
                int columns = int.Parse(parts[1]);
                int rows = int.Parse(parts[2]);

                SizeF boxSize = new SizeF(marginBounds.Width / (float)columns, marginBounds.Height / (float)rows);


                var boxes = new List<RectangleF>();
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < columns; c++)
                    {

                        boxes.Add(new RectangleF
                        {
                            X = marginBounds.X + c * boxSize.Width,
                            Y = marginBounds.Y + r * boxSize.Height,
                            Width = boxSize.Width,
                            Height = boxSize.Height
                        });
                    }
                }

                return boxes.ToArray();
            }
            else
            {
                throw new ArgumentException(string.Format("ImageDisplayFormat {0} invalid", this.ImageDisplayFormat),
                    "ImageDisplayFormat");
            }
        }

        #endregion


        #region Load and Save

        public void Save(string filmBoxFolder)
        {
            var filmBoxDicomFile = string.Format(@"{0}\FilmBox.dcm", filmBoxFolder);
            var filmBoxTextFile = string.Format(@"{0}\FilmBox.txt", filmBoxFolder);
            var file = new DicomFile(this);
            file.Save(filmBoxDicomFile);


            System.IO.File.WriteAllText(filmBoxTextFile, this.WriteToString());

            var imageBoxFolderInfo = new System.IO.DirectoryInfo(string.Format(@"{0}\Images", filmBoxFolder));
            imageBoxFolderInfo.Create();
            for (int i = 0; i < this.BasicImageBoxes.Count; i++)
            {
                var imageBox = this.BasicImageBoxes[i];
                imageBox.Save(string.Format(@"{0}\I{1:000000}", imageBoxFolderInfo.FullName, i + 1));
            }
        }
        public static FilmBox Load(FilmSession filmSession, string filmBoxFolder)
        {
            var filmBoxFile = string.Format(@"{0}\FilmBox.dcm", filmBoxFolder);

            var file = DicomFile.Open(filmBoxFile);
            

            var filmBox = new FilmBox(filmSession, file.FileMetaInfo.MediaStorageSOPInstanceUID, file.Dataset);

            var imagesFolder = new System.IO.DirectoryInfo(string.Format(@"{0}\Images", filmBoxFolder));
            foreach (var image in imagesFolder.EnumerateFiles("*.dcm"))
            {
                var imageBox = ImageBox.Load(filmBox, image.FullName);

                filmBox.BasicImageBoxes.Add(imageBox);
            }
            return filmBox;
        }

        #endregion
    }
}
