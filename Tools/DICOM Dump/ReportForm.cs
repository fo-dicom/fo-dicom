using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dicom;
using Dicom.StructuredReport;

namespace Dicom.Dump {
	public partial class ReportForm : Form {
		private DicomFile _file;

		public ReportForm(DicomFile file) {
			_file = file;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			try {
				tvReport.BeginUpdate();
				tvReport.Nodes.Clear();

				var sr = new DicomStructuredReport(_file.Dataset);
				var node = tvReport.Nodes.Add(sr.ToString());
				foreach (var child in sr.Children())
					AddTreeNode(child, node);

				tvReport.ExpandAll();
				node.EnsureVisible();
				tvReport.EndUpdate();
			} catch (Exception ex) {
				OnException(ex);
			}
		}

		private void AddTreeNode(DicomContentItem item, TreeNode parent) {
			var node = parent.Nodes.Add(item.ToString());

			foreach (var child in item.Children())
				AddTreeNode(child, node);
		}

		private delegate void ExceptionHandler(Exception e);

		protected void OnException(Exception e) {
			if (InvokeRequired) {
				BeginInvoke(new ExceptionHandler(OnException), e);
				return;
			}

			MessageBox.Show(this, e.Message, "Structured Report Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Close();
		}
	}
}
