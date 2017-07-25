﻿// // Copyright (c) 2012-2017 fo-dicom contributors.
// // Licensed under the Microsoft Public License (MS-PL).
// 

using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Dicom.Printing
{
    public class ImageBoxTest
    {
        #region Unit tests

        [Theory]
        [MemberData(nameof(SopClassUids))]
        public void ImageSequence_NoSequenceInImageBox_ReturnsNull(DicomUID sopClassUid)
        {
            var session = new FilmSession(DicomUID.BasicFilmSessionSOPClass);
            var filmBox = new FilmBox(session, null, DicomTransferSyntax.ImplicitVRLittleEndian);
            var imageBox = new ImageBox(filmBox, sopClassUid, null);

            Assert.Null(imageBox.ImageSequence);
        }

        #endregion

        #region Support data

        public static readonly IEnumerable<object[]> SopClassUids = new[]
        {
            new object[] { ImageBox.GraySOPClassUID },
            new object[] { ImageBox.ColorSOPClassUID } 
        };

        #endregion
    }
}

