// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Mathematics;
using System.Collections.Generic;

namespace FellowOakDicom.Imaging.Reconstruction
{

    public enum StackType
    {
        Axial = 1,
        Coronal = 2,
        Sagittal = 3
    }

    /// <summary>
    /// Represents a new calculated stack of slices taken from a volume
    /// </summary>
    public class Stack
    {

        private readonly VolumeData _volume;

        public List<Slice> Slices { get; set; } = [];

        public decimal SliceDistance { get; }


        public Stack(VolumeData volume, StackType stackType, decimal spacing, decimal sliceDistance)
        {
            _volume = volume;
            SliceDistance = sliceDistance;
            switch (stackType)
            {
                case StackType.Axial:
                    CalculateAxial(spacing, SliceDistance);
                    break;
                case StackType.Coronal:
                    CalculateCoronal(spacing, SliceDistance);
                    break;
                case StackType.Sagittal:
                    CalculateSagittal(spacing, SliceDistance);
                    break;
            }
        }


        private void CalculateSagittal(decimal spacing, decimal sliceDistance)
        {
            var volumeVector = _volume.BoundingMax - _volume.BoundingMin;
            var topLeft = new Point3<decimal>(_volume.BoundingMin.X, _volume.BoundingMin.Y, _volume.BoundingMax.Z);
            var perSliceVec = new Vector3<decimal>(sliceDistance, 0, 0);

            var numberOfSlices = volumeVector.X / sliceDistance + 1;

            for (int i = 1; i <= numberOfSlices; i++)
            {
                var slice = new Slice(_volume, topLeft, new Vector3<decimal>(0, 1, 0), new Vector3<decimal>(0, 0, -1), (int)(volumeVector.Z / spacing), (int)(volumeVector.Y / spacing), spacing);
                Slices.Add(slice);
                topLeft += perSliceVec;
            }
        }


        private void CalculateCoronal(decimal spacing, decimal sliceDistance)
        {
            var volumeVector = _volume.BoundingMax - _volume.BoundingMin;
            var topLeft = new Point3<decimal>(_volume.BoundingMin.X, _volume.BoundingMin.Y, _volume.BoundingMax.Z);
            var perSliceVec = new Vector3<decimal>(0, sliceDistance, 0);

            var numberOfSlices = volumeVector.Y / sliceDistance + 1;

            for (int i = 1; i <= numberOfSlices; i++)
            {
                var slice = new Slice(_volume, topLeft, new Vector3<decimal>(1, 0, 0), new Vector3<decimal>(0, 0, -1), (int)(volumeVector.Z / spacing), (int)(volumeVector.X / spacing), spacing);
                Slices.Add(slice);
                topLeft += perSliceVec;
            }
        }


        private void CalculateAxial(decimal spacing, decimal sliceDistance)
        {
            var volumeVector = _volume.BoundingMax - _volume.BoundingMin;
            var topLeft = new Point3<decimal>(_volume.BoundingMin.X, _volume.BoundingMin.Y, _volume.BoundingMax.Z);
            var perSliceVec = new Vector3<decimal>(0, 0, -sliceDistance);

            var numberOfSlices = volumeVector.Z / sliceDistance + 1;

            for (int i = 1; i <= numberOfSlices; i++)
            {
                var slice = new Slice(_volume, topLeft, new Vector3<decimal>(1, 0, 0), new Vector3<decimal>(0, 1, 0), (int)(volumeVector.Y / spacing), (int)(volumeVector.X / spacing), spacing);
                Slices.Add(slice);
                topLeft += perSliceVec;
            }
        }

    }
}
