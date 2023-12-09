// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Imaging.Render;

namespace FellowOakDicom.Imaging.Reconstruction
{

    /// <summary>
    /// A wrapper around a source image to build a volume out of it
    /// </summary>
    public class ImageData
    {

        public DicomDataset Dataset { get; }

        public FrameGeometry Geometry { get; }

        public string FrameOfReferenceUID => Geometry?.FrameOfReferenceUid;

        public FrameOrientation Orientation => Geometry.Orientation;

        public DicomPixelData PixelData { get; }

        public IPixelData Pixels { get; }

        public double SortingValue { get; }

        public int InstanceNumber => Dataset.TryGetSingleValue(DicomTag.InstanceNumber, out int numb) ? numb : 0;


        public ImageData(string filename)
        {
            Dataset = DicomFile
                .Open(filename) // open file
                .Clone(DicomTransferSyntax.ExplicitVRLittleEndian) // ensure decompressed
                .Dataset; // the dataset of the decompressed file
            Geometry = new FrameGeometry(Dataset);
            PixelData = DicomPixelData.Create(Dataset);
            Pixels = PixelDataFactory.Create(PixelData, 0);

            SortingValue = Geometry.DirectionNormal.DotProduct(Geometry.PointTopLeft);
        }


        public ImageData(DicomDataset dataset)
        {
            Dataset = dataset
                .Clone(DicomTransferSyntax.ExplicitVRLittleEndian); // ensure decompressed
            Geometry = new FrameGeometry(Dataset);
            PixelData = DicomPixelData.Create(Dataset);
            Pixels = PixelDataFactory.Create(PixelData, 0);

            SortingValue = Geometry.DirectionNormal.DotProduct(Geometry.PointTopLeft);
        }

    }
}
