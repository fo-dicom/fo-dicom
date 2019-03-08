// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

using Dicom.IO;
using Dicom.IO.Buffer;
using Dicom.IO.Reader;

namespace Dicom.Log
{
    public class DicomParserLogger : IDicomReaderObserver
    {
        private Logger _log;

        private LogLevel _level;

        private int _depth;

        private string _pad;

        public DicomParserLogger(Logger log, LogLevel level)
        {
            _log = log;
            _level = level;
            _depth = 0;
            _pad = String.Empty;
        }

        public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data)
        {
            _log.Log(
                _level,
                "{marker:x8}: {padding}{tag} {vrCode} {tagDictionaryEntryName} [{size}]",
                source.Marker,
                _pad,
                tag,
                vr.Code,
                tag.DictionaryEntry.Name,
                data.Size);
        }

        public void OnBeginSequence(IByteSource source, DicomTag tag, uint length)
        {
            _log.Log(
                _level,
                "{marker:x8}: {padding}{tag} SQ {length}",
                source.Marker,
                _pad,
                tag,
                tag.DictionaryEntry.Name,
                length);
            IncreaseDepth();
        }

        public void OnBeginSequenceItem(IByteSource source, uint length)
        {
            _log.Log(_level, "{marker:x8}: {padding}Item:", source.Marker, _pad);
            IncreaseDepth();
        }

        public void OnEndSequenceItem()
        {
            DecreaseDepth();
        }

        public void OnEndSequence()
        {
            DecreaseDepth();
        }

        public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr)
        {
            _log.Log(
                _level,
                "{marker:x8}: {padding}{tag} {vrCode} {tagDictionaryEntryName}",
                source.Marker,
                _pad,
                tag,
                vr.Code,
                tag.DictionaryEntry.Name);
            IncreaseDepth();
        }

        public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data)
        {
            _log.Log(_level, "{marker:x8}: {padding}Fragment [{size}]", source.Marker, _pad, data.Size);
        }

        public void OnEndFragmentSequence()
        {
            DecreaseDepth();
        }

        private void IncreaseDepth()
        {
            _depth++;
        }

        private void DecreaseDepth()
        {
            _depth--;
        }
    }
}
