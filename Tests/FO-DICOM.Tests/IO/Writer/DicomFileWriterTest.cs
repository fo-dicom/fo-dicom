﻿// Copyright (c) 2012-2020 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO;
using FellowOakDicom.IO.Writer;
using System.Net.Http.Headers;
using Xunit;

namespace FellowOakDicom.Tests.IO.Writer
{

    [Collection("General")]
    public class DicomFileWriterTest
    {
        #region Fields

        private const string Comment = "Some meaningful comment.";

        private static readonly DicomTag DoseCommentTag = DicomTag.DoseComment;

        private readonly DicomFileMetaInformation metaInfo;

        private readonly DicomDataset dataset;

        private readonly object _locker = new object();

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
                    writer.Write(target, this.metaInfo, this.dataset);
                }

                var expected = Comment;
                var readFile = DicomFile.Open(fileName);
                var actual = readFile.Dataset.GetSingleValue<string>(DoseCommentTag);
                Assert.Equal(expected, actual);

                var syntax = readFile.FileMetaInfo.TransferSyntax;
                Assert.Equal(DicomTransferSyntax.JPEG2000Lossless, syntax);
            }
        }

        #endregion
    }
}
