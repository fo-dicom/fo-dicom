// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using System;

namespace FellowOakDicom.Imaging.LUT
{

    /// <summary>
    /// Modality Sequence LUT implementation of <see cref="IModalityLUT"/> and <see cref="ILUT"/>
    /// </summary>
    public class ModalitySequenceLUT : IModalityLUT
    {
        #region Private Members

        private readonly DicomDataset _modalityLUTItem;

        private int _nrOfEntries;

        private int _firstInputValue;

        private int _nrOfBitsPerEntry;

        private int[] _LUTDataArray;

        private bool _signed;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="ModalitySequenceLUT"/> using the specified Modality LUT Descriptor and Data
        /// </summary>
        /// <param name="modalityLUTSequenceItem">One item of the ModalityLUTSequence</param>
        public ModalitySequenceLUT(DicomDataset modalityLUTSequenceItem, bool signed)
        {
            _signed = signed;
            _modalityLUTItem = modalityLUTSequenceItem;
            Recalculate();
        }

        #endregion

        #region Public Properties

        public bool IsValid => false; //always recalculate

        public double MinimumOutputValue => _LUTDataArray[0];

        public double MaximumOutputValue => _LUTDataArray[_nrOfEntries - 1];

        public double this[double value]
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
                        return _LUTDataArray[unchecked((int)(value - _firstInputValue))];
                }
            }

        }

        #endregion

        #region Public Methods

        public void Recalculate()
        {
            GetLUTDescriptor();

            var LUTDataElement = _modalityLUTItem.GetDicomItem<DicomElement>(DicomTag.LUTData);
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
            var lutDescriptorElement = _modalityLUTItem.GetDicomItem<DicomElement>(DicomTag.LUTDescriptor);
            if (_signed)
            {
                var signedLutDescriptor = new DicomSignedShort(lutDescriptorElement.Tag, lutDescriptorElement.Buffer);
                _nrOfEntries = Math.Abs(signedLutDescriptor.Get<int>(0)); //Sometimes negative number is defined and this makes no sense
                _firstInputValue = signedLutDescriptor.Get<int>(1);
                _nrOfBitsPerEntry = signedLutDescriptor.Get<int>(2);
            }
            else
            {
                var unsignedLutDescriptor = new DicomUnsignedShort(lutDescriptorElement.Tag, lutDescriptorElement.Buffer);
                _nrOfEntries = unsignedLutDescriptor.Get<int>(0);
                _firstInputValue = unsignedLutDescriptor.Get<int>(1);
                _nrOfBitsPerEntry = unsignedLutDescriptor.Get<int>(2);
            }
        }

        //Implementation of Array.ConvertAll()
        //Since .NET Core doesn't know Array.ConvertAll
        private TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Func<TInput, TOutput> converter)
        {
            array = array ?? throw new ArgumentNullException(nameof (array));
            converter = converter ?? throw new ArgumentNullException(nameof (converter));

            var outputArray = new TOutput[array.Length];
            for (int index = 0; index < array.Length; ++index)
            {
                outputArray[index] = converter(array[index]);
            }

            return outputArray;
        }

        #endregion
    }
}
