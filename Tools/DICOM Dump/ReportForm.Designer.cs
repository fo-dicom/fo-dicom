namespace Dicom.Dump {
	partial class ReportForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tvReport = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// tvReport
			// 
			this.tvReport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvReport.Location = new System.Drawing.Point(0, 0);
			this.tvReport.Name = "tvReport";
			this.tvReport.ShowRootLines = false;
			this.tvReport.Size = new System.Drawing.Size(784, 561);
			this.tvReport.TabIndex = 2;
			// 
			// ReportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.tvReport);
			this.Name = "ReportForm";
			this.ShowInTaskbar = false;
			this.Text = "Structured Report";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tvReport;
	}
}