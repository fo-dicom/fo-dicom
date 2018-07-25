// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Text;

#if !NET35
using System.Threading.Tasks;
#endif

namespace Dicom.IO.Reader
{

    /// <summary>
    /// Class for reading DICOM file objects.
    /// </summary>
    public class DicomFileReader
    {
        #region INNER TYPES

        private class ParseResult
        {
            #region CONSTRUCTORS

            public ParseResult(DicomReaderResult result, DicomFileFormat format, DicomTransferSyntax syntax)
            {
                Result = result;
                Format = format;
                Syntax = syntax;
            }

            #endregion

            #region PROPERTIES

            public DicomReaderResult Result { get; }

            public DicomFileFormat Format { get; }

            public DicomTransferSyntax Syntax { get; }

            #endregion
        }

        #endregion

        #region FIELDS

        private static readonly DicomTag _FileMetaInfoStopTag = new DicomTag(0x0002, 0xffff);

        private static readonly Func<ParseState, bool> _FileMetaInfoStopCriterion =
            state => state.Tag.CompareTo(_FileMetaInfoStopTag) >= 0;

        private readonly object _locker;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of <see cref="DicomFileReader"/>.
        /// </summary>
        public DicomFileReader()
        {
            FileFormat = DicomFileFormat.Unknown;
            Syntax = null;
            _locker = new object();
        }

      #endregion

      #region PROPERTIES

      /// <summary>
      /// Gets file format of latest read.
      /// </summary>
      public DicomFileFormat FileFormat { get; private set; }

      /// <summary>
      /// Gets the transfer syntax of latest read.
      /// </summary>
      public DicomTransferSyntax Syntax { get; private set; }

      #endregion

      #region METHODS

      /// <summary>
      /// Read DICOM file object.
      /// </summary>
      /// <param name="source">Byte source to read.</param>
      /// <param name="fileMetaInfo">Reader observer for file meta information.</param>
      /// <param name="dataset">Reader observer for dataset.</param>
      /// <param name="stop">Stop criterion in dataset.</param>
      /// <returns>Reader result.</returns>
      public DicomReaderResult Read(
            IByteSource source,
            IDicomReaderObserver fileMetaInfo,
            IDicomReaderObserver dataset,
            Func<ParseState, bool> stop = null)
        {
            var parse = Parse(source, fileMetaInfo, dataset, stop);
            lock (_locker)
            {
                FileFormat = parse.Format;
                Syntax = parse.Syntax;
            }

            return parse.Result;
        }

#if !NET35
        /// <summary>
        /// Asynchronously read DICOM file object.
        /// </summary>
        /// <param name="source">Byte source to read.</param>
        /// <param name="fileMetaInfo">Reader observer for file meta information.</param>
        /// <param name="dataset">Reader observer for dataset.</param>
        /// <param name="stop">Stop criterion in dataset.</param>
        /// <returns>Awaitable reader result.</returns>
        public async Task<DicomReaderResult> ReadAsync(
            IByteSource source,
            IDicomReaderObserver fileMetaInfo,
            IDicomReaderObserver dataset,
            Func<ParseState, bool> stop = null)
        {
            var parse = await ParseAsync(source, fileMetaInfo, dataset, stop).ConfigureAwait(false);
            lock (_locker)
            {
                FileFormat = parse.Item2;
                Syntax = parse.Item3;
            }
            return parse.Item1;
        }
#endif

        private static ParseResult Parse(
            IByteSource source,
            IDicomReaderObserver fileMetasetInfoObserver,
            IDicomReaderObserver datasetObserver,
            Func<ParseState, bool> stop)
        {
            if (!source.Require(132))
            {
                return new ParseResult(DicomReaderResult.Error, DicomFileFormat.Unknown, null);
            }

            var fileFormat = DicomFileFormat.Unknown;
            var syntax = DicomTransferSyntax.ExplicitVRLittleEndian;

            Preprocess(source, ref fileFormat, ref syntax);

            var result = DoParse(
                source,
                fileMetasetInfoObserver,
                datasetObserver,
                stop,
                ref syntax,
                ref fileFormat);

            return new ParseResult(result, fileFormat, syntax);
        }

#if !NET35
        private static async Task<Tuple<DicomReaderResult, DicomFileFormat, DicomTransferSyntax>> ParseAsync(
            IByteSource source,
            IDicomReaderObserver fileMetasetInfoObserver,
            IDicomReaderObserver datasetObserver,
            Func<ParseState, bool> stop)
        {
            if (!source.Require(132))
            {
                return Tuple.Create(DicomReaderResult.Error, DicomFileFormat.Unknown, (DicomTransferSyntax)null);
            }

            var fileFormat = DicomFileFormat.Unknown;
            var syntax = DicomTransferSyntax.ExplicitVRLittleEndian;

            Preprocess(source, ref fileFormat, ref syntax);

            return
                await
                DoParseAsync(source, fileMetasetInfoObserver, datasetObserver, stop, syntax, fileFormat).ConfigureAwait(false);
        }
#endif

        private static void Preprocess(
            IByteSource source,
            ref DicomFileFormat fileFormat,
            ref DicomTransferSyntax syntax)
        {
            // mark file origin
            source.Mark();

            // test for DICM preamble
            source.Skip(128);
            if (source.GetUInt8() == 'D' && source.GetUInt8() == 'I' && source.GetUInt8() == 'C'
                && source.GetUInt8() == 'M')
            {
                fileFormat = DicomFileFormat.DICOM3;
            }

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

                if (@group > 0x00ff)
                {
                    source.Endian = Endian.Big;
                    syntax = DicomTransferSyntax.ExplicitVRBigEndian;

                    @group = Endian.Swap(@group);
                }

                if (@group > 0x00ff)
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
                var tag = new DicomTag(@group, element);

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

            // Adopt transfer syntax endianess to byte source.
            source.Endian = syntax.Endian;
        }

        private static DicomReaderResult DoParse(
            IByteSource source,
            IDicomReaderObserver fileMetasetInfoObserver,
            IDicomReaderObserver datasetObserver,
            Func<ParseState, bool> stop,
            ref DicomTransferSyntax syntax,
            ref DicomFileFormat fileFormat)
        {
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

            var reader = new DicomReader { IsExplicitVR = syntax.IsExplicitVR, IsDeflated = false };

            DicomReaderResult result;
            if (fileFormat == DicomFileFormat.DICOM3NoFileMetaInfo)
            {
                result = reader.Read(source, new DicomReaderMultiObserver(obs, datasetObserver), stop);
                UpdateFileFormatAndSyntax(code, uid, ref fileFormat, ref syntax);
            }
            else
            {
                if (reader.Read(source, new DicomReaderMultiObserver(obs, fileMetasetInfoObserver), _FileMetaInfoStopCriterion)
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
                reader.IsDeflated = syntax.IsDeflate;
                result = reader.Read(source, datasetObserver, stop);
            }

            return result;
        }

#if !NET35
        private static async Task<Tuple<DicomReaderResult, DicomFileFormat, DicomTransferSyntax>> DoParseAsync(
            IByteSource source,
            IDicomReaderObserver fileMetasetInfoObserver,
            IDicomReaderObserver datasetObserver,
            Func<ParseState, bool> stop,
            DicomTransferSyntax syntax,
            DicomFileFormat fileFormat)
        {
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

            var reader = new DicomReader { IsExplicitVR = syntax.IsExplicitVR, IsDeflated = false };

            DicomReaderResult result;
            if (fileFormat == DicomFileFormat.DICOM3NoFileMetaInfo)
            {
                result =
                    await
                    reader.ReadAsync(source, new DicomReaderMultiObserver(obs, datasetObserver), stop).ConfigureAwait(false);
                UpdateFileFormatAndSyntax(code, uid, ref fileFormat, ref syntax);
            }
            else
            {
                if (
                    await
                    reader.ReadAsync(
                        source,
                        new DicomReaderMultiObserver(obs, fileMetasetInfoObserver),
                        _FileMetaInfoStopCriterion).ConfigureAwait(false) != DicomReaderResult.Stopped)
                {
                    throw new DicomReaderException("DICOM File Meta Info ended prematurely");
                }

                UpdateFileFormatAndSyntax(code, uid, ref fileFormat, ref syntax);

                // rewind to last marker (start of previous tag)... ugly because 
                // it requires knowledge of how the parser is implemented
                source.Rewind();

                source.Endian = syntax.Endian;
                reader.IsExplicitVR = syntax.IsExplicitVR;
                reader.IsDeflated = syntax.IsDeflate;
                result = await reader.ReadAsync(source, datasetObserver, stop).ConfigureAwait(false);
            }

            return Tuple.Create(result, fileFormat, syntax);
        }
#endif

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
