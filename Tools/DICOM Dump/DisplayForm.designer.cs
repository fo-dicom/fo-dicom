namespace Dicom.Dump {
	partial class DisplayForm {
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
			this.pbDisplay = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbDisplay)).BeginInit();
			this.SuspendLayout();
			// 
			// pbDisplay
			// 
			this.pbDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbDisplay.Location = new System.Drawing.Point(0, 0);
			this.pbDisplay.Name = "pbDisplay";
			this.pbDisplay.Size = new System.Drawing.Size(284, 262);
			this.pbDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbDisplay.TabIndex = 0;
			this.pbDisplay.TabStop = false;
			this.pbDisplay.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
			this.pbDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
			this.pbDisplay.MouseLeave += new System.EventHandler(this.OnMouseLeave);
			this.pbDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			this.pbDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
			// 
			// DisplayForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.pbDisplay);
			this.DoubleBuffered = true;
			this.MinimizeBox = false;
			this.Name = "DisplayForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DICOM Image Display";
			this.ClientSizeChanged += new System.EventHandler(this.OnClientSizeChanged);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
			this.StyleChanged += new System.EventHandler(this.OnClientSizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.pbDisplay)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbDisplay;
	}
}