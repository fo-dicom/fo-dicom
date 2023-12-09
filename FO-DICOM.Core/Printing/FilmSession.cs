// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Printing
{
    /// <summary>
    /// Basic film session
    /// </summary>
    public class FilmSession : DicomDataset
    {
        #region Properties and Attributes

        /// <summary>
        /// Basic film session SOP class UID
        /// </summary>
        public DicomUID SOPClassUID { get; private set; }

        /// <summary>
        /// Basic film session SOP instance uID
        /// </summary>
        public DicomUID SOPInstanceUID { get; private set; }

        /// <summary>
        /// Film destination.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="bullet">
        /// <item>
        ///   <term>MAGAZINE</term>
        ///   <description>the exposed film is stored in film magazine</description>
        /// </item>
        /// <item>
        ///   <term>PROCESSOR</term>
        ///   <description>the exposed film is developed in film processor</description>
        /// </item>
        /// <item>
        ///   <term>BIN_i</term>
        ///   <description>
        ///   The exposed film is deposited in a sorter bin where “I” represents the bin 
        ///   number. Film sorter BINs shall be numbered sequentially starting from one and no maxium 
        ///   is placed on the number of BINs. The encoding of the BIN number shall not contain leading
        ///   zeros.
        ///   </description>
        /// </item>
        /// </list>
        /// </remarks>
        public string FilmDestination
        {
            get => GetSingleValueOrDefault(DicomTag.FilmDestination, string.Empty);
            set => AddOrUpdate(DicomTag.FilmDestination, value);
        }

        public string FilmSessionLabel
        {
            get => GetSingleValueOrDefault(DicomTag.FilmSessionLabel, string.Empty);
            set => AddOrUpdate(DicomTag.FilmSessionLabel, value);
        }

        /// <summary>
        /// Human readable label that identifies the film session.
        /// </summary>
        public int MemoryAllocation
        {
            get => GetSingleValueOrDefault(DicomTag.MemoryAllocation, 0);
            set => AddOrUpdate(DicomTag.MemoryAllocation, value);
        }

        /// <summary>
        /// Type of medium on which the print job will be printed.
        /// </summary>
        /// <remarks>
        /// Defined Terms:
        /// <list type="bullet">
        /// <item><description>PAPER</description></item>
        /// <item><description>CLEAR FILM</description></item>
        /// <item><description>BLUE FILM</description></item>
        /// <item><description>MAMMO CLEAR FILM</description></item>
        /// <item><description>MAMMO BLUE FILM</description></item>
        /// </list>
        /// </remarks>
        public string MediumType
        {
            get => GetSingleValueOrDefault(DicomTag.MediumType, string.Empty);
            set => AddOrUpdate(DicomTag.MediumType, value);
        }

        /// <summary>
        /// Specifies the priority of the print job.
        /// </summary>
        /// <remarks>
        /// Enumerated values:
        /// <list type="bullet">
        /// <item><description>HIGH</description></item>
        /// <item><description>MED</description></item>
        /// <item><description>LOW</description></item>
        /// </list>
        /// </remarks>
        public string PrintPriority
        {
            get => GetSingleValueOrDefault(DicomTag.PrintPriority, string.Empty);
            set => AddOrUpdate(DicomTag.PrintPriority, value);
        }

        /// <summary>
        /// Number of copies to be printed for each film of the film session.
        /// </summary>
        public int NumberOfCopies
        {
            get => GetSingleValueOrDefault(DicomTag.NumberOfCopies, 1);
            set => AddOrUpdate(DicomTag.NumberOfCopies, value);
        }

        /// <summary>
        /// Basic Film Boxes list
        /// </summary>
        public IList<FilmBox> BasicFilmBoxes { get; private set; }


        public IList<PresentationLut> PresentationLuts { get; private set; }

        public bool IsColor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct new film session from scratch
        /// </summary>
        /// <param name="sopClassUID">Film session SOP Class UID</param>
        /// <param name="sopInstance">Film session SOP instance UID</param>
        /// <param name="isColor">Color images?</param>
        public FilmSession(DicomUID sopClassUID, DicomUID sopInstance = null, bool isColor = false)
        {
            InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            SOPClassUID = sopClassUID ?? throw new ArgumentNullException(nameof(sopClassUID));

            if (string.IsNullOrEmpty(sopInstance?.UID))
            {
                SOPInstanceUID = DicomUIDGenerator.GenerateDerivedFromUUID();
            }
            else
            {
                SOPInstanceUID = sopInstance;
            }

            Add(DicomTag.SOPClassUID, SOPClassUID);
            Add(DicomTag.SOPInstanceUID, SOPInstanceUID);

            BasicFilmBoxes = new List<FilmBox>();
            PresentationLuts = new List<PresentationLut>();

            IsColor = isColor;
        }

        /// <summary>
        /// Construct new film session for specified SOP instance UID
        /// </summary>
        /// <param name="sopClassUID">Film session SOP Class UID</param>
        /// <param name="sopInstance">Film session SOP instance UID</param>
        /// <param name="dataset">Film session dataset</param>
        /// <param name="isColor">Color images?</param>
        public FilmSession(DicomUID sopClassUID, DicomUID sopInstance, DicomDataset dataset, bool isColor = false)
            : this(sopClassUID, sopInstance, isColor)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException(nameof(dataset));
            }
            dataset.CopyTo(this);

            InternalTransferSyntax = dataset.InternalTransferSyntax;
        }

        #endregion

        #region Film Box Manipulation

        /// <summary>
        /// Create new basic film box and add it to the film session
        /// </summary>
        /// <param name="sopInstance">The new film box SOP instance UID</param>
        /// <param name="dataset">The new film box dataset</param>
        /// <returns>The created film box instance</returns>
        public FilmBox CreateFilmBox(DicomUID sopInstance, DicomDataset dataset)
        {
            DicomUID uid = sopInstance;
            if (string.IsNullOrEmpty(uid?.UID))
            {
                uid = DicomUID.Generate();
            }

            var filmBox = new FilmBox(this, uid, dataset);

            BasicFilmBoxes.Add(filmBox);

            return filmBox;
        }

        /// <summary>
        /// Delete film box with specified SOP instance UID
        /// </summary>
        /// <param name="sopInstance">Target film box SOP instance UID</param>
        public bool DeleteFilmBox(DicomUID sopInstance)
        {
            var filmBox = FindFilmBox(sopInstance);

            if (filmBox != null)
            {
                BasicFilmBoxes.Remove(filmBox);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find film box instance with specified SOP instance UID
        /// </summary>
        /// <param name="sopInstance">Target film box SOP instance UID</param>
        /// <returns>Target film box instance or null if no matching found</returns>
        public FilmBox FindFilmBox(DicomUID sopInstance)
        {
            return BasicFilmBoxes.FirstOrDefault(f => f.SOPInstanceUID.UID == sopInstance.UID);
        }

        /// <summary>
        /// Find image box instance with specified SOP instance UID
        /// </summary>
        /// <param name="sopInstnace">Target image box SOP instance UID</param>
        /// <returns>Target image box instance or null if no matching found</returns>
        public ImageBox FindImageBox(DicomUID sopInstnace)
        {
            foreach (var filmBox in BasicFilmBoxes)
            {
                var imageBox = filmBox.FindImageBox(sopInstnace);
                if (imageBox != null)
                {
                    return imageBox;
                }
            }
            return null;
        }

        public PresentationLut CreatePresentationLut(DicomUID sopInstance, DicomDataset dataset)
        {
            DicomUID uid = sopInstance;
            if (string.IsNullOrEmpty(uid?.UID))
            {
                uid = new DicomUID(
                    string.Format("{0}.{1}", SOPInstanceUID.UID, BasicFilmBoxes.Count + 1),
                    SOPInstanceUID.Name,
                    SOPInstanceUID.Type);
            }

            var presentationLut = new PresentationLut(uid, dataset);

            PresentationLuts.Add(presentationLut);

            return presentationLut;
        }

        public PresentationLut FindPresentationLut(DicomUID sopInstance)
        {
            return PresentationLuts.FirstOrDefault(p => p.SopInstanceUid == sopInstance);
        }

        public void DeletePresentationLut(DicomUID sopInstance)
        {
            var presentationLut = FindPresentationLut(sopInstance);

            if (presentationLut != null)
            {
                PresentationLuts.Remove(presentationLut);
            }
        }

        /// <summary>
        /// Create a cloned film session of this film session instance
        /// </summary>
        /// <returns>Cloned film session instance</returns>
        public FilmSession CloneFilmSession()
        {
            return new FilmSession(SOPClassUID, SOPInstanceUID, this, IsColor);
        }

        #endregion

        #region Load and Save

        public static FilmSession Load(string filmSessionFile)
        {
            var file = DicomFile.Open(filmSessionFile);

            var filmSession = new FilmSession(
                file.FileMetaInfo.MediaStorageSOPClassUID,
                file.FileMetaInfo.MediaStorageSOPInstanceUID,
                file.Dataset);
            return filmSession;
        }

        #endregion
    }
}
