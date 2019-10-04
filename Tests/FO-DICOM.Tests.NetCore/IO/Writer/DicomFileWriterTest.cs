// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO.Writer
{
    using Xunit;

    [Collection("General")]
    public class DicomFileWriterTest
    {
        #region Fields

        private const string Comment = "Some meaningful comment.";

        private static readonly DicomTag DoseCommentTag = DicomTag.DoseComment;

        private readonly DicomFileMetaInformation metaInfo;

        private readonly DicomDataset dataset;

        private readonly object locker;

        #endregion

        #region Constructors

        public DicomFileWriterTest()
        {
            this.metaInfo = new DicomFileMetaInformation
                               {
                                   MediaStorageSOPClassUID = DicomUID.RTDoseStorage,
                                   TransferSyntax = DicomTransferSyntax.JPEG2000Lossless
                               };
            this.dataset = new DicomDataset(
                new DicomUniqueIdentifier(DicomTag.SOPClassUID, DicomUID.RTDoseStorage),
                new DicomUniqueIdentifier(DicomTag.SOPInstanceUID, "1.2.3"),
                new DicomLongString(DoseCommentTag, Comment));

            this.locker = new object();
        }

        #endregion

        #region Unit tests

        [Fact]
        public void Write_SimpleFile_CommentMaintained()
        {
            lock (this.locker)
            {
                const string fileName = @".\Test Data\dicomfilewriter_write.dcm";
                var file = IOManager.CreateFileReference(fileName);

                using (var target = new FileByteTarget(file))
                {
                    var writer = new DicomFileWriter(new DicomWriteOptions());
                    writer.Write(target, this.metaInfo, this.dataset);
                }

                var expected = Comment;
                var readFile = DicomFile.Open(fileName);
                var actual = readFile.Dataset.Get<string>(DoseCommentTag);
                Assert.Equal(expected, actual);

                var syntax = readFile.FileMetaInfo.TransferSyntax;
                Assert.Equal(DicomTransferSyntax.JPEG2000Lossless, syntax);
            }
        }

        #endregion
    }
}
