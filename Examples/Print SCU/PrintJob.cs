using Dicom;
using Dicom.Imaging;
using Dicom.IO;
using Dicom.Network;
using Dicom.Printing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print_SCU
{
    class PrintJob
    {
        public string CallingAE { get; set; }
        public string CalledAE { get; set; }
        public string RemoteAddress { get; set; }
        public int RemotePort { get; set; }

        public FilmSession FilmSession { get; private set; }

        private FilmBox _currentFilmBox;
        public PrintJob(string jobLabel)
        {
            FilmSession = new FilmSession(DicomUID.BasicFilmSessionSOPClass)
            {
                FilmSessionLabel = jobLabel,
                MediumType = "PAPER",
                NumberOfCopies = 1
            };
        }

        public FilmBox StartFilmBox(string format, string orientation, string filmSize)
        {
            var filmBox = new FilmBox(FilmSession, null, DicomTransferSyntax.ExplicitVRLittleEndian)
            {
                ImageDisplayFormat = format,
                FilmOrienation = orientation,
                FilmSizeID = filmSize,
                MagnificationType = "NONE",
                BorderDensity = "BLACK",
                EmptyImageDensity = "BLACK"
            };

            filmBox.Initialize();
            FilmSession.BasicFilmBoxes.Add(filmBox);

            _currentFilmBox = filmBox;
            return filmBox;
        }

        public void AddImage(Bitmap bitmap, int index)
        {
            if(FilmSession.IsColor)
            {
                AddColorImage(bitmap, index);
            }
            else
            {
                AddGreyscaleImage(bitmap, index);
            }
        }
        private void AddGreyscaleImage(Bitmap bitmap, int index)
        {
            if (_currentFilmBox == null)
            {
                throw new InvalidOperationException("Start film box first!");
            }
            if (index < 0 || index > _currentFilmBox.BasicImageBoxes.Count)
            {
                throw new ArgumentOutOfRangeException("Image box index out of range");
            }

            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb &&
               bitmap.PixelFormat != PixelFormat.Format32bppArgb &&
               bitmap.PixelFormat != PixelFormat.Format32bppRgb
              )
            {
                throw new ArgumentException("Not supported bitmap format");
            }

            var dataset = new DicomDataset();
            dataset.Add<ushort>(DicomTag.Columns, (ushort)bitmap.Width)
                   .Add<ushort>(DicomTag.Rows, (ushort)bitmap.Height)
                   .Add<ushort>(DicomTag.BitsAllocated, 8)
                   .Add<ushort>(DicomTag.BitsStored, 8)
                   .Add<ushort>(DicomTag.HighBit, 7)
                   .Add(DicomTag.PixelRepresentation, (ushort)PixelRepresentation.Unsigned)
                   .Add(DicomTag.PlanarConfiguration, (ushort)PlanarConfiguration.Interleaved)
                   .Add<ushort>(DicomTag.SamplesPerPixel, 1)
                   .Add(DicomTag.PhotometricInterpretation, PhotometricInterpretation.Monochrome2.Value);

            var pixelData = DicomPixelData.Create(dataset, true);

            var pixels = GetGreyBytes(bitmap);
            var buffer = new Dicom.IO.Buffer.MemoryByteBuffer(pixels.Data);

            pixelData.AddFrame(buffer);

            var imageBox = _currentFilmBox.BasicImageBoxes[index];
            imageBox.ImageSequence = dataset;

            pixels.Dispose();
        }

        private void AddColorImage(Bitmap bitmap, int index)
        {
            if (_currentFilmBox == null)
            {
                throw new InvalidOperationException("Start film box first!");
            }
            if (index < 0 || index > _currentFilmBox.BasicImageBoxes.Count)
            {
                throw new ArgumentOutOfRangeException("Image box index out of range");
            }

            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb &&
               bitmap.PixelFormat != PixelFormat.Format32bppArgb &&
               bitmap.PixelFormat != PixelFormat.Format32bppRgb
              )
            {
                throw new ArgumentException("Not supported bitmap format");
            }

            var dataset = new DicomDataset();
            dataset.Add<ushort>(DicomTag.Columns, (ushort)bitmap.Width)
                   .Add<ushort>(DicomTag.Rows, (ushort)bitmap.Height)
                   .Add<ushort>(DicomTag.BitsAllocated, 8)
                   .Add<ushort>(DicomTag.BitsStored, 8)
                   .Add<ushort>(DicomTag.HighBit, 7)
                   .Add(DicomTag.PixelRepresentation, (ushort)PixelRepresentation.Unsigned)
                   .Add(DicomTag.PlanarConfiguration, (ushort)PlanarConfiguration.Interleaved)
                   .Add<ushort>(DicomTag.SamplesPerPixel, 3)
                   .Add(DicomTag.PhotometricInterpretation, PhotometricInterpretation.Rgb.Value);

            var pixelData = DicomPixelData.Create(dataset, true);

            var pixels = GetColorbytes(bitmap);
            var buffer = new Dicom.IO.Buffer.MemoryByteBuffer(pixels.Data);

            pixelData.AddFrame(buffer);

            var imageBox = _currentFilmBox.BasicImageBoxes[index];
            imageBox.ImageSequence = dataset;

            pixels.Dispose();
        }

        public void EndFilmBox()
        {
            _currentFilmBox = null;
        }

        public void Print()
        {
            var dicomClient = new DicomClient();
            dicomClient.AddRequest(new DicomNCreateRequest(FilmSession.SOPClassUID, FilmSession.SOPInstanceUID, 0)
            {
                Dataset = FilmSession
            });


            foreach (var filmbox in FilmSession.BasicFilmBoxes)
            {

                var imageBoxRequests = new List<DicomNSetRequest>();

                var filmBoxRequest = new DicomNCreateRequest(FilmBox.SOPClassUID, filmbox.SOPInstanceUID, 0)
                {
                    Dataset = filmbox
                };
                filmBoxRequest.OnResponseReceived = (request, response) =>
                {
                    if (response.HasDataset)
                    {
                        var seq = response.Dataset.Get<DicomSequence>(DicomTag.ReferencedImageBoxSequence);
                        for (int i = 0; i < seq.Items.Count; i++)
                        {
                            var req = imageBoxRequests[i];
                            var imageBox = req.Dataset;
                            var sopInstanceUid = seq.Items[i].Get<string>(DicomTag.ReferencedSOPInstanceUID);
                            imageBox.Add(DicomTag.SOPInstanceUID, sopInstanceUid);
                            req.Command.Add(DicomTag.RequestedSOPInstanceUID, sopInstanceUid);
                        }
                    }
                };
                dicomClient.AddRequest(filmBoxRequest);



                foreach (var image in filmbox.BasicImageBoxes)
                {
                    var req = new DicomNSetRequest(image.SOPClassUID, image.SOPInstanceUID)
                    {
                        Dataset = image
                    };

                    imageBoxRequests.Add(req);
                    dicomClient.AddRequest(req);
                }
            }

            dicomClient.AddRequest(new DicomNActionRequest(FilmSession.SOPClassUID, FilmSession.SOPInstanceUID, 0x0001));

            dicomClient.Send(RemoteAddress, RemotePort, false, CallingAE, CalledAE);
        }


        private unsafe PinnedByteArray GetGreyBytes(Bitmap bitmap)
        {
            var pixels = new PinnedByteArray(bitmap.Width * bitmap.Height);

            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var srcComponents = bitmap.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;

            var dstLine = (byte*)pixels.Pointer;
            var srcLine = (byte*)data.Scan0.ToPointer();

            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {
                    var pixel = srcLine + j * srcComponents;
                    int grey = (int)(pixel[0] * 0.3 + pixel[1] * 0.59 + pixel[2] * 0.11);
                    dstLine[j] = (byte)grey;
                }

                srcLine += data.Stride;
                dstLine += data.Width;
            }
            bitmap.UnlockBits(data);

            return pixels;
        }

        private unsafe PinnedByteArray GetColorbytes(Bitmap bitmap)
        {
            var pixels = new PinnedByteArray(bitmap.Width * bitmap.Height * 3);

            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var srcComponents = bitmap.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;

            var dstLine = (byte*)pixels.Pointer;
            var srcLine = (byte*)data.Scan0.ToPointer();

            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {
                    var srcPixel = srcLine + j * srcComponents;
                    var dstPixel = dstLine + j * 3;

                    //convert from bgr to rgb
                    dstPixel[0] = srcPixel[2];
                    dstPixel[1] = srcPixel[1];
                    dstPixel[2] = srcPixel[0];
                }

                srcLine += data.Stride;
                dstLine += data.Width * 3;
            }
            bitmap.UnlockBits(data);

            return pixels;
        }

    }
}
