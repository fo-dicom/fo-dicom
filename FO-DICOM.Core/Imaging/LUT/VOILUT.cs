// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Abstract VOI LUT implementation of <see cref="ILUT"/>
    /// </summary>
    public abstract class VOILUT : ILUT
    {
        #region Private Members

        private readonly GrayscaleRenderOptions _renderOptions;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="VOILUT"/>
        /// </summary>
        /// <param name="options">Render options</param>
        protected VOILUT(GrayscaleRenderOptions options)
        {
            _renderOptions = options;
            Recalculate();
        }

        #endregion

        #region Public Properties

        protected double WindowCenter { get; private set; }

        protected double WindowWidth { get; private set; }

        protected double WindowCenterMin05 { get; private set; }

        protected double WindowWidthMin1 { get; private set; }

        protected double WindowWidthDiv2 { get; private set; }

        protected int WindowStart { get; private set; }

        protected int WindowEnd { get; private set; }

        public double MinimumOutputValue => 0;

        public double MaximumOutputValue => 255;

        public int OutputRange => 255;

        public bool IsValid => false; // always recalculate

        public abstract double this[double value] { get; }

        #endregion

        #region Public Methods

        public void Recalculate()
        {
            if (_renderOptions.WindowWidth != WindowWidth || _renderOptions.WindowCenter != WindowCenter)
            {
                WindowWidth = _renderOptions.WindowWidth;
                WindowCenter = _renderOptions.WindowCenter;
                WindowCenterMin05 = WindowCenter - 0.5;
                WindowWidthMin1 = WindowWidth - 1;
                WindowWidthDiv2 = WindowWidthMin1 / 2;
                WindowStart = (int)(WindowCenterMin05 - WindowWidthDiv2);
                WindowEnd = (int)(WindowCenterMin05 + WindowWidthDiv2);
            }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Create a new VOILUT according to specifications in <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Render options.</param>
        /// <returns></returns>
        public static VOILUT Create(GrayscaleRenderOptions options)
        {
            switch (options.VOILUTFunction.ToUpperInvariant())
            {
                case "SIGMOID":
                    return new VOISigmoidLUT(options);
                case "LINEAR_EXACT": // DICOM C.11.2.1.3.2
                    return new VOILinearExactLUT(options);
                default:
                    break;
            }

            return new VOILinearLUT(options);
        }

        #endregion
    }

    /// <summary>
    /// LINEAR VOI LUT
    /// </summary>
    public class VOILinearLUT : VOILUT
    {

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="VOILinearLUT"/>
        /// </summary>
        /// <param name="options">Render options</param>
        public VOILinearLUT(GrayscaleRenderOptions options)
            : base(options)
        {
        }

        #endregion

        #region Public Properties

        public override double this[double value]
        {
            get
            {
                unchecked
                {
                    if (WindowWidth == 1)
                    {
                        return value < WindowCenterMin05 ? MinimumOutputValue : MaximumOutputValue;
                    }
                    else
                    {
                        return Math.Min(MaximumOutputValue,
                            Math.Max(MinimumOutputValue,
                            (((value - WindowCenterMin05) / WindowWidthMin1) + 0.5) * OutputRange + MinimumOutputValue
                            ));
                    }
                }
            }
        }

        #endregion
    }


    /// <summary>
    /// LINEAR_EXACT VOI LUT
    /// </summary>
    public class VOILinearExactLUT : VOILUT
    {

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="VOILinearLUT"/>
        /// </summary>
        /// <param name="options">Render options</param>
        public VOILinearExactLUT(GrayscaleRenderOptions options)
            : base(options)
        {
        }

        #endregion

        #region Public Properties

        public override double this[double value]
        {
            get
            {
                unchecked
                {
                    if (WindowWidth == 1)
                    {
                        return value < WindowCenterMin05 ? MinimumOutputValue : MaximumOutputValue;
                    }
                    else
                    {
                        return Math.Min(MaximumOutputValue,
                            Math.Max(MinimumOutputValue,
                            ((value - WindowCenter) / WindowWidth + 0.5) * OutputRange + MinimumOutputValue
                            ));
                    }
                }
            }
        }

        #endregion
    }


    /// <summary>
    /// SIGMOID VOI LUT
    /// </summary>
    public class VOISigmoidLUT : VOILUT
    {
        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="VOISigmoidLUT"/>
        /// </summary>
        /// <param name="options">Render options</param>
        public VOISigmoidLUT(GrayscaleRenderOptions options)
            : base(options)
        {
        }

        #endregion

        #region Public Properties

        public override double this[double value]
        {
            get
            {
                unchecked
                {
                    return 255.0 / (1.0 + Math.Exp(-4.0 * ((value - WindowCenter) / WindowWidth)));
                }
            }
        }

        #endregion
    }
}
