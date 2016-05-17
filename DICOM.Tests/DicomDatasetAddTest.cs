// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System.Linq;

    using Dicom.IO.Buffer;

    using Xunit;

    [Collection("General")]
    public class DicomDatasetAddTest
    {
        #region Unit tests

        [Fact]
        public void DicomSignedShortTest()
        {
            short[] values = new short[] { 5 }; //single Value element
            DicomSignedShort element = new DicomSignedShort(DicomTag.TagAngleSecondAxis, values);

            TestAddElementToDatasetAsString<short>(element, values);

            values = new short[] { 5, 8 }; //multi-value element
            element = new DicomSignedShort(DicomTag.CenterOfCircularExposureControlSensingRegion, values);

            TestAddElementToDatasetAsString<short>(element, values);
        }

        [Fact]
        public void DicomAttributeTagTest()
        {
            var expected = new DicomTag[] { DicomTag.ALinePixelSpacing }; //single value
            DicomElement element = new DicomAttributeTag(DicomTag.DimensionIndexPointer, expected);


            TestAddElementToDatasetAsString<string>(element, expected.Select(n => n.ToString("J", null)).ToArray());

            expected = new DicomTag[] { DicomTag.ALinePixelSpacing, DicomTag.AccessionNumber }; //multi-value
            element = new DicomAttributeTag(DicomTag.FrameIncrementPointer, expected);

            TestAddElementToDatasetAsString(element, expected.Select(n => n.ToString("J", null)).ToArray());
        }

        [Fact]
        public void DicomUnsignedShortTest()
        {
            ushort[] testValues = new ushort[] { 1, 2, 3, 4, 5 };

            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumbers, testValues);

            TestAddElementToDatasetAsString<ushort>(element, testValues);
        }

        [Fact]
        public void DicomSignedLongTest()
        {
            var testValues = new int[] { 0, 1, 2 };
            var element = new DicomSignedLong(DicomTag.ReferencePixelX0, testValues);

            TestAddElementToDatasetAsString(element, testValues);
        }

        [Fact]
        public void DicomOtherDoubleTest()
        {
            var testValues = new double[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };

            var element = new DicomOtherDouble(DicomTag.DoubleFloatPixelData, testValues);

            TestAddElementToDatasetAsByteBuffer<double>(element, testValues);
        }

        [Fact]
        public void DicomOtherByteTest()
        {
            var testValues = new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 };

            var element = new DicomOtherByte(DicomTag.PixelData, testValues);

            TestAddElementToDatasetAsByteBuffer(element, testValues);
        }
        #endregion

        #region Support methods

        private void TestAddElementToDatasetAsString<T>(DicomElement element, T[] testValues)
        {
            DicomDataset ds = new DicomDataset();
            string[] stringValues;


            if (typeof(T) == typeof(string))
            {
                stringValues = testValues.Cast<string>().ToArray();
            }
            else
            {
                stringValues = testValues.Select(x => x.ToString()).ToArray();
            }


            ds.Add<string>(element.Tag, stringValues);


            for (int index = 0; index < element.Count; index++)
            {
                string val;

                val = GetStringValue(element, ds, index);

                Assert.Equal(stringValues[index], val);
            }

            if (element.Tag.DictionaryEntry.ValueMultiplicity.Maximum > 1)
            {
                var stringValue = string.Join("\\", testValues);

                ds.Add<string>(element.Tag, stringValue);

                for (int index = 0; index < element.Count; index++)
                {
                    string val;

                    val = GetStringValue(element, ds, index);

                    Assert.Equal(stringValues[index], val);
                }
            }
        }

        private string GetStringValue(DicomElement element, DicomDataset ds, int index)
        {
            string val;


            if (element.ValueRepresentation == DicomVR.AT)
            {
                //Should this be a updated in the AT DicomTag?
                val = GetATElementValue(element, ds, index);
            }
            else
            {
                val = ds.Get<string>(element.Tag, index);
            }

            return val;
        }

        private static string GetATElementValue(DicomElement element, DicomDataset ds, int index)
        {
            var atElement = ds.Get<DicomElement>(element.Tag, null);

            var testValue = atElement.Get<DicomTag>(index);

            return testValue.ToString("J", null);
        }

        private void TestAddElementToDatasetAsByteBuffer<T>(DicomElement element, T[] testValues)
        {
            DicomDataset ds = new DicomDataset();


            ds.Add<IByteBuffer>(element.Tag, element.Buffer);

            for (int index = 0; index < testValues.Count(); index++)
            {
                Assert.Equal(testValues[index], ds.Get<T>(element.Tag, index));
            }
        }

        #endregion


    }
}
