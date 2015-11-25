using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dicom.Network;
using System.Threading;
using System.IO;

namespace C_Store_SCU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                tbDir.Text = folderBrowserDialog1.SelectedPath.Trim();
        }

        private void btnCStore_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(tbDir.Text);
            foreach (var file in files)
            {
                ThreadPool.QueueUserWorkItem(CStoreTask, file);
            }
        }
        private static int index = 0;
        private void CStoreTask(object state)
        {
            Interlocked.Increment(ref index);
            string file = (string)state;
            DicomCStoreRequest cstoreReq = new DicomCStoreRequest(file);
            DicomClient client = new DicomClient();
            client.AddRequest(cstoreReq);
            client.Send("127.0.0.1", 104, false, string.Format("ZSSURE_{0}",index), "STORESCP");
        }
    }
}
