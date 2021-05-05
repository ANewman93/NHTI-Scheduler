namespace NHTI_Scheduler
{
    partial class frmBmpViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdSaveBmp = new System.Windows.Forms.Button();
            this.pictBoxImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSaveBmp
            // 
            this.cmdSaveBmp.Location = new System.Drawing.Point(12, 12);
            this.cmdSaveBmp.Name = "cmdSaveBmp";
            this.cmdSaveBmp.Size = new System.Drawing.Size(75, 23);
            this.cmdSaveBmp.TabIndex = 0;
            this.cmdSaveBmp.Text = "Save";
            this.cmdSaveBmp.UseVisualStyleBackColor = true;
            // 
            // pictBoxImage
            // 
            this.pictBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictBoxImage.Location = new System.Drawing.Point(12, 54);
            this.pictBoxImage.Name = "pictBoxImage";
            this.pictBoxImage.Size = new System.Drawing.Size(676, 533);
            this.pictBoxImage.TabIndex = 1;
            this.pictBoxImage.TabStop = false;
            // 
            // frmBmpViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 599);
            this.Controls.Add(this.pictBoxImage);
            this.Controls.Add(this.cmdSaveBmp);
            this.MinimumSize = new System.Drawing.Size(315, 230);
            this.Name = "frmBmpViewer";
            this.Text = "Bitmap View";
            this.Load += new System.EventHandler(this.frmBmpViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdSaveBmp;
        private System.Windows.Forms.PictureBox pictBoxImage;
    }
}