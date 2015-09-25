// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Reader
{
    using System;
    using System.Text;

    /// <summary>
    /// Class for reading DICOM file objects.
    /// </summary>
    public class DicomFileReader
    {
        #region FIELDS

        private static readonly DicomTag FileMetaInfoStopTag = new DicomTag(0x0002, 0xffff);

        private DicomFileFormat fileFormat;

        private DicomTransferSyntax syntax;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="DicomFileReader"/>.
        /// </summary>
        public DicomFileReader()
        {
            this.fileFormat = DicomFileFormat.Unknown;
            this.syntax = DicomTransferSyntax.ExplicitVRLittleEndian;
        }

        #endregion

        #region PROPERTIES

        [Obsolete]
        public IByteSource Source
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets file format of latest read.
        /// </summary>
        public DicomFileFormat FileFormat
        {
            get
            {
                return this.fileFormat;
            }
        }

        /// <summary>
        /// Gets the transfer syntax of latest read.
        /// </summary>
        public DicomTransferSyntax Syntax
        {
            get
            {
                return this.syntax;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Read DICOM file object.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="fileMetaInfo">Reader observer for file meta information.</param>
        /// <param name="dataset">Reader observer for dataset.</param>
        /// <returns>Reader result.</returns>
        public DicomReaderResult Read(
            IByteSource source,
            IDicomReaderObserver fileMetaInfo,
            IDicomReaderObserver dataset)
        {
            return DoParse(source, fileMetaInfo, dataset, out this.fileFormat, out this.syntax);
        }

        private static DicomReaderResult DoParse(
            IByteSource source,
            IDicomReaderObserver fileMetasetInfoObserver,
            IDicomReaderObserver datasetObserver,
            out DicomFileFormat fileFormat,
            out DicomTransferSyntax syntax)
        {
            fileFormat = DicomFileFormat.Unknown;
            syntax = DicomTransferSyntax.ExplicitVRLittleEndian;

            if (!source.Require(132)) return DicomReaderResult.Error;

            var reader = new DicomReader();

            // mark file origin
            source.Mark();

            // test for DICM preamble
            source.Skip(128);
            if (source.GetUInt8() == 'D' && source.GetUInt8() == 'I' && source.GetUInt8() == 'C'
                && source.GetUInt8() == 'M') fileFormat = DicomFileFormat.DICOM3;

            // test for incorrect syntax in file meta info
            do
            {
                if (fileFormat == DicomFileFormat.DICOM3)
                {
                    // move milestone to after preamble
                    source.Mark();
                }
                else
                {
                    // rewind to origin milestone
                    source.Rewind();
                }

                // test for file meta info
                var group = source.GetUInt16();

                if (group > 0x00ff)
                {
                    source.Endian = Endian.Big;
                    syntax = DicomTransferSyntax.ExplicitVRBigEndian;

                    group = Endian.Swap(group);
                }

                if (group > 0x00ff)
                {
                    // invalid starting tag
                    fileFormat = DicomFileFormat.Unknown;
                    source.Rewind();
                    break;
                }

                if (fileFormat == DicomFileFormat.Unknown)
                {
                    fileFormat = @group == 0x0002
                                     ? DicomFileFormat.DICOM3NoPreamble
                                     : DicomFileFormat.DICOM3NoFileMetaInfo;
                }

                var element = source.GetUInt16();
                var tag = new DicomTag(group, element);

                // test for explicit VR
                var vrt = Encoding.UTF8.GetBytes(tag.DictionaryEntry.ValueRepresentations[0].Code);
                var vrs = source.GetBytes(2);

                if (vrt[0] != vrs[0] || vrt[1] != vrs[1])
                {
                    // implicit VR
                    syntax = syntax.Endian == Endian.Little
                                 ? DicomTransferSyntax.ImplicitVRLittleEndian
                                 : DicomTransferSyntax.ImplicitVRBigEndian;
                }

                source.Rewind();
            }
            while (fileFormat == DicomFileFormat.Unknown);

            if (fileFormat == DicomFileFormat.Unknown)
            {
                throw new DicomReaderException("Attempted to read invalid DICOM file");
            }

            string code = null, uid = null;
            var obs = new DicomReaderCallbackObserver();
            if (fileFormat != DicomFileFormat.DICOM3)
            {
                obs.Add(
                    DicomTag.RecognitionCodeRETIRED,
                    (sender, ea) =>
                        {
                            try
                            {
                                code = Encoding.UTF8.GetString(ea.Data.Data, 0, ea.Data.Data.Length);
                            }
                            catch
                            {
                            }
                        });
            }
            obs.Add(
                DicomTag.TransferSyntaxUID,
                (sender, ea) =>
                    {
                        try
                        {
                            uid = Encoding.UTF8.GetString(ea.Data.Data, 0, ea.Data.Data.Length);
                        }
                        catch
                        {
                        }
                    });

            source.Endian = syntax.Endian;
            reader.IsExplicitVR = syntax.IsExplicitVR;

            DicomReaderResult result;
            if (fileFormat == DicomFileFormat.DICOM3NoFileMetaInfo)
            {
                result = reader.Read(source, new DicomReaderMultiObserver(obs, datasetObserver));
                UpdateFileFormatAndSyntax(code, uid, ref fileFormat, ref syntax);
            }
            else
            {
                if (reader.Read(source, new DicomReaderMultiObserver(obs, fileMetasetInfoObserver), FileMetaInfoStopTag)
                    != DicomReaderResult.Stopped)
                {
                    throw new DicomReaderException("DICOM File Meta Info ended prematurely");
                }

                UpdateFileFormatAndSyntax(code, uid, ref fileFormat, ref syntax);

                // rewind to last marker (start of previous tag)... ugly because 
                // it requires knowledge of how the parser is implemented
                source.Rewind();

                source.Endian = syntax.Endian;
                reader.IsExplicitVR = syntax.IsExplicitVR;
                result = reader.Read(source, datasetObserver);
            }

            return result;
        }

        private static void UpdateFileFormatAndSyntax(
            string code,
            string uid,
            ref DicomFileFormat fileFormat,
            ref DicomTransferSyntax syntax)
        {
            if (code != null)
            {
                if (code == "ACR-NEMA 1.0")
                {
                    fileFormat = DicomFileFormat.ACRNEMA1;
                }
                else if (code == "ACR-NEMA 2.0")
                {
                    fileFormat = DicomFileFormat.ACRNEMA2;
                }
            }
            if (uid != null)
            {
                syntax = DicomTransferSyntax.Parse(uid);
            }
        }

        #endregion
    }
}
