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
			this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuItemView});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(792, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.OnClickOpen);
			// 
			// menuItemView
			// 
			this.menuItemView.Enabled = false;
			this.menuItemView.Name = "menuItemView";
			this.menuItemView.Size = new System.Drawing.Size(41, 20);
			this.menuItemView.Text = "&View";
			this.menuItemView.Click += new System.EventHandler(this.OnClickView);
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
			this.cmDicom.Size = new System.Drawing.Size(153, 48);
			this.cmDicom.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuOpening);
			// 
			// copyValueToolStripMenuItem
			// 
			this.copyValueToolStripMenuItem.Name = "copyValueToolStripMenuItem";
			this.copyValueToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
		private System.Windows.Forms.ToolStripMenuItem menuItemView;
		private System.Windows.Forms.ContextMenuStrip cmDicom;
		private System.Windows.Forms.ToolStripMenuItem copyValueToolStripMenuItem;
	}
}

