// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Linq;

namespace Dicom.StructuredReport
{
    using Xunit;

    [Collection("General")]
    public class DicomContentItemTest
    {
        #region Unit tests

        [Fact]
        public void Children_NodeWithoutChildren_Success()
        {
            var contentItem = new DicomContentItem(new DicomCodeItem("113820", "DCM", "CT Acquisition Type"),
                DicomRelationship.Contains,
                new DicomCodeItem("113805", "DCM", "Constant Angle Acquisition"));

            Assert.Empty(contentItem.Children());
        }

        [Fact]
        public void Children_NodeWithChildren_Success()
        {
            var contentItem = new DicomContentItem(
                new DicomCodeItem("113820", "DCM", "CT Acquisition Type"),
                DicomRelationship.Contains,
                new DicomCodeItem("113805", "DCM", "Constant Angle Acquisition"));
            contentItem.Add(new DicomCodeItem("113961", "DCM", "Reconstruction Algorithm"),
                DicomRelationship.Contains, new DicomCodeItem("113962", "DCM", "Filtered Back Projection"));

            var children = contentItem.Children().ToList();
            Assert.Single(children);
            Assert.Equal(new DicomCodeItem("113961", "DCM", null), children[0].Code);
            Assert.Equal(DicomRelationship.Contains, children[0].Relationship);
            Assert.Equal(new DicomCodeItem("113962", "DCM", null), children[0].Get<DicomCodeItem>());
        }

        #endregion
    }
}
