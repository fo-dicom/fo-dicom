namespace Dicom.Compare {
	partial class MainForm {
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cbIgnorePixelData = new System.Windows.Forms.ToolStripMenuItem();
			this.cbIgnoreUIDs = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lblFile1 = new System.Windows.Forms.Label();
			this.lblFile2 = new System.Windows.Forms.Label();
			this.cbIgnoreVR = new System.Windows.Forms.ToolStripMenuItem();
			this.cbIgnoreGroupLengths = new System.Windows.Forms.ToolStripMenuItem();
			this.lvFile1 = new Dicom.Compare.ListViewEx();
			this.columnHeaderTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderVR = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvFile2 = new Dicom.Compare.ListViewEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFilesToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// selectFilesToolStripMenuItem
			// 
			this.selectFilesToolStripMenuItem.Name = "selectFilesToolStripMenuItem";
			this.selectFilesToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.selectFilesToolStripMenuItem.Text = "&Select Files";
			this.selectFilesToolStripMenuItem.Click += new System.EventHandler(this.OnClickSelect);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbIgnorePixelData,
            this.cbIgnoreUIDs,
            this.cbIgnoreVR,
            this.cbIgnoreGroupLengths});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// cbIgnorePixelData
			// 
			this.cbIgnorePixelData.Checked = true;
			this.cbIgnorePixelData.CheckOnClick = true;
			this.cbIgnorePixelData.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIgnorePixelData.Name = "cbIgnorePixelData";
			this.cbIgnorePixelData.Size = new System.Drawing.Size(189, 22);
			this.cbIgnorePixelData.Text = "Ignore Pixel Data";
			this.cbIgnorePixelData.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
			// 
			// cbIgnoreUIDs
			// 
			this.cbIgnoreUIDs.Checked = true;
			this.cbIgnoreUIDs.CheckOnClick = true;
			this.cbIgnoreUIDs.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIgnoreUIDs.Name = "cbIgnoreUIDs";
			this.cbIgnoreUIDs.Size = new System.Drawing.Size(189, 22);
			this.cbIgnoreUIDs.Text = "Ignore UIDs";
			this.cbIgnoreUIDs.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lvFile1);
			this.splitContainer1.Panel1.Controls.Add(this.lblFile1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lvFile2);
			this.splitContainer1.Panel2.Controls.Add(this.lblFile2);
			this.splitContainer1.Size = new System.Drawing.Size(1008, 705);
			this.splitContainer1.SplitterDistance = 504;
			this.splitContainer1.TabIndex = 1;
			// 
			// lblFile1
			// 
			this.lblFile1.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblFile1.Location = new System.Drawing.Point(0, 0);
			this.lblFile1.Name = "lblFile1";
			this.lblFile1.Size = new System.Drawing.Size(502, 25);
			this.lblFile1.TabIndex = 0;
			this.lblFile1.Text = "(none)";
			this.lblFile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFile2
			// 
			this.lblFile2.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblFile2.Location = new System.Drawing.Point(0, 0);
			this.lblFile2.Name = "lblFile2";
			this.lblFile2.Size = new System.Drawing.Size(498, 25);
			this.lblFile2.TabIndex = 1;
			this.lblFile2.Text = "(none)";
			this.lblFile2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbIgnoreVR
			// 
			this.cbIgnoreVR.Checked = true;
			this.cbIgnoreVR.CheckOnClick = true;
			this.cbIgnoreVR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIgnoreVR.Name = "cbIgnoreVR";
			this.cbIgnoreVR.Size = new System.Drawing.Size(189, 22);
			this.cbIgnoreVR.Text = "Ignore VR";
			this.cbIgnoreVR.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
			// 
			// cbIgnoreGroupLengths
			// 
			this.cbIgnoreGroupLengths.CheckOnClick = true;
			this.cbIgnoreGroupLengths.Name = "cbIgnoreGroupLengths";
			this.cbIgnoreGroupLengths.Size = new System.Drawing.Size(189, 22);
			this.cbIgnoreGroupLengths.Text = "Ignore Group Lengths";
			this.cbIgnoreGroupLengths.CheckedChanged += new System.EventHandler(this.OnOptionChanged);
			// 
			// lvFile1
			// 
			this.lvFile1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTag,
            this.columnHeaderVR,
            this.columnHeaderLength,
            this.columnHeaderValue});
			this.lvFile1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFile1.FullRowSelect = true;
			this.lvFile1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvFile1.HideSelection = false;
			this.lvFile1.Location = new System.Drawing.Point(0, 25);
			this.lvFile1.MultiSelect = false;
			this.lvFile1.Name = "lvFile1";
			this.lvFile1.Size = new System.Drawing.Size(502, 678);
			this.lvFile1.TabIndex = 2;
			this.lvFile1.UseCompatibleStateImageBehavior = false;
			this.lvFile1.View = System.Windows.Forms.View.Details;
			this.lvFile1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScroll);
			this.lvFile1.SelectedIndexChanged += new System.EventHandler(this.OnSelect);
			this.lvFile1.SizeChanged += new System.EventHandler(this.OnSizeChanged);
			this.lvFile1.MouseEnter += new System.EventHandler(this.OnMouseEnter);
			// 
			// columnHeaderTag
			// 
			this.columnHeaderTag.Text = "Tag";
			this.columnHeaderTag.Width = 230;
			// 
			// columnHeaderVR
			// 
			this.columnHeaderVR.Text = "VR";
			this.columnHeaderVR.Width = 40;
			// 
			// columnHeaderLength
			// 
			this.columnHeaderLength.Text = "Length";
			// 
			// columnHeaderValue
			// 
			this.columnHeaderValue.Text = "Value";
			this.columnHeaderValue.Width = 440;
			// 
			// lvFile2
			// 
			this.lvFile2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.lvFile2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFile2.FullRowSelect = true;
			this.lvFile2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvFile2.HideSelection = false;
			this.lvFile2.Location = new System.Drawing.Point(0, 25);
			this.lvFile2.MultiSelect = false;
			this.lvFile2.Name = "lvFile2";
			this.lvFile2.Size = new System.Drawing.Size(498, 678);
			this.lvFile2.TabIndex = 3;
			this.lvFile2.UseCompatibleStateImageBehavior = false;
			this.lvFile2.View = System.Windows.Forms.View.Details;
			this.lvFile2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScroll);
			this.lvFile2.SelectedIndexChanged += new System.EventHandler(this.OnSelect);
			this.lvFile2.SizeChanged += new System.EventHandler(this.OnSizeChanged);
			this.lvFile2.MouseEnter += new System.EventHandler(this.OnMouseEnter);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Tag";
			this.columnHeader1.Width = 230;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "VR";
			this.columnHeader2.Width = 40;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Length";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Value";
			this.columnHeader4.Width = 440;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 729);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DICOM Compare";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label lblFile1;
		private Dicom.Compare.ListViewEx lvFile1;
		private System.Windows.Forms.ColumnHeader columnHeaderTag;
		private System.Windows.Forms.ColumnHeader columnHeaderVR;
		private System.Windows.Forms.ColumnHeader columnHeaderLength;
		private System.Windows.Forms.ColumnHeader columnHeaderValue;
		private Dicom.Compare.ListViewEx lvFile2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label lblFile2;
		private System.Windows.Forms.ToolStripMenuItem selectFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cbIgnorePixelData;
		private System.Windows.Forms.ToolStripMenuItem cbIgnoreUIDs;
		private System.Windows.Forms.ToolStripMenuItem cbIgnoreVR;
		private System.Windows.Forms.ToolStripMenuItem cbIgnoreGroupLengths;
	}
}

