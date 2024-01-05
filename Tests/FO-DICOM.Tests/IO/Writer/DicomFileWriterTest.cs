// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using FellowOakDicom.IO.Writer;
using Xunit;

namespace FellowOakDicom.Tests.IO.Writer
{

    [Collection(TestCollections.General)]
    public class DicomFileWriterTest
    {
        #region Fields

        private const string _comment = "Some meaningful comment.";

        private static readonly DicomTag _doseCommentTag = DicomTag.DoseComment;

        private readonly DicomFileMetaInformation _metaInfo;

        private readonly DicomDataset _dataset;

        private readonly object _locker = new object();

        #endregion

        #region Constructors

        public DicomFileWriterTest()
        {
            _metaInfo = new DicomFileMetaInformation
                               {
                                   MediaStorageSOPClassUID = DicomUID.RTDoseStorage,
                                   TransferSyntax = DicomTransferSyntax.JPEG2000Lossless
                               };
            _dataset = new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.RTDoseStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"),
                new DicomLongString(_doseCommentTag, _comment));
        }

        #endregion

        #region Unit tests

        [Fact]
        public void Write_SimpleFile_CommentMaintained()
        {
            lock (_locker)
            {
                string fileName = TestData.Resolve("dicomfilewriter_write.dcm");
                var file = new FileReference(fileName);

                using (var target = new FileByteTarget(file))
                {
                    var writer = new DicomFileWriter(new DicomWriteOptions());
                    writer.Write(target, _metaInfo, _dataset);
                }

                var expected = _comment;
                var readFile = DicomFile.Open(fileName);
                var actual = readFile.Dataset.GetSingleValue<string>(_doseCommentTag);
                Assert.Equal(expected, actual);

                var syntax = readFile.FileMetaInfo.TransferSyntax;
                Assert.Equal(DicomTransferSyntax.JPEG2000Lossless, syntax);
            }
        }

        #endregion
    }
}
