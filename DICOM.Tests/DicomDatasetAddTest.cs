// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Dicom.IO.Buffer;

    using Xunit;

    [Collection("General")]
    public class DicomDatasetAddTest
    {
        #region Unit tests

        [Fact]
        public void DicomSignedShortTest ()
        {
            short[] values = new short [] { 5, 8 } ;
            DicomSignedShort element = new DicomSignedShort(DicomTag.TagAngleSecondAxis, values );
            
            TestAddElementToDatasetAsString<short> ( element, values ) ;
        }

        [Fact]
        public void DicomAttributeTagTest()
        {
            var expected = new DicomTag[] { DicomTag.ALinePixelSpacing};
            DicomElement element = new DicomAttributeTag(DicomTag.DimensionIndexPointer, expected);

            TestAddElementToDatasetAsString(element, expected);
        }

        [Fact]
        public void DicomUnsignedShortTest ()
        {
            ushort[] testValues = new ushort[] {1, 2, 3, 4, 5} ;

            var element = new DicomUnsignedShort(DicomTag.ReferencedFrameNumbers, testValues);

            TestAddElementToDatasetAsString<ushort> (element, testValues) ;
        }

        [Fact]
        public void DicomSignedLongTest ()
        {
            var testValues = new int[] {0,1,2} ;
            var element = new DicomSignedLong(DicomTag.ReferencePixelX0, testValues);
            
            TestAddElementToDatasetAsString (element, testValues ) ;
        }

        [Fact]
        public void DicomOtherDoubleTest()
        {
            var testValues = new double[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 } ;

            var element = new DicomOtherDouble(DicomTag.DoubleFloatPixelData, testValues );
            
            TestAddElementToDatasetAsByteBuffer<double> ( element, testValues ) ;
            TestAddElementToDatasetAsString<double>(element,testValues) ;
        }
        
        [Fact]
        public void DicomOtherByteTest ( ) 
        {
            var testValues = new byte[] { 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80 } ;

            var element = new DicomOtherByte(DicomTag.PixelData, testValues );
            
            TestAddElementToDatasetAsByteBuffer<byte> ( element, testValues ) ;
        }
        #endregion

        #region Support methods

        private void TestAddElementToDatasetAsString<T>(DicomElement element, T[] testValues )
        {
            DicomDataset ds = new DicomDataset ( ) ;
            var stringValues = testValues.Select ( x=>x.ToString());


            ds.Add<string> (element.Tag, stringValues.ToArray ( ) ) ;

            for (int index = 0; index < element.Count; index++ )
            {
                Assert.Equal(testValues[index], ds.Get<T> ( element.Tag, index )) ;
            }
        }

        private void TestAddElementToDatasetAsByteBuffer<T>(DicomElement element, T[] testValues )
        {
            DicomDataset ds = new DicomDataset ( ) ;


            ds.Add<IByteBuffer> (element.Tag, element.Buffer) ;

            for ( int index = 0; index < testValues.Count ( ); index++ )
            {
                Assert.Equal(testValues[index], ds.Get<T> ( element.Tag, index )) ;            
            }
        }
                
        #endregion

        
    }
}