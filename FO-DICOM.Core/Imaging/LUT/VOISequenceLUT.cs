// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO;
using System;

namespace FellowOakDicom.Imaging.LUT
{

    public class VOISequenceLUT : ILUT
    {
        private readonly DicomDataset _voiLUTItem;

        private int _nrOfEntries;

        private int _firstInputValue;

        private int _nrOfBitsPerEntry;

        private int[] _LUTDataArray;

        #region Public Constructors

        /// <summary>
        /// Initialize new instance of <see cref="VOISequenceLUT"/> using the specified VOI LUT Descriptor and Data
        /// </summary>
        /// <param name="options">Render options</param>
        public VOISequenceLUT(DicomDataset voiLUTSequenceItem)
        {
            _voiLUTItem = voiLUTSequenceItem;
            Recalculate();
        }

        #endregion

        #region Public Properties

        public bool IsValid => false; //always recalculate


        public double MinimumOutputValue => _LUTDataArray[0];

        public double MaximumOutputValue => _LUTDataArray[_nrOfEntries - 1];

        public int NumberOfBitsPerEntry => _nrOfBitsPerEntry;

        public int NumberOfEntries => _nrOfEntries;

        public double this[double value]
        {
            get
            {
                unchecked
                {
                    if (value < _firstInputValue)
                    {
                        return _LUTDataArray[0];
                    }
                    else if (value > (_firstInputValue + _nrOfEntries - 1))
                    {
                        return _LUTDataArray[_nrOfEntries - 1];
                    }
                    else
                    {
                        return _LUTDataArray[(int)(value - _firstInputValue)];
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public void Recalculate()
        {
            GetLUTDescriptor();

            var lutDataElement = _voiLUTItem.GetDicomItem<DicomElement>(DicomTag.LUTData);
            switch (lutDataElement.ValueRepresentation.Code)
            {
                case "OW":
                {
                    var LUTData = lutDataElement as DicomOtherWord;
                    _LUTDataArray = ConvertAll(ByteConverter.ToArray<ushort>(LUTData.Buffer), x => (int)x);
                    break;
                }
                case "US":
                {
                    var LUTData = lutDataElement as DicomUnsignedShort;
                    _LUTDataArray = ConvertAll(ByteConverter.ToArray<ushort>(LUTData.Buffer), x => (int)x);
                    break;
                }
                case "SS":
                {
                    var LUTData = lutDataElement as DicomSignedShort;
                    _LUTDataArray = ConvertAll(ByteConverter.ToArray<short>(LUTData.Buffer), x => (int)x);
                    break;
                }
            }
            if (_LUTDataArray.Length < _nrOfEntries)
            {
                throw new DicomImagingException($"Number of entries in VOI LUT Sequence do not match");
            }
        }

        #endregion

        #region Private Methods

        private void GetLUTDescriptor()
        {
            var lutDescriptorElement = _voiLUTItem.GetDicomItem<DicomElement>(DicomTag.LUTDescriptor);
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
            // according to DICOM Standard Section C.11.2.1.1
            if (_nrOfEntries == 0)
            {
                _nrOfEntries = 1 << 16;
            }
        }

        //Implementation of Array.ConvertAll()
        //Since .NET Core doesn't know Array.ConvertAll
        private TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Func<TInput, TOutput> converter)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof (array));
            }

            if (converter == null)
            {
                throw new ArgumentNullException(nameof (converter));
            }

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
