namespace Dicom.Dump {
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
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemTools = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSyntax = new System.Windows.Forms.ToolStripMenuItem();
			this.explicitVRLittleEndianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.implicitVRLittleEndianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.explicitVRBigEndianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jPEGLosslessP14SV1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jPEG2000LosslessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jPEGLSLosslessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rLELosslessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lvDicom = new System.Windows.Forms.ListView();
			this.columnHeaderTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderVR = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmDicom = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.cmDicom.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuItemTools});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(792, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.menuItemSave});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.OnClickOpen);
			// 
			// menuItemSave
			// 
			this.menuItemSave.Enabled = false;
			this.menuItemSave.Name = "menuItemSave";
			this.menuItemSave.Size = new System.Drawing.Size(152, 22);
			this.menuItemSave.Text = "&Save";
			this.menuItemSave.Click += new System.EventHandler(this.OnClickSave);
			// 
			// menuItemTools
			// 
			this.menuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemView,
            this.menuItemSyntax});
			this.menuItemTools.Name = "menuItemTools";
			this.menuItemTools.Size = new System.Drawing.Size(44, 20);
			this.menuItemTools.Text = "&Tools";
			// 
			// menuItemView
			// 
			this.menuItemView.Enabled = false;
			this.menuItemView.Name = "menuItemView";
			this.menuItemView.Size = new System.Drawing.Size(152, 22);
			this.menuItemView.Text = "&View";
			this.menuItemView.Click += new System.EventHandler(this.OnClickView);
			// 
			// menuItemSyntax
			// 
			this.menuItemSyntax.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.explicitVRLittleEndianToolStripMenuItem,
            this.implicitVRLittleEndianToolStripMenuItem,
            this.explicitVRBigEndianToolStripMenuItem,
            this.jPEGLosslessP14SV1ToolStripMenuItem,
            this.jPEG2000LosslessToolStripMenuItem,
            this.jPEGLSLosslessToolStripMenuItem,
            this.rLELosslessToolStripMenuItem});
			this.menuItemSyntax.Enabled = false;
			this.menuItemSyntax.Name = "menuItemSyntax";
			this.menuItemSyntax.Size = new System.Drawing.Size(152, 22);
			this.menuItemSyntax.Text = "&Change Syntax";
			// 
			// explicitVRLittleEndianToolStripMenuItem
			// 
			this.explicitVRLittleEndianToolStripMenuItem.Name = "explicitVRLittleEndianToolStripMenuItem";
			this.explicitVRLittleEndianToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.explicitVRLittleEndianToolStripMenuItem.Text = "Explicit VR Little Endian";
			this.explicitVRLittleEndianToolStripMenuItem.Click += new System.EventHandler(this.OnClickExplicitVRLittleEndian);
			// 
			// implicitVRLittleEndianToolStripMenuItem
			// 
			this.implicitVRLittleEndianToolStripMenuItem.Name = "implicitVRLittleEndianToolStripMenuItem";
			this.implicitVRLittleEndianToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.implicitVRLittleEndianToolStripMenuItem.Text = "Implicit VR Little Endian";
			this.implicitVRLittleEndianToolStripMenuItem.Click += new System.EventHandler(this.OnClickImplicitVRLittleEndian);
			// 
			// explicitVRBigEndianToolStripMenuItem
			// 
			this.explicitVRBigEndianToolStripMenuItem.Name = "explicitVRBigEndianToolStripMenuItem";
			this.explicitVRBigEndianToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.explicitVRBigEndianToolStripMenuItem.Text = "Explicit VR Big Endian";
			this.explicitVRBigEndianToolStripMenuItem.Click += new System.EventHandler(this.OnClickExplicitVRBigEndian);
			// 
			// jPEGLosslessP14SV1ToolStripMenuItem
			// 
			this.jPEGLosslessP14SV1ToolStripMenuItem.Name = "jPEGLosslessP14SV1ToolStripMenuItem";
			this.jPEGLosslessP14SV1ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.jPEGLosslessP14SV1ToolStripMenuItem.Text = "JPEG Lossless P14 SV1";
			this.jPEGLosslessP14SV1ToolStripMenuItem.Click += new System.EventHandler(this.OnClickJPEGLosslessP14SV1);
			// 
			// jPEG2000LosslessToolStripMenuItem
			// 
			this.jPEG2000LosslessToolStripMenuItem.Name = "jPEG2000LosslessToolStripMenuItem";
			this.jPEG2000LosslessToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.jPEG2000LosslessToolStripMenuItem.Text = "JPEG 2000 Lossless";
			this.jPEG2000LosslessToolStripMenuItem.Click += new System.EventHandler(this.OnClickJPEG2000Lossless);
			// 
			// jPEGLSLosslessToolStripMenuItem
			// 
			this.jPEGLSLosslessToolStripMenuItem.Name = "jPEGLSLosslessToolStripMenuItem";
			this.jPEGLSLosslessToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.jPEGLSLosslessToolStripMenuItem.Text = "JPEG-LS Lossless";
			this.jPEGLSLosslessToolStripMenuItem.Click += new System.EventHandler(this.OnClickJPEGLSLossless);
			// 
			// rLELosslessToolStripMenuItem
			// 
			this.rLELosslessToolStripMenuItem.Name = "rLELosslessToolStripMenuItem";
			this.rLELosslessToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.rLELosslessToolStripMenuItem.Text = "RLE Lossless";
			this.rLELosslessToolStripMenuItem.Click += new System.EventHandler(this.OnClickRLELossless);
			// 
			// lvDicom
			// 
			this.lvDicom.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTag,
            this.columnHeaderVR,
            this.columnHeaderLength,
            this.columnHeaderValue});
			this.lvDicom.ContextMenuStrip = this.cmDicom;
			this.lvDicom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvDicom.FullRowSelect = true;
			this.lvDicom.Location = new System.Drawing.Point(0, 24);
			this.lvDicom.MultiSelect = false;
			this.lvDicom.Name = "lvDicom";
			this.lvDicom.Size = new System.Drawing.Size(792, 549);
			this.lvDicom.TabIndex = 1;
			this.lvDicom.UseCompatibleStateImageBehavior = false;
			this.lvDicom.View = System.Windows.Forms.View.Details;
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
			// cmDicom
			// 
			this.cmDicom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyValueToolStripMenuItem});
			this.cmDicom.Name = "cmDicom";
			this.cmDicom.Size = new System.Drawing.Size(129, 26);
			this.cmDicom.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuOpening);
			// 
			// copyValueToolStripMenuItem
			// 
			this.copyValueToolStripMenuItem.Name = "copyValueToolStripMenuItem";
			this.copyValueToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.copyValueToolStripMenuItem.Text = "&Copy Value";
			this.copyValueToolStripMenuItem.Click += new System.EventHandler(this.OnClickContextMenuCopyValue);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 573);
			this.Controls.Add(this.lvDicom);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "DICOM Dump";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.cmDicom.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ListView lvDicom;
		private System.Windows.Forms.ColumnHeader columnHeaderTag;
		private System.Windows.Forms.ColumnHeader columnHeaderVR;
		private System.Windows.Forms.ColumnHeader columnHeaderLength;
		private System.Windows.Forms.ColumnHeader columnHeaderValue;
		private System.Windows.Forms.ContextMenuStrip cmDicom;
		private System.Windows.Forms.ToolStripMenuItem copyValueToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuItemSave;
		private System.Windows.Forms.ToolStripMenuItem menuItemTools;
		private System.Windows.Forms.ToolStripMenuItem menuItemView;
		private System.Windows.Forms.ToolStripMenuItem menuItemSyntax;
		private System.Windows.Forms.ToolStripMenuItem jPEGLosslessP14SV1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem explicitVRLittleEndianToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem implicitVRLittleEndianToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem explicitVRBigEndianToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem jPEG2000LosslessToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem jPEGLSLosslessToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem rLELosslessToolStripMenuItem;
	}
}

