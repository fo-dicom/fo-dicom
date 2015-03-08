using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using Dicom.Log;

namespace Dicom.Printing
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
        /// Color or grayscal basic image sequence
        /// </summary>
        public DicomDataset ImageSequence
        {
            get
            {
                DicomSequence seq = null;
                if (SOPClassUID == ColorSOPClassUID)
                {
                    seq = this.Get<DicomSequence>(DicomTag.BasicColorImageSequence);
                }
                if (seq == null)
                {
                    seq = this.Get<DicomSequence>(DicomTag.BasicGrayscaleImageSequence);
                }

                if (seq != null && seq.Items.Count > 0)
                {
                    return seq.Items[0];
                }

                return null;
            }
            set
            {
                DicomSequence seq = null;
                if (SOPClassUID == ColorSOPClassUID)
                {
                    this.Add(DicomTag.BasicColorImageSequence, value);
                }
                else 
                {
                    this.Add(DicomTag.BasicGrayscaleImageSequence, value);

                }
            }
        }

        /// <summary>
        /// The position of the image on the film, based on image display format. See C.13.5.1 for specification.
        /// </summary>
        public ushort ImageBoxPosition
        {
            get { return this.Get<ushort>(DicomTag.ImageBoxPosition, 1); }
            set { this.Add(DicomTag.ImageBoxPosition, value); }
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
            get { return this.Get(DicomTag.Polarity, "NORMAL"); }
            set { this.Add(DicomTag.Polarity, value); }
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
            get { return this.Get(DicomTag.MagnificationType, FilmBox.MagnificationType); }
            set { this.Add(DicomTag.MagnificationType, value); }
        }

        /// <summary>
        /// Further specifies the type of the interpolation function. Values are defined in Conformance Statement.
        /// Only valid for Magnification Type (2010,0060) = CUBIC
        /// </summary>
        public string SmoothingType
        {
            get { return this.Get(DicomTag.SmoothingType, FilmBox.SmoothingType); }
            set { this.Add(DicomTag.SmoothingType, value); }
        }

        /// <summary>
        /// Maximum density of the images on the film, expressed in hundredths of OD. 
        /// If Max Density is higher than maximum printer density than Max Density is set to maximum printer density.
        /// </summary>
        public ushort MaxDensity
        {
            get { return this.Get<ushort>(DicomTag.MaxDensity, FilmBox.MaxDensity); }
            set { this.Add(DicomTag.MaxDensity, value); }
        }

        /// <summary>
        /// Minimum density of the images on the film, expressed in hundredths of OD. 
        /// If Min Density is lower than minimum printer density than Min Density is set to minimum printer density.
        /// </summary>
        public ushort MinDensity
        {
            get { return this.Get<ushort>(DicomTag.MinDensity, FilmBox.MinDensity); }
            set { this.Add(DicomTag.MinDensity, value); }
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
            get { return this.Get(DicomTag.ConfigurationInformation, FilmBox.ConfigurationInformation); }
            set { this.Add(DicomTag.ConfigurationInformation, value); }
        }

        /// <summary>
        /// Width (x-dimension) in mm of the image to be printed. This value overrides the size that corresponds with 
        /// optimal filling of the Image Box.
        /// </summary>
        public double RequestedImageSize
        {
            get { return this.Get<double>(DicomTag.RequestedImageSize, 0.0); }
            set { this.Add(DicomTag.RequestedImageSize, value); }
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
            get { return this.Get(DicomTag.RequestedDecimateCropBehavior, "DECIMATE"); }
            set { this.Add(DicomTag.RequestedDecimateCropBehavior, value); }
        }

        #endregion

        #region Constructors and Initialization
        /// <summary>
        /// Construct new ImageBox for specified filmBox using specified SOP class UID and SOP instance UID
        /// </summary>
        /// <param name="filmBox"></param>
        /// <param name="sopClass"></param>
        /// <param name="sopInstance"></param>
        public ImageBox(FilmBox filmBox, DicomUID sopClass, DicomUID sopInstance)
            : base()
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
        /// Construct new ImageBox cloned from another imagebox
        /// </summary>
        /// <param name="imageBox">The source ImageBox instance to clone</param>
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

        #region Printing

        public void Print(Graphics graphics, RectangleF box, int imageResolution)
        {
            var state = graphics.Save();

            FillBox(box, graphics);

            var imageBox = box;
            if (FilmBox.Trim == "YES")
            {
                imageBox.Inflate(-BORDER, -BORDER);
            }

            if (ImageSequence != null && ImageSequence.Contains(DicomTag.PixelData))
            {
                Image bitmap = null;
                try
                {
                var image = new Dicom.Imaging.DicomImage(ImageSequence);
                    var frame = image.RenderImage(0);

                    bitmap = frame;// new Bitmap(frame);
                    //frame.Dispose();

                    DrawBitmap(graphics, box, bitmap, imageResolution);
                }
                finally
                {
                    if (bitmap != null)
                    {
                bitmap.Dispose();
            }
                }
            }

            graphics.Restore(state);
        }

        private void FillBox(RectangleF box, Graphics graphics)
        {
            if (FilmBox.EmptyImageDensity == "BLACK")
            {
                RectangleF fillBox = box;
                if (FilmBox.BorderDensity == "WHITE" && FilmBox.Trim == "YES")
                {
                    fillBox.Inflate(-BORDER, -BORDER);
                }
                using (var brush = new SolidBrush(Color.Black))
                {
                    graphics.FillRectangle(brush, fillBox);
                }
            }
        }

        private void DrawBitmap(Graphics graphics, RectangleF box, Image bitmap, int imageResolution)
        {
            var imageSizeInInch = new SizeF(100 * bitmap.Width / imageResolution, 100 * bitmap.Height / imageResolution);
            double factor = Math.Min(box.Height / imageSizeInInch.Height, box.Width / imageSizeInInch.Width);

            if (factor > 1)
            {
                var targetSize = new Size
            {
                    Width = (int)(imageResolution * box.Width / 100),
                    Height = (int)(imageResolution * box.Height / 100)
            };


                using (var membmp = new Bitmap(targetSize.Width, targetSize.Height))
                {
                    membmp.SetResolution(imageResolution, imageResolution);

                    using (var memg = Graphics.FromImage(membmp))
                    {

                        memg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                        memg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        if (FilmBox.EmptyImageDensity == "BLACK")
                        {
                            using (var brush = new SolidBrush(Color.Black))
                            {
                                memg.FillRectangle(brush, 0, 0, targetSize.Width, targetSize.Height);
                            }
                        }

                        factor = Math.Min(targetSize.Height / (double)bitmap.Height,
                            targetSize.Width / (double)bitmap.Width);

            RectangleF srcRect = new RectangleF(0, 0, bitmap.Width, bitmap.Height);
            RectangleF dstRect = new RectangleF
            {
                X = (float)((targetSize.Width - bitmap.Width * factor) / 2.0f),
                Y = (float)((targetSize.Height - bitmap.Height * factor) / 2.0f),
                Width = (float)(bitmap.Width * factor),
                Height = (float)(bitmap.Height * factor),
            };
                        memg.DrawImage(bitmap, dstRect, srcRect, GraphicsUnit.Pixel);
                    }
                    graphics.DrawImage(membmp, box.X, box.Y, box.Width, box.Height);
                }
            }
            else
            {
                //var bmp = new Bitmap(bitmap);
                //bmp.SetResolution(imageResolution, imageResolution);
                RectangleF dstRect = new RectangleF
                {
                    X = box.X + (float)(box.Width - imageSizeInInch.Width * factor) / 2.0f,
                    Y = box.Y + (float)(box.Height - imageSizeInInch.Height * factor) / 2.0f,
                    Width = (float)(imageSizeInInch.Width * factor),
                    Height = (float)(imageSizeInInch.Height * factor),
                };

                graphics.DrawImage(bitmap, dstRect);
                //bmp.Dispose();
            }

        }
        #endregion

        #region Load and Save

        public static ImageBox Load(FilmBox filmBox, string imageBoxFile)
        {
            var file = DicomFile.Open(imageBoxFile);

            var imageBox = new ImageBox(filmBox, file.FileMetaInfo.MediaStorageSOPClassUID, file.FileMetaInfo.MediaStorageSOPInstanceUID);

            file.Dataset.CopyTo(imageBox);
            return imageBox;
        }

        public void Save(string imageBoxFile)
        {
            var imageBoxDicomFile = imageBoxFile + ".dcm";
            var imageBoxTextFile = imageBoxFile + ".txt";


            System.IO.File.WriteAllText(imageBoxTextFile, this.WriteToString());
            var file = new DicomFile(this);
            file.Save(imageBoxDicomFile);
        }

        #endregion
    }
}
