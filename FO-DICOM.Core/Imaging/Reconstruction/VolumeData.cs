// Copyright (c) 2012-2025 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.LUT;
using FellowOakDicom.Imaging.Mathematics;
using FellowOakDicom.Imaging.Render;
using FellowOakDicom.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FellowOakDicom.Imaging.Reconstruction
{

    /// <summary>
    /// Represents a volume by having a list of ImageData instances. 
    /// </summary>
    public class VolumeData
    {

        private readonly List<ImageData> _slices;
        private double[] _sortOrders;

        private Vector3D _slicesNormal;
        private double _maxSliceSpace;
        private double _minSliceSpace;

        public Point3D BoundingMin { get; private set; }
        public Point3D BoundingMax { get; private set; }

        public double PixelSpacingInSource => _slices?.FirstOrDefault()?.Geometry.PixelSpacingBetweenColumns ?? 0;
        public IntervalD SliceSpaces => new IntervalD(_minSliceSpace, _maxSliceSpace);

        private ILUT _lut = null;

        private readonly Lazy<DicomDataset> _commonData;
        public DicomDataset CommonData => _commonData.Value;

        
        /// <summary>
        /// Constructs a VolumeData object from a multi-layer dataset (eg. Enhanced CT).
        /// It is strongly recommended this dataset is already decompressed before being passed to this constructor, or each slice will be decompressed separately.
        /// </summary>
        /// <param name="dataset">The dataset, containing at least the tag <see cref="DicomTag.NumberOfFrames"/></param>
        public VolumeData(DicomDataset dataset) : this(ConstructSlicesFromMultiFrameDataset(dataset))
        {
        }

        
        public VolumeData(IEnumerable<ImageData> slices)
        {
            slices = new List<ImageData>(slices
                    .Where(s => s != null) // only use valid slices
                    .Where(s => s.FrameOfReferenceUID != null) // remove all slices without geometry data (presentation states, luts, ..)
                    );

            // validate data
            ValidateInput(slices.Select(s => s.FrameOfReferenceUID).Distinct().Count() == 1, "The images are mixed up from different stacks");

            _slices = slices.GroupBy(s => s.Orientation).OrderBy(g => g.Count()).Last().ToList();
            ValidateInput(slices.Count() > 5, "There are too few images for reconstruction");

            // TODO: check for each having the same normal vector

            _commonData = new Lazy<DicomDataset>(GetCommonData);
            BuildVolumeData();
        }

        
        private static IEnumerable<ImageData> ConstructSlicesFromMultiFrameDataset(DicomDataset dataset)
        {
            ValidateInput(dataset.Contains(DicomTag.NumberOfFrames), "Given dataset must contain multiple frames");
            
            var numberOfFrames = dataset.GetItem(DicomTag.NumberOfFrames).Value;
            var pixelData = DicomPixelData.Create(dataset);
            
            return Enumerable.Range(0, numberOfFrames)
                .Select(frame => new ImageData(dataset, pixelData, frame));
        }
        

        private static void ValidateInput(Func<bool> validation, string message = "")
        {
            if (!validation())
            {
                throw new DicomDataException(message);
            }
        }


        private static void ValidateInput(bool validated, string message = "") => ValidateInput(() => validated, message);


        private void BuildVolumeData()
        {
            // sort the slices
            _slices.Sort((a, b) => a.SortingValue.CompareTo(b.SortingValue));

            // calcualate values
            _slicesNormal = _slices[0].Geometry.DirectionNormal;
            var sliceDistances = _slices.Diff((a, b) => b.SortingValue - a.SortingValue);
            _minSliceSpace = sliceDistances.Min();
            _maxSliceSpace = sliceDistances.Max();

            var boundings = _slices.Select(s => s.Geometry.GetBoundingBox());
            BoundingMin = boundings.Select(b => b.min).GetBoundingBox().min;
            BoundingMax = boundings.Select(b => b.max).GetBoundingBox().max;
            _sortOrders = _slices.Select(s => s.SortingValue).ToArray();
        }


        private DicomDataset GetCommonData()
        {
            var valueComparer = new DicomValueComparer();
            var commonData = new DicomDataset().NotValidated();
            commonData.Add(
                _slices
                .Select(s => s.Dataset.Where(t => t.Tag != DicomTag.PixelData || (DicomOverlayData.IsOverlaySequence(t))))
                .Aggregate((x, y) => x.Intersect(y, valueComparer))
                );
            return commonData;
        }


        public ILUT Lut
        {
            get
            {
                if (_lut == null)
                {
                    var option = GrayscaleRenderOptions.FromDataset(_slices[0].Dataset, 0);
                    var pipelie = new GenericGrayscalePipeline(option);
                    _lut = pipelie.LUT;
                    _lut.Recalculate();
                }
                return _lut;
            }
        }


        private int SortingIndex(double value, int guess)
        {
            var len = _sortOrders.Length;
            while (_sortOrders[guess] >= value && guess > 0)
            {
                guess--;
            }

            while (guess < len)
            {
                if (_sortOrders[guess] >= value)
                {
                    return guess;
                }
                guess++;
            }
            return -1;
        }


        /// <summary>
        /// ['1', '0', '0', '0', '0', '-1'] you are dealing with Coronal plane view
        /// ['0', '1', '0', '0', '0', '-1'] you are dealing with Sagittal plane view
        /// ['1', '0', '0', '0', '1', '0'] you are dealing with Axial plane view
        /// </summary>
        /// <param name="topleft"></param>
        /// <param name="rowDir"></param>
        /// <param name="colDir"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="spacing"></param>
        /// <returns></returns>
        public double[] GetCut(Point3D topleft, Vector3D rowDir, Vector3D colDir, int rows, int cols, double spacing)
        {
            var output = new double[rows * cols];

            var deltaX = spacing * rowDir;
            var deltaY = spacing * colDir;
            var orderedDeltaX = _slicesNormal.DotProduct(deltaX);
            var orderedDeltaY = _slicesNormal.DotProduct(deltaY);

            var orderedRowStart = _slicesNormal.DotProduct(topleft);

            var lastIndex = 0;

            Parallel.For(0, cols, x =>
            {
                var pointInPatSpace = topleft + x * deltaX;
                var ordered = orderedRowStart + x * orderedDeltaX;
                for (int y = 0; y < rows; y++)
                {

                    // get index of the two planes
                    var index = SortingIndex(ordered, lastIndex);
                    if (index > 0)
                    {
                        lastIndex = index;

                        var nextSlice = _slices[index];
                        var prevSlice = _slices[index - 1];

                        var nextImgSpace = nextSlice.Geometry.TransformPatientPointToImage(pointInPatSpace);
                        var prevImgSpace = prevSlice.Geometry.TransformPatientPointToImage(pointInPatSpace);

                        var nextPixel = Interpolate(nextSlice.Pixels, nextImgSpace);
                        var prevPixel = Interpolate(prevSlice.Pixels, prevImgSpace);

                        if (nextPixel.HasValue && prevPixel.HasValue)
                        {
                            var pixel = (prevPixel.Value * (nextSlice.SortingValue - ordered) + nextPixel.Value * (ordered - prevSlice.SortingValue)) / (nextSlice.SortingValue - prevSlice.SortingValue);
                            // convert from 12bit to 8 bit
                            output[x + y * cols] = pixel;
                        }
                    }
                    pointInPatSpace += deltaY;
                    ordered += orderedDeltaY;
                }
            });

            return output;
        }


        private double? Interpolate(IPixelData pixels, Point2D imgSpace)
        {
            if ((imgSpace.X >= 0.0) && (imgSpace.X < pixels.Width - 1) && (imgSpace.Y >= 0.0) && (imgSpace.Y < pixels.Height - 1))
            {
                var posX = (int)Math.Floor(imgSpace.X);
                double alphaX = imgSpace.X - posX;
                var posY = (int)Math.Floor(imgSpace.Y);
                double alphaY = imgSpace.Y - posY;

                return (1 - alphaX) * ((1 - alphaY) * pixels.GetPixel(posX, posY)
                    + alphaY * pixels.GetPixel(posX, posY + 1))
                    + alphaX * ((1 - alphaY) * pixels.GetPixel(posX + 1, posY)
                    + alphaY * pixels.GetPixel(posX + 1, posY + 1));
            }

            return null;
        }

    }
}
