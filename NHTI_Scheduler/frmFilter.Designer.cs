namespace NHTI_Scheduler
{
    partial class frmFilter
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
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.chkBoxRemaining = new System.Windows.Forms.CheckBox();
            this.txtRemainingStudentsMin = new System.Windows.Forms.TextBox();
            this.chkBoxDays = new System.Windows.Forms.CheckBox();
            this.txtDaysToInclude = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdApply
            // 
            this.cmdApply.Location = new System.Drawing.Point(203, 141);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(84, 31);
            this.cmdApply.TabIndex = 0;
            this.cmdApply.Text = "Apply";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(309, 141);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 31);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // chkBoxRemaining
            // 
            this.chkBoxRemaining.AutoSize = true;
            this.chkBoxRemaining.Location = new System.Drawing.Point(23, 34);
            this.chkBoxRemaining.Name = "chkBoxRemaining";
            this.chkBoxRemaining.Size = new System.Drawing.Size(251, 21);
            this.chkBoxRemaining.TabIndex = 3;
            this.chkBoxRemaining.Text = "Remaining Students Greater Than:";
            this.chkBoxRemaining.UseVisualStyleBackColor = true;
            // 
            // txtRemainingStudentsMin
            // 
            this.txtRemainingStudentsMin.Location = new System.Drawing.Point(289, 34);
            this.txtRemainingStudentsMin.Name = "txtRemainingStudentsMin";
            this.txtRemainingStudentsMin.Size = new System.Drawing.Size(95, 22);
            this.txtRemainingStudentsMin.TabIndex = 4;
            this.txtRemainingStudentsMin.Text = "0";
            this.txtRemainingStudentsMin.TextChanged += new System.EventHandler(this.txtRemainingStudentsMin_TextChanged);
            // 
            // chkBoxDays
            // 
            this.chkBoxDays.AutoSize = true;
            this.chkBoxDays.Location = new System.Drawing.Point(23, 76);
            this.chkBoxDays.Name = "chkBoxDays";
            this.chkBoxDays.Size = new System.Drawing.Size(224, 21);
            this.chkBoxDays.TabIndex = 5;
            this.chkBoxDays.Text = "Days - Include only (MTWRFS)";
            this.chkBoxDays.UseVisualStyleBackColor = true;
            // 
            // txtDaysToInclude
            // 
            this.txtDaysToInclude.Location = new System.Drawing.Point(289, 76);
            this.txtDaysToInclude.Name = "txtDaysToInclude";
            this.txtDaysToInclude.Size = new System.Drawing.Size(95, 22);
            this.txtDaysToInclude.TabIndex = 6;
            this.txtDaysToInclude.Text = "MTWRFS";
            this.txtDaysToInclude.TextChanged += new System.EventHandler(this.txtDaysToInclude_TextChanged);
            this.txtDaysToInclude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDaysToInclude_KeyPress);
            // 
            // frmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 208);
            this.Controls.Add(this.txtDaysToInclude);
            this.Controls.Add(this.chkBoxDays);
            this.Controls.Add(this.txtRemainingStudentsMin);
            this.Controls.Add(this.chkBoxRemaining);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFilter";
            this.Text = "Filter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.CheckBox chkBoxRemaining;
        private System.Windows.Forms.TextBox txtRemainingStudentsMin;
        private System.Windows.Forms.CheckBox chkBoxDays;
        private System.Windows.Forms.TextBox txtDaysToInclude;
    }
}