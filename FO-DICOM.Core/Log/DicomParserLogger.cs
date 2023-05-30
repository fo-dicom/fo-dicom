﻿// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Reader;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Log
{
    public class DicomParserLogger : IDicomReaderObserver
    {
        private readonly ILogger _log;

        private readonly LogLevel _level;

        private int _depth;

        private readonly string _pad;

        public DicomParserLogger(ILogger log, LogLevel level)
        {
            _log = log;
            _level = level;
            _depth = 0;
            _pad = string.Empty;
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
            _log.Log(_level, "{marker:x8}: {padding}Fragment [{size}]", source.Marker, _pad, data?.Size ?? 0);
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
