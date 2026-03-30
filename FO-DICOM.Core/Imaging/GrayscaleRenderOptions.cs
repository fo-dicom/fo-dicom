// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.Imaging.Codec;
using FellowOakDicom.Imaging.LUT;
using System;
using System.Linq;

namespace FellowOakDicom.Imaging
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
        /// Modality LUT Sequence
        /// </summary>
        public IModalityLUT ModalityLUT { get; private set; }

        /// <summary>
        /// VOI LUT Sequence
        /// </summary>
        public DicomSequence VOILUTSequence { get; private set; }

        /// <summary>
        /// Window width
        /// </summary>
        public double WindowWidth { get; set; }

        /// <summary>
        /// Window center
        /// </summary>
        public double WindowCenter { get; set; }

        private bool _useVOILUT = false;
        /// <summary>
        /// Use VOI LUT if available
        /// </summary>
        public bool UseVOILUT
        {
            get => _useVOILUT && VOILUTSequence != null;
            set => _useVOILUT = value;
        }

        /// <summary>
        /// Gets or sets the color map associated with the grayscale image.
        /// </summary>
        public Color32[] ColorMap
        {
            get => _colorMap;
            set
            {
                if (value == null || value.Length != 256)
                {
                    throw new DicomImagingException("Expected 256 entry color map");
                }
                _colorMap = value;
            }
        }

        /// <summary>
        /// Set to true to render the output in inverted grey
        /// </summary>
        public bool Invert { get; set; } = false;

        #endregion

        #region METHODS

        /// <summary>
        /// Create <see cref="GrayscaleRenderOptions"/> from <paramref name="dataset"/> and populate the options properties with values:
        /// Bit Depth
        /// Rescale Slope
        /// Rescale Intercept
        /// Window Width
        /// Window Center
        /// </summary>
        /// <param name="dataset">Dataset to extract <see cref="GrayscaleRenderOptions"/> from</param>
        /// <param name="frame">The zero-based frame index for which the grayscale options should be extracted. Use 0 in case of a single-framed image.</param>
        /// <returns>New grayscale render options instance</returns>
        public static GrayscaleRenderOptions FromDataset(DicomDataset dataset, int frame)
        {
            GrayscaleRenderOptions grayscaleRenderOptions;
            if (dataset.TryGetValue(DicomTag.WindowWidth, 0, out double windowWidth) && windowWidth > 0
                && dataset.TryGetValue(DicomTag.WindowCenter, 0, out double _))
            {
                // If dataset contains WindowWidth and WindowCenter valid attributes used initially for the grayscale options
                grayscaleRenderOptions = FromWindowLevel(dataset, frame);
            }
            else if (dataset.FunctionalGroupValues(frame) is { } functionalGroupValues
                     && functionalGroupValues.TryGetValue(DicomTag.WindowWidth, 0, out double functionalWindowWidth) && functionalWindowWidth > 0
                     && functionalGroupValues.TryGetValue(DicomTag.WindowCenter, 0, out double _))
            {
                grayscaleRenderOptions = FromFunctionalWindowLevel(dataset, frame);
            }
            else if (dataset.TryGetSingleValue(DicomTag.SmallestImagePixelValue, out int smallest) &&
                dataset.TryGetSingleValue(DicomTag.LargestImagePixelValue, out int largest)
                && smallest < largest)
            {
                // If dataset contains valid SmallesImagePixelValue and LargesImagePixelValue attributes, use range to calculate
                // WindowWidth and WindowCenter
                grayscaleRenderOptions = FromImagePixelValueTags(dataset);
            }
            else
            {
                // If reached here, minimum and maximum pixel values calculated from pixels data to calculate
                // WindowWidth and WindowCenter
                grayscaleRenderOptions = FromMinMax(dataset);
            }

            /*
                David Clunie in comp.protocols.dicom (2000-12-13)
                https://groups.google.com/g/comp.protocols.dicom/c/UBxhOZ2anJ0/m/D0R_QP8V2wIJ
                --------------------------------------------------
                
                Modality LUTs in XA and XRF objects are totally screwy and do not follow the normal rules. 
                [...]
                A Modality LUT may be included with the image to allow it to be scaled back to its proportional value to X-Ray beam intensity. 
                In other words, for the objects that use this module (XA and XRF), the Modality LUT is used BACKWARDS. 
                It is used to convert stored pixels to X-Ray beam intensity space, but it is NOT APPLIED to stored pixels for the purpose of display 
                (or more specifically prior to application of the VOI LUT Module attributes to the stored pixel data).
            */
            if (grayscaleRenderOptions.ModalityLUT != null
                && dataset.TryGetSingleValue(DicomTag.SOPClassUID, out DicomUID sopClassUID)
                && (sopClassUID == DicomUID.XRayAngiographicImageStorage
                || sopClassUID == DicomUID.XRayRadiofluoroscopicImageStorage
                || sopClassUID == DicomUID.XRayAngiographicBiPlaneImageStorageRETIRED))
            {
                grayscaleRenderOptions.ModalityLUT = null;
            }

            return grayscaleRenderOptions;
        }

        /// <summary>
        /// Create grayscale render options based on window level data.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which render options should be obtained.</param>
        /// <param name="frame">Zero-based frame index. Use 0 in case of a single-framed image.</param>
        /// <returns>Grayscale render options based on window level data.</returns>
        public static GrayscaleRenderOptions FromWindowLevel(DicomDataset dataset, int frame = 0)
        {
            if (!dataset.TryGetValue(DicomTag.WindowWidth, 0, out double windowWidth) ||
                !dataset.TryGetValue(DicomTag.WindowCenter, 0, out double windowCenter))
            {
                return null;
            }

            var functional = dataset.FunctionalGroupValues(frame);
            var bits = BitDepth.FromDataset(dataset);

            // #1891 VOI LUT Function with empty value causes a crash
            if (!functional.TryGetValue(DicomTag.VOILUTFunction, 0, out string voiLutFunction)
            && !dataset.TryGetValue(DicomTag.VOILUTFunction, 0, out voiLutFunction))
            {
                voiLutFunction = "LINEAR";
            }

            // #1905 LINEAR function requires a windowWidth of >= 1. So if windowWidth is lower, then LINEAR_EXACT has to be applied
            if (windowWidth < 1.0 && voiLutFunction == "LINEAR")
            {
                voiLutFunction = "LINEAR_EXACT";
            }

            var options = new GrayscaleRenderOptions(bits)
            {
                WindowWidth = windowWidth,
                WindowCenter = windowCenter,
                VOILUTFunction = voiLutFunction,
                ColorMap = GetColorMap(dataset)
            };

            if (dataset.TryGetNonEmptySequence(DicomTag.ModalityLUTSequence, out DicomSequence modalityLutSequence))
            {
                options.ModalityLUT = new ModalitySequenceLUT(modalityLutSequence.First(), bits.IsSigned);
                options.RescaleSlope = 1.0;
                options.RescaleIntercept = 0.0;
            }
            else
            {
                options.RescaleSlope = dataset.TryGetSingleValue<double>(DicomTag.RescaleSlope, out var slope)
                                       || functional.TryGetSingleValue<double>(DicomTag.RescaleSlope, out slope)
                    ? slope
                    : 1.0;
                options.RescaleIntercept = dataset.TryGetSingleValue<double>(DicomTag.RescaleIntercept, out var intercept)
                                           || functional.TryGetSingleValue(DicomTag.RescaleIntercept, out intercept)
                    ? intercept
                    : 0.0;
            }

            if (dataset.TryGetNonEmptySequence(DicomTag.VOILUTSequence, out DicomSequence voiLutSequence))
            {
                options.VOILUTSequence = voiLutSequence;
                options.UseVOILUT = true;
            }

            return options;
        }

        /// <summary>
        /// Create grayscale render options based on window level data stored in functional groups in enhanced multiframe images
        /// </summary>
        /// <param name="dataset">DICOM dataset from which render options should be obtained.</param>
        /// <param name="frame">Zero-based frame index. Use 0 in case of a single-framed image.</param>
        /// <returns>Grayscale render options based on window level data.</returns>
        public static GrayscaleRenderOptions FromFunctionalWindowLevel(DicomDataset dataset, int frame)
        {
            var functional = dataset.FunctionalGroupValues(frame);
            if (!functional.Any() || !functional.Contains(DicomTag.WindowWidth) ||
               !functional.Contains(DicomTag.WindowCenter))
            {
                return null;
            }

            if (!functional.TryGetValue(DicomTag.WindowWidth, 0, out double windowWidth) ||
                !functional.TryGetValue(DicomTag.WindowCenter, 0, out double windowCenter))
            {
                return null;
            }

            // #1891 VOI LUT Function with empty value causes a crash
            if (!functional.TryGetValue(DicomTag.VOILUTFunction, 0, out string voiLutFunction)
            && !dataset.TryGetValue(DicomTag.VOILUTFunction, 0, out voiLutFunction))
            {
                voiLutFunction = "LINEAR";
            }

            // #1905 LINEAR function requires a windowWidth of >= 1. So if windowWidth is lower, then LINEAR_EXACT has to be applied
            if (windowWidth < 1.0 && voiLutFunction == "LINEAR")
            {
                voiLutFunction = "LINEAR_EXACT";
            }

            var bits = BitDepth.FromDataset(dataset);
            var options = new GrayscaleRenderOptions(bits)
            {
                WindowWidth = windowWidth,
                WindowCenter = windowCenter,
                VOILUTFunction = voiLutFunction,
                ColorMap = GetColorMap(dataset)
            };

            if (dataset.TryGetNonEmptySequence(DicomTag.ModalityLUTSequence, out DicomSequence modalityLutSequence))
            {
                options.ModalityLUT = new ModalitySequenceLUT(modalityLutSequence.First(), bits.IsSigned);
                options.RescaleSlope = 1.0;
                options.RescaleIntercept = 0.0;
            }
            else
            {
                options.RescaleSlope = dataset.TryGetSingleValue<double>(DicomTag.RescaleSlope, out var slope)
                               || functional.TryGetSingleValue<double>(DicomTag.RescaleSlope, out slope)
                    ? slope
                    : 1.0;
                options.RescaleIntercept = dataset.TryGetSingleValue<double>(DicomTag.RescaleIntercept, out var intercept)
                                   || functional.TryGetSingleValue(DicomTag.RescaleIntercept, out intercept)
                    ? intercept
                    : 0.0;
            }

            if (dataset.TryGetNonEmptySequence(DicomTag.VOILUTSequence, out DicomSequence voiLutSequence))
            {
                options.VOILUTSequence = voiLutSequence;
                options.UseVOILUT = true;
            }

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
            {
                return null;
            }

            var bits = BitDepth.FromDataset(dataset);
            var options = new GrayscaleRenderOptions(bits);

            int smallValue = dataset.GetSingleValue<int>(DicomTag.SmallestImagePixelValue);
            int largeValue = dataset.GetSingleValue<int>(DicomTag.LargestImagePixelValue);

            if (smallValue >= largeValue)
            {
                throw new DicomImagingException(
                    $"Smallest Image Pixel Value ({smallValue}) > Largest Value ({largeValue})");
            }

            options.WindowWidth = Math.Abs(largeValue - smallValue);
            options.WindowCenter = (largeValue + smallValue) / 2.0;

            options.VOILUTFunction = dataset.GetSingleValueOrDefault(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            if (dataset.TryGetNonEmptySequence(DicomTag.ModalityLUTSequence, out DicomSequence modalityLutSequence))
            {
                options.ModalityLUT = new ModalitySequenceLUT(modalityLutSequence.First(), bits.IsSigned);
                options.RescaleSlope = 1.0;
                options.RescaleIntercept = 0.0;
            }
            else
            {
                options.RescaleSlope = dataset.GetSingleValueOrDefault(DicomTag.RescaleSlope, 1.0);
                options.RescaleIntercept = dataset.GetSingleValueOrDefault(DicomTag.RescaleIntercept, 0.0);
            }

            if (dataset.TryGetNonEmptySequence(DicomTag.VOILUTSequence, out DicomSequence voiLutSequence))
            {
                options.VOILUTSequence = voiLutSequence;
                options.UseVOILUT = true;
            }

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

            int padding = dataset.GetValueOrDefault(DicomTag.PixelPaddingValue, 0, int.MinValue);

            if (bits.BitsStored == 1)
            {
                options.WindowWidth = 1;
                options.WindowCenter = 1;
                options.VOILUTFunction = "LINEAR";
                options.RescaleSlope = 1.0;
                options.RescaleIntercept = 0.0;
            }
            else
            {
                var transcoder = new DicomTranscoder(
                    dataset.InternalTransferSyntax,
                    DicomTransferSyntax.ExplicitVRLittleEndian);

                var pixels = transcoder.DecodePixelData(dataset, 0);
                var range = pixels.GetMinMax(padding);

                if (range.Minimum < bits.MinimumValue || range.Minimum == double.MaxValue)
                {
                    range.Minimum = bits.MinimumValue;
                }
                if (range.Maximum > bits.MaximumValue || range.Maximum == double.MinValue)
                {
                    range.Maximum = bits.MaximumValue;
                }

                var min = range.Minimum;
                var max = range.Maximum;
                if (dataset.TryGetNonEmptySequence(DicomTag.ModalityLUTSequence, out DicomSequence modalityLutSequence))
                {
                    options.ModalityLUT = new ModalitySequenceLUT(modalityLutSequence.Items[0], bits.IsSigned);
                    options.RescaleSlope = 1.0;
                    options.RescaleIntercept = 0.0;
                    // if there is a modalityLUT sequence, then the values have to be mapped
                    min = options.ModalityLUT[min];
                    max = options.ModalityLUT[max];
                }
                else
                {
                    // no modalityLUT sequence, so apply rescale slope and intercept
                    options.RescaleSlope = dataset.GetSingleValueOrDefault(DicomTag.RescaleSlope, 1.0);
                    options.RescaleIntercept = dataset.GetSingleValueOrDefault(DicomTag.RescaleIntercept, 0.0);
                    min = min * options.RescaleSlope + options.RescaleIntercept;
                    max = max * options.RescaleSlope + options.RescaleIntercept;
                }

                options.WindowWidth = Math.Max(1, Math.Abs(max - min));
                options.WindowCenter = (max + min) / 2.0;

                options.VOILUTFunction = dataset.GetSingleValueOrDefault(DicomTag.VOILUTFunction, "LINEAR");
            }

            options.ColorMap = GetColorMap(dataset);

            if (dataset.TryGetNonEmptySequence(DicomTag.VOILUTSequence, out DicomSequence voiLutSequence))
            {
                options.VOILUTSequence = voiLutSequence;
                options.UseVOILUT = true;
            }

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

            double min;
            double max;
            if (dataset.TryGetNonEmptySequence(DicomTag.ModalityLUTSequence, out DicomSequence modalityLutSequence))
            {
                options.ModalityLUT = new ModalitySequenceLUT(modalityLutSequence.Items[0], bits.IsSigned);
                options.RescaleSlope = 1.0;
                options.RescaleIntercept = 0.0;
                // if there is a modalityLUT sequence, then we can get the values from the LUT itself
                min = options.ModalityLUT.MinimumOutputValue;
                max = options.ModalityLUT.MaximumOutputValue;
            }
            else
            {
                // no modalityLUT sequence, so apply rescale slope and intercept
                options.RescaleSlope = dataset.GetSingleValueOrDefault(DicomTag.RescaleSlope, 1.0);
                options.RescaleIntercept = dataset.GetSingleValueOrDefault(DicomTag.RescaleIntercept, 0.0);
                min = bits.MinimumValue * options.RescaleSlope + options.RescaleIntercept;
                max = bits.MaximumValue * options.RescaleSlope + options.RescaleIntercept;
            }

            options.WindowWidth = Math.Abs(max - min);
            options.WindowCenter = (max + min) / 2.0;

            options.VOILUTFunction = dataset.GetSingleValueOrDefault(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            if (dataset.TryGetNonEmptySequence(DicomTag.VOILUTSequence, out DicomSequence voiLutSequence))
            {
                options.VOILUTSequence = voiLutSequence;
            }

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

            var transcoder = new DicomTranscoder(
                dataset.InternalTransferSyntax,
                DicomTransferSyntax.ExplicitVRLittleEndian);

            var pixels = transcoder.DecodePixelData(dataset, 0);
            var histogram = pixels.GetHistogram(0);

            if (dataset.TryGetValue(DicomTag.PixelPaddingValue, 0, out int padding))
            {
                histogram.Clear(padding);
            }

            histogram.ApplyWindow(percent);

            double min = histogram.WindowStart;
            double max = histogram.WindowEnd;
            
            if (dataset.TryGetNonEmptySequence(DicomTag.ModalityLUTSequence, out DicomSequence modalityLutSequence))
            {
                options.ModalityLUT = new ModalitySequenceLUT(modalityLutSequence.First(), bits.IsSigned);
                options.RescaleSlope = 1.0;
                options.RescaleIntercept = 0.0;
                // if there is a modalityLUT sequence, then the values have to be mapped
                min = options.ModalityLUT[histogram.WindowStart];
                max = options.ModalityLUT[histogram.WindowEnd];
            }
            else
            {
                options.RescaleSlope = dataset.GetSingleValueOrDefault(DicomTag.RescaleSlope, 1.0);
                options.RescaleIntercept = dataset.GetSingleValueOrDefault(DicomTag.RescaleIntercept, 0.0);
                min = min * options.RescaleSlope + options.RescaleIntercept;
                max = max * options.RescaleSlope + options.RescaleIntercept;
            }

            options.WindowWidth = Math.Abs(max - min);
            options.WindowCenter = (max + min) / 2.0;

            options.VOILUTFunction = dataset.GetSingleValueOrDefault(DicomTag.VOILUTFunction, "LINEAR");
            options.ColorMap = GetColorMap(dataset);

            if (dataset.TryGetNonEmptySequence(DicomTag.VOILUTSequence, out DicomSequence voiLutSequence))
            {
                options.VOILUTSequence = voiLutSequence;
            }

            return options;
        }

        /// <summary>
        /// Get grayscale color map based on Photometric Interpretation.
        /// </summary>
        /// <param name="dataset">DICOM dataset from which Photometric Interpretation should be obtained.</param>
        /// <returns>Color map associated with the identified Photometric Interpretation.</returns>
        private static Color32[] GetColorMap(DicomDataset dataset)
        {
            return dataset.GetSingleValueOrDefault<PhotometricInterpretation>(DicomTag.PhotometricInterpretation, null)
                   == PhotometricInterpretation.Monochrome1
                       ? ColorTable.Monochrome1
                       : ColorTable.Monochrome2;
        }

        public static GrayscaleRenderOptions CreateLinearOption(BitDepth bits, double minValue, double maxValue)
            => new GrayscaleRenderOptions(bits)
            {
                WindowWidth = maxValue - minValue,
                WindowCenter = (maxValue + minValue) / 2
            };

        #endregion
    }
}
