// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Buffer;
using FellowOakDicom.IO.Reader;
using Microsoft.Extensions.Logging;

namespace FellowOakDicom.Log
{
    public class DicomParserLogger : IDicomReaderObserver
    {
        private readonly Microsoft.Extensions.Logging.ILogger _log;

        private readonly Microsoft.Extensions.Logging.LogLevel _level;

        private int _depth;

        private readonly string _pad;

        public DicomParserLogger(Microsoft.Extensions.Logging.ILogger log, Microsoft.Extensions.Logging.LogLevel level)
        {
            _log = log;
            _level = level;
            _pad = string.Empty;
        }

        public void OnElement(IByteSource source, DicomTag tag, DicomVR vr, IByteBuffer data) =>
            _log.Log(
                _level,
                "{Marker:x8}: {Padding}{Tag} {VrCode} {TagDictionaryEntryName} [{Size}]",
                source.Marker,
                _pad,
                tag,
                vr.Code,
                tag.DictionaryEntry.Name,
                data.Size);

        public void OnBeginSequence(IByteSource source, DicomTag tag, uint length)
        {
            _log.Log(
                _level,
                "{Marker:x8}: {Padding}{Tag} {TagDictionaryEntryName} SQ {Length}",
                source.Marker,
                _pad,
                tag,
                tag.DictionaryEntry.Name,
                length);
            IncreaseDepth();
        }

        public void OnBeginSequenceItem(IByteSource source, uint length)
        {
            _log.Log(_level, "{Marker:x8}: {Padding}Item:", source.Marker, _pad);
            IncreaseDepth();
        }

        public void OnEndSequenceItem() => DecreaseDepth();

        public void OnEndSequence() => DecreaseDepth();

        public void OnBeginFragmentSequence(IByteSource source, DicomTag tag, DicomVR vr)
        {
            _log.Log(
                _level,
                "{Marker:x8}: {Padding}{Tag} {VrCode} {TagDictionaryEntryName}",
                source.Marker,
                _pad,
                tag,
                vr.Code,
                tag.DictionaryEntry.Name);
            IncreaseDepth();
        }

        public void OnFragmentSequenceItem(IByteSource source, IByteBuffer data) => _log.Log(_level, "{Marker:x8}: {Padding}Fragment [{Size}]", source.Marker, _pad, data?.Size ?? 0);

        public void OnEndFragmentSequence() => DecreaseDepth();

        private void IncreaseDepth() => _depth++;

        private void DecreaseDepth() => _depth--;
    }
}
