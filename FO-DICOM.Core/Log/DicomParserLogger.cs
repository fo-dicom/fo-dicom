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

        public void OnElement(IByteSource source, long position, DicomTag tag, DicomVR vr, IByteBuffer data) =>
            _log.Log(
                _level,
                "{Marker:x8}: {Padding}{Tag} {VrCode} {TagDictionaryEntryName} [{Size}]",
                position,
                _pad,
                tag,
                vr.Code,
                tag.DictionaryEntry.Name,
                data.Size);

        public void OnBeginSequence(IByteSource source, long position, DicomTag tag, uint length)
        {
            _log.Log(
                _level,
                "{Marker:x8}: {Padding}{Tag} {TagDictionaryEntryName} SQ {Length}",
                position,
                _pad,
                tag,
                tag.DictionaryEntry.Name,
                length);
            IncreaseDepth();
        }

        public void OnBeginSequenceItem(IByteSource source, long position, uint length)
        {
            _log.Log(_level, "{Marker:x8}: {Padding}Item:", position, _pad);
            IncreaseDepth();
        }

        public void OnEndSequenceItem() => DecreaseDepth();

        public void OnEndSequence() => DecreaseDepth();

        public void OnBeginFragmentSequence(IByteSource source, long position, DicomTag tag, DicomVR vr)
        {
            _log.Log(
                _level,
                "{Marker:x8}: {Padding}{Tag} {VrCode} {TagDictionaryEntryName}",
                position,
                _pad,
                tag,
                vr.Code,
                tag.DictionaryEntry.Name);
            IncreaseDepth();
        }

        public void OnFragmentSequenceItem(IByteSource source, long position, IByteBuffer data) => _log.Log(_level, "{Marker:x8}: {Padding}Fragment [{Size}]", position, _pad, data?.Size ?? 0);

        public void OnEndFragmentSequence() => DecreaseDepth();

        private void IncreaseDepth() => _depth++;

        private void DecreaseDepth() => _depth--;
    }
}
