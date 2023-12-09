// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Printing;
using System.IO;
using Xunit;

namespace FellowOakDicom.Tests.Printing
{

    [Collection(TestCollections.General)]
    public class FilmBoxTest
    {
        #region Unit tests

        [Fact]
        public void Save_BasicFilmBox_CreatesRelevantFilesAndFolders()
        {
            var path = TestData.Resolve("Film Box Test 1");
            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);

            var session = new FilmSession(DicomUID.BasicFilmSession);
            var box = new FilmBox(session, null, DicomTransferSyntax.ImplicitVRLittleEndian);
            box.BasicImageBoxes.Add(new ImageBox(box, DicomUID.BasicGrayscaleImageBox, null));
            box.Save(path);

            Assert.True(File.Exists(Path.Combine(path, "FilmBox.dcm")));
            Assert.True(File.Exists(Path.Combine(path, "FilmBox.txt")));
            Assert.True(Directory.Exists(Path.Combine(path, "Images")));
            Assert.True(Directory.GetFiles(Path.Combine(path, "Images")).Length > 0);
        }

        [Fact]
        public void Load_BasicFilmBox_ExpectedSopClassFound()
        {
            var path = TestData.Resolve("Film Box Test 2");
            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);

            var expected = DicomUID.Generate();

            var session = new FilmSession(DicomUID.BasicFilmSession);
            var box = new FilmBox(session, expected, DicomTransferSyntax.ImplicitVRLittleEndian);
            box.BasicImageBoxes.Add(new ImageBox(box, DicomUID.BasicGrayscaleImageBox, null));
            box.Save(path);

            var loaded = FilmBox.Load(session, path);
            var actual = loaded.SOPInstanceUID;
            Assert.Equal(expected, actual);
            Assert.True(loaded.BasicImageBoxes.Count > 0);
        }

        [Fact]
        public void PresentationLut_NoReferencedPresentationLutSequence_GetterReturnsNull()
        {
            var session = new FilmSession(DicomUID.BasicFilmSession);
            var box = new FilmBox(session, null, DicomTransferSyntax.ImplicitVRLittleEndian);

            Assert.Null(box.PresentationLut);
        }

        #endregion
    }
}
