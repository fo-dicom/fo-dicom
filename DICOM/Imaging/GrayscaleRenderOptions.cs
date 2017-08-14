// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

using Dicom.Imaging.Codec;

namespace Dicom.Imaging
{
    /// <summary>
    /// Grayscale rendering options class
    /// </summary>
    public class GrayscaleRenderOptions
    {
        #region FIELDS

        private Color32[] _colorMap;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// GrayscaleRenderOptions constructor using BitDepth values
        /// </summary>
        /// <param name="bits">Bit depth information</param>
        private GrayscaleRenderOptions(BitDepth bits)
        {
            BitDepth = bits;
            Invert = false;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// BitDepth used to initialize the GrayscaleRenderOptions
        /// </summary>
        public BitDepth BitDepth { get; private set; }

        /// <summary>
        /// Pixel data rescale slope
        /// </summary>
        public double RescaleSlope { get; private set; }

        /// <summary>
        /// Pixel data rescale interception
        /// </summary>
        public double RescaleIntercept { get; private set; }

        /// <summary>
        /// VOI LUT function (LINEAR or SEGMOID)
        /// </summary>
        public string VOILUTFunction { get; private set; }

        /// <summary>
        /// Window width
        /// </summary>
        public double WindowWidth { get; set; }

        /// <summary>
        /// Window center
        /// </summary>
        public double WindowCenter { get; set; }

        /// <summary>
        /// Gets or sets the color map associated with the grayscale image.
        /// </summary>
        public Color32[] ColorMap
        {
            get
            {
                return _colorMap;
            }
            set
            {
                if (value == null || value.Length != 256) throw new DicomImagingException("Expected 256 entry color map");
                _colorMap = value;
            }
        }

        /// <summary>
        /// Set to true to render the output in inverted grey
        /// </summary>
        public bool Invert { get; private set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Create <see cref="GrayscaleRenderOptions"/>  from <paramref name="dataset"/> and populate the options properties with values:
        /// Bit Depth
        /// Rescale Slope
        /// Rescale Intercept
        /// Window Width
        /// Window Center
        /// </summary>
        /// <param name="dataset">Dataset to extract <see cref="GrayscaleRenderOptions"/> from</param>
        /// <returns>New grayscale render options instance</returns>
        public static GrayscaleRenderOptions FromDataset(DicomDataset dataset)
        {
            if (dataset.Contains(DicomTag.WindowWidth) && dataset.Get<double>(DicomTag.WindowWidth) >= 1.0
                && dataset.Contains(DicomTag.WindowCenter))
            {
                //If dataset contains WindowWidth and WindowCenter valid attributes used initially for the grayscale options
                return FromWindowLevel(dataset);
            }

            if (dataset.Contains(DicomTag.SmallestImagePixelValue) && dataset.Contains(DicomTag.LargestImagePixelValue)
                && dataset.Get<int>(DicomTag.SmallestImagePixelValue)
                < dataset.Get<int>(DicomTag.LargestImagePixelValue))
            {
                //If dataset contains valid SmallesImagePixelValue and LargesImagePixelValue attributes, use range to calculate
                //WindowWidth and WindowCenter
                return FromImagePixelValueTags(dataset);
            }

            //If reached here, minimum and maximum pixel values calculated from pixels data to calculate
            //WindowWidth and WindowCenter
            return FromMinMax(dataset);
        }

        /// <summary>
        /// Create grayscale render options based on window level data.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which render options should be obtained.</param>
        /// <returns>Grayscale render options based on window level data.</returns>
        public static GrayscaleRenderOptions FromWindowLevel(DicomDataset dataset)
        {
            if (!dataset.Contains(DicomTag.WindowWidth) ||
                !dataset.Contains(DicomTag.WindowCenter))
                return null;

            var bits = BitDepth.FromDataset(dataset);
            var options = new GrayscaleRenderOptions(bits);

            options.RescaleSlope = dataset.Get<double>(DicomTag.RescaleSlope, 1.0);
            options.RescaleIntercept = dataset.Get<double>(DicomTag.RescaleIntercept, 0.0);

            options.WindowWidth = dataset.Get<double>(DicomTag.WindowWidth);
            options.WindowCenter = dataset.Get<double>(DicomTag.WindowCenter);

            options.VOILUTFunction = dataset.Get<string>(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            return options;
        }

        /// <summary>
        /// Create grayscale render options based on specified image pixel values.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which render options should be obtained.</param>
        /// <returns>Grayscale render options based on specified image pixel values.</returns>
        public static GrayscaleRenderOptions FromImagePixelValueTags(DicomDataset dataset)
        {
            if (!dataset.Contains(DicomTag.SmallestImagePixelValue) ||
                !dataset.Contains(DicomTag.LargestImagePixelValue))
                return null;

            var bits = BitDepth.FromDataset(dataset);
            var options = new GrayscaleRenderOptions(bits);

            options.RescaleSlope = dataset.Get<double>(DicomTag.RescaleSlope, 1.0);
            options.RescaleIntercept = dataset.Get<double>(DicomTag.RescaleIntercept, 0.0);

            int smallValue = dataset.Get<int>(DicomTag.SmallestImagePixelValue);
            int largeValue = dataset.Get<int>(DicomTag.LargestImagePixelValue);

            if (smallValue >= largeValue)
            {
                throw new DicomImagingException(
                    string.Format("Smallest Image Pixel Value ({0}) > Largest Value ({1})", smallValue, largeValue));
            }

            options.WindowWidth = Math.Abs(largeValue - smallValue);
            options.WindowCenter = (largeValue + smallValue) / 2.0;

            options.VOILUTFunction = dataset.Get<string>(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            return options;
        }

        /// <summary>
        /// Create grayscale render options based on identified minimum and maximum pixel values.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which render options should be obtained.</param>
        /// <returns>Grayscale render options based on identified minimum and maximum pixel values.</returns>
        public static GrayscaleRenderOptions FromMinMax(DicomDataset dataset)
        {
            var bits = BitDepth.FromDataset(dataset);
            var options = new GrayscaleRenderOptions(bits);

            options.RescaleSlope = dataset.Get<double>(DicomTag.RescaleSlope, 1.0);
            options.RescaleIntercept = dataset.Get<double>(DicomTag.RescaleIntercept, 0.0);

            int padding = dataset.Get<int>(DicomTag.PixelPaddingValue, 0, Int32.MinValue);

            var transcoder = new DicomTranscoder(
                dataset.InternalTransferSyntax,
                DicomTransferSyntax.ExplicitVRLittleEndian);

            var pixels = transcoder.DecodePixelData(dataset, 0);
            var range = pixels.GetMinMax(padding);

            if (range.Minimum < bits.MinimumValue || range.Minimum == Double.MaxValue) range.Minimum = bits.MinimumValue;
            if (range.Maximum > bits.MaximumValue || range.Maximum == Double.MinValue) range.Maximum = bits.MaximumValue;

            var min = range.Minimum * options.RescaleSlope + options.RescaleIntercept;
            var max = range.Maximum * options.RescaleSlope + options.RescaleIntercept;

            options.WindowWidth = Math.Abs(max - min);
            options.WindowCenter = (max + min) / 2.0;

            options.VOILUTFunction = dataset.Get<string>(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            return options;
        }

        /// <summary>
        /// Create grayscale render options based on bit range.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which render options should be obtained.</param>
        /// <returns>Grayscale render options based on bit range.</returns>
        public static GrayscaleRenderOptions FromBitRange(DicomDataset dataset)
        {
            var bits = BitDepth.FromDataset(dataset);
            var options = new GrayscaleRenderOptions(bits);

            options.RescaleSlope = dataset.Get<double>(DicomTag.RescaleSlope, 1.0);
            options.RescaleIntercept = dataset.Get<double>(DicomTag.RescaleIntercept, 0.0);

            var min = bits.MinimumValue * options.RescaleSlope + options.RescaleIntercept;
            var max = bits.MaximumValue * options.RescaleSlope + options.RescaleIntercept;

            options.WindowWidth = Math.Abs(max - min);
            options.WindowCenter = (max + min) / 2.0;

            options.VOILUTFunction = dataset.Get<string>(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            return options;
        }

        /// <summary>
        /// Create grayscale render options based on pixel data histogram.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which render options should be obtained.</param>
        /// <param name="percent">Percentage of histogram window to include.</param>
        /// <returns>Grayscale render options based on pixel data histogram.</returns>
        public static GrayscaleRenderOptions FromHistogram(DicomDataset dataset, int percent = 90)
        {
            var bits = BitDepth.FromDataset(dataset);
            var options = new GrayscaleRenderOptions(bits);

            options.RescaleSlope = dataset.Get<double>(DicomTag.RescaleSlope, 1.0);
            options.RescaleIntercept = dataset.Get<double>(DicomTag.RescaleIntercept, 0.0);

            var transcoder = new DicomTranscoder(
                dataset.InternalTransferSyntax,
                DicomTransferSyntax.ExplicitVRLittleEndian);

            var pixels = transcoder.DecodePixelData(dataset, 0);
            var histogram = pixels.GetHistogram(0);

            int padding = dataset.Get<int>(DicomTag.PixelPaddingValue, 0, Int32.MinValue);
            if (padding != Int32.MinValue) histogram.Clear(padding);

            histogram.ApplyWindow(percent);

            var min = histogram.WindowStart * options.RescaleSlope + options.RescaleIntercept;
            var max = histogram.WindowEnd * options.RescaleSlope + options.RescaleIntercept;

            options.WindowWidth = Math.Abs(max - min);
            options.WindowCenter = (max + min) / 2.0;

            options.VOILUTFunction = dataset.Get<string>(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            return options;
        }

        /// <summary>
        /// Get grayscale color map based on Photometric Interpretation.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which Photometric Interpretation should be obtained.</param>
        /// <returns>Color map associated with the identified Photometric Interpretation.</returns>
        private static Color32[] GetColorMap(DicomDataset dataset)
        {
            return dataset.Get<PhotometricInterpretation>(DicomTag.PhotometricInterpretation, null)
                   == PhotometricInterpretation.Monochrome1
                       ? ColorTable.Monochrome1
                       : ColorTable.Monochrome2;
        }

        #endregion
    }
}
