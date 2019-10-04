// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using FellowOakDicom.IO;
using System;

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Modality Sequence LUT implementation of <seealso cref="IModalityLUT"/> and <seealso cref="ILUT"/>
    /// </summary>
    public class ModalitySequenceLUT : IModalityLUT
    {
        #region Private Members

        private GrayscaleRenderOptions _renderOptions;

        private int _nrOfEntries;

        private int _firstInputValue;

        private int _nrOfBitsPerEntry;

        private int[] _LUTDataArray;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <seealso cref="Dicom.Imaging.LUT.ModalitySequenceLUT"/> using the specified Modality LUT Descriptor and Data
        /// </summary>
        /// <param name="options">Render options</param>
        public ModalitySequenceLUT(GrayscaleRenderOptions options)
        {
            _renderOptions = options;
            Recalculate();
        }

        #endregion

        #region Public Properties

        public bool IsValid => false; //always recalculate

        public int MinimumOutputValue => _LUTDataArray[0];

        public int MaximumOutputValue => _LUTDataArray[_nrOfEntries - 1];

        public int this[int value]
        {
            get
            {
                unchecked
                {
                    if (value < _firstInputValue)
                        return _LUTDataArray[0];
                    else if (value > (_firstInputValue + _nrOfEntries - 1))
                        return _LUTDataArray[_nrOfEntries - 1];
                    else
                        return _LUTDataArray[value - _firstInputValue];
                }
            }

        }

        #endregion

        #region Public Methods

        public void Recalculate()
        {
            GetLUTDescriptor();

            var LUTDataElement = _renderOptions.ModalityLUTSequence.Items[0].GetDicomItem<DicomElement>(DicomTag.LUTData);
            switch (LUTDataElement.ValueRepresentation.Code)
            {
                case "OW":
                {
                    var LUTData = LUTDataElement as DicomOtherWord;
                    _LUTDataArray = ConvertAll(ByteConverter.ToArray<ushort>(LUTData.Buffer), x => (int)x);
                    break;
                }
                case "US":
                {
                    var LUTData = LUTDataElement as DicomUnsignedShort;
                    _LUTDataArray = ConvertAll(ByteConverter.ToArray<ushort>(LUTData.Buffer), x => (int)x);
                    break;
                }
                case "SS":
                {
                    var LUTData = LUTDataElement as DicomSignedShort;
                    _LUTDataArray = ConvertAll(ByteConverter.ToArray<short>(LUTData.Buffer), x => (int)x);
                    break;
                }
            }
        }

        #endregion

        #region Private Methods

        private void GetLUTDescriptor()
        {
            var lutDescriptorElement = _renderOptions.ModalityLUTSequence.Items[0].GetDicomItem<DicomElement>(DicomTag.LUTDescriptor);
            if (lutDescriptorElement.ValueRepresentation.Code == "SS")
            {
                var LUTDescriptor = lutDescriptorElement as DicomSignedShort;
                _nrOfEntries = Math.Abs(LUTDescriptor.Get<int>(0)); //Sometimes negative number is defined and this makes no sense
                _firstInputValue = LUTDescriptor.Get<int>(1);
                _nrOfBitsPerEntry = LUTDescriptor.Get<int>(2);
            }
            else
            {
                var LUTDescriptor = lutDescriptorElement as DicomUnsignedShort;
                _nrOfEntries = LUTDescriptor.Get<int>(0);
                _firstInputValue = LUTDescriptor.Get<int>(1);
                _nrOfBitsPerEntry = LUTDescriptor.Get<int>(2);
            }
        }

        //Implementation of Array.ConvertAll()
        //Since .NET Core doesn't know Array.ConvertAll
        private TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Func<TInput, TOutput> converter)
        {
            if (array == null)
                throw new ArgumentNullException(nameof (array));
            if (converter == null)
                throw new ArgumentNullException(nameof (converter));
            TOutput[] outputArray = new TOutput[array.Length];
            for (int index = 0; index < array.Length; ++index)
                outputArray[index] = converter(array[index]);
            return outputArray;
        }

        #endregion
    }
}
