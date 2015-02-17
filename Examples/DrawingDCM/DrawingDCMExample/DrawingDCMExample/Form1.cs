using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Dicom;
using Dicom.IO;
using Dicom.IO.Buffer;
using Dicom.Imaging;
using Dicom.Imaging.Codec;
using Dicom.Imaging.Codec.Jpeg;
using System.Runtime.InteropServices;


namespace DrawingDCMExample
{
    public partial class DICOMProcessDemo : Form
    {

        /// <summary>
        /// The Image member that inserted in PictureBox
        /// </summary>
        private string defaultImageBG;//the default background image
        public DICOMProcessDemo()
        {
            InitializeComponent();
            string appPath = Application.StartupPath;
            string imagePath = appPath + @"\..\..\..\..\" + @"Resource\defaultbackgroundImage.bmp";
            defaultImageBG = imagePath;
            pictureBox1.Image = Image.FromFile(imagePath);
            DicomTranscoder.LoadCodecs(Application.StartupPath, "Dicom.Native*.dll");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter="DCM files(*.DCM)|*.DCM| dcm files(*.dcm)|*.dcm";
                openFileDialog1.RestoreDirectory=true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string dcmPath = openFileDialog1.FileName.ToString();
                    //string dcmFileName = Path.Substring(Path.LastIndexOf("\\") + 1);
                    listView1.Items.Clear();
                    ShowAndDrawingDCMImage(dcmPath);

                }
            }
        }
        /// <summary>
        /// Show the metaInfo of the DCM file and Drawing the image on the Screen
        /// </summary>
        /// <param name="dcmFileName">The path of your DCM file</param>
        private void ShowAndDrawingDCMImage(string dcmFileName)
        {
            try
            {
                DicomFile dcmFile = DicomFile.Open(dcmFileName);
                //Load and Show the meta info of the DCM file
                LoadAndShowDCMMetaInfo(dcmFile);
                listView1.Update();
                //Drawing the image in your DCM file to screen.
                DrawingDCMData2Screen(dcmFile);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


        }
        /// <summary>
        /// Load all of the Elements in the DCM Meta 
        /// </summary>
        /// <param name="dcm">the DICOM file</param>
        private void LoadAndShowDCMMetaInfo(DicomFile dcm)
        {
            //Show the message of DCM file to the listview in the dialog.
            DicomDataset dcmDataset = dcm.Dataset;
            DicomFileMetaInformation dcmMetaInfo = dcm.FileMetaInfo;
            listView1.BeginUpdate();
            //DICOM MetaInfo
            if (!String.IsNullOrWhiteSpace(dcmMetaInfo.MediaStorageSOPClassUID==null?"":dcmMetaInfo.MediaStorageSOPClassUID.ToString()))
            {
                //Insert the ImplementationClassUID
                ListViewItem newItem = new ListViewItem("0002");
                newItem.SubItems.Add("0002");
                newItem.SubItems.Add(dcmMetaInfo.MediaStorageSOPClassUID.ToString());
                listView1.Items.Add(newItem);
            }
            if (!String.IsNullOrWhiteSpace(dcmMetaInfo.MediaStorageSOPInstanceUID==null?"":dcmMetaInfo.MediaStorageSOPInstanceUID.ToString()))
            {
                //Insert the ImplementationClassUID
                ListViewItem newItem = new ListViewItem("0002");
                newItem.SubItems.Add("0003");
                newItem.SubItems.Add(dcmMetaInfo.MediaStorageSOPInstanceUID.ToString());
                listView1.Items.Add(newItem);

            }
            if (!String.IsNullOrWhiteSpace(dcmMetaInfo.TransferSyntax==null?"":dcmMetaInfo.TransferSyntax.ToString()))
            {
                //Insert the ImplementationClassUID
                ListViewItem newItem = new ListViewItem("0002");
                newItem.SubItems.Add("0010");
                newItem.SubItems.Add(dcmMetaInfo.TransferSyntax.ToString());
                listView1.Items.Add(newItem);

            }
            if (!String.IsNullOrWhiteSpace(dcmMetaInfo.ImplementationClassUID==null?"":dcmMetaInfo.ImplementationClassUID.ToString()))
            {
                //Insert the ImplementationClassUID
                ListViewItem newItem = new ListViewItem("0002");
                newItem.SubItems.Add("0012");
                newItem.SubItems.Add(dcmMetaInfo.ImplementationClassUID.ToString());
                listView1.Items.Add(newItem);

            }
            if (!String.IsNullOrWhiteSpace(dcmMetaInfo.ImplementationVersionName==null?"":dcmMetaInfo.ImplementationVersionName.ToString()))
            {
                //Insert the ImplementationClassUID
                ListViewItem newItem = new ListViewItem("0002");
                newItem.SubItems.Add("0013");
                newItem.SubItems.Add(dcmMetaInfo.ImplementationVersionName.ToString());
                listView1.Items.Add(newItem);

            }

            //Patient Info
            string patientName = dcmDataset.Get<string>(DicomTag.PatientName, "");
            if (!String.IsNullOrWhiteSpace(patientName))
            {
                ListViewItem newItem = new ListViewItem("0010");
                newItem.SubItems.Add("0010");
                newItem.SubItems.Add(patientName);
                listView1.Items.Add(newItem);
            }
            string patientID = dcmDataset.Get<string>(DicomTag.PatientID, "");
            if (!String.IsNullOrWhiteSpace(patientID))
            {
                ListViewItem newItem = new ListViewItem("0010");
                newItem.SubItems.Add("0020");
                newItem.SubItems.Add(patientID);
                listView1.Items.Add(newItem);
            }
            string patientBirth = dcmDataset.Get<string>(DicomTag.PatientBirthDate);
            if (!String.IsNullOrWhiteSpace(patientBirth))
            {
                ListViewItem newItem = new ListViewItem("0010");
                newItem.SubItems.Add("0030");
                newItem.SubItems.Add(patientBirth);
                listView1.Items.Add(newItem);
            }
            string patientSex = dcmDataset.Get<string>(DicomTag.PatientSex);
            if (!String.IsNullOrWhiteSpace(patientSex))
            {
                ListViewItem newItem = new ListViewItem("0010");
                newItem.SubItems.Add("0040");
                newItem.SubItems.Add(patientSex);
                listView1.Items.Add(newItem);
            }
            string patientAge = dcmDataset.Get<string>(DicomTag.PatientAge);
            if (!String.IsNullOrWhiteSpace(patientAge))
            {
                ListViewItem newItem = new ListViewItem("0010");
                newItem.SubItems.Add("1010");
                newItem.SubItems.Add(patientAge);
                listView1.Items.Add(newItem);
            }
            //Study & Series Info
            string studyDate = dcmDataset.Get<string>(DicomTag.StudyDate);
            if (!String.IsNullOrWhiteSpace(studyDate))
            {
                ListViewItem newItem = new ListViewItem("0008");
                newItem.SubItems.Add("0020");
                newItem.SubItems.Add(studyDate);
                listView1.Items.Add(newItem);
            }
            string studyTime = dcmDataset.Get<string>(DicomTag.StudyTime);
            if (!String.IsNullOrWhiteSpace(studyTime))
            {
                ListViewItem newItem = new ListViewItem("0008");
                newItem.SubItems.Add("0030");
                newItem.SubItems.Add(studyTime);
                listView1.Items.Add(newItem);
            }
            string studyInstanceID = dcmDataset.Get<string>(DicomTag.StudyInstanceUID);
            if (!String.IsNullOrWhiteSpace(studyInstanceID))
            {
                ListViewItem newItem = new ListViewItem("0020");
                newItem.SubItems.Add("000D");
                newItem.SubItems.Add(studyInstanceID);
                listView1.Items.Add(newItem);
            }
            string seriesInstanceID = dcmDataset.Get<string>(DicomTag.SeriesInstanceUID);
            if (!String.IsNullOrWhiteSpace(seriesInstanceID))
            {
                ListViewItem newItem = new ListViewItem("0020");
                newItem.SubItems.Add("000E");
                newItem.SubItems.Add(seriesInstanceID);
                listView1.Items.Add(newItem);
            }
            string studyID = dcmDataset.Get<string>(DicomTag.StudyID);
            if (!String.IsNullOrWhiteSpace(studyID))
            {
                ListViewItem newItem = new ListViewItem("0020");
                newItem.SubItems.Add("0010");
                newItem.SubItems.Add(studyID);
                listView1.Items.Add(newItem);
            }
            //Image Info
            string imageHeight = dcmDataset.Get<string>(DicomTag.Rows);
            if (!String.IsNullOrWhiteSpace(imageHeight))
            {
                ListViewItem newItem = new ListViewItem("0028");
                newItem.SubItems.Add("0010");
                newItem.SubItems.Add(imageHeight);
                listView1.Items.Add(newItem);
            }
            string imageWidth = dcmDataset.Get<string>(DicomTag.Columns);
            if (!String.IsNullOrWhiteSpace(imageWidth))
            {
                ListViewItem newItem = new ListViewItem("0028");
                newItem.SubItems.Add("0011");
                newItem.SubItems.Add(imageWidth);
                listView1.Items.Add(newItem);
            }
            string bitsAlloctaed = dcmDataset.Get<string>(DicomTag.BitsAllocated);
            if (!String.IsNullOrWhiteSpace(bitsAlloctaed))
            {
                ListViewItem newItem = new ListViewItem("0028");
                newItem.SubItems.Add("0100");
                newItem.SubItems.Add(bitsAlloctaed);
                listView1.Items.Add(newItem);
            }
            string bitsStored = dcmDataset.Get<string>(DicomTag.BitsStored);
            if (!String.IsNullOrWhiteSpace(bitsStored))
            {
                ListViewItem newItem = new ListViewItem("0028");
                newItem.SubItems.Add("0101");
                newItem.SubItems.Add(bitsStored);
                listView1.Items.Add(newItem);
            }
            string highBits = dcmDataset.Get<string>(DicomTag.HighBit);
            if (!String.IsNullOrWhiteSpace(highBits))
            {
                ListViewItem newItem = new ListViewItem("0028");
                newItem.SubItems.Add("0102");
                newItem.SubItems.Add(highBits);
                listView1.Items.Add(newItem);
            }
            listView1.EndUpdate();

        }
        /// <summary>
        /// Drawing the image in your DCM to the Dialog
        /// </summary>
        /// <param name="dcm"></param>
        private void DrawingDCMData2Screen(DicomFile dcm)
        {
            pictureBox1.Image = ConvertDCMData2BitImage(dcm);
        }

        /// <summary>
        /// Convert the byte data retrieved from DCM file to Image
        /// </summary>
        /// <param name="imagedata"></param>
        /// <returns></returns>
        private Bitmap ConvertDCMData2BitImage(DicomFile dcm)
        {
            DicomFile newDcmFile = null;
            if (dcm.FileMetaInfo.TransferSyntax.IsEncapsulated)//if the data is compressed
            {
               // System.Reflection.Assembly.LoadFrom(Path.Combine(Application.StartupPath,"Dicom.Native64.dll"));
                DicomTranscoder.LoadCodecs(Application.StartupPath, "Dicom.Native*.dll");
                newDcmFile=dcm.ChangeTransferSyntax(DicomTransferSyntax.ExplicitVRLittleEndian,new DicomJpegLsParams());
            }
            DicomImage imageDcm=null;
            if (newDcmFile != null)
                imageDcm = new DicomImage(newDcmFile.Dataset);
            else
                imageDcm = new DicomImage(dcm.Dataset);
            DicomDataset dataset = dcm.Dataset;
            byte[] fs = imageDcm.PixelData.NumberOfFrames < 2 ? imageDcm.PixelData.GetFrame(0).Data : imageDcm.PixelData.GetFrame(1).Data;
            uint size = (uint)Marshal.SizeOf(typeof(short));
            uint padding = (uint)fs.Length % size;
            uint count = (uint)fs.Length / size;
            short[] values = new short[count];
            System.Buffer.BlockCopy(fs, 0, values, 0, (int)(fs.Length - padding));

            int height = dataset.Get<int>(DicomTag.Rows);
            int width = dataset.Get<int>(DicomTag.Columns);
            int windowsWidth = (int)dataset.Get<double>(DicomTag.WindowWidth);
            int windowsCenter = (int)dataset.Get<double>(DicomTag.WindowCenter);
            //if the windowsWidth = 0, the DCM file is not standard type.
            if (windowsWidth == 0 || windowsCenter==0)
            {
                windowsWidth = values.Max<short>()-values.Min<short>();
                windowsCenter = windowsWidth / 2;
            }
            int low = windowsCenter - windowsWidth / 2;
            int high = windowsCenter + windowsWidth / 2;
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    int r, g, b;
                    int temp = (int)values[(width - j - 1) * height + i];
                    int val = temp > high ? 255 : (temp < low ? 0 : ((temp - low) * 255 / windowsWidth));
                    r = g = b = val;
                    bitmap.SetPixel(i, width-j-1, Color.FromArgb(r, g, b));
                }
            }
            return bitmap;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The program is just a demo written by C# using fo-dicom!****zssure****");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            pictureBox1.Image = Image.FromFile(defaultImageBG);
            MessageBox.Show("The close is just back to original state.Please click 'X' to exit! ");
        }

    }
}
