// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System;
using System.Collections.Generic;

namespace FellowOakDicom.Imaging.Reconstruction
{

    /// <summary>
    /// Helps to create DICOM datasets from generated stack.
    /// 
    /// The new DataSets are based on the common DataSets of the source images.
    /// All the tags that have equal values in all of the source images are also copyied into the new DicomDatasets.
    /// </summary>
    public class DicomGenerator
    {

        private readonly DicomDataset _commonDataset;

        /// <summary>
        /// Constructor of DicomGenerator
        /// </summary>
        /// <param name="commonDataset">A Dataset that contains all the values that are common in the source images. So this should contain all the patient-, studies- and technics-data that will also fit to the new generated images.</param>
        public DicomGenerator(DicomDataset commonDataset)
        {
            _commonDataset = commonDataset;
        }


        /// <summary>
        /// Iterates all the calculated slices in a stack, wraps it into a DicomDataset with the metadata of the source images and returns it as enumerable.
        /// </summary>
        /// <param name="newSeriesDescription">The description of this new series</param>
        public IEnumerable<DicomDataset> StoreAsDicom(Stack stackToStore, string newSeriesDescription)
        {
            var newSeriesUID = DicomUID.Generate();
            var iCounter = 1;

            foreach (var slice in stackToStore.Slices)
            {
                var dataset = _commonDataset.Clone();

                dataset.AddOrUpdate(DicomTag.SeriesDescription, newSeriesDescription);
                dataset.AddOrUpdate(DicomTag.SeriesInstanceUID, newSeriesUID);
                dataset.AddOrUpdate(DicomTag.SOPInstanceUID, DicomUID.Generate());
                dataset.AddOrUpdate(DicomTag.InstanceNumber, iCounter++);
                dataset.AddOrUpdate(DicomTag.AcquisitionDate, DateTime.Now);
                dataset.AddOrUpdate(DicomTag.AcquisitionDateTime, DateTime.Now);

                dataset.AddOrUpdate(DicomTag.Rows, (ushort)slice.Rows);
                dataset.AddOrUpdate(DicomTag.Columns, (ushort)slice.Columns);
                dataset.AddOrUpdate(DicomTag.ImagePositionPatient, slice.TopLeft.ToArray());
                dataset.AddOrUpdate(DicomTag.ImageOrientationPatient, new double[] { slice.RowDirection.X, slice.RowDirection.Y, slice.RowDirection.Z, slice.ColumnDirection.X, slice.ColumnDirection.Y, slice.ColumnDirection.Z });
                dataset.AddOrUpdate(DicomTag.PixelSpacing, new double[] { slice.Spacing, slice.Spacing });
                dataset.AddOrUpdate(DicomTag.SliceThickness, stackToStore.SliceDistance);

                if (!dataset.Contains(DicomTag.BitsStored))
                {
                    var interval = slice.GetMinMaxValue();
                    var bitsStored = (ushort)Math.Ceiling(Math.Log(interval.Max, 2));

                    dataset.AddOrUpdate(DicomTag.BitsStored, bitsStored);
                    dataset.AddOrUpdate(DicomTag.HighBit, bitsStored - 1);
                }

                dataset.AddOrUpdate(DicomTag.PatientPosition, (string)null);

                var pixelData = DicomPixelData.Create(dataset, newPixelData: true);

                pixelData.AddFrame(new MemoryByteBuffer(slice.RenderRawData(pixelData.BytesAllocated)));

                yield return dataset;
            }
        }


    }
}
