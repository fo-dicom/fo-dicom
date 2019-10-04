// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Output LUT implementation of <seealso cref="ILUT"/> used to map grayscale images to RGB grays, or colorize grayscale 
    /// images using custom color map
    /// </summary>
    public class OutputLUT : ILUT
    {
        #region Private Members

        private readonly GrayscaleRenderOptions _options;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <seealso cref="OutputLUT"/> 
        /// </summary>
        /// <param name="options">The grayscale render options containing the grayscale color map.</param>
        public OutputLUT(GrayscaleRenderOptions options)
        {
            _options = options;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the color map
        /// </summary>
        public Color32[] ColorMap
        {
            get
            {
                return _options.ColorMap;
            }
        }

        /// <summary>
        /// Get the minimum output value
        /// </summary>
        public int MinimumOutputValue
        {
            get
            {
                return int.MinValue;
            }
        }

        /// <summary>
        /// Get the maximum output value
        /// </summary>
        public int MaximumOutputValue
        {
            get
            {
                return int.MaxValue;
            }
        }

        /// <summary>
        /// Returns true if the lookup table is valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this._options != null;
            }
        }

        /// <summary>
        /// Indexer to transform input value into output value
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>Output value</returns>
        public int this[int value]
        {
            get
            {
                unchecked
                {
                    if (value < 0) return _options.ColorMap[0].Value;
                    if (value > 255) return _options.ColorMap[255].Value;
                    return _options.ColorMap[value].Value;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Force the recalculation of LUT
        /// </summary>
        public void Recalculate()
        {
        }

        #endregion
    }
}
