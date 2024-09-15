// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Printing
{

    /// <summary>
    /// Basic film box
    /// </summary>
    public class FilmBox : DicomDataset
    {
        #region Properties and Attributes

        private static ILogger _logger;
        private static ILogger Logger => _logger ??= Setup.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(Log.LogCategories.Printing);

        private readonly FilmSession _filmSession = null;

        public FilmSession FilmSession
        {
            get
            {
                return _filmSession;
            }
        }

        /// <summary>
        /// Basic film box SOP class UID
        /// </summary>
        public static readonly DicomUID SOPClassUID = DicomUID.BasicFilmBox;

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
            get => GetSingleValue<string>(DicomTag.ImageDisplayFormat);
            set => AddOrUpdate(DicomTag.ImageDisplayFormat, value);
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
        public string FilmOrientation
        {
            get => GetSingleValueOrDefault(DicomTag.FilmOrientation, "PORTRAIT");
            set => AddOrUpdate(DicomTag.FilmOrientation, value);
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
            get => GetSingleValueOrDefault(DicomTag.FilmSizeID, "A4");
            set => AddOrUpdate(DicomTag.FilmSizeID, value);
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
            get => GetSingleValueOrDefault(DicomTag.MagnificationType, "BILINEAR");
            set => AddOrUpdate(DicomTag.MagnificationType, value);
        }

        /// <summary>
        /// Maximum density of the images on the film, expressed in hundredths of OD. If Max Density is higher than 
        /// maximum printer density than Max Density is set to maximum printer density.
        /// </summary>
        public ushort MaxDensity
        {
            get => GetSingleValueOrDefault(DicomTag.MaxDensity, (ushort)0);
            set => AddOrUpdate(DicomTag.MaxDensity, value);
        }


        /// <summary>
        /// Minimum density of the images on the film, expressed in hundredths of OD. If Min Density is lower than 
        /// minimum printer density than Min Density is set to minimum printer density.
        /// </summary>
        public ushort MinDensity
        {
            get => GetSingleValueOrDefault(DicomTag.MinDensity, (ushort)0);
            set => AddOrUpdate(DicomTag.MinDensity, value);
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
            get => GetSingleValueOrDefault(DicomTag.ConfigurationInformation, string.Empty);
            set => AddOrUpdate(DicomTag.ConfigurationInformation, value);
        }

        /// <summary>
        /// Identification of annotation display format. The definition of the annotation 
        /// display formats and the annotation box position sequence are defined in the Conformance 
        /// Statement.
        /// </summary>
        public string AnnotationDisplayFormatID
        {
            get => GetSingleValueOrDefault(DicomTag.AnnotationDisplayFormatID, string.Empty);
            set => AddOrUpdate(DicomTag.AnnotationDisplayFormatID, value);
        }

        /// <summary>
        /// Further specifies the type of the interpolation function. Values are defined in Conformance Statement.
        /// Only valid for Magnification Type (2010,0060) = CUBIC
        /// </summary>
        public string SmoothingType
        {
            get => GetSingleValueOrDefault(DicomTag.SmoothingType, string.Empty);
            set => AddOrUpdate(DicomTag.SmoothingType, value);
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
            get => GetSingleValueOrDefault(DicomTag.BorderDensity, "BLACK");
            set => AddOrUpdate(DicomTag.BorderDensity, value);
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
            get => GetSingleValueOrDefault(DicomTag.EmptyImageDensity, "BLACK");
            set => AddOrUpdate(DicomTag.EmptyImageDensity, value);
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
            get => GetSingleValueOrDefault(DicomTag.Trim, "NO");
            set => AddOrUpdate(DicomTag.Trim, value);
        }

        /// <summary>
        /// Luminance of lightbox illuminating a piece of transmissive film, or for the case of reflective media, 
        /// luminance obtainable from diffuse reflection of the illumination present. Expressed as L0, in candelas
        /// per square meter (cd/m2).
        /// </summary>
        public ushort Illumination
        {
            get => GetSingleValueOrDefault(DicomTag.Illumination, (ushort)0);
            set => AddOrUpdate(DicomTag.Illumination, value);
        }

        /// <summary>
        /// For transmissive film, luminance contribution due to reflected ambient light. 
        /// Expressed as La, in candelas per square meter (cd/m2).
        /// </summary>
        public ushort ReflectedAmbientLight
        {
            get => GetSingleValueOrDefault(DicomTag.ReflectedAmbientLight, (ushort)0);
            set => AddOrUpdate(DicomTag.ReflectedAmbientLight, value);
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
            get => GetSingleValueOrDefault(DicomTag.RequestedResolutionID, "STANDARD");
            set => AddOrUpdate(DicomTag.RequestedResolutionID, value);
        }

        public DicomSequence ReferencedPresentationLutSequence
        {
            get => TryGetSequence(DicomTag.ReferencedPresentationLUTSequence, out DicomSequence dummy) ? dummy : null;
            set
            {
                if (value.Tag.Equals(DicomTag.ReferencedPresentationLUTSequence))
                {
                    throw new InvalidOperationException(
                        $"Added sequence must be Referenced Presentation LUT Sequence, is: {value.Tag}");
                }
                AddOrUpdate(value);
            }
        }

        public PresentationLut PresentationLut
        {
            get
            {
                if (ReferencedPresentationLutSequence?.Items.Count > 0)
                {
                    var sopInstanceUid =
                        ReferencedPresentationLutSequence.Items[0].GetSingleValue<DicomUID>(DicomTag.ReferencedSOPInstanceUID);
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
        /// <param name="transferSyntax">Requested internal transfer syntax.</param>
        public FilmBox(FilmSession filmSession, DicomUID sopInstance, DicomTransferSyntax transferSyntax)
        {
            InternalTransferSyntax = transferSyntax;
            _filmSession = filmSession;
            if (string.IsNullOrEmpty(sopInstance?.UID))
            {
                SOPInstanceUID = DicomUID.Generate();
            }
            else
            {
                SOPInstanceUID = sopInstance;
            }
            Add(DicomTag.SOPClassUID, SOPClassUID);
            Add(DicomTag.SOPInstanceUID, SOPInstanceUID);
            Add(DicomTag.ReferencedFilmSessionSequence, new DicomDataset {
                new DicomUniqueIdentifier(DicomTag.ReferencedSOPClassUID, filmSession.SOPClassUID),
                new DicomUniqueIdentifier(DicomTag.ReferencedSOPInstanceUID, filmSession.SOPInstanceUID)
            });

            BasicImageBoxes = new List<ImageBox>();
        }

        public FilmBox(FilmSession filmSession, DicomUID sopInstance, DicomDataset dataset)
            : this(filmSession, sopInstance, dataset.InternalTransferSyntax)
        {
            dataset.CopyTo(this);
            InternalTransferSyntax = dataset.InternalTransferSyntax;
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
        /// Initalize the film box dataset attributes to defaults
        /// </summary>
        /// <returns>True if film box attributes could be initialized, false otherwise.</returns>
        public bool Initialize()
        {
            //initialization
            AddOrUpdate(new DicomSequence(DicomTag.ReferencedImageBoxSequence));

            if (!Contains(DicomTag.FilmOrientation))
            {
                FilmOrientation = "PORTRAIT";
            }
            if (!Contains(DicomTag.FilmSizeID))
            {
                FilmSizeID = "A4";
            }
            if (!Contains(DicomTag.MagnificationType))
            {
                MagnificationType = "BILINEAR";
            }
            if (!Contains(DicomTag.MaxDensity))
            {
                MaxDensity = 0;
            }
            if (!Contains(DicomTag.MinDensity))
            {
                MinDensity = 0;
            }
            if (!Contains(DicomTag.BorderDensity))
            {
                BorderDensity = "BLACK";
            }
            if (!Contains(DicomTag.EmptyImageDensity))
            {
                EmptyImageDensity = "BLACK";
            }
            if (!Contains(DicomTag.Trim))
            {
                Trim = "NO";
            }
            if (!Contains(DicomTag.RequestedResolutionID))
            {
                RequestedResolutionID = "STANDARD";
            }

            try
            {
                if (string.IsNullOrEmpty(ImageDisplayFormat))
                {
                    Logger.LogError("No display format present in N-CREATE Basic Film Box dataset");
                    return false;
                }

                Logger.LogInformation($"Applying display format {ImageDisplayFormat} for film box {SOPInstanceUID}");

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
                Logger.LogError("FilmBox.Initialize, exception message: {0}", ex.Message);
            }

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

        /// <summary>
        /// Create image box from DICOM data.
        /// </summary>
        private void CreateImageBox()
        {
            DicomUID classUid = DicomUID.BasicGrayscaleImageBox;

            if (_filmSession.IsColor)
            {
                classUid = DicomUID.BasicColorImageBox;
            }

            var sopInstance = new DicomUID(
                string.Format("{0}.{1}", SOPInstanceUID.UID, BasicImageBoxes.Count + 1),
                SOPInstanceUID.Name,
                SOPInstanceUID.Type);

            var imageBox = new ImageBox(this, classUid, sopInstance)
            {
                ImageBoxPosition = (ushort)(BasicImageBoxes.Count + 1)
            };

            BasicImageBoxes.Add(imageBox);

            var item = new DicomDataset
            {
                { DicomTag.ReferencedSOPClassUID, classUid },
                { DicomTag.ReferencedSOPInstanceUID, sopInstance }
            };

            var seq = GetSequence(DicomTag.ReferencedImageBoxSequence);
            seq.Items.Add(item);
        }

        /// <summary>
        /// Gets whether one or more image boxes is colored.
        /// </summary>
        /// <returns></returns>
        public bool IsColor()
        {
            return BasicImageBoxes.Any(i => i.SOPClassUID == ImageBox.ColorSOPClassUID);
        }

        #endregion

        #region Printing Methods

        /// <summary>
        /// Generate rectangles arranged in column format.
        /// </summary>
        /// <param name="parts">Display format data.</param>
        /// <param name="marginBounds">Margin bounds.</param>
        /// <returns>Rectangles arranged in column format.</returns>
        public static RectF[] PrintColumnFormat(string[] parts, RectF marginBounds)
        {
            if (parts.Length >= 2)
            {
                int colsCount = parts.Length - 1;

                var boxWidth = marginBounds.Width / colsCount;

                var boxes = new List<RectF>();

                for (int c = 0; c < colsCount; c++)
                {
                    int rowsCount = int.Parse(parts[c + 1]);

                    var boxHeight = marginBounds.Height / rowsCount;

                    for (int r = 0; r < rowsCount; r++)
                    {
                        boxes.Add(
                            new RectF
                            {
                                X = marginBounds.X + c * boxWidth,
                                Y = marginBounds.Y + r * boxHeight,
                                Width = boxWidth,
                                Height = boxHeight
                            });
                    }
                }
                return boxes.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Generate rectangles arranged in row format.
        /// </summary>
        /// <param name="parts">Display format data.</param>
        /// <param name="marginBounds">Margin bounds.</param>
        /// <returns>Rectangles arranged in row format.</returns>
        public static RectF[] PrintRowFormat(string[] parts, RectF marginBounds)
        {
            if (parts.Length >= 2)
            {
                int rowsCount = parts.Length - 1;

                var boxHeight = marginBounds.Height / rowsCount;

                var boxes = new List<RectF>();

                for (int r = 0; r < rowsCount; r++)
                {
                    int colsCount = int.Parse(parts[r + 1]);

                    var boxWidth = marginBounds.Width / colsCount;

                    for (int c = 0; c < colsCount; c++)
                    {
                        boxes.Add(
                            new RectF
                            {
                                X = marginBounds.X + c * boxWidth,
                                Y = marginBounds.Y + r * boxHeight,
                                Width = boxWidth,
                                Height = boxHeight
                            });
                    }
                }
                return boxes.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Generate rectangles arranged in standard format.
        /// </summary>
        /// <param name="parts">Display format data.</param>
        /// <param name="marginBounds">Margin bounds.</param>
        /// <returns>Rectangles arranged in standard format.</returns>
        public static RectF[] PrintStandardFormat(string[] parts, RectF marginBounds)
        {
            if (parts.Length >= 3)
            {
                int columns = int.Parse(parts[1]);
                int rows = int.Parse(parts[2]);

                var boxWidth = marginBounds.Width / columns;
                var boxHeight = marginBounds.Height / rows;

                var boxes = new List<RectF>();
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < columns; c++)
                    {

                        boxes.Add(
                            new RectF
                            {
                                X = marginBounds.X + c * boxWidth,
                                Y = marginBounds.Y + r * boxHeight,
                                Width = boxWidth,
                                Height = boxHeight
                            });
                    }
                }

                return boxes.ToArray();
            }

            return null;
        }

        #endregion


        #region Load and Save

        /// <summary>
        /// Save the contents of the film box.
        /// </summary>
        /// <param name="filmBoxFolder">Folder in which to save the film box contents.</param>
        public void Save(string filmBoxFolder)
        {
            var filmBoxDicomFile = Path.Combine(filmBoxFolder, "FilmBox.dcm");
            var filmBoxTextFile = Path.Combine(filmBoxFolder, "FilmBox.txt");
            var file = new DicomFile(this);
            file.Save(filmBoxDicomFile);

            var filmBoxFile = Setup.ServiceProvider.GetService<IFileReferenceFactory>().Create(filmBoxTextFile);
            using var writer = new StreamWriter(filmBoxFile.Create());
            writer.Write(Log.Extensions.WriteToString(this));

            var imageBoxFolderInfo = new DirectoryReference(Path.Combine(filmBoxFolder, "Images"));
            imageBoxFolderInfo.Create();
            for (int i = 0; i < BasicImageBoxes.Count; i++)
            {
                var imageBox = BasicImageBoxes[i];
                imageBox.Save(Path.Combine(imageBoxFolderInfo.Name, string.Format(@"I{0:000000}", i + 1)));
            }
        }

        /// <summary>
        /// Load a film box for a specific session from a specific folder.
        /// </summary>
        /// <param name="filmSession">Film session.</param>
        /// <param name="filmBoxFolder">Folder in which film box is stored.</param>
        /// <returns>Film box for the specified <paramref name="filmSession"/> located in the <paramref name="filmBoxFolder"/>.</returns>
        public static FilmBox Load(FilmSession filmSession, string filmBoxFolder)
        {
            var filmBoxFile = Path.Combine(filmBoxFolder, "FilmBox.dcm");

            var file = DicomFile.Open(filmBoxFile);

            var filmBox = new FilmBox(filmSession, file.FileMetaInfo.MediaStorageSOPInstanceUID, file.Dataset);

            var imagesFolder = new DirectoryReference(Path.Combine(filmBoxFolder, "Images"));
            foreach (var image in imagesFolder.EnumerateFileNames("*.dcm").OrderBy(i => i))
            {
                var imageBox = ImageBox.Load(filmBox, image);

                filmBox.BasicImageBoxes.Add(imageBox);
            }
            return filmBox;
        }

        #endregion
    }
}
