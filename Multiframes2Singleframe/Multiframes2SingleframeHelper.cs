using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dicom;
using Dicom.IO.Buffer;
using System.IO;
using Dicom.Imaging;

namespace Multiframes2Singleframe
{
    /// <summary>
    /// Try to extract each frame from Multi-frame dcm file, using the code from DicomImage class.
    /// Author: zssure@163.com
    /// Date:   20160805
    /// </summary>
    public class Multiframes2SingleframeHelper
    {
        public static List<string> ExtractMultiframes2Singleframe(string src)
        {
            List<string> destFiles = new List<string>();
            DicomFile dcmFile = DicomFile.Open(src);
            DicomDataset dcmDataset = dcmFile.Dataset;
            int frames = dcmFile.Dataset.Get<int>(DicomTag.NumberOfFrames);
            if (frames > 1)
            {
                for (int i = 0; i < frames; ++i)
                {
                    try
                    {
                        DicomFile tmpFile = dcmFile.Clone();
                        string seriesiuid=tmpFile.Dataset.Get<string>(DicomTag.SeriesInstanceUID,"1.2.3.4.5.6.7.8.9.2234");
                        tmpFile.Dataset.Remove(new DicomTag[] { DicomTag.InstanceNumber,DicomTag.NumberOfFrames, DicomTag.SOPInstanceUID, DicomTag.PixelData });
                        tmpFile.Dataset.Add<int>(DicomTag.NumberOfFrames, 1);
                        tmpFile.Dataset.Add<string>(DicomTag.SOPInstanceUID,string.Format("{0}.{1}",seriesiuid,i));
                        tmpFile.Dataset.Add<int>(DicomTag.InstanceNumber, i + 1);
                        IByteBuffer pixelData = ExtractSingleFrame(dcmDataset, i);
                        CreateAndAddPixelData(tmpFile.Dataset, pixelData);
                        string srcName = Path.GetFileName(src);
                        string tmpFileName = string.Format(@"{2}linkingmed.{1}.frame{0}.dcm", i, srcName.Split('.')[0], Path.GetTempPath());
                        tmpFile.Save(tmpFileName);
                        destFiles.Add(tmpFileName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("Errors:{0},Details:{1}",e.Message+e.StackTrace));
                    }

                }

            }
            return destFiles;
        }
        public static IByteBuffer ExtractSingleFrame(DicomDataset dataset, int frame)
        {
            DicomPixelData pixelData = DicomPixelData.Create(dataset);
            int frames = pixelData.NumberOfFrames;
            if (frame > frames)
                return null;
            else
            {
                var frameData = pixelData.GetFrame(frame);
                return frameData;

            }
        }
        private static void CreateAndAddPixelData(DicomDataset dataset, IByteBuffer pixelData)
        {
            var syntax = dataset.InternalTransferSyntax;
            if (syntax == DicomTransferSyntax.ImplicitVRLittleEndian)
            {
                var Element = new DicomOtherWord(DicomTag.PixelData, new CompositeByteBuffer());
                CompositeByteBuffer buffer = Element.Buffer as CompositeByteBuffer;
                buffer.Buffers.Add(pixelData);
                dataset.Add(Element);
                return;
            }
            if (syntax.IsEncapsulated)
            {
                var Element = new DicomOtherByteFragment(DicomTag.PixelData);
                long pos = Element.Fragments.Sum(x => (long)x.Size + 8);
                if (pos < uint.MaxValue)
                {
                    Element.OffsetTable.Add((uint)pos);
                }
                else
                {
                    // do not create an offset table for very large datasets
                    Element.OffsetTable.Clear();
                }

                pixelData = EndianByteBuffer.Create(pixelData, dataset.InternalTransferSyntax.Endian, dataset.Get<ushort>(DicomTag.BitsAllocated));
                Element.Fragments.Add(pixelData);
                dataset.Add(Element);
                return;
            }
            if (dataset.Get<ushort>(DicomTag.BitsAllocated) == 16)
            {
                var Element = new DicomOtherWord(DicomTag.PixelData, new CompositeByteBuffer());
                CompositeByteBuffer buffer = Element.Buffer as CompositeByteBuffer;
                buffer.Buffers.Add(pixelData);
                dataset.Add(Element);
            }
            else
            {
                var Element = new DicomOtherByte(DicomTag.PixelData, new CompositeByteBuffer());
                CompositeByteBuffer buffer = Element.Buffer as CompositeByteBuffer;
                buffer.Buffers.Add(pixelData);
                dataset.Add(Element);
            }
        }
    }
}
